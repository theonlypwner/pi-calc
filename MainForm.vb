Public Class MainForm

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' CPU Information
        Dim q As New ObjectQuery("SELECT * FROM Win32_Processor")
        Dim p As New ManagementObjectSearcher(q)
        Dim i, i2 As Integer
        Dim s As String = Nothing
        For Each cpu As ManagementObject In p.Get() ' there could be more than one processor, like a server?
            i += cpu("NumberOfCores") ' counts cores ' reminds me when I used i += 1, "why doesn't VB have i++ like c++?"
            i2 += cpu("NumberOfLogicalProcessors") ' counts threads
            If s Is Nothing Then ' CPU name
                s = cpu("Name").Replace("   ", "") & ", " & (cpu("CurrentVoltage") / 10) & "v"
            End If
        Next
        lblCPU.Text = s & ", " & i & " cores, " & i2 & " threads"
    End Sub
End Class
