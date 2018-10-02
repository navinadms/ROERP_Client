Imports System.Data.SqlClient

Public Class ODEngineeringReport
    '

    Dim strCriteria As String

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ddlEngineerList_Bind()



    End Sub
    Public Sub ddlEngineerList_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")
        Dim data = linq_obj.SP_Get_Service_Engineer_Master_List().ToList()
        For Each item As SP_Get_Service_Engineer_Master_ListResult In data
            dt.Rows.Add(item.Pk_Engineer_ID, item.Name)
        Next
        ddlEngineerList.DataSource = dt
        ddlEngineerList.DisplayMember = "Name"
        ddlEngineerList.ValueMember = "ID"
        ddlEngineerList.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ddlEngineerList.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEngineerList.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub InquiryReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ddlEnqType.Items.Add("Select")

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If ddlEngineerList.SelectedValue > 0 Then

            Dim ODEngineerDS As New DataSet

            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_Get_ODEngineering_Report_By_EngineerID"
            cmd.Connection = linq_obj.Connection
            cmd.Parameters.Add("@Pk_Engineer_ID", SqlDbType.Int).Value = ddlEngineerList.SelectedValue
            Dim rpt As New rpt_ODEngineerReport
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ODEngineerDS, "ODEngineerDS")
            ODEngineerDS.Tables(0).Rows.Add()
            ODEngineerDS.Tables(0).Rows.Add()
            ODEngineerDS.Tables(0).Rows.Add()
            ODEngineerDS.Tables(0).Rows.Add()

            If ODEngineerDS.Tables(0).Rows.Count > 0 Then
                Class1.WriteXMlFile(ODEngineerDS, "SP_Get_ODEngineering_Report_By_EngineerID", "ODEngineerDS")
                rpt.Database.Tables(0).SetDataSource(ODEngineerDS.Tables("ODEngineerDS"))
                CrystalReportViewer1.ReportSource = rpt
                rpt.Refresh()
                'Dim frmRpt As New FrmCommanReportView(rpt)
                'frmRpt.Show()
            Else
                CrystalReportViewer1.ReportSource = rpt
                rpt.Refresh()
                MessageBox.Show("Data Not Found...")

            End If
        Else
            MessageBox.Show("Data Not Found...")
        End If
    End Sub


    'MessageBox.Show(criteria)

    '    Dim cmd As New SqlCommand
    '    cmd.CommandText = "SP_Get_ODEngineering_Report_By_EngineerID"
    '    cmd.Parameters.Add("@Pk_Engineer_ID ", SqlDbType.BigInt).Value = ddlEngineerList.SelectedValue
    '    Dim objclass As New Class1
    '    Dim ds As New DataSet
    '    ds = objclass.GetEnqReportData(cmd)
    '    If ds.Tables(1).Rows.Count < 1 Then
    '        MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End If
    '    Dim rpt As New rpt_ODEngineerReport

    '    rpt.SetDataSource(ds.Tables(1))

    '    CrystalReportViewer1.ReportSource = rpt
    '    rpt.Refresh()

     
End Class