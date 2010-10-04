Public Class MainForm

	Protected Friend t As Thread
	Protected WithEvents piCalc As CalculatePi
	''' <summary>CR &amp; LF = Windows New Line</summary>
	Public Const CrLf As String = Chr(13) & Chr(10)

	Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		' Title information
		Me.Text = "Pi Calculator (Single Thread)"
		' CPU Information
		Dim q As New ObjectQuery("SELECT * FROM Win32_Processor")
		Dim p As New ManagementObjectSearcher(q)
		Dim i, i2 As Long ' Long Is Int64
		Dim s As String = Nothing
		For Each cpu As ManagementObject In p.Get()	' there could be more than one processor, like a server?
			i += CInt(cpu("NumberOfCores"))	' counts cores ' reminds me when I used i += 1, "why doesn't VB have i++ like c++?"
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
		i2 = 0 ' now counting elements to pop
		Select Case i ' bytes of memory
			Case Is < 268435456	' You should have at least 256 MB of RAM
				Me.Close() ' should not use this program
			Case 268435456 To 536870912	' 256MB to 512MB
				i2 = 5 ' not suitable for more than 16M
		End Select
		For i = 1 To i2
			MsgBox("rem")
			cmbPrecision.Items.RemoveAt(cmbPrecision.Items.Count - 1)
		Next
		If i2 > 0 Then ' popped elements, update maximum
			maxValChanged(Me, Nothing)
		End If
		' ComboBox Initalization - Visual Studio doesn't have these important properties available for ComboBox controls
		cmbPrecision.SelectedIndex = 6
		cmbDScale.SelectedIndex = 0
		cmbBuffer.SelectedIndex = 2
		' Store progress size difference
		progress.Tag = Me.Width - progress.Width
	End Sub

	' Fix for the size of the progressbar in the status bar
	Private Sub MainForm_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
		If IsNumeric(progress.Tag) Then progress.Size = New Drawing.Size(Me.Width - CInt(progress.Tag), progress.Height)
	End Sub

	Private Sub copyTextBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles copyTextBtn.Click
		' Copy the static label text
		Clipboard.SetText(CType(CType(CType(sender, ToolStripMenuItem).Owner, ContextMenuStrip).SourceControl, Control).Text)
	End Sub

	Private Sub numPrecision_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numPrecision.ValueChanged
		If cmbDScale.SelectedIndex = 0 Then ' IndexOf Note: [compare not] 0 because -1 is not there, 0 is first char
			' 1024s
			If numPrecision.Value Mod 1048576 = 0 Then
				' M
				Dim j As UInteger = CUInt(numPrecision.Value / 1048576)
				For Each i As String In cmbPrecision.Items
					If i.IndexOf("M"c) > 0 And i.Replace("M", "") = CStr(j) Then
						cmbPrecision.SelectedItem = i
						Exit Sub
					End If
				Next
			End If
			If numPrecision.Value Mod 1024 = 0 Then
				' K
				Dim j As UInteger = CUInt(numPrecision.Value / 1024)
				For Each i As String In cmbPrecision.Items
					If i.IndexOf("K"c) > 0 And i.Replace("K", "") = CStr(j) Then
						cmbPrecision.SelectedItem = i
						Exit Sub
					End If
				Next
			End If
		Else
			' 1000s
			If numPrecision.Value Mod 1000000 = 0 Then
				' m
				Dim j As UInteger = CUInt(numPrecision.Value / 1000000)
				For Each i As String In cmbPrecision.Items
					If i.IndexOf("M"c) > 0 And i.Replace("M", "") = CStr(j) Then
						cmbPrecision.SelectedItem = i
						Exit Sub
					End If
				Next
			ElseIf numPrecision.Value Mod 1000 = 0 Then
				' k
				Dim j As UInteger = CUInt(numPrecision.Value / 1000)
				For Each i As String In cmbPrecision.Items
					If i.IndexOf("K"c) > 0 And i.Replace("K", "") = CStr(j) Then
						cmbPrecision.SelectedItem = i
						Exit Sub
					End If
				Next
			End If
		End If
		For Each i As String In cmbPrecision.Items
			If i.IndexOf("K"c) < 0 And i.IndexOf("M"c) < 0 And i = CStr(numPrecision.Value) Then
				cmbPrecision.SelectedItem = i
				Exit Sub
			End If
		Next
		cmbPrecision.SelectedItem = "?"
	End Sub

	Private Sub precisionComboChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPrecision.SelectedIndexChanged, cmbDScale.SelectedIndexChanged
		If cmbPrecision.SelectedIndex = 0 Then
			numPrecision_ValueChanged(sender, New EventArgs)
		ElseIf CStr(cmbPrecision.SelectedItem).IndexOf("M"c) > 0 Then
			numPrecision.Value = CInt(CStr(cmbPrecision.SelectedItem).Replace("M", "")) * If(cmbDScale.SelectedIndex = 0, 1048576, 1000000)
		ElseIf CStr(cmbPrecision.SelectedItem).IndexOf("K"c) > 0 Then
			numPrecision.Value = CInt(CStr(cmbPrecision.SelectedItem).Replace("K", "")) * If(cmbDScale.SelectedIndex = 0, 1024, 1000)
		Else
			numPrecision.Value = CInt(cmbPrecision.SelectedItem)
		End If
	End Sub

	Private Sub maxValChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDScale.SelectedIndexChanged
		Dim l As String = CStr(cmbPrecision.Items(cmbPrecision.Items.Count - 1))
		If l.IndexOf("M"c) > 0 Then
			l = l.Replace("M", "")
			l = CStr(CInt(l) * If(cmbDScale.SelectedIndex = 0, 1048576, 1000000))
		ElseIf l.IndexOf("K"c) > 0 Then
			l = l.Replace("K", "")
			l = CStr(CInt(l) * If(cmbDScale.SelectedIndex = 0, 1024, 1000))
		End If
		numPrecision.Maximum = CDec(l)
	End Sub

	Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
		' update GUI to allow stop
		btnGo.Enabled = False
		btnStop.Enabled = True
		' lock GUI
		numPrecision.Enabled = False
		cmbDScale.Enabled = False
		cmbPrecision.Enabled = False
		' update progress bar
		progress.Value = 0
		progress.Maximum = CInt(numPrecision.Value)
		progressText.Text = "0%"
		' start thread
		piCalc = New CalculatePi(CUInt(numPrecision.Value), Not cmbBuffer.SelectedIndex = 0)
		piCalc.startTicks = Now.Ticks
		' create the thread
		t = New Thread(AddressOf piCalc.Process)
		t.Start()
		t.Name = "Pi Calculator Calculation Thread"
	End Sub

	Public Delegate Sub controlEventInvoker(ByVal sender As System.Object, ByVal e As System.EventArgs)

	Protected Friend Sub stopThread(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click, piCalc.onComplete
		If Me.InvokeRequired Then
			Me.Invoke(New controlEventInvoker(AddressOf stopThread), sender, e)
			Exit Sub
		End If
		' update GUI to allow start
		btnGo.Enabled = True
		btnStop.Enabled = False
		' unlock GUI
		numPrecision.Enabled = True
		cmbDScale.Enabled = True
		cmbPrecision.Enabled = True
		' fill progress bar
		progress.Value = 1
		progress.Maximum = 1
		' update progress text
		progressText.Text = "Idle"
		' delete threads
		If t.IsAlive Then t.Abort() ' stop thread before delete
		t = Nothing	' delete the instance, .NET will reclaim the memory for me
		' retrieve data and delete piCalc
		Dim r As CalculatePi.TimedResult = piCalc.ResultDataNoDelete(If(cmbBuffer.SelectedIndex > 1, CalculatePi.ResultType.First2000, CalculatePi.ResultType.None))
		txtResult.Text = If(sender Is btnStop, "Calculation was stopped" & CrLf, "") & "NOT FINISHED CODING" & CrLf & r.s
		' ticks = 100-nanoseconds
		r.timeDiff = New TimeSpan(Now.Ticks - r.timeStart)
		lblDisplay.Text = r.timeDiff.Seconds.ToString.PadLeft(2, "0"c) & "s " & r.timeDiff.Milliseconds.ToString.PadLeft(3, "0"c) & "ms " & _
		 CStr(Math.Round(r.timeDiff.Ticks * 10) Mod 1000).PadLeft(3, "0"c) & "µs"
		piCalc.diffTicks = New TimeSpan(Now.Ticks - piCalc.startTicks)
		lblCalc.Text = piCalc.diffTicks.Hours.ToString.PadLeft(2, "0"c) & "h " & piCalc.diffTicks.Minutes.ToString.PadLeft(2, "0"c) & "m " & _
		 piCalc.diffTicks.Seconds.ToString.PadLeft(2, "0"c) & "s " & piCalc.diffTicks.Milliseconds.ToString.PadLeft(3, "0"c) & "ms " & _
		 CStr(Math.Round(piCalc.diffTicks.Ticks * 10) Mod 1000).PadLeft(3, "0"c) & "µs"
		piCalc = Nothing
	End Sub

	Public Delegate Sub oneParamInvoker(ByVal i As Object)

	Protected Friend Sub calcProgress(ByVal p As UInteger) Handles piCalc.onProgress
		If Me.InvokeRequired Then
			Me.Invoke(New oneParamInvoker(AddressOf calcProgress), p)
		End If
		progress.Value = CInt(p)	' progress bar
		progressText.Text = CStr(Math.Round(p * 100 / progress.Maximum)) + "%" ' progress text
	End Sub
End Class
