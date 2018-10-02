﻿Imports System.Collections.Generic
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
Imports System.Data.SqlClient



Public Class QuotationMaster
    Shared selectSrNo As String
    Shared Address_ID As Integer
    Private con1 As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader
    Private da As SqlDataAdapter
    Private ds As DataSet
    Private da1 As SqlDataAdapter
    Private ds1 As DataSet
    Shared dt As DataTable
    Public capacityType As String
    Shared QuotationMaxId As Int32
    Shared Fk_SalesExecutiveQtnID As Integer
    Shared Path11 As String
    Public UserID As Int32
    Public QuationId As Int32
    Public QPath As String
    Shared DocumentStatus As Int16
    Dim appPath As String
    Dim lines As String
    Shared LanguageId As Int32
    Shared FlagPdf As Int32
    Shared sys1total As Decimal
    Shared sys1mutotal As Decimal
    Shared QtempPath As String
    Shared sys2total As Decimal
    Shared sys2mutotal As Decimal
    Dim EnqMax As Int16
    Shared sys3total As Decimal
    Shared sys3mutotal As Decimal
    Shared sys4total As Decimal
    Shared sys4mutotal As Decimal
    Shared TotalLeavevar As Decimal
    Dim str
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        InitializeComponent()
        con1 = Class1.con
        LanguageId = 1
        RdbEnglish.Checked = True
        appPath = Path.GetDirectoryName(Application.ExecutablePath)
        BindOnLanguageName()
        GetTechnicalData()
        ddlEnqType_Bind()
        GetKind_SubData()
        GetcheckData()
        '  GvQuotationSearch_Bind(LanguageId)
        QPath = Class1.global.QPath
        TxtTax.Text = "0"
        Class1.global.QuatationId = 0
        txtDesignation.Text = Class1.global.Designation
        txtUserName.Text = Class1.global.UserName
        txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        txtLatterDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
        PicSignature.ImageLocation = Class1.global.Signature
        PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
        'If (Class1.global.QuatationId <> 0) Then
        '    GvCategorySearch_DoubleClick(Nothing, Nothing)
        'End If
        GetClientDetails_Bind()
        ' BindEnqType()

        getPageRight()

    End Sub

    Public Sub GvCategorySearch_For_SalesExecutive_Bind()

        If rblNew.Checked = True Then
            Dim data = linq_obj.SP_Get_SalesExecutiveQuotation_List().ToList().Where(Function(p) p.ToID = Class1.global.UserID).ToList().Where(Function(p) p.Status = "Pending").ToList()

            Dim dt As New DataTable
            Dim status As Boolean
            dt.Columns.Add("Fk_SalesExecutiveQtnID")
            dt.Columns.Add("EnqNo")
            dt.Columns.Add("From")
            dt.Columns.Add("Status")
            dt.Columns.Add("Priority")
            For Each item As SP_Get_SalesExecutiveQuotation_ListResult In data
                If item.QuotationType.Trim() = "RO" Or item.QuotationType.Trim() = "NON ISI" Or item.QuotationType.Trim() = "SPARE" Or item.QuotationType.Trim() = "SODA SOFT DRINK" Or item.QuotationType.Trim() = "PACKING" Then
                    dt.Rows.Add(item.PK_SalesExecutiveQtnID, item.EnqNo, item.FromUser, item.Status, item.Priority)
                End If

            Next

            GvCategorySearch.DataSource = dt

            For index = 0 To GvCategorySearch.RowCount - 1
                If (GvCategorySearch.Rows(index).Cells(4).Value = "Urgent") Then
                    GvCategorySearch.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                    GvCategorySearch.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.White
                End If
                If (GvCategorySearch.Rows(index).Cells(4).Value = "Courier") Then
                    GvCategorySearch.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Green
                    GvCategorySearch.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.White
                End If
            Next
            GvCategorySearch.Columns(0).Visible = False
            GvCategorySearch.Columns(4).Visible = False
            txtTotalRecord.Text = data.Count()
        End If
    End Sub

    Public Sub GvSingle__SalesExecutive_Bind(ByVal Fk_SalesExecutiveQtnID As Integer)
        setLanguageId()
        Dim StrArray() As String
        If txtNoContent.Text.Trim() <> "" Then

            If RblSingle.Checked = True Then
                dt = New DataTable()
                dt.Columns.Add("Remove", GetType(Boolean))
                dt.Columns.Add("Select", GetType(Boolean))
                dt.Columns.Add("SrNo", GetType(Int32))
                dt.Columns.Add("Category", GetType(String))
                dt.Columns.Add("Price", GetType(String))
                dt.Columns.Add("Tax", GetType(String))

            ElseIf RblOther.Checked = True Then
                dt = New DataTable()
                dt.Columns.Add("Remove", GetType(Boolean))
                dt.Columns.Add("Select", GetType(Boolean))
                dt.Columns.Add("SrNo", GetType(Int32))
                dt.Columns.Add("Category", GetType(String))
                dt.Columns.Add("Price", GetType(String))
                dt.Columns.Add("Qty", GetType(String))
                dt.Columns.Add("Total", GetType(String))
                dt.Columns.Add("Tax", GetType(String))

            End If

            Dim str As String
            str = "Select  SA.SNo,CM.Category,CM.Capacity,CM.Price from SalesExecutive_Technical_Data (NOLOCK) AS SA  inner join Category_Master as CM on CM.SNo=SA.SNo where  SA.MainCategory='" + txtQoutType.Text.Trim() + "' and CM.MainCategory='" + txtQoutType.Text.Trim() + "' and CM.Capacity ='" & txtCapacity1.Text & "' and SA.Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & " ORDER BY SA.SNo"
            'str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity1.Text & "' and MainCategory='" + txtQoutType.Text.Trim() + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)


            If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContent.Text) Then
                For i As Integer = 0 To Convert.ToInt32(txtNoContent.Text) - 1
                    If RblSingle.Checked = True Then
                        StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "0", "0")

                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)

                    ElseIf RblOther.Checked = True Then
                        StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "0", "1")
                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text)
                    End If

                Next
                If dt Is Nothing Then
                Else
                    If dt.Rows.Count > 0 Then
                        Dim dView As New DataView(dt)
                        dView.Sort = "SrNo ASC"
                        dt = dView.ToTable()
                    End If
                End If
                GvTechnical.DataSource = dt
                '   lblSno.Text = GvTechnical.RowCount + 1
                da.Dispose()
                ds.Dispose()
                Total1()
            Else
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If RblSingle.Checked = True Then
                        StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "0", "1")
                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)
                        'dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)

                    ElseIf RblOther.Checked = True Then
                        StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "0", "1")

                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text)
                    End If
                Next
                If dt Is Nothing Then
                Else
                    If dt.Rows.Count > 0 Then
                        Dim dView As New DataView(dt)
                        dView.Sort = "SrNo ASC"
                        dt = dView.ToTable()
                    End If
                End If
                GvTechnical.DataSource = dt
                '  lblSno.Text = GvTechnical.RowCount + 1
                da.Dispose()
                ds.Dispose()
                Total1()
            End If

        End If


    End Sub
    Public Sub GvMultiple_SalesExecutive_Bind(ByVal Fk_SalesExecutiveQtnID As Integer)
        setLanguageId()
        If txtNoContent.Text.Trim() <> "" Then
            dt = New DataTable()

            dt.Columns.Add("Remove", GetType(Boolean))
            dt.Columns.Add("Select", GetType(Boolean))
            dt.Columns.Add("SrNo", GetType(Int32))
            dt.Columns.Add("Category", GetType(String))
            dt.Columns.Add("Price1", GetType(String))
            dt.Columns.Add("Price2", GetType(String))
            dt.Columns.Add("Tax", GetType(String))

            Dim str As String
            Dim str1 As String
            If txtCapacity2.Text.Trim() <> "" And txtCapacity1.Text.Trim() <> "" Then

                str = "Select  SA.SNo,CM.Category,CM.Capacity,CM.Price from SalesExecutive_Technical_Data (NOLOCK) AS SA  inner join Category_Master as CM on CM.SNo=SA.SNo where  SA.MainCategory='" + txtQoutType.Text.Trim() + "' and CM.MainCategory='" + txtQoutType.Text.Trim() + "' and CM.Capacity ='" & txtCapacity1.Text & "' and SA.Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & " ORDER BY SA.SNo"
                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)


                str1 = "Select  SA.SNo,CM.Category,CM.Capacity,CM.Price from SalesExecutive_Technical_Data (NOLOCK) AS SA  inner join Category_Master as CM on CM.SNo=SA.SNo where  SA.MainCategory='" + txtQoutType.Text.Trim() + "' and CM.MainCategory='" + txtQoutType.Text.Trim() + "' and CM.Capacity ='" & txtCapacity2.Text & "' and SA.Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & " ORDER BY SA.SNo"
                da1 = New SqlDataAdapter(str1, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                Dim StrArray() As String
                If ds.Tables(0).Rows.Count >= ds1.Tables(0).Rows.Count Then

                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContent.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContent.Text) - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "0")
                                dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
                            End If

                        Next
                        If dt Is Nothing Then
                        Else
                            If dt.Rows.Count > 0 Then
                                Dim dView As New DataView(dt)
                                dView.Sort = "SrNo ASC"
                                dt = dView.ToTable()
                            End If
                        End If
                        GvTechnical.DataSource = dt
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "0")
                                dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
                            End If
                        Next
                        If dt Is Nothing Then
                        Else
                            If dt.Rows.Count > 0 Then
                                Dim dView As New DataView(dt)
                                dView.Sort = "SrNo ASC"
                                dt = dView.ToTable()
                            End If
                        End If
                        GvTechnical.DataSource = dt
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If
                Else
                    MessageBox.Show("Technical Data Not Match")

                End If
            End If

        End If
    End Sub

    Public Sub BindEnqType()
        'Dim enq = linq_obj.SP_Get_AddressList().ToList().Where(Function(t) t.EnqStatus = 1)
        'Dim dt As New DataTable
        'dt.Columns.Add("ID")
        'dt.Columns.Add("Name")
        'dt.Columns.Add("EnqNo")
        'For Each item As SP_Get_AddressListResult In enq
        '    dt.Rows.Add(item.Pk_AddressID, item.Name, item.EnqNo)
        'Next
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
            dataView.RowFilter = "([DetailName] like 'Non-ISI Quatation')"
            If (dataView.Count > 0) Then
                dv = dataView.ToTable()
                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            btnAdd.Enabled = True
                        Else
                            btnAdd.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsUpdate") = True) Then
                            btnSave1.Enabled = True
                        Else
                            btnSave1.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsDelete") = True) Then
                            btndeletequotation.Enabled = True
                        Else
                            btndeletequotation.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsPrint") = True) Then
                            btnHF.Enabled = True
                            btnPDFFirstPageOnly.Enabled = True
                            btnPdfPriceWOHF.Enabled = True
                            btnPDFPRICEOnly.Enabled = True
                            btnPDFFirstWOHF.Enabled = True
                            btnWf.Enabled = True
                        Else
                            btnHF.Enabled = False
                            btnPDFFirstPageOnly.Enabled = False
                            btnPdfPriceWOHF.Enabled = False
                            btnPDFPRICEOnly.Enabled = False
                            btnPDFFirstWOHF.Enabled = False
                            btnWf.Enabled = False

                        End If

                    Next
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub GetClientDetails_Bind()
        'txtstate.Text = item.State
        'txtdistrict.Text = item.District
        'txtcoperson.Text = item.ContactPerson
        'txtphoneNo.Text = item.LandlineNo
        'txtmobileNo.Text = item.MobileNo
        'txtEmailID.Text = item.EmailID
        'txtEntryNo.Text = item.EnqNo

    End Sub

    Public Sub GetcheckData()
        If RdbEnglish.Checked Then
            txt1.Text = "PRICES ARE EX. FACTORY NARODA, AHMEDABAD."
            'txt2.Text = "VAT EXTRA AT ACTUAL AT THE TIME OF DELIVERY."
            txt2.Text = "GST WILL APPLICABLE AS PER CENTRAL GOVT. NORMS"
            txt3.Text = "FORWARDING, TRANSPORTATION, OCTROI & INSURANCE SHALL BE EXTRAAT ACTUAL AT THE TIME OF DELIVERY."
            txt4.Text = "ERECTION & COMMISSIONING : EXTRA AT ACTUAL"
            txt41.Text = "To & Fro, + Lodging & Boarding + Local Conveyance charges to buyer's account."
            txt42.Text = "Necessary Tools, Tackles, Labour & Chemical should be provided by the client."
            txt5.Text = "PAYMENT TERMS :"
            txt51.Text = "40% Advance alongwith Techno-Commercial clear order &"
            txt52.Text = "60% Balance against Performa Invoice before despatch."
            txt6.Text = "DELIVERY :"
            txt61.Text = "Within 3-4 weeks after receipt of your order with advance."
            txt7.Text = "GUARANTEE / WARRANTY :"
            txt71.Text = "We can offer you guarantee for 12 months from the date of supply of the plant."
            txt8.Text = "Offer Validity :30 Days"
        ElseIf RdbGujarati.Checked Then
            txt1.Text = "ગુજરાત ટોલ ટેક્ષ /વેટ ડીલીવરી સમયે જે લાગુ હશે તે પ્રમાણે ભરવાનો રહેશે."
            txt2.Text = "ખરીદ ઓર્ડેર મળ્યાથી ૪ -૬ અઠવાડિયામા ડિલીવરી કરવામાં આવશે."
            txt3.Text = "ટ્રાન્સપોર્ટ(અમારી ફેક્ટરીથી આપના સ્થળ સુધી) , જકાત(જો લાગુ પડતી હોય તો) વીમા નો એક્ચુઅલ ચાર્જ ભરવાનો રહેશે."
            txt4.Text = "બાંધકામ & કમીશન : જે લાગુ હશે તે પ્રમાણે ભરવાનો રહેશે."
            txt5.Text = "ઓર્ડેર સમયે ૪૦% રકમ અને બાકી ૬૦% રકમ બીલ સમયે ચુકવવાની રહેશે."
            txt6.Text = "આવવા જવાનો ખર્ચ,રહેવાનો,જમવાનો અને સ્થાનિક ટ્રાન્સપોર્ટશન ચાર્જ પ્લાન્ટ ખરીદનાર પર રહેશે."
            txt7.Text = ""
            txt71.Text = ""
            txt61.Text = ""
            txt41.Text = ""
            txt42.Text = ""
            txt8.Text = "Offer Validity :30 Days"
        ElseIf RdbHindi.Checked Then
            txt1.Text = "PRICES ARE EX. FACTORY NARODA, AHMEDABAD."
            txt2.Text = "VAT EXTRA AT ACTUAL AT THE TIME OF DELIVERY."
            txt3.Text = "FORWARDING, TRANSPORTATION, OCTROI & INSURANCE SHALL BE EXTRAAT ACTUAL AT THE TIME OF DELIVERY."
            txt4.Text = "ERECTION & COMMISSIONING : EXTRA AT ACTUAL"
            txt41.Text = "To & Fro, + Lodging & Boarding + Local Conveyance charges to buyer's account."
            txt42.Text = "Necessary Tools, Tackles, Labour & Chemical should be provided by the client."
            txt5.Text = "PAYMENT TERMS :"
            txt51.Text = "40% Advance alongwith Techno-Commercial clear order &"
            txt52.Text = "60% Balance against Performa Invoice before despatch."
            txt6.Text = "DELIVERY :"
            txt61.Text = "Within 3-4 weeks after receipt of your order with advance."
            txt7.Text = "GUARANTEE / WARRANTY :"
            txt71.Text = "We can offer you guarantee for 12 months from the date of supply of the plant."
            txt8.Text = "Offer Validity :30 Days"
        ElseIf RdbMarathi.Checked Then
            txt1.Text = "PRICES ARE EX. FACTORY NARODA, AHMEDABAD."
            txt2.Text = "VAT EXTRA AT ACTUAL AT THE TIME OF DELIVERY."
            txt3.Text = "FORWARDING, TRANSPORTATION, OCTROI & INSURANCE SHALL BE EXTRAAT ACTUAL AT THE TIME OF DELIVERY."
            txt4.Text = "ERECTION & COMMISSIONING : EXTRA AT ACTUAL"
            txt41.Text = "To & Fro, + Lodging & Boarding + Local Conveyance charges to buyer's account."
            txt42.Text = "Necessary Tools, Tackles, Labour & Chemical should be provided by the client."
            txt5.Text = "PAYMENT TERMS :"
            txt51.Text = "40% Advance alongwith Techno-Commercial clear order &"
            txt52.Text = "60% Balance against Performa Invoice before despatch."
            txt6.Text = "DELIVERY :"
            txt61.Text = "Within 3-4 weeks after receipt of your order with advance."
            txt7.Text = "GUARANTEE / WARRANTY :"
            txt71.Text = "We can offer you guarantee for 12 months from the date of supply of the plant."
            txt8.Text = "Offer Validity :30 Days"
        ElseIf RdbTamil.Checked Then
            txt1.Text = "PRICES ARE EX. FACTORY NARODA, AHMEDABAD."
            txt2.Text = "VAT EXTRA AT ACTUAL AT THE TIME OF DELIVERY."
            txt3.Text = "FORWARDING, TRANSPORTATION, OCTROI & INSURANCE SHALL BE EXTRAAT ACTUAL AT THE TIME OF DELIVERY."
            txt4.Text = "ERECTION & COMMISSIONING : EXTRA AT ACTUAL"
            txt41.Text = "To & Fro, + Lodging & Boarding + Local Conveyance charges to buyer's account."
            txt42.Text = "Necessary Tools, Tackles, Labour & Chemical should be provided by the client."
            txt5.Text = "PAYMENT TERMS :"
            txt51.Text = "40% Advance alongwith Techno-Commercial clear order &"
            txt52.Text = "60% Balance against Performa Invoice before despatch."
            txt6.Text = "DELIVERY :"
            txt61.Text = "Within 3-4 weeks after receipt of your order with advance."
            txt7.Text = "GUARANTEE / WARRANTY :"
            txt71.Text = "We can offer you guarantee for 12 months from the date of supply of the plant."
            txt8.Text = "Offer Validity :30 Days"
        ElseIf RdbTelugu.Checked Then
            txt1.Text = "PRICES ARE EX. FACTORY NARODA, AHMEDABAD."
            txt2.Text = "VAT EXTRA AT ACTUAL AT THE TIME OF DELIVERY."
            txt3.Text = "FORWARDING, TRANSPORTATION, OCTROI & INSURANCE SHALL BE EXTRAAT ACTUAL AT THE TIME OF DELIVERY."
            txt4.Text = "ERECTION & COMMISSIONING : EXTRA AT ACTUAL"
            txt41.Text = "To & Fro, + Lodging & Boarding + Local Conveyance charges to buyer's account."
            txt42.Text = "Necessary Tools, Tackles, Labour & Chemical should be provided by the client."
            txt5.Text = "PAYMENT TERMS :"
            txt51.Text = "40% Advance alongwith Techno-Commercial clear order &"
            txt52.Text = "60% Balance against Performa Invoice before despatch."
            txt6.Text = "DELIVERY :"
            txt61.Text = "Within 3-4 weeks after receipt of your order with advance."
            txt7.Text = "GUARANTEE / WARRANTY :"
            txt71.Text = "We can offer you guarantee for 12 months from the date of supply of the plant."
            txt8.Text = "Offer Validity :30 Days"
        End If

        txt5.Visible = True
        txt6.Visible = True
        txt61.Visible = True
        txt71.Visible = True
        txt7.Visible = True
        txt5.Visible = True
        txt51.Visible = True
        txt52.Visible = True
        txt42.Visible = True

    End Sub

    Public Sub ddlEnqType_Bind()

        Try

            con1.Close()
        Catch ex As Exception

        End Try
        con1.Open()
        Dim str As String
        str = "select * from Enq_Type (NOLOCK)"
        da = New SqlDataAdapter(str, con1)
        ds = New DataSet()
        da.Fill(ds)
        ddlEnqType.DataSource = ds.Tables(0)
        ddlEnqType.DisplayMember = "EnqType"
        ddlEnqType.ValueMember = "Code"
        da.Dispose()
        ds.Dispose()
        con1.Close()
    End Sub

    Public Sub GetTechnicalData()
        Dim desg As String
        setLanguageId()
        txtContent1.AutoCompleteCustomSource.Clear()

        If (txtCapacity1.Text.Trim() <> "") Then
            desg = "select * from Category_Master (NOLOCK) where Capacity='" & txtCapacity1.Text & "' and MainCategory='" & txtQoutType.Text.Trim() & "' and LanguageId = " & LanguageId & " ORDER BY SNo"
            da = New SqlDataAdapter(desg, con1)
            ds = New DataSet()
            da.Fill(ds)
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtContent1.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
            Next
            da.Dispose()
            ds.Dispose()
        End If
        ' lblSno.Text = GvTechnical.RowCount + 1

    End Sub

    Public Sub GvQuotationSearch_Bind(ByVal LanguageId As Integer)
        Try


            Dim str As String
            str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master (NOLOCK) where Quatation_Type = 'NON ISI' and LanguageId = " & LanguageId & " order by Pk_QuotationID desc  "
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)
            bindSearchGrid()
            'txtTotalRecord.Text = tt.ToString()
            da.Dispose()
            ds.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetKind_SubData()
        Dim enqtype As String
        enqtype = "select distinct KindAtt,Subject,Name,QType,Quot_Type,Buss_Excecutive,Buss_Name,Enq_No from Quotation_Master (NOLOCK)"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtBussness_Exe.AutoCompleteCustomSource.Add(dr1.Item("Buss_Excecutive").ToString())
            txtBuss_Name.AutoCompleteCustomSource.Add(dr1.Item("Buss_Name").ToString())
            txtType.AutoCompleteCustomSource.Add(dr1.Item("QType").ToString())
            txtQoutType.AutoCompleteCustomSource.Add(dr1.Item("Quot_Type").ToString())
            txtSearchName.AutoCompleteCustomSource.Add(dr1.Item("Name").ToString())
            txtName.AutoCompleteCustomSource.Add(dr1.Item("Name").ToString())
            txtKind.AutoCompleteCustomSource.Add(dr1.Item("KindAtt").ToString())
            txtSub.AutoCompleteCustomSource.Add(dr1.Item("Subject").ToString())
            txtEnqNo.AutoCompleteCustomSource.Add(dr1.Item("Enq_No").ToString())
        Next
    End Sub

    Private Sub RblSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RblSingle.CheckedChanged
        SetCleanHeader()
        txtCapacity1.Visible = True
        txtCapacity2.Visible = False
        txtCapacity1_Leave(sender, e)


    End Sub

    Private Sub RblMultiple_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RblMultiple.CheckedChanged
        SetCleanHeader()
        txtCapacity1.Visible = True
        txtCapacity2.Visible = True
        txtCapacity2_Leave(sender, e)


    End Sub

    Private Sub RblOther_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RblOther.CheckedChanged
        SetCleanHeader()
        txtCapacity1.Visible = True
        txtCapacity2.Visible = False

        txtCapacity1_Leave(sender, e)




    End Sub

    Public Sub SetCleanHeader()
        txtContent1.Text = ""
        txtPrice_11.Text = ""
        txtPrice_21.Text = ""
        txtPrice_31.Text = ""
        lblHeader.Text = ""
        lblHeader2.Text = ""
        lblHeader3.Text = ""

    End Sub

    Private Sub txtCapacity1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCapacity1.Leave
        If RblSingle.Checked = True Then
            lblHeader.Text = txtCapacity1.Text
            lblHeader.Visible = True
            lblHeader2.Visible = False
            lblHeader3.Visible = False

            GroupBox4.Visible = True
            GroupBox5.Visible = False
            GroupBox6.Visible = False
            If RblSingle.Checked = True Then
                Dim totalcapacity As Int64
                If txtCapacity1.Text.Trim() <> "" Then
                    totalcapacity = Convert.ToInt32(txtCapacity1.Text) * 20
                    If RdbEnglish.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    ElseIf RdbGujarati.Checked Then
                        txtCapacityWord.Text = ("ક્ષમતા: " + txtCapacity1.Text + " લિટર/કલાક. . . . " + totalcapacity.ToString() + " લિટર/દિવસ.").ToString()
                    ElseIf RdbHindi.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    ElseIf RdbMarathi.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    ElseIf RdbTamil.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    ElseIf RdbTelugu.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    End If
                End If
            End If


            GetTechnicalData()
            GvSingle_Bind()
        End If

        If RblOther.Checked = True Then
            lblHeader.Text = "Qty"
            lblHeader2.Text = "Price"
            lblHeader3.Text = "Total"
            lblHeader.Visible = True
            lblHeader2.Visible = True
            lblHeader3.Visible = True
            GroupBox4.Visible = True
            GroupBox5.Visible = True
            GroupBox6.Visible = True
            If RblOther.Checked = True Then
                Dim totalcapacity As Int64
                If txtCapacity1.Text.Trim() <> "" Then
                    totalcapacity = Convert.ToInt32(txtCapacity1.Text) * 20
                    If RdbEnglish.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    ElseIf RdbGujarati.Checked Then
                        txtCapacityWord.Text = ("ક્ષમતા: " + txtCapacity1.Text + " લિટર/કલાક. . . . " + totalcapacity.ToString() + " લિટર/દિવસ.").ToString()
                    ElseIf RdbHindi.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    ElseIf RdbMarathi.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    ElseIf RdbTamil.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    ElseIf RdbTelugu.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                    End If
                End If
            End If
            GetTechnicalData()
            GvSingle_Bind()
        End If



    End Sub

    Private Sub txtCapacity2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCapacity2.Leave
        If RblMultiple.Checked = True Then
            lblHeader.Text = txtCapacity2.Text
            lblHeader2.Text = txtCapacity1.Text
            lblHeader.Visible = True
            lblHeader2.Visible = True
            lblHeader3.Visible = False

            GroupBox4.Visible = True
            GroupBox5.Visible = True
            GroupBox6.Visible = False

            If RblMultiple.Checked = True Then
                Dim totalcapacity1 As Int64
                Dim totalcapacity2 As Int64

                If txtCapacity1.Text.Trim() <> "" And txtCapacity2.Text.Trim() <> "" Then
                    totalcapacity1 = Convert.ToInt64(txtCapacity1.Text) * 20
                    totalcapacity2 = Convert.ToInt64(txtCapacity2.Text) * 20

                    Dim Newline As String
                    Newline = System.Environment.NewLine
                    If RdbEnglish.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity1.ToString() + " LITER/DAY" & Newline & "CAPACITY: " + txtCapacity2.Text + "  LITER/HR . . . . " + totalcapacity2.ToString() + " LITER/DAY  ").ToString()
                    ElseIf RdbGujarati.Checked Then
                        txtCapacityWord.Text = ("ક્ષમતા: " + txtCapacity1.Text + " લિટર/કલાક. . . . " + totalcapacity1.ToString() + " લિટર/દિવસ." & Newline & "ક્ષમતા: " + txtCapacity2.Text + "  લિટર/કલાક. . . . " + totalcapacity2.ToString() + "લિટર/દિવસ  ").ToString()
                    ElseIf RdbHindi.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity1.ToString() + " LITER/DAY" & Newline & "CAPACITY: " + txtCapacity2.Text + "  LITER/HR . . . . " + totalcapacity2.ToString() + " LITER/DAY  ").ToString()
                    ElseIf RdbMarathi.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity1.ToString() + " LITER/DAY" & Newline & "CAPACITY: " + txtCapacity2.Text + "  LITER/HR . . . . " + totalcapacity2.ToString() + " LITER/DAY  ").ToString()
                    ElseIf RdbTamil.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity1.ToString() + " LITER/DAY" & Newline & "CAPACITY: " + txtCapacity2.Text + "  LITER/HR . . . . " + totalcapacity2.ToString() + " LITER/DAY  ").ToString()
                    ElseIf RdbTelugu.Checked Then
                        txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity1.ToString() + " LITER/DAY" & Newline & "CAPACITY: " + txtCapacity2.Text + "  LITER/HR . . . . " + totalcapacity2.ToString() + " LITER/DAY  ").ToString()
                    End If
                End If
            End If
            GetTechnicalData()
            GvMultiple_Bind()
        End If

    End Sub

    Public Function TotalTaxCreate(ByVal strPrice1 As String, ByVal strPrice2 As String, ByVal Qty As String) As String()
        Dim total(2) As String
        Try

            If RblSingle.Checked = True Then

                Dim finalamount As Decimal
                Dim rate1 As Decimal
                Dim tax As Decimal
                rate1 = 0
                tax = 0
                finalamount = 0
                rate1 = Convert.ToDecimal(strPrice1).ToString()
                tax = (rate1 * (Convert.ToDecimal(TxtTax.Text))) / 100
                finalamount = rate1 + tax
                total(0) = finalamount.ToString("N2")

            End If
            If RblOther.Checked = True Then

                Dim finalamount As Decimal
                Dim rate1 As Decimal
                Dim tax As Decimal
                rate1 = 0
                tax = 0
                finalamount = 0
                rate1 = Convert.ToDecimal(strPrice1.ToString())
                tax = (rate1 * Convert.ToDecimal(TxtTax.Text)) / 100
                finalamount = rate1 + tax
                total(0) = finalamount.ToString("N2")
                total(2) = (Convert.ToDecimal(Qty) * finalamount).ToString("N2")
                total(1) = (Convert.ToDecimal(Qty)).ToString("N2")
            End If

            If RblMultiple.Checked = True Then

                Dim finalamount1 As Decimal
                Dim finalamount2 As Decimal
                Dim rate1 As Decimal
                Dim rate2 As Decimal
                Dim tax1 As Decimal
                Dim tax2 As Decimal
                rate1 = 0
                rate2 = 0
                tax1 = 0
                tax2 = 0
                finalamount1 = 0
                finalamount2 = 0
                rate1 = Convert.ToDecimal(strPrice1)
                tax1 = (rate1 * Convert.ToDecimal(TxtTax.Text) / 100)
                finalamount1 = rate1 + tax1
                total(0) = finalamount1.ToString("N2")
                rate2 = Convert.ToDecimal(strPrice2)
                tax2 = (rate2 * Convert.ToDecimal(TxtTax.Text) / 100)
                finalamount2 = rate2 + tax2
                total(1) = finalamount2.ToString("N2")

            End If
        Catch ex As Exception

        End Try
        Return total
    End Function

    Public Sub GvSingle_Bind()
        setLanguageId()
        Dim StrArray() As String
        If txtNoContent.Text.Trim() <> "" Then

            If RblSingle.Checked = True Then
                dt = New DataTable()
                dt.Columns.Add("Remove", GetType(Boolean))
                dt.Columns.Add("Select", GetType(Boolean))
                dt.Columns.Add("SrNo", GetType(Int32))
                dt.Columns.Add("Category", GetType(String))
                dt.Columns.Add("Price", GetType(String))
                dt.Columns.Add("Tax", GetType(String))

            ElseIf RblOther.Checked = True Then
                dt = New DataTable()
                dt.Columns.Add("Remove", GetType(Boolean))
                dt.Columns.Add("Select", GetType(Boolean))
                dt.Columns.Add("SrNo", GetType(Int32))
                dt.Columns.Add("Category", GetType(String))
                dt.Columns.Add("Price", GetType(String))
                dt.Columns.Add("Qty", GetType(String))
                dt.Columns.Add("Total", GetType(String))
                dt.Columns.Add("Tax", GetType(String))

            End If

            Dim str As String
            str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity1.Text & "' and MainCategory='" + txtQoutType.Text.Trim() + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)


            If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContent.Text) Then
                For i As Integer = 0 To Convert.ToInt32(txtNoContent.Text) - 1
                    If RblSingle.Checked = True Then
                        StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "0", "0")

                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)

                    ElseIf RblOther.Checked = True Then
                        StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "0", "1")
                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text)
                    End If

                Next
                If dt Is Nothing Then
                Else
                    If dt.Rows.Count > 0 Then
                        Dim dView As New DataView(dt)
                        dView.Sort = "SrNo ASC"
                        dt = dView.ToTable()
                    End If
                End If
                GvTechnical.DataSource = dt
                '   lblSno.Text = GvTechnical.RowCount + 1
                da.Dispose()
                ds.Dispose()
                Total1()
            Else
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If RblSingle.Checked = True Then
                        StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "0", "1")
                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)
                        'dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)

                    ElseIf RblOther.Checked = True Then
                        StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "0", "1")

                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text)
                    End If
                Next
                If dt Is Nothing Then
                Else
                    If dt.Rows.Count > 0 Then
                        Dim dView As New DataView(dt)
                        dView.Sort = "SrNo ASC"
                        dt = dView.ToTable()
                    End If
                End If
                GvTechnical.DataSource = dt
                '  lblSno.Text = GvTechnical.RowCount + 1
                da.Dispose()
                ds.Dispose()
                Total1()
            End If

        End If


    End Sub

    Public Sub GvMultiple_Bind()
        setLanguageId()
        If txtNoContent.Text.Trim() <> "" Then
            dt = New DataTable()

            dt.Columns.Add("Remove", GetType(Boolean))
            dt.Columns.Add("Select", GetType(Boolean))
            dt.Columns.Add("SrNo", GetType(Int32))
            dt.Columns.Add("Category", GetType(String))
            dt.Columns.Add("Price1", GetType(String))
            dt.Columns.Add("Price2", GetType(String))
            dt.Columns.Add("Tax", GetType(String))

            Dim str As String
            Dim str1 As String
            If txtCapacity2.Text.Trim() <> "" And txtCapacity1.Text.Trim() <> "" Then


                str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity1.Text & "' and MainCategory='" + txtQoutType.Text.Trim() + "'  and LanguageId = " & LanguageId & " ORDER BY SNo"

                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)


                str1 = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity2.Text & "' and MainCategory='" + txtQoutType.Text.Trim() + "'  and LanguageId = " & LanguageId & " ORDER BY SNo"
                da1 = New SqlDataAdapter(str1, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                Dim StrArray() As String
                If ds.Tables(0).Rows.Count >= ds1.Tables(0).Rows.Count Then

                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContent.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContent.Text) - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "0")
                                dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
                            End If

                        Next
                        If dt Is Nothing Then
                        Else
                            If dt.Rows.Count > 0 Then
                                Dim dView As New DataView(dt)
                                dView.Sort = "SrNo ASC"
                                dt = dView.ToTable()
                            End If
                        End If
                        GvTechnical.DataSource = dt
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "0")
                                dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
                            End If
                        Next
                        If dt Is Nothing Then
                        Else
                            If dt.Rows.Count > 0 Then
                                Dim dView As New DataView(dt)
                                dView.Sort = "SrNo ASC"
                                dt = dView.ToTable()
                            End If
                        End If
                        GvTechnical.DataSource = dt
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If
                Else
                    MessageBox.Show("Technical Data Not Match")

                End If
            End If

        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        '' add entry in grid how using sql datasource
        Try
            Dim t As Int16
            Dim taxData As Integer
            If TxtTax.Text = "" Then
                taxData = 0
            Else
                taxData = Convert.ToDecimal(TxtTax.Text)
            End If

            'Added by rajesh to check blank data.

            dt = GvTechnical.DataSource

            t = GvTechnical.Rows.Count

            Dim a As Integer

            a = dt.Columns.IndexOf("Tax")

            If a > 0 Then

            Else
                dt.Columns.Add("Tax")
            End If

            t = t + 1

            If RblSingle.Checked = True Then
                dt.Rows.Add(0, 0, lblSno.Text, txtContent1.Text, txtPrice_11.Text, taxData.ToString())
            ElseIf RblOther.Checked = True Then
                dt.Rows.Add(0, 0, Convert.ToInt32(lblSno.Text), txtContent1.Text, txtPrice_21.Text, txtPrice_11.Text, txtPrice_31.Text, taxData.ToString())
            ElseIf RblMultiple.Checked = True Then
                dt.Rows.Add(0, 0, lblSno.Text, txtContent1.Text, txtPrice_21.Text, txtPrice_11.Text, taxData.ToString())
            End If


            If dt Is Nothing Then
            Else
                If dt.Rows.Count > 0 Then
                    Dim dView As New DataView(dt)
                    dView.Sort = "SrNo ASC"
                    dt = dView.ToTable()
                End If
            End If
            GvTechnical.DataSource = dt
            Total1()
            txtContent1.Text = ""
            txtPrice_11.Text = ""
            txtPrice_21.Text = ""
            txtPrice_31.Text = ""
            '   lblSno.Text = GvTechnical.RowCount + 1
            Try
                If TxtTax.Text = "" Then
                    GvTechnical.Columns("Tax").Visible = False
                Else
                    GvTechnical.Columns("Tax").Visible = False
                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtContent1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContent1.Leave
        Dim str As String
        Dim ds1 As DataSet
        Dim da1 As SqlDataAdapter

        If txtContent1.Text.Trim() <> "" Then

            If RblSingle.Checked = True Or RblOther.Checked = True Or RblMultiple.Checked = True Then
                str = "select * from Category_Master (NOLOCK) where Category='" + txtContent1.Text.Trim() + "' and Capacity='" + txtCapacity1.Text.Trim() + "' and MainCategory='" + txtQoutType.Text + "'"
                da1 = New SqlDataAdapter(str, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)
                If (ds1.Tables(0).Rows.Count > 0) Then


                    If RblSingle.Checked = True Then
                        txtPrice_11.Text = Convert.ToDecimal(ds1.Tables(0).Rows(0)("Price")).ToString("N2")
                        lblSno.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                    ElseIf RblOther.Checked = True Then
                        txtPrice_21.Text = Convert.ToDecimal(ds1.Tables(0).Rows(0)("Price")).ToString("N2")
                        lblSno.Text = ds1.Tables(0).Rows(0)("SNo").ToString()

                    ElseIf RblMultiple.Checked = True Then
                        Dim da2 As SqlDataAdapter
                        Dim ds2 As DataSet

                        str = "select * from Category_Master (NOLOCK) where Category='" + txtContent1.Text.Trim() + "' and Capacity='" + txtCapacity2.Text.Trim() + "' and MainCategory='" + txtQoutType.Text + "'"
                        da2 = New SqlDataAdapter(str, con1)
                        ds2 = New DataSet()
                        da2.Fill(ds2)
                        'Capacity 1'
                        If (ds1.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtPrice_21.Text = Convert.ToDecimal(ds1.Tables(0).Rows(0)("Price")).ToString("N2")
                                lblSno.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                            End If
                        End If
                        'capacity 2'

                        If (ds2.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtPrice_11.Text = Convert.ToDecimal(ds2.Tables(0).Rows(0)("Price")).ToString("N2")

                            End If
                        End If
                        da1.Dispose()
                        ds1.Dispose()
                        da2.Dispose()
                        ds2.Dispose()
                    End If

                End If
            End If
        End If





    End Sub

    Public Sub Total1()
        Dim total As Decimal
        Dim total1 As Decimal
        total = 0
        total1 = 0


        For i As Integer = 0 To GvTechnical.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTechnical.Rows(i).Cells(0).Value)
            If IsTicked Then
            Else
                If GvTechnical.Rows.Count > 0 Then


                    If RblSingle.Checked = True Then



                        total = total + Convert.ToDecimal(GvTechnical.Rows(i).Cells(4).Value)
                        txtTotal.Text = total.ToString()
                        txtTotal1.Visible = False
                        'Navin 19-3-2014 N2
                        lblAllsysTotal.Text = total.ToString("N2")
                    ElseIf RblOther.Checked = True Then

                        total = total + (Convert.ToDecimal(GvTechnical.Rows(i).Cells(4).Value) * Convert.ToDecimal(GvTechnical.Rows(i).Cells(6).Value))
                        txtTotal.Text = total.ToString()
                        txtTotal1.Visible = False
                        'Navin 19-3-2014 N2

                        lblAllsysTotal.Text = total.ToString("N2")

                    ElseIf RblMultiple.Checked = True Then

                        total = total + Convert.ToDecimal(GvTechnical.Rows(i).Cells(4).Value)
                        txtTotal.Text = total.ToString()
                        total1 = total1 + Convert.ToDecimal(GvTechnical.Rows(i).Cells(5).Value)
                        txtTotal1.Text = total1.ToString()
                        txtTotal1.Visible = True

                        'Navin 19-3-2014 N2

                        lblAllsysTotal.Text = total.ToString("N2")
                        lblAllMulSysTotal.Text = total1.ToString("N2")

                    End If

                End If

            End If
        Next

        TotalLeave()

    End Sub

    Public Sub TotalLeave()

        Dim disc As Decimal
        Dim Vat As Decimal
        Dim ISI As Decimal
        Dim Transporation As Decimal
        Dim Insuarnce As Decimal
        Dim Packing As Decimal
        Dim Erection As Decimal
        Dim AllMulSysTotal As Decimal
        Dim AllSysTotal As Decimal
        disc = 0
        Vat = 0
        ISI = 0
        Transporation = 0
        Insuarnce = 0
        Packing = 0
        Erection = 0
        AllMulSysTotal = 0
        If IsNumeric(txtspdiscount.Text) Then
            disc = Convert.ToDecimal(txtspdiscount.Text)
        End If
        If IsNumeric(txtVat.Text) Then
            Vat = Convert.ToDecimal(txtVat.Text)
        End If
        If IsNumeric(txtTransporation.Text) Then
            Transporation = Convert.ToDecimal(txtTransporation.Text)
        End If
        If IsNumeric(txtInsurance.Text) Then
            Insuarnce = Convert.ToDecimal(txtInsurance.Text)
        End If
        If IsNumeric(txtPakingforwarding.Text) Then
            Packing = Convert.ToDecimal(txtPakingforwarding.Text)
        End If

        If IsNumeric(txtErection.Text) Then
            Erection = Convert.ToDecimal(txtErection.Text)
        End If
        TotalLeavevar = Vat + ISI + Transporation + Insuarnce + Packing + Erection - disc
        'lblAllsysTotal.Text = Convert.ToString(AllSysTotal)
        'lblAllMulSysTotal.Text = Convert.ToString(AllMulSysTotal)

        If IsNumeric(lblAllsysTotal.Text) Then
            TotalLeavevar = TotalLeavevar + lblAllsysTotal.Text
        End If
        If IsNumeric(lblAllMulSysTotal.Text) Then
            TotalLeavevar = TotalLeavevar + lblAllMulSysTotal.Text
        End If
        'navin 19-03-2014
        'txtFinalPrice.Text =  TotalLeavevar
        txtFinalPrice.Text = TotalLeavevar.ToString("N2")
    End Sub

    Private Sub GvTechnical_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvTechnical.CellValueChanged
        Total1()
    End Sub

    Private Sub txtNoContent_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoContent.Leave
        GvSingle_Bind()
    End Sub

    Private Sub txtPrice_11_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice_11.Leave
        If RblOther.Checked = True Then
            If (txtPrice_11.Text.Trim() <> "" And txtPrice_21.Text.Trim() <> "") Then
                txtPrice_31.Text = (Convert.ToDecimal(txtPrice_11.Text) * Convert.ToDecimal(txtPrice_21.Text)).ToString()
            End If

        End If
    End Sub

    Public Sub setLanguageId()

        If RdbEnglish.Checked Then
            LanguageId = 1
        ElseIf RdbGujarati.Checked Then
            LanguageId = 2
        ElseIf RdbHindi.Checked Then
            LanguageId = 3
        ElseIf RdbMarathi.Checked Then
            LanguageId = 4
        ElseIf RdbTamil.Checked Then
            LanguageId = 5
        ElseIf RdbTelugu.Checked Then
            LanguageId = 6
        End If
    End Sub

    Private Sub btnSave1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave1.Click
        'If PicDefault.Image Is Nothing Then
        '    MessageBox.Show("Please Select Default Image...")
        'Else

        If rblNew.Checked = True Then

            Dim QStatus As String

            If Fk_SalesExecutiveQtnID > 0 Then

                If rblDone.Checked = True Then
                    QStatus = "Done"
                ElseIf rblPending.Checked = True Then
                    QStatus = "Pending"
                Else
                    QStatus = "Cancel"

                End If
                linq_obj.SP_Update_SalesExecutiveQuotation_Status(Fk_SalesExecutiveQtnID, QStatus.Trim())
                linq_obj.SubmitChanges()
            End If
        End If


        Dim StrImageLocation As String
        If PicDefault.Image Is Nothing Then
            StrImageLocation = ""
        Else
            StrImageLocation = PicDefault.ImageLocation.ToString()
        End If

        Dim str As String
        Dim techinical12 As String
        Dim QMaxId As Int32
        Try
            con1.Close()
            con1.Open()
            If RblSingle.Checked = True Then
                capacityType = RblSingle.Text
                txtCapacity2.Text = "0"

            ElseIf RblMultiple.Checked = True Then
                capacityType = RblMultiple.Text
            ElseIf RblOther.Checked = True Then

                capacityType = RblOther.Text
                txtCapacity2.Text = "0"
            End If

            str = ""
            Dim year1 As Int32
            year1 = Convert.ToInt32(txtDate.Text.Substring(txtDate.Text.Length - 2))
            If btnSave1.Text = "Update" Then
                str = "update Quotation_Master  Set QType ='" + txtType.Text + "',Fk_EnqTypeID  ='" & ddlEnqType.SelectedValue & "',Quot_No =" & QuotationMaxId & ",Q_Year =" & year1 & "," + _
Environment.NewLine + _
                "Enq_No ='" + txtEnqNo.Text + "'," + _
Environment.NewLine + _
               "Ref='" + txtRef.Text + "'," + _
Environment.NewLine + _
                "Quot_Type='" + txtQoutType.Text + "'," + _
Environment.NewLine + _
                "Name='" + txtName.Text + "'," + _
Environment.NewLine + _
                "Address='" + txtAddress.Text + "', " + _
Environment.NewLine + _
                "Capacity_Type='" + capacityType.ToString() + "'," + _
Environment.NewLine + _
                "Capacity_Single='" + txtCapacity1.Text + "'," + _
Environment.NewLine + _
                "Capacity_Multiple='" + txtCapacity2.Text + "'," + _
Environment.NewLine + _
                "KindAtt='" + txtKind.Text + "'," + _
Environment.NewLine + _
                "Subject='" + txtSub.Text + "'," + _
Environment.NewLine + _
                "Buss_Excecutive='" + txtBussness_Exe.Text + "'," + _
Environment.NewLine + _
                "Buss_Name='" + txtBuss_Name.Text + "'," + _
Environment.NewLine + _
                "Later_Description='" + txtDescription.Text + "'," + _
Environment.NewLine + _
                "Later_Date= '" + txtLatterDate.Text + "'," + _
Environment.NewLine + _
                "Capacity_Word='" + txtCapacityWord.Text + "'," + _
Environment.NewLine + _
                "UserName='" + Class1.global.UserName.ToString() + "'," + _
Environment.NewLine + _
                "DefaultImage='" + StrImageLocation.ToString() + "'," + _
Environment.NewLine + _
                "QDate='" + txtDate.Text + "' where Pk_QuotationID =" & QuationId & ""
                cmd = New SqlCommand(str, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Else

                If RdbEnglish.Checked Then
                    LanguageId = 1
                ElseIf RdbGujarati.Checked Then
                    LanguageId = 2
                ElseIf RdbHindi.Checked Then
                    LanguageId = 3
                ElseIf RdbMarathi.Checked Then
                    LanguageId = 4
                ElseIf RdbTamil.Checked Then
                    LanguageId = 5
                ElseIf RdbTelugu.Checked Then
                    LanguageId = 6
                End If
                str = "insert into Quotation_Master (QType,Fk_EnqTypeID,Quot_No,Q_Year,Enq_No,Ref,Quot_Type,Name,Address,Capacity_Type,Capacity_Single,Capacity_Multiple,KindAtt,Subject,Buss_Excecutive,Buss_Name,Later_Description,Later_Date,Capacity_Word,UserName,DefaultImage,QDate,Quatation_Type,LanguageId,Fk_AddressId) values('" + txtType.Text + "','" & ddlEnqType.SelectedValue & "'," & QuotationMaxId & "," & year1 & ",'" + txtEnqNo.Text + "','" + txtRef.Text + "','" + txtQoutType.Text + "','" + txtName.Text + "','" + txtAddress.Text + "', '" + capacityType.ToString() + "','" + txtCapacity1.Text + "','" + txtCapacity2.Text + "','" + txtKind.Text + "','" + txtSub.Text + "','" + txtBussness_Exe.Text + "','" + txtBuss_Name.Text + "','" + txtDescription.Text + "','" + txtLatterDate.Text + "','" + txtCapacityWord.Text + "','" + Class1.global.UserName.ToString() + "','" + StrImageLocation.ToString() + "','" + txtDate.Text + "','" + "NON ISI" + "'," & LanguageId & "," & Address_ID & ")"
                cmd = New SqlCommand(str, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

            End If


            'str = "insert into Quotation_Master (QType,Fk_EnqTypeID,Quot_No,Q_Year,Enq_No,Ref,Quot_Type,Name,Address,Capacity_Type,Capacity_Single,Capacity_Multiple,KindAtt,Subject,Buss_Excecutive,Buss_Name,Later_Description,Later_Date,Capacity_Word,UserName,DefaultImage,QDate) values('" + txtType.Text + "','" & ddlEnqType.SelectedValue & "'," & QuotationMaxId & "," & year1 & ",'" + txtEnqNo.Text + "','" + txtRef.Text + "','" + txtQoutType.Text + "','" + txtName.Text + "','" + txtAddress.Text + "', '" + capacityType.ToString() + "','" + txtCapacity1.Text + "','" + txtCapacity2.Text + "','" + txtKind.Text + "','" + txtSub.Text + "','" + txtBussness_Exe.Text + "','" + txtBuss_Name.Text + "','" + txtDescription.Text + "','" + txtLatterDate.Text + "','" + txtCapacityWord.Text + "','" + Class1.global.UserName.ToString() + "','" + PicDefault.ImageLocation.ToString() + "','" + txtDate.Text + "')"
            'cmd = New SqlCommand(str, con1)
            'cmd.ExecuteNonQuery()
            'cmd.Dispose()

            Dim mm As String
            mm = "select Max(Pk_QuotationID) as QMax from Quotation_Master (NOLOCK)"
            cmd = New SqlCommand(mm, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            QMaxId = dr("QMax").ToString()
            cmd.Dispose()
            dr.Dispose()




            If btnSave1.Text = "Update" Then

                techinical12 = "delete from Technical_Data where Fk_QuotationID =" & QuationId & ""
                cmd = New SqlCommand(techinical12, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Else
                Dim strPdfInsert As String
                strPdfInsert = "insert into PDFGenerate_Check(FK_QuatationID,RefNo,IsCreated) values(" & QMaxId & ",'" + txtRef.Text + "','" + "No" + "')"
                cmd = New SqlCommand(strPdfInsert, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            End If


            If btnSave1.Text = "Update" Then
                techinical12 = "delete from Discount_master where Fk_QuotationID =" & QuationId & ""
                cmd = New SqlCommand(techinical12, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                Dim strPdfInsert12 As String
                strPdfInsert12 = "insert into Discount_master(Fk_QuotationID,SpecialDisc,Vat,ISIFee,Transportation,Insurance,Packing,Erection,FinalTotal) values(" & QuationId & ",'" + txtspdiscount.Text + "','" + txtVat.Text + "','" + txtISI.Text + "','" + txtTransporation.Text + "','" + txtInsurance.Text + "','" + txtPakingforwarding.Text + "','" + txtErection.Text + "','" + txtFinalPrice.Text + "')"
                cmd = New SqlCommand(strPdfInsert12, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Else
                Dim strPdfInsert1 As String
                strPdfInsert1 = "insert into Discount_master(Fk_QuotationID,SpecialDisc,Vat,ISIFee,Transportation,Insurance,Packing,Erection,FinalTotal) values(" & QMaxId & ",'" + txtspdiscount.Text + "','" + txtVat.Text + "','" + txtISI.Text + "','" + txtTransporation.Text + "','" + txtInsurance.Text + "','" + txtPakingforwarding.Text + "','" + txtErection.Text + "','" + txtFinalPrice.Text + "')"
                cmd = New SqlCommand(strPdfInsert1, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

            End If

            For i As Integer = 0 To GvTechnical.Rows.Count - 1

                Dim RemoveStatus As Boolean = CBool(GvTechnical.Rows(i).Cells(0).Value)
                If RemoveStatus Then

                Else

                    Dim status As String
                    Dim selectStatus As Boolean = CBool(GvTechnical.Rows(i).Cells(1).Value)
                    status = ""

                    If selectStatus Then
                        status = "Yes"
                    Else
                        status = "No"

                    End If

                    Dim MainCategory As String
                    MainCategory = "NON ISI"

                    If btnSave1.Text = "Update" Then
                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,MainCategory) values(" & QuationId & ",'" + GvTechnical.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,MainCategory) values(" & QuationId & ",'" + GvTechnical.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,MainCategory) values(" & QuationId & ",'" + GvTechnical.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If
                    Else
                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,MainCategory) values(" & QMaxId & ",'" + GvTechnical.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,MainCategory) values(" & QMaxId & ",'" + GvTechnical.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,MainCategory) values(" & QMaxId & ",'" + GvTechnical.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If

                    End If
                End If
            Next

            con1.Close()
            BindOnLanguageName()
            'GvQuotationSearch_Bind(LanguageId)
            MessageBox.Show("Successfully Submit .....")

            btnSave1.Text = "Update"


        Catch ex As Exception

        End Try



    End Sub

    Private Sub txtEnqType_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim quotNo As String

        con1.Open()




    End Sub

    Private Sub ddlEnqType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEnqType.SelectionChangeCommitted
        Dim str1 As String
        Try
            str1 = "SELECT Quot_No FROM Quotation_Master where Fk_EnqTypeID='" & ddlEnqType.SelectedValue & "' order by Pk_QuotationID desc "
            da = New SqlDataAdapter(str1, con1)
            ds = New DataSet()
            da.Fill(ds)
            Dim t As Int32
            t = ds.Tables(0).Rows.Count()

            If (ds.Tables(0).Rows.Count > 0) Then

                If (ds.Tables(0).Rows(0)("Quot_No").ToString() <> "") Then
                    QuotationMaxId = Convert.ToInt32(ds.Tables(0).Rows(0)("Quot_No").ToString()) + 1
                    txtQoutNo.Text = ddlEnqType.SelectedValue + QuotationMaxId.ToString()
                End If
            Else
                QuotationMaxId = 1
                txtQoutNo.Text = ddlEnqType.SelectedValue + QuotationMaxId.ToString()
            End If
            da.Dispose()
            ds.Dispose()


        Catch ex As Exception

        Finally

        End Try

    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        Dim Ref As String

        Dim str As String
        'txtName.Text = ""
        'txtAddress.Text = ""

        If (txtEnqNo.Text.Trim() <> "") Then

            con1.Open()
            EnqMax = 0
            Dim Claient = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text.Trim())
            For Each item As SP_Get_AddressListByEnqNoResult In Claient
                Address_ID = item.Pk_AddressID
                txtName.Text = item.Name
                txtAddress.Text = item.Address + "," + item.City + "," + item.State
                txtEnqNo.Text = item.EnqNo
            Next

            Dim year1 As Int32
            year1 = Convert.ToInt32(txtDate.Text.Substring(txtDate.Text.Length - 2))

            str = "select count(Enq_No) as TotalCount from Quotation_Master (NOLOCK) where Enq_No='" & txtEnqNo.Text & "' and Q_Year=" & year1 & " "
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            If (dr.HasRows) Then
                dr.Read()
                If (dr("TotalCount").ToString() <> "") Then

                    EnqMax = Convert.ToInt32(dr("TotalCount").ToString()) + 1
                End If
            Else
                EnqMax = 1

            End If
            cmd.Dispose()
            dr.Dispose()
            con1.Close()


            Ref = "IIECL-Q / " + Class1.global.User.ToString() + " / " + txtEnqNo.Text + " - " + EnqMax.ToString() + " / " + year1.ToString() + ""
            txtRef.Text = Ref.ToString()
            con1.Close()

            con1.Open()
            If btnSave1.Text <> "Update" Then

                str = "select Address,Name from Quotation_Master (NOLOCK) where Enq_No='" + txtEnqNo.Text.Trim() + "' "
                cmd = New SqlCommand(str, con1)
                dr = cmd.ExecuteReader()
                If (dr.HasRows) Then
                    dr.Read()
                    If (dr("Address").ToString() <> "") Then
                        txtAddress.Text = dr("Address").ToString()
                        txtName.Text = dr("Name").ToString()

                    End If
                    If (dr("Name").ToString() <> "") Then
                        txtName.Text = dr("Name").ToString()
                    End If
                End If
                cmd.Dispose()
                dr.Dispose()
                con1.Close()

            End If



        End If

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String


        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
        PicDefault.ImageLocation = imgSrc
        PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
        'txtPhoto1.Text = imgSrc
        'Path1 = openFileDialog1.FileName
    End Sub

    Private Sub txtName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.Leave


    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If dt.Rows.Count > 0 Then
            dt.Dispose()
            GvTechnical.DataSource = dt
            da.Dispose()
            ds.Dispose()



        End If
    End Sub


    Private Sub txtTax_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If RblSingle.Checked = True Then





            Total1()
        End If

    End Sub

    Private Sub GvTechnical_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvTechnical.CellEndEdit


        If RblOther.Checked = True Then


            If e.ColumnIndex = 5 Then

                GvTechnical.Rows(e.RowIndex).Cells(6).Value = (Convert.ToDecimal(GvTechnical.Rows(e.RowIndex).Cells(4).Value) * Convert.ToDecimal(GvTechnical.Rows(e.RowIndex).Cells(5).Value)).ToString("N2")

            End If

        End If

        If RblSingle.Checked = True Then
            If e.ColumnIndex = 5 Then
                Dim finalamount As Decimal
                Dim rate1 As Decimal
                Dim tax As Decimal
                rate1 = 0
                tax = 0
                finalamount = 0
                rate1 = Convert.ToDecimal(Me.GvTechnical.Rows(e.RowIndex).Cells(4).Value.ToString())
                tax = (rate1 * Convert.ToInt32(GvTechnical.Rows(e.RowIndex).Cells(5).Value)) / 100
                finalamount = rate1 + tax
                GvTechnical.Rows(e.RowIndex).Cells(4).Value = finalamount.ToString("N2")
            End If
        End If
        If RblOther.Checked = True Then
            If e.ColumnIndex = 7 Then
                Dim finalamount As Decimal
                Dim rate1 As Decimal
                Dim tax As Decimal
                rate1 = 0
                tax = 0
                finalamount = 0
                rate1 = Convert.ToDecimal(Me.GvTechnical.Rows(e.RowIndex).Cells(6).Value.ToString())
                tax = (rate1 * Convert.ToInt32(GvTechnical.Rows(e.RowIndex).Cells(7).Value)) / 100
                finalamount = rate1 + tax
                Dim totalcalc As Decimal
                totalcalc = Convert.ToDecimal(Convert.ToDecimal(GvTechnical.Rows(e.RowIndex).Cells("Qty").Value) * finalamount)
                GvTechnical.Rows(e.RowIndex).Cells("Price").Value = finalamount.ToString("N2")

                GvTechnical.Rows(e.RowIndex).Cells(6).Value = totalcalc.ToString("N2")
                'GvTechnical.Rows(e.RowIndex).Cells(6).Value = Convert.ToDecimal(GvTechnical.Rows(e.RowIndex).Cells(4).Value) * Convert.ToDecimal(GvTechnical.Rows(e.RowIndex).Cells(5).Value)
            End If

        End If

        If RblMultiple.Checked = True Then

            If e.ColumnIndex = 6 Then
                Dim finalamount1 As Decimal
                Dim finalamount2 As Decimal
                Dim rate1 As Decimal
                Dim rate2 As Decimal
                Dim tax1 As Decimal
                Dim tax2 As Decimal
                rate1 = 0
                rate2 = 0
                tax1 = 0
                tax2 = 0
                finalamount1 = 0
                finalamount2 = 0
                rate1 = Convert.ToDecimal(Me.GvTechnical.Rows(e.RowIndex).Cells(4).Value.ToString())
                tax1 = (rate1 * Convert.ToInt32(GvTechnical.Rows(e.RowIndex).Cells(6).Value)) / 100
                finalamount1 = rate1 + tax1
                GvTechnical.Rows(e.RowIndex).Cells(4).Value = finalamount1.ToString("N2")
                rate2 = Convert.ToDecimal(Me.GvTechnical.Rows(e.RowIndex).Cells(5).Value.ToString())
                tax2 = (rate2 * Convert.ToInt32(GvTechnical.Rows(e.RowIndex).Cells(6).Value)) / 100
                finalamount2 = rate2 + tax2
                GvTechnical.Rows(e.RowIndex).Cells(5).Value = finalamount2.ToString("N2")

            End If
        End If
    End Sub

    Protected Sub FinalDucumetation()

        Dim wordApplication As Word.Application
        Dim wordDocument As Word.Document
        Dim objApp12 As Word.Application
        Dim objDoc12 As Word.Document
        '    Try
        Dim missing As Object = System.Reflection.Missing.Value

        Dim Visible As Object = True

        Dim start1 As Object = 0

        Dim end1 As Object = 0
        Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        Dim objApp As New Word.Application()


        Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)

        objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        Dim oTable3 As Word.Tables = objDoc.Tables
        Dim defaultTableBehavior As [Object] = Type.Missing
        Dim autoFitBehavior As [Object] = Type.Missing
        Dim rng As Word.Range = objDoc.Range(start1, missing)
        oTable3.Add(rng, 5, 2, missing, missing)
        rng.Font.Name = "Times New Roman"
        Dim tbl As Word.Table = objDoc.Tables(1)
        tbl.Range.ParagraphFormat.SpaceAfter = 0
        '  tbl.Borders.Enable = 1
        For ikf = 1 To 5
            tbl.Cell(ikf, 1).Range.Font.Name = "Times New Roman"
        Next
        'For Each cel As Word.Cell In tbl.Range.Cells
        '    cel.TopPadding = 2
        'Next

        ' tbl.Range.ParagraphFormat.SpaceAfter = 3
        tbl.Cell(1, 1).Range.Text = "Ref :  " + txtRef.Text + " "
        tbl.Cell(1, 1).TopPadding = 10

        tbl.Cell(1, 1).Range.Font.Name = "Times New Roman"
        tbl.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        tbl.Cell(1, 2).Range.Text = "Date:" + txtDate.Text + " "
        tbl.Cell(1, 2).Range.Font.Name = "Times New Roman"
        tbl.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        tbl.Cell(1, 2).TopPadding = 3

        tbl.Cell(2, 1).Range.Text = ""
        tbl.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        tbl.Cell(3, 1).Range.Text = "TO,"
        tbl.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        tbl.Cell(4, 1).Range.Text = txtName.Text + " "
        tbl.Cell(4, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        tbl.Cell(5, 1).Range.Text = txtAddress.Text + " "
        tbl.Cell(5, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        Dim strt As Object = tbl.Range.[End]
        Dim oCollapseEnd As Object = Word.WdCollapseDirection.wdCollapseEnd

        Dim ran As Word.Range = objDoc.Range(strt, strt)
        rng = objDoc.Content
        rng.Collapse(oCollapseEnd)

        If RdbEnglish.Checked Then

            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 21, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0
            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "

            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text

            oTable.Cell(13, 1).Range.Text = ""

            oTable.Cell(14, 1).Range.Text = " In this regard, we are submitting herewith our offer for following capacities:"

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."

            oTable.Cell(19, 1).Range.Text = ""


            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(21, 1).Range.Text = ""


            oTable.Cell(22, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(23, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(24, 1).Range.Text = ""

            oTable.Cell(25, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(26, 1).Range.Text = " "
            oTable.Cell(27, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(28, 1).Range.Text = ""

            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            ' oTable.Rows.Item(12).Range.Font.Bold = True 'name'
            oTable.Rows.Item(26).Range.Font.Bold = True 'designation'
            ' oTable.Rows.Item(24).Range.Font.Bold = True
            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14
            oTable.Rows.Item(8).Range.Underline = True


            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 1, 1, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable51.Range.ParagraphFormat.SpaceAfter = 0



            strt3 = objDoc.Tables(1).Range.[End]
            oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 2, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0

        ElseIf RdbGujarati.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 14, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "પ્રતિ શ્રી:" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "વિષય:" + txtSub.Text + " "
            oTable.Cell(8, 1).Range.Font.Size = 9


            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "માનનીય,"
            oTable.Cell(10, 1).Range.Font.Size = 9


            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(11, 1).Range.Text = txtDescription.Text

            oTable.Cell(12, 1).Range.Text = "તેના આધારિત આપની જાણ સારું મિનરલ વોટર પ્લાન્ટની ક્ષમતા તેમજ જરૂરી માહિત નીચે દર્શાવેલ છે."
            oTable.Cell(12, 1).Range.Font.Size = 9

            oTable.Cell(13, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(14, 1).Range.Font.Size = 8
            oTable.Cell(14, 1).Range.Text = " "

            oTable.Cell(15, 1).Range.Text = "મિનરલ વોટર પ્લાન્ટની  ટેકનીકલ માહિતી આ સાથે જોડેલ છે."
            oTable.Cell(15, 1).Range.Font.Size = 9


            oTable.Cell(16, 1).Range.Text = "અમને આશા છે, ઉપર દર્શાવેલ વિગતો આપની જરૂરિયાત મુજબ છે.જો તમારે ટેકનીકલ બાબતો માં"
            oTable.Cell(16, 1).Range.Font.Size = 9

            oTable.Cell(17, 1).Range.Text = "કોઈ જાણકારી જોઈતી હોય તો અમારો સંપર્ક કરવા વિનંતી છે.અમે તેનું નિરાકરણ જેમ બને તેમ જલ્દીથી આપવા પ્રયત્ન કરીશું"
            oTable.Cell(17, 1).Range.Font.Size = 9

            'oTable.Cell(21, 1).Range.Text = "અમે તેનું નિરાકરણ જેમ બને તેમ જલ્દીથી આપવા પ્રયત્ન કરીશું."
            'oTable.Cell(21, 1).Range.Font.Size = 9

            oTable.Cell(18, 1).Range.Text = "આભાર સહ"
            oTable.Cell(18, 1).Range.Font.Size = 9

            oTable.Cell(19, 1).Range.Text = " "
            oTable.Cell(19, 1).Range.Text = "આપનો વિશ્વાસુ,"
            oTable.Cell(19, 1).Range.Font.Size = 9
            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(14, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(13).Range.Font.Bold = True
            oTable.Rows.Item(13).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(13).Range.Font.Size = 14
            oTable.Rows.Item(8).Range.Underline = True

            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)

            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 1, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0


        ElseIf RdbHindi.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 21, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "
            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"

            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text
            oTable.Cell(12, 1).Range.Font.Size = 9


            oTable.Cell(13, 1).Range.Text = ""

            oTable.Cell(14, 1).Range.Text = "In this regard, we are submitting herewith our offer for following capacities:"
            oTable.Cell(14, 1).Range.Font.Size = 9

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text
            oTable.Cell(16, 1).Range.Font.Size = 9


            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."
            oTable.Cell(18, 1).Range.Font.Size = 9

            oTable.Cell(19, 1).Range.Text = ""


            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(20, 1).Range.Font.Size = 9

            oTable.Cell(21, 1).Range.Text = ""


            oTable.Cell(22, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(23, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(24, 1).Range.Text = ""

            oTable.Cell(25, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(26, 1).Range.Text = " "
            oTable.Cell(27, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(28, 1).Range.Text = ""
            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            ' oTable.Rows.Item(12).Range.Font.Bold = True 'name'
            'oTable.Rows.Item(26).Range.Font.Bold = True 'designation'
            '' oTable.Rows.Item(24).Range.Font.Bold = True
            'oTable.Rows.Item(32).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(33).Range.Font.Bold = True 'Indian' 
            'oTable.Rows.Item(29).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(30).Range.Font.Bold = True 'Indian' 

            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14

            'oTable.Rows.Item(32).Range.Font.Size = 12 'name'
            'oTable.Rows.Item(33).Range.Font.Size = 12 'designation'


            oTable.Rows.Item(8).Range.Underline = True

            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 3, 1, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable51.Range.ParagraphFormat.SpaceAfter = 0




            strt3 = objDoc.Tables(1).Range.[End]
            oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 2, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0


        ElseIf RdbMarathi.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 21, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "

            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"

            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text

            oTable.Cell(13, 1).Range.Text = ""


            oTable.Cell(14, 1).Range.Text = "In this regard, we are submitting herewith our offer for following capacities:"

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."

            oTable.Cell(19, 1).Range.Text = ""


            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(21, 1).Range.Text = ""


            oTable.Cell(22, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(23, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(24, 1).Range.Text = ""

            oTable.Cell(25, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(26, 1).Range.Text = " "
            oTable.Cell(27, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(28, 1).Range.Text = ""
            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            ' oTable.Rows.Item(12).Range.Font.Bold = True 'name'
            'oTable.Rows.Item(26).Range.Font.Bold = True 'designation'
            '' oTable.Rows.Item(24).Range.Font.Bold = True
            'oTable.Rows.Item(32).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(33).Range.Font.Bold = True 'Indian' 
            'oTable.Rows.Item(29).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(30).Range.Font.Bold = True 'Indian' 

            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14

            'oTable.Rows.Item(32).Range.Font.Size = 12 'name'
            'oTable.Rows.Item(33).Range.Font.Size = 12 'designation'


            oTable.Rows.Item(8).Range.Underline = True

            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 3, 1, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable51.Range.ParagraphFormat.SpaceAfter = 0




            strt3 = objDoc.Tables(1).Range.[End]
            oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 2, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0


        ElseIf RdbTamil.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 21, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "

            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"

            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text

            oTable.Cell(13, 1).Range.Text = ""

            oTable.Cell(14, 1).Range.Text = "In this regard, we are submitting herewith our offer for following capacities:"

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."

            oTable.Cell(19, 1).Range.Text = ""


            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(21, 1).Range.Text = ""


            oTable.Cell(22, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(23, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(24, 1).Range.Text = ""

            oTable.Cell(25, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(26, 1).Range.Text = " "
            oTable.Cell(27, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(28, 1).Range.Text = ""
            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            ' oTable.Rows.Item(12).Range.Font.Bold = True 'name'
            'oTable.Rows.Item(26).Range.Font.Bold = True 'designation'
            '' oTable.Rows.Item(24).Range.Font.Bold = True
            'oTable.Rows.Item(32).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(33).Range.Font.Bold = True 'Indian' 
            'oTable.Rows.Item(29).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(30).Range.Font.Bold = True 'Indian' 

            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14

            'oTable.Rows.Item(32).Range.Font.Size = 12 'name'
            'oTable.Rows.Item(33).Range.Font.Size = 12 'designation'


            oTable.Rows.Item(8).Range.Underline = True
            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 3, 1, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable51.Range.ParagraphFormat.SpaceAfter = 0




            strt3 = objDoc.Tables(1).Range.[End]
            oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 2, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0


        ElseIf RdbTelugu.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 21, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "

            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"

            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text

            oTable.Cell(13, 1).Range.Text = ""

            oTable.Cell(14, 1).Range.Text = "In this regard, we are submitting herewith our offer for following capacities:"

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."

            oTable.Cell(19, 1).Range.Text = ""


            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(21, 1).Range.Text = ""


            oTable.Cell(22, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(23, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(24, 1).Range.Text = ""

            oTable.Cell(25, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(26, 1).Range.Text = " "
            oTable.Cell(27, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(28, 1).Range.Text = ""


            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            ' oTable.Rows.Item(12).Range.Font.Bold = True 'name'
            'oTable.Rows.Item(26).Range.Font.Bold = True 'designation'
            '' oTable.Rows.Item(24).Range.Font.Bold = True
            'oTable.Rows.Item(32).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(33).Range.Font.Bold = True 'Indian' 
            'oTable.Rows.Item(29).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(30).Range.Font.Bold = True 'Indian' 

            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14

            'oTable.Rows.Item(32).Range.Font.Size = 12 'name'
            'oTable.Rows.Item(33).Range.Font.Size = 12 'designation'


            oTable.Rows.Item(8).Range.Underline = True

            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 3, 1, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable51.Range.ParagraphFormat.SpaceAfter = 0




            strt3 = objDoc.Tables(1).Range.[End]
            oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 2, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0


        End If






        Dim Rowa1 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        Rowa1.Cells(1).Range.Text = "For, INDIAN ION EXCHANGE"
        Rowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        Rowa1.Range.Font.Size = 12
        Rowa1.Cells(1).Width = 250
        Rowa1.Cells(2).Width = 30
        Rowa1.Cells(4).Width = 50
        Rowa1.Cells(3).Width = 50
        Rowa1.Range.Font.Bold = True

        Dim Rowa2 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        Rowa2.Cells(2).Range.Text = "   & CHEMICALS LTD."
        Rowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        Rowa2.Range.Font.Bold = True
        Rowa2.Cells(1).Width = 30
        Rowa2.Cells(2).Width = 200
        Rowa2.Cells(4).Width = 50
        Rowa2.Cells(3).Width = 200
        Rowa2.Range.Font.Size = 12

        Dim newRow22 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        If DocumentStatus = 0 Then
            newRow22.Cells(2).Range.InlineShapes.AddPicture(Class1.global.Signature).Width = 100
            newRow22.Cells(3).Range.InlineShapes.AddPicture(appPath + "\SIGN.jpg").Width = 100

        Else
            newRow22.Cells(2).Range.InlineShapes.AddPicture(appPath + "\blanksign.jpg").Width = 100
            newRow22.Cells(3).Range.InlineShapes.AddPicture(appPath + "\blanksign.jpg").Width = 100
        End If
        newRow22.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        newRow22.Cells(1).Width = 30
        newRow22.Cells(1).Height = 50
        newRow22.Cells(2).Width = 200
        newRow22.Cells(4).Width = 50
        newRow22.Cells(3).Width = 200
        newRow22.Cells(3).Height = 50
        newRow22.Range.Font.Bold = True
        Dim newRow221 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRow221.Height = 20
        newRow221.Cells(2).Range.Text = Convert.ToString(Class1.global.UserName)
        newRow221.Cells(3).Range.Text = "DR. BHAVIN VYAS"
        newRow221.Cells(3).Range.Font.Name = "Times New Roman"
        newRow221.Cells(1).Width = 30
        newRow221.Cells(2).Width = 200
        newRow221.Cells(4).Width = 50
        newRow221.Cells(3).Width = 200

        newRow221.Range.Font.Bold = True
        newRow221.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow221.Cells(3).Range.Font.Name = "Times New Roman"
        newRow221.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

        Dim newRow222 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRow222.Height = 20
        newRow222.Range.Font.Bold = True
        newRow222.Cells(2).Range.Text = "(" + txtDesignation.Text + ")"
        newRow222.Cells(3).Range.Text = "(TECH.DIRECTOR)"
        newRow222.Cells(2).Range.Font.Name = "Times New Roman"
        newRow222.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow222.Cells(3).Range.Font.Name = "Times New Roman"
        newRow222.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

        newRow222.Cells(1).Width = 30
        newRow222.Cells(2).Width = 200
        newRow222.Cells(4).Width = 50
        newRow222.Cells(3).Width = 200

        'ection.HeadersFooters.Add(New HeaderFooter(document, HeaderFooterType.FooterDefault, New Paragraph(document, New Picture(document, "MyImage.png"))))


        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HeaderRo.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next


            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange, Word.WdFieldType.wdFieldPage)
                footerrange.Text = "Corporate Office : D-11,First Floor, Diamond Park, G.I.D.C.,Naroda, Ahmedabad-382330, Gujarat (India) Tele Fax : 91-079-22819065/67/68            export@indianionexchange.com            www.indianionexchange.com          www.bottlingindia.com"
                footerrange.Font.Size = 8
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        Else

            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
        End If


        Dim format As Object = Word.WdSaveFormat.wdFormatPDF



        objDoc.SaveAs(appPath + "\Letter1.doc")



        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document
        wordDocument1 = New Word.Document
        wordApplication1 = New Word.Application

        Dim paramSourceDocPath1 As Object = appPath + "\Letter1.doc"
        Dim Targets1 As Object = appPath + "\Letter1.pdf"



        Dim formating1 As Object
        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)



        objDoc.Close()
        objDoc = Nothing
        objApp.Quit()
        objApp = Nothing

        If Not IsNothing(wordDocument) Then
            wordDocument1.Close()
            wordDocument1 = Nothing
            wordApplication1.Quit()
            wordApplication1 = Nothing
        End If
        'wordDocument1.Close()
        'wordDocument1 = Nothing
        'wordApplication1.Quit()
        'wordApplication1 = Nothing

        ''''''''JIGAR MISTRI END''''''''

        Dim _pdfforge As New PDF.PDF
        Dim _pdftext As New PDF.PDFText
        Dim GetImage As String

        GetImage = ""

        For t As Integer = 0 To GvTechnical.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTechnical.Rows(t).Cells(1).Value)
            If IsTicked Then
                Dim GetSrNo As String = GvTechnical.Rows(t).Cells(2).Value.ToString()
                GetImage += GetSrNo + ","
            End If
        Next

        selectSrNo = GetImage.TrimEnd(",")
        If selectSrNo.Length > 0 Then


            Dim str12 As String
            Dim da33 As New SqlDataAdapter
            Dim ds33 As New DataSet


            str12 = "select * from Category_Master (NOLOCK) where SNo IN(" & selectSrNo & ") and Capacity='" + txtCapacity1.Text.Trim() + "' AND MainCategory = '" + txtQoutType.Text.Trim() + "' ORDER BY SNo"
            da33 = New SqlDataAdapter(str12, con1)
            ds33 = New DataSet
            da33.Fill(ds33)
            Dim totalimage As Int32
            totalimage = Convert.ToInt32(ds.Tables(0).Rows.Count) * 3


            ReDim str(5)
            Dim p As Int32
            p = 0
            str(p) = PicDefault.ImageLocation.ToString()
            p = 1
            ReDim Preserve str(p)


            'Check Quotation Type :Regular Quotation Or Detail Quotation
            Dim PhotoStartNo As Integer
            Dim PhotoEndNo As Integer
            If chkDetailQuotation.Checked = True Then
                'Detail Quotation Photo Between 1 To 20
                PhotoStartNo = 1
                PhotoEndNo = 20
            Else

                'Regular Quotation Photo Between 21 to 24
                PhotoStartNo = 21
                PhotoEndNo = 24
            End If


            For x As Integer = 0 To ds33.Tables(0).Rows.Count - 1
                Dim PhotoNo As String
                For photocount As Integer = PhotoStartNo To PhotoEndNo
                    'Add multiple Photo 1 - 20 No     26-07-2016 Navin Goradara
                    PhotoNo = "Photo" & photocount
                    If ds33.Tables(0).Rows(x)(PhotoNo).ToString().Trim() <> "" Then
                        str(p) = ds33.Tables(0).Rows(x)(PhotoNo).ToString().Replace("D:", "\\192.168.1.102\") 'add navin .Replace("D:", "\\192.168.1.20") 06-05-2015
                        p = p + 1
                        ReDim Preserve str(p)
                    End If
                Next

            Next

            ReDim Preserve str(p - 1)
            Dim ikl As Integer
            Dim missing12 As Object = System.Reflection.Missing.Value
            objApp12 = New Word.Application
            objDoc12 = New Word.Document
            objDoc12 = objApp12.Documents.Add(missing12, missing12, missing12, missing12)
            Dim start12 As Object = 0
            Dim end12 As Object = 0
            objApp12 = New Word.Application
            objDoc12 = New Word.Document
            Dim rng12 As Word.Range = objDoc12.Range(start12, missing12)
            '  oTable12.Add(rng12, 1, 1, missing12, missing12)
            Dim defaultTableBehavior12 As [Object] = Type.Missing
            Dim autoFitBehavior12 As [Object] = Type.Missing
            rng12.Font.Name = "Times New Roman"

            objDoc12.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4


            For ikl = 0 To str.Length - 1
                'tbl12.Range.ParagraphFormat.SpaceAfter = 0
                'tbl12.Cell(1, 1).Range.InlineShapes.AddPicture(str(ikl))
                If str(ikl) <> "" Then
                    Try
                        objDoc12.Application.Selection.InlineShapes.AddPicture(str(ikl)).Height = 650
                    Catch ex As Exception
                    End Try
                    ' objDoc12.Application.Selection.FitTextWidth = 550

                End If


                'tbl12.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                If DocumentStatus = 0 Then
                    For Each section As Word.Section In objDoc12.Sections
                        Dim headerRange1 As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                        headerRange1.Fields.Add(headerRange1)
                        headerRange1.InlineShapes.AddPicture(appPath + "\HederRoimg.jpg")
                        headerRange1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                        '   headerRange.Delete = Word.WdFieldType.wdFieldPage
                    Next
                End If
            Next



            objDoc12.SaveAs(appPath + "\" + Convert.ToString(0) + ".doc")

            objDoc12.Close()
            objDoc12 = Nothing
            objApp12.Quit()
            objApp12 = Nothing

            Dim exportFormat12 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
            Dim paramMissing12 As Object = Type.Missing


            Dim paramSourceDocPath12 As Object = appPath + "\" + Convert.ToString(0) + ".doc"
            Dim Targets12 As Object = appPath + "\" + Convert.ToString("step3") + ".pdf"

            'wordDocument1 = wordApplication1.Documents.Open(paramSourceDocPath1)
            'objDoc12 = objApp12.Documents.Open(paramSourceDocPath12)
            wordDocument = New Word.Document
            wordApplication = New Word.Application
            wordDocument = wordApplication.Documents.Open(paramSourceDocPath12)



            Dim formating12 As Object
            formating12 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
            wordDocument.SaveAs(Targets12, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)



            ' tbl12 = Nothing
            wordDocument.Close()
            wordDocument = Nothing
            wordApplication.NormalTemplate.Saved = True
            wordApplication.Quit()
            wordApplication = Nothing
            Dim str1(0) As String
            ReDim str1(0)
            str1(0) = appPath + "\" + Convert.ToString(0) + ".doc"
            Dim i As Integer
            '  i = _pdfforge.Images2PDF(str1, appPath + "\step3.pdf", 0)
        ElseIf PicDefault.ImageLocation.ToString() <> "" Then
            ReDim Preserve str(0)
            str(0) = PicDefault.ImageLocation.ToString()
            Dim ikl As Integer
            Dim missing12 As Object = System.Reflection.Missing.Value
            objApp12 = New Word.Application
            objDoc12 = New Word.Document
            objDoc12 = objApp12.Documents.Add(missing12, missing12, missing12, missing12)
            Dim start12 As Object = 0
            Dim end12 As Object = 0
            objApp12 = New Word.Application
            objDoc12 = New Word.Document
            Dim rng12 As Word.Range = objDoc12.Range(start12, missing12)
            '  oTable12.Add(rng12, 1, 1, missing12, missing12)
            Dim defaultTableBehavior12 As [Object] = Type.Missing
            Dim autoFitBehavior12 As [Object] = Type.Missing
            rng12.Font.Name = "Times New Roman"

            objDoc12.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4



            For ikl = 0 To str.Length - 1
                'tbl12.Range.ParagraphFormat.SpaceAfter = 0
                'tbl12.Cell(1, 1).Range.InlineShapes.AddPicture(str(ikl))
                If str(ikl) <> "" Then
                    Try
                        objDoc12.Application.Selection.InlineShapes.AddPicture(str(ikl)).Height = 650
                    Catch ex As Exception
                    End Try
                    ' objDoc12.Application.Selection.FitTextWidth = 550

                End If


                'tbl12.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                If DocumentStatus = 0 Then
                    For Each section As Word.Section In objDoc12.Sections
                        Dim headerRange1 As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                        headerRange1.Fields.Add(headerRange1)
                        headerRange1.InlineShapes.AddPicture(appPath + "\HederRoimg.jpg")
                        headerRange1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                        '   headerRange.Delete = Word.WdFieldType.wdFieldPage
                    Next
                End If
            Next
            objDoc12.SaveAs(appPath + "\" + Convert.ToString(0) + ".doc")
            objDoc12.Close()
            objDoc12 = Nothing
            objApp12.Quit()
            objApp12 = Nothing

            Dim exportFormat12 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
            Dim paramMissing12 As Object = Type.Missing


            Dim paramSourceDocPath12 As Object = appPath + "\" + Convert.ToString(0) + ".doc"
            Dim Targets12 As Object = appPath + "\" + Convert.ToString("step3") + ".pdf"

            'wordDocument1 = wordApplication1.Documents.Open(paramSourceDocPath1)
            'objDoc12 = objApp12.Documents.Open(paramSourceDocPath12)
            wordDocument = New Word.Document
            wordApplication = New Word.Application
            wordDocument = wordApplication.Documents.Open(paramSourceDocPath12)


            Dim formating12 As Object
            formating12 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
            wordDocument.SaveAs(Targets12, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)



            ' tbl12 = Nothing
            wordDocument.Close()
            wordDocument = Nothing
            wordApplication.NormalTemplate.Saved = True
            wordApplication.Quit()
            wordApplication = Nothing
            Dim str1(0) As String
            ReDim str1(0)
            str1(0) = appPath + "\" + Convert.ToString(0) + ".doc"

        End If

        PriceSheet()
        Dim exportFormat As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing As Object = Type.Missing

        wordDocument = New Word.Document
        wordApplication = New Word.Application

        Dim paramSourceDocPath As Object = appPath + "\Letter2.doc"
        Dim Targets As Object = appPath + "\Letter2.pdf"

        wordDocument = wordApplication.Documents.Open(paramSourceDocPath)

        Dim formating As Object
        formating = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        wordDocument.SaveAs(Targets, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        wordDocument.Close()
        wordDocument = Nothing
        wordApplication.NormalTemplate.Saved = True
        wordApplication.Quit()
        wordApplication = Nothing

        If (Not System.IO.Directory.Exists(QPath + "\NON ISI")) Then
            System.IO.Directory.CreateDirectory(QPath + "\NON ISI")
        End If
        Dim str3333 As String
        If txtDate.Text.Contains("/") Then
            If (RblSingle.Checked = True Or RblOther.Checked = True) Then
                str3333 = txtCapacity1.Text + " LPH -" + txtDate.Text.Replace("/", "-")
            Else
                str3333 = txtCapacity1.Text + " LPH -" + txtCapacity2.Text + " LPH -" + txtDate.Text.Replace("/", "-")
            End If

        Else
            If (RblSingle.Checked = True Or RblOther.Checked = True) Then
                str3333 = txtCapacity1.Text + " LPH -" + txtDate.Text
            Else
                str3333 = txtCapacity1.Text + " LPH -" + txtCapacity2.Text + " LPH -" + txtDate.Text
            End If
        End If

        If Not IsNothing(str) Then
            str = Nothing
            Dim files(2) As String
            files(0) = appPath + "\Letter1.pdf"
            files(1) = appPath + "\" + Convert.ToString("step3") + ".pdf"
            'files(1) = appPath + "\step3.pdf"
            files(2) = appPath + "\Letter2.pdf"
            Dim fullpath12 As String
            fullpath12 = QPath + "\NON ISI\" + txtEnqNo.Text.Trim().Replace("/", "-") + "-" + Convert.ToString(EnqMax) + "-" + txtName.Text.Trim().Replace("/", "-") + "-" + str3333 + " - NON-ISI" + ".pdf"
            'fullpath12 = QPath + "\NON ISI\" + "\final.pdf"
            _pdfforge.MergePDFFiles(files, fullpath12, False)
            'Dim pros As Process() = Process.GetProcesses()
            'For i As Integer = 0 To pros.Count() - 1
            '    If pros(i).ProcessName.ToLower().Contains("winword") Then
            '        pros(i).Kill()
            '    End If
            'Next

        Else
            str = Nothing
            Dim files(1) As String
            files(0) = appPath + "\Letter1.pdf"
            files(1) = appPath + "\Letter2.pdf"
            Dim fullpath12 As String
            fullpath12 = QPath + "\NON ISI\" + txtEnqNo.Text.Trim().Replace("/", "-") + "-" + Convert.ToString(EnqMax) + "-" + txtName.Text.Trim().Replace("/", "-") + "-" + str3333 + " - NON-ISI" + ".pdf"
            _pdfforge.MergePDFFiles(files, fullpath12, False)



        End If

        MessageBox.Show("Document Ready !")

        System.Diagnostics.Process.Start(QPath + "\NON ISI\" + txtEnqNo.Text.Trim().Replace("/", "-") + "-" + Convert.ToString(EnqMax) + "-" + txtName.Text.Trim().Replace("/", "-") + "-" + str3333 + " - NON-ISI" + ".pdf")


        Try

            'Class1.killProcessOnUser()
        Catch ex As Exception

        End Try
        If Not IsNothing(objApp12) Then
            objApp12.NormalTemplate.Saved = True
            objApp12.Quit()
        End If
        If Not IsNothing(objDoc12) Then
            objDoc12.Close()
        End If


        If Not IsNothing(wordDocument) Then
            wordDocument.Close()
        End If
        If Not IsNothing(wordApplication) Then
            wordDocument.Close()
        End If


        'Add Navin PDF To PNG 




    End Sub

    Public Sub bindQuatData()
        Try
            If (Class1.global.QuatationId <> 0) Then
                QuationId = Class1.global.QuatationId
                btnAddClear.Enabled = True
                btnCancel.Enabled = False
                btnAddClear.Text = "View"
            Else
                btnAddClear.Enabled = True
                btnCancel.Enabled = True

            End If
            con1.Close()
            btnSave1.Text = "Update"
            Display()
            Gv_GetTechnicalData()
            GetTechnicalData()
            Total1()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GvCategorySearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCategorySearch.DoubleClick

        chkDetailQuotation.Checked = False

        GroupQuotationStatus.Visible = False
        Try
            If rblOld.Checked = True Then
                QuationId = Convert.ToInt32(Me.GvCategorySearch.SelectedCells(0).Value)
                bindQuatData()
            Else
                'Sales Executive Quotation Details
                GroupQuotationStatus.Visible = True
                Fk_SalesExecutiveQtnID = Convert.ToInt64(Me.GvCategorySearch.SelectedCells(0).Value)
                DisplaySalesExecutive_Bind()
                Total1()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub DisplaySalesExecutive_Bind()

        ''Basic Details
        Dim data = linq_obj.SP_Get_SalesExecutiveQuotationByAddressID(Fk_SalesExecutiveQtnID).ToList()
        For Each item As SP_Get_SalesExecutiveQuotationByAddressIDResult In data
            txtType.Text = item.QtnType
            Address_ID = item.Pk_AddressID
            txtQoutType.Text = item.QuotationType
            txtName.Text = item.Name


            txtEnqNo.Text = item.EnqNo

            txtLatterDate.Text = item.CreatedDate
            lblThrough.Visible = True

            lblThrough.Text = "By:" + item.Through

            lblEmailText.Text = item.EmailID
            lblDescription.Text = item.Description
            lblMobile.Text = item.MobileNo



            If item.Status = "Done" Then
                rblDone.Checked = True

            End If
            txtQoutType_Leave(Nothing, Nothing)
            ' user master

            Dim data_user = linq_obj.SP_Get_User_Master_By_UserID(item.Fk_UserId).ToList()

            For Each item_u As SP_Get_User_Master_By_UserIDResult In data_user
                txtBussness_Exe.Text = item_u.Designation
                txtBuss_Name.Text = item_u.FirstName

            Next


            If item.FK_EnqTypeID = 1 Then
                ddlEnqType.Text = "Domestic"
            ElseIf item.FK_EnqTypeID = 2 Then
                ddlEnqType.Text = "B2B"
            End If
            ddlEnqType_SelectionChangeCommitted(Nothing, Nothing)
            txtEnqNo_Leave(Nothing, Nothing)
            txtAddress.Text = item.City + "," + item.DeliveryState
            RdbEnglish.Checked = True
            ddlBussines_Executive.Text = "TELEPHONIC"

            If item.QuotationType = "NON ISI" Then
                txtSub.Text = "REQUIREMENT FOR MINERAL WATER PROJECT -" & item.QuotationType
            Else
                txtSub.Text = "REQUIREMENT FOR REVERSE OSMOSIS PLANT -" & item.QuotationType
            End If
            ddlBussines_Executive_SelectionChangeCommitted(Nothing, Nothing)
            txtNoContent.Text = "6"
            Dim flagstatus As Integer
            If item.CapacityType = 1 Then
                flagstatus = 1
                '  RblSingle.Checked = True
                txtCapacity1.Text = item.Capacity1
                RblSingle.Checked = True
                GvSingle__SalesExecutive_Bind(item.Fk_SalesExecutiveQtnID)
                GvMultiple_SalesExecutive_Bind(item.Fk_SalesExecutiveQtnID)

                Dim totalcapacity As Int64
                totalcapacity = Convert.ToInt32(txtCapacity1.Text) * 20
                txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
            Else
                flagstatus = 3
                '  RblMultiple.Checked = True
                RblMultiple.Checked = True
                txtCapacity1.Text = item.Capacity1
                txtCapacity2.Text = item.Capacity2

                GvSingle__SalesExecutive_Bind(item.Fk_SalesExecutiveQtnID)
                GvMultiple_SalesExecutive_Bind(item.Fk_SalesExecutiveQtnID)
                Dim totalcapacity1 As Int64
                Dim totalcapacity2 As Int64
                totalcapacity1 = Convert.ToInt64(txtCapacity1.Text) * 20
                totalcapacity2 = Convert.ToInt64(txtCapacity2.Text) * 20
                Dim Newline As String
                Newline = System.Environment.NewLine
                txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity1.ToString() + " LITER/DAY" & Newline & "CAPACITY: " + txtCapacity2.Text + "  LITER/HR . . . . " + totalcapacity2.ToString() + " LITER/DAY  ").ToString()

            End If
            txtEnqNo.Enabled = False


            'get username



            Exit For


        Next

        txtNoContent.Enabled = False
        txtCapacity1.Enabled = False
        txtCapacity2.Enabled = False
        GroupBox2.Enabled = False
        'technical data      
        GetTechnicalData()




    End Sub
    Public Sub Display()

        Dim str As String
        Try
            con1.Close()
            con1.Open()

            str = "select * from Quotation_Master (NOLOCK) where Pk_QuotationID=" & QuationId & ""
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            dr.Read()

            txtQoutType.Text = dr("Quot_Type").ToString()
            txtName.Text = dr("Name").ToString()
            txtAddress.Text = dr("Address").ToString()


            txtType.Text = dr("QType").ToString()
            txtQoutNo.Text = dr("Fk_EnqTypeID").ToString() + dr("Quot_No").ToString()
            txtEnqNo.Text = dr("Enq_No").ToString()
            txtRef.Text = dr("Ref").ToString()

            'Added by rajesh
            'get a name for a file name.
            EnqMax = Class1.getTotalNo(dr("Ref"))

            'EnqMax = Convert.ToInt16(dr("Quot_No"))

            txtBussness_Exe.Text = dr("Buss_Excecutive").ToString()
            If dr("LanguageId").ToString() = "1" Then
                RdbEnglish.Checked = True
            ElseIf dr("LanguageId").ToString() = "2" Then
                RdbGujarati.Checked = True

            ElseIf dr("LanguageId").ToString() = "3" Then
                RdbHindi.Checked = True

            ElseIf dr("LanguageId").ToString() = "4" Then
                RdbMarathi.Checked = True

            ElseIf dr("LanguageId").ToString() = "5" Then
                RdbTamil.Checked = True

            ElseIf dr("LanguageId").ToString() = "6" Then
                RdbTelugu.Checked = True
            End If


            Dim flagstatus As Integer

            If dr("Capacity_Type").ToString() = "Single" Then

                flagstatus = 1
                '  RblSingle.Checked = True
                txtCapacity1.Text = dr("Capacity_Single").ToString()
            End If
            If dr("Capacity_Type").ToString() = "Other" Then
                flagstatus = 2
                ' RblOther.Checked = True
                txtCapacity1.Text = dr("Capacity_Single").ToString()

            End If

            If dr("Capacity_Type").ToString() = "Multiple" Then
                flagstatus = 3
                '  RblMultiple.Checked = True
                txtCapacity1.Text = dr("Capacity_Single").ToString()
                txtCapacity2.Text = dr("Capacity_Multiple").ToString()

            End If
            txtEnqNo.Enabled = False

            txtKind.Text = dr("KindAtt").ToString()
            txtSub.Text = dr("Subject").ToString()
            ddlBussines_Executive.SelectedItem = dr("Buss_Excecutive").ToString()
            txtBuss_Name.Text = dr("Buss_Name").ToString()
            txtDescription.Text = dr("Later_Description").ToString()
            txtLatterDate.Text = dr("Later_Date").ToString()
            txtCapacityWord.Text = dr("Capacity_Word").ToString()
            'txtUserName.Text = dr("UserName").ToString()
            'PicDefault.ImageLocation = dr("DefaultImage").ToString()
            TxtTax.Visible = False
            lbltxt.Visible = False
            dr.Dispose()
            cmd.Dispose()

            txtNoContent.Enabled = False
            txtCapacity1.Enabled = False
            txtCapacity2.Enabled = False



            str = "select * from Discount_master (NOLOCK) where Fk_QuotationID=" & QuationId & ""
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            txtspdiscount.Text = dr("SpecialDisc").ToString()
            txtVat.Text = dr("Vat").ToString()
            txtErection.Text = dr("Erection").ToString()
            txtInsurance.Text = dr("Insurance").ToString()

            'navin 19-03-2014

            'txtFinalPrice.Text = dr("FinalTotal").ToString()
            txtFinalPrice.Text = Convert.ToDecimal(dr("FinalTotal").ToString()).ToString("N2")

            ''   txtISI.Text = dr("ISIFee").ToString()
            txtTransporation.Text = dr("Transportation").ToString()
            txtPakingforwarding.Text = dr("Packing").ToString()
            cmd.Dispose()
            dr.Dispose()
            con1.Close()

            con1.Close()
            Gv_GetTechnicalData()

            '  lblSno.Text = GvTechnical.Rows.Count + 1
            Try
                If TxtTax.Text = "" Then
                    GvTechnical.Columns("Tax").Visible = False
                Else
                    GvTechnical.Columns("Tax").Visible = False
                End If
            Catch ex As Exception

            End Try




            If flagstatus = 1 Then
                RblSingle.Checked = True
            ElseIf flagstatus = 2 Then

                RblOther.Checked = True
            Else
                RblMultiple.Checked = True
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub

    Public Sub Gv_GetTechnicalData()

        If RblSingle.Checked = True Then

            Dim str1 As String
            Dim da123 As New SqlDataAdapter
            Dim ds123 As New DataSet
            Dim dt12 As New DataTable



            dt12.Columns.Add("Remove", GetType(Boolean))
            dt12.Columns.Add("Select", GetType(Boolean))
            dt12.Columns.Add("SrNo", GetType(Int32))
            dt12.Columns.Add("Category", GetType(String))
            dt12.Columns.Add("Price", GetType(String))
            str1 = "select  SNo,TechnicalData,Price1,DocumentationImage from Technical_Data (NOLOCK) where Fk_QuotationID=" & QuationId & ""
            da123 = New SqlDataAdapter(str1, con1)
            ds123 = New DataSet()
            da123.Fill(ds123)

            For S1 = 0 To ds123.Tables(0).Rows.Count - 1

                Dim imagestatus As Int16
                imagestatus = 0
                If ds123.Tables(0).Rows(S1)("DocumentationImage") = "Yes" Then
                    imagestatus = 1
                End If

                dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString())

            Next
            If dt12 Is Nothing Then
            Else
                If dt12.Rows.Count > 0 Then
                    Dim dView As New DataView(dt12)
                    dView.Sort = "SrNo ASC"
                    dt12 = dView.ToTable()
                End If
            End If
            GvTechnical.DataSource = dt12
            RblSingle.Enabled = False
            RblOther.Enabled = False
            RblMultiple.Enabled = False

            ''GvTechnical.ReadOnly = True
            ' btnAdd.Visible = False
            da123.Dispose()
            dt12.Dispose()
            ds123.Dispose()

        End If

        If RblOther.Checked = True Then

            Dim str1 As String
            Dim da123 As New SqlDataAdapter
            Dim ds123 As New DataSet
            Dim dt12 As New DataTable
            dt12.Columns.Add("Remove", GetType(Boolean))
            dt12.Columns.Add("Select", GetType(Boolean))
            dt12.Columns.Add("SrNo", GetType(Int32))
            dt12.Columns.Add("Category", GetType(String))
            dt12.Columns.Add("Price", GetType(String))
            dt12.Columns.Add("Qty", GetType(String))
            dt12.Columns.Add("Total", GetType(String))

            str1 = "select  SNo,TechnicalData,Price1,Qty,Price2,DocumentationImage from Technical_Data (NOLOCK) where Fk_QuotationID=" & QuationId & ""
            da123 = New SqlDataAdapter(str1, con1)
            ds123 = New DataSet()
            da123.Fill(ds123)
            For S1 = 0 To ds123.Tables(0).Rows.Count - 1

                Dim imagestatus As Int16
                imagestatus = 0
                If ds123.Tables(0).Rows(S1)("DocumentationImage") = "Yes" Then
                    imagestatus = 1
                End If
                dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString(), ds123.Tables(0).Rows(S1)("Qty").ToString(), ds123.Tables(0).Rows(S1)("Price2").ToString())

            Next
            If dt12 Is Nothing Then
            Else
                If dt12.Rows.Count > 0 Then
                    Dim dView As New DataView(dt12)
                    dView.Sort = "SrNo ASC"
                    dt12 = dView.ToTable()
                End If
            End If
            If dt12 Is Nothing Then
            Else
                If dt12.Rows.Count > 0 Then
                    Dim dView As New DataView(dt12)
                    dView.Sort = "SrNo ASC"
                    dt12 = dView.ToTable()
                End If
            End If
            GvTechnical.DataSource = dt12
            RblSingle.Enabled = False
            RblOther.Enabled = False
            RblMultiple.Enabled = False

            ''GvTechnical.ReadOnly = True
            ' btnAdd.Visible = False
            da123.Dispose()
            dt12.Dispose()
            ds123.Dispose()

        End If
        If RblMultiple.Checked = True Then



            Dim str1 As String
            Dim da123 As New SqlDataAdapter
            Dim ds123 As New DataSet
            Dim dt12 As New DataTable
            dt12.Columns.Add("Remove", GetType(Boolean))
            dt12.Columns.Add("Select", GetType(Boolean))
            dt12.Columns.Add("SrNo", GetType(Int32))
            dt12.Columns.Add("Category", GetType(String))
            dt12.Columns.Add("Price1", GetType(String))
            dt12.Columns.Add("Price2", GetType(String))
            str1 = "select  SNo,TechnicalData,Price1,Price2,DocumentationImage from Technical_Data (NOLOCK) where Fk_QuotationID=" & QuationId & ""
            da123 = New SqlDataAdapter(str1, con1)
            ds123 = New DataSet()
            da123.Fill(ds123)
            For S1 = 0 To ds123.Tables(0).Rows.Count - 1

                Dim imagestatus As Int16
                imagestatus = 0
                If ds123.Tables(0).Rows(S1)("DocumentationImage") = "Yes" Then
                    imagestatus = 1
                End If
                dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString(), ds123.Tables(0).Rows(S1)("Price2").ToString())

            Next
            If dt12 Is Nothing Then
            Else
                If dt12.Rows.Count > 0 Then
                    Dim dView As New DataView(dt12)
                    dView.Sort = "SrNo ASC"
                    dt12 = dView.ToTable()
                End If
            End If
            GvTechnical.DataSource = dt12
            RblSingle.Enabled = False
            RblOther.Enabled = False
            RblMultiple.Enabled = False

            ''GvTechnical.ReadOnly = True
            '  btnAdd.Visible = False
            da123.Dispose()
            dt12.Dispose()
            ds123.Dispose()

        End If

    End Sub

    Private Sub txtPreviousQuatation_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPreviousQuatation.Leave
        If txtPreviousQuatation.Text.Trim() <> "" Then

            Try

                Dim Pre_str As String
                Dim cmd1 As New SqlCommand
                Dim dr1 As SqlDataReader

                con1.Close()
                con1.Open()

                Pre_str = "select * from Quotation_Master (NOLOCK) where Fk_EnqTypeID='" + ddlEnqType.SelectedValue + "' and Quot_No=" & txtPreviousQuatation.Text & ""
                cmd1 = New SqlCommand(Pre_str, con1)
                dr1 = cmd1.ExecuteReader()
                dr1.Read()
                QuationId = Convert.ToInt32(dr1("Pk_QuotationID").ToString())
                If dr1("Capacity_Type").ToString() = "Single" Then
                    RblSingle.Checked = True
                    txtCapacity1.Text = dr1("Capacity_Single").ToString()
                End If
                If dr1("Capacity_Type").ToString() = "Other" Then
                    RblOther.Checked = True
                    txtCapacity1.Text = dr1("Capacity_Single").ToString()

                End If

                If dr1("Capacity_Type").ToString() = "Multiple" Then
                    RblMultiple.Checked = True
                    txtCapacity1.Text = dr1("Capacity_Single").ToString()
                    txtCapacity2.Text = dr1("Capacity_Multiple").ToString()

                End If
                cmd1.Dispose()
            Catch ex As Exception

            End Try
            Gv_GetTechnicalData()
            con1.Close()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim str As String
        If (TPMAINQuatation.SelectedTab.Text = "Quatation") Then


            If txtSearchName.Text <> "" And txtSearchEnQ.Text = "" Then
                str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master (NOLOCK) where Name like '%" + txtSearchName.Text + "%' and Quatation_Type like 'NON ISI' "
                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)
                bindSearchGrid()
                da.Dispose()
                ds.Dispose()
            ElseIf txtSearchEnQ.Text <> "" And txtSearchName.Text = "" Then
                str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master (NOLOCK) where Enq_No like '%" + txtSearchEnQ.Text + "%' and Quatation_Type like 'NON ISI' "
                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)
                bindSearchGrid()
                da.Dispose()
                ds.Dispose()
            ElseIf txtSearchEnQ.Text <> "" And txtSearchName.Text <> "" Then
                str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master (NOLOCK) where Enq_No like '%" + txtSearchEnQ.Text + "%' and Name like '%" + txtSearchName.Text + "%' and Quatation_Type like 'NON ISI' "
                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)
                bindSearchGrid()
                da.Dispose()
                ds.Dispose()
            End If
        Else

        End If

        ''str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master where Enq_No='" + txtSearchEnQ.Text + "' or Name='" + txtSearchName.Text + "'  "

    End Sub

    Private Sub ddlBussines_Executive_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBussines_Executive.SelectionChangeCommitted

        'If ddlBussines_Executive.SelectedItem = "TELEPHONIC" Then

        '    Dim desc As String
        '    desc = "Thank you for your Telecon with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " for Mineral Water Plant  regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."

        '    txtDescription.Text = desc.ToString()


        'End If
        'If ddlBussines_Executive.SelectedItem = "MAIL" Then

        '    Dim desc As String
        '    desc = "This refers to your mail dated " + txtLatterDate.Text + " regarding your subject requirement. We thank you very much for your enquiry and indeed appreciated your interest in range of our products."

        '    txtDescription.Text = desc.ToString()
        'End If
        'If ddlBussines_Executive.SelectedItem = "VISIT NARODA FACTORY" Then

        '    Dim desc As String
        '    desc = "We thank you very much for paying your visit at our Naroda Factory and personal discussion you had with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " in regards to subject matter. Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."

        '    txtDescription.Text = desc.ToString()


        'End If

        'If ddlBussines_Executive.SelectedItem = "PERSONAL VISIT " Then

        '    Dim desc As String
        '    Dim Newline As String
        '    Newline = System.Environment.NewLine
        '    desc = "The courtesy and consideration extended to our Director Mr. J. B. Vyas during his personal visit at your office of the date to discussed regarding subject matter, are sincerely appreciated. We thank you very much for sparing your valued time for the discussion and showing interest in range of our products." + Newline + " " + Newline + " We thank you very much for sparing your valuable time for personal discussion you had with our  " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " regarding your subject requirement are indeed appreciated. We thank you very much for sparing your valued time for the discussion and showing interest in range of our products."
        '    txtDescription.Text = desc.ToString()


        'End If
        DifferentLanguageThankyou()


    End Sub

    Public Sub bindSearchGrid()
        GvCategorySearch.DataSource = ds.Tables(0)
        Dim tt As Int32
        tt = GvCategorySearch.Rows.Count()
        If (tt > 0) Then
            txtTotalRecord.Text = tt.ToString()
            GvCategorySearch.Columns(0).Visible = False

        End If

    End Sub

    Public Sub DifferentLanguageThankyou()
        If ddlBussines_Executive.SelectedItem = "TELEPHONIC" Then
            Dim desc As String



            If RdbEnglish.Checked Then
                LanguageId = 1

                ''Added By Rajesh
                'For Change Text of quatation by type.
                If txtQoutType.Text.ToUpper() = "ISI" Or txtQoutType.Text.ToUpper() = "NON ISI" Then
                    desc = "Thank you for your telephonic discussion with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " for Mineral Water Plant  regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                    txtDescription.Text = desc.ToString()
                    Class1.global.LanguageId = LanguageId

                    ''Added By Rajesh
                    'For Change Text of quatation by type.
                Else
                    desc = "Thank you for your telephonic discussion with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " for " + txtQoutType.Text + "  regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                    txtDescription.Text = desc.ToString()
                    Class1.global.LanguageId = LanguageId
                End If



            ElseIf RdbGujarati.Checked Then
                LanguageId = 2
                desc = "સવિનય સાથે જાણવાનું છે કે, આપની મિનરલ વોટર પ્લાન્ટના અનુસંધાનમાં અમારા " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " સાથે ટેલિફોનિક વાતચીત થયેલ હતી. આપના જણાવ્યા મુજબ આપની મિનરલ વોટર પ્લાનની " + " જરૂરીયાત અનુસાર કોટેશન નીચે દર્શાવેલ છે."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbHindi.Checked Then
                LanguageId = 3
                desc = "Thank you for your telephonic discussion with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " for Mineral Water Plant  regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbMarathi.Checked Then
                LanguageId = 4
                desc = "Thank you for your telephonic discussion with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " for Mineral Water Plant  regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbTamil.Checked Then
                LanguageId = 5
                desc = "Thank you for your telephonic discussion with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " for Mineral Water Plant  regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbTelugu.Checked Then
                LanguageId = 6
                desc = "Thank you for your telephonic discussion with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " for Mineral Water Plant  regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            End If



        End If
        If ddlBussines_Executive.SelectedItem = "MAIL" Then

            Dim desc As String

            If RdbEnglish.Checked Then
                LanguageId = 1
                desc = "This refers to your mail dated " + txtLatterDate.Text + " regarding your subject requirement. We thank you very much for your enquiry and indeed appreciated your interest in range of our products."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbGujarati.Checked Then
                LanguageId = 2
                desc = "This refers to your mail dated " + txtLatterDate.Text + " regarding your subject requirement. We thank you very much for your enquiry and indeed appreciated your interest in range of our products."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbHindi.Checked Then
                LanguageId = 3
                desc = "This refers to your mail dated " + txtLatterDate.Text + " regarding your subject requirement. We thank you very much for your enquiry and indeed appreciated your interest in range of our products."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbMarathi.Checked Then
                LanguageId = 4
                desc = "This refers to your mail dated " + txtLatterDate.Text + " regarding your subject requirement. We thank you very much for your enquiry and indeed appreciated your interest in range of our products."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbTamil.Checked Then
                LanguageId = 5
                desc = "This refers to your mail dated " + txtLatterDate.Text + " regarding your subject requirement. We thank you very much for your enquiry and indeed appreciated your interest in range of our products."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbTelugu.Checked Then
                LanguageId = 6
                desc = "This refers to your mail dated " + txtLatterDate.Text + " regarding your subject requirement. We thank you very much for your enquiry and indeed appreciated your interest in range of our products."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            End If
        End If
        If ddlBussines_Executive.SelectedItem = "VISIT NARODA FACTORY" Then

            Dim desc As String

            If RdbEnglish.Checked Then
                LanguageId = 1
                desc = "We thank you very much for paying your visit at our Naroda Factory and personal discussion you had with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " in regards to subject matter. Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbGujarati.Checked Then
                LanguageId = 2
                desc = "We thank you very much for paying your visit at our Naroda Factory and personal discussion you had with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " in regards to subject matter. Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbHindi.Checked Then
                LanguageId = 3
                desc = "We thank you very much for paying your visit at our Naroda Factory and personal discussion you had with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " in regards to subject matter. Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbMarathi.Checked Then
                LanguageId = 4
                desc = "We thank you very much for paying your visit at our Naroda Factory and personal discussion you had with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " in regards to subject matter. Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbTamil.Checked Then
                LanguageId = 5
                desc = "We thank you very much for paying your visit at our Naroda Factory and personal discussion you had with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " in regards to subject matter. Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbTelugu.Checked Then
                LanguageId = 6
                desc = "We thank you very much for paying your visit at our Naroda Factory and personal discussion you had with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " in regards to subject matter. Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            End If

        End If

        If ddlBussines_Executive.SelectedItem = "PERSONAL VISIT " Then

            Dim desc As String
            Dim Newline As String
            If RdbEnglish.Checked Then
                Newline = System.Environment.NewLine
                desc = "The courtesy and consideration extended to our" + txtBussness_Exe.Text + " " + txtBuss_Name.Text + "  during his personal visit at your office with our on dated " + txtLatterDate.Text + " to discussed regarding subject matter, are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbGujarati.Checked Then
                LanguageId = 2
                desc = "The courtesy and consideration extended to our" + txtBussness_Exe.Text + " " + txtBuss_Name.Text + "  during his personal visit at your office with our on dated " + txtLatterDate.Text + " to discussed regarding subject matter, are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbHindi.Checked Then
                LanguageId = 3
                desc = "The courtesy and consideration extended to our" + txtBussness_Exe.Text + " " + txtBuss_Name.Text + "  during his personal visit at your office with our on dated " + txtLatterDate.Text + " to discussed regarding subject matter, are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbMarathi.Checked Then
                LanguageId = 4
                desc = "The courtesy and consideration extended to our" + txtBussness_Exe.Text + " " + txtBuss_Name.Text + "  during his personal visit at your office with our on dated " + txtLatterDate.Text + " to discussed regarding subject matter, are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbTamil.Checked Then
                LanguageId = 5
                desc = "The courtesy and consideration extended to our" + txtBussness_Exe.Text + " " + txtBuss_Name.Text + "  during his personal visit at your office with our on dated " + txtLatterDate.Text + " to discussed regarding subject matter, are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            ElseIf RdbTelugu.Checked Then
                LanguageId = 6
                desc = "The courtesy and consideration extended to our" + txtBussness_Exe.Text + " " + txtBuss_Name.Text + "  during his personal visit at your office with our on dated " + txtLatterDate.Text + " to discussed regarding subject matter, are sincerely appreciated."
                txtDescription.Text = desc.ToString()
                Class1.global.LanguageId = LanguageId
            End If
        End If

    End Sub

    Private Sub txtLatterDate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ddlBussines_Executive_SelectionChangeCommitted(Nothing, Nothing)

    End Sub

    Private Sub txtBuss_Name_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ddlBussines_Executive_SelectionChangeCommitted(Nothing, Nothing)
    End Sub

    Private Sub btndeletequotation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeletequotation.Click
        Try
            con1.Open()

            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete Quotation?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then

                If Me.GvCategorySearch.SelectedRows.Count > 0 Then

                    Dim delete As String = "delete from Quotation_Master where Pk_QuotationID=" & GvCategorySearch.SelectedRows(0).Cells(0).Value & ""
                    cmd = New SqlCommand(delete, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    Dim delete1 As String = "delete from Technical_Data where Fk_QuotationID=" & GvCategorySearch.SelectedRows(0).Cells(0).Value & ""
                    cmd = New SqlCommand(delete1, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    MessageBox.Show("Delete Quotation Successfully..")
                End If
            End If
        Catch ex As Exception
        End Try
        con1.Close()
        BindOnLanguageName()
        'GvQuotationSearch_Bind(LanguageId)
    End Sub


    Public Shared Function GetProcessInfoByPID(ByVal PID As Integer)
        Dim User As String = [String].Empty
        Dim Domain As String = [String].Empty
        Dim OwnerSID As String = [String].Empty
        Dim processname As String = [String].Empty
        Try
            'Dim sq As New ObjectQuery("Select * from Win32_Process  Where Name LIKE 'WINWORD%' and ProcessID = " + Convert.ToString(PID))
            Dim sq As New ObjectQuery("Select * from Win64_Process  Where Name LIKE 'WINWORD%' and ProcessID = " + Convert.ToString(PID)) ' change 30-12-2014
            'Dim sq As New ObjectQuery("Select * from Win32_Process")

            Dim searcher As New ManagementObjectSearcher(sq)
            If searcher.[Get]().Count = 0 Then
                Return OwnerSID
            End If
            For Each oReturn As ManagementObject In searcher.[Get]()
                Dim o As String() = New [String](1) {}
                'Invoke the method and populate the o var with the user name and domain
                oReturn.InvokeMethod("GetOwner", DirectCast(o, Object()))

                'int pid = (int)oReturn["ProcessID"];
                processname = DirectCast(oReturn("Name"), String)
                'dr[2] = oReturn["Description"];
                User = o(0)
                If User Is Nothing Then
                    User = [String].Empty
                End If
                Domain = o(1)
                If Domain Is Nothing Then
                    Domain = [String].Empty
                End If
                Dim sid As String() = New [String](0) {}
                oReturn.InvokeMethod("GetOwner", DirectCast(sid, Object()))
                If Domain <> "" Then
                    OwnerSID = Domain + "\" + User
                Else
                    OwnerSID = Domain
                End If
                Return OwnerSID
            Next
        Catch
            Return OwnerSID
        End Try
        Return OwnerSID
    End Function

    Public Sub PDFSetQuatationTrue()
        Try
            con1.Close()

        Catch ex As Exception

        End Try

        Try
            con1.Open()
            Dim QMaxId As Integer
            Dim PQMaxId As Integer

            Dim RefName As String


            Dim mm As String
            mm = "select Max(Pk_QuotationID) as QMax from Quotation_Master (NOLOCK)"
            cmd = New SqlCommand(mm, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            QMaxId = dr("QMax").ToString()
            cmd.Dispose()
            dr.Dispose()

            mm = "select Max(FK_QuatationID) as QMax from PDFGenerate_Check (NOLOCK)"
            cmd = New SqlCommand(mm, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            PQMaxId = dr("QMax").ToString()
            cmd.Dispose()
            dr.Dispose()


            mm = "select [Ref] as Referenceno from Quotation_Master (NOLOCK) where Pk_QuotationID = " & QMaxId & ""
            cmd = New SqlCommand(mm, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            RefName = dr("Referenceno").ToString()
            cmd.Dispose()
            dr.Dispose()

            If btnSave1.Text = "Save" Then
                If PQMaxId = QMaxId Then

                    Dim strPdfInsert As String
                    strPdfInsert = "Update  PDFGenerate_Check  Set IsCreated = 'Yes'  where FK_QuatationID =" & QMaxId & ""
                    cmd = New SqlCommand(strPdfInsert, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    con1.Close()
                    Class1.global.GobalMaxId = PQMaxId
                    FlagPdf = 1

                Else
                    FlagPdf = 0

                End If
            Else
                Dim strPdfInsert As String
                strPdfInsert = "Update  PDFGenerate_Check  Set IsCreated = 'Yes'  where FK_QuatationID =" & QuationId & ""
                cmd = New SqlCommand(strPdfInsert, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con1.Close()
                Class1.global.GobalMaxId = QuationId
                FlagPdf = 1
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Specialpricesheet()
        '    Try
        Dim missing As Object = System.Reflection.Missing.Value

        Dim Visible As Object = True

        Dim start1 As Object = 0

        Dim end1 As Object = 0
        Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        Dim objApp As New Word.Application()

        Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)
        objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        Dim oTable3 As Word.Tables = objDoc.Tables
        Dim defaultTableBehavior As [Object] = Type.Missing
        Dim autoFitBehavior As [Object] = Type.Missing

        Dim rng As Word.Range = objDoc.Range(start1, missing)
        oTable3.Add(rng, 1, 4, missing, missing)
        rng.Font.Name = "Calibri"
        rng.Borders.Enable = 0
        rng.Font.Size = 8
        rng.Font.Bold = True

    End Sub

    Private Sub btnHF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHF.Click

        Try
            Class1.killProcessOnUser()

            Me.UseWaitCursor = True
            DocumentStatus = 0
            PDFSetQuatationTrue()
            OleMessageFilter.Register()

            FinalDucumetation()
            OleMessageFilter.Revoke()
            Me.UseWaitCursor = False
            SetClean()

            btnAdd.Visible = True
            GvTechnical.DataSource = Null
            GvTechnical.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
            Me.UseWaitCursor = False
            SetClean()
            btnAdd.Visible = True
            GvTechnical.DataSource = Null
            GvTechnical.Refresh()
            'Class1.killProcessOnUser()
        End Try




    End Sub

    Private Sub btnWf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWf.Click
        Try
            Class1.killProcessOnUser()
            Me.UseWaitCursor = True
            DocumentStatus = 1
            PDFSetQuatationTrue()
            OleMessageFilter.Register()

            FinalDucumetation()
            OleMessageFilter.Revoke()
            Me.UseWaitCursor = False
            SetClean()
            btnAdd.Visible = True
            GvTechnical.DataSource = Null
            GvTechnical.Refresh()

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Me.UseWaitCursor = False
            SetClean()
            btnAdd.Visible = True
            GvTechnical.DataSource = Null
            GvTechnical.Refresh()
            Class1.killProcessOnUser()
        End Try

    End Sub

    Public Sub Validation_Text()
        txtType.BackColor = System.Drawing.Color.White
        txtType.ForeColor = System.Drawing.Color.Black
        If (txtType.Text.Trim() = "") Then
            txtType.BackColor = System.Drawing.Color.Red

            Dim exc As New Exception()
            txtType.ForeColor = System.Drawing.Color.White
            txtType.Focus()
            Throw exc
        End If
        If (txtQoutNo.Text.Trim() = "") Then
            txtQoutNo.BackColor = System.Drawing.Color.Red
            Dim exc As New Exception()
            txtQoutNo.ForeColor = System.Drawing.Color.White
            txtQoutNo.Focus()
            Throw exc
        End If
        If (txtEnqNo.Text.Trim() = "") Then
            txtEnqNo.BackColor = System.Drawing.Color.Red
            Dim exc As New Exception()
            txtEnqNo.ForeColor = System.Drawing.Color.White
            txtEnqNo.Focus()
            Throw exc
        End If
    End Sub

    Public Sub SetClean()
        txtEnqNo.Enabled = True
        txtType.Text = ""
        TxtTax.Text = "0"
        txtQoutNo.Text = ""
        txtEnqNo.Text = ""
        txtRef.Text = ""
        txtName.Text = ""
        txtAddress.Text = ""
        txtKind.Text = ""
        txtSub.Text = ""
        txtBuss_Name.Text = ""
        txtDescription.Text = ""
        txtCapacity1.Text = ""
        txtCapacity2.Text = ""
        txtDescription.Text = ""
        txtCapacityWord.Text = ""
        TxtTax.Visible = True
        lbltxt.Visible = True
        PicDefault.ImageLocation = Nothing
        txtNoContent.Enabled = True
        txtCapacity1.Enabled = True
        txtCapacity2.Enabled = True
        txtQoutType.Text = ""
        txtBussness_Exe.Text = ""
        lblAllsysTotal.Text = ""
        lblAllMulSysTotal.Text = ""
        txtContent1.Text = ""
        txtPrice_21.Text = ""
        txtPrice_11.Text = ""
        txtPrice_31.Text = ""
        lblHeader2.Text = ""
        lblHeader.Text = ""
        lblHeader3.Text = ""
        txtFinalPrice.Text = ""
        'Addition Price List
        txtspdiscount.Text = ""
        txtVat.Text = ""
        txtISI.Text = ""
        txtTransporation.Text = ""
        txtInsurance.Text = ""
        txtPakingforwarding.Text = ""
        txtErection.Text = ""
        txtFinalPrice.Text = ""


    End Sub

    Private Sub btnAddClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddClear.Click
        Try
            con1.Close()

        Catch ex As Exception

        End Try
        Try
            txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
            txtLatterDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
            If btnAddClear.Text = "View" Then
                Dim Targets1 As Object = appPath + "\OrderData" + "\" + Convert.ToString(QuationId) + ".pdf"
                Dim TargetsSpecial1 As Object = appPath + "\OrderData" + "\SpecialPrice\" + Convert.ToString(QuationId) + ".pdf"
                Dim flag As Integer = 0
                Dim cnt As Integer = 0
                Dim _pdfforge As New PDF.PDF
                Dim _pdftext As New PDF.PDFText

                If (System.IO.File.Exists(TargetsSpecial1)) Then
                    flag = 1
                End If
                If flag = 1 Then
                    cnt = 0
                Else
                    cnt = 1
                End If
                Dim files(cnt) As String
                files(0) = Targets1
                If (flag <> 1) Then
                    files(1) = TargetsSpecial1
                End If
                If (Not System.IO.Directory.Exists(appPath + "\Temporaryview")) Then
                    System.IO.Directory.CreateDirectory(appPath + "\Temporaryview")
                End If
                _pdfforge.MergePDFFiles(files, appPath + "\Temporaryview\" + Convert.ToString(QuationId) + ".pdf", False)

                System.Diagnostics.Process.Start(appPath + "\Temporaryview\" + Convert.ToString(QuationId) + ".pdf")

            Else
                btnSave1.Text = "Save"
                SetClean()
                RblMultiple.Enabled = True
                RblOther.Enabled = True
                RblSingle.Enabled = True
                QuationId = 0
                If GvTechnical.Rows.Count > 0 Then
                    GvTechnical.DataSource = Null
                    btnAdd.Visible = True
                End If
            End If
            txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
            txtLatterDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BindOnLanguageName()
        'GvQuotationSearch_Bind(LanguageId)
    End Sub

    Private Sub txttax_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            con1.Close()

        Catch ex As Exception

        End Try

        SetClean()
        GvTechnical.ReadOnly = False
        btnAdd.Visible = True
        GvTechnical.DataSource = Null
        GvTechnical.Refresh()
        btnSave1.Text = "Save"
        RblSingle.Enabled = True
        RblMultiple.Enabled = True
        RblOther.Enabled = True

        QuationId = 0
    End Sub

    Private Sub txtQoutType_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQoutType.Leave
        If txtQoutType.Text.Trim().ToUpper() = "NON ISI" Then
            PicDefault.ImageLocation = appPath + "\image\SIGNATURES\NONISI.jpg"

        ElseIf txtQoutType.Text.Trim().ToUpper() = "RO" Then

            PicDefault.ImageLocation = appPath + "\image\SIGNATURES\RO.jpg"

        Else
            PicDefault.ImageLocation = Nothing
        End If


    End Sub

    Private Sub rdbTerms2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTerms2.CheckedChanged
        txt9.Visible = False
        txt91.Visible = False
        txtPayTerms.Visible = False
        txt1.Text = "Price bases Ex. Godown Ahmedabad."
        ' txt2.Text = "15% Vat Extra at actual"
        txt2.Text = "GST will applicable as per Central Govt.norms"
        txt3.Text = "Transportation, Forwarding, Fright, Insurance, etc., will be extra at actual. "
        txt4.Text = "Payment : 100% against Performa Invoice before dispatch"
        txt41.Text = "Delivery :Ready Stock"
        txt42.Text = ""
        txt42.Visible = False
        txt5.Text = ""
        txt5.Visible = False
        txt6.Text = ""
        txt6.Visible = False
        txt61.Text = ""
        txt61.Visible = False
        txt71.Text = ""
        txt71.Visible = False
        txt7.Visible = False
        txt7.Text = ""
        txt5.Text = ""
        txt5.Visible = False
        txt51.Text = ""
        txt51.Visible = False
        txt52.Text = ""
        txt52.Visible = False

    End Sub

    Private Sub rdbTerms1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTerms1.CheckedChanged
        GetcheckData()
        txt9.Visible = False
        txt91.Visible = False
        txtPayTerms.Visible = False

    End Sub

    Private Sub RdbEnglish_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbEnglish.CheckedChanged
        BindOnLanguageName()
        GvTechnical.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()
    End Sub

    Private Sub RdbGujarati_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbGujarati.CheckedChanged
        BindOnLanguageName()
        GvTechnical.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()

    End Sub

    Private Sub RdbHindi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbHindi.CheckedChanged
        BindOnLanguageName()
        GvTechnical.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        SetClean()

    End Sub

    Private Sub RdbMarathi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbMarathi.CheckedChanged
        BindOnLanguageName()
        GvTechnical.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()

    End Sub

    Private Sub RdbTamil_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbTamil.CheckedChanged
        BindOnLanguageName()
        GvTechnical.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()

    End Sub

    Private Sub RdbTelugu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbTelugu.CheckedChanged
        BindOnLanguageName()
        GvTechnical.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()
    End Sub

    Public Sub BindOnLanguageName()
        If RdbEnglish.Checked Then
            LanguageId = 1
            Class1.global.LanguageId = LanguageId
            GvQuotationSearch_Bind(LanguageId)
            GetcheckData()
        ElseIf RdbGujarati.Checked Then
            LanguageId = 2
            Class1.global.LanguageId = LanguageId
            GvQuotationSearch_Bind(LanguageId)
            GetcheckData()
        ElseIf RdbHindi.Checked Then
            LanguageId = 3
            Class1.global.LanguageId = LanguageId
            GvQuotationSearch_Bind(LanguageId)
            GetcheckData()
        ElseIf RdbMarathi.Checked Then
            LanguageId = 4
            Class1.global.LanguageId = LanguageId
            GvQuotationSearch_Bind(LanguageId)
            GetcheckData()
        ElseIf RdbTamil.Checked Then
            LanguageId = 5
            Class1.global.LanguageId = LanguageId
            GvQuotationSearch_Bind(LanguageId)
            GetcheckData()
        ElseIf RdbTelugu.Checked Then
            LanguageId = 6
            Class1.global.LanguageId = LanguageId
            GvQuotationSearch_Bind(LanguageId)
            GetcheckData()
        End If
    End Sub

    Private Sub BtnCreateOrderDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub FirstPage()
        Class1.killProcessOnUser()
        '    Try
        Dim missing As Object = System.Reflection.Missing.Value

        Dim Visible As Object = True

        Dim start1 As Object = 0

        Dim end1 As Object = 0
        Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        Dim objApp As New Word.Application()

        Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)
        objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        Dim oTable3 As Word.Tables = objDoc.Tables
        Dim defaultTableBehavior As [Object] = Type.Missing
        Dim autoFitBehavior As [Object] = Type.Missing
        Dim rng As Word.Range = objDoc.Range(start1, missing)
        oTable3.Add(rng, 5, 2, missing, missing)
        rng.Font.Name = "Times New Roman"
        Dim tbl As Word.Table = objDoc.Tables(1)
        tbl.Range.ParagraphFormat.SpaceAfter = 0
        '  tbl.Borders.Enable = 1
        For ikf = 1 To 5
            tbl.Cell(ikf, 1).Range.Font.Name = "Times New Roman"
        Next
        'For Each cel As Word.Cell In tbl.Range.Cells
        '    cel.TopPadding = 2
        'Next

        ' tbl.Range.ParagraphFormat.SpaceAfter = 3
        tbl.Cell(1, 1).Range.Text = "Ref :  " + txtRef.Text + " "
        tbl.Cell(1, 1).TopPadding = 10

        tbl.Cell(1, 1).Range.Font.Name = "Times New Roman"
        tbl.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        tbl.Cell(1, 2).Range.Text = "Date:" + txtDate.Text + " "
        tbl.Cell(1, 2).Range.Font.Name = "Times New Roman"
        tbl.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        tbl.Cell(1, 2).TopPadding = 3

        tbl.Cell(2, 1).Range.Text = ""
        tbl.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        tbl.Cell(3, 1).Range.Text = "TO,"
        tbl.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        tbl.Cell(4, 1).Range.Text = txtName.Text + " "
        tbl.Cell(4, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        tbl.Cell(5, 1).Range.Text = txtAddress.Text + " "
        tbl.Cell(5, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        Dim strt As Object = tbl.Range.[End]
        Dim oCollapseEnd As Object = Word.WdCollapseDirection.wdCollapseEnd

        Dim ran As Word.Range = objDoc.Range(strt, strt)
        rng = objDoc.Content
        rng.Collapse(oCollapseEnd)
        Dim intcheck As Integer

        Dim desc As String
        If RdbEnglish.Checked Then

            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 22, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0



            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "

            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"
            Try
                con1.Close()
            Catch ex As Exception

            End Try

            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text

            oTable.Cell(13, 1).Range.Text = ""

            oTable.Cell(14, 1).Range.Text = "In this regard, we are submitting herewith our offer for following capacities:"

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."

            oTable.Cell(19, 1).Range.Text = ""
            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(21, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(22, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(23, 1).Range.Text = ""

            oTable.Cell(24, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(25, 1).Range.Text = " "
            oTable.Cell(26, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(27, 1).Range.Text = ""
            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            ' oTable.Rows.Item(12).Range.Font.Bold = True 'name'
            oTable.Rows.Item(25).Range.Font.Bold = True 'designation'
            ' oTable.Rows.Item(24).Range.Font.Bold = True
            'oTable.Rows.Item(32).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(33).Range.Font.Bold = True 'Indian' 
            'oTable.Rows.Item(29).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(30).Range.Font.Bold = True 'Indian' 

            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14

            'oTable.Rows.Item(32).Range.Font.Size = 12 'name'
            'oTable.Rows.Item(33).Range.Font.Size = 12 'designation'
            oTable.Rows.Item(8).Range.Underline = True
            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            If RblMultiple.Checked Then
                intcheck = 1
            ElseIf RblSingle.Checked Then
                intcheck = 2
            Else
                intcheck = 2
            End If
            Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, intcheck, 1, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable51.Range.ParagraphFormat.SpaceAfter = 0




            strt3 = objDoc.Tables(1).Range.[End]
            oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 2, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0
        ElseIf RdbGujarati.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 19, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "પ્રતિ શ્રી:" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "વિષય:" + txtSub.Text + " "
            oTable.Cell(8, 1).Range.Font.Size = 9


            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "માનનીય,"
            oTable.Cell(10, 1).Range.Font.Size = 9


            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text

            oTable.Cell(13, 1).Range.Text = "તેના આધારિત આપની જાણ સારું મિનરલ વોટર પ્લાન્ટની ક્ષમતા તેમજ જરૂરી માહિત નીચે દર્શાવેલ છે."
            oTable.Cell(13, 1).Range.Font.Size = 9

            oTable.Cell(14, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(15, 1).Range.Font.Size = 8
            oTable.Cell(15, 1).Range.Text = " "

            oTable.Cell(16, 1).Range.Text = "મિનરલ વોટર પ્લાન્ટની  ટેકનીકલ માહિતી આ સાથે જોડેલ છે."
            oTable.Cell(16, 1).Range.Font.Size = 9


            oTable.Cell(17, 1).Range.Text = "અમને આશા છે, ઉપર દર્શાવેલ વિગતો આપની જરૂરિયાત મુજબ છે.જો તમારે ટેકનીકલ બાબતો માં"
            oTable.Cell(17, 1).Range.Font.Size = 9

            oTable.Cell(18, 1).Range.Text = "કોઈ જાણકારી જોઈતી હોય તો અમારો સંપર્ક કરવા વિનંતી છે.અમે તેનું નિરાકરણ જેમ બને તેમ જલ્દીથી આપવા પ્રયત્ન કરીશું"
            oTable.Cell(18, 1).Range.Font.Size = 9

            'oTable.Cell(21, 1).Range.Text = "અમે તેનું નિરાકરણ જેમ બને તેમ જલ્દીથી આપવા પ્રયત્ન કરીશું."
            'oTable.Cell(21, 1).Range.Font.Size = 9

            oTable.Cell(19, 1).Range.Text = "આભાર સહ"
            oTable.Cell(19, 1).Range.Font.Size = 9

            oTable.Cell(20, 1).Range.Text = " "
            oTable.Cell(20, 1).Range.Text = "આપનો વિશ્વાસુ,"
            oTable.Cell(20, 1).Range.Font.Size = 9
            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(14, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(14).Range.Font.Bold = True
            oTable.Rows.Item(14).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(14).Range.Font.Size = 14
            oTable.Rows.Item(8).Range.Underline = True

            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            'rng = objDoc.Content
            'rng.Collapse(oCollapseEnd)
            'Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 1, 1, missing, missing)
            ''    newRow4.HeadingFormat = 3
            'oTable51.Range.ParagraphFormat.SpaceAfter = 0

            'strt3 = objDoc.Tables(1).Range.[End]
            'oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            'ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 1, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0

        ElseIf RdbHindi.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 21, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "


            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"

            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text
            oTable.Cell(12, 1).Range.Font.Size = 9


            oTable.Cell(13, 1).Range.Text = ""

            oTable.Cell(14, 1).Range.Text = "In this regard, we are submitting herewith our offer for following capacities:"
            oTable.Cell(14, 1).Range.Font.Size = 9

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text
            oTable.Cell(16, 1).Range.Font.Size = 9


            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."
            oTable.Cell(18, 1).Range.Font.Size = 9

            oTable.Cell(19, 1).Range.Text = ""


            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(20, 1).Range.Font.Size = 9

            oTable.Cell(21, 1).Range.Text = ""


            oTable.Cell(22, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(23, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(24, 1).Range.Text = ""

            oTable.Cell(25, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(26, 1).Range.Text = " "
            oTable.Cell(27, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(28, 1).Range.Text = ""
            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'
            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14
            oTable.Rows.Item(8).Range.Underline = True

            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 3, 1, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable51.Range.ParagraphFormat.SpaceAfter = 0




            strt3 = objDoc.Tables(1).Range.[End]
            oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 2, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0
        ElseIf RdbMarathi.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 21, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "

            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"

            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text

            oTable.Cell(13, 1).Range.Text = ""


            oTable.Cell(14, 1).Range.Text = "In this regard, we are submitting herewith our offer for following capacities:"

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."

            oTable.Cell(19, 1).Range.Text = ""


            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(21, 1).Range.Text = ""


            oTable.Cell(22, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(23, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(24, 1).Range.Text = ""

            oTable.Cell(25, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(26, 1).Range.Text = " "
            oTable.Cell(27, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(28, 1).Range.Text = ""
            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14
            oTable.Rows.Item(8).Range.Underline = True

            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 3, 1, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable51.Range.ParagraphFormat.SpaceAfter = 0




            strt3 = objDoc.Tables(1).Range.[End]
            oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 2, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0
        ElseIf RdbTamil.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 21, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "

            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"

            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text

            oTable.Cell(13, 1).Range.Text = ""

            oTable.Cell(14, 1).Range.Text = "In this regard, we are submitting herewith our offer for following capacities:"

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."

            oTable.Cell(19, 1).Range.Text = ""


            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(21, 1).Range.Text = ""


            oTable.Cell(22, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(23, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(24, 1).Range.Text = ""

            oTable.Cell(25, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(26, 1).Range.Text = " "
            oTable.Cell(27, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(28, 1).Range.Text = ""
            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

            ' oTable.Rows.Item(12).Range.Font.Bold = True 'name'
            'oTable.Rows.Item(26).Range.Font.Bold = True 'designation'
            '' oTable.Rows.Item(24).Range.Font.Bold = True
            'oTable.Rows.Item(32).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(33).Range.Font.Bold = True 'Indian' 
            'oTable.Rows.Item(29).Range.Font.Bold = True 'chemical'
            'oTable.Rows.Item(30).Range.Font.Bold = True 'Indian' 

            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14
            oTable.Rows.Item(8).Range.Underline = True

            Dim strt3 As Object = objDoc.Tables(1).Range.[End]
            Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)


            Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 3, 1, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable51.Range.ParagraphFormat.SpaceAfter = 0




            strt3 = objDoc.Tables(1).Range.[End]
            oCollapseEnd3 = Word.WdCollapseDirection.wdCollapseEnd
            ran3 = objDoc.Range(strt3, strt3)
            rng = objDoc.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 2, 4, missing, missing)
            '    newRow4.HeadingFormat = 3
            oTable5.Range.ParagraphFormat.SpaceAfter = 0
        ElseIf RdbTelugu.Checked Then
            Dim oTable As Word.Table = objDoc.Tables.Add(ran, 21, 1, missing, missing)
            lines = ""
            oTable.Range.ParagraphFormat.SpaceAfter = 0


            If txtKind.Text.Trim() <> "" Then
                oTable.Cell(7, 1).Range.Text = "KIND ATTN :" + txtKind.Text + " "

            End If
            oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "

            oTable.Cell(9, 1).Range.Text = " "
            oTable.Cell(10, 1).Range.Text = "Dear Sir,"

            con1.Open()

            oTable.Cell(11, 1).Range.Text = ""
            oTable.Cell(12, 1).Range.Text = txtDescription.Text

            oTable.Cell(13, 1).Range.Text = ""

            oTable.Cell(14, 1).Range.Text = "In this regard, we are submitting herewith our offer for following capacities:"

            oTable.Cell(15, 1).Range.Text = " "
            oTable.Cell(15, 1).Range.Font.Size = 8

            oTable.Cell(16, 1).Range.Text = txtCapacityWord.Text

            oTable.Cell(17, 1).Range.Font.Size = 8
            oTable.Cell(17, 1).Range.Text = " "

            oTable.Cell(18, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."

            oTable.Cell(19, 1).Range.Text = ""


            oTable.Cell(20, 1).Range.Text = "We hope that you will find the above in line with your requirement."
            oTable.Cell(21, 1).Range.Text = ""


            oTable.Cell(22, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."
            oTable.Cell(23, 1).Range.Text = "We shall be glad to furnish the same."
            oTable.Cell(24, 1).Range.Text = ""

            oTable.Cell(25, 1).Range.Text = "Thanking you and anticipating a favourable reply."
            oTable.Cell(26, 1).Range.Text = " "
            oTable.Cell(27, 1).Range.Text = "Yours faithfully,"
            oTable.Cell(28, 1).Range.Text = ""


            oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Cell(16, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = True
            oTable.Rows.Item(4).Range.Font.Bold = True
            oTable.Rows.Item(5).Range.Font.Bold = True
            oTable.Rows.Item(6).Range.Font.Bold = True
            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(15).Range.Font.Bold = True
            oTable.Rows.Item(16).Range.Font.Bold = True

            oTable.Rows.Item(7).Range.Font.Bold = True
            oTable.Rows.Item(8).Range.Font.Bold = True 'sub'
            oTable.Rows.Item(1).Range.Font.Size = 12
            oTable.Rows.Item(4).Range.Font.Size = 12
            oTable.Rows.Item(5).Range.Font.Size = 12
            oTable.Rows.Item(8).Range.Font.Size = 12
            oTable.Rows.Item(6).Range.Font.Size = 16
            oTable.Rows.Item(7).Range.Font.Size = 12
            oTable.Rows.Item(16).Range.Font.Size = 14
            oTable.Rows.Item(8).Range.Underline = True
        End If





        cmd.Dispose()
        dr.Dispose()
        con1.Close()




        Dim Rowa1 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        Rowa1.Cells(1).Range.Text = "For, INDIAN ION EXCHANGE"
        Rowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        Rowa1.Range.Font.Size = 12
        Rowa1.Cells(1).Width = 250
        Rowa1.Cells(2).Width = 30
        Rowa1.Cells(4).Width = 50
        Rowa1.Cells(3).Width = 50
        Rowa1.Range.Font.Bold = True

        Dim Rowa2 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        Rowa2.Cells(2).Range.Text = "   & CHEMICALS LTD."
        Rowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        Rowa2.Range.Font.Bold = True
        Rowa2.Cells(1).Width = 30
        Rowa2.Cells(2).Width = 200
        Rowa2.Cells(4).Width = 50
        Rowa2.Cells(3).Width = 200
        Rowa2.Range.Font.Size = 12


        Dim newRow22 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        If DocumentStatus = 0 Then
            newRow22.Cells(2).Range.InlineShapes.AddPicture(Class1.global.Signature).Width = 100
            newRow22.Cells(3).Range.InlineShapes.AddPicture(appPath + "\SIGN.jpg").Width = 100

        Else
            newRow22.Cells(2).Range.InlineShapes.AddPicture(appPath + "\blanksign.jpg").Width = 100
            newRow22.Cells(3).Range.InlineShapes.AddPicture(appPath + "\blanksign.jpg").Width = 100
        End If
        newRow22.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        newRow22.Cells(1).Width = 30
        newRow22.Cells(1).Height = 25
        newRow22.Cells(2).Height = 25
        newRow22.Cells(2).Width = 200
        newRow22.Cells(4).Width = 50
        newRow22.Cells(3).Width = 200
        newRow22.Cells(3).Height = 25
        newRow22.Range.Font.Bold = True

        Dim newRow221 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRow221.Height = 20
        newRow221.Cells(2).Range.Text = Convert.ToString(Class1.global.UserName)
        newRow221.Cells(3).Range.Text = "DR. BHAVIN VYAS"
        newRow221.Cells(3).Range.Font.Name = "Times New Roman"
        newRow221.Cells(1).Width = 30
        newRow221.Cells(2).Width = 200
        newRow221.Cells(4).Width = 50
        newRow221.Cells(3).Width = 200


        newRow221.Range.Font.Bold = True
        newRow221.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow221.Cells(3).Range.Font.Name = "Times New Roman"
        newRow221.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

        Dim newRow222 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRow222.Height = 20
        newRow222.Range.Font.Bold = True
        newRow222.Cells(2).Range.Text = "(" + txtDesignation.Text + ")"
        newRow222.Cells(3).Range.Text = "(TECH.DIRECTOR)"
        newRow222.Cells(2).Range.Font.Name = "Times New Roman"
        newRow222.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow222.Cells(3).Range.Font.Name = "Times New Roman"
        newRow222.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

        newRow222.Cells(1).Width = 30
        newRow222.Cells(2).Width = 200
        newRow222.Cells(4).Width = 50
        newRow222.Cells(3).Width = 200


        'ection.HeadersFooters.Add(New HeaderFooter(document, HeaderFooterType.FooterDefault, New Paragraph(document, New Picture(document, "MyImage.png"))))


        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HeaderRo.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next


            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange, Word.WdFieldType.wdFieldPage)
                footerrange.Text = "Corporate Office : D-11,First Floor, Diamond Park, G.I.D.C.,Naroda, Ahmedabad-382330, Gujarat (India) Tele Fax : 91-079-22819065/67/68            export@indianionexchange.com            www.indianionexchange.com          www.bottlingindia.com"
                footerrange.Font.Size = 8
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        Else

            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
        End If


        Dim format As Object = Word.WdSaveFormat.wdFormatPDF
        ' objDoc.SaveAs(appPath + "\Letter1.doc")

        objDoc.Close()
        objDoc = Nothing
        objApp.NormalTemplate.Saved = True
        objApp.Quit()

        objApp = Nothing

        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document
        wordDocument1 = New Word.Document
        wordApplication1 = New Word.Application

        Dim paramSourceDocPath1 As Object = appPath + "\Letter1.doc"
        Dim Targets1 As Object = appPath + "\Letter1.pdf"
        ' wordDocument1 =
        wordDocument1 = wordApplication1.Documents.Open(paramSourceDocPath1)


        Dim formating1 As Object
        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        wordDocument1.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        wordDocument1.Close()
        wordDocument1 = Nothing
        wordApplication1.NormalTemplate.Saved = True
        wordApplication1.Quit()
        wordApplication1 = Nothing

    End Sub

    Public Sub PriceSheet()
        Dim oCollapseEnd As Object = Word.WdCollapseDirection.wdCollapseEnd
        Dim missing As Object = System.Reflection.Missing.Value

        Dim rng As Word.Range

        Dim dt33 As DataTable
        dt33 = New DataTable

        If RblSingle.Checked = True Then
            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price")
            For t1 As Integer = 0 To GvTechnical.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnical.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnical.Rows(t1).Cells(2).Value.ToString(), GvTechnical.Rows(t1).Cells(3).Value.ToString(), GvTechnical.Rows(t1).Cells(4).Value.ToString())

                End If
            Next
        End If


        If RblOther.Checked = True Then
            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price")
            dt33.Columns.Add("Qty")
            dt33.Columns.Add("Total")
            For t1 As Integer = 0 To GvTechnical.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnical.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnical.Rows(t1).Cells(2).Value.ToString(),
                                  GvTechnical.Rows(t1).Cells(3).Value.ToString(), GvTechnical.Rows(t1).Cells(4).Value.ToString(), GvTechnical.Rows(t1).Cells(5).Value.ToString(), GvTechnical.Rows(t1).Cells(6).Value.ToString())

                End If
            Next
        End If



        If RblMultiple.Checked = True Then
            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price1")
            dt33.Columns.Add("Price2")
            For t1 As Integer = 0 To GvTechnical.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnical.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnical.Rows(t1).Cells(2).Value.ToString(), GvTechnical.Rows(t1).Cells(3).Value.ToString(), GvTechnical.Rows(t1).Cells(4).Value.ToString(), GvTechnical.Rows(t1).Cells(5).Value.ToString())

                End If
            Next
        End If


        Dim missing1 As Object = System.Reflection.Missing.Value

        Dim objApp1 As New Word.Application
        Dim objDoc1 As Word.Document = objApp1.Documents.Add(missing1, missing1, missing1, missing1)


        Dim start2 As Object = 0
        Dim end2 As Object = 0

        'objApp1 = New Word.Application
        'objDoc1 = New Word.Document
        Dim oTable2 As Word.Tables = objDoc1.Tables
        Dim rng2 As Word.Range = objDoc1.Range(start2, missing1)
        If RblSingle.Checked = True Then
            oTable2.Add(rng2, 1, 2, missing1, missing1)

            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)

            oTable2.Add(rng2, dt33.Rows.Count + 2, 3, missing1, missing1)
        End If
        If RblOther.Checked = True Then
            oTable2.Add(rng2, 1, 2, missing1, missing1)

            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)
            oTable2.Add(rng2, dt33.Rows.Count + 2, 5, missing1, missing1)
        End If
        If RblMultiple.Checked = True Then
            oTable2.Add(rng2, 1, 2, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)
            oTable2.Add(rng2, dt33.Rows.Count + 2, 4, missing1, missing1)
        End If
        Dim defaultTableBehavior1 As [Object] = Type.Missing
        Dim autoFitBehavior1 As [Object] = Type.Missing

        'objDoc1.Selection.TypeText("")
        ' Dim rng1 As Word.Range = objDoc1.Range(0, 0)
        rng2.Font.Name = "Times New Roman"
        ' oTable2.Range.ParagraphFormat.SpaceAfter = 3
        Dim tbl2 As Word.Table = objDoc1.Tables(1)

        tbl2.Borders.Enable = 0
        objDoc1.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4

        rng2.Font.Name = "Times New Roman"
        tbl2.Range.ParagraphFormat.SpaceAfter = 0

        If RdbEnglish.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PROJECT COST"
            If RblOther.Checked = True Then
                tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
            End If
        ElseIf RdbGujarati.Checked Then
            tbl2.Cell(1, 1).Range.Text = "પ્રોજેક્ટ કિંમત"
            tbl2.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            If RblOther.Checked = True Then
                tbl2.Cell(1, 2).Range.Text = "(કિંમત લાખમાં)"
                tbl2.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            End If
        ElseIf RdbHindi.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PROJECT COST"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        ElseIf RdbMarathi.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PROJECT COST"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        ElseIf RdbTamil.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PROJECT COST"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        ElseIf RdbTelugu.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PROJECT COST"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        End If
        tbl2.Cell(1, 1).Width = 240
        tbl2.Cell(1, 2).Width = 240

        tbl2.Cell(1, 1).Range.Font.Color = Word.WdColor.wdColorWhite
        tbl2.Cell(1, 2).Range.Font.Color = Word.WdColor.wdColorWhite
        tbl2.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        tbl2.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        tbl2.Rows.Item(1).Shading.BackgroundPatternColor = RGB(12, 28, 71)
        tbl2.Rows.Item(1).Range.Font.Name = "Arial"
        tbl2.Rows.Item(1).Range.Font.Bold = True 'Indian' 
        tbl2.Rows.Item(1).Range.Font.Size = 14

        tbl2.Cell(2, 1).Range.Borders.Enable = 0
        tbl2.Cell(2, 2).Range.Borders.Enable = 0
        tbl2.Cell(2, 3).Range.Borders.Enable = 0
        If RblOther.Checked = True Then
            tbl2.Cell(2, 4).Range.Borders.Enable = 0
            tbl2.Cell(2, 5).Range.Borders.Enable = 0

        End If
        If RblMultiple.Checked = True Then
            tbl2.Cell(2, 4).Range.Borders.Enable = 0
        End If


        tbl2.Rows.Item(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        '  newRow2.HeadingFormat = 2

        Dim newRow3 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        If RblSingle.Checked = True Then
            tbl2.Rows.Item(3).Shading.BackgroundPatternColor = RGB(12, 25, 117)
            '            oTable2.Rows.Item(3).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
            If RdbEnglish.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            ElseIf RdbGujarati.Checked Then
                tbl2.Cell(3, 1).Range.Text = "નંબર"
                tbl2.Cell(3, 2).Range.Text = "માહિતી"
            ElseIf RdbHindi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            ElseIf RdbMarathi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            ElseIf RdbTamil.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            ElseIf RdbTelugu.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            End If
            tbl2.Cell(3, 3).Range.Text = "Price"
            tbl2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            tbl2.Rows.Item(3).Range.Font.Size = 10
            tbl2.Cell(3, 2).Width = 300
            tbl2.Cell(3, 1).Width = 70
            tbl2.Cell(3, 3).Width = 110
            tbl2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
            tbl2.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            tbl2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

        End If
        If RblOther.Checked = True Then
            tbl2.Rows.Item(3).Shading.BackgroundPatternColor = RGB(12, 25, 117)



            If RdbEnglish.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "Description"
                tbl2.Cell(3, 3).Range.Text = "Qty"
                tbl2.Cell(3, 4).Range.Text = "PRICE"
                tbl2.Cell(3, 5).Range.Text = "Total"
            ElseIf RdbGujarati.Checked Then
                tbl2.Cell(3, 1).Range.Text = "નંબર"
                tbl2.Cell(3, 2).Range.Text = "માહિતી"
                tbl2.Cell(3, 3).Range.Text = "જથ્થો"
                tbl2.Cell(3, 4).Range.Text = "કિંમત"
                tbl2.Cell(3, 5).Range.Text = "ટોટલ"
            ElseIf RdbHindi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "Description"
                tbl2.Cell(3, 3).Range.Text = "Qty"
                tbl2.Cell(3, 4).Range.Text = "PRICE"
                tbl2.Cell(3, 5).Range.Text = "Total"
            ElseIf RdbMarathi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "Description"
                tbl2.Cell(3, 3).Range.Text = "Qty"
                tbl2.Cell(3, 4).Range.Text = "PRICE"
                tbl2.Cell(3, 5).Range.Text = "Total"
            ElseIf RdbTamil.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "Description"
                tbl2.Cell(3, 3).Range.Text = "Qty"
                tbl2.Cell(3, 4).Range.Text = "PRICE"
                tbl2.Cell(3, 5).Range.Text = "Total"
            ElseIf RdbTelugu.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "Description"
                tbl2.Cell(3, 3).Range.Text = "Qty"
                tbl2.Cell(3, 4).Range.Text = "PRICE"
                tbl2.Cell(3, 5).Range.Text = "Total"
            End If



            tbl2.Cell(3, 2).Width = 200
            tbl2.Cell(3, 1).Width = 70
            tbl2.Cell(3, 3).Width = 30
            tbl2.Cell(3, 4).Width = 110
            tbl2.Cell(3, 5).Width = 70

            tbl2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            tbl2.Rows.Item(3).Range.Font.Size = 10
            tbl2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
            tbl2.Cell(3, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            tbl2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

        End If
        If RblMultiple.Checked = True Then
            tbl2.Rows.Item(3).Shading.BackgroundPatternColor = RGB(12, 25, 117)
            If RdbEnglish.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            ElseIf RdbGujarati.Checked Then
                tbl2.Cell(3, 1).Range.Text = "નંબર"
                tbl2.Cell(3, 2).Range.Text = "માહિતી"
            ElseIf RdbHindi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            ElseIf RdbMarathi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            ElseIf RdbTamil.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            ElseIf RdbTelugu.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SR.NO."
                tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
            End If
            tbl2.Cell(3, 3).Range.Text = txtCapacity1.Text
            tbl2.Cell(3, 4).Range.Text = txtCapacity2.Text

            tbl2.Cell(3, 2).Width = 230
            tbl2.Cell(3, 1).Width = 70
            tbl2.Cell(3, 3).Width = 110
            tbl2.Cell(3, 4).Width = 70

            tbl2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            tbl2.Rows.Item(3).Range.Font.Size = 10
            tbl2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
            tbl2.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            tbl2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If
        Dim finaltotal As New Decimal
        Dim finaltotal1 As New Decimal
        Dim qty As Integer
        qty = 0
        finaltotal = 0


        Dim Cnt As Integer
        Cnt = 1


        For i = 4 To dt33.Rows.Count + 4 - 1

            If RblSingle.Checked = True Then
                '   Dim newRow43 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                tbl2.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                tbl2.Rows.Item(i).Range.Font.Bold = 0
                tbl2.Cell(i, 1).Range.Text = Cnt.ToString()
                tbl2.Cell(i, 2).Range.Text = dt33.Rows(i - 4)(1).ToString()
                tbl2.Cell(i, 3).Range.Text = "`   " + dt33.Rows(i - 4)(2).ToString()
                tbl2.Cell(i, 3).Range.Font.Name = "Rupee"
                tbl2.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                tbl2.Rows.Item(i).Range.Font.Size = 10
                tbl2.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue
                finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i - 4)(2).ToString())

                tbl2.Cell(i, 2).Width = 300
                tbl2.Cell(i, 1).Width = 70
                tbl2.Cell(i, 3).Width = 110



                tbl2.Cell(i, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2.Cell(i, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                tbl2.Cell(i, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            End If
            If RblOther.Checked = True Then
                '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                tbl2.Cell(i, 1).Range.Text = Cnt.ToString()
                tbl2.Cell(i, 2).Range.Text = dt33.Rows(i - 4)(1).ToString()
                tbl2.Cell(i, 3).Range.Text = dt33.Rows(i - 4)(3).ToString()
                tbl2.Cell(i, 4).Range.Text = "`    " + dt33.Rows(i - 4)(2).ToString()
                tbl2.Cell(i, 4).Range.Font.Name = "Rupee"
                tbl2.Cell(i, 5).Range.Font.Name = "Rupee"
                tbl2.Cell(i, 5).Range.Text = "`    " + dt33.Rows(i - 4)(4).ToString()
                tbl2.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                tbl2.Rows.Item(i).Range.Font.Bold = 0
                tbl2.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                tbl2.Rows.Item(i).Range.Font.Size = 10
                tbl2.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue
                tbl2.Cell(i, 2).Width = 200
                tbl2.Cell(i, 1).Width = 70
                tbl2.Cell(i, 3).Width = 30
                tbl2.Cell(i, 4).Width = 110
                tbl2.Cell(i, 5).Width = 70

                tbl2.Cell(i, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2.Cell(i, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2.Cell(i, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                tbl2.Cell(i, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2.Cell(i, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i - 4)(3).ToString())
                qty = qty + Convert.ToDecimal(dt33.Rows(i - 4)(2).ToString())
                finaltotal1 = finaltotal1 + Convert.ToDecimal(dt33.Rows(i - 4)(4).ToString())

            End If
            If RblMultiple.Checked = True Then
                '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

                tbl2.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                tbl2.Rows.Item(i).Range.Font.Bold = 0
                tbl2.Cell(i, 1).Range.Text = Cnt.ToString()
                tbl2.Cell(i, 2).Range.Text = dt33.Rows(i - 4)(1).ToString()
                tbl2.Cell(i, 3).Range.Text = "`    " + dt33.Rows(i - 4)(2).ToString()
                tbl2.Cell(i, 3).Range.Font.Name = "Rupee"
                tbl2.Cell(i, 4).Range.Text = "`    " + dt33.Rows(i - 4)(3).ToString()
                tbl2.Cell(i, 4).Range.Font.Name = "Rupee"
                tbl2.Cell(i, 2).Width = 230
                tbl2.Cell(i, 1).Width = 70
                tbl2.Cell(i, 3).Width = 110
                tbl2.Cell(i, 4).Width = 70



                tbl2.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                tbl2.Rows.Item(i).Range.Font.Size = 9
                tbl2.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue

                tbl2.Cell(i, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2.Cell(i, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2.Cell(i, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                tbl2.Cell(i, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


                finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i - 4)(2).ToString())
                finaltotal1 = finaltotal1 + Convert.ToDecimal(dt33.Rows(i - 4)(3).ToString())
            End If
            Cnt = Cnt + 1
        Next

        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Shading.BackgroundPatternColor = RGB(12, 25, 117)
            newRow4.Cells(1).Range.Text = ""
            If RblPriceYes.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                newRow4.Cells(3).Range.Font.Name = "Rupee"
                newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            Else
                newRow4.Cells(2).Range.Text = ""
                newRow4.Cells(3).Range.Font.Name = ""
                newRow4.Cells(3).Range.Text = ""
            End If

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Range.Font.Size = 10
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Width = 300
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 110
            'newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


        End If
        If RblOther.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Shading.BackgroundPatternColor = RGB(12, 25, 117)
            newRow4.Cells(1).Range.Text = ""
            newRow4.Cells(2).Range.Text = ""

            If RblPriceYes.Checked = True Then
                newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
                newRow4.Cells(3).Range.Font.Name = "Rupee"
                newRow4.Cells(3).Range.Text = ""
                newRow4.Cells(4).Range.Text = "TOTAL PRICE  :"
                newRow4.Cells(5).Range.Text = "`    " + Convert.ToString(finaltotal1)
                newRow4.Cells(5).Range.Font.Name = "Rupee"
            Else
                newRow4.Cells(3).Range.Text = ""
                newRow4.Cells(3).Range.Font.Name = ""
                newRow4.Cells(3).Range.Text = ""
                newRow4.Cells(4).Range.Text = ""
                newRow4.Cells(5).Range.Text = ""
                newRow4.Cells(5).Range.Font.Name = ""
            End If
            newRow4.Cells(2).Width = 200
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 30
            newRow4.Cells(4).Width = 110
            newRow4.Cells(5).Width = 70
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 

            newRow4.Range.Font.Size = 10
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            '            newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If
        If RblMultiple.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4.Shading.BackgroundPatternColor = RGB(12, 25, 117)
            newRow4.Cells(1).Range.Text = ""
            newRow4.Range.Font.Size = 10

            If RblPriceYes.Checked Then
                If RdbEnglish.Checked Then
                    newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                ElseIf RdbGujarati.Checked Then
                    newRow4.Cells(2).Range.Text = "ટોટલ કિંમત :"
                ElseIf RdbHindi.Checked Then
                    newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                ElseIf RdbMarathi.Checked Then
                    newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                ElseIf RdbTamil.Checked Then
                    newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                ElseIf RdbTelugu.Checked Then
                    newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                End If
                newRow4.Cells(3).Range.Font.Name = "Rupee"
                newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
                newRow4.Cells(4).Range.Font.Name = "Rupee"
                newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
            Else
                newRow4.Cells(2).Range.Text = ""
                newRow4.Cells(3).Range.Font.Name = ""
                newRow4.Cells(3).Range.Text = ""
                newRow4.Cells(4).Range.Font.Name = ""
                newRow4.Cells(4).Range.Text = ""

            End If

            newRow4.Cells(1).Width = 70
            newRow4.Cells(2).Width = 210
            newRow4.Cells(3).Width = 110
            newRow4.Cells(4).Width = 90

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            '           newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If
        Dim newRow51 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow51.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
        newRow51.Cells(1).Range.Text = ""
        newRow51.Cells(2).Range.Text = ""
        newRow51.Cells(3).Range.Text = ""
        newRow51.Cells(2).Range.Font.Bold = True 'Indian' 
        newRow51.Cells(2).Range.Font.Size = 14
        newRow51.Cells(2).Range.Font.Name = "Times New Roman"
        newRow51.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
        newRow51.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
        newRow51.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
        newRow51.Cells(2).Range.Borders.Enable = 0
        newRow51.Cells(1).Range.Borders.Enable = 0
        newRow51.Cells(3).Range.Borders.Enable = 0


        If RblOther.Checked = True Then
            newRow51.Cells(4).Range.Borders.Enable = 0
            newRow51.Cells(5).Range.Borders.Enable = 0
        End If
        If RblMultiple.Checked = True Then
            newRow51.Cells(4).Range.Borders.Enable = 0
        End If


        If FlagPdf = 1 Then

            Dim objApp1t As New Word.Application
            Dim objDoc1t As Word.Document = objApp1t.Documents.Add(missing1, missing1, missing1, missing1)

            Dim start2t As Object = 0
            Dim end2t As Object = 0

            'objApp1 = New Word.Application
            'objDoc1 = New Word.Document
            Dim oTable2t As Word.Tables = objDoc1t.Tables
            Dim rng2t As Word.Range = objDoc1t.Range(start2t, missing1)
            If RblSingle.Checked = True Then
                oTable2t.Add(rng2t, 1, 2, missing1, missing1)

                start2t = objDoc1t.Tables(1).Range.[End]
                rng2t = objDoc1t.Range(start2t, missing1)

                oTable2t.Add(rng2t, dt33.Rows.Count + 2, 3, missing1, missing1)
            End If
            If RblOther.Checked = True Then
                oTable2t.Add(rng2t, 1, 2, missing1, missing1)

                start2t = objDoc1t.Tables(1).Range.[End]
                rng2t = objDoc1t.Range(start2t, missing1)

                oTable2t.Add(rng2t, dt33.Rows.Count + 2, 5, missing1, missing1)
            End If
            If RblMultiple.Checked = True Then
                oTable2t.Add(rng2t, 1, 2, missing1, missing1)

                start2t = objDoc1t.Tables(1).Range.[End]
                rng2t = objDoc1t.Range(start2t, missing1)

                oTable2t.Add(rng2t, dt33.Rows.Count + 2, 4, missing1, missing1)
            End If
            Dim defaultTableBehavior1t As [Object] = Type.Missing
            Dim autoFitBehavior1t As [Object] = Type.Missing

            'objDoc1.Selection.TypeText("Refzxczxxczccxxxxxxxxxxxxcxcxcccxcxcxcxcxccxxcxccxccxcxcc")
            ' Dim rng1 As Word.Range = objDoc1.Range(0, 0)
            rng2t.Font.Name = "Times New Roman"
            ' oTable2.Range.ParagraphFormat.SpaceAfter = 3
            Dim tbl2t As Word.Table = objDoc1t.Tables(1)

            tbl2t.Borders.Enable = 0
            objDoc1t.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4

            rng2t.Font.Name = "Times New Roman"
            tbl2t.Range.ParagraphFormat.SpaceAfter = 0

            If RdbEnglish.Checked Then
                tbl2t.Cell(1, 1).Range.Text = "PROJECT COST"
                tbl2t.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
            ElseIf RdbGujarati.Checked Then
                tbl2t.Cell(1, 1).Range.Text = "પ્રોજેક્ટ કિંમત"
                tbl2t.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2t.Cell(1, 2).Range.Text = "(કિંમત લાખમાં)"
                tbl2t.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            ElseIf RdbHindi.Checked Then
                tbl2t.Cell(1, 1).Range.Text = "PROJECT COST"
                tbl2t.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
            ElseIf RdbMarathi.Checked Then
                tbl2t.Cell(1, 1).Range.Text = "PROJECT COST"
                tbl2t.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
            ElseIf RdbTamil.Checked Then
                tbl2t.Cell(1, 1).Range.Text = "PROJECT COST"
                tbl2t.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
            ElseIf RdbTelugu.Checked Then
                tbl2t.Cell(1, 1).Range.Text = "PROJECT COST"
                tbl2t.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
            End If

            tbl2t.Cell(1, 1).Range.Font.Color = Word.WdColor.wdColorWhite
            tbl2t.Rows.Item(1).Shading.BackgroundPatternColor = RGB(12, 28, 71)
            tbl2t.Rows.Item(1).Range.Font.Name = "Arial"
            tbl2t.Rows.Item(1).Range.Font.Bold = True 'Indian' 
            tbl2t.Rows.Item(1).Range.Font.Size = 14

            tbl2t.Cell(2, 1).Range.Borders.Enable = 0
            tbl2t.Cell(2, 2).Range.Borders.Enable = 0
            tbl2t.Cell(2, 3).Range.Borders.Enable = 0
            If RblOther.Checked = True Then
                tbl2t.Cell(2, 4).Range.Borders.Enable = 0
                tbl2t.Cell(2, 5).Range.Borders.Enable = 0

            End If
            If RblMultiple.Checked = True Then
                tbl2t.Cell(2, 4).Range.Borders.Enable = 0
            End If


            tbl2t.Rows.Item(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '  newRow2.HeadingFormat = 2

            Dim newRow3t As Word.Row = objDoc1t.Tables(1).Rows.Add(Type.Missing)
            If RblSingle.Checked = True Then
                tbl2t.Rows.Item(3).Shading.BackgroundPatternColor = RGB(12, 25, 117)
                '            oTable2.Rows.Item(3).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
                If RdbEnglish.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "DESCRIPTION"
                ElseIf RdbGujarati.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "નંબર"
                    tbl2t.Cell(3, 2).Range.Text = "માહિતી"
                ElseIf RdbHindi.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "DESCRIPTION"
                ElseIf RdbMarathi.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "DESCRIPTION"
                ElseIf RdbTamil.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "DESCRIPTION"
                ElseIf RdbTelugu.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "DESCRIPTION"
                End If
                tbl2t.Cell(3, 3).Range.Text = txtCapacity1.Text + " LPH"
                tbl2t.Rows.Item(3).Range.Font.Bold = True 'Indian' 
                tbl2t.Rows.Item(3).Range.Font.Size = 10
                tbl2t.Cell(3, 2).Width = 300
                tbl2t.Cell(3, 1).Width = 70
                tbl2t.Cell(3, 3).Width = 110
                tbl2t.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
                tbl2t.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2t.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                tbl2t.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            End If
            If RblOther.Checked = True Then
                tbl2t.Rows.Item(3).Shading.BackgroundPatternColor = RGB(12, 25, 117)



                If RdbEnglish.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "Description"
                    tbl2t.Cell(3, 3).Range.Text = "Qty"
                    tbl2t.Cell(3, 4).Range.Text = "PRICE"
                    tbl2t.Cell(3, 5).Range.Text = "Total"
                ElseIf RdbGujarati.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "નંબર"
                    tbl2t.Cell(3, 2).Range.Text = "માહિતી"
                    tbl2t.Cell(3, 3).Range.Text = "જથ્થો"
                    tbl2t.Cell(3, 4).Range.Text = "કિંમત"
                    tbl2t.Cell(3, 5).Range.Text = "ટોટલ"
                ElseIf RdbHindi.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "Description"
                    tbl2t.Cell(3, 3).Range.Text = "Qty"
                    tbl2t.Cell(3, 4).Range.Text = "PRICE"
                    tbl2t.Cell(3, 5).Range.Text = "Total"
                ElseIf RdbMarathi.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "Description"
                    tbl2t.Cell(3, 3).Range.Text = "Qty"
                    tbl2t.Cell(3, 4).Range.Text = "PRICE"
                    tbl2t.Cell(3, 5).Range.Text = "Total"
                ElseIf RdbTamil.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "Description"
                    tbl2t.Cell(3, 3).Range.Text = "Qty"
                    tbl2t.Cell(3, 4).Range.Text = "PRICE"
                    tbl2t.Cell(3, 5).Range.Text = "Total"
                ElseIf RdbTelugu.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "Description"
                    tbl2t.Cell(3, 3).Range.Text = "Qty"
                    tbl2t.Cell(3, 4).Range.Text = "PRICE"
                    tbl2t.Cell(3, 5).Range.Text = "Total"
                End If



                tbl2t.Cell(3, 2).Width = 200
                tbl2t.Cell(3, 1).Width = 70
                tbl2t.Cell(3, 3).Width = 30
                tbl2t.Cell(3, 4).Width = 110
                tbl2t.Cell(3, 5).Width = 70

                tbl2t.Rows.Item(3).Range.Font.Bold = True 'Indian' 
                tbl2t.Rows.Item(3).Range.Font.Size = 10
                tbl2t.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
                tbl2t.Cell(3, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2t.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2t.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2t.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                tbl2t.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            End If
            If RblMultiple.Checked = True Then
                tbl2t.Rows.Item(3).Shading.BackgroundPatternColor = RGB(12, 25, 117)
                If RdbEnglish.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "DESCRIPTION"
                ElseIf RdbGujarati.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "નંબર"
                    tbl2t.Cell(3, 2).Range.Text = "માહિતી"
                ElseIf RdbHindi.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "DESCRIPTION"
                ElseIf RdbMarathi.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "DESCRIPTION"
                ElseIf RdbTamil.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2.Cell(3, 2).Range.Text = "DESCRIPTION"
                ElseIf RdbTelugu.Checked Then
                    tbl2t.Cell(3, 1).Range.Text = "SR.NO."
                    tbl2t.Cell(3, 2).Range.Text = "DESCRIPTION"
                End If
                tbl2t.Cell(3, 3).Range.Text = txtCapacity1.Text
                tbl2t.Cell(3, 4).Range.Text = txtCapacity2.Text

                tbl2t.Cell(3, 2).Width = 230
                tbl2t.Cell(3, 1).Width = 70
                tbl2t.Cell(3, 3).Width = 110
                tbl2t.Cell(3, 4).Width = 70

                tbl2t.Rows.Item(3).Range.Font.Bold = True 'Indian' 
                tbl2t.Rows.Item(3).Range.Font.Size = 10
                tbl2t.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
                tbl2t.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2t.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                tbl2t.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                tbl2t.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            End If
            Dim finaltotalt As New Decimal
            Dim finaltotal1t As New Decimal
            Dim qtyt As Integer
            qtyt = 0
            finaltotalt = 0





            For i = 4 To dt33.Rows.Count + 4 - 1

                If RblSingle.Checked = True Then
                    '   Dim newRow43 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    tbl2t.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    tbl2t.Rows.Item(i).Range.Font.Bold = 0
                    tbl2t.Cell(i, 1).Range.Text = dt33.Rows(i - 4)(0).ToString()
                    tbl2t.Cell(i, 2).Range.Text = dt33.Rows(i - 4)(1).ToString()
                    tbl2t.Cell(i, 3).Range.Text = "`   " + dt33.Rows(i - 4)(2).ToString()
                    tbl2t.Cell(i, 3).Range.Font.Name = "Rupee"
                    tbl2t.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                    tbl2t.Rows.Item(i).Range.Font.Size = 10
                    tbl2t.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue
                    finaltotalt = finaltotalt + Convert.ToDecimal(dt33.Rows(i - 4)(2).ToString())

                    tbl2t.Cell(i, 2).Width = 300
                    tbl2t.Cell(i, 1).Width = 70
                    tbl2t.Cell(i, 3).Width = 110



                    tbl2t.Cell(i, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    tbl2t.Cell(i, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    tbl2t.Cell(i, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


                End If
                If RblOther.Checked = True Then
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    tbl2t.Cell(i, 1).Range.Text = dt33.Rows(i - 4)(0).ToString()
                    tbl2t.Cell(i, 2).Range.Text = dt33.Rows(i - 4)(1).ToString()
                    tbl2t.Cell(i, 3).Range.Text = dt33.Rows(i - 4)(3).ToString()
                    tbl2t.Cell(i, 4).Range.Text = "`    " + dt33.Rows(i - 4)(2).ToString()
                    tbl2t.Cell(i, 4).Range.Font.Name = "Rupee"
                    tbl2t.Cell(i, 5).Range.Font.Name = "Rupee"
                    tbl2t.Cell(i, 5).Range.Text = "`    " + dt33.Rows(i - 4)(4).ToString()
                    tbl2t.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    tbl2t.Rows.Item(i).Range.Font.Bold = 0
                    tbl2t.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                    tbl2t.Rows.Item(i).Range.Font.Size = 10
                    tbl2t.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue
                    tbl2t.Cell(i, 2).Width = 200
                    tbl2t.Cell(i, 1).Width = 70
                    tbl2t.Cell(i, 3).Width = 30
                    tbl2t.Cell(i, 4).Width = 110
                    tbl2t.Cell(i, 5).Width = 70



                    tbl2t.Cell(i, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    tbl2t.Cell(i, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    tbl2t.Cell(i, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    tbl2t.Cell(i, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    tbl2t.Cell(i, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    finaltotalt = finaltotalt + Convert.ToDecimal(dt33.Rows(i - 4)(3).ToString())
                    qtyt = qtyt + Convert.ToDecimal(dt33.Rows(i - 4)(2).ToString())
                    finaltotal1t = finaltotal1t + Convert.ToDecimal(dt33.Rows(i - 4)(4).ToString())

                End If
                If RblMultiple.Checked = True Then
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

                    tbl2t.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    tbl2t.Rows.Item(i).Range.Font.Bold = 0
                    tbl2t.Cell(i, 1).Range.Text = dt33.Rows(i - 4)(0).ToString()
                    tbl2t.Cell(i, 2).Range.Text = dt33.Rows(i - 4)(1).ToString()
                    tbl2t.Cell(i, 3).Range.Text = "`    " + dt33.Rows(i - 4)(2).ToString()
                    tbl2t.Cell(i, 3).Range.Font.Name = "Rupee"
                    tbl2t.Cell(i, 4).Range.Text = "`    " + dt33.Rows(i - 4)(3).ToString()
                    tbl2t.Cell(i, 4).Range.Font.Name = "Rupee"
                    tbl2t.Cell(i, 2).Width = 230
                    tbl2t.Cell(i, 1).Width = 70
                    tbl2t.Cell(i, 3).Width = 110
                    tbl2t.Cell(i, 4).Width = 70



                    tbl2t.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                    tbl2t.Rows.Item(i).Range.Font.Size = 10
                    tbl2t.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue

                    tbl2t.Cell(i, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    tbl2t.Cell(i, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    tbl2t.Cell(i, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    tbl2t.Cell(i, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


                    finaltotalt = finaltotalt + Convert.ToDecimal(dt33.Rows(i - 4)(2).ToString())
                    finaltotal1t = finaltotalt + Convert.ToDecimal(dt33.Rows(i - 4)(3).ToString())
                End If
            Next

            If RblSingle.Checked = True Then
                Dim newRow4 As Word.Row = objDoc1t.Tables(1).Rows.Add(Type.Missing)
                newRow4.Shading.BackgroundPatternColor = RGB(12, 25, 117)
                newRow4.Cells(1).Range.Text = ""
                If RblPriceYes.Checked Then
                    newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                    newRow4.Cells(3).Range.Font.Name = "Rupee"
                    newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotalt)
                Else
                    newRow4.Cells(2).Range.Text = ""
                    newRow4.Cells(3).Range.Font.Name = ""
                    newRow4.Cells(3).Range.Text = ""
                End If

                newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
                newRow4.Cells(1).Range.Font.Size = 12
                newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4.Cells(2).Width = 300
                newRow4.Cells(1).Width = 70
                newRow4.Cells(3).Width = 110
                'newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            End If
            If RblOther.Checked = True Then
                Dim newRow4 As Word.Row = objDoc1t.Tables(1).Rows.Add(Type.Missing)
                newRow4.Shading.BackgroundPatternColor = RGB(12, 25, 117)
                newRow4.Cells(1).Range.Text = ""
                newRow4.Cells(2).Range.Text = ""

                If RblPriceYes.Checked = True Then
                    newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotalt)
                    newRow4.Cells(3).Range.Font.Name = "Rupee"
                    newRow4.Cells(3).Range.Text = ""
                    newRow4.Cells(4).Range.Text = "TOTAL PRICE  :"
                    newRow4.Cells(5).Range.Text = "`    " + Convert.ToString(finaltotal1t)
                    newRow4.Cells(5).Range.Font.Name = "Rupee"
                Else
                    newRow4.Cells(3).Range.Text = ""
                    newRow4.Cells(3).Range.Font.Name = ""
                    newRow4.Cells(3).Range.Text = ""
                    newRow4.Cells(4).Range.Text = ""
                    newRow4.Cells(5).Range.Text = ""
                    newRow4.Cells(5).Range.Font.Name = ""
                End If
                newRow4.Cells(2).Width = 200
                newRow4.Cells(1).Width = 70
                newRow4.Cells(3).Width = 30
                newRow4.Cells(4).Width = 110
                newRow4.Cells(5).Width = 70
                newRow4.Cells(1).Range.Font.Bold = True 'Indian' 

                newRow4.Cells(1).Range.Font.Size = 12
                newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                '            newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            End If
            If RblMultiple.Checked = True Then
                Dim newRow4 As Word.Row = objDoc1t.Tables(1).Rows.Add(Type.Missing)

                newRow4.Shading.BackgroundPatternColor = RGB(12, 25, 117)
                newRow4.Cells(1).Range.Text = ""
                If RblPriceYes.Checked Then
                    If RdbEnglish.Checked Then
                        newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                    ElseIf RdbGujarati.Checked Then
                        newRow4.Cells(2).Range.Text = "ટોટલ કિંમત :"
                    ElseIf RdbHindi.Checked Then
                        newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                    ElseIf RdbMarathi.Checked Then
                        newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                    ElseIf RdbTamil.Checked Then
                        newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                    ElseIf RdbTelugu.Checked Then
                        newRow4.Cells(2).Range.Text = "TOTAL PRICE  :"
                    End If
                    newRow4.Cells(3).Range.Font.Name = "Rupee"
                    newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
                Else
                    newRow4.Cells(2).Range.Text = ""
                    newRow4.Cells(3).Range.Font.Name = ""
                    newRow4.Cells(3).Range.Text = ""
                    newRow4.Cells(4).Range.Font.Name = ""
                    newRow4.Cells(4).Range.Text = ""

                End If

                newRow4.Cells(2).Width = 230
                newRow4.Cells(1).Width = 70
                newRow4.Cells(3).Width = 110
                newRow4.Cells(4).Width = 70

                newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
                newRow4.Cells(1).Range.Font.Size = 12
                newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

                newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                '           newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            End If
            Dim newRow51t As Word.Row = objDoc1t.Tables(1).Rows.Add(Type.Missing)
            newRow51t.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow51t.Cells(1).Range.Text = ""
            newRow51t.Cells(2).Range.Text = ""
            newRow51t.Cells(3).Range.Text = ""
            newRow51t.Cells(2).Range.Font.Bold = True 'Indian' 
            newRow51t.Cells(2).Range.Font.Size = 14
            newRow51t.Cells(2).Range.Font.Name = "Times New Roman"
            newRow51t.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow51t.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow51t.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow51t.Cells(2).Range.Borders.Enable = 0
            newRow51t.Cells(1).Range.Borders.Enable = 0
            newRow51t.Cells(3).Range.Borders.Enable = 0


            If RblOther.Checked = True Then
                newRow51t.Cells(4).Range.Borders.Enable = 0
                newRow51t.Cells(5).Range.Borders.Enable = 0
            End If
            If RblMultiple.Checked = True Then
                newRow51t.Cells(4).Range.Borders.Enable = 0
            End If
            objDoc1t.SaveAs(appPath + "\OrderData" + "\" + Class1.global.GobalMaxId.ToString() + ".doc")
            objDoc1t.Close()
            objDoc1t = Nothing
            objApp1t.Quit()

            objApp1t = Nothing


        End If


        If RblSingle.Checked = True Or RblOther.Checked = True Then

            Dim newRowa21 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowa21.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowa21.Range.ParagraphFormat.SpaceAfter = 0
            newRowa21.Range.ParagraphFormat.SpaceBefore = 0

            newRowa21.Cells(1).Range.Text = ""
            newRowa21.Range.Borders.Enable = 0
            newRowa21.Range.Font.Size = 2
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Range.Font.Color = Word.WdColor.wdColorBlack
            newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            newRowa21.Cells(1).Width = 400
            newRowa21.Cells(2).Range.Text = ""
            newRowa21.Cells(2).Range.Font.Name = "Rupee"
            newRowa21.Cells(2).Width = 80

            Dim specialTotal As Decimal
            specialTotal = 0
            specialTotal = finaltotal
            If txtspdiscount.Text <> "" Then


                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Range.Font.Color = Word.WdColor.wdColorBlack


                newRowa21.Cells(1).Range.Text = "SPECIAL DISCOUNT = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtspdiscount.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight


                ''with discount
                Dim afterdisc As Decimal
                afterdisc = 0
                afterdisc = finaltotal - Convert.ToDecimal(txtspdiscount.Text)
                specialTotal = afterdisc
                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + afterdisc.ToString("N2")
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

            End If





            ''with vat tax
            If txtVat.Text <> "" Then

                Dim vattotal As Decimal
                Dim vatPerc As Decimal

                ''                vattotal = (FinalSysAllTotal) * Convert.ToDecimal(txtVat.Text)
                ''              vattotal = vattotal / 100
                vattotal = Convert.ToDecimal(txtVat.Text)
                Try
                    vatPerc = (vattotal * 100) / finaltotal
                Catch ex As Exception
                    vatPerc = 0
                End Try

                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                'newRowa21.Cells(1).Range.Text = "+ VAT (" + vatPerc.ToString("N2") + ") = = > >"
                newRowa21.Cells(1).Range.Text = "+ GST= = > >"

                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + vattotal.ToString("N2")
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                specialTotal = specialTotal + vattotal

            End If


            If txtTransporation.Text <> "" Then
                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "+ TRANSPORATION (APX.) = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtTransporation.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                specialTotal = specialTotal + Convert.ToDecimal(txtTransporation.Text)
            End If

            If txtInsurance.Text <> "" Then
                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "+ INSURANCE  (APX.)  = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtInsurance.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                specialTotal = specialTotal + Convert.ToDecimal(txtInsurance.Text)

            End If


            If txtPakingforwarding.Text <> "" Then

                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "+ PACKING & FORWADING CHARGES = = >>"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtPakingforwarding.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                specialTotal = specialTotal + Convert.ToDecimal(txtPakingforwarding.Text)

            End If
            If txtErection.Text <> "" Then

                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "+ ERECTION & COMMISSIONING CHARGES = = >> "
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtErection.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                specialTotal = specialTotal + Convert.ToDecimal(txtErection.Text)

            End If
            'navin 19-03-2014
            'txtFinalPrice.Text = specialTotal.ToString()
            txtFinalPrice.Text = specialTotal.ToString("N2")



            If txtspdiscount.Text.Trim() = "" And txtVat.Text.Trim() = "" And txtTransporation.Text.Trim() = "" And txtInsurance.Text.Trim() = "" And txtPakingforwarding.Text.Trim() = "" And txtErection.Text.Trim() = "" Then
            Else
                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0


                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                'navin 19-03-2014
                ' newRowa21.Cells(1).Range.Text = "FINAL COST OF ENTIRE MINERAL WATER PROJECT"
                newRowa21.Cells(1).Range.Text = "FINAL COST "
                newRowa21.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack

                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                newRowa21.Range.Font.Color = Word.WdColor.wdColorWhite
                newRowa21.Range.Font.Size = 10
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                If RblPriceYes.Checked = True Then
                    newRowa21.Cells(2).Range.Text = "` " + txtFinalPrice.Text
                Else
                    newRowa21.Cells(2).Range.Text = ""
                End If
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

            End If


        Else




            Dim newRowa21 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite

            newRowa21.Range.Font.Color = Word.WdColor.wdColorWhite
            newRowa21.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PROJECT"
            newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowa21.Cells(1).Width = 320
            newRowa21.Cells(2).Range.Font.Name = "Rupee"
            newRowa21.Cells(3).Range.Font.Name = "Rupee"
            newRowa21.Cells(2).Range.Text = "` " + Convert.ToString(finaltotal)
            newRowa21.Cells(3).Range.Text = "` " + Convert.ToString(finaltotal1)
            newRowa21.Cells(2).Width = 78
            newRowa21.Cells(3).Width = 80

            newRowa21.Range.ParagraphFormat.SpaceAfter = 0
            newRowa21.Range.ParagraphFormat.SpaceBefore = 0
            newRowa21.Range.Borders.Enable = 0
            newRowa21.Range.Font.Size = 2
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone



            Dim specialTotal As Decimal
            Dim specialmuTotal As Decimal

            specialTotal = 0
            specialTotal = finaltotal1
            If txtspdiscount.Text <> "" Then


                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.Font.Color = Word.WdColor.wdColorBlack
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                newRowa21.Cells(1).Range.Text = "SPECIAL DISCOUNT = = > >"
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = txtspdiscount.Text
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa21.Cells(3).Range.Text = txtspdiscount.Text
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                ''with discount
                Dim afterdisc As Decimal
                afterdisc = 0
                afterdisc = finaltotal - Convert.ToDecimal(txtspdiscount.Text)
                specialTotal = afterdisc
                specialmuTotal = finaltotal1 - Convert.ToDecimal(txtspdiscount.Text)

                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = Convert.ToString(afterdisc)
                afterdisc = finaltotal1 - Convert.ToDecimal(txtspdiscount.Text)
                specialTotal = afterdisc

                newRowa21.Cells(3).Range.Text = Convert.ToString(afterdisc)
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            End If





            ''with vat tax
            If txtVat.Text <> "" Then

                'Dim vattotal As Decimal
                'vattotal = (FinalSysAllTotal) * Convert.ToDecimal(txtVat.Text)
                'vattotal = vattotal / 100

                Dim vattotal As Decimal
                Dim vatPerc As Decimal

                ''                vattotal = (FinalSysAllTotal) * Convert.ToDecimal(txtVat.Text)
                ''              vattotal = vattotal / 100
                vattotal = Convert.ToDecimal(txtVat.Text)
                Try
                    vatPerc = (vattotal * 100) / finaltotal1
                Catch ex As Exception
                    vatPerc = 0
                End Try

                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "+ VAT (" + vatPerc.ToString("N2") + ") = = > >"

                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + Convert.ToString(vattotal)
                'vattotal = (finalsysmutotal) * Convert.ToDecimal(txtVat.Text)
                'vattotal = vattotal / 100

                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(3).Range.Text = vattotal
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                specialTotal = specialTotal + vattotal
                specialmuTotal = specialmuTotal + vattotal

            End If



            If txtTransporation.Text <> "" Then
                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "+ TRANSPORATION (APX.) = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtTransporation.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                newRowa21.Cells(3).Range.Text = "` " + txtTransporation.Text
                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                specialTotal = specialTotal + Convert.ToDecimal(txtTransporation.Text)
                specialmuTotal = specialmuTotal + Convert.ToDecimal(txtTransporation.Text)
            End If

            If txtInsurance.Text <> "" Then
                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "+ INSURANCE  (APX.)  = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtInsurance.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                newRowa21.Cells(3).Range.Text = "` " + txtInsurance.Text
                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


                specialTotal = specialTotal + Convert.ToDecimal(txtInsurance.Text)
                specialmuTotal = specialmuTotal + Convert.ToDecimal(txtInsurance.Text)
            End If


            If txtPakingforwarding.Text <> "" Then

                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "+ PACKING & FORWADING CHARGES = = >>"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtPakingforwarding.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                newRowa21.Cells(3).Range.Text = "` " + txtPakingforwarding.Text
                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


                specialTotal = specialTotal + Convert.ToDecimal(txtPakingforwarding.Text)
                specialmuTotal = specialmuTotal + Convert.ToDecimal(txtPakingforwarding.Text)

            End If
            If txtErection.Text <> "" Then
                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
                newRowa21.Range.Font.Size = 9
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Text = "+ ERECTION & COMMISSIONING CHARGES = = >> "
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtErection.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                newRowa21.Cells(3).Range.Text = "` " + txtErection.Text
                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


                specialTotal = specialTotal + Convert.ToDecimal(txtErection.Text)
                specialmuTotal = specialmuTotal + Convert.ToDecimal(txtErection.Text)
            End If
            'navin 19-03-2014

            ' txtFinalPrice.Text = Convert.ToString(specialTotal + specialmuTotal)

            txtFinalPrice.Text = Convert.ToDecimal(specialTotal + specialmuTotal).ToString("N2")

            If txtspdiscount.Text.Trim() = "" And txtVat.Text.Trim() = "" And txtTransporation.Text.Trim() = "" And txtInsurance.Text.Trim() = "" And txtPakingforwarding.Text.Trim() = "" And txtErection.Text.Trim() = "" Then
            Else

                newRowa21 = objDoc1.Tables(1).Rows.Add(Type.Missing)


                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                'navin 19-03-2014
                ' newRowa21.Cells(1).Range.Text = "FINAL COST OF ENTIRE MINERAL WATER PROJECT"
                newRowa21.Cells(1).Range.Text = "FINAL COST "
                newRowa21.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack

                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                newRowa21.Range.Font.Color = Word.WdColor.wdColorWhite
                newRowa21.Range.Font.Size = 10
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0


                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)

                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                newRowa21.Range.Font.Color = Word.WdColor.wdColorWhite
                newRowa21.Cells(2).Range.Font.Name = "Rupee"

                newRowa21.Cells(2).Range.Text = "` " + Convert.ToString(specialTotal)
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


                newRowa21.Cells(3).Range.Text = "` " + Convert.ToString(specialmuTotal)
                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If
        End If






        Dim newRow5 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow5.Range.Font.Size = 12
        'newRow5.Cells(1).Shading.BackgroundPatternColor = RGB(0, 102, 153)
        newRow5.Shading.BackgroundPatternColor = RGB(256, 256, 256)
        newRow5.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRow5.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRow5.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRow5.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRow5.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRow5.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRow5.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRow5.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRow5.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRow5.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRow5.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRow5.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        If RblSingle.Checked = True Then
            If RdbEnglish.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbGujarati.Checked Then
                newRow5.Cells(1).Range.Text = "નિયમો અને શરતો"
            ElseIf RdbHindi.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbMarathi.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbTamil.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbTelugu.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            End If


            newRow5.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow5.Cells(2).Range.Text = ""
            newRow5.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow5.Cells(3).Range.Text = ""
            newRow5.Cells(2).Width = 0
            newRow5.Cells(1).Width = 250
            newRow5.Cells(3).Width = 0
        End If
        If RblOther.Checked = True Then
            newRow5.Cells(2).Width = 0
            newRow5.Cells(1).Width = 250
            newRow5.Cells(3).Width = 0
            newRow5.Cells(4).Width = 0
            newRow5.Cells(5).Width = 0
            If RdbEnglish.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbGujarati.Checked Then
                newRow5.Cells(1).Range.Text = "નિયમો અને શરતો"
            ElseIf RdbHindi.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbMarathi.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbTamil.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbTelugu.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            End If
            newRow5.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow5.Cells(2).Range.Text = ""
            newRow5.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow5.Cells(3).Range.Text = ""
        End If
        If RblMultiple.Checked = True Then
            If RdbEnglish.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbGujarati.Checked Then
                newRow5.Cells(1).Range.Text = "નિયમો અને શરતો"
            ElseIf RdbHindi.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbMarathi.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbTamil.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            ElseIf RdbTelugu.Checked Then
                newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS"
            End If
            newRow5.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow5.Cells(2).Range.Text = ""
            newRow5.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow5.Cells(3).Range.Text = ""
            newRow5.Cells(2).Width = 0
            newRow5.Cells(1).Width = 250
            newRow5.Cells(3).Width = 0
            newRow5.Cells(4).Width = 0

        End If


        newRow5.Cells(2).Range.Font.Bold = True 'Indian' 
        newRow5.Cells(3).Range.Font.Bold = True 'Indian' 
        newRow5.Cells(2).Range.Font.Size = 12
        newRow5.Cells(3).Range.Font.Size = 12
        newRow5.Cells(3).Range.Font.Name = "Times New Roman"
        newRow5.Cells(2).Range.Font.Name = "Times New Roman"
        newRow5.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
        newRow5.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
        newRow5.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite

        newRow5.Cells(1).Shading.BackgroundPatternColor = RGB(0, 102, 153)



        Dim strt2 As Object = objDoc1.Tables(1).Range.[End]
        Dim oCollapseEnd2 As Object = Word.WdCollapseDirection.wdCollapseEnd

        Dim ran2 As Word.Range = objDoc1.Range(strt2, strt2)
        rng = objDoc1.Content
        rng.Collapse(oCollapseEnd)
        '  Dim oTable2 As Word.Table = objDoc.Tables.Add(ran, 5, 5, missing, missing)
        Dim oTable4 As Word.Table = objDoc1.Tables.Add(ran2, 1, 1, missing, missing)
        '    newRow4.HeadingFormat = 3
        objDoc1.Tables(1).Range.ParagraphFormat.SpaceAfter = 3.5



        If txt1.Text.Trim() <> "" Then
            Dim newRow6 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow6.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
            newRow6.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow6.Cells(1).Range.Text = txt1.Text
            newRow6.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow6.Cells(1).Range.Font.Size = 12
            newRow6.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow6.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow6.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
            newRow6.Range.ListFormat.ListOutdent()
        End If
        If txt2.Text.Trim() <> "" Then
            Dim newRow7 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow7.Cells(1).Range.Text = txt2.Text
            newRow7.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow7.Cells(1).Range.Font.Size = 12
            newRow7.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow7.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow7.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
        End If


        If txt3.Text.Trim() <> "" Then
            Dim newRow8 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow8.Cells(1).Range.Text = txt3.Text
            newRow8.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow8.Cells(1).Range.Font.Size = 12
            newRow8.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow8.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow8.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
            '  newRow8.Range.ListFormat.ListIndent()
        End If

        If txt4.Text.Trim() <> "" Then
            Dim newRow9 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow9.Cells(1).Range.Text = txt4.Text
            newRow9.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
            newRow9.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow9.Cells(1).Range.Font.Size = 12
            newRow9.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow9.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow9.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustFirstColumn)
            newRow9.Range.ListFormat.ListOutdent()
            newRow9.Range.ListFormat.ApplyBulletDefault(Nothing)
        End If

        'Added by Rajesh
        'export terms

        If rdbExport.Checked = True Then

            Dim newRow99 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow99.Cells(1).Range.Text = txtPayTerms.Text
            newRow99.Cells(1).Range.Font.Name = "Times New Roman"
            newRow99.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
            newRow99.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow99.Cells(1).Range.Font.Size = 12
            newRow99.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow99.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow99.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustFirstColumn)
            newRow99.Range.ListFormat.ListOutdent()
            newRow99.Range.ListFormat.ApplyBulletDefault(Nothing)


        End If



        If (rdbTerms2.Checked = True) Then
            If txt41.Text.Trim() <> "" Then
                Dim newRow81 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

                newRow81.Cells(1).Range.Text = txt41.Text
                newRow81.Cells(1).Range.Font.Bold = True 'Indian' 
                newRow81.Cells(1).Range.Font.Size = 12
                newRow81.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                newRow81.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow81.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)

            End If

            If txt42.Text.Trim() <> "" Then
                Dim newRow82 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

                newRow82.Cells(1).Range.Text = txt42.Text
                newRow82.Range.ListFormat.ApplyBulletDefault(Nothing)
                newRow82.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow82.Cells(1).Range.Font.Bold = True 'Indian' 
                newRow82.Cells(1).Range.Font.Size = 12
                newRow82.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                newRow82.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow82.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow82.Range.ListFormat.ListOutdent()
                newRow82.Range.ListFormat.ApplyBulletDefault(Nothing)
            End If



        End If



        'Added For Export Terms By Rajesh
        If (rdbTerms1.Checked = True Or rdbExport.Checked = True) Then

            If txt41.Text.Trim() <> "" Then

                Dim newRow10 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow10.Cells(1).Range.Text = "         -- " + txt41.Text
                newRow10.Range.Font.Bold = 0
                newRow10.Cells(1).Range.Font.Size = 12
                newRow10.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow10.Range.ListFormat.ListOutdent()
                newRow10.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow10.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
            End If

            If txt42.Text.Trim() <> "" Then
                Dim newRow101 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow101.Cells(1).Range.Text = ".       -- " + txt42.Text
                newRow101.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow101.Range.Font.Bold = 0
                newRow101.Cells(1).Range.Font.Size = 12
                newRow101.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow101.SetLeftIndent(1.2, Word.WdRulerStyle.wdAdjustFirstColumn)
                newRow101.Range.ListFormat.ApplyBulletDefault(Nothing)
            End If


            If txt5.Text.Trim() <> "" Then

                Dim newRow11 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow11.Cells(1).Range.Text = txt5.Text
                newRow11.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                '     newRow11.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow11.Cells(1).Range.Font.Bold = True 'Indian' 
                newRow11.Cells(1).Range.Font.Size = 12
                newRow11.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                newRow11.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow11.Range.ListFormat.ListOutdent()
            End If


            If txt51.Text.Trim() <> "" Then
                Dim newRow12 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow12.Cells(1).Range.Text = "        -- " + txt51.Text
                newRow12.Range.Font.Bold = 0

                newRow12.Cells(1).Range.Font.Size = 12
                newRow12.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow12.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If

            If txt52.Text.Trim() <> "" Then
                Dim newRow13 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow13.Cells(1).Range.Text = ".       --" + txt52.Text
                newRow13.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                'newRow13.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow13.Range.Font.Bold = 0
                newRow13.Cells(1).Range.Font.Size = 12
                newRow13.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow13.Range.ListFormat.ApplyBulletDefault(Nothing)
            End If


            If txt6.Text.Trim() <> "" Then
                Dim newRow14 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow14.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow14.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow14.Cells(1).Range.Text = txt6.Text
                newRow14.Cells(1).Range.Font.Bold = True 'Indian' 
                newRow14.Cells(1).Range.Font.Size = 12
                newRow14.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                newRow14.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow14.Range.ListFormat.ListOutdent()
            End If




            If txt61.Text.Trim() <> "" Then
                Dim newRow15 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow15.Range.ListFormat.ApplyBulletDefault(Nothing)
                newRow15.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow15.Cells(1).Range.Text = "     -- " + txt61.Text
                newRow15.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow15.Cells(1).Range.Font.Size = 12
                newRow15.Range.Font.Bold = 0
                newRow15.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow15.Range.ListFormat.ListOutdent()
                newRow15.Range.ListFormat.ApplyBulletDefault(Nothing)
            End If
            If txt7.Text.Trim() <> "" Then

                Dim newRow161 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow161.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow161.Range.ListFormat.ListOutdent()
                newRow161.Cells(1).Range.Text = txt7.Text
                newRow161.Cells(1).Range.Font.Size = 12
                newRow161.Range.Font.Bold = True
                newRow161.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            End If

            If txt71.Text.Trim() <> "" Then
                Dim newRow171 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow171.Range.ListFormat.ApplyBulletDefault(Nothing)
                newRow171.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow171.Cells(1).Range.Text = "     -- " + txt71.Text
                newRow171.Cells(1).Range.Font.Size = 12
                newRow171.Range.Font.Bold = 0
                newRow171.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow171.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow171.Range.ListFormat.ListOutdent()
                newRow171.Range.ListFormat.ApplyBulletDefault(Nothing)
            End If

        End If

        'Added By Rajesh
        'export Terms
        If rdbExport.Checked = True Then

            Dim newRow88 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow88.Cells(1).Range.Font.Name = "Times New Roman"
            newRow88.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
            newRow88.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
            newRow88.Cells(1).Range.Text = txt9.Text
            newRow88.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow88.Cells(1).Range.Font.Size = 12
            newRow88.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow88.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow88.Range.ListFormat.ListOutdent()



            Dim newRow881 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow881.Cells(1).Range.Font.Name = "Times New Roman"
            newRow881.Range.ListFormat.ApplyBulletDefault(Nothing)
            newRow881.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
            newRow881.Cells(1).Range.Text = "     -- " + txt91.Text
            newRow881.Cells(1).Range.Font.Size = 12
            newRow881.Range.Font.Bold = 0
            newRow881.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow881.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
            newRow881.Range.ListFormat.ListOutdent()
            newRow881.Range.ListFormat.ApplyBulletDefault(Nothing)

        End If

        Dim newRow16 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        If RdbEnglish.Checked Then
            newRow16.Cells(1).Range.Text = "( NB : Guarantee clause is valid for Mfg. Defect/Workmanship Defect only,"
        ElseIf RdbGujarati.Checked Then
            newRow16.Cells(1).Range.Text = "(નોટ: ગેરંટી માત્ર મેન્યુફેક્ચરિંગ ડિફેક્ટ/વર્કમેનશીપ ડિફેક્ટ સુધી જ માન્ય છે,"
        ElseIf RdbHindi.Checked Then
            newRow16.Cells(1).Range.Text = "( NB : Guarantee clause is valid for Mfg. Defect/Workmanship Defect only,"
        ElseIf RdbMarathi.Checked Then
            newRow16.Cells(1).Range.Text = "( NB : Guarantee clause is valid for Mfg. Defect/Workmanship Defect only,"
        ElseIf RdbTamil.Checked Then
            newRow16.Cells(1).Range.Text = "( NB : Guarantee clause is valid for Mfg. Defect/Workmanship Defect only,"
        ElseIf RdbTelugu.Checked Then
            newRow16.Cells(1).Range.Text = "( NB : Guarantee clause is valid for Mfg. Defect/Workmanship Defect only,"
        End If
        newRow16.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow16.Cells(1).Range.Font.Size = 12
        newRow16.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow16.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow16.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
        newRow16.Range.ListFormat.ListOutdent()

        Dim newRow17 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        If RdbEnglish.Checked Then
            newRow17.Cells(1).Range.Text = "      Our liability is limited to repair or replace of the same. )"
        ElseIf RdbGujarati.Checked Then
            newRow17.Cells(1).Range.Text = "      અમારી જવાબદારી માત્ર રીપૈર અથવા રિપ્લેસ સુધી જ માન્ય છે)"
        ElseIf RdbHindi.Checked Then
            newRow17.Cells(1).Range.Text = "      Our liability is limited to repair or replace of the same. )"
        ElseIf RdbMarathi.Checked Then
            newRow17.Cells(1).Range.Text = "      Our liability is limited to repair or replace of the same. )"
        ElseIf RdbTamil.Checked Then
            newRow17.Cells(1).Range.Text = "      Our liability is limited to repair or replace of the same. )"
        ElseIf RdbTelugu.Checked Then
            newRow17.Cells(1).Range.Text = "      Our liability is limited to repair or replace of the same. )"
        End If
        'newRow17.Cells(1).Range.Text = "      Our liability is limited to repair or replace of the same. )"
        newRow17.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow17.Cells(1).Range.Font.Size = 12
        newRow17.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow17.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow17.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
        newRow17.Range.ListFormat.ListOutdent()


        Dim newRow18 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow18.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
        If (rdbTerms1.Checked = True And rdbExport.Checked = True) Then
            If RdbEnglish.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            ElseIf RdbGujarati.Checked Then
                newRow18.Cells(1).Range.Text = "ઓફર મર્યાદા: 30 દિવસ"
            ElseIf RdbHindi.Checked Then
                newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
            ElseIf RdbMarathi.Checked Then
                newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
            ElseIf RdbTamil.Checked Then
                newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
            ElseIf RdbTelugu.Checked Then
                newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
            End If
            'newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
        Else
            If RdbEnglish.Checked Then
                newRow18.Cells(1).Range.Text = "Offer Validity :30 Days"
            ElseIf RdbGujarati.Checked Then
                newRow18.Cells(1).Range.Text = "ઓફર મર્યાદા: 30 દિવસ"
            ElseIf RdbHindi.Checked Then
                newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
            ElseIf RdbMarathi.Checked Then
                newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
            ElseIf RdbTamil.Checked Then
                newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
            ElseIf RdbTelugu.Checked Then
                newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
            End If
            'newRow18.Cells(1).Range.Text = "Offer Validity : 1 Week"
        End If

        newRow18.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow18.Cells(1).Range.Font.Size = 12
        newRow18.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow18.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow18.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
        newRow18.Range.ListFormat.ListOutdent()
        If (rdbTerms2.Checked = True) Then
            Dim strt5 As Object = objDoc1.Tables(1).Range.[End]
            Dim oCollapseEnd5 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran5 As Word.Range = objDoc1.Range(strt5, strt5)
            rng = objDoc1.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable519 As Word.Table = objDoc1.Tables.Add(ran5, 1, 4, missing, missing)

        End If


        Dim strt4 As Object = objDoc1.Tables(1).Range.[End]
        Dim oCollapseEnd4 As Object = Word.WdCollapseDirection.wdCollapseEnd
        Dim ran4 As Word.Range = objDoc1.Range(strt4, strt4)
        rng = objDoc1.Content
        rng.Collapse(oCollapseEnd)


        Dim oTable511 As Word.Table = objDoc1.Tables.Add(ran4, 3, 4, missing, missing)

        oTable511.Range.ParagraphFormat.SpaceAfter = 0




        Dim Rowa12 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        Rowa12.Cells(1).Range.Text = "For, INDIAN ION EXCHANGE"
        Rowa12.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        Rowa12.Range.Font.Size = 12
        Rowa12.Cells(1).Width = 250
        Rowa12.Cells(2).Width = 30
        Rowa12.Cells(4).Width = 50
        Rowa12.Cells(3).Width = 50
        Rowa12.Range.Font.Bold = True

        Dim Rowa23 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        Rowa23.Cells(2).Range.Text = "   & CHEMICALS LTD."
        Rowa23.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        Rowa23.Range.Font.Bold = True
        Rowa23.Cells(1).Width = 30
        Rowa23.Cells(2).Width = 200
        Rowa23.Cells(4).Width = 50
        Rowa23.Cells(3).Width = 200
        Rowa23.Range.Font.Size = 12

        Dim newRow2211 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        If DocumentStatus = 0 Then
            newRow2211.Cells(2).Range.InlineShapes.AddPicture(Class1.global.Signature).Width = 100
            newRow2211.Cells(3).Range.InlineShapes.AddPicture(appPath + "\SIGN.jpg").Width = 100

        Else
            newRow2211.Cells(2).Range.InlineShapes.AddPicture(appPath + "\blanksign.jpg").Width = 100
            newRow2211.Cells(3).Range.InlineShapes.AddPicture(appPath + "\blanksign.jpg").Width = 100
        End If
        newRow2211.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        newRow2211.Cells(1).Width = 30
        newRow2211.Cells(2).Width = 200
        newRow2211.Cells(2).Height = 50
        newRow2211.Cells(4).Width = 50
        newRow2211.Cells(3).Width = 200
        newRow2211.Cells(3).Height = 50

        newRow2211.Range.Font.Bold = True

        Dim newRow2212 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow2212.Height = 20
        newRow2212.Cells(2).Range.Text = Convert.ToString(Class1.global.UserName)
        newRow2212.Cells(3).Range.Text = "DR. BHAVIN VYAS"
        newRow2212.Cells(3).Range.Font.Name = "Times New Roman"
        newRow2212.Cells(1).Width = 30
        newRow2212.Cells(2).Width = 200
        newRow2212.Cells(4).Width = 50
        newRow2212.Cells(3).Width = 200
        newRow2212.Range.Font.Bold = True
        newRow2212.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow2212.Cells(3).Range.Font.Name = "Times New Roman"
        newRow2212.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

        Dim newRow122 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow122.Range.Font.Bold = True
        newRow122.Height = 20
        newRow122.Cells(2).Range.Text = "(" + txtDesignation.Text + ")"
        newRow122.Cells(3).Range.Text = "(TECH.DIRECTOR)"
        newRow122.Cells(2).Range.Font.Name = "Times New Roman"
        newRow122.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow122.Cells(3).Range.Font.Name = "Times New Roman"
        newRow122.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

        newRow122.Cells(1).Width = 30
        newRow122.Cells(2).Width = 200
        newRow122.Cells(4).Width = 50
        newRow122.Cells(3).Width = 200





        objDoc1.SaveAs(appPath + "\Letter2.doc")





        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document
        wordDocument1 = New Word.Document
        wordApplication1 = New Word.Application
        Static Targets1 As Object
        Static paramSourceDocPath1 As Object
        If btnAddClear.Text = "View" Then

            paramSourceDocPath1 = appPath + "\OrderData" + "\" + Convert.ToString(QuationId) + ".doc"
            Targets1 = appPath + "\OrderData" + "\" + Convert.ToString(QuationId) + ".pdf"

        Else

            paramSourceDocPath1 = appPath + "\Letter2.doc"
            Targets1 = appPath + "\Letter2.pdf"

        End If

        Dim formating1 As Object
        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF

        objDoc1.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        objDoc1.Close()
        objDoc1 = Nothing
        objApp1.NormalTemplate.Saved = True
        objApp1.Quit()
        objApp1 = Nothing
        If Not IsNothing(objApp1) Then
            objApp1.Quit()
        End If
        If Not IsNothing(objDoc1) Then
            objDoc1.Close()
        End If
    End Sub
    Private Sub btnPdfPriceWOHF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdfPriceWOHF.Click
        Try


            DocumentStatus = 1
            OleMessageFilter.Register()

            AdditionalPriceSheet()
            OleMessageFilter.Revoke()

            'MessageBox.Show("Price Sheet Ready !")
            'System.Diagnostics.Process.Start(appPath + "\Letter2.pdf")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Class1.killProcessOnUser()
        End Try
    End Sub

    Private Sub btnPDFPRICEOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDFPRICEOnly.Click
        Try


            DocumentStatus = 0
            OleMessageFilter.Register()

            AdditionalPriceSheet()
            OleMessageFilter.Revoke()

            'MessageBox.Show("Price Sheet Ready !")
            'System.Diagnostics.Process.Start(appPath + "\Letter2.pdf")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Class1.killProcessOnUser()
        End Try
    End Sub

    Public Sub AdditionalPriceSheet()

        PriceSheet()
        Dim rng As Word.Range
        Dim _pdfforge As New PDF.PDF
        Dim _pdftext As New PDF.PDFText
        Dim GetImage As String
        'Specialpricesheet()
        Dim flag As Integer
        Dim cnt As Integer
        If txtspdiscount.Text.Trim() = "" And txtVat.Text.Trim() = "" And txtTransporation.Text.Trim() = "" And txtInsurance.Text.Trim() = "" And txtPakingforwarding.Text.Trim() = "" And txtErection.Text.Trim() = "" Then
            flag = 1
        Else
            ' Specialpricesheet()
        End If

        If flag = 1 Then
            cnt = 0
        Else
            cnt = 0
        End If
        Dim files(cnt) As String
        If (btnAddClear.Text = "View") Then
            files(0) = appPath + "\OrderData\" + Convert.ToString(QuationId) + ".pdf"
        Else
            files(0) = appPath + "\Letter2.pdf"
        End If
        '   files(0) = appPath + "\Letter2.pdf"

        If (Not System.IO.Directory.Exists(appPath + "\Temporaryview")) Then
            System.IO.Directory.CreateDirectory(appPath + "\Temporaryview")
        End If
        If (btnAddClear.Text = "View") Then
            _pdfforge.MergePDFFiles(files, appPath + "\Temporaryview\" + Convert.ToString(QuationId) + ".pdf", False)

        Else
            _pdfforge.MergePDFFiles(files, appPath + "\Price_Sheet.pdf", False)
        End If
        ''_pdfforge.MergePDFFiles(files, appPath + "\AdditionPriceSheet.pdf", False)
        Class1.killProcessOnUser()
        MessageBox.Show("Price Sheet Ready !")
        If (btnAddClear.Text = "View") Then
            System.Diagnostics.Process.Start(appPath + "\Temporaryview\" + Convert.ToString(QuationId) + ".pdf")
        Else
            System.Diagnostics.Process.Start(appPath + "\Price_Sheet.pdf")
        End If

    End Sub

    Private Sub btnPDFFirstWOHF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDFFirstWOHF.Click
        Try


            DocumentStatus = 1

            OleMessageFilter.Register()

            FirstPage()
            OleMessageFilter.Revoke()
            MessageBox.Show("First Letter Ready !")
            System.Diagnostics.Process.Start(appPath + "\Letter1.pdf")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Class1.killProcessOnUser()
        End Try
    End Sub

    Private Sub btnPDFFirstPageOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDFFirstPageOnly.Click
        Try


            DocumentStatus = 0
            OleMessageFilter.Register()

            FirstPage()
            OleMessageFilter.Revoke()

            MessageBox.Show("First Letter Ready !")
            System.Diagnostics.Process.Start(appPath + "\Letter1.pdf")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Class1.killProcessOnUser()
        End Try
    End Sub

    Private Sub TxtTax_Leave_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTax.Leave

        If rblOld.Checked = True Then

            If RblSingle.Checked = True Then
                If TxtTax.Text <> "" And txtCapacity1.Text.Trim <> "" Then
                    GvSingle_Bind()
                End If
            ElseIf RblOther.Checked = True Then
                If TxtTax.Text <> "" And txtCapacity1.Text.Trim <> "" Then
                    GvSingle_Bind()
                End If
            ElseIf RblMultiple.Checked = True Then
                If TxtTax.Text <> "" And txtCapacity1.Text.Trim <> "" And txtCapacity2.Text.Trim <> "" Then
                    GvMultiple_Bind()
                End If
            End If
        Else
            GvSingle__SalesExecutive_Bind(Fk_SalesExecutiveQtnID)
        End If
    End Sub
    Private Sub txtspdiscount_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtspdiscount.Validating
        Try

            If IsNumeric(txtspdiscount.Text) Then
            Else
                txtspdiscount.Text = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtVat_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtVat.Validating
        Try

            If IsNumeric(txtVat.Text) Then
            Else
                txtVat.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtTransporation_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTransporation.Validating
        Try

            If IsNumeric(txtTransporation.Text) Then
            Else
                txtTransporation.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtInsurance_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtInsurance.Validating
        Try

            If IsNumeric(txtInsurance.Text) Then
            Else
                txtInsurance.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPakingforwarding_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPakingforwarding.Validating
        Try

            If IsNumeric(txtPakingforwarding.Text) Then
            Else
                txtPakingforwarding.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtErection_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtErection.Validating
        Try

            If IsNumeric(txtErection.Text) Then
            Else
                txtErection.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtVat_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVat.Leave
        TotalLeave()
    End Sub


    Private Sub txtTransporation_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTransporation.Leave
        TotalLeave()
    End Sub

    Private Sub txtInsurance_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsurance.Leave
        Total1()
    End Sub

    Private Sub txtPakingforwarding_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPakingforwarding.Leave
        TotalLeave()
    End Sub

    Private Sub txtErection_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtErection.Leave
        TotalLeave()
    End Sub

    Private Sub txtspdiscount_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtspdiscount.Leave
        TotalLeave()
    End Sub

    Private Sub gvEnquiry_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        GetClientDetails_Bind()
    End Sub



    Private Sub rdbExport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbExport.CheckedChanged
        txt5.Visible = True
        txt6.Visible = True
        txt61.Visible = True
        txt71.Visible = True
        txt7.Visible = True
        txt5.Visible = True
        txt51.Visible = True
        txt52.Visible = True
        txt42.Visible = True
        txt9.Visible = True
        txt91.Visible = True
        txtPayTerms.Visible = True
        txt1.Text = "PRICES ARE EX. FACTORY NARODA, AHMEDABAD.".ToUpper()
        txt2.Text = "Wooden packing charges extra at actual.".ToUpper()
        txt3.Text = "To & Fro, + Lodging & Boarding + Local Conveyance charges to buyer's account.".ToUpper()
        txt4.Text = "Necessary Tools, Tackles, Labour & Chemical should be provided by the client.".ToUpper()
        txtPayTerms.Text = "PAYMENT TERMS :".ToUpper()
        txt41.Text = "30% advance against accepting our Performa invoice by Telegraphic transfer."
        txt42.Text = "70% confirmed irrevocable letter of credit at sight, our bank is ICICI"
        txt5.Text = "DELIVERY :".ToUpper()
        txt51.Text = "With in 8-9 week after receipt of your order with advance for Asian continent."
        txt52.Text = "With in 9-10 week after receipt of your order with advance for African countries."
        txt6.Text = "Conflict of contract :".ToUpper()
        txt61.Text = "The Indian arbitration and conciliation act.1996."
        txt7.Text = "INSPECTION :".ToUpper()
        txt71.Text = "Third party inspection charges shall be extra at actual to buyers account."
        txt9.Text = "GUARANTEE / WARRANTY :"
        txt91.Text = "WE CAN OFFER YOU GUARANTEE FOR 12 MONTHS FROM THE DATE OF SUPPLY OF THE PLANT"
        txt8.Text = "Offer Validity :30 Days"
    End Sub

    Private Sub GvCategorySearch_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles GvCategorySearch.PreviewKeyDown

    End Sub
    Public Sub bindDataAddress()
        Dim Claient = linq_obj.SP_Get_AddressListById(Address_ID).ToList()
        For Each item As SP_Get_AddressListByIdResult In Claient
            Address_ID = item.Pk_AddressID
            txtName.Text = item.Name
            txtEnqNo.Text = item.EnqNo
            txtAddress.Text = item.Address + "," + item.Area + "," + item.District + "," + item.City + "," + item.State + "-" + item.Pincode
        Next

    End Sub




    Private Sub QuotationMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each control As Control In Me.Controls
            ' The textbox control.
            Dim parentControl As New Control


            If (control.GetType() Is GetType(GroupBox)) Then
                Dim grpBox As GroupBox = TryCast(control, GroupBox)
                parentControl = grpBox
            ElseIf (control.GetType() Is GetType(TabControl)) Then
                Dim TC As TabControl = TryCast(control, TabControl)
                parentControl = TC
            ElseIf (control.GetType() Is GetType(Panel)) Then
                Dim Panel As Panel = TryCast(control, Panel)
                parentControl = Panel

            Else
                Try
                    parentControl = control
                Catch ex As Exception

                End Try

            End If

            For Each subcontrol As Control In parentControl.Controls
                If (subcontrol.GetType() Is GetType(TabControl)) Then
                    For Each subcontrolTwo As Control In subcontrol.Controls
                        If (subcontrolTwo.GetType() Is GetType(TabPage)) Then
                            For Each subcontrolthree As Control In subcontrolTwo.Controls
                                If (subcontrolthree.GetType() Is GetType(Panel)) Then
                                    For Each subcontrolFour As Control In subcontrolthree.Controls
                                        If (subcontrolFour.GetType() Is GetType(TextBox)) Then
                                            Dim textBox As TextBox = TryCast(subcontrolFour, TextBox)

                                            ' If not null, set the handler.
                                            If textBox IsNot Nothing Then
                                                AddHandler textBox.Enter, AddressOf TextBox_Enter
                                                AddHandler textBox.Leave, AddressOf TextBox_Leave
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
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

    Private Sub GvCategorySearch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvCategorySearch.CellContentClick

    End Sub

    Private Sub GvCategorySearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GvCategorySearch.KeyUp
        Try
            If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
                QuationId = Convert.ToInt32(Me.GvCategorySearch.Rows(GvCategorySearch.CurrentRow.Index).Cells(0).Value)
                bindQuatData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub rblNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblNew.CheckedChanged
        GvCategorySearch_For_SalesExecutive_Bind()
    End Sub

    Private Sub rblOld_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblOld.CheckedChanged
        GvQuotationSearch_Bind(1)
    End Sub
End Class

