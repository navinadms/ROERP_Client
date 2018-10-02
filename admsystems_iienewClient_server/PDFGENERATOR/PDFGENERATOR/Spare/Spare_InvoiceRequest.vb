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
Imports CrystalDecisions.Shared

Public Class Spare_InvoiceRequest
    Dim PK_Address_ID As Integer
    Dim Pk_Spare_Invoice_Master_ID As Integer
    Dim Pk_Spare_Invoice_Detail_ID As Integer
    Dim OrderTempPath, QPath As String
    Dim TotalRate, TotalDisc, FinalAmount

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ddlCategory_Bind()
        ddlEngineerList_Bind()
        GvSpareInvoiceList_Bind()

        If Class1.global.UserAllotType.ToLower() = "head" Then

            btnAddNew.Visible = False
        End If


    End Sub
    Public Sub ddlCategory_Bind()

        Dim data = linq_obj.SP_Get_Spare_Category_List().ToList()
        ddlCategory.DataSource = data
        ddlCategory.DisplayMember = "Spare_Category"
        ddlCategory.ValueMember = "Pk_Spare_Category_ID"

        Dim dtstatus As New DataTable
        dtstatus.Columns.Add("Status")

        dtstatus.Rows.Add("Pending")
        dtstatus.Rows.Add("Final")
        dtstatus.Rows.Add("Done")
        dtstatus.Rows.Add("Cancel")

        ddlStatus.DataSource = dtstatus
        ddlStatus.DisplayMember = "Status"
        ddlStatus.ValueMember = "Status"

        Dim dtSearchstatus As New DataTable
        dtSearchstatus.Columns.Add("Status")
        dtSearchstatus.Rows.Add("All")
        dtSearchstatus.Rows.Add("Pending")
        dtSearchstatus.Rows.Add("Final")
        dtSearchstatus.Rows.Add("Done")
        dtSearchstatus.Rows.Add("Cancel")

        ddlSearchStatus.DataSource = dtSearchstatus
        ddlSearchStatus.DisplayMember = "Status"
        ddlSearchStatus.ValueMember = "Status"

    End Sub

    Public Sub TotalCalculation()
        Dim Total As Decimal
        Total = 0

        For index = 0 To GvSpareInvoiceDetail.Rows.Count - 1
            Total = Total + Convert.ToDecimal(GvSpareInvoiceDetail.Rows(index).Cells("FinalPrice").Value)

        Next
        txtTotalAmount.Text = Total.ToString("N2")


    End Sub
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


    Public Sub GvSpareInvoiceList_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")
        dt.Columns.Add("InvoiceNo")

        Dim criteria As String
        criteria = "and "
        If txtSearchEnqno.Text.Trim() <> "" Then
            criteria = criteria + " AM.EnqNo like '%" + txtSearchEnqno.Text + "%'and "
        End If
        If txtSearchName.Text.Trim() <> "" Then
            criteria = criteria + " AM.Name like '%" + txtSearchName.Text + "%'and "
        End If
        If txtSearchInvoiceNo.Text.Trim() <> "" Then
            criteria = criteria + "SIM.Invoice_No like '%" + txtSearchInvoiceNo.Text + "%'and "
        End If
        If ddlSearchStatus.SelectedValue.ToString().ToLower() <> "all" Then
            criteria = criteria + "SIM.Status like '%" + ddlSearchStatus.SelectedValue.ToString() + "%'and "
        End If

        If criteria = "and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.Trim().ToString().Substring(0, criteria.Trim().Length - 3)
        End If


        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Spare_Invoice_Master_Criteria"
        cmd.Parameters.Add("@Criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
        cmd.Parameters.AddWithValue("@UserID", Class1.global.UserID.ToString())

        'End 
        cmd.CommandTimeout = 3000
        Dim objclass As New Class1
        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvSpareInvoiceList.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            For i = 0 To ds.Tables(1).Rows.Count - 1
                dt.Rows.Add(ds.Tables(1).Rows(i)("PK_Spare_Invoice_ID"), ds.Tables(1).Rows(i)("EnqNo"), ds.Tables(1).Rows(i)("Name"), ds.Tables(1).Rows(i)("Invoice_No"))

            Next
            GvSpareInvoiceList.DataSource = dt
            'txtTotal.Text = Convert.ToString(dt.Rows.Count)

        End If

    End Sub

    Public Sub GvSpareInvoiceDetail_Bind()

        Dim detailinvoice = linq_obj.SP_Get_Spare_Invoice_Detail_by_InvoiceID(Pk_Spare_Invoice_Master_ID).ToList()
        GvSpareInvoiceDetail.DataSource = detailinvoice
        GvSpareInvoiceDetail.Columns(0).Visible = False
        GvSpareInvoiceDetail.Columns(1).Visible = False
        GvSpareInvoiceDetail.Columns(2).Visible = False

        TotalCalculation()
    End Sub
    Public Sub ddlCategory_Item_Bind()
        Dim data = linq_obj.SP_Get_Spare_Category_Item_By_Category_ID(Convert.ToInt64(ddlCategory.SelectedValue)).ToList()
        ddlItem.DataSource = data
        ddlItem.DisplayMember = "Item_Name"
        ddlItem.ValueMember = "Pk_Spare_Cat_Item_ID"

    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try

            Dim datacheck = linq_obj.SP_Check_Spare_Invoice_No(txtInvoiceNo.Text.Trim()).ToList()
            If datacheck.Count > 0 Then
                Pk_Spare_Invoice_Master_ID = datacheck(0).PK_Spare_Invoice_ID
                btnSubmit.Text = "Update"
            Else
                Pk_Spare_Invoice_Master_ID = linq_obj.SP_Insert_Update_Spare_Invoice_Master(0, PK_Address_ID, Class1.global.UserID, txtInvoiceNo.Text, txtTallyInvoiceNo.Text, Class1.global.UserID, Convert.ToInt64(ddlMainEngineer.SelectedValue), Convert.ToDateTime(dtDoneDate.Value.Date), txtRemarks.Text, ddlStatus.Text, txtTinNo.Text, txtGstNo.Text, Convert.ToDateTime(dtDoneDate.Value.Date))
                linq_obj.SubmitChanges()
                btnSubmit.Text = "Update"

            End If

            If btnAdd.Text = "Add" Then
                linq_obj.SP_Insert_Update_Spare_Invoice_Detail(0, Pk_Spare_Invoice_Master_ID, Convert.ToInt64(ddlCategory.SelectedValue), Convert.ToInt64(ddlItem.SelectedValue), Convert.ToDecimal(txtRate.Text), Convert.ToInt32(txtQty.Text), txtUnit.Text, Convert.ToDecimal(txtFinalRate.Text), Convert.ToDecimal(lblGST.Text), Convert.ToDecimal(txtGSTAmount.Text), Convert.ToDecimal(txtPrice.Text))
                linq_obj.SubmitChanges()
                MessageBox.Show("Add Sucessfully...")
            Else
                linq_obj.SP_Insert_Update_Spare_Invoice_Detail(Pk_Spare_Invoice_Detail_ID, Pk_Spare_Invoice_Master_ID, Convert.ToInt64(ddlCategory.SelectedValue), Convert.ToInt64(ddlItem.SelectedValue), Convert.ToDecimal(txtRate.Text), Convert.ToInt32(txtQty.Text), txtUnit.Text, Convert.ToDecimal(txtFinalRate.Text), Convert.ToDecimal(lblGST.Text), Convert.ToDecimal(txtGSTAmount.Text), Convert.ToDecimal(txtPrice.Text))
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully...")
            End If

            Clear_Text()
            GvSpareInvoiceList_Bind()
            GvSpareInvoiceDetail_Bind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Clear_Text()
        txtPrice.Text = ""
        txtQty.Text = ""
        txtGSTAmount.Text = ""
        txtPrice.Text = ""
        txtUnit.Text = ""
        txtFinalRate.Text = ""
        txtRate.Text = ""

        btnAdd.Text = "Add"
        Pk_Spare_Invoice_Detail_ID = 0



    End Sub

    Public Sub GetMax_Invoice()
        Dim ref As String
        Dim year1, MaxNo As Int32
        Dim Maxno1 = linq_obj.SP_Get_Max_Invoice_No().ToList()
        year1 = Convert.ToInt32(DtInvoice.Value.Year)
        ref = "PER/ " + Class1.global.User.ToString().ToUpper() + " / " + txtEnqNo.Text + " - " + Maxno1(0).MaxInvoiceNo.ToString() + " / " + year1.ToString()
        txtInvoiceNo.Text = ref.ToString()


    End Sub
    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label18.Click

    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        If txtEnqNo.Text.Trim() <> "" Then
            Dim data = linq_obj.SP_Get_ServiceODSite_Allotment_EnqNo(txtEnqNo.Text.Trim()).ToList()
            If data.Count > 0 Then
                For Each item As SP_Get_ServiceODSite_Allotment_EnqNoResult In data
                    txtPartyName.Text = item.Name
                    txtCity.Text = item.City
                    txtState.Text = item.State
                    txtTaluka.Text = item.Taluka
                    txtAddress.Text = item.Address
                    txtPincode.Text = item.Pincode
                    PK_Address_ID = item.Pk_AddressID
                    GetMax_Invoice()
                Next
            Else
                MessageBox.Show("Invalid EnqNo...")
                txtEnqNo.Focus()

            End If


        End If
    End Sub

    Private Sub ddlCategory_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectionChangeCommitted
        ddlCategory_Item_Bind()
    End Sub

    Private Sub ddlItem_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlItem.SelectionChangeCommitted
        Dim ItemData = linq_obj.SP_Get_Spare_Category_Item_By_Category_ID(Convert.ToInt64(ddlCategory.SelectedValue)).Where(Function(p) p.Pk_Spare_Cat_Item_ID = Convert.ToInt64(ddlItem.SelectedValue)).ToList()

        For Each item As SP_Get_Spare_Category_Item_By_Category_IDResult In ItemData
            txtRate.Text = item.Item_Rate
            txtUnit.Text = item.Qty
            txtQty.Text = "1"
            lblOrignalRate.Text = item.Item_Rate
            lblGST.Text = item.GST
            Discout_Cal()
        Next
    End Sub
    Public Sub Discout_Cal()
        btnAdd.Enabled = True
        If txtRate.Text.Trim() <> "" Then

            Dim finalamount As Decimal
            Dim GstAmount As Decimal
            Dim CheckDiscount As Decimal
            Dim FinalRate As Decimal
            Dim DiffAmount As Int64

            If ddlCategory.Text.Trim().ToLower() = "other" Then
                FinalRate = (Convert.ToDecimal(txtRate.Text)) * Convert.ToInt32(txtQty.Text)
                txtFinalRate.Text = FinalRate.ToString("N2")
                GstAmount = FinalRate * Convert.ToDecimal(lblGST.Text) / 100
                txtGSTAmount.Text = GstAmount.ToString("N2")
                txtPrice.Text = (FinalRate + GstAmount).ToString("N2")
            Else
                CheckDiscount = (Convert.ToDecimal(lblOrignalRate.Text) * 20) / 100
                DiffAmount = Convert.ToDecimal(lblOrignalRate.Text) - Convert.ToDecimal(CheckDiscount)
                If (Convert.ToDecimal(txtRate.Text) < Convert.ToDecimal(DiffAmount)) And Class1.global.User.ToLower() <> "rk" Then
                    MessageBox.Show("You have Not Permission...")
                    btnAdd.Enabled = False
                Else
                    FinalRate = (Convert.ToDecimal(txtRate.Text)) * Convert.ToInt32(txtQty.Text)
                    txtFinalRate.Text = FinalRate.ToString("N2")
                    GstAmount = FinalRate * Convert.ToDecimal(lblGST.Text) / 100
                    txtGSTAmount.Text = GstAmount.ToString("N2")
                    txtPrice.Text = (FinalRate + GstAmount).ToString("N2")
                End If


            End If
        End If


    End Sub

    Private Sub txtQty_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.Leave
        Discout_Cal()
    End Sub

    Private Sub txtRate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.Leave
        Discout_Cal()
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        If (btnSubmit.Text = "Update") Then

            Pk_Spare_Invoice_Master_ID = linq_obj.SP_Insert_Update_Spare_Invoice_Master(Pk_Spare_Invoice_Master_ID, PK_Address_ID, Class1.global.UserID, txtInvoiceNo.Text, txtTallyInvoiceNo.Text, Class1.global.UserID, Convert.ToInt64(ddlMainEngineer.SelectedValue), Convert.ToDateTime(dtDoneDate.Value.Date), txtRemarks.Text, ddlStatus.Text, txtTinNo.Text, txtGstNo.Text, Convert.ToDateTime(dtDoneDate.Value.Date))
            linq_obj.SubmitChanges()
            MessageBox.Show("Update Sucessfully...")
            Clear_All()
        End If
    End Sub
    Public Sub Display_Data(ByVal InvoiceNo As String)

        Dim data = linq_obj.SP_Check_Spare_Invoice_No(InvoiceNo).ToList()

        For Each item As SP_Check_Spare_Invoice_NoResult In data
            txtEnqNo.Text = item.EnqNo
            txtInvoiceNo.Text = item.Invoice_No
            txtPartyName.Text = item.Name
            txtCity.Text = item.City
            txtState.Text = item.State
            txtTaluka.Text = item.Taluka
            txtPincode.Text = item.Pincode
            txtRemarks.Text = item.Remark
            txtTinNo.Text = item.TinNo
            txtGstNo.Text = item.GSTNo
            txtAddress.Text = item.Address
            DtInvoice.Text = item.InvoiceDate
            PK_Address_ID = item.Fk_Address_ID
            txtTallyInvoiceNo.Text = item.Tally_Invoice_No
            ddlMainEngineer.SelectedValue = item.Fk_Engineer_ID
            ddlStatus.Text = item.Status
            dtDoneDate.Text = item.Done_Date
            Pk_Spare_Invoice_Master_ID = item.PK_Spare_Invoice_ID
            GvSpareInvoiceDetail_Bind()
            btnSubmit.Text = "Update"

        Next

    End Sub

    Private Sub GvSpareInvoiceList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSpareInvoiceList.DoubleClick
        Clear_All()
        txtEnqNo.Enabled = False
        txtInvoiceNo.Enabled = False
        DtInvoice.Enabled = False
        Display_Data(GvSpareInvoiceList.SelectedRows(0).Cells("InvoiceNo").Value.ToString())
    End Sub

    Public Sub Clear_All()
        txtEnqNo.Text = ""
        txtInvoiceNo.Text = ""
        txtPartyName.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtTaluka.Text = ""
        txtPincode.Text = ""
        txtRemarks.Text = ""
        txtTinNo.Text = ""
        txtGstNo.Text = ""
        txtPrice.Text = ""
        txtQty.Text = ""
        txtGSTAmount.Text = ""
        txtPrice.Text = ""
        txtUnit.Text = ""
        txtFinalRate.Text = ""
        txtEnqNo.Enabled = True
        txtInvoiceNo.Enabled = True
        DtInvoice.Enabled = True
        txtEnqNo.Text = ""
        txtInvoiceNo.Text = ""
        DtInvoice.Text = System.DateTime.Now.ToString()
        txtTallyInvoiceNo.Text = ""
        txtTotalAmount.Text = ""
        GvSpareInvoiceDetail.DataSource = Nothing
        PK_Address_ID = 0
        Pk_Spare_Invoice_Detail_ID = 0
        Pk_Spare_Invoice_Master_ID = 0
        btnSubmit.Text = "Submit"

    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Clear_All()
    End Sub

    Private Sub GvSpareInvoiceDetail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSpareInvoiceDetail.DoubleClick
        btnAdd.Text = "Update"

        Pk_Spare_Invoice_Detail_ID = GvSpareInvoiceDetail.SelectedRows(0).Cells("Pk_Spare_Invoice_Detail_ID").Value
        ddlCategory.SelectedValue = GvSpareInvoiceDetail.SelectedRows(0).Cells("Fk_Spare_Category_ID").Value
        ddlCategory_SelectionChangeCommitted(Nothing, Nothing)
        ddlItem.SelectedValue = GvSpareInvoiceDetail.SelectedRows(0).Cells("Fk_Spare_Cat_Item_ID").Value
        txtRate.Text = GvSpareInvoiceDetail.SelectedRows(0).Cells("Rate").Value.ToString()
        lblOrignalRate.Text = GvSpareInvoiceDetail.SelectedRows(0).Cells("Rate").Value.ToString()
        txtQty.Text = GvSpareInvoiceDetail.SelectedRows(0).Cells("Qty").Value.ToString()
        txtUnit.Text = GvSpareInvoiceDetail.SelectedRows(0).Cells("Unit").Value.ToString()
        txtFinalRate.Text = GvSpareInvoiceDetail.SelectedRows(0).Cells("FinalRate").Value.ToString()
        lblGST.Text = Convert.ToString(GvSpareInvoiceDetail.SelectedRows(0).Cells("GST").Value)
        txtGSTAmount.Text = Convert.ToString(GvSpareInvoiceDetail.SelectedRows(0).Cells("GST_Amount").Value)
        txtPrice.Text = GvSpareInvoiceDetail.SelectedRows(0).Cells("FinalPrice").Value.ToString()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        If ddlMainEngineer.SelectedValue > 0 Then
            'Dim rpt As New ReportDocument
            Dim ds As New DataSet
            Dim dt = New DataTable("PerformaInvoice")
            dt.Columns.Add("InvoiceNo")
            dt.Columns.Add("InvoiceDate")
            dt.Columns.Add("BuyerName")
            dt.Columns.Add("Address")
            dt.Columns.Add("City")
            dt.Columns.Add("State")
            dt.Columns.Add("Pincode")
            dt.Columns.Add("ContactPerson")
            dt.Columns.Add("ContactNo")
            dt.Columns.Add("TinNo")
            dt.Columns.Add("GSTNo")
            dt.Columns.Add("Description")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Qty")
            dt.Columns.Add("Amount")
            dt.Columns.Add("GST")
            dt.Columns.Add("FinalAmount")
            dt.Columns.Add("GrossTotal")

            Dim InvoiceList = linq_obj.SP_Get_Spare_Invoice_Print_ID(Pk_Spare_Invoice_Master_ID)
            Dim GrossTotal As Decimal
            Dim Address As String
            Address = ""
            Address = txtAddress.Text + "," + txtCity.Text.Trim() + "," + txtState.Text + " " + txtPincode.Text

            For Each item As SP_Get_Spare_Invoice_Print_IDResult In InvoiceList
                GrossTotal = GrossTotal + item.FinalPrice
                dt.Rows.Add(item.Invoice_No, Convert.ToDateTime(item.InvoiceDate).ToString("dd/MM/yyyy"), txtPartyName.Text, Address, item.City, item.State, item.Pincode, item.ContactPerson, item.MobileNo, item.TinNo, item.GSTNo, item.Item_Name, item.Rate, item.Qty, item.FinalRate, item.Gst_Amount.ToString() + "(" + item.Gst.ToString() + ")", item.FinalPrice, Math.Round(GrossTotal, 0))

            Next
            'Dim GrossAmount As Decimal

            'Dim t1 As TimeSpan
            't1 = dtEnd.Value.Date - dtStart.Value.Date
            'Dim NoofDay As Integer
            'NoofDay = t1.TotalDays + 1
            'For Each item As SP_Get_Expense_Voucher_By_EngineerResult In ExpenseList
            '    GrossAmount = GrossAmount + item.Amount
            '    dt.Rows.Add(ddlMainEngineer.Text, dtStart.Value.Date.ToString("dd/MM/yyyy"), dtEnd.Value.Date.ToString("dd/MM/yyyy"), NoofDay, Convert.ToDateTime(item.CreateDate).ToString("dd/MM/yyyy"), item.ODType, 0, item.City + " - " + item.State, item.Expense_Type, item.Remarks, item.Amount, GrossAmount, item.EngineerName)

            'Next
            ds.Tables.Add(dt)

            Dim rpt As New rpt_SpareInvoice
            Class1.WriteXMlFile(ds, "SP_Get_Spare_Invoice_Print_ID", "PerformaInvoice")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("PerformaInvoice"))



            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        End If


    End Sub

    Private Sub ddlSearchStatus_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlSearchStatus.SelectionChangeCommitted
        GvSpareInvoiceList_Bind()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GvSpareInvoiceList_Bind()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        txtSearchEnqno.Text = ""
        txtSearchInvoiceNo.Text = ""
        txtSearchName.Text = ""
        ddlSearchStatus.Text = "All"

        GvSpareInvoiceList_Bind()
    End Sub
End Class