Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports


Public Class OrderMaster_1_
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim Pk_PackingDetailNew_ID As Integer

    Dim tblFollowup As New DataTable
    Dim tblSFollowup As New DataTable
    Dim tblProjectDetail As New DataTable
    Dim TblISIDesc As New DataTable
    Dim TblProInst As New DataTable
    Dim TblLetter As New DataTable
    Dim TblRawMaterial As New DataTable
    Dim tblLog As New DataTable
    ''Delete Rows
    Dim rwIDDelOrderDetail As Integer = -1
    Dim rwIDDelFollowDetail As Integer = -1
    Dim rwIDISIProcessDetail As Integer = -1
    Dim TeamId As Integer = 0
    Dim rwIDDelLetterMailDetail As Integer = -1
    Dim rwIDDelRawMaterialDetail As Integer = -1
    Dim rwIDLetterLogDetail As Integer = -1
    Dim Pk_OrderFollowupDetailId As Integer
    Dim PK_LetterDetailID As Integer
    Dim Pk_ProjctDetailId As Integer
    Dim Pk_ProjectLetterId As Integer
    Dim Pk_PlantDraw_Doc_ID As Integer
    Dim Pk_OrderRawMaterialId As Integer
    Dim FK_ProjectMaster_ID As Integer

    Dim msPic1 As New MemoryStream
    Dim msPic2 As New MemoryStream
    Dim msPic3 As New MemoryStream
    Dim msPic4 As New MemoryStream


    Enum RawMaterial
        ItemName = 0
        Qty = 1
        Price = 2
        Amount = 3
        IsOrderConfirm = 4
        IsPaymentReceived = 5
        OrderDate = 6
        DisDate = 7
        Ten_DisDate = 8

    End Enum


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        dtDisDate.Text = Class1.Datecheck(System.DateTime.Now.ToString)
        txtPItemOther.Visible = False
        txtPCapacityOther.Visible = False
        AutoComplete_Text()
        GvInEnq_Bind()
        TCorderMast.TabPages.RemoveAt(4)
        TCorderMast.TabPages.RemoveAt(6)
        RavSoft.CueProvider.SetCue(txtEmail1, "Email 1")
        RavSoft.CueProvider.SetCue(txtEmail2, "Community")

        bindTeam()
        bindUser()
        ddlPacking_Item_List_Bind()

        TeamId = cmbTeam.SelectedValue
        btnPlantDraDelete.Visible = False
        btnPackingDelete.Visible = False
        btnDelFollowup.Visible = False

        If (Class1.global.UserAllotType.ToLower() = "head") Then
            grpAllotment.Visible = True

            btnProjectDetailDelete.Visible = True
            btnPlantDraDelete.Visible = True
            btnPackingDelete.Visible = True
            btnDelFollowup.Visible = True



        Else
            grpAllotment.Visible = False

            btnProjectDetailDelete.Visible = False

        End If
       
       



        'btnPlantDraDelete
        Project_AutoComplated()
        bindClientDataTexts()
        '  bindGrid()
    End Sub
    Public Sub bindClientDataTexts()
        RavSoft.CueProvider.SetCue(txtPh11, "Name")
        RavSoft.CueProvider.SetCue(txtPh16, "Email")
        RavSoft.CueProvider.SetCue(txtPh13, "Business")
        RavSoft.CueProvider.SetCue(txtPh14, "Address")
        RavSoft.CueProvider.SetCue(txtPh15, "Remarks")
        RavSoft.CueProvider.SetCue(txtPh12, "Mobile")

        RavSoft.CueProvider.SetCue(txtPH21, "Name")
        RavSoft.CueProvider.SetCue(txtPH26, "Email")
        RavSoft.CueProvider.SetCue(txtPH23, "Business")
        RavSoft.CueProvider.SetCue(txtPH24, "Address")
        RavSoft.CueProvider.SetCue(txtPH25, "Remarks")
        RavSoft.CueProvider.SetCue(txtPH22, "Mobile")

        RavSoft.CueProvider.SetCue(txtPH31, "Name")
        RavSoft.CueProvider.SetCue(txtPH36, "Email")
        RavSoft.CueProvider.SetCue(txtPH33, "Business")
        RavSoft.CueProvider.SetCue(txtPH34, "Address")
        RavSoft.CueProvider.SetCue(txtPH35, "Remarks")
        RavSoft.CueProvider.SetCue(txtPH32, "Mobile")

        RavSoft.CueProvider.SetCue(txtph41, "Name")
        RavSoft.CueProvider.SetCue(txtph46, "Email")
        RavSoft.CueProvider.SetCue(txtph43, "Business")
        RavSoft.CueProvider.SetCue(txtph44, "Address")
        RavSoft.CueProvider.SetCue(txtph45, "Remarks")
        RavSoft.CueProvider.SetCue(txtph42, "Mobile")

    End Sub
    Public Sub bindUser()
        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_UserId")
        datatable.Columns.Add("UserName")
        Dim dataUserSearch = linq_obj.SP_Get_UserListByTeam(Convert.ToInt32(cmbTeam.SelectedValue)).ToList()
        For Each item As SP_Get_UserListByTeamResult In dataUserSearch
            datatable.Rows.Add(item.Pk_UserId, item.UserName)
        Next
        Dim newRow As DataRow = datatable.NewRow()

        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        cmbUser.DataSource = datatable
        cmbUser.DisplayMember = "UserName"
        cmbUser.ValueMember = "Pk_UserId"
        cmbUser.AutoCompleteMode = AutoCompleteMode.Append
        cmbUser.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub


    Public Sub bindTeam()
        cmbTeam.DataSource = Nothing
        Dim dataTeam = linq_obj.SP_Tbl_TeamMaster_SelectAll().Where(Function(p) p.Department.ToUpper() = "ORDER").ToList()
        cmbTeam.DataSource = dataTeam
        cmbTeam.DisplayMember = "TeamName"
        cmbTeam.ValueMember = "Pk_TeamId"
        cmbTeam.AutoCompleteMode = AutoCompleteMode.Append
        cmbTeam.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTeam.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub ddlPacking_Item_List_Bind()
        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_Packing_Item_Master_ID")
        datatable.Columns.Add("Packing_Item")
        Dim dataUserSearch = linq_obj.SP_Get_Packing_Item_Master_List().ToList()
        For Each item As SP_Get_Packing_Item_Master_ListResult In dataUserSearch
            datatable.Rows.Add(item.Pk_Packing_Item_Master_ID, item.Packing_Item)
        Next
        Dim newRow As DataRow = datatable.NewRow()

        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        ddlPackingItem.DataSource = datatable
        ddlPackingItem.DisplayMember = "Packing_Item"
        ddlPackingItem.ValueMember = "Pk_Packing_Item_Master_ID"
        ddlPackingItem.AutoCompleteMode = AutoCompleteMode.Append
        ddlPackingItem.DropDownStyle = ComboBoxStyle.DropDownList
        ddlPackingItem.AutoCompleteSource = AutoCompleteSource.ListItems

    End Sub
    Public Sub ddlPacking_Item_Capacity_Bind()
        Dim datatable As New DataTable
        datatable.Columns.Add("PK_Packing_item_Capacity_ID")
        datatable.Columns.Add("Packing_Capacity")
        Dim dataPacking = linq_obj.SP_Get_Packing_Item_Capacity(ddlPackingItem.Text).ToList()
        For Each item As SP_Get_Packing_Item_CapacityResult In dataPacking
            datatable.Rows.Add(item.Packing_Capacity, item.Packing_Capacity)
        Next
        Dim newRow As DataRow = datatable.NewRow()
        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        ddlPackingCapacity.DataSource = datatable
        ddlPackingCapacity.DisplayMember = "Packing_Capacity"
        ddlPackingCapacity.ValueMember = "PK_Packing_item_Capacity_ID"
        ddlPackingCapacity.AutoCompleteMode = AutoCompleteMode.Append
        ddlPackingCapacity.DropDownStyle = ComboBoxStyle.DropDownList
        ddlPackingCapacity.AutoCompleteSource = AutoCompleteSource.ListItems

    End Sub
    Public Sub GvPackingDetailNewList_Bind()

        Dim datatable As New DataTable

        Dim data = linq_obj.SP_Get_PackingDetail_New_List(Address_ID).ToList()
        GvPackingDetailNewList.DataSource = data
        GvPackingDetailNewList.Columns(0).Visible = False
        GvPackingDetailNewList.Columns(1).Visible = False



    End Sub
    Public Sub GvInEnq_Bind()
        bindEnqGrid()
    End Sub

    Public Sub bindEnqGrid()
        Try
            Dim criteria As String
            criteria = " and "

            If txtParty.Text.Trim() <> "" Then
                criteria = criteria + " Name like '%" + txtParty.Text + "%' and"
            End If
            If txtcoPerson.Text.Trim() <> "" Then
                criteria = criteria + " ContactPerson like '%" + txtcoPerson.Text + "%' and"
            End If
            If txtStation.Text.Trim <> "" Then
                criteria = criteria + " City like '%" + txtStation.Text + "%' and"
            End If
            If txtDisStatus.Text.Trim() <> "" Then
                criteria = criteria + " TypeOfEnq like '" + txtDisStatus.Text + "' and"
            End If
            If txtSearchEnqNo.Text.Trim() <> "" Then
                criteria = criteria + " EnqNo like '%" + txtSearchEnqNo.Text + "%' and"
            End If
            If txtSearchMobile.Text.Trim() <> "" Then
                criteria = criteria + " MobileNo like '%" + txtSearchMobile.Text + "%' and"
            End If
            If txtSearchEmail.Text.Trim() <> "" Then
                criteria = criteria + " EmailID like '%" + txtSearchEmail.Text + "%' and"
            End If

            If criteria = " and " Then
                criteria = ""
            End If

            If (criteria.Length > 0) Then
                criteria = criteria.ToString().Substring(0, criteria.Length - 3)
            End If

            Dim cmd As New SqlCommand

            If (chkAllStatus.Checked = True) Then
                cmd.CommandText = "SP_Search_AllOrder_WithAllotment"
            Else
                cmd.CommandText = "SP_Search_Order_WithAllotment"
            End If

            cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
            ' cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = If(Class1.global.UserName.ToLower() = "admin", "0", Class1.global.UserID.ToString())

            'Add Navin 29-8-2013 Start 
            If (Class1.global.InquiryView = "1") Then
                cmd.Parameters.AddWithValue("@User", 0)
            Else
                cmd.Parameters.AddWithValue("@User", Class1.global.UserID.ToString())
            End If
            'End 

            Dim objclass As New Class1

            Dim dt As New DataTable
            dt.Columns.Add("Pk_AddressID")
            dt.Columns.Add("EnqNo")
            dt.Columns.Add("Name")
            Dim dtData As DataTable

            dtData = objclass.GetEnqOrderReportData(cmd)
            If dtData.Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DGVOrderDetails.DataSource = Nothing
                dtData.Dispose()
                dt.Dispose()
            Else
                For index = 0 To dtData.Rows.Count - 1
                    dt.Rows.Add(dtData.Rows(index)(0), dtData(index)(1), dtData(index)(2))
                Next
                DGVOrderDetails.DataSource = dt
                Me.DGVOrderDetails.Columns(0).Visible = False
                dtData.Dispose()
                dt.Dispose()
            End If
            txtTotOrders.Text = DGVOrderDetails.RowCount
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub getPageRight()
        Try
            Dim dv As New DataTable
            Dim dataView As New DataView
            Dim Class1 As New Class1
            Dim RowCount As Integer
            Dim statusCheck As Boolean = False
            Dim TabIndexNo As Integer
            Dim strName As String = ""
            Dim TabCountValue As Integer

            dataView = Class1.global.UserPermissionDataset.Tables(0).DefaultView

            dataView.RowFilter = "([Name] like 'Order Manager')"

            TabCountValue = TCorderMast.TabCount - 1

            dv = dataView.ToTable()


            If (dv.Rows(0)("Type") = 1) Then

            Else
                Dim indexTest As Integer = 0
                While indexTest <= TabCountValue
                    statusCheck = False
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("DetailName").ToString().ToUpper() = TCorderMast.TabPages(indexTest).Text.ToUpper()) Then
                            statusCheck = True
                            TabIndexNo = indexTest
                            Exit For
                        End If
                    Next
                    If statusCheck = False Then
                        TCorderMast.TabPages.RemoveAt(indexTest)
                        indexTest = -1
                        TabCountValue = TabCountValue - 1
                    End If
                    indexTest += 1
                End While
            End If

            'making a visible and invisible by their rights.
            For RowCount = 0 To dv.Rows.Count - 1
                If (dv.Rows(RowCount)("IsAdd") = True) Then
                    btnSave.Enabled = True
                Else
                    btnSave.Enabled = False
                End If
                If (dv.Rows(RowCount)("IsUpdate") = True) Then
                    btnChange.Enabled = True
                Else
                    btnChange.Enabled = False
                End If


                If (dv.Rows(RowCount)("IsDelete") = True) Then
                    btnDelete.Enabled = True
                    btnDelFollowup.Enabled = True
                Else
                    btnDelete.Enabled = False
                    btnDelFollowup.Enabled = False

                End If
                If (dv.Rows(RowCount)("IsPrint") = True) Then
                    btnPfolowup.Enabled = True
                    btnLFolowup.Enabled = True
                    btnIfolowup.Enabled = True
                    btnVisitor.Enabled = True

                Else
                    btnPfolowup.Enabled = False
                    btnLFolowup.Enabled = False
                    btnIfolowup.Enabled = False
                    btnVisitor.Enabled = False


                End If
            Next



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub AutoComplete_Text()


        'Name
        Dim data = linq_obj.SP_Get_AddressListAutoComplete("Name").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In data
            txtPartyName.AutoCompleteCustomSource.Add(iteam.Result)

        Next

        'EnqNo
        Dim dataEnq = linq_obj.SP_Get_AddressListAutoComplete("EnqNo").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In dataEnq
            txtSearchEnqNo.AutoCompleteCustomSource.Add(iteam.Result)
            txtOrderNo.AutoCompleteCustomSource.Add(iteam.Result)
        Next


        'Station
        Dim dataEnqCity = linq_obj.SP_Get_AddressListAutoComplete("City").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In dataEnqCity
            txtStation.AutoCompleteCustomSource.Add(iteam.Result)

        Next




        Dim GetUser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In GetUser
            txtSendCourierBy.AutoCompleteCustomSource.Add(item.UserName)
            txtRecCourierBy.AutoCompleteCustomSource.Add(item.UserName)
            txtSendMailBy.AutoCompleteCustomSource.Add(item.UserName)
            txtRecMailBy.AutoCompleteCustomSource.Add(item.UserName)
        Next
    End Sub

    Public Sub GetClientDetails_Bind()
        Try
            Dim Claient = linq_obj.SP_Get_AddressListById(Address_ID).ToList()
            For Each item As SP_Get_AddressListByIdResult In Claient
                txtPartyName.Text = item.Name
                txtBillAdresss.Text = item.Address
                txtCity.Text = item.City
                txtState.Text = item.State
                txtDistrict.Text = item.District
                txtTaluka.Text = item.Taluka
                txtPincode.Text = item.Pincode
                txtArea.Text = item.Area
                txtcontctName.Text = item.ContactPerson
                txtContactNo.Text = item.MobileNo
                txtOrdermastEmail.Text = item.EmailID
                txtOrderNo.Text = item.EnqNo
                txtDeladress.Text = item.DeliveryAddress
                txtDelArea.Text = item.DeliveryArea
                txtDelCity.Text = item.DeliveryCity
                txtDelDistrict.Text = item.DeliveryDistrict
                txtDelPincode.Text = item.DeliveryPincode
                txtDelState.Text = item.DeliveryState
                txtDelTaluka.Text = item.DeliveryTaluka
                txtEmail1.Text = item.EmailID1
                txtEmail2.Text = item.EmailID2
            Next
            'Get Enquiry Client Master Details
            Dim dataOrder = linq_obj.SP_GetOrderDetailFromAgreements(txtOrderNo.Text.Trim()).ToList()
            If (dataOrder.Count > 0) Then
                txtPONo.Text = Convert.ToString(dataOrder(0).Ref)
                txtModel.Text = Convert.ToString(dataOrder(0).Model)
                txtPlantName.Text = Convert.ToString(dataOrder(0).Plant)
                txttentiveSchem.Text = Convert.ToString(dataOrder(0).TreatmentScheme)
            End If

            'Dim partyDetail = linq_obj.SP_Select_Party_Master_ByAddressId(Address_ID).ToList()
            'If partyDetail.Count > 0 Then
            '    ' txtPONo.Text = partyDetail(0).PONo
            '    '  dtOrderDate.Text = Convert.ToString(partyDetail(0).OrderDate)
            'End If
        Catch ex As Exception
            MessageBox.Show("Error :" + ex.Message)
        End Try
    End Sub
    Public Sub GvFileList_Bind()

        GvFileList.Columns.Clear()
        GvFileList.ColumnCount = 4
        GvFileList.Columns(0).Name = "Pk_PlantDraw_Doc_ID"
        GvFileList.Columns(1).Name = "DocumentName"
        GvFileList.Columns(2).Name = "Path"
        GvFileList.Columns(3).Name = "Remarks"


        Dim data = linq_obj.SP_Get_Tbl_Plant_Drawing_Upload(Address_ID).ToList()

        For Each item As SP_Get_Tbl_Plant_Drawing_UploadResult In data
            GvFileList.Rows.Add(item.Pk_PlantDraw_Doc_ID, item.DocumentName, item.DocumentPath, item.DocumentRemarks)
        Next
        Dim dgvButton As New DataGridViewButtonColumn()
        dgvButton.FlatStyle = FlatStyle.System
        dgvButton.HeaderText = "View Doc"
        dgvButton.Name = "View Doc"
        dgvButton.UseColumnTextForButtonValue = True
        dgvButton.Text = "View"
        GvFileList.Columns.Add(dgvButton)
        GvFileList.Columns(0).Visible = False


    End Sub 
    'get Followup detail data
    Public Sub DGVfollowUp_Bind()

        Dim datafolloupDetail = linq_obj.SP_Select_All_Tbl_OrderFollowupDetailByFollowUp(Address_ID).ToList()
        DGVfollowUp.DataSource = datafolloupDetail
        DGVfollowUp.Columns(0).Visible = False
        DGVfollowUp.Columns(1).Visible = False


    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Address_ID > 0 Then
            Dim rpt As New ReportDocument
            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Rpt_Order_RawMaterialDetail"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Rpt_Order_RawMaterialDetail")

            Class1.WriteXMlFile(ds, "SP_Rpt_Order_RawMaterialDetail", "SP_Rpt_Order_RawMaterialDetail")

            rpt.Load(Application.StartupPath & "\Reports\Rpt_OrderRawMaterial.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Rpt_Order_RawMaterialDetail"))


            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        End If
    End Sub

    Private Sub txtPhto11_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    'Private Sub OrderMaster_1__KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = 13 Then
    '        SendKeys.Send("{TAB}")
    '    End If
    '    If e.KeyCode = 27 Then
    '        Me.Close()
    '    End If
    'End Sub

    Private Sub OrderMaster_1__Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        'add Columns To all Tables

        tblFollowup.Columns.Add("F_Date")
        tblFollowup.Columns.Add("Followup")
        tblFollowup.Columns.Add("N_F_FollowpDate")
        tblFollowup.Columns.Add("Status")
        tblFollowup.Columns.Add("ByWhom")
        tblFollowup.Columns.Add("Pro_Type")
        tblFollowup.Columns.Add("Remarks")

        'Added A Service FollowUp
        tblSFollowup.Columns.Add("Date")
        tblSFollowup.Columns.Add("ServiceType")
        tblSFollowup.Columns.Add("ComplainNo")
        tblSFollowup.Columns.Add("AttendDate")
        tblSFollowup.Columns.Add("AttendBy")
        tblSFollowup.Columns.Add("Engineer")
        tblSFollowup.Columns.Add("FollowUp")
        tblSFollowup.Columns.Add("TentativeDate")
        tblSFollowup.Columns.Add("Status")
        tblSFollowup.Columns.Add("Remarks")

        'Project Detail
        tblProjectDetail = New DataTable()

        tblProjectDetail.Columns.Add("SrNo")
        tblProjectDetail.Columns.Add("PlantScheme")
        tblProjectDetail.Columns.Add("VendorName")
        tblProjectDetail.Columns.Add("DispatchDate")


        '/ ISI Desc
        TblISIDesc.Columns.Add("F_Date")
        TblISIDesc.Columns.Add("Followup")
        TblISIDesc.Columns.Add("N_F_FollowpDate")
        TblISIDesc.Columns.Add("Status")
        TblISIDesc.Columns.Add("ByWhom")
        TblISIDesc.Columns.Add("Pro_Type")
        TblISIDesc.Columns.Add("Remarks")

        '/ Product Install
        TblProInst.Columns.Add("PDate")
        TblProInst.Columns.Add("Dis_Date")
        TblProInst.Columns.Add("Product_Name")
        TblProInst.Columns.Add("Vendor_Name")
        TblProInst.Columns.Add("Station")
        TblProInst.Columns.Add("Send_CU_To")
        TblProInst.Columns.Add("Rec_CU_From")
        TblProInst.Columns.Add("CU_To_Venue")
        TblProInst.Columns.Add("Comp_Date_With_Inst")
        TblProInst.Columns.Add("By_Whom")
        TblProInst.Columns.Add("Remark")

        '/ Letter Desc
        TblLetter.Columns.Add("Date")
        TblLetter.Columns.Add("Card_Date")
        TblLetter.Columns.Add("Card_Rem")
        TblLetter.Columns.Add("Mail_Rec")
        TblLetter.Columns.Add("Mail_Send")
        TblLetter.Columns.Add("BY_Whom")
        TblLetter.Columns.Add("Mail_Rem")


        '/ RawMaterial Details
        TblRawMaterial.Columns.Add("Pk_OrderRawMaterialId")
        TblRawMaterial.Columns.Add("ItemName")
        TblRawMaterial.Columns.Add("Qty")
        TblRawMaterial.Columns.Add("Price")
        TblRawMaterial.Columns.Add("Amount")
        TblRawMaterial.Columns.Add("OrderConfirm")
        TblRawMaterial.Columns.Add("PaymentReceived")
        TblRawMaterial.Columns.Add("OrderDate")
        TblRawMaterial.Columns.Add("DisDate")
        TblRawMaterial.Columns.Add("Ten_DisDate")

        '/Log
        tblLog.Columns.Add("Title")
        tblLog.Columns.Add("MailDate")
        tblLog.Columns.Add("MailBy")
        tblLog.Columns.Add("CourierDate")
        tblLog.Columns.Add("CourierBy")
        tblLog.Columns.Add("RecMailDate")
        tblLog.Columns.Add("RecMailBy")
        tblLog.Columns.Add("RecCourierDate")
        tblLog.Columns.Add("RecCourierBy")

        'permission get
        getPageRight()

        Dim i As Integer = 0
        'set a backcolor of focus textboxes
        For Each control As Control In Me.Controls
            ' The textbox control.
            Dim parentControl As New Control


            If (control.GetType() Is GetType(GroupBox)) Then
                Dim grpBox As GroupBox = TryCast(control, GroupBox)
                parentControl = grpBox

                ''For Tab

                For Each subcontrol As Control In parentControl.Controls

                    If (subcontrol.GetType() Is GetType(TabControl)) Then
                        For Each subcontrolTwo As Control In subcontrol.Controls
                            If (subcontrolTwo.GetType() Is GetType(TabPage)) Then
                                For Each subcontrolthree As Control In subcontrolTwo.Controls
                                    If (subcontrolthree.GetType() Is GetType(TextBox)) Then
                                        Dim textBox As TextBox = TryCast(subcontrolthree, TextBox)

                                        ' If not null, set the handler.
                                        If textBox IsNot Nothing Then
                                            AddHandler textBox.Enter, AddressOf TextBox_Enter
                                            AddHandler textBox.Leave, AddressOf TextBox_Leave
                                        End If
                                    End If
                                Next
                            End If
                        Next


                    End If

                    ''For Panel


                    If (subcontrol.GetType() Is GetType(Panel)) Then
                        For Each subcontrolTwo As Control In subcontrol.Controls
                            If (subcontrolTwo.GetType() Is GetType(TextBox)) Then
                                Dim textBox As TextBox = TryCast(subcontrolTwo, TextBox)

                                ' If not null, set the handler.
                                If textBox IsNot Nothing Then
                                    AddHandler textBox.Enter, AddressOf TextBox_Enter
                                    AddHandler textBox.Leave, AddressOf TextBox_Leave
                                End If
                            End If

                        Next


                    End If
                    If (subcontrol.GetType() Is GetType(TextBox)) Then
                        Dim textBox As TextBox = TryCast(subcontrol, TextBox)

                        ' If not null, set the handler.
                        If textBox IsNot Nothing Then
                            AddHandler textBox.Enter, AddressOf TextBox_Enter
                            AddHandler textBox.Leave, AddressOf TextBox_Leave
                        End If
                    End If
                    ' Set the handler.
                Next

            ElseIf (control.GetType() Is GetType(TabControl)) Then
                Dim TC As TabControl = TryCast(control, TabControl)
                parentControl = TC
            ElseIf (control.GetType() Is GetType(Panel)) Then
                Dim Panel As Panel = TryCast(control, Panel)
                parentControl = Panel

            Else
                Try
                    parentControl = control
                Catch ex As Exception

                End Try

            End If

            For Each subcontrol As Control In parentControl.Controls
                If (subcontrol.GetType() Is GetType(TabControl)) Then
                    For Each subcontrolTwo As Control In subcontrol.Controls
                        If (subcontrolTwo.GetType() Is GetType(TabPage)) Then
                            For Each subcontrolthree As Control In subcontrolTwo.Controls
                                If (subcontrolthree.GetType() Is GetType(TextBox)) Then
                                    Dim textBox As TextBox = TryCast(subcontrolthree, TextBox)

                                    ' If not null, set the handler.
                                    If textBox IsNot Nothing Then
                                        AddHandler textBox.Enter, AddressOf TextBox_Enter
                                        AddHandler textBox.Leave, AddressOf TextBox_Leave
                                    End If
                                End If
                            Next
                        End If
                    Next


                End If
                If (subcontrol.GetType() Is GetType(TextBox)) Then
                    Dim textBox As TextBox = TryCast(subcontrol, TextBox)

                    ' If not null, set the handler.
                    If textBox IsNot Nothing Then
                        AddHandler textBox.Enter, AddressOf TextBox_Enter
                        AddHandler textBox.Leave, AddressOf TextBox_Leave

                    End If

                End If

                ' Set the handler.
            Next
            ' Set the handler.
        Next

    End Sub
    Private Sub TextBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)

        txt.BackColor = Color.LightYellow
    End Sub
    Private Sub TextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub TextBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)

        txt.BackColor = Color.White
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveAllData()
        btnRefresh_Click(Nothing, Nothing)


    End Sub

    Public Sub SaveAllData()
        Try

            'Save All Data
            If Address_ID > 0 Then
                'save main Order Details.

                If txtRecMKT.Text <> "" And dtOrderDate.Text = "" Then
                    MessageBox.Show("Enter OrderDate ,Beacuase after ordredate ,Accept Rec MKT Date")
                    Exit Sub
                End If

                Dim resorder As Integer
                resorder = linq_obj.SP_Insert_Tbl_OrderOneMaster(txtOrderNo.Text, dtEntryDate.Text, txtPONo.Text, txtOrderNo.Text, Convert.ToDateTime(If(dtOrderDate.Text = "", "01-01-1900 00:00:00", dtOrderDate.Text)), Convert.ToDateTime(If(dtDispatchDate.Text = "", "01-01-1900 00:00:00", dtDispatchDate.Text)), txtPartyName.Text,
                                                              txtBatchName.Text, Address_ID, txtOrderRec.Text, txtOrderFollowBy.Text, cmbOrderStatus.Text, Convert.ToDateTime(If(txtRecMKT.Text = "", "01-01-1900 00:00:00", txtRecMKT.Text)))
                If (resorder > 0) Then
                    linq_obj.SubmitChanges()
                End If

                'Update Contact Detail
                Dim resContact As Integer
                resContact = linq_obj.SP_UpdateAddressContactDetail(txtcoPerson.Text, txtContactNo.Text, txtOrdermastEmail.Text, txtEmail1.Text, txtEmail2.Text, Address_ID)
                linq_obj.SubmitChanges()

                'bind Address Detail
                Dim resAddress As Integer
                resAddress = linq_obj.SP_UpdateAddress(txtPartyName.Text, txtBillAdresss.Text, txtArea.Text, txtCity.Text, txtPincode.Text, txtTaluka.Text, txtDistrict.Text, txtState.Text, txtDeladress.Text, txtDelArea.Text, txtDelCity.Text, txtDelPincode.Text, txtDelTaluka.Text, txtDelDistrict.Text, txtDelState.Text, Address_ID)
                If resAddress >= 0 Then
                    linq_obj.SubmitChanges()
                End If

                'bind Project Information

                Dim resProjectId As Integer
                resProjectId = linq_obj.SP_Insert_Tbl_ProjectInformationMaster(txtPlantName.Text, txtModel.Text, txtProject.Text, txtCapacity.Text, txtPowerAvail.Text, txtPlantShape.Text,
                                                                             txtLandArea.Text, txtDtype.Text, txttentiveSchem.Text, txtJarDis.Text, txtBmouldDis.Text, Convert.ToDateTime(If(dtTentativeDate.Text = "", "01-01-1900 00:00:00", dtTentativeDate.Text)), Address_ID)

                'Save Folloup Details
                Dim resFollowupMaster As Integer

                resFollowupMaster = linq_obj.SP_Insert_Tbl_OrderFollowupMaster(txtProjectDetail.Text, Address_ID)
                If (resFollowupMaster > 0) Then 
                    linq_obj.SubmitChanges() 
                End If


                
                'Save Visitor Detail
                'Save Visitor Detail
                Dim str1, str2, str3 As String
                Dim ms1 As New MemoryStream
                Dim ms2 As New MemoryStream
                Dim ms3 As New MemoryStream
                Dim ms4 As New MemoryStream

                Dim Pic1Data As Byte()
                Pic1Data = Nothing

                If Not Pic1.Image Is Nothing Then
                    Pic1.Image.Save(ms1, Pic1.Image.RawFormat)
                    Pic1Data = ms1.GetBuffer()
                End If

                Dim Pic2Data As Byte()

                If Not Pic2.Image Is Nothing Then
                    Pic2.Image.Save(ms2, Pic2.Image.RawFormat)
                    Pic2Data = ms2.GetBuffer()
                End If

                Dim Pic3Data As Byte()
                If Not Pic3.Image Is Nothing Then
                    Pic3.Image.Save(ms3, Pic3.Image.RawFormat)
                    Pic3Data = ms3.GetBuffer()
                End If

                Dim Pic4Data As Byte()
                If Not Pic4.Image Is Nothing Then
                    Pic4.Image.Save(ms4, Pic4.Image.RawFormat)
                    Pic4Data = ms4.GetBuffer()
                End If

                'Update Visitor Detail

                Dim VisitorPhoto = linq_obj.SP_insert_Update_Tbl_OrderVisitorDetail_New(0,
                                                                  Address_ID,
                                                                  Pic1Data,
                                                                  txtPh11.Text,
                                                                  txtPh12.Text,
                                                                  txtPh13.Text,
                                                                  txtPh14.Text,
                                                                  txtPh15.Text,
                                                                  txtPh16.Text,
                                                                  Pic2Data,
                                                                  txtPH21.Text,
                                                                  txtPH22.Text,
                                                                  txtPH23.Text,
                                                                  txtPH24.Text,
                                                                  txtPH25.Text,
                                                                  txtPH26.Text,
                                                                  Pic3Data,
                                                                  txtPH31.Text,
                                                                  txtPH32.Text,
                                                                  txtPH33.Text,
                                                                  txtPH34.Text,
                                                                  txtPH35.Text,
                                                                  txtPH36.Text,
                                                                  Pic4Data,
                                                                  txtph41.Text,
                                                                  txtph42.Text,
                                                                  txtph43.Text,
                                                                  txtph44.Text,
                                                                  txtph45.Text,
                                                                  txtph46.Text)
                linq_obj.SubmitChanges()



                'Dim resPlan As Integer
                'resPlan = linq_obj.SP_Tbl_OrderPlanDrawing_Insert(txtDR11.Text, txtDR12.Text, path1, txtDR21.Text, txtDR22.Text, path2, txtDR31.Text, txtDR32.Text, path3, Address_ID)
                'If (resPlan > 0) Then
                '    linq_obj.SubmitChanges()
                'End If

                ''/ Letter Header  11/12/2017 Navin


                'linq_obj.SP_Delete_Tbl_ProductInstallationMaster_Two(Address_ID)
                'linq_obj.SubmitChanges()
                ''/ Product Instalation
                'For i As Integer = 0 To TblProInst.Rows.Count - 1
                '    linq_obj.SP_Insert_Tbl_ProductInstallationMaster_Two(Address_ID,
                '    Convert.ToDateTime(TblProInst.Rows(i)("PDate").ToString()),
                '    Convert.ToDateTime(TblProInst.Rows(i)("Dis_Date").ToString()),
                '    TblProInst.Rows(i)("Product_Name").ToString(),
                '    TblProInst.Rows(i)("Vendor_Name").ToString(),
                '    TblProInst.Rows(i)("Station").ToString(),
                '    TblProInst.Rows(i)("Send_CU_To").ToString(),
                '    TblProInst.Rows(i)("Rec_CU_From").ToString(),
                '    TblProInst.Rows(i)("CU_To_Venue").ToString(),
                '    Convert.ToDateTime(TblProInst.Rows(i)("Comp_Date_With_Inst").ToString()),
                '    TblProInst.Rows(i)("By_Whom").ToString(),
                '    TblProInst.Rows(i)("Remark").ToString())
                '    linq_obj.SubmitChanges()
                'Next


                '/ISI Header
                Dim resisiprocess As Integer
                resisiprocess = linq_obj.SP_Insert_Tbl_ISIProcessMaster_Two(Address_ID,
                                                            TxtScheme.Text,
                                                            Convert.ToDateTime(If(TxtRecDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtRecDate.Text)),
                                                              Convert.ToDateTime(If(TxtDocFDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtDocFDate.Text)),
                                                                 Convert.ToDateTime(If(TxtDocRDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtDocRDate.Text)),
                                                                 TxtSToP.Text, TxtFSubmit.Text,
                                                                Convert.ToDateTime(If(TxtFileRegDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtFileRegDate.Text)),
                                                                  Convert.ToDateTime(If(TxtBISInspDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtBISInspDate.Text)),
                                                                   Convert.ToDateTime(If(TxtLicenceDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtLicenceDate.Text)),
                                                                   TxtVender.Text, TxtRemark.Text,
                                                             Convert.ToDateTime(If(txtVendorDate.Text.Trim() = "", "01-01-1900 00:00:00", txtVendorDate.Text)),
                                                              Convert.ToDateTime(If(txtVendorRecDate.Text.Trim() = "", "01-01-1900 00:00:00", txtVendorRecDate.Text)))
                If (resisiprocess > 0) Then
                    linq_obj.SubmitChanges()

                    '/ ISI Description 11/12/2017 Navin

                    'linq_obj.SP_Delete_Tbl_ISIProcess_DetailMaster_Two(Address_ID)
                    'linq_obj.SubmitChanges()
                    'For i As Integer = 0 To TblISIDesc.Rows.Count - 1
                    '    linq_obj.SP_Insert_Tbl_ISIProcess_DetailMaster_Two(Address_ID,
                    '                                                    Convert.ToDateTime(TblISIDesc.Rows(i)("F_Date").ToString()),
                    '                                                    TblISIDesc.Rows(i)("Followup").ToString(),
                    '                                                     Convert.ToDateTime(TblISIDesc.Rows(i)("N_F_FollowpDate").ToString()),
                    '                                                   TblISIDesc.Rows(i)("Status").ToString(),
                    '                                                   TblISIDesc.Rows(i)("ByWhom").ToString(),
                    '                                                    TblISIDesc.Rows(i)("Pro_Type").ToString(),
                    '                                                   TblISIDesc.Rows(i)("Remarks").ToString())
                    '    linq_obj.SubmitChanges()
                    'Next
                End If


                'Allotment
                Dim resAllotment As Integer

                If (cmbUser.SelectedValue > 0 And cmbTeam.SelectedValue > 0) Then


                    resAllotment = linq_obj.SP_Tbl_UserAllotmentDetail_Insert(Address_ID, Convert.ToInt32(cmbUser.SelectedValue), Convert.ToInt32(cmbTeam.SelectedValue))
                    If (resAllotment > 0) Then
                        ' MessageBox.Show("Successfully Alloted To  Team : " + cmbTeam.Text + " and User : " + cmbUser.Text)
                    Else
                        ' MessageBox.Show("Already Alloted...")
                    End If
                End If

                MessageBox.Show("Successfully Saved")
                clearAll()
                btnSave.Enabled = False
            Else
                MessageBox.Show("No Address Informations Found")
            End If

        Catch ex As Exception
            MessageBox.Show("Error :" + ex.Message)
        End Try
    End Sub

    Private Sub btnAddFolowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFolowup.Click
        txtSerType.Text = ""
        txtComplainNo.Text = ""
        txtAtendedBy.Text = ""
        txtEnginer.Text = ""
        txtFolowup1.Text = ""
        txtstatus.Text = ""
        txtSfolwRemarks.Text = ""
        dtSFDate.Value = DateTime.Now
        dtAttendDate.Value = DateTime.Now
        dtTentativeAttendDate.Value = DateTime.Now
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Public Sub BindAllGridData()
        ' clearAll()
        Try
            GetClientDetails_Bind()
            BindAllData()
            TeamId = cmbTeam.SelectedValue
            bindGrid()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGVOrderDetails_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVOrderDetails.DoubleClick
        clearAll()
     
        If (Me.DGVOrderDetails.SelectedRows.Count > 0) Then
            Address_ID = Convert.ToInt64(Me.DGVOrderDetails.SelectedCells(0).Value)
            BindAllGridData()
            'Packing Detail List 03-08-2016
            GvPackingDetailNewList_Bind()
            GvFileList_Bind()
            EnqClient_Bio_Data()

            GvEnqFollowp_Bind()
            ddlPipe_ProjectType_Bind()
        End If
    End Sub

    Public Sub ddlPipe_ProjectType_Bind()

        Dim AssignProject = linq_obj.SP_Get_Pipeline_ProjectDepartment_ProjectType_Assign_By_Address_ID(Address_ID).ToList()

        Dim datatable As New DataTable
        datatable.Columns.Add("Fk_Project_Type_Id")
        datatable.Columns.Add("ProjectType")

        For Each item As SP_Get_Pipeline_ProjectDepartment_ProjectType_Assign_By_Address_IDResult In AssignProject
            datatable.Rows.Add(item.Fk_Project_Type_Id, item.ProjectType)
        Next
        Dim newRow As DataRow = datatable.NewRow()
        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        ddlPipe_ProjectType.DataSource = datatable
        ddlPipe_ProjectType.DisplayMember = "ProjectType"
        ddlPipe_ProjectType.ValueMember = "Fk_Project_Type_Id"
        ddlPipe_ProjectType.AutoCompleteMode = AutoCompleteMode.Append
        ddlPipe_ProjectType.DropDownStyle = ComboBoxStyle.DropDownList
        ddlPipe_ProjectType.AutoCompleteSource = AutoCompleteSource.ListItems

    End Sub

    Public Sub Pipeline_Milestone_Bind()
        Gv_Project_Log.DataSource = Nothing
        ddlMileStone.DataSource = Nothing
        ChkEngineerList.Items.Clear()
        chkMachineList.Items.Clear()
        
        'Steps Bind 
        Dim dtSteps As New DataTable
        dtSteps.Columns.Add("Pk_Milestone_ID")
        dtSteps.Columns.Add("Steps_Name")

        Dim Department As Integer
        Department = 2
        'Pipeline Deaprtment 2 = Project 
        'Pipeline Project Type 8 = EC
        Dim dataMilestone = linq_obj.SP_Get_Pipline_ProjectDepartment_Milestone_By_Address_ID(Department, Convert.ToInt32(ddlPipe_ProjectType.SelectedValue), Address_ID).ToList()
        For Each item As SP_Get_Pipline_ProjectDepartment_Milestone_By_Address_IDResult In dataMilestone
            FK_ProjectMaster_ID = item.PK_PipelineProjectMaster_ID
            dtSteps.Rows.Add(item.Pk_Milestone_ID, item.Steps_Name)
        Next
        dtSteps = dtSteps.DefaultView.ToTable(True, "Pk_Milestone_ID", "Steps_Name")
        ddlMileStone.DataSource = dtSteps
        ddlMileStone.DisplayMember = "Steps_Name"
        ddlMileStone.ValueMember = "Pk_Milestone_ID"

        ddlMileStone.AutoCompleteMode = AutoCompleteMode.Append
        ddlMileStone.DropDownStyle = ComboBoxStyle.DropDownList
        ddlMileStone.AutoCompleteSource = AutoCompleteSource.ListItems
        Gv_Project_Log_Bind()


    End Sub

    Public Function Datecheck(ByVal date1 As String) As String
        Dim finalDt = ""
        If (Convert.ToString(date1) <> "") Then
            If (date1.ToString().Contains("1900")) Then
                finalDt = ""
            Else
                finalDt = Convert.ToDateTime(date1).ToShortDateString()
            End If
        End If
        Return finalDt
    End Function

    Public Sub BindAllData()

        Dim dataOrderDetail = linq_obj.Sp_GetOrderRecFollowDetail(Address_ID).ToList()
        If (dataOrderDetail.Count > 0) Then
            For Each item As Sp_GetOrderRecFollowDetailResult In dataOrderDetail
                txtOrderRec.Text = Convert.ToString(item.RecBy)
                txtOrderFollowBy.Text = Convert.ToString(item.FollowBy)
            Next
        End If
        Dim dataEntryDetail = linq_obj.Sp_GetOrderEntryDate(Address_ID).ToList()
        If (dataEntryDetail.Count > 0) Then
            dtEntryDate.Value = Convert.ToDateTime(IIf(dataEntryDetail(0).F_Date Is Nothing, Date.Now, dataEntryDetail(0).F_Date)).ToShortDateString()
        End If
        'allotment bind 
        Dim dataAlott = linq_obj.SP_Get_AllotedTeamUser(Address_ID).ToList().Where(Function(p) p.Fk_TeamId = Class1.global.TeamID And p.Fk_UserId = Class1.global.UserID).ToList()

        If (dataAlott.Count) Then
            cmbTeam.SelectedValue = dataAlott(0).Fk_TeamId
            cmbUser.SelectedValue = dataAlott(0).Fk_UserId
        End If
        Dim dataOrder = linq_obj.SP_Select_All_Tbl_OrderOneMaster(Address_ID).ToList()
        If (dataOrder.Count > 0) Then
            btnSave.Enabled = False
            'bind Main Order Data
            For Each item As SP_Select_All_Tbl_OrderOneMasterResult In dataOrder
                dtEntryDate.Text = Convert.ToDateTime(IIf(item.EntryDate Is Nothing, Date.Now, item.EntryDate)).ToShortDateString()
                txtPONo.Text = item.PONo
                txtRecMKT.Text = Datecheck(Convert.ToString(item.OrderRecFromMkt))
                dtOrderDate.Text = Datecheck(Convert.ToString(item.OrderDate))
                dtDispatchDate.Text = Datecheck(Convert.ToString(item.DispatchDate))
                txtBatchName.Text = item.BrandName
                txtOrderFollowBy.Text = item.OrderFollowBy
                txtOrderRec.Text = item.OrderRecBy
                cmbOrderStatus.Text = item.OrderStatus

            Next
            'bind project Detail
            Dim dataprojectMaster = linq_obj.SP_Select_Tbl_ProjectInformationMaster(Address_ID).ToList()
            If (dataprojectMaster.Count > 0) Then
                For Each item As SP_Select_Tbl_ProjectInformationMasterResult In dataprojectMaster
                    txtPlantName.Text = item.PlantName
                    txtModel.Text = item.Model
                    txtProject.Text = item.ProjectName
                    txtCapacity.Text = item.Capacity
                    txtPowerAvail.Text = item.PowerAvailable
                    txtPlantShape.Text = item.PlantShape
                    txtLandArea.Text = item.LandArea
                    txtDtype.Text = item.DType
                    txttentiveSchem.Text = item.TreatmentScheme
                    txtJarDis.Text = item.JarDis
                    txtBmouldDis.Text = item.BMouldDis
                    dtTentativeDate.Text = item.TentativeDispatch

                    dtTentativeAttendDate.Text = Datecheck(Convert.ToString(item.TentativeDispatch))
                Next
            End If

            'Bind Project Information 20-07-2017 Navin 


            DGVOrderdtail_All_Data()

            'Auto complated Text box Plant name in Project information
            DGVOrderdtail_Bind()

            'get FollowUp Data
            Dim dataFollowUp = linq_obj.SP_Select_Tbl_OrderFollowupMasterByOrder(Address_ID).ToList()
            If (dataFollowUp.Count > 0) Then
                txtProjectDetail.Text = dataFollowUp(0).ProjectDetail
            End If

            ' FollowUp Data 19-07-2017  navin
            DGVfollowUp_Bind()

            'get Service Follow Up.

            Dim dataservicefolloupDetail = linq_obj.SP_Select_Tbl_OrderServiceFollowUpDetailByOrder(Address_ID).ToList()
            If (dataservicefolloupDetail.Count > 0) Then

                tblSFollowup.Clear()
                For Each item As SP_Select_Tbl_OrderServiceFollowUpDetailByOrderResult In dataservicefolloupDetail
                    Dim dr As DataRow
                    dr = tblSFollowup.NewRow()
                    dr("Date") = item.SFDate
                    dr("ServiceType") = item.ServiceType
                    dr("ComplainNo") = item.ComplainNo
                    dr("AttendDate") = item.AttendDate
                    dr("AttendBy") = item.AttendBy
                    dr("Engineer") = item.Engineer
                    dr("FollowUp") = item.FollowUp
                    dr("TentativeDate") = item.TentativeADate
                    dr("Status") = item.Status
                    dr("Remarks") = item.Remarks
                    tblSFollowup.Rows.Add(dr)
                Next
                DGVFolowup.DataSource = tblSFollowup

            End If

            'get visitor detail

            'get PlantDrawing detail
            Dim dataPlantDetail = linq_obj.SP_Tbl_OrderPlanDrawing_Select(Address_ID).ToList()
            If (dataPlantDetail.Count > 0) Then
                For Each item As SP_Tbl_OrderPlanDrawing_SelectResult In dataPlantDetail
                Next
            End If

            '/ Fill Data by Tbl_LetterMailComMaster_Two Table
            Dim LetterH = linq_obj.SP_Select_Tbl_LetterMailComMaster_Two(Address_ID).ToList()
            If LetterH.Count > 0 Then
                TxtLetterProDetail.Text = LetterH(0).ProjectDetail
            End If

            TblLetter.Clear()
            '/ Fill Data by Tbl_LetterMailComMaster_Detail_Two Table

            Latter_Email_Bind() '19-07-2017 Navin

            TblProInst.Clear()
            '/ Fill Data by Tbl_ProductInstallationMaster_Two Table
            Dim ProInst = linq_obj.SP_Select_Tbl_ProductInstallationMaster_Two(Address_ID).ToList()
            If ProInst.Count > 0 Then
                For Each item As SP_Select_Tbl_ProductInstallationMaster_TwoResult In ProInst
                    Dim dr As DataRow
                    dr = TblProInst.NewRow()
                    dr("PDate") = Class1.Datecheck(item.PDate)
                    dr("Dis_Date") = Class1.Datecheck(item.Dis_Date)
                    dr("Product_Name") = item.Product_Name
                    dr("Vendor_Name") = item.Vendor_Name
                    dr("Station") = item.Station
                    dr("Send_CU_To") = item.Send_CU_To
                    dr("Rec_CU_From") = item.Rec_CU_From
                    dr("CU_To_Venue") = item.CU_To_Venue
                    dr("Comp_Date_With_Inst") = Class1.Datecheck(item.Comp_Date_With_Inst)
                    dr("By_Whom") = item.By_Whom
                    dr("Remark") = item.Remark
                    TblProInst.Rows.Add(dr)
                Next
                DataProInst.DataSource = TblProInst
            End If
            '/ Fill Data by Tbl_ProductInstallationMaster_Two Table
            Dim ISI = linq_obj.SP_Select_Tbl_ISIProcessMaster_Two(Address_ID).ToList()
            If ISI.Count > 0 Then
                TxtScheme.Text = ISI(0).Scheme_Name
                TxtRecDate.Text = Datecheck(Convert.ToString(ISI(0).D_Rec_Date))
                TxtDocFDate.Text = Datecheck(Convert.ToString(ISI(0).P_Doc_F_Date))
                TxtDocRDate.Text = Datecheck(Convert.ToString(ISI(0).P_Doc_R_Date))
                TxtSToP.Text = ISI(0).F_Ok_S_Tc_P
                TxtFSubmit.Text = ISI(0).F_Submit_P
                TxtFileRegDate.Text = Datecheck(Convert.ToString(ISI(0).File_Reg_Date))
                TxtBISInspDate.Text = Datecheck(Convert.ToString(ISI(0).BIS_Insp_Date))
                TxtLicenceDate.Text = Datecheck(Convert.ToString(ISI(0).Licence_Date))
                TxtVender.Text = ISI(0).Vender
                TxtRemark.Text = ISI(0).ISIRemark
                txtVendorDate.Text = Convert.ToDateTime(ISI(0).Vendor_Date)
                txtVendorRecDate.Text = Convert.ToDateTime(ISI(0).Receive_Date)

            End If
            TblISIDesc.Clear()
            '/ Fill Data by Tbl_ISIProcess_DetailMaster_Two Table
            Dim ISIDes = linq_obj.SP_Select_Tbl_ISIProcess_DetailMaster_Two(Address_ID).ToList()
            If ISIDes.Count > 0 Then
                For Each item As SP_Select_Tbl_ISIProcess_DetailMaster_TwoResult In ISIDes
                    Dim dr As DataRow
                    dr = TblISIDesc.NewRow()
                    dr("F_Date") = Class1.Datecheck(item.FDate)
                    dr("Followup") = item.FollowUp
                    dr("N_F_FollowpDate") = Class1.Datecheck(item.NFDate)
                    dr("Status") = item.Status
                    dr("ByWhom") = item.ByWhom
                    dr("Pro_Type") = item.ProjectType
                    dr("Remarks") = item.Remarks
                    TblISIDesc.Rows.Add(dr)
                Next
                DataISIGrid.DataSource = TblISIDesc
            End If
            TblRawMaterial.Clear()
            'RawMaterail  
            DGRawMaterialData_Bind()
            'Latter Log 20-07-2017 Navin 
            Latter_Log_Bind()
        Else
            btnSave.Enabled = True
        End If



    End Sub
    Public Sub Latter_Email_Bind()
        Dim LetterDesc = linq_obj.SP_Select_Tbl_LetterMailComMaster_Detail_Two(Address_ID).ToList()
        DataLetter.DataSource = LetterDesc
        DataLetter.Columns(0).Visible = False
        DataLetter.Columns(1).Visible = False

    End Sub
    Public Sub Latter_Log_Bind()
        Dim logdata = linq_obj.SP_Tbl_ProjectLetterLog_Select(Address_ID).ToList()
        dgLog.DataSource = logdata
        dgLog.Columns(0).Visible = False
        dgLog.Columns(10).Visible = False
    End Sub

    Public Sub DGVOrderdtail_All_Data()

        Dim dt As New DataTable
        dt.Columns.Add("Pk_ProjctDetailId")
        dt.Columns.Add("Fk_AddressId")
        dt.Columns.Add("SrNo")
        dt.Columns.Add("PlantScheme")
        dt.Columns.Add("VendorName")
        dt.Columns.Add("EntryDate")
        Dim SRNo As Integer
        SRNo = 1
        Dim dataProjectDetail = linq_obj.SP_Select_Tbl_ProjectDetail(Address_ID).ToList()
        For Each item As SP_Select_Tbl_ProjectDetailResult In dataProjectDetail
            dt.Rows.Add(item.Pk_ProjctDetailId, item.Fk_AddressId, SRNo, item.PlantScheme, item.VendorName, Convert.ToDateTime(item.DispatchDate).ToString("dd/MM/yyyy"))
            SRNo = SRNo + 1
        Next
        DGVOrderdtail.DataSource = dt
        txtSrNo.Text = DGVOrderdtail.Rows.Count + 1
        DGVOrderdtail.Columns(0).Visible = False
        DGVOrderdtail.Columns(1).Visible = False
        DGVOrderdtail.Columns(4).Visible = False
    End Sub
    Public Sub DGRawMaterialData_Bind()
        TblRawMaterial.Clear()
        Dim rawMaterialData = linq_obj.SP_Tbl_OrderRawMaterialDetail_Select(Address_ID).ToList()
        If rawMaterialData.Count > 0 Then
            For Each item As SP_Tbl_OrderRawMaterialDetail_SelectResult In rawMaterialData
                Dim dr As DataRow
                dr = TblRawMaterial.NewRow()
                dr("Pk_OrderRawMaterialId") = item.Pk_OrderRawMaterialId
                dr("ItemName") = item.ItemName
                dr("Qty") = item.Qty
                dr("Price") = item.Rate
                dr("Amount") = item.Amount
                dr("OrderConfirm") = If(item.IsOrderConfirm = True, "YES", "NO")
                dr("PaymentReceived") = If(item.IsPaymentReceived = True, "YES", "NO")
                dr("OrderDate") = Class1.Datecheck(item.OrderDate)
                dr("DisDate") = Class1.Datecheck(item.DisDate)
                dr("Ten_DisDate") = Class1.Datecheck(item.TenDate)
                TblRawMaterial.Rows.Add(dr)

            Next
            DGRawMaterialData.DataSource = TblRawMaterial
            DGRawMaterialData.Columns(0).Visible = False

        End If
    End Sub



    Private Sub btnsavefolwup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavefolwup.Click
        ''add new row runtime and display into grid. it will save after click on save button
        Dim dr As DataRow
        dr = tblSFollowup.NewRow()
        dr("Date") = dtSFDate.Text
        dr("ServiceType") = txtSerType.Text
        dr("ComplainNo") = txtComplainNo.Text
        dr("AttendDate") = dtAttendDate.Text
        dr("AttendBy") = txtAtendedBy.Text
        dr("Engineer") = txtEnginer.Text
        dr("FollowUp") = txtFolowup1.Text
        dr("TentativeDate") = dtTentativeAttendDate.Text
        dr("Status") = txtstatus.Text
        dr("Remarks") = txtSfolwRemarks.Text
        linq_obj.SP_Insert_Tbl_OrderServiceFollowUpDetail(dtSFDate.Text,
                                                               txtSerType.Text,
                                                              txtComplainNo.Text,
                                                               dtAttendDate.Text,
                                                               txtAtendedBy.Text,
                                                               txtEnginer.Text,
                                                               txtFolowup1.Text,
                                                               dtTentativeAttendDate.Text,
                                                               txtstatus.Text,
                                                               txtSfolwRemarks.Text,
                                                               Address_ID)

        tblSFollowup.Rows.Add(dr)
        DGVFolowup.DataSource = tblSFollowup
        txtSerType.Text = ""
        txtComplainNo.Text = ""
        txtAtendedBy.Text = ""
        txtEnginer.Text = ""
        txtFolowup1.Text = ""
        txtstatus.Text = ""
        txtSfolwRemarks.Text = ""
        dtSFDate.Value = DateTime.Now
        dtAttendDate.Value = DateTime.Now
        dtTentativeAttendDate.Value = DateTime.Now
        txtFolowup3.Text = ""
    End Sub

    Private Sub btnSaveFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFollowup.Click
        ''add new row runtime and display into grid. it will save after click on save button
        Try

            If (txtFolloupDetail.Text.Trim() = "") Then
                MessageBox.Show("Follow Up Cannot Be Blank..")
                Return
            ElseIf (txtorderstatus.Text.Trim() = "") Then
                MessageBox.Show("Status Cannot Be Blank..")
                Return
            ElseIf (txtBWHOM.Text.Trim() = "") Then
                MessageBox.Show("ByWhom Cannot Be Blank..")
                Return

            ElseIf (txtProType.Text.Trim() = "") Then
                MessageBox.Show("Pro.Type Cannot Be Blank..")
                Return

            Else

                If (Address_ID > 0) Then

                    If btnSaveFollowup.Text = "Save" Then
                        linq_obj.SP_Insert_Tbl_OrderFollowupDetail(Address_ID, Convert.ToDateTime(dtFollowDate.Text), txtFolloupDetail.Text, Convert.ToDateTime(dtNFDate.Text), txtorderstatus.Text, txtBWHOM.Text, txtProType.Text, txtRemarksFollowup.Text)
                        linq_obj.SubmitChanges()
                        MessageBox.Show("Add Sucessfully...")
                    Else
                        linq_obj.SP_Update_Tbl_OrderFollowupDetailnew(Pk_OrderFollowupDetailId, Address_ID, Convert.ToDateTime(dtFollowDate.Text), txtFolloupDetail.Text, Convert.ToDateTime(dtNFDate.Text), txtorderstatus.Text, txtBWHOM.Text, txtProType.Text, txtRemarksFollowup.Text)
                        linq_obj.SubmitChanges()
                        MessageBox.Show("Update Sucessfully...")
                    End If

                    Clear_Followp_text()
                    DGVfollowUp_Bind()

                Else
                    MessageBox.Show("EnqNo Not Found...")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub Clear_Followp_text()


        txtRemarksFollowup.Text = ""
        txtorderstatus.Text = ""
        txtFolloupDetail.Text = ""
        txtBWHOM.Text = ""
        txtProType.Text = ""
        txtDaysAfter.Text = ""
        btnSaveFollowup.Text = "Save"
        dtFollowDate.Text = System.DateTime.Now.ToString()
        Pk_OrderFollowupDetailId = 0
        btnAddFollowup.Focus()

    End Sub
    Private Sub btnsaveOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsaveOrder.Click
        ''add new row runtime and display into grid. it will save after click on save button
        Try

            'Check Data First Time
            Dim dataProjectDetail = linq_obj.SP_Select_Tbl_ProjectDetail(Address_ID).ToList()
            If dataProjectDetail.Count = 0 And DGVOrderdtail.Rows.Count > 0 Then
                For index = 0 To DGVOrderdtail.Rows.Count - 1
                    linq_obj.SP_Insert_Tbl_ProjectDetail(Address_ID, Convert.ToInt32(DGVOrderdtail.Rows(index).Cells("SrNo").Value), DGVOrderdtail.Rows(index).Cells("PlantScheme").Value, txtVendorName.Text, Class1.Datecheck(System.DateTime.Now.ToShortDateString()))
                    linq_obj.SubmitChanges()
                Next
                MessageBox.Show("Add Sucessfully...")
            Else

                If (txtSrNo.Text.Trim() = "") Then
                    MessageBox.Show("Sr.No Cannot Be Blank..")
                    Return

                ElseIf (txtPlantScheme.Text.Trim() = "") Then
                    MessageBox.Show("Plant Scheme Cannot Be Blank..")
                    Return

                Else

                    If (Address_ID > 0) Then
                        If btnsaveOrder.Text = "Save" Then
                            linq_obj.SP_Insert_Tbl_ProjectDetail(Address_ID, txtSrNo.Text, txtPlantScheme.Text, txtVendorName.Text, Class1.Datecheck(dtDisDate.Text))
                            linq_obj.SubmitChanges()
                            MessageBox.Show("Add Sucessfully...")
                        Else
                            linq_obj.SP_Update_Tbl_ProjectDetail_New(Pk_ProjctDetailId, Address_ID, txtSrNo.Text, txtPlantScheme.Text, txtVendorName.Text, Class1.Datecheck(dtDisDate.Text))
                            linq_obj.SubmitChanges()
                            MessageBox.Show("Update Sucessfully...")

                        End If

                        'SrNo Auto 
                        txtPlantScheme.Text = ""
                        txtVendorName.Text = ""
                        txtSrNo.Text = ""
                        btnsaveOrder.Text = "Save"
                        btnAddOrder.Focus()
                        DGVOrderdtail_All_Data()
                    Else
                        MessageBox.Show("EnqNo Not Found...")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAddOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOrder.Click
        txtSrNo.Focus()
        txtPlantScheme.Text = ""
        txtVendorName.Text = ""
        btnsaveOrder.Text = "Save"
        txtSrNo.Text = DGVOrderdtail.Rows.Count + 1

    End Sub

    Private Sub btnDelFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelFollowup.Click
        If DGVfollowUp.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then

                linq_obj.SP_Delete_Tbl_OrderFollowupDetail_new(Convert.ToInt64(DGVfollowUp.SelectedCells(0).Value))
                linq_obj.SubmitChanges()
                DGVfollowUp_Bind()
                Clear_Followp_text()
            End If
        End If
    End Sub
    Public Sub GridSetting(ByVal Gd As DataGridView)
        Gd.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub
    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFollowup.Click
        Clear_Followp_text()
    End Sub

    Private Sub btnDelfolwup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelfolwup.Click
        If DGVFolowup.Rows.Count > 0 Then
            tblSFollowup.Rows(DGVFolowup.CurrentRow.Index).Delete()
            DGVFolowup.DataSource = tblSFollowup
        End If
    End Sub

    Private Sub btnBrowse1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        Pic1.SizeMode = PictureBoxSizeMode.StretchImage
        Pic1.ImageLocation = imgSrc
        Pic1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub btnBrowse2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        Pic2.SizeMode = PictureBoxSizeMode.StretchImage
        Pic2.ImageLocation = imgSrc
        Pic2.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub btnBrowse3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        Pic3.SizeMode = PictureBoxSizeMode.StretchImage
        Pic3.ImageLocation = imgSrc
        Pic3.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        UpdateAll()
    End Sub

    Public Sub EnqClient_Bio_Data()


        'Get Order Visitor Data

        Dim DataOrder = linq_obj.SP_Get_Tbl_OrderVisitorDetail_New(Address_ID).ToList()
        If DataOrder.Count > 0 Then
            For Each item As SP_Get_Tbl_OrderVisitorDetail_NewResult In DataOrder
                Try
                    Dim image1Data As Byte() = IIf(item.Photo1 = Nothing, Nothing, item.Photo1.ToArray())
                    If Not image1Data Is Nothing Then
                        msPic1 = New MemoryStream(image1Data, 0, image1Data.Length)
                        msPic1.Write(image1Data, 0, image1Data.Length)
                        Pic1.Image = Image.FromStream(msPic1, True)
                        Pic1.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                    Dim image2Data As Byte() = IIf(item.Photo2 = Nothing, Nothing, item.Photo2.ToArray())
                    If image2Data.Length <> 0 Then
                        msPic2 = New MemoryStream(image2Data, 0, image2Data.Length)
                        msPic2.Write(image2Data, 0, image2Data.Length)
                        Pic2.Image = Image.FromStream(msPic2, True)
                        Pic2.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                    Dim image3Data As Byte() = IIf(item.Photo3 = Nothing, Nothing, item.Photo3.ToArray())
                    If image3Data.Length <> 0 Then
                        msPic3 = New MemoryStream(image3Data, 0, image3Data.Length)
                        msPic3.Write(image3Data, 0, image3Data.Length)
                        Pic3.Image = Image.FromStream(msPic3, True)
                        Pic3.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                    Dim image4Data As Byte() = IIf(item.Photo4 = Nothing, Nothing, item.Photo4.ToArray())
                    If image4Data.Length <> 0 Then
                        msPic4 = New MemoryStream(image4Data, 0, image4Data.Length)
                        msPic4.Write(image4Data, 0, image4Data.Length)
                        Pic4.Image = Image.FromStream(msPic4, True)
                        Pic4.SizeMode = PictureBoxSizeMode.StretchImage
                    End If
                Catch ex As Exception

                End Try


                txtPh11.Text = item.Ph1_Value1
                txtPh12.Text = item.Ph1_Value2
                txtPh13.Text = item.Ph1_Value3
                txtPh14.Text = item.Ph1_Value4
                txtPh15.Text = item.Ph1_Value5
                txtPh16.Text = item.Ph1_Value6

                txtPH21.Text = item.Ph2_Value1
                txtPH22.Text = item.Ph2_Value2
                txtPH23.Text = item.Ph2_Value3
                txtPH24.Text = item.Ph2_Value4
                txtPH25.Text = item.Ph2_Value5
                txtPH26.Text = item.Ph2_Value6

                txtPH31.Text = item.Ph3_Value1
                txtPH32.Text = item.Ph3_Value2
                txtPH33.Text = item.Ph3_Value3
                txtPH34.Text = item.Ph3_Value4
                txtPH35.Text = item.Ph3_Value5
                txtPH36.Text = item.Ph3_Value6

                txtph41.Text = item.Ph4_Value1
                txtph42.Text = item.Ph4_Value2
                txtph43.Text = item.Ph4_Value3
                txtph44.Text = item.Ph4_Value4
                txtph45.Text = item.Ph4_value5
                txtph46.Text = item.Ph4_Value6
            Next
        Else
            'Inqury Bio Data
            Dim BioDataDetails = linq_obj.SP_Get_Enq_BioDataMasterListByID(Address_ID).ToList()
            For Each item As SP_Get_Enq_BioDataMasterListByIDResult In BioDataDetails
                Try
                    Dim image1Data As Byte() = IIf(item.Photo1 = Nothing, Nothing, item.Photo1.ToArray())
                    If Not image1Data Is Nothing Then
                        msPic1 = New MemoryStream(image1Data, 0, image1Data.Length)
                        msPic1.Write(image1Data, 0, image1Data.Length)
                        Pic1.Image = Image.FromStream(msPic1, True)
                        Pic1.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                    Dim image2Data As Byte() = IIf(item.Photo2 = Nothing, Nothing, item.Photo2.ToArray())
                    If image2Data.Length <> 0 Then
                        msPic2 = New MemoryStream(image2Data, 0, image2Data.Length)
                        msPic2.Write(image2Data, 0, image2Data.Length)
                        Pic2.Image = Image.FromStream(msPic2, True)
                        Pic2.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                    Dim image3Data As Byte() = IIf(item.Photo3 = Nothing, Nothing, item.Photo3.ToArray())
                    If image3Data.Length <> 0 Then
                        msPic3 = New MemoryStream(image3Data, 0, image3Data.Length)
                        msPic3.Write(image3Data, 0, image3Data.Length)
                        Pic3.Image = Image.FromStream(msPic3, True)
                        Pic3.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                    Dim image4Data As Byte() = IIf(item.Photo4 = Nothing, Nothing, item.Photo4.ToArray())
                    If image4Data.Length <> 0 Then
                        msPic4 = New MemoryStream(image4Data, 0, image4Data.Length)
                        msPic4.Write(image4Data, 0, image4Data.Length)
                        Pic4.Image = Image.FromStream(msPic4, True)
                        Pic4.SizeMode = PictureBoxSizeMode.StretchImage
                    End If
                Catch ex As Exception
                End Try
                txtPh11.Text = item.Ph1_Value1
                txtPh12.Text = item.Ph1_Value2
                txtPh13.Text = item.Ph1_Value3
                txtPh14.Text = item.Ph1_Value4
                txtPh15.Text = item.Ph1_Value5
                txtPh16.Text = item.Ph1_Value6

                txtPH21.Text = item.Ph2_Value1
                txtPH22.Text = item.Ph2_Value2
                txtPH23.Text = item.Ph2_Value3
                txtPH24.Text = item.Ph2_Value4
                txtPH25.Text = item.Ph2_Value5
                txtPH26.Text = item.Ph2_Value6

                txtPH31.Text = item.Ph3_Value1
                txtPH32.Text = item.Ph3_Value2
                txtPH33.Text = item.Ph3_Value3
                txtPH34.Text = item.Ph3_Value4
                txtPH35.Text = item.Ph3_Value5
                txtPH36.Text = item.Ph3_Value6

                txtph41.Text = item.Ph4_Value1
                txtph42.Text = item.Ph4_Value2
                txtph43.Text = item.Ph4_Value3
                txtph44.Text = item.Ph4_Value4
                txtph45.Text = item.Ph4_value5
                txtph46.Text = item.Ph4_Value6
            Next

        End If

    End Sub

    Public Sub GvEnqFollowp_Bind()


        'Get FollowDetails
        Dim tblFollowup As New DataTable

        tblFollowup.Columns.Add("F_Date")
        tblFollowup.Columns.Add("Followup")
        tblFollowup.Columns.Add("N_F_FollowpDate")
        tblFollowup.Columns.Add("Status")
        tblFollowup.Columns.Add("ByWhom")
        tblFollowup.Columns.Add("EnqType")
        tblFollowup.Columns.Add("Remarks")
        Dim FolloupDetails = linq_obj.SP_Get_Enq_FollowDetailsListById(Address_ID).ToList()
        tblFollowup.Clear()
        For Each item As SP_Get_Enq_FollowDetailsListByIdResult In FolloupDetails
            Dim dr As DataRow
            dr = tblFollowup.NewRow()
            dr("F_Date") = item.F_Date
            dr("Followup") = item.Followup
            dr("N_F_FollowpDate") = item.N_F_FollowpDate
            dr("Status") = item.Status
            dr("ByWhom") = item.ByWhom
            dr("EnqType") = item.EnqType
            dr("Remarks") = item.Remarks

            tblFollowup.Rows.Add(dr)
        Next
        GvEnqFollowpList.DataSource = tblFollowup
    End Sub

    Public Sub UpdateAll()
        Try

            'Save All Data
            If Address_ID > 0 Then
                'save main Order Details.

                If txtRecMKT.Text <> "" And dtOrderDate.Text = "" Then
                    MessageBox.Show("Enter OrderDate ,Beacuase after ordredate ,Accept Rec MKT Date")
                    Exit Sub
                End If
                Dim resorder As Integer
                resorder = linq_obj.SP_Update_Tbl_OrderOneMaster(txtOrderNo.Text, dtEntryDate.Text, txtPONo.Text, txtOrderNo.Text, Convert.ToDateTime(If(dtOrderDate.Text = "", "01-01-1900 00:00:00", dtOrderDate.Text)), Convert.ToDateTime(If(dtDispatchDate.Text = "", "01-01-1900 00:00:00", dtDispatchDate.Text)), txtPartyName.Text,
                                                              txtBatchName.Text, Address_ID, txtOrderRec.Text, txtOrderFollowBy.Text, cmbOrderStatus.Text, Convert.ToDateTime(If(txtRecMKT.Text = "", "01-01-1900 00:00:00", txtRecMKT.Text)))
                If (resorder > 0) Then
                    linq_obj.SubmitChanges()
                End If

                'bind Address Detail
                Dim resAddress As Integer
                resAddress = linq_obj.SP_UpdateAddress(txtPartyName.Text, txtBillAdresss.Text, txtArea.Text, txtCity.Text, txtPincode.Text, txtTaluka.Text, txtDistrict.Text, txtState.Text, txtDeladress.Text, txtDelArea.Text, txtDelCity.Text, txtDelPincode.Text, txtDelTaluka.Text, txtDelDistrict.Text, txtDelState.Text, Address_ID)
                If resAddress >= 0 Then
                    linq_obj.SubmitChanges()
                End If

                'Update Contact Detail
                Dim resContact As Integer
                resContact = linq_obj.SP_UpdateAddressContactDetail(txtcontctName.Text, txtContactNo.Text, txtOrdermastEmail.Text, txtEmail1.Text, txtEmail2.Text, Address_ID)
                linq_obj.SubmitChanges()

                'bind Project Information
                Dim resProjectId As Integer
                resProjectId = linq_obj.SP_Insert_Tbl_ProjectInformationMaster(txtPlantName.Text, txtModel.Text, txtProject.Text, txtCapacity.Text, txtPowerAvail.Text, txtPlantShape.Text,
                                                                             txtLandArea.Text, txtDtype.Text, txttentiveSchem.Text, txtJarDis.Text, txtBmouldDis.Text, Convert.ToDateTime(If(dtTentativeDate.Text = "", "01-01-1900 00:00:00", dtTentativeDate.Text)), Address_ID)
                If (resProjectId > 0) Then
                    linq_obj.SubmitChanges()
                Else
                    Dim resProjectupdateId As Integer
                    resProjectupdateId = linq_obj.SP_Update_Tbl_ProjectInformationMaster(txtPlantName.Text, txtModel.Text, txtProject.Text, txtCapacity.Text, txtPowerAvail.Text, txtPlantShape.Text,
                                                                                 txtLandArea.Text, txtDtype.Text, txttentiveSchem.Text, txtJarDis.Text, txtBmouldDis.Text, Convert.ToDateTime(If(dtTentativeAttendDate.Text = "", "01-01-1900 00:00:00", dtTentativeAttendDate.Text)), Address_ID)
                    If (resProjectupdateId >= 0) Then
                        linq_obj.SubmitChanges()
                    End If
                End If

                Dim resFollowupMaster As Integer
                resFollowupMaster = linq_obj.SP_Insert_Tbl_OrderFollowupMaster(txtProjectDetail.Text, Address_ID)
                If (resFollowupMaster > 0) Then
                    linq_obj.SubmitChanges()
                Else
                    Dim resFollowupMasterupdate As Integer
                    resFollowupMasterupdate = linq_obj.SP_Update_Tbl_OrderFollowupMaster(txtProjectDetail.Text, Address_ID)
                    If (resFollowupMasterupdate >= 0) Then
                        linq_obj.SubmitChanges()
                    End If
                End If

                ''Save Service Folllowup detail  11/12/2017 Navin
                'Dim delservicedata = linq_obj.SP_Delete_Tbl_OrderServiceFollowUpDetail(Address_ID)
                'linq_obj.SubmitChanges()

                'For i As Integer = 0 To tblSFollowup.Rows.Count - 1
                '    linq_obj.SP_Insert_Tbl_OrderServiceFollowUpDetail(
                '                                                Convert.ToDateTime(tblSFollowup.Rows(i)("Date").ToString()),
                '                                                tblSFollowup.Rows(i)("ServiceType").ToString(),
                '                                                tblSFollowup.Rows(i)("ComplainNo").ToString(),
                '                                                Convert.ToDateTime(tblSFollowup.Rows(i)("AttendDate").ToString()),
                '                                                tblSFollowup.Rows(i)("AttendBy").ToString(),
                '                                                tblSFollowup.Rows(i)("Engineer").ToString(),
                '                                                tblSFollowup.Rows(i)("FollowUp").ToString(),
                '                                                Convert.ToDateTime(tblSFollowup.Rows(i)("TentativeDate").ToString()),
                '                                                tblSFollowup.Rows(i)("Status").ToString(),
                '                                                tblSFollowup.Rows(i)("Remarks").ToString(),
                '                                                Address_ID)
                '    linq_obj.SubmitChanges()
                'Next

                'Save Visitor Detail
                Dim str1, str2, str3 As String
                Dim ms1 As New MemoryStream
                Dim ms2 As New MemoryStream
                Dim ms3 As New MemoryStream
                Dim ms4 As New MemoryStream

                Dim Pic1Data As Byte()
                Pic1Data = Nothing

                If Not Pic1.Image Is Nothing Then
                    Pic1.Image.Save(ms1, Pic1.Image.RawFormat)
                    Pic1Data = ms1.GetBuffer()
                End If

                Dim Pic2Data As Byte()

                If Not Pic2.Image Is Nothing Then
                    Pic2.Image.Save(ms2, Pic2.Image.RawFormat)
                    Pic2Data = ms2.GetBuffer()
                End If

                Dim Pic3Data As Byte()
                If Not Pic3.Image Is Nothing Then
                    Pic3.Image.Save(ms3, Pic3.Image.RawFormat)
                    Pic3Data = ms3.GetBuffer()
                End If

                Dim Pic4Data As Byte()
                If Not Pic4.Image Is Nothing Then
                    Pic4.Image.Save(ms4, Pic4.Image.RawFormat)
                    Pic4Data = ms4.GetBuffer()
                End If

                'Update Visitor Detail

                Dim VisitorPhoto = linq_obj.SP_insert_Update_Tbl_OrderVisitorDetail_New(0,
                                                                  Address_ID,
                                                                  Pic1Data,
                                                                  txtPh11.Text,
                                                                  txtPh12.Text,
                                                                  txtPh13.Text,
                                                                  txtPh14.Text,
                                                                  txtPh15.Text,
                                                                  txtPh16.Text,
                                                                  Pic2Data,
                                                                  txtPH21.Text,
                                                                  txtPH22.Text,
                                                                  txtPH23.Text,
                                                                  txtPH24.Text,
                                                                  txtPH25.Text,
                                                                  txtPH26.Text,
                                                                  Pic3Data,
                                                                  txtPH31.Text,
                                                                  txtPH32.Text,
                                                                  txtPH33.Text,
                                                                  txtPH34.Text,
                                                                  txtPH35.Text,
                                                                  txtPH36.Text,
                                                                  Pic4Data,
                                                                  txtph41.Text,
                                                                  txtph42.Text,
                                                                  txtph43.Text,
                                                                  txtph44.Text,
                                                                  txtph45.Text,
                                                                  txtph46.Text)
                linq_obj.SubmitChanges()

                'Save Order Details
                '/ Letter Header 
                Dim resletter As Integer
                resletter = linq_obj.SP_Insert_Tbl_LetterMailComMaster_Two(Address_ID,
                                                            TxtLetterProDetail.Text)
                If (resletter > 0) Then
                    linq_obj.SubmitChanges()
                Else
                    linq_obj.SP_Update_Tbl_LetterMailComMaster_Two(Address_ID, TxtLetterProDetail.Text)
                    linq_obj.SubmitChanges()
                End If
                '/ Product Instalation 
                'linq_obj.SP_Delete_Tbl_ProductInstallationMaster_Two(Address_ID)
                'linq_obj.SubmitChanges()
                'For i As Integer = 0 To TblProInst.Rows.Count - 1
                '    linq_obj.SP_Insert_Tbl_ProductInstallationMaster_Two(Address_ID,
                '    Convert.ToDateTime(TblProInst.Rows(i)("PDate").ToString()),
                '    Convert.ToDateTime(TblProInst.Rows(i)("Dis_Date").ToString()),
                '    TblProInst.Rows(i)("Product_Name").ToString(),
                '    TblProInst.Rows(i)("Vendor_Name").ToString(),
                '    TblProInst.Rows(i)("Station").ToString(),
                '    TblProInst.Rows(i)("Send_CU_To").ToString(),
                '    TblProInst.Rows(i)("Rec_CU_From").ToString(),
                '    TblProInst.Rows(i)("CU_To_Venue").ToString(),
                '    Convert.ToDateTime(TblProInst.Rows(i)("Comp_Date_With_Inst").ToString()),
                '    TblProInst.Rows(i)("By_Whom").ToString(),
                '    TblProInst.Rows(i)("Remark").ToString())
                '    linq_obj.SubmitChanges()
                'Next
                '/ISI Header
                Dim resisiprocess As Integer
                resisiprocess = linq_obj.SP_Insert_Tbl_ISIProcessMaster_Two(Address_ID,
                                                            TxtScheme.Text,
                                                                Convert.ToDateTime(If(TxtRecDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtRecDate.Text)),
                                                                Convert.ToDateTime(If(TxtDocFDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtDocFDate.Text)),
                                                                Convert.ToDateTime(If(TxtDocRDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtDocRDate.Text)),
                                                                TxtSToP.Text, TxtFSubmit.Text,
                                                                Convert.ToDateTime(If(TxtFileRegDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtFileRegDate.Text)),
                                                                Convert.ToDateTime(If(TxtBISInspDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtBISInspDate.Text)),
                                                                Convert.ToDateTime(If(TxtLicenceDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtLicenceDate.Text)),
                                                                TxtVender.Text,
                                                                TxtRemark.Text,
                                                                Convert.ToDateTime(If(txtVendorDate.Text.Trim() = "", "01-01-1900 00:00:00", txtVendorDate.Text)),
                                                                Convert.ToDateTime(If(txtVendorRecDate.Text.Trim() = "", "01-01-1900 00:00:00", txtVendorRecDate.Text))
                                                            )
                If (resisiprocess > 0) Then
                    linq_obj.SubmitChanges()
                Else
                    linq_obj.SP_Update_Tbl_ISIProcessMaster_Two(Address_ID,
                                                                TxtScheme.Text,
                                                               Convert.ToDateTime(If(TxtRecDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtRecDate.Text)),
                                                              Convert.ToDateTime(If(TxtDocFDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtDocFDate.Text)),
                                                                 Convert.ToDateTime(If(TxtDocRDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtDocRDate.Text)),
                                                                 TxtSToP.Text, TxtFSubmit.Text,
                                                                Convert.ToDateTime(If(TxtFileRegDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtFileRegDate.Text)),
                                                                  Convert.ToDateTime(If(TxtBISInspDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtBISInspDate.Text)),
                                                                   Convert.ToDateTime(If(TxtLicenceDate.Text.Trim() = "", "01-01-1900 00:00:00", TxtLicenceDate.Text)),
                                                                TxtVender.Text,
                                                                TxtRemark.Text,
                                                                 Convert.ToDateTime(If(txtVendorDate.Text.Trim() = "", "01-01-1900 00:00:00", txtVendorDate.Text)),
                                                                  Convert.ToDateTime(If(txtVendorRecDate.Text.Trim() = "", "01-01-1900 00:00:00", txtVendorRecDate.Text)))
                    linq_obj.SubmitChanges()
                End If


                ''/ ISI Description 
                'linq_obj.SP_Delete_Tbl_ISIProcess_DetailMaster_Two(Address_ID)
                'linq_obj.SubmitChanges()

                'For i As Integer = 0 To TblISIDesc.Rows.Count - 1
                '    linq_obj.SP_Insert_Tbl_ISIProcess_DetailMaster_Two(Address_ID,
                '    Convert.ToDateTime(TblISIDesc.Rows(i)("F_Date").ToString()),
                '                                                   TblISIDesc.Rows(i)("Followup").ToString(),
                '                                                   Convert.ToDateTime(TblISIDesc.Rows(i)("N_F_FollowpDate").ToString()),
                '                                                   TblISIDesc.Rows(i)("Status").ToString(),
                '                                                   TblISIDesc.Rows(i)("ByWhom").ToString(),
                '                                                   TblISIDesc.Rows(i)("Pro_Type").ToString(),
                '                                                   TblISIDesc.Rows(i)("Remarks").ToString())
                '    linq_obj.SubmitChanges()
                'Next

                'Allotment
                Dim resAllotment As Integer
                If (cmbUser.SelectedValue > 0 And cmbTeam.SelectedValue > 0) Then



                    resAllotment = linq_obj.SP_Tbl_UserAllotmentDetail_Insert(Address_ID, Convert.ToInt32(cmbUser.SelectedValue), Convert.ToInt32(cmbTeam.SelectedValue))
                    If (resAllotment > 0) Then
                        ' MessageBox.Show("Successfully Alloted To  Team : " + cmbTeam.Text + " and User : " + cmbUser.Text)
                    Else
                        ' MessageBox.Show("Already Alloted...")
                    End If
                End If

                insertLog()


                MessageBox.Show("Update Sucessfully..")
                clearAll()
            Else
                MessageBox.Show("No Address Informations Found")
            End If

        Catch ex As Exception
            MessageBox.Show("Error :" + ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clearAll()

    End Sub

    Public Sub clearAll()
        txtDtype.Text = "None"

        Packing_Clear()
        Clear_Followp_text()
        Document_Clear()
        LatterLog_Clean()
        CardMail_log_Clear()

        btnAddOrder_Click(Nothing, Nothing)
        cmbOrderStatus.SelectedIndex = -1
        Address_ID = 0
        tblFollowup.Clear()
        tblProjectDetail.Clear()
        tblSFollowup.Clear()
        TblRawMaterial.Clear()
        tblLog.Clear()
        txtFolowup3.Text = ""
        txtDaysAfter.Text = ""
        txtFolloupDetail.Text = ""
        txtProjectDetail.Text = ""
        'clear visitordata
        Pic1.ImageLocation = Nothing

        txtOrderFollowBy.Text = ""
        txtOrderRec.Text = ""
        dtTentativeDate.Text = ""

        'clear a Project information
        txtPlantName.Text = ""
        txtModel.Text = ""
        txtProject.Text = ""
        txtCapacity.Text = ""
        txtPowerAvail.Text = ""
        txtPlantShape.Text = ""
        txtLandArea.Text = ""
        txtDtype.Text = ""
        txttentiveSchem.Text = ""
        txtJarDis.Text = ""
        txtBmouldDis.Text = ""
        dtTentativeAttendDate.Text = ""

        'clear order detail 
        dtEntryDate.Text = DateTime.Now
        txtPONo.Text = ""
        txtOrderNo.Text = ""
        dtOrderDate.Text = DateTime.Now

        txtPartyName.Text = ""
        dtDispatchDate.Text = ""
        txtBatchName.Text = ""

        'clear 
        txtPartyName.Text = ""
        txtBillAdresss.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtDistrict.Text = ""
        txtTaluka.Text = ""
        txtPincode.Text = ""
        txtArea.Text = ""
        txtcontctName.Text = ""


        dtEntryDate.Value = DateTime.Now
        txtRecMKT.Text = ""
        txtDeladress.Text = ""
        txtDelArea.Text = ""
        txtDelCity.Text = ""
        txtDelDistrict.Text = ""
        txtDelPincode.Text = ""
        txtDelState.Text = ""
        txtDelTaluka.Text = ""

        dtOrderDate.Text = ""
        ''Clear All Data Of order[2] data

        TxtScheme.Text = ""
        TxtRecDate.Text = ""
        TxtDocFDate.Text = ""
        TxtDocRDate.Text = ""
        TxtSToP.Text = ""
        TxtFSubmit.Text = ""
        TxtFileRegDate.Text = ""
        TxtBISInspDate.Text = ""
        TxtLicenceDate.Text = ""
        TxtVender.Text = ""
        TxtRemark.Text = ""
        txtVendorDate.Text = ""
        txtVendorRecDate.Text = ""

        txtISIFollowRemarks.Text = ""
        txtISIStatus.Text = ""
        txtISIFollowUp.Text = ""
        txtISIByWhom.Text = ""
        txtISIProType.Text = ""
        txtISINextDays.Text = ""
        dtISIFDate.Value = DateTime.Now
        dtISINFDate.Value = DateTime.Now

        TxtProDate.Text = Date.Now
        TxtProDisDate.Text = Date.Now
        TxtProProductName.Text = ""
        TxtProVenderName.Text = ""
        TxtProStation.Text = ""
        TxtProSendCuTo.Text = ""
        TxtProRecCUFrom.Text = ""
        TxtProCuToVenue.Text = ""
        TxtProCompDateInst.Text = Date.Now
        TxtProByWhom.Text = ""
        TxtProRemark.Text = ""

        TxtLetterProDetail.Text = ""
        TxtLetterDate.Text = Date.Now
        TxtLetterCreDate.Text = Date.Now
        TxtLetterCardRem.Text = ""
        TxtLetterMailRec.Text = ""
        TxtLetterMailSend.Text = ""
        TxtLetterByWhom.Text = ""
        TxtLetterMailRem.Text = ""

        TblISIDesc.Clear()
        TblProInst.Clear()
        TblLetter.Clear()
        btnSave.Enabled = True

        txtEmail1.Text = ""
        txtEmail2.Text = ""

        txtItemName.Text = ""
        txtQty.Text = ""
        txtAmount.Text = ""
        txtPrice.Text = ""

        txtContactNo.Text = ""
        txtOrdermastEmail.Text = ""


        'cleanAll biodata detail
        txtPh11.Text = ""
        txtPh12.Text = ""
        txtPh13.Text = ""
        txtPh14.Text = ""
        txtPh15.Text = ""
        txtPh16.Text = ""

        txtPH21.Text = ""
        txtPH22.Text = ""
        txtPH23.Text = ""
        txtPH24.Text = ""
        txtPH25.Text = ""
        txtPH26.Text = ""

        txtPH31.Text = ""
        txtPH32.Text = ""
        txtPH33.Text = ""
        txtPH34.Text = ""
        txtPH35.Text = ""
        txtPH36.Text = ""

        txtph41.Text = ""
        txtph42.Text = ""
        txtph43.Text = ""
        txtph44.Text = ""
        txtph45.Text = ""
        txtph46.Text = ""

        Pic1.Image = Nothing
        Pic2.Image = Nothing
        Pic3.Image = Nothing
        Pic4.Image = Nothing

        cmbUser.SelectedIndex = 0
        GvAllotedData.DataSource = Nothing
        DGVfollowUp.DataSource = Nothing
        DGVOrderdtail.DataSource = Nothing
        GvPackingDetailNewList.DataSource = Nothing
        DataLetter.DataSource = Nothing
        dgLog.DataSource = Nothing
        GvEnqFollowpList.DataSource = Nothing


        rwIDDelOrderDetail = -1
        rwIDDelFollowDetail = -1
        rwIDISIProcessDetail = -1
        rwIDDelLetterMailDetail = -1
        rwIDDelRawMaterialDetail = -1
        rwIDLetterLogDetail = -1

        'Pipeline 
        Gv_Project_Log.DataSource = Nothing
        ddlPipe_ProjectType.DataSource = Nothing

        ddlMileStone.DataSource = Nothing
        ChkEngineerList.Items.Clear()
        Clear_Pipeline()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        clearAll()
    End Sub

    'Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
    '    Dim result As DialogResult = MessageBox.Show("Are You Sure?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
    '    If result = DialogResult.Yes Then

    '        Dim resDelete As Integer
    '        Dim resProjDelete As Integer
    '        Dim resPack As Integer
    '        Dim resFollowUp As Integer
    '        Dim resFollowupDetail As Integer
    '        Dim resSerFollow As Integer
    '        Dim resVisDetail As Integer
    '        Dim resPlant As Integer

    '        resProjDelete = linq_obj.SP_Delete_Tbl_ProjectDetail(Address_ID)
    '        resPack = linq_obj.SP_Delete_Tbl_PackagingMaster(Address_ID)
    '        resDelete = linq_obj.SP_Delete_Tbl_OrderOneMaster(Address_ID)
    '        resFollowUp = linq_obj.SP_Delete_Tbl_OrderFollowupMaster(Address_ID)

    '        resSerFollow = linq_obj.SP_Delete_Tbl_OrderServiceFollowUpDetail(Address_ID)
    '        resVisDetail = linq_obj.SP_Delete_Tbl_OrderVisitorDetail(Address_ID)
    '        resPlant = linq_obj.SP_Tbl_OrderPlanDrawing_Delete(Address_ID)
    '        '/Letter Header
    '        linq_obj.SP_Delete_Tbl_LetterMailComMaster_Two(Address_ID)
    '        linq_obj.SubmitChanges()

    '        '/Letter Detail
    '        linq_obj.SP_Delete_Tbl_LetterMailComMaster_Detail_Two(Address_ID)
    '        linq_obj.SubmitChanges()

    '        '/Product Inst
    '        linq_obj.SP_Delete_Tbl_ProductInstallationMaster_Two(Address_ID)
    '        linq_obj.SubmitChanges()

    '        '/ISI Header
    '        linq_obj.SP_Delete_Tbl_ISIProcessMaster_Two(Address_ID)
    '        linq_obj.SubmitChanges()

    '        '/ISI Detail
    '        linq_obj.SP_Delete_Tbl_ISIProcess_DetailMaster_Two(Address_ID)
    '        linq_obj.SubmitChanges()


    '        'Delete Project Information
    '        linq_obj.SP_Delete_Tbl_ProjectInformationMaster(Address_ID)
    '        linq_obj.SubmitChanges()

    '        'Delete RawMatirial

    '        linq_obj.SP_Tbl_OrderRawMaterialDetail_Delete(Address_ID)
    '        linq_obj.SubmitChanges()

    '        'Delete Letter Log
    '        linq_obj.SP_Tbl_ProjectLetterLog_Delete(Address_ID)
    '        linq_obj.SubmitChanges()

    '        clearAll()
    '        MessageBox.Show("Successfully Deleted")
    '        'Refresh 
    '        btnRefresh_Click(Nothing, Nothing)


    '    End If


    'End Sub

    Private Sub txtFolowup3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolowup3.TextChanged
        If txtFolowup3.Text <> "" Then
            dtTentativeAttendDate.Value = dtSFDate.Value.Date.AddDays(txtFolowup3.Text)
        End If
    End Sub

    Private Sub txtDaysAfter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDaysAfter.TextChanged
        If txtDaysAfter.Text <> "" Then
            dtNFDate.Value = dtFollowDate.Value.Date.AddDays(txtDaysAfter.Text)
        End If
    End Sub

    Private Sub txtEntryNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtOrderNo.Text).ToList()
        If (data.Count > 0) Then
            clearAll()
            Address_ID = data(0).Pk_AddressID
            BindAllData()
        End If
    End Sub

    Private Sub txtPartyName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartyName.Leave
        If (txtPartyName.Text <> "") Then
            Dim data = linq_obj.SP_Get_AddressListByNameForOrder(txtPartyName.Text).ToList()
            If (data.Count > 0) Then
                Address_ID = data(0).Pk_AddressID
                BindAllGridData()
            End If


        End If
    End Sub

    Private Sub btnNewReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MessageBox.Show("Format Required")
    End Sub

    Private Sub btnfolowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfolowup.Click
        If Address_ID > 0 Then

            Dim rpt As New ReportDocument
            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Rpt_FollowUp"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Rpt_FollowUp")
            Class1.WriteXMlFile(ds, "SP_Rpt_FollowUp", "SP_Rpt_FollowUp")
            rpt.Load(Application.StartupPath & "\Reports\Rpt_FollowUp.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Rpt_FollowUp"))


            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()
        End If
    End Sub

    Private Sub btnVisitor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisitor.Click
        MessageBox.Show("Format Required Pending")
    End Sub
    '/ ISI Desc Add More
    Private Sub BtnDescSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDescSave.Click
        ''add new row runtime and display into grid. it will save after click on save button

        If (txtISIFollowUp.Text.Trim() = "") Then
            MessageBox.Show("Follow up Cannot Be Blank...")
            Return
        ElseIf (txtISIStatus.Text.Trim() = "") Then
            MessageBox.Show("Status Cannot Be Blank...")
            Return
        ElseIf (txtISIByWhom.Text.Trim() = "") Then
            MessageBox.Show("ByWhom Cannot Be Blank...")
            Return
        ElseIf (txtISIProType.Text.Trim() = "") Then
            MessageBox.Show("Project Type Cannot Be Blank...")
            Return
        Else

            Try
                If (Address_ID > 0) Then
                    Dim dr As DataRow
                    dr = TblISIDesc.NewRow()
                    dr("F_Date") = Class1.Datecheck(dtISIFDate.Text)
                    dr("Followup") = txtISIFollowUp.Text
                    dr("N_F_FollowpDate") = Class1.Datecheck(dtISINFDate.Text)
                    dr("Status") = txtISIStatus.Text
                    dr("ByWhom") = txtISIByWhom.Text
                    dr("Pro_Type") = txtISIProType.Text
                    dr("Remarks") = txtISIFollowRemarks.Text
                    If (rwIDISIProcessDetail >= 0) Then
                        'DataISIGrid.Rows.RemoveAt(rwIDISIProcessDetail)
                        DataISIGrid.Rows(rwIDISIProcessDetail).Cells(0).Value = dr("F_Date")
                        DataISIGrid.Rows(rwIDISIProcessDetail).Cells(1).Value = dr("Followup")
                        DataISIGrid.Rows(rwIDISIProcessDetail).Cells(2).Value = dr("N_F_FollowpDate")
                        DataISIGrid.Rows(rwIDISIProcessDetail).Cells(3).Value = dr("Status")
                        DataISIGrid.Rows(rwIDISIProcessDetail).Cells(4).Value = dr("ByWhom")
                        DataISIGrid.Rows(rwIDISIProcessDetail).Cells(5).Value = dr("Pro_Type")
                        DataISIGrid.Rows(rwIDISIProcessDetail).Cells(6).Value = dr("Remarks")
                        TblISIDesc = DataISIGrid.DataSource
                        'TblISIDesc.Rows.Add(dr)
                    Else
                        TblISIDesc.Rows.Add(dr)
                    End If
                    DataISIGrid.DataSource = TblISIDesc

                    linq_obj.SP_Insert_Tbl_ISIProcess_DetailMaster_Two(Address_ID,
                                                      dtISIFDate.Text,
                                                     txtISIFollowUp.Text,
                                                     dtISINFDate.Text,
                                                     txtISIStatus.Text,
                                                     txtISIByWhom.Text,
                                                     txtISIProType.Text,
                                                     txtISIFollowRemarks.Text)

                    linq_obj.SubmitChanges()


                Else
                    MessageBox.Show("No Party Data Found")
                End If
                ISI_Process_Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
    Public Sub ISI_Process_Clear()

        txtISIFollowRemarks.Text = ""
        txtISIStatus.Text = ""
        txtISIFollowUp.Text = ""
        txtISIByWhom.Text = ""
        txtISIProType.Text = ""
        txtISINextDays.Text = ""
        dtISIFDate.Value = DateTime.Now
        dtISINFDate.Value = DateTime.Now
        rwIDISIProcessDetail = -1
        BtnDescAdd.Focus()
    End Sub
    '/ Product Install Add More
    Private Sub BtnSaveProInst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveProInst.Click
        Dim dr As DataRow
        dr = TblProInst.NewRow()
        dr("PDate") = TxtProDate.Text
        dr("Dis_Date") = TxtProDisDate.Text
        dr("Product_Name") = TxtProProductName.Text
        dr("Vendor_Name") = TxtProVenderName.Text
        dr("Station") = TxtProStation.Text
        dr("Send_CU_To") = TxtProSendCuTo.Text
        dr("Rec_CU_From") = TxtProRecCUFrom.Text
        dr("CU_To_Venue") = TxtProCuToVenue.Text
        dr("Comp_Date_With_Inst") = TxtProCompDateInst.Text
        dr("By_Whom") = TxtProByWhom.Text
        dr("Remark") = TxtProRemark.Text

        TblProInst.Rows.Add(dr)
        DataProInst.DataSource = TblProInst

        linq_obj.SP_Insert_Tbl_ProductInstallationMaster_Two(Address_ID,
                    TxtProDate.Text,
                    TxtProDisDate.Text,
                     TxtProProductName.Text,
                     TxtProVenderName.Text,
                    TxtProStation.Text,
                    TxtProSendCuTo.Text,
                    TxtProRecCUFrom.Text,
                    TxtProCuToVenue.Text,
                    TxtProCompDateInst.Text,
                    TxtProByWhom.Text,
                    TxtProRemark.Text)
        linq_obj.SubmitChanges()

        TxtProDate.Text = Date.Now
        TxtProDisDate.Text = Date.Now
        TxtProProductName.Text = ""
        TxtProVenderName.Text = ""
        TxtProStation.Text = ""
        TxtProSendCuTo.Text = ""
        TxtProRecCUFrom.Text = ""
        TxtProCuToVenue.Text = ""
        TxtProCompDateInst.Text = Date.Now
        TxtProByWhom.Text = ""
        TxtProRemark.Text = ""
    End Sub
    '/ Letter Desc Add More
    Private Sub BtnLetterSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLetterSave.Click

        Try

            If rblisOther.Checked = True Then
                If (TxtLetterMailRec.Text.Trim() = "" And TxtLetterMailSend.Text.Trim() = "") Then
                    MessageBox.Show("MAIL(REC) or MAIL(SEND) Both Date Cannot Be Blank..")
                    Return
                ElseIf (TxtLetterByWhom.Text.Trim() = "") Then
                    MessageBox.Show("BY WHOM Cannot Be Blank..")
                    Return
                End If
            End If


            If rblisCard.Checked = True Then
                If (TxtLetterCardRem.Text.Trim() = "") Then
                    MessageBox.Show("Latter Card Remarks Cannot Be Blank..")
                    Return
                End If

            End If

            If (Address_ID > 0) Then


                If BtnLetterSave.Text = "Save" Then

                    linq_obj.SP_Insert_Tbl_LetterMailComMaster_Detail_Two(Address_ID,
                        TxtLetterDate.Value.Date,
                        TxtLetterCreDate.Value.Date,
                        TxtLetterCardRem.Text,
                         TxtLetterMailRec.Text,
                       TxtLetterMailSend.Text,
                         TxtLetterByWhom.Text,
                       TxtLetterMailRem.Text)
                    linq_obj.SubmitChanges()
                    MessageBox.Show("Save Sucessfully...")
                Else

                    linq_obj.SP_Update_Tbl_LetterMailComMaster_Detail_Two_new(PK_LetterDetailID, Address_ID,
                       TxtLetterDate.Value.Date,
                       TxtLetterCreDate.Value.Date,
                       TxtLetterCardRem.Text,
                        TxtLetterMailRec.Text,
                      TxtLetterMailSend.Text,
                        TxtLetterByWhom.Text,
                      TxtLetterMailRem.Text)
                    linq_obj.SubmitChanges()
                    MessageBox.Show("Update Sucessfully...")
                End If


                CardMail_log_Clear()
                Latter_Email_Bind()
            Else
                MessageBox.Show("EnqNo Not Found...")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
    Public Sub CardMail_log_Clear()

        BtnLetterSave.Text = "Save"

        TxtLetterCardRem.Text = ""
        TxtLetterMailRec.Text = ""
        TxtLetterMailSend.Text = ""
        TxtLetterByWhom.Text = ""
        TxtLetterMailRem.Text = ""
        PK_LetterDetailID = 0
        rwIDDelLetterMailDetail = -1
        BtnAddLetter.Focus()
    End Sub
    '/ DataProInst Row Delete
    'Private Sub BtnProDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnProDelete.Click
    '    If DataProInst.Rows.Count > 0 Then
    '        TblProInst.Rows(DataProInst.CurrentRow.Index).Delete()
    '        DataProInst.DataSource = TblProInst
    '    End If
    'End Sub

    '/ DataISIGrid Row Delete
    'Private Sub BtnDescDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDescDelete.Click
    '    If DataISIGrid.Rows.Count > 0 Then
    '        Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
    '        If result = DialogResult.Yes Then
    '            'TblISIDesc.Rows(DataISIGrid.CurrentRow.Index).Delete()
    '            DataISIGrid.Rows.RemoveAt(DataISIGrid.CurrentRow.Index)
    '            TblISIDesc = DataISIGrid.DataSource
    '            'DataISIGrid.DataSource = TblISIDesc
    '        End If
    '    End If
    'End Sub

    Private Sub BtnAddLetter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddLetter.Click
        TxtLetterDate.Text = Date.Now
        TxtLetterCreDate.Text = Date.Now
        TxtLetterCardRem.Text = ""
        TxtLetterMailRec.Text = ""
        TxtLetterMailSend.Text = ""
        TxtLetterByWhom.Text = ""
        TxtLetterMailRem.Text = ""
        BtnLetterSave.Text = "Save"

        rwIDDelLetterMailDetail = -1
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TxtProDate.Text = Date.Now
        TxtProDisDate.Text = Date.Now
        TxtProProductName.Text = ""
        TxtProVenderName.Text = ""
        TxtProStation.Text = ""
        TxtProSendCuTo.Text = ""
        TxtProRecCUFrom.Text = ""
        TxtProCuToVenue.Text = ""
        TxtProCompDateInst.Text = Date.Now
        TxtProByWhom.Text = ""
        TxtProRemark.Text = ""
    End Sub

    Private Sub BtnDescAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDescAdd.Click
        txtISIFollowRemarks.Text = ""
        txtISIStatus.Text = ""
        txtISIFollowUp.Text = ""
        txtISIByWhom.Text = ""
        txtISIProType.Text = ""
        txtISINextDays.Text = ""
        dtISIFDate.Value = DateTime.Now
        dtISINFDate.Value = DateTime.Now
        rwIDISIProcessDetail = -1

    End Sub

    Private Sub txtDaysAfter_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDaysAfter.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If

    End Sub

    Private Sub txtFolowup3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFolowup3.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If

    End Sub

    Private Sub btnLFolowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLFolowup.Click
        If Address_ID > 0 Then

            Dim rpt As New ReportDocument
            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Rpt_FollowUp_LatterMail"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Rpt_FollowUp_LatterMail")

            Class1.WriteXMlFile(ds, "SP_Rpt_FollowUp_LatterMail", "SP_Rpt_FollowUp_LatterMail")

            rpt.Load(Application.StartupPath & "\Reports\Rpt_FollowUp_LetterMail.rpt")

            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Rpt_FollowUp_LatterMail"))

            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()


        End If
    End Sub

    Private Sub btnIfolowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIfolowup.Click
        If Address_ID > 0 Then

            Dim rpt As New ReportDocument
            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Rpt_FollowUp_ProjectISIProcess"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Rpt_FollowUp_ProjectISIProcess")
            Class1.WriteXMlFile(ds, "SP_Rpt_FollowUp_ProjectISIProcess", "SP_Rpt_FollowUp_ProjectISIProcess")
            rpt.Load(Application.StartupPath & "\Reports\Rpt_FollowUp_ISIProcess.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Rpt_FollowUp_ProjectISIProcess"))
            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()
        End If
    End Sub

    Private Sub btnPfolowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPfolowup.Click
        If Address_ID > 0 Then
            Dim rpt As New ReportDocument
            Dim ds As New DataSet
            Dim ds1 As New DataSet
            Dim cmd As New SqlCommand
            Dim cmd1 As New SqlCommand

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Rpt_FollowUp_ProjectSummary"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "Rpt_FollowUp_ProjectSummary")
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.CommandText = "SP_Rpt_FollowUp_ProjectSummary_PackingDetail"
            cmd1.Connection = linq_obj.Connection
            cmd1.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID
            Dim da1 As New SqlDataAdapter()
            da1.SelectCommand = cmd1
            da1.Fill(ds1, "SP_Rpt_FollowUp_ProjectSummary_PackingDetail")
            Class1.WriteXMlFile(ds, "Rpt_FollowUp_ProjectSummary", "Rpt_FollowUp_ProjectSummary")
            rpt.Load(Application.StartupPath & "\Reports\Rpt_FollowUp_ProjectSummary.rpt")
            rpt.SetDataSource(ds.Tables("Rpt_FollowUp_ProjectSummary"))
            rpt.Subreports(0).SetDataSource(ds1.Tables("SP_Rpt_FollowUp_ProjectSummary_PackingDetail"))
            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        End If
    End Sub

    Private Sub DGVOrderDetails_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles DGVOrderDetails.PreviewKeyDown
        Try
            Dim irow As Integer

            clearAll()
            irow = DGVOrderDetails.CurrentRow.Index
            If (e.KeyCode = Keys.Down) Then
                If DGVOrderDetails.Rows.Count - 1 > DGVOrderDetails.CurrentRow.Index Then
                    Address_ID = Convert.ToInt64(Me.DGVOrderDetails.Rows(Me.DGVOrderDetails.CurrentRow.Index + 1).Cells(0).Value)
                End If
            Else
                If DGVOrderDetails.CurrentRow.Index > 0 Then
                    Address_ID = Convert.ToInt64(Me.DGVOrderDetails.Rows(Me.DGVOrderDetails.CurrentRow.Index - 1).Cells(0).Value)
                End If
            End If
            BindAllGridData()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnClearItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearItem.Click
        txtItemName.Text = ""
        txtQty.Text = ""
        txtAmount.Text = ""
        txtPrice.Text = ""
        rwIDDelRawMaterialDetail = -1


    End Sub

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Try
            If dtRowOrderDate.Text.Trim() = "" Then
                dtRowOrderDate.Text = "01/01/1990"

            End If
            If dtRowDisdate.Text.Trim() = "" Then
                dtRowDisdate.Text = "01/01/1990"

            End If
            If txtRowTentDispDate.Text.Trim() = "" Then
                txtRowTentDispDate.Text = "01/01/1990"

            End If
            Dim OrderConfirm As Boolean
            Dim PaymentReceived As Boolean

            OrderConfirm = False
            PaymentReceived = False

            If (rbYes.Checked = True) Then
                OrderConfirm = True
            End If
            If (rbPYes.Checked = True) Then
                PaymentReceived = True

            End If


            If (txtItemName.Text.Trim() = "") Then
                MessageBox.Show("ItemName Cannot Be Blank..")
                txtItemName.Focus()
                Return
            ElseIf (txtQty.Text.Trim() = "") Then
                MessageBox.Show("Qty Cannot Be Blank..")
                txtQty.Focus()
                Return
            ElseIf (txtPrice.Text.Trim() = "") Then
                MessageBox.Show("Price Cannot Be Blank..")
                txtPrice.Focus()
                Return
            ElseIf (txtAmount.Text.Trim() = "") Then
                MessageBox.Show("Amount Cannot Be Blank..")
                txtAmount.Focus()
                Return
            ElseIf (dtRowOrderDate.Text.Trim() = "") Then
                MessageBox.Show("Orderdate Cannot Be Blank..")
                dtRowOrderDate.Focus()
                Return
            Else
                If (Address_ID > 0) Then

                    If btnAddItem.Text.Trim() = "Save" Then
                        linq_obj.SP_Tbl_OrderRawMaterialDetail_Insert(Address_ID,
                                                     txtItemName.Text,
                                                       Convert.ToInt32(txtQty.Text),
                                                      Convert.ToDecimal(txtPrice.Text),
                                                      Convert.ToDecimal(txtAmount.Text),
                                                       OrderConfirm,
                                                       PaymentReceived,
                                                      dtRowOrderDate.Text,
                                                      dtRowDisdate.Text,
                                                     txtRowTentDispDate.Text) '
                        linq_obj.SubmitChanges()
                        MessageBox.Show("Add Sucessfully...")
                    Else

                        linq_obj.SP_Tbl_OrderRawMaterialDetail_Update_New(Pk_OrderRawMaterialId, Address_ID,
                                                      txtItemName.Text,
                                                      Convert.ToInt32(txtQty.Text),
                                                      Convert.ToDecimal(txtPrice.Text),
                                                      Convert.ToDecimal(txtAmount.Text),
                                                      OrderConfirm,
                                                      PaymentReceived,
                                                      dtRowOrderDate.Text,
                                                      dtRowDisdate.Text,
                                                      txtRowTentDispDate.Text) '
                        linq_obj.SubmitChanges()
                        MessageBox.Show("Update Sucessfully...")

                    End If




                    btnAddItem.Text = "Save"
                    DGRawMaterialData_Bind()
                    txtItemName.Text = ""
                    txtQty.Text = ""
                    txtAmount.Text = ""
                    txtPrice.Text = ""
                    dtRowOrderDate.Text = ""
                    dtRowDisdate.Text = ""
                    txtRowTentDispDate.Text = ""
                    rwIDDelRawMaterialDetail = -1
                    btnClearItem.Focus()
                Else
                    MessageBox.Show("Enq No Not Found")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub



    Private Sub btnDelItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelItem.Click
        If DGRawMaterialData.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                linq_obj.SP_Tbl_OrderRawMaterialDetail_Delete_ID(Convert.ToInt64(DGRawMaterialData.SelectedCells(0).Value))
                linq_obj.SubmitChanges()

                MessageBox.Show("Delete Sucessfully..")
                DGRawMaterialData_Bind()

            End If
        End If
    End Sub


    Private Sub txtPrice_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice.KeyPress, txtQty.KeyPress
        e.Handled = Class1.OnlyNumeric(sender, e)
    End Sub

    Private Sub txtPrice_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice.Leave
        Try
            If (txtQty.Text.Trim() <> "" AndAlso txtPrice.Text.Trim() <> "") Then
                txtAmount.Text = Convert.ToString(Convert.ToInt32(txtQty.Text) * Convert.ToDecimal(txtPrice.Text))
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtISINextDays_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtISINextDays.TextChanged
        If txtISINextDays.Text <> "" Then
            dtISINFDate.Value = dtISIFDate.Value.Date.AddDays(txtISINextDays.Text)
        End If
    End Sub

    Private Sub txtISINextDays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtISINextDays.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLog.Click
        txtLetterTitle.Text = ""
        btnLogSave.Text = "Save"

        txtSendCourierBy.Text = ""
        txtSendCourierDate.Text = ""
        txtSendMailBy.Text = ""
        txtSendMailDate.Text = ""

        txtRecCourierBy.Text = ""
        txtRecCourierDate.Text = ""
        txtRecMailBy.Text = ""
        txtRecMailDate.Text = ""
        rwIDLetterLogDetail = -1
    End Sub


    Public Sub LatterLog_Validation()

        Try


        Catch ex As Exception


        End Try
    End Sub

    Private Sub btnLogSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogSave.Click
        Try
            If (txtLetterTitle.Text.Trim() = "") Then
                MessageBox.Show("Letter Title Cannot Be Blank")
                Return
            ElseIf (txtSendMailDate.Text.Trim() = "") Then
                MessageBox.Show("Send Mail Date Cannot Be Blank")
                Return
            ElseIf (txtSendMailBy.Text.Trim() = "") Then
                MessageBox.Show("Send Mail  Can Not Be Blank")
                Return
            ElseIf (txtSendCourierDate.Text.Trim() = "") Then
                MessageBox.Show(" Send Courier Date Cannot Be Blank")
                Return
            ElseIf (txtSendCourierBy.Text.Trim() = "") Then
                MessageBox.Show(" Send Courier By Cannot Be Blank")
                Return
            ElseIf (txtRecMailDate.Text.Trim() = "") Then
                MessageBox.Show("Receive Mail Date Can Not Be Blank")
                Return
            ElseIf (txtRecMailBy.Text.Trim() = "") Then
                MessageBox.Show("Receive Mail By Can Not Be Blank")
                Return
            ElseIf (txtRecCourierDate.Text.Trim() = "") Then
                MessageBox.Show(" Receive Courier Date Can Not Be Blank")
                Return
            ElseIf (txtRecCourierBy.Text.Trim() = "") Then
                MessageBox.Show(" Receive Courier By Can Not Be Blank")
                Return
            Else
                If (Address_ID > 0) Then

                    If btnLogSave.Text.Trim() = "Save" Then
                        linq_obj.SP_Tbl_ProjectLetterLog_Insert(txtLetterTitle.Text, txtSendMailBy.Text, Class1.Datecheck(txtSendMailDate.Text), txtSendCourierBy.Text, Class1.Datecheck(txtSendCourierDate.Text), txtRecMailBy.Text, Class1.Datecheck(txtRecMailDate.Text), txtRecCourierBy.Text, Class1.Datecheck(txtRecCourierDate.Text), Address_ID)
                        linq_obj.SubmitChanges()

                        MessageBox.Show("Save Sucessfully...")
                    Else


                        linq_obj.SP_Tbl_ProjectLetterLog_Update_New(Pk_ProjectLetterId, txtLetterTitle.Text, txtSendMailBy.Text, Class1.Datecheck(txtSendMailDate.Text), txtSendCourierBy.Text, Class1.Datecheck(txtSendCourierDate.Text), txtRecMailBy.Text, Class1.Datecheck(txtRecMailDate.Text), txtRecCourierBy.Text, Class1.Datecheck(txtRecCourierDate.Text), Address_ID)
                        linq_obj.SubmitChanges()

                        MessageBox.Show("Update Sucessfully...")

                    End If

                    LatterLog_Clean()
                    Latter_Log_Bind()

                Else
                    MessageBox.Show("EnqNo Not Found...")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub LatterLog_Clean()
        txtLetterTitle.Text = ""
        txtSendCourierBy.Text = ""
        txtSendCourierDate.Text = ""
        txtSendMailBy.Text = ""
        txtSendMailDate.Text = ""
        btnLogSave.Text = "Save"

        txtRecCourierBy.Text = ""
        txtRecCourierDate.Text = ""
        txtRecMailBy.Text = ""
        txtRecMailDate.Text = ""
        rwIDLetterLogDetail = -1
        btnAddLog.Focus()
    End Sub


    'Private Sub btnDelLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelLog.Click
    '    If dgLog.Rows.Count > 0 Then
    '        Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
    '        If result = DialogResult.Yes Then
    '            dgLog.Rows.RemoveAt(dgLog.CurrentRow.Index)
    '            tblLog = dgLog.DataSource
    '            ''insertLog()
    '        End If
    '    End If
    'End Sub

    Public Sub insertLog()


    End Sub


    Private Sub btnViewFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewFollowup.Click
        Try
            clearAll()
            Dim cmd As New SqlCommand
            If (RBFollowUp.Checked = True) Then
                cmd.CommandText = "SP_Get_OrderFollowDetails"
            ElseIf (rbDisp.Checked = True) Then
                cmd.CommandText = "SP_Get_OrderDispatchDetails"
            ElseIf (rbReceive.Checked = True) Then
                cmd.CommandText = "SP_Get_OrderReceiveDetails"
            ElseIf (rbTentDis.Checked = True) Then
                cmd.CommandText = "SP_Get_OrderTentativeDispatchDetails"
            Else
                MessageBox.Show("select function")
                Exit Sub
            End If

            cmd.Parameters.AddWithValue("@Start", Class1.getDate(dtStart.Text))
            cmd.Parameters.AddWithValue("@End", Class1.getDate(dtEnd.Text))
            If (Class1.global.InquiryView = "1") Then
                cmd.Parameters.AddWithValue("@user", 0)
            Else
                cmd.Parameters.AddWithValue("@user", Class1.global.UserID)
            End If

            Dim objclass As New Class1

            Dim ds As New DataSet
            ds = objclass.GetSearchData(cmd)
            If ds.Tables(1).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DGVOrderDetails.DataSource = Nothing
            Else
                DGVOrderDetails.DataSource = ds.Tables(1)
                DGVOrderDetails.Columns(0).Visible = False

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        chkAllStatus.Checked = False
    End Sub

    Private Sub txtOrderNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrderNo.Leave
        btnSave.Enabled = True

        Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtOrderNo.Text).ToList()
        If (data.Count > 0) Then
            Address_ID = data(0).Pk_AddressID
            BindAllGridData()
        End If
    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GvInEnq_Bind()
        chkAllStatus.Checked = False
    End Sub

    Private Sub DGVOrderDetails_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVOrderDetails.CellClick
        Try
            clearAll()
            LatterLog_Clean()

            Address_ID = Convert.ToInt64(Me.DGVOrderDetails.SelectedCells(0).Value)
            BindAllGridData()

            GvPackingDetailNewList_Bind()
            GvFileList_Bind()
            EnqClient_Bio_Data()

            GvEnqFollowp_Bind()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub btnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        chkAllStatus.Checked = True
        GvInEnq_Bind()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GvInEnq_Bind()
        chkAllStatus.Checked = False
    End Sub

    Private Sub chkAllStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllStatus.CheckedChanged
        If (chkAllStatus.Checked = True) Then
            btnAll_Click(Nothing, Nothing)
        Else
            btnRefresh_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub lnkClear_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkClear.LinkClicked
        txtParty.Text = ""
        txtcoPerson.Text = ""
        txtDisStatus.Text = ""
        txtStation.Text = ""
        txtSearchEnqNo.Text = ""
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        lblEnqNo.Text = ""
        lblLastDate.Text = ""
        lblLastFollowUpBy.Text = ""
        lblAllotedTo.Text = ""
        grpFollowDetail.Visible = False
    End Sub

    Private Sub btnSearchAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAll.Click
        clearAll()
        bindEnqGrid()
    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub DGVOrderdtail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVOrderdtail.DoubleClick
        Try
            btnsaveOrder.Text = "Update"

            If DGVOrderdtail.Rows.Count > 0 Then
                Pk_ProjctDetailId = DGVOrderdtail.SelectedCells(0).Value
                txtSrNo.Text = DGVOrderdtail.SelectedCells(2).Value
                txtPlantScheme.Text = DGVOrderdtail.SelectedCells(3).Value
                txtVendorName.Text = DGVOrderdtail.SelectedCells(4).Value
                dtDisDate.Text = Class1.Datecheck(DGVOrderdtail.SelectedCells(5).Value)

            End If
            'tblProjectDetail.Rows.RemoveAt(rwIDDelOrderDetail)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGVfollowUp_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVfollowUp.DoubleClick
        btnSaveFollowup.Text = "Update"

        Try

            Pk_OrderFollowupDetailId = DGVfollowUp.SelectedCells(0).Value
            dtFollowDate.Value = DGVfollowUp.SelectedCells(2).Value
            txtFolloupDetail.Text = DGVfollowUp.SelectedCells(3).Value
            dtNFDate.Value = DGVfollowUp.SelectedCells(4).Value
            txtorderstatus.Text = DGVfollowUp.SelectedCells(5).Value
            txtBWHOM.Text = DGVfollowUp.SelectedCells(6).Value
            txtProType.Text = DGVfollowUp.SelectedCells(7).Value
            txtRemarksFollowup.Text = DGVfollowUp.SelectedCells(8).Value
            'Dim result As TimeSpan = Convert.ToDateTime(DGVfollowUp.SelectedCells(2).Value).Subtract(Convert.ToDateTime(DGVfollowUp.SelectedCells(0).Value))
            'txtDaysAfter.Text = result.Days.ToString()
            'rwIDDelFollowDetail = DGVfollowUp.CurrentCell.RowIndex





        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub DataLetter_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataLetter.DoubleClick

        Try
            BtnLetterSave.Text = "Update"

            PK_LetterDetailID = DataLetter.SelectedCells(0).Value
            TxtLetterDate.Text = DataLetter.SelectedCells(2).Value
            TxtLetterCreDate.Text = DataLetter.SelectedCells(3).Value
            TxtLetterCardRem.Text = DataLetter.SelectedCells(4).Value
            TxtLetterMailRec.Text = DataLetter.SelectedCells(5).Value
            TxtLetterMailSend.Text = DataLetter.SelectedCells(6).Value
            TxtLetterByWhom.Text = DataLetter.SelectedCells(7).Value
            TxtLetterMailRem.Text = DataLetter.SelectedCells(8).Value



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DataISIGrid_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataISIGrid.DoubleClick
        Try
            If DataISIGrid.Rows.Count > 0 Then
                dtISIFDate.Text = DataISIGrid.SelectedCells(0).Value
                txtISIFollowUp.Text = DataISIGrid.SelectedCells(1).Value
                dtISINFDate.Text = DataISIGrid.SelectedCells(2).Value
                txtISIStatus.Text = DataISIGrid.SelectedCells(3).Value
                txtISIByWhom.Text = DataISIGrid.SelectedCells(4).Value
                txtISIProType.Text = DataISIGrid.SelectedCells(5).Value
                txtISIFollowRemarks.Text = DataISIGrid.SelectedCells(6).Value
                rwIDISIProcessDetail = DataISIGrid.CurrentCell.RowIndex
                Dim result As TimeSpan = Convert.ToDateTime(DataISIGrid.SelectedCells(2).Value).Subtract(Convert.ToDateTime(DataISIGrid.SelectedCells(0).Value))
                txtISINextDays.Text = result.Days.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGRawMaterialData_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGRawMaterialData.DoubleClick
        btnAddItem.Text = "Update"

        Try
            If DGRawMaterialData.Rows.Count > 0 Then
                Pk_OrderRawMaterialId = DGRawMaterialData.SelectedCells(0).Value
                txtItemName.Text = DGRawMaterialData.SelectedCells(1).Value
                txtQty.Text = DGRawMaterialData.SelectedCells(2).Value
                txtPrice.Text = DGRawMaterialData.SelectedCells(3).Value
                txtAmount.Text = DGRawMaterialData.SelectedCells(4).Value

                If (DGRawMaterialData.SelectedCells(5).Value = "YES") Then
                    rbYes.Checked = True
                Else
                    rbNo.Checked = True
                End If

                If (DGRawMaterialData.SelectedCells(6).Value = "YES") Then
                    rbPYes.Checked = True
                Else
                    rbPNo.Checked = True
                End If

                dtRowOrderDate.Text = ChkDBNull(DGRawMaterialData.SelectedCells(7).Value)
                dtRowDisdate.Text = ChkDBNull(DGRawMaterialData.SelectedCells(8).Value)
                txtRowTentDispDate.Text = ChkDBNull(DGRawMaterialData.SelectedCells(9).Value)



            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgLog_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgLog.DoubleClick
        Try
            btnLogSave.Text = "Update"
            Pk_ProjectLetterId = dgLog.Rows(dgLog.CurrentRow.Index).Cells(0).Value
            txtLetterTitle.Text = dgLog.Rows(dgLog.CurrentRow.Index).Cells(1).Value
            txtSendMailBy.Text = dgLog.Rows(dgLog.CurrentRow.Index).Cells(2).Value
            txtSendMailDate.Text = dgLog.Rows(dgLog.CurrentRow.Index).Cells(3).Value
            txtSendCourierBy.Text = dgLog.Rows(dgLog.CurrentRow.Index).Cells(4).Value
            txtSendCourierDate.Text = dgLog.Rows(dgLog.CurrentRow.Index).Cells(5).Value

            txtRecMailBy.Text = dgLog.Rows(dgLog.CurrentRow.Index).Cells(6).Value
            txtRecMailDate.Text = dgLog.Rows(dgLog.CurrentRow.Index).Cells(7).Value


            txtRecCourierBy.Text = dgLog.Rows(dgLog.CurrentRow.Index).Cells(8).Value
            txtRecCourierDate.Text = dgLog.Rows(dgLog.CurrentRow.Index).Cells(9).Value


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub bindGrid()
        TeamId = cmbTeam.SelectedValue 'add Navin 04-03-2015
        If (TeamId > 0) Then
            Dim userAllotedData = linq_obj.SP_Tbl_UserAllotmentDetail_SelectByTeamAndUser(TeamId, 0).Where(Function(p) p.Fk_AddressId = Address_ID).ToList()
            If (userAllotedData.Count) Then
                GvAllotedData.DataSource = userAllotedData
                GvAllotedData.Columns(0).Visible = False
                GvAllotedData.Columns(1).Visible = False
                GvAllotedData.Columns(2).Visible = False
                GvAllotedData.Columns(3).Visible = False
                GvAllotedData.Columns(4).Visible = False
            Else
                GvAllotedData.DataSource = Nothing

            End If
        Else
            GvAllotedData.DataSource = Nothing
        End If

    End Sub
    Private Sub cmbTeam_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTeam.SelectionChangeCommitted
        bindGrid()
    End Sub

    Private Sub DGVOrderdtail_CellValueNeeded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles DGVOrderdtail.CellValueNeeded

    End Sub

    Private Sub dtOrderDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtOrderDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtOrderDate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtOrderDate.Validating
        If Class1.ChkVaildDate(dtOrderDate.Text) = False Then
            MessageBox.Show("Date formate is not valid")
            e.Cancel = True
        End If
    End Sub

    Private Sub txtRecMKT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRecMKT.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub


    Private Sub txtRecMKT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRecMKT.Validating
        If Class1.ChkVaildDate(txtRecMKT.Text) = False Then
            MessageBox.Show("Date formate is not valid")
            e.Cancel = True
        End If
        If txtRecMKT.Text <> "" And dtOrderDate.Text = "" Then
            MessageBox.Show("You cannot enter Rec MKT Date,Because Order date is blank")
            e.Cancel = True
        End If
    End Sub

    Private Sub dtTentativeDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtTentativeDate.KeyPress, txtRecMailDate.KeyPress, txtSendCourierDate.KeyPress, txtRecCourierDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtTentativeDate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtTentativeDate.Validating, txtRecMailDate.Validating, txtSendCourierDate.Validating, txtRecCourierDate.Validating
        If Class1.ChkVaildDate(sender.Text) = False Then
            MessageBox.Show("Date formate is not valid")
            e.Cancel = True
        End If
    End Sub
    Private Sub dtRowOrderDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtRowOrderDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub


    Private Sub dtRowDisdate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtRowDisdate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Function ChkDBNull(ByVal p1 As Object) As String
        If IsDBNull(p1) = True Then
            Return ""
        Else
            Return p1
        End If
    End Function

    Private Sub DGVOrderDetails_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVOrderDetails.CellContentClick

    End Sub

    Private Sub dtRowOrderDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtRowOrderDate.TextChanged

    End Sub

    Private Sub dtRowOrderDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtRowOrderDate.Validating
        If Class1.ChkVaildDate(dtRowOrderDate.Text) = False Then
            MessageBox.Show("Date formate is not valid")
            e.Cancel = True
        End If
    End Sub



    Private Sub dtRowDisdate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtRowDisdate.Validating
        If Class1.ChkVaildDate(dtRowDisdate.Text) = False Then
            MessageBox.Show("Date formate is not valid")
            e.Cancel = True
        End If

        If dtRowDisdate.Text <> "" Then
            If Class1.SetDate(dtRowOrderDate.Text) > Class1.SetDate(dtRowDisdate.Text) Then
                MessageBox.Show("Dis Date sholud less than orderdate")
                e.Cancel = True
            End If
        End If

    End Sub


    Private Sub dtDispatchDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtDispatchDate.TextChanged

    End Sub

    Private Sub dtDispatchDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtDispatchDate.Validating

    End Sub

    Private Sub txtRecMKT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRecMKT.TextChanged

    End Sub

    Private Sub dtTentativeDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtTentativeDate.TextChanged

    End Sub

    Private Sub TxtRecDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtRecDate.KeyPress, TxtDocFDate.KeyPress, TxtDocRDate.KeyPress, TxtFileRegDate.KeyPress, TxtBISInspDate.KeyPress, TxtLicenceDate.KeyPress, dtDispatchDate.KeyPress, txtSendMailDate.KeyPress, txtRecMailDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub



    Private Sub TxtRecDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtRecDate.Validating, TxtDocFDate.Validating, TxtDocRDate.Validating, TxtFileRegDate.Validating, TxtBISInspDate.Validating, TxtLicenceDate.Validating, dtDispatchDate.Validating, txtSendMailDate.Validating, txtRecMailDate.Validating
        If Class1.ChkVaildDate(sender.Text) = False Then
            MessageBox.Show("Date formate is not valid")
            e.Cancel = True
        End If
    End Sub

    Private Sub DGVOrderdtail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVOrderdtail.CellContentClick

    End Sub

    Private Sub DataISIGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataISIGrid.CellContentClick

    End Sub

    Private Sub txtSendMailDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSendMailDate.TextChanged

    End Sub

    Private Sub GvAllotedData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvAllotedData.CellContentClick

    End Sub

    Private Sub DGVfollowUp_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVfollowUp.CellContentClick

    End Sub

    Private Sub DGVFolowup_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVFolowup.CellContentClick

    End Sub

    Private Sub DataLetter_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataLetter.CellContentClick

    End Sub

    Private Sub DataProInst_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataProInst.CellContentClick

    End Sub

    Private Sub DGRawMaterialData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGRawMaterialData.CellContentClick

    End Sub

    Private Sub dgLog_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgLog.CellContentClick

    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged

    End Sub

    Private Sub txtPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice.TextChanged

    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress

    End Sub

    Private Sub txtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged

    End Sub

    Private Sub dtRowDisdate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtRowDisdate.TextChanged

    End Sub

    Public Sub Project_AutoComplated()

        txtProject.AutoCompleteCustomSource.Clear()

        Dim Project = linq_obj.SP_Get_ANNEXURE1_QtypeList().ToList()
        For Each item In Project
            txtProject.AutoCompleteCustomSource.Add(item.Qtype)
        Next

    End Sub


    Private Sub txtProject_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProject.Leave
        Dim Plant = linq_obj.SP_Get_ANNEXURE1_PlantList(txtProject.Text.Trim()).ToList()
        For Each item In Plant
            txtPlantName.AutoCompleteCustomSource.Add(item.Plant)
        Next
        txtPlantName.Focus()

    End Sub

    Private Sub txtPlantName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlantName.Leave
        Dim Model = linq_obj.SP_Get_ANNEXURE1_ModelList(txtPlantName.Text.Trim()).ToList()
        For Each item In Model
            txtModel.AutoCompleteCustomSource.Add(item.Model)
        Next


    End Sub

    Public Sub DGVOrderdtail_Bind()


        If txtProject.Text.Trim() <> "" And txtPlantName.Text.Trim() <> "" And txtModel.Text.Trim() <> "" Then


            If DGVOrderdtail.Rows.Count = 0 Then
                Dim dt As New DataTable
                dt.Columns.Add("Pk_ProjctDetailId")
                dt.Columns.Add("Fk_AddressId")
                dt.Columns.Add("SrNo")
                dt.Columns.Add("PlantScheme")
                dt.Columns.Add("VendorName")
                dt.Columns.Add("EntryDate")
                Dim Data = linq_obj.SP_Get_ANNEXURE1_List(txtProject.Text.Trim(), txtPlantName.Text.Trim(), txtModel.Text.Trim()).ToList().Where(Function(t) t.Sr_No <= 18)
                For Each item In Data
                    dt.Rows.Add(0, Address_ID, item.Sr_No, item.Description, "", Class1.Datecheck(System.DateTime.Now.ToShortDateString()))
                Next
                DGVOrderdtail.DataSource = dt
                DGVOrderdtail.Columns(0).Visible = False
                DGVOrderdtail.Columns(1).Visible = False
                DGVOrderdtail.Columns(4).Visible = False
                txtSrNo.Text = DGVOrderdtail.Rows.Count + 1
            End If

            'Auto Compated Text PlantScheme
            txtPlantScheme.AutoCompleteCustomSource.Clear()
            Dim Model = linq_obj.SP_Get_ANNEXURE1_List(txtProject.Text.Trim(), txtPlantName.Text.Trim(), txtModel.Text.Trim()).ToList()
            For Each item In Model
                txtPlantScheme.AutoCompleteCustomSource.Add(item.Description)
            Next
        End If


    End Sub

    Private Sub txtModel_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtModel.Leave
        'Get Details By Project,Plant,Model

        DGVOrderdtail_Bind()
        txtCapacity.Focus()
    End Sub
    Public Sub Card_Status_False()
        TxtLetterCreDate.Enabled = False
        TxtLetterCardRem.Enabled = False

    End Sub
    Public Sub Other_Status_False()
        TxtLetterDate.Enabled = False
        TxtLetterMailRec.Enabled = False
        TxtLetterMailSend.Enabled = False
        TxtLetterByWhom.Enabled = False
        TxtLetterMailRem.Enabled = False




    End Sub
    Public Sub Card_Status_True()
        TxtLetterCreDate.Enabled = True
        TxtLetterCardRem.Enabled = True

    End Sub
    Public Sub Other_Status_True()
        TxtLetterDate.Enabled = False
        TxtLetterMailRec.Enabled = True
        TxtLetterMailSend.Enabled = True
        TxtLetterByWhom.Enabled = True
        TxtLetterMailRem.Enabled = True




    End Sub

    Private Sub txtModel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtModel.TextChanged

    End Sub

    Private Sub rblisCard_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblisCard.CheckedChanged
        If rblisCard.Checked = True Then
            Other_Status_False()
            Card_Status_True()


        End If
    End Sub

    Private Sub rblisOther_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblisOther.CheckedChanged
        If rblisOther.Checked = True Then
            Card_Status_False()
            Other_Status_True()

        End If

    End Sub

    Private Sub btnAddPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPacking.Click

        Dim ItemName As String
        Dim Capacity As String
        ItemName = ""
        Capacity = ""
        If txtPItemOther.Text.Trim() <> "" Then
            ItemName = txtPItemOther.Text
        Else
            ItemName = ddlPackingItem.Text
        End If
        If txtPCapacityOther.Text.Trim() <> "" Then
            Capacity = txtPCapacityOther.Text
        Else
            Capacity = ddlPackingCapacity.Text
        End If


        Try
            If (btnAddPacking.Text.Trim() = "Add") Then
                linq_obj.SP_Insert_Update_PackingDetail_New(0, Address_ID, ItemName, Capacity, txtPackingType.Text, txtPackingQty.Text, txtPackingVendor.Text, txtPackingSize.Text, txtPackingStatus.Text, txtPackingRemark.Text, txtPackingDispdate.Text, txtPackingDate.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Packing Detail Add Successfull...")

            Else
                linq_obj.SP_Insert_Update_PackingDetail_New(Pk_PackingDetailNew_ID, Address_ID, ItemName, Capacity, txtPackingType.Text, txtPackingQty.Text, txtPackingVendor.Text, txtPackingSize.Text, txtPackingStatus.Text, txtPackingRemark.Text, txtPackingDispdate.Text, txtPackingDate.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Packing Detail Update Successfull...")
            End If
            GvPackingDetailNewList_Bind()
            Packing_Clear()



        Catch ex As Exception

        End Try


    End Sub

    Public Sub Packing_Clear()
        btnAddPacking.Text = "Add"
        ddlPackingCapacity.Text = ""
        txtPackingQty.Text = ""
        txtPackingType.Text = ""
        txtPackingVendor.Text = ""
        txtPackingStatus.Text = ""
        txtPackingDispdate.Text = ""
        txtPackingRemark.Text = ""
        txtPackingSize.Text = ""

        ddlPackingItem.Focus()

    End Sub

    Private Sub GvPackingDetailNewList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvPackingDetailNewList.DoubleClick
        Packing_Clear()
        btnAddPacking.Text = "Update"
        If (Me.GvPackingDetailNewList.SelectedRows.Count > 0) Then
            Pk_PackingDetailNew_ID = Convert.ToInt64(Me.GvPackingDetailNewList.SelectedCells(0).Value)
            Address_ID = Convert.ToInt64(Me.GvPackingDetailNewList.SelectedCells(1).Value)
            Packing_Detail()
        End If
    End Sub
    Public Sub Packing_Detail()

        txtPItemOther.Text = ""
        txtPCapacityOther.Text = ""
        txtPItemOther.Visible = False
        txtPCapacityOther.Visible = False
        Dim CheckPackingItem = linq_obj.SP_Get_Packing_Item_Master_List().ToList().Where(Function(p) p.Packing_Item.ToLower() = Convert.ToString(GvPackingDetailNewList.SelectedCells(2).Value).ToLower())

        If (CheckPackingItem.Count > 0) Then
            ddlPackingItem.Text = Me.GvPackingDetailNewList.SelectedCells(2).Value
            ddlPacking_Item_Capacity_Bind()
            ddlPackingCapacity.Text = Me.GvPackingDetailNewList.SelectedCells(3).Value
        Else
            txtPItemOther.Visible = True
            txtPCapacityOther.Visible = True
            ddlPackingItem.Text = "OTHER"
            ddlPackingCapacity.Text = "OTHER"

            txtPItemOther.Text = Me.GvPackingDetailNewList.SelectedCells(2).Value
            txtPCapacityOther.Text = Me.GvPackingDetailNewList.SelectedCells(3).Value
        End If

        txtPackingType.Text = Me.GvPackingDetailNewList.SelectedCells(4).Value
        txtPackingQty.Text = Me.GvPackingDetailNewList.SelectedCells(5).Value

        txtPackingVendor.Text = Me.GvPackingDetailNewList.SelectedCells(6).Value
        txtPackingSize.Text = Me.GvPackingDetailNewList.SelectedCells(7).Value

        txtPackingStatus.Text = Me.GvPackingDetailNewList.SelectedCells(8).Value
        txtPackingRemark.Text = Me.GvPackingDetailNewList.SelectedCells(9).Value
        txtPackingDispdate.Text = Me.GvPackingDetailNewList.SelectedCells(10).Value
        txtPackingDate.Text = Me.GvPackingDetailNewList.SelectedCells(11).Value

    End Sub

    Private Sub btnPackingDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPackingDelete.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            Address_ID = Convert.ToInt64(Me.GvPackingDetailNewList.SelectedCells(1).Value)
            linq_obj.SP_Delete_PackingDetailBy_ID(Me.GvPackingDetailNewList.SelectedCells(0).Value)
            linq_obj.SubmitChanges()
            MessageBox.Show("Delete Sucessfully")
            GvPackingDetailNewList_Bind()
            Packing_Clear()
        End If


    End Sub

    Private Sub btnPfolowupnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnFileAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileAdd.Click


        If btnFileAdd.Text.Trim() = "Add" Then
            linq_obj.SP_Insert_Update_Tbl_Plant_Drawing_Upload(0, Address_ID, txtFileName.Text.Trim(), txtFilePath.Text.Trim(), txtFileRemarks.Text.Trim())
            linq_obj.SubmitChanges()
            Document_Clear()
            MessageBox.Show("Add Sucessfully..")
        Else

            linq_obj.SP_Insert_Update_Tbl_Plant_Drawing_Upload(Pk_PlantDraw_Doc_ID, Address_ID, txtFileName.Text.Trim(), txtFilePath.Text.Trim(), txtFileRemarks.Text.Trim())
            linq_obj.SubmitChanges()
            Document_Clear()
            MessageBox.Show("Update Sucessfully..")
        End If

        Document_Clear()
        GvFileList_Bind()

    End Sub
    Public Sub Document_Clear()

        txtFileName.Text = ""
        txtFilePath.Text = ""
        txtFileRemarks.Text = ""
        btnFileAdd.Text = "Add"


    End Sub

    Private Sub GvFileList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvFileList.DoubleClick

        btnFileAdd.Text = "Update"
        Pk_PlantDraw_Doc_ID = GvFileList.SelectedCells(0).Value
        txtFileName.Text = GvFileList.SelectedCells(1).Value
        txtFilePath.Text = GvFileList.SelectedCells(2).Value
        txtFileRemarks.Text = GvFileList.SelectedCells(3).Value
    End Sub

    Private Sub btnFileBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileBrowse.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\PlantUploadDoc\") + openFileDialog1.SafeFileName
        txtFilePath.Text = imgSrc
    End Sub

    Private Sub GvFileList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvFileList.CellContentClick
        If (e.ColumnIndex = 4) Then
            Dim fullpath As String
            fullpath = GvFileList.Rows(e.RowIndex).Cells(2).Value.ToString()

            System.Diagnostics.Process.Start(fullpath.Replace("\", "\\"))

        End If
    End Sub


    Private Sub btnBrowse1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse1.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Pic1.Image = Image.FromFile(OpenFileDialog1.FileName)
            Pic1.SizeMode = PictureBoxSizeMode.StretchImage
        End If

    End Sub

    Private Sub btnBrowse2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse2.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Pic2.Image = Image.FromFile(OpenFileDialog1.FileName)
            Pic2.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Pic3.Image = Image.FromFile(OpenFileDialog1.FileName)
            Pic3.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub

    Private Sub btnbrowse4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowse4.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Pic4.Image = Image.FromFile(OpenFileDialog1.FileName)
            Pic4.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub

    Private Sub btnPlantDraDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlantDraDelete.Click


        Dim result As DialogResult = MessageBox.Show("Are You Sure Delete Document ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            linq_obj.SP_Delete_Tbl_Plant_Drawing_Upload(Convert.ToInt64(GvFileList.SelectedRows(0).Cells("Pk_PlantDraw_Doc_ID").Value))
            linq_obj.SubmitChanges()

            MessageBox.Show("Delete Sucessfully...")
            GvFileList_Bind()
            Document_Clear()
        End If

    End Sub

    Private Sub btnProjectDetailDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProjectDetailDelete.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure Delete Project Detail ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            linq_obj.SP_Delete_Tbl_ProjectDetail_By_ID(Convert.ToInt64(DGVOrderdtail.SelectedCells(0).Value))
            linq_obj.SubmitChanges()

            MessageBox.Show("Delete Sucessfully...")
            DGVOrderdtail_All_Data()
            btnAddOrder_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub BtnLetterDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLetterDelete.Click

    End Sub

    Private Sub btnAddNewPlantDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewPlantDraw.Click
        Document_Clear()
    End Sub

    Private Sub ddlPackingItem_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPackingItem.SelectionChangeCommitted

        If ddlPackingItem.Text.Trim.ToLower() = "other" Then
            txtPItemOther.Visible = True
            txtPCapacityOther.Visible = True

        Else
            txtPItemOther.Visible = False
            txtPCapacityOther.Visible = False

        End If

        ddlPacking_Item_Capacity_Bind()
    End Sub

    Private Sub txtDtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDtype.SelectedIndexChanged
        txtPackingDispdate.Enabled = True
        txtPackingDispdate.Text = ""

        If txtDtype.Text.Trim().ToLower() = "full" Then
            If dtDispatchDate.Text.Trim() <> "" Then
                txtPackingDispdate.Text = dtDispatchDate.Text
                txtPackingDispdate.Enabled = False
            Else
                MessageBox.Show("Please Enter Dispatch Date")
            End If


        End If


    End Sub

    Private Sub txtPackingDispdate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPackingDispdate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub ddlMileStone_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

   

    Private Sub ddlPipe_ProjectType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPipe_ProjectType.SelectionChangeCommitted
        chkMachineList.Visible = False
        lblMachine.Visible = False
        If (ddlPipe_ProjectType.Text.ToLower() = "production") Then
            chkMachineList.Visible = True
            lblMachine.Visible = True
        End If


        Pipeline_Milestone_Bind()


    End Sub

    Private Sub ddlMileStone_SelectionChangeCommitted_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMileStone.SelectionChangeCommitted
        Pipeline_Machine_Engineer_Bind()
    End Sub
    Public Sub Pipeline_Machine_Engineer_Bind()

        Dim DepartmentID As Integer
        Dim ProjectType As Integer
        
        Dim Department As Integer
        Department = 2
        'Machine Name
        'Pipeline Deaprtment 2 =Service 
        'Pipeline Project Type 8 = EC
        Dim dataMachineName = linq_obj.SP_Get_Pipline_ProjectDepartment_Milestone_By_Address_ID(Department, Convert.ToInt32(ddlPipe_ProjectType.SelectedValue), Address_ID).ToList().Where(Function(p) p.Pk_Milestone_ID = Convert.ToInt32(ddlMileStone.SelectedValue)).ToList()
        Dim dtMachine As New DataTable
        dtMachine.Columns.Add("MachineName")
        For Each item As SP_Get_Pipline_ProjectDepartment_Milestone_By_Address_IDResult In dataMachineName
            Dim strArr() As String
            If item.MachineName.Contains(",") Then
                strArr = item.MachineName.Split(",")
                For index = 0 To strArr.Length - 1
                    dtMachine.Rows.Add(strArr(index))
                Next
            Else
                dtMachine.Rows.Add(item.MachineName)
            End If

        Next
        dtMachine = dtMachine.DefaultView.ToTable(True, "MachineName")
        chkMachineList.Items.Clear()
        For index = 0 To dtMachine.Rows.Count - 1
            chkMachineList.Items.Add(dtMachine.Rows(index)(0))
        Next

        'Engineer Name

        Dim dtEngineer As New DataTable
        dtEngineer.Columns.Add("EngineerName")
        For Each item As SP_Get_Pipline_ProjectDepartment_Milestone_By_Address_IDResult In dataMachineName
            Dim strArr() As String
            If item.EngineerName.Contains(",") Then
                strArr = item.EngineerName.Split(",")
                For index = 0 To strArr.Length - 1
                    dtEngineer.Rows.Add(strArr(index))
                Next
            Else
                dtEngineer.Rows.Add(item.EngineerName)
            End If

        Next
        dtEngineer = dtEngineer.DefaultView.ToTable(True, "EngineerName")
        ChkEngineerList.Items.Clear()
        For index = 0 To dtEngineer.Rows.Count - 1
            ChkEngineerList.Items.Add(dtEngineer.Rows(index)(0))
        Next



    End Sub

    Private Sub btnPipelineAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPipelineAdd.Click
        Try
            Dim MachineName As String
            MachineName = ""
            For Each item In chkMachineList.CheckedItems
                MachineName = MachineName + item + ","
            Next

            Dim StatusId As Integer

            If rblP_Open.Checked = True Then
                StatusId = 1
            Else
                StatusId = 2

            End If

            Dim EngineerID As String
            EngineerID = ""
            For Each item In ChkEngineerList.CheckedItems
                Dim strArr() As String
                strArr = item.Split("|")
                EngineerID = EngineerID + strArr(1) + ","
            Next


            If EngineerID.Trim() <> "" Then
                linq_obj.SP_Insert_Update_Pipeline_Project_Log_Entry(0, FK_ProjectMaster_ID, Convert.ToInt32(ddlMileStone.SelectedValue), MachineName.Trim(), EngineerID.Trim(), txtP_Remarks.Text, StatusId, 1, txtP_Date.Value)
                linq_obj.SubmitChanges()
                MessageBox.Show("Add Sucessfully...")
                Gv_Project_Log_Bind()
                Clear_Pipeline()
            Else
                MessageBox.Show("Please Select Machine and Engineer")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub Clear_Pipeline()
        txtP_Remarks.Text = ""
        rblP_Open.Checked = True

    End Sub
    Public Sub Gv_Project_Log_Bind()
        Dim ProjectLog = linq_obj.SP_Get_Pipeline_Project_Log_Entry_By_AddressID(0, FK_ProjectMaster_ID).ToList()
        If ProjectLog.Count > 0 Then
            Gv_Project_Log.DataSource = ProjectLog
            Gv_Project_Log.Columns(0).Visible = False
        End If


    End Sub

    Private Sub btnPDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDelete.Click
        If Gv_Project_Log.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                linq_obj.SP_Delete_Pipeline_Project_Log_Entry_By_ID(Convert.ToInt64(Gv_Project_Log.SelectedCells(0).Value))
                linq_obj.SubmitChanges()
                Gv_Project_Log_Bind()
            End If
        End If
    End Sub
End Class