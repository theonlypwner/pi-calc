Partial Public Class libbcmath
	''' <summary>Adds two arbitrary precision numbers</summary>
	''' <param name="base">The first number</param>
	''' <param name="addend">The number to add onto it</param>
	''' <param name="scale_min">The minimum scale for the result</param>
	Public Shared Function Add(ByRef base As BCNum, ByRef addend As BCNum, Optional ByVal scale_min As Integer = 0) As BCNum
		Dim sum As New BCNum, cmp_res As Integer = -2
		If base.sign = addend.sign Then	' Same sign; add
			sum.sign = base.sign
			sum = DoAdd(base, addend, scale_min)
		Else ' subtraction must be done
			cmp_res = DoCompareAdvanced(base, addend, False, False)
			Select Case cmp_res
				Case -1	' second number is bigger
					sum.sign = addend.sign
					sum = libbcmath.DoSub(addend, base, scale_min)
				Case 0 ' equal numbers
					sum = new_num(1, Math.Max(scale_min, Math.Max(base.scale, addend.scale)))
					memset(sum.value, 0, 0, sum.scale + 1)
				Case 1 ' first number is bigger
					sum.sign = base.sign
					sum = libbcmath.DoSub(base, addend, scale_min)
			End Select
		End If
		' Return our final answer
		Return sum
	End Function
End Class
