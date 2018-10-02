
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Data
Imports System.Windows.Forms
Imports pdfforge
Imports System.IO
Imports System.IO.File
Imports System.IO.StreamWriter
Imports Microsoft.Office.Interop
Imports System.Security.Principal
Imports System.Management
Imports System.Globalization
Imports System.Threading
Imports System.Configuration
Imports System.Configuration.Configuration
Imports System.Runtime.InteropServices

Public Class Class1
    Shared s As String
    Shared str As String
    Public Shared dbUserName As String
    Public Shared dbPassword As String
    Public Shared g_sCompanyName As String
    Public Shared Function myc() As String
        Dim path As String
        s = System.Windows.Forms.Application.StartupPath
        str = System.Configuration.ConfigurationManager.AppSettings("constr").ToString()
        dbUserName = System.Configuration.ConfigurationManager.AppSettings("UserName").ToString()
        dbPassword = System.Configuration.ConfigurationManager.AppSettings("PassWord").ToString()

        path = Application.StartupPath & "\XmlFile\"
        If Directory.Exists(path) = False Then
            Directory.CreateDirectory(path)
        End If

        path = Application.StartupPath & "\Script\"
        If Directory.Exists(path) = False Then
            Directory.CreateDirectory(path)
        End If

        path = Application.StartupPath & "\BackUp\"
        If Directory.Exists(path) = False Then
            Directory.CreateDirectory(path)
        End If

        Return s

    End Function

    Public Shared Function getDate(ByVal str As String) As String
        Dim strIn As String() = New String(3) {}
        Dim strOut As String
        If (str.Contains("/")) Then
            strIn = str.Split("/")
        ElseIf (str.Contains("-")) Then
            strIn = str.Split("-")
        End If

        strOut = strIn(1) + "/" + strIn(0) + "/" + strIn(2)
        Return strOut

    End Function
    Shared p1 As String = myc()

    Public Shared IDAddress As Integer
    Public Shared Flag As Integer
    Public Shared con As New SqlConnection(str)

    Public Class [global]
        Public Shared QuatationId As Long
        Public Shared UserID As Integer
        Public Shared TeamID As Integer
        Public Shared User As String
        Public Shared GobalMaxId As Integer
        Public Shared Designation As String
        Public Shared UserName As String
        Public Shared Signature As String
        Public Shared QPath As String
        Public Shared LanguageId As Integer
        Public Shared GobalPathForSaveDoc As Integer
        Public Shared UserPermissionDataset As New DataSet
        Public Shared UserPermissionDatasettmp As New DataSet
        Public Shared ParentMenu As String
        Public Shared InquiryView As Integer
        Public Shared RptUserType As Boolean
        Public Shared UserAllotType As String
        Public Shared loginuser As String
        Public Shared imagepath As String
        Public Shared imageName As String
        Dim ef As New rptEnqFormat
        Public Shared Sub Glb_User_Permissiontmp(ByVal SpName As String, ByVal a_int_ID As Integer)

            Dim cmd As New SqlCommand
            con.Close()
            cmd.Connection = con

            Dim ds As New DataSet

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = SpName
            cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = a_int_ID
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd
            ds.Tables.Clear()
            ds.Dispose()
            da.Fill(UserPermissionDatasettmp)

        End Sub
        Public Shared Sub Glb_User_Permission(ByVal SpName As String, ByVal a_int_ID As Integer)

            Dim cmd As New SqlCommand
            con.Close()
            cmd.Connection = con

            Dim ds As New DataSet

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = SpName
            cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = a_int_ID
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd
            ds.Tables.Clear()
            ds.Dispose()
            da.Fill(UserPermissionDataset)

        End Sub
    End Class
    Public Function GetSearchData(ByVal cmd As SqlCommand) As DataSet
        cmd.Connection = con
        Dim ds As New EnqReport
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        da.Fill(ds)
        Return ds
    End Function
    Public Function GetOrderCountData(ByVal cmd As SqlCommand) As DataSet
        cmd.Connection = con
        Dim ds As New DataSet
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        da.Fill(ds)
        Return ds
    End Function
    Public Function GetStockData(ByVal cmd As SqlCommand) As DataSet
        cmd.Connection = con
        Dim ds As New EnqReport
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        da.Fill(ds)
        Return ds
    End Function
    Public Function GetEnqReportData(ByVal cmd As SqlCommand) As DataSet
        cmd.Connection = con
        Dim ds As New EnqReport
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        da.Fill(ds)
        Return ds
    End Function

    Public Function GetEnqOrderReportData(ByVal cmd As SqlCommand) As DataTable
        cmd.Connection = con
        Dim dt As New DataTable
        Dim ds As New EnqFormat.OrderGridDataTable
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        da.Fill(ds)
        dt = ds
        Return dt
    End Function
    Public Function getTotalInward(ByVal cmd As SqlCommand) As Integer
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        Dim res As Integer
        Try
            con.Open()
            res = cmd.ExecuteScalar()
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        Return res
    End Function

    Public Function GetPartyOutStanding(ByVal cmd As SqlCommand) As DataSet

        cmd.Connection = con
        Dim ds As New PartyDataset
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        da.Fill(ds.Tables("PartyMaster"))

        Return ds
    End Function


    ''' <summary>
    ''' ADDED BY RAJESH
    ''' FOR GETTING A REFNO. INDEX OF THE FILE NAME.
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getTotalNo(ByVal str As String) As String

        Dim strdata As String() = str.Split("/"c)
        Dim mainstr As String() = Convert.ToString(strdata(2)).Split("-"c)
        Return mainstr(mainstr.Length - 1)
    End Function

    Public Shared Function killProcessOnUser()
        'Dim processlist As Process() = Process.GetProcessesByName("WINWORD")
        'For Each theprocess As Process In processlist
        '    Dim ProcessUserSID As String = GetProcessInfoByPID(theprocess.Id)
        '    Dim strfilename As String
        '    strfilename = theprocess.MainModule.FileName

        '    ' MessageBox.Show(WindowsIdentity.GetCurrent().Name)

        '    Dim CurrentUser As String = WindowsIdentity.GetCurrent().Name
        '    If (CurrentUser = "") Then
        '        CurrentUser = WindowsIdentity.GetCurrent().User.Value
        '    End If
        '    ' MessageBox.Show(theprocess.ProcessName)


        '    If theprocess.ProcessName = "WINWORD" Then
        '        Try
        '            If theprocess.HasExited Then
        '            Else
        '                theprocess.Kill()

        '            End If
        '        Catch ex As Exception

        '        End Try

        '    End If
        'Next
    End Function

    Public Shared Function GetProcessInfoByPID(ByVal PID As Integer)
        Dim User As String = [String].Empty
        Dim Domain As String = [String].Empty
        Dim OwnerSID As String = [String].Empty
        Dim processname As String = [String].Empty
        Try
            'Dim sq As New ObjectQuery("Select * from Win32_Process  Where Name LIKE 'WINWORD%' and ProcessID = " + Convert.ToString(PID))
            Dim sq As New ObjectQuery("Select * from Win64_Process  Where Name LIKE 'WINWORD%' and ProcessID = " + Convert.ToString(PID))
            'Dim sq As New ObjectQuery("Select * from Win32_Process")

            Dim searcher As New ManagementObjectSearcher(sq)
            If searcher.[Get]().Count = 0 Then
                Return OwnerSID
            End If
            For Each oReturn As ManagementObject In searcher.[Get]()
                Dim o As String() = New [String](1) {}
                'Invoke the method and populate the o var with the user name and domain
                oReturn.InvokeMethod("GetOwner", DirectCast(o, Object()))

                'int pid = (int)oReturn["ProcessID"];
                processname = DirectCast(oReturn("Name"), String)
                'dr[2] = oReturn["Description"];
                User = o(0)
                If User Is Nothing Then
                    User = [String].Empty
                End If
                Domain = o(1)
                If Domain Is Nothing Then
                    Domain = [String].Empty
                End If
                Dim sid As String() = New [String](0) {}
                oReturn.InvokeMethod("GetOwner", DirectCast(sid, Object()))
                If Domain <> "" Then
                    OwnerSID = Domain + "\" + User
                Else
                    OwnerSID = Domain
                End If
                Return OwnerSID
            Next
        Catch
            Return OwnerSID
        End Try
        Return OwnerSID
    End Function

    Public Shared Function GetProcessInfoByPIDByMain(ByVal PID As Integer)
        Dim User As String = [String].Empty
        Dim Domain As String = [String].Empty
        Dim OwnerSID As String = [String].Empty
        Dim processname As String = [String].Empty
        Try
            'Dim sq As New ObjectQuery("Select * from Win32_Process  Where Name LIKE 'PDFGENERATOR%' and ProcessID = " + Convert.ToString(PID))
            Dim sq As New ObjectQuery("Select * from Win64_Process  Where Name LIKE 'WINWORD%' and ProcessID = " + Convert.ToString(PID)) ' change 30-12-2014

            'Dim sq As New ObjectQuery("Select * from Win32_Process")

            Dim searcher As New ManagementObjectSearcher(sq)
            If searcher.[Get]().Count = 0 Then
                Return OwnerSID
            End If
            For Each oReturn As ManagementObject In searcher.[Get]()
                Dim o As String() = New [String](1) {}
                'Invoke the method and populate the o var with the user name and domain
                oReturn.InvokeMethod("GetOwner", DirectCast(o, Object()))

                'int pid = (int)oReturn["ProcessID"];
                processname = DirectCast(oReturn("Name"), String)
                'dr[2] = oReturn["Description"];
                User = o(0)
                If User Is Nothing Then
                    User = [String].Empty
                End If
                Domain = o(1)
                If Domain Is Nothing Then
                    Domain = [String].Empty
                End If
                Dim sid As String() = New [String](0) {}
                oReturn.InvokeMethod("GetOwner", DirectCast(sid, Object()))
                If Domain <> "" Then
                    OwnerSID = Domain + "\" + User
                Else
                    OwnerSID = Domain
                End If
                Return OwnerSID
            Next
        Catch
            Return OwnerSID
        End Try
        Return OwnerSID
    End Function

    Public Function GetSelectDataset(ByVal SpName As String) As DataSet
        Dim cmd As New SqlCommand
        cmd.Connection = con
        Dim ds As New DataSet
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = SpName
        '  cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = a_int_ID
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        da.Fill(ds)
        Return ds
    End Function
    'viraj
    Public Function GetUserPermission(ByVal SpName As String, ByVal a_int_ID As Integer) As DataSet

        Dim cmd As New SqlCommand
        cmd.Connection = con
        Dim ds As New DataSet
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = SpName
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = a_int_ID
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        da.Fill(ds)
        Return ds

    End Function
    Public Shared Function IsDateFormate(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean

        If e.KeyChar <> ChrW(Keys.Back) And e.KeyChar <> ChrW(47) And e.KeyChar <> "-" Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If

        If (sender.Text.Length = 2 Or sender.Text.Length = 5) And e.KeyChar <> ChrW(Keys.Back) Then

            If e.KeyChar = "-" And sender.Text.Length <> 5 Then
                e.KeyChar = "-"
            ElseIf sender.Text.Length <> 5 Then
                e.KeyChar = "/"
            ElseIf e.KeyChar = "-" And sender.Text.Length = 5 And InStr(sender.Text, "-", CompareMethod.Text) > 0 Then
                e.KeyChar = "-"
            ElseIf sender.Text.Length = 5 Then
                e.KeyChar = "/"
            End If
        End If

        If sender.Text.Length > 9 And e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If

        Return e.Handled
    End Function
    Public Shared Function ChkVaildDate(ByVal strDate As String) As Boolean
        ChkVaildDate = True
        Dim str As String()
        Dim i As Integer

        Dim delimiters() = New Char() {"-", "/"}
        str = strDate.Split(delimiters)

        If IsDate(strDate) = False And strDate <> "" Then
            ChkVaildDate = False
        End If
        If str.Length > 3 And strDate <> "" Then
            ChkVaildDate = False
        End If
    End Function
    Public Shared Function Datecheck(ByVal date1 As Object) As String
        Dim finalDt = ""
        If (Convert.ToString(date1) <> "" Or date1 Is Null) Then
            If (date1.ToString().Contains("1900")) Then
                finalDt = ""
            Else
                finalDt = Convert.ToDateTime(date1).ToShortDateString()
            End If
        End If
        Return finalDt
    End Function
    Public Shared Function SetDate(ByVal strDate As String) As String
        'If(TblRawMaterial.Rows(i)("DisDate").ToString = "", "01-01-1900", TblRawMaterial.Rows(i)("DisDate").ToString)
        If strDate.Trim.ToString = "" Then
            SetDate = "01-01-1900"
        Else
            SetDate = Convert.ToDateTime(strDate)
        End If
    End Function
    Public Shared Function ConvertNumeric(ByVal strValue As String) As Integer
        'If(TblRawMaterial.Rows(i)("DisDate").ToString = "", "01-01-1900", TblRawMaterial.Rows(i)("DisDate").ToString)
        If strValue.Trim.ToString = "" Then
            strValue = 0
        Else
            strValue = Convert.ToInt16(strValue)
        End If
    End Function

    Public Shared Function OnlyNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        If Char.IsDigit(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        Return e.Handled
    End Function

    Public Shared Function IsValidEmail(ByVal strIn As String) As Boolean
        Try
            If strIn <> "" Then
                Dim b As New System.Net.Mail.MailAddress(strIn)
            End If
        Catch
            Return False
        End Try
        Return True
    End Function
    Public Shared Sub WriteXMlFile(ByVal DataSet As DataSet, ByVal sFileName As String, ByVal sTableName As String)
        Dim ds As New DataSet
        ds = DataSet
        ds.Tables(sTableName).WriteXmlSchema(Application.StartupPath & "\XmlFile\" & sFileName & ".xml")
    End Sub 
End Class
