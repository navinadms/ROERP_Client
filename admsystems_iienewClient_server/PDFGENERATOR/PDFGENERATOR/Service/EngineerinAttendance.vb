Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine


Public Class EngineerinAttendance
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Pk_Engineer_ID As Integer
    Shared AttStatus As String

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    Public Sub GvServiceEngineer_Bind()

        GvEngineerAttedance.DataSource = Nothing
        Dim dt As New DataTable
        dt.Columns.Add("Fk_Address_ID") '0
        dt.Columns.Add("Fk_Engineer_ID") '1
        dt.Columns.Add("TodayDate")     '2
        dt.Columns.Add("EngineerName")  '3
        dt.Columns.Add("MobileNo") '4
        dt.Columns.Add("Status") '5
        dt.Columns.Add("ODType") '6
        dt.Columns.Add("SiteNo") '7
        dt.Columns.Add("EnqNo") '8
        dt.Columns.Add("PartyName") '9
        dt.Columns.Add("Station") '10
        dt.Columns.Add("PartyMobileNo") '11
        dt.Columns.Add("PartyEmail") '12
        dt.Columns.Add("Machine") '13
        dt.Columns.Add("Capacity") '14
        dt.Columns.Add("NoOfDays") '15
        dt.Columns.Add("EndDate") '16
        dt.Columns.Add("Type") '17
        dt.Columns.Add("Days") '18   
        dt.Columns.Add("MachineStatus") '19
        dt.Columns.Add("MachineType") '20
        dt.Columns.Add("ODSiteStatus") '21
        dt.Columns.Add("Remark") '22
        dt.Columns.Add("Engg_Code") '23
        dt.Columns.Add("Expense_Status") '24

        If rblOffice.Checked = True Then
            AttStatus = "Office"
        ElseIf rblAbsent.Checked = True Then
            AttStatus = "Absent"
        ElseIf rblOD.Checked = True Then
            AttStatus = "OD"
        ElseIf rblOfficeOD.Checked = True Then
            AttStatus = "Office OD"
        ElseIf rblLeave.Checked = True Then
            AttStatus = "Leave"
        ElseIf rblSunday.Checked = True Then
            AttStatus = "SUNDAY"
        ElseIf rblHoliday.Checked = True Then
            AttStatus = "HOLIDAY"
        Else
            AttStatus = "All"
        End If

        Dim officetotal As Integer
        Dim absenttotal As Integer
        Dim odtotal As Integer
        Dim officeodtotal As Integer
        Dim leavetotal As Integer
        Dim sundaytotal As Integer
        Dim Holidaytotal As Integer
        Dim alltotal As Integer


        officetotal = 0
        absenttotal = 0
        odtotal = 0
        officeodtotal = 0
        leavetotal = 0
        sundaytotal = 0

        alltotal = 0
        Holidaytotal = 0

        Dim EngineerData = linq_obj.SP_Get_Service_Engineer_Master_List().ToList()
        For Each itemEng As SP_Get_Service_Engineer_Master_ListResult In EngineerData
            'Date Loop
            Dim CurrentDate As DateTime = dtStartDate.Value.Date
            While CurrentDate <= dtEndDate.Value.Date
                '    CurrentDate = CurrentDate.AddDays(1)
                'End While
                For Flash = 0 To 2
                    Dim cmd As New SqlCommand
                    cmd.CommandTimeout = 3000
                    cmd.CommandText = "SP_Get_Engineer_Attandance_List_New"
                    cmd.Parameters.AddWithValue("@Flag", SqlDbType.Int).Value = Flash
                    cmd.Parameters.AddWithValue("@Fk_Address_ID", SqlDbType.BigInt).Value = 0
                    cmd.Parameters.AddWithValue("@Fk_Engineer_ID", SqlDbType.BigInt).Value = itemEng.Pk_Engineer_ID
                    cmd.Parameters.Add("@TodayDate", SqlDbType.DateTime).Value = CurrentDate.Date
                    Dim objclass As New Class1
                    Dim ds As New DataSet
                    ds = objclass.GetEnqReportData(cmd)
                    If ds.Tables(1).Rows.Count > 0 Then
                        'All Data
                        If AttStatus = "All" Then
                            'total Engineer data 

                            Dim i As Integer
                            i = 0
                            For i = 0 To ds.Tables(1).Rows.Count - 1
                                If Convert.ToString(ds.Tables(1).Rows(i)("Status").ToString().ToLower()) = "office" Then
                                    officetotal = officetotal + 1

                                ElseIf Convert.ToString(ds.Tables(1).Rows(i)("Status").ToString().ToLower()) = "absent" Then
                                    absenttotal = absenttotal + 1

                                ElseIf Convert.ToString(ds.Tables(1).Rows(i)("Status").ToString().ToLower()) = "od" Then
                                    odtotal = odtotal + 1

                                ElseIf Convert.ToString(ds.Tables(1).Rows(i)("Status").ToString().ToLower()) = "office od" Then
                                    officeodtotal = officeodtotal + 1
                                ElseIf Convert.ToString(ds.Tables(1).Rows(i)("Status").ToString().ToLower()) = "leave" Then
                                    leavetotal = leavetotal + 1

                                ElseIf Convert.ToString(ds.Tables(1).Rows(i)("Status").ToString().ToLower()) = "sunday" Then
                                    sundaytotal = sundaytotal + 1
                                ElseIf Convert.ToString(ds.Tables(1).Rows(i)("Status").ToString().ToLower()) = "holiday" Then
                                    Holidaytotal = Holidaytotal + 1
                                End If
                                dt.Rows.Add(ds.Tables(1).Rows(i)("Fk_Address_ID"),
                                        ds.Tables(1).Rows(i)("Fk_Engineer_ID"),
                                        CurrentDate.Date.ToString("dd/MM/yyyy"),
                                        ds.Tables(1).Rows(i)("EngineerName"),
                                        ds.Tables(1).Rows(i)("MobileNo"),
                                        ds.Tables(1).Rows(i)("Status"),
                                        ds.Tables(1).Rows(i)("ODType"),
                                        ds.Tables(1).Rows(i)("SiteNo"),
                                        ds.Tables(1).Rows(i)("EnqNo"),
                                        ds.Tables(1).Rows(i)("PartyName"),
                                        ds.Tables(1).Rows(i)("Station"),
                                        ds.Tables(1).Rows(i)("PartyMobileNo"),
                                        ds.Tables(1).Rows(i)("PartyEmail"),
                                        ds.Tables(1).Rows(i)("Machine"),
                                        ds.Tables(1).Rows(i)("Capacity"),
                                        ds.Tables(1).Rows(i)("NoOfDays"),
                                        ds.Tables(1).Rows(i)("EndDate"),
                                        ds.Tables(1).Rows(i)("Type"),
                                        ds.Tables(1).Rows(i)("Days"),
                                        ds.Tables(1).Rows(i)("MachineStatus"),
                                        ds.Tables(1).Rows(i)("MachineType"),
                                        ds.Tables(1).Rows(i)("ODSiteStatus"),
                                        ds.Tables(1).Rows(i)("Remark"),
                                        ds.Tables(1).Rows(i)("Engg_Code"),
                                        ds.Tables(1).Rows(i)("Expense_Status")
                                    )
                            Next
                            lblALLTotal.Text = Convert.ToString(officetotal + absenttotal + odtotal + officeodtotal + leavetotal + sundaytotal + Holidaytotal)

                        Else

                            'total Engineer data 
                            Dim j As Integer
                            j = 0

                            For j = 0 To ds.Tables(1).Rows.Count - 1
                                If Convert.ToString(ds.Tables(1).Rows(j)("Status").ToString().ToLower()) = Convert.ToString(AttStatus.ToLower()) Then
                                    If Convert.ToString(ds.Tables(1).Rows(j)("Status").ToString().ToLower()) = "office" Then
                                        officetotal = officetotal + 1
                                    ElseIf Convert.ToString(ds.Tables(1).Rows(j)("Status").ToString().ToLower()) = "absent" Then
                                        absenttotal = absenttotal + 1
                                    ElseIf Convert.ToString(ds.Tables(1).Rows(j)("Status").ToString().ToLower()) = "od" Then
                                        odtotal = odtotal + 1
                                    ElseIf Convert.ToString(ds.Tables(1).Rows(j)("Status").ToString().ToLower()) = "office od" Then
                                        officeodtotal = officeodtotal + 1
                                    ElseIf Convert.ToString(ds.Tables(1).Rows(j)("Status").ToString().ToLower()) = "leave" Then
                                        leavetotal = leavetotal + 1
                                    ElseIf Convert.ToString(ds.Tables(1).Rows(j)("Status").ToString().ToLower()) = "sunday holiday" Then
                                        sundaytotal = sundaytotal + 1
                                    ElseIf Convert.ToString(ds.Tables(1).Rows(j)("Status").ToString().ToLower()) = "holiday" Then
                                        Holidaytotal = Holidaytotal + 1
                                    End If
                                    dt.Rows.Add(ds.Tables(1).Rows(j)("Fk_Address_ID"),
                                          ds.Tables(1).Rows(j)("Fk_Engineer_ID"),
                                          CurrentDate.Date.ToString("dd/MM/yyyy"),
                                          ds.Tables(1).Rows(j)("EngineerName"),
                                            ds.Tables(1).Rows(j)("MobileNo"),
                                            ds.Tables(1).Rows(j)("Status"),
                                            ds.Tables(1).Rows(j)("ODType"),
                                            ds.Tables(1).Rows(j)("SiteNo"),
                                            ds.Tables(1).Rows(j)("EnqNo"),
                                            ds.Tables(1).Rows(j)("PartyName"),
                                            ds.Tables(1).Rows(j)("Station"),
                                            ds.Tables(1).Rows(j)("PartyMobileNo"),
                                            ds.Tables(1).Rows(j)("PartyEmail"),
                                            ds.Tables(1).Rows(j)("Machine"),
                                            ds.Tables(1).Rows(j)("Capacity"),
                                            ds.Tables(1).Rows(j)("NoOfDays"),
                                            ds.Tables(1).Rows(j)("EndDate"),
                                            ds.Tables(1).Rows(j)("Type"),
                                            ds.Tables(1).Rows(j)("Days"),
                                            ds.Tables(1).Rows(j)("MachineStatus"),
                                            ds.Tables(1).Rows(j)("MachineType"),
                                            ds.Tables(1).Rows(j)("ODSiteStatus"),
                                            ds.Tables(1).Rows(j)("Remark"),
                                            ds.Tables(1).Rows(j)("Engg_Code"),
                                            ds.Tables(1).Rows(j)("Expense_Status")
                                          )
                                End If
                            Next


                        End If


                        Exit For



                    End If
                Next
                CurrentDate = CurrentDate.AddDays(1)
            End While

        Next
        If dt.Rows.Count > 0 Then
            GvEngineerAttedance.DataSource = Nothing
            GvEngineerAttedance.DataSource = dt
            GvEngineerAttedance.Columns("TodayDate").Width = 50
            GvEngineerAttedance.Columns("Status").Width = 70
            GvEngineerAttedance.Columns("ODType").Width = 70
            GvEngineerAttedance.Columns("NoOfDays").Width = 50
            GvEngineerAttedance.Columns("EndDate").Width = 100
            GvEngineerAttedance.Columns("Type").Width = 100
            GvEngineerAttedance.Columns(0).Visible = False
            GvEngineerAttedance.Columns(1).Visible = False
            GvEngineerAttedance.Columns(23).Visible = False
            GvEngineerAttedance.Columns("ODType").Frozen = True
            GvEngineerAttedance.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Else
            GvEngineerAttedance.DataSource = Nothing

        End If
        'lblTotalOffice.Text = GvEngineerAttedance.Rows.Count.ToString()
        officecount.Text = officetotal.ToString()
        absentcount.Text = absenttotal.ToString()
        odcount.Text = odtotal.ToString()
        officeodcount.Text = officeodtotal.ToString()
        leavecount.Text = leavetotal.ToString()
        sundaycount.Text = sundaytotal.ToString()
        lblHolidayTotal.Text = Holidaytotal.ToString()
        AddTooltips()
        Engineer_Color_Code()
        Machine_Capacity_Color()
    End Sub
    Public Sub Engineer_Color_Code()

        For index = 0 To GvEngineerAttedance.RowCount - 1
            If (GvEngineerAttedance.Rows(index).Cells("Engg_Code").Value.ToString().Contains("A-")) Then
                GvEngineerAttedance.Rows(index).Cells("EngineerName").Style.BackColor = System.Drawing.Color.Black
                GvEngineerAttedance.Rows(index).Cells("EngineerName").Style.ForeColor = System.Drawing.Color.White
            End If
            If (GvEngineerAttedance.Rows(index).Cells("Engg_Code").Value.ToString().Contains("B-")) Then
                GvEngineerAttedance.Rows(index).Cells("EngineerName").Style.BackColor = System.Drawing.Color.Yellow
                GvEngineerAttedance.Rows(index).Cells("EngineerName").Style.ForeColor = System.Drawing.Color.Black

            End If
            If (GvEngineerAttedance.Rows(index).Cells("Engg_Code").Value.ToString().Contains("C-")) Then
                GvEngineerAttedance.Rows(index).Cells("EngineerName").Style.BackColor = System.Drawing.Color.Green
                GvEngineerAttedance.Rows(index).Cells("EngineerName").Style.ForeColor = System.Drawing.Color.White

            End If
        Next
    End Sub
    Public Sub Machine_Capacity_Color()
        For index = 0 To GvEngineerAttedance.RowCount - 1
            Dim OdType As String = GvEngineerAttedance.Rows(index).Cells("ODType").Value

            Dim Machine As String = GvEngineerAttedance.Rows(index).Cells("Machine").Value
            Dim Capacity As String = GvEngineerAttedance.Rows(index).Cells("Capacity").Value
            Dim Status As String = GvEngineerAttedance.Rows(index).Cells("Status").Value
            If (Status.Trim().ToLower() = "od") Then

                If OdType.ToLower() = "o_ec_g" Or OdType.ToLower() = "o_ec_b" Or OdType.ToLower() = "o_ec_y" Or OdType.ToLower() = "o_service_b" Or OdType.ToLower() = "o_service_g" Or OdType.ToLower() = "o_service_y" Or OdType.ToLower() = "o_training_y" Or OdType.ToLower() = "o_training_b" Or OdType.ToLower() = "o_training_g" Then
                    If OdType.ToLower() = "o_service_b" Or OdType.ToLower() = "o_ec_b" Or OdType.ToLower() = "o_training_b" Then
                        GvEngineerAttedance.Rows(index).Cells("Machine").Style.BackColor = System.Drawing.Color.Black
                        GvEngineerAttedance.Rows(index).Cells("Machine").Style.ForeColor = System.Drawing.Color.White
                        GvEngineerAttedance.Rows(index).Cells("Capacity").Style.BackColor = System.Drawing.Color.Black
                        GvEngineerAttedance.Rows(index).Cells("Capacity").Style.ForeColor = System.Drawing.Color.White

                    ElseIf OdType.ToLower() = "o_service_y" Or OdType.ToLower() = "o_ec_y" Or OdType.ToLower() = "o_training_y" Then
                        GvEngineerAttedance.Rows(index).Cells("Machine").Style.BackColor = System.Drawing.Color.Yellow
                        GvEngineerAttedance.Rows(index).Cells("Machine").Style.ForeColor = System.Drawing.Color.Black
                        GvEngineerAttedance.Rows(index).Cells("Capacity").Style.BackColor = System.Drawing.Color.Yellow
                        GvEngineerAttedance.Rows(index).Cells("Capacity").Style.ForeColor = System.Drawing.Color.Black

                    ElseIf OdType.ToLower() = "o_service_g" Or OdType.ToLower() = "o_ec_g" Or OdType.ToLower() = "o_training_g" Then
                        GvEngineerAttedance.Rows(index).Cells("Machine").Style.BackColor = System.Drawing.Color.Green
                        GvEngineerAttedance.Rows(index).Cells("Machine").Style.ForeColor = System.Drawing.Color.White
                        GvEngineerAttedance.Rows(index).Cells("Capacity").Style.BackColor = System.Drawing.Color.Green
                        GvEngineerAttedance.Rows(index).Cells("Capacity").Style.ForeColor = System.Drawing.Color.White
                    End If
                Else
                    Dim data = linq_obj.SP_Get_Packing_Item_Capacity_Color(OdType.Trim(), Machine.Trim(), Capacity.Trim()).ToList()
                    If data.Count > 0 Then
                        For Each item As SP_Get_Packing_Item_Capacity_ColorResult In data
                            If (item.ColorName = "black") Then
                                GvEngineerAttedance.Rows(index).Cells("Machine").Style.BackColor = System.Drawing.Color.Black
                                GvEngineerAttedance.Rows(index).Cells("Machine").Style.ForeColor = System.Drawing.Color.White
                                GvEngineerAttedance.Rows(index).Cells("Capacity").Style.BackColor = System.Drawing.Color.Black
                                GvEngineerAttedance.Rows(index).Cells("Capacity").Style.ForeColor = System.Drawing.Color.White

                            ElseIf (item.ColorName = "yellow") Then
                                GvEngineerAttedance.Rows(index).Cells("Machine").Style.BackColor = System.Drawing.Color.Yellow
                                GvEngineerAttedance.Rows(index).Cells("Machine").Style.ForeColor = System.Drawing.Color.Black
                                GvEngineerAttedance.Rows(index).Cells("Capacity").Style.BackColor = System.Drawing.Color.Yellow
                                GvEngineerAttedance.Rows(index).Cells("Capacity").Style.ForeColor = System.Drawing.Color.Black
                                GvEngineerAttedance.Rows(index).Cells("NoOfDays").Value = item.ExpDays
                            ElseIf (item.ColorName = "green") Then
                                GvEngineerAttedance.Rows(index).Cells("Machine").Style.BackColor = System.Drawing.Color.Green
                                GvEngineerAttedance.Rows(index).Cells("Machine").Style.ForeColor = System.Drawing.Color.White
                                GvEngineerAttedance.Rows(index).Cells("Capacity").Style.BackColor = System.Drawing.Color.Green
                                GvEngineerAttedance.Rows(index).Cells("Capacity").Style.ForeColor = System.Drawing.Color.White
                            End If

                        Next


                    End If
                End If

            End If
        Next
    End Sub
    Public Sub Machine_Capacity_Color1(ByVal Rowindex As Integer, ByVal OdType As String, ByVal Machine As String, ByVal Capacity As String)

        If OdType.ToLower() = "o_ec_g" Or OdType.ToLower() = "o_ec_b" Or OdType.ToLower() = "o_ec_y" Or OdType.ToLower() = "o_service_b" Or OdType.ToLower() = "o_service_g" Or OdType.ToLower() = "o_service_y" Or OdType.ToLower() = "o_training_y" Or OdType.ToLower() = "o_training_b" Or OdType.ToLower() = "o_training_g" Then
            If OdType.ToLower() = "o_service_b" Or OdType.ToLower() = "o_ec_b" Or OdType.ToLower() = "o_training_b" Then
                GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.BackColor = System.Drawing.Color.Black
                GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.ForeColor = System.Drawing.Color.White
                GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.BackColor = System.Drawing.Color.Black
                GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.ForeColor = System.Drawing.Color.White

            ElseIf OdType.ToLower() = "o_service_y" Or OdType.ToLower() = "o_ec_y" Or OdType.ToLower() = "o_training_y" Then
                GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.BackColor = System.Drawing.Color.Yellow
                GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.ForeColor = System.Drawing.Color.Black
                GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.BackColor = System.Drawing.Color.Yellow
                GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.ForeColor = System.Drawing.Color.Black

            ElseIf OdType.ToLower() = "o_service_g" Or OdType.ToLower() = "o_ec_g" Or OdType.ToLower() = "o_training_g" Then
                GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.BackColor = System.Drawing.Color.Green
                GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.ForeColor = System.Drawing.Color.White
                GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.BackColor = System.Drawing.Color.Green
                GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.ForeColor = System.Drawing.Color.White
            End If
        Else
            Dim data = linq_obj.SP_Get_Packing_Item_Capacity_Color(OdType.Trim().ToLower(), Machine.Trim(), Capacity.Trim()).ToList()
            If data.Count > 0 Then
                For Each item As SP_Get_Packing_Item_Capacity_ColorResult In data
                    If (item.ColorName = "black") Then
                        GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.BackColor = System.Drawing.Color.Black
                        GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.ForeColor = System.Drawing.Color.White
                        GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.BackColor = System.Drawing.Color.Black
                        GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.ForeColor = System.Drawing.Color.White
                        GvEngineerAttedance.Rows(Rowindex).Cells("NoOfDays").Value = item.ExpDays
                    ElseIf (item.ColorName = "yellow") Then
                        GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.BackColor = System.Drawing.Color.Yellow
                        GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.ForeColor = System.Drawing.Color.Black
                        GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.BackColor = System.Drawing.Color.Yellow
                        GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.ForeColor = System.Drawing.Color.Black
                        GvEngineerAttedance.Rows(Rowindex).Cells("NoOfDays").Value = item.ExpDays
                    ElseIf (item.ColorName = "green") Then
                        GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.BackColor = System.Drawing.Color.Green
                        GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.ForeColor = System.Drawing.Color.White
                        GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.BackColor = System.Drawing.Color.Green
                        GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.ForeColor = System.Drawing.Color.White
                        GvEngineerAttedance.Rows(Rowindex).Cells("NoOfDays").Value = item.ExpDays
                    End If

                Next
            Else
                MessageBox.Show("Color not found...")
                GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.BackColor = System.Drawing.Color.White
                GvEngineerAttedance.Rows(Rowindex).Cells("Machine").Style.ForeColor = System.Drawing.Color.Black
                GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.BackColor = System.Drawing.Color.White
                GvEngineerAttedance.Rows(Rowindex).Cells("Capacity").Style.ForeColor = System.Drawing.Color.Black
            End If
        End If

    End Sub


    Private Sub AddTooltips()
        For Each drow As DataGridViewRow In GvEngineerAttedance.Rows

            'Status
            Dim dgvCell5 As DataGridViewCell
            dgvCell5 = drow.Cells("Status")
            dgvCell5.ToolTipText = "ABSENT,OFFICE,OD,OFFICE OD,LEAVE,SUNDAY HOLIDAY"

            'OD Type
            Dim dgvCell6 As DataGridViewCell
            dgvCell6 = drow.Cells("ODType")
            dgvCell6.ToolTipText = "EC,Service,Visit,Training"

            'Type
            Dim dgvCell9 As DataGridViewCell
            dgvCell9 = drow.Cells("Type")
            dgvCell9.ToolTipText = "UP,DOWN,WORK"

            'Machine Status
            Dim dgvCell18 As DataGridViewCell
            dgvCell18 = drow.Cells("MachineStatus")
            dgvCell18.ToolTipText = "PENDING,DONE"

            'Machine Type
            Dim dgvCell20 As DataGridViewCell
            dgvCell20 = drow.Cells("MachineType")
            dgvCell20.ToolTipText = "EC,Erection,Commission,Service"

            'OD Site Status
            Dim dgvCell21 As DataGridViewCell
            dgvCell21 = drow.Cells("ODSiteStatus")
            dgvCell21.ToolTipText = "RUNNING,DONE,CANCEL"


        Next
    End Sub
    Private Sub GvEngineerAttedance_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvEngineerAttedance.CellValueChanged


        If e.ColumnIndex = 5 Then 'Status
            Dim Status As String = Convert.ToString(GvEngineerAttedance.Rows(e.RowIndex).Cells("Status").Value)
            If Status.ToLower() = "absent" And Status.ToLower() = "office" Then
                GvEngineerAttedance.Rows(e.RowIndex).Cells("ODType").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("EndDate").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("Type").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("Days").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("SiteNo").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("EnqNo").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("PartyName").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("Station").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("PartyMobileNo").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("PartyEmail").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("Machine").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("MachineStatus").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("MachineType").ReadOnly = True
                GvEngineerAttedance.Rows(e.RowIndex).Cells("ODSiteStatus").ReadOnly = True

            End If

            If Status.ToLower() <> "od" Then
                GvEngineerAttedance.Rows(e.RowIndex).Cells("ODType").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("EndDate").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("Type").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("Days").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("SiteNo").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("EnqNo").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("PartyName").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("Station").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("PartyMobileNo").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("PartyEmail").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("Machine").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("MachineStatus").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("MachineType").Value = ""
                GvEngineerAttedance.Rows(e.RowIndex).Cells("ODSiteStatus").Value = ""

            End If
        End If

        If e.ColumnIndex = 15 Then 'No of Days

            Dim value As String = GvEngineerAttedance.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            For Each c As Char In value
                If Char.IsDigit(c) Then
                    'This will set the defaultvalue of the datagrid cell in question to the value of "3"
                    ' Exit Sub
                    Dim NoofDays As String = GvEngineerAttedance.Rows(e.RowIndex).Cells("NoOfDays").Value
                    GvEngineerAttedance.Rows(e.RowIndex).Cells("EndDate").Value = dtEndDate.Value.AddDays(Convert.ToInt32(NoofDays)).ToString("dd/MM/yyyy")
                Else
                    MessageBox.Show("Please Enter numeric Value")
                    GvEngineerAttedance.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                End If
            Next
        End If

        If e.ColumnIndex = 8 Then 'Enq No
            Dim EnqNo As String = Convert.ToString(GvEngineerAttedance.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            If EnqNo <> "" Then
                Dim data = linq_obj.SP_Get_ServiceODSite_Allotment_EnqNo(EnqNo.Trim()).ToList()
                If data.Count > 0 Then
                    For Each item As SP_Get_ServiceODSite_Allotment_EnqNoResult In data
                        GvEngineerAttedance.Rows(e.RowIndex).Cells("PartyName").Value = item.Name
                        GvEngineerAttedance.Rows(e.RowIndex).Cells("Station").Value = item.City + "," + item.BillState
                        GvEngineerAttedance.Rows(e.RowIndex).Cells("PartyMobileNo").Value = item.MobileNo
                        GvEngineerAttedance.Rows(e.RowIndex).Cells("PartyEmail").Value = item.EmailID
                        GvEngineerAttedance.Rows(e.RowIndex).Cells(0).Value = item.Pk_AddressID

                    Next
                Else
                    MessageBox.Show("Invalid EnqNo...")
                End If
            End If
        End If

        If e.ColumnIndex = 13 Then 'Machine     
            Dim dataCapacity = linq_obj.SP_Get_PackingDetail_New_List(Convert.ToInt64(GvEngineerAttedance.Rows(e.RowIndex).Cells("Fk_Address_ID").Value)).ToList()
            GvEngineerAttedance.Rows(e.RowIndex).Cells("Capacity").Value = ""
            For Each item As SP_Get_PackingDetail_New_ListResult In dataCapacity
                If Convert.ToString(GvEngineerAttedance.Rows(e.RowIndex).Cells("Machine").Value).Trim().ToLower() = Convert.ToString(item.Item).ToLower().Trim() Then
                    GvEngineerAttedance.Rows(e.RowIndex).Cells("Capacity").Value = Convert.ToString(item.Capacity)
                End If
            Next

            If Convert.ToString(GvEngineerAttedance.Rows(e.RowIndex).Cells("ODType").Value) <> "" And Convert.ToString(GvEngineerAttedance.Rows(e.RowIndex).Cells("Machine").Value) <> "" And Convert.ToString(GvEngineerAttedance.Rows(e.RowIndex).Cells("Capacity").Value) <> "" Then
                Dim OdType As String = GvEngineerAttedance.Rows(e.RowIndex).Cells("ODType").Value
                Dim Machine As String = GvEngineerAttedance.Rows(e.RowIndex).Cells("Machine").Value
                Dim Capacity As String = GvEngineerAttedance.Rows(e.RowIndex).Cells("Capacity").Value
                If OdType.Trim() <> "" And Machine.Trim() <> "" And Capacity.Trim() <> "" Then
                    Machine_Capacity_Color1(e.RowIndex, OdType, Machine, Capacity)
                End If
            End If
        End If
        If e.ColumnIndex = 14 Or e.ColumnIndex = 6 Then 'Capacity Color  
            If Convert.ToString(GvEngineerAttedance.Rows(e.RowIndex).Cells("ODType").Value) <> "" And Convert.ToString(GvEngineerAttedance.Rows(e.RowIndex).Cells("Machine").Value) <> "" And Convert.ToString(GvEngineerAttedance.Rows(e.RowIndex).Cells("Capacity").Value) <> "" Then
                Dim OdType As String = GvEngineerAttedance.Rows(e.RowIndex).Cells("ODType").Value
                Dim Machine As String = GvEngineerAttedance.Rows(e.RowIndex).Cells("Machine").Value
                Dim Capacity As String = GvEngineerAttedance.Rows(e.RowIndex).Cells("Capacity").Value
                If OdType.Trim() <> "" And Machine.Trim() <> "" And Capacity.Trim() <> "" Then
                    Machine_Capacity_Color1(e.RowIndex, OdType, Machine, Capacity)
                End If
            End If
        End If
        If e.ColumnIndex = 21 Then 'Check OD  Site Status  Update Status
            Dim value As String = GvEngineerAttedance.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Dim Fk_Address_ID As String = GvEngineerAttedance.Rows(e.RowIndex).Cells(0).Value
            If value.ToLower() = "done" Or value.ToLower = "cancel" Then
                Dim result As DialogResult = MessageBox.Show("Are You Sure OD Site " + value + " ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                If result = DialogResult.Yes Then
                    linq_obj.SP_Update_ODSite_Master_Status(Convert.ToInt32(Fk_Address_ID), value)
                    linq_obj.SubmitChanges()
                    MessageBox.Show("OD Site Update Sucessfully...")
                End If

            End If
        End If
    End Sub

    Private Sub txtTodayDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtEndDate.ValueChanged
        GvServiceEngineer_Bind()
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            For Each row As DataGridViewRow In GvEngineerAttedance.Rows
                If Not row.IsNewRow Then
                    'MessageBox.Show(row.Cells(0).Value.ToString & "," & row.Cells(1).Value.ToString)
                    linq_obj.SP_Insert_Update_Service_Attendance_Master(0, Convert.ToInt64(row.Cells("Fk_Address_ID").Value.ToString()),
                                                                            Convert.ToInt64(row.Cells("Fk_Engineer_ID").Value.ToString()),
                                                                            row.Cells("Status").Value.ToString(),
                                                                            row.Cells("ODType").Value.ToString(),
                                                                            row.Cells("NoOfDays").Value.ToString(),
                                                                            row.Cells("EndDate").Value.ToString(),
                                                                            row.Cells("Type").Value.ToString(),
                                                                            row.Cells("Days").Value.ToString(),
                                                                            row.Cells("SiteNo").Value.ToString(),
                                                                            row.Cells("EnqNo").Value.ToString(),
                                                                            row.Cells("PartyName").Value.ToString(),
                                                                            row.Cells("Station").Value.ToString(),
                                                                            row.Cells("Machine").Value.ToString(),
                                                                            row.Cells("Capacity").Value.ToString(),
                                                                            row.Cells("MachineStatus").Value.ToString(),
                                                                            row.Cells("MachineType").Value.ToString(),
                                                                            row.Cells("ODSiteStatus").Value.ToString(),
                                                                            row.Cells("Remark").Value.ToString(),
                                                                            row.Cells("Expense_Status").Value.ToString(),
                                                                            row.Cells("TodayDate").Value.ToString())
                    linq_obj.SubmitChanges()


                End If
            Next

            MessageBox.Show("Submit Sucessfully..")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GvEngineerAttedance_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles GvEngineerAttedance.EditingControlShowing
        If GvEngineerAttedance.CurrentCell.ColumnIndex = 5 AndAlso TypeOf e.Control Is TextBox Then
            Dim Status As String = GvEngineerAttedance.Columns(5).HeaderText
            If Status.Equals("Status") Then
                Dim autoText_status As TextBox = TryCast(e.Control, TextBox)
                If autoText_status IsNot Nothing Then
                    autoText_status.AutoCompleteMode = AutoCompleteMode.Suggest
                    autoText_status.AutoCompleteSource = AutoCompleteSource.CustomSource
                    Dim DataCollection_Status As New AutoCompleteStringCollection()
                    addItems_Status(DataCollection_Status)
                    autoText_status.AutoCompleteCustomSource = DataCollection_Status
                End If
            End If
        End If
        If GvEngineerAttedance.CurrentCell.ColumnIndex = 6 AndAlso TypeOf e.Control Is TextBox Then
            Dim ODType As String = GvEngineerAttedance.Columns(6).HeaderText
            If ODType.Equals("ODType") Then
                Dim autoText_status As TextBox = TryCast(e.Control, TextBox)
                If autoText_status IsNot Nothing Then
                    autoText_status.AutoCompleteMode = AutoCompleteMode.Suggest
                    autoText_status.AutoCompleteSource = AutoCompleteSource.CustomSource
                    Dim DataCollection_Status As New AutoCompleteStringCollection()
                    addItems_ODType(DataCollection_Status)
                    autoText_status.AutoCompleteCustomSource = DataCollection_Status
                End If
            End If
        End If
        If GvEngineerAttedance.CurrentCell.ColumnIndex = 17 AndAlso TypeOf e.Control Is TextBox Then
            Dim Type As String = GvEngineerAttedance.Columns("Type").HeaderText
            If Type.Equals("Type") Then
                Dim autoText As TextBox = TryCast(e.Control, TextBox)
                If autoText IsNot Nothing Then
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource
                    Dim DataCollection As New AutoCompleteStringCollection()
                    addItems_Type(DataCollection)
                    autoText.AutoCompleteCustomSource = DataCollection
                End If
            End If
        End If

        'dt.Columns.Add("Machine") '17
        'dt.Columns.Add("Capacity") '18
        'dt.Columns.Add("MachineStatus") '19
        'dt.Columns.Add("MachineType") '20
        'dt.Columns.Add("ODSiteStatus") '21
        'dt.Columns.Add("Remark") '22

        If GvEngineerAttedance.CurrentCell.ColumnIndex = 13 AndAlso TypeOf e.Control Is TextBox Then

            Dim Machine As String = GvEngineerAttedance.Columns("Machine").HeaderText
            Dim Fk_Address_ID As String = GvEngineerAttedance.CurrentRow.Cells(0).Value
            If Machine.Equals("Machine") Then
                Dim autoText As TextBox = TryCast(e.Control, TextBox)
                If autoText IsNot Nothing Then
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource
                    Dim DataCollection As New AutoCompleteStringCollection()
                    addItems_Machine(DataCollection, Fk_Address_ID)
                    autoText.AutoCompleteCustomSource = DataCollection
                End If
            End If
        End If

        'If GvEngineerAttedance.CurrentCell.ColumnIndex = 18 AndAlso TypeOf e.Control Is TextBox Then
        '    Dim Capacity As String = GvEngineerAttedance.Columns(18).HeaderText
        '    Dim Fk_Address_ID As String = GvEngineerAttedance.CurrentRow.Cells(0).Value
        '    If Capacity.Equals("Capacity") Then

        '        If GvEngineerAttedance.CurrentRow.Cells(17).Value <> "" Then

        '            Dim autoText As TextBox = TryCast(e.Control, TextBox)
        '            If autoText IsNot Nothing Then
        '                autoText.AutoCompleteMode = AutoCompleteMode.Suggest
        '                autoText.AutoCompleteSource = AutoCompleteSource.CustomSource
        '                Dim DataCollection As New AutoCompleteStringCollection()
        '                addItems_Machine_Capacity(DataCollection, Fk_Address_ID)
        '                autoText.AutoCompleteCustomSource = DataCollection
        '            End If
        '        Else

        '            MessageBox.Show("Select Machine Name")

        '        End If
        '    End If
        'End If
        If GvEngineerAttedance.CurrentCell.ColumnIndex = 19 AndAlso TypeOf e.Control Is TextBox Then
            Dim Type As String = GvEngineerAttedance.Columns(19).HeaderText
            If Type.Equals("MachineStatus") Then
                Dim autoText As TextBox = TryCast(e.Control, TextBox)
                If autoText IsNot Nothing Then
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource
                    Dim DataCollection As New AutoCompleteStringCollection()
                    addItems_MachineStatus(DataCollection)
                    autoText.AutoCompleteCustomSource = DataCollection
                End If
            End If
        End If
        If GvEngineerAttedance.CurrentCell.ColumnIndex = 20 AndAlso TypeOf e.Control Is TextBox Then
            Dim Type As String = GvEngineerAttedance.Columns(20).HeaderText
            If Type.Equals("MachineType") Then
                Dim autoText As TextBox = TryCast(e.Control, TextBox)
                If autoText IsNot Nothing Then
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource
                    Dim DataCollection As New AutoCompleteStringCollection()
                    addItems_MachineType(DataCollection)
                    autoText.AutoCompleteCustomSource = DataCollection
                End If
            End If
        End If
        If GvEngineerAttedance.CurrentCell.ColumnIndex = 21 AndAlso TypeOf e.Control Is TextBox Then
            Dim ODSiteStatus As String = GvEngineerAttedance.Columns(21).HeaderText
            If ODSiteStatus.Equals("ODSiteStatus") Then
                Dim autoText As TextBox = TryCast(e.Control, TextBox)
                If autoText IsNot Nothing Then
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource
                    Dim DataCollection As New AutoCompleteStringCollection()
                    addItems_ODSiteStatus(DataCollection)
                    autoText.AutoCompleteCustomSource = DataCollection
                End If
            End If
        End If
        If GvEngineerAttedance.CurrentCell.ColumnIndex = 24 AndAlso TypeOf e.Control Is TextBox Then
            Dim Expense_Status As String = GvEngineerAttedance.Columns(24).HeaderText
            If Expense_Status.Equals("Expense_Status") Then
                Dim autoText As TextBox = TryCast(e.Control, TextBox)
                If autoText IsNot Nothing Then
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource
                    Dim DataCollection As New AutoCompleteStringCollection()
                    addItems_Expense_Status(DataCollection)
                    autoText.AutoCompleteCustomSource = DataCollection
                End If
            End If
        End If
    End Sub
    Public Sub addItems_Status(ByVal col As AutoCompleteStringCollection)
        col.Add("ABSENT")
        col.Add("OFFICE")
        col.Add("OD")
        col.Add("OFFICE OD")
        col.Add("LEAVE")
        col.Add("SUNDAY")
        col.Add("HOLIDAY")
    End Sub

    Public Sub addItems_ODType(ByVal col As AutoCompleteStringCollection)
        col.Add("EC")
        col.Add("Service")
        col.Add("Visit")
        col.Add("Training")
        col.Add("O_EC_B")
        col.Add("O_EC_Y")
        col.Add("O_EC_G")
        col.Add("O_SERVICE_B")
        col.Add("O_SERVICE_Y")
        col.Add("O_SERVICE_G")
        col.Add("O_TRAINING_B")
        col.Add("O_TRAINING_Y")
        col.Add("O_TRAINING_G")
    End Sub
    Public Sub addItems_Type(ByVal col As AutoCompleteStringCollection)
        col.Add("UP")
        col.Add("DOWN")
        col.Add("WORK")
    End Sub
    Public Sub addItems_MachineStatus(ByVal col As AutoCompleteStringCollection)
        col.Add("PENDING")
        col.Add("DONE")
    End Sub
    Public Sub addItems_MachineType(ByVal col As AutoCompleteStringCollection)
        col.Add("EC")
        col.Add("Erection")
        col.Add("Commission")
        col.Add("Service")
    End Sub
    Public Sub addItems_ODSiteStatus(ByVal col As AutoCompleteStringCollection)
        col.Add("RUNNING")
        col.Add("DONE")
        col.Add("CANCEL")
    End Sub
    Public Sub addItems_Expense_Status(ByVal col As AutoCompleteStringCollection)
        col.Add("PENDING")
        col.Add("DONE")
    End Sub

    Public Sub addItems_Machine(ByVal col As AutoCompleteStringCollection, ByVal Fk_Address_ID As String)
        Dim dataItem = linq_obj.SP_Get_PackingDetail_New_List(Convert.ToInt64(Fk_Address_ID)).ToList()
        col.Clear()
        For Each item As SP_Get_PackingDetail_New_ListResult In dataItem
            col.Add(item.Item)
        Next

    End Sub

    Public Sub addItems_Machine_Capacity1(ByVal Fk_Address_ID As String)

    End Sub
    Private Sub rblAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AttStatus = ""
        GvServiceEngineer_Bind()
    End Sub

    Private Sub rblOffice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblOffice.CheckedChanged
        AttStatus = "OFFICE"
        GvServiceEngineer_Bind()

    End Sub

    Private Sub rblAbsent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblAbsent.CheckedChanged
        AttStatus = "ABSENT"
        GvServiceEngineer_Bind()
    End Sub

    Private Sub rblOD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblOD.CheckedChanged
        AttStatus = "OD"
        GvServiceEngineer_Bind()
    End Sub

    Private Sub rblAll_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblAll.CheckedChanged
        GvServiceEngineer_Bind()
    End Sub
    Private Sub rblOfficeOD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblOfficeOD.CheckedChanged
        AttStatus = "Office OD"
        GvServiceEngineer_Bind()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim count As Integer
            Dim app As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()

            ' creating new WorkBook within Excel application
            Dim workbook As Microsoft.Office.Interop.Excel._Workbook = app.Workbooks.Add(Type.Missing)

            ' creating new Excelsheet in workbookz
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
            For i As Integer = 1 To GvEngineerAttedance.Columns.Count
                worksheet.Cells(1, i) = GvEngineerAttedance.Columns(i - 1).HeaderText
            Next
            ' storing Each row and column value to excel sheet
            For i As Integer = 0 To GvEngineerAttedance.Rows.Count - 1
                For j As Integer = 0 To GvEngineerAttedance.Columns.Count - 1
                    'Dim range As Microsoft.Office.Interop.Excel.Range = CType(worksheet.Cells((i + 2), (j + 1)), Microsoft.Office.Interop.Excel.Range)
                    worksheet.Cells(i + 2, j + 1) = GvEngineerAttedance.Rows(i).Cells(j).Value.ToString()
                    'range.Interior.Color = System.Drawing.ColorTranslator.ToOle(GvEngineerAttedance.Rows(i).DefaultCellStyle.BackColor)
                    count += i + 2 + j + 1
                Next
            Next
            MessageBox.Show("Export to Excel Sucessfully...")

        Catch generatedExceptionName As Exception
            MessageBox.Show(generatedExceptionName.Message)
        End Try
    End Sub

    Private Sub rblSunday_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblSunday.CheckedChanged
        AttStatus = "sunday"
        GvServiceEngineer_Bind()
    End Sub

    Private Sub rblHoliday_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblHoliday.CheckedChanged
        AttStatus = "holiday"
        GvServiceEngineer_Bind()
    End Sub

    Private Sub rblLeave_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblLeave.CheckedChanged
        AttStatus = "leave"
        GvServiceEngineer_Bind()
    End Sub


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GvServiceEngineer_Bind()
    End Sub
End Class