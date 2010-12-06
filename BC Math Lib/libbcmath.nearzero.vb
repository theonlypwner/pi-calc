Partial Public Class libbcmath
	''' <summary>
	''' In some places we need to check if the number NUM is almost zero.
	''' Specifically, all but the last digit is 0 and the last digit is 1.
	''' Last digit is defined by scale.
	''' </summary>
	''' <param name="num">The number to check</param>
	''' <param name="scale">The last digit</param>
	Protected Shared Function IsNearZero(ByVal num As BCNum, ByVal scale As Integer) As Boolean
		Dim count, nptr As Integer

		' Error checking
		If scale > num.scale Then scale = num.scale

		' Initialize
		count = num.length + scale
		nptr = 0

		' The check
		While count > 0 And num(nptr) = 0
			nptr += 1
			count -= 1
		End While

		nptr -= 1
		Return count = 0 Or (count = 1 And num(nptr) = 1)
	End Function
End Class
