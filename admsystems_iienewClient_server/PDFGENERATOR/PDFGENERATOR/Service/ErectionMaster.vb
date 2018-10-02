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
Public Class ErectionMaster
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public Sub New()
        InitializeComponent()
        ddlEngineerList_Bind()
        ddlMachineItem_Bind()
    End Sub

    Public Sub ddlEngineerList_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")
        Dim data = linq_obj.SP_Get_Service_Engineer_Master_List().ToList()
        For Each item As SP_Get_Service_Engineer_Master_ListResult In data
            dt.Rows.Add(item.Pk_Engineer_ID, item.Name)
        Next
        ddlEngineerList.DataSource = dt
        ddlEngineerList.DisplayMember = "Name"
        ddlEngineerList.ValueMember = "ID"

        ddlEngineerList.AutoCompleteMode = AutoCompleteMode.Append
        ddlEngineerList.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEngineerList.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Public Sub ddlMachineItem_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")
        Dim data = linq_obj.SP_Get_Packing_Item_Master_List().ToList()
        For Each item As SP_Get_Packing_Item_Master_ListResult In data
            dt.Rows.Add(item.Pk_Packing_Item_Master_ID, item.Packing_Item)
        Next
        ddlMachineItem.DataSource = dt
        ddlMachineItem.DisplayMember = "Name"
        ddlMachineItem.ValueMember = "ID"
        ddlMachineItem.AutoCompleteMode = AutoCompleteMode.Append
        ddlMachineItem.DropDownStyle = ComboBoxStyle.DropDownList
        ddlMachineItem.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
   
    Private Sub txtAddEC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddEC.Click

    End Sub

    
End Class