

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient

Public Class OrderCategory_Master

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim msPic1 As New MemoryStream
    Dim msPic2 As New MemoryStream
    Dim PK_Order_Master_ID As Integer
    Dim PK_Order_Detail_ID As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        txtNo.Text = "1"
        GvCategory_Master_Bind()
    End Sub
    Public Sub GvCategory_Master_Bind()

        Dim Data = linq_obj.SP_Get_Order_Category_Master_List()
        GvOrderCategoryList.DataSource = Data
        GvOrderCategoryList.Columns(0).Visible = False
        GvOrderCategoryList.Columns(3).Visible = False
        GvOrderCategoryList.Columns(4).Visible = False
    End Sub


    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try

            'insert Category
            If (btnUpdate.Text.Trim() = "Submit") Then

                Dim data1 = linq_obj.SP_Insert_Update_Order_Category_Master(0, txtMainCategory.Text, txtModel.Text, txtGST.Text, txtPrice.Text, txtCapacity.Text)
                linq_obj.SubmitChanges()
                PK_Order_Master_ID = data1
                btnUpdate.Text = "Update"

            Else

                Dim data1 = linq_obj.SP_Insert_Update_Order_Category_Master(PK_Order_Master_ID, txtMainCategory.Text, txtModel.Text, txtGST.Text, txtPrice.Text, txtCapacity.Text)
                linq_obj.SubmitChanges()
            End If


            'insert category Detail
             
            If (BtnSave.Text = "Add") Then
                Dim CategoryDetail1 = linq_obj.SP_Insert_Update_Order_Category_Details(0, PK_Order_Master_ID, Convert.ToInt32(txtNo.Text), txtItemName.Text.Trim(), txtPhoto.Text.Trim(), txtPhoto1.Text.Trim(), txtRemarks.Text.Trim())
                linq_obj.SubmitChanges()
            Else
                Dim CategoryDetail1 = linq_obj.SP_Insert_Update_Order_Category_Details(PK_Order_Detail_ID, PK_Order_Master_ID, Convert.ToInt32(txtNo.Text), txtItemName.Text.Trim(), txtPhoto.Text.Trim(), txtPhoto1.Text.Trim(), txtRemarks.Text.Trim())
                linq_obj.SubmitChanges()

            End If
             
          
            MessageBox.Show("Submit Sucessfully...")
             
            BtnSave.Text = "Add"
            txtItemName.Text = ""
            txtPhoto.Text = ""
            GvCategory_Master_Bind()
            GvCategory_Detail_Bind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GvCategory_Detail_Bind()

        Dim CatDetail = linq_obj.SP_Get_Order_Category_Master_Detail_By_CategoryID(PK_Order_Master_ID).ToList()
        GvOrderCategoryDetail.DataSource = CatDetail
        GvOrderCategoryDetail.Columns(0).Visible = False
        GvOrderCategoryDetail.Columns(1).Visible = False
        GvOrderCategoryDetail.Columns(2).Width = 150
        txtNo.Text = GvOrderCategoryDetail.Rows.Count + 1



    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click

        txtItemName.Text = ""
        txtPhoto.Text = ""
        txtNo.Text = GvOrderCategoryDetail.Rows.Count + 1



    End Sub
    Public Sub Clean()
        txtMainCategory.Text = ""
        txtEntryNo.Text = ""

        txtModel.Text = ""
        txtCapacity.Text = ""
        txtPrice.Text = ""
        txtGST.Text = ""

        PK_Order_Master_ID = 0
        txtNo.Text = "1"
        txtItemName.Text = ""
        txtPhoto.Text = ""
        BtnSave.Text = "Add"
        btnUpdate.Text = "Submit"

        GvOrderCategoryDetail.DataSource = Nothing



    End Sub

    Private Sub GvOrderCategoryList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvOrderCategoryList.DoubleClick
        PK_Order_Master_ID = GvOrderCategoryList.SelectedCells(0).Value
        Diplay_Data()
    End Sub

    Public Sub Diplay_Data()

        Dim CategoryMaster1 = linq_obj.SP_Get_Order_Category_Master_List().Where(Function(p) p.Pk_OrderCategory_ID = PK_Order_Master_ID).ToList()

        For Each item As SP_Get_Order_Category_Master_ListResult In CategoryMaster1
            txtEntryNo.Text = item.Pk_OrderCategory_ID

            txtMainCategory.Text = item.MainCategory
            txtModel.Text = item.Model
            txtGST.Text = item.GST

            txtPrice.Text = item.Price
            txtCapacity.Text = item.Capacity
            btnUpdate.Text = "Update"

            ''Order CAtegory Detail 

            GvCategory_Detail_Bind()


        Next


    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto.Text = imgSrc
    End Sub

    Private Sub GvOrderCategoryDetail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvOrderCategoryDetail.DoubleClick

        BtnSave.Text = "Update"
        PK_Order_Detail_ID = GvOrderCategoryDetail.SelectedCells(0).Value
        txtNo.Text = GvOrderCategoryDetail.SelectedCells(2).Value
        txtItemName.Text = GvOrderCategoryDetail.SelectedCells(3).Value
        txtPhoto.Text = GvOrderCategoryDetail.SelectedCells(4).Value
        txtPhoto1.Text = GvOrderCategoryDetail.SelectedCells(5).Value

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Clean()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim data = linq_obj.SP_Insert_Update_Order_Category_Master(PK_Order_Master_ID, txtMainCategory.Text, txtModel.Text, txtGST.Text.Trim(), txtPrice.Text, txtCapacity.Text)
        linq_obj.SubmitChanges()
        GvCategory_Master_Bind()
        MessageBox.Show("Update Sucessfully..")

        Clean()
    End Sub

    Private Sub btnAddNewCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewCat.Click
        Clean()
    End Sub

    Private Sub btnBrowse1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse1.Click
        Dim openFileDialog1 As New OpenFileDialog
        Dim imgSrc As String
        Dim imgPath As String
        openFileDialog1.ShowDialog()
        imgSrc = openFileDialog1.FileName.ToString()
        imgPath = (Path.GetDirectoryName(Application.ExecutablePath) & "\Images\") + openFileDialog1.SafeFileName
        txtPhoto1.Text = imgSrc
    End Sub
End Class