Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.Sql
Imports pdfforge
Imports System.IO
Imports System.IO.File
Imports System.IO.StreamWriter
Imports Microsoft.Office.Interop
Imports System.Security.Principal
Imports System.Management
Imports System.Globalization
Imports System.Threading
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports

Public Class Expense_Voucher
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Pk_Address_ID As Integer
    Dim Pk_Expense_Inovice_ID As Integer
    Public Sub New()
        InitializeComponent()

        ddlEngineerList_Bind()
    End Sub '
    Public Sub ddlEngineerList_Bind()


        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")

        Dim data = linq_obj.SP_Get_Service_Engineer_Master_List()
        For Each item As SP_Get_Service_Engineer_Master_ListResult In data

            dt.Rows.Add(item.Pk_Engineer_ID, item.Name)

        Next


        ddlMainEngineer.DataSource = dt
        ddlMainEngineer.DisplayMember = "Name"
        ddlMainEngineer.ValueMember = "ID"



    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If ddlMainEngineer.SelectedValue > 0 Then
            'Dim rpt As New ReportDocument
            Dim ds As New DataSet
            Dim dt = New DataTable("Expense_Master")
            dt.Columns.Add("Engg_Name")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("EndDate")
            dt.Columns.Add("NoofDays")
            dt.Columns.Add("Exp_Date")
            dt.Columns.Add("ODType")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Station")
            dt.Columns.Add("Exp_Type")
            dt.Columns.Add("Exp_Remarks")
            dt.Columns.Add("Amount")
            dt.Columns.Add("GrossAmount")
            dt.Columns.Add("SupportEngg")

            Dim ExpenseList = linq_obj.SP_Get_Expense_Voucher_By_Engineer(Convert.ToInt64(ddlMainEngineer.SelectedValue), dtStart.Value.Date, dtEnd.Value.Date)

            Dim GrossAmount As Decimal

            Dim t1 As TimeSpan
            t1 = dtEnd.Value.Date - dtStart.Value.Date
            Dim NoofDay As Integer
            NoofDay = t1.TotalDays + 1 
            For Each item As SP_Get_Expense_Voucher_By_EngineerResult In ExpenseList
                GrossAmount = GrossAmount + item.Amount
                dt.Rows.Add(ddlMainEngineer.Text, dtStart.Value.Date.ToString("dd/MM/yyyy"), dtEnd.Value.Date.ToString("dd/MM/yyyy"), NoofDay, Convert.ToDateTime(item.CreateDate).ToString("dd/MM/yyyy"), item.ODType, 0, item.City + " - " + item.State, item.Expense_Type, item.Remarks, item.Amount, GrossAmount, item.EngineerName)

            Next
            ds.Tables.Add(dt)

            Dim rpt As New rptExpense_Report
            Class1.WriteXMlFile(ds, "SP_Get_Expense_Voucher_By_Engineer", "Expense_Master")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("Expense_Master"))



            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        End If
    End Sub

    Private Sub btnNewPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewPrint.Click
        If ddlMainEngineer.SelectedValue > 0 Then
            'Dim rpt As New ReportDocument
            Dim ds As New DataSet
            Dim dt = New DataTable("Expense_MasterNew")
            dt.Columns.Add("Engg_Name")
            dt.Columns.Add("StartDate")
            dt.Columns.Add("EndDate")
            dt.Columns.Add("NoofDays")
            dt.Columns.Add("Exp_Date")
            dt.Columns.Add("ODType")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Station")
            dt.Columns.Add("Exp_Type")
            dt.Columns.Add("Exp_Remarks")
            dt.Columns.Add("Amount")
            dt.Columns.Add("GrossAmount")
            dt.Columns.Add("SupportEngg")
            dt.Columns.Add("CustomerName")

            Dim ExpenseList = linq_obj.SP_Get_Expense_Voucher_By_Engineer_NewDesign(Convert.ToInt64(ddlMainEngineer.SelectedValue), dtStart.Value.Date, dtEnd.Value.Date)

            Dim GrossAmount As Decimal

            Dim t1 As TimeSpan
            t1 = dtEnd.Value.Date - dtStart.Value.Date
            Dim NoofDay As Integer
            NoofDay = t1.TotalDays + 1
            For Each item As SP_Get_Expense_Voucher_By_Engineer_NewDesignResult In ExpenseList
                GrossAmount = GrossAmount + item.Amount
                dt.Rows.Add(ddlMainEngineer.Text, dtStart.Value.Date.ToString("dd/MM/yyyy"), dtEnd.Value.Date.ToString("dd/MM/yyyy"), NoofDay, Convert.ToDateTime(item.CreateDate).ToString("dd/MM/yyyy"), item.ODType, 0, item.City + " - " + item.State, item.Expense_Type, item.Remarks, item.Amount, GrossAmount, item.EngineerName, item.Name)

            Next
            ds.Tables.Add(dt)

            Dim rpt As New rptExpense_Report_New
            Class1.WriteXMlFile(ds, "SP_Get_Expense_Voucher_By_Engineer_NewDesign", "Expense_MasterNew")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("Expense_MasterNew"))



            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        End If
    End Sub
End Class