Partial Public Class libbcmath
	''' <summary>Raise an arbitrary precision number to a power of another arbitrary power</summary>
	''' <param name="base">The base number</param>
	''' <param name="power">The power to raise the base to</param>
	''' <param name="scale_min">The minimum scale to use</param>
	Public Shared Function Raise(ByVal base As BCNum, ByVal power As BCNum, Optional ByVal scale_min As Integer = defaultScale) As BCNum
		Dim result As BCNum = InitNum()
		DoRaise(base, power, result, scale_min)
		Return result
	End Function

	''' <summary>Calculate a power</summary>
	''' <param name="num1">The base</param>
	''' <param name="num2">The power to raise it to</param>
	''' <param name="result">The result, passed by reference</param>
	''' <param name="scale">The minimum scale to use</param>
	Private Shared Sub DoRaise(ByVal num1 As BCNum, ByVal num2 As BCNum, ByRef result As BCNum, ByVal scale As Integer)
		Dim temp, power As BCNum
		Dim exponent As Long
		Dim rscale, pwrscale, calcscale As Integer
		Dim neg As Boolean

		' Check the exponent for scale digits and convert to a long.
		' If num2.scale <> 0 then Throw New Exception("non-zero scale in exponent") ' warning, not error
		exponent = num2long(num2)

		If exponent = 0 And (num2.length > 1 Or num2.value(0) <> 0) Then Throw New Exception("Exponent too large in raise")

		' Special case if exponent is a zero.
		If exponent = 0 Then
			result = 1
			Return
		End If

		' Other initializations.
		If exponent < 0 Then
			neg = True
			exponent = -exponent
			rscale = scale
		Else : neg = False
			rscale = Math.Min(CInt(num1.scale * exponent), Math.Max(scale, num1.scale))
		End If

		' Set initial value of temp.
		power = num1
		pwrscale = num1.scale
		Do While (exponent And 1) = 0
			pwrscale *= 2
			power = Multiply(power, power, pwrscale)
			exponent = exponent >> 1
		Loop
		temp = power
		calcscale = pwrscale
		exponent = exponent >> 1

		' Do the calculation.
		While exponent > 0
			pwrscale *= 2
			power = Multiply(power, power, pwrscale)
			If (exponent And 1) = 1 Then
				calcscale = pwrscale + calcscale
				temp = Multiply(temp, power, calcscale)
			End If
			exponent = exponent >> 1
		End While

		' Assign the value.
		If (neg) Then
			result = Divide(1, temp, rscale)
		Else
			result = temp.GetShallowCopy ' *result = temp;
			If (result.scale > rscale) Then result.scale = rscale
		End If
	End Sub

	Public Const LONG_MAX As Integer = &H7FFFFFF

	Public Shared Function num2long(ByVal num As BCNum) As Long
		Dim val As Long = 0
		Dim nptr, index As Integer ' char *, int
		' Extract the int value, ignore the fraction.
		nptr = 0
		index = num.length ' revising the for loop to work with VB
		Do While (index > 0) And (val <= (LONG_MAX / BASE))
			index -= 1
			val = val * BASE + num(nptr)
			nptr += 1
		Loop

		' Check for overflow.  If overflow, return zero.
		If index > 0 Then val = 0
		If val < 0 Then val = 0

		' Return the value.
		Return If(num.sign = PLUS, val, -val)
	End Function
End Class
