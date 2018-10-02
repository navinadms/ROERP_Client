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


Public Class AddressForm
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim dataAddress As DataTable

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        ddlEnqType_Bind()
        ddlCategory_Bind()
        ddlSubCategory_Bind()
        ddlEnqStatus_Bind()
        ddlBillCountry_Bind()
        ddlBillState_Bind()


        GvAddress_Bind()
        If (Address_ID <= 0) Then
            Button_Status()
            btnSubmit.Visible = True
        Else
            Button_Status()
            btnUpdate.Visible = True
        End If
        RavSoft.CueProvider.SetCue(txtEmail1, "Email 1")
        RavSoft.CueProvider.SetCue(txtEmail2, "Community")
        ddlFilter.SelectedIndex = 0
        getPageRight()
    End Sub
    Public Sub AutoCompated_Text()
        getAutoCompleteData("Name")
        getAutoCompleteData("City")
        getAutoCompleteData("State")
        getAutoCompleteData("Area")
        getAutoCompleteData("District")
        getAutoCompleteData("EnqNo")
        getAutoCompleteData("TypeOfEnq")
    End Sub

    Public Sub ddlEnqType_Bind()
        ddlEnqType.Items.Clear()
        Dim Enq = linq_obj.SP_Get_EnqTypeList().ToList()
        ddlEnqType.DataSource = Enq
        ddlEnqType.DisplayMember = "EnqType"
        ddlEnqType.ValueMember = "Pk_EnqTypeID"
        ddlEnqType.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqType.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqType.AutoCompleteSource = AutoCompleteSource.ListItems
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


    'delivery Country State District City

    'Public Sub ddlDelCountry_Bind()
    '    Dim DelCountry = linq_obj.SP_Get_Country_Master_List().ToList()

    '    ddlDelCountry.DataSource = DelCountry
    '    ddlDelCountry.DisplayMember = "CountryName"
    '    ddlDelCountry.ValueMember = "PK_Country_ID"
    '    ddlDelCountry.AutoCompleteMode = AutoCompleteMode.Append
    '    ddlDelCountry.DropDownStyle = ComboBoxStyle.DropDownList
    '    ddlDelCountry.AutoCompleteSource = AutoCompleteSource.ListItems
    'End Sub

    'Public Sub ddlDelState_Bind()

    '    Dim DelState = linq_obj.SP_Get_Add_State_Master_List(Convert.ToInt32(ddlDelCountry.SelectedValue)).ToList()
    '    ddlDelState.DataSource = DelState
    '    ddlDelState.DisplayMember = "StateName"
    '    ddlDelState.ValueMember = "PK_Address_State_ID"
    '    ddlDelState.AutoCompleteMode = AutoCompleteMode.Append
    '    ddlDelState.DropDownStyle = ComboBoxStyle.DropDownList
    '    ddlDelState.AutoCompleteSource = AutoCompleteSource.ListItems

    'End Sub


    'Public Sub ddlDelDistrict_Bind()

    '    Dim DelDistrict = linq_obj.SP_Get_Add_District_Master_List(Convert.ToInt32(ddlDelState.SelectedValue)).ToList()
    '    ddlDelDistrict.DataSource = DelDistrict
    '    ddlDelDistrict.DisplayMember = "Add_District_Name"
    '    ddlDelDistrict.ValueMember = "PK_Add_District_ID"
    '    ddlDelDistrict.AutoCompleteMode = AutoCompleteMode.Append
    '    ddlDelDistrict.DropDownStyle = ComboBoxStyle.DropDownList
    '    ddlDelDistrict.AutoCompleteSource = AutoCompleteSource.ListItems
    'End Sub
    'Public Sub ddlDelCity_Bind()

    '    Dim DelCity2 = linq_obj.SP_Get_Add_City_Master_List(Convert.ToInt32(ddlDelDistrict.SelectedValue)).ToList()
    '    ddlDelCity2.DataSource = DelCity2
    '    ddlDelCity2.DisplayMember = "Add_CityName"
    '    ddlDelCity2.ValueMember = "PK_Add_City_Master_ID"
    '    ddlDelCity2.AutoCompleteMode = AutoCompleteMode.Append
    '    ddlDelCity2.DropDownStyle = ComboBoxStyle.DropDownList
    '    ddlDelCity2.AutoCompleteSource = AutoCompleteSource.ListItems
    'End Sub





    Public Sub ddlCategory_Bind()
        ddlCategory.Items.Clear()
        Dim category = linq_obj.SP_Get_AddressCategory().ToList()
        ddlCategory.DataSource = category
        ddlCategory.DisplayMember = "Category"
        ddlCategory.ValueMember = "Pk_AddressCategoryID"
        ddlCategory.AutoCompleteMode = AutoCompleteMode.Append
        ddlCategory.DropDownStyle = ComboBoxStyle.DropDownList
        ddlCategory.AutoCompleteSource = AutoCompleteSource.ListItems

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
            dataView.RowFilter = "([DetailName] like 'Address')"

            If (dataView.Count > 0) Then


                dv = dataView.ToTable()

                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            btnSubmit.Enabled = True
                        Else
                            btnSubmit.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsUpdate") = True) Then
                            btnUpdate.Enabled = True
                        Else
                            btnUpdate.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsDelete") = True) Then
                            btnDelete.Enabled = True
                        Else
                            btnDelete.Enabled = False
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

    Public Sub ddlSubCategory_Bind()

        ddlSubcategory.Items.Clear()

        Dim subcategory = linq_obj.SP_Get_AddSubCategory().ToList()

        ddlSubcategory.DataSource = subcategory
        ddlSubcategory.DisplayMember = "SubCategory"
        ddlSubcategory.ValueMember = "Pk_AddressSubID"
        ddlSubcategory.AutoCompleteMode = AutoCompleteMode.Append
        ddlSubcategory.DropDownStyle = ComboBoxStyle.DropDownList
        ddlSubcategory.AutoCompleteSource = AutoCompleteSource.ListItems


    End Sub
    Public Sub ddlEnqStatus_Bind()

        ddlEnqStatus.Items.Clear()
        Dim EnqStatus = linq_obj.SP_Get_EnqStatusList().ToList()
        ddlEnqStatus.DataSource = EnqStatus
        ddlEnqStatus.DisplayMember = "EnqStatus"
        ddlEnqStatus.ValueMember = "Pk_EnqStatus"
        ddlEnqStatus.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqStatus.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqStatus.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub GvAddress_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("PKID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")
        Try
            Dim getdata = linq_obj.SP_Get_AddressListForQuatation(0).ToList()
            If (getdata.Count > 0) Then
                'For Each item As SP_Get_AddressListForQuatationResult In getdata
                '    dt.Rows.Add(item.Pk_AddressID, item.EnqNo, item.Name)
                'Next
                GvAddress.DataSource = getdata
                GvAddress.Columns(0).Visible = False
                'Dim entryno = linq_obj.SP_Get_AddressList().Count()
                txtEntryNo.Text = getdata(0).Pk_AddressID + 1
                If (txtEntryNo.Text = "") Then
                    txtEntryNo.Text = 1
                End If
                txtTotalCount.Text = getdata.Count.ToString()
            Else
                If (txtEntryNo.Text = "") Then
                    txtEntryNo.Text = 1
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub Button_Status()
        btnSubmit.Visible = False
        btnUpdate.Visible = False
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If (txtName.Text.Trim() <> "" And txtEnqNo.Text.Trim() <> "" And txtReference.Text.Trim() <> "") Then
                txtEnqType.Text = ddlSubcategory.Text
                txtEnqAttendBy.Text = txtReference2.Text
                Dim res As Integer
                If (ddlEnqStatus.Text = "Enq") Then

                    res = linq_obj.SP_Insert_Update_Address_Master_New(0, Convert.ToInt64(ddlEnqType.SelectedValue), txtEnqNo.Text, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubcategory.SelectedValue), txtName.Text, txtAddress.Text, txtArea.Text, ddlBillCity1.Text, txtPincode.Text, txtTaluka.Text, ddlBillDistrict.Text, txtUser.Text, txtContactPerson.Text, txtLandlineNo.Text, txtmobileno.Text, txtEmail.Text, txtEmail1.Text, txtEmail2.Text, txtRemark.Text, txtEnqDate.Value, txtCreateDate.Value, ddlEnqStatus.SelectedValue, txtReference.Text, txtReference2.Text, txtEnqType.Text, txtEnqAttendBy.Text, txtenquiryfor.Text, txtCapacity.Text, txtDelAddress.Text, txtDelArea.Text, txtDelCity.Text, txtDelPincode.Text, txtDelTaluka.Text, txtDelDistrict.Text, txtDelState.Text, ddlBillCountry.Text, ddlBillState.Text)
                    linq_obj.SubmitChanges()
                    If (res > 0) Then

                        'Auto State Allotment


                        If txtReference.Text.Trim() = "JUSTDIAL" Then
                            'user JSD
                            linq_obj.SP_Tbl_UserAllotmentDetail_Insert(res, 172, 9)
                            linq_obj.SubmitChanges()
                        ElseIf txtReference.Text.Trim() = "TRADEBIRD" Then
                            'user TRB
                            linq_obj.SP_Tbl_UserAllotmentDetail_Insert(res, 175, 9)
                            linq_obj.SubmitChanges()
                        ElseIf txtReference.Text.Trim() = "TRADEXL" Then
                            'user  TRX
                            linq_obj.SP_Tbl_UserAllotmentDetail_Insert(res, 176, 9)
                            linq_obj.SubmitChanges()
                        ElseIf txtReference.Text.Trim() = "WEBBI" Or txtReference.Text.Trim() = "WEBIIE" Then
                            'user WEBBI
                            linq_obj.SP_Tbl_UserAllotmentDetail_Insert(res, 177, 10)
                            linq_obj.SubmitChanges()
                        Else
                            'default user CL
                            linq_obj.SP_Tbl_UserAllotmentDetail_Insert(res, 76, 16)
                            linq_obj.SubmitChanges()

                        End If

                        Dim resorder As Integer
                        If (txtEnqType.Text = "OC") Then
                            resorder = linq_obj.SP_Insert_Tbl_OrderOneMaster("", DateTime.Now.Date, "", txtEnqNo.Text, txtEnqDate.Value, "01-01-1900 00:00:00", txtName.Text, "", res, "", "", "", "01-01-1900 00:00:00")
                            If (resorder >= 0) Then
                                linq_obj.SubmitChanges()
                            End If
                        End If
                        GvAddress_Bind()
                        SetClean()
                        MessageBox.Show("Submit Sucessfully.....")
                        txtName.Focus()
                        ddlEnqType_SelectionChangeCommitted(Nothing, Nothing)
                    Else
                        MessageBox.Show("Error In Insertion.....")
                    End If
                Else
                    MessageBox.Show("Please Select EnqType.....")

                End If
            Else
                MessageBox.Show("Enq Number and Name are not blank,...")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub SetClean()
        txtEnqNo.Text = ""
        txtName.Text = ""
        txtAddress.Text = ""
        txtArea.Text = ""

        txtPincode.Text = ""
        txtTaluka.Text = ""

        txtUser.Text = ""
        txtDelState.Text = ""

        txtEmail1.Text = ""
        txtEmail2.Text = ""


        txtContactPerson.Text = ""
        txtLandlineNo.Text = ""
        txtEmail.Text = ""
        txtRemark.Text = ""
        txtEnqDate.Text = ""
        txtCreateDate.Text = ""
        txtmobileno.Text = ""
        txtReference.Text = ""
        txtReference2.Text = ""
        txtEnqType.Text = ""
        txtEnqAttendBy.Text = ""
        txtenquiryfor.Text = ""
        txtCapacity.Text = ""

        txtDelCountry.Text = ""
        txtDelState.Text = ""
        txtDelCity.Text = ""
        txtDelDistrict.Text = ""
        txtDelAddress.Text = ""
        txtDelArea.Text = ""
        txtDelPincode.Text = ""
        txtDelTaluka.Text = ""



        Address_ID = 0
        txtUser.SelectedIndex = 0
        ddlBillCountry.SelectedIndex = 0
        ddlBillState.SelectedIndex = 0
        ddlBillCity1.SelectedIndex = 0
        ddlBillDistrict.DataSource = Nothing
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If (txtName.Text.Trim() <> "" AndAlso txtEnqNo.Text.Trim() <> "") Then
                txtEnqType.Text = ddlSubcategory.Text
                txtEnqAttendBy.Text = txtReference2.Text
                linq_obj.SP_Insert_Update_Address_Master_New(Address_ID, Convert.ToInt32(ddlEnqType.SelectedValue), txtEnqNo.Text, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSubcategory.SelectedValue), txtName.Text, txtAddress.Text, txtArea.Text, ddlBillCity1.Text, txtPincode.Text, txtTaluka.Text, ddlBillDistrict.Text, txtUser.Text, txtContactPerson.Text, txtLandlineNo.Text, txtmobileno.Text, txtEmail.Text, txtEmail1.Text, txtEmail2.Text, txtRemark.Text, txtEnqDate.Value, txtCreateDate.Value, ddlEnqStatus.SelectedValue, txtReference.Text, txtReference2.Text, txtEnqType.Text, txtEnqAttendBy.Text, txtenquiryfor.Text, txtCapacity.Text, txtDelAddress.Text, txtDelArea.Text, txtDelCity.Text, txtDelPincode.Text, txtDelTaluka.Text, txtDelDistrict.Text, txtDelState.Text, ddlBillCountry.Text, ddlBillState.Text)

                linq_obj.SubmitChanges()
                GvAddress_Bind()
                SetClean()
                MessageBox.Show("Update Sucessfully.....")
                txtEnqNo.Focus()
            Else
                MessageBox.Show("Required Enq Number Or Name...")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub bindData()
        Try
            'lblHeader.Text = "Edit Address"
            'Dim id As Integer
            Button_Status()
            btnUpdate.Visible = True
            Dim address_get = linq_obj.SP_Get_AddressListById_New(Address_ID).ToList()
            For Each item As SP_Get_AddressListById_NewResult In address_get
                ddlEnqType.SelectedValue = item.FK_EnqTypeID
                ddlCategory.SelectedValue = item.FK_CategoryID
                ddlSubcategory.SelectedValue = item.FK_SubCategoryID
                txtEnqNo.Text = item.EnqNo
                txtName.Text = item.Name
                txtAddress.Text = item.Address
                txtArea.Text = item.Area

                Dim allotDetail = linq_obj.SP_Get_AllotedUser(Address_ID).ToList()
                If (allotDetail.Count > 0) Then
                    txtUser.Text = Convert.ToString(allotDetail(0).EnqAllotedTo)
                End If

                ddlBillCountry.Text = item.Country
                ddlBillCountry_SelectionChangeCommitted(Nothing, Nothing)
                ddlBillState.Text = item.BillState
                ddlBillState_SelectionChangeCommitted(Nothing, Nothing)
                ddlBillCity1.Text = item.City
                ddlBillCity1_SelectionChangeCommitted(Nothing, Nothing)
                ddlBillDistrict.Text = item.District


                txtPincode.Text = item.Pincode
                txtTaluka.Text = item.Taluka


                txtDelCountry.Text = item.Country
                txtDelDistrict.Text = item.DeliveryDistrict
                txtDelState.Text = item.DeliveryState
                txtDelCity.Text = item.DeliveryCity

                txtContactPerson.Text = item.ContactPerson
                txtLandlineNo.Text = item.LandlineNo
                txtmobileno.Text = item.MobileNo
                txtEmail.Text = item.EmailID
                txtRemark.Text = item.Remark
                txtEnqDate.Text = IIf(item.EnqDate Is Nothing, DateTime.Now, item.EnqDate)
                txtCreateDate.Text = IIf(item.CreateDate Is Nothing, DateTime.Now, item.CreateDate)

                ' id = item.EnqStatus
                ddlEnqStatus.SelectedValue = Convert.ToInt32(item.EnqStatus)


                txtEmail1.Text = item.EmailID1
                txtEmail2.Text = item.EmailID2
                txtDelAddress.Text = item.DeliveryAddress
                txtDelArea.Text = item.DeliveryArea

                txtDelPincode.Text = item.DeliveryPincode
                txtDelTaluka.Text = item.DeliveryTaluka
                txtReference.Text = item.Reference1
                txtReference2.Text = item.Reference2
                txtEnqType.Text = item.TypeOfEnq
                txtCapacity.Text = item.CapacityHour
                txtEnqAttendBy.Text = item.EnqAttendBy
                txtenquiryfor.Text = item.EnqFor




            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub GvAddress_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvAddress.DoubleClick
        Try
            Address_ID = Convert.ToInt32(Me.GvAddress.SelectedCells(0).Value)
            bindData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        SetClean()
        Button_Status()
        btnSubmit.Visible = True
        ddlCategory.Focus()
        'lblHeader.Text = "Add Address"
    End Sub



    Private Sub btnAddCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCategory.Click



        txtSearchAddCategory.Text += txtSearchSubCategory.Text.Trim() + ",".Trim()
        txtSearchSubCategory.Text = ""



    End Sub

    Private Sub btnclearCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclearCategory.Click
        txtSearchAddCategory.Text = ""
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

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
            Case "City"
                txtSearchCity.AutoCompleteCustomSource.Clear()
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("City").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    txtSearchCity.AutoCompleteCustomSource.Add(iteam.Result)
                Next


            Case "Area"
                txtArea.AutoCompleteCustomSource.Clear()
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("Area").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    txtArea.AutoCompleteCustomSource.Add(iteam.Result)
                Next
            Case "State"
                'Search State Auto complated 
                txtSearchState.AutoCompleteCustomSource.Clear()
                Dim S_data = linq_obj.SP_Get_AddressListAutoComplete("State").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In S_data
                    txtSearchState.AutoCompleteCustomSource.Add(iteam.Result)

                Next


                'txtState.Items.Clear()
                'txtState.DataSource = Nothing

                Dim datatable As New DataTable
                datatable.Columns.Add("UserName")
                Dim data = linq_obj.SP_Get_UserList().ToList()
                For Each item As SP_Get_UserListResult In data
                    datatable.Rows.Add(item.UserName)
                Next
                Dim newRow As DataRow = datatable.NewRow()

                newRow(0) = "Select"
                datatable.Rows.InsertAt(newRow, 0)
                txtUser.DataSource = datatable
                txtUser.DisplayMember = "UserName"
                txtUser.ValueMember = "UserName"
                txtUser.AutoCompleteMode = AutoCompleteMode.Append
                txtUser.DropDownStyle = ComboBoxStyle.DropDownList
                txtUser.AutoCompleteSource = AutoCompleteSource.ListItems


                Dim datatablenew As New DataTable
                datatablenew.Columns.Add("Result")


                Dim dataDel = linq_obj.SP_Get_AddressListAutoComplete("State").ToList()
                'For Each item As SP_Get_AddressListAutoCompleteResult In data
                '    datatablenew.Rows.Add(item.Result)
                'Next
                'Dim newRow1 As DataRow = datatablenew.NewRow()

                'newRow1(0) = "Select"
                'datatablenew.Rows.InsertAt(newRow1, 0)

                'txtDelState.DataSource = datatablenew
                'txtDelState.DisplayMember = "Result"
                'txtDelState.ValueMember = "Result"
                'txtDelState.AutoCompleteMode = AutoCompleteMode.Append
                'txtDelState.DropDownStyle = ComboBoxStyle.DropDownList
                'txtDelState.AutoCompleteSource = AutoCompleteSource.ListItems


            Case "EnqNo"
                TxtSearchEnqNo.AutoCompleteCustomSource.Clear()

                Dim data = linq_obj.SP_Get_AddressListAutoComplete("EnqNo").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    TxtSearchEnqNo.AutoCompleteCustomSource.Add(iteam.Result)
                Next
            Case "MobileNo"
                txtSearchMobile.AutoCompleteCustomSource.Clear()
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("MobileNo").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    txtSearchMobile.AutoCompleteCustomSource.Add(iteam.Result)
                Next
            Case "EmailID"
                txtSearchEmail.AutoCompleteCustomSource.Clear()
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("EmailID").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    txtSearchEmail.AutoCompleteCustomSource.Add(iteam.Result)
                Next
            Case "TypeOfEnq"
                txtSearchSubCategory.AutoCompleteCustomSource.Clear()
                Dim data = linq_obj.SP_Get_AddressListAutoComplete("TypeOfEnq").ToList()
                For Each iteam As SP_Get_AddressListAutoCompleteResult In data
                    txtSearchSubCategory.AutoCompleteCustomSource.Add(iteam.Result)
                Next
        End Select
    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        Dim resStatus As Integer = linq_obj.SP_CheckEnqNo(txtEnqNo.Text.Trim())
        If (resStatus = 1) Then
            btnSubmit.Visible = True
        Else
            MessageBox.Show("Enquiry Number Already Exists.")
            btnSubmit.Visible = False
        End If

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete Address?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                linq_obj.SP_Delete_Address(Address_ID)
                linq_obj.SubmitChanges()
                MessageBox.Show("Successfully Deleted...")
                SetClean()
                GvAddress_Bind()
                Button_Status()
                btnSubmit.Visible = True
                ddlCategory.Focus()
            Else

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim criteria As String

        criteria = " Where Address_Master.EnqStatus=1 and "

        If TxtSearchEnqNo.Text.Trim() <> "" Then
            criteria = criteria + " EnqNo like '%" + TxtSearchEnqNo.Text + "%' and"
        End If
        If txtSearchName.Text.Trim() <> "" Then
            criteria = criteria + " Name like '%" + txtSearchName.Text + "%' and"
        End If
        If txtSearchState.Text.Trim() <> "" Then
            criteria = criteria + " BillState like '%" + txtSearchState.Text + "%' and"
        End If
        If txtSearchDistrict.Text.Trim() <> "" Then
            criteria = criteria + " District like '%" + txtSearchDistrict.Text + "%' and"
        End If
        If txtSearchCity.Text.Trim() <> "" Then
            criteria = criteria + " City like '%" + txtSearchCity.Text + "%' and"
        End If
        If txtSearchMobile.Text.Trim() <> "" Then
            criteria = criteria + " MobileNo like '%" + txtSearchMobile.Text + "%' and"
        End If
        If txtSearchEmail.Text.Trim() <> "" Then
            criteria = criteria + " EmailID like '%" + txtSearchEmail.Text + "%' and"
        End If

        If ddlFilter.Text = "State Excluding Gujarat" Then
            criteria = criteria + " BillState <>'Gujarat' and"
        End If
        If ddlFilter.Text = "Gujarat Excluding Ahmedabad" Then
            criteria = criteria + " City<>'Ahmedabad' and BillState='Gujarat' and"
        End If
        If txtSearchAddCategory.Text.Trim() <> "" Then
            criteria = criteria + " TypeOfEnq in (SELECT value FROM dbo.fn_Split('" + SplitString(txtSearchAddCategory.Text.Trim()) + "',',')) and"
        End If

        If criteria = " Where Address_Master.EnqStatus=1 and " Then
            criteria = ""
        End If

        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If

        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Search_AddressMaster"
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
        Dim objclass As New Class1

        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            GvAddress.DataSource = Nothing

            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim dt As New DataTable
            dt.Columns.Add("PKID")
            dt.Columns.Add("EnqNo")

            dt.Columns.Add("Name")

            For index = 0 To ds.Tables(1).Rows.Count - 1
                dt.Rows.Add(ds.Tables(1).Rows(index)("Pk_AddressID"), ds.Tables(1).Rows(index)("EnqNo"), ds.Tables(1).Rows(index)("Name"))
            Next

            GvAddress.DataSource = dt
            txtTotalCount.Text = dt.Rows.Count.ToString()

        End If


    End Sub

    Private Sub AddressForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim criteria As String
        criteria = " and "
        If ddlFilter.Text = "State Excluding Gujarat" Then
            criteria = criteria + " BillState <>'Gujarat'  and"
        End If
        If ddlFilter.Text = "Gujarat Excluding Ahmedabad" Then
            criteria = criteria + " City<>'Ahmedabad' and BillState='Gujarat' and"
        End If
        If txtSearchAddCategory.Text.Trim() <> "" Then

            criteria = criteria + " a.TypeOfEnq in (SELECT value FROM dbo.fn_Split('" + SplitString(txtSearchAddCategory.Text.Trim()) + "',',')) and"

        End If

        If criteria = " and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If

        If (rb12Label.Checked = True) Then
            Dim ds As New DataSet
            Dim rpt As New ReportDocument
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Search_AddressPrint"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
            If (chkPrintDate.Checked = True) Then
                cmd.Parameters.AddWithValue("@start", DtStartDate.Value.Date)
            Else
                cmd.Parameters.AddWithValue("@start", "01/01/2010")
            End If
            cmd.Parameters.AddWithValue("@end", DtEndDate.Value.Date)
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Search_AddressPrint")
            Class1.WriteXMlFile(ds, "SP_Search_AddressPrint", "SP_Search_AddressPrint")
            rpt.Load(Application.StartupPath & "\Reports\AddressPrint12.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Search_AddressPrint"))
            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        ElseIf (rb21Label.Checked = True) Then
            Dim ds As New DataSet
            Dim rpt As New ReportDocument
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Search_AddressPrint"
            cmd.Connection = linq_obj.Connection
            If (chkPrintDate.Checked = True) Then
                cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
                cmd.Parameters.AddWithValue("@start", DtStartDate.Value.Date)
            Else
                cmd.Parameters.AddWithValue("@start", "01/01/2010")
            End If
            cmd.Parameters.AddWithValue("@end", DtEndDate.Value.Date)
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds, "SP_Search_AddressPrint")
            Class1.WriteXMlFile(ds, "SP_Search_AddressPrint", "SP_Search_AddressPrint")
            rpt.Load(Application.StartupPath & "\Reports\AddressPrint21.rpt")
            rpt.Database.Tables(0).SetDataSource(ds.Tables("SP_Search_AddressPrint"))

            Dim frmRpt As New FrmCommanReportView(rpt)
            frmRpt.Show()

        End If

    End Sub
    Function SplitString(ByVal str As String) As String
        If str.Equals("") Or str.Equals("All") Or str.Equals("All,") Then
            If (str.Equals("All,")) Then
                str = str.ToString().Substring(0, str.Length - 1)
            End If
            Return str
        End If
        Dim strfinal As String = ""
        Dim cnt As Integer
        cnt = str.Split(",").Length
        Dim tmpstr As [String]() = New [String](cnt) {}
        tmpstr = str.Split(",")
        ' strfinal = ""
        If cnt > 0 Then
            For index = 0 To tmpstr.Length - 1
                If tmpstr(index) <> "" Then
                    strfinal += tmpstr(index) + ","
                End If
            Next
            strfinal = strfinal.ToString().Substring(0, strfinal.Length - 1)
            ' strfinal += ""
        End If

        Return strfinal
    End Function

    Private Sub GvAddress_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvAddress.CellClick
        Try
            Address_ID = Convert.ToInt32(Me.GvAddress.SelectedCells(0).Value)
            bindData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GvAddress_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvAddress.CellContentClick
    End Sub

    Private Sub GvAddress_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GvAddress.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            Try
                Address_ID = Convert.ToInt32(Me.GvAddress.SelectedCells(0).Value)
                bindData()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GvAddress_Bind()
        txtSearchAddCategory.Text = ""
        txtSearchSubCategory.Text = ""
        txtSearchCity.Text = ""
        txtSearchDistrict.Text = ""
        TxtSearchEnqNo.Text = ""
        txtSearchName.Text = ""
        txtSearchState.Text = ""
        ddlFilter.SelectedIndex = 0
    End Sub

    Private Sub ddlEnqType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEnqType.SelectionChangeCommitted
        Dim Enq_ID As Integer
        Enq_ID = ddlEnqType.SelectedValue
        Dim enq_no = linq_obj.SP_Get_MaxEnqNoByEnqID(Enq_ID).ToList()
        For Each item As SP_Get_MaxEnqNoByEnqIDResult In enq_no
            txtEnqNo.Text = item.EnqNo
        Next
        txtName.Focus()
    End Sub
    Private Sub txtEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.Leave
        If txtEmail.Text.Trim() <> "" Then
            Dim Email = linq_obj.SP_CheckEmailDuplicate(txtEmail.Text.Trim())
            If Email = 1 Then
                MessageBox.Show("Email alredy exists")
            End If
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
    Private Sub txtEmail2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail2.Leave
    End Sub
    Private Sub txtmobileno_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmobileno.Leave
        If txtmobileno.Text.Trim() <> "" Then
            Dim Mobile = linq_obj.SP_CheckDuplicateMobile(txtmobileno.Text.Trim())
            If Mobile = 1 Then
                MessageBox.Show("Mobile No Alredy Exists")
            End If
        End If
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