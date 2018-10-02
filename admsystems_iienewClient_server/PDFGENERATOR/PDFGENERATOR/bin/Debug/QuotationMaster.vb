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

Public Class QuotationMaster

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
    Shared Path11 As String
    Public UserID As Int32
    Public QuationId As Int32
    Public QPath As String
    Shared DocumentStatus As Int16
    Dim appPath As String
    Dim lines As String
    Public Sub New()
        InitializeComponent()
        con1 = Class1.con

        appPath = Path.GetDirectoryName(Application.ExecutablePath)
        GetTechnicalData()
        ddlEnqType_Bind()
        GetKind_SubData()
        GetBuss_Name()
        GvQuotationSearch_Bind()
        QPath = Class1.global.QPath
        PicSignature.ImageLocation = Class1.global.Signature
        txtDesignation.Text = Class1.global.Designation
        txtUserName.Text = Class1.global.UserName
        txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        txtLatterDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")

        PicSignature.ImageLocation = Class1.global.Signature


    End Sub

    Public Sub ddlEnqType_Bind()


        Dim str As String
        str = "select * from Enq_Type"
        da = New SqlDataAdapter(str, con1)
        ds = New DataSet()
        da.Fill(ds)
        ddlEnqType.DataSource = ds.Tables(0)
        ddlEnqType.DisplayMember = "EnqType"
        ddlEnqType.ValueMember = "Code"
        da.Dispose()
        ds.Dispose()

    End Sub

    Public Sub GetTechnicalData()
        Dim desg As String

        txtContent1.AutoCompleteCustomSource.Clear()



        If (txtCapacity1.Text.Trim() <> "") Then
            desg = "select * from Category_Master where Capacity='" & txtCapacity1.Text & "'"
            da = New SqlDataAdapter(desg, con1)
            ds = New DataSet()
            da.Fill(ds)
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtContent1.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
            Next
            da.Dispose()
            ds.Dispose()
        End If

    End Sub
    Public Sub GetBuss_Name()
        Dim enqtype As String
        enqtype = "select Designation,FirstName+ ' '+ LastName as BName from User_Master"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows


            txtBuss_Name.AutoCompleteCustomSource.Add(dr1.Item("BName").ToString())
        Next
    End Sub
    Public Sub GvQuotationSearch_Bind()

        Dim str As String
        str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master"
        da = New SqlDataAdapter(str, con1)
        ds = New DataSet()
        da.Fill(ds)
        GvCategorySearch.DataSource = ds.Tables(0)
        Dim tt As Int32
        tt = GvCategorySearch.Rows.Count()

        txtTotalRecord.Text = tt.ToString()

        da.Dispose()
        ds.Dispose()
    End Sub

    Public Sub GetKind_SubData()
        Dim enqtype As String
        enqtype = "select distinct KindAtt,Subject,Name,QType,Quot_Type from Quotation_Master"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtType.AutoCompleteCustomSource.Add(dr1.Item("QType").ToString())
            txtQoutType.AutoCompleteCustomSource.Add(dr1.Item("Quot_Type").ToString())
            txtSearchName.AutoCompleteCustomSource.Add(dr1.Item("Name").ToString())
            txtName.AutoCompleteCustomSource.Add(dr1.Item("Name").ToString())
            txtKind.AutoCompleteCustomSource.Add(dr1.Item("KindAtt").ToString())
            txtSub.AutoCompleteCustomSource.Add(dr1.Item("Subject").ToString())
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
                    txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
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
                    txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
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
                    txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity1.ToString() + " LITER/DAY" & Newline & "CAPACITY: " + txtCapacity2.Text + "  LITER/HR . . . . " + totalcapacity2.ToString() + " LITER/DAY  ").ToString()
                End If
            End If
            GetTechnicalData()
            GvMultiple_Bind()
        End If

    End Sub

    Public Sub GvSingle_Bind()

        If txtNoContent.Text.Trim() <> "" Then
            dt = New DataTable()
            If RblSingle.Checked = True Then

                dt.Columns.Add("Remove", GetType(Boolean))
                dt.Columns.Add("Select", GetType(Boolean))
                dt.Columns.Add("SrNo", GetType(Int32))
                dt.Columns.Add("Category", GetType(String))
                dt.Columns.Add("Price", GetType(String))
                dt.Columns.Add("Tax", GetType(String))

            ElseIf RblOther.Checked = True Then
                dt.Columns.Add("Remove", GetType(Boolean))
                dt.Columns.Add("Select", GetType(Boolean))
                dt.Columns.Add("SrNo", GetType(Int32))
                dt.Columns.Add("Category", GetType(String))
                dt.Columns.Add("Qty", GetType(String))
                dt.Columns.Add("Price", GetType(String))
                dt.Columns.Add("Total", GetType(String))
                dt.Columns.Add("Tax", GetType(String))

            End If

            Dim str As String
            str = "select * from Category_Master where Capacity  ='" & txtCapacity1.Text & "'"
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)


            If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContent.Text) Then
                For i As Integer = 0 To Convert.ToInt32(txtNoContent.Text) - 1
                    If RblSingle.Checked = True Then
                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), ds.Tables(0).Rows(i)("Price").ToString(), "0")

                    ElseIf RblOther.Checked = True Then
                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), "1", ds.Tables(0).Rows(i)("Price").ToString(), ds.Tables(0).Rows(i)("Price").ToString(), "0")
                    End If

                Next
                GvTechnical.DataSource = dt

                da.Dispose()
                ds.Dispose()
                Total1()
            Else
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If RblSingle.Checked = True Then
                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), ds.Tables(0).Rows(i)("Price").ToString(), "0")

                    ElseIf RblOther.Checked = True Then

                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), "1", ds.Tables(0).Rows(i)("Price").ToString(), ds.Tables(0).Rows(i)("Price").ToString(), "0")
                    End If
                Next
                GvTechnical.DataSource = dt
                da.Dispose()
                ds.Dispose()
                Total1()
            End If

        End If


    End Sub

    Public Sub GvMultiple_Bind()
        If txtNoContent.Text.Trim() <> "" Then
            dt = New DataTable()
            If RblMultiple.Checked = True Then
                dt.Columns.Add("Remove", GetType(Boolean))
                dt.Columns.Add("Select", GetType(Boolean))
                dt.Columns.Add("SrNo", GetType(Int32))
                dt.Columns.Add("Category", GetType(String))
                dt.Columns.Add("Price1", GetType(String))
                dt.Columns.Add("Price2", GetType(String))
                dt.Columns.Add("Tax", GetType(String))
            End If
            Dim str As String
            Dim str1 As String

            str = "select * from Category_Master where Capacity  ='" & txtCapacity1.Text & "'"
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)


            str1 = "select * from Category_Master where Capacity  ='" & txtCapacity2.Text & "'"
            da1 = New SqlDataAdapter(str1, con1)
            ds1 = New DataSet()
            da1.Fill(ds1)

            If ds.Tables(0).Rows.Count = ds1.Tables(0).Rows.Count Then

                If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContent.Text) Then
                    For i As Integer = 0 To Convert.ToInt32(txtNoContent.Text) - 1
                        If RblMultiple.Checked = True Then
                            dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "0")
                        End If

                    Next
                    GvTechnical.DataSource = dt
                    da.Dispose()
                    ds.Dispose()
                    Total1()
                Else
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If RblMultiple.Checked = True Then
                            dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "0")
                        End If
                    Next
                    GvTechnical.DataSource = dt
                    da.Dispose()
                    ds.Dispose()
                    Total1()
                End If
            Else
                MessageBox.Show("Technical Data Not Match")

            End If
        End If
    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim t As Int16
        t = dt.Rows.Count
        t = t + 1
        If RblSingle.Checked = True Then
            dt.Rows.Add(0, 0, lblSno.Text, txtContent1.Text, txtPrice_11.Text, "0")
        ElseIf RblOther.Checked = True Then
            dt.Rows.Add(0, 0, lblSno.Text, txtContent1.Text, txtPrice_11.Text, txtPrice_21.Text, txtPrice_31.Text, "0")
        ElseIf RblMultiple.Checked = True Then
            dt.Rows.Add(0, 0, lblSno.Text, txtContent1.Text, txtPrice_21.Text, txtPrice_11.Text, "0")
        End If


        GvTechnical.DataSource = dt
        Total1()
        txtContent1.Text = ""
        txtPrice_11.Text = ""
        txtPrice_21.Text = ""
        txtPrice_31.Text = ""
        lblSno.Text = ""


    End Sub


    Private Sub txtContent1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContent1.Leave
        Dim str As String
        Dim ds1 As DataSet
        Dim da1 As SqlDataAdapter

        If txtContent1.Text.Trim() <> "" Then


            If RblSingle.Checked = True Or RblOther.Checked = True Or RblMultiple.Checked = True Then
                str = "select * from Category_Master where Category='" + txtContent1.Text.Trim() + "' and Capacity='" + txtCapacity1.Text.Trim() + "'"
                da1 = New SqlDataAdapter(str, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)
                If (ds1.Tables(0).Rows.Count > 0) Then


                    If RblSingle.Checked = True Then
                        txtPrice_11.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblSno.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                    ElseIf RblOther.Checked = True Then
                        txtPrice_21.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblSno.Text = ds1.Tables(0).Rows(0)("SNo").ToString()

                    ElseIf RblMultiple.Checked = True Then
                        Dim da2 As SqlDataAdapter
                        Dim ds2 As DataSet

                        str = "select * from Category_Master where Category='" + txtContent1.Text.Trim() + "' and Capacity='" + txtCapacity2.Text.Trim() + "'"
                        da2 = New SqlDataAdapter(str, con1)
                        ds2 = New DataSet()
                        da2.Fill(ds2)
                        'Capacity 1'
                        If (ds1.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtPrice_21.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                                lblSno.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                            End If
                        End If
                        'capacity 2'

                        If (ds2.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtPrice_11.Text = ds2.Tables(0).Rows(0)("Price").ToString()

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

                If RblSingle.Checked = True Then
                    total = total + Convert.ToDecimal(GvTechnical.Rows(i).Cells(4).Value)
                    txtTotal.Text = total.ToString()
                    txtTotal1.Visible = False

                ElseIf RblOther.Checked = True Then
                    total = total + Convert.ToDecimal(GvTechnical.Rows(i).Cells(6).Value)
                    txtTotal.Text = total.ToString()
                    txtTotal1.Visible = False

                ElseIf RblMultiple.Checked = True Then

                    total = total + Convert.ToDecimal(GvTechnical.Rows(i).Cells(4).Value)
                    txtTotal.Text = total.ToString()
                    total1 = total1 + Convert.ToDecimal(GvTechnical.Rows(i).Cells(5).Value)
                    txtTotal1.Text = total1.ToString()
                    txtTotal1.Visible = True

                End If


            End If
        Next



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

    Private Sub btnSave1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave1.Click
        If PicDefault.Image Is Nothing Then

            MessageBox.Show("Please Select Default Image...")
        Else


            Dim str As String
            Dim techinical12 As String
            Dim QMaxId As Int32

            Try
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

                str = "insert into Quotation_Master (QType,Fk_EnqTypeID,Quot_No,Q_Year,Enq_No,Ref,Quot_Type,Name,Address,Capacity_Type,Capacity_Single,Capacity_Multiple,KindAtt,Subject,Buss_Excecutive,Buss_Name,Later_Description,Later_Date,Capacity_Word,UserName,DefaultImage,QDate) values('" + txtType.Text + "','" & ddlEnqType.SelectedValue & "'," & QuotationMaxId & "," & year1 & ",'" + txtEnqNo.Text + "','" + txtRef.Text + "','" + txtQoutType.Text + "','" + txtName.Text + "','" + txtAddress.Text + "', '" + capacityType.ToString() + "','" + txtCapacity1.Text + "','" + txtCapacity2.Text + "','" + txtKind.Text + "','" + txtSub.Text + "','" + ddlBussines_Executive.SelectedItem + "','" + txtBuss_Name.Text + "','" + txtDescription.Text + "','" + txtLatterDate.Text + "','" + txtCapacityWord.Text + "','" + Class1.global.UserName.ToString() + "','" + PicDefault.ImageLocation.ToString() + "','" + txtDate.Text + "')"
                cmd = New SqlCommand(str, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()

                Dim mm As String
                mm = "select Max(Pk_QuotationID) as QMax from Quotation_Master"
                cmd = New SqlCommand(mm, con1)
                dr = cmd.ExecuteReader()
                dr.Read()
                QMaxId = dr("QMax").ToString()
                cmd.Dispose()
                dr.Dispose()
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
                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage) values(" & QMaxId & ",'" + GvTechnical.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(4).Value.ToString() + "','" + status + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage) values(" & QMaxId & ",'" + GvTechnical.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(6).Value.ToString() + "','" + status + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage) values(" & QMaxId & ",'" + GvTechnical.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnical.Rows(i).Cells(5).Value.ToString() + "','" + status + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If
                    End If
                Next

                con1.Close()
                GvQuotationSearch_Bind()
                MessageBox.Show("Successfully Submit .....")

            Catch ex As Exception

            End Try

        End If

    End Sub


    Public Sub Pdf_Creation()
        Dim _pdfforge As New PDF.PDF
        Dim _pdftext As New PDF.PDFText
        'Dim html1 As New HtmlDocument


        Dim latter1 As String

        Dim hDoc As HtmlDocument
        Dim hCol As HtmlElementCollection
        Dim filename As String





        'Dim emailText As [String] = [String].Format(File.ReadAllText("C:\PDFGENERATOR\PDFGENERATOR\latterpage.txt"), txtRef.Text, txtLatterDate.Text, txtName.Text, txtAddress.Text, txtKind.Text, txtSub.Text, txtDescription.Text, txtCapacity1.Text, txtCapacityWord.Text)
        'Dim sr As New System.IO.StreamReader("C:\PDFGENERATOR\PDFGENERATOR\latterpage.txt")
        'Dim line1 As String
        'line1 = ""

        'While sr.Peek() <> -1
        '    ' line1 = line1 + sr.ReadLine()

        'End While

        _pdftext.Text = Now.Date.ToShortDateString
        _pdftext.FontColorRed = 0
        _pdftext.FontColorGreen = 0
        _pdftext.FontColorBlue = 0
        _pdftext.FontName = "Arial.ttf"
        _pdftext.FontPath = "C:\Windows\Fonts\"
        _pdftext.FontSize = 12
        '_pdftext.Rotation = 0
        _pdftext.XPosition = 100
        _pdftext.YPosition = 50
        _pdftext.FillOpacity = 1.0

        _pdfforge.AddTextToPDFFile("base.pdf", "step1.pdf", 1, 1, _pdftext)
        '_pdftext.Text = TextBox2.Text
        '_pdftext.FontColorRed = 0
        '_pdftext.FontColorGreen = 0
        '_pdftext.FontColorBlue = 0
        '_pdftext.FontName = "Arial.ttf"
        '_pdftext.FontPath = "C:\Windows\Fonts\"
        '_pdftext.FontSize = 30
        '_pdftext.Rotation = 0
        '_pdftext.XPosition = 0
        '_pdftext.YPosition = 0
        '_pdftext.FillOpacity = 1.0
        '_pdfforge.AddTextToPDFFile("base.pdf", "step2.pdf", 1, 1, (_pdftext))

        If RblSingle.Checked Then

            Dim str(1) As String
            str(0) = "250LPH1.jpg"
            str(1) = "250LPH1.jpg"
            Dim i As Integer
            i = _pdfforge.Images2PDF(str, "step3.pdf", 0)

        End If



        If RblMultiple.Checked Then

            Dim str(1) As String
            str(0) = "250LPH2.jpg"
            str(1) = "250LPH2.jpg"
            Dim i As Integer
            i = _pdfforge.Images2PDF(str, "step3.pdf", 0)

        End If



        If RblOther.Checked Then

            Dim str(1) As String
            str(0) = "250LPH3.jpg"
            str(1) = "250LPH3.jpg"
            Dim i As Integer
            i = _pdfforge.Images2PDF(str, "step3.pdf", 0)

        End If

        Dim files(1) As String

        files(0) = "C:\PDFGENERATOR\PDFGENERATOR\bin\Debug\step1.pdf"
        'files(1) = "C:\PDFGENERATOR\PDFGENERATOR\bin\Debug\step2.pdf"
        files(1) = "C:\PDFGENERATOR\PDFGENERATOR\bin\Debug\step3.pdf"


        _pdfforge.MergePDFFiles(files, "C:\PDFGENERATOR\PDFGENERATOR\bin\Debug\final.pdf", False)




        'Dim _clspdfcreator As New PDFCreator.clsPDFCreator

        '_clspdfcreator.cOptions.UseAutosave = 1
        '_clspdfcreator.cOptions.AutosaveDirectory = "C:\Users\shrenik\Desktop\Work\ADMS\Projects\pdfgenerator\PDFGENERATOR\PDFGENERATOR\bin\Debug"
        '_clspdfcreator.cOptions.AutosaveFilename = "final.pdf"

        '_clspdfcreator.cAddPrintjob("step1")
        '_clspdfcreator.cAddPrintjob("step2")
        '_clspdfcreator.cAddPrintjob("step3")
        '_clspdfcreator.cCombineAll()
        '_clspdfcreator.cPrinterStop = False
        'While _clspdfcreator.cCountOfPrintjobs <> 0

        'End While
        '_clspdfcreator.cPrinterStop = True
        MsgBox("document is ready")
    End Sub

    Private Sub txtEnqType_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim quotNo As String

        con1.Open()




    End Sub

    Private Sub ddlEnqType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEnqType.SelectionChangeCommitted

        Dim str1 As String

        Try


            str1 = "SELECT Quot_No FROM   Quotation_Master where Fk_EnqTypeID='" & ddlEnqType.SelectedValue & "' order by Pk_QuotationID desc "
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
        Dim EnqMax As Int16
        Dim str As String

        If (txtEnqNo.Text.Trim() <> "") Then

            con1.Open()
            EnqMax = 0


            Dim year1 As Int32
            year1 = Convert.ToInt32(txtDate.Text.Substring(txtDate.Text.Length - 2))

            str = "select count(Enq_No) as TotalCount from Quotation_Master where Enq_No='" & txtEnqNo.Text & "' and Q_Year=" & year1 & " "
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
        PicDefault.ImageLocation = imgSrc
        'txtPhoto1.Text = imgSrc
        'Path1 = openFileDialog1.FileName
    End Sub

    Private Sub txtName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.Leave
        Dim str As String
        con1.Close()
        con1.Open()
        str = "select Address from Quotation_Master where Name='" & txtName.Text & "'"
        cmd = New SqlCommand(str, con1)
        dr = cmd.ExecuteReader()
        If (dr.HasRows) Then
            dr.Read()
            If (dr("Address").ToString() <> "") Then
                txtAddress.Text = dr("Address").ToString()
            End If
        End If
        cmd.Dispose()
        dr.Dispose()
        con1.Close()



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




            'Dim total As Decimal
            'Dim total1 As Decimal

            'total = 0
            'total1 = 0

            'tax_status()


            'If RblSingle.Checked = True Or RblOther.Checked = True Then
            '    If txtTotal.Text.Trim() <> "" And txtTax.Text.Trim() <> "" Then
            '        total = (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtTax.Text)) / 100
            '        lblTax1.Text = total.ToString()

            '        lblTax1.Visible = True
            '        lblFinalAmount1.Visible = True

            '        lblFinalAmount1.Text = Convert.ToDecimal(txtTotal.Text) + total


            '    End If
            'Else
            '    If txtTotal.Text.Trim() <> "" And txtTax.Text.Trim() <> "" And txtTotal1.Text.Trim() Then

            '        total = (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtTax.Text)) / 100
            '        lblTax1.Text = total.ToString()

            '        lblFinalAmount1.Text = Convert.ToDecimal(txtTotal.Text) + total


            '        total1 = (Convert.ToDecimal(txtTotal1.Text) * Convert.ToDecimal(txtTax.Text)) / 100
            '        lblTax2.Text = total1.ToString()

            '        lblFinalAmount2.Text = Convert.ToDecimal(txtTotal1.Text) + total1
            '        lblTax1.Visible = True
            '        lblTax2.Visible = True
            '        lblFinalAmount1.Visible = True
            '        lblFinalAmount2.Visible = True
            '    End If

            'End If




            Total1()
        End If

    End Sub

    Private Sub GvTechnical_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvTechnical.CellEndEdit

        If RblOther.Checked = True Then


            If e.ColumnIndex = 4 Then

                GvTechnical.Rows(e.RowIndex).Cells(6).Value = Convert.ToDecimal(GvTechnical.Rows(e.RowIndex).Cells(4).Value) * Convert.ToDecimal(GvTechnical.Rows(e.RowIndex).Cells(5).Value)

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
                GvTechnical.Rows(e.RowIndex).Cells(4).Value = finalamount.ToString()
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
                rate1 = Convert.ToDecimal(Me.GvTechnical.Rows(e.RowIndex).Cells(5).Value.ToString())
                tax = (rate1 * Convert.ToInt32(GvTechnical.Rows(e.RowIndex).Cells(7).Value)) / 100
                finalamount = rate1 + tax
                GvTechnical.Rows(e.RowIndex).Cells(5).Value = finalamount.ToString()
                GvTechnical.Rows(e.RowIndex).Cells(6).Value = Convert.ToDecimal(GvTechnical.Rows(e.RowIndex).Cells(4).Value) * Convert.ToDecimal(GvTechnical.Rows(e.RowIndex).Cells(5).Value)
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
                GvTechnical.Rows(e.RowIndex).Cells(4).Value = finalamount1.ToString()
                rate2 = Convert.ToDecimal(Me.GvTechnical.Rows(e.RowIndex).Cells(5).Value.ToString())
                tax2 = (rate2 * Convert.ToInt32(GvTechnical.Rows(e.RowIndex).Cells(6).Value)) / 100
                finalamount2 = rate2 + tax2
                GvTechnical.Rows(e.RowIndex).Cells(5).Value = finalamount2.ToString()
            End If
        End If
    End Sub

    Protected Sub FinalDucumetation()


        lines = ""
        ''''''''JIGAR MISTRI'''''
        Dim objApp As Word.Application
        Dim objDoc As Word.Document
        'Open new instance
        objApp = New Word.Application
        objDoc = New Word.Document
        Dim oTable As Word.Table

        Dim rng As Word.Range = objDoc.Range(0, 0)
        oTable = objDoc.Tables.Add(Range:=rng, NumRows:=29, NumColumns:=1)

        oTable.Range.ParagraphFormat.SpaceAfter = 3
        oTable.Cell(1, 1).Range.Text = "Ref :  " + txtRef.Text + "                                                      Date:" + txtDate.Text + " "
        oTable.Cell(1, 1).Range.Font.Name = "Times New Roman"
        oTable.Cell(3, 1).Range.Text = "TO,"
        oTable.Cell(3, 1).Range.Font.Name = "Times New Roman"

        'Dim rng As Word.Range = objDoc1.Range(0, 0)
        rng.Font.Name = "Times New Roman"

        For ikf = 1 To 29
            oTable.Cell(ikf, 1).Range.Font.Name = "Times New Roman"
        Next

        oTable.Cell(4, 1).Range.Text = txtName.Text + " "


        oTable.Cell(5, 1).Range.Text = txtAddress.Text + " "


        oTable.Cell(6, 1).Range.Text = ""

        If txtKind.Text.Trim() <> "" Then
            oTable.Cell(7, 1).Range.Text = "Kind Att :" + txtKind.Text + " "

        End If
        oTable.Cell(8, 1).Range.Text = "SUB:" + txtSub.Text + " "

        oTable.Cell(9, 1).Range.Text = " "
        oTable.Cell(10, 1).Range.Text = "Dear Sir,"


        oTable.Cell(11, 1).Range.Text = ""
        oTable.Cell(12, 1).Range.Text = txtDescription.Text


        oTable.Cell(13, 1).Range.Text = " In this regard, we are submitting herewith our offer for following capacities:"



        oTable.Cell(14, 1).Range.Text = " "
        oTable.Cell(14, 1).Range.Font.Size = 8

        oTable.Cell(15, 1).Range.Text = txtCapacityWord.Text

        oTable.Cell(16, 1).Range.Font.Size = 8
        oTable.Cell(16, 1).Range.Text = " "

        oTable.Cell(17, 1).Range.Text = "The detailed offer is as per attached INDEX and ANNEXURES."



        oTable.Cell(18, 1).Range.Text = "We hope that you will find the above in line with your requirement."

        oTable.Cell(19, 1).Range.Text = "Kindly contact us for any further information / clarification require in this matter."




        oTable.Cell(20, 1).Range.Text = "We shall be glad to furnish the same."
        oTable.Cell(21, 1).Range.Text = "Thanking you and anticipating a favourable reply."
        oTable.Cell(22, 1).Range.Text = " "
        oTable.Cell(23, 1).Range.Text = "Yours faithfully,"
        oTable.Cell(24, 1).Range.Text = ""

        oTable.Cell(25, 1).Range.Text = "For, INDIAN ION EXCHANGE"
        oTable.Cell(26, 1).Range.Text = "     & CHEMICALS LTD."
        oTable.Cell(27, 1).Range.Text = ""

        oTable.Cell(28, 1).Range.Text = txtUserName.Text + "                                                  DR. BHAVIN VYAS"
        oTable.Cell(29, 1).Range.Text = "(" + txtDesignation.Text + ")                                                (TECH.DIRECTOR)"


        oTable.Cell(7, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        oTable.Cell(8, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        oTable.Cell(15, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


        oTable.Rows.Item(1).Range.Font.Bold = True
        oTable.Rows.Item(2).Range.Font.Bold = True
        oTable.Rows.Item(3).Range.Font.Bold = True
        oTable.Rows.Item(4).Range.Font.Bold = True
        oTable.Rows.Item(5).Range.Font.Bold = True
        oTable.Rows.Item(6).Range.Font.Bold = True
        oTable.Rows.Item(15).Range.Font.Bold = True

        oTable.Rows.Item(7).Range.Font.Bold = True
        oTable.Rows.Item(8).Range.Font.Bold = True 'sub'

        ' oTable.Rows.Item(12).Range.Font.Bold = True 'name'
        oTable.Rows.Item(26).Range.Font.Bold = True 'designation'
        oTable.Rows.Item(25).Range.Font.Bold = True
        oTable.Rows.Item(28).Range.Font.Bold = True 'chemical'
        oTable.Rows.Item(29).Range.Font.Bold = True 'Indian' 

        oTable.Rows.Item(1).Range.Font.Size = 12
        oTable.Rows.Item(3).Range.Font.Size = 16
        oTable.Rows.Item(4).Range.Font.Size = 16
        oTable.Rows.Item(5).Range.Font.Size = 16
        oTable.Rows.Item(8).Range.Font.Size = 14
        oTable.Rows.Item(6).Range.Font.Size = 16
        oTable.Rows.Item(7).Range.Font.Size = 14
        oTable.Rows.Item(15).Range.Font.Size = 16

        oTable.Rows.Item(28).Range.Font.Size = 14 'name'
        oTable.Rows.Item(29).Range.Font.Size = 14 'designation'

        oTable.Rows.Item(7).Range.Underline = True
        oTable.Rows.Item(8).Range.Underline = True
        'ection.HeadersFooters.Add(New HeaderFooter(document, HeaderFooterType.FooterDefault, New Paragraph(document, New Picture(document, "MyImage.png"))))


        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage)


                headerRange.InlineShapes.AddPicture(appPath + "\HeaderRo.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight


            Next


            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange, Word.WdFieldType.wdFieldPage)
                footerrange.Text = "Corporate Office : D-11,First Floor, Diamond Park, G.I.D.C.,Naroda, Ahmedabad-382330, Gujarat (India) Tele Fax : 91-079-22819065/67/68            export@indianionexchange.com            www.indianionexchange.com          www.bottlingindia.com"
                footerrange.Font.Size = 8
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        End If

        Dim format As Object = Word.WdSaveFormat.wdFormatPDF



        objDoc.SaveAs(appPath + "\Letter1.doc")


        objDoc.Close()
        objDoc = Nothing
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

        wordDocument1 = wordApplication1.Documents.Open(paramSourceDocPath1)


        Dim formating1 As Object
        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        wordDocument1.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        wordDocument1.Close()
        wordDocument1 = Nothing
        wordApplication1.Quit()
        wordApplication1 = Nothing








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

        Dim selectSrNo = GetImage.TrimEnd(",")
        If selectSrNo.Length > 0 Then


            Dim str12 As String
            Dim da33 As New SqlDataAdapter
            Dim ds33 As New DataSet


            str12 = "select Photo1,Photo2,Photo3 from Category_Master where SNo IN(" & selectSrNo & ") and Capacity='" + txtCapacity1.Text.Trim() + "'"
            da33 = New SqlDataAdapter(str12, con1)
            ds33 = New DataSet
            da33.Fill(ds33)
            Dim totalimage As Int32
            totalimage = Convert.ToInt32(ds.Tables(0).Rows.Count) * 3

            Dim str
            ReDim str(5)
            Dim p As Int32
            p = 0
            str(p) = PicDefault.ImageLocation.ToString()

            p = 1
            ReDim Preserve str(p)

            For x As Integer = 0 To ds33.Tables(0).Rows.Count - 1


                If ds33.Tables(0).Rows(x)("Photo1").ToString() <> "" Then
                    str(p) = ds33.Tables(0).Rows(x)("Photo1").ToString()
                    p = p + 1
                    ReDim Preserve str(p)
                End If

                If ds33.Tables(0).Rows(x)("Photo2").ToString() <> "" Then
                    str(p) = ds33.Tables(0).Rows(x)("Photo2").ToString()
                    p = p + 1
                    ReDim Preserve str(p)
                End If

                If ds33.Tables(0).Rows(x)("Photo3").ToString() <> "" Then
                    str(p) = ds33.Tables(0).Rows(x)("Photo3").ToString()
                    p = p + 1
                    ReDim Preserve str(p)
                End If

            Next
            ReDim Preserve str(p - 1)
            Dim i As Integer
            i = _pdfforge.Images2PDF(str, appPath + "\step3.pdf", 0)
        End If

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
                    dt33.Rows.Add(GvTechnical.Rows(t1).Cells(2).Value.ToString(), GvTechnical.Rows(t1).Cells(3).Value.ToString(), GvTechnical.Rows(t1).Cells(4).Value.ToString(), GvTechnical.Rows(t1).Cells(5).Value.ToString(), GvTechnical.Rows(t1).Cells(6).Value.ToString())

                End If
            Next
        End If



        If RblMultiple.Checked = True Then
            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price1")
            dt33.Columns.Add("Peice2")
            For t1 As Integer = 0 To GvTechnical.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnical.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnical.Rows(t1).Cells(2).Value.ToString(), GvTechnical.Rows(t1).Cells(3).Value.ToString(), GvTechnical.Rows(t1).Cells(4).Value.ToString(), GvTechnical.Rows(t1).Cells(5).Value.ToString())

                End If
            Next
        End If



        Dim objApp1 As Word.Application
        Dim objDoc1 As Word.Document

        objApp1 = New Word.Application
        objDoc1 = New Word.Document
        Dim oTable2 As Word.Table
        'objDoc1.Selection.TypeText("Refzxczxxczccxxxxxxxxxxxxcxcxcccxcxcxcxcxccxxcxccxccxcxcc")
        Dim rng1 As Word.Range = objDoc1.Range(0, 0)
        rng1.Font.Name = "Times New Roman"
        ' oTable2.Range.ParagraphFormat.SpaceAfter = 3
        oTable2 = objDoc1.Tables.Add(Range:=rng1, NumRows:=dt33.Rows.Count, NumColumns:=1)

        '   Dim newRow1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        oTable2.Cell(1, 1).Range.Text = "PROJECT COST"

        oTable2.Cell(1, 1).Range.Font.Color = Word.WdColor.wdColorWhite
        oTable2.Cell(1, 1).Shading.BackgroundPatternColor = Word.WdColor.wdColorDarkBlue
        oTable2.Rows.Item(1).Range.Font.Name = "Times New roman"
        oTable2.Rows.Item(1).Range.Font.Bold = True 'Indian' 
        oTable2.Rows.Item(1).Range.Font.Size = 16
        oTable2.Rows.Item(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

        ' Dim newRow2 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        oTable2.Cell(2, 1).Range.Text = "PRICE : MINERAL WATER PLANT COMPLETE WITH DESIGN, ENGINEERING & SUPPLY"
        oTable2.Rows.Item(2).Range.Font.Bold = True 'Indian' 
        oTable2.Rows.Item(2).Range.Font.Size = 12
        oTable2.Rows.Item(2).Range.Font.Color = Word.WdColor.wdColorDarkBlue
        oTable2.Rows.Item(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        '  newRow2.HeadingFormat = 2

        Dim newRow3 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        If RblSingle.Checked = True Then
            oTable2.Rows.Item(3).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
            oTable2.Cell(3, 1).Range.Text = "SR.NO.                     DESCRIPTION                 PRICE(IN Lacs)"
            oTable2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            oTable2.Rows.Item(3).Range.Font.Size = 10

            oTable2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
            oTable2.Rows.Item(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If
        If RblOther.Checked = True Then
            oTable2.Rows.Item(3).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
            oTable2.Cell(3, 1).Range.Text = "SR.NO.                     DESCRIPTION                PRICE(IN Lacs)            Qty            Total"
            oTable2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            oTable2.Rows.Item(3).Range.Font.Size = 10
            oTable2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite

            oTable2.Rows.Item(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If
        If RblMultiple.Checked = True Then
            oTable2.Rows.Item(3).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
            oTable2.Cell(3, 1).Range.Text = "SR.NO.                      DESCRIPTION                PRICE1            PRICE2"
            oTable2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            oTable2.Rows.Item(3).Range.Font.Size = 10
            oTable2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
            oTable2.Rows.Item(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If
        Dim finaltotal As New Decimal
        Dim finaltotal1 As New Decimal
        Dim qty As Integer
        qty = 0
        finaltotal = 0
        For i = 4 To dt33.Rows.Count + 4 - 1

            If RblSingle.Checked = True Then
                Dim newRow43 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                'Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                'newRow.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                'newRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                'newRow.Cells(1).Range.Text = dt33.Rows(i)(0).ToString() + "                      " + dt33.Rows(i)(1).ToString() + "                     " + dt33.Rows(i)(2).ToString()
                'newRow.Cells(1).Range.Font.Size = 10
                'newRow.Cells(1).Range.Font.Color = Word.WdColor.wdColorDarkBlue
                'newRow.Range.Font.Bold = 0
                oTable2.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                oTable2.Rows.Item(i).Range.Font.Bold = 0
                oTable2.Cell(i, 1).Range.Text = dt33.Rows(i - 4)(0).ToString() + "                      " + dt33.Rows(i - 4)(1).ToString() + "                     " + dt33.Rows(i - 4)(2).ToString()
                oTable2.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                oTable2.Rows.Item(i).Range.Font.Size = 10
                oTable2.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue
                oTable2.Rows.Item(i).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i - 4)(2).ToString())



            End If
            If RblOther.Checked = True Then
                Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                'newRow.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                'newRow.Range.Font.Bold = 0
                'newRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                oTable2.Cell(i, 1).Range.Text = dt33.Rows(i - 4)(0).ToString() + "                      " + dt33.Rows(i - 4)(1).ToString() + "                     " + dt33.Rows(i - 4)(3).ToString() + "                        " + dt33.Rows(i - 4)(2).ToString() + "            " + dt33.Rows(i - 4)(4).ToString()
                'newRow.Cells(1).Range.Font.Size = 10
                'newRow.Cells(1).Range.Font.Color = Word.WdColor.wdColorDarkBlue
                oTable2.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                oTable2.Rows.Item(i).Range.Font.Bold = 0
                'oTable2.Cell(i, 1).Range.Text = dt33.Rows(i - 4)(0).ToString() + "                      " + dt33.Rows(i - 4)(1).ToString() + "                     " + dt33.Rows(i - 4)(2).ToString()
                oTable2.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                oTable2.Rows.Item(i).Range.Font.Size = 10
                oTable2.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue
                oTable2.Rows.Item(i).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i - 4)(3).ToString())
                qty = qty + Convert.ToDecimal(dt33.Rows(i - 4)(2).ToString())
                finaltotal1 = finaltotal + Convert.ToDecimal(dt33.Rows(i - 4)(4).ToString())

            End If
            If RblMultiple.Checked = True Then
                Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                'newRow.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                'newRow.Range.Font.Bold = 0
                'newRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                'newRow.Cells(1).Range.Text = dt33.Rows(i)(0).ToString() + "                      " + dt33.Rows(i)(1).ToString() + "                     " + dt33.Rows(i)(2).ToString() + "               " + dt33.Rows(i)(3).ToString()
                'newRow.Cells(1).Range.Font.Size = 10
                'newRow.Cells(1).Range.Font.Color = Word.WdColor.wdColorDarkBlue

                oTable2.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                oTable2.Rows.Item(i).Range.Font.Bold = 0
                oTable2.Cell(i, 1).Range.Text = dt33.Rows(i - 4)(0).ToString() + "                      " + dt33.Rows(i - 4)(1).ToString() + "                     " + dt33.Rows(i - 4)(2).ToString() + "               " + dt33.Rows(i - 4)(3).ToString()
                oTable2.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                oTable2.Rows.Item(i).Range.Font.Size = 10
                oTable2.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue
                oTable2.Rows.Item(i).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i - 4)(2).ToString())
                finaltotal1 = finaltotal + Convert.ToDecimal(dt33.Rows(i - 4)(3).ToString())
            End If
        Next
        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
            newRow4.Cells(1).Range.Text = "                    TOTAL PRICE  :                       " + Convert.ToString(finaltotal) + "                      "
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(1).Range.Font.Size = 12
            newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


        End If
        If RblOther.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
            newRow4.Cells(1).Range.Text = "                     TOTAL PRICE  :                       " + Convert.ToString(finaltotal) + "                      " + Convert.ToString(qty) + "               " + Convert.ToString(finaltotal1)
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(1).Range.Font.Size = 12
            newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If
        If RblMultiple.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
            newRow4.Cells(1).Range.Text = "                       TOTAL PRICE  :                       " + Convert.ToString(finaltotal) + "                      " + Convert.ToString(finaltotal1)
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(1).Range.Font.Size = 12
            newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If

        '    newRow4.HeadingFormat = 3

        Dim newRow5 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow5.Shading.BackgroundPatternColor = Word.WdColor.wdColorDarkBlue
        newRow5.Cells(1).Range.Text = "TERMS AND CONDITIONS "
        newRow5.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow5.Cells(1).Range.Font.Size = 16
        newRow5.Cells(1).Range.Font.Name = "Times New Roman"
        newRow5.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite



        '    newRow5.HeadingFormat = 3
        newRow5.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        Dim newRow6 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow6.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
        newRow6.Cells(1).Range.Text = ">> PRICES ARE EX. FACTORY NARODA, AHMEDABAD."
        newRow6.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow6.Cells(1).Range.Font.Size = 10
        newRow6.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow6.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


        Dim newRow7 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow7.Cells(1).Range.Text = ">> VAT EXTRA AT ACTUAL AT THE TIME OF DELIVERY."
        newRow7.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow7.Cells(1).Range.Font.Size = 10
        newRow7.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow7.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft



        Dim newRow8 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow8.Cells(1).Range.Text = ">> FORWARDING, TRANSPORTATION, OCTROI & INSURANCE SHALL BE EXTRA" +
            "AT ACTUAL AT THE TIME OF DELIVERY."
        newRow8.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow8.Cells(1).Range.Font.Size = 10
        newRow8.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow8.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft



        Dim newRow9 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow9.Cells(1).Range.Text = ">> ERECTION & COMMISSIONING : EXTRA AT ACTUAL"
        newRow9.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow9.Cells(1).Range.Font.Size = 10
        newRow9.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow9.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft




        Dim newRow10 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow10.Cells(1).Range.Text = "      -- Necessary Tools, Tackles, Labour & Chemical should be provided by the client."
        newRow10.Range.Font.Bold = 0
        newRow10.Cells(1).Range.Font.Size = 10

        newRow10.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


        Dim newRow11 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow11.Cells(1).Range.Text = ">> PAYMENT TERMS :"
        newRow11.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow11.Cells(1).Range.Font.Size = 10
        newRow11.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow9.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


        Dim newRow12 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow12.Cells(1).Range.Text = "     -- 40% Advance alongwith Techno-Commercial clear order &"
        newRow12.Range.Font.Bold = 0
        newRow12.Cells(1).Range.Font.Size = 10
        newRow12.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft



        Dim newRow13 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow13.Cells(1).Range.Text = "     -- 60% Balance against Performa Invoice before despatch."
        newRow13.Range.Font.Bold = 0
        newRow13.Cells(1).Range.Font.Size = 10

        newRow13.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        Dim newRow14 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow14.Cells(1).Range.Text = ">> DELIVERY :"
        newRow14.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow14.Cells(1).Range.Font.Size = 10
        newRow14.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow14.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        '  newRow14.Range.Paragraphs.SpaceAfter = 1


        Dim newRow15 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow15.Cells(1).Range.Text = "     -- Within 3-4 weeks after receipt of your order with advance."
        newRow15.Cells(1).Range.Font.Size = 10
        newRow15.Range.Font.Bold = 0
        newRow15.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft



        Dim newRow16 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow16.Cells(1).Range.Text = "( NB : Guarantee clause is valid for Mfg. Defect/Workmanship Defect only,"
        newRow16.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow16.Cells(1).Range.Font.Size = 10
        newRow16.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow16.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


        Dim newRow17 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow17.Cells(1).Range.Text = "      Our liability is limited to repair or replace of the same. )"
        newRow17.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow17.Cells(1).Range.Font.Size = 10
        newRow17.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow17.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft



        Dim newRow18 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
        newRow18.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow18.Cells(1).Range.Font.Size = 10
        newRow18.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow18.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft



        Dim newRow19 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow19.Cells(1).Range.Text = "For, INDIAN ION EXCHANGE"
        newRow19.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow19.Cells(1).Range.Font.Size = 12
        newRow19.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow19.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft




        Dim newRow20 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow20.Cells(1).Range.Text = "& CHEMICALS LTD."
        newRow20.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow20.Cells(1).Range.Font.Size = 12
        newRow20.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow20.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft



        Dim newRow22 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow22.Cells(1).Range.Text = txtUserName.Text + "                                                                   DR. BHAVIN VYAS"
        newRow22.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow22.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        'rng1.SetRange = objDoc1.Range(rng1.End, rng1.End)
        'oTable.Cell(28, 1).Range.Text = txtUserName.Text + "                                                                   DR. BHAVIN VYAS"



        Dim newRow21 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        newRow21.Cells(1).Range.Text = "(" + txtDesignation.Text + ")                                                                        (TECH.DIRECTOR)"
        newRow21.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


        objDoc1.SaveAs(appPath + "\Letter2.doc")

        objDoc1.Close()
        objDoc1 = Nothing
        objApp1.Quit()
        objApp1 = Nothing
        Dim exportFormat As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing As Object = Type.Missing
        Dim wordApplication As Word.Application
        Dim wordDocument As Word.Document
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
        wordApplication.Quit()
        wordApplication.NormalTemplate.Saved = True
        wordApplication = Nothing






        ' objDoc.SaveAs(Path.GetDirectoryName(Application.ExecutablePath) & "\Letter1.pdf", format)
        Dim files(2) As String
        files(0) = appPath + "\Letter1.pdf"


        files(1) = appPath + "\step3.pdf"
        files(2) = appPath + "\Letter2.pdf"


        Dim fullpath12 As String
        fullpath12 = QPath + "\final.pdf"
        _pdfforge.MergePDFFiles(files, fullpath12, False)
        MessageBox.Show("Document Ready !")



    End Sub


    Private Sub GvCategorySearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCategorySearch.DoubleClick

        con1.Close()

        QuationId = Convert.ToInt32(Me.GvCategorySearch.SelectedCells(0).Value)
        Display()

    End Sub
    Public Sub Display()

        Dim str As String
        Try
            con1.Open()

            str = "select * from Quotation_Master where Pk_QuotationID=" & QuationId & ""
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            txtType.Text = dr("QType").ToString()
            txtQoutNo.Text = dr("Fk_EnqTypeID").ToString() + dr("Quot_No").ToString()
            txtEnqNo.Text = dr("Enq_No").ToString()
            txtRef.Text = dr("Ref").ToString()
            txtQoutType.Text = dr("Quot_Type").ToString()
            txtName.Text = dr("Name").ToString()
            txtAddress.Text = dr("Address").ToString()
            If dr("Capacity_Type").ToString() = "Single" Then
                RblSingle.Checked = True
                txtCapacity1.Text = dr("Capacity_Single").ToString()
            End If
            If dr("Capacity_Type").ToString() = "Other" Then
                RblOther.Checked = True
                txtCapacity1.Text = dr("Capacity_Single").ToString()

            End If

            If dr("Capacity_Type").ToString() = "Multiple" Then
                RblMultiple.Checked = True
                txtCapacity1.Text = dr("Capacity_Single").ToString()
                txtCapacity2.Text = dr("Capacity_Multiple").ToString()

            End If

            txtKind.Text = dr("KindAtt").ToString()
            txtSub.Text = dr("Subject").ToString()
            ddlBussines_Executive.SelectedItem = dr("Buss_Excecutive").ToString()
            txtBuss_Name.Text = dr("Buss_Name").ToString()
            txtDescription.Text = dr("Later_Description").ToString()
            txtLatterDate.Text = dr("Later_Date").ToString()
            txtCapacityWord.Text = dr("Capacity_Word").ToString()
            txtUserName.Text = dr("UserName").ToString()
            PicDefault.ImageLocation = dr("DefaultImage").ToString()

            cmd.Dispose()
            dr.Dispose()

            Gv_GetTechnicalData()



            con1.Close()
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
            str1 = "select  SNo,TechnicalData,Price1,DocumentationImage from Technical_Data where Fk_QuotationID=" & QuationId & ""
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
            GvTechnical.DataSource = dt12
            GvTechnical.ReadOnly = True
            btnAdd.Visible = False
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
            dt12.Columns.Add("Qty", GetType(String))
            dt12.Columns.Add("Price", GetType(String))
            dt12.Columns.Add("Total", GetType(String))

            str1 = "select  SNo,TechnicalData,Price1,Qty,Price2,DocumentationImage from Technical_Data where Fk_QuotationID=" & QuationId & ""
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
            GvTechnical.DataSource = dt12
            GvTechnical.ReadOnly = True
            btnAdd.Visible = False
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
            str1 = "select  SNo,TechnicalData,Price1,Price2,DocumentationImage from Technical_Data where Fk_QuotationID=" & QuationId & ""
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
            GvTechnical.DataSource = dt12
            GvTechnical.ReadOnly = True
            btnAdd.Visible = False
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

                Pre_str = "select * from Quotation_Master where Fk_EnqTypeID='" + ddlEnqType.SelectedValue + "' and Quot_No=" & txtPreviousQuatation.Text & ""
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
            Catch ex As Exception

            End Try
            Gv_GetTechnicalData()
            con1.Close()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim str As String
        str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master where Enq_No='" + txtSearchEnQ.Text + "' or Name='" + txtSearchName.Text + "'  "
        da = New SqlDataAdapter(str, con1)
        ds = New DataSet()
        da.Fill(ds)
        GvCategorySearch.DataSource = ds.Tables(0)
        Dim tt As Int32
        tt = GvCategorySearch.Rows.Count()

        txtTotalRecord.Text = tt.ToString()

        da.Dispose()
        ds.Dispose()
    End Sub

    Private Sub ddlBussines_Executive_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBussines_Executive.SelectionChangeCommitted

        If ddlBussines_Executive.SelectedItem = "TELEPHONIC" Then

            Dim desc As String
            desc = "Thank you for your Telecon with our Sales executive     for Mineral Water Plant  regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."

            txtDescription.Text = desc.ToString()


        End If
        If ddlBussines_Executive.SelectedItem = "MAIL" Then

            Dim desc As String
            desc = "This refers to your mail dated " + txtLatterDate.Text + " regarding your subject requirement. We thank you very much for your enquiry and indeed appreciated your interest in range of our products."

            txtDescription.Text = desc.ToString()
        End If
        If ddlBussines_Executive.SelectedItem = "VISIT NARODA FACTORY" Then

            Dim desc As String
            desc = "We thank you very much for paying your visit at our Naroda Factory and personal discussion you had with our Sales Executive " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " in regards to subject matter. Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."

            txtDescription.Text = desc.ToString()


        End If

        If ddlBussines_Executive.SelectedItem = "PERSONAL VISIT " Then

            Dim desc As String
            Dim Newline As String
            Newline = System.Environment.NewLine
            desc = "The courtesy and consideration extended to our Director Mr. J. B. Vyas during his personal visit at your office of the date to discussed regarding subject matter, are sincerely appreciated. We thank you very much for sparing your valued time for the discussion and showing interest in range of our products." + Newline + " " + Newline + " We thank you very much for sparing your valuable time for personal discussion you had with our  Sales Executive " + txtBuss_Name.Text + " on " + txtLatterDate.Text + " regarding your subject requirement are indeed appreciated. We thank you very much for sparing your valued time for the discussion and showing interest in range of our products."
            txtDescription.Text = desc.ToString()


        End If

    End Sub

    Private Sub txtLatterDate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLatterDate.Leave
        ddlBussines_Executive_SelectionChangeCommitted(Nothing, Nothing)

    End Sub

    Private Sub txtBuss_Name_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuss_Name.Leave
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
        GvQuotationSearch_Bind()
    End Sub


    Private Sub btnHF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHF.Click


        Me.UseWaitCursor = True

        DocumentStatus = 0
        FinalDucumetation()
        Me.UseWaitCursor = False
        SetClean()

    End Sub



    Private Sub btnWf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWf.Click
        Me.UseWaitCursor = True
        DocumentStatus = 1
        FinalDucumetation()
        Me.UseWaitCursor = False
        SetClean()
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
        txtType.Text = ""
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



    End Sub

    Private Sub btnAddClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddClear.Click
        SetClean()

        QuationId = 0

        If GvTechnical.Rows.Count > 0 Then
            GvTechnical.DataSource = Null
            btnAdd.Visible = True

        End If



    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GvQuotationSearch_Bind()

    End Sub
End Class

