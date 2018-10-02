<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ODSiteAllotment1
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
        Me.txtpriorityno = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtPlantType = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rblVisit = New System.Windows.Forms.RadioButton()
        Me.rblType_EC = New System.Windows.Forms.RadioButton()
        Me.rblType_Service = New System.Windows.Forms.RadioButton()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtStation = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ddlSiteStatus = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnDeleteEngineer = New System.Windows.Forms.Button()
        Me.GvSiteEngineerList = New System.Windows.Forms.DataGridView()
        Me.btnAddSiteEngg = New System.Windows.Forms.Button()
        Me.ddlEnggStatus = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ddlEngineerList = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCreateDate = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDownTime = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtUpTime = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNoofDays = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEnqNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rblSearchVisit = New System.Windows.Forms.RadioButton()
        Me.rblSearchEC = New System.Windows.Forms.RadioButton()
        Me.rblSearchService = New System.Windows.Forms.RadioButton()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkEngineer = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rblSearchAll = New System.Windows.Forms.RadioButton()
        Me.rblSearchSCH = New System.Windows.Forms.RadioButton()
        Me.rblSearchPending = New System.Windows.Forms.RadioButton()
        Me.rblCancelSearch = New System.Windows.Forms.RadioButton()
        Me.rblDoneSearch = New System.Windows.Forms.RadioButton()
        Me.rblRunningSearch = New System.Windows.Forms.RadioButton()
        Me.btnSearchRefresh = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNameSearch = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtEnqNoSearch = New System.Windows.Forms.TextBox()
        Me.GvODSiteList = New System.Windows.Forms.DataGridView()
        Me.DirectoryEntry1 = New System.DirectoryServices.DirectoryEntry()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnODEngineerReport = New System.Windows.Forms.Button()
        Me.rblSearchtypeAll = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.GvSiteEngineerList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.GvODSiteList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtpriorityno)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtPlantType)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtStation)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.ddlSiteStatus)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtRemark)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.btnAddSiteEngg)
        Me.GroupBox1.Controls.Add(Me.ddlEnggStatus)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.ddlEngineerList)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtEndDate)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtStartDate)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCreateDate)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtDownTime)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtUpTime)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtNoofDays)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtEnqNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(293, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(692, 557)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtpriorityno
        '
        Me.txtpriorityno.Location = New System.Drawing.Point(475, 268)
        Me.txtpriorityno.Name = "txtpriorityno"
        Me.txtpriorityno.Size = New System.Drawing.Size(67, 20)
        Me.txtpriorityno.TabIndex = 43
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(489, 245)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(38, 13)
        Me.Label20.TabIndex = 42
        Me.Label20.Text = "Priority"
        '
        'txtPlantType
        '
        Me.txtPlantType.Location = New System.Drawing.Point(67, 83)
        Me.txtPlantType.Name = "txtPlantType"
        Me.txtPlantType.Size = New System.Drawing.Size(184, 20)
        Me.txtPlantType.TabIndex = 41
        Me.txtPlantType.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(4, 83)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(58, 13)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Plant Type"
        Me.Label18.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rblVisit)
        Me.GroupBox4.Controls.Add(Me.rblType_EC)
        Me.GroupBox4.Controls.Add(Me.rblType_Service)
        Me.GroupBox4.Location = New System.Drawing.Point(232, 9)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(219, 29)
        Me.GroupBox4.TabIndex = 39
        Me.GroupBox4.TabStop = False
        '
        'rblVisit
        '
        Me.rblVisit.AutoSize = True
        Me.rblVisit.Location = New System.Drawing.Point(147, 9)
        Me.rblVisit.Name = "rblVisit"
        Me.rblVisit.Size = New System.Drawing.Size(44, 17)
        Me.rblVisit.TabIndex = 2
        Me.rblVisit.TabStop = True
        Me.rblVisit.Text = "Visit"
        Me.rblVisit.UseVisualStyleBackColor = True
        '
        'rblType_EC
        '
        Me.rblType_EC.AutoSize = True
        Me.rblType_EC.Location = New System.Drawing.Point(87, 9)
        Me.rblType_EC.Name = "rblType_EC"
        Me.rblType_EC.Size = New System.Drawing.Size(39, 17)
        Me.rblType_EC.TabIndex = 1
        Me.rblType_EC.TabStop = True
        Me.rblType_EC.Text = "EC"
        Me.rblType_EC.UseVisualStyleBackColor = True
        '
        'rblType_Service
        '
        Me.rblType_Service.AutoSize = True
        Me.rblType_Service.Location = New System.Drawing.Point(20, 9)
        Me.rblType_Service.Name = "rblType_Service"
        Me.rblType_Service.Size = New System.Drawing.Size(61, 17)
        Me.rblType_Service.TabIndex = 0
        Me.rblType_Service.TabStop = True
        Me.rblType_Service.Text = "Service"
        Me.rblType_Service.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(190, 19)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(36, 13)
        Me.Label17.TabIndex = 38
        Me.Label17.Text = "Types"
        '
        'txtStation
        '
        Me.txtStation.Location = New System.Drawing.Point(518, 46)
        Me.txtStation.Name = "txtStation"
        Me.txtStation.Size = New System.Drawing.Size(168, 20)
        Me.txtStation.TabIndex = 37
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(472, 49)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(40, 13)
        Me.Label16.TabIndex = 36
        Me.Label16.Text = "Station"
        '
        'ddlSiteStatus
        '
        Me.ddlSiteStatus.FormattingEnabled = True
        Me.ddlSiteStatus.Items.AddRange(New Object() {"Pending", "SCH", "Running", "Done", "Cancel"})
        Me.ddlSiteStatus.Location = New System.Drawing.Point(505, 114)
        Me.ddlSiteStatus.Name = "ddlSiteStatus"
        Me.ddlSiteStatus.Size = New System.Drawing.Size(117, 21)
        Me.ddlSiteStatus.TabIndex = 35
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(439, 119)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(58, 13)
        Me.Label15.TabIndex = 34
        Me.Label15.Text = "Site Status"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(412, 163)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(244, 47)
        Me.txtRemark.TabIndex = 33
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(357, 164)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(44, 13)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "Remark"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnDeleteEngineer)
        Me.GroupBox3.Controls.Add(Me.GvSiteEngineerList)
        Me.GroupBox3.Location = New System.Drawing.Point(7, 295)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(662, 254)
        Me.GroupBox3.TabIndex = 31
        Me.GroupBox3.TabStop = False
        '
        'btnDeleteEngineer
        '
        Me.btnDeleteEngineer.Location = New System.Drawing.Point(565, 13)
        Me.btnDeleteEngineer.Name = "btnDeleteEngineer"
        Me.btnDeleteEngineer.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteEngineer.TabIndex = 1
        Me.btnDeleteEngineer.Text = "Delete"
        Me.btnDeleteEngineer.UseVisualStyleBackColor = True
        '
        'GvSiteEngineerList
        '
        Me.GvSiteEngineerList.AllowDrop = True
        Me.GvSiteEngineerList.AllowUserToAddRows = False
        Me.GvSiteEngineerList.AllowUserToDeleteRows = False
        Me.GvSiteEngineerList.AllowUserToOrderColumns = True
        Me.GvSiteEngineerList.AllowUserToResizeColumns = False
        Me.GvSiteEngineerList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvSiteEngineerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvSiteEngineerList.Location = New System.Drawing.Point(7, 42)
        Me.GvSiteEngineerList.Name = "GvSiteEngineerList"
        Me.GvSiteEngineerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvSiteEngineerList.Size = New System.Drawing.Size(649, 206)
        Me.GvSiteEngineerList.TabIndex = 0
        '
        'btnAddSiteEngg
        '
        Me.btnAddSiteEngg.Location = New System.Drawing.Point(572, 259)
        Me.btnAddSiteEngg.Name = "btnAddSiteEngg"
        Me.btnAddSiteEngg.Size = New System.Drawing.Size(75, 31)
        Me.btnAddSiteEngg.TabIndex = 30
        Me.btnAddSiteEngg.Text = "Add"
        Me.btnAddSiteEngg.UseVisualStyleBackColor = True
        '
        'ddlEnggStatus
        '
        Me.ddlEnggStatus.FormattingEnabled = True
        Me.ddlEnggStatus.Items.AddRange(New Object() {"Yes", "No"})
        Me.ddlEnggStatus.Location = New System.Drawing.Point(341, 268)
        Me.ddlEnggStatus.Name = "ddlEnggStatus"
        Me.ddlEnggStatus.Size = New System.Drawing.Size(104, 21)
        Me.ddlEnggStatus.TabIndex = 29
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(357, 245)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 13)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "Engg Status"
        '
        'ddlEngineerList
        '
        Me.ddlEngineerList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ddlEngineerList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ddlEngineerList.FormattingEnabled = True
        Me.ddlEngineerList.Location = New System.Drawing.Point(23, 268)
        Me.ddlEngineerList.Name = "ddlEngineerList"
        Me.ddlEngineerList.Size = New System.Drawing.Size(284, 21)
        Me.ddlEngineerList.TabIndex = 27
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(118, 245)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(49, 13)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Engineer"
        '
        'txtEndDate
        '
        Me.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtEndDate.Location = New System.Drawing.Point(232, 163)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(105, 20)
        Me.txtEndDate.TabIndex = 23
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(178, 166)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 13)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "End Date"
        '
        'txtStartDate
        '
        Me.txtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtStartDate.Location = New System.Drawing.Point(67, 163)
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.Size = New System.Drawing.Size(105, 20)
        Me.txtStartDate.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 166)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 13)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Start Date"
        '
        'txtCreateDate
        '
        Me.txtCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtCreateDate.Location = New System.Drawing.Point(581, 15)
        Me.txtCreateDate.Name = "txtCreateDate"
        Me.txtCreateDate.Size = New System.Drawing.Size(105, 20)
        Me.txtCreateDate.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(545, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Date"
        '
        'txtDownTime
        '
        Me.txtDownTime.Location = New System.Drawing.Point(360, 116)
        Me.txtDownTime.Name = "txtDownTime"
        Me.txtDownTime.Size = New System.Drawing.Size(53, 20)
        Me.txtDownTime.TabIndex = 17
        Me.txtDownTime.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(293, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Down Time"
        '
        'txtUpTime
        '
        Me.txtUpTime.Location = New System.Drawing.Point(212, 116)
        Me.txtUpTime.Name = "txtUpTime"
        Me.txtUpTime.Size = New System.Drawing.Size(75, 20)
        Me.txtUpTime.TabIndex = 15
        Me.txtUpTime.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(158, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Up Time"
        '
        'txtNoofDays
        '
        Me.txtNoofDays.Location = New System.Drawing.Point(77, 116)
        Me.txtNoofDays.Name = "txtNoofDays"
        Me.txtNoofDays.Size = New System.Drawing.Size(75, 20)
        Me.txtNoofDays.TabIndex = 13
        Me.txtNoofDays.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "No of Days"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(67, 46)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(399, 20)
        Me.txtName.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Name"
        '
        'txtEnqNo
        '
        Me.txtEnqNo.Location = New System.Drawing.Point(67, 16)
        Me.txtEnqNo.Name = "txtEnqNo"
        Me.txtEnqNo.Size = New System.Drawing.Size(100, 20)
        Me.txtEnqNo.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "EnqNo"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.txtTotal)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.chkEngineer)
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.btnSearchRefresh)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtNameSearch)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtEnqNoSearch)
        Me.GroupBox2.Controls.Add(Me.GvODSiteList)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(275, 604)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.rblSearchtypeAll)
        Me.GroupBox6.Controls.Add(Me.rblSearchVisit)
        Me.GroupBox6.Controls.Add(Me.rblSearchEC)
        Me.GroupBox6.Controls.Add(Me.rblSearchService)
        Me.GroupBox6.Location = New System.Drawing.Point(44, 150)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(225, 29)
        Me.GroupBox6.TabIndex = 43
        Me.GroupBox6.TabStop = False
        '
        'rblSearchVisit
        '
        Me.rblSearchVisit.AutoSize = True
        Me.rblSearchVisit.Location = New System.Drawing.Point(122, 9)
        Me.rblSearchVisit.Name = "rblSearchVisit"
        Me.rblSearchVisit.Size = New System.Drawing.Size(44, 17)
        Me.rblSearchVisit.TabIndex = 2
        Me.rblSearchVisit.Text = "Visit"
        Me.rblSearchVisit.UseVisualStyleBackColor = True
        '
        'rblSearchEC
        '
        Me.rblSearchEC.AutoSize = True
        Me.rblSearchEC.Location = New System.Drawing.Point(70, 9)
        Me.rblSearchEC.Name = "rblSearchEC"
        Me.rblSearchEC.Size = New System.Drawing.Size(39, 17)
        Me.rblSearchEC.TabIndex = 1
        Me.rblSearchEC.Text = "EC"
        Me.rblSearchEC.UseVisualStyleBackColor = True
        '
        'rblSearchService
        '
        Me.rblSearchService.AutoSize = True
        Me.rblSearchService.Location = New System.Drawing.Point(9, 8)
        Me.rblSearchService.Name = "rblSearchService"
        Me.rblSearchService.Size = New System.Drawing.Size(61, 17)
        Me.rblSearchService.TabIndex = 0
        Me.rblSearchService.Text = "Service"
        Me.rblSearchService.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(5, 161)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(36, 13)
        Me.Label19.TabIndex = 42
        Me.Label19.Text = "Types"
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(87, 568)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(64, 20)
        Me.txtTotal.TabIndex = 40
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(40, 571)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Total"
        '
        'chkEngineer
        '
        Me.chkEngineer.AutoSize = True
        Me.chkEngineer.Location = New System.Drawing.Point(99, 53)
        Me.chkEngineer.Name = "chkEngineer"
        Me.chkEngineer.Size = New System.Drawing.Size(68, 17)
        Me.chkEngineer.TabIndex = 38
        Me.chkEngineer.Text = "Engineer"
        Me.chkEngineer.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rblSearchAll)
        Me.GroupBox5.Controls.Add(Me.rblSearchSCH)
        Me.GroupBox5.Controls.Add(Me.rblSearchPending)
        Me.GroupBox5.Controls.Add(Me.rblCancelSearch)
        Me.GroupBox5.Controls.Add(Me.rblDoneSearch)
        Me.GroupBox5.Controls.Add(Me.rblRunningSearch)
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(9, 185)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(252, 63)
        Me.GroupBox5.TabIndex = 37
        Me.GroupBox5.TabStop = False
        '
        'rblSearchAll
        '
        Me.rblSearchAll.AutoSize = True
        Me.rblSearchAll.Checked = True
        Me.rblSearchAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rblSearchAll.Location = New System.Drawing.Point(156, 35)
        Me.rblSearchAll.Name = "rblSearchAll"
        Me.rblSearchAll.Size = New System.Drawing.Size(38, 19)
        Me.rblSearchAll.TabIndex = 5
        Me.rblSearchAll.TabStop = True
        Me.rblSearchAll.Text = "All"
        Me.rblSearchAll.UseVisualStyleBackColor = True
        '
        'rblSearchSCH
        '
        Me.rblSearchSCH.AutoSize = True
        Me.rblSearchSCH.Location = New System.Drawing.Point(90, 10)
        Me.rblSearchSCH.Name = "rblSearchSCH"
        Me.rblSearchSCH.Size = New System.Drawing.Size(50, 19)
        Me.rblSearchSCH.TabIndex = 4
        Me.rblSearchSCH.Text = "SCH"
        Me.rblSearchSCH.UseVisualStyleBackColor = True
        '
        'rblSearchPending
        '
        Me.rblSearchPending.AutoSize = True
        Me.rblSearchPending.Location = New System.Drawing.Point(14, 8)
        Me.rblSearchPending.Name = "rblSearchPending"
        Me.rblSearchPending.Size = New System.Drawing.Size(71, 19)
        Me.rblSearchPending.TabIndex = 3
        Me.rblSearchPending.Text = "Pending"
        Me.rblSearchPending.UseVisualStyleBackColor = True
        '
        'rblCancelSearch
        '
        Me.rblCancelSearch.AutoSize = True
        Me.rblCancelSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rblCancelSearch.Location = New System.Drawing.Point(87, 35)
        Me.rblCancelSearch.Name = "rblCancelSearch"
        Me.rblCancelSearch.Size = New System.Drawing.Size(63, 19)
        Me.rblCancelSearch.TabIndex = 2
        Me.rblCancelSearch.Text = "Cancel"
        Me.rblCancelSearch.UseVisualStyleBackColor = True
        '
        'rblDoneSearch
        '
        Me.rblDoneSearch.AutoSize = True
        Me.rblDoneSearch.Location = New System.Drawing.Point(15, 35)
        Me.rblDoneSearch.Name = "rblDoneSearch"
        Me.rblDoneSearch.Size = New System.Drawing.Size(55, 19)
        Me.rblDoneSearch.TabIndex = 1
        Me.rblDoneSearch.Text = "Done"
        Me.rblDoneSearch.UseVisualStyleBackColor = True
        '
        'rblRunningSearch
        '
        Me.rblRunningSearch.AutoSize = True
        Me.rblRunningSearch.Location = New System.Drawing.Point(151, 9)
        Me.rblRunningSearch.Name = "rblRunningSearch"
        Me.rblRunningSearch.Size = New System.Drawing.Size(72, 19)
        Me.rblRunningSearch.TabIndex = 0
        Me.rblRunningSearch.Text = "Running"
        Me.rblRunningSearch.UseVisualStyleBackColor = True
        '
        'btnSearchRefresh
        '
        Me.btnSearchRefresh.Location = New System.Drawing.Point(150, 115)
        Me.btnSearchRefresh.Name = "btnSearchRefresh"
        Me.btnSearchRefresh.Size = New System.Drawing.Size(83, 30)
        Me.btnSearchRefresh.TabIndex = 36
        Me.btnSearchRefresh.Text = "Refresh"
        Me.btnSearchRefresh.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(61, 114)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(83, 32)
        Me.btnSearch.TabIndex = 35
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(20, 80)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(35, 13)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "Name"
        '
        'txtNameSearch
        '
        Me.txtNameSearch.Location = New System.Drawing.Point(70, 77)
        Me.txtNameSearch.Name = "txtNameSearch"
        Me.txtNameSearch.Size = New System.Drawing.Size(179, 20)
        Me.txtNameSearch.TabIndex = 33
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(22, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 13)
        Me.Label12.TabIndex = 32
        Me.Label12.Text = "EnqNo"
        '
        'txtEnqNoSearch
        '
        Me.txtEnqNoSearch.Location = New System.Drawing.Point(70, 19)
        Me.txtEnqNoSearch.Name = "txtEnqNoSearch"
        Me.txtEnqNoSearch.Size = New System.Drawing.Size(179, 20)
        Me.txtEnqNoSearch.TabIndex = 1
        '
        'GvODSiteList
        '
        Me.GvODSiteList.AllowDrop = True
        Me.GvODSiteList.AllowUserToAddRows = False
        Me.GvODSiteList.AllowUserToDeleteRows = False
        Me.GvODSiteList.AllowUserToOrderColumns = True
        Me.GvODSiteList.AllowUserToResizeColumns = False
        Me.GvODSiteList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvODSiteList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvODSiteList.Location = New System.Drawing.Point(9, 264)
        Me.GvODSiteList.Name = "GvODSiteList"
        Me.GvODSiteList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvODSiteList.Size = New System.Drawing.Size(254, 293)
        Me.GvODSiteList.TabIndex = 0
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(434, 583)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(83, 42)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(554, 583)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(83, 42)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnODEngineerReport
        '
        Me.btnODEngineerReport.Location = New System.Drawing.Point(848, 584)
        Me.btnODEngineerReport.Name = "btnODEngineerReport"
        Me.btnODEngineerReport.Size = New System.Drawing.Size(114, 41)
        Me.btnODEngineerReport.TabIndex = 4
        Me.btnODEngineerReport.Text = "OD Engineer Report"
        Me.btnODEngineerReport.UseVisualStyleBackColor = True
        '
        'rblSearchtypeAll
        '
        Me.rblSearchtypeAll.AutoSize = True
        Me.rblSearchtypeAll.Checked = True
        Me.rblSearchtypeAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rblSearchtypeAll.Location = New System.Drawing.Point(172, 8)
        Me.rblSearchtypeAll.Name = "rblSearchtypeAll"
        Me.rblSearchtypeAll.Size = New System.Drawing.Size(38, 19)
        Me.rblSearchtypeAll.TabIndex = 6
        Me.rblSearchtypeAll.TabStop = True
        Me.rblSearchtypeAll.Text = "All"
        Me.rblSearchtypeAll.UseVisualStyleBackColor = True
        '
        'ODSiteAllotment1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(997, 637)
        Me.Controls.Add(Me.btnODEngineerReport)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ODSiteAllotment1"
        Me.Text = "ODSiteAllotment1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.GvSiteEngineerList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.GvODSiteList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GvODSiteList As System.Windows.Forms.DataGridView
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEnqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNoofDays As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUpTime As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDownTime As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DirectoryEntry1 As System.DirectoryServices.DirectoryEntry
    Friend WithEvents txtCreateDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ddlEngineerList As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ddlEnggStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnAddSiteEngg As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GvSiteEngineerList As System.Windows.Forms.DataGridView
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtEnqNoSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ddlSiteStatus As System.Windows.Forms.ComboBox
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtStation As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rblType_Service As System.Windows.Forms.RadioButton
    Friend WithEvents rblType_EC As System.Windows.Forms.RadioButton
    Friend WithEvents txtPlantType As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteEngineer As System.Windows.Forms.Button
    Friend WithEvents btnSearchRefresh As System.Windows.Forms.Button
    Friend WithEvents rblVisit As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rblRunningSearch As System.Windows.Forms.RadioButton
    Friend WithEvents rblDoneSearch As System.Windows.Forms.RadioButton
    Friend WithEvents rblCancelSearch As System.Windows.Forms.RadioButton
    Friend WithEvents chkEngineer As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rblSearchVisit As System.Windows.Forms.RadioButton
    Friend WithEvents rblSearchEC As System.Windows.Forms.RadioButton
    Friend WithEvents rblSearchService As System.Windows.Forms.RadioButton
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents rblSearchPending As System.Windows.Forms.RadioButton
    Friend WithEvents rblSearchSCH As System.Windows.Forms.RadioButton
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtpriorityno As System.Windows.Forms.TextBox
    Friend WithEvents btnODEngineerReport As System.Windows.Forms.Button
    Friend WithEvents rblSearchAll As System.Windows.Forms.RadioButton
    Friend WithEvents rblSearchtypeAll As System.Windows.Forms.RadioButton
End Class
