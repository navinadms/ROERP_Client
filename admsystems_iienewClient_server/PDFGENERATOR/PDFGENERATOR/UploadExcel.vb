Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class UploadExcel

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Try
        '    Dim con As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
        '    con.Open()

        '    Dim connString As String = ""
        '    'Dim strFileType As String = Path.GetExtension(fileuploadExcel.FileName).ToLower()
        '    'Dim path__1 As String = fileuploadExcel.PostedFile.FileName
        '    'fileuploadExcel.SaveAs(Server.MapPath("~/test/" & path__1 & ""))
        '    'Dim path1 As String = "~/test/'" & path__1 & "'"
        '    ''Connection String to Excel Workbook
        '    'If strFileType.Trim() = ".xls" Then
        '    '    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("~/test/" & path__1 & "") & ";Extended Properties=Excel 12.0"
        '    'ElseIf strFileType.Trim() = ".xlsx" Then
        '    '    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("~/test/" & path__1 & "") & ";Extended Properties=Excel 12.0"
        '    'End If
        '    Dim query As String = "SELECT * FROM [Sheet1$]"
        '    Dim conn As New OleDbConnection(connString)
        '    If conn.State = ConnectionState.Closed Then
        '        conn.Open()
        '    End If
        '    Dim cmd As New OleDbCommand(query, conn)
        '    Dim da As New OleDbDataAdapter(cmd)
        '    Dim ds As New DataSet()
        '    da.Fill(ds)
        '    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '        Dim qry1 As String = "Insert into Teacher_Master values(1,1,1,'" & ds.Tables(0).Rows(i)("FirstName").ToString() & "'," & "'" & ds.Tables(0).Rows(i)("LastName").ToString() & "','" & getDatetime(ds.Tables(0).Rows(i)("DOB").ToString().Substring(0, 10)) & "'," & "'" & ds.Tables(0).Rows(i)("Gender").ToString() & "','" & ds.Tables(0).Rows(i)("Address").ToString().Replace("'", "''") & "'," & "'" & ds.Tables(0).Rows(i)("City").ToString() & "','" & ds.Tables(0).Rows(i)("ZipCode").ToString() & "'," & "'" & ds.Tables(0).Rows(i)("Phone").ToString() & "','" & ds.Tables(0).Rows(i)("Mobile").ToString() & "'," & "'" & ds.Tables(0).Rows(i)("Email").ToString() & "','" & 10001 & "',1,'123','" & ds.Tables(0).Rows(i)("Email").ToString() & "',0,0,'" & ds.Tables(0).Rows(i)("Qualification").ToString() & "'," & "'" & getDatetime(ds.Tables(0).Rows(i)("JoinDate").ToString().Substring(0, 10)) & "','#000000')"

        '        Dim cmd2 As New SqlCommand("set dateformat 'dmy'", con)
        '        cmd2.ExecuteNonQuery()
        '        Dim cmd1 As New SqlCommand(qry1, con)

        '        cmd1.ExecuteNonQuery()
        '    Next
        '    da.Dispose()
        '    conn.Close()
        '    conn.Dispose()
        'Catch generatedExceptionName As Exception

        '
        'End Try
    End Sub
End Class