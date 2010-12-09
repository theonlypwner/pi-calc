Imports Pi.libbcmath
''' <summary>Provides a direct interface to libbcmath</summary>
Public NotInheritable Class BCMath
	''' <summary>Delegate sub for lazily directing subroutines into libbcmath</summary>
	''' <param name="left">The left/first operand</param>
	''' <param name="right">The right/second operand</param>
	''' <param name="scale_min"></param>
	Public Delegate Function BCStdDelegate(ByVal left As BCNum, ByVal right As BCNum, ByVal scale_min As Integer) As BCNum

	''' <summary>Function to handle most lazy subroutines</summary>
	''' <param name="left">The left/first operand</param>
	''' <param name="right">The right/second operand</param>
	''' <param name="scale">The scale to use</param>
	''' <param name="callback">The callback to use to process the numbers</param>
	Protected Shared Function BCStd(ByVal left As BCNum, ByVal right As BCNum, ByVal scale As Integer, ByVal callback As BCStdDelegate) As BCNum
		Dim scale_min As Integer = If(scale < 0, 0, scale)
		Dim result As BCNum = callback(left, right, scale_min)
		If result.scale > scale Then result.scale = scale
		Return result
	End Function

	''' <summary>Add two arbitrary precision numbers</summary>
	''' <param name="base">The left operand</param>
	''' <param name="addend">The right operand</param>
	''' <param name="scale">This sets the scale after the decimal place</param>
	Public Shared Function bcadd(ByVal base As BCNum, ByVal addend As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Return BCStd(base, addend, scale, AddressOf Add)
	End Function

	''' <summary>Subtract one arbitrary precision number from another</summary>
	''' <param name="minuend">The left operand</param>
	''' <param name="subtrahend">The right operand</param>
	''' <param name="scale">This sets the scale after the decimal place</param>
	Public Shared Function bcsub(ByVal minuend As BCNum, ByVal subtrahend As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Return BCStd(minuend, subtrahend, scale, AddressOf Subtract)
	End Function

	''' <summary>
	''' Compare two arbitrary precision numbers
	''' 0: Left/Right are equal, 1 if left > right, -1 otherwise
	''' </summary>
	''' <param name="left_operand">The first operand</param>
	''' <param name="right_operand">The second operand</param>
	Function bccmp(ByVal left_operand As BCNum, ByVal right_operand As BCNum) As SByte
		Return DoCompare(left_operand, right_operand)
	End Function

	' ''' <summary>
	' ''' Set default scale parameter for all bcmath functions.
	' ''' Returns false on failure and true on success
	' ''' </summary>
	' ''' <param name="scale">The scale factor (0 for unlimited)</param>
	'Public Shared Function bcscale(ByVal scale As Integer)
	'	If scale < 0 Then Return False
	'	defaultScale = scale ' cannot assign to a constant
	'	Return True
	'End Function

	''' <summary>Divide two arbitrary precision numbers</summary>
	''' <param name="dividend">The left operand</param>
	''' <param name="divisor">The right operand</param>
	''' <param name="scale">This sets the scale after the decimal place</param>
	Public Shared Function bcdiv(ByVal dividend As BCNum, ByVal divisor As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Return BCStd(dividend, divisor, scale, AddressOf Divide)
	End Function

	''' <summary>Multiply two arbitrary precision numbers</summary>
	''' <param name="factor">The left operand</param>
	''' <param name="multiplier">The right operand</param>
	''' <param name="scale">This sets the scale after the decimal place</param>
	Public Shared Function bcmul(ByVal factor As BCNum, ByVal multiplier As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Return BCStd(factor, multiplier, scale, AddressOf Multiply)
	End Function

	''' <summary>Raise an arbitrary precision number to a power of another arbitrary power</summary>
	''' <param name="base">The base number</param>
	''' <param name="power">The power to raise the base to</param>
	''' <param name="scale">The minimum scale to use</param>
	Public Shared Function bcpow(ByVal base As BCNum, ByVal power As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Return BCStd(base, power, scale, AddressOf Raise)
	End Function
	''' <summary>Get the square root of an arbitrary precision</summary>
	''' <param name="num">The number</param>
	''' <param name="scale">This sets the scale after the decimal place</param>
	Public Shared Function bcsqrt(ByVal num As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Dim tmp As BCNum = num
		SquareRoot(num, scale)
		Return num
	End Function

	''' <summary>Convert a string to an arbitrary precision detecting the proper scale</summary>
	''' <param name="str">The string to convert</param>
	Public Shared Function StrToNum(ByVal str As String) As BCNum
		Dim result As BCNum = InitNum()
		result = str2num(str)
		Return result
	End Function

	''' <summary>Convert an integer to arbitrary precision</summary>
	''' <param name="int">The integer to convert</param>
	Public Shared Function IntToNum(ByVal int As Integer) As BCNum
		Dim result As BCNum = InitNum()
		result = str2num(int.ToString)
		Return result
	End Function

	''' <summary>Calculate a factorial</summary>
	''' <param name="int">The number to calculate a factorial of</param>
	Public Shared Function bcfact(ByVal int As BCNum) As BCNum
		int.scale = 0 ' integers only now
		Return If(IsZero(int) Or IsOne(int), int, bcmul(int, bcsub(int, IntToNum(1))))
	End Function
End Class
