﻿Public Class ProductRegisterMaster

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PRID As Long

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindGrid()
        PRID = 0
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
            dataView.RowFilter = "([DetailName] like 'ProductMaster')"
            If (dataView.Count > 0) Then
                dv = dataView.ToTable()

                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then

                            btnSave.Enabled = True
                        Else

                            btnSave.Enabled = False
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
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim tmpId As Long
        tmpId = 0
        Try
            Dim result As DialogResult = MessageBox.Show("Are You Sure To Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tmpId = Convert.ToInt64(Me.DGVcategory.SelectedCells(0).Value)
                If tmpId <> 0 Then
                    linq_obj.SP_Delete_ProductRegisterMaster(tmpId)
                    linq_obj.SubmitChanges()
                    lblError.Text = "Successfully Deleted"
                    bindGrid()
                    txtTotRecords.Text = getTotalEntry().ToString()
                Else
                    MessageBox.Show("No Row Selected For Delete")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click


        If txtCategoryName.Text <> "" Then
            If btnSave.Text = "Save" Then


                PRID = linq_obj.SP_Insert_ProductRegisterMaster(txtCategoryName.Text.Trim(), True)
                If (PRID > 0) Then
                    lblError.Text = "Successfully Inserted"
                    bindGrid()
                    txtCategoryName.Text = ""
                Else
                    lblError.Text = "Error In Insertion"
                End If
            Else
                Dim tmp As Integer
                tmp = linq_obj.SP_Update_ProductRegisterMaster(txtCategoryName.Text, True, PRID)

                If tmp >= 0 Then
                    linq_obj.SubmitChanges()
                    bindGrid()
                    lblError.Text = "Successfully Updated"
                    txtCategoryName.Text = ""
                    btnSave.Text = "Save"
                Else
                    lblError.Text = "Error In Updation"
                End If




            End If

        End If
        txtTotRecords.Text = getTotalEntry().ToString()
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
            DGVcategory.DataSource = dt
            DGVcategory.Columns(0).Visible = False

        End If
    End Sub
    Public Function getTotalEntry() As Long
        Return DGVcategory.Rows.Count
    End Function

    Public Sub clean()
        txtCategoryName.Text = ""
        lblError.Text = ""
        btnSave.Text = "Save"
    End Sub

    Private Sub DGVcategory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVcategory.DoubleClick
        PRID = Convert.ToInt64(Me.DGVcategory.SelectedCells(0).Value)
        txtCategoryName.Text = Me.DGVcategory.SelectedCells(2).Value
        btnSave.Text = "Update"

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clean()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            If (txtcategory.Text.Trim() <> "") Then
                Dim datatableTest As DataTable
                datatableTest = DGVcategory.DataSource
                Dim dv As DataView
                dv = datatableTest.DefaultView

                dv.RowFilter = "Category like '%" & txtcategory.Text & "%'"
                If (dv.Count > 0) Then
                    datatableTest = dv.ToTable()
                    DGVcategory.DataSource = datatableTest
                Else
                    DGVcategory.DataSource = Nothing
                End If

            Else
                bindGrid()
            End If
          

            txtTotRecords.Text = getTotalEntry().ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        bindGrid()
        txtTotRecords.Text = getTotalEntry().ToString()
    End Sub

    Private Sub ProductRegisterMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
End Class