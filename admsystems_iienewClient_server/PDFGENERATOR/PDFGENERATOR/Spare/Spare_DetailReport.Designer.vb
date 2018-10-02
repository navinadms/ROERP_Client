<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Spare_DetailReport
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
        Me.GvSpareReport = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.GrpStatus = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtStatusAll = New System.Windows.Forms.TextBox()
        Me.btnAddStatus = New System.Windows.Forms.Button()
        Me.ddlSearchStatus = New System.Windows.Forms.ComboBox()
        Me.GrpUser = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAllUser = New System.Windows.Forms.TextBox()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rblOutStanding = New System.Windows.Forms.RadioButton()
        Me.rblInvoice = New System.Windows.Forms.RadioButton()
        Me.rblQuotation = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GvSpareReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpStatus.SuspendLayout()
        Me.GrpUser.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GvSpareReport)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 208)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(914, 329)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " "
        '
        'GvSpareReport
        '
        Me.GvSpareReport.AllowDrop = True
        Me.GvSpareReport.AllowUserToAddRows = False
        Me.GvSpareReport.AllowUserToDeleteRows = False
        Me.GvSpareReport.AllowUserToOrderColumns = True
        Me.GvSpareReport.AllowUserToResizeColumns = False
        Me.GvSpareReport.AllowUserToResizeRows = False
        Me.GvSpareReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvSpareReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvSpareReport.Location = New System.Drawing.Point(6, 19)
        Me.GvSpareReport.Name = "GvSpareReport"
        Me.GvSpareReport.Size = New System.Drawing.Size(902, 304)
        Me.GvSpareReport.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(41, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Start Date"
        '
        'dtStartDate
        '
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDate.Location = New System.Drawing.Point(118, 60)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(95, 20)
        Me.dtStartDate.TabIndex = 3
        '
        'dtEndDate
        '
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEndDate.Location = New System.Drawing.Point(118, 101)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(95, 20)
        Me.dtEndDate.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "End Date"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(353, 172)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(89, 33)
        Me.btnSearch.TabIndex = 42
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(459, 172)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(89, 33)
        Me.btnCancel.TabIndex = 43
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(828, 174)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(92, 28)
        Me.btnExport.TabIndex = 44
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'GrpStatus
        '
        Me.GrpStatus.Controls.Add(Me.Label3)
        Me.GrpStatus.Controls.Add(Me.txtStatusAll)
        Me.GrpStatus.Controls.Add(Me.btnAddStatus)
        Me.GrpStatus.Controls.Add(Me.ddlSearchStatus)
        Me.GrpStatus.Location = New System.Drawing.Point(291, 46)
        Me.GrpStatus.Name = "GrpStatus"
        Me.GrpStatus.Size = New System.Drawing.Size(257, 120)
        Me.GrpStatus.TabIndex = 45
        Me.GrpStatus.TabStop = False
        Me.GrpStatus.Text = " "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Status"
        '
        'txtStatusAll
        '
        Me.txtStatusAll.Location = New System.Drawing.Point(61, 79)
        Me.txtStatusAll.Multiline = True
        Me.txtStatusAll.Name = "txtStatusAll"
        Me.txtStatusAll.Size = New System.Drawing.Size(177, 33)
        Me.txtStatusAll.TabIndex = 2
        '
        'btnAddStatus
        '
        Me.btnAddStatus.Location = New System.Drawing.Point(96, 48)
        Me.btnAddStatus.Name = "btnAddStatus"
        Me.btnAddStatus.Size = New System.Drawing.Size(75, 23)
        Me.btnAddStatus.TabIndex = 1
        Me.btnAddStatus.Text = "Add"
        Me.btnAddStatus.UseVisualStyleBackColor = True
        '
        'ddlSearchStatus
        '
        Me.ddlSearchStatus.FormattingEnabled = True
        Me.ddlSearchStatus.Location = New System.Drawing.Point(61, 19)
        Me.ddlSearchStatus.Name = "ddlSearchStatus"
        Me.ddlSearchStatus.Size = New System.Drawing.Size(160, 21)
        Me.ddlSearchStatus.TabIndex = 0
        '
        'GrpUser
        '
        Me.GrpUser.Controls.Add(Me.Label4)
        Me.GrpUser.Controls.Add(Me.txtAllUser)
        Me.GrpUser.Controls.Add(Me.btnAddUser)
        Me.GrpUser.Controls.Add(Me.cmbUser)
        Me.GrpUser.Location = New System.Drawing.Point(569, 46)
        Me.GrpUser.Name = "GrpUser"
        Me.GrpUser.Size = New System.Drawing.Size(244, 120)
        Me.GrpUser.TabIndex = 46
        Me.GrpUser.TabStop = False
        Me.GrpUser.Text = " "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "User"
        '
        'txtAllUser
        '
        Me.txtAllUser.Location = New System.Drawing.Point(62, 79)
        Me.txtAllUser.Multiline = True
        Me.txtAllUser.Name = "txtAllUser"
        Me.txtAllUser.Size = New System.Drawing.Size(177, 33)
        Me.txtAllUser.TabIndex = 2
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(97, 48)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(75, 23)
        Me.btnAddUser.TabIndex = 1
        Me.btnAddUser.Text = "Add"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'cmbUser
        '
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(62, 19)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(160, 21)
        Me.cmbUser.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rblOutStanding)
        Me.GroupBox2.Controls.Add(Me.rblInvoice)
        Me.GroupBox2.Controls.Add(Me.rblQuotation)
        Me.GroupBox2.Location = New System.Drawing.Point(325, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(306, 35)
        Me.GroupBox2.TabIndex = 47
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " "
        '
        'rblOutStanding
        '
        Me.rblOutStanding.AutoSize = True
        Me.rblOutStanding.Location = New System.Drawing.Point(200, 12)
        Me.rblOutStanding.Name = "rblOutStanding"
        Me.rblOutStanding.Size = New System.Drawing.Size(87, 17)
        Me.rblOutStanding.TabIndex = 2
        Me.rblOutStanding.Text = "Out Standing"
        Me.rblOutStanding.UseVisualStyleBackColor = True
        '
        'rblInvoice
        '
        Me.rblInvoice.AutoSize = True
        Me.rblInvoice.Location = New System.Drawing.Point(115, 12)
        Me.rblInvoice.Name = "rblInvoice"
        Me.rblInvoice.Size = New System.Drawing.Size(60, 17)
        Me.rblInvoice.TabIndex = 1
        Me.rblInvoice.TabStop = True
        Me.rblInvoice.Text = "Invoice"
        Me.rblInvoice.UseVisualStyleBackColor = True
        '
        'rblQuotation
        '
        Me.rblQuotation.AutoSize = True
        Me.rblQuotation.Checked = True
        Me.rblQuotation.Location = New System.Drawing.Point(27, 12)
        Me.rblQuotation.Name = "rblQuotation"
        Me.rblQuotation.Size = New System.Drawing.Size(71, 17)
        Me.rblQuotation.TabIndex = 0
        Me.rblQuotation.TabStop = True
        Me.rblQuotation.Text = "Quotation"
        Me.rblQuotation.UseVisualStyleBackColor = True
        '
        'Spare_DetailReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(947, 549)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GrpUser)
        Me.Controls.Add(Me.GrpStatus)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.dtEndDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtStartDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Spare_DetailReport"
        Me.Text = "Spare_DetailReport"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GvSpareReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpStatus.ResumeLayout(False)
        Me.GrpStatus.PerformLayout()
        Me.GrpUser.ResumeLayout(False)
        Me.GrpUser.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents GvSpareReport As System.Windows.Forms.DataGridView
    Friend WithEvents GrpStatus As System.Windows.Forms.GroupBox
    Friend WithEvents txtStatusAll As System.Windows.Forms.TextBox
    Friend WithEvents btnAddStatus As System.Windows.Forms.Button
    Friend WithEvents ddlSearchStatus As System.Windows.Forms.ComboBox
    Friend WithEvents GrpUser As System.Windows.Forms.GroupBox
    Friend WithEvents txtAllUser As System.Windows.Forms.TextBox
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rblOutStanding As System.Windows.Forms.RadioButton
    Friend WithEvents rblInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents rblQuotation As System.Windows.Forms.RadioButton
End Class
