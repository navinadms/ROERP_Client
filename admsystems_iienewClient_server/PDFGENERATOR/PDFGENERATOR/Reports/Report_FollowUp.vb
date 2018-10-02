Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames

Public Class Report_FollowUp
    Dim strCriteria As String
    Dim dataTable As DataTable
    Dim ds As New DataSet
    Dim count As Integer

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        If (Class1.global.RptUserType = True) Then
            grpUser.Visible = True
        Else
            GrpUser.Visible = False
        End If
        ' ddlEnqType_Bind()
    End Sub
    Public Sub AutoCompated_Text()
        Dim GetUser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In GetUser
            txtUser.AutoCompleteCustomSource.Add(item.UserName)
        Next
    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As Date?
        dt = dtStartDate.Value
        If (cmbFollow.Text = "Today Call") Then
            dgFollowDetail.DataSource = Nothing
            Dim FollowData = linq_obj.SP_Get_Enq_FollowDetailsByCriteriaByUserByUser(If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User), dtStartDate.Value, dtEndDate.Value, "Today Call").ToList()
            If (FollowData.Count > 0) Then
                dgFollowDetail.DataSource = FollowData
                dgFollowDetail.Columns(dgFollowDetail.Columns.Count - 1).Visible = False

                Dim countToday = linq_obj.GetFollowUpCountTodayByUser(If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User), dtStartDate.Value, dtEndDate.Value).ToList()

                If (countToday.Count > 0) Then
                    dgFollowCounter.DataSource = countToday
                Else
                    dgFollowCounter.DataSource = Nothing
                End If

                Dim TotalFollowp As Integer
                TotalFollowp = 0
                For Each item As GetFollowUpCountTodayByUserResult In countToday
                    TotalFollowp = TotalFollowp + item.FollowUp

                Next

                lblTotal.Text = TotalFollowp

            Else
                dgFollowDetail.DataSource = Nothing
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            dgFollowDetail.DataSource = Nothing
            Dim FollowData1 = linq_obj.SP_Get_Enq_FollowDetailsByCriteriaForDailyByUser(If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User), dtStartDate.Value, dtEndDate.Value, "Daily Call").ToList()
            If (FollowData1.Count > 0) Then
                dgFollowDetail.DataSource = FollowData1
                dgFollowDetail.Columns(dgFollowDetail.Columns.Count - 1).Visible = False
                Dim countDaily = linq_obj.GetFollowUpCountDailyByUser(If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User), dtStartDate.Value, dtEndDate.Value).ToList()
                If (countDaily.Count > 0) Then
                    dgFollowCounter.DataSource = countDaily
                Else
                    dgFollowCounter.DataSource = Nothing
                End If

                Dim TotalFollowp As Integer
                TotalFollowp = 0
                For Each item As GetFollowUpCountDailyByUserResult In countDaily
                    TotalFollowp = TotalFollowp + item.FollowUp

                Next

                lblTotal.Text = TotalFollowp
            Else
                dgFollowDetail.DataSource = Nothing
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If



        End If


        ''Work Duration
        Dim Datawork = linq_obj.SP_Get_Visit_CallDuration_Employeer(If(Class1.global.RptUserType = True, SplitString(txtAllUser.Text), Class1.global.User), dtStartDate.Value, dtEndDate.Value).ToList()
        If (Datawork.Count > 0) Then
            GvWorkDuration.DataSource = Datawork

        Else
            GvWorkDuration.DataSource = Nothing
        End If

        ''Pending Duration
        Dim cmd As New SqlCommand
        cmd.CommandText = "SP_Get_Followp_Pending_Report"
        If txtAllUser.Text.Trim() <> "" Then
            cmd.Parameters.AddWithValue("@byUser", SplitString(txtAllUser.Text))
        Else
            cmd.Parameters.AddWithValue("@byUser", Class1.global.User)
        End If
        cmd.Parameters.AddWithValue("@ByFollowDate", dtStartDate.Value)
        cmd.Parameters.AddWithValue("@NextFollowDate", dtEndDate.Value)

        Dim objclass As New Class1

        Dim ds As New DataSet
        ds = objclass.GetSearchData(cmd)
        If ds.Tables(1).Rows.Count < 1 Then

            GvPending.DataSource = Nothing
        Else


            GvPending.DataSource = ds.Tables(1)

            Dim TotalFollowp As Integer
            TotalFollowp = 0
            For index = 0 To ds.Tables(1).Rows.Count - 1
                TotalFollowp = TotalFollowp + Convert.ToInt32(ds.Tables(1).Rows(index)("PendingCall"))

            Next

            lblTotalPending.Text = TotalFollowp

            ds.Dispose()

        End If

        txtUser.Text = ""
        ''cmbFollow.SelectedIndex = 0


    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim rpt As New rptFollowUpByUser
            rpt.SetDataSource(dgFollowDetail.DataSource)
            rpt.PrintToPrinter(1, False, 0, 1)
            rpt.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click

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
        For i As Integer = 1 To dgFollowDetail.Columns.Count - 1
            worksheet.Cells(1, i) = dgFollowDetail.Columns(i - 1).HeaderText
        Next

        ' storing Each row and column value to excel sheet
        For i As Integer = 0 To dgFollowDetail.Rows.Count - 1
            For j As Integer = 0 To dgFollowDetail.Columns.Count - 2
                worksheet.Cells(i + 2, j + 1) = dgFollowDetail.Rows(i).Cells(j).Value.ToString()
                count += i + 2 + j + 1
            Next
        Next

        ' storing header part in Excel
        'For i As Integer = 1 To dgFollowCounter.Columns.Count
        '    worksheet.Cells(dgFollowDetail.Rows.Count + 3, i) = dgFollowCounter.Columns(i - 1).HeaderText
        'Next


        worksheet.Cells(dgFollowDetail.Rows.Count + 3, 1) = "Total Call"
        worksheet.Cells(dgFollowDetail.Rows.Count + 3, 2) = dgFollowDetail.Rows.Count.ToString()



        ' storing Each row and column value to excel sheet
        For i As Integer = 0 To dgFollowCounter.Rows.Count - 1
            For j As Integer = 0 To dgFollowCounter.Columns.Count - 1
                worksheet.Cells(dgFollowDetail.Rows.Count + i + 4, j + 1) = dgFollowCounter.Rows(i).Cells(j).Value.ToString()
                count += i + 2 + j + 1
            Next
        Next

        If GvPending.Rows.Count <> 0 Then

            ' storing header part in Excel
            For i As Integer = 1 To GvPending.Columns.Count
                worksheet.Cells(dgFollowDetail.Rows.Count + dgFollowCounter.Rows.Count + 5, i) = GvPending.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvPending.Rows.Count - 1
                For j As Integer = 0 To GvPending.Columns.Count - 1
                    worksheet.Cells(dgFollowDetail.Rows.Count + i + dgFollowCounter.Rows.Count + 6, j + 1) = GvPending.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next
        End If

        If GvWorkDuration.Rows.Count <> 0 Then
            For i As Integer = 1 To GvWorkDuration.Columns.Count
                worksheet.Cells(dgFollowDetail.Rows.Count + dgFollowCounter.Rows.Count + 5 + GvPending.Columns.Count + 7, i) = GvWorkDuration.Columns(i - 1).HeaderText
            Next
            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvWorkDuration.Rows.Count - 1
                For j As Integer = 0 To GvWorkDuration.Columns.Count - 1
                    worksheet.Cells(dgFollowDetail.Rows.Count + i + dgFollowCounter.Rows.Count + 6 + GvPending.Columns.Count + 8, j + 1) = GvWorkDuration.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next
        End If

    End Sub

    Private Sub btnExportPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportPDF.Click
        Try
            Dim rpt As New rpt_inquiryReport
            rpt.SetDataSource(dgFollowDetail.DataSource)

            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "DEF.xls")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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
                    '  Dim tmpstr1 As [String]() = New [String](2) {}
                    'tmpstr1 = tmpstr(index).Split("+")
                    strfinal += tmpstr(index) + ","
                End If
            Next
            strfinal = strfinal.ToString().Substring(0, strfinal.Length - 1)
            ' strfinal += ""
        End If

        Return strfinal
    End Function
  
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        txtAllUser.Text += txtUser.Text.Trim() + ","
    End Sub
End Class