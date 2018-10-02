<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Expense_Master
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
        Me.ddlODType = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ddlEnggType = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.ddlMainEngineer = New System.Windows.Forms.ComboBox()
        Me.btnUpdateInvoiceStatus = New System.Windows.Forms.Button()
        Me.ddlStatus = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.ChkEngineer = New System.Windows.Forms.CheckedListBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dtInvoiceDate = New System.Windows.Forms.DateTimePicker()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtDistrict = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtTaluka = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GvExpenseDetail = New System.Windows.Forms.DataGridView()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnExpeseAdd = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.ddlExpense = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEnqNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GvExpenseList = New System.Windows.Forms.DataGridView()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rblDoneSearch = New System.Windows.Forms.RadioButton()
        Me.rblPendingSearch = New System.Windows.Forms.RadioButton()
        Me.txtNameSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEnqNoSearch = New System.Windows.Forms.TextBox()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.lnkCalculator = New System.Windows.Forms.LinkLabel()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GvExpenseDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GvExpenseList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lnkCalculator)
        Me.GroupBox1.Controls.Add(Me.ddlODType)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.ddlEnggType)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.ddlMainEngineer)
        Me.GroupBox1.Controls.Add(Me.btnUpdateInvoiceStatus)
        Me.GroupBox1.Controls.Add(Me.ddlStatus)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.ChkEngineer)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.dtInvoiceDate)
        Me.GroupBox1.Controls.Add(Me.txtState)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtDistrict)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtTaluka)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtCity)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtDate)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.GvExpenseDetail)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.btnExpeseAdd)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtAmount)
        Me.GroupBox1.Controls.Add(Me.ddlExpense)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtPartyName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtEnqNo)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1007, 559)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " "
        '
        'ddlODType
        '
        Me.ddlODType.FormattingEnabled = True
        Me.ddlODType.Location = New System.Drawing.Point(811, 250)
        Me.ddlODType.Name = "ddlODType"
        Me.ddlODType.Size = New System.Drawing.Size(98, 21)
        Me.ddlODType.TabIndex = 7
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(805, 232)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 13)
        Me.Label16.TabIndex = 51
        Me.Label16.Text = "  OD Type"
        '
        'ddlEnggType
        '
        Me.ddlEnggType.FormattingEnabled = True
        Me.ddlEnggType.Location = New System.Drawing.Point(696, 246)
        Me.ddlEnggType.Name = "ddlEnggType"
        Me.ddlEnggType.Size = New System.Drawing.Size(106, 21)
        Me.ddlEnggType.TabIndex = 6
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(712, 230)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(37, 13)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "  Type"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(427, 216)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(75, 13)
        Me.Label18.TabIndex = 47
        Me.Label18.Text = "Main Engineer"
        '
        'ddlMainEngineer
        '
        Me.ddlMainEngineer.FormattingEnabled = True
        Me.ddlMainEngineer.Location = New System.Drawing.Point(409, 242)
        Me.ddlMainEngineer.Name = "ddlMainEngineer"
        Me.ddlMainEngineer.Size = New System.Drawing.Size(126, 21)
        Me.ddlMainEngineer.TabIndex = 4
        '
        'btnUpdateInvoiceStatus
        '
        Me.btnUpdateInvoiceStatus.Location = New System.Drawing.Point(610, 128)
        Me.btnUpdateInvoiceStatus.Name = "btnUpdateInvoiceStatus"
        Me.btnUpdateInvoiceStatus.Size = New System.Drawing.Size(80, 28)
        Me.btnUpdateInvoiceStatus.TabIndex = 45
        Me.btnUpdateInvoiceStatus.Text = "Update"
        Me.btnUpdateInvoiceStatus.UseVisualStyleBackColor = True
        '
        'ddlStatus
        '
        Me.ddlStatus.AutoCompleteCustomSource.AddRange(New String() {" "})
        Me.ddlStatus.FormattingEnabled = True
        Me.ddlStatus.Items.AddRange(New Object() {" "})
        Me.ddlStatus.Location = New System.Drawing.Point(416, 128)
        Me.ddlStatus.Name = "ddlStatus"
        Me.ddlStatus.Size = New System.Drawing.Size(152, 21)
        Me.ddlStatus.TabIndex = 2
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(337, 132)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(37, 13)
        Me.Label19.TabIndex = 43
        Me.Label19.Text = "Status"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(928, 323)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(63, 23)
        Me.btnDelete.TabIndex = 42
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'ChkEngineer
        '
        Me.ChkEngineer.FormattingEnabled = True
        Me.ChkEngineer.Location = New System.Drawing.Point(541, 191)
        Me.ChkEngineer.Name = "ChkEngineer"
        Me.ChkEngineer.Size = New System.Drawing.Size(149, 79)
        Me.ChkEngineer.TabIndex = 5
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(848, 21)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(30, 13)
        Me.Label17.TabIndex = 38
        Me.Label17.Text = "Date"
        '
        'dtInvoiceDate
        '
        Me.dtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtInvoiceDate.Location = New System.Drawing.Point(890, 18)
        Me.dtInvoiceDate.Name = "dtInvoiceDate"
        Me.dtInvoiceDate.Size = New System.Drawing.Size(90, 20)
        Me.dtInvoiceDate.TabIndex = 2
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(890, 92)
        Me.txtState.Name = "txtState"
        Me.txtState.ReadOnly = True
        Me.txtState.Size = New System.Drawing.Size(109, 20)
        Me.txtState.TabIndex = 15
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(846, 95)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(32, 13)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "State"
        '
        'txtDistrict
        '
        Me.txtDistrict.Location = New System.Drawing.Point(733, 92)
        Me.txtDistrict.Name = "txtDistrict"
        Me.txtDistrict.ReadOnly = True
        Me.txtDistrict.Size = New System.Drawing.Size(109, 20)
        Me.txtDistrict.TabIndex = 14
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(693, 94)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(39, 13)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "District"
        '
        'txtTaluka
        '
        Me.txtTaluka.Location = New System.Drawing.Point(572, 89)
        Me.txtTaluka.Name = "txtTaluka"
        Me.txtTaluka.ReadOnly = True
        Me.txtTaluka.Size = New System.Drawing.Size(109, 20)
        Me.txtTaluka.TabIndex = 13
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(528, 92)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Taluko"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(418, 88)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReadOnly = True
        Me.txtCity.Size = New System.Drawing.Size(109, 20)
        Me.txtCity.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(337, 92)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 13)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "City"
        '
        'txtDate
        '
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtDate.Location = New System.Drawing.Point(308, 240)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(90, 20)
        Me.txtDate.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(330, 216)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 13)
        Me.Label12.TabIndex = 25
        Me.Label12.Text = "Att.Date"
        '
        'GvExpenseDetail
        '
        Me.GvExpenseDetail.AllowDrop = True
        Me.GvExpenseDetail.AllowUserToAddRows = False
        Me.GvExpenseDetail.AllowUserToDeleteRows = False
        Me.GvExpenseDetail.AllowUserToOrderColumns = True
        Me.GvExpenseDetail.AllowUserToResizeColumns = False
        Me.GvExpenseDetail.AllowUserToResizeRows = False
        Me.GvExpenseDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvExpenseDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvExpenseDetail.Location = New System.Drawing.Point(300, 350)
        Me.GvExpenseDetail.Name = "GvExpenseDetail"
        Me.GvExpenseDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvExpenseDetail.Size = New System.Drawing.Size(691, 185)
        Me.GvExpenseDetail.TabIndex = 23
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(696, 311)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(213, 20)
        Me.txtRemarks.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(753, 295)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Remarks"
        '
        'btnExpeseAdd
        '
        Me.btnExpeseAdd.Location = New System.Drawing.Point(921, 264)
        Me.btnExpeseAdd.Name = "btnExpeseAdd"
        Me.btnExpeseAdd.Size = New System.Drawing.Size(75, 50)
        Me.btnExpeseAdd.TabIndex = 11
        Me.btnExpeseAdd.Text = "Add"
        Me.btnExpeseAdd.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(569, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Invoice No"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(647, 17)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(102, 20)
        Me.txtInvoiceNo.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(565, 286)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Amount"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(554, 311)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(115, 20)
        Me.txtAmount.TabIndex = 9
        '
        'ddlExpense
        '
        Me.ddlExpense.FormattingEnabled = True
        Me.ddlExpense.Location = New System.Drawing.Point(430, 310)
        Me.ddlExpense.Name = "ddlExpense"
        Me.ddlExpense.Size = New System.Drawing.Size(97, 21)
        Me.ddlExpense.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(437, 286)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Expense Type"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(585, 175)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Engineer"
        '
        'txtPartyName
        '
        Me.txtPartyName.Location = New System.Drawing.Point(416, 55)
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.ReadOnly = True
        Me.txtPartyName.Size = New System.Drawing.Size(517, 20)
        Me.txtPartyName.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(335, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Name"
        '
        'txtEnqNo
        '
        Me.txtEnqNo.Location = New System.Drawing.Point(418, 18)
        Me.txtEnqNo.Name = "txtEnqNo"
        Me.txtEnqNo.Size = New System.Drawing.Size(94, 20)
        Me.txtEnqNo.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(334, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "EnqNo"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.GvExpenseList)
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.txtNameSearch)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtEnqNoSearch)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(288, 540)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " "
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(109, 82)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 34)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GvExpenseList
        '
        Me.GvExpenseList.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar
        Me.GvExpenseList.AllowDrop = True
        Me.GvExpenseList.AllowUserToAddRows = False
        Me.GvExpenseList.AllowUserToDeleteRows = False
        Me.GvExpenseList.AllowUserToOrderColumns = True
        Me.GvExpenseList.AllowUserToResizeColumns = False
        Me.GvExpenseList.AllowUserToResizeRows = False
        Me.GvExpenseList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvExpenseList.Location = New System.Drawing.Point(6, 172)
        Me.GvExpenseList.Name = "GvExpenseList"
        Me.GvExpenseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvExpenseList.Size = New System.Drawing.Size(276, 350)
        Me.GvExpenseList.TabIndex = 58
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rblDoneSearch)
        Me.GroupBox5.Controls.Add(Me.rblPendingSearch)
        Me.GroupBox5.Location = New System.Drawing.Point(26, 122)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(237, 36)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = " "
        '
        'rblDoneSearch
        '
        Me.rblDoneSearch.AutoSize = True
        Me.rblDoneSearch.Location = New System.Drawing.Point(27, 13)
        Me.rblDoneSearch.Name = "rblDoneSearch"
        Me.rblDoneSearch.Size = New System.Drawing.Size(51, 17)
        Me.rblDoneSearch.TabIndex = 1
        Me.rblDoneSearch.Text = "Done"
        Me.rblDoneSearch.UseVisualStyleBackColor = True
        '
        'rblPendingSearch
        '
        Me.rblPendingSearch.AutoSize = True
        Me.rblPendingSearch.Checked = True
        Me.rblPendingSearch.Location = New System.Drawing.Point(115, 13)
        Me.rblPendingSearch.Name = "rblPendingSearch"
        Me.rblPendingSearch.Size = New System.Drawing.Size(65, 17)
        Me.rblPendingSearch.TabIndex = 0
        Me.rblPendingSearch.TabStop = True
        Me.rblPendingSearch.Text = "Running"
        Me.rblPendingSearch.UseVisualStyleBackColor = True
        '
        'txtNameSearch
        '
        Me.txtNameSearch.Location = New System.Drawing.Point(69, 56)
        Me.txtNameSearch.Name = "txtNameSearch"
        Me.txtNameSearch.Size = New System.Drawing.Size(194, 20)
        Me.txtNameSearch.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "EnqNo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Name"
        '
        'txtEnqNoSearch
        '
        Me.txtEnqNoSearch.Location = New System.Drawing.Point(69, 23)
        Me.txtEnqNoSearch.Name = "txtEnqNoSearch"
        Me.txtEnqNoSearch.Size = New System.Drawing.Size(194, 20)
        Me.txtEnqNoSearch.TabIndex = 0
        '
        'btnAddNew
        '
        Me.btnAddNew.Location = New System.Drawing.Point(546, 574)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(75, 32)
        Me.btnAddNew.TabIndex = 1
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.UseVisualStyleBackColor = True
        '
        'lnkCalculator
        '
        Me.lnkCalculator.AutoSize = True
        Me.lnkCalculator.Location = New System.Drawing.Point(615, 286)
        Me.lnkCalculator.Name = "lnkCalculator"
        Me.lnkCalculator.Size = New System.Drawing.Size(54, 13)
        Me.lnkCalculator.TabIndex = 52
        Me.lnkCalculator.TabStop = True
        Me.lnkCalculator.Text = "Calculator"
        '
        'Expense_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1031, 611)
        Me.Controls.Add(Me.btnAddNew)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Expense_Master"
        Me.Text = "Expense_Master"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GvExpenseDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.GvExpenseList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEnqNoSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEnqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPartyName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ddlExpense As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents btnExpeseAdd As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents GvExpenseDetail As System.Windows.Forms.DataGridView
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtTaluka As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtDistrict As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents dtInvoiceDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents ChkEngineer As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnAddNew As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ddlStatus As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rblPendingSearch As System.Windows.Forms.RadioButton
    Friend WithEvents rblDoneSearch As System.Windows.Forms.RadioButton
    Friend WithEvents btnUpdateInvoiceStatus As System.Windows.Forms.Button
    Friend WithEvents ddlMainEngineer As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents ddlEnggType As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents ddlODType As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents GvExpenseList As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lnkCalculator As System.Windows.Forms.LinkLabel
End Class
