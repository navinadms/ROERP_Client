Imports System.Data.SqlClient

Public Class UserAllotment
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim UserId As Integer
    Dim TeamId As Integer
    Dim allotID As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindTeam()
        bindUser()
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
        'cmbSearchUser.DataSource = Nothing

    End Sub
    Public Sub Search_User()
        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_UserId")
        datatable.Columns.Add("UserName")
        Dim dataUserSearch = linq_obj.SP_Get_UserListByTeam(Convert.ToInt32(cmbSearchTeam.SelectedValue)).ToList()
        For Each item As SP_Get_UserListByTeamResult In dataUserSearch
            datatable.Rows.Add(item.Pk_UserId, item.UserName)
        Next
        Dim newRow As DataRow = datatable.NewRow()

        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        cmbSearchUser.DataSource = datatable
        cmbSearchUser.DisplayMember = "UserName"
        cmbSearchUser.ValueMember = "Pk_UserId"
        cmbSearchUser.AutoCompleteMode = AutoCompleteMode.Append
        cmbSearchUser.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSearchUser.AutoCompleteSource = AutoCompleteSource.ListItems
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

        cmbSearchTeam.DataSource = Nothing
        Dim dataTeamSearch = linq_obj.SP_Tbl_TeamMaster_SelectAll().ToList()
        cmbSearchTeam.DataSource = dataTeamSearch
        cmbSearchTeam.DisplayMember = "TeamName"
        cmbSearchTeam.ValueMember = "Pk_TeamId"
        cmbSearchTeam.AutoCompleteMode = AutoCompleteMode.Append
        cmbSearchTeam.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSearchTeam.AutoCompleteSource = AutoCompleteSource.ListItems

    End Sub

    Private Sub btnAllot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllot.Click
        Dim res As Integer
        Try
            If (cmbUser.SelectedIndex >= 0 AndAlso txtEnqNo.Text.Trim() <> "") Then

                Dim result As DialogResult = MessageBox.Show("Are You Sure ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                If result = DialogResult.Yes Then
                    res = linq_obj.SP_Tbl_UserAllotmentDetail_Insert(Address_ID, Convert.ToInt32(cmbUser.SelectedValue), Convert.ToInt32(cmbTeam.SelectedValue))
                    If (res > 0) Then
                        MessageBox.Show("Successfully Alloted To  Team : " + cmbTeam.Text + " and User : " + cmbUser.Text)
                        txtCustName.Text = ""
                        txtEnqNo.Text = ""
                        txtEnqNo.Focus()
                    Else
                        MessageBox.Show("Already Alloted...")

                    End If


                End If
            Else
                MessageBox.Show("Select User For Allotment...")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text.Trim()).ToList()

        If (data.Count > 0) Then
            txtCustName.Text = Convert.ToString(data(0).Name)
            Address_ID = data(0).Pk_AddressID
            'Dim dataUser = linq_obj.SP_Get_AllotedUserForAddress(Address_ID).ToList()
            'If (dataUser.Count > 0) Then
            '    cmbTeam.SelectedValue = dataUser(0).Fk_TeamId
            '    bindUser()
            '    cmbUser.SelectedValue = dataUser(0).Fk_UserId
            'Else
            '    MessageBox.Show("Not Alloted To Any User...")
            'End If
        End If
    End Sub

    Private Sub cmbTeam_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTeam.SelectionChangeCommitted
        bindUser()
        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_RemainingEnqForAllotment"
        cmd.Parameters.Add("@teamid", SqlDbType.Int).Value = Convert.ToInt32(cmbTeam.SelectedValue)
        Dim objclass As New Class1

        Dim dt As New DataTable
        dt.Columns.Add("Pk_AddressID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")
        Dim dtData As DataTable

        dtData = objclass.GetEnqOrderReportData(cmd)
        If dtData.Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtData.Dispose()
            dt.Dispose()
        Else
            txtCustName.AutoCompleteCustomSource.Clear()
            txtEnqNo.AutoCompleteCustomSource.Clear()
            For index = 0 To dtData.Rows.Count - 1
                txtEnqNo.AutoCompleteCustomSource.Add(dtData.Rows(index)(1))
                txtCustName.AutoCompleteCustomSource.Add(dtData.Rows(index)(1))
            Next
            lblCount.Text = Convert.ToInt32(dtData.Rows.Count - 1)
            dtData.Dispose()
            dt.Dispose()
        End If

    End Sub


    Private Sub UserAllotment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                    MessageBox.Show(ex.Message)
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtEnqNo.Text = ""
        txtCustName.Text = ""
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        TeamId = cmbSearchTeam.SelectedValue
        UserId = cmbSearchUser.SelectedValue
        bindGrid()
    End Sub
    Public Sub bindGrid()
        If (TeamId > 0) Then
            Dim userAllotedData = linq_obj.SP_Tbl_UserAllotmentDetail_SelectByTeamAndUser(TeamId, UserId).ToList()
            If (userAllotedData.Count) Then
                GvAllotedData.DataSource = userAllotedData

                GvAllotedData.Columns(0).Visible = False
                GvAllotedData.Columns(1).Visible = False
                GvAllotedData.Columns(2).Visible = False
                GvAllotedData.Columns(3).Visible = False
                GvAllotedData.Columns(4).Visible = False
            Else
                GvAllotedData.DataSource = Nothing

            End If
        Else
            GvAllotedData.DataSource = Nothing
        End If

    End Sub
    Public Sub bindData()
        If (Address_ID > 0) Then
            Dim data = linq_obj.SP_Get_AddressListById(Address_ID).ToList()
            If (data.Count > 0) Then
                txtCustName.Text = Convert.ToString(data(0).Name)
                txtEnqNo.Text = Convert.ToString(data(0).EnqNo)
            End If
        End If

    End Sub
    Private Sub GvAllotedData_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvAllotedData.DoubleClick
        Try
            allotID = Convert.ToInt64(Me.GvAllotedData.SelectedCells(0).Value)
            Address_ID = Convert.ToInt64(Me.GvAllotedData.SelectedCells(1).Value)
            cmbUser.SelectedValue = Convert.ToInt32(Me.GvAllotedData.SelectedCells(2).Value)
            cmbTeam.SelectedValue = Convert.ToInt32(Me.GvAllotedData.SelectedCells(3).Value)
            bindData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            '*
            ' * display a confirmation message
            '                 
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                Dim cntSelect As Integer = GvAllotedData.SelectedRows.Count
                For Each dr As DataGridViewRow In GvAllotedData.SelectedRows
                    Dim resDelete As Integer = linq_obj.SP_Tbl_UserAllotmentDetail_Delete(Convert.ToInt32(dr.Cells(0).Value))
                    linq_obj.SubmitChanges()
                Next
                bindGrid()

                MessageBox.Show("Successfully Deleted...")

            End If
        Catch ex As Exception

            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub cmbSearchTeam_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearchTeam.SelectionChangeCommitted
        Search_User()

    End Sub
End Class