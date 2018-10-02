

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class PaymentReceive
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim rpt As New ReportDocument

    Dim ss As String
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ddlCreditPaymentType_Bind()

    End Sub
    Public Sub ddlCreditPaymentType_Bind()

        ddlPaymentType.Items.Clear()
        Dim Enq = linq_obj.SP_Get_PartyCredit_PaymentList().ToList()
        ddlPaymentType.DataSource = Enq
        ddlPaymentType.DisplayMember = "PType"
        ddlPaymentType.ValueMember = "PType"
        ddlPaymentType.AutoCompleteMode = AutoCompleteMode.Append
        ddlPaymentType.DropDownStyle = ComboBoxStyle.DropDownList
        ddlPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click

        ss = Application.LocalUserAppDataPath()


        Dim ds As New DataSet

        Dim cmd As New SqlCommand
        Dim con As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
        con.Open()

        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "Get_Rpt_PaymentTypeWiseReceivePayment"
        cmd.Connection = con

        cmd.Parameters.AddWithValue("@From", dtFrom.Text)
        cmd.Parameters.AddWithValue("@To", dtTo.Text)
        cmd.Parameters.AddWithValue("@PType", ddlPaymentType.Text)

        Dim da As New SqlDataAdapter()
        da.SelectCommand = cmd
        da.Fill(ds, "Get_Rpt_PaymentTypeWiseReceivePayment")

        Class1.WriteXMlFile(ds, "Get_Rpt_PaymentTypeWiseReceivePayment", "Get_Rpt_PaymentTypeWiseReceivePayment")


        
        CrystalReportViewer1.Dock = DockStyle.Fill

        rpt.Load(Application.StartupPath & "\Reports\Rpt_PaymentTypePaymentRecieve.rpt")
        rpt.Database.Tables(0).SetDataSource(ds.Tables("Get_Rpt_PaymentTypeWiseReceivePayment"))
        
        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.Show()




    End Sub
End Class