Imports System.Data.SqlClient

Public Class SalesExecutiveByCriteria
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim count As Integer

    Dim ds As New DataSet
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        'AutoCompated_Text()
        ddlEnqType_Bind()
        bindUser()
        If (Class1.global.RptUserType = True) Then
            bindUser()
            grpUser.Enabled = True
        Else
            txtAllUser.Text = Class1.global.User + ","
            txtAllToUser.Text = Convert.ToString(Class1.global.UserID) + ","

            grpUser.Enabled = False
        End If
    End Sub
    Public Sub ddlEnqType_Bind()

        'EnqType 

        ddlEnqType.Items.Clear()
        Dim datatypeEnq = linq_obj.SP_Get_EnqTypeAllByType().ToList()
        ddlEnqType.DataSource = datatypeEnq
        ddlEnqType.DisplayMember = "EnqType"
        ddlEnqType.ValueMember = "EnqType"
        ddlEnqType.AutoCompleteMode = AutoCompleteMode.Append
        ddlEnqType.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEnqType.AutoCompleteSource = AutoCompleteSource.ListItems


        'Quotation Type 

        ddlQuotationType.Items.Clear()
        Dim dt As New DataTable
        dt.Columns.Add("Item")
        dt.Rows.Add("ISI")
        dt.Rows.Add("NON ISI")
        dt.Rows.Add("RO")
        dt.Rows.Add("PACKING")
        dt.Rows.Add("SPARE")
        dt.Rows.Add("SODA SOFT DRINK")
        ddlQuotationType.DataSource = dt
        ddlQuotationType.DisplayMember = "Item"
        ddlQuotationType.ValueMember = "Item"




    End Sub
    Public Sub bindUser()

        'User 
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

    Private Sub btnAddEnqType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEnqType.Click
        txtALLEnqType.Text += ddlEnqType.Text.Trim() + ",".Trim()
        ddlEnqType.Focus()
    End Sub

    Private Sub BtnAddQuotationtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddQuotationtype.Click
        txtAllQuotationType.Text += ddlQuotationType.Text.Trim() + ",".Trim()
        ddlQuotationType.Focus()
    End Sub

    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click

        txtAllUser.Text += cmbUser.Text.Trim() + ",".Trim() 'Username

        txtAllToUser.Text += cmbUser.SelectedValue + ",".Trim() 'User ID
        cmbUser.Focus()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criteria As String
        criteria = " and "

        If txtALLEnqType.Text.Trim() <> "" Then
            criteria = criteria + " ET.EnqType in (SELECT value FROM dbo.fn_Split('" + SplitString(txtALLEnqType.Text.Trim()) + "',',')) and"
        End If
        'From User 
        If rblFrom.Checked = True Then
            If txtAllUser.Text.Trim <> "" Then
                criteria = criteria + " UM.UserName in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllUser.Text.Trim()) + "',','))  and"
            End If
        End If
        'Sales Executive 

        If rblSalesExecutive.Checked = True Then
            If txtAllQuotationType.Text.Trim() <> "" Then
                criteria = criteria + " SQ.QuotationType in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllQuotationType.Text.Trim()) + "',',')) and"
            End If
            If txtQtnType.Text.Trim() <> "" Then
                criteria = criteria + " SQ.QtnType in (SELECT value FROM dbo.fn_Split('" + SplitString(txtQtnType.Text.Trim()) + "',','))  and"
            End If
            If txtStatus.Text.Trim() <> "" Then
                criteria = criteria + " SQ.Status in (SELECT value FROM dbo.fn_Split('" + SplitString(txtStatus.Text.Trim()) + "',',')) and"
            End If
            If txtPriority.Text.Trim() <> "" Then
                criteria = criteria + " SQ.Priority in (SELECT value FROM dbo.fn_Split('" + SplitString(txtPriority.Text.Trim()) + "',',')) and"
            End If
            If txtThrough.Text.Trim() <> "" Then
                criteria = criteria + " SQ.Through in (SELECT value FROM dbo.fn_Split('" + SplitString(txtThrough.Text.Trim()) + "',',')) and"
            End If
            'To User
            If rblTo.Checked = True Then
                If txtAllUser.Text.Trim <> "" Then
                    criteria = criteria + " q1.Fk_UserID in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllToUser.Text.Trim()) + "',',')) and"
                End If
            End If
        End If

        'Visit

        If rblVisit.Checked = True Then
            If txtQtnType.Text.Trim() <> "" Then
                criteria = criteria + " SV.QtnType in (SELECT value FROM dbo.fn_Split('" + SplitString(txtQtnType.Text.Trim()) + "',',')) and"
            End If
            If txtStatus.Text.Trim() <> "" Then
                criteria = criteria + " SV.Status in (SELECT value FROM dbo.fn_Split('" + SplitString(txtStatus.Text.Trim()) + "',',')) and"
            End If
            If txtPriority.Text.Trim() <> "" Then
                criteria = criteria + " SV.Priority in (SELECT value FROM dbo.fn_Split('" + SplitString(txtPriority.Text.Trim()) + "',',')) and"
            End If
            If txtThrough.Text.Trim() <> "" Then
                criteria = criteria + " SV.Through in (SELECT value FROM dbo.fn_Split('" + SplitString(txtThrough.Text.Trim()) + "',',')) and"
            End If
            If txtVisitType.Text.Trim() <> "" Then
                criteria = criteria + " SV.Vtype in (SELECT value FROM dbo.fn_Split('" + SplitString(txtVisitType.Text.Trim()) + "',',')) and"
            End If
            'To User ID
            If rblTo.Checked = True Then
                If txtAllUser.Text.Trim <> "" Then
                    criteria = criteria + " SV.Fk_To_UserID in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllToUser.Text.Trim()) + "',',')) and"
                End If
            End If

        End If

        'Other 

        If rblOther.Checked = True Then

            If txtQtnType.Text.Trim() <> "" Then
                criteria = criteria + " SO.QtnType in (SELECT value FROM dbo.fn_Split('" + SplitString(txtQtnType.Text.Trim()) + "',',')) and"
            End If
            If txtStatus.Text.Trim() <> "" Then
                criteria = criteria + " SO.Status in (SELECT value FROM dbo.fn_Split('" + SplitString(txtStatus.Text.Trim()) + "',',')) and"
            End If
            If txtPriority.Text.Trim() <> "" Then
                criteria = criteria + " SO.OtherPriority in (SELECT value FROM dbo.fn_Split('" + SplitString(txtPriority.Text.Trim()) + "',',')) and"
            End If
            If txtThrough.Text.Trim() <> "" Then
                criteria = criteria + " SO.OtherThrough in (SELECT value FROM dbo.fn_Split('" + SplitString(txtThrough.Text.Trim()) + "',',')) and"
            End If
            'To User ID
            If rblTo.Checked = True Then
                If txtAllUser.Text.Trim <> "" Then
                    criteria = criteria + " SO.Fk_To_UserID in (SELECT value FROM dbo.fn_Split('" + SplitString(txtAllToUser.Text.Trim()) + "',',')) and"
                End If
            End If

        End If

        If criteria = " and " Then
            criteria = ""
        End If
        If (criteria.Length > 0) Then
            criteria = criteria.ToString().Substring(0, criteria.Length - 3)
        End If
        Dim cmd As New SqlCommand
        cmd.CommandTimeout = 3000
        If rblSalesExecutive.Checked = True Then
            cmd.CommandText = "SP_SalesExecutive_Criteria_Report"
        ElseIf rblVisit.Checked = True Then
            cmd.CommandText = "SP_SalesExecutive_Criteria_Report_Visit"
        Else
            cmd.CommandText = "SP_SalesExecutive_Criteria_Report_Other"
        End If

        Dim FlagDate As Integer
        FlagDate = 0

        If rblToDate.Checked = True Then
            FlagDate = 1

        End If

        cmd.CommandTimeout = 3000
        cmd.Parameters.Add("@Flag", SqlDbType.Int).Value = FlagDate
        cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = dtEndDate.Value
        cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = dtStartDate.Value
        cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
        cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User)
        Dim objclass As New Class1
        ds = objclass.GetEnqReportData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DataGridView1.DataSource = Nothing
        Else
            DataGridView1.DataSource = ds.Tables(1)
            DataGridView1.Columns(0).Width = 30
            If Class1.global.RptUserType = False Then
                ''use can not display email,mobileno
                DataGridView1.Columns(4).Visible = False
                DataGridView1.Columns(5).Visible = False


            End If
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

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtALLEnqType.Text = ""
        txtAllQuotationType.Text = ""
        If Class1.global.RptUserType = True Then
            txtAllUser.Text = ""
        End If
        txtPriority.Text = ""
        txtQtnType.Text = ""
        txtStatus.Text = ""
        txtThrough.Text = ""
        txtVisitType.Text = ""


    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
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

            MessageBox.Show("Export to Excel Sucessfully...")


        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub
End Class

