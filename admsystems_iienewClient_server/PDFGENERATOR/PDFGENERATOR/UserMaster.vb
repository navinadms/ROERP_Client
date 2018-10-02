﻿Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.IO

Public Class UserMaster
    Private con1 As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader
    Private da As SqlDataAdapter
    Private ds As DataSet

    Shared Path11 As String
    Public UserID As Int32
    Shared signPath1 As String

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim statusViewEnq As Int32
    Dim statusType As String
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub
    Public Sub New()
        InitializeComponent()
        con1 = Class1.con
        GvUser_Bind()
        AutoDesignation()
        bindTeam()
    End Sub
    Public Sub bindTeam()
        cmbTeam.DataSource = Nothing
        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_TeamId")
        datatable.Columns.Add("TeamName")
        Dim dataTeam = linq_obj.SP_Tbl_TeamMaster_SelectAll().ToList()
        For Each item As SP_Tbl_TeamMaster_SelectAllResult In dataTeam
            datatable.Rows.Add(item.Pk_TeamId, item.TeamName)
        Next
        Dim newRow As DataRow = datatable.NewRow()

        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        cmbTeam.DataSource = datatable
        cmbTeam.DisplayMember = "TeamName"
        cmbTeam.ValueMember = "Pk_TeamId"

        cmbTeam.AutoCompleteMode = AutoCompleteMode.Append
        cmbTeam.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTeam.AutoCompleteSource = AutoCompleteSource.ListItems
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
            dataView.RowFilter = "([DetailName] like 'User')"
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
                            btnUpdate.Enabled = True
                        Else
                            btnUpdate.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsDelete") = True) Then
                            btnDelete.Enabled = True
                        Else
                            btnDelete.Enabled = False
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub AutoDesignation()
        Dim desg = linq_obj.SP_Tbl_DesignationMaster_SelectAll().ToList()
        For Each item As SP_Tbl_DesignationMaster_SelectAllResult In desg
            txtDesignation.AutoCompleteCustomSource.Add(item.DesignationTitle)
        Next
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()

        signPath1 = imgSrc
        'signPath1 = openFileDialog1.FileName
        txtPath.Text = signPath1
        Path11 = signPath1
        PictureSignature.ImageLocation = imgSrc
        PictureSignature.Text = imgSrc

    End Sub

    Public Sub GvUser_Bind()
        Dim user12 As String
        user12 = "select Pk_UserID as ID,FirstName,LastName from User_Master"
        da = New SqlDataAdapter(user12, con1)
        ds = New DataSet()
        da.Fill(ds)
        GvUser.DataSource = ds.Tables(0)
        da.Dispose()
        ds.Dispose()

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim user_insert As String
        Try
            If (RBHead.Checked = True) Then
                statusType = "Head"
            Else
                statusType = "Sub"
            End If
            If (RBAll.Checked = True) Then
                statusViewEnq = 1
            Else
                statusViewEnq = 0
            End If

            validation_Text()
            con1.Close()

            If signPath1 <> "" Then
                Dim total As Integer

                total = linq_obj.SP_Get_Count_UserTeam(Convert.ToInt32(cmbTeam.SelectedValue))

                If (total = 1 And statusType = "Sub" Or total = 0 Or txtDesignation.Text.Trim() = "Admin Div." Or cmbTeam.SelectedValue = 0) Then
                    con1.Open()
                    '

                    user_insert = "insert into User_Master (FirstName,LastName,UserName,Password1,Designation,Signature,Path,CreateDate,Status,Fk_TeamId,UserTeamName,IsViewAllInquiry) values('" + txtfname.Text + "','" + txtlname.Text + "','" + txtUserName.Text + "','" + txtPassword.Text + "','" + txtDesignation.Text + "','" + signPath1 + "','" + txtPath.Text + "','" + DateTime.Now.Date + "','" + statusType + "'," + cmbTeam.SelectedValue + ",'" + txtTeamIdentity.Text + "'," & statusViewEnq & ")"

                    cmd = New SqlCommand(user_insert, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    con1.Close()

                    MessageBox.Show("Add User Successfully..")
                    SetClean()
                    Path11 = ""
                    PictureSignature.Image = Nothing
                Else
                    MessageBox.Show("One Team Has Only Has One Head")
                End If
                If con1.State = ConnectionState.Open Then
                    con1.Close()
                End If
            Else
                MessageBox.Show("Please Select Signature..")
                If con1.State = ConnectionState.Open Then
                    con1.Close()
                End If
            End If
            GvUser_Bind()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            If con1.State = ConnectionState.Open Then
                con1.Close()
            End If
        End Try
    End Sub
    Public Sub SetClean()
        txtfname.Text = ""
        txtlname.Text = ""
        txtUserName.Text = ""
        txtPassword.Text = ""
        txtConformPwd.Text = ""
        txtDesignation.Text = ""
        txtPath.Text = ""
        btnSave.Visible = True
        btnUpdate.Visible = False
        txtTeamIdentity.Text = ""
        UserID = 0
        PictureSignature.ImageLocation = ""
        RBSub.Checked = True
        RBAllotted.Checked = True
    End Sub

    Private Sub txtUserName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserName.Leave
        Try
            con1.Open()
            Dim str As String = "select * from User_Master where UserName='" + txtUserName.Text + "'"
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            If (dr.HasRows) Then
                MessageBox.Show("This UserName Already Used..")

            End If
            con1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtConformPwd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConformPwd.Leave
        If (txtPassword.Text.Trim() <> txtConformPwd.Text.Trim()) Then
            MessageBox.Show("Confirm Password Not Match...")
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim user12 As String
        user12 = "select Pk_UserID as ID,FirstName,LastName from User_Master where FirstName='" + txtFirstNameSearch.Text.Trim() + "' or UserName='" + txtUserNameSeach.Text.Trim() + "' "
        da = New SqlDataAdapter(user12, con1)
        ds = New DataSet()
        da.Fill(ds)
        GvUser.DataSource = ds.Tables(0)
        da.Dispose()
        ds.Dispose()
    End Sub

    Private Sub GvUser_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvUser.DoubleClick
        bindData()
    End Sub
    Public Sub bindData()
        UserID = DirectCast(GvUser(0, Convert.ToInt32(GvUser.CurrentRow.Index)).Value, Int32)
        Try
            con1.Open()
            Dim userdata As String
            userdata = "select * from User_Master where Pk_UserId=" & Convert.ToInt32(UserID) & ""
            cmd = New SqlCommand(userdata, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            txtfname.Text = dr("FirstName").ToString()
            txtlname.Text = dr("LastName").ToString()
            txtUserName.Text = dr("UserName").ToString()
            txtPassword.Text = dr("Password1").ToString()
            txtConformPwd.Text = dr("Password1").ToString()
            txtDesignation.Text = dr("Designation").ToString()
            txtPath.Text = dr("Path").ToString()
            Path11 = dr("Signature").ToString()
            txtTeamIdentity.Text = Convert.ToString(dr("UserTeamName"))
            PictureSignature.ImageLocation = Path11
            If (dr("Status").ToString() = "Head") Then
                RBHead.Checked = True
            Else
                RBSub.Checked = True
            End If
            If (dr("IsViewAllInquiry").ToString() = "1") Then
                RBAll.Checked = True
            Else
                RBAllotted.Checked = True
            End If
            cmbTeam.SelectedValue = Convert.ToString(dr("Fk_TeamId"))
            con1.Close()

            btnSave.Visible = False
            btnUpdate.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub GvUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub validation_Text()

        txtfname.BackColor = System.Drawing.Color.White
        txtfname.ForeColor = System.Drawing.Color.Black

        txtlname.BackColor = System.Drawing.Color.White
        txtlname.ForeColor = System.Drawing.Color.Black

        txtUserName.BackColor = System.Drawing.Color.White
        txtUserName.ForeColor = System.Drawing.Color.Black

        txtPassword.BackColor = System.Drawing.Color.White
        txtPassword.ForeColor = System.Drawing.Color.Black


        txtConformPwd.BackColor = System.Drawing.Color.White
        txtConformPwd.ForeColor = System.Drawing.Color.Black

        txtDesignation.BackColor = System.Drawing.Color.White
        txtDesignation.ForeColor = System.Drawing.Color.Black

        txtPath.BackColor = System.Drawing.Color.White
        txtPath.ForeColor = System.Drawing.Color.Black



        If (txtfname.Text.Trim() = "") Then
            txtfname.BackColor = System.Drawing.Color.Red

            Dim exc As New Exception("First Name Can Not Be Blank")
            txtfname.ForeColor = System.Drawing.Color.White
            txtfname.Focus()
            Throw exc

        End If
        If (txtlname.Text.Trim() = "") Then
            txtlname.BackColor = System.Drawing.Color.Red
            Dim exc As New Exception("Last Name CanNot Be Blank")
            txtlname.ForeColor = System.Drawing.Color.White
            txtlname.Focus()
            Throw exc

        End If
        If (txtUserName.Text.Trim() = "") Then
            txtUserName.BackColor = System.Drawing.Color.Red
            Dim exc As New Exception("USer Name CanNot Be Blank")
            txtUserName.ForeColor = System.Drawing.Color.White
            txtUserName.Focus()
            Throw exc

        End If
        If (txtPassword.Text.Trim() = "") Then
            txtPassword.BackColor = System.Drawing.Color.Red
            Dim exc As New Exception("Password CanNot Be Blank")
            txtPassword.ForeColor = System.Drawing.Color.White
            txtPassword.Focus()
            Throw exc

        End If
        If (txtConformPwd.Text.Trim() = "") Then
            txtConformPwd.BackColor = System.Drawing.Color.Red
            Dim exc As New Exception("Conform Password CanNot Be Blank")
            txtConformPwd.ForeColor = System.Drawing.Color.White
            txtConformPwd.Focus()
            Throw exc

        End If

        If (txtDesignation.Text.Trim() = "") Then
            txtDesignation.BackColor = System.Drawing.Color.Red
            Dim exc As New Exception("Designation CanNot Be Blank")
            txtDesignation.ForeColor = System.Drawing.Color.White
            txtDesignation.Focus()
            Throw exc

        End If

        If (txtPath.Text.Trim() = "") Then
            txtPath.BackColor = System.Drawing.Color.Red
            Dim exc As New Exception("Folder Path CanNot Be Blank")
            txtPath.ForeColor = System.Drawing.Color.White
            txtPath.Focus()
            Throw exc

        End If

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Dim user_update As String
        Try
            validation_Text()
            con1.Close()
            con1.Open()




            If Path11.ToString() <> "" Then


                If (RBHead.Checked = True) Then
                    statusType = "Head"
                Else
                    statusType = "Sub"
                End If
                If (RBAll.Checked = True) Then
                    statusViewEnq = 1
                Else
                    statusViewEnq = 0
                End If

                Dim total As Integer

                total = linq_obj.SP_Get_Count_UserTeam(Convert.ToInt32(cmbTeam.SelectedValue))

                If (total = 1 Or total = 0 Or txtDesignation.Text.Trim() = "Admin Div." Or cmbTeam.SelectedValue = 0) Then

                    user_update = "Update User_Master Set FirstName='" + txtfname.Text + "',LastName='" + txtlname.Text + "',UserName='" + txtUserName.Text + "',Password1='" + txtPassword.Text + "',Designation='" + txtDesignation.Text + " ',Signature='" + Path11 + "',Path='" + txtPath.Text + "',Status='" + statusType + "',Fk_TeamId=" + cmbTeam.SelectedValue + ",UserTeamName='" + txtTeamIdentity.Text + "', IsViewAllInquiry = " & statusViewEnq & " where Pk_UserId=" & UserID & " "
                    cmd = New SqlCommand(user_update, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    MessageBox.Show("Update Record Successfully..")
                    GvUser_Bind()
                    SetClean()
                    Path11 = ""
                    PictureSignature.Image = Nothing
                    con1.Close()
                Else
                    MessageBox.Show("One Team Has Only Has One Head")
                End If
                If con1.State = ConnectionState.Open Then
                    con1.Close()
                End If
            Else
                Dim total As Integer

                total = linq_obj.SP_Get_Count_UserTeam(cmbTeam.SelectedValue)

                If (RBHead.Checked = True) Then
                    statusType = "Head"
                Else
                    statusType = "Sub"
                End If
                If (RBAll.Checked = True) Then
                    statusViewEnq = 1
                Else
                    statusViewEnq = 0
                End If

                If (total = 0) Then
                    user_update = "Update User_Master Set FirstName='" + txtfname.Text + "',LastName='" + txtlname.Text + "',UserName='" + txtUserName.Text + "',Password1='" + txtPassword.Text + "',Designation='" + txtDesignation.Text + " ',Signature='" + Path11 + "',Path='" + txtPath.Text + "',Status='" + statusType + "',Fk_TeamId=" + cmbTeam.SelectedValue + ",UserTeamName='" + txtTeamIdentity.Text + "', IsViewAllInquiry = " & statusViewEnq & " where Pk_UserId=" & UserID & " "
                    cmd = New SqlCommand(user_update, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    MessageBox.Show("Update Record Successfully..")
                    GvUser_Bind()
                    SetClean()
                    Path11 = ""
                    PictureSignature.Image = Nothing
                    con1.Close()
                Else
                    MessageBox.Show("One Team Has Only Has One Head")
                End If
                If con1.State = ConnectionState.Open Then
                    con1.Close()
                End If


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            If con1.State = ConnectionState.Open Then
                con1.Close()
            End If
        End Try

    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        SetClean()
        UserID = 0

    End Sub

    Private Sub GvUser_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles GvUser.PreviewKeyDown
        bindData()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If (UserID > 0) Then
                Dim result As DialogResult = MessageBox.Show("Are You Sure Delete User?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                If result = DialogResult.Yes Then
                    If con1.State = ConnectionState.Open Then
                        con1.Close()
                    End If
                    con1.Open()
                    Dim str As String = "delete from User_Master where Pk_UserId=" & UserID & ""
                    cmd = New SqlCommand(str, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    MessageBox.Show("Deleted Record Successfully..")
                    If con1.State = ConnectionState.Open Then
                        con1.Close()
                    End If
                    SetClean()
                    Path11 = ""
                    PictureSignature.Image = Nothing

                    con1.Close()
                    GvUser_Bind()
                End If
            Else
                MessageBox.Show("No Data Found For Delete...")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UserMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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


    Private Sub DataRow(ByVal p1 As Object)
        Throw New NotImplementedException
    End Sub

End Class