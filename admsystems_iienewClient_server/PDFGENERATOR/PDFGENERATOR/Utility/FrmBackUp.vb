Imports System.Data
Imports System.Data.SqlClient
Public Class FrmBackUp

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFilePath.Text = SaveFileDialog1.FileName
        End If
    End Sub

    Private Sub btnBackUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackUp.Click
        Try

            Dim sBackupPath As String
            sBackupPath = txtFilePath.Text

            If sBackupPath = "" Then
                MessageBox.Show("Select Save file name")
                Exit Sub
            End If
            sBackupPath = sBackupPath & ".bak"
            Dim sqlConn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
            sqlConn.Open()

            Dim sCommand = "BACKUP DATABASE ROTESTDB TO DISK = N'" & sBackupPath & "'WITH COPY_ONLY "

            Dim sqlCmd As New SqlCommand(sCommand, sqlConn)
            sqlCmd.ExecuteNonQuery()
            MessageBox.Show("Backup successful")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub FrmBackUp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim regDate As Date = Date.Now()
        Dim strDate As String = regDate.ToString("ddMMMyyyy")

        Dim sBackUpPath As String = Application.StartupPath & "\BackUp\ROTESTDB" & strDate
        txtFilePath.Text = sBackUpPath
    End Sub
End Class