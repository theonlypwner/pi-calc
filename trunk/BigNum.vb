Public Class BigNumber
	Private number As Byte()
	Private size As Integer
	Private maxDigits As Integer

	Public Sub New(ByVal maxDigits As Integer)
		Me.maxDigits = maxDigits
		Me.size = CInt(Math.Ceiling(CSng(maxDigits) * 0.104)) + 2
		number = New Byte(size - 1) {}
	End Sub

	Public Sub New(ByVal maxDigits As Integer, ByVal intPart As Byte)
		Me.New(maxDigits)
		number(0) = intPart
		For i As Integer = 1 To size - 1
			number(i) = 0
		Next
	End Sub

	Private Sub VerifySameSize(ByVal value As BigNumber)
		If [Object].ReferenceEquals(Me, value) Then
			Throw New Exception("BigNumbers cannot operate on themselves")
		End If
		If value.size <> Me.size Then
			Throw New Exception("BigNumbers must have the same size")
		End If
	End Sub

	Public Sub Add(ByVal value As BigNumber)
		VerifySameSize(value)

		Dim index As Integer = size - 1
		While index >= 0 AndAlso value.number(index) = 0
			index -= 1
		End While

		Dim carry As UInt32 = 0
		While index >= 0
			Dim result As UInt64 = CType(number(index), UInt64) + value.number(index) + carry
			number(index) = CByte(result Mod (Byte.MaxValue + 1))
			If result >= &H100000000UL Then
				carry = 1
			Else
				carry = 0
			End If
			index -= 1
		End While
	End Sub

	Public Sub Subtract(ByVal value As BigNumber)
		VerifySameSize(value)

		Dim index As Integer = size - 1
		While index >= 0 AndAlso value.number(index) = 0
			index -= 1
		End While

		Dim borrow As UInt32 = 0
		While index >= 0
			Dim result As UInt64 = &H100000000UL + CType(number(index), UInt64) - value.number(index) - borrow
			number(index) = CByte(result Mod (Byte.MaxValue + 1))
			If result >= &H100000000UL Then
				borrow = 0
			Else
				borrow = 1
			End If
			index -= 1
		End While
	End Sub

	Public Sub Multiply(ByVal value As UInt32)
		Dim index As Integer = size - 1
		While index >= 0 AndAlso number(index) = 0
			index -= 1
		End While

		Dim carry As UInt32 = 0
		While index >= 0
			Dim result As UInt64 = CType(number(index), UInt64) * value + carry
			number(index) = CByte(result Mod (Byte.MaxValue + 1))
			carry = CType(result >> 32, UInt32)
			index -= 1
		End While
	End Sub

	Public Sub Divide(ByVal value As UInt32)
		Dim index As Integer = 0
		While index < size AndAlso number(index) = 0
			index += 1
		End While

		Dim carry As UInt32 = 0
		While index < size
			Dim result As UInt64 = CULng((number(index) + (CType(carry, UInt64) << 32)) / CType(value, UInt64))
			number(index) = CByte(result Mod (Byte.MaxValue + 1))
			carry = CType(result Mod CType(value, UInt64), UInt32)
			index += 1
		End While
	End Sub

	Public Sub Assign(ByVal value As BigNumber)
		VerifySameSize(value)
		For i As Integer = 0 To size - 1
			number(i) = value.number(i)
		Next
	End Sub
	Public Function IsZero() As Boolean
		For Each item As UInt32 In number
			If item <> 0 Then
				Return False
			End If
		Next
		Return True
	End Function

	Public Sub ArcTan(ByVal multiplicand As Byte, ByVal reciprocal As UInt32)
		Dim X As New BigNumber(maxDigits, multiplicand)
		X.Divide(reciprocal)
		reciprocal *= reciprocal

		Me.Assign(X)

		Dim term As New BigNumber(maxDigits)
		Dim divisor As UInt32 = 1
		Dim subtractTerm As Boolean = True
		While True
			X.Divide(reciprocal)
			term.Assign(X)
			divisor = CUInt(divisor + 2)
			term.Divide(divisor)
			If term.IsZero() Then
				Exit While
			End If

			If subtractTerm Then
				Me.Subtract(term)
			Else
				Me.Add(term)
			End If
			subtractTerm = Not subtractTerm
		End While
	End Sub

	' stupid property macro hacks

	Default ReadOnly Property Item(ByVal i As Integer) As UInteger
		Get
			Return number(i)
		End Get
	End Property

	Public ReadOnly Property Length As Integer
		Get
			Return size
		End Get
	End Property
End Class