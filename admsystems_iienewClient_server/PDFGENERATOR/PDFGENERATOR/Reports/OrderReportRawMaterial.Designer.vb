<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderReportRawMaterial
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtToDate = New System.Windows.Forms.TextBox()
        Me.TxtFromDate = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rblTentiveDate = New System.Windows.Forms.RadioButton()
        Me.rblDispDate = New System.Windows.Forms.RadioButton()
        Me.rblOrderDate = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GvRawMaterial = New System.Windows.Forms.DataGridView()
        Me.lblTotalCount = New System.Windows.Forms.Label()
        Me.btnExportToExcel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GvRawMaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TxtToDate)
        Me.GroupBox1.Controls.Add(Me.TxtFromDate)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(758, 97)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(180, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "To Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 15)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "From Date"
        '
        'TxtToDate
        '
        Me.TxtToDate.Location = New System.Drawing.Point(243, 60)
        Me.TxtToDate.Name = "TxtToDate"
        Me.TxtToDate.Size = New System.Drawing.Size(95, 20)
        Me.TxtToDate.TabIndex = 4
        '
        'TxtFromDate
        '
        Me.TxtFromDate.Location = New System.Drawing.Point(95, 61)
        Me.TxtFromDate.Name = "TxtFromDate"
        Me.TxtFromDate.Size = New System.Drawing.Size(81, 20)
        Me.TxtFromDate.TabIndex = 3
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(412, 56)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(84, 35)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Status"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rblTentiveDate)
        Me.GroupBox3.Controls.Add(Me.rblDispDate)
        Me.GroupBox3.Controls.Add(Me.rblOrderDate)
        Me.GroupBox3.Location = New System.Drawing.Point(93, 9)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(287, 46)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'rblTentiveDate
        '
        Me.rblTentiveDate.AutoSize = True
        Me.rblTentiveDate.Location = New System.Drawing.Point(195, 16)
        Me.rblTentiveDate.Name = "rblTentiveDate"
        Me.rblTentiveDate.Size = New System.Drawing.Size(87, 17)
        Me.rblTentiveDate.TabIndex = 2
        Me.rblTentiveDate.TabStop = True
        Me.rblTentiveDate.Text = "Tentive Date"
        Me.rblTentiveDate.UseVisualStyleBackColor = True
        '
        'rblDispDate
        '
        Me.rblDispDate.AutoSize = True
        Me.rblDispDate.Location = New System.Drawing.Point(93, 16)
        Me.rblDispDate.Name = "rblDispDate"
        Me.rblDispDate.Size = New System.Drawing.Size(96, 17)
        Me.rblDispDate.TabIndex = 1
        Me.rblDispDate.TabStop = True
        Me.rblDispDate.Text = " Dispatch Date"
        Me.rblDispDate.UseVisualStyleBackColor = True
        '
        'rblOrderDate
        '
        Me.rblOrderDate.AutoSize = True
        Me.rblOrderDate.Location = New System.Drawing.Point(14, 16)
        Me.rblOrderDate.Name = "rblOrderDate"
        Me.rblOrderDate.Size = New System.Drawing.Size(77, 17)
        Me.rblOrderDate.TabIndex = 0
        Me.rblOrderDate.TabStop = True
        Me.rblOrderDate.Text = "Order Date"
        Me.rblOrderDate.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GvRawMaterial)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 125)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(957, 344)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'GvRawMaterial
        '
        Me.GvRawMaterial.AllowDrop = True
        Me.GvRawMaterial.AllowUserToAddRows = False
        Me.GvRawMaterial.AllowUserToDeleteRows = False
        Me.GvRawMaterial.AllowUserToOrderColumns = True
        Me.GvRawMaterial.AllowUserToResizeColumns = False
        Me.GvRawMaterial.AllowUserToResizeRows = False
        Me.GvRawMaterial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvRawMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvRawMaterial.Location = New System.Drawing.Point(6, 19)
        Me.GvRawMaterial.Name = "GvRawMaterial"
        Me.GvRawMaterial.Size = New System.Drawing.Size(945, 318)
        Me.GvRawMaterial.TabIndex = 0
        '
        'lblTotalCount
        '
        Me.lblTotalCount.AutoSize = True
        Me.lblTotalCount.ForeColor = System.Drawing.Color.Maroon
        Me.lblTotalCount.Location = New System.Drawing.Point(19, 115)
        Me.lblTotalCount.Name = "lblTotalCount"
        Me.lblTotalCount.Size = New System.Drawing.Size(39, 13)
        Me.lblTotalCount.TabIndex = 2
        Me.lblTotalCount.Text = "Label2"
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(833, 91)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(130, 28)
        Me.btnExportToExcel.TabIndex = 18
        Me.btnExportToExcel.Text = "Export To Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'OrderReportRawMaterial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(981, 494)
        Me.Controls.Add(Me.btnExportToExcel)
        Me.Controls.Add(Me.lblTotalCount)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "OrderReportRawMaterial"
        Me.Text = "OrderReportRawMaterial"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.GvRawMaterial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rblDispDate As System.Windows.Forms.RadioButton
    Friend WithEvents rblOrderDate As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GvRawMaterial As System.Windows.Forms.DataGridView
    Friend WithEvents lblTotalCount As System.Windows.Forms.Label
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtToDate As System.Windows.Forms.TextBox
    Friend WithEvents TxtFromDate As System.Windows.Forms.TextBox
    Friend WithEvents rblTentiveDate As System.Windows.Forms.RadioButton
End Class
