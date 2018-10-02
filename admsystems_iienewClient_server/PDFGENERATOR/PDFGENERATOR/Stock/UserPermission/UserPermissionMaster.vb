Imports iTextSharp.text

Public Class UserPermissionMaster
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoComplete_Text()
        bindDropdown()

    End Sub

    Public Sub AutoComplete_Text()

    End Sub
    Public Sub bindDropdown()
        cmbUserList.Items.Clear()
        Dim users = linq_obj.SP_Get_UserList().ToList()
        cmbUserList.DataSource = users
        cmbUserList.DisplayMember = "UserName"
        cmbUserList.ValueMember = "Pk_UserId"
        cmbUserList.AutoCompleteMode = AutoCompleteMode.Append
        cmbUserList.DropDownStyle = ComboBoxStyle.DropDownList
        cmbUserList.AutoCompleteSource = AutoCompleteSource.ListItems
        'bind Software
        checkListSoftware.Items.Clear()
        Dim getSoftware = linq_obj.SP_Tbl_SoftwareMaster_Select_All().ToList()

        checkListSoftware.DataSource = getSoftware
        checkListSoftware.DisplayMember = "Name"
        checkListSoftware.ValueMember = "Pk_SoftwareId"
    End Sub
    Public Function getValue() As String
        Dim str As String

        '  Type t = checkListSoftware.SelectedItems(0))
        str = "Inquiry Manager"

        Return str

    End Function
    Private Sub RBGeneral_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
      
    End Sub

    Private Sub RBGeneral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBGeneral.Click
        If RBGeneral.Checked = True Then
            checkListDetail.Items.Clear()
            Dim str As String
            str = getValue()
            If str <> "" Then
                grpModules.Visible = True
                lblModule.Visible = True
                Dim getSoftwareDetail = linq_obj.SP_Tbl_SoftwareDetail_Select(str).ToList()
                checkListDetail.DataSource = getSoftwareDetail
                checkListDetail.DisplayMember = "DetailName"
                checkListDetail.ValueMember = "Pk_SoftwareDetail"
            Else
                grpModules.Visible = False
                lblModule.Visible = False
            End If
        Else

        End If
    End Sub

   

End Class