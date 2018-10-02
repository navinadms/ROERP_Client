﻿Imports System.Data.SqlClient

Public Class RptVisitorDetail

    Dim strCriteria As String
    Dim dataTable As DataTable
    Dim ds As New DataSet
    Dim count As Integer

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        If (Class1.global.RptUserType = True) Then
            pnlFollowby.Visible = True
        Else
            pnlFollowby.Visible = False
        End If
    End Sub

    Function SplitString(ByVal str As String) As String
        If str.Equals("") Or str.Equals("All") Or str.Equals("All,") Then
            If (str.Equals("All,")) Then
                str = str.ToString().Substring(0, str.Length - 1)
            End If
            Return str
        End If
        Dim strfinal As String = ""
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
    Public Sub AutoCompated_Text()

        Dim GetVisitorDetail = linq_obj.SP_Get_Enq_VisitorDetailsList().ToList()
        For Each item As SP_Get_Enq_VisitorDetailsListResult In GetVisitorDetail
            txtFollowBy.AutoCompleteCustomSource.Add(item.FollowBy)
            txtExec.AutoCompleteCustomSource.Add(item.SalesExc)
            txtstatus.AutoCompleteCustomSource.Add(item.V_Status)
            txtFollowBy.AutoCompleteCustomSource.Add(item.FollowBy)
            txtEnqType.AutoCompleteCustomSource.Add(item.E_Type)
        Next
    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dsnew As New DataSet
        dgVisitorDetail.DataSource = Nothing
        dsnew.Dispose()

        Dim criteria As String
        criteria = " and "

        If txtMainEnq.Text.Trim() <> "" Then
            criteria = criteria + " vd.E_Type in (SELECT value FROM dbo.fn_Split('" + SplitString(txtMainEnq.Text.Trim()) + "',',')) and"
        End If
        If txtMainExec.Text.Trim() <> "" Then
            criteria = criteria + " vd.SalesExc in (SELECT value FROM dbo.fn_Split('" + SplitString(txtMainExec.Text.Trim()) + "',',')) and"
        End If
      
        If txtMainStatus.Text.Trim() <> "" Then
            criteria = criteria + " vd.V_Status in (SELECT value FROM dbo.fn_Split('" + SplitString(txtMainStatus.Text.Trim()) + "',',')) and"
        End If
      

        If criteria = " and " Then
            criteria = ""
        End If

        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If 
        Dim cmd As New SqlCommand
        cmd.Connection = Class1.con
        cmd.CommandText = "SP_SearchAllVisitorReportDetailByUser"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = dtEndDate.Value
        cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = dtStartDate.Value
        cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = If(Class1.global.RptUserType = True, SplitString(txtMainFollowBy.Text), Class1.global.User)
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
      
        Try
            Dim objclass As New Class1
            dsnew = objclass.GetEnqReportData(cmd)


            If dsnew.Tables(1).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgVisitorDetail.DataSource = Nothing
                dgVisitorCount.DataSource = Nothing
            Else
                dgVisitorDetail.DataSource = dsnew.Tables(1)
                dgVisitorDetail.Columns(dgVisitorDetail.Columns.Count - 1).Visible = False
                'bind Count
                Dim dsnewCount As New DataSet
                dsnewCount.Dispose()
                Dim cmdnew As New SqlCommand
                cmdnew.Connection = Class1.con
                cmdnew.CommandText = "SP_SearchAllVisitorReportDetailCountByUser"
                cmdnew.CommandType = CommandType.StoredProcedure
                cmdnew.Parameters.Add("@end", SqlDbType.DateTime).Value = dtEndDate.Value
                cmdnew.Parameters.Add("@start", SqlDbType.DateTime).Value = dtStartDate.Value
                cmdnew.Parameters.Add("@user", SqlDbType.VarChar).Value = If(Class1.global.RptUserType = True, SplitString(txtMainFollowBy.Text), Class1.global.User)
                cmdnew.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria


                Dim objclass1 As New Class1
                dsnewCount = objclass1.GetEnqReportData(cmdnew)


                If dsnewCount.Tables(1).Rows.Count < 1 Then
                    MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    dgVisitorCount.DataSource = Nothing

                Else
                    dgVisitorCount.DataSource = dsnewCount.Tables(1)

                End If
            End If


        Catch ex As Exception
            Class1.con.Close()
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub btnick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEnqType.Click
        If (txtEnqType.Text.Trim() <> "") Then
            txtMainEnq.Text += txtEnqType.Text.Trim() + ",".Trim()
        End If
        txtEnqType.Text = ""
        txtEnqType.Focus()

    End Sub

    Private Sub btnADDStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADDStatus.Click
        If (txtstatus.Text.Trim() <> "") Then
            txtMainStatus.Text += txtstatus.Text.Trim() + ",".Trim()
        End If
        txtstatus.Text = ""
        txtstatus.Focus()



    End Sub

    Private Sub btnAddExecutive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddExecutive.Click
        If (txtExec.Text.Trim() <> "") Then
            txtMainExec.Text += txtExec.Text.Trim() + ",".Trim()
        End If
        txtExec.Text = ""
        txtExec.Focus()
    End Sub

    Private Sub btnAddFollowby_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFollowby.Click
        If (txtFollowBy.Text.Trim() <> "") Then
            txtMainFollowBy.Text += txtFollowBy.Text.Trim() + ",".Trim()
        End If
        txtFollowBy.Text = ""
        txtFollowBy.Focus()
    End Sub
    Public Sub clearAll()
        txtMainEnq.Text = ""
        txtMainExec.Text = ""
        txtMainFollowBy.Text = ""
        txtMainStatus.Text = ""
        txtstatus.Text = ""
        txtFollowBy.Text = ""
    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub
    Function getData(ByVal str As String) As String
        If str.Equals("") Then
            Return ""
        End If
        Dim strfinal As String
        Dim cnt As Integer
        cnt = str.Split(",").Length
        Dim tmpstr As [String]() = New [String](cnt) {}
        tmpstr = str.Split(",")
        strfinal = "'"
        If cnt > 0 Then
            For index = 0 To tmpstr.Length - 1
                If tmpstr(index) <> "" Then
                    strfinal += tmpstr(index) + "','"
                End If
            Next
            strfinal += "'"
        End If

        Return strfinal
    End Function

    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
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
            For i As Integer = 1 To dgVisitorDetail.Columns.Count - 1
                worksheet.Cells(1, i) = dgVisitorDetail.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To dgVisitorDetail.Rows.Count - 1
                For j As Integer = 0 To dgVisitorDetail.Columns.Count - 2

                    If j = 4 Then
                        worksheet.Cells(i + 2, j + 1) = dgVisitorDetail.Rows(i).Cells(j).Value
                    Else
                        worksheet.Cells(i + 2, j + 1) = dgVisitorDetail.Rows(i).Cells(j).Value.ToString()
                    End If


                    count += i + 2 + j + 1
                Next
            Next


            ' save the application
            'workbook.SaveAs("d:\output(" & count & ").xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _
            ' Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

            '' Exit from the application
            '' app.Quit();
            '' storing Each row and column value to excel sheet
            'MessageBox.Show("Excel Download SucessFully on D:\output(" & count & ").xls ")
        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try

        '  MessageBox.Show("Successfully Exported")

    End Sub
End Class