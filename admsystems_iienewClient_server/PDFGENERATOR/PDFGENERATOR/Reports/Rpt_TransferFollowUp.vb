Public Class Rpt_TransferFollowUp
    Dim strCriteria As String
    Dim dataTable As DataTable
    Dim ds As New DataSet
    Dim count As Integer

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AutoCompated_Text()
        If (Class1.global.RptUserType = True) Then

            GrpUser.Visible = True
        Else
            GrpUser.Visible = False
        End If
        ' ddlEnqType_Bind()
    End Sub
    Public Sub AutoCompated_Text()
        Dim GetUser = linq_obj.SP_Get_UserList().ToList()
        For Each item As SP_Get_UserListResult In GetUser
            txtUser.AutoCompleteCustomSource.Add(item.UserName)
        Next
    End Sub
End Class