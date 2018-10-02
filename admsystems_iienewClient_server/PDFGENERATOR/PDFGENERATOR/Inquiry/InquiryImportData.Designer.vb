<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InquiryImportData
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
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtExcelPath = New System.Windows.Forms.TextBox()
        Me.GvInquiryData = New System.Windows.Forms.DataGridView()
        Me.Search = New System.Windows.Forms.Button()
        Me.btnImportInquiry = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GvInquiryData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnImportInquiry)
        Me.GroupBox1.Controls.Add(Me.Search)
        Me.GroupBox1.Controls.Add(Me.GvInquiryData)
        Me.GroupBox1.Controls.Add(Me.txtExcelPath)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(761, 532)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " "
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(522, 19)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 31)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtExcelPath
        '
        Me.txtExcelPath.Location = New System.Drawing.Point(131, 23)
        Me.txtExcelPath.Name = "txtExcelPath"
        Me.txtExcelPath.Size = New System.Drawing.Size(375, 20)
        Me.txtExcelPath.TabIndex = 1
        '
        'GvInquiryData
        '
        Me.GvInquiryData.AllowDrop = True
        Me.GvInquiryData.AllowUserToAddRows = False
        Me.GvInquiryData.AllowUserToDeleteRows = False
        Me.GvInquiryData.AllowUserToOrderColumns = True
        Me.GvInquiryData.AllowUserToResizeColumns = False
        Me.GvInquiryData.AllowUserToResizeRows = False
        Me.GvInquiryData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvInquiryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvInquiryData.Location = New System.Drawing.Point(20, 94)
        Me.GvInquiryData.Name = "GvInquiryData"
        Me.GvInquiryData.Size = New System.Drawing.Size(723, 365)
        Me.GvInquiryData.TabIndex = 2
        '
        'Search
        '
        Me.Search.Location = New System.Drawing.Point(271, 60)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(91, 28)
        Me.Search.TabIndex = 3
        Me.Search.Text = "Search"
        Me.Search.UseVisualStyleBackColor = True
        '
        'btnImportInquiry
        '
        Me.btnImportInquiry.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportInquiry.Location = New System.Drawing.Point(302, 475)
        Me.btnImportInquiry.Name = "btnImportInquiry"
        Me.btnImportInquiry.Size = New System.Drawing.Size(154, 42)
        Me.btnImportInquiry.TabIndex = 4
        Me.btnImportInquiry.Text = "Import Inquiry"
        Me.btnImportInquiry.UseVisualStyleBackColor = True
        '
        'InquiryImportData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(785, 556)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "InquiryImportData"
        Me.Text = "Inquiry Import Data"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GvInquiryData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtExcelPath As System.Windows.Forms.TextBox
    Friend WithEvents GvInquiryData As System.Windows.Forms.DataGridView
    Friend WithEvents Search As System.Windows.Forms.Button
    Friend WithEvents btnImportInquiry As System.Windows.Forms.Button
End Class
