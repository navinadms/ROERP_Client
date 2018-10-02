﻿Public Class StateTeamAllotment
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim StateId As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindTeam()
        bindGrid()
    End Sub
    Public Sub bindGrid()
        Dim selectAll = linq_obj.SP_Tbl_StateTeamAllotment_SelectAll().ToList()
        DGDetail.DataSource = selectAll
        DGDetail.Columns(0).Visible = False

    End Sub
    Public Sub bindUser()
        cmbUser.DataSource = Nothing
        Dim dataUser = linq_obj.SP_Get_UserListByTeam(Convert.ToInt32(cmbTeam.SelectedValue)).ToList()
        cmbUser.DataSource = dataUser
        cmbUser.DisplayMember = "UserName"
        cmbUser.ValueMember = "Pk_UserId"
        cmbUser.AutoCompleteMode = AutoCompleteMode.Append
        cmbUser.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Public Sub bindTeam()
        cmbTeam.DataSource = Nothing
        Dim dataTeam = linq_obj.SP_Tbl_TeamMaster_SelectAll().ToList()
        cmbTeam.DataSource = dataTeam
        cmbTeam.DisplayMember = "TeamName"
        cmbTeam.ValueMember = "Pk_TeamId"
        cmbTeam.AutoCompleteMode = AutoCompleteMode.Append
        cmbTeam.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTeam.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub btnAllot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllot.Click
        If (btnAllot.Text = "Update") Then
            If (txtStateName.Text.Trim() <> "") Then
                Dim res As Integer
                res = linq_obj.SP_Tbl_StateTeamAllotment_Update(txtStateName.Text, Convert.ToInt32(cmbTeam.SelectedValue), Convert.ToInt32(cmbUser.SelectedValue), StateId)
                MessageBox.Show("Updated Successfully ")
                txtStateName.Text = ""
                bindGrid()
                Clear()
            Else
                MessageBox.Show("Enter State Name...")
            End If
        Else
            If (txtStateName.Text.Trim() <> "") Then
                Dim res As Integer

                res = linq_obj.SP_Tbl_StateTeamAllotment_Insert(txtStateName.Text, Convert.ToInt64(cmbTeam.SelectedValue), Convert.ToInt32(cmbUser.SelectedValue))
                If (res > 0) Then


                    MessageBox.Show("Successfully Alloted ")
                    txtStateName.Text = ""
                    bindGrid()
                    Clear()
                Else
                    MessageBox.Show("Already Alloted")
                    txtStateName.Text = ""
                    bindGrid()
                End If

            Else
                MessageBox.Show("Enter State Name...")
            End If
        End If




    End Sub

    Private Sub cmbTeam_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTeam.SelectionChangeCommitted
        bindUser()
    End Sub
    Public Sub Clear()
        btnAllot.Text = "Allot"
        txtStateName.Text = ""

    End Sub

    Private Sub DGDetail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGDetail.DoubleClick
        Try
            StateId = Convert.ToInt32(Me.DGDetail.SelectedCells(0).Value)
            Dim data = linq_obj.SP_Tbl_StateTeamAllotment_Select(StateId).ToList()
            If (data.Count > 0) Then
                txtStateName.Text = data(0).StateName
                cmbTeam.SelectedValue = data(0).Fk_TeamId.Value
                bindUser()
                cmbUser.SelectedValue = data(0).Fk_UserId.Value
                btnAllot.Text = "Update"

            Else
                MessageBox.Show("Data Not Found...")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Clear()
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            '*
            ' * display a confirmation message
            '                 
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                Dim cntSelect As Integer = DGDetail.SelectedRows.Count
                For Each dr As DataGridViewRow In DGDetail.SelectedRows
                    Dim resDelete As Integer = linq_obj.SP_Tbl_StateTeamAllotment_Delete(Convert.ToInt32(dr.Cells(0).Value))
                    linq_obj.SubmitChanges()
                Next
                bindGrid()

                MessageBox.Show("Successfully Deleted...")

            End If
        Catch ex As Exception

            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class