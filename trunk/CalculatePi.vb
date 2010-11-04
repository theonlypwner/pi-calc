''' <summary>Responsible for calculating pi</summary>
Public Class CalculatePi
	''' <summary>CR &amp; LF = Windows New Line</summary>
	Public Const CrLf As String = Chr(13) & Chr(10) ' CR & LF = Windows New Line

	''' <summary>How many digits of pi to compute</summary>
	Public precision As Integer
	''' <summary>How many digits of pi to compute before updating the progress bar</summary>
	Public progressUpdateInterval As UInteger

	''' <summary>First storage variable</summary>
	Protected Friend result() As SByte
	''' <summary>Second storage variable</summary>
	Protected sourceValue() As SByte

	''' <summary>Ticks when calculation started</summary>
	Protected Friend startTicks As Long
	''' <summary>Ticks used to process</summary>
	Protected Friend diffTicks As TimeSpan

	''' <summary>Raised upon thread completion</summary>
	Public Event onComplete(ByVal sender As System.Object, ByVal e As System.EventArgs)
	''' <summary>Raised upon progress update</summary>
	Public Event onProgress(ByVal p As Byte)

	''' <param name="p">Precision to calculate pi</param>
	Public Sub New(ByVal p As Integer)
		precision = p + 1
		progressUpdateInterval = CUInt(Math.Floor(precision / 200))
		ReDim result(precision), sourceValue(precision)
	End Sub

	''' <summary>The Thread instance calls this function</summary>
	Protected Friend Sub Process()
		ArcTangent(result, sourceValue, 2)
		ArcTangent(result, sourceValue, 3)
		ArrayMult(result, 4)
		sourceValue = Nothing
		RaiseEvent onComplete(Me, New EventArgs)
	End Sub

	Public Enum ResultType
		BufferOnly = 0
		First2000 = 1
		All = 2
	End Enum

	''' <summary>Holds a result string with start timestamp and difference to finish</summary>
	Public Structure TimedResult
		Public s As String
		Public timeStart As Long
		Public timeDiff As TimeSpan
	End Structure

	Protected Friend Function ResultDataNoDelete(Optional ByVal n As ResultType = ResultType.BufferOnly) As TimedResult
		Dim ret As TimedResult
		ret.timeStart = Now.Ticks
		If result.Length < 1 Then
			ret.s = "No buffer"
			Return ret
		ElseIf n = ResultType.BufferOnly Then
			ret.s = "Buffer not displayed"
			Return ret
		End If
		ret.s = "3."
		If n = ResultType.First2000 And precision > 2000 Then
			ret.s = "Result is trimmed" & CrLf & "3."
		End If

		For i As UInteger = 1 To CUInt(If(n = ResultType.First2000 And precision > 2000, Math.Min(result.Length, 2000), result.Length) - 3)
			ret.s &= CStr(result.GetValue(CInt(i)))
		Next
		Return ret
	End Function

	Protected Friend ReadOnly Property ResultData(Optional ByVal n As ResultType = ResultType.All) As TimedResult
		Get
			Dim r As TimedResult = Me.ResultDataNoDelete(n)
			result = Nothing
			Return r
		End Get
	End Property

#Region "Array Pi Adaptation"
	' Adapted from java2s.com/Tutorial/VB/0060__Operator/CalculatePI.htm
	' Which quotes:
	' Visual Basic 2005 Cookbook Solutions for VB 2005 Programmers by Tim Patrick (Author), John Craig (Author)
	' Publisher: O'Reilly Media, Inc. (September 21, 2006)
	' Language: English
	' ISBN-10: 0596101775
	' ISBN-13: 978-0596101770

	''' <summary>Multiply an array number by another number by hand. The product remains in the array number.</summary>
	Protected Sub ArrayMult(ByRef baseNumber() As SByte, ByRef multiplier As Integer)
		Dim carry As Integer
		Dim position As Integer
		Dim holdDigit As Integer
		' Multiple each base digit, from right to left.
		For position = precision To 0 Step -1
			' If the multiplication went past 9, carry the tens value to the next column.
			holdDigit = (baseNumber(position) * multiplier) + carry
			carry = holdDigit \ 10
			baseNumber(position) = CSByte(holdDigit Mod 10)
		Next position
	End Sub

	''' <summary>Divide an array number by another number by hand. The quotient remains in the array number.</summary>
	Protected Sub ArrayDivide(ByRef dividend() As SByte, ByRef divisor As Integer)
		Dim borrow As Integer
		Dim position As Integer
		Dim holdDigit As Integer

		' Process division for each digit.
		For position = 0 To precision
			' If the division can't happen directly, borrow from the previous position.
			holdDigit = dividend(position) + borrow * 10
			dividend(position) = CSByte(holdDigit \ divisor)
			borrow = holdDigit Mod divisor
		Next position
	End Sub

	''' <summary>Add two array numbers together. The sum remains in the first array number.</summary>
	Protected Sub ArrayAdd(ByRef baseNumber() As SByte, ByRef addend() As SByte)
		Dim carry As Integer
		Dim position As Integer
		Dim holdDigit As Integer

		' Add each digit from right to left.
		For position = precision To 0 Step -1
			' If the sum goes beyond 9, carry the tens value to the next column.
			holdDigit = baseNumber(position) + addend(position) + carry
			carry = holdDigit \ 10
			baseNumber(position) = CSByte(holdDigit Mod 10)
		Next position
	End Sub

	''' <summary>Subtract one array number from another. The difference remains in the first array number.</summary>
	Protected Sub ArraySub(ByRef minuend() As SByte, ByRef subtrahend() As SByte)
		Dim borrow As Integer
		Dim position As Integer
		Dim holdDigit As Integer

		' Subtract the digits from right to left.
		For position = precision To 0 Step -1
			' If the subtraction would give a negative value for a column, we will have to borrow.
			holdDigit = minuend(position) - subtrahend(position) + 10
			borrow = holdDigit \ 10
			minuend(position) = CSByte(holdDigit Mod 10)
			If (borrow = 0) Then minuend(position - 1) -= CSByte(1)
		Next position
	End Sub

	''' <summary>Report whether an array number is all zero.</summary>
	Protected Function ArrayZero(ByRef baseNumber() As SByte) As Boolean
		Dim position As Integer

		' Examine each digit.
		For position = 0 To precision
			If (baseNumber(position) <> 0) Then
				' The number is nonzero
				Return False
			End If
		Next position

		' The number is zero.
		Return True
	End Function

	''' <summary>Calculate an arctangent of a fraction, 1/divFactor.
	'''       This routine performs a modified Maclaurin series to 
	'''       calculate the arctangent. The base formula is:
	'''          arctan(x) = x - x^3/3 + x^5/5 - x^7/7 + x^9/9 - ...
	'''       where -1 [less than] x [lt] 1 (it's 1/divFactor in this case).
	''' </summary>
	Protected Sub ArcTangent(ByRef targetValue() As SByte, ByRef sourceValue() As SByte, ByVal divFactor As Integer)
		' Calculate an arctangent of a fraction, 1/divFactor. This routine performs a modified Maclaurin series to calculate the arctangent.
		' The base formula is:
		'	arctan(x) = x - x^3/3 + x^5/5 - x^7/7 + x^9/9 - ...
		'		where -1 < x < 1 (it's 1/divFactor in this case).
		Dim workingFactor As Integer
		Dim incremental As Integer

		' Figure out the "x" part, 1/divFactor.
		sourceValue(0) = 1
		incremental = 1
		workingFactor = divFactor
		ArrayDivide(sourceValue, workingFactor)

		' Add "x" to the total.
		ArrayAdd(targetValue, sourceValue)
		Do
			' Perform the "- (xy)/y" part.
			ArrayMult(sourceValue, incremental)
			workingFactor = divFactor * divFactor
			ArrayDivide(sourceValue, workingFactor)
			incremental += 2
			workingFactor = incremental
			ArrayDivide(sourceValue, workingFactor)
			ArraySub(targetValue, sourceValue)

			' Perform the "+ (xy)/y" part.
			ArrayMult(sourceValue, incremental)
			workingFactor = divFactor * divFactor
			ArrayDivide(sourceValue, workingFactor)
			incremental += 2
			workingFactor = incremental
			ArrayDivide(sourceValue, workingFactor)
			ArrayAdd(targetValue, sourceValue)
		Loop Until ArrayZero(sourceValue)
		If divFactor < 3 Then
			RaiseEvent onProgress(33)
		Else
			RaiseEvent onProgress(66)
		End If
	End Sub
#End Region

End Class
