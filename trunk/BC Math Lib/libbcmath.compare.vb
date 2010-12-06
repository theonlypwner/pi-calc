Partial Public Class libbcmath
	''' <summary>Compare two arbitrary precision numbers</summary>
	''' <param name="n1">The first number (1 if larger)</param>
	''' <param name="n2">The second number (-1 if larger)</param>
	Public Shared Function DoCompare(ByRef n1 As BCNum, ByRef n2 As BCNum) As SByte
		Return DoCompareAdvanced(n1, n2, True, False)
	End Function

	''' <summary>Raw compare of two arbitrary precision numbers</summary>
	''' <param name="n1">The first number (1 if larger)</param>
	''' <param name="n2">The second number (-1 if larger)</param>
	''' <param name="use_sign">Check the signs, only magnitude is checked if false</param>
	''' <param name="ignore_last">Whether or not to ignore the last digit</param>
	Protected Shared Function DoCompareAdvanced(ByRef n1 As BCNum, ByRef n2 As BCNum, ByVal use_sign As Boolean, ByVal ignore_last As Boolean) As SByte
		Dim n1ptr As Integer = 0, n2ptr As Integer = 0, count As Integer = 0

		' First, compare signs.
		If use_sign And (n1.sign <> n2.sign) Then
			Return CSByte(If(n1.sign = PLUS, 1, -1)) ' POSITIVE n1 > NEGATIVE n2 = 1
		End If ' the signs must be the same now!

		' Now compare the magnitude.
		If n1.length > n2.length Then
			Return CSByte(If((Not use_sign) Or n1.sign = PLUS, 1, -1))
		ElseIf n1.length < n2.length Then
			Return CSByte(If((Not use_sign) Or n1.sign = PLUS, -1, 1)) ' same sign
		End If

		' If we get here, they have the same number of integer digits.
		' Check the integer part and the equal length part of the fraction.
		count = n1.length + Math.Min(n1.scale, n2.scale)
		While (count > 0) And (n1(n1ptr) = n2(n2ptr))
			n1ptr += 1
			n2ptr += 1
			count -= 1
		End While

		If ignore_last And (count = 1) And (n1.scale = n2.scale) Then Return 0

		If count <> 0 Then
			If n1(n1ptr) > n2(n2ptr) Then : Return CSByte(If((Not use_sign) Or n1.sign = PLUS, 1, -1))
			Else : Return CSByte(If((Not use_sign) Or n1.sign = PLUS, -1, 1))
			End If
		End If

		' They are equal up to the last part of the equal part of the fraction.
		If n1.scale > n2.scale Then
			For count = (n1.scale - n2.scale) To 1 Step -1
				If (n1(n1ptr) <> 0) Then
					Return CSByte(If((Not use_sign) Or n1.sign = PLUS, 1, -1))
				End If
				n1ptr += 1
			Next
		ElseIf n1.scale < n2.scale Then
			For count = (n2.scale - n1.scale) To 1 Step -1
				If (n2(n2ptr) <> 0) Then
					Return CSByte(If((Not use_sign) Or n1.sign = PLUS, -1, 1))
				End If
				n2ptr += 1
			Next
		End If

		' They must be equal!
		Return 0
	End Function
End Class
