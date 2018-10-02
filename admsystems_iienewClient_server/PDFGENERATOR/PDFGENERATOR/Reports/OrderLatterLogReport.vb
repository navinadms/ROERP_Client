
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine.ReportDocument
Public Class OrderLatterLogReport
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim dataAddress As DataTable
    Dim count As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()

    End Sub
    Public Sub AutoCompated_Text()

        '' State Auto Complated 
        txtUserName.AutoCompleteCustomSource.Clear()
        Dim data1 = linq_obj.SP_Get_UserList().ToList()
        For Each iteam As SP_Get_UserListResult In data1
            txtUserName.AutoCompleteCustomSource.Add(iteam.UserName)

        Next
    End Sub


    Private Sub btnUserAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserAdd.Click
     


        Dim str As String = "'" + String.Join("','", txtUserName.Text.ToString()) + "'"
        txtUserMul.Text += str + ","
        txtUserName.Text = ""
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            Dim criteria As String
            criteria = ""

            If txtUserMul.Text.Trim() <> "" Then
                criteria = criteria + "LatterLog.MailBy in (" + txtUserMul.Text.Trim().TrimEnd(",") + ") and "
            End If

            ''Latter Log  Between Date 
            If (chkIsCardDate.Checked = True) Then
                criteria = criteria + "LatterLog.Card_Date  >= convert(varchar(10), '" + Class1.getDate(txtstartDt.Text) + "',112) and LatterLog.Card_Date <=  convert(varchar(10), '" + Class1.getDate(txtEndDate.Text) + "',112) and "
            End If
            If (chkOther.Checked = True) Then
                criteria = criteria + "LatterLog.LDate  >= convert(varchar(10), '" + Class1.getDate(txtstartDt.Text) + "',112) and LatterLog.LDate <=  convert(varchar(10), '" + Class1.getDate(txtEndDate.Text) + "',112) and "
            End If
            If (criteria.Length > 0) Then
                criteria = criteria.ToString().Trim().Substring(0, criteria.Trim().Length - 3)
            End If
            Dim cmd As New SqlCommand
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter
            Dim str As String
            str = System.Configuration.ConfigurationManager.AppSettings("constr").ToString()
            Dim con As New SqlConnection(str)
            Dim Query As String
            Query = " select ROW_NUMBER() OVER(ORDER BY LatterLog.PK_LetterDetailID) as 'SrNO', addr.Name,addr.City as Station,LatterLog.LDate as LDate,LatterLog.Card_Date,LatterLog.Card_Rem,LatterLog.Mail_Rec,LatterLog.Mail_Send from Tbl_LetterMailComMaster_Detail_Two as LatterLog  "
            Query += " inner join Address_Master as addr on addr.Pk_AddressID=LatterLog.Fk_AddressId "
            If (criteria.Trim().Length > 0) Then
                Query += " where " + criteria + ""
            End If
            da = New SqlDataAdapter(Query, con)
            ds = New DataSet()
            da.Fill(ds)

          
            If ds.Tables(0).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GvLatterLogList.DataSource = Nothing
            Else
                GvLatterLogList.DataSource = ds.Tables(0)
                GvLatterLogList.Columns(0).Width = 40
            End If
            lblTotalCount.Text = "Total:" + ds.Tables(0).Rows.Count.ToString()
            ds.Dispose()
            da.Dispose()


        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnExportToexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToexcel.Click
        Try
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
            For i As Integer = 1 To GvLatterLogList.Columns.Count
                worksheet.Cells(1, i) = GvLatterLogList.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvLatterLogList.Rows.Count - 1
                For j As Integer = 0 To GvLatterLogList.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvLatterLogList.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next



        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try

    End Sub
End Class