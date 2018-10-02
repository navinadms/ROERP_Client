Public Class StockReportFinal
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PRID As Long
    Dim rwID As Integer
    Dim tblItem As New DataTable
    Dim count As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ddlcategory_Bind()

    End Sub
    Public Sub ddlcategory_Bind()
        cmb_Category.Items.Clear()
        Dim dt As New DataTable
        dt.Columns.Add("Id")
        dt.Columns.Add("Category")
        Dim productMain = linq_obj.SP_Select_All_ProductRegisterMaster().ToList()
        For Each item As SP_Select_All_ProductRegisterMasterResult In productMain
            dt.Rows.Add(item.Pk_ProductRegisterId, item.CategoryName)
        Next
        Dim dr As DataRow = dt.NewRow()
        dr(1) = "Select"
        dt.Rows.InsertAt(dr, 0)
        cmb_Category.DataSource = dt
        cmb_Category.DisplayMember = "Category"
        cmb_Category.ValueMember = "Id"
        cmb_Category.AutoCompleteMode = AutoCompleteMode.Append
        cmb_Category.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_Category.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If chkDate.Checked = True Then

            If cmb_Category.SelectedIndex <> 0 Then
                Dim data = linq_obj.SP_Tbl_Stock_ProductRegister_SelectBy_Criteria(0, Convert.ToInt64(cmb_Category.SelectedValue), txtstartdate.Value, txtenddate.Value).ToList()
                If (data.Count > 0) Then
                    GvStockReport.DataSource = data
                    GvStockReport.Columns(0).Visible = False
                    GvStockReport.Columns(1).Visible = False
                    GvStockReport.Columns(2).Visible = False
                    GvStockReport.Columns(2).Visible = False
                Else
                    GvStockReport.DataSource = Nothing

                End If
            Else
                Dim data = linq_obj.SP_Tbl_Stock_ProductRegister_SelectBy_Criteria(1, 0, txtstartdate.Value, txtenddate.Value).ToList()
                If (data.Count > 0) Then
                    GvStockReport.DataSource = data
                    GvStockReport.Columns(0).Visible = False
                    GvStockReport.Columns(1).Visible = False
                    GvStockReport.Columns(2).Visible = False
                    GvStockReport.Columns(2).Visible = False
                Else
                    GvStockReport.DataSource = Nothing
                End If
            End If


        End If

        If chkDate.Checked = False Then


            If cmb_Category.SelectedIndex <> 0 Then
                Dim data = linq_obj.SP_Tbl_Stock_ProductRegister_SelectByCategory(Convert.ToInt64(cmb_Category.SelectedValue)).ToList()
                If (data.Count > 0) Then
                    GvStockReport.DataSource = data
                    GvStockReport.Columns(0).Visible = False
                    GvStockReport.Columns(1).Visible = False
                    GvStockReport.Columns(2).Visible = False
                    GvStockReport.Columns(2).Visible = False
                Else
                    GvStockReport.DataSource = Nothing

                End If
            End If
        End If
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
            For i As Integer = 1 To GvStockReport.Columns.Count
                worksheet.Cells(1, i) = GvStockReport.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvStockReport.Rows.Count - 1
                For j As Integer = 0 To GvStockReport.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvStockReport.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next
            ' save the application
            'workbook.SaveAs("d:\output(" & count & ").xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _
            'Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
            ' Exit from the application
            ' app.Quit();
            ' storing Each row and column value to excel sheet
            ' MessageBox.Show("Excel Download SucessFully on D:\output(" & count & ").xls ")
        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try

        'Dim rpt As New rpt_inquiryReport
        'rpt.SetDataSource(ds.Tables(1))
        'rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, "ABC.xls")
        'MessageBox.Show("Successfully Exported")
        'FileOpen(1, "DEF.pdf", OpenMode.Append, OpenAccess.Write)
        'rpt.Refresh()
    End Sub
End Class