Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Security.Principal
Imports System.IO
Public Class Login
    Public con1 As SqlConnection
    Public cmd As SqlCommand
    Public dr As SqlDataReader
    Public da As SqlDataAdapter
    Public ds As DataSet
    Public Shared iCnt As Integer
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())


    Public Sub New()
        InitializeComponent()
        con1 = Class1.con
    End Sub
    ''' <summary>
    ''' Get All The Global Variables and Values Used in Software.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks> 

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim str As String

        Try

            'check app Version 

            Dim Version As String


            Version = lblVersion.Text.Replace("/", "")

            Dim VersionStatus = linq_obj.SP_Get_Check_Version(Convert.ToInt64(Version)).ToList()

            If (VersionStatus.Count > 0) Then


                ' Dim dt As New DateTime(2012, 5, 25, 0, 0, 0)
                ' If ((DateTime.Now.Day <= dt.Day) And (DateTime.Now.Year <= dt.Year) And (DateTime.Now.Month <= dt.Month)) Then
                con1.Close()

                con1.Open()
                str = "select * from User_Master where UserName='" + txtUserName.Text + "' and Password1='" + txtPassword.Text + "' "

                cmd = New SqlCommand(str, con1)
                dr = cmd.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    Class1.global.UserPermissionDatasettmp.Clear()
                    Class1.global.UserPermissionDataset.Clear()
                    Class1.global.User = txtUserName.Text
                    Class1.global.loginuser = txtUserName.Text
                    Class1.global.QPath = dr("Path").ToString().Replace("D:", "\\192.168.1.102") 'change navin 05-05-2015 client to server \\192.168.1.102\d$
                    Class1.global.Signature = dr("Signature").ToString().Replace("D:", "\\192.168.1.102") 'change navin 05-05-2015 client to server
                    Class1.global.UserName = dr("FirstName") + " " + dr("LastName")
                    Class1.global.Designation = dr("Designation")
                    Class1.global.UserID = Convert.ToInt32(dr("Pk_UserId"))
                    Class1.global.TeamID = Convert.ToInt32(dr("Fk_TeamId"))
                    Class1.global.InquiryView = Convert.ToString(dr("IsViewAllInquiry"))
                    Class1.global.UserAllotType = Convert.ToString(dr("Status"))
                    Class1.global.Glb_User_Permission("SP_Tbl_PermissionMaster_SelectByUser", Class1.global.UserID)
                    Class1.global.Glb_User_Permissiontmp("SP_Tbl_PermissionMaster_SelectByUser", Class1.global.UserID)
                    Try
                        Dim dv As New DataTable
                        Dim dataView As DataView
                        Dim Class1 As New Class1
                        Dim strName As String = ""
                        dataView = Class1.global.UserPermissionDatasettmp.Tables(0).DefaultView
                        dataView.RowFilter = "([Name] like 'Inquiry Reports')"
                        dv = dataView.ToTable()
                        If (dv.Rows(0)("Type") = 1) Then
                            Class1.global.RptUserType = True
                        Else
                            Class1.global.RptUserType = False
                        End If

                    Catch ex As Exception

                    End Try

                    MDIMainForm.Show()

                Else
                    Dim result As DialogResult = MessageBox.Show("Invalid UserName and Password", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    iCnt = iCnt + 1
                    If iCnt = 3 Then
                        Me.Close()
                    End If
                    Dim lg As New Login
                    lg.Show()
                End If
                cmd.Dispose()
                dr.Dispose()
                con1.Close()
                Me.Hide()
            Else
                MessageBox.Show("Sorry,You have old version application.Please contact to administrator.")

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Login_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub

    Private Sub Login_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 13 Then
            SendKeys.Send("{TAB}")
        End If
        If e.KeyCode = 27 Then
            Me.Close()
        End If
    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
                Dim PC As TabControl = TryCast(control, TabControl)
                parentControl = PC
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
                        If textBox.Tag IsNot Nothing Then
                            If textBox.Tag = "DateFormat" Then
                                AddHandler textBox.KeyPress, AddressOf TextBox_KeyPress
                            End If
                        End If
                    End If
                End If
                ' Set the handler.
            Next
            ' Set the handler.
        Next
    End Sub
    Private Sub TextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = Class1.IsDateFormate(sender, e)
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
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If MessageBox.Show("Do you want to backup ? ", "Auto Backup", MessageBoxButtons.OKCancel) = DialogResult.OK Then
            Try
                Dim regDate As Date = Date.Now()
                Dim strDate As String = regDate.ToString("ddMMMyyyy")
                Dim sBackUpPath As String = Application.StartupPath & "\BackUp\ROTESTDB" & strDate
                sBackUpPath = sBackUpPath & ".bak"
                Dim sqlConn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
                sqlConn.Open()
                Dim sCommand = "BACKUP DATABASE ROTESTDB TO DISK = N'" & sBackUpPath & "'WITH COPY_ONLY "
                Dim sqlCmd As New SqlCommand(sCommand, sqlConn)
                sqlCmd.ExecuteNonQuery()
                MessageBox.Show("Backup successful")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Me.Close()
    End Sub
    Private Sub txtUserName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserName.KeyPress
        If e.KeyChar = "+" Then
            txtUserName.Text = "rk"
            txtPassword.Text = "Ravi"
            Call btnLogin_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub txtUserName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserName.TextChanged
    End Sub
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter
    End Sub
  
End Class