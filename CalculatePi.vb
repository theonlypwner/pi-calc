''' <summary>Responsible for calculating pi</summary>
Public Class CalculatePi
	''' <summary>CR &amp; LF = Windows New Line</summary>
	Public Const CrLf As String = Chr(13) & Chr(10) ' CR & LF = Windows New Line

	''' <summary>How many digits of pi to compute</summary>
	Public precision As UShort
	''' <summary>If the result buffer should be filled</summary>
	Protected store As Boolean
	''' <summary>temp unsigned 16-bit integer</summary>
	Private i As UInt16
	''' <summary>Contains the results</summary>
	Protected result As ArrayList

	''' <summary>Raised upon thread completion</summary>
	Public Event onComplete(ByVal sender As System.Object, ByVal e As System.EventArgs)
	''' <summary>Raised upon progress update</summary>
	Public Event onProgress(ByVal p As UShort)

	''' <param name="p">Precision to calculate pi</param>
	''' <param name="s">Store in memory or not</param>
	Public Sub New(ByVal p As UShort, ByVal s As Boolean)
		precision = p
		store = s
		result = New ArrayList()
	End Sub

	''' <summary>The Thread instance calls this function</summary>
	Protected Friend Sub Process()
		Do
			i = CUShort(i + 1)
			If i > precision Then
				RaiseEvent onComplete(Me, Nothing)
				Exit Sub
			End If
			If store Then result.Add(i Mod 10)
			RaiseEvent onProgress(i)
			' Thread.CurrentThread.Sleep(ms)
		Loop
	End Sub

	Public Enum ResultType
		None = 0
		First2000 = 1
		All = 2
	End Enum

	''' <summary>Holds a result string with start timestamp and difference to finish</summary>
	Public Structure TimedResult
		Public s As String
		Public timeStart As Long
		Public timeDiff As TimeSpan
	End Structure

	Protected Friend Function ResultDataNoDelete(Optional ByVal n As ResultType = ResultType.None) As TimedResult
		Dim ret As TimedResult
		ret.timeStart = Now.Ticks
		If (Not store) Or result.Count < 1 Then
			ret.s = "No buffer"
			Return ret
		ElseIf n = ResultType.None Then
			ret.s = "Buffer not displayed"
			Return ret
		End If
		ret.s = "3."
		If n = ResultType.First2000 And precision > 2000 Then
			ret.s = "Result is trimmed" & CrLf & "3."
		End If
		Dim j As Object() = result.ToArray
		For i As UShort = 0 To If(n = ResultType.First2000 And precision > 2000, Math.Min(j.Length, 2000), j.Length) - 1
			ret.s &= CStr(j(i))
		Next
		Return ret
	End Function

	Protected Friend ReadOnly Property ResultData(Optional ByVal n As ResultType = ResultType.All) As TimedResult
		Get
			Dim r = Me.ResultDataNoDelete(n)
			result = Nothing
			Return r
		End Get
	End Property
End Class
