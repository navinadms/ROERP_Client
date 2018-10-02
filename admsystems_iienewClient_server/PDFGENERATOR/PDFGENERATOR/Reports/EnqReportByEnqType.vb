
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
Public Class EnqReportByEnqType
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim dataAddress As DataTable
    Dim count As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        EnqType_Text()

    End Sub
    Public Sub EnqType_Text()

        txtNewStatus.AutoCompleteCustomSource.Clear()
        Dim enqStatusData = linq_obj.SP_Get_EnqTypeAll().ToList()
        For Each itemenq As SP_Get_EnqTypeAllResult In enqStatusData

            txtNewStatus.AutoCompleteCustomSource.Add(itemenq.SubCategory)
        Next


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
        If (txtNewStatusAll.Text.Trim() <> "") Then

            Dim cmd As New SqlCommand
            Dim ds As New DataSet
            Dim da As SqlDataAdapter
            Dim str As String
            str = System.Configuration.ConfigurationManager.AppSettings("constr").ToString()
            Dim con As New SqlConnection(str)
            cmd = New SqlCommand("SP_Get_Enq_Report_EnqTypeList", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 3000
            cmd.Parameters.AddWithValue("@StartDate", txtstartDt.Value.Date)
            cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Value.Date)
            cmd.Parameters.AddWithValue("@NewStatus", SplitString(txtNewStatusAll.Text))
            da = New SqlDataAdapter(cmd)
            da.Fill(ds)

            If ds.Tables(0).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GvEnqTypeList.DataSource = Nothing
            Else
                GvEnqTypeList.DataSource = ds.Tables(0)
                GvEnqTypeList.Columns(5).Visible = False
                lblTotalCount.Text = "Total:" + ds.Tables(0).Rows.Count.ToString()
                If GvEnqTypeList.Rows.Count <> 0 Then
                    For i As Integer = 0 To 2
                        GenerateUniqueData(i)
                    Next
                End If
            End If
        Else
            MessageBox.Show("Please Enter New Status...")
        End If
        ''setRowNumber(GvEnqTypeList)

    End Sub
    Private Sub setRowNumber(ByVal dgv As DataGridView)
        For Each row As DataGridViewRow In GvEnqTypeList.Rows
            row.HeaderCell.Value = row.Index + 1
        Next
    End Sub
    Private Sub GenerateUniqueData(ByVal cellno As Integer)
        'Logic for unique names

        'Step 1:
        Dim initialnamevalue As String = GvEnqTypeList.Rows(0).Cells(cellno).Value.ToString()
        'Step 2:        

        For i As Integer = 1 To GvEnqTypeList.Rows.Count - 1

            If GvEnqTypeList.Rows(i).Cells(cellno).Value.ToString() = initialnamevalue.ToString() Then
                GvEnqTypeList.Rows(i).Cells(cellno).Value = String.Empty
            Else
                initialnamevalue = GvEnqTypeList.Rows(i).Cells(cellno).Value.ToString()
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
            For i As Integer = 1 To GvEnqTypeList.Columns.Count
                worksheet.Cells(1, i) = GvEnqTypeList.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvEnqTypeList.Rows.Count - 1
                For j As Integer = 0 To GvEnqTypeList.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvEnqTypeList.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next



        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub

    Private Sub btnAddEnqType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEnqType.Click
        txtNewStatusAll.Text += txtNewStatus.Text + ","
        txtNewStatus.Text = ""

    End Sub
End Class