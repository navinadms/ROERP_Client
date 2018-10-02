Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports

Public Class InquiryForm
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public SoftwareID As Integer = 0
    Public focussedTextBox As TextBox
    Dim Address_ID As Integer
    Dim PK_DocumentId As Integer 'add navin 1-10-2015
    Dim PK_Enq_Order_Status_Master_ID As Integer
    Dim Pk_Enq_Mark_Foll_ID As Integer
    Dim Rec_Count_ClientMaster As Boolean
    Dim Rec_Count_EnqMaster As Boolean
    Dim Rec_Count_WaterMaster As Boolean
    Dim Rec_Count_FollowMaster As Boolean
    Dim Rec_Count_FollowDetails As Boolean
    Dim Rec_Count_BioDataDetails As Boolean
    Dim Rec_Count_VisitorDetails As Boolean
    Dim tblFollowup As New DataTable
    Dim tblVisitor As New DataTable
    Dim msPicClient As New MemoryStream
    Dim msPic1 As New MemoryStream
    Dim msPic2 As New MemoryStream
    Dim msPic3 As New MemoryStream
    Dim msPic4 As New MemoryStream
    Dim dataTable As DataTable
    Dim currentRow As Integer
    Dim currentRowVisitor As Integer
    Dim VisitorID As Integer

    Private Sub ShowForm(ByVal _frm As Form)
        _frm.MdiParent = Me
        _frm.MaximizeBox = False
        _frm.MinimizeBox = False
        _frm.StartPosition = FormStartPosition.CenterScreen
        '  _frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
        _frm.Show()
    End Sub


    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            'TODO: Add code here to open the file.
        End If
    End Sub
    Public Sub ddlSubCategory_Bind()

        txtFD_EnqType.Items.Clear()
        Dim subcategory = linq_obj.SP_Get_AddSubCategory().ToList()
        txtFD_EnqType.DataSource = subcategory
        txtFD_EnqType.DisplayMember = "SubCategory"
        txtFD_EnqType.ValueMember = "Pk_AddressSubID"
        txtFD_EnqType.AutoCompleteMode = AutoCompleteMode.Append
        txtFD_EnqType.DropDownStyle = ComboBoxStyle.DropDownList
        txtFD_EnqType.AutoCompleteSource = AutoCompleteSource.ListItems

    End Sub
    Public Sub ddlEnqOrderStatus_Bind()
        Dim EnqOrderReason = linq_obj.SP_Get_Enq_Order_Type_Reason(txtFD_EnqType.Text).ToList()
        ddlEnqOrderStatus.DataSource = EnqOrderReason
        ddlEnqOrderStatus.DisplayMember = "Reason"
        ddlEnqOrderStatus.ValueMember = "PK_Enq_Order_Reason_ID"
        ddlEnqOrderStatus.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqOrderStatus.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqOrderStatus.AutoCompleteSource = AutoCompleteSource.ListItems

    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ddlSubCategory_Bind()
        chkAllStatus.Enabled = True
        getAutoCompleteData("Name")
        getAutoCompleteData("City")
        'AutoComplete_Text()
        bindAllDocumentData()
        GvInEnq_Bind()
        If Class1.global.UserName = "RAVI KHAKHARIYA" Then
            btnDeleteInquiry.Visible = True
        Else
            'TabControl1.TabPages.Remove(TPClientBdata)
            'TabControl1.TabPages.Remove(TPvisitordetail)
            btnDeleteInquiry.Visible = False
        End If
        bindClientDataTexts()
        bindAllDocumentData()
        ''  ddlEnqType_Bind()
        RavSoft.CueProvider.SetCue(txtvalue1, "Email 1")
        RavSoft.CueProvider.SetCue(txtValue2, "Community")
        ddlEnqMasterList_Data()

        'Enq Status Tabe
        ddlExecutive1_Bind()
        ddlExecutive2_Bind()
        ddlStatusSales_Bind()
        ddlStatusTeam_Bind()
        ddlEnqStatusType_Bind()


    End Sub

    Public Sub ddlExecutive1_Bind()

        'User 
        ddlExecutive1.DataSource = Nothing
        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_UserId")
        datatable.Columns.Add("UserName")
        Dim datauser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In datauser
            datatable.Rows.Add(item.Pk_UserId, item.UserName)
        Next
        Dim newRow As DataRow = datatable.NewRow()
        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        ddlExecutive1.DataSource = datatable
        ddlExecutive1.DisplayMember = "UserName"
        ddlExecutive1.ValueMember = "Pk_UserId"
        ddlExecutive1.AutoCompleteMode = AutoCompleteMode.Append
        ddlExecutive1.DropDownStyle = ComboBoxStyle.DropDownList
        ddlExecutive1.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Public Sub ddlExecutive2_Bind()

        'User 
        ddlExecutive2.DataSource = Nothing
        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_UserId")
        datatable.Columns.Add("UserName")
        Dim datauser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In datauser
            datatable.Rows.Add(item.Pk_UserId, item.UserName)
        Next
        Dim newRow As DataRow = datatable.NewRow()
        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        ddlExecutive2.DataSource = datatable
        ddlExecutive2.DisplayMember = "UserName"
        ddlExecutive2.ValueMember = "Pk_UserId"
        ddlExecutive2.AutoCompleteMode = AutoCompleteMode.Append
        ddlExecutive2.DropDownStyle = ComboBoxStyle.DropDownList
        ddlExecutive2.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub ddlStatusSales_Bind()

        'User 
        ddlEnqStatusSales.DataSource = Nothing
        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_UserId")
        datatable.Columns.Add("UserName")
        Dim datauser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In datauser
            datatable.Rows.Add(item.Pk_UserId, item.UserName)
        Next
        Dim newRow As DataRow = datatable.NewRow()
        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        ddlEnqStatusSales.DataSource = datatable
        ddlEnqStatusSales.DisplayMember = "UserName"
        ddlEnqStatusSales.ValueMember = "Pk_UserId"
        ddlEnqStatusSales.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqStatusSales.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqStatusSales.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Public Sub ddlStatusTeam_Bind()

        'User 
        ddlEnqStatusTeam.DataSource = Nothing
        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_TeamId")
        datatable.Columns.Add("TeamName")
        Dim dataTeam = linq_obj.SP_Tbl_TeamMaster_SelectAll().ToList()
        For Each item As SP_Tbl_TeamMaster_SelectAllResult In dataTeam
            datatable.Rows.Add(item.Pk_TeamId, item.TeamName)
        Next
        Dim newRow As DataRow = datatable.NewRow()
        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        ddlEnqStatusTeam.DataSource = datatable
        ddlEnqStatusTeam.DisplayMember = "TeamName"
        ddlEnqStatusTeam.ValueMember = "Pk_TeamId"
        ddlEnqStatusTeam.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqStatusTeam.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqStatusTeam.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub ddlEnqStatusType_Bind()
       
        Dim EnqType = linq_obj.SP_Get_Enq_Order_Type_List().ToList().Where(Function(p) p.Enq_Order_Type <> "REGRET").ToList()
        ddlEnqStatusType.DataSource = EnqType
        ddlEnqStatusType.DisplayMember = "Enq_Order_Type"
        ddlEnqStatusType.ValueMember = "PK_Enq_Order_Type_ID"
        ddlEnqStatusType.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqStatusType.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqStatusType.AutoCompleteSource = AutoCompleteSource.ListItems

    End Sub

    Public Sub Edit_ddlEnqStatusType_Bind()

        Dim EnqType = linq_obj.SP_Get_Enq_Order_Type_List().ToList().ToList()
        ddlEnqStatusType.DataSource = EnqType
        ddlEnqStatusType.DisplayMember = "Enq_Order_Type"
        ddlEnqStatusType.ValueMember = "PK_Enq_Order_Type_ID"
        ddlEnqStatusType.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqStatusType.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqStatusType.AutoCompleteSource = AutoCompleteSource.ListItems

    End Sub

    Public Sub ddlEnqStatusSubType_Bind()

        Dim EnqOrderStatus = linq_obj.SP_Get_Enq_Order_Type_Reason(ddlEnqStatusType.Text).ToList()
        ddlEnqStatusSubType.DataSource = EnqOrderStatus
        ddlEnqStatusSubType.DisplayMember = "Reason"
        ddlEnqStatusSubType.ValueMember = "PK_Enq_Order_Reason_ID"
        ddlEnqStatusSubType.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqStatusSubType.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqStatusSubType.AutoCompleteSource = AutoCompleteSource.ListItems

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

    Public Sub AutoComplete_Text()
        txtPartyName.AutoCompleteCustomSource.Clear()

        'Dim data = linq_obj.SP_Get_AddressListAutoComplete("Name").ToList()
        'For Each iteam As SP_Get_AddressListAutoCompleteResult In data
        '    txtPartyName.AutoCompleteCustomSource.Add(iteam.Result)
        'Next

        txtInqSearchOfferNo.AutoCompleteCustomSource.Clear()
        Dim dataEnq = linq_obj.SP_Get_AddressListAutoComplete("EnqNo").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In dataEnq
            txtInqSearchOfferNo.AutoCompleteCustomSource.Add(iteam.Result)
        Next

        txtF_CourierBy.AutoCompleteCustomSource.Clear()
        txtFD_BWHOM.AutoCompleteCustomSource.Clear()
        txtPFollowBy.AutoCompleteCustomSource.Clear()
        Dim GetUser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In GetUser
            txtF_CourierBy.AutoCompleteCustomSource.Add(Convert.ToString(item.UserName))
            txtFD_BWHOM.AutoCompleteCustomSource.Add(Convert.ToString(item.UserName))
            txtPFollowBy.AutoCompleteCustomSource.Add(Convert.ToString(item.UserName))
        Next
        txtFD_follow1.AutoCompleteCustomSource.Clear()
        txtFD_status.AutoCompleteCustomSource.Clear()
        Dim getfolowUp = linq_obj.SP_Get_Enq_FollowDetailsList().Distinct().ToList()
        For Each itm As SP_Get_Enq_FollowDetailsListResult In getfolowUp
            txtFD_follow1.AutoCompleteCustomSource.Add(Convert.ToString(itm.Followup))
            txtFD_status.AutoCompleteCustomSource.Add(Convert.ToString(itm.Status))

            '  txtFD_EnqType.AutoCompleteCustomSource.Add(itm.EnqType)
            ' txtFD_Remarks.AutoCompleteCustomSource.Add(Convert.ToString(itm.Remarks))
        Next

        txtFD_EnqType.AutoCompleteCustomSource.Clear()
        txtPEtype.AutoCompleteCustomSource.Clear()
        txtWtypeofEnq.AutoCompleteCustomSource.Clear()
        Dim enqStatusData = linq_obj.SP_Get_EnqTypeAll().ToList()
        For Each itemenq As SP_Get_EnqTypeAllResult In enqStatusData
            txtFD_EnqType.AutoCompleteCustomSource.Add(itemenq.SubCategory)
            txtPEtype.AutoCompleteCustomSource.Add(itemenq.SubCategory)
            txtWtypeofEnq.AutoCompleteCustomSource.Add(itemenq.SubCategory)
        Next


        txtPerDay.AutoCompleteCustomSource.Clear()
        txtPerReg.AutoCompleteCustomSource.Clear()
        TXTTTDS.AutoCompleteCustomSource.Clear()
        TXTTTH.AutoCompleteCustomSource.Clear()
        TXTTPH.AutoCompleteCustomSource.Clear()
        TXTRPH.AutoCompleteCustomSource.Clear()
        TXTRTDS.AutoCompleteCustomSource.Clear()
        TXTRTH.AutoCompleteCustomSource.Clear()
        Dim EnquiryDetails = linq_obj.SP_Get_Enq_EnqMasterList().Distinct().Take(10).ToList()

        For Each itemenq As SP_Get_Enq_EnqMasterListResult In EnquiryDetails
            txtPerDay.AutoCompleteCustomSource.Add(itemenq.PerDay)
            txtPerReg.AutoCompleteCustomSource.Add(itemenq.PerReg)
            TXTTTDS.AutoCompleteCustomSource.Add(itemenq.TTDS)
            TXTTTH.AutoCompleteCustomSource.Add(itemenq.TTH)
            TXTTPH.AutoCompleteCustomSource.Add(itemenq.TPH)
            TXTRPH.AutoCompleteCustomSource.Add(itemenq.RPH)
            TXTRTDS.AutoCompleteCustomSource.Add(itemenq.RTDS)
            TXTRTH.AutoCompleteCustomSource.Add(itemenq.RTH)
        Next

        txtApplication.AutoCompleteCustomSource.Clear()
        ' txtW_Remark.AutoCompleteCustomSource.Add(itemWater.Remarks)
        txtW_EnqAtta.AutoCompleteCustomSource.Clear()
        txtW_SalesExc.AutoCompleteCustomSource.Clear()
        txtW_EnqAllocated.AutoCompleteCustomSource.Clear()


        Dim WaterMasterDetails = linq_obj.SP_Get_Enq_WaterMasterList().Distinct().ToList()

        For Each itemWater As SP_Get_Enq_WaterMasterListResult In WaterMasterDetails
            txtApplication.AutoCompleteCustomSource.Add(itemWater.Application)
            ' txtW_Remark.AutoCompleteCustomSource.Add(itemWater.Remarks)
            txtW_EnqAtta.AutoCompleteCustomSource.Add(itemWater.EnqAttandBy)
            txtW_SalesExc.AutoCompleteCustomSource.Add(Convert.ToString(itemWater.SalesExc))
            txtW_EnqAllocated.AutoCompleteCustomSource.Add(Convert.ToString(itemWater.EnqAllotted))
        Next
        txtIncliens.AutoCompleteCustomSource.Clear()
        txtReadiness.AutoCompleteCustomSource.Clear()
        txtMrkVision.AutoCompleteCustomSource.Clear()
        txtImpPerson.AutoCompleteCustomSource.Clear()
        txtInflu.AutoCompleteCustomSource.Clear()
        txtStrength.AutoCompleteCustomSource.Clear()
        txtSpExp4Final.AutoCompleteCustomSource.Clear()
        txtFinStrength.AutoCompleteCustomSource.Clear()
        txtMktStrength.AutoCompleteCustomSource.Clear()
        txtAwt4Comp.AutoCompleteCustomSource.Clear()
        txtProduct.AutoCompleteCustomSource.Clear()
        txtLand.AutoCompleteCustomSource.Clear()
        txtPower.AutoCompleteCustomSource.Clear()
        Dim ItemVisitorMaster = linq_obj.SP_Get_Enq_VisitorMasterList().Distinct().Take(10).ToList()

        For Each item As SP_Get_Enq_VisitorMasterListResult In ItemVisitorMaster
            txtIncliens.AutoCompleteCustomSource.Add(Convert.ToString(item.Incliness))
            txtReadiness.AutoCompleteCustomSource.Add(Convert.ToString(item.Readiness))
            txtMrkVision.AutoCompleteCustomSource.Add(Convert.ToString(item.Mkt_Vision))
            txtImpPerson.AutoCompleteCustomSource.Add(Convert.ToString(item.ImpPerson))
            txtInflu.AutoCompleteCustomSource.Add(Convert.ToString(item.Influ))
            txtStrength.AutoCompleteCustomSource.Add(Convert.ToString(item.Strength))
            txtSpExp4Final.AutoCompleteCustomSource.Add(Convert.ToString(item.Sp_Expect))
            txtFinStrength.AutoCompleteCustomSource.Add(Convert.ToString(item.Fin_Strength))
            txtMktStrength.AutoCompleteCustomSource.Add(Convert.ToString(item.Mkt_Strength))
            txtAwt4Comp.AutoCompleteCustomSource.Add(Convert.ToString(item.AwtFo))
            txtProduct.AutoCompleteCustomSource.Add(Convert.ToString(item.Product))
            txtLand.AutoCompleteCustomSource.Add(Convert.ToString(item.Land))
            txtPower.AutoCompleteCustomSource.Add(Convert.ToString(item.Power))
        Next

        txtPVstatus.AutoCompleteCustomSource.Clear()
        txtPSExec.AutoCompleteCustomSource.Clear()

        txtPDuration.AutoCompleteCustomSource.Clear()
        Dim itemVisitorDetails = linq_obj.SP_Get_Enq_VisitorDetailsList.Distinct().ToList()
        For Each item As SP_Get_Enq_VisitorDetailsListResult In itemVisitorDetails
            txtPVstatus.AutoCompleteCustomSource.Add(Convert.ToString(item.V_Status))
            txtPSExec.AutoCompleteCustomSource.Add(Convert.ToString(item.SalesExc))
            ' txtPViRemarks.AutoCompleteCustomSource.Add(Convert.ToString(item.Remarks))
            txtPDuration.AutoCompleteCustomSource.Add(Convert.ToString(item.Duration))
            ' txtPEtype.AutoCompleteCustomSource.Add(item.E_Type)
            '  txtpjbvremarks.AutoCompleteCustomSource.Add(Convert.ToString(item.JBVRemarks))


        Next



    End Sub
    Public Sub bindAllDocumentData()

        ' ddlDocumentType.Items.Clear()
        Dim dataDoc = linq_obj.SP_Select_Tbl_DocMaster().ToList()

        ddlDocumentType.DataSource = dataDoc
        ddlDocumentType.DisplayMember = "DocumentName"
        ddlDocumentType.ValueMember = "Pk_DocId"
        ddlDocumentType.AutoCompleteMode = AutoCompleteMode.Append
        ddlDocumentType.DropDownStyle = ComboBoxStyle.DropDownList
        ddlDocumentType.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub ddlEnqMasterList_Data()

        ' ddlDocumentType.Items.Clear()
        Dim dataMarketing = linq_obj.SP_Get_Enq_Marketing_List().ToList()

        ddlMarketing.DataSource = dataMarketing
        ddlMarketing.DisplayMember = "MarketingName"
        ddlMarketing.ValueMember = "Pk_Enq_Master_List_ID"
        ddlMarketing.AutoCompleteMode = AutoCompleteMode.Append
        ddlMarketing.DropDownStyle = ComboBoxStyle.DropDownList
        ddlMarketing.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub GvInEnq_Bind()

        bindEnqGrid()
        btnrefresh_Click(Nothing, Nothing)
        txtTotEnquiry.Text = GvInquery.RowCount.ToString()
        'End If
    End Sub
    Public Sub bindAllData(ByVal addressid As Integer)
        cleanAll()
        Address_ID = addressid
        GetClientDetails_Bind()
        ' bindAllDocumentData()
        BindDocumentDataByID(addressid)
    End Sub
    Public Sub BindDocumentDataByID(ByVal addressid As Integer)

        Dim dataDoc = linq_obj.SP_Select_Tbl_DocumentMaster(addressid).ToList()
        GvdDatadocumentList.DataSource = dataDoc
        GvdDatadocumentList.Columns(0).Visible = False
        GvdDatadocumentList.Columns(2).Visible = False


    End Sub
    Private Sub GvInquery_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvInquery.DoubleClick
        cleanAll()
        Enq_Marketing_Clear()
        Marketing_Clear()
        Enq_Status_Clear()
        Try
            Address_ID = Convert.ToInt32(Me.GvInquery.SelectedCells(0).Value)
            bindAllData(Address_ID)
            GvMarketingList_Bind()
            'TabControl1.TabPages(0).Select()

            GvEnqStatusLog_Bind()
        Catch ex As Exception

        End Try
    End Sub
     

    Public Sub Edit_Enq_StatusDetail()
        btnStatusSubmit.Visible = True
        lblOrderStatusID.Visible = False
        ddlEnqStatusType.Enabled = True
        ddlEnqStatusSubType.Enabled = True 
        Dim EnqStatus = linq_obj.SP_Get_Enq_Order_Status_Master_By_Address_ID(Address_ID).ToList().Where(Function(p) p.PK_Enq_Order_Status_Master_ID = PK_Enq_Order_Status_Master_ID).ToList()

        If EnqStatus.Count > 0 Then
            For Each item As SP_Get_Enq_Order_Status_Master_By_Address_IDResult In EnqStatus
                btnStatusSubmit.Text = "Update"
                btnStatusSubmit.Visible = True
                PK_Enq_Order_Status_Master_ID = item.PK_Enq_Order_Status_Master_ID
                lblOrderStatusID.Text = item.FK_Enq_Order_Reason_ID.ToString()
                ddlEnqStatusType.SelectedValue = item.FK_Enq_Order_Type_ID
                ddlEnqStatusType.Enabled = False

                ddlEnqStatusType_SelectionChangeCommitted(Nothing, Nothing)
                ddlEnqStatusSubType.SelectedValue = item.FK_Enq_Order_Reason_ID
                ddlEnqStatusSubType.Enabled = False

                dtEnqStatusDate.Text = item.Enq_Order_StatusDate
                txtEnqStatusValue.Text = Convert.ToDecimal(item.OrderValue).ToString("N2")
                txtEnqStatusOrderNo.Text = item.OrderNo
                txtEnqStatusDispValue.Text = Convert.ToDecimal(item.DispValue).ToString("N2")

                If Convert.ToString(item.Fk_User_Executive_ID1) <> "" Then
                    ddlExecutive1.SelectedValue = item.Fk_User_Executive_ID1
                End If
                If Convert.ToString(item.Fk_User_Executive_ID2) <> "" Then
                    ddlExecutive2.SelectedValue = item.Fk_User_Executive_ID2
                End If
                If Convert.ToString(item.Fk_User_Sales_ID) <> "" Then
                    ddlEnqStatusSales.SelectedValue = item.Fk_User_Sales_ID
                End If
                If Convert.ToString(item.Fk_User_Team_ID) <> "" Then
                    ddlEnqStatusTeam.SelectedValue = item.Fk_User_Team_ID
                End If
                txtEnqStatusRemarks.Text = item.Remarks
            Next

        End If

    End Sub

    Public Sub GvEnqStatusLog_Bind()
        Dim EnqStatusList = linq_obj.SP_Get_Enq_Order_Status_Master_By_Address_ID(Address_ID).OrderByDescending(Function(p) p.PK_Enq_Order_Status_Master_ID).ToList()
        If EnqStatusList.Count > 0 Then
            Dim dt As New DataTable
            dt.Columns.Add("ID")
            dt.Columns.Add("EnqType")
            dt.Columns.Add("SubType")
            dt.Columns.Add("Value")
            dt.Columns.Add("OrderNo")
            dt.Columns.Add("DispValue")
            dt.Columns.Add("StatusDate")
            For Each item_L As SP_Get_Enq_Order_Status_Master_By_Address_IDResult In EnqStatusList
                dt.Rows.Add(item_L.PK_Enq_Order_Status_Master_ID, item_L.Enq_Order_Type, item_L.Reason, Convert.ToDecimal(item_L.OrderValue).ToString("N2"), item_L.OrderNo, Convert.ToDecimal(item_L.DispValue).ToString("N2"), Convert.ToDateTime(item_L.Enq_Order_StatusDate).ToString("dd/MM/yyyy"))

            Next
            GvEnqStatusLog.DataSource = dt
            GvEnqStatusLog.Columns(0).Visible = False
        Else
            GvEnqStatusLog.DataSource = Nothing

        End If
    End Sub
    Public Sub GetClientDetails_Bind()

        If Address_ID <> 0 Then

            Try
                'Get Enqiury Details
                Dim EnquiryDetailsnew = linq_obj.SP_Get_Enq_EnqMasterListByID(Address_ID).ToList()
                If EnquiryDetailsnew.Count > 0 Then
                    Rec_Count_EnqMaster = True
                Else
                    Rec_Count_EnqMaster = False
                End If
                For Each item As SP_Get_Enq_EnqMasterListByIDResult In EnquiryDetailsnew
                    txtRef.Text = item.Reference
                    txtenquiryfor.Text = item.EnqFor
                    txtPerHr.Text = item.PerHr
                    txtPerDay.Text = item.PerDay
                    txtPerReg.Text = item.PerReg
                    TXTRTDS.Text = item.RTDS
                    TXTTTDS.Text = item.TTDS
                    TXTRTH.Text = item.RTH
                    TXTTTH.Text = item.TTH
                    TXTRPH.Text = item.RPH
                    TXTTPH.Text = item.TPH
                Next
                'Get WaterMaster Details
                Dim WaterMasterDetails = linq_obj.SP_Get_Enq_WaterMasterListByID(Address_ID).ToList()
                If WaterMasterDetails.Count > 0 Then
                    Rec_Count_WaterMaster = True
                Else
                    Rec_Count_WaterMaster = False
                End If
                For Each item As SP_Get_Enq_WaterMasterListByIDResult In WaterMasterDetails
                    txtApplication.Text = item.Application
                    txtWtypeofEnq.Text = item.TypeofEnq
                    txtW_Remark.Text = item.Remarks
                    txtW_EnqAtta.Text = item.EnqAttandBy
                    txtW_SalesExc.Text = item.SalesExc
                    txtW_EnqAllocated.Text = item.EnqAllotted

                    dtCommitVisitIn.Text = If(Convert.ToString(item.CommitVisitIn) = "01-01-1900 00:00:00", "", Convert.ToString(item.CommitVisitIn))
                    dtCommitVisitOut.Text = If(Convert.ToString(item.CommitVisitOut) = "01-01-1900 00:00:00", "", Convert.ToString(item.CommitVisitOut))
                    dtFinalizeBy.Text = If(Convert.ToString(item.FinaliseBy) = "01-01-1900 00:00:00", "", Convert.ToString(item.FinaliseBy))
                Next

                'Get FollowMaster Details
                Dim FollowMaster = linq_obj.SP_Get_Enq_FollowMasterListByID(Address_ID).ToList()
                If FollowMaster.Count > 0 Then
                    Rec_Count_FollowMaster = True
                Else
                    Rec_Count_FollowMaster = False
                End If
                For Each item As SP_Get_Enq_FollowMasterListByIDResult In FollowMaster
                    txtF_offerNo.Text = item.OfferNo
                    dtF_OfferDate.Text = item.OfferDate 'change navin 17-02-2016
                    dtF_OfferTime.Text = item.OfferTime 'change navin 17-02-2016
                    txtF_ProdModel.Text = item.ProductModel
                    txtF_CourierBy.Text = item.CourierBy
                    txtF_CommValue.Text = item.CommValue
                Next
                'Get FollowDetails

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
                GRVFDData.DataSource = tblFollowup
                Try
                    'Get Client Bio-Data Details
                    Dim BioDataDetails = linq_obj.SP_Get_Enq_BioDataMasterListByID(Address_ID).ToList()
                    If BioDataDetails.Count > 0 Then
                        Rec_Count_BioDataDetails = True
                    Else
                        Rec_Count_BioDataDetails = False
                    End If
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
                Catch ex As Exception
                    ' MessageBox.Show("BioData Photo Not Found")
                End Try
                'Get Visitor Master Details
                Dim VisitorMaster = linq_obj.SP_Get_Enq_VisitorMasterListById(Address_ID).ToList()
                If VisitorMaster.Count > 0 Then
                    Rec_Count_VisitorDetails = True
                Else
                    Rec_Count_VisitorDetails = False
                End If
                For Each item As SP_Get_Enq_VisitorMasterListByIdResult In VisitorMaster
                    txtIncliens.Text = item.Incliness
                    txtReadiness.Text = item.Readiness
                    txtMrkVision.Text = item.Mkt_Vision
                    txtImpPerson.Text = item.ImpPerson
                    txtInflu.Text = item.Influ
                    txtStrength.Text = item.Strength
                    txtSpExp4Final.Text = item.Sp_Expect
                    txtFinStrength.Text = item.Fin_Strength
                    txtMktStrength.Text = item.Mkt_Strength
                    txtAwt4Comp.Text = item.AwtFo
                    txtProduct.Text = item.Product
                    txtLand.Text = item.Land
                    txtPower.Text = item.Power
                    txtContruction.Text = item.Contruction
                Next
                'Get VisitorDetails
                Dim VisitorDetails = linq_obj.SP_Get_Enq_VisitorDetailsListById(Address_ID).ToList()
                tblVisitor.Rows.Clear()

                For Each item As SP_Get_Enq_VisitorDetailsListByIdResult In VisitorDetails
                    Dim dr As DataRow
                    dr = tblVisitor.NewRow()
                    dr("V_Date") = item.V_Date
                    dr("VFNo") = item.VFNo.ToString()
                    dr("V_Status") = item.V_Status
                    dr("SalesExc") = item.SalesExc
                    dr("Remarks") = item.Remarks
                    dr("FollowBy") = item.FollowBy
                    dr("Duration") = item.Duration
                    dr("E_Type") = item.E_Type
                    dr("JBVRemarks") = item.JBVRemarks
                    dr("VisitorID") = item.Pk_VisitorDetailID
                    tblVisitor.Rows.Add(dr)
                Next
                DGVvisitorDetail.DataSource = tblVisitor
                ' DGVvisitorDetail.Columns("VisitorID").Visible = False   'Viraj Rana
                ''Display Last Record of Visitor Details into Fields
                Dim lastRec As Integer
                lastRec = VisitorDetails.Count - 1
                If VisitorDetails.Count > 0 Then
                    dtVDate.Text = VisitorDetails.ToList.Item(lastRec).V_Date.ToString()
                    txtVFNo.Text = VisitorDetails.ToList.Item(lastRec).VFNo.ToString()
                    txtVstatus.Text = VisitorDetails.ToList.Item(lastRec).V_Status
                    txtExecutive.Text = VisitorDetails.ToList.Item(lastRec).SalesExc
                    txtEnqType.Text = VisitorDetails.ToList.Item(lastRec).E_Type
                    txtFollowBy.Text = VisitorDetails.ToList.Item(lastRec).FollowBy
                    txtExeRemarks.Text = VisitorDetails.ToList.Item(lastRec).Remarks
                    txtduration.Text = VisitorDetails.ToList.Item(lastRec).Duration
                    txtJVBRemarks.Text = VisitorDetails.ToList.Item(lastRec).JBVRemarks
                End If

                'client basic detail bind


                Dim Claient = linq_obj.SP_Get_AddressListById_New(Address_ID).ToList()
                For Each item As SP_Get_AddressListById_NewResult In Claient
                    txtPartyName.Text = item.Name

                    txtaddress.Text = item.Address
                    txtstation.Text = item.Area
                    txtstate.Text = item.BillState
                    txtdistrict.Text = item.District
                    txtCity.Text = item.City
                    txtPincode.Text = item.Pincode
                    txtTaluka.Text = item.Taluka
                    txtDelAddress.Text = item.DeliveryAddress
                    txtDelStation.Text = item.DeliveryArea
                    txtDelState.Text = item.DeliveryState
                    txtDelDistrict.Text = item.DeliveryDistrict
                    txtDelCity.Text = item.DeliveryCity
                    txtDelPincode.Text = item.DeliveryPincode
                    txtDelTaluka.Text = item.DeliveryTaluka

                    txtcoperson.Text = item.ContactPerson
                    txtphoneNo.Text = item.LandlineNo
                    txtmobileNo.Text = item.MobileNo
                    txtEmailID.Text = item.EmailID
                    txtEntryNo.Text = item.EnqNo
                    txtRef.Text = item.Reference1
                    txtRef2.Text = item.Reference2
                    txtWtypeofEnq.Text = item.TypeOfEnq
                    txtW_EnqAtta.Text = item.EnqAttendBy
                    txtenquiryfor.Text = item.EnqFor
                    'txtF_ProdModel.Text = item.EnqFor
                    txtPerHr.Text = item.CapacityHour
                    txtcustType.Text = item.EnqType

                    txtvalue1.Text = item.EmailID1
                    txtValue2.Text = item.EmailID2
                    txtW_Remark.Text = item.Remark

                    'navin 24-04-2014

                    'dtF_OfferDate.Value = item.EnqDate change navin 17-02-2016
                    'dtEnqTime.Value = item.CreateDate
                    dtEnqDate.Value = item.EnqDate
                    ' dtF_OfferTime.Value = item.CreateDate
                    txtEntryDate.Value = item.EnqDate

                    txtEnqStatusEnqDate.Text = Convert.ToDateTime(item.EnqDate).ToString("dd/MM/yyyy")


                Next
                'Get Enquiry Client Master Details
                Dim allotDetail = linq_obj.SP_Get_AllotedUser(Address_ID).ToList()
                If (allotDetail.Count > 0) Then
                    txtW_EnqAllocated.Text = Convert.ToString(allotDetail(0).EnqAllotedTo)
                End If
                Try

                    Dim EnquiryClientMaster = linq_obj.SP_Get_Enq_ClientMasterListByID(Address_ID).ToList()

                    If EnquiryClientMaster.Count > 0 Then
                        Rec_Count_ClientMaster = True
                    Else
                        Rec_Count_ClientMaster = False
                    End If
                    For Each item As SP_Get_Enq_ClientMasterListByIDResult In EnquiryClientMaster
                        Dim imageClientData As Byte() = IIf(item.ClientPhoto = Nothing, Nothing, item.ClientPhoto.ToArray())
                        If imageClientData.Length > 0 Then
                            msPicClient = New MemoryStream(imageClientData, 0, imageClientData.Length)
                            msPicClient.Write(imageClientData, 0, imageClientData.Length)
                            PicClaintPhoto.Image = Image.FromStream(msPicClient, True)
                            PicClaintPhoto.SizeMode = PictureBoxSizeMode.StretchImage
                        End If
                    Next
                Catch ex As Exception
                    MessageBox.Show("Photo Not Found")
                End Try

                ''Enq Marketing Data


                Dim dataMarketing = linq_obj.SP_Get_Enq_Followp_Marketing(Address_ID).ToList()

                For Each item As SP_Get_Enq_Followp_MarketingResult In dataMarketing

                    txtEnqR1.Text = item.Enq_R1
                    txtEnqR2.Text = item.Enq_R2
                    txtEnqB1.Text = item.Enq_B1
                    txtEnqB2.Text = item.Enq_B2
                    txtEnqF1.Text = item.Enq_F1
                    txtEnqF2.Text = item.Enq_F2
                    txtEnqPI1.Text = item.Enq_PI1
                    txtEnqPI2.Text = item.Enq_PI2
                    txtEnqLP1.Text = item.Enq_LP1
                    txtEnqLP2.Text = item.Enq_LP2

                Next

            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try
        End If
    End Sub
    Private Sub btnClientSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ms1 As New MemoryStream
        Dim Pic1ClientData As Byte()
        Pic1ClientData = Nothing
        If Not PicClaintPhoto.Image Is Nothing Then
            PicClaintPhoto.Image.Save(ms1, PicClaintPhoto.Image.RawFormat)
            Pic1ClientData = ms1.GetBuffer()
        End If
        Dim Type As Integer
        If Rec_Count_ClientMaster = True Then
            Type = 1
        Else
            Type = 0
        End If
        Try
            ' insert into Client Master
            linq_obj.SP_Insert_Update_Enq_ClientMaster(Type, Address_ID, Pic1ClientData, txtEntryDate.Text)
            linq_obj.SubmitChanges()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnEnqDetailSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnWaterSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnFollowpSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Insert into Followp Master

            'linq_obj.SP_insert_Update_Enq_FollowMaster(0, Address_ID, txtF_offerNo.Text, dtFD_Date.Value.Date.ToString(), txtF_ProdModel.Text, txtF_OfferTime.Text, txtF_CourierBy.Text, txtF_CommValue.Text)
            'linq_obj.SubmitChanges()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnfolloupSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnsaveFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsaveFollowup.Click
        ''add new row runtime and display into grid. it will save after click on save button 
        Dim Status As Boolean
        Status = True
        If (btnsaveFollowup.Text = "Save" AndAlso Address_ID > 0) Then

            If (txtFD_EnqType.Text = "REGRET") Then
                If ddlEnqOrderStatus.SelectedValue = 0 Then
                    Status = False
                    MessageBox.Show("Please Select Reason..")
                Else
                    linq_obj.SP_Insert_Update_Enq_Order_Status_Master_Followp(0, Address_ID, Convert.ToInt64(ddlEnqOrderStatus.SelectedValue), Class1.global.UserID, dtFD_Date.Value.Date, txtFD_Remarks.Text.Trim())
                    linq_obj.SubmitChanges()
                End If
            End If


            If Status = True Then
                If (txtFD_follow1.Text <> "") Then
                    Dim dr As DataRow
                    dr = tblFollowup.NewRow()
                    dr("F_Date") = dtFD_Date.Value.Date
                    dr("Followup") = txtFD_follow1.Text
                    dr("N_F_FollowpDate") = dtFD_NFUpdate.Value.Date
                    dr("Status") = txtFD_status.Text
                    dr("ByWhom") = txtFD_BWHOM.Text
                    dr("EnqType") = txtFD_EnqType.Text
                    dr("Remarks") = txtFD_Remarks.Text
                    tblFollowup.Rows.Add(dr)
                    GRVFDData.DataSource = tblFollowup
                    linq_obj.SP_insert_Update_Enq_FollowDetails(Address_ID, dtFD_Date.Value.Date, txtFD_follow1.Text,
                    dtFD_NFUpdate.Value.Date,
                    txtFD_status.Text,
                    txtFD_BWHOM.Text,
                    txtFD_EnqType.Text,
                    txtFD_Remarks.Text)
                    linq_obj.SubmitChanges()

                    linq_obj.SP_UpdateAddressStatus(txtFD_EnqType.Text.Trim(), Address_ID)

                    txtWtypeofEnq.Text = txtFD_EnqType.Text
                    If (txtFD_EnqType.Text = "OC" Or txtFD_EnqType.Text = "OL" Or txtFD_EnqType.Text = "REGRET" Or txtFD_EnqType.Text = "POSTPOND") Then
                        'create a Entry into the orderOne MasterFor Order Data....
                        Dim resorder As Integer

                        If (txtFD_EnqType.Text = "OC") Then
                            resorder = linq_obj.SP_Insert_Tbl_OrderOneMaster("", DateTime.Now.Date, "", txtF_offerNo.Text, dtFD_Date.Value, "01-01-1900 00:00:00", txtPartyName.Text, "", Address_ID, "", txtFD_BWHOM.Text, "", "01-01-1900 00:00:00")
                            If (resorder >= 0) Then
                                linq_obj.SubmitChanges()
                            End If
                        End If

                        GvInEnq_Bind()
                        cleanAll()
                    Else
                        dtFD_Date.Value = DateTime.Now
                        txtFD_follow2.Text = ""
                        txtFD_follow1.Text = ""
                        dtFD_NFUpdate.Value = DateTime.Now
                        txtFD_status.Text = ""
                        txtFD_BWHOM.Text = ""
                        txtFD_EnqType.Text = ""
                        txtFD_Remarks.Text = ""
                    End If


                End If
            End If
        Else


            tblFollowup.Rows(currentRow).Delete()
            GRVFDData.DataSource = tblFollowup
            If (txtFD_follow1.Text <> "") Then
                Dim dr As DataRow
                dr = tblFollowup.NewRow()
                dr("F_Date") = dtFD_Date.Value.Date
                dr("Followup") = txtFD_follow1.Text
                dr("N_F_FollowpDate") = dtFD_NFUpdate.Value.Date
                dr("Status") = txtFD_status.Text
                dr("ByWhom") = txtFD_BWHOM.Text
                dr("EnqType") = txtFD_EnqType.Text
                dr("Remarks") = txtFD_Remarks.Text

                tblFollowup.Rows.InsertAt(dr, currentRow)
                GRVFDData.DataSource = tblFollowup
                'delete previous record from Followup Details then we add updated/modified records
                linq_obj.SP_Delete_Enq_FollowDetails(Address_ID)
                'insert into Followup Details
                For i As Integer = 0 To tblFollowup.Rows.Count - 1
                    linq_obj.SP_insert_Update_Enq_FollowDetails(Address_ID,
                                                                tblFollowup.Rows(i)("F_Date").ToString(),
                                                                tblFollowup.Rows(i)("Followup").ToString(),
                                                                tblFollowup.Rows(i)("N_F_FollowpDate").ToString(),
                                                                tblFollowup.Rows(i)("Status").ToString(),
                                                                tblFollowup.Rows(i)("ByWhom").ToString(),
                                                                tblFollowup.Rows(i)("EnqType").ToString(),
                                                                tblFollowup.Rows(i)("Remarks").ToString()
                                                                )

                Next
                linq_obj.SubmitChanges()

                linq_obj.SP_UpdateAddressStatus(txtFD_EnqType.Text.Trim(), Address_ID)
                currentRow = 0

                If (txtFD_EnqType.Text = "OC" Or txtFD_EnqType.Text = "OL" Or txtFD_EnqType.Text = "REGRET" Or txtFD_EnqType.Text = "POSTPOND") Then
                    GvInEnq_Bind()
                    cleanAll()
                Else
                    dtFD_Date.Value = Date.Now
                    txtFD_follow2.Text = ""
                    txtFD_follow1.Text = ""
                    dtFD_NFUpdate.Value = Date.Now
                    txtFD_status.Text = ""
                    txtFD_BWHOM.Text = ""
                    txtFD_EnqType.Text = ""
                    txtFD_Remarks.Text = ""
                    btnsaveFollowup.Text = "Save"
                End If
            End If


        End If
        btnAddFollowup.Focus()




    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click

        Dim result As DialogResult = MessageBox.Show("Are You Sure?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then

            tblFollowup.Rows(GRVFDData.CurrentRow.Index).Delete()
            GRVFDData.DataSource = tblFollowup

            'delete previous record from Followup Details then we add updated/modified records
            linq_obj.SP_Delete_Enq_FollowDetails(Address_ID)
            'insert into Followup Details
            For i As Integer = 0 To tblFollowup.Rows.Count - 1
                linq_obj.SP_insert_Update_Enq_FollowDetails(Address_ID,
                                                            tblFollowup.Rows(i)("F_Date").ToString(),
                                                            tblFollowup.Rows(i)("Followup").ToString(),
                                                            tblFollowup.Rows(i)("N_F_FollowpDate").ToString(),
                                                            tblFollowup.Rows(i)("Status").ToString(),
                                                            tblFollowup.Rows(i)("ByWhom").ToString(),
                                                            tblFollowup.Rows(i)("EnqType").ToString(),
                                                            tblFollowup.Rows(i)("Remarks").ToString()
                                                            )

            Next
            linq_obj.SubmitChanges()

        End If

    End Sub
    Private Sub Set_Next_Followup_Date()
        Try


            If txtFD_follow2.Text <> "" Then
                dtFD_NFUpdate.Value = dtFD_Date.Value.Date.AddDays(txtFD_follow2.Text)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtFD_follow2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFD_follow2.TextChanged
        Set_Next_Followup_Date()

    End Sub
    Private Sub dtFD_Date_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFD_Date.ValueChanged
        Set_Next_Followup_Date()
    End Sub

    'Private Sub InquiryForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = 13 Then
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub
    Private Sub InquiryForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '  oForm = Nothing
        ''all flag set false
        Rec_Count_ClientMaster = False
        Rec_Count_BioDataDetails = False
        Rec_Count_EnqMaster = False
        Rec_Count_FollowDetails = False
        Rec_Count_FollowMaster = False
        Rec_Count_VisitorDetails = False
        Rec_Count_WaterMaster = False



        ''add column into tblfolloup table first time  and assign to it datagrid and give proper name to it.
        tblFollowup.Columns.Add("F_Date")
        tblFollowup.Columns.Add("Followup")
        tblFollowup.Columns.Add("N_F_FollowpDate")
        tblFollowup.Columns.Add("Status")
        tblFollowup.Columns.Add("ByWhom")
        tblFollowup.Columns.Add("EnqType")
        tblFollowup.Columns.Add("Remarks")
        GRVFDData.DataSource = tblFollowup
        GRVFDData.Columns("F_Date").HeaderText = "Folloup Date"
        GRVFDData.Columns("Followup").HeaderText = "Follow up"
        GRVFDData.Columns("N_F_FollowpDate").HeaderText = "Next Followup Date"
        GRVFDData.Columns("Status").HeaderText = "Status"
        GRVFDData.Columns("ByWhom").HeaderText = "By Whom"
        GRVFDData.Columns("EnqType").HeaderText = "Enquiry Type"
        GRVFDData.Columns("Remarks").HeaderText = "Remarks"

        ''add column into tblvisitor table first time  and assign to it datagrid and give proper name to it.

        tblVisitor.Columns.Add("V_Date")
        tblVisitor.Columns.Add("VFNo")
        tblVisitor.Columns.Add("V_Status")
        tblVisitor.Columns.Add("SalesExc")
        tblVisitor.Columns.Add("Remarks")
        tblVisitor.Columns.Add("FollowBy")
        tblVisitor.Columns.Add("Duration")
        tblVisitor.Columns.Add("E_Type")
        tblVisitor.Columns.Add("JBVRemarks")
        tblVisitor.Columns.Add("VisitorID")


        DGVvisitorDetail.DataSource = tblVisitor
        DGVvisitorDetail.Columns("V_Date").HeaderText = "Visitor Date"
        DGVvisitorDetail.Columns("VFNo").HeaderText = "V.F. No."
        DGVvisitorDetail.Columns("V_Status").HeaderText = "Visitor Status"
        DGVvisitorDetail.Columns("SalesExc").HeaderText = "Sales Executive"
        DGVvisitorDetail.Columns("Remarks").HeaderText = "Remarks"
        DGVvisitorDetail.Columns("FollowBy").HeaderText = "Follow By"
        DGVvisitorDetail.Columns("Duration").HeaderText = "Duration"
        DGVvisitorDetail.Columns("E_Type").HeaderText = "Enq Type"
        DGVvisitorDetail.Columns("VisitorID").HeaderText = "Enq Type"



        cleanAll()
        'GvInEnq_Bind()
        GetClientDetails_Bind()
        bindAllDocumentData()
        'If (Class1.IDAddress > 0 And Class1.Flag = 1) Then
        '    cleanAll()
        '    Address_ID = Class1.IDAddress
        '    GetClientDetails_Bind()
        '    bindAllDocumentData()
        '    Class1.Flag = 0
        'Else
        '    Class1.Flag = 0
        '    MDIMainForm.Show()

        '    ListFolloUps.MdiParent = MDIMainForm
        '    ListFolloUps.MaximizeBox = False
        '    ListFolloUps.MinimizeBox = False
        '    ListFolloUps.StartPosition = FormStartPosition.CenterScreen
        '    '  _frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
        '    GetClientDetails_Bind()
        '    bindAllDocumentData()

        '    ListFolloUps.Show()
        '    ListFolloUps.Focus()


        'End If

        'set a rights and role wise view...
        getPageRight()

        For Each control As Control In Me.Controls
            ' The textbox control.
            Dim parentControl As New Control


            If (control.GetType() Is GetType(GroupBox)) Then
                Dim grpBox As GroupBox = TryCast(control, GroupBox)
                parentControl = grpBox
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
                                If (subcontrolthree.GetType() Is GetType(Panel)) Then
                                    For Each subcontrolFour As Control In subcontrolthree.Controls
                                        If (subcontrolFour.GetType() Is GetType(TextBox)) Then
                                            Dim textBox As TextBox = TryCast(subcontrolFour, TextBox)

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

    Private Sub TextBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)

        txt.BackColor = Color.White
    End Sub

    Public Sub getPageRight()
        Try
            Dim dv As New DataTable
            Dim dataView As DataView

            Dim Class1 As New Class1
            Dim RowCount As Integer
            Dim statusCheck As Boolean = False
            Dim TabIndexNo As Integer
            Dim strName As String = ""
            Dim TabCountValue As Integer
            dataView = Class1.global.UserPermissionDataset.Tables(0).DefaultView
            dataView.RowFilter = "([Name] like 'Inquiry Manager')"
            dv = dataView.ToTable()
            TabCountValue = TabControl1.TabCount - 1
            If (dv.Rows(0)("Type") = 1) Then
            Else
                Dim indexTest As Integer = 0
                While indexTest <= TabCountValue
                    statusCheck = False
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("DetailName").ToString().ToUpper().Trim() = TabControl1.TabPages(indexTest).Text.ToUpper()) Then
                            statusCheck = True
                            TabIndexNo = indexTest
                            Exit For
                        End If
                    Next
                    If statusCheck = False Then
                        TabControl1.TabPages.RemoveAt(indexTest)
                        indexTest = -1
                        TabCountValue = TabCountValue - 1
                    End If
                    indexTest += 1
                End While
            End If

            For RowCount = 0 To dv.Rows.Count - 1
                If (dv.Rows(RowCount)("IsAdd") = True) Then
                    btnSave.Enabled = True
                Else
                    btnSave.Enabled = False
                End If
                If (btnsaveFollowup.Text = "Update") Then
                    If (dv.Rows(RowCount)("IsUpdate") = True) Then
                        btnsaveFollowup.Enabled = True
                        btnSave.Enabled = True
                    Else
                        btnsaveFollowup.Enabled = False
                        btnSave.Enabled = False
                    End If
                End If
                If (dv.Rows(RowCount)("IsDelete") = True) Then
                    btndelete.Enabled = True
                    btnDeleteInquiry.Enabled = True
                Else
                    btndelete.Enabled = False
                    btnDeleteInquiry.Enabled = False
                End If
                If (dv.Rows(RowCount)("IsPrint") = True) Then
                    btnPrintDoc.Enabled = True

                    btnVisitFollowUp.Enabled = True
                    btnVisitorDetails.Enabled = True
                    btnVisitSummary.Enabled = True
                    btnRptFolloupSheet.Enabled = True
                    btnRptEnqformat.Enabled = True
                Else
                    btnPrintDoc.Enabled = False
                    btnVisitFollowUp.Enabled = False
                    btnVisitorDetails.Enabled = False
                    btnVisitSummary.Enabled = False
                    btnRptFolloupSheet.Enabled = False
                    btnRptEnqformat.Enabled = False

                End If

            Next


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnBrowse1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse1.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Pic1.Image = Image.FromFile(OpenFileDialog1.FileName)
            Pic1.SizeMode = PictureBoxSizeMode.StretchImage
        End If

    End Sub

    Private Sub btnBrowse2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse2.Click
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ''add new row runtime and display into grid. it will save after click on save button

        Dim flag As Integer 'add navin 06-07-2017 s
        flag = 0

        If txtPVstatus.Text.Trim() = "AN TIME" Or txtPVstatus.Text.Trim() = "EOD TIME" Then
            Dim numericCheck As Boolean
            ' The following call to IsNumeric returns True.
            numericCheck = IsNumeric(txtPDuration.Text.Trim())
            If numericCheck = True Then
                flag = 1
            Else
                flag = 0

            End If
        Else
            flag = 1
        End If


        If flag = 1 Then

            If (btnSave.Text = "Save" AndAlso Address_ID > 0) Then
                If txtPVstatus.Text <> "" Then
                    Dim dr As DataRow
                    dr = tblVisitor.NewRow()
                    dr("V_Date") = dtPVisitDate.Value.Date
                    dr("VFNo") = txtDetailVFNo.Text
                    dr("V_Status") = txtPVstatus.Text
                    dr("SalesExc") = txtPSExec.Text
                    dr("Remarks") = txtPViRemarks.Text
                    dr("FollowBy") = txtPFollowBy.Text
                    dr("Duration") = txtPDuration.Text.Trim()
                    dr("E_Type") = txtPEtype.Text
                    dr("JBVRemarks") = txtpjbvremarks.Text


                    tblVisitor.Rows.Add(dr)
                    DGVvisitorDetail.DataSource = tblVisitor

                    linq_obj.SP_insert_Update_Enq_VisitorDetails(Address_ID, dtPVisitDate.Value.Date, txtDetailVFNo.Text, txtPVstatus.Text, txtPSExec.Text, txtPViRemarks.Text, txtPFollowBy.Text, txtPDuration.Text.Trim(), txtPEtype.Text, txtpjbvremarks.Text)
                    linq_obj.SubmitChanges()

                    txtWtypeofEnq.Text = txtPEtype.Text
                    txtVstatus.Text = txtPVstatus.Text

                    txtExeRemarks.Text = txtPViRemarks.Text

                    txtduration.Text = txtPDuration.Text.Trim()
                    txtEnqType.Text = txtPEtype.Text
                    txtJVBRemarks.Text = txtpjbvremarks.Text
                    dtVDate.Value = dtPVisitDate.Value
                    txtExecutive.Text = txtPSExec.Text
                    txtFollowBy.Text = txtPFollowBy.Text
                    txtVFNo.Text = txtDetailVFNo.Text
                    linq_obj.SP_UpdateAddressStatus(txtPEtype.Text.Trim(), Address_ID)


                    If (txtPEtype.Text = "OL" Or txtPEtype.Text = "REGRET" Or txtPEtype.Text = "POSTPOND" Or txtPEtype.Text = "OC") Then
                        GvInEnq_Bind()
                        cleanAll()
                    Else

                        'insert into a  second allotment User

                        If (DGVvisitorDetail.RowCount > 1) Then
                            Dim res As Integer = linq_obj.SP_Tbl_SecondAllotmentDetail_Insert(Address_ID, 27)
                            If (res > 0) Then
                                linq_obj.SubmitChanges()
                            End If
                        Else
                            Dim res As Integer = linq_obj.SP_Tbl_SecondAllotmentDetail_Delete(Address_ID, 27)
                            If (res >= 0) Then
                                linq_obj.SubmitChanges()
                            End If
                        End If
                    End If
                    txtPVstatus.Text = ""
                    txtPSExec.Text = ""
                    txtPViRemarks.Text = ""
                    txtPFollowBy.Text = ""
                    txtPDuration.Text = ""
                    txtPEtype.Text = ""
                    txtpjbvremarks.Text = ""
                    dtPVisitDate.Value = DateTime.Now

                    txtDetailVFNo.Text = ""







                End If
            Else

                If txtPVstatus.Text <> "" Then
                    tblVisitor.Rows(currentRowVisitor).Delete()
                    DGVvisitorDetail.DataSource = tblVisitor
                    Dim dr As DataRow
                    dr = tblVisitor.NewRow()
                    dr("V_Date") = dtPVisitDate.Value.Date
                    dr("VFNo") = txtDetailVFNo.Text
                    dr("V_Status") = txtPVstatus.Text
                    dr("SalesExc") = txtPSExec.Text
                    dr("Remarks") = txtPViRemarks.Text
                    dr("FollowBy") = txtPFollowBy.Text
                    dr("Duration") = txtPDuration.Text.Trim()
                    dr("E_Type") = txtPEtype.Text
                    dr("JBVRemarks") = txtpjbvremarks.Text
                    tblVisitor.Rows.InsertAt(dr, currentRowVisitor)
                    DGVvisitorDetail.DataSource = tblVisitor

                    'delete a visitor detail

                    linq_obj.SP_Delete_Enq_VisitorDetails(Address_ID)
                    'insert into Followup Details
                    For i As Integer = 0 To tblVisitor.Rows.Count - 1
                        linq_obj.SP_insert_Update_Enq_VisitorDetails(Address_ID, Convert.ToDateTime(tblVisitor.Rows(i)("V_Date")), tblVisitor.Rows(i)("VFNo"), tblVisitor.Rows(i)("V_Status"), tblVisitor.Rows(i)("SalesExc"), tblVisitor.Rows(i)("Remarks"), tblVisitor.Rows(i)("FollowBy"), tblVisitor.Rows(i)("Duration"), tblVisitor.Rows(i)("E_Type"), tblVisitor.Rows(i)("JBVRemarks"))
                        linq_obj.SubmitChanges()
                    Next
                    txtWtypeofEnq.Text = txtPEtype.Text
                    txtVstatus.Text = txtPVstatus.Text
                    txtVFNo.Text = ""
                    txtExeRemarks.Text = txtPViRemarks.Text
                    txtduration.Text = txtPDuration.Text.Trim()
                    txtEnqType.Text = txtPEtype.Text
                    txtJVBRemarks.Text = txtpjbvremarks.Text
                    dtVDate.Value = dtPVisitDate.Value
                    txtExecutive.Text = txtPSExec.Text
                    txtFollowBy.Text = txtPFollowBy.Text
                    txtVFNo.Text = txtDetailVFNo.Text
                    linq_obj.SP_UpdateAddressStatus(txtPEtype.Text.Trim(), Address_ID)
                    If (txtPEtype.Text = "OB" Or txtPEtype.Text = "OC") Then
                        GvInEnq_Bind()
                        cleanAll()
                    Else
                        txtDetailVFNo.Text = ""
                        txtPVstatus.Text = ""
                        txtPSExec.Text = ""
                        txtPViRemarks.Text = ""
                        txtPFollowBy.Text = ""
                        txtPDuration.Text = ""
                        txtPEtype.Text = ""
                        txtpjbvremarks.Text = ""
                        currentRowVisitor = 0
                        btnSave.Text = "Save"
                    End If
                End If

            End If

            DGVvisitorDetail.DataSource = Nothing
            DGVvisitorDetail.Rows.Clear()
            Dim VisitorDetails = linq_obj.SP_Get_Enq_VisitorDetailsList.Where(Function(t) t.FK_AddressID = Address_ID).ToList()
            tblVisitor.Rows.Clear()
            For Each item As SP_Get_Enq_VisitorDetailsListResult In VisitorDetails
                Dim dr1 As DataRow
                dr1 = tblVisitor.NewRow()
                dr1("V_Date") = item.V_Date.ToString()
                dr1("VFNo") = item.VFNo
                dr1("V_Status") = item.V_Status
                dr1("SalesExc") = item.SalesExc.ToString()
                dr1("Remarks") = item.Remarks
                dr1("FollowBy") = item.FollowBy
                dr1("Duration") = item.Duration
                dr1("E_Type") = item.E_Type
                dr1("JBVRemarks") = item.JBVRemarks
                dr1("VisitorID") = item.Pk_VisitorDetailID
                tblVisitor.Rows.Add(dr1)
            Next
            btnAddVisitor.Focus()
            DGVvisitorDetail.DataSource = tblVisitor
            DGVvisitorDetail.Columns("VisitorID").Visible = False
        Else

            If txtPVstatus.Text.Trim() = "AN TIME" Or txtPVstatus.Text.Trim() = "EOD TIME" Then
                MessageBox.Show("Invalid Duration...")
            End If
        End If

    End Sub


    Private Sub btnPDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDelete.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            tblVisitor.Rows(DGVvisitorDetail.CurrentRow.Index).Delete()
            DGVvisitorDetail.DataSource = tblVisitor
            'insert into a  second allotment User

            If (DGVvisitorDetail.RowCount > 1) Then
                'Dim res As Integer = linq_obj.SP_Tbl_SecondAllotmentDetail_Insert(Address_ID, 27)
                'If (res > 0) Then
                '    linq_obj.SubmitChanges()
                'End If
            Else
                Dim res As Integer = linq_obj.SP_Tbl_SecondAllotmentDetail_Delete(Address_ID, 27)
                If (res >= 0) Then
                    linq_obj.SubmitChanges()
                End If
            End If
            linq_obj.SP_Delete_Enq_VisitorDetails(Address_ID)
            'insert into Visitor Details
            For i As Integer = 0 To tblVisitor.Rows.Count - 1
                linq_obj.SP_insert_Update_Enq_VisitorDetails(Address_ID, tblVisitor.Rows(i)("V_Date").ToString(), tblVisitor.Rows(i)("VFNo").ToString(), tblVisitor.Rows(i)("V_Status").ToString(), tblVisitor.Rows(i)("SalesExc").ToString(), tblVisitor.Rows(i)("Remarks").ToString(), tblVisitor.Rows(i)("FollowBy").ToString(), tblVisitor.Rows(i)("Duration").ToString(), tblVisitor.Rows(i)("E_type").ToString(), tblVisitor.Rows(i)("JBVRemarks").ToString())
            Next
            linq_obj.SubmitChanges()
        End If
    End Sub

    Private Sub btnRptEnqformat_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRptEnqformat.Click
        If Address_ID > 0 Then

            Dim Enq_EnqMaster As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Get_Rpt_Enquiry_Formate"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(Enq_EnqMaster, "Enq_EnqMaster")

            Class1.WriteXMlFile(Enq_EnqMaster, "Get_Rpt_Enquiry_Formate", "Enq_EnqMaster")

            Dim rpt As New rptEnqFormat

            rpt.Database.Tables(0).SetDataSource(Enq_EnqMaster.Tables("Enq_EnqMaster"))


            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()
        Else
            MessageBox.Show("Data Not Found...")
        End If

    End Sub

    Private Sub btnRptFolloupSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRptFolloupSheet.Click
        If Address_ID > 0 Then
            'Dim ds As New EnqFormat
            Dim Enq_EnqMaster As New DataSet  '  ds.Enq_FolloupMaster

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Get_Rpt_FolloupSheet_New"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(Enq_EnqMaster, "Enq_FolloupMaster")
            Class1.WriteXMlFile(Enq_EnqMaster, "Get_Rpt_FolloupSheet", "Enq_FolloupMaster")

            Dim rpt As New RptFolloupSheet
            rpt.SetDataSource(Enq_EnqMaster)

            Dim frm As New FrmCommanReportView(rpt)
            frm.Show()
        Else
            MessageBox.Show("Data Not Found...")
        End If
    End Sub

    Private Sub btnbrowse4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowse4.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Pic4.Image = Image.FromFile(OpenFileDialog1.FileName)
            Pic4.SizeMode = PictureBoxSizeMode.StretchImage
        End If

    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PicClaintPhoto.Image = Image.FromFile(OpenFileDialog1.FileName)
            PicClaintPhoto.SizeMode = PictureBoxSizeMode.StretchImage
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInquirySearch.Click
        bindEnqGrid()
        txtTotEnquiry.Text = GvInquery.RowCount.ToString()

        'txtInqSearchCOperson.Text = ""
        'txtInqSearchEmailID.Text = ""
        'txtInqSearchMobileNo.Text = ""
        'txtInqSearchOfferNo.Text = ""
        'txtInqSearchParty.Text = ""
        'txtInqSearchStation.Text = ""

    End Sub

    Public Sub bindEnqGrid()

        Dim criteria As String
        criteria = " and "

        If txtInqSearchParty.Text.Trim() <> "" Then
            criteria = criteria + " Name like '%" + txtInqSearchParty.Text + "%' and"
        End If
        If txtInqSearchCOperson.Text.Trim() <> "" Then
            criteria = criteria + " ContactPerson like '%" + txtInqSearchCOperson.Text + "%' and"
        End If
        If txtInqSearchStation.Text.Trim <> "" Then
            criteria = criteria + " City like '%" + txtInqSearchStation.Text + "%' and"
        End If
        'If txtInqSearchLandLineNo.Text.Trim() <> "" Then
        '    criteria = criteria + " LandlineNo like '%" + txtInqSearchLandLineNo.Text + "%' and"
        'End If
        If txtInqSearchMobileNo.Text.Trim() <> "" Then
            criteria = criteria + " MobileNo like '%" + txtInqSearchMobileNo.Text + "%' and"
        End If
        If txtInqSearchEmailID.Text.Trim() <> "" Then
            criteria = criteria + " EmailID like '%" + txtInqSearchEmailID.Text + "%' and"
        End If
        If txtInqSearchOfferNo.Text.Trim() <> "" Then
            criteria = criteria + " EnqNo like '%" + txtInqSearchOfferNo.Text + "%' and"
        End If

        If criteria = " and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If

        Dim cmd As New SqlCommand

        If (Class1.global.Designation.ToUpper() = "EXE. ASS. TO MD") Then
            cmd.CommandText = "SP_Search_InquiryForSalesExec"
        ElseIf (chkAllStatus.Checked = True) Then
            cmd.CommandText = "SP_Get_AddressListByUser"
        Else
            cmd.CommandText = "SP_Search_Inquiry"
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
        cmd.CommandTimeout = 3000

        Dim objclass As New Class1

        Dim ds As New DataSet

        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvInquery.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            GvInquery.DataSource = ds.Tables(1)
            For index = 0 To GvInquery.RowCount - 1
                If (GvInquery.Rows(index).Cells(3).Value > 0) Then
                    GvInquery.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.DarkSlateGray
                    GvInquery.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.Yellow
                End If
            Next
            'GvInquery.DataSource = Nothing
            'GvInquery.DataSource = ds.Tables(1)
            GvInquery.Columns(0).Visible = False
            GvInquery.Columns(3).Visible = False

        End If
    End Sub

    Private Sub btnClientBioDataSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClientBioDataSubmit.Click
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

        Dim type As Integer
        If Rec_Count_BioDataDetails = True Then
            type = 1
        Else
            type = 0
        End If

        Try
            Dim ClientBioData = linq_obj.SP_insert_Update_Enq_ClientBioData(type,
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

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        linq_obj.SubmitChanges()


    End Sub

    Public Sub visitorSave()
        '' Add/Update Data into Visitor Master
        If Rec_Count_VisitorDetails = True Then
            linq_obj.SP_Insert_Update_Enq_VisitorMaster(Address_ID,
                                                        Address_ID,
                                                        txtIncliens.Text,
                                                        txtReadiness.Text,
                                                        txtMrkVision.Text,
                                                        txtImpPerson.Text,
                                                        txtInflu.Text,
                                                        txtStrength.Text,
                                                        txtSpExp4Final.Text,
                                                        txtFinStrength.Text,
                                                        txtMktStrength.Text,
                                                        txtAwt4Comp.Text,
                                                        txtProduct.Text,
                                                        txtLand.Text,
                                                        txtPower.Text, txtVFNo.Text, txtContruction.Text)
        Else
            linq_obj.SP_Insert_Update_Enq_VisitorMaster(0,
                                                        Address_ID,
                                                        txtIncliens.Text,
                                                        txtReadiness.Text,
                                                        txtMrkVision.Text,
                                                        txtImpPerson.Text,
                                                        txtInflu.Text,
                                                        txtStrength.Text,
                                                        txtSpExp4Final.Text,
                                                        txtFinStrength.Text,
                                                        txtMktStrength.Text,
                                                        txtAwt4Comp.Text,
                                                        txtProduct.Text,
                                                        txtLand.Text,
                                                        txtPower.Text, txtVFNo.Text, txtContruction.Text)
        End If
        ''Add Data into Enq_VisitorDetail 
        'First of All ,delete previous record from Visitor Details then we add updated/modified records
        'linq_obj.SP_Delete_Enq_VisitorDetails(Address_ID)
        ''insert into Visitor Details
        'For i As Integer = 0 To tblVisitor.Rows.Count - 1
        '    linq_obj.SP_insert_Update_Enq_VisitorDetails(Address_ID,
        '                                                tblVisitor.Rows(i)("V_Date").ToString(),
        '                                                tblVisitor.Rows(i)("VFNo").ToString(),
        '                                                tblVisitor.Rows(i)("V_Status").ToString(),
        '                                                tblVisitor.Rows(i)("SalesExc").ToString(),
        '                                                tblVisitor.Rows(i)("Remarks").ToString(),
        '                                                tblVisitor.Rows(i)("FollowBy").ToString(),
        '                                                tblVisitor.Rows(i)("Duration").ToString(),
        '                                                tblVisitor.Rows(i)("E_type").ToString(),
        '                                                tblVisitor.Rows(i)("JBVRemarks").ToString()
        '                                                )

        'Next
        'linq_obj.SubmitChanges()



        ' DGVvisitorDetail.Columns("VisitorID").Visible = False   'Viraj Rana
    End Sub

    Public Sub SaveClientDetail()
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

        Dim type As Integer
        If Rec_Count_BioDataDetails = True Then
            type = 1
        Else
            type = 0
        End If
        Try
            Dim ClientBioData = linq_obj.SP_insert_Update_Enq_ClientBioData(type,
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

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        linq_obj.SubmitChanges()

    End Sub

    Private Sub btnVisitorDetailSubmit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisitorDetail.Click

    End Sub

    Private Sub btnfolloupSubmit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfolloupSubmit.Click
        Try

            'insert into Followup Master
            Dim strFolloupTime As String
            ' strFolloupTime = dtF_OfferTime.Value.Hour.ToString() & ":" & dtF_OfferTime.Value.Minute.ToString() & ":" & dtF_OfferTime.Value.Second.ToString()

            If Rec_Count_FollowMaster = True Then
                linq_obj.SP_insert_Update_Enq_FollowMaster(Address_ID, Address_ID, txtF_offerNo.Text, dtF_OfferDate.Text, txtF_ProdModel.Text, dtF_OfferTime.Text, txtF_CourierBy.Text, txtF_CommValue.Text)
            Else
                linq_obj.SP_insert_Update_Enq_FollowMaster(0, Address_ID, txtF_offerNo.Text, dtF_OfferDate.Text, txtF_ProdModel.Text, dtF_OfferTime.Text, txtF_CourierBy.Text, txtF_CommValue.Text)
            End If

            'delete previous record from Followup Details then we add updated/modified records
            linq_obj.SP_Delete_Enq_FollowDetails(Address_ID)
            'insert into Followup Details
            For i As Integer = 0 To tblFollowup.Rows.Count - 1
                linq_obj.SP_insert_Update_Enq_FollowDetails(Address_ID,
                                                            tblFollowup.Rows(i)("F_Date").ToString(),
                                                            tblFollowup.Rows(i)("Followup").ToString(),
                                                            tblFollowup.Rows(i)("N_F_FollowpDate").ToString(),
                                                            tblFollowup.Rows(i)("Status").ToString(),
                                                            tblFollowup.Rows(i)("ByWhom").ToString(),
                                                            tblFollowup.Rows(i)("EnqType").ToString(),
                                                            tblFollowup.Rows(i)("Remarks").ToString()
                                                            )

            Next
            linq_obj.SubmitChanges()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnWaterSubmit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWaterSubmit.Click
        Try
            'insert into Water Master

            linq_obj.SP_UpdateAddressStatus(txtWtypeofEnq.Text.Trim(), Address_ID)
            If Rec_Count_WaterMaster = True Then
                linq_obj.SP_Insert_Update_Enq_WaterMaster(Address_ID, Address_ID, txtApplication.Text, txtWtypeofEnq.Text, txtW_Remark.Text, txtW_EnqAtta.Text, txtW_SalesExc.Text, txtW_EnqAllocated.Text, Convert.ToDateTime(If(dtCommitVisitIn.Text = "", "01-01-1900 00:00:00", dtCommitVisitIn.Text)), Convert.ToDateTime(If(dtCommitVisitOut.Text = "", "01-01-1900 00:00:00", dtCommitVisitOut.Text)), Convert.ToDateTime(If(dtFinalizeBy.Text = "", "01-01-1900 00:00:00", dtFinalizeBy.Text)))
            Else
                linq_obj.SP_Insert_Update_Enq_WaterMaster(0, Address_ID, txtApplication.Text, txtWtypeofEnq.Text, txtW_Remark.Text, txtW_EnqAtta.Text, txtW_SalesExc.Text, txtW_EnqAllocated.Text, Convert.ToDateTime(If(dtCommitVisitIn.Text = "", "01-01-1900 00:00:00", dtCommitVisitIn.Text)), Convert.ToDateTime(If(dtCommitVisitOut.Text = "", "01-01-1900 00:00:00", dtCommitVisitOut.Text)), Convert.ToDateTime(If(dtFinalizeBy.Text = "", "01-01-1900 00:00:00", dtFinalizeBy.Text)))
            End If

            linq_obj.SubmitChanges()
            If (txtWtypeofEnq.Text = "OC") Then
                GvInEnq_Bind()

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnEnqDetailSubmit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnqDetailSubmit.Click
        Try
            'insert into Enq Master
            Dim strEnqTime As String
            strEnqTime = dtEnqTime.Value.Hour.ToString() & ":" & dtEnqTime.Value.Minute.ToString() & ":" & dtEnqTime.Value.Second.ToString()
            Dim TYPE As Integer
            If Rec_Count_EnqMaster = True Then
                TYPE = 1
            Else
                TYPE = 0
            End If

            linq_obj.SP_Insert_Update_Enq_EnqMaster(TYPE,
                                                    Address_ID,
                                                    txtRef.Text,
                                                    dtEnqDate.Value.Date.ToString(),
                                                    strEnqTime,
                                                    txtenquiryfor.Text,
                                                    txtPerHr.Text,
                                                    txtPerDay.Text,
                                                    txtPerReg.Text,
                                                    TXTRTDS.Text,
                                                    TXTTTDS.Text,
                                                    TXTRTH.Text,
                                                    TXTTTH.Text,
                                                    TXTRPH.Text,
                                                    TXTTPH.Text)

            linq_obj.SubmitChanges()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClientSubmit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClientSubmit.Click
        Dim ms1 As New MemoryStream
        Dim Pic1ClientData As Byte()
        Pic1ClientData = Nothing
        If Not PicClaintPhoto.Image Is Nothing Then
            PicClaintPhoto.Image.Save(ms1, PicClaintPhoto.Image.RawFormat)
            Pic1ClientData = ms1.GetBuffer()
        End If
        Dim Type As Integer
        If Rec_Count_ClientMaster = True Then
            Type = 1
        Else
            Type = 0
        End If
        Try
            ' insert into Client Master
            linq_obj.SP_Insert_Update_Enq_ClientMaster(Type, Address_ID, Pic1ClientData, Convert.ToDateTime(txtEntryDate.Text))
            linq_obj.SubmitChanges()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub updateAddressDetails()
        linq_obj.SP_UpdateAddress(txtPartyName.Text, txtaddress.Text, txtstation.Text, txtCity.Text, txtPincode.Text, txtTaluka.Text, txtdistrict.Text, txtstate.Text, txtDelAddress.Text, txtDelStation.Text, txtDelCity.Text, txtDelPincode.Text, txtDelTaluka.Text, txtDelDistrict.Text, txtDelState.Text, Address_ID)
        linq_obj.SP_UpdateAddressContactDetail(txtcoperson.Text, txtmobileNo.Text, txtEmailID.Text, txtvalue1.Text, txtValue2.Text, Address_ID)
        linq_obj.SP_UpdateAddressOtherDetail(txtRef.Text, txtRef2.Text, txtWtypeofEnq.Text, txtenquiryfor.Text, txtPerHr.Text, txtW_Remark.Text, Address_ID)
        linq_obj.SubmitChanges()
    End Sub

    Private Sub btnFinalSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalSubmit.Click
        btnClientSubmit_Click_1(Nothing, Nothing)
        btnEnqDetailSubmit_Click_1(Nothing, Nothing)
        btnWaterSubmit_Click_1(Nothing, Nothing)
        btnfolloupSubmit_Click_1(Nothing, Nothing)
        Call visitorSave()
        Call SaveClientDetail()
        'saveAllDocument()
        updateAddressDetails()
        Enq_Marketing()

        'AutoComplete_Text()
        MessageBox.Show("Successfully Saved...")
        cleanAll()
        Enq_Marketing_Clear()
    End Sub

    Public Sub Enq_Marketing()

        linq_obj.SP_insert_Update_Enq_Followp_Marketing(Address_ID, txtEnqR1.Text, txtEnqR2.Text, txtEnqB1.Text, txtEnqB2.Text, txtEnqF1.Text, txtEnqF2.Text, txtEnqPI1.Text, txtEnqPI2.Text, txtEnqLP1.Text, txtEnqLP2.Text)
        linq_obj.SubmitChanges()


    End Sub
    Public Sub Enq_Marketing_Clear()
        txtEnqR1.Text = ""
        txtEnqR2.Text = ""
        txtEnqB1.Text = ""
        txtEnqB2.Text = ""
        txtEnqF1.Text = ""
        txtEnqF2.Text = ""
        txtEnqPI1.Text = ""
        txtEnqPI2.Text = ""
        txtEnqLP1.Text = ""
        txtEnqLP2.Text = ""
    End Sub
    Private Sub txtcoperson_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcoperson.TextChanged

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub cleanAll()
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
        'client data
        txtvalue1.Text = ""
        txtValue2.Text = ""
        txtEntryDate.Text = ""

        'water detail
        txtApplication.Text = ""
        txtWtypeofEnq.Text = ""
        txtW_Remark.Text = ""
        txtW_EnqAtta.Text = ""
        txtW_SalesExc.Text = ""
        txtW_EnqAllocated.Text = ""


        'enquiry detail
        txtRef.Text = ""
        dtEnqDate.Value = Date.Now

        txtenquiryfor.Text = ""
        txtPerHr.Text = ""
        txtPerDay.Text = ""
        txtPerReg.Text = ""
        TXTRTDS.Text = ""
        TXTTTDS.Text = ""
        TXTRTH.Text = ""
        TXTTTH.Text = ""
        TXTRPH.Text = ""
        TXTTPH.Text = ""

        'detail

        txtF_offerNo.Text = ""
        dtFD_Date.Value = Date.Now
        dtF_OfferDate.Text = ""
        dtF_OfferTime.Text = ""
        txtF_ProdModel.Text = ""
        txtF_CourierBy.Text = ""
        txtF_CommValue.Text = ""
        'visitor

        txtIncliens.Text = ""
        txtReadiness.Text = ""
        txtMrkVision.Text = ""
        txtImpPerson.Text = ""
        txtInflu.Text = ""
        txtStrength.Text = ""
        txtSpExp4Final.Text = ""
        txtFinStrength.Text = ""
        txtMktStrength.Text = ""
        txtAwt4Comp.Text = ""
        txtProduct.Text = ""
        txtLand.Text = ""
        txtPower.Text = ""
        txtExecutive.Text = ""
        txtExeRemarks.Text = ""

        ' Visitor Details
        txtPVstatus.Text = ""
        txtDetailVFNo.Text = ""
        txtVFNo.Text = ""
        txtPSExec.Text = ""
        txtPViRemarks.Text = ""
        txtPFollowBy.Text = ""
        txtPDuration.Text = ""
        txtPEtype.Text = ""
        txtpjbvremarks.Text = ""
        btnSave.Text = "Save"

        GRVFDData.DataSource = Nothing
        DGVvisitorDetail.DataSource = Nothing
        txtPartyName.Text = ""
        txtaddress.Text = ""
        txtstation.Text = ""
        txtstate.Text = ""
        txtdistrict.Text = ""
        txtCity.Text = ""
        txtTaluka.Text = ""
        txtPincode.Text = ""
        txtcoperson.Text = ""
        txtphoneNo.Text = ""
        txtmobileNo.Text = ""
        txtEmailID.Text = ""
        txtEntryNo.Text = ""

        txtvalue1.Text = ""
        txtValue2.Text = ""


        txtDelAddress.Text = ""
        txtDelStation.Text = ""
        txtDelCity.Text = ""
        txtDelPincode.Text = ""
        txtDelTaluka.Text = ""
        txtDelDistrict.Text = ""
        txtDelState.Text = ""


        ' tblFollowup = Nothing
        ' tblVisitor = Nothing

        txtVstatus.Text = ""
        txtSpExp4Final.Text = ""
        txtFollowBy.Text = ""
        txtduration.Text = ""
        txtEnqType.Text = ""
        txtJVBRemarks.Text = ""
        dtPVisitDate.Value = Date.Now
        Address_ID = 0

        Pic1.Image = Nothing
        Pic2.Image = Nothing
        Pic3.Image = Nothing
        Pic4.Image = Nothing

        txtcustType.Text = ""
        dtFD_Date.Value = Date.Now
        txtFD_follow2.Text = ""
        txtFD_follow1.Text = ""
        dtFD_NFUpdate.Value = Date.Now
        txtFD_status.Text = ""
        txtFD_BWHOM.Text = ""
        txtFD_EnqType.Text = ""
        txtFD_Remarks.Text = ""
        btnsaveFollowup.Text = "Save"
        PicClaintPhoto.Image = Nothing
        txtContruction.Text = ""
        dtCommitVisitIn.Text = ""
        dtCommitVisitOut.Text = ""
        dtFinalizeBy.Text = ""

        'datadocument log


        GvdDatadocumentList.DataSource = Nothing

        Marketing_Clear()
        GvEnqStatusLog.DataSource = Nothing


    End Sub

    Private Sub btnCleanAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCleanAll.Click
        cleanAll()
    End Sub

    Private Sub txtenquiryfor_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtenquiryfor.Leave
        'txtF_ProdModel.Text = txtenquiryfor.Text

    End Sub

    Private Sub InquiryForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub txtPartyName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartyName.Leave
        'If (txtPartyName.Text <> "") Then
        '    Dim data = linq_obj.SP_Get_AddressListByName(txtPartyName.Text).ToList()
        '    If (data.Count > 0) Then
        '        Address_ID = data(0).Pk_AddressID
        '        bindAllData(Address_ID)
        '    End If

        'End If
    End Sub

    Private Sub GRVFDData_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GRVFDData.DoubleClick
        Try
            currentRow = GRVFDData.CurrentRow.Index
            txtFD_follow2.Text = ""
            dtFD_Date.Value = GRVFDData.SelectedCells(0).Value
            txtFD_follow1.Text = GRVFDData.SelectedCells(1).Value
            dtFD_NFUpdate.Text = GRVFDData.SelectedCells(2).Value
            txtFD_status.Text = GRVFDData.SelectedCells(3).Value
            txtFD_BWHOM.Text = GRVFDData.SelectedCells(4).Value
            txtFD_EnqType.Text = GRVFDData.SelectedCells(5).Value
            txtFD_Remarks.Text = GRVFDData.SelectedCells(6).Value
            btnsaveFollowup.Text = "Update"
            getPageRight()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnViewFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewFollowup.Click
        Try
            Dim cmd As New SqlCommand
            cmd.CommandText = "SP_Get_FollowDetails"
            cmd.Parameters.AddWithValue("@Start", dtStart.Value)
            cmd.Parameters.AddWithValue("@End", dtEnd.Value)
            cmd.Parameters.AddWithValue("@byUser ", Class1.global.UserID)
            Dim objclass As New Class1

            Dim ds As New DataSet
            ds = objclass.GetSearchData(cmd)
            If ds.Tables(1).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GvInquery.DataSource = Nothing
            Else
                GvInquery.DataSource = ds.Tables(1)

                ''bind Color
                For index = 0 To GvInquery.RowCount - 1
                    If (GvInquery.Rows(index).Cells(3).Value > 0) Then
                        GvInquery.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.DarkSlateGray
                        GvInquery.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.Yellow
                    End If
                Next
                'GvInquery.DataSource = Nothing
                'GvInquery.DataSource = ds.Tables(1)
                GvInquery.Columns(0).Visible = False
                GvInquery.Columns(3).Visible = False
                ds.Dispose()

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)


        End Try

        txtTotEnquiry.Text = GvInquery.RowCount.ToString()
        chkAllStatus.Checked = False
    End Sub

    Private Sub txtFD_follow2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFD_follow2.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If

    End Sub

    Private Sub txtmobileNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmobileNo.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If

    End Sub

    Private Sub rbtAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAll.Click
        'Dim dataAll = linq_obj.SP_Get_All_AddressList().ToList()
        'GvInquery.DataSource = dataAll
        'Me.GvInquery.Columns(0).Visible = False
        'txtTotEnquiry.Text = GvInquery.RowCount.ToString()
        'chkAllStatus.Checked = True

        Dim criteria As String
        criteria = " and "

        If txtInqSearchParty.Text.Trim() <> "" Then
            criteria = criteria + " Name like '%" + txtInqSearchParty.Text + "%' and"
        End If
        If txtInqSearchCOperson.Text.Trim() <> "" Then
            criteria = criteria + " ContactPerson like '%" + txtInqSearchCOperson.Text + "%' and"
        End If
        If txtInqSearchStation.Text.Trim <> "" Then
            criteria = criteria + " City like '%" + txtInqSearchStation.Text + "%' and"
        End If
        'If txtInqSearchLandLineNo.Text.Trim() <> "" Then
        '    criteria = criteria + " LandlineNo like '%" + txtInqSearchLandLineNo.Text + "%' and"
        'End If
        If txtInqSearchMobileNo.Text.Trim() <> "" Then
            criteria = criteria + " MobileNo like '%" + txtInqSearchMobileNo.Text + "%' and"
        End If
        If txtInqSearchEmailID.Text.Trim() <> "" Then
            criteria = criteria + " EmailID like '%" + txtInqSearchEmailID.Text + "%' and"
        End If
        If txtInqSearchOfferNo.Text.Trim() <> "" Then
            criteria = criteria + " EnqNo like '%" + txtInqSearchOfferNo.Text + "%' and"
        End If

        If criteria = " and " Then
            criteria = ""
        End If

        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If

        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_AddressListByUser"
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

        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            GvInquery.DataSource = ds.Tables(1)
            For index = 0 To GvInquery.RowCount - 1
                If (GvInquery.Rows(index).Cells(3).Value > 0) Then
                    GvInquery.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.DarkSlateGray
                    GvInquery.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.Yellow
                End If
            Next
            'GvInquery.DataSource = Nothing
            'GvInquery.DataSource = ds.Tables(1)
            GvInquery.Columns(0).Visible = False
            GvInquery.Columns(3).Visible = False
            ds.Dispose()
        End If
        chkAllStatus.Checked = True
        txtTotEnquiry.Text = GvInquery.RowCount.ToString()


    End Sub

    Private Sub DGVvisitorDetail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVvisitorDetail.DoubleClick

        'save row number for update a values
        Try
            currentRowVisitor = DGVvisitorDetail.CurrentRow.Index
            btnSave.Text = "Update"
            dtPVisitDate.Text = DGVvisitorDetail.Rows(currentRowVisitor).Cells(0).Value
            txtDetailVFNo.Text = DGVvisitorDetail.Rows(currentRowVisitor).Cells(1).Value
            txtPVstatus.Text = DGVvisitorDetail.Rows(currentRowVisitor).Cells(2).Value
            txtPSExec.Text = DGVvisitorDetail.Rows(currentRowVisitor).Cells(3).Value
            txtPViRemarks.Text = DGVvisitorDetail.Rows(currentRowVisitor).Cells(4).Value
            txtPFollowBy.Text = DGVvisitorDetail.Rows(currentRowVisitor).Cells(5).Value
            txtPDuration.Text = DGVvisitorDetail.Rows(currentRowVisitor).Cells(6).Value.ToString().Trim()
            txtPEtype.Text = DGVvisitorDetail.Rows(currentRowVisitor).Cells(7).Value
            txtpjbvremarks.Text = DGVvisitorDetail.Rows(currentRowVisitor).Cells(8).Value
            txtWtypeofEnq.Text = txtPEtype.Text
            txtVstatus.Text = txtPVstatus.Text
            txtExeRemarks.Text = txtPViRemarks.Text
            txtduration.Text = txtPDuration.Text.Trim()
            txtEnqType.Text = txtPEtype.Text
            txtJVBRemarks.Text = txtpjbvremarks.Text
            dtVDate.Value = dtPVisitDate.Value
            txtExecutive.Text = txtPSExec.Text
            txtFollowBy.Text = txtPFollowBy.Text
            txtVFNo.Text = txtDetailVFNo.Text
            VisitorID = DGVvisitorDetail.Rows(currentRowVisitor).Cells("VisitorID").Value   'Viraj Rana 

            getPageRight()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnAddVisitor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddVisitor.Click
        txtPVstatus.Text = ""
        txtDetailVFNo.Text = ""
        txtVFNo.Text = ""
        txtPSExec.Text = ""
        txtPViRemarks.Text = ""
        txtPFollowBy.Text = ""
        txtPDuration.Text = ""
        txtPEtype.Text = ""
        txtpjbvremarks.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True


    End Sub

    Private Sub btnAddFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFollowup.Click
        dtFD_Date.Value = DateTime.Now

        txtFD_follow1.Text = ""
        txtFD_follow2.Text = ""

        dtFD_NFUpdate.Value = DateTime.Now
        txtFD_status.Text = ""

        txtFD_BWHOM.Text = ""

        txtFD_EnqType.Text = ""

        txtFD_Remarks.Text = ""
        btnsaveFollowup.Text = "Save"
        btnsaveFollowup.Enabled = True



    End Sub

    Private Sub btnDeleteInquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteInquiry.Click
        If (Address_ID > 0) Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                linq_obj.SP_Delete_Enq_FollowDetails(Address_ID)
                linq_obj.SP_Delete_Enq_VisitorDetails(Address_ID)
                linq_obj.SP_Delete_Tbl_DocumentMaster(Address_ID)
                linq_obj.SP_Delete_Enq_BioDataMaster(Address_ID)
                linq_obj.SP_Delete_Enq_ClientMaster(Address_ID)
                linq_obj.SP_Delete_Enq_FollowMaster(Address_ID)
                linq_obj.SP_Delete_Enq_VisitorMaster(Address_ID)
                linq_obj.SP_Delete_Enq_WaterMaster(Address_ID)
            End If
        Else
            MessageBox.Show("No Record Found")
        End If
        '   End If
    End Sub

    Private Sub GvInquery_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles GvInquery.PreviewKeyDown

    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click

        bindEnqGrid()
        chkAllStatus.Checked = False
        txtTotEnquiry.Text = GvInquery.RowCount.ToString()
    End Sub

    Private Sub btnVisitFollowUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisitFollowUp.Click
        If Address_ID > 0 And VisitorID > 0 Then
            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Rpt_FollowUp_visit"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID
            cmd.Parameters.Add("@VisitorDetailID", SqlDbType.Int).Value = VisitorID

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Rpt_FollowUp_visit")

            Class1.WriteXMlFile(ds, "SP_Rpt_FollowUp_visit", "SP_Rpt_FollowUp_visit")

            Dim rpt As New ReportDocument

            rpt.Load(Application.StartupPath & "\Reports\Rpt_FollowUp_Visit.rpt")

            rpt.Database.Tables(0).SetDataSource(ds.Tables(0))


            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        Else
            MessageBox.Show("No Data Found!!!")
        End If
    End Sub

    Private Sub btnVisitSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisitSummary.Click
        If Address_ID > 0 Then
            Dim rpt As New ReportDocument
            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Rpt_VisitFollowUp_Summary"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Rpt_VisitFollowUp_Summary")

            Class1.WriteXMlFile(ds, "SP_Rpt_VisitFollowUp_Summary", "SP_Rpt_VisitFollowUp_Summary")

            rpt.Load(Application.StartupPath & "\Reports\Rpt_FollowUp_Summary.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Rpt_VisitFollowUp_Summary"))


            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()


        Else
            MessageBox.Show("Data Not Found...")
        End If
    End Sub

    Private Sub chkAllStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllStatus.CheckedChanged
        If (chkAllStatus.Checked = True) Then
            rbtAll_Click(Nothing, Nothing)
        Else
            btnrefresh_Click(Nothing, Nothing)

        End If
    End Sub

    Private Sub btnPrintDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintDoc.Click


        If Address_ID > 0 Then
            Dim rpt As New ReportDocument
            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Select_Tbl_DocumentMaster"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@Fk_AddressId", SqlDbType.Int).Value = Address_ID

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Select_Tbl_DocumentMaster")

            Class1.WriteXMlFile(ds, "SP_Select_Tbl_DocumentMaster", "SP_Select_Tbl_DocumentMaster")

            rpt.Load(Application.StartupPath & "\Reports\Rpt_Document_Log.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Select_Tbl_DocumentMaster"))


            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()


        End If
    End Sub

    Public Sub getAutoCompleteData(ByVal strType As String)
        Select Case strType.Trim()
            Case "Name"
                txtInqSearchParty.AutoCompleteCustomSource.Clear()
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("Name").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    txtInqSearchParty.AutoCompleteCustomSource.Add(iteam.Result)

                Next
            Case "City"
                txtInqSearchStation.AutoCompleteCustomSource.Clear()

                Dim data = linq_obj.SP_Get_AddressListAutoComplete("City").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    txtInqSearchStation.AutoCompleteCustomSource.Add(iteam.Result)

                Next


        End Select
    End Sub

    Private Sub btnSearchAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAll.Click

        Dim criteria As String
        criteria = " and "

        If txtInqSearchParty.Text.Trim() <> "" Then
            criteria = criteria + " Name like '%" + txtInqSearchParty.Text + "%' and"
        End If
        If txtInqSearchMobileNo.Text.Trim() <> "" Then
            criteria = criteria + " MobileNo like '%" + txtInqSearchMobileNo.Text + "%' and"
        End If
        If txtInqSearchEmailID.Text.Trim() <> "" Then
            criteria = criteria + " EmailID like '%" + txtInqSearchEmailID.Text + "%' and"
        End If
        If txtInqSearchOfferNo.Text.Trim() <> "" Then
            criteria = criteria + " EnqNo like '%" + txtInqSearchOfferNo.Text + "%' and"
        End If

        If criteria = " and " Then
            criteria = ""
        End If

        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If

        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Search_InquiryAll"
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)

        Dim objclass As New Class1

        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            grpFollowDetail.Visible = False
            lblEnqNo.Text = ""
            lblLastDate.Text = ""
            lblLastFollowUpBy.Text = ""
        Else
            For index = 0 To ds.Tables(1).Rows.Count - 1
                lblEnqNo.Text = ds.Tables(1).Rows(0)("EnqNo").ToString()
                lblLastDate.Text = ds.Tables(1).Rows(0)("LastFDate").ToString()
                lblLastFollowUpBy.Text = ds.Tables(1).Rows(0)("LastFollowBy").ToString()
                lblAllotedTo.Text = ds.Tables(1).Rows(0)("EnqAllotedTo").ToString()
            Next
            grpFollowDetail.Visible = True
            ds.Dispose()
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        lblEnqNo.Text = ""
        lblLastDate.Text = ""
        lblLastFollowUpBy.Text = ""
        lblAllotedTo.Text = ""
        grpFollowDetail.Visible = False
    End Sub

    Private Sub txtDelAddress_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDelAddress.Enter

    End Sub

    Private Sub btnViewAllotment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewAllotment.Click
        Try

            Dim cmd As New SqlCommand
            cmd.CommandText = "SP_Get_TodayAllotData"
            cmd.Parameters.AddWithValue("@Start", dtStart.Value)
            cmd.Parameters.AddWithValue("@End", dtEnd.Value)
            cmd.Parameters.AddWithValue("@byUser ", Class1.global.UserID)
            Dim objclass As New Class1

            Dim ds As New DataSet
            ds = objclass.GetSearchData(cmd)
            If ds.Tables(1).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GvInquery.DataSource = Nothing

            Else
                GvInquery.DataSource = ds.Tables(1)

                'bind color
                For index = 0 To GvInquery.RowCount - 1
                    If (GvInquery.Rows(index).Cells(3).Value > 0) Then
                        GvInquery.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.DarkSlateGray
                        GvInquery.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.Yellow
                    End If
                Next

                GvInquery.Columns(0).Visible = False
                GvInquery.Columns(3).Visible = False
                ds.Dispose()

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)


        End Try

        txtTotEnquiry.Text = GvInquery.RowCount.ToString()
        chkAllStatus.Checked = False
    End Sub

    Private Sub GvInquery_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvInquery.CellClick
        cleanAll()
        Enq_Marketing_Clear()
        Marketing_Clear()
        Try
            Address_ID = Convert.ToInt32(Me.GvInquery.SelectedCells(0).Value)
            bindAllData(Address_ID)
            GvMarketingList_Bind()
            TabControl1.TabPages(0).Select()
            GvInquery.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtEntryNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntryNo.Leave

    End Sub

    Private Sub lnkClearSearch_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkClearSearch.LinkClicked
        txtInqSearchCOperson.Text = ""
        txtInqSearchEmailID.Text = ""
        txtInqSearchMobileNo.Text = ""
        txtInqSearchOfferNo.Text = ""
        txtInqSearchParty.Text = ""
        txtInqSearchStation.Text = ""
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Address_ID > 0 Then
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter()
            Dim ds As New DataSet
            Dim WeekNo As String

            WeekNo = InputBox("Enter Week No", "Enter Input", "")

            If WeekNo = "" Then
                MessageBox.Show("Pls Enter Week No")
                Exit Sub
            ElseIf IsNumeric(WeekNo) = False Then
                MessageBox.Show("You can Only for number")
                Exit Sub
            ElseIf WeekNo > 53 Then
                MessageBox.Show("Enter Valid WeekNo")
                Exit Sub
            End If

            ''Create Dataset To XmlFile

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Get_Rpt_Enquiry_Formate"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID



            da.SelectCommand = cmd
            da.Fill(ds, "Get_Rpt_Enquiry_Formate")

            ds.AcceptChanges()
            ds.Tables("Get_Rpt_Enquiry_Formate").WriteXml(Application.StartupPath & "\XmlFile\Get_Rpt_Enquiry_Formate.xml")

            cmd = New SqlCommand
            da = New SqlDataAdapter
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Get_Rpt_FolloupSheet"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Address_ID



            da.SelectCommand = cmd
            da.Fill(ds, "Get_Rpt_FolloupSheet")

            ds.AcceptChanges()


            ds.Tables("Get_Rpt_FolloupSheet").WriteXml(Application.StartupPath & "\XmlFile\Get_Rpt_FolloupSheet.xml")
            Dim rpt As New Rpt_AssessmentSheet
            'rpt.SetDataSource(ds)
            rpt.Database.Tables(0).SetDataSource(ds.Tables(0))
            ''rpt.Database.Tables("Party_Dispatch").SetDataSource(ds.Tables("Party_Dispatch"))
            rpt.SetParameterValue("WeekNo", WeekNo)

            Dim dv As DataTable
            For i As Integer = 0 To 7
                Dim DataView As New DataView

                DataView = ds.Tables(1).DefaultView
                DataView.RowFilter = "([weekno] ='" & Convert.ToInt16(WeekNo) + i & "')"
                dv = DataView.ToTable()
                If dv.Rows.Count > 0 Then
                    rpt.SetParameterValue("Status" & i, dv.Rows(0)("Status"))
                    rpt.SetParameterValue("Cnt" & i, dv.Rows(0)("Cnt"))
                Else
                    rpt.SetParameterValue("Status" & i, "")
                    rpt.SetParameterValue("Cnt" & i, "")
                End If

            Next

            Dim frm As New FrmCommanReportView(rpt)
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.Show()
        Else
            MessageBox.Show("Data Not Found...")
        End If
    End Sub

    Private Sub txtInqSearchParty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInqSearchParty.TextChanged

    End Sub

    Private Sub GvInquery_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvInquery.CellContentClick

    End Sub

    Private Sub GvInquery_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GvInquery.KeyUp
        If (e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up) Then
            cleanAll()
            Try
                Address_ID = Convert.ToInt64(GvInquery.Rows(Me.GvInquery.CurrentRow.Index).Cells(0).Value)
                bindAllData(Address_ID)
                TabControl1.TabPages(0).Select()
                GvInquery.Focus()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnAddDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDoc.Click

        If (btnAddDoc.Text = "Add") Then
            linq_obj.SP_Insert_Tbl_DocumentLog(Convert.ToInt64(Address_ID), Convert.ToInt32(ddlDocumentType.SelectedValue), txtMailBy.Text, dtMailDate.Text, txtCourierBy.Text, dtCourierDate.Text)
            linq_obj.SubmitChanges()
            BindDocumentDataByID(Address_ID)
            MessageBox.Show("Add Successfully...")
        Else
            linq_obj.SP_Update_Tbl_DocumentLog(Convert.ToInt64(PK_DocumentId), Convert.ToInt32(ddlDocumentType.SelectedValue), txtMailBy.Text, dtMailDate.Text, txtCourierBy.Text, dtCourierDate.Text)
            linq_obj.SubmitChanges()
            BindDocumentDataByID(Address_ID)
            MessageBox.Show("Update Successfully...")
        End If
        btnAddDoc.Text = "Add"

        txtclearDocument()

    End Sub

    Public Sub txtclearDocument()

        txtMailBy.Text = ""
        dtMailDate.Text = ""
        txtCourierBy.Text = ""
        dtCourierDate.Text = ""
        btnAddDoc.Text = "Add"

    End Sub

    Private Sub GvdDatadocumentList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvdDatadocumentList.DoubleClick
        Try


            PK_DocumentId = GvdDatadocumentList.SelectedCells(0).Value
            ddlDocumentType.SelectedValue = GvdDatadocumentList.SelectedCells(2).Value
            txtMailBy.Text = GvdDatadocumentList.SelectedCells(4).Value
            dtMailDate.Text = GvdDatadocumentList.SelectedCells(5).Value
            txtCourierBy.Text = GvdDatadocumentList.SelectedCells(6).Value
            dtCourierDate.Text = GvdDatadocumentList.SelectedCells(7).Value
            btnAddDoc.Text = "Update"

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddNewDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewDoc.Click
        txtclearDocument()
    End Sub
    Public Sub GvMarketingList_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("Pk_Enq_Mark_Foll_ID")
        dt.Columns.Add("CreateDate")
        dt.Columns.Add("Marketing")
        dt.Columns.Add("WeekNo")
        dt.Columns.Add("Type")
        dt.Columns.Add("Status")
        dt.Columns.Add("Remarks")
        dt.Columns.Add("MarketingID")
        Dim data = linq_obj.SP_Get_Enq_Marketing_FollowDetails(Address_ID).ToList()
        If data.Count > 0 Then
            For Each item As SP_Get_Enq_Marketing_FollowDetailsResult In data
                dt.Rows.Add(item.Pk_Enq_Mark_Foll_ID, item.Enq_Marketing_Date, item.MarketingName, item.Enq_WeekNo, item.Enq_Type, item.Enq_Status, item.Enq_Remarks, item.Fk_Marketing_ListID)

            Next
            GvMarketingList.DataSource = dt
            GvMarketingList.Columns(0).Visible = False
            GvMarketingList.Columns(7).Visible = False
        Else
            GvMarketingList.DataSource = Nothing

        End If



    End Sub


    Private Sub btnMarketingAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarketingAdd.Click

        Try

            '' insert 

            If (btnMarketingAdd.Text = "Add") Then

                linq_obj.SP_Insert_Update_Enq_Marketing_Follow_Detail(0, Convert.ToInt64(Address_ID), Convert.ToInt32(ddlMarketing.SelectedValue), Convert.ToInt32(txtEnqWeekNo.Text), txtMarketingType.Text, ddlEnqm_Status.Text, txtMarketingRemark.Text, dtmarketingdate.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Add Sucessfully..")
            Else

                linq_obj.SP_Insert_Update_Enq_Marketing_Follow_Detail(Pk_Enq_Mark_Foll_ID, Convert.ToInt64(Address_ID), Convert.ToInt32(ddlMarketing.SelectedValue), Convert.ToInt32(txtEnqWeekNo.Text), txtMarketingType.Text, ddlEnqm_Status.Text, txtMarketingRemark.Text, dtmarketingdate.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully..")
            End If

            Marketing_Clear()
            GvMarketingList_Bind()
        Catch ex As Exception
            MessageBox.Show("Something Wrong Check Value...")
        End Try
    End Sub

    Public Sub Marketing_Clear()
        txtEnqWeekNo.Text = "0"
        txtMarketingType.Text = ""
        txtMarketingRemark.Text = ""
        dtmarketingdate.Text = ""
        ddlEnqm_Status.Text = "Pending"
        btnMarketingAdd.Text = "Add"


    End Sub

    Private Sub GvMarketingList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvMarketingList.DoubleClick
        Try
            btnMarketingAdd.Text = "Update"

            Pk_Enq_Mark_Foll_ID = GvMarketingList.SelectedCells(0).Value
            dtmarketingdate.Text = GvMarketingList.SelectedCells(1).Value
            ddlMarketing.Text = GvMarketingList.SelectedCells(2).Value
            txtEnqWeekNo.Text = GvMarketingList.SelectedCells(3).Value
            txtMarketingType.Text = GvMarketingList.SelectedCells(4).Value
            txtMarketingRemark.Text = GvMarketingList.SelectedCells(6).Value
            ddlEnqm_Status.Text = GvMarketingList.SelectedCells(5).Value

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnMaketingNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMaketingNew.Click
        Marketing_Clear()
    End Sub

    Private Sub btnMarketingDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarketingDelete.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure Delete ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            Pk_Enq_Mark_Foll_ID = GvMarketingList.SelectedCells(0).Value
            linq_obj.SP_Delete_Enq_Marketing_FollowDetails(Pk_Enq_Mark_Foll_ID)
            linq_obj.SubmitChanges()
            MessageBox.Show("Delete Sucessfully...")
            Marketing_Clear()
            GvMarketingList_Bind()
        End If
    End Sub

    Private Sub txtFD_EnqType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFD_EnqType.SelectionChangeCommitted
        lblEnqOrder.Visible = False
        ddlEnqOrderStatus.Visible = False
        If txtFD_EnqType.Text.Trim() = "REGRET" Then
            ddlEnqOrderStatus_Bind()
            lblEnqOrder.Visible = True
            ddlEnqOrderStatus.Visible = True

        End If
    End Sub

    Private Sub btnStatusSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStatusSubmit.Click

        Try

            If Address_ID > 0 Then
                Dim IsValidation As Boolean
                IsValidation = True
                If ddlEnqStatusType.Text.Trim() = "" Then
                    MessageBox.Show("EnqType Can not be Blank")
                    IsValidation = False
                ElseIf Convert.ToString(ddlEnqStatusSubType.SelectedValue) = "" Then
                    IsValidation = False
                    MessageBox.Show("Sub Type Can not be Blank")
                ElseIf txtEnqStatusValue.Text = "" Then
                    IsValidation = False
                    MessageBox.Show("Value Can not be Blank")
                ElseIf txtEnqStatusOrderNo.Text = "" Then
                    IsValidation = False
                    MessageBox.Show("Token Can not be Blank")
                End If

                If IsValidation = True Then
                    If btnStatusSubmit.Text.Trim() = "Submit" Then
                        linq_obj.SP_Insert_Update_Enq_Order_Status_Master(0, Address_ID, Convert.ToInt32(ddlEnqStatusType.SelectedValue), Convert.ToInt64(ddlEnqStatusSubType.SelectedValue), Class1.global.UserID, Convert.ToDecimal(txtEnqStatusValue.Text), txtEnqStatusOrderNo.Text, Convert.ToInt64(ddlExecutive1.SelectedValue), Convert.ToInt64(ddlExecutive2.SelectedValue), Convert.ToInt64(ddlEnqStatusSales.SelectedValue), Convert.ToInt64(ddlEnqStatusTeam.SelectedValue), txtEnqStatusRemarks.Text, dtEnqStatusDate.Value.Date, Convert.ToDecimal(txtEnqStatusDispValue.Text))
                        linq_obj.SubmitChanges()
                        linq_obj.SP_UpdateAddressStatus(ddlEnqStatusType.Text.Trim(), Address_ID)
                        linq_obj.SubmitChanges()

                        MessageBox.Show("Submit Sucessfully...")
                    Else
                        linq_obj.SP_Insert_Update_Enq_Order_Status_Master(PK_Enq_Order_Status_Master_ID, Address_ID, Convert.ToInt32(ddlEnqStatusType.SelectedValue), Convert.ToInt64(ddlEnqStatusSubType.SelectedValue), Class1.global.UserID, Convert.ToDecimal(txtEnqStatusValue.Text), txtEnqStatusOrderNo.Text, Convert.ToInt64(ddlExecutive1.SelectedValue), Convert.ToInt64(ddlExecutive2.SelectedValue), Convert.ToInt64(ddlEnqStatusSales.SelectedValue), Convert.ToInt64(ddlEnqStatusTeam.SelectedValue), txtEnqStatusRemarks.Text, dtEnqStatusDate.Value.Date, Convert.ToDecimal(txtEnqStatusDispValue.Text))
                        linq_obj.SubmitChanges()
                        linq_obj.SP_UpdateAddressStatus(ddlEnqStatusType.Text.Trim(), Address_ID)
                        linq_obj.SubmitChanges()
                        MessageBox.Show("Update Sucessfully...")
                    End If
                    GvEnqStatusLog_Bind()
                    Enq_Status_Clear()


                End If
            Else
                MessageBox.Show("Please select EnqNo")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Public Sub Enq_Status_Clear()
        btnStatusSubmit.Text = "Submit"
        txtEnqStatusEnqDate.Text = ""
        ddlEnqStatusType.SelectedIndex = 0
        ddlExecutive1.SelectedValue = 0
        ddlExecutive2.SelectedValue = 0
        ddlEnqStatusType.SelectedIndex = 0
        ddlEnqStatusSubType.SelectedIndex = -1
        ddlEnqStatusTeam.SelectedValue = 0
        ddlEnqStatusSales.SelectedValue = 0
        txtEnqStatusOrderNo.Text = "0"
        txtEnqStatusValue.Text = "0"
        txtEnqStatusDispValue.Text = "0"
        lblOrderStatusID.Text = "0"
        txtEnqStatusRemarks.Text = ""

        ddlEnqStatusType.Enabled = True
        ddlEnqStatusSubType.Enabled = True


    End Sub

    Private Sub ddlEnqStatusType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEnqStatusType.SelectionChangeCommitted
        ddlEnqStatusSubType_Bind()
    End Sub

    Private Sub btnEnqStatusNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnStatusSubmit.Visible = True
        Enq_Status_Clear()
    End Sub

    Private Sub btnEnqStatusNew_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnqStatusNew.Click
        Enq_Status_Clear()
        ddlEnqStatusType_Bind()
    End Sub
     
    
    Private Sub GvEnqStatusLog_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvEnqStatusLog.DoubleClick
        PK_Enq_Order_Status_Master_ID = Convert.ToInt32(Me.GvEnqStatusLog.SelectedCells(0).Value)
        Edit_ddlEnqStatusType_Bind()
        Edit_Enq_StatusDetail()
    End Sub

    
End Class