﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockRegister
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtTotRecords = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.dgCategories = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtcategory = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnfinalstaock = New System.Windows.Forms.Button()
        Me.btnStockReports = New System.Windows.Forms.Button()
        Me.btnViewTodayStock = New System.Windows.Forms.Button()
        Me.cmb_Category = New System.Windows.Forms.ComboBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dtEntrydate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtOutward = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtClosing = New System.Windows.Forms.TextBox()
        Me.txtOpening = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtunit = New System.Windows.Forms.TextBox()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgRawMaterialData = New System.Windows.Forms.DataGridView()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtReorder = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtIssueStock = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cmb_RawMaterial = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnSetMonthStock = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnAddNewAll = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgCategories, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgRawMaterialData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(294, 571)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.txtTotRecords)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.txtcategory)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(6, 10)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(285, 542)
        Me.Panel1.TabIndex = 0
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(170, 513)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtTotRecords
        '
        Me.txtTotRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotRecords.Location = New System.Drawing.Point(114, 516)
        Me.txtTotRecords.Name = "txtTotRecords"
        Me.txtTotRecords.ReadOnly = True
        Me.txtTotRecords.Size = New System.Drawing.Size(41, 20)
        Me.txtTotRecords.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 519)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Total Records"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.dgCategories)
        Me.Panel2.Location = New System.Drawing.Point(7, 37)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(274, 474)
        Me.Panel2.TabIndex = 3
        '
        'dgCategories
        '
        Me.dgCategories.AllowUserToAddRows = False
        Me.dgCategories.AllowUserToDeleteRows = False
        Me.dgCategories.AllowUserToOrderColumns = True
        Me.dgCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCategories.Location = New System.Drawing.Point(2, 6)
        Me.dgCategories.Margin = New System.Windows.Forms.Padding(2)
        Me.dgCategories.Name = "dgCategories"
        Me.dgCategories.ReadOnly = True
        Me.dgCategories.RowTemplate.Height = 24
        Me.dgCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgCategories.Size = New System.Drawing.Size(266, 460)
        Me.dgCategories.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(216, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(59, 23)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtcategory
        '
        Me.txtcategory.Location = New System.Drawing.Point(65, 7)
        Me.txtcategory.Name = "txtcategory"
        Me.txtcategory.Size = New System.Drawing.Size(144, 20)
        Me.txtcategory.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Category"
        '
        'GroupBox2
        '
        Me.GroupBox2.AutoSize = True
        Me.GroupBox2.Controls.Add(Me.btnfinalstaock)
        Me.GroupBox2.Controls.Add(Me.btnStockReports)
        Me.GroupBox2.Controls.Add(Me.btnViewTodayStock)
        Me.GroupBox2.Controls.Add(Me.cmb_Category)
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Cmb_RawMaterial)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Location = New System.Drawing.Point(308, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(820, 513)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'btnfinalstaock
        '
        Me.btnfinalstaock.Location = New System.Drawing.Point(695, 16)
        Me.btnfinalstaock.Name = "btnfinalstaock"
        Me.btnfinalstaock.Size = New System.Drawing.Size(95, 43)
        Me.btnfinalstaock.TabIndex = 6
        Me.btnfinalstaock.Text = "Final Stock"
        Me.btnfinalstaock.UseVisualStyleBackColor = True
        '
        'btnStockReports
        '
        Me.btnStockReports.Location = New System.Drawing.Point(530, 42)
        Me.btnStockReports.Name = "btnStockReports"
        Me.btnStockReports.Size = New System.Drawing.Size(122, 23)
        Me.btnStockReports.TabIndex = 5
        Me.btnStockReports.Text = "Stock Reports"
        Me.btnStockReports.UseVisualStyleBackColor = True
        '
        'btnViewTodayStock
        '
        Me.btnViewTodayStock.Location = New System.Drawing.Point(530, 14)
        Me.btnViewTodayStock.Name = "btnViewTodayStock"
        Me.btnViewTodayStock.Size = New System.Drawing.Size(122, 23)
        Me.btnViewTodayStock.TabIndex = 4
        Me.btnViewTodayStock.Text = "ViewTodayStock"
        Me.btnViewTodayStock.UseVisualStyleBackColor = True
        '
        'cmb_Category
        '
        Me.cmb_Category.FormattingEnabled = True
        Me.cmb_Category.Location = New System.Drawing.Point(114, 10)
        Me.cmb_Category.Margin = New System.Windows.Forms.Padding(2)
        Me.cmb_Category.Name = "cmb_Category"
        Me.cmb_Category.Size = New System.Drawing.Size(289, 21)
        Me.cmb_Category.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnExport)
        Me.GroupBox4.Controls.Add(Me.dtEntrydate)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.txtOutward)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.txtClosing)
        Me.GroupBox4.Controls.Add(Me.txtOpening)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txtunit)
        Me.GroupBox4.Controls.Add(Me.btnDel)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.dgRawMaterialData)
        Me.GroupBox4.Controls.Add(Me.btnAdd)
        Me.GroupBox4.Controls.Add(Me.txtReorder)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.txtIssueStock)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.btnNew)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 71)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(807, 423)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(688, 42)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(76, 21)
        Me.btnExport.TabIndex = 20
        Me.btnExport.Text = "Export Excel"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'dtEntrydate
        '
        Me.dtEntrydate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEntrydate.Location = New System.Drawing.Point(523, 35)
        Me.dtEntrydate.Name = "dtEntrydate"
        Me.dtEntrydate.Size = New System.Drawing.Size(81, 20)
        Me.dtEntrydate.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(524, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Entry Date"
        '
        'txtOutward
        '
        Me.txtOutward.Location = New System.Drawing.Point(293, 36)
        Me.txtOutward.Name = "txtOutward"
        Me.txtOutward.Size = New System.Drawing.Size(74, 20)
        Me.txtOutward.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(290, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Outward"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(447, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(41, 13)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Closing"
        '
        'txtClosing
        '
        Me.txtClosing.Location = New System.Drawing.Point(447, 36)
        Me.txtClosing.Name = "txtClosing"
        Me.txtClosing.Size = New System.Drawing.Size(65, 20)
        Me.txtClosing.TabIndex = 6
        '
        'txtOpening
        '
        Me.txtOpening.Location = New System.Drawing.Point(121, 36)
        Me.txtOpening.Name = "txtOpening"
        Me.txtOpening.Size = New System.Drawing.Size(68, 20)
        Me.txtOpening.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(118, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Opening"
        '
        'txtunit
        '
        Me.txtunit.Location = New System.Drawing.Point(73, 36)
        Me.txtunit.Name = "txtunit"
        Me.txtunit.Size = New System.Drawing.Size(37, 20)
        Me.txtunit.TabIndex = 1
        '
        'btnDel
        '
        Me.btnDel.Location = New System.Drawing.Point(688, 15)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(76, 21)
        Me.btnDel.TabIndex = 9
        Me.btnDel.Text = "Delete"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(76, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Unit"
        '
        'dgRawMaterialData
        '
        Me.dgRawMaterialData.AllowUserToAddRows = False
        Me.dgRawMaterialData.AllowUserToDeleteRows = False
        Me.dgRawMaterialData.AllowUserToOrderColumns = True
        Me.dgRawMaterialData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgRawMaterialData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgRawMaterialData.Location = New System.Drawing.Point(5, 68)
        Me.dgRawMaterialData.Margin = New System.Windows.Forms.Padding(2)
        Me.dgRawMaterialData.Name = "dgRawMaterialData"
        Me.dgRawMaterialData.ReadOnly = True
        Me.dgRawMaterialData.RowTemplate.Height = 24
        Me.dgRawMaterialData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgRawMaterialData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgRawMaterialData.Size = New System.Drawing.Size(797, 350)
        Me.dgRawMaterialData.TabIndex = 10
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(610, 34)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(67, 21)
        Me.btnAdd.TabIndex = 8
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtReorder
        '
        Me.txtReorder.Enabled = False
        Me.txtReorder.Location = New System.Drawing.Point(379, 36)
        Me.txtReorder.Name = "txtReorder"
        Me.txtReorder.Size = New System.Drawing.Size(55, 20)
        Me.txtReorder.TabIndex = 5
        Me.txtReorder.Text = "0"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(376, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 13)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "ReInward"
        '
        'txtIssueStock
        '
        Me.txtIssueStock.Location = New System.Drawing.Point(211, 36)
        Me.txtIssueStock.Name = "txtIssueStock"
        Me.txtIssueStock.Size = New System.Drawing.Size(74, 20)
        Me.txtIssueStock.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(208, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Inward"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(5, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(45, 47)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Category"
        '
        'Cmb_RawMaterial
        '
        Me.Cmb_RawMaterial.FormattingEnabled = True
        Me.Cmb_RawMaterial.Location = New System.Drawing.Point(114, 47)
        Me.Cmb_RawMaterial.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_RawMaterial.Name = "Cmb_RawMaterial"
        Me.Cmb_RawMaterial.Size = New System.Drawing.Size(289, 21)
        Me.Cmb_RawMaterial.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Raw Material Name"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnSetMonthStock)
        Me.GroupBox3.Controls.Add(Me.btnBack)
        Me.GroupBox3.Controls.Add(Me.btnCancel)
        Me.GroupBox3.Controls.Add(Me.btnDelete)
        Me.GroupBox3.Controls.Add(Me.btnAddNewAll)
        Me.GroupBox3.Location = New System.Drawing.Point(311, 520)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(817, 56)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        '
        'btnSetMonthStock
        '
        Me.btnSetMonthStock.BackColor = System.Drawing.Color.IndianRed
        Me.btnSetMonthStock.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSetMonthStock.Location = New System.Drawing.Point(587, 7)
        Me.btnSetMonthStock.Name = "btnSetMonthStock"
        Me.btnSetMonthStock.Size = New System.Drawing.Size(117, 46)
        Me.btnSetMonthStock.TabIndex = 21
        Me.btnSetMonthStock.Text = "Set Month Stock"
        Me.btnSetMonthStock.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(486, 7)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(73, 46)
        Me.btnBack.TabIndex = 20
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(395, 7)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(73, 46)
        Me.btnCancel.TabIndex = 19
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(304, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(73, 46)
        Me.btnDelete.TabIndex = 18
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnAddNewAll
        '
        Me.btnAddNewAll.Location = New System.Drawing.Point(213, 7)
        Me.btnAddNewAll.Name = "btnAddNewAll"
        Me.btnAddNewAll.Size = New System.Drawing.Size(73, 46)
        Me.btnAddNewAll.TabIndex = 15
        Me.btnAddNewAll.Text = "Add"
        Me.btnAddNewAll.UseVisualStyleBackColor = True
        '
        'StockRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1152, 588)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "StockRegister"
        Me.Text = "Stock Register"
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgCategories, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.dgRawMaterialData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents txtTotRecords As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtcategory As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtReorder As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtClosing As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtIssueStock As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dgRawMaterialData As System.Windows.Forms.DataGridView
    Friend WithEvents cmb_Category As System.Windows.Forms.ComboBox
    Friend WithEvents Cmb_RawMaterial As System.Windows.Forms.ComboBox
    Friend WithEvents dgCategories As System.Windows.Forms.DataGridView
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents txtunit As System.Windows.Forms.TextBox
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnAddNewAll As System.Windows.Forms.Button
    Friend WithEvents btnSetMonthStock As System.Windows.Forms.Button
    Friend WithEvents txtOutward As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtOpening As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtEntrydate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnStockReports As System.Windows.Forms.Button
    Friend WithEvents btnViewTodayStock As System.Windows.Forms.Button
    Friend WithEvents btnfinalstaock As System.Windows.Forms.Button
End Class
