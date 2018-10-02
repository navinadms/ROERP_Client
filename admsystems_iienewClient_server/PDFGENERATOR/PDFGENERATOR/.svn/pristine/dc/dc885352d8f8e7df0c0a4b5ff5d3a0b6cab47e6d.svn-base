Imports System.Data.SqlClient
Public Class TestInquiryReport

    Dim strCriteria As String

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        ddlEnqType_Bind()



    End Sub
    Private Sub InquiryReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ddlEnqType.Items.Add("Select")

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criteria As String
        criteria = " and "

        If txtCity.Text.Trim() <> "" Then
            criteria = criteria + " City like '" + txtCity.Text + "%' and"


            If ddlEnqType.SelectedValue <> 0 Then
                criteria = criteria + " ty.EnqType like '" + ddlEnqType.Text + "' and"


            End If
        Else

            If ddlEnqType.SelectedValue <> 0 Then
                criteria = criteria + " ty.EnqType like '" + ddlEnqType.Text + "' and"


            End If
        End If



        If criteria = " and " Then
            criteria = ""
        End If

        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If
        'MessageBox.Show(criteria)

        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Search_InquiryReport"
        cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = dtEndDate.Value


        cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = dtStartDate.Value
        Dim str As String

        str = dtStartDate.Value.ToShortDateString
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
        Dim objclass As New Class1

        Dim ds As New DataSet
        ds = objclass.GetEnqReportData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        DataGridView1.DataSource = ds.Tables(1)

        'Dim rpt As New rpt_inquiryReport

        '        rpt.SetDataSource(ds.Tables(1))

        ' CrystalReportViewer1.ReportSource = rpt
        ' rpt.Refresh()

    End Sub

    Public Sub AutoCompated_Text()
        Dim data = linq_obj.SP_Get_AddressListAutoComplete("City").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In data
            txtCity.AutoCompleteCustomSource.Add(iteam.Result)
        Next

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

End Class
