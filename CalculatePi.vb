''' <summary>
''' Responsible for calculating pi
''' </summary>
Public Class CalculatePi
    ''' <summary>How many digits of pi to compute</summary>
    Public precision As Integer
    ''' <summary>Form instance that requests pi calcuation</summary>
    Protected owner As MainForm
    Private i As UInt16

    ''' <param name="o">Instance of MainForm</param>
    ''' <param name="p">Precision to calculate pi</param>
    Public Sub New(ByVal o As MainForm, ByVal p As Integer)
        precision = p
        owner = o
    End Sub

    ''' <summary>The Thread instance calls this function</summary>
    Public Sub Process()
        Do
            i += 1
            If i > 200 Then onComplete() ' testing
        Loop
    End Sub

    Public Sub onComplete() ' no need to use an event on one method
        ' call owner's clean-up functions
        owner.stopThread(Me, Nothing)
    End Sub
End Class
