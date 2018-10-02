Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Public Class FrmCommanReportView
    Dim rptDoc As New ReportDocument

    Public Sub New(ByVal rpt As ReportDocument)

        ' This call is required by the designer.
        InitializeComponent()
        rptDoc = rpt
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub FrmCommanReportView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CrystalReportViewer1.ReportSource = rptDoc
        CrystalReportViewer1.Show()
    End Sub
End Class