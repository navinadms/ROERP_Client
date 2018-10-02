﻿Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports pdfforge
Imports System.IO
Imports System.IO.File
Imports System.IO.StreamWriter
Imports Microsoft.Office.Interop
Imports System.Security.Principal
Imports System.Management
Public Class frmpurchaseAgreement
    Private con1 As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader
    Private da As SqlDataAdapter
    Private ds As DataSet
    Private da1 As SqlDataAdapter
    Private ds1 As DataSet
    Shared dt As DataTable
    Public capacityType As String
    Shared PurchaseMaxId As Int32
    Shared PurchaseId As Int32
    Public QPath As String
    Shared Str As String
    Shared Path11 As String
    Public UserID As Int32
    Shared Flag As Integer
    Shared CMaxId As Integer
    Shared CompanyId As Int32
    Shared TotalPrice As Decimal
    Shared DocumentStatus As Int16
    Shared OrderAgreementTempPath As String
    Dim appPath As String



    Public Sub New()
        InitializeComponent()
        GridPurchaseDataBind()
        GridTermsAndConditionDataBind()
        FillAutoCompleteDescription()
        Gv_GetPurchaseAggrementData()
        QPath = Class1.global.QPath
        Dim year1 As Int32
        year1 = Convert.ToInt32(System.DateTime.Now.ToString("yy"))
        txtSrno.Text = "1"
        txtPOrefNo.Text = "IIECL- / " + Class1.global.User.ToString() + " / " + year1.ToString() + ""

        If (Not System.IO.Directory.Exists(QPath + "\Purchase Agreement")) Then
            System.IO.Directory.CreateDirectory(QPath + "\Purchase Agreements")
        End If
        appPath = Path.GetDirectoryName(Application.ExecutablePath)
        OrderAgreementTempPath = QPath + "\Purchase Agreements"

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
            dataView.RowFilter = "([DetailName] like 'Purchase Order')"

            If (dataView.Count > 0) Then
                dv = dataView.ToTable()

                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            BtnPosave.Enabled = True
                        Else
                            BtnPosave.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsPrint") = True) Then
                            btnPdfWf.Enabled = True
                        Else
                            btnPdfWf.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsDelete") = True) Then
                            btndeletequotation.Enabled = True
                        Else
                            btndeletequotation.Enabled = False
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub GridPurchaseDataBind()
        Try
            con1 = Class1.con
            con1.Close()
        Catch ex As Exception

        End Try
        Try
            dt = New DataTable()
            dt.Columns.Add("Remove", GetType(Boolean))
            dt.Columns.Add("Select", GetType(Boolean))
            dt.Columns.Add("Sr_No", GetType(String))
            dt.Columns.Add("Plant", GetType(String))
            dt.Columns.Add("Model", GetType(String))
            dt.Columns.Add("Capacity", GetType(String))
            dt.Columns.Add("Qty", GetType(String))
            dt.Columns.Add("Price", GetType(String))
            dt.Columns.Add("Scheme", GetType(String))
            dt.Columns.Add("Scope_of_supply", GetType(String))

            con1.Open()
            Dim str As String
            str = "select Sr_No,Plant,Model,Capacity,Qty,Price,Scheme,Scope_of_supply from Purchase_agreement order by Sr_No ASC "
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtPlantNo.AutoCompleteCustomSource.Add(dr1.Item("Plant").ToString())
                txtModel.AutoCompleteCustomSource.Add(dr1.Item("Model").ToString())
                txtCapacity.AutoCompleteCustomSource.Add(dr1.Item("Capacity").ToString())
                txtScheme.AutoCompleteCustomSource.Add(dr1.Item("Scheme").ToString())
                txtScopeOfsupply.AutoCompleteCustomSource.Add(dr1.Item("Scope_of_supply").ToString())
            Next
            da.Dispose()
            ds.Dispose()
            con1.Close()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub FillAutoCompleteDescription()
        Dim enqtype As String = "select * from Purchase_agreement"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtQuatType.AutoCompleteCustomSource.Add(dr1.Item("Qtype").ToString())
            txtModel.AutoCompleteCustomSource.Add(dr1.Item("Model").ToString())
            txtPlantNo.AutoCompleteCustomSource.Add(dr1.Item("Plant").ToString())
        Next
        da.Dispose()
        ds.Dispose()
    End Sub
    Private Sub txtPlantNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlantNo.Leave

    End Sub

    Private Sub txtModel_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '    GridPurchaseDataBind()


    End Sub

    Public Sub GridTermsAndConditionDataBind()
        Try
            con1 = Class1.con
            con1.Close()
        Catch ex As Exception

        End Try
        Try
            dt = New DataTable()
            dt.Columns.Add("Remove", GetType(Boolean))
            dt.Columns.Add("Select", GetType(Boolean))
            dt.Columns.Add("Sr_No", GetType(String))
            dt.Columns.Add("Description", GetType(String))

            con1.Open()
            Dim str As String
            str = "select Sr_No,Description from Purchase_Terms order by Sr_No ASC"
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()

            da.Fill(ds)
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                dt.Rows.Add(0, 1, Convert.ToString(ds.Tables(0).Rows(i)("Sr_No")), ds.Tables(0).Rows(i)("Description").ToString())
            Next
            GvTermsAndcondition.DataSource = dt
            da.Dispose()
            ds.Dispose()
            con1.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtenqno_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Ref As String
        Dim EnqMax As Int16
        Dim str As String

    End Sub
    Public Sub Total1()
        For i As Integer = 0 To gvPurchaseDetail.Rows.Count - 1
            If gvPurchaseDetail.Rows(i).Cells("Total").Value.ToString().Trim() <> "" Then
                If IsNumeric(gvPurchaseDetail.Rows(i).Cells("Total").Value.ToString()) Then
                    TotalPrice = TotalPrice + Convert.ToDecimal(gvPurchaseDetail.Rows(i).Cells("Total").Value)
                End If
            End If
        Next
        txtFinalTotal.Text = TotalPrice.ToString()
    End Sub

    Private Sub btnAddmore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddmore.Click
        Dim Str As String
        Try
            con1.Close()
        Catch ex As Exception
        End Try

        Try

            Dim t As Int16
            If gvPurchaseDetail.DataSource Is Nothing Then
                dt = New DataTable()
                dt.Columns.Add("Remove", GetType(Boolean))
                dt.Columns.Add("Select", GetType(Boolean))
                dt.Columns.Add("Sr_No", GetType(String))
                dt.Columns.Add("Plant", GetType(String))
                dt.Columns.Add("Model", GetType(String))
                dt.Columns.Add("Qtype", GetType(String))
                dt.Columns.Add("Capacity", GetType(String))
                dt.Columns.Add("Qty", GetType(String))
                dt.Columns.Add("Price", GetType(String))
                dt.Columns.Add("Scheme", GetType(String))
                dt.Columns.Add("Scope_of_supply", GetType(String))
                dt.Columns.Add("Total", GetType(String))

            Else
                dt = gvPurchaseDetail.DataSource
            End If

            t = dt.Rows.Count
            t = t + 1
            txtSrno.Text = t.ToString()
            Total1()
            txtFinalTotal.Text = TotalPrice.ToString()
            dt.Rows.Add(0, 0, txtSrno.Text.Trim(), txtPlantNo.Text.Trim(), txtModel.Text.Trim(), txtQuatType.Text.Trim(), txtCapacity.Text.Trim(), txtQty.Text.Trim(), txtPrice.Text.Trim(), txtScheme.Text.Trim(), txtScopeOfsupply.Text.Trim(), txtTotal.Text.Trim())

            gvPurchaseDetail.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Please Try Again..!")
        End Try

    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label15.Click

    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.Click

    End Sub


    Private Sub BtnPosave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPosave.Click
        Try
            con1.Close()
        Catch ex As Exception

        End Try
        Try

            con1.Open()
            'AnneIds = ""
            'For i As Integer = 0 To GvAnexture.Rows.Count - 1
            '    Dim IsTicked As Boolean = CBool(GvAnexture.Rows(i).Cells(2).Value)
            '    If IsTicked Then
            '        AnneIds += Convert.ToString(GvAnexture.Rows(i).Cells(0).Value) + ","
            '    Else
            '    End If
            'Next

            'If AnneIds.Length > 0 Then
            '    AnneIds = AnneIds.Remove(AnneIds.Length - 1, 1)
            'End If
            'AnneIds = ""
            Dim Phone As Long
            Phone = 0

            If BtnPosave.Text = "Update" Then
                Str = "update Purchase_Quatation  Set Po_RefNo='" + txtPOrefNo.Text + "',OfferNo  ='" + txtOfferNo.Text + "',PoDate ='" + txtPoDate.Text + "',SupplierComapnyName ='" + txtSuplName.Text + "'," + _
Environment.NewLine + _
                "Supplier1 ='" + txtsuppl1.Text + "'," + _
Environment.NewLine + _
               "Supplier2='" + txtsupp2.Text + "'," + _
Environment.NewLine + _
                "Supplier3='" + txtsupp3.Text + "'," + _
Environment.NewLine + _
                "Supp_addr1='" + txtSuplAddr1.Text + "'," + _
Environment.NewLine + _
          "Supp_addr2='" + txtSuplAddr2.Text + "'," + _
Environment.NewLine + _
                "Supp_tele='" + txtSupltelePhone.Text + "'," + _
Environment.NewLine + _
               "Purchase_CompanyName='" + txtPurchaseCmnyName.Text.Trim() + "'," + _
Environment.NewLine + _
    "Purchase1='" + txtPurchase1.Text.Trim() + "'," + _
Environment.NewLine + _
    "Purchase2='" + txtPurchase2.Text.Trim() + "'," + _
Environment.NewLine + _
    "Purchase3='" + txtPurchase3.Text.Trim() + "'," + _
Environment.NewLine + _
    "Purchase4='" + txtPurchase4.Text.Trim() + "'," + _
Environment.NewLine + _
    "Purch_addr1='" + txtPurcAddr1.Text.Trim() + "'," + _
Environment.NewLine + _
    "Purch_addr2='" + txtPurcAddress2.Text.Trim() + "'," + _
Environment.NewLine + _
    "Purch_Telephone='" + txtPurTelephone.Text.Trim() + "'," + _
Environment.NewLine + _
    "ChequeNo='" + txtCno.Text.Trim() + "'," + _
Environment.NewLine + _
    "ChequeDate='" + txtcdate.Text.Trim() + "'," + _
Environment.NewLine + _
    "BankName='" + txtBankName.Text.Trim() + "'," + _
Environment.NewLine + _
    "Advance='" + txtAdvance.Text.Trim() + "'," + _
Environment.NewLine + _
    "AdvanceText='" + txtAdvanceText.Text.Trim() + "'," + _
    Environment.NewLine + _
    "Discount='" + txtDicount.Text.Trim() + "'" + _
                "where Pk_ID =" & PurchaseId & ""

                cmd = New SqlCommand(Str, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

            Else
                Str = "Insert into Purchase_Quatation(Po_RefNo,OfferNo,PoDate,SupplierComapnyName,Supplier1,Supplier2,Supplier3,Supp_addr1,Supp_addr2,Supp_tele,Purchase_CompanyName,Purchase1,Purchase2,Purchase3,Purchase4,Purch_addr1,Purch_addr2,Purch_Telephone,ChequeNo,ChequeDate,BankName,Advance,AdvanceText,Discount) values('" + txtPOrefNo.Text.Trim() + "','" + txtOfferNo.Text.Trim() + "','" + txtPoDate.Text.Trim() + "','" + txtSuplName.Text.Trim() + "','" + txtsuppl1.Text.Trim() + "','" + txtsupp2.Text.Trim() + "','" + txtsupp3.Text.Trim() + "','" + txtSuplAddr1.Text.Trim() + "','" + txtSuplAddr2.Text.Trim() + "','" + txtSupltelePhone.Text.Trim() + "','" + txtPurchaseCmnyName.Text.Trim() + "','" + txtPurchase1.Text.Trim() + "','" + txtPurchase2.Text.Trim() + "','" + txtPurchase3.Text.Trim() + "','" + txtPurchase4.Text.Trim() + "','" + txtPurcAddr1.Text.Trim() + "','" + txtPurcAddress2.Text.Trim() + "','" + txtPurTelephone.Text.Trim() + "','" + txtCno.Text.Trim() + "','" + txtcdate.Text.Trim() + "','" + txtBankName.Text.Trim() + "','" + txtAdvance.Text.Trim() + "','" + txtAdvanceText.Text.Trim() + "','" + txtDicount.Text.Trim() + "')"
                cmd = New SqlCommand(Str, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()


            End If
            Dim mm As String
            Dim techinical12 As String
            mm = "select Max(Pk_ID) as PMaxID from Purchase_Quatation"
            cmd = New SqlCommand(mm, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            If dr("PMaxID").ToString() = "" Then
                CMaxId = 1
            Else
                CMaxId = Convert.ToInt32(dr("PMaxID").ToString())
            End If
            cmd.Dispose()
            dr.Dispose()

            If BtnPosave.Text = "Update" Then

                techinical12 = "delete from Purchase_data where Fk_PurchaseId =" & PurchaseId & ""
                cmd = New SqlCommand(techinical12, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

            End If



            For i As Integer = 0 To gvPurchaseDetail.Rows.Count - 1

                Dim RemoveStatus As Boolean = CBool(gvPurchaseDetail.Rows(i).Cells(0).Value)
                If RemoveStatus Then
                Else
                    Dim status As String
                    Dim selectStatus As Boolean = CBool(gvPurchaseDetail.Rows(i).Cells(1).Value)
                    status = ""

                    If selectStatus Then
                        status = "Yes"
                    Else
                        status = "No"
                    End If
                    Dim MainCategory As String
                    If BtnPosave.Text = "Save" Then
                        techinical12 = "insert into Purchase_data(Fk_PurchaseId,Sr_No,Plant,Model,Qtype,Capacity,Qty,Price,Scheme,Scope_of_supply,IsSelected,Total) values(" & CMaxId & ",'" + gvPurchaseDetail.Rows(i).Cells("Sr_No").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Plant").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Model").Value.ToString().Trim() + "','" + gvPurchaseDetail.Rows(i).Cells("Qtype").Value.ToString().Trim() + "','" + gvPurchaseDetail.Rows(i).Cells("Capacity").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Qty").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Price").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Scheme").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Scope_of_Supply").Value.ToString() + "','" + status + "','" + gvPurchaseDetail.Rows(i).Cells("Total").Value.ToString() + "')"
                        cmd = New SqlCommand(techinical12, con1)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                    Else
                        techinical12 = "insert into Purchase_data(Fk_PurchaseId,Sr_No,Plant,Model,Qtype,Capacity,Qty,Price,Scheme,Scope_of_supply,IsSelected,Total) values(" & PurchaseId & ",'" + gvPurchaseDetail.Rows(i).Cells("Sr_No").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Plant").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Model").Value.ToString().Trim() + "','" + gvPurchaseDetail.Rows(i).Cells("Qtype").Value.ToString().Trim() + "','" + gvPurchaseDetail.Rows(i).Cells("Capacity").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Qty").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Price").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Scheme").Value.ToString() + "','" + gvPurchaseDetail.Rows(i).Cells("Scope_of_Supply").Value.ToString() + "','" + status + "','" + gvPurchaseDetail.Rows(i).Cells("Total").Value.ToString() + "')"
                        cmd = New SqlCommand(techinical12, con1)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()

                    End If
                End If
            Next

            con1.Close()
            MessageBox.Show("Successfully Submit .....")

            Gv_GetPurchaseAggrementData()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub


    Public Sub Gv_GetPurchaseAggrementData()
        Try
            con1.Close()
        Catch ex As Exception

        End Try
        con1.Open()

        Dim str1 As String
        Dim da123 As New SqlDataAdapter
        Dim ds123 As New DataSet
        Dim dt12 As New DataTable


        dt12.Columns.Add("Pk_ID", GetType(String))
        dt12.Columns.Add("Po_RefNo", GetType(String))
        dt12.Columns.Add("OfferNo", GetType(String))
        dt12.Columns.Add("SupplierComapnyName", GetType(String))
        dt12.Columns.Add("Purchase_CompanyName", GetType(String))

        str1 = "select Pk_ID,Po_RefNo,OfferNo,SupplierComapnyName,Purchase_CompanyName from Purchase_Quatation "
        da123 = New SqlDataAdapter(str1, con1)
        ds123 = New DataSet()
        da123.Fill(ds123)

        For Each dr1 As DataRow In ds123.Tables(0).Rows
            txtCompnaysearch.AutoCompleteCustomSource.Add(dr1.Item("OfferNo").ToString())

        Next

        For S1 = 0 To ds123.Tables(0).Rows.Count - 1

            dt12.Rows.Add(ds123.Tables(0).Rows(S1)("Pk_ID").ToString(), ds123.Tables(0).Rows(S1)("Po_RefNo").ToString(), ds123.Tables(0).Rows(S1)("OfferNo").ToString(), ds123.Tables(0).Rows(S1)("SupplierComapnyName").ToString(), ds123.Tables(0).Rows(S1)("Purchase_CompanyName").ToString())
        Next
        GvPurchaseSearch.DataSource = dt12
        txtTotalAgreeMent.Text = dt12.Rows.Count.ToString()
        da123.Dispose()
        dt12.Dispose()
        ds123.Dispose()
        con1.Close()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        BtnPosave.Text = "Save"
        ClearText()
    End Sub
    Public Sub ClearText()
        txtAdvance.Text = ""
        txtAdvanceText.Text = ""
        txtCno.Text = ""
        txtCompnaysearch.Text = ""
        txtDicount.Text = ""
        txtCapacity.Text = ""
        txtFinalTotal.Text = ""
        txtModel.Text = ""
        txtOfferNo.Text = ""
        txtpalace.Text = ""
        txtPurcAddr1.Text = ""
        txtPurcAddress2.Text = ""
        txtPurchase1.Text = ""
        txtPurchase2.Text = ""
        txtPurchase3.Text = ""
        txtPurchase4.Text = ""
        txtPrice.Text = ""
        txtPurchaseCmnyName.Text = ""
        txtTotal.Text = ""
        txtScopeOfsupply.Text = ""
        txtScheme.Text = ""
        txtQty.Text = ""
        txtTotal.Text = ""
        txtCapacity.Text = ""
        txtQuatType.Text = ""
        txtModel.Text = ""
        txtPlantNo.Text = ""
        txtBankName.Text = ""
    End Sub

    Private Sub btnPdfHf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdfHf.Click
        DocumentStatus = 0
        Class1.killProcessOnUser()
        GeneratePurchaseOrder()

    End Sub
    Public Sub GeneratePurchaseOrder()
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
        oTable3.Add(rng, 1, 6, missing, missing)
        rng.Font.Name = "Calibri"

        'First Header Order Agreement
        Dim newRowa1 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.ParagraphFormat.SpaceBefore = 0
        newRowa1.Range.Font.Size = 18
        newRowa1.Borders.Enable = 0
        newRowa1.Cells(1).Range.Text = "ORDER AGREEMENT"
        newRowa1.Cells(1).Range.Underline = True
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

        newRowa1.Cells(1).Width = 480
        newRowa1.Range.Font.Bold = True


        ' blank line generate 
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.ParagraphFormat.SpaceBefore = 0
        newRowa1.Range.ParagraphFormat.SpaceAfter = 0

        newRowa1.Range.Font.Size = 2
        newRowa1.Borders.Enable = 0
        newRowa1.Cells(1).Width = 240
        newRowa1.Cells(2).Width = 240
        newRowa1.Cells(1).Range.Underline = False
        ' First Line of Purchase supplier
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = "PO NO. :" + txtPOrefNo.Text
        newRowa1.Cells(2).Range.Text = "DATE " + txtPoDate.Text
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa1.Cells(1).Width = 240
        newRowa1.Cells(2).Width = 240


        ' Second Line of Purchase supplier
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = "OFFER NO:" + txtOfferNo.Text
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Width = 240
        newRowa1.Cells(2).Width = 240



        'Third Line of Purchase supplier header
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = "SUPPLIER :"
        newRowa1.Cells(1).Range.Underline = True
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(2).Range.Text = "PURCHASER :"
        newRowa1.Cells(2).Range.Underline = True
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone




        ' Company name Line of Purchase supplier header
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(2).Range.Underline = False
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = txtSuplName.Text
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(2).Range.Text = txtPurchaseCmnyName.Text
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        ' Company name Line of Purchase supplier header
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = txtSuplName.Text
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(2).Range.Text = txtPurchase1.Text + "/" + txtPurchase2.Text
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone


        ' Company address   Line of Purchase supplier header
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = txtSuplAddr1.Text
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(2).Range.Text = txtPurchase3.Text + "/" + txtPurchase4.Text
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone


        ' Company address 2   Line of Purchase supplier header
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = txtSuplAddr2.Text
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(2).Range.Text = txtPurcAddr1.Text
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone



        ' Company address 3   Line of Purchase supplier header
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = txtSupltelePhone.Text
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(2).Range.Text = txtPurcAddress2.Text
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone



        ' Company Purchase  Telephone 2   Line of Purchase supplier header
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = ""
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(2).Range.Text = txtPurTelephone.Text
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        ' Company Grid Start Of purchase order
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = "NO"
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

        newRowa1.Cells(2).Range.Text = "PARTICULER"
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(3).Range.Text = "Qty"
        newRowa1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(4).Range.Text = "Rate"
        newRowa1.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(5).Range.Text = "Amount"
        newRowa1.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Width = 30
        newRowa1.Cells(2).Width = 240
        newRowa1.Cells(3).Width = 50
        newRowa1.Cells(4).Width = 75
        newRowa1.Cells(5).Width = 85

        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa1.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(3).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(3).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa1.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(4).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(4).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa1.Cells(5).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(5).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(5).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(5).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle



        For i = 0 To gvPurchaseDetail.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(gvPurchaseDetail.Rows(i).Cells(1).Value)
            If IsTicked Then

                If gvPurchaseDetail.Rows(i).Cells("Model").Value.ToString().Trim() <> "" Then
                    newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                    newRowa1.Range.Font.Size = 10
                    newRowa1.Range.Font.Bold = True
                    newRowa1.Cells(1).Range.Text = Convert.ToString(i + 1)
                    newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    newRowa1.Cells(2).Range.Text = "Plant -" + gvPurchaseDetail.Rows(i).Cells("Plant").Value.ToString()
                    newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    newRowa1.Cells(3).Range.Text = gvPurchaseDetail.Rows(i).Cells("Qty").Value.ToString() + "Nos."
                    newRowa1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    newRowa1.Cells(4).Range.Text = "Rs" + gvPurchaseDetail.Rows(i).Cells("Price").Value.ToString() + "/-"
                    newRowa1.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    newRowa1.Cells(5).Range.Text = "Rs" + gvPurchaseDetail.Rows(i).Cells("Total").Value.ToString() + "/-"
                    newRowa1.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                    newRowa1.Cells(2).Range.Text = "Model -" + gvPurchaseDetail.Rows(i).Cells("Model").Value.ToString()
                    newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone


                    newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                    newRowa1.Cells(2).Range.Text = "Capacity -" + gvPurchaseDetail.Rows(i).Cells("Capacity").Value.ToString()
                    newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone

                    If gvPurchaseDetail.Rows(i).Cells("Scheme").Value.ToString().Trim() <> "" Then
                        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                        newRowa1.Cells(2).Range.Text = "Scheme: " + gvPurchaseDetail.Rows(i).Cells("Scheme").Value.ToString()
                        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    End If

                    If gvPurchaseDetail.Rows(i).Cells("Scope_of_Supply").Value.ToString().Trim() <> "" Then
                        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                        newRowa1.Cells(2).Range.Text = "CLIENT SCOPE: " + gvPurchaseDetail.Rows(i).Cells("Scope_of_Supply").Value.ToString()
                        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    End If

                Else

                    newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)

                    newRowa1.Range.Font.Size = 10
                    newRowa1.Range.Font.Bold = True
                    newRowa1.Cells(1).Range.Text = Convert.ToString(i + 1)
                    newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    newRowa1.Cells(2).Range.Text = gvPurchaseDetail.Rows(i).Cells("Plant").Value.ToString()
                    newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    newRowa1.Cells(3).Range.Text = gvPurchaseDetail.Rows(i).Cells("Qty").Value.ToString() + "Nos."
                    newRowa1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    newRowa1.Cells(4).Range.Text = "Rs. " + gvPurchaseDetail.Rows(i).Cells("Price").Value.ToString() + "/-"
                    newRowa1.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                    newRowa1.Cells(5).Range.Text = "Rs. " + gvPurchaseDetail.Rows(i).Cells("Total").Value.ToString() + "/-"
                    newRowa1.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                End If
            End If

        Next

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = "FINAL AMOUNT ==>>"
        newRowa1.Cells(2).Range.Text = "Rs. " + txtFinalTotal.Text + "/."

        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        newRowa1.Cells(1).Width = 395
        newRowa1.Cells(2).Width = 85

        newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        If txtDicount.Text <> "" Then
            newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa1.Range.Font.Size = 10
            newRowa1.Borders.Enable = 0
            newRowa1.Range.Font.Bold = True
            newRowa1.Cells(1).Range.Text = "SPECIAL DISCOUNT = = >>"
            newRowa1.Cells(2).Range.Text = "Rs. " + txtDicount.Text + "/."

            newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            newRowa1.Cells(1).Width = 395
            newRowa1.Cells(2).Width = 85

            newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa1.Range.Font.Size = 10
            newRowa1.Borders.Enable = 0
            newRowa1.Range.Font.Bold = True

            Dim Final As Decimal
            Final = 0
            Final = Convert.ToDecimal(txtFinalTotal.Text) - (Convert.ToDecimal(txtDicount.Text))
            newRowa1.Cells(1).Range.Text = "FINAL COST OF MINERAL WATER PLANT = = = > > >"
            newRowa1.Cells(2).Range.Text = "Rs. " + Final.ToString() + "/."

            newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            newRowa1.Cells(1).Width = 395
            newRowa1.Cells(2).Width = 85

            newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle



        End If


        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = "ADVANCE: "
        newRowa1.Cells(2).Range.Text = txtAdvance.Text
        newRowa1.Cells(3).Range.Text = "/-("
        newRowa1.Cells(4).Range.Text = txtAdvanceText.Text
        newRowa1.Cells(5).Range.Text = ")"


        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        newRowa1.Cells(1).Width = 70

        newRowa1.Cells(2).Width = 110
        newRowa1.Cells(2).Range.Underline = True

        newRowa1.Cells(3).Width = 25
        newRowa1.Cells(4).Width = 200
        newRowa1.Cells(4).Range.Underline = True
        newRowa1.Cells(5).Width = 80



        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Cells(1).Range.Text = "CH.NO: "
        newRowa1.Cells(2).Range.Text = txtCno.Text
        newRowa1.Cells(3).Range.Text = "DATE:"
        newRowa1.Cells(4).Range.Text = txtcdate.Text
        newRowa1.Cells(5).Range.Text = "BANK:"
        newRowa1.Cells(6).Range.Text = txtBankName.Text


        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(3).Range.Underline = False
        newRowa1.Cells(5).Range.Underline = False

        newRowa1.Cells(2).Range.Underline = True
        newRowa1.Cells(4).Range.Underline = True
        newRowa1.Cells(6).Range.Underline = True

        newRowa1.Cells(1).Width = 60
        newRowa1.Cells(2).Width = 100
        newRowa1.Cells(3).Width = 60
        newRowa1.Cells(4).Width = 100
        newRowa1.Cells(5).Width = 60
        newRowa1.Cells(6).Width = 100



        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Cells(1).Width = 240
        newRowa1.Cells(2).Width = 240
        newRowa1.Cells(1).Range.Text = "TERMS AND CONDITION:"
        newRowa1.Cells(1).Range.Underline = True
        newRowa1.Cells(2).Range.Text = "E & O U:"
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

        For i = 0 To GvTermsAndcondition.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTermsAndcondition.Rows(i).Cells(1).Value)
            If IsTicked Then

                newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa1.Cells(1).Width = 30
                newRowa1.Cells(2).Width = 450
                newRowa1.Cells(1).Range.Underline = False
                newRowa1.Cells(2).Range.Underline = False
                newRowa1.Cells(2).Range.Bold = False
                newRowa1.Cells(1).Range.Bold = False
                newRowa1.Borders.Enable = 0
                newRowa1.Range.Font.Size = 10
                newRowa1.Cells(1).Range.Text = GvTermsAndcondition.Rows(i).Cells("Sr_No").Value.ToString()
                newRowa1.Cells(2).Range.Text = GvTermsAndcondition.Rows(i).Cells("Description").Value.ToString()
                newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Else
            End If
        Next

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)

        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(2).Range.Underline = False
        newRowa1.Cells(2).Range.Bold = True
        newRowa1.Cells(1).Range.Bold = True
        newRowa1.Cells(1).Range.Text = "FOR INDIAN ION EXCHANGE"
        newRowa1.Cells(2).Range.Text = "M/s." + txtPurchaseCmnyName.Text
        newRowa1.Cells(1).Width = 300
        newRowa1.Cells(2).Width = 180

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(2).Range.Underline = False

        newRowa1.Cells(1).Range.Text = "& CHEMICALS LIMITED"
        newRowa1.Cells(2).Range.Text = ""
        newRowa1.Cells(1).Width = 300
        newRowa1.Cells(2).Width = 180

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = txtsuppl1.Text
        newRowa1.Cells(2).Range.Text = txtPurchase1.Text
        newRowa1.Cells(1).Width = 300
        newRowa1.Cells(2).Width = 180

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = txtsupp2.Text
        newRowa1.Cells(2).Range.Text = txtPurchase2.Text
        newRowa1.Cells(1).Width = 300
        newRowa1.Cells(2).Width = 180

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = txtsupp3.Text
        newRowa1.Cells(2).Range.Text = txtPurchase3.Text

        newRowa1.Cells(1).Width = 300
        newRowa1.Cells(2).Width = 180

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders.Enable = 0
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = "DATE  :" + chkdate.Text
        newRowa1.Cells(2).Range.Text = txtPurchase4.Text
        newRowa1.Cells(1).Width = 300
        newRowa1.Cells(2).Width = 180



        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders.Enable = 0
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = "PALACE  : " + txtpalace.Text
        newRowa1.Cells(1).Width = 480

        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg2.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next



        Else




        End If


        objDoc.Tables(1).Rows.First.Range.Font.Size = 2




        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document
        wordDocument1 = New Word.Document
        wordApplication1 = New Word.Application
        Dim formating1 As Object
        Dim fullpath12 As String
        fullpath12 = OrderAgreementTempPath + "\" + txtPOrefNo.Text.Replace("/", " ").Trim() + "-" + txtOfferNo.Text.Trim() + "-" + txtPurchaseCmnyName.Text.Trim() + "-" + txtPoDate.Text.Replace("/", "-") + " PA" + ".doc"


        objDoc.SaveAs(fullpath12)
        Dim Targets1 As Object = OrderAgreementTempPath + "\" + txtPOrefNo.Text.Replace("/", " ").Trim() + "-" + txtOfferNo.Text.Trim() + "-" + txtPurchaseCmnyName.Text.Trim() + "-" + txtPoDate.Text.Replace("/", "-") + " PA" + ".pdf"
        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)

        wordDocument1.Close()
        wordDocument1 = Nothing
        wordApplication1.Quit()
        wordApplication1 = Nothing

        objDoc.Close()
        objDoc = Nothing
        objApp.Quit()
        objApp = Nothing

        MessageBox.Show("Purchase Letter Ready !")
        System.Diagnostics.Process.Start(Targets1)

    End Sub
    Private Sub btnPdfWf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdfWf.Click
        DocumentStatus = 1
        GeneratePurchaseOrder()
    End Sub



    Private Sub GvPurchaseSearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvPurchaseSearch.DoubleClick
        Try
            con1.Close()

        Catch ex As Exception

        End Try
        BtnPosave.Text = "Update"
        PurchaseId = Convert.ToInt32(Me.GvPurchaseSearch.SelectedCells(0).Value)

        Display()
    End Sub
    Public Sub Display()
        Dim str As String
        Try
            con1.Open()

            str = "select * from Purchase_Quatation where Pk_ID=" & PurchaseId & ""
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            txtPOrefNo.Text = dr("Po_RefNo").ToString()
            txtOfferNo.Text = dr("OfferNo").ToString()
            txtPoDate.Text = dr("PoDate").ToString()
            txtSuplName.Text = dr("SupplierComapnyName").ToString()
            txtsuppl1.Text = dr("Supplier1").ToString()
            txtsupp2.Text = dr("Supplier2").ToString()
            txtsupp3.Text = dr("Supplier3").ToString()
            txtSuplAddr1.Text = dr("Supp_addr1").ToString()
            txtSuplAddr2.Text = dr("Supp_addr2").ToString()
            txtSupltelePhone.Text = dr("Supp_tele").ToString()
            txtPurchaseCmnyName.Text = dr("Purchase_CompanyName").ToString()
            txtPurchase1.Text = dr("Purchase1").ToString()
            txtPurchase2.Text = dr("Purchase2").ToString()
            txtPurchase3.Text = dr("Purchase3").ToString()
            txtPurchase4.Text = dr("Purchase4").ToString()
            txtPurcAddr1.Text = dr("Purch_addr1").ToString()
            txtPurcAddress2.Text = dr("Purch_addr2").ToString()

            txtPurTelephone.Text = dr("Purch_Telephone").ToString()
            txtCno.Text = dr("ChequeNo").ToString()
            txtcdate.Text = dr("ChequeDate").ToString()
            txtBankName.Text = dr("BankName").ToString()
            txtAdvance.Text = dr("Advance").ToString()
            txtAdvanceText.Text = dr("AdvanceText").ToString()
            txtDicount.Text = dr("Discount").ToString()

            cmd.Dispose()
            dr.Dispose()
            TotalPrice = 0

            Gv_GetTechnicalData()
            Total1()
            con1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub
    Public Sub Gv_GetTechnicalData()
        Dim str1 As String
        Dim da123 As New SqlDataAdapter
        Dim ds123 As New DataSet
        Dim dt12 As New DataTable

        dt12.Columns.Add("Remove", GetType(Boolean))
        dt12.Columns.Add("Select", GetType(Boolean))
        dt12.Columns.Add("Sr_No", GetType(String))
        dt12.Columns.Add("Plant", GetType(String))
        dt12.Columns.Add("Model", GetType(String))
        dt12.Columns.Add("Qtype", GetType(String))
        dt12.Columns.Add("Capacity", GetType(String))
        dt12.Columns.Add("Qty", GetType(String))
        dt12.Columns.Add("Price", GetType(String))
        dt12.Columns.Add("Scheme", GetType(String))
        dt12.Columns.Add("Scope_of_supply", GetType(String))
        dt12.Columns.Add("Total", GetType(String))

        str1 = "select *  from Purchase_data where Fk_PurchaseId=" & PurchaseId & ""
        da123 = New SqlDataAdapter(str1, con1)
        ds123 = New DataSet()
        da123.Fill(ds123)

        For S1 = 0 To ds123.Tables(0).Rows.Count - 1

            Dim imagestatus As Int16
            imagestatus = 0
            If ds123.Tables(0).Rows(S1)("IsSelected") = "Yes" Then
                imagestatus = 1
            End If
            dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("Sr_No").ToString(), ds123.Tables(0).Rows(S1)("Plant").ToString(), ds123.Tables(0).Rows(S1)("Model").ToString(), ds123.Tables(0).Rows(S1)("Qtype").ToString(), ds123.Tables(0).Rows(S1)("Capacity").ToString(), ds123.Tables(0).Rows(S1)("Qty").ToString(), ds123.Tables(0).Rows(S1)("Price").ToString(), ds123.Tables(0).Rows(S1)("Scheme").ToString(), ds123.Tables(0).Rows(S1)("Scope_of_supply").ToString(), ds123.Tables(0).Rows(S1)("Total").ToString())
        Next
        gvPurchaseDetail.DataSource = dt12
        txtSrno.Text = Convert.ToString(dt12.Rows.Count + 1)
    End Sub

    Private Sub txtQty_LocationChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.LocationChanged

    End Sub

    Private Sub txtQty_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.Leave
        Try

            If IsNumeric(txtQty.Text) Then
            Else
                txtQty.Text = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtPrice_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice.Leave
        Try

            If IsNumeric(txtPrice.Text) Then
                If IsNumeric(txtQty.Text) Then
                    txtTotal.Text = Convert.ToString(Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQty.Text))
                Else
                    txtQty.Text = ""
                End If
            Else
                txtPrice.Text = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtTotal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotal.Leave
        Try

            If IsNumeric(txtTotal.Text) Then
            Else
                txtTotal.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btndeletequotation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeletequotation.Click
        Try
            con1.Open()

            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete Quotation?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then

                If Me.GvPurchaseSearch.SelectedRows.Count > 0 Then

                    Dim delete As String = "delete from Purchase_Quatation  where Pk_ID=" & GvPurchaseSearch.SelectedRows(0).Cells(0).Value & ""
                    cmd = New SqlCommand(delete, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    Dim delete1 As String = "delete from Purchase_data   Fk_PurchaseId =" & GvPurchaseSearch.SelectedRows(0).Cells(0).Value & ""
                    cmd = New SqlCommand(delete1, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    MessageBox.Show("Delete Purchase Quotation Successfully..")
                End If
            End If
        Catch ex As Exception
        End Try
        con1.Close()
        Gv_GetPurchaseAggrementData()
    End Sub

    Private Sub btnSearchCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompany.Click
        Dim str1 As String
        Dim da123 As New SqlDataAdapter
        Dim ds123 As New DataSet
        Dim dt12 As New DataTable


        dt12.Columns.Add("Pk_ID", GetType(String))
        dt12.Columns.Add("Po_RefNo", GetType(String))
        dt12.Columns.Add("OfferNo", GetType(String))
        dt12.Columns.Add("SupplierComapnyName", GetType(String))
        dt12.Columns.Add("Purchase_CompanyName", GetType(String))

        str1 = "select Pk_ID,Po_RefNo,OfferNo,SupplierComapnyName,Purchase_CompanyName from Purchase_Quatation where  OfferNo  like  '%" + txtCompnaysearch.Text.Trim() + "%'"
        da123 = New SqlDataAdapter(str1, con1)
        ds123 = New DataSet()
        da123.Fill(ds123)

        For S1 = 0 To ds123.Tables(0).Rows.Count - 1

            dt12.Rows.Add(ds123.Tables(0).Rows(S1)("Pk_ID").ToString(), ds123.Tables(0).Rows(S1)("Po_RefNo").ToString(), ds123.Tables(0).Rows(S1)("OfferNo").ToString(), ds123.Tables(0).Rows(S1)("SupplierComapnyName").ToString(), ds123.Tables(0).Rows(S1)("Purchase_CompanyName").ToString())
        Next
        GvPurchaseSearch.DataSource = dt12
        da123.Dispose()
        dt12.Dispose()
        ds123.Dispose()

        Dim tt As Int32
        tt = GvPurchaseSearch.Rows.Count()

    End Sub

    Private Sub txtDicount_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDicount.Leave
        Try

            If IsNumeric(txtDicount.Text) Then
            Else
                txtDicount.Text = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub frmpurchaseAgreement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

    Private Sub GvPurchaseSearch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvPurchaseSearch.CellContentClick

    End Sub
End Class