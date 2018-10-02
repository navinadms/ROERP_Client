Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Configuration

Public Class AddressCateogry

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Category_ID As Integer


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        GvCategory_Bind()
        If (lblHeader.Text = "Add Category") Then
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
            dataView.RowFilter = "([DetailName] like 'AddressCategory')"

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

    Public Sub GvCategory_Bind()

        Try
            Dim getdata = linq_obj.SP_Get_AddressCategory().ToList()
            GvCategory.DataSource = getdata
            GvCategory.Columns(0).Visible = False
        Catch ex As Exception

        End Try

    End Sub
    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try

            linq_obj.SP_Insert_update_AddressCategory_Master(0, txtCategory.Text)
            linq_obj.SubmitChanges()
            SetClean()
            MessageBox.Show("Insert Sucessfully....")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())

        End Try
        GvCategory_Bind()

    End Sub
    Public Sub SetClean()
        txtCategory.Text = ""
    End Sub
    Public Sub Button_Status()
        btnSubmit.Visible = False
        btnUpdate.Visible = False
    End Sub
    Public Sub bindData()
        Button_Status()
        btnUpdate.Visible = True
        lblHeader.Text = "Edit Category"
        Category_ID = Convert.ToInt32(Me.GvCategory.SelectedCells(0).Value)
        txtCategory.Text = Me.GvCategory.SelectedCells(1).Value
    End Sub
    Private Sub GvCategory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCategory.DoubleClick
        bindData()
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            linq_obj.SP_Insert_update_AddressCategory_Master(Category_ID, txtCategory.Text)
            linq_obj.SubmitChanges()
            SetClean()
            MessageBox.Show("Update Sucessfully....")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())

        End Try
        GvCategory_Bind()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Button_Status()
        btnSubmit.Visible = True
        lblHeader.Text = "Add Category"
    End Sub
    Private Sub GvCategory_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles GvCategory.PreviewKeyDown
        bindData()
    End Sub
End Class