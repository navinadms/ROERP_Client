Public Class TeamMaster
    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())
    Dim TeamId As Integer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        bindGrid()
    End Sub
    Public Sub bindGrid()
        Dim selectAll = linq_obj.SP_Tbl_TeamMaster_SelectAll().ToList()
        DGTeam.DataSource = selectAll
        DGTeam.Columns(0).Visible = False

    End Sub
    Private Sub btnAllot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllot.Click
        If (btnAllot.Text = "Update") Then
            If (txtTeamname.Text.Trim() <> "") Then
                Dim res As Integer = linq_obj.SP_Tbl_TeamMaster_Update(txtTeamname.Text.Trim(), txtDepartment.Text.Trim(), TeamId)
                If (res >= 0) Then
                    bindGrid()

                    btnCancel_Click(Nothing, Nothing)
                Else
                    MessageBox.Show("Error In Updation...")
                End If
            Else
                MessageBox.Show("Enter Team Name...")
            End If
        Else
            If (txtTeamname.Text.Trim() <> "") Then
                Dim res As Integer = linq_obj.SP_Tbl_TeamMaster_Insert(txtTeamname.Text.Trim(), txtDepartment.Text.Trim())
                If (res > 0) Then
                    bindGrid()

                    btnCancel_Click(Nothing, Nothing)
                Else
                    MessageBox.Show("Error In Insertion...")
                End If
            Else
                MessageBox.Show("Enter Team Name...")
            End If
        End If


    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtDepartment.Text = ""
        txtTeamname.Text = ""
        btnAllot.Text = "Submit"

    End Sub

    Private Sub DGTeam_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGTeam.DoubleClick
        Try
            TeamId = Convert.ToInt32(Me.DGTeam.SelectedCells(0).Value)
            Dim data = linq_obj.SP_Tbl_TeamMaster_Select(TeamId).ToList()
            If (data.Count > 0) Then
                txtDepartment.Text = Convert.ToString(data(0).Department)
                txtTeamname.Text = Convert.ToString(data(0).TeamName)
                btnAllot.Text = "Update"
            Else
                MessageBox.Show("Data Not Found...")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            '*
            ' * display a confirmation message
            '                 

            Dim result As DialogResult = MessageBox.Show("Are You Sure Delete ?", "Important Message", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If result = DialogResult.Yes Then


                Dim cntSelect As Integer = DGTeam.SelectedRows.Count
                For Each dr As DataGridViewRow In DGTeam.SelectedRows
                    Dim resDelete As Integer = linq_obj.SP_Tbl_TeamMaster_Delete(Convert.ToInt32(dr.Cells(0).Value))
                    linq_obj.SubmitChanges()
                Next
                bindGrid()

                MessageBox.Show("Successfully Deleted...")

            End If
        Catch ex As Exception

            MessageBox.Show(ex.ToString())
        End Try
    End Sub

   
End Class