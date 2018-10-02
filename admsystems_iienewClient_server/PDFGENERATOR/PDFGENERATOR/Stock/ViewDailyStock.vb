﻿Public Class ViewDailyStock

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindDropDown()

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
                Dim rowMaterial = linq_obj.SP_Select_All_RowMaterialMaster(Convert.ToInt64(cmb_Category.SelectedValue)).ToList()
                Cmb_RawMaterial.DataSource = rowMaterial
                Cmb_RawMaterial.DisplayMember = "RowMaterialName"
                Cmb_RawMaterial.ValueMember = "Pk_RowMaterialId"
                Cmb_RawMaterial.AutoCompleteMode = AutoCompleteMode.Append
                Cmb_RawMaterial.DropDownStyle = ComboBoxStyle.DropDownList
                Cmb_RawMaterial.AutoCompleteSource = AutoCompleteSource.ListItems
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            If cmb_Category.Text <> "Select" Then
                Dim data = linq_obj.getTodayInOutData(Convert.ToInt64(cmb_Category.SelectedValue), Convert.ToInt64(Cmb_RawMaterial.SelectedValue)).ToList()
                For Each iteam As getTodayInOutDataResult In data

                    lblRemainingStock.Text = iteam.RemainingStock


                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        cmb_Category.SelectedIndex = 0
        dtDate.Value = DateTime.Now
        lblOpening.Text = ""
        lblTodayInward.Text = ""
        lblTotal.Text = ""
        lblTotalOutward.Text = ""
        lblClosing.Text = ""

    End Sub
End Class