<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderManagerHotListReport
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
        Me.btnOk = New System.Windows.Forms.Button()
        Me.lstOrderStatus = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnExportToExcel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GvHotListReport = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GvHotListReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnOk)
        Me.GroupBox1.Controls.Add(Me.lstOrderStatus)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnExportToExcel)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.GvHotListReport)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(865, 419)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(302, 16)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 71
        Me.btnOk.Text = "Search"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'lstOrderStatus
        '
        Me.lstOrderStatus.FormattingEnabled = True
        Me.lstOrderStatus.Items.AddRange(New Object() {"ISI HOT", "ISI CONFIRM", "NON ISI HOT", "NON ISI CONFIRM"})
        Me.lstOrderStatus.Location = New System.Drawing.Point(163, 15)
        Me.lstOrderStatus.Name = "lstOrderStatus"
        Me.lstOrderStatus.Size = New System.Drawing.Size(120, 64)
        Me.lstOrderStatus.TabIndex = 70
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(110, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 17)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = "O.Status"
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(718, 22)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(121, 23)
        Me.btnExportToExcel.TabIndex = 2
        Me.btnExportToExcel.Text = "Export To Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Hot List"
        '
        'GvHotListReport
        '
        Me.GvHotListReport.AllowDrop = True
        Me.GvHotListReport.AllowUserToAddRows = False
        Me.GvHotListReport.AllowUserToDeleteRows = False
        Me.GvHotListReport.AllowUserToOrderColumns = True
        Me.GvHotListReport.AllowUserToResizeColumns = False
        Me.GvHotListReport.AllowUserToResizeRows = False
        Me.GvHotListReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvHotListReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvHotListReport.Location = New System.Drawing.Point(6, 114)
        Me.GvHotListReport.Name = "GvHotListReport"
        Me.GvHotListReport.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.GvHotListReport.Size = New System.Drawing.Size(853, 299)
        Me.GvHotListReport.TabIndex = 0
        '
        'OrderManagerHotListReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(889, 443)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "OrderManagerHotListReport"
        Me.Text = "OrderManagerHotListReport"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GvHotListReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GvHotListReport As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents lstOrderStatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
