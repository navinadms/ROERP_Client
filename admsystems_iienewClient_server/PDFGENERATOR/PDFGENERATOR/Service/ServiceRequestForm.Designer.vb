<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServiceRequestForm
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rblCancelStatus = New System.Windows.Forms.RadioButton()
        Me.rblDoneStatus = New System.Windows.Forms.RadioButton()
        Me.rblPendingStatus = New System.Windows.Forms.RadioButton()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtClientHistory = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtReqLevel = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtMaterial = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.chkExp3 = New System.Windows.Forms.CheckBox()
        Me.chkexp2 = New System.Windows.Forms.CheckBox()
        Me.chkexp1 = New System.Windows.Forms.CheckBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtServiceReqDetail = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtCapacity = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.ddlMachineType = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtEmailID = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtContactNo = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtPin = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDist = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTal = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCallAttandBy = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtServiceReqBy = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTime = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtComplainNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCreateDate = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.GvServiceComplain = New System.Windows.Forms.DataGridView()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rblSearchCancel = New System.Windows.Forms.RadioButton()
        Me.rblSearchDone = New System.Windows.Forms.RadioButton()
        Me.rblSearchPending = New System.Windows.Forms.RadioButton()
        Me.txtSearchCompany = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtSearchComp = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.rblSearchNone = New System.Windows.Forms.RadioButton()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.GvServiceComplain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.btnSubmit)
        Me.GroupBox1.Controls.Add(Me.txtRemark)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtClientHistory)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtReqLevel)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.txtMaterial)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.chkExp3)
        Me.GroupBox1.Controls.Add(Me.chkexp2)
        Me.GroupBox1.Controls.Add(Me.chkexp1)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.txtServiceReqDetail)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtCapacity)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.ddlMachineType)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtEmailID)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtContactNo)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtState)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtPin)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtDist)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtTal)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtCity)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCompanyName)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtCallAttandBy)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtServiceReqBy)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTime)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtComplainNo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtCreateDate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(269, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(715, 625)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " "
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rblCancelStatus)
        Me.GroupBox2.Controls.Add(Me.rblDoneStatus)
        Me.GroupBox2.Controls.Add(Me.rblPendingStatus)
        Me.GroupBox2.Location = New System.Drawing.Point(130, 546)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(242, 33)
        Me.GroupBox2.TabIndex = 52
        Me.GroupBox2.TabStop = False
        '
        'rblCancelStatus
        '
        Me.rblCancelStatus.AutoSize = True
        Me.rblCancelStatus.Location = New System.Drawing.Point(141, 10)
        Me.rblCancelStatus.Name = "rblCancelStatus"
        Me.rblCancelStatus.Size = New System.Drawing.Size(58, 17)
        Me.rblCancelStatus.TabIndex = 2
        Me.rblCancelStatus.Text = "Cancel"
        Me.rblCancelStatus.UseVisualStyleBackColor = True
        '
        'rblDoneStatus
        '
        Me.rblDoneStatus.AutoSize = True
        Me.rblDoneStatus.Location = New System.Drawing.Point(84, 10)
        Me.rblDoneStatus.Name = "rblDoneStatus"
        Me.rblDoneStatus.Size = New System.Drawing.Size(51, 17)
        Me.rblDoneStatus.TabIndex = 1
        Me.rblDoneStatus.Text = "Done"
        Me.rblDoneStatus.UseVisualStyleBackColor = True
        '
        'rblPendingStatus
        '
        Me.rblPendingStatus.AutoSize = True
        Me.rblPendingStatus.Checked = True
        Me.rblPendingStatus.Location = New System.Drawing.Point(14, 10)
        Me.rblPendingStatus.Name = "rblPendingStatus"
        Me.rblPendingStatus.Size = New System.Drawing.Size(64, 17)
        Me.rblPendingStatus.TabIndex = 0
        Me.rblPendingStatus.TabStop = True
        Me.rblPendingStatus.Text = "Pending"
        Me.rblPendingStatus.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(33, 559)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(83, 13)
        Me.Label27.TabIndex = 51
        Me.Label27.Text = "Complain Status"
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(280, 585)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 28)
        Me.btnSubmit.TabIndex = 50
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(463, 480)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(229, 59)
        Me.txtRemark.TabIndex = 49
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(388, 482)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(60, 13)
        Me.Label26.TabIndex = 48
        Me.Label26.Text = "REMARKS"
        '
        'txtClientHistory
        '
        Me.txtClientHistory.Location = New System.Drawing.Point(130, 477)
        Me.txtClientHistory.Multiline = True
        Me.txtClientHistory.Name = "txtClientHistory"
        Me.txtClientHistory.Size = New System.Drawing.Size(242, 62)
        Me.txtClientHistory.TabIndex = 47
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(28, 480)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(96, 13)
        Me.Label25.TabIndex = 46
        Me.Label25.Text = "CLIENT HISTORY"
        '
        'txtReqLevel
        '
        Me.txtReqLevel.FormattingEnabled = True
        Me.txtReqLevel.Items.AddRange(New Object() {"A+++", "A++", "A+"})
        Me.txtReqLevel.Location = New System.Drawing.Point(487, 418)
        Me.txtReqLevel.Name = "txtReqLevel"
        Me.txtReqLevel.Size = New System.Drawing.Size(121, 21)
        Me.txtReqLevel.TabIndex = 45
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(334, 421)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(147, 13)
        Me.Label24.TabIndex = 44
        Me.Label24.Text = "SERVICE REQUEST LEVEL "
        '
        'txtMaterial
        '
        Me.txtMaterial.Location = New System.Drawing.Point(463, 363)
        Me.txtMaterial.Multiline = True
        Me.txtMaterial.Name = "txtMaterial"
        Me.txtMaterial.Size = New System.Drawing.Size(229, 42)
        Me.txtMaterial.TabIndex = 43
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(338, 366)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(121, 13)
        Me.Label23.TabIndex = 42
        Me.Label23.Text = " MATERIAL / SPARES "
        '
        'chkExp3
        '
        Me.chkExp3.AutoSize = True
        Me.chkExp3.Location = New System.Drawing.Point(280, 434)
        Me.chkExp3.Name = "chkExp3"
        Me.chkExp3.Size = New System.Drawing.Size(44, 17)
        Me.chkExp3.TabIndex = 41
        Me.chkExp3.Text = "Yes"
        Me.chkExp3.UseVisualStyleBackColor = True
        '
        'chkexp2
        '
        Me.chkexp2.AutoSize = True
        Me.chkexp2.Location = New System.Drawing.Point(280, 401)
        Me.chkexp2.Name = "chkexp2"
        Me.chkexp2.Size = New System.Drawing.Size(44, 17)
        Me.chkexp2.TabIndex = 40
        Me.chkexp2.Text = "Yes"
        Me.chkexp2.UseVisualStyleBackColor = True
        '
        'chkexp1
        '
        Me.chkexp1.AutoSize = True
        Me.chkexp1.Location = New System.Drawing.Point(280, 366)
        Me.chkexp1.Name = "chkexp1"
        Me.chkexp1.Size = New System.Drawing.Size(44, 17)
        Me.chkexp1.TabIndex = 39
        Me.chkexp1.Text = "Yes"
        Me.chkexp1.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(28, 434)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(247, 13)
        Me.Label22.TabIndex = 38
        Me.Label22.Text = "F.O.C. BASIS – OUTSIDE WARRANTEE PERIOD"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(28, 405)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(239, 13)
        Me.Label21.TabIndex = 37
        Me.Label21.Text = "F.O.C. BASIS – WITHIN WARRANTEE PERIOD"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(28, 370)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(161, 13)
        Me.Label20.TabIndex = 36
        Me.Label20.Text = "TO BE BORNE BY CUSOTMER"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(28, 342)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(124, 13)
        Me.Label19.TabIndex = 35
        Me.Label19.Text = "Charges Expectation"
        '
        'txtServiceReqDetail
        '
        Me.txtServiceReqDetail.Location = New System.Drawing.Point(150, 292)
        Me.txtServiceReqDetail.Multiline = True
        Me.txtServiceReqDetail.Name = "txtServiceReqDetail"
        Me.txtServiceReqDetail.Size = New System.Drawing.Size(548, 42)
        Me.txtServiceReqDetail.TabIndex = 34
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(28, 292)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(116, 13)
        Me.Label18.TabIndex = 33
        Me.Label18.Text = "Service Request Detail"
        '
        'txtCapacity
        '
        Me.txtCapacity.Location = New System.Drawing.Point(466, 260)
        Me.txtCapacity.Name = "txtCapacity"
        Me.txtCapacity.Size = New System.Drawing.Size(120, 20)
        Me.txtCapacity.TabIndex = 32
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(399, 263)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(48, 13)
        Me.Label17.TabIndex = 31
        Me.Label17.Text = "Capacity"
        '
        'ddlMachineType
        '
        Me.ddlMachineType.FormattingEnabled = True
        Me.ddlMachineType.Items.AddRange(New Object() {"ISI", "NON ISI", "IND", "OTH"})
        Me.ddlMachineType.Location = New System.Drawing.Point(150, 259)
        Me.ddlMachineType.Name = "ddlMachineType"
        Me.ddlMachineType.Size = New System.Drawing.Size(160, 21)
        Me.ddlMachineType.TabIndex = 30
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(28, 263)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(87, 13)
        Me.Label16.TabIndex = 29
        Me.Label16.Text = "Type of Machine"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(28, 241)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(95, 13)
        Me.Label15.TabIndex = 28
        Me.Label15.Text = "Complain Detail"
        '
        'txtEmailID
        '
        Me.txtEmailID.Location = New System.Drawing.Point(397, 200)
        Me.txtEmailID.Name = "txtEmailID"
        Me.txtEmailID.Size = New System.Drawing.Size(295, 20)
        Me.txtEmailID.TabIndex = 27
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(340, 204)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(32, 13)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "Email"
        '
        'txtContactNo
        '
        Me.txtContactNo.Location = New System.Drawing.Point(144, 204)
        Me.txtContactNo.Name = "txtContactNo"
        Me.txtContactNo.Size = New System.Drawing.Size(163, 20)
        Me.txtContactNo.TabIndex = 25
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(28, 204)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(84, 13)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "Contact Number"
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(592, 164)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(100, 20)
        Me.txtState.TabIndex = 23
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(554, 167)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(32, 13)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "State"
        '
        'txtPin
        '
        Me.txtPin.Location = New System.Drawing.Point(463, 165)
        Me.txtPin.Name = "txtPin"
        Me.txtPin.Size = New System.Drawing.Size(86, 20)
        Me.txtPin.TabIndex = 21
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(435, 168)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(22, 13)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Pin"
        '
        'txtDist
        '
        Me.txtDist.Location = New System.Drawing.Point(329, 164)
        Me.txtDist.Name = "txtDist"
        Me.txtDist.Size = New System.Drawing.Size(100, 20)
        Me.txtDist.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(298, 167)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 13)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Dist"
        '
        'txtTal
        '
        Me.txtTal.Location = New System.Drawing.Point(192, 164)
        Me.txtTal.Name = "txtTal"
        Me.txtTal.Size = New System.Drawing.Size(100, 20)
        Me.txtTal.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(164, 168)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(22, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Tal"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(58, 164)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(100, 20)
        Me.txtCity.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(28, 168)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 13)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "City"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(144, 123)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(548, 30)
        Me.txtAddress.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 121)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Address"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(144, 92)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(548, 20)
        Me.txtCompanyName.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(28, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Company Name"
        '
        'txtCallAttandBy
        '
        Me.txtCallAttandBy.Enabled = False
        Me.txtCallAttandBy.Location = New System.Drawing.Point(430, 59)
        Me.txtCallAttandBy.Name = "txtCallAttandBy"
        Me.txtCallAttandBy.Size = New System.Drawing.Size(262, 20)
        Me.txtCallAttandBy.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(340, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Call Attand By"
        '
        'txtServiceReqBy
        '
        Me.txtServiceReqBy.Location = New System.Drawing.Point(144, 59)
        Me.txtServiceReqBy.Name = "txtServiceReqBy"
        Me.txtServiceReqBy.Size = New System.Drawing.Size(163, 20)
        Me.txtServiceReqBy.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Service Request By"
        '
        'txtTime
        '
        Me.txtTime.Enabled = False
        Me.txtTime.Location = New System.Drawing.Point(590, 28)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.Size = New System.Drawing.Size(102, 20)
        Me.txtTime.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(537, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Time"
        '
        'txtComplainNo
        '
        Me.txtComplainNo.Location = New System.Drawing.Point(343, 28)
        Me.txtComplainNo.Name = "txtComplainNo"
        Me.txtComplainNo.Size = New System.Drawing.Size(155, 20)
        Me.txtComplainNo.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(270, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Complain No"
        '
        'txtCreateDate
        '
        Me.txtCreateDate.Enabled = False
        Me.txtCreateDate.Location = New System.Drawing.Point(144, 28)
        Me.txtCreateDate.Name = "txtCreateDate"
        Me.txtCreateDate.Size = New System.Drawing.Size(120, 20)
        Me.txtCreateDate.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtTotal)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.GvServiceComplain)
        Me.GroupBox3.Controls.Add(Me.btnRefresh)
        Me.GroupBox3.Controls.Add(Me.btnSearch)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Controls.Add(Me.txtSearchCompany)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.txtSearchComp)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(251, 613)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " "
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(83, 578)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(100, 20)
        Me.txtTotal.TabIndex = 60
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(20, 584)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(31, 13)
        Me.Label30.TabIndex = 59
        Me.Label30.Text = "Total"
        '
        'GvServiceComplain
        '
        Me.GvServiceComplain.AllowUserToAddRows = False
        Me.GvServiceComplain.AllowUserToDeleteRows = False
        Me.GvServiceComplain.AllowUserToOrderColumns = True
        Me.GvServiceComplain.AllowUserToResizeColumns = False
        Me.GvServiceComplain.AllowUserToResizeRows = False
        Me.GvServiceComplain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvServiceComplain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvServiceComplain.Location = New System.Drawing.Point(8, 205)
        Me.GvServiceComplain.Name = "GvServiceComplain"
        Me.GvServiceComplain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvServiceComplain.Size = New System.Drawing.Size(237, 367)
        Me.GvServiceComplain.TabIndex = 58
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(133, 114)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 27)
        Me.btnRefresh.TabIndex = 57
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(38, 114)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 27)
        Me.btnSearch.TabIndex = 56
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rblSearchNone)
        Me.GroupBox4.Controls.Add(Me.rblSearchCancel)
        Me.GroupBox4.Controls.Add(Me.rblSearchDone)
        Me.GroupBox4.Controls.Add(Me.rblSearchPending)
        Me.GroupBox4.Location = New System.Drawing.Point(9, 154)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(242, 33)
        Me.GroupBox4.TabIndex = 53
        Me.GroupBox4.TabStop = False
        '
        'rblSearchCancel
        '
        Me.rblSearchCancel.AutoSize = True
        Me.rblSearchCancel.Location = New System.Drawing.Point(126, 9)
        Me.rblSearchCancel.Name = "rblSearchCancel"
        Me.rblSearchCancel.Size = New System.Drawing.Size(58, 17)
        Me.rblSearchCancel.TabIndex = 2
        Me.rblSearchCancel.Text = "Cancel"
        Me.rblSearchCancel.UseVisualStyleBackColor = True
        '
        'rblSearchDone
        '
        Me.rblSearchDone.AutoSize = True
        Me.rblSearchDone.Location = New System.Drawing.Point(76, 10)
        Me.rblSearchDone.Name = "rblSearchDone"
        Me.rblSearchDone.Size = New System.Drawing.Size(51, 17)
        Me.rblSearchDone.TabIndex = 1
        Me.rblSearchDone.Text = "Done"
        Me.rblSearchDone.UseVisualStyleBackColor = True
        '
        'rblSearchPending
        '
        Me.rblSearchPending.AutoSize = True
        Me.rblSearchPending.Location = New System.Drawing.Point(10, 10)
        Me.rblSearchPending.Name = "rblSearchPending"
        Me.rblSearchPending.Size = New System.Drawing.Size(64, 17)
        Me.rblSearchPending.TabIndex = 0
        Me.rblSearchPending.Text = "Pending"
        Me.rblSearchPending.UseVisualStyleBackColor = True
        '
        'txtSearchCompany
        '
        Me.txtSearchCompany.Location = New System.Drawing.Point(94, 75)
        Me.txtSearchCompany.Name = "txtSearchCompany"
        Me.txtSearchCompany.Size = New System.Drawing.Size(138, 20)
        Me.txtSearchCompany.TabIndex = 55
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(6, 78)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(82, 13)
        Me.Label29.TabIndex = 54
        Me.Label29.Text = "Company Name"
        '
        'txtSearchComp
        '
        Me.txtSearchComp.Location = New System.Drawing.Point(94, 38)
        Me.txtSearchComp.Name = "txtSearchComp"
        Me.txtSearchComp.Size = New System.Drawing.Size(138, 20)
        Me.txtSearchComp.TabIndex = 53
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 41)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(67, 13)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "Complain No"
        '
        'rblSearchNone
        '
        Me.rblSearchNone.AutoSize = True
        Me.rblSearchNone.Checked = True
        Me.rblSearchNone.Location = New System.Drawing.Point(189, 10)
        Me.rblSearchNone.Name = "rblSearchNone"
        Me.rblSearchNone.Size = New System.Drawing.Size(51, 17)
        Me.rblSearchNone.TabIndex = 3
        Me.rblSearchNone.TabStop = True
        Me.rblSearchNone.Text = "None"
        Me.rblSearchNone.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(372, 585)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 28)
        Me.btnCancel.TabIndex = 53
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'ServiceRequestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(990, 637)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ServiceRequestForm"
        Me.Text = "ServiceRequestForm"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.GvServiceComplain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCreateDate As System.Windows.Forms.TextBox
    Friend WithEvents txtComplainNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtServiceReqBy As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCallAttandBy As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDist As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTal As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPin As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtEmailID As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtContactNo As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents ddlMachineType As System.Windows.Forms.ComboBox
    Friend WithEvents txtCapacity As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtServiceReqDetail As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents chkExp3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkexp2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkexp1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtMaterial As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtReqLevel As System.Windows.Forms.ComboBox
    Friend WithEvents txtClientHistory As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rblPendingStatus As System.Windows.Forms.RadioButton
    Friend WithEvents rblDoneStatus As System.Windows.Forms.RadioButton
    Friend WithEvents rblCancelStatus As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearchComp As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtSearchCompany As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rblSearchCancel As System.Windows.Forms.RadioButton
    Friend WithEvents rblSearchDone As System.Windows.Forms.RadioButton
    Friend WithEvents rblSearchPending As System.Windows.Forms.RadioButton
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents GvServiceComplain As System.Windows.Forms.DataGridView
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents rblSearchNone As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
