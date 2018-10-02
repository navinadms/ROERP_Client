

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Public Class SubCategoryForm
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim SubCategory_ID As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ddlCtegory_Bind()
        GvSubCategory_Bind()
        If (lblHeader.Text = "Add Sub Category") Then
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
            dataView.RowFilter = "([DetailName] like 'AddressSubCategory')"

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
                        If (dv.Rows(RowCount)("IsDelete") = True) Then

                        Else

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
    Public Sub Button_Status()
        btnSubmit.Visible = False
        btnUpdate.Visible = False
    End Sub

    Public Sub ddlCtegory_Bind()
        ddlCategory.Items.Clear()
        Dim category = linq_obj.SP_Get_AddressCategory().ToList()
        ddlCategory.DataSource = category
        ddlCategory.DisplayMember = "Category"
        ddlCategory.ValueMember = "Pk_AddressCategoryID"
        ddlCategory.AutoCompleteMode = AutoCompleteMode.Append
        ddlCategory.DropDownStyle = ComboBoxStyle.DropDownList
        ddlCategory.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Public Sub GvSubCategory_Bind()
        Try
            Dim getdata = linq_obj.SP_Get_AddSubCategory().ToList()
            GvSubCategory.DataSource = getdata
            GvSubCategory.Columns(0).Visible = False
            GvSubCategory.Columns(1).Visible = False
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            linq_obj.SP_Insert_Update_AddressSubCategory_Master(0, ddlCategory.SelectedValue, txtSubCategory.Text)
            linq_obj.SubmitChanges()
            SetClean()
            GvSubCategory_Bind()
            MessageBox.Show("Add Sucessfully...")
        Catch ex As Exception

        End Try
    End Sub
    Public Sub SetClean()
        txtSubCategory.Text = ""
    End Sub

    Private Sub GvSubCategory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSubCategory.DoubleClick
        bindData()
    End Sub
    Public Sub bindData()
        Button_Status()
        btnUpdate.Visible = True
        lblHeader.Text = "Edit Sub Category"
        SubCategory_ID = Convert.ToInt32(Me.GvSubCategory.SelectedCells(0).Value)
        ddlCategory.SelectedValue = Me.GvSubCategory.SelectedCells(1).Value
        txtSubCategory.Text = Me.GvSubCategory.SelectedCells(3).Value
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Button_Status()
        btnSubmit.Visible = True
        lblHeader.Text = "Add Sub Category"
        SetClean()


    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

            linq_obj.SP_Insert_Update_AddressSubCategory_Master(SubCategory_ID, ddlCategory.SelectedValue, txtSubCategory.Text)
            linq_obj.SubmitChanges()
            SetClean()
            GvSubCategory_Bind()
            MessageBox.Show("Add Sucessfully...")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GvSubCategory_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles GvSubCategory.PreviewKeyDown
        bindData()
    End Sub
End Class