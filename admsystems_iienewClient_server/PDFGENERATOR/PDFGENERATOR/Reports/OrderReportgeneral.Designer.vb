<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderReportgeneral
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
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.rblClientWithPacking = New System.Windows.Forms.RadioButton()
        Me.rblClient = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTotalCount = New System.Windows.Forms.Label()
        Me.btnExportToExcel = New System.Windows.Forms.Button()
        Me.GvOrdeReportList = New System.Windows.Forms.DataGridView()
        Me.btnFinalSearch = New System.Windows.Forms.Button()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.rblDispAll = New System.Windows.Forms.RadioButton()
        Me.rbldispwithout = New System.Windows.Forms.RadioButton()
        Me.rblDispatchWith = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.btnDisStatusClear = New System.Windows.Forms.Button()
        Me.txtDispStatusMul = New System.Windows.Forms.TextBox()
        Me.btnDispAdd = New System.Windows.Forms.Button()
        Me.txtDispStatus = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtProjectType = New System.Windows.Forms.ComboBox()
        Me.btnOStatusClear = New System.Windows.Forms.Button()
        Me.txtProjectTypeMulti = New System.Windows.Forms.TextBox()
        Me.btnProjectTypeAdd = New System.Windows.Forms.Button()
        Me.txtProjectType1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnPlantClear = New System.Windows.Forms.Button()
        Me.txtPlantMul = New System.Windows.Forms.TextBox()
        Me.btnPlantAdd = New System.Windows.Forms.Button()
        Me.txtPlant = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnStationClear = New System.Windows.Forms.Button()
        Me.txtStationMul = New System.Windows.Forms.TextBox()
        Me.btnstationAdd = New System.Windows.Forms.Button()
        Me.txtStation = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnStateClear = New System.Windows.Forms.Button()
        Me.txtStateMul = New System.Windows.Forms.TextBox()
        Me.btnstateAdd = New System.Windows.Forms.Button()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkMKTDate = New System.Windows.Forms.CheckBox()
        Me.chkIsDispaDate = New System.Windows.Forms.CheckBox()
        Me.chkIsDate = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtstartDt = New System.Windows.Forms.DateTimePicker()
        Me.txtAllUser = New System.Windows.Forms.TextBox()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        CType(Me.GvOrdeReportList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtAllUser)
        Me.GroupBox1.Controls.Add(Me.lblUser)
        Me.GroupBox1.Controls.Add(Me.cmbUser)
        Me.GroupBox1.Controls.Add(Me.btnAddUser)
        Me.GroupBox1.Controls.Add(Me.GroupBox9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.lblTotalCount)
        Me.GroupBox1.Controls.Add(Me.btnExportToExcel)
        Me.GroupBox1.Controls.Add(Me.GvOrdeReportList)
        Me.GroupBox1.Controls.Add(Me.btnFinalSearch)
        Me.GroupBox1.Controls.Add(Me.GroupBox8)
        Me.GroupBox1.Controls.Add(Me.GroupBox7)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(968, 585)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.rblClientWithPacking)
        Me.GroupBox9.Controls.Add(Me.rblClient)
        Me.GroupBox9.Location = New System.Drawing.Point(55, 202)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(263, 43)
        Me.GroupBox9.TabIndex = 19
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = " "
        '
        'rblClientWithPacking
        '
        Me.rblClientWithPacking.AutoSize = True
        Me.rblClientWithPacking.Checked = True
        Me.rblClientWithPacking.Location = New System.Drawing.Point(133, 16)
        Me.rblClientWithPacking.Name = "rblClientWithPacking"
        Me.rblClientWithPacking.Size = New System.Drawing.Size(118, 17)
        Me.rblClientWithPacking.TabIndex = 1
        Me.rblClientWithPacking.TabStop = True
        Me.rblClientWithPacking.Text = "Client With Packing"
        Me.rblClientWithPacking.UseVisualStyleBackColor = True
        '
        'rblClient
        '
        Me.rblClient.AutoSize = True
        Me.rblClient.Location = New System.Drawing.Point(33, 16)
        Me.rblClient.Name = "rblClient"
        Me.rblClient.Size = New System.Drawing.Size(75, 17)
        Me.rblClient.TabIndex = 0
        Me.rblClient.TabStop = True
        Me.rblClient.Text = "Client Only"
        Me.rblClient.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 218)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Party"
        '
        'lblTotalCount
        '
        Me.lblTotalCount.AutoSize = True
        Me.lblTotalCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCount.ForeColor = System.Drawing.Color.Maroon
        Me.lblTotalCount.Location = New System.Drawing.Point(21, 216)
        Me.lblTotalCount.Name = "lblTotalCount"
        Me.lblTotalCount.Size = New System.Drawing.Size(35, 15)
        Me.lblTotalCount.TabIndex = 18
        Me.lblTotalCount.Text = "       "
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(818, 284)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(130, 28)
        Me.btnExportToExcel.TabIndex = 17
        Me.btnExportToExcel.Text = "Export To Excel"
        Me.btnExportToExcel.UseVisualStyleBackColor = True
        '
        'GvOrdeReportList
        '
        Me.GvOrdeReportList.AllowDrop = True
        Me.GvOrdeReportList.AllowUserToAddRows = False
        Me.GvOrdeReportList.AllowUserToDeleteRows = False
        Me.GvOrdeReportList.AllowUserToOrderColumns = True
        Me.GvOrdeReportList.AllowUserToResizeColumns = False
        Me.GvOrdeReportList.AllowUserToResizeRows = False
        Me.GvOrdeReportList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvOrdeReportList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvOrdeReportList.Location = New System.Drawing.Point(9, 329)
        Me.GvOrdeReportList.Name = "GvOrdeReportList"
        Me.GvOrdeReportList.Size = New System.Drawing.Size(939, 250)
        Me.GvOrdeReportList.TabIndex = 16
        '
        'btnFinalSearch
        '
        Me.btnFinalSearch.Location = New System.Drawing.Point(370, 275)
        Me.btnFinalSearch.Name = "btnFinalSearch"
        Me.btnFinalSearch.Size = New System.Drawing.Size(130, 37)
        Me.btnFinalSearch.TabIndex = 15
        Me.btnFinalSearch.Text = "Search"
        Me.btnFinalSearch.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.rblDispAll)
        Me.GroupBox8.Controls.Add(Me.rbldispwithout)
        Me.GroupBox8.Controls.Add(Me.rblDispatchWith)
        Me.GroupBox8.Controls.Add(Me.Label8)
        Me.GroupBox8.Location = New System.Drawing.Point(9, 141)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(319, 53)
        Me.GroupBox8.TabIndex = 14
        Me.GroupBox8.TabStop = False
        '
        'rblDispAll
        '
        Me.rblDispAll.AutoSize = True
        Me.rblDispAll.Checked = True
        Me.rblDispAll.Location = New System.Drawing.Point(265, 19)
        Me.rblDispAll.Name = "rblDispAll"
        Me.rblDispAll.Size = New System.Drawing.Size(36, 17)
        Me.rblDispAll.TabIndex = 7
        Me.rblDispAll.TabStop = True
        Me.rblDispAll.Text = "All"
        Me.rblDispAll.UseVisualStyleBackColor = True
        '
        'rbldispwithout
        '
        Me.rbldispwithout.AutoSize = True
        Me.rbldispwithout.Location = New System.Drawing.Point(196, 19)
        Me.rbldispwithout.Name = "rbldispwithout"
        Me.rbldispwithout.Size = New System.Drawing.Size(67, 17)
        Me.rbldispwithout.TabIndex = 6
        Me.rbldispwithout.Text = "With Out"
        Me.rbldispwithout.UseVisualStyleBackColor = True
        '
        'rblDispatchWith
        '
        Me.rblDispatchWith.AutoSize = True
        Me.rblDispatchWith.Location = New System.Drawing.Point(142, 19)
        Me.rblDispatchWith.Name = "rblDispatchWith"
        Me.rblDispatchWith.Size = New System.Drawing.Size(47, 17)
        Me.rblDispatchWith.TabIndex = 5
        Me.rblDispatchWith.Text = "With"
        Me.rblDispatchWith.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(128, 13)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Dispatch Date Status"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.btnDisStatusClear)
        Me.GroupBox7.Controls.Add(Me.txtDispStatusMul)
        Me.GroupBox7.Controls.Add(Me.btnDispAdd)
        Me.GroupBox7.Controls.Add(Me.txtDispStatus)
        Me.GroupBox7.Controls.Add(Me.Label7)
        Me.GroupBox7.Location = New System.Drawing.Point(816, 19)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(144, 123)
        Me.GroupBox7.TabIndex = 13
        Me.GroupBox7.TabStop = False
        '
        'btnDisStatusClear
        '
        Me.btnDisStatusClear.Location = New System.Drawing.Point(75, 51)
        Me.btnDisStatusClear.Name = "btnDisStatusClear"
        Me.btnDisStatusClear.Size = New System.Drawing.Size(55, 23)
        Me.btnDisStatusClear.TabIndex = 4
        Me.btnDisStatusClear.Text = "Clear"
        Me.btnDisStatusClear.UseVisualStyleBackColor = True
        '
        'txtDispStatusMul
        '
        Me.txtDispStatusMul.Enabled = False
        Me.txtDispStatusMul.Location = New System.Drawing.Point(6, 79)
        Me.txtDispStatusMul.Multiline = True
        Me.txtDispStatusMul.Name = "txtDispStatusMul"
        Me.txtDispStatusMul.Size = New System.Drawing.Size(124, 38)
        Me.txtDispStatusMul.TabIndex = 3
        '
        'btnDispAdd
        '
        Me.btnDispAdd.Location = New System.Drawing.Point(15, 52)
        Me.btnDispAdd.Name = "btnDispAdd"
        Me.btnDispAdd.Size = New System.Drawing.Size(55, 23)
        Me.btnDispAdd.TabIndex = 2
        Me.btnDispAdd.Text = "Add"
        Me.btnDispAdd.UseVisualStyleBackColor = True
        '
        'txtDispStatus
        '
        Me.txtDispStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtDispStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtDispStatus.Location = New System.Drawing.Point(6, 31)
        Me.txtDispStatus.Name = "txtDispStatus"
        Me.txtDispStatus.Size = New System.Drawing.Size(124, 20)
        Me.txtDispStatus.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(25, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Dispatch Status"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtProjectType)
        Me.GroupBox6.Controls.Add(Me.btnOStatusClear)
        Me.GroupBox6.Controls.Add(Me.txtProjectTypeMulti)
        Me.GroupBox6.Controls.Add(Me.btnProjectTypeAdd)
        Me.GroupBox6.Controls.Add(Me.txtProjectType1)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Location = New System.Drawing.Point(666, 19)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(144, 123)
        Me.GroupBox6.TabIndex = 12
        Me.GroupBox6.TabStop = False
        '
        'txtProjectType
        '
        Me.txtProjectType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtProjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txtProjectType.FormattingEnabled = True
        Me.txtProjectType.Items.AddRange(New Object() {"ISI HOT", "ISI CONFIRM", "NON ISI HOT", "NON ISI CONFIRM"})
        Me.txtProjectType.Location = New System.Drawing.Point(7, 30)
        Me.txtProjectType.Name = "txtProjectType"
        Me.txtProjectType.Size = New System.Drawing.Size(121, 21)
        Me.txtProjectType.TabIndex = 19
        '
        'btnOStatusClear
        '
        Me.btnOStatusClear.Location = New System.Drawing.Point(72, 51)
        Me.btnOStatusClear.Name = "btnOStatusClear"
        Me.btnOStatusClear.Size = New System.Drawing.Size(55, 23)
        Me.btnOStatusClear.TabIndex = 4
        Me.btnOStatusClear.Text = "Clear"
        Me.btnOStatusClear.UseVisualStyleBackColor = True
        '
        'txtProjectTypeMulti
        '
        Me.txtProjectTypeMulti.Enabled = False
        Me.txtProjectTypeMulti.Location = New System.Drawing.Point(6, 79)
        Me.txtProjectTypeMulti.Multiline = True
        Me.txtProjectTypeMulti.Name = "txtProjectTypeMulti"
        Me.txtProjectTypeMulti.Size = New System.Drawing.Size(124, 38)
        Me.txtProjectTypeMulti.TabIndex = 3
        '
        'btnProjectTypeAdd
        '
        Me.btnProjectTypeAdd.Location = New System.Drawing.Point(11, 52)
        Me.btnProjectTypeAdd.Name = "btnProjectTypeAdd"
        Me.btnProjectTypeAdd.Size = New System.Drawing.Size(55, 23)
        Me.btnProjectTypeAdd.TabIndex = 2
        Me.btnProjectTypeAdd.Text = "Add"
        Me.btnProjectTypeAdd.UseVisualStyleBackColor = True
        '
        'txtProjectType1
        '
        Me.txtProjectType1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtProjectType1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtProjectType1.Location = New System.Drawing.Point(6, 31)
        Me.txtProjectType1.Name = "txtProjectType1"
        Me.txtProjectType1.Size = New System.Drawing.Size(124, 20)
        Me.txtProjectType1.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(35, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Order Status"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnPlantClear)
        Me.GroupBox5.Controls.Add(Me.txtPlantMul)
        Me.GroupBox5.Controls.Add(Me.btnPlantAdd)
        Me.GroupBox5.Controls.Add(Me.txtPlant)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Location = New System.Drawing.Point(516, 19)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(144, 123)
        Me.GroupBox5.TabIndex = 11
        Me.GroupBox5.TabStop = False
        '
        'btnPlantClear
        '
        Me.btnPlantClear.Location = New System.Drawing.Point(74, 50)
        Me.btnPlantClear.Name = "btnPlantClear"
        Me.btnPlantClear.Size = New System.Drawing.Size(55, 23)
        Me.btnPlantClear.TabIndex = 4
        Me.btnPlantClear.Text = "Clear"
        Me.btnPlantClear.UseVisualStyleBackColor = True
        '
        'txtPlantMul
        '
        Me.txtPlantMul.Enabled = False
        Me.txtPlantMul.Location = New System.Drawing.Point(6, 79)
        Me.txtPlantMul.Multiline = True
        Me.txtPlantMul.Name = "txtPlantMul"
        Me.txtPlantMul.Size = New System.Drawing.Size(124, 38)
        Me.txtPlantMul.TabIndex = 3
        '
        'btnPlantAdd
        '
        Me.btnPlantAdd.Location = New System.Drawing.Point(15, 51)
        Me.btnPlantAdd.Name = "btnPlantAdd"
        Me.btnPlantAdd.Size = New System.Drawing.Size(55, 23)
        Me.btnPlantAdd.TabIndex = 2
        Me.btnPlantAdd.Text = "Add"
        Me.btnPlantAdd.UseVisualStyleBackColor = True
        '
        'txtPlant
        '
        Me.txtPlant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtPlant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPlant.Location = New System.Drawing.Point(6, 31)
        Me.txtPlant.Name = "txtPlant"
        Me.txtPlant.Size = New System.Drawing.Size(124, 20)
        Me.txtPlant.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(45, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Plant"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnStationClear)
        Me.GroupBox4.Controls.Add(Me.txtStationMul)
        Me.GroupBox4.Controls.Add(Me.btnstationAdd)
        Me.GroupBox4.Controls.Add(Me.txtStation)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Location = New System.Drawing.Point(192, 16)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(155, 123)
        Me.GroupBox4.TabIndex = 10
        Me.GroupBox4.TabStop = False
        '
        'btnStationClear
        '
        Me.btnStationClear.Location = New System.Drawing.Point(79, 52)
        Me.btnStationClear.Name = "btnStationClear"
        Me.btnStationClear.Size = New System.Drawing.Size(52, 23)
        Me.btnStationClear.TabIndex = 4
        Me.btnStationClear.Text = "Clear"
        Me.btnStationClear.UseVisualStyleBackColor = True
        '
        'txtStationMul
        '
        Me.txtStationMul.Enabled = False
        Me.txtStationMul.Location = New System.Drawing.Point(6, 79)
        Me.txtStationMul.Multiline = True
        Me.txtStationMul.Name = "txtStationMul"
        Me.txtStationMul.Size = New System.Drawing.Size(142, 38)
        Me.txtStationMul.TabIndex = 3
        '
        'btnstationAdd
        '
        Me.btnstationAdd.Location = New System.Drawing.Point(14, 53)
        Me.btnstationAdd.Name = "btnstationAdd"
        Me.btnstationAdd.Size = New System.Drawing.Size(52, 23)
        Me.btnstationAdd.TabIndex = 2
        Me.btnstationAdd.Text = "Add"
        Me.btnstationAdd.UseVisualStyleBackColor = True
        '
        'txtStation
        '
        Me.txtStation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtStation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtStation.Location = New System.Drawing.Point(6, 31)
        Me.txtStation.Name = "txtStation"
        Me.txtStation.Size = New System.Drawing.Size(142, 20)
        Me.txtStation.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(60, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Station"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnStateClear)
        Me.GroupBox3.Controls.Add(Me.txtStateMul)
        Me.GroupBox3.Controls.Add(Me.btnstateAdd)
        Me.GroupBox3.Controls.Add(Me.txtState)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(360, 16)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(156, 123)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        '
        'btnStateClear
        '
        Me.btnStateClear.Location = New System.Drawing.Point(77, 53)
        Me.btnStateClear.Name = "btnStateClear"
        Me.btnStateClear.Size = New System.Drawing.Size(52, 23)
        Me.btnStateClear.TabIndex = 4
        Me.btnStateClear.Text = "Clear"
        Me.btnStateClear.UseVisualStyleBackColor = True
        '
        'txtStateMul
        '
        Me.txtStateMul.Enabled = False
        Me.txtStateMul.Location = New System.Drawing.Point(6, 79)
        Me.txtStateMul.Multiline = True
        Me.txtStateMul.Name = "txtStateMul"
        Me.txtStateMul.Size = New System.Drawing.Size(140, 38)
        Me.txtStateMul.TabIndex = 3
        '
        'btnstateAdd
        '
        Me.btnstateAdd.Location = New System.Drawing.Point(15, 53)
        Me.btnstateAdd.Name = "btnstateAdd"
        Me.btnstateAdd.Size = New System.Drawing.Size(51, 23)
        Me.btnstateAdd.TabIndex = 2
        Me.btnstateAdd.Text = "Add"
        Me.btnstateAdd.UseVisualStyleBackColor = True
        '
        'txtState
        '
        Me.txtState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtState.Location = New System.Drawing.Point(10, 31)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(136, 20)
        Me.txtState.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(51, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "State"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkMKTDate)
        Me.GroupBox2.Controls.Add(Me.chkIsDispaDate)
        Me.GroupBox2.Controls.Add(Me.chkIsDate)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtEndDate)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtstartDt)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(186, 126)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'chkMKTDate
        '
        Me.chkMKTDate.AutoSize = True
        Me.chkMKTDate.Location = New System.Drawing.Point(47, 41)
        Me.chkMKTDate.Name = "chkMKTDate"
        Me.chkMKTDate.Size = New System.Drawing.Size(75, 17)
        Me.chkMKTDate.TabIndex = 12
        Me.chkMKTDate.Text = "MKT Date"
        Me.chkMKTDate.UseVisualStyleBackColor = True
        '
        'chkIsDispaDate
        '
        Me.chkIsDispaDate.AutoSize = True
        Me.chkIsDispaDate.Location = New System.Drawing.Point(116, 15)
        Me.chkIsDispaDate.Name = "chkIsDispaDate"
        Me.chkIsDispaDate.Size = New System.Drawing.Size(70, 17)
        Me.chkIsDispaDate.TabIndex = 11
        Me.chkIsDispaDate.Text = "Dis_Date"
        Me.chkIsDispaDate.UseVisualStyleBackColor = True
        '
        'chkIsDate
        '
        Me.chkIsDate.AutoSize = True
        Me.chkIsDate.Location = New System.Drawing.Point(47, 15)
        Me.chkIsDate.Name = "chkIsDate"
        Me.chkIsDate.Size = New System.Drawing.Size(69, 17)
        Me.chkIsDate.TabIndex = 10
        Me.chkIsDate.Text = "Ord Date"
        Me.chkIsDate.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(12, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 13)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Date"
        '
        'txtEndDate
        '
        Me.txtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtEndDate.Location = New System.Drawing.Point(79, 94)
        Me.txtEndDate.Name = "txtEndDate"
        Me.txtEndDate.Size = New System.Drawing.Size(85, 20)
        Me.txtEndDate.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Start Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "End Date"
        '
        'txtstartDt
        '
        Me.txtstartDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtstartDt.Location = New System.Drawing.Point(76, 64)
        Me.txtstartDt.Name = "txtstartDt"
        Me.txtstartDt.Size = New System.Drawing.Size(88, 20)
        Me.txtstartDt.TabIndex = 6
        '
        'txtAllUser
        '
        Me.txtAllUser.Location = New System.Drawing.Point(366, 202)
        Me.txtAllUser.Multiline = True
        Me.txtAllUser.Name = "txtAllUser"
        Me.txtAllUser.Size = New System.Drawing.Size(192, 34)
        Me.txtAllUser.TabIndex = 39
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.Location = New System.Drawing.Point(352, 158)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(33, 13)
        Me.lblUser.TabIndex = 38
        Me.lblUser.Text = "User"
        '
        'cmbUser
        '
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(392, 151)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(176, 21)
        Me.cmbUser.TabIndex = 36
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(452, 171)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(37, 23)
        Me.btnAddUser.TabIndex = 37
        Me.btnAddUser.Text = "Add"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'OrderReportgeneral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(998, 600)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "OrderReportgeneral"
        Me.Text = "OrderReportgeneral"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        CType(Me.GvOrdeReportList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtstartDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnstateAdd As System.Windows.Forms.Button
    Friend WithEvents txtStateMul As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtStationMul As System.Windows.Forms.TextBox
    Friend WithEvents btnstationAdd As System.Windows.Forms.Button
    Friend WithEvents txtStation As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPlantMul As System.Windows.Forms.TextBox
    Friend WithEvents btnPlantAdd As System.Windows.Forms.Button
    Friend WithEvents txtPlant As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtProjectTypeMulti As System.Windows.Forms.TextBox
    Friend WithEvents btnProjectTypeAdd As System.Windows.Forms.Button
    Friend WithEvents txtProjectType1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDispStatusMul As System.Windows.Forms.TextBox
    Friend WithEvents btnDispAdd As System.Windows.Forms.Button
    Friend WithEvents txtDispStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rbldispwithout As System.Windows.Forms.RadioButton
    Friend WithEvents rblDispatchWith As System.Windows.Forms.RadioButton
    Friend WithEvents rblDispAll As System.Windows.Forms.RadioButton
    Friend WithEvents btnFinalSearch As System.Windows.Forms.Button
    Friend WithEvents GvOrdeReportList As System.Windows.Forms.DataGridView
    Friend WithEvents btnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkIsDate As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotalCount As System.Windows.Forms.Label
    Friend WithEvents btnDisStatusClear As System.Windows.Forms.Button
    Friend WithEvents btnOStatusClear As System.Windows.Forms.Button
    Friend WithEvents btnPlantClear As System.Windows.Forms.Button
    Friend WithEvents btnStationClear As System.Windows.Forms.Button
    Friend WithEvents btnStateClear As System.Windows.Forms.Button
    Friend WithEvents txtProjectType As System.Windows.Forms.ComboBox
    Friend WithEvents chkIsDispaDate As System.Windows.Forms.CheckBox
    Friend WithEvents chkMKTDate As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents rblClient As System.Windows.Forms.RadioButton
    Friend WithEvents rblClientWithPacking As System.Windows.Forms.RadioButton
    Friend WithEvents txtAllUser As System.Windows.Forms.TextBox
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
End Class
