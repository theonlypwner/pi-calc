Partial Public Class libbcmath
	''' <summary>Subtracts two arbitrary precision numbers</summary>
	''' <param name="minuend">The big (base) number</param>
	''' <param name="subtrahend">The number to subtract from it</param>
	Public Shared Function Subtract(ByVal minuend As BCNum, ByVal subtrahend As BCNum, Optional ByVal scale_min As Integer = defaultScale) As BCNum
		Dim diff As New BCNum
		Dim cmp_res As Integer = 0, res_scale As Integer = 0
		If minuend.sign <> subtrahend.sign Then
			diff = DoAdd(minuend, subtrahend, scale_min)
			diff.sign = minuend.sign
		Else ' subtraction must be done
			' Compare magnitudes
			cmp_res = DoCompareAdvanced(minuend, subtrahend, False, False)
			Select Case cmp_res
				Case -1	' The minuend is less than the subtrahend, subtract the minuend from the subtrahend.
					diff = DoSub(subtrahend, minuend, scale_min)
					diff.sign = InvertSign(minuend.sign)
				Case 0 ' They are equal! Return zero!
					res_scale = Math.Max(scale_min, Math.Max(minuend.scale, subtrahend.scale))
					diff = NewNum(1, res_scale)
					memset(diff.value, 0, 0, res_scale + 1)
				Case 1 ' The subtrahend is less than the minuend, subtract the subtrahend from the minuend.
					diff = DoSub(minuend, subtrahend, scale_min)
					diff.sign = minuend.sign
			End Select
		End If
		' Return the difference
		Return diff
	End Function
End Class
