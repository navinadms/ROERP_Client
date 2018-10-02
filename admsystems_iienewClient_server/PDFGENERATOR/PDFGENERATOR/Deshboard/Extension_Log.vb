Imports System.Data.SqlClient

Public Class Extension_Log
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim count As Integer

    Dim ds As New DataSet
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

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            
            Dim cmd As New SqlCommand
            cmd.CommandTimeout = 3000
            cmd.CommandText = "SP_Get_Clock_In_Out_Log_Report"
            cmd.Parameters.Add("@StartDate ", SqlDbType.DateTime).Value = dtStartDate.Value.Date
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dtEndDate.Value.Date
            cmd.Parameters.Add("@Fk_User_ID", SqlDbType.Int).Value = cmbUser.SelectedValue
            Dim ds As New DataSet

            Dim objclass As New Class1
            ds = objclass.GetEnqReportData(cmd)
            If ds.Tables(1).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GvExtension.DataSource = Nothing
            Else
                GvExtension.DataSource = ds.Tables(1)
                GvExtension.Columns(0).Width = 30

            End If

        Catch ex As Exception

        End Try
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
            For i As Integer = 1 To GvExtension.Columns.Count
                worksheet.Cells(1, i) = GvExtension.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvExtension.Rows.Count - 1
                For j As Integer = 0 To GvExtension.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvExtension.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next


            MessageBox.Show("Export to Excel Sucessfully...")


        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub
End Class