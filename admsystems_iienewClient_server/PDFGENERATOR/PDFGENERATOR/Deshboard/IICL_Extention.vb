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
Imports Microsoft.Win32
Imports System.Net
Imports System.Drawing

Public Class IICL_Extention
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Public Sub New()
        ''txtQoutType.Font.Name = "Sa"
        InitializeComponent()
        PictureBox1.ImageLocation = "\\192.168.1.102\\ROERP\\USER\\RK\\IICL_Expention.jpg"
        'PictureBox1.ImageLocation = "D:\\ROERP\\USER\\RK\\IICL_Expention.jpg"
        'PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Dim I_ms2 As New MemoryStream
        If Not PictureBox1.Image Is Nothing Then
            PictureBox1.Image.Save(I_ms2, PictureBox1.Image.RawFormat)
            ' I_by2 = I_ms2.GetBuffer()
        End If
        'PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Event_Log()

    End Sub
    Public Sub Event_Log()

        btn_color()
        btn_ForColor()
        Dim UserLog = linq_obj.SP_Get_Clock_In_Out_Live_Deshboard().ToList()
        For Each item As SP_Get_Clock_In_Out_Live_DeshboardResult In UserLog
            'ToolTip1.SetToolTip(btn176, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            If item.System_IP.Trim() = "2" Then
                btn2.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn2, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "111" Then
                btn111.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn111, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "112" Then
                btn112.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn112, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "114" Then
                btn114.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn114, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "115" Then
                btn115.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn115, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "116" Then
                btn116.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn116, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "117" Then
                btn117.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn117, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "118" Then
                btn118.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn118, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If

            If item.System_IP.Trim() = "119" Then
                btn119.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn119, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "120" Then
                btn120.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn120, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If

            If item.System_IP.Trim() = "121" Then
                btn121.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn121, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "122" Then
                btn122.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn122, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "123" Then
                btn123.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn123, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "124" Then
                btn124.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn124, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "125" Then
                btn125.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn125, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If

            If item.System_IP.Trim() = "126" Then
                btn126.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn126, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "127" Then
                btn127.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn127, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "128" Then
                btn128.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn128, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "129" Then
                btn129.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn129, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "130" Then
                btn130.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn130, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "131" Then
                btn131.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn131, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "132" Then
                btn132.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn132, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "133" Then
                btn133.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn133, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "143" Then
                btn143.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn143, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "144" Then
                btn144.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn144, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "145" Then
                btn145.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn145, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "146" Then
                btn146.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn146, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "147" Then
                btn147.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn147, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "148" Then
                btn148.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn148, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "152" Then
                btn152.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn152, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "154" Then
                btn154.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn154, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "155" Then
                btn155.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn155, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "156" Then
                btn156.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn156, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "157" Then
                btn157.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn157, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "158" Then
                btn158.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn158, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "159" Then
                btn159.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn159, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "161" Then
                btn161.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn161, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "162" Then
                btn162.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn162, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "163" Then
                btn163.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn163, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "164" Then
                btn164.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn164, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "165" Then
                btn165.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn165, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "166" Then
                btn166.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn166, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "167" Then
                btn167.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn167, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "168" Then
                btn168.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn168, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "173" Then
                btn173.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn173, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "175" Then
                btn175.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn175, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "176" Then
                ToolTip1.SetToolTip(btn176, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
                btn176.Text = item.UserName + "-" + item.System_IP.ToString()
            End If
            If item.System_IP.Trim() = "174" Then
                btn174.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn174, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "179" Then
                btn179.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn179, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "184" Then
                btn184.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn184, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "185" Then
                btn185.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn185, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "187" Then
                btn187.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn187, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "188" Then
                btn188.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn188, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "189" Or item.System_IP.Trim() = "209" Then
                btn189.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn189, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "191" Or item.System_IP.Trim() = "211" Then
                btn191.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn191, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "192" Or item.System_IP.Trim() = "212" Then
                btn192.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn192, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "193" Or item.System_IP.Trim() = "213" Then
                btn193.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn193, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "194" Or item.System_IP.Trim() = "214" Then
                btn194.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn194, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If

            If item.System_IP.Trim() = "195" Or item.System_IP.Trim() = "215" Then
                btn195.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn195, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If

            If item.System_IP.Trim() = "200" Then
                btn200.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn200, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "203" Then
                btn203.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn203, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "206" Then
                btn206.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn206, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "225" Then
                btn225.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn225, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "226" Then
                btn226.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn226, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "235" Then
                btn235.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn235, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "237" Then
                btn237.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn237, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "238" Then
                btn238.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn238, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "242" Then
                btn242.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn242, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "244" Then
                btn244.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn244, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If
            If item.System_IP.Trim() = "247" Or item.System_IP.Trim() = "248" Then
                btn247.Text = item.UserName + "-" + item.System_IP.ToString()
                ToolTip1.SetToolTip(btn247, "User :- " + item.UserName + Environment.NewLine + "IP :-" + item.System_IP.ToString() + Environment.NewLine + "Type :-" + item.Clock_In_Out_Type + Environment.NewLine + "Remarks :-" + item.Remarks)
            End If


            If item.System_IP.Trim() = "2" And item.ClockIn = 0 Then
                btn2.BackColor = Color.Red
            End If
            If item.System_IP = "101" And item.ClockIn = 0 Then
                btn101.BackColor = Color.Red
            End If
            If item.System_IP = "111" And item.ClockIn = 0 Then
                btn111.BackColor = Color.Red
            End If
            If item.System_IP = "112" And item.ClockIn = 0 Then
                btn112.BackColor = Color.Red
            End If
            If item.System_IP = "114" And item.ClockIn = 0 Then
                btn114.BackColor = Color.Red
            End If
            If item.System_IP = "115" And item.ClockIn = 0 Then
                btn115.BackColor = Color.Red
            End If
            If item.System_IP = "116" And item.ClockIn = 0 Then
                btn116.BackColor = Color.Red
            End If
            If item.System_IP = "117" And item.ClockIn = 0 Then
                btn117.BackColor = Color.Red
            End If
            If item.System_IP = "118" And item.ClockIn = 0 Then
                btn118.BackColor = Color.Red
            End If
            If item.System_IP = "119" And item.ClockIn = 0 Then
                btn119.BackColor = Color.Red
            End If
            If item.System_IP = "120" And item.ClockIn = 0 Then
                btn120.BackColor = Color.Red
            End If
            If item.System_IP = "121" And item.ClockIn = 0 Then
                btn121.BackColor = Color.Red
            End If
            If item.System_IP = "122" And item.ClockIn = 0 Then
                btn122.BackColor = Color.Red
            End If
            If item.System_IP = "123" And item.ClockIn = 0 Then
                btn123.BackColor = Color.Red
            End If
            If item.System_IP = "124" And item.ClockIn = 0 Then
                btn124.BackColor = Color.Red
            End If
            If item.System_IP = "125" And item.ClockIn = 0 Then
                btn125.BackColor = Color.Red
            End If
            If item.System_IP = "126" And item.ClockIn = 0 Then
                btn126.BackColor = Color.Red
            End If
            If item.System_IP = "127" And item.ClockIn = 0 Then
                btn127.BackColor = Color.Red
            End If
            If item.System_IP = "128" And item.ClockIn = 0 Then
                btn128.BackColor = Color.Red
            End If
            If item.System_IP = "129" And item.ClockIn = 0 Then
                btn129.BackColor = Color.Red
            End If
            If item.System_IP = "130" And item.ClockIn = 0 Then
                btn130.BackColor = Color.Red
            End If
            If item.System_IP = "131" And item.ClockIn = 0 Then
                btn131.BackColor = Color.Red
            End If
            If item.System_IP = "132" And item.ClockIn = 0 Then
                btn132.BackColor = Color.Red
            End If
            If item.System_IP = "133" And item.ClockIn = 0 Then
                btn133.BackColor = Color.Red
            End If
            If item.System_IP = "144" And item.ClockIn = 0 Then
                btn144.BackColor = Color.Red
            End If
            If item.System_IP = "143" And item.ClockIn = 0 Then
                btn143.BackColor = Color.Red
            End If
            If item.System_IP = "145" And item.ClockIn = 0 Then
                btn145.BackColor = Color.Red
            End If
            If item.System_IP = "146" And item.ClockIn = 0 Then
                btn146.BackColor = Color.Red
            End If
            If item.System_IP = "147" And item.ClockIn = 0 Then
                btn147.BackColor = Color.Red
            End If
            If item.System_IP = "148" And item.ClockIn = 0 Then
                btn148.BackColor = Color.Red
            End If
            If item.System_IP = "152" And item.ClockIn = 0 Then
                btn152.BackColor = Color.Red
            End If
            If item.System_IP = "154" And item.ClockIn = 0 Then
                btn154.BackColor = Color.Red
            End If
            If item.System_IP = "155" And item.ClockIn = 0 Then
                btn155.BackColor = Color.Red
            End If
            If item.System_IP = "156" And item.ClockIn = 0 Then
                btn156.BackColor = Color.Red
            End If
            If item.System_IP = "157" And item.ClockIn = 0 Then
                btn157.BackColor = Color.Red
            End If
            If item.System_IP = "158" And item.ClockIn = 0 Then
                btn158.BackColor = Color.Red
            End If
            If item.System_IP = "159" And item.ClockIn = 0 Then
                btn159.BackColor = Color.Red
            End If
            If item.System_IP = "161" And item.ClockIn = 0 Then
                btn161.BackColor = Color.Red
            End If
            If item.System_IP = "162" And item.ClockIn = 0 Then
                btn162.BackColor = Color.Red
            End If
            If item.System_IP = "163" And item.ClockIn = 0 Then
                btn163.BackColor = Color.Red
            End If
            If item.System_IP = "164" And item.ClockIn = 0 Then
                btn164.BackColor = Color.Red
            End If
            If item.System_IP = "165" And item.ClockIn = 0 Then
                btn165.BackColor = Color.Red
            End If
            If item.System_IP = "173" And item.ClockIn = 0 Then
                btn173.BackColor = Color.Red
            End If
            If item.System_IP = "174" And item.ClockIn = 0 Then
                btn174.BackColor = Color.Red
            End If
            If item.System_IP = "175" And item.ClockIn = 0 Then
                btn175.BackColor = Color.Red
            End If
            If item.System_IP = "176" And item.ClockIn = 0 Then
                btn176.BackColor = Color.Red
            End If
            If item.System_IP = "179" And item.ClockIn = 0 Then
                btn174.BackColor = Color.Red
            End If
            If item.System_IP = "184" And item.ClockIn = 0 Then
                btn184.BackColor = Color.Red
            End If
            If item.System_IP = "185" And item.ClockIn = 0 Then
                btn185.BackColor = Color.Red
            End If
            If item.System_IP = "187" And item.ClockIn = 0 Then
                btn187.BackColor = Color.Red
            End If
            If item.System_IP = "188" And item.ClockIn = 0 Then
                btn188.BackColor = Color.Red
            End If

            If item.System_IP = "200" And item.ClockIn = 0 Then
                btn200.BackColor = Color.Red
            End If
            If item.System_IP = "203" And item.ClockIn = 0 Then
                btn203.BackColor = Color.Red
            End If
            If item.System_IP = "206" And item.ClockIn = 0 Then
                btn206.BackColor = Color.Red
            End If
            If item.System_IP = "225" And item.ClockIn = 0 Then
                btn225.BackColor = Color.Red
            End If
            If item.System_IP = "226" And item.ClockIn = 0 Then
                btn226.BackColor = Color.Red
            End If
            If item.System_IP = "237" And item.ClockIn = 0 Then
                btn237.BackColor = Color.Red
            End If
            If item.System_IP = "238" And item.ClockIn = 0 Then
                btn238.BackColor = Color.Red
            End If
            If item.System_IP = "242" And item.ClockIn = 0 Then
                btn242.BackColor = Color.Red
            End If
            If item.System_IP = "244" And item.ClockIn = 0 Then
                btn244.BackColor = Color.Red
            End If


            'For Laptop
            If (item.System_IP.Trim() = "247" Or item.System_IP.Trim() = "248") And item.ClockIn = 0 Then
                btn247.BackColor = Color.Red
            End If

            If (item.System_IP.Trim() = "189" Or item.System_IP.Trim() = "209") And item.ClockIn = 0 Then
                btn189.BackColor = Color.Red

            End If
            If (item.System_IP.Trim() = "191" Or item.System_IP.Trim() = "211") And item.ClockIn = 0 Then
                btn191.BackColor = Color.Red
            End If
            If (item.System_IP.Trim() = "192" Or item.System_IP.Trim() = "212") And item.ClockIn = 0 Then
                btn192.BackColor = Color.Red

            End If
            If (item.System_IP.Trim() = "193" Or item.System_IP.Trim() = "213") And item.ClockIn = 0 Then
                btn193.BackColor = Color.Red
            End If
            If (item.System_IP.Trim() = "194" Or item.System_IP.Trim() = "214") And item.ClockIn = 0 Then
                btn194.BackColor = Color.Red
            End If

            If (item.System_IP.Trim() = "195" Or item.System_IP.Trim() = "215") And item.ClockIn = 0 Then
                btn195.BackColor = Color.Red
            End If

        Next


    End Sub
    Public Sub btn_color()

        btn154.BackColor = Color.Green
        btn206.BackColor = Color.Green
        btn155.BackColor = Color.Green
        btn200.BackColor = Color.Green
        btn242.BackColor = Color.Green
        btn152.BackColor = Color.Green
        btn148.BackColor = Color.Green
        btn244.BackColor = Color.Green
        btn146.BackColor = Color.Green
        btn145.BackColor = Color.Green
        btn130.BackColor = Color.Green
        btn131.BackColor = Color.Green
        btn132.BackColor = Color.Green
        btn133.BackColor = Color.Green
        btn128.BackColor = Color.Green
        btn129.BackColor = Color.Green
        btn188.BackColor = Color.Green
        btn111.BackColor = Color.Green
        btn123.BackColor = Color.Green
        btn124.BackColor = Color.Green
        btn225.BackColor = Color.Green
        btn226.BackColor = Color.Green
        btn126.BackColor = Color.Green
        btn101.BackColor = Color.Green
        btn127.BackColor = Color.Green
        btn126.BackColor = Color.Green
        btn173.BackColor = Color.Green
        btn185.BackColor = Color.Green
        btn156.BackColor = Color.Green
        btn237.BackColor = Color.Green
        btn101.BackColor = Color.Green
        btn184.BackColor = Color.Green
        btn119.BackColor = Color.Green
        btn120.BackColor = Color.Green
        btn121.BackColor = Color.Green
        btn122.BackColor = Color.Green
        btn125.BackColor = Color.Green
        btn116.BackColor = Color.Green
        btn117.BackColor = Color.Green
        btn118.BackColor = Color.Green
        btn114.BackColor = Color.Green
        btn157.BackColor = Color.Green
        btn112.BackColor = Color.Green
        btn115.BackColor = Color.Green
        btn147.BackColor = Color.Green
        btn238.BackColor = Color.Green
        btn203.BackColor = Color.Green
        btn174.BackColor = Color.Green
        btn175.BackColor = Color.Green
        btn158.BackColor = Color.Green
        btn187.BackColor = Color.Green
        btn174.BackColor = Color.Green
        btn159.BackColor = Color.Green
        btn143.BackColor = Color.Green
        btn144.BackColor = Color.Green
        btn176.BackColor = Color.Green
        btn164.BackColor = Color.Green
        btn165.BackColor = Color.Green
        btn161.BackColor = Color.Green
        btn162.BackColor = Color.Green
        btn163.BackColor = Color.Green
        btn144.BackColor = Color.Green
        btn2.BackColor = Color.Green
        btn103.BackColor = Color.Green

        btn166.BackColor = Color.Green
        btn167.BackColor = Color.Green
        btn168.BackColor = Color.Green

        btn179.BackColor = Color.Green
        btn174.BackColor = Color.Green
        btn247.BackColor = Color.Green
        btn189.BackColor = Color.Green
        btn191.BackColor = Color.Green
        btn192.BackColor = Color.Green
        btn193.BackColor = Color.Green
        btn194.BackColor = Color.Green
        btn195.BackColor = Color.Green




    End Sub
    Public Sub btn_ForColor()
        btn154.ForeColor = Color.White
        btn206.ForeColor = Color.White
        btn155.ForeColor = Color.White
        btn200.ForeColor = Color.White
        btn242.ForeColor = Color.White
        btn152.ForeColor = Color.White
        btn148.ForeColor = Color.White
        btn244.ForeColor = Color.White
        btn146.ForeColor = Color.White
        btn145.ForeColor = Color.White
        btn130.ForeColor = Color.White
        btn131.ForeColor = Color.White
        btn132.ForeColor = Color.White
        btn133.ForeColor = Color.White
        btn128.ForeColor = Color.White
        btn129.ForeColor = Color.White
        btn188.ForeColor = Color.White
        btn111.ForeColor = Color.White
        btn123.ForeColor = Color.White
        btn124.ForeColor = Color.White
        btn225.ForeColor = Color.White
        btn226.ForeColor = Color.White
        btn126.ForeColor = Color.White
        btn101.ForeColor = Color.White
        btn127.ForeColor = Color.White
        btn126.ForeColor = Color.White
        btn173.ForeColor = Color.White
        btn185.ForeColor = Color.White
        btn156.ForeColor = Color.White
        btn237.ForeColor = Color.White
        btn101.ForeColor = Color.White
        btn184.ForeColor = Color.White
        btn119.ForeColor = Color.White
        btn120.ForeColor = Color.White
        btn121.ForeColor = Color.White
        btn122.ForeColor = Color.White
        btn125.ForeColor = Color.White
        btn116.ForeColor = Color.White
        btn117.ForeColor = Color.White
        btn118.ForeColor = Color.White
        btn114.ForeColor = Color.White
        btn157.ForeColor = Color.White
        btn112.ForeColor = Color.White
        btn115.ForeColor = Color.White
        btn147.ForeColor = Color.White
        btn238.ForeColor = Color.White
        btn203.ForeColor = Color.White
        btn174.ForeColor = Color.White
        btn175.ForeColor = Color.White
        btn158.ForeColor = Color.White
        btn187.ForeColor = Color.White
        btn174.ForeColor = Color.White
        btn159.ForeColor = Color.White
        btn144.ForeColor = Color.White
        btn176.ForeColor = Color.White
        btn164.ForeColor = Color.White
        btn165.ForeColor = Color.White
        btn161.ForeColor = Color.White
        btn162.ForeColor = Color.White
        btn163.ForeColor = Color.White
        btn144.ForeColor = Color.White
        btn143.ForeColor = Color.White
        btn2.ForeColor = Color.White
        btn103.ForeColor = Color.White

        btn166.ForeColor = Color.White
        btn167.ForeColor = Color.White
        btn168.ForeColor = Color.White


        btn179.ForeColor = Color.White
        btn235.ForeColor = Color.White
        btn174.ForeColor = Color.White
        btn247.ForeColor = Color.White
        btn189.ForeColor = Color.White
        btn191.ForeColor = Color.White
        btn192.ForeColor = Color.White
        btn193.ForeColor = Color.White
        btn194.ForeColor = Color.White
        btn195.ForeColor = Color.White

    End Sub





End Class