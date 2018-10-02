
Public Class ListFolloUps
    Dim dataTable As DataTable
    Dim ds As New DataSet
    Dim classobj = New Class1()

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.

        InitializeComponent()

        bindGrid()


    End Sub
    Public Sub bindGrid()
        Try
            dataTable = New DataTable()
            dataTable.Columns.Add("Follow Date")
            dataTable.Columns.Add("Follow UP")
            dataTable.Columns.Add("Next Follow Date")
            dataTable.Columns.Add("Status")
            dataTable.Columns.Add("By Whom")
            dataTable.Columns.Add("Enquiry Type")
            dataTable.Columns.Add("Remarks")
            dataTable.Columns.Add("AddressId")
            Dim data = linq_obj.SP_Select_FollowUp_Daily().ToList()
            If (data.Count > 0) Then
                For Each item As SP_Select_FollowUp_DailyResult In data
                    dataTable.Rows.Add(item.FollowUp, item.FollowUp, item.NextFollowUp, item.Status, item.ByWhom, item.EnqType, item.Remarks, item.Fk_AddressID)
                Next
                dgFollowUps.DataSource = dataTable
                dgFollowUps.Columns("AddressId").Visible = False
            Else
                dgFollowUps.DataSource = dataTable
                MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub dgFollowUps_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgFollowUps.DoubleClick
        Try
            Class1.IDAddress = dgFollowUps.SelectedCells(7).Value
            Class1.Flag = 1
            'Dim oform As InquiryForm
            'oform = New InquiryForm()
            MDIMainForm.Show()
            InquiryForm.MdiParent = MDIMainForm
            InquiryForm.MaximizeBox = False
            InquiryForm.MinimizeBox = False
            InquiryForm.StartPosition = FormStartPosition.CenterScreen
            '  _frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            InquiryForm.Show()

            Me.Close()
            ' oform.Visible = True
        Catch ex As Exception

        End Try


    End Sub

    Private Sub ListFolloUps_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


        Catch ex As Exception

        End Try

    End Sub
End Class