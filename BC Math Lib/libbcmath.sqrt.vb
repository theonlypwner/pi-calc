Partial Public Class libbcmath
	''' <summary>Get the square root of an arbitrary precision number directly</summary>
	''' <param name="num">The (only) number</param>
	''' <param name="scale_min">The minimum scale for the result</param>
	Public Shared Function SquareRoot(ByRef num As BCNum, Optional ByVal scale_min As Integer = 0) As Boolean
		Dim rscale, cmp_res, cscale As Integer
		Dim done As Boolean
		Dim guess, guess1, point5, diff As BCNum
		' Initial check
		cmp_res = DoCompare(num, 0)
		If cmp_res < 0 Then : Return False
		ElseIf cmp_res = 0 Then
			num = 0
			Return True
		End If
		cmp_res = DoCompare(num, 1)
		If cmp_res = 0 Then
			num = 1
			Return True
		End If

		' Initialize the variables.
		rscale = Math.Max(scale_min, num.scale)
		guess = InitNum()
		guess1 = InitNum()
		diff = InitNum()
		point5 = NewNum(1, 1)
		point5(1) = 5

		' Calculate the initial guess.
		If cmp_res < 0 Then
			' The number is between 0 and 1.  Guess should start at 1.
			guess = 1
			cscale = num.scale
		Else
			' The number is greater than 1.  Guess should start at 10^(exp/2).
			guess = 10
			guess1 = num.length
			guess1 = Multiply(guess1, point5, 0)
			guess1.scale = 0
			' HACK: Link the raise
			' bc_raise (guess, guess1, &guess, 0 TSRMLS_CC);
			cscale = 3
		End If

		' Find the square root using Newton's algorithm.
		done = False
		Do Until done
			guess1 = guess
			guess = Divide(num, guess, cscale)
			guess = Add(guess, guess1, 0)
			guess = Multiply(guess, point5, cscale)
			diff = Subtract(guess, guess1, cscale + 1)
			If IsNearZero(diff, cscale) Then
				If (cscale < rscale + 1) Then : cscale = Math.Min(cscale * 3, rscale + 1)
				Else : done = True
				End If
			End If
		Loop

		' Assign the number
		num = Divide(guess, 1, rscale)
		Return True
	End Function
End Class
