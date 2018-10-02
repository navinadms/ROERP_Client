
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.Common


Public Class CategoryMaster
    Private con1 As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader
    Private da As SqlDataAdapter
    Private ds As DataSet
   
    Public Edi_Cat_ID As Int32
    Shared LanguageId As Int32
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim tran As DbTransaction = Nothing


    Public Sub New()
        InitializeComponent()
        con1 = Class1.con
        LanguageId = 1
        RdbEnglish.Checked = True
        Category_Id()
        AutoDesignation()
        GvCategorySearch_Bind(LanguageId)

        getPageRight()

    End Sub


    Public Sub getPageRight()
        Try
            Dim dv As New DataTable
            Dim dataView As DataView

            Dim Class1 As New Class1
            Dim RowCount As Integer
            Dim statusCheck As Boolean = False

            Dim strName As String = ""

            dataView = Class1.global.UserPermissionDataset.Tables(0).DefaultView
            dataView.RowFilter = "([DetailName] like 'Category')"

            If (dataView.Count > 0) Then

                dv = dataView.ToTable()

                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            BtnSave.Enabled = True
                            btnAddNew.Enabled = True
                        Else
                            BtnSave.Enabled = False
                            btnAddNew.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsUpdate") = True) Then
                            btnUpdate.Enabled = True
                        Else
                            btnUpdate.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsDelete") = True) Then
                            btnDeleteSub.Enabled = True
                        Else
                            btnDeleteSub.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsPrint") = True) Then

                        Else


                        End If

                    Next
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub AutoDesignation()
        Dim desg As String
        desg = "select * from Category_Master"
        da = New SqlDataAdapter(desg, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtCategory.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())
            txtMainCategory.AutoCompleteCustomSource.Add(dr1.Item("MainCategory").ToString())
        Next
    End Sub
    Public Sub Category_Id()
        Dim cId As String
        Try
            con1.Open()
            cId = "select Max(Pk_CategoryID) as MaxId from Category_Master"
            cmd = New SqlCommand(cId, con1)
            dr = cmd.ExecuteReader()
            If (dr.HasRows) Then
                dr.Read()
                If (dr("MaxId").ToString() <> "") Then
                    txtEntryNo.Text = (Convert.ToInt32(dr("MaxId").ToString()) + 1).ToString()
                Else
                    txtEntryNo.Text = "1"
                End If
            End If
        Catch ex As Exception

        End Try
        con1.Close()
    End Sub
    Public Sub Category_Sub_Id()
        Dim SNo As String
        Try
            con1.Open()
            SNo = "select Max(SNo) as MaxId from Category_Master where Capacity='" + txtCapacity.Text + "' and MainCategory ='" + txtMainCategory.Text + "'"
            cmd = New SqlCommand(SNo, con1)
            dr = cmd.ExecuteReader()
            If (dr.HasRows) Then
                dr.Read()
                If (dr("MaxId").ToString() <> "") Then

                    txtNo.Text = (Convert.ToInt32(dr("MaxId").ToString()) + 1).ToString()
                Else
                    txtNo.Text = "1"
                End If
            End If
        Catch ex As Exception

        End Try
        con1.Close()
    End Sub

    Public Sub GvCategorySearch_Bind(ByVal Language As Integer)
        Dim cc As String
        cc = "SELECT  DISTINCT MainCategory,Capacity,Price  FROM Category_Master where LanguageId = " & LanguageId & ""
        da = New SqlDataAdapter(cc, con1)
        ds = New DataSet()
        da.Fill(ds)
        GvCategorySearch.DataSource = ds.Tables(0)
        da.Dispose()
        ds.Dispose()

    End Sub
    Public Sub GvCategory_Bind()
        Dim cc As String
        cc = "select Pk_CategoryID as ID,Category,Capacity,Price,Photo1,Photo2,Photo3 from Category_Master where Capacity='" + txtCapacity.Text + "' and MainCategory ='" + txtMainCategory.Text + "' and LanguageId = " & LanguageId & ""
        da = New SqlDataAdapter(cc, con1)
        ds = New DataSet()
        da.Fill(ds)
        GvCategory.DataSource = ds.Tables(0)
        da.Dispose()
        ds.Dispose()
    End Sub

    Private Sub btnPhoto1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto1.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto1.Text = imgSrc

    End Sub
    Private Sub btnPhoto2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto2.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto2.Text = imgSrc

    End Sub
    Private Sub btnPhoto3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto3.Click

        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto3.Text = imgSrc


    End Sub
    Private Sub btnPhoto4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto4.Click

        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto4.Text = imgSrc


    End Sub


    Public Sub Validation_Text()
        txtMainCategory.BackColor = System.Drawing.Color.White
        txtMainCategory.ForeColor = System.Drawing.Color.Black

        txtCategory.BackColor = System.Drawing.Color.White
        txtCategory.ForeColor = System.Drawing.Color.Black

        txtCapacity.BackColor = System.Drawing.Color.White
        txtCapacity.ForeColor = System.Drawing.Color.Black

        txtPrice.BackColor = System.Drawing.Color.White
        txtPrice.ForeColor = System.Drawing.Color.Black

        txtPhoto1.BackColor = System.Drawing.Color.White
        txtPhoto1.ForeColor = System.Drawing.Color.Black

        If (txtMainCategory.Text.Trim() = "") Then
            txtMainCategory.BackColor = System.Drawing.Color.Red

            Dim exc As New Exception("MainCategory Can Not Be Blank")
            txtMainCategory.ForeColor = System.Drawing.Color.White
            txtMainCategory.Focus()
            Throw exc
        End If

        If (txtCategory.Text.Trim() = "") Then
            txtCategory.BackColor = System.Drawing.Color.Red

            Dim exc As New Exception("Category Can Not Be Blank")
            txtCategory.ForeColor = System.Drawing.Color.White
            txtCategory.Focus()
            Throw exc
        End If

        If (txtCapacity.Text.Trim() = "") Then
            txtCapacity.BackColor = System.Drawing.Color.Red

            Dim exc As New Exception("Capacity Can Not Be Blank")
            txtCapacity.ForeColor = System.Drawing.Color.White
            txtCapacity.Focus()
            Throw exc
        End If

        If (txtPrice.Text.Trim() = "") Then
            txtPrice.BackColor = System.Drawing.Color.Red
            Dim exc As New Exception("Price Can not Be Blank")
            txtPrice.ForeColor = System.Drawing.Color.White
            txtPrice.Focus()
            Throw exc
        End If



    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim insert_Category As String

        Try
            Validation_Text()
            con1.Close()
            con1.Open()
            insert_Category = "Insert into Category_Master(MainCategory,Category,SNo,Capacity,Price,Photo1,Photo2,Photo3,Photo4,Photo5,Photo6,Photo7,Photo8,Photo9,Photo10,Photo11,Photo12,Photo13,Photo14,Photo15,Photo16,Photo17,Photo18,Photo19,Photo20,Photo21,Photo22,Photo23,Photo24,LanguageId) values('" + txtMainCategory.Text + "','" + txtCategory.Text + "'," & txtNo.Text & ",'" + txtCapacity.Text + "','" + txtPrice.Text + "','" + txtPhoto1.Text.Trim() + "','" + txtPhoto2.Text.Trim() + "','" + txtPhoto3.Text.Trim() + "','" + txtPhoto4.Text.Trim() + "','" + txtPhoto5.Text.Trim() + "','" + txtPhoto6.Text.Trim() + "','" + txtPhoto7.Text.Trim() + "','" + txtPhoto8.Text.Trim() + "','" + txtPhoto9.Text.Trim() + "','" + txtPhoto10.Text.Trim() + "','" + txtPhoto11.Text.Trim() + "','" + txtPhoto12.Text.Trim() + "','" + txtPhoto13.Text.Trim() + "','" + txtPhoto14.Text.Trim() + "','" + txtPhoto15.Text.Trim() + "','" + txtPhoto16.Text.Trim() + "','" + txtPhoto17.Text.Trim() + "','" + txtPhoto18.Text.Trim() + "','" + txtPhoto19.Text.Trim() + "','" + txtPhoto20.Text.Trim() + "','" + txtPhoto21.Text.Trim() + "','" + txtPhoto22.Text.Trim() + "','" + txtPhoto23.Text.Trim() + "','" + txtPhoto24.Text.Trim() + "'," & LanguageId & ")"
            cmd = New SqlCommand(insert_Category, con1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con1.Close()

            SetClean()
            MessageBox.Show("Add Category Successfull..")
        Catch ex As Exception

        End Try
        GvCategory_Bind()
        Category_Id()
        Category_Sub_Id()

    End Sub
    Public Sub SetClean()
        txtPrice.Text = ""
        txtPhoto1.Text = ""
        txtPhoto2.Text = ""
        txtPhoto3.Text = ""
        txtPhoto4.Text = ""
        txtPhoto5.Text = ""
        txtPhoto6.Text = ""
        txtPhoto7.Text = ""
        txtPhoto8.Text = ""
        txtPhoto9.Text = ""
        txtPhoto10.Text = ""
        txtPhoto11.Text = ""
        txtPhoto12.Text = ""
        txtPhoto13.Text = ""
        txtPhoto14.Text = ""
        txtPhoto15.Text = ""
        txtPhoto16.Text = ""
        txtPhoto17.Text = ""
        txtPhoto18.Text = ""
        txtPhoto19.Text = ""
        txtPhoto20.Text = ""
        txtPhoto21.Text = ""
        txtPhoto22.Text = ""
        txtPhoto23.Text = ""
        txtPhoto24.Text = ""

        txtCategory.Text = ""
       

    End Sub

    Private Sub txtCapacity_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCapacity.Leave
        GvCategory_Bind()
        Category_Sub_Id()
    End Sub

    Private Sub GvCategorySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCategorySearch.Click
        Try
            Dim cap As String = Me.GvCategorySearch.SelectedCells(1).Value
            txtCapacity.Text = cap
            txtMainCategory.Text = Me.GvCategorySearch.SelectedCells(0).Value
            txtCapacity_Leave(Nothing, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GvCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCategory.Click
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Try
            Validation_Text()
            con1.Close()
            con1.Open()
            Dim category_update = "update Category_Master set MainCategory='" + txtMainCategory.Text.Trim() + "',Category='" + txtCategory.Text.Trim() + "',Capacity='" + txtCapacity.Text.Trim() + "',Price='" + txtPrice.Text.Trim() + "',Photo1='" + txtPhoto1.Text.Trim() + "',Photo2='" + txtPhoto2.Text.Trim() + "',Photo3='" + txtPhoto3.Text.Trim() + "' ,Photo4='" + txtPhoto4.Text.Trim() + "',Photo5='" + txtPhoto5.Text.Trim() + "',Photo6='" + txtPhoto6.Text.Trim() + "',Photo7='" + txtPhoto7.Text.Trim() + "',Photo8='" + txtPhoto8.Text.Trim() + "',Photo9='" + txtPhoto9.Text.Trim() + "',Photo10='" + txtPhoto10.Text.Trim() + "',Photo11='" + txtPhoto11.Text.Trim() + "',Photo12='" + txtPhoto12.Text.Trim() + "',Photo13='" + txtPhoto13.Text.Trim() + "',Photo14='" + txtPhoto14.Text.Trim() + "',Photo15='" + txtPhoto15.Text.Trim() + "',Photo16='" + txtPhoto16.Text.Trim() + "',Photo17='" + txtPhoto17.Text.Trim() + "',Photo18='" + txtPhoto18.Text.Trim() + "',Photo19='" + txtPhoto19.Text.Trim() + "' , Photo20='" + txtPhoto20.Text.Trim() + "', Photo21='" + txtPhoto21.Text.Trim() + "', Photo22='" + txtPhoto22.Text.Trim() + "', Photo23='" + txtPhoto23.Text.Trim() + "', Photo24='" + txtPhoto24.Text.Trim() + "' where Pk_CategoryID=" & Edi_Cat_ID & ""
            cmd = New SqlCommand(category_update, con1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con1.Close()
            SetClean()
            MessageBox.Show(" Update Successfull...")

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())


        End Try
        GvCategory_Bind()


    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        SetClean()
        txtCapacity.Text = ""
        txtNo.Text = ""
        txtMainCategory.Text = ""
        btnUpdate.Visible = False
        BtnSave.Visible = True

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cc As String

        If txtsearch.Text.Trim() <> "" Then



            cc = ""
            If RblMainCategory.Checked = True Then
                cc = "SELECT  DISTINCT MainCategory,Capacity,Price  FROM Category_Master where MainCategory='" + txtsearch.Text.Trim() + "'"
                da = New SqlDataAdapter(cc, con1)
                ds = New DataSet()
                da.Fill(ds)
                GvCategorySearch.DataSource = ds.Tables(0)
                GvCategorySearch.ReadOnly = True
                da.Dispose()
                ds.Dispose()
                txtTotalRecord.Text = GvCategorySearch.Rows.Count.ToString()
            End If

            If RblCapacity.Checked = True Then
                cc = "SELECT  DISTINCT MainCategory,Capacity,Price  FROM Category_Master where Capacity='" + txtsearch.Text.Trim() + "'"
                da = New SqlDataAdapter(cc, con1)
                ds = New DataSet()
                da.Fill(ds)
                GvCategorySearch.DataSource = ds.Tables(0)
                GvCategorySearch.ReadOnly = True
                da.Dispose()
                ds.Dispose()
                txtTotalRecord.Text = GvCategorySearch.Rows.Count.ToString()
            End If

            If RblPrice.Checked = True Then
                cc = "SELECT  DISTINCT MainCategory,Capacity,Price  FROM Category_Master where Price='" + txtsearch.Text.Trim() + "'"
                da = New SqlDataAdapter(cc, con1)
                ds = New DataSet()
                da.Fill(ds)
                GvCategorySearch.DataSource = ds.Tables(0)

                GvCategorySearch.ReadOnly = True

                da.Dispose()
                ds.Dispose()

                txtTotalRecord.Text = GvCategorySearch.Rows.Count.ToString()
            End If
        Else
            GvCategorySearch_Bind(1)


        End If


    End Sub
    Public Sub BindOnLanguageName()
        If RdbEnglish.Checked Then
            LanguageId = 1
            Class1.global.LanguageId = LanguageId
            GvCategorySearch_Bind(LanguageId)
            GvCategory_Bind()
        ElseIf RdbGujarati.Checked Then
            LanguageId = 2
            Class1.global.LanguageId = LanguageId
            GvCategorySearch_Bind(LanguageId)
            GvCategory_Bind()
        ElseIf RdbHindi.Checked Then
            LanguageId = 3
            Class1.global.LanguageId = LanguageId
            GvCategorySearch_Bind(LanguageId)
            GvCategory_Bind()
        ElseIf RdbMarathi.Checked Then
            LanguageId = 4
            Class1.global.LanguageId = LanguageId
            GvCategorySearch_Bind(LanguageId)
            GvCategory_Bind()
        ElseIf RdbTamil.Checked Then
            LanguageId = 5
            Class1.global.LanguageId = LanguageId
            GvCategorySearch_Bind(LanguageId)
            GvCategory_Bind()
        ElseIf RdbTelugu.Checked Then
            LanguageId = 6
            Class1.global.LanguageId = LanguageId
            GvCategorySearch_Bind(LanguageId)
            GvCategory_Bind()
        End If
    End Sub

    Private Sub RdbEnglish_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbEnglish.CheckedChanged
        BindOnLanguageName()
    End Sub

    Private Sub RdbGujarati_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbGujarati.CheckedChanged
        BindOnLanguageName()
    End Sub

    Private Sub RdbHindi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbHindi.CheckedChanged
        BindOnLanguageName()
    End Sub

    Private Sub RdbMarathi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbMarathi.CheckedChanged
        BindOnLanguageName()
    End Sub

    Private Sub RdbTamil_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbTamil.CheckedChanged
        BindOnLanguageName()
    End Sub

    Private Sub RdbTelugu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbTelugu.CheckedChanged
        BindOnLanguageName()
    End Sub

    Private Sub GvCategorySearch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvCategorySearch.CellContentClick

    End Sub

    Private Sub txtMainCategory_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMainCategory.Leave
        If txtMainCategory.Text.Trim() = "System 3" Then

        End If
    End Sub

    Private Sub btnDeleteSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSub.Click
        Try
            '*
            ' * display a confirmation message
            '                 

            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete Category?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then
                If linq_obj.Connection.State = System.Data.ConnectionState.Closed Then
                    linq_obj.Connection.Open()
                End If
                tran = linq_obj.Connection.BeginTransaction()
                linq_obj.Transaction = tran
                Dim cntSelect As Integer = GvCategory.SelectedRows.Count
                For Each dr As DataGridViewRow In GvCategory.SelectedRows
                    Dim resDelete As Integer = linq_obj.SP_Delete_Category(Convert.ToInt32(dr.Cells(0).Value))
                    linq_obj.SubmitChanges()
                    GvCategory.Rows.RemoveAt(dr.Index)

                Next

                tran.Commit()

            End If
        Catch ex As Exception
            tran.Rollback()
            If linq_obj.Connection IsNot Nothing AndAlso linq_obj.Connection.State = System.Data.ConnectionState.Open Then
                linq_obj.Connection.Close()
            End If
            linq_obj.Connection.Close()
            MessageBox.Show(ex.ToString())
        End Try


    End Sub

    Private Sub GvCategory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCategory.DoubleClick

        BtnSave.Visible = False
        btnUpdate.Visible = True

        Edi_Cat_ID = Convert.ToInt32(Me.GvCategory.SelectedCells(0).Value)
        Dim str As String
        Try
            con1.Close()
            con1.Open()
            str = "select * from Category_Master where Pk_CategoryID=" & Edi_Cat_ID & ""
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            txtMainCategory.Text = dr("MainCategory").ToString()
            txtCategory.Text = dr("Category").ToString()
            txtNo.Text = dr("SNo").ToString()
            txtCapacity.Text = dr("Capacity").ToString()
            txtPrice.Text = dr("Price").ToString()
            txtPhoto1.Text = dr("Photo1").ToString()
            txtPhoto2.Text = dr("Photo2").ToString()
            txtPhoto3.Text = dr("Photo3").ToString()
            txtPhoto4.Text = dr("Photo4").ToString()
            txtPhoto5.Text = dr("Photo5").ToString()
            txtPhoto6.Text = dr("Photo6").ToString()
            txtPhoto7.Text = dr("Photo7").ToString()
            txtPhoto8.Text = dr("Photo8").ToString()
            txtPhoto9.Text = dr("Photo9").ToString()
            txtPhoto10.Text = dr("Photo10").ToString()
            txtPhoto11.Text = dr("Photo11").ToString()
            txtPhoto12.Text = dr("Photo12").ToString()
            txtPhoto13.Text = dr("Photo13").ToString()
            txtPhoto14.Text = dr("Photo14").ToString()
            txtPhoto15.Text = dr("Photo15").ToString()
            txtPhoto16.Text = dr("Photo16").ToString()
            txtPhoto17.Text = dr("Photo17").ToString()
            txtPhoto18.Text = dr("Photo18").ToString()
            txtPhoto19.Text = dr("Photo19").ToString()
            txtPhoto20.Text = dr("Photo20").ToString()
            txtPhoto21.Text = dr("Photo21").ToString()
            txtPhoto22.Text = dr("Photo22").ToString()
            txtPhoto23.Text = dr("Photo23").ToString()
            txtPhoto24.Text = dr("Photo24").ToString()

            cmd.Dispose()
            dr.Dispose()
            con1.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GvCategorySearch_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles GvCategorySearch.PreviewKeyDown


    End Sub

    Private Sub CategoryMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        For Each control As Control In Me.Controls
            ' The textbox control.
            Dim parentControl As New Control


            If (control.GetType() Is GetType(GroupBox)) Then
                Dim grpBox As GroupBox = TryCast(control, GroupBox)
                parentControl = grpBox
            ElseIf (control.GetType() Is GetType(TabControl)) Then
                Dim TC As TabControl = TryCast(control, TabControl)
                parentControl = TC
            Else
                Try
                    parentControl = control
                Catch ex As Exception

                End Try

            End If

            For Each subcontrol As Control In parentControl.Controls
                If (subcontrol.GetType() Is GetType(TextBox)) Then
                    Dim textBox As TextBox = TryCast(subcontrol, TextBox)

                    ' If not null, set the handler.
                    If textBox IsNot Nothing Then
                        AddHandler textBox.Enter, AddressOf TextBox_Enter
                        AddHandler textBox.Leave, AddressOf TextBox_Leave
                    End If
                End If

                ' Set the handler.
            Next

            ' Set the handler.
        Next
    End Sub
    Private Sub TextBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)

        txt.BackColor = Color.LightYellow
    End Sub
    Private Sub TextBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)

        txt.BackColor = Color.White
    End Sub

   
    Private Sub btnPhoto5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto5.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto5.Text = imgSrc

    End Sub

    Private Sub btnPhoto6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto6.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto6.Text = imgSrc

    End Sub

    Private Sub btnPhoto7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto7.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto7.Text = imgSrc

    End Sub

    Private Sub btnPhoto8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto8.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto8.Text = imgSrc

    End Sub

    Private Sub btnPhoto9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto9.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto9.Text = imgSrc

    End Sub

    Private Sub btnPhoto10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto10.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto10.Text = imgSrc

    End Sub

    Private Sub btnPhoto11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto11.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto11.Text = imgSrc

    End Sub

    Private Sub btnPhoto12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto12.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto12.Text = imgSrc

    End Sub

    Private Sub btnPhoto13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto13.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto13.Text = imgSrc

    End Sub

    Private Sub btnPhoto14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto14.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto14.Text = imgSrc

    End Sub

    Private Sub btnPhoto15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto15.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto15.Text = imgSrc

    End Sub

    Private Sub btnPhoto16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto16.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto16.Text = imgSrc
    End Sub

    Private Sub btnPhoto17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto17.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto17.Text = imgSrc
    End Sub

    Private Sub btnPhoto18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto18.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto18.Text = imgSrc
    End Sub

    Private Sub btnPhoto19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto19.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto19.Text = imgSrc
    End Sub

    Private Sub btnPhoto20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto20.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto20.Text = imgSrc
    End Sub

    Private Sub btnPhoto21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto21.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto21.Text = imgSrc
    End Sub

    Private Sub btnPhoto22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto22.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto22.Text = imgSrc
    End Sub

    Private Sub btnPhoto23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto23.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto23.Text = imgSrc
    End Sub

    Private Sub btnPhoto24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPhoto24.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto24.Text = imgSrc
    End Sub
End Class
