Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Public Class Rpt_DailyAllotment
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim PK_Address_ID As Integer


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        If (Class1.global.RptUserType = True) Then
            bindUser()
            grpUser.Visible = True
        Else
            grpUser.Visible = False
        End If
    End Sub

    Public Sub bindUser()
        cmbUser.DataSource = Nothing


        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_UserId")
        datatable.Columns.Add("UserName")


        Dim datauser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In datauser
            datatable.Rows.Add(item.Pk_UserId, item.UserName)
        Next
        Dim newRow As DataRow = datatable.NewRow()

        newRow(0) = "0"
        newRow(1) = "All"
        datatable.Rows.InsertAt(newRow, 0)
        cmbUser.DataSource = datatable
        cmbUser.DisplayMember = "UserName"
        cmbUser.ValueMember = "Pk_UserId"
        cmbUser.AutoCompleteMode = AutoCompleteMode.Append
        cmbUser.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim rpt As New ReportDocument
        Dim ss As String
        Try
            ss = Application.LocalUserAppDataPath() 
            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 3000
            If rblUserAllotment.Checked = True Then
                cmd.CommandText = "SP_Rpt_Get_TodayAllotDataByUser"
            Else
                cmd.CommandText = "SP_Rpt_Get_TodayAllotDataByUser_Log"
                cmd.Parameters.AddWithValue("@Pk_AddressID", PK_Address_ID)

            End If 
            cmd.Connection = linq_obj.Connection
            If chkDate.Checked = True Then
                cmd.Parameters.AddWithValue("@Start", dtStartDate.Value.Date)
                cmd.Parameters.AddWithValue("@End", dtEndDate.Value.Date)
                cmd.Parameters.AddWithValue("@byUser", If(Class1.global.RptUserType = True, SplitString(txtAllUsers.Text), Class1.global.User))
            Else
                cmd.Parameters.AddWithValue("@Start", "1/1/2010")
                cmd.Parameters.AddWithValue("@End", dtEndDate.Value.Date)
                cmd.Parameters.AddWithValue("@byUser", If(Class1.global.RptUserType = True, SplitString(txtAllUsers.Text), Class1.global.User))

            End If
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                GvEnqAllotment.DataSource = ds.Tables(0)
            Else
                GvEnqAllotment.DataSource = Nothing
                MessageBox.Show("No Recoard Found..")
            End If 
 
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Function SplitString(ByVal str As String) As String
        If str.Equals("") Or str.Equals("All") Or str.Equals("All,") Then
            If (str.Equals("All,")) Then
                str = str.ToString().Substring(0, str.Length - 1)
            End If
            Return str
        End If
        Dim strfinal As String
        Dim cnt As Integer
        cnt = str.Split(",").Length
        Dim tmpstr As [String]() = New [String](cnt) {}
        tmpstr = str.Split(",")
        ' strfinal = ""
        If cnt > 0 Then
            For index = 0 To tmpstr.Length - 1
                If tmpstr(index) <> "" Then
                    '  Dim tmpstr1 As [String]() = New [String](2) {}
                    'tmpstr1 = tmpstr(index).Split("+")
                    strfinal += tmpstr(index) + ","
                End If
            Next
            strfinal = strfinal.ToString().Substring(0, strfinal.Length - 1)
            ' strfinal += ""
        End If

        Return strfinal
    End Function
    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        txtAllUsers.Text += cmbUser.Text.Trim() + ",".Trim()
        txtEnqNo.Text = ""
        lblPartyName.Text = ""
        PK_Address_ID = 0
    End Sub

    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        Try
            Dim count As Integer
            Dim app As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()

            ' creating new WorkBook within Excel application
            Dim workbook As Microsoft.Office.Interop.Excel._Workbook = app.Workbooks.Add(Type.Missing)
            ' creating new Excelsheet in workbook
            Dim worksheet As Microsoft.Office.Interop.Excel._Worksheet = Nothing
            ' see the excel sheet behind the program
            app.Visible = True
            ' get the reference of first sheet. By default its name is Sheet1.
            ' store its reference to worksheet
            worksheet = workbook.Sheets("Sheet1")
            worksheet = workbook.ActiveSheet
            ' changing the name of active sheet
            worksheet.Name = Me.Name
            ' storing header part in Excel
            For i As Integer = 1 To GvEnqAllotment.Columns.Count
                worksheet.Cells(1, i) = GvEnqAllotment.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvEnqAllotment.Rows.Count - 1
                For j As Integer = 0 To GvEnqAllotment.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvEnqAllotment.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next

        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        If txtEnqNo.Text.Trim() <> "" Then
            Dim data = linq_obj.SP_Get_ServiceODSite_Allotment_EnqNo(txtEnqNo.Text.Trim()).ToList()
            If data.Count > 0 Then
                For Each item As SP_Get_ServiceODSite_Allotment_EnqNoResult In data
                    lblPartyName.Text = item.Name
                    PK_Address_ID = item.Pk_AddressID
                    btnSearch.Focus()
                Next
            Else

                MessageBox.Show("Invalid EnqNo...")

                txtEnqNo.Focus()

            End If
        Else
            txtEnqNo.Text = ""
            lblPartyName.Text = ""
            PK_Address_ID = 0
        End If
    End Sub

    Private Sub rblUserAllotmentLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblUserAllotmentLog.CheckedChanged
        txtEnqNo.Enabled = True

    End Sub

    Private Sub rblUserAllotment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblUserAllotment.CheckedChanged
        txtEnqNo.Text = ""
        lblPartyName.Text = ""
        PK_Address_ID = 0
         
        txtEnqNo.Enabled = False

    End Sub
End Class