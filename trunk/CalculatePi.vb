' Imports Nothing

''' <summary>Responsible for calculating pi</summary>
Public Class CalculatePi
	''' <summary>CR &amp; LF = Windows New Line</summary>
	Public Const CrLf As String = Chr(13) & Chr(10) ' CR & LF = Windows New Line

	''' <summary>How many digits of pi to compute</summary>
	Public precision As Integer
	''' <summary>How many digits of pi to compute before updating the progress bar</summary>
	Public progressUpdateInterval As UInteger

	''' <summary>Result storage variable</summary>
	Protected Friend result As BigNumber

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
		precision = p + 3
		progressUpdateInterval = CUInt(Math.Floor(precision / 200))
	End Sub

	''' <summary>The Thread instance calls this function</summary>
	Protected Friend Sub Process()
		Dim limit As Integer = CInt(Math.Round(precision / 14 - 1))
		' lol old method?
		'result = New BigNumber(precision)
		'result.ArcTan(4, 1)

		' new better method
		result = New BigNumber(1000)
		Dim y = New BigNumber(1000)
		result.ArcTan(16, 5)
		y.ArcTan(4, 239)
		result.Subtract(y)

		' done!
		RaiseEvent onComplete(Me, New EventArgs)
	End Sub

	Public Enum ResultType
		BufferOnly
		First2K
		All
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
		If n = ResultType.First2K And precision - 3 > MainForm.KprecisionP * 2 Then
			ret.s = "Result is trimmed" & CrLf & "3."
		End If
		Dim max As Integer = CInt( _
		 If(n = ResultType.First2K And precision > MainForm.KprecisionP * 2, Math.Min(result.Length, MainForm.KprecisionP * 2), result.Length) - 3)
		For i As Integer = 1 To max
			ret.s &= CStr(result(CInt(i)))
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

	''' <summary>Calculate a Factorial</summary>
	Protected Function ArrayFactorial(ByVal baseNumber As ULong) As ULong
		Return If(baseNumber = 0 Or baseNumber = 1, CULng(1), baseNumber * ArrayFactorial(CULng(baseNumber - 1)))
	End Function
End Class
