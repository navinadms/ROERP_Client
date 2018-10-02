<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServiceECStatusReport
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.ddlECStatus = New System.Windows.Forms.ComboBox()
        Me.GvECStatus = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.txtECall = New System.Windows.Forms.TextBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtCancel = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.chkIsDate = New System.Windows.Forms.CheckBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.dtToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtFromDate = New System.Windows.Forms.DateTimePicker()
        CType(Me.GvECStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(568, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "EC Status"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(554, 124)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 32)
        Me.btnSearch.TabIndex = 16
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'ddlECStatus
        '
        Me.ddlECStatus.FormattingEnabled = True
        Me.ddlECStatus.Items.AddRange(New Object() {"Done", "Pending By IIE", "Pending By Client", "Running", "Process"})
        Me.ddlECStatus.Location = New System.Drawing.Point(533, 24)
        Me.ddlECStatus.Name = "ddlECStatus"
        Me.ddlECStatus.Size = New System.Drawing.Size(194, 21)
        Me.ddlECStatus.TabIndex = 15
        '
        'GvECStatus
        '
        Me.GvECStatus.AllowDrop = True
        Me.GvECStatus.AllowUserToAddRows = False
        Me.GvECStatus.AllowUserToDeleteRows = False
        Me.GvECStatus.AllowUserToOrderColumns = True
        Me.GvECStatus.AllowUserToResizeColumns = False
        Me.GvECStatus.AllowUserToResizeRows = False
        Me.GvECStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvECStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvECStatus.Location = New System.Drawing.Point(12, 162)
        Me.GvECStatus.Name = "GvECStatus"
        Me.GvECStatus.Size = New System.Drawing.Size(1141, 397)
        Me.GvECStatus.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(310, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "User"
        '
        'cmbUser
        '
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(376, 21)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(133, 21)
        Me.cmbUser.TabIndex = 18
        '
        'txtECall
        '
        Me.txtECall.Location = New System.Drawing.Point(533, 72)
        Me.txtECall.Multiline = True
        Me.txtECall.Name = "txtECall"
        Me.txtECall.Size = New System.Drawing.Size(194, 41)
        Me.txtECall.TabIndex = 20
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(593, 47)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(58, 23)
        Me.btnAdd.TabIndex = 21
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtCancel
        '
        Me.txtCancel.Location = New System.Drawing.Point(643, 124)
        Me.txtCancel.Name = "txtCancel"
        Me.txtCancel.Size = New System.Drawing.Size(75, 32)
        Me.txtCancel.TabIndex = 22
        Me.txtCancel.Text = "Cancel"
        Me.txtCancel.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(1030, 124)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(97, 32)
        Me.btnExport.TabIndex = 23
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'chkIsDate
        '
        Me.chkIsDate.AutoSize = True
        Me.chkIsDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsDate.Location = New System.Drawing.Point(376, 53)
        Me.chkIsDate.Name = "chkIsDate"
        Me.chkIsDate.Size = New System.Drawing.Size(67, 17)
        Me.chkIsDate.TabIndex = 81
        Me.chkIsDate.Text = "Is Date"
        Me.chkIsDate.UseVisualStyleBackColor = True
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(324, 113)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(20, 13)
        Me.Label34.TabIndex = 80
        Me.Label34.Text = "To"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(323, 79)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(30, 13)
        Me.Label33.TabIndex = 79
        Me.Label33.Text = "From"
        '
        'dtToDate
        '
        Me.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtToDate.Location = New System.Drawing.Point(368, 111)
        Me.dtToDate.Name = "dtToDate"
        Me.dtToDate.Size = New System.Drawing.Size(127, 20)
        Me.dtToDate.TabIndex = 78
        '
        'dtFromDate
        '
        Me.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFromDate.Location = New System.Drawing.Point(368, 76)
        Me.dtFromDate.Name = "dtFromDate"
        Me.dtFromDate.Size = New System.Drawing.Size(127, 20)
        Me.dtFromDate.TabIndex = 77
        '
        'ServiceECStatusReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1165, 571)
        Me.Controls.Add(Me.chkIsDate)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.dtToDate)
        Me.Controls.Add(Me.dtFromDate)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.txtCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtECall)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbUser)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.ddlECStatus)
        Me.Controls.Add(Me.GvECStatus)
        Me.Name = "ServiceECStatusReport"
        Me.Text = "ServiceECStatusReport"
        CType(Me.GvECStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents ddlECStatus As System.Windows.Forms.ComboBox
    Friend WithEvents GvECStatus As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents txtECall As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtCancel As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents chkIsDate As System.Windows.Forms.CheckBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents dtToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFromDate As System.Windows.Forms.DateTimePicker
End Class
