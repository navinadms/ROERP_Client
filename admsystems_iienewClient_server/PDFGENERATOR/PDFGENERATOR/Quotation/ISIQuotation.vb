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
Imports System.Globalization
Imports System.Threading
Imports System.Data.SqlClient


Public Class ISIQuotation
    Private con1 As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader
    Private da As SqlDataAdapter
    Private ds As DataSet
    Private da1 As SqlDataAdapter
    Private ds1 As DataSet
    Shared dt As DataTable
    Shared Address_ID As Integer
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

    Shared sys3total As Decimal
    Shared sys3mutotal As Decimal

    Shared sys4total As Decimal
    Shared sys4mutotal As Decimal
    Shared TotalLeavevar As Decimal
    Dim EnqMax As Int16
    Dim file1 As String
    Dim file2 As String
    Dim file3 As String

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Dim str
    Public Sub New()
        ''txtQoutType.Font.Name = "Sa"
        InitializeComponent()
        con1 = Class1.con
        LanguageId = 1
        appPath = Path.GetDirectoryName(Application.ExecutablePath)
        BindOnLanguageName()
        RdbEnglish.Checked = True
        GetTechnicalData(LanguageId)
        ddlEnqType_Bind()
        GetKind_SubData()
        GetcheckData()
        txtNoContentSYS1.Text = "5"
        txtNoContentSYS2.Text = "1"
        txtNoContentSYS3.Text = "6"
        txtNoContentSYS4.Text = "6"
        txtDefaultImage.Text = "Mineral Water Turnkey"
        Class1.global.QuatationId = 0
        'QPath = Class1.global.QPath
        'If (Not System.IO.Directory.Exists(QPath + "\TempDocument")) Then
        '    System.IO.Directory.CreateDirectory(QPath + "\TempDocument")
        'End If

        'QtempPath = Class1.global.QPath + "\TempDocument"

        'change navin 07-05-2015

        QPath = Class1.global.QPath
        If (Not System.IO.Directory.Exists(appPath + "\TempDocument")) Then
            System.IO.Directory.CreateDirectory(appPath + "\TempDocument")
        End If
        QtempPath = appPath + "\TempDocument"


        PicSignature.ImageLocation = Class1.global.Signature
        txtDesignation.Text = Class1.global.Designation
        txtUserName.Text = Class1.global.UserName
        txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        txtLatterDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        PicSignature.SizeMode = PictureBoxSizeMode.StretchImage
        PicSignature.ImageLocation = Class1.global.Signature
        PicSignature.SizeMode = PictureBoxSizeMode.StretchImage
        TxtTax.Text = "0"
        ' BindEnqType()

        'getPageRight()
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
            dataView.RowFilter = "([DetailName] like 'ISI Quatation')"
            If (dataView.Count > 0) Then


                dv = dataView.ToTable()

                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            btnAddClear.Enabled = True
                            btnSave1.Enabled = True
                        Else
                            btnAddClear.Enabled = False
                            btnSave1.Enabled = False
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
    Public Sub GvCategorySearch_For_SalesExecutive_Bind()

        If rblNew.Checked = True Then
            Dim data = linq_obj.SP_Get_SalesExecutiveQuotation_List().ToList().Where(Function(p) p.ToID = Class1.global.UserID And p.QuotationType = "ISI" And p.Status = "Pending").ToList()
            Dim dt As New DataTable
            Dim status As Boolean
            dt.Columns.Add("Fk_SalesExecutiveQtnID")
            dt.Columns.Add("EnqNo")
            dt.Columns.Add("From")
            dt.Columns.Add("Status")
            dt.Columns.Add("Priority")
            For Each item As SP_Get_SalesExecutiveQuotation_ListResult In data
                dt.Rows.Add(item.PK_SalesExecutiveQtnID, item.EnqNo, item.FromUser, item.Status, item.Priority)
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
    Public Sub Gv_Single__System3_System_4_SalesExecuvtive_Bind(ByVal strSys As String, ByVal Fk_SalesExecutiveQtnID As Integer)
        If txtCapacity1.Text.Trim() <> "" Then
            LanguageId = 1
            Dim StrArray() As String
            If strSys = "System 3" Then
                If txtNoContentSYS3.Text.Trim() <> "" Then

                    If RblSingle.Checked = True Then
                        dt = New DataTable()
                        dt.Columns.Add("Remove", GetType(Boolean))
                        dt.Columns.Add("Select", GetType(Boolean))
                        dt.Columns.Add("SrNo", GetType(Int32))
                        dt.Columns.Add("Category", GetType(String))
                        dt.Columns.Add("Price", GetType(String))
                        dt.Columns.Add("Tax", GetType(String))
                        dt.Columns.Add("Capacity", GetType(String))

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
                        dt.Columns.Add("Capacity", GetType(String))
                    End If


                    Dim str As String
                    str = "select  SA.SNo,CM.Category,SA.Capacity,CM.Price from SalesExecutive_Technical_Data (NOLOCK) AS SA inner join Category_Master as CM on CM.SNo=SA.SNo where SA.MainCategory='" + strSys + "'  and CM.MainCategory='" + strSys + "' and SA.Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & "  ORDER BY SA.SNo"
                    da = New SqlDataAdapter(str, con1)
                    ds = New DataSet()
                    da.Fill(ds)


                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS3.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS3.Text) - 1


                            If RblSingle.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text, ds.Tables(0).Rows(i)("Capacity").ToString())
                            ElseIf RblOther.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text, ds.Tables(0).Rows(i)("Capacity").ToString())
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
                        If strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt

                            '  lblsNosys3.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                            ' lblSrNOSys4.Text = (dt.Rows.Count + 1).ToString()
                        End If
                        'GvTechnicalSYS1.DataSource = dt

                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                            If RblSingle.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text, ds.Tables(0).Rows(i)("Capacity").ToString())
                            ElseIf RblOther.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text.Trim(), ds.Tables(0).Rows(i)("Capacity").ToString())
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

                        If strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt

                        End If

                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If

                End If

            ElseIf strSys = "System 4" Then
                If txtNoContentSYS4.Text.Trim() <> "" Then
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
                    str = ""
                    If txtExpCapSys1.Text.Trim() = "" Then
                        str = "Select  SA.SNo,CM.Category,CM.Capacity,CM.Price from SalesExecutive_Technical_Data (NOLOCK) AS SA  inner join Category_Master as CM on CM.SNo=SA.SNo where  SA.MainCategory='" + strSys + "' and CM.MainCategory='" + strSys + "' and CM.Capacity  ='" & txtCapacity1.Text & "' and SA.Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & " ORDER BY SA.SNo"
                    End If
                    da = New SqlDataAdapter(str, con1)
                    ds = New DataSet()
                    da.Fill(ds)

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If RblSingle.Checked = True Then
                            StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "")
                            dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)
                        ElseIf RblOther.Checked = True Then
                            StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                            dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text)
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

                    '  lblsNosys2.Text = (dt.Rows.Count + 1).ToString()
                    If strSys = "System 3" Then
                        GvTechnicalSYS3.DataSource = dt

                        ' lblsNosys3.Text = (dt.Rows.Count + 1).ToString()
                    ElseIf strSys = "System 4" Then
                        GvTechnicalSYS4.DataSource = Null
                        GvTechnicalSYS4.DataSource = dt
                        'lblSrNOSys4.Text = (dt.Rows.Count + 1).ToString()
                    End If
                    'GvTechnicalSYS1.DataSource = dt

                    da.Dispose()
                    ds.Dispose()
                    Total1()
                Else



                End If

            End If
        End If




    End Sub

    Public Sub GvMultiple_System3_System4_SalesExecutive_Bind(ByVal strSys As String, ByVal Fk_SalesExecutiveQtnID As Integer)
        Dim StrArray() As String

        If strSys = "System 3" Then

            If txtNoContentSYS3.Text.Trim() <> "" Then

                dt = New DataTable()
                If RblMultiple.Checked = True Then
                    dt.Columns.Add("Remove", GetType(Boolean))
                    dt.Columns.Add("Select", GetType(Boolean))
                    dt.Columns.Add("SrNo", GetType(Int32))
                    dt.Columns.Add("Category", GetType(String))
                    dt.Columns.Add("Price1", GetType(String))
                    dt.Columns.Add("Price2", GetType(String))
                    dt.Columns.Add("Tax", GetType(String))
                    dt.Columns.Add("Capacity", GetType(String))
                End If
                Dim str As String
                Dim str1 As String


                str = "select  SA.SNo,CM.Category,SA.Capacity,CM.Price from SalesExecutive_Technical_Data (NOLOCK) AS SA inner join Category_Master as CM on CM.SNo=SA.SNo where SA.MainCategory='" + strSys + "'  and CM.MainCategory='" + strSys + "' and SA.Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & "  ORDER BY SA.SNo"

                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)


                str1 = "select  SA.SNo,CM.Category,SA.Capacity,CM.Price from SalesExecutive_Technical_Data (NOLOCK) AS SA inner join Category_Master as CM on CM.SNo=SA.SNo where SA.MainCategory='" + strSys + "'  and CM.MainCategory='" + strSys + "' and SA.Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & "  ORDER BY SA.SNo"
                da1 = New SqlDataAdapter(str1, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                If ds.Tables(0).Rows.Count = ds1.Tables(0).Rows.Count Then

                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS3.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS3.Text) - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text, ds1.Tables(0).Rows(i)("Capacity").ToString())
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text, ds.Tables(0).Rows(i)("Capacity").ToString())
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

                        If strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If
                Else
                    'MessageBox.Show("Technical Data " + strSys + " Not Match")

                End If
                da1.Dispose()
                ds1.Dispose()

            End If

        ElseIf strSys = "System 4" Then
            If txtNoContentSYS4.Text.Trim() <> "" Then

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


                str = "Select  SA.SNo,CM.Category,CM.Capacity,CM.Price from SalesExecutive_Technical_Data (NOLOCK) AS SA  inner join Category_Master as CM on CM.SNo=SA.SNo where  SA.MainCategory='" + strSys + "' and CM.MainCategory='" + strSys + "' and CM.Capacity  ='" & txtCapacity1.Text & "' and SA.Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & " ORDER BY SA.SNo"

                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)


                str1 = "Select  SA.SNo,CM.Category,CM.Capacity,CM.Price from SalesExecutive_Technical_Data (NOLOCK) AS SA  inner join Category_Master as CM on CM.SNo=SA.SNo where  SA.MainCategory='" + strSys + "' and CM.MainCategory='" + strSys + "' and CM.Capacity  ='" & txtCapacity1.Text & "' and SA.Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & " ORDER BY SA.SNo"
                da1 = New SqlDataAdapter(str1, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                If ds.Tables(0).Rows.Count = ds1.Tables(0).Rows.Count Then

                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS4.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS4.Text) - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
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

                        If strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
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

                        If strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If
                Else
                    'MessageBox.Show("Technical Data " + strSys + " Not Match")

                End If
                da1.Dispose()
                ds1.Dispose()
            End If
        End If

    End Sub

    Private Sub GvInquery_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetClientDetails_Bind()
    End Sub
    Public Sub GetClientDetails_Bind()

    End Sub
    'Public Sub BindRadioButtoninGroup()
    '    Dim str As String
    '    str = "select * from Language_Master"
    '    da = New SqlDataAdapter(str, con1)
    '    ds = New DataSet()
    '    da.Fill(ds)
    '    Dim ik As Integer
    '    For i = 0 To ds.Tables(0).Rows.Count - 1
    '        Dim Rdb As New RadioButton
    '        Rdb.Name = ds.Tables(0).Rows(i)("ID").ToString()
    '        Rdb.Text = ds.Tables(0).Rows(i)("Language_Name").ToString()
    '        Rdb.Location = New Point(ik, ik)
    '        ik += 50
    '        grpLangaugeBar.Controls.Add(Rdb)
    '    Next
    'End Sub
    Private Sub ChangeLanguage(ByVal lang As String)
        For Each c As Control In Me.Controls
            Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(ISIQuotation))
            resources.ApplyResources(c, c.Name, New CultureInfo(lang))
        Next c
        '        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("fr-FR");
        'System.Threading.Thread.CurrentThread.CurrentCulture = ci;

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
            txt1.Text = "આ રકમ EX ફેક્ટરી નરોડા પ્રમાણે છે."
            txt2.Text = "ગુજરાત ટોલ ટેક્ષ /વેટ ડીલીવરી સમયે જે લાગુ હશે તે પ્રમાણે ભરવાનો રહેશે."
            txt3.Text = "ફોરવર્ડઈંગ,ટ્રાન્સપોર્ટ(અમારી ફેક્ટરીથી આપના સ્થળ સુધી) , જકાત(જો લાગુ પડતી હોય તો) વીમા નો એક્ચુઅલ ચાર્જ ભરવાનો રહેશે."
            txt4.Text = "બાંધકામ & કમીશન : જે લાગુ હશે તે પ્રમાણે ભરવાનો રહેશે."
            txt41.Text = "જરૂરી સાધનો,ટેક્લેશ,મજુરી અને કેમિકલની વ્યવસ્થાની જવાબદારી ગ્રાહકની રહેશે."
            txt42.Text = ""
            txt5.Text = "પૈસાની ચૂકવણીની શરતો"
            txt51.Text = "40% રકમ ઓર્ડર સમયે ચુકવવાની રહેશે અને"
            txt52.Text = "બાકી 60% રકમ બીલ સામે ચુકવવાની રહેશે."
            txt6.Text = "ડીલીવરી"
            txt61.Text = "તમારા ઓર્ડેર રસીદ ની ચૂકવણી ના ૩ થી ૪ અઠવાડિયા ની અંદર."
            txt7.Text = "ગેરન્ટી/ વોરંટી:"
            txt71.Text = "પ્લાન્ટ ને ડીલીવરી ની તારીખ થી ૧૨ મહિના સુધી અમે તમને ગેરંટી આપીએ છે"
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

    End Sub

    Public Sub GetTechnicalData(ByVal Langid As Integer)
        Dim desg As String

        txtContentsys1.AutoCompleteCustomSource.Clear()
        If (txtCapacity1.Text.Trim() <> "") Then
            desg = "select * from Category_Master (NOLOCK) where Capacity='" & txtCapacity1.Text & "' and MainCategory ='System 1' and LanguageId = " & Langid & " ORDER BY SNo"
            da = New SqlDataAdapter(desg, con1)
            ds = New DataSet()
            da.Fill(ds)
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtContentsys1.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
            Next
            da.Dispose()
            ds.Dispose()

        End If


        desg = ""

        txtcontentnosys2.AutoCompleteCustomSource.Clear()
        If (txtCapacity1.Text.Trim() <> "") Then
            desg = "select * from Category_Master (NOLOCK) where Capacity='" & txtCapacity1.Text & "' and MainCategory ='System 2'  and LanguageId = " & Langid & " ORDER BY SNo"
            da = New SqlDataAdapter(desg, con1)
            ds = New DataSet()
            da.Fill(ds)
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtcontentnosys2.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
            Next
            da.Dispose()
            ds.Dispose()

        End If

        desg = ""
        con1 = Class1.con
        txtcontentnosys3.AutoCompleteCustomSource.Clear()
        If (txtCapacity1.Text.Trim() <> "") Then
            desg = "select * from Category_Master (NOLOCK) where  MainCategory ='System 3'  and LanguageId = " & Langid & " ORDER BY SNo"
            da = New SqlDataAdapter(desg, con1)
            ds = New DataSet()
            'add by rajesh 
            ' Try
            If (con1.State = ConnectionState.Closed) Then
                con1.Open()
            End If


            da.Fill(ds)
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtcontentnosys3.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
                txtCapacitySys3.AutoCompleteCustomSource.Add(dr1.Item("Capacity").ToString())
            Next
            da.Dispose()
            ds.Dispose()

            desg = ""

            txtcontentnosys4.AutoCompleteCustomSource.Clear()
            If (txtCapacity1.Text.Trim() <> "") Then
                desg = "select * from Category_Master (NOLOCK) where Capacity='" & txtCapacity1.Text & "' and MainCategory = 'System 4'  and LanguageId = " & Langid & "  ORDER BY SNo"
                da = New SqlDataAdapter(desg, con1)
                ds = New DataSet()
                da.Fill(ds)
                For Each dr1 As DataRow In ds.Tables(0).Rows
                    txtcontentnosys4.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
                Next
                da.Dispose()
                ds.Dispose()

            End If
            con1.Close()
            'add by rajesh
            'Catch ex As Exception
            ' MessageBox.Show(ex.Message)
            'End Try
        End If
    End Sub

    Public Sub GvQuotationSearch_Bind(ByVal LanguageId As Integer)
        Try
            con1 = Class1.con
            con1.Close()
        Catch ex As Exception

        End Try
        Try

            con1.Open()

            Dim str As String
            str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master (NOLOCK) where Quatation_Type = 'ISI' and LanguageId = " & LanguageId & " order by Pk_QuotationID desc"
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()

            da.Fill(ds)
            bindSearchGrid()


            da.Dispose()
            ds.Dispose()
            con1.Close()
        Catch ex As Exception

        End Try

        'con1.Close()
    End Sub

    Public Sub GetKind_SubData()
        Dim enqtype As String
        enqtype = "select distinct KindAtt,Subject,Name,QType,Quot_Type,Buss_Excecutive,Buss_Name,Enq_No from Quotation_Master (NOLOCK) where Quatation_Type = 'ISI'"
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
            txtSearchEnQ.AutoCompleteCustomSource.Add(dr1.Item("Enq_No").ToString())
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
        txtContentsys1.Text = ""

        txtPricemultipleSys1.Text = ""
        txtPriceSingleSys1.Text = ""
        txtPriceothersys1_31.Text = ""

        txtPriceOtherSys2.Text = ""
        txtpriceSinglesys2.Text = ""
        txtpriceMultipleSys2.Text = ""


        txtMultipleSys3.Text = ""
        txtSingleSys3.Text = ""
        txtOtherSys3.Text = ""


        txtOtherSys4.Text = ""
        txtMultipleSys4.Text = ""
        txtOtherSys4.Text = ""

        lblHeadermultiSys1.Text = ""
        lblHeaderMulSys3.Text = ""
        lblHeaderMultSys2.Text = ""
        lblHeadermultiSys4.Text = ""

        lblHeaderOtherSys1.Text = ""
        lblHeaderotherSys2.Text = ""
        lblHeaderOtherSys3.Text = ""
        lblHeaderOtherSys4.Text = ""

        lblHeaderSingleSys1.Text = ""
        lblHeaderSinlgeSys2.Text = ""
        lblHeaderSingleSys3.Text = ""
        lblHeaderSingleSys4.Text = ""


    End Sub
    Public Sub VisibleTrue()
        lblHeadermultiSys1.Visible = True
        lblHeaderMulSys3.Visible = True
        lblHeaderMultSys2.Visible = True
        lblHeadermultiSys4.Visible = True

        txtPricemultipleSys1.Visible = True
        txtpriceMultipleSys2.Visible = True
        txtMultipleSys3.Visible = True
        txtMultipleSys4.Visible = True


        lblHeaderOtherSys1.Visible = True
        lblHeaderotherSys2.Visible = True
        lblHeaderOtherSys3.Visible = True
        lblHeaderOtherSys4.Visible = True

        txtPriceOtherSys2.Visible = True
        txtPriceothersys1_31.Visible = True
        txtOtherSys3.Visible = True
        txtOtherSys4.Visible = True

        lblHeaderSingleSys1.Visible = True
        lblHeaderSinlgeSys2.Visible = True
        lblHeaderSingleSys3.Visible = True
        lblHeaderSingleSys4.Visible = True

        txtPriceSingleSys1.Visible = True
        txtpriceSinglesys2.Visible = True
        txtSingleSys3.Visible = True
        txtSingleSys4.Visible = True




    End Sub
    Public Sub VisibleFalse()

        lblHeadermultiSys1.Visible = False
        lblHeaderMulSys3.Visible = False
        lblHeaderMultSys2.Visible = False
        lblHeadermultiSys4.Visible = False

        txtPricemultipleSys1.Visible = False
        txtpriceMultipleSys2.Visible = False
        txtMultipleSys3.Visible = False
        txtMultipleSys4.Visible = False


        lblHeaderOtherSys1.Visible = False
        lblHeaderotherSys2.Visible = False
        lblHeaderOtherSys3.Visible = False
        lblHeaderOtherSys4.Visible = False

        txtPriceOtherSys2.Visible = False
        txtPriceothersys1_31.Visible = False
        txtOtherSys3.Visible = False
        txtOtherSys4.Visible = False


        lblHeaderSingleSys1.Visible = False
        lblHeaderSinlgeSys2.Visible = False
        lblHeaderSingleSys3.Visible = False
        lblHeaderSingleSys4.Visible = False

        txtPriceSingleSys1.Visible = False
        txtpriceSinglesys2.Visible = False
        txtSingleSys3.Visible = False
        txtSingleSys4.Visible = False

    End Sub

    Private Sub txtCapacity1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCapacity1.Leave
        If RblSingle.Checked = True Then
            VisibleFalse()
            lblHeadermultiSys1.Text = txtCapacity1.Text
            lblHeaderMulSys3.Text = txtCapacity1.Text
            lblHeaderMultSys2.Text = txtCapacity1.Text
            lblHeadermultiSys4.Text = txtCapacity1.Text

            lblHeadermultiSys1.Visible = True
            lblHeaderMulSys3.Visible = True
            lblHeaderMultSys2.Visible = True
            lblHeadermultiSys4.Visible = True

            txtPricemultipleSys1.Visible = True
            txtpriceMultipleSys2.Visible = True
            txtMultipleSys3.Visible = True
            txtMultipleSys4.Visible = True

            If RblSingle.Checked = True Then
                Dim totalcapacity As Int64
                If txtCapacity1.Text.Trim() <> "" Then
                    totalcapacity = Convert.ToInt32(txtCapacity1.Text) * 20
                    If txtExpCapSys1.Text.Trim() = "" Then

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
                    Else
                        totalcapacity = Convert.ToInt32(txtExpCapSys1.Text) * 20
                        If RdbEnglish.Checked Then
                            txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + " EXPASION " + txtExpCapSys1.Text + "   LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                        ElseIf RdbGujarati.Checked Then
                            txtCapacityWord.Text = ("ક્ષમતા: " + txtCapacity1.Text + " વિસ્તરણ " + txtExpCapSys1.Text + " લિટર/કલાક. . . . " + totalcapacity.ToString() + " લિટર/દિવસ.").ToString()
                        ElseIf RdbHindi.Checked Then
                            txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "Expasion " + txtExpCapSys1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                        ElseIf RdbMarathi.Checked Then
                            txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "Expasion " + txtExpCapSys1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                        ElseIf RdbTamil.Checked Then
                            txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "Expasion " + txtExpCapSys1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                        ElseIf RdbTelugu.Checked Then
                            txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "Expasion " + txtExpCapSys1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                        End If
                    End If
                End If
            End If
            GvTechnicalSYS1.DataSource = Null
            GvTechnicalSYS4.DataSource = Null
            GetTechnicalData(LanguageId)
            For index = 1 To 4
                If index = 1 Then
                    GvSingle_Bind("System 1")
                ElseIf index = 2 Then
                    GvSingle_Bind("System 2")
                ElseIf index = 3 Then
                    GvSingle_Bind("System 3")
                Else
                    GvSingle_Bind("System 4")
                End If
            Next


        End If

        If RblOther.Checked = True Then
            lblHeadermultiSys1.Text = "Qty"
            lblHeaderMulSys3.Text = "Qty"
            lblHeaderMultSys2.Text = "Qty"
            lblHeadermultiSys4.Text = "Qty"

            lblHeaderSingleSys1.Text = "Price"
            lblHeaderSinlgeSys2.Text = "Price"
            lblHeaderSingleSys3.Text = "Price"
            lblHeaderSingleSys4.Text = "Price"

            lblHeaderotherSys2.Text = "Total"
            lblHeaderOtherSys3.Text = "Total"
            lblHeaderOtherSys4.Text = "Total"
            lblHeaderOtherSys1.Text = "Total"

            VisibleTrue()

            'lblHeadermultiSys1.Visible = True
            'lblHeaderSingleSys1.Visible = True
            'lblHeaderOtherSys1.Visible = True



            If RblOther.Checked = True Then
                Dim totalcapacity As Int64
                If txtCapacity1.Text.Trim() <> "" Then
                    If txtExpCapSys1.Text.Trim() = "" Then

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

                    Else

                        totalcapacity = Convert.ToInt32(txtExpCapSys1.Text) * 20
                        If RdbEnglish.Checked Then
                            txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + " Expansion " + txtExpCapSys1.Text.Trim() + " LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()
                        ElseIf RdbGujarati.Checked Then
                            txtCapacityWord.Text = ("ક્ષમતા: " + txtCapacity1.Text + " વિસ્તરણ " + txtExpCapSys1.Text.Trim() + " લિટર/કલાક. . . . " + totalcapacity.ToString() + " લિટર/દિવસ.").ToString()
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
            End If
            GetTechnicalData(LanguageId)
            GvTechnicalSYS1.DataSource = Null
            GvTechnicalSYS4.DataSource = Null
            GvSingle_Bind("System 1")
            GvSingle_Bind("System 2")
            GvSingle_Bind("System 3")
            GvSingle_Bind("System 4")
        End If



    End Sub

    Private Sub txtCapacity2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCapacity2.Leave
        If RblMultiple.Checked = True Then

            VisibleFalse()
            lblHeadermultiSys1.Text = txtCapacity2.Text
            lblHeaderMultSys2.Text = txtCapacity2.Text
            lblHeaderMulSys3.Text = txtCapacity2.Text
            lblHeadermultiSys4.Text = txtCapacity2.Text


            lblHeaderSingleSys1.Text = txtCapacity1.Text
            lblHeaderSinlgeSys2.Text = txtCapacity1.Text
            lblHeaderSingleSys3.Text = txtCapacity1.Text
            lblHeaderSingleSys4.Text = txtCapacity1.Text


            lblHeadermultiSys1.Visible = True
            lblHeaderMultSys2.Visible = True
            lblHeaderMulSys3.Visible = True
            lblHeadermultiSys4.Visible = True


            lblHeaderSingleSys1.Visible = True
            lblHeaderSinlgeSys2.Visible = True
            lblHeaderSingleSys3.Visible = True
            lblHeaderSingleSys4.Visible = True

            txtPricemultipleSys1.Visible = True
            txtpriceMultipleSys2.Visible = True
            txtMultipleSys3.Visible = True
            txtMultipleSys4.Visible = True

            txtSingleSys3.Visible = True
            txtSingleSys4.Visible = True
            txtPriceSingleSys1.Visible = True
            txtpriceSinglesys2.Visible = True





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
                    '   txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity1.ToString() + " LITER/DAY" & Newline & "CAPACITY: " + txtCapacity2.Text + "  LITER/HR . . . . " + totalcapacity2.ToString() + " LITER/DAY  ").ToString()
                End If
            End If
            GetTechnicalData(LanguageId)
            GvTechnicalSYS1.DataSource = Null
            GvTechnicalSYS4.DataSource = Null
            GvMultiple_Bind("System 1")
            GvMultiple_Bind("System 2")
            GvMultiple_Bind("System 3")
            GvMultiple_Bind("System 4")
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
                total(1) = Convert.ToDecimal(Qty).ToString("N2")
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


    Public Sub GvSingle_Bind(ByVal strSys As String)
        If txtCapacity1.Text.Trim() <> "" Then


            Dim StrArray() As String

            If strSys = "System 1" Then
                If txtNoContentSYS1.Text.Trim() <> "" Then
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
                    If txtExpCapSys1.Text.Trim = "" Then
                        str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity1.Text & "' and MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
                    Else
                        str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtExpCapSys1.Text & "' and MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"

                    End If
                    da = New SqlDataAdapter(str, con1)
                    ds = New DataSet()
                    da.Fill(ds)
                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS1.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS1.Text) - 1
                            If RblSingle.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text.Trim())
                            ElseIf RblOther.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text.Trim())
                            End If
                        Next

                    End If
                    If dt Is Nothing Then
                    Else
                        If dt.Rows.Count > 0 Then
                            Dim dView As New DataView(dt)
                            dView.Sort = "SrNo ASC"
                            dt = dView.ToTable()
                        End If
                    End If

                    If strSys = "System 1" Then
                        GvTechnicalSYS1.DataSource = Null
                        GvTechnicalSYS1.DataSource = dt
                        'lblSnoSys1.Text = (dt.Rows.Count + 1).ToString()
                    End If
                    da.Dispose()
                    ds.Dispose()
                    Total1()
                Else
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If RblSingle.Checked = True Then
                            StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "")
                            dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)
                        ElseIf RblOther.Checked = True Then

                            StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "1", "1")
                            dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text)
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


                    If strSys = "System 1" Then
                        GvTechnicalSYS1.DataSource = dt

                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If

                End If



            ElseIf strSys = "System 2" Then

                If txtNoContentSYS2.Text.Trim() <> "" Then
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
                    str = "select * from Category_Master (NOLOCK) where  MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
                    da = New SqlDataAdapter(str, con1)
                    ds = New DataSet()
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS2.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS2.Text) - 1
                            If RblSingle.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)
                            ElseIf RblOther.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text)
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

                        Dim t As Integer
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = dt
                            'add rajesh

                            '   lblSnoSys1.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                            'add rajesh               

                        End If
                        'GvTechnicalSYS1.DataSource = dt
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If RblSingle.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)

                            ElseIf RblOther.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text)
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                            ' lblSnoSys1.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt

                            lblsNosys2.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt

                            ' lblsNosys3.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                            'lblSrNOSys4.Text = (dt.Rows.Count + 1).ToString()
                        End If

                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If

                End If

            ElseIf strSys = "System 3" Then
                If txtNoContentSYS3.Text.Trim() <> "" Then

                    If RblSingle.Checked = True Then
                        dt = New DataTable()
                        dt.Columns.Add("Remove", GetType(Boolean))
                        dt.Columns.Add("Select", GetType(Boolean))
                        dt.Columns.Add("SrNo", GetType(Int32))
                        dt.Columns.Add("Category", GetType(String))
                        dt.Columns.Add("Price", GetType(String))
                        dt.Columns.Add("Tax", GetType(String))
                        dt.Columns.Add("Capacity", GetType(String))

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
                        dt.Columns.Add("Capacity", GetType(String))
                    End If


                    Dim str As String
                    str = "select * from Category_Master (NOLOCK) where   MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
                    da = New SqlDataAdapter(str, con1)
                    ds = New DataSet()
                    da.Fill(ds)


                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS3.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS3.Text) - 1


                            If RblSingle.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text, ds.Tables(0).Rows(i)("Capacity").ToString())
                            ElseIf RblOther.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text, ds.Tables(0).Rows(i)("Capacity").ToString())
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                            ' lblSnoSys1.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt

                            ' lblsNosys2.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt

                            '  lblsNosys3.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                            ' lblSrNOSys4.Text = (dt.Rows.Count + 1).ToString()
                        End If
                        'GvTechnicalSYS1.DataSource = dt

                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                            If RblSingle.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text, ds.Tables(0).Rows(i)("Capacity").ToString())
                            ElseIf RblOther.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text.Trim(), ds.Tables(0).Rows(i)("Capacity").ToString())
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
                        If strSys = "System 1" Then

                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                            '  lblSnoSys1.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 2" Then

                            GvTechnicalSYS2.DataSource = dt

                            '  lblsNosys2.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt

                            ' lblsNosys3.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                            ' lblSrNOSys4.Text = (dt.Rows.Count + 1).ToString()
                        End If

                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If

                End If

            ElseIf strSys = "System 4" Then
                If txtNoContentSYS4.Text.Trim() <> "" Then
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
                    str = ""
                    If txtExpCapSys1.Text.Trim() = "" Then
                        str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity1.Text & "' and MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
                    End If

                    If txtExpCapSys1.Text.Trim() <> "" Then
                        str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtExpCapSys1.Text & "' and MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
                    End If
                    da = New SqlDataAdapter(str, con1)
                    ds = New DataSet()
                    da.Fill(ds)
                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS4.Text) Then

                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS4.Text) - 1
                            If RblSingle.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), TxtTax.Text)
                            ElseIf RblOther.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), "", "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), "1", StrArray(0), TxtTax.Text)
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                            '    lblSnoSys1.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt

                            '  lblsNosys2.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt

                            ' lblsNosys3.Text = (dt.Rows.Count + 1).ToString()
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                            'lblSrNOSys4.Text = (dt.Rows.Count + 1).ToString()
                        End If
                        'GvTechnicalSYS1.DataSource = dt

                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else




                    End If

                End If
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

    Public Sub GvMultiple_Bind(ByVal strSys As String)
        Dim StrArray() As String
        If strSys = "System 1" Then
            If txtNoContentSYS1.Text.Trim() <> "" Then


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

                setLanguageId()
                str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity1.Text & "' and MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"

                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)


                str1 = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity2.Text & "' and MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
                da1 = New SqlDataAdapter(str1, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                If ds.Tables(0).Rows.Count = ds1.Tables(0).Rows.Count Then

                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS1.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS1.Text) - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")

                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If
                Else
                    'MessageBox.Show("Technical Data " + strSys + " Not Match")

                End If
                da1.Dispose()
                ds1.Dispose()
            End If




        ElseIf strSys = "System 2" Then
            If txtNoContentSYS2.Text.Trim() <> "" Then


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


                str = "select * from Category_Master (NOLOCK) where MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"

                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)


                str1 = "select * from Category_Master (NOLOCK) where  MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
                da1 = New SqlDataAdapter(str1, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                If ds.Tables(0).Rows.Count = ds1.Tables(0).Rows.Count Then

                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS2.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS2.Text) - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If
                Else
                    'MessageBox.Show("Technical Data " + strSys + " Not Match")

                End If
                da1.Dispose()
                ds1.Dispose()
            End If

        ElseIf strSys = "System 3" Then

            If txtNoContentSYS3.Text.Trim() <> "" Then

                dt = New DataTable()
                If RblMultiple.Checked = True Then
                    dt.Columns.Add("Remove", GetType(Boolean))
                    dt.Columns.Add("Select", GetType(Boolean))
                    dt.Columns.Add("SrNo", GetType(Int32))
                    dt.Columns.Add("Category", GetType(String))
                    dt.Columns.Add("Price1", GetType(String))
                    dt.Columns.Add("Price2", GetType(String))
                    dt.Columns.Add("Tax", GetType(String))
                    dt.Columns.Add("Capacity", GetType(String))
                End If
                Dim str As String
                Dim str1 As String


                str = "select * from Category_Master (NOLOCK) where MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"

                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)


                str1 = "select * from Category_Master (NOLOCK) where MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
                da1 = New SqlDataAdapter(str1, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                If ds.Tables(0).Rows.Count = ds1.Tables(0).Rows.Count Then

                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS3.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS3.Text) - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text, ds1.Tables(0).Rows(i)("Capacity").ToString())
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text, ds.Tables(0).Rows(i)("Capacity").ToString())
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If
                Else
                    'MessageBox.Show("Technical Data " + strSys + " Not Match")

                End If
                da1.Dispose()
                ds1.Dispose()

            End If

        ElseIf strSys = "System 4" Then
            If txtNoContentSYS4.Text.Trim() <> "" Then

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


                str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity1.Text & "' and MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"

                da = New SqlDataAdapter(str, con1)
                ds = New DataSet()
                da.Fill(ds)


                str1 = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity2.Text & "' and MainCategory='" + strSys + "' and LanguageId = " & LanguageId & " ORDER BY SNo"
                da1 = New SqlDataAdapter(str1, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                If ds.Tables(0).Rows.Count = ds1.Tables(0).Rows.Count Then

                    If ds.Tables(0).Rows.Count >= Convert.ToInt32(txtNoContentSYS4.Text) Then
                        For i As Integer = 0 To Convert.ToInt32(txtNoContentSYS4.Text) - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    Else
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            If RblMultiple.Checked = True Then
                                StrArray = TotalTaxCreate(ds.Tables(0).Rows(i)("Price").ToString(), ds1.Tables(0).Rows(i)("Price").ToString(), "1")
                                dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), StrArray(0), StrArray(1), TxtTax.Text)
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
                        If strSys = "System 1" Then
                            GvTechnicalSYS1.DataSource = Null
                            GvTechnicalSYS1.DataSource = dt
                        ElseIf strSys = "System 2" Then
                            GvTechnicalSYS2.DataSource = dt
                        ElseIf strSys = "System 3" Then
                            GvTechnicalSYS3.DataSource = dt
                        ElseIf strSys = "System 4" Then
                            GvTechnicalSYS4.DataSource = Null
                            GvTechnicalSYS4.DataSource = dt
                        End If
                        da.Dispose()
                        ds.Dispose()
                        Total1()
                    End If
                Else
                    'MessageBox.Show("Technical Data " + strSys + " Not Match")

                End If
                da1.Dispose()
                ds1.Dispose()
            End If
        End If

    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub








    Public Sub Total1()
        Dim total As Decimal
        Dim total1 As Decimal
        total = 0
        total1 = 0
        lblSys1Total.Text = ""
        lblSys2Total.Text = ""
        lblSys3Total.Text = ""
        lblSys4Total.Text = ""
        lblAllMulSysTotal.Text = ""
        lblAllsysTotal.Text = ""
        lblSys1MulTotal.Text = ""
        lblSys2MulTotal.Text = ""
        lblSys3MulTotal.Text = ""
        lblSys4MulTotal.Text = ""


        For i As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTechnicalSYS1.Rows(i).Cells(0).Value)
            If IsTicked Then
            Else

                If RblSingle.Checked = True Then
                    Try
                        total = total + Convert.ToDecimal(If(GvTechnicalSYS1.Rows(i).Cells(4).Value = "", 0, GvTechnicalSYS1.Rows(i).Cells(4).Value))
                        txtTotalSys1.Text = total.ToString()
                        txtTotal1Sys1.Visible = False
                        lblSys1Total.Text = total.ToString()
                        lblSys1MulTotal.Visible = False
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                ElseIf RblOther.Checked = True Then
                    Try
                        total = total + Convert.ToDecimal(If(GvTechnicalSYS1.Rows(i).Cells(5).Value = "", 0, GvTechnicalSYS1.Rows(i).Cells(5).Value))

                        txtTotalSys1.Text = total.ToString()
                        txtTotal1Sys1.Visible = False
                        lblSys1Total.Text = total.ToString()
                        lblSys1MulTotal.Visible = False
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)

                    End Try


                ElseIf RblMultiple.Checked = True Then
                    Try
                        total = total + Convert.ToDecimal(If(GvTechnicalSYS1.Rows(i).Cells(4).Value = "", 0, GvTechnicalSYS1.Rows(i).Cells(4).Value))
                        txtTotalSys1.Text = total.ToString()
                        total1 = total1 + Convert.ToDecimal(If(GvTechnicalSYS1.Rows(i).Cells(5).Value = "", 0, GvTechnicalSYS1.Rows(i).Cells(5).Value))
                        txtTotal1Sys1.Text = total1.ToString()
                        lblSys1Total.Text = total.ToString()
                        lblSys1MulTotal.Visible = True
                        lblSys1MulTotal.Text = total1.ToString()
                        txtTotal1Sys1.Visible = True
                        txtTotalSys1.Visible = True
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)

                    End Try

                End If


            End If


        Next

        total = 0
        total1 = 0

        For i As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(i).Cells(0).Value)
            If IsTicked Then
            Else

                If RblSingle.Checked = True Then
                    total = total + Convert.ToDecimal(If(GvTechnicalSYS2.Rows(i).Cells(4).Value = "", 0, GvTechnicalSYS2.Rows(i).Cells(4).Value))
                    txtTotalSys2.Text = total.ToString()
                    txtTotal1Sys2.Visible = False
                    lblSys2Total.Text = total.ToString()
                    lblSys2MulTotal.Visible = False
                ElseIf RblOther.Checked = True Then
                    total = total + Convert.ToDecimal(If(GvTechnicalSYS2.Rows(i).Cells(5).Value = "", 0, GvTechnicalSYS2.Rows(i).Cells(5).Value))
                    txtTotalSys2.Text = total.ToString()
                    txtTotal1Sys2.Visible = False
                    lblSys2Total.Text = total.ToString()
                    lblSys2MulTotal.Visible = False
                ElseIf RblMultiple.Checked = True Then

                    total = total + Convert.ToDecimal(If(GvTechnicalSYS2.Rows(i).Cells(4).Value = "", 0, GvTechnicalSYS2.Rows(i).Cells(4).Value))
                    txtTotalSys2.Text = total.ToString()
                    total1 = total1 + Convert.ToDecimal(If(GvTechnicalSYS2.Rows(i).Cells(5).Value = "", 0, GvTechnicalSYS2.Rows(i).Cells(5).Value))
                    txtTotal1Sys2.Text = total1.ToString()
                    txtTotal1Sys2.Visible = True
                    txtTotalSys2.Visible = True
                    lblSys2Total.Text = total.ToString()
                    lblSys2MulTotal.Visible = True
                    lblSys2MulTotal.Text = total1.ToString()

                End If


            End If


        Next


        total = 0
        total1 = 0

        For i As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(i).Cells(0).Value)
            If IsTicked Then
            Else

                If RblSingle.Checked = True Then
                    total = total + Convert.ToDecimal(If(GvTechnicalSYS3.Rows(i).Cells(4).Value = "", 0, GvTechnicalSYS3.Rows(i).Cells(4).Value))

                    txtTotalSys3.Text = total.ToString()
                    txtTotal1Sys3.Visible = False
                    lblSys3Total.Text = total.ToString()
                    lblSys3MulTotal.Visible = False
                ElseIf RblOther.Checked = True Then
                    total = total + Convert.ToDecimal(If(GvTechnicalSYS3.Rows(i).Cells(5).Value = "", 0, GvTechnicalSYS3.Rows(i).Cells(5).Value))


                    txtTotalSys3.Text = total.ToString()
                    txtTotal1Sys3.Visible = False
                    lblSys3Total.Text = total.ToString()
                    lblSys3MulTotal.Visible = False

                ElseIf RblMultiple.Checked = True Then

                    total = total + Convert.ToDecimal(If(GvTechnicalSYS3.Rows(i).Cells(4).Value = "", 0, GvTechnicalSYS3.Rows(i).Cells(4).Value))
                    txtTotalSys3.Text = total.ToString()
                    total1 = total1 + Convert.ToDecimal(If(GvTechnicalSYS3.Rows(i).Cells(5).Value = "", 0, GvTechnicalSYS3.Rows(i).Cells(5).Value))
                    txtTotal1Sys3.Text = total1.ToString()
                    txtTotal1Sys3.Visible = True
                    txtTotalSys3.Visible = True
                    lblSys3Total.Text = total.ToString()
                    lblSys3MulTotal.Visible = True
                    lblSys3MulTotal.Text = total1.ToString()

                End If


            End If


        Next


        total = 0
        total1 = 0

        For i As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(i).Cells(0).Value)
            If IsTicked Then
            Else

                If RblSingle.Checked = True Then
                    total = total + Convert.ToDecimal(If(GvTechnicalSYS4.Rows(i).Cells(4).Value = "", 0, GvTechnicalSYS4.Rows(i).Cells(4).Value))
                    txtTotalSys4.Text = total.ToString()
                    txtTotal1Sys4.Visible = False
                    lblSys4Total.Text = total.ToString()
                    lblSys4MulTotal.Visible = False
                ElseIf RblOther.Checked = True Then
                    total = total + Convert.ToDecimal(If(GvTechnicalSYS4.Rows(i).Cells(5).Value = "", 0, GvTechnicalSYS4.Rows(i).Cells(5).Value))
                    txtTotalSys4.Text = total.ToString()
                    txtTotal1Sys4.Visible = False
                    lblSys4Total.Text = total.ToString()
                    lblSys4MulTotal.Visible = False

                ElseIf RblMultiple.Checked = True Then

                    total = total + Convert.ToDecimal(If(GvTechnicalSYS4.Rows(i).Cells(4).Value = "", 0, GvTechnicalSYS4.Rows(i).Cells(4).Value))
                    txtTotalSys4.Text = total.ToString()
                    total1 = total1 + Convert.ToDecimal(If(GvTechnicalSYS4.Rows(i).Cells(5).Value = "", 0, GvTechnicalSYS4.Rows(i).Cells(5).Value))
                    txtTotal1Sys4.Text = total1.ToString()
                    txtTotal1Sys4.Visible = True
                    txtTotalSys4.Visible = True
                    lblSys4Total.Text = total.ToString()
                    lblSys4MulTotal.Visible = True
                    lblSys4MulTotal.Text = total1.ToString()

                End If


            End If


        Next
        TotalLeave()





    End Sub



    Private Sub GvTechnical_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvTechnicalSYS1.CellValueChanged
        Total1()
    End Sub


    Private Sub txtNoContentSYS3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoContentSYS3.Leave
        GetTechnicalData(LanguageId)

        GvSingle_Bind("System 2")
    End Sub

    Private Sub txtNoContentSYS4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoContentSYS4.Leave
        GetTechnicalData(LanguageId)
        GvSingle_Bind("System 3")
    End Sub

    Private Sub txtNoContentSYS2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoContentSYS2.Leave
        GetTechnicalData(LanguageId)
        GvSingle_Bind("System 4")
    End Sub



    Private Sub btnSave1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave1.Click
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



        Try
            con1.Close()
        Catch ex As Exception
        End Try
        Dim str As String
        Dim techinical12 As String
        Dim QMaxId As Int32
        Dim StrImageLocation As String
        Try

            con1.Open()
            ' StrImageLocation = ""
            If PicDefault.Image Is Nothing Then
                StrImageLocation = ""
            Else

                StrImageLocation = PicDefault.ImageLocation.ToString()
            End If
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
                "DefaultImage='" + StrImageLocation + "'," + _
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
                str = "insert into Quotation_Master (QType,Fk_EnqTypeID,Quot_No,Q_Year,Enq_No,Ref,Quot_Type,Name,Address,Capacity_Type,Capacity_Single,Capacity_Multiple,KindAtt,Subject,Buss_Excecutive,Buss_Name,Later_Description,Later_Date,Capacity_Word,UserName,DefaultImage,QDate,Quatation_Type,LanguageId,Fk_AddressId) values('" + txtType.Text + "','" & ddlEnqType.SelectedValue & "'," & QuotationMaxId & "," & year1 & ",'" + txtEnqNo.Text + "','" + txtRef.Text + "','" + txtQoutType.Text + "','" + txtName.Text + "','" + txtAddress.Text + "', '" + capacityType.ToString() + "','" + txtCapacity1.Text + "','" + txtCapacity2.Text + "','" + txtKind.Text + "','" + txtSub.Text + "','" + txtBussness_Exe.Text + "','" + txtBuss_Name.Text + "','" + txtDescription.Text + "','" + txtLatterDate.Text + "','" + txtCapacityWord.Text + "','" + Class1.global.UserName.ToString() + "','" + StrImageLocation + "','" + txtDate.Text + "','" + "ISI" + "'," & LanguageId & "," & Address_ID & ")"
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
            For i As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1

                Dim RemoveStatus As Boolean = CBool(GvTechnicalSYS1.Rows(i).Cells(0).Value)
                If RemoveStatus Then
                Else

                    Dim status As String
                    Dim selectStatus As Boolean = CBool(GvTechnicalSYS1.Rows(i).Cells(1).Value)
                    status = ""
                    If selectStatus Then
                        status = "Yes"
                    Else
                        status = "No"

                    End If
                    Dim MainCategory As String
                    MainCategory = "System 1"
                    If btnSave1.Text = "Update" Then
                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,Tax,MainCategory) values(" & QuationId & ",'" + GvTechnicalSYS1.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + GvTechnicalSYS1.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QuationId & ",'" + GvTechnicalSYS1.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + GvTechnicalSYS1.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QuationId & ",'" + GvTechnicalSYS1.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + GvTechnicalSYS1.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If
                    Else
                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,Tax,MainCategory) values(" & QMaxId & ",'" + GvTechnicalSYS1.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + GvTechnicalSYS1.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QMaxId & ",'" + GvTechnicalSYS1.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + GvTechnicalSYS1.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QMaxId & ",'" + GvTechnicalSYS1.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS1.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + GvTechnicalSYS1.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If
                    End If

                End If
            Next
            For i As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1

                Dim RemoveStatus As Boolean = CBool(GvTechnicalSYS2.Rows(i).Cells(0).Value)
                If RemoveStatus Then

                Else

                    Dim status As String
                    Dim selectStatus As Boolean = CBool(GvTechnicalSYS2.Rows(i).Cells(1).Value)
                    status = ""

                    If selectStatus Then
                        status = "Yes"
                    Else
                        status = "No"

                    End If
                    Dim MainCategory As String
                    MainCategory = "System 2"

                    If btnSave1.Text = "Update" Then

                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,Tax,MainCategory) values(" & QuationId & ",'" + GvTechnicalSYS2.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + GvTechnicalSYS2.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QuationId & ",'" + GvTechnicalSYS2.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + GvTechnicalSYS2.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QuationId & ",'" + GvTechnicalSYS2.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + GvTechnicalSYS2.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If
                    Else
                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,Tax,MainCategory) values(" & QMaxId & ",'" + GvTechnicalSYS2.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + GvTechnicalSYS2.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QMaxId & ",'" + GvTechnicalSYS2.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + GvTechnicalSYS2.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QMaxId & ",'" + GvTechnicalSYS2.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS2.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + GvTechnicalSYS2.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If

                    End If
                End If
            Next

            For i As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1

                Dim RemoveStatus As Boolean = CBool(GvTechnicalSYS3.Rows(i).Cells(0).Value)
                If RemoveStatus Then

                Else

                    Dim status As String
                    Dim selectStatus As Boolean = CBool(GvTechnicalSYS3.Rows(i).Cells(1).Value)
                    status = ""

                    If selectStatus Then
                        status = "Yes"
                    Else
                        status = "No"

                    End If
                    Dim MainCategory As String
                    MainCategory = "System 3"

                    If btnSave1.Text = "Update" Then
                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,Tax,MainCategory,Capacity) values(" & QuationId & ",'" + GvTechnicalSYS3.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + GvTechnicalSYS3.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "','" + GvTechnicalSYS3.Rows(i).Cells("Capacity").Value.ToString() + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,Tax,MainCategory,Capacity) values(" & QuationId & ",'" + GvTechnicalSYS3.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + GvTechnicalSYS3.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "','" + GvTechnicalSYS3.Rows(i).Cells("Capacity").Value.ToString() + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,Tax,MainCategory,Capacity) values(" & QuationId & ",'" + GvTechnicalSYS3.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + GvTechnicalSYS3.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "','" + GvTechnicalSYS3.Rows(i).Cells("Capacity").Value.ToString() + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If
                    Else
                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,Tax,MainCategory,Capacity) values(" & QMaxId & ",'" + GvTechnicalSYS3.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + GvTechnicalSYS3.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "','" + GvTechnicalSYS3.Rows(i).Cells("Capacity").Value.ToString() + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,Tax,MainCategory,Capacity) values(" & QMaxId & ",'" + GvTechnicalSYS3.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + GvTechnicalSYS3.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "','" + GvTechnicalSYS3.Rows(i).Cells("Capacity").Value.ToString() + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,Tax,MainCategory,Capacity) values(" & QMaxId & ",'" + GvTechnicalSYS3.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + GvTechnicalSYS3.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "','" + GvTechnicalSYS3.Rows(i).Cells("Capacity").Value.ToString() + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If
                    End If
                End If

            Next

            For i As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1

                Dim RemoveStatus As Boolean = CBool(GvTechnicalSYS4.Rows(i).Cells(0).Value)
                If RemoveStatus Then

                Else

                    Dim status As String
                    Dim selectStatus As Boolean = CBool(GvTechnicalSYS4.Rows(i).Cells(1).Value)
                    status = ""

                    If selectStatus Then
                        status = "Yes"
                    Else
                        status = "No"

                    End If
                    Dim MainCategory As String
                    MainCategory = "System 4"

                    If btnSave1.Text = "Update" Then

                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,Tax,MainCategory) values(" & QuationId & ",'" + GvTechnicalSYS4.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + GvTechnicalSYS4.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QuationId & ",'" + GvTechnicalSYS4.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + GvTechnicalSYS4.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QuationId & ",'" + GvTechnicalSYS4.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + GvTechnicalSYS4.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If
                    Else
                        If RblSingle.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,DocumentationImage,Tax,MainCategory) values(" & QMaxId & ",'" + GvTechnicalSYS4.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(4).Value.ToString() + "','" + status + "','" + GvTechnicalSYS4.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblOther.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Qty,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QMaxId & ",'" + GvTechnicalSYS4.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(5).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(6).Value.ToString() + "','" + status + "','" + GvTechnicalSYS4.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        ElseIf RblMultiple.Checked = True Then
                            techinical12 = "insert into Technical_Data(Fk_QuotationID,SNo,TechnicalData,Price1,Price2,DocumentationImage,Tax,MainCategory) values(" & QMaxId & ",'" + GvTechnicalSYS4.Rows(i).Cells(2).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(3).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(4).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(5).Value.ToString() + "','" + status + "','" + GvTechnicalSYS4.Rows(i).Cells("Tax").Value.ToString() + "','" + MainCategory + "')"
                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If


                    End If

                End If
            Next

            btnSave1.Text = "Update"
            'dt.Dispose()

            con1.Close()
            BindOnLanguageName()

            MessageBox.Show("Successfully Submit .....")

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

        Try
            con1.Close()
        Catch ex As Exception

        End Try
        If (txtEnqNo.Text.Trim() <> "") Then
            con1.Open()
            EnqMax = 0
            Dim Claient = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text.Trim())
            For Each item As SP_Get_AddressListByEnqNoResult In Claient
                txtName.Text = item.Name
                txtAddress.Text = item.Address + "," + item.City + "," + item.State
                txtEnqNo.Text = item.EnqNo
                Address_ID = item.Pk_AddressID
              

            Next

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

            Try
                con1.Close()
            Catch ex As Exception

            End Try
            Ref = "IIECL-Q / " + Class1.global.User.ToString() + " / " + txtEnqNo.Text + " - " + EnqMax.ToString() + " / " + year1.ToString() + ""
            txtRef.Text = Ref.ToString()
            con1.Open()
            str = "select Address,Name from Quotation_Master (NOLOCK) where Enq_No='" + txtEnqNo.Text.Trim() + "' "
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            If (dr.HasRows) Then
                dr.Read()
                If (dr("Address").ToString() <> "") Then
                    txtAddress.Text = dr("Address").ToString()
                End If
                If (dr("Name").ToString() <> "") Then
                    txtName.Text = dr("Name").ToString()
                End If
            End If
            cmd.Dispose()
            dr.Dispose()
            con1.Close()

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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If dt.Rows.Count > 0 Then
            dt.Dispose()
            GvTechnicalSYS1.DataSource = dt
            GvTechnicalSYS2.DataSource = dt
            GvTechnicalSYS3.DataSource = dt
            GvTechnicalSYS4.DataSource = dt
            da.Dispose()
            ds.Dispose()
        End If
    End Sub


    Private Sub txtTax_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If RblSingle.Checked = True Then
            Total1()
        End If

    End Sub
    Public Sub FirstPage()

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
        Dim limittable As Integer

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
            Dim oTable5 As Word.Table = objDoc.Tables.Add(ran3, 1, 4, missing, missing)
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
            Try
                con1.Close()
            Catch ex As Exception

            End Try


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
            Try
                con1.Close()
            Catch ex As Exception

            End Try


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



        objDoc.SaveAs(QtempPath + "\Letter1.doc")
        objDoc.SaveAs(QtempPath + "\Letter1.pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


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

        Dim paramSourceDocPath1 As Object = QtempPath + "\Letter1.doc"
        Dim Targets1 As Object = QtempPath + "\Letter1.pdf"
        'added by rajesh for read only mode?
        wordDocument1 = wordApplication1.Documents.Open(paramSourceDocPath1)


        Dim formating1 As Object
        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF

        wordDocument1.Close()
        wordDocument1 = Nothing
        wordApplication1.Quit()
        wordApplication1 = Nothing

    End Sub
    Public Sub indexs()

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
        oTable3.Add(rng, 1, 2, missing, missing)
        rng.Font.Name = "Arial"
        rng.Borders.Enable = 0
        rng.Font.Size = 8

        Dim newRowa2 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Borders.Enable = 0

        newRowa2.Cells(2).Range.Text = "INDEX"
        newRowa2.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
        newRowa2.Range.Font.Size = 14
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Range.Shading.BackgroundPatternColor = RGB(12, 28, 71)
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 450
        newRowa2.Cells(1).Width = 30

        newRowa2.Range.Font.Bold = True


        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
        newRowa2.Range.Borders.Enable = 0
        newRowa2.Cells(1).Range.Text = "SR.NO."
        newRowa2.Cells(2).Range.Text = "DESCRIPTION"
        newRowa2.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack

        newRowa2.Range.Bold = True
        newRowa2.Range.ParagraphFormat.SpaceAfter = 9
        newRowa2.Range.ParagraphFormat.SpaceBefore = 9
        newRowa2.Range.Font.Size = 11
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 60

        Dim srno As Integer
        srno = 0


        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
        newRowa2.Range.Borders.Enable = 0
        newRowa2.Cells(1).Range.Text = srno + 1
        newRowa2.Cells(2).Range.Text = txtDefaultImage.Text
        newRowa2.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack

        newRowa2.Range.Bold = True
        newRowa2.Range.ParagraphFormat.SpaceAfter = 9
        newRowa2.Range.ParagraphFormat.SpaceBefore = 9
        newRowa2.Range.Font.Size = 11
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Range.ParagraphFormat.SpaceAfter = 0
        newRowa2.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 60

        srno = srno + 1
        'Lamda(Expression) : Grid(Grid(Grid))
        Dim cntIndex As Integer
        cntIndex = 1
        For ik = 0 To GvTechnicalSYS1.Rows.Count - 1
            If ik = 0 Then

                newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa2.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowa2.Range.Borders.Enable = 0
                newRowa2.Cells(1).Range.Text = Convert.ToString(srno + 1)
                newRowa2.Cells(2).Range.Text = "SYS-1 Mineral Water Project : Photo With Technical Data"
                newRowa2.Range.Bold = True
                newRowa2.Range.ParagraphFormat.SpaceAfter = 9
                newRowa2.Range.ParagraphFormat.SpaceBefore = 9
                newRowa2.Range.Font.Size = 11
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa2.Cells(2).Width = 420
                newRowa2.Cells(1).Width = 60
                srno = srno + 1
            End If

        Next


        For ik = 0 To GvTechnicalSYS2.Rows.Count - 1

            Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(ik).Cells(1).Value)
            If IsTicked Then

                newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa2.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowa2.Range.Borders.Enable = 0
                newRowa2.Cells(1).Range.Text = Convert.ToString(srno + 1)
                newRowa2.Cells(2).Range.Text = "SYS-2." + (cntIndex).ToString() + " " + GvTechnicalSYS2.Rows(ik).Cells("Category").Value.ToString()
                newRowa2.Range.Bold = True
                newRowa2.Range.ParagraphFormat.SpaceAfter = 9
                newRowa2.Range.ParagraphFormat.SpaceBefore = 9
                newRowa2.Range.Font.Size = 11
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa2.Cells(2).Width = 420
                newRowa2.Cells(1).Width = 60
                srno = srno + 1
                cntIndex = cntIndex + 1
            End If
        Next

        cntIndex = 1

        For ik = 0 To GvTechnicalSYS3.Rows.Count - 1

            Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(ik).Cells(1).Value)
            If IsTicked Then

                newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa2.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowa2.Range.Borders.Enable = 0
                newRowa2.Cells(1).Range.Text = Convert.ToString(srno + 1)
                newRowa2.Cells(2).Range.Text = "SYS-3." + (cntIndex).ToString() + " " + GvTechnicalSYS3.Rows(ik).Cells("Category").Value.ToString()
                newRowa2.Range.Bold = True
                newRowa2.Range.ParagraphFormat.SpaceAfter = 9
                newRowa2.Range.ParagraphFormat.SpaceBefore = 9
                newRowa2.Range.Font.Size = 11
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa2.Cells(2).Width = 420
                newRowa2.Cells(1).Width = 60
                srno = srno + 1
                cntIndex = cntIndex + 1
            End If
        Next

        cntIndex = 1
        For ik = 0 To GvTechnicalSYS4.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(ik).Cells(1).Value)
            If IsTicked Then

                newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa2.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowa2.Range.Borders.Enable = 0
                newRowa2.Cells(1).Range.Text = Convert.ToString(srno + 1)
                newRowa2.Cells(2).Range.Text = "SYS-4." + (cntIndex).ToString() + " " + GvTechnicalSYS4.Rows(ik).Cells("Category").Value.ToString()
                newRowa2.Range.Bold = True
                newRowa2.Range.ParagraphFormat.SpaceAfter = 9
                newRowa2.Range.ParagraphFormat.SpaceBefore = 9
                newRowa2.Range.Font.Size = 11
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa2.Cells(2).Width = 420
                newRowa2.Cells(1).Width = 60
                srno = srno + 1
                cntIndex = cntIndex + 1
            End If
        Next

        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
        newRowa2.Range.Borders.Enable = 0
        newRowa2.Cells(1).Range.Text = Convert.ToString(srno + 1)
        newRowa2.Cells(2).Range.Text = "TOTAL COST OF ENTIRE MINERAL WATER PROJECT"
        newRowa2.Range.Bold = True
        newRowa2.Range.ParagraphFormat.SpaceAfter = 9
        newRowa2.Range.ParagraphFormat.SpaceBefore = 9
        newRowa2.Range.Font.Size = 11
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 60

        srno = srno + 1

        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
        newRowa2.Range.Borders.Enable = 0
        newRowa2.Cells(1).Range.Text = Convert.ToString(srno + 1)
        newRowa2.Cells(2).Range.Text = "TERMS & CONDITIONS"
        newRowa2.Range.Bold = True
        newRowa2.Range.ParagraphFormat.SpaceAfter = 9
        newRowa2.Range.ParagraphFormat.SpaceBefore = 9
        newRowa2.Range.Font.Size = 11
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 60


        Dim count As Integer
        count = 0

        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
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


        'objDoc.Tables(1).Columns.Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
        '    .LineStyle = objApp.Options.DefaultBorderLineStyle
        '    .LineWidth = objApp.Options.DefaultBorderLineWidth
        '    .Color = objApp.Options.DefaultBorderColor
        'End With


        'objDoc.Tables(1).Rows.First.Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
        '    .LineStyle = Word.WdLineStyle.wdLineStyleNone
        '    .LineWidth = Word.WdLineStyle.wdLineStyleNone
        'End With
        'objDoc.Tables(1).Range.Rows(1).Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
        '    .LineStyle = Word.WdLineStyle.wdLineStyleNone
        '    .LineWidth = Word.WdLineStyle.wdLineStyleNone
        'End With
        'objDoc.Tables(1).Range.Rows(2).Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
        '    .LineStyle = Word.WdLineStyle.wdLineStyleNone
        '    .LineWidth = Word.WdLineStyle.wdLineStyleNone
        'End With
        'objDoc.Tables(1).Range.Rows(3).Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
        '    .LineStyle = Word.WdLineStyle.wdLineStyleNone
        '    .LineWidth = Word.WdLineStyle.wdLineStyleNone
        'End With
        'objDoc.Tables(1).Range.Rows(4).Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
        '    .LineStyle = Word.WdLineStyle.wdLineStyleNone
        '    .LineWidth = Word.WdLineStyle.wdLineStyleNone
        'End With



        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document
        wordDocument1 = New Word.Document
        wordApplication1 = New Word.Application


        Dim formating1 As Object

        objDoc.SaveAs(QtempPath + "\Index.doc")
        Dim paramSourceDocPath1 As Object = QtempPath + "\Index.doc"
        Dim Targets1 As Object = QtempPath + "\Index.pdf"


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




    End Sub

    Public Sub OrderWNBPriceSheet()
        Dim str1(0) As String
        ReDim str1(0)
        str1(0) = QtempPath + "\" + Convert.ToString(0) + ".doc"
        Dim i As Integer
        '  i = _pdfforge.Images2PDF(str1, appPath + "\step3.pdf", 0)
        Dim FinalSysAllTotal As Decimal
        FinalSysAllTotal = 0

        Dim dt33 As DataTable
        dt33 = New DataTable

        Dim dtSys2 As DataTable
        dtSys2 = New DataTable

        Dim dtSys3 As DataTable
        dtSys3 = New DataTable

        Dim dtSys4 As DataTable
        dtSys4 = New DataTable

        Dim price As Decimal
        Dim price1 As Decimal
        Dim qty1 As Decimal


        price = 0
        price1 = 0
        qty1 = 0
        If RblSingle.Checked = True Then
            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price")
            For t1 As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS1.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnicalSYS1.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(4).Value.ToString())
                End If
            Next

            dtSys2.Columns.Add("No")
            dtSys2.Columns.Add("Description")
            dtSys2.Columns.Add("Price")

            For t1 As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(t1).Cells(0).Value)
                If IsTicked Then
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                Else
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    'dtSys2.Rows.Add(GvTechnicalSYS2.Rows(t1).Cells(2).Value.ToString(), "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                End If
            Next
            dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price)
            dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price)
            dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price)

            dtSys3.Columns.Add("No")
            dtSys3.Columns.Add("Description")
            dtSys3.Columns.Add("Price")
            dtSys3.Columns.Add("Capacity")

            For t1 As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys3.Rows.Add(GvTechnicalSYS3.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(6).Value)
                End If
            Next

            dtSys4.Columns.Add("No")
            dtSys4.Columns.Add("Description")
            dtSys4.Columns.Add("Price")

            For t1 As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys4.Rows.Add(GvTechnicalSYS4.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(4).Value.ToString())
                End If
            Next
        End If


        If RblOther.Checked = True Then

            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price")
            dt33.Columns.Add("Qty")
            dt33.Columns.Add("Total")

            For t1 As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS1.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnicalSYS1.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(6).Value.ToString())

                End If
            Next


            dtSys2.Columns.Add("No")
            dtSys2.Columns.Add("Description")
            dtSys2.Columns.Add("Price")
            dtSys2.Columns.Add("Qty")
            dtSys2.Columns.Add("Total")
            price = 0
            price1 = 0
            qty1 = 0

            For t1 As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(t1).Cells(0).Value)
                If IsTicked Then
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    qty1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                    price1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(6).Value.ToString())
                Else
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    qty1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                    price1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(6).Value.ToString())

                    ' dtSys2.Rows.Add(GvTechnicalSYS2.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(6).Value.ToString())
                End If
            Next
            dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price.ToString(), qty1.ToString(), price1.ToString())
            dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price.ToString(), qty1.ToString(), price1.ToString())
            dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price.ToString(), qty1.ToString(), price1.ToString())


            'dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price)
            'dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price)
            'dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price)

            dtSys3.Columns.Add("No")
            dtSys3.Columns.Add("Description")
            dtSys3.Columns.Add("Price")
            dtSys3.Columns.Add("Qty")
            dtSys3.Columns.Add("Total")
            dtSys3.Columns.Add("Capacity")

            For t1 As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys3.Rows.Add(GvTechnicalSYS3.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(6).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(7).Value)
                End If
            Next

            dtSys4.Columns.Add("No")
            dtSys4.Columns.Add("Description")
            dtSys4.Columns.Add("Price")
            dtSys4.Columns.Add("Qty")
            dtSys4.Columns.Add("Total")

            For t1 As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys4.Rows.Add(GvTechnicalSYS4.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(6).Value.ToString())
                End If
            Next

        End If



        If RblMultiple.Checked = True Then

            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price1")
            dt33.Columns.Add("Price2")
            For t1 As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS1.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnicalSYS1.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(5).Value.ToString())
                End If
            Next
            price = 0
            price1 = 0

            dtSys2.Columns.Add("No")
            dtSys2.Columns.Add("Description")
            dtSys2.Columns.Add("Price1")
            dtSys2.Columns.Add("Price2")
            For t1 As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(t1).Cells(0).Value)
                If IsTicked Then
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    price1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                Else
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    price1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                    'dtSys2.Rows.Add(GvTechnicalSYS2.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                End If
            Next
            dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price.ToString(), price1.ToString())
            dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price.ToString(), price1.ToString())
            dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price.ToString(), price1.ToString())

            'dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price.ToString(), qty1.ToString(), price1.ToString())
            'dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price.ToString(), qty1.ToString(), price1.ToString())
            'dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price.ToString(), qty1.ToString(), price1.ToString())



            dtSys3.Columns.Add("No")
            dtSys3.Columns.Add("Description")
            dtSys3.Columns.Add("Price1")
            dtSys3.Columns.Add("Price2")
            dtSys3.Columns.Add("Capacity")

            For t1 As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys3.Rows.Add(GvTechnicalSYS3.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells("Capacity").Value)
                End If
            Next

            dtSys4.Columns.Add("No")
            dtSys4.Columns.Add("Description")
            dtSys4.Columns.Add("Price1")
            dtSys4.Columns.Add("Price2")
            For t1 As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys4.Rows.Add(GvTechnicalSYS4.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(5).Value.ToString())
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


        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc1.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
        Else

            For Each section As Word.Section In objDoc1.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
        End If


        Dim oTable2 As Word.Tables = objDoc1.Tables
        Dim rng2 As Word.Range = objDoc1.Range(start2, missing1)
        If RblSingle.Checked = True Then

            oTable2.Add(rng2, 1, 2, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)

            oTable2.Add(rng2, 1, 4, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)


        End If
        If RblOther.Checked = True Then
            oTable2.Add(rng2, 1, 2, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)


            oTable2.Add(rng2, 1, 6, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)

        End If
        If RblMultiple.Checked = True Then

            oTable2.Add(rng2, 1, 2, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)


            oTable2.Add(rng2, 1, 5, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)

        End If
        Dim defaultTableBehavior1 As [Object] = Type.Missing
        Dim autoFitBehavior1 As [Object] = Type.Missing



        'objDoc1.Selection.TypeText("Refzxczxxczccxxxxxxxxxxxxcxcxcccxcxcxcxcxccxxcxccxccxcxcc")
        ' Dim rng1 As Word.Range = objDoc1.Range(0, 0)
        rng2.Font.Name = "Arial"
        ' oTable2.Range.ParagraphFormat.SpaceAfter = 3
        Dim tbl2 As Word.Table = objDoc1.Tables(1)

        objDoc1.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4

        rng2.Font.Name = "Arial"
        tbl2.Range.ParagraphFormat.SpaceAfter = 1.5

        If RdbEnglish.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"

        ElseIf RdbGujarati.Checked Then
            tbl2.Cell(1, 1).Range.Text = "કિંમત - મિનરલ વોટર પ્રોજેક્ટ"
            tbl2.Cell(1, 2).Range.Text = "(કિંમત લાખમાં)"
        ElseIf RdbHindi.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        ElseIf RdbMarathi.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        ElseIf RdbTamil.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        ElseIf RdbTelugu.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        End If


        If RblSingle.Checked = True Then

            tbl2.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(1, 1).Width = 300
            tbl2.Cell(1, 1).Range.Borders.Enable = 0

            tbl2.Cell(1, 2).Range.Borders.Enable = 0
            tbl2.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            tbl2.Cell(1, 2).Width = 180
        End If
        If RblOther.Checked = True Then
            tbl2.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(1, 1).Width = 300
            tbl2.Cell(1, 1).Range.Borders.Enable = 0

            tbl2.Cell(1, 2).Range.Borders.Enable = 0
            tbl2.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            tbl2.Cell(1, 2).Width = 180
        End If
        If RblMultiple.Checked = True Then
            tbl2.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(1, 1).Width = 300
            tbl2.Cell(1, 1).Range.Borders.Enable = 0

            tbl2.Cell(1, 2).Range.Borders.Enable = 0
            tbl2.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            tbl2.Cell(1, 2).Width = 180
        End If
        tbl2.Cell(1, 1).Range.Font.Color = Word.WdColor.wdColorWhite
        tbl2.Cell(1, 2).Range.Font.Color = Word.WdColor.wdColorWhite

        tbl2.Rows.Item(1).Shading.BackgroundPatternColor = RGB(12, 28, 71)

        tbl2.Rows.Item(1).Range.Font.Name = "Arial"
        tbl2.Rows.Item(1).Range.Font.Bold = True 'Indian' 
        tbl2.Rows.Item(1).Range.Font.Size = 14

        tbl2.Cell(2, 1).Range.Borders.Enable = 1
        tbl2.Cell(2, 1).Range.Text = "PRICE : MINERAL WATER PLANT COMPLETE WITH DESIGN, ENGINEERING & SUPPLY"
        tbl2.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        tbl2.Cell(2, 1).Width = 480
        tbl2.Cell(2, 1).Range.Borders.Enable = 1

        tbl2.Rows.Item(2).Range.Font.Name = "Arial"
        tbl2.Rows.Item(2).Range.Font.Bold = True 'Indian' 
        tbl2.Rows.Item(2).Range.Font.Size = 10



        tbl2.Rows.Item(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        '  newRow2.HeadingFormat = 2
        Dim newRow3 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        If RblSingle.Checked = True Then
            tbl2.Rows.Item(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            '            oTable2.Rows.Item(3).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
            If RdbEnglish.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbGujarati.Checked Then
                tbl2.Cell(3, 1).Range.Text = "સિસ્ટમ"
                tbl2.Cell(3, 2).Range.Text = "નંબર"
                tbl2.Cell(3, 3).Range.Text = "માહિતી"
            ElseIf RdbHindi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbMarathi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbTamil.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbTelugu.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            End If
            ' tbl2.Cell(3, 1).Range.Text = "SYSTEM"
            'tbl2.Cell(3, 2).Range.Text = "SR.NO."
            'tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            tbl2.Cell(3, 4).Range.Text = "PRICE"
            tbl2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            tbl2.Rows.Item(3).Range.Font.Size = 9
            tbl2.Cell(3, 3).Width = 275
            tbl2.Cell(3, 1).Width = 80
            tbl2.Cell(3, 2).Width = 45
            tbl2.Cell(3, 4).Width = 80
            tbl2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
            tbl2.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            Dim newRowStatic1st As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1st.Range.Font.Name = "Arial"
            newRowStatic1st.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1st.Range.Font.Size = 9
            newRowStatic1st.Range.Font.Bold = True
            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0


            If RdbEnglish.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "વોટર પ્યુરિફિકેશન સિસ્ટમ"
            ElseIf RdbHindi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTamil.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            End If

            'newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            newRowStatic1st.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1st.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1st.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1st.Cells(3).Width = 275
            newRowStatic1st.Cells(1).Width = 80
            newRowStatic1st.Cells(2).Width = 45
            newRowStatic1st.Cells(4).Width = 80

            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0



        End If

        ''6:29 i am here
        ''5":05 i am here 

        If RblOther.Checked = True Then
            tbl2.Rows.Item(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)

            If RdbEnglish.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            ElseIf RdbGujarati.Checked Then
                tbl2.Cell(3, 1).Range.Text = "સિસ્ટમ"
                tbl2.Cell(3, 2).Range.Text = "નંબર"
                tbl2.Cell(3, 3).Range.Text = "માહિતી"
                tbl2.Cell(3, 4).Range.Text = "જથ્થો"
                tbl2.Cell(3, 5).Range.Text = "કિંમત"
                tbl2.Cell(3, 6).Range.Text = "ટોટલ"
            ElseIf RdbHindi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            ElseIf RdbMarathi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            ElseIf RdbTamil.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            ElseIf RdbTelugu.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            End If


            tbl2.Cell(3, 1).Width = 65
            tbl2.Cell(3, 2).Width = 45
            tbl2.Cell(3, 3).Width = 190
            tbl2.Cell(3, 4).Width = 45
            tbl2.Cell(3, 5).Width = 80
            tbl2.Cell(3, 6).Width = 55

            tbl2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            tbl2.Rows.Item(3).Range.Font.Size = 9
            tbl2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
            tbl2.Cell(3, 6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            Dim newRowStatic1st As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1st.Range.Font.Name = "Arial"
            newRowStatic1st.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1st.Range.Font.Size = 9
            newRowStatic1st.Range.Font.Bold = True

            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0
            newRowStatic1st.Cells(5).Borders.Enable = 0
            newRowStatic1st.Cells(6).Borders.Enable = 0


            If RdbEnglish.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "વોટર પ્યુરિફિકેશન સિસ્ટમ"
            ElseIf RdbHindi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTamil.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            End If
            newRowStatic1st.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1st.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1st.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            newRowStatic1st.Cells(1).Width = 80
            newRowStatic1st.Cells(2).Width = 30
            newRowStatic1st.Cells(3).Width = 190
            newRowStatic1st.Cells(4).Width = 45
            newRowStatic1st.Cells(5).Width = 80
            newRowStatic1st.Cells(6).Width = 55

            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0
            newRowStatic1st.Cells(5).Borders.Enable = 0
            newRowStatic1st.Cells(6).Borders.Enable = 0
        End If

        If RblMultiple.Checked = True Then
            tbl2.Rows.Item(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            If RdbEnglish.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbGujarati.Checked Then
                tbl2.Cell(3, 1).Range.Text = "સિસ્ટમ"
                tbl2.Cell(3, 2).Range.Text = "નંબર"
                tbl2.Cell(3, 3).Range.Text = "માહિતી"
            ElseIf RdbHindi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbMarathi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbTamil.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbTelugu.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            End If
            tbl2.Cell(3, 4).Range.Text = txtCapacity1.Text
            tbl2.Cell(3, 5).Range.Text = txtCapacity2.Text


            tbl2.Cell(3, 1).Width = 70
            tbl2.Cell(3, 2).Width = 45
            tbl2.Cell(3, 3).Width = 265
            tbl2.Cell(3, 4).Width = 50
            tbl2.Cell(3, 5).Width = 50


            tbl2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            tbl2.Rows.Item(3).Range.Font.Size = 9
            tbl2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite

            tbl2.Cell(3, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            Dim newRowStatic1st As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1st.Range.Font.Name = "Arial"
            newRowStatic1st.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1st.Range.Font.Size = 9
            newRowStatic1st.Range.Font.Bold = True


            If RdbEnglish.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "વોટર પ્યુરિફિકેશન સિસ્ટમ"
            ElseIf RdbHindi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTamil.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            End If
            newRowStatic1st.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1st.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1st.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            newRowStatic1st.Cells(1).Width = 70
            newRowStatic1st.Cells(2).Width = 45
            newRowStatic1st.Cells(3).Width = 265
            newRowStatic1st.Cells(4).Width = 50
            newRowStatic1st.Cells(5).Width = 50

            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0
            newRowStatic1st.Cells(5).Borders.Enable = 0

        End If
        Dim finaltotal As New Decimal
        Dim sumall As New Decimal

        Dim sumall2 As New Decimal
        Dim finaltotal1 As New Decimal
        Dim qty As Integer
        qty = 0
        sumall = 0
        sumall2 = 0

        finaltotal = 0
        Dim flgsys As New Integer
        flgsys = 0
        Dim limit As New Integer

        If RdbGujarati.Checked Then
            limit = 4
            objDoc1.Tables(1).Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle
            objDoc1.Tables(1).Range.ParagraphFormat.SpaceAfter = 0
            'objDoc1.Tables(1).Range.ParagraphFormat.LineSpacing = 1
            objDoc1.Tables(1).Rows.Application.Selection.Range.Font.Size = 9
        ElseIf RdbEnglish.Checked Then
            limit = 4
        End If

        For i = 0 To limit
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            ' newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Bold = 0
            'newRow4.Range.Font.Bold = True
            newRow4.Range.Font.Color = Word.WdColor.wdColorBlack
            'newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow4.Range.Borders.Enable = 0
            If i = 2 Then
                If RdbEnglish.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                ElseIf RdbGujarati.Checked Then
                    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૧"
                    'newRow4.Range.ParagraphFormat.SpaceAfter = 0

                ElseIf RdbHindi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                ElseIf RdbMarathi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                ElseIf RdbTamil.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                ElseIf RdbTelugu.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                End If
            End If
            If dt33.Rows.Count > i Then
                If RblSingle.Checked = True Then
                    If flgsys = 0 Then
                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    newRow4.Cells(2).Range.Text = "1." + t.ToString()

                    newRow4.Cells(3).Range.Text = dt33.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = "`   " + dt33.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    'newRow4.Range.Font.Bold = True 'Indian' 
                    newRow4.Range.Font.Size = 9

                    finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dt33.Rows(i)(2).ToString())
                    newRow4.Cells(3).Width = 275
                    newRow4.Cells(1).Width = 80
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(4).Width = 80

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                End If

                If RblOther.Checked = True Then

                    If flgsys = 0 Then
                        flgsys = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        newRow4.Cells(6).Borders.Enable = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "1." + t.ToString()
                    newRow4.Cells(3).Range.Text = dt33.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = dt33.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Text = "`    " + dt33.Rows(i)(2).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Text = "`    " + dt33.Rows(i)(4).ToString()
                    'tbl2.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    'tbl2.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                    'tbl2.Rows.Item(i).Range.Font.Size = 10
                    'tbl2.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue

                    newRow4.Cells(1).Width = 65
                    newRow4.Cells(2).Width = 30
                    newRow4.Cells(3).Width = 215
                    newRow4.Cells(4).Width = 45
                    newRow4.Cells(5).Width = 70
                    newRow4.Cells(6).Width = 55


                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i)(3).ToString())
                    sumall += Convert.ToDecimal(dt33.Rows(i)(3).ToString())
                    qty = qty + Convert.ToDecimal(dt33.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dt33.Rows(i)(4).ToString())
                    sumall2 += Convert.ToDecimal(dt33.Rows(i)(4).ToString())


                End If
                If RblMultiple.Checked = True Then

                    If flgsys = 0 Then

                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    newRow4.Cells(2).Range.Text = "1." + t.ToString()
                    newRow4.Cells(3).Range.Text = dt33.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = "`    " + dt33.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    newRow4.Cells(5).Range.Text = "`    " + dt33.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"


                    newRow4.Cells(1).Width = 70
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(3).Width = 265
                    newRow4.Cells(4).Width = 50
                    newRow4.Cells(5).Width = 50


                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


                    finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dt33.Rows(i)(2).ToString())

                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dt33.Rows(i)(3).ToString())
                    sumall2 += Convert.ToDecimal(dt33.Rows(i)(3).ToString())

                End If
            End If

        Next
        'If RdbEnglish.Checked Then

        '    If dt33.Rows.Count < 5 Then
        '        Dim IK As Integer
        '        IK = 0
        '        IK = 6 - dt33.Rows.Count
        '        For index = 1 To IK
        '            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        '            newRow4.Range.Font.Name = "Arial"
        '            newRow4.Range.Font.Size = 9
        '            newRow4.Cells(1).Range.Text = ""
        '        Next

        '    End If
        'End If

        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9


            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વોટર ટ્રીટમેન્ટ પ્લાન્ટની ટોટલ કિંમત (સિસ્ટમ-૧)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys1total = finaltotal

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            'newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Width = 330
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 80
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


        End If
        If RblOther.Checked = True Then

            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9


            newRow4.Cells(1).Range.Text = ""
            newRow4.Cells(2).Range.Text = ""


            newRow4.Cells(1).Range.Text = "`    " + Convert.ToString(finaltotal)
            newRow4.Cells(1).Range.Font.Name = "Rupee"
            newRow4.Cells(1).Range.Text = ""
            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વોટર ટ્રીટમેન્ટ પ્લાન્ટની ટોટલ કિંમત(સિસ્ટમ-૧)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            newRow4.Cells(6).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys1total = finaltotal1
            newRow4.Cells(6).Range.Font.Name = "Rupee"

            newRow4.Cells(1).Width = 80
            newRow4.Cells(2).Width = 300
            newRow4.Cells(3).Width = 15
            newRow4.Cells(4).Width = 15
            newRow4.Cells(5).Width = 15
            newRow4.Cells(6).Width = 55

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(5).Borders.Enable = 0
            'newRow4.Cells(6).Borders.Enable = 0
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

        End If

        If RblMultiple.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            ''newRow4.Range.Font.Color = Word.WdColor.wdColorDarkBlue
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 8


            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(4).Shading.BackgroundPatternColor = RGB(0, 0, 128)


            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વોટર ટ્રીટમેન્ટ પ્લાન્ટની ટોટલ કિંમત(સિસ્ટમ-૧)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys1total = finaltotal
            newRow4.Cells(4).Range.Font.Name = "Rupee"
            newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys1mutotal = finaltotal1


            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            ' newRow4.Cells(4).Borders.Enable = 0
            ' newRow4.Cells(5).Borders.Enable = 0

            'newRow4.Cells(5).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone



            newRow4.Cells(1).Width = 70
            newRow4.Cells(2).Width = 310
            newRow4.Cells(3).Width = 50
            newRow4.Cells(4).Width = 50
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

        End If

        ''''''''''''''''''Static Text

        If RblSingle.Checked = True Then

            Dim newRowStatic As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic.Cells(3).Width = 245
            newRowStatic.Cells(1).Width = 80
            newRowStatic.Cells(2).Width = 45
            newRowStatic.Cells(4).Width = 110

            newRowStatic.Range.Font.Name = "Arial"
            newRowStatic.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic.Range.Font.Size = 9
            newRowStatic.Range.Font.Bold = True
            newRowStatic.Range.Borders.Enable = 0

            newRowStatic.Cells(1).Borders.Enable = 0
            newRowStatic.Cells(2).Borders.Enable = 0
            newRowStatic.Cells(4).Borders.Enable = 0
            newRowStatic.Cells(3).Borders.Enable = 0
            newRowStatic.Cells(1).Width = 80
            newRowStatic.Cells(2).Width = 45
            newRowStatic.Cells(4).Width = 110

            If RdbEnglish.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbGujarati.Checked Then
                newRowStatic.Cells(3).Range.Text = "ક્વાલિટી કંટ્રોલ લેબોરેટરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbMarathi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTamil.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTelugu.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            End If

            'newRowStatic.Cells(3).Width = 480
            newRowStatic.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '  newRowStatic.Range.Borders.Enable = 0

        ElseIf RblOther.Checked = True Then

            Dim newRowStatic As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic.Cells(1).Width = 80
            newRowStatic.Cells(2).Width = 30
            newRowStatic.Cells(3).Width = 190
            newRowStatic.Cells(4).Width = 45
            newRowStatic.Cells(5).Width = 80
            newRowStatic.Cells(6).Width = 55

            'newRowStatic.Cells(1).Width = 65
            'newRowStatic.Cells(2).Width = 30
            'newRowStatic.Cells(3).Width = 215
            'newRowStatic.Cells(4).Width = 45
            'newRowStatic.Cells(5).Width = 70
            'newRowStatic.Cells(6).Width = 55


            newRowStatic.Range.Font.Name = "Arial"
            newRowStatic.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic.Range.Font.Size = 9
            newRowStatic.Range.Font.Bold = True
            newRowStatic.Cells(1).Borders.Enable = 0
            newRowStatic.Cells(2).Borders.Enable = 0
            newRowStatic.Cells(4).Borders.Enable = 0
            newRowStatic.Cells(3).Borders.Enable = 0
            newRowStatic.Cells(5).Borders.Enable = 0
            newRowStatic.Cells(6).Borders.Enable = 0

            '  newRowStatic.Range.Borders.Enable = 0
            If RdbEnglish.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbGujarati.Checked Then
                newRowStatic.Cells(3).Range.Text = "ક્વાલિટી કંટ્રોલ લેબોરેટરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbMarathi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTamil.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTelugu.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            End If
            newRowStatic.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '   newRowStatic.Range.Borders.Enable = 0

        ElseIf RblMultiple.Checked = True Then

            Dim newRowStatic As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic.Cells(1).Width = 70
            newRowStatic.Cells(2).Width = 45
            newRowStatic.Cells(3).Width = 265
            newRowStatic.Cells(4).Width = 50
            newRowStatic.Cells(5).Width = 50



            newRowStatic.Range.Font.Name = "Arial"
            newRowStatic.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic.Range.Font.Size = 9
            newRowStatic.Range.Font.Bold = True
            '  newRowStatic.Range.Borders.Enable = 0
            If RdbEnglish.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbGujarati.Checked Then
                newRowStatic.Cells(3).Range.Text = "ક્વાલિટી કંટ્રોલ લેબોરેટરીની ટોટલ કિંમત"
            ElseIf RdbHindi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbMarathi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTamil.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTelugu.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            End If
            ' newRowStatic.Cells(1).Width = 480
            newRowStatic.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '   newRowStatic.Range.Borders.Enable = 0
            newRowStatic.Cells(1).Borders.Enable = 0
            newRowStatic.Cells(2).Borders.Enable = 0
            newRowStatic.Cells(4).Borders.Enable = 0
            newRowStatic.Cells(3).Borders.Enable = 0
            newRowStatic.Cells(5).Borders.Enable = 0

        End If

        finaltotal = 0
        finaltotal1 = 0

        If RdbGujarati.Checked Then
            limit = 3
        ElseIf RdbEnglish.Checked Then
            limit = 3
        End If

        flgsys = 0
        For i = 0 To limit
            'For i = 0 To dtSys2.Rows.Count - 1
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Borders.Enable = 0
            newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow4.Range.Font.Bold = 0
            '  newRow4.Range.Font.Bold = True 'Indian' 
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Color = Word.WdColor.wdColorBlack
            newRow4.Range.Font.Name = "Arial"
            If i = 2 Then
                'If dtSys2.Rows.Count < 4 Then
                '    Dim IK As Integer
                If RdbEnglish.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                ElseIf RdbGujarati.Checked Then
                    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૨"
                ElseIf RdbHindi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                ElseIf RdbMarathi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                ElseIf RdbTamil.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                ElseIf RdbTelugu.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                End If
                'newRow4.Cells(1).Range.Text = "SYS-2"
                'newRow4.Range.Borders.Enable = 1
            End If

            If dtSys2.Rows.Count > i Then

                If RblSingle.Checked = True Then

                    If flgsys = 0 Then
                        '   newRow4.Cells(1).Range.Text = "SYS-2"
                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        flgsys = 1
                    End If
                    If i = 2 Then
                        newRow4.Cells(2).Range.Font.Name = "Arial"
                        newRow4.Cells(4).Range.Font.Name = "Rupee"

                        'If dtSys2.Rows.Count < 4 Then
                        '    Dim IK As Integer
                        If RdbEnglish.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbGujarati.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbHindi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbMarathi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbTamil.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbTelugu.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        End If
                        'newRow4.Cells(1).Range.Text = "SYS-2"
                        'newRow4.Range.Borders.Enable = 1
                    Else
                        newRow4.Cells(2).Range.Text = ""
                        newRow4.Cells(4).Range.Text = ""
                    End If

                    ' newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                    newRow4.Cells(3).Range.Text = dtSys2.Rows(i)(1).ToString()
                    'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                    'newRow4.Cells(4).Range.Font.Name = "Rupee"

                    newRow4.Range.Font.Size = 9

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys2.Rows(i)("Price").ToString())
                    sumall += Convert.ToDecimal(dtSys2.Rows(i)("Price").ToString())

                    newRow4.Cells(3).Width = 275
                    newRow4.Cells(1).Width = 80
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(4).Width = 80

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.Font.Bold = True

                End If

                If RblOther.Checked = True Then
                    If flgsys = 0 Then
                        ' newRow4.Cells(1).Range.Text = "SYS-2"
                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        newRow4.Cells(6).Borders.Enable = 1

                        flgsys = 1
                    End If

                    If i = 2 Then
                        newRow4.Cells(2).Range.Font.Name = "Arial"
                        newRow4.Cells(4).Range.Font.Name = "Arial"
                        newRow4.Cells(5).Range.Font.Name = "Rupee"
                        newRow4.Cells(6).Range.Font.Name = "Rupee"
                        'If dtSys2.Rows.Count < 4 Then
                        '    Dim IK As Integer
                        If RdbEnglish.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                        ElseIf RdbGujarati.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                        ElseIf RdbHindi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()

                        ElseIf RdbMarathi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                        ElseIf RdbTamil.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                        ElseIf RdbTelugu.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                        End If
                        'newRow4.Cells(1).Range.Text = "SYS-2"
                        'newRow4.Range.Borders.Enable = 1
                    Else
                        newRow4.Cells(2).Range.Text = ""
                        newRow4.Cells(4).Range.Text = ""
                        newRow4.Cells(5).Range.Text = ""
                        newRow4.Cells(6).Range.Text = ""

                    End If


                    '  newRow4.Cells(2).Range.Text = "2." + dtSys2.Rows(i)(0).ToString()
                    newRow4.Cells(3).Range.Text = dtSys2.Rows(i)(1).ToString()
                    'newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                    'newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                    'newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()

                    newRow4.Range.Font.Size = 9

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(5).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorBlack

                    'newRow4.Cells(1).Width = 80
                    'newRow4.Cells(2).Width = 30
                    'newRow4.Cells(3).Width = 190
                    'newRow4.Cells(4).Width = 45
                    'newRow4.Cells(5).Width = 80
                    'newRow4.Cells(6).Width = 55


                    newRow4.Cells(1).Width = 65
                    newRow4.Cells(2).Width = 30
                    newRow4.Cells(3).Width = 215
                    newRow4.Cells(4).Width = 45
                    newRow4.Cells(5).Width = 70
                    newRow4.Cells(6).Width = 55


                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.Font.Bold = True

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys2.Rows(i)(3).ToString())
                    sumall += Convert.ToDecimal(dtSys2.Rows(i)(3).ToString())

                    qty = qty + Convert.ToDecimal(dtSys2.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys2.Rows(i)(4).ToString())
                    sumall2 += Convert.ToDecimal(dtSys2.Rows(i)(4).ToString())

                End If
                If RblMultiple.Checked = True Then
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    If flgsys = 0 Then
                        newRow4.Range.Borders.Enable = 1
                        '  newRow4.Cells(1).Range.Text = "SYS-2"
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1

                        flgsys = 1
                    End If

                    If i = 2 Then
                        newRow4.Cells(2).Range.Font.Name = "Arial"
                        newRow4.Cells(4).Range.Font.Name = "Rupee"

                        'If dtSys2.Rows.Count < 4 Then
                        '    Dim IK As Integer
                        If RdbEnglish.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            ' newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbGujarati.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbHindi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbMarathi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbTamil.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbTelugu.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            '     newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        End If
                        'newRow4.Cells(1).Range.Text = "SYS-2"
                        'newRow4.Range.Borders.Enable = 1
                    Else
                        newRow4.Cells(2).Range.Text = ""
                        newRow4.Cells(4).Range.Text = ""
                        newRow4.Cells(5).Range.Text = ""

                    End If

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack


                    '   newRow4.Cells(2).Range.Text = "2." + dtSys2.Rows(i)(0).ToString()
                    newRow4.Cells(3).Range.Text = dtSys2.Rows(i)(1).ToString()

                    newRow4.Range.Font.Size = 9

                    newRow4.Cells(1).Width = 70
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(3).Width = 265
                    newRow4.Cells(4).Width = 50
                    newRow4.Cells(5).Width = 50


                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.Font.Bold = True


                    finaltotal = finaltotal + Convert.ToDecimal(dtSys2.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys2.Rows(i)(2).ToString())

                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys2.Rows(i)(3).ToString())
                    sumall2 += Convert.ToDecimal(dtSys2.Rows(i)(3).ToString())

                End If
            End If

        Next


        'If RdbEnglish.Checked Then

        '    If dtSys2.Rows.Count < 4 Then
        '        Dim IK As Integer
        '        IK = 0
        '        IK = 6 - dtSys2.Rows.Count
        '        For index = 1 To IK
        '            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        '            newRow4.Range.Font.Name = "Arial"
        '            newRow4.Range.Font.Size = 9
        '            newRow4.Cells(1).Range.Text = ""
        '        Next

        '    End If
        'End If

        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            For il = 0 To dtSys2.Rows.Count - 1
                finaltotal = dtSys2.Rows(il)(2).ToString()
            Next

            '            newRow4.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "ક્વોલિટી કન્ટ્રોલ લેબોરેટરીની ટોટલ કિમત (સિસ્ટમ-૨)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            End If
            ' newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys2total = finaltotal


            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            'newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Width = 330
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 80
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            'newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


        End If
        If RblOther.Checked = True Then
            For il = 0 To dtSys2.Rows.Count - 1
                finaltotal = dtSys2.Rows(il)(2).ToString()
                finaltotal1 = dtSys2.Rows(il)(3).ToString()
            Next

            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Bold = True

            newRow4.Cells(1).Range.Text = ""
            newRow4.Cells(2).Range.Text = ""




            newRow4.Cells(1).Range.Text = "`    " + Convert.ToString(finaltotal)
            newRow4.Cells(1).Range.Font.Name = "Rupee"
            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "ક્વોલિટી કન્ટ્રોલ લેબોરેટરીની ટોટલ કિમત (સિસ્ટમ-૨)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            End If
            newRow4.Cells(6).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys2total = finaltotal1
            newRow4.Cells(6).Range.Font.Name = "Rupee"

            newRow4.Cells(1).Width = 80
            newRow4.Cells(2).Width = 300
            newRow4.Cells(3).Width = 15
            newRow4.Cells(4).Width = 15
            newRow4.Cells(5).Width = 15
            newRow4.Cells(6).Width = 55

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(5).Borders.Enable = 0
            'newRow4.Cells(6).Borders.Enable = 0
            newRow4.Range.Borders.Enable = 0
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '            newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If



        If RblMultiple.Checked = True Then
            For il = 0 To dtSys2.Rows.Count - 1
                finaltotal = dtSys2.Rows(il)(2).ToString()
                finaltotal1 = dtSys2.Rows(il)(3).ToString()
            Next

            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Font.Name = "Arial"

            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(4).Shading.BackgroundPatternColor = RGB(0, 0, 128)

            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "ક્વોલિટી કન્ટ્રોલ લેબોરેટરીની ટોટલ કિમત (સિસ્ટમ-૨)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            End If
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys2total = finaltotal
            newRow4.Cells(4).Range.Font.Name = "Rupee"
            newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys2mutotal = finaltotal1

            newRow4.Cells(2).Width = 310
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 50
            newRow4.Cells(4).Width = 50
            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            '  newRow4.Cells(4).Borders.Enable = 0
            '  newRow4.Cells(5).Borders.Enable = 0
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(2).Range.Font.Size = 8

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            '           newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If


        If RblSingle.Checked Then
            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વોશિંગ,ફીલિંગ અને પેકિંગ મશીનરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            End If
            'newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            'newRowStatic1.Range.Borders.Enable = 1
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(3).Width = 275
            newRowStatic1.Cells(1).Width = 80
            newRowStatic1.Cells(2).Width = 45
            newRowStatic1.Cells(4).Width = 80

            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0

        ElseIf RblOther.Checked Then
            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વોશિંગ,ફીલિંગ અને પેકિંગ મશીનરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            End If
            newRowStatic1.Range.Borders.Enable = 0
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(1).Width = 80
            newRowStatic1.Cells(2).Width = 30
            newRowStatic1.Cells(3).Width = 295
            newRowStatic1.Cells(4).Width = 25
            newRowStatic1.Cells(5).Width = 25
            newRowStatic1.Cells(6).Width = 25



            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(5).Borders.Enable = 0
            newRowStatic1.Cells(6).Borders.Enable = 0


        ElseIf RblMultiple.Checked Then
            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વોશિંગ,ફીલિંગ અને પેકિંગ મશીનરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            End If
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(1).Width = 70
            newRowStatic1.Cells(2).Width = 45
            newRowStatic1.Cells(3).Width = 300
            newRowStatic1.Cells(4).Width = 15
            newRowStatic1.Cells(5).Width = 50

            newRowStatic1.Cells(5).Borders.Enable = 0
            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0

        End If




        finaltotal = 0
        finaltotal1 = 0
        If RdbGujarati.Checked Then
            limit = 6
        ElseIf RdbEnglish.Checked Then

            limit = 6
        End If

        flgsys = 0
        For i = 0 To limit
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow4.Range.Font.Bold = 0
            'newRow4.Range.Font.Bold = True 'Indian' 
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Color = Word.WdColor.wdColorBlack
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Borders.Enable = 0
            newRow4.Cells(1).Range.Text = ""
            If i = 2 Then
                If RdbEnglish.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                ElseIf RdbGujarati.Checked Then
                    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૩"
                ElseIf RdbHindi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                ElseIf RdbMarathi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                ElseIf RdbTamil.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                ElseIf RdbTelugu.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                End If
            End If
            If dtSys3.Rows.Count > i Then

                If RblSingle.Checked = True Then
                    If flgsys = 0 Then
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    newRow4.Cells(2).Range.Text = "3." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)("Description").ToString()
                    newRow4.Cells(4).Range.Text = "`   " + dtSys3.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    finaltotal = finaltotal + Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())
                    newRow4.Cells(3).Width = 275
                    newRow4.Cells(1).Width = 80
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(4).Width = 80

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    newRow4 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)(3).ToString()
                    newRow4.Range.Borders.Enable = 0


                End If

                If RblOther.Checked = True Then
                    If flgsys = 0 Then

                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        newRow4.Cells(6).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "3." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)("Description").ToString()
                    newRow4.Cells(4).Range.Text = dtSys3.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Text = "`    " + dtSys3.Rows(i)(2).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Text = "`    " + dtSys3.Rows(i)(4).ToString()
                    'newRow4.Cells(6).Range.Text = "`    " + dtSys3.Rows(i)(4).ToString()

                    ''newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    newRow4.Range.Font.Bold = 0
                    'newRow4.Range.Font.Bold = True 'Indian' 
                    ' newRow4.Range.Font.Size = 10

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(5).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorBlack

                    'newRow4.Cells(1).Width = 80
                    'newRow4.Cells(2).Width = 30
                    'newRow4.Cells(3).Width = 190
                    'newRow4.Cells(4).Width = 45
                    'newRow4.Cells(5).Width = 80
                    'newRow4.Cells(6).Width = 55

                    newRow4.Cells(1).Width = 65
                    newRow4.Cells(2).Width = 30
                    newRow4.Cells(3).Width = 215
                    newRow4.Cells(4).Width = 45
                    newRow4.Cells(5).Width = 70
                    newRow4.Cells(6).Width = 55

                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys3.Rows(i)(3).ToString())
                    sumall += Convert.ToDecimal(dtSys3.Rows(i)(3).ToString())

                    qty = qty + Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys3.Rows(i)(4).ToString())
                    sumall2 += Convert.ToDecimal(dtSys3.Rows(i)(4).ToString())
                    newRow4 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)("Capacity").ToString()
                    newRow4.Range.Borders.Enable = 0
                End If
                If RblMultiple.Checked = True Then

                    If flgsys = 0 Then

                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        flgsys = 1
                    End If

                    Dim t As Integer
                    t = i + 1
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "3." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)("Description").ToString()
                    newRow4.Cells(4).Range.Text = "`    " + dtSys3.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    newRow4.Cells(5).Range.Text = "`    " + dtSys3.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"

                    newRow4.Cells(1).Width = 70
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(3).Width = 265
                    newRow4.Cells(4).Width = 50
                    newRow4.Cells(5).Width = 50

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    newRow4 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)(4).ToString()
                    newRow4.Range.Borders.Enable = 0

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())

                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys3.Rows(i)(3).ToString())
                    sumall2 += Convert.ToDecimal(dtSys3.Rows(i)(3).ToString())

                End If
            End If

        Next
        'If RdbEnglish.Checked Then

        '    If dtSys3.Rows.Count < 12 Then
        '        Dim IK As Integer
        '        IK = 0
        '        IK = 12 - dtSys3.Rows.Count
        '        For index = 1 To IK
        '            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        '            newRow4.Range.Font.Name = "Arial"
        '            newRow4.Range.Font.Size = 9
        '            newRow4.Cells(1).Range.Text = ""
        '        Next

        '    End If

        'End If



        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.ParagraphFormat.SpaceBefore = 0
            newRow4.Range.ParagraphFormat.SpaceAfter = 0
            newRow4.Range.ParagraphFormat.SpaceAfter = 0.5
            newRow4.Range.ParagraphFormat.SpaceBefore = 0.5

            '            newRow4.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "પેકિંગ મશીન ની ટોટલ કિમત (સિસ્ટમ-૩)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            End If

            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys3total = finaltotal


            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(1).Range.Font.Size = 12

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Width = 330
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 80
            'newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            ' newRow4.Borders.Enable = 0
            '  newRow4.Borders.Enable = 0
            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            'newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


        End If
        If RblOther.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Bold = True
            newRow4.Cells(1).Range.Text = ""
            newRow4.Cells(2).Range.Text = ""


            newRow4.Cells(1).Range.Text = "`    " + Convert.ToString(finaltotal)
            newRow4.Cells(1).Range.Font.Name = "Rupee"
            newRow4.Cells(1).Range.Text = ""
            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "પેકિંગ મશીન ની ટોટલ કિમત (સિસ્ટમ-૩)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            newRow4.Cells(6).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys3total = finaltotal1

            newRow4.Cells(6).Range.Font.Name = "Rupee"

            newRow4.Cells(1).Width = 80
            newRow4.Cells(2).Width = 300
            newRow4.Cells(3).Width = 15
            newRow4.Cells(4).Width = 15
            newRow4.Cells(5).Width = 15
            newRow4.Cells(6).Width = 55

            newRow4.Range.Borders.Enable = 0
            newRow4.Borders.Enable = 0
            newRow4.Borders.Enable = 0
            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(5).Borders.Enable = 0
            'newRow4.Cells(6).Borders.Enable = 0

            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            '            newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If

        If RblMultiple.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(4).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "પેકિંગ મશીન ની ટોટલ કિમત (સિસ્ટમ-૩)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            End If
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys3total = finaltotal

            newRow4.Cells(4).Range.Font.Name = "Rupee"
            newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys3mutotal = finaltotal1



            newRow4.Cells(2).Width = 310
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 50
            newRow4.Cells(4).Width = 50

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            '  newRow4.Cells(4).Borders.Enable = 0

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Size = 8
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            '           newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If

        If RblSingle.Checked Then

            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            End If
            'newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            'newRowStatic1.Range.Borders.Enable = 1
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(3).Width = 275
            newRowStatic1.Cells(1).Width = 80
            newRowStatic1.Cells(2).Width = 45
            newRowStatic1.Cells(4).Width = 80

            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        ElseIf RblOther.Checked Then

            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True

            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            End If
            'newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"

            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(1).Width = 80
            newRowStatic1.Cells(2).Width = 30
            newRowStatic1.Cells(3).Width = 190
            newRowStatic1.Cells(4).Width = 45
            newRowStatic1.Cells(5).Width = 80
            newRowStatic1.Cells(6).Width = 55


            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(5).Borders.Enable = 0
            newRowStatic1.Cells(6).Borders.Enable = 0
            newRowStatic1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(5).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(6).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle



        ElseIf RblMultiple.Checked Then



            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            End If
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(1).Width = 70
            newRowStatic1.Cells(2).Width = 45
            newRowStatic1.Cells(3).Width = 265
            newRowStatic1.Cells(4).Width = 50
            newRowStatic1.Cells(5).Width = 50

            newRowStatic1.Cells(5).Borders.Enable = 0
            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0

            newRowStatic1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(5).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        End If




        finaltotal = 0
        finaltotal1 = 0
        If RdbEnglish.Checked Then

            limit = 5
        ElseIf RdbGujarati.Checked Then
            limit = 5
        End If

        flgsys = 0
        For i = 0 To limit


            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow4.Range.Font.Bold = 0
            '   newRow4.Range.Font.Bold = True 'Indian' 
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Color = Word.WdColor.wdColorBlack
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Borders.Enable = 0
            newRow4.Cells(1).Range.Text = ""
            If i = 3 Then
                If RdbEnglish.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                ElseIf RdbGujarati.Checked Then
                    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૪"
                    'newRow4.Range.ParagraphFormat.SpaceAfter = 0
                    'newRow4.Range.ParagraphFormat.SpaceBefore = 0

                ElseIf RdbHindi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                ElseIf RdbMarathi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                ElseIf RdbTamil.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                ElseIf RdbTelugu.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                End If
            End If

            If dtSys4.Rows.Count > i Then

                If RblSingle.Checked = True Then
                    If flgsys = 0 Then
                        'If RdbEnglish.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'ElseIf RdbGujarati.Checked Then
                        '    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૪"
                        'ElseIf RdbHindi.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'ElseIf RdbMarathi.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'ElseIf RdbTamil.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'ElseIf RdbTelugu.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'End If
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1

                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    newRow4.Cells(2).Range.Text = "4." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys4.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = "`   " + dtSys4.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    finaltotal = finaltotal + Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())


                    newRow4.Cells(3).Width = 275
                    newRow4.Cells(1).Width = 80
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(4).Width = 80

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                End If

                If RblOther.Checked = True Then
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    If flgsys = 0 Then

                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        newRow4.Cells(6).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "4." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys4.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = dtSys4.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Text = "`    " + dtSys4.Rows(i)(2).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Text = "`    " + dtSys4.Rows(i)(4).ToString()
                    ''newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    newRow4.Range.Font.Bold = 0
                    ' newRow4.Range.Font.Bold = True 'Indian' 
                    '  newRow4.Range.Font.Size = 10

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(5).Range.Font.Color = Word.WdColor.wdColorBlack

                    'newRow4.Cells(1).Width = 80
                    'newRow4.Cells(2).Width = 30
                    'newRow4.Cells(3).Width = 190
                    'newRow4.Cells(4).Width = 45
                    'newRow4.Cells(5).Width = 80
                    'newRow4.Cells(6).Width = 55
                    newRow4.Cells(1).Width = 65
                    newRow4.Cells(2).Width = 30
                    newRow4.Cells(3).Width = 215
                    newRow4.Cells(4).Width = 45
                    newRow4.Cells(5).Width = 70
                    newRow4.Cells(6).Width = 55

                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys4.Rows(i)(3).ToString())
                    sumall += Convert.ToDecimal(dtSys4.Rows(i)(3).ToString())

                    qty = qty + Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys4.Rows(i)(4).ToString())
                    sumall2 += Convert.ToDecimal(dtSys4.Rows(i)(4).ToString())

                End If
                If RblMultiple.Checked = True Then
                    If flgsys = 0 Then

                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1

                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1

                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "4." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys4.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = "`    " + dtSys4.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    newRow4.Cells(5).Range.Text = "`    " + dtSys4.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"

                    newRow4.Cells(1).Width = 70
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(3).Width = 265
                    newRow4.Cells(4).Width = 50
                    newRow4.Cells(5).Width = 50

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


                    finaltotal = finaltotal + Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys4.Rows(i)(3).ToString())
                    sumall2 += Convert.ToDecimal(dtSys4.Rows(i)(3).ToString())

                End If
            End If
        Next

        'If RdbEnglish.Checked Then

        '    If dtSys4.Rows.Count < 5 Then
        '        Dim IK As Integer
        '        IK = 0
        '        IK = 7 - dtSys4.Rows.Count
        '        For index = 1 To IK
        '            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        '            newRow4.Range.Font.Name = "Arial"
        '            newRow4.Range.Font.Size = 9
        '            newRow4.Cells(1).Range.Text = ""
        '        Next

        '    End If
        'End If

        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            '            newRow4.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજીની ટોટલ કિંમત (સિસ્ટમ-૪)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys4total = finaltotal


            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Width = 330
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 80
            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0

            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            'newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True

        End If
        If RblOther.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Bold = True

            newRow4.Cells(1).Range.Text = ""
            newRow4.Cells(2).Range.Text = ""


            newRow4.Cells(1).Range.Text = "`    " + Convert.ToString(finaltotal)
            newRow4.Cells(1).Range.Font.Name = "Rupee"
            newRow4.Cells(1).Range.Text = ""
            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજીની ટોટલ કિંમત (સિસ્ટમ-૪)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            End If

            newRow4.Cells(6).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys4total = finaltotal1
            newRow4.Cells(6).Range.Font.Name = "Rupee"


            newRow4.Cells(1).Width = 80
            newRow4.Cells(2).Width = 300
            newRow4.Cells(3).Width = 15
            newRow4.Cells(4).Width = 15
            newRow4.Cells(5).Width = 15
            newRow4.Cells(6).Width = 55

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(5).Borders.Enable = 0
            '  newRow4.Cells(6).Borders.Enable = 0
            newRow4.Range.Borders.Enable = 0
            newRow4.Range.Borders.Enable = 0

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
        End If

        If RblMultiple.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(4).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"

            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજીની ટોટલ કિંમત (સિસ્ટમ-૪)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            End If

            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys4total = finaltotal
            newRow4.Cells(4).Range.Font.Name = "Rupee"
            newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys4mutotal = finaltotal1

            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(2).Width = 310
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 50
            newRow4.Cells(4).Width = 50

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            'newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Size = 8

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            '           newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If
        sumall = sys1total + sys2total + sys3total + sys4total
        sumall2 = sys1mutotal + sys2mutotal + sys3mutotal + sys4mutotal


        If RblSingle.Checked = True Then

            Dim newRow4BLueFooter As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            If RdbEnglish.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"

            ElseIf RdbGujarati.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોડ્કટની ટોટલ કિંમત(સિસ્ટમ 1+2+3+4)"

            ElseIf RdbHindi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"

            ElseIf RdbMarathi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"

            ElseIf RdbTamil.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
            ElseIf RdbTelugu.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
            End If
            'If RblPriceYes.Checked Then
            '    newRow4BLueFooter.Cells(4).Range.Text = "` " + Convert.ToString(sys1total + sys2total + sys3total + sys4total)
            'Else
            '    newRow4BLueFooter.Cells(4).Range.Text = ""
            'End If

            newRow4BLueFooter.Cells(4).Range.Text = "` " + Convert.ToString(sys1total + sys2total + sys3total + sys4total)

            'newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોડ્કટની ટોટલ કિંમત(સિસ્ટમ 1+2+3+4)"
            newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
            newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"

            newRow4BLueFooter.Cells(1).Width = 340
            newRow4BLueFooter.Cells(2).Width = 15
            newRow4BLueFooter.Cells(3).Width = 45
            newRow4BLueFooter.Cells(4).Width = 80


            newRow4BLueFooter.Cells(1).Range.Font.Size = 9
            newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            newRow4BLueFooter.Range.Borders.Enable = 0
            newRow4BLueFooter.Cells(1).Borders.Enable = 0
            newRow4BLueFooter.Cells(2).Borders.Enable = 0
            newRow4BLueFooter.Cells(3).Borders.Enable = 0
            newRow4BLueFooter.Cells(4).Borders.Enable = 0


            newRow4BLueFooter.Cells(1).Range.Font.Size = 9
            newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            newRow4BLueFooter.Range.Borders.Enable = 0
            newRow4BLueFooter.Cells(1).Borders.Enable = 0
            newRow4BLueFooter.Cells(2).Borders.Enable = 0
            newRow4BLueFooter.Cells(3).Borders.Enable = 0
            newRow4BLueFooter.Cells(4).Borders.Enable = 0



            If txtspdiscount.Text.Trim() <> "" Then
                newRow4BLueFooter = objDoc1.Tables(1).Rows.Add(Type.Missing)

                newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbGujarati.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "સ્પેશિયલ ડિસ્કાઉન્ટ==>>"
                ElseIf RdbHindi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbMarathi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTamil.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTelugu.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                End If
                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(4).Range.Text = "` " + txtspdiscount.Text.Trim()
                newRow4BLueFooter.Cells(1).Range.Font.Size = 9
                newRow4BLueFooter.Cells(1).Width = 340
                newRow4BLueFooter.Cells(2).Width = 15
                newRow4BLueFooter.Cells(3).Width = 45
                newRow4BLueFooter.Cells(4).Width = 80
                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4BLueFooter.Cells(1).Range.Font.Bold = True

                newRow4BLueFooter = objDoc1.Tables(1).Rows.Add(Type.Missing)
                Dim finaled As Decimal

                finaled = Convert.ToDecimal(txtspdiscount.Text.Trim())
                newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbGujarati.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોજેક્ટ ની અંતીમ  કિમત"
                ElseIf RdbHindi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbMarathi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTamil.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTelugu.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                End If

                sumall = sumall - finaled
                newRow4BLueFooter.Cells(4).Range.Text = "` " + Convert.ToString(sumall)
                newRow4BLueFooter.Cells(1).Width = 340
                newRow4BLueFooter.Cells(2).Width = 15
                newRow4BLueFooter.Cells(3).Width = 45
                newRow4BLueFooter.Cells(4).Width = 80

                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

                newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            End If


        End If
        If RblOther.Checked = True Then

            Dim newRow4BLueFooter As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            If RdbEnglish.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
            ElseIf RdbGujarati.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોડ્કટની ટોટલ કિંમત(સિસ્ટમ 1+2+3+4)"
            ElseIf RdbHindi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
            ElseIf RdbMarathi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
            ElseIf RdbTamil.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
            ElseIf RdbTelugu.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"

            End If
            If RblPriceYes.Checked Then
                newRow4BLueFooter.Cells(6).Range.Text = "`" + Convert.ToString(sumall)
            Else
                newRow4BLueFooter.Cells(6).Range.Text = ""
            End If
            newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(2).Range.Font.Name = "Arial"
            newRow4BLueFooter.Cells(6).Range.Font.Name = "Rupee"
            newRow4BLueFooter.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(1).Width = 365
            newRow4BLueFooter.Cells(2).Width = 15
            newRow4BLueFooter.Cells(3).Width = 15
            newRow4BLueFooter.Cells(4).Width = 15
            newRow4BLueFooter.Cells(5).Width = 15
            newRow4BLueFooter.Cells(6).Width = 55


            newRow4BLueFooter.Cells(1).Range.Font.Size = 9
            newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            newRow4BLueFooter.Range.Borders.Enable = 0
            newRow4BLueFooter.Cells(1).Borders.Enable = 0
            newRow4BLueFooter.Cells(2).Borders.Enable = 0
            newRow4BLueFooter.Cells(3).Borders.Enable = 0
            newRow4BLueFooter.Cells(4).Borders.Enable = 0
            newRow4BLueFooter.Cells(5).Borders.Enable = 0
            newRow4BLueFooter.Cells(6).Borders.Enable = 0


            If txtspdiscount.Text.Trim() <> "" Then
                newRow4BLueFooter = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbGujarati.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "સ્પેશિયલ ડિસ્કાઉન્ટ==>>"
                ElseIf RdbHindi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbMarathi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTamil.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTelugu.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                End If
                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(6).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(6).Range.Text = "` " + txtspdiscount.Text.Trim()
                newRow4BLueFooter.Cells(1).Range.Font.Size = 9
                newRow4BLueFooter.Cells(1).Width = 375
                newRow4BLueFooter.Cells(2).Width = 15
                newRow4BLueFooter.Cells(3).Width = 15
                newRow4BLueFooter.Cells(4).Width = 45
                newRow4BLueFooter.Cells(5).Width = 70
                newRow4BLueFooter.Cells(6).Width = 55
                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
                newRow4BLueFooter.Cells(6).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4BLueFooter.Cells(1).Range.Font.Bold = True

                newRow4BLueFooter = objDoc1.Tables(1).Rows.Add(Type.Missing)
                Dim finaled As Decimal

                finaled = Convert.ToDecimal(txtspdiscount.Text.Trim())
                newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbGujarati.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "અંતીમ મિનરલ વોટર પ્રોજેક્ટ ની કિમત"
                ElseIf RdbHindi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbMarathi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTamil.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTelugu.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                End If
                sumall = sys1total + sys2total + sys3total + sys4total
                sumall = sumall - finaled
                newRow4BLueFooter.Cells(6).Range.Text = "` " + Convert.ToString(sumall)
                newRow4BLueFooter.Cells(1).Width = 375
                newRow4BLueFooter.Cells(2).Width = 15
                newRow4BLueFooter.Cells(3).Width = 15
                newRow4BLueFooter.Cells(4).Width = 45
                newRow4BLueFooter.Cells(5).Width = 70
                newRow4BLueFooter.Cells(6).Width = 55

                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite

                newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
                newRow4BLueFooter.Cells(6).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            End If



        End If

        If RblMultiple.Checked = True Then

            Dim newRow4BLueFooter As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            If RdbEnglish.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)
            ElseIf RdbGujarati.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોડ્કટની ટોટલ કિંમત(સિસ્ટમ 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            ElseIf RdbHindi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            ElseIf RdbMarathi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            ElseIf RdbTamil.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            ElseIf RdbTelugu.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            End If
            newRow4BLueFooter.Cells(4).Range.Text = "` " + Convert.ToString(sumall)
            newRow4BLueFooter.Cells(5).Range.Text = "` " + Convert.ToString(sumall2)
            newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
            newRow4BLueFooter.Cells(5).Range.Font.Name = "Rupee"

            newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(5).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
            newRow4BLueFooter.Cells(1).Width = 350
            newRow4BLueFooter.Cells(2).Width = 15
            newRow4BLueFooter.Cells(3).Width = 15
            newRow4BLueFooter.Cells(4).Width = 50
            newRow4BLueFooter.Cells(5).Width = 50
            newRow4BLueFooter.Cells(1).Range.Font.Size = 9
            newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            newRow4BLueFooter.Range.Borders.Enable = 0
            newRow4BLueFooter.Cells(1).Borders.Enable = 0
            newRow4BLueFooter.Cells(2).Borders.Enable = 0
            newRow4BLueFooter.Cells(3).Borders.Enable = 0
            newRow4BLueFooter.Cells(4).Borders.Enable = 0
            newRow4BLueFooter.Cells(5).Borders.Enable = 0

            If txtspdiscount.Text.Trim() <> "" Then
                Dim trdiscount As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

                trdiscount.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbGujarati.Checked Then
                    trdiscount.Cells(1).Range.Text = "સ્પેશિયલ ડિસ્કાઉન્ટ==>>"
                ElseIf RdbHindi.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbMarathi.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTamil.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTelugu.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                End If
                trdiscount.Cells(4).Range.Text = txtspdiscount.Text.Trim()
                trdiscount.Cells(5).Range.Text = txtspdiscount.Text.Trim()
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(5).Range.Font.Name = "Rupee"
                trdiscount.Cells(1).Width = 350
                trdiscount.Cells(2).Width = 15
                trdiscount.Cells(3).Width = 15
                trdiscount.Cells(4).Width = 50
                trdiscount.Cells(5).Width = 50
                trdiscount.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                trdiscount.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
                trdiscount.Cells(5).Range.Font.Color = Word.WdColor.wdColorWhite

                trdiscount.Cells(1).Range.Font.Name = "Arial"
                trdiscount.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                trdiscount.Cells(1).Range.Font.Bold = True

                trdiscount = objDoc1.Tables(1).Rows.Add(Type.Missing)
                Dim finaled As Decimal

                finaled = Convert.ToDecimal(txtspdiscount.Text.Trim())
                trdiscount.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbGujarati.Checked Then
                    trdiscount.Cells(1).Range.Text = "અંતીમ મિનરલ વોટર પ્રોજેક્ટ ની કિમત"
                ElseIf RdbHindi.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbMarathi.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTamil.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTelugu.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                End If
                sumall = sys1total + sys2total + sys3total + sys4total
                sumall2 = sys1mutotal + sys2mutotal + sys3mutotal + sys4mutotal
                sumall = sumall - finaled
                sumall2 = sumall2 - finaled
                trdiscount.Cells(4).Range.Font.Name = "Rupee"
                trdiscount.Cells(5).Range.Font.Name = "Rupee"
                trdiscount.Cells(4).Range.Text = "` " + Convert.ToString(sumall)
                trdiscount.Cells(5).Range.Text = "` " + Convert.ToString(sumall2)
                trdiscount.Cells(1).Width = 350
                trdiscount.Cells(2).Width = 15
                trdiscount.Cells(3).Width = 15
                trdiscount.Cells(4).Width = 50
                trdiscount.Cells(5).Width = 50

                trdiscount.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                trdiscount.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
                trdiscount.Cells(5).Range.Font.Color = Word.WdColor.wdColorWhite


                trdiscount.Cells(1).Range.Font.Name = "Arial"
                trdiscount.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                trdiscount.Cells(1).Range.Font.Bold = True
            End If


        End If




        objDoc1.Tables(1).Columns.Select()
        With objApp1.Selection.Borders(Word.WdBorderType.wdBorderTop)
            .LineStyle = objApp1.Options.DefaultBorderLineStyle
            .LineWidth = objApp1.Options.DefaultBorderLineWidth
            .Color = objApp1.Options.DefaultBorderColor
        End With
        objDoc1.Tables(1).Columns.Select()
        With objApp1.Selection.Borders(Word.WdBorderType.wdBorderRight)
            .LineStyle = objApp1.Options.DefaultBorderLineStyle
            .LineWidth = objApp1.Options.DefaultBorderLineWidth
            .Color = objApp1.Options.DefaultBorderColor

        End With
        objDoc1.Tables(1).Columns.Select()
        With objApp1.Selection.Borders(Word.WdBorderType.wdBorderLeft)
            .LineStyle = objApp1.Options.DefaultBorderLineStyle
            .LineWidth = objApp1.Options.DefaultBorderLineWidth
            .Color = objApp1.Options.DefaultBorderColor
        End With
        objDoc1.Tables(1).Columns.Select()
        With objApp1.Selection.Borders(Word.WdBorderType.wdBorderBottom)
            .LineStyle = objApp1.Options.DefaultBorderLineStyle
            .LineWidth = objApp1.Options.DefaultBorderLineWidth
            .Color = objApp1.Options.DefaultBorderColor
        End With



        If FlagPdf = 1 Then
            objDoc1.SaveAs(appPath + "\OrderData" + "\" + Class1.global.GobalMaxId.ToString() + ".doc")
            objDoc1.SaveAs(appPath + "\OrderData" + "\" + Class1.global.GobalMaxId.ToString() + ".pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)
        End If


        objDoc1.Close()
        objDoc1 = Nothing
        objApp1.Quit()
        objApp1 = Nothing


    End Sub

    Public Sub PriceSheet()
        Dim str1(0) As String
        ReDim str1(0)
        str1(0) = QtempPath + "\" + Convert.ToString(0) + ".doc"
        Dim i As Integer
        '  i = _pdfforge.Images2PDF(str1, appPath + "\step3.pdf", 0)
        Dim FinalSysAllTotal As Decimal
        FinalSysAllTotal = 0

        Dim dt33 As DataTable
        dt33 = New DataTable

        Dim dtSys2 As DataTable
        dtSys2 = New DataTable

        Dim dtSys3 As DataTable
        dtSys3 = New DataTable

        Dim dtSys4 As DataTable
        dtSys4 = New DataTable

        Dim price As Decimal
        Dim price1 As Decimal
        Dim qty1 As Decimal


        price = 0
        price1 = 0
        qty1 = 0
        If RblSingle.Checked = True Then
            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price")
            For t1 As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS1.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnicalSYS1.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(4).Value.ToString())
                End If
            Next

            dtSys2.Columns.Add("No")
            dtSys2.Columns.Add("Description")
            dtSys2.Columns.Add("Price")

            For t1 As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(t1).Cells(0).Value)
                If IsTicked Then
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                Else
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    'dtSys2.Rows.Add(GvTechnicalSYS2.Rows(t1).Cells(2).Value.ToString(), "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                End If
            Next
            dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price)
            dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price)
            dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price)

            dtSys3.Columns.Add("No")
            dtSys3.Columns.Add("Description")
            dtSys3.Columns.Add("Price")
            dtSys3.Columns.Add("Capacity")

            For t1 As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys3.Rows.Add(GvTechnicalSYS3.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(6).Value)
                End If
            Next

            dtSys4.Columns.Add("No")
            dtSys4.Columns.Add("Description")
            dtSys4.Columns.Add("Price")

            For t1 As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys4.Rows.Add(GvTechnicalSYS4.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(4).Value.ToString())
                End If
            Next
        End If


        If RblOther.Checked = True Then

            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price")
            dt33.Columns.Add("Qty")
            dt33.Columns.Add("Total")

            For t1 As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS1.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnicalSYS1.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(6).Value.ToString())

                End If
            Next


            dtSys2.Columns.Add("No")
            dtSys2.Columns.Add("Description")
            dtSys2.Columns.Add("Price")
            dtSys2.Columns.Add("Qty")
            dtSys2.Columns.Add("Total")
            price = 0
            price1 = 0
            qty1 = 0

            For t1 As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(t1).Cells(0).Value)
                If IsTicked Then
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    qty1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                    price1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(6).Value.ToString())
                Else
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    qty1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                    price1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(6).Value.ToString())

                    ' dtSys2.Rows.Add(GvTechnicalSYS2.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(6).Value.ToString())
                End If
            Next
            dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price.ToString(), qty1.ToString(), price1.ToString())
            dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price.ToString(), qty1.ToString(), price1.ToString())
            dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price.ToString(), qty1.ToString(), price1.ToString())


            'dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price)
            'dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price)
            'dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price)

            dtSys3.Columns.Add("No")
            dtSys3.Columns.Add("Description")
            dtSys3.Columns.Add("Price")
            dtSys3.Columns.Add("Qty")
            dtSys3.Columns.Add("Total")
            dtSys3.Columns.Add("Capacity")

            For t1 As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys3.Rows.Add(GvTechnicalSYS3.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(6).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells("Capacity").Value)
                End If
            Next

            dtSys4.Columns.Add("No")
            dtSys4.Columns.Add("Description")
            dtSys4.Columns.Add("Price")
            dtSys4.Columns.Add("Qty")
            dtSys4.Columns.Add("Total")

            For t1 As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys4.Rows.Add(GvTechnicalSYS4.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(6).Value.ToString())
                End If
            Next

        End If



        If RblMultiple.Checked = True Then

            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price1")
            dt33.Columns.Add("Price2")
            For t1 As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS1.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dt33.Rows.Add(GvTechnicalSYS1.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(5).Value.ToString())
                End If
            Next
            price = 0
            price1 = 0

            dtSys2.Columns.Add("No")
            dtSys2.Columns.Add("Description")
            dtSys2.Columns.Add("Price1")
            dtSys2.Columns.Add("Price2")
            For t1 As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(t1).Cells(0).Value)
                If IsTicked Then
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    price1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                Else
                    price = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString())
                    price1 = Convert.ToDecimal(GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                    'dtSys2.Rows.Add(GvTechnicalSYS2.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS2.Rows(t1).Cells(5).Value.ToString())
                End If
            Next
            dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price.ToString(), price1.ToString())
            dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price.ToString(), price1.ToString())
            dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price.ToString(), price1.ToString())

            'dtSys2.Rows.Add("2.1", "Micro & Chem Lab As Per Bureau Of Indian Standard Norms", price.ToString(), qty1.ToString(), price1.ToString())
            'dtSys2.Rows.Add("2.1", "- Lab Equipment,Chemicals,Glassware, Media, General Item", price.ToString(), qty1.ToString(), price1.ToString())
            'dtSys2.Rows.Add("2.1", "(BIS LICENSE FEES EXCLUSIVE EXTRA AT ACTUAL)", price.ToString(), qty1.ToString(), price1.ToString())



            dtSys3.Columns.Add("No")
            dtSys3.Columns.Add("Description")
            dtSys3.Columns.Add("Price1")
            dtSys3.Columns.Add("Price2")
            dtSys3.Columns.Add("Capacity")

            For t1 As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys3.Rows.Add(GvTechnicalSYS3.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS3.Rows(t1).Cells("Capacity").Value)
                End If
            Next

            dtSys4.Columns.Add("No")
            dtSys4.Columns.Add("Description")
            dtSys4.Columns.Add("Price1")
            dtSys4.Columns.Add("Price2")
            For t1 As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1
                Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(t1).Cells(0).Value)
                If IsTicked Then
                Else
                    dtSys4.Rows.Add(GvTechnicalSYS4.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS4.Rows(t1).Cells(5).Value.ToString())
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


        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc1.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
        Else

            For Each section As Word.Section In objDoc1.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
        End If


        Dim oTable2 As Word.Tables = objDoc1.Tables
        Dim rng2 As Word.Range = objDoc1.Range(start2, missing1)
        If RblSingle.Checked = True Then

            oTable2.Add(rng2, 1, 2, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)

            oTable2.Add(rng2, 1, 4, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)


        End If
        If RblOther.Checked = True Then
            oTable2.Add(rng2, 1, 2, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)


            oTable2.Add(rng2, 1, 6, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)

        End If
        If RblMultiple.Checked = True Then

            oTable2.Add(rng2, 1, 2, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)


            oTable2.Add(rng2, 1, 5, missing1, missing1)
            start2 = objDoc1.Tables(1).Range.[End]
            rng2 = objDoc1.Range(start2, missing1)

        End If
        Dim defaultTableBehavior1 As [Object] = Type.Missing
        Dim autoFitBehavior1 As [Object] = Type.Missing



        'objDoc1.Selection.TypeText("Refzxczxxczccxxxxxxxxxxxxcxcxcccxcxcxcxcxccxxcxccxccxcxcc")
        ' Dim rng1 As Word.Range = objDoc1.Range(0, 0)
        rng2.Font.Name = "Arial"
        ' oTable2.Range.ParagraphFormat.SpaceAfter = 3
        Dim tbl2 As Word.Table = objDoc1.Tables(1)

        objDoc1.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4

        rng2.Font.Name = "Arial"
        tbl2.Range.ParagraphFormat.SpaceAfter = 1.5

        If RdbEnglish.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"

        ElseIf RdbGujarati.Checked Then
            tbl2.Cell(1, 1).Range.Text = "કિંમત - મિનરલ વોટર પ્રોજેક્ટ"
            tbl2.Cell(1, 2).Range.Text = "(કિંમત લાખમાં)"
        ElseIf RdbHindi.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        ElseIf RdbMarathi.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        ElseIf RdbTamil.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        ElseIf RdbTelugu.Checked Then
            tbl2.Cell(1, 1).Range.Text = "PRICE - MINERAL WATER PROJECT - ISI"
            tbl2.Cell(1, 2).Range.Text = "(PRICE IN LACS)"
        End If


        If RblSingle.Checked = True Then

            tbl2.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(1, 1).Width = 300
            tbl2.Cell(1, 1).Range.Borders.Enable = 0

            tbl2.Cell(1, 2).Range.Borders.Enable = 0
            tbl2.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            tbl2.Cell(1, 2).Width = 180
        End If
        If RblOther.Checked = True Then
            tbl2.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(1, 1).Width = 300
            tbl2.Cell(1, 1).Range.Borders.Enable = 0

            tbl2.Cell(1, 2).Range.Borders.Enable = 0
            tbl2.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            tbl2.Cell(1, 2).Width = 180
        End If
        If RblMultiple.Checked = True Then
            tbl2.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(1, 1).Width = 300
            tbl2.Cell(1, 1).Range.Borders.Enable = 0

            tbl2.Cell(1, 2).Range.Borders.Enable = 0
            tbl2.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            tbl2.Cell(1, 2).Width = 180
        End If
        tbl2.Cell(1, 1).Range.Font.Color = Word.WdColor.wdColorWhite
        tbl2.Cell(1, 2).Range.Font.Color = Word.WdColor.wdColorWhite

        tbl2.Rows.Item(1).Shading.BackgroundPatternColor = RGB(12, 28, 71)

        tbl2.Rows.Item(1).Range.Font.Name = "Arial"
        tbl2.Rows.Item(1).Range.Font.Bold = True 'Indian' 
        tbl2.Rows.Item(1).Range.Font.Size = 14

        tbl2.Cell(2, 1).Range.Borders.Enable = 1
        tbl2.Cell(2, 1).Range.Text = "PRICE : MINERAL WATER PLANT COMPLETE WITH DESIGN, ENGINEERING & SUPPLY"
        tbl2.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        tbl2.Cell(2, 1).Width = 480
        tbl2.Cell(2, 1).Range.Borders.Enable = 1

        tbl2.Rows.Item(2).Range.Font.Name = "Arial"
        tbl2.Rows.Item(2).Range.Font.Bold = True 'Indian' 
        tbl2.Rows.Item(2).Range.Font.Size = 10



        tbl2.Rows.Item(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        '  newRow2.HeadingFormat = 2
        Dim newRow3 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        If RblSingle.Checked = True Then
            tbl2.Rows.Item(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            '            oTable2.Rows.Item(3).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray70
            If RdbEnglish.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbGujarati.Checked Then
                tbl2.Cell(3, 1).Range.Text = "સિસ્ટમ"
                tbl2.Cell(3, 2).Range.Text = "નંબર"
                tbl2.Cell(3, 3).Range.Text = "માહિતી"
            ElseIf RdbHindi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbMarathi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbTamil.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbTelugu.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            End If
            ' tbl2.Cell(3, 1).Range.Text = "SYSTEM"
            'tbl2.Cell(3, 2).Range.Text = "SR.NO."
            'tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            tbl2.Cell(3, 4).Range.Text = "PRICE"
            tbl2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            tbl2.Rows.Item(3).Range.Font.Size = 9
            tbl2.Cell(3, 3).Width = 275
            tbl2.Cell(3, 1).Width = 80
            tbl2.Cell(3, 2).Width = 45
            tbl2.Cell(3, 4).Width = 80
            tbl2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
            tbl2.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            Dim newRowStatic1st As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1st.Range.Font.Name = "Arial"
            newRowStatic1st.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1st.Range.Font.Size = 9
            newRowStatic1st.Range.Font.Bold = True
            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0


            If RdbEnglish.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "વોટર પ્યુરિફિકેશન સિસ્ટમ"
            ElseIf RdbHindi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTamil.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            End If

            'newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            newRowStatic1st.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1st.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1st.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1st.Cells(3).Width = 275
            newRowStatic1st.Cells(1).Width = 80
            newRowStatic1st.Cells(2).Width = 45
            newRowStatic1st.Cells(4).Width = 80

            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0



        End If

        ''6:29 i am here
        ''5":05 i am here 

        If RblOther.Checked = True Then
            tbl2.Rows.Item(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)

            If RdbEnglish.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            ElseIf RdbGujarati.Checked Then
                tbl2.Cell(3, 1).Range.Text = "સિસ્ટમ"
                tbl2.Cell(3, 2).Range.Text = "નંબર"
                tbl2.Cell(3, 3).Range.Text = "માહિતી"
                tbl2.Cell(3, 4).Range.Text = "જથ્થો"
                tbl2.Cell(3, 5).Range.Text = "કિંમત"
                tbl2.Cell(3, 6).Range.Text = "ટોટલ"
            ElseIf RdbHindi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            ElseIf RdbMarathi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            ElseIf RdbTamil.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            ElseIf RdbTelugu.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "Description"
                tbl2.Cell(3, 4).Range.Text = "Qty"
                tbl2.Cell(3, 5).Range.Text = "PRICE"
                tbl2.Cell(3, 6).Range.Text = "Total"
            End If


            tbl2.Cell(3, 1).Width = 65
            tbl2.Cell(3, 2).Width = 45
            tbl2.Cell(3, 3).Width = 190
            tbl2.Cell(3, 4).Width = 45
            tbl2.Cell(3, 5).Width = 80
            tbl2.Cell(3, 6).Width = 55

            tbl2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            tbl2.Rows.Item(3).Range.Font.Size = 9
            tbl2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite
            tbl2.Cell(3, 6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            Dim newRowStatic1st As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1st.Range.Font.Name = "Arial"
            newRowStatic1st.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1st.Range.Font.Size = 9
            newRowStatic1st.Range.Font.Bold = True

            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0
            newRowStatic1st.Cells(5).Borders.Enable = 0
            newRowStatic1st.Cells(6).Borders.Enable = 0


            If RdbEnglish.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "વોટર પ્યુરિફિકેશન સિસ્ટમ"
            ElseIf RdbHindi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTamil.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            End If
            newRowStatic1st.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1st.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1st.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            newRowStatic1st.Cells(1).Width = 80
            newRowStatic1st.Cells(2).Width = 30
            newRowStatic1st.Cells(3).Width = 190
            newRowStatic1st.Cells(4).Width = 45
            newRowStatic1st.Cells(5).Width = 80
            newRowStatic1st.Cells(6).Width = 55

            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0
            newRowStatic1st.Cells(5).Borders.Enable = 0
            newRowStatic1st.Cells(6).Borders.Enable = 0
        End If

        If RblMultiple.Checked = True Then
            tbl2.Rows.Item(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            If RdbEnglish.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbGujarati.Checked Then
                tbl2.Cell(3, 1).Range.Text = "સિસ્ટમ"
                tbl2.Cell(3, 2).Range.Text = "નંબર"
                tbl2.Cell(3, 3).Range.Text = "માહિતી"
            ElseIf RdbHindi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbMarathi.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbTamil.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            ElseIf RdbTelugu.Checked Then
                tbl2.Cell(3, 1).Range.Text = "SYSTEM"
                tbl2.Cell(3, 2).Range.Text = "SR.NO."
                tbl2.Cell(3, 3).Range.Text = "DESCRIPTION"
            End If
            tbl2.Cell(3, 4).Range.Text = txtCapacity1.Text
            tbl2.Cell(3, 5).Range.Text = txtCapacity2.Text


            tbl2.Cell(3, 1).Width = 70
            tbl2.Cell(3, 2).Width = 45
            tbl2.Cell(3, 3).Width = 265
            tbl2.Cell(3, 4).Width = 50
            tbl2.Cell(3, 5).Width = 50


            tbl2.Rows.Item(3).Range.Font.Bold = True 'Indian' 
            tbl2.Rows.Item(3).Range.Font.Size = 9
            tbl2.Rows.Item(3).Range.Font.Color = Word.WdColor.wdColorWhite

            tbl2.Cell(3, 5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            tbl2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            Dim newRowStatic1st As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1st.Range.Font.Name = "Arial"
            newRowStatic1st.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1st.Range.Font.Size = 9
            newRowStatic1st.Range.Font.Bold = True


            If RdbEnglish.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "વોટર પ્યુરિફિકેશન સિસ્ટમ"
            ElseIf RdbHindi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTamil.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1st.Cells(3).Range.Text = "WATER PURIFICATION SYSTEM"
            End If
            newRowStatic1st.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1st.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1st.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


            newRowStatic1st.Cells(1).Width = 70
            newRowStatic1st.Cells(2).Width = 45
            newRowStatic1st.Cells(3).Width = 265
            newRowStatic1st.Cells(4).Width = 50
            newRowStatic1st.Cells(5).Width = 50

            newRowStatic1st.Cells(1).Borders.Enable = 0
            newRowStatic1st.Cells(2).Borders.Enable = 0
            newRowStatic1st.Cells(3).Borders.Enable = 0
            newRowStatic1st.Cells(4).Borders.Enable = 0
            newRowStatic1st.Cells(5).Borders.Enable = 0

        End If
        Dim finaltotal As New Decimal
        Dim sumall As New Decimal

        Dim sumall2 As New Decimal
        Dim finaltotal1 As New Decimal
        Dim qty As Integer
        qty = 0
        sumall = 0
        sumall2 = 0

        finaltotal = 0
        Dim flgsys As New Integer
        flgsys = 0
        Dim limit As New Integer

        If RdbGujarati.Checked Then
            limit = 4
            objDoc1.Tables(1).Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle
            objDoc1.Tables(1).Range.ParagraphFormat.SpaceAfter = 0
            'objDoc1.Tables(1).Range.ParagraphFormat.LineSpacing = 1
            objDoc1.Tables(1).Rows.Application.Selection.Range.Font.Size = 9
        ElseIf RdbEnglish.Checked Then
            limit = 4
        End If

        For i = 0 To limit
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            ' newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Bold = 0
            'newRow4.Range.Font.Bold = True
            newRow4.Range.Font.Color = Word.WdColor.wdColorBlack
            'newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow4.Range.Borders.Enable = 0
            If i = 2 Then
                If RdbEnglish.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                ElseIf RdbGujarati.Checked Then
                    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૧"
                    'newRow4.Range.ParagraphFormat.SpaceAfter = 0

                ElseIf RdbHindi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                ElseIf RdbMarathi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                ElseIf RdbTamil.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                ElseIf RdbTelugu.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-1"
                End If
            End If
            If dt33.Rows.Count > i Then
                If RblSingle.Checked = True Then
                    If flgsys = 0 Then
                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    newRow4.Cells(2).Range.Text = "1." + t.ToString()

                    newRow4.Cells(3).Range.Text = dt33.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = "`   " + dt33.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    'newRow4.Range.Font.Bold = True 'Indian' 
                    newRow4.Range.Font.Size = 9

                    finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dt33.Rows(i)(2).ToString())
                    newRow4.Cells(3).Width = 275
                    newRow4.Cells(1).Width = 80
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(4).Width = 80

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                End If

                If RblOther.Checked = True Then

                    If flgsys = 0 Then
                        flgsys = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        newRow4.Cells(6).Borders.Enable = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "1." + t.ToString()
                    newRow4.Cells(3).Range.Text = dt33.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = dt33.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Text = "`    " + dt33.Rows(i)(2).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Text = "`    " + dt33.Rows(i)(4).ToString()
                    'tbl2.Rows.Item(i).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    'tbl2.Rows.Item(i).Range.Font.Bold = True 'Indian' 
                    'tbl2.Rows.Item(i).Range.Font.Size = 10
                    'tbl2.Rows.Item(i).Range.Font.Color = Word.WdColor.wdColorDarkBlue

                    newRow4.Cells(1).Width = 65
                    newRow4.Cells(2).Width = 30
                    newRow4.Cells(3).Width = 215
                    newRow4.Cells(4).Width = 45
                    newRow4.Cells(5).Width = 70
                    newRow4.Cells(6).Width = 55


                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i)(3).ToString())
                    sumall += Convert.ToDecimal(dt33.Rows(i)(3).ToString())
                    qty = qty + Convert.ToDecimal(dt33.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dt33.Rows(i)(4).ToString())
                    sumall2 += Convert.ToDecimal(dt33.Rows(i)(4).ToString())


                End If
                If RblMultiple.Checked = True Then

                    If flgsys = 0 Then

                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    newRow4.Cells(2).Range.Text = "1." + t.ToString()
                    newRow4.Cells(3).Range.Text = dt33.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = "`    " + dt33.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    newRow4.Cells(5).Range.Text = "`    " + dt33.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"


                    newRow4.Cells(1).Width = 70
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(3).Width = 265
                    newRow4.Cells(4).Width = 50
                    newRow4.Cells(5).Width = 50


                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


                    finaltotal = finaltotal + Convert.ToDecimal(dt33.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dt33.Rows(i)(2).ToString())

                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dt33.Rows(i)(3).ToString())
                    sumall2 += Convert.ToDecimal(dt33.Rows(i)(3).ToString())

                End If
            End If

        Next
        'If RdbEnglish.Checked Then

        '    If dt33.Rows.Count < 5 Then
        '        Dim IK As Integer
        '        IK = 0
        '        IK = 6 - dt33.Rows.Count
        '        For index = 1 To IK
        '            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        '            newRow4.Range.Font.Name = "Arial"
        '            newRow4.Range.Font.Size = 9
        '            newRow4.Cells(1).Range.Text = ""
        '        Next

        '    End If
        'End If

        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9


            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વોટર ટ્રીટમેન્ટ પ્લાન્ટની ટોટલ કિંમત (સિસ્ટમ-૧)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys1total = finaltotal

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            'newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Width = 330
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 80
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


        End If
        If RblOther.Checked = True Then

            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9


            newRow4.Cells(1).Range.Text = ""
            newRow4.Cells(2).Range.Text = ""


            newRow4.Cells(1).Range.Text = "`    " + Convert.ToString(finaltotal)
            newRow4.Cells(1).Range.Font.Name = "Rupee"
            newRow4.Cells(1).Range.Text = ""
            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વોટર ટ્રીટમેન્ટ પ્લાન્ટની ટોટલ કિંમત(સિસ્ટમ-૧)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            newRow4.Cells(6).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys1total = finaltotal1
            newRow4.Cells(6).Range.Font.Name = "Rupee"

            newRow4.Cells(1).Width = 80
            newRow4.Cells(2).Width = 300
            newRow4.Cells(3).Width = 15
            newRow4.Cells(4).Width = 15
            newRow4.Cells(5).Width = 15
            newRow4.Cells(6).Width = 55

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(5).Borders.Enable = 0
            'newRow4.Cells(6).Borders.Enable = 0
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

        End If

        If RblMultiple.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            ''newRow4.Range.Font.Color = Word.WdColor.wdColorDarkBlue
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 8


            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(4).Shading.BackgroundPatternColor = RGB(0, 0, 128)

            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વોટર ટ્રીટમેન્ટ પ્લાન્ટની ટોટલ કિંમત(સિસ્ટમ-૧)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR WATER TREATMENT SYSTEM(SYS-1)"
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys1total = finaltotal
            newRow4.Cells(4).Range.Font.Name = "Rupee"
            newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys1mutotal = finaltotal1


            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            ' newRow4.Cells(4).Borders.Enable = 0
            ' newRow4.Cells(5).Borders.Enable = 0

            'newRow4.Cells(5).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone



            newRow4.Cells(1).Width = 70
            newRow4.Cells(2).Width = 310
            newRow4.Cells(3).Width = 50
            newRow4.Cells(4).Width = 50
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

        End If

        ''''''''''''''''''Static Text

        If RblSingle.Checked = True Then

            Dim newRowStatic As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic.Cells(3).Width = 245
            newRowStatic.Cells(1).Width = 80
            newRowStatic.Cells(2).Width = 45
            newRowStatic.Cells(4).Width = 110

            newRowStatic.Range.Font.Name = "Arial"
            newRowStatic.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic.Range.Font.Size = 9
            newRowStatic.Range.Font.Bold = True
            newRowStatic.Range.Borders.Enable = 0

            newRowStatic.Cells(1).Borders.Enable = 0
            newRowStatic.Cells(2).Borders.Enable = 0
            newRowStatic.Cells(4).Borders.Enable = 0
            newRowStatic.Cells(3).Borders.Enable = 0
            newRowStatic.Cells(1).Width = 80
            newRowStatic.Cells(2).Width = 45
            newRowStatic.Cells(4).Width = 110

            If RdbEnglish.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbGujarati.Checked Then
                newRowStatic.Cells(3).Range.Text = "ક્વાલિટી કંટ્રોલ લેબોરેટરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbMarathi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTamil.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTelugu.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            End If

            'newRowStatic.Cells(3).Width = 480
            newRowStatic.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '  newRowStatic.Range.Borders.Enable = 0

        ElseIf RblOther.Checked = True Then

            Dim newRowStatic As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic.Cells(1).Width = 80
            newRowStatic.Cells(2).Width = 30
            newRowStatic.Cells(3).Width = 190
            newRowStatic.Cells(4).Width = 45
            newRowStatic.Cells(5).Width = 80
            newRowStatic.Cells(6).Width = 55

            'newRowStatic.Cells(1).Width = 65
            'newRowStatic.Cells(2).Width = 30
            'newRowStatic.Cells(3).Width = 215
            'newRowStatic.Cells(4).Width = 45
            'newRowStatic.Cells(5).Width = 70
            'newRowStatic.Cells(6).Width = 55


            newRowStatic.Range.Font.Name = "Arial"
            newRowStatic.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic.Range.Font.Size = 9
            newRowStatic.Range.Font.Bold = True
            newRowStatic.Cells(1).Borders.Enable = 0
            newRowStatic.Cells(2).Borders.Enable = 0
            newRowStatic.Cells(4).Borders.Enable = 0
            newRowStatic.Cells(3).Borders.Enable = 0
            newRowStatic.Cells(5).Borders.Enable = 0
            newRowStatic.Cells(6).Borders.Enable = 0

            '  newRowStatic.Range.Borders.Enable = 0
            If RdbEnglish.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbGujarati.Checked Then
                newRowStatic.Cells(3).Range.Text = "ક્વાલિટી કંટ્રોલ લેબોરેટરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbMarathi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTamil.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTelugu.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            End If
            newRowStatic.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '   newRowStatic.Range.Borders.Enable = 0

        ElseIf RblMultiple.Checked = True Then

            Dim newRowStatic As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic.Cells(1).Width = 70
            newRowStatic.Cells(2).Width = 45
            newRowStatic.Cells(3).Width = 265
            newRowStatic.Cells(4).Width = 50
            newRowStatic.Cells(5).Width = 50



            newRowStatic.Range.Font.Name = "Arial"
            newRowStatic.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic.Range.Font.Size = 9
            newRowStatic.Range.Font.Bold = True
            '  newRowStatic.Range.Borders.Enable = 0
            If RdbEnglish.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbGujarati.Checked Then
                newRowStatic.Cells(3).Range.Text = "ક્વાલિટી કંટ્રોલ લેબોરેટરીની ટોટલ કિંમત"
            ElseIf RdbHindi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbMarathi.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTamil.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            ElseIf RdbTelugu.Checked Then
                newRowStatic.Cells(3).Range.Text = "QUALITY CONTROL LAB"
            End If
            ' newRowStatic.Cells(1).Width = 480
            newRowStatic.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '   newRowStatic.Range.Borders.Enable = 0
            newRowStatic.Cells(1).Borders.Enable = 0
            newRowStatic.Cells(2).Borders.Enable = 0
            newRowStatic.Cells(4).Borders.Enable = 0
            newRowStatic.Cells(3).Borders.Enable = 0
            newRowStatic.Cells(5).Borders.Enable = 0

        End If

        finaltotal = 0
        finaltotal1 = 0

        If RdbGujarati.Checked Then
            limit = 3
        ElseIf RdbEnglish.Checked Then
            limit = 3
        End If

        flgsys = 0
        For i = 0 To limit
            'For i = 0 To dtSys2.Rows.Count - 1
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Borders.Enable = 0
            newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow4.Range.Font.Bold = 0
            '  newRow4.Range.Font.Bold = True 'Indian' 
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Color = Word.WdColor.wdColorBlack
            newRow4.Range.Font.Name = "Arial"
            If i = 2 Then
                'If dtSys2.Rows.Count < 4 Then
                '    Dim IK As Integer
                If RdbEnglish.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                ElseIf RdbGujarati.Checked Then
                    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૨"
                ElseIf RdbHindi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                ElseIf RdbMarathi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                ElseIf RdbTamil.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                ElseIf RdbTelugu.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-2"
                End If
                'newRow4.Cells(1).Range.Text = "SYS-2"
                'newRow4.Range.Borders.Enable = 1
            End If

            If dtSys2.Rows.Count > i Then

                If RblSingle.Checked = True Then

                    If flgsys = 0 Then
                        '   newRow4.Cells(1).Range.Text = "SYS-2"
                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        flgsys = 1
                    End If
                    If i = 2 Then
                        newRow4.Cells(2).Range.Font.Name = "Arial"
                        newRow4.Cells(4).Range.Font.Name = "Rupee"

                        'If dtSys2.Rows.Count < 4 Then
                        '    Dim IK As Integer
                        If RdbEnglish.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbGujarati.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbHindi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbMarathi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbTamil.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        ElseIf RdbTelugu.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)("Price").ToString()

                        End If
                        'newRow4.Cells(1).Range.Text = "SYS-2"
                        'newRow4.Range.Borders.Enable = 1
                    Else
                        newRow4.Cells(2).Range.Text = ""
                        newRow4.Cells(4).Range.Text = ""
                    End If

                    ' newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                    newRow4.Cells(3).Range.Text = dtSys2.Rows(i)(1).ToString()
                    'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                    'newRow4.Cells(4).Range.Font.Name = "Rupee"

                    newRow4.Range.Font.Size = 9

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys2.Rows(i)("Price").ToString())
                    sumall += Convert.ToDecimal(dtSys2.Rows(i)("Price").ToString())

                    newRow4.Cells(3).Width = 275
                    newRow4.Cells(1).Width = 80
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(4).Width = 80

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.Font.Bold = True

                End If

                If RblOther.Checked = True Then
                    If flgsys = 0 Then
                        ' newRow4.Cells(1).Range.Text = "SYS-2"
                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        newRow4.Cells(6).Borders.Enable = 1

                        flgsys = 1
                    End If

                    If i = 2 Then
                        newRow4.Cells(2).Range.Font.Name = "Arial"
                        newRow4.Cells(4).Range.Font.Name = "Arial"
                        newRow4.Cells(5).Range.Font.Name = "Rupee"
                        newRow4.Cells(6).Range.Font.Name = "Rupee"
                        'If dtSys2.Rows.Count < 4 Then
                        '    Dim IK As Integer
                        If RdbEnglish.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                        ElseIf RdbGujarati.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                        ElseIf RdbHindi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()

                        ElseIf RdbMarathi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                        ElseIf RdbTamil.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                        ElseIf RdbTelugu.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                        End If
                        'newRow4.Cells(1).Range.Text = "SYS-2"
                        'newRow4.Range.Borders.Enable = 1
                    Else
                        newRow4.Cells(2).Range.Text = ""
                        newRow4.Cells(4).Range.Text = ""
                        newRow4.Cells(5).Range.Text = ""
                        newRow4.Cells(6).Range.Text = ""

                    End If


                    '  newRow4.Cells(2).Range.Text = "2." + dtSys2.Rows(i)(0).ToString()
                    newRow4.Cells(3).Range.Text = dtSys2.Rows(i)(1).ToString()
                    'newRow4.Cells(4).Range.Text = dtSys2.Rows(i)(3).ToString()
                    'newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()

                    'newRow4.Cells(6).Range.Text = "`    " + dtSys2.Rows(i)(4).ToString()

                    newRow4.Range.Font.Size = 9

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(5).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorBlack

                    'newRow4.Cells(1).Width = 80
                    'newRow4.Cells(2).Width = 30
                    'newRow4.Cells(3).Width = 190
                    'newRow4.Cells(4).Width = 45
                    'newRow4.Cells(5).Width = 80
                    'newRow4.Cells(6).Width = 55


                    newRow4.Cells(1).Width = 65
                    newRow4.Cells(2).Width = 30
                    newRow4.Cells(3).Width = 215
                    newRow4.Cells(4).Width = 45
                    newRow4.Cells(5).Width = 70
                    newRow4.Cells(6).Width = 55


                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.Font.Bold = True

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys2.Rows(i)(3).ToString())
                    sumall += Convert.ToDecimal(dtSys2.Rows(i)(3).ToString())

                    qty = qty + Convert.ToDecimal(dtSys2.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys2.Rows(i)(4).ToString())
                    sumall2 += Convert.ToDecimal(dtSys2.Rows(i)(4).ToString())

                End If
                If RblMultiple.Checked = True Then
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    If flgsys = 0 Then
                        newRow4.Range.Borders.Enable = 1
                        '  newRow4.Cells(1).Range.Text = "SYS-2"
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1

                        flgsys = 1
                    End If

                    If i = 2 Then
                        newRow4.Cells(2).Range.Font.Name = "Arial"
                        newRow4.Cells(4).Range.Font.Name = "Rupee"

                        'If dtSys2.Rows.Count < 4 Then
                        '    Dim IK As Integer
                        If RdbEnglish.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            ' newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbGujarati.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbHindi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbMarathi.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbTamil.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            'newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        ElseIf RdbTelugu.Checked Then
                            newRow4.Cells(2).Range.Text = dtSys2.Rows(i)(0).ToString()
                            '     newRow4.Cells(4).Range.Text = "`   " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Text = "`    " + dtSys2.Rows(i)(2).ToString()
                            newRow4.Cells(4).Range.Font.Name = "Rupee"
                            newRow4.Cells(5).Range.Text = "`    " + dtSys2.Rows(i)(3).ToString()
                            newRow4.Cells(5).Range.Font.Name = "Rupee"
                        End If
                        'newRow4.Cells(1).Range.Text = "SYS-2"
                        'newRow4.Range.Borders.Enable = 1
                    Else
                        newRow4.Cells(2).Range.Text = ""
                        newRow4.Cells(4).Range.Text = ""
                        newRow4.Cells(5).Range.Text = ""

                    End If

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack


                    '   newRow4.Cells(2).Range.Text = "2." + dtSys2.Rows(i)(0).ToString()
                    newRow4.Cells(3).Range.Text = dtSys2.Rows(i)(1).ToString()

                    newRow4.Range.Font.Size = 9

                    newRow4.Cells(1).Width = 70
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(3).Width = 265
                    newRow4.Cells(4).Width = 50
                    newRow4.Cells(5).Width = 50


                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.Font.Bold = True


                    finaltotal = finaltotal + Convert.ToDecimal(dtSys2.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys2.Rows(i)(2).ToString())

                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys2.Rows(i)(3).ToString())
                    sumall2 += Convert.ToDecimal(dtSys2.Rows(i)(3).ToString())

                End If
            End If

        Next


        'If RdbEnglish.Checked Then

        '    If dtSys2.Rows.Count < 4 Then
        '        Dim IK As Integer
        '        IK = 0
        '        IK = 6 - dtSys2.Rows.Count
        '        For index = 1 To IK
        '            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        '            newRow4.Range.Font.Name = "Arial"
        '            newRow4.Range.Font.Size = 9
        '            newRow4.Cells(1).Range.Text = ""
        '        Next

        '    End If
        'End If

        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            For il = 0 To dtSys2.Rows.Count - 1
                finaltotal = dtSys2.Rows(il)(2).ToString()
            Next

            '            newRow4.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Range.Text = ""


            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "ક્વોલિટી કન્ટ્રોલ લેબોરેટરીની ટોટલ કિમત (સિસ્ટમ-૨)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            End If
            ' newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys2total = finaltotal


            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            'newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Width = 330
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 80
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            'newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


        End If
        If RblOther.Checked = True Then
            For il = 0 To dtSys2.Rows.Count - 1
                finaltotal = dtSys2.Rows(il)(2).ToString()
                finaltotal1 = dtSys2.Rows(il)(3).ToString()
            Next

            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Bold = True

            newRow4.Cells(1).Range.Text = ""
            newRow4.Cells(2).Range.Text = ""





            newRow4.Cells(1).Range.Text = "`    " + Convert.ToString(finaltotal)
            newRow4.Cells(1).Range.Font.Name = "Rupee"
            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "ક્વોલિટી કન્ટ્રોલ લેબોરેટરીની ટોટલ કિમત (સિસ્ટમ-૨)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            End If
            newRow4.Cells(6).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys2total = finaltotal1
            newRow4.Cells(6).Range.Font.Name = "Rupee"

            newRow4.Cells(1).Width = 80
            newRow4.Cells(2).Width = 300
            newRow4.Cells(3).Width = 15
            newRow4.Cells(4).Width = 15
            newRow4.Cells(5).Width = 15
            newRow4.Cells(6).Width = 55

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(5).Borders.Enable = 0
            'newRow4.Cells(6).Borders.Enable = 0
            newRow4.Range.Borders.Enable = 0
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            '            newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If



        If RblMultiple.Checked = True Then
            For il = 0 To dtSys2.Rows.Count - 1
                finaltotal = dtSys2.Rows(il)(2).ToString()
                finaltotal1 = dtSys2.Rows(il)(3).ToString()
            Next

            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Font.Name = "Arial"

            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(4).Shading.BackgroundPatternColor = RGB(0, 0, 128)

            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "ક્વોલિટી કન્ટ્રોલ લેબોરેટરીની ટોટલ કિમત (સિસ્ટમ-૨)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR QUALITY CONTROL LAB (SYS-2)"
            End If
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys2total = finaltotal
            newRow4.Cells(4).Range.Font.Name = "Rupee"
            newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys2mutotal = finaltotal1

            newRow4.Cells(2).Width = 310
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 50
            newRow4.Cells(4).Width = 50
            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            '  newRow4.Cells(4).Borders.Enable = 0
            '  newRow4.Cells(5).Borders.Enable = 0
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(2).Range.Font.Size = 8

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            '           newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If


        If RblSingle.Checked Then
            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વોશિંગ,ફીલિંગ અને પેકિંગ મશીનરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            End If
            'newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            'newRowStatic1.Range.Borders.Enable = 1
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(3).Width = 275
            newRowStatic1.Cells(1).Width = 80
            newRowStatic1.Cells(2).Width = 45
            newRowStatic1.Cells(4).Width = 80

            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0

        ElseIf RblOther.Checked Then
            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વોશિંગ,ફીલિંગ અને પેકિંગ મશીનરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            End If
            newRowStatic1.Range.Borders.Enable = 0
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(1).Width = 80
            newRowStatic1.Cells(2).Width = 30
            newRowStatic1.Cells(3).Width = 295
            newRowStatic1.Cells(4).Width = 25
            newRowStatic1.Cells(5).Width = 25
            newRowStatic1.Cells(6).Width = 25



            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(5).Borders.Enable = 0
            newRowStatic1.Cells(6).Borders.Enable = 0


        ElseIf RblMultiple.Checked Then
            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વોશિંગ,ફીલિંગ અને પેકિંગ મશીનરી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "WASHING, FILLING & PACKING MACHINERIES"
            End If
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(1).Width = 70
            newRowStatic1.Cells(2).Width = 45
            newRowStatic1.Cells(3).Width = 300
            newRowStatic1.Cells(4).Width = 15
            newRowStatic1.Cells(5).Width = 50

            newRowStatic1.Cells(5).Borders.Enable = 0
            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0

        End If




        finaltotal = 0
        finaltotal1 = 0
        If RdbGujarati.Checked Then
            limit = 6
        ElseIf RdbEnglish.Checked Then

            limit = 6
        End If

        flgsys = 0
        For i = 0 To limit
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow4.Range.Font.Bold = 0
            'newRow4.Range.Font.Bold = True 'Indian' 
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Color = Word.WdColor.wdColorBlack
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Borders.Enable = 0
            newRow4.Cells(1).Range.Text = ""
            If i = 2 Then
                If RdbEnglish.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                ElseIf RdbGujarati.Checked Then
                    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૩"
                ElseIf RdbHindi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                ElseIf RdbMarathi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                ElseIf RdbTamil.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                ElseIf RdbTelugu.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-3"
                End If
            End If
            If dtSys3.Rows.Count > i Then

                If RblSingle.Checked = True Then
                    If flgsys = 0 Then
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    newRow4.Cells(2).Range.Text = "3." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)("Description").ToString()
                    newRow4.Cells(4).Range.Text = "`   " + dtSys3.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    finaltotal = finaltotal + Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())
                    newRow4.Cells(3).Width = 275
                    newRow4.Cells(1).Width = 80
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(4).Width = 80

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    newRow4 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)(3).ToString()
                    newRow4.Range.Borders.Enable = 0


                End If

                If RblOther.Checked = True Then
                    If flgsys = 0 Then

                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        newRow4.Cells(6).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "3." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)("Description").ToString()
                    newRow4.Cells(4).Range.Text = dtSys3.Rows(i)("Qty").ToString()
                    newRow4.Cells(5).Range.Text = "`    " + dtSys3.Rows(i)(2).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Text = "`    " + dtSys3.Rows(i)(4).ToString()
                    'newRow4.Cells(6).Range.Text = "`    " + dtSys3.Rows(i)(4).ToString()

                    ''newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    newRow4.Range.Font.Bold = 0
                    'newRow4.Range.Font.Bold = True 'Indian' 
                    ' newRow4.Range.Font.Size = 10

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(5).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorBlack

                    'newRow4.Cells(1).Width = 80
                    'newRow4.Cells(2).Width = 30
                    'newRow4.Cells(3).Width = 190
                    'newRow4.Cells(4).Width = 45
                    'newRow4.Cells(5).Width = 80
                    'newRow4.Cells(6).Width = 55

                    newRow4.Cells(1).Width = 65
                    newRow4.Cells(2).Width = 30
                    newRow4.Cells(3).Width = 215
                    newRow4.Cells(4).Width = 45
                    newRow4.Cells(5).Width = 70
                    newRow4.Cells(6).Width = 55

                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys3.Rows(i)(3).ToString())
                    sumall += Convert.ToDecimal(dtSys3.Rows(i)(3).ToString())

                    qty = qty + Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys3.Rows(i)(4).ToString())
                    sumall2 += Convert.ToDecimal(dtSys3.Rows(i)(4).ToString())
                    newRow4 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)("Capacity").ToString()
                    newRow4.Range.Borders.Enable = 0
                End If
                If RblMultiple.Checked = True Then

                    If flgsys = 0 Then

                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        flgsys = 1
                    End If

                    Dim t As Integer
                    t = i + 1
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "3." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)("Description").ToString()
                    newRow4.Cells(4).Range.Text = "`    " + dtSys3.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    newRow4.Cells(5).Range.Text = "`    " + dtSys3.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"

                    newRow4.Cells(1).Width = 70
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(3).Width = 265
                    newRow4.Cells(4).Width = 50
                    newRow4.Cells(5).Width = 50

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    newRow4 = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(3).Range.Text = dtSys3.Rows(i)("Capacity").ToString()
                    newRow4.Range.Borders.Enable = 0

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys3.Rows(i)(2).ToString())

                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys3.Rows(i)(3).ToString())
                    sumall2 += Convert.ToDecimal(dtSys3.Rows(i)(3).ToString())

                End If
            End If

        Next
        'If RdbEnglish.Checked Then

        '    If dtSys3.Rows.Count < 12 Then
        '        Dim IK As Integer
        '        IK = 0
        '        IK = 12 - dtSys3.Rows.Count
        '        For index = 1 To IK
        '            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        '            newRow4.Range.Font.Name = "Arial"
        '            newRow4.Range.Font.Size = 9
        '            newRow4.Cells(1).Range.Text = ""
        '        Next

        '    End If

        'End If



        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.ParagraphFormat.SpaceBefore = 0
            newRow4.Range.ParagraphFormat.SpaceAfter = 0
            newRow4.Range.ParagraphFormat.SpaceAfter = 0.5
            newRow4.Range.ParagraphFormat.SpaceBefore = 0.5

            '            newRow4.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "પેકિંગ મશીન ની ટોટલ કિમત (સિસ્ટમ-૩)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            End If

            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys3total = finaltotal


            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(1).Range.Font.Size = 12

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Width = 330
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 80
            'newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            ' newRow4.Borders.Enable = 0
            '  newRow4.Borders.Enable = 0
            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            'newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


        End If
        If RblOther.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Bold = True
            newRow4.Cells(1).Range.Text = ""
            newRow4.Cells(2).Range.Text = ""

            newRow4.Cells(1).Range.Text = "`    " + Convert.ToString(finaltotal)
            newRow4.Cells(1).Range.Font.Name = "Rupee"
            newRow4.Cells(1).Range.Text = ""
            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "પેકિંગ મશીન ની ટોટલ કિમત (સિસ્ટમ-૩)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            newRow4.Cells(6).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys3total = finaltotal1

            newRow4.Cells(6).Range.Font.Name = "Rupee"

            newRow4.Cells(1).Width = 80
            newRow4.Cells(2).Width = 300
            newRow4.Cells(3).Width = 15
            newRow4.Cells(4).Width = 15
            newRow4.Cells(5).Width = 15
            newRow4.Cells(6).Width = 55

            newRow4.Range.Borders.Enable = 0
            newRow4.Borders.Enable = 0
            newRow4.Borders.Enable = 0
            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(5).Borders.Enable = 0
            'newRow4.Cells(6).Borders.Enable = 0

            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            '            newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If

        If RblMultiple.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(4).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "પેકિંગ મશીન ની ટોટલ કિમત (સિસ્ટમ-૩)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR PACKING M/C (SYS-3)"
            End If
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys3total = finaltotal

            newRow4.Cells(4).Range.Font.Name = "Rupee"
            newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys3mutotal = finaltotal1


            newRow4.Cells(2).Width = 310
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 50
            newRow4.Cells(4).Width = 50

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            '  newRow4.Cells(4).Borders.Enable = 0

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Size = 8
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            '           newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If

        If RblSingle.Checked Then
            If rbSys3NbYes.Checked Then

                Dim newRowStatic2 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowStatic2.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowStatic2.Range.Font.Name = "Arial"
                newRowStatic2.Range.Font.Size = 9
                newRowStatic2.Range.Font.Bold = True
                newRowStatic2.Cells(1).Range.Text = "NB : YOU MAY SELECT ALL / EITHER AS PER REQUIREMENT  FROM SYS.3"
                'newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
                'newRowStatic1.Range.Borders.Enable = 1
                newRowStatic2.Cells(1).Range.Font.Bold = True 'Indian' 
                newRowStatic2.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                newRowStatic2.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowStatic2.Cells(1).Width = 350
                newRowStatic2.Cells(2).Width = 43
                newRowStatic2.Cells(3).Width = 43
                newRowStatic2.Cells(4).Width = 44
                newRowStatic2.Cells(3).Borders.Enable = 0
                newRowStatic2.Cells(1).Borders.Enable = 0
                newRowStatic2.Cells(2).Borders.Enable = 0
                newRowStatic2.Cells(4).Borders.Enable = 0



                newRowStatic2.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowStatic2.Cells(4).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowStatic2.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            End If


            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            End If
            'newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            'newRowStatic1.Range.Borders.Enable = 1
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(3).Width = 275
            newRowStatic1.Cells(1).Width = 80
            newRowStatic1.Cells(2).Width = 45
            newRowStatic1.Cells(4).Width = 80

            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        ElseIf RblOther.Checked Then
            If rbSys3NbYes.Checked Then


                Dim newRowStatic2 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowStatic2.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowStatic2.Range.Font.Name = "Arial"
                newRowStatic2.Range.Font.Size = 9
                newRowStatic2.Range.Font.Bold = True
                newRowStatic2.Cells(1).Range.Text = "NB : YOU MAY SELECT ALL / EITHER AS PER REQUIREMENT  FROM SYS.3"
                'newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
                'newRowStatic1.Range.Borders.Enable = 1
                newRowStatic2.Cells(1).Range.Font.Bold = True 'Indian' 
                newRowStatic2.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                newRowStatic2.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowStatic2.Cells(1).Width = 350
                newRowStatic2.Cells(2).Width = 43
                newRowStatic2.Cells(3).Width = 43
                newRowStatic2.Cells(4).Width = 44
                newRowStatic2.Cells(3).Borders.Enable = 0
                newRowStatic2.Cells(1).Borders.Enable = 0
                newRowStatic2.Cells(2).Borders.Enable = 0
                newRowStatic2.Cells(4).Borders.Enable = 0



                newRowStatic2.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowStatic2.Cells(4).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle



                Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowStatic1.Range.Font.Name = "Arial"
                newRowStatic1.Range.Font.Size = 9
                newRowStatic1.Range.Font.Bold = True

                If RdbEnglish.Checked Then
                    newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
                ElseIf RdbGujarati.Checked Then
                    newRowStatic1.Cells(3).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજી"
                ElseIf RdbHindi.Checked Then
                    newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
                ElseIf RdbMarathi.Checked Then
                    newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
                ElseIf RdbTamil.Checked Then
                    newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
                ElseIf RdbTelugu.Checked Then
                    newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
                End If
                'newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"

                newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
                newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                newRowStatic1.Cells(1).Width = 80
                newRowStatic1.Cells(2).Width = 30
                newRowStatic1.Cells(3).Width = 190
                newRowStatic1.Cells(4).Width = 45
                newRowStatic1.Cells(5).Width = 80
                newRowStatic1.Cells(6).Width = 55


                newRowStatic1.Cells(3).Borders.Enable = 0
                newRowStatic1.Cells(1).Borders.Enable = 0
                newRowStatic1.Cells(2).Borders.Enable = 0
                newRowStatic1.Cells(4).Borders.Enable = 0
                newRowStatic1.Cells(5).Borders.Enable = 0
                newRowStatic1.Cells(6).Borders.Enable = 0
                newRowStatic1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowStatic1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowStatic1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowStatic1.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowStatic1.Cells(5).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowStatic1.Cells(6).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            End If


        ElseIf RblMultiple.Checked Then
            If rbSys3NbYes.Checked Then
                Dim newRowStatic2 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                newRowStatic2.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowStatic2.Range.Font.Name = "Arial"
                newRowStatic2.Range.Font.Size = 9
                newRowStatic2.Range.Font.Bold = True
                newRowStatic2.Cells(1).Range.Text = "NB : YOU MAY SELECT ALL / EITHER AS PER REQUIREMENT  FROM SYS.3"
                'newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
                'newRowStatic1.Range.Borders.Enable = 1
                newRowStatic2.Cells(1).Range.Font.Bold = True 'Indian' 
                newRowStatic2.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                newRowStatic2.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowStatic2.Cells(1).Width = 350
                newRowStatic2.Cells(2).Width = 43
                newRowStatic2.Cells(3).Width = 43
                newRowStatic2.Cells(4).Width = 44
                newRowStatic2.Cells(3).Borders.Enable = 0
                newRowStatic2.Cells(1).Borders.Enable = 0
                newRowStatic2.Cells(2).Borders.Enable = 0
                newRowStatic2.Cells(4).Borders.Enable = 0
                newRowStatic2.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowStatic2.Cells(4).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            End If

            Dim newRowStatic1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRowStatic1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRowStatic1.Range.Font.Name = "Arial"
            newRowStatic1.Range.Font.Size = 9
            newRowStatic1.Range.Font.Bold = True
            If RdbEnglish.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbGujarati.Checked Then
                newRowStatic1.Cells(3).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજી"
            ElseIf RdbHindi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbMarathi.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTamil.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            ElseIf RdbTelugu.Checked Then
                newRowStatic1.Cells(3).Range.Text = "VALUE ADDED TECHNOLOGY"
            End If
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(3).Range.Font.Bold = True 'Indian' 
            newRowStatic1.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
            newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowStatic1.Cells(1).Width = 70
            newRowStatic1.Cells(2).Width = 45
            newRowStatic1.Cells(3).Width = 265
            newRowStatic1.Cells(4).Width = 50
            newRowStatic1.Cells(5).Width = 50

            newRowStatic1.Cells(5).Borders.Enable = 0
            newRowStatic1.Cells(3).Borders.Enable = 0
            newRowStatic1.Cells(1).Borders.Enable = 0
            newRowStatic1.Cells(2).Borders.Enable = 0
            newRowStatic1.Cells(4).Borders.Enable = 0
            newRowStatic1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(5).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowStatic1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        End If




        finaltotal = 0
        finaltotal1 = 0
        If RdbEnglish.Checked Then

            limit = 5
        ElseIf RdbGujarati.Checked Then
            limit = 5
        End If

        flgsys = 0
        For i = 0 To limit


            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow4.Range.Font.Bold = 0

            '   newRow4.Range.Font.Bold = True 'Indian' 
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Color = Word.WdColor.wdColorBlack
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Borders.Enable = 0
            newRow4.Cells(1).Range.Text = ""
            If i = 3 Then
                If RdbEnglish.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                ElseIf RdbGujarati.Checked Then
                    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૪"
                    'newRow4.Range.ParagraphFormat.SpaceAfter = 0
                    'newRow4.Range.ParagraphFormat.SpaceBefore = 0

                ElseIf RdbHindi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                ElseIf RdbMarathi.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                ElseIf RdbTamil.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                ElseIf RdbTelugu.Checked Then
                    newRow4.Cells(1).Range.Text = "SYS-4"
                End If
            End If

            If dtSys4.Rows.Count > i Then

                If RblSingle.Checked = True Then
                    If flgsys = 0 Then
                        'If RdbEnglish.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'ElseIf RdbGujarati.Checked Then
                        '    newRow4.Cells(1).Range.Text = "સિસ્ટમ-૪"
                        'ElseIf RdbHindi.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'ElseIf RdbMarathi.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'ElseIf RdbTamil.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'ElseIf RdbTelugu.Checked Then
                        '    newRow4.Cells(1).Range.Text = "SYS-4"
                        'End If
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1

                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    newRow4.Cells(2).Range.Text = "4." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys4.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = "`   " + dtSys4.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    finaltotal = finaltotal + Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())


                    newRow4.Cells(3).Width = 275
                    newRow4.Cells(1).Width = 80
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(4).Width = 80

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                End If

                If RblOther.Checked = True Then
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    If flgsys = 0 Then

                        newRow4.Range.Borders.Enable = 1
                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1
                        newRow4.Cells(6).Borders.Enable = 1
                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1
                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "4." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys4.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = dtSys4.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Text = "`    " + dtSys4.Rows(i)(2).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Font.Name = "Rupee"
                    newRow4.Cells(6).Range.Text = "`    " + dtSys4.Rows(i)(4).ToString()
                    ''newRow4.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    newRow4.Range.Font.Bold = 0
                    ' newRow4.Range.Font.Bold = True 'Indian' 
                    '  newRow4.Range.Font.Size = 10

                    newRow4.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRow4.Cells(5).Range.Font.Color = Word.WdColor.wdColorBlack

                    'newRow4.Cells(1).Width = 80
                    'newRow4.Cells(2).Width = 30
                    'newRow4.Cells(3).Width = 190
                    'newRow4.Cells(4).Width = 45
                    'newRow4.Cells(5).Width = 80
                    'newRow4.Cells(6).Width = 55
                    newRow4.Cells(1).Width = 65
                    newRow4.Cells(2).Width = 30
                    newRow4.Cells(3).Width = 215
                    newRow4.Cells(4).Width = 45
                    newRow4.Cells(5).Width = 70
                    newRow4.Cells(6).Width = 55

                    newRow4.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                    finaltotal = finaltotal + Convert.ToDecimal(dtSys4.Rows(i)(3).ToString())
                    sumall += Convert.ToDecimal(dtSys4.Rows(i)(3).ToString())

                    qty = qty + Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys4.Rows(i)(4).ToString())
                    sumall2 += Convert.ToDecimal(dtSys4.Rows(i)(4).ToString())

                End If
                If RblMultiple.Checked = True Then
                    If flgsys = 0 Then

                        newRow4.Cells(1).Borders.Enable = 1
                        newRow4.Cells(2).Borders.Enable = 1
                        newRow4.Cells(3).Borders.Enable = 1
                        newRow4.Cells(4).Borders.Enable = 1
                        newRow4.Cells(5).Borders.Enable = 1

                        flgsys = 1
                    End If
                    Dim t As Integer
                    t = i + 1

                    '    Dim newRow As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
                    newRow4.Cells(2).Range.Text = "4." + t.ToString()
                    newRow4.Cells(3).Range.Text = dtSys4.Rows(i)(1).ToString()
                    newRow4.Cells(4).Range.Text = "`    " + dtSys4.Rows(i)(2).ToString()
                    newRow4.Cells(4).Range.Font.Name = "Rupee"
                    newRow4.Cells(5).Range.Text = "`    " + dtSys4.Rows(i)(3).ToString()
                    newRow4.Cells(5).Range.Font.Name = "Rupee"

                    newRow4.Cells(1).Width = 70
                    newRow4.Cells(2).Width = 45
                    newRow4.Cells(3).Width = 265
                    newRow4.Cells(4).Width = 50
                    newRow4.Cells(5).Width = 50

                    newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRow4.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


                    finaltotal = finaltotal + Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())
                    sumall += Convert.ToDecimal(dtSys4.Rows(i)(2).ToString())
                    finaltotal1 = finaltotal1 + Convert.ToDecimal(dtSys4.Rows(i)(3).ToString())
                    sumall2 += Convert.ToDecimal(dtSys4.Rows(i)(3).ToString())

                End If
            End If
        Next

        'If RdbEnglish.Checked Then

        '    If dtSys4.Rows.Count < 5 Then
        '        Dim IK As Integer
        '        IK = 0
        '        IK = 7 - dtSys4.Rows.Count
        '        For index = 1 To IK
        '            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
        '            newRow4.Range.Font.Name = "Arial"
        '            newRow4.Range.Font.Size = 9
        '            newRow4.Cells(1).Range.Text = ""
        '        Next

        '    End If
        'End If

        If RblSingle.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            '            newRow4.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજીની ટોટલ કિંમત (સિસ્ટમ-૪)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            End If
            'newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys4total = finaltotal


            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(2).Width = 330
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 80
            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0

            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            'newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True

        End If
        If RblOther.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"
            newRow4.Range.Font.Size = 9
            newRow4.Range.Font.Bold = True

            newRow4.Cells(1).Range.Text = ""
            newRow4.Cells(2).Range.Text = ""


            newRow4.Cells(1).Range.Text = "`    " + Convert.ToString(finaltotal)
            newRow4.Cells(1).Range.Font.Name = "Rupee"
            newRow4.Cells(1).Range.Text = ""
            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજીની ટોટલ કિંમત (સિસ્ટમ-૪)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            End If

            newRow4.Cells(6).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys4total = finaltotal1
            newRow4.Cells(6).Range.Font.Name = "Rupee"


            newRow4.Cells(1).Width = 80
            newRow4.Cells(2).Width = 300
            newRow4.Cells(3).Width = 15
            newRow4.Cells(4).Width = 15
            newRow4.Cells(5).Width = 15
            newRow4.Cells(6).Width = 55

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(5).Borders.Enable = 0
            '  newRow4.Cells(6).Borders.Enable = 0
            newRow4.Range.Borders.Enable = 0
            newRow4.Range.Borders.Enable = 0

            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
        End If

        If RblMultiple.Checked = True Then
            Dim newRow4 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4.Cells(1).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(2).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(3).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Cells(4).Shading.BackgroundPatternColor = RGB(0, 0, 128)
            newRow4.Range.Font.Name = "Arial"

            newRow4.Cells(1).Range.Text = ""

            If RdbEnglish.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbGujarati.Checked Then
                newRow4.Cells(2).Range.Text = "વેલ્યુ એડેડ ટેકનોલોજીની ટોટલ કિંમત (સિસ્ટમ-૪)"
            ElseIf RdbHindi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbMarathi.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTamil.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            ElseIf RdbTelugu.Checked Then
                newRow4.Cells(2).Range.Text = "TOTAL PRICE FOR VALUE ADDED TECHNOLOGY (SYS-4)"
            End If

            newRow4.Cells(3).Range.Font.Name = "Rupee"
            newRow4.Cells(3).Range.Text = "`    " + Convert.ToString(finaltotal)
            sys4total = finaltotal
            newRow4.Cells(4).Range.Font.Name = "Rupee"
            newRow4.Cells(4).Range.Text = "`    " + Convert.ToString(finaltotal1)
            sys4mutotal = finaltotal1

            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(2).Width = 310
            newRow4.Cells(1).Width = 70
            newRow4.Cells(3).Width = 50
            newRow4.Cells(4).Width = 50

            newRow4.Cells(1).Borders.Enable = 0
            newRow4.Cells(2).Borders.Enable = 0
            newRow4.Cells(3).Borders.Enable = 0
            'newRow4.Cells(4).Borders.Enable = 0
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRow4.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


            newRow4.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow4.Cells(2).Range.Font.Size = 8

            newRow4.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

            newRow4.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRow4.Range.Font.Bold = 1
            newRow4.Range.Font.Bold = True
            '           newRow4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        End If
        sumall = sys1total + sys2total + sys3total + sys4total
        sumall2 = sys1mutotal + sys2mutotal + sys3mutotal + sys4mutotal

        If rbSys4NbYes.Checked Then

            Dim newRow4Footer As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4Footer.Borders.Enable = 0
            newRow4Footer.Range.Borders.Enable = 0
            newRow4Footer.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            newRow4Footer.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            If RdbEnglish.Checked Then
                newRow4Footer.Cells(1).Range.Text = "NB:VALUE ADDED TECH IS FOR BETTERMENT OF TASTE OF WATER YOU MAY SELECT ALL /"
            ElseIf RdbGujarati.Checked Then
                newRow4Footer.Cells(1).Range.Text = "નોટ: પાણી ના ટેસ્ટ ની ગુણવત્તા સુધારવા માટે વેલ્યુ એડેડ ટેકનોલોજી પ્રસ્તુત કરેલ છે તમે બધા અથવા "
            ElseIf RdbHindi.Checked Then
                newRow4Footer.Cells(1).Range.Text = "NB:VALUE ADDED TECH IS FOR BETTERMENT OF TASTE OF WATER YOU MAY SELECT ALL /"
            ElseIf RdbMarathi.Checked Then
                newRow4Footer.Cells(1).Range.Text = "NB:VALUE ADDED TECH IS FOR BETTERMENT OF TASTE OF WATER YOU MAY SELECT ALL /"
            ElseIf RdbTamil.Checked Then
                newRow4Footer.Cells(1).Range.Text = "NB:VALUE ADDED TECH IS FOR BETTERMENT OF TASTE OF WATER YOU MAY SELECT ALL /"
            ElseIf RdbTelugu.Checked Then
                newRow4Footer.Cells(1).Range.Text = "NB:VALUE ADDED TECH IS FOR BETTERMENT OF TASTE OF WATER YOU MAY SELECT ALL /"
            End If

            'newRow4Footer.Cells(1).Range.Text = "NB:VALUE ADDED TECH IS FOR BETTERMENT OF TASTE OF WATER YOU MAY SELECT ALL /"
            newRow4Footer.Cells(1).Range.Font.Name = "Arial"
            newRow4Footer.Cells(1).Width = 480
            newRow4Footer.Cells(1).Range.Font.Size = 9
            newRow4Footer.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow4Footer.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4Footer.Cells(1).Range.Font.Bold = True


            Dim newRow4Footer1 As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4Footer1.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            If RdbEnglish.Checked Then
                newRow4Footer1.Cells(1).Range.Text = "          EITHER AS PER YOUR REQUIREMENT FROM (SYS-4)"
            ElseIf RdbGujarati.Checked Then
                newRow4Footer1.Cells(1).Range.Text = "          (સિસ્ટમ -૪)માંથી તમારી  જરૂરીયાત અનુસાર પસંદ કરી શકો છો."
            ElseIf RdbHindi.Checked Then
                newRow4Footer1.Cells(1).Range.Text = "          EITHER AS PER YOUR REQUIREMENT FROM (SYS-4)"
            ElseIf RdbMarathi.Checked Then
                newRow4Footer1.Cells(1).Range.Text = "          EITHER AS PER YOUR REQUIREMENT FROM (SYS-4)"
            ElseIf RdbTamil.Checked Then
                newRow4Footer1.Cells(1).Range.Text = "          EITHER AS PER YOUR REQUIREMENT FROM (SYS-4)"
            ElseIf RdbTelugu.Checked Then
                newRow4Footer1.Cells(1).Range.Text = "          EITHER AS PER YOUR REQUIREMENT FROM (SYS-4)"
            End If

            'newRow4Footer1.Cells(1).Range.Text = "          EITHER AS PER YOUR REQUIREMENT FROM (SYS-4)"
            newRow4Footer1.Cells(1).Range.Font.Name = "Arial"
            newRow4Footer1.Cells(1).Width = 480

            newRow4Footer1.Cells(1).Range.Font.Size = 9
            newRow4Footer1.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack

            newRow4Footer1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4Footer1.Cells(1).Range.Font.Bold = True

        End If



        If RblSingle.Checked = True Then

            Dim newRow4BLueFooter As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            If RdbEnglish.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"

            ElseIf RdbGujarati.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોડ્કટની ટોટલ કિંમત(સિસ્ટમ 1+2+3+4)"

            ElseIf RdbHindi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"

            ElseIf RdbMarathi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"

            ElseIf RdbTamil.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"

            ElseIf RdbTelugu.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"


            End If
            'If RblPriceYes.Checked = True Then
            ' newRow4BLueFooter.Cells(4).Range.Text = "` " + Convert.ToString(sumall)
            'End If

            If RblPriceYes.Checked Then
                newRow4BLueFooter.Cells(4).Range.Text = "`" + Convert.ToString(sumall)
            Else
                newRow4BLueFooter.Cells(4).Range.Text = ""
            End If

            'newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોડ્કટની ટોટલ કિંમત(સિસ્ટમ 1+2+3+4)"
            newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
            newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"

            newRow4BLueFooter.Cells(1).Width = 340
            newRow4BLueFooter.Cells(2).Width = 15
            newRow4BLueFooter.Cells(3).Width = 45
            newRow4BLueFooter.Cells(4).Width = 80


            newRow4BLueFooter.Cells(1).Range.Font.Size = 9
            newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            newRow4BLueFooter.Range.Borders.Enable = 0
            newRow4BLueFooter.Cells(1).Borders.Enable = 0
            newRow4BLueFooter.Cells(2).Borders.Enable = 0
            newRow4BLueFooter.Cells(3).Borders.Enable = 0
            newRow4BLueFooter.Cells(4).Borders.Enable = 0


            newRow4BLueFooter.Cells(1).Range.Font.Size = 9
            newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            newRow4BLueFooter.Range.Borders.Enable = 0
            newRow4BLueFooter.Cells(1).Borders.Enable = 0
            newRow4BLueFooter.Cells(2).Borders.Enable = 0
            newRow4BLueFooter.Cells(3).Borders.Enable = 0
            newRow4BLueFooter.Cells(4).Borders.Enable = 0



            If txtspdiscount.Text.Trim() <> "" Then
                newRow4BLueFooter = objDoc1.Tables(1).Rows.Add(Type.Missing)

                newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbGujarati.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "સ્પેશિયલ ડિસ્કાઉન્ટ==>>"
                ElseIf RdbHindi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbMarathi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTamil.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTelugu.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                End If
                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(4).Range.Text = "` " + txtspdiscount.Text.Trim()
                newRow4BLueFooter.Cells(1).Range.Font.Size = 9
                newRow4BLueFooter.Cells(1).Width = 340
                newRow4BLueFooter.Cells(2).Width = 15
                newRow4BLueFooter.Cells(3).Width = 45
                newRow4BLueFooter.Cells(4).Width = 80
                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4BLueFooter.Cells(1).Range.Font.Bold = True

                newRow4BLueFooter = objDoc1.Tables(1).Rows.Add(Type.Missing)
                Dim finaled As Decimal

                finaled = Convert.ToDecimal(txtspdiscount.Text.Trim())
                newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbGujarati.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોજેક્ટ ની અંતીમ  કિમત"
                ElseIf RdbHindi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbMarathi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTamil.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTelugu.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                End If
                sumall = sumall - finaled

                If RblPriceYes.Checked = True Then
                    newRow4BLueFooter.Cells(4).Range.Text = "` " + Convert.ToString(sumall)
                Else
                    newRow4BLueFooter.Cells(4).Range.Text = ""
                End If


                newRow4BLueFooter.Cells(1).Width = 340
                newRow4BLueFooter.Cells(2).Width = 15
                newRow4BLueFooter.Cells(3).Width = 45
                newRow4BLueFooter.Cells(4).Width = 80

                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite

                newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            End If


        End If
        If RblOther.Checked = True Then

            Dim newRow4BLueFooter As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

            newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            sumall = sys1total + sys2total + sys3total + sys4total

            If RdbEnglish.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(6).Range.Text = Convert.ToString(sumall)

            ElseIf RdbGujarati.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોડ્કટની ટોટલ કિંમત(સિસ્ટમ 1+2+3+4)"
                newRow4BLueFooter.Cells(6).Range.Text = Convert.ToString(sumall)
            ElseIf RdbHindi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(6).Range.Text = Convert.ToString(sumall)
            ElseIf RdbMarathi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(6).Range.Text = Convert.ToString(sumall)
            ElseIf RdbTamil.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(6).Range.Text = (Convert.ToString(sumall))
            ElseIf RdbTelugu.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(6).Range.Text = Convert.ToString(sumall)
            End If
            'navin 19-3-2014
            If RblPriceYes.Checked = True Then
                newRow4BLueFooter.Cells(6).Range.Text = "`" + Convert.ToString(sumall)
            Else
                newRow4BLueFooter.Cells(6).Range.Text = ""
            End If
            newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(2).Range.Font.Name = "Arial"
            newRow4BLueFooter.Cells(6).Range.Font.Name = "Rupee"
            newRow4BLueFooter.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(1).Width = 365
            newRow4BLueFooter.Cells(2).Width = 15
            newRow4BLueFooter.Cells(3).Width = 15
            newRow4BLueFooter.Cells(4).Width = 15
            newRow4BLueFooter.Cells(5).Width = 15
            newRow4BLueFooter.Cells(6).Width = 55


            newRow4BLueFooter.Cells(1).Range.Font.Size = 9
            newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            newRow4BLueFooter.Range.Borders.Enable = 0
            newRow4BLueFooter.Cells(1).Borders.Enable = 0
            newRow4BLueFooter.Cells(2).Borders.Enable = 0
            newRow4BLueFooter.Cells(3).Borders.Enable = 0
            newRow4BLueFooter.Cells(4).Borders.Enable = 0
            newRow4BLueFooter.Cells(5).Borders.Enable = 0
            newRow4BLueFooter.Cells(6).Borders.Enable = 0


            If txtspdiscount.Text.Trim() <> "" Then
                newRow4BLueFooter = objDoc1.Tables(1).Rows.Add(Type.Missing)

                newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbGujarati.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "સ્પેશિયલ ડિસ્કાઉન્ટ==>>"
                ElseIf RdbHindi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbMarathi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTamil.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTelugu.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                End If
                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(6).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(6).Range.Text = "` " + txtspdiscount.Text.Trim()
                newRow4BLueFooter.Cells(1).Range.Font.Size = 9
                newRow4BLueFooter.Cells(1).Width = 375
                newRow4BLueFooter.Cells(2).Width = 15
                newRow4BLueFooter.Cells(3).Width = 15
                newRow4BLueFooter.Cells(4).Width = 45
                newRow4BLueFooter.Cells(5).Width = 70
                newRow4BLueFooter.Cells(6).Width = 55
                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
                newRow4BLueFooter.Cells(6).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4BLueFooter.Cells(1).Range.Font.Bold = True

                newRow4BLueFooter = objDoc1.Tables(1).Rows.Add(Type.Missing)
                Dim finaled As Decimal

                finaled = Convert.ToDecimal(txtspdiscount.Text.Trim())
                newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbGujarati.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "અંતીમ મિનરલ વોટર પ્રોજેક્ટ ની કિમત"
                ElseIf RdbHindi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbMarathi.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTamil.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTelugu.Checked Then
                    newRow4BLueFooter.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                End If
                sumall = sys1total + sys2total + sys3total + sys4total
                sumall2 = sys1mutotal + sys2mutotal + sys3mutotal + sys4mutotal
                sumall = sumall - finaled
                newRow4BLueFooter.Cells(6).Range.Text = "` " + Convert.ToString(sumall)
                newRow4BLueFooter.Cells(1).Width = 375
                newRow4BLueFooter.Cells(2).Width = 15
                newRow4BLueFooter.Cells(3).Width = 15
                newRow4BLueFooter.Cells(4).Width = 45
                newRow4BLueFooter.Cells(5).Width = 70
                newRow4BLueFooter.Cells(6).Width = 55

                newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                newRow4BLueFooter.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite

                newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
                newRow4BLueFooter.Cells(6).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            End If



        End If

        If RblMultiple.Checked = True Then

            Dim newRow4BLueFooter As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)
            sumall = sys1total + sys2total + sys3total + sys4total
            sumall2 = sys1mutotal + sys2mutotal + sys3mutotal + sys4mutotal

            newRow4BLueFooter.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
            If RdbEnglish.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)
            ElseIf RdbGujarati.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "મિનરલ વોટર પ્રોડ્કટની ટોટલ કિંમત(સિસ્ટમ 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            ElseIf RdbHindi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            ElseIf RdbMarathi.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            ElseIf RdbTamil.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            ElseIf RdbTelugu.Checked Then
                newRow4BLueFooter.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PLANT (SYS 1+2+3+4)"
                newRow4BLueFooter.Cells(4).Range.Text = Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = Convert.ToString(sumall2)

            End If
            'navin 19-3-2014
            If RblPriceYes.Checked = True Then
                newRow4BLueFooter.Cells(4).Range.Text = "` " + Convert.ToString(sumall)
                newRow4BLueFooter.Cells(5).Range.Text = "` " + Convert.ToString(sumall2)
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(5).Range.Font.Name = "Rupee"
            Else

                newRow4BLueFooter.Cells(4).Range.Text = ""
                newRow4BLueFooter.Cells(5).Range.Text = ""
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(5).Range.Font.Name = "Rupee"
            End If



            newRow4BLueFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(5).Range.Font.Color = Word.WdColor.wdColorWhite
            newRow4BLueFooter.Cells(1).Range.Font.Name = "Arial"
            newRow4BLueFooter.Cells(1).Width = 350
            newRow4BLueFooter.Cells(2).Width = 15
            newRow4BLueFooter.Cells(3).Width = 15
            newRow4BLueFooter.Cells(4).Width = 50
            newRow4BLueFooter.Cells(5).Width = 50
            newRow4BLueFooter.Cells(1).Range.Font.Size = 9
            newRow4BLueFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow4BLueFooter.Cells(1).Range.Font.Bold = True
            newRow4BLueFooter.Range.Borders.Enable = 0
            newRow4BLueFooter.Cells(1).Borders.Enable = 0
            newRow4BLueFooter.Cells(2).Borders.Enable = 0
            newRow4BLueFooter.Cells(3).Borders.Enable = 0
            newRow4BLueFooter.Cells(4).Borders.Enable = 0
            newRow4BLueFooter.Cells(5).Borders.Enable = 0

            If txtspdiscount.Text.Trim() <> "" Then
                Dim trdiscount As Word.Row = objDoc1.Tables(1).Rows.Add(Type.Missing)

                trdiscount.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbGujarati.Checked Then
                    trdiscount.Cells(1).Range.Text = "સ્પેશિયલ ડિસ્કાઉન્ટ==>>"
                ElseIf RdbHindi.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbMarathi.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTamil.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                ElseIf RdbTelugu.Checked Then
                    trdiscount.Cells(1).Range.Text = "SPECIAL DISCOUNT==>>"
                End If
                trdiscount.Cells(4).Range.Text = txtspdiscount.Text.Trim()
                trdiscount.Cells(5).Range.Text = txtspdiscount.Text.Trim()
                newRow4BLueFooter.Cells(4).Range.Font.Name = "Rupee"
                newRow4BLueFooter.Cells(5).Range.Font.Name = "Rupee"
                trdiscount.Cells(1).Width = 350
                trdiscount.Cells(2).Width = 15
                trdiscount.Cells(3).Width = 15
                trdiscount.Cells(4).Width = 50
                trdiscount.Cells(5).Width = 50
                trdiscount.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                trdiscount.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
                trdiscount.Cells(5).Range.Font.Color = Word.WdColor.wdColorWhite

                trdiscount.Cells(1).Range.Font.Name = "Arial"
                trdiscount.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                trdiscount.Cells(1).Range.Font.Bold = True

                trdiscount = objDoc1.Tables(1).Rows.Add(Type.Missing)
                Dim finaled As Decimal

                finaled = Convert.ToDecimal(txtspdiscount.Text.Trim())
                trdiscount.Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                If RdbEnglish.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbGujarati.Checked Then
                    trdiscount.Cells(1).Range.Text = "અંતીમ મિનરલ વોટર પ્રોજેક્ટ ની કિમત"
                ElseIf RdbHindi.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbMarathi.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTamil.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                ElseIf RdbTelugu.Checked Then
                    trdiscount.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT "
                End If
                sumall = sys1total + sys2total + sys3total + sys4total
                sumall2 = sys1mutotal + sys2mutotal + sys3mutotal + sys4mutotal

                sumall = sumall - finaled
                sumall2 = sumall2 - finaled
                trdiscount.Cells(4).Range.Font.Name = "Rupee"
                trdiscount.Cells(5).Range.Font.Name = "Rupee"
                trdiscount.Cells(4).Range.Text = "` " + Convert.ToString(sumall)
                trdiscount.Cells(5).Range.Text = "` " + Convert.ToString(sumall2)
                trdiscount.Cells(1).Width = 350
                trdiscount.Cells(2).Width = 15
                trdiscount.Cells(3).Width = 15
                trdiscount.Cells(4).Width = 50
                trdiscount.Cells(5).Width = 50

                trdiscount.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                trdiscount.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
                trdiscount.Cells(5).Range.Font.Color = Word.WdColor.wdColorWhite


                trdiscount.Cells(1).Range.Font.Name = "Arial"
                trdiscount.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                trdiscount.Cells(1).Range.Font.Bold = True
            End If


        End If




        objDoc1.Tables(1).Columns.Select()
        With objApp1.Selection.Borders(Word.WdBorderType.wdBorderTop)
            .LineStyle = objApp1.Options.DefaultBorderLineStyle
            .LineWidth = objApp1.Options.DefaultBorderLineWidth
            .Color = objApp1.Options.DefaultBorderColor
        End With
        objDoc1.Tables(1).Columns.Select()
        With objApp1.Selection.Borders(Word.WdBorderType.wdBorderRight)
            .LineStyle = objApp1.Options.DefaultBorderLineStyle
            .LineWidth = objApp1.Options.DefaultBorderLineWidth
            .Color = objApp1.Options.DefaultBorderColor

        End With
        objDoc1.Tables(1).Columns.Select()
        With objApp1.Selection.Borders(Word.WdBorderType.wdBorderLeft)
            .LineStyle = objApp1.Options.DefaultBorderLineStyle
            .LineWidth = objApp1.Options.DefaultBorderLineWidth
            .Color = objApp1.Options.DefaultBorderColor
        End With
        objDoc1.Tables(1).Columns.Select()
        With objApp1.Selection.Borders(Word.WdBorderType.wdBorderBottom)
            .LineStyle = objApp1.Options.DefaultBorderLineStyle
            .LineWidth = objApp1.Options.DefaultBorderLineWidth
            .Color = objApp1.Options.DefaultBorderColor
        End With

        Static Targets1 As Object
        Static paramSourceDocPath1 As Object
        If btnAddClear.Text = "View" Then

            paramSourceDocPath1 = appPath + "\OrderData" + "\" + Convert.ToString(QuationId) + ".doc"
            Targets1 = appPath + "\OrderData" + "\" + Convert.ToString(QuationId) + ".pdf"

        Else

            paramSourceDocPath1 = QtempPath + "\Letter2.doc"
            Targets1 = QtempPath + "\Letter2.pdf"

        End If
        objDoc1.SaveAs(paramSourceDocPath1)
        '   Dim Targets As Object
        ' Targets = QtempPath + "\Letter2.pdf"
        Dim formating As Object
        formating = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        objDoc1.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)

        'If FlagPdf = 1 Then
        '    'objDoc1.SaveAs("D:\adms21\OrderData" + "\" + Class1.global.GobalMaxId.ToString() + ".doc")
        '    objDoc1.SaveAs(appPath + "\OrderData" + "\" + Class1.global.GobalMaxId.ToString() + ".pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)
        'End If


        objDoc1.Close()
        objDoc1 = Nothing
        objApp1.Quit()
        objApp1 = Nothing


    End Sub




    Protected Sub FinalDucumetation()

        FirstPage()
        indexs()
        Dim missing As Object = System.Reflection.Missing.Value
        Dim wordApplication As Word.Application = Nothing

        Dim wordDocument As Word.Document
        Dim objApp12 As Word.Application
        Dim objDoc12 As Word.Document

        Dim oCollapseEnd As Object = Word.WdCollapseDirection.wdCollapseEnd

        Dim rng As Word.Range
        Dim _pdfforge As New PDF.PDF
        Dim _pdftext As New PDF.PDFText
        Dim GetImage As String

        GetImage = ""
        Dim strPrice
        ReDim strPrice(5)
        'For t As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
        '    Dim IsTicked As Boolean = CBool(GvTechnicalSYS1.Rows(t).Cells(1).Value)
        '    If IsTicked Then
        '        Dim GetSrNo As String = GvTechnicalSYS1.Rows(t).Cells(2).Value.ToString()
        '        GetImage += GetSrNo + ","
        '    End If
        'Next
        ''by edit 27/12/2013



        Dim selectSrNo = GetImage.TrimEnd(",")
        Dim isa As Integer
        isa = 1

        Dim str12 As String
        Dim da33 As New SqlDataAdapter
        Dim ds33 As New DataSet
        ReDim str(5)
        Dim Q As Int32
        Q = 0
        Dim p As Int32
        p = 0
        Dim r As Int32
        r = 0
        Dim StrImageCheckAvail
        'If selectSrNo.Length > 0 Then
        'rajesh Add
        Dim totalimage As Int32
        Try


            If (ds.Tables.Count >= 0) Then
                totalimage = Convert.ToInt32(ds.Tables(0).Rows.Count) * 3

            Else
                totalimage = 0
            End If
        Catch ex As Exception

        End Try
        If PicDefault.Image Is Nothing Then
            StrImageCheckAvail = ""
        Else
            StrImageCheckAvail = PicDefault.ImageLocation.ToString()

        End If
        str(p) = StrImageCheckAvail
        p = 1
        r = 1
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

        For t As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTechnicalSYS1.Rows(t).Cells(1).Value)
            If IsTicked Then
                str12 = "select * from Category_Master (NOLOCK) where SNo IN(" & GvTechnicalSYS1.Rows(t).Cells(2).Value.ToString() & ") and Capacity='" + txtCapacity1.Text.Trim() + "' AND MainCategory ='System 1' and LanguageId = " & LanguageId & " ORDER BY SNo"
                da33 = New SqlDataAdapter(str12, con1)
                ds33 = New DataSet
                da33.Fill(ds33)

                For x As Integer = 0 To ds33.Tables(0).Rows.Count - 1

                    Dim PhotoNo As String
                    For photocount As Integer = PhotoStartNo To PhotoEndNo
                        'Add multiple Photo 1 - 20 No     26-07-2016 Navin Goradara
                        PhotoNo = "Photo" & photocount
                        If ds33.Tables(0).Rows(x)(PhotoNo).ToString() <> "" Then
                            str(p) = ds33.Tables(0).Rows(x)(PhotoNo).ToString().Replace("D:", "\\192.168.1.102\") 'add navin .Replace("D:", "\\192.168.1.102\d$") 06-05-2015
                            p = p + 1
                            Q = Q + 1
                            isa = isa + 1
                            ReDim Preserve str(p)
                            If RblMultiple.Checked = True Then
                                strPrice(r) = GvTechnicalSYS1.Rows(t).Cells("Price1").Value.ToString() + "|" + "SYS-1 1." + (Q).ToString()
                            Else
                                strPrice(r) = GvTechnicalSYS1.Rows(t).Cells("Price").Value.ToString() + "|" + "SYS-1 1." + (Q).ToString()
                            End If

                            r = r + 1
                            ReDim Preserve strPrice(r)
                        End If
                    Next


                 
                Next
                ds33.Dispose()
                da33.Dispose()
            End If
        Next

        'End If
        Q = 0
        GetImage = ""
        'For t As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1
        '    Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(t).Cells(1).Value)
        '    If IsTicked Then
        '        Dim GetSrNo As String = GvTechnicalSYS2.Rows(t).Cells(2).Value.ToString()
        '        GetImage += GetSrNo + ","
        '    End If
        'Next


        For t As Integer = 0 To GvTechnicalSYS2.Rows.Count - 1
            Dim IsTicked As Boolean = CBool(GvTechnicalSYS2.Rows(t).Cells(1).Value)
            If IsTicked Then
                str12 = "select * from Category_Master (NOLOCK) where SNo IN(" & GvTechnicalSYS2.Rows(t).Cells(2).Value.ToString() & ") and Capacity='" + txtCapacity1.Text.Trim() + "' AND MainCategory ='System 2' and LanguageId = " & LanguageId & "  ORDER BY SNo"

                da33 = New SqlDataAdapter(str12, con1)
                ds33 = New DataSet
                da33.Fill(ds33)
                For x As Integer = 0 To ds33.Tables(0).Rows.Count - 1

                    Dim PhotoNo As String
                    For photocount As Integer = PhotoStartNo To PhotoEndNo
                        'Add multiple Photo 1 - 20 No     26-07-2016 Navin Goradara
                        PhotoNo = "Photo" & photocount
                        If ds33.Tables(0).Rows(x)(PhotoNo).ToString() <> "" Then
                            str(p) = ds33.Tables(0).Rows(x)(PhotoNo).ToString().Replace("D:", "\\192.168.1.102\")
                            p = p + 1
                            ReDim Preserve str(p)
                            Q = Q + 1
                            If RblMultiple.Checked = True Then
                                strPrice(r) = GvTechnicalSYS2.Rows(t).Cells("Price1").Value.ToString() + "|" + "SYS-2 2." + (Q).ToString()
                            Else
                                strPrice(r) = GvTechnicalSYS2.Rows(t).Cells("Price").Value.ToString() + "|" + "SYS-2 2." + (Q).ToString()
                            End If
                            r = r + 1
                            ReDim Preserve strPrice(r)
                        End If
                    Next

                  
                Next
                ds33.Dispose()
                da33.Dispose()
            End If
        Next


        Q = 0
        GetImage = ""
        'For t As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
        '    Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(t).Cells(1).Value)
        '    If IsTicked Then
        '        Dim GetSrNo As String = GvTechnicalSYS3.Rows(t).Cells(2).Value.ToString()
        '        GetImage += GetSrNo + ","
        '    End If
        'Next

        selectSrNo = GetImage.TrimEnd(",")
        For t As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
            'str12 = "select Photo1,Photo2,Photo3,Price,SNo from Category_Master where SNo IN(" & GvTechnicalSYS3.Rows(t).Cells(2).Value.ToString() & ") and Capacity='" + txtCapacity1.Text.Trim() + "' AND MainCategory ='System 2' and LanguageId = " & LanguageId & "  ORDER BY SNo"

            'da33 = New SqlDataAdapter(str12, con1)
            'ds33 = New DataSet
            'da33.Fill(ds33)
            Dim IsTicked As Boolean = CBool(GvTechnicalSYS3.Rows(t).Cells(1).Value)
            If IsTicked Then
                str12 = "select * from Category_Master (NOLOCK) where SNo IN(" & GvTechnicalSYS3.Rows(t).Cells(2).Value.ToString() & ") AND MainCategory ='System 3' and LanguageId = " & LanguageId & "  ORDER BY SNo"

                da33 = New SqlDataAdapter(str12, con1)
                ds33 = New DataSet
                da33.Fill(ds33)
                For x As Integer = 0 To ds33.Tables(0).Rows.Count - 1

                    Dim PhotoNo As String
                    For photocount As Integer = PhotoStartNo To PhotoEndNo
                        'Add multiple Photo 1 - 20 No     26-07-2016 Navin Goradara
                        PhotoNo = "Photo" & photocount
                        If ds33.Tables(0).Rows(x)(PhotoNo).ToString().Trim() <> "" Then
                            str(p) = ds33.Tables(0).Rows(x)(PhotoNo).ToString().Replace("D:", "\\192.168.1.102\")
                            p = p + 1
                            ReDim Preserve str(p)
                            Q = Q + 1
                            If RblMultiple.Checked = True Then
                                strPrice(r) = GvTechnicalSYS3.Rows(t).Cells("Price1").Value.ToString() + "|" + "SYS-3 3." + (Q).ToString()
                            Else
                                strPrice(r) = GvTechnicalSYS3.Rows(t).Cells("Price").Value.ToString() + "|" + "SYS-3 3." + (Q).ToString()
                            End If
                            r = r + 1
                            ReDim Preserve strPrice(r)
                        End If

                    Next

                  

                Next
            End If

        Next


        ds33.Dispose()
        da33.Dispose()
        Q = 0
        GetImage = ""

        'For t As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1
        '    Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(t).Cells(1).Value)
        '    If IsTicked Then
        '        Dim GetSrNo As String = GvTechnicalSYS4.Rows(t).Cells(2).Value.ToString()
        '        GetImage += GetSrNo + ","
        '    End If
        'Next

        'selectSrNo = GetImage.TrimEnd(",")
        'If selectSrNo.Length > 0 Then

        For t As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1

            Dim IsTicked As Boolean = CBool(GvTechnicalSYS4.Rows(t).Cells(1).Value)
            If IsTicked Then
                str12 = "select * from Category_Master (NOLOCK) where SNo IN(" & GvTechnicalSYS4.Rows(t).Cells(2).Value.ToString() & ") and Capacity='" + txtCapacity1.Text.Trim() + "' AND MainCategory ='System 4' and LanguageId = " & LanguageId & "  ORDER BY SNo"

                da33 = New SqlDataAdapter(str12, con1)
                ds33 = New DataSet
                da33.Fill(ds33)
                For x As Integer = 0 To ds33.Tables(0).Rows.Count - 1

                    Dim PhotoNo As String
                    For photocount As Integer = PhotoStartNo To PhotoEndNo
                        'Add multiple Photo 1 - 20 No     26-07-2016 Navin Goradara
                        PhotoNo = "Photo" & photocount
                        If ds33.Tables(0).Rows(x)(PhotoNo).ToString().Trim() <> "" Then
                            str(p) = ds33.Tables(0).Rows(x)(PhotoNo).ToString().Replace("D:", "\\192.168.1.102\")
                            p = p + 1
                            ReDim Preserve str(p)
                            Q = Q + 1
                            If RblMultiple.Checked = True Then
                                strPrice(r) = GvTechnicalSYS4.Rows(t).Cells("Price1").Value.ToString() + "|" + "SYS-4 4." + (Q).ToString()

                            Else
                                strPrice(r) = GvTechnicalSYS4.Rows(t).Cells("Price").Value.ToString() + "|" + "SYS-4 4." + (Q).ToString()

                            End If
                            r = r + 1
                            ReDim Preserve strPrice(r)
                        End If
                    Next

                Next
                ds33.Dispose()
                da33.Dispose()
            End If
        Next




        ReDim Preserve str(p - 1)
        Dim ikl As Integer
        Dim missing12 As Object = System.Reflection.Missing.Value
        objApp12 = New Word.Application



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
        rng12.Font.Size = 2

        Dim oTable2Foot As Word.Tables = objDoc12.Tables
        Dim missing1Foot As Object = System.Reflection.Missing.Value
        Dim rng2Foot As Word.Range = objDoc12.Range(start12, missing12)
        oTable2Foot.Add(rng2Foot, 1, 6, missing12, missing12)
        start12 = objDoc12.Tables(1).Range.[End]
        rng2Foot = objDoc12.Range(start12, missing12)

        Dim dt331 As DataTable
        dt331 = New DataTable

        If RblSingle.Checked = True Then
            dt331.Columns.Add("No")
            dt331.Columns.Add("Description")
            dt331.Columns.Add("Price")
            For t1 As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
                dt331.Rows.Add(GvTechnicalSYS1.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(4).Value.ToString())
            Next
        End If


        If RblOther.Checked = True Then
            dt331.Columns.Add("No")
            dt331.Columns.Add("Description")
            dt331.Columns.Add("Price")
            dt331.Columns.Add("Qty")
            dt331.Columns.Add("Total")
            For t1 As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
                dt331.Rows.Add(GvTechnicalSYS1.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(5).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(6).Value.ToString())
            Next
        End If



        If RblMultiple.Checked = True Then
            dt331.Columns.Add("No")
            dt331.Columns.Add("Description")
            dt331.Columns.Add("Price1")
            dt331.Columns.Add("Price2")
            For t1 As Integer = 0 To GvTechnicalSYS1.Rows.Count - 1
                dt331.Rows.Add(GvTechnicalSYS1.Rows(t1).Cells(2).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(3).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(4).Value.ToString(), GvTechnicalSYS1.Rows(t1).Cells(5).Value.ToString())
            Next
        End If
        If dt331 Is Nothing Then
        Else
            If dt331.Rows.Count > 0 Then
                Dim dView As New DataView(dt331)
                dView.Sort = "No ASC"
                dt331 = dView.ToTable()
            End If
        End If
        isa = isa - 1
        Dim cntfirstrow As Integer
        cntfirstrow = 0
        Dim totalsys1 As Decimal
        totalsys1 = 0

        '  objDoc12.Activate()
        '  objDoc12.SaveAs(appPath + "\" + Convert.ToString(0) + ".doc")


        For ikl = 0 To str.Length - 1
            'tbl12.Range.ParagraphFormat.SpaceAfter = 0
            'tbl12.Cell(1, 1).Range.InlineShapes.AddPicture(str(ikl))
            If str(ikl) <> "" Then
                ' objDoc12.Application.Selection.FitTextWidth = 550
                Dim newRowStatic1Foot1 As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                newRowStatic1Foot1.Cells(1).Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowStatic1Foot1.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                newRowStatic1Foot1.Cells(1).Width = 480
                'objDoc12.Application.Selection.InlineShapes.AddPicture(str(ikl)).Height = 550


                If ikl = isa Then
                    If RblMultiple.Checked = True Then
                        newRowStatic1Foot1.Cells(1).Range.InlineShapes.AddPicture(str(ikl)).Height = 420

                    Else
                        newRowStatic1Foot1.Cells(1).Range.InlineShapes.AddPicture(str(ikl)).Height = 450
                    End If
                    'newRowStatic1Foot1.Cells(1).Range.InlineShapes.AddPicture(str(ikl)).Height = 450
                    For ind = 0 To dt331.Rows.Count - 1

                        If cntfirstrow = 0 Then
                            If RblSingle.Checked = True Or RblMultiple.Checked = True Then

                                Dim newRowStatic1Foot As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                                newRowStatic1Foot.Range.Font.Size = 9
                                newRowStatic1Foot.Range.Font.Bold = True
                                newRowStatic1Foot.Range.Font.Name = "Arial"

                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                                newRowStatic1Foot.Cells(1).Range.Text = "SPECIAL FEATURES :"
                                newRowStatic1Foot.Cells(1).Range.Font.Size = 12
                                newRowStatic1Foot.Cells(1).Range.Font.Bold = True
                                newRowStatic1Foot.Cells(1).Width = 200
                                newRowStatic1Foot.Cells(2).Width = 45
                                newRowStatic1Foot.Cells(3).Width = 180
                                newRowStatic1Foot.Cells(4).Width = 55
                                newRowStatic1Foot.Range.ParagraphFormat.SpaceAfter = 0
                                newRowStatic1Foot.Cells(2).Range.Text = "NO"
                                newRowStatic1Foot.Cells(2).Range.Borders.Enable = 1
                                'newRowStatic1Foot2.Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle
                                newRowStatic1Foot.Cells(3).Range.Text = "DESCRIPTION"
                                newRowStatic1Foot.Cells(3).Range.Borders.Enable = 1

                                newRowStatic1Foot.Cells(4).Range.Text = "PRICE"
                                newRowStatic1Foot.Cells(4).Range.Borders.Enable = 1

                                'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                            End If
                            If RblOther.Checked = True Then
                                Dim newRowStatic1Foot As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                                newRowStatic1Foot.Range.Font.Size = 9
                                newRowStatic1Foot.Range.Font.Name = "Arial"
                                newRowStatic1Foot.Cells(1).Width = 200
                                newRowStatic1Foot.Cells(2).Width = 45
                                newRowStatic1Foot.Cells(3).Width = 145
                                newRowStatic1Foot.Cells(4).Width = 40
                                newRowStatic1Foot.Cells(5).Width = 30
                                newRowStatic1Foot.Cells(6).Width = 40
                                newRowStatic1Foot.Range.ParagraphFormat.SpaceAfter = 0
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                                newRowStatic1Foot.Cells(1).Range.Text = "SPECIAL FEATURES :"
                                newRowStatic1Foot.Range.Font.Bold = True
                                newRowStatic1Foot.Cells(1).Range.Font.Size = 12

                                newRowStatic1Foot.Cells(2).Range.Text = "NO"
                                newRowStatic1Foot.Cells(2).Range.Borders.Enable = 1

                                newRowStatic1Foot.Cells(3).Range.Text = "DESCRIPTION"
                                newRowStatic1Foot.Cells(3).Range.Borders.Enable = 1

                                newRowStatic1Foot.Cells(4).Range.Text = "PRICE"
                                newRowStatic1Foot.Cells(4).Range.Borders.Enable = 1

                                newRowStatic1Foot.Cells(5).Range.Text = "Qty"
                                newRowStatic1Foot.Cells(5).Range.Borders.Enable = 1

                                newRowStatic1Foot.Cells(6).Range.Text = "Total"
                                newRowStatic1Foot.Cells(6).Range.Borders.Enable = 1

                                'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                            End If
                            'If RblMultiple.Checked = True Then
                            '    Dim newRowStatic1Foot As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                            '    newRowStatic1Foot.Range.ParagraphFormat.SpaceAfter = 0
                            '    newRowStatic1Foot.Range.Font.Bold = True
                            '    newRowStatic1Foot.Range.Font.Size = 9
                            '    newRowStatic1Foot.Range.Font.Name = "Arial"
                            '    newRowStatic1Foot.Cells(1).Width = 180
                            '    newRowStatic1Foot.Cells(2).Width = 45
                            '    newRowStatic1Foot.Cells(3).Width = 160
                            '    newRowStatic1Foot.Cells(4).Width = 110
                            '    '      newRowStatic1Foot.Cells(5).Width = 55
                            '    newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                            '    newRowStatic1Foot.Cells(1).Range.Text = "SPECIAL FEATURES :"
                            '    newRowStatic1Foot.Cells(1).Range.Font.Bold = True
                            '    newRowStatic1Foot.Cells(1).Range.Font.Size = 12

                            '    newRowStatic1Foot.Cells(2).Range.Text = "NO"
                            '    newRowStatic1Foot.Cells(2).Range.Borders.Enable = 1

                            '    newRowStatic1Foot.Cells(3).Range.Text = "DESCRIPTION"
                            '    newRowStatic1Foot.Cells(3).Range.Borders.Enable = 1

                            '    newRowStatic1Foot.Cells(4).Range.Text = txtCapacity1.Text
                            '    newRowStatic1Foot.Cells(4).Range.Borders.Enable = 1

                            '    'newRowStatic1Foot.Cells(5).Range.Text = txtCapacity2.Text
                            '    'newRowStatic1Foot.Cells(5).Range.Borders.Enable = 1

                            '    'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                            '    newRowStatic1Foot.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                            '    newRowStatic1Foot.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                            '    newRowStatic1Foot.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                            '    newRowStatic1Foot.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                            'End If
                            cntfirstrow = 1
                        End If


                        If RblSingle.Checked = True Or RblMultiple.Checked = True Then
                            Dim newRowStatic1Foot As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                            newRowStatic1Foot.Range.ParagraphFormat.SpaceAfter = 1.5
                            newRowStatic1Foot.Range.Font.Name = "Arial"
                            newRowStatic1Foot.Range.Font.Size = 9
                            newRowStatic1Foot.Cells(1).Range.Font.Bold = True
                            newRowStatic1Foot.Cells(2).Range.Font.Bold = True

                            newRowStatic1Foot.Cells(3).Range.Borders.Enable = 1
                            newRowStatic1Foot.Cells(2).Range.Borders.Enable = 1
                            newRowStatic1Foot.Cells(1).Width = 200
                            newRowStatic1Foot.Cells(2).Width = 45
                            newRowStatic1Foot.Cells(3).Width = 180
                            newRowStatic1Foot.Cells(4).Width = 55
                            If ind = 0 Then

                                newRowStatic1Foot.Cells(1).Range.Text = "■ TOTAL SS PLANT"
                                newRowStatic1Foot.Cells(2).Range.Text = "1 to 9"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"

                            ElseIf ind = 1 Then

                                newRowStatic1Foot.Cells(1).Range.Text = "■ ISI APPROVED QUALITY"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"

                                newRowStatic1Foot.Cells(2).Range.Text = "A - 1"

                            ElseIf ind = 2 Then

                                newRowStatic1Foot.Cells(1).Range.Text = "■ HIGH GRADE SS316L STEEL"
                                newRowStatic1Foot.Cells(2).Range.Text = "A - 2"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"

                            ElseIf ind = 3 Then

                                newRowStatic1Foot.Cells(1).Range.Text = "■ UV SYSTEM - JAPANESE TECHNOLOGY"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                                newRowStatic1Foot.Cells(2).Range.Text = "A - 3"
                            ElseIf ind = 4 Then
                                newRowStatic1Foot.Cells(1).Width = 200
                                newRowStatic1Foot.Cells(2).Width = 225
                                newRowStatic1Foot.Cells(3).Width = 55
                                newRowStatic1Foot.Cells(4).Width = 0
                                newRowStatic1Foot.Cells(1).Range.Text = "■ WATER SYSTEM - EUROPEAN TECHNOLOGY"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                                newRowStatic1Foot.Cells(2).Range.Text = ""
                                newRowStatic1Foot.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                                newRowStatic1Foot.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                                newRowStatic1Foot.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                                newRowStatic1Foot.Cells(4).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                                newRowStatic1Foot.Cells(4).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                            End If

                            '  newRowStatic1Foot.Range.ParagraphFormat.SpaceAfter = 0

                            If ind = 4 Then
                                newRowStatic1Foot.Cells(3).Range.Font.Name = "Rupee"
                                newRowStatic1Foot.Cells(2).Range.Font.Bold = False
                                newRowStatic1Foot.Cells(3).Range.Font.Bold = False

                                newRowStatic1Foot.Cells(2).Range.Text = dt331.Rows(ind)(1).ToString()
                                newRowStatic1Foot.Cells(3).Range.Text = "` " + dt331.Rows(ind)(2).ToString()
                                totalsys1 = totalsys1 + Convert.ToDecimal(dt331.Rows(ind)(2).ToString())
                                'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                            Else

                                newRowStatic1Foot.Cells(4).Range.Font.Name = "Rupee"
                                newRowStatic1Foot.Cells(3).Range.Font.Bold = False
                                newRowStatic1Foot.Cells(4).Range.Font.Bold = False

                                newRowStatic1Foot.Cells(3).Range.Text = dt331.Rows(ind)(1).ToString()
                                newRowStatic1Foot.Cells(4).Range.Text = "` " + dt331.Rows(ind)(2).ToString()
                                totalsys1 = totalsys1 + Convert.ToDecimal(dt331.Rows(ind)(2).ToString())
                                'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                                ''''''''''''''''''''''''''''''''''''''''new row footer sys 1 ''''''''''''''''''''''''''''''''''''''''''''''''
                            End If



                        End If

                        'If RblMultiple.Checked = True Then
                        '    Dim newRowStatic1Foot As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                        '    newRowStatic1Foot.Range.ParagraphFormat.SpaceAfter = 1.5
                        '    newRowStatic1Foot.Range.Font.Name = "Arial"
                        '    newRowStatic1Foot.Cells(1).Range.Font.Bold = True
                        '    newRowStatic1Foot.Cells(2).Range.Font.Bold = True
                        '    newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"

                        '    newRowStatic1Foot.Cells(3).Range.Borders.Enable = 1
                        '    newRowStatic1Foot.Cells(2).Range.Borders.Enable = 1
                        '    newRowStatic1Foot.Cells(1).Width = 200
                        '    newRowStatic1Foot.Cells(2).Width = 45
                        '    newRowStatic1Foot.Cells(3).Width = 180
                        '    newRowStatic1Foot.Cells(4).Width = 55

                        '    If ind = 0 Then


                        '        newRowStatic1Foot.Cells(1).Range.Text = "■ TOTAL SS PLANT"
                        '        newRowStatic1Foot.Cells(2).Range.Text = "1 to 9"
                        '        newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                        '        newRowStatic1Foot.Cells(1).Range.Font.Size = 9

                        '    ElseIf ind = 1 Then


                        '        newRowStatic1Foot.Cells(1).Range.Text = "■ ISI APPROVED QUALITY"
                        '        newRowStatic1Foot.Cells(2).Range.Text = "A - 1"
                        '        newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                        '        newRowStatic1Foot.Cells(1).Range.Font.Size = 9
                        '    ElseIf ind = 2 Then


                        '        newRowStatic1Foot.Cells(1).Range.Text = "■ HIGH GRADE SS316L STEEL"
                        '        newRowStatic1Foot.Cells(2).Range.Text = "A - 2"
                        '        newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                        '        newRowStatic1Foot.Cells(1).Range.Font.Size = 9
                        '    ElseIf ind = 3 Then


                        '        newRowStatic1Foot.Cells(1).Range.Text = "■ UV SYSTEM - JAPANESE TECHNOLOGY"
                        '        newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                        '        newRowStatic1Foot.Cells(1).Range.Font.Size = 9
                        '        newRowStatic1Foot.Cells(2).Range.Text = "A - 3"
                        '    ElseIf ind = 4 Then
                        '        newRowStatic1Foot.Cells(1).Width = 200
                        '        newRowStatic1Foot.Cells(2).Width = 225
                        '        newRowStatic1Foot.Cells(3).Width = 55
                        '        newRowStatic1Foot.Cells(4).Width = 0
                        '        newRowStatic1Foot.Cells(1).Range.Text = "■ WATER SYSTEM - EUROPEAN TECHNOLOGY"
                        '        newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                        '        newRowStatic1Foot.Cells(2).Range.Text = ""
                        '        newRowStatic1Foot.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        '        newRowStatic1Foot.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                        '        newRowStatic1Foot.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        '        newRowStatic1Foot.Cells(4).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        '        newRowStatic1Foot.Cells(4).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone


                        '    End If
                        '    If ind = 4 Then
                        '        newRowStatic1Foot.Cells(3).Range.Font.Name = "Rupee"
                        '        newRowStatic1Foot.Cells(2).Range.Font.Bold = False
                        '        newRowStatic1Foot.Cells(3).Range.Font.Bold = False

                        '        newRowStatic1Foot.Cells(2).Range.Text = dt331.Rows(ind)(1).ToString()
                        '        newRowStatic1Foot.Cells(3).Range.Text = "` " + dt331.Rows(ind)(2).ToString()
                        '        totalsys1 = totalsys1 + Convert.ToDecimal(dt331.Rows(ind)(2).ToString())
                        '        'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                        '        newRowStatic1Foot.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        '        newRowStatic1Foot.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        '        newRowStatic1Foot.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                        '        newRowStatic1Foot.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                        '    Else

                        '        newRowStatic1Foot.Cells(4).Range.Font.Name = "Rupee"
                        '        newRowStatic1Foot.Cells(3).Range.Font.Bold = False
                        '        newRowStatic1Foot.Cells(4).Range.Font.Bold = False

                        '        newRowStatic1Foot.Cells(3).Range.Text = dt331.Rows(ind)(1).ToString()
                        '        newRowStatic1Foot.Cells(4).Range.Text = "` " + dt331.Rows(ind)(2).ToString()
                        '        totalsys1 = totalsys1 + Convert.ToDecimal(dt331.Rows(ind)(2).ToString())
                        '        'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                        '        newRowStatic1Foot.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        '        newRowStatic1Foot.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                        '        newRowStatic1Foot.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                        '        newRowStatic1Foot.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                        '        ''''''''''''''''''''''''''''''''''''''''new row footer sys 1 ''''''''''''''''''''''''''''''''''''''''''''''''
                        '    End If
                        'End If
                        If RblOther.Checked = True Then
                            Dim newRowStatic1Foot As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                            newRowStatic1Foot.Range.Font.Name = "Arial"
                            newRowStatic1Foot.Range.Font.Size = 9
                            newRowStatic1Foot.Cells(3).Range.Borders.Enable = 1
                            newRowStatic1Foot.Cells(2).Range.Borders.Enable = 1
                            newRowStatic1Foot.Cells(1).Range.Font.Bold = True
                            newRowStatic1Foot.Cells(2).Range.Font.Bold = True

                            newRowStatic1Foot.Cells(1).Width = 200
                            newRowStatic1Foot.Cells(2).Width = 45
                            newRowStatic1Foot.Cells(3).Width = 145
                            newRowStatic1Foot.Cells(4).Width = 40
                            newRowStatic1Foot.Cells(5).Width = 30
                            newRowStatic1Foot.Cells(6).Width = 40
                            If ind = 0 Then

                                newRowStatic1Foot.Cells(1).Range.Text = "■ TOTAL SS PLANT"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                                newRowStatic1Foot.Cells(1).Range.Font.Size = 9
                                newRowStatic1Foot.Cells(2).Range.Text = "1 to 9"
                            ElseIf ind = 1 Then

                                newRowStatic1Foot.Cells(1).Range.Text = "■ ISI APPROVED QUALITY"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                                newRowStatic1Foot.Cells(1).Range.Font.Size = 9

                                newRowStatic1Foot.Cells(2).Range.Text = "A - 1"
                            ElseIf ind = 2 Then
                                newRowStatic1Foot.Cells(1).Range.Text = "■ HIGH GRADE SS316L STEEL"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                                newRowStatic1Foot.Cells(1).Range.Font.Size = 9
                                newRowStatic1Foot.Cells(2).Range.Text = "A - 2"
                            ElseIf ind = 3 Then
                                newRowStatic1Foot.Cells(1).Range.Text = "■ UV SYSTEM - JAPANESE TECHNOLOGY"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                                newRowStatic1Foot.Cells(1).Range.Font.Size = 9
                                newRowStatic1Foot.Cells(2).Range.Text = "A - 3"
                            ElseIf ind = 4 Then
                                newRowStatic1Foot.Cells(1).Width = 200
                                newRowStatic1Foot.Cells(2).Width = 190
                                newRowStatic1Foot.Cells(3).Width = 40
                                newRowStatic1Foot.Cells(4).Width = 30
                                newRowStatic1Foot.Cells(1).Range.Text = "■ WATER SYSTEM - EUROPEAN TECHNOLOGY"
                                newRowStatic1Foot.Cells(1).Range.Font.Name = "Times New Roman"
                                newRowStatic1Foot.Cells(2).Range.Text = ""
                                newRowStatic1Foot.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                                newRowStatic1Foot.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                                newRowStatic1Foot.Cells(6).Borders.Enable = 0
                                newRowStatic1Foot.Cells(4).Borders.Enable = 1
                                newRowStatic1Foot.Cells(5).Borders.Enable = 1



                            End If

                            newRowStatic1Foot.Cells(4).Range.Font.Name = "Rupee"
                            newRowStatic1Foot.Cells(6).Range.Font.Name = "Rupee"

                            If ind = 4 Then

                                newRowStatic1Foot.Cells(2).Range.Font.Bold = False
                                newRowStatic1Foot.Cells(3).Range.Font.Bold = False
                                newRowStatic1Foot.Cells(5).Range.Font.Bold = False


                                newRowStatic1Foot.Cells(5).Width = 40
                                newRowStatic1Foot.Cells(3).Range.Font.Name = "Rupee"
                                newRowStatic1Foot.Cells(5).Range.Font.Name = "Rupee"

                                newRowStatic1Foot.Cells(2).Range.Text = dt331.Rows(ind)(1).ToString()
                                newRowStatic1Foot.Cells(3).Range.Text = "`" + dt331.Rows(ind)(2).ToString()
                                newRowStatic1Foot.Cells(4).Range.Text = dt331.Rows(ind)(3).ToString()
                                newRowStatic1Foot.Cells(5).Range.Text = "`" + dt331.Rows(ind)(4).ToString()
                                totalsys1 = totalsys1 + Convert.ToDecimal(dt331.Rows(ind)(2).ToString())
                                'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                            Else
                                '''' one cell shifted to left

                                newRowStatic1Foot.Cells(3).Range.Text = dt331.Rows(ind)(1).ToString()
                                newRowStatic1Foot.Cells(4).Range.Text = "`" + dt331.Rows(ind)(2).ToString()
                                newRowStatic1Foot.Cells(5).Range.Text = dt331.Rows(ind)(3).ToString()
                                newRowStatic1Foot.Cells(6).Range.Text = "`" + dt331.Rows(ind)(4).ToString()
                                newRowStatic1Foot.Cells(3).Range.Font.Bold = False
                                newRowStatic1Foot.Cells(4).Range.Font.Bold = False
                                newRowStatic1Foot.Cells(5).Range.Font.Bold = False
                                newRowStatic1Foot.Cells(6).Range.Font.Bold = False
                                totalsys1 = totalsys1 + Convert.ToDecimal(dt331.Rows(ind)(4).ToString())
                                'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                                newRowStatic1Foot.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(5).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                                newRowStatic1Foot.Cells(6).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                            End If

                        End If

                    Next


                    Dim newRowStatic1 As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                    newRowStatic1.Range.Font.Size = 9
                    newRowStatic1.Range.Font.Bold = True
                    newRowStatic1.Range.Borders.Enable = 0
                    newRowStatic1.Cells(1).Range.Font.Name = "Times New Roman"
                    newRowStatic1.Cells(1).Range.Text = "■ MEMBRANE FROM USA"
                    newRowStatic1.Cells(1).Width = 200
                    newRowStatic1.Cells(2).Width = 280
                    If RblOther.Checked Then
                        newRowStatic1.Cells(2).Width = 45
                        newRowStatic1.Cells(3).Width = 165
                        newRowStatic1.Cells(4).Width = 30
                        newRowStatic1.Cells(5).Width = 30
                        newRowStatic1.Cells(6).Width = 30

                        newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


                        newRowStatic1.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(3).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        newRowStatic1.Cells(3).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


                        newRowStatic1.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(4).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        newRowStatic1.Cells(4).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

                        newRowStatic1.Cells(5).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(5).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        newRowStatic1.Cells(5).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(5).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

                        newRowStatic1.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(6).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        newRowStatic1.Cells(6).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        newRowStatic1.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    End If
                    'ElseIf RblMultiple.Checked Then

                    '    newRowStatic1.Cells(1).Width = 180
                    '    newRowStatic1.Cells(2).Width = 45
                    '    newRowStatic1.Cells(3).Width = 160
                    '    newRowStatic1.Cells(4).Width = 55
                    '    newRowStatic1.Cells(5).Width = 55

                    '    newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    '    newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


                    '    newRowStatic1.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(3).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    '    newRowStatic1.Cells(3).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


                    '    newRowStatic1.Cells(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(4).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    '    newRowStatic1.Cells(4).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(4).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

                    '    newRowStatic1.Cells(5).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(5).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    '    newRowStatic1.Cells(5).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(5).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone

                    '    newRowStatic1.Cells(6).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(6).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(6).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    '    newRowStatic1.Cells(6).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


                    'End If





                    newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowStatic1.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone


                    'newRowStatic1Foot.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRowStatic1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRowStatic1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    newRowStatic1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRowStatic1.Cells(4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter


                    '''''old 

                    Dim newRowStatic1Foot2 As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                    newRowStatic1Foot2.Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle
                    newRowStatic1Foot2.Range.ParagraphFormat.SpaceAfter = 0
                    newRowStatic1Foot2.Range.Borders.Enable = 0
                    newRowStatic1Foot2.Range.Font.Name = "Arial"
                    newRowStatic1Foot2.Cells(2).Shading.BackgroundPatternColor = Word.WdColor.wdColorBlack
                    newRowStatic1Foot2.Cells(4).Shading.BackgroundPatternColor = Word.WdColor.wdColorBlack
                    newRowStatic1Foot2.Cells(5).Shading.BackgroundPatternColor = Word.WdColor.wdColorBlack
                    newRowStatic1Foot2.Cells(6).Shading.BackgroundPatternColor = Word.WdColor.wdColorBlack

                    newRowStatic1Foot2.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowStatic1Foot2.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowStatic1Foot2.Cells(5).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowStatic1Foot2.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite


                    newRowStatic1Foot2.Range.Font.Bold = 1
                    newRowStatic1Foot2.Range.Font.Bold = True

                    newRowStatic1Foot2.Cells(1).Range.Font.Size = 18
                    newRowStatic1Foot2.Cells(2).Range.Font.Size = 18
                    newRowStatic1Foot2.Cells(3).Range.Font.Size = 18
                    newRowStatic1Foot2.Cells(4).Range.Font.Size = 18

                    newRowStatic1Foot2.Cells(6).Range.Font.Size = 18


                    'Dim strsplited
                    'strsplited = strPrice(ikl).ToString.Split("|")


                    newRowStatic1Foot2.Cells(2).Range.Text = "SYS-1"
                    newRowStatic1Foot2.Cells(4).Range.Text = "COST:"
                    newRowStatic1Foot2.Cells(5).Range.Font.Name = "Rupee"
                    newRowStatic1Foot2.Cells(5).Range.Text = "`"
                    newRowStatic1Foot2.Cells(6).Range.Text = Convert.ToString(totalsys1) + " Lacs"
                    newRowStatic1Foot2.Cells(2).Range.Font.Bold = True 'Indian' 
                    newRowStatic1Foot2.Cells(5).Range.Font.Size = 18
                    newRowStatic1Foot2.Cells(1).Width = 160
                    newRowStatic1Foot2.Cells(2).Width = 90
                    newRowStatic1Foot2.Cells(3).Width = 30
                    newRowStatic1Foot2.Cells(4).Width = 70
                    newRowStatic1Foot2.Cells(5).Width = 15
                    newRowStatic1Foot2.Cells(6).Width = 115

                Else
                    If ikl <> 0 Then

                        newRowStatic1Foot1.Cells(1).Width = 480
                        'newRowStatic1Foot1.Cells(1).Height = 560
                        If ikl > isa Then

                            Dim newRowStatic1Foot As Word.Row = objDoc12.Tables(1).Rows.Add(Type.Missing)
                            newRowStatic1Foot.Cells(1).Width = 150
                            newRowStatic1Foot.Cells(2).Width = 100
                            newRowStatic1Foot.Cells(3).Width = 15
                            newRowStatic1Foot.Cells(4).Width = 70
                            newRowStatic1Foot.Cells(5).Width = 15
                            newRowStatic1Foot.Cells(6).Width = 110
                            newRowStatic1Foot.Range.Font.Size = 9
                            newRowStatic1Foot.Range.Borders.Enable = 0
                            newRowStatic1Foot.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                            newRowStatic1Foot.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                            newRowStatic1Foot.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                            newRowStatic1Foot.Cells(4).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(4).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(4).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                            newRowStatic1Foot.Cells(5).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(5).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(5).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(5).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                            newRowStatic1Foot.Cells(6).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(6).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(6).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                            newRowStatic1Foot.Cells(6).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone


                            newRowStatic1Foot.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite

                            newRowStatic1Foot = objDoc12.Tables(1).Rows.Add(Type.Missing)
                            newRowStatic1Foot.Range.Borders.Enable = 0
                            newRowStatic1Foot.Range.Font.Name = "Arial"
                            newRowStatic1Foot.Cells(2).Shading.BackgroundPatternColor = Word.WdColor.wdColorBlack
                            newRowStatic1Foot.Cells(4).Shading.BackgroundPatternColor = Word.WdColor.wdColorBlack
                            newRowStatic1Foot.Cells(5).Shading.BackgroundPatternColor = Word.WdColor.wdColorBlack
                            newRowStatic1Foot.Cells(6).Shading.BackgroundPatternColor = Word.WdColor.wdColorBlack

                            newRowStatic1Foot.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
                            newRowStatic1Foot.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
                            newRowStatic1Foot.Cells(5).Range.Font.Color = Word.WdColor.wdColorWhite
                            newRowStatic1Foot.Cells(6).Range.Font.Color = Word.WdColor.wdColorWhite


                            newRowStatic1Foot.Range.Font.Bold = 1
                            newRowStatic1Foot.Range.Font.Bold = True
                            newRowStatic1Foot.Range.Font.Size = 18
                            Dim strsplited


                            strsplited = strPrice(ikl).ToString.Split("|")


                            newRowStatic1Foot.Cells(2).Range.Text = strsplited(1)
                            newRowStatic1Foot.Cells(4).Range.Text = "COST:"
                            newRowStatic1Foot.Cells(5).Range.Text = "`"
                            newRowStatic1Foot.Cells(5).Range.Font.Name = "Rupee"
                            newRowStatic1Foot.Cells(6).Range.Text = strsplited(0) + " Lacs"
                            newRowStatic1Foot.Cells(2).Range.Font.Bold = True 'Indian' 
                            newRowStatic1Foot1.Cells(1).Range.InlineShapes.AddPicture(str(ikl)).Height = 590

                        Else

                            newRowStatic1Foot1.Cells(1).Range.InlineShapes.AddPicture(str(ikl)).Height = 630
                        End If
                    Else
                        newRowStatic1Foot1.Cells(1).Range.InlineShapes.AddPicture(str(ikl)).Height = 630
                    End If

                End If

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
        'objDoc12.Tables(1)..Select()
        'With objApp12.Selection.Font.Size = 2
        'End With

        objDoc12.SaveAs(QtempPath + "\" + Convert.ToString(0) + ".doc")
        Dim Targets12 As Object = QtempPath + "\" + Convert.ToString("step3") + ".pdf"
        objDoc12.SaveAs(Targets12, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)
        objDoc12.Close()
        objDoc12 = Nothing

        objApp12.Quit()
        objApp12 = Nothing

        'Dim exportFormat12 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        'Dim paramMissing12 As Object = Type.Missing


        'Dim paramSourceDocPath12 As Object = appPath + "\" + Convert.ToString(0) + ".doc"

        ''wordDocument1 = wordApplication1.Documents.Open(paramSourceDocPath1)
        ''objDoc12 = objApp12.Documents.Open(paramSourceDocPath12)
        'wordDocument = New Word.Document
        'wordApplication = New Word.Application
        'wordDocument = wordApplication.Documents.Open(paramSourceDocPath12)

        'Dim formating12 As Object
        'formating12 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        'wordDocument.SaveAs(Targets12, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)

        ' tbl12 = Nothing
        'wordDocument.Close()
        'wordDocument = Nothing
        ' wordApplication.Quit()  wordApplication.NormalTemplate.Saved = True
        'wordApplication = Nothing
        PriceSheet()
        OrderWNBPriceSheet()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim missing11 As Object = System.Reflection.Missing.Value

        Dim objApp11 As New Word.Application
        Dim objDoc11 As Word.Document = objApp11.Documents.Add(missing11, missing11, missing11, missing11)

        Dim start21 As Object = 0
        Dim end21 As Object = 0

        'objApp1 = New Word.Application
        'objDoc1 = New Word.Document
        Dim oTable21 As Word.Tables = objDoc11.Tables
        Dim rng21 As Word.Range = objDoc11.Range(start21, missing11)
        oTable21.Add(rng21, 1, 5, missing11, missing11)
        start21 = objDoc11.Tables(1).Range.[End]
        rng21 = objDoc11.Range(start21, missing11)





        Dim newRow5 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
        'newRow5.Cells(1).Shading.BackgroundPatternColor = RGB(0, 102, 153)
        newRow5.Shading.BackgroundPatternColor = RGB(256, 256, 256)
        If RblSingle.Checked = True Then

            'newRow5.Cells(1).Range.Text = "નિયમો અને શરતો"
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
            newRow5.Cells(1).Range.Font.Name = "Times New Roman"

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
            newRow5.Cells(1).Range.Font.Name = "Times New Roman"

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
            newRow5.Cells(1).Range.Font.Name = "Times New Roman"

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



        Dim strt2 As Object = objDoc11.Tables(1).Range.[End]
        Dim oCollapseEnd2 As Object = Word.WdCollapseDirection.wdCollapseEnd

        Dim ran2 As Word.Range = objDoc11.Range(strt2, strt2)
        rng = objDoc11.Content
        rng.Collapse(oCollapseEnd)
        '  Dim oTable2 As Word.Table = objDoc.Tables.Add(ran, 5, 5, missing, missing)
        Dim oTable4 As Word.Table = objDoc11.Tables.Add(ran2, 1, 1, missing, missing)
        '    newRow4.HeadingFormat = 3
        objDoc11.Tables(1).Range.ParagraphFormat.SpaceAfter = 3.5
        If DocumentStatus = 0 Then
            For Each section As Word.Section In objDoc11.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
        Else

            For Each section As Word.Section In objDoc11.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
        End If




        If txt1.Text.Trim() <> "" Then
            Dim newRow6 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
            newRow6.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
            newRow6.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
            newRow6.Cells(1).Range.Text = txt1.Text
            newRow6.Cells(1).Range.Font.Name = "Times New Roman"
            newRow6.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow6.Cells(1).Range.Font.Size = 12
            newRow6.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow6.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow6.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
            newRow6.Range.ListFormat.ListOutdent()
        End If
        If txt2.Text.Trim() <> "" Then
            Dim newRow7 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
            newRow7.Cells(1).Range.Text = txt2.Text
            newRow7.Cells(1).Range.Font.Name = "Times New Roman"
            newRow7.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow7.Cells(1).Range.Font.Size = 12
            newRow7.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow7.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow7.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
        End If


        If txt3.Text.Trim() <> "" Then
            Dim newRow8 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
            newRow8.Cells(1).Range.Text = txt3.Text
            newRow8.Cells(1).Range.Font.Name = "Times New Roman"
            newRow8.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow8.Cells(1).Range.Font.Size = 12
            newRow8.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow8.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow8.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
            '  newRow8.Range.ListFormat.ListIndent()
        End If

        If txt4.Text.Trim() <> "" Then
            Dim newRow9 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
            newRow9.Cells(1).Range.Text = txt4.Text
            newRow9.Cells(1).Range.Font.Name = "Times New Roman"
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

            Dim newRow99 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
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
                Dim newRow81 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow81.Cells(1).Range.Font.Name = "Times New Roman"
                newRow81.Cells(1).Range.Text = txt41.Text
                newRow81.Cells(1).Range.Font.Bold = True 'Indian' 
                newRow81.Cells(1).Range.Font.Size = 12
                newRow81.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                newRow81.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow81.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)

            End If

            If txt42.Text.Trim() <> "" Then
                Dim newRow82 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow82.Cells(1).Range.Font.Name = "Times New Roman"
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

                Dim newRow10 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow10.Cells(1).Range.Font.Name = "Times New Roman"
                newRow10.Cells(1).Range.Text = "         -- " + txt41.Text
                newRow10.Range.Font.Bold = 0
                newRow10.Cells(1).Range.Font.Size = 12
                newRow10.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow10.Range.ListFormat.ListOutdent()
                newRow10.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow10.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
            End If

            If txt42.Text.Trim() <> "" Then
                Dim newRow101 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow101.Cells(1).Range.Text = ".       -- " + txt42.Text
                newRow101.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow101.Range.Font.Bold = 0
                newRow101.Cells(1).Range.Font.Size = 12
                newRow101.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow101.SetLeftIndent(1.2, Word.WdRulerStyle.wdAdjustFirstColumn)
                newRow101.Range.ListFormat.ApplyBulletDefault(Nothing)
            End If


            If txt5.Text.Trim() <> "" Then

                Dim newRow11 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow11.Cells(1).Range.Text = txt5.Text
                newRow11.Cells(1).Range.Font.Name = "Times New Roman"

                newRow11.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                'newRow11.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow11.Cells(1).Range.Font.Bold = True 'Indian' 
                newRow11.Cells(1).Range.Font.Size = 12
                newRow11.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                newRow11.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow11.Range.ListFormat.ListOutdent()
            End If


            If txt51.Text.Trim() <> "" Then
                Dim newRow12 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow12.Cells(1).Range.Text = "        -- " + txt51.Text
                newRow12.Cells(1).Range.Font.Name = "Times New Roman"
                newRow12.Range.Font.Bold = 0

                newRow12.Cells(1).Range.Font.Size = 12
                newRow12.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow12.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If

            If txt52.Text.Trim() <> "" Then
                Dim newRow13 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow13.Cells(1).Range.Font.Name = "Times New Roman"
                newRow13.Cells(1).Range.Text = ".       --" + txt52.Text
                newRow13.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                'newRow13.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
                newRow13.Range.Font.Bold = 0
                newRow13.Cells(1).Range.Font.Size = 12
                newRow13.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRow13.Range.ListFormat.ApplyBulletDefault(Nothing)
            End If


            If txt6.Text.Trim() <> "" Then
                Dim newRow14 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow14.Cells(1).Range.Font.Name = "Times New Roman"
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
                Dim newRow15 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow15.Cells(1).Range.Font.Name = "Times New Roman"
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

                Dim newRow161 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow161.Cells(1).Range.Font.Name = "Times New Roman"
                newRow161.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
                newRow161.Range.ListFormat.ListOutdent()
                newRow161.Cells(1).Range.Text = txt7.Text
                newRow161.Cells(1).Range.Font.Size = 12
                newRow161.Range.Font.Bold = True
                newRow161.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            End If

            If txt71.Text.Trim() <> "" Then
                Dim newRow171 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
                newRow171.Cells(1).Range.Font.Name = "Times New Roman"
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


            Dim newRow88 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
            newRow88.Cells(1).Range.Font.Name = "Times New Roman"
            newRow88.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
            newRow88.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
            newRow88.Cells(1).Range.Text = txt9.Text
            newRow88.Cells(1).Range.Font.Bold = True 'Indian' 
            newRow88.Cells(1).Range.Font.Size = 12
            newRow88.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
            newRow88.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRow88.Range.ListFormat.ListOutdent()



            Dim newRow881 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
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





        Dim newRow16 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
        newRow16.Cells(1).Range.Font.Name = "Times New Roman"
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
        'newRow16.Cells(1).Range.Text = "( NB : Guarantee clause is valid for Mfg. Defect/Workmanship Defect only,"
        newRow16.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow16.Cells(1).Range.Font.Size = 12
        newRow16.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow16.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow16.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
        newRow16.Range.ListFormat.ListOutdent()




        Dim newRow17 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
        newRow17.Cells(1).Range.Font.Name = "Times New Roman"
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




        Dim newRow18 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
        newRow18.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior)
        newRow18.Cells(1).Range.Font.Name = "Times New Roman"



        If (rdbTerms1.Checked = True Or rdbExport.Checked = True) Then
            If RdbEnglish.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            ElseIf RdbGujarati.Checked Then
                newRow18.Cells(1).Range.Text = "ઓફર મર્યાદા: 30 દિવસ"
            ElseIf RdbHindi.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            ElseIf RdbMarathi.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            ElseIf RdbTamil.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            ElseIf RdbTelugu.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            End If
            'newRow18.Cells(1).Range.Text = "Offer Validity : 30 Days"
        Else
            If RdbEnglish.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            ElseIf RdbGujarati.Checked Then
                newRow18.Cells(1).Range.Text = "ઓફર મર્યાદા: 30 દિવસ"
            ElseIf RdbHindi.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            ElseIf RdbMarathi.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            ElseIf RdbTamil.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            ElseIf RdbTelugu.Checked Then
                newRow18.Cells(1).Range.Text = txt8.Text
            End If
            ' newRow18.Cells(1).Range.Text = "Offer Validity : 1 Week"
        End If

        newRow18.Cells(1).Range.Font.Bold = True 'Indian' 
        newRow18.Cells(1).Range.Font.Size = 12
        newRow18.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
        newRow18.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRow18.SetLeftIndent(0.0, Word.WdRulerStyle.wdAdjustNone)
        newRow18.Range.ListFormat.ListOutdent()
        If (rdbTerms2.Checked = True) Then
            Dim strt5 As Object = objDoc11.Tables(1).Range.[End]
            Dim oCollapseEnd5 As Object = Word.WdCollapseDirection.wdCollapseEnd
            Dim ran5 As Word.Range = objDoc11.Range(strt5, strt5)
            rng = objDoc11.Content
            rng.Collapse(oCollapseEnd)
            Dim oTable519 As Word.Table = objDoc11.Tables.Add(ran5, 1, 4, missing, missing)

        End If


        Dim strt4 As Object = objDoc11.Tables(1).Range.[End]
        Dim oCollapseEnd4 As Object = Word.WdCollapseDirection.wdCollapseEnd
        Dim ran4 As Word.Range = objDoc11.Range(strt4, strt4)
        rng = objDoc11.Content
        rng.Collapse(oCollapseEnd)


        Dim oTable511 As Word.Table = objDoc11.Tables.Add(ran4, 1, 4, missing, missing)

        oTable511.Range.ParagraphFormat.SpaceAfter = 0

        Dim Rowa12 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
        Rowa12.Cells(1).Range.Text = "For, INDIAN ION EXCHANGE"
        Rowa12.Cells(1).Range.Font.Name = "Times New Roman"
        Rowa12.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        Rowa12.Range.Font.Size = 12
        Rowa12.Cells(1).Width = 250
        Rowa12.Cells(2).Width = 30
        Rowa12.Cells(4).Width = 50
        Rowa12.Cells(3).Width = 50
        Rowa12.Range.Font.Bold = True

        Dim Rowa23 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
        Rowa23.Cells(2).Range.Font.Name = "Times New Roman"
        Rowa23.Cells(2).Range.Text = "   & CHEMICALS LTD."
        Rowa23.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        Rowa23.Range.Font.Bold = True
        Rowa23.Cells(1).Width = 30
        Rowa23.Cells(2).Width = 200
        Rowa23.Cells(4).Width = 50
        Rowa23.Cells(3).Width = 200
        Rowa23.Range.Font.Size = 12

        Dim newRow2211 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
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

        Dim newRow2212 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
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

        Dim newRow122 As Word.Row = objDoc11.Tables(1).Rows.Add(Type.Missing)
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

        If RdbEnglish.Checked Then

        End If
        'Dim app As Word.Application = DirectCast(System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application"), Word.Application)

        'If app Is Nothing Then
        '    Return
        'End If

        'For Each d As Word.Document In app.Documents
        '    If d.FullName.ToLower() = (QtempPath + "Letter3.doc").ToLower() Then
        '        Dim saveOption As Object = Word.WdSaveOptions.wdDoNotSaveChanges
        '        Dim originalFormat As Object = Word.WdOriginalFormat.wdOriginalDocumentFormat
        '        Dim routeDocument As Object = False
        '        d.Close(saveOption, originalFormat, routeDocument)
        '        Return
        '    End If
        'Next
        'Return

        ' wordApplication.Documents("NewDocument.doc").Close(Word.WdSaveOptions.wdDoNotSaveChanges)
        objDoc11.SaveAs(QtempPath + "\Letter3.doc")

        objDoc11.SaveAs(QtempPath + "\Letter3.pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)

        objDoc11.Close()
        objDoc11 = Nothing
        objApp11.NormalTemplate.Saved = True
        objApp11 = Nothing
        Dim exportFormat As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing As Object = Type.Missing

        wordDocument = New Word.Document
        wordApplication = New Word.Application

        Dim paramSourceDocPath As Object = QtempPath + "\Letter3.doc"
        Dim Targets As Object = QtempPath + "\Letter3.pdf"

        wordDocument = wordApplication.Documents.Open(paramSourceDocPath)

        Dim formating As Object
        formating = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        '  wordDocument.SaveAs(Targets, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)

        wordDocument = New Word.Document
        wordApplication = New Word.Application



        paramSourceDocPath = QtempPath + "\Letter2.doc"
        Targets = QtempPath + "\Letter2.pdf"

        wordDocument = wordApplication.Documents.Open(paramSourceDocPath)

        formating = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        wordDocument.SaveAs(Targets, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)




        wordDocument.Close()
        wordDocument = Nothing
        wordApplication.NormalTemplate.Saved = True
        wordApplication.Quit()
        wordApplication = Nothing



        Dim cnt As Integer
        Dim flag As Integer
        If txtspdiscount.Text.Trim() = "" And txtVat.Text.Trim() = "" And txtISI.Text.Trim() = "" And txtTransporation.Text.Trim() = "" And txtInsurance.Text.Trim() = "" And txtPakingforwarding.Text.Trim() = "" And txtErection.Text.Trim() = "" Then
            flag = 1
        Else
            Specialpricesheet()
        End If

        If (Not System.IO.Directory.Exists(QPath + "\ISI")) Then
            System.IO.Directory.CreateDirectory(QPath + "\ISI")
        End If

        Dim str3333 As String

        If Not IsNothing(str) Then
            str = Nothing

            If flag = 1 Then
                cnt = 4
            Else
                cnt = 5
            End If
            Dim files(cnt) As String



            'Added by Dipak Patel
            'file1 = Today.Day.ToString + Today.Month.ToString + Today.Year.ToString + Today.Hour.ToString + Today.Minute.ToString + Today.Millisecond.ToString + "1"
            'file2 = Today.Day.ToString + Today.Month.ToString + Today.Year.ToString + Today.Hour.ToString + Today.Minute.ToString + Today.Millisecond.ToString + "2"
            'file3 = Today.Day.ToString + Today.Month.ToString + Today.Year.ToString + Today.Hour.ToString + Today.Minute.ToString + Today.Millisecond.ToString + "3"

            'files(0) = QtempPath + "\" + file1 + ".pdf"
            'files(1) = QtempPath + "\index.pdf"
            'files(2) = QtempPath + "\" + Convert.ToString("step3") + ".pdf"
            'files(3) = QtempPath + "\" + file2 + ".pdf"
            'files(4) = QtempPath + "\" + file3 + ".pdf"
            'End by Dipak

            files(0) = QtempPath + "\Letter1.pdf"
            files(1) = QtempPath + "\index.pdf"
            files(2) = QtempPath + "\" + Convert.ToString("step3") + ".pdf"
            files(3) = QtempPath + "\Letter2.pdf"
            files(4) = QtempPath + "\Letter3.pdf"
            If (flag <> 1) Then
                files(5) = QtempPath + "\SpecialPriceSheet.pdf"
            End If

            Dim fullpath12 As String
            ' fullpath12 = QPath + "\" + txtQoutNo.Text.Trim() + "-" + txtName.Text.Trim() + "-" + Convert.ToDateTime(txtDate.Text.Trim()).Day.ToString() + "-" + Convert.ToDateTime(txtDate.Text.Trim()).Month.ToString() + "-" + Convert.ToDateTime(txtDate.Text.Trim()).Year.ToString() + ".pdf"

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

            fullpath12 = QPath + "\ISI\" + txtEnqNo.Text.Trim().Replace("/", "-") + "-" + Convert.ToString(EnqMax) + "-" + txtName.Text.Trim().Replace("/", "-") + "-" + str3333 + " - ISI" + ".pdf"

            _pdfforge.MergePDFFiles(files, fullpath12, False)
            'added by Rajesh


            For index = 1 To files.Length



            Next




            'Class1.killProcessOnUser()
        Else
            str = Nothing
            If flag = 1 Then
                cnt = 4
            Else
                cnt = 5
            End If
            Dim files(cnt) As String



            'added by rajesh 
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
            ''added by Dipak Patel
            'files(0) = QtempPath + "\" + file1 + ".pdf"
            'files(1) = QtempPath + "\index.pdf"
            'files(2) = QtempPath + "\" + Convert.ToString("step3") + ".pdf"
            'files(3) = QtempPath + "\" + file2 + ".pdf"
            'files(4) = QtempPath + "\" + file3 + ".pdf"
            ''End by Dipak Patel         



            files(0) = QtempPath + "\Letter1.pdf"
            files(1) = QtempPath + "\index.pdf"
            files(2) = QtempPath + "\" + Convert.ToString("step3") + ".pdf"
            files(3) = QtempPath + "\Letter2.pdf"
            files(4) = QtempPath + "\Letter3.pdf"
            If (flag <> 1) Then
                files(5) = QtempPath + "\SpecialPriceSheet.pdf"
            End If

            Dim fullpath12 As String
            fullpath12 = QPath + "\ISI\" + txtEnqNo.Text.Trim().Replace("/", "-") + "-" + Convert.ToString(EnqMax) + "-" + txtName.Text.Trim().Replace("/", "-") + "-" + str3333 + " - ISI" + ".pdf"
            _pdfforge.MergePDFFiles(files, fullpath12, False)
            '  Class1.killProcessOnUser()
        End If

        MessageBox.Show("Document Ready !")

        'MessageBox.Show(QPath + "\ISI\" + txtEnqNo.Text.Trim().Replace("/", "-") + "-" + Convert.ToString(EnqMax) + "-" + txtName.Text.Trim().Replace("/", "-") + "-" + str3333 + " - ISI.pdf")

        System.Diagnostics.Process.Start(QPath + "\ISI\" + txtEnqNo.Text.Trim().Replace("/", "-") + "-" + Convert.ToString(EnqMax) + "-" + txtName.Text.Trim().Replace("/", "-") + "-" + str3333 + " - ISI.pdf")


        Try
            ' Class1.killProcessOnUser()
        Catch ex As Exception

        End Try
        If Not IsNothing(objApp12) Then
            objApp12.NormalTemplate.Saved = True
            objApp12.Quit()
        End If
        If Not IsNothing(objDoc12) Then
            objDoc12.Close()
        End If
        'If Not IsNothing(objApp1) Then
        '    objApp1.Quit()
        'End If
        'If Not IsNothing(objDoc1) Then
        '    objDoc1.Close()
        'End If

        If Not IsNothing(wordDocument) Then
            wordDocument.Close()
        End If
        If Not IsNothing(wordApplication) Then
            wordDocument.Close()
        End If

    End Sub

    Protected Sub Finalcalcultation(ByVal objDoc1 As Word.Document, ByVal finaltotal As Decimal, ByVal finaltotal1 As Decimal)



    End Sub
    Public Sub bindQuatData()

    End Sub
    Private Sub GvCategorySearch_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvCategorySearch.CellDoubleClick

    End Sub

    Public Sub GvCategorySearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCategorySearch.DoubleClick

        chkDetailQuotation.Checked = False
        GroupQuotationStatus.Visible = False

        Try
            If rblOld.Checked = True Then
                'Old ISI Quotation

                If (Class1.global.QuatationId <> 0) Then
                    QuationId = Class1.global.QuatationId
                    btnAddClear.Enabled = True
                    btnAddClear.Text = "View"
                    btnCancel.Enabled = False
                Else
                    btnAddClear.Enabled = True
                    btnAddClear.Text = "Add New"
                    btnCancel.Enabled = True
                    QuationId = Convert.ToInt32(Me.GvCategorySearch.SelectedCells(0).Value)
                End If
                con1.Close()
                btnSave1.Text = "Update"

                If txtCapacity1.Text <> "" Then
                    txtCapacity1_Leave(sender, e)
                End If
                If txtCapacity2.Text <> "" Then
                    txtCapacity2_Leave(sender, e)
                End If

                Display()
                Total1()
            Else

                'Pending Quotation
                btnCancel_Click(Nothing, Nothing)
                GroupQuotationStatus.Visible = True
                Fk_SalesExecutiveQtnID = Convert.ToInt64(Me.GvCategorySearch.SelectedCells(0).Value)
                DisplaySalesExecutive_Bind()
                Total1()
            End If


        Catch ex As Exception

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
            txtAddress.Text = item.City + "," + item.DeliveryState
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

            If item.QuotationType = "ISI" Then
                txtSub.Text = "REQUIREMENT FOR MINERAL WATER PROJECT -" & item.QuotationType
            Else
                txtSub.Text = "REQUIREMENT FOR REVERSE OSMOSIS PLANT -" & item.QuotationType
            End If
            ddlBussines_Executive_SelectionChangeCommitted(Nothing, Nothing)

            Dim flagstatus As Integer
            If item.CapacityType = 1 Then
                flagstatus = 1
                '  RblSingle.Checked = True
                txtCapacity1.Text = item.Capacity1
                RblSingle.Checked = True
                'txtCapacity1_Leave(Nothing, Nothing)

                Dim totalcapacity As Int64
                totalcapacity = Convert.ToInt32(txtCapacity1.Text) * 20
                txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity.ToString() + " LITER/DAY").ToString()


                If item.System1 = "YES" Then
                    GvSingle_Bind("System 1")
                End If
                If item.System2 = "YES" Then
                    GvSingle_Bind("System 2")
                End If
                'technical data Sales Executive
                Gv_Single__System3_System_4_SalesExecuvtive_Bind("System 3", Fk_SalesExecutiveQtnID)
                Gv_Single__System3_System_4_SalesExecuvtive_Bind("System 4", Fk_SalesExecutiveQtnID)


            Else

                flagstatus = 3
                '  RblMultiple.Checked = True
                RblMultiple.Checked = True
                txtCapacity1.Text = item.Capacity1
                txtCapacity2.Text = item.Capacity2

                Dim totalcapacity1 As Int64
                Dim totalcapacity2 As Int64
                totalcapacity1 = Convert.ToInt64(txtCapacity1.Text) * 20
                totalcapacity2 = Convert.ToInt64(txtCapacity2.Text) * 20
                Dim Newline As String
                Newline = System.Environment.NewLine
                txtCapacityWord.Text = ("CAPACITY: " + txtCapacity1.Text + "  LITER/HR . . . . " + totalcapacity1.ToString() + " LITER/DAY" & Newline & "CAPACITY: " + txtCapacity2.Text + "  LITER/HR . . . . " + totalcapacity2.ToString() + " LITER/DAY  ").ToString()

                'txtCapacity2_Leave(Nothing, Nothing)


                GvMultiple_Bind("System 1")
                GvMultiple_Bind("System 2")
                GvMultiple_System3_System4_SalesExecutive_Bind("System 3", Fk_SalesExecutiveQtnID)
                GvMultiple_System3_System4_SalesExecutive_Bind("System 4", Fk_SalesExecutiveQtnID)



            End If
            txtEnqNo.Enabled = False

            'technical data 



            Exit For
        Next
        'Load Autocomplated Text
        GetTechnicalData(1)


        'txtNoContent.Enabled = False
        txtCapacity1.Enabled = False
        txtCapacity2.Enabled = False
        GroupBox2.Enabled = False



    End Sub
    Public Sub Display()
        Try
            con1.Close()
        Catch ex As Exception

        End Try

        Dim str As String
        Try
            con1.Open()


            str = "select * from Quotation_Master (NOLOCK) where Pk_QuotationID=" & QuationId & ""
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            dr.Read()


            txtEnqNo.Enabled = False

            txtQoutType.Text = dr("Quot_Type").ToString()
            txtName.Text = dr("Name").ToString()
            txtAddress.Text = dr("Address").ToString()
            txtBussness_Exe.Text = dr("Buss_Excecutive").ToString()
            txtType.Text = dr("QType").ToString()
            txtQoutNo.Text = dr("Fk_EnqTypeID").ToString() + dr("Quot_No").ToString()


            txtEnqNo.Text = dr("Enq_No").ToString()

            'Added by rajesh
            'get a name for a file name.
            EnqMax = Class1.getTotalNo(dr("Ref"))
            txtRef.Text = dr("Ref").ToString()
            '     EnqMax = Convert.ToInt16(dr("Quot_No"))



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

                ' RblSingle.Checked = True
                flagstatus = 1
                txtCapacity1.Text = dr("Capacity_Single").ToString()
            End If
            If dr("Capacity_Type").ToString() = "Other" Then

                flagstatus = 2
                txtCapacity1.Text = dr("Capacity_Single").ToString()
                'RblOther.Checked = True
            End If

            If dr("Capacity_Type").ToString() = "Multiple" Then

                flagstatus = 3
                txtCapacity1.Text = dr("Capacity_Single").ToString()
                txtCapacity2.Text = dr("Capacity_Multiple").ToString()
                ' RblMultiple.Checked = True

            End If

            txtKind.Text = dr("KindAtt").ToString()
            txtSub.Text = dr("Subject").ToString()
            ddlBussines_Executive.SelectedItem = dr("Buss_Excecutive").ToString()
            txtBuss_Name.Text = dr("Buss_Name").ToString()
            txtDescription.Text = dr("Later_Description").ToString()
            txtLatterDate.Text = dr("Later_Date").ToString()
            txtCapacityWord.Text = dr("Capacity_Word").ToString()
            'txtUserName.Text = dr("UserName").ToString()
            PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
            ' PicDefault.ImageLocation = dr("DefaultImage").ToString()
            PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
            txtNoContentSYS1.Enabled = False
            txtNoContentSYS2.Enabled = False
            txtNoContentSYS3.Enabled = False
            txtNoContentSYS4.Enabled = False
            txtCapacity1.Enabled = False
            txtCapacity2.Enabled = False


            Dim ip As Integer
            ip = Convert.ToInt32(dr("LanguageId"))
            dr.Dispose()
            cmd.Dispose()
            GetTechnicalData(ip)
            TxtTax.Visible = False
            lbltxttax.Visible = False
            Gv_GetTechnicalData("System 1")
            Gv_GetTechnicalData("System 2")
            Gv_GetTechnicalData("System 3")
            Gv_GetTechnicalData("System 4")
            Total1()
            str = "select * from Discount_master (NOLOCK) where Fk_QuotationID=" & QuationId & ""
            'added by rajesh error execute reader.

            If (con1.State = ConnectionState.Closed) Then
                con1.Open()
            End If
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            txtspdiscount.Text = dr("SpecialDisc").ToString()
            txtVat.Text = dr("Vat").ToString()
            txtErection.Text = dr("Erection").ToString()
            txtInsurance.Text = dr("Insurance").ToString()
            txtFinalPrice.Text = dr("FinalTotal").ToString()
            txtISI.Text = dr("ISIFee").ToString()
            txtTransporation.Text = dr("Transportation").ToString()
            txtPakingforwarding.Text = dr("Packing").ToString()
            cmd.Dispose()
            dr.Dispose()
            con1.Close()

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
    Public Sub Gv_GetTechnicalData(ByVal strVal As String)
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
            dt12.Columns.Add("Tax", GetType(String))

            If strVal = "System 3" Then
                dt12.Columns.Add("Capacity", GetType(String))

            End If
            str1 = "select  SNo,TechnicalData,Price1,DocumentationImage,Tax,Capacity from Technical_Data (NOLOCK) where Fk_QuotationID=" & QuationId & " and MainCategory ='" + strVal + "'"
            da123 = New SqlDataAdapter(str1, con1)
            ds123 = New DataSet()
            da123.Fill(ds123)

            For S1 = 0 To ds123.Tables(0).Rows.Count - 1

                Dim imagestatus As Int16
                imagestatus = 0
                If ds123.Tables(0).Rows(S1)("DocumentationImage") = "Yes" Then
                    imagestatus = 1
                End If
                If strVal = "System 3" Then
                    dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString(), ds123.Tables(0).Rows(S1)("Tax").ToString(), ds123.Tables(0).Rows(S1)("Capacity").ToString())
                    'Gv_GetTechnicalData("System 1")
                Else
                    dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString(), ds123.Tables(0).Rows(S1)("Tax").ToString())
                End If
            Next
            If dt12 Is Nothing Then
            Else
                If dt12.Rows.Count > 0 Then
                    Dim dView As New DataView(dt12)
                    dView.Sort = "SrNo ASC"
                    dt12 = dView.ToTable()
                End If
            End If
            ' added by a sr no in systems grid total

            If strVal = "System 1" Then

                GvTechnicalSYS1.DataSource = dt12
                'lblSnoSys1.Text = (GvTechnicalSYS1.Rows.Count + 1).ToString()
                'Gv_GetTechnicalData("System 1")
            ElseIf strVal = "System 2" Then
                GvTechnicalSYS2.DataSource = dt12
                ' lblsNosys2.Text = (GvTechnicalSYS2.Rows.Count + 1).ToString()
                'Gv_GetTechnicalData("System 2")
            ElseIf strVal = "System 3" Then
                GvTechnicalSYS3.DataSource = dt12
                ' lblsNosys3.Text = (GvTechnicalSYS3.Rows.Count + 1).ToString()
                'Gv_GetTechnicalData("System 3")
            ElseIf strVal = "System 4" Then
                GvTechnicalSYS4.DataSource = dt12
                ' lblSrNOSys4.Text = (GvTechnicalSYS4.Rows.Count + 1).ToString()
                'Gv_GetTechnicalData("System 4")

            End If
            ''GvTechnical.ReadOnly = True
            '  btnAddSys1.Visible = False
            ' btnSaveSys2.Visible = False
            ' btnSaveSys3.Visible = False
            ' btnSaveSys4.Visible = False
            RblSingle.Enabled = False
            RblOther.Enabled = False
            RblMultiple.Enabled = False
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
            dt12.Columns.Add("Tax", GetType(String))

            If strVal = "System 3" Then
                dt12.Columns.Add("Capacity", GetType(String))

            End If
            str1 = "select  SNo,TechnicalData,Price1,Qty,Price2,Tax,DocumentationImage,Capacity from Technical_Data (NOLOCK) where Fk_QuotationID=" & QuationId & " and MainCategory ='" + strVal + "'"
            da123 = New SqlDataAdapter(str1, con1)
            ds123 = New DataSet()
            da123.Fill(ds123)
            For S1 = 0 To ds123.Tables(0).Rows.Count - 1

                Dim imagestatus As Int16
                imagestatus = 0
                If ds123.Tables(0).Rows(S1)("DocumentationImage") = "Yes" Then
                    imagestatus = 1
                End If
                If strVal = "System 3" Then
                    dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString(), ds123.Tables(0).Rows(S1)("Qty").ToString(), ds123.Tables(0).Rows(S1)("Price2").ToString(), ds123.Tables(0).Rows(S1)("Tax").ToString(), ds123.Tables(0).Rows(S1)("Capacity").ToString())
                    'Gv_GetTechnicalData("System 1")
                Else
                    dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString(), ds123.Tables(0).Rows(S1)("Qty").ToString(), ds123.Tables(0).Rows(S1)("Price2").ToString(), ds123.Tables(0).Rows(S1)("Tax").ToString())
                End If

                ' dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString(), ds123.Tables(0).Rows(S1)("Qty").ToString(), ds123.Tables(0).Rows(S1)("Price2").ToString())

            Next
            If dt12 Is Nothing Then
            Else
                If dt12.Rows.Count > 0 Then

                    Dim dView As New DataView(dt12)
                    dView.Sort = "SrNo ASC"
                    dt12 = dView.ToTable()
                End If
            End If
            If strVal = "System 1" Then
                GvTechnicalSYS1.DataSource = dt12
                'Gv_GetTechnicalData("System 1")
            ElseIf strVal = "System 2" Then
                GvTechnicalSYS2.DataSource = dt12
                'Gv_GetTechnicalData("System 2")
            ElseIf strVal = "System 3" Then
                GvTechnicalSYS3.DataSource = dt12
                'Gv_GetTechnicalData("System 3")
            ElseIf strVal = "System 4" Then
                GvTechnicalSYS4.DataSource = dt12
                'Gv_GetTechnicalData("System 4")

            End If
          
            RblSingle.Enabled = False
            RblOther.Enabled = False
            RblMultiple.Enabled = False
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
            dt12.Columns.Add("Tax", GetType(String))
            If strVal = "System 3" Then
                dt12.Columns.Add("Capacity", GetType(String))
            End If
            str1 = "select  SNo,TechnicalData,Price1,Price2,Tax,DocumentationImage,Capacity from Technical_Data (NOLOCK) where Fk_QuotationID=" & QuationId & " and MainCategory ='" + strVal + "'"
            da123 = New SqlDataAdapter(str1, con1)
            ds123 = New DataSet()
            da123.Fill(ds123)
            For S1 = 0 To ds123.Tables(0).Rows.Count - 1

                Dim imagestatus As Int16
                imagestatus = 0
                If ds123.Tables(0).Rows(S1)("DocumentationImage") = "Yes" Then
                    imagestatus = 1
                End If
                If strVal = "System 3" Then
                    dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString(), ds123.Tables(0).Rows(S1)("Price2").ToString(), ds123.Tables(0).Rows(S1)("Tax").ToString(), ds123.Tables(0).Rows(S1)("Capacity").ToString())
                Else
                    dt12.Rows.Add(0, imagestatus, ds123.Tables(0).Rows(S1)("SNo").ToString(), ds123.Tables(0).Rows(S1)("TechnicalData").ToString(), ds123.Tables(0).Rows(S1)("Price1").ToString(), ds123.Tables(0).Rows(S1)("Price2").ToString(), ds123.Tables(0).Rows(S1)("Tax").ToString())
                End If


            Next
            If dt12 Is Nothing Then
            Else
                If dt12.Rows.Count > 0 Then

                    Dim dView As New DataView(dt12)
                    dView.Sort = "SrNo ASC"
                    dt12 = dView.ToTable()
                End If
            End If

            If strVal = "System 1" Then
                GvTechnicalSYS1.DataSource = dt12
                'Gv_GetTechnicalData("System 1")
            ElseIf strVal = "System 2" Then
                GvTechnicalSYS2.DataSource = dt12
                'Gv_GetTechnicalData("System 2")
            ElseIf strVal = "System 3" Then
                GvTechnicalSYS3.DataSource = dt12
                'Gv_GetTechnicalData("System 3")
            ElseIf strVal = "System 4" Then
                GvTechnicalSYS4.DataSource = dt12
                'Gv_GetTechnicalData("System 4")

            End If

            RblSingle.Enabled = False
            RblOther.Enabled = False
            RblMultiple.Enabled = False
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

                Try
                    con1.Close()
                Catch ex As Exception

                End Try

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
            Catch ex As Exception

            End Try

            For index = 1 To 4 Step 1
                If index = 1 Then
                    Gv_GetTechnicalData("System 1")
                ElseIf index = 2 Then
                    Gv_GetTechnicalData("System 2")
                ElseIf index = 3 Then
                    Gv_GetTechnicalData("System 3")
                ElseIf index = 4 Then
                    Gv_GetTechnicalData("System 4")

                End If
                'Gv_GetTechnicalData()
            Next
            con1.Close()
        End If
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
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim str As String
        If txtSearchName.Text <> "" And txtSearchEnQ.Text = "" Then
            ''            str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master where Name='" + txtSearchName.Text + "'  "
            str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master (NOLOCK) where Quatation_Type = 'ISI' and Name like '%" + txtSearchName.Text + "%' "
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)

            bindSearchGrid()
            da.Dispose()
            ds.Dispose()
        ElseIf txtSearchEnQ.Text <> "" And txtSearchName.Text = "" Then
            ''str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master where Enq_No='" + txtEnqNo.Text + "'  "
            str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master (NOLOCK) where Quatation_Type = 'ISI' and Enq_No like '%" + txtSearchEnQ.Text + "%' "
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)
            bindSearchGrid()

            da.Dispose()
            ds.Dispose()
        ElseIf txtSearchEnQ.Text <> "" And txtSearchName.Text <> "" Then
            str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master (NOLOCK) where Quatation_Type = 'ISI' and Enq_No like '%" + txtSearchEnQ.Text + "%' or Name='" + txtSearchName.Text + "'  "
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)
            bindSearchGrid()
            da.Dispose()
            ds.Dispose()
        End If


        ''      Dim str As String
        ''  str = "select Pk_QuotationID as ID, Enq_No,Later_Date as QDate ,Name,Capacity_Single as Capacity from Quotation_Master where   Enq_No like '%" + txtSearchEnQ.Text + "%' or Name='" + txtSearchName.Text + "'  "

    End Sub

    Public Sub DifferentLanguageThankyou()
        If ddlBussines_Executive.SelectedItem = "TELEPHONIC" Then
            Dim desc As String

            If RdbEnglish.Checked Then

                ''Added By Rajesh
                'For Change Text of quatation by type.
                If txtQoutType.Text.ToUpper() = "ISI" Or txtQoutType.Text.ToUpper() = "NON ISI" Then
                    LanguageId = 1
                    desc = "Thank you for your telephonic discussion with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " for Mineral Water Plant  regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
                    txtDescription.Text = desc.ToString()
                    Class1.global.LanguageId = LanguageId
                Else
                    LanguageId = 1
                    desc = "Thank you for your telephonic discussion with our " + txtBussness_Exe.Text + " " + txtBuss_Name.Text + " for " + txtQoutType.Text + " regards to subject matter on " + txtLatterDate.Text + ". Your sparing valued time for the discussion and showing interest in range of our products are sincerely appreciated."
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


    Private Sub ddlBussines_Executive_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBussines_Executive.SelectionChangeCommitted

        DifferentLanguageThankyou()

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
        BindOnLanguageName()
    End Sub

    '

    Public Shared Function GetProcessInfoByPID(ByVal PID As Integer)
        Dim User As String = [String].Empty
        Dim Domain As String = [String].Empty
        Dim OwnerSID As String = [String].Empty
        Dim processname As String = [String].Empty
        Try
            'Dim sq As New ObjectQuery("Select * from Win32_Process  Where Name LIKE 'WINWORD%' and ProcessID = " + Convert.ToString(PID))
            'Dim sq As New ObjectQuery("Select * from Win32_Process")
            Dim sq As New ObjectQuery("Select * from Win64_Process  Where Name LIKE 'WINWORD%' and ProcessID = " + Convert.ToString(PID)) ' change 30-12-2014

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
                    strPdfInsert = "Update  PDFGenerate_Check  Set IsCreated = 'Yes' where FK_QuatationID =" & QMaxId & ""
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
                strPdfInsert = "Update  PDFGenerate_Check  Set IsCreated = 'Yes' where FK_QuatationID =" & QuationId & ""
                cmd = New SqlCommand(strPdfInsert, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con1.Close()
                Class1.global.GobalMaxId = PQMaxId
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

        Dim newRowa2 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)

        newRowa2.Range.Borders.Enable = 0
        newRowa2.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
        newRowa2.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
        newRowa2.Cells(3).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)


        newRowa2.Cells(2).Range.Text = "FINAL PROJECT COST OF"
        newRowa2.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
        newRowa2.Range.Font.Size = 22
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Range.ParagraphFormat.SpaceAfter = 0
        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30

        newRowa2.Range.Font.Bold = True
        newRowa2.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
        newRowa2.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
        newRowa2.Cells(3).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)


        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Cells(2).Range.Text = "MINERAL WATER PROJECT (PRICE IN LACS)"
        newRowa2.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
        newRowa2.Range.Font.Size = 22
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30


        newRowa2.Range.Font.Bold = True

        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Font.Color = Word.WdColor.wdColorBlack

        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30

        newRowa2.Range.Borders.Enable = 0
        newRowa2.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
        newRowa2.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
        newRowa2.Cells(2).Range.Text = "PRICE  : MINERAL WATER PLANT COMPLETE WITH DESIGN, ENGINEERING, & SUPPLY"
        newRowa2.Range.ParagraphFormat.SpaceAfter = 7.0
        newRowa2.Range.ParagraphFormat.SpaceBefore = 7.0
        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


        newRowa2.Range.Font.Size = 9


        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        Dim FinalSysAllTotal As Decimal
        FinalSysAllTotal = 0

        Dim dt33 As DataTable
        dt33 = New DataTable

        Dim dtSys2 As DataTable
        dtSys2 = New DataTable

        Dim dtSys3 As DataTable
        dtSys3 = New DataTable

        Dim dtSys4 As DataTable
        dtSys4 = New DataTable

        Dim price As Decimal
        Dim price1 As Decimal
        Dim qty1 As Decimal


        price = 0
        price1 = 0
        qty1 = 0
        If RblSingle.Checked = True Or RblOther.Checked = True Then
            dt33.Columns.Add("No")
            dt33.Columns.Add("Description")
            dt33.Columns.Add("Price")
            dt33.Rows.Add("SR. NO", "DESCRIPTION", txtCapacity1.Text.Trim())
            dt33.Rows.Add("01", "System – 1 – Water Treatment System With Ozone System, 2 Tank and SS", sys1total)
            dt33.Rows.Add("01", "Piping (For Filling Section)", sys1total)
            dt33.Rows.Add("01", "(Item No : 01-05)", sys1total)
            dt33.Rows.Add("02", "System – 2 – Quality Control Laboratory (Microbiology & Chemical)", sys2total)
            dt33.Rows.Add("02", "(Item No : 06)", sys2total)
            dt33.Rows.Add("03", "System – 3 – Packing Machineries", sys3total)
            dt33.Rows.Add("03", "(Item NO : )", sys3total)
            dt33.Rows.Add("04", "System – 4 – Value Added Technology", sys4total)
            dt33.Rows.Add("04", "Item No.", sys4total)

        ElseIf RblMultiple.Checked = True Then

            dtSys2.Columns.Add("No")
            dtSys2.Columns.Add("Description")
            dtSys2.Columns.Add("Price1")
            dtSys2.Columns.Add("Price2")
            dtSys2.Rows.Add("SR. NO", "DESCRIPTION", txtCapacity1.Text.Trim(), txtCapacity2.Text.Trim())
            dtSys2.Rows.Add("01", "System – 1 – Water Treatment System With Ozone System, 2 Tank and SS", sys1total, sys1mutotal)
            dtSys2.Rows.Add("01", "Piping (For Filling Section)", sys1total, sys1mutotal)
            dtSys2.Rows.Add("01", "(Item No : 01-05)", sys1total, sys1mutotal)
            dtSys2.Rows.Add("02", "System – 2 – Quality Control Laboratory (Microbiology & Chemical)", sys2total, sys2mutotal)
            dtSys2.Rows.Add("02", "(Item No : 06)", sys1total, sys1mutotal)
            dtSys2.Rows.Add("03", "System – 3 – Packing Machineries", sys3total, sys3mutotal)
            dtSys2.Rows.Add("03", "(Item NO : )", sys3total, sys3mutotal)
            dtSys2.Rows.Add("04", "System – 4 – Value Added Technology", sys4total, sys4mutotal)
            dtSys2.Rows.Add("04", "Item No.", sys4total, sys4mutotal)
        End If

        If RblSingle.Checked = True Or RblOther.Checked = True Then


            For i = 0 To dt33.Rows.Count - 1
                Dim newRowa21 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.Borders.Enable = 0
                If i = 0 Then
                    newRowa21.Cells(3).Range.Text = "Price"
                    newRowa21.Cells(1).Range.Text = dt33.Rows(i)("No").ToString()
                    newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                    newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                    newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                    newRowa21.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowa21.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowa21.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                    newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                Else
                    newRowa21.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRowa21.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRowa21.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRowa21.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                End If
                If i = 1 Or i = 4 Or i = 6 Or i = 8 Then
                    newRowa21.Cells(1).Range.Text = dt33.Rows(i)("No").ToString()
                    newRowa21.Cells(3).Range.Text = "` " + dt33.Rows(i)("Price").ToString()
                    newRowa21.Range.ParagraphFormat.SpaceAfter = 4.5
                    newRowa21.Range.ParagraphFormat.SpaceBefore = 4.5
                End If

                If i = 2 Or i = 3 Or i = 5 Or i = 7 Or i = 9 Then
                    newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                    newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                End If
                newRowa21.Cells(2).Range.Text = dt33.Rows(i)("Description").ToString()


                newRowa21.Range.Font.Size = 11
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.Font.Name = "Calibri"
                newRowa21.Cells(1).Range.Font.Name = "Calibri"

                newRowa21.Cells(2).Width = 375
                newRowa21.Cells(1).Width = 45
                newRowa21.Cells(3).Width = 60



            Next

        ElseIf RblMultiple.Checked = True Then

            For i = 0 To dtSys2.Rows.Count - 1
                Dim newRowa21 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.Borders.Enable = 0
                If i = 0 Then
                    newRowa21.Cells(3).Range.Text = "Price1"
                    newRowa21.Cells(4).Range.Text = "Price2"
                    newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                    newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                    newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                    newRowa21.Cells(4).Range.Shading.BackgroundPatternColor = RGB(0, 0, 128)
                    newRowa21.Cells(2).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowa21.Cells(1).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowa21.Cells(3).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowa21.Cells(4).Range.Font.Color = Word.WdColor.wdColorWhite
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowa21.Cells(4).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowa21.Cells(4).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle


                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                    newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0

                Else
                    newRowa21.Cells(2).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRowa21.Cells(1).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRowa21.Cells(3).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRowa21.Cells(4).Range.Font.Color = Word.WdColor.wdColorBlack
                    newRowa21.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(4).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa21.Cells(4).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                End If
                If i = 1 Or i = 4 Or i = 6 Or i = 8 Then
                    newRowa21.Cells(1).Range.Text = dtSys2.Rows(i)("No").ToString()
                    newRowa21.Cells(3).Range.Text = "` " + dtSys2.Rows(i)("Price1").ToString()
                    newRowa21.Cells(4).Range.Text = "` " + dtSys2.Rows(i)("Price2").ToString()
                    newRowa21.Range.ParagraphFormat.SpaceAfter = 4.5
                    newRowa21.Range.ParagraphFormat.SpaceBefore = 4.5
                End If

                If i = 2 Or i = 3 Or i = 5 Or i = 7 Or i = 9 Then
                    newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                    newRowa21.Range.ParagraphFormat.SpaceBefore = 0
                    newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                    newRowa21.Cells(4).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone

                End If

                'newRowa21.Cells(1).Range.Text = dtSys2.Rows(i)("No").ToString()
                newRowa21.Cells(2).Range.Text = dtSys2.Rows(i)("Description").ToString()


                newRowa21.Range.Font.Size = 10
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(4).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.Font.Name = "Calibri"
                newRowa21.Cells(1).Range.Font.Name = "Calibri"

                newRowa21.Cells(2).Width = 320
                newRowa21.Cells(1).Width = 45
                newRowa21.Cells(4).Width = 57
                newRowa21.Cells(3).Width = 58

            Next
        End If

        FinalSysAllTotal = sys1total + sys2total + sys3total + sys4total
        Dim finalsysmutotal As Decimal
        finalsysmutotal = sys1mutotal + sys2mutotal + sys3mutotal + sys4mutotal
        If RblSingle.Checked = True Or RblOther.Checked = True Then

            Dim newRowa21 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
            newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

            newRowa21.Range.ParagraphFormat.SpaceAfter = 0
            newRowa21.Range.ParagraphFormat.SpaceBefore = 0

            newRowa21.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PROJECT "
            newRowa21.Range.Borders.Enable = 0
            newRowa21.Range.Font.Size = 9
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            newRowa21.Cells(1).Width = 420
            newRowa21.Cells(2).Range.Text = "` " + Convert.ToString(FinalSysAllTotal)
            newRowa21.Cells(2).Range.Font.Name = "Rupee"
            newRowa21.Cells(2).Width = 60

            newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa21.Cells(1).Range.Text = "(System 1+2+3+4)"
            newRowa21.Range.Borders.Enable = 0
            newRowa21.Range.Font.Size = 9
            newRowa21.Range.ParagraphFormat.SpaceAfter = 0
            newRowa21.Range.ParagraphFormat.SpaceBefore = 0
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle



            newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            newRowa21.Cells(1).Width = 420
            newRowa21.Cells(2).Width = 60
            Dim specialTotal As Decimal
            specialTotal = 0
            specialTotal = FinalSysAllTotal
            If txtspdiscount.Text <> "" Then


                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle




                newRowa21.Cells(1).Range.Text = "SPECIAL DISCOUNT = = > >"

                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = txtspdiscount.Text

                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


                ''with discount
                Dim afterdisc As Decimal
                afterdisc = 0
                afterdisc = FinalSysAllTotal - Convert.ToDecimal(txtspdiscount.Text)
                specialTotal = afterdisc
                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = Convert.ToString(afterdisc)
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            End If





            ''with vat tax
            If txtVat.Text <> "" Then

                Dim vattotal As Decimal
                Dim vatPerc As Decimal

                ''                vattotal = (FinalSysAllTotal) * Convert.ToDecimal(txtVat.Text)
                ''              vattotal = vattotal / 100
                vattotal = Convert.ToDecimal(txtVat.Text)
                Try
                    vatPerc = (vattotal * 100) / FinalSysAllTotal
                Catch ex As Exception
                    vatPerc = 0
                End Try

                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                ' newRowa21.Cells(1).Range.Text = "+ GST (" + vatPerc.ToString("N2") + ") = = > >"
                newRowa21.Cells(1).Range.Text = "+ GST = = > >"

                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + vattotal.ToString("N2")
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                specialTotal = specialTotal + vattotal

            End If

            If txtISI.Text <> "" Then
                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Text = "+ ISI FEES (AS PER GOVT. RULES & REGULATION)  = = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtISI.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                specialTotal = specialTotal + Convert.ToDecimal(txtISI.Text)
            End If

            If txtTransporation.Text <> "" Then
                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Text = "+ TRANSPORATION (APX.) = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtTransporation.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                specialTotal = specialTotal + Convert.ToDecimal(txtTransporation.Text)
            End If

            If txtInsurance.Text <> "" Then
                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Text = "+ INSURANCE  (APX.)  = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtInsurance.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                specialTotal = specialTotal + Convert.ToDecimal(txtInsurance.Text)

            End If


            If txtPakingforwarding.Text <> "" Then

                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Text = "+ PACKING & FORWADING CHARGES = = >>"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtPakingforwarding.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                specialTotal = specialTotal + Convert.ToDecimal(txtPakingforwarding.Text)

            End If
            If txtErection.Text <> "" Then
                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0

                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Text = "+ ERECTION & COMMISSIONING CHARGES = = >> "
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtErection.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                specialTotal = specialTotal + Convert.ToDecimal(txtErection.Text)

            End If
            txtFinalPrice.Text = specialTotal.ToString()

            If specialTotal <> 0 Then

                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0


                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Text = "FINAL COST OF ENTIRE MINERAL WATER PROJECT"


                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                newRowa21.Range.Font.Color = Word.WdColor.wdColorWhite
                newRowa21.Range.Font.Size = 14

                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)

                newRowa21.Cells(1).Range.Text = "(System 1+2+3+4)"


                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                newRowa21.Range.Font.Color = Word.WdColor.wdColorWhite
                newRowa21.Cells(2).Range.Font.Name = "Rupee"

                If RblPriceYes.Checked = True Then
                    newRowa21.Cells(2).Range.Text = "` " + txtFinalPrice.Text
                Else
                    newRowa21.Cells(2).Range.Text = ""
                End If
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If


        Else




            Dim newRowa21 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
            newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
            newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)


            newRowa21.Cells(1).Range.Text = "TOTAL PRICE FOR ENTIRE MINERAL WATER PROJECT"
            newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowa21.Cells(1).Width = 365
            newRowa21.Cells(2).Range.Font.Name = "Rupee"
            newRowa21.Cells(3).Range.Font.Name = "Rupee"
            newRowa21.Cells(2).Range.Text = "` " + Convert.ToString(FinalSysAllTotal)
            newRowa21.Cells(3).Range.Text = "` " + Convert.ToString(finalsysmutotal)
            newRowa21.Cells(2).Width = 57
            newRowa21.Cells(3).Width = 58
            newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
            newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

            newRowa21.Range.ParagraphFormat.SpaceAfter = 0
            newRowa21.Range.ParagraphFormat.SpaceBefore = 0
            newRowa21.Range.Borders.Enable = 0
            newRowa21.Range.Font.Size = 9
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone


            newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa21.Cells(1).Range.Text = "(System 1+2+3+4)"
            newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            newRowa21.Cells(1).Width = 365
            newRowa21.Cells(2).Range.Font.Name = "Rupee"
            newRowa21.Cells(3).Range.Font.Name = "Rupee"

            newRowa21.Cells(2).Width = 57
            newRowa21.Cells(3).Width = 58
            newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)
            newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(242, 221, 220)

            newRowa21.Range.ParagraphFormat.SpaceAfter = 0
            newRowa21.Range.ParagraphFormat.SpaceBefore = 0
            newRowa21.Range.Borders.Enable = 0
            newRowa21.Range.Font.Size = 9
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle




            Dim specialTotal As Decimal
            Dim specialmuTotal As Decimal

            specialTotal = 0
            specialTotal = FinalSysAllTotal
            If txtspdiscount.Text <> "" Then


                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                newRowa21.Cells(1).Range.Text = "SPECIAL DISCOUNT = = > >"
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = txtspdiscount.Text
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                newRowa21.Cells(3).Range.Text = txtspdiscount.Text
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                ''with discount
                Dim afterdisc As Decimal
                afterdisc = 0
                afterdisc = FinalSysAllTotal - Convert.ToDecimal(txtspdiscount.Text)
                specialTotal = afterdisc
                specialmuTotal = finalsysmutotal - Convert.ToDecimal(txtspdiscount.Text)

                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Text = "FINAL PROJECT COST OF ENTIRE MINERAL WATER PROJECT = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = Convert.ToString(afterdisc)
                afterdisc = finalsysmutotal - Convert.ToDecimal(txtspdiscount.Text)
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
                    vatPerc = (vattotal * 100) / FinalSysAllTotal
                Catch ex As Exception
                    vatPerc = 0
                End Try

                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
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

            If txtISI.Text <> "" Then
                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Text = "+ ISI FEES (AS PER GOVT. RULES & REGULATION)  = = = > >"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                newRowa21.Cells(2).Range.Text = "` " + txtISI.Text
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(3).Range.Text = "` " + txtISI.Text
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                specialTotal = specialTotal + Convert.ToDecimal(txtISI.Text)
                specialmuTotal = specialmuTotal + Convert.ToDecimal(txtISI.Text)
            End If

            If txtTransporation.Text <> "" Then
                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
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
                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
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

                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
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
                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Range.ParagraphFormat.SpaceAfter = 8.0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 8.0
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
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
            txtFinalPrice.Text = Convert.ToString(specialTotal + specialmuTotal)

            If specialTotal <> 0 Then

                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(3).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa21.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                newRowa21.Cells(1).Range.Text = "FINAL COST OF ENTIRE MINERAL WATER PROJECT"

                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                newRowa21.Range.Font.Color = Word.WdColor.wdColorWhite
                newRowa21.Range.Font.Size = 14
                newRowa21.Range.ParagraphFormat.SpaceAfter = 0
                newRowa21.Range.ParagraphFormat.SpaceBefore = 0


                newRowa21 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa21.Cells(1).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)
                newRowa21.Cells(2).Range.Shading.BackgroundPatternColor = RGB(0, 0, 64)

                newRowa21.Cells(1).Range.Text = "(System 1+2+3+4)"
                newRowa21.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                newRowa21.Range.Font.Color = Word.WdColor.wdColorWhite
                newRowa21.Cells(2).Range.Font.Name = "Rupee"
                If RblPriceYes.Checked = True Then
                    newRowa21.Cells(2).Range.Text = "` " + Convert.ToString(specialTotal)
                End If
                newRowa21.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                If RblPriceYes.Checked = True Then
                    newRowa21.Cells(3).Range.Text = "` " + Convert.ToString(specialmuTotal)
                End If
                newRowa21.Cells(3).Range.Font.Name = "Rupee"
                newRowa21.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


            End If










        End If




        If DocumentStatus = 0 Then
            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
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
        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document

        wordDocument1 = New Word.Document

        wordApplication1 = New Word.Application
        Dim formating1 As Object

        objDoc.SaveAs(QtempPath + "\SpecialPriceSheet.doc")
        Dim paramSourceDocPath1 As Object = QtempPath + "\SpecialPriceSheet.doc"
        Dim Targets1 As Object = QtempPath + "\SpecialPriceSheet.pdf"
        If (FlagPdf = 1) Or (btnAddClear.Text = "View") Then
            If btnAddClear.Text = "View" Then
                Class1.global.GobalMaxId = QuationId
            End If
            objDoc.SaveAs(appPath + "\OrderData\SpecialPrice" + "\" + Class1.global.GobalMaxId.ToString() + ".doc")
            objDoc.SaveAs(appPath + "\OrderData\SpecialPrice" + "\" + Class1.global.GobalMaxId.ToString() + ".pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)
        End If

        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        wordDocument1.Close()
        wordDocument1 = Nothing
        wordApplication1.NormalTemplate.Saved = True
        wordApplication1.Quit()
        wordApplication1 = Nothing

        objDoc.Close()
        objDoc = Nothing
        objApp.NormalTemplate.Saved = True
        objApp.Quit()
        objApp = Nothing


    End Sub
    Private Sub btnHF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHF.Click

        Class1.killProcessOnUser()
        Try
            con1.Close()
            ' Dim dte As EnvDTE80.DTE2


            Me.UseWaitCursor = True
            DocumentStatus = 0
            If btnSave1.Text = "Update" Then
                Class1.global.GobalMaxId = QuationId
                FlagPdf = 1
                con1.Open()
                Dim strPdfInsert As String
                strPdfInsert = "Update  PDFGenerate_Check  Set IsCreated = 'Yes' where FK_QuatationID =" & QuationId & ""
                cmd = New SqlCommand(strPdfInsert, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con1.Close()
            Else
                PDFSetQuatationTrue()
            End If

            'indexs()
            OleMessageFilter.Register()
            FinalDucumetation()
            OleMessageFilter.Revoke()
            Me.UseWaitCursor = False
            SetClean()
            btnAddSys1.Visible = True
            btnSaveSys2.Visible = True
            btnSaveSys3.Visible = True
            btnSaveSys4.Visible = True

            GvTechnicalSYS1.DataSource = Null
            GvTechnicalSYS1.Refresh()
            GvTechnicalSYS2.DataSource = Null
            GvTechnicalSYS2.Refresh()
            GvTechnicalSYS3.DataSource = Null
            GvTechnicalSYS3.Refresh()
            GvTechnicalSYS4.DataSource = Null
            GvTechnicalSYS4.Refresh()
            'Throw New Exception()

        Catch ex As Exception
            'MessageBox.Show("1 " & ex.Message)

            'MessageBox.Show("2 " & ex.StackTrace())

            Class1.killProcessOnUser()
            Me.UseWaitCursor = False
            SetClean()
            btnAddSys1.Visible = True
            btnSaveSys2.Visible = True
            btnSaveSys3.Visible = True
            btnSaveSys4.Visible = True

            GvTechnicalSYS1.DataSource = Null
            GvTechnicalSYS1.Refresh()
            GvTechnicalSYS2.DataSource = Null
            GvTechnicalSYS2.Refresh()
            GvTechnicalSYS3.DataSource = Null
            GvTechnicalSYS3.Refresh()
            GvTechnicalSYS4.DataSource = Null
            GvTechnicalSYS4.Refresh()
            MessageBox.Show(ex.Message.ToString())
        End Try


    End Sub

    Private Sub btnWf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWf.Click
        Class1.killProcessOnUser()
        Try
            con1.Close()
        Catch ex As Exception

        End Try
        Try


            Me.UseWaitCursor = True
            DocumentStatus = 1
            If btnSave1.Text = "Update" Then
                Class1.global.GobalMaxId = QuationId
                FlagPdf = 1
                con1.Open()
                Dim strPdfInsert As String
                strPdfInsert = "Update PDFGenerate_Check  Set IsCreated = 'Yes' where FK_QuatationID =" & QuationId & ""
                cmd = New SqlCommand(strPdfInsert, con1)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con1.Close()
            Else
                PDFSetQuatationTrue()
            End If

            OleMessageFilter.Register()
            FinalDucumetation()
            OleMessageFilter.Revoke()
            Me.UseWaitCursor = False
            SetClean()
            btnAddSys1.Visible = True
            btnSaveSys2.Visible = True
            btnSaveSys3.Visible = True
            btnSaveSys4.Visible = True

            GvTechnicalSYS1.DataSource = Null
            GvTechnicalSYS1.Refresh()
            GvTechnicalSYS2.DataSource = Null
            GvTechnicalSYS2.Refresh()
            GvTechnicalSYS3.DataSource = Null
            GvTechnicalSYS3.Refresh()
            GvTechnicalSYS4.DataSource = Null
            GvTechnicalSYS4.Refresh()
        Catch ex As Exception
            Me.UseWaitCursor = False
            SetClean()
            btnAddSys1.Visible = True
            btnSaveSys2.Visible = True
            btnSaveSys3.Visible = True
            btnSaveSys4.Visible = True

            GvTechnicalSYS1.DataSource = Null
            GvTechnicalSYS1.Refresh()
            GvTechnicalSYS2.DataSource = Null
            GvTechnicalSYS2.Refresh()
            GvTechnicalSYS3.DataSource = Null
            GvTechnicalSYS3.Refresh()
            GvTechnicalSYS4.DataSource = Null
            GvTechnicalSYS4.Refresh()

            Class1.killProcessOnUser()
            MessageBox.Show(ex.ToString())
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
        TxtTax.Visible = True
        lbltxttax.Visible = True
        TxtTax.Enabled = True
        TxtTax.Text = "0"
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
        btnSave1.Text = "Save"
        txtNoContentSYS1.Enabled = True
        txtNoContentSYS2.Enabled = True
        txtNoContentSYS3.Enabled = True
        txtNoContentSYS4.Enabled = True
        txtCapacity1.Enabled = True
        txtCapacity2.Enabled = True
        txtEnqNo.Enabled = True
        txtspdiscount.Text = ""
        txtVat.Text = ""
        txtErection.Text = ""
        txtInsurance.Text = ""
        txtFinalPrice.Text = ""
        txtISI.Text = ""
        txtTransporation.Text = ""
        txtPakingforwarding.Text = ""
        txtBussness_Exe.Text = ""
        txtQoutType.Text = ""
        lblHeaderSingleSys1.Text = ""
        lblHeadermultiSys1.Text = ""
        lblHeaderOtherSys1.Text = ""
        lblHeaderSinlgeSys2.Text = ""
        lblHeaderMultSys2.Text = ""
        lblHeaderotherSys2.Text = ""
        lblHeaderSingleSys3.Text = ""
        txtMultipleSys3.Text = ""
        txtOtherSys3.Text = ""
        lblHeaderSingleSys4.Text = ""
        lblHeadermultiSys4.Text = ""
        lblHeaderOtherSys4.Text = ""
        lblSys1Total.Text = ""
        lblSys2Total.Text = ""
        Label22.Text = ""
        Label27.Text = ""
        lblSys3MulTotal.Text = ""
        lblSys4MulTotal.Text = ""
        lblAllsysTotal.Text = ""
        lblAllMulSysTotal.Text = ""
        lblSys1MulTotal.Text = ""
        lblSys2MulTotal.Text = ""
        lblSys3Total.Text = ""
        lblSys4Total.Text = ""
        txtExpCapSys1.Text = ""
        txtExpSys4.Text = ""


    End Sub

    Private Sub btnAddClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddClear.Click
        Try
            con1.Close()
        Catch ex As Exception

        End Try
        Try

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

                'Dim Targets1 As Object = appPath + "\OrderData" + "\" + Convert.ToString(QuationId) + ".pdf"
                'System.Diagnostics.Process.Start(Targets1)
            Else

                SetClean()
                btnSave1.Text = "Save"
                RblMultiple.Enabled = True
                RblOther.Enabled = True
                RblSingle.Enabled = True
                QuationId = 0

                If GvTechnicalSYS1.Rows.Count > 0 Then
                    GvTechnicalSYS1.DataSource = Null
                    btnAddSys1.Visible = True
                    btnSaveSys2.Visible = True
                    btnSaveSys3.Visible = True
                    btnSaveSys4.Visible = True

                End If

                If GvTechnicalSYS2.Rows.Count > 0 Then
                    GvTechnicalSYS2.DataSource = Null
                    btnAddSys1.Visible = True
                    btnSaveSys2.Visible = True
                    btnSaveSys3.Visible = True
                    btnSaveSys4.Visible = True
                End If


                If GvTechnicalSYS3.Rows.Count > 0 Then
                    GvTechnicalSYS3.DataSource = Null
                    btnAddSys1.Visible = True
                    btnSaveSys2.Visible = True
                    btnSaveSys3.Visible = True
                    btnSaveSys4.Visible = True

                End If

                If GvTechnicalSYS4.Rows.Count > 0 Then
                    GvTechnicalSYS4.DataSource = Null
                    btnAddSys1.Visible = True
                    btnSaveSys2.Visible = True
                    btnSaveSys3.Visible = True
                    btnSaveSys4.Visible = True

                End If
            End If
            txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
            txtLatterDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        BindOnLanguageName()
    End Sub

    Private Sub txttax_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        SetClean()
        GvTechnicalSYS1.ReadOnly = False
        GvTechnicalSYS2.ReadOnly = False
        GvTechnicalSYS3.ReadOnly = False
        GvTechnicalSYS4.ReadOnly = False

        RblSingle.Enabled = True
        RblMultiple.Enabled = True
        RblOther.Enabled = True

        btnAddSys1.Visible = True
        btnSaveSys2.Visible = True
        btnSaveSys3.Visible = True
        btnSaveSys4.Visible = True

        GvTechnicalSYS1.DataSource = Null
        GvTechnicalSYS1.Refresh()
        GvTechnicalSYS2.DataSource = Null
        GvTechnicalSYS2.Refresh()
        GvTechnicalSYS3.DataSource = Null
        GvTechnicalSYS3.Refresh()
        GvTechnicalSYS4.DataSource = Null
        GvTechnicalSYS4.Refresh()
        btnSave1.Text = "Save"
        QuationId = 0
    End Sub

    Private Sub txtQoutType_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQoutType.Leave
        If txtQoutType.Text.Trim().ToUpper() = "ISI" Then
            PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
            PicDefault.ImageLocation = appPath + "\image\SIGNATURES\ISI.jpg"
            PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
        ElseIf txtQoutType.Text.Trim().ToUpper() = "RO" Then
            PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
            PicDefault.ImageLocation = appPath + "\image\SIGNATURES\RO.jpg"
            PicDefault.SizeMode = PictureBoxSizeMode.StretchImage
        End If


    End Sub

    Private Sub rdbTerms2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTerms2.CheckedChanged
        txt9.Visible = False
        txt91.Visible = False
        txtPayTerms.Visible = False
        txt1.Text = "Price bases Ex. Godown Ahmedabad."
        'txt2.Text = "15% Vat Extra at actual"
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

    Private Sub rdbTerms1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetcheckData()
    End Sub

    Private Sub btnSys1Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSys1.Click

        Try
            If (TxtTax.Text = "") Then
                TxtTax.Text = "0"
            End If

            Dim t As Int16
            dt = GvTechnicalSYS1.DataSource

            t = dt.Rows.Count

            t = t + 1
            If RblSingle.Checked = True Then
                dt.Rows.Add(0, 0, lblSnoSys1.Text, txtContentsys1.Text, Convert.ToDecimal(txtPricemultipleSys1.Text) + Convert.ToDecimal((Convert.ToDecimal(txtPricemultipleSys1.Text) * Convert.ToDecimal(TxtTax.Text)) / 100), TxtTax.Text)
            ElseIf RblOther.Checked = True Then
                dt.Rows.Add(0, 0, lblSnoSys1.Text, txtContentsys1.Text, txtPriceSingleSys1.Text, txtPricemultipleSys1.Text, Convert.ToDecimal(txtPriceothersys1_31.Text) + (Convert.ToDecimal(txtPriceothersys1_31.Text) * Convert.ToDecimal(TxtTax.Text) / 100), TxtTax.Text)
            ElseIf RblMultiple.Checked = True Then
                dt.Rows.Add(0, 0, lblSnoSys1.Text, txtContentsys1.Text, Convert.ToDecimal(txtPriceSingleSys1.Text) + Convert.ToDecimal(Convert.ToDecimal(txtPriceSingleSys1.Text) * Convert.ToDecimal(TxtTax.Text) / 100), Convert.ToDecimal(txtPricemultipleSys1.Text) + Convert.ToDecimal(Convert.ToDecimal(txtPricemultipleSys1.Text) * Convert.ToDecimal(TxtTax.Text) / 100), TxtTax.Text)
            End If

            If dt Is Nothing Then
            Else
                If dt.Rows.Count > 0 Then
                    Dim dView As New DataView(dt)
                    dView.Sort = "SrNo ASC"
                    dt = dView.ToTable()
                End If
            End If
            GvTechnicalSYS1.DataSource = dt
            Total1()
            txtContentsys1.Text = ""
            txtPricemultipleSys1.Text = ""
            txtPriceSingleSys1.Text = ""
            txtPriceothersys1_31.Text = ""
            'lblSnoSys1.Text = (dt.Rows.Count + 1).ToString()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnSaveSys2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSys2.Click

        Try
            If (TxtTax.Text = "") Then
                TxtTax.Text = "0"
            End If

            Dim t As Int16
            dt = GvTechnicalSYS2.DataSource
            t = dt.Rows.Count
            t = t + 1
            If RblSingle.Checked = True Then
                dt.Rows.Add(0, 0, lblsNosys2.Text, txtcontentnosys2.Text, Convert.ToDecimal(txtpriceMultipleSys2.Text) + (Convert.ToDecimal(txtpriceMultipleSys2.Text) * Convert.ToDecimal(TxtTax.Text) / 100), TxtTax.Text)
            ElseIf RblOther.Checked = True Then
                dt.Rows.Add(0, 0, lblsNosys2.Text, txtcontentnosys2.Text, txtpriceSinglesys2.Text, txtpriceMultipleSys2.Text, Convert.ToDecimal(txtPriceOtherSys2.Text) + (Convert.ToDecimal(txtPriceOtherSys2.Text) * Convert.ToDecimal(TxtTax.Text) / 100), TxtTax.Text)
            ElseIf RblMultiple.Checked = True Then
                dt.Rows.Add(0, 0, lblsNosys2.Text, txtcontentnosys2.Text, Convert.ToDecimal(txtpriceSinglesys2.Text) + (Convert.ToDecimal(txtpriceSinglesys2.Text) * Convert.ToDecimal(TxtTax.Text) / 100), Convert.ToDecimal(txtpriceMultipleSys2.Text) + (Convert.ToDecimal(txtpriceMultipleSys2.Text) * Convert.ToDecimal(TxtTax.Text)) / 100, TxtTax.Text)
            End If


            GvTechnicalSYS2.DataSource = dt
            Total1()
            txtcontentnosys2.Text = ""
            txtpriceMultipleSys2.Text = ""
            txtpriceSinglesys2.Text = ""
            txtPriceOtherSys2.Text = ""
            lblsNosys2.Text = (dt.Rows.Count + 1).ToString()
        Catch ex As Exception

        End Try

        'txtcontentnosys2.Text = ""
        'txtSingleSys3.Text = ""
        'txtMultipleSys3.Text = ""
        'txtOtherSys3.Text = ""
    End Sub

    Private Sub btnSaveSys3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSys3.Click
        Try
            If (TxtTax.Text = "") Then
                TxtTax.Text = "0"
            End If
            Dim t As Int16
            dt = GvTechnicalSYS3.DataSource
            t = dt.Rows.Count
            t = t + 1
            If RblSingle.Checked = True Then
                dt.Rows.Add(0, 0, lblsNosys3.Text, txtcontentnosys3.Text, Convert.ToDecimal(txtMultipleSys3.Text) + (Convert.ToDecimal(txtMultipleSys3.Text) * Convert.ToDecimal(TxtTax.Text) / 100), TxtTax.Text, txtCapacitySys3.Text)
            ElseIf RblOther.Checked = True Then
                dt.Rows.Add(0, 0, lblsNosys3.Text, txtcontentnosys3.Text, txtSingleSys3.Text, txtMultipleSys3.Text, Convert.ToDecimal(txtOtherSys3.Text) + (Convert.ToDecimal(txtOtherSys3.Text) * Convert.ToDecimal(TxtTax.Text) / 100), TxtTax.Text, txtCapacitySys3.Text)
            ElseIf RblMultiple.Checked = True Then
                dt.Rows.Add(0, 0, lblsNosys3.Text, txtcontentnosys3.Text, Convert.ToDecimal(txtSingleSys3.Text) + (Convert.ToDecimal(txtSingleSys3.Text) * Convert.ToDecimal(TxtTax.Text) / 100), Convert.ToDecimal(txtMultipleSys3.Text) + (Convert.ToDecimal(txtMultipleSys3.Text) * Convert.ToDecimal(TxtTax.Text) / 100), TxtTax.Text, txtCapacitySys3.Text)
            End If

            GvTechnicalSYS3.DataSource = dt
            Total1()
            'txtContentsys1.Text = ""
            'txtPricemultipleSys1.Text = ""
            'txtPriceSingleSys1.Text = ""
            'txtPriceothersys1_31.Text = ""
            ' lblsNosys3.Text = (dt.Rows.Count + 1).ToString()
            txtCapacitySys3.Text = ""
            txtcontentnosys3.Text = ""
            txtSingleSys3.Text = ""
            txtMultipleSys3.Text = ""
            txtOtherSys3.Text = ""
            GvTechnicalSYS3_CellEndEdit(Nothing, Nothing)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnSaveSys4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSys4.Click
        Try
            If (TxtTax.Text = "") Then
                TxtTax.Text = "0"
            End If
            Dim t As Int16
            dt = GvTechnicalSYS4.DataSource
            t = dt.Rows.Count
            t = t + 1
            If RblSingle.Checked = True Then
                dt.Rows.Add(0, 0, lblSrNOSys4.Text, txtcontentnosys4.Text, Convert.ToDecimal(txtMultipleSys4.Text) + (Convert.ToDecimal(txtMultipleSys4.Text) * Convert.ToDecimal(TxtTax.Text) / 100), TxtTax.Text)
            ElseIf RblOther.Checked = True Then
                dt.Rows.Add(0, 0, lblSrNOSys4.Text, txtcontentnosys4.Text, txtSingleSys4.Text, txtMultipleSys4.Text, Convert.ToDecimal(txtOtherSys4.Text) + (Convert.ToDecimal(txtOtherSys4.Text) * Convert.ToDecimal(TxtTax.Text)), TxtTax.Text)
            ElseIf RblMultiple.Checked = True Then
                dt.Rows.Add(0, 0, lblSrNOSys4.Text, txtcontentnosys4.Text, txtSingleSys4.Text, txtMultipleSys4.Text, TxtTax.Text)
            End If
            GvTechnicalSYS4.DataSource = dt
            Total1()
            txtcontentnosys4.Text = ""
            txtSingleSys4.Text = ""
            txtMultipleSys4.Text = ""
            txtOtherSys4.Text = ""
            ' lblSrNOSys4.Text = (dt.Rows.Count + 1).ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GvCategorySearch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvCategorySearch.CellContentClick

    End Sub

    Private Sub txtNoContent_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoContentSYS1.Leave
        GetTechnicalData(LanguageId)
        GvSingle_Bind("System 1")

    End Sub

    Private Sub txtContent1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContentsys1.Leave
        Dim str As String
        Dim ds1 As DataSet
        Dim da1 As SqlDataAdapter

        If txtContentsys1.Text.Trim() <> "" Then


            If RblSingle.Checked = True Or RblOther.Checked = True Or RblMultiple.Checked = True Then
                str = "select * from Category_Master (NOLOCK) where Category='" + txtContentsys1.Text.Trim() + "' and Capacity='" + txtCapacity1.Text.Trim() + "' and MainCategory='System 1' and LanguageId = " & LanguageId & ""
                da1 = New SqlDataAdapter(str, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)
                If (ds1.Tables(0).Rows.Count > 0) Then

                    If RblSingle.Checked = True Then
                        txtPricemultipleSys1.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblSnoSys1.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                    ElseIf RblOther.Checked = True Then
                        txtPriceSingleSys1.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblSnoSys1.Text = ds1.Tables(0).Rows(0)("SNo").ToString()

                    ElseIf RblMultiple.Checked = True Then
                        Dim da2 As SqlDataAdapter
                        Dim ds2 As DataSet

                        str = "select * from Category_Master (NOLOCK) where Category='" + txtContentsys1.Text.Trim() + "' and Capacity='" + txtCapacity2.Text.Trim() + "' and MainCategory='System 1' and LanguageId = " & LanguageId & ""
                        da2 = New SqlDataAdapter(str, con1)
                        ds2 = New DataSet()
                        da2.Fill(ds2)
                        'Capacity 1'
                        If (ds1.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtPriceSingleSys1.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                                lblSnoSys1.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                            End If
                        End If
                        'capacity 2'

                        If (ds2.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtPricemultipleSys1.Text = ds2.Tables(0).Rows(0)("Price").ToString()

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

    Private Sub txtcontentnosys2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcontentnosys2.Leave
        Dim str As String
        Dim ds1 As DataSet
        Dim da1 As SqlDataAdapter

        If txtcontentnosys2.Text.Trim() <> "" Then


            If RblSingle.Checked = True Or RblOther.Checked = True Or RblMultiple.Checked = True Then
                str = "select * from Category_Master (NOLOCK) where Category='" + txtcontentnosys2.Text.Trim() + "' and Capacity='" + txtCapacity1.Text.Trim() + "' and MainCategory='System 2' and LanguageId = " & LanguageId & ""
                da1 = New SqlDataAdapter(str, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                If (ds1.Tables(0).Rows.Count > 0) Then
                    If RblSingle.Checked = True Then
                        txtpriceMultipleSys2.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblsNosys2.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                    ElseIf RblOther.Checked = True Then
                        txtpriceSinglesys2.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblsNosys2.Text = ds1.Tables(0).Rows(0)("SNo").ToString()

                    ElseIf RblMultiple.Checked = True Then
                        Dim da2 As SqlDataAdapter
                        Dim ds2 As DataSet

                        str = "select * from Category_Master (NOLOCK) where Category='" + txtcontentnosys2.Text.Trim() + "' and Capacity='" + txtCapacity2.Text.Trim() + "' and MainCategory='System 2' and LanguageId = " & LanguageId & ""
                        da2 = New SqlDataAdapter(str, con1)
                        ds2 = New DataSet()
                        da2.Fill(ds2)
                        'Capacity 1'
                        If (ds1.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtpriceSinglesys2.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                                lblsNosys2.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                            End If
                        End If
                        'capacity 2'

                        If (ds2.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtpriceMultipleSys2.Text = ds2.Tables(0).Rows(0)("Price").ToString()

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

    Public Sub System1_AutoComplete()

    End Sub


    Private Sub txtcontentnosys3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcontentnosys3.Leave
        Dim str As String
        Dim desg As String
        Dim ds1 As DataSet
        Dim da1 As SqlDataAdapter

        If txtcontentnosys3.Text.Trim() <> "" Then
            If RblSingle.Checked = True Or RblOther.Checked = True Or RblMultiple.Checked = True Then
                str = "select * from Category_Master (NOLOCK) where Category='" + txtcontentnosys3.Text.Trim() + "'  and  MainCategory='System 3' and LanguageId = " & LanguageId & ""
                da1 = New SqlDataAdapter(str, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)
                txtCapacitySys3.AutoCompleteCustomSource.Clear()
                For Each dr1 As DataRow In ds1.Tables(0).Rows
                    txtCapacitySys3.AutoCompleteCustomSource.Add(dr1.Item("Capacity").ToString())
                Next

                If (ds1.Tables(0).Rows.Count > 0) Then


                    If RblSingle.Checked = True Then
                        txtMultipleSys3.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblsNosys3.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                        txtCapacitySys3.Text = ds1.Tables(0).Rows(0)("Capacity").ToString()
                    ElseIf RblOther.Checked = True Then
                        txtSingleSys3.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblsNosys3.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                        txtCapacitySys3.Text = ds1.Tables(0).Rows(0)("Capacity").ToString()

                    ElseIf RblMultiple.Checked = True Then
                        Dim da2 As SqlDataAdapter
                        Dim ds2 As DataSet

                        str = "select * from Category_Master (NOLOCK) where MainCategory='System 3' and Category='" + txtcontentnosys3.Text.Trim() + "' and LanguageId = " & LanguageId & ""
                        da2 = New SqlDataAdapter(str, con1)
                        ds2 = New DataSet()
                        da2.Fill(ds2)
                        'Capacity 1'
                        If (ds1.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtSingleSys3.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                                lblsNosys3.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                                '   txtCapacitySys3.Text = ds1.Tables(0).Rows(0)("Capacity").ToString()

                            End If
                        End If
                        'capacity 2'

                        If (ds2.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtMultipleSys3.Text = ds2.Tables(0).Rows(0)("Price").ToString()

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

    Private Sub txtcontentnosys4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcontentnosys4.Leave
        Dim str As String
        Dim ds1 As DataSet
        Dim da1 As SqlDataAdapter

        If txtcontentnosys4.Text.Trim() <> "" Then


            If RblSingle.Checked = True Or RblOther.Checked = True Or RblMultiple.Checked = True Then
                str = "select * from Category_Master (NOLOCK) where Category='" + txtcontentnosys4.Text.Trim() + "' and Capacity='" + txtCapacity1.Text.Trim() + "' and MainCategory='System 4' and LanguageId = " & LanguageId & ""
                da1 = New SqlDataAdapter(str, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)

                If (ds1.Tables(0).Rows.Count > 0) Then

                    If RblSingle.Checked = True Then
                        txtMultipleSys4.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblSrNOSys4.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                    ElseIf RblOther.Checked = True Then
                        txtSingleSys4.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblSrNOSys4.Text = ds1.Tables(0).Rows(0)("SNo").ToString()

                    ElseIf RblMultiple.Checked = True Then
                        Dim da2 As SqlDataAdapter
                        Dim ds2 As DataSet

                        str = "select * from Category_Master (NOLOCK) where Category='" + txtcontentnosys4.Text.Trim() + "' and Capacity='" + txtCapacity2.Text.Trim() + "' and MainCategory='System 4' and LanguageId = " & LanguageId & ""
                        da2 = New SqlDataAdapter(str, con1)
                        ds2 = New DataSet()
                        da2.Fill(ds2)
                        'Capacity 1'
                        If (ds1.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtSingleSys4.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                                lblSrNOSys4.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                            End If
                        End If
                        'capacity 2'

                        If (ds2.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtMultipleSys4.Text = ds2.Tables(0).Rows(0)("Price").ToString()

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

    Private Sub txtContentsys1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContentsys1.TextChanged

    End Sub

    Private Sub txtpriceMultipleSys2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpriceMultipleSys2.Leave
        If RblOther.Checked = True Then
            If (txtpriceMultipleSys2.Text.Trim() <> "" And txtpriceSinglesys2.Text.Trim() <> "") Then
                txtPriceOtherSys2.Text = (Convert.ToDecimal(txtpriceMultipleSys2.Text) * Convert.ToDecimal(txtpriceSinglesys2.Text)).ToString()
            End If
        End If
    End Sub

    Private Sub txtMultipleSys3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMultipleSys3.Leave
        If RblOther.Checked = True Then
            If (txtMultipleSys3.Text.Trim() <> "" And txtSingleSys3.Text.Trim() <> "") Then
                txtOtherSys3.Text = (Convert.ToDecimal(txtMultipleSys3.Text) * Convert.ToDecimal(txtSingleSys3.Text)).ToString()
            End If
        End If
    End Sub

    Private Sub txtMultipleSys4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMultipleSys4.Leave
        If RblOther.Checked = True Then
            If (txtMultipleSys4.Text.Trim() <> "" And txtSingleSys4.Text.Trim() <> "") Then
                txtOtherSys4.Text = (Convert.ToDecimal(txtMultipleSys4.Text) * Convert.ToDecimal(txtSingleSys4.Text)).ToString()
            End If
        End If

    End Sub

    Private Sub txtPrice_11_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPricemultipleSys1.Leave
        If RblOther.Checked = True Then
            If (txtPricemultipleSys1.Text.Trim() <> "" And txtPriceSingleSys1.Text.Trim() <> "") Then
                txtPriceothersys1_31.Text = (Convert.ToDecimal(txtPricemultipleSys1.Text) * Convert.ToDecimal(txtPriceSingleSys1.Text)).ToString()
            End If
        End If
    End Sub

    Private Sub RdbEnglish_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbEnglish.CheckedChanged
        BindOnLanguageName()
        GvTechnicalSYS1.DataSource = Null
        GvTechnicalSYS2.DataSource = Null
        GvTechnicalSYS3.DataSource = Null
        GvTechnicalSYS4.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()

    End Sub

    Private Sub RdbGujarati_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbGujarati.CheckedChanged
        BindOnLanguageName()
        GvTechnicalSYS1.DataSource = Null
        GvTechnicalSYS2.DataSource = Null
        GvTechnicalSYS3.DataSource = Null
        GvTechnicalSYS4.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        RblOther.Enabled = True
        SetClean()

    End Sub

    Private Sub RdbHindi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbHindi.CheckedChanged
        BindOnLanguageName()
        GvTechnicalSYS1.DataSource = Null
        GvTechnicalSYS2.DataSource = Null
        GvTechnicalSYS3.DataSource = Null
        GvTechnicalSYS4.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()

    End Sub

    Private Sub RdbMarathi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbMarathi.CheckedChanged
        BindOnLanguageName()
        GvTechnicalSYS1.DataSource = Null
        GvTechnicalSYS2.DataSource = Null
        GvTechnicalSYS3.DataSource = Null
        GvTechnicalSYS4.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()

    End Sub

    Private Sub RdbTamil_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbTamil.CheckedChanged
        BindOnLanguageName()
        GvTechnicalSYS1.DataSource = Null
        GvTechnicalSYS2.DataSource = Null
        GvTechnicalSYS3.DataSource = Null
        GvTechnicalSYS4.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()

    End Sub

    Private Sub RdbTelugu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbTelugu.CheckedChanged
        BindOnLanguageName()
        GvTechnicalSYS1.DataSource = Null
        GvTechnicalSYS2.DataSource = Null
        GvTechnicalSYS3.DataSource = Null
        GvTechnicalSYS4.DataSource = Null
        RblMultiple.Enabled = True
        RblSingle.Enabled = True
        RblOther.Enabled = True
        SetClean()

    End Sub

    Private Sub ISIQuotation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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


    Private Sub rdbTerms1_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTerms1.CheckedChanged
        GetcheckData()
        txt9.Visible = False
        txt91.Visible = False
        txtPayTerms.Visible = False
    End Sub

    Private Sub txtCapacitySys3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCapacitySys3.Leave
        Dim str As String
        Dim ds1 As DataSet
        Dim da1 As SqlDataAdapter

        If txtCapacitySys3.Text.Trim() <> "" Then

            If RblSingle.Checked = True Or RblOther.Checked = True Or RblMultiple.Checked = True Then
                str = "select * from Category_Master (NOLOCK) where MainCategory='System 3' and Capacity = '" + txtCapacitySys3.Text.Trim() + "' and Category = '" + txtcontentnosys3.Text.Trim() + "' and LanguageId = " & LanguageId & ""
                da1 = New SqlDataAdapter(str, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)
                If (ds1.Tables(0).Rows.Count > 0) Then


                    If RblSingle.Checked = True Then
                        txtMultipleSys3.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblsNosys3.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                        txtCapacitySys3.Text = ds1.Tables(0).Rows(0)("Capacity").ToString()
                    ElseIf RblOther.Checked = True Then
                        txtSingleSys3.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                        lblsNosys3.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                        txtCapacitySys3.Text = ds1.Tables(0).Rows(0)("Capacity").ToString()

                    ElseIf RblMultiple.Checked = True Then
                        Dim da2 As SqlDataAdapter
                        Dim ds2 As DataSet

                        str = "select * from Category_Master (NOLOCK) where MainCategory='System 3' and Capacity = '" + txtCapacitySys3.Text.Trim() + "' and Category = '" + txtcontentnosys3.Text.Trim() + "' and LanguageId = " & LanguageId & ""
                        da2 = New SqlDataAdapter(str, con1)
                        ds2 = New DataSet()
                        da2.Fill(ds2)
                        'Capacity 1'
                        If (ds1.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtSingleSys3.Text = ds1.Tables(0).Rows(0)("Price").ToString()
                                lblsNosys3.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                                txtCapacitySys3.Text = ds1.Tables(0).Rows(0)("Capacity").ToString()

                            End If
                        End If
                        'capacity 2'

                        If (ds2.Tables(0).Rows.Count > 0) Then
                            If RblMultiple.Checked = True Then
                                txtMultipleSys3.Text = ds2.Tables(0).Rows(0)("Price").ToString()

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

    Private Sub GvTechnical_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvTechnicalSYS1.CellEndEdit
        Try
            If RblOther.Checked = True Then
                If e.ColumnIndex = 5 Then
                    Dim totalcalc As Decimal
                    totalcalc = Convert.ToDecimal(GvTechnicalSYS1.Rows(e.RowIndex).Cells("Qty").Value) * Convert.ToDecimal(GvTechnicalSYS1.Rows(e.RowIndex).Cells("Total").Value)
                    GvTechnicalSYS1.Rows(e.RowIndex).Cells("Total").Value = totalcalc.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS1.Rows(e.RowIndex).Cells("Price").Value.ToString())
                    tax = (rate1 * Convert.ToInt32(GvTechnicalSYS1.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount = rate1 + tax
                    GvTechnicalSYS1.Rows(e.RowIndex).Cells("Price").Value = finalamount.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS1.Rows(e.RowIndex).Cells("Price").Value.ToString())
                    tax = (rate1 * Convert.ToInt32(GvTechnicalSYS1.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount = rate1 + tax
                    GvTechnicalSYS1.Rows(e.RowIndex).Cells("Price").Value = finalamount.ToString("N2")
                    Dim totalcalc As Decimal
                    totalcalc = Convert.ToDecimal(Convert.ToDecimal(GvTechnicalSYS1.Rows(e.RowIndex).Cells("Qty").Value) * finalamount)
                    GvTechnicalSYS1.Rows(e.RowIndex).Cells("Total").Value = totalcalc.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS1.Rows(e.RowIndex).Cells("Price1").Value.ToString())
                    tax1 = (rate1 * Convert.ToInt32(GvTechnicalSYS1.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount1 = rate1 + tax1
                    GvTechnicalSYS1.Rows(e.RowIndex).Cells("Price1").Value = finalamount1.ToString("N2")
                    rate2 = Convert.ToDecimal(Me.GvTechnicalSYS1.Rows(e.RowIndex).Cells("Price2").Value.ToString())
                    tax2 = (rate2 * Convert.ToInt32(GvTechnicalSYS1.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount2 = rate2 + tax2
                    GvTechnicalSYS1.Rows(e.RowIndex).Cells("Price2").Value = finalamount2.ToString("N2")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GvTechnicalSYS2_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvTechnicalSYS2.CellEndEdit
        Try



            If RblOther.Checked = True Then


                If e.ColumnIndex = 5 Then
                    Dim totalcalc As Decimal
                    totalcalc = Convert.ToDecimal(GvTechnicalSYS2.Rows(e.RowIndex).Cells("Qty").Value) * Convert.ToDecimal(GvTechnicalSYS2.Rows(e.RowIndex).Cells("Total").Value)
                    GvTechnicalSYS2.Rows(e.RowIndex).Cells("Total").Value = totalcalc.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS2.Rows(e.RowIndex).Cells("Price").Value.ToString())
                    tax = (rate1 * Convert.ToInt32(GvTechnicalSYS2.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount = rate1 + tax
                    GvTechnicalSYS2.Rows(e.RowIndex).Cells("Price").Value = finalamount.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS2.Rows(e.RowIndex).Cells("Price").Value.ToString())
                    tax = (rate1 * Convert.ToInt32(GvTechnicalSYS2.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount = rate1 + tax
                    GvTechnicalSYS2.Rows(e.RowIndex).Cells("Price").Value = finalamount.ToString("N2")
                    Dim totalcalc As Decimal
                    totalcalc = Convert.ToDecimal(Convert.ToDecimal(GvTechnicalSYS2.Rows(e.RowIndex).Cells("Qty").Value) * finalamount)
                    GvTechnicalSYS2.Rows(e.RowIndex).Cells("Total").Value = totalcalc.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS2.Rows(e.RowIndex).Cells("Price1").Value.ToString())
                    tax1 = (rate1 * Convert.ToInt32(GvTechnicalSYS2.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount1 = rate1 + tax1
                    GvTechnicalSYS2.Rows(e.RowIndex).Cells("Price1").Value = finalamount1.ToString("N2")
                    rate2 = Convert.ToDecimal(Me.GvTechnicalSYS2.Rows(e.RowIndex).Cells("Price2").Value.ToString())
                    tax2 = (rate2 * Convert.ToInt32(GvTechnicalSYS2.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount2 = rate2 + tax2
                    GvTechnicalSYS2.Rows(e.RowIndex).Cells("Price2").Value = finalamount2.ToString("N2")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GvTechnicalSYS3_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvTechnicalSYS3.CellEndEdit
        Try



            If RblOther.Checked = True Then


                If e.ColumnIndex = 5 Then
                    Dim totalcalc As Decimal
                    totalcalc = Convert.ToDecimal(GvTechnicalSYS3.Rows(e.RowIndex).Cells("Qty").Value) * Convert.ToDecimal(GvTechnicalSYS3.Rows(e.RowIndex).Cells("Total").Value)
                    GvTechnicalSYS3.Rows(e.RowIndex).Cells("Total").Value = totalcalc.ToString("N2")

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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS3.Rows(e.RowIndex).Cells("Price").Value.ToString())
                    tax = (rate1 * Convert.ToInt32(GvTechnicalSYS3.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount = rate1 + tax
                    GvTechnicalSYS3.Rows(e.RowIndex).Cells("Price").Value = finalamount.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS3.Rows(e.RowIndex).Cells("Price").Value.ToString())
                    tax = (rate1 * Convert.ToInt32(GvTechnicalSYS3.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount = rate1 + tax
                    GvTechnicalSYS3.Rows(e.RowIndex).Cells("Price").Value = finalamount.ToString("N2")
                    Dim totalcalc As Decimal
                    totalcalc = Convert.ToDecimal(Convert.ToDecimal(GvTechnicalSYS3.Rows(e.RowIndex).Cells("Qty").Value) * finalamount)
                    GvTechnicalSYS3.Rows(e.RowIndex).Cells("Total").Value = totalcalc.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS3.Rows(e.RowIndex).Cells("Price1").Value.ToString())
                    tax1 = (rate1 * Convert.ToInt32(GvTechnicalSYS3.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount1 = rate1 + tax1
                    GvTechnicalSYS3.Rows(e.RowIndex).Cells("Price1").Value = finalamount1.ToString("N2")
                    rate2 = Convert.ToDecimal(Me.GvTechnicalSYS3.Rows(e.RowIndex).Cells("Price2").Value.ToString())
                    tax2 = (rate2 * Convert.ToInt32(GvTechnicalSYS3.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount2 = rate2 + tax2
                    GvTechnicalSYS3.Rows(e.RowIndex).Cells("Price2").Value = finalamount2.ToString("N2")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub CalculateTax()



    End Sub

    Private Sub GvTechnicalSYS4_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvTechnicalSYS4.CellEndEdit
        Try

            If RblOther.Checked = True Then
                If e.ColumnIndex = 5 Then
                    Dim totalcalc As Decimal
                    totalcalc = Convert.ToDecimal(Convert.ToDecimal(GvTechnicalSYS4.Rows(e.RowIndex).Cells("Qty").Value) * Convert.ToDecimal(GvTechnicalSYS4.Rows(e.RowIndex).Cells("Total").Value))
                    GvTechnicalSYS4.Rows(e.RowIndex).Cells("Total").Value = totalcalc.ToString("N2")

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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS4.Rows(e.RowIndex).Cells("Price").Value.ToString())
                    tax = (rate1 * Convert.ToInt32(GvTechnicalSYS4.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount = rate1 + tax
                    GvTechnicalSYS4.Rows(e.RowIndex).Cells("Price").Value = finalamount.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS4.Rows(e.RowIndex).Cells("Price").Value.ToString())
                    tax = (rate1 * Convert.ToInt32(GvTechnicalSYS4.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount = rate1 + tax
                    GvTechnicalSYS4.Rows(e.RowIndex).Cells("Price").Value = finalamount.ToString("N2")
                    Dim totalCalc As Decimal
                    totalCalc = Convert.ToDecimal(Convert.ToDecimal(GvTechnicalSYS4.Rows(e.RowIndex).Cells("Qty").Value) * finalamount)
                    GvTechnicalSYS4.Rows(e.RowIndex).Cells("Total").Value = totalCalc.ToString("N2")
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
                    rate1 = Convert.ToDecimal(Me.GvTechnicalSYS4.Rows(e.RowIndex).Cells("Price1").Value.ToString())
                    tax1 = (rate1 * Convert.ToInt32(GvTechnicalSYS4.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount1 = rate1 + tax1
                    GvTechnicalSYS4.Rows(e.RowIndex).Cells("Price1").Value = finalamount1.ToString("N2")
                    rate2 = Convert.ToDecimal(Me.GvTechnicalSYS4.Rows(e.RowIndex).Cells("Price2").Value.ToString())
                    tax2 = (rate2 * Convert.ToInt32(GvTechnicalSYS4.Rows(e.RowIndex).Cells("Tax").Value)) / 100
                    finalamount2 = rate2 + tax2
                    GvTechnicalSYS4.Rows(e.RowIndex).Cells("Price2").Value = finalamount2.ToString("N2")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPDFPRICEOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDFPRICEOnly.Click
        Try

            Class1.killProcessOnUser()


            DocumentStatus = 0
            OleMessageFilter.Register()
            AdditionalPriceSheet()
            OleMessageFilter.Revoke()

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
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
        If txtspdiscount.Text.Trim() = "" And txtVat.Text.Trim() = "" And txtISI.Text.Trim() = "" And txtTransporation.Text.Trim() = "" And txtInsurance.Text.Trim() = "" And txtPakingforwarding.Text.Trim() = "" And txtErection.Text.Trim() = "" Then
            flag = 1
        Else
            Specialpricesheet()
        End If

        If flag = 1 Then
            cnt = 0
        Else
            cnt = 1
        End If
        Dim files(cnt) As String
        If (btnAddClear.Text = "View") Then
            files(0) = appPath + "\OrderData\" + Convert.ToString(QuationId) + ".pdf"
        Else
            files(0) = QtempPath + "\Letter2.pdf"
        End If
        If (flag <> 1) Then
            If (btnAddClear.Text = "View") Then
                files(1) = appPath + "\OrderData\SpecialPrice\" + Convert.ToString(QuationId) + ".pdf"
            Else
                files(1) = QtempPath + "\SpecialPriceSheet.pdf"
            End If
            '   files(1) = QtempPath + "\SpecialPriceSheet.pdf"
        End If
        If (Not System.IO.Directory.Exists(appPath + "\Temporaryview")) Then
            System.IO.Directory.CreateDirectory(appPath + "\Temporaryview")
        End If
        If (btnAddClear.Text = "View") Then
            _pdfforge.MergePDFFiles(files, appPath + "\Temporaryview\" + Convert.ToString(QuationId) + ".pdf", False)
        Else
            _pdfforge.MergePDFFiles(files, QtempPath + "\AdditionPriceSheet.pdf", False)
        End If
        Class1.killProcessOnUser()
        MessageBox.Show("Price Sheet Ready !")
        If (btnAddClear.Text = "View") Then
            System.Diagnostics.Process.Start(appPath + "\Temporaryview\" + Convert.ToString(QuationId) + ".pdf")
        Else
            System.Diagnostics.Process.Start(QtempPath + "\AdditionPriceSheet.pdf")
        End If

    End Sub

    Private Sub btnPdfPriceWOHF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdfPriceWOHF.Click
        Class1.killProcessOnUser()

        DocumentStatus = 1

        OleMessageFilter.Register()
        AdditionalPriceSheet()
        OleMessageFilter.Revoke()
        'PriceSheet()
        'Specialpricesheet()
        'MessageBox.Show("Price Sheet Ready !")
        'System.Diagnostics.Process.Start(appPath + "\Letter2.pdf")


    End Sub

    Private Sub btnPDFFirstPageOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDFFirstPageOnly.Click
        Try


            Class1.killProcessOnUser()

            DocumentStatus = 0
            OleMessageFilter.Register()
            FirstPage()
            OleMessageFilter.Revoke()

            MessageBox.Show("First Letter Ready !")
            System.Diagnostics.Process.Start(QtempPath + "\Letter1.pdf")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())

        End Try
    End Sub

    Private Sub btnPDFFirstWOHF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDFFirstWOHF.Click
        Try


            Class1.killProcessOnUser()

            DocumentStatus = 1
            OleMessageFilter.Register()
            FirstPage()
            OleMessageFilter.Revoke()
            MessageBox.Show("First Letter Ready !")
            System.Diagnostics.Process.Start(QtempPath + "\Letter1.pdf")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
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

    Private Sub txtISI_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtISI.Validating
        Try

            If IsNumeric(txtISI.Text) Then
            Else
                txtISI.Text = ""
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

    Private Sub tbAddiprice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbAddiprice.Click

    End Sub

    Private Sub txtspdiscount_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtspdiscount.Leave
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
        If IsNumeric(txtISI.Text) Then
            ISI = Convert.ToDecimal(txtISI.Text)
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

        If IsNumeric(lblSys1Total.Text) Then
            TotalLeavevar = TotalLeavevar + Convert.ToDecimal(lblSys1Total.Text)
            AllSysTotal = AllSysTotal + Convert.ToDecimal(lblSys1Total.Text)
        End If

        If IsNumeric(lblSys2Total.Text) Then
            TotalLeavevar = TotalLeavevar + Convert.ToDecimal(lblSys2Total.Text)
            AllSysTotal = AllSysTotal + Convert.ToDecimal(lblSys2Total.Text)
        End If

        If IsNumeric(lblSys3Total.Text) Then
            TotalLeavevar = TotalLeavevar + Convert.ToDecimal(lblSys3Total.Text)
            AllSysTotal = AllSysTotal + Convert.ToDecimal(lblSys3Total.Text)
        End If

        If IsNumeric(lblSys4Total.Text) Then
            TotalLeavevar = TotalLeavevar + Convert.ToDecimal(lblSys4Total.Text)
            AllSysTotal = AllSysTotal + Convert.ToDecimal(lblSys4Total.Text)

        End If
        lblAllsysTotal.Text = Convert.ToString(AllSysTotal)

        If IsNumeric(lblSys1MulTotal.Text) Then
            TotalLeavevar = TotalLeavevar + Convert.ToDecimal(lblSys1MulTotal.Text)
            AllMulSysTotal = AllMulSysTotal + Convert.ToDecimal(lblSys1MulTotal.Text)
        End If

        If IsNumeric(lblSys2MulTotal.Text) Then
            TotalLeavevar = TotalLeavevar + Convert.ToDecimal(lblSys2MulTotal.Text)
            AllMulSysTotal = AllMulSysTotal + Convert.ToDecimal(lblSys2MulTotal.Text)
        End If

        If IsNumeric(lblSys3MulTotal.Text) Then
            TotalLeavevar = TotalLeavevar + Convert.ToDecimal(lblSys3MulTotal.Text)
            AllMulSysTotal = AllMulSysTotal + Convert.ToDecimal(lblSys3MulTotal.Text)
        End If

        If IsNumeric(lblSys4MulTotal.Text) Then
            TotalLeavevar = TotalLeavevar + Convert.ToDecimal(lblSys4MulTotal.Text)
            AllMulSysTotal = AllMulSysTotal + Convert.ToDecimal(lblSys4MulTotal.Text)
        End If
        lblAllMulSysTotal.Text = Convert.ToString(AllMulSysTotal)

        txtFinalPrice.Text = TotalLeavevar
    End Sub

    Private Sub txtVat_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVat.Leave
        TotalLeave()
    End Sub

    Private Sub txtISI_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtISI.Leave
        TotalLeave()
    End Sub

    Private Sub txtTransporation_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTransporation.Leave
        TotalLeave()
    End Sub

    Private Sub txtInsurance_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsurance.Leave
        TotalLeave()
    End Sub

    Private Sub txtPakingforwarding_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPakingforwarding.Leave
        TotalLeave()
    End Sub

    Private Sub txtErection_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtErection.Leave
        TotalLeave()
    End Sub



    Private Sub txtExpCapSys1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExpCapSys1.Leave
        If RblSingle.Checked Then
            GvSingle_Bind("System 1")
            txtExpSys4.Text = txtExpCapSys1.Text
            GvSingle_Bind("System 4")
        End If

    End Sub

    Private Sub txtExpSys4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExpSys4.Leave
        If RblSingle.Checked Then
            GvSingle_Bind("System 4")
        End If

    End Sub

    Private Sub TxtTax_Leave_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTax.Leave


        If RblSingle.Checked = True Or RblOther.Checked = True Then
            If TxtTax.Text <> "" And txtCapacity1.Text.Trim <> "" Then
                GvSingle_Bind("System 1")
                GvSingle_Bind("System 2")
                GvSingle_Bind("System 3")
                GvSingle_Bind("System 4")
            End If

        ElseIf RblMultiple.Checked = True Then
            If TxtTax.Text <> "" And txtCapacity1.Text.Trim <> "" And txtCapacity2.Text.Trim <> "" Then
                GvMultiple_Bind("System 1")
                GvMultiple_Bind("System 2")
                GvMultiple_Bind("System 3")
                GvMultiple_Bind("System 4")
            End If
        End If


    End Sub

    Private Sub txtSearchName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchName.Leave
        Try
            con1.Close()
        Catch ex As Exception
        End Try
        con1.Open()
        Dim str As String
        str = "select Enq_No from Quotation_Master (NOLOCK) where Name='" + txtSearchName.Text + "'"
        da = New SqlDataAdapter(str, con1)
        ds = New DataSet()
        da.Fill(ds)
        Dim tt As Int32
        'tt = GvComapnySearch.Rows.Count()

        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtSearchEnQ.AutoCompleteCustomSource.Add(dr1.Item("Enq_No").ToString())
        Next

        da.Dispose()
        ds.Dispose()
    End Sub

    Private Sub txtName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.Leave
        Dim str As String
        Try
            con1.Close()

        Catch ex As Exception

        End Try
        con1.Close()
        con1.Open()
        str = "select Address,Name from Quotation_Master (NOLOCK) where Name='" & txtName.Text & "' or Enq_No='" + txtEnqNo.Text.Trim() + "' "
        cmd = New SqlCommand(str, con1)
        dr = cmd.ExecuteReader()
        If (dr.HasRows) Then
            dr.Read()
            If (dr("Address").ToString() <> "") Then
                txtAddress.Text = dr("Address").ToString()
                'txtName.Text = dr("Name").ToString()

            End If
        End If
        cmd.Dispose()
        dr.Dispose()
        con1.Close()

    End Sub

    Private Sub TextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Leave

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



    Private Sub GvCategorySearch_NewRowNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles GvCategorySearch.NewRowNeeded

    End Sub

    Private Sub GvCategorySearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GvCategorySearch.KeyUp

        Try
            If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
                If (Class1.global.QuatationId <> 0) Then
                    QuationId = Class1.global.QuatationId
                    btnAddClear.Enabled = True
                    btnAddClear.Text = "View"
                    btnCancel.Enabled = False
                Else
                    btnAddClear.Enabled = True
                    btnAddClear.Text = "Add New"
                    btnCancel.Enabled = True
                    QuationId = Convert.ToInt32(Me.GvCategorySearch.Rows(GvCategorySearch.CurrentRow.Index).Cells(0).Value)
                End If
                con1.Close()
                btnSave1.Text = "Update"

                If txtCapacity1.Text <> "" Then
                    txtCapacity1_Leave(sender, e)
                End If
                If txtCapacity2.Text <> "" Then
                    txtCapacity2_Leave(sender, e)
                End If

                Display()
                Total1()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub rblNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblNew.CheckedChanged
        GvCategorySearch_For_SalesExecutive_Bind()
    End Sub
    Private Sub rblOld_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblOld.CheckedChanged
        GvQuotationSearch_Bind(1)
    End Sub

  
End Class