Imports Pi.libbcmath
''' <summary>Provides a direct interface to libbcmath</summary>
Public NotInheritable Class BCMath
	''' <summary>Delegate sub for lazily directing subroutines into libbcmath</summary>
	''' <param name="left">The left/first operand</param>
	''' <param name="right">The right/second operand</param>
	''' <param name="scale_min"></param>
	Public Delegate Function BCStdOp(ByRef left As BCNum, ByRef right As BCNum, ByVal scale_min As Integer) As BCNum

	''' <summary>Function to handle most lazy subroutines</summary>
	''' <param name="left">The left/first operand</param>
	''' <param name="right">The right/second operand</param>
	''' <param name="scale">The scale to use</param>
	''' <param name="callback">The callback to use to process the numbers</param>
	Protected Shared Function bcstd(ByRef left As BCNum, ByRef right As BCNum, ByVal scale As Integer, ByVal callback As BCStdOp) As BCNum
		Dim scale_min As Integer = If(scale < 0, 0, scale)
		Dim result As BCNum = callback(left, right, scale_min)
		If result.scale > scale Then result.scale = scale
		Return result
	End Function

	''' <summary>Add two arbitrary precision numbers</summary>
	''' <param name="base">The left operand</param>
	''' <param name="addend">The right operand</param>
	''' <param name="scale">This sets the scale after the decimal place</param>
	Public Shared Function bcadd(ByRef base As BCNum, ByRef addend As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Return bcstd(base, addend, scale, AddressOf Add)
	End Function

	''' <summary>Subtract one arbitrary precision number from another</summary>
	''' <param name="minuend">The left operand</param>
	''' <param name="subtrahend">The right operand</param>
	''' <param name="scale">This sets the scale after the decimal place</param>
	Public Shared Function bcsub(ByRef minuend As BCNum, ByRef subtrahend As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Return bcstd(minuend, subtrahend, scale, AddressOf Subtract)
	End Function

	''' <summary>
	''' Compare two arbitrary precision numbers
	''' 0: Left/Right are equal, 1 if left > right, -1 otherwise
	''' </summary>
	''' <param name="left_operand">The first operand</param>
	''' <param name="right_operand">The second operand</param>
	Function bccmp(ByRef left_operand As BCNum, ByRef right_operand As BCNum) As SByte
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
	Public Shared Function bcdiv(ByRef dividend As BCNum, ByRef divisor As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Return bcstd(dividend, divisor, scale, AddressOf Divide)
	End Function

	''' <summary>Multiply two arbitrary precision numbers</summary>
	''' <param name="factor">The left operand</param>
	''' <param name="multiplier">The right operand</param>
	''' <param name="scale">This sets the scale after the decimal place</param>
	Public Shared Function bcmul(ByRef factor As BCNum, ByRef multiplier As BCNum, Optional ByVal scale As Integer = defaultScale) As BCNum
		Return bcstd(factor, multiplier, scale, AddressOf Multiply)
	End Function

	''' <summary>Convert to an arbitrary precision detecting the proper scale</summary>
	''' <param name="str">The string to convert</param>
	Public Shared Function StrToNum(ByVal str As String) As BCNum
		Dim result As BCNum = InitNum()
		result = str2num(str)
		Return result
	End Function
End Class
