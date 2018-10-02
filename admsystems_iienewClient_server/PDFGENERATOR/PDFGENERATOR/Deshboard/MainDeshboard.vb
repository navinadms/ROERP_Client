

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.Common
Imports System.Windows.Forms.DataVisualization.Charting

Public Class MainDeshboard
    Private con1 As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader
    Private da As SqlDataAdapter
    Private ds As DataSet

    Public Edi_Cat_ID As Int32
    Shared LanguageId As Int32
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim tran As DbTransaction = Nothing
    Public Sub New()
        InitializeComponent()
        Display_Deshboard()
        con1 = Class1.con
    End Sub

    Public Sub Display_Deshboard()

        'Engineer OD
        Dim EngineerOD = linq_obj.SP_Get_Deshboard_Service_Engineer_OD_Count(DtStart.Value.Date, DtEnd.Value.Date).ToList()
        lblECCount.Text = "0"
        lblServiceCount.Text = "0"
        lblVisitCount.Text = "0"
        lblTrainingCount.Text = "0"
        Dim TotalOD As Integer
        TotalOD = 0
        For Each item As SP_Get_Deshboard_Service_Engineer_OD_CountResult In EngineerOD

            If Convert.ToString(item.EnggType).ToLower() = "ec" Then
                lblECCount.Text = item.TotalCount.ToString()
                TotalOD = TotalOD + Convert.ToUInt64(item.TotalCount)

            ElseIf Convert.ToString(item.EnggType).ToLower() = "service" Then
                lblServiceCount.Text = item.TotalCount.ToString()
                TotalOD = TotalOD + Convert.ToUInt64(item.TotalCount)
            ElseIf Convert.ToString(item.EnggType).ToLower() = "visit" Then
                lblVisitCount.Text = item.TotalCount.ToString()
                TotalOD = TotalOD + Convert.ToUInt64(item.TotalCount)
            ElseIf Convert.ToString(item.EnggType).ToLower() = "training" Then
                lblTrainingCount.Text = item.TotalCount.ToString()
                TotalOD = TotalOD + Convert.ToUInt64(item.TotalCount)
            End If
             
        Next
        lblODTotal.Text = TotalOD.ToString()

        'Attendace Count

        Dim Attendance = linq_obj.SP_Get_Deshboard_Engineer_Attendance_Count(DtStart.Value.Date, DtEnd.Value.Date).ToList()

        lblAtteaAbsent.Text = "0"
        lblAtteLeave.Text = "0"
        lblAtteOffice.Text = "0"
        lblAttOD.Text = "0"
        lblAttOfficeOD.Text = "0"
        lblAtteTotal.Text = "0"

        Dim TotalAttandace As Integer
        TotalAttandace = 0
        For Each item As SP_Get_Deshboard_Engineer_Attendance_CountResult In Attendance

            If Convert.ToString(item.Status).ToLower() = "od" Then
                lblAttOD.Text = item.TotalDays.ToString()
                TotalAttandace = TotalAttandace + Convert.ToUInt64(item.TotalDays)
            ElseIf Convert.ToString(item.Status).ToLower() = "office od" Then
                lblAttOfficeOD.Text = item.TotalDays.ToString()
                TotalAttandace = TotalAttandace + Convert.ToUInt64(item.TotalDays)
            ElseIf Convert.ToString(item.Status).ToLower() = "absent" Then
                lblAtteaAbsent.Text = item.TotalDays.ToString()
                TotalAttandace = TotalAttandace + Convert.ToUInt64(item.TotalDays)
            ElseIf Convert.ToString(item.Status).ToLower() = "office" Then
                lblAtteOffice.Text = item.TotalDays.ToString()
                TotalAttandace = TotalAttandace + Convert.ToUInt64(item.TotalDays)
            ElseIf Convert.ToString(item.Status).ToLower() = "leave" Then
                lblAtteLeave.Text = item.TotalDays.ToString()
                TotalAttandace = TotalAttandace + Convert.ToUInt64(item.TotalDays)
            ElseIf Convert.ToString(item.Status).ToLower() = "leave" Then
                lblAtteLeave.Text = item.TotalDays.ToString()
                TotalAttandace = TotalAttandace + Convert.ToUInt64(item.TotalDays)
            End If

        Next
        lblAtteTotal.Text = TotalAttandace.ToString()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Display_Deshboard()
    End Sub

    Private Sub lblODChart_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblODChart.LinkClicked

        'System.Diagnostics.Process.Start("http://localhost:2397/ROERPChart/gentelella-master/production/EngineerOD.aspx?StartDate=" + DtStart.Text + "&EndDate=" + DtEnd.Text)
        System.Diagnostics.Process.Start("http://192.168.1.176/ROERPChart/gentelella-master/production/EngineerOD.aspx?StartDate=" + DtStart.Text + "&EndDate=" + DtEnd.Text)

    End Sub

    Private Sub lnkAttendance_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkAttendance.LinkClicked
        'System.Diagnostics.Process.Start("http://localhost:2397/ROERPChart/gentelella-master/production/AttendanceList.aspx?StartDate=" + DtStart.Text + "&EndDate=" + DtEnd.Text)
        System.Diagnostics.Process.Start("http://192.168.1.176/ROERPChart/gentelella-master/production/AttendanceList.aspx?StartDate=" + DtStart.Text + "&EndDate=" + DtEnd.Text)
    End Sub
End Class