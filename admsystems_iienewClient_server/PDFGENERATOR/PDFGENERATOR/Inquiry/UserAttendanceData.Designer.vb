<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserAttendanceData
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
        Me.btnImportAttendance = New System.Windows.Forms.Button()
        Me.Search = New System.Windows.Forms.Button()
        Me.GvAttendaceData = New System.Windows.Forms.DataGridView()
        Me.txtExcelPath = New System.Windows.Forms.TextBox()
        Me.btnBrowseAttandace = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GvAttendaceData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnImportAttendance)
        Me.GroupBox1.Controls.Add(Me.Search)
        Me.GroupBox1.Controls.Add(Me.GvAttendaceData)
        Me.GroupBox1.Controls.Add(Me.txtExcelPath)
        Me.GroupBox1.Controls.Add(Me.btnBrowseAttandace)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(761, 532)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " "
        '
        'btnImportAttendance
        '
        Me.btnImportAttendance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportAttendance.Location = New System.Drawing.Point(302, 475)
        Me.btnImportAttendance.Name = "btnImportAttendance"
        Me.btnImportAttendance.Size = New System.Drawing.Size(154, 42)
        Me.btnImportAttendance.TabIndex = 4
        Me.btnImportAttendance.Text = "Import Attendance"
        Me.btnImportAttendance.UseVisualStyleBackColor = True
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
        'GvAttendaceData
        '
        Me.GvAttendaceData.AllowDrop = True
        Me.GvAttendaceData.AllowUserToAddRows = False
        Me.GvAttendaceData.AllowUserToDeleteRows = False
        Me.GvAttendaceData.AllowUserToOrderColumns = True
        Me.GvAttendaceData.AllowUserToResizeColumns = False
        Me.GvAttendaceData.AllowUserToResizeRows = False
        Me.GvAttendaceData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvAttendaceData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvAttendaceData.Location = New System.Drawing.Point(20, 94)
        Me.GvAttendaceData.Name = "GvAttendaceData"
        Me.GvAttendaceData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvAttendaceData.Size = New System.Drawing.Size(723, 365)
        Me.GvAttendaceData.TabIndex = 2
        '
        'txtExcelPath
        '
        Me.txtExcelPath.Location = New System.Drawing.Point(131, 23)
        Me.txtExcelPath.Name = "txtExcelPath"
        Me.txtExcelPath.Size = New System.Drawing.Size(375, 20)
        Me.txtExcelPath.TabIndex = 1
        '
        'btnBrowseAttandace
        '
        Me.btnBrowseAttandace.Location = New System.Drawing.Point(522, 19)
        Me.btnBrowseAttandace.Name = "btnBrowseAttandace"
        Me.btnBrowseAttandace.Size = New System.Drawing.Size(75, 31)
        Me.btnBrowseAttandace.TabIndex = 0
        Me.btnBrowseAttandace.Text = "Browse"
        Me.btnBrowseAttandace.UseVisualStyleBackColor = True
        '
        'UserAttendanceData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(785, 556)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "UserAttendanceData"
        Me.Text = "User Attendance Import Data"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GvAttendaceData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowseAttandace As System.Windows.Forms.Button
    Friend WithEvents txtExcelPath As System.Windows.Forms.TextBox
    Friend WithEvents GvAttendaceData As System.Windows.Forms.DataGridView
    Friend WithEvents Search As System.Windows.Forms.Button
    Friend WithEvents btnImportAttendance As System.Windows.Forms.Button
End Class
