
Imports System.Windows.Forms.DataGridView
Imports System.Windows.Forms.BindingSource
Imports System.Data.SqlClient



Public Class AddUserPermissionMaster
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Private Sub AddUserPermissionMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoComplete_Text()
        bindDropdown()
        bindSoftwareList()

    End Sub

    Public Sub AutoComplete_Text()

    End Sub

    Public Sub bindDropdown()
        Try
            cmbUserList.DataSource = Nothing
            Dim users = linq_obj.SP_Get_UserList().ToList()
            cmbUserList.DataSource = users
            cmbUserList.DisplayMember = "UserName"
            cmbUserList.ValueMember = "Pk_UserId"
            cmbUserList.AutoCompleteMode = AutoCompleteMode.Append
            cmbUserList.DropDownStyle = ComboBoxStyle.DropDownList
            cmbUserList.AutoCompleteSource = AutoCompleteSource.ListItems
            cmbUserList.SelectedIndex = -1
            btnSave.Enabled = False
            dgvSoftwareList.Enabled = False
        Catch ex As Exception

        End Try


    End Sub

    Private Sub dgvSoftwareList_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvSoftwareList.DataError

    End Sub

    Private Sub dgvSoftwareList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSoftwareList.CellContentClick
        Dim l_int_SoftID As Integer = 0
        Dim l_int_loop As Integer = 0


        If dgvSoftwareList.Columns(e.ColumnIndex).Name = "Admin" Then
            If CBool(dgvSoftwareList.Rows(e.RowIndex).Cells("Admin").EditedFormattedValue) = True Then
                dgvSoftwareList.Rows(e.RowIndex).Cells("General").Value = False
            Else
                dgvSoftwareList.Rows(e.RowIndex).Cells("General").Value = True
            End If
        End If

        If dgvSoftwareList.Columns(e.ColumnIndex).Name = "General" Then
            If CBool(dgvSoftwareList.Rows(e.RowIndex).Cells("General").EditedFormattedValue) = True Then
                dgvSoftwareList.Rows(e.RowIndex).Cells("Admin").Value = False
            Else
                dgvSoftwareList.Rows(e.RowIndex).Cells("Admin").Value = True
            End If
        End If



        'Expand and UnExpand Page

        If dgvSoftwareList.Columns(e.ColumnIndex).Name = "General" Or dgvSoftwareList.Columns(e.ColumnIndex).Name = "Admin" Then
            'If dgvSoftwareList.Rows(e.RowIndex).Cells("ColText").Value = " +" And dgvSoftwareList.Rows(e.RowIndex).Cells("General").EditedFormattedValue = True Then

            If dgvSoftwareList.Rows(e.RowIndex).Cells("General").EditedFormattedValue = True Then
                dgvSoftwareList.Rows(e.RowIndex).Cells("ColText").Value = " -"
                l_int_SoftID = dgvSoftwareList.Rows(e.RowIndex).Cells("ColText").Tag
                For l_int_loop = e.RowIndex + 1 To dgvSoftwareList.Rows.Count - 1
                    If l_int_SoftID = dgvSoftwareList.Rows(l_int_loop).Cells("ColText").Tag Then
                        dgvSoftwareList.Rows(l_int_loop).Visible = True
                    Else
                        Exit For
                    End If
                Next

                'ElseIf dgvSoftwareList.Rows(e.RowIndex).Cells("ColText").Value = " -" Then
            ElseIf dgvSoftwareList.Rows(e.RowIndex).Cells("General").EditedFormattedValue = False Then
                dgvSoftwareList.Rows(e.RowIndex).Cells("ColText").Value = " +"
                l_int_SoftID = dgvSoftwareList.Rows(e.RowIndex).Cells("ColText").Tag
                For l_int_loop = e.RowIndex + 1 To dgvSoftwareList.Rows.Count - 1
                    If l_int_SoftID = dgvSoftwareList.Rows(l_int_loop).Cells("ColText").Tag Then
                        dgvSoftwareList.Rows(l_int_loop).Visible = False
                    Else
                        Exit For
                    End If
                Next
            End If
        End If




        'Clear Selection 

        If dgvSoftwareList.Columns(e.ColumnIndex).Name = "ClearSelection" Then
            If dgvSoftwareList.Rows(e.RowIndex).Cells("ColText").Value = " +" Or dgvSoftwareList.Rows(e.RowIndex).Cells("ColText").Value = " -" Then
                dgvSoftwareList.Rows(e.RowIndex).Cells("General").Value = False
                dgvSoftwareList.Rows(e.RowIndex).Cells("Admin").Value = False
            Else
                dgvSoftwareList.Rows(e.RowIndex).Cells("PAdd").Value = False
                dgvSoftwareList.Rows(e.RowIndex).Cells("PUpdate").Value = False
                dgvSoftwareList.Rows(e.RowIndex).Cells("PDelete").Value = False
                dgvSoftwareList.Rows(e.RowIndex).Cells("PPrint").Value = False
            End If

        End If


    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim Dt As New DataTable
        Dim l_int_i As Integer
        Dim dr As DataRow
        Dim Soft_ID As Integer
        Dim l_int_j As Integer
        Dim l_int_PKID As Integer
        With Dt.Columns
            .Add("Fk_SoftwareID")
            .Add("Fk_SoftwareDetailId")
            .Add("Fk_UserId")
            .Add("Type")
            .Add("IsAdd")
            .Add("IsUpdate")
            .Add("IsDelete")
            .Add("IsPrint")
            .Add("CreationDate")
        End With

        Try

            For l_int_i = 0 To dgvSoftwareList.Rows.Count - 1
                If dgvSoftwareList.Rows(l_int_i).Cells("ColName").Tag = 0 Then
                    If dgvSoftwareList.Rows(l_int_i).Cells("General").Value = True Then
                        Soft_ID = dgvSoftwareList.Rows(l_int_i).Cells("ColText").Tag
                        For l_int_j = l_int_i + 1 To dgvSoftwareList.Rows.Count - 1
                            If Soft_ID = dgvSoftwareList.Rows(l_int_j).Cells("ColText").Tag Then
                                With dgvSoftwareList.Rows(l_int_j)
                                    If .Cells("PAdd").EditedFormattedValue = True OrElse .Cells("PUpdate").EditedFormattedValue = True OrElse .Cells("PDelete").EditedFormattedValue = True OrElse .Cells("PPrint").EditedFormattedValue = True Then
                                        dr = Dt.NewRow()
                                        dr("Fk_SoftwareID") = .Cells("ColText").Tag
                                        dr("Fk_SoftwareDetailId") = .Cells("ColName").Tag
                                        dr("Fk_UserId") = cmbUserList.SelectedValue
                                        dr("Type") = 0
                                        dr("IsAdd") = IIf(.Cells("PAdd").EditedFormattedValue = True, 1, 0)
                                        dr("IsUpdate") = IIf(.Cells("PUpdate").EditedFormattedValue = True, 1, 0)
                                        dr("IsDelete") = IIf(.Cells("PDelete").EditedFormattedValue = True, 1, 0)
                                        dr("IsPrint") = IIf(.Cells("PPrint").EditedFormattedValue = True, 1, 0)
                                        dr("CreationDate") = Date.Now
                                        Dt.Rows.Add(dr)
                                    End If
                                End With
                            Else
                                Exit For
                            End If
                        Next
                    ElseIf dgvSoftwareList.Rows(l_int_i).Cells("Admin").Value = True Then
                        dr = Dt.NewRow()
                        With dgvSoftwareList.Rows(l_int_i)
                            dr("Fk_SoftwareID") = .Cells("ColText").Tag
                            dr("Fk_SoftwareDetailId") = .Cells("ColName").Tag
                            dr("Fk_UserId") = cmbUserList.SelectedValue
                            dr("Type") = 1
                            dr("IsAdd") = 1
                            dr("IsUpdate") = 1
                            dr("IsDelete") = 1
                            dr("IsPrint") = 1
                            dr("CreationDate") = Date.Now
                        End With
                        Dt.Rows.Add(dr)
                    End If
                End If
            Next


            linq_obj.SP_Tbl_PermissionMaster_Delete(Convert.ToInt16(cmbUserList.SelectedValue))

            For l_int_i = 0 To Dt.Rows.Count - 1
                With Dt.Rows(l_int_i)
                    l_int_PKID = linq_obj.SP_Tbl_PermissionMaster_Insert(
                        Convert.ToInt16(.Item("Fk_SoftwareID")),
                        Convert.ToInt16(.Item("Fk_SoftwareDetailId")),
                        Convert.ToInt16(.Item("Fk_UserId")),
                        Convert.ToInt16(.Item("Type")),
                        CBool(.Item("IsAdd")),
                        CBool(.Item("IsUpdate")),
                        CBool(.Item("IsDelete")),
                        CBool(.Item("IsPrint")),
                        Convert.ToDateTime(.Item("CreationDate")))
                    linq_obj.SubmitChanges()
                End With
            Next

            MessageBox.Show("Save Successfully")
            bindSoftwareList()
            bindDropdown()

            Class1.global.UserPermissionDataset.Dispose()
            Class1.global.UserPermissionDataset.Clear()
            Class1.global.Glb_User_Permission("SP_Tbl_PermissionMaster_SelectByUser", Class1.global.UserID)

        Catch ex As Exception
            MessageBox.Show("Error In Data")
        End Try

    End Sub



    Private Sub bindSoftwareList()
        Dim Ds As New DataSet
        Dim RowLoop As Integer
        Dim RowCount As Integer
        Dim l_int_SoftwareID As Integer = 0
        Dim objclass As New Class1
        Dim objPAdd As DataGridViewTextBoxCell
        Dim objPUpdate As DataGridViewTextBoxCell
        Dim objPDelete As DataGridViewTextBoxCell
        Dim objPPrint As DataGridViewTextBoxCell
        Dim objGeneral As DataGridViewTextBoxCell
        Dim objAdmin As DataGridViewTextBoxCell


        Try
            Ds = objclass.GetSelectDataset("SP_Get_SoftwareDetails_For_Permission")

            dgvSoftwareList.Rows.Clear()

            If Ds.Tables(0).Rows.Count = 0 Then Exit Sub

            For RowLoop = 0 To Ds.Tables(0).Rows.Count - 1
                With Ds.Tables(0)
                    If l_int_SoftwareID <> .Rows(RowLoop)("PK_SoftwareID") Then
                        dgvSoftwareList.Rows.Add()
                        RowCount = dgvSoftwareList.Rows.Count - 1
                        dgvSoftwareList.Rows(RowCount).Cells("ColText").Value = " +"
                        dgvSoftwareList.Rows(RowCount).Cells("ColName").Value = .Rows(RowLoop)("Name")
                        dgvSoftwareList.Rows(RowCount).Cells("ColText").Tag = .Rows(RowLoop)("Pk_SoftwareID")
                        dgvSoftwareList.Rows(RowCount).Cells("ColName").Tag = 0



                        objPAdd = New DataGridViewTextBoxCell
                        objPUpdate = New DataGridViewTextBoxCell
                        objPDelete = New DataGridViewTextBoxCell
                        objPPrint = New DataGridViewTextBoxCell

                        dgvSoftwareList.Rows(RowCount).Cells("PAdd") = objPAdd
                        dgvSoftwareList.Rows(RowCount).Cells("PUpdate") = objPUpdate
                        dgvSoftwareList.Rows(RowCount).Cells("PDelete") = objPDelete
                        dgvSoftwareList.Rows(RowCount).Cells("PPrint") = objPPrint
                        objPAdd.ReadOnly = True
                        objPUpdate.ReadOnly = True
                        objPDelete.ReadOnly = True
                        objPPrint.ReadOnly = True
                        dgvSoftwareList.Rows(RowCount).DefaultCellStyle.BackColor = Color.Cornsilk



                        dgvSoftwareList.Rows.Add()
                        RowCount = dgvSoftwareList.Rows.Count - 1
                        dgvSoftwareList.Rows(RowCount).Cells("PageName").Value = .Rows(RowLoop)("DetailName")
                        dgvSoftwareList.Rows(RowCount).Cells("ColText").Tag = .Rows(RowLoop)("Pk_SoftwareID")
                        dgvSoftwareList.Rows(RowCount).Cells("ColName").Tag = .Rows(RowLoop)("Pk_SoftwareDetail")



                        objGeneral = New DataGridViewTextBoxCell
                        objAdmin = New DataGridViewTextBoxCell
                        dgvSoftwareList.Rows(RowCount).Cells("Admin") = objAdmin
                        dgvSoftwareList.Rows(RowCount).Cells("General") = objGeneral
                        objAdmin.ReadOnly = True
                        objGeneral.ReadOnly = True
                        dgvSoftwareList.Rows(RowCount).Visible = False
                    Else
                        dgvSoftwareList.Rows.Add()
                        RowCount = dgvSoftwareList.Rows.Count - 1
                        dgvSoftwareList.Rows(RowCount).Cells("PageName").Value = .Rows(RowLoop)("DetailName")
                        dgvSoftwareList.Rows(RowCount).Cells("ColText").Tag = .Rows(RowLoop)("Pk_SoftwareID")
                        dgvSoftwareList.Rows(RowCount).Cells("ColName").Tag = .Rows(RowLoop)("Pk_SoftwareDetail")

                        objGeneral = New DataGridViewTextBoxCell
                        objAdmin = New DataGridViewTextBoxCell
                        dgvSoftwareList.Rows(RowCount).Cells("Admin") = objAdmin
                        dgvSoftwareList.Rows(RowCount).Cells("General") = objGeneral
                        objAdmin.ReadOnly = True
                        objGeneral.ReadOnly = True

                        dgvSoftwareList.Rows(RowCount).Visible = False
                    End If
                    l_int_SoftwareID = .Rows(RowLoop)("Pk_SoftwareID")

                End With

            Next

        Catch ex As Exception

        End Try
    End Sub


    Private Sub cmbUserList_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUserList.SelectionChangeCommitted
        Dim objclass As New Class1
        Dim ds As New DataSet
        Dim l_int_i As Integer
        Dim l_int_j As Integer
        bindSoftwareList()


        If cmbUserList.SelectedIndex >= 0 Then
            btnSave.Enabled = True
            dgvSoftwareList.Enabled = True
            ds = objclass.GetUserPermission("SP_Tbl_PermissionMaster_SelectByUser", Convert.ToInt16(cmbUserList.SelectedValue))
            For l_int_i = 0 To dgvSoftwareList.Rows.Count - 1    'for Check All Grid Rows 
                For l_int_j = 0 To ds.Tables(0).Rows.Count - 1    ' For Check All User Permission Added 
                    If ds.Tables(0).Rows(l_int_j)("Fk_SoftwareDetailID") > 0 Then

                        If dgvSoftwareList.Rows(l_int_i).Cells("colText").Tag = ds.Tables(0).Rows(l_int_j)("Fk_SoftwareID") And
                            dgvSoftwareList.Rows(l_int_i).Cells("colText").Value = " +" Then
                            dgvSoftwareList.Rows(l_int_i).Cells("Admin").Value = False
                            dgvSoftwareList.Rows(l_int_i).Cells("General").Value = True
                        End If
                        If dgvSoftwareList.Rows(l_int_i).Cells("colName").Tag = ds.Tables(0).Rows(l_int_j)("Fk_SoftwareDetailID") Then
                            dgvSoftwareList.Rows(l_int_i).Cells("PAdd").Value = ds.Tables(0).Rows(l_int_j)("IsAdd")
                            dgvSoftwareList.Rows(l_int_i).Cells("PUpdate").Value = ds.Tables(0).Rows(l_int_j)("IsUpdate")
                            dgvSoftwareList.Rows(l_int_i).Cells("PDelete").Value = ds.Tables(0).Rows(l_int_j)("IsDelete")
                            dgvSoftwareList.Rows(l_int_i).Cells("PPrint").Value = ds.Tables(0).Rows(l_int_j)("IsPrint")
                        End If
                        If dgvSoftwareList.Rows(l_int_i).Cells("colText").Tag = ds.Tables(0).Rows(l_int_j)("Fk_SoftwareID") Then
                            dgvSoftwareList.Rows(l_int_i).Visible = True
                        End If
                    Else
                        If dgvSoftwareList.Rows(l_int_i).Cells("colText").Tag = ds.Tables(0).Rows(l_int_j)("Fk_SoftwareID") And
                            dgvSoftwareList.Rows(l_int_i).Cells("colText").Value = " +" Then 'Check User Permission Added and Grid Software ID are Same or not 
                            dgvSoftwareList.Rows(l_int_i).Cells("General").Value = False
                            dgvSoftwareList.Rows(l_int_i).Cells("Admin").Value = True
                        End If
                    End If
                Next
            Next
            dgvSoftwareList.Focus()
        End If
    End Sub
End Class