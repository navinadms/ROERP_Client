<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rpt_DailyAllotment
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
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grpUser = New System.Windows.Forms.GroupBox()
        Me.txtAllUsers = New System.Windows.Forms.TextBox()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.chkDate = New System.Windows.Forms.CheckBox()
        Me.GvEnqAllotment = New System.Windows.Forms.DataGridView()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rblUserAllotmentLog = New System.Windows.Forms.RadioButton()
        Me.rblUserAllotment = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEnqNo = New System.Windows.Forms.TextBox()
        Me.lblPartyName = New System.Windows.Forms.Label()
        Me.grpUser.SuspendLayout()
        CType(Me.GvEnqAllotment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(448, 167)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(89, 36)
        Me.btnSearch.TabIndex = 18
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'dtEndDate
        '
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEndDate.Location = New System.Drawing.Point(87, 113)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(121, 20)
        Me.dtEndDate.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(39, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "To :"
        '
        'dtStartDate
        '
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDate.Location = New System.Drawing.Point(87, 76)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(121, 20)
        Me.dtStartDate.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(39, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "From :"
        '
        'cmbUser
        '
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(61, 18)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(150, 21)
        Me.cmbUser.TabIndex = 32
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "User"
        '
        'grpUser
        '
        Me.grpUser.Controls.Add(Me.lblPartyName)
        Me.grpUser.Controls.Add(Me.txtEnqNo)
        Me.grpUser.Controls.Add(Me.Label4)
        Me.grpUser.Controls.Add(Me.txtAllUsers)
        Me.grpUser.Controls.Add(Me.cmbUser)
        Me.grpUser.Controls.Add(Me.btnAddUser)
        Me.grpUser.Controls.Add(Me.Label3)
        Me.grpUser.Location = New System.Drawing.Point(236, 53)
        Me.grpUser.Name = "grpUser"
        Me.grpUser.Size = New System.Drawing.Size(537, 98)
        Me.grpUser.TabIndex = 1
        Me.grpUser.TabStop = False
        '
        'txtAllUsers
        '
        Me.txtAllUsers.Location = New System.Drawing.Point(300, 11)
        Me.txtAllUsers.Multiline = True
        Me.txtAllUsers.Name = "txtAllUsers"
        Me.txtAllUsers.Size = New System.Drawing.Size(155, 39)
        Me.txtAllUsers.TabIndex = 15
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(230, 18)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(44, 23)
        Me.btnAddUser.TabIndex = 12
        Me.btnAddUser.Text = "Add"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'chkDate
        '
        Me.chkDate.AutoSize = True
        Me.chkDate.Location = New System.Drawing.Point(99, 30)
        Me.chkDate.Name = "chkDate"
        Me.chkDate.Size = New System.Drawing.Size(52, 17)
        Me.chkDate.TabIndex = 20
        Me.chkDate.Text = "Date "
        Me.chkDate.UseVisualStyleBackColor = True
        '
        'GvEnqAllotment
        '
        Me.GvEnqAllotment.AllowUserToAddRows = False
        Me.GvEnqAllotment.AllowUserToDeleteRows = False
        Me.GvEnqAllotment.AllowUserToOrderColumns = True
        Me.GvEnqAllotment.AllowUserToResizeColumns = False
        Me.GvEnqAllotment.AllowUserToResizeRows = False
        Me.GvEnqAllotment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvEnqAllotment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvEnqAllotment.Location = New System.Drawing.Point(34, 238)
        Me.GvEnqAllotment.Name = "GvEnqAllotment"
        Me.GvEnqAllotment.Size = New System.Drawing.Size(1135, 359)
        Me.GvEnqAllotment.TabIndex = 21
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Location = New System.Drawing.Point(1062, 196)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(91, 36)
        Me.btnExportExcel.TabIndex = 22
        Me.btnExportExcel.Text = "Export Excel"
        Me.btnExportExcel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rblUserAllotmentLog)
        Me.GroupBox1.Controls.Add(Me.rblUserAllotment)
        Me.GroupBox1.Location = New System.Drawing.Point(282, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(353, 42)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        '
        'rblUserAllotmentLog
        '
        Me.rblUserAllotmentLog.AutoSize = True
        Me.rblUserAllotmentLog.Location = New System.Drawing.Point(166, 13)
        Me.rblUserAllotmentLog.Name = "rblUserAllotmentLog"
        Me.rblUserAllotmentLog.Size = New System.Drawing.Size(114, 17)
        Me.rblUserAllotmentLog.TabIndex = 1
        Me.rblUserAllotmentLog.TabStop = True
        Me.rblUserAllotmentLog.Text = "User Allotment Log"
        Me.rblUserAllotmentLog.UseVisualStyleBackColor = True
        '
        'rblUserAllotment
        '
        Me.rblUserAllotment.AutoSize = True
        Me.rblUserAllotment.Checked = True
        Me.rblUserAllotment.Location = New System.Drawing.Point(28, 13)
        Me.rblUserAllotment.Name = "rblUserAllotment"
        Me.rblUserAllotment.Size = New System.Drawing.Size(96, 17)
        Me.rblUserAllotment.TabIndex = 0
        Me.rblUserAllotment.TabStop = True
        Me.rblUserAllotment.Text = "User Allotment "
        Me.rblUserAllotment.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "EnqNo"
        '
        'txtEnqNo
        '
        Me.txtEnqNo.Enabled = False
        Me.txtEnqNo.Location = New System.Drawing.Point(61, 60)
        Me.txtEnqNo.Name = "txtEnqNo"
        Me.txtEnqNo.Size = New System.Drawing.Size(150, 20)
        Me.txtEnqNo.TabIndex = 35
        '
        'lblPartyName
        '
        Me.lblPartyName.AutoSize = True
        Me.lblPartyName.Location = New System.Drawing.Point(234, 63)
        Me.lblPartyName.Name = "lblPartyName"
        Me.lblPartyName.Size = New System.Drawing.Size(59, 13)
        Me.lblPartyName.TabIndex = 36
        Me.lblPartyName.Text = "PartyName"
        '
        'Rpt_DailyAllotment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1210, 625)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.GvEnqAllotment)
        Me.Controls.Add(Me.chkDate)
        Me.Controls.Add(Me.grpUser)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.dtEndDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtStartDate)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Rpt_DailyAllotment"
        Me.Text = "Rpt_DailyAllotment"
        Me.grpUser.ResumeLayout(False)
        Me.grpUser.PerformLayout()
        CType(Me.GvEnqAllotment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grpUser As System.Windows.Forms.GroupBox
    Friend WithEvents txtAllUsers As System.Windows.Forms.TextBox
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
    Friend WithEvents chkDate As System.Windows.Forms.CheckBox
    Friend WithEvents GvEnqAllotment As System.Windows.Forms.DataGridView
    Friend WithEvents btnExportExcel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rblUserAllotmentLog As System.Windows.Forms.RadioButton
    Friend WithEvents rblUserAllotment As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEnqNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPartyName As System.Windows.Forms.Label
End Class
