Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.CrystalReports
Imports System.Data
Imports System.Data.SqlClient

Public Class MDIMainForm

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Private Const CP_NOCLOSE_BUTTON As Integer = &H200
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property
    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub ShowForm(ByVal _frm As Form)
        _frm.MdiParent = Me
        _frm.MaximizeBox = False
        _frm.MinimizeBox = False
        _frm.StartPosition = FormStartPosition.CenterScreen
        _frm.KeyPreview = True
        '  _frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
        AddHandler _frm.KeyDown, AddressOf Form_KeyDown
        _frm.Show()
    End Sub
    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim frm As New Form
        frm = DirectCast(sender, Form)

        If e.KeyCode = 13 Then
            SendKeys.Send("{TAB}")
        End If
        If e.KeyCode = 27 Then
            frm.Close()
        End If
    End Sub
    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        'Dim frmParent As New MDIMainForm()
        Dim frmuser As New UserMaster()
        ShowForm(frmuser)
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer

    Private Sub PrintSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frmcategory1 As New CategoryMaster
        ShowForm(frmcategory1)
    End Sub

    Private Sub EditMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frmchild As New QuotationMaster
        ShowForm(frmchild)

    End Sub

    Private Sub WindowsMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WindowsMenu.Click
        'Me.Close()
        'Dim frm As New Login
        'frm.Show()


    End Sub

    Private Sub ISIQuotation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ISIQuotation.Click
    End Sub


    Private Sub ttOrder_Detail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub tblPurchaseOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub ISIQuotationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ISIQuotationToolStripMenuItem.Click
        Dim frmISI As New ISIQuotation()
        ShowForm(frmISI)
    End Sub

    Private Sub NonISIQuotationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NonISIQuotationToolStripMenuItem.Click
        Dim frmNONISI As New QuotationMaster()
        ShowForm(frmNONISI)
    End Sub

    Private Sub PurchaseOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseOrderToolStripMenuItem.Click
        Dim frmOrderDetail As New frmpurchaseAgreement
        ShowForm(frmOrderDetail)
    End Sub

    Private Sub OrderDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrderDetailsToolStripMenuItem.Click
        Dim frmCompany As New CompanyMaster
        ShowForm(frmCompany)
    End Sub

    Private Sub AddressToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddressToolStripMenuItem.Click
        Dim frmaddres As New AddressForm
        ShowForm(frmaddres)
    End Sub

    Private Sub CourierToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CourierToolStripMenuItem.Click
        Dim frmcourier As New CourierForm
        ShowForm(frmcourier)
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Dim frmcategory As New AddressCateogry
        ShowForm(frmcategory)
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Dim frmsubCategory As New SubCategoryForm
        ShowForm(frmsubCategory)
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Dim frmQuaCategory As New CategoryMaster
        ShowForm(frmQuaCategory)
    End Sub

    Private Sub EnquiryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnquiryToolStripMenuItem.Click
        Dim frmInquery As New InquiryForm
        ShowForm(frmInquery)
    End Sub

    Private Sub InwardRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InwardRegisterToolStripMenuItem.Click
        Dim frmInward As New InwardRegister
        ShowForm(frmInward)
    End Sub

    Private Sub OutwardRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutwardRegisterToolStripMenuItem.Click
        Dim frmOutward As New OutwardRegister
        ShowForm(frmOutward)
    End Sub

    Private Sub POMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frmPO As New POMaster
        ShowForm(frmPO)
    End Sub

    Private Sub ProductRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frmPrd As New StockRegister
        ShowForm(frmPrd)
    End Sub

    Private Sub ReInwardRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frmRE As New Re_Invard_Register
        ShowForm(frmRE)
    End Sub

    Private Sub AccountMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountMasterToolStripMenuItem.Click
        Dim frmParty As New PartyAccountMaster
        ShowForm(frmParty)
    End Sub

    Private Sub InvoiceMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InvoiceMasterToolStripMenuItem.Click
        'Dim frmPartyInvoice As New PartyInvoiceMaster
        'ShowForm(frmPartyInvoice)
    End Sub
    Private Sub clickHandler(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' MsgBox(DirectCast(sender, ToolStripMenuItem).Name)
        Dim strmenu As String
        Try
            Class1.global.ParentMenu = Convert.ToString(DirectCast(sender, ToolStripMenuItem).OwnerItem.Name)
        Catch ex As Exception
            Class1.global.ParentMenu = ""
        End Try
        strmenu = DirectCast(sender, ToolStripMenuItem).Name
        Select Case strmenu.Trim()
            Case "Inquiry Manager"
                Dim frmInquery As New InquiryForm
                ShowForm(frmInquery)
            Case "Service Manager"
                Dim srvcMnger As New ServiceManager
                ShowForm(srvcMnger)
            Case "Address Manager"
                '  Dim frmaddres As New AddressForm
                ' ShowForm(frmaddres)
            Case "Party Manager"
            Case "SecondAllotment"
                Dim frmSecond As New SecondAllotment
                ShowForm(frmSecond)
            Case "Order Manager"
                Dim frmorder As New OrderMaster_1_
                ShowForm(frmorder)
            Case "Category Master"
                Dim frmQuaCategory As New CategoryMaster
                ShowForm(frmQuaCategory)
            Case "ISI Quotation"
                Dim frmISI As New ISIQuotation()
                ShowForm(frmISI)
            Case "Non-ISI Quotation"
                Dim frmNONISI As New QuotationMaster()
                ShowForm(frmNONISI)
            Case "Sales Executive"
                Dim frmsalesexec As New SalesExecutiveQuotation()
                ShowForm(frmsalesexec)
            Case "Quotation Allotment"
                Dim frmQuoAll As New QuotationAllotment()
                ShowForm(frmQuoAll)
            Case "Sales Executive Visit & Other"
                Dim frmQuoAll As New SalesExecutiveVisit()
                ShowForm(frmQuoAll)
            Case "Sales Executive Report"
                Dim frmsales As New SalesExecutiveByCriteria()
                ShowForm(frmsales)
            Case "Purchase Order"
                Dim frmOrderDetail As New frmpurchaseAgreement
                ShowForm(frmOrderDetail)
            Case "Order Details"
                Dim frmCompany As New CompanyMaster
                ShowForm(frmCompany)
            Case "Category"
                Dim frmcategory As New AddressCateogry
                ShowForm(frmcategory)
            Case "Address"
                Dim frmaddres As New AddressForm
                ShowForm(frmaddres)
            Case "SubCategory"
                Dim frmsubCategory As New SubCategoryForm
                ShowForm(frmsubCategory)
            Case "Courier"
                Dim frmcourier As New CourierForm
                ShowForm(frmcourier)
            Case "InwardRegister"
                Dim frmInward As New InwardRegister
                ShowForm(frmInward)
            Case "OutwardRegister"
                Dim frmOutward As New OutwardRegister
                ShowForm(frmOutward)
            Case "ProductMaster"
                Dim frmProduct As New ProductRegisterMaster
                ShowForm(frmProduct)
            Case "RowMaterialMaster"
                Dim frmMaterial As New RowMaterialMaster
                ShowForm(frmMaterial)
            Case "View Stock"
                Dim viewStock As New ViewStockDetail
                ShowForm(viewStock)
            Case "Stock Register"
                Dim stckReg As New StockRegister
                ShowForm(stckReg)
            Case "Account Master"
                Dim frmParty As New PartyAccountMaster
                ShowForm(frmParty)
            Case "Invoice Master"
                Dim frmPartyInvoice As New PartyInvoiceMaster
                ShowForm(frmPartyInvoice)
            Case "Main Inquiry Detail"
                Dim frmREP As New InqiurySearchByCriteria
                ShowForm(frmREP)

            Case "Rpt_OrderWeeklyReport"
                Dim frmREP As New Rpt_OrderWeeklyReport
                ShowForm(frmREP)
            Case "Enquiry Detail"
                Dim frmREP As New InquiryReport
                ShowForm(frmREP)
            Case "Inquiry Reference"
                Dim frmREP As New InquiryByReference
                ShowForm(frmREP)
            Case "View Follow Up"
                Dim frmREP As New Report_FollowUp
                ShowForm(frmREP)

            Case "Enq Type"
                Dim frmREP As New EnqReportByEnqType
                ShowForm(frmREP)

            Case "View Visitor Detail"
                Dim frmREP As New RptVisitorDetail
                ShowForm(frmREP)

            Case "StateTeamAllotment"
                Dim frmAccount As New StateTeamAllotment
                ShowForm(frmAccount)
            Case "OrderAccount"
                Dim frmAccount As New AccountMaster
                ShowForm(frmAccount)
            Case "Order Manager"
                Dim frmorder As New OrderMaster_1_
                ShowForm(frmorder)
            Case "Order Followp"
                Dim frmorder As New OrderFollowp
                ShowForm(frmorder)
            Case "User Permission"
                Dim userPermission As New AddUserPermissionMaster
                ShowForm(userPermission)
            Case "Designation"
                Dim dsgMstr As New DesignationMaster
                ShowForm(dsgMstr)
            Case "User"
                Dim frmuser As New UserMaster()
                ShowForm(frmuser)
            Case "User Allotment"
                Dim userAllotment As New UserAllotment
                ShowForm(userAllotment)

            Case "Order Party Detail"
                Dim rpt As New ReportDocument
                Dim ds As New DataSet
                Dim cmd As New SqlCommand
                Dim da As New SqlDataAdapter()

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Get_Rpt_Party_Details"
                cmd.Connection = linq_obj.Connection
                'cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = 0


                da.SelectCommand = cmd
                da.Fill(ds, "Get_Rpt_Party_Details")

                Class1.WriteXMlFile(ds, "Get_Rpt_Party_Details", "Get_Rpt_Party_Details")

                rpt.Load(Application.StartupPath & "\Reports\Rpt_OrderPartyDetail.rpt")

                rpt.Database.Tables(0).SetDataSource(ds.Tables("Get_Rpt_Party_Details"))

                rpt.SetParameterValue("fCompanyName", Class1.g_sCompanyName)


                Dim frmRpt As New FrmCommanReportView(rpt)
                frmRpt.Show()
            Case "Kasar Party Detail"
                Dim rpt As New ReportDocument
                Dim ds As New DataSet
                Dim cmd As New SqlCommand
                Dim da As New SqlDataAdapter()

                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "Get_Rpt_Party_KasarData"
                cmd.Connection = linq_obj.Connection
                'cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = 0

                da.SelectCommand = cmd
                da.Fill(ds, "Get_Rpt_Party_KasarData")
                Class1.WriteXMlFile(ds, "Get_Rpt_Party_KasarData", "Get_Rpt_Party_KasarData")
                rpt.Load(Application.StartupPath & "\Reports\Rpt_PartyKasarDetail.rpt")
                rpt.Database.Tables(0).SetDataSource(ds.Tables("Get_Rpt_Party_KasarData"))
                rpt.SetParameterValue("fCompanyName", Class1.g_sCompanyName)
                Dim frmRpt As New FrmCommanReportView(rpt)
                frmRpt.Show()
            Case "After Dispatch Party Outstanding"
                Dim frm As New PartyOutstandingAfterDispatch
                frm.Show()
            Case "After Dispatch Party Outstanding By Executive"
                Dim frm As New PartyOutstandingAfterDispatch
                frm.Show()
            Case "Payment Type Wise Payment Receive"
                Dim frm As New PaymentReceive
                ShowForm(frm)
            Case "FollowUp"
                Dim frm As New ViewFollowUps
                ShowForm(frm)
            Case "ViewAllotment"
                Dim frm As New Rpt_DailyAllotment
                ShowForm(frm)
            Case "TeamMaster"
                Dim frm As New TeamMaster
                ShowForm(frm)
            Case "Order Report Details"
                Dim frm As New OrderReportgeneral
                ShowForm(frm)
            Case "Letter Log"
                Dim frm As New OrderLatterLogReport
                ShowForm(frm)
            Case "ISI Process"
                Dim frm As New OrderReportISIProcess
                ShowForm(frm)
            Case "Raw Material"
                Dim frm As New OrderReportRawMaterial
                ShowForm(frm)
            Case "Hot List"
                Dim frm As New OrderManagerHotListReport
                ShowForm(frm)
            Case "Logout"
                Me.Close()
                Dim frmLogin As New Login
                Class1.global.UserPermissionDataset.Tables(0).Rows.Clear()
                Class1.global.UserPermissionDataset.Dispose()
                frmLogin.Show()
            Case "Back Up"
                Dim frm As New FrmBackUp
                ShowForm(frm)
            Case "ANNEXURE"
                Dim frm As New frmAnnxure
                ShowForm(frm)
            Case "Engineering Master"
                Dim frme As New EngineeringMaster
                ShowForm(frme)
            Case "Attendance Master"
                Dim frmAtt As New EngineerinAttendance
                ShowForm(frmAtt)
                'Case "E&C Master"
                '    Dim frmEM As New ErectionMaster
                '    ShowForm(frmEM)
            Case "OD Site Allotment"
                Dim frmOD As New ODSiteAllotment1
                ShowForm(frmOD)

            Case "Party Detail"
                Dim frmServ As New ServicePartyMaster
                ShowForm(frmServ)

            Case "Service Report"
                Dim frmServEnggReport As New ServiceEngineerReport
                ShowForm(frmServEnggReport)
            Case "Service Allocation"
                Dim frmPartyAllo As New ServicePartyAllocation
                ShowForm(frmPartyAllo)
            Case "Service Followp"
                Dim frmRptFoll As New Report_Service_FollowUp
                ShowForm(frmRptFoll)
            Case "Followp Pending"
                Dim frmFpending As New FollowpPending
                ShowForm(frmFpending)
            Case "Request Followp"
                Dim frmSerForm As New ServiceComplainFollowp
                ShowForm(frmSerForm)
            Case "Request Followp Report"
                Dim frmSerCFoll As New ServiceComplainReport
                ShowForm(frmSerCFoll)
            Case "Marketing Report"
                Dim EnqMr As New EnqMarketingReport
                ShowForm(EnqMr)
            Case "EC Report"
                Dim SerEC As New ServiceECStatusReport
                ShowForm(SerEC)
            Case "Order Agreement New"
                Dim SerEC As New OrderAgreementNew
                ShowForm(SerEC)
            Case "Order Category"
                Dim SerEC As New OrderCategory_Master
                ShowForm(SerEC)
            Case "Deshboard"
                Dim MainDes As New IICL_Extention
                ShowForm(MainDes)
            Case "Add Expense"
                Dim frmExp As New Expense_Master
                ShowForm(frmExp)
            Case "Print Voucher"
                Dim frmExpv As New Expense_Voucher
                ShowForm(frmExpv)
            Case "Spare Allotment"
                Dim frmSpare As New Spare_Allotment
                ShowForm(frmSpare)
            Case "Spare Party"
                Dim frmSpareP As New Spare_PartyDetail
                ShowForm(frmSpareP)
            Case "Spare Quotation"
                Dim frmSpareP As New Spare_Quotation
                ShowForm(frmSpareP)
            Case "Spare Invoice"
                Dim frmSparePI As New Spare_InvoiceRequest
                ShowForm(frmSparePI)
            Case "Spare Report"
                Dim frmSpareReport As New Spare_DetailReport
                ShowForm(frmSpareReport)
            Case "Spare Followp"
                Dim frmSpareFReport As New Spare_FollowpReport
                ShowForm(frmSpareFReport)
            Case "PC Log"
                Dim frmPclog As New Extension_Log
                ShowForm(frmPclog)
            Case "Import Data"
                Dim frmenqImprt As New InquiryImportData
                ShowForm(frmenqImprt)
            Case "Attendance Import"
                Dim frmAtt As New UserAttendanceData
                ShowForm(frmAtt)
            Case Else



                ' MessageBox.Show("No Data Found")
        End Select
    End Sub
    Private Sub MDIMainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'generate a dynamic menu 


        Try
            Dim dv As New DataTable
            Dim l_ds_AllMenu As New DataSet

            Dim Class1 As New Class1
            mnuMainMenu = New MenuStrip()
            Dim mainMenu1 As ToolStripMenuItem = New ToolStripMenuItem("Logout", Nothing, AddressOf clickHandler, "Logout")
            mnuMainMenu.Items.Add(mainMenu1)
            dv = Class1.global.UserPermissionDataset.Tables(0).DefaultView.ToTable(True, "Name", "FK_SoftWareID", "Type")
            If dv.Rows.Count > 0 Then
                If dv.Rows(0)("Type") = 1 Then
                    Dim dataSoftwares = linq_obj.SP_GetUserSoftwareName(Class1.global.UserID).ToList()
                    If dataSoftwares.Count > 0 Then
                        For Each itemMain As SP_GetUserSoftwareNameResult In dataSoftwares
                            'add a main menu
                            Dim mainMenu As ToolStripMenuItem = New ToolStripMenuItem(itemMain.Name, Nothing, AddressOf clickHandler, itemMain.Name)
                            Dim dataSoftwareDetail = linq_obj.SP_GetUserSoftwareDetailName(Class1.global.UserID, itemMain.Name, "admin").ToList()
                            'create a sub menu
                            If (itemMain.Name = "Inquiry Manager" Or itemMain.Name = "Service Manager" Or itemMain.Name = "Order Manager" Or itemMain.Name = "OrderAccount") Then
                            Else
                                For Each item As SP_GetUserSoftwareDetailNameResult In dataSoftwareDetail
                                    If (item.Type = 0) Then
                                        Dim subMenu As ToolStripMenuItem = New ToolStripMenuItem(item.DetailName, Nothing, AddressOf clickHandler, item.DetailName)
                                        mainMenu.DropDownItems.Add(subMenu)
                                    Else
                                        Dim detailSoftname = linq_obj.SP_GetSoftwareDetailBySoftwareName(item.Name).ToList()
                                        For Each itemDetailSoft As SP_GetSoftwareDetailBySoftwareNameResult In detailSoftname
                                            Dim subMenu As ToolStripMenuItem = New ToolStripMenuItem(itemDetailSoft.DetailName, Nothing, AddressOf clickHandler, itemDetailSoft.DetailName)
                                            mainMenu.DropDownItems.Add(subMenu)
                                        Next
                                    End If
                                Next
                            End If

                            mnuMainMenu.Items.Add(mainMenu)
                        Next

                        Me.Controls.Add(mnuMainMenu)
                    End If

                Else
                    Dim dataSoftwares = linq_obj.SP_GetUserSoftwareName(Class1.global.UserID).ToList()
                    If dataSoftwares.Count > 0 Then
                        For Each itemMain As SP_GetUserSoftwareNameResult In dataSoftwares
                            'add a main menu

                            Dim mainMenu As ToolStripMenuItem = New ToolStripMenuItem(itemMain.Name, Nothing, AddressOf clickHandler, itemMain.Name)
                            Dim dataSoftwareDetail = linq_obj.SP_GetUserSoftwareDetailName(Class1.global.UserID, itemMain.Name, "other").ToList()
                            'create a sub menu
                            If (itemMain.Name = "Inquiry Manager" Or itemMain.Name = "Service Manager" Or itemMain.Name = "Order Manager") Then

                            Else
                                For Each item As SP_GetUserSoftwareDetailNameResult In dataSoftwareDetail
                                    If (item.Type = 0) Then
                                        Dim subMenu As ToolStripMenuItem = New ToolStripMenuItem(item.DetailName, Nothing, AddressOf clickHandler, item.DetailName)
                                        mainMenu.DropDownItems.Add(subMenu)
                                    Else
                                        Dim detailSoftname = linq_obj.SP_GetSoftwareDetailBySoftwareName(item.Name).ToList()
                                        For Each itemDetailSoft As SP_GetSoftwareDetailBySoftwareNameResult In detailSoftname
                                            Dim subMenu As ToolStripMenuItem = New ToolStripMenuItem(itemDetailSoft.DetailName, Nothing, AddressOf clickHandler, itemDetailSoft.DetailName)
                                            mainMenu.DropDownItems.Add(subMenu)
                                        Next
                                    End If
                                Next
                            End If
                            mnuMainMenu.Items.Add(mainMenu)
                        Next

                        Me.Controls.Add(mnuMainMenu)

                    End If
                End If
            End If
            Me.Text = "INDIAN ION EXCHANGE & CHEMICALS LTD. " + Class1.global.UserName
            'Me.Text = "WATER INDIA ENGINEERS"
            Class1.g_sCompanyName = "INDIAN ION EXCHANGE & CHEMICALS LTD."

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub InquiryDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InquiryDetailToolStripMenuItem.Click
        Dim frmREP As New InquiryReport
        ShowForm(frmREP)
    End Sub

    Private Sub MainInquiryDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainInquiryDetailToolStripMenuItem.Click
        Dim frmREP As New InqiurySearchByCriteria
        ShowForm(frmREP)
    End Sub

    'Private Sub LogOutToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
    '    Me.Close()
    '    Dim frmLogin As New Login
    '    frmLogin.Show()
    'End Sub

    Private Sub InquiryReferenceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InquiryReferenceToolStripMenuItem.Click
        Dim frmREP As New InquiryByReference
        ShowForm(frmREP)
    End Sub

    Private Sub FollowUpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FollowUpToolStripMenuItem.Click
        Dim frmREP As New Report_FollowUp
        ShowForm(frmREP)
    End Sub

    Private Sub MDIMainForm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub LogoutToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem1.Click
        Me.Close()
        Dim frmLogin As New Login
        Class1.global.UserPermissionDataset.Tables(0).Rows.Clear()
        Class1.global.UserPermissionDataset.Dispose()
        frmLogin.Show()
    End Sub

    Private Sub AccountMasterToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountMasterToolStripMenuItem1.Click
        Dim frmAccount As New AccountMaster
        ShowForm(frmAccount)
    End Sub

    Private Sub OrderMaster1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrderMaster1ToolStripMenuItem.Click
        Dim frmOrder As New OrderMaster_1_
        ShowForm(frmOrder)
    End Sub

    Private Sub OrderMaster2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim frmOrder2 As New OrderMaster_2_
        'ShowForm(frmOrder2)
    End Sub

    Private Sub ProductMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductMasterToolStripMenuItem.Click
        Dim frmProduct As New ProductRegisterMaster
        ShowForm(frmProduct)
    End Sub

    Private Sub RowMaterialMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RowMaterialMasterToolStripMenuItem.Click
        Dim frmMaterial As New RowMaterialMaster
        ShowForm(frmMaterial)
    End Sub

    Private Sub ViewStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewStockToolStripMenuItem.Click
        Dim viewStock As New ViewStockDetail
        ShowForm(viewStock)
    End Sub
    Private Sub ServiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServiceToolStripMenuItem.Click
        Dim srvcMnger As New ServiceManager
        ShowForm(srvcMnger)
    End Sub
    Private Sub StockRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockRegisterToolStripMenuItem.Click
        Dim stckReg As New StockRegister
        ShowForm(stckReg)
    End Sub
    Private Sub MDIMainForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ' Application.Exit()
    End Sub

    Private Sub VisitorDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VisitorDetailToolStripMenuItem.Click
        Dim rptVisitor As New RptVisitorDetail
        ShowForm(rptVisitor)
    End Sub

    Private Sub PermissionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PermissionsToolStripMenuItem.Click
        Dim userPermission As New AddUserPermissionMaster
        ShowForm(userPermission)
    End Sub

    Private Sub DesignationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DesignationToolStripMenuItem.Click
        Dim dsgMstr As New DesignationMaster
        ShowForm(dsgMstr)
    End Sub

    Private Sub UserAllotmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserAllotmentToolStripMenuItem.Click
        Dim userAllotment As New UserAllotment
        ShowForm(userAllotment)
    End Sub




    Private Sub MenuStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip.ItemClicked

    End Sub
End Class
