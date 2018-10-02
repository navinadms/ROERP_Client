
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
Public Class OrderManagerHotListReport
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim dataAddress As DataTable
    Dim count As Integer
    Dim dt As New DataTable

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        GvHotList_Bind()

    End Sub

    Public Sub GvHotList_Bind()
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        cmd.CommandText = "SP_Get_OrderManager_HotList_Report"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = linq_obj.Connection
        da.SelectCommand = cmd
        da.Fill(dt)
        'Dim HotList = linq_obj.SP_Get_OrderManager_HotList_Report().ToList()
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow
            dr = dt.NewRow()
            dr("Srno") = 0
            dr("Name") = "Total"
            For j As Int16 = 3 To dt.Columns.Count - 1
                dr(dt.Columns(j).ColumnName) = "0"
                For x As Int16 = 0 To dt.Rows.Count - 1
                    If Convert.ToString(dt.Rows(x)(j)) <> "" Then
                        dr(dt.Columns(j).ColumnName) = Convert.ToInt16(dr(dt.Columns(j).ColumnName)) + 1
                    End If
                Next
            Next
            dt.Rows.Add(dr)
            GvHotListReport.DataSource = dt
        End If



        
        'For Each item In HotList
        '    dt.Rows.Add(item.SrNo, item.Name, item.Station, item.Capacity, item.Model, item.Lab, item.JarWashing, item.Pouch, item.Blow, item.BatchCoding, item.PackBulk, item.Glass, item.Chiller, item.Soda)
        'Next

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
            For i As Integer = 1 To GvHotListReport.Columns.Count
                worksheet.Cells(1, i) = GvHotListReport.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvHotListReport.Rows.Count - 1
                For j As Integer = 0 To GvHotListReport.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = Convert.ToString(GvHotListReport.Rows(i).Cells(j).Value)
                    count += i + 2 + j + 1
                Next
            Next



        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim dv As DataTable
        Dim sWhere As String = ""
        Dim sFiled As String
        sFiled = "OrderStatus = '"
        'sWhere = " Name in ("
        Dim str As String
        If lstOrderStatus.CheckedItems.Count > 0 Then
            For i As Integer = 0 To lstOrderStatus.CheckedItems.Count - 1
                If lstOrderStatus.CheckedItems(i).ToString = "All" Then
                    sWhere = ""
                    GoTo b
                Else
                    str = sFiled + lstOrderStatus.CheckedItems(i)
                    sWhere += str + "' or "

                    'sWhere = ""

                    'sWhere = sWhere + "'" + lstOrderStatus.CheckedItems(i) & "',"
                End If
            Next
            sWhere = sWhere.Trim().Substring(0, sWhere.Trim().Length - 3)
            'sWhere = sWhere + ")"
        End If
b:
        Dim dataView As New DataView(dt)
        Dim Class1 As New Class1
        Dim strName As String = ""


        If sWhere = "" Then
            dataView.RowFilter = Nothing
        Else
            dataView.RowFilter = sWhere

        End If

        dv = dataView.ToTable()

        If dv.Rows.Count > 0 Then
            If sWhere.Length > 0 Then
                Dim dr As DataRow
                dr = dv.NewRow()
                dr("Srno") = 0
                dr("Name") = "Total"
                For j As Int16 = 3 To dv.Columns.Count - 1
                    dr(dv.Columns(j).ColumnName) = "0"
                    For x As Int16 = 0 To dv.Rows.Count - 1
                        If Convert.ToString(dv.Rows(x)(j)) <> "" Then
                            dr(dv.Columns(j).ColumnName) = Convert.ToInt16(dr(dv.Columns(j).ColumnName)) + 1
                        End If
                    Next
                Next
                dv.Rows.Add(dr)
            End If
            GvHotListReport.DataSource = dv
            GvHotListReport.Columns(GvHotListReport.ColumnCount - 1).Visible = False
        Else
            GvHotListReport.DataSource = Nothing
        End If



    End Sub
End Class