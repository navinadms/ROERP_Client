Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Configuration
Imports System.Data.SqlClient


Public Class ViewStockDetail
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Category_ID As Integer
    Dim ds As New DataSet

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindDetail()
    End Sub
    Public Sub bindDetail()
        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Total_INOUT_Detail"
        Dim objclass As New Class1
        ds = objclass.GetStockData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            dgViewStock.DataSource = ds.Tables(1)
            dgViewStock.Columns(dgViewStock.Columns.Count - 2).Visible = False
            dgViewStock.Columns(dgViewStock.Columns.Count - 1).Visible = False
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindDetail()
    End Sub
End Class