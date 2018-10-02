<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServiceComplainReport
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
        Me.chkIsDoneDate = New System.Windows.Forms.CheckBox()
        Me.btnCancelAll = New System.Windows.Forms.Button()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.rblECComplain = New System.Windows.Forms.RadioButton()
        Me.rblServiceComplain = New System.Windows.Forms.RadioButton()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtALLSSR = New System.Windows.Forms.TextBox()
        Me.btnAddSSR = New System.Windows.Forms.Button()
        Me.ddlSSR = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ddlReqLevel = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAllLevel = New System.Windows.Forms.TextBox()
        Me.btnAddLevel = New System.Windows.Forms.Button()
        Me.ddlMachineType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.txtAllMachine = New System.Windows.Forms.TextBox()
        Me.txtAllUser = New System.Windows.Forms.TextBox()
        Me.btnAddMachine = New System.Windows.Forms.Button()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.chkIsDate = New System.Windows.Forms.CheckBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.dtToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtFromDate = New System.Windows.Forms.DateTimePicker()
        Me.GvServiceComplainReport = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.GvServiceComplainReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkIsDoneDate)
        Me.GroupBox1.Controls.Add(Me.btnCancelAll)
        Me.GroupBox1.Controls.Add(Me.Label37)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.Panel5)
        Me.GroupBox1.Controls.Add(Me.btnExport)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.chkIsDate)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Controls.Add(Me.dtToDate)
        Me.GroupBox1.Controls.Add(Me.dtFromDate)
        Me.GroupBox1.Controls.Add(Me.GvServiceComplainReport)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1026, 627)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkIsDoneDate
        '
        Me.chkIsDoneDate.AutoSize = True
        Me.chkIsDoneDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsDoneDate.Location = New System.Drawing.Point(67, 37)
        Me.chkIsDoneDate.Name = "chkIsDoneDate"
        Me.chkIsDoneDate.Size = New System.Drawing.Size(122, 17)
        Me.chkIsDoneDate.TabIndex = 82
        Me.chkIsDoneDate.Text = "SSR Status Date"
        Me.chkIsDoneDate.UseVisualStyleBackColor = True
        '
        'btnCancelAll
        '
        Me.btnCancelAll.Location = New System.Drawing.Point(542, 215)
        Me.btnCancelAll.Name = "btnCancelAll"
        Me.btnCancelAll.Size = New System.Drawing.Size(111, 31)
        Me.btnCancelAll.TabIndex = 81
        Me.btnCancelAll.Text = "Cancel"
        Me.btnCancelAll.UseVisualStyleBackColor = True
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(397, 29)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(86, 13)
        Me.Label37.TabIndex = 80
        Me.Label37.Text = "Request Type"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.RadioButton1)
        Me.GroupBox5.Controls.Add(Me.rblECComplain)
        Me.GroupBox5.Controls.Add(Me.rblServiceComplain)
        Me.GroupBox5.Location = New System.Drawing.Point(487, 19)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(196, 35)
        Me.GroupBox5.TabIndex = 79
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = " "
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(132, 12)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(44, 17)
        Me.RadioButton1.TabIndex = 2
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "ALL"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'rblECComplain
        '
        Me.rblECComplain.AutoSize = True
        Me.rblECComplain.Location = New System.Drawing.Point(79, 10)
        Me.rblECComplain.Name = "rblECComplain"
        Me.rblECComplain.Size = New System.Drawing.Size(39, 17)
        Me.rblECComplain.TabIndex = 1
        Me.rblECComplain.TabStop = True
        Me.rblECComplain.Text = "EC"
        Me.rblECComplain.UseVisualStyleBackColor = True
        '
        'rblServiceComplain
        '
        Me.rblServiceComplain.AutoSize = True
        Me.rblServiceComplain.Checked = True
        Me.rblServiceComplain.Location = New System.Drawing.Point(9, 10)
        Me.rblServiceComplain.Name = "rblServiceComplain"
        Me.rblServiceComplain.Size = New System.Drawing.Size(61, 17)
        Me.rblServiceComplain.TabIndex = 0
        Me.rblServiceComplain.TabStop = True
        Me.rblServiceComplain.Text = "Service"
        Me.rblServiceComplain.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.txtALLSSR)
        Me.Panel5.Controls.Add(Me.btnAddSSR)
        Me.Panel5.Controls.Add(Me.ddlSSR)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.ddlReqLevel)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.txtAllLevel)
        Me.Panel5.Controls.Add(Me.btnAddLevel)
        Me.Panel5.Controls.Add(Me.ddlMachineType)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Controls.Add(Me.cmbUser)
        Me.Panel5.Controls.Add(Me.txtAllMachine)
        Me.Panel5.Controls.Add(Me.txtAllUser)
        Me.Panel5.Controls.Add(Me.btnAddMachine)
        Me.Panel5.Controls.Add(Me.btnAddUser)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Location = New System.Drawing.Point(212, 67)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(796, 133)
        Me.Panel5.TabIndex = 17
        '
        'txtALLSSR
        '
        Me.txtALLSSR.Location = New System.Drawing.Point(606, 84)
        Me.txtALLSSR.Multiline = True
        Me.txtALLSSR.Name = "txtALLSSR"
        Me.txtALLSSR.Size = New System.Drawing.Size(166, 39)
        Me.txtALLSSR.TabIndex = 89
        '
        'btnAddSSR
        '
        Me.btnAddSSR.Location = New System.Drawing.Point(657, 55)
        Me.btnAddSSR.Name = "btnAddSSR"
        Me.btnAddSSR.Size = New System.Drawing.Size(44, 23)
        Me.btnAddSSR.TabIndex = 88
        Me.btnAddSSR.Text = "Add"
        Me.btnAddSSR.UseVisualStyleBackColor = True
        '
        'ddlSSR
        '
        Me.ddlSSR.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ddlSSR.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlSSR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlSSR.FormattingEnabled = True
        Me.ddlSSR.Items.AddRange(New Object() {"Pending", "SCH", "Running", "Done", "Cancel"})
        Me.ddlSSR.Location = New System.Drawing.Point(612, 30)
        Me.ddlSSR.Name = "ddlSSR"
        Me.ddlSSR.Size = New System.Drawing.Size(166, 21)
        Me.ddlSSR.TabIndex = 87
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(654, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "SSR Type"
        '
        'ddlReqLevel
        '
        Me.ddlReqLevel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ddlReqLevel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlReqLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlReqLevel.FormattingEnabled = True
        Me.ddlReqLevel.Items.AddRange(New Object() {"A+++", "A++", "A+"})
        Me.ddlReqLevel.Location = New System.Drawing.Point(419, 30)
        Me.ddlReqLevel.Name = "ddlReqLevel"
        Me.ddlReqLevel.Size = New System.Drawing.Size(166, 21)
        Me.ddlReqLevel.TabIndex = 85
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(461, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 84
        Me.Label2.Text = "Request Level"
        '
        'txtAllLevel
        '
        Me.txtAllLevel.Location = New System.Drawing.Point(419, 85)
        Me.txtAllLevel.Multiline = True
        Me.txtAllLevel.Name = "txtAllLevel"
        Me.txtAllLevel.Size = New System.Drawing.Size(166, 39)
        Me.txtAllLevel.TabIndex = 82
        '
        'btnAddLevel
        '
        Me.btnAddLevel.Location = New System.Drawing.Point(477, 56)
        Me.btnAddLevel.Name = "btnAddLevel"
        Me.btnAddLevel.Size = New System.Drawing.Size(44, 23)
        Me.btnAddLevel.TabIndex = 81
        Me.btnAddLevel.Text = "Add"
        Me.btnAddLevel.UseVisualStyleBackColor = True
        '
        'ddlMachineType
        '
        Me.ddlMachineType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ddlMachineType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlMachineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlMachineType.FormattingEnabled = True
        Me.ddlMachineType.Items.AddRange(New Object() {"BATCH COADING", "BLOW", "BOTTLING", "BULK", "DM", "DM+MB", "MB", "GLASS", "IND", "ISI", "JAR", "LAB", "LABLE", "NON ISI", "OTHER", "POUCH", "RO", "SODA", "SF"})
        Me.ddlMachineType.Location = New System.Drawing.Point(244, 28)
        Me.ddlMachineType.Name = "ddlMachineType"
        Me.ddlMachineType.Size = New System.Drawing.Size(139, 21)
        Me.ddlMachineType.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(274, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "Machine Type"
        '
        'cmbUser
        '
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(33, 30)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(176, 21)
        Me.cmbUser.TabIndex = 79
        '
        'txtAllMachine
        '
        Me.txtAllMachine.Location = New System.Drawing.Point(231, 84)
        Me.txtAllMachine.Multiline = True
        Me.txtAllMachine.Name = "txtAllMachine"
        Me.txtAllMachine.Size = New System.Drawing.Size(166, 39)
        Me.txtAllMachine.TabIndex = 15
        '
        'txtAllUser
        '
        Me.txtAllUser.Location = New System.Drawing.Point(33, 88)
        Me.txtAllUser.Multiline = True
        Me.txtAllUser.Name = "txtAllUser"
        Me.txtAllUser.Size = New System.Drawing.Size(176, 38)
        Me.txtAllUser.TabIndex = 17
        '
        'btnAddMachine
        '
        Me.btnAddMachine.Location = New System.Drawing.Point(289, 55)
        Me.btnAddMachine.Name = "btnAddMachine"
        Me.btnAddMachine.Size = New System.Drawing.Size(44, 23)
        Me.btnAddMachine.TabIndex = 3
        Me.btnAddMachine.Text = "Add"
        Me.btnAddMachine.UseVisualStyleBackColor = True
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(106, 59)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(44, 23)
        Me.btnAddUser.TabIndex = 1
        Me.btnAddUser.Text = "Add"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(103, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "User"
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(903, 216)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(94, 31)
        Me.btnExport.TabIndex = 78
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(400, 215)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(111, 31)
        Me.btnSearch.TabIndex = 77
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'chkIsDate
        '
        Me.chkIsDate.AutoSize = True
        Me.chkIsDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsDate.Location = New System.Drawing.Point(72, 75)
        Me.chkIsDate.Name = "chkIsDate"
        Me.chkIsDate.Size = New System.Drawing.Size(67, 17)
        Me.chkIsDate.TabIndex = 76
        Me.chkIsDate.Text = "Is Date"
        Me.chkIsDate.UseVisualStyleBackColor = True
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(20, 149)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(20, 13)
        Me.Label34.TabIndex = 75
        Me.Label34.Text = "To"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(19, 115)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(30, 13)
        Me.Label33.TabIndex = 74
        Me.Label33.Text = "From"
        '
        'dtToDate
        '
        Me.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtToDate.Location = New System.Drawing.Point(64, 147)
        Me.dtToDate.Name = "dtToDate"
        Me.dtToDate.Size = New System.Drawing.Size(127, 20)
        Me.dtToDate.TabIndex = 73
        '
        'dtFromDate
        '
        Me.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFromDate.Location = New System.Drawing.Point(64, 112)
        Me.dtFromDate.Name = "dtFromDate"
        Me.dtFromDate.Size = New System.Drawing.Size(127, 20)
        Me.dtFromDate.TabIndex = 72
        '
        'GvServiceComplainReport
        '
        Me.GvServiceComplainReport.AllowDrop = True
        Me.GvServiceComplainReport.AllowUserToAddRows = False
        Me.GvServiceComplainReport.AllowUserToDeleteRows = False
        Me.GvServiceComplainReport.AllowUserToOrderColumns = True
        Me.GvServiceComplainReport.AllowUserToResizeColumns = False
        Me.GvServiceComplainReport.AllowUserToResizeRows = False
        Me.GvServiceComplainReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvServiceComplainReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvServiceComplainReport.Location = New System.Drawing.Point(6, 253)
        Me.GvServiceComplainReport.Name = "GvServiceComplainReport"
        Me.GvServiceComplainReport.Size = New System.Drawing.Size(1002, 352)
        Me.GvServiceComplainReport.TabIndex = 0
        '
        'ServiceComplainReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 651)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ServiceComplainReport"
        Me.Text = "ServiceComplainReport"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.GvServiceComplainReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GvServiceComplainReport As System.Windows.Forms.DataGridView
    Friend WithEvents chkIsDate As System.Windows.Forms.CheckBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents dtToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txtAllMachine As System.Windows.Forms.TextBox
    Friend WithEvents txtAllUser As System.Windows.Forms.TextBox
    Friend WithEvents btnAddMachine As System.Windows.Forms.Button
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ddlMachineType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAllLevel As System.Windows.Forms.TextBox
    Friend WithEvents btnAddLevel As System.Windows.Forms.Button
    Friend WithEvents ddlReqLevel As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents rblECComplain As System.Windows.Forms.RadioButton
    Friend WithEvents rblServiceComplain As System.Windows.Forms.RadioButton
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents ddlSSR As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtALLSSR As System.Windows.Forms.TextBox
    Friend WithEvents btnAddSSR As System.Windows.Forms.Button
    Friend WithEvents btnCancelAll As System.Windows.Forms.Button
    Friend WithEvents chkIsDoneDate As System.Windows.Forms.CheckBox
End Class
