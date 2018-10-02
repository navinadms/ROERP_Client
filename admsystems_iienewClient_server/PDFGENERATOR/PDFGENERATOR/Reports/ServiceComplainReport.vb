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

Public Class ServiceComplainReport
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PK_Service_Request_ID As Integer
    Dim Pk_Address_ID As Integer
    Dim Pk_Service_Complain_Followp_ID As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindUser()
    End Sub
    Public Sub GvServiceComplain_List()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("CompanyName")
        dt.Columns.Add("AttandBy")
        Dim ComplainType As String
        ComplainType = ""

        If rblServiceComplain.Checked = True Then
            ComplainType = "Service"
        ElseIf rblECComplain.Checked = True Then
            ComplainType = "EC"
        Else
            ComplainType = "All"
        End If

        Dim criteria As String
        criteria = "and "

        If ComplainType <> "All" Then
            criteria = criteria + " ComplainType like '%" + ComplainType + "%' and "
        End If

        If txtAllUser.Text.Trim() <> "" Then
            criteria = criteria + " ServiceAttBy in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllUser.Text.Trim()) + "',',')) and"
        End If
        If txtAllMachine.Text.Trim() <> "" Then
            criteria = criteria + " SCD.MachineType  in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllMachine.Text.Trim()) + "',',')) and"
        End If
        If txtAllLevel.Text.Trim() <> "" Then
            criteria = criteria + " Req_Level in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllLevel.Text.Trim()) + "',',')) and"
        End If

        If txtALLSSR.Text.Trim() <> "" Then
            criteria = criteria + " ComplainStatus in (SELECT value FROM dbo.fn_Split('" + SplitString(txtALLSSR.Text.Trim()) + "',',')) and"
        End If

        If criteria = "and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.Trim().ToString().Substring(0, criteria.Trim().Length - 3)
        End If

        Dim cmd As New SqlCommand

        If chkIsDoneDate.Checked = True Then
            'Service  SSR Status
            cmd.CommandText = "SP_Get_Service_Complain_Master_FullDetail_Criteria_Report"
            cmd.Parameters.AddWithValue("@UserID", Class1.global.UserID.ToString())
            cmd.Parameters.AddWithValue("@ComplainStatus", SplitString(txtALLSSR.Text.Trim()))
            cmd.Parameters.AddWithValue("@ComplainType", ComplainType.Trim().ToString().ToLower())
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dtFromDate.Value.Date
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dtToDate.Value.Date
        Else

            cmd.CommandText = "SP_Get_Service_Complain_Master_Criteria_Report"
            cmd.Parameters.Add("@Criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
            cmd.Parameters.AddWithValue("@UserID", Class1.global.UserID.ToString())
            If chkIsDate.Checked = False Then
                cmd.Parameters.AddWithValue("@StartDate", "01/01/2001")
            Else
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dtFromDate.Value.Date

            End If
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dtToDate.Value.Date


        End If


        'End 
        cmd.CommandTimeout = 3000
        Dim objclass As New Class1
        Dim ds As New DataSet

        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvServiceComplainReport.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else

            GvServiceComplainReport.DataSource = ds.Tables(1)
        End If




    End Sub
    Function SplitString(ByVal str As String) As String
        If str.Equals("") Or str.Equals("All") Or str.Equals("All,") Then
            If (str.Equals("All,")) Then
                str = str.ToString().Substring(0, str.Length - 1)
            End If
            Return str
        End If
        Dim strfinal As String = ""
        Dim cnt As Integer
        cnt = str.Split(",").Length
        Dim tmpstr As [String]() = New [String](cnt) {}
        tmpstr = str.Split(",")
        ' strfinal = ""
        If cnt > 0 Then
            For index = 0 To tmpstr.Length - 1
                If tmpstr(index) <> "" Then
                    strfinal += tmpstr(index) + ","
                End If
            Next
            strfinal = strfinal.ToString().Substring(0, strfinal.Length - 1)
            ' strfinal += ""
        End If

        Return strfinal
    End Function
    Public Sub bindUser()
        cmbUser.DataSource = Nothing


        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_UserId")
        datatable.Columns.Add("UserName")


        Dim datauser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In datauser
            datatable.Rows.Add(item.Pk_UserId, item.UserName)
        Next
        Dim newRow As DataRow = datatable.NewRow()

        newRow(0) = "0"
        newRow(1) = "All"
        datatable.Rows.InsertAt(newRow, 0)
        cmbUser.DataSource = datatable
        cmbUser.DisplayMember = "UserName"
        cmbUser.ValueMember = "Pk_UserId"
        cmbUser.AutoCompleteMode = AutoCompleteMode.Append
        cmbUser.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GvServiceComplain_List()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim count As Integer
            Dim app As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()

            ' creating new WorkBook within Excel application
            Dim workbook As Microsoft.Office.Interop.Excel._Workbook = app.Workbooks.Add(Type.Missing)

            ' creating new Excelsheet in workbook
            Dim worksheet As Microsoft.Office.Interop.Excel._Worksheet = Nothing

            ' see the excel sheet behind the program
            app.Visible = True

            ' get the reference of first sheet. By default its name is Sheet1.
            ' store its reference to worksheet
            worksheet = workbook.Sheets("Sheet1")
            worksheet = workbook.ActiveSheet
            ' changing the name of active sheet
            worksheet.Name = Me.Name

            ' storing header part in Excel
            For i As Integer = 1 To GvServiceComplainReport.Columns.Count
                worksheet.Cells(1, i) = GvServiceComplainReport.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvServiceComplainReport.Rows.Count - 1
                For j As Integer = 0 To GvServiceComplainReport.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvServiceComplainReport.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next


            MessageBox.Show("Export to Excel Sucessfully...")


        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub

    Private Sub btnCancelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelAll.Click

    End Sub


    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        If cmbUser.Text <> "" Then
            txtAllUser.Text += cmbUser.Text.Trim() + ",".Trim()
        End If
    End Sub

    Private Sub btnAddMachine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMachine.Click
        If ddlMachineType.Text <> "" Then
            txtAllMachine.Text += ddlMachineType.Text.Trim() + ",".Trim()
        End If
    End Sub

    Private Sub btnAddLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLevel.Click
        If ddlReqLevel.Text <> "" Then
            txtAllLevel.Text += ddlReqLevel.Text.Trim() + ",".Trim()

        End If
    End Sub

    Private Sub btnAddSSR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSSR.Click
        If ddlSSR.Text <> "" Then
            txtALLSSR.Text += ddlSSR.Text.Trim() + ",".Trim()

        End If
    End Sub

    Private Sub chkIsDoneDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsDoneDate.CheckedChanged
        btnAddUser.Enabled = True
        btnAddMachine.Enabled = True
        btnAddLevel.Enabled = True

        chkIsDate.Enabled = True
        If chkIsDoneDate.Checked = True Then

            btnAddUser.Enabled = False
            btnAddMachine.Enabled = False
            btnAddLevel.Enabled = False
            chkIsDate.Enabled = False

        End If
    End Sub
End Class