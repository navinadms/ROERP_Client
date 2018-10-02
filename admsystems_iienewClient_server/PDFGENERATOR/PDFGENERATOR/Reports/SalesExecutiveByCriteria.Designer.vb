<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SalesExecutiveByCriteria
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
        Me.txtThrough = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.grpUser = New System.Windows.Forms.Panel()
        Me.txtAllUser = New System.Windows.Forms.TextBox()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtPriority = New System.Windows.Forms.TextBox()
        Me.txtAllQuotationType = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnAddQuotationtype = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ddlQuotationType = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtALLEnqType = New System.Windows.Forms.TextBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnAddEnqType = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ddlEnqType = New System.Windows.Forms.ComboBox()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.txtVisitType = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtQtnType = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rblOther = New System.Windows.Forms.RadioButton()
        Me.rblVisit = New System.Windows.Forms.RadioButton()
        Me.rblSalesExecutive = New System.Windows.Forms.RadioButton()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.txtAllToUser = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rblTo = New System.Windows.Forms.RadioButton()
        Me.rblFrom = New System.Windows.Forms.RadioButton()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rblToDate = New System.Windows.Forms.RadioButton()
        Me.rblFromDate = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.grpUser.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtThrough)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.grpUser)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.dtStartDate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtEndDate)
        Me.GroupBox1.Controls.Add(Me.Panel4)
        Me.GroupBox1.Location = New System.Drawing.Point(46, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(959, 191)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtThrough
        '
        Me.txtThrough.AutoCompleteCustomSource.AddRange(New String() {"Mail", "Courier", "H2H", "WhatsApp", "SPEEDPOST", "MAIL + SPEEDPOST", "MAIL + COURIER", "MAIL + WHATSAPP", "COURIER + WHATSAPP", "SPEEDPOST + WHATSAPP", "MAIL + SPEEDPOST"})
        Me.txtThrough.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtThrough.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtThrough.Location = New System.Drawing.Point(741, 158)
        Me.txtThrough.Name = "txtThrough"
        Me.txtThrough.Size = New System.Drawing.Size(159, 20)
        Me.txtThrough.TabIndex = 28
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(796, 135)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Through"
        '
        'grpUser
        '
        Me.grpUser.Controls.Add(Me.txtAllUser)
        Me.grpUser.Controls.Add(Me.btnAddUser)
        Me.grpUser.Controls.Add(Me.Label8)
        Me.grpUser.Controls.Add(Me.cmbUser)
        Me.grpUser.Location = New System.Drawing.Point(701, 10)
        Me.grpUser.Name = "grpUser"
        Me.grpUser.Size = New System.Drawing.Size(245, 109)
        Me.grpUser.TabIndex = 24
        '
        'txtAllUser
        '
        Me.txtAllUser.Location = New System.Drawing.Point(73, 69)
        Me.txtAllUser.Multiline = True
        Me.txtAllUser.Name = "txtAllUser"
        Me.txtAllUser.Size = New System.Drawing.Size(159, 33)
        Me.txtAllUser.TabIndex = 23
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(105, 40)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(44, 23)
        Me.btnAddUser.TabIndex = 21
        Me.btnAddUser.Text = "Add"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(19, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "User"
        '
        'cmbUser
        '
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(73, 13)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(159, 21)
        Me.cmbUser.TabIndex = 20
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtPriority)
        Me.Panel2.Controls.Add(Me.txtAllQuotationType)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.BtnAddQuotationtype)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.ddlQuotationType)
        Me.Panel2.Location = New System.Drawing.Point(450, 10)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(245, 175)
        Me.Panel2.TabIndex = 21
        '
        'txtPriority
        '
        Me.txtPriority.AutoCompleteCustomSource.AddRange(New String() {"Normal", "Medium", "Urgent", "Courier"})
        Me.txtPriority.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtPriority.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPriority.Location = New System.Drawing.Point(73, 138)
        Me.txtPriority.Name = "txtPriority"
        Me.txtPriority.Size = New System.Drawing.Size(159, 20)
        Me.txtPriority.TabIndex = 24
        '
        'txtAllQuotationType
        '
        Me.txtAllQuotationType.Location = New System.Drawing.Point(73, 69)
        Me.txtAllQuotationType.Multiline = True
        Me.txtAllQuotationType.Name = "txtAllQuotationType"
        Me.txtAllQuotationType.Size = New System.Drawing.Size(159, 33)
        Me.txtAllQuotationType.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(131, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Priority"
        '
        'BtnAddQuotationtype
        '
        Me.BtnAddQuotationtype.Location = New System.Drawing.Point(105, 40)
        Me.BtnAddQuotationtype.Name = "BtnAddQuotationtype"
        Me.BtnAddQuotationtype.Size = New System.Drawing.Size(44, 23)
        Me.BtnAddQuotationtype.TabIndex = 21
        Me.BtnAddQuotationtype.Text = "Add"
        Me.BtnAddQuotationtype.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Quot.Type"
        '
        'ddlQuotationType
        '
        Me.ddlQuotationType.FormattingEnabled = True
        Me.ddlQuotationType.Location = New System.Drawing.Point(73, 13)
        Me.ddlQuotationType.Name = "ddlQuotationType"
        Me.ddlQuotationType.Size = New System.Drawing.Size(159, 21)
        Me.ddlQuotationType.TabIndex = 20
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtALLEnqType)
        Me.Panel1.Controls.Add(Me.txtStatus)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.btnAddEnqType)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.ddlEnqType)
        Me.Panel1.Location = New System.Drawing.Point(212, 9)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(233, 178)
        Me.Panel1.TabIndex = 20
        '
        'txtALLEnqType
        '
        Me.txtALLEnqType.Location = New System.Drawing.Point(58, 69)
        Me.txtALLEnqType.Multiline = True
        Me.txtALLEnqType.Name = "txtALLEnqType"
        Me.txtALLEnqType.Size = New System.Drawing.Size(159, 41)
        Me.txtALLEnqType.TabIndex = 23
        '
        'txtStatus
        '
        Me.txtStatus.AutoCompleteCustomSource.AddRange(New String() {"Pending", "Cancel", "Done"})
        Me.txtStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtStatus.Location = New System.Drawing.Point(57, 139)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(159, 20)
        Me.txtStatus.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(101, 116)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Status"
        '
        'btnAddEnqType
        '
        Me.btnAddEnqType.Location = New System.Drawing.Point(96, 41)
        Me.btnAddEnqType.Name = "btnAddEnqType"
        Me.btnAddEnqType.Size = New System.Drawing.Size(44, 23)
        Me.btnAddEnqType.TabIndex = 17
        Me.btnAddEnqType.Text = "Add"
        Me.btnAddEnqType.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Enq.Type"
        '
        'ddlEnqType
        '
        Me.ddlEnqType.FormattingEnabled = True
        Me.ddlEnqType.Location = New System.Drawing.Point(66, 13)
        Me.ddlEnqType.Name = "ddlEnqType"
        Me.ddlEnqType.Size = New System.Drawing.Size(159, 21)
        Me.ddlEnqType.TabIndex = 16
        '
        'dtStartDate
        '
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDate.Location = New System.Drawing.Point(68, 45)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(111, 20)
        Me.dtStartDate.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "From :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "To :"
        '
        'dtEndDate
        '
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEndDate.Location = New System.Drawing.Point(68, 82)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(111, 20)
        Me.dtEndDate.TabIndex = 5
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.txtVisitType)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.txtQtnType)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Location = New System.Drawing.Point(6, 9)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(200, 175)
        Me.Panel4.TabIndex = 26
        '
        'txtVisitType
        '
        Me.txtVisitType.AllowDrop = True
        Me.txtVisitType.AutoCompleteCustomSource.AddRange(New String() {"IN", "OUT"})
        Me.txtVisitType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtVisitType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtVisitType.Location = New System.Drawing.Point(103, 139)
        Me.txtVisitType.Name = "txtVisitType"
        Me.txtVisitType.Size = New System.Drawing.Size(94, 20)
        Me.txtVisitType.TabIndex = 27
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(114, 113)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 13)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Visit Type"
        '
        'txtQtnType
        '
        Me.txtQtnType.AllowDrop = True
        Me.txtQtnType.AutoCompleteCustomSource.AddRange(New String() {"Rev", "New"})
        Me.txtQtnType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtQtnType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtQtnType.Location = New System.Drawing.Point(3, 139)
        Me.txtQtnType.Name = "txtQtnType"
        Me.txtQtnType.Size = New System.Drawing.Size(94, 20)
        Me.txtQtnType.TabIndex = 25
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(14, 113)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "QTN type"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rblOther)
        Me.GroupBox2.Controls.Add(Me.rblVisit)
        Me.GroupBox2.Controls.Add(Me.rblSalesExecutive)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(293, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(378, 44)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        '
        'rblOther
        '
        Me.rblOther.AutoSize = True
        Me.rblOther.Location = New System.Drawing.Point(258, 14)
        Me.rblOther.Name = "rblOther"
        Me.rblOther.Size = New System.Drawing.Size(62, 21)
        Me.rblOther.TabIndex = 12
        Me.rblOther.Text = "Other"
        Me.rblOther.UseVisualStyleBackColor = True
        '
        'rblVisit
        '
        Me.rblVisit.AutoSize = True
        Me.rblVisit.Location = New System.Drawing.Point(186, 14)
        Me.rblVisit.Name = "rblVisit"
        Me.rblVisit.Size = New System.Drawing.Size(52, 21)
        Me.rblVisit.TabIndex = 11
        Me.rblVisit.Text = "Visit"
        Me.rblVisit.UseVisualStyleBackColor = True
        '
        'rblSalesExecutive
        '
        Me.rblSalesExecutive.AutoSize = True
        Me.rblSalesExecutive.Checked = True
        Me.rblSalesExecutive.Location = New System.Drawing.Point(62, 14)
        Me.rblSalesExecutive.Name = "rblSalesExecutive"
        Me.rblSalesExecutive.Size = New System.Drawing.Size(118, 21)
        Me.rblSalesExecutive.TabIndex = 10
        Me.rblSalesExecutive.TabStop = True
        Me.rblSalesExecutive.Text = "SaleExecutive "
        Me.rblSalesExecutive.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(355, 257)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 37)
        Me.btnSearch.TabIndex = 17
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(456, 257)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 37)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DataGridView1)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 306)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1086, 341)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(6, 18)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1074, 319)
        Me.DataGridView1.TabIndex = 0
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(930, 277)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(75, 23)
        Me.btnExport.TabIndex = 20
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'txtAllToUser
        '
        Me.txtAllToUser.Location = New System.Drawing.Point(1011, 184)
        Me.txtAllToUser.Name = "txtAllToUser"
        Me.txtAllToUser.Size = New System.Drawing.Size(100, 20)
        Me.txtAllToUser.TabIndex = 21
        Me.txtAllToUser.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rblTo)
        Me.GroupBox4.Controls.Add(Me.rblFrom)
        Me.GroupBox4.Location = New System.Drawing.Point(761, 29)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(218, 31)
        Me.GroupBox4.TabIndex = 22
        Me.GroupBox4.TabStop = False
        '
        'rblTo
        '
        Me.rblTo.AutoSize = True
        Me.rblTo.Location = New System.Drawing.Point(115, 10)
        Me.rblTo.Name = "rblTo"
        Me.rblTo.Size = New System.Drawing.Size(40, 17)
        Me.rblTo.TabIndex = 1
        Me.rblTo.TabStop = True
        Me.rblTo.Text = "TO"
        Me.rblTo.UseVisualStyleBackColor = True
        '
        'rblFrom
        '
        Me.rblFrom.AutoSize = True
        Me.rblFrom.Checked = True
        Me.rblFrom.Location = New System.Drawing.Point(46, 10)
        Me.rblFrom.Name = "rblFrom"
        Me.rblFrom.Size = New System.Drawing.Size(56, 17)
        Me.rblFrom.TabIndex = 0
        Me.rblFrom.TabStop = True
        Me.rblFrom.Text = "FROM"
        Me.rblFrom.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rblToDate)
        Me.GroupBox5.Controls.Add(Me.rblFromDate)
        Me.GroupBox5.Location = New System.Drawing.Point(46, 25)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(218, 31)
        Me.GroupBox5.TabIndex = 23
        Me.GroupBox5.TabStop = False
        '
        'rblToDate
        '
        Me.rblToDate.AutoSize = True
        Me.rblToDate.Location = New System.Drawing.Point(115, 10)
        Me.rblToDate.Name = "rblToDate"
        Me.rblToDate.Size = New System.Drawing.Size(40, 17)
        Me.rblToDate.TabIndex = 1
        Me.rblToDate.TabStop = True
        Me.rblToDate.Text = "TO"
        Me.rblToDate.UseVisualStyleBackColor = True
        '
        'rblFromDate
        '
        Me.rblFromDate.AutoSize = True
        Me.rblFromDate.Checked = True
        Me.rblFromDate.Location = New System.Drawing.Point(46, 10)
        Me.rblFromDate.Name = "rblFromDate"
        Me.rblFromDate.Size = New System.Drawing.Size(56, 17)
        Me.rblFromDate.TabIndex = 0
        Me.rblFromDate.TabStop = True
        Me.rblFromDate.Text = "FROM"
        Me.rblFromDate.UseVisualStyleBackColor = True
        '
        'SalesExecutiveByCriteria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1096, 659)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.txtAllToUser)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "SalesExecutiveByCriteria"
        Me.Text = "SalesExecutiveByCriteria"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpUser.ResumeLayout(False)
        Me.grpUser.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rblOther As System.Windows.Forms.RadioButton
    Friend WithEvents rblVisit As System.Windows.Forms.RadioButton
    Friend WithEvents rblSalesExecutive As System.Windows.Forms.RadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtAllQuotationType As System.Windows.Forms.TextBox
    Friend WithEvents BtnAddQuotationtype As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ddlQuotationType As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnAddEnqType As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ddlEnqType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtALLEnqType As System.Windows.Forms.TextBox
    Friend WithEvents grpUser As System.Windows.Forms.Panel
    Friend WithEvents txtAllUser As System.Windows.Forms.TextBox
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents txtQtnType As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtPriority As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents txtVisitType As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtThrough As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAllToUser As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rblFrom As System.Windows.Forms.RadioButton
    Friend WithEvents rblTo As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rblToDate As System.Windows.Forms.RadioButton
    Friend WithEvents rblFromDate As System.Windows.Forms.RadioButton
End Class
