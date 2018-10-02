Imports System.Data
Imports System.Data.SqlClient
Public Class frmAnnxure
    Dim bs As BindingSource
    Dim oDataAcesss As New DataAccess
    Dim intPkId As Integer = 0

    Enum DGIndex
        PkId = 0
        SRno = 1
        Description = 2
        Remarks = 3
        Qtype = 4
        Plant = 5
        Model = 6

    End Enum

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim cmd As New SqlCommand
            Dim iRowAffected As Integer = 0
            Dim iSrno As Integer = 0

            If txtSrno.Text = "" Then
                txtSrno.Text = 0
            End If

            cmd.CommandText = "SP_Insert_Update_ANNEXURE1"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@Pk_Id", SqlDbType.BigInt).Value = intPkId

            Dim oPara As SqlParameter
            oPara = New SqlParameter()
            oPara.ParameterName = "@Srno"
            oPara.Value = Convert.ToInt32(txtSrno.Text)
            oPara.Direction = ParameterDirection.InputOutput
            oPara.SqlDbType = SqlDbType.BigInt
            cmd.Parameters.Add(oPara)

            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtDesciption.Text
            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = txtRemarks.Text
            cmd.Parameters.Add("@QType", SqlDbType.VarChar).Value = txtQtype.Text
            cmd.Parameters.Add("@Plant", SqlDbType.VarChar).Value = txtPlant.Text
            cmd.Parameters.Add("@Model", SqlDbType.VarChar).Value = txtModel.Text

            iRowAffected = oDataAcesss.ExecuteQuery(cmd)

            If iRowAffected <= 0 Then
                Exit Sub
            End If
            iSrno = cmd.Parameters("@Srno").Value
            btnSave.Text = "Save"
            MessageBox.Show("Data " + btnSave.Text + " Succussful " + Environment.NewLine + "SrNo :- " + Convert.ToString(iSrno))
            ClearAll()
            DGFind()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub DGFind()
        intPkId = 0
        
        Dim dt As DataTable
        Dim cmd As New SqlCommand

        cmd.CommandText = "SP_Select_ANNEXURE1"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@Pk_Id", SqlDbType.BigInt).Value = intPkId

        dt = oDataAcesss.ExecuteDataTable(cmd)
        DGMain.DataSource = Nothing

        If dt.Rows.Count > 0 Then
            DGMain.DataSource = dt
            bs = New BindingSource(dt, Nothing)
            DGMain.DataSource = bs
            DGMain.Columns(0).Visible = False
        End If


    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearAll()
        btnSave.Text = "Save"
    End Sub


    Private Sub frmAnnxure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

        
        ClearAll()
        DGFind()
            AutoComplete_Text()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtSearchBy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchBy.TextChanged
        bs.Filter = Nothing
      If cmbSearchBy.Text = "Qtype" Then
            If bs IsNot Nothing Then
                bs.Filter = ("Qtype like '%" + txtSearchBy.Text & "%'")
            End If
        ElseIf cmbSearchBy.Text = "Plant" Then
            If bs IsNot Nothing Then
                bs.Filter = ("Plant like '%" + txtSearchBy.Text & "%'")
            End If
        ElseIf cmbSearchBy.Text = "Model" Then
            If bs IsNot Nothing Then
                bs.Filter = ("Model like '%" + txtSearchBy.Text & "%'")
            End If
        End If
    End Sub

    

    Private Sub DGMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGMain.DoubleClick
        Try
            ClearAll()
        
        Dim iCurrentRow As Integer
        iCurrentRow = DGMain.CurrentRow.Index
            btnSave.Text = "Update"
        intPkId = DGMain.Rows(iCurrentRow).Cells(DGIndex.PkId).Value
        txtSrno.Text = DGMain.Rows(iCurrentRow).Cells(DGIndex.SRno).Value
        txtDesciption.Text = DGMain.Rows(iCurrentRow).Cells(DGIndex.Description).Value
        txtRemarks.Text = DGMain.Rows(iCurrentRow).Cells(DGIndex.Remarks).Value
        txtQtype.Text = DGMain.Rows(iCurrentRow).Cells(DGIndex.Qtype).Value
        txtModel.Text = DGMain.Rows(iCurrentRow).Cells(DGIndex.Model).Value
        txtPlant.Text = DGMain.Rows(iCurrentRow).Cells(DGIndex.Plant).Value

        
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub ClearAll()
        intPkId = 0
        txtSrno.Text = ""
        txtDesciption.Text = ""
        txtRemarks.Text = ""
        txtPlant.Text = ""
        txtModel.Text = ""
        txtQtype.Text = ""
        btnSave.Text = "Save"

    End Sub

    Private Sub btnCancel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel1.Click
        ClearAll()
        btnSave.Text = "Save"
    End Sub

    Private Sub cmbSearchBy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSearchBy.SelectedIndexChanged
        bs.Filter = Nothing
        txtSearchBy.Text = ""

        Dim dt As DataTable
        Dim cmd As New SqlCommand

        cmd.CommandText = "SP_Select_ANNEXURE1"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@Pk_Id", SqlDbType.BigInt).Value = 0
        cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = "S"

        dt = oDataAcesss.ExecuteDataTable(cmd)
        
        If cmbSearchBy.Text = "Qtype" Then
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtSearchBy.AutoCompleteCustomSource.Add(dr("Qtype"))
                Next
            End If

        ElseIf cmbSearchBy.Text = "Plant" Then
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtSearchBy.AutoCompleteCustomSource.Add(dr("Plant"))
                Next
            End If

        ElseIf cmbSearchBy.Text = "Model" Then
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtSearchBy.AutoCompleteCustomSource.Add(dr("Model"))
                Next
            End If

        End If

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If MessageBox.Show("Do you want to delete data", "Delete Data", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                Dim cmd As New SqlCommand
                Dim iRowAffected As Integer = 0


                cmd.CommandText = "SP_Delete_ANNEXURE1"
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@Pk_Id", SqlDbType.BigInt).Value = intPkId

                iRowAffected = oDataAcesss.ExecuteQuery(cmd)

                If iRowAffected <= 0 Then
                    Exit Sub
                End If

                MessageBox.Show("Data Deleted Succussful")
                ClearAll()
                DGFind()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub txtSrno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSrno.KeyPress
        e.Handled = Class1.OnlyNumeric(sender, e)
    End Sub
    Public Sub AutoComplete_Text()
        Dim dt As DataTable
        Dim cmd As New SqlCommand

        cmd.CommandText = "SP_Select_ANNEXURE1"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@Pk_Id", SqlDbType.BigInt).Value = 0
        cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = "S"

        dt = oDataAcesss.ExecuteDataTable(cmd)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                txtQtype.AutoCompleteCustomSource.Add(dr("Qtype"))
                txtPlant.AutoCompleteCustomSource.Add(dr("Plant"))
                txtModel.AutoCompleteCustomSource.Add(dr("Model"))
            Next
        End If

    End Sub

    Private Sub DGMain_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGMain.CellContentClick

    End Sub
End Class