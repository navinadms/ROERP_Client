
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

Public Class ServiceRequestForm
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PK_Service_Request_ID As Integer


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        txtCreateDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        txtTime.Text = TimeOfDay.ToString("hh:mm tt")
        txtCallAttandBy.Text = Class1.global.User
        GvServiceComplain_List()
    End Sub

    Public Sub GvServiceComplain_List()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("ComplainNo")
        dt.Columns.Add("Status")
        Dim searchComplainStatus As String
        searchComplainStatus = ""
        If rblSearchPending.Checked = True Then
            searchComplainStatus = "Pending"
        ElseIf rblSearchDone.Checked = True Then
            searchComplainStatus = "Done"
        ElseIf rblSearchCancel.Checked = True Then

            searchComplainStatus = "Cancel"

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

        If criteria = "and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.Trim().ToString().Substring(0, criteria.Trim().Length - 3)
        End If
        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Service_Complain_Master_Criteria"
        cmd.Parameters.AddWithValue("@UserID", Class1.global.UserID.ToString())
        cmd.Parameters.Add("@Criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
        cmd.Parameters.AddWithValue("@StartDate", Class1.global.UserID.ToString())


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
                dt.Rows.Add(ds.Tables(1).Rows(i)("PK_Service_Request_ID"), ds.Tables(1).Rows(i)("ComplainNo"), ds.Tables(1).Rows(i)("ComplainStatus"))

            Next
            GvServiceComplain.DataSource = dt
            txtTotal.Text = Convert.ToString(dt.Rows.Count)

        End If
        GvServiceComplain.Columns(0).Visible = False



    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim ComplainStatus As String
            Dim Exp1, Exp2, Exp3 As String
            Exp1 = "No"
            Exp2 = "No"
            Exp3 = "No"


            If chkexp1.Checked = True Then
                Exp1 = "Yes"

            End If

            If chkexp2.Checked = True Then
                Exp2 = "Yes"

            End If
            If chkExp3.Checked = True Then
                Exp3 = "Yes"

            End If

            If rblPendingStatus.Checked = True Then
                ComplainStatus = "Pending"
            ElseIf rblDoneStatus.Checked = True Then
                ComplainStatus = "Done"
            Else
                ComplainStatus = "Cancel"
            End If


            If btnSubmit.Text = "Submit" Then


                linq_obj.SP_Insert_Update_Service_Complain_Master(0,
                                                                  0,
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
                                                                txtServiceReqDetail.Text,
                                                                 Exp1,
                                                                 Exp2,
                                                                 Exp3,
                                                                txtMaterial.Text,
                                                                txtReqLevel.Text,
                                                                txtClientHistory.Text,
                                                                txtRemark.Text,
                                                                txtCreateDate.Text,
                                                                txtTime.Text,
                                                                ComplainStatus)
                linq_obj.SubmitChanges()
                MessageBox.Show("Submit Sucessfully..")
            Else
                linq_obj.SP_Insert_Update_Service_Complain_Master(PK_Service_Request_ID,
                                                               0,
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
                                                             txtServiceReqDetail.Text,
                                                              Exp1,
                                                              Exp2,
                                                              Exp3,
                                                             txtMaterial.Text,
                                                             txtReqLevel.Text,
                                                             txtClientHistory.Text,
                                                             txtRemark.Text,
                                                             txtCreateDate.Text,
                                                             txtTime.Text,
                                                             ComplainStatus)
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully..")
            End If 
        Catch ex As Exception

        End Try
        GvServiceComplain_List()
        Set_Clean()
    End Sub
    Public Sub Set_Clean()

        txtComplainNo.Text = ""
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
        txtServiceReqDetail.Text = ""
        chkexp1.Checked = False
        chkexp2.Checked = False
        chkExp3.Checked = False
        rblPendingStatus.Checked = True
        txtMaterial.Text = ""
        txtReqLevel.Text = ""
        txtClientHistory.Text = ""
        txtRemark.Text = ""
        txtCreateDate.Text = ""
        txtTime.Text = ""
        btnSubmit.Text = "Submit"
        txtCreateDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        txtTime.Text = TimeOfDay.ToString("hh:mm tt")
        txtCallAttandBy.Text = Class1.global.User
    End Sub

    Private Sub GvServiceComplain_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvServiceComplain.DoubleClick
        PK_Service_Request_ID = Me.GvServiceComplain.SelectedCells(0).Value
        btnSubmit.Text = "Update"
        GvComplain_Display()
    End Sub
    Public Sub GvComplain_Display()

        Dim data = linq_obj.SP_Get_Service_Complain_Master_List(Class1.global.UserID).ToList().Where(Function(p) p.PK_Service_Request_ID = PK_Service_Request_ID).ToList()


        For Each item As SP_Get_Service_Complain_Master_ListResult In data
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
            ddlMachineType.Text = item.MachineType
            txtCapacity.Text = item.Capacity
            txtServiceReqDetail.Text = item.ServiceReqDetail

            chkexp1.Checked = False
            chkexp2.Checked = False
            chkExp3.Checked = False

            If item.Exp1 = "Yes" Then
                chkexp1.Checked = True
            End If
            If item.Exp2 = "Yes" Then
                chkexp2.Checked = True
            End If
            If item.Exp3 = "Yes" Then
                chkExp3.Checked = True
            End If
            txtMaterial.Text = item.SP_Material
            txtReqLevel.Text = item.Req_Level
            txtClientHistory.Text = item.ClientHistory
            txtRemark.Text = item.Remarks
            txtCreateDate.Text = item.CreateDate
            txtTime.Text = item.Time

            If item.ComplainStatus = "Pending" Then
                rblPendingStatus.Checked = True
            ElseIf item.ComplainStatus = "Done" Then
                rblDoneStatus.Checked = True
            Else
                rblCancelStatus.Checked = True
            End If
        Next


    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GvServiceComplain_List()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        rblSearchNone.Checked = True
        txtSearchComp.Text = ""
        txtSearchCompany.Text = ""
        GvServiceComplain_List()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Set_Clean()
        GvServiceComplain_List()
    End Sub
End Class
