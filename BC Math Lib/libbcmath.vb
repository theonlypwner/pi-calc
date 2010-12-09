Partial Public NotInheritable Class libbcmath
	' Constants
	Public Const PLUS As Char = "+"c
	Public Const MINUS As Char = "-"c
	Public Const BASE As Byte = 10 ' must be 10
	Public Const defaultScale As Integer = 0

	''' <summary>Basic Number Structure</summary>
	Public Structure BCNum
		''' <summary>The sign of the character</summary>
		Dim sign As Char
		''' <summary>Digits before the decimal point</summary>
		Dim length As Integer
		''' <summary>Digits after the decimal point</summary>
		Dim scale As Integer
		''' <summary>Array for value; 1.23 = [1, 2, 3]</summary>
		Dim value As List(Of Byte)

		''' <summary>Converts this number to a string</summary>
		Overrides Function ToString() As String
			Dim r As String = "", tmp As String = ""
			For Each c As Byte In value
				tmp &= c.ToString
			Next
			' add minus sign (if applicable) then add the integer part
			r = If(Me.sign = libbcmath.PLUS, "", Me.sign) & tmp.Substring(0, Me.length)
			' if there are decimal places, add a . and the decimal part
			If Me.scale > 0 Then
				r &= "."c + tmp.Substring(Me.length, Me.scale)
			End If
			Return r
		End Function

		''' <summary>Gets a shallow copy of this instance of BCNum</summary>
		Public Function GetShallowCopy() As BCNum
			Return CreateShallowCopy(Me)
		End Function

		''' <summary>Creates a shallow copy (memberwise clone) of a BCNum</summary>
		''' <param name="obj">An instance of BCNum</param>
		Public Shared Function CreateShallowCopy(ByVal obj As BCNum) As BCNum
			Return CType(obj.MemberwiseClone, BCNum)
		End Function

		''' <summary>Get or set the value from a specific index in the value list</summary>
		''' <param name="i">The index of the location</param>
		Default Property Item(ByVal i As Integer) As Byte
			Get
				Return value.Item(i)
			End Get
			Set(ByVal val As Byte)
				value.Item(i) = val
			End Set
		End Property

		Shared Widening Operator CType(ByVal i As Integer) As BCNum
			Return BCMath.IntToNum(i)
		End Operator
	End Structure

	''' <summary>Creates a new instance of a structure representing an arbitrary precision number</summary>
	''' <param name="length">The length before the decimal</param>
	''' <param name="scale">The length after the decimal</param>
	Public Shared Function NewNum(ByVal length As Integer, ByVal scale As Integer) As BCNum
		Dim temp As New BCNum
		temp.sign = PLUS
		temp.length = length
		temp.scale = scale
		temp.value = libbcmath.safe_emalloc(length + scale)
		libbcmath.memset(temp.value, 0, 0, length + scale)
		Return temp
	End Function

	''' <summary>Create a new arbitrary precision number</summary>
	Shared Function InitNum() As BCNum
		Return libbcmath.NewNum(1, 0)
	End Function

	''' <summary>Strips zeros until there is only one left</summary>
	''' <param name="num">The arbitrary precision number to strip the zeros from</param>
	Friend Shared Sub RemoveLeadingZeros(ByRef num As BCNum)
		' We can move value to point to the first non zero digit!
		While num(0) = 0 And num.length > 1
			num.value.RemoveAt(0)
			num.length -= 1
		End While
	End Sub

	''' <summary>Convert to an arbitrary precision detecting the proper scale</summary>
	''' <param name="str">The string to convert</param>
	Public Overloads Shared Function str2num(ByVal str As String) As BCNum
		Dim p As Integer = str.IndexOf("."c)
		If p = -1 Then
			Return libbcmath.str2num(str, 0)
		Else
			Return libbcmath.str2num(str, (str.Length - p))
		End If
	End Function

	Public Overloads Shared Function str2num(ByVal s As String, ByVal scale As Integer) As BCNum
		Dim str As Char() = s.ToCharArray, num As New BCNum
		Dim ptr = 0, digits = 0, strscale = 0, nptr = 0
		Dim zero_int As Boolean = False
		If str(0) = PLUS Or s(0) = MINUS Then ' skip sign
			ptr += 1 ' next
		End If
		While str(ptr) = "0"c ' skip leading zeros
			ptr += 1 'next
		End While
		While Char.IsDigit(str(ptr)) ' count digits
			ptr += 1 ' next
			digits += 1	' counted a digit
		End While
		If str(ptr) = "."c Then ptr += 1 ' skip decimal point
		While (Char.IsDigit(str(ptr))) ' count digits after decimal point
			ptr += 1 ' next character
			strscale += 1 ' digits after decimal point
		End While

		If str(ptr) <> Nothing Or digits + strscale = 0 Then
			Return InitNum() ' invalid number, return zero
		End If

		' Adjust numbers and allocate storage and initialize fields.
		strscale = Math.Min(strscale, scale)
		If digits = 0 Then
			zero_int = True
			digits = 1
		End If

		num = NewNum(digits, strscale)

		' Build the whole number
		ptr = 1
		If str(0) = MINUS Then : num.sign = MINUS
		Else
			num.sign = PLUS
			If Not str(0) = PLUS Then ptr = 0
		End If

		' now we start all over again with the counting :(
		While str(ptr) = "0"c ' skip leading zeros again
			ptr += 1
		End While

		' Everything before the decimal
		nptr = 0 ' destination pointer
		If zero_int Then
			num(0) = 0
			nptr += 1
			digits = 0
		End If
		While digits > 0
			num(nptr) = CByte(Val(str(ptr)))
			nptr += 1
			ptr += 1
			digits -= 1
		End While

		' Build the fractional part
		If strscale > 0 Then
			ptr += 1 ' skip the decimal point!
			While strscale > 0
				num(nptr) = CByte(Val(str(ptr)))
				nptr += 1
				ptr += 1
				strscale -= 1
			End While
		End If

		' Finally, return the result
		Return num
	End Function

	''' <summary>Determines if a number (up to Long) is odd or even</summary>
	''' <param name="a">The number to see if it is odd</param>
	Public Shared Function Odd(ByVal a As Long) As Boolean
		Return (a And 1) > 0
	End Function

	''' <summary>Determine if the arbitrary precision number specified is zero or not</summary>
	''' <param name="num">The number to check</param>
	Public Shared Function IsZero(ByVal num As BCNum) As Boolean
		Dim count As Integer = num.length + num.scale, nptr As Integer = 0
		While count > 0 And num(nptr) = 0
			nptr += 1
			count -= 1
		End While
		Return count = 0
	End Function

	''' <summary>Determine if the arbitrary precision number specified is one</summary>
	''' <param name="num">The number to check</param>
	Public Shared Function IsOne(ByVal num As BCNum) As Boolean
		Dim i As Integer
		For i = num.length To num.length + num.scale - 1
			If num(i) <> 0 Then Return False ' Decimal found
		Next
		For i = 0 To num.length - 2
			If num(i) <> 0 Then Return False ' Non-zero before last number
		Next
		Return num(num.length - 1) = 1 ' If the last digit is one
	End Function

	''' <summary>Inverts the sign (- => +, + => -)</summary>
	''' <param name="sign">The character with the sign to invert</param>
	Public Shared Function InvertSign(ByVal sign As Char) As Char
		Return If(sign = PLUS, MINUS, PLUS)
	End Function
End Class
