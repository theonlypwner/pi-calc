Partial Public Class libbcmath
	' Constants
	Public Const PLUS As Char = "+"c
	Public Const MINUS As Char = "-"c
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
	End Structure

	''' <summary>Creates a new instance of a structure representing an arbitrary precision number</summary>
	''' <param name="length">The length before the decimal</param>
	''' <param name="scale">The length after the decimal</param>
	Public Shared Function new_num(ByVal length As Integer, ByVal scale As Integer) As BCNum
		Dim temp As New BCNum
		temp.sign = PLUS
		temp.length = length
		temp.scale = scale
		temp.value = libbcmath.safe_emalloc(length + scale)
		libbcmath.memset(temp.value, 0, 0, length + scale)
		Return temp
	End Function

	''' <summary>Create a new arbitrary precision number</summary>
	Shared Function init_num() As BCNum
		Return libbcmath.new_num(1, 0)
	End Function

	''' <summary>Strips zeros until there is only one left</summary>
	''' <param name="num">The arbitrary precision number to strip the zeros from</param>
	Public Shared Sub RemoveLeadingZeros(ByRef num As BCNum)
		' We can move value to point to the first non zero digit!
		While num.value(0) = 0 And num.length > 1
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
			Return init_num() ' invalid number, return zero
		End If

		' Adjust numbers and allocate storage and initialize fields.
		strscale = Math.Min(strscale, scale)
		If digits = 0 Then
			zero_int = True
			digits = 1
		End If

		num = new_num(digits, strscale)

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
			num.value(0) = 0
			nptr += 1
			digits = 0
		End If
		While digits > 0
			num.value(nptr) = CByte(Val(str(ptr)))
			nptr += 1
			ptr += 1
			digits -= 1
		End While

		' Build the fractional part
		If strscale > 0 Then
			ptr += 1 ' skip the decimal point!
			While strscale > 0
				num.value(nptr) = CByte(Val(str(ptr)))
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
	Public Shared Function IsZero(ByRef num As BCNum) As Boolean
		Dim count As Integer = num.length + num.scale, nptr As Integer = 0
		While count > 0 And num.value(nptr) = 0
			nptr += 1
			count -= 1
		End While
		Return count = 0
	End Function
End Class
