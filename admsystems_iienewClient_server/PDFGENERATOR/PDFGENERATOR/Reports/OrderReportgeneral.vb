Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine.ReportDocument

Public Class OrderReportgeneral
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Address_ID As Integer
    Dim dataAddress As DataTable
    Dim count As Integer

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        bindUser()

    End Sub
    Public Sub AutoCompated_Text()

        '' State Auto Complated 
        txtState.AutoCompleteCustomSource.Clear()
        Dim data = linq_obj.SP_Get_AddressListAutoComplete("State").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In data
            txtState.AutoCompleteCustomSource.Add(iteam.Result)

        Next

        '' Station Auto Complated 
        txtStation.AutoCompleteCustomSource.Clear()
        Dim data1 = linq_obj.SP_Get_AddressListAutoComplete("City").ToList()
        For Each iteam As SP_Get_AddressListAutoCompleteResult In data1
            txtStation.AutoCompleteCustomSource.Add(iteam.Result)

        Next


        '' Dipatch Status Auto Complated as  where  Status=0
        txtDispStatus.AutoCompleteCustomSource.Clear()
        Dim DispStatus = linq_obj.SP_Get_Tbl_ProjectInformationMaster_DType_AutoComp(0).ToList()
        For Each iteam As SP_Get_Tbl_ProjectInformationMaster_DType_AutoCompResult In DispStatus
            txtDispStatus.AutoCompleteCustomSource.Add(iteam.F_Type)
        Next


        '' Plant Auto Complated as  where  Status=1
        txtPlant.AutoCompleteCustomSource.Clear()
        Dim PlantName = linq_obj.SP_Get_Tbl_ProjectInformationMaster_DType_AutoComp(1).ToList()
        For Each iteam As SP_Get_Tbl_ProjectInformationMaster_DType_AutoCompResult In PlantName
            txtPlant.AutoCompleteCustomSource.Add(iteam.F_Type)
        Next

        '' Project Type AutoComplated 

        Dim dtpro As New DataTable
        dtpro.Columns.Add("Status")
        dtpro.Rows.Add("ISI HOT")
        dtpro.Rows.Add("ISI CONFIRM")
        dtpro.Rows.Add("NON ISI HOT")
        dtpro.Rows.Add("NON ISI CONFIRM")
        txtProjectType1.AutoCompleteCustomSource.Clear()
        For index = 1 To dtpro.Rows.Count - 1
            txtProjectType1.AutoCompleteCustomSource.Add(dtpro.Rows(index)("Status"))
        Next

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
    Private Sub btnstateAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnstateAdd.Click



        Dim str As String = "'" + String.Join("','", txtState.Text.ToString()) + "'"
        txtStateMul.Text += str + ","
        txtState.Text = ""




    End Sub

    Private Sub btnstationAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnstationAdd.Click

        Dim str As String = "'" + String.Join("','", txtStation.Text.ToString()) + "'"
        txtStationMul.Text += str + ","
        txtStation.Text = ""


    End Sub

    Private Sub btnPlantAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlantAdd.Click


        Dim str As String = "'" + String.Join("','", txtPlant.Text.ToString()) + "'"
        txtPlantMul.Text += str + ","
        txtPlant.Text = ""



    End Sub

    Private Sub btnProjectTypeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProjectTypeAdd.Click


        Dim str As String = "'" + String.Join("','", txtProjectType.Text.ToString()) + "'"
        txtProjectTypeMulti.Text += str + ","
        txtProjectType.Text = ""



    End Sub

    Private Sub btnDispAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDispAdd.Click

        Dim str As String = "'" + String.Join("','", txtDispStatus.Text.ToString()) + "'"
        txtDispStatusMul.Text += str + ","
        txtDispStatus.Text = ""



    End Sub

    Private Sub btnFinalSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalSearch.Click


        Try

            Dim criteria As String
            criteria = ""
            If txtStateMul.Text.Trim() <> "" Then
                criteria = criteria + "addr.State in (" + txtStateMul.Text.Trim().TrimEnd(",") + ") and "
            End If
            If txtStationMul.Text.Trim() <> "" Then
                criteria = criteria + " addr.City  in (" + txtStationMul.Text.Trim().TrimEnd(",") + ") and "
            End If
            If txtPlantMul.Text.Trim() <> "" Then
                criteria = criteria + "project.PlantName in(" + txtPlantMul.Text.Trim().TrimEnd(",") + ") and "
            End If
            If txtProjectTypeMulti.Text.Trim() <> "" Then
                criteria = criteria + " order1.OrderStatus in (" + txtProjectTypeMulti.Text.Trim().TrimEnd(",") + ") and "
            End If
            If txtDispStatusMul.Text.Trim() <> "" Then
                criteria = criteria + " project.DType in(" + txtDispStatusMul.Text.Trim().TrimEnd(",") + ") and "
            End If

            If txtAllUser.Text.Trim() <> "" Then
                criteria = criteria + " UM.UserName in(" + txtAllUser.Text.Trim().TrimEnd(",") + ") and "
            End If
            ''OrderDate  Between Date 
            If (chkIsDate.Checked = True) Then
                criteria = criteria + " CONVERT(date, order1.OrderDate,103)  >= CONVERT(date ,'" + txtstartDt.Value.Date + "',103) And CONVERT(date, order1.OrderDate,103) <= CONVERT(date ,'" + txtEndDate.Value.Date + "',103) And "
            End If

            ''Dispatch Date With 
            If (rblDispatchWith.Checked = True) Then
                criteria = criteria + " CONVERT(date, order1.DispatchDate,103)  >= CONVERT(date ,'" + txtstartDt.Value.Date + "',103) And CONVERT(date, order1.DispatchDate,103) <= CONVERT(date ,'" + txtEndDate.Value.Date + "',103) And "

            End If
            ''Dispatch Date WithOut 
            If (rbldispwithout.Checked = True) Then
                criteria = criteria + " order1.DispatchDate ='1900-01-01 00:00:00.000' and "
            End If

            ''Dispatch Date Report Between navin 24-06-2014
            ''OrderDate  Between Date 
            If (chkIsDispaDate.Checked = True) Then
                If rblClientWithPacking.Checked = True Then
                    criteria = criteria + " CONVERT(date,PDN.Packing_DispDate,103) >= '" + Class1.getDate(txtstartDt.Text) + "' And CONVERT(date,PDN.Packing_DispDate,103) <= '" + Class1.getDate(txtEndDate.Text) + "' And "
                End If
            End If

            'Rec. MKT.Dt Navin 24-06-2014
            If (chkMKTDate.Checked = True) Then
                criteria = criteria + " CONVERT(date, order1.OrderRecFromMkt,103) >= '" + Class1.getDate(txtstartDt.Text) + "' And CONVERT(date, order1.OrderRecFromMkt,103) <= '" + Class1.getDate(txtEndDate.Text) + "' And "
            End If

            If criteria = " and " Then
                criteria = ""
            End If
            If (criteria.Length > 0) Then
                criteria = criteria.ToString().Trim().Substring(0, criteria.Trim().Length - 3)
            End If
            Dim cmd As New SqlCommand
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter
            Dim str As String
            str = System.Configuration.ConfigurationManager.AppSettings("constr").ToString()
            Dim con As New SqlConnection(str)
            Dim Query As String
            If rblClientWithPacking.Checked = True Then
                Query = " select ROW_NUMBER() OVER(ORDER BY addr.Pk_AddressID) as 'SrNO',addr.EnqNo,addr.Name,addr.MobileNo,addr.EmailID,addr.DeliveryCity,addr.DeliveryDistrict,addr.DeliveryState,addr.DeliveryAddress,addr.DeliveryPincode,order1.BrandName,PDN.Packing_Item,PDN.Packing_Capacity,PDN.Packing_Status as ByProject,project.DType as DisStatus,CONVERT(date,PDN.Packing_DispDate,103) as DispatchDate, order1.OrderStatus,CONVERT(date,order1.OrderDate,103)as OrderDate,UM.UserName from dbo.Address_Master as addr "
                Query += " inner join dbo.Tbl_OrderOneMaster as order1 on addr.Pk_AddressID=order1.Fk_AddressId "
                Query += " inner join dbo.Tbl_UserAllotmentDetail as UA on UA.Fk_AddressID=order1.Fk_AddressId "
                Query += " inner join dbo.User_Master as UM on UM.Pk_UserId=UA.Fk_UserId " 
                Query += " left join  dbo.Tbl_ProjectInformationMaster as project on project.Fk_AddressId =addr.Pk_AddressID "
                Query += " left join PackingDetail_New as PDN on PDN.Fk_Address_ID=order1.Fk_AddressId "
            Else
                Query = " select ROW_NUMBER() OVER(ORDER BY addr.Pk_AddressID) as 'SrNO',addr.EnqNo,addr.Name,addr.MobileNo,addr.EmailID,addr.DeliveryCity,addr.DeliveryDistrict,addr.DeliveryState,addr.DeliveryAddress,addr.DeliveryPincode,order1.BrandName,project.DType as DisStatus, order1.OrderStatus,CONVERT(date,order1.DispatchDate,103) as DispatchDate,CONVERT(date,order1.OrderDate,103)as OrderDate,UM.UserName from dbo.Address_Master as addr "
                Query += " inner join dbo.Tbl_OrderOneMaster as order1 on addr.Pk_AddressID=order1.Fk_AddressId "
                Query += " inner join dbo.Tbl_UserAllotmentDetail as UA on UA.Fk_AddressID=order1.Fk_AddressId "
                Query += " inner join dbo.User_Master as UM on UM.Pk_UserId=UA.Fk_UserId " 
                Query += " left join  dbo.Tbl_ProjectInformationMaster as project on project.Fk_AddressId = addr.Pk_AddressID "
            End If
            If (criteria.Trim().Length > 0) Then
                Query += " where " + criteria + " order by addr.Pk_AddressID "
            End If

            da = New SqlDataAdapter(Query, con)
            ds = New DataSet()
            da.Fill(ds)

            'MessageBox.Show(Query)

            'cmd = New SqlCommand(Query)
            'Dim objclass As New Class1
            'ds = objclass.GetEnqReportData(cmd)
            If ds.Tables(0).Rows.Count < 1 Then
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GvOrdeReportList.DataSource = Nothing
            Else
                GvOrdeReportList.DataSource = ds.Tables(0)
                GvOrdeReportList.Columns(0).Width = 40
            End If
            lblTotalCount.Text = "Total:" + ds.Tables(0).Rows.Count.ToString()
            ds.Dispose()
            da.Dispose()

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
                    strfinal += tmpstr(index) + ","
                End If
            Next
            strfinal = strfinal.ToString().Substring(0, strfinal.Length - 1)
            ' strfinal += ""
        End If

        Return strfinal
    End Function

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
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
            For i As Integer = 1 To GvOrdeReportList.Columns.Count
                worksheet.Cells(1, i) = GvOrdeReportList.Columns(i - 1).HeaderText
            Next

            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvOrdeReportList.Rows.Count - 1
                For j As Integer = 0 To GvOrdeReportList.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1) = GvOrdeReportList.Rows(i).Cells(j).Value.ToString()
                    count += i + 2 + j + 1
                Next
            Next 
        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)



    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
        '    Microsoft.Office.Interop.Excel.Workbook workbook=excelApp.Workbooks.Open(@"E:\Personal\Code\VBA\Excel\PrintTest.xlsx");
        '    Microsoft.Office.Interop.Excel.Worksheet worksheet;
        '    for (int i = 1; i <= workbook.Worksheets.Count; i++)
        '    {
        '        if (i != 1)
        '        {
        '            worksheet = workbook.Worksheets[i];
        '            //modify you specify title row here
        '            worksheet.PageSetup.PrintTitleRows = "$1:$2";
        '        }
        '    }
        '    workbook.Save();
        '    workbook.Close();
        '    excelApp.Quit();
    End Sub

    Private Sub btnStationClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStationClear.Click
        txtStationMul.Text = ""
    End Sub

    Private Sub btnStateClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStateClear.Click
        txtStateMul.Text = ""
    End Sub

    Private Sub btnPlantClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlantClear.Click
        txtPlantMul.Text = ""
    End Sub

    Private Sub btnOStatusClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOStatusClear.Click
        txtProjectTypeMulti.Text = ""

    End Sub

    Private Sub btnDisStatusClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisStatusClear.Click
        txtDispStatusMul.Text = ""
    End Sub

    Private Sub chkIsDispaDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsDispaDate.CheckedChanged
        Clean_Multiline()


    End Sub
    Public Sub Clean_Multiline()

        txtStationMul.Text = ""
        txtStateMul.Text = ""
        txtPlantMul.Text = ""
        txtProjectTypeMulti.Text = ""
        txtDispStatusMul.Text = ""
        chkIsDate.Checked = False






    End Sub

    Private Sub chkMKTDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMKTDate.CheckedChanged
        Clean_Multiline()

    End Sub

    Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Dim str As String = "'" + String.Join("','", cmbUser.Text.ToString()) + "'"
        txtAllUser.Text += str + ","


    End Sub

  
End Class