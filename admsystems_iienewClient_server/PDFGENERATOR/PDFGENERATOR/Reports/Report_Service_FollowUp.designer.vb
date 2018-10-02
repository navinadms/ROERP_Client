<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Report_Service_FollowUp
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
        Me.cmbFollow = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grpUser = New System.Windows.Forms.GroupBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtAllUser = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnExportPDF = New System.Windows.Forms.Button()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dgServiceFollowDetail = New System.Windows.Forms.DataGridView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgFollowCounter = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.grpUser.SuspendLayout()
        CType(Me.dgServiceFollowDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgFollowCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbFollow
        '
        Me.cmbFollow.FormattingEnabled = True
        Me.cmbFollow.Items.AddRange(New Object() {"Daily FollowUp", "Today Call"})
        Me.cmbFollow.Location = New System.Drawing.Point(407, 17)
        Me.cmbFollow.Name = "cmbFollow"
        Me.cmbFollow.Size = New System.Drawing.Size(195, 21)
        Me.cmbFollow.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From :"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grpUser)
        Me.Panel1.Controls.Add(Me.dtEndDate)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.btnExportPDF)
        Me.Panel1.Controls.Add(Me.btnExportExcel)
        Me.Panel1.Controls.Add(Me.btnPrint)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.cmbFollow)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.dtStartDate)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1099, 103)
        Me.Panel1.TabIndex = 4
        '
        'grpUser
        '
        Me.grpUser.Controls.Add(Me.btnAdd)
        Me.grpUser.Controls.Add(Me.txtAllUser)
        Me.grpUser.Controls.Add(Me.txtUser)
        Me.grpUser.Controls.Add(Me.Label3)
        Me.grpUser.Location = New System.Drawing.Point(19, 44)
        Me.grpUser.Name = "grpUser"
        Me.grpUser.Size = New System.Drawing.Size(442, 52)
        Me.grpUser.TabIndex = 17
        Me.grpUser.TabStop = False
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(198, 22)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(56, 23)
        Me.btnAdd.TabIndex = 15
        Me.btnAdd.Text = "ADD"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtAllUser
        '
        Me.txtAllUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtAllUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtAllUser.Location = New System.Drawing.Point(260, 10)
        Me.txtAllUser.Multiline = True
        Me.txtAllUser.Name = "txtAllUser"
        Me.txtAllUser.Size = New System.Drawing.Size(159, 36)
        Me.txtAllUser.TabIndex = 16
        '
        'txtUser
        '
        Me.txtUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtUser.Location = New System.Drawing.Point(94, 25)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(98, 20)
        Me.txtUser.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "By Whom :"
        '
        'dtEndDate
        '
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEndDate.Location = New System.Drawing.Point(199, 18)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(93, 20)
        Me.dtEndDate.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(163, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "To :"
        '
        'btnExportPDF
        '
        Me.btnExportPDF.Location = New System.Drawing.Point(805, 67)
        Me.btnExportPDF.Name = "btnExportPDF"
        Me.btnExportPDF.Size = New System.Drawing.Size(75, 23)
        Me.btnExportPDF.TabIndex = 13
        Me.btnExportPDF.Text = "Export PDF"
        Me.btnExportPDF.UseVisualStyleBackColor = True
        Me.btnExportPDF.Visible = False
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Location = New System.Drawing.Point(630, 67)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(75, 23)
        Me.btnExportExcel.TabIndex = 12
        Me.btnExportExcel.Text = "Export Excel"
        Me.btnExportExcel.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(711, 67)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 23)
        Me.btnPrint.TabIndex = 11
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(630, 13)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(121, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(298, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Select FollowUp "
        '
        'dtStartDate
        '
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDate.Location = New System.Drawing.Point(64, 18)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(93, 20)
        Me.dtStartDate.TabIndex = 0
        '
        'dgServiceFollowDetail
        '
        Me.dgServiceFollowDetail.AllowUserToOrderColumns = True
        Me.dgServiceFollowDetail.AllowUserToResizeColumns = False
        Me.dgServiceFollowDetail.AllowUserToResizeRows = False
        Me.dgServiceFollowDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgServiceFollowDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgServiceFollowDetail.Location = New System.Drawing.Point(12, 133)
        Me.dgServiceFollowDetail.Name = "dgServiceFollowDetail"
        Me.dgServiceFollowDetail.Size = New System.Drawing.Size(1099, 330)
        Me.dgServiceFollowDetail.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(28, 466)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Counter"
        '
        'dgFollowCounter
        '
        Me.dgFollowCounter.AllowDrop = True
        Me.dgFollowCounter.AllowUserToAddRows = False
        Me.dgFollowCounter.AllowUserToDeleteRows = False
        Me.dgFollowCounter.AllowUserToOrderColumns = True
        Me.dgFollowCounter.AllowUserToResizeColumns = False
        Me.dgFollowCounter.AllowUserToResizeRows = False
        Me.dgFollowCounter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgFollowCounter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgFollowCounter.Location = New System.Drawing.Point(85, 469)
        Me.dgFollowCounter.Name = "dgFollowCounter"
        Me.dgFollowCounter.Size = New System.Drawing.Size(240, 147)
        Me.dgFollowCounter.TabIndex = 7
        '
        'Report_Service_FollowUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1121, 619)
        Me.Controls.Add(Me.dgFollowCounter)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dgServiceFollowDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Report_Service_FollowUp"
        Me.Text = "Report_FollowUp"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpUser.ResumeLayout(False)
        Me.grpUser.PerformLayout()
        CType(Me.dgServiceFollowDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgFollowCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbFollow As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgServiceFollowDetail As System.Windows.Forms.DataGridView
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExportPDF As System.Windows.Forms.Button
    Friend WithEvents btnExportExcel As System.Windows.Forms.Button
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dgFollowCounter As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtAllUser As System.Windows.Forms.TextBox
    Friend WithEvents grpUser As System.Windows.Forms.GroupBox
End Class
