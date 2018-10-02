Imports System.Data.SqlClient

Public Class InqiurySearchByCriteria
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim count As Integer

    Dim ds As New DataSet
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        ddlEnqType_Bind()
        If (Class1.global.RptUserType = True) Then
            bindUser()
            GrpUser.Visible = True
        Else
            GrpUser.Visible = False
        End If
    End Sub
  
    Private Sub btnAddRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRef.Click
        If txtReference1.Text <> "" Then
            txtAllReference.Text += txtReference1.Text.Trim() + ",".Trim()
            txtReference1.Text = ""
        End If
    End Sub

    Private Sub btnAddEnqType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEnqType.Click
        txtAllEnq.Text += ddlEnqType.Text.Trim() + ",".Trim()
        ddlEnqType.Focus()
    End Sub

    Private Sub btnAddState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddState.Click
        If txtState.Text <> "" Then
            txtAllState.Text += txtState.Text.Trim() + ",".Trim()
            txtState.Text = ""
        End If
    End Sub

    Private Sub btnAddCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCity.Click
        If txtCity.Text <> "" Then
            txtAllCity.Text += txtCity.Text.Trim() + ",".Trim()
            txtCity.Text = ""
        End If
    End Sub

    Private Sub btnAddPlant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPlant.Click
        If txtPlant.Text <> "" Then
            txtAllPlant.Text += txtPlant.Text.Trim() + ",".Trim()
            txtPlant.Text = ""

        End If

    End Sub

    Public Sub AutoCompated_Text()
        Dim data = linq_obj.SP_Get_AddressListAutoComplete("City").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In data
            txtCity.AutoCompleteCustomSource.Add(iteam.Result)
        Next
        'Search State Auto complated 
        txtState.AutoCompleteCustomSource.Clear()
        Dim S_data = linq_obj.SP_Get_AddressListAutoComplete("State").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In S_data
            txtState.AutoCompleteCustomSource.Add(iteam.Result)

        Next

        Dim getPlant = linq_obj.SP_Get_Enq_EnqMasterList().ToList()

        For Each item As SP_Get_Enq_EnqMasterListResult In getPlant
            txtPlant.AutoCompleteCustomSource.Add(item.EnqFor)
            ''txtRefrence.AutoCompleteCustomSource.Add(item.Reference)
        Next


    End Sub
    Public Sub ddlEnqType_Bind()
        ddlEnqType.Items.Clear()
        Dim datatypeEnq = linq_obj.SP_Get_EnqTypeAllByType().ToList()
        ddlEnqType.DataSource = datatypeEnq
        ddlEnqType.DisplayMember = "EnqType"
        ddlEnqType.ValueMember = "EnqType"
        ddlEnqType.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqType.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqType.AutoCompleteSource = AutoCompleteSource.ListItems


        cmbSubCategory.Items.Clear()

        Dim enqStatusData = linq_obj.SP_Get_EnqTypeAll().ToList()
        cmbSubCategory.DataSource = enqStatusData
        cmbSubCategory.DisplayMember = "SubCategory"
        cmbSubCategory.ValueMember = "SubCategory"
        cmbSubCategory.AutoCompleteMode = AutoCompleteMode.Append
        cmbSubCategory.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSubCategory.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Function getCity(ByVal str As String) As String
        If str.Equals("") Then
            Return ""
        End If
        Dim strfinal As String
        Dim cnt As Integer
        cnt = str.Split(",").Length
        Dim tmpstr As [String]() = New [String](cnt) {}
        tmpstr = str.Split(",")
        strfinal = "'"
        If cnt > 0 Then
            For index = 0 To tmpstr.Length - 1
                If tmpstr(index) <> "" Then
                    strfinal += tmpstr(index) + "','"
                End If
            Next
            strfinal += "'"
        End If

        Return strfinal
    End Function

    Private Sub btnSearchAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAll.Click
        GenerateReport()
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
        newRow(1) = "All"
        datatable.Rows.InsertAt(newRow, 0)
        cmbUser.DataSource = datatable
        cmbUser.DisplayMember = "UserName"
        cmbUser.ValueMember = "Pk_UserId"
        cmbUser.AutoCompleteMode = AutoCompleteMode.Append
        cmbUser.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Public Sub GenerateReport()
        '  and City in (SELECT value FROM dbo.fn_Split('''+@city+''','',''))'


        Dim criteria As String
        criteria = " and "

        If txtAllCity.Text.Trim() <> "" Then
            criteria = criteria + " City in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllCity.Text.Trim()) + "',',')) and"
        End If
        If txtAllState.Text.Trim() <> "" Then
            criteria = criteria + " BillState in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllState.Text.Trim()) + "',',')) and"
        End If
        If txtAllEnq.Text.Trim <> "" Then
            criteria = criteria + " ty.EnqType in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllEnq.Text.Trim()) + "',',')) and"
        End If

        If txtAllPlant.Text.Trim() <> "" Then
            criteria = criteria + " en.EnqFor in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllPlant.Text.Trim()) + "',',')) and"
        End If
        If txtAllReference.Text.Trim() <> "" Then
            criteria = criteria + " a.Reference1 in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllReference.Text.Trim()) + "',',')) and"
        End If
        If txtAllSubCategory.Text.Trim() <> "" Then
            criteria = criteria + " a.TypeOfEnq in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllSubCategory.Text.Trim()) + "',',')) and"
        End If

        If criteria = " and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If

        Dim cmd As New SqlCommand
        cmd.CommandTimeout = 3000
        If chkSubCategory.Checked = False Then
            'check address enq date
            cmd.CommandText = "SP_SearchAllInquiryReportByUserWithCriteria"
        End If
        'If chkSubCategory.Checked = True Then
        '    'check Enq Followp Details F_DATE
        '    cmd.CommandText = "SP_SearchAllInquiryReportByUserWithCriteria_Subcategory"
        'End If
        cmd.CommandTimeout = 3000
        cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = dtEndDate.Value.Date
        cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = dtStartDate.Value.Date
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
        cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User)
        Dim objclass As New Class1
        ds = objclass.GetEnqReportData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DataGridView1.DataSource = Nothing
        Else
            DataGridView1.DataSource = ds.Tables(1)
            DataGridView1.Columns(0).Width = 50
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim rpt As New rpt_inquiryReport
        rpt.SetDataSource(ds.Tables(1))
        rpt.PrintToPrinter(1, False, 0, 1)
        rpt.Refresh()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        Try
            Dim app As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()

            ' creating new WorkBook within Excel application
            Dim workbook As Microsoft.Office.Interop.Excel._Workbook = app.Workbooks.Add(Type.Missing) 
            ' creating new Excelsheet in workbook
            Dim worksheet As Microsoft.Office.Interop.Excel._Worksheet = Nothing 
            ' see the excel sheet behind the program
            app.Visible = True 
            ' get the reference of first sheet. By default its name is Sheet1.
            ' store its reference to worksheet
            worksheet = workbook.Sheets("Sheet1")
            worksheet = workbook.ActiveSheet
            ' changing the name of active sheet
            worksheet.Name = Me.Name
            ' storing header part in Excel
            For i As Integer = 1 To DataGridView1.Columns.Count
                worksheet.Cells(1, i) = DataGridView1.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                For j As Integer = 0 To DataGridView1.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = DataGridView1.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next
             
        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try

       
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPDF.Click
        Dim rpt As New rpt_inquiryReport
        rpt.SetDataSource(ds.Tables(1))
        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "DEF.pdf")
        MessageBox.Show("Successfully Exported")

        'FileOpen(1, "DEF.pdf", OpenMode.Append, OpenAccess.Write)

    End Sub

    Private Sub dtEndDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtEndDate.ValueChanged

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

    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        txtAllUser.Text += cmbUser.Text + ","
    End Sub

    Private Sub btnAddSubCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSubCategory.Click
        txtAllSubCategory.Text += cmbSubCategory.Text + ","
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        txtAllUser.Text = ""
        txtAllEnq.Text = ""
        txtAllPlant.Text = ""
        txtAllReference.Text = ""
        txtAllState.Text = ""
        txtAllSubCategory.Text = ""
        txtAllCity.Text = ""
        dtEndDate.Value = DateTime.Now
        dtStartDate.Value = DateTime.Now
        cmbUser.SelectedIndex = 0
        DataGridView1.DataSource = Nothing

    End Sub 
     
    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub
    Private Sub InqiurySearchByCriteria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class