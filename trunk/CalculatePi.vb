''' <summary>
''' Responsible for calculating pi
''' </summary>
Public Class CalculatePi
    ''' <summary>How many digits of pi to compute</summary>
    Public precision As UInteger
    ''' <summary>Form instance that requests pi calcuation</summary>
    Protected owner As MainForm
    ''' <summary>If data storage is needed</summary>
    Protected store As Boolean
    ''' <summary>temp unsigned 16-bit integer</summary>
    Private i As UInt16
    ''' <summary>Raised upon thread completion</summary>
    Event onComplete(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''' <summary>Contains the results</summary>
    Protected result As ArrayList

    ''' <param name="o">Instance of MainForm</param>
    ''' <param name="p">Precision to calculate pi</param>
    Public Sub New(ByVal o As MainForm, ByVal p As Integer, ByVal s As Boolean)
        precision = p
        owner = o
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
            Thread.CurrentThread.Sleep(10)
        Loop
    End Sub
End Class
