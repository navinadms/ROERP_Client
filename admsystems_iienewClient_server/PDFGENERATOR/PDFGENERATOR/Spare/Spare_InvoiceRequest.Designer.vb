<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Spare_InvoiceRequest
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
        Me.ddlSearchStatus = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GvSpareInvoiceList = New System.Windows.Forms.DataGridView()
        Me.txtSearchInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearchName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtSearchEnqno = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblOrignalRate = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.txtGstNo = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtTinNo = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.dtDoneDate = New System.Windows.Forms.DateTimePicker()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.ddlMainEngineer = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtFinalRate = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtTallyInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblGST = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtPincode = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtTaluka = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.DtInvoice = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GvSpareInvoiceDetail = New System.Windows.Forms.DataGridView()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ddlItem = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ddlCategory = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ddlStatus = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEnqNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GvSpareInvoiceList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GvSpareInvoiceDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ddlSearchStatus)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.GvSpareInvoiceList)
        Me.GroupBox1.Controls.Add(Me.txtSearchInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.txtSearchName)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtSearchEnqno)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(248, 613)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'ddlSearchStatus
        '
        Me.ddlSearchStatus.FormattingEnabled = True
        Me.ddlSearchStatus.Location = New System.Drawing.Point(58, 171)
        Me.ddlSearchStatus.Name = "ddlSearchStatus"
        Me.ddlSearchStatus.Size = New System.Drawing.Size(162, 21)
        Me.ddlSearchStatus.TabIndex = 84
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 174)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(37, 13)
        Me.Label28.TabIndex = 83
        Me.Label28.Text = "Status"
        '
        'GvSpareInvoiceList
        '
        Me.GvSpareInvoiceList.AllowDrop = True
        Me.GvSpareInvoiceList.AllowUserToAddRows = False
        Me.GvSpareInvoiceList.AllowUserToDeleteRows = False
        Me.GvSpareInvoiceList.AllowUserToOrderColumns = True
        Me.GvSpareInvoiceList.AllowUserToResizeColumns = False
        Me.GvSpareInvoiceList.AllowUserToResizeRows = False
        Me.GvSpareInvoiceList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvSpareInvoiceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvSpareInvoiceList.Location = New System.Drawing.Point(6, 221)
        Me.GvSpareInvoiceList.Name = "GvSpareInvoiceList"
        Me.GvSpareInvoiceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvSpareInvoiceList.Size = New System.Drawing.Size(234, 364)
        Me.GvSpareInvoiceList.TabIndex = 15
        '
        'txtSearchInvoiceNo
        '
        Me.txtSearchInvoiceNo.Location = New System.Drawing.Point(90, 49)
        Me.txtSearchInvoiceNo.Name = "txtSearchInvoiceNo"
        Me.txtSearchInvoiceNo.Size = New System.Drawing.Size(130, 20)
        Me.txtSearchInvoiceNo.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Invoice No"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(145, 115)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 35)
        Me.btnRefresh.TabIndex = 12
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(39, 116)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 34)
        Me.btnSearch.TabIndex = 11
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearchName
        '
        Me.txtSearchName.Location = New System.Drawing.Point(90, 79)
        Me.txtSearchName.Name = "txtSearchName"
        Me.txtSearchName.Size = New System.Drawing.Size(130, 20)
        Me.txtSearchName.TabIndex = 10
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(23, 81)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(35, 13)
        Me.Label16.TabIndex = 9
        Me.Label16.Text = "Name"
        '
        'txtSearchEnqno
        '
        Me.txtSearchEnqno.Location = New System.Drawing.Point(90, 18)
        Me.txtSearchEnqno.Name = "txtSearchEnqno"
        Me.txtSearchEnqno.Size = New System.Drawing.Size(130, 20)
        Me.txtSearchEnqno.TabIndex = 8
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(20, 19)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(43, 13)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "Enq No"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtAddress)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.lblOrignalRate)
        Me.GroupBox2.Controls.Add(Me.btnPrint)
        Me.GroupBox2.Controls.Add(Me.txtGstNo)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.txtTinNo)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.btnAddNew)
        Me.GroupBox2.Controls.Add(Me.dtDoneDate)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.txtRemarks)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.btnSubmit)
        Me.GroupBox2.Controls.Add(Me.ddlMainEngineer)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.txtFinalRate)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.txtTallyInvoiceNo)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.lblGST)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.txtPincode)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.txtState)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.txtTaluka)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.txtCity)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtTotalAmount)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.DtInvoice)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtGSTAmount)
        Me.GroupBox2.Controls.Add(Me.txtQty)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtRate)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.GvSpareInvoiceDetail)
        Me.GroupBox2.Controls.Add(Me.btnAdd)
        Me.GroupBox2.Controls.Add(Me.txtPrice)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtUnit)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.ddlItem)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.ddlCategory)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.ddlStatus)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtPartyName)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtEnqNo)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(266, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(827, 613)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'txtAddress
        '
        Me.txtAddress.Enabled = False
        Me.txtAddress.Location = New System.Drawing.Point(56, 61)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(374, 36)
        Me.txtAddress.TabIndex = 85
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(6, 69)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(45, 13)
        Me.Label29.TabIndex = 84
        Me.Label29.Text = "Address"
        '
        'lblOrignalRate
        '
        Me.lblOrignalRate.AutoSize = True
        Me.lblOrignalRate.Location = New System.Drawing.Point(248, 299)
        Me.lblOrignalRate.Name = "lblOrignalRate"
        Me.lblOrignalRate.Size = New System.Drawing.Size(13, 13)
        Me.lblOrignalRate.TabIndex = 83
        Me.lblOrignalRate.Text = "0"
        Me.lblOrignalRate.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(376, 567)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 33)
        Me.btnPrint.TabIndex = 82
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'txtGstNo
        '
        Me.txtGstNo.Location = New System.Drawing.Point(514, 127)
        Me.txtGstNo.Name = "txtGstNo"
        Me.txtGstNo.Size = New System.Drawing.Size(280, 20)
        Me.txtGstNo.TabIndex = 81
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(456, 131)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(46, 13)
        Me.Label27.TabIndex = 80
        Me.Label27.Text = "GST No"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTinNo
        '
        Me.txtTinNo.Location = New System.Drawing.Point(514, 100)
        Me.txtTinNo.Name = "txtTinNo"
        Me.txtTinNo.Size = New System.Drawing.Size(280, 20)
        Me.txtTinNo.TabIndex = 79
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(456, 104)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(36, 13)
        Me.Label26.TabIndex = 78
        Me.Label26.Text = "TinNo"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAddNew
        '
        Me.btnAddNew.Location = New System.Drawing.Point(243, 567)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(75, 33)
        Me.btnAddNew.TabIndex = 77
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.UseVisualStyleBackColor = True
        '
        'dtDoneDate
        '
        Me.dtDoneDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDoneDate.Location = New System.Drawing.Point(315, 192)
        Me.dtDoneDate.Name = "dtDoneDate"
        Me.dtDoneDate.Size = New System.Drawing.Size(100, 20)
        Me.dtDoneDate.TabIndex = 76
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(245, 192)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(59, 13)
        Me.Label25.TabIndex = 75
        Me.Label25.Text = "Done Date"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(61, 186)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(174, 37)
        Me.txtRemarks.TabIndex = 74
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 186)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(49, 13)
        Me.Label24.TabIndex = 73
        Me.Label24.Text = "Remarks"
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(431, 186)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(58, 31)
        Me.btnSubmit.TabIndex = 72
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'ddlMainEngineer
        '
        Me.ddlMainEngineer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlMainEngineer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlMainEngineer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlMainEngineer.FormattingEnabled = True
        Me.ddlMainEngineer.Location = New System.Drawing.Point(514, 157)
        Me.ddlMainEngineer.Name = "ddlMainEngineer"
        Me.ddlMainEngineer.Size = New System.Drawing.Size(280, 21)
        Me.ddlMainEngineer.TabIndex = 71
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(456, 160)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(52, 13)
        Me.Label23.TabIndex = 70
        Me.Label23.Text = "Engineer "
        '
        'txtFinalRate
        '
        Me.txtFinalRate.Enabled = False
        Me.txtFinalRate.Location = New System.Drawing.Point(522, 293)
        Me.txtFinalRate.Name = "txtFinalRate"
        Me.txtFinalRate.Size = New System.Drawing.Size(72, 20)
        Me.txtFinalRate.TabIndex = 68
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(526, 273)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(55, 13)
        Me.Label22.TabIndex = 69
        Me.Label22.Text = "Final Rate"
        '
        'txtTallyInvoiceNo
        '
        Me.txtTallyInvoiceNo.Location = New System.Drawing.Point(307, 153)
        Me.txtTallyInvoiceNo.Name = "txtTallyInvoiceNo"
        Me.txtTallyInvoiceNo.Size = New System.Drawing.Size(128, 20)
        Me.txtTallyInvoiceNo.TabIndex = 67
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(220, 155)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 13)
        Me.Label3.TabIndex = 66
        Me.Label3.Text = "Tally Invoice No"
        '
        'lblGST
        '
        Me.lblGST.AutoSize = True
        Me.lblGST.Location = New System.Drawing.Point(596, 273)
        Me.lblGST.Name = "lblGST"
        Me.lblGST.Size = New System.Drawing.Size(54, 13)
        Me.lblGST.TabIndex = 65
        Me.lblGST.Text = " GST_Per"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(600, 247)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(43, 13)
        Me.Label21.TabIndex = 64
        Me.Label21.Text = "GST(%)"
        '
        'txtPincode
        '
        Me.txtPincode.Enabled = False
        Me.txtPincode.Location = New System.Drawing.Point(306, 127)
        Me.txtPincode.Name = "txtPincode"
        Me.txtPincode.Size = New System.Drawing.Size(129, 20)
        Me.txtPincode.TabIndex = 63
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(240, 133)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(49, 13)
        Me.Label20.TabIndex = 62
        Me.Label20.Text = "Pincode "
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtState
        '
        Me.txtState.Enabled = False
        Me.txtState.Location = New System.Drawing.Point(56, 130)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(136, 20)
        Me.txtState.TabIndex = 61
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(7, 130)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(32, 13)
        Me.Label19.TabIndex = 60
        Me.Label19.Text = "State"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTaluka
        '
        Me.txtTaluka.Enabled = False
        Me.txtTaluka.Location = New System.Drawing.Point(306, 102)
        Me.txtTaluka.Name = "txtTaluka"
        Me.txtTaluka.Size = New System.Drawing.Size(129, 20)
        Me.txtTaluka.TabIndex = 59
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(241, 107)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(40, 13)
        Me.Label15.TabIndex = 58
        Me.Label15.Text = "Taluka"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCity
        '
        Me.txtCity.Enabled = False
        Me.txtCity.Location = New System.Drawing.Point(56, 103)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(136, 20)
        Me.txtCity.TabIndex = 57
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(11, 104)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(24, 13)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "Ctiy"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.Enabled = False
        Me.txtTotalAmount.Location = New System.Drawing.Point(694, 551)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(118, 20)
        Me.txtTotalAmount.TabIndex = 55
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(612, 554)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(70, 13)
        Me.Label18.TabIndex = 54
        Me.Label18.Text = "Total Amount"
        '
        'DtInvoice
        '
        Me.DtInvoice.Enabled = False
        Me.DtInvoice.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtInvoice.Location = New System.Drawing.Point(712, 11)
        Me.DtInvoice.Name = "DtInvoice"
        Me.DtInvoice.Size = New System.Drawing.Size(100, 20)
        Me.DtInvoice.TabIndex = 53
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(636, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Invoice Date"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(244, 12)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(191, 20)
        Me.txtInvoiceNo.TabIndex = 51
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(179, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Invoice No"
        '
        'txtGSTAmount
        '
        Me.txtGSTAmount.Enabled = False
        Me.txtGSTAmount.Location = New System.Drawing.Point(599, 293)
        Me.txtGSTAmount.Name = "txtGSTAmount"
        Me.txtGSTAmount.Size = New System.Drawing.Size(60, 20)
        Me.txtGSTAmount.TabIndex = 49
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(420, 293)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(42, 20)
        Me.txtQty.TabIndex = 35
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(479, 273)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 13)
        Me.Label12.TabIndex = 42
        Me.Label12.Text = "Unit"
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(329, 293)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(86, 20)
        Me.txtRate.TabIndex = 31
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(359, 273)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 13)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "Rate"
        '
        'GvSpareInvoiceDetail
        '
        Me.GvSpareInvoiceDetail.AllowDrop = True
        Me.GvSpareInvoiceDetail.AllowUserToAddRows = False
        Me.GvSpareInvoiceDetail.AllowUserToDeleteRows = False
        Me.GvSpareInvoiceDetail.AllowUserToOrderColumns = True
        Me.GvSpareInvoiceDetail.AllowUserToResizeColumns = False
        Me.GvSpareInvoiceDetail.AllowUserToResizeRows = False
        Me.GvSpareInvoiceDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvSpareInvoiceDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvSpareInvoiceDetail.Location = New System.Drawing.Point(5, 324)
        Me.GvSpareInvoiceDetail.Name = "GvSpareInvoiceDetail"
        Me.GvSpareInvoiceDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvSpareInvoiceDetail.Size = New System.Drawing.Size(807, 218)
        Me.GvSpareInvoiceDetail.TabIndex = 40
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(763, 284)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(58, 31)
        Me.btnAdd.TabIndex = 39
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtPrice
        '
        Me.txtPrice.Enabled = False
        Me.txtPrice.Location = New System.Drawing.Point(665, 293)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(92, 20)
        Me.txtPrice.TabIndex = 38
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(709, 265)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 13)
        Me.Label10.TabIndex = 36
        Me.Label10.Text = "Price"
        '
        'txtUnit
        '
        Me.txtUnit.Enabled = False
        Me.txtUnit.Location = New System.Drawing.Point(468, 293)
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.Size = New System.Drawing.Size(50, 20)
        Me.txtUnit.TabIndex = 37
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(428, 276)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 13)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "Qty"
        '
        'ddlItem
        '
        Me.ddlItem.AllowDrop = True
        Me.ddlItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlItem.FormattingEnabled = True
        Me.ddlItem.Location = New System.Drawing.Point(244, 247)
        Me.ddlItem.Name = "ddlItem"
        Me.ddlItem.Size = New System.Drawing.Size(326, 21)
        Me.ddlItem.TabIndex = 29
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(362, 224)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 13)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Item"
        '
        'ddlCategory
        '
        Me.ddlCategory.AllowDrop = True
        Me.ddlCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlCategory.FormattingEnabled = True
        Me.ddlCategory.Location = New System.Drawing.Point(10, 247)
        Me.ddlCategory.Name = "ddlCategory"
        Me.ddlCategory.Size = New System.Drawing.Size(228, 21)
        Me.ddlCategory.TabIndex = 28
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(70, 228)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Category"
        '
        'ddlStatus
        '
        Me.ddlStatus.FormattingEnabled = True
        Me.ddlStatus.Location = New System.Drawing.Point(56, 155)
        Me.ddlStatus.Name = "ddlStatus"
        Me.ddlStatus.Size = New System.Drawing.Size(136, 21)
        Me.ddlStatus.TabIndex = 26
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(7, 153)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(37, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "Status"
        '
        'txtPartyName
        '
        Me.txtPartyName.Location = New System.Drawing.Point(56, 39)
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.Size = New System.Drawing.Size(379, 20)
        Me.txtPartyName.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtEnqNo
        '
        Me.txtEnqNo.Location = New System.Drawing.Point(56, 11)
        Me.txtEnqNo.Name = "txtEnqNo"
        Me.txtEnqNo.Size = New System.Drawing.Size(100, 20)
        Me.txtEnqNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Enq No"
        '
        'Spare_InvoiceRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1105, 648)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Spare_InvoiceRequest"
        Me.Text = "Spare_InvoiceRequest"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GvSpareInvoiceList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.GvSpareInvoiceDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEnqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPartyName As System.Windows.Forms.TextBox
    Friend WithEvents ddlStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GvSpareInvoiceDetail As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ddlItem As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ddlCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtGSTAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtInvoice As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSearchInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearchName As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtSearchEnqno As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GvSpareInvoiceList As System.Windows.Forms.DataGridView
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTaluka As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtPincode As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblGST As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtTallyInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFinalRate As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents ddlMainEngineer As System.Windows.Forms.ComboBox
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents dtDoneDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnAddNew As System.Windows.Forms.Button
    Friend WithEvents txtTinNo As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtGstNo As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents ddlSearchStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblOrignalRate As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
End Class
