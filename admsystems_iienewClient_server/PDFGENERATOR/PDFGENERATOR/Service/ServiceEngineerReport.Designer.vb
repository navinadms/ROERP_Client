<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServiceEngineerReport
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.GvEngineerReport = New System.Windows.Forms.DataGridView()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtMachineAll = New System.Windows.Forms.TextBox()
        Me.btnAddMachine = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ddlMachine = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtEngineer_All = New System.Windows.Forms.TextBox()
        Me.btnEnggAdd = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ddlEngineer = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtAtta_Status = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rblODStatus_None = New System.Windows.Forms.RadioButton()
        Me.rblODStatus_Cancel = New System.Windows.Forms.RadioButton()
        Me.rblODStatus_Running = New System.Windows.Forms.RadioButton()
        Me.rblODStatus_Done = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rblODType_Visit = New System.Windows.Forms.RadioButton()
        Me.rblODType_None = New System.Windows.Forms.RadioButton()
        Me.rblODType_Service = New System.Windows.Forms.RadioButton()
        Me.rblODTypeEC = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.GvEngineerReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExportExcel)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.GroupBox8)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1019, 638)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Location = New System.Drawing.Point(857, 185)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(91, 31)
        Me.btnExportExcel.TabIndex = 10
        Me.btnExportExcel.Text = "Export"
        Me.btnExportExcel.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(463, 173)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 36)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(323, 173)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(120, 36)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.GvEngineerReport)
        Me.GroupBox8.Location = New System.Drawing.Point(29, 222)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(949, 410)
        Me.GroupBox8.TabIndex = 5
        Me.GroupBox8.TabStop = False
        '
        'GvEngineerReport
        '
        Me.GvEngineerReport.AllowDrop = True
        Me.GvEngineerReport.AllowUserToAddRows = False
        Me.GvEngineerReport.AllowUserToDeleteRows = False
        Me.GvEngineerReport.AllowUserToOrderColumns = True
        Me.GvEngineerReport.AllowUserToResizeColumns = False
        Me.GvEngineerReport.AllowUserToResizeRows = False
        Me.GvEngineerReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvEngineerReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvEngineerReport.Location = New System.Drawing.Point(15, 9)
        Me.GvEngineerReport.Name = "GvEngineerReport"
        Me.GvEngineerReport.Size = New System.Drawing.Size(934, 373)
        Me.GvEngineerReport.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtMachineAll)
        Me.GroupBox4.Controls.Add(Me.btnAddMachine)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.ddlMachine)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(810, 14)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(200, 153)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        '
        'txtMachineAll
        '
        Me.txtMachineAll.Location = New System.Drawing.Point(6, 94)
        Me.txtMachineAll.Multiline = True
        Me.txtMachineAll.Name = "txtMachineAll"
        Me.txtMachineAll.Size = New System.Drawing.Size(175, 49)
        Me.txtMachineAll.TabIndex = 3
        '
        'btnAddMachine
        '
        Me.btnAddMachine.Location = New System.Drawing.Point(47, 61)
        Me.btnAddMachine.Name = "btnAddMachine"
        Me.btnAddMachine.Size = New System.Drawing.Size(75, 23)
        Me.btnAddMachine.TabIndex = 2
        Me.btnAddMachine.Text = "Add"
        Me.btnAddMachine.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(62, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 15)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Machine"
        '
        'ddlMachine
        '
        Me.ddlMachine.FormattingEnabled = True
        Me.ddlMachine.Location = New System.Drawing.Point(24, 34)
        Me.ddlMachine.Name = "ddlMachine"
        Me.ddlMachine.Size = New System.Drawing.Size(146, 23)
        Me.ddlMachine.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtEngineer_All)
        Me.GroupBox3.Controls.Add(Me.btnEnggAdd)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.ddlEngineer)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(607, 16)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(200, 151)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'txtEngineer_All
        '
        Me.txtEngineer_All.Location = New System.Drawing.Point(6, 92)
        Me.txtEngineer_All.Multiline = True
        Me.txtEngineer_All.Name = "txtEngineer_All"
        Me.txtEngineer_All.Size = New System.Drawing.Size(188, 49)
        Me.txtEngineer_All.TabIndex = 3
        '
        'btnEnggAdd
        '
        Me.btnEnggAdd.Location = New System.Drawing.Point(44, 61)
        Me.btnEnggAdd.Name = "btnEnggAdd"
        Me.btnEnggAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnEnggAdd.TabIndex = 2
        Me.btnEnggAdd.Text = "Add"
        Me.btnEnggAdd.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(62, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Engineer"
        '
        'ddlEngineer
        '
        Me.ddlEngineer.FormattingEnabled = True
        Me.ddlEngineer.Location = New System.Drawing.Point(24, 32)
        Me.ddlEngineer.Name = "ddlEngineer"
        Me.ddlEngineer.Size = New System.Drawing.Size(146, 23)
        Me.ddlEngineer.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox7)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.dtEndDate)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.dtStartDate)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(16, 18)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(591, 145)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtPartyName)
        Me.GroupBox7.Controls.Add(Me.Label8)
        Me.GroupBox7.Controls.Add(Me.txtAtta_Status)
        Me.GroupBox7.Location = New System.Drawing.Point(384, 27)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(201, 112)
        Me.GroupBox7.TabIndex = 8
        Me.GroupBox7.TabStop = False
        '
        'txtPartyName
        '
        Me.txtPartyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtPartyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPartyName.Location = New System.Drawing.Point(6, 76)
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.Size = New System.Drawing.Size(195, 21)
        Me.txtPartyName.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(56, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 15)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Party Name"
        '
        'txtAtta_Status
        '
        Me.txtAtta_Status.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtAtta_Status.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtAtta_Status.Location = New System.Drawing.Point(13, 19)
        Me.txtAtta_Status.Name = "txtAtta_Status"
        Me.txtAtta_Status.Size = New System.Drawing.Size(148, 21)
        Me.txtAtta_Status.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(413, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(105, 15)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Attendance Status"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.rblODStatus_None)
        Me.GroupBox6.Controls.Add(Me.rblODStatus_Cancel)
        Me.GroupBox6.Controls.Add(Me.rblODStatus_Running)
        Me.GroupBox6.Controls.Add(Me.rblODStatus_Done)
        Me.GroupBox6.Location = New System.Drawing.Point(100, 94)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(278, 45)
        Me.GroupBox6.TabIndex = 6
        Me.GroupBox6.TabStop = False
        '
        'rblODStatus_None
        '
        Me.rblODStatus_None.AutoSize = True
        Me.rblODStatus_None.Checked = True
        Me.rblODStatus_None.Location = New System.Drawing.Point(217, 15)
        Me.rblODStatus_None.Name = "rblODStatus_None"
        Me.rblODStatus_None.Size = New System.Drawing.Size(55, 19)
        Me.rblODStatus_None.TabIndex = 4
        Me.rblODStatus_None.TabStop = True
        Me.rblODStatus_None.Text = "None"
        Me.rblODStatus_None.UseVisualStyleBackColor = False
        '
        'rblODStatus_Cancel
        '
        Me.rblODStatus_Cancel.AutoSize = True
        Me.rblODStatus_Cancel.Location = New System.Drawing.Point(150, 15)
        Me.rblODStatus_Cancel.Name = "rblODStatus_Cancel"
        Me.rblODStatus_Cancel.Size = New System.Drawing.Size(63, 19)
        Me.rblODStatus_Cancel.TabIndex = 2
        Me.rblODStatus_Cancel.TabStop = True
        Me.rblODStatus_Cancel.Text = "Cancel"
        Me.rblODStatus_Cancel.UseVisualStyleBackColor = True
        '
        'rblODStatus_Running
        '
        Me.rblODStatus_Running.AutoSize = True
        Me.rblODStatus_Running.Location = New System.Drawing.Point(74, 14)
        Me.rblODStatus_Running.Name = "rblODStatus_Running"
        Me.rblODStatus_Running.Size = New System.Drawing.Size(72, 19)
        Me.rblODStatus_Running.TabIndex = 1
        Me.rblODStatus_Running.TabStop = True
        Me.rblODStatus_Running.Text = "Running"
        Me.rblODStatus_Running.UseVisualStyleBackColor = True
        '
        'rblODStatus_Done
        '
        Me.rblODStatus_Done.AutoSize = True
        Me.rblODStatus_Done.Location = New System.Drawing.Point(10, 14)
        Me.rblODStatus_Done.Name = "rblODStatus_Done"
        Me.rblODStatus_Done.Size = New System.Drawing.Size(55, 19)
        Me.rblODStatus_Done.TabIndex = 0
        Me.rblODStatus_Done.TabStop = True
        Me.rblODStatus_Done.Text = "Done"
        Me.rblODStatus_Done.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 15)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "OD Site Status"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rblODType_Visit)
        Me.GroupBox5.Controls.Add(Me.rblODType_None)
        Me.GroupBox5.Controls.Add(Me.rblODType_Service)
        Me.GroupBox5.Controls.Add(Me.rblODTypeEC)
        Me.GroupBox5.Location = New System.Drawing.Point(70, 55)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(308, 33)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        '
        'rblODType_Visit
        '
        Me.rblODType_Visit.AutoSize = True
        Me.rblODType_Visit.Location = New System.Drawing.Point(149, 10)
        Me.rblODType_Visit.Name = "rblODType_Visit"
        Me.rblODType_Visit.Size = New System.Drawing.Size(47, 19)
        Me.rblODType_Visit.TabIndex = 4
        Me.rblODType_Visit.TabStop = True
        Me.rblODType_Visit.Text = "Visit"
        Me.rblODType_Visit.UseVisualStyleBackColor = True
        '
        'rblODType_None
        '
        Me.rblODType_None.AutoSize = True
        Me.rblODType_None.Checked = True
        Me.rblODType_None.Location = New System.Drawing.Point(216, 10)
        Me.rblODType_None.Name = "rblODType_None"
        Me.rblODType_None.Size = New System.Drawing.Size(55, 19)
        Me.rblODType_None.TabIndex = 3
        Me.rblODType_None.TabStop = True
        Me.rblODType_None.Text = "None"
        Me.rblODType_None.UseVisualStyleBackColor = True
        '
        'rblODType_Service
        '
        Me.rblODType_Service.AutoSize = True
        Me.rblODType_Service.Location = New System.Drawing.Point(74, 10)
        Me.rblODType_Service.Name = "rblODType_Service"
        Me.rblODType_Service.Size = New System.Drawing.Size(65, 19)
        Me.rblODType_Service.TabIndex = 1
        Me.rblODType_Service.TabStop = True
        Me.rblODType_Service.Text = "Service"
        Me.rblODType_Service.UseVisualStyleBackColor = True
        '
        'rblODTypeEC
        '
        Me.rblODTypeEC.AutoSize = True
        Me.rblODTypeEC.Location = New System.Drawing.Point(9, 10)
        Me.rblODTypeEC.Name = "rblODTypeEC"
        Me.rblODTypeEC.Size = New System.Drawing.Size(41, 19)
        Me.rblODTypeEC.TabIndex = 0
        Me.rblODTypeEC.TabStop = True
        Me.rblODTypeEC.Text = "EC"
        Me.rblODTypeEC.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 15)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "OD Type"
        '
        'dtEndDate
        '
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEndDate.Location = New System.Drawing.Point(219, 24)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(122, 21)
        Me.dtEndDate.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(184, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "End"
        '
        'dtStartDate
        '
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDate.Location = New System.Drawing.Point(87, 24)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(90, 21)
        Me.dtStartDate.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Start"
        '
        'ServiceEngineerReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1043, 667)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ServiceEngineerReport"
        Me.Text = "ServiceEngineerReport"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        CType(Me.GvEngineerReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ddlEngineer As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMachineAll As System.Windows.Forms.TextBox
    Friend WithEvents btnAddMachine As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ddlMachine As System.Windows.Forms.ComboBox
    Friend WithEvents txtEngineer_All As System.Windows.Forms.TextBox
    Friend WithEvents btnEnggAdd As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rblODTypeEC As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rblODStatus_Running As System.Windows.Forms.RadioButton
    Friend WithEvents rblODStatus_Done As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents rblODType_Service As System.Windows.Forms.RadioButton
    Friend WithEvents rblODStatus_Cancel As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents GvEngineerReport As System.Windows.Forms.DataGridView
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents rblODType_None As System.Windows.Forms.RadioButton
    Friend WithEvents rblODStatus_None As System.Windows.Forms.RadioButton
    Friend WithEvents btnExportExcel As System.Windows.Forms.Button
    Friend WithEvents rblODType_Visit As System.Windows.Forms.RadioButton
    Friend WithEvents txtAtta_Status As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPartyName As System.Windows.Forms.TextBox
End Class
