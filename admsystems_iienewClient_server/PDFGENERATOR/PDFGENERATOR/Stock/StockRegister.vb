﻿Public Class StockRegister

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PRID As Long
    Dim rwID As Integer
    Dim tblItem As New DataTable
    Dim count As Integer
    Private Sub ProductRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        tblItem.Columns.Add("SrNo")
        tblItem.Columns.Add("Category")
        tblItem.Columns.Add("Row Material Name")
        tblItem.Columns.Add("Quantity")
        tblItem.Columns.Add("Unit")
        tblItem.Columns.Add("Fk_Category")
        tblItem.Columns.Add("Fk_RowMaterial")
        For Each control As Control In Me.Controls
            ' The textbox control.
            Dim parentControl As New Control


            If (control.GetType() Is GetType(GroupBox)) Then
                Dim grpBox As GroupBox = TryCast(control, GroupBox)
                parentControl = grpBox
            ElseIf (control.GetType() Is GetType(TabControl)) Then
                Dim TC As TabControl = TryCast(control, TabControl)
                parentControl = TC
            Else
                Try
                    parentControl = control
                Catch ex As Exception

                End Try

            End If

            For Each subcontrol As Control In parentControl.Controls
                If (subcontrol.GetType() Is GetType(TextBox)) Then
                    Dim textBox As TextBox = TryCast(subcontrol, TextBox)

                    ' If not null, set the handler.
                    If textBox IsNot Nothing Then
                        AddHandler textBox.Enter, AddressOf TextBox_Enter
                        AddHandler textBox.Leave, AddressOf TextBox_Leave
                    End If
                End If

                ' Set the handler.
            Next

            ' Set the handler.
        Next
    End Sub

    Private Sub TextBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)
        txt.BackColor = Color.LightYellow
    End Sub

    Private Sub TextBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)
        txt.BackColor = Color.White
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindGrid()
        bindDropDown()
        getPageRight()
    End Sub

    Public Sub getPageRight()
        Try
            Dim dv As New DataTable
            Dim dataView As DataView
            Dim Class1 As New Class1
            Dim RowCount As Integer
            Dim statusCheck As Boolean = False
            Dim strName As String = ""

            dataView = Class1.global.UserPermissionDataset.Tables(0).DefaultView
            dataView.RowFilter = "([DetailName] like 'Stock Register')"
            If (dataView.Count > 0) Then
                dv = dataView.ToTable()
                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then

                            btnAdd.Enabled = True
                        Else
                            btnAddNewAll.Enabled = False
                            btnAdd.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsUpdate") = True) Then
                            If (btnAdd.Text = "Update") Then
                                btnAdd.Enabled = True
                            End If
                        Else
                            If (btnAdd.Text = "Update") Then
                                btnAdd.Enabled = False
                            End If
                        End If
                        If (dv.Rows(RowCount)("IsDelete") = True) Then
                            btnDelete.Enabled = True
                        Else
                            btnDelete.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsPrint") = True) Then

                        Else

                        End If

                    Next
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub bindGrid()
        Dim prData = linq_obj.SP_Select_All_ProductRegisterMaster().ToList()
        If (prData.Count > 0) Then
            Dim dt As New DataTable
            dt.Columns.Add("ID")
            dt.Columns.Add("Category")
            For Each item As SP_Select_All_ProductRegisterMasterResult In prData
                dt.Rows.Add(item.Pk_ProductRegisterId, item.CategoryName)
            Next
            dgCategories.DataSource = dt
            txtTotRecords.Text = prData.Count
        End If
    End Sub

    Protected Sub bindDropDown()
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

    Private Sub cmb_Category_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_Category.SelectionChangeCommitted
        Try
            If cmb_Category.Text <> "Select" Then
                PRID = cmb_Category.SelectedValue
                Dim rowMaterial = linq_obj.SP_Select_All_RowMaterialMaster(PRID).ToList()
                Cmb_RawMaterial.DataSource = rowMaterial
                Cmb_RawMaterial.DisplayMember = "RowMaterialName"
                Cmb_RawMaterial.ValueMember = "Pk_RowMaterialId"
                Cmb_RawMaterial.AutoCompleteMode = AutoCompleteMode.Append
                Cmb_RawMaterial.DropDownStyle = ComboBoxStyle.DropDownList
                Cmb_RawMaterial.AutoCompleteSource = AutoCompleteSource.ListItems
                PRID = Convert.ToInt32(cmb_Category.SelectedValue)
                GridBind()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GridBind()
        Dim data = linq_obj.SP_Tbl_Stock_ProductRegister_SelectByCategory(Convert.ToInt64(cmb_Category.SelectedValue)).ToList()
        If (data.Count > 0) Then
            dgRawMaterialData.DataSource = data
            dgRawMaterialData.Columns(0).Visible = False
            dgRawMaterialData.Columns(1).Visible = False
            dgRawMaterialData.Columns(2).Visible = False
            dgRawMaterialData.Columns(2).Visible = False
        Else
            dgRawMaterialData.DataSource = Nothing

        End If

        For index = 0 To dgRawMaterialData.RowCount - 1
            If (dgRawMaterialData.Rows(index).Cells(13).Value >= dgRawMaterialData.Rows(index).Cells(9).Value) Then
                dgRawMaterialData.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                dgRawMaterialData.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.Black
            End If
        Next


    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim res As Integer

            If (cmb_Category.SelectedValue >= 0 And Cmb_RawMaterial.SelectedValue >= 0) Then
                If (btnAdd.Text = "Add") Then
                    If (dtEntrydate.Value <= Date.Now) Then
                        res = linq_obj.SP_Tbl_Stock_ProductRegister_Insert(Convert.ToInt64(cmb_Category.SelectedValue), Convert.ToInt64(Cmb_RawMaterial.SelectedValue), Convert.ToInt32(txtIssueStock.Text), Convert.ToInt32(txtOutward.Text), Convert.ToInt64(If(txtReorder.Text.Trim() = "", 0, txtReorder.Text.Trim())), Convert.ToInt64(txtClosing.Text), txtunit.Text, Convert.ToInt32(txtOpening.Text), dtEntrydate.Value)
                        If (res > 0) Then
                            linq_obj.SubmitChanges()
                            'MessageBox.Show("Insert Sucessfully....")
                            GridBind()
                            BlankText()
                        Else
                            MessageBox.Show("RawMaterial Already Inserted...")
                            Cmb_RawMaterial.Focus()
                        End If
                    Else
                        MessageBox.Show("Enter Valid Date...")
                    End If
                Else
                    If (dtEntrydate.Value <= Date.Now) Then
                        res = linq_obj.SP_Tbl_Stock_ProductRegister_Update(Convert.ToInt64(cmb_Category.SelectedValue), Convert.ToInt64(Cmb_RawMaterial.SelectedValue), Convert.ToInt64(txtClosing.Text), txtunit.Text, Convert.ToInt32(txtOpening.Text), rwID, dtEntrydate.Value)
                        If (res >= 0) Then
                            linq_obj.SubmitChanges()
                            'MessageBox.Show("Insert Sucessfully....")
                            GridBind()
                            BlankText()
                        Else
                            MessageBox.Show("RawMaterial Already Inserted...")
                            Cmb_RawMaterial.Focus()
                        End If

                    Else
                        MessageBox.Show("Enter Valid Date...")
                    End If
                End If
            Else

                MessageBox.Show("Select Category Or Rawmaterial")
                Cmb_RawMaterial.Focus()
                btnNew.Focus()

            End If

        Catch ex As Exception
            MessageBox.Show("Error in Insertion")
        End Try
    End Sub
    Private Sub BlankText()
        txtIssueStock.Text = ""
        txtReorder.Text = ""
        txtClosing.Text = ""
        txtOpening.Text = ""
        txtOutward.Text = ""
        dtEntrydate.Value = Date.Now

        txtunit.Text = ""
        cmb_Category.Enabled = True
        btnAdd.Text = "Add"
        txtClosing.Enabled = True
        txtIssueStock.Enabled = True
        txtOutward.Enabled = True

    End Sub
    Private Sub dgCategories_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgCategories.DoubleClick
        If (Me.dgCategories.SelectedCells(0).Value > 0) Then
            PRID = Convert.ToInt64(Me.dgCategories.SelectedCells(0).Value)
            cmb_Category.SelectedValue = PRID
            GridBind()
        End If
    End Sub
    Private Sub txtIssueStock_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIssueStock.Leave
        txtClosing.Text = Convert.ToString((Convert.ToInt32(IIf(txtIssueStock.Text = "", 0, txtIssueStock.Text)) + Convert.ToInt32(IIf(txtOpening.Text = "", 0, txtOpening.Text))) - Convert.ToInt32(IIf(txtOutward.Text = "", 0, txtOutward.Text)))
        If (Convert.ToInt32(txtClosing.Text.Trim()) < 0) Then
            MessageBox.Show("Please Enter Valid Stock numbers")
            txtIssueStock.Focus()
        End If
    End Sub
    Private Sub Cmb_RawMaterial_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_RawMaterial.SelectionChangeCommitted
        Dim dataRaw = linq_obj.SP_Select_RowMaterialMaster_ById(Cmb_RawMaterial.SelectedValue).ToList()
        If (dataRaw.Count > 0) Then
            txtunit.Text = Convert.ToString(dataRaw(0).Unit)
        End If
    End Sub
    Private Sub btnAddNewAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewAll.Click
        clear()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clear()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            Dim cntSelect As Integer = dgRawMaterialData.SelectedRows.Count
            For Each dr As DataGridViewRow In dgRawMaterialData.SelectedRows
                Dim resDelete As Integer = linq_obj.SP_Tbl_Stock_ProductRegister_Delete(Convert.ToInt32(dr.Cells(0).Value))
                linq_obj.SubmitChanges()
            Next
            MessageBox.Show("Successfully Deleted!!!")
            GridBind()
        End If

    End Sub
    Public Sub subClear()

    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        BlankText()
    End Sub
    Public Sub clear()
        Try
            BlankText()
            btnAdd.Enabled = True
            btnAdd.Text = "Add"
            PRID = 0
            cmb_Category.SelectedIndex = 0
            '   Cmb_RawMaterial.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub txtOutward_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOutward.Leave
        txtClosing.Text = Convert.ToString((Convert.ToInt32(IIf(txtIssueStock.Text = "", 0, txtIssueStock.Text)) + Convert.ToInt32(IIf(txtOpening.Text = "", 0, txtOpening.Text))) - Convert.ToInt32(IIf(txtOutward.Text = "", 0, txtOutward.Text)))
        If (Convert.ToInt32(txtClosing.Text.Trim()) < 0) Then
            MessageBox.Show("Please Enter Valid Stock numbers")
            txtOutward.Focus()
        End If
    End Sub

    Private Sub txtOpening_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOpening.Leave
        txtClosing.Text = Convert.ToString((Convert.ToInt32(IIf(txtIssueStock.Text = "", 0, txtIssueStock.Text)) + Convert.ToInt32(IIf(txtOpening.Text = "", 0, txtOpening.Text))) - Convert.ToInt32(IIf(txtOutward.Text = "", 0, txtOutward.Text)))
        If (Convert.ToInt32(txtClosing.Text.Trim()) < 0) Then
            MessageBox.Show("Please Enter Valid Stock numbers")
            txtOpening.Focus()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim prData = linq_obj.SP_Select_All_ProductRegisterMaster().Where(Function(p) p.CategoryName.ToLower().Contains(txtcategory.Text)).ToList()
        If (prData.Count > 0) Then
            Dim dt As New DataTable
            dt.Columns.Add("ID")
            dt.Columns.Add("Category")
            For Each item As SP_Select_All_ProductRegisterMasterResult In prData
                dt.Rows.Add(item.Pk_ProductRegisterId, item.CategoryName)
            Next
            dgCategories.DataSource = dt
            txtTotRecords.Text = prData.Count
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        bindGrid()
    End Sub

    Private Sub dgRawMaterialData_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgRawMaterialData.DoubleClick
        Try
            rwID = dgRawMaterialData.SelectedCells(0).Value
            cmb_Category.SelectedValue = dgRawMaterialData.SelectedCells(1).Value
            'cmb_Category_SelectionChangeCommitted(Nothing, Nothing)

            Cmb_RawMaterial.SelectedValue = dgRawMaterialData.SelectedCells(2).Value
            txtunit.Text = dgRawMaterialData.SelectedRows(0).Cells("Unit").Value
            txtOpening.Text = dgRawMaterialData.SelectedRows(0).Cells("OpeningStock").Value
            txtClosing.Text = dgRawMaterialData.SelectedRows(0).Cells("RemainingStock").Value
            txtIssueStock.Text = dgRawMaterialData.SelectedRows(0).Cells("InwardStock").Value
            txtReorder.Text = dgRawMaterialData.SelectedRows(0).Cells("ReInwardStock").Value
            txtOutward.Text = dgRawMaterialData.SelectedRows(0).Cells("OutwardStock").Value
            dtEntrydate.Value = dgRawMaterialData.SelectedRows(0).Cells("EntryDate").Value
            btnAdd.Text = "Update"
            cmb_Category.Enabled = False
            txtIssueStock.Enabled = False
            txtOutward.Enabled = False
            txtClosing.Enabled = False
            txtReorder.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
   
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If (cmb_Category.SelectedValue > 0) Then
            If result = DialogResult.Yes Then

                Dim resDelete As Integer = linq_obj.SP_Tbl_Stock_ProductRegister_DeleteByCategory(cmb_Category.SelectedValue)
                linq_obj.SubmitChanges()

                MessageBox.Show("Successfully Deleted!!!")
                GridBind()
            End If
        Else
            MessageBox.Show("No Data Found For Delete...")
        End If
    End Sub


    Private Sub btnSetMonthStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMonthStock.Click
        Dim res As Integer = linq_obj.SP_Tbl_StockReportLogDetail_Insert(Class1.global.UserID, DateTime.Now, "Test", True)
        If (res >= 0) Then
            linq_obj.SubmitChanges()
            MessageBox.Show("Successfully Saved A Log...")
        End If
    End Sub

    Private Sub dgCategories_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgCategories.CellClick
        Try
            If (Me.dgCategories.SelectedCells(0).Value > 0) Then
                PRID = Convert.ToInt64(Me.dgCategories.SelectedCells(0).Value)
                cmb_Category.SelectedValue = PRID
                cmb_Category_SelectionChangeCommitted(Nothing, Nothing)
                GridBind()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtOpening_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOpening.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtIssueStock_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIssueStock.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtOutward_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOutward.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtClosing_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClosing.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtReorder_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReorder.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
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
            For i As Integer = 1 To dgRawMaterialData.Columns.Count
                worksheet.Cells(1, i) = dgRawMaterialData.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To dgRawMaterialData.Rows.Count - 1
                For j As Integer = 0 To dgRawMaterialData.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = dgRawMaterialData.Rows(i).Cells(j).Value.ToString()
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

    Private Sub btnViewTodayStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewTodayStock.Click
        Dim form As New ViewDailyStock
        form.Show()

    End Sub

    Private Sub btnStockReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStockReports.Click
        Dim form As New Rpt_StockMainReport
        form.Show()
    End Sub

    Private Sub btnfinalstaock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfinalstaock.Click
        Dim stk As New StockReportFinal
        stk.Show()

    End Sub
End Class