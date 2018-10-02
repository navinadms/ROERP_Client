
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

Public Class Spare_Quotation
    Dim PK_Address_ID As Integer
    Dim Pk_Spare_Quot_Master_ID As Integer
    Dim Pk_Spare_Quot_Detail_ID As Integer
    Dim OrderTempPath, QPath As String
    Dim TotalRate, TotalDisc, FinalAmount, Total_GST, Total_FinalRate

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        txtDesignation.Text = "( " + Class1.global.Designation + " )"
        txtUserName.Text = Class1.global.UserName
        Terms_Bind()
        GvSpareQuotationList_Bind()
        ddlCategory_Bind()
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



    End Sub



    Public Sub ddlCategory_Item_Bind()

        Dim data = linq_obj.SP_Get_Spare_Category_Item_By_Category_ID(Convert.ToInt64(ddlCategory.SelectedValue)).ToList()
        ddlItem.DataSource = data
        ddlItem.DisplayMember = "Item_Name"
        ddlItem.ValueMember = "Pk_Spare_Cat_Item_ID"

    End Sub
    Public Sub Terms_Bind()
        txtTerms1.Text = "1.Price Bases Ex. Godown Ahmedabad."
        txtTerms2.Text = "2.Transportation Charge,Service-Travelling Charge, , Packing charges, Insurance, Etc., Will Be Extra At Actual.(CLIENT SCOPE)"
        txtTerms3.Text = "3.Payment : 100% Advance against Performa invoice before dispatch"
        txtTerms4.Text = "4.Delivery : 1 Week After conform order."
        txtTerms5.Text = "5.Guarantee clause is valid for mfg. Defect/workmanship defect only."
        txtTerms6.Text = "6.Our liability is limited to repair or replace of the same."
        txtTerms7.Text = "7.Offer validity : 1 week."

    End Sub

    Public Sub GvSpareQuotationDetail_Bind()
        Dim data = linq_obj.SP_Get_Spare_Quotation_Detail_List(Pk_Spare_Quot_Master_ID).ToList()
        GvSpareQuotationDetail.DataSource = data
        GvSpareQuotationDetail.Columns(0).Visible = False
        GvSpareQuotationDetail.Columns(1).Visible = False
        GvSpareQuotationDetail.Columns(2).Visible = False
        TotalCalculation()

    End Sub



    Private Sub ddlCategory_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectionChangeCommitted
        ddlCategory_Item_Bind()
    End Sub



    Private Sub ddlBussines_Executive_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlLetterType.SelectionChangeCommitted
        If ddlLetterType.SelectedItem = "TELEPHONIC" Then

            Dim desc As String
            desc = "Thank you for your Telecon with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + "  regards to subject matter on " + dtLetterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."

            txtDescription.Text = desc.ToString()


        End If
        If ddlLetterType.SelectedItem = "MAIL" Then

            Dim desc As String
            desc = "This refers to your mail dated " + dtLetterDate.Text + " regarding your subject requirement. We thank you very much for your enquiry and indeed appreciated your interest in range of our products."

            txtDescription.Text = desc.ToString()
        End If
        If ddlLetterType.SelectedItem = "VISIT NARODA FACTORY" Then

            Dim desc As String
            desc = "We thank you very much for paying your visit at our Naroda Factory and personal discussion you had with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " on " + dtLetterDate.Text + " in regards to subject matter. Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."

            txtDescription.Text = desc.ToString()


        End If

        If ddlLetterType.SelectedItem = "PERSONAL VISIT " Then

            Dim desc As String
            Dim Newline As String
            Newline = System.Environment.NewLine
            desc = "The courtesy and consideration extended to our Director Mr. J. B. Vyas during his personal visit at your office of the date to discussed regarding subject matter, are sincerely appreciated. We thank you very much for sparing your valued time for the discussion and showing interest in range of our products."
            txtDescription.Text = desc.ToString()


        End If
    End Sub

    Private Sub txtQty_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.Leave
        If txtRate.Text.Trim() <> "" Then
            Discout_Cal()
        End If
    End Sub
    Public Sub Discout_Cal()
        Dim finalamount As Decimal
        Dim GstAmount As Decimal
        Dim CheckDiscount As Decimal
        Dim FinalRate As Decimal
        If txtRate.Text.Trim() <> "" And txtDisc.Text.Trim() <> "" Then

            If ddlCategory.Text.Trim.ToLower() = "other" Then
                btnAdd.Enabled = True
                FinalRate = (Convert.ToDecimal(txtRate.Text) - Convert.ToDecimal(txtDisc.Text))
                txtFinalRate.Text = FinalRate.ToString("N2")
                finalamount = FinalRate * Convert.ToInt32(txtQty.Text)
                GstAmount = finalamount * Convert.ToDecimal(lblGST.Text) / 100
                txtGSTAmount.Text = GstAmount.ToString("N2")
                txtPrice.Text = (finalamount + GstAmount).ToString("N2")
            Else
                CheckDiscount = (Convert.ToDecimal(txtRate.Text) * 20) / 100
                If (Convert.ToInt64(CheckDiscount) < Convert.ToInt64(txtDisc.Text)) And Class1.global.User.ToLower() <> "rk" Then
                    MessageBox.Show("You have Not Permission...")
                    btnAdd.Enabled = False
                Else
                    btnAdd.Enabled = True
                    FinalRate = (Convert.ToDecimal(txtRate.Text) - Convert.ToDecimal(txtDisc.Text))
                    txtFinalRate.Text = FinalRate.ToString("N2")
                    finalamount = FinalRate * Convert.ToInt32(txtQty.Text)
                    GstAmount = finalamount * Convert.ToDecimal(lblGST.Text) / 100
                    txtGSTAmount.Text = GstAmount.ToString("N2")
                    txtPrice.Text = (finalamount + GstAmount).ToString("N2")
                End If
            End If
        End If
    End Sub

    Private Sub ddlItem_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlItem.SelectionChangeCommitted
        Dim ItemData = linq_obj.SP_Get_Spare_Category_Item_By_Category_ID(Convert.ToInt64(ddlCategory.SelectedValue)).Where(Function(p) p.Pk_Spare_Cat_Item_ID = Convert.ToInt64(ddlItem.SelectedValue)).ToList()

        For Each item As SP_Get_Spare_Category_Item_By_Category_IDResult In ItemData
            txtRate.Text = item.Item_Rate
            txtUnit.Text = item.Qty
            txtQty.Text = "1"
            txtDisc.Text = "0"
            lblGST.Text = item.GST
            Discout_Cal()
        Next

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try


            'check Quotation

            If PK_Address_ID > 0 Then



                Dim CheckQuotation = linq_obj.SP_Get_Spare_Quotaion_By_QuotaionNo(txtQuotationNo.Text.Trim()).ToList()
                If CheckQuotation.Count > 0 Then
                    Pk_Spare_Quot_Master_ID = CheckQuotation(0).Pk_Spare_Quot_Master_ID
                    btnSubmit.Text = "Update"
                Else
                    'Spare Quotation Master Data
                    Pk_Spare_Quot_Master_ID = linq_obj.SP_insert_Update_Spare_Quotation_Master(0, PK_Address_ID, Class1.global.UserID, 1, txtQuotationNo.Text, txtKind.Text, txtBussness_Exe.Text, txtBuss_Name.Text, dtLetterDate.Value.Date, ddlLetterType.Text, txtTerms1.Text, txtTerms2.Text, txtTerms3.Text, txtTerms4.Text, txtTerms5.Text, txtTerms6.Text, txtTerms7.Text, txtDate.Text, ddlStatus.Text)
                    linq_obj.SubmitChanges()
                End If

                'Spare Quotation Detail Data


                If (btnAdd.Text = "Add") Then
                    Dim ItemData = linq_obj.SP_Insert_Update_Spare_Quotation_Detail(0, Pk_Spare_Quot_Master_ID, ddlCategory.SelectedValue, ddlItem.SelectedValue, txtRate.Text, txtQty.Text, txtUnit.Text, txtDisc.Text, txtFinalRate.Text, lblGST.Text, txtGSTAmount.Text, txtPrice.Text)
                    linq_obj.SubmitChanges()
                    MessageBox.Show("Add Sucessfully...")
                Else
                    Dim ItemData = linq_obj.SP_Insert_Update_Spare_Quotation_Detail(Pk_Spare_Quot_Detail_ID, Pk_Spare_Quot_Master_ID, ddlCategory.SelectedValue, ddlItem.SelectedValue, txtRate.Text, txtQty.Text, txtUnit.Text, txtDisc.Text, txtFinalRate.Text, lblGST.Text, txtGSTAmount.Text, txtPrice.Text)
                    linq_obj.SubmitChanges()
                    MessageBox.Show("Update Sucessfully...")
                End If

                GvSpareQuotationDetail_Bind()

                Clear_Text()
            Else
                MessageBox.Show("EnqNo not found..")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub Clear_Text()
        txtPrice.Text = ""
        txtQty.Text = ""
        txtRate.Text = ""
        txtUnit.Text = ""
        txtDisc.Text = ""
        btnAdd.Text = "Add"
        lblGST.Text = "0"

        txtGSTAmount.Text = "0"

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            TotalCalculation()
            Print_Quotation()
        Catch ex As Exception

        End Try


    End Sub

    Public Sub TotalCalculation()
        TotalRate = 0
        TotalDisc = 0
        FinalAmount = 0
        Total_FinalRate = 0
        Total_GST = 0


        For index = 0 To GvSpareQuotationDetail.Rows.Count - 1

            TotalRate = TotalRate + Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("Rate").Value)
            TotalDisc = TotalDisc + Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("Disc").Value) * Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("Qty").Value)
            FinalAmount = FinalAmount + Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("Price").Value)
            Total_GST = Total_GST + Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("GST_Amount").Value)
            Total_FinalRate = Total_FinalRate + Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("FinalRate").Value)
        Next
        lblTotalRate.Text = Convert.ToDecimal(TotalRate).ToString("N2")
        lblTotalDisc.Text = Convert.ToDecimal(TotalDisc).ToString("N2")

        lblFinalAmount.Text = Convert.ToDecimal(Math.Round(FinalAmount, 0)).ToString("N2")
        lblTotal_Gst.Text = Convert.ToDecimal(Total_GST).ToString("N2")


        lblTotalFinalRate.Text = Convert.ToDecimal(Total_FinalRate).ToString("N2")




    End Sub


    Public Sub Print_Quotation()

        Try


            QPath = Class1.global.QPath
            If (Not System.IO.Directory.Exists(QPath + "\SpareQuotation")) Then
                System.IO.Directory.CreateDirectory(QPath + "\SpareQuotation")
            End If
            OrderTempPath = QPath + "\SpareQuotation"
            Dim OrderPriceDS As New DataSet()
            Dim dt = New DataTable("SpareQuotation")
            dt.Columns.Add("RefNo")
            dt.Columns.Add("PartyName")
            dt.Columns.Add("Sub")
            dt.Columns.Add("Description")
            dt.Columns.Add("ItemName")
            dt.Columns.Add("Rate", GetType(Decimal))
            dt.Columns.Add("Qty")
            dt.Columns.Add("Disc", GetType(Decimal))
            dt.Columns.Add("FinalRate", GetType(Decimal))
            dt.Columns.Add("GST")
            dt.Columns.Add("Amount", GetType(Decimal))
            dt.Columns.Add("Terms1")
            dt.Columns.Add("Terms2")
            dt.Columns.Add("Terms3")
            dt.Columns.Add("Terms4")
            dt.Columns.Add("Terms5")
            dt.Columns.Add("Terms6")
            dt.Columns.Add("Terms7")
            dt.Columns.Add("TotalRate")
            dt.Columns.Add("TotalDisc")
            dt.Columns.Add("CreateDate")
            dt.Columns.Add("FinalAmount")
            dt.Columns.Add("Total_GST", GetType(Decimal))
            dt.Columns.Add("Total_FinalRate", GetType(Decimal))
            dt.Columns.Add("UserName")
            dt.Columns.Add("Designation")
            Dim CompanyName As String

            If chkCompany.Checked = True Then
                CompanyName = "M/s.                                                              ."
            Else
                CompanyName = txtName.Text
            End If
            For index = 0 To GvSpareQuotationDetail.Rows.Count - 1
                Dim GST As String
                GST = ""

                GST = Convert.ToString(GvSpareQuotationDetail.Rows(index).Cells("GST_Amount").Value) + " (" + Convert.ToString(GvSpareQuotationDetail.Rows(index).Cells("GST").Value) + "%)"

                dt.Rows.Add(txtQuotationNo.Text.Trim(), CompanyName, txtKind.Text.Trim(), txtDescription.Text.Trim(), GvSpareQuotationDetail.Rows(index).Cells("Item_Name").Value.ToString(), Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("Rate").Value).ToString("N2"),
                            GvSpareQuotationDetail.Rows(index).Cells("Qty").Value.ToString() + " " + GvSpareQuotationDetail.Rows(index).Cells("UnitType").Value.ToString(), Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("Disc").Value).ToString("N2"), Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("FinalRate").Value).ToString("N2"), GST, Convert.ToDecimal(GvSpareQuotationDetail.Rows(index).Cells("Price").Value).ToString("N2"), txtTerms1.Text.Trim(), txtTerms2.Text.Trim(), txtTerms3.Text.Trim(), txtTerms4.Text.Trim(), txtTerms5.Text.Trim(), txtTerms6.Text.Trim(), txtTerms7.Text.Trim(), lblTotalRate.Text, lblTotalDisc.Text, dtLetterDate.Text, lblFinalAmount.Text, lblTotal_Gst.Text, lblTotalFinalRate.Text, txtUserName.Text, txtDesignation.Text)

            Next
            OrderPriceDS.Tables.Add(dt)
            Dim fullpath12 As String
            If rblStandard.Checked = True Then
                Dim rpt4 As New rpt_SpareQuotation
                Class1.WriteXMlFile(OrderPriceDS, "Generate_Payment_Terms", "SpareQuotation")
                rpt4.Database.Tables(0).SetDataSource(OrderPriceDS.Tables("SpareQuotation"))
                fullpath12 = txtQuotationNo.Text.Trim().Replace("/", "-") + ".pdf"
                rpt4.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\" + fullpath12)
                MessageBox.Show("Document Ready !")
                System.Diagnostics.Process.Start(OrderTempPath + "\\" + fullpath12)
                rpt4.Dispose()
            Else
                Dim rpt4 As New rpt_SpareQuotationWithDisc
                Class1.WriteXMlFile(OrderPriceDS, "Generate_Payment_Terms", "SpareQuotation")
                rpt4.Database.Tables(0).SetDataSource(OrderPriceDS.Tables("SpareQuotation"))
                fullpath12 = txtQuotationNo.Text.Trim().Replace("/", "-") + ".pdf"
                rpt4.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\" + fullpath12)
                MessageBox.Show("Document Ready !")
                System.Diagnostics.Process.Start(OrderTempPath + "\\" + fullpath12)
                rpt4.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub


    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        If txtEnqNo.Text.Trim() <> "" Then
            Dim data = linq_obj.SP_Get_ServiceODSite_Allotment_EnqNo(txtEnqNo.Text.Trim()).ToList()
            If data.Count > 0 Then
                For Each item As SP_Get_ServiceODSite_Allotment_EnqNoResult In data
                    txtName.Text = item.Name
                    txtCity.Text = item.City
                    txtTaluka.Text = item.Taluka
                    txtState.Text = item.State
                    txtPincode.Text = item.Pincode


                    PK_Address_ID = item.Pk_AddressID
                    Dim ref As String
                    Dim year1, MaxNo As Int32

                    Dim Maxno1 = linq_obj.SP_Get_Max_Quotation_No_By_EnqNo(PK_Address_ID).ToList()


                    year1 = Convert.ToInt32(txtDate.Text.Substring(txtDate.Text.Length - 2))
                    ref = "IIES-S / " + Class1.global.User.ToString().ToUpper() + " / " + txtEnqNo.Text + " - " + Maxno1(0).MaxNo.ToString() + " / " + year1.ToString()
                    txtQuotationNo.Text = ref.ToString()
                Next
            Else
                MessageBox.Show("Invalid EnqNo...")
                txtEnqNo.Focus()

            End If


        End If
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        If Not IsNumeric(e.KeyChar) And Not e.KeyChar = ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDisc_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDisc.KeyPress
        If Not IsNumeric(e.KeyChar) And Not e.KeyChar = ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtRate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRate.KeyPress
        If Not IsNumeric(e.KeyChar) And Not e.KeyChar = ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDisc_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDisc.Leave
        Discout_Cal()
    End Sub

    Private Sub txtRate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRate.Leave
        Discout_Cal()
    End Sub

    Private Sub GvSpareQuotation_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSpareQuotationList.DoubleClick
        btnSubmit.Text = "Update"
        txtEnqNo.Enabled = False
        Pk_Spare_Quot_Master_ID = GvSpareQuotationList.SelectedRows(0).Cells(0).Value
        Diplay_Data()
    End Sub
    Public Sub Diplay_Data()

        Dim data = linq_obj.SP_Get_Spare_Quotation_Master_List().Where(Function(p) p.Pk_Spare_Quot_Master_ID = Pk_Spare_Quot_Master_ID).ToList()
        For Each item As SP_Get_Spare_Quotation_Master_ListResult In data
            PK_Address_ID = item.Pk_AddressID
            txtEnqNo.Text = item.EnqNo
            txtQuotationNo.Text = item.QuotationNo
            txtName.Text = item.Name
            txtCity.Text = item.City
            txtTaluka.Text = item.Taluka
            txtState.Text = item.State
            txtPincode.Text = item.Pincode


            txtDate.Text = item.CreateDate
            ddlStatus.Text = item.Status
            txtKind.Text = item.Subject
            ddlLetterType.Text = item.Letter_type
            txtBussness_Exe.Text = item.Buss_Exec
            txtBuss_Name.Text = item.Buss_Name
            dtLetterDate.Text = item.Letter_Date
            txtTerms1.Text = item.Terms1
            txtTerms2.Text = item.Terms2
            txtTerms3.Text = item.Terms3
            txtTerms4.Text = item.Terms4
            txtTerms5.Text = item.Terms5
            txtTerms6.Text = item.Terms6
            txtTerms7.Text = item.Terms7

            GvSpareQuotationDetail_Bind()
            ddlBussines_Executive_SelectionChangeCommitted(Nothing, Nothing)
        Next
    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GvSpareQuotationList_Bind()
    End Sub


    Public Sub GvSpareQuotationList_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim criteria As String
        criteria = "and "
        If txtSearchEnqno.Text.Trim() <> "" Then
            criteria = criteria + " AM.EnqNo like '%" + txtSearchEnqno.Text + "%'and "
        End If
        If txtSearchName.Text.Trim() <> "" Then
            criteria = criteria + " AM.Name like '%" + txtSearchName.Text + "%'and "
        End If


        If criteria = "and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.Trim().ToString().Substring(0, criteria.Trim().Length - 3)
        End If


        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Spare_Quotation_Master_Criteria"
        cmd.Parameters.Add("@Criteria", SqlDbType.VarChar).Value = IIf(criteria = "", "", criteria)
        cmd.Parameters.AddWithValue("@UserID", Class1.global.UserID.ToString())

        'End 
        cmd.CommandTimeout = 3000
        Dim objclass As New Class1
        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvSpareQuotationList.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            For i = 0 To ds.Tables(1).Rows.Count - 1
                dt.Rows.Add(ds.Tables(1).Rows(i)("Pk_Spare_Quot_Master_ID"), ds.Tables(1).Rows(i)("EnqNo"), ds.Tables(1).Rows(i)("Name"))

            Next
            GvSpareQuotationList.DataSource = dt
            txtTotal.Text = Convert.ToString(dt.Rows.Count)

        End If

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GvSpareQuotationList_Bind()
        txtSearchName.Text = ""
        txtSearchEnqno.Text = ""

    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Pk_Spare_Quot_Master_ID = linq_obj.SP_insert_Update_Spare_Quotation_Master(Pk_Spare_Quot_Master_ID, PK_Address_ID, Class1.global.UserID, 1, txtQuotationNo.Text, txtKind.Text, txtBussness_Exe.Text, txtBuss_Name.Text, dtLetterDate.Value.Date, ddlLetterType.Text, txtTerms1.Text, txtTerms2.Text, txtTerms3.Text, txtTerms4.Text, txtTerms5.Text, txtTerms6.Text, txtTerms7.Text, txtDate.Text, ddlStatus.Text)
        linq_obj.SubmitChanges()
        MessageBox.Show("Update Sucessfully..")
    End Sub
    Public Sub Clear_TextAll()
        txtPrice.Text = ""
        txtQty.Text = ""
        txtRate.Text = ""
        txtUnit.Text = ""
        txtDisc.Text = ""
        btnAdd.Text = "Add"
        btnSubmit.Text = "Submit"
        PK_Address_ID = 0
        Pk_Spare_Quot_Detail_ID = 0
        Pk_Spare_Quot_Master_ID = 0
        txtEnqNo.Text = ""
        txtName.Text = ""
        txtQuotationNo.Text = ""
        txtKind.Text = ""
        txtBuss_Name.Text = ""
        txtBussness_Exe.Text = ""
        txtEnqNo.Enabled = True
        GvSpareQuotationDetail.DataSource = Nothing

    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Clear_TextAll()
    End Sub

    Private Sub GvSpareQuotationDetail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSpareQuotationDetail.DoubleClick
        btnAdd.Text = "Update"

        Pk_Spare_Quot_Detail_ID = GvSpareQuotationDetail.SelectedRows(0).Cells("Pk_Spare_Quot_Detail_ID").Value
        ddlCategory.SelectedValue = GvSpareQuotationDetail.SelectedRows(0).Cells("Fk_Category_ID").Value
        ddlCategory_SelectionChangeCommitted(Nothing, Nothing)
        ddlItem.SelectedValue = GvSpareQuotationDetail.SelectedRows(0).Cells("Fk_Item_ID").Value
        txtRate.Text = GvSpareQuotationDetail.SelectedRows(0).Cells("Rate").Value.ToString()
        txtQty.Text = GvSpareQuotationDetail.SelectedRows(0).Cells("Qty").Value.ToString()
        txtUnit.Text = GvSpareQuotationDetail.SelectedRows(0).Cells("UnitType").Value.ToString()
        txtDisc.Text = GvSpareQuotationDetail.SelectedRows(0).Cells("Disc").Value.ToString()
        lblGST.Text = Convert.ToString(GvSpareQuotationDetail.SelectedRows(0).Cells("GST").Value)
        txtGSTAmount.Text = Convert.ToString(GvSpareQuotationDetail.SelectedRows(0).Cells("GST_Amount").Value)
        txtPrice.Text = GvSpareQuotationDetail.SelectedRows(0).Cells("Price").Value.ToString()

    End Sub
End Class