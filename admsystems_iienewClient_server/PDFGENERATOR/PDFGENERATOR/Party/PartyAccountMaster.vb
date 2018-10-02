Imports System.Data.SqlClient

Public Class PartyAccountMaster
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim PartyId As Integer
    Dim PartyDebitId As Integer
    Dim PartyCreditId As Integer
    Dim tblDebit As New DataTable
    Dim tblCredit As New DataTable
    Dim rwIDDelDebitDetail As Integer = -1
    Dim rwIDDelCreditDetail As Integer = -1
    Dim bNotClearObject As Boolean = False
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoComplete_Text()
        GvInEnq_Bind()
        txtDebitNo.Text = If(gvDebit.Rows.Count = 0, 1, gvDebit.Rows.Count + 1).ToString()
        txtCreditNo.Text = If(gvCredit.Rows.Count = 0, 1, gvCredit.Rows.Count + 1).ToString()
        PartyId = 0
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
            dataView.RowFilter = "([DetailName] like 'Party Master')"

            If (dataView.Count > 0) Then
                dv = dataView.ToTable()

                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            btnPOSave.Enabled = True
                        Else
                            btnPOSave.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsUpdate") = True) Then
                            btnchangePO.Enabled = True
                        Else
                            btnchangePO.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsDelete") = True) Then
                            btnDelPO.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsPrint") = True) Then
                            btnReportPrint_new.Enabled = True
                        Else
                            btnReportPrint_new.Enabled = False
                        End If

                    Next
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub AutoComplete_Text()
        Dim data = linq_obj.SP_Get_AddressListAutoCompleteForOrder("Name").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteForOrderResult In data
            txtPartyName.AutoCompleteCustomSource.Add(iteam.Result)
            txtParty.AutoCompleteCustomSource.Add(iteam.Result)
        Next
        Dim dataEnq = linq_obj.SP_Get_AddressListAutoCompleteForOrder("EnqNo").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteForOrderResult In dataEnq
            txtEntryNo.AutoCompleteCustomSource.Add(iteam.Result)
        Next
    End Sub

    Public Sub GvInEnq_Bind()
        bindEnqGrid()
        'Dim enq = linq_obj.SP_Get_AddressForOrder().ToList()
        'Dim dt As New DataTable
        'dt.Columns.Add("ID")
        'dt.Columns.Add("Name")
        'dt.Columns.Add("EnqNo")
        'For Each item As SP_Get_AddressForOrderResult In enq
        '    dt.Rows.Add(item.Pk_AddressID, item.Name, item.EnqNo)
        'Next
        'gvAddressList.DataSource = dt
        'Me.gvAddressList.Columns(0).Visible = False

    End Sub

    Public Sub bindAllData(ByVal address As Integer)
        ClearAll()
        Address_ID = Convert.ToInt32(address)
        GetClientDetails_Bind()
        tblDebit.Clear()
        tblCredit.Clear()
        Dim data = linq_obj.SP_Select_Party_Master_ByAddressId(Address_ID).ToList()
        If data.Count > 0 Then

            txtEntryNo.Text = Convert.ToString(data(0).EntryNo)
            dtODate.Text = Class1.Datecheck(data(0).OrderDate)
            txtPONo.Text = Convert.ToString(data(0).PONo)
            txtOType.Text = Convert.ToString(data(0).OType)
            'txtPlantName.Text = Convert.ToString(data(0).PlantName)
            'txtCapacity.Text = (Convert.ToString(data(0).Capacity))
            dtDispatchDate.Text = Class1.Datecheck(data(0).DispatchDate)
            txtExecutive.Text = Convert.ToString(data(0).ExecutiveName)
            txtOrderStatus.Text = Convert.ToString(data(0).OrderStatus)
            txtRemarks.Text = Convert.ToString(data(0).Remarks)
            txtBreakNo.Text = IIf(Convert.ToString(data(0).BreakSrNo) = 0, "", Convert.ToString(data(0).BreakSrNo))
            txtPDCRem.Text = Convert.ToString(data(0).PDCReminder)
            Dim debitData = linq_obj.SP_Select_Party_DebitDetail_ByPartyId(data(0).Fk_AddressId).ToList()
            For Each item As SP_Select_Party_DebitDetail_ByPartyIdResult In debitData
                Dim dr As DataRow
                dr = tblDebit.NewRow()
                dr("No") = item.DebitEntryNo
                dr("PlantName") = item.PlantScheme
                dr("Qty") = item.Qty
                dr("Amount") = item.Amount
                dr("TotAmount") = (item.Qty * item.Amount)
                tblDebit.Rows.Add(dr)
                txtDebitDiscount.Text = Convert.ToString(item.Discount)
                txtDebitNetDebit.Text = Convert.ToString(item.NetDebit)
                txtDebitTotal.Text = Convert.ToString(item.TotalDebit)
                PartyDebitId = item.Pk_PartyDebitId
            Next

            Dim party = linq_obj.SP_Select_Tbl_ProjectInformationMaster(Address_ID).ToList()

            If party.Count > 0 Then
                txtPlantName.Text = Convert.ToString(party(0).PlantName)
                txtCapacity.Text = Convert.ToString(party(0).Capacity)
            End If

            gvDebit.DataSource = tblDebit
            txtDebitNo.Text = If(gvDebit.Rows.Count = 0, 1, gvDebit.Rows.Count + 1).ToString()

            Dim creditData = linq_obj.SP_Select_Party_CreditDetail_ByPartyId(data(0).Fk_AddressId).ToList()
            For Each item As SP_Select_Party_CreditDetail_ByPartyIdResult In creditData
                Dim dr As DataRow
                dr = tblCredit.NewRow()
                dr("No") = item.EntryNo
                dr("PType") = item.PType
                dr("PDate") = item.PDate
                dr("Amount") = item.Amount
                tblCredit.Rows.Add(dr)
                txtCreditAdvance.Text = Convert.ToString(item.Advance)
                txtCreditOutstanding.Text = Convert.ToString(item.Outstanding)
                txtCreditTotal.Text = Convert.ToString(item.TotalCredit)
                txtCreditKasar.Text = Convert.ToString(item.Kasar)
                PartyCreditId = item.Pk_PartyCreditId
            Next
            gvCredit.DataSource = tblCredit
            txtCreditNo.Text = If(gvCredit.Rows.Count = 0, 1, gvCredit.Rows.Count + 1).ToString()
            btnPOSave.Enabled = False


            ''CFORM Details
            Dim CForm = linq_obj.SP_Select_Tbl_PartyCFormDetail(Address_ID).ToList()
            For Each item As SP_Select_Tbl_PartyCFormDetailResult In CForm
                dtCFormRecDate.Text = item.CFormRecDate
                txtCFormNo.Text = item.CFormNo
                txtCFormRemarks.Text = item.CFormRemarks
                If (item.CFormRequired = 0) Then
                    RBCFormYes.Checked = True
                Else
                    RBCFormNo.Checked = True
                End If


                If (item.CFormStatus = 0) Then
                    RBStatusPending.Checked = True
                Else
                    RBStatusReceived.Checked = True
                End If
            Next

            ''PDC Detail


            Dim PDC = linq_obj.SP_Select_Tbl_PartyPDCDetail(Address_ID).ToList()
            For Each item As SP_Select_Tbl_PartyPDCDetailResult In PDC
                dtChequeDate.Text = item.PDCDate
                txtChequeAmount.Text = item.Amount
                If (item.Status = 0) Then

                    RBClear.Checked = True
                Else
                    RBUnClear.Checked = True

                End If



            Next

        Else
            GetClientDetails_Bind()
            PartyId = 0
            btnPOSave.Enabled = True

        End If
    End Sub
#Region "Event"

    Private Sub gvAddressList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvAddressList.DoubleClick
        Try
            Address_ID = Me.gvAddressList.SelectedCells(0).Value
            bindAllData(Address_ID)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnDebitDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDebitDelete.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then
            gvDebit.Rows.RemoveAt(gvDebit.CurrentRow.Index)
            SetSrno(gvDebit, 0)
            tblDebit = gvDebit.DataSource
            txtDebitTotal.Text = getDebitTotal()
            txtDebitNo.Text = ""
        End If

    End Sub
    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn.Click
        txtDebitNo.Text = ""
        txtDebitAmount.Text = ""
        txtDebitDiscount.Text = ""
        txtDebitPlantName.Text = ""
        rwIDDelCreditDetail = -1
        txtQty.Text = ""

        txtDebitNo.Text = If(gvDebit.Rows.Count = 0, 1, gvDebit.Rows.Count + 1).ToString()
        txtDebitPlantName.Focus()
    End Sub

    'Private Sub PartyAccountMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = 13 Then
    '        SendKeys.Send("{TAB}")
    '    End If
    '    If e.KeyCode = 27 Then
    '        Me.Close()
    '    End If
    'End Sub
    Private Sub PartyAccountMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        ''add column into creditData

        tblCredit.Columns.Add("No")
        tblCredit.Columns.Add("PType")
        tblCredit.Columns.Add("PDate")
        tblCredit.Columns.Add("Amount")
        gvCredit.DataSource = tblCredit

        'add columns in debitdata

        tblDebit.Columns.Add("No")
        tblDebit.Columns.Add("PlantName")
        tblDebit.Columns.Add("Qty")
        tblDebit.Columns.Add("Amount")
        tblDebit.Columns.Add("TotAmount")

        gvDebit.DataSource = tblDebit
        txtPartyName.Enabled = False
        For Each control As Control In Me.Controls
            ' The textbox control.
            Dim parentControl As New Control




            If (control.GetType() Is GetType(GroupBox)) Then
                Dim grpBox As GroupBox = TryCast(control, GroupBox)

                parentControl = grpBox
                For Each subcontrol As Control In parentControl.Controls
                    If (control.GetType() Is GetType(GroupBox)) Then

                    End If
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
            ElseIf (control.GetType() Is GetType(TabControl)) Then
                Dim TC As TabControl = TryCast(control, TabControl)
                parentControl = TC
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
            ElseIf (control.GetType() Is GetType(Panel)) Then
                Dim PC As TabControl = TryCast(control, TabControl)
                parentControl = PC
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
            Else
                Try
                    parentControl = control
                Catch ex As Exception

                End Try

            End If

            'For Each subcontrol As Control In parentControl.Controls
            '    If (subcontrol.GetType() Is GetType(TextBox)) Then
            '        Dim textBox As TextBox = TryCast(subcontrol, TextBox)

            '        ' If not null, set the handler.
            '        If textBox IsNot Nothing Then
            '            AddHandler textBox.Enter, AddressOf TextBox_Enter
            '            AddHandler textBox.Leave, AddressOf TextBox_Leave
            '        End If
            '    End If

            '    ' Set the handler.
            'Next

            ' Set the handler.
        Next

    End Sub
    Private Sub btnDebitSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDebitSave.Click
        ''add new row runtime and display into grid. it will save after click on save button


        If (txtDebitPlantName.Text.Trim() = "") Then

            MessageBox.Show("Debit Plan Name Cannot be Blank..")
            Return

        ElseIf (txtQty.Text.Trim() = "") Then
            MessageBox.Show("Debit Qty Cannot be Blank..")
            Return

        ElseIf (txtDebitAmount.Text.Trim() = "") Then
            MessageBox.Show("Debit Amount Cannot be Blank..")
            Return
        Else

            Dim dr As DataRow
            dr = tblDebit.NewRow()
            dr("No") = txtDebitNo.Text
            dr("PlantName") = txtDebitPlantName.Text
            dr("Amount") = txtDebitAmount.Text
            dr("Qty") = txtQty.Text
            dr("TotAmount") = (txtQty.Text * txtDebitAmount.Text)

            If (rwIDDelDebitDetail >= 0) Then
                ''gvDebit.Rows.RemoveAt(rwIDDelDebitDetail)
                gvDebit.Rows(rwIDDelDebitDetail).Cells(0).Value = dr("No")
                gvDebit.Rows(rwIDDelDebitDetail).Cells(1).Value = dr("PlantName")
                gvDebit.Rows(rwIDDelDebitDetail).Cells(2).Value = dr("Qty")
                gvDebit.Rows(rwIDDelDebitDetail).Cells(3).Value = dr("Amount")
                gvDebit.Rows(rwIDDelDebitDetail).Cells(4).Value = dr("TotAmount")
                tblDebit = gvDebit.DataSource
                'tblDebit.Rows.Add(dr)
            Else
                tblDebit.Rows.Add(dr)
            End If

            gvDebit.DataSource = tblDebit
            txtDebitTotal.Text = getDebitTotal.ToString()

            txtDebitNo.Text = If(gvDebit.Rows.Count = 0, 1, gvDebit.Rows.Count + 1).ToString()

            'Debit SrNo Auto

            Dim i As Integer = 1
            For Each row As DataGridViewRow In gvDebit.Rows
                row.Cells("No").Value = i
                i += 1
            Next

            txtQty.Text = ""
            txtDebitAmount.Text = ""
            txtDebitPlantName.Text = ""
            btn.Focus()
            rwIDDelDebitDetail = -1
        End If
    End Sub

    Private Sub btnCreditSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreditSave.Click
        ''add new row runtime and display into grid. it will save after click on save button

        If (txtCreditPType.Text.Trim() = "") Then

            MessageBox.Show("P.Type Cannot be Blank...")
            Return
        ElseIf (txtCreditAmount.Text.Trim() = "") Then
            MessageBox.Show("Credit Amount Cannot be Blank...")
            Return
        Else

            Dim dr As DataRow
            dr = tblCredit.NewRow()
            dr("No") = txtCreditNo.Text
            dr("PType") = txtCreditPType.Text
            dr("PDate") = dtCreditPDate.Value.Date
            dr("Amount") = txtCreditAmount.Text
            If (rwIDDelCreditDetail >= 0) Then

                ''gvCredit.Rows.RemoveAt(rwIDDelCreditDetail)

                gvCredit.Rows(rwIDDelCreditDetail).Cells(0).Value = dr("No")
                gvCredit.Rows(rwIDDelCreditDetail).Cells(1).Value = dr("PType")
                gvCredit.Rows(rwIDDelCreditDetail).Cells(2).Value = dr("PDate")
                gvCredit.Rows(rwIDDelCreditDetail).Cells(3).Value = dr("Amount")

                tblCredit = gvCredit.DataSource

                'tblCredit.Rows.Add(dr)
            Else
                tblCredit.Rows.Add(dr)
            End If

            gvCredit.DataSource = tblCredit
            txtCreditNo.Text = If(gvCredit.Rows.Count = 0, 1, gvCredit.Rows.Count + 1).ToString()


            txtCreditTotal.Text = getCreditTotal()
            'Credit SrNo Auto

            Dim i As Integer = 1
            For Each row As DataGridViewRow In gvDebit.Rows
                row.Cells("No").Value = i
                i += 1
            Next

            dtCreditPDate.Value = DateTime.Now
            txtCreditAmount.Text = ""
            'txtCreditNo.Text = ""
            txtCreditPType.Text = ""
            rwIDDelDebitDetail = -1
            btnCreditAdd.Focus()
        End If
    End Sub

    Private Sub btnCreditAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreditAdd.Click
        txtCreditAmount.Text = ""
        txtCreditNo.Text = ""
        txtCreditPType.Text = ""
        txtCreditNo.Text = If(gvCredit.Rows.Count = 0, 1, gvCredit.Rows.Count + 1).ToString()
        txtCreditPType.Focus()
        rwIDDelCreditDetail = -1
    End Sub

    Private Sub dtODate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtODate.KeyPress, dtDispatchDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub txtCreditAdvance_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCreditAdvance.Leave
        SetCreditOutStn()
    End Sub
    Private Sub dtODate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtODate.Validating, dtDispatchDate.Validating
        If Class1.ChkVaildDate(sender.Text) = False Then
            MessageBox.Show("Date formate is not valid")
            e.Cancel = True
        End If
    End Sub
    Private Sub btnPOAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPOAdd.Click
        ClearAll()
    End Sub

    Private Sub btnPOSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPOSave.Click
        Dim sErr As String = ""
        If txtAddress.Text.Trim() = "" Then
            Label39.Text = "Error... "
            ''ErrorProvider1.SetError(txtAddress, Label39.Text)
            ' Return
        End If
        If (Address_ID > 0) Then
            Dim result As Integer

            result = linq_obj.SP_Insert_Party_Master(txtEntryNo.Text, Convert.ToDateTime(If(dtODate.Text = "", "01-01-1900", dtODate.Text)), txtPONo.Text, txtOType.Text, Address_ID, txtPlantName.Text, txtCapacity.Text,
                                                     Convert.ToDateTime(If(dtDispatchDate.Text = "", "01-01-1900", dtDispatchDate.Text)), txtExecutive.Text, txtOrderStatus.Text, txtRemarks.Text, If(txtBreakNo.Text.Trim() = "", 0, Convert.ToDecimal(txtBreakNo.Text)), txtPDCRem.Text)

            If result = -1 Then
                MessageBox.Show("This Enqry no have no Order")
                Exit Sub
            End If

            If result > 0 Then
                linq_obj.SubmitChanges()
                'Update Contact Detail
                Dim resContact As Integer
                resContact = linq_obj.SP_UpdateAddressContactDetailProject(txtContactPerson.Text, txtContact.Text, txtEmailAddress.Text, Address_ID)
                linq_obj.SubmitChanges()

                'bind Address Detail
                Dim resAddress As Integer
                resAddress = linq_obj.SP_UpdateAddressProject(txtPartyName.Text, txtAddress.Text, txtStationName.Text, txtState.Text, Address_ID)
                If resAddress >= 0 Then
                    linq_obj.SubmitChanges()
                End If
                Dim resDebit As Integer

                resDebit = linq_obj.SP_Insert_Party_Debit(Address_ID, If(txtDebitTotal.Text.Trim() = "", 0, Convert.ToDecimal(txtDebitTotal.Text.Trim())), If(txtDebitDiscount.Text.Trim() = "", 0, Convert.ToDecimal(txtDebitDiscount.Text.Trim())), If(txtDebitNetDebit.Text.Trim() = "", 0, Convert.ToDecimal(txtDebitNetDebit.Text.Trim())))

                If resDebit > 0 Then
                    linq_obj.SubmitChanges()
                    Dim resdetail As Integer
                    For index = 0 To tblDebit.Rows.Count - 1
                        resdetail = linq_obj.SP_Insert_Party_DebitDetail(Address_ID, If(tblDebit.Rows(index)("No").ToString() = "", 0, Convert.ToInt32(tblDebit.Rows(index)("No").ToString())), tblDebit.Rows(index)("PlantName").ToString(), If(tblDebit.Rows(index)("Qty").ToString() = "", 0, Convert.ToInt32(tblDebit.Rows(index)("Qty").ToString())), If(tblDebit.Rows(index)("Amount").ToString() = "", 0, Convert.ToDecimal(tblDebit.Rows(index)("Amount").ToString())))
                        If (resdetail > 0) Then
                            linq_obj.SubmitChanges()
                        End If
                    Next
                End If

                Dim resCredit As Integer
                resCredit = linq_obj.SP_Insert_Party_Credit(Address_ID, If(txtCreditTotal.Text.Trim() = "", 0, Convert.ToDecimal(txtCreditTotal.Text.Trim())), If(txtCreditKasar.Text.Trim() = "", 0, Convert.ToDecimal(txtCreditKasar.Text.Trim())), If(txtCreditOutstanding.Text.Trim() = "", 0, Convert.ToDecimal(txtCreditOutstanding.Text.Trim())), If(txtCreditAdvance.Text.Trim() = "", 0, Convert.ToDecimal(txtCreditAdvance.Text.Trim())))
                If resCredit > 0 Then
                    linq_obj.SubmitChanges()
                    Dim resdetail As Integer
                    For index = 0 To tblCredit.Rows.Count - 1
                        resdetail = linq_obj.SP_Insert_Party_CreditDetail(Address_ID, If(tblCredit.Rows(index)("No").ToString() = "", 0, Convert.ToInt32(tblCredit.Rows(index)("No").ToString())), tblCredit.Rows(index)("PType").ToString(), Convert.ToDateTime(tblCredit.Rows(index)("PDate").ToString()), If(tblCredit.Rows(index)("Amount").ToString() = "", 0, Convert.ToDecimal(tblCredit.Rows(index)("Amount").ToString())))
                        If (resdetail > 0) Then
                            linq_obj.SubmitChanges()
                        End If
                    Next
                End If
                MessageBox.Show("Successfully Saved")
                ClearAll()
                Address_ID = 0
            Else
                MessageBox.Show("Error In Save")
            End If
        Else

        End If
        bindEnqGrid()
    End Sub

    Private Sub txtDebitDiscount_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDebitDiscount.Leave
        txtDebitNetDebit.Text = (If(txtDebitTotal.Text.Trim() = "", 0, Convert.ToDecimal(txtDebitTotal.Text.Trim())) - If(txtDebitDiscount.Text.Trim() = "", 0, Convert.ToDecimal(txtDebitDiscount.Text))).ToString()
    End Sub
    Private Sub txtCreditKasar_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCreditKasar.Leave
        SetCreditOutStn()
    End Sub

    Private Sub btnchangePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchangePO.Click
        If txtAddress.Text.Trim() = "" Then
            Label39.Text = "Error... "
            ''ErrorProvider1.SetError(txtAddress, Label39.Text)
            ' Return
        End If
        If (Address_ID > 0) Then


            Dim result As Integer

            result = linq_obj.SP_Update_Party_Master(txtEntryNo.Text, Convert.ToDateTime(If(dtODate.Text = "", "01-01-1900", dtODate.Text)), txtPONo.Text, txtOType.Text, Address_ID, txtPlantName.Text, txtCapacity.Text,
                                                     Class1.SetDate(dtDispatchDate.Text), txtExecutive.Text, txtOrderStatus.Text, txtRemarks.Text, Convert.ToDecimal(IIf(txtBreakNo.Text = "", 0, txtBreakNo.Text)), txtPDCRem.Text, PartyId)

            If result >= 0 Then
                linq_obj.SubmitChanges()
                Dim resdeleteDebit As Integer
                resdeleteDebit = linq_obj.SP_Delete_Party_Debit(Address_ID)
                resdeleteDebit = linq_obj.SP_Delete_Party_DebitDetail(Address_ID)
                linq_obj.SubmitChanges()
                'Update Contact Detail
                Dim resContact As Integer
                resContact = linq_obj.SP_UpdateAddressContactDetailProject(txtContactPerson.Text, txtContact.Text, txtEmailAddress.Text, Address_ID)
                linq_obj.SubmitChanges()

                'bind Address Detail
                Dim resAddress As Integer
                resAddress = linq_obj.SP_UpdateAddressProject(txtPartyName.Text, txtAddress.Text, txtStationName.Text, txtState.Text, Address_ID)
                If resAddress >= 0 Then
                    linq_obj.SubmitChanges()
                End If
                Dim resDebit As Integer

                resDebit = linq_obj.SP_Insert_Party_Debit(Address_ID, If(txtDebitTotal.Text.Trim() = "", 0, Convert.ToDecimal(txtDebitTotal.Text.Trim())), If(txtDebitDiscount.Text.Trim() = "", 0, Convert.ToDecimal(txtDebitDiscount.Text.Trim())), If(txtDebitNetDebit.Text.Trim() = "", 0, Convert.ToDecimal(txtDebitNetDebit.Text.Trim())))

                If resDebit >= 0 Then
                    linq_obj.SubmitChanges()
                    Dim resdetail As Integer
                    For index = 0 To tblDebit.Rows.Count - 1
                        resdetail = linq_obj.SP_Insert_Party_DebitDetail(Address_ID, If(tblDebit.Rows(index)("No").ToString() = "", 0, Convert.ToInt32(tblDebit.Rows(index)("No").ToString())), tblDebit.Rows(index)("PlantName").ToString(), If(tblDebit.Rows(index)("Qty").ToString() = "", 0, Convert.ToInt32(tblDebit.Rows(index)("Qty").ToString())), If(tblDebit.Rows(index)("Amount").ToString() = "", 0, Convert.ToDecimal(tblDebit.Rows(index)("Amount").ToString())))
                        If (resdetail > 0) Then
                            linq_obj.SubmitChanges()
                        End If
                    Next
                End If
                Dim resdeletecredit As Integer
                resdeletecredit = linq_obj.SP_Delete_Party_Credit(Address_ID)
                resdeletecredit = linq_obj.SP_Delete_Party_CreditDetail(Address_ID)

                linq_obj.SubmitChanges()
                Dim resCredit As Integer
                resCredit = linq_obj.SP_Insert_Party_Credit(Address_ID, If(txtCreditTotal.Text.Trim() = "", 0, Convert.ToDecimal(txtCreditTotal.Text.Trim())), If(txtCreditKasar.Text.Trim() = "", 0, Convert.ToDecimal(txtCreditKasar.Text.Trim())), If(txtCreditOutstanding.Text.Trim() = "", 0, Convert.ToDecimal(txtCreditOutstanding.Text.Trim())), If(txtCreditAdvance.Text.Trim() = "", 0, Convert.ToDecimal(txtCreditAdvance.Text.Trim())))
                If resCredit > 0 Then
                    linq_obj.SubmitChanges()
                    Dim resdetail As Integer
                    For index = 0 To tblCredit.Rows.Count - 1
                        resdetail = linq_obj.SP_Insert_Party_CreditDetail(Address_ID, If(tblCredit.Rows(index)("No").ToString() = "", 0, Convert.ToInt32(tblCredit.Rows(index)("No").ToString())), tblCredit.Rows(index)("PType").ToString(), Convert.ToDateTime(tblCredit.Rows(index)("PDate").ToString()), If(tblCredit.Rows(index)("Amount").ToString() = "", 0, Convert.ToDecimal(tblCredit.Rows(index)("Amount").ToString())))
                        If (resdetail > 0) Then
                            linq_obj.SubmitChanges()
                        End If
                    Next
                End If
                MessageBox.Show("Successfully Updated")
                ClearAll()
                Address_ID = 0
            Else
                MessageBox.Show("Error In Update")
            End If

        Else
            MessageBox.Show("Error : No Data Found For Update")

        End If

    End Sub

#End Region

#Region "Function"
    Public Function getDebitTotal() As Decimal
        Dim total As Decimal = 0
        For i = 0 To gvDebit.Rows.Count - 1
            total += (Convert.ToDecimal(gvDebit.Rows(i).Cells("Amount").Value) * Convert.ToDecimal(gvDebit.Rows(i).Cells("Qty").Value))
        Next
        txtDebitNetDebit.Text = (total - If(txtDebitDiscount.Text.Trim() = "", 0, Convert.ToDecimal(txtDebitDiscount.Text))).ToString()

        Return total
    End Function

    Public Function getCreditTotal() As Decimal
        Dim total As Decimal = 0
        For i = 0 To gvCredit.Rows.Count - 1
            total += Convert.ToDecimal(gvCredit.Rows(i).Cells("Amount").Value)
        Next
        txtCreditOutstanding.Text = (total - If(txtCreditKasar.Text.Trim() = "", 0, Convert.ToDecimal(txtCreditKasar.Text))).ToString()

        Return total
    End Function
    Function validateForm() As Boolean
        Return True
    End Function

#End Region
#Region "Procedure"
    Public Sub GetClientDetails_Bind()
        Try
            Dim Claient = linq_obj.SP_Get_AddressListById(Address_ID).ToList()
            For Each item As SP_Get_AddressListByIdResult In Claient
                txtPartyName.Text = item.Name
                txtAddress.Text = item.Address
                txtStationName.Text = item.City
                txtState.Text = item.State
                txtContactPerson.Text = item.ContactPerson
                txtContact.Text = item.MobileNo
                txtEmailAddress.Text = item.EmailID
                txtEntryNo.Text = item.EnqNo
            Next
            'Get Enquiry Client Master Details
        Catch ex As Exception
            MessageBox.Show("Error :" + ex.Message)
        End Try
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
#End Region

    
    

   

   


    
    
   
    
    Private Sub ClearAll()
        txtDebitNo.Text = ""
        txtQty.Text = ""

        txtDebitAmount.Text = ""
        txtDebitTotal.Text = "0"
        txtDebitDiscount.Text = "0"
        txtDebitNetDebit.Text = "0"
        txtDebitPlantName.Text = ""
        txtDisStatus.Text = ""

        txtQty.Text = ""
        txtCreditNo.Text = ""
        txtCreditPType.Text = ""
        txtCreditAdvance.Text = ""
        txtCreditKasar.Text = ""
        txtCreditOutstanding.Text = ""
        txtCreditTotal.Text = ""
        txtPartyName.Text = ""
        txtAddress.Text = ""
        txtStationName.Text = ""
        txtState.Text = ""
        txtContactPerson.Text = ""
        txtContact.Text = ""
        txtEmailAddress.Text = ""
        txtEntryNo.Text = ""
        txtEntryNo.Text = ""
        txtPlantName.Text = ""
        txtCapacity.Text = ""
        dtDispatchDate.Text = ""
        dtODate.Text = ""
        txtPONo.Text = ""
        txtOType.Text = ""
        txtExecutive.Text = ""
        txtOrderStatus.Text = ""
        txtRemarks.Text = ""
        txtBreakNo.Text = ""
        txtPDCRem.Text = ""
        tblCredit.Clear()
        tblDebit.Clear()
        PartyId = 0
        btnPOSave.Enabled = True

        Address_ID = 0
    End Sub

    Private Sub btnCancelPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearAll()
    End Sub

    Private Sub btnDelPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelPO.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then

            linq_obj.SP_Delete_Party_Debit(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Delete_Party_DebitDetail(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Delete_Party_Credit(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Delete_Party_CreditDetail(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Delete_Tbl_PartyCFormDetail(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Delete_Tbl_PartyPDCDetail(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Delete_Party_Master(Address_ID)
            linq_obj.SubmitChanges()

            MessageBox.Show("Successfully Deleted")
            ClearAll()
            Address_ID = 0
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindEnqGrid()

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
        If txtcontactNo.Text.Trim() <> "" Then
            criteria = criteria + " MobileNo like '%" + txtcontactNo.Text + "%' and"
        End If
        If txtEmail.Text.Trim() <> "" Then
            criteria = criteria + " EmialID like '%" + txtEmail.Text + "%' and"
        End If
        If txtDisStatus.Text.Trim() <> "" Then
            criteria = criteria + " TypeOfEnq like '" + txtDisStatus.Text + "' and"
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
        cmd.CommandText = "SP_Get_AddressForPartyDetail"
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
        Dim objclass As New Class1

        Dim dt As New DataTable
        dt.Columns.Add("Pk_AddressID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")
        Dim dtData As DataTable

        dtData = objclass.GetEnqOrderReportData(cmd)
        If dtData.Rows.Count < 1 Then
            ''MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            gvAddressList.DataSource = Nothing
            dtData.Dispose()
            dt.Dispose()
        Else
            For index = 0 To dtData.Rows.Count - 1
                dt.Rows.Add(dtData.Rows(index)(0), dtData(index)(1), dtData(index)(2))
            Next
            gvAddressList.DataSource = dt
            Me.gvAddressList.Columns(0).Visible = False
            dtData.Dispose()
            dt.Dispose()
        End If
        txtTotOrders.Text = gvAddressList.RowCount
    End Sub
    
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ''CFORM Details

        Dim CForm = linq_obj.SP_Select_Tbl_PartyCFormDetail(Address_ID).ToList()
        For Each item As SP_Select_Tbl_PartyCFormDetailResult In CForm
            dtCFormRecDate.Text = item.CFormRecDate
            txtCFormNo.Text = item.CFormNo
            txtCFormRemarks.Text = item.CFormRemarks
            If (item.CFormRequired = 0) Then
                RBCFormYes.Checked = True
            Else
                RBCFormNo.Checked = True
            End If


            If (item.CFormStatus = 0) Then
                RBStatusPending.Checked = True
            Else
                RBStatusReceived.Checked = True
            End If
        Next
        grpCFORM.BringToFront()
        grpCFORM.Visible = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim PDC = linq_obj.SP_Select_Tbl_PartyPDCDetail(Address_ID).ToList()
        For Each item As SP_Select_Tbl_PartyPDCDetailResult In PDC
            dtChequeDate.Text = item.PDCDate
            txtChequeAmount.Text = item.Amount

            If (item.Status = True) Then
                RBClear.Checked = True
            Else
                RBUnClear.Checked = True
            End If

        Next
        grpPDC.BringToFront()
        grpPDC.Visible = True
    End Sub

    Public Sub clearCForm()
        txtCFormNo.Text = ""
        txtCFormRemarks.Text = ""
        dtCFormRecDate.Text = ""
    End Sub

    Private Sub btnCFormOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCFormOk.Click
        If (Address_ID > 0) Then
            Try
                linq_obj.SP_Delete_Tbl_PartyCFormDetail(Address_ID)
                linq_obj.SubmitChanges()

                If (dtCFormRecDate.Text.Trim() = "") Then
                    MessageBox.Show("Confirm Rec Date Can Not Be Blank..")
                    Return
                ElseIf (txtCFormNo.Text.Trim() = "") Then
                    MessageBox.Show("Confirm Form Can Not Be Blank..")
                    Return

                Else
                    linq_obj.SP_Insert_Tbl_PartyCFormDetail(Address_ID, IIf(RBCFormYes.Checked, True, False), IIf(RBStatusReceived.Checked, True, False), Convert.ToDateTime(If(dtCFormRecDate.Text = "", "01-01-1900", dtCFormRecDate.Text)), txtCFormNo.Text, txtCFormRemarks.Text)
                    linq_obj.SubmitChanges()
                    clearCForm()
                    grpCFORM.Visible = False
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If



    End Sub

    Public Sub clearPDC()
        txtPDCRem.Text = ""
        dtChequeDate.Text = ""
        txtChequeAmount.Text = ""

    End Sub

    Private Sub btnPDCOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDCOk.Click
        If (Address_ID > 0) Then
            Try
                linq_obj.SP_Delete_Tbl_PartyPDCDetail(Address_ID)
                linq_obj.SubmitChanges()

                If (dtChequeDate.Text.Trim() = "") Then
                    MessageBox.Show("PDC Date Can Not Be Blank..")
                    Return
                ElseIf (txtChequeAmount.Text.Trim() = "") Then
                    MessageBox.Show("PDC Amount Can Not Be Blank..")
                    Return
                Else

                    linq_obj.SP_Insert_Tbl_PartyPDCDetail(Address_ID, Convert.ToDateTime(If(dtChequeDate.Text = "", "01-01-1900", dtChequeDate.Text)), Convert.ToDecimal(txtChequeAmount.Text), IIf(RBClear.Checked, True, False))
                    linq_obj.SubmitChanges()
                    clearPDC()
                    grpPDC.Visible = False
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnCFromClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCFromClear.Click
        clearCForm()
        grpCFORM.Visible = False
    End Sub

    Private Sub btnPDCClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDCClear.Click
        clearPDC()
        grpPDC.Visible = False
    End Sub

    Private Sub txtPartyName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub txtPartyName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (txtPartyName.Text <> "") Then
            Dim data = linq_obj.SP_Get_AddressListByNameForOrder(txtPartyName.Text).ToList()
            If (data.Count > 0) Then
                bNotClearObject = True
                Address_ID = data(0).Pk_AddressID
                bindAllData(Address_ID)
            End If
        End If

    End Sub

    Private Sub btnReportPrint_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReportPrint_new.Click
        If Address_ID > 0 Then
           
            Dim ds As New PartyDataset

            'Get Delivery Details


            Dim Disp_Details = linq_obj.SP_Get_AddressListById(Address_ID).ToList()

            For Each item As SP_Get_AddressListByIdResult In Disp_Details
                ds.Tables("Party_Dispatch").Rows.Add(item.DeliveryAddress, item.DeliveryArea, item.DeliveryCity, item.DeliveryPincode, item.DeliveryTaluka, item.DeliveryDistrict, item.DeliveryState)
            Next


            Dim dataParty = linq_obj.SP_Select_Party_Detail(Address_ID).ToList()
            Dim Breaks As Integer
            For Each item As SP_Select_Party_DetailResult In dataParty
                ds.Tables("PartyMaster").Rows.Add(Convert.ToString(Convert.ToDateTime(item.OrderDate).ToShortDateString()), Convert.ToString(item.PONo), Convert.ToString(item.OType), Convert.ToString(item.PlantName), Convert.ToString(item.Capacity), Convert.ToString(Convert.ToDateTime(item.DispatchDate).ToShortDateString()), Convert.ToString(item.OrderStatus), Convert.ToString(item.TotalDebit), Convert.ToString(item.Discount), Convert.ToString(item.NetDebit), Convert.ToString(item.TotalCredit), Convert.ToString(item.Kasar), Convert.ToString(item.Outstanding), Convert.ToString(item.Advance), Convert.ToString(item.Name), Convert.ToString(item.Address), Convert.ToString(item.District), Convert.ToString(item.State), Convert.ToString(item.ContactPerson), Convert.ToString(item.MobileNo), Convert.ToString(item.Remarks), Convert.ToString(item.EmailID), Convert.ToString(item.Area))
                Breaks = item.BreakSrNo

            Next
            Dim debitData = linq_obj.SP_Select_Party_DebitDetail_ByPartyId(dataParty(0).Fk_AddressId).ToList()
            For Each item As SP_Select_Party_DebitDetail_ByPartyIdResult In debitData
                ds.Tables("PartyDebit").Rows.Add(Convert.ToString(item.DebitEntryNo), Convert.ToString(item.PlantScheme), Convert.ToString(item.Amount * item.Qty), Breaks)
            Next



            Dim creditData = linq_obj.SP_Select_Party_CreditDetail_ByPartyId(dataParty(0).Fk_AddressId).ToList()
            For Each item As SP_Select_Party_CreditDetail_ByPartyIdResult In creditData
                ds.Tables("PartyCredit").Rows.Add(Convert.ToString(item.EntryNo), Convert.ToString(item.PType), Convert.ToString(Convert.ToDateTime(item.PDate).ToShortDateString()), Convert.ToString(item.Amount))
            Next

            Dim rpt As New Rpt_PartyOutstanding_New

            ' cryRpt.Load("D:\\ROERP\\PDFGENERATOR\\PDFGENERATOR\\Reports\\Rpt_PartyOutstanding.rpt")
            'cryRpt.SetDataSource(ds)

            rpt.Database.Tables("Party_Dispatch").SetDataSource(ds.Tables("Party_Dispatch"))
            rpt.Database.Tables("PartyMaster").SetDataSource(ds.Tables("PartyMaster"))
            rpt.Database.Tables("PartyDebit").SetDataSource(ds.Tables("PartyDebit"))
            rpt.Database.Tables("PartyCredit").SetDataSource(ds.Tables("PartyCredit"))

            rpt.Subreports("DebitDetail").Database.Tables("PartyDebit").SetDataSource(ds.Tables("PartyDebit"))
            rpt.Subreports("CreditDetail").Database.Tables("PartyCredit").SetDataSource(ds.Tables("PartyCredit"))

            Dim frm As New FrmCommanReportView(rpt)
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.Show()

            ds.Dispose()
            rpt.Refresh()
        End If
    End Sub

    Private Sub gvAddressList_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles gvAddressList.PreviewKeyDown
        Try
            If (e.KeyCode = Keys.Down) Then
                If gvAddressList.Rows.Count - 1 > gvAddressList.CurrentRow.Index Then
                    Address_ID = Convert.ToInt64(Me.gvAddressList.Rows(Me.gvAddressList.CurrentRow.Index + 1).Cells(0).Value)
                    bindAllData(Address_ID)
                End If
            Else
                If gvAddressList.CurrentRow.Index > 0 Then
                    Address_ID = Convert.ToInt64(Me.gvAddressList.Rows(Me.gvAddressList.CurrentRow.Index - 1).Cells(0).Value)
                    bindAllData(Address_ID)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub gvAddressList_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gvAddressList.CellClick
        Try
            Address_ID = Me.gvAddressList.SelectedCells(0).Value
            bindAllData(Address_ID)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub gvDebit_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvDebit.DoubleClick
        If gvDebit.SelectedRows.Count > 0 Then
            txtDebitPlantName.Text = gvDebit.SelectedCells(1).Value.ToString()
            txtQty.Text = gvDebit.SelectedCells(2).Value.ToString()
            txtDebitAmount.Text = gvDebit.SelectedCells(3).Value.ToString()
            txtDebitNo.Text = gvDebit.SelectedCells(0).Value.ToString()
            rwIDDelDebitDetail = gvDebit.CurrentCell.RowIndex
        End If
    End Sub

    Private Sub gvCredit_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvCredit.DoubleClick
        If gvCredit.SelectedRows.Count > 0 Then
            txtCreditNo.Text = gvCredit.SelectedCells(0).Value.ToString()
            txtCreditPType.Text = gvCredit.SelectedCells(1).Value.ToString()
            dtCreditPDate.Value = gvCredit.SelectedCells(2).Value.ToString()
            txtCreditAmount.Text = gvCredit.SelectedCells(3).Value.ToString()
            rwIDDelCreditDetail = gvCredit.CurrentCell.RowIndex
        End If
    End Sub

    Private Sub txtEntryNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntryNo.Leave
        If (txtEntryNo.Text <> "") Then

            Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEntryNo.Text).ToList()
            If (data.Count > 0) Then
                Address_ID = data(0).Pk_AddressID
                bindAllData(Address_ID)
            End If

            Dim data1 = linq_obj.SP_Select_All_Tbl_OrderOneMaster(Address_ID).ToList()
            If data1.Count = 0 Then
                MessageBox.Show("This Enqry have not Order")
                ClearAll()
                txtEntryNo.Focus()
                Exit Sub
            End If
        End If

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        bindEnqGrid()
    End Sub

    Private Sub btnDeleteCredit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnDelDebit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtDebitAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDebitAmount.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtCreditAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditAmount.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtDebitTotal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDebitTotal.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtDebitDiscount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDebitDiscount.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtCreditTotal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditTotal.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtCreditKasar_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditKasar.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtCreditOutstanding_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditOutstanding.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtCreditAdvance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditAdvance.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub gvDebit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvDebit.KeyDown

    End Sub

    Public Sub SetSrno(ByVal gv As DataGridView, ByVal iCol As Integer)
        Dim i As Integer = 0
        For i = 0 To gv.Rows.Count - 1
            gv.Rows(i).Cells(iCol).Value = i + 1
        Next
    End Sub
    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExit.Click
        Me.Close()
    End Sub

    Private Sub gvCredit_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gvCredit.CellContentClick

    End Sub

    Private Sub gvCredit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvCredit.KeyDown

    End Sub

    
    Public Sub SetCreditOutStn()
        txtCreditOutstanding.Text = Convert.ToDouble(IIf(txtCreditTotal.Text = "", 0, txtCreditTotal.Text)) - (Convert.ToDouble(IIf(txtCreditKasar.Text = "", 0, txtCreditKasar.Text)) + Convert.ToDouble(IIf(txtCreditAdvance.Text = "", 0, txtCreditAdvance.Text)))
    End Sub


    

    Private Sub dtCFormRecDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtCFormRecDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtCFormRecDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtCFormRecDate.TextChanged

    End Sub

    Private Sub dtCFormRecDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtCFormRecDate.Validating
        If Class1.ChkVaildDate(dtCFormRecDate.Text) = False Then
            MessageBox.Show("Date Format is not valid ")
            e.Cancel = True
        End If
    End Sub

    Private Sub dtChequeDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtChequeDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtChequeDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtChequeDate.TextChanged

    End Sub

    Private Sub dtChequeDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtChequeDate.Validating
        If Class1.ChkVaildDate(dtChequeDate.Text) = False Then
            MessageBox.Show("Date Format is not valid ")
            e.Cancel = True
        End If
    End Sub

  

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then

            gvCredit.Rows.RemoveAt(gvCredit.CurrentRow.Index)
            tblCredit = gvCredit.DataSource
            txtCreditTotal.Text = getCreditTotal()
            txtCreditNo.Text = ""
        End If

    End Sub

    Private Sub txtCreditKasar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCreditKasar.TextChanged

    End Sub



    Private Sub dtODate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtODate.TextChanged

    End Sub

    Private Sub btnDebitDelete_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dtDispatchDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtDispatchDate.KeyPress

    End Sub

    Private Sub dtDispatchDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtDispatchDate.TextChanged

    End Sub

    Private Sub gvAddressList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gvAddressList.CellContentClick

    End Sub

    Private Sub txtContact_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContact.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtContact_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContact.TextChanged

    End Sub

    Private Sub txtcontactNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcontactNo.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtcontactNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcontactNo.TextChanged

    End Sub

    Private Sub txtEmailAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmailAddress.TextChanged

    End Sub

    Private Sub txtEmailAddress_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtEmailAddress.Validating
        If Class1.IsValidEmail(txtEmailAddress.Text) = False Then
            MessageBox.Show("Pls Enter Valid Email Id")
            e.Cancel = True
        End If
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged

    End Sub

    Private Sub txtBreakNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBreakNo.TextChanged

    End Sub
End Class