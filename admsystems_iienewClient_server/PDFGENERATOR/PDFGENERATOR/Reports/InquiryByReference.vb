Imports System.Data.SqlClient

Public Class InquiryByReference


    Dim strCriteria As String
    Dim dataTable As DataTable
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        ddlEnqType_Bind()

        If (Class1.global.RptUserType = True) Then
            bindUser()
            lblUser.Visible = True
            cmbUser.Visible = True
        Else
            lblUser.Visible = False
            cmbUser.Visible = False
        End If

    End Sub
    Public Sub bindUser()
        cmbUser.DataSource = Nothing
        Dim datatable As New DataTable
        datatable.Columns.Add("Pk_UserId")
        datatable.Columns.Add("UserName")
        Dim datauser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In datauser
            datatable.Rows.Add(item.Pk_UserId, item.UserName)
        Next
        Dim newRow As DataRow = datatable.NewRow()
        newRow(0) = "0"
        newRow(1) = "Select"
        datatable.Rows.InsertAt(newRow, 0)
        cmbUser.DataSource = datatable
        cmbUser.DisplayMember = "UserName"
        cmbUser.ValueMember = "Pk_UserId"
        cmbUser.AutoCompleteMode = AutoCompleteMode.Append
        cmbUser.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub
    Private Sub InquiryReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ddlEnqType.Items.Add("Select")

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As Date?
        dt = dtStartDate.Value
        dataTable = New DataTable()
        dataTable.Columns.Add("Name")
        dataTable.Columns.Add("Address")
        dataTable.Columns.Add("MobileNo")
        dataTable.Columns.Add("District")
        dataTable.Columns.Add("State")
        dataTable.Columns.Add("OfferNo")
        dataTable.Columns.Add("CustType")
        dataTable.Columns.Add("EnqType")
        dataTable.Columns.Add("EnqFor")
        dataTable.Columns.Add("EnqDate")

        Dim data = linq_obj.SP_Search_InquiryReportByReferenceByUser(ddlReference.Text, dtStartDate.Value, dtEndDate.Value, If(Class1.global.RptUserType = True, Convert.ToInt32(cmbUser.SelectedValue), Convert.ToInt32(Class1.global.UserID))).ToList()
        If (data.Count > 0) Then
            dataTable.Rows.Clear()
            For Each item As SP_Search_InquiryReportByReferenceByUserResult In data
                dataTable.Rows.Add(Convert.ToString(item.Name), Convert.ToString(item.Address), Convert.ToString(item.MobileNo), Convert.ToString(item.District), Convert.ToString(item.State),
                                   Convert.ToString(item.OfferNo), Convert.ToString(item.CustType), Convert.ToString(item.EnqType), Convert.ToString(item.EnqFor), Convert.ToDateTime(item.EnqDate).ToShortDateString())
            Next

        Else
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Dim rpt As New rpt_inquiryReport
        'dt = data
        rpt.SetDataSource(dataTable)
        Dim ds As New EnqReport
        CrystalReportViewer1.ReportSource = rpt
        rpt.Refresh()
    End Sub

    Public Sub AutoCompated_Text()

    End Sub
    Public Sub ddlEnqType_Bind()
        ddlReference.Items.Clear()
        Dim getPlant = linq_obj.SP_Get_Enq_References().ToList()
        '   Dim Enq = linq_obj.SP_Get_EnqTypeList().ToList()
        ddlReference.DataSource = getPlant
        ddlReference.DisplayMember = "Reference"
        ddlReference.ValueMember = "Reference"



        ddlReference.AutoCompleteMode = AutoCompleteMode.Append
        ddlReference.DropDownStyle = ComboBoxStyle.DropDownList

        ddlReference.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

End Class