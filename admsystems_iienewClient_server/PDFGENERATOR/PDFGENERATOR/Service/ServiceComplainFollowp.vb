Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Public Class ServiceComplainFollowp
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PK_Service_Request_ID As Integer
    Dim Pk_Address_ID As Integer
    Dim Pk_Service_Complain_Followp_ID As Integer
    Dim Pk_Service_CompDeta_ID As Integer


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        txtCreateDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        txtTime.Text = TimeOfDay.ToString("hh:mm tt")
        txtCallAttandBy.Text = Class1.global.User
        txtCompanyName.AutoCompleteCustomSource.Clear()
        Dim data = linq_obj.SP_Get_AddressListAutoComplete("Name").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In data
            txtCompanyName.AutoCompleteCustomSource.Add(iteam.Result)
        Next
        GvServiceComplain_List()
        FllowpEntry_User()
        ddlEngineerList_Bind()
    End Sub

    Public Sub FllowpEntry_User()
        If Class1.global.User.ToLower() = "rk" Or Class1.global.User.ToLower() = "nr" Or Class1.global.User.ToLower() = "ar" Or Class1.global.User.ToLower() = "pp" Then
            btnSaveFollowup.Visible = True
            btnDelFollowup.Visible = True
            btnAddFollowup.Visible = True
        Else
            btnSaveFollowup.Visible = False
            btnDelFollowup.Visible = False
            btnAddFollowup.Visible = False


        End If

    End Sub
    Public Sub ddlEngineerList_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")
        Dim data = linq_obj.SP_Get_Service_Engineer_Master_List().ToList()
        For Each item As SP_Get_Service_Engineer_Master_ListResult In data
            dt.Rows.Add(item.Pk_Engineer_ID, item.Name)
        Next
        ddlEngineerList.DataSource = dt
        ddlEngineerList.DisplayMember = "Name"
        ddlEngineerList.ValueMember = "ID"

        ddlEngineerList.AutoCompleteMode = AutoCompleteMode.Append
        ddlEngineerList.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEngineerList.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Public Sub GvComplainDetail_Bind()
        Dim data = linq_obj.SP_Get_Service_Complain_Detail_By_ServiceID(PK_Service_Request_ID).ToList()
        GvServiceComplainDetail.DataSource = data
        GvServiceComplainDetail.Columns(0).Visible = False
        GvServiceComplainDetail.Columns(1).Visible = False

        GvServiceComplainDetail.Columns(7).Visible = False
        GvServiceComplainDetail.Columns(9).Visible = False



    End Sub
    Public Sub GvServiceComplain_List()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("CompanyName")
        dt.Columns.Add("AttandBy")
        Dim searchComplainStatus As String
        searchComplainStatus = ""

        If rblSearchPending.Checked = True Then
            searchComplainStatus = "Pending"
        ElseIf rblSearchSCH.Checked = True Then
            searchComplainStatus = "SCH"
        ElseIf rblSearchRunning.Checked = True Then
            searchComplainStatus = "Running"
        ElseIf rblSearchDone.Checked = True Then
            searchComplainStatus = "Done"
        ElseIf rblSearchCancel.Checked = True Then
            searchComplainStatus = "Cancel"
        Else
            searchComplainStatus = ""

        End If


        Dim criteria As String
        criteria = "and "
        If txtSearchComp.Text.Trim() <> "" Then
            criteria = criteria + " ComplainNo like '%" + txtSearchComp.Text + "%'and "
        End If
        If txtSearchCompany.Text.Trim() <> "" Then
            criteria = criteria + " CompanyName like '%" + txtSearchCompany.Text + "%'and "
        End If
        If searchComplainStatus <> "" Then
            criteria = criteria + " ComplainStatus like '%" + searchComplainStatus + "%'and "
        End If
        If txtSearchCity.Text <> "" Then
            criteria = criteria + " City like '%" + txtSearchCity.Text + "%'and "
        End If
        If txtSearchMobile.Text <> "" Then
            criteria = criteria + " ContactNo like '%" + txtSearchMobile.Text + "%'and "
        End If



        If criteria = "and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.Trim().ToString().Substring(0, criteria.Trim().Length - 3)
        End If
        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Service_Complain_Master_Criteria"
        cmd.Parameters.Add("@Criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
        cmd.Parameters.AddWithValue("@UserID", Class1.global.UserID.ToString())
        If chkIsDate.Checked = False Then
            cmd.Parameters.AddWithValue("@StartDate", "01/01/2001")
        Else
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dtFromDate.Value.ToString("dd/MM/yyyy")

        End If
        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dtToDate.Value.ToString("dd/MM/yyyy")



        'End 
        cmd.CommandTimeout = 3000
        Dim objclass As New Class1
        Dim ds As New DataSet

        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvServiceComplain.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            For i = 0 To ds.Tables(1).Rows.Count - 1
                dt.Rows.Add(ds.Tables(1).Rows(i)("PK_Service_Request_ID"), ds.Tables(1).Rows(i)("CompanyName"), ds.Tables(1).Rows(i)("ServiceAttBy"))

            Next
            GvServiceComplain.DataSource = dt
            txtTotal.Text = Convert.ToString(dt.Rows.Count)
            GvServiceComplain.Columns(0).Visible = False

        End If




    End Sub
    Public Sub Set_Clean()


        txtServiceReqBy.Text = ""
        txtCallAttandBy.Text = ""

        txtCompanyName.Text = ""
        txtAddress.Text = ""
        txtCity.Text = ""
        txtTal.Text = ""
        txtDist.Text = ""
        txtPin.Text = ""
        txtState.Text = ""
        txtContactNo.Text = ""
        txtEmailID.Text = ""
        ddlMachineType.Text = ""
        txtCapacity.Text = ""
        '  txtServiceReqDetail.Text = ""
        chkexp1.Checked = False
        chkexp2.Checked = False
        '  chkExp3.Checked = False
        rblPendingStatus.Checked = True
        txtMaterial.Text = ""
        txtReqLevel.Text = ""
        txtClientHistory.Text = ""
        txtRemark.Text = ""
        txtCreateDate.Text = ""
        txtTime.Text = ""
        txtEnQNo.Text = ""

        txtCustomerID.Text = ""


        txtBillDate.Text = ""
        txtBillAmount.Text = ""
        txtPaidByClient.Text = ""
        txtLoading.Text = ""
        txtToFro.Text = ""
        txtClintManageName.Text = ""
        txtClientMDate.Text = ""
        txtClientMTime.Text = ""
        txtClientEName.Text = ""
        txtClinetEDate.Text = ""
        txtClientETime.Text = ""
        btnSubmit.Text = "Submit"
        lblSRLogEntryUser.Text = ""
        txtCreateDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        txtSSRdateLog.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        txtTime.Text = TimeOfDay.ToString("hh:mm tt")
        txtCallAttandBy.Text = Class1.global.User
    End Sub

    Private Sub GvServiceComplain_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvServiceComplain.DoubleClick

        Set_Clean()
        Clear_Text()
        PK_Service_Request_ID = Me.GvServiceComplain.SelectedCells(0).Value
        btnSubmit.Visible = True
        btnSubmit.Text = "Update"
        GvComplain_Display()
        GvServiceFollowp_Bind()
        GvComplainDetail_Bind()
    End Sub
    Public Sub GvComplain_Display()

        Dim data = linq_obj.SP_Get_Service_Complain_Master_List(PK_Service_Request_ID).ToList()
        For Each item As SP_Get_Service_Complain_Master_ListResult In data
            Pk_Address_ID = item.Fk_Address_ID
            txtComplainNo.Text = item.ComplainNo
            txtServiceReqBy.Text = item.ServiceReqBy
            txtCallAttandBy.Text = item.ServiceAttBy
            txtCompanyName.Text = item.CompanyName
            txtAddress.Text = item.Address
            txtCity.Text = item.City
            txtTal.Text = item.Taluko
            txtDist.Text = item.District
            txtPin.Text = item.Pincode
            txtState.Text = item.State
            txtContactNo.Text = item.ContactNo
            txtEmailID.Text = item.EmailID
            txtEnQNo.Text = item.EnqNo
            chkexp1.Checked = False
            chkexp2.Checked = False

            txtCustomerID.Text = item.CustomerID
            ''ComplainStatus
            If rblServiceComplain.Text = item.ComplainType Then
                rblServiceComplain.Checked = True
            Else
                rblECComplain.Checked = True

            End If
            If item.Exp1 = "Yes" Then
                chkexp1.Checked = True
            End If
            If item.Exp2 = "Yes" Then
                chkexp2.Checked = True
            End If
            If item.Exp3 = "Yes" Then
                ' chkExp3.Checked = True
            End If
            txtMaterial.Text = item.SP_Material
            txtReqLevel.Text = item.Req_Level
            txtClientHistory.Text = item.ClientHistory
            txtRemark.Text = item.Remarks
            txtCreateDate.Text = item.CreateDate
            txtTime.Text = item.Time

            If item.ComplainStatus = "Pending" Then
                rblPendingStatus.Checked = True
            ElseIf item.ComplainStatus = "SCH" Then
                rblComplainSCH.Checked = True
            ElseIf item.ComplainStatus = "Running" Then
                rblComplainRunning.Checked = True
            ElseIf item.ComplainStatus = "Done" Then
                rblDoneStatus.Checked = True
            Else
                rblCancelStatus.Checked = True
            End If

            txtBillDate.Text = item.BillDate
            txtBillAmount.Text = item.BillAmount
            txtPaidByClient.Text = item.PaidByClient
            txtLoading.Text = item.Loading
            txtToFro.Text = item.ToFro
            txtClintManageName.Text = item.Client_MName
            txtClientMDate.Text = item.Client_MDate
            txtClientMTime.Text = item.Client_MTime
            txtClientEName.Text = item.Client_EName
            txtClinetEDate.Text = item.Client_EDate
            txtClientETime.Text = item.Client_ETime
            If txtCreateDate.Text <> System.DateTime.Now.ToString("dd/MM/yyyy") Then
                If Class1.global.UserAllotType <> "Head" Then
                    btnSubmit.Visible = False
                End If
            End If
            If Class1.global.UserAllotType = "Head" Then
                GroupBox2.Enabled = True
            End If

            Dim datass = linq_obj.SP_Get_Service_Complain_SSR_Log_By_FK_Service_Request_ID(PK_Service_Request_ID)
            For Each item_sl As SP_Get_Service_Complain_SSR_Log_By_FK_Service_Request_IDResult In datass
                lblLastUpdateDate.Text = Convert.ToDateTime(item_sl.CreateDate)
                lblSRLogEntryUser.Text = item_sl.UserName


            Next

        Next


    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GvServiceComplain_List()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        ' rblSearchNone.Checked = True
        txtSearchComp.Text = ""
        txtSearchCompany.Text = ""
        GvServiceComplain_List()
    End Sub

    Private Sub btnSaveFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFollowup.Click
        Try

            If btnSaveFollowup.Text = "Add" Then
                linq_obj.SP_Insert_Update_Service_Complain_Follow_Up_Master(0, PK_Service_Request_ID, dtFollowDate.Text, dtNFDate.Text, txtFolloupDetail.Text, txtFollowstatus.Text, txtFollowpBywhoom.Text, txtFollowpProType.Text, txtFollowupRemark.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Add Sucessfully...")
            Else
                linq_obj.SP_Insert_Update_Service_Complain_Follow_Up_Master(Pk_Service_Complain_Followp_ID, PK_Service_Request_ID, dtFollowDate.Text, dtNFDate.Text, txtFolloupDetail.Text, txtFollowstatus.Text, txtFollowpBywhoom.Text, txtFollowpProType.Text, txtFollowupRemark.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully...")
            End If

            GvServiceFollowp_Bind()
            Clear_Text()


        Catch ex As Exception
        End Try
    End Sub

    Private Sub GvServiceFollowp_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvServiceFollowp.DoubleClick

        btnSaveFollowup.Text = "Update"
        Pk_Service_Complain_Followp_ID = GvServiceFollowp.SelectedCells(0).Value
        dtFollowDate.Text = GvServiceFollowp.SelectedCells(2).Value
        dtNFDate.Text = GvServiceFollowp.SelectedCells(3).Value
        txtFolloupDetail.Text = GvServiceFollowp.SelectedCells(4).Value
        txtFollowstatus.Text = GvServiceFollowp.SelectedCells(5).Value
        txtFollowpBywhoom.Text = GvServiceFollowp.SelectedCells(6).Value
        txtFollowpProType.Text = GvServiceFollowp.SelectedCells(7).Value
        txtFollowupRemark.Text = GvServiceFollowp.SelectedCells(8).Value

    End Sub
    Public Sub Clear_Text()
        btnSaveFollowup.Text = "Add"
        txtFolloupDetail.Text = ""
        txtFollowstatus.Text = ""
        txtFollowpBywhoom.Text = ""
        txtFollowpProType.Text = ""
        txtFollowupRemark.Text = ""


        Pk_Service_Complain_Followp_ID = 0
    End Sub
    Public Sub GvServiceFollowp_Bind()
        Dim data = linq_obj.SP_GetService_Complain_Follow_Up_Master_By_ComplainID(PK_Service_Request_ID).ToList()
        GvServiceFollowp.DataSource = data
        GvServiceFollowp.Columns(0).Visible = False
        GvServiceFollowp.Columns(1).Visible = False


    End Sub

    Private Sub txtDaysAfter_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDaysAfter.Leave
        If txtDaysAfter.Text <> "" Then
            dtNFDate.Value = dtFollowDate.Value.Date.AddDays(txtDaysAfter.Text)
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim ComplainStatus As String
            Dim Exp1, Exp2, Exp3 As String
            Exp1 = "No"
            Exp2 = "No"
            Exp3 = "No"

            Dim RequestType As String


            If rblServiceComplain.Checked = True Then
                RequestType = "Service"
            Else
                RequestType = "EC"

            End If
            If chkexp1.Checked = True Then
                Exp1 = "Yes"

            End If

            If chkexp2.Checked = True Then
                Exp2 = "Yes"

            End If
            If rblPendingStatus.Checked = True Then
                ComplainStatus = "Pending"
            ElseIf rblComplainSCH.Checked = True Then
                ComplainStatus = "SCH"
            ElseIf rblComplainRunning.Checked = True Then
                ComplainStatus = "Running"
            ElseIf rblDoneStatus.Checked = True Then
                ComplainStatus = "Done"
            Else
                ComplainStatus = "Cancel"
            End If

            If txtComplainNo.Text.Trim() <> "" Then

                If btnSubmit.Text = "Submit" Then
                    Maximum_ComplainNo()

                    PK_Service_Request_ID = linq_obj.SP_Insert_Update_Service_Complain_Master(0,
                                                                       Pk_Address_ID,
                                                                      txtComplainNo.Text,
                                                                      txtServiceReqBy.Text,
                                                                      txtCallAttandBy.Text,
                                                                     Class1.global.UserID,
                                                                     txtCompanyName.Text,
                                                                     txtAddress.Text,
                                                                     txtCity.Text,
                                                                     txtTal.Text,
                                                                     txtDist.Text,
                                                                     txtPin.Text,
                                                                     txtState.Text,
                                                                     txtContactNo.Text,
                                                                     txtEmailID.Text,
                                                                     ddlMachineType.Text,
                                                                    txtCapacity.Text,
                                                                    "",
                                                                     Exp1,
                                                                     Exp2,
                                                                     Exp3,
                                                                    txtMaterial.Text,
                                                                    txtReqLevel.Text,
                                                                    txtClientHistory.Text,
                                                                    txtRemark.Text,
                                                                    txtCreateDate.Text,
                                                                    txtTime.Text,
                                                                    ComplainStatus, txtCustomerID.Text, RequestType, txtBillDate.Text, txtBillAmount.Text,
                                                                    txtPaidByClient.Text, txtLoading.Text, txtToFro.Text, txtClintManageName.Text, txtClientMDate.Text, txtClientMTime.Text, txtClientEName.Text, txtClinetEDate.Text, txtClientETime.Text, txtSSRdateLog.Text.Trim())
                    linq_obj.SubmitChanges()



                    'Insert Service Complain SSR Log  
                    linq_obj.SP_Insert_Service_Complain_SSR_Log(PK_Service_Request_ID, Class1.global.UserID, ComplainStatus, txtSSRdateLog.Text.Trim())
                    linq_obj.SubmitChanges()

                    GvServiceComplain_List()
                Else
                    linq_obj.SP_Insert_Update_Service_Complain_Master(PK_Service_Request_ID,
                                                                   Pk_Address_ID,
                                                                   txtComplainNo.Text,
                                                                   txtServiceReqBy.Text,
                                                                   txtCallAttandBy.Text,
                                                                   Class1.global.UserID,
                                                                  txtCompanyName.Text,
                                                                  txtAddress.Text,
                                                                  txtCity.Text,
                                                                  txtTal.Text,
                                                                  txtDist.Text,
                                                                  txtPin.Text,
                                                                  txtState.Text,
                                                                  txtContactNo.Text,
                                                                  txtEmailID.Text,
                                                                  ddlMachineType.Text,
                                                                 txtCapacity.Text,
                                                                 "",
                                                                  Exp1,
                                                                  Exp2,
                                                                  Exp3,
                                                                 txtMaterial.Text,
                                                                 txtReqLevel.Text,
                                                                 txtClientHistory.Text,
                                                                 txtRemark.Text,
                                                                 txtCreateDate.Text,
                                                                 txtTime.Text,
                                                                 ComplainStatus, txtCustomerID.Text, RequestType, txtBillDate.Text, txtBillAmount.Text,
                                                                    txtPaidByClient.Text, txtLoading.Text, txtToFro.Text, txtClintManageName.Text, txtClientMDate.Text, txtClientMTime.Text, txtClientEName.Text, txtClinetEDate.Text, txtClientETime.Text, txtSSRdateLog.Text.Trim())
                    linq_obj.SubmitChanges()
                    MessageBox.Show("Update Sucessfully..")
                    linq_obj.SP_Insert_Service_Complain_SSR_Log(PK_Service_Request_ID, Class1.global.UserID, ComplainStatus, txtSSRdateLog.Text.Trim())
                    linq_obj.SubmitChanges()
                    GvServiceComplain_List()
                    txtComplainNo.Text = ""
                    Set_Clean()
                End If
            Else
                MessageBox.Show("Complain No can not Blank..")

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtComplainNo.Text = ""
        Set_Clean()
        Clear_Text()
    End Sub

    Private Sub txtEnQNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnQNo.Leave
        If txtEnQNo.Text.Trim() <> "" Then
            Dim data = linq_obj.SP_Get_ServiceODSite_Allotment_EnqNo(txtEnQNo.Text.Trim()).ToList()
            If data.Count > 0 Then
                For Each item As SP_Get_ServiceODSite_Allotment_EnqNoResult In data
                    txtCompanyName.Text = item.Name
                    Pk_Address_ID = item.Pk_AddressID
                    txtCity.Text = item.City
                    txtState.Text = item.State
                    txtAddress.Text = item.Address
                    txtTal.Text = item.Taluka
                    txtPin.Text = item.Pincode
                    txtDist.Text = item.District
                    txtContactNo.Text = item.MobileNo
                    txtEmailID.Text = item.EmailID

                Next
            Else
                MessageBox.Show("Invalid EnqNo...")
                Clear_Text()
                txtEnQNo.Focus()

            End If
            'Assign OD Site Engineer List

        End If
    End Sub
    Public Sub Maximum_ComplainNo()
        Dim complainNo = linq_obj.SP_Get_Max_Service_ComplainNo(Class1.global.UserID).ToList()
        For Each item As SP_Get_Max_Service_ComplainNoResult In complainNo
            txtComplainNo.Text = item.ComplainNo
        Next
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        txtComplainNo.Text = ""
        Maximum_ComplainNo()
        GvServiceComplainDetail.DataSource = Nothing
        PK_Service_Request_ID = 0
        Clear_Text()
        Set_Clean()
    End Sub

    Private Sub btnAddFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFollowup.Click
        Clear_Text()
    End Sub

    Private Sub btnComplainAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComplainAdd.Click
        Try

            If btnSubmit.Text = "Submit" Then
                btnSubmit_Click(Nothing, Nothing)
                btnSubmit.Text = "Update"
            End If
            If btnComplainAdd.Text = "Add" Then
                linq_obj.SP_Insert_Update_Service_Complain_Detail(0, PK_Service_Request_ID, ddlMachineType.Text, txtCapacity.Text, txtComplainRequest.Text, txtComplainSolution.Text, txtAllotDate.Text, Convert.ToInt64(ddlEngineerList.SelectedValue), txtDoneDate.Text, "")
                linq_obj.SubmitChanges()


                MessageBox.Show("Add Sucessfully..")

            Else
                linq_obj.SP_Insert_Update_Service_Complain_Detail(Pk_Service_CompDeta_ID, PK_Service_Request_ID, ddlMachineType.Text, txtCapacity.Text, txtComplainRequest.Text, txtComplainSolution.Text, txtAllotDate.Text, Convert.ToInt64(ddlEngineerList.SelectedValue), txtDoneDate.Text, "")
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully..")
            End If
            Clear_Complain()
            GvComplainDetail_Bind()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Clear_Complain()
        btnComplainAdd.Text = "Add"

        txtCapacity.Text = ""
        txtComplainRequest.Text = ""
        txtComplainSolution.Text = ""
        txtAllotDate.Text = ""
        txtDoneDate.Text = ""


    End Sub

    Private Sub GvServiceComplainDetail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvServiceComplainDetail.DoubleClick
        btnComplainAdd.Text = "Update"
        Pk_Service_CompDeta_ID = GvServiceComplainDetail.SelectedCells(0).Value
        ddlMachineType.Text = GvServiceComplainDetail.SelectedCells(2).Value
        txtCapacity.Text = GvServiceComplainDetail.SelectedCells(3).Value
        txtComplainRequest.Text = GvServiceComplainDetail.SelectedCells(4).Value
        txtComplainSolution.Text = GvServiceComplainDetail.SelectedCells(5).Value
        txtAllotDate.Text = GvServiceComplainDetail.SelectedCells(6).Value

        txtDoneDate.Text = GvServiceComplainDetail.SelectedCells(8).Value
        ' ddlEngineerList.SelectedValue = GvServiceComplainDetail.SelectedCells(7).Value

    End Sub

    Private Sub GvServiceComplain_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvServiceComplain.CellContentClick

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If PK_Service_Request_ID > 0 Then

            Dim flag As Boolean
            flag = True

            'check engineer is assign or not 
            Dim Enginerr = linq_obj.SP_Get_Service_Complain_Detail_By_ServiceID(PK_Service_Request_ID).ToList()

            If Enginerr(0).EnggName.Trim() = "00NONE" Then
                flag = False
            End If 

            If flag = True Then

                Dim ODEngineerDS As New DataSet

                Dim cmd As New SqlCommand
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "SP_Rpt_Service_Complain_FormPrint"
                cmd.Connection = linq_obj.Connection
                cmd.Parameters.Add("@PK_Service_Request_ID", SqlDbType.Int).Value = PK_Service_Request_ID

                Dim da As New SqlDataAdapter()
                da.SelectCommand = cmd
                da.Fill(ODEngineerDS, "ODEngineerDS")

                Class1.WriteXMlFile(ODEngineerDS, "SP_Rpt_Service_Complain_FormPrint", "ODEngineerDS")

                Dim rpt As New rptServiceComplainForm

                rpt.Database.Tables(0).SetDataSource(ODEngineerDS.Tables("ODEngineerDS"))

                Dim frmRpt As New FrmCommanReportView(rpt)
                frmRpt.Show()
            Else
                MessageBox.Show("Please Assign Engineer.. for Print Complain")
            End If
        Else
            MessageBox.Show("Data Not Found...")
        End If
    End Sub



    Private Sub rblComplainSCH_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblComplainSCH.CheckedChanged
        txtSSRdateLog.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub rblComplainRunning_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblComplainRunning.CheckedChanged
        txtSSRdateLog.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub rblDoneStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblDoneStatus.CheckedChanged
        txtSSRdateLog.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub rblCancelStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblCancelStatus.CheckedChanged
        txtSSRdateLog.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
    End Sub
End Class