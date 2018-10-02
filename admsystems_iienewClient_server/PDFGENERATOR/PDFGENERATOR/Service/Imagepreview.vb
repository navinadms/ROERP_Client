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
Public Class Imagepreview
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim Pk_Engineer_ID As Integer
    Dim ProfilePhoto As String
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Text = Class1.global.imageName

        PictureBox1.ImageLocation = Class1.global.imagepath
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage

    End Sub
End Class