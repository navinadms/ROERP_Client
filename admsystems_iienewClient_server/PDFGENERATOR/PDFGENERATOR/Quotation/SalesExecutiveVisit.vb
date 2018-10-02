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
Imports CrystalDecisions.CrystalReports.Engine

Public Class SalesExecutiveVisit
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
    Shared Pk_VisitID As Integer
    Shared Pk_OtherID As Integer

    Public capacityType As String

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
        'GvSalesExecutive_Visit_Other_Data()
        rblVisit.Checked = True

        rblVisit_CheckedChanged(Nothing, Nothing)
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

    Public Sub GvSalesExecutive_Visit_Other_Data()

        'For sales Executive
        Dim dt As New DataTable
        dt.Columns.Add("Pk_VisitID")
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
        If rblVisit.Checked = True Then

            If Class1.global.UserID = 75 Then
                'Assign USer 
                If chkALL.Checked = True Then
                    'All Data Pending,Done,Cancel
                    Dim data = linq_obj.SP_Get_SalesExecutive_Visit_List_FromUserID(1, Class1.global.UserID).ToList()
                    For Each item As SP_Get_SalesExecutive_Visit_List_FromUserIDResult In data
                        If item.Status = "Pending" Then
                            Total_Pending = Total_Pending + 1
                        ElseIf item.Status = "Done" Then
                            Total_Done = Total_Done + 1
                        Else
                            Total_Cancel = Total_Cancel + 1

                        End If
                        dt.Rows.Add(item.Pk_VisitID, item.EnqNo, item.Status, item.UserName, item.Priority)
                    Next
                    txtTotalRecord.Text = data.Count()
                Else
                    'Only Pending
                    Dim data = linq_obj.SP_Get_SalesExecutive_Visit_List_FromUserID(1, Class1.global.UserID).ToList().Where(Function(p) p.Status <> "Done" And p.Status <> "Cancel").ToList()
                    For Each item As SP_Get_SalesExecutive_Visit_List_FromUserIDResult In data
                        If item.Status = "Pending" Then
                            Total_Pending = Total_Pending + 1
                        ElseIf item.Status = "Done" Then

                            Total_Done = Total_Done + 1
                        Else
                            Total_Cancel = Total_Cancel + 1

                        End If
                        dt.Rows.Add(item.Pk_VisitID, item.EnqNo, item.Status, item.UserName, item.Priority)
                    Next
                    txtTotalRecord.Text = data.Count()
                End If

                GroupVisitStatus.Visible = True
                GvSalesExecutiveData.DataSource = Nothing
                GvSalesExecutiveData.DataSource = dt
                GvSalesExecutiveData.Columns(0).Visible = False
                txtVremark.ReadOnly = False

            Else
                'Sales Executive 


                If chkALL.Checked = True Then
                    'All Data Pending,Done,Cancel
                    Dim data = linq_obj.SP_Get_SalesExecutive_Visit_List_FromUserID(0, Class1.global.UserID).ToList()
                    For Each item As SP_Get_SalesExecutive_Visit_List_FromUserIDResult In data
                        If item.Status = "Pending" Then
                            Total_Pending = Total_Pending + 1
                        ElseIf item.Status = "Done" Then

                            Total_Done = Total_Done + 1
                        Else
                            Total_Cancel = Total_Cancel + 1

                        End If
                        dt.Rows.Add(item.Pk_VisitID, item.EnqNo, item.Status, item.UserName, item.Priority)
                    Next
                    txtTotalRecord.Text = data.Count()
                Else
                    'Only Pending Data
                    Dim data = linq_obj.SP_Get_SalesExecutive_Visit_List_FromUserID(0, Class1.global.UserID).ToList().Where(Function(p) p.Status <> "Done" And p.Status <> "Cancel").ToList()
                    For Each item As SP_Get_SalesExecutive_Visit_List_FromUserIDResult In data
                        If item.Status = "Pending" Then
                            Total_Pending = Total_Pending + 1
                        ElseIf item.Status = "Done" Then

                            Total_Done = Total_Done + 1
                        Else
                            Total_Cancel = Total_Cancel + 1

                        End If
                        dt.Rows.Add(item.Pk_VisitID, item.EnqNo, item.Status, item.UserName, item.Priority)
                    Next
                    txtTotalRecord.Text = data.Count()
                End If

                GroupVisitStatus.Visible = False
                txtVremark.ReadOnly = True

                GvSalesExecutiveData.DataSource = Nothing
                GvSalesExecutiveData.DataSource = dt
                GvSalesExecutiveData.Columns(0).Visible = False

            End If
        End If

        'OTHER Detail 

        Dim dt1 As New DataTable
        dt1.Columns.Add("Pk_OtherID")
        dt1.Columns.Add("Select", GetType(Boolean))
        dt1.Columns.Add("EnqNo")
        dt1.Columns.Add("Status")
        dt1.Columns.Add("To")
        dt1.Columns.Add("Priority")
        If rblOther.Checked = True Then
            If Class1.global.UserID = 4 Then
                'Assign user 1 
                If chkALL.Checked = True Then
                    Dim data1 = linq_obj.SP_Get_SalesExecutive_Other_List_FromUserID(1, Class1.global.UserID).ToList()
                    For Each item As SP_Get_SalesExecutive_Other_List_FromUserIDResult In data1

                        If item.Status = "Pending" Then
                            Total_Pending = Total_Pending + 1
                        ElseIf item.Status = "Done" Then

                            Total_Done = Total_Done + 1
                        Else
                            Total_Cancel = Total_Cancel + 1

                        End If
                        dt1.Rows.Add(item.Pk_OtherID, 0, item.EnqNo, item.Status, item.UserName, item.OtherPriority)
                    Next
                    txtTotalRecord.Text = data1.Count()
                Else
                    Dim data1 = linq_obj.SP_Get_SalesExecutive_Other_List_FromUserID(1, Class1.global.UserID).ToList().Where(Function(p) p.Status <> "Done" And p.Status <> "Cancel").ToList()

                    For Each item As SP_Get_SalesExecutive_Other_List_FromUserIDResult In data1
                        If item.Status = "Pending" Then
                            Total_Pending = Total_Pending + 1
                        ElseIf item.Status = "Done" Then

                            Total_Done = Total_Done + 1
                        Else
                            Total_Cancel = Total_Cancel + 1

                        End If
                        dt1.Rows.Add(item.Pk_OtherID, 0, item.EnqNo, item.Status, item.UserName, item.OtherPriority)
                    Next
                    txtTotalRecord.Text = data1.Count()

                End If

                GroupOtherStatus.Visible = True
                GvSalesExecutiveData.DataSource = Nothing
                GvSalesExecutiveData.DataSource = dt1
                GvSalesExecutiveData.Columns(0).Visible = False


            Else
                'Sales Executive entry 0

                If chkALL.Checked = True Then
                    Dim data1 = linq_obj.SP_Get_SalesExecutive_Other_List_FromUserID(0, Class1.global.UserID).ToList()
                    For Each item As SP_Get_SalesExecutive_Other_List_FromUserIDResult In data1
                        If item.Status = "Pending" Then
                            Total_Pending = Total_Pending + 1
                        ElseIf item.Status = "Done" Then

                            Total_Done = Total_Done + 1
                        Else
                            Total_Cancel = Total_Cancel + 1

                        End If
                        dt1.Rows.Add(item.Pk_OtherID, 0, item.EnqNo, item.Status, item.UserName, item.OtherPriority)
                    Next
                    txtTotalRecord.Text = data1.Count()
                Else

                    Dim data1 = linq_obj.SP_Get_SalesExecutive_Other_List_FromUserID(0, Class1.global.UserID).ToList().Where(Function(p) p.Status <> "Done" And p.Status <> "Cancel").ToList()
                    For Each item As SP_Get_SalesExecutive_Other_List_FromUserIDResult In data1
                        If item.Status = "Pending" Then
                            Total_Pending = Total_Pending + 1
                        ElseIf item.Status = "Done" Then

                            Total_Done = Total_Done + 1
                        Else
                            Total_Cancel = Total_Cancel + 1

                        End If
                        dt1.Rows.Add(item.Pk_OtherID, 0, item.EnqNo, item.Status, item.UserName, item.OtherPriority)
                    Next
                    txtTotalRecord.Text = data1.Count()
                End If

                GroupOtherStatus.Visible = False
                GvSalesExecutiveData.DataSource = Nothing
                GvSalesExecutiveData.DataSource = dt1
                GvSalesExecutiveData.Columns(0).Visible = False
            End If
        End If

        'Total ,Pending,Done,Cancel

        'Urgent Color 

        If rblOther.Checked = True Then 

            For index = 0 To GvSalesExecutiveData.RowCount - 1
                If (GvSalesExecutiveData.Rows(index).Cells(5).Value = "Urgent") Then
                    GvSalesExecutiveData.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                    GvSalesExecutiveData.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.White
                End If
                If (GvSalesExecutiveData.Rows(index).Cells(5).Value = "Courier") Then
                    GvSalesExecutiveData.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Green
                    GvSalesExecutiveData.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.White
                End If
            Next
            GvSalesExecutiveData.Columns(5).Visible = False
        Else

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
            GvSalesExecutiveData.Columns(4).Visible = False
        End If
        txtTotalPending.Text = Total_Pending.ToString()
        txtTotalDone.Text = Total_Done.ToString()
        txtTotalCancelNew.Text = Total_Cancel.ToString()

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
        dt.Rows.Add("Visit")
        dt.Rows.Add("Other")


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


        Dim dt4 As New DataTable
        ddlQuotationFor.Items.Clear()
        dt4.Columns.Add("Item")
        dt4.Rows.Add("MWP")
        dt4.Rows.Add("IND")
        dt4.Rows.Add("EXP")
        dt4.Rows.Add("ORD")
        dt4.Rows.Add("OTH")


        ddlQuotationFor.DataSource = dt4
        ddlQuotationFor.DisplayMember = "Item"
        ddlQuotationFor.ValueMember = "Item"

      



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
                 
            Case "EnqNo"
                TxtSearchEnqNo.AutoCompleteCustomSource.Clear() 
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("EnqNo").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    TxtSearchEnqNo.AutoCompleteCustomSource.Add(iteam.Result)
                Next


        End Select
    End Sub

    Private Sub ddlQType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlQType.SelectionChangeCommitted

        GroupVisitor.Visible = False
        GroupOther.Visible = False
        If ddlQType.Text = "Visit" Then
            GroupVisitor.Visible = True
            Tabcontrol.TabPages(0).Text = ddlQType.Text & " Detail"
        Else
            GroupOther.Visible = True
            GroupOther.Text = ""
            Tabcontrol.TabPages(0).Text = ddlQType.Text & " Quotation"

        End If


    End Sub

    Private Sub txtCapacity1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub btnSave1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave1.Click
        Dim FlagEnqStatus As Integer

        Try


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

                Dim VisitStatus As String
                Dim OtherStatus As String
                VisitStatus = ""
                OtherStatus = ""

                'address insert        
                If btnSave1.Text.Trim() = "Save" Then
                    Dim Enq_ID As Integer
                    Enq_ID = ddlEnqType.SelectedValue
                    Dim enq_no = linq_obj.SP_Get_MaxEnqNoByEnqID(Enq_ID).ToList()

                    For Each item As SP_Get_MaxEnqNoByEnqIDResult In enq_no
                        txtEnqNo.Text = item.EnqNo
                    Next

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
                    'update address data Sales Executive Only New Enq Update
                    If ddlQtnType.Text = "New" Then
                        Address_ID = linq_obj.SP_Insert_Update_Address_Master_for_SalesExecutive(Address_ID, ddlEnqType.SelectedValue, txtEnqNo.Text, 1, 13, txtName.Text.Trim(), txtAddress.Text.Trim(), txtArea.Text.Trim(),
                                                                                   ddlBillCity1.Text, txtPincode.Text, txtTaluka.Text, ddlBillDistrict.Text, ddlBillState.Text, "", txtLandlineNo.Text, txtmobileno.Text, txtEmail.Text, txtEmail1.Text, txtDate.Text, txtDate.Text, "1", txtReference.Text, txtReference2.Text, "New", "", "", txtRemark.Text, ddlBillCountry.Text, ddlBillState.Text)
                        linq_obj.SubmitChanges()
                    End If

                End If


                If ddlQType.Text = "Visit" Then
                    If Address_ID > 0 Then
                        If rblVPending.Checked = True Then
                            VisitStatus = "Pending"
                        ElseIf rblVDone.Checked = True Then
                            VisitStatus = "Done"
                        Else
                            VisitStatus = "Cancel"
                        End If

                        'insert sales executive visit data
                        If btnSave1.Text.Trim() = "Save" Then

                            linq_obj.Sp_Insert_Update_SalesExecutive_Visit(0, txtEnqNo.Text.Trim(), Address_ID, Class1.global.UserID, 75, ddlVType.Text.Trim(), txtVplant.Text.Trim(), txtVvalue.Text.Trim(), txtVLand.Text.Trim(), txtVcost.Text.Trim(), txtVBank.Text.Trim(), txtVtravelBy.Text.Trim(), txtVPickup.Text.Trim(), txtVhotel.Text.Trim(), txtVhardcopy.Text.Trim(), txtVqtnvalue.Text.Trim(), txtVvisittime.Text, txtVdescription.Text, txtVremark.Text, "Pending", txtVisitDate.Text, ddlPriority.Text, ddlthrough.Text, ddlQtnType.Text, txtVisitDoneDate.Text, System.DateTime.Now.ToString("dd/MM/yyyy"))
                            linq_obj.SubmitChanges()

                        Else
                            linq_obj.Sp_Insert_Update_SalesExecutive_Visit(Pk_VisitID, txtEnqNo.Text.Trim(), Address_ID, Class1.global.UserID, 75, ddlVType.Text.Trim(), txtVplant.Text.Trim(), txtVvalue.Text.Trim(), txtVLand.Text.Trim(), txtVcost.Text.Trim(), txtVBank.Text.Trim(), txtVtravelBy.Text.Trim(), txtVPickup.Text.Trim(), txtVhotel.Text.Trim(), txtVhardcopy.Text.Trim(), txtVqtnvalue.Text.Trim(), txtVvisittime.Text, txtVdescription.Text, txtVremark.Text, VisitStatus, txtVisitDate.Text, ddlPriority.Text, ddlthrough.Text, ddlQtnType.Text, txtVisitDoneDate.Text, System.DateTime.Now.ToString("dd/MM/yyyy"))
                            linq_obj.SubmitChanges()

                        End If
                        MessageBox.Show("Submit Sucessfully...")

                    End If
                End If

                If ddlQType.Text.Trim() = "Other" Then
                    If Address_ID > 0 Then
                        If rblOStatusPending.Checked = True Then
                            OtherStatus = "Pending"
                        ElseIf rblOStatusDone.Checked = True Then
                            OtherStatus = "Done"
                        Else
                            OtherStatus = "Cancel"
                        End If

                        'insert Other Data
                        If btnSave1.Text.Trim() = "Save" Then
                            linq_obj.SP_Insert_Update_SalesExecutive_Other(0, txtEnqNo.Text, Address_ID, Class1.global.UserID, 4, ddlQtnType.Text, ddlPriority.Text, ddlthrough.Text, txtOtherPlant.Text, txtOtherCapacity.Text, txtOtherDescription.Text, "Pending", ddlQuotationFor.Text, txtOtherDate.Text)
                            linq_obj.SubmitChanges()

                        Else
                            linq_obj.SP_Insert_Update_SalesExecutive_Other(Pk_OtherID, txtEnqNo.Text, Address_ID, Class1.global.UserID, Class1.global.UserID, ddlQtnType.Text, ddlPriority.Text, ddlthrough.Text, txtOtherPlant.Text, txtOtherCapacity.Text, txtOtherDescription.Text, OtherStatus, ddlQuotationFor.Text, txtOtherDate.Text)
                            linq_obj.SubmitChanges()
                        End If
                        MessageBox.Show("Submit Sucessfully...")


                    End If

                End If
                If btnSave1.Text.Trim() = "Save" Then
                    GvSalesExecutive_Visit_Other_Data()

                End If

            Else

                MessageBox.Show("Enq Type And EnqNo Not Match...")
                con1.Close()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())

        End Try
        cleartext()
    End Sub
    Private Sub btnAddClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddClear.Click
        cleartext()
        btnSave1.Text = "Save"

    End Sub

    Public Sub cleartext()

        'Address
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
        txtReference.Text = ""
        txtReference2.Text = ""


        'Visit

        txtEnqNo.Text = ""
        Address_ID = 0
        ddlVType.Text = ""
        txtVplant.Text = ""
        txtVvalue.Text = ""
        txtVLand.Text = ""
        txtVcost.Text = ""
        txtVBank.Text = ""
        txtVtravelBy.Text = ""
        txtVPickup.Text = ""
        txtVhotel.Text = ""
        txtVhardcopy.Text = ""
        txtVqtnvalue.Text = ""
        txtVvisittime.Text = ""
        txtVdescription.Text = ""
        txtVremark.Text = ""
        txtVisitDate.Text = ""
        rblVPending.Checked = True

        'Other

        txtOtherCapacity.Text = ""
        txtOtherPlant.Text = ""
        txtOtherDescription.Text = ""
        rblOStatusPending.Checked = True
        ddlBillCountry.SelectedIndex = 0
        ddlBillState.SelectedIndex = 0
        ddlBillCity1.DataSource = Nothing
        ddlBillDistrict.DataSource = Nothing

        lblState.Text = ""
        lblDistrictName.Text = ""
        lblCity.Text = ""

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
        If txtmobileno.Text.Trim() <> "" Then
            Dim Mobile = linq_obj.SP_CheckDuplicateMobile(txtmobileno.Text.Trim())
            If Mobile = 1 Then
                MessageBox.Show("Mobile No Alredy Exists")

            End If
        End If
    End Sub

    Private Sub txtEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.Leave
        If txtEmail.Text.Trim() <> "" Then
            Dim Email = linq_obj.SP_CheckEmailDuplicate(txtEmail.Text.Trim())
            If Email = 1 Then
                MessageBox.Show("Email alredy exists")

            End If
        End If
    End Sub

    Private Sub GvSalesExecutiveData_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSalesExecutiveData.DoubleClick

        cleartext()
        If rblVisit.Checked = True Then
            Pk_VisitID = Convert.ToInt32(Me.GvSalesExecutiveData.SelectedCells(0).Value)
            DisplaySalesExecutive_Visit_Data()
        Else
            Pk_OtherID = Convert.ToInt32(Me.GvSalesExecutiveData.SelectedCells(0).Value)
            DisplaySalesExecutive_Other_Data()
        End If


    End Sub

    Public Sub DisplaySalesExecutive_Visit_Data()

        btnSave1.Text = "Update"

        Dim data = linq_obj.SP_Get_SalesExecutive_Visit_List(Pk_VisitID).ToList()

        For Each item As SP_Get_SalesExecutive_Visit_ListResult In data

            ddlQtnType.Text = item.QtnType
            ddlEnqType.SelectedValue = item.FK_EnqTypeID
            ddlQtnType.Text = item.QtnType
            Address_ID = item.Pk_AddressID
            ddlPriority.Text = item.Priority
            txtDate.Text = item.Vcreatedate
            txtEnqNo.Text = item.EnqNo
            txtName.Text = item.Name
            txtAddress.Text = item.Address
            txtArea.Text = item.Area
            lblState.Text = item.State
            lblDistrictName.Text = item.District
            lblCity.Text = item.City

            ddlBillCountry.Text = item.Country
            ddlBillCountry_SelectionChangeCommitted(Nothing, Nothing)
            ddlBillState.Text = item.BillState
            ddlBillState_SelectionChangeCommitted(Nothing, Nothing)
            ddlBillDistrict.Text = item.District
            ddlBillDistrict_SelectionChangeCommitted(Nothing, Nothing)
            ddlBillCity1.Text = item.City

            ddlBillDistrict.Text = item.District
            txtPincode.Text = item.Pincode
            ddlBillCity1.Text = item.City
            txtTaluka.Text = item.Taluka
            txtLandlineNo.Text = item.LandlineNo
            txtEmail.Text = item.EmailID
            ddlQType.Text = "Visit"
            txtRemark.Text = item.VDescription
            txtmobileno.Text = item.MobileNo
            ddlthrough.Text = item.Through
            txtRemark.Text = item.Remark

            txtReference.Text = item.Reference1
            txtReference2.Text = item.Reference2


            Dim data_user = linq_obj.SP_Get_User_Master_By_UserID(item.Fk_From_UserID).ToList()
            For Each item_u As SP_Get_User_Master_By_UserIDResult In data_user
                txtSendBy.Text = item_u.UserName
            Next

            ddlQType_SelectionChangeCommitted(Nothing, Nothing)

            'Visit Data
            ddlVType.Text = item.Vtype
            txtVplant.Text = item.Plant
            txtVvalue.Text = item.Values1
            txtVLand.Text = item.Land
            txtVcost.Text = item.Const
            txtVBank.Text = item.Bank
            txtVtravelBy.Text = item.TravelBy
            txtVPickup.Text = item.Pickup
            txtVhotel.Text = item.Hotel
            txtVhardcopy.Text = item.Hardcopy
            txtVqtnvalue.Text = item.Qtnvalue
            txtVvisittime.Text = item.VisitTime
            txtVdescription.Text = item.VDescription
            txtVremark.Text = item.Vremark
            txtVisitDate.Text = item.VisitDate
            txtVisitDoneDate.Text = item.VisitDoneDate

            If item.VStatus = "Pending" Then
                rblVPending.Checked = True
            ElseIf item.VStatus = "Done" Then
                rblVDone.Checked = True
            Else
                rblVCancel.Checked = True

            End If
            'check syste1 or system 2
        Next



    End Sub
    Public Sub DisplaySalesExecutive_Other_Data()

        btnSave1.Text = "Update"

        Dim data = linq_obj.SP_Get_SalesExecutive_Other_List(Pk_OtherID).ToList()


        For Each item As SP_Get_SalesExecutive_Other_ListResult In data

            ddlQtnType.Text = item.QtnType
            ddlEnqType.SelectedValue = item.FK_EnqTypeID
            txtEnqNo.Text = item.EnqNo
            Address_ID = item.Pk_AddressID
            ddlPriority.Text = item.OtherPriority
            txtDate.Text = item.OtherCreateDate
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
            ddlBillDistrict.Text = item.District
            ddlBillDistrict_SelectionChangeCommitted(Nothing, Nothing)
            ddlBillCity1.Text = item.City 

            txtPincode.Text = item.Pincode
            txtTaluka.Text = item.Taluka
            txtLandlineNo.Text = item.LandlineNo
            txtEmail.Text = item.EmailID
            ddlQType.Text = "Other"
            'txtdescription.Text = item.VDescription
            txtmobileno.Text = item.MobileNo
            ddlthrough.Text = item.OtherThrough
            txtRemark.Text = item.Remark
            txtReference.Text = item.Reference1
            txtReference2.Text = item.Reference2

            'To User ID

            Dim data_user = linq_obj.SP_Get_User_Master_By_UserID(item.Fk_From_UserID).ToList()
            For Each item_u As SP_Get_User_Master_By_UserIDResult In data_user
                txtSendBy.Text = item_u.UserName
            Next

            ddlQType_SelectionChangeCommitted(Nothing, Nothing) 
            'Visit Data
            txtOtherDate.Text = item.OtherCreateDate
            txtOtherPlant.Text = item.OtherPlant
            txtOtherDescription.Text = item.OtherDescription
            txtOtherCapacity.Text = item.OtherCapacity
            ddlQuotationFor.Text = item.QuotationFor

            If item.OtherStatus = "Pending" Then
                rblOStatusPending.Checked = True
            ElseIf item.OtherStatus = "Done" Then
                rblOStatusDone.Checked = True
            Else
                rblOtherCancel.Checked = True

            End If


            'check syste1 or system 2
        Next



    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        cleartext()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criteria As String
        criteria = ""
        If rblVisit.Checked = True Then
            If Class1.global.UserID = 75 Then
                criteria = " where SV.Fk_To_UserID=" & Class1.global.UserID & "  and"
            Else
                criteria = " where SV.Fk_From_UserID=" & Class1.global.UserID & "  and"
            End If
        End If
        If rblOther.Checked = True Then
            If Class1.global.UserID = 4 Then
                criteria = " where SO.Fk_To_UserID=" & Class1.global.UserID & "  and"
            Else
                criteria = " where SO.Fk_From_UserID=" & Class1.global.UserID & "  and"
            End If
        End If

        If txtSearchEnQ.Text.Trim() <> "" Then
            criteria = criteria + " AM.EnqNo like '%" + txtSearchEnQ.Text + "%' and"
        End If
        If txtSearchName.Text.Trim() <> "" Then
            criteria = criteria + " AM.Name like '%" + txtSearchName.Text + "%' and"
        End If

        If txtSearchStatus.Text.Trim() <> "" Then
            If rblVisit.Checked = True Then
                criteria = criteria + " SV.Status like '%" + txtSearchStatus.Text + "%' and"
            Else

                criteria = criteria + " SO.Status like '%" + txtSearchStatus.Text + "%' and"
            End If
        End If

        If txtSearchSendBy.Text.Trim() <> "" Then
            criteria = criteria + " UM.UserName like '%" + txtSearchSendBy.Text + "%' and"
        End If


        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If

        Dim cmd As New SqlCommand

        'Visit Search
        If rblVisit.Checked = True Then
            cmd.CommandText = "SP_Get_SalesExecutive_Visit_List_From_To_UserIDAs_Criteria"
            If Class1.global.UserID = 75 Then
                cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 0 'Assign user
            Else
                cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 1 'Sales Executive Entry user 
            End If
            cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
        End If


        'Other Search

        If rblOther.Checked = True Then
            cmd.CommandText = "SP_Get_SalesExecutive_Other_List_From_To_UserID_Criteria"
            If Class1.global.UserID = 75 Then
                cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 0 'Assign Default user
            Else
                cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = 1 'Sales Executive Entry user 
            End If
            cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
        End If

        Dim objclass As New Class1
        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvSalesExecutiveData.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim dt As New DataTable
            dt.Columns.Add("Pk_VisitID")
            dt.Columns.Add("Select", GetType(Boolean))
            dt.Columns.Add("EnqNo")
            dt.Columns.Add("Status")
            dt.Columns.Add("To")
            For index = 0 To ds.Tables(1).Rows.Count - 1
                dt.Rows.Add(ds.Tables(1).Rows(index)("Pk_VisitID"), 0, ds.Tables(1).Rows(index)("EnqNo"), ds.Tables(1).Rows(index)("Status"), ds.Tables(1).Rows(index)("UserName"))
            Next

            GvSalesExecutiveData.DataSource = dt
            GvSalesExecutiveData.Columns(0).Visible = False

        End If
    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        lblState.Text = ""
        lblDistrictName.Text = ""
        lblCity.Text = ""
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
        If txtEmail1.Text.Trim() <> "" Then
            Dim Email = linq_obj.SP_CheckEmailDuplicate(txtEmail1.Text.Trim())
            If Email = 1 Then

                MessageBox.Show("Email alredy exists")

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

    Private Sub rblVisit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblVisit.CheckedChanged

        GvSalesExecutive_Visit_Other_Data()

    End Sub

    Private Sub rblOther_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblOther.CheckedChanged


        GvSalesExecutive_Visit_Other_Data()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GvSalesExecutive_Visit_Other_Data()
    End Sub

    Private Sub chkALL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkALL.CheckedChanged
        GvSalesExecutive_Visit_Other_Data()
    End Sub

    Private Sub txtOtherDate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOtherDate.Leave
        Dim dd1 As Date, dd2 As Date
        Dim diff As Integer
        btnSave1.Enabled = True
        dd1 = txtOtherDate.Text
        dd2 = DateTime.Now.ToString()
        diff = DateDiff("d", dd1, dd2)
        If diff > 0 Then
            MsgBox("Date is not Less then Current Date....")
            btnSave1.Enabled = False

        End If
    End Sub

    Private Sub txtVisitDate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVisitDate.Leave
        Dim dd1 As Date, dd2 As Date
        Dim diff As Integer
        btnSave1.Enabled = True
        dd1 = txtVisitDate.Text
        dd2 = DateTime.Now.ToString()
        diff = DateDiff("d", dd1, dd2)
        If diff > 0 Then
            MsgBox("Date is not Less then Current Date....")
            btnSave1.Enabled = False

        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Pk_VisitID > 0 Then

            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Rpt_SalesExecutive_Visit"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@Pk_VisitID", SqlDbType.Int).Value = Pk_VisitID
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Rpt_SalesExecutive_Visit")
            Class1.WriteXMlFile(ds, "SP_Rpt_SalesExecutive_Visit", "SP_Rpt_SalesExecutive_Visit")
            Dim rpt As New ReportDocument
            rpt.Load(Application.StartupPath & "\Reports\Rpt_SalesExecutive_Visit.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables(0))
            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        Else
            MessageBox.Show("No Data Found!!!")
        End If
    End Sub

    Private Sub btnOtherPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtherPrint.Click
        If Pk_OtherID > 0 Then

            Dim ds As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Rpt_SalesExecutive_Other"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@Pk_OtherID", SqlDbType.Int).Value = Pk_OtherID
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Rpt_SalesExecutive_Other")
            Class1.WriteXMlFile(ds, "SP_Rpt_SalesExecutive_Other", "SP_Rpt_SalesExecutive_Other")
            Dim rpt As New ReportDocument
            rpt.Load(Application.StartupPath & "\Reports\Rpt_SalesExecutive_Other.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables(0))
            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        Else
            MessageBox.Show("No Data Found!!!")
        End If
    End Sub

    Private Sub GvSalesExecutiveData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvSalesExecutiveData.Click

        cleartext()
        If rblVisit.Checked = True Then
            Pk_VisitID = Convert.ToInt32(Me.GvSalesExecutiveData.SelectedCells(0).Value)
            DisplaySalesExecutive_Visit_Data()
        Else
            Pk_OtherID = Convert.ToInt32(Me.GvSalesExecutiveData.SelectedCells(0).Value)
            DisplaySalesExecutive_Other_Data()
        End If
    End Sub
    Private Sub ddlBillState_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBillState.SelectionChangeCommitted

        ddlBillCity_Bind()
        '        ddlBillDistrict_Bind()

    End Sub

    Private Sub ddlBillDistrict_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBillDistrict.SelectionChangeCommitted

    End Sub

    Private Sub ddlBillCountry_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBillCountry.SelectionChangeCommitted
        ddlBillState_Bind()
    End Sub


    
    Private Sub ddlBillCity1_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlBillCity1.SelectionChangeCommitted
        ddlBillDistrict_Bind()
    End Sub


    Private Sub btnQuotationAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuotationAssign.Click
        Try
            'insert Quotation Allotment 
            For i As Integer = 0 To GvSalesExecutiveData.Rows.Count - 1
                Dim status As Boolean = GvSalesExecutiveData.Rows(i).Cells(1).Value
                If status Then
                    linq_obj.SP_Update_SalesExecutive_Other_Allotment(Convert.ToInt64(GvSalesExecutiveData.Rows(i).Cells(0).Value), Convert.ToInt32(ddlUserAllotment.SelectedValue))
                    linq_obj.SubmitChanges()
                End If
            Next
            MessageBox.Show("Assign Sucessfully...")
            GvSalesExecutive_Visit_Other_Data()
        Catch ex As Exception

        End Try
    End Sub

End Class