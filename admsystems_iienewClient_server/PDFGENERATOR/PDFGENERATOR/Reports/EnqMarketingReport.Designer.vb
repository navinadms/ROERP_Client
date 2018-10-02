<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EnqMarketingReport
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
        Me.GvEnqMarkRp = New System.Windows.Forms.DataGridView()
        Me.ddlMarketing = New System.Windows.Forms.ComboBox()
        Me.GvEnqMarketingRp = New System.Windows.Forms.DataGridView()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rblAll = New System.Windows.Forms.RadioButton()
        Me.rblSearchCancel = New System.Windows.Forms.RadioButton()
        Me.rblSearchDone = New System.Windows.Forms.RadioButton()
        Me.rblSearchPending = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblTotalPrice = New System.Windows.Forms.Label()
        CType(Me.GvEnqMarkRp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvEnqMarketingRp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GvEnqMarkRp
        '
        Me.GvEnqMarkRp.AllowDrop = True
        Me.GvEnqMarkRp.AllowUserToAddRows = False
        Me.GvEnqMarkRp.AllowUserToDeleteRows = False
        Me.GvEnqMarkRp.AllowUserToOrderColumns = True
        Me.GvEnqMarkRp.AllowUserToResizeColumns = False
        Me.GvEnqMarkRp.AllowUserToResizeRows = False
        Me.GvEnqMarkRp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvEnqMarkRp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvEnqMarkRp.Location = New System.Drawing.Point(31, 158)
        Me.GvEnqMarkRp.Name = "GvEnqMarkRp"
        Me.GvEnqMarkRp.Size = New System.Drawing.Size(1002, 359)
        Me.GvEnqMarkRp.TabIndex = 1
        '
        'ddlMarketing
        '
        Me.ddlMarketing.FormattingEnabled = True
        Me.ddlMarketing.Location = New System.Drawing.Point(331, 64)
        Me.ddlMarketing.Name = "ddlMarketing"
        Me.ddlMarketing.Size = New System.Drawing.Size(217, 21)
        Me.ddlMarketing.TabIndex = 6
        '
        'GvEnqMarketingRp
        '
        Me.GvEnqMarketingRp.AllowDrop = True
        Me.GvEnqMarketingRp.AllowUserToAddRows = False
        Me.GvEnqMarketingRp.AllowUserToDeleteRows = False
        Me.GvEnqMarketingRp.AllowUserToOrderColumns = True
        Me.GvEnqMarketingRp.AllowUserToResizeColumns = False
        Me.GvEnqMarketingRp.AllowUserToResizeRows = False
        Me.GvEnqMarketingRp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvEnqMarketingRp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvEnqMarketingRp.Location = New System.Drawing.Point(31, 193)
        Me.GvEnqMarketingRp.Name = "GvEnqMarketingRp"
        Me.GvEnqMarketingRp.Size = New System.Drawing.Size(1002, 324)
        Me.GvEnqMarketingRp.TabIndex = 1
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(914, 129)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(75, 23)
        Me.btnExport.TabIndex = 8
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(593, 64)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 43)
        Me.btnSearch.TabIndex = 9
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(265, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(265, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "User"
        '
        'cmbUser
        '
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(331, 26)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(217, 21)
        Me.cmbUser.TabIndex = 11
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rblAll)
        Me.GroupBox1.Controls.Add(Me.rblSearchCancel)
        Me.GroupBox1.Controls.Add(Me.rblSearchDone)
        Me.GroupBox1.Controls.Add(Me.rblSearchPending)
        Me.GroupBox1.Location = New System.Drawing.Point(268, 98)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 38)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'rblAll
        '
        Me.rblAll.AutoSize = True
        Me.rblAll.Checked = True
        Me.rblAll.Location = New System.Drawing.Point(209, 12)
        Me.rblAll.Name = "rblAll"
        Me.rblAll.Size = New System.Drawing.Size(36, 17)
        Me.rblAll.TabIndex = 3
        Me.rblAll.TabStop = True
        Me.rblAll.Text = "All"
        Me.rblAll.UseVisualStyleBackColor = True
        '
        'rblSearchCancel
        '
        Me.rblSearchCancel.AutoSize = True
        Me.rblSearchCancel.Location = New System.Drawing.Point(138, 11)
        Me.rblSearchCancel.Name = "rblSearchCancel"
        Me.rblSearchCancel.Size = New System.Drawing.Size(58, 17)
        Me.rblSearchCancel.TabIndex = 2
        Me.rblSearchCancel.TabStop = True
        Me.rblSearchCancel.Text = "Cancel"
        Me.rblSearchCancel.UseVisualStyleBackColor = True
        '
        'rblSearchDone
        '
        Me.rblSearchDone.AutoSize = True
        Me.rblSearchDone.Location = New System.Drawing.Point(80, 12)
        Me.rblSearchDone.Name = "rblSearchDone"
        Me.rblSearchDone.Size = New System.Drawing.Size(51, 17)
        Me.rblSearchDone.TabIndex = 1
        Me.rblSearchDone.TabStop = True
        Me.rblSearchDone.Text = "Done"
        Me.rblSearchDone.UseVisualStyleBackColor = True
        '
        'rblSearchPending
        '
        Me.rblSearchPending.AutoSize = True
        Me.rblSearchPending.Location = New System.Drawing.Point(7, 11)
        Me.rblSearchPending.Name = "rblSearchPending"
        Me.rblSearchPending.Size = New System.Drawing.Size(64, 17)
        Me.rblSearchPending.TabIndex = 0
        Me.rblSearchPending.TabStop = True
        Me.rblSearchPending.Text = "Pending"
        Me.rblSearchPending.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(575, 533)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 15)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Price"
        '
        'lblTotalPrice
        '
        Me.lblTotalPrice.AutoSize = True
        Me.lblTotalPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPrice.Location = New System.Drawing.Point(628, 533)
        Me.lblTotalPrice.Name = "lblTotalPrice"
        Me.lblTotalPrice.Size = New System.Drawing.Size(14, 15)
        Me.lblTotalPrice.TabIndex = 15
        Me.lblTotalPrice.Text = "0"
        '
        'EnqMarketingReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1079, 558)
        Me.Controls.Add(Me.lblTotalPrice)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbUser)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.ddlMarketing)
        Me.Controls.Add(Me.GvEnqMarkRp)
        Me.Name = "EnqMarketingReport"
        Me.Text = "EnqMarketingReport"
        CType(Me.GvEnqMarkRp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvEnqMarketingRp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GvEnqMarkRp As System.Windows.Forms.DataGridView
    Friend WithEvents ddlMarketing As System.Windows.Forms.ComboBox
    Friend WithEvents GvEnqMarketingRp As System.Windows.Forms.DataGridView
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rblSearchPending As System.Windows.Forms.RadioButton
    Friend WithEvents rblSearchDone As System.Windows.Forms.RadioButton
    Friend WithEvents rblSearchCancel As System.Windows.Forms.RadioButton
    Friend WithEvents rblAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTotalPrice As System.Windows.Forms.Label
End Class
