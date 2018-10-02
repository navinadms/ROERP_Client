Imports System.Data.SqlClient

Public Class Rpt_OrderWeeklyReport
    Dim strCriteria As String
    Dim dataTable As DataTable
    Dim ds As New DataSet
    Dim count As Integer

    Dim linq_obj As New RoErpDataContext(System.Configuration.ConfigurationManager.AppSettings("constr").ToString())

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As Date?
        dt = dtStartDate.Value




        Dim data = linq_obj.Sp_GetWeeklyOrderReport(dtStartDate.Value, dtEndDate.Value, cmbOrderStatus.Text).ToList()
        If (data.Count > 0) Then

            dgRptMainView.DataSource = data
            dgRptMainView.Columns(dgRptMainView.Columns.Count - 1).Visible = False

            Dim strName() As String = {"LAB", "JAR", "POUCH", "BOTTLE", "BLOW", "GLASS", "BATCH", "BULK", "CHILLER", "SODA"}
            Dim strType As String = ""
            Dim strNameView As String = ""
            Dim cmd As New SqlCommand

            cmd.CommandText = "Sp_GetWeeklyOrderReportSummaryCountByType"

            cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = dtStartDate.Text
            cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = dtEndDate.Text
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = cmbOrderStatus.Text.Trim()


            For i = 0 To strName.Length - 1

                Select Case strName(i)
                    Case "LAB"
                        strType = "Lab"
                        strNameView = strName(i)
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgLAB.DataSource = Nothing
                        Else
                            dgLAB.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                    Case "JAR"
                        strType = "JarWashingCapacity"
                        strNameView = strName(i)
                        cmd.Parameters.RemoveAt("@TYPE")
                        cmd.Parameters.RemoveAt("@NAME")
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgJAR.DataSource = Nothing

                        Else
                            dgJAR.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                    Case "POUCH"
                        strType = "Pouch"
                        strNameView = strName(i)
                        cmd.Parameters.RemoveAt("@TYPE")
                        cmd.Parameters.RemoveAt("@NAME")
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgPouch.DataSource = Nothing
                        Else
                            dgPouch.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                    Case "BOTTLE"
                        strType = "BottllingCapacity"
                        strNameView = strName(i)
                        cmd.Parameters.RemoveAt("@TYPE")
                        cmd.Parameters.RemoveAt("@NAME")
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgBottle.DataSource = Nothing
                        Else
                            dgBottle.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                    Case "BLOW"
                        strType = "BlowCapacity"
                        strNameView = strName(i)
                        cmd.Parameters.RemoveAt("@TYPE")
                        cmd.Parameters.RemoveAt("@NAME")
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgBlow.DataSource = Nothing
                        Else
                            dgBlow.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                    Case "BULK"
                        strType = "BulkCapacity"
                        strNameView = strName(i)
                        cmd.Parameters.RemoveAt("@TYPE")
                        cmd.Parameters.RemoveAt("@NAME")
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgBulk.DataSource = Nothing
                        Else
                            dgBulk.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                    Case "GLASS"
                        strType = "GlassCapacity"
                        strNameView = strName(i)
                        cmd.Parameters.RemoveAt("@TYPE")
                        cmd.Parameters.RemoveAt("@NAME")
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgGlass.DataSource = Nothing
                        Else
                            dgGlass.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                    Case "BATCH"
                        strType = "BatchCoding"
                        strNameView = strName(i)
                        cmd.Parameters.RemoveAt("@TYPE")
                        cmd.Parameters.RemoveAt("@NAME")
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgBatch.DataSource = Nothing
                        Else
                            dgBatch.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                    Case "CHILLER"
                        strType = "Chiller"
                        strNameView = strName(i)
                        cmd.Parameters.RemoveAt("@TYPE")
                        cmd.Parameters.RemoveAt("@NAME")
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgChiller.DataSource = Nothing
                        Else
                            dgChiller.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                    Case "SODA"
                        strType = "SodaCapacity"
                        strNameView = strName(i)
                        cmd.Parameters.RemoveAt("@TYPE")
                        cmd.Parameters.RemoveAt("@NAME")
                        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = strType
                        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = strNameView
                        Dim objclass As New Class1

                        Dim ds As New DataSet
                        ds = objclass.GetOrderCountData(cmd)
                        If ds.Tables(0).Rows.Count < 1 Then
                            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgSoda.DataSource = Nothing
                        Else
                            dgSoda.DataSource = ds.Tables(0)
                            ds.Dispose()
                        End If
                End Select


            Next
        Else
            dgRptMainView.DataSource = Nothing
            MessageBox.Show("Record Not Found", "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If


    End Sub
End Class