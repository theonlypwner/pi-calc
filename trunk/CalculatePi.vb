Imports Pi.BCMath

''' <summary>Responsible for calculating pi</summary>
Public Class CalculatePi
	''' <summary>CR &amp; LF = Windows New Line</summary>
	Public Const CrLf As String = Chr(13) & Chr(10) ' CR & LF = Windows New Line

	''' <summary>How many digits of pi to compute</summary>
	Public precision As Integer
	''' <summary>How many digits of pi to compute before updating the progress bar</summary>
	Public progressUpdateInterval As UInteger

	''' <summary>Result storage variable</summary>
	Protected Friend result As libbcmath.BCNum

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
		' Declare the reused numbers now to save time
		Dim p1 As libbcmath.BCNum = 1
		Dim p3 As libbcmath.BCNum = 3
		Dim n1 As libbcmath.BCNum = -1
		Dim p640320 As libbcmath.BCNum = 640320
		Dim p13591409 As libbcmath.BCNum = 13591409
		Dim p545140134 As libbcmath.BCNum = 545140134
		Dim s640320 As libbcmath.BCNum = bcsqrt(640320)

		For k As Integer = 0 To limit
			result = bcdiv( _
				bcmul( _
					bcadd(p13591409, _
						bcmul(p545140134, k) _
					), _
					bcmul(if(k Mod 2 = 1, n1, p1), bcfact(6 * k)) _
				), _
				bcmul( _
					bcmul( _
						bcpow(p640320,3 * k + 1), _
						s640320 _
					), _
					bcmul( _
						bcfact(3 * k), _
						bcpow(bcfact(k), p3) _
					) _
				) _
			)
		Next
		result = bcdiv(1, (bcmul(12, (result))), precision)
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
		 If(n = ResultType.First2K And precision > MainForm.KprecisionP * 2, Math.Min(result.length, MainForm.KprecisionP * 2), result.length) - 3)
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
	Protected Function ArrayFactorial(ByRef baseNumber As ULong) As ULong
		Return If(baseNumber = 0 Or baseNumber = 1, CULng(1), baseNumber * ArrayFactorial(CULng(baseNumber - 1)))
	End Function
End Class
