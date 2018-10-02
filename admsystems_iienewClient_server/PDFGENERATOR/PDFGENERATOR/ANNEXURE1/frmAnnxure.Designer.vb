<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAnnxure
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
        Me.GbMain = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnCancel1 = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.txtPlant = New System.Windows.Forms.TextBox()
        Me.txtQtype = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtDesciption = New System.Windows.Forms.TextBox()
        Me.txtSrno = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GbFind = New System.Windows.Forms.GroupBox()
        Me.txtSearchBy = New System.Windows.Forms.TextBox()
        Me.DGMain = New System.Windows.Forms.DataGridView()
        Me.cmbSearchBy = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GbMain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GbFind.SuspendLayout()
        CType(Me.DGMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GbMain
        '
        Me.GbMain.Controls.Add(Me.Panel2)
        Me.GbMain.Controls.Add(Me.txtModel)
        Me.GbMain.Controls.Add(Me.txtPlant)
        Me.GbMain.Controls.Add(Me.txtQtype)
        Me.GbMain.Controls.Add(Me.txtRemarks)
        Me.GbMain.Controls.Add(Me.txtDesciption)
        Me.GbMain.Controls.Add(Me.txtSrno)
        Me.GbMain.Controls.Add(Me.Label6)
        Me.GbMain.Controls.Add(Me.Label5)
        Me.GbMain.Controls.Add(Me.Label4)
        Me.GbMain.Controls.Add(Me.Label3)
        Me.GbMain.Controls.Add(Me.Label2)
        Me.GbMain.Controls.Add(Me.Label1)
        Me.GbMain.Location = New System.Drawing.Point(9, 7)
        Me.GbMain.Name = "GbMain"
        Me.GbMain.Size = New System.Drawing.Size(703, 488)
        Me.GbMain.TabIndex = 13
        Me.GbMain.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnCancel1)
        Me.Panel2.Controls.Add(Me.btnExit)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Location = New System.Drawing.Point(314, 446)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(379, 36)
        Me.Panel2.TabIndex = 6
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(118, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnCancel1
        '
        Me.btnCancel1.Location = New System.Drawing.Point(206, 5)
        Me.btnCancel1.Name = "btnCancel1"
        Me.btnCancel1.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel1.TabIndex = 1
        Me.btnCancel1.Text = "Cancel"
        Me.btnCancel1.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(296, 6)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(28, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtModel
        '
        Me.txtModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtModel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtModel.Location = New System.Drawing.Point(83, 41)
        Me.txtModel.Name = "txtModel"
        Me.txtModel.Size = New System.Drawing.Size(247, 20)
        Me.txtModel.TabIndex = 3
        '
        'txtPlant
        '
        Me.txtPlant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtPlant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPlant.Location = New System.Drawing.Point(400, 16)
        Me.txtPlant.Name = "txtPlant"
        Me.txtPlant.Size = New System.Drawing.Size(288, 20)
        Me.txtPlant.TabIndex = 2
        '
        'txtQtype
        '
        Me.txtQtype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtQtype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtQtype.Location = New System.Drawing.Point(203, 15)
        Me.txtQtype.Name = "txtQtype"
        Me.txtQtype.Size = New System.Drawing.Size(127, 20)
        Me.txtQtype.TabIndex = 1
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(403, 75)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(285, 65)
        Me.txtRemarks.TabIndex = 5
        '
        'txtDesciption
        '
        Me.txtDesciption.Location = New System.Drawing.Point(83, 73)
        Me.txtDesciption.Multiline = True
        Me.txtDesciption.Name = "txtDesciption"
        Me.txtDesciption.Size = New System.Drawing.Size(245, 67)
        Me.txtDesciption.TabIndex = 4
        '
        'txtSrno
        '
        Me.txtSrno.Enabled = False
        Me.txtSrno.Location = New System.Drawing.Point(84, 14)
        Me.txtSrno.Name = "txtSrno"
        Me.txtSrno.Size = New System.Drawing.Size(60, 20)
        Me.txtSrno.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(36, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Model"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(344, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Plant"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(158, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "QType"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(344, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Remarks"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Description"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SrNo"
        '
        'GbFind
        '
        Me.GbFind.Controls.Add(Me.txtSearchBy)
        Me.GbFind.Controls.Add(Me.DGMain)
        Me.GbFind.Controls.Add(Me.cmbSearchBy)
        Me.GbFind.Controls.Add(Me.Label7)
        Me.GbFind.Location = New System.Drawing.Point(17, 163)
        Me.GbFind.Name = "GbFind"
        Me.GbFind.Size = New System.Drawing.Size(687, 284)
        Me.GbFind.TabIndex = 14
        Me.GbFind.TabStop = False
        '
        'txtSearchBy
        '
        Me.txtSearchBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtSearchBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtSearchBy.Location = New System.Drawing.Point(234, 16)
        Me.txtSearchBy.Name = "txtSearchBy"
        Me.txtSearchBy.Size = New System.Drawing.Size(425, 20)
        Me.txtSearchBy.TabIndex = 4
        '
        'DGMain
        '
        Me.DGMain.AllowUserToAddRows = False
        Me.DGMain.AllowUserToDeleteRows = False
        Me.DGMain.AllowUserToOrderColumns = True
        Me.DGMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGMain.Location = New System.Drawing.Point(13, 51)
        Me.DGMain.Name = "DGMain"
        Me.DGMain.ReadOnly = True
        Me.DGMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGMain.Size = New System.Drawing.Size(663, 230)
        Me.DGMain.TabIndex = 3
        '
        'cmbSearchBy
        '
        Me.cmbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchBy.FormattingEnabled = True
        Me.cmbSearchBy.Items.AddRange(New Object() {"Qtype", "Plant", "Model"})
        Me.cmbSearchBy.Location = New System.Drawing.Point(97, 17)
        Me.cmbSearchBy.Name = "cmbSearchBy"
        Me.cmbSearchBy.Size = New System.Drawing.Size(121, 21)
        Me.cmbSearchBy.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Search By"
        '
        'frmAnnxure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 501)
        Me.Controls.Add(Me.GbFind)
        Me.Controls.Add(Me.GbMain)
        Me.Name = "frmAnnxure"
        Me.Text = "ANNEXURE1"
        Me.GbMain.ResumeLayout(False)
        Me.GbMain.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GbFind.ResumeLayout(False)
        Me.GbFind.PerformLayout()
        CType(Me.DGMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GbMain As System.Windows.Forms.GroupBox
    Friend WithEvents txtModel As System.Windows.Forms.TextBox
    Friend WithEvents txtPlant As System.Windows.Forms.TextBox
    Friend WithEvents txtQtype As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtDesciption As System.Windows.Forms.TextBox
    Friend WithEvents txtSrno As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GbFind As System.Windows.Forms.GroupBox
    Friend WithEvents DGMain As System.Windows.Forms.DataGridView
    Friend WithEvents cmbSearchBy As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtSearchBy As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel1 As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
End Class
