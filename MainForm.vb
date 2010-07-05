Public Class MainForm

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' CPU Information
        Dim q As New ObjectQuery("SELECT * FROM Win32_Processor")
        Dim p As New ManagementObjectSearcher(q)
        Dim i, i2 As Long ' Long Is Int64
        Dim s As String = Nothing
        For Each cpu As ManagementObject In p.Get() ' there could be more than one processor, like a server?
            i += cpu("NumberOfCores") ' counts cores ' reminds me when I used i += 1, "why doesn't VB have i++ like c++?"
            i2 += cpu("NumberOfLogicalProcessors") ' counts threads
            If s Is Nothing Then ' CPU name
                s = cpu("Name").Replace("   ", "") & ", " & (cpu("CurrentVoltage") / 10) & "v"
            End If
        Next
        lblCPU.Text = s & ", " & i & " cores, " & i2 & " threads"
        ' OS Information
        q = New ObjectQuery("SELECT * FROM Win32_OperatingSystem")
        p = New ManagementObjectSearcher(q)
        For Each os As ManagementObject In p.Get()
            lblOS.Text = os("Caption") & " " & os("Version") & " " & os("OSArchitecture")
            Exit For ' stupid collection doesn't let me do p.Get()[0]
        Next
        ' Memory Information
        q = New ObjectQuery("SELECT * FROM Win32_PhysicalMemory")
        p = New ManagementObjectSearcher(q)
        i = 0
        s = Nothing
        For Each mm As ManagementObject In p.Get()
            i += mm("Capacity") ' add the memory module's capacity
            If s Is Nothing Then ' describe the memory
                Select Case mm("MemoryType") ' Covers DDR and DDR2, others and assumes DDR3
                    Case 1 To 19 ' not DDR memory
                        s = "Unknown"
                    Case 20
                        s = "DDR"
                    Case 21
                        s = "DDR2"
                    Case Else ' Assume unknown memory (Case 0) to be DDR3
                        s = "DDR3"
                End Select
                s &= "-" & mm("Speed")
            End If
        Next
        Dim sf As Char() = {"K"c, "M"c, "G"c}
        i2 = 0
        For Each c As Char In sf
            If i / (1024 ^ i2) < 1024 Then Exit For ' don't reduce it again
            i2 += 1
        Next
        lblMemory.Text = s & " " & (i / (1024 ^ i2)) & If(i2 > 0, sf(i2 - 1), "") & "B"
    End Sub

    Private Sub copyTextBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles copyTextBtn.Click
        Clipboard.SetText(CType(CType(CType(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl, Control).Text)
    End Sub
End Class
