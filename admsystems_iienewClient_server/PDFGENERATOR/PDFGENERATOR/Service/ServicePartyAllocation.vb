
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
Public Class ServicePartyAllocation
    Dim PK_Address_ID As Integer
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ddlUserAllotment_Bind()
    End Sub

    Public Sub ddlUserAllotment_Bind()
        ddlUserAllotment.DataSource = Nothing
        Dim dataUser = linq_obj.SP_Get_UserList().ToList()
        ddlUserAllotment.DataSource = dataUser
        ddlUserAllotment.DisplayMember = "UserName"
        ddlUserAllotment.ValueMember = "Pk_UserId"
        ddlUserAllotment.AutoCompleteMode = AutoCompleteMode.Append
        ddlUserAllotment.DropDownStyle = ComboBoxStyle.DropDownList
        ddlUserAllotment.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try

            If PK_Address_ID > 0 Then
                linq_obj.SP_Insert_Update_Service_Party_Allocation(PK_Address_ID, Convert.ToInt32(ddlUserAllotment.SelectedValue))
                linq_obj.SubmitChanges()
                MessageBox.Show("Assign Sucessfully...")
                txtEnqNo.Text = ""
                lblPartyName.Text = ""
            Else

                MessageBox.Show("Invalid Enq No try Again...")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtEnqNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEnqNo.Leave
        If txtEnqNo.Text.Trim() <> "" Then
            Dim data = linq_obj.SP_Get_ServiceODSite_Allotment_EnqNo(txtEnqNo.Text.Trim()).ToList()
            If data.Count > 0 Then
                For Each item As SP_Get_ServiceODSite_Allotment_EnqNoResult In data
                    lblPartyName.Text = item.Name
                    PK_Address_ID = item.Pk_AddressID

                Next
            Else

                MessageBox.Show("Invalid EnqNo...")

                txtEnqNo.Focus()

            End If


        End If
    End Sub
End Class