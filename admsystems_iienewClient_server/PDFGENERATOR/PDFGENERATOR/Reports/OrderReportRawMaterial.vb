Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine.ReportDocument
Public Class OrderReportRawMaterial
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim dataAddress As DataTable
    Dim count As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            Dim criteria As String
            criteria = ""

            ''With Order Date
            If (rblOrderDate.Checked = True) Then
                If TxtFromDate.Text = "" Or TxtToDate.Text = "" Then
                    MessageBox.Show("Pls Enter From  date and To ")
                    Exit Sub
                End If
                criteria = criteria + " isnull(row_M.OrderDate,'')!='1900-01-01 00:00:00.000' and isnull(row_M.OrderDate,'') > = '" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "' and isnull(row_M.OrderDate,'')<='" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "' and "
            End If
            '' With Dispatch Date
            If (rblDispDate.Checked = True) Then
                criteria = criteria + " isnull(row_M.DisDate,'')!='1900-01-01 00:00:00.000' and isnull(row_M.DisDate,'') > = '" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "' and isnull(row_M.DisDate,'')<='" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "' and "
            End If
            '' With Tentive Date
            If (rblDispDate.Checked = True) Then
                criteria = criteria + " isnull(row_M.TenDate,'')!='1900-01-01 00:00:00.000' and isnull(row_M.TenDate,'') > = '" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "' and isnull(row_M.TenDate,'')<='" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "' and "
            End If

            If criteria = " and " Then
                criteria = ""
            End If
            If (criteria.Length > 0) Then
                criteria = criteria.ToString().Trim().Substring(0, criteria.Trim().Length - 3)
            End If
            Dim cmd As New SqlCommand
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter
            Dim str As String
            str = System.Configuration.ConfigurationManager.AppSettings("constr").ToString()
            Dim con As New SqlConnection(str)
            Dim Query As String
            Query = " select  addr.Name,addr.City as station,row_M.OrderDate,row_M.DisDate,row_M.TenDate,row_M.ItemName,row_M.Qty from dbo.Address_Master as addr"
            Query += " inner join Tbl_OrderOneMaster as ord_M on ord_M.Fk_AddressId=addr.Pk_AddressID "
            Query += " inner join  dbo.Tbl_OrderRawMaterialDetail AS row_M on addr.Pk_AddressID=row_M.Fk_AddressId where addr.EnqStatus=1 "
            If (criteria.Trim().Length > 0) Then
                Query += " and " + criteria + " order by addr.Name asc "

            End If
            da = New SqlDataAdapter(Query, con)
            ds = New DataSet()
            da.Fill(ds)
            If ds.Tables(0).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GvRawMaterial.DataSource = Nothing
            Else
                GvRawMaterial.DataSource = ds.Tables(0)
            End If
            lblTotalCount.Text = "Total:" + ds.Tables(0).Rows.Count.ToString()
            ds.Dispose()
            da.Dispose()
            If GvRawMaterial.Rows.Count <> 0 Then
                For i As Integer = 0 To 2
                    GenerateUniqueData(i)
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GenerateUniqueData(ByVal cellno As Integer)
        'Logic for unique names

        'Step 1:

        Dim initialnamevalue As String = GvRawMaterial.Rows(0).Cells(cellno).Value.ToString()

        'Step 2:        

        For i As Integer = 1 To GvRawMaterial.Rows.Count - 1

            If GvRawMaterial.Rows(i).Cells(cellno).Value.ToString() = initialnamevalue.ToString() Then
                GvRawMaterial.Rows(i).Cells(cellno).Value = String.Empty
            Else
                initialnamevalue = GvRawMaterial.Rows(i).Cells(cellno).Value.ToString()
            End If
        Next
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
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
            For i As Integer = 1 To GvRawMaterial.Columns.Count
                worksheet.Cells(1, i) = GvRawMaterial.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvRawMaterial.Rows.Count - 1
                For j As Integer = 0 To GvRawMaterial.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvRawMaterial.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next

        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub

    Private Sub TxtFromDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFromDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub


    Private Sub TxtToDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtToDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub


    Private Sub TxtFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtFromDate.Validating
        If Class1.ChkVaildDate(TxtFromDate.Text) = False Then
            MessageBox.Show("Date Format is not valid")
            e.Cancel = True
        End If
    End Sub

    Private Sub TxtToDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtToDate.TextChanged

    End Sub

    Private Sub TxtToDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtToDate.Validating
        If Class1.ChkVaildDate(TxtToDate.Text) = False Then
            MessageBox.Show("Date Format is not valid")
            e.Cancel = True
        End If
    End Sub

    Private Sub rblNotDisp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblDispDate.CheckedChanged
       
    End Sub

    Private Sub rblNotDisp_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblDispDate.Enter

    End Sub
End Class