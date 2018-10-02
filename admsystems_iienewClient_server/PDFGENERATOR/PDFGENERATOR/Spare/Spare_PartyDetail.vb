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
Imports System.IO
Imports System.Net

Public Class Spare_PartyDetail
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Pk_Address_ID As Integer
    Dim Pk_Service_Doc_ID As Integer
    Dim Pk_Spare_Followp As Integer
    Dim Pk_Spare_Payment_Receive_ID As Integer

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        GvSparePartyList_Bind()
        ddlFollowpStatus_Bind()

    End Sub

    Public Sub ddlFollowpStatus_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("FStatus")

        dt.Rows.Add("PAYMENT")
        dt.Rows.Add("QTN")
        dt.Rows.Add("SERVICE")
        dt.Rows.Add("OTHER")

        ddlFollowpStatus.DataSource = dt
        ddlFollowpStatus.DisplayMember = "FStatus"
        ddlFollowpStatus.ValueMember = "FStatus"
    End Sub
    Public Sub GvCredit_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("Pk_Spare_Payment_Receive_ID")
        dt.Columns.Add("Fk_Address_ID")
        dt.Columns.Add("ID")
        dt.Columns.Add("InvoiceNo")
        dt.Columns.Add("ReceiveDate")
        dt.Columns.Add("Amount")
        dt.Columns.Add("Remarks")
        Dim SrNo As Integer
        SrNo = 1
        Dim TotalCredit As Decimal
        TotalCredit = 0

        Dim Payment = linq_obj.SP_Get_Spare_Payment_Receive_By_Fk_Address_ID(Pk_Address_ID).ToList()
        For Each item As SP_Get_Spare_Payment_Receive_By_Fk_Address_IDResult In Payment
            TotalCredit = TotalCredit + item.Receive_Amount
            dt.Rows.Add(item.Pk_Spare_Payment_Receive_ID, item.Fk_Address_ID, SrNo, item.InvoiceNo, item.ReceiveDate, item.Receive_Amount, item.Remarks)
            SrNo = SrNo + 1

        Next
        GvCredit.DataSource = dt
        GvCredit.Columns(0).Visible = False
        GvCredit.Columns(1).Visible = False
        GvCredit.Columns(6).Visible = False
        lblCreditAmount.Text = TotalCredit.ToString("N2")

        lblOutStandingAmount.Text = (Convert.ToDecimal(lblDebitAmount.Text) - TotalCredit).ToString("N2")
        lblFlpOutstanding.Text = "PAYMENT OUTSTANDING :- " + lblOutStandingAmount.Text
    End Sub
    Public Sub GvDebit_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("TallyInvoiceNo")
        dt.Columns.Add("InvoiceDate")
        dt.Columns.Add("Amount")
        Dim SrNo As Integer
        SrNo = 1
        Dim TotalDebit As Decimal
        TotalDebit = 0

        Dim InvoiceList = linq_obj.SP_Get_Spare_Invoice_List_By_EnqNo(Pk_Address_ID).ToList().Where(Function(p) p.Status.ToLower() = "done").ToList()
        For Each item As SP_Get_Spare_Invoice_List_By_EnqNoResult In InvoiceList
            TotalDebit = TotalDebit + item.FinalPrice
            dt.Rows.Add(SrNo, item.Tally_Invoice_No, Convert.ToDateTime(item.InvoiceDate).ToString("dd/MM/yyyy"), item.FinalPrice)
            SrNo = SrNo + 1

        Next
        GvDebit.DataSource = dt
        lblDebitAmount.Text = TotalDebit.ToString("N2")

    End Sub

    Public Sub GvSparePartyList_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")
        dt.Columns.Add("FStatus")
        Dim criteria As String
        criteria = "and "
        If txtSearchEnqno.Text.Trim() <> "" Then
            criteria = criteria + " EnqNo like '%" + txtSearchEnqno.Text + "%'and "
        End If
        If txtSearchName.Text.Trim() <> "" Then
            criteria = criteria + " Name like '%" + txtSearchName.Text + "%'and "
        End If
        If txtSearchMobile.Text.Trim() <> "" Then
            criteria = criteria + " MobileNo like '%" + txtSearchMobile.Text + "%'and "
        End If
        If txtSearchEmail.Text.Trim() <> "" Then
            criteria = criteria + " EmailID like '%" + txtSearchEmail.Text + "%'and "
        End If

        If criteria = "and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.Trim().ToString().Substring(0, criteria.Trim().Length - 3)
        End If


        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Spare_Party_Allotment_AssinByUserId_Criteria"
        cmd.Parameters.Add("@Criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
        cmd.Parameters.AddWithValue("@UserID", Class1.global.UserID.ToString())

        'End 
        cmd.CommandTimeout = 3000
        Dim objclass As New Class1
        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvSparePartyList.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            For i = 0 To ds.Tables(1).Rows.Count - 1
                dt.Rows.Add(ds.Tables(1).Rows(i)("Pk_AddressID"), ds.Tables(1).Rows(i)("EnqNo"), ds.Tables(1).Rows(i)("Name"), ds.Tables(1).Rows(i)("FStatus"))

            Next
            GvSparePartyList.DataSource = dt
            txtTotal.Text = Convert.ToString(dt.Rows.Count)

        End If

    End Sub
    Private Sub GvSparePartyList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSparePartyList.DoubleClick
        Pk_Address_ID = GvSparePartyList.SelectedCells(0).Value
        Display_Data()
        GvSpareFollowp_Bind()
        GvSpareInvoice_Bind()
        GvSpareQuotation_Bind()
        GvDebit_Bind()
        GvCredit_Bind()
    End Sub

    Public Sub Display_Data()
        'Client Detail 
        Dim Claient = linq_obj.SP_Get_AddressListById(Pk_Address_ID).ToList()
        For Each item As SP_Get_AddressListByIdResult In Claient
            txtName.Text = item.Name
            txtaddress.Text = item.Address
            txtstation.Text = item.Area
            txtstate.Text = item.State
            txtdistrict.Text = item.District
            txtCity.Text = item.City
            txtPincode.Text = item.Pincode
            txtTaluka.Text = item.Taluka
            txtDelAddress.Text = item.DeliveryAddress
            txtDelStation.Text = item.DeliveryArea
            txtDelState.Text = item.DeliveryState
            txtDelDistrict.Text = item.DeliveryDistrict
            txtDelCity.Text = item.DeliveryCity
            txtDelPincode.Text = item.DeliveryPincode
            txtDelTaluka.Text = item.DeliveryTaluka
            txtcoperson.Text = item.ContactPerson
            txtphoneNo.Text = item.LandlineNo
            txtmobileNo.Text = item.MobileNo
            txtEmailID.Text = item.EmailID
            txtEnqNo.Text = item.EnqNo
            txtvalue1.Text = item.EmailID1
            txtValue2.Text = item.EmailID2
        Next

    End Sub

    Private Sub txtDaysAfter_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDaysAfter.Leave
        If txtDaysAfter.Text <> "" Then
            dtNFDate.Value = dtFollowDate.Value.Date.AddDays(txtDaysAfter.Text)
        End If
    End Sub
    Public Sub GvSpareFollowp_Bind()
        Dim data = linq_obj.SP_Get_Spare_Follow_Up_List_By_AddresID(Pk_Address_ID).ToList()
        GvSpareFollowp.DataSource = data
        GvSpareFollowp.Columns(0).Visible = False
        GvSpareFollowp.Columns(1).Visible = False

    End Sub
    Public Sub GvSpareQuotation_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("QuotationNo")
        dt.Columns.Add("Buss_Name")
        dt.Columns.Add("Letter_Date")
        dt.Columns.Add("Status")
        Dim SrNo As Integer
        SrNo = 1
        Dim dataQuotation = linq_obj.SP_Get_Spare_Quotation_Master_List().ToList().Where(Function(p) p.Fk_Address_ID = Pk_Address_ID).ToList()
        For Each item As SP_Get_Spare_Quotation_Master_ListResult In dataQuotation
            dt.Rows.Add(SrNo, item.QuotationNo, item.Buss_Name, Convert.ToDateTime(item.Letter_Date).ToString("dd/MM/yyyy"), item.Status)
            SrNo = SrNo + 1

        Next
        GvSpareQuotationList.DataSource = dt

    End Sub
    Public Sub GvSpareInvoice_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("InvoiceNo")
        dt.Columns.Add("TallyInvoiceNo")
        dt.Columns.Add("InvoiceDate")
        dt.Columns.Add("Status")
        dt.Columns.Add("Amount") 
        Dim SrNo As Integer
        SrNo = 1
        Dim InvoiceList = linq_obj.SP_Get_Spare_Invoice_List_By_EnqNo(Pk_Address_ID).ToList()
        For Each item As SP_Get_Spare_Invoice_List_By_EnqNoResult In InvoiceList
            dt.Rows.Add(SrNo, item.Invoice_No, item.Tally_Invoice_No, Convert.ToDateTime(item.InvoiceDate).ToString("dd/MM/yyyy"), item.Status, item.FinalPrice)
            SrNo = SrNo + 1

        Next
        GvInvoiceList.DataSource = dt

    End Sub
   

    Private Sub btnSaveFollowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFollowup.Click
        Try

            If btnSaveFollowup.Text = "Add" Then
                linq_obj.SP_Insert_Update_Spare_Follow_Up_Master(0, Pk_Address_ID, Convert.ToDateTime(dtFollowDate.Value.Date), Convert.ToDateTime(dtNFDate.Value.Date), txtFolloupDetail.Text, ddlFollowpStatus.Text, txtFollowpBywhoom.Text, txtFollowpProType.Text, txtFollowupRemark.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Add Sucessfully...")
            Else
                linq_obj.SP_Insert_Update_Spare_Follow_Up_Master(Pk_Spare_Followp, Pk_Address_ID, Convert.ToDateTime(dtFollowDate.Value.Date), Convert.ToDateTime(dtNFDate.Value.Date), txtFolloupDetail.Text, ddlFollowpStatus.Text, txtFollowpBywhoom.Text, txtFollowpProType.Text, txtFollowupRemark.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully...")
            End If

            GvSpareFollowp_Bind()
            Clear_Text()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub Clear_Text()
        btnSaveFollowup.Text = "Add"
        txtFolloupDetail.Text = ""

        txtFollowpBywhoom.Text = ""
        txtFollowpProType.Text = ""
        txtFollowupRemark.Text = ""
        dtFollowDate.Value = DateTime.Now
        Pk_Spare_Followp = 0



    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GvSparePartyList_Bind()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GvSparePartyList_Bind()
    End Sub

    Private Sub GvSpareFollowp_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSpareFollowp.DoubleClick
        btnSaveFollowup.Text = "Update"
        Pk_Spare_Followp = GvSpareFollowp.SelectedCells(0).Value
        dtFollowDate.Text = GvSpareFollowp.SelectedCells(2).Value
        dtNFDate.Text = GvSpareFollowp.SelectedCells(3).Value
        txtFolloupDetail.Text = GvSpareFollowp.SelectedCells(4).Value
        ddlFollowpStatus.Text = GvSpareFollowp.SelectedCells(5).Value
        txtFollowpBywhoom.Text = GvSpareFollowp.SelectedCells(6).Value
        txtFollowpProType.Text = GvSpareFollowp.SelectedCells(7).Value
        txtFollowupRemark.Text = GvSpareFollowp.SelectedCells(8).Value
    End Sub

    Private Sub btnPReceive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPReceive.Click
        Try

            If btnPReceive.Text = "Receive" Then
                linq_obj.SP_Insert_Update_Spare_Payment_Receive(0, Pk_Address_ID, Class1.global.UserID, txtPInvoice.Text, txtPAmount.Text, txtPRemarks.Text, Convert.ToDateTime(DtPaymentReceive.Value.Date))
                linq_obj.SubmitChanges()
                MessageBox.Show("Receive Sucessfully..")
            Else
                linq_obj.SP_Insert_Update_Spare_Payment_Receive(Pk_Spare_Payment_Receive_ID, Pk_Address_ID, Class1.global.UserID, txtPInvoice.Text, txtPAmount.Text, txtPRemarks.Text, Convert.ToDateTime(DtPaymentReceive.Value.Date))
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully..")
            End If

            GvCredit_Bind()
            PaymentClear_Text()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub PaymentClear_Text()

        txtPInvoice.Text = ""
        txtPAmount.Text = ""
        txtPRemarks.Text = ""
        btnPReceive.Text = "Receive"

    End Sub


    Private Sub GvPaymentReceive_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCredit.DoubleClick
        btnPReceive.Text = "Update" 

        Pk_Spare_Payment_Receive_ID = GvCredit.SelectedRows(0).Cells("Pk_Spare_Payment_Receive_ID").Value
        Pk_Address_ID = GvCredit.SelectedRows(0).Cells("Fk_Address_ID").Value
        txtPInvoice.Text = GvCredit.SelectedRows(0).Cells("InvoiceNo").Value.ToString()
        txtPAmount.Text = GvCredit.SelectedRows(0).Cells("Amount").Value.ToString()
        txtPRemarks.Text = GvCredit.SelectedRows(0).Cells("Remarks").Value.ToString()
        DtPaymentReceive.Text = GvCredit.SelectedRows(0).Cells("ReceiveDate").Value.ToString()

    End Sub

    Public Sub GvSpare_Follow_Allot_Bind(ByVal type As Integer)
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")
        dt.Columns.Add("FStatus")

        Dim FollowpData = linq_obj.SP_Get_Spare_Today_Followp_By_User(0, Class1.global.UserID, dtStart.Value.Date, dtEnd.Value.Date).ToList()
        If FollowpData.Count > 0 Then
            For Each item As SP_Get_Spare_Today_Followp_By_UserResult In FollowpData

                dt.Rows.Add(item.Pk_AddressID, item.EnqNo, item.Name, item.FStatus)

            Next
            GvSparePartyList.DataSource = dt
        Else
            GvSparePartyList.DataSource = Nothing
            MessageBox.Show("Record Not Found..")
        End If

    End Sub

    Private Sub btnTodayFollowp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTodayFollowp.Click
        GvSpare_Follow_Allot_Bind(0)
    End Sub

    Private Sub btnTodayAllotment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTodayAllotment.Click
        GvSpare_Follow_Allot_Bind(1)
    End Sub
End Class