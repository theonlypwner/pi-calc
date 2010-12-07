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

	End Sub
End Class
