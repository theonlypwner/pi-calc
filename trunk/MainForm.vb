﻿Public Class MainForm

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' CPU Information
        Dim q As New ObjectQuery("SELECT * FROM Win32_Processor")
        Dim p As New ManagementObjectSearcher(q)
        Dim i, i2 As Long ' Long Is Int64
        Dim s As String = Nothing
        For Each cpu As ManagementObject In p.Get() ' there could be more than one processor, like a server?
            i += CInt(cpu("NumberOfCores")) ' counts cores ' reminds me when I used i += 1, "why doesn't VB have i++ like c++?"
            i2 += CInt(cpu("NumberOfLogicalProcessors")) ' counts threads
            If s Is Nothing Then ' CPU name
                s = cpu("Name").ToString.Replace("   ", "") & ", " & (CInt(cpu("CurrentVoltage")) / 10) & "v"
            End If
        Next
        lblCPU.Text = s & ", " & i & " cores, " & i2 & " threads"
        ' OS Information
        q = New ObjectQuery("SELECT * FROM Win32_OperatingSystem")
        p = New ManagementObjectSearcher(q)
        For Each os As ManagementObject In p.Get()
            lblOS.Text = CStr(os("Caption")) & " " & CStr(os("Version")) & " " & CStr(os("OSArchitecture"))
            Exit For ' stupid collection doesn't let me do p.Get()[0]
        Next
        ' Memory Information
        q = New ObjectQuery("SELECT * FROM Win32_PhysicalMemory")
        p = New ManagementObjectSearcher(q)
        i = 0
        s = Nothing
        For Each mm As ManagementObject In p.Get()
            i += CLng(mm("Capacity")) ' add the memory module's capacity
            If s Is Nothing Then ' describe the memory
                Select Case CShort(mm("MemoryType")) ' Covers DDR and DDR2, others and assumes DDR3
                    Case 1 To 19 ' not DDR memory
                        s = "Unknown"
                    Case 20
                        s = "DDR"
                    Case 21
                        s = "DDR2"
                    Case Else ' Assume unknown memory (Case 0) to be DDR3
                        s = "DDR3"
                End Select
                s &= "-" & CStr(mm("Speed"))
            End If
        Next
        Dim sf As Char() = {"K"c, "M"c, "G"c}
        i2 = 0
        Do Until (i / (1024 ^ i2) < 1024) Or i2 > sf.Length
            i2 += 1
        Loop
        lblMemory.Text = s & " " & (i / (1024 ^ i2)) & If(i2 > 0, sf(CInt(i2 - 1)), "") & "B"
        ' ComboBox Initalization - Visual Studio doesn't have these important properties available for ComboBox controls
        cmbPrecision.SelectedIndex = 6
        cmbDScale.SelectedIndex = 0
    End Sub

    Private Sub copyTextBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles copyTextBtn.Click
        ' Copy the static label text
        Clipboard.SetText(CType(CType(CType(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl, Control).Text)
    End Sub

    Private Sub numPrecision_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numPrecision.ValueChanged
        If cmbDScale.SelectedIndex = 0 Then
            ' 1024s
            If numPrecision.Value Mod 1048576 = 0 Then
                ' m
                ' match numPrecision.Value / 1048576
                'For Each i As String In cmbPrecision.Items
                For i As Byte = 0 To CByte(cmbPrecision.Items.Count - 1)
                    If cmbPrecision.Items(i).IndexOf("M") >= 0 Then
                        ' this
                    End If
                Next
            End If
            If numPrecision.Value Mod 1024 = 0 Then
                ' k
            End If
        Else
            ' 1000s
            If numPrecision.Value Mod 1000000 = 0 Then
                ' m
            ElseIf numPrecision.Value Mod 1000 = 0 Then
                ' k
            End If
        End If
    End Sub

    Private Sub cmbPrecision_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPrecision.SelectedIndexChanged

    End Sub
End Class
