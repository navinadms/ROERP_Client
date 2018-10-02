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
Imports System.IO

Public Class EngineeringMaster

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Pk_Engineer_ID As Integer
    Dim ProfilePhoto As String
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        GvServiceEngineer_Bind()
    End Sub

    Public Sub GvServiceEngineer_Bind()

        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")
        Dim data = linq_obj.SP_Get_Service_Engineer_Master_List().ToList()
        For Each item As SP_Get_Service_Engineer_Master_ListResult In data
            dt.Rows.Add(item.Pk_Engineer_ID, item.Engg_Code, item.Name)

        Next
        GvServiceEngineer.DataSource = dt

        For index = 0 To GvServiceEngineer.RowCount - 1
            If (GvServiceEngineer.Rows(index).Cells(1).Value.ToString().Contains("A-")) Then
                GvServiceEngineer.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Black
                GvServiceEngineer.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.White
            End If
            If (GvServiceEngineer.Rows(index).Cells(1).Value.ToString().Contains("B-")) Then
                GvServiceEngineer.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                GvServiceEngineer.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.Black

            End If
            If (GvServiceEngineer.Rows(index).Cells(1).Value.ToString().Contains("C-")) Then
                GvServiceEngineer.Rows(index).DefaultCellStyle.BackColor = System.Drawing.Color.Green
                GvServiceEngineer.Rows(index).DefaultCellStyle.ForeColor = System.Drawing.Color.White

            End If
        Next
        GvServiceEngineer.Columns(0).Visible = False
        GvServiceEngineer.Refresh()

    End Sub
    Public Sub GvEngineerRepor_List()

        Dim dt As New DataTable

        dt.Columns.Add("FK_Engineer_ID")
        dt.Columns.Add("FK_Machine_ID")
        dt.Columns.Add("Machine")
        dt.Columns.Add("Service")
        dt.Columns.Add("EC")
        dt.Columns.Add("Erection")
        dt.Columns.Add("Commission")
        dt.Columns.Add("Total")
        Dim data = linq_obj.SP_Get_Engineer_Summary_List(Pk_Engineer_ID).ToList()
        Dim total As Integer
        Dim Service As Integer
        Dim EC As Integer
        Dim Erection As Integer
        Dim Commission As Integer

        total = 0
        Service = 0
        EC = 0
        Erection = 0
        Commission = 0




        For Each item As SP_Get_Engineer_Summary_ListResult In data
            total = 0

            Service = 0
            EC = 0
            Erection = 0
            Commission = 0

            If (Convert.ToString(item.Service) = "") Then
                Service = 0
            Else
                Service = Convert.ToInt32(item.Service)
            End If

            If (Convert.ToString(item.EC) = "") Then
                EC = 0
            Else
                EC = Convert.ToInt32(item.EC)
            End If


            If (Convert.ToString(item.Erection) = "") Then
                Erection = 0
            Else
                Erection = Convert.ToInt32(item.Erection)
            End If

            If (Convert.ToString(item.Commission) = "") Then
                Commission = 0
            Else
                Commission = Convert.ToInt32(item.Commission)
            End If

           

            total = Convert.ToInt32(Service) + Convert.ToInt32(EC) + Convert.ToInt32(Erection) + Convert.ToInt32(Commission)



            dt.Rows.Add(Pk_Engineer_ID, item.Pk_Packing_Item_Master_ID, item.Packing_Item, Service, EC, Erection, Commission, total)
        Next

        GvEngineerReport.DataSource = dt

        GvEngineerReport.Columns(0).Visible = False
        GvEngineerReport.Columns(1).Visible = False

    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Try
            If btnSubmit.Text.Trim() = "Submit" Then
                linq_obj.SP_insert_Update_Service_Engineer_Master(0, txtEngCode.Text, txtName.Text, txtAddress.Text, txtCity.Text, txtState.Text, txtPincode.Text, txtMobileNo.Text, txtResContact.Text, txtEmail.Text, "Yes", ProfilePhoto, "", txtJoiningDate.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Submit Sucessfully....")

            Else
                linq_obj.SP_insert_Update_Service_Engineer_Master(Pk_Engineer_ID, txtEngCode.Text, txtName.Text, txtAddress.Text, txtCity.Text, txtState.Text, txtPincode.Text, txtMobileNo.Text, txtResContact.Text, txtEmail.Text, "Yes", ProfilePhoto, "", txtJoiningDate.Text)
                linq_obj.SubmitChanges()
                MessageBox.Show("Update Sucessfully...")
            End If
            Clear()
            GvServiceEngineer_Bind()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Clear()
        btnSubmit.Text = "Submit"

        txtEngCode.Text = ""
        txtName.Text = ""
        txtAddress.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtPincode.Text = ""
        txtMobileNo.Text = ""
        txtResContact.Text = ""
        txtEmail.Text = ""
        PicPhoto.ImageLocation = ""

    End Sub

    Private Sub GvServiceEngineer_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvServiceEngineer.DoubleClick
        Pk_Engineer_ID = Me.GvServiceEngineer.SelectedCells(0).Value
        btnSubmit.Text = "Update"
        Dim data = linq_obj.SP_Get_Service_Engineer_Master_List().ToList().Where(Function(p) p.Pk_Engineer_ID = Pk_Engineer_ID).ToList()

        For Each item As SP_Get_Service_Engineer_Master_ListResult In data
            txtEngCode.Text = item.Engg_Code
            txtName.Text = item.Name
            txtAddress.Text = item.Address
            txtCity.Text = item.City
            txtState.Text = item.State
            txtPincode.Text = item.Pincode
            txtMobileNo.Text = item.MobileNo
            txtResContact.Text = item.ResContact
            txtEmail.Text = item.EmailID

            ProfilePhoto = item.Photo
            PicPhoto.ImageLocation = item.Photo
            PicPhoto.SizeMode = PictureBoxSizeMode.StretchImage

        Next
        GvEngineerRepor_List()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Clear()
    End Sub

    Private Sub btnPhotoUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhotoUpload.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Photo\") + openFileDialog1.SafeFileName
        ProfilePhoto = imgSrc
        PicPhoto.ImageLocation = imgSrc
        PicPhoto.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        If txtEngIDSearch.Text.Trim() <> "" Then
            Dim data = linq_obj.SP_Get_Service_Engineer_Master_List().ToList().Where(Function(p) p.Engg_Code = txtEngIDSearch.Text.Trim()).ToList()

            For Each item As SP_Get_Service_Engineer_Master_ListResult In data
                dt.Rows.Add(item.Pk_Engineer_ID, item.Engg_Code, item.Name)

            Next
        Else
            Dim data = linq_obj.SP_Get_Service_Engineer_Master_List().ToList().Where(Function(p) p.Name.ToLower() = txtNameSearch.Text.ToLower().Trim()).ToList()

            For Each item As SP_Get_Service_Engineer_Master_ListResult In data
                dt.Rows.Add(item.Pk_Engineer_ID, item.Engg_Code, item.Name)

            Next
        End If


        GvServiceEngineer.DataSource = dt
        GvServiceEngineer.Columns(0).Visible = False

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        GvServiceEngineer_Bind()
        txtEngIDSearch.Text = ""
        txtNameSearch.Text = ""


    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
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
            worksheet.Name = txtName.Text


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

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnUpdateSumm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateSumm.Click


        Try
            For Each row As DataGridViewRow In GvEngineerReport.Rows
                If Not row.IsNewRow Then
                    'MessageBox.Show(row.Cells(0).Value.ToString & "," & row.Cells(1).Value.ToString)



                    linq_obj.SP_Insert_Update_Service_Engineer_Summary(Convert.ToInt64(row.Cells("FK_Engineer_ID").Value),
                                                                            Convert.ToInt64(row.Cells("FK_Machine_ID").Value),
                                                                            Convert.ToInt32(row.Cells("Service").Value),
                                                                            Convert.ToInt32(row.Cells("EC").Value),
                                                                            Convert.ToInt32(row.Cells("Erection").Value),
                                                                            Convert.ToInt32(row.Cells("Commission").Value),
                                                                            Convert.ToInt32(row.Cells("Total").Value))


                    linq_obj.SubmitChanges()


                End If
            Next

            MessageBox.Show("Update Sucessfully..")

        Catch ex As Exception

            MessageBox.Show("Some thing wrong data")
        End Try
    End Sub

     
End Class