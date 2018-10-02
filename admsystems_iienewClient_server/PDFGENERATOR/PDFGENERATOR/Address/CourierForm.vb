﻿Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class CourierForm
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Courier_ID As Integer
    Dim Address_ID As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()

        ddlCategory_Bind()
        ddlSubCategory_Bind()
        GvCourier_Bind()
        AutoComplete_Text()
        If (lblHeader.Text = "Add Courier") Then
            Button_Status()
            btnSubmit.Visible = True
        Else
            Button_Status()
            btnUpdate.Visible = True
        End If
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
            dataView.RowFilter = "([DetailName] like 'Courier')"

            If (dataView.Count > 0) Then


                dv = dataView.ToTable()

                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            btnSubmit.Enabled = True
                        Else
                            btnSubmit.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsUpdate") = True) Then
                            btnUpdate.Enabled = True
                        Else
                            btnUpdate.Enabled = False
                        End If



                    Next
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub AutoCompated_Text()

        Dim GetCourier = linq_obj.SP_Get_CourierList().ToList()
        For Each iteam As SP_Get_CourierListResult In GetCourier
            txtName.AutoCompleteCustomSource.Add(iteam.Name)
            txtArea.AutoCompleteCustomSource.Add(iteam.Area)
            txtCity.AutoCompleteCustomSource.Add(iteam.City)
            txtState.AutoCompleteCustomSource.Add(iteam.State)
            txtDistrict.AutoCompleteCustomSource.Add(iteam.District)
            'Search Filter
            txtSearchName.AutoCompleteCustomSource.Add(iteam.Name)
            txtSearchCity.AutoCompleteCustomSource.Add(iteam.City)
            txtSearchDistrict.AutoCompleteCustomSource.Add(iteam.District)


        Next


    End Sub
    Public Sub ddlCategory_Bind()

        ddlCategory.Items.Clear()
        Dim category = linq_obj.SP_Get_AddressCategory().ToList()
        ddlCategory.DataSource = category
        ddlCategory.DisplayMember = "Category"
        ddlCategory.ValueMember = "Pk_AddressCategoryID"
        ddlCategory.AutoCompleteMode = AutoCompleteMode.Append
        ddlCategory.DropDownStyle = ComboBoxStyle.DropDownList
        ddlCategory.AutoCompleteSource = AutoCompleteSource.ListItems

        For Each iteam As SP_Get_AddressCategoryResult In category
            txtSearchCategory.AutoCompleteCustomSource.Add(iteam.Category)
        Next


    End Sub
    Public Sub ddlSubCategory_Bind()

        ddlSubCategory.Items.Clear()

        Dim subcategory = linq_obj.SP_Get_AddSubCategory().ToList()

        ddlSubCategory.DataSource = subcategory
        ddlSubCategory.DisplayMember = "SubCategory"
        ddlSubCategory.ValueMember = "Pk_AddressSubID"
        ddlSubCategory.AutoCompleteMode = AutoCompleteMode.Append
        ddlSubCategory.DropDownStyle = ComboBoxStyle.DropDownList
        ddlSubCategory.AutoCompleteSource = AutoCompleteSource.ListItems


    End Sub
    Public Sub GvCourier_Bind()

        bindGrid()
    End Sub
    Public Sub Button_Status()
        btnSubmit.Visible = False
        btnUpdate.Visible = False
    End Sub
    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            linq_obj.SP_Insert_Update_CourierMaster(0, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, cmbPrintType.Text, txtName.Text, txtAddress.Text, txtArea.Text, txtCity.Text, txtPincode.Text, txtTaluka.Text, txtDistrict.Text, txtState.Text, txtContactPer.Text, txtContactNo.Text, txtCourierTh.Text, txtDocketNo.Text, txtCreateDate.Text, txtRecBy.Text, txtFollowUpBy.Text, txtRemarks.Text, txtEnqNo.Text, txtRecDate.Text, cmbSubCagtegory1.Text)
            linq_obj.SubmitChanges()
            GvCourier_Bind()
            MessageBox.Show("Submit Sucessfully.....")
            SetClean()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub SetClean()
        txtName.Text = ""
        txtAddress.Text = ""
        txtArea.Text = ""
        txtCity.Text = ""
        txtPincode.Text = ""
        txtTaluka.Text = ""
        txtDistrict.Text = ""
        txtState.Text = ""
        txtContactPer.Text = ""
        txtContactNo.Text = ""
        txtCreateDate.Text = ""
        txtCourierTh.Text = ""
        txtDocketNo.Text = ""
        Courier_ID = 0
        txtRecBy.Text = ""
        txtFollowUpBy.Text = ""
        txtRemarks.Text = ""
        txtEnqNo.Text = ""
        txtRecDate.Text = ""
        cmbSubCagtegory1.Text = ""
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            linq_obj.SP_Insert_Update_CourierMaster(Courier_ID, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, cmbPrintType.Text, txtName.Text, txtAddress.Text, txtArea.Text, txtCity.Text, txtPincode.Text, txtTaluka.Text, txtDistrict.Text, txtState.Text, txtContactPer.Text, txtContactNo.Text, txtCourierTh.Text, txtDocketNo.Text, txtCreateDate.Text, txtRecBy.Text, txtFollowUpBy.Text, txtRemarks.Text, txtEnqNo.Text, txtRecDate.Text, cmbSubCagtegory1.Text)
            linq_obj.SubmitChanges()
            MessageBox.Show("Update Sucessfully.....")
            GvCourier_Bind()
            SetClean()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub bindData()
        Try
            lblHeader.Text = "Edit Courier"
            Button_Status()
            btnUpdate.Visible = True
            Courier_ID = Convert.ToInt32(Me.GvCourier.SelectedCells(0).Value)
            Dim courier_get = linq_obj.SP_Get_CourierList().Where(Function(c) c.Pk_CourierID = Courier_ID).ToList()
            For Each item As SP_Get_CourierListResult In courier_get
                cmbPrintType.SelectedIndex = cmbPrintType.Items.IndexOf(Convert.ToString(item.PrintType))
                ddlCategory.SelectedValue = item.FK_CategoryID
                ddlSubCategory.SelectedValue = item.FK_SubCategoryID
                txtName.Text = item.Name
                txtAddress.Text = item.Address
                txtArea.Text = item.Area
                txtCity.Text = item.City
                txtPincode.Text = item.Pincode
                txtTaluka.Text = item.Taluka
                txtDistrict.Text = item.District
                txtState.Text = item.State
                txtContactPer.Text = item.ContactPerson
                txtContactNo.Text = item.ContactNo
                txtCreateDate.Text = item.CreateDate
                txtCourierTh.Text = item.CourierTh
                txtDocketNo.Text = item.DocketNo
                txtRecBy.Text = item.RecBy
                txtRecDate.Text = item.RecDate
                txtRemarks.Text = item.Remarks
                txtFollowUpBy.Text = item.FollowBy
                txtEnqNo.Text = item.EnqNo
                cmbSubCagtegory1.Text = item.SubCategory
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub GvCourier_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCourier.DoubleClick
        bindData()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Button_Status()
        SetClean()
        btnSubmit.Visible = True
        lblHeader.Text = "Add Courier"

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindGrid()
    End Sub

    Private Sub GvCourier_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles GvCourier.PreviewKeyDown
        bindData()
    End Sub
    Public Sub bindGrid()
        Dim dt As New DataTable
        dt.Columns.Add("PKID")
        dt.Columns.Add("Category")
        dt.Columns.Add("SubCategory")
        dt.Columns.Add("Name")

        Try
            Dim oDataAccess = New DataAccess()
            Dim cmd As New SqlCommand()

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Get_CourierList1"


            If txtSearchName.Text.Trim() <> "" Then
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtSearchName.Text
            ElseIf txtSearchCategory.Text <> "" Then
                cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = txtSearchCategory.Text
            ElseIf txtSearchState.Text <> "" Then
                cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = txtSearchState.Text
            ElseIf txtSearchDistrict.Text.Trim() <> "" Then
                cmd.Parameters.Add("@District", SqlDbType.VarChar).Value = txtSearchDistrict.Text
            ElseIf txtSearchCity.Text.Trim() <> "" Then
                cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = txtSearchCity.Text
            End If


            dt = oDataAccess.ExecuteDataTable(cmd)
            GvCourier.DataSource = Nothing
            txtTotalRecord.Text = "0"
            If dt.Rows.Count > 0 Then
                GvCourier.DataSource = dt
                txtSearchCategory.Text = ""
                GvCourier.Columns(0).Visible = False
                txtTotalRecord.Text = dt.Rows.Count
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If (Courier_ID > 0) Then


                Dim result As DialogResult = MessageBox.Show("Are You Sure Delete Courier?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                If result = DialogResult.Yes Then
                    linq_obj.SP_Delete_Courier(Courier_ID)
                    linq_obj.SubmitChanges()
                    MessageBox.Show("Successfully Deleted...")
                    SetClean()
                    GvCourier_Bind()
                    Button_Status()
                    btnSubmit.Visible = True
                    ddlCategory.Focus()
                Else

                End If
            Else
                MessageBox.Show("No Courier Data Found For Delete...")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.Leave

    End Sub
    Public Sub AutoComplete_Text()
        Dim data = linq_obj.SP_Get_AddressListAutoComplete("Name").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In data
            txtName.AutoCompleteCustomSource.Add(iteam.Result)
        Next

        Dim dataEnq = linq_obj.SP_Get_AddressListAutoComplete("EnqNo").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In dataEnq
            txtEnqNo.AutoCompleteCustomSource.Add(iteam.Result)
        Next
    End Sub
    Public Sub bindAddressData()
        Dim Claient = linq_obj.SP_Get_AddressListById(Address_ID).ToList()
        For Each item As SP_Get_AddressListByIdResult In Claient
            txtName.Text = item.Name
            txtAddress.Text = item.Address
            txtCity.Text = item.City
            txtState.Text = item.State
            txtDistrict.Text = item.District
            txtTaluka.Text = item.Taluka
            txtPincode.Text = item.Pincode
            txtArea.Text = item.Area
            txtContactNo.Text = item.MobileNo
            txtContactPer.Text = item.ContactPerson
        Next
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        'Dim criteria As String
        'criteria = " and "

        'If txtSearchName.Text.Trim() <> "" Then
        '    criteria = criteria + " Name like '%" + txtSearchName.Text + "%' and"
        'End If
        'If txtSearchState.Text.Trim() <> "" Then
        '    criteria = criteria + " State like '%" + txtSearchState.Text + "%' and"
        'End If
        'If txtSearchDistrict.Text.Trim() <> "" Then
        '    criteria = criteria + " District like '%" + txtSearchDistrict.Text + "%' and"
        'End If
        'If txtSearchCity.Text.Trim() <> "" Then
        '    criteria = criteria + " City like '%" + txtSearchCity.Text + "%' and"
        'End If
        'If txtSearchCategory.Text <> "" Then
        '    criteria = criteria + " Category like '" + txtSearchCategory.Text + "' and"
        'End If

        If (rb12Label.Checked = True) Then
            Dim ds As New DataSet
            Dim rpt As New ReportDocument
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Search_CourierPrint"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.AddWithValue("@start", dtFrom.Value)
            cmd.Parameters.AddWithValue("@end", dtTo.Value)
            cmd.Parameters.AddWithValue("@type", "Label")

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Search_CourierPrint")

            Class1.WriteXMlFile(ds, "SP_Search_CourierPrint", "SP_Search_CourierPrint")

            rpt.Load(Application.StartupPath & "\Reports\CourierPrint12.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Search_CourierPrint"))
            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()
        ElseIf (rb21Label.Checked = True) Then
            Dim ds As New DataSet
            Dim rpt As New ReportDocument
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Search_CourierPrint"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.AddWithValue("@start", dtFrom.Value)
            cmd.Parameters.AddWithValue("@end", dtTo.Value)
            cmd.Parameters.AddWithValue("@type", "Label")

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Search_CourierPrint")

            Class1.WriteXMlFile(ds, "SP_Search_CourierPrint", "SP_Search_CourierPrint")

            rpt.Load(Application.StartupPath & "\Reports\CourierPrint21.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Search_CourierPrint"))
            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()
        ElseIf (rbList.Checked = True) Then
            Dim ds As New DataSet
            Dim rpt As New ReportDocument
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Search_CourierPrint"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.AddWithValue("@start", dtFrom.Value)
            cmd.Parameters.AddWithValue("@end", dtTo.Value)
            cmd.Parameters.AddWithValue("@type", "List")

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Search_CourierPrint")

            Class1.WriteXMlFile(ds, "SP_Search_CourierPrint", "SP_Search_CourierPrint")

            rpt.Load(Application.StartupPath & "\Reports\Rpt_CourierList.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Search_CourierPrint"))
            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()
        Else
            MessageBox.Show("Select Any Type ...")
        End If
    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        If (txtEnqNo.Text <> "") Then
            Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text).ToList()
            If (data.Count > 0) Then
                Address_ID = data(0).Pk_AddressID
                bindAddressData()
            End If

        End If
    End Sub

    Private Sub CourierForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        bindGrid()

        For Each control As Control In Me.Controls
            ' The textbox control.
            Dim parentControl As New Control


            If (control.GetType() Is GetType(GroupBox)) Then
                Dim grpBox As GroupBox = TryCast(control, GroupBox)
                parentControl = grpBox
            ElseIf (control.GetType() Is GetType(TabControl)) Then
                Dim TC As TabControl = TryCast(control, TabControl)
                parentControl = TC
            ElseIf (control.GetType() Is GetType(Panel)) Then
                Dim Panel As Panel = TryCast(control, Panel)
                parentControl = Panel

            Else
                Try
                    parentControl = control
                Catch ex As Exception

                End Try

            End If

            For Each subcontrol As Control In parentControl.Controls
                If (subcontrol.GetType() Is GetType(TabControl)) Then
                    For Each subcontrolTwo As Control In subcontrol.Controls
                        If (subcontrolTwo.GetType() Is GetType(TabPage)) Then
                            For Each subcontrolthree As Control In subcontrolTwo.Controls
                                If (subcontrolthree.GetType() Is GetType(Panel)) Then
                                    For Each subcontrolFour As Control In subcontrolthree.Controls
                                        If (subcontrolFour.GetType() Is GetType(TextBox)) Then
                                            Dim textBox As TextBox = TryCast(subcontrolFour, TextBox)

                                            ' If not null, set the handler.
                                            If textBox IsNot Nothing Then
                                                AddHandler textBox.Enter, AddressOf TextBox_Enter
                                                AddHandler textBox.Leave, AddressOf TextBox_Leave
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
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

    Private Sub btnSearchCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCancel.Click
        GvCourier_Bind()
    End Sub

  
    
    Private Sub btnDateSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDateSearch.Click
        Dim dt As New DataTable
        dt.Columns.Add("PKID")
        dt.Columns.Add("Category")
        dt.Columns.Add("SubCategory")
        dt.Columns.Add("Name")

        Try
            Dim oDataAccess = New DataAccess()
            Dim cmd As New SqlCommand()

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Get_CourierList1"



            cmd.Parameters.Add("@dtFrom", SqlDbType.DateTime).Value = dtFrom.Value
            cmd.Parameters.Add("@dtTo", SqlDbType.DateTime).Value = dtTo.Value


            dt = oDataAccess.ExecuteDataTable(cmd)
            GvCourier.DataSource = Nothing
            txtTotalRecord.Text = 0
            If dt.Rows.Count > 0 Then
                GvCourier.DataSource = dt
                txtSearchCategory.Text = ""
                GvCourier.Columns(0).Visible = False
                txtTotalRecord.Text = dt.Rows.Count
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GvCourier_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvCourier.CellContentClick

    End Sub
End Class