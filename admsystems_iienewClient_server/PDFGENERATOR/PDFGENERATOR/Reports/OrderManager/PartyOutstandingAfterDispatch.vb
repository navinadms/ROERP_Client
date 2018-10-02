Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports System.Data.SqlClient
Public Class PartyOutstandingAfterDispatch
    Dim rpt As New ReportDocument

    Dim ss As String

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click

        ss = Application.LocalUserAppDataPath()

        Dim ds As New DataSet

        Dim cmd As New SqlCommand
        Dim con As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
        con.Open()

        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "Get_Rpt_Party_OutsatndingDetailAfterDispatch"
        cmd.Connection = con

        cmd.Parameters.AddWithValue("@From", dtFrom.Text)
        cmd.Parameters.AddWithValue("@To", dtTo.Text)


        Dim da As New SqlDataAdapter()
        da.SelectCommand = cmd
        da.Fill(ds, "Get_Rpt_Party_OutsatndingDetailAfterDispatch")

        Class1.WriteXMlFile(ds, "Get_Rpt_Party_OutsatndingDetailAfterDispatch", "Get_Rpt_Party_OutsatndingDetailAfterDispatch")




        CrystalReportViewer1.Dock = DockStyle.Fill
        rpt.Load(Application.StartupPath & "\Reports\Rpt_PartyOutstandingAfterDispatch.rpt")
        rpt.Database.Tables(0).SetDataSource(ds.Tables("Get_Rpt_Party_OutsatndingDetailAfterDispatch"))
        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.Show()



    End Sub
End Class