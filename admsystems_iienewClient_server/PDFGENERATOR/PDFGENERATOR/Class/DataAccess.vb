Imports System.Data
Imports System.Data.SqlClient

Public Class DataAccess

    Dim oConn As SqlConnection
    Public iError As Integer
    Public sMessage As String

    Public Sub New()
        iError = -1
        sMessage = ""
    End Sub

    Private Sub OpenConnection()
        Try

            Dim sConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("constr").ToString()

            If oConn Is Nothing Then
                oConn = New SqlConnection(sConnectionString)
                oConn.Open()
            Else
                If oConn.State <> ConnectionState.Open Then
                    oConn = New SqlConnection(sConnectionString)
                    oConn.Open()
                End If
            End If

        Catch ex As Exception
            iError = 1
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
                oConn.Dispose()
            End If
        End Try

    End Sub

    Private Sub CloseConnection()

       Try
            If Not (oConn Is Nothing) Then
                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                    oConn.Dispose()
                End If
            End If
        Catch ex As SqlException
            iError = ex.ErrorCode
        End Try
    End Sub

     Public Function ExecuteQuery(oCmd As SqlCommand) As Integer
        Dim iRowAffected As Integer = 0
        Dim oTran As SqlTransaction = Nothing
        OpenConnection()
        Try
            oCmd.Connection = oConn
            oTran = oConn.BeginTransaction()
            oCmd.Transaction = oTran
            iRowAffected = oCmd.ExecuteNonQuery()
            oTran.Commit()
        Catch ex As SqlException
            iError = ex.Number
            sMessage = ex.Message
            MessageBox.Show("Error No " & iError & " - " & sMessage)
            oTran.Rollback()
        Finally
            oTran.Dispose()
            oCmd.Dispose()
        End Try
        CloseConnection()

        Return iRowAffected
    End Function

    Public Function ExecuteSP(ByVal oCmd() As SqlCommand, Optional ByVal rtnCmd As SqlCommand = Nothing, Optional ByVal oParaName As String = "") As Boolean
        Dim iRowAffected As Boolean = False
        Dim oTran As SqlTransaction = Nothing
        OpenConnection()
        Try
            oTran = oConn.BeginTransaction()
            For Each item As SqlCommand In oCmd
                Debug.Print(item.CommandText)
                If rtnCmd IsNot Nothing Then
                    For Each itempara As SqlParameter In item.Parameters
                        If itempara.SqlDbType = SqlDbType.VarChar Then
                            If Convert.ToString(itempara.Value) = "GetRtnValue" Then
                                itempara.Value = rtnCmd.Parameters(oParaName).Value
                                Exit For
                            End If
                        End If
                    Next
                End If

                item.Connection = oConn
                item.Transaction = oTran
                item.ExecuteNonQuery()
            Next
            oTran.Commit()
            iRowAffected = True
        Catch ex As SqlException
            iError = ex.Number
            sMessage = ex.Message
            oTran.Rollback()
            MessageBox.Show("Error No " & iError & " - " & sMessage)
        Finally
            oTran.Dispose()
        End Try
        CloseConnection()

        Return iRowAffected
    End Function

    Public Function ExecuteDataTable(ByVal oCmd As SqlCommand) As DataTable
        Dim oDataAdpt As SqlDataAdapter = Nothing
        Dim oDataTable As DataTable = Nothing
        OpenConnection()
        Try
            oCmd.Connection = oConn
            oDataTable = New DataTable()
            oDataAdpt = New SqlDataAdapter(oCmd)
            oDataAdpt.Fill(oDataTable)

            CloseConnection()
        Catch ex As SqlException

            iError = ex.Number
        Finally
            oCmd.Dispose()
            oDataAdpt.Dispose()
        End Try
        CloseConnection()
        Return oDataTable


    End Function
    
    '    public DataSet ExecuteDataSet(SqlCommand oCmd)
    '    {
    '        SqlDataAdapter oDataAdpt = null;
    '        DataSet oDataSet = null;
    '        OpenConnection();
    '                            Try
    '        {
    '            oCmd.Connection = oConn;
    '            oDataSet = new DataSet();
    '            oDataAdpt = new SqlDataAdapter(oCmd);
    '            oDataAdpt.Fill(oDataSet);
    '        }
    '        catch (SqlException ex)
    '        {
    '            iError = ex.Number;
    '        }
    '                            Finally
    '        {
    '            oCmd.Dispose();
    '            oDataAdpt.Dispose();
    '        }
    '        CloseConnection();
    '        return oDataSet;
    '    }
    '}
End Class
