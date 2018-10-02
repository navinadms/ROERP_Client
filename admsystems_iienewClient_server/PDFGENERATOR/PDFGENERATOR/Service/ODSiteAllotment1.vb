Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.Sql
Imports pdfforge
Imports System.IO
Imports System.IO.File
Imports System.IO.StreamWriter
Imports Microsoft.Office.Interop
Imports System.Security.Principal
Imports System.Management
Imports System.Globalization
Imports System.Threading
Imports System.Data.SqlClient
Public Class ODSiteAllotment1

    Dim PK_Address_ID As Integer
    Dim Pk_ODSite_Master As Integer
    Dim Pk_ODEngineer_ID As Integer

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public Sub New()
        InitializeComponent()


        GvODSiteList_Bind()
        ddlEngineerList_Bind()
    End Sub
    Public Sub GvODSiteList_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")
        Dim SearchType As String
        Dim ODType As String
        If rblSearchService.Checked = True Then
            ODType = "Service"
        ElseIf rblSearchEC.Checked = True Then
            ODType = "EC"
        ElseIf rblSearchVisit.Checked = True Then

            ODType = "Visit"
        Else
            ODType = ""

        End If


        If rblRunningSearch.Checked = True Then
            SearchType = "Running"
        ElseIf rblSearchPending.Checked = True Then
            SearchType = "Pending"
        ElseIf rblSearchSCH.Checked = True Then
            SearchType = "SCH"
        ElseIf rblDoneSearch.Checked = True Then
            SearchType = "Done"
        ElseIf rblCancelSearch.Checked = True Then

            SearchType = "Cancel"
        Else
            SearchType = ""

        End If


        Dim criteria As String
        criteria = "and "
        If txtEnqNoSearch.Text.Trim() <> "" Then
            criteria = criteria + " AM.EnqNo like '%" + txtEnqNoSearch.Text + "%'and "
        End If
        If txtNameSearch.Text.Trim() <> "" Then
            If chkEngineer.Checked = False Then
                criteria = criteria + " AM.Name like '%" + txtNameSearch.Text + "%'and "
            Else
                criteria = criteria + " EngineerName like '%" + txtNameSearch.Text + "%'and "
            End If

        End If
        If ODType.Trim() <> "" Then
            criteria = criteria + " SM.Type like '%" + ODType + "%'and "
        End If
        If SearchType.Trim() <> "" Then
            criteria = criteria + " SM.SiteStatus like '%" + SearchType.Trim() + "%'and "
        End If

        If criteria = "and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.Trim().ToString().Substring(0, criteria.Trim().Length - 3)
        End If



        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Service_ODSite_Master_List_Criteria"
        cmd.Parameters.Add("@Criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
        'End 
        cmd.CommandTimeout = 3000
        Dim objclass As New Class1
        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvODSiteList.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            For i = 0 To ds.Tables(1).Rows.Count - 1
                dt.Rows.Add(ds.Tables(1).Rows(i)("Pk_ODSite_Master"), ds.Tables(1).Rows(i)("EnqNo"), ds.Tables(1).Rows(i)("Name"))

            Next
            GvODSiteList.DataSource = dt



            txtTotal.Text = Convert.ToString(dt.Rows.Count)

        End If





        'If SearchType.Trim <> "" Then
        '    Dim data = linq_obj.SP_Get_Service_ODSite_Master_List().Where(Function(p) Convert.ToString(p.SiteStatus).ToLower() = SearchType.ToLower() And Convert.ToString(p.Type).ToLower() = ODType.ToLower()).ToList()

        '    For Each item As SP_Get_Service_ODSite_Master_ListResult In data
        '        dt.Rows.Add(item.Pk_ODSite_Master, item.EnqNo, item.Name)
        '    Next
        'Else
        '    Dim data = linq_obj.SP_Get_Service_ODSite_Master_List().Where(Function(p) Convert.ToString(p.Type).ToLower() = ODType.ToLower()).ToList()

        '    For Each item As SP_Get_Service_ODSite_Master_ListResult In data
        '        dt.Rows.Add(item.Pk_ODSite_Master, item.EnqNo, item.Name)
        '    Next
        'End If

        'GvODSiteList.DataSource = dt
        'txtTotal.Text = dt.Rows.Count

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
        ddlEngineerList.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ddlEngineerList.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEngineerList.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
   


    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try

            'insert data
            Dim type As String
            If rblType_Service.Checked = True Then
                type = "Service"
            ElseIf rblType_EC.Checked = True Then
                type = "EC"
            Else
                type = "Visit"

            End If
             
            If btnSubmit.Text.Trim() = "Submit" Then
                linq_obj.SP_Insert_Update_Service_ODSite_Master(0, PK_Address_ID, type, txtPlantType.Text, txtNoofDays.Text.Trim(), txtUpTime.Text.Trim(), txtDownTime.Text.Trim(), txtRemark.Text.Trim(), txtStartDate.Text, txtEndDate.Text, ddlSiteStatus.Text, txtCreateDate.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Submit Sucessfully..")
            Else
                linq_obj.SP_Insert_Update_Service_ODSite_Master(Pk_ODSite_Master, PK_Address_ID, type, txtPlantType.Text, txtNoofDays.Text.Trim(), txtUpTime.Text.Trim(), txtDownTime.Text.Trim(), txtRemark.Text.Trim(), txtStartDate.Text, txtEndDate.Text, ddlSiteStatus.Text, txtCreateDate.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully..")
            End If
            Clear_Text()
            GvODSiteList_Bind()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Clear_Text()

        txtNoofDays.Text = ""
        txtUpTime.Text = ""
        txtDownTime.Text = ""
        txtEnqNo.Text = ""
        txtName.Text = ""
        txtStation.Text = ""
        txtRemark.Text = ""
        PK_Address_ID = 0
        Pk_ODEngineer_ID = 0
        Pk_ODSite_Master = 0
        btnAddSiteEngg.Text = "Add"
        btnSubmit.Text = "Submit"
        txtPlantType.Text = ""
        GvSiteEngineerList.DataSource = Nothing
    End Sub

    Private Sub GvODSiteList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvODSiteList.DoubleClick
        Clear_Text()
        btnSubmit.Text = "Update"
        If Me.GvODSiteList.SelectedRows.Count > 0 Then
            Pk_ODSite_Master = Me.GvODSiteList.SelectedCells(0).Value
        End If
        Display_Data()
    End Sub

    Public Sub Display_Data()
        Dim data = linq_obj.SP_Get_Service_ODSite_Master_List().ToList().Where(Function(p) p.Pk_ODSite_Master = Pk_ODSite_Master).ToList()
        For Each item As SP_Get_Service_ODSite_Master_ListResult In data
            PK_Address_ID = item.Fk_AddressID
            txtEnqNo.Text = item.EnqNo
            txtName.Text = item.Name
            txtStation.Text = item.City
            txtNoofDays.Text = item.NoofDays
            txtUpTime.Text = item.UpTime
            txtDownTime.Text = item.DownTime
            txtRemark.Text = item.Remark
            ddlSiteStatus.Text = item.SiteStatus
            txtCreateDate.Text = item.CreateDate
            txtStartDate.Text = item.StartDate
            txtEndDate.Text = item.EndDate
            ddlSiteStatus.Text = item.SiteStatus
            If item.Type = "Service" Then
                rblType_Service.Checked = True
            ElseIf item.Type = "EC" Then
                rblType_EC.Checked = True
            Else
                rblVisit.Checked = True
            End If
            txtPlantType.Text = item.Type
        Next
        Display_Data_OD_Site_Engineer()
    End Sub

    Public Sub Display_Data_OD_Site_Engineer()
        Dim data = linq_obj.SP_Get_Service_ODSite_Engineer_Detail_ByID(Pk_ODSite_Master).ToList()
        GvSiteEngineerList.DataSource = data
        GvSiteEngineerList.Columns(0).Visible = False

        GvSiteEngineerList.Columns(4).Visible = False
        'GvSiteEngineerList.Columns(5).Visible = False

    End Sub

    Private Sub btnAddSiteEngg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSiteEngg.Click
        Try
            'Add Assign Engineer 
            Dim type As String
            If rblType_Service.Checked = True Then
                type = "Service"
            ElseIf rblType_EC.Checked = True Then
                type = "EC"
            Else
                type = "Visit"

            End If
            If Pk_ODSite_Master = 0 Then
                If btnSubmit.Text.Trim() = "Submit" Then
                    Pk_ODSite_Master = linq_obj.SP_Insert_Update_Service_ODSite_Master(0, PK_Address_ID, type, txtPlantType.Text, txtNoofDays.Text.Trim(), txtUpTime.Text.Trim(), txtDownTime.Text.Trim(), txtRemark.Text.Trim(), txtStartDate.Text, txtEndDate.Text, ddlSiteStatus.Text, txtCreateDate.Text)
                    linq_obj.SubmitChanges()
                    btnSubmit.Text = "Update"
                End If
            End If
            If btnAddSiteEngg.Text.Trim() = "Add" Then
                linq_obj.SP_insert_update_Service_ODSite_Engineer_Detail(0, Pk_ODSite_Master, Convert.ToInt32(ddlEngineerList.SelectedValue), ddlEnggStatus.Text, txtpriorityno.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Add Sucessfully..")
            Else
                linq_obj.SP_insert_update_Service_ODSite_Engineer_Detail(Pk_ODEngineer_ID, Pk_ODSite_Master, Convert.ToInt32(ddlEngineerList.SelectedValue), ddlEnggStatus.Text, txtpriorityno.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully..")
            End If
            Display_Data_OD_Site_Engineer()
            btnAddSiteEngg.Text = "Add"

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave

        If txtEnqNo.Text.Trim() <> "" Then
            Dim data = linq_obj.SP_Get_ServiceODSite_Allotment_EnqNo(txtEnqNo.Text.Trim()).ToList()
            If data.Count > 0 Then
                For Each item As SP_Get_ServiceODSite_Allotment_EnqNoResult In data
                    txtName.Text = item.Name
                    txtStation.Text = item.City
                    PK_Address_ID = item.Pk_AddressID

                Next
            Else
                MessageBox.Show("Invalid EnqNo...")
                Clear_Text()
                txtEnqNo.Focus()

            End If
            'Assign OD Site Engineer List
            Display_Data_OD_Site_Engineer()
        End If
    End Sub

    Private Sub GvSiteEngineerList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSiteEngineerList.DoubleClick
        ddlEngineerList_Bind()
        btnAddSiteEngg.Text = "Update"
        Pk_ODEngineer_ID = Me.GvSiteEngineerList.SelectedCells(0).Value
        ddlEnggStatus.Text = Me.GvSiteEngineerList.SelectedCells(2).Value
        txtpriorityno.Text = Me.GvSiteEngineerList.SelectedCells(3).Value
        ddlEngineerList.SelectedValue = Me.GvSiteEngineerList.SelectedCells(4).Value
    End Sub

    Private Sub btnDeleteEngineer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteEngineer.Click

        Dim result As DialogResult = MessageBox.Show("Are You Sure Delete Engineer?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            linq_obj.SP_Delete_Service_ODSite_Engineer_Detail_ID(Me.GvSiteEngineerList.SelectedCells(0).Value)
            linq_obj.SubmitChanges()
            MessageBox.Show("Delete Sucessfully..")
            Display_Data_OD_Site_Engineer()
        End If


    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Clear_Text()
    End Sub

    Private Sub txtNoofDays_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoofDays.Leave
        txtEndDate.Text = txtStartDate.Text
        txtEndDate.Text = txtEndDate.Value.AddDays(txtNoofDays.Text)

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")
        If chkEngineer.Checked = False Then
            If txtEnqNoSearch.Text.Trim() <> "" Then
                Dim data = linq_obj.SP_Get_Service_ODSite_Master_List().ToList().Where(Function(p) p.EnqNo.ToLower() = txtEnqNoSearch.Text.ToLower().Trim()).ToList()
                For Each item As SP_Get_Service_ODSite_Master_ListResult In data
                    dt.Rows.Add(item.Pk_ODSite_Master, item.EnqNo, item.Name)
                Next
            Else
                Dim data = linq_obj.SP_Get_Service_ODSite_Master_List().ToList().Where(Function(p) p.Name.ToLower().StartsWith(txtNameSearch.Text.ToLower().Trim())).ToList()
                For Each item As SP_Get_Service_ODSite_Master_ListResult In data
                    dt.Rows.Add(item.Pk_ODSite_Master, item.EnqNo, item.Name)
                Next
            End If
        Else
            Dim data = linq_obj.SP_Get_Service_ODSite_Master_By_Engineer(txtNameSearch.Text).ToList()
            For Each item As SP_Get_Service_ODSite_Master_By_EngineerResult In data
                dt.Rows.Add(item.Pk_ODSite_Master, item.EnqNo, item.Name)
            Next
        End If

        If dt.Rows.Count > 0 Then
            GvODSiteList.DataSource = dt
            GvODSiteList.Columns(0).Visible = False
        Else
            GvODSiteList.DataSource = Nothing
        End If

    End Sub

    Private Sub btnSearchRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchRefresh.Click
        GvODSiteList_Bind()
        txtEnqNoSearch.Text = ""
        txtNameSearch.Text = ""
    End Sub

    Private Sub rblRunningSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblRunningSearch.CheckedChanged
        GvODSiteList_Bind()
    End Sub

    Private Sub rblDoneSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblDoneSearch.CheckedChanged
        GvODSiteList_Bind()
    End Sub

    Private Sub rblCancelSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblCancelSearch.CheckedChanged
        GvODSiteList_Bind()
    End Sub

    Private Sub rblSearchService_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblSearchService.CheckedChanged
        GvODSiteList_Bind()
    End Sub

    Private Sub rblSearchEC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblSearchEC.CheckedChanged
        GvODSiteList_Bind()
    End Sub

    Private Sub rblSearchVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblSearchVisit.CheckedChanged
        GvODSiteList_Bind()
    End Sub

    Private Sub btnODEngineerReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnODEngineerReport.Click
        Dim frm As New ODEngineeringReport
        frm.Show()

    End Sub

    Private Sub rblSearchAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblSearchAll.CheckedChanged
        GvODSiteList_Bind()
    End Sub

    Private Sub rblSearchtypeAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblSearchtypeAll.CheckedChanged
        GvODSiteList_Bind()
    End Sub
End Class