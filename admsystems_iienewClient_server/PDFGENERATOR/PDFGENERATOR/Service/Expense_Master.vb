
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
Public Class Expense_Master
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Pk_Address_ID As Integer
    Dim Pk_Expense_Inovice_ID As Integer

    Public Sub New()
        InitializeComponent()
        ddlExpenseList_Bind()
        GvExpenseList_Bind()
        ddlEnggType_Bind()
        ddlODType_Bind()
       
    End Sub

    Public Sub ddlEnggType_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("EnggType")

        dt.Rows.Add("Work")
        dt.Rows.Add("Up")
        dt.Rows.Add("Down")

        ddlEnggType.DataSource = dt
        ddlEnggType.DisplayMember = "EnggType"
        ddlEnggType.ValueMember = "EnggType"


        Dim dtstatus As New DataTable
        dtstatus.Columns.Add("Status")
        dtstatus.Rows.Add("Running")
        dtstatus.Rows.Add("Done")

        ddlStatus.DataSource = dtstatus
        ddlStatus.DisplayMember = "Status"
        ddlStatus.ValueMember = "Status"



    End Sub

    Public Sub ddlODType_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("ODType")

        dt.Rows.Add("EC")
        dt.Rows.Add("Service")
        dt.Rows.Add("Visit")
        dt.Rows.Add("Training")
        dt.Rows.Add("Warranty")

        ddlODType.DataSource = dt
        ddlODType.DisplayMember = "ODType"
        ddlODType.ValueMember = "ODType"


    End Sub


    Public Sub GvExpenseList_Bind()

        Dim Expense_Status As String
        Expense_Status = "Running"
        ddlStatus.Text = "Running"
        If rblDoneSearch.Checked = True Then
            Expense_Status = "Done"
            ddlStatus.Text = "Done"
        End If

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")
        dt.Columns.Add("InvoiceNo")
        dt.Columns.Add("Date")

        Dim data = linq_obj.SP_Get_Expense_Invoice_Master(Expense_Status).ToList()

        For Each item As SP_Get_Expense_Invoice_MasterResult In data

            dt.Rows.Add(item.Pk_Expense_Invoce_ID, item.EnqNo, item.Name, item.Invoice_No, item.Invoice_Date)
        Next
        If (data.Count > 0) Then

            GvExpenseList.DataSource = dt
           
        Else
            GvExpenseList.DataSource = Nothing

        End If
       
    End Sub
    Public Sub GvExpenseDetail_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("Pk_ID")
        dt.Columns.Add("Date")
        dt.Columns.Add("EngType")
        dt.Columns.Add("ODtype")
        dt.Columns.Add("ExpenseType")
        dt.Columns.Add("Amount")
        dt.Columns.Add("Remarks")
        Dim ExpenseDetail = linq_obj.SP_Get_Expense_Party_Detail_List(Pk_Expense_Inovice_ID).ToList()

        For Each item As SP_Get_Expense_Party_Detail_ListResult In ExpenseDetail
            ddlStatus.Text = item.Expense_Status

            dt.Rows.Add(item.PK_Expense_Master_ID, Convert.ToDateTime(item.CreateDate).ToString("dd/MM/yyyy"), item.EnggType, item.ODType, item.Expense_Type, item.Amount, item.Remarks)

        Next
        GvExpenseDetail.DataSource = dt
        GvExpenseDetail.Columns(0).Visible = False
    End Sub
    Public Sub ddlEngineerList_Bind()
        If txtEnqNo.Text.Trim() <> "" Then
            Dim dt As New DataTable
            dt.Columns.Add("ID")
            dt.Columns.Add("Name")
            ChkEngineer.Items.Clear()

            Dim data = linq_obj.SP_Get_Service_Engineer_Assign_Attendance_List(txtDate.Value.Date, Pk_Address_ID).ToList()

            If data.Count > 0 Then
                For Each item As SP_Get_Service_Engineer_Assign_Attendance_ListResult In data
                    ChkEngineer.Items.Add(item.Name + " | " + Convert.ToString(item.Pk_Engineer_ID), True)
                    dt.Rows.Add(item.Pk_Engineer_ID, item.Name)
                Next

                ddlMainEngineer.DataSource = dt
                ddlMainEngineer.DisplayMember = "Name"
                ddlMainEngineer.ValueMember = "ID"
            Else
                ddlMainEngineer.DataSource = Nothing
            End If


        End If
    End Sub
    Public Sub ddlExpenseList_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("PK_Expense_Type_ID")
        dt.Columns.Add("Expense_Type")
        Dim data = linq_obj.SP_Get_Expense_Type_List().ToList()
        For Each item As SP_Get_Expense_Type_ListResult In data
            dt.Rows.Add(item.PK_Expense_Type_ID, item.Expense_Type)
        Next
        ddlExpense.DataSource = dt
        ddlExpense.DisplayMember = "Expense_Type"
        ddlExpense.ValueMember = "PK_Expense_Type_ID"
        ddlExpense.SelectAll()


    End Sub

    Private Sub btnExpeseAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpeseAdd.Click
        Try
            If (ddlStatus.Text.ToLower() = "running") Then
                Dim Engineer_ID As String
                Engineer_ID = ""
                Dim sb As New System.Text.StringBuilder
                For Each item In ChkEngineer.CheckedItems

                    Dim strArr() As String
                    strArr = item.Split("|")
                    Engineer_ID = Engineer_ID & strArr(1).Trim() & ","
                Next

                If Convert.ToInt64(ddlMainEngineer.SelectedValue) > 0 And Engineer_ID.Trim().Length() > 0 Then
                    If txtAmount.Text.Trim() <> "" Then
                        Pk_Expense_Inovice_ID = linq_obj.SP_Insert_Update_Expense_Invoice_Master(0, Pk_Address_ID, txtInvoiceNo.Text, ddlStatus.Text, dtInvoiceDate.Value.Date)
                        linq_obj.SubmitChanges()

                        linq_obj.SP_Insert_Update_Expense_Master(0, Pk_Expense_Inovice_ID, Convert.ToInt64(ddlMainEngineer.SelectedValue), Engineer_ID, Convert.ToUInt32(ddlExpense.SelectedValue), ddlEnggType.Text, ddlODType.Text, txtAmount.Text, txtRemarks.Text, txtDate.Value.Date)
                        linq_obj.SubmitChanges()
                        GvExpenseList_Bind()
                        GvExpenseDetail_Bind()
                        Clear_Text()

                        ddlExpense.Focus()
                    Else
                        MessageBox.Show("Amount is Blank...")
                    End If
                Else
                    MessageBox.Show("Please Select Main Engineer & Engineer...")
                End If
            Else
                MessageBox.Show("Invoice Status must be Running")

            End If
        Catch ex As Exception

        End Try



    End Sub

    Public Sub Clear_Text()

        txtAmount.Text = ""
        txtRemarks.Text = ""
    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        Dim AddressData = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text.Trim()).ToList()
        For Each item As SP_Get_AddressListByEnqNoResult In AddressData
            Pk_Address_ID = item.Pk_AddressID
            txtPartyName.Text = item.Name
            txtCity.Text = item.City
            txtDistrict.Text = item.District
            txtTaluka.Text = item.Taluka
            txtState.Text = item.State
        Next
        ddlEngineerList_Bind()
        GvExpenseDetail_Bind()
    End Sub






    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Clear_All()
        txtEnqNo.Enabled = True
        dtInvoiceDate.Enabled = True
    End Sub

    Public Sub Clear_All()

        txtEnqNo.Text = ""
        txtInvoiceNo.Text = ""
        txtPartyName.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtDistrict.Text = ""
        txtTaluka.Text = ""
        txtAmount.Text = ""
        txtRemarks.Text = ""
        Pk_Address_ID = 0
        Pk_Expense_Inovice_ID = 0

        ChkEngineer.Items.Clear()
        GvExpenseDetail.DataSource = Nothing

    End Sub

    Private Sub rblDoneSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblDoneSearch.CheckedChanged
        Clear_All()
        GvExpenseList_Bind()
        
    End Sub

    Private Sub rblPendingSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblPendingSearch.CheckedChanged
        Clear_All()
        GvExpenseList_Bind()
       
    End Sub

    Private Sub btnUpdateInvoiceStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateInvoiceStatus.Click
        linq_obj.SP_Update_Expense_Invoice_Master(Pk_Expense_Inovice_ID, txtInvoiceNo.Text, ddlStatus.Text)
        linq_obj.SubmitChanges()
        MessageBox.Show("Update Sucessfully...")
        GvExpenseList_Bind()
        Clear_All()
    End Sub

    Private Sub GvExpenseList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvExpenseList.DoubleClick

        txtEnqNo.Enabled = False
        dtInvoiceDate.Enabled = False 
        Pk_Expense_Inovice_ID = GvExpenseList.SelectedRows(0).Cells(0).Value
        txtEnqNo.Text = GvExpenseList.SelectedRows(0).Cells(1).Value
        txtEnqNo_Leave(Nothing, Nothing)


        txtInvoiceNo.Text = GvExpenseList.SelectedRows(0).Cells(3).Value
        dtInvoiceDate.Text = Convert.ToString(GvExpenseList.SelectedRows(0).Cells(4).Value)


    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        '
        If GvExpenseDetail.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                linq_obj.SP_Delete_Expense_Master_Data(Convert.ToInt64(GvExpenseDetail.SelectedCells(0).Value))
                linq_obj.SubmitChanges()
                GvExpenseDetail_Bind()
            End If
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New Expense_Voucher
        frm.ShowDialog()
    End Sub


    Private Sub txtDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDate.ValueChanged

        If txtEnqNo.Text.Trim() <> "" Then
            ddlEngineerList_Bind()
        Else
            MessageBox.Show("EnqNo is Blank...")
        End If

    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")
        dt.Columns.Add("InvoiceNo")
        dt.Columns.Add("Date")

        If txtEnqNoSearch.Text.Trim() <> "" Then
            Dim data = linq_obj.SP_Get_Expense_Invoice_Master_Search_Criteria().ToList().Where(Function(p) p.EnqNo.ToLower().Contains(txtEnqNoSearch.Text.Trim().ToLower())).ToList()
            For Each item As SP_Get_Expense_Invoice_Master_Search_CriteriaResult In data
                dt.Rows.Add(item.Pk_Expense_Invoce_ID, item.EnqNo, item.Name, item.Invoice_No, item.Invoice_Date)
            Next
            If (data.Count > 0) Then
                GvExpenseList.DataSource = dt
            Else
                GvExpenseList.DataSource = Nothing
            End If
        Else
            Dim data = linq_obj.SP_Get_Expense_Invoice_Master_Search_Criteria().ToList().Where(Function(p) p.Name.ToLower().Contains(txtNameSearch.Text.Trim().ToLower())).ToList()

            For Each item As SP_Get_Expense_Invoice_Master_Search_CriteriaResult In data
                dt.Rows.Add(item.Pk_Expense_Invoce_ID, item.EnqNo, item.Name, item.Invoice_No, item.Invoice_Date)
            Next
            If (data.Count > 0) Then
                GvExpenseList.DataSource = dt
            Else
                GvExpenseList.DataSource = Nothing
            End If

        End If 
    End Sub

    Private Sub lnkCalculator_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCalculator.LinkClicked
        System.Diagnostics.Process.Start("calc.exe")
    End Sub
End Class