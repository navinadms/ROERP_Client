﻿Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient

Public Class Rpt_StockMainReport
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim count As Integer
    Dim dt As DataTable

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim str As String = cmbReportType.Text
        Select Case str
            Case "Inward Detail"
                Dim dataStock = linq_obj.Sp_Rpt_FullStockDetail(dtStartDate.Value, dtEndDate.Value).ToList()
                If (dataStock.Count > 0) Then
                    gvStockDetail.DataSource = dataStock
                    gvStockDetail.Columns(0).Visible = True
                    gvStockDetail.Columns(1).Visible = True
                    gvStockDetail.Columns(2).Visible = True
                    gvStockDetail.Columns(3).Visible = True
                    gvStockDetail.Columns(4).Visible = False
                    gvStockDetail.Columns(5).Visible = False
                    gvStockDetail.Columns(6).Visible = True
                    gvStockDetail.Columns(7).Visible = False
                    gvStockDetail.Columns(8).Visible = False
                    gvStockDetail.Columns(9).Visible = False
                Else
                    gvStockDetail.DataSource = Nothing
                End If
                ' gvStockDetail.Columns(10).Visible = False
            Case "Outward Detail"
                Dim dataStock = linq_obj.Sp_Rpt_FullStockDetail(dtStartDate.Value, dtEndDate.Value).ToList()

                If (dataStock.Count > 0) Then
                    gvStockDetail.DataSource = dataStock
                    gvStockDetail.Columns(0).Visible = True
                    gvStockDetail.Columns(1).Visible = True
                    gvStockDetail.Columns(2).Visible = True
                    gvStockDetail.Columns(3).Visible = False
                    gvStockDetail.Columns(4).Visible = True
                    gvStockDetail.Columns(5).Visible = False
                    gvStockDetail.Columns(6).Visible = False
                    gvStockDetail.Columns(7).Visible = True
                    gvStockDetail.Columns(8).Visible = False
                    gvStockDetail.Columns(9).Visible = False
                Else
                    gvStockDetail.DataSource = Nothing
                End If
                ' gvStockDetail.Columns(10).Visible = False
            Case "Combined Detail"
                Dim dataStock = linq_obj.Sp_Rpt_FullStockDetail(dtStartDate.Value, dtEndDate.Value).ToList()
                If (dataStock.Count > 0) Then
                    gvStockDetail.DataSource = dataStock
                    gvStockDetail.Columns(0).Visible = True
                    gvStockDetail.Columns(1).Visible = True
                    gvStockDetail.Columns(2).Visible = True
                    gvStockDetail.Columns(3).Visible = True
                    gvStockDetail.Columns(4).Visible = True
                    gvStockDetail.Columns(5).Visible = True
                    gvStockDetail.Columns(6).Visible = True
                    gvStockDetail.Columns(7).Visible = True
                    gvStockDetail.Columns(8).Visible = True
                    gvStockDetail.Columns(9).Visible = True
                    '    gvStockDetail.Columns(10).Visible = True
                Else
                    gvStockDetail.DataSource = Nothing
                End If
            Case "General Inward Detail"
                Dim dataStock = linq_obj.Sp_Rpt_FullInwardDetail(dtStartDate.Value, dtEndDate.Value).ToList()

                If (dataStock.Count > 0) Then
                    gvStockDetail.DataSource = dataStock
                    
                Else
                    gvStockDetail.DataSource = Nothing

                End If

            Case "General Outward Detail"
                Dim dataStock = linq_obj.Sp_Rpt_FullOutwardDetail(dtStartDate.Value, dtEndDate.Value).ToList()
                If (dataStock.Count > 0) Then
                    gvStockDetail.DataSource = dataStock
                    
                Else
                    gvStockDetail.DataSource = Nothing

                End If

            Case "SupplierWise Inward Detail"
                Dim dataStock = linq_obj.SP_Get_StockReportBySupply().ToList()
                If (dataStock.Count > 0) Then
                    gvStockDetail.DataSource = dataStock

                Else
                    gvStockDetail.DataSource = Nothing

                End If
            Case Else
                gvStockDetail.DataSource = Nothing


        End Select




    End Sub
    Private Sub GenerateUniqueData1(ByVal cellno As Integer)
        'Logic for unique names

        'Step 1:

        Dim initialnamevalue As String = gvStockDetail.Rows(0).Cells(cellno).Value.ToString()

        'Step 2:       

        For i As Integer = 1 To gvStockDetail.Rows.Count - 1

            If gvStockDetail.Rows(i).Cells(cellno).Value.ToString() = initialnamevalue.ToString() And gvStockDetail.Rows(i).Cells(0).Value.ToString() = gvStockDetail.Rows(0).Cells(cellno).Value.ToString() Then
                gvStockDetail.Rows(i).Cells(cellno).Value = String.Empty
            Else
                initialnamevalue = gvStockDetail.Rows(i).Cells(cellno).Value.ToString()
            End If
        Next
    End Sub
    Private Sub GenerateUniqueData(ByVal cellno As Integer)
        'Logic for unique names

        'Step 1:

        Dim initialnamevalue As String = gvStockDetail.Rows(0).Cells(cellno).Value.ToString()

        'Step 2:       

        For i As Integer = 1 To gvStockDetail.Rows.Count - 1

            If gvStockDetail.Rows(i).Cells(cellno).Value.ToString() = initialnamevalue.ToString() Then
                gvStockDetail.Rows(i).Cells(cellno).Value = String.Empty
            Else
                initialnamevalue = gvStockDetail.Rows(i).Cells(cellno).Value.ToString()
            End If
        Next
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
            For i As Integer = 1 To gvStockDetail.Columns.Count - 1

                worksheet.Cells(1, i) = gvStockDetail.Columns(i - 1).HeaderText

            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To gvStockDetail.Rows.Count - 1
                For j As Integer = 0 To gvStockDetail.Columns.Count - 1
                    If gvStockDetail.Columns(j).Visible = True Then
                        worksheet.Cells(i + 2, j + 1) = gvStockDetail.Rows(i).Cells(j).Value.ToString()
                        count += i + 2 + j + 1
                    End If
                Next
            Next
            ' save the application

        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try


    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click


        Dim rpt As New ReportDocument
        Dim ds As New DataSet
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@From", dtStartDate.Value)
        cmd.Parameters.AddWithValue("@To", dtEndDate.Value)
        cmd.Connection = linq_obj.Connection
        Dim str As String = cmbReportType.Text
        Select str
            Case "General Inward Detail"
                cmd.CommandText = "Sp_Rpt_FullInwardDetail"
                da.SelectCommand = cmd
                da.Fill(ds, "Sp_Rpt_FullInwardDetail")
                Class1.WriteXMlFile(ds, "Sp_Rpt_FullInwardDetail", "Sp_Rpt_FullInwardDetail")
                rpt.Load(Application.StartupPath & "\Reports\stockinwardreport.rpt")
                rpt.Database.Tables(0).SetDataSource(ds.Tables("Sp_Rpt_FullInwardDetail"))
                Dim frmRpt As New FrmCommanReportView(rpt)
                frmRpt.Show()
            Case "General Outward Detail"
                cmd.CommandText = "Sp_Rpt_FullOutwardDetail"
                da.SelectCommand = cmd
                da.Fill(ds, "Sp_Rpt_FullOutwardDetail")
                Class1.WriteXMlFile(ds, "Sp_Rpt_FullOutwardDetail", "Sp_Rpt_FullOutwardDetail")
                rpt.Load(Application.StartupPath & "\Reports\stockoutward.rpt")
                rpt.Database.Tables(0).SetDataSource(ds.Tables("Sp_Rpt_FullOutwardDetail"))
                Dim frmRpt As New FrmCommanReportView(rpt)
                frmRpt.Show()
        End Select
    End Sub
End Class