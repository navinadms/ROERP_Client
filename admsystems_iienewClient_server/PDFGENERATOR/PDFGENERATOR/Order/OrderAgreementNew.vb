
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports pdfforge


Public Class OrderAgreementNew
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public SoftwareID As Integer = 0
    Public Pk_OrderID As Integer
    Public focussedTextBox As TextBox
    Dim msPic1 As New MemoryStream
    Dim msPic2 As New MemoryStream
    Dim msPic3 As New MemoryStream
    Dim msPic4 As New MemoryStream

    Dim Fk_Address_ID As Integer
    Dim Order_Category_Master_ID As String
    Dim Pk_Order_Agreement_ID As Integer
    Public Sys1_Total, Sys2_Total, Sys3_Total, Sys4_Total, GrossAmount, Discount, FinalAmount1 As Decimal
    Dim OrderTempPath, QPath As String
    Dim increment As Integer

    Dim dt1 As New DataTable
    Dim dt2 As New DataTable
    Dim dt3 As New DataTable
    Dim dt4 As New DataTable
    Dim dttech As New DataTable
    Dim bits1() As Byte = Nothing
    Dim bits2() As Byte = Nothing
    Dim bits3() As Byte = Nothing
    Dim bits4() As Byte = Nothing
    Dim bitsExecutive() As Byte = Nothing
    Dim bSystem1() As Byte = Nothing
    Dim profilephoto1, profilephoto2, profilephoto3, FullName, Designation As String

    Dim OrderAgreementDStmp As New DataSet
    Dim OrderAgreementDS As New DataSet
    Dim OrderAgreementDSNew As New DataSet

    Dim cmd As New SqlCommand
    Dim da As New SqlDataAdapter()

    Dim SrNocount As Integer
    Dim DiscountAmt As Decimal
    Dim FinalRate As Decimal
    Dim GstAmount As Decimal
    Dim FinalAmount As Decimal
    Dim TotalRate, TotalDisc, TotalFinalRate, TotalGST As Decimal

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        State_Bind()
        QPath = Class1.global.QPath
        If (Not System.IO.Directory.Exists(QPath + "\OrderTempFile")) Then
            System.IO.Directory.CreateDirectory(QPath + "\OrderTempFile")
        End If
        OrderTempPath = QPath + "\OrderTempFile"
        GvOrderAgreementList_Bind()
    End Sub
    Public Sub State_Bind()


        Dim datatable As New DataTable
        datatable.Columns.Add("Result")
        Dim data = linq_obj.SP_Get_AddressListAutoComplete("State").ToList()
        For Each item As SP_Get_AddressListAutoCompleteResult In data
            datatable.Rows.Add(item.Result)
        Next
        Dim newRow As DataRow = datatable.NewRow()
        newRow(0) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        txtState.DataSource = datatable
        txtState.DisplayMember = "Result"
        txtState.ValueMember = "Result"
        txtState.AutoCompleteMode = AutoCompleteMode.Append
        txtState.DropDownStyle = ComboBoxStyle.DropDownList
        txtState.AutoCompleteSource = AutoCompleteSource.ListItems

        'Reg Office


        Dim datatable1 As New DataTable
        datatable1.Columns.Add("Result")
        Dim data1 = linq_obj.SP_Get_AddressListAutoComplete("State").ToList()
        For Each item As SP_Get_AddressListAutoCompleteResult In data1
            datatable1.Rows.Add(item.Result)
        Next
        Dim newRow1 As DataRow = datatable1.NewRow()

        newRow1(0) = "Select"
        datatable1.Rows.InsertAt(newRow1, 0)
        txtRState.DataSource = datatable1
        txtRState.DisplayMember = "Result"
        txtRState.ValueMember = "Result"
        txtRState.AutoCompleteMode = AutoCompleteMode.Append
        txtRState.DropDownStyle = ComboBoxStyle.DropDownList
        txtRState.AutoCompleteSource = AutoCompleteSource.ListItems


    End Sub
    Public Sub GvOrderAgreementList_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("PK_OrderAgreement_ID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")


        If txtSearchEnqNo.Text.Trim() = "" And txtSearchName.Text.Trim() = "" Then
            Dim DataOrder = linq_obj.SP_Get_Order_Agreement_Master_List().ToList()
            For Each item As SP_Get_Order_Agreement_Master_ListResult In DataOrder
                dt.Rows.Add(item.PK_Order_Agreement_ID, item.EnqNo, item.Name)
            Next
        ElseIf txtSearchEnqNo.Text.Trim() <> "" Then

            Dim DataOrder = linq_obj.SP_Get_Order_Agreement_Master_List().ToList().Where(Function(p) p.EnqNo.Trim().ToLower() = txtSearchEnqNo.Text.Trim().ToLower()).ToList()

            For Each item As SP_Get_Order_Agreement_Master_ListResult In DataOrder
                dt.Rows.Add(item.PK_Order_Agreement_ID, item.EnqNo, item.Name)
            Next
        ElseIf txtSearchName.Text.Trim() <> "" Then

            Dim DataOrder = linq_obj.SP_Get_Order_Agreement_Master_List().ToList().Where(Function(p) p.Name.Trim().ToLower().StartsWith(txtSearchName.Text.Trim().ToLower())).ToList()

            For Each item As SP_Get_Order_Agreement_Master_ListResult In DataOrder
                dt.Rows.Add(item.PK_Order_Agreement_ID, item.EnqNo, item.Name)
            Next
        End If





        GvOrderAgreementList.DataSource = dt
        GvOrderAgreementList.Columns(0).Visible = False
    End Sub
    Public Sub DiplayData()


        Dim data = linq_obj.SP_Get_OrderAgreement_By_EnqNo(txtEnqNo.Text.Trim()).ToList()
        If (data.Count > 0) Then
            For Each item As SP_Get_OrderAgreement_By_EnqNoResult In data
                'Personal Information
                Fk_Address_ID = item.Pk_AddressID
                txtCompanyName.Text = item.Name
                txtAddress.Text = item.Address
                txtCity.Text = item.City
                txtDistrict.Text = item.District
                item.State = item.State
                txtPincode.Text = item.Pincode
                txtTaluko.Text = item.Taluka
                txtRAddress.Text = item.DeliveryAddress
                txtRCity.Text = item.DeliveryCity
                txtRState.Text = item.DeliveryState
                txtRTaluko.Text = item.DeliveryTaluka
                txtRPincode.Text = item.DeliveryPincode

                If item.Photo1 <> Nothing Then
                    If item.Photo1.Length > 2 Then
                        Dim image1Data As Byte() = IIf(item.Photo1 = Nothing, Nothing, item.Photo1.ToArray())
                        If (image1Data.Length > 0) Then
                            If Not image1Data Is Nothing Then
                                msPic1 = New MemoryStream(image1Data, 0, image1Data.Length)
                                msPic1.Write(image1Data, 0, image1Data.Length)
                                PicPhoto1.Image = Image.FromStream(msPic1, True)
                                PicPhoto1.SizeMode = PictureBoxSizeMode.StretchImage
                            End If
                        End If
                    End If
                End If

                If item.Photo2 <> Nothing Then
                    If item.Photo2.Length > 2 Then
                        Dim image2Data As Byte() = IIf(item.Photo2 = Nothing, Nothing, item.Photo2.ToArray())
                        If (image2Data.Length > 0) Then
                            If Not image2Data Is Nothing Then
                                msPic2 = New MemoryStream(image2Data, 0, image2Data.Length)
                                msPic2.Write(image2Data, 0, image2Data.Length)
                                PicPhoto2.Image = Image.FromStream(msPic2, True)
                                PicPhoto2.SizeMode = PictureBoxSizeMode.StretchImage
                            End If
                        End If
                    End If
                End If
                txtCName1.Text = item.Ph1_Value1
                txtCMobile1.Text = item.Ph1_Value2
                txtCBusiness1.Text = item.Ph1_Value3
                txtCEmail1.Text = item.Ph1_Value6
                txtCName2.Text = item.Ph2_Value1
                txtCMobile2.Text = item.Ph2_Value2
                txtCBusiness2.Text = item.Ph2_Value3
                txtCEmail2.Text = item.Ph2_Value6
                'Plant Detail 

                If item.Photo3 <> Nothing Then
                    If item.Photo3.Length > 2 Then
                        Dim image3Data As Byte() = IIf(item.Photo3 = Nothing, Nothing, item.Photo3.ToArray())
                        If (image3Data.Length > 0) Then
                            If Not image3Data Is Nothing Then
                                msPic3 = New MemoryStream(image3Data, 0, image3Data.Length)
                                msPic3.Write(image3Data, 0, image3Data.Length)
                                PicPPic1.Image = Image.FromStream(msPic3, True)
                                PicPPic1.SizeMode = PictureBoxSizeMode.StretchImage
                            End If
                        End If

                    End If
                End If
                If item.Photo4 <> Nothing Then
                    If item.Photo4.Length > 2 Then
                        Dim image4Data As Byte() = IIf(item.Photo4 = Nothing, Nothing, item.Photo4.ToArray())
                        If (image4Data.Length > 0) Then
                            If Not image4Data Is Nothing Then
                                msPic4 = New MemoryStream(image4Data, 0, image4Data.Length)
                                msPic4.Write(image4Data, 0, image4Data.Length)
                                PicPPic2.Image = Image.FromStream(msPic4, True)
                                PicPPic2.SizeMode = PictureBoxSizeMode.StretchImage
                            End If
                        End If
                    End If
                End If

                txtPName1.Text = item.Ph3_Value1
                txtPMobile1.Text = item.Ph3_Value2
                txtPEmail1.Text = item.Ph3_Value6
                txtPBusiness1.Text = item.Ph3_Value3

                txtPName2.Text = item.Ph4_Value1
                txtPMobile2.Text = item.Ph4_Value2
                txtPEmail2.Text = item.Ph4_Value6
                txtPBusiness2.Text = item.Ph4_Value3






                Payment_Terms()

            Next
        Else
            MessageBox.Show("EnqNo not found....")
        End If

    End Sub
    Public Sub Payment_Terms()

        'Payment
        txtPT_Terms.Text = "40% Advance along with Techno-Commercial clear order & 60% Balance against Performa Invoice before despatch"
        txtPT_Gov.Text = "GST as applicable from time to time If any variation in the norms from Government to be borne by the Buyer"
        txtPT_Deliver.Text = "Within 3-4 weeks after receipt of your order with advance"
        txtPT_Transp1.Text = "Transportation charges extra at actual"
        txtPT_Transp2.Text = "After loading the goods, the seller’s responsibility would cease to exist and stands transferred to the buyer and shall ensure safe custody"
        txtPT_Transp3.Text = "The buyer will arrange to insure the goods for transit period and will ensure safe and secures unloading of goods"
        txtPT_Transp4.Text = "All costs and charges on account of lodging, boarding, and conveyance other contingencies, medical and safety of staff, necessary infrastructure and facilities for erection and commissioning of the plant will be at actual and borne by the buyer."

        txtPT_Errection1.Text = "Erection and Commissioning will be extra at actual"
        txtPT_Errection2.Text = "To & Fro, + Lodging & Boding,+ Local Conveyance Charges To At The Time Of Plant Erection & Commissioning by client."


        'Terms 

        Dim Gaurantee As String
        Gaurantee = "12 Months From The Date Of Commissioning Of Plant Or 18 Months From The Date Of Supply Of The Plant Whichever Is Earlier."
        Gaurantee = Gaurantee + "(Nb: Guarantee Clause Is Valid For Mfg. Defect/ Workmanship defect Only Liability Is Limited To Repair Or Replace Of The Same.)"
        txtPT_Gaurantee1.Text = Gaurantee
        txtPT_Gaurantee2.Text = "In the event of occurrence of any natural calamities such as flood, earthquake, strikes, embargo, enemies hostilities, public disorder, labour disputes, lockouts, Demonstrations etc.  The terms and conditions would stand altered suitably without any prior notice and the buyer shall not create any disputes on account of such contingencies"

        Dim Terms As String

        txtPT_Terms1.Text = "Advance payment is NOT subject to Refund under any circumstances and this agreement is NOT subject to any amendments whatsoever"
        txtPT_Terms2.Text = "Post receipt of token, Order will not be cancelled under any circumstances"
        txtPT_Terms3.Text = "Agreement Final Price Validity : 3 Months From Date Of order   Agreement"
        txtPT_Terms4.Text = "Raw Material Price (Jar, Cool Jug, Dispenser, Pouch Roll, Cap)  Will Be Price Extra At Actual At The Time Of Delivery"
        txtPT_Terms5.Text = "All disputes are subject to Ahmedabad jurisdiction"
    End Sub
    Public Sub AutoComplate_text()
        txtSys1ItemName.AutoCompleteCustomSource.Clear()
        Dim Sytem1Auto = linq_obj.SP_Get_Order_Category_By_Criteria("System-1", "", txtCapacity.Text.Trim()).ToList()
        For Each item As SP_Get_Order_Category_By_CriteriaResult In Sytem1Auto
            txtSys1ItemName.AutoCompleteCustomSource.Add(item.Model)
        Next

        'Technical Data
        txtSys1ItemTechName.AutoCompleteCustomSource.Clear()
        Dim Sytem1Tech = linq_obj.SP_Get_Order_Category_Detail_By_Capacity("System-1").ToList().Where(Function(p) p.Model = "Mineral Water Plant - Xcel" And p.Capacity = txtCapacity.Text.Trim()).ToList()
        For Each item As SP_Get_Order_Category_Detail_By_CapacityResult In Sytem1Tech
            txtSys1ItemTechName.AutoCompleteCustomSource.Add(item.Item_Name)
        Next

        txtSys2ItemName.AutoCompleteCustomSource.Clear()
        Dim Sytem2Auto = linq_obj.SP_Get_Order_Category_By_Criteria("System-2", "", "").ToList() 'No Capacity
        For Each item As SP_Get_Order_Category_By_CriteriaResult In Sytem2Auto
            txtSys2ItemName.AutoCompleteCustomSource.Add(item.Model)
        Next

        txtSys3ItemName.AutoCompleteCustomSource.Clear()

        Dim Sytem3Auto = linq_obj.SP_Get_Order_Category_By_Criteria("System-3", "", "").ToList() 'No Capacity
        For Each item As SP_Get_Order_Category_By_CriteriaResult In Sytem3Auto
            txtSys3ItemName.AutoCompleteCustomSource.Add(item.Model)

        Next

        txtSys4ItemName.AutoCompleteCustomSource.Clear()
        Dim Sytem4Auto = linq_obj.SP_Get_Order_Category_By_Criteria("System-4", "", txtCapacity.Text.Trim()).ToList()
        For Each item As SP_Get_Order_Category_By_CriteriaResult In Sytem4Auto
            txtSys4ItemName.AutoCompleteCustomSource.Add(item.Model)
        Next

    End Sub
    Public Sub Order_Category_Data_Bind()

        'System 1
        AutoComplate_text()
        txtPCapacity.Text = txtCapacity.Text
        dt1 = New DataTable
        dt4 = New DataTable
        If (ddlQtnType.Text = "ISI") Then


            Dim System1 = linq_obj.SP_Get_Order_Category_By_Criteria("System-1", "", txtCapacity.Text.Trim()).Take(5)
            dt1.Columns.Add("PK_ID")
            dt1.Columns.Add("PriceList", GetType(Boolean))
            dt1.Columns.Add("SrNo")
            dt1.Columns.Add("ItemName")
            dt1.Columns.Add("Price")
            dt1.Columns.Add("Disc")
            dt1.Columns.Add("FinalRate")
            dt1.Columns.Add("GST")
            dt1.Columns.Add("GSTAmt")
            dt1.Columns.Add("FinalAmount")
            SrNocount = 1
            For Each item As SP_Get_Order_Category_By_CriteriaResult In System1
                DiscountAmt = 0
                FinalRate = 0
                GstAmount = 0
                DiscountAmt = Discount_Calculation(item.Price)
                FinalRate = item.Price - DiscountAmt
                GstAmount = GST_Calculation(FinalRate, item.GST)
                FinalAmount = FinalRate + GstAmount
                dt1.Rows.Add(item.Pk_OrderCategory_ID, 1, SrNocount, item.Model, item.Price, DiscountAmt, FinalRate, item.GST, Convert.ToString(GstAmount), FinalAmount)
                SrNocount = SrNocount + 1
            Next
            GvSystem1.DataSource = dt1
            GvSystem1.Columns(0).Visible = False

            'System 1 Technical data
            dttech = New DataTable()
            dttech.Columns.Add("PK_ID")
            dttech.Columns.Add("Text", GetType(Boolean))
            dttech.Columns.Add("Image", GetType(Boolean))
            dttech.Columns.Add("SrNo")
            dttech.Columns.Add("ItemName")
            dttech.Columns.Add("Description")
            Dim datatech = linq_obj.SP_Get_Order_Category_Detail_By_Capacity("System-1").ToList().Where(Function(p) p.Capacity = txtCapacity.Text And p.Model = "Mineral Water Plant - Xcel").ToList().Take(13)
            SrNocount = 1
            For Each item As SP_Get_Order_Category_Detail_By_CapacityResult In datatech
                dttech.Rows.Add(item.Pk_OrderCategory_Detaild_ID, 1, 1, item.SrNo, item.Item_Name, item.Remarks)
                SrNocount = SrNocount + 1
            Next
            GvSystem1Tech.DataSource = dttech
            GvSystem1Tech.Columns(0).Visible = False

            'System 2
            SrNocount = 1
            Dim System2 = linq_obj.SP_Get_Order_Category_By_Criteria("System-2", "", "").ToList().Take(2)
            dt2 = New DataTable
            dt2.Columns.Add("PK_ID")
            dt2.Columns.Add("PriceList", GetType(Boolean))
            dt2.Columns.Add("Technical", GetType(Boolean))
            dt2.Columns.Add("SrNo")
            dt2.Columns.Add("ItemName")
            dt2.Columns.Add("Price")
            dt2.Columns.Add("Disc")
            dt2.Columns.Add("FinalRate")
            dt2.Columns.Add("GST")
            dt2.Columns.Add("GSTAmt")
            dt2.Columns.Add("FinalAmount")


            For Each item As SP_Get_Order_Category_By_CriteriaResult In System2
                DiscountAmt = 0
                FinalRate = 0
                GstAmount = 0
                DiscountAmt = Discount_Calculation(item.Price)
                FinalRate = item.Price - DiscountAmt
                GstAmount = GST_Calculation(FinalRate, item.GST)
                FinalAmount = FinalRate + GstAmount

                dt2.Rows.Add(item.Pk_OrderCategory_ID, 1, 1, SrNocount, item.Model, item.Price, DiscountAmt, FinalRate, item.GST, Convert.ToString(GstAmount), FinalAmount)
                SrNocount = SrNocount + 1
            Next
            GvSystem2.DataSource = dt2
            GvSystem2.Columns(0).Visible = False

            'System 3
            SrNocount = 1
            Dim System3 = linq_obj.SP_Get_Order_Category_By_Criteria("System-3", "", "").ToList().Take(6)
            dt3 = New DataTable
            dt3.Columns.Add("PK_ID")
            dt3.Columns.Add("PriceList", GetType(Boolean))
            dt3.Columns.Add("Technical", GetType(Boolean))
            dt3.Columns.Add("SrNo")
            dt3.Columns.Add("ItemName")
            dt3.Columns.Add("Capacity")
            dt3.Columns.Add("Price")
            dt3.Columns.Add("Disc")
            dt3.Columns.Add("FinalRate")
            dt3.Columns.Add("GST")
            dt3.Columns.Add("GSTAmt")
            dt3.Columns.Add("FinalAmount")
            dt3.Columns.Add("Description")
            For Each item As SP_Get_Order_Category_By_CriteriaResult In System3
                DiscountAmt = 0
                FinalRate = 0
                GstAmount = 0
                DiscountAmt = Discount_Calculation(item.Price)
                FinalRate = item.Price - DiscountAmt
                GstAmount = GST_Calculation(FinalRate, item.GST)
                FinalAmount = FinalRate + GstAmount
                dt3.Rows.Add(item.Pk_OrderCategory_ID, 1, 1, SrNocount, item.Model, item.Capacity, item.Price, DiscountAmt, FinalRate, item.GST, Convert.ToString(GstAmount), FinalAmount, item.Remarks)
                SrNocount = SrNocount + 1
            Next
            GvSystem3.DataSource = dt3
            GvSystem3.Columns(0).Visible = False

            'System 4
            SrNocount = 1
            Dim System4 = linq_obj.SP_Get_Order_Category_By_Criteria("System-4", "", txtCapacity.Text.Trim()).ToList().Take(6)

            dt4.Columns.Add("PK_ID")
            dt4.Columns.Add("PriceList", GetType(Boolean))
            dt4.Columns.Add("Technical", GetType(Boolean))
            dt4.Columns.Add("SrNo")
            dt4.Columns.Add("ItemName")
            dt4.Columns.Add("Price")
            dt4.Columns.Add("Disc")
            dt4.Columns.Add("FinalRate")
            dt4.Columns.Add("GST")
            dt4.Columns.Add("GSTAmt")
            dt4.Columns.Add("FinalAmount")
            dt4.Columns.Add("Description")
            For Each item As SP_Get_Order_Category_By_CriteriaResult In System4
                DiscountAmt = 0
                FinalRate = 0
                GstAmount = 0


                DiscountAmt = Discount_Calculation(item.Price)
                FinalRate = item.Price - DiscountAmt

                GstAmount = GST_Calculation(FinalRate, item.GST)
                FinalAmount = FinalRate + GstAmount
                dt4.Rows.Add(item.Pk_OrderCategory_ID, 1, 1, SrNocount, item.Model, item.Price, DiscountAmt, FinalRate, item.GST, Convert.ToString(GstAmount), FinalAmount, item.Remarks)
                SrNocount = SrNocount + 1

            Next
            GvSystem4.DataSource = dt4
            GvSystem4.Columns(0).Visible = False

            'Price_Total_Calculation()

            'Client Scope Data Bind 

            'Client General
            SrNocount = 1
            Dim ClientGeneral = linq_obj.SP_Get_Order_Category_Detail_By_Capacity("CLIENT SCOPE - GENERAL").ToList()
            Dim dtClientGene As New DataTable
            dtClientGene.Columns.Add("PK_ID")
            dtClientGene.Columns.Add("Text", GetType(Boolean))
            dtClientGene.Columns.Add("Image", GetType(Boolean))
            dtClientGene.Columns.Add("SrNo")
            dtClientGene.Columns.Add("ItemName")
            dtClientGene.Columns.Add("Description")

            For Each item As SP_Get_Order_Category_Detail_By_CapacityResult In ClientGeneral
                dtClientGene.Rows.Add(item.Pk_OrderCategory_Detaild_ID, 1, 1, SrNocount, item.Item_Name, item.Remarks)
                SrNocount = SrNocount + 1
            Next
            GvClientGeneralNew.DataSource = dtClientGene
            GvClientGeneralNew.Columns(0).Visible = False


            'Client Raw Material

            GvClientRawMaterial_Bind()

            'Client Mam Power
            SrNocount = 1
            Dim ClientManPwr = linq_obj.SP_Get_Order_Category_Detail_By_Capacity("CLIENT SCOPE - MAN POWER").ToList()
            Dim dtClientManPwr As New DataTable
            dtClientManPwr.Columns.Add("PK_ID")
            dtClientManPwr.Columns.Add("Text", GetType(Boolean))
            dtClientManPwr.Columns.Add("Image", GetType(Boolean))
            dtClientManPwr.Columns.Add("SrNo")
            dtClientManPwr.Columns.Add("ItemName")
            dtClientManPwr.Columns.Add("Description")

            For Each item As SP_Get_Order_Category_Detail_By_CapacityResult In ClientManPwr
                dtClientManPwr.Rows.Add(item.Pk_OrderCategory_Detaild_ID, 1, 1, SrNocount, item.Item_Name, item.Remarks)
                SrNocount = SrNocount + 1
            Next
            GvClientManPower.DataSource = dtClientManPwr
            GvClientManPower.Columns(0).Visible = False


            'Complimentory
            SrNocount = 1
            Dim Complimentory = linq_obj.SP_Get_Order_Category_Detail_By_Capacity("COMPLILMENTORY").ToList()
            Dim dtComplimentory As New DataTable
            dtComplimentory.Columns.Add("PK_ID")
            dtComplimentory.Columns.Add("Text", GetType(Boolean))
            dtComplimentory.Columns.Add("Image", GetType(Boolean))
            dtComplimentory.Columns.Add("SrNo")
            dtComplimentory.Columns.Add("ItemName")
            dtComplimentory.Columns.Add("Description")

            For Each item As SP_Get_Order_Category_Detail_By_CapacityResult In Complimentory
                dtComplimentory.Rows.Add(item.Pk_OrderCategory_Detaild_ID, 1, 1, SrNocount, item.Item_Name, item.Remarks)
                SrNocount = SrNocount + 1
            Next
            GvComplimentory.DataSource = dtComplimentory
            GvComplimentory.Columns(0).Visible = False


        Else

            MessageBox.Show("No Record Found")
        End If

    End Sub
    Public Function Discount_Calculation(ByVal Price As Decimal) As Decimal
        Dim Discount As Decimal
        Dim DiscountAmount As Decimal
        If (txtMainDiscount.Text.Trim() = "") Then
            txtMainDiscount.Text = "0"

        End If
        Discount = txtMainDiscount.Text
        DiscountAmount = Math.Round(Price * Discount / 100.0, 2)
        Return DiscountAmount.ToString("N2")

    End Function
    Public Function Edit_Discount_Calculation(ByVal Price As Decimal) As Decimal
        Dim Discount As Decimal
        Dim DiscountAmount As Decimal
        If (txtEditDiscount.Text.Trim() = "") Then
            txtEditDiscount.Text = "0"

        End If
        Discount = txtEditDiscount.Text
        DiscountAmount = Math.Round(Price * Discount / 100.0, 2)
        Return DiscountAmount.ToString("N2")

    End Function
    Public Function GST_Calculation(ByVal Price As Decimal, ByVal GstAmt As Decimal) As Decimal

        Dim GstAmount As Decimal
        GstAmount = Math.Round(Price * GstAmt / 100.0, 2)
        Return GstAmount

    End Function

    Public Sub GvClientRawMaterial_Bind()
        Dim SrNocount As Integer
        Dim System3ID As String
        System3ID = ""

        For i As Integer = 0 To GvSystem3.RowCount - 1
            'Text 
            Dim IsTicked As Boolean = CBool(GvSystem3.Rows(i).Cells(1).Value)
            If IsTicked Then
                System3ID = System3ID + GvSystem3.Rows(i).Cells(0).Value + ","
            End If
        Next
        SrNocount = 1
        Dim ClientRawMa = linq_obj.SP_Get_Order_RawMaterail_Brg_ID(System3ID).ToList()
        Dim dtClientRawMa As New DataTable
        dtClientRawMa.Columns.Add("PK_ID")
        dtClientRawMa.Columns.Add("Text", GetType(Boolean))
        dtClientRawMa.Columns.Add("Image", GetType(Boolean))
        dtClientRawMa.Columns.Add("SrNo")
        dtClientRawMa.Columns.Add("ItemName")
        dtClientRawMa.Columns.Add("Description")
        For Each item As SP_Get_Order_RawMaterail_Brg_IDResult In ClientRawMa
            dtClientRawMa.Rows.Add(item.Pk_OrderCategory_Detaild_ID, 1, 1, SrNocount, item.Item_Name, item.Remarks)
            SrNocount = SrNocount + 1
        Next
        GvClientRawMaterial.DataSource = dtClientRawMa
        GvClientRawMaterial.Columns(0).Visible = False

    End Sub
    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        UpdateAddressData()
        InsertOrderData()


        GvOrderAgreementList_Bind()
    End Sub
    Public Sub Progress_Bar(ByVal incrr_per As Integer)

        If incrr_per = 0 Then
            increment = 0
        End If
        increment = increment + incrr_per
        ProgressBar1.Value = increment

    End Sub
    Public Sub InsertOrderData()

        'Insert Order Agreement Data

        If (btnSubmit.Text = "Submit") Then
            Pk_Order_Agreement_ID = linq_obj.SP_Insert_Update_Order_Agreement_Master(0, Fk_Address_ID, Class1.global.UserID, txtEnqNo.Text, txtOrderNO.Text, ddlQtnType.Text, txtCapacity.Text, txtPPlant.Text, txtPModel.Text, txtEditDiscount.Text, "", txtOrderDate.Value.Date, txtGstNo.Text, txtGPanNo.Text, txtGlName.Text, txtGTradeName.Text)
            linq_obj.SubmitChanges()
        Else

            Dim Id = linq_obj.SP_Insert_Update_Order_Agreement_Master(Pk_Order_Agreement_ID, Fk_Address_ID, Class1.global.UserID, txtEnqNo.Text, txtOrderNO.Text, ddlQtnType.Text, txtCapacity.Text, txtPPlant.Text, txtPModel.Text, txtEditDiscount.Text, "", txtOrderDate.Value.Date, txtGstNo.Text, txtGPanNo.Text, txtGlName.Text, txtGTradeName.Text)
            linq_obj.SubmitChanges()
        End If

        'Insert Order Technical Data 
        Insert_Update_Tech_Data(Pk_Order_Agreement_ID)

        'Client Photo
        Dim ms1 As New MemoryStream
        Dim ms2 As New MemoryStream
        Dim ms3 As New MemoryStream
        Dim ms4 As New MemoryStream
        Dim Pic1Data As Byte()
        Pic1Data = Nothing
        If Not PicPhoto1.Image Is Nothing Then
            PicPhoto1.Image.Save(ms1, PicPhoto1.Image.RawFormat)
            Pic1Data = ms1.GetBuffer()
        End If
        Dim Pic2Data As Byte()
        Pic2Data = Nothing

        If Not PicPhoto2.Image Is Nothing Then
            PicPhoto2.Image.Save(ms2, PicPhoto2.Image.RawFormat)
            Pic2Data = ms2.GetBuffer()
        End If
        Dim Pic3Data As Byte()
        Pic3Data = Nothing

        If Not PicPPic1.Image Is Nothing Then
            PicPPic1.Image.Save(ms3, PicPPic1.Image.RawFormat)
            Pic3Data = ms3.GetBuffer()
        End If
        Dim Pic4Data As Byte()
        Pic4Data = Nothing

        If Not PicPPic2.Image Is Nothing Then
            PicPPic2.Image.Save(ms4, PicPPic2.Image.RawFormat)
            Pic4Data = ms4.GetBuffer()
        End If
        Try
            Dim ClientPhoto = linq_obj.SP_insert_Update_Order_ClientPhoto_Master(Pk_Order_Agreement_ID, Pic1Data, txtCName1.Text, txtCMobile1.Text, txtCBusiness1.Text, "", "", txtCEmail1.Text, Pic2Data, txtCName2.Text, txtCMobile2.Text, txtCBusiness2.Text, "", "", txtCEmail2.Text, Pic3Data, txtPName1.Text, txtPMobile1.Text, txtPBusiness1.Text, "", "", txtPEmail1.Text, Pic4Data, txtPName2.Text, txtPMobile2.Text, txtPBusiness2.Text, "", "", txtPEmail2.Text)
            linq_obj.SubmitChanges()


            'Order Agreement Terms

            If txtPT_FirstAdvance.Text.Trim() = "" Then
                txtPT_FirstAdvance.Text = "0"
            End If
            If txtPT_SecondAdvance.Text.Trim() = "" Then
                txtPT_SecondAdvance.Text = "0"
            End If
            If txtPT_FinalPayment.Text.Trim() = "" Then
                txtPT_FinalPayment.Text = "0"
            End If
            If txtPT_PaymentReceive.Text.Trim() = "" Then
                txtPT_PaymentReceive.Text = "0"
            End If
            If txtPT_TentaDT.Text.Trim() = "" Then
                txtPT_TentaDT.Text = "01-01-1900 00:00:00"
            End If
            If txtPT_DispDT.Text.Trim() = "" Then
                txtPT_DispDT.Text = "01-01-1900 00:00:00"
            End If

            If (btnSubmit.Text = "Submit") Then
                linq_obj.SP_Insert_Update_Order_Agreement_Terms(0, Pk_Order_Agreement_ID, txtPT_FinalPayment.Text, txtPT_SecondAdvance.Text, txtPT_FinalPayment.Text, txtPT_PaymentReceive.Text, Convert.ToDateTime(txtPT_TentaDT.Text), Convert.ToDateTime(txtPT_DispDT.Text), txtPT_Errection1.Text, txtPT_Errection2.Text, txtPT_Gaurantee1.Text, txtPT_Gaurantee2.Text, txtPT_Terms1.Text, txtPT_Terms2.Text, txtPT_Terms3.Text, txtPT_Terms4.Text, txtPT_Terms5.Text, txtPT_Terms.Text, txtPT_Gov.Text, txtPT_Deliver.Text, txtPT_Transp1.Text, txtPT_Transp2.Text, txtPT_Transp3.Text, txtPT_Transp4.Text)

                linq_obj.SubmitChanges()
            Else

                linq_obj.SP_Insert_Update_Order_Agreement_Terms(Pk_Order_Agreement_ID, Pk_Order_Agreement_ID, txtPT_FinalPayment.Text, txtPT_SecondAdvance.Text, txtPT_FinalPayment.Text, txtPT_PaymentReceive.Text, Convert.ToDateTime(txtPT_TentaDT.Text), Convert.ToDateTime(txtPT_DispDT.Text), txtPT_Errection1.Text, txtPT_Errection2.Text, txtPT_Gaurantee1.Text, txtPT_Gaurantee2.Text, txtPT_Terms1.Text, txtPT_Terms2.Text, txtPT_Terms3.Text, txtPT_Terms4.Text, txtPT_Terms5.Text, txtPT_Terms.Text, txtPT_Gov.Text, txtPT_Deliver.Text, txtPT_Transp1.Text, txtPT_Transp2.Text, txtPT_Transp3.Text, txtPT_Transp4.Text)

                linq_obj.SubmitChanges()
            End If

            'Order Acceptance 
            Dim Pk_User_ID As Integer

            Dim dataUser = linq_obj.SP_Get_UserList().ToList().Where(Function(p) p.UserName.ToLower() = txtusername1.Text.ToLower())

            For Each item As SP_Get_UserListResult In dataUser
                Pk_User_ID = item.Pk_UserId

            Next
            linq_obj.SP_Insert_Update_Order_Agreement_Acceptance(Pk_Order_Agreement_ID, Pk_User_ID, txtRemarks1.Text, txtRemarks2.Text, txtRemarks3.Text, txtRemarks4.Text, txtRemarks5.Text)
            linq_obj.SubmitChanges()
            If (btnSubmit.Text = "Submit") Then
                MessageBox.Show("Submit Sucessfully.....")
            Else
                MessageBox.Show("Update Sucessfully.....")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



    End Sub
    Public Sub UpdateAddressData()

        linq_obj.SP_Update_Address_Master_From_OrderAgreement_By_EnqNo(Fk_Address_ID, txtCompanyName.Text, txtAddress.Text, txtCity.Text, txtTaluko.Text, txtDistrict.Text, txtState.Text, txtPincode.Text, txtRAddress.Text, txtRCity.Text, txtRTaluko.Text, txtRDistrict.Text, txtRState.Text, txtRPincode.Text)
        linq_obj.SubmitChanges()

    End Sub

    Public Sub Insert_Update_Tech_Data(ByVal Pk_Order_Agreement_ID As Integer)
        Dim Serverpath As String
        Serverpath = ""

        'Delete Previous Entry Both Table

        linq_obj.SP_Delete_Order_Agreement_TechData_By_Order_ID(Pk_Order_Agreement_ID)
        linq_obj.SubmitChanges()

        linq_obj.SP_Delete_Order_Agreement_Client_TechData_By_Order_ID(Pk_Order_Agreement_ID)
        linq_obj.SubmitChanges()
        'System 1 
        Dim SrNocount As Integer
        Dim IsImage As Integer
        IsImage = 0
        SrNocount = 1
        For i As Integer = 0 To GvSystem1.RowCount - 1
            'Image
            IsImage = 0
            Dim IsTickedI As Boolean = CBool(GvSystem1.Rows(i).Cells(2).Value)
            If IsTickedI Then
                IsImage = 1
            End If
            'Text 
            Dim IsTicked As Boolean = CBool(GvSystem1.Rows(i).Cells(1).Value)
            If IsTicked Then
                linq_obj.SP_Insert_Order_Agreement_TechData(Pk_Order_Agreement_ID,
                                                            Convert.ToInt64(GvSystem1.Rows(i).Cells("PK_ID").Value),
                                                            SrNocount,
                                                            GvSystem1.Rows(i).Cells("ItemName").Value,
                                                            "",
                                                          Convert.ToDecimal(GvSystem1.Rows(i).Cells("Price").Value),
                                                           Convert.ToDecimal(GvSystem1.Rows(i).Cells("Disc").Value),
                                                          Convert.ToDecimal(GvSystem1.Rows(i).Cells("FinalRate").Value),
                                                           Convert.ToDecimal(GvSystem1.Rows(i).Cells("GST").Value),
                                                           Convert.ToDecimal(GvSystem1.Rows(i).Cells("GSTAmt").Value),
                                                           Convert.ToDecimal(GvSystem1.Rows(i).Cells("FinalAmount").Value),
                                                            "",
                                                          IsImage)
                linq_obj.SubmitChanges()
                SrNocount = SrNocount + 1
            End If
        Next


        SrNocount = 1
        'System 1 Tech Data        
        For i As Integer = 0 To GvSystem1Tech.RowCount - 1
            'Text 
            Dim IsTicked As Boolean = CBool(GvSystem1Tech.Rows(i).Cells(1).Value)
            If IsTicked Then
                linq_obj.SP_Insert_Order_Agreement_Client_TechData(Pk_Order_Agreement_ID, Convert.ToInt64(GvSystem1Tech.Rows(i).Cells("PK_ID").Value), SrNocount, GvSystem1Tech.Rows(i).Cells("ItemName").Value, GvSystem1Tech.Rows(i).Cells("Description").Value)
                linq_obj.SubmitChanges()
                SrNocount = SrNocount + 1
            End If
        Next

        'System 2
        SrNocount = 1
        For i As Integer = 0 To GvSystem2.RowCount - 1
            'Image
            IsImage = 0
            Dim IsTickedI As Boolean = CBool(GvSystem2.Rows(i).Cells(2).Value)
            If IsTickedI Then
                IsImage = 1
            End If
            'Text 
            Dim IsTicked As Boolean = CBool(GvSystem2.Rows(i).Cells(1).Value)
            If IsTicked Then

                linq_obj.SP_Insert_Order_Agreement_TechData(Pk_Order_Agreement_ID,
                                                         Convert.ToInt64(GvSystem2.Rows(i).Cells("PK_ID").Value),
                                                         SrNocount,
                                                         GvSystem2.Rows(i).Cells("ItemName").Value,
                                                         "",
                                                         Convert.ToDecimal(GvSystem2.Rows(i).Cells("Price").Value),
                                                         Convert.ToDecimal(GvSystem2.Rows(i).Cells("Disc").Value),
                                                         Convert.ToDecimal(GvSystem2.Rows(i).Cells("FinalRate").Value),
                                                         Convert.ToDecimal(GvSystem2.Rows(i).Cells("GST").Value),
                                                         Convert.ToDecimal(GvSystem2.Rows(i).Cells("GSTAmt").Value),
                                                         Convert.ToDecimal(GvSystem2.Rows(i).Cells("FinalAmount").Value),
                                                         "",
                                                         IsImage)
                linq_obj.SubmitChanges()
                SrNocount = SrNocount + 1
            End If
        Next

        'System 3  

        SrNocount = 1
        For i As Integer = 0 To GvSystem3.RowCount - 1
            'Image
            IsImage = 0
            Dim IsTickedI As Boolean = CBool(GvSystem3.Rows(i).Cells(2).Value)
            If IsTickedI Then
                IsImage = 1
            End If
            'Text 
            Dim IsTicked As Boolean = CBool(GvSystem3.Rows(i).Cells(1).Value)
            If IsTicked Then
                linq_obj.SP_Insert_Order_Agreement_TechData(Pk_Order_Agreement_ID,
                                                        Convert.ToInt64(GvSystem3.Rows(i).Cells("PK_ID").Value),
                                                        SrNocount,
                                                        GvSystem3.Rows(i).Cells("ItemName").Value,
                                                         GvSystem3.Rows(i).Cells("Capacity").Value,
                                                        Convert.ToDecimal(GvSystem3.Rows(i).Cells("Price").Value),
                                                        Convert.ToDecimal(GvSystem3.Rows(i).Cells("Disc").Value),
                                                        Convert.ToDecimal(GvSystem3.Rows(i).Cells("FinalRate").Value),
                                                        Convert.ToDecimal(GvSystem3.Rows(i).Cells("GST").Value),
                                                        Convert.ToDecimal(GvSystem3.Rows(i).Cells("GSTAmt").Value),
                                                        Convert.ToDecimal(GvSystem3.Rows(i).Cells("FinalAmount").Value),
                                                        GvSystem3.Rows(i).Cells("Description").Value,
                                                        IsImage)
                linq_obj.SubmitChanges()
                SrNocount = SrNocount + 1
            End If
        Next
        'System 4   
        SrNocount = 1
        For i As Integer = 0 To GvSystem4.RowCount - 1
            'Image
            IsImage = 0

            Dim IsTickedI As Boolean = CBool(GvSystem4.Rows(i).Cells(2).Value)
            If IsTickedI Then
                IsImage = 1
            End If
            'Text 
            Dim IsTicked As Boolean = CBool(GvSystem4.Rows(i).Cells(1).Value)
            If IsTicked Then
                linq_obj.SP_Insert_Order_Agreement_TechData(Pk_Order_Agreement_ID,
                                                       Convert.ToInt64(GvSystem4.Rows(i).Cells("PK_ID").Value),
                                                       SrNocount,
                                                       GvSystem4.Rows(i).Cells("ItemName").Value,
                                                        "",
                                                        Convert.ToDecimal(GvSystem4.Rows(i).Cells("Price").Value),
                                                        Convert.ToDecimal(GvSystem4.Rows(i).Cells("Disc").Value),
                                                        Convert.ToDecimal(GvSystem4.Rows(i).Cells("FinalRate").Value),
                                                        Convert.ToDecimal(GvSystem4.Rows(i).Cells("GST").Value),
                                                       Convert.ToDecimal(GvSystem4.Rows(i).Cells("GSTAmt").Value),
                                                       Convert.ToDecimal(GvSystem4.Rows(i).Cells("FinalAmount").Value),
                                                       GvSystem4.Rows(i).Cells("Description").Value,
                                                       IsImage)
                linq_obj.SubmitChanges()
                SrNocount = SrNocount + 1
            End If
        Next
        'Client Scope - General 

        GvClientRawMaterial_Bind()
        SrNocount = 1
        For i As Integer = 0 To GvClientGeneralNew.RowCount - 1

            'Text 
            Dim IsTicked As Boolean = CBool(GvClientGeneralNew.Rows(i).Cells(1).Value)
            If IsTicked Then
                linq_obj.SP_Insert_Order_Agreement_Client_TechData(Pk_Order_Agreement_ID, Convert.ToInt64(GvClientGeneralNew.Rows(i).Cells("PK_ID").Value), SrNocount, GvClientGeneralNew.Rows(i).Cells("ItemName").Value, GvClientGeneralNew.Rows(i).Cells("Description").Value)
                linq_obj.SubmitChanges()
                SrNocount = SrNocount + 1

            End If
        Next

        'Client Scope - Raw Material
        SrNocount = 1
        For i As Integer = 0 To GvClientRawMaterial.RowCount - 1

            'Text 
            Dim IsTicked As Boolean = CBool(GvClientRawMaterial.Rows(i).Cells(1).Value)
            If IsTicked Then
                linq_obj.SP_Insert_Order_Agreement_Client_TechData(Pk_Order_Agreement_ID, Convert.ToInt64(GvClientRawMaterial.Rows(i).Cells("PK_ID").Value), SrNocount, GvClientRawMaterial.Rows(i).Cells("ItemName").Value, GvClientRawMaterial.Rows(i).Cells("Description").Value)
                linq_obj.SubmitChanges()
                SrNocount = SrNocount + 1
            End If
        Next

        'Client Scope - Man Power 
        SrNocount = 1
        For i As Integer = 0 To GvClientManPower.RowCount - 1

            'Text 
            Dim IsTicked As Boolean = CBool(GvClientManPower.Rows(i).Cells(1).Value)
            If IsTicked Then
                linq_obj.SP_Insert_Order_Agreement_Client_TechData(Pk_Order_Agreement_ID, Convert.ToInt64(GvClientManPower.Rows(i).Cells("PK_ID").Value), SrNocount, GvClientManPower.Rows(i).Cells("ItemName").Value, GvClientManPower.Rows(i).Cells("Description").Value)
                linq_obj.SubmitChanges()
                SrNocount = SrNocount + 1
            End If
        Next
        'Complimentory
        SrNocount = 1
        For i As Integer = 0 To GvComplimentory.RowCount - 1

            'Text 
            Dim IsTicked As Boolean = CBool(GvComplimentory.Rows(i).Cells(1).Value)
            If IsTicked Then
                linq_obj.SP_Insert_Order_Agreement_Client_TechData(Pk_Order_Agreement_ID, Convert.ToInt64(GvComplimentory.Rows(i).Cells("PK_ID").Value), SrNocount, GvComplimentory.Rows(i).Cells("ItemName").Value, GvComplimentory.Rows(i).Cells("Description").Value)
                linq_obj.SubmitChanges()
                SrNocount = SrNocount + 1
            End If
        Next

    End Sub

    Public Sub Generate_OrderPage1()

        'First Page

        OrderAgreementDStmp = New DataSet
        OrderAgreementDSNew = New DataSet
        Dim dt As New DataTable("OrderPage1")
        dt.Columns.Add("CompanyName")
        dt.Columns.Add("Station")
        dt.Columns.Add("Dist")
        dt.Columns.Add("State")
        dt.Columns.Add("ContactNo")
        dt.Columns.Add("Email")
        dt.Columns.Add("Model")
        dt.Columns.Add("Capacity")
        dt.Columns.Add("Photo1", GetType(Byte()))
        dt.Columns.Add("Photo1Name")
        dt.Columns.Add("Photo2", GetType(Byte()))
        dt.Columns.Add("Photo2Name")

        Dim photo1 As String

        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SP_Get_Order_Agreement_Master_ID"
        cmd.Connection = linq_obj.Connection
        cmd.Parameters.Add("@PK_Order_Agreement_ID", SqlDbType.BigInt).Value = Pk_Order_Agreement_ID
        Dim da As New SqlDataAdapter()
        da.SelectCommand = cmd
        bits1 = Nothing
        bits2 = Nothing
        bits3 = Nothing
        bits4 = Nothing

        da.Fill(OrderAgreementDStmp, "OrderAgreementDStmp")
        For index = 0 To OrderAgreementDStmp.Tables(0).Rows.Count - 1
            'Dim str As String
            Dim CompanyName As String
            CompanyName = ""

            If OrderAgreementDStmp.Tables(0).Rows(0)("Photo1").ToString().Length > 1 Then
                bits1 = CType(OrderAgreementDStmp.Tables(0).Rows(0)("Photo1"), Byte())
            End If
            If OrderAgreementDStmp.Tables(0).Rows(0)("Photo2").ToString().Length > 1 Then
                bits2 = CType(OrderAgreementDStmp.Tables(0).Rows(0)("Photo2"), Byte())
            End If
            If OrderAgreementDStmp.Tables(0).Rows(0)("Photo3").ToString().Length > 1 Then
                bits3 = CType(OrderAgreementDStmp.Tables(0).Rows(0)("Photo3"), Byte())
            End If
            If chkCompany.Checked = True Then
                CompanyName = "M/s.                                                              ."
            Else
                CompanyName = OrderAgreementDStmp.Tables(0).Rows(index)("Name")
            End If
            dt.Rows.Add(
                        CompanyName,
                        OrderAgreementDStmp.Tables(0).Rows(index)("City"),
                        OrderAgreementDStmp.Tables(0).Rows(index)("District"),
                        OrderAgreementDStmp.Tables(0).Rows(index)("State"),
                        txtCMobile1.Text.Trim() + "," + txtCMobile2.Text.Trim(),
                        OrderAgreementDStmp.Tables(0).Rows(index)("EmailID"),
                        "MODEL : " + txtPModel.Text,
                        "CAPACITY : " + txtPCapacity.Text + " LPH",
                        bits1,
                        OrderAgreementDStmp.Tables(0).Rows(index)("Ph1_Value1"),
                        bits2,
                        OrderAgreementDStmp.Tables(0).Rows(index)("Ph2_Value1"))

        Next
        OrderAgreementDSNew.Tables.Add(dt)
        Class1.WriteXMlFile(OrderAgreementDSNew, "SP_Get_OrderAgreement_By_EnqNo", "OrderPage1")
        Dim rpt As New rpt_OrderAgreementFinal1_New
        rpt.Database.Tables(0).SetDataSource(OrderAgreementDSNew.Tables("OrderPage1"))
        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\1.pdf")

        rpt.Dispose()

    End Sub


    Public Sub Generate_OrderPage2()
        'Second Page       
        Dim dt As New DataTable()
        OrderAgreementDStmp = New DataSet()
        OrderAgreementDSNew = New DataSet()

        dt = New DataTable("OrderPage2")
        dt.Columns.Add("OrderNo")
        dt.Columns.Add("CompanyName")
        dt.Columns.Add("Address")
        dt.Columns.Add("City")
        dt.Columns.Add("District")
        dt.Columns.Add("State")
        dt.Columns.Add("Pincode")
        dt.Columns.Add("R_Address")
        dt.Columns.Add("R_City")
        dt.Columns.Add("R_District")
        dt.Columns.Add("R_State")
        dt.Columns.Add("R_Pincode")
        dt.Columns.Add("P_Plant")
        dt.Columns.Add("P_Type")
        dt.Columns.Add("P_Scheme")
        dt.Columns.Add("P_Capacity")
        dt.Columns.Add("P_Model")
        dt.Columns.Add("O_Date")
        dt.Columns.Add("GstNo")
        dt.Columns.Add("Panno")
        dt.Columns.Add("LName")
        dt.Columns.Add("LTrade")

        dt.Columns.Add("ContactName1")
        dt.Columns.Add("ContactMobile1")
        dt.Columns.Add("ContactEmail1")

        dt.Columns.Add("ContactName2")
        dt.Columns.Add("ContactMobile2")
        dt.Columns.Add("ContactEmail2")

        dt.Columns.Add("ContactName3")
        dt.Columns.Add("ContactMobile3")
        dt.Columns.Add("ContactEmail3")

        dt.Columns.Add("ContactName4")
        dt.Columns.Add("ContactMobile4")
        dt.Columns.Add("ContactEmail4")



        cmd = New SqlCommand
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SP_Get_Order_Agreement_Master_ID"
        cmd.Connection = linq_obj.Connection
        cmd.Parameters.Add("@Pk_Order_Agreement_ID", SqlDbType.BigInt).Value = Pk_Order_Agreement_ID
        da = New SqlDataAdapter()
        da.SelectCommand = cmd
        da.Fill(OrderAgreementDStmp, "OrderPage2")

        Dim Name1, Name2, Name3, Name4 As String
        Name1 = ""
        Name2 = ""
        Name3 = ""
        Name4 = ""


        If txtCName1.Text.Trim() <> "" Then
            Name1 = "1) " + txtCName1.Text

        End If
        If txtCName2.Text.Trim() <> "" Then
            Name2 = "2) " + txtCName2.Text

        End If
        If txtPName1.Text.Trim() <> "" Then
            Name3 = "3) " + txtPName1.Text

        End If
        If txtPName2.Text.Trim() <> "" Then
            Name4 = "4) " + txtPName2.Text

        End If


        For index = 0 To OrderAgreementDStmp.Tables(0).Rows.Count - 1
            'Dim str As String  
            dt.Rows.Add(
                        txtOrderNO.Text,
                        txtCompanyName.Text,
                        txtAddress.Text,
                         txtCity.Text,
                         txtDistrict.Text,
                         txtState.Text,
                         txtPincode.Text,
                         txtRAddress.Text,
                         txtRCity.Text,
                         txtRDistrict.Text,
                         txtRState.Text,
                         txtRPincode.Text,
                         txtPPlant.Text, txtPType.Text, "", txtPCapacity.Text, txtPModel.Text, txtOrderDate.Text, txtGstNo.Text, txtGPanNo.Text, txtGlName.Text, txtGTradeName.Text,
           Name1, txtCMobile1.Text, txtCEmail1.Text,
            Name2, txtCMobile2.Text, txtCEmail2.Text,
           Name3, txtPMobile1.Text, txtPEmail1.Text,
             Name4, txtPMobile2.Text, txtPEmail2.Text)



        Next
        OrderAgreementDSNew.Tables.Add(dt)
        Class1.WriteXMlFile(OrderAgreementDSNew, "SP_Get_Order_Agreement_Master_ID", "OrderPage2")
        Dim rpt3 As New rpt_OrderAgreementFinal2_New
        rpt3.Database.Tables(0).SetDataSource(OrderAgreementDSNew.Tables("OrderPage2"))
        rpt3.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\2.pdf")

        rpt3.Dispose()

    End Sub

    Public Sub Generate_OrderPage3()
        'Second Page       

        Dim dt As New DataTable()
        OrderAgreementDStmp = New DataSet()
        OrderAgreementDSNew = New DataSet()
        dt = New DataTable("OrderPage3")
        dt.Columns.Add("T_Image1", GetType(Byte()))
        dt.Columns.Add("T_Image2", GetType(Byte()))
        dt.Columns.Add("T_Image3", GetType(Byte()))
        dt.Columns.Add("T_Image4", GetType(Byte()))
        dt.Columns.Add("T_Image5", GetType(Byte()))
        dt.Columns.Add("T_Image6", GetType(Byte()))
        dt.Columns.Add("T_Image7", GetType(Byte()))
        dt.Columns.Add("T_Image8", GetType(Byte()))
        dt.Columns.Add("T_Image9", GetType(Byte()))
        dt.Columns.Add("T_Image10", GetType(Byte()))
        dt.Columns.Add("T_Image11", GetType(Byte()))
        dt.Columns.Add("T_Image12", GetType(Byte()))
        dt.Columns.Add("T_Image13", GetType(Byte()))
        dt.Columns.Add("T_Image14", GetType(Byte()))
        dt.Columns.Add("T_Image15", GetType(Byte()))
        dt.Columns.Add("Sys1_Tech1")
        dt.Columns.Add("Sys1_Tech2")
        dt.Columns.Add("Sys1_Tech3")
        dt.Columns.Add("Sys1_Tech4")
        dt.Columns.Add("Sys1_Tech5")
        dt.Columns.Add("Sys1_Tech6")
        dt.Columns.Add("Sys1_Tech7")
        dt.Columns.Add("Sys1_Tech8")
        dt.Columns.Add("Sys1_Tech9")
        dt.Columns.Add("Sys1_Tech10")
        dt.Columns.Add("Sys1_Tech11")
        dt.Columns.Add("Sys1_Tech12")
        dt.Columns.Add("Sys1_Tech13")
        cmd = New SqlCommand
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SP_Get_Order_Agreement_Master_ID"
        cmd.Connection = linq_obj.Connection
        cmd.Parameters.Add("@Pk_Order_Agreement_ID", SqlDbType.BigInt).Value = Pk_Order_Agreement_ID
        da = New SqlDataAdapter()
        da.SelectCommand = cmd
        da.Fill(OrderAgreementDStmp, "OrderPage3")

        Dim Sys1_Tech1, Sys1_Tech2, Sys1_Tech3, Sys1_Tech4, Sys1_Tech5, Sys1_Tech6, Sys1_Tech7, Sys1_Tech8, Sys1_Tech9, Sys1_Tech10, Sys1_Tech11, Sys1_Tech12, Sys1_Tech13 As String


        Dim I_ms1 As New MemoryStream
        Dim I_ms2 As New MemoryStream
        Dim I_ms3 As New MemoryStream
        Dim I_ms4 As New MemoryStream
        Dim I_ms5 As New MemoryStream
        Dim I_ms6 As New MemoryStream
        Dim I_ms7 As New MemoryStream
        Dim I_ms8 As New MemoryStream
        Dim I_ms9 As New MemoryStream
        Dim I_ms10 As New MemoryStream
        Dim I_ms11 As New MemoryStream
        Dim I_ms12 As New MemoryStream
        Dim I_ms13 As New MemoryStream
        Dim I_ms14 As New MemoryStream
        Dim I_ms15 As New MemoryStream


        Dim I_by1 As Byte()
        Dim I_by2 As Byte()
        Dim I_by3 As Byte()
        Dim I_by4 As Byte()
        Dim I_by5 As Byte()
        Dim I_by6 As Byte()
        Dim I_by7 As Byte()
        Dim I_by8 As Byte()
        Dim I_by9 As Byte()
        Dim I_by10 As Byte()
        Dim I_by11 As Byte()
        Dim I_by12 As Byte()
        Dim I_by13 As Byte()
        Dim I_by14 As Byte()
        Dim I_by15 As Byte()

        I_by1 = Nothing
        I_by2 = Nothing
        I_by3 = Nothing
        I_by4 = Nothing
        I_by5 = Nothing
        I_by6 = Nothing
        I_by7 = Nothing
        I_by8 = Nothing
        I_by9 = Nothing
        I_by10 = Nothing
        I_by11 = Nothing
        I_by12 = Nothing
        I_by13 = Nothing
        I_by14 = Nothing
        I_by15 = Nothing

        If Not Img_PicMain.Image Is Nothing Then
            Img_PicMain.Image.Save(I_ms1, Img_PicMain.Image.RawFormat)
            I_by1 = I_ms1.GetBuffer()
        End If

        If Not Pic_Sys2_1.Image Is Nothing Then
            Pic_Sys2_1.Image.Save(I_ms2, Pic_Sys2_1.Image.RawFormat)
            I_by2 = I_ms2.GetBuffer()
        End If

        If Not Pic_Sys3_1.Image Is Nothing Then
            Pic_Sys3_1.Image.Save(I_ms3, Pic_Sys3_1.Image.RawFormat)
            I_by3 = I_ms3.GetBuffer()
        End If
        If Not Pic_Sys3_2.Image Is Nothing Then
            Pic_Sys3_2.Image.Save(I_ms4, Pic_Sys3_2.Image.RawFormat)
            I_by4 = I_ms4.GetBuffer()
        End If
        If Not Pic_Sys3_3.Image Is Nothing Then
            Pic_Sys3_3.Image.Save(I_ms5, Pic_Sys3_3.Image.RawFormat)
            I_by5 = I_ms5.GetBuffer()
        End If
        If Not Pic_Sys3_4.Image Is Nothing Then
            Pic_Sys3_4.Image.Save(I_ms6, Pic_Sys3_4.Image.RawFormat)
            I_by6 = I_ms6.GetBuffer()
        End If
        If Not Pic_Sys3_5.Image Is Nothing Then
            Pic_Sys3_5.Image.Save(I_ms7, Pic_Sys3_5.Image.RawFormat)
            I_by7 = I_ms7.GetBuffer()
        End If
        If Not Pic_Sys3_6.Image Is Nothing Then
            Pic_Sys3_6.Image.Save(I_ms8, Pic_Sys3_6.Image.RawFormat)
            I_by8 = I_ms8.GetBuffer()
        End If
        If Not Pic_Sys3_7.Image Is Nothing Then
            Pic_Sys3_7.Image.Save(I_ms9, Pic_Sys3_7.Image.RawFormat)
            I_by9 = I_ms9.GetBuffer()
        End If
        If Not Pic_Sys3_8.Image Is Nothing Then
            Pic_Sys3_8.Image.Save(I_ms10, Pic_Sys3_8.Image.RawFormat)
            I_by10 = I_ms10.GetBuffer()
        End If


        If Not Pic_Sys4_1.Image Is Nothing Then
            Pic_Sys4_1.Image.Save(I_ms11, Pic_Sys4_1.Image.RawFormat)
            I_by11 = I_ms11.GetBuffer()
        End If
        If Not Pic_Sys4_2.Image Is Nothing Then
            Pic_Sys4_2.Image.Save(I_ms12, Pic_Sys4_2.Image.RawFormat)
            I_by12 = I_ms12.GetBuffer()
        End If
        If Not Pic_Sys4_3.Image Is Nothing Then
            Pic_Sys4_3.Image.Save(I_ms13, Pic_Sys4_3.Image.RawFormat)
            I_by13 = I_ms13.GetBuffer()
        End If
        If Not Pic_Sys4_4.Image Is Nothing Then
            Pic_Sys4_4.Image.Save(I_ms14, Pic_Sys4_4.Image.RawFormat)
            I_by14 = I_ms14.GetBuffer()
        End If
        If Not Pic_Sys4_5.Image Is Nothing Then
            Pic_Sys4_5.Image.Save(I_ms15, Pic_Sys4_5.Image.RawFormat)
            I_by15 = I_ms15.GetBuffer()
        End If

        'System 1 Technical Data
        Dim count As Integer
        count = 0
        Sys1_Tech1 = ""
        Sys1_Tech2 = ""
        Sys1_Tech3 = ""
        Sys1_Tech4 = ""
        Sys1_Tech5 = ""
        Sys1_Tech6 = ""
        Sys1_Tech7 = ""
        Sys1_Tech8 = ""
        Sys1_Tech9 = ""
        Sys1_Tech10 = ""
        Sys1_Tech11 = ""
        Sys1_Tech12 = ""
        Sys1_Tech13 = ""



        For index = 0 To GvSystem1Tech.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvSystem1Tech.Rows(index).Cells(1).Value)
            If IsTicked Then
                If count = 0 Then
                    Sys1_Tech1 = "1) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If

                If count = 1 Then
                    Sys1_Tech2 = "2) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If

                If count = 2 Then
                    Sys1_Tech3 = "3) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If

                If count = 3 Then
                    Sys1_Tech4 = "4) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If

                If count = 4 Then
                    Sys1_Tech5 = "5) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If

                If count = 5 Then
                    Sys1_Tech6 = "6) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If

                If count = 6 Then
                    Sys1_Tech7 = "7) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If

                If count = 7 Then
                    Sys1_Tech8 = "8) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If

                If count = 8 Then
                    Sys1_Tech9 = "9) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If
                If count = 9 Then
                    Sys1_Tech10 = "10) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If
                If count = 10 Then
                    Sys1_Tech11 = "11) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If
                If count = 11 Then
                    Sys1_Tech12 = "12) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If
                If count = 12 Then
                    Sys1_Tech13 = "13) " + GvSystem1Tech.Rows(index).Cells("ItemName").Value.ToString()
                End If
                count = count + 1

            End If
        Next

        dt.Rows.Add(
                    I_by1,
                    I_by2,
                    I_by3,
                    I_by4,
                    I_by5,
                    I_by6,
                    I_by7,
                    I_by8,
                    I_by9,
                    I_by10,
                    I_by11,
                    I_by12,
                    I_by13,
                    I_by14,
                    I_by15,
                      Sys1_Tech1,
                       Sys1_Tech2,
                       Sys1_Tech3,
                       Sys1_Tech4,
                       Sys1_Tech5,
                       Sys1_Tech6,
                       Sys1_Tech7,
                       Sys1_Tech8,
                       Sys1_Tech9,
                       Sys1_Tech10, Sys1_Tech11, Sys1_Tech12, Sys1_Tech13)

        OrderAgreementDSNew.Tables.Add(dt)
        Class1.WriteXMlFile(OrderAgreementDSNew, "SP_Get_Order_Agreement_Master_ID", "OrderPage3")
        Dim rpt3 As New rpt_OrderAgreementFinal3_New
        rpt3.Database.Tables(0).SetDataSource(OrderAgreementDSNew.Tables("OrderPage3"))
        rpt3.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\3.pdf")

        rpt3.Dispose()

    End Sub

    Public Sub Generate_OrderPage4()
        Try
            Dim OrderAgreementDStmp As New DataSet()
            Dim OrderAgreementDSNew As New DataSet()


            Dim dt = New DataTable("OrderPage4")
            dt.Columns.Add("G_Image1", GetType(Byte()))
            dt.Columns.Add("G_Text1")
            dt.Columns.Add("G_Image2", GetType(Byte()))
            dt.Columns.Add("G_Text2")
            dt.Columns.Add("G_Image3", GetType(Byte()))
            dt.Columns.Add("G_Text3")
            dt.Columns.Add("G_Image4", GetType(Byte()))
            dt.Columns.Add("G_Text4")
            dt.Columns.Add("G_Image5", GetType(Byte()))
            dt.Columns.Add("G_Text5")
            dt.Columns.Add("G_Image6", GetType(Byte()))
            dt.Columns.Add("G_Text6")

            dt.Columns.Add("R_Image1", GetType(Byte()))
            dt.Columns.Add("R_Text1")
            dt.Columns.Add("R_Image2", GetType(Byte()))
            dt.Columns.Add("R_Text2")
            dt.Columns.Add("R_Image3", GetType(Byte()))
            dt.Columns.Add("R_Text3")
            dt.Columns.Add("R_Image4", GetType(Byte()))
            dt.Columns.Add("R_Text4")
            dt.Columns.Add("R_Image5", GetType(Byte()))
            dt.Columns.Add("R_Text5")
            dt.Columns.Add("R_Image6", GetType(Byte()))
            dt.Columns.Add("R_Text6")
            dt.Columns.Add("R_Image7", GetType(Byte()))
            dt.Columns.Add("R_Text7")
            dt.Columns.Add("R_Image8", GetType(Byte()))
            dt.Columns.Add("R_Text8")

            dt.Columns.Add("M_Image1", GetType(Byte()))
            dt.Columns.Add("M_Text1")
            dt.Columns.Add("M_Image2", GetType(Byte()))
            dt.Columns.Add("M_Text2")
            dt.Columns.Add("M_Image3", GetType(Byte()))
            dt.Columns.Add("M_Text3")
            dt.Columns.Add("M_Image4", GetType(Byte()))
            dt.Columns.Add("M_Text4")
            dt.Columns.Add("M_Image5", GetType(Byte()))
            dt.Columns.Add("M_Text5")
            dt.Columns.Add("M_Image6", GetType(Byte()))
            dt.Columns.Add("M_Text6")

            Dim Serverpath As String
            Dim G_Image1() As Byte = Nothing
            Dim G_Image2() As Byte = Nothing
            Dim G_Image3() As Byte = Nothing
            Dim G_Image4() As Byte = Nothing
            Dim G_Image5() As Byte = Nothing
            Dim G_Image6() As Byte = Nothing
            Dim R_Image1() As Byte = Nothing
            Dim R_Image2() As Byte = Nothing
            Dim R_Image3() As Byte = Nothing
            Dim R_Image4() As Byte = Nothing
            Dim R_Image5() As Byte = Nothing
            Dim R_Image6() As Byte = Nothing
            Dim R_Image7() As Byte = Nothing
            Dim R_Image8() As Byte = Nothing
            Dim M_Image1() As Byte = Nothing
            Dim M_Image2() As Byte = Nothing
            Dim M_Image3() As Byte = Nothing
            Dim M_Image4() As Byte = Nothing
            Dim M_Image5() As Byte = Nothing
            Dim M_Image6() As Byte = Nothing

            Dim G_Text1, G_Text2, G_Text3, G_Text4, G_Text5, G_Text6, R_Text1, R_Text2, R_Text3, R_Text4, R_Text5, R_Text6, R_Text7, R_Text8, M_Text1, M_Text2, M_Text3, M_Text4, M_Text5, M_Text6 As String

            Serverpath = ""
            Dim imagecount As Integer
            imagecount = 1

            Dim MainCategory As String
            For ReportNo = 0 To 2
                imagecount = 1

                If ReportNo = 0 Then
                    MainCategory = "CLIENT SCOPE - GENERAL"
                    Dim ClientTechData = linq_obj.SP_Get_Order_Agreement_Client_TechData_By_ID(Pk_Order_Agreement_ID).ToList().Where(Function(p) p.MainCategory = MainCategory).ToList()
                    For Each item As SP_Get_Order_Agreement_Client_TechData_By_IDResult In ClientTechData
                        Serverpath = ""
                        Dim bArr As Byte()
                        If Convert.ToString(item.Item_Photo).Trim() <> "" Then
                            Serverpath = Convert.ToString(item.Item_Photo).Replace("D:", "\\192.168.1.102")
                            'Serverpath = "D:\\tmp.jpg"
                            'image to byteArray
                            Dim img As Image = Image.FromFile(Serverpath)
                            bArr = imgToByteConverter(img)
                            If imagecount = 1 Then
                                G_Image1 = bArr
                                G_Text1 = item.ItemName
                            End If
                            If imagecount = 2 Then
                                G_Image2 = bArr
                                G_Text2 = item.ItemName
                            End If
                            If imagecount = 3 Then
                                G_Image3 = bArr
                                G_Text3 = item.ItemName
                            End If
                            If imagecount = 4 Then
                                G_Image4 = bArr
                                G_Text4 = item.ItemName
                            End If
                            If imagecount = 5 Then
                                G_Image5 = bArr
                                G_Text5 = item.ItemName
                            End If
                            If imagecount = 6 Then
                                G_Image6 = bArr
                                G_Text6 = item.ItemName
                            End If
                            imagecount = imagecount + 1

                        End If
                    Next

                ElseIf ReportNo = 1 Then

                    MainCategory = "CLIENT SCOPE - RAW MATERIAL"
                    Dim ClientTechData = linq_obj.SP_Get_Order_Agreement_Client_TechData_By_ID(Pk_Order_Agreement_ID).ToList().Where(Function(p) p.MainCategory = MainCategory).ToList()
                    For Each item As SP_Get_Order_Agreement_Client_TechData_By_IDResult In ClientTechData
                        Serverpath = ""
                        Dim bArr As Byte()
                        If Convert.ToString(item.Item_Photo).Trim() <> "" Then
                            Serverpath = Convert.ToString(item.Item_Photo).Replace("D:", "\\192.168.1.102")
                            'Serverpath = "D:\\tmp.jpg"
                            'image to byteArray
                            Dim img As Image = Image.FromFile(Serverpath)
                            bArr = imgToByteConverter(img)
                            If imagecount = 1 Then
                                R_Image1 = bArr
                                R_Text1 = item.ItemName
                            End If
                            If imagecount = 2 Then
                                R_Image2 = bArr
                                R_Text2 = item.ItemName
                            End If
                            If imagecount = 3 Then
                                R_Image3 = bArr
                                R_Text3 = item.ItemName
                            End If
                            If imagecount = 4 Then
                                R_Image4 = bArr
                                R_Text4 = item.ItemName
                            End If
                            If imagecount = 5 Then
                                R_Image5 = bArr
                                R_Text5 = item.ItemName
                            End If
                            If imagecount = 6 Then
                                R_Image6 = bArr
                                R_Text6 = item.ItemName
                            End If
                            If imagecount = 7 Then
                                R_Image7 = bArr
                                R_Text7 = item.ItemName
                            End If
                            If imagecount = 8 Then
                                R_Image8 = bArr
                                R_Text8 = item.ItemName
                            End If
                            imagecount = imagecount + 1
                        End If
                    Next
                ElseIf ReportNo = 2 Then
                    MainCategory = "CLIENT SCOPE - MAN POWER"
                    Dim ClientTechData = linq_obj.SP_Get_Order_Agreement_Client_TechData_By_ID(Pk_Order_Agreement_ID).ToList().Where(Function(p) p.MainCategory = MainCategory).ToList()
                    For Each item As SP_Get_Order_Agreement_Client_TechData_By_IDResult In ClientTechData
                        Serverpath = ""
                        Dim bArr As Byte()
                        If Convert.ToString(item.Item_Photo).Trim() <> "" Then
                            Serverpath = Convert.ToString(item.Item_Photo).Replace("D:", "\\192.168.1.102")
                            'Serverpath = "D:\\tmp.jpg"
                            'image to byteArray
                            Dim img As Image = Image.FromFile(Serverpath)
                            bArr = imgToByteConverter(img)
                            If imagecount = 1 Then
                                M_Image1 = bArr
                                M_Text1 = item.ItemName
                            End If
                            If imagecount = 2 Then
                                M_Image2 = bArr
                                M_Text2 = item.ItemName
                            End If
                            If imagecount = 3 Then
                                M_Image3 = bArr
                                M_Text3 = item.ItemName
                            End If
                            If imagecount = 4 Then
                                M_Image4 = bArr
                                M_Text4 = item.ItemName
                            End If
                            If imagecount = 5 Then
                                M_Image5 = bArr
                                M_Text5 = item.ItemName
                            End If
                            If imagecount = 6 Then
                                M_Image6 = bArr
                                M_Text6 = item.ItemName
                            End If
                            imagecount = imagecount + 1
                        End If
                    Next
                End If

            Next


            dt.Rows.Add(G_Image1, G_Text1, G_Image2, G_Text2, G_Image3, G_Text3, G_Image4, G_Text4, G_Image5, G_Text5, G_Image6, G_Text6,
             R_Image1, R_Text1, R_Image2, R_Text2, R_Image3, R_Text3, R_Image4, R_Text4, R_Image5, R_Text5, R_Image6, R_Text6, R_Image7, R_Text7, R_Image8, R_Text8,
             M_Image1, M_Text1, M_Image2, M_Text2, M_Image3, M_Text3, M_Image4, M_Text4, M_Image5, M_Text5, M_Image6, M_Text6)


            OrderAgreementDSNew.Tables.Add(dt)
            Dim rpt4 As New rpt_OrderAgreementFinal4_New
            Class1.WriteXMlFile(OrderAgreementDSNew, "SP_Get_Order_Category_Detail_Master_ID", "OrderPage4")
            rpt4.Database.Tables(0).SetDataSource(OrderAgreementDSNew.Tables("OrderPage4"))
            rpt4.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\4.pdf")

            rpt4.Dispose()


        Catch ex As Exception
            MessageBox.Show(ex.InnerException.Message.ToString())
        End Try
    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        'Generate_Order_Acceptance()
        'If Pk_OrderID > 0 Then
        For Each filepath As String In Directory.GetFiles(OrderTempPath)
            File.Delete(filepath)
        Next

        



            Try
                OrderAgreementDStmp = New DataSet
                OrderAgreementDS = New DataSet

                If Fk_Address_ID > 0 Then
                    ' frmdialog.ShowDialog()
                    Dim _pdfforge As New PDF.PDF
                    ProgressBar1.Visible = True

                    Dim files(6) As String
                    If rblComplimentory1.Checked = True Or rblComplimentory2.Checked = True Then
                        ReDim Preserve files(7)
                    Else
                        ReDim Preserve files(6) 'remove complimentry  pdf file
                    End If

                    Progress_Bar(10)
                    'First Page
                    Generate_OrderPage1()

                    'Second  Page
                    Generate_OrderPage2()

                    Progress_Bar(30)
                    'Third Page         

                    Generate_OrderPage3()

                    'System 2 
                    'Order_Category_Master_ID = ""
                    'For i = 0 To GvSystem2.Rows.Count - 1
                    '    Dim IsTicked As Boolean = CBool(GvSystem2.Rows(i).Cells(1).Value)
                    '    If IsTicked Then
                    '        Order_Category_Master_ID = Order_Category_Master_ID + GvSystem2.Rows(i).Cells(0).Value + ","
                    '    End If

                    'Next
                    ' Get_PDf_From_DBPath(Order_Category_Master_ID, OrderTempPath) '4 
                    ' Fourth Page             

                    Generate_OrderPage4() '4 page

                    Progress_Bar(30)
                    Generate_Price_List() '5 Price List 

                    Generate_Payment_Terms() '6 Terms & condition

                    Generate_Order_Acceptance() '7 Order Acceptance

                    Progress_Bar(10)

                    Dim fileEntries As String() = Directory.GetFiles(OrderTempPath)
                    ' Process the list of files found in the directory.
                    Dim fileName As String
                    Dim count As Integer
                    count = 0
                    For Each fileName In fileEntries
                        files(count) = fileName
                        count = count + 1
                    Next fileName

                    If rblComplimentory1.Checked = True Then
                        files(count) = "\\192.168.1.102\ROERP\USER\\Comp\\8.pdf"
                    End If
                    If rblComplimentory2.Checked = True Then
                        files(count) = "\\192.168.1.102\ROERP\USER\\Comp\\9.pdf"
                    End If

                    Dim fullpath12 As String

                    'fullpath12 = "D:\ROERP\USER\RK\FinalOrderAgreement\" + txtOrderNO.Text.Trim().Replace("/", "-") + "-" + Convert.ToString(txtCapacity.Text) + "-" + txtCompanyName.Text.Trim().Replace("/", "-") + " - ISI" + ".pdf"
                    fullpath12 = "\\192.168.1.102\ROERP\USER\Final Agreement\" + txtOrderNO.Text.Trim().Replace("/", "-") + "-" + Convert.ToString(txtCapacity.Text) + "-" + txtCompanyName.Text.Trim().Replace("/", "-") + " - ISI" + ".pdf"
                    Progress_Bar(20)
                    _pdfforge.MergePDFFiles(files, fullpath12, False)
                    MessageBox.Show("Document Ready !")
                    Progress_Bar(0)
                    ProgressBar1.Value = increment

                    System.Diagnostics.Process.Start(fullpath12)
                    ProgressBar1.Visible = False

                Else
                    MessageBox.Show("EnqNo not found")

                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString())
                Progress_Bar(0)
            End Try

    End Sub
    Public Sub Generate_Price_List()
        'Second  Page
        Dim OrderPriceDS As New DataSet

        Dim Sys1_1, Sys1_2, Sys1_3, Sys1_4, Sys1_5, Sys2_1, Sys2_2, Sys2_3, Sys3_1, Sys3_2, Sys3_3, Sys3_4, Sys3_5, Sys3_6, Sys3_7, Sys3_8, Sys3_9, Sys4_1, Sys4_2, Sys4_3, Sys4_4, Sys4_5, Sys4_6 As String
        Dim Sys1_1_text, Sys1_2_text, Sys1_3_text, Sys1_4_text, Sys1_5_text, Sys2_1_text, Sys2_2_text, Sys2_3_text, Sys3_1_text, Sys3_2_text, Sys3_3_text, Sys3_4_text, Sys3_5_text, Sys3_6_text, Sys3_7_text, Sys3_8_text, Sys3_9_text, Sys4_1_text, Sys4_2_text, Sys4_3_text, Sys4_4_text, Sys4_5_text, Sys4_6_text As String
        Dim Sys1_1_Price, Sys1_2_Price, Sys1_3_Price, Sys1_4_Price, Sys1_5_Price, Sys2_1_Price, Sys2_2_Price, Sys2_3_Price, Sys3_1_Price, Sys3_2_Price, Sys3_3_Price, Sys3_4_Price, Sys3_5_Price, Sys3_6_Price, Sys3_7_Price, Sys3_8_Price, Sys3_9_Price, Sys4_1_Price, Sys4_2_Price, Sys4_3_Price, Sys4_4_Price, Sys4_5_Price, Sys4_6_Price As String
        Dim Sys3_1_Capacity, Sys3_2_Capacity, Sys3_3_Capacity, Sys3_4_Capacity, Sys3_5_Capacity, Sys3_6_Capacity, Sys3_7_Capacity, Sys3_8_Capacity, Sys3_9_Capacity As String

        Dim Sys1_1_Disc, Sys1_2_Disc, Sys1_3_Disc, Sys1_4_Disc, Sys1_5_Disc, Sys2_1_Disc, Sys2_2_Disc, Sys2_3_Disc, Sys3_1_Disc, Sys3_2_Disc, Sys3_3_Disc, Sys3_4_Disc, Sys3_5_Disc, Sys3_6_Disc, Sys3_7_Disc, Sys3_8_Disc, Sys3_9_Disc, Sys4_1_Disc, Sys4_2_Disc, Sys4_3_Disc, Sys4_4_Disc, Sys4_5_Disc, Sys4_6_Disc As String
        Dim Sys1_1_FinalRate, Sys1_2_FinalRate, Sys1_3_FinalRate, Sys1_4_FinalRate, Sys1_5_FinalRate, Sys2_FinalRate, Sys2_1_FinalRate, Sys2_2_FinalRate, Sys2_3_FinalRate, Sys3_1_FinalRate, Sys3_2_FinalRate, Sys3_3_FinalRate, Sys3_4_FinalRate, Sys3_5_FinalRate, Sys3_6_FinalRate, Sys3_7_FinalRate, Sys3_8_FinalRate, Sys3_9_FinalRate, Sys4_1_FinalRate, Sys4_2_FinalRate, Sys4_3_FinalRate, Sys4_4_FinalRate, Sys4_5_FinalRate, Sys4_6_FinalRate As String
        Dim Sys1_1_GST, Sys1_2_GST, Sys1_3_GST, Sys1_4_GST, Sys1_5_GST, Sys2_1_GST, Sys2_2_GST, Sys2_3_GST, Sys3_1_GST, Sys3_2_GST, Sys3_3_GST, Sys3_4_GST, Sys3_5_GST, Sys3_6_GST, Sys3_7_GST, Sys3_8_GST, Sys3_9_GST, Sys4_1_GST, Sys4_2_GST, Sys4_3_GST, Sys4_4_GST, Sys4_5_GST, Sys4_6_GST As String
        Dim Sys1_1_FinalAmount, Sys1_2_FinalAmount, Sys1_3_FinalAmount, Sys1_4_FinalAmount, Sys1_5_FinalAmount, Sys2_1_FinalAmount, Sys2_2_FinalAmount, Sys2_3_FinalAmount, Sys3_1_FinalAmount, Sys3_2_FinalAmount, Sys3_3_FinalAmount, Sys3_4_FinalAmount, Sys3_5_FinalAmount, Sys3_6_FinalAmount, Sys3_7_FinalAmount, Sys3_8_FinalAmount, Sys3_9_FinalAmount, Sys4_1_FinalAmount, Sys4_2_FinalAmount, Sys4_3_FinalAmount, Sys4_4_FinalAmount, Sys4_5_FinalAmount, Sys4_6_FinalAmount As String


        QPath = Class1.global.QPath
        If (Not System.IO.Directory.Exists(QPath + "\OrderTempFile")) Then
            System.IO.Directory.CreateDirectory(QPath + "\OrderTempFile")
        End If


        OrderTempPath = QPath + "\OrderTempFile"
        Dim dt1 As New DataTable("OrderPriceTbl")
        dt1.Columns.Add("Sys1_1")
        dt1.Columns.Add("Sys1_2")
        dt1.Columns.Add("Sys1_3")
        dt1.Columns.Add("Sys1_4")
        dt1.Columns.Add("Sys1_5")
        dt1.Columns.Add("Sys1_1_text")
        dt1.Columns.Add("Sys1_2_text")
        dt1.Columns.Add("Sys1_3_text")
        dt1.Columns.Add("Sys1_4_text")
        dt1.Columns.Add("Sys1_5_text")

        dt1.Columns.Add("Sys1_1_Price")
        dt1.Columns.Add("Sys1_2_Price")
        dt1.Columns.Add("Sys1_3_Price")
        dt1.Columns.Add("Sys1_4_Price")
        dt1.Columns.Add("Sys1_5_Price")

        dt1.Columns.Add("Sys1_1_Disc")
        dt1.Columns.Add("Sys1_2_Disc")
        dt1.Columns.Add("Sys1_3_Disc")
        dt1.Columns.Add("Sys1_4_Disc")
        dt1.Columns.Add("Sys1_5_Disc")

        dt1.Columns.Add("Sys1_1_FinalRate")
        dt1.Columns.Add("Sys1_2_FinalRate")
        dt1.Columns.Add("Sys1_3_FinalRate")
        dt1.Columns.Add("Sys1_4_FinalRate")
        dt1.Columns.Add("Sys1_5_FinalRate")

        dt1.Columns.Add("Sys1_1_GST")
        dt1.Columns.Add("Sys1_2_GST")
        dt1.Columns.Add("Sys1_3_GST")
        dt1.Columns.Add("Sys1_4_GST")
        dt1.Columns.Add("Sys1_5_GST")

        dt1.Columns.Add("Sys1_1_FinalAmount")
        dt1.Columns.Add("Sys1_2_FinalAmount")
        dt1.Columns.Add("Sys1_3_FinalAmount")
        dt1.Columns.Add("Sys1_4_FinalAmount")
        dt1.Columns.Add("Sys1_5_FinalAmount")



        dt1.Columns.Add("Sys2_1_text")
        dt1.Columns.Add("Sys2_1_Price")
        dt1.Columns.Add("Sys2_1_Disc")
        dt1.Columns.Add("Sys2_1_FinalRate")
        dt1.Columns.Add("Sys2_1_GST")
        dt1.Columns.Add("Sys2_1_FinalAmount")


        dt1.Columns.Add("Sys3_1")
        dt1.Columns.Add("Sys3_2")
        dt1.Columns.Add("Sys3_3")
        dt1.Columns.Add("Sys3_4")
        dt1.Columns.Add("Sys3_5")
        dt1.Columns.Add("Sys3_6")
        dt1.Columns.Add("Sys3_7")
        dt1.Columns.Add("Sys3_1_text")
        dt1.Columns.Add("Sys3_2_text")
        dt1.Columns.Add("Sys3_3_text")
        dt1.Columns.Add("Sys3_4_text")
        dt1.Columns.Add("Sys3_5_text")
        dt1.Columns.Add("Sys3_6_text")
        dt1.Columns.Add("Sys3_7_text")

        dt1.Columns.Add("Sys3_1_Capacity")
        dt1.Columns.Add("Sys3_2_Capacity")
        dt1.Columns.Add("Sys3_3_Capacity")
        dt1.Columns.Add("Sys3_4_Capacity")
        dt1.Columns.Add("Sys3_5_Capacity")
        dt1.Columns.Add("Sys3_6_Capacity")
        dt1.Columns.Add("Sys3_7_Capacity")

        dt1.Columns.Add("Sys3_1_Price")
        dt1.Columns.Add("Sys3_2_Price")
        dt1.Columns.Add("Sys3_3_Price")
        dt1.Columns.Add("Sys3_4_Price")
        dt1.Columns.Add("Sys3_5_Price")
        dt1.Columns.Add("Sys3_6_Price")
        dt1.Columns.Add("Sys3_7_Price")

        dt1.Columns.Add("Sys3_1_Disc")
        dt1.Columns.Add("Sys3_2_Disc")
        dt1.Columns.Add("Sys3_3_Disc")
        dt1.Columns.Add("Sys3_4_Disc")
        dt1.Columns.Add("Sys3_5_Disc")
        dt1.Columns.Add("Sys3_6_Disc")
        dt1.Columns.Add("Sys3_7_Disc")


        dt1.Columns.Add("Sys3_1_FinalRate")
        dt1.Columns.Add("Sys3_2_FinalRate")
        dt1.Columns.Add("Sys3_3_FinalRate")
        dt1.Columns.Add("Sys3_4_FinalRate")
        dt1.Columns.Add("Sys3_5_FinalRate")
        dt1.Columns.Add("Sys3_6_FinalRate")
        dt1.Columns.Add("Sys3_7_FinalRate")

        dt1.Columns.Add("Sys3_1_GST")
        dt1.Columns.Add("Sys3_2_GST")
        dt1.Columns.Add("Sys3_3_GST")
        dt1.Columns.Add("Sys3_4_GST")
        dt1.Columns.Add("Sys3_5_GST")
        dt1.Columns.Add("Sys3_6_GST")
        dt1.Columns.Add("Sys3_7_GST")

        dt1.Columns.Add("Sys3_1_FinalAmount")
        dt1.Columns.Add("Sys3_2_FinalAmount")
        dt1.Columns.Add("Sys3_3_FinalAmount")
        dt1.Columns.Add("Sys3_4_FinalAmount")
        dt1.Columns.Add("Sys3_5_FinalAmount")
        dt1.Columns.Add("Sys3_6_FinalAmount")
        dt1.Columns.Add("Sys3_7_FinalAmount")



        dt1.Columns.Add("Sys4_1")
        dt1.Columns.Add("Sys4_2")
        dt1.Columns.Add("Sys4_3")
        dt1.Columns.Add("Sys4_4")
        dt1.Columns.Add("Sys4_5")
        dt1.Columns.Add("Sys4_1_text")
        dt1.Columns.Add("Sys4_2_text")
        dt1.Columns.Add("Sys4_3_text")
        dt1.Columns.Add("Sys4_4_text")
        dt1.Columns.Add("Sys4_5_text")

        dt1.Columns.Add("Sys4_1_Price")
        dt1.Columns.Add("Sys4_2_Price")
        dt1.Columns.Add("Sys4_3_Price")
        dt1.Columns.Add("Sys4_4_Price")
        dt1.Columns.Add("Sys4_5_Price")

        dt1.Columns.Add("Sys4_1_Disc")
        dt1.Columns.Add("Sys4_2_Disc")
        dt1.Columns.Add("Sys4_3_Disc")
        dt1.Columns.Add("Sys4_4_Disc")
        dt1.Columns.Add("Sys4_5_Disc")

        dt1.Columns.Add("Sys4_1_FinalRate")
        dt1.Columns.Add("Sys4_2_FinalRate")
        dt1.Columns.Add("Sys4_3_FinalRate")
        dt1.Columns.Add("Sys4_4_FinalRate")
        dt1.Columns.Add("Sys4_5_FinalRate")

        dt1.Columns.Add("Sys4_1_GST")
        dt1.Columns.Add("Sys4_2_GST")
        dt1.Columns.Add("Sys4_3_GST")
        dt1.Columns.Add("Sys4_4_GST")
        dt1.Columns.Add("Sys4_5_GST")

        dt1.Columns.Add("Sys4_1_FinalAmount")
        dt1.Columns.Add("Sys4_2_FinalAmount")
        dt1.Columns.Add("Sys4_3_FinalAmount")
        dt1.Columns.Add("Sys4_4_FinalAmount")
        dt1.Columns.Add("Sys4_5_FinalAmount")

        dt1.Columns.Add("Sys1_Total")
        dt1.Columns.Add("Sys3_Total")
        dt1.Columns.Add("Sys4_Total")
        dt1.Columns.Add("GrossTotal")
        dt1.Columns.Add("Discount")
        dt1.Columns.Add("FinalTotal")
        dt1.Columns.Add("Capacity")
        dt1.Columns.Add("Discount_Text")
        'Total
        dt1.Columns.Add("TotalRate")
        dt1.Columns.Add("TotalDisc")
        dt1.Columns.Add("TotalFinalRate")
        dt1.Columns.Add("TotalGst")

        dt1.Columns.Add("Sys2_1")
        dt1.Columns.Add("Sys2_2")
        dt1.Columns.Add("Sys2_3")

        dt1.Columns.Add("Sys2_2_text")
        dt1.Columns.Add("Sys2_2_Price")
        dt1.Columns.Add("Sys2_2_Disc")
        dt1.Columns.Add("Sys2_2_FinalRate")
        dt1.Columns.Add("Sys2_2_GST")
        dt1.Columns.Add("Sys2_2_FinalAmount")

        dt1.Columns.Add("Sys2_3_text")
        dt1.Columns.Add("Sys2_3_Price")
        dt1.Columns.Add("Sys2_3_Disc")
        dt1.Columns.Add("Sys2_3_FinalRate")
        dt1.Columns.Add("Sys2_3_GST")
        dt1.Columns.Add("Sys2_3_FinalAmount")

        dt1.Columns.Add("Sys3_8")
        dt1.Columns.Add("Sys3_9")

        dt1.Columns.Add("Sys3_8_text")
        dt1.Columns.Add("Sys3_8_Price")
        dt1.Columns.Add("Sys3_8_Disc")
        dt1.Columns.Add("Sys3_8_FinalRate")
        dt1.Columns.Add("Sys3_8_GST")
        dt1.Columns.Add("Sys3_8_FinalAmount")

        dt1.Columns.Add("Sys3_9_text")
        dt1.Columns.Add("Sys3_9_Price")
        dt1.Columns.Add("Sys3_9_Disc")
        dt1.Columns.Add("Sys3_9_FinalRate")
        dt1.Columns.Add("Sys3_9_GST")
        dt1.Columns.Add("Sys3_9_FinalAmount")

        dt1.Columns.Add("Sys4_6")

        dt1.Columns.Add("Sys4_6_text")
        dt1.Columns.Add("Sys4_6_Price")
        dt1.Columns.Add("Sys4_6_Disc")
        dt1.Columns.Add("Sys4_6_FinalRate")
        dt1.Columns.Add("Sys4_6_GST")
        dt1.Columns.Add("Sys4_6_FinalAmount")


        dt1.Columns.Add("Sys2_Total")

        dt1.Columns.Add("Sys3_8_Capacity")
        dt1.Columns.Add("Sys3_9_Capacity")
        'System-1 Price  


        Dim CountSrNo As Integer
        Dim Discount_Text = ""
        CountSrNo = 0
        For index = 0 To GvSystem1.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvSystem1.Rows(index).Cells(1).Value)
            If IsTicked Then
                If CountSrNo = 0 Then
                    Sys1_1 = "1.1"
                    Sys1_1_text = GvSystem1.Rows(index).Cells("ItemName").Value
                    Sys1_1_Price = GvSystem1.Rows(index).Cells("Price").Value
                    Sys1_1_Disc = GvSystem1.Rows(index).Cells("Disc").Value
                    Sys1_1_FinalRate = GvSystem1.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys1_1_GST = GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys1_1_GST = GvSystem1.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys1_1_FinalAmount = GvSystem1.Rows(index).Cells("FinalAmount").Value


                End If
                If CountSrNo = 1 Then
                    Sys1_2 = "1.2"
                    Sys1_2_text = GvSystem1.Rows(index).Cells("ItemName").Value
                    Sys1_2_Price = GvSystem1.Rows(index).Cells("Price").Value
                    Sys1_2_Disc = GvSystem1.Rows(index).Cells("Disc").Value
                    Sys1_2_FinalRate = GvSystem1.Rows(index).Cells("FinalRate").Value

                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys1_2_GST = GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys1_2_GST = GvSystem1.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys1_2_FinalAmount = GvSystem1.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 2 Then
                    Sys1_3 = "1.3"
                    Sys1_3_text = GvSystem1.Rows(index).Cells("ItemName").Value
                    Sys1_3_Price = GvSystem1.Rows(index).Cells("Price").Value
                    Sys1_3_Disc = GvSystem1.Rows(index).Cells("Disc").Value
                    Sys1_3_FinalRate = GvSystem1.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys1_3_GST = GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys1_3_GST = GvSystem1.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys1_3_FinalAmount = GvSystem1.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 3 Then
                    Sys1_4 = "1.4"
                    Sys1_4_text = GvSystem1.Rows(index).Cells("ItemName").Value
                    Sys1_4_Price = GvSystem1.Rows(index).Cells("Price").Value
                    Sys1_4_Disc = GvSystem1.Rows(index).Cells("Disc").Value
                    Sys1_4_FinalRate = GvSystem1.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys1_4_GST = GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys1_4_GST = GvSystem1.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys1_4_FinalAmount = GvSystem1.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 4 Then
                    Sys1_5 = "1.5"
                    Sys1_5_text = GvSystem1.Rows(index).Cells("ItemName").Value
                    Sys1_5_Price = GvSystem1.Rows(index).Cells("Price").Value
                    Sys1_5_Disc = GvSystem1.Rows(index).Cells("Disc").Value
                    Sys1_5_FinalRate = GvSystem1.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys1_5_GST = GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys1_5_GST = GvSystem1.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem1.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys1_5_FinalAmount = GvSystem1.Rows(index).Cells("FinalAmount").Value

                End If
                CountSrNo = CountSrNo + 1
            End If
        Next

        'System-2 Price 
        CountSrNo = 0
        For index = 0 To GvSystem2.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvSystem2.Rows(index).Cells(1).Value)
            If IsTicked Then
                If CountSrNo = 0 Then
                    Sys2_1 = "2.1"
                    Sys2_1_text = GvSystem2.Rows(index).Cells("ItemName").Value.ToString()
                    Sys2_1_Price = GvSystem2.Rows(index).Cells("Price").Value.ToString()
                    Sys2_1_Disc = GvSystem2.Rows(index).Cells("Disc").Value.ToString()
                    Sys2_1_FinalRate = GvSystem2.Rows(index).Cells("FinalRate").Value.ToString()
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys2_1_GST = GvSystem2.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys2_1_GST = GvSystem2.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem2.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If


                    Sys2_1_FinalAmount = GvSystem2.Rows(index).Cells("FinalAmount").Value.ToString()
                End If
                If CountSrNo = 1 Then
                    Sys2_2 = "2.2"
                    Sys2_2_text = GvSystem2.Rows(index).Cells("ItemName").Value.ToString()
                    Sys2_2_Price = GvSystem2.Rows(index).Cells("Price").Value.ToString()
                    Sys2_2_Disc = GvSystem2.Rows(index).Cells("Disc").Value.ToString()
                    Sys2_2_FinalRate = GvSystem2.Rows(index).Cells("FinalRate").Value.ToString()
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys2_2_GST = GvSystem2.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys2_2_GST = GvSystem2.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem2.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys2_2_FinalAmount = GvSystem2.Rows(index).Cells("FinalAmount").Value.ToString()
                End If
                If CountSrNo = 2 Then
                    Sys2_3 = "2.3"
                    Sys2_3_text = GvSystem2.Rows(index).Cells("ItemName").Value.ToString()
                    Sys2_3_Price = GvSystem2.Rows(index).Cells("Price").Value.ToString()
                    Sys2_3_Disc = GvSystem2.Rows(index).Cells("Disc").Value.ToString()
                    Sys2_3_FinalRate = GvSystem2.Rows(index).Cells("FinalRate").Value.ToString()
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys2_3_GST = GvSystem2.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys2_3_GST = GvSystem2.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem2.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys2_3_FinalAmount = GvSystem2.Rows(index).Cells("FinalAmount").Value.ToString()
                End If
            End If
            CountSrNo = CountSrNo + 1

        Next

        'System-3 Price  
        CountSrNo = 0
        For index = 0 To GvSystem3.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvSystem3.Rows(index).Cells(1).Value)

            If IsTicked Then
                If CountSrNo = 0 Then
                    Sys3_1 = "3.1"
                    Sys3_1_text = GvSystem3.Rows(index).Cells("ItemName").Value
                    Sys3_1_Capacity = GvSystem3.Rows(index).Cells("Capacity").Value
                    Sys3_1_Price = GvSystem3.Rows(index).Cells("Price").Value
                    Sys3_1_Disc = GvSystem3.Rows(index).Cells("Disc").Value
                    Sys3_1_FinalRate = GvSystem3.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys3_1_GST = GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys3_1_GST = GvSystem3.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If

                    Sys3_1_FinalAmount = GvSystem3.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 1 Then
                    Sys3_2 = "3.2"
                    Sys3_2_text = GvSystem3.Rows(index).Cells("ItemName").Value
                    Sys3_2_Capacity = GvSystem3.Rows(index).Cells("Capacity").Value
                    Sys3_2_Price = GvSystem3.Rows(index).Cells("Price").Value
                    Sys3_2_Disc = GvSystem3.Rows(index).Cells("Disc").Value
                    Sys3_2_FinalRate = GvSystem3.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys3_2_GST = GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys3_2_GST = GvSystem3.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys3_2_FinalAmount = GvSystem3.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 2 Then
                    Sys3_3 = "3.3"
                    Sys3_3_text = GvSystem3.Rows(index).Cells("ItemName").Value
                    Sys3_3_Capacity = GvSystem3.Rows(index).Cells("Capacity").Value
                    Sys3_3_Price = GvSystem3.Rows(index).Cells("Price").Value
                    Sys3_3_Disc = GvSystem3.Rows(index).Cells("Disc").Value
                    Sys3_3_FinalRate = GvSystem3.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys3_3_GST = GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys3_3_GST = GvSystem3.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys3_3_FinalAmount = GvSystem3.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 3 Then
                    Sys3_4 = "3.4"
                    Sys3_4_text = GvSystem3.Rows(index).Cells("ItemName").Value
                    Sys3_4_Capacity = GvSystem3.Rows(index).Cells("Capacity").Value
                    Sys3_4_Price = GvSystem3.Rows(index).Cells("Price").Value
                    Sys3_4_Disc = GvSystem3.Rows(index).Cells("Disc").Value
                    Sys3_4_FinalRate = GvSystem3.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys3_4_GST = GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys3_4_GST = GvSystem3.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys3_4_FinalAmount = GvSystem3.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 4 Then
                    Sys3_5 = "3.5"
                    Sys3_5_text = GvSystem3.Rows(index).Cells("ItemName").Value
                    Sys3_5_Capacity = GvSystem3.Rows(index).Cells("Capacity").Value
                    Sys3_5_Price = GvSystem3.Rows(index).Cells("Price").Value
                    Sys3_5_Disc = GvSystem3.Rows(index).Cells("Disc").Value
                    Sys3_5_FinalRate = GvSystem3.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys3_5_GST = GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys3_5_GST = GvSystem3.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys3_5_FinalAmount = GvSystem3.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 5 Then
                    Sys3_6 = "3.6"
                    Sys3_6_text = GvSystem3.Rows(index).Cells("ItemName").Value
                    Sys3_6_Capacity = GvSystem3.Rows(index).Cells("Capacity").Value
                    Sys3_6_Price = GvSystem3.Rows(index).Cells("Price").Value
                    Sys3_6_Disc = GvSystem3.Rows(index).Cells("Disc").Value
                    Sys3_6_FinalRate = GvSystem3.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys3_6_GST = GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys3_6_GST = GvSystem3.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys3_6_FinalAmount = GvSystem3.Rows(index).Cells("FinalAmount").Value

                End If
                If index = 6 Then
                    Sys3_7 = "3.7"
                    Sys3_7_text = GvSystem3.Rows(index).Cells("ItemName").Value
                    Sys3_7_Capacity = GvSystem3.Rows(index).Cells("Capacity").Value
                    Sys3_7_Price = GvSystem3.Rows(index).Cells("Price").Value
                    Sys3_7_Disc = GvSystem3.Rows(index).Cells("Disc").Value
                    Sys3_7_FinalRate = GvSystem3.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys3_7_GST = GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys3_7_GST = GvSystem3.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys3_7_FinalAmount = GvSystem3.Rows(index).Cells("FinalAmount").Value

                End If
                If index = 7 Then
                    Sys3_8 = "3.8"
                    Sys3_8_text = GvSystem3.Rows(index).Cells("ItemName").Value
                    Sys3_8_Capacity = GvSystem3.Rows(index).Cells("Capacity").Value
                    Sys3_8_Price = GvSystem3.Rows(index).Cells("Price").Value
                    Sys3_8_Disc = GvSystem3.Rows(index).Cells("Disc").Value
                    Sys3_8_FinalRate = GvSystem3.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys3_8_GST = GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys3_8_GST = GvSystem3.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys3_8_FinalAmount = GvSystem3.Rows(index).Cells("FinalAmount").Value

                End If
                If index = 8 Then
                    Sys3_9 = "3.9"
                    Sys3_9_text = GvSystem3.Rows(index).Cells("ItemName").Value
                    Sys3_9_Capacity = GvSystem3.Rows(index).Cells("Capacity").Value
                    Sys3_9_Price = GvSystem3.Rows(index).Cells("Price").Value
                    Sys3_9_Disc = GvSystem3.Rows(index).Cells("Disc").Value
                    Sys3_9_FinalRate = GvSystem3.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys3_9_GST = GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys3_9_GST = GvSystem3.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem3.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys3_9_FinalAmount = GvSystem3.Rows(index).Cells("FinalAmount").Value

                End If
                CountSrNo = CountSrNo + 1

            End If

        Next

        'System-4 Price 

        CountSrNo = 0

        For index = 0 To GvSystem4.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvSystem4.Rows(index).Cells(1).Value)

            If IsTicked Then

                If CountSrNo = 0 Then
                    Sys4_1 = "4.1"
                    Sys4_1_text = GvSystem4.Rows(index).Cells("ItemName").Value
                    Sys4_1_Price = GvSystem4.Rows(index).Cells("Price").Value
                    Sys4_1_Disc = GvSystem4.Rows(index).Cells("Disc").Value
                    Sys4_1_FinalRate = GvSystem4.Rows(index).Cells("FinalRate").Value

                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys4_1_GST = GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys4_1_GST = GvSystem4.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If

                    Sys4_1_FinalAmount = GvSystem4.Rows(index).Cells("FinalAmount").Value

                End If

                If CountSrNo = 1 Then
                    Sys4_2 = "4.2"
                    Sys4_2_text = GvSystem4.Rows(index).Cells("ItemName").Value
                    Sys4_2_Price = GvSystem4.Rows(index).Cells("Price").Value
                    Sys4_2_Disc = GvSystem4.Rows(index).Cells("Disc").Value
                    Sys4_2_FinalRate = GvSystem4.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys4_2_GST = GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys4_2_GST = GvSystem4.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys4_2_FinalAmount = GvSystem4.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 2 Then
                    Sys4_3 = "4.3"
                    Sys4_3_text = GvSystem4.Rows(index).Cells("ItemName").Value
                    Sys4_3_Price = GvSystem4.Rows(index).Cells("Price").Value
                    Sys4_3_Disc = GvSystem4.Rows(index).Cells("Disc").Value
                    Sys4_3_FinalRate = GvSystem4.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys4_3_GST = GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys4_3_GST = GvSystem4.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys4_3_FinalAmount = GvSystem4.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 3 Then
                    Sys4_4 = "4.4"
                    Sys4_4_text = GvSystem4.Rows(index).Cells("ItemName").Value
                    Sys4_4_Price = GvSystem4.Rows(index).Cells("Price").Value
                    Sys4_4_Disc = GvSystem4.Rows(index).Cells("Disc").Value
                    Sys4_4_FinalRate = GvSystem4.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys4_4_GST = GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys4_4_GST = GvSystem4.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys4_4_FinalAmount = GvSystem4.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 4 Then
                    Sys4_5 = "4.5"
                    Sys4_5_text = GvSystem4.Rows(index).Cells("ItemName").Value
                    Sys4_5_Price = GvSystem4.Rows(index).Cells("Price").Value
                    Sys4_5_Disc = GvSystem4.Rows(index).Cells("Disc").Value
                    Sys4_5_FinalRate = GvSystem4.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys4_5_GST = GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys4_5_GST = GvSystem4.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys4_5_FinalAmount = GvSystem4.Rows(index).Cells("FinalAmount").Value

                End If
                If CountSrNo = 5 Then
                    Sys4_6 = "4.6"
                    Sys4_6_text = GvSystem4.Rows(index).Cells("ItemName").Value
                    Sys4_6_Price = GvSystem4.Rows(index).Cells("Price").Value
                    Sys4_6_Disc = GvSystem4.Rows(index).Cells("Disc").Value
                    Sys4_6_FinalRate = GvSystem4.Rows(index).Cells("FinalRate").Value
                    If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                        Sys4_6_GST = GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + " %"
                    Else
                        Sys4_6_GST = GvSystem4.Rows(index).Cells("GSTAmt").Value + " (" + GvSystem4.Rows(index).Cells("GST").Value.ToString().Replace(".00", "").Trim() + ") %"
                    End If
                    Sys4_6_FinalAmount = GvSystem4.Rows(index).Cells("FinalAmount").Value

                End If
                CountSrNo = CountSrNo + 1

            End If


        Next


        'Total All Rate,Disc,FinalRate,GST Calculation




        Dim Discout_Amount_Print As String

        Discout_Amount_Print = ""


        If txtPT_TotalDiscount.Text.Trim() <> "0" Then
            Discount_Text = "SPECIAL DISCOUNT ==>"
            Discount = Convert.ToDecimal(txtPT_TotalDiscount.Text)
            Discout_Amount_Print = Discount.ToString()

        Else
            Discount_Text = ""
            Discout_Amount_Print = ""

        End If

        dt1.Rows.Add(Sys1_1, Sys1_2, Sys1_3, Sys1_4, Sys1_5,
                     Sys1_1_text, Sys1_2_text, Sys1_3_text, Sys1_4_text, Sys1_5_text,
                     Sys1_1_Price, Sys1_2_Price, Sys1_3_Price, Sys1_4_Price, Sys1_5_Price,
                     Sys1_1_Disc, Sys1_2_Disc, Sys1_3_Disc, Sys1_4_Disc, Sys1_5_Disc,
                     Sys1_1_FinalRate, Sys1_2_FinalRate, Sys1_3_FinalRate, Sys1_4_FinalRate, Sys1_5_FinalRate,
                     Sys1_1_GST, Sys1_2_GST, Sys1_3_GST, Sys1_4_GST, Sys1_5_GST, Sys1_1_FinalAmount, Sys1_2_FinalAmount, Sys1_3_FinalAmount, Sys1_4_FinalAmount, Sys1_5_FinalAmount,
                     Convert.ToString(Sys2_1_text), Convert.ToString(Sys2_1_Price), Convert.ToString(Sys2_1_Disc), Convert.ToString(Sys2_1_FinalRate), Convert.ToString(Sys2_1_GST), Convert.ToString(Sys2_1_FinalAmount),
                     Sys3_1, Sys3_2, Sys3_3, Sys3_4, Sys3_5, Sys3_6, Sys3_7,
                     Sys3_1_text, Sys3_2_text, Sys3_3_text, Sys3_4_text, Sys3_5_text, Sys3_6_text, Sys3_7_text,
                     Sys3_1_Capacity, Sys3_2_Capacity, Sys3_3_Capacity, Sys3_4_Capacity, Sys3_5_Capacity, Sys3_6_Capacity, Sys3_7_Capacity,
                     Sys3_1_Price, Sys3_2_Price, Sys3_3_Price, Sys3_4_Price, Sys3_5_Price, Sys3_6_Price, Sys3_7_Price,
                     Sys3_1_Disc, Sys3_2_Disc, Sys3_3_Disc, Sys3_4_Disc, Sys3_5_Disc, Sys3_6_Disc, Sys3_7_Disc,
                        Sys3_1_FinalRate, Sys3_2_FinalRate, Sys3_3_FinalRate, Sys3_4_FinalRate, Sys3_5_FinalRate, Sys3_6_FinalRate, Sys3_7_FinalRate,
                        Sys3_1_GST, Sys3_2_GST, Sys3_3_GST, Sys3_4_GST, Sys3_5_GST, Sys3_6_GST, Sys3_7_GST,
                        Sys3_1_FinalAmount, Sys3_2_FinalAmount, Sys3_3_FinalAmount, Sys3_4_FinalAmount, Sys3_5_FinalAmount, Sys3_6_FinalAmount, Sys3_7_FinalAmount,
                        Sys4_1, Sys4_2, Sys4_3, Sys4_4, Sys4_5,
                        Sys4_1_text, Sys4_2_text, Sys4_3_text, Sys4_4_text, Sys4_5_text,
                        Sys4_1_Price, Sys4_2_Price, Sys4_3_Price, Sys4_4_Price, Sys4_5_Price,
                        Sys4_1_Disc, Sys4_2_Disc, Sys4_3_Disc, Sys4_4_Disc, Sys4_5_Disc,
                        Sys4_1_FinalRate, Sys4_2_FinalRate, Sys4_3_FinalRate, Sys4_4_FinalRate, Sys4_5_FinalRate,
                        Sys4_1_GST, Sys4_2_GST, Sys4_3_GST, Sys4_4_GST, Sys4_5_GST,
                        Sys4_1_FinalAmount, Sys4_2_FinalAmount, Sys4_3_FinalAmount, Sys4_4_FinalAmount, Sys4_5_FinalAmount,
                        Sys1_Total.ToString(), Sys3_Total.ToString(), Sys4_Total.ToString(), Convert.ToString(GrossAmount),
                         Discout_Amount_Print, Convert.ToString(FinalAmount1), txtPDFCapacity.Text, Discount_Text, TotalRate.ToString(), TotalDisc.ToString(), TotalFinalRate.ToString(), TotalGST.ToString(),
                         Sys2_1, Sys2_2, Sys2_3,
                         Sys2_2_text, Sys2_2_Price, Sys2_2_Disc, Sys2_2_FinalRate, Sys2_2_GST, Sys2_2_FinalAmount,
                         Sys2_3_text, Sys2_3_Price, Sys2_3_Disc, Sys2_3_FinalRate, Sys2_3_GST, Sys2_3_FinalAmount,
                         Sys3_8, Sys3_9,
                        Sys3_8_text, Sys3_8_Price, Sys3_8_Disc, Sys3_8_FinalRate, Sys3_8_GST, Sys3_8_FinalAmount,
                        Sys3_9_text, Sys3_9_Price, Sys3_9_Disc, Sys3_9_FinalRate, Sys3_9_GST, Sys3_9_FinalAmount,
                        Sys4_6, Sys4_6_text, Sys4_6_Price, Sys4_6_Disc, Sys4_6_FinalRate, Sys4_6_GST, Sys4_6_FinalAmount,
                        Sys2_Total,
                        Sys3_8_Capacity, Sys3_9_Capacity)

        OrderPriceDS.Tables.Add(dt1)
        Class1.WriteXMlFile(OrderPriceDS, "OrderAgreementPrice", "OrderPriceTbl")

        If rblWithoutDisc.Checked Then
            Dim rpt1 As New rpt_OrderAgreementPriceWithoutDisc
            rpt1.Database.Tables(0).SetDataSource(OrderPriceDS.Tables("OrderPriceTbl"))
            rpt1.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\5.pdf")
            rpt1.Dispose()
        ElseIf rblDetail.Checked Then
            Dim rpt1 As New rpt_OrderAgreementPrice
            rpt1.Database.Tables(0).SetDataSource(OrderPriceDS.Tables("OrderPriceTbl"))
            rpt1.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\5.pdf")
            rpt1.Dispose()
        ElseIf rblStandardDisc.Checked Then
            Dim rpt1 As New rpt_OrderAgreementPriceStandardWithout
            rpt1.Database.Tables(0).SetDataSource(OrderPriceDS.Tables("OrderPriceTbl"))
            rpt1.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\5.pdf")
            rpt1.Dispose()
        Else
            Dim rpt1 As New rpt_OrderAgreementPriceStandard
            rpt1.Database.Tables(0).SetDataSource(OrderPriceDS.Tables("OrderPriceTbl"))
            rpt1.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\5.pdf")
            rpt1.Dispose()


        End If


    End Sub


    Public Sub Price_Total_Calculation()

        Dim FinalAmount_Special_Price As Decimal

        Sys1_Total = 0
        Sys2_Total = 0
        Sys3_Total = 0
        Sys4_Total = 0
        GrossAmount = 0
        FinalAmount1 = 0
        FinalAmount_Special_Price = 0
        TotalRate = 0
        TotalDisc = 0
        TotalFinalRate = 0
        TotalGST = 0

        'dt1.Columns.Add("Price")
        'dt1.Columns.Add("Disc")
        'dt1.Columns.Add("FinalRate")
        'dt1.Columns.Add("GST")
        'dt1.Columns.Add("GSTAmt")

        For i As Integer = 0 To GvSystem1.RowCount - 1
            Dim IsTicked As Boolean = CBool(GvSystem1.Rows(i).Cells(1).Value)
            If IsTicked Then
                TotalRate = TotalRate + GvSystem1.Rows(i).Cells("Price").Value()
                TotalDisc = TotalDisc + GvSystem1.Rows(i).Cells("Disc").Value()
                TotalFinalRate = TotalFinalRate + GvSystem1.Rows(i).Cells("FinalRate").Value()
                TotalGST = TotalGST + GvSystem1.Rows(i).Cells("GSTAmt").Value()

                If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                    Sys1_Total = Sys1_Total + GvSystem1.Rows(i).Cells("Price").Value()

                Else
                    Sys1_Total = Sys1_Total + GvSystem1.Rows(i).Cells("FinalAmount").Value()
                End If

            End If

        Next
        For i As Integer = 0 To GvSystem2.RowCount - 1
            Dim IsTicked As Boolean = CBool(GvSystem2.Rows(i).Cells(1).Value)
            If IsTicked Then
                TotalRate = TotalRate + GvSystem2.Rows(i).Cells("Price").Value()
                TotalDisc = TotalDisc + GvSystem2.Rows(i).Cells("Disc").Value()
                TotalFinalRate = TotalFinalRate + GvSystem2.Rows(i).Cells("FinalRate").Value()
                TotalGST = TotalGST + GvSystem2.Rows(i).Cells("GSTAmt").Value()

                If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                    Sys2_Total = Sys2_Total + GvSystem2.Rows(i).Cells("Price").Value()

                Else
                    Sys2_Total = Sys2_Total + GvSystem2.Rows(i).Cells("FinalAmount").Value()
                End If

            End If

        Next
        For i As Integer = 0 To GvSystem3.RowCount - 1
            Dim IsTicked As Boolean = CBool(GvSystem3.Rows(i).Cells(1).Value)
            If IsTicked Then
                TotalRate = TotalRate + GvSystem3.Rows(i).Cells("Price").Value()
                TotalDisc = TotalDisc + GvSystem3.Rows(i).Cells("Disc").Value()
                TotalFinalRate = TotalFinalRate + GvSystem3.Rows(i).Cells("FinalRate").Value()
                TotalGST = TotalGST + GvSystem3.Rows(i).Cells("GSTAmt").Value()

                If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                    Sys3_Total = Sys3_Total + GvSystem3.Rows(i).Cells("Price").Value()

                Else
                    Sys3_Total = Sys3_Total + GvSystem3.Rows(i).Cells("FinalAmount").Value()
                End If

            End If

        Next
        For i As Integer = 0 To GvSystem4.RowCount - 1
            Dim IsTicked As Boolean = CBool(GvSystem4.Rows(i).Cells(1).Value)
            If IsTicked Then
                TotalRate = TotalRate + GvSystem4.Rows(i).Cells("Price").Value()
                TotalDisc = TotalDisc + GvSystem4.Rows(i).Cells("Disc").Value()
                TotalFinalRate = TotalFinalRate + GvSystem4.Rows(i).Cells("FinalRate").Value()
                TotalGST = TotalGST + GvSystem4.Rows(i).Cells("GSTAmt").Value()

                If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
                    Sys4_Total = Sys4_Total + GvSystem4.Rows(i).Cells("Price").Value()

                Else
                    Sys4_Total = Sys4_Total + GvSystem4.Rows(i).Cells("FinalAmount").Value()
                End If

            End If
        Next


        GrossAmount = Sys1_Total + Sys2_Total + Sys3_Total + Sys4_Total
        FinalAmount1 = GrossAmount
        txtPT_TotalRate.Text = TotalRate.ToString("N2")
        txtPT_TotalDiscount.Text = TotalDisc.ToString("N2")
        txtPT_TotalFinalRate.Text = TotalFinalRate.ToString("N2")
        txtPT_TotalGST.Text = TotalGST.ToString("N2")
        txtPT_TotalFinalAmount.Text = FinalAmount1.ToString("N2")
    End Sub

    Public Shared Function imgToByteConverter(ByVal inImg As Image) As Byte()
        Dim imgCon As New ImageConverter()
        Return DirectCast(imgCon.ConvertTo(inImg, GetType(Byte())), Byte())
    End Function
    Public Sub Get_PDf_From_DBPath(ByVal Order_Category_Master_ID As String, ByVal DestinationPath As String)

        Dim ds As New DataSet()
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SP_Get_Order_Category_Detail_Master_ID"
        cmd.Connection = linq_obj.Connection
        cmd.Parameters.Add("@Fk_Order_Category_ID", SqlDbType.VarChar).Value = Order_Category_Master_ID
        Dim da As New SqlDataAdapter()
        da.SelectCommand = cmd
        da.Fill(ds)
        If (File.Exists(ds.Tables(0).Rows(0)("Item_Photo"))) Then
            Dim dest As String = Path.Combine(DestinationPath, Path.GetFileName(ds.Tables(0).Rows(0)("Item_Photo")))
            'File.Copy(ds.Tables(0).Rows(0)("Item_Photo").Replace("D:", "\\192.168.1.102"), dest, True)
            File.Copy(ds.Tables(0).Rows(0)("Item_Photo"), dest, True)
        End If

    End Sub
    Public Sub Get_Complimentry(ByVal SourcePath As String, ByVal DestinationPath As String)

        If (File.Exists(SourcePath)) Then
            Dim dest As String = Path.Combine(DestinationPath, SourcePath)
            'File.Copy(ds.Tables(0).Rows(0)("Item_Photo").Replace("D:", "\\192.168.1.102"), dest, True)
            File.Copy(SourcePath, dest, True)
        End If

    End Sub
    Public Sub Generate_Payment_Terms()

        QPath = Class1.global.QPath
        If (Not System.IO.Directory.Exists(QPath + "\OrderTempFile")) Then
            System.IO.Directory.CreateDirectory(QPath + "\OrderTempFile")
        End If
        OrderTempPath = QPath + "\OrderTempFile"
        Dim OrderPriceDS As New DataSet()
        Dim dt = New DataTable("OrderPage6")
        dt.Columns.Add("P_FinalRate")
        dt.Columns.Add("P_GST")
        dt.Columns.Add("P_FinalAmount")
        dt.Columns.Add("P_FirstAdvance")
        dt.Columns.Add("P_SecondAdvance")
        dt.Columns.Add("P_FinalPayment")
        dt.Columns.Add("P_Receive")
        dt.Columns.Add("P_TentativeDt")
        dt.Columns.Add("P_FinalDt")
        dt.Columns.Add("T_PaymentTerms")
        dt.Columns.Add("T_Gov_Tax")
        dt.Columns.Add("T_Delivery")
        dt.Columns.Add("T_Transport1")
        dt.Columns.Add("T_Transport2")
        dt.Columns.Add("T_Transport3")
        dt.Columns.Add("T_Transport4")
        dt.Columns.Add("T_ErrectionComm1")
        dt.Columns.Add("T_ErrectionComm2")
        dt.Columns.Add("T_Gaurantee1")
        dt.Columns.Add("T_Gaurantee2")
        dt.Columns.Add("T_Terms1")
        dt.Columns.Add("T_Terms2")
        dt.Columns.Add("T_Terms3")
        dt.Columns.Add("T_Terms4")
        dt.Columns.Add("T_Terms5")
        dt.Columns.Add("CompanyName")

        If txtPT_FirstAdvance.Text = "0.00" Then
            txtPT_FirstAdvance.Text = ""
        End If
        If txtPT_SecondAdvance.Text = "0.00" Then
            txtPT_SecondAdvance.Text = ""

        End If

        If txtPT_FinalPayment.Text = "0.00" Then
            txtPT_FinalPayment.Text = ""
        End If

        If txtPT_PaymentReceive.Text = "0.00" Then
            txtPT_PaymentReceive.Text = ""
        End If


        If txtPT_TentaDT.Text.Contains("1900") Then
            txtPT_TentaDT.Text = " "

        End If
        If txtPT_DispDT.Text.Contains("1900") Then
            txtPT_DispDT.Text = " "

        End If
        Dim CompanyName As String

        If chkCompany.Checked = True Then
            CompanyName = "M/s.                                                              ."
        Else
            CompanyName = txtCompanyName.Text
        End If

        Dim GST As String


        If rblStandard.Checked = True Or rblStandardDisc.Checked = True Then
            GST = "extra at actual"
            txtPT_TotalFinalAmount.Text = txtPT_TotalFinalRate.Text

        Else
            GST = txtPT_TotalGST.Text.Trim()

        End If


        dt.Rows.Add(txtPT_TotalFinalRate.Text.Trim(), GST, txtPT_TotalFinalAmount.Text.Trim(), txtPT_FirstAdvance.Text.Trim(), txtPT_SecondAdvance.Text.Trim(), txtPT_FinalPayment.Text.Trim(), txtPT_PaymentReceive.Text.Trim(), txtPT_TentaDT.Text.Trim(), txtPT_DispDT.Text.Trim(),
                    txtPT_Terms.Text, txtPT_Gov.Text, txtPT_Deliver.Text, txtPT_Transp1.Text, txtPT_Transp2.Text, txtPT_Transp3.Text, txtPT_Transp4.Text, txtPT_Errection1.Text.Trim(), txtPT_Errection2.Text.Trim(), txtPT_Gaurantee1.Text.Trim(), txtPT_Gaurantee2.Text.Trim(), txtPT_Terms1.Text.Trim(), txtPT_Terms2.Text.Trim(), txtPT_Terms3.Text.Trim(), txtPT_Terms4.Text.Trim(), txtPT_Terms5.Text.Trim(), CompanyName)

        OrderPriceDS.Tables.Add(dt)

        Dim rpt4 As New rpt_OrderAgreementPaymentTerms_New
        Class1.WriteXMlFile(OrderPriceDS, "Generate_Payment_Terms", "OrderPage6")
        rpt4.Database.Tables(0).SetDataSource(OrderPriceDS.Tables("OrderPage6"))
        rpt4.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\6.pdf")
        rpt4.Dispose()



    End Sub


    Public Sub Generate_Order_Acceptance()

        QPath = Class1.global.QPath
        If (Not System.IO.Directory.Exists(QPath + "\OrderTempFile")) Then
            System.IO.Directory.CreateDirectory(QPath + "\OrderTempFile")
        End If
        OrderTempPath = QPath + "\OrderTempFile"
        Dim OrderPriceDS As New DataSet()
        Dim OrderPriceDSTemp As New DataSet()
        Dim dt = New DataTable("OrderAcceptance")
        dt.Columns.Add("Remarks1")
        dt.Columns.Add("Remarks2")
        dt.Columns.Add("Remarks3")
        dt.Columns.Add("Remarks4")
        dt.Columns.Add("Remarks5")

        dt.Columns.Add("I_Photo1")
        dt.Columns.Add("I_Name1")
        dt.Columns.Add("I_Photo2")
        dt.Columns.Add("I_Name2")
        dt.Columns.Add("I_Photo3", GetType(Byte()))
        dt.Columns.Add("I_Name3")

        dt.Columns.Add("C_Photo1", GetType(Byte()))
        dt.Columns.Add("C_Name1")
        dt.Columns.Add("C_Photo2", GetType(Byte()))
        dt.Columns.Add("C_Name2")
        dt.Columns.Add("C_Photo3", GetType(Byte()))
        dt.Columns.Add("C_Name3")
        dt.Columns.Add("C_CompanyName")
        dt.Columns.Add("C_Designation")

        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SP_Get_OrderAgreement_By_EnqNo"
        cmd.Connection = linq_obj.Connection
        cmd.Parameters.Add("@EnqNo", SqlDbType.VarChar).Value = txtEnqNo.Text
        Dim da As New SqlDataAdapter()
        da.SelectCommand = cmd
        da.Fill(OrderPriceDSTemp, "OrderPriceDSTemp")


        Dim msExecutive As New MemoryStream
        Dim bitsExecutive As Byte()

        If Not PictProfile12.Image Is Nothing Then
            PictProfile12.Image.Save(msExecutive, PictProfile12.Image.RawFormat)
            bitsExecutive = msExecutive.GetBuffer()
        End If
        Dim appPath As String

        appPath = Path.GetDirectoryName(Application.ExecutablePath)
        profilephoto1 = "D:\ROERP\image\ProfilePhoto\Profile1.jpg"
        profilephoto2 = "D:\ROERP\image\ProfilePhoto\Profile2.jpg"

        'Client Path

        profilephoto1 = profilephoto1.Replace("D:", "\\192.168.1.102")
        profilephoto2 = profilephoto2.Replace("D:", "\\192.168.1.102")


        Dim companyName As String
        companyName = ""
        If chkCompany.Checked = True Then
            companyName = "M/s.                                                              ."
        Else
            companyName = txtCompanyName.Text.Trim()
        End If

        dt.Rows.Add(txtRemarks1.Text,
                    txtRemarks2.Text,
                    txtRemarks3.Text,
                    txtRemarks4.Text,
                    txtRemarks5.Text,
                    profilephoto1,
                    txtCName1.Text.Trim(),
                    profilephoto2.ToString(),
                    txtCName2.Text.Trim(),
                    bitsExecutive,
                    FullName,
                     bits1,
                    txtCName1.Text.Trim(),
                    bits2,
                    txtCName2.Text.Trim(),
                    bits3,
                    txtPName1.Text.Trim(),
                    companyName, "(" + Designation + ")")

        OrderPriceDS.Tables.Add(dt)

        Dim rpt4 As New rpt_OrderAgreementOrderAcceptance
        Class1.WriteXMlFile(OrderPriceDS, "Generate_Order_Acceptance", "OrderAcceptance")
        rpt4.Database.Tables(0).SetDataSource(OrderPriceDS.Tables("OrderAcceptance"))
        rpt4.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\7.pdf")
        rpt4.Dispose()

    End Sub
    Public Sub Get_ProfilePhoto()
        PictProfile12.Image = Nothing

        Dim GetUser = linq_obj.SP_Get_UserList().ToList().Where(Function(p) p.UserName.ToLower() = txtusername1.Text.ToLower()).ToList()
        For Each item As SP_Get_UserListResult In GetUser
            Dim Imagepath = Convert.ToString(item.Signature).Replace("D:", "\\192.168.1.102")
            'profilephoto3 = item.Signature.ToString()
            'PictProfile12.ImageLocation = profilephoto3
            'PictProfile12.SizeMode = PictureBoxSizeMode.StretchImage
            FullName = item.FirstName + " " + item.LastName
            Designation = item.Designation
            'PictProfile12.Refresh()

            PictProfile12.ImageLocation = Imagepath
            PictProfile12.SizeMode = PictureBoxSizeMode.StretchImage
        Next

    End Sub
    Public Sub Generate_Index_Page()
        QPath = Class1.global.QPath
        If (Not System.IO.Directory.Exists(QPath + "\OrderTempFile")) Then
            System.IO.Directory.CreateDirectory(QPath + "\OrderTempFile")
        End If
        OrderTempPath = QPath + "\OrderTempFile"
        Dim OrderAgreementDS As New DataSet()
        Dim dt = New DataTable("tblIndex")
        dt.Columns.Add("SrNo")
        dt.Columns.Add("Description")
        dt.Rows.Add("1", "System-1 : Water Treatment System")
        dt.Rows.Add("2", "System-2 : Quality Control Lab")

        Dim CountSrNo As Integer
        Dim SRNo As Integer

        CountSrNo = 1
        SRNo = 3

        For index = 0 To GvSystem3.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvSystem3.Rows(index).Cells(1).Value)
            If IsTicked Then
                Dim str As String
                str = "System-3." + CountSrNo.ToString()
                CountSrNo = CountSrNo + 1
                dt.Rows.Add(SRNo.ToString(), str + " : " + GvSystem3.Rows(index).Cells(4).Value)
                SRNo = SRNo + 1

            End If
        Next

        dt.Rows.Add(SRNo, "System-4 : Value Added Technology")
        dt.Rows.Add(SRNo + 1, "Client Scope : General")
        dt.Rows.Add(SRNo + 2, "Client Scope : Raw Material")
        dt.Rows.Add(SRNo + 3, "Client Scope : Man Power")
        dt.Rows.Add(SRNo + 4, "Price Sheet")
        dt.Rows.Add(SRNo + 5, "Terms & Conditions")
        dt.Rows.Add(SRNo + 6, "Complimentory")
        dt.Rows.Add(SRNo + 7, "Civil Work/Readiness for E&C")
        dt.Rows.Add(SRNo + 8, "Varients in Projects")

        OrderAgreementDS.Tables.Add(dt)
        Dim rpt4 As New rpt_OrderAgreementIndex
        Class1.WriteXMlFile(OrderAgreementDS, "OrderAgreementIndex", "tblIndex")
        rpt4.Database.Tables(0).SetDataSource(OrderAgreementDS.Tables("tblIndex"))
        rpt4.ExportToDisk(ExportFormatType.PortableDocFormat, OrderTempPath + "\\2index.pdf")
        rpt4.Dispose()

    End Sub
    Private Sub BtnSearchAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchAll.Click

        clear_text_payment_Terms()

        ProgressBar1.Visible = False
        DiplayData()
        Order_Category_Data_Bind()
        Price_Total_Calculation()
    End Sub
    Private Sub txtMainCategory_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtCapacity.Focus()
    End Sub
    Private Sub btnSys3Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub txtCapacity_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCapacity.Leave
        txtPCapacity.Text = txtCapacity.Text
        txtPDFCapacity.Text = txtCapacity.Text + "LPH"
    End Sub
    Private Sub btnSys1Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSys1Add.Click
        If txtSys1ItemName.Text.Trim() <> "" Then
            Dim System1 = linq_obj.SP_Get_Order_Category_By_Criteria("System-1", "", txtCapacity.Text.Trim()).ToList().Where(Function(p) p.Model = txtSys1ItemName.Text.Trim()).ToList()
            Dim SrNocount1 As Integer
            If (dt1.Rows.Count > 0) Then
                SrNocount1 = dt1.Rows.Count + 1
                For Each item As SP_Get_Order_Category_By_CriteriaResult In System1
                    DiscountAmt = 0
                    FinalRate = 0
                    GstAmount = 0
                    DiscountAmt = Discount_Calculation(item.Price)
                    FinalRate = item.Price - DiscountAmt
                    GstAmount = GST_Calculation(FinalRate, item.GST)
                    FinalAmount = FinalRate + GstAmount
                    dt1.Rows.Add(item.Pk_OrderCategory_ID, 1, SrNocount1, item.Model, item.Price, DiscountAmt, FinalRate, item.GST, Convert.ToString(GstAmount), FinalAmount)
                    SrNocount1 = SrNocount1 + 1
                Next
                GvSystem1.DataSource = dt1
                GvSystem1.Columns(0).Visible = False
            End If
            Price_Total_Calculation()
        End If
        txtSys1ItemName.Text = ""

    End Sub
    Private Sub btnSys4Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSys4Add.Click
        If txtSys4ItemName.Text.Trim() <> "" Then
            Dim System4 = linq_obj.SP_Get_Order_Category_By_Criteria("System-4", "", txtCapacity.Text.Trim()).ToList().Where(Function(p) p.Model = txtSys4ItemName.Text.Trim()).ToList()
            Dim SrNocount4 As Integer
            If (dt4.Rows.Count > 0) Then
                SrNocount4 = dt4.Rows.Count + 1
                For Each item As SP_Get_Order_Category_By_CriteriaResult In System4
                    DiscountAmt = 0
                    FinalRate = 0
                    GstAmount = 0
                    DiscountAmt = Discount_Calculation(item.Price)
                    FinalRate = item.Price - DiscountAmt
                    GstAmount = GST_Calculation(FinalRate, item.GST)
                    FinalAmount = FinalRate + GstAmount
                    dt4.Rows.Add(item.Pk_OrderCategory_ID, 1, 1, SrNocount4, item.Model, item.Price, DiscountAmt, FinalRate, item.GST, Convert.ToString(GstAmount), FinalAmount, "")
                    SrNocount4 = SrNocount4 + 1
                Next
                GvSystem4.DataSource = dt4
                GvSystem4.Columns(0).Visible = False
            End If
            Price_Total_Calculation()
            txtSys4ItemName.Text = ""

        End If
    End Sub
    Private Sub btnSys1New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSys1New.Click
        txtSys1ItemName.Text = ""
    End Sub
    Private Sub btnSys4New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSys4New.Click
        txtSys4ItemName.Text = ""
    End Sub
    Private Sub btnSys3Add_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSys3Add.Click
        If txtSys3ItemName.Text.Trim() <> "" And txtSys3ItemNameCapacity.Text.Trim() <> "" Then
            Dim System3 = linq_obj.SP_Get_Order_Category_By_Criteria("System-3", "", "").ToList().Where(Function(p) p.Model = txtSys3ItemName.Text.Trim() And p.Capacity = txtSys3ItemNameCapacity.Text.Trim()).ToList()
            Dim SrNocount3 As Integer
            If (dt3.Rows.Count > 0) Then
                SrNocount3 = dt3.Rows.Count + 1
                For Each item As SP_Get_Order_Category_By_CriteriaResult In System3
                    DiscountAmt = 0
                    FinalRate = 0
                    GstAmount = 0
                    DiscountAmt = Discount_Calculation(item.Price)
                    FinalRate = item.Price - DiscountAmt
                    GstAmount = GST_Calculation(FinalRate, item.GST)
                    FinalAmount = FinalRate + GstAmount

                    dt3.Rows.Add(item.Pk_OrderCategory_ID, 1, 1, SrNocount3, item.Model, item.Capacity, item.Price, DiscountAmt, FinalRate, item.GST, Convert.ToString(GstAmount), FinalAmount, item.Remarks)
                    SrNocount3 = SrNocount3 + 1
                Next
                GvSystem3.DataSource = dt3
                GvSystem3.Columns(0).Visible = False
            End If
            Price_Total_Calculation()
            txtSys3ItemName.Text = ""
            txtSys3ItemNameCapacity.Text = ""
            GvClientRawMaterial_Bind()
        Else
            MessageBox.Show("Sub Category and capacity not blank..")
        End If
    End Sub
    Private Sub btnSys2New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSys2New.Click
        txtSys2ItemName.Text = ""
    End Sub
    Private Sub btnSys3New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSys3New.Click
        txtSys3ItemName.Text = ""
        txtSys3ItemNameCapacity.Text = ""

    End Sub
    Private Sub btnSys2Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSys2Add.Click
        If txtSys2ItemName.Text.Trim() <> "" Then
            Dim System2 = linq_obj.SP_Get_Order_Category_By_Criteria("System-2", "", "").ToList().Where(Function(p) p.Model = txtSys2ItemName.Text.Trim()).ToList()
            Dim SrNocount2 As Integer
            If (dt2.Rows.Count > 0) Then
                SrNocount2 = dt2.Rows.Count + 1
                For Each item As SP_Get_Order_Category_By_CriteriaResult In System2
                    DiscountAmt = 0
                    FinalRate = 0
                    GstAmount = 0
                    DiscountAmt = Discount_Calculation(item.Price)
                    FinalRate = item.Price - DiscountAmt
                    GstAmount = GST_Calculation(FinalRate, item.GST)
                    FinalAmount = FinalRate + GstAmount

                    dt2.Rows.Add(item.Pk_OrderCategory_ID, 1, 1, SrNocount2, item.Model, item.Price, DiscountAmt, FinalRate, item.GST, Convert.ToString(GstAmount), FinalAmount)

                    SrNocount2 = SrNocount2 + 1
                Next
                GvSystem2.DataSource = dt2
                GvSystem2.Columns(0).Visible = False
            End If
            Price_Total_Calculation()
        End If
        txtSys2ItemName.Text = ""

    End Sub
    Private Sub btnPic1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPic1.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PicPhoto1.Image = Image.FromFile(OpenFileDialog1.FileName)
            PicPhoto1.SizeMode = PictureBoxSizeMode.StretchImage
            'Dim gp As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath
            'gp.AddEllipse(0, 0, (PicPhoto1.Width - 3), (PicPhoto1.Height - 3))
            'Dim rg As Region = New Region(gp)
            'PicPhoto1.Region = rg
            'PicPhoto1.SizeMode = PictureBoxSizeMode.StretchImage
        End If

    End Sub

    Private Sub btnPics2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPics2.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PicPhoto2.Image = Image.FromFile(OpenFileDialog1.FileName)
            PicPhoto2.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub

    Private Sub btnPics3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPics3.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PicPPic1.Image = Image.FromFile(OpenFileDialog1.FileName)
            PicPPic1.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub

    Private Sub btnPics4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPics4.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PicPPic2.Image = Image.FromFile(OpenFileDialog1.FileName)
            PicPPic2.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        State_Bind()
        Clear_Text()
        Enable_True()
        Payment_Terms()
    End Sub

    Public Sub Enable_False()
        txtEnqNo.ReadOnly = True
        ddlQtnType.Enabled = False
        txtCapacity.Enabled = False
        txtMainDiscount.Enabled = False
        BtnSearchAll.Enabled = False
    End Sub
    Public Sub Enable_True()
        txtEnqNo.ReadOnly = False
        ddlQtnType.Enabled = True
        txtCapacity.Enabled = True
        txtMainDiscount.Enabled = True
        BtnSearchAll.Enabled = True
    End Sub
    Public Sub Clear_Text()
        ProgressBar1.Visible = False




        btnSubmit.Text = "Submit"



        Pk_Order_Agreement_ID = 0

        Fk_Address_ID = 0
        txtEnqNo.Text = ""
        txtCapacity.Text = ""

        txtMainDiscount.Text = "0"

        txtCompanyName.Text = ""
        txtAddress.Text = ""
        txtCity.Text = ""
        txtDistrict.Text = ""

        txtPincode.Text = ""
        txtTaluko.Text = ""
        txtRAddress.Text = ""
        txtRCity.Text = ""
        txtRState.Text = ""
        txtRTaluko.Text = ""
        txtRPincode.Text = ""
        txtCName1.Text = ""
        txtCMobile1.Text = ""
        txtCBusiness1.Text = ""
        txtCEmail1.Text = ""
        txtCName2.Text = ""
        txtCMobile2.Text = ""
        txtCBusiness2.Text = ""
        txtCEmail2.Text = ""
        'Plant Detail
        txtPPlant.Text = "Mineral Water Plant"
        txtPType.Text = ddlQtnType.Text

        txtPModel.Text = "Xcel"
        txtPCapacity.Text = ""
        txtPName1.Text = ""

        txtPMobile1.Text = ""
        txtPEmail1.Text = ""
        txtPBusiness1.Text = ""
        txtPName2.Text = ""
        txtPMobile2.Text = ""
        txtPEmail2.Text = ""
        txtPBusiness2.Text = ""

        txtSAddress.Text = ""
        txtSCity.Text = ""
        txtSState.Text = ""
        txtSDistrict.Text = ""
        txtSPincode.Text = ""


        txtGstNo.Text = ""
        txtGPanNo.Text = ""
        txtGlName.Text = ""
        txtGTradeName.Text = ""

        PictProfile12.Image = Nothing
        txtusername1.Text = ""

        PicPhoto1.Image = Nothing
        PicPhoto2.Image = Nothing
        PicPPic1.Image = Nothing
        PicPPic2.Image = Nothing

        GvSystem1.DataSource = Nothing
        GvSystem2.DataSource = Nothing
        GvSystem3.DataSource = Nothing
        GvSystem4.DataSource = Nothing
        GvSystem1Tech.DataSource = Nothing
        GvClientGeneralNew.DataSource = Nothing
        GvClientRawMaterial.DataSource = Nothing
        GvClientManPower.DataSource = Nothing
        GvComplimentory.DataSource = Nothing

        clear_text_payment_Terms()
    End Sub
    Public Sub clear_text_payment_Terms()
        'Payment Terms

        txtPT_TotalRate.Text = " "
        txtPT_TotalGST.Text = " "

        txtPT_FirstAdvance.Text = " "
        txtPT_SecondAdvance.Text = " "
        txtPT_FinalPayment.Text = " "
        txtPT_PaymentReceive.Text = " "
        txtPT_TentaDT.Text = ""
        txtPT_DispDT.Text = ""
        txtPT_Errection1.Text = ""
        txtPT_Gaurantee1.Text = ""
        txtPT_Terms1.Text = ""

        'Acceptance 
        txtRemarks1.Text = "-"
        txtRemarks2.Text = "-"
        txtRemarks3.Text = "-"
        txtRemarks4.Text = "-"
        txtRemarks5.Text = "-"

    End Sub

    Private Sub txtSys3ItemName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSys3ItemName.Leave
        txtSys3ItemNameCapacity.Text = ""


        txtSys3ItemNameCapacity.AutoCompleteCustomSource.Clear()
        Dim Sytem3Auto = linq_obj.SP_Get_Order_Category_By_Criteria("System-3", "", "").ToList().Where(Function(p) p.Model = txtSys3ItemName.Text.Trim()).ToList() 'No Capacity
        For Each item As SP_Get_Order_Category_By_CriteriaResult In Sytem3Auto
            txtSys3ItemNameCapacity.AutoCompleteCustomSource.Add(item.Capacity)
        Next
    End Sub

    Private Sub GvSystem1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvSystem1.CellClick
        Price_Total_Calculation()
    End Sub

    Private Sub GvSystem2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvSystem2.CellContentClick
        Price_Total_Calculation()
    End Sub

    Private Sub GvSystem3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvSystem3.CellContentClick
        Price_Total_Calculation()

    End Sub

    Private Sub GvSystem4_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvSystem4.CellContentClick
        Price_Total_Calculation()
    End Sub

    Private Sub btnBrowseProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim of1 As OpenFileDialog = New OpenFileDialog

            If (of1.ShowDialog = DialogResult.OK) Then
                'txtImage.Text = of1.FileName 
                Dim FileInfo As New FileInfo(of1.FileName)

                'this gets the filename and extension e.g. readme.txt
                MessageBox.Show(FileInfo.Name)

                PictProfile12.Image = Image.FromFile(of1.FileName)
            End If

        Catch ex As Exception
            MessageBox.Show(("Error :" + ex.Message))
        End Try
    End Sub

    Private Sub txtusername1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtusername1.Leave
        Get_ProfilePhoto()
    End Sub

    Private Sub btnAddTech_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTech.Click
        If txtSys1ItemTechName.Text.Trim() <> "" Then
            Dim Sytem1Tech = linq_obj.SP_Get_Order_Category_Detail_By_Capacity("System-1").ToList().Where(Function(p) p.Item_Name = txtSys1ItemTechName.Text.Trim() And txtCapacity.Text.Trim() = txtCapacity.Text.Trim() And p.Model = "Mineral Water Plant - Xcel").ToList()

            Dim SrNocount1Tech As Integer
            If (dttech.Rows.Count > 0) Then
                SrNocount1Tech = dttech.Rows.Count + 1
                For Each item As SP_Get_Order_Category_Detail_By_CapacityResult In Sytem1Tech
                    dttech.Rows.Add(item.Pk_OrderCategory_Detaild_ID, 1, 1, item.SrNo, item.Item_Name, "")
                    SrNocount1Tech = SrNocount1Tech + 1
                Next
                GvSystem1Tech.DataSource = dttech
                GvSystem1Tech.Columns(0).Visible = False
            End If

            txtSys1ItemTechName.Text = ""

        End If
    End Sub


    Private Sub GvSystem3_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles GvSystem3.CellMouseClick
        GvClientRawMaterial_Bind()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 8 Then
            'do stuff
            GvClientRawMaterial_Bind()
        End If
        If TabControl1.SelectedIndex = 13 Then
            'do stuff
            Tech_Data_Image()
        End If
    End Sub

    Public Sub Tech_Data_Image()

        Dim Fk_Order_Category_ID As String
        Fk_Order_Category_ID = ""

        Clear_Image_Tab()

        Dim Techdata = linq_obj.SP_Get_Order_Category_Detail_By_Capacity(txtPPlant.Text.Trim().ToLower()).ToList().Where(Function(p) p.Model.ToLower() = txtPModel.Text.ToLower() And p.Capacity = txtCapacity.Text.Trim()).ToList()
        For Each item As SP_Get_Order_Category_Detail_By_CapacityResult In Techdata
            Img_PicMain.ImageLocation = Convert.ToString(item.Item_Photo1).Replace("D:", "\\192.168.1.102")
            Img_PicMain.SizeMode = PictureBoxSizeMode.StretchImage
        Next
        'System 2 
        Dim TechSystem2 = linq_obj.SP_Get_Order_Category_Detail_By_Capacity("System 2-Image").ToList()
        For Each item As SP_Get_Order_Category_Detail_By_CapacityResult In TechSystem2
            Pic_Sys2_1.ImageLocation = Convert.ToString(item.Item_Photo1).Replace("D:", "\\192.168.1.102")
            Pic_Sys2_1.SizeMode = PictureBoxSizeMode.StretchImage
        Next
        'System 3
        For index = 0 To GvSystem3.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvSystem3.Rows(index).Cells(1).Value)
            If IsTicked Then
                Fk_Order_Category_ID = Fk_Order_Category_ID + GvSystem3.Rows(index).Cells("PK_ID").Value.ToString() + ","
            End If
        Next

        'Call SP

        Dim datatech3 = linq_obj.SP_Get_Order_Agreement_Tech_Image_By_Category_ID(Fk_Order_Category_ID).ToList()
        For i = 0 To datatech3.Count - 1

            Dim Imagepath = Convert.ToString(datatech3(i).Item_Photo1).Replace("D:", "\\192.168.1.102")


            If i = 0 Then
                Pic_Sys3_1.ImageLocation = Imagepath
                Pic_Sys3_1.SizeMode = PictureBoxSizeMode.StretchImage
            End If
            If i = 1 Then

                Pic_Sys3_2.ImageLocation = Imagepath
                Pic_Sys3_2.SizeMode = PictureBoxSizeMode.StretchImage


            End If
            If i = 2 Then

                Pic_Sys3_3.ImageLocation = Imagepath
                Pic_Sys3_3.SizeMode = PictureBoxSizeMode.StretchImage

            End If
            If i = 3 Then

                Pic_Sys3_4.ImageLocation = Imagepath
                Pic_Sys3_4.SizeMode = PictureBoxSizeMode.StretchImage

            End If
            If i = 4 Then

                Pic_Sys3_5.ImageLocation = Imagepath
                Pic_Sys3_5.SizeMode = PictureBoxSizeMode.StretchImage

            End If
            If i = 5 Then

                Pic_Sys3_6.ImageLocation = Imagepath
                Pic_Sys3_6.SizeMode = PictureBoxSizeMode.StretchImage

            End If
            If i = 6 Then

                Pic_Sys3_7.ImageLocation = Imagepath
                Pic_Sys3_7.SizeMode = PictureBoxSizeMode.StretchImage

            End If
            If i = 7 Then

                Pic_Sys3_8.ImageLocation = Imagepath
                Pic_Sys3_8.SizeMode = PictureBoxSizeMode.StretchImage

            End If



        Next
        'System 4

        Fk_Order_Category_ID = ""
        For index = 0 To GvSystem4.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvSystem4.Rows(index).Cells(1).Value)
            If IsTicked Then
                Fk_Order_Category_ID = Fk_Order_Category_ID + GvSystem4.Rows(index).Cells("PK_ID").Value.ToString() + ","
            End If
        Next

        'Call SP

        Dim datatech4 = linq_obj.SP_Get_Order_Agreement_Tech_Image_By_Category_ID(Fk_Order_Category_ID).ToList()
        For i = 0 To datatech4.Count - 1

            Dim Imagepath = Convert.ToString(datatech4(i).Item_Photo1).Replace("D:", "\\192.168.1.102")

            If i = 0 Then

                Pic_Sys4_1.ImageLocation = Imagepath
                Pic_Sys4_1.SizeMode = PictureBoxSizeMode.StretchImage

            End If

            If i = 1 Then

                Pic_Sys4_2.ImageLocation = Imagepath
                Pic_Sys4_2.SizeMode = PictureBoxSizeMode.StretchImage

            End If
            If i = 2 Then

                Pic_Sys4_3.ImageLocation = Imagepath
                Pic_Sys4_3.SizeMode = PictureBoxSizeMode.StretchImage

            End If
            If i = 3 Then

                Pic_Sys4_4.ImageLocation = Imagepath
                Pic_Sys4_4.SizeMode = PictureBoxSizeMode.StretchImage

            End If
            If i = 4 Then

                Pic_Sys4_5.ImageLocation = Imagepath
                Pic_Sys4_5.SizeMode = PictureBoxSizeMode.StretchImage

            End If


        Next
    End Sub
    Public Sub Clear_Image_Tab()

        Img_PicMain.ImageLocation = Nothing


        Pic_Sys2_1.ImageLocation = Nothing
        Pic_Sys3_1.ImageLocation = Nothing

        Pic_Sys3_2.ImageLocation = Nothing

        Pic_Sys3_3.ImageLocation = Nothing

        Pic_Sys3_4.ImageLocation = Nothing

        Pic_Sys3_5.ImageLocation = Nothing

        Pic_Sys3_6.ImageLocation = Nothing

        Pic_Sys3_7.ImageLocation = Nothing

        Pic_Sys3_8.ImageLocation = Nothing

        Pic_Sys4_1.ImageLocation = Nothing

        Pic_Sys4_2.ImageLocation = Nothing

        Pic_Sys4_3.ImageLocation = Nothing

        Pic_Sys4_4.ImageLocation = Nothing

        Pic_Sys4_5.ImageLocation = Nothing


    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GvOrderAgreementList_Bind()
    End Sub

    Private Sub GvOrderAgreementList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvOrderAgreementList.DoubleClick
        Clear_Text()
        Pk_Order_Agreement_ID = GvOrderAgreementList.SelectedCells(0).Value
        btnSubmit.Text = "Update"
        Order_Personal_Information()
        Order_Display_Data()
        Price_Total_Calculation()
        Get_Order_Agreement_Terms()
        Get_Order_Agreement_Acceptance()
        Enable_False()

    End Sub
    Public Sub Get_Order_Agreement_Terms()

        Dim data = linq_obj.SP_Get_Order_Agreement_Terms_ID(Pk_Order_Agreement_ID).ToList()

        For Each item As SP_Get_Order_Agreement_Terms_IDResult In data

            txtPT_FirstAdvance.Text = item.First_Advance
            txtPT_SecondAdvance.Text = item.Second_Advance
            txtPT_PaymentReceive.Text = item.Payment_Receive
            txtPT_FinalPayment.Text = item.Final_Payment
            txtPT_TentaDT.Text = item.Tentative_DT
            txtPT_DispDT.Text = item.Disp_DT
            txtPT_Errection1.Text = item.Errection1
            txtPT_Errection2.Text = item.Errection2
            txtPT_Gaurantee1.Text = item.Gaurantee1
            txtPT_Gaurantee2.Text = item.Gaurantee2
            txtPT_Terms1.Text = item.Terms1
            txtPT_Terms2.Text = item.Terms2
            txtPT_Terms3.Text = item.Terms3
            txtPT_Terms4.Text = item.Terms4
            txtPT_Terms5.Text = item.Terms5

            txtPT_Terms.Text = item.PaymentTerms
            txtPT_Gov.Text = item.Govt_Tax
            txtPT_Deliver.Text = item.Delivery
            txtPT_Transp1.Text = item.Transportation1
            txtPT_Transp2.Text = item.Transportation2
            txtPT_Transp3.Text = item.Transportation3
            txtPT_Transp4.Text = item.Transportation4

        Next


    End Sub
    Public Sub Order_Personal_Information()

        Dim Personal = linq_obj.SP_Get_Order_Agreement_Master_ID(Pk_Order_Agreement_ID).ToList()
        txtMainDiscount.Text = "0"

        For Each item As SP_Get_Order_Agreement_Master_IDResult In Personal

            txtOrderNO.Text = item.OrderNo
            txtOrderDate.Text = item.OrderDate
            txtEnqNo.Text = item.Enq_No
            ddlQtnType.Text = item.Qty_Type
            txtCapacity.Text = item.Capacity
            txtPCapacity.Text = item.Capacity
            txtMainDiscount.Text = item.BussinessDetail
            txtEditDiscount.Text = item.BussinessDetail
            Fk_Address_ID = item.Fk_Address_ID
            txtCompanyName.Text = item.Name
            txtAddress.Text = item.Address
            txtCity.Text = item.City
            txtDistrict.Text = item.District
            txtState.Text = item.State
            txtPincode.Text = item.Pincode
            txtTaluko.Text = item.Taluka
            txtRAddress.Text = item.DeliveryAddress
            txtRCity.Text = item.DeliveryCity
            txtRState.Text = item.DeliveryState
            txtRTaluko.Text = item.DeliveryTaluka
            txtRDistrict.Text = item.DeliveryDistrict
            txtRPincode.Text = item.DeliveryPincode
            chkCopy.Checked = True

            chkCopy_CheckedChanged(Nothing, Nothing)


            txtGstNo.Text = item.GSTNo
            txtGPanNo.Text = item.PanNo
            txtGlName.Text = item.Lname
            txtGTradeName.Text = item.TName


            If item.Photo1 <> Nothing Then
                If item.Photo1.Length > 2 Then
                    Dim image1Data As Byte() = IIf(item.Photo1 = Nothing, Nothing, item.Photo1.ToArray())
                    If (image1Data.Length > 0) Then
                        If Not image1Data Is Nothing Then
                            msPic1 = New MemoryStream(image1Data, 0, image1Data.Length)
                            msPic1.Write(image1Data, 0, image1Data.Length)
                            PicPhoto1.Image = Image.FromStream(msPic1, True)
                            PicPhoto1.SizeMode = PictureBoxSizeMode.StretchImage
                        End If
                    End If
                End If
            End If

            If item.Photo2 <> Nothing Then
                If item.Photo2.Length > 2 Then
                    Dim image2Data As Byte() = IIf(item.Photo2 = Nothing, Nothing, item.Photo2.ToArray())
                    If (image2Data.Length > 0) Then
                        If Not image2Data Is Nothing Then
                            msPic2 = New MemoryStream(image2Data, 0, image2Data.Length)
                            msPic2.Write(image2Data, 0, image2Data.Length)
                            PicPhoto2.Image = Image.FromStream(msPic2, True)
                            PicPhoto2.SizeMode = PictureBoxSizeMode.StretchImage
                        End If
                    End If
                End If
            End If
            txtCName1.Text = item.Ph1_Value1
            txtCMobile1.Text = item.Ph1_Value2
            txtCBusiness1.Text = item.Ph1_Value3
            txtCEmail1.Text = item.Ph1_Value6
            txtCName2.Text = item.Ph2_Value1
            txtCMobile2.Text = item.Ph2_Value2
            txtCBusiness2.Text = item.Ph2_Value3
            txtCEmail2.Text = item.Ph2_Value6
            'Plant Detail
            txtPPlant.Text = item.P_Pant
            'txtPType.Text = item.ProjectName
            txtPModel.Text = item.P_Model
            txtPCapacity.Text = item.Capacity
            If item.Photo3 <> Nothing Then
                If item.Photo3.Length > 2 Then
                    Dim image3Data As Byte() = IIf(item.Photo3 = Nothing, Nothing, item.Photo3.ToArray())
                    If (image3Data.Length > 0) Then
                        If Not image3Data Is Nothing Then
                            msPic3 = New MemoryStream(image3Data, 0, image3Data.Length)
                            msPic3.Write(image3Data, 0, image3Data.Length)
                            PicPPic1.Image = Image.FromStream(msPic3, True)
                            PicPPic1.SizeMode = PictureBoxSizeMode.StretchImage
                        End If
                    End If

                End If
            End If
            If item.Photo4 <> Nothing Then
                If item.Photo4.Length > 2 Then
                    Dim image4Data As Byte() = IIf(item.Photo4 = Nothing, Nothing, item.Photo4.ToArray())
                    If (image4Data.Length > 0) Then
                        If Not image4Data Is Nothing Then
                            msPic4 = New MemoryStream(image4Data, 0, image4Data.Length)
                            msPic4.Write(image4Data, 0, image4Data.Length)
                            PicPPic2.Image = Image.FromStream(msPic4, True)
                            PicPPic2.SizeMode = PictureBoxSizeMode.StretchImage
                        End If
                    End If
                End If
            End If

            txtPName1.Text = item.Ph3_Value1
            txtPMobile1.Text = item.Ph3_Value2
            txtPEmail1.Text = item.Ph3_Value6
            txtPBusiness1.Text = item.Ph3_Value3

            txtPName2.Text = item.Ph4_Value1
            txtPMobile2.Text = item.Ph4_Value2
            txtPEmail2.Text = item.Ph4_Value6
            txtPBusiness2.Text = item.Ph4_Value3

        Next



    End Sub
    Public Sub Order_Display_Data()

        'System 1
        AutoComplate_text()
        txtPCapacity.Text = txtCapacity.Text
        dt1 = New DataTable
        dt2 = New DataTable
        dt3 = New DataTable
        dt4 = New DataTable


        If (ddlQtnType.Text = "ISI") Then

            Dim SrNocount As Integer
            Dim System1 = linq_obj.SP_Get_Order_Agreement_TechData_By_ID(Pk_Order_Agreement_ID).ToList()
            dt1.Columns.Add("PK_ID")
            dt1.Columns.Add("PriceList", GetType(Boolean))
            dt1.Columns.Add("SrNo")
            dt1.Columns.Add("ItemName")
            dt1.Columns.Add("Price")
            dt1.Columns.Add("Disc")
            dt1.Columns.Add("FinalRate")
            dt1.Columns.Add("GST")
            dt1.Columns.Add("GSTAmt")
            dt1.Columns.Add("FinalAmount")
            SrNocount = 1
            For Each item As SP_Get_Order_Agreement_TechData_By_IDResult In System1.Where(Function(p) p.MainCategory = "System-1").ToList()

                dt1.Rows.Add(item.Fk_OrderCategory_ID, 1, SrNocount, item.ItemName, item.Price, item.DiscAmt, item.FinalRate, item.GST, item.GSTAmt, item.FinalAmount)
                SrNocount = SrNocount + 1
            Next
            GvSystem1.DataSource = dt1
            GvSystem1.Columns(0).Visible = False

            Dim System1Tech = linq_obj.SP_Get_Order_Agreement_Client_TechData_By_ID(Pk_Order_Agreement_ID).ToList()

            'System 1 Technical data
            dttech = New DataTable()
            dttech.Columns.Add("PK_ID")
            dttech.Columns.Add("Text", GetType(Boolean))
            dttech.Columns.Add("Image", GetType(Boolean))
            dttech.Columns.Add("SrNo")
            dttech.Columns.Add("ItemName")
            dttech.Columns.Add("Description")

            SrNocount = 1
            For Each item As SP_Get_Order_Agreement_Client_TechData_By_IDResult In System1Tech.Where(Function(p) p.MainCategory = "System-1").ToList()
                dttech.Rows.Add(item.Fk_OrderCategory_Detail_ID, 1, 1, item.SrNo, item.ItemName, item.Description)
                SrNocount = SrNocount + 1
            Next
            GvSystem1Tech.DataSource = dttech
            GvSystem1Tech.Columns(0).Visible = False

            'System 2
            SrNocount = 1

            dt2.Columns.Add("PK_ID")
            dt2.Columns.Add("PriceList", GetType(Boolean))
            dt2.Columns.Add("Technical", GetType(Boolean))
            dt2.Columns.Add("SrNo")
            dt2.Columns.Add("ItemName")
            dt2.Columns.Add("Price")
            dt2.Columns.Add("Disc")
            dt2.Columns.Add("FinalRate")
            dt2.Columns.Add("GST")
            dt2.Columns.Add("GSTAmt")
            dt2.Columns.Add("FinalAmount")
            For Each item As SP_Get_Order_Agreement_TechData_By_IDResult In System1.Where(Function(p) p.MainCategory = "System-2").ToList()
                dt2.Rows.Add(item.Fk_OrderCategory_ID, 1, 1, SrNocount, item.ItemName, item.Price, item.DiscAmt, item.FinalRate, item.GST, item.GSTAmt, item.FinalAmount)
                SrNocount = SrNocount + 1
            Next
            GvSystem2.DataSource = dt2
            GvSystem2.Columns(0).Visible = False

            'System 3
            SrNocount = 1

            dt3 = New DataTable
            dt3.Columns.Add("PK_ID")
            dt3.Columns.Add("PriceList", GetType(Boolean))
            dt3.Columns.Add("Technical", GetType(Boolean))
            dt3.Columns.Add("SrNo")
            dt3.Columns.Add("ItemName")
            dt3.Columns.Add("Capacity")
            dt3.Columns.Add("Price")
            dt3.Columns.Add("Disc")
            dt3.Columns.Add("FinalRate")
            dt3.Columns.Add("GST")
            dt3.Columns.Add("GSTAmt")
            dt3.Columns.Add("FinalAmount")
            dt3.Columns.Add("Description")
            For Each item As SP_Get_Order_Agreement_TechData_By_IDResult In System1.Where(Function(p) p.MainCategory = "System-3").ToList()
                dt3.Rows.Add(item.Fk_OrderCategory_ID, 1, 1, SrNocount, item.ItemName, item.Capacity, item.Price, item.DiscAmt, item.FinalRate, item.GST, item.GSTAmt, item.FinalAmount, item.Description)
                SrNocount = SrNocount + 1
            Next
            GvSystem3.DataSource = dt3
            GvSystem3.Columns(0).Visible = False

            'System 4
            SrNocount = 1
            dt4.Columns.Add("PK_ID")
            dt4.Columns.Add("PriceList", GetType(Boolean))
            dt4.Columns.Add("Technical", GetType(Boolean))
            dt4.Columns.Add("SrNo")
            dt4.Columns.Add("ItemName")
            dt4.Columns.Add("Price")
            dt4.Columns.Add("Disc")
            dt4.Columns.Add("FinalRate")
            dt4.Columns.Add("GST")
            dt4.Columns.Add("GSTAmt")
            dt4.Columns.Add("FinalAmount")
            dt4.Columns.Add("Description")
            For Each item As SP_Get_Order_Agreement_TechData_By_IDResult In System1.Where(Function(p) p.MainCategory = "System-4").ToList()
                dt4.Rows.Add(item.Fk_OrderCategory_ID, 1, 1, SrNocount, item.ItemName, item.Price, item.DiscAmt, item.FinalRate, item.GST, item.GSTAmt, item.FinalAmount, item.Description)
                SrNocount = SrNocount + 1

            Next
            GvSystem4.DataSource = dt4
            GvSystem4.Columns(0).Visible = False
            'Price_Total_Calculation()

            'Client Scope Data Bind 

            'Client General
            SrNocount = 1

            Dim ClientGeneral = linq_obj.SP_Get_Order_Agreement_Client_TechData_By_ID(Pk_Order_Agreement_ID).ToList()

            Dim dtClientGene As New DataTable
            dtClientGene = New DataTable

            dtClientGene.Columns.Add("PK_ID")
            dtClientGene.Columns.Add("Text", GetType(Boolean))
            dtClientGene.Columns.Add("Image", GetType(Boolean))
            dtClientGene.Columns.Add("SrNo")
            dtClientGene.Columns.Add("ItemName")
            dtClientGene.Columns.Add("Description")

            For Each item As SP_Get_Order_Agreement_Client_TechData_By_IDResult In ClientGeneral.Where(Function(p) p.MainCategory.Trim() = "CLIENT SCOPE - GENERAL").ToList()
                dtClientGene.Rows.Add(item.Fk_OrderCategory_Detail_ID, 1, 1, SrNocount, item.ItemName, item.Description)
                SrNocount = SrNocount + 1
            Next
            GvClientGeneralNew.DataSource = dtClientGene
            GvClientGeneralNew.Columns(0).Visible = False


            'Client Raw Material

            Dim dtClientRawmateria As New DataTable
            dtClientRawmateria = New DataTable

            dtClientRawmateria.Columns.Add("PK_ID")
            dtClientRawmateria.Columns.Add("Text", GetType(Boolean))
            dtClientRawmateria.Columns.Add("Image", GetType(Boolean))
            dtClientRawmateria.Columns.Add("SrNo")
            dtClientRawmateria.Columns.Add("ItemName")
            dtClientRawmateria.Columns.Add("Description")

            For Each item As SP_Get_Order_Agreement_Client_TechData_By_IDResult In ClientGeneral.Where(Function(p) p.MainCategory.Trim() = "CLIENT SCOPE - RAW MATERIAL").ToList()
                dtClientRawmateria.Rows.Add(item.Fk_OrderCategory_Detail_ID, 1, 1, SrNocount, item.ItemName, item.Description)
                SrNocount = SrNocount + 1
            Next
            GvClientRawMaterial.DataSource = dtClientRawmateria
            GvClientRawMaterial.Columns(0).Visible = False
            'Client Mam Power
            SrNocount = 1

            Dim dtClientManPwr As New DataTable
            dtClientManPwr.Columns.Add("PK_ID")
            dtClientManPwr.Columns.Add("Text", GetType(Boolean))
            dtClientManPwr.Columns.Add("Image", GetType(Boolean))
            dtClientManPwr.Columns.Add("SrNo")
            dtClientManPwr.Columns.Add("ItemName")
            dtClientManPwr.Columns.Add("Description")

            For Each item As SP_Get_Order_Agreement_Client_TechData_By_IDResult In ClientGeneral.Where(Function(p) p.MainCategory = "CLIENT SCOPE - MAN POWER").ToList()
                dtClientManPwr.Rows.Add(item.Fk_OrderCategory_Detail_ID, 1, 1, SrNocount, item.ItemName, item.Description)
                SrNocount = SrNocount + 1
            Next
            GvClientManPower.DataSource = dtClientManPwr
            GvClientManPower.Columns(0).Visible = False


            'Complimentory
            SrNocount = 1

            Dim dtComplimentory As New DataTable
            dtComplimentory.Columns.Add("PK_ID")
            dtComplimentory.Columns.Add("Text", GetType(Boolean))
            dtComplimentory.Columns.Add("Image", GetType(Boolean))
            dtComplimentory.Columns.Add("SrNo")
            dtComplimentory.Columns.Add("ItemName")
            dtComplimentory.Columns.Add("Description")

            For Each item As SP_Get_Order_Agreement_Client_TechData_By_IDResult In ClientGeneral.Where(Function(p) p.MainCategory = "COMPLILMENTORY").ToList()
                dtComplimentory.Rows.Add(item.Fk_OrderCategory_Detail_ID, 1, 1, SrNocount, item.ItemName, item.Description)
                SrNocount = SrNocount + 1
            Next
            GvComplimentory.DataSource = dtComplimentory
            GvComplimentory.Columns(0).Visible = False

            'Terms 

            Dim Gaurantee As String
            Gaurantee = "12 Months From The Date Of Commissioning Of Plant Or  18 Months From The Date Of Supply Of The Plant Whichever Is Earlier."
            Gaurantee = Gaurantee + "(Nb:Guarantee Clause Is Valid For Mfg. Defect/Workmanshipdefect Only Liability Is Limited To Repair Or Replace Of The Same.)"

            Dim Terms As String

            txtPT_Terms1.Text = "In Case Of Order Cancellation Advance Will Be Not Refundable"
            txtPT_Terms2.Text = "Agreement Final Price Validity : 3 Months From Date Oforder   Agreement"
            txtPT_Terms3.Text = "Raw Material Price (Jar, Cool Jug, Dispenser, Pouch Roll, Cap)  Will Be Price Extra At Actual At The Time Of Delivery"


            txtPT_Errection1.Text = "To & Fro, + Lodging & Boding,+ Local Conveyance Charges To At The Time Of Plant Erection & Commissioning."
            txtPT_Gaurantee1.Text = Gaurantee

        Else

            MessageBox.Show("No Record Found")
        End If

    End Sub
    Public Sub Get_Order_Agreement_Acceptance()

        Dim data = linq_obj.SP_Get_Order_Agreement_Acceptance_ID(Pk_Order_Agreement_ID).ToList()

        For Each item As SP_Get_Order_Agreement_Acceptance_IDResult In data
            txtusername1.Text = item.UserName
            txtusername1_Leave(Nothing, Nothing)
            txtRemarks1.Text = item.Accept1
            txtRemarks2.Text = item.Accept2
            txtRemarks3.Text = item.Accept3
            txtRemarks4.Text = item.Accept4
            txtRemarks5.Text = item.Accept5


        Next
    End Sub

    Private Sub GvSystem1_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvSystem1.CellEndEdit
        If (e.ColumnIndex = 5 And GvSystem1.Rows(e.RowIndex).Cells("Disc").Value <> "") Or (e.ColumnIndex = 7 And GvSystem1.Rows(e.RowIndex).Cells("GST").Value <> "") Then
            Dim FinalRate As Decimal
            Dim GSTValue As String
            Dim GSTAmt As Decimal
            Dim GST As String
            'Edit Discount Amount
            FinalRate = Convert.ToDecimal(GvSystem1.Rows(e.RowIndex).Cells("Price").Value) - Convert.ToDecimal(GvSystem1.Rows(e.RowIndex).Cells("Disc").Value)
            GvSystem1.Rows(e.RowIndex).Cells("FinalRate").Value = FinalRate.ToString("N2")
            'Gst Calculation
            GSTValue = GvSystem1.Rows(e.RowIndex).Cells("GST").Value
            GSTAmt = GST_Calculation(FinalRate, Convert.ToDecimal(GSTValue.ToString().Trim()))
            GvSystem1.Rows(e.RowIndex).Cells("GSTAmt").Value = Convert.ToDecimal(GSTAmt).ToString("N2")
            GvSystem1.Rows(e.RowIndex).Cells("FinalAmount").Value = Convert.ToDecimal(FinalRate + GSTAmt).ToString("N2")
            Price_Total_Calculation()
        End If

    End Sub

    Private Sub GvSystem2_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvSystem2.CellEndEdit
        If (e.ColumnIndex = 6 And GvSystem2.Rows(e.RowIndex).Cells("Disc").Value <> "") Or (e.ColumnIndex = 8 And GvSystem2.Rows(e.RowIndex).Cells("GST").Value <> "") Then
            Dim FinalRate As Decimal
            Dim GSTValue As String
            Dim GSTAmt As Decimal
            Dim GST As String
            'Edit Discount Amount
            FinalRate = Convert.ToDecimal(GvSystem2.Rows(e.RowIndex).Cells("Price").Value) - Convert.ToDecimal(GvSystem2.Rows(e.RowIndex).Cells("Disc").Value)
            GvSystem2.Rows(e.RowIndex).Cells("FinalRate").Value = FinalRate.ToString("N2")
            'Gst Calculation
            GSTValue = GvSystem2.Rows(e.RowIndex).Cells("GST").Value
            GSTAmt = GST_Calculation(FinalRate, Convert.ToDecimal(GSTValue.ToString().Trim()))
            GvSystem2.Rows(e.RowIndex).Cells("GSTAmt").Value = Convert.ToDecimal(GSTAmt).ToString("N2")
            GvSystem2.Rows(e.RowIndex).Cells("FinalAmount").Value = Convert.ToDecimal(FinalRate + GSTAmt).ToString("N2")

            Price_Total_Calculation()
        End If
    End Sub

    Private Sub GvSystem3_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvSystem3.CellEndEdit
        If (e.ColumnIndex = 7 And GvSystem3.Rows(e.RowIndex).Cells("Disc").Value <> "") Or (e.ColumnIndex = 9 And GvSystem3.Rows(e.RowIndex).Cells("GST").Value <> "") Then
            Dim FinalRate As Decimal
            Dim GSTValue As String
            Dim GSTAmt As Decimal
            Dim GST As String
            'Edit Discount Amount
            FinalRate = Convert.ToDecimal(GvSystem3.Rows(e.RowIndex).Cells("Price").Value) - Convert.ToDecimal(GvSystem3.Rows(e.RowIndex).Cells("Disc").Value)
            GvSystem3.Rows(e.RowIndex).Cells("FinalRate").Value = FinalRate.ToString("N2")
            'Gst Calculation
            GSTValue = GvSystem3.Rows(e.RowIndex).Cells("GST").Value
            GSTAmt = GST_Calculation(FinalRate, Convert.ToDecimal(GSTValue.ToString().Trim()))
            GvSystem3.Rows(e.RowIndex).Cells("GSTAmt").Value = Convert.ToDecimal(GSTAmt).ToString("N2")
            GvSystem3.Rows(e.RowIndex).Cells("FinalAmount").Value = Convert.ToDecimal(FinalRate + GSTAmt).ToString("N2")
            Price_Total_Calculation()
        End If
    End Sub

    Private Sub GvSystem4_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvSystem4.CellEndEdit
        If (e.ColumnIndex = 6 And GvSystem3.Rows(e.RowIndex).Cells("Disc").Value <> "") Or (e.ColumnIndex = 8 And GvSystem3.Rows(e.RowIndex).Cells("GST").Value <> "") Then
            Dim FinalRate As Decimal
            Dim GSTValue As String
            Dim GSTAmt As Decimal
            Dim GST As String
            'Edit Discount Amount
            FinalRate = Convert.ToDecimal(GvSystem4.Rows(e.RowIndex).Cells("Price").Value) - Convert.ToDecimal(GvSystem4.Rows(e.RowIndex).Cells("Disc").Value)
            GvSystem4.Rows(e.RowIndex).Cells("FinalRate").Value = FinalRate.ToString("N2")
            'Gst Calculation
            GSTValue = GvSystem4.Rows(e.RowIndex).Cells("GST").Value
            GSTAmt = GST_Calculation(FinalRate, Convert.ToDecimal(GSTValue.ToString().Trim()))
            GvSystem4.Rows(e.RowIndex).Cells("GSTAmt").Value = Convert.ToDecimal(GSTAmt).ToString("N2")
            GvSystem4.Rows(e.RowIndex).Cells("FinalAmount").Value = Convert.ToDecimal(FinalRate + GSTAmt).ToString("N2")
            Price_Total_Calculation()
        End If
    End Sub

    Private Sub chkCopy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCopy.CheckedChanged
        If chkCopy.Checked = True Then

            txtSAddress.Text = txtRAddress.Text
            txtSCity.Text = txtRCity.Text
            txtSTaluko.Text = txtRTaluko.Text
            txtSState.Text = txtRState.Text
            txtSDistrict.Text = txtRDistrict.Text
            txtSPincode.Text = txtRPincode.Text
        Else
            txtSAddress.Text = ""
            txtSCity.Text = ""
            txtSState.Text = ""
            txtSDistrict.Text = ""
            txtSPincode.Text = ""
        End If
    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        Dim year1 As Int32
        Dim Ref As String

        Dim EnqNoCount As Integer
        EnqNoCount = 0
        year1 = Convert.ToInt32(txtOrderDate.Text.Substring(txtOrderDate.Text.Length - 2))
        Dim EnqNo = linq_obj.SP_Get_Order_Agreement_Master_List().Where(Function(p) p.Enq_No = txtEnqNo.Text.Trim()).ToList()
        EnqNoCount = EnqNo.Count() + 1

        Ref = "IIECL-OA / " + Class1.global.User.ToString().ToUpper() + " / " + txtEnqNo.Text + " - " + EnqNoCount.ToString() + "/ " + year1.ToString() + ""
        txtOrderNO.Text = Ref.ToString()

    End Sub


    Private Sub txtPT_FirstAdvance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPT_FirstAdvance.KeyPress

        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")

    End Sub

    Private Sub txtPT_SecondAdvance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPT_SecondAdvance.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
    End Sub

    Private Sub txtPT_FinalPayment_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPT_FinalPayment.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
    End Sub

    Private Sub txtPT_PaymentReceive_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPT_PaymentReceive.KeyPress
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ".")
    End Sub


    Public Sub txtPT_TentaDT_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPT_TentaDT.Leave

    End Sub


    Private Sub ddlQtnType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlQtnType.SelectedIndexChanged
        txtPType.Text = ddlQtnType.Text
        txtPPlant.Text = "Mineral Water Plant"
        txtPModel.Text = "Xcel"
    End Sub

    Public Sub Edit_Discount()

        For i As Integer = 0 To GvSystem1.RowCount - 1
            DiscountAmt = 0
            FinalRate = 0
            GstAmount = 0


            DiscountAmt = Edit_Discount_Calculation(Convert.ToDecimal(GvSystem1.Rows(i).Cells("Price").Value()))
            FinalRate = Convert.ToDecimal(GvSystem1.Rows(i).Cells("Price").Value()) - DiscountAmt
            GvSystem1.Rows(i).Cells("Disc").Value = DiscountAmt.ToString()
            GstAmount = GST_Calculation(FinalRate, GvSystem1.Rows(i).Cells("GST").Value())
            FinalAmount = FinalRate + GstAmount

            GvSystem1.Rows(i).Cells("FinalRate").Value = FinalRate.ToString()
            GvSystem1.Rows(i).Cells("FinalAmount").Value = FinalAmount.ToString()
        Next
        For i As Integer = 0 To GvSystem2.RowCount - 1
            DiscountAmt = 0
            FinalRate = 0
            GstAmount = 0

            DiscountAmt = Edit_Discount_Calculation(Convert.ToDecimal(GvSystem2.Rows(i).Cells("Price").Value()))
            FinalRate = Convert.ToDecimal(GvSystem2.Rows(i).Cells("Price").Value()) - DiscountAmt
            GstAmount = GST_Calculation(FinalRate, GvSystem2.Rows(i).Cells("GST").Value())
            FinalAmount = FinalRate + GstAmount
            GvSystem2.Rows(i).Cells("Disc").Value = DiscountAmt.ToString()
            GvSystem2.Rows(i).Cells("FinalRate").Value = FinalRate.ToString()
            GvSystem2.Rows(i).Cells("FinalAmount").Value = FinalAmount.ToString()

        Next
        For i As Integer = 0 To GvSystem3.RowCount - 1
            DiscountAmt = 0
            FinalRate = 0
            GstAmount = 0

            DiscountAmt = Edit_Discount_Calculation(Convert.ToDecimal(GvSystem3.Rows(i).Cells("Price").Value()))
            FinalRate = Convert.ToDecimal(GvSystem3.Rows(i).Cells("Price").Value()) - DiscountAmt
            GstAmount = GST_Calculation(FinalRate, GvSystem3.Rows(i).Cells("GST").Value())
            FinalAmount = FinalRate + GstAmount
            GvSystem3.Rows(i).Cells("Disc").Value = DiscountAmt.ToString()
            GvSystem3.Rows(i).Cells("FinalRate").Value = FinalRate.ToString()
            GvSystem3.Rows(i).Cells("FinalAmount").Value = FinalAmount.ToString()

        Next
        For i As Integer = 0 To GvSystem4.RowCount - 1
            DiscountAmt = 0
            FinalRate = 0
            GstAmount = 0

            DiscountAmt = Edit_Discount_Calculation(Convert.ToDecimal(GvSystem4.Rows(i).Cells("Price").Value()))
            FinalRate = Convert.ToDecimal(GvSystem4.Rows(i).Cells("Price").Value()) - DiscountAmt
            GstAmount = GST_Calculation(FinalRate, GvSystem4.Rows(i).Cells("GST").Value())
            FinalAmount = FinalRate + GstAmount
            GvSystem4.Rows(i).Cells("Disc").Value = DiscountAmt.ToString()
            GvSystem4.Rows(i).Cells("FinalRate").Value = FinalRate.ToString()
            GvSystem4.Rows(i).Cells("FinalAmount").Value = FinalAmount.ToString()
        Next

        Price_Total_Calculation()
    End Sub


    Private Sub txtEditDiscount_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEditDiscount.Leave
        Edit_Discount()
    End Sub

    Private Sub txtMainDiscount_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMainDiscount.Leave
        txtEditDiscount.Text = txtMainDiscount.Text
    End Sub


    
End Class