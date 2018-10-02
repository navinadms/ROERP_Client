Public Class RowMaterialMaster

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PRID As Long
    Dim RWID As Long
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindDropDown()
        bindGrid()
        PRID = 0
        txtTotRecords.Text = getTotalEntry().ToString()
        txtEntryCode.Text = cmbCategory.SelectedValue
        PRID = cmbCategory.SelectedValue
        bindRowGrid()
        txtProdNo.Text = IIf(dgRowMaterial.Rows.Count = 0, 1, dgRowMaterial.Rows.Count)
        'hide first Id Columns
        If dgRowMaterial.Columns.Count > 0 Then
            dgRowMaterial.Columns(0).Visible = False

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
            dataView.RowFilter = "([DetailName] like 'RawMaterial')"

            If (dataView.Count > 0) Then
                dv = dataView.ToTable()
                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            btnAddNew.Enabled = True
                            btnsave.Enabled = True
                        Else
                            btnAddNew.Enabled = False
                            btnsave.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsUpdate") = True) Then

                        Else

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
    Public Function getTotalEntry() As Long
        Return dgCategories.Rows.Count - 1
    End Function
    Public Sub bindDropDown()
        cmbCategory.Items.Clear()
        Dim productMain = linq_obj.SP_Select_All_ProductRegisterMaster().ToList()
        cmbCategory.DataSource = productMain
        cmbCategory.DisplayMember = "CategoryName"
        cmbCategory.ValueMember = "Pk_ProductRegisterId"
        cmbCategory.AutoCompleteMode = AutoCompleteMode.Append
        cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCategory.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Public Sub bindGrid()
        Dim prData = linq_obj.SP_Select_All_ProductRegisterMaster().ToList()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("SRNO")
        dt.Columns.Add("Category")
        For Each item As SP_Select_All_ProductRegisterMasterResult In prData
            dt.Rows.Add(item.Pk_ProductRegisterId, item.SRNO, item.CategoryName)
        Next
        If (dt.Rows.Count > 0) Then
            dgCategories.DataSource = dt
            dgCategories.Columns(0).Visible = False

        End If

    End Sub
    Public Sub subClear()
        txtRMName.Text = ""
        txtReorder.Text = ""
        txtUnit.Text = ""
        txtEntryCode.Text = ""
        txtProdNo.Text = IIf(dgRowMaterial.Rows.Count = 0, 1, dgRowMaterial.Rows.Count)
        btnsave.Text = "Add"
        cmbCategory.Enabled = True
    End Sub
    Public Sub bindRowGrid()
        Dim rwData = linq_obj.SP_Select_All_RowMaterialMaster(PRID).ToList()
        If (rwData.Count > 0) Then


            Dim dt As New DataTable
            dt.Columns.Add("ID")
            dt.Columns.Add("No.")
            dt.Columns.Add("Category")
            dt.Columns.Add("RowMaterial")
            dt.Columns.Add("Unit")
            dt.Columns.Add("ReOrder")
            dt.Columns.Add("CategoryId")
            For Each item As SP_Select_All_RowMaterialMasterResult In rwData
                dt.Rows.Add(item.Pk_RowMaterialId, item.SrNo, item.CategoryName, item.RowMaterialName, item.Unit, item.ReOrder, item.Fk_ProductRegisterId)
            Next
            dgRowMaterial.DataSource = dt
            dgRowMaterial.Columns("CategoryId").Visible = False

        End If
    End Sub
    Private Sub dgCategories_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgCategories.DoubleClick
        PRID = Convert.ToInt64(Me.dgCategories.SelectedCells(0).Value)
        bindRowGrid()
        cmbCategory.SelectedValue = PRID
        txtEntryCode.Text = PRID
        txtProdNo.Text = IIf(dgRowMaterial.Rows.Count = 0, 1, dgRowMaterial.Rows.Count)

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tmpId As Long
        tmpId = 0
        Try
            Dim result As DialogResult = MessageBox.Show("Are You Sure To Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tmpId = Convert.ToInt64(Me.dgRowMaterial.SelectedCells(0).Value)
                If tmpId <> 0 Then
                    linq_obj.SP_Delete_RowMaterialMaster(tmpId)
                    linq_obj.SubmitChanges()
                    'lblError.Text = "Successfully Deleted"
                    bindRowGrid()

                Else
                    MessageBox.Show("No Row Selected For Delete...")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("No Row Selected For Delete...")
        End Try

    End Sub

    Private Sub btnSaveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAll.Click
        Clear()
    End Sub

    Public Sub Clear()
        txtRMName.Text = ""
        txtReorder.Text = ""
        txtUnit.Text = ""
        txtEntryCode.Text = ""
        txtcategory.Text = ""
        txtEntryCode.Text = ""
        txtUnit.Text = ""
        cmbCategory.Enabled = True
        dgRowMaterial.DataSource = Nothing
        txtProdNo.Text = IIf(dgRowMaterial.Rows.Count = 0, 1, dgRowMaterial.Rows.Count)
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Clear()
    End Sub

    Private Sub cmbCategory_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectionChangeCommitted
        Try


            PRID = cmbCategory.SelectedValue
            txtEntryCode.Text = PRID
            bindRowGrid()
            txtProdNo.Text = IIf(dgRowMaterial.Rows.Count = 0, 1, dgRowMaterial.Rows.Count)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RowMaterialMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        subClear()
    End Sub

    Private Sub btnBackP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackP.Click
        Me.Close()
    End Sub

    Private Sub btncancelP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelP.Click
        Clear()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If (txtcategory.Text.Trim() <> "") Then
                Dim datatableTest As DataTable
                datatableTest = dgCategories.DataSource
                Dim dv As DataView
                dv = datatableTest.DefaultView
                dv.RowFilter = "Category like '%" & txtcategory.Text & "%'"
                If (dv.Count > 0) Then
                    datatableTest = dv.ToTable()
                    dgCategories.DataSource = datatableTest
                Else
                    dgCategories.DataSource = Nothing
                End If

            Else
                bindGrid()
            End If
            txtTotRecords.Text = getTotalEntry().ToString()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        bindGrid()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If (txtRMName.Text <> "") Then
                If btnsave.Text = "Add" Then
                    Dim tmp As Integer
                    tmp = linq_obj.SP_Insert_RowMaterialMaster(txtRMName.Text, txtUnit.Text, IIf(txtReorder.Text <> "", Convert.ToInt64(txtReorder.Text), 0), PRID, True)
                    If (tmp > 0) Then
                        linq_obj.SubmitChanges()
                        txtRMName.Text = ""
                        txtReorder.Text = ""
                        txtUnit.Text = ""
                        bindRowGrid()
                        txtProdNo.Text = IIf(dgRowMaterial.Rows.Count = 0, 1, dgRowMaterial.Rows.Count)
                    End If
                Else
                    Dim tmp As Integer
                    tmp = linq_obj.SP_Update_RowMaterialMaster(txtRMName.Text, txtUnit.Text, IIf(txtReorder.Text <> "", Convert.ToInt64(txtReorder.Text), 0), PRID, True, RWID)
                    If tmp >= 0 Then
                        linq_obj.SubmitChanges()
                        cmbCategory.Enabled = True
                        linq_obj.SubmitChanges()
                        txtRMName.Text = ""
                        txtReorder.Text = ""
                        txtUnit.Text = ""
                        bindRowGrid()
                        txtProdNo.Text = IIf(dgRowMaterial.Rows.Count = 0, 1, dgRowMaterial.Rows.Count)

                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgRowMaterial_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgRowMaterial.DoubleClick
        Try
            RWID = Convert.ToInt64(dgRowMaterial.SelectedCells(0).Value)
            PRID = dgRowMaterial.SelectedRows(0).Cells("CategoryId").Value
            ' cmbCategory.SelectedValue = dgRowMaterial.SelectedRows(0).Cells("CategoryId").Value
            'cmb_Category_SelectionChangeCommitted(Nothing, Nothing)
            txtRMName.Text = dgRowMaterial.SelectedRows(0).Cells("RowMaterial").Value
            txtUnit.Text = dgRowMaterial.SelectedRows(0).Cells("Unit").Value
            txtReorder.Text = dgRowMaterial.SelectedRows(0).Cells("ReOrder").Value
            btnsave.Text = "Update"
            cmbCategory.Enabled = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged

    End Sub
End Class