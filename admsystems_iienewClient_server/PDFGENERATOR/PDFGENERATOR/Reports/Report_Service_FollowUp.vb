﻿Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames

Public Class Report_Service_FollowUp
    Dim strCriteria As String
    Dim dataTable As DataTable
    Dim ds As New DataSet
    Dim count As Integer

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        If (Class1.global.RptUserType = True) Then
            grpUser.Visible = True
        Else
            grpUser.Visible = False
        End If
        ' ddlEnqType_Bind()
    End Sub
    Public Sub AutoCompated_Text()
        Dim GetUser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In GetUser
            txtUser.AutoCompleteCustomSource.Add(item.UserName)
        Next
    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As Date?
        dt = dtStartDate.Value
        linq_obj.CommandTimeout = 3000
        If (cmbFollow.Text = "Today Call") Then
            dgServiceFollowDetail.DataSource = Nothing
            Dim FollowData = linq_obj.SP_Get_Service_TodayFollowpList(If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User), dtStartDate.Value.Date, dtEndDate.Value.Date).ToList()
            If (FollowData.Count > 0) Then
                dgServiceFollowDetail.DataSource = FollowData
                dgServiceFollowDetail.Columns(dgServiceFollowDetail.Columns.Count - 1).Visible = False

                'Count

                Dim countToday = linq_obj.SP_Get_Service_TodayFollowpListCount(If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User), dtStartDate.Value.Date, dtEndDate.Value.Date).ToList()

                If (countToday.Count > 0) Then
                    dgFollowCounter.DataSource = countToday
                Else
                    dgFollowCounter.DataSource = Nothing
                End If
            Else
                dgServiceFollowDetail.DataSource = Nothing
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else

            dgServiceFollowDetail.DataSource = Nothing
            Dim FollowData1 = linq_obj.SP_Get_Service_TodayCallFollowpList(If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User), dtStartDate.Value.Date, dtEndDate.Value.Date).ToList()
            If (FollowData1.Count > 0) Then
                dgServiceFollowDetail.DataSource = FollowData1
                dgServiceFollowDetail.Columns(dgServiceFollowDetail.Columns.Count - 1).Visible = False

                'Count

                Dim countDaily = linq_obj.SP_Get_Service_TodayCallFollowpListCount(If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User), dtStartDate.Value.Date, dtEndDate.Value.Date).ToList()
                If (countDaily.Count > 0) Then
                    dgFollowCounter.DataSource = countDaily
                Else
                    dgFollowCounter.DataSource = Nothing
                End If
            Else
                dgServiceFollowDetail.DataSource = Nothing
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        txtUser.Text = ""
        ''cmbFollow.SelectedIndex = 0


    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim rpt As New rptFollowUpByUser
            rpt.SetDataSource(dgServiceFollowDetail.DataSource)
            rpt.PrintToPrinter(1, False, 0, 1)
            rpt.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
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
            For i As Integer = 1 To dgServiceFollowDetail.Columns.Count - 1
                worksheet.Cells(1, i) = dgServiceFollowDetail.Columns(i - 1).HeaderText
            Next
            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To dgServiceFollowDetail.Rows.Count - 1
                For j As Integer = 0 To dgServiceFollowDetail.Columns.Count - 2
                    worksheet.Cells(i + 2, j + 1) = dgServiceFollowDetail.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next

            ' storing header part in Excel
            For i As Integer = 1 To dgFollowCounter.Columns.Count
                worksheet.Cells(dgServiceFollowDetail.Rows.Count + 3, i) = dgFollowCounter.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To dgFollowCounter.Rows.Count
                For j As Integer = 0 To dgFollowCounter.Columns.Count - 1
                    worksheet.Cells(dgServiceFollowDetail.Rows.Count + i + 4, j + 1) = dgFollowCounter.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next

            ' save the application
            'workbook.SaveAs("d:\output(" & count & ").xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _
            ' Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

            '' Exit from the application
            '' app.Quit();
            '' storing Each row and column value to excel sheet
            'MessageBox.Show("Excel Download SucessFully on D:\output(" & count & ").xls ")
        Catch generatedExceptionName As Exception
        End Try

    End Sub

    Private Sub btnExportPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPDF.Click
        Try
            Dim rpt As New rpt_inquiryReport
            rpt.SetDataSource(dgServiceFollowDetail.DataSource)

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "DEF.xls")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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
                    '  Dim tmpstr1 As [String]() = New [String](2) {}
                    'tmpstr1 = tmpstr(index).Split("+")
                    strfinal += tmpstr(index) + ","
                End If
            Next
            strfinal = strfinal.ToString().Substring(0, strfinal.Length - 1)
            ' strfinal += ""
        End If

        Return strfinal
    End Function

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        txtAllUser.Text += txtUser.Text.Trim() + ","
    End Sub
End Class