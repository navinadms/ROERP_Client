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
Imports System.IO
Imports System.Net


Public Class ServicePartyMaster

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Pk_Address_ID As Integer
    Dim Pk_Service_Doc_ID As Integer
    Dim FK_ProjectMaster_ID As Integer

    Dim Pk_Service_Followp As Integer

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        GvPartyList_Bind()
    End Sub
    Public Sub GvPartyList_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")
        dt.Columns.Add("FStatus")


        Dim criteria As String
        criteria = "and "
        If txtSearchEnqno.Text.Trim() <> "" Then
            criteria = criteria + " EnqNo like '%" + txtSearchEnqno.Text + "%'and "
        End If
        If txtSearchName.Text.Trim() <> "" Then
            criteria = criteria + " Name like '%" + txtSearchName.Text + "%'and "
        End If
        If txtSearchMobile.Text.Trim() <> "" Then
            criteria = criteria + " MobileNo like '%" + txtSearchMobile.Text + "%'and "
        End If
        If txtSearchEmail.Text.Trim() <> "" Then
            criteria = criteria + " EmailID like '%" + txtSearchEmail.Text + "%'and "
        End If

        If criteria = "and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.Trim().ToString().Substring(0, criteria.Trim().Length - 3)
        End If


        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Service_Party_Allotment_AssinByUserId_Criteria"
        cmd.Parameters.Add("@Criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
        cmd.Parameters.AddWithValue("@UserID", Class1.global.UserID.ToString())

        'End 
        cmd.CommandTimeout = 3000

        Dim objclass As New Class1

        Dim ds As New DataSet

        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvPartyList.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            For i = 0 To ds.Tables(1).Rows.Count - 1
                dt.Rows.Add(ds.Tables(1).Rows(i)("Pk_AddressID"), ds.Tables(1).Rows(i)("EnqNo"), ds.Tables(1).Rows(i)("Name"), ds.Tables(1).Rows(i)("FStatus"))

            Next
            GvPartyList.DataSource = dt
            txtTotal.Text = Convert.ToString(dt.Rows.Count)

        End If


        GvColor_bind()

    End Sub

    Private Sub GvPaDataist_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvPartyList.DoubleClick
        Clear_Document_Text()
        Clear_Text()
        Pk_Address_ID = GvPartyList.SelectedCells(0).Value
        Pipeline_Milestone_Bind()
        Display_Data()
        GvService_documet_Bind()
    End Sub

    Public Sub Display_Data()
        'Client Detail 
        Dim Claient = linq_obj.SP_Get_AddressListById_New(Pk_Address_ID).ToList()
        For Each item As SP_Get_AddressListById_NewResult In Claient
            txtName.Text = item.Name
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
            txtEnqNo.Text = item.EnqNo
            txtvalue1.Text = item.EmailID1
            txtValue2.Text = item.EmailID2
        Next

        'Disp Date and Tent  Date from Order Manager
        Dim data = linq_obj.SP_Get_OrderOneMaster_Disp_Date().ToList().Where(Function(p) p.Pk_AddressID = Pk_Address_ID).ToList()
        For Each item As SP_Get_OrderOneMaster_Disp_DateResult In data
            txtDispDate.Text = item.DispatchDate
            txtOrderDate.Text = item.OrderDate
        Next

        'bind Project Information 
        Dim dataprojectMaster = linq_obj.SP_Select_Tbl_ProjectInformationMaster(Pk_Address_ID).ToList()
        If (dataprojectMaster.Count > 0) Then
            For Each item As SP_Select_Tbl_ProjectInformationMasterResult In dataprojectMaster
                txtPlantName.Text = item.PlantName
                txtModel.Text = item.Model
                txtProject.Text = item.ProjectName
                txtCapacity.Text = item.Capacity
                txtPowerAvail.Text = item.PowerAvailable
                txtPlantShape.Text = item.PlantShape
                txtLandArea.Text = item.LandArea
                txttentiveSchem.Text = item.TreatmentScheme
                txtJarDis.Text = item.JarDis
                txtBmouldDis.Text = item.BMouldDis

            Next
        End If

        'Project Detail

        Dim dataProjectDetail = linq_obj.SP_Select_Tbl_ProjectDetail(Pk_Address_ID).ToList()
        If (dataProjectDetail.Count > 0) Then

            Dim dt As New DataTable
            dt.Columns.Add("SrNo")
            dt.Columns.Add("PlantScheme")
            dt.Columns.Add("VendorName")
            dt.Columns.Add("EntryDate")
            For Each item As SP_Select_Tbl_ProjectDetailResult In dataProjectDetail
                dt.Rows.Add(item.SrNo, item.PlantScheme, item.VendorName, Class1.Datecheck(item.DispatchDate))
            Next
            DGVOrderdtail.DataSource = dt
        End If

        'Get Daily OD Engineer Attandance Entry By AddressID 

        Dim enggdata = linq_obj.SP_Get_Engineer_OD_AttandanceBy_Address_ID(Pk_Address_ID).ToList()

        GvServiceAttData.DataSource = enggdata

        'Packing Detail

        Dim EngName As String
        Dim Donedate As String
        Dim MachineStatus As String
        Dim MachineType As String



        EngName = ""
        Donedate = ""
        MachineStatus = ""
        MachineType = ""




        Dim dtpacking As New DataTable
        dtpacking.Columns.Add("SrNo")
        dtpacking.Columns.Add("Machine")
        dtpacking.Columns.Add("Capacity")
        dtpacking.Columns.Add("MachineType")
        dtpacking.Columns.Add("EngineerName")
        dtpacking.Columns.Add("Status")
        dtpacking.Columns.Add("Date")

        Dim packdata = linq_obj.SP_Get_PackingDetail_New_List(Pk_Address_ID).ToList()
        For Each itempk As SP_Get_PackingDetail_New_ListResult In packdata
            EngName = ""
            Donedate = ""
            MachineStatus = ""
            MachineType = ""
            Dim MachineAttance = linq_obj.SP_Get_Engineer_OD_AttandanceBy_Address_ID(Pk_Address_ID).ToList().Where(Function(p) p.Machine = itempk.Item).ToList()
            For Each itm As SP_Get_Engineer_OD_AttandanceBy_Address_IDResult In MachineAttance
                EngName = EngName + itm.Engineer + ","
                Donedate = itm.CreateDate
                MachineStatus = itm.MachineStatus
                MachineType = itm.MachineType

            Next

            dtpacking.Rows.Add(dtpacking.Rows.Count + 1, itempk.Item, itempk.Capacity, MachineType, EngName, MachineStatus, Donedate)


        Next

        'Dim Packing = linq_obj.SP_Get_Service_PackingDetail_By_AddressID(Pk_Address_ID).ToList()
        GvPackingDetailNewList.DataSource = dtpacking


        'EC Detail 

        Dim EC = linq_obj.SP_Get_Service_ECDetails_Status(Pk_Address_ID).ToList()
        For Each item As SP_Get_Service_ECDetails_StatusResult In EC

            ddlECSStatus.Text = item.EC_Status
            txtECRemark.Text = item.Ec_Remarks
            DtEcDate.Text = item.Ec_Date


        Next

        'Project Folloup Detail

        Dim dtproject As New DataTable
        dtproject.Columns.Add("F_Date")
        dtproject.Columns.Add("Followup")
        dtproject.Columns.Add("N_F_FollowpDate")
        dtproject.Columns.Add("Status")
        dtproject.Columns.Add("ByWhom")
        dtproject.Columns.Add("Pro_Type")
        dtproject.Columns.Add("Remarks")


        Dim datafolloupDetail = linq_obj.SP_Select_All_Tbl_OrderFollowupDetailByFollowUp(Pk_Address_ID).ToList()
        If (datafolloupDetail.Count > 0) Then

            For Each item As SP_Select_All_Tbl_OrderFollowupDetailByFollowUpResult In datafolloupDetail
                dtproject.Rows.Add(item.FDate, item.FollowUp, item.NFDate, item.Status, item.ByWhom, item.ProjectType, item.Remarks)

            Next
            GvProjectFolloup.DataSource = dtproject
        End If

        'Service Follow Up 
        GvServiceFollowp_Bind()

    End Sub

    Public Sub GvServiceFollowp_Bind()
        Dim data = linq_obj.SP_Get_Service_Follow_Up_List_By_AddresID(Pk_Address_ID).ToList()
        GvServiceFollowp.DataSource = data
        GvServiceFollowp.Columns(0).Visible = False
        GvServiceFollowp.Columns(1).Visible = False

    End Sub

    Public Sub GvService_documet_Bind()



        GvDocumentUpload.Columns.Clear()
        GvDocumentUpload.ColumnCount = 4
        GvDocumentUpload.Columns(0).Name = "Pk_Service_Doc_ID"
        GvDocumentUpload.Columns(1).Name = "DocumentName"
        GvDocumentUpload.Columns(2).Name = "Path"
        GvDocumentUpload.Columns(3).Name = "CreateDate"


        Dim data = linq_obj.SP_Get_Service_Document_Upload(Pk_Address_ID).ToList()

        For Each item As SP_Get_Service_Document_UploadResult In data
            GvDocumentUpload.Rows.Add(item.Pk_Service_Doc_ID, item.DocumentName, item.DocumentPath, item.CreateDate)
        Next


        Dim dgvButton As New DataGridViewButtonColumn()
        dgvButton.FlatStyle = FlatStyle.System
        dgvButton.HeaderText = "View Doc"
        dgvButton.Name = "View Doc"
        dgvButton.UseColumnTextForButtonValue = True
        dgvButton.Text = "View"
        GvDocumentUpload.Columns.Add(dgvButton)
        GvDocumentUpload.Columns(0).Visible = False





    End Sub

    Private Sub btnSaveFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFollowup.Click
        Try

            If btnSaveFollowup.Text = "Add" Then
                linq_obj.SP_Insert_Update_Service_Follow_Up_Master(0, Pk_Address_ID, dtFollowDate.Text, dtNFDate.Text, txtFolloupDetail.Text, txtFollowstatus.Text, txtFollowpBywhoom.Text, txtFollowpProType.Text, txtFollowupRemark.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Add Sucessfully...")
            Else
                linq_obj.SP_Insert_Update_Service_Follow_Up_Master(Pk_Service_Followp, Pk_Address_ID, dtFollowDate.Text, dtNFDate.Text, txtFolloupDetail.Text, txtFollowstatus.Text, txtFollowpBywhoom.Text, txtFollowpProType.Text, txtFollowupRemark.Text)
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
        Pk_Service_Followp = GvServiceFollowp.SelectedCells(0).Value
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
        dtFollowDate.Value = DateTime.Now
        Pk_Service_Followp = 0

        txtECRemark.Text = ""
        DtEcDate.Text = DateTime.Now



    End Sub

    Private Sub txtDaysAfter_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDaysAfter.Leave




        If txtDaysAfter.Text <> "" Then
            dtNFDate.Value = dtFollowDate.Value.Date.AddDays(txtDaysAfter.Text)
        End If
    End Sub

    Private Sub btnTodayFollowp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTodayFollowp.Click

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")
        dt.Columns.Add("FStatus")
        Dim data = linq_obj.SP_Get_Service_TodayFollowp_By_UserID(Class1.global.UserID, dtStart.Value.Date, dtEnd.Value.Date).ToList()
        For Each item As SP_Get_Service_TodayFollowp_By_UserIDResult In data
            dt.Rows.Add(item.Pk_AddressID, item.EnqNo, item.Name, item.FStatus)
        Next
        GvPartyList.DataSource = dt
        GvColor_bind()
        txtTotal.Text = Convert.ToString(data.Count)
    End Sub
    Public Sub GvColor_bind()

        'Bind Color for followp data is 0
        If GvPartyList.RowCount > 0 Then
            ''bind Color
            For index = 0 To GvPartyList.RowCount - 1
                If (GvPartyList.Rows(index).Cells(3).Value > 0) Then
                    GvPartyList.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.DarkSlateGray
                    GvPartyList.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.Yellow
                End If
            Next
            GvPartyList.Columns(0).Visible = False
            GvPartyList.Columns(3).Visible = False
        End If
    End Sub

    Private Sub btnTodayAllotment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTodayAllotment.Click
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")
        dt.Columns.Add("FStatus")
        Dim data = linq_obj.SP_Get_Service_Party_TodayAllotment_ByDate_UserId(Class1.global.UserID, dtStart.Value.Date, dtEnd.Value.Date).ToList()
        For Each item As SP_Get_Service_Party_TodayAllotment_ByDate_UserIdResult In data
            dt.Rows.Add(item.Pk_AddressID, item.EnqNo, item.Name, item.FStatus)
        Next
        GvPartyList.DataSource = dt
        GvColor_bind()

        txtTotal.Text = Convert.ToString(data.Count)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GvPartyList_Bind()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click


        GvPartyList_Bind()



    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

            linq_obj.SP_UpdateAddress(txtName.Text, txtaddress.Text, txtstation.Text, txtCity.Text, txtPincode.Text, txtTaluka.Text, txtdistrict.Text, txtstate.Text, txtDelAddress.Text, txtDelStation.Text, txtDelCity.Text, txtDelPincode.Text, txtDelTaluka.Text, txtDelDistrict.Text, txtDelState.Text, Pk_Address_ID)
            linq_obj.SP_UpdateAddressContactDetail(txtcoperson.Text, txtmobileNo.Text, txtEmailID.Text, txtvalue1.Text, txtValue2.Text, Pk_Address_ID)

            linq_obj.SubmitChanges()
            MessageBox.Show("Update Sucessfully...")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click

        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\ServiceUploadDoc\") + openFileDialog1.SafeFileName
        txtDocPath.Text = imgSrc


    End Sub

    Private Sub btnDocAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocAdd.Click
        Try

            If (btnDocAdd.Text = "Add") Then
                linq_obj.SP_Insert_Update_Service_Document_Upload(0, Pk_Address_ID, txtDocumentName.Text, txtDocPath.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Add Sucessfully..")
            Else
                linq_obj.SP_Insert_Update_Service_Document_Upload(Pk_Service_Doc_ID, Pk_Address_ID, txtDocumentName.Text, txtDocPath.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully..")
            End If
            GvService_documet_Bind()
            Clear_Document_Text()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Clear_Document_Text()
        txtDocumentName.Text = ""
        txtDocPath.Text = ""
        btnDocAdd.Text = "Add"
        Pk_Service_Doc_ID = 0
    End Sub

    Private Sub GvDocumentUpload_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvDocumentUpload.DoubleClick
        btnDocAdd.Text = "Update"
        Pk_Service_Doc_ID = GvDocumentUpload.SelectedCells(0).Value
        txtDocumentName.Text = GvDocumentUpload.SelectedCells(1).Value
        txtDocPath.Text = GvDocumentUpload.SelectedCells(2).Value
    End Sub

    Private Sub GvDocumentUpload_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvDocumentUpload.CellContentClick
        If (e.ColumnIndex = 4) Then
            ' MessageBox.Show(GvDocumentUpload.Rows(e.RowIndex).Cells(2).Value.ToString())

            Class1.global.imagepath = GvDocumentUpload.Rows(e.RowIndex).Cells(2).Value.ToString()
            Class1.global.imageName = GvDocumentUpload.Rows(e.RowIndex).Cells(1).Value.ToString()
            Process.Start(Class1.global.imagepath)

            'Dim img = New Imagepreview
            'img.ShowDialog()



        End If

    End Sub

    Private Sub btnEcUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEcUpdate.Click
        Try

            linq_obj.SP_insert_Update_Service_ECDetails_Status(Pk_Address_ID, Class1.global.UserID, ddlECSStatus.Text, txtECRemark.Text, DtEcDate.Value)
            linq_obj.SubmitChanges()
            MessageBox.Show("Update Sucessfully")
        Catch ex As Exception

        End Try
    End Sub


    Public Sub Pipeline_Milestone_Bind()
        Gv_Project_Log.DataSource = Nothing
        ddlMileStone.DataSource = Nothing
        ChkEngineerList.Items.Clear()
        chkMachineList.Items.Clear()
        Dim DepartmentID As Integer
        Dim ProjectType As Integer
        DepartmentID = 0
        ProjectType = 0
        If rblpipeline_ec.Checked = True Then
            DepartmentID = 3 'Service
            ProjectType = 8  'EC 
        Else
            DepartmentID = 3 'Service
            ProjectType = 9 'Service  

        End If
        'Steps Bind 
        Dim dtSteps As New DataTable
        dtSteps.Columns.Add("Pk_Milestone_ID")
        dtSteps.Columns.Add("Steps_Name")
        'Pipeline Deaprtment 2 =Service 
        'Pipeline Project Type 8 = EC
        Dim dataMilestone = linq_obj.SP_Get_Pipline_Milestone_By_Address_ID(DepartmentID, ProjectType, Pk_Address_ID).ToList()
        For Each item As SP_Get_Pipline_Milestone_By_Address_IDResult In dataMilestone

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

    Public Sub Pipeline_Machine_Engineer_Bind()

        Dim DepartmentID As Integer
        Dim ProjectType As Integer
        DepartmentID = 0
        ProjectType = 0
        If rblpipeline_ec.Checked = True Then
            DepartmentID = 3 'Service
            ProjectType = 8  'EC 
        Else
            DepartmentID = 3 'Service
            ProjectType = 9 'Service  

        End If
        'Machine Name
        'Pipeline Deaprtment 2 =Service 
        'Pipeline Project Type 8 = EC
        Dim dataMachineName = linq_obj.SP_Get_Pipline_Milestone_By_Address_ID(DepartmentID, ProjectType, Pk_Address_ID).ToList().Where(Function(p) p.Pk_Milestone_ID = Convert.ToInt32(ddlMileStone.SelectedValue)).ToList()
        Dim dtMachine As New DataTable
        dtMachine.Columns.Add("MachineName")
        For Each item As SP_Get_Pipline_Milestone_By_Address_IDResult In dataMachineName
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
        For Each item As SP_Get_Pipline_Milestone_By_Address_IDResult In dataMachineName
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

    Private Sub ddlMileStone_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMileStone.SelectionChangeCommitted
        Pipeline_Machine_Engineer_Bind()
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


            If MachineName.Trim() <> "" And EngineerID.Trim() <> "" Then

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