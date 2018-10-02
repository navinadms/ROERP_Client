<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EngineerinAttendance
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GvEngineerAttedance = New System.Windows.Forms.DataGridView()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.lblHolidayTotal = New System.Windows.Forms.Label()
        Me.rblHoliday = New System.Windows.Forms.RadioButton()
        Me.sundaycount = New System.Windows.Forms.Label()
        Me.leavecount = New System.Windows.Forms.Label()
        Me.officeodcount = New System.Windows.Forms.Label()
        Me.odcount = New System.Windows.Forms.Label()
        Me.absentcount = New System.Windows.Forms.Label()
        Me.rblSunday = New System.Windows.Forms.RadioButton()
        Me.rblLeave = New System.Windows.Forms.RadioButton()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.officecount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rblOfficeOD = New System.Windows.Forms.RadioButton()
        Me.rblAll = New System.Windows.Forms.RadioButton()
        Me.rblOD = New System.Windows.Forms.RadioButton()
        Me.rblAbsent = New System.Windows.Forms.RadioButton()
        Me.rblOffice = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.lblALLTotal = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GvEngineerAttedance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox1.Controls.Add(Me.GvEngineerAttedance)
        Me.GroupBox1.Controls.Add(Me.btnSubmit)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(997, 588)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GvEngineerAttedance
        '
        Me.GvEngineerAttedance.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar
        Me.GvEngineerAttedance.AllowUserToAddRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GvEngineerAttedance.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GvEngineerAttedance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvEngineerAttedance.Cursor = System.Windows.Forms.Cursors.Default
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GvEngineerAttedance.DefaultCellStyle = DataGridViewCellStyle2
        Me.GvEngineerAttedance.Location = New System.Drawing.Point(9, 113)
        Me.GvEngineerAttedance.Name = "GvEngineerAttedance"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GvEngineerAttedance.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.GvEngineerAttedance.Size = New System.Drawing.Size(982, 427)
        Me.GvEngineerAttedance.TabIndex = 0
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(421, 552)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(87, 30)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblALLTotal)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.dtStartDate)
        Me.GroupBox2.Controls.Add(Me.lblHolidayTotal)
        Me.GroupBox2.Controls.Add(Me.rblHoliday)
        Me.GroupBox2.Controls.Add(Me.sundaycount)
        Me.GroupBox2.Controls.Add(Me.leavecount)
        Me.GroupBox2.Controls.Add(Me.officeodcount)
        Me.GroupBox2.Controls.Add(Me.odcount)
        Me.GroupBox2.Controls.Add(Me.absentcount)
        Me.GroupBox2.Controls.Add(Me.rblSunday)
        Me.GroupBox2.Controls.Add(Me.rblLeave)
        Me.GroupBox2.Controls.Add(Me.btnExport)
        Me.GroupBox2.Controls.Add(Me.officecount)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.rblOfficeOD)
        Me.GroupBox2.Controls.Add(Me.rblAll)
        Me.GroupBox2.Controls.Add(Me.rblOD)
        Me.GroupBox2.Controls.Add(Me.rblAbsent)
        Me.GroupBox2.Controls.Add(Me.rblOffice)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.dtEndDate)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(7, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(982, 98)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(716, 14)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 26)
        Me.btnSearch.TabIndex = 28
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(541, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 16)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "To"
        '
        'dtStartDate
        '
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDate.Location = New System.Drawing.Point(414, 13)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(121, 22)
        Me.dtStartDate.TabIndex = 26
        '
        'lblHolidayTotal
        '
        Me.lblHolidayTotal.AutoSize = True
        Me.lblHolidayTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHolidayTotal.ForeColor = System.Drawing.Color.Maroon
        Me.lblHolidayTotal.Location = New System.Drawing.Point(772, 70)
        Me.lblHolidayTotal.Name = "lblHolidayTotal"
        Me.lblHolidayTotal.Size = New System.Drawing.Size(17, 17)
        Me.lblHolidayTotal.TabIndex = 25
        Me.lblHolidayTotal.Text = "0"
        '
        'rblHoliday
        '
        Me.rblHoliday.AutoSize = True
        Me.rblHoliday.Location = New System.Drawing.Point(753, 46)
        Me.rblHoliday.Name = "rblHoliday"
        Me.rblHoliday.Size = New System.Drawing.Size(73, 20)
        Me.rblHoliday.TabIndex = 24
        Me.rblHoliday.Text = "Holiday"
        Me.rblHoliday.UseVisualStyleBackColor = True
        '
        'sundaycount
        '
        Me.sundaycount.AutoSize = True
        Me.sundaycount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sundaycount.ForeColor = System.Drawing.Color.Maroon
        Me.sundaycount.Location = New System.Drawing.Point(694, 71)
        Me.sundaycount.Name = "sundaycount"
        Me.sundaycount.Size = New System.Drawing.Size(17, 17)
        Me.sundaycount.TabIndex = 23
        Me.sundaycount.Text = "0"
        '
        'leavecount
        '
        Me.leavecount.AutoSize = True
        Me.leavecount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.leavecount.ForeColor = System.Drawing.Color.Maroon
        Me.leavecount.Location = New System.Drawing.Point(636, 71)
        Me.leavecount.Name = "leavecount"
        Me.leavecount.Size = New System.Drawing.Size(17, 17)
        Me.leavecount.TabIndex = 22
        Me.leavecount.Text = "0"
        '
        'officeodcount
        '
        Me.officeodcount.AutoSize = True
        Me.officeodcount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.officeodcount.ForeColor = System.Drawing.Color.Maroon
        Me.officeodcount.Location = New System.Drawing.Point(566, 71)
        Me.officeodcount.Name = "officeodcount"
        Me.officeodcount.Size = New System.Drawing.Size(17, 17)
        Me.officeodcount.TabIndex = 21
        Me.officeodcount.Text = "0"
        '
        'odcount
        '
        Me.odcount.AutoSize = True
        Me.odcount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.odcount.ForeColor = System.Drawing.Color.Maroon
        Me.odcount.Location = New System.Drawing.Point(497, 71)
        Me.odcount.Name = "odcount"
        Me.odcount.Size = New System.Drawing.Size(17, 17)
        Me.odcount.TabIndex = 20
        Me.odcount.Text = "0"
        '
        'absentcount
        '
        Me.absentcount.AutoSize = True
        Me.absentcount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.absentcount.ForeColor = System.Drawing.Color.Maroon
        Me.absentcount.Location = New System.Drawing.Point(427, 70)
        Me.absentcount.Name = "absentcount"
        Me.absentcount.Size = New System.Drawing.Size(17, 17)
        Me.absentcount.TabIndex = 19
        Me.absentcount.Text = "0"
        '
        'rblSunday
        '
        Me.rblSunday.AutoSize = True
        Me.rblSunday.Location = New System.Drawing.Point(679, 46)
        Me.rblSunday.Name = "rblSunday"
        Me.rblSunday.Size = New System.Drawing.Size(72, 20)
        Me.rblSunday.TabIndex = 18
        Me.rblSunday.Text = "Sunday"
        Me.rblSunday.UseVisualStyleBackColor = True
        '
        'rblLeave
        '
        Me.rblLeave.AutoSize = True
        Me.rblLeave.Location = New System.Drawing.Point(614, 46)
        Me.rblLeave.Name = "rblLeave"
        Me.rblLeave.Size = New System.Drawing.Size(64, 20)
        Me.rblLeave.TabIndex = 17
        Me.rblLeave.Text = "Leave"
        Me.rblLeave.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(858, 65)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(118, 29)
        Me.btnExport.TabIndex = 16
        Me.btnExport.Text = "Export To Excel"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'officecount
        '
        Me.officecount.AutoSize = True
        Me.officecount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.officecount.ForeColor = System.Drawing.Color.Maroon
        Me.officecount.Location = New System.Drawing.Point(352, 70)
        Me.officecount.Name = "officecount"
        Me.officecount.Size = New System.Drawing.Size(17, 17)
        Me.officecount.TabIndex = 15
        Me.officecount.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(284, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 16)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Total"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'rblOfficeOD
        '
        Me.rblOfficeOD.AutoSize = True
        Me.rblOfficeOD.Location = New System.Drawing.Point(531, 45)
        Me.rblOfficeOD.Name = "rblOfficeOD"
        Me.rblOfficeOD.Size = New System.Drawing.Size(83, 20)
        Me.rblOfficeOD.TabIndex = 13
        Me.rblOfficeOD.Text = "Office OD"
        Me.rblOfficeOD.UseVisualStyleBackColor = True
        '
        'rblAll
        '
        Me.rblAll.AutoSize = True
        Me.rblAll.Location = New System.Drawing.Point(831, 44)
        Me.rblAll.Name = "rblAll"
        Me.rblAll.Size = New System.Drawing.Size(41, 20)
        Me.rblAll.TabIndex = 12
        Me.rblAll.Text = "All"
        Me.rblAll.UseVisualStyleBackColor = True
        '
        'rblOD
        '
        Me.rblOD.AutoSize = True
        Me.rblOD.Location = New System.Drawing.Point(479, 46)
        Me.rblOD.Name = "rblOD"
        Me.rblOD.Size = New System.Drawing.Size(46, 20)
        Me.rblOD.TabIndex = 11
        Me.rblOD.Text = "OD"
        Me.rblOD.UseVisualStyleBackColor = True
        '
        'rblAbsent
        '
        Me.rblAbsent.AutoSize = True
        Me.rblAbsent.Location = New System.Drawing.Point(405, 46)
        Me.rblAbsent.Name = "rblAbsent"
        Me.rblAbsent.Size = New System.Drawing.Size(68, 20)
        Me.rblAbsent.TabIndex = 10
        Me.rblAbsent.Text = "Absent"
        Me.rblAbsent.UseVisualStyleBackColor = True
        '
        'rblOffice
        '
        Me.rblOffice.AutoSize = True
        Me.rblOffice.Location = New System.Drawing.Point(339, 46)
        Me.rblOffice.Name = "rblOffice"
        Me.rblOffice.Size = New System.Drawing.Size(60, 20)
        Me.rblOffice.TabIndex = 9
        Me.rblOffice.Text = "Office"
        Me.rblOffice.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(281, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Type"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(328, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "From Date"
        '
        'dtEndDate
        '
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEndDate.Location = New System.Drawing.Point(572, 13)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(121, 22)
        Me.dtEndDate.TabIndex = 5
        '
        'lblALLTotal
        '
        Me.lblALLTotal.AutoSize = True
        Me.lblALLTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblALLTotal.ForeColor = System.Drawing.Color.Maroon
        Me.lblALLTotal.Location = New System.Drawing.Point(835, 70)
        Me.lblALLTotal.Name = "lblALLTotal"
        Me.lblALLTotal.Size = New System.Drawing.Size(17, 17)
        Me.lblALLTotal.TabIndex = 29
        Me.lblALLTotal.Text = "0"
        '
        'EngineerinAttendance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 601)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "EngineerinAttendance"
        Me.Text = "Engineering Attendance"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GvEngineerAttedance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GvEngineerAttedance As System.Windows.Forms.DataGridView
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rblOffice As System.Windows.Forms.RadioButton
    Friend WithEvents rblAbsent As System.Windows.Forms.RadioButton
    Friend WithEvents rblOD As System.Windows.Forms.RadioButton
    Friend WithEvents rblAll As System.Windows.Forms.RadioButton
    Friend WithEvents rblOfficeOD As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents officecount As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents rblLeave As System.Windows.Forms.RadioButton
    Friend WithEvents rblSunday As System.Windows.Forms.RadioButton
    Friend WithEvents absentcount As System.Windows.Forms.Label
    Friend WithEvents odcount As System.Windows.Forms.Label
    Friend WithEvents officeodcount As System.Windows.Forms.Label
    Friend WithEvents leavecount As System.Windows.Forms.Label
    Friend WithEvents sundaycount As System.Windows.Forms.Label
    Friend WithEvents rblHoliday As System.Windows.Forms.RadioButton
    Friend WithEvents lblHolidayTotal As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblALLTotal As System.Windows.Forms.Label
End Class
