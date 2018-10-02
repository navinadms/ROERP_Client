Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames
Public Class Spare_FollowpReport
    Dim strCriteria As String
    Dim dataTable As DataTable
    Dim ds As New DataSet
    Dim count As Integer

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        If (Class1.global.RptUserType = True) Then
            bindUser()
        End If
    End Sub
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
    Public Sub GenerateReport()
        '  and City in (SELECT value FROM dbo.fn_Split('''+@city+''','',''))'

        Dim Type As Integer

        If cmbFollowptype.Text = "Daily FollowUp" Then
            Type = 0
        Else
            Type = 1

        End If

        Dim criteria As String
        criteria = " and " 
        If txtAllUser.Text.Trim() <> "" Then
            criteria = criteria + " UM.UserName in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllUser.Text.Trim()) + "',',')) and"
        End If
         
        If criteria = " and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Trim().Length - 3)
        End If
        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Spare_Today_Followp_By_Criteria"
        cmd.CommandTimeout = 3000
        cmd.Parameters.Add("@Type", SqlDbType.Int).Value = Type
        cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dtStartDate.Value.Date
        cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dtEndDate.Value.Date
        cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = txtAllUser.Text.Trim()
        Dim objclass As New Class1
        ds = objclass.GetEnqReportData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            GvSpareReportFollowp.DataSource = Nothing
        Else
            GvSpareReportFollowp.DataSource = ds.Tables(1)
            GvSpareReportFollowp.Columns(0).Width = 30
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
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GenerateReport()
    End Sub

    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        txtAllUser.Text += cmbUser.Text + ","
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
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
            For i As Integer = 1 To GvSpareReportFollowp.Columns.Count
                worksheet.Cells(1, i) = GvSpareReportFollowp.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvSpareReportFollowp.Rows.Count - 1
                For j As Integer = 0 To GvSpareReportFollowp.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvSpareReportFollowp.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next
            MessageBox.Show("Download Complated...")
        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try

    End Sub
End Class