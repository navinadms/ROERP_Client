Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine.ReportDocument
Public Class OrderReportISIProcess
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim dataAddress As DataTable
    Dim count As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Vendor_Autocomplated()

    End Sub

    Public Sub Vendor_Autocomplated()


        txtUserName.AutoCompleteCustomSource.Clear()
        Dim data = linq_obj.SP_Get_Tbl_ISIProcessMaster_Two_AutoComplate().ToList()
        For Each iteam As SP_Get_Tbl_ISIProcessMaster_Two_AutoComplateResult In data
            txtUserName.AutoCompleteCustomSource.Add(iteam.Vender)
        Next

    End Sub



    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criteria As String
        criteria = ""
        If txtUserMul.Text.Trim() <> "" Then
            criteria = criteria + "ISI_M.Vender in (" + txtUserMul.Text.Trim().TrimEnd(",") + ") and "
        End If
        ''ISI Process Vendor Send  Between Date 
        If (RblSendDate.Checked = True) Then
            criteria = criteria + "ISI_M.Vendor_Date  >= convert(varchar(10), '" + Class1.getDate(txtstartDt.Text) + "',112) and ISI_M.Vendor_Date <= convert(varchar(10), '" + Class1.getDate(txtEndDate.Text) + "',112) and "
        End If
        ''ISI Process Vendor Receive  Between Date 
        If (rblReceiveDate.Checked = True) Then
            criteria = criteria + "ISI_M.Receive_Date  >= convert(varchar(10), '" + Class1.getDate(txtstartDt.Text) + "',112) and ISI_M.Receive_Date <= convert(varchar(10), '" + Class1.getDate(txtEndDate.Text) + "',112) and "
        End If

        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Trim().Substring(0, criteria.Trim().Length - 3)
        End If
        Dim cmd As New SqlCommand
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim str As String
        str = System.Configuration.ConfigurationManager.AppSettings("constr").ToString()
        Dim con As New SqlConnection(str)
        Dim Query As String
        Query = "select ROW_NUMBER() OVER(ORDER BY addr.Pk_AddressID ) as 'SrNo', addr.Name,addr.City as Station,ISI_M.Scheme_Name,ISI_M.F_Submit_P,ISI_M.BIS_Insp_Date, ISI_D.ByWhom,ISI_D.FDate,ISI_D.NFDate,ISI_D.Remarks from dbo.Tbl_ISIProcessMaster_Two as ISI_M  "
        Query += " inner join dbo.Tbl_ISIProcess_DetailMaster_Two aS ISI_D on ISI_M.Fk_AddressId=ISI_D.Fk_AddressId "
        Query += " inner join dbo.Address_Master as addr on ISI_M.Fk_AddressId=addr.Pk_AddressID where addr.EnqStatus=1 "
        If (criteria.Trim().Length > 0) Then
            Query += " and  " + criteria + ""
        End If
        da = New SqlDataAdapter(Query, con)
        ds = New DataSet()
        da.Fill(ds)
        If ds.Tables(0).Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            GvISIProcessList.DataSource = Nothing
        Else
            GvISIProcessList.DataSource = ds.Tables(0)
            GvISIProcessList.Columns(0).Width = 40
        End If
        lblTotalCount.Text = "Total:" + ds.Tables(0).Rows.Count.ToString()
        ds.Dispose()
        da.Dispose()


    End Sub

    Private Sub btnUserAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserAdd.Click
        Dim str As String = "'" + String.Join("','", txtUserName.Text.ToString()) + "'"
        txtUserMul.Text += str + ","
        txtUserName.Text = ""




    End Sub
End Class