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

Public Class ServiceECStatusReport
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PK_Service_Request_ID As Integer
    Dim Pk_Address_ID As Integer
    Dim Pk_Service_Complain_Followp_ID As Integer
    Dim count As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindUser()
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
    Public Sub GvEnqMarketingReport_List()

        'Dim dt As New DataTable
        'dt.Columns.Add("ID")
        'dt.Columns.Add("CompanyName")
        'dt.Columns.Add("AttandBy")
        Dim searchMarketingtatus As String
        searchMarketingtatus = ""
        'If rblSearchPending.Checked = True Then
        '    searchMarketingtatus = "Pending"
        'ElseIf rblSearchDone.Checked = True Then
        '    searchMarketingtatus = "Done"
        'ElseIf rblSearchCancel.Checked = True Then
        '    searchMarketingtatus = "Cancel"
        'Else
        '    searchMarketingtatus = ""
        'End If
        Dim criteria As String
        criteria = "and "

        If ddlECStatus.Text <> "" Then
            criteria = criteria + " ES.EC_Status in (SELECT value FROM dbo.fn_Split('" + SplitString(txtECall.Text.Trim()) + "',',')) and"

        End If
        If cmbUser.Text <> "All" Then
            criteria = criteria + " UM.UserName like '%" + cmbUser.Text + "%' and "
        End If
        If criteria = "and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.Trim().ToString().Substring(0, criteria.Trim().Length - 3)
        End If
        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Service_EC_Status_Criteria"
        cmd.Parameters.AddWithValue("@Pk_UserId", Convert.ToInt32(cmbUser.SelectedValue))
        cmd.Parameters.AddWithValue("@Criteria", criteria)

        If (chkIsDate.Checked = True) Then
            cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = dtFromDate.Value
        Else
            cmd.Parameters.AddWithValue("@start", "01/01/2001")

        End If
        cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = dtToDate.Value

        cmd.CommandTimeout = 3000
        Dim objclass As New Class1
        Dim ds As New DataSet
        Dim PriceTotal As Decimal
        PriceTotal = 0.0
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvECStatus.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            GvECStatus.DataSource = ds.Tables(1)

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
        GvEnqMarketingReport_List()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If ddlECStatus.Text <> "" Then
            txtECall.Text += ddlECStatus.Text.Trim() + ",".Trim()

        End If
    End Sub

    Private Sub txtCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCancel.Click
        txtECall.Text = ""

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
            For i As Integer = 1 To GvECStatus.Columns.Count
                worksheet.Cells(1, i) = GvECStatus.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvECStatus.Rows.Count - 1
                For j As Integer = 0 To GvECStatus.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvECStatus.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next



        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try

    End Sub
End Class