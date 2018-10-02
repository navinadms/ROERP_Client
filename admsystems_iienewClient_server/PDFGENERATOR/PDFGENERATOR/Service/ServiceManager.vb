Imports System.IO
Imports System.Data.SqlClient

Public Class ServiceManager
    Dim imgQualityReportSrc As String
    Dim imgQualityReportPath As String
    Dim imgDoc1Src As String
    Dim imgDoc2Src As String
    Dim imgDoc3Src As String
    Dim imgDoc1Path As String
    Dim imgDoc2Path As String
    Dim imgDoc3Path As String
    Dim AddressId As Integer
    Dim ServiceId As Integer
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim tblEngFollowup As New DataTable
    Dim tblECDone As New DataTable
    Dim tblTPR As New DataTable
    Dim tblChkLog As New DataTable
    Dim tblSpareDetail As New DataTable
    Dim tblGenService As New DataTable
    Dim tblContactDetail As New DataTable
    'viraj
    Dim imgDocErrectionSrc As String
    Dim imgDocErrectionPath As String
    Dim tblPrDetail As New DataTable
    Dim tblQuality As New DataTable
    Dim tblWaterDetails As New DataTable
    Dim rwIDDelPartyReadyness As Integer = -1
    Dim rwIDDelErrection As Integer = -1
    Dim rwIDDelWaterDetail As Integer = -1
    Dim rwIDDelContectDetail As Integer = -1
    Dim b_EditDGEFDetails As Boolean
    Dim i_EditDGEFDetails As Integer
    Dim b_EditDGVECDoneDetail As Boolean
    Dim i_EditDGVECDoneDetail As Integer
    Dim b_EditDGEPFDetail As Boolean
    Dim i_EditDGEPFDetail As Integer
    Dim b_EditDGCheckLog As Boolean
    Dim i_EditDGCheckLog As Integer
    Dim b_EditDGVSpareDetails As Boolean
    Dim i_EditDGVSpareDetails As Integer
    Dim b_EditDGVGenService As Boolean
    Dim i_EditDGVGenService As Integer

    Enum PartyReadyness
        PartyReadyDate = 0
        HeaderName = 1
        Status = 2
        CompletionDate = 3
        Remarks = 4
    End Enum
    Enum Quality
        RawWater = 0
        Parameter = 1
        TreatedWater = 2
    End Enum

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoComplete_Text()
        GvInEnq_Bind()
        bindCombo()
        txtTotOrders.Text = Convert.ToString(DGVServiceDetails.Rows.Count)
        RavSoft.CueProvider.SetCue(txtEmail1, "Email 1")
        RavSoft.CueProvider.SetCue(txtEmail2, "Email 2")
    End Sub


    Public Sub getPageRight()
        Try
            Dim dv As New DataTable
            Dim dataView As New DataView

            Dim Class1 As New Class1
            Dim RowCount As Integer
            Dim statusCheck As Boolean = False
            Dim TabIndexNo As Integer
            Dim strName As String = ""
            Dim TabCountValue As Integer
            dataView = Class1.global.UserPermissionDataset.Tables(0).DefaultView
            dataView.RowFilter = "([Name] like 'Service Manager')"
            TabCountValue = TCServiceMaster.TabCount - 1
            dv = dataView.ToTable()

            If (dv.Rows(0)("Type") = 1) Then

            Else
                Dim indexTest As Integer = 0
                While indexTest <= TabCountValue
                    statusCheck = False
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("DetailName").ToString().ToUpper() = TCServiceMaster.TabPages(indexTest).Text.ToUpper()) Then
                            statusCheck = True
                            TabIndexNo = indexTest
                            Exit For
                        End If
                    Next
                    If statusCheck = False Then
                        TCServiceMaster.TabPages.RemoveAt(indexTest)
                        indexTest = -1
                        TabCountValue = TabCountValue - 1
                    End If
                    indexTest += 1
                End While
            End If
            'making a visible and invisible by their rights.
            For RowCount = 0 To dv.Rows.Count - 1
                If (dv.Rows(RowCount)("IsAdd") = True) Then
                    btnSave.Enabled = True
                Else
                    btnSave.Enabled = False
                End If
                If (dv.Rows(RowCount)("IsUpdate") = True) Then
                    btnChange.Enabled = True
                Else
                    btnChange.Enabled = False
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


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Private Sub ServiceManager_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = 13 Then
    '        SendKeys.Send("{TAB}")
    '    End If

    '    If e.KeyCode = 27 Then
    '        Me.Close()
    '    End If
    'End Sub


    Private Sub ServiceManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'add Columns To all Tables
        'EngFollowupTable
        tblEngFollowup.Columns.Add("Date")
        tblEngFollowup.Columns.Add("StartTime")
        tblEngFollowup.Columns.Add("EndTime")
        tblEngFollowup.Columns.Add("InstallationFor")
        tblEngFollowup.Columns.Add("WorkDone")
        tblEngFollowup.Columns.Add("isDelay")
        tblEngFollowup.Columns.Add("DelayRemarks")
        tblEngFollowup.Columns.Add("Remarks")

        'Added Service ECDone
        tblECDone.Columns.Add("InstallationType")
        tblECDone.Columns.Add("isDone")
        tblECDone.Columns.Add("DoneDate")

        'Added Technical Performance Report
        tblTPR.Columns.Add("EngineerName")
        tblTPR.Columns.Add("ReportBy")
        tblTPR.Columns.Add("PerformanceCategory")
        tblTPR.Columns.Add("Remarks")

        'Check Log Detail
        tblChkLog.Columns.Add("Title")
        tblChkLog.Columns.Add("isDone")
        tblChkLog.Columns.Add("Date")
        tblChkLog.Columns.Add("Remarks")

        '/ Spares Details
        tblSpareDetail.Columns.Add("ItemName")
        tblSpareDetail.Columns.Add("Qty")
        tblSpareDetail.Columns.Add("Price")
        tblSpareDetail.Columns.Add("Amount")


        'Added A Service FollowUp
        tblGenService.Columns.Add("FollowUp")
        tblGenService.Columns.Add("TentativeDate")
        tblGenService.Columns.Add("Status")
        tblGenService.Columns.Add("Remarks")

        tblPrDetail.Columns.Add("PartyReadyDate")
        tblPrDetail.Columns.Add("HeaderName")
        tblPrDetail.Columns.Add("Status")
        tblPrDetail.Columns.Add("CompletionDate")
        tblPrDetail.Columns.Add("Remarks")

        tblQuality.Columns.Add("RawWater")
        tblQuality.Columns.Add("Parameter")
        tblQuality.Columns.Add("TreatedWater")

        tblWaterDetails.Columns.Add("WaterType")
        tblWaterDetails.Columns.Add("Status")
        tblWaterDetails.Columns.Add("Remarks")

        tblContactDetail.Columns.Add("Name")
        tblContactDetail.Columns.Add("Post")
        tblContactDetail.Columns.Add("Contact")
        tblContactDetail.Columns.Add("Email")

        cmbService.DataSource = Nothing

        'get Permission Master
        getPageRight()
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

                                If (subcontrolthree.GetType() Is GetType(TextBox)) Then
                                    Dim textBox As TextBox = TryCast(subcontrolthree, TextBox)

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
    Public Sub bindCombo()

    End Sub
    Public Sub GvInEnq_Bind()
        Dim enq = linq_obj.SP_Select_Tbl_ServiceMaster(0).ToList()
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("EnqNo")
        dt.Columns.Add("Name")
        For Each item As SP_Select_Tbl_ServiceMasterResult In enq
            dt.Rows.Add(item.Pk_AddressID, item.EnqNo, item.Name)
        Next
        DGVServiceDetails.DataSource = dt
        DGVServiceDetails.Columns(0).Visible = False
        txtTotOrders.Text = Convert.ToString(DGVServiceDetails.Rows.Count)
    End Sub
    Public Sub AutoComplete_Text()
        Dim Getadd = linq_obj.SP_Get_AddressForService().ToList()
        For Each iteam As SP_Get_AddressForServiceResult In Getadd
            txtPartyName.AutoCompleteCustomSource.Add(iteam.Name)
            txtEntryNo.AutoCompleteCustomSource.Add(iteam.EnqNo)
            txtSEnqNo.AutoCompleteCustomSource.Add(iteam.EnqNo)
            txtParty.AutoCompleteCustomSource.Add(iteam.Name)
            txtcoPerson.AutoCompleteCustomSource.Add(iteam.ContactPerson)
            txtStation.AutoCompleteCustomSource.Add(iteam.Area)
            txtConatactno.AutoCompleteCustomSource.Add(iteam.MobileNo)
            txtEmail.AutoCompleteCustomSource.Add(iteam.EmailID)
        Next
        Dim dataorderService = linq_obj.SP_Select_Tbl_OrderServiceFollowUpDetailByOrder(AddressId).ToList()
        For Each item As SP_Select_Tbl_OrderServiceFollowUpDetailByOrderResult In dataorderService
            txtSearchComplainNo.AutoCompleteCustomSource.Add(item.ComplainNo)
        Next

        'WaterTreatment 
        txtWaterPoint.AutoCompleteCustomSource.Clear()
        Dim water = linq_obj.SP_Get_Autocomp_Physical_WaterTreatment_Master()
        For Each item As SP_Get_Autocomp_Physical_WaterTreatment_MasterResult In water
            txtWaterPoint.AutoCompleteCustomSource.Add(item.WaterType)
        Next


    End Sub
    Public Sub GetClientDetails_Bind()
        Try
            Dim Claient = linq_obj.SP_Get_AddressList().ToList().Where(Function(t) t.Pk_AddressID = Address_ID)
            For Each item As SP_Get_AddressListResult In Claient
                txtPartyName.Text = item.Name
                txtBillAdresss.Text = item.Address
                txtCity.Text = item.City
                txtState.Text = item.State
                txtDistrict.Text = item.District
                txtTaluka.Text = item.Taluka
                txtPincode.Text = item.Pincode
                txtArea.Text = item.Area
                txtcontctName.Text = item.ContactPerson
                txtContactNo.Text = item.MobileNo
                txtOrdermastEmail.Text = item.EmailID
                txtEntryNo.Text = item.EnqNo
                txtDeladress.Text = item.DeliveryAddress
                txtDelArea.Text = item.DeliveryArea
                txtDelCity.Text = item.DeliveryCity
                txtDelDistrict.Text = item.DeliveryDistrict
                txtDelPincode.Text = item.DeliveryPincode
                txtDelState.Text = item.DeliveryState
                txtDelTaluka.Text = item.DeliveryTaluka
                txtEmail1.Text = item.EmailID1
                txtEmail2.Text = item.EmailID2
                'dtEntryDate.Value = item.EnqDate
            Next
            'Get Enquiry Client Master Details
            Dim orderProjectDetail = linq_obj.SP_Select_Tbl_ProjectInformationMaster(Address_ID).ToList()
            For Each item As SP_Select_Tbl_ProjectInformationMasterResult In orderProjectDetail
                txtSPlanName.Text = Convert.ToString(item.PlantName)
                txtSCapacity.Text = Convert.ToString(item.Capacity)
                txtSModel.Text = Convert.ToString(item.Model)
                txtSScheme.Text = Convert.ToString(item.TreatmentScheme)
            Next



        Catch ex As Exception
            MessageBox.Show("Error :" + ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnBrowseQuality_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseQuality.Click
        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.ShowDialog()
        imgQualityReportSrc = openFileDialog1.FileName.ToString()
        imgQualityReportPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName

    End Sub
    Private Sub GroupBox16_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criteria As String
        criteria = " and"
        If txtParty.Text.Trim() <> "" Then
            criteria = criteria + " Name like '%" + txtParty.Text + "%' and"
        End If
        If txtcoPerson.Text.Trim() <> "" Then
            criteria = criteria + " ContactPerson like '%" + txtcoPerson.Text + "%' and"
        End If
        If txtStation.Text.Trim <> "" Then
            criteria = criteria + " Area like '%" + txtStation.Text + "%' and"
        End If
        'If txtInqSearchLandLineNo.Text.Trim() <> "" Then
        '    criteria = criteria + " LandlineNo like '%" + txtInqSearchLandLineNo.Text + "%' and"
        'End If
        If txtConatactno.Text.Trim() <> "" Then
            criteria = criteria + " MobileNo like '%" + txtConatactno.Text + "%' and"
        End If
        'If txtSearchComplainNo.Text.Trim() <> "" Then
        '    criteria = criteria + " f.Status like '%" + txtSearchComplainNo.Text + "%' and"
        'End If
        If txtEmail.Text.Trim() <> "" Then
            criteria = criteria + " EmailID like '%" + txtEmail.Text + "%' and"
        End If
        If txtSEnqNo.Text.Trim() <> "" Then
            criteria = criteria + " am.EnqNo like '%" + txtSEnqNo.Text.Trim() + "%' and"
        End If

        If criteria = " and " Then
            criteria = ""
        End If

        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If

        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_AddressForServiceCriteria"
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
        Dim objclass As New Class1

        Dim ds As DataSet
        ds = objclass.GetEnqReportData(cmd)
        If ds.Tables("Table").Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DGVServiceDetails.DataSource = Nothing
            ds.Dispose()
        Else
            DGVServiceDetails.DataSource = ds.Tables("Table")
            Me.DGVServiceDetails.Columns(0).Visible = False
            ds.Dispose()
        End If
        txtTotOrders.Text = Convert.ToString(DGVServiceDetails.Rows.Count)

    End Sub
    Public Sub BindAllGridData(ByVal sAddress_ID As Integer)
        Try
            clearAll()
            Address_ID = sAddress_ID
            GetClientDetails_Bind()
            BindAllData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub DGVServiceDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVServiceDetails.Click
        If DGVServiceDetails.SelectedRows.Count > 0 Then
            BindAllGridData(Convert.ToInt64(Me.DGVServiceDetails.SelectedCells(0).Value))
        End If
    End Sub
    
    Public Sub SaveAllData()

        Try
            'Save All Data
            If Address_ID > 0 Then
                'save main Order Details.

                'Insert into servicemaster

                Dim resSer As Integer
                resSer = linq_obj.SP_Insert_Update_Tbl_ServiceMaster(txtEntryNo.Text, Class1.SetDate(dtServicedate.Text), Address_ID, 1)
                linq_obj.SubmitChanges()

                
                'Update Contact Detail
                Dim resContact As Integer
                resContact = linq_obj.SP_UpdateAddressContactDetail(txtcontctName.Text, txtContactNo.Text, txtOrdermastEmail.Text, txtEmail1.Text, txtEmail2.Text, Address_ID)
                linq_obj.SubmitChanges()

                'bind Address Detail
                Dim resAddress As Integer
                resAddress = linq_obj.SP_UpdateAddress(txtPartyName.Text, txtBillAdresss.Text, txtArea.Text, txtCity.Text, txtPincode.Text, txtTaluka.Text, txtDistrict.Text, txtState.Text, txtDeladress.Text, txtDelArea.Text, txtDelCity.Text, txtDelPincode.Text, txtDelTaluka.Text, txtDelDistrict.Text, txtDelState.Text, Address_ID)
                If resAddress >= 0 Then
                    linq_obj.SubmitChanges()
                End If


                'Save AddressDetail
                'delete Old Data
                Dim delConatctDetail = linq_obj.SP_Tbl_Service_ContactInfo_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblContactDetail.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_ContactInfo_Insert(Address_ID,
                    tblContactDetail.Rows(i)("Name").ToString(),
                    tblContactDetail.Rows(i)("Post").ToString(),
                    tblContactDetail.Rows(i)("Contact").ToString(),
                   tblContactDetail.Rows(i)("Email").ToString())

                    linq_obj.SubmitChanges()
                Next


                'Save Folloup Details
                ''Delelte

                Dim resECEngDel As Integer
                resECEngDel = linq_obj.SP_Tbl_Service_ECEngRepMaster_Delete(Address_ID)
                linq_obj.SubmitChanges()

                Dim resECEng As Integer
                resECEng = linq_obj.SP_Tbl_Service_ECEngRepMaster_Insert(Address_ID, IIf(chkECDone.Checked, True, False), Convert.ToDateTime(If(dtECDone.Text = "", "01-01-1900", dtECDone.Text)))
                If (resECEng > 0) Then
                    linq_obj.SubmitChanges()
                    'delete Old Data
                    Dim delfollowDetail = linq_obj.SP_Tbl_Service_ECEngineerReportDetail_Delete(Address_ID)
                    linq_obj.SubmitChanges()

                    For i As Integer = 0 To tblEngFollowup.Rows.Count - 1
                        linq_obj.SP_Tbl_Service_ECEngineerReportDetail_Insert(Address_ID,
                                                                    Convert.ToDateTime(tblEngFollowup.Rows(i)("Date").ToString()),
                                                                    tblEngFollowup.Rows(i)("StartTime").ToString(), tblEngFollowup.Rows(i)("EndTime").ToString(),
                                                                    tblEngFollowup.Rows(i)("WorkDone").ToString(),
                                                                     Convert.ToBoolean(tblEngFollowup.Rows(i)("isDelay").ToString()),
                         tblEngFollowup.Rows(i)("DelayRemarks").ToString(),
                        tblEngFollowup.Rows(i)("InstallationFor").ToString(),
                        tblEngFollowup.Rows(i)("Remarks").ToString())
                        linq_obj.SubmitChanges()
                    Next
                End If


                linq_obj.SP_Tbl_Service_ECDoneDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblECDone.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_ECDoneDetail_Insert(tblECDone.Rows(i)("InstallationType").ToString(),
                                                                Convert.ToBoolean(tblECDone.Rows(i)("isDone").ToString()),
                                                                Convert.ToDateTime(tblECDone.Rows(i)("DoneDate").ToString()), Address_ID)
                Next
                linq_obj.SubmitChanges()



                'TPR
                linq_obj.SP_Tbl_Service_TechPerformanceRepDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()


                For i As Integer = 0 To tblTPR.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_TechPerformanceRepDetail_Insert(Address_ID, tblTPR.Rows(i)("EngineerName").ToString(),
                                                                tblTPR.Rows(i)("ReportBy").ToString(),
                                                               tblTPR.Rows(i)("PerformanceCategory").ToString(),
                                                               tblTPR.Rows(i)("Remarks").ToString())
                Next
                linq_obj.SubmitChanges()


                'save All service

                Dim resservice As Integer
                resservice = linq_obj.SP_Tbl_Service_ServiceDetail_Insert(Address_ID, dtServDelDate.Value, txtSerEngName.Text, txtSerRemarks.Text, txtITDS.Text, txtITH.Text, txtIPH.Text, txtIRemarks.Text, txtOTDS.Text, txtOTH.Text, txtOPH.Text, txtORemarks.Text, txtSerType.Text, IIf(chkServiceDone.Checked, True, False), Convert.ToDateTime(If(dtServiceDoneDate.Text = "", "01-01-1900", dtServiceDoneDate.Text)))
                linq_obj.SubmitChanges()


                linq_obj.SP_Tbl_Service_CheckLogDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblChkLog.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_CheckLogDetail_Insert(Address_ID, tblChkLog.Rows(i)("Title").ToString(),
                                                                tblChkLog.Rows(i)("isDone").ToString(),
                                                                Class1.SetDate(tblChkLog.Rows(i)("Date").ToString()),
                                                               tblChkLog.Rows(i)("Remarks").ToString())
                Next
                linq_obj.SubmitChanges()



                'Spare Detail

                linq_obj.SP_Tbl_Service_SpareMaster_Insert(Address_ID, Class1.SetDate(dtSpareDate.Text), txtSpareRecBy.Text, txtDisVia.Text, txtSpareEngName.Text, txtSpareRemarks.Text)
                linq_obj.SubmitChanges()

                linq_obj.SP_Tbl_Service_SpareDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblSpareDetail.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_SpareDetail_Insert(Address_ID, tblSpareDetail.Rows(i)("ItemName").ToString(),
                                                                Convert.ToInt32(tblSpareDetail.Rows(i)("Qty").ToString()),
                      Convert.ToDecimal(tblSpareDetail.Rows(i)("Price").ToString()),
                                                               Convert.ToDecimal(tblSpareDetail.Rows(i)("Amount").ToString()))
                    linq_obj.SubmitChanges()
                Next


                'Save General service Detail

                Dim resgenService As Integer
                resgenService = linq_obj.SP_Tbl_Service_GeneralServiceMaster_Insert(Class1.SetDate(dtGSDate.Text), txtSerType.Text, txtComplainNo.Text, Class1.SetDate(dtAttendDate.Text), txtAtendedBy.Text, txtEnginer.Text, Address_ID)
                linq_obj.SubmitChanges()

                linq_obj.SP_Tbl_Service_GeneralServiceDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblGenService.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_GeneralServiceDetail_Insert(Address_ID, tblGenService.Rows(i)("FollowUp").ToString(),
                                                                Class1.SetDate(tblGenService.Rows(i)("TentativeDate").ToString()),
                                                                tblGenService.Rows(i)("Status").ToString(),
                                                               tblGenService.Rows(i)("Remarks").ToString())
                Next
                linq_obj.SubmitChanges()



                'viraj

                'save main Order Details.
                Dim resorder As Integer
                resorder = linq_obj.SP_Tbl_Service_ProjectDetail_Insert(Address_ID, txtSMoc.Text, txtSType.Text, txtSInspectionBy.Text, Class1.SetDate(dtpSInspectionDate.Text))

                If (resorder > 0) Then
                    linq_obj.SubmitChanges()
                End If


                Dim resProjectId As Integer
                resProjectId = linq_obj.SP_Tbl_Service_PartyReadynessMaster_Insert(Address_ID, imgQualityReportPath)

                If (resProjectId > 0) Then
                    linq_obj.SubmitChanges()

                    Dim delfollowDetail = linq_obj.SP_Tbl_Service_PartyReadynessDetail_Delete(Address_ID)
                    linq_obj.SubmitChanges()

                    For i As Integer = 0 To tblPrDetail.Rows.Count - 1
                        With tblPrDetail
                            linq_obj.SP_Tbl_Service_PartyReadynessDetail_Insert(Address_ID,
                                                                    .Rows(i)("HeaderName").ToString(),
                                                                    .Rows(i)("Status").ToString(),
                                                                    Class1.SetDate(.Rows(i)("CompletionDate").ToString()),
                                                                    .Rows(i)("Remarks").ToString(), Convert.ToDateTime(.Rows(i)("PartyReadyDate").ToString())
                                                                    )
                            linq_obj.SubmitChanges()
                        End With
                    Next
                End If


                'Save All Erreaction Details

                Dim Errectionid As Integer
                Errectionid = linq_obj.SP_Tbl_Service_ErrectionCommMaster_Insert(Address_ID, t1.Text, n1.Text, t2.Text, n2.Text, t3.Text, n3.Text, t4.Text, n4.Text, t5.Text, n5.Text, imgDocErrectionPath, Class1.SetDate(dtpErrectionDate.Text), txtECRemarks.Text)

                If (Errectionid > 0) Then

                    linq_obj.SubmitChanges()
                    'delete Old Data
                    Dim delfollowDetail = linq_obj.SP_Tbl_Service_ErrectionCommDetails_Delete(Address_ID)
                    linq_obj.SubmitChanges()

                    For i As Integer = 0 To tblQuality.Rows.Count - 1
                        With tblQuality
                            linq_obj.SP_Tbl_Service_ErrectionCommDetails_Insert(Address_ID,
                                                .Rows(i)("RawWater").ToString(),
                                                .Rows(i)("Parameter").ToString(),
                                                .Rows(i)("TreatedWater").ToString())
                            linq_obj.SubmitChanges()
                        End With
                    Next
                End If


                'Save Water Detail

                Dim WaterID As Integer
                WaterID = linq_obj.SP_Tbl_Service_PhysicalVerificationDayOneMaster_Insert(Address_ID, chkBottle1.Checked, txtBottle1.Text, chkBottle2.Checked, txtBottle2.Text,
                chkBlow1.Checked, txtBlow1.Text, chkBlow2.Checked, txtBlow2.Text, chkLab1.Checked, txtLab1.Text, chkLab2.Checked, txtLab2.Text, chkOther1.Checked, txtOther1.Text, chkOther2.Checked, txtOther2.Text, txtpendingMaterials.Text)


                If (WaterID > 0) Then
                    linq_obj.SubmitChanges()

                    Dim delfollowDetail = linq_obj.SP_Tbl_Service_Physical_WaterTreatment_Master_Delete(Address_ID)
                    linq_obj.SubmitChanges()
                    For i As Integer = 0 To tblWaterDetails.Rows.Count - 1

                        linq_obj.SubmitChanges()
                        'delete Old Data

                        With tblWaterDetails
                            linq_obj.SP_Tbl_Service_Physical_WaterTreatment_Master_Insert(Address_ID,
                                                                    .Rows(i)("WaterType").ToString(),
                                                                    .Rows(i)("Status").ToString(),
                                                                    .Rows(i)("Remarks").ToString())
                            linq_obj.SubmitChanges()
                        End With
                    Next
                End If

                Dim RemarkID As Integer
                RemarkID = linq_obj.SP_Tbl_Service_PartyRemarksMaster_Insert(Address_ID, imgDoc1Path, imgDoc2Path, imgDoc3Path)
                If (RemarkID > 0) Then
                    linq_obj.SubmitChanges()
                End If

                MessageBox.Show("Successfully Saved")
                clearAll()
            Else
                MessageBox.Show("No Address Informations Found")
            End If

        Catch ex As Exception
            MessageBox.Show("Error :" + ex.Message)
        End Try
    End Sub
    Private Sub txtFolowup3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFolowup3.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If

    End Sub
    Private Sub txtQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress, txtPrice.KeyPress
        e.Handled = Class1.OnlyNumeric(sender, e)
    End Sub
    Private Sub txtPrice_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice.Leave
        txtAmount.Text = Convert.ToString(Convert.ToInt32(txtQty.Text) * Convert.ToDecimal(txtPrice.Text))
    End Sub
    Public Sub clearAll()
        Address_ID = 0
        cmbService.DataSource = Nothing
        dtServicedate.Text = ""
        tblEngFollowup.Clear()
        tblECDone.Clear()
        tblTPR.Clear()
        tblChkLog.Clear()
        tblSpareDetail.Clear()
        tblGenService.Clear()
        tblContactDetail.Clear()
        tblPrDetail.Clear()
        tblQuality.Clear()
        tblWaterDetails.Clear()
        dtGSDate.Text = ""
        txtSerType.Text = ""
        txtComplainNo.Text = ""
        ''dtAttendDate.Text = DateTime.Now.
        txtAtendedBy.Text = ""
        txtEnginer.Text = ""
        txtName.Text = ""
        txtPost.Text = ""
        txtClientContactNo.Text = ""
        txtClientEmail.Text = ""
        grpComplain.Visible = False

        lblComplainDate.Text = ""
        lblComplainDoneDate.Text = ""
        lblComplainEngName.Text = ""
        lblComplainStatus.Text = ""
        lblComplainType.Text = ""

        dtServDelDate.Text = DateTime.Now
        txtSerEngName.Text = ""
        txtSerRemarks.Text = ""
        txtITDS.Text = ""
        txtITH.Text = ""
        txtIPH.Text = ""
        txtIRemarks.Text = ""
        txtOTDS.Text = ""
        txtOTH.Text = ""
        txtOPH.Text = ""
        txtORemarks.Text = ""
        txtSerType.Text = ""
        chkServiceDone.Checked = False
        dtServiceDoneDate.Text = ""
        chkECDone.Checked = False
        dtECDone.Text = ""

        dtSpareDate.Text = ""
        txtSpareRecBy.Text = ""
        txtDisVia.Text = ""
        txtSpareEngName.Text = ""
        txtSpareRemarks.Text = ""




        txtPartyName.Text = ""
        txtBillAdresss.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtDistrict.Text = ""
        txtTaluka.Text = ""
        txtPincode.Text = ""
        txtArea.Text = ""
        txtcontctName.Text = ""
        txtContactNo.Text = ""
        txtOrdermastEmail.Text = ""
        txtEntryNo.Text = ""
        txtDeladress.Text = ""
        txtDelArea.Text = ""
        txtDelCity.Text = ""
        txtDelDistrict.Text = ""
        txtDelPincode.Text = ""
        txtDelState.Text = ""
        txtDelTaluka.Text = ""
        txtSPlanName.Text = ""
        txtSCapacity.Text = ""
        txtSModel.Text = ""
        txtSScheme.Text = ""

        txtEFInstallationFor.Text = ""
        txtEFRemarks.Text = ""
        dtEFDate.Text = ""
        tmStartTime.Text = ""
        tmEndTime.Text = ""
        chkECDone.Checked = False
        txtWorkDone.Text = ""
        txtWorkDelayRemarks.Text = ""
        txtEFRemarks.Text = ""
        txtECInstallationFor.Text = ""
        chkECDone.Checked = False
        dtECDDate.Text = ""
        txtEnginer.Text = ""
        txtReportBy.Text = ""
        txtEPRemarks.Text = ""
        rbBetter.Checked = False
        rbGood.Checked = False
        rbExcellent.Checked = False
        rbNotGood.Checked = False
        txtChkLogRemarks.Text = ""
        txtChklogTitle.Text = ""
        chkChkLogisDone.Checked = False
        dtChklogDate.Text = ""
        txtItemName.Text = ""
        txtQty.Text = ""
        txtPrice.Text = ""
        txtAmount.Text = ""
        txtGenSerFollowup.Text = ""
        txtGenSerRem.Text = ""
        txtGenServiceStatus.Text = ""
        txtFolowup3.Text = ""
        dtTentativeAttendDate.Text = ""
        txtSMoc.Text = ""
        txtSType.Text = ""
        txtSInspectionBy.Text = ""
        dtpSInspectionDate.Text = ""
        imgQualityReportPath = ""
        lblSuccess.Text = ""
        n1.Text = ""
        n2.Text = ""
        n3.Text = ""
        n4.Text = ""
        n5.Text = ""
        t1.Text = ""
        t2.Text = ""
        t3.Text = ""
        t4.Text = ""
        t5.Text = ""

        imgDocErrectionPath = ""
        lblSuccessEC.Text = ""
        dtpErrectionDate.Text = ""
        chkBottle1.Checked = False
        txtBottle1.Text = ""
        chkBottle2.Checked = False
        txtBottle2.Text = ""
        chkBlow1.Checked = False
        txtBlow1.Text = ""
        chkBlow2.Checked = False
        txtBlow2.Text = ""
        chkLab1.Checked = False
        txtLab1.Text = ""
        chkLab2.Checked = False
        txtLab2.Text = ""
        chkOther1.Checked = False
        txtOther1.Text = ""
        chkOther2.Checked = False
        txtOther2.Text = ""
        txtpendingMaterials.Text = ""
        lblSuccessDoc1.Text = ""
        lblSuccessDoc2.Text = ""
        lblSuccessDoc3.Text = ""
        txtSPlanName.Text = ""
        txtSCapacity.Text = ""
        txtSScheme.Text = ""
        txtSModel.Text = ""


        txtEmail1.Text = ""
        txtEmail2.Text = ""

        txtRemarks.Text = ""
        txtTitleReady.Text = ""
        dtPartyReadyDate.Text = ""
        dtpCompletionDate.Text = ""
        rbYes.Checked = True
        rwIDDelPartyReadyness = -1
        txtTitleReady.SelectedIndex = -1
        txtWaterPoint.Text = ""
        txtECRemarks.Text = ""

        txtRawWaterValue.Text = ""
        cmbParameters.SelectedIndex = -1
        txtTreatedWaterValue.Text = ""
        txtEngName.Text = ""
        'DGPRDetail.Rows.Clear()
        'dgvErrectionDetails.Rows.Clear()
        'DGWaterDetail.Rows.Clear()

    End Sub
    Private Sub txtEntryNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntryNo.Leave
        Dim data = linq_obj.SP_Get_AddressListByEnqNo(txtEntryNo.Text).ToList()
        If (data.Count > 0) Then
            Address_ID = data(0).Pk_AddressID
            BindAllGridData(Address_ID)
        End If
    End Sub


    Private Sub btnECDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnECDADD.Click
        txtECInstallationFor.Text = ""
        chkDoneEC.Checked = False
        dtECDDate.Text = ""
        b_EditDGVECDoneDetail = False
    End Sub

    Private Sub btnADDEF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnADDEF.Click
        txtEFInstallationFor.Text = ""
        txtEFRemarks.Text = ""
        dtEFDate.Text = ""
        tmStartTime.Text = ""
        tmEndTime.Text = ""
        chkECDone.Checked = False
        txtWorkDone.Text = ""
        txtWorkDelayRemarks.Text = ""
        txtEFRemarks.Text = ""
        b_EditDGEFDetails = False
    End Sub

    Private Sub btnTPRADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTPRADD.Click
        txtEngName.Text = ""
        txtReportBy.Text = ""
        txtEPRemarks.Text = ""
        rbBetter.Checked = False
        rbGood.Checked = False
        rbExcellent.Checked = False
        rbNotGood.Checked = False
        b_EditDGEPFDetail = False
    End Sub

    Private Sub btnChkLogAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChkLogAdd.Click
        txtChkLogRemarks.Text = ""
        txtChklogTitle.Text = ""
        chkChkLogisDone.Checked = False
        dtChklogDate.Text = ""
        b_EditDGCheckLog = False
    End Sub

    Private Sub btnClearItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearItem.Click
        txtItemName.Text = ""
        txtQty.Text = ""
        txtPrice.Text = ""
        txtAmount.Text = ""

    End Sub

    Private Sub btnAddFolowup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFolowup.Click
        txtGenSerFollowup.Text = ""
        txtGenSerRem.Text = ""
        txtGenServiceStatus.Text = ""
        txtFolowup3.Text = ""
        dtTentativeAttendDate.Text = ""


    End Sub

    Private Sub txtFolowup3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolowup3.Leave

    End Sub

    Private Sub txtFolowup3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolowup3.TextChanged
        If txtFolowup3.Text <> "" Then
            dtTentativeAttendDate.Text = Convert.ToDateTime(Class1.SetDate(dtGSDate.Text)).Date.AddDays(txtFolowup3.Text)
        End If
    End Sub

    Private Sub btnSaveEF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveEF.Click
        ''add new row runtime and display into grid. it will save after click on save button
        Try
            Dim dr As DataRow

            If dtEFDate.Text = "" Then
                MessageBox.Show("Ec Report Date cannot be blank")
                Exit Sub
            End If

            dr = tblEngFollowup.NewRow()
            dr("Date") = dtEFDate.Text
            dr("StartTime") = tmStartTime.Text
            dr("EndTime") = tmEndTime.Text
            dr("WorkDone") = txtWorkDone.Text
            dr("InstallationFor") = txtEFInstallationFor.Text
            dr("isDelay") = IIf(chkWorkDelay.Checked, True, False)
            dr("DelayRemarks") = txtWorkDelayRemarks.Text
            dr("Remarks") = txtEFRemarks.Text

            'insert into the service eng report
            If b_EditDGEFDetails = True Then
                tblEngFollowup.Rows(i_EditDGEFDetails)(0) = dr("Date")
                tblEngFollowup.Rows(i_EditDGEFDetails)(1) = dr("StartTime")
                tblEngFollowup.Rows(i_EditDGEFDetails)(2) = dr("EndTime")
                tblEngFollowup.Rows(i_EditDGEFDetails)(4) = dr("WorkDone")
                tblEngFollowup.Rows(i_EditDGEFDetails)(3) = dr("InstallationFor")
                tblEngFollowup.Rows(i_EditDGEFDetails)(5) = dr("isDelay")
                tblEngFollowup.Rows(i_EditDGEFDetails)(6) = dr("DelayRemarks")
                tblEngFollowup.Rows(i_EditDGEFDetails)(7) = dr("Remarks")
            Else
                linq_obj.SP_Tbl_Service_ECEngineerReportDetail_Insert(Address_ID,
                                                                 Class1.SetDate(dtEFDate.Text),
                                                                  tmStartTime.Text.ToString(),
                                                                 tmEndTime.Text.ToString(),
                                                                 txtWorkDone.Text,
                                                                 IIf(chkWorkDelay.Checked, True, False),
                                                                 txtWorkDelayRemarks.Text,
                                                                 txtEFInstallationFor.Text,
                                                                 txtEFRemarks.Text)
                linq_obj.SubmitChanges()

                tblEngFollowup.Rows.Add(dr)
            End If

            DGEFDetails.DataSource = tblEngFollowup

            txtEFInstallationFor.Text = ""
            txtEFRemarks.Text = ""
            dtEFDate.Text = ""
            tmStartTime.Text = ""
            tmEndTime.Text = ""
            chkECDone.Checked = False
            txtWorkDone.Text = ""
            txtWorkDelayRemarks.Text = ""
            b_EditDGEFDetails = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelEF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelEF.Click
        If DGEFDetails.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblEngFollowup.Rows(DGEFDetails.CurrentRow.Index).Delete()
                DGEFDetails.DataSource = tblEngFollowup
            End If
        End If

    End Sub

    Private Sub btnECDSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnECDSave.Click

        If txtECInstallationFor.Text = "" Then
            MessageBox.Show("Installation Type cannot be blank")
            Exit Sub
        End If
        Dim dr As DataRow
        dr = tblECDone.NewRow()
        dr("InstallationType") = txtECInstallationFor.Text
        dr("isDone") = IIf(chkDoneEC.Checked, True, False)
        If chkDoneEC.Checked = False Then
            dtECDDate.Text = ""
        End If
        dr("DoneDate") = dtECDDate.Text

        'insert into the service eng report
        If b_EditDGVECDoneDetail = True Then
            tblECDone.Rows(i_EditDGVECDoneDetail)(0) = dr("InstallationType")
            tblECDone.Rows(i_EditDGVECDoneDetail)(1) = dr("isDone")
            tblECDone.Rows(i_EditDGVECDoneDetail)(2) = dr("DoneDate")
        Else
            linq_obj.SP_Tbl_Service_ECDoneDetail_Insert(txtECInstallationFor.Text,
                                                              IIf(chkDoneEC.Checked, True, False),
                                                            Class1.SetDate(dtECDDate.Text), Address_ID)
            linq_obj.SubmitChanges()
            tblECDone.Rows.Add(dr)
        End If
        
        DGVECDoneDetail.DataSource = tblECDone

        txtECInstallationFor.Text = ""
        chkDoneEC.Checked = False
        dtECDDate.Text = ""
        b_EditDGVECDoneDetail = False
        i_EditDGVECDoneDetail = -1
    End Sub

    Private Sub btnECDDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnECDDelete.Click
        If DGVECDoneDetail.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblECDone.Rows(DGVECDoneDetail.CurrentRow.Index).Delete()
                DGVECDoneDetail.DataSource = tblECDone
            End If
        End If

    End Sub

    Private Sub btnTPRSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTPRSAVE.Click
        Dim dr As DataRow
        dr = tblTPR.NewRow()
        Dim strpercat As String

        If txtEngName.Text = "" Then
            MessageBox.Show("You cannot be blank EngName")
            Exit Sub
        End If

        dr("EngineerName") = txtEngName.Text
        dr("ReportBy") = txtReportBy.Text
        If (rbGood.Checked = True) Then
            strpercat = "Good"
            dr("PerformanceCategory") = "Good"

        ElseIf (rbBetter.Checked = True) Then
            strpercat = "Better"
            dr("PerformanceCategory") = "Better"
        ElseIf (rbExcellent.Checked = True) Then
            strpercat = "Excellent"
            dr("PerformanceCategory") = "Excellent"
        ElseIf (rbNotGood.Checked = True) Then
            strpercat = "Not Good"
            dr("PerformanceCategory") = "Not Good"

        Else
            strpercat = ""
        End If


        dr("Remarks") = txtEPRemarks.Text
        'insert into the service eng report
        If b_EditDGEPFDetail = True Then
            tblTPR.Rows(i_EditDGEPFDetail)(0) = dr("EngineerName")
            tblTPR.Rows(i_EditDGEPFDetail)(1) = dr("ReportBy")
            tblTPR.Rows(i_EditDGEPFDetail)(2) = strpercat
            tblTPR.Rows(i_EditDGEPFDetail)(3) = dr("Remarks")
        Else
            linq_obj.SP_Tbl_Service_TechPerformanceRepDetail_Insert(Address_ID, txtEngName.Text, txtReportBy.Text, strpercat, txtEPRemarks.Text)
            linq_obj.SubmitChanges()

            tblTPR.Rows.Add(dr)

        End If
        DGEPFDetail.DataSource = tblTPR
        'DGEPFDetail.Columns(0).HeaderText = "Eng Name"
        txtEngName.Text = ""
        txtReportBy.Text = ""
        txtEPRemarks.Text = ""
        rbBetter.Checked = False
        rbGood.Checked = False
        rbExcellent.Checked = False
        rbNotGood.Checked = False
        b_EditDGEPFDetail = False
        i_EditDGEPFDetail = -1
    End Sub

    Private Sub btnTPRDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTPRDelete.Click
        If DGEPFDetail.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblTPR.Rows(DGEPFDetail.CurrentRow.Index).Delete()
                DGEPFDetail.DataSource = tblTPR
            End If
        End If

    End Sub

    Private Sub btnChkLogSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChkLogSave.Click

        Dim dr As DataRow

        If txtChklogTitle.Text = "" Then
            MessageBox.Show("Title cannot be blank")
            Exit Sub
        End If

        If cmbService.Text = "" Then
            MessageBox.Show("Service cannot be blank")
            Exit Sub
        End If
        dr = tblChkLog.NewRow()
        dr("Title") = txtChklogTitle.Text
        dr("isDone") = IIf(chkChkLogisDone.Checked, True, False)
        dr("Date") = dtChklogDate.Text
        dr("Remarks") = txtChkLogRemarks.Text

        If b_EditDGCheckLog = True Then
            tblChkLog.Rows(i_EditDGCheckLog)(0) = dr("Title")
            tblChkLog.Rows(i_EditDGCheckLog)(1) = dr("isDone")
            tblChkLog.Rows(i_EditDGCheckLog)(2) = dr("Date")
            tblChkLog.Rows(i_EditDGCheckLog)(3) = dr("Remarks")
        Else
            linq_obj.SP_Tbl_Service_CheckLogDetail_Insert(Address_ID, txtChklogTitle.Text, IIf(chkChkLogisDone.Checked, True, False), Class1.SetDate(dtChklogDate.Text), txtChkLogRemarks.Text)
            linq_obj.SubmitChanges()
            tblChkLog.Rows.Add(dr)
        End If
        
        DGCheckLog.DataSource = tblChkLog
        txtChkLogRemarks.Text = ""
        txtChklogTitle.Text = ""
        chkChkLogisDone.Checked = False
        dtChklogDate.Text = ""
        b_EditDGCheckLog = False
        i_EditDGCheckLog = -1
    End Sub

    Private Sub btnChkLogDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChkLogDelete.Click
        If DGCheckLog.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblChkLog.Rows(DGCheckLog.CurrentRow.Index).Delete()
                DGCheckLog.DataSource = tblChkLog
            End If
        End If

    End Sub

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click

        Dim dr As DataRow
        dr = tblSpareDetail.NewRow()
        dr("ItemName") = txtItemName.Text
        dr("Qty") = txtQty.Text
        dr("Price") = txtPrice.Text
        dr("Amount") = txtAmount.Text
        If b_EditDGVSpareDetails = True Then
            tblSpareDetail.Rows(i_EditDGVSpareDetails)(0) = dr("ItemName")
            tblSpareDetail.Rows(i_EditDGVSpareDetails)(1) = dr("Qty")
            tblSpareDetail.Rows(i_EditDGVSpareDetails)(2) = dr("Price")
            tblSpareDetail.Rows(i_EditDGVSpareDetails)(3) = dr("Amount")
        Else
            linq_obj.SP_Tbl_Service_SpareDetail_Insert(Address_ID, txtItemName.Text, Convert.ToInt32(txtQty.Text), Convert.ToDecimal(txtPrice.Text), Convert.ToDecimal(txtAmount.Text))
            linq_obj.SubmitChanges()
            tblSpareDetail.Rows.Add(dr)
        End If

        DGVSpareDetails.DataSource = tblSpareDetail
        txtItemName.Text = ""
        txtQty.Text = ""
        txtPrice.Text = ""
        txtAmount.Text = ""
        b_EditDGVSpareDetails = False
    End Sub

    Private Sub btnDelItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelItem.Click
        If DGVSpareDetails.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblSpareDetail.Rows(DGVSpareDetails.CurrentRow.Index).Delete()
                DGVSpareDetails.DataSource = tblSpareDetail
            End If
        End If

    End Sub

    Private Sub btnsavefolwup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavefolwup.Click
        Dim dr As DataRow

        dr = tblGenService.NewRow()

        dr("FollowUp") = txtGenSerFollowup.Text
        dr("TentativeDate") = dtTentativeAttendDate.Text
        dr("Status") = txtGenServiceStatus.Text
        dr("Remarks") = txtGenSerRem.Text

        If b_EditDGVGenService = True Then
            tblGenService.Rows(i_EditDGVGenService)(0) = dr("FollowUp")
            tblGenService.Rows(i_EditDGVGenService)(1) = dr("TentativeDate")
            tblGenService.Rows(i_EditDGVGenService)(2) = dr("Status")
            tblGenService.Rows(i_EditDGVGenService)(3) = dr("Remarks")
        Else
            linq_obj.SP_Tbl_Service_GeneralServiceDetail_Insert(Address_ID, txtGenSerFollowup.Text,
                                                              Class1.SetDate(dtTentativeAttendDate.Text),
                                                               txtGenServiceStatus.Text,
                                                               txtGenSerRem.Text
                                                              )

            tblGenService.Rows.Add(dr)
        End If
        
        DGVGenService.DataSource = tblGenService
        txtGenSerFollowup.Text = ""
        txtGenSerRem.Text = ""
        txtGenServiceStatus.Text = ""
        txtFolowup3.Text = ""
        dtTentativeAttendDate.Text = ""
        b_EditDGVGenService = False
    End Sub

    Private Sub chkECDone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkECDone.CheckedChanged
        If (chkECDone.Checked = True) Then
            dtECDone.Visible = True
        Else
            dtECDone.Visible = False

        End If
    End Sub

    Private Sub chkDoneEC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDoneEC.CheckedChanged
        If (chkDoneEC.Checked = True) Then
            dtECDDate.Visible = True
        Else
            dtECDDate.Visible = False

        End If
    End Sub

    Private Sub chkWorkDelay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWorkDelay.CheckedChanged
        If (chkWorkDelay.Checked = True) Then
            txtWorkDelayRemarks.Visible = True
        Else
            txtWorkDelayRemarks.Visible = False

        End If
    End Sub

    Private Sub chkServiceDone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkServiceDone.CheckedChanged

        If (chkServiceDone.Checked = True) Then
            'Dim result As DialogResult = MessageBox.Show("Is Service Completed?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            'If result = DialogResult.Yes Then
            dtServiceDoneDate.Visible = True
            'End If
        Else
            dtServiceDoneDate.Visible = False

        End If

    End Sub

    Private Sub chkChkLogisDone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkChkLogisDone.CheckedChanged
        If (chkChkLogisDone.Checked = True) Then
            dtChklogDate.Visible = True
        Else
            dtChklogDate.Visible = False

        End If
    End Sub

    Private Sub cmbService_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbService.SelectionChangeCommitted

        Dim dataService = linq_obj.SP_Get_ServiceDate(Address_ID, lblServiceType.Text).ToList()
        If (dataService.Count > 0) Then

            dtApproxDate.Value = dataService(0).ServiceDate.Value
            If (cmbService.SelectedText = "Service1") Then
                grpCheckLog.Visible = True
            Else
                grpCheckLog.Visible = True
            End If
        End If



    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveAllData()
        GvInEnq_Bind()
        btnSave.Enabled = False

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        clearAll()

    End Sub

    'viraj

    Private Sub btnUp1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp1.Click
        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.ShowDialog()
        imgDoc1Src = openFileDialog1.FileName.ToString()
        imgDoc1Path = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        lblSuccessDoc1.Text = imgDoc1Path
    End Sub

    Private Sub btnUp2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp2.Click
        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.ShowDialog()
        imgDoc2Src = openFileDialog1.FileName.ToString()
        imgDoc2Path = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        lblSuccessDoc2.Text = imgDoc2Path
    End Sub

    Private Sub btnUp3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp3.Click
        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.ShowDialog()
        imgDoc3Src = openFileDialog1.FileName.ToString()
        imgDoc3Path = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        lblSuccessDoc3.Text = imgDoc3Path
    End Sub



    

    Private Sub btnPRAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRAdd.Click
        ''add new row runtime and display into grid. it will save after click on save button

        If dtPartyReadyDate.Text = "" Then
            MessageBox.Show("Party Readydate cannot be blank")
            Exit Sub
        End If

        Dim dr As DataRow
        

        dr = tblPrDetail.NewRow()

        dr("HeaderName") = txtTitleReady.Text
        If rbYes.Checked = True Then
            dr("Status") = rbYes.Text
        ElseIf rbNo.Checked = True Then
            dr("Status") = rbNo.Text
        Else
            dr("Status") = rbprocess.Text
        End If
        dr("CompletionDate") = dtpCompletionDate.Text
        dr("Remarks") = txtRemarks.Text
        dr("PartyReadyDate") = dtPartyReadyDate.Text

        If (rwIDDelPartyReadyness >= 0) Then

            DGPRDetail.Rows(rwIDDelPartyReadyness).Cells(PartyReadyness.HeaderName).Value = dr("HeaderName")
            DGPRDetail.Rows(rwIDDelPartyReadyness).Cells(PartyReadyness.Status).Value = dr("Status")
            DGPRDetail.Rows(rwIDDelPartyReadyness).Cells(PartyReadyness.CompletionDate).Value = dr("CompletionDate")
            DGPRDetail.Rows(rwIDDelPartyReadyness).Cells(PartyReadyness.Remarks).Value = dr("Remarks")
            DGPRDetail.Rows(rwIDDelPartyReadyness).Cells(PartyReadyness.PartyReadyDate).Value = dr("PartyReadyDate")
            
            tblPrDetail = DGPRDetail.DataSource

            'TblRawMaterial.Rows.Add(dr)
        Else
            tblPrDetail.Rows.Add(dr)
        End If

        insertPartyReadynessDetails()

        DGPRDetail.DataSource = tblPrDetail


        txtRemarks.Text = ""
        txtTitleReady.Text = ""
        txtTitleReady.SelectedIndex = -1
        dtPartyReadyDate.Text = ""
        dtpCompletionDate.Text = ""
        rbYes.Checked = True
        rwIDDelPartyReadyness = -1
    End Sub
    Public Sub insertPartyReadynessDetails()
        'insert into the service eng report+
        Dim delfollowDetail = linq_obj.SP_Tbl_Service_PartyReadynessDetail_Delete(Address_ID)
        linq_obj.SubmitChanges()

        For i As Integer = 0 To tblPrDetail.Rows.Count - 1
            With tblPrDetail
                linq_obj.SP_Tbl_Service_PartyReadynessDetail_Insert(Address_ID,
                                                        .Rows(i)("HeaderName").ToString(),
                                                        .Rows(i)("Status").ToString(),
                                                        Class1.SetDate(.Rows(i)("CompletionDate").ToString()),
                                                        .Rows(i)("Remarks").ToString(), Convert.ToDateTime(.Rows(i)("PartyReadyDate").ToString())
                                                        )
                linq_obj.SubmitChanges()
            End With
        Next


    End Sub
    Private Sub btnErrectionAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErrectionAdd.Click

        If txtRawWaterValue.Text = "" Then
            MessageBox.Show("You cannot be blank Raw water ")
            Exit Sub
        End If

        Dim dr As DataRow

        dr = tblQuality.NewRow()

        dr("RawWater") = txtRawWaterValue.Text
        dr("Parameter") = cmbParameters.Text
        dr("TreatedWater") = txtTreatedWaterValue.Text

        If rwIDDelErrection > 0 Then
            tblQuality.Rows(rwIDDelErrection)(Quality.RawWater) = dr("RawWater")
            tblQuality.Rows(rwIDDelErrection)(Quality.Parameter) = dr("Parameter")
            tblQuality.Rows(rwIDDelErrection)(Quality.TreatedWater) = dr("TreatedWater")
        Else
            linq_obj.SP_Tbl_Service_ErrectionCommDetails_Insert(Address_ID, txtRawWaterValue.Text, cmbParameters.Text, txtTreatedWaterValue.Text)
            linq_obj.SubmitChanges()
            tblQuality.Rows.Add(dr)
        End If

        dgvErrectionDetails.DataSource = tblQuality
        txtRawWaterValue.Text = ""
        cmbParameters.SelectedIndex = -1
        txtTreatedWaterValue.Text = ""
        rwIDDelErrection = -1

    End Sub
    Private Sub btnAddPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPoint.Click

        If txtWaterPoint.Text = "" Then
            MessageBox.Show("You cannot be blank point")
            Exit Sub
        End If

        Dim dr As DataRow
        dr = tblWaterDetails.NewRow()
        dr("WaterType") = txtWaterPoint.Text
        dr("Status") = IIf(chkWaterRemark.Checked, True, False)
        dr("Remarks") = txtWaterRemark.Text

        If rwIDDelWaterDetail >= 0 Then
            tblWaterDetails.Rows(rwIDDelWaterDetail)(0) = dr("Watertype")
            tblWaterDetails.Rows(rwIDDelWaterDetail)(1) = dr("Status")
            tblWaterDetails.Rows(rwIDDelWaterDetail)(2) = dr("Remarks")
        Else
            linq_obj.SP_Tbl_Service_Physical_WaterTreatment_Master_Insert(Address_ID, txtWaterPoint.Text, IIf(chkWaterRemark.Checked, True, False), txtWaterRemark.Text)
            linq_obj.SubmitChanges()
            tblWaterDetails.Rows.Add(dr)
        End If

        DGWaterDetail.DataSource = tblWaterDetails
        chkWaterRemark.Checked = False
        txtWaterRemark.Text = ""
        txtWaterPoint.Text = ""
    End Sub



    Private Sub btnBrowseEC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseEC.Click
        Dim openFileDialog1 As New OpenFileDialog
        lblSuccessEC.Text = ""

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            imgDocErrectionSrc = openFileDialog1.FileName.ToString()
            imgDocErrectionPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
            lblSuccessEC.Text = imgDocErrectionPath
        End If
    End Sub

    Private Sub btnPRDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRDelete.Click
        If DGPRDetail.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblPrDetail.Rows(DGPRDetail.CurrentRow.Index).Delete()
                DGPRDetail.DataSource = tblPrDetail
            End If
        End If

    End Sub


    Private Sub btnErrectionDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErrectionDelete.Click
        If dgvErrectionDetails.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblQuality.Rows(dgvErrectionDetails.CurrentRow.Index).Delete()
                dgvErrectionDetails.DataSource = tblQuality
            End If
        End If

    End Sub

    Private Sub btnDeletePoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeletePoint.Click
        If DGWaterDetail.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblWaterDetails.Rows(DGWaterDetail.CurrentRow.Index).Delete()
                DGWaterDetail.DataSource = tblWaterDetails
            End If
        End If

    End Sub

    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        UpdateAll()
        

    End Sub


    Public Sub UpdateAll()
        Try
            'Save All Data
            If Address_ID > 0 Then

                ''Update Service master
                Dim resSer As Integer
                resSer = linq_obj.SP_Insert_Update_Tbl_ServiceMaster(txtEntryNo.Text, Class1.SetDate(dtServicedate.Text), Address_ID, 1)
                linq_obj.SubmitChanges()

                'Update Contact Detail
                Dim resContact As Integer
                resContact = linq_obj.SP_UpdateAddressContactDetail(txtcontctName.Text, txtContactNo.Text, txtOrdermastEmail.Text, txtEmail1.Text, txtEmail2.Text, Address_ID)
                linq_obj.SubmitChanges()

                'bind Address Detail
                Dim resAddress As Integer
                resAddress = linq_obj.SP_UpdateAddress(txtPartyName.Text, txtBillAdresss.Text, txtArea.Text, txtCity.Text, txtPincode.Text, txtTaluka.Text, txtDistrict.Text, txtState.Text, txtDeladress.Text, txtDelArea.Text, txtDelCity.Text, txtDelPincode.Text, txtDelTaluka.Text, txtDelDistrict.Text, txtDelState.Text, Address_ID)
                If resAddress >= 0 Then
                    linq_obj.SubmitChanges()
                End If


                'Save AddressDetail
                'delete Old Data
                Dim delConatctDetail = linq_obj.SP_Tbl_Service_ContactInfo_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblContactDetail.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_ContactInfo_Insert(Address_ID,
                    tblContactDetail.Rows(i)("Name").ToString(),
                    tblContactDetail.Rows(i)("Post").ToString(),
                    tblContactDetail.Rows(i)("Contact").ToString(),
                   tblContactDetail.Rows(i)("Email").ToString())

                    linq_obj.SubmitChanges()
                Next


                'Save Folloup Details

                Dim resECEng As Integer
                resECEng = linq_obj.SP_Tbl_Service_ECEngRepMaster_Update(Address_ID, IIf(chkECDone.Checked, True, False), Convert.ToDateTime(If(dtECDone.Text = "", "01-01-1900", dtECDone.Text)))
                If (resECEng > 0) Then
                    linq_obj.SubmitChanges()
                End If

                'delete Old Data
                Dim delfollowDetail1 = linq_obj.SP_Tbl_Service_ECEngineerReportDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblEngFollowup.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_ECEngineerReportDetail_Insert(Address_ID,
                                                                 Class1.SetDate(tblEngFollowup.Rows(i)("Date").ToString()),
                                                                tblEngFollowup.Rows(i)("StartTime").ToString(), tblEngFollowup.Rows(i)("EndTime").ToString(),
                                                                tblEngFollowup.Rows(i)("WorkDone").ToString(),
                                                                 Convert.ToBoolean(tblEngFollowup.Rows(i)("isDelay").ToString()),
                     tblEngFollowup.Rows(i)("DelayRemarks").ToString(),
                    tblEngFollowup.Rows(i)("InstallationFor").ToString(),
                    tblEngFollowup.Rows(i)("Remarks").ToString())
                    linq_obj.SubmitChanges()
                Next


                linq_obj.SP_Tbl_Service_ECDoneDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblECDone.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_ECDoneDetail_Insert(tblECDone.Rows(i)("InstallationType").ToString(),
                                                                Convert.ToBoolean(tblECDone.Rows(i)("isDone").ToString()),
                                                                Class1.SetDate(tblECDone.Rows(i)("DoneDate").ToString()), Address_ID)
                Next
                linq_obj.SubmitChanges()



                'TPR
                linq_obj.SP_Tbl_Service_TechPerformanceRepDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()


                For i As Integer = 0 To tblTPR.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_TechPerformanceRepDetail_Insert(Address_ID, tblTPR.Rows(i)("EngineerName").ToString(),
                                                                tblTPR.Rows(i)("ReportBy").ToString(),
                                                               tblTPR.Rows(i)("PerformanceCategory").ToString(),
                                                               tblTPR.Rows(i)("Remarks").ToString())
                Next
                linq_obj.SubmitChanges()


                'save All service
                If chkServiceDone.Checked = True And dtServiceDoneDate.Text <> "" Then
                    Dim resserviceUpd As Integer
                    resserviceUpd = linq_obj.SP_Tbl_Service_ServiceLogMaster_Update(Address_ID, cmbService.Text, Now, "Complete", Class1.SetDate(dtServiceDoneDate.Text))
                    linq_obj.SubmitChanges()
                End If

                'save All service

                Dim resservice As Integer
                resservice = linq_obj.SP_Tbl_Service_ServiceDetail_Update(Address_ID, dtServDelDate.Value, txtSerEngName.Text, txtSerRemarks.Text, txtITDS.Text, txtITH.Text, txtIPH.Text, txtIRemarks.Text, txtOTDS.Text, txtOTH.Text, txtOPH.Text, txtORemarks.Text, txtSerType.Text, IIf(chkServiceDone.Checked, True, False), Convert.ToDateTime(If(dtServiceDoneDate.Text = "", "01-01-1900", dtServiceDoneDate.Text)))
                linq_obj.SubmitChanges()





                linq_obj.SP_Tbl_Service_CheckLogDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblChkLog.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_CheckLogDetail_Insert(Address_ID, tblChkLog.Rows(i)("Title").ToString(),
                                                                tblChkLog.Rows(i)("isDone").ToString(),
                                                               Class1.SetDate(tblChkLog.Rows(i)("Date").ToString()),
                                                               tblChkLog.Rows(i)("Remarks").ToString())
                Next
                linq_obj.SubmitChanges()



                'Spare Detail

                linq_obj.SP_Tbl_Service_SpareMaster_Update(Address_ID, Class1.SetDate(dtSpareDate.Text), txtSpareRecBy.Text, txtDisVia.Text, txtSpareEngName.Text, txtSpareRemarks.Text)
                linq_obj.SubmitChanges()

                linq_obj.SP_Tbl_Service_SpareDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblSpareDetail.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_SpareDetail_Insert(Address_ID, tblSpareDetail.Rows(i)("ItemName").ToString(),
                                                                Convert.ToInt32(tblSpareDetail.Rows(i)("Qty").ToString()),
                      Convert.ToDecimal(tblSpareDetail.Rows(i)("Price").ToString()),
                                                               Convert.ToDecimal(tblSpareDetail.Rows(i)("Amount").ToString()))
                    linq_obj.SubmitChanges()
                Next


                'Save General service Detail

                Dim resgenService As Integer
                resgenService = linq_obj.SP_Tbl_Service_GeneralServiceMaster_Update(Class1.SetDate(dtGSDate.Text), txtSerType.Text, txtComplainNo.Text, Class1.SetDate(dtAttendDate.Text), txtAtendedBy.Text, txtEnginer.Text, Address_ID)
                linq_obj.SubmitChanges()

                linq_obj.SP_Tbl_Service_GeneralServiceDetail_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For i As Integer = 0 To tblGenService.Rows.Count - 1
                    linq_obj.SP_Tbl_Service_GeneralServiceDetail_Insert(Address_ID, tblGenService.Rows(i)("FollowUp").ToString(),
                                                                Convert.ToDateTime(tblGenService.Rows(i)("TentativeDate").ToString()),
                                                                tblGenService.Rows(i)("Status").ToString(),
                                                               tblGenService.Rows(i)("Remarks").ToString())
                Next
                linq_obj.SubmitChanges()

                'viraj

                'save main Order Details.
                Dim resorder As Integer
                resorder = linq_obj.SP_Tbl_Service_ProjectDetail_Update(Address_ID, txtSMoc.Text, txtSType.Text, txtSInspectionBy.Text, Class1.SetDate(dtpSInspectionDate.Text))

                If (resorder >= 0) Then
                    linq_obj.SubmitChanges()
                End If


                Dim resProjectId As Integer
                resProjectId = linq_obj.SP_Tbl_Service_PartyReadynessMaster_Update(Address_ID, imgQualityReportPath)

                If (resProjectId >= 0) Then
                    linq_obj.SubmitChanges()

                    Dim delfollowDetail = linq_obj.SP_Tbl_Service_PartyReadynessDetail_Delete(Address_ID)
                    linq_obj.SubmitChanges()

                    For i As Integer = 0 To tblPrDetail.Rows.Count - 1
                        With tblPrDetail
                            linq_obj.SP_Tbl_Service_PartyReadynessDetail_Insert(Address_ID,
                                                                    .Rows(i)("HeaderName").ToString(),
                                                                    .Rows(i)("Status").ToString(),
                                                                    Class1.SetDate(.Rows(i)("CompletionDate").ToString()),
                                                                    .Rows(i)("Remarks").ToString(), Convert.ToDateTime(.Rows(i)("PartyReadyDate").ToString())
                                                                    )
                            linq_obj.SubmitChanges()
                        End With
                    Next
                End If


                'Save All Erreaction Details

                Dim Errectionid As Integer
                Errectionid = linq_obj.SP_Tbl_Service_ErrectionCommMaster_Update(Address_ID, t1.Text, n1.Text, t2.Text, n2.Text, t3.Text, n3.Text, t4.Text, n4.Text, t5.Text, n5.Text, imgDocErrectionPath, Class1.SetDate(dtpErrectionDate.Text), txtECRemarks.Text)

                If (Errectionid >= 0) Then

                    linq_obj.SubmitChanges()
                    'delete Old Data
                    'delete Old Data
                    Dim delfollowDetail = linq_obj.SP_Tbl_Service_ErrectionCommDetails_Delete(Address_ID)
                    linq_obj.SubmitChanges()

                    For i As Integer = 0 To tblQuality.Rows.Count - 1
                        With tblQuality

                            linq_obj.SP_Tbl_Service_ErrectionCommDetails_Insert(Address_ID,
                                                .Rows(i)("RawWater").ToString(),
                                                .Rows(i)("Parameter").ToString(),
                                                .Rows(i)("TreatedWater").ToString())
                            linq_obj.SubmitChanges()
                        End With
                    Next
                End If


                'Save Water Detail

                Dim WaterID As Integer
                WaterID = linq_obj.SP_Tbl_Service_PhysicalVerificationDayOneMaster_Update(Address_ID, chkBottle1.Checked, txtBottle1.Text, chkBottle2.Checked, txtBottle2.Text,
                chkBlow1.Checked, txtBlow1.Text, chkBlow2.Checked, txtBlow2.Text, chkLab1.Checked, txtLab1.Text, chkLab2.Checked, txtLab2.Text, chkOther1.Checked, txtOther1.Text, chkOther2.Checked, txtOther2.Text, txtpendingMaterials.Text)


                If (WaterID >= 0) Then
                    linq_obj.SubmitChanges()

                    'delete Old Data
                    Dim delfollowDetail = linq_obj.SP_Tbl_Service_Physical_WaterTreatment_Master_Delete(Address_ID)
                    linq_obj.SubmitChanges()

                    For i As Integer = 0 To tblWaterDetails.Rows.Count - 1
                        With tblWaterDetails
                            linq_obj.SP_Tbl_Service_Physical_WaterTreatment_Master_Insert(Address_ID,
                                                                    .Rows(i)("WaterType").ToString(),
                                                                    .Rows(i)("Status").ToString(),
                                                                    .Rows(i)("Remarks").ToString())
                            linq_obj.SubmitChanges()
                        End With
                    Next
                End If

                Dim RemarkID As Integer
                RemarkID = linq_obj.SP_Tbl_Service_PartyRemarksMaster_Update(Address_ID, imgDoc1Path, imgDoc2Path, imgDoc3Path)
                If (RemarkID >= 0) Then
                    linq_obj.SubmitChanges()
                End If
                MessageBox.Show("Successfully Updated")
                clearAll()
            Else
                MessageBox.Show("No Address Informations Found")
            End If

        Catch ex As Exception
            MessageBox.Show("Error :" + ex.Message)
        End Try
    End Sub


    Public Sub BindAllData()
        Try
            'Auto Complate WaterTreatment 
            txtWaterPoint.AutoCompleteCustomSource.Clear()
            Dim water = linq_obj.SP_Get_Autocomp_Physical_WaterTreatment_Master()
            For Each item As SP_Get_Autocomp_Physical_WaterTreatment_MasterResult In water
                txtWaterPoint.AutoCompleteCustomSource.Add(item.WaterType)
            Next

            Dim Service = linq_obj.SP_Select_Tbl_ServiceMaster(Address_ID).ToList

            If Service.Count > 0 Then
                'Add Navin 03-03-2015 
                'btnSave.Enabled = False
                Dim checkservice = linq_obj.SP_Check_TblServiceMasterEntry(Address_ID).ToList()
                If checkservice.Count = 0 Then
                    btnSave.Enabled = True
                    btnChange.Enabled = False

                Else
                    btnSave.Enabled = False
                    btnChange.Enabled = True

                End If



                'bind Main Order Data

                For Each item As SP_Select_Tbl_ServiceMasterResult In Service
                    With item
                        txtEntryNo.Text = item.EnqNo
                        txtPartyName.Text = item.Name
                        dtServicedate.Text = Class1.Datecheck(item.ServiceDate)
                    End With
                Next

                'bind Service details
                Dim ProjectDetails = linq_obj.SP_Tbl_Service_ProjectDetail_Select(Address_ID).ToList()
                If (ProjectDetails.Count > 0) Then
                    btnSave.Enabled = False
                    'bind Main Order Data

                    For Each item As SP_Tbl_Service_ProjectDetail_SelectResult In ProjectDetails
                        With item
                            txtSMoc.Text = .MOC
                            txtSType.Text = .PlantType
                            txtSInspectionBy.Text = .InspectionBy
                            dtpSInspectionDate.Text = Class1.Datecheck(.InspectionDate)
                        End With
                    Next
                End If

                'bind PartyRadyness Detail
                Dim PartyReadyness = linq_obj.SP_Tbl_Service_PartyReadynessMaster_Select(Address_ID).ToList()
                If (PartyReadyness.Count > 0) Then
                    For Each item As SP_Tbl_Service_PartyReadynessMaster_SelectResult In PartyReadyness
                        imgQualityReportPath = item.QualityReportPath
                        lblSuccess.Text = item.QualityReportPath
                    Next
                End If


                Dim RadynessDetails = linq_obj.SP_Tbl_Service_PartyReadynessDetail_Select(Address_ID).ToList()
                If (RadynessDetails.Count > 0) Then

                    tblPrDetail.Clear()

                    For Each item As SP_Tbl_Service_PartyReadynessDetail_SelectResult In RadynessDetails
                        Dim dr As DataRow
                        dr = tblPrDetail.NewRow()

                        dr("HeaderName") = item.HeaderName
                        dr("Status") = item.Status
                        dr("CompletionDate") = Class1.Datecheck(item.CompletionDate)
                        dr("Remarks") = item.Remarks
                        dr("PartyReadyDate") = Class1.Datecheck(item.PartyReadyDate)

                        tblPrDetail.Rows.Add(dr)
                    Next
                    DGPRDetail.DataSource = tblPrDetail
                End If


                'bind Errection Detail
                Dim ErrectionMaster = linq_obj.SP_Tbl_Service_ErrectionCommMaster_Select(Address_ID).ToList()
                If (ErrectionMaster.Count > 0) Then
                    For Each item As SP_Tbl_Service_ErrectionCommMaster_SelectResult In ErrectionMaster
                        With item
                            n1.Text = .Engg_Name1
                            n2.Text = .Engg_Name2
                            n3.Text = .Engg_Name3
                            n4.Text = .Engg_Name4
                            n5.Text = .Engg_Name5
                            t1.Text = .Engg_Title1
                            t2.Text = .Engg_Title2
                            t3.Text = .Engg_Title3
                            t4.Text = .Engg_Title4
                            t5.Text = .Engg_Title5

                            imgDocErrectionPath = .Documentupload
                            lblSuccessEC.Text = .Documentupload
                            dtpErrectionDate.Text = Class1.Datecheck(.ECDate)
                        End With
                    Next
                End If


                Dim ErrectionDetials = linq_obj.SP_Tbl_Service_ErrectionCommDetails_Select(Address_ID).ToList()
                If (ErrectionDetials.Count > 0) Then

                    tblQuality.Clear()

                    For Each item As SP_Tbl_Service_ErrectionCommDetails_SelectResult In ErrectionDetials
                        Dim dr As DataRow
                        dr = tblQuality.NewRow()

                        dr("RawWater") = item.RawWaterValue
                        dr("Parameter") = item.Parameter
                        dr("TreatedWater") = item.TreatedWaterValue

                        tblQuality.Rows.Add(dr)
                    Next
                    dgvErrectionDetails.DataSource = tblQuality
                End If


                ''Water Details



                Dim WaterMaster = linq_obj.SP_Tbl_Service_PhysicalVerificationDayOneMaster_Select(Address_ID).ToList()
                If (ErrectionMaster.Count > 0) Then
                    For Each item As SP_Tbl_Service_PhysicalVerificationDayOneMaster_SelectResult In WaterMaster

                        With item
                            chkBottle1.Checked = .BottleConvStatus
                            txtBottle1.Text = .BottleConvRemarks
                            chkBottle2.Checked = .BottleCompStatus
                            txtBottle2.Text = .BottleCompRemarks
                            chkBlow1.Checked = .BlowCompStatus
                            txtBlow1.Text = .BlowCompRemarks
                            chkBlow2.Checked = .BlowAirStatus
                            txtBlow2.Text = .BlowAirRemarks
                            chkLab1.Checked = .LabPack1Status
                            txtLab1.Text = .LabPack1Remarks
                            chkLab2.Checked = .LabPack2Status
                            txtLab2.Text = .LabPack2Remarks
                            chkOther1.Checked = .Other1Status
                            txtOther1.Text = .Other1Remarks
                            chkOther2.Checked = .Other2Status
                            txtOther2.Text = .Other2Remarks
                            txtpendingMaterials.Text = .PendingMaterials
                        End With
                    Next
                End If


                Dim WatterDetails = linq_obj.SP_Tbl_Service_Physical_WaterTreatment_Master_Select(Address_ID).ToList()
                If (WatterDetails.Count > 0) Then

                    tblWaterDetails.Clear()

                    For Each item As SP_Tbl_Service_Physical_WaterTreatment_Master_SelectResult In WatterDetails
                        Dim dr As DataRow
                        dr = tblWaterDetails.NewRow()

                        dr("WaterType") = item.WaterType
                        dr("Status") = item.Status
                        dr("Remarks") = item.Remarks

                        tblWaterDetails.Rows.Add(dr)
                    Next
                    DGWaterDetail.DataSource = tblWaterDetails
                End If

                Dim PartyRemark = linq_obj.SP_Tbl_Service_PartyRemarksMaster_Select(Address_ID).ToList()
                If (ErrectionMaster.Count > 0) Then
                    For Each item As SP_Tbl_Service_PartyRemarksMaster_SelectResult In PartyRemark

                        With item
                            lblSuccessDoc1.Text = .Document1
                            lblSuccessDoc2.Text = .Document2
                            lblSuccessDoc3.Text = .Document3
                        End With
                    Next
                End If

                Dim ProjectDetails1 = linq_obj.SP_Select_Tbl_ProjectInformationMaster(Address_ID).ToList()
                If (ProjectDetails1.Count > 0) Then
                    For Each item As SP_Select_Tbl_ProjectInformationMasterResult In ProjectDetails1

                        With item
                            txtSPlanName.Text = .PlantName
                            txtSModel.Text = .Model
                            txtSCapacity.Text = .Capacity
                            ''txtSScheme.Text = .TreatmentScheme
                            txtSType.Text = .ProjectName
                        End With
                    Next
                End If
                'Get order project information details Navin 03-03-2015

                Dim plantscheme As String
                plantscheme = ""

                Dim dataProjectDetail = linq_obj.SP_Select_Tbl_ProjectDetail(Address_ID).ToList()
                If (dataProjectDetail.Count > 0) Then
                    For Each item As SP_Select_Tbl_ProjectDetailResult In dataProjectDetail
                        plantscheme = plantscheme + item.PlantScheme + " , "
                    Next

                    txtSScheme.Text = plantscheme.Trim()


                End If





                'rajesh
                Dim dataEcEng = linq_obj.SP_Tbl_Service_ECEngRepMaster_Select(Address_ID).ToList()
                If (dataEcEng.Count > 0) Then
                    For Each item As SP_Tbl_Service_ECEngRepMaster_SelectResult In dataEcEng

                        With item
                            If (item.IsDone.Value = True) Then
                                chkECDone.Checked = True
                                dtECDone.Text = Class1.Datecheck(item.ECDoneDate.Value)
                            Else
                                chkECDone.Checked = False
                                dtECDone.Visible = False

                            End If
                        End With
                    Next

                End If

                Dim dataECEngDetail = linq_obj.SP_Tbl_Service_ECEngineerReportDetail_Select(Address_ID).ToList()

                If (dataECEngDetail.Count > 0) Then
                    For Each item As SP_Tbl_Service_ECEngineerReportDetail_SelectResult In dataECEngDetail

                        With item
                            Dim dr As DataRow
                            dr = tblEngFollowup.NewRow()
                            dr("Date") = Class1.Datecheck(item.ECRepDate)
                            dr("StartTime") = Convert.ToString(item.ECStartTime)
                            dr("EndTime") = Convert.ToString(item.ECEndTime)
                            dr("WorkDone") = Convert.ToString(item.ECWorkDone)
                            dr("isDelay") = Convert.ToString(item.ECIsWorkDelay)
                            dr("DelayRemarks") = Convert.ToString(item.ECWorkDelayRemarks)
                            dr("InstallationFor") = Convert.ToString(item.ECInstallationFor)
                            dr("Remarks") = Convert.ToString(item.ECRemarks)
                            tblEngFollowup.Rows.Add(dr)
                        End With
                    Next
                    DGEFDetails.DataSource = tblEngFollowup
                End If



                Dim dataECDone = linq_obj.SP_Tbl_Service_ECDoneDetail_Select(Address_ID).ToList()
                If (dataECDone.Count > 0) Then
                    For Each item As SP_Tbl_Service_ECDoneDetail_SelectResult In dataECDone

                        With item


                            Dim dr As DataRow
                            dr = tblECDone.NewRow()
                            dr("InstallationType") = Convert.ToString(item.ECInstallationType)
                            dr("isDone") = Convert.ToString(item.isDone)
                            dr("DoneDate") = Class1.Datecheck(item.ECDate)
                            tblECDone.Rows.Add(dr)
                        End With
                    Next
                    DGVECDoneDetail.DataSource = tblECDone
                    DGVECDoneDetail.Columns(1).HeaderText = "Done"
                    DGVECDoneDetail.Columns(2).HeaderText = "Done Date"

                End If


                Dim dataTPR = linq_obj.SP_Tbl_Service_TechPerformanceRepDetail_Select(Address_ID).ToList()
                If (dataTPR.Count > 0) Then
                    For Each item As SP_Tbl_Service_TechPerformanceRepDetail_SelectResult In dataTPR
                        With item
                            Dim dr As DataRow
                            dr = tblTPR.NewRow()
                            dr("EngineerName") = Convert.ToString(item.EngineerName)
                            dr("ReportBy") = Convert.ToString(item.ReportBy)
                            dr("PerformanceCategory") = Convert.ToString(item.PerformanceCategory)
                            dr("Remarks") = Convert.ToString(item.Remarks)
                            tblTPR.Rows.Add(dr)
                        End With
                    Next
                    DGEPFDetail.DataSource = tblTPR
                    DGEPFDetail.Columns(0).HeaderText = "Engineer Name"
                    DGEPFDetail.Columns(2).HeaderText = "Performance Category"
                End If


                Dim dataService = linq_obj.SP_Tbl_Service_ServiceDetail_Select(Address_ID).ToList()
                If (dataService.Count > 0) Then
                    For Each item As SP_Tbl_Service_ServiceDetail_SelectResult In dataService
                        With item
                            dtServDelDate.Value = item.DelServiceDate
                            txtSerEngName.Text = item.EngineerName
                            txtSerRemarks.Text = item.Remarks
                            txtITDS.Text = Convert.ToString(item.I_TDS)
                            txtITH.Text = Convert.ToString(item.I_TH)
                            txtIPH.Text = Convert.ToString(item.I_PH)
                            txtIRemarks.Text = item.I_Remarks
                            txtOTDS.Text = Convert.ToString(item.O_TDS)
                            txtOTH.Text = Convert.ToString(item.O_TH)
                            txtOPH.Text = Convert.ToString(item.O_PH)
                            txtORemarks.Text = Convert.ToString(item.O_Remarks)
                            txtSerType.Text = Convert.ToString(item.ServiceType)
                            'If (item.isDone.Value = True) Then
                            '    chkServiceDone.Checked = True
                            '    dtServiceDoneDate.Text = Convert.ToString(item.DoneDate.Value.ToShortDateString())
                            'Else
                            '    chkServiceDone.Checked = False
                            '    dtServiceDoneDate.Visible = False

                            'End If
                        End With
                    Next
                End If

                Dim datachkLog = linq_obj.SP_Tbl_Service_CheckLogDetail_Select(Address_ID).ToList()
                If (datachkLog.Count > 0) Then
                    For Each item As SP_Tbl_Service_CheckLogDetail_SelectResult In datachkLog
                        With item
                            Dim dr As DataRow
                            dr = tblChkLog.NewRow()
                            dr("Title") = Convert.ToString(item.ChkLogTitle)
                            dr("isDone") = Convert.ToString(IIf(item.isDone.Value, True, False))
                            dr("Date") = Class1.Datecheck(item.LogDate)
                            dr("Remarks") = Convert.ToString(item.Remarks)
                            tblChkLog.Rows.Add(dr)
                        End With
                    Next
                    DGCheckLog.DataSource = tblChkLog
                End If

                Dim dataSparemaster = linq_obj.SP_Tbl_Service_SpareMaster_Select(Address_ID).ToList()
                If (dataSparemaster.Count > 0) Then
                    For Each item As SP_Tbl_Service_SpareMaster_SelectResult In dataSparemaster
                        With item

                            txtSpareEngName.Text = item.DispatchBy
                            txtSpareRecBy.Text = item.RecieveBy
                            txtSpareRemarks.Text = item.Remarks
                            txtDisVia.Text = item.DispatchVia
                            dtSpareDate.Text = Class1.Datecheck(item.SpareOrderDate.Value)
                        End With
                    Next
                End If

                Dim dataSpareDetail = linq_obj.SP_Tbl_Service_SpareDetail_Select(Address_ID).ToList()
                If (dataSpareDetail.Count > 0) Then
                    For Each item As SP_Tbl_Service_SpareDetail_SelectResult In dataSpareDetail
                        With item
                            Dim dr As DataRow
                            dr = tblSpareDetail.NewRow()
                            dr("ItemName") = Convert.ToString(item.ItemName)
                            dr("Qty") = Convert.ToString(item.Qty)
                            dr("Price") = Convert.ToString(item.Price)
                            dr("Amount") = Convert.ToString(item.Amount)
                            tblSpareDetail.Rows.Add(dr)
                        End With
                    Next
                    DGVSpareDetails.DataSource = tblSpareDetail
                End If

                Dim dataGenservice = linq_obj.SP_Tbl_Service_GeneralServiceMaster_Select(Address_ID).ToList()
                If (dataGenservice.Count > 0) Then
                    For Each item As SP_Tbl_Service_GeneralServiceMaster_SelectResult In dataGenservice
                        With item

                            dtGSDate.Text = Class1.Datecheck(item.GSDate.Value)
                            txtSerType.Text = item.ServiceType
                            txtComplainNo.Text = item.ComplainNo
                            txtAtendedBy.Text = item.AttendBy
                            txtEnginer.Text = item.Engineer
                            dtAttendDate.Text = Class1.Datecheck(item.AttendDate.Value)

                        End With
                    Next
                End If



                Dim dataGenServiceDetail = linq_obj.SP_Tbl_Service_GeneralServiceDetail_Select(Address_ID).ToList()
                If (dataGenServiceDetail.Count > 0) Then
                    For Each item As SP_Tbl_Service_GeneralServiceDetail_SelectResult In dataGenServiceDetail
                        With item
                            Dim dr As DataRow
                            dr = tblGenService.NewRow()
                            dr("FollowUp") = Convert.ToString(item.FollowUp)
                            dr("TentativeDate") = Class1.Datecheck(item.TentativeADate)
                            dr("Status") = Convert.ToString(item.Status)
                            dr("Remarks") = Convert.ToString(item.Remarks)
                            tblGenService.Rows.Add(dr)
                        End With
                    Next
                    DGVGenService.DataSource = tblGenService
                End If



                Dim dataClientDetail = linq_obj.SP_Tbl_Service_ContactInfo_Select(Address_ID).ToList()
                If (dataClientDetail.Count > 0) Then
                    For Each item As SP_Tbl_Service_ContactInfo_SelectResult In dataClientDetail
                        With item
                            Dim dr As DataRow
                            dr = tblContactDetail.NewRow()
                            dr("Name") = Convert.ToString(item.Name)
                            dr("Post") = Convert.ToString(item.Post)
                            dr("Contact") = Convert.ToString(item.ContactNo)
                            dr("Email") = Convert.ToString(item.EmailId)
                            tblContactDetail.Rows.Add(dr)
                        End With
                    Next
                    DGClientDetail.DataSource = tblContactDetail
                End If
                cmbService.DataSource = Nothing

                Dim dataService1 = linq_obj.SP_Get_ServiceList(Address_ID).ToList()
                If (dataService1.Count) Then
                    cmbService.DataSource = dataService1
                    cmbService.DisplayMember = "ServiceType"
                    cmbService.ValueMember = "ServiceType"
                    cmbService.AutoCompleteMode = AutoCompleteMode.Append
                    cmbService.DropDownStyle = ComboBoxStyle.DropDownList
                    cmbService.AutoCompleteSource = AutoCompleteSource.ListItems
                    lblServiceType.Text = dataService(0).ServiceType
                End If
            Else
                btnSave.Enabled = True
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim result As DialogResult = MessageBox.Show("Are You Sure?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If result = DialogResult.Yes Then

            '/Letter Header
            linq_obj.SP_Tbl_Service_ProjectDetail_Delete(Address_ID)
            linq_obj.SubmitChanges()

            '/Letter Detail
            linq_obj.SP_Tbl_Service_PartyReadynessMaster_Delete(Address_ID)
            linq_obj.SubmitChanges()

            '/Product Inst
            linq_obj.SP_Tbl_Service_PartyReadynessDetail_Delete(Address_ID)
            linq_obj.SubmitChanges()
            '/ISI Header
            linq_obj.SP_Tbl_Service_ErrectionCommMaster_Delete(Address_ID)
            linq_obj.SubmitChanges()
            '/ISI Header
            linq_obj.SP_Tbl_Service_ErrectionCommDetails_Delete(Address_ID)
            linq_obj.SubmitChanges()


            '/ISI Header
            linq_obj.SP_Tbl_Service_PhysicalVerificationDayOneMaster_Delete(Address_ID)
            linq_obj.SubmitChanges()


            '/ISI Header
            linq_obj.SP_Tbl_Service_Physical_WaterTreatment_Master_Delete(Address_ID)
            linq_obj.SubmitChanges()

            '/ISI Detail
            linq_obj.SP_Tbl_Service_PartyRemarksMaster_Delete(Address_ID)
            linq_obj.SubmitChanges()

            ''Service For 
            linq_obj.SP_Tbl_Service_ServiceLogMaster_Delete(Address_ID)
            linq_obj.SubmitChanges()

            ''Service Details
            linq_obj.SP_Tbl_Service_ServiceDetail_Delete(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Tbl_Service_ContactInfo_Delete(Address_ID)
            linq_obj.SubmitChanges()

            'Service log delails

            linq_obj.SP_Tbl_Service_CheckLogDetail_Delete(Address_ID)
            linq_obj.SubmitChanges()

            'delete Old Data
            linq_obj.SP_Tbl_Service_ECEngineerReportDetail_Delete(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Tbl_Service_TechPerformanceRepDetail_Delete(Address_ID)
            linq_obj.SubmitChanges()

            ''Tbl_Service_SpareMaster

            linq_obj.SP_Tbl_Service_SpareMaster_Delete(Address_ID)
            linq_obj.SubmitChanges()

            ''Tbl_Service_SpareDetail

            linq_obj.SP_Tbl_Service_SpareDetail_Delete(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Tbl_Service_GeneralServiceMaster_Delete(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Tbl_Service_GeneralServiceDetail_Delete(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Tbl_Service_ECEngRepMaster_Delete(Address_ID)
            linq_obj.SubmitChanges()

            linq_obj.SP_Tbl_Service_ECDoneDetail_Delete(Address_ID)
            linq_obj.SubmitChanges()

            '/Service master Delete
            linq_obj.SP_Delete_Tbl_ServiceMaster(Address_ID)
            linq_obj.SubmitChanges()

            btnAdd_Click(Nothing, Nothing)
            GvInEnq_Bind()
            MessageBox.Show("Successfully Deleted")
            clearAll()

        End If

    End Sub

    Private Sub btnAddAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAddress.Click
        Dim dr As DataRow
        dr = tblContactDetail.NewRow()

        If txtName.Text = "" Then
            MessageBox.Show("Name cannot be blank")
            Exit Sub
        End If

        dr("Name") = txtName.Text
        dr("Post") = txtPost.Text
        dr("Contact") = txtClientContactNo.Text
        dr("Email") = txtClientEmail.Text

        If tblContactDetail.Rows.Count < rwIDDelContectDetail Then
            MessageBox.Show("Grid Postion is not found")
            Exit Sub
        End If

        If rwIDDelContectDetail >= 0 Then
            tblContactDetail.Rows(rwIDDelContectDetail)(0) = dr("Name")
            tblContactDetail.Rows(rwIDDelContectDetail)(1) = dr("Post")
            tblContactDetail.Rows(rwIDDelContectDetail)(2) = dr("Contact")
            tblContactDetail.Rows(rwIDDelContectDetail)(3) = dr("Email")
        Else
            linq_obj.SP_Tbl_Service_ContactInfo_Insert(Address_ID, txtName.Text, txtPost.Text, txtClientContactNo.Text, txtClientEmail.Text)
            linq_obj.SubmitChanges()
            tblContactDetail.Rows.Add(dr)
        End If

        DGClientDetail.DataSource = tblContactDetail
        txtName.Text = ""
        txtPost.Text = ""
        txtClientContactNo.Text = ""
        txtClientEmail.Text = ""
        rwIDDelContectDetail = -1
    End Sub

    Private Sub btnclientAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclientAdd.Click
        txtName.Text = ""
        txtPost.Text = ""
        txtClientContactNo.Text = ""
        txtClientEmail.Text = ""
    End Sub

    Private Sub btnDelClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelClient.Click
        If DGClientDetail.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblContactDetail.Rows(DGClientDetail.CurrentRow.Index).Delete()
                DGClientDetail.DataSource = tblContactDetail
            End If
        End If

    End Sub

    Private Sub txtpendingMaterials_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpendingMaterials.TextChanged

    End Sub

    Private Sub Label43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label43.Click

    End Sub

    Private Sub TCorderMast_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TCServiceMaster.SelectedIndexChanged

    End Sub

    Private Sub txtAddQuality_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddQuality.Click
        txtRawWaterValue.Text = ""
        cmbParameters.SelectedIndex = -1
        txtTreatedWaterValue.Text = ""
    End Sub

    Private Sub btnAddWater_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddWater.Click
        txtWaterPoint.Text = ""
        chkWaterRemark.Checked = False
        txtWaterRemark.Text = ""
        dtPartyReadyDate.Text = ""
        dtpCompletionDate.Text = ""

    End Sub

    Private Sub btnAddPartyReady_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPartyReady.Click
        txtRemarks.Text = ""
        txtTitleReady.Text = ""
        rbYes.Checked = True
        dtPartyReadyDate.Text = ""
        dtPartyReadyDate.Focus()
    End Sub


    Private Sub txtGSComplainNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGSComplainNo.Leave
        Try
            If (txtGSComplainNo.Text <> "") Then
                Dim cmd As New SqlCommand
                cmd.CommandText = "SP_Get_ComplainDetails"
                cmd.Parameters.Add("@ComplainNo", SqlDbType.VarChar).Value = txtGSComplainNo.Text
                ' cmd.Parametds.Tables(1)ers.Add("@AddressID", SqlDbType.BigInt).Value = Address_ID

                Dim objclass As New Class1

                Dim ds As DataSet
                ds = objclass.GetSearchData(cmd)
                If ds.Tables(1).Rows.Count < 1 Then
                    grpComplain.Visible = False

                    MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else

                    grpComplain.Visible = True
                    lblComplainDate.Text = Convert.ToString(ds.Tables(1).Rows(0)(0))
                    lblComplainDoneDate.Text = Convert.ToString(ds.Tables(1).Rows(0)("DoneDate"))
                    lblComplainEngName.Text = Convert.ToString(ds.Tables(1).Rows(0)("Engineer"))
                    lblComplainStatus.Text = Convert.ToString(ds.Tables(1).Rows(0)("ComlainStatus"))
                    lblComplainType.Text = Convert.ToString(ds.Tables(1).Rows(0)("ServiceType"))

                End If
                ds.Dispose()
            Else

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub btnECDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnECDone.Click
        If (chkECDone.Checked = True AndAlso dtECDone.Text <> "") Then
            Dim result As DialogResult = MessageBox.Show("Is Errection Commissioning Done?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then

                linq_obj.SP_Tbl_Service_ServiceLogMaster_Delete(Address_ID)
                linq_obj.SubmitChanges()

                For index = 1 To 5
                    If (index = 1) Then
                        linq_obj.SP_Tbl_Service_ServiceLogMaster_Insert(Address_ID, "Service" + index.ToString(), Convert.ToDateTime(dtECDone.Text).AddMonths(1), "Pending", DateTime.Now)
                    ElseIf (index = 2) Then
                        linq_obj.SP_Tbl_Service_ServiceLogMaster_Insert(Address_ID, "Service" + index.ToString(), Convert.ToDateTime(dtECDone.Text).AddMonths(3), "Pending", DateTime.Now)
                    ElseIf (index = 3) Then
                        linq_obj.SP_Tbl_Service_ServiceLogMaster_Insert(Address_ID, "Service" + index.ToString(), Convert.ToDateTime(dtECDone.Text).AddMonths(6), "Pending", DateTime.Now)
                    ElseIf (index = 4) Then
                        linq_obj.SP_Tbl_Service_ServiceLogMaster_Insert(Address_ID, "Service" + index.ToString(), Convert.ToDateTime(dtECDone.Text).AddMonths(9), "Pending", DateTime.Now)
                    ElseIf (index = 5) Then
                        linq_obj.SP_Tbl_Service_ServiceLogMaster_Insert(Address_ID, "Service" + index.ToString(), Convert.ToDateTime(dtECDone.Text).AddMonths(12), "Pending", DateTime.Now)
                    End If
                Next


                Dim dataService = linq_obj.SP_Get_ServiceList(Address_ID).ToList()
                If (dataService.Count) Then
                    cmbService.DataSource = dataService
                    cmbService.DisplayMember = "ServiceType"
                    cmbService.ValueMember = "ServiceType"
                    'cmbService.AutoCompleteMode = AutoCompleteMode.Append
                    'cmbService.DropDownStyle = ComboBoxStyle.DropDownList
                    'cmbService.AutoCompleteSource = AutoCompleteSource.ListItems
                    lblServiceType.Text = dataService(0).ServiceType
                End If
            End If
        Else
            MessageBox.Show("Select Errection Commission Done")

        End If




    End Sub


    Private Sub dtPartyReadyDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtPartyReadyDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub
    Private Sub dtPartyReadyDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtPartyReadyDate.Validating
        If Class1.ChkVaildDate(dtPartyReadyDate.Text) = False Then
            MessageBox.Show("Date format is not valid,Pls Check Date")
            e.Cancel = True
        End If
    End Sub

    

    Private Sub dtpCompletionDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpCompletionDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtpCompletionDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpCompletionDate.TextChanged

    End Sub

    Private Sub dtpCompletionDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpCompletionDate.Validating
        If Class1.ChkVaildDate(dtpCompletionDate.Text) = False Then
            MessageBox.Show("Date format is not valid,Pls Check Date")
            e.Cancel = True
        End If

        If dtPartyReadyDate.Text <> "" And dtpCompletionDate.Text <> "" Then
            If Convert.ToDateTime(dtPartyReadyDate.Text) > Convert.ToDateTime(dtpCompletionDate.Text) Then
                MessageBox.Show("You cannot enter completiondate less than start date")
                e.Cancel = True
            End If
        End If
    End Sub



    Private Sub DGPRDetail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGPRDetail.DoubleClick
        Try
            If DGPRDetail.Rows.Count > 0 Then
                Dim iRow As Integer = DGPRDetail.CurrentCell.RowIndex
                Dim strStatus As String = ""

                txtTitleReady.Text = DGPRDetail.Rows(iRow).Cells(PartyReadyness.HeaderName).Value
                strStatus = DGPRDetail.Rows(iRow).Cells(PartyReadyness.Status).Value

                If strStatus = rbYes.Text Then
                    rbYes.Checked = True
                ElseIf strStatus = rbNo.Text Then
                    rbNo.Checked = True
                ElseIf strStatus = rbprocess.Text Then
                    rbprocess.Checked = True
                End If

                dtpCompletionDate.Text = Class1.Datecheck(DGPRDetail.Rows(iRow).Cells(PartyReadyness.CompletionDate).Value)
                dtPartyReadyDate.Text = Class1.Datecheck(DGPRDetail.Rows(iRow).Cells(PartyReadyness.PartyReadyDate).Value)
                txtRemarks.Text = DGPRDetail.Rows(iRow).Cells(PartyReadyness.Remarks).Value

                rwIDDelPartyReadyness = DGPRDetail.CurrentCell.RowIndex
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtPartyReadyDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPartyReadyDate.TextChanged

    End Sub

    Private Sub Label101_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dtAttendDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtAttendDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtAttendDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtAttendDate.TextChanged

    End Sub

    Private Sub dtAttendDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtAttendDate.Validating
        If Class1.ChkVaildDate(dtAttendDate.Text) = False Then
            MessageBox.Show("Date is not valid ,Pls Check Date")
            e.Cancel = True
        End If
    End Sub


    Private Sub dtTentativeAttendDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtTentativeAttendDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtTentativeAttendDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtTentativeAttendDate.Validating
        If Class1.ChkVaildDate(dtTentativeAttendDate.Text) = False Then
            MessageBox.Show("Date is not valid ,Pls Check Date")
            e.Cancel = True
        End If

    End Sub

    Private Sub txtEntryNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEntryNo.TextChanged

    End Sub

    Private Sub dtServicedate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtServicedate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtServicedate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtServicedate.TextChanged

    End Sub

    Private Sub dtServicedate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtServicedate.Validating
        If Class1.ChkVaildDate(dtServicedate.Text) = False Then
            MessageBox.Show("Date format is not valid")
            e.Cancel = True
        End If
    End Sub

    Private Sub DGVServiceDetails_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVServiceDetails.CellContentClick

    End Sub

    Private Sub dtpSInspectionDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpSInspectionDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtpSInspectionDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpSInspectionDate.TextChanged

    End Sub

    Private Sub dtpSInspectionDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpSInspectionDate.Validating
        If Class1.ChkVaildDate(dtpSInspectionDate.Text) = False Then
            MessageBox.Show("Date format is not valid")
            e.Cancel = True
        End If
    End Sub

    Private Sub dtpErrectionDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpErrectionDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtpErrectionDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpErrectionDate.TextChanged

    End Sub

    Private Sub dtpErrectionDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpErrectionDate.Validating
        If Class1.ChkVaildDate(dtpErrectionDate.Text) = False Then
            MessageBox.Show("Date formate is not valid")
            e.Cancel = True
        End If
    End Sub

    Private Sub DGPRDetail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGPRDetail.CellContentClick

    End Sub

    Private Sub dgvErrectionDetails_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvErrectionDetails.CellContentClick

    End Sub

    Private Sub dgvErrectionDetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvErrectionDetails.DoubleClick
        Try
            If dgvErrectionDetails.Rows.Count > 0 Then
                Dim iRow As Integer
                iRow = dgvErrectionDetails.CurrentRow.Index
                txtRawWaterValue.Text = dgvErrectionDetails.Rows(iRow).Cells(Quality.RawWater).Value
                cmbParameters.Text = dgvErrectionDetails.Rows(iRow).Cells(Quality.Parameter).Value
                txtTreatedWaterValue.Text = dgvErrectionDetails.Rows(iRow).Cells(Quality.TreatedWater).Value
                rwIDDelErrection = dgvErrectionDetails.CurrentRow.Index
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGWaterDetail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGWaterDetail.DoubleClick
        Try
            If DGWaterDetail.Rows.Count > 0 Then
                txtWaterPoint.Text = DGWaterDetail.SelectedCells(0).Value
                chkWaterRemark.Checked = DGWaterDetail.SelectedCells(1).Value
                txtWaterRemark.Text = DGWaterDetail.SelectedCells(2).Value

                rwIDDelWaterDetail = DGWaterDetail.CurrentCell.RowIndex
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGWaterDetail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGWaterDetail.CellContentClick

    End Sub

    Private Sub DGClientDetail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGClientDetail.CellContentClick

    End Sub

    Private Sub DGClientDetail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGClientDetail.DoubleClick
        Try
            If DGClientDetail.Rows.Count > 0 Then
                Dim iRow As Integer
                iRow = DGClientDetail.CurrentRow.Index
                txtName.Text = DGClientDetail.Rows(iRow).Cells(0).Value
                txtPost.Text = DGClientDetail.Rows(iRow).Cells(1).Value
                txtClientContactNo.Text = DGClientDetail.Rows(iRow).Cells(2).Value
                txtClientEmail.Text = DGClientDetail.Rows(iRow).Cells(3).Value
                rwIDDelContectDetail = iRow
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgvErrectionDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvErrectionDetails.KeyDown
        If e.KeyCode = Keys.D Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure All Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblQuality.Rows.Clear()
                dgvErrectionDetails.DataSource = tblQuality
            End If
        End If
    End Sub

    Private Sub DGEFDetails_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGEFDetails.CellContentClick

    End Sub

    Private Sub DGEFDetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGEFDetails.DoubleClick
        Try
            If DGEFDetails.Rows.Count > 0 Then
                Dim iRow As Integer
                iRow = DGEFDetails.CurrentRow.Index
                i_EditDGEFDetails = iRow
                dtEFDate.Text = Class1.Datecheck(DGEFDetails.Rows(iRow).Cells(0).Value)
                tmStartTime.Text = DGEFDetails.Rows(iRow).Cells(1).Value
                tmEndTime.Text = DGEFDetails.Rows(iRow).Cells(2).Value
                txtEFInstallationFor.Text = DGEFDetails.Rows(iRow).Cells(3).Value
                txtWorkDone.Text = DGEFDetails.Rows(iRow).Cells(4).Value
                chkWorkDelay.Checked = DGEFDetails.Rows(iRow).Cells(5).Value
                txtWorkDelayRemarks.Text = DGEFDetails.Rows(iRow).Cells(6).Value
                txtEFRemarks.Text = DGEFDetails.Rows(iRow).Cells(7).Value
                b_EditDGEFDetails = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtEFDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtEFDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtEFDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtEFDate.TextChanged

    End Sub

    Private Sub dtEFDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtEFDate.Validating
        If Class1.ChkVaildDate(dtEFDate.Text) = False Then
            MessageBox.Show("Date formate is not valid ")
            e.Cancel = True
        End If
    End Sub

    Private Sub dtECDDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtECDDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtECDDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtECDDate.TextChanged

    End Sub

    Private Sub dtECDDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtECDDate.Validating
        If Class1.ChkVaildDate(dtECDDate.Text) = False Then
            MessageBox.Show("Date formate not valid ")
            e.Cancel = True
        End If

    End Sub

    Private Sub DGVECDoneDetail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVECDoneDetail.CellContentClick

    End Sub

    Private Sub DGVECDoneDetail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVECDoneDetail.DoubleClick
        Try
            If DGVECDoneDetail.Rows.Count > 0 Then
                Dim iRow As Integer
                iRow = DGVECDoneDetail.CurrentRow.Index
                i_EditDGVECDoneDetail = iRow
                txtECInstallationFor.Text = DGVECDoneDetail.Rows(iRow).Cells(0).Value
                chkDoneEC.Checked = DGVECDoneDetail.Rows(iRow).Cells(1).Value
                dtECDDate.Text = Class1.Datecheck(DGVECDoneDetail.Rows(iRow).Cells(2).Value)
                b_EditDGVECDoneDetail = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGEPFDetail_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGEPFDetail.CellContentClick

    End Sub

    Private Sub DGEPFDetail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGEPFDetail.DoubleClick

        Try

            If DGEPFDetail.Rows.Count > 0 Then
                Dim strpercat As String
                Dim iRow As Integer
                iRow = DGEPFDetail.CurrentRow.Index
                i_EditDGEPFDetail = iRow
                txtEngName.Text = DGEPFDetail.Rows(iRow).Cells(0).Value
                txtReportBy.Text = DGEPFDetail.Rows(iRow).Cells(1).Value
                strpercat = IIf(IsDBNull(DGEPFDetail.Rows(iRow).Cells(2).Value), "", DGEPFDetail.Rows(iRow).Cells(2).Value)
                txtEPRemarks.Text = DGEPFDetail.Rows(iRow).Cells(3).Value
                If strpercat = "Good" Then
                    rbGood.Checked = True
                ElseIf strpercat = "Better" Then
                    rbBetter.Checked = True
                ElseIf strpercat = "Excellent" Then
                    rbExcellent.Checked = True
                ElseIf strpercat = "Not Good" Then
                    rbNotGood.Checked = True
                End If
                b_EditDGEPFDetail = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtChklogDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtChklogDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub


    Private Sub dtChklogDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtChklogDate.Validating
        If Class1.ChkVaildDate(dtChklogDate.Text) = False Then
            MessageBox.Show("Date formate is not valid")
            e.Cancel = True
        End If
    End Sub

    Private Sub DGCheckLog_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGCheckLog.CellContentClick

    End Sub

    Private Sub DGCheckLog_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGCheckLog.DoubleClick
        Try
            'linq_obj.SP_Tbl_Service_CheckLogDetail_Insert(Address_ID, txtChklogTitle.Text, IIf(chkChkLogisDone.Checked, True, False), Convert.ToDateTime(dtChklogDate.Text), txtChkLogRemarks.Text)
            If DGCheckLog.Rows.Count > 0 Then
                Dim iRow As Integer
                iRow = DGCheckLog.CurrentRow.Index
                txtChklogTitle.Text = DGCheckLog.Rows(iRow).Cells(0).Value
                chkChkLogisDone.Checked = DGCheckLog.Rows(iRow).Cells(1).Value
                dtChklogDate.Text = DGCheckLog.Rows(iRow).Cells(2).Value
                txtChkLogRemarks.Text = DGCheckLog.Rows(iRow).Cells(3).Value
                i_EditDGCheckLog = iRow
                b_EditDGCheckLog = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGVSpareDetails_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVSpareDetails.CellContentClick

    End Sub

    Private Sub DGVSpareDetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVSpareDetails.DoubleClick
        Try
            If DGVSpareDetails.Rows.Count > 0 Then

                Dim iRow As Integer
                iRow = DGVSpareDetails.CurrentRow.Index
                'linq_obj.SP_Tbl_Service_SpareDetail_Insert(Address_ID, txtItemName.Text, Convert.ToInt32(txtQty.Text), Convert.ToDecimal(txtPrice.Text), Convert.ToDecimal(txtAmount.Text))
                txtItemName.Text = DGVSpareDetails.Rows(iRow).Cells(0).Value
                txtQty.Text = DGVSpareDetails.Rows(iRow).Cells(1).Value
                txtPrice.Text = DGVSpareDetails.Rows(iRow).Cells(2).Value
                txtAmount.Text = DGVSpareDetails.Rows(iRow).Cells(3).Value
                i_EditDGVSpareDetails = iRow
                b_EditDGVSpareDetails = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DGVGenService_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVGenService.CellContentClick

    End Sub

    Private Sub DGVGenService_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVGenService.DoubleClick
        'linq_obj.SP_Tbl_Service_GeneralServiceDetail_Insert(Address_ID, txtGenSerFollowup.Text,
        '                                                     Class1.SetDate(dtTentativeAttendDate.Text),
        '                                                      txtGenServiceStatus.Text,
        '                                                      txtGenSerRem.Text
        '                                                     )
        Try
            If DGVGenService.Rows.Count > 0 Then
                Dim iRow As Integer

                iRow = DGVGenService.CurrentRow.Index
                txtGenSerFollowup.Text = DGVGenService.Rows(iRow).Cells(0).Value
                dtTentativeAttendDate.Text = Class1.Datecheck(DGVGenService.Rows(iRow).Cells(1).Value)
                txtGenServiceStatus.Text = DGVGenService.Rows(iRow).Cells(2).Value
                txtGenSerRem.Text = DGVGenService.Rows(iRow).Cells(3).Value
                i_EditDGVGenService = iRow
                b_EditDGVGenService = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnDelfolwup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelfolwup.Click
        If DGVGenService.Rows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                tblGenService.Rows(DGVGenService.CurrentRow.Index).Delete()
                DGVGenService.DataSource = tblGenService
            End If
        End If

    End Sub

    Private Sub txtBatchName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtPartyName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartyName.TextChanged

    End Sub

    Private Sub dtGSDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtGSDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtGSDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtGSDate.TextChanged

    End Sub

    Private Sub DGVServiceDetails_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVServiceDetails.RowEnter

    End Sub

    Private Sub DGVServiceDetails_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles DGVServiceDetails.PreviewKeyDown

    End Sub

    Private Sub DGVServiceDetails_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DGVServiceDetails.KeyPress

    End Sub

    Private Sub DGVServiceDetails_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGVServiceDetails.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            BindAllGridData(Convert.ToInt32(DGVServiceDetails.Rows(DGVServiceDetails.CurrentRow.Index).Cells(0).Value))
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        GvInEnq_Bind()
    End Sub

    Private Sub txtClientContactNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClientContactNo.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtClientContactNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClientContactNo.TextChanged

    End Sub

    Private Sub DGVServiceDetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVServiceDetails.DoubleClick

    End Sub

    Private Sub txtContactNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContactNo.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtContactNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContactNo.TextChanged

    End Sub

    Private Sub dtECDone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtECDone.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub

    Private Sub dtECDone_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtECDone.TextChanged

    End Sub

    Private Sub dtECDone_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtECDone.Validating
        If Class1.ChkVaildDate(dtECDone.Text) = False Then
            MessageBox.Show("Date format is not valid ")
            e.Cancel = True
        End If
    End Sub

    Private Sub tmStartTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tmStartTime.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub tmStartTime_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmStartTime.TextChanged

    End Sub

    Private Sub tmEndTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tmEndTime.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub tmEndTime_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmEndTime.TextChanged

    End Sub

    Private Sub dtServiceDoneDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtServiceDoneDate.KeyPress, dtSpareDate.KeyPress
        e.Handled = Class1.IsDateFormate(sender, e)
    End Sub


    Private Sub dtServiceDoneDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtServiceDoneDate.TextChanged

    End Sub

    Private Sub dtServiceDoneDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtServiceDoneDate.Validating, dtSpareDate.Validating
        If Class1.ChkVaildDate(sender.Text) = False Then
            MessageBox.Show("Date format is not valid")
            e.Cancel = True
        End If
    End Sub

    Private Sub dtSpareDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtSpareDate.TextChanged

    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged

    End Sub

    Private Sub TPClientDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TPClientDetail.Click

    End Sub
End Class
