<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderReportISIProcess
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtUserMul = New System.Windows.Forms.TextBox()
        Me.btnUserAdd = New System.Windows.Forms.Button()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnExportToexcel = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblTotalCount = New System.Windows.Forms.Label()
        Me.GvISIProcessList = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtstartDt = New System.Windows.Forms.DateTimePicker()
        Me.RblSendDate = New System.Windows.Forms.RadioButton()
        Me.rblReceiveDate = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.GvISIProcessList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.btnExportToexcel)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(32, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(989, 510)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtUserMul)
        Me.GroupBox3.Controls.Add(Me.btnUserAdd)
        Me.GroupBox3.Controls.Add(Me.txtUserName)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(290, 23)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(156, 123)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        '
        'txtUserMul
        '
        Me.txtUserMul.Location = New System.Drawing.Point(6, 79)
        Me.txtUserMul.Multiline = True
        Me.txtUserMul.Name = "txtUserMul"
        Me.txtUserMul.Size = New System.Drawing.Size(140, 38)
        Me.txtUserMul.TabIndex = 3
        '
        'btnUserAdd
        '
        Me.btnUserAdd.Location = New System.Drawing.Point(53, 53)
        Me.btnUserAdd.Name = "btnUserAdd"
        Me.btnUserAdd.Size = New System.Drawing.Size(51, 23)
        Me.btnUserAdd.TabIndex = 2
        Me.btnUserAdd.Text = "Add"
        Me.btnUserAdd.UseVisualStyleBackColor = True
        '
        'txtUserName
        '
        Me.txtUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtUserName.Location = New System.Drawing.Point(10, 31)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(136, 20)
        Me.txtUserName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(51, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Vendor"
        '
        'btnExportToexcel
        '
        Me.btnExportToexcel.Location = New System.Drawing.Point(782, 120)
        Me.btnExportToexcel.Name = "btnExportToexcel"
        Me.btnExportToexcel.Size = New System.Drawing.Size(130, 26)
        Me.btnExportToexcel.TabIndex = 14
        Me.btnExportToexcel.Text = "Export To Excel"
        Me.btnExportToexcel.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblTotalCount)
        Me.GroupBox4.Controls.Add(Me.GvISIProcessList)
        Me.GroupBox4.Location = New System.Drawing.Point(10, 149)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(948, 355)
        Me.GroupBox4.TabIndex = 13
        Me.GroupBox4.TabStop = False
        '
        'lblTotalCount
        '
        Me.lblTotalCount.AutoSize = True
        Me.lblTotalCount.ForeColor = System.Drawing.Color.Maroon
        Me.lblTotalCount.Location = New System.Drawing.Point(6, 20)
        Me.lblTotalCount.Name = "lblTotalCount"
        Me.lblTotalCount.Size = New System.Drawing.Size(39, 13)
        Me.lblTotalCount.TabIndex = 1
        Me.lblTotalCount.Text = "Label4"
        '
        'GvISIProcessList
        '
        Me.GvISIProcessList.AllowDrop = True
        Me.GvISIProcessList.AllowUserToAddRows = False
        Me.GvISIProcessList.AllowUserToDeleteRows = False
        Me.GvISIProcessList.AllowUserToOrderColumns = True
        Me.GvISIProcessList.AllowUserToResizeColumns = False
        Me.GvISIProcessList.AllowUserToResizeRows = False
        Me.GvISIProcessList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvISIProcessList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvISIProcessList.Location = New System.Drawing.Point(6, 43)
        Me.GvISIProcessList.Name = "GvISIProcessList"
        Me.GvISIProcessList.Size = New System.Drawing.Size(902, 306)
        Me.GvISIProcessList.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(598, 57)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(116, 38)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rblReceiveDate)
        Me.GroupBox2.Controls.Add(Me.RblSendDate)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtEndDate)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtstartDt)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(280, 126)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(11, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 13)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Date"
        '
        'txtEndDate
        '
        Me.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtEndDate.Location = New System.Drawing.Point(137, 81)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(81, 20)
        Me.txtEndDate.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "P.Doc.F.Dt"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "P.Doc.F.To Dt"
        '
        'txtstartDt
        '
        Me.txtstartDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtstartDt.Location = New System.Drawing.Point(138, 44)
        Me.txtstartDt.Name = "txtstartDt"
        Me.txtstartDt.Size = New System.Drawing.Size(81, 20)
        Me.txtstartDt.TabIndex = 6
        '
        'RblSendDate
        '
        Me.RblSendDate.AutoSize = True
        Me.RblSendDate.Location = New System.Drawing.Point(67, 15)
        Me.RblSendDate.Name = "RblSendDate"
        Me.RblSendDate.Size = New System.Drawing.Size(76, 17)
        Me.RblSendDate.TabIndex = 10
        Me.RblSendDate.TabStop = True
        Me.RblSendDate.Text = "Send Date"
        Me.RblSendDate.UseVisualStyleBackColor = True
        '
        'rblReceiveDate
        '
        Me.rblReceiveDate.AutoSize = True
        Me.rblReceiveDate.Location = New System.Drawing.Point(162, 15)
        Me.rblReceiveDate.Name = "rblReceiveDate"
        Me.rblReceiveDate.Size = New System.Drawing.Size(91, 17)
        Me.rblReceiveDate.TabIndex = 11
        Me.rblReceiveDate.TabStop = True
        Me.rblReceiveDate.Text = "Receive Date"
        Me.rblReceiveDate.UseVisualStyleBackColor = True
        '
        'OrderReportISIProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 530)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "OrderReportISIProcess"
        Me.Text = "OrderReportISIProcess"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.GvISIProcessList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExportToexcel As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalCount As System.Windows.Forms.Label
    Friend WithEvents GvISIProcessList As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtstartDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtUserMul As System.Windows.Forms.TextBox
    Friend WithEvents btnUserAdd As System.Windows.Forms.Button
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RblSendDate As System.Windows.Forms.RadioButton
    Friend WithEvents rblReceiveDate As System.Windows.Forms.RadioButton
End Class
