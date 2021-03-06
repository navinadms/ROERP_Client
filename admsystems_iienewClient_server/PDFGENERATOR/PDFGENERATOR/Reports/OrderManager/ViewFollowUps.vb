﻿Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames
Public Class ViewFollowUps
    Dim strCriteria As String
    Dim dataTable As DataTable
    Dim ds As New DataSet
    Dim count As Integer

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
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


        If (cmbType.Text = "Common") Then
            If (cmbFollow.Text = "Today Call") Then
                dgFollowDetail.DataSource = Nothing
                Dim FollowData = linq_obj.SP_Get_Order_FollowDetailsByCriteria(txtUser.Text, dtStartDate.Value, dtEndDate.Value).ToList()
                If (FollowData.Count > 0) Then
                    dgFollowDetail.DataSource = FollowData
                    dgFollowDetail.Columns(dgFollowDetail.Columns.Count - 1).Visible = False
                  
                Else
                    dgFollowDetail.DataSource = FollowData
                    MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else

                dgFollowDetail.DataSource = Nothing

                Dim FollowData1 = linq_obj.SP_Get_Order_FollowDetailsByCriteriaForDaily(txtUser.Text, dtStartDate.Value, dtEndDate.Value).ToList()
                If (FollowData1.Count > 0) Then
                    dgFollowDetail.DataSource = FollowData1
                    dgFollowDetail.Columns(dgFollowDetail.Columns.Count - 1).Visible = False

                Else
                    dgFollowDetail.DataSource = FollowData1
                    MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Else
            If (cmbFollow.Text = "Today Call") Then
                dgFollowDetail.DataSource = Nothing
                Dim FollowData = linq_obj.SP_Get_ISI_FollowDetailsByCriteria(txtUser.Text, dtStartDate.Value, dtEndDate.Value).ToList()
                If (FollowData.Count > 0) Then
                    dgFollowDetail.DataSource = FollowData
                    dgFollowDetail.Columns(dgFollowDetail.Columns.Count - 1).Visible = False
                    ' Dim countToday = linq_obj.GetFollowUpCountToday(txtUser.Text, dtStartDate.Value, dtEndDate.Value).ToList()
                    ' dgFollowCounter.DataSource = countToday

                Else
                    dgFollowDetail.DataSource = FollowData
                    MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else

                dgFollowDetail.DataSource = Nothing

                Dim FollowData1 = linq_obj.SP_Get_ISI_FollowDetailsByCriteriaForDaily(txtUser.Text, dtStartDate.Value, dtEndDate.Value).ToList()
                If (FollowData1.Count > 0) Then
                    dgFollowDetail.DataSource = FollowData1
                    dgFollowDetail.Columns(dgFollowDetail.Columns.Count - 1).Visible = False


                    ' Dim countDaily = linq_obj.GetFollowUpCountDaily(txtUser.Text, dtStartDate.Value, dtEndDate.Value).ToList()
                    ' dgFollowCounter.DataSource = countDaily

                Else
                    dgFollowDetail.DataSource = FollowData1
                    MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim rpt As New rptFollowUpByUser
            rpt.SetDataSource(dgFollowDetail.DataSource)
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
            For i As Integer = 1 To dgFollowDetail.Columns.Count - 1
                worksheet.Cells(1, i) = dgFollowDetail.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To dgFollowDetail.Rows.Count - 1
                For j As Integer = 0 To dgFollowDetail.Columns.Count - 2
                    worksheet.Cells(i + 2, j + 1) = dgFollowDetail.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next


            ' save the application
            workbook.SaveAs("d:\output(" & count & ").xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _
             Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

            ' Exit from the application
            ' app.Quit();
            ' storing Each row and column value to excel sheet
            MessageBox.Show("Excel Download SucessFully on D:\output(" & count & ").xls ")
        Catch generatedExceptionName As Exception
        End Try

    End Sub

    Private Sub btnExportPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPDF.Click
        Try
            Dim rpt As New rpt_inquiryReport
            rpt.SetDataSource(dgFollowDetail.DataSource)

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "DEF.xls")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class