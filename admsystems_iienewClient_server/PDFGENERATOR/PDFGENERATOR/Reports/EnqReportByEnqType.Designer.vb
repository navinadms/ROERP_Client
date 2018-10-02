<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EnqReportByEnqType
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblTotalCount = New System.Windows.Forms.Label()
        Me.btnExportToExcel = New System.Windows.Forms.Button()
        Me.GvEnqTypeList = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtstartDt = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtNewStatus = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNewStatusAll = New System.Windows.Forms.TextBox()
        Me.btnAddEnqType = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GvEnqTypeList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblTotalCount)
        Me.GroupBox2.Controls.Add(Me.btnExportToExcel)
        Me.GroupBox2.Controls.Add(Me.GvEnqTypeList)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(972, 495)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'lblTotalCount
        '
        Me.lblTotalCount.AutoSize = True
        Me.lblTotalCount.ForeColor = System.Drawing.Color.Maroon
        Me.lblTotalCount.Location = New System.Drawing.Point(13, 146)
        Me.lblTotalCount.Name = "lblTotalCount"
        Me.lblTotalCount.Size = New System.Drawing.Size(39, 13)
        Me.lblTotalCount.TabIndex = 19
        Me.lblTotalCount.Text = "Label4"
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(793, 132)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(130, 28)
        Me.btnExportToExcel.TabIndex = 18
        Me.btnExportToExcel.Text = "Export To Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'GvEnqTypeList
        '
        Me.GvEnqTypeList.AllowDrop = True
        Me.GvEnqTypeList.AllowUserToAddRows = False
        Me.GvEnqTypeList.AllowUserToDeleteRows = False
        Me.GvEnqTypeList.AllowUserToOrderColumns = True
        Me.GvEnqTypeList.AllowUserToResizeColumns = False
        Me.GvEnqTypeList.AllowUserToResizeRows = False
        Me.GvEnqTypeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvEnqTypeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvEnqTypeList.Location = New System.Drawing.Point(13, 180)
        Me.GvEnqTypeList.Name = "GvEnqTypeList"
        Me.GvEnqTypeList.Size = New System.Drawing.Size(926, 297)
        Me.GvEnqTypeList.TabIndex = 16
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(494, 72)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(91, 42)
        Me.btnSearch.TabIndex = 15
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtEndDate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtstartDt)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(176, 126)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        '
        'txtEndDate
        '
        Me.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtEndDate.Location = New System.Drawing.Point(79, 77)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(85, 20)
        Me.txtEndDate.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Start Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "End Date"
        '
        'txtstartDt
        '
        Me.txtstartDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtstartDt.Location = New System.Drawing.Point(76, 34)
        Me.txtstartDt.Name = "txtstartDt"
        Me.txtstartDt.Size = New System.Drawing.Size(88, 20)
        Me.txtstartDt.TabIndex = 6
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtNewStatusAll)
        Me.GroupBox3.Controls.Add(Me.btnAddEnqType)
        Me.GroupBox3.Controls.Add(Me.txtNewStatus)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Location = New System.Drawing.Point(198, 11)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(261, 149)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        '
        'txtNewStatus
        '
        Me.txtNewStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtNewStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtNewStatus.Location = New System.Drawing.Point(59, 31)
        Me.txtNewStatus.Name = "txtNewStatus"
        Me.txtNewStatus.Size = New System.Drawing.Size(120, 20)
        Me.txtNewStatus.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(103, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "New"
        '
        'txtNewStatusAll
        '
        Me.txtNewStatusAll.Location = New System.Drawing.Point(38, 87)
        Me.txtNewStatusAll.Multiline = True
        Me.txtNewStatusAll.Name = "txtNewStatusAll"
        Me.txtNewStatusAll.Size = New System.Drawing.Size(162, 34)
        Me.txtNewStatusAll.TabIndex = 37
        '
        'btnAddEnqType
        '
        Me.btnAddEnqType.Location = New System.Drawing.Point(98, 56)
        Me.btnAddEnqType.Name = "btnAddEnqType"
        Me.btnAddEnqType.Size = New System.Drawing.Size(37, 23)
        Me.btnAddEnqType.TabIndex = 36
        Me.btnAddEnqType.Text = "Add"
        Me.btnAddEnqType.UseVisualStyleBackColor = True
        '
        'EnqReportByEnqType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 497)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "EnqReportByEnqType"
        Me.Text = "EnqReportByEnqType"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.GvEnqTypeList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtstartDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GvEnqTypeList As System.Windows.Forms.DataGridView
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents lblTotalCount As System.Windows.Forms.Label
    Friend WithEvents txtNewStatusAll As System.Windows.Forms.TextBox
    Friend WithEvents btnAddEnqType As System.Windows.Forms.Button
End Class
