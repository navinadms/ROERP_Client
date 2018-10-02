Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Public Class UserAttendanceData
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    Private Sub btnBrowseAttandace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseAttandace.Click

        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        ' imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Photo\") + openFileDialog1.SafeFileName
        txtExcelPath.Text = imgSrc
    End Sub

    Private Sub Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search.Click
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim DtSet As System.Data.DataSet
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
        MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + txtExcelPath.Text.Trim() + "';Extended Properties=Excel 8.0;")
        MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection)
        MyCommand.TableMappings.Add("Table", "Net-informations.com")
        DtSet = New System.Data.DataSet
        MyCommand.Fill(DtSet)
        GvAttendaceData.DataSource = DtSet.Tables(0)
        MyConnection.Close()

    End Sub
    Private Sub btnImportAttendance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportAttendance.Click


        Try

            For index = 0 To GvAttendaceData.Rows.Count

                Dim ID = linq_obj.SP_Insert_Update_User_Attendance_Log(Convert.ToInt64(GvAttendaceData.Rows(index).Cells("UserID").Value), Convert.ToDateTime(GvAttendaceData.Rows(index).Cells("AttandaceDate").Value), Convert.ToDecimal(GvAttendaceData.Rows(index).Cells("DayTime").Value), Convert.ToInt32(GvAttendaceData.Rows(index).Cells("AttaStatus").Value))
                linq_obj.SubmitChanges()
                If (ID > 0) Then
                    GvAttendaceData.Rows(index).DefaultCellStyle.BackColor = Color.Green
                    GvAttendaceData.Rows(index).DefaultCellStyle.ForeColor = Color.White

                Else
                    GvAttendaceData.Rows(index).DefaultCellStyle.BackColor = Color.Red
                    GvAttendaceData.Rows(index).DefaultCellStyle.ForeColor = Color.White
                End If

            Next

        Catch ex As Exception

        End Try
    End Sub



End Class