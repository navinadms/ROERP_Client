﻿Imports System.Data.SqlClient

Public Class PartyInvoiceMaster
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim PartyId As Integer
    Dim Fk_PartyDebitId As Integer
    Dim stringAddress As String
    Dim tblInvoiceDetail As New DataTable

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoComplete_Text()
        bindEnqGrid()
        RavSoft.CueProvider.SetCue(txtbankDetail, "Bank Information")
        RavSoft.CueProvider.SetCue(txtTransporter, "Transporter Name")
        txtTotRI.Text = Convert.ToString(GDVInqDetail.Rows.Count)
        getPageRight()
    End Sub

    Public Sub getPageRight()
        Try
            Dim dv As New DataTable
            Dim dataView As DataView

            Dim Class1 As New Class1
            Dim RowCount As Integer
            Dim statusCheck As Boolean = False

            Dim strName As String = ""

            dataView = Class1.global.UserPermissionDataset.Tables(0).DefaultView
            dataView.RowFilter = "([DetailName] like 'Invoice Master')"


            If (dataView.Count > 0) Then

                dv = dataView.ToTable()

                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            btnSave.Enabled = True
                        Else
                            btnSave.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsUpdate") = True) Then
                            btnchange.Enabled = True
                        Else
                            btnchange.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsDelete") = True) Then
                            btnDel.Enabled = True
                        Else
                            btnDel.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsPrint") = True) Then
                            btnReportInvoice.Enabled = True
                        Else
                            btnReportInvoice.Enabled = False

                        End If

                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Public Sub AutoComplete_Text()
        txtParty.AutoCompleteCustomSource.Clear()
        Dim dataParty = linq_obj.SP_Get_AddressListAutoComplete("Name").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In dataParty
            txtParty.AutoCompleteCustomSource.Add(iteam.Result)
        Next

        Dim data = linq_obj.SP_Get_AddressListAutoCompleteForOrder("Name").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteForOrderResult In data
            txtBuyerName.AutoCompleteCustomSource.Add(iteam.Result)
        Next
        Dim dataEnq = linq_obj.SP_Get_AddressListAutoCompleteForOrder("EnqNo").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteForOrderResult In dataEnq
            txtEntryNo.AutoCompleteCustomSource.Add(iteam.Result)
        Next
    End Sub

    Private Sub btnPOSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim res As Integer
        res = linq_obj.SP_Insert_Tbl_InvoiceMaster(Address_ID, Fk_PartyDebitId, PartyId, txtInvoiceNo.Text, txtTransporter.Text, txtRRLRNo.Text,
                                                   If(txtBasic.Text.Trim() = "", 0, Convert.ToDecimal(txtBasic.Text.Trim())), If(txtDiscount.Text.Trim() = "", 0, Convert.ToDecimal(txtDiscount.Text.Trim())), If(txtTotal.Text.Trim() = "", 0, Convert.ToDecimal(txtTotal.Text.Trim())),
                                                   If(txtCstPerc.Text.Trim() = "", 0, Convert.ToInt32(txtCstPerc.Text)), If(txtSubTotal.Text.Trim() = "", 0, Convert.ToDecimal(txtSubTotal.Text.Trim())), If(txtAdvance.Text.Trim() = "", 0, Convert.ToDecimal(txtAdvance.Text.Trim())),
                                                    If(txtNetAmount.Text.Trim() = "", 0, Convert.ToDecimal(txtNetAmount.Text.Trim())), txtbankDetail.Text, dtInvoiceDate.Text, txtCSTNo.Text, txtGSTNo.Text)

        If (res > 0) Then
            linq_obj.SubmitChanges()
            bindEnqGrid()
            Address_ID = 0
            ClearAll()
            MessageBox.Show("Successfully Saved..")
        Else
            MessageBox.Show("Error In Save")
        End If
    End Sub

    Private Sub btnchangePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchange.Click
        Dim res As Integer
        res = linq_obj.SP_Update_Tbl_InvoiceMaster(Address_ID, txtInvoiceNo.Text, txtTransporter.Text, txtRRLRNo.Text,
                                                   If(txtBasic.Text.Trim() = "", 0, Convert.ToDecimal(txtBasic.Text.Trim())), If(txtDiscount.Text.Trim() = "", 0, Convert.ToDecimal(txtDiscount.Text.Trim())), If(txtTotal.Text.Trim() = "", 0, Convert.ToDecimal(txtTotal.Text.Trim())),
                                                   If(txtCstPerc.Text.Trim() = "", 0, Convert.ToInt32(txtCstPerc.Text)), If(txtSubTotal.Text.Trim() = "", 0, Convert.ToDecimal(txtSubTotal.Text.Trim())), If(txtAdvance.Text.Trim() = "", 0, Convert.ToDecimal(txtAdvance.Text.Trim())),
                                                    If(txtNetAmount.Text.Trim() = "", 0, Convert.ToDecimal(txtNetAmount.Text.Trim())), txtbankDetail.Text, dtInvoiceDate.Text, txtCSTNo.Text, txtGSTNo.Text)

        If (res >= 0) Then
            linq_obj.SubmitChanges()
            Address_ID = 0
            ClearAll()
            MessageBox.Show("Successfully Updated")
            bindEnqGrid()
        Else
            MessageBox.Show("Not Updated Invoice Detail")
        End If
    End Sub
    

    Private Sub btnPOAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        ClearAll()
    End Sub

    Public Sub bindEnqGrid()
        Dim criteria As String
        criteria = " and "

        If txtParty.Text.Trim() <> "" Then
            criteria = criteria + " Name like '%" + txtParty.Text + "%' and"
        End If
        If txtCoperson.Text.Trim() <> "" Then
            criteria = criteria + " ContactPerson like '%" + txtCoperson.Text + "%' and"
        End If
        If txtstation.Text.Trim <> "" Then
            criteria = criteria + " City like '%" + txtstation.Text + "%' and"
        End If
        'If txtInqSearchLandLineNo.Text.Trim() <> "" Then
        '    criteria = criteria + " LandlineNo like '%" + txtInqSearchLandLineNo.Text + "%' and"
        'End If
        If txtContactNo.Text.Trim() <> "" Then
            criteria = criteria + " MobileNo like '%" + TextBox1.Text + "%' and"
        End If


        If txtSearchEnqNo.Text.Trim() <> "" Then
            criteria = criteria + " EnqNo like '%" + txtSearchEnqNo.Text + "%' and"
        End If

        If criteria = " and " Then
            criteria = ""
        End If

        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If
        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_AddressForPartyInvoiceDetail"
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
        Dim objclass As New Class1

        Dim dt As New DataTable
        dt.Columns.Add("Pk_AddressID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")
        Dim dtData As DataTable

        dtData = objclass.GetEnqOrderReportData(cmd)
        If dtData.Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            GDVInqDetail.DataSource = Nothing
            dtData.Dispose()
            dt.Dispose()
        Else
            For index = 0 To dtData.Rows.Count - 1
                dt.Rows.Add(dtData.Rows(index)(0), dtData(index)(1), dtData(index)(2))
            Next
            GDVInqDetail.DataSource = dt
            Me.GDVInqDetail.Columns(0).Visible = False
            dtData.Dispose()
            dt.Dispose()
        End If
        txtTotRI.Text = GDVInqDetail.RowCount
    End Sub

    
    Private Sub GDVInqDetail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GDVInqDetail.DoubleClick
        Address_ID = Me.GDVInqDetail.SelectedCells(0).Value()
        bindInvoiceData(Address_ID)
    End Sub

    Public Sub bindInvoiceData(ByVal Address_ID)
        Try
            Dim datainvoice = linq_obj.SP_Select_Tbl_InvoiceMaster(Convert.ToInt64(Address_ID)).ToList()

            If (datainvoice.Count > 0) Then
                btnSave.Enabled = False
                bindAllData(Address_ID)
                txtRRLRNo.Text = Convert.ToString(datainvoice(0).RRLRNo)
                txtAdvance.Text = Convert.ToString(datainvoice(0).Advance)
                txtCstPerc.Text = Convert.ToString(datainvoice(0).CSTPerc)
                txtNetAmount.Text = Convert.ToString(datainvoice(0).NetAmount)
                txtAdvance.Text = Convert.ToString(datainvoice(0).Advance)
                txtInvoiceNo.Text = Convert.ToString(datainvoice(0).InvoiceNo)
                txtTransporter.Text = Convert.ToString(datainvoice(0).Transporter)
                txtbankDetail.Text = Convert.ToString(datainvoice(0).BankDetail)
                dtInvoiceDate.Value = datainvoice(0).InvoiceDate.Value
                txtCSTAmount.Text = Convert.ToString((Convert.ToDecimal(If(txtTotal.Text = "", 0, txtTotal.Text)) * Convert.ToDecimal(If(txtCstPerc.Text = "", 0, txtCstPerc.Text))) / 100)
                txtSubTotal.Text = Convert.ToString((Convert.ToDecimal(If(txtTotal.Text = "", 0, txtTotal.Text)) + Convert.ToDecimal(If(txtCSTAmount.Text = "", 0, txtCSTAmount.Text))))
                txtNetAmount.Text = Convert.ToString((Convert.ToDecimal(If(txtSubTotal.Text = "", 0, txtSubTotal.Text)) - Convert.ToDecimal(If(txtAdvance.Text = "", 0, txtAdvance.Text))))
                txtRsWords.Text = RupeeConvert.changeToWords(If(txtNetAmount.Text = "", 0, txtNetAmount.Text), True)
                txtGSTNo.Text = Convert.ToString(datainvoice(0).GSTNo)
                txtCSTNo.Text = Convert.ToString(datainvoice(0).CSTNo)
            Else
                btnSave.Enabled = True

            End If
        Catch ex As Exception
            btnSave.Enabled = True

        End Try
    End Sub

    Public Sub bindAllData(ByVal address As Integer)
        Address_ID = Convert.ToInt32(address)
        GetClientDetails_Bind()
        bindPartyDetail()
        txtCSTAmount.Text = Convert.ToString((Convert.ToDecimal(If(txtTotal.Text = "", 0, txtTotal.Text)) * Convert.ToDecimal(If(txtCstPerc.Text = "", 0, txtCstPerc.Text))) / 100)
        txtSubTotal.Text = Convert.ToString((Convert.ToDecimal(If(txtTotal.Text = "", 0, txtTotal.Text)) + Convert.ToDecimal(If(txtCSTAmount.Text = "", 0, txtCSTAmount.Text))))
        txtNetAmount.Text = Convert.ToString((Convert.ToDecimal(If(txtSubTotal.Text = "", 0, txtSubTotal.Text)) - Convert.ToDecimal(If(txtAdvance.Text = "", 0, txtAdvance.Text))))
        txtRsWords.Text = RupeeConvert.changeToWords(If(txtNetAmount.Text = "", 0, txtNetAmount.Text), True)
    End Sub

    Public Function Datecheck(ByVal date1 As String) As String


        Dim finalDt = ""

        If (Convert.ToString(date1) <> "") Then

            If (date1.ToString().Contains("1900")) Then
                finalDt = ""
            Else
                finalDt = Convert.ToDateTime(date1).ToShortDateString()
            End If
        End If

        Return finalDt


    End Function
    Public Sub bindPartyDetail()
        Dim data = linq_obj.SP_Select_Party_Master_ByAddressId(Address_ID).ToList()
        If (data.Count > 0) Then
            txtPONo.Text = Convert.ToString(data(0).PONo)
            dtPODate.Text = Datecheck(data(0).OrderDate)

            'BIND A DEBIT DETAIL
            tblInvoiceDetail.Clear()
            PartyId = Convert.ToInt64(data(0).Pk_PartyId)

            Dim dataDetail = linq_obj.SP_Select_Party_DebitDetail_ByPartyId(data(0).Fk_AddressId).ToList()
            If (dataDetail.Count > 0) Then
                For Each item As SP_Select_Party_DebitDetail_ByPartyIdResult In dataDetail
                    Dim dr As DataRow
                    dr = tblInvoiceDetail.NewRow()
                    dr("No") = Convert.ToString(item.DebitEntryNo)
                    dr("Description") = Convert.ToString(item.PlantScheme)
                    dr("Rate") = Convert.ToString(item.Amount)
                    dr("Qty") = Convert.ToString(If(item.Qty Is Nothing, 1, item.Qty))
                    dr("Total") = Convert.ToString(Convert.ToDecimal(item.Amount) * Convert.ToInt32(If(item.Qty Is Nothing, 1, item.Qty)))
                    tblInvoiceDetail.Rows.Add(dr)

                Next
                dgInvoiceDetail.DataSource = tblInvoiceDetail
                txtBasic.Text = dataDetail(0).TotalDebit.Value.ToString()
                txtDiscount.Text = dataDetail(0).Discount.Value.ToString()
                txtTotal.Text = dataDetail(0).NetDebit.Value.ToString()
            End If
        End If


    End Sub

    Private Sub ClearAll()
        Address_ID = 0
        tblInvoiceDetail.Clear()
        dgInvoiceDetail.DataSource = tblInvoiceDetail
        txtAddress.Text = ""
        txtAdvance.Text = 0
        txtbankDetail.Text = ""
        txtBasic.Text = ""
        txtContactNo.Text = ""
        txtBuyerName.Text = ""
        txtContactPerson.Text = ""
        txtCSTAmount.Text = ""
        txtCstPerc.Text = 2
        txtCSTNo.Text = ""
        txtDiscount.Text = ""
        txtEntryNo.Text = ""
        txtGSTNo.Text = ""
        txtInvoiceNo.Text = ""
        txtNetAmount.Text = ""
        txtPONo.Text = ""
        txtRRLRNo.Text = ""
        txtRsWords.Text = ""
        txtSubTotal.Text = ""
        txtTotal.Text = ""
        txtTransporter.Text = ""
        dtInvoiceDate.Value = DateTime.Now
        dtPODate.Text = ""

    End Sub
   
    Public Sub GetClientDetails_Bind()
        Try
            Dim Claient = linq_obj.SP_Get_AddressListById(Address_ID).ToList()
            For Each item As SP_Get_AddressListByIdResult In Claient
                txtBuyerName.Text = item.Name
                txtAddress.Text = item.Address + "," + item.Area + "," + item.District + "," + item.City + "," + item.State + "-" + item.Pincode
                'txtAddress.Text += "Delivery Address -" + item.DeliveryAddress + "," + item.DeliveryArea + "," + item.DeliveryDistrict + "," + item.DeliveryCity + "," + item.DeliveryState + "," + item.DeliveryPincode
                txtContactPerson.Text = item.ContactPerson
                txtContactNo.Text = item.MobileNo
                txtEntryNo.Text = item.EnqNo
                'txtEntryNo.Text = item.EnqNo
            Next
            'Get Enquiry Client Master Details
        Catch ex As Exception
            MessageBox.Show("Error :" + ex.Message)
        End Try
    End Sub

    'Private Sub PartyInvoiceMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = 13 Then
    '        SendKeys.Send("{TAB}")
    '    End If
    '    If e.KeyCode = 27 Then
    '        Me.Close()
    '    End If
    'End Sub

    Private Sub PartyInvoiceMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'add columns in debitdata
        tblInvoiceDetail.Columns.Add("No")
        tblInvoiceDetail.Columns.Add("Description")
        tblInvoiceDetail.Columns.Add("Qty")
        tblInvoiceDetail.Columns.Add("Rate")
        tblInvoiceDetail.Columns.Add("Total")
        dgInvoiceDetail.DataSource = tblInvoiceDetail


        For Each control As Control In Me.Controls
            ' The textbox control.
            Dim parentControl As New Control
            If (control.GetType() Is GetType(GroupBox)) Then
                Dim grpBox As GroupBox = TryCast(control, GroupBox)
                parentControl = grpBox
            ElseIf (control.GetType() Is GetType(TabControl)) Then
                Dim TC As TabControl = TryCast(control, TabControl)
                parentControl = TC
            Else
                Try
                    parentControl = control
                Catch ex As Exception
                End Try

            End If

            For Each subcontrol As Control In parentControl.Controls
                If (subcontrol.GetType() Is GetType(TextBox)) Then
                    Dim textBox As TextBox = TryCast(subcontrol, TextBox)

                    ' If not null, set the handler.
                    If textBox IsNot Nothing Then
                        AddHandler textBox.Enter, AddressOf TextBox_Enter
                        AddHandler textBox.Leave, AddressOf TextBox_Leave
                    End If
                End If

                ' Set the handler.
            Next

            ' Set the handler.
        Next
    End Sub
    Private Sub TextBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)

        txt.BackColor = Color.LightYellow
    End Sub
    Private Sub TextBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)

        txt.BackColor = Color.White
    End Sub
    Private Sub txtCstPerc_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCstPerc.Leave
        txtCSTAmount.Text = Convert.ToString((Convert.ToDecimal(If(txtTotal.Text = "", 0, txtTotal.Text)) * Convert.ToDecimal(If(txtCstPerc.Text = "", 0, txtCstPerc.Text))) / 100)
        txtSubTotal.Text = Convert.ToString((Convert.ToDecimal(If(txtTotal.Text = "", 0, txtTotal.Text)) + Convert.ToDecimal(If(txtCSTAmount.Text = "", 0, txtCSTAmount.Text))))

        txtNetAmount.Text = Convert.ToString((Convert.ToDecimal(If(txtSubTotal.Text = "", 0, txtSubTotal.Text)) - Convert.ToDecimal(If(txtAdvance.Text = "", 0, txtAdvance.Text))))
    End Sub
    Private Sub txtTotal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotal.Leave
        txtCSTAmount.Text = Convert.ToString((Convert.ToDecimal(If(txtTotal.Text = "", 0, txtTotal.Text)) * Convert.ToDecimal(If(txtCstPerc.Text = "", 0, txtCstPerc.Text))) / 100)
        txtSubTotal.Text = Convert.ToString((Convert.ToDecimal(If(txtTotal.Text = "", 0, txtTotal.Text)) + Convert.ToDecimal(If(txtCSTAmount.Text = "", 0, txtCSTAmount.Text))))
        txtNetAmount.Text = Convert.ToString((Convert.ToDecimal(If(txtSubTotal.Text = "", 0, txtSubTotal.Text)) - Convert.ToDecimal(If(txtAdvance.Text = "", 0, txtAdvance.Text))))
        txtRsWords.Text = RupeeConvert.changeToWords(If(txtNetAmount.Text = "", 0, txtNetAmount.Text), True)
    End Sub
    Private Sub txtCstPerc_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCstPerc.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub
    Private Sub txtSubTotal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubTotal.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub
    Private Sub txtEntryNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntryNo.Leave

        Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEntryNo.Text).ToList()
        If (data.Count > 0) Then
            Address_ID = data(0).Pk_AddressID

            Dim dataParty = linq_obj.SP_Select_Party_Master_ByAddressId(Address_ID).ToList()

            If dataParty.Count <= 0 Then
                MessageBox.Show("Party Master not found data")
                ClearAll()
                Exit Sub
            End If

            bindAllData(Address_ID)
        End If
    End Sub

    Private Sub txtAdvance_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdvance.Leave
        txtNetAmount.Text = Convert.ToString((Convert.ToDecimal(If(txtSubTotal.Text = "", 0, txtSubTotal.Text)) - Convert.ToDecimal(If(txtAdvance.Text = "", 0, txtAdvance.Text))))
        txtRsWords.Text = RupeeConvert.changeToWords(If(txtNetAmount.Text = "", 0, txtNetAmount.Text), True)
    End Sub

    Private Sub txtAdvance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvance.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClearAll()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearAll()
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            linq_obj.SP_Delete_Tbl_InvoiceMaster(Address_ID)
            linq_obj.SubmitChanges()
            ClearAll()
            bindEnqGrid()
        End If
    End Sub

    Private Sub btnReportInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReportInvoice.Click
        If Address_ID > 0 Then
            Dim ds As New PerformaReport
            Dim dataParty = linq_obj.SP_Select_OrderParty_Detail(Address_ID).ToList()
            Dim ToWords As String

            If (dataParty.Count > 0) Then


                For Each item As SP_Select_OrderParty_DetailResult In dataParty
                    ToWords = "" & RupeeConvert.changeToWords(Convert.ToString(item.NetAmount), True)

                    ds.Tables("PartyMaster").Rows.Add(item.Name, item.Address, item.District, item.State, item.ContactPerson, item.MobileNo, item.CSTNo, item.GSTNo, item.InvoiceNo, item.InvoiceDate, item.Transporter,
                                                      item.PONo, item.CreateDate, item.RRLRNo, item.BasicAmount, item.SubTotal, item.Discount, item.Advance, item.Total, item.NetAmount, item.CSTPerc, item.BankDetail,
                                                     ToWords, item.OrderDate)
                Next
                Dim debitData = linq_obj.SP_Select_Party_DebitDetail_ByPartyId(dataParty(0).Fk_AddressId).ToList()
                For Each item As SP_Select_Party_DebitDetail_ByPartyIdResult In debitData
                    ds.Tables("PartyDebit").Rows.Add(item.DebitEntryNo, item.PlantScheme, item.Amount, If(item.Qty Is Nothing, 1, item.Qty), Convert.ToDecimal(item.Amount) * Convert.ToInt32(If(item.Qty Is Nothing, 1, item.Qty)), item.TotalDebit, item.NetDebit)
                Next

                ', Convert.ToString(item.Amount), Convert.ToString(item.Qty), Double.Parse(item.Amount) * Double.Parse(item.Qty), item.TotalDebit, item.NetDebit

                'Dim creditData = linq_obj.SP_Select_Party_CreditDetail_ByPartyId(dataParty(0).Pk_PartyId).ToList()
                'For Each item As SP_Select_Party_CreditDetail_ByPartyIdResult In creditData
                '    ds.Tables("PartyCredit").Rows.Add(Convert.ToString(item.EntryNo), Convert.ToString(item.PType), Convert.ToString(Convert.ToDateTime(item.PDate).ToShortDateString()), Convert.ToString(item.Amount))
                'Next


                Dim rpt As New Rpt_ProformaInvoice


                ' cryRpt.Load("D:\\ROERP\\PDFGENERATOR\\PDFGENERATOR\\Reports\\Rpt_PartyOutstanding.rpt")

                'cryRpt.SetDataSource(ds)

                rpt.Database.Tables("PartyMaster").SetDataSource(ds.Tables("PartyMaster"))
                rpt.Database.Tables("PartyDebit").SetDataSource(ds.Tables("PartyDebit"))
                'rpt.Database.Tables("PartyCredit").SetDataSource(ds.Tables("PartyCredit"))

                Dim frm As New FrmCommanReportView(rpt)
                frm.StartPosition = FormStartPosition.CenterScreen
                frm.Show()

                ds.Dispose()
                rpt.Refresh()
                ds.Tables("PartyMaster").Dispose()
            Else
                MessageBox.Show("No Data Found...")

            End If


        End If

        '  MessageBox.Show("Display A Reports")
    End Sub

    
    Private Sub GDVInqDetail_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GDVInqDetail.CellClick
        Try
            Address_ID = Me.GDVInqDetail.SelectedCells(0).Value()
            bindAllData(Address_ID)
            bindInvoiceData(Address_ID)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefRI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefRI.Click
        bindEnqGrid()
    End Sub

    Private Sub GroupBox6_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox6.Enter

    End Sub

    Private Sub txtBuyerName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuyerName.Leave
        If (txtBuyerName.Text <> "") Then
            Dim data = linq_obj.SP_Get_AddressListByNameForOrder(txtBuyerName.Text).ToList()
            If (data.Count > 0) Then
                Address_ID = data(0).Pk_AddressID
                bindAllData(Address_ID)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindEnqGrid()
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub txtRRLRNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRRLRNo.Leave
        txtBasic.Focus()
    End Sub

    

    Private Sub GDVInqDetail_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GDVInqDetail.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            Address_ID = Me.GDVInqDetail.SelectedCells(0).Value()
            bindInvoiceData(Address_ID)
            bindAllData(Address_ID)
        End If
    End Sub
End Class
