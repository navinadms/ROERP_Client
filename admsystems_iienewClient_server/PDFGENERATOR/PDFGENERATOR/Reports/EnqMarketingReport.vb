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

Public Class EnqMarketingReport
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PK_Service_Request_ID As Integer
    Dim Pk_Address_ID As Integer
    Dim Pk_Service_Complain_Followp_ID As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ddlEnqMasterList_Data()
        bindUser()
        If (Class1.global.UserAllotType = "Head") Then
            cmbUser.Enabled = True

        Else
            cmbUser.Enabled = False
            cmbUser.Text = Class1.global.User
        End If
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
        If rblSearchPending.Checked = True Then
            searchMarketingtatus = "Pending"
        ElseIf rblSearchDone.Checked = True Then
            searchMarketingtatus = "Done"
        ElseIf rblSearchCancel.Checked = True Then
            searchMarketingtatus = "Cancel"
        Else
            searchMarketingtatus = ""
        End If


        Dim criteria As String
        criteria = "and "

        If searchMarketingtatus <> "" Then
            criteria = criteria + " EM.Enq_Status like '%" + searchMarketingtatus + "%' and "
        End If
        If ddlMarketing.Text <> "All" Then
            criteria = criteria + " EML.MarketingName like '%" + ddlMarketing.Text + "%' and "
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
        cmd.CommandText = "SP_Get_Enq_Marketing_Report"
        cmd.Parameters.AddWithValue("@Pk_UserId", Convert.ToInt32(cmbUser.SelectedValue))
        cmd.Parameters.AddWithValue("@Criteria", criteria)
        
        'If chkIsDate.Checked = False Then
        '    cmd.Parameters.AddWithValue("@StartDate", "01/01/2001")
        'Else
        '    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dtFromDate.Value.ToString("dd/MM/yyyy")

        'End If
        'cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dtToDate.Value.ToString("dd/MM/yyyy")
        'End 
        cmd.CommandTimeout = 3000
        Dim objclass As New Class1
        Dim ds As New DataSet
        Dim PriceTotal As Decimal
        PriceTotal = 0.0
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvEnqMarkRp.DataSource = Nothing
            lblTotalPrice.Text = PriceTotal.ToString()
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else

            For index = 0 To ds.Tables(1).Rows.Count - 1

                PriceTotal = PriceTotal + Convert.ToDecimal(ds.Tables(1).Rows(index)("Price"))

            Next

            GvEnqMarkRp.DataSource = ds.Tables(1)
            lblTotalPrice.Text = PriceTotal.ToString()

        End If




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
            worksheet.Name = ddlMarketing.Text


            ' storing header part in Excel
            For i As Integer = 1 To GvEnqMarkRp.Columns.Count
                worksheet.Cells(1, i) = GvEnqMarkRp.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvEnqMarkRp.Rows.Count - 1
                For j As Integer = 0 To GvEnqMarkRp.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvEnqMarkRp.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next
            MessageBox.Show("Export to Excel Sucessfully...")
        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GvEnqMarketingReport_List()
    End Sub
End Class