
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

Public Class ServiceEngineerReport

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public Sub New()
        InitializeComponent()
        ddlEngineerList_Bind()
        ddlMachineItem_Bind()

    End Sub

    Public Sub ddlEngineerList_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")
        Dim data = linq_obj.SP_Get_Service_Engineer_Master_List().ToList()
        For Each item As SP_Get_Service_Engineer_Master_ListResult In data
            dt.Rows.Add(item.Pk_Engineer_ID, item.Name)
        Next
        ddlEngineer.DataSource = dt
        ddlEngineer.DisplayMember = "Name"
        ddlEngineer.ValueMember = "ID"

        ddlEngineer.AutoCompleteMode = AutoCompleteMode.Append
        ddlEngineer.DropDownStyle = ComboBoxStyle.DropDownList
        ddlEngineer.AutoCompleteSource = AutoCompleteSource.ListItems

        txtAtta_Status.AutoCompleteMode = AutoCompleteMode.Suggest
        txtAtta_Status.AutoCompleteSource = AutoCompleteSource.CustomSource
        Dim DataCollection As New AutoCompleteStringCollection()
        addItems(DataCollection)
        txtAtta_Status.AutoCompleteCustomSource = DataCollection
    End Sub
    Public Sub ddlMachineItem_Bind()
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")
        Dim data = linq_obj.SP_Get_Packing_Item_Master_List().ToList()
        For Each item As SP_Get_Packing_Item_Master_ListResult In data
            dt.Rows.Add(item.Pk_Packing_Item_Master_ID, item.Packing_Item)
        Next
        ddlMachine.DataSource = dt
        ddlMachine.DisplayMember = "Name"
        ddlMachine.ValueMember = "ID"
        ddlMachine.AutoCompleteMode = AutoCompleteMode.Append
        ddlMachine.DropDownStyle = ComboBoxStyle.DropDownList
        ddlMachine.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim criteria As String
            criteria = " and "
            'For Engineer
            If txtEngineer_All.Text.Trim() <> "" Then
                criteria = criteria + " SEM.Name in (SELECT value FROM dbo.fn_Split('" + SplitString(txtEngineer_All.Text.Trim()) + "',',')) and"
            End If
            'From Machine
            If txtMachineAll.Text.Trim <> "" Then
                criteria = criteria + " SAM.Machine in (SELECT value FROM dbo.fn_Split('" + SplitString(txtMachineAll.Text.Trim()) + "',','))  and"
            End If
            'For PartyName
            If txtPartyName.Text.Trim <> "" Then
                criteria = criteria + " SAM.PartyName like '%" + txtPartyName.Text.Trim() + "%'   and"
            End If
            'OD Type 
            Dim ODType As String
            Dim OD
            If rblODTypeEC.Checked = True Then
                ODType = "EC"
            ElseIf rblODType_Service.Checked = True Then
                ODType = "Service"
            ElseIf rblODType_Visit.Checked = True Then

                ODType = "Visit"
            Else
                ODType = ""
            End If
            If ODType.Trim() <> "" Then
                criteria = criteria + " SAM.EnggType='" + ODType + "'  and"
            End If

            'OD Site Status

            Dim ODSiteStatus As String
            If rblODStatus_Done.Checked = True Then
                ODSiteStatus = "Done"
            ElseIf rblODStatus_Running.Checked = True Then
                ODSiteStatus = "Running"
            ElseIf rblODStatus_Cancel.Checked = True Then
                ODSiteStatus = "Cancel"
            Else
                ODSiteStatus = ""

            End If

            If ODSiteStatus.Trim() <> "" Then
                criteria = criteria + " SAM.ODSiteStatus='" + ODSiteStatus + "'  and"
            End If

            'For Attandance        
 

            If txtAtta_Status.Text.Trim() <> "" Then
                criteria = criteria + " SAM.Status='" + txtAtta_Status.Text.Trim() + "'  and"
            End If

            If criteria = " and " Then
                criteria = ""
            End If
            If (criteria.Length > 0) Then
                criteria = criteria.ToString().Substring(0, criteria.Length - 3)
            End If

            Dim cmd As New SqlCommand
            cmd.CommandTimeout = 3000
            cmd.CommandText = "SP_Get_Service_Engineer_Report_By_Criteria"
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dtStartDate.Value
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dtEndDate.Value
            cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria
            Dim ds As New DataSet

            Dim objclass As New Class1
            ds = objclass.GetEnqReportData(cmd)
            If ds.Tables(1).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GvEngineerReport.DataSource = Nothing
            Else
                GvEngineerReport.DataSource = ds.Tables(1)
                GvEngineerReport.Columns(0).Width = 30

            End If

        Catch ex As Exception

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
                    strfinal += tmpstr(index) + ","
                End If
            Next
            strfinal = strfinal.ToString().Substring(0, strfinal.Length - 1)
            ' strfinal += ""
        End If

        Return strfinal
    End Function

    Private Sub btnEnggAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnggAdd.Click
        txtEngineer_All.Text += ddlEngineer.Text.Trim() + ",".Trim()
        ddlEngineer.Focus()
    End Sub

    Private Sub btnAddMachine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMachine.Click
        txtMachineAll.Text += ddlMachine.Text.Trim() + ",".Trim()
        ddlMachine.Focus()
    End Sub

    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        Try
            Dim count As Integer
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
            For i As Integer = 1 To GvEngineerReport.Columns.Count
                worksheet.Cells(1, i) = GvEngineerReport.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvEngineerReport.Rows.Count - 1
                For j As Integer = 0 To GvEngineerReport.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvEngineerReport.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next


            MessageBox.Show("Export to Excel Sucessfully...")


        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub

    Public Sub addItems(ByVal col As AutoCompleteStringCollection)
        col.Add("ABSENT")
        col.Add("OFFICE")
        col.Add("OD")
        col.Add("OFFICE OD")
        col.Add("LEAVE")
        col.Add("SUNDAY HOLIDAY")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtEngineer_All.Text = ""
        txtMachineAll.Text = ""
        rblODStatus_None.Checked = True
        rblODType_None.Checked = True
    End Sub
End Class