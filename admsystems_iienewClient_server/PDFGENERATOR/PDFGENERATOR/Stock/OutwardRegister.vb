﻿Imports System.Data.SqlClient

Public Class OutwardRegister
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PRID As Long
    Dim PRIDRE As Long
    Dim OutwardId As Long
    Dim REInwardId As Long
    Dim REInwardDetailId As Long
    Dim OutwardDetailId As Long
    Dim AddressId As Long
    Dim tblItem As New DataTable
    Dim RWID As Long
    Dim ds As New DataSet
    Dim flag As Integer
    Dim rwIDDelDetail As Integer = 0
    Dim frmReInward As Form

    Public Sub New()
        'This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        bindDropDown()
        bindGrid()
        OutwardId = 0
        txtTotalRecords.Text = getTotalEntry().ToString()
        txtNo.Text = IIf(DGVSellItems.Rows.Count = 0, 1, DGVSellItems.Rows.Count)
        bindItemGrid()
        If (OutwardId > 0) Then
            btnSave.Enabled = False
        Else
            btnSave.Enabled = True
        End If
        txtOutwardNo.Text = linq_obj.GetMaxOutwardId().ToString()
        getPageRight()
        bindCustomerData()

    End Sub
    Public Sub bindCustomerData()
        If (rbCustomer.Checked = True) Then
            AutoCompated_Text()
            lblEnqNo.Visible = True
            txtEnqNo.Visible = True
            txtEnqNo.Focus()
        Else
            lblEnqNo.Visible = False
            txtEnqNo.Visible = False
        End If
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
            dataView.RowFilter = "([DetailName] like 'OutwardRegister')"
            If (dataView.Count > 0) Then
                dv = dataView.ToTable()

                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            btnAdd.Enabled = True
                            btnSave.Enabled = True
                        Else
                            btnAdd.Enabled = False
                            btnSave.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsUpdate") = True) Then
                            btnChange.Enabled = True
                        Else
                            btnChange.Enabled = False
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
    
    Public Sub AutoCompated_Text()
        'txtEnqNo.AutoCompleteCustomSource.Clear()
        'txtCustomer.AutoCompleteCustomSource.Clear()
        'Dim GetCustList = linq_obj.GetAddressOForderData().ToList()
        'For Each iteam As GetAddressOForderDataResult In GetCustList
        '    txtCustomer.AutoCompleteCustomSource.Add(iteam.Name)
        '    txtEnqNo.AutoCompleteCustomSource.Add(iteam.EnqNo)
        'Next

        'Navin 26-02-2015

        txtCustomer.AutoCompleteCustomSource.Clear()
        Dim data = linq_obj.SP_Select_All_Tbl_OutwardMaster().ToList()
        For Each iteam As SP_Select_All_Tbl_OutwardMasterResult In data
            txtCustomer.AutoCompleteCustomSource.Add(iteam.CustomerName)

        Next

    End Sub
   
    Protected Sub bindDropDown()
        cmbCategory.Items.Clear()
        Dim dt As New DataTable
        dt.Columns.Add("Id")
        dt.Columns.Add("Category")
        Dim productMain = linq_obj.SP_Select_All_ProductRegisterByStockRegister().ToList()
        For Each item As SP_Select_All_ProductRegisterByStockRegisterResult In productMain
            dt.Rows.Add(item.Pk_ProductRegisterId, item.CategoryName)
        Next
        Dim dr As DataRow = dt.NewRow()
        dr(1) = "Select"
        dt.Rows.InsertAt(dr, 0)
        cmbCategory.DataSource = dt
        cmbCategory.DisplayMember = "Category"
        cmbCategory.ValueMember = "Id"
        cmbCategory.AutoCompleteMode = AutoCompleteMode.Append
        cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCategory.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    ''' <summary>
    ''' 
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub bindGrid()
        Dim mainData = linq_obj.SP_Select_All_Tbl_OutwardMaster().ToList()
        Dim dt As New DataTable
        dt.Columns.Add("No")
        dt.Columns.Add("Date")
        dt.Columns.Add("CustomerName")
        For Each item As SP_Select_All_Tbl_OutwardMasterResult In mainData
            dt.Rows.Add(item.Pk_OutwardId, item.OutwardDate, item.CustomerName)
        Next
        GDVOutwardDetail.DataSource = dt
        txtTotalRecords.Text = getTotalEntry().ToString()

        If (GDVOutwardDetail.RowCount > 0) Then
            '  GDVOutwardDetail.Columns(0).Visible = False
        End If
        txtOutwardNo.Text = linq_obj.GetMaxOutwardId().ToString()
    End Sub
    Public Function getTotalEntry() As Long
        Return GDVOutwardDetail.RowCount
    End Function
    Public Sub bindItemGrid()
        Dim itemData = linq_obj.SP_Select_All_Tbl_OutwardDetail(OutwardId).ToList()
        If (itemData.Count > 0) Then
            DGVSellItems.DataSource = itemData
            Dim rowCount As Integer
            rowCount = DGVSellItems.RowCount
            If rowCount > 0 Then
                DGVSellItems.Columns(0).Visible = False
                DGVSellItems.Columns(1).Visible = False
                DGVSellItems.Columns(2).Visible = False
                DGVSellItems.Columns(3).Visible = False
            End If
            txtNo.Text = IIf(DGVSellItems.Rows.Count = 0, 1, DGVSellItems.Rows.Count + 1)
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim mainData = linq_obj.SP_Select_All_Tbl_OutwardMaster().Where(Function(t) t.OutwardDate > dtStartDate.Value And t.OutwardDate < dtEndDate.Value).ToList()
        Dim dt As New DataTable
        dt.Columns.Add("No")
        dt.Columns.Add("Date")
        dt.Columns.Add("CustomerName")
        For Each item As SP_Select_All_Tbl_OutwardMasterResult In mainData
            dt.Rows.Add(item.Pk_OutwardId, item.OutwardDate, item.CustomerName)
        Next
        GDVOutwardDetail.DataSource = dt
    End Sub
    Private Sub cmbCategory_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectionChangeCommitted
        Try

            If cmbCategory.SelectedValue > 0 Then
                PRID = cmbCategory.SelectedValue
                Dim rowMaterial = linq_obj.SP_Select_All_RawMaterialByStockRegister(PRID).ToList()
                cmbRowMaterial.DataSource = rowMaterial
                cmbRowMaterial.DisplayMember = "RowMaterialName"
                cmbRowMaterial.ValueMember = "Pk_RowMaterialId"
                cmbRowMaterial.AutoCompleteMode = AutoCompleteMode.Append
                cmbRowMaterial.DropDownStyle = ComboBoxStyle.DropDownList
                cmbRowMaterial.AutoCompleteSource = AutoCompleteSource.ListItems
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message())
        End Try

    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click


        If (flag = 1) Then
            MessageBox.Show("Stock Not Available!!!")
        Else
            If (txtPrevOutNo.Text.Trim() = "") Then

                Try
                    If txtOutwardNo.Text.Trim() <> "" And OutwardId = 0 Then
                        OutwardId = linq_obj.SP_Insert_Tbl_OutwardMaster(If(rbCustomer.Checked = True, AddressId, 0), txtCustomer.Text, txtAddress.Text, txtContactNo.Text, txtRemark.Text, dtOutDate.Value, txtOutEngineer.Text)
                    End If
                    If (OutwardId > 0) Then
                        If (btnAdd.Text = "Add") Then
                            'insert into  Details
                            linq_obj.SP_Insert_Tbl_OutwardDetail(OutwardId, Convert.ToInt64(cmbCategory.SelectedValue), Convert.ToInt64(cmbRowMaterial.SelectedValue), Convert.ToInt64(txtQty.Text.Trim()), txtUnit.Text, txtDetailRemarks.Text)
                            ' MessageBox.Show("Successfully Saved")
                        Else
                            'insert into  Details
                            linq_obj.SP_Update_Tbl_OutwardDetail(OutwardId, Convert.ToInt64(cmbCategory.SelectedValue), Convert.ToInt64(cmbRowMaterial.SelectedValue), Convert.ToInt64(txtQty.Text.Trim()), txtUnit.Text, txtDetailRemarks.Text, OutwardDetailId)
                            ' MessageBox.Show("Successfully Saved")
                        End If

                    End If
                    bindItemGrid()
                    txtNo.Text = IIf(DGVSellItems.Rows.Count = 0, 1, DGVSellItems.Rows.Count + 1)
                    subClear()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                bindGrid()
                cmbCategory.Focus()

                ' btnAddNew.Focus()
            Else

                ''''add a datarow in data tabl for previous outward 

                Dim dr As DataRow
                dr = tblItem.NewRow()



                dr("Pk_OutDetailId") = 0

                dr("Fk_CategoryId") = cmbCategory.SelectedValue
                dr("Fk_RawmaterialId") = cmbRowMaterial.SelectedValue
                dr("Fk_OutwardId") = 0
                dr("SrNo") = IIf(DGVSellItems.Rows.Count = 0, 1, DGVSellItems.Rows.Count + 1)
                dr("CategoryName") = cmbCategory.Text
                dr("RawMaterialName") = cmbRowMaterial.Text
                dr("Unit") = txtUnit.Text
                dr("Quanity") = txtQty.Text
                dr("Remarks") = txtDetailRemarks.Text
              
                If (rwIDDelDetail > 0) Then
                    DGVSellItems.Rows.RemoveAt(rwIDDelDetail)
                    tblItem = DGVSellItems.DataSource
                    tblItem.Rows.Add(dr)
                Else
                    tblItem.Rows.Add(dr)
                End If
                DGVSellItems.DataSource = tblItem
                rwIDDelDetail = 0
                Dim rowCount As Integer
                rowCount = DGVSellItems.RowCount
                If rowCount > 0 Then
                    DGVSellItems.Columns(0).Visible = False
                    DGVSellItems.Columns(1).Visible = False
                    DGVSellItems.Columns(2).Visible = False
                    DGVSellItems.Columns(3).Visible = False
                End If
                txtNo.Text = IIf(DGVSellItems.Rows.Count = 0, 1, DGVSellItems.Rows.Count + 1)
            End If

        End If

    End Sub

    'Private Sub OutwardRegister_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = 13 Then
    '        SendKeys.Send("{TAB}")
    '    End If

    '    If e.KeyCode = 27 Then
    '        Me.Close()
    '    End If
    'End Sub
    Private Sub OutwardRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



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
        Next

        ' Set the handler.

        'Added A Service FollowUp
        tblItem.Columns.Add("Pk_OutDetailId")
        tblItem.Columns.Add("Fk_CategoryId")
        tblItem.Columns.Add("Fk_RawmaterialId")
        tblItem.Columns.Add("Fk_OutwardId")
        tblItem.Columns.Add("SrNo")
        tblItem.Columns.Add("CategoryName")
        tblItem.Columns.Add("RawMaterialName")
        tblItem.Columns.Add("Unit")
        tblItem.Columns.Add("Quanity")
        tblItem.Columns.Add("Remarks")
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
    Public Sub subReINClear()
        cmbRICategory.SelectedIndex = 0
        txtRIQty.Text = ""
        txtRIUnit.Text = ""
        txtRISubRemark.Text = ""
        btnRISubADD.Text = "Add"
        txtSrNo.Text = IIf(DGReInwardDetail.Rows.Count = 0, 1, DGReInwardDetail.Rows.Count + 1)
        txtSrNo.Focus()
    End Sub

    Public Sub subClear()
        ' txtNo.Text = ""
        cmbRowMaterial.SelectedIndex = 0
        txtUnit.Text = ""
        txtQty.Text = ""
        txtDetailRemarks.Text = ""
        btnAdd.Text = "Add"

        txtNo.Text = IIf(DGVSellItems.Rows.Count = 0, 1, DGVSellItems.Rows.Count + 1)


    End Sub

    Public Sub clear()
        txtAddress.Text = ""
        txtCustomer.Text = ""
        txtNo.Text = ""
        txtPrevOutNo.Text = ""
        txtQty.Text = ""
        txtUnit.Text = ""
        txtContactNo.Text = ""
        txtRemark.Text = ""
        txtOutEngineer.Text = ""

        cmbCategory.SelectedIndex = 0
        btnSave.Enabled = True
        btnAdd.Enabled = True
        btnChange.Enabled = True
        btnDelete.Enabled = True
        btnDeleteSub.Enabled = True
        btnREInward.Enabled = True
        txtOutwardNo.BackColor = Color.White
        dtOutDate.Value = DateTime.Now
        OutwardId = 0
        DGVSellItems.DataSource = Nothing

        txtOutwardNo.Text = linq_obj.GetMaxOutwardId().ToString()
    End Sub
    Private Sub GDVOutwardDetail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GDVOutwardDetail.DoubleClick
        bindAllData()
    End Sub
    Public Sub bindAllData()
        Try
            If (Me.GDVOutwardDetail.SelectedCells(0).Value > 0) Then
                OutwardId = Convert.ToInt64(Me.GDVOutwardDetail.SelectedCells(0).Value)
                Dim outwardData = linq_obj.SP_Select_Tbl_OutwardMaster_ById(OutwardId).ToList()
                If (outwardData.Count > 0) Then
                    bindItemGrid()
                    If (outwardData(0).Fk_AddressId > 0) Then
                        rbCustomer.Checked = True
                        AddressId = outwardData(0).Fk_AddressId
                    Else
                        rbOther.Checked = True
                        AddressId = 0
                    End If
                    txtAddress.Text = Convert.ToString(outwardData(0).Address)
                    txtCustomer.Text = Convert.ToString(outwardData(0).CustomerName)
                    txtContactNo.Text = Convert.ToString(outwardData(0).ContactNo)
                    txtRemark.Text = Convert.ToString(outwardData(0).Remarks)
                    txtOutEngineer.Text = Convert.ToString(outwardData(0).EngineerName)
                    dtOutDate.Value = outwardData(0).OutwardDate
                    If (outwardData(0).Status = False) Then
                        btnAdd.Enabled = True
                        btnChange.Enabled = True
                        btnDelete.Enabled = True
                        btnDeleteSub.Enabled = True
                        txtOutwardNo.BackColor = Color.White
                        DGVSellItems.ReadOnly = False
                        btnREInward.Enabled = True
                    Else
                        btnAdd.Enabled = False
                        btnChange.Enabled = False
                        btnDelete.Enabled = False
                        btnDeleteSub.Enabled = False
                        txtOutwardNo.BackColor = Color.Yellow
                        DGVSellItems.ReadOnly = False
                        btnREInward.Enabled = False
                    End If
                    ' txtOutwardNo.Text = outwardData(0).OutwardNo
                End If
                If (OutwardId > 0) Then
                    txtOutwardNo.Text = OutwardId.ToString()
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        subClear()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (OutwardId > 0) Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                Dim res As Integer
                res = linq_obj.SP_Delete_Tbl_OutwardMaster(OutwardId)
                MessageBox.Show("Delete Successfully..")
            End If
            btnAdd.Enabled = True
            btnChange.Enabled = True
            btnDelete.Enabled = True
            btnDeleteSub.Enabled = True
            txtOutwardNo.BackColor = Color.White
        Else
            MessageBox.Show("No Data Selected!!!")
        End If
    End Sub

    Private Sub btnAddNewAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewAll.Click
        subClear()

        clear()
    End Sub

    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        Try
            If (OutwardId > 0) Then
                Dim res As Integer
                If txtOutwardNo.Text.Trim() <> "" Then
                    res = linq_obj.SP_Update_Tbl_OutwardMaster(If(rbCustomer.Checked = True, AddressId, 0), txtCustomer.Text, txtAddress.Text, txtContactNo.Text, txtRemark.Text, dtOutDate.Value, txtOutEngineer.Text, OutwardId)
                    If (res >= 0) Then
                        linq_obj.SubmitChanges()
                        MessageBox.Show("Successfully Saved...")
                        bindGrid()
                    End If

                Else
                    MessageBox.Show("No Address Detail Found")
                End If
            Else
                MessageBox.Show("No Record For Updations")

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (txtPrevOutNo.Text.Trim() = "") Then
            btnChange_Click(Nothing, Nothing)
        Else
            If txtOutwardNo.Text.Trim() <> "" And OutwardId = 0 Then
                OutwardId = linq_obj.SP_Insert_Tbl_OutwardMaster(If(rbCustomer.Checked = True, AddressId, 0), txtCustomer.Text, txtAddress.Text, txtContactNo.Text, txtRemark.Text, dtOutDate.Value, txtOutEngineer.Text)
            End If
            If (OutwardId > 0) Then
                For i As Integer = 0 To tblItem.Rows.Count - 1
                    linq_obj.SP_Insert_Tbl_OutwardDetail(OutwardId,
                                                                Convert.ToInt64(tblItem.Rows(i)("Fk_CategoryId").ToString()),
                                                                Convert.ToInt64(tblItem.Rows(i)("Fk_RawmaterialId").ToString()),
                                                                Convert.ToInt64(tblItem.Rows(i)("Quanity").ToString()),
                                                                tblItem.Rows(i)("Unit").ToString(), tblItem.Rows(i)("Remarks").ToString())
                    linq_obj.SubmitChanges()

                Next
            End If
            bindGrid()

        End If

    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
    Private Sub txtPrevOutNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrevOutNo.Leave
        Try
            If (txtPrevOutNo.Text.Trim() <> "") Then
                Dim outData = linq_obj.SP_Select_Tbl_OutwardMaster_ById(Convert.ToInt64(txtPrevOutNo.Text.Trim())).ToList()
                If (outData.Count > 0) Then
                    OutwardId = Convert.ToInt64(outData(0).Pk_OutwardId)
                    bindItemGrid()
                    For i = 0 To DGVSellItems.RowCount - 1
                        tblItem.Rows.Add(DGVSellItems.Rows(i).Cells(0).Value, DGVSellItems.Rows(i).Cells(1).Value, DGVSellItems.Rows(i).Cells(2).Value,
                        DGVSellItems.Rows(i).Cells(3).Value,
                        DGVSellItems.Rows(i).Cells(4).Value,
                        DGVSellItems.Rows(i).Cells(5).Value,
                        DGVSellItems.Rows(i).Cells(6).Value,
                        DGVSellItems.Rows(i).Cells(7).Value,
                        DGVSellItems.Rows(i).Cells(8).Value,
                        DGVSellItems.Rows(i).Cells(9).Value)
                    Next

                    OutwardId = 0
                End If
            Else
                MessageBox.Show("No Data Found")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub txtQty_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.Leave
        'declare a variable for identify total sell and remaining stock
        flag = 0
        Dim itemTotalRem As Decimal
        Dim itemTotalForSell As Decimal
        'Dim cmd As New SqlCommand
        'cmd.CommandText = "SP_Get_Total_INOUT_Detail"
        'Dim objclass As New Class1
        'ds = objclass.GetStockData(cmd)
        'If ds.Tables(1).Rows.Count < 1 Then
        '    ' MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    itemTotalRem = 0
        'Else
        '    Dim count As Integer
        '    count = ds.Tables(1).Rows.Count
        '    If count > 0 Then
        '        For i As Integer = 0 To count - 1
        '            If (Convert.ToString(ds.Tables(1).Rows(i)(0)) = cmbCategory.SelectedValue AndAlso Convert.ToString(ds.Tables(1).Rows(i)(1)) = cmbRowMaterial.SelectedValue) Then
        '                itemTotalRem += Convert.ToDecimal(ds.Tables(1).Rows(i)(7))
        '                txtUnit.Text = Convert.ToString(ds.Tables(1).Rows(i)(9))
        '            End If
        '        Next
        '    End If
        'End If

        'add Navin 02-04-2015
        Dim productMain = linq_obj.SP_Get_Total_INOUT_DetailByCategoryID(Convert.ToInt32(cmbCategory.SelectedValue), Convert.ToInt32(cmbRowMaterial.SelectedValue)).ToList()
        For Each item As SP_Get_Total_INOUT_DetailByCategoryIDResult In productMain
            itemTotalRem = item.RemainingStock
        Next


        If DGVSellItems.Rows.Count < 1 Then
            itemTotalForSell = 0
            'MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim count As Integer
            count = DGVSellItems.Rows.Count
            If count > 0 Then
                For i As Integer = 0 To count - 1
                    If (Convert.ToString(DGVSellItems.Rows(i).Cells(1)) = cmbCategory.SelectedValue AndAlso Convert.ToString(DGVSellItems.Rows(i).Cells(2)) = cmbRowMaterial.SelectedValue) Then
                        itemTotalForSell += Convert.ToDecimal(DGVSellItems.Rows(i).Cells(8))
                    End If
                Next
            End If
        End If
        Dim enterQty As Integer

        If (txtQty.Text <> "") Then
            enterQty = Convert.ToDecimal(txtQty.Text)
        Else
            enterQty = 0
        End If
        itemTotalForSell += enterQty
        flag = 0
        If (itemTotalRem >= itemTotalForSell) Then
            flag = 0

        Else
            flag = 1

        End If


        If (flag = 1) Then
            MessageBox.Show("Please Enter Proper Qty")
        End If
    End Sub

    Private Sub rbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCustomer.CheckedChanged
        bindCustomerData()
    End Sub

    Private Sub txtCustomer_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomer.Leave
        If (txtCustomer.Text.Trim() <> "") Then

            Dim data = linq_obj.SP_Select_All_Tbl_OutwardMaster().ToList().Where(Function(t) t.CustomerName = txtCustomer.Text).ToList()

            For Each iteam As SP_Select_All_Tbl_OutwardMasterResult In data
                txtAddress.Text = iteam.Address
                txtContactNo.Text = iteam.ContactNo

            Next
        End If


    End Sub

    Private Sub btnDeleteSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSub.Click
        subClear()
        Dim result As DialogResult = MessageBox.Show("Are You Sure To Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            Dim cntSelect As Integer = DGVSellItems.SelectedRows.Count
            For Each dr As DataGridViewRow In DGVSellItems.SelectedRows
                Dim resDelete As Integer = linq_obj.SP_Delete_OutwardDetailById(Convert.ToInt32(dr.Cells(0).Value))
                linq_obj.SubmitChanges()
            Next
            MessageBox.Show("Successfully Deleted!!!")
            bindItemGrid()
        End If
    End Sub


    Private Sub txtTransEffect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTransEffect.Click
        Try
            linq_obj.SP_Update_Tbl_OutwardTransaction(OutwardId, True)
            linq_obj.SubmitChanges()
            btnAdd.Enabled = False
            btnChange.Enabled = False
            btnSave.Enabled = False

            btnDelete.Enabled = False
            btnDeleteSub.Enabled = False
            txtOutwardNo.BackColor = Color.Yellow
            btnREInward.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtcancelTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcancelTrans.Click
        Try
            linq_obj.SP_Update_Tbl_OutwardTransaction(OutwardId, False)
            linq_obj.SubmitChanges()
            btnAdd.Enabled = True
            btnChange.Enabled = True
            btnDelete.Enabled = True
            btnDeleteSub.Enabled = True
            btnSave.Enabled = True

            txtOutwardNo.BackColor = Color.White
            btnREInward.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        bindGrid()
    End Sub

    Private Sub DGVSellItems_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVSellItems.DoubleClick
        If DGVSellItems.SelectedRows.Count > 0 Then
            OutwardDetailId = DGVSellItems.SelectedCells(0).Value
            cmbCategory.SelectedValue = DGVSellItems.SelectedCells(1).Value
            cmbCategory_SelectionChangeCommitted(Nothing, Nothing)
            cmbRowMaterial.SelectedValue = DGVSellItems.SelectedCells(2).Value
            txtUnit.Text = DGVSellItems.SelectedRows(0).Cells("Unit").Value
            txtDetailRemarks.Text = DGVSellItems.SelectedRows(0).Cells("Remarks").Value
            txtQty.Text = DGVSellItems.SelectedRows(0).Cells("Quantity").Value
            If (txtOutwardNo.Text.Trim() = "") Then
                btnAdd.Text = "Update"
            Else
                rwIDDelDetail = DGVSellItems.CurrentCell.RowIndex
            End If

        End If
    End Sub


    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        If (txtEnqNo.Text <> "") Then
            Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text).ToList()
            If (data.Count > 0) Then
                txtCustomer.Text = data(0).Name
                txtAddress.Text = data(0).Address
                txtContactNo.Text = data(0).MobileNo
                txtRemark.Text = data(0).Remark
            End If
        End If


    End Sub

    Private Sub rbOther_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbOther.CheckedChanged
        bindCustomerData()
    End Sub

    Private Sub txtPrevOutNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrevOutNo.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub btnRICancelTrans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRICancelTrans.Click
        Try
            linq_obj.SP_Update_Tbl_ReInwardTransaction(REInwardId, False)
            linq_obj.SubmitChanges()
            btnRISubADD.Enabled = True
            btnRIChange.Enabled = True
            btnRIDelete.Enabled = True
            btnRISubDelete.Enabled = True

            frmReInward.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub btnREInward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnREInward.Click
        If (OutwardId > 0) Then
            grpReInward.Visible = True
            Dim x As Integer = 5, y As Integer = 5
            frmReInward = New Form()
            frmReInward.Text = "Enter ReInward Details"
            frmReInward.MaximizeBox = False
            frmReInward.MinimizeBox = False
            '  frmReInward.StartPosition = FormStartPosition.CenterScreen
            frmReInward.Controls.Add(grpReInward)
            grpReInward.Location = New System.Drawing.Point(x, y)
            frmReInward.Width = 700
            frmReInward.Height = 500
            grpReInward.Width = 659
            grpReInward.Height = 450


            'bindAll data

            bindREDropDown()

            txtSrNo.Text = IIf(DGReInwardDetail.Rows.Count = 0, 1, DGReInwardDetail.Rows.Count + 1)

            Dim dataRein = linq_obj.GetReInwardIdFromOutward(OutwardId).ToList()
            If (dataRein.Count > 0) Then
                REInwardId = Convert.ToInt64(dataRein(0).ReIwardId)
            Else
                REInwardId = 0
            End If

            bindAllREINData()
            frmReInward.ShowDialog()
        Else
            MessageBox.Show("No Outward Detail Found...")
        End If

    End Sub
    Public Sub bindAllREINData()
        Try

            Dim ReinData = linq_obj.SP_Select_All_Tbl_ReInwardMasterByOutward(OutwardId).ToList()
            If (ReinData.Count > 0) Then
                REInwardId = Convert.ToInt64(ReinData(0).Pk_ReInwardId)
                bindREInItemGrid()
                If (ReinData.Count > 0) Then
                    txttREINEngineer.Text = Convert.ToString(ReinData(0).EngineerName)
                    dtRIDate.Value = ReinData(0).ReInwardDate
                    txtRemark.Text = ReinData(0).Remarks
                    If (ReinData(0).Status = False) Then
                        btnAdd.Enabled = True
                        btnChange.Enabled = True
                        btnDelete.Enabled = True
                        btnDeleteSub.Enabled = True
                        txtOutwardNo.BackColor = Color.White
                    Else
                        btnAdd.Enabled = False
                        btnChange.Enabled = False
                        btnDelete.Enabled = False
                        btnDeleteSub.Enabled = False
                        txtOutwardNo.BackColor = Color.Yellow
                    End If
                End If
                If (REInwardId > 0) Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If
            Else
                ' MessageBox.Show("No Data Found")

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub btnRIBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRIBack.Click
        frmReInward.Close()
    End Sub

    Private Sub btnRITransEffect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRITransEffect.Click
        Try
            linq_obj.SP_Update_Tbl_ReInwardTransaction(REInwardId, True)
            linq_obj.SubmitChanges()
            btnRISubADD.Enabled = False
            btnRIChange.Enabled = False
            btnRIDelete.Enabled = False
            btnRISubDelete.Enabled = False

            frmReInward.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnRISubADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRISubADD.Click
        Try
            If txttREINEngineer.Text.Trim() <> "" And REInwardId = 0 Then
                REInwardId = linq_obj.SP_Insert_Tbl_RIInwardMaster(OutwardId, txttREINEngineer.Text, dtRIDate.Value, btnRIRemarks.Text, False)

            End If
            If (REInwardId > 0) Then
                If (btnRISubADD.Text = "Add") Then
                    'insert into  Details
                    linq_obj.SP_Insert_Tbl_RIInwardDetail(Convert.ToInt64(cmbRICategory.SelectedValue), Convert.ToInt64(cmbRIRawMaterial.SelectedValue), REInwardId, Convert.ToInt64(txtRIQty.Text), txtRIUnit.Text, txtRISubRemark.Text)
                    '  MessageBox.Show("Successfully Saved")
                Else
                    'insert into  Details
                    linq_obj.SP_Update_Tbl_ReInwardDetail(Convert.ToInt64(cmbRICategory.SelectedValue), Convert.ToInt64(cmbRIRawMaterial.SelectedValue), REInwardId, Convert.ToInt64(txtRIQty.Text), txtRIUnit.Text, txtRISubRemark.Text, REInwardDetailId)
                    ' MessageBox.Show("Successfully Saved")
                End If

            End If
            bindREInItemGrid()
            txtSrNo.Text = IIf(DGReInwardDetail.Rows.Count = 0, 1, DGReInwardDetail.Rows.Count + 1)
            subReINClear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Public Sub bindREInItemGrid()
        Dim itemData = linq_obj.SP_Select_All_Tbl_RIInwardDetailByRIInward(REInwardId).ToList()
        If (itemData.Count > 0) Then
            DGReInwardDetail.DataSource = itemData
            Dim rowCount As Integer
            rowCount = DGReInwardDetail.RowCount
            If rowCount > 0 Then
                DGReInwardDetail.Columns(0).Visible = False
                DGReInwardDetail.Columns(1).Visible = False
                DGReInwardDetail.Columns(2).Visible = False
                DGReInwardDetail.Columns(3).Visible = False
            End If
            txtSrNo.Text = IIf(DGReInwardDetail.Rows.Count = 0, 1, DGReInwardDetail.Rows.Count + 1)
        End If
    End Sub

    Private Sub btnRICancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRICancel.Click
        REclear()
    End Sub
    Public Sub REclear()
        txttREINEngineer.Text = ""
        btnRIRemarks.Text = ""
        dtRIDate.Value = Date.Now

        btnSave.Enabled = True
        btnAdd.Enabled = True
        btnChange.Enabled = True
        btnDelete.Enabled = True
        btnDeleteSub.Enabled = True
        txtOutwardNo.BackColor = Color.White
        REInwardId = 0

    End Sub



    Private Sub btnRINew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRINew.Click
        subReINClear()
    End Sub




    Protected Sub bindREDropDown()
        '   cmbRICategory.Items.Clear()
        Dim dt As New DataTable
        dt.Columns.Add("Id")
        dt.Columns.Add("Category")
        Dim productMain = linq_obj.SP_Select_All_ProductRegisterByOutward().ToList()
        For Each item As SP_Select_All_ProductRegisterByOutwardResult In productMain
            dt.Rows.Add(item.Pk_ProductRegisterId, item.CategoryName)
        Next
        Dim dr As DataRow = dt.NewRow()
        dr(1) = "Select"
        dt.Rows.InsertAt(dr, 0)
        cmbRICategory.DataSource = dt
        cmbRICategory.DisplayMember = "Category"
        cmbRICategory.ValueMember = "Id"
        cmbRICategory.AutoCompleteMode = AutoCompleteMode.Append
        cmbRICategory.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRICategory.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub




    Private Sub cmbRICategory_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRICategory.SelectionChangeCommitted
        Try


            If cmbRICategory.SelectedValue > 0 Then
                PRIDRE = cmbRICategory.SelectedValue
                Dim rowMaterial = linq_obj.SP_Select_All_RawMatrialByOutward(PRIDRE).ToList()
                cmbRIRawMaterial.DataSource = rowMaterial
                cmbRIRawMaterial.DisplayMember = "RowMaterialName"
                cmbRIRawMaterial.ValueMember = "Pk_RowMaterialId"
                cmbRIRawMaterial.AutoCompleteMode = AutoCompleteMode.Append
                cmbRIRawMaterial.DropDownStyle = ComboBoxStyle.DropDownList
                cmbRIRawMaterial.AutoCompleteSource = AutoCompleteSource.ListItems
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRISubDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRISubDelete.Click
        subReINClear()
        Dim result As DialogResult = MessageBox.Show("Are You Sure To Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            Dim cntSelect As Integer = DGReInwardDetail.SelectedRows.Count
            For Each dr As DataGridViewRow In DGReInwardDetail.SelectedRows
                Dim resDelete As Integer = linq_obj.SP_Delete_Tbl_RIInwardDetail(Convert.ToInt32(dr.Cells(0).Value))
                linq_obj.SubmitChanges()
            Next
            MessageBox.Show("Successfully Deleted!!!")
            bindREInItemGrid()
        End If
    End Sub

    Private Sub btnRIDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRIDelete.Click
        If (REInwardId > 0) Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                Dim res As Integer
                res = linq_obj.SP_Delete_Tbl_RIInwardStockMaster(REInwardId)
                res = linq_obj.SP_Delete_Tbl_RIInwardDetailByInward(REInwardId)
                MessageBox.Show("Delete Successfully..")
            End If
            btnRISubADD.Enabled = True
            btnRIChange.Enabled = True
            btnRIDelete.Enabled = True
            btnRISubDelete.Enabled = True
            txttREINEngineer.BackColor = Color.White
        Else
            MessageBox.Show("No Data Selected!!!")
        End If
    End Sub

    Private Sub btnRIChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRIChange.Click
        Try
            If (REInwardId > 0) Then
                Dim res As Integer

                res = linq_obj.SP_Update_Tbl_REInwardMaster(OutwardId, txttREINEngineer.Text, dtRIDate.Value, btnRIRemarks.Text)
                If (res >= 0) Then
                    linq_obj.SubmitChanges()
                    MessageBox.Show("Successfully Saved...")
                    bindGrid()
                End If

            Else
                MessageBox.Show("No Record For Updations")

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRISave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRISave.Click
        btnRIChange_Click(Nothing, Nothing)
    End Sub


    Private Sub cmbRIRawMaterial_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRIRawMaterial.SelectionChangeCommitted
        Try
            Dim dataRaw = linq_obj.SP_Select_RowMaterialMaster_ById(cmbRIRawMaterial.SelectedValue).ToList()
            If (dataRaw.Count > 0) Then
                txtRIUnit.Text = Convert.ToString(dataRaw(0).Unit)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GDVOutwardDetail_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GDVOutwardDetail.CellClick
        bindAllData()
    End Sub

    Private Sub GDVOutwardDetail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GDVOutwardDetail.CellContentClick

    End Sub

    Private Sub btnOutWard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutWard.Click
        If OutwardId > 0 Then
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter()
            Dim ds As New DataSet
            
            ''Create Dataset To XmlFile

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Select_Tbl_OutwardMaster_ById"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@Pk_OutwardId", SqlDbType.BigInt).Value = OutwardId



            da.SelectCommand = cmd
            da.Fill(ds, "SP_Select_Tbl_OutwardMaster_ById")

            ds.AcceptChanges()
            ds.Tables("SP_Select_Tbl_OutwardMaster_ById").WriteXml(Application.StartupPath & "\XmlFile\SP_Select_Tbl_OutwardMaster_ById.xml")

            cmd = New SqlCommand
            da = New SqlDataAdapter
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Select_All_Tbl_OutwardDetail"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@Fk_outwardid", SqlDbType.Int).Value = OutwardId



            da.SelectCommand = cmd
            da.Fill(ds, "SP_Select_All_Tbl_OutwardDetail")

            ds.AcceptChanges()


            ds.Tables("SP_Select_All_Tbl_OutwardDetail").WriteXml(Application.StartupPath & "\XmlFile\SP_Select_All_Tbl_OutwardDetail.xml")
            Dim rpt As New RptOutWardReport
            

            rpt.Database.Tables(0).SetDataSource(ds.Tables(0))
            rpt.Database.Tables(1).SetDataSource(ds.Tables(1))

            rpt.SetParameterValue("fCompanyName", Class1.g_sCompanyName)
            

            Dim frm As New FrmCommanReportView(rpt)
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.Show()
        Else
            MessageBox.Show("Data Not Found...")
        End If
    End Sub


End Class