''' <summary>
''' Responsible for calculating pi
''' </summary>
Public Class CalculatePi
    ''' <summary>How many digits of pi to compute</summary>
    Public precision As UInteger
    ''' <summary>If data storage is needed</summary>
    Protected store As Boolean
    ''' <summary>temp unsigned 16-bit integer</summary>
    Private i As UInt16
    ''' <summary>Contains the results</summary>
    Protected result As ArrayList

    ''' <summary>Raised upon thread completion</summary>
    Public Event onComplete(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''' <summary>Raised upon progress update</summary>
    Public Event onProgress(ByVal p As UInteger)

    ''' <param name="p">Precision to calculate pi</param>
    ''' <param name="s">If calculation should be stored</param>
    Public Sub New(ByVal p As Integer, ByVal s As Boolean)
        precision = p
        store = s
    End Sub

    ''' <summary>The Thread instance calls this function</summary>
    Public Sub Process()
        Do
            i += 1
            If i > precision Then
                RaiseEvent onComplete(Me, Nothing)
                Exit Sub
            End If
            RaiseEvent onProgress(i)
            Thread.CurrentThread.Sleep(10)
        Loop
    End Sub
End Class
