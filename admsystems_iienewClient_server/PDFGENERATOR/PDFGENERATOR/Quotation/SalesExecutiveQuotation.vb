Imports System.Collections.Generic
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
Public Class SalesExecutiveQuotation
    Private con1 As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader
    Private da As SqlDataAdapter
    Private ds As DataSet
    Private da1 As SqlDataAdapter
    Private ds1 As DataSet
    Shared dt As DataTable
    Shared dt1 As DataTable
    Shared Address_ID As Integer
    Shared Fk_SalesExecutiveQtnID As Integer

    Public capacityType As String
    Shared QuotationMaxId As Int32
    Shared Path11 As String
    Public UserID As Int32
    Public QuationId As Int32
    Public QPath As String

    Shared DocumentStatus As Int16
    Dim appPath As String
    Dim lines As String
    Shared LanguageId As Int32


    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Dim str
    Public Sub New()
        ''txtQoutType.Font.Name = "Sa"
        InitializeComponent()
        con1 = Class1.con
        LanguageId = 1
        appPath = Path.GetDirectoryName(Application.ExecutablePath)
        txtSendBy.Text = Class1.global.User
        ddlBillCountry_Bind()
        ddlEnqType_Bind()
        AutoCompated_Text()
        GvSalesExecutive_Data()
        txtEnqNo.Enabled = False

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

                        Else

                        End If
                        If (dv.Rows(RowCount)("IsPrint") = True) Then

                        Else


                        End If


                    Next
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub GvSalesExecutive_Data()

        'For sales Executive
        Dim dt As New DataTable
        dt = New DataTable
        dt.Columns.Add("Fk_SalesExecutiveQtnID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Status")
        dt.Columns.Add("To")
        dt.Columns.Add("Priority")
        Dim Total_Pending As Integer
        Dim Total_Done As Integer
        Dim Total_Cancel As Integer

        Total_Pending = 0
        Total_Done = 0
        Total_Cancel = 0


        If chkALL.Checked = False Then
            Dim data = linq_obj.SP_Get_SalesExecutiveQuotation_List().ToList().Where(Function(p) p.FromUser.ToLower() = Class1.global.loginuser.ToLower() And p.Status.Trim() <> "Done" And p.Status.Trim() <> "Cancel").ToList()

            For Each item As SP_Get_SalesExecutiveQuotation_ListResult In data
                If item.Status = "Pending" Then
                    Total_Pending = Total_Pending + 1
                ElseIf item.Status = "Done" Then

                    Total_Done = Total_Done + 1
                Else
                    Total_Cancel = Total_Cancel + 1

                End If
                dt.Rows.Add(item.PK_SalesExecutiveQtnID, item.EnqNo, item.Status, item.ToUSer, item.Priority)
            Next
            txtTotalRecord.Text = data.Count()
        Else
            Dim data = linq_obj.SP_Get_SalesExecutiveQuotation_List().ToList().Where(Function(p) p.FromUser.ToLower() = Class1.global.loginuser.ToLower()).ToList()
            For Each item As SP_Get_SalesExecutiveQuotation_ListResult In data
                If item.Status = "Pending" Then
                    Total_Pending = Total_Pending + 1
                ElseIf item.Status = "Done" Then

                    Total_Done = Total_Done + 1
                Else
                    Total_Cancel = Total_Cancel + 1

                End If
                dt.Rows.Add(item.PK_SalesExecutiveQtnID, item.EnqNo, item.Status, item.ToUSer, item.Priority)
            Next
            txtTotalRecord.Text = data.Count()
        End If
        GvSalesExecutiveData.DataSource = dt


        'Urgent Color 

        For index = 0 To GvSalesExecutiveData.RowCount - 1
            If (GvSalesExecutiveData.Rows(index).Cells(4).Value = "Urgent") Then
                GvSalesExecutiveData.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                GvSalesExecutiveData.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.White
            End If
            If (GvSalesExecutiveData.Rows(index).Cells(4).Value = "Courier") Then
                GvSalesExecutiveData.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Green
                GvSalesExecutiveData.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.White
            End If
        Next
        GvSalesExecutiveData.Columns(0).Visible = False
        GvSalesExecutiveData.Columns(4).Visible = False

        txtTotalPending.Text = Total_Pending.ToString()
        txtTotalDone.Text = Total_Done.ToString()
        txtTotalCancel.Text = Total_Cancel.ToString()


    End Sub

    Public Sub ddlBillCountry_Bind()
        ddlBillCountry.Items.Clear()

        Dim BillCountry = linq_obj.SP_Get_Country_Master_List().ToList()
        ddlBillCountry.DataSource = BillCountry
        ddlBillCountry.DisplayMember = "CountryName"
        ddlBillCountry.ValueMember = "PK_Country_ID"
        ddlBillCountry.AutoCompleteMode = AutoCompleteMode.Append
        ddlBillCountry.DropDownStyle = ComboBoxStyle.DropDownList
        ddlBillCountry.AutoCompleteSource = AutoCompleteSource.ListItems


    End Sub

    Public Sub ddlBillState_Bind()

        Dim BillState = linq_obj.SP_Get_Add_State_Master_List(Convert.ToInt32(ddlBillCountry.SelectedValue)).ToList()
        ddlBillState.DataSource = BillState
        ddlBillState.DisplayMember = "StateName"
        ddlBillState.ValueMember = "PK_Address_State_ID"
        ddlBillState.AutoCompleteMode = AutoCompleteMode.Append
        ddlBillState.DropDownStyle = ComboBoxStyle.DropDownList
        ddlBillState.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub ddlBillDistrict_Bind()

        ' Dim BillDistrict = linq_obj.SP_Get_Add_District_Master_List(Convert.ToInt32(ddlBillState.SelectedValue)).ToList()
        Dim BillDistrict = linq_obj.SP_Get_Address_Master_DistrictName_By_State_City(ddlBillState.Text.Trim(), ddlBillCity1.Text.Trim())
        ddlBillDistrict.DataSource = BillDistrict
        ddlBillDistrict.DisplayMember = "Add_District_Name"
        ddlBillDistrict.ValueMember = "PK_Add_District_ID"
        ddlBillDistrict.AutoCompleteMode = AutoCompleteMode.Append
        ddlBillDistrict.DropDownStyle = ComboBoxStyle.DropDownList
        ddlBillDistrict.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Public Sub ddlBillCity_Bind()

        Dim BillCity1 = linq_obj.SP_Get_Add_City_Master_List(Convert.ToInt32(ddlBillState.SelectedValue)).ToList()
        ddlBillCity1.DataSource = BillCity1
        ddlBillCity1.DisplayMember = "Add_CityName"
        ddlBillCity1.ValueMember = "PK_Add_City_Master_ID"
        ddlBillCity1.AutoCompleteMode = AutoCompleteMode.Append
        ddlBillCity1.DropDownStyle = ComboBoxStyle.DropDownList
        ddlBillCity1.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub ddlEnqType_Bind()
        Dim str As String
        str = "select * from Enq_Type (NOLOCK)"
        da = New SqlDataAdapter(str, con1)
        ds = New DataSet()
        da.Fill(ds)
        ddlEnqType.DataSource = ds.Tables(0)
        ddlEnqType.DisplayMember = "EnqType"
        ddlEnqType.ValueMember = "Pk_EnqTypeID"
        da.Dispose()
        ds.Dispose()


        'Quotation Type


        ddlQType.Items.Clear()
        Dim dt As New DataTable
        dt.Columns.Add("Item")
        dt.Rows.Add("ISI")
        dt.Rows.Add("NON ISI")
        dt.Rows.Add("RO")
        dt.Rows.Add("PACKING")
        dt.Rows.Add("SPARE")
        dt.Rows.Add("SODA SOFT DRINK")




        ddlQType.DataSource = dt
        ddlQType.DisplayMember = "Item"
        ddlQType.ValueMember = "Item"

        'QTN type

        Dim dt1 As New DataTable
        ddlQtnType.Items.Clear()
        dt1.Columns.Add("Item")
        dt1.Rows.Add("New")
        dt1.Rows.Add("Rev")


        ddlQtnType.DataSource = dt1
        ddlQtnType.DisplayMember = "Item"
        ddlQtnType.ValueMember = "Item"


        Dim dt2 As New DataTable
        ddlPriority.Items.Clear()
        dt2.Columns.Add("Item")
        dt2.Rows.Add("Normal")
        dt2.Rows.Add("Medium")
        dt2.Rows.Add("Urgent")
        dt2.Rows.Add("Courier")



        ddlPriority.DataSource = dt2
        ddlPriority.DisplayMember = "Item"
        ddlPriority.ValueMember = "Item"



        Dim dt3 As New DataTable
        ddlthrough.Items.Clear()
        dt3.Columns.Add("Item")
        dt3.Rows.Add("Mail")
        dt3.Rows.Add("Courier")
        dt3.Rows.Add("H2H")
        dt3.Rows.Add("WhatsApp")
        dt3.Rows.Add("SPEEDPOST")
        dt3.Rows.Add("MAIL + SPEEDPOST")
        dt3.Rows.Add("MAIL + COURIER")
        dt3.Rows.Add("MAIL + WHATSAPP")
        dt3.Rows.Add("COURIER + WHATSAPP")
        dt3.Rows.Add("SPEEDPOST + WHATSAPP")



        ddlthrough.DataSource = dt3
        ddlthrough.DisplayMember = "Item"
        ddlthrough.ValueMember = "Item"


    End Sub

    Public Sub AutoCompated_Text()
        getAutoCompleteData("Name")
        getAutoCompleteData("City")
        getAutoCompleteData("State")
        getAutoCompleteData("Area")
        getAutoCompleteData("District")
        getAutoCompleteData("EnqNo")
    End Sub

    Public Sub getAutoCompleteData(ByVal strType As String)
        Select Case strType.Trim()
            Case "Name"
                txtName.AutoCompleteCustomSource.Clear()
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("Name").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    txtName.AutoCompleteCustomSource.Add(iteam.Result)
                    txtSearchName.AutoCompleteCustomSource.Add(iteam.Result)
                Next
                'Case "City"
                '    txtCity.AutoCompleteCustomSource.Clear()
                '    Dim data = linq_obj.SP_Get_AddressListAutoComplete("City").ToList()
                '    For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                '        txtCity.AutoCompleteCustomSource.Add(iteam.Result)

                '    Next

                '    For Each iteam As SP_Get_AddressListAutoCompleteResult In data


                '    Next
            Case "Area"
                txtArea.AutoCompleteCustomSource.Clear()
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("Area").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    txtArea.AutoCompleteCustomSource.Add(iteam.Result)
                Next
            Case "State"


                'Search State Auto complated 

                ddlBillState.Items.Clear()

                ddlBillState.DataSource = Nothing

                Dim datatable As New DataTable
                datatable.Columns.Add("Result")
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("State").ToList()
                For Each item As SP_Get_AddressListAutoCompleteResult In data
                    datatable.Rows.Add(item.Result)
                Next
                Dim newRow As DataRow = datatable.NewRow()

                newRow(0) = "Select"
                datatable.Rows.InsertAt(newRow, 0)
                ddlBillState.DataSource = datatable
                ddlBillState.DisplayMember = "Result"
                ddlBillState.ValueMember = "Result"
                ddlBillState.AutoCompleteMode = AutoCompleteMode.Append
                ddlBillState.DropDownStyle = ComboBoxStyle.DropDownList
                ddlBillState.AutoCompleteSource = AutoCompleteSource.ListItems


                Dim datatablenew As New DataTable
                datatablenew.Columns.Add("Result")


                Dim dataDel = linq_obj.SP_Get_AddressListAutoComplete("State").ToList()
                For Each item As SP_Get_AddressListAutoCompleteResult In data
                    datatablenew.Rows.Add(item.Result)
                Next
                Dim newRow1 As DataRow = datatablenew.NewRow()

                newRow1(0) = "Select"
                datatablenew.Rows.InsertAt(newRow1, 0)
                ddlBillState.Text = "CL"

                 

            Case "EnqNo"
                TxtSearchEnqNo.AutoCompleteCustomSource.Clear()

                Dim data = linq_obj.SP_Get_AddressListAutoComplete("EnqNo").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    TxtSearchEnqNo.AutoCompleteCustomSource.Add(iteam.Result)
                Next



        End Select
    End Sub

    Private Sub ddlQType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlQType.SelectionChangeCommitted
        Tabcontrol.TabPages(0).Text = ddlQType.Text & " Quotation"
        grp12.Visible = False
        grp3.Visible = False
        grp4.Visible = False
        lblSys3.Visible = False

        If ddlQType.Text.Trim() = "ISI" Then
            grp12.Visible = True
            grp3.Visible = True
            grp4.Visible = True
            lblSys3.Visible = True
            lblCapacity3text.Text = "Capacity"
            txtSrNoSystem3.Visible = True
            lblSys3SRNo.Visible = True

        Else
            lblSys3.Visible = False
            txtSrNoSystem3.Visible = False
            lblSys3SRNo.Visible = False
            lblCapacity3text.Text = "Sr.No"
            grp3.Visible = True
            GvTechnicalSYS3.DataSource = Nothing
            GvTechnicalSYS4.DataSource = Nothing

        End If

    End Sub

    Public Sub GvTechnical_Single_Data()

        'Capacity 1 and Capacity 2
        Dim desg As String
        con1.Close()
        If ddlQType.Text.Trim() = "ISI" Then
            dt = New DataTable()
            dt.Columns.Add("Select", GetType(Boolean))
            dt.Columns.Add("SrNo", GetType(Int32))
            dt.Columns.Add("Category", GetType(String))
            dt.Columns.Add("Capacity", GetType(String))
            'get System 3 data
            desg = "select * from Category_Master (NOLOCK) where  MainCategory='System 3' ORDER BY SNo"
            da = New SqlDataAdapter(desg, con1)
            ds = New DataSet()
            da.Fill(ds)
            If ds.Tables(0).Rows.Count >= 6 Then
                For i As Integer = 0 To 5
                    dt.Rows.Add(1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), ds.Tables(0).Rows(i)("Capacity").ToString())
                Next
                GvTechnicalSYS3.DataSource = Null
                GvTechnicalSYS3.DataSource = dt
                GvTechnicalSYS3.Columns(0).Width = 40
                GvTechnicalSYS3.Columns(1).Width = 25
                da.Dispose()
                ds.Dispose()

            Else
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    dt.Rows.Add(1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString(), ds.Tables(0).Rows(i)("Capacity").ToString())
                Next
                GvTechnicalSYS3.DataSource = Null
                GvTechnicalSYS3.DataSource = dt
                GvTechnicalSYS3.Columns(0).Width = 40
                GvTechnicalSYS3.Columns(1).Width = 25

            End If

            'Auto Complated Data fill
            txtSrNoSystem3.AutoCompleteCustomSource.Clear()
            txtSys3Capacity.AutoCompleteCustomSource.Clear()
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtSrNoSystem3.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
                txtSys3Capacity.AutoCompleteCustomSource.Add(dr1.Item("Capacity").ToString())

            Next

            da.Dispose()
            ds.Dispose()


            '-----------------------------------------------------------------------------------'
            'get System 4 data

            Dim dt1 As New DataTable
            dt1 = New DataTable()
            dt1.Columns.Add("Select", GetType(Boolean))
            dt1.Columns.Add("SrNo", GetType(Int32))
            dt1.Columns.Add("Category", GetType(String))

            desg = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity1.Text & "' and MainCategory='System 4' ORDER BY SNo"
            da = New SqlDataAdapter(desg, con1)
            ds = New DataSet()
            da.Fill(ds)
            If ds.Tables(0).Rows.Count >= 6 Then
                For i As Integer = 0 To 5
                    dt1.Rows.Add(0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString())
                Next
                GvTechnicalSYS4.DataSource = Null
                GvTechnicalSYS4.DataSource = dt1
                GvTechnicalSYS4.Columns(0).Width = 40
                GvTechnicalSYS4.Columns(1).Width = 25

                da.Dispose()
                ds.Dispose()
            Else
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    dt1.Rows.Add(0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString())
                Next
                GvTechnicalSYS4.DataSource = Null
                GvTechnicalSYS4.DataSource = dt1
                GvTechnicalSYS4.Columns(0).Width = 40
                GvTechnicalSYS4.Columns(1).Width = 25

                da.Dispose()
                ds.Dispose()

            End If


        Else

            'NON-ISI / RO data
            dt = New DataTable()
            dt.Columns.Add("Select", GetType(Boolean))
            dt.Columns.Add("SrNo", GetType(Int32))
            dt.Columns.Add("Category", GetType(String))
            Dim str As String
            str = "select * from Category_Master (NOLOCK) where Capacity  ='" & txtCapacity1.Text & "' and MainCategory='" + ddlQType.Text.Trim() + "' ORDER BY SNo"
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)
            'Auto Complated Data fill
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtSys3Capacity.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())

            Next

            Dim defaulvalue As Integer
            If ddlQType.Text.Trim() = "RO" Or ddlQType.Text.Trim() = "NON ISI" Then
                defaulvalue = 8
            Else
                defaulvalue = 0
            End If

            If ds.Tables(0).Rows.Count > defaulvalue Then
                For i As Integer = 0 To defaulvalue - 1
                    If i < 2 Then
                        dt.Rows.Add(1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString())
                    Else
                        dt.Rows.Add(0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString())
                    End If

                Next
                GvTechnicalSYS3.DataSource = Null
                GvTechnicalSYS3.DataSource = dt
                GvTechnicalSYS3.Columns(0).Width = 40
                GvTechnicalSYS3.Columns(1).Width = 25

                da.Dispose()
                ds.Dispose()

            Else
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If i < 2 Then
                        dt.Rows.Add(1, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString())
                    Else
                        dt.Rows.Add(0, Convert.ToInt32(ds.Tables(0).Rows(i)("SNo")), ds.Tables(0).Rows(i)("Category").ToString())
                    End If

                Next
                GvTechnicalSYS3.DataSource = Null
                GvTechnicalSYS3.DataSource = dt
                GvTechnicalSYS3.Columns(0).Width = 40
                GvTechnicalSYS3.Columns(1).Width = 25
                da.Dispose()
                ds.Dispose()

            End If

        End If

    End Sub


    Public Sub GetTechnicalData_Autocomplated()
        Dim desg As String

        If ddlQType.Text = "ISI" Then
            desg = ""
            con1 = Class1.con
            desg = "select * from Category_Master (NOLOCK) where  MainCategory ='System 3' and LanguageId = 1 ORDER BY SNo"
            da = New SqlDataAdapter(desg, con1)
            ds = New DataSet()
            da.Fill(ds)
            txtSrNoSystem3.AutoCompleteCustomSource.Clear()
            txtSys3Capacity.AutoCompleteCustomSource.Clear()
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtSrNoSystem3.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
                txtSys3Capacity.AutoCompleteCustomSource.Add(dr1.Item("Capacity").ToString())
            Next
            da.Dispose()
            ds.Dispose()
        Else
            desg = ""
            con1 = Class1.con
            desg = "select * from Category_Master (NOLOCK) where  MainCategory ='" + ddlQType.Text + "'  and LanguageId = 1 ORDER BY SNo"
            da = New SqlDataAdapter(desg, con1)
            ds = New DataSet()
            da.Fill(ds)

            txtSys3Capacity.AutoCompleteCustomSource.Clear()
            For Each dr1 As DataRow In ds.Tables(0).Rows
                txtSys3Capacity.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
            Next
            da.Dispose()
            ds.Dispose()
        End If


    End Sub




    Private Sub rblSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblSingle.CheckedChanged
        txtCapacity2.Visible = False
        'If rblSingle.Checked = True Then
        '    GvTechnical_Single_Data()
        'Else
        '    GvMultiple_Bind()

        'End If

    End Sub

    Private Sub rblMultiple_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblMultiple.CheckedChanged
        txtCapacity2.Visible = True
        'If rblSingle.Checked = True Then
        '    GvTechnical_Single_Data()
        'Else
        '    GvMultiple_Bind()

        'End If

    End Sub

    Private Sub txtCapacity1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCapacity1.Leave

        GvTechnical_Single_Data()

    End Sub

    Private Sub btnAddMoresystem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMoresystem3.Click

        Try

            Dim t As Int16
            dt = GvTechnicalSYS3.DataSource
            t = dt.Rows.Count
            t = t + 1

            If ddlQType.Text = "ISI" Then
                dt.Rows.Add(1, lblSys3SRNo.Text, txtSrNoSystem3.Text, txtSys3Capacity.Text)
            Else
                'NON ISI /RO
                dt.Rows.Add(1, lblCapacity3text.Text, txtSys3Capacity.Text)
            End If

            GvTechnicalSYS3.DataSource = dt
            txtSrNoSystem3.Text = ""
            txtSys3Capacity.Text = ""



        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtSrNoSystem3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSrNoSystem3.Leave
        Dim str As String
        Dim desg As String
        Dim ds1 As DataSet
        Dim da1 As SqlDataAdapter

        If txtSrNoSystem3.Text.Trim() <> "" Then
            If rblSingle.Checked = True Or rblMultiple.Checked = True Then
                str = "select * from Category_Master (NOLOCK) where Category='" + txtSrNoSystem3.Text.Trim() + "'  and  MainCategory='System 3' and LanguageId = 1"
                da1 = New SqlDataAdapter(str, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)
                txtSys3Capacity.AutoCompleteCustomSource.Clear()
                For Each dr1 As DataRow In ds1.Tables(0).Rows
                    txtSys3Capacity.AutoCompleteCustomSource.Add(dr1.Item("Capacity").ToString())
                Next

                If (ds1.Tables(0).Rows.Count > 0) Then
                    If rblSingle.Checked = True Then
                        lblSys3SRNo.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                        txtSys3Capacity.Text = ds1.Tables(0).Rows(0)("Capacity").ToString()
                    ElseIf rblMultiple.Checked = True Then
                        Dim da2 As SqlDataAdapter
                        Dim ds2 As DataSet

                        str = "select * from Category_Master (NOLOCK) where MainCategory='System 3' and Category='" + txtSrNoSystem3.Text.Trim() + "' and LanguageId = 1"
                        da2 = New SqlDataAdapter(str, con1)
                        ds2 = New DataSet()
                        da2.Fill(ds2)
                        'Capacity 1'
                        If (ds1.Tables(0).Rows.Count > 0) Then
                            If rblMultiple.Checked = True Then
                                lblSys3SRNo.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                                txtSys3Capacity.Text = ds1.Tables(0).Rows(0)("Capacity").ToString()

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

    Private Sub txtSys3Capacity_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSys3Capacity.Leave


        If ddlQType.Text <> "ISI" Then
            Dim str As String
            Dim ds1 As DataSet
            Dim da1 As SqlDataAdapter
            If txtSys3Capacity.Text.Trim() <> "" Then
                str = "select * from Category_Master (NOLOCK) where Category='" + txtSys3Capacity.Text.Trim() + "' and Capacity='" + txtCapacity1.Text.Trim() + "' and MainCategory='" + ddlQType.Text + "'"
                da1 = New SqlDataAdapter(str, con1)
                ds1 = New DataSet()
                da1.Fill(ds1)
                If (ds1.Tables(0).Rows.Count > 0) Then
                    lblCapacity3text.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                End If
            End If
        Else

            If ddlQType.Text.Trim() = "ISI" Then
                If txtSys3Capacity.Text.Trim() <> "" Then
                    str = "select * from Category_Master (NOLOCK) where Category='" + txtSrNoSystem3.Text.Trim() + "' and Capacity='" + txtSys3Capacity.Text.Trim() + "' and MainCategory='System 3'"
                    da1 = New SqlDataAdapter(str, con1)
                    ds1 = New DataSet()
                    da1.Fill(ds1)
                    If (ds1.Tables(0).Rows.Count > 0) Then
                        lblSys3SRNo.Text = ds1.Tables(0).Rows(0)("SNo").ToString()
                    End If
                End If
            End If


        End If
    End Sub

    Private Sub btnSave1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave1.Click
        Try
            Dim FlagEnqStatus As Integer
            FlagEnqStatus = 0 'check EnqType & EnqNo equal

            If ddlEnqType.SelectedValue = 1 Then
                If txtEnqNo.Text.Contains("D") Then
                    FlagEnqStatus = 1
                End If
            ElseIf ddlEnqType.SelectedValue = 2 Then
                If txtEnqNo.Text.Contains("B") Then
                    FlagEnqStatus = 1
                End If
            ElseIf ddlEnqType.SelectedValue = 3 Then
                If txtEnqNo.Text.Contains("E") Then
                    FlagEnqStatus = 1
                End If
            ElseIf ddlEnqType.SelectedValue = 4 Then
                If txtEnqNo.Text.Contains("S") Then
                    FlagEnqStatus = 1
                End If
            End If


            If FlagEnqStatus = 1 Then

                'address insert 
                Dim techinical12 As String
                Dim System1 As String
                Dim System2 As String
                System1 = ""
                System2 = ""

                If btnSave1.Text.Trim() = "Save" Then
                    If ddlQtnType.Text = "New" Then
                        Dim Enq_ID As Integer
                        Enq_ID = ddlEnqType.SelectedValue
                        Dim enq_no = linq_obj.SP_Get_MaxEnqNoByEnqID(Enq_ID).ToList()
                        For Each item As SP_Get_MaxEnqNoByEnqIDResult In enq_no
                            txtEnqNo.Text = item.EnqNo
                        Next
                    End If
                    'insert new record
                    If Address_ID = 0 Then
                        Address_ID = linq_obj.SP_Insert_Update_Address_Master_for_SalesExecutive(0, ddlEnqType.SelectedValue, txtEnqNo.Text, 1, 13, txtName.Text.Trim(), txtAddress.Text.Trim(), txtArea.Text.Trim(),
                                                                                    ddlBillCity1.Text, txtPincode.Text, txtTaluka.Text, ddlBillDistrict.Text, ddlBillState.Text, "", txtLandlineNo.Text, txtmobileno.Text, txtEmail.Text, txtEmail1.Text, txtDate.Text, txtDate.Text, "1", txtReference.Text, txtReference2.Text, "New", "", "", txtRemark.Text, ddlBillCountry.Text, ddlBillState.Text)
                        linq_obj.SubmitChanges()


                        'Assign Default user CL
                        linq_obj.SP_Tbl_UserAllotmentDetail_Insert(Address_ID, 76, 16)

                        linq_obj.SubmitChanges()

                    End If
                Else
                    'update address data 'Only New Enq Update 
                    If ddlQtnType.Text = "New" Then
                        Address_ID = linq_obj.SP_Insert_Update_Address_Master_for_SalesExecutive(Address_ID, ddlEnqType.SelectedValue, txtEnqNo.Text, 1, 13, txtName.Text.Trim(), txtAddress.Text.Trim(), txtArea.Text.Trim(),
                                                                                   ddlBillCity1.Text, txtPincode.Text, txtTaluka.Text, ddlBillDistrict.Text, ddlBillState.Text, "", txtLandlineNo.Text, txtmobileno.Text, txtEmail.Text, txtEmail1.Text, txtDate.Text, txtDate.Text, "1", txtReference.Text, txtReference2.Text, "New", "", "", txtRemark.Text, ddlBillCountry.Text, ddlBillState.Text)
                        linq_obj.SubmitChanges()
                    End If




                End If

                If Address_ID > 0 Then
                    'sales executive data
                    System1 = "YES"

                    If chksystem1.Checked = True Then
                    End If
                    If chksystem2.Checked = True Then
                        System2 = "YES"

                    End If


                    'Capacity Type Single: 1 and Multiple :2

                    Dim capacitytype As Integer
                    capacitytype = 1

                    If rblMultiple.Checked = True Then
                        capacitytype = 2

                    End If
                    If btnSave1.Text.Trim() = "Save" Then
                        'insert Quotation
                        Fk_SalesExecutiveQtnID = linq_obj.Sp_insert_SalesExecutiveQuotation(0, Address_ID, Class1.global.UserID, ddlQtnType.Text, ddlPriority.SelectedValue, ddlthrough.SelectedValue, ddlQType.Text, capacitytype, txtCapacity1.Text, txtCapacity2.Text, System1, System2, "0", txtDescription.Text, txtDate.Text, "Pending")
                        linq_obj.SubmitChanges()
                    Else
                        'update Quotation
                        linq_obj.Sp_insert_SalesExecutiveQuotation(Fk_SalesExecutiveQtnID, Address_ID, Class1.global.UserID, ddlQtnType.Text, ddlPriority.SelectedValue, ddlthrough.SelectedValue, ddlQType.Text, capacitytype, txtCapacity1.Text, txtCapacity2.Text, System1, System2, "0", txtDescription.Text, txtDate.Text, "Pending")
                        linq_obj.SubmitChanges()
                    End If

                    'Delete Old Technical Data System 3 & Ssytem 4
                    con1.Close()
                    con1.Open()

                    techinical12 = "delete from SalesExecutive_Technical_Data where Fk_SalesExecutiveQtnID=" & Fk_SalesExecutiveQtnID & ""
                    cmd = New SqlCommand(techinical12, con1)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    'add Technical Data to Sales Executive :System 3 for ISI 
                    con1.Close()
                    con1.Open()
                    For i As Integer = 0 To GvTechnicalSYS3.Rows.Count - 1
                        Dim status As String
                        Dim selectStatus As Boolean = CBool(GvTechnicalSYS3.Rows(i).Cells(0).Value)
                        status = ""
                        If selectStatus Then
                            status = "Yes"
                        Else
                            status = "Yes"
                        End If
                        Dim MainCategory As String
                        If ddlQType.Text.Trim() = "ISI" Then
                            MainCategory = "System 3"
                        Else
                            MainCategory = ddlQType.Text.Trim() ' NON-ISI/RO
                        End If



                        'only selected check box selectStatus insert Technical data
                        If selectStatus Then
                            If ddlQType.Text = "ISI" Then
                                techinical12 = "insert into SalesExecutive_Technical_Data(Fk_SalesExecutiveQtnID,Fk_AddressID,SNo,TechnicalData,DocumentationImage,MainCategory,Capacity) values(" & Fk_SalesExecutiveQtnID & "," & Address_ID & ",'" + GvTechnicalSYS3.Rows(i).Cells(1).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(2).Value.ToString() + "','" + status + "','" + MainCategory + "','" + GvTechnicalSYS3.Rows(i).Cells(3).Value.ToString() + "')"
                            Else
                                techinical12 = "insert into SalesExecutive_Technical_Data(Fk_SalesExecutiveQtnID,Fk_AddressID,SNo,TechnicalData,DocumentationImage,MainCategory,Capacity) values(" & Fk_SalesExecutiveQtnID & "," & Address_ID & ",'" + GvTechnicalSYS3.Rows(i).Cells(1).Value.ToString() + "','" + GvTechnicalSYS3.Rows(i).Cells(2).Value.ToString() + "','" + status + "','" + MainCategory + "','')"
                            End If

                            cmd = New SqlCommand(techinical12, con1)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                        End If




                    Next
                    con1.Close()
                    'Only System 4 ISI Quotation

                    con1.Open()
                    If ddlQType.Text = "ISI" Then

                        For i As Integer = 0 To GvTechnicalSYS4.Rows.Count - 1

                            Dim status As String
                            Dim selectStatus As Boolean = CBool(GvTechnicalSYS4.Rows(i).Cells(0).Value)
                            status = ""

                            If selectStatus Then
                                status = "Yes"
                            Else
                                status = "No"

                            End If
                            Dim MainCategory As String
                            MainCategory = "System 4"

                            If selectStatus Then
                                techinical12 = "insert into SalesExecutive_Technical_Data(Fk_SalesExecutiveQtnID,Fk_AddressID,SNo,TechnicalData,DocumentationImage,MainCategory) values(" & Fk_SalesExecutiveQtnID & "," & Address_ID & ",'" + GvTechnicalSYS4.Rows(i).Cells(1).Value.ToString() + "','" + GvTechnicalSYS4.Rows(i).Cells(2).Value.ToString() + "','" + status + "','" + MainCategory + "')"

                                cmd = New SqlCommand(techinical12, con1)

                                cmd.ExecuteNonQuery()

                            End If

                            cmd.Dispose()
                        Next
                    End If
                    con1.Close()
                    MessageBox.Show("Submit Sucessfully...")
                    cleartext()
                    GvSalesExecutive_Data()
                Else
                    MessageBox.Show("Something is Wrong...try Again")
                    con1.Close()
                End If
            Else
                MessageBox.Show("Enq Type And EnqNo Not Match...")
                con1.Close()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
            con1.Close()

        End Try

    End Sub

    Private Sub txtCapacity2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCapacity2.Leave

        GvTechnical_Single_Data()

    End Sub



    Private Sub btnAddClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddClear.Click
        cleartext()
        btnSave1.Text = "Save"

    End Sub

    Public Sub cleartext()
        btnSave1.Text = "Save"
        Address_ID = 0
        txtEnqNo.Text = ""
        txtName.Text = ""
        txtAddress.Text = ""
        ddlBillState.Text = ""

        txtArea.Text = ""

        txtTaluka.Text = ""
        txtRemark.Text = ""
        txtmobileno.Text = ""
        txtEmail.Text = ""
        txtEmail1.Text = ""
        txtLandlineNo.Text = ""
        txtPincode.Text = ""
        txtCapacity1.Text = ""
        txtCapacity2.Text = ""
        txtReference.Text = ""
        txtReference2.Text = ""
        txtDescription.Text = ""
        ddlBillState.Text = "CL"
        chksystem1.Checked = True
        chksystem2.Checked = True


        lblState.Text = ""
        lblDistrictName.Text = ""
        lblCity.Text = ""

        rblSingle.Checked = True
        rblSingle_CheckedChanged(Nothing, Nothing)
        dt = Nothing
        dt1 = Nothing

        GvTechnicalSYS3.DataSource = Nothing
        GvTechnicalSYS4.DataSource = Nothing

        ddlBillCountry.SelectedIndex = 0
        ddlBillState.SelectedIndex = 0
        ddlBillDistrict.DataSource = Nothing
        ddlBillCity1.DataSource = Nothing


    End Sub

    Private Sub ddlEnqType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEnqType.SelectionChangeCommitted
        txtEnqNo.Text = ""
        If ddlQtnType.Text = "New" Then
            Dim Enq_ID As Integer
            Enq_ID = ddlEnqType.SelectedValue
            Dim enq_no = linq_obj.SP_Get_MaxEnqNoByEnqID(Enq_ID).ToList()
            For Each item As SP_Get_MaxEnqNoByEnqIDResult In enq_no
                txtEnqNo.Text = item.EnqNo
            Next
            txtName.Focus()
        Else
            txtEnqNo.Focus()

        End If
    End Sub


    Private Sub txtmobileno_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmobileno.Leave

        If ddlQtnType.Text.Trim() <> "Rev" Then
            If txtmobileno.Text.Trim() <> "" Then
                Dim Mobile = linq_obj.SP_CheckDuplicateMobile(txtmobileno.Text.Trim())
                If Mobile = 1 Then
                    MessageBox.Show("Mobile No Alredy Exists")

                End If
            End If
        End If
    End Sub

    Private Sub txtEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.Leave
        If ddlQtnType.Text.Trim() <> "Rev" Then
            If txtEmail.Text.Trim() <> "" Then
                Dim Email = linq_obj.SP_CheckEmailDuplicate(txtEmail.Text.Trim())
                If Email = 1 Then
                    MessageBox.Show("Email alredy exists")

                End If
            End If
        End If

    End Sub

    Private Sub GvSalesExecutiveData_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSalesExecutiveData.DoubleClick

        cleartext()
        Fk_SalesExecutiveQtnID = Convert.ToInt32(Me.GvSalesExecutiveData.SelectedCells(0).Value)
        GetData()
    End Sub

    Public Sub GetData()

        btnSave1.Text = "Update"

        Dim data = linq_obj.SP_Get_SalesExecutiveQuotationByAddressID(Fk_SalesExecutiveQtnID).ToList()
        For Each item As SP_Get_SalesExecutiveQuotationByAddressIDResult In data
            ddlQtnType.Text = item.QtnType
            ddlEnqType.SelectedValue = item.FK_EnqTypeID

            Address_ID = item.Pk_AddressID
            ddlPriority.Text = item.Priority
            txtDate.Text = item.CreatedDate
            txtEnqNo.Text = item.EnqNo
            txtName.Text = item.Name
            txtAddress.Text = item.Address
            txtArea.Text = item.Area



            lblState.Text = item.DeliveryState
            lblDistrictName.Text = item.District
            lblCity.Text = item.City



            ddlBillCountry.Text = item.Country
            ddlBillCountry_SelectionChangeCommitted(Nothing, Nothing)
            ddlBillState.Text = item.BillState
            ddlBillState_SelectionChangeCommitted(Nothing, Nothing)
            ddlBillCity1.Text = item.City
            ddlBillCity1_SelectionChangeCommitted(Nothing, Nothing)
            ddlBillDistrict.Text = item.District


            ddlBillCity1.Text = item.City
            txtPincode.Text = item.Pincode
            txtTaluka.Text = item.Taluka
            txtLandlineNo.Text = item.LandlineNo
            txtEmail.Text = item.EmailID
            ddlQType.Text = item.QuotationType
            txtRemark.Text = item.Remark
            txtDescription.Text = item.Description
            txtmobileno.Text = item.MobileNo
            ddlthrough.Text = item.Through
            txtReference.Text = item.Reference1
            txtReference2.Text = item.Reference2
            ddlQType_SelectionChangeCommitted(Nothing, Nothing)
            'check syste1 or system 2
            If item.System1.Trim() = "YES" Then
                chksystem1.Checked = True
            Else
                chksystem1.Checked = False

            End If

            If item.System2.Trim() = "YES" Then
                chksystem2.Checked = True
            Else
                chksystem2.Checked = False

            End If

            If item.QuotationType = "ISI" Then
                If IsNothing(dt) Then
                    'System 3
                    dt = New DataTable()
                    dt.Columns.Add("Select", GetType(Boolean))
                    dt.Columns.Add("SrNo", GetType(Int32))
                    dt.Columns.Add("Category", GetType(String))
                    dt.Columns.Add("Capacity", GetType(String))
                End If
                'System 4
                If IsNothing(dt1) Then
                    dt1 = New DataTable()
                    dt1.Columns.Add("Select", GetType(Boolean))
                    dt1.Columns.Add("SrNo", GetType(Int32))
                    dt1.Columns.Add("Category", GetType(String))
                End If
            Else
                'Non ISI /RO 
                If IsNothing(dt) Then
                    dt = New DataTable()
                    dt.Columns.Add("Select", GetType(Boolean))
                    dt.Columns.Add("SrNo", GetType(Int32))
                    dt.Columns.Add("Category", GetType(String))
                End If
            End If


            If item.Capacity2 <> "" Then
                rblMultiple.Checked = True
                txtCapacity1.Text = item.Capacity1
                txtCapacity2.Text = item.Capacity2

            Else
                rblSingle.Checked = True
                txtCapacity1.Text = item.Capacity1

            End If

            If item.QuotationType = "ISI" Then
                'System 3 Data

                If item.MainCategory = "System 3" Then
                    dt.Rows.Add(1, item.SNo, item.TechnicalData, item.Capacity)
                End If

                'System 4 Data

                If item.MainCategory = "System 4" Then
                    dt1.Rows.Add(1, item.SNo, item.TechnicalData)
                End If

            Else
                'NON ISI/RO 
                If rblSingle.Checked = True Then
                    dt.Rows.Add(1, item.SNo, item.TechnicalData)
                Else
                    dt.Rows.Add(1, item.SNo, item.TechnicalData)
                End If

            End If


        Next

        GvTechnicalSYS3.DataSource = Nothing
        GvTechnicalSYS4.DataSource = Nothing

        'System 3

        If IsNothing(dt) Then
        Else
            GvTechnicalSYS3.DataSource = dt
            GvTechnicalSYS3.Columns(0).Width = 40
            GvTechnicalSYS3.Columns(1).Width = 25

        End If
        'System 4
        If IsNothing(dt1) Then
        Else
            GvTechnicalSYS4.DataSource = dt1
            GvTechnicalSYS4.Columns(0).Width = 40
            GvTechnicalSYS4.Columns(1).Width = 25

        End If
        GetTechnicalData_Autocomplated()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        cleartext()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criteria As String

        If txtSearchEnQ.Text.Trim() <> "" And txtSearchName.Text.Trim() <> "" Then
            criteria = "where Fk_UserId=" & Class1.global.UserID & " and"
        Else
            criteria = "where Fk_UserId=" & Class1.global.UserID & " and"
        End If


        If txtSearchEnQ.Text.Trim() <> "" Then
            criteria = criteria + " AM.EnqNo like '%" + txtSearchEnQ.Text + "%' and"
        End If
        If txtSearchName.Text.Trim() <> "" Then
            criteria = criteria + " AM.Name like '%" + txtSearchName.Text + "%' and"
        End If
        If txtSearchStatus.Text.Trim() <> "" Then
            criteria = criteria + " SA.Status like '%" + txtSearchStatus.Text.Trim() + "%' and"
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If

        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_SalesExecutiveQuotationBySales_ID_ByCriteria"
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
        Dim objclass As New Class1

        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvAddress.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim dt As New DataTable
            dt.Columns.Add("Fk_SalesExecutiveQtnID")
            dt.Columns.Add("EnqNo")
            dt.Columns.Add("To")
            dt.Columns.Add("Status")
            Dim Total_Pending As Integer
            Dim Total_Done As Integer
            Dim Total_Cancel As Integer
            Total_Pending = 0
            Total_Done = 0
            Total_Cancel = 0

            For index = 0 To ds.Tables(1).Rows.Count - 1
                If ds.Tables(1).Rows(index)("Status") = "Pending" Then
                    Total_Pending = Total_Pending + 1
                ElseIf ds.Tables(1).Rows(index)("Status") = "Done" Then
                    Total_Done = Total_Done + 1
                Else
                    Total_Cancel = Total_Cancel + 1
                End If
                dt.Rows.Add(ds.Tables(1).Rows(index)("Pk_SalesExecutiveQtnID"), ds.Tables(1).Rows(index)("EnqNo"), ds.Tables(1).Rows(index)("ToUSer"), ds.Tables(1).Rows(index)("Status"))
            Next

            GvSalesExecutiveData.DataSource = dt

            txtTotalRecord.Text = ds.Tables(1).Rows.Count
            txtTotalPending.Text = Total_Pending.ToString()
            txtTotalDone.Text = Total_Done.ToString()
            txtTotalCancel.Text = Total_Cancel.ToString()
            GvSalesExecutiveData.Columns(0).Visible = False
        End If
    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave

        If ddlQtnType.Text = "Rev" Then
            If (txtEnqNo.Text <> "") Then
                If ddlEnqType.SelectedValue = 1 Then

                    If txtEnqNo.Text.Contains("D") Then
                        If (txtEnqNo.Text <> "") Then
                            Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text.Trim()).ToList()
                            If (data.Count > 0) Then
                                Address_ID = data(0).Pk_AddressID
                                bindAddressData()
                            End If

                        End If
                    Else
                        MessageBox.Show("Wrong Domestic No...")
                    End If

                End If
                If ddlEnqType.SelectedValue = 2 Then
                    If txtEnqNo.Text.Contains("B") Then
                        If (txtEnqNo.Text <> "") Then
                            Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text.Trim()).ToList()
                            If (data.Count > 0) Then
                                Address_ID = data(0).Pk_AddressID
                                bindAddressData()
                            End If

                        End If
                    Else
                        MessageBox.Show("Wrong B2B No...")
                    End If
                End If
                If ddlEnqType.SelectedValue = 3 Then
                    If txtEnqNo.Text.Contains("E") Then
                        If (txtEnqNo.Text <> "") Then
                            Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text.Trim()).ToList()
                            If (data.Count > 0) Then
                                Address_ID = data(0).Pk_AddressID
                                bindAddressData()
                            End If

                        End If
                    Else
                        MessageBox.Show("Wrong Export No...")
                    End If
                End If
                If ddlEnqType.SelectedValue = 4 Then
                    If txtEnqNo.Text.Contains("S") Then
                        If (txtEnqNo.Text <> "") Then
                            Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text.Trim()).ToList()
                            If (data.Count > 0) Then
                                Address_ID = data(0).Pk_AddressID
                                bindAddressData()
                            End If

                        End If
                    Else
                        MessageBox.Show("Wrong Spare No...")
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub bindAddressData()
        lblState.Text = ""
        lblDistrictName.Text = ""
        lblCity.Text = ""
        Dim Claient = linq_obj.SP_Get_AddressListBy_SalesExecutive_Assign_EnqNo(txtEnqNo.Text.Trim(), Class1.global.UserID).ToList()
        If Claient.Count > 0 Then
            For Each item As SP_Get_AddressListBy_SalesExecutive_Assign_EnqNoResult In Claient
                txtName.Text = item.Name
                Address_ID = item.Pk_AddressID
                txtAddress.Text = item.Address

                lblState.Text = item.DeliveryState
                lblDistrictName.Text = item.District
                lblCity.Text = item.City


                ddlBillCountry.Text = item.Country
                ddlBillCountry_SelectionChangeCommitted(Nothing, Nothing)
                ddlBillState.Text = item.BillState
                ddlBillState_SelectionChangeCommitted(Nothing, Nothing)
                ddlBillCity1.Text = item.City
                ddlBillCity1_SelectionChangeCommitted(Nothing, Nothing)
                ddlBillDistrict.Text = item.District
 
                txtTaluka.Text = item.Taluka
                txtPincode.Text = item.Pincode
                txtArea.Text = item.Area
                txtLandlineNo.Text = item.LandlineNo
                txtmobileno.Text = item.MobileNo
                txtEmail.Text = item.EmailID
                txtEmail1.Text = item.EmailID
                txtReference.Text = item.Reference1
                txtReference2.Text = item.Reference2
            Next
        Else

            Dim Claient1 = linq_obj.SP_Get_AddressListByEnqNo(txtEnqNo.Text.Trim()).ToList()
            For Each item As SP_Get_AddressListByEnqNoResult In Claient1
                txtName.Text = item.Name
                Address_ID = item.Pk_AddressID


                lblState.Text = item.DeliveryState
                lblDistrictName.Text = item.District
                lblCity.Text = item.City


                ddlBillCountry.Text = item.Country
                ddlBillCountry_SelectionChangeCommitted(Nothing, Nothing)
                ddlBillState.Text = item.BillState
                ddlBillState_SelectionChangeCommitted(Nothing, Nothing)
                ddlBillCity1.Text = item.City
                ddlBillCity1_SelectionChangeCommitted(Nothing, Nothing)
                ddlBillDistrict.Text = item.District

               
            Next
        End If

    End Sub

    Private Sub txtEmail1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail1.Leave
        If ddlQtnType.Text.Trim() <> "Rev" Then
            If txtEmail1.Text.Trim() <> "" Then
                Dim Email = linq_obj.SP_CheckEmailDuplicate(txtEmail1.Text.Trim())
                If Email = 1 Then
                    MessageBox.Show("Email alredy exists")

                End If
            End If
        End If

    End Sub

    Private Sub ddlQtnType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlQtnType.SelectionChangeCommitted
        If ddlQtnType.Text = "New" Then
            txtEnqNo.Enabled = False
            txtName.Focus()
        Else
            txtEnqNo.Enabled = True
            txtEnqNo.Text = ""
            txtEnqNo.Focus()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GvSalesExecutive_Data()
    End Sub

    Private Sub txtCapacity1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCapacity1.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If

    End Sub

    Private Sub txtCapacity2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCapacity2.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub chkALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkALL.CheckedChanged

        GvSalesExecutive_Data()


    End Sub

    Private Sub ddlBillState_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBillState.SelectionChangeCommitted

        ddlBillCity_Bind() 
    End Sub

    Private Sub ddlBillDistrict_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBillDistrict.SelectionChangeCommitted

    End Sub

    Private Sub ddlBillCountry_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBillCountry.SelectionChangeCommitted
        ddlBillState_Bind()
    End Sub


   
    Private Sub ddlBillCity1_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBillCity1.SelectionChangeCommitted
        ddlBillDistrict_Bind()
    End Sub
End Class