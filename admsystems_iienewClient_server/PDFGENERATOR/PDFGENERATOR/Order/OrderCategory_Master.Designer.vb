<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderCategory_Master
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
        Me.btnAddNewCat = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnBrowse1 = New System.Windows.Forms.Button()
        Me.txtPhoto1 = New System.Windows.Forms.TextBox()
        Me.txtGST = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.txtPhoto = New System.Windows.Forms.TextBox()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GvOrderCategoryDetail = New System.Windows.Forms.DataGridView()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCapacity = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMainCategory = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEntryNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.RblCapacity = New System.Windows.Forms.RadioButton()
        Me.RblPrice = New System.Windows.Forms.RadioButton()
        Me.RblMainCategory = New System.Windows.Forms.RadioButton()
        Me.GvOrderCategoryList = New System.Windows.Forms.DataGridView()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtTotalRecord = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.GvOrderCategoryDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.GvOrderCategoryList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAddNewCat)
        Me.GroupBox1.Controls.Add(Me.btnUpdate)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1027, 608)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnAddNewCat
        '
        Me.btnAddNewCat.Location = New System.Drawing.Point(383, 573)
        Me.btnAddNewCat.Name = "btnAddNewCat"
        Me.btnAddNewCat.Size = New System.Drawing.Size(85, 31)
        Me.btnAddNewCat.TabIndex = 93
        Me.btnAddNewCat.Text = "Add  New"
        Me.btnAddNewCat.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(501, 575)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 29)
        Me.btnUpdate.TabIndex = 92
        Me.btnUpdate.Text = "Submit"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(615, 573)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 29)
        Me.btnCancel.TabIndex = 91
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtRemarks)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.btnBrowse1)
        Me.GroupBox2.Controls.Add(Me.txtPhoto1)
        Me.GroupBox2.Controls.Add(Me.txtGST)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtItemName)
        Me.GroupBox2.Controls.Add(Me.btnBrowse)
        Me.GroupBox2.Controls.Add(Me.btnAddNew)
        Me.GroupBox2.Controls.Add(Me.txtPhoto)
        Me.GroupBox2.Controls.Add(Me.BtnSave)
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.txtPrice)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtModel)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtNo)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtCapacity)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtMainCategory)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtEntryNo)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(295, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(743, 574)
        Me.GroupBox2.TabIndex = 28
        Me.GroupBox2.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(540, 112)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 15)
        Me.Label12.TabIndex = 95
        Me.Label12.Tag = "Item Name"
        Me.Label12.Text = "Photo1"
        '
        'btnBrowse1
        '
        Me.btnBrowse1.Location = New System.Drawing.Point(628, 142)
        Me.btnBrowse1.Name = "btnBrowse1"
        Me.btnBrowse1.Size = New System.Drawing.Size(34, 25)
        Me.btnBrowse1.TabIndex = 94
        Me.btnBrowse1.Text = "+"
        Me.btnBrowse1.UseVisualStyleBackColor = True
        '
        'txtPhoto1
        '
        Me.txtPhoto1.Location = New System.Drawing.Point(503, 142)
        Me.txtPhoto1.Name = "txtPhoto1"
        Me.txtPhoto1.Size = New System.Drawing.Size(119, 21)
        Me.txtPhoto1.TabIndex = 93
        '
        'txtGST
        '
        Me.txtGST.Location = New System.Drawing.Point(419, 65)
        Me.txtGST.Name = "txtGST"
        Me.txtGST.Size = New System.Drawing.Size(78, 21)
        Me.txtGST.TabIndex = 91
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(437, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 15)
        Me.Label9.TabIndex = 92
        Me.Label9.Text = "GST"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(379, 108)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 15)
        Me.Label8.TabIndex = 90
        Me.Label8.Tag = "Item Name"
        Me.Label8.Text = "Photo"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(184, 108)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 15)
        Me.Label7.TabIndex = 89
        Me.Label7.Tag = "Item Name"
        Me.Label7.Text = "Item Name"
        '
        'txtItemName
        '
        Me.txtItemName.Location = New System.Drawing.Point(140, 142)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(178, 21)
        Me.txtItemName.TabIndex = 5
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(463, 140)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(34, 25)
        Me.btnBrowse.TabIndex = 7
        Me.btnBrowse.Text = "+"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'btnAddNew
        '
        Me.btnAddNew.Location = New System.Drawing.Point(13, 127)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(64, 44)
        Me.btnAddNew.TabIndex = 9
        Me.btnAddNew.Text = "Add New "
        Me.btnAddNew.UseVisualStyleBackColor = True
        '
        'txtPhoto
        '
        Me.txtPhoto.Location = New System.Drawing.Point(324, 142)
        Me.txtPhoto.Name = "txtPhoto"
        Me.txtPhoto.Size = New System.Drawing.Size(131, 21)
        Me.txtPhoto.TabIndex = 6
        '
        'BtnSave
        '
        Me.BtnSave.Location = New System.Drawing.Point(668, 137)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(63, 30)
        Me.BtnSave.TabIndex = 8
        Me.BtnSave.Text = "Add"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.AutoSize = True
        Me.GroupBox4.Controls.Add(Me.GvOrderCategoryDetail)
        Me.GroupBox4.Location = New System.Drawing.Point(16, 219)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(716, 348)
        Me.GroupBox4.TabIndex = 18
        Me.GroupBox4.TabStop = False
        '
        'GvOrderCategoryDetail
        '
        Me.GvOrderCategoryDetail.AllowDrop = True
        Me.GvOrderCategoryDetail.AllowUserToAddRows = False
        Me.GvOrderCategoryDetail.AllowUserToDeleteRows = False
        Me.GvOrderCategoryDetail.AllowUserToOrderColumns = True
        Me.GvOrderCategoryDetail.AllowUserToResizeRows = False
        Me.GvOrderCategoryDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvOrderCategoryDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvOrderCategoryDetail.Location = New System.Drawing.Point(12, 20)
        Me.GvOrderCategoryDetail.Name = "GvOrderCategoryDetail"
        Me.GvOrderCategoryDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvOrderCategoryDetail.Size = New System.Drawing.Size(689, 308)
        Me.GvOrderCategoryDetail.TabIndex = 0
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(513, 65)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(78, 21)
        Me.txtPrice.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(531, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Price"
        '
        'txtModel
        '
        Me.txtModel.Location = New System.Drawing.Point(106, 63)
        Me.txtModel.Name = "txtModel"
        Me.txtModel.Size = New System.Drawing.Size(286, 21)
        Me.txtModel.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Sub Category"
        '
        'txtNo
        '
        Me.txtNo.Location = New System.Drawing.Point(88, 142)
        Me.txtNo.Name = "txtNo"
        Me.txtNo.ReadOnly = True
        Me.txtNo.Size = New System.Drawing.Size(46, 21)
        Me.txtNo.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(106, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "No"
        '
        'txtCapacity
        '
        Me.txtCapacity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtCapacity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtCapacity.Location = New System.Drawing.Point(616, 64)
        Me.txtCapacity.Name = "txtCapacity"
        Me.txtCapacity.Size = New System.Drawing.Size(116, 21)
        Me.txtCapacity.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(641, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Capacity"
        '
        'txtMainCategory
        '
        Me.txtMainCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtMainCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtMainCategory.Location = New System.Drawing.Point(290, 23)
        Me.txtMainCategory.Name = "txtMainCategory"
        Me.txtMainCategory.Size = New System.Drawing.Size(327, 21)
        Me.txtMainCategory.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(184, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Main Category"
        '
        'txtEntryNo
        '
        Me.txtEntryNo.Location = New System.Drawing.Point(80, 23)
        Me.txtEntryNo.Name = "txtEntryNo"
        Me.txtEntryNo.ReadOnly = True
        Me.txtEntryNo.Size = New System.Drawing.Size(77, 21)
        Me.txtEntryNo.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Entry No"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.RblCapacity)
        Me.GroupBox6.Controls.Add(Me.RblPrice)
        Me.GroupBox6.Controls.Add(Me.RblMainCategory)
        Me.GroupBox6.Controls.Add(Me.GvOrderCategoryList)
        Me.GroupBox6.Controls.Add(Me.btnRefresh)
        Me.GroupBox6.Controls.Add(Me.txtTotalRecord)
        Me.GroupBox6.Controls.Add(Me.Label10)
        Me.GroupBox6.Controls.Add(Me.Button1)
        Me.GroupBox6.Controls.Add(Me.Label11)
        Me.GroupBox6.Controls.Add(Me.txtsearch)
        Me.GroupBox6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(15, 19)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(274, 570)
        Me.GroupBox6.TabIndex = 27
        Me.GroupBox6.TabStop = False
        '
        'RblCapacity
        '
        Me.RblCapacity.AutoSize = True
        Me.RblCapacity.Location = New System.Drawing.Point(113, 26)
        Me.RblCapacity.Name = "RblCapacity"
        Me.RblCapacity.Size = New System.Drawing.Size(72, 19)
        Me.RblCapacity.TabIndex = 18
        Me.RblCapacity.TabStop = True
        Me.RblCapacity.Text = "Capacity"
        Me.RblCapacity.UseVisualStyleBackColor = True
        '
        'RblPrice
        '
        Me.RblPrice.AutoSize = True
        Me.RblPrice.Location = New System.Drawing.Point(190, 26)
        Me.RblPrice.Name = "RblPrice"
        Me.RblPrice.Size = New System.Drawing.Size(53, 19)
        Me.RblPrice.TabIndex = 17
        Me.RblPrice.TabStop = True
        Me.RblPrice.Text = "Price"
        Me.RblPrice.UseVisualStyleBackColor = True
        '
        'RblMainCategory
        '
        Me.RblMainCategory.AutoSize = True
        Me.RblMainCategory.Location = New System.Drawing.Point(9, 26)
        Me.RblMainCategory.Name = "RblMainCategory"
        Me.RblMainCategory.Size = New System.Drawing.Size(103, 19)
        Me.RblMainCategory.TabIndex = 16
        Me.RblMainCategory.TabStop = True
        Me.RblMainCategory.Text = "Main Category"
        Me.RblMainCategory.UseVisualStyleBackColor = True
        '
        'GvOrderCategoryList
        '
        Me.GvOrderCategoryList.AllowDrop = True
        Me.GvOrderCategoryList.AllowUserToAddRows = False
        Me.GvOrderCategoryList.AllowUserToDeleteRows = False
        Me.GvOrderCategoryList.AllowUserToOrderColumns = True
        Me.GvOrderCategoryList.AllowUserToResizeColumns = False
        Me.GvOrderCategoryList.AllowUserToResizeRows = False
        Me.GvOrderCategoryList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvOrderCategoryList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvOrderCategoryList.Location = New System.Drawing.Point(6, 123)
        Me.GvOrderCategoryList.Name = "GvOrderCategoryList"
        Me.GvOrderCategoryList.ReadOnly = True
        Me.GvOrderCategoryList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvOrderCategoryList.Size = New System.Drawing.Size(260, 432)
        Me.GvOrderCategoryList.TabIndex = 15
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(175, 571)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 30)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtTotalRecord
        '
        Me.txtTotalRecord.Location = New System.Drawing.Point(88, 574)
        Me.txtTotalRecord.Name = "txtTotalRecord"
        Me.txtTotalRecord.Size = New System.Drawing.Size(80, 21)
        Me.txtTotalRecord.TabIndex = 13
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 574)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 15)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Total Record"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(191, 80)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 84)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 15)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Category"
        '
        'txtsearch
        '
        Me.txtsearch.Location = New System.Drawing.Point(65, 83)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(120, 21)
        Me.txtsearch.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(140, 183)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(477, 21)
        Me.txtRemarks.TabIndex = 96
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(71, 189)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(58, 15)
        Me.Label13.TabIndex = 97
        Me.Label13.Tag = "Item Name"
        Me.Label13.Text = "Remarks"
        '
        'OrderCategory_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1062, 632)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "OrderCategory_Master"
        Me.Text = "OrderCategory_Master"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.GvOrderCategoryDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.GvOrderCategoryList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents RblCapacity As System.Windows.Forms.RadioButton
    Friend WithEvents RblPrice As System.Windows.Forms.RadioButton
    Friend WithEvents RblMainCategory As System.Windows.Forms.RadioButton
    Friend WithEvents GvOrderCategoryList As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents txtTotalRecord As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents btnAddNew As System.Windows.Forms.Button
    Friend WithEvents txtPhoto As System.Windows.Forms.TextBox
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GvOrderCategoryDetail As System.Windows.Forms.DataGridView
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtModel As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCapacity As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMainCategory As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEntryNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnAddNewCat As System.Windows.Forms.Button
    Friend WithEvents txtGST As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse1 As System.Windows.Forms.Button
    Friend WithEvents txtPhoto1 As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
End Class
