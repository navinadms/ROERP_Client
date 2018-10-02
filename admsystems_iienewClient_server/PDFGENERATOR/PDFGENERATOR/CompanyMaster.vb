Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports pdfforge
Imports System.IO
Imports System.IO.File
Imports System.IO.StreamWriter
Imports Microsoft.Office.Interop
Imports System.Security.Principal
Imports System.Management
Imports System.Globalization
Imports System.Threading
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports

Public Class CompanyMaster
    Shared con1 As SqlConnection
    Shared cmd As SqlCommand
    Shared dr As SqlDataReader
    Shared da As SqlDataAdapter
    Shared ds As DataSet
    Shared da1 As SqlDataAdapter
    Shared ds1 As DataSet
    Shared dt As DataTable
    Public capacityType As String
    Shared QuotationMaxId As Int32
    Shared Path11 As String
    Public UserID As Int32
    Shared Flag As Integer
    Shared CMaxId As Integer
    Shared CompanyId As Int32

    Public QPath As String
    Shared DocumentStatus As Int16
    Dim appPath As String
    Dim lines As String
    Shared LanguageId As Int32
    Shared Quatationid As Int32
    Shared OrderPath As String
    Shared OrderAgreementPath As String
    Shared OrderAgreementTempPath As String
    Shared flagExistspecialDoc As Integer
    Shared AnneIds As String
    Dim oDataAccess As DataAccess
    Dim str
    Public Sub New()
        flagExistspecialDoc = 0
        Flag = 0
        Quatationid = 0
        ' This call is required by the designer.
        InitializeComponent()
        appPath = Path.GetDirectoryName(Application.ExecutablePath)

        OrderPath = appPath + "\OrderData"
        QPath = Class1.global.QPath
        If (Not System.IO.Directory.Exists(QPath + "\Order Agreements")) Then
            System.IO.Directory.CreateDirectory(QPath + "\Order Agreements")
        End If
        If (Not System.IO.Directory.Exists(QPath + "\Order Agreements\Individual Files")) Then
            System.IO.Directory.CreateDirectory(QPath + "\Order Agreements\Individual Files")
        End If
        txtAnne1Default.Text = "25"
        If (Not System.IO.Directory.Exists(appPath + "\OrderData")) Then
            System.IO.Directory.CreateDirectory(appPath + "\OrderData")
        End If
        If (Not System.IO.Directory.Exists(appPath + "\OrderData\SpecialPrice")) Then
            System.IO.Directory.CreateDirectory(appPath + "\OrderData\SpecialPrice")
        End If


        OrderAgreementPath = QPath + "\Order Agreements"
        'set Static Path rightnow
        OrderAgreementTempPath = QPath + "\Order Agreements\Individual Files"

        GvQuotationSearch_Bind()
        con1 = Class1.con
        GetKind_SubData()
        BindGvComapnySearch()
        GetANN2()
        GvGetOnlyAnn4()
        GetComplementary()
        ' Add any initialization after the InitializeComponent() call.


        getPageRight()
    End Sub
    Public Sub getPageRight()
        Try
            Dim dv As New DataTable
            Dim dataView As DataView

            Dim Class1 As New Class1
            Dim RowCount As Integer
            Dim statusCheck As Boolean = False

            Dim strName As String = ""
            dataView = Class1.global.UserPermissionDataset.Tables(0).DefaultView
            dataView.RowFilter = "([DetailName] like 'Order Details')"
            If (dataView.Count > 0) Then
                dv = dataView.ToTable()
                If (dv.Rows(0)("Type") = 1) Then

                Else
                    For RowCount = 0 To dv.Rows.Count - 1
                        If (dv.Rows(RowCount)("IsAdd") = True) Then
                            btnSave1.Enabled = True
                        Else
                            btnSave1.Enabled = False
                        End If

                        If (dv.Rows(RowCount)("IsPrint") = True) Then
                            btnHF.Enabled = True
                            btnFirstQuat.Enabled = True
                            btnPriceSheet.Enabled = True
                            btnWf.Enabled = True
                            btnTerms.Enabled = True

                        Else
                            btnHF.Enabled = False
                            btnFirstQuat.Enabled = False
                            btnPriceSheet.Enabled = False
                            btnWf.Enabled = False
                            btnTerms.Enabled = False
                        End If
                        If (dv.Rows(RowCount)("IsDelete") = True) Then

                        Else

                        End If
                    Next
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub GetComplementary()
        Try
            con1 = Class1.con
            con1.Close()
        Catch ex As Exception

        End Try
        Try
            dt = New DataTable()
            dt.Columns.Add("Remove", GetType(Boolean))
            dt.Columns.Add("Select", GetType(Boolean))
            dt.Columns.Add("Sr_No", GetType(String))
            dt.Columns.Add("Description", GetType(String))
            dt.Columns.Add("Qty", GetType(String))

            con1.Open()

            Dim str As String
            str = "select Sr_No,Description,Qty from Complementry order by Sr_No ASC"
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()

            da.Fill(ds)
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                dt.Rows.Add(0, 1, Convert.ToString(ds.Tables(0).Rows(i)("Sr_No")), ds.Tables(0).Rows(i)("Description").ToString(), ds.Tables(0).Rows(i)("Qty").ToString())
            Next

            dgComplem.DataSource = dt

            da.Dispose()
            ds.Dispose()
            con1.Close()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub GvQuotationSearch_Bind()
        
        Try

            Dim criteria As String
            criteria = " and "



            If txtSearchEnQ.Text.Trim() <> "" Then
                criteria = criteria + " Enq_No like '%" + txtSearchEnQ.Text + "%' and"
            End If

            If txtSearchName.Text.Trim() <> "" Then
                criteria = criteria + " Name like '%" + txtSearchName.Text + "%' and"
            End If

            If txtSQuationType.Text.Trim() <> "" Then
                criteria = criteria + " Quatation_Type = '" + txtSQuationType.Text + "' and"
            End If

            If criteria = " and " Then
                criteria = ""
            End If

            If (criteria.Length > 0) Then
                criteria = criteria.ToString().Substring(0, criteria.Length - 3)
            End If

            Dim cmd As New SqlCommand
            Dim dt As New DataTable
            oDataAccess = New DataAccess

            cmd.CommandText = "SP_Get_Quotation"
            cmd.CommandType = CommandType.StoredProcedure


            cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria

            dt = oDataAccess.ExecuteDataTable(cmd)

            GvCategorySearch.DataSource = dt
            Dim tt As Int32
            tt = GvCategorySearch.Rows.Count()
            txtTotalRecord.Text = tt.ToString()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
       
    End Sub
    Public Sub GetTechnicalData(ByVal Langid As Integer)
        Dim desg As String

        'txtContentsys1.AutoCompleteCustomSource.Clear()
        'If (txtCapacity1.Text.Trim() <> "") Then
        '    desg = "select * from Category_Master where Capacity='" & txtCapacity1.Text & "' and MainCategory ='System 1' and LanguageId = " & Langid & " ORDER BY SNo"
        '    da = New SqlDataAdapter(desg, con1)
        '    ds = New DataSet()
        '    da.Fill(ds)
        '    For Each dr1 As DataRow In ds.Tables(0).Rows
        '        txtContentsys1.AutoCompleteCustomSource.Add(dr1.Item("Category").ToString())

        '    Next
        '    da.Dispose()
        '    ds.Dispose()

        'End If


    End Sub

    Private Sub Gv_GetAnnex2EditData()

        Try



            Dim str1 As String
            Dim da1232 As New SqlDataAdapter
            Dim ds1232 As New DataSet
            Dim dt122 As New DataTable

            dt122.Columns.Add("Remove", GetType(Boolean))
            dt122.Columns.Add("Select", GetType(Boolean))
            dt122.Columns.Add("SrNo", GetType(String))
            dt122.Columns.Add("Description", GetType(String))
            dt122.Columns.Add("Remarks", GetType(String))
            str1 = "select Sr_No,Description,Remarks,IsSelected from Annexure2data where Fk_CompanyID= " & CompanyId & ""
            da1232 = New SqlDataAdapter(str1, con1)
            ds1232 = New DataSet()
            da1232.Fill(ds1232)

            For S1 = 0 To ds1232.Tables(0).Rows.Count - 1

                Dim imagestatus As Int16
                imagestatus = 0
                If ds1232.Tables(0).Rows(S1)("IsSelected") = "Yes" Then
                    imagestatus = 1
                End If
                dt122.Rows.Add(0, imagestatus, ds1232.Tables(0).Rows(S1)("Sr_No").ToString(), ds1232.Tables(0).Rows(S1)("Description").ToString(), ds1232.Tables(0).Rows(S1)("Remarks").ToString())
            Next
            gvanne2.DataSource = dt122
            da1232.Dispose()
            dt122.Dispose()
            ds1232.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Gv_GetAnnexEditData()
        Dim str1 As String
        Dim da1231 As New SqlDataAdapter
        Dim ds1231 As New DataSet
        Dim dt121 As New DataTable

        dt121.Columns.Add("Remove", GetType(Boolean))
        dt121.Columns.Add("Select", GetType(Boolean))
        dt121.Columns.Add("SrNo", GetType(Int32))
        dt121.Columns.Add("Description", GetType(String))
        dt121.Columns.Add("Remarks", GetType(String))
        str1 = "select Sr_No,Description,Remarks,IsSelected,Qtype from AnnexureData where Fk_CompanyID= " & CompanyId & ""
        da1231 = New SqlDataAdapter(str1, con1)
        ds1231 = New DataSet()
        da1231.Fill(ds1231)

        For S1 = 0 To ds1231.Tables(0).Rows.Count - 1

            Dim imagestatus As Int16
            imagestatus = 0
            If ds1231.Tables(0).Rows(S1)("IsSelected") = "Yes" Then
                imagestatus = 1
            End If
            txtQuatType.Text = ds1231.Tables(0).Rows(S1)("Qtype").ToString()
            dt121.Rows.Add(0, imagestatus, ds1231.Tables(0).Rows(S1)("Sr_No").ToString(), ds1231.Tables(0).Rows(S1)("Description").ToString(), ds1231.Tables(0).Rows(S1)("Remarks").ToString())
        Next
        If dt121.Rows.Count > 0 Then
            '    Dim DView As New DataView(ds.Tables(0).DefaultView.Sort)
            '    DView.Sort = "SrNo DESC"
            dt121.DefaultView.Sort = "SrNo ASC"
        End If
        GvAnexture.DataSource = dt121
        da1231.Dispose()
        dt121.Dispose()
        ds1231.Dispose()

    End Sub
    Public Sub GetAnexData(ByVal QuatTypes As String, ByVal PlantNo As String, ByVal Model As String)
        Try

            dt = New DataTable()

            dt.Columns.Add("Remove", GetType(Boolean))
            dt.Columns.Add("Select", GetType(Boolean))
            dt.Columns.Add("SrNo", GetType(Int32))
            dt.Columns.Add("Description", GetType(String))
            dt.Columns.Add("Remarks", GetType(String))


            Dim str As String
            str = "select Sr_No,Description,Remarks from ANNEXURE1 where Qtype = '" + QuatTypes.Trim() + "' and Plant = '" + PlantNo.Trim() + "' and Model = '" + Model.Trim() + "' order by Sr_No ASC"
            da = New SqlDataAdapter(str, con1)
            ds = New DataSet()
            da.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                '    Dim DView As New DataView(ds.Tables(0).DefaultView.Sort)
                '    DView.Sort = "SrNo DESC"
                ds.Tables(0).DefaultView.Sort = "Sr_No ASC"
            End If

            Dim dt123 As New DataTable
            dt123.Columns.Add("Remove", GetType(Boolean))
            dt123.Columns.Add("Select", GetType(Boolean))
            dt123.Columns.Add("SrNo", GetType(Int32))
            dt123.Columns.Add("Description", GetType(String))
            dt123.Columns.Add("Remarks", GetType(String))

            If txtAnne1Default.Text.Trim() <> "" Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If Convert.ToInt32(txtAnne1Default.Text) > i Then
                        dt.Rows.Add(0, 1, Convert.ToInt32(ds.Tables(0).Rows(i)("Sr_No")), ds.Tables(0).Rows(i)("Description").ToString(), ds.Tables(0).Rows(i)("Remarks").ToString())
                    Else
                        dt.Rows.Add(0, 0, Convert.ToInt32(ds.Tables(0).Rows(i)("Sr_No")), ds.Tables(0).Rows(i)("Description").ToString(), ds.Tables(0).Rows(i)("Remarks").ToString())
                    End If

                Next
            End If
            If dt.Rows.Count > 0 Then
                Dim DView As New DataView(dt)
                DView.Sort = "SrNo ASC"
                '' dt.DefaultView.Sort = "SrNo ASC"
            End If

            If txtAnne1Default.Text.Trim() <> "" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Convert.ToInt32(txtAnne1Default.Text) > i Then
                        dt123.Rows.Add(0, 1, Convert.ToInt32(dt.Rows(i)("SrNo")), dt.Rows(i)("Description").ToString(), dt.Rows(i)("Remarks").ToString())
                    Else
                        dt123.Rows.Add(0, 0, Convert.ToInt32(dt.Rows(i)("SrNo")), dt.Rows(i)("Description").ToString(), dt.Rows(i)("Remarks").ToString())
                    End If

                Next
            End If
            If dt123.Rows.Count > 0 Then
                '    Dim DView As New DataView(ds.Tables(0).DefaultView.Sort)
                '    DView.Sort = "SrNo DESC"
                dt123.DefaultView.Sort = "SrNo ASC"
            End If
            GvAnexture.DataSource = dt

            da.Dispose()
            ds.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub

    Public Sub GetANN2()

        dt = New DataTable()

        dt.Columns.Add("Remove", GetType(Boolean))
        dt.Columns.Add("Select", GetType(Boolean))
        dt.Columns.Add("SrNo", GetType(String))
        dt.Columns.Add("Description", GetType(String))
        dt.Columns.Add("Remarks", GetType(String))


        Dim str As String
        str = "select Sr_No,Description,Remarks from ANNEXURE2"
        da = New SqlDataAdapter(str, con1)
        ds = New DataSet()
        da.Fill(ds)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            dt.Rows.Add(0, 1, Convert.ToString(ds.Tables(0).Rows(i)("Sr_No")), Convert.ToString(ds.Tables(0).Rows(i)("Description")), Convert.ToString(ds.Tables(0).Rows(i)("Remarks")))
        Next

        gvanne2.DataSource = dt
        da.Dispose()
        ds.Dispose()



    End Sub

    Public Sub GvGetOnlyAnn4()
        dt = New DataTable()
        dt.Columns.Add("Remove", GetType(Boolean))
        dt.Columns.Add("Select", GetType(Boolean))
        dt.Columns.Add("SrNo", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Columns.Add("Description", GetType(String))

        str = "select Sr_No,T_Name,Description from ANNEXURE4"
        da = New SqlDataAdapter(str, con1)
        ds = New DataSet()
        da.Fill(ds)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            dt.Rows.Add(0, 1, Convert.ToString(ds.Tables(0).Rows(i)("Sr_No")), ds.Tables(0).Rows(i)("T_Name").ToString(), ds.Tables(0).Rows(i)("Description").ToString())
        Next

        gvAnne4.DataSource = dt
        da.Dispose()
        ds.Dispose()

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
    Public Sub GetKind_SubData()
        Try
            con1.Close()
        Catch ex As Exception

        End Try

        con1.Open()
        Dim enqtype As String
        enqtype = "select distinct [Ref] as Referenceno ,KindAtt,Subject,Name,QType,Quot_Type,Buss_Excecutive,Buss_Name,Enq_No  from Quotation_Master"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtRef.AutoCompleteCustomSource.Add(dr1.Item("Referenceno").ToString())
            txtSearchName.AutoCompleteCustomSource.Add(dr1.Item("Name").ToString())
            txtSearchEnQ.AutoCompleteCustomSource.Add(dr1.Item("Enq_No").ToString())

            '   txtRef.AutoCompleteCustomSource.Add(dr1.Item("Referenceno").ToString())
        Next
        da.Dispose()
        ds.Dispose()

        enqtype = "select Company_Master.Pk_CompanyID,Quotation_Master.Ref as referenceNo,Company_Master.Company_Name from Company_Master INNER JOIN Quotation_Master ON Quotation_Master.Pk_QuotationID = Company_Master.FK_QuatationID"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtCompnaysearch.AutoCompleteCustomSource.Add(dr1.Item("Company_Name").ToString())
        Next
        da.Dispose()
        ds.Dispose()

        enqtype = "select Plant,Model,Qtype,Description,Remarks from ANNEXURE1"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtQuatType.AutoCompleteCustomSource.Add(dr1.Item("Qtype").ToString())
            txtSQuationType.AutoCompleteCustomSource.Add(dr1.Item("Qtype").ToString())
        Next
        da.Dispose()
        ds.Dispose()


        con1.Close()
    End Sub

    Private Sub btnSave1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave1.Click

        Dim cmd(5) As SqlCommand
        Dim curIndex As Integer
        oDataAccess = New DataAccess
        Dim iRowAffected As Integer = 0

        Try
            If Quatationid <> 0 Then

                ''Insert Company Master

                If txtName.Text = "" Then
                    MessageBox.Show("Companyname cannot be blank")
                    Exit Sub
                End If

                cmd(0) = New SqlCommand

                cmd(0).CommandText = "SP_Insert_Update_Company_Master"
                cmd(0).CommandType = CommandType.StoredProcedure

                Dim oPara As SqlParameter
                oPara = New SqlParameter()
                oPara.ParameterName = "@Pk_CompanyID"
                If btnSave1.Text = "Update" Then
                    oPara.Value = CompanyId
                Else
                    oPara.Value = 0
                End If

                oPara.Direction = ParameterDirection.InputOutput
                oPara.SqlDbType = SqlDbType.BigInt

                cmd(0).Parameters.Add(oPara)

                cmd(0).Parameters.Add("@Company_Name", SqlDbType.VarChar).Value = txtName.Text
                cmd(0).Parameters.Add("@Address", SqlDbType.VarChar).Value = txtAddress.Text
                cmd(0).Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtPhoneNO.Text
                cmd(0).Parameters.Add("@EmailId", SqlDbType.VarChar).Value = txtEmailAddress.Text
                cmd(0).Parameters.Add("@Plant", SqlDbType.VarChar).Value = txtPlantNo.Text
                cmd(0).Parameters.Add("@Capacity", SqlDbType.VarChar).Value = txtCapacity1.Text
                cmd(0).Parameters.Add("@Model", SqlDbType.VarChar).Value = txtModel.Text
                cmd(0).Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = Class1.SetDate(txtDate.Text)
                cmd(0).Parameters.Add("@FK_QuatationID", SqlDbType.VarChar).Value = Quatationid
                cmd(0).Parameters.Add("@Sec_Name", SqlDbType.VarChar).Value = txtName2.Text

                ''Delete Data

                cmd(1) = New SqlCommand
                cmd(1).CommandText = "SP_Delete_AnnexureData"
                cmd(1).CommandType = CommandType.StoredProcedure
                cmd(1).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"

                cmd(2) = New SqlCommand
                cmd(2).CommandText = "SP_Delete_Annexure2data"
                cmd(2).CommandType = CommandType.StoredProcedure
                cmd(2).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"

                cmd(3) = New SqlCommand
                cmd(3).CommandText = "SP_Delete_ComplementryData"
                cmd(3).CommandType = CommandType.StoredProcedure
                cmd(3).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"

                cmd(4) = New SqlCommand
                cmd(4).CommandText = "SP_Delete_ANNEXURE4Data"
                cmd(4).CommandType = CommandType.StoredProcedure
                cmd(4).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"

                cmd(5) = New SqlCommand
                cmd(5).CommandText = "SP_Delete_ChequeDetail"
                cmd(5).CommandType = CommandType.StoredProcedure
                cmd(5).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"


                ''Insert Annexuredata 

                For i As Integer = 0 To GvAnexture.Rows.Count - 1

                    Dim RemoveStatus As Boolean = CBool(GvAnexture.Rows(i).Cells(0).Value)
                    If RemoveStatus Then
                    Else
                        ReDim Preserve cmd(UBound(cmd) + 1)
                        curIndex = UBound(cmd)

                        Dim status As String
                        Dim selectStatus As Boolean = CBool(GvAnexture.Rows(i).Cells(1).Value)
                        status = ""

                        If selectStatus Then
                            status = "Yes"
                        Else
                            status = "No"
                        End If


                        cmd(curIndex) = New SqlCommand

                        cmd(curIndex).CommandText = "SP_Insert_Annexuredata"
                        cmd(curIndex).CommandType = CommandType.StoredProcedure
                        cmd(curIndex).Parameters.Add("@Pk_ID", SqlDbType.BigInt).Value = 0
                        cmd(curIndex).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"
                        cmd(curIndex).Parameters.Add("@Sr_No", SqlDbType.VarChar).Value = GvAnexture.Rows(i).Cells(2).Value.ToString()
                        cmd(curIndex).Parameters.Add("@Description", SqlDbType.VarChar).Value = GvAnexture.Rows(i).Cells(3).Value.ToString()
                        cmd(curIndex).Parameters.Add("@Remarks", SqlDbType.VarChar).Value = GvAnexture.Rows(i).Cells(4).Value.ToString()
                        cmd(curIndex).Parameters.Add("@Qtype", SqlDbType.VarChar).Value = txtQuatType.Text.Trim()
                        cmd(curIndex).Parameters.Add("@Plant", SqlDbType.VarChar).Value = txtPlantNo.Text.Trim()
                        cmd(curIndex).Parameters.Add("@Model", SqlDbType.VarChar).Value = txtModel.Text
                        cmd(curIndex).Parameters.Add("@IsSelected", SqlDbType.VarChar).Value = status

                    End If
                Next

                ''Insert Annexure2data

                For i As Integer = 0 To gvanne2.Rows.Count - 1

                    Dim RemoveStatus As Boolean = CBool(gvanne2.Rows(i).Cells(0).Value)
                    If RemoveStatus Then
                    Else
                        ReDim Preserve cmd(UBound(cmd) + 1)
                        curIndex = UBound(cmd)

                        Dim status As String
                        Dim selectStatus As Boolean = CBool(gvanne2.Rows(i).Cells(1).Value)
                        status = ""

                        If selectStatus Then
                            status = "Yes"
                        Else
                            status = "No"
                        End If

                        cmd(curIndex) = New SqlCommand
                        cmd(curIndex).CommandText = "SP_Insert_Annexure2data"
                        cmd(curIndex).CommandType = CommandType.StoredProcedure
                        cmd(curIndex).Parameters.Add("@Pk_ID", SqlDbType.BigInt).Value = 0
                        cmd(curIndex).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"
                        cmd(curIndex).Parameters.Add("@Sr_No", SqlDbType.VarChar).Value = gvanne2.Rows(i).Cells(2).Value.ToString()
                        cmd(curIndex).Parameters.Add("@Description", SqlDbType.VarChar).Value = gvanne2.Rows(i).Cells(3).Value.ToString()
                        cmd(curIndex).Parameters.Add("@Remarks", SqlDbType.VarChar).Value = gvanne2.Rows(i).Cells(4).Value.ToString()
                        cmd(curIndex).Parameters.Add("@IsSelected", SqlDbType.VarChar).Value = status

                    End If
                Next

                'Insert ComplementryData
                For i As Integer = 0 To dgComplem.Rows.Count - 1

                  
                    Dim RemoveStatus As Boolean = CBool(dgComplem.Rows(i).Cells(0).Value)
                    If RemoveStatus Then
                    Else
                        ReDim Preserve cmd(UBound(cmd) + 1)
                        curIndex = UBound(cmd)

                        Dim status As String
                        Dim selectStatus As Boolean = CBool(dgComplem.Rows(i).Cells(1).Value)
                        status = ""

                        If selectStatus Then
                            status = "Yes"
                        Else
                            status = "No"
                        End If

                        cmd(curIndex) = New SqlCommand
                        cmd(curIndex).CommandText = "SP_Insert_ComplementryData"
                        cmd(curIndex).CommandType = CommandType.StoredProcedure

                        cmd(curIndex).Parameters.Add("@Pk_ID", SqlDbType.BigInt).Value = 0
                        cmd(curIndex).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"
                        cmd(curIndex).Parameters.Add("@Sr_No", SqlDbType.VarChar).Value = dgComplem.Rows(i).Cells(2).Value.ToString()
                        cmd(curIndex).Parameters.Add("@Description", SqlDbType.VarChar).Value = dgComplem.Rows(i).Cells(3).Value.ToString()
                        cmd(curIndex).Parameters.Add("@Qty", SqlDbType.VarChar).Value = dgComplem.Rows(i).Cells(4).Value.ToString()
                        cmd(curIndex).Parameters.Add("@IsSelected", SqlDbType.VarChar).Value = status
                    End If
                Next

                ''Insert ANNEXURE4Data

                For i As Integer = 0 To gvAnne4.Rows.Count - 1
                    Dim RemoveStatus As Boolean = CBool(gvAnne4.Rows(i).Cells(0).Value)
                    If RemoveStatus Then
                    Else
                        ReDim Preserve cmd(UBound(cmd) + 1)
                        curIndex = UBound(cmd)
                        Dim status As String
                        Dim selectStatus As Boolean = CBool(gvAnne4.Rows(i).Cells(1).Value)
                        status = ""

                        If selectStatus Then
                            status = "Yes"
                        Else
                            status = "No"
                        End If


                        cmd(curIndex) = New SqlCommand
                        cmd(curIndex).CommandText = "SP_Insert_ANNEXURE4Data"
                        cmd(curIndex).CommandType = CommandType.StoredProcedure

                        cmd(curIndex).Parameters.Add("@Pk_ID", SqlDbType.BigInt).Value = 0
                        cmd(curIndex).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"
                        cmd(curIndex).Parameters.Add("@Sr_No", SqlDbType.VarChar).Value = gvAnne4.Rows(i).Cells(2).Value.ToString()
                        cmd(curIndex).Parameters.Add("@T_Name", SqlDbType.VarChar).Value = gvAnne4.Rows(i).Cells(3).Value.ToString()
                        cmd(curIndex).Parameters.Add("@Description", SqlDbType.VarChar).Value = gvAnne4.Rows(i).Cells(4).Value.ToString()
                        cmd(curIndex).Parameters.Add("@IsSelected", SqlDbType.VarChar).Value = status
                    End If
                Next

                'Insert ChequeDetail

                ReDim Preserve cmd(UBound(cmd) + 1)
                curIndex = UBound(cmd)

                cmd(curIndex) = New SqlCommand
                cmd(curIndex).CommandText = "SP_Insert_ChequeDetail"
                cmd(curIndex).CommandType = CommandType.StoredProcedure

                cmd(curIndex).Parameters.Add("@Pk_ID", SqlDbType.Int).Value = 0
                cmd(curIndex).Parameters.Add("@Fk_CompanyID", SqlDbType.VarChar).Value = "GetRtnValue"
                cmd(curIndex).Parameters.Add("@Cheque_No", SqlDbType.VarChar).Value = txtchq.Text
                cmd(curIndex).Parameters.Add("@Cheque_date", SqlDbType.VarChar).Value = Class1.SetDate(txtcdate.Text)
                cmd(curIndex).Parameters.Add("@Bank_Name", SqlDbType.VarChar).Value = txtcbank.Text
                cmd(curIndex).Parameters.Add("@Rupees", SqlDbType.VarChar).Value = txtcrace.Text
                cmd(curIndex).Parameters.Add("@Rupees_string", SqlDbType.VarChar).Value = txtrrscmnt.Text
                cmd(curIndex).Parameters.Add("@Objective1", SqlDbType.VarChar).Value = txtrobj.Text
                cmd(curIndex).Parameters.Add("@Objective2", SqlDbType.VarChar).Value = txtrObj2.Text
                cmd(curIndex).Parameters.Add("@Objective3", SqlDbType.VarChar).Value = txtrobj3.Text
                cmd(curIndex).Parameters.Add("@MsCompany_name", SqlDbType.VarChar).Value = txtlname1.Text
                cmd(curIndex).Parameters.Add("@Auth_lname1", SqlDbType.VarChar).Value = txtrname1.Text
                cmd(curIndex).Parameters.Add("@Auth_lname2", SqlDbType.VarChar).Value = txtrname2.Text
                cmd(curIndex).Parameters.Add("@Auth_lname3", SqlDbType.VarChar).Value = txtrname4.Text
                cmd(curIndex).Parameters.Add("@Auth_rname1", SqlDbType.VarChar).Value = txtlname2.Text
                cmd(curIndex).Parameters.Add("@Auth_rname2", SqlDbType.VarChar).Value = txtlname3.Text
                cmd(curIndex).Parameters.Add("@RecieveDate", SqlDbType.VarChar).Value = Class1.SetDate(chkdate.Text)
                cmd(curIndex).Parameters.Add("@Place", SqlDbType.VarChar).Value = txtpalace.Text
                cmd(curIndex).Parameters.Add("@Auth_rname4", SqlDbType.VarChar).Value = txtlname4.Text

                If oDataAccess.ExecuteSP(cmd, cmd(0), "@Pk_CompanyID") = False Then
                    Exit Sub
                End If

                MessageBox.Show("Successfully Submit .....")

                Call btnAddClear_Click(Nothing, Nothing)
                Call BindGvComapnySearch()
            Else
                MessageBox.Show("Please select Quatation from left side Grid")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub

    Public Sub SetClean()
        txtRef.Text = ""
        txtPlantNo.Text = ""
        txtPhoneNO.Text = ""
        txtName.Text = ""
        txtModel.Text = ""
        txtEmailAddress.Text = ""
        txtDate.Text = ""
        txtCapacity2.Text = ""
        txtCapacity1.Text = ""
        txtAddress.Text = ""
        txtQuatType.Text = ""
        txtchq.Text = ".                           ."
        txtcbank.Text = ".                           ."
        txtcrace.Text = ".                           ."
        txtrrscmnt.Text = ".                             ."
        txtrobj.Text = ".                                                                                                                                            ."
        txtrObj2.Text = ".                                                                                                                                            ."
        txtrobj3.Text = ".                                                                                                                                            ."
        txtlname1.Text = ""
        txtlname2.Text = ""
        txtlname3.Text = ""
        txtlname4.Text = ""

        txtName2.Text = ""
    End Sub

    Private Sub txtRef_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRef.Leave
        'GetdataOnID(txtRef.Text.Trim())

    End Sub

    Public Sub GetdataOnID(ByVal strRef As String)
        Try
            con1.Close()
        Catch ex As Exception

        End Try
        con1.Open()
        Dim SqlQuatationId As String
        SqlQuatationId = "select * from Quotation_Master where Pk_QuotationID = " & Convert.ToInt32(strRef) & ""
        da = New SqlDataAdapter(SqlQuatationId, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            Quatationid = Convert.ToInt32(dr1.Item("Pk_QuotationID").ToString())
            txtName2.Text = Convert.ToString(dr1.Item("Name").ToString())
            txtAddress.Text = Convert.ToString(dr1.Item("Address").ToString())
            txtCapacity1.Text = Convert.ToString(dr1.Item("Capacity_Single").ToString())
            If Class1.ConvertNumeric(dr1.Item("Capacity_Multiple").ToString()) = 0 Then
                txtCapacity1.Visible = False
            Else
                txtCapacity1.Visible = True
                txtCapacity1.Text = Convert.ToString(dr1.Item("Capacity_Multiple").ToString())
            End If
            'txtDate.Text = Convert.ToString(dr1.Item("QDate").ToString())
            txtQuatType.Text = dr1("Quatation_Type").ToString()
            If dr1("Capacity_Type").ToString().Trim = "Single" Then

            ElseIf dr1("Capacity_Type").ToString().Trim = "Multiple" Then

            Else

            End If
            'txtName.Text = Quatationid
        Next

        da.Dispose()
        ds.Dispose()
        con1.Close()
    End Sub
    Public Sub BindGvComapnySearch()
        Try

            Dim criteria As String
            criteria = " Where "

            If txtCompnaysearch.Text.Trim() <> "" Then
                criteria = criteria + " Company_Name like '%" + txtCompnaysearch.Text + "%' and"
            End If

            If criteria = " Where " Then
                criteria = ""
            End If

            If (criteria.Length > 0) Then
                criteria = criteria.ToString().Substring(0, criteria.Length - 3)
            End If

            Dim cmd As New SqlCommand
            Dim dt As New DataTable
            oDataAccess = New DataAccess

            cmd.CommandText = "SP_Select_Quotation_Company_Master"
            cmd.CommandType = CommandType.StoredProcedure


            cmd.Parameters.Add("@Fk_CompanyID", SqlDbType.BigInt).Value = 0
            cmd.Parameters.Add("@criteria", SqlDbType.VarChar).Value = criteria

            dt = oDataAccess.ExecuteDataTable(cmd)

            'str = "select Company_Master.Pk_CompanyID,Quotation_Master.Ref as referenceNo,Company_Master.Company_Name from Company_Master INNER JOIN Quotation_Master ON Quotation_Master.Pk_QuotationID = Company_Master.FK_QuatationID where Company_Name ='" + txtCompnaysearch.Text.Trim() + "'"
            'da = New SqlDataAdapter(str, con1)
            'ds = New DataSet()
            'da.Fill(ds)

            GvComapnySearch.DataSource = dt 'ds.Tables(0)
            Dim tt As Int32
            tt = GvComapnySearch.Rows.Count()
            txtTotalAgreeMent.Text = tt.ToString()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub GvCategorySearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub Display(ByVal pass As Integer)
        Dim str As String
        Try
            SetClean()
            Try
                con1.Close()
            Catch ex As Exception

            End Try

            con1.Open()
            If Flag = 1 Then
                str = "select Quotation_Master.Ref as referenceNo,Quotation_Master.Capacity_Single,Quotation_Master.Capacity_Type ,Quotation_Master.Capacity_Multiple  ,Quotation_Master.Capacity_Type,Quotation_Master.Capacity_Single,Quotation_Master.Capacity_Multiple, Quotation_Master.Quatation_Type,Company_Master.Company_Name,Company_Master.Address,Company_Master.ContactNo,Company_Master.EmailID,Company_Master.Plant,Company_Master.Capacity,Company_Master.Model,Company_Master.OrderDate,Company_Master.FK_QuatationID,Company_Master.Sec_Name,Quotation_Master.Enq_No from Quotation_Master  INNER JOIN Company_Master ON Company_Master.FK_QuatationID = Quotation_Master.Pk_QuotationID where Company_Master.Pk_CompanyID=" & pass & ""
                cmd = New SqlCommand(str, con1)
                dr = cmd.ExecuteReader()
                dr.Read()
                txtRef.Text = dr("referenceNo").ToString()
                txtPlantNo.Text = dr("Plant").ToString()
                txtPhoneNO.Text = dr("ContactNo").ToString()
                txtCapacity1.Text = dr("Capacity_Single").ToString()
                txtModel.Text = dr("Model").ToString()
                txtName.Text = dr("Company_Name").ToString()
                txtAddress.Text = dr("Address").ToString()
                txtDate.Text = dr("OrderDate").ToString()
                txtCapacity2.Text = dr("Capacity_Multiple").ToString()
                txtQuatType.Text = dr("Quatation_Type").ToString()
                txtEmailAddress.Text = dr("EmailID").ToString()
                txtName2.Text = dr("Sec_Name").ToString()
                Quatationid = dr("FK_QuatationID").ToString()
                txtEnqNo.Text = dr("Enq_No").ToString()

                If dr("Capacity_Type").ToString().Trim = "Single" Then
                ElseIf dr("Capacity_Type").ToString().Trim = "Multiple" Then
                Else
                End If

                If dr("Capacity_Type").ToString() = "Single" Then
                    RblSingle.Checked = True
                    txtCapacity1.Text = dr("Capacity_Single").ToString()
                End If
                If dr("Capacity_Type").ToString() = "Other" Then
                    RblOther.Checked = True
                    txtCapacity1.Text = dr("Capacity_Single").ToString()

                End If

                If dr("Capacity_Type").ToString() = "Multiple" Then
                    RblMultiple.Checked = True
                    txtCapacity1.Text = dr("Capacity_Single").ToString()
                    txtCapacity2.Text = dr("Capacity_Multiple").ToString()

                End If
                GetdataOnID(Convert.ToString(Quatationid))
                cmd.Dispose()
                dr.Dispose()
                dr.Close()
            ElseIf Flag = 2 Then
                str = "select * from Quotation_Master where Pk_QuotationID=" & pass & ""
                cmd = New SqlCommand(str, con1)
                dr = cmd.ExecuteReader()
                dr.Read()
                txtRef.Text = dr("Ref").ToString()
                txtName2.Text = dr("Name").ToString()
                txtAddress.Text = dr("Address").ToString()
                txtEnqNo.Text = dr("Enq_No").ToString()

                If dr("Capacity_Type").ToString().Trim = "Single" Then
                ElseIf dr("Capacity_Type").ToString().Trim = "Multiple" Then
                Else
                End If

                If dr("Capacity_Type").ToString() = "Single" Then
                    RblSingle.Checked = True
                    txtCapacity1.Text = dr("Capacity_Single").ToString()
                End If
                If dr("Capacity_Type").ToString() = "Other" Then
                    RblOther.Checked = True
                    txtCapacity1.Text = dr("Capacity_Single").ToString()

                End If

                If dr("Capacity_Type").ToString() = "Multiple" Then
                    RblMultiple.Checked = True
                    txtCapacity1.Text = dr("Capacity_Single").ToString()
                    txtCapacity2.Text = dr("Capacity_Multiple").ToString()

                End If

                dr.Dispose()
                dr.Close()
            End If


            con1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub


    Private Sub btnHF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHF.Click
        ' Try
        Try

            Me.UseWaitCursor = True

            DocumentStatus = 0
            FinalDucumetationAnn()
            FinalDucumetationAnn2()
            FinalAnn4()
            FinalComplementary()
            FinalDucumetation()
            Me.UseWaitCursor = False
            SetClean()
            'Catch ex As Exception
            '    MessageBox.Show("Error")
            'End Try

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub FinalAnn4()

        '    Try
        Dim missing As Object = System.Reflection.Missing.Value

        Dim Visible As Object = True

        Dim start1 As Object = 0

        Dim end1 As Object = 0
        Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        Dim objApp As New Word.Application()

        Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)
        objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        Dim oTable3 As Word.Tables = objDoc.Tables
        Dim defaultTableBehavior As [Object] = Type.Missing
        Dim autoFitBehavior As [Object] = Type.Missing

        Dim rng As Word.Range = objDoc.Range(start1, missing)
        oTable3.Add(rng, 1, 3, missing, missing)
        rng.Font.Name = "Arial"
        rng.Borders.Enable = 0
        rng.Font.Size = 8

        Dim newRowa2 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Borders.Enable = 0

        newRowa2.Cells(2).Range.Text = "ANNEXURE-IV"
        newRowa2.Range.Font.Size = 16
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Range.ParagraphFormat.SpaceAfter = 0
        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30

        newRowa2.Range.Font.Bold = True

        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Cells(2).Range.Text = "ORDER ACCEPTANCE"
        newRowa2.Range.Font.Size = 16
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30

        newRowa2.Range.Font.Bold = True




        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)

        newRowa2.Range.Font.Size = 9
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30


        'start1 = objDoc.Tables(1).Range.[End]
        'rng = objDoc.Range(start1, missing)
        Dim newRowa1 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders.Enable = 0

        newRowa1.Cells(1).Range.Text = "TERMS & CONDITIONS:-"
        newRowa1.Cells(2).Range.Text = ""
        newRowa1.Cells(3).Range.Text = ""
        newRowa1.Cells(1).Range.Underline = True

        newRowa1.Range.Font.Size = 12
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        newRowa1.Cells(1).Width = 160
        newRowa1.Cells(2).Width = 160
        newRowa1.Cells(3).Width = 160
        newRowa1.Range.Font.Bold = True
        newRowa1.Range.Font.Bold = True





        For i = 0 To gvAnne4.Rows.Count - 1



            newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa1.Range.Borders.Enable = 0
            newRowa1.Range.Font.Bold = False
            newRowa1.Range.Font.Size = 10
            newRowa1.Cells(1).Width = 50
            newRowa1.Cells(2).Width = 310
            newRowa1.Cells(3).Width = 120
            newRowa1.Cells(1).Range.Underline = False


            If i = 0 Then
                newRowa1.Range.Borders.Enable = 0
            End If
            newRowa1.Cells(1).Range.Text = gvAnne4.Rows(i).Cells(2).Value.ToString()
            newRowa1.Cells(2).Range.Text = gvAnne4.Rows(i).Cells(3).Value.ToString()
            newRowa1.Cells(3).Range.Text = gvAnne4.Rows(i).Cells(4).Value.ToString()
            newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRowa1.Range.Font.Size = 10
            newRowa1.Cells(1).Width = 15
            newRowa1.Cells(2).Width = 115
            newRowa1.Cells(3).Width = 365
            If i = gvAnne4.Rows.Count - 1 Then
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleThickThinSmallGap
            End If
        Next
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders.Enable = 1
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleThickThinSmallGap
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders.Enable = 0
        newRowa1.Range.Font.Bold = True
        newRowa1.Range.Font.Size = 12
        newRowa1.Cells(1).Width = 480
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa1.Cells(1).Range.Text = "ADVANCE"
        newRowa1.Cells(1).Range.Underline = True
        newRowa1.Cells(2).Width = 15
        newRowa1.Cells(3).Width = 15

        Dim oCollapseEnd As Object = Word.WdCollapseDirection.wdCollapseEnd
        Dim strt3 As Object = objDoc.Tables(1).Range.[End]
        Dim oCollapseEnd3 As Object = Word.WdCollapseDirection.wdCollapseEnd
        Dim ran3 As Word.Range = objDoc.Range(strt3, strt3)
        rng = objDoc.Content
        rng.Collapse(oCollapseEnd)
        Dim oTable51 As Word.Table = objDoc.Tables.Add(ran3, 1, 6, missing, missing)
        '    newRow4.HeadingFormat = 3
        oTable51.Range.ParagraphFormat.SpaceAfter = 0

        rng.Font.Size = 10

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Size = 10
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = "CASH/CHEQUE NO. "
        newRowa1.Cells(2).Range.Text = txtchq.Text.Trim()
        newRowa1.Cells(2).Range.Underline = True

        newRowa1.Cells(3).Range.Text = "DT"
        newRowa1.Cells(4).Range.Text = txtcdate.Text.Trim()
        newRowa1.Range.Borders.Enable = 0
        newRowa1.Cells(4).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(4).Range.Underline = True
        newRowa1.Cells(5).Range.Text = "BANK"
        newRowa1.Cells(6).Range.Text = txtcbank.Text.Trim()
        newRowa1.Cells(6).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(6).Range.Underline = True
        newRowa1.Cells(1).Width = 120
        newRowa1.Cells(2).Width = 100
        newRowa1.Cells(3).Width = 30
        newRowa1.Cells(4).Width = 100
        newRowa1.Cells(5).Width = 40
        newRowa1.Cells(6).Width = 110


        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(6).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa1.Range.Font.Size = 10
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = "OF RS."
        newRowa1.Cells(2).Range.Text = txtcrace.Text.Trim()
        newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(2).Range.Underline = True
        newRowa1.Cells(3).Range.Text = "-/("
        newRowa1.Cells(4).Range.Text = txtrrscmnt.Text.Trim()
        newRowa1.Cells(4).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(4).Range.Underline = True
        newRowa1.Cells(5).Range.Text = ")"
        newRowa1.Cells(1).Width = 50
        newRowa1.Cells(2).Width = 100
        newRowa1.Cells(3).Width = 30
        newRowa1.Cells(4).Width = 200
        newRowa1.Cells(5).Width = 100

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = "REMARKS / OBJECTIVE FROM FORM ORDER AGREEMENT BY CLIENT"
        newRowa1.Cells(1).Width = 420
        newRowa1.Cells(2).Width = 15
        newRowa1.Cells(3).Width = 15
        newRowa1.Cells(4).Width = 15
        newRowa1.Cells(5).Width = 15




        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(2).Range.Text = "1."
        newRowa1.Cells(3).Range.Text = txtrobj.Text.Trim()
        newRowa1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(3).Range.Underline = True
        newRowa1.Cells(1).Width = 15
        newRowa1.Cells(2).Width = 20
        newRowa1.Cells(3).Width = 445

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(2).Range.Text = "2."
        newRowa1.Cells(3).Range.Text = txtrObj2.Text.Trim()
        newRowa1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(3).Range.Underline = True
        newRowa1.Cells(1).Width = 15
        newRowa1.Cells(2).Width = 20
        newRowa1.Cells(3).Width = 445

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(2).Range.Text = "3."
        newRowa1.Cells(3).Range.Text = txtrobj3.Text.Trim()
        newRowa1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Cells(3).Range.Underline = True
        newRowa1.Cells(1).Width = 15
        newRowa1.Cells(2).Width = 20
        newRowa1.Cells(3).Width = 445


        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = "Nb: If You Have Any Objective Or Above Order Agreement, Kindly Mentioned On Above Remarks Column."
        newRowa1.Cells(1).Width = 500
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Bold = True

        newRowa1.Cells(1).Range.Underline = False

        newRowa1.Cells(1).Range.Text = "I / We Read The Complete Order Agreement And I Agree With This And Happy To Enter This"
        newRowa1.Cells(1).Width = 480

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = "Agreement. I / We Sign Below As Acceptance Of The Above Order."
        newRowa1.Cells(1).Width = 480

        rng = objDoc.Content
        rng.Collapse(oCollapseEnd)
        Dim oTable52 As Word.Table = objDoc.Tables.Add(ran3, 1, 2, missing, missing)
        '    newRow4.HeadingFormat = 3
        oTable51.Range.ParagraphFormat.SpaceAfter = 0
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Font.Bold = True
        newRowa1.Range.Font.Size = 10

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(2).Range.Underline = False

        newRowa1.Cells(1).Range.Text = "FOR INDIAN ION EXCHANGE"
        newRowa1.Cells(2).Range.Text = txtlname1.Text
        newRowa1.Cells(1).Width = 240
        newRowa1.Cells(2).Width = 240

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(2).Range.Underline = False

        newRowa1.Cells(1).Range.Text = "& CHEMICALS LIMITED"
        newRowa1.Cells(2).Range.Text = ""
        newRowa1.Cells(1).Width = 240
        newRowa1.Cells(2).Width = 240

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = txtrname1.Text
        newRowa1.Cells(2).Range.Text = txtlname2.Text
        newRowa1.Cells(1).Width = 240
        newRowa1.Cells(2).Width = 240
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = txtrname2.Text
        newRowa1.Cells(2).Range.Text = txtlname3.Text
        newRowa1.Cells(1).Width = 240
        newRowa1.Cells(2).Width = 240

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = txtrname4.Text
        newRowa1.Cells(2).Range.Text = txtlname4.Text



        newRowa1.Cells(1).Width = 240
        newRowa1.Cells(2).Width = 240

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders.Enable = 0
        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = "DATE  :" + chkdate.Text
        newRowa1.Cells(1).Width = 480


        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders.Enable = 0
        newRowa1.Cells(1).Range.Underline = False
        newRowa1.Cells(1).Range.Text = "PALACE  : " + txtpalace.Text
        newRowa1.Cells(1).Width = 480
        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg2.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next

            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        Else
            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next



        End If


        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document
        wordDocument1 = New Word.Document
        wordApplication1 = New Word.Application
        Dim formating1 As Object

        objDoc.SaveAs(OrderAgreementTempPath + "\ANNEXURE3.doc")
        Dim paramSourceDocPath1 As Object = OrderAgreementTempPath + "\ANNEXURE3.doc"
        Dim Targets1 As Object = OrderAgreementTempPath + "\ANNEXURE3.pdf"


        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        wordDocument1.Close()
        wordDocument1 = Nothing
        wordApplication1.Quit()
        wordApplication1 = Nothing

        objDoc.Close()
        objDoc = Nothing
        objApp.Quit()
        objApp = Nothing
    End Sub

    Private Sub btnWf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWf.Click
        Me.UseWaitCursor = True

        DocumentStatus = 1
        FinalDucumetationAnn()
        FinalDucumetationAnn2()
        FinalAnn4()

        FinalComplementary()

        FinalDucumetation()
        Me.UseWaitCursor = False
        SetClean()


    End Sub
    Public Sub FinalDucumetationAnn2()

        '    Try
        Dim missing As Object = System.Reflection.Missing.Value

        Dim Visible As Object = True

        Dim start1 As Object = 0

        Dim end1 As Object = 0
        Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        Dim objApp As New Word.Application()

        Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)
        objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        Dim oTable3 As Word.Tables = objDoc.Tables
        Dim defaultTableBehavior As [Object] = Type.Missing
        Dim autoFitBehavior As [Object] = Type.Missing

        Dim rng As Word.Range = objDoc.Range(start1, missing)
        oTable3.Add(rng, 1, 3, missing, missing)
        rng.Font.Name = "Arial"
        rng.Borders.Enable = 0
        rng.Font.Size = 8

        Dim newRowa2 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Borders.Enable = 0

        newRowa2.Cells(2).Range.Text = "ANNEXURE-II"
        newRowa2.Range.Font.Size = 16
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Range.ParagraphFormat.SpaceAfter = 0
        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30

        newRowa2.Range.Font.Bold = True

        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Cells(2).Range.Text = "SCOPE OF SUPPLY - CLIENT"
        newRowa2.Range.Font.Size = 16
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30

        newRowa2.Range.Font.Bold = True




        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)

        newRowa2.Range.Font.Size = 9
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30


        'start1 = objDoc.Tables(1).Range.[End]
        'rng = objDoc.Range(start1, missing)
        Dim newRowa1 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders.Enable = 0

        newRowa1.Cells(1).Range.Text = "Sr.No"
        newRowa1.Cells(2).Range.Text = "Description"
        newRowa1.Cells(3).Range.Text = "Remarks"

        newRowa1.Range.Font.Size = 10
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa1.Cells(1).Width = 50
        newRowa1.Cells(2).Width = 310
        newRowa1.Cells(3).Width = 120
        newRowa1.Range.Font.Bold = True

        Dim srno As Integer
        'newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        'newRowa1.Range.Borders.Enable = 0
        'newRowa1.Range.Font.Bold = False
        'newRowa1.Range.Font.Size = 10
        'newRowa1.Cells(1).Width = 50
        'newRowa1.Cells(2).Width = 310
        'newRowa1.Cells(3).Width = 120
        srno = 1
        Dim count As Integer
        count = 0

        Dim CntFirst As Integer
        CntFirst = 0
        For i = 0 To 25


            If gvanne2.Rows.Count > i Then

                Dim IsTicked As Boolean = CBool(gvanne2.Rows(i).Cells(1).Value)
                If IsTicked Then
                    newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                    newRowa1.Range.Borders.Enable = 0
                    newRowa1.Range.Font.Bold = False
                    newRowa1.Range.Font.Size = 10
                    newRowa1.Cells(1).Width = 50
                    newRowa1.Cells(2).Width = 310
                    newRowa1.Cells(3).Width = 120


                    If CntFirst = 0 Then
                        newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5
                    Else
                        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                        'newRowa1.Range.ParagraphFormat.SpaceAfter = 5.5

                    End If
                    CntFirst = 1
                    newRowa1.Cells(1).Range.Text = srno.ToString()
                    newRowa1.Cells(2).Range.Text = gvanne2.Rows(i).Cells(3).Value.ToString()
                    newRowa1.Cells(3).Range.Text = gvanne2.Rows(i).Cells(4).Value.ToString()
                    newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRowa1.Range.Font.Size = 10
                    newRowa1.Cells(1).Width = 50
                    newRowa1.Cells(2).Width = 310
                    newRowa1.Cells(3).Width = 120
                    srno = srno + 1
                Else
                    count = count + 1
                End If
                'newRowa1.Range.ParagraphFormat.SpaceAfter = 5.5

            ElseIf 25 = i Then

                newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5

            Else
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
                newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5


            End If
            If gvanne2.Rows.Count < i Then
                newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                newRowa1.Range.Borders.Enable = 0
                newRowa1.Range.Font.Bold = False
                newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5

                newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            ElseIf i = 25 Then

                newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5

            Else
            End If

        Next
        For ii = 0 To count
            newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa1.Range.Borders.Enable = 0
            newRowa1.Range.Font.Bold = False
            newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            If count = ii Then
                newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            End If
        Next




        objDoc.Tables(1).Columns.Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = objApp.Options.DefaultBorderLineStyle
            .LineWidth = objApp.Options.DefaultBorderLineWidth
            .Color = objApp.Options.DefaultBorderColor
        End With


        objDoc.Tables(1).Rows.First.Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With
        objDoc.Tables(1).Range.Rows(1).Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With
        objDoc.Tables(1).Range.Rows(2).Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With
        objDoc.Tables(1).Range.Rows(3).Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With
        objDoc.Tables(1).Range.Rows(4).Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With



        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg1.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next

            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        Else



            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr1.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next


            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        End If
        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document
        wordDocument1 = New Word.Document
        wordApplication1 = New Word.Application
        Dim formating1 As Object

        objDoc.SaveAs(OrderAgreementTempPath + "\ANNEXURE2.doc")
        Dim paramSourceDocPath1 As Object = OrderAgreementTempPath + "\ANNEXURE2.doc"
        Dim Targets1 As Object = OrderAgreementTempPath + "\ANNEXURE2.pdf"


        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        wordDocument1.Close()
        wordDocument1 = Nothing
        wordApplication1.Quit()
        wordApplication1 = Nothing

        objDoc.Close()
        objDoc = Nothing
        objApp.Quit()
        objApp = Nothing



    End Sub


    Public Sub FinalDucumetationAnn()
        Try
            Dim rpt As New rptOrder1
            Dim dt As New DataTable


            Dim ds As New DataSet
            Dim iRow As Integer = 0
            'newRowa1.Cells(1).Range.Text = "Sr.No"
            'newRowa1.Cells(2).Range.Text = "Description"
            'newRowa1.Cells(3).Range.Text = "Remarks"

            dt.Columns.Add("Sr_No")
            dt.Columns.Add("Description")
            dt.Columns.Add("Remarks")

            

            For i = 0 To GvAnexture.Rows.Count - 1
                Dim dr As DataRow
                dr = dt.NewRow()
                dr("Sr_No") = i
                dr("Description") = GvAnexture.Rows(i).Cells(3).Value
                dr("Remarks") = GvAnexture.Rows(i).Cells(4).Value
                dt.Rows.Add(dr)

            Next

            dt.TableName = "ANNEX-1"
            ds.Tables.Add(dt)

            Class1.WriteXMlFile(ds, "ANNEX-1", "ANNEX-1")

            ''rpt.Load(Application.StartupPath & "\Report\rptOrder1.rpt")

            Dim Targets1 As String = OrderAgreementTempPath + "\ANNEXURE1.pdf"

            ''Create Footer Text 

            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            Dim ListData As DataTable = New DataTable()
            Dim FirstImageColumn As DataColumn = New DataColumn()
            FirstImageColumn.DataType = System.Type.GetType("System.Byte[]")
            FirstImageColumn.ColumnName = "Footer"
            ListData.Columns.Add(FirstImageColumn)

            Dim img = Image.FromFile(appPath + "\OrderFooter1.jpg")
            Dim row = ListData.NewRow()
            Dim ms = New MemoryStream()
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            row("Footer") = ms.ToArray()
            ListData.Rows.Add(row)

            ListData.TableName = "ANNEX-1FooterImage"
            Dim di As New DataSet

            di.Tables.Add(ListData)

            Class1.WriteXMlFile(di, "ANNEX-1FooterImage", "ANNEX-1FooterImage")




            rpt.Database.Tables(0).SetDataSource(dt)
            rpt.Database.Tables(1).SetDataSource(ListData)



            Dim CrExportOptions As ExportOptions
            Dim CrDiskFileDestinationOptions As New  _
            DiskFileDestinationOptions()
            Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
            CrDiskFileDestinationOptions.DiskFileName = Targets1

            CrExportOptions = rpt.ExportOptions
            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            rpt.Export()

            

            ''Dim f As New FrmCommanReportView(rpt)
            ''f.Show()

            Exit Sub
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        
        'Class1.killProcessOnUser()

        ''    Try
        'Dim missing As Object = System.Reflection.Missing.Value

        'Dim Visible As Object = True

        'Dim start1 As Object = 0

        'Dim end1 As Object = 0
        'Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        'Dim objApp As New Word.Application()

        'Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)
        'objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        'Dim oTable3 As Word.Tables = objDoc.Tables
        'Dim defaultTableBehavior As [Object] = Type.Missing
        'Dim autoFitBehavior As [Object] = Type.Missing

        'Dim rng As Word.Range = objDoc.Range(start1, missing)
        'oTable3.Add(rng, 1, 3, missing, missing)
        'rng.Font.Name = "Arial"
        'rng.Borders.Enable = 0
        'rng.Font.Size = 8

        'Dim newRowa2 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        'newRowa2.Range.Borders.Enable = 0

        'newRowa2.Cells(2).Range.Text = "ANNEXURE-I"
        'newRowa2.Range.Font.Size = 16
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Range.ParagraphFormat.SpaceAfter = 0
        'newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        'newRowa2.Cells(2).Width = 420
        'newRowa2.Cells(1).Width = 30
        'newRowa2.Cells(3).Width = 30

        'newRowa2.Range.Font.Bold = True

        'newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        'newRowa2.Cells(2).Range.Text = "SCOPE OF SUPPLY - IIECL"
        'newRowa2.Range.Font.Size = 16
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        'newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        'newRowa2.Cells(2).Width = 420
        'newRowa2.Cells(1).Width = 30
        'newRowa2.Cells(3).Width = 30

        'newRowa2.Range.Font.Bold = True

        'newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)

        'newRowa2.Range.Font.Size = 9
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        'newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        'newRowa2.Cells(2).Width = 420
        'newRowa2.Cells(1).Width = 30
        'newRowa2.Cells(3).Width = 30


        ''start1 = objDoc.Tables(1).Range.[End]
        ''rng = objDoc.Range(start1, missing)
        'Dim newRowa1 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        'newRowa1.Range.Borders.Enable = 0

        'newRowa1.Cells(1).Range.Text = "Sr.No"
        'newRowa1.Cells(2).Range.Text = "Description"
        'newRowa1.Cells(3).Range.Text = "Remarks"

        'newRowa1.Range.Font.Size = 12
        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        'newRowa1.Range.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa1.Range.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa1.Range.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa1.Range.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa1.Range.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        'newRowa1.Range.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        'newRowa1.Cells(1).Width = 50
        'newRowa1.Cells(2).Width = 310
        'newRowa1.Cells(3).Width = 120
        'newRowa1.Range.Font.Bold = True
        'newRowa1.Range.Font.Bold = True
        'newRowa1.Range.Font.Size = 10
        'newRowa1.Cells(1).Width = 50
        'newRowa1.Cells(2).Width = 310
        'newRowa1.Cells(3).Width = 120


        'Dim srNo As Integer
        'srNo = 1

        'Dim count As Integer
        'count = 0
        'Dim CntFirst As Integer
        'CntFirst = 0
        'Dim TotalRowCreated As Integer
        'TotalRowCreated = 0
        'Dim SecondPageSrNoBottomBorder As Integer
        'SecondPageSrNoBottomBorder = 0
        'Dim IsCreatedSecondPage As Integer
        'IsCreatedSecondPage = 0

        'For i = 0 To GvAnexture.Rows.Count - 1


        '    Dim IsTicked As Boolean = CBool(GvAnexture.Rows(i).Cells(1).Value)
        '    If IsTicked Then
        '        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        '        newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5

        '        If SecondPageSrNoBottomBorder = 1 Then
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '        Else
        '            newRowa1.Range.Borders.Enable = 0
        '        End If
        '        newRowa1.Range.Font.Size = 12
        '        newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa1.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa1.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa1.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5

        '        newRowa1.Cells(1).Width = 50
        '        newRowa1.Cells(2).Width = 310
        '        newRowa1.Cells(3).Width = 120
        '        newRowa1.Range.Font.Bold = True
        '        newRowa1.Range.Font.Bold = True
        '        newRowa1.Range.Font.Size = 10
        '        newRowa1.Cells(1).Width = 50
        '        newRowa1.Cells(2).Width = 310
        '        newRowa1.Cells(3).Width = 120
        '        newRowa1.Range.Font.Bold = False
        '        If CntFirst = 0 Then
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5
        '            'ElseIf GvAnexture.Rows.Count - 1 = i Then

        '            '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '        End If

        '        newRowa1.Cells(1).Range.Text = srNo
        '        newRowa1.Cells(2).Range.Text = GvAnexture.Rows(i).Cells(3).Value.ToString()
        '        newRowa1.Cells(3).Range.Text = GvAnexture.Rows(i).Cells(4).Value.ToString()
        '        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        '        newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        srNo = srNo + 1
        '        TotalRowCreated = TotalRowCreated + 1

        '        'If i = 26 Then

        '        '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '        'End If
        '        SecondPageSrNoBottomBorder = 0
        '        If CntFirst = 28 * (IsCreatedSecondPage + 1) Then
        '            If IsCreatedSecondPage > 0 Then
        '                newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        '                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '                newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        '                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '                newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '            End If
        '            IsCreatedSecondPage = IsCreatedSecondPage + 1
        '            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        '            newRowa2.Range.Borders.Enable = 0

        '            newRowa2.Cells(2).Range.Text = "ANNEXURE-I"
        '            newRowa2.Range.Font.Size = 16
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Range.ParagraphFormat.SpaceAfter = 0
        '            newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        '            newRowa2.Cells(2).Width = 420
        '            newRowa2.Cells(1).Width = 30
        '            newRowa2.Cells(3).Width = 30

        '            newRowa2.Range.Font.Bold = True

        '            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        '            newRowa2.Cells(2).Range.Text = "SCOPE OF SUPPLY - IIECL"
        '            newRowa2.Range.Font.Size = 16
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle



        '            newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        '            newRowa2.Cells(2).Width = 420
        '            newRowa2.Cells(1).Width = 30
        '            newRowa2.Cells(3).Width = 30


        '            newRowa2.Range.Font.Bold = True

        '            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)

        '            newRowa2.Range.Font.Size = 9
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        '            newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        '            newRowa2.Cells(2).Width = 420
        '            newRowa2.Cells(1).Width = 30
        '            newRowa2.Cells(3).Width = 30




        '            'start1 = objDoc.Tables(1).Range.[End]
        '            'rng = objDoc.Range(start1, missing)
        '            newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        '            newRowa1.Range.Borders.Enable = 0

        '            newRowa1.Cells(1).Range.Text = "Sr.No"
        '            newRowa1.Cells(2).Range.Text = "Description"
        '            newRowa1.Cells(3).Range.Text = "Remarks"

        '            newRowa1.Range.Font.Size = 12
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '            newRowa1.Range.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '            newRowa1.Range.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        '            newRowa1.Cells(1).Width = 50
        '            newRowa1.Cells(2).Width = 310
        '            newRowa1.Cells(3).Width = 120
        '            newRowa1.Range.Font.Bold = True
        '            newRowa1.Range.Font.Bold = True
        '            newRowa1.Range.Font.Size = 10
        '            newRowa1.Cells(1).Width = 50
        '            newRowa1.Cells(2).Width = 310
        '            newRowa1.Cells(3).Width = 120

        '            SecondPageSrNoBottomBorder = 1
        '        End If
        '        CntFirst = CntFirst + 1
        '        count = count + 1
        '    Else

        '        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '        'newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        '        'newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5

        '    End If


        '    'If GvAnexture.Rows.Count < i Then
        '    '    newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        '    '    TotalRowCreated = TotalRowCreated + 1
        '    '    newRowa1.Range.Borders.Enable = 0
        '    '    newRowa1.Range.Font.Bold = False
        '    '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    'ElseIf i = 26 Then

        '    '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '    'Else
        '    'End If

        'Next
        'Dim CntExtraRows As Integer
        'CntExtraRows = GvAnexture.Rows.Count
        'If IsCreatedSecondPage > 0 Then
        '    CntExtraRows = (28 * (IsCreatedSecondPage + 1)) - count
        'Else
        '    CntExtraRows = 28 - count
        'End If

        'For ii = 0 To CntExtraRows
        '    newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        '    newRowa2.Range.Borders.Enable = 0

        '    newRowa2.Range.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    newRowa2.Range.Cells(2).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    newRowa2.Range.Cells(3).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '    newRowa2.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    newRowa2.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '    newRowa2.Range.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    newRowa2.Range.Cells(2).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    newRowa2.Range.Cells(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '    If CntExtraRows = ii Then
        '        newRowa2.Range.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa2.Range.Cells(2).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        '        newRowa2.Range.Cells(3).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        '    End If
        'Next




        'objDoc.Tables(1).Rows.First.Range.Font.Size = 2
        'objDoc.Tables(1).Rows.First.Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
        '    .LineStyle = Word.WdLineStyle.wdLineStyleNone
        '    .LineWidth = Word.WdLineStyle.wdLineStyleNone

        'End With

        'If DocumentStatus = 0 Then


        '    For Each section As Word.Section In objDoc.Sections
        '        Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
        '        headerRange.Fields.Add(headerRange)
        '        headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg1.jpg")
        '        headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

        '        ' headerRange.Fields.Add(oTable.Rows.Item(33))
        '        '   headerRange.Delete = Word.WdFieldType.wdFieldPage
        '    Next

        'Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
        'Dim newImage = New Bitmap(bmp.Width, bmp.Height)

        'Dim gr = Graphics.FromImage(newImage)
        'gr.DrawImageUnscaled(bmp, 0, 0)

        'Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
        'gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

        'newImage.Save(appPath + "\OrderFooter1.jpg")

        'For Each section As Word.Section In objDoc.Sections
        '    Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
        '    footerrange.Fields.Add(footerrange)
        '    footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
        '    footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        '    footerrange.Font.Bold = True
        'Next
        'Else



        '    For Each section As Word.Section In objDoc.Sections
        '        Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
        '        headerRange.Fields.Add(headerRange)
        '        headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr1.jpg")
        '        headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

        '        ' headerRange.Fields.Add(oTable.Rows.Item(33))
        '        '   headerRange.Delete = Word.WdFieldType.wdFieldPage
        '    Next
        '    Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
        '    Dim newImage = New Bitmap(bmp.Width, bmp.Height)

        '    Dim gr = Graphics.FromImage(newImage)
        '    gr.DrawImageUnscaled(bmp, 0, 0)

        '    Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
        '    gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

        'newImage.Save(appPath + "\OrderFooter1.jpg")

        '    For Each section As Word.Section In objDoc.Sections
        '        Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
        '        footerrange.Fields.Add(footerrange)
        '        footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
        '        footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        '        footerrange.Font.Bold = True
        '    Next
        'End If
        'objDoc.SaveAs(OrderAgreementTempPath + "\ANNEXURE1.doc")



        'Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        'Dim paramMissing1 As Object = Type.Missing
        'Dim wordApplication1 As Word.Application
        'Dim wordDocument1 As Word.Document
        'wordDocument1 = New Word.Document
        'wordApplication1 = New Word.Application
        'Dim formating1 As Object

        'Dim paramSourceDocPath1 As Object = OrderAgreementTempPath + "\ANNEXURE1.doc"
        'Dim Targets1 As Object = OrderAgreementTempPath + "\ANNEXURE1.pdf"

        'formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        'objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        'wordDocument1.Close()
        'wordDocument1 = Nothing
        'wordApplication1.Quit()
        'wordApplication1 = Nothing

        'objDoc.Close()
        'objDoc = Nothing
        'objApp.Quit()
        'objApp = Nothing



    End Sub
    Public Sub FinalComplementary()

        '    Try
        Dim missing As Object = System.Reflection.Missing.Value

        Dim Visible As Object = True

        Dim start1 As Object = 0

        Dim end1 As Object = 0
        Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        Dim objApp As New Word.Application()

        Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)
        objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        Dim oTable3 As Word.Tables = objDoc.Tables
        Dim defaultTableBehavior As [Object] = Type.Missing
        Dim autoFitBehavior As [Object] = Type.Missing

        Dim rng As Word.Range = objDoc.Range(start1, missing)
        oTable3.Add(rng, 1, 3, missing, missing)
        rng.Font.Name = "Arial"
        rng.Borders.Enable = 0
        rng.Font.Size = 8

        Dim newRowa2 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Borders.Enable = 0

        newRowa2.Cells(2).Range.Text = "COMPLIMENTRY"
        newRowa2.Range.Font.Size = 16
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Range.ParagraphFormat.SpaceAfter = 0
        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30

        newRowa2.Range.Font.Bold = True

        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Cells(2).Range.Text = "FROM INDIAN ION EXCHANGE"
        newRowa2.Range.Font.Size = 16
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30

        newRowa2.Range.Font.Bold = True




        newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)

        newRowa2.Range.Font.Size = 9
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(2).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(3).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleNone
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa2.Cells(1).Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle


        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        newRowa2.Cells(2).Width = 420
        newRowa2.Cells(1).Width = 30
        newRowa2.Cells(3).Width = 30


        'start1 = objDoc.Tables(1).Range.[End]
        'rng = objDoc.Range(start1, missing)
        Dim newRowa1 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders.Enable = 0

        newRowa1.Cells(1).Range.Text = "Sr.No"
        newRowa1.Cells(2).Range.Text = "Description"
        newRowa1.Cells(3).Range.Text = "Qty"

        newRowa1.Range.Font.Size = 12
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

        newRowa1.Cells(1).Width = 50
        newRowa1.Cells(2).Width = 310
        newRowa1.Cells(3).Width = 120
        newRowa1.Range.Font.Bold = True
        newRowa1.Range.Font.Bold = True
        newRowa1.Range.Font.Size = 10
        newRowa1.Cells(1).Width = 50
        newRowa1.Cells(2).Width = 310
        newRowa1.Cells(3).Width = 120


        Dim srNo As Integer
        srNo = 1
        Dim flagd As Integer
        flagd = 0
        Dim count As Integer
        count = 0
        Dim CntFirst As Integer
        CntFirst = 0

        For i = 0 To dgComplem.Rows.Count - 1


            Dim IsTicked As Boolean = CBool(dgComplem.Rows(i).Cells(1).Value)
            If IsTicked Then

                If CntFirst = 0 Then
                    newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                    newRowa1.Range.Borders.Enable = 0
                    newRowa1.Range.Font.Bold = False
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5
                    CntFirst = 1
                    'ElseIf GvAnexture.Rows.Count - 1 = i Then

                    '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

                End If


                'newRowa1.Cells(1).Range.Text = srNo

                Dim flag As Integer
                flag = 0

                If dgComplem.Rows(i).Cells(3).Value.ToString().Contains("|") Then

                    Dim strsplited1
                    strsplited1 = dgComplem.Rows(i).Cells(3).Value.ToString().Split("|")

                    For uo = 0 To strsplited1.Length - 1
                        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                        newRowa1.Range.Borders.Enable = 0
                        newRowa1.Range.Font.Bold = False
                        If flag = 0 Then
                            newRowa1.Cells(1).Range.Text = srNo
                            srNo = srNo + 1
                        End If
                        newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5

                        newRowa1.Cells(2).Range.Text = strsplited1(uo).ToString()
                        newRowa1.Cells(3).Range.Text = ""
                        newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                        flag = 1
                        flagd = flagd + 1

                        count = count + 1
                    Next


                Else
                    newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
                    newRowa1.Range.Borders.Enable = 0
                    newRowa1.Range.Font.Bold = False
                    newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5

                    newRowa1.Cells(1).Range.Text = srNo
                    newRowa1.Cells(2).Range.Text = dgComplem.Rows(i).Cells(3).Value.ToString()
                    newRowa1.Cells(3).Range.Text = dgComplem.Rows(i).Cells(4).Value.ToString()
                    newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    srNo = srNo + 1
                    count = count + 1
                End If



            End If

            'ElseIf i = 27 Then

            'newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            'newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            'newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            'Else

            'newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            'newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            'newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone
            'newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
            'newRowa1.Range.ParagraphFormat.SpaceAfter = 6.5

            'If dgComplem.Rows.Count < i Then
            '    newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
            '    newRowa1.Range.Borders.Enable = 0
            '    newRowa1.Range.Font.Bold = False
            '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            'ElseIf i = 27 Then

            '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            '    newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle

            'Else
            'End If

        Next
        count = 27 - count

        For ii = 0 To count - 1
            newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa1.Range.Borders.Enable = 0
            newRowa1.Range.Font.Bold = False
            newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            If ii = count - 1 Then
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
                newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            End If
        Next



        objDoc.Tables(1).Columns.Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = objApp.Options.DefaultBorderLineStyle
            .LineWidth = objApp.Options.DefaultBorderLineWidth
            .Color = objApp.Options.DefaultBorderColor
        End With


        objDoc.Tables(1).Rows.First.Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With
        objDoc.Tables(1).Range.Rows(1).Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With
        objDoc.Tables(1).Range.Rows(2).Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With
        objDoc.Tables(1).Range.Rows(3).Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With
        objDoc.Tables(1).Range.Rows(4).Select()
        With objApp.Selection.Borders(Word.WdBorderType.wdBorderVertical)
            .LineStyle = Word.WdLineStyle.wdLineStyleNone
            .LineWidth = Word.WdLineStyle.wdLineStyleNone
        End With

        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg1.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next

            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        Else



            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr1.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next

            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        End If
        objDoc.SaveAs(OrderAgreementTempPath + "\Compl1.doc")



        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document
        wordDocument1 = New Word.Document
        wordApplication1 = New Word.Application
        Dim formating1 As Object

        Dim paramSourceDocPath1 As Object = OrderAgreementTempPath + "\Compl1.doc"
        Dim Targets1 As Object = OrderAgreementTempPath + "\Compl1.pdf"

        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        wordDocument1.Close()
        wordDocument1 = Nothing
        wordApplication1.Quit()
        wordApplication1 = Nothing

        objDoc.Close()
        objDoc = Nothing
        objApp.Quit()
        objApp = Nothing


    End Sub
    Public Sub PriceSheetHF()
        '    Try
        Dim missing As Object = System.Reflection.Missing.Value

        Dim Visible As Object = True

        Dim start1 As Object = 0

        Dim end1 As Object = 0
        Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        Dim objApp As New Word.Application()

        Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)
        objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        Dim paramSourceDocPath1 As Object = appPath + "\OrderData" + "\" + Convert.ToString(Quatationid) + ".doc"
        Dim Targets1 As Object = appPath + "\OrderData" + "\" + Convert.ToString(Quatationid) + ".pdf"
        objDoc = objApp.Documents.Open(paramSourceDocPath1)

        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg1.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next

            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        Else



            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr1.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        End If
        Dim formating1 As Object
        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


    End Sub


    Public Sub AdvancedPriceSheetHF()
        '    Try
        Dim missing As Object = System.Reflection.Missing.Value

        Dim Visible As Object = True

        Dim start1 As Object = 0

        Dim end1 As Object = 0
        Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        Dim objApp As New Word.Application()

        Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)
        objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        Dim paramSourceDocPath1 As Object = appPath + "\OrderData" + "\SpecialPrice\" + Convert.ToString(Quatationid) + ".doc"
        Dim Targets1 As Object = appPath + "\OrderData" + "\SpecialPrice\" + Convert.ToString(Quatationid) + ".pdf"
        Try
            objDoc = objApp.Documents.Open(paramSourceDocPath1)
        Catch ex As Exception
            flagExistspecialDoc = 1
        End Try

        If flagExistspecialDoc = 0 Then




            If DocumentStatus = 0 Then


                For Each section As Word.Section In objDoc.Sections
                    Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                    headerRange.Fields.Add(headerRange)
                    headerRange.InlineShapes.AddPicture(appPath + "\HederRoimg1.jpg")
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                    ' headerRange.Fields.Add(oTable.Rows.Item(33))
                    '   headerRange.Delete = Word.WdFieldType.wdFieldPage
                Next

                Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
                Dim newImage = New Bitmap(bmp.Width, bmp.Height)

                Dim gr = Graphics.FromImage(newImage)
                gr.DrawImageUnscaled(bmp, 0, 0)

                Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
                gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

                newImage.Save(appPath + "\OrderFooter1.jpg")

                For Each section As Word.Section In objDoc.Sections
                    Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                    footerrange.Fields.Add(footerrange)
                    footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                    footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    footerrange.Font.Bold = True
                Next
            Else

                For Each section As Word.Section In objDoc.Sections
                    Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                    headerRange.Fields.Add(headerRange)
                    headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr1.jpg")
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
                Next
                Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
                Dim newImage = New Bitmap(bmp.Width, bmp.Height)

                Dim gr = Graphics.FromImage(newImage)
                gr.DrawImageUnscaled(bmp, 0, 0)

                Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
                gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

                newImage.Save(appPath + "\OrderFooter1.jpg")

                For Each section As Word.Section In objDoc.Sections
                    Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                    footerrange.Fields.Add(footerrange)
                    footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                    footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                    footerrange.Font.Bold = True
                Next


            End If
            Dim formating1 As Object
            formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
            objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)

        End If
    End Sub
    Public Sub FirstPage()
        '    Try
        Dim missing As Object = System.Reflection.Missing.Value

        Dim Visible As Object = True

        Dim start1 As Object = 0

        Dim end1 As Object = 0
        Dim styleTypeTable As Object = Word.WdStyleType.wdStyleTypeTable
        Dim objApp As New Word.Application()

        Dim objDoc As Word.Document = objApp.Documents.Add(missing, missing, missing, missing)
        objDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4
        Dim oTable3 As Word.Tables = objDoc.Tables
        Dim defaultTableBehavior As [Object] = Type.Missing
        Dim autoFitBehavior As [Object] = Type.Missing

        Dim rng As Word.Range = objDoc.Range(start1, missing)
        oTable3.Add(rng, 3, 3, missing, missing)
        rng.Font.Name = "Times New Roman"
        'start1 = objDoc.Tables(1).Range.[End]
        'rng = objDoc.Range(start1, missing)


        'oTable3.Add(rng, 1, 3, missing, missing)
        'start1 = objDoc.Tables(1).Range.[End]
        'rng = objDoc.Range(start1, missing)


        Dim newRowa1 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.ParagraphFormat.SpaceBefore = 5.5
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleDouble

        newRowa1.Cells(1).Range.Text = txtRef.Text
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa1.Cells(3).Range.Text = "DATE " + Convert.ToDateTime(txtDate.Text).ToShortDateString()
        newRowa1.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        newRowa1.Cells(3).Range.Underline = True
        newRowa1.Cells(1).Range.Underline = True
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        newRowa1.Range.Font.Color = Word.WdColor.wdColorRed
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleDouble



        newRowa1.Range.Font.Size = 10
        newRowa1.Cells(1).Width = 220
        newRowa1.Cells(2).Width = 40
        newRowa1.Cells(3).Width = 220
        newRowa1.Range.Font.Bold = True

        newRowa1 = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        newRowa1.Range.Font.Color = Word.WdColor.wdColorBlack
        newRowa1.Range.ParagraphFormat.SpaceBefore = 0
        newRowa1.Cells(1).Range.Text = ""
        newRowa1.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa1.Cells(2).Range.Text = ""
        newRowa1.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        newRowa1.Cells(2).Range.Underline = True
        newRowa1.Cells(1).Range.Underline = True
        newRowa1.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleDouble


        newRowa1.Range.Font.Size = 10
        newRowa1.Cells(1).Width = 220
        newRowa1.Cells(2).Width = 40
        newRowa1.Cells(3).Width = 220
        newRowa1.Range.Font.Bold = True
        Dim newRowa2 As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        newRowa2.Range.Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        newRowa2.Range.Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        newRowa2.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone


        If txtName.Text.Trim() <> "" Then

            newRowa2.Cells(1).Range.Text = "COMAPANY NAME "
            newRowa2.Cells(2).Range.Text = ":"
            newRowa2.Cells(3).Range.Text = txtName.Text

            newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            newRowa2.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRowa2.Range.Font.Bold = True

            newRowa2.Cells(1).Width = 220
            newRowa2.Cells(2).Width = 40
            newRowa2.Cells(3).Width = 220
            newRowa2.Range.Underline = False
            newRowa2.Range.Font.Size = 10
            newRowa2.Range.Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone

        End If

        If txtAddress.Text.Trim() <> "" Then

            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa2.Cells(1).Range.Text = "ADDRESS"
            newRowa2.Cells(2).Range.Text = ":"
            newRowa2.Cells(3).Range.Text = txtAddress.Text

            newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
            newRowa2.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            newRowa2.Range.Font.Bold = True

            newRowa2.Cells(1).Width = 220
            newRowa2.Cells(2).Width = 40
            newRowa2.Cells(3).Width = 220
            newRowa2.Cells(3).Height = 80

            newRowa2.Cells(1).Range.Underline = False
            newRowa2.Range.Font.Size = 10


        End If

        If txtName2.Text.Trim() <> "" Then

            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa2.Cells(3).Height = 20
            newRowa2.Cells(1).Range.Text = "CONTACT PERSON"
            newRowa2.Cells(2).Range.Text = ":"
            newRowa2.Cells(3).Range.Text = txtName2.Text

        End If

        If txtname21.Text.Trim() <> "" Then

            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa2.Cells(3).Height = 18
            newRowa2.Cells(1).Range.Text = ""
            newRowa2.Cells(2).Range.Text = ""
            newRowa2.Cells(3).Range.Text = txtname21.Text

        End If

        If txtname22.Text.Trim() <> "" Then

            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa2.Cells(3).Height = 18
            newRowa2.Cells(1).Range.Text = ""
            newRowa2.Cells(2).Range.Text = ""
            newRowa2.Cells(3).Range.Text = txtname22.Text

        End If

        If txtPhoneNO.Text.Trim() <> "" Then
            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa2.Cells(1).Range.Text = "CONTACT NO."
            newRowa2.Cells(2).Range.Text = ":"
            newRowa2.Cells(3).Range.Text = txtPhoneNO.Text
        End If
        If txtEmailAddress.Text.Trim() <> "" Then
            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa2.Cells(1).Range.Text = "EMAIL ADDRESS."
            newRowa2.Cells(2).Range.Text = ":"
            newRowa2.Cells(3).Range.Text = txtEmailAddress.Text

        End If
        If txtPlantNo.Text.Trim() <> "" Then
            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa2.Cells(1).Range.Text = "PLANT."
            newRowa2.Cells(2).Range.Text = ":"
            newRowa2.Cells(3).Range.Text = txtPlantNo.Text

        End If
        If txtCapacity1.Text.Trim() <> "" Then
            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa2.Cells(1).Range.Text = "CAPACITY."
            newRowa2.Cells(2).Range.Text = ":"
            newRowa2.Cells(3).Range.Text = txtCapacity1.Text

        End If

        If txtModel.Text.Trim() <> "" Then
            newRowa2 = objDoc.Tables(1).Rows.Add(Type.Missing)
            newRowa2.Cells(1).Range.Text = "MODEL."
            newRowa2.Cells(2).Range.Text = ":"
            newRowa2.Cells(3).Range.Text = txtModel.Text
            newRowa2.Range.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleDouble

        End If

        'start1 = objDoc.Tables(1).Range.[End]
        'rng = objDoc.Range(start1, missing)
        'oTable3.Add(rng, 1, 1, missing, missing)

        Dim NewRowFooter As Word.Row = objDoc.Tables(1).Rows.Add(Type.Missing)
        NewRowFooter.Borders.Enable = 0
        NewRowFooter.Cells(1).Range.Font.Name = "Arial"
        NewRowFooter.Cells(1).Range.Font.Color = Word.WdColor.wdColorRed
        NewRowFooter.Range.Font.Bold = 1
        NewRowFooter.Range.Font.Bold = True
        NewRowFooter.Cells(1).Width = 480
        NewRowFooter.Cells(1).Range.Underline = False
        NewRowFooter.Range.Font.Size = 25
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleDouble

        NewRowFooter = objDoc.Tables(1).Rows.Add(Type.Missing)
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderLeft).LineStyle = Word.WdLineStyle.wdLineStyleDouble
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone

        NewRowFooter.Cells(1).Range.Text = "ORDER AGREEMENT"
        NewRowFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone

        NewRowFooter = objDoc.Tables(1).Rows.Add(Type.Missing)
        NewRowFooter.Cells(1).Range.Text = "FOR"
        NewRowFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone


        NewRowFooter = objDoc.Tables(1).Rows.Add(Type.Missing)
        NewRowFooter.Cells(1).Range.Text = txtPlantNo.Text
        NewRowFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone


        NewRowFooter = objDoc.Tables(1).Rows.Add(Type.Missing)
        NewRowFooter.Cells(1).Range.Text = ""
        NewRowFooter.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderTop).LineStyle = Word.WdLineStyle.wdLineStyleNone
        NewRowFooter.Cells(1).Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleDouble


        newRowa2.Cells(1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa2.Cells(2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight
        newRowa2.Cells(3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        newRowa2.Range.Font.Bold = True

        newRowa2.Cells(1).Width = 220
        newRowa2.Cells(2).Width = 40
        newRowa2.Cells(3).Width = 220
        newRowa2.Cells(1).Range.Underline = False
        newRowa2.Range.Font.Size = 10


        If DocumentStatus = 0 Then


            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\HeaderRo.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next

            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        Else



            For Each section As Word.Section In objDoc.Sections
                Dim headerRange As Word.Range = section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                headerRange.Fields.Add(headerRange)
                headerRange.InlineShapes.AddPicture(appPath + "\Blankhdr1.jpg")
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight

                ' headerRange.Fields.Add(oTable.Rows.Item(33))
                '   headerRange.Delete = Word.WdFieldType.wdFieldPage
            Next
            Dim bmp = Bitmap.FromFile(appPath + "\OrderFooter.jpg")
            Dim newImage = New Bitmap(bmp.Width, bmp.Height)

            Dim gr = Graphics.FromImage(newImage)
            gr.DrawImageUnscaled(bmp, 0, 0)

            Dim drawFont As New Font("Times new Roman", 11, FontStyle.Bold)
            gr.DrawString(txtName.Text, drawFont, Brushes.Black, New RectangleF(480, 20, 200, 0))

            newImage.Save(appPath + "\OrderFooter1.jpg")

            For Each section As Word.Section In objDoc.Sections
                Dim footerrange As Word.Range = section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerrange.Fields.Add(footerrange)
                footerrange.InlineShapes.AddPicture(appPath + "\OrderFooter1.jpg")
                footerrange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                footerrange.Font.Bold = True
            Next
        End If






        objDoc.SaveAs(OrderAgreementTempPath + "\Order1.doc")

        Dim paramSourceDocPath1 As Object = OrderAgreementTempPath + "\Order1.doc"
        Dim Targets1 As Object = OrderAgreementTempPath + "\Order1.pdf"


        Dim formating1 As Object
        formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        objDoc.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        objDoc.Close()
        objDoc = Nothing
        objApp.Quit()
        objApp = Nothing

    End Sub


    Public Sub FinalDucumetation()



        Dim exportFormat1 As Word.WdExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF
        Dim paramMissing1 As Object = Type.Missing
        Dim wordApplication1 As Word.Application
        Dim wordDocument1 As Word.Document
        wordDocument1 = New Word.Document
        wordApplication1 = New Word.Application
        FirstPage()
        PriceSheetHF()
        If flagExistspecialDoc = 0 Then
            AdvancedPriceSheetHF()
        End If
        'paramSourceDocPath1 = appPath + "\OA_2AND3.doc"
        'Targets1 = QPath + "\OA_2AND3.pdf"

        'wordDocument1 = wordApplication1.Documents.Open(paramSourceDocPath1)

        'formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        'wordDocument1.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)

        'paramSourceDocPath1 = QPath + "\OA_5AND6.doc"
        'Targets1 = QPath + "\OA_5AND6.pdf"

        'wordDocument1 = wordApplication1.Documents.Open(paramSourceDocPath1)

        'formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        'wordDocument1.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)


        'paramSourceDocPath1 = QPath + "\" + Quatationid.ToString() + ".doc"
        'Targets1 = QPath + "\" + Quatationid.ToString() + ".pdf"


        'wordDocument1 = wordApplication1.Documents.Open(paramSourceDocPath1)

        'formating1 = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF
        'wordDocument1.SaveAs(Targets1, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)

        Dim fullpath12 As String
        Dim fullpath12Price As String

        ' fullpath12 = QPath + "\" + txtQoutNo.Text.Trim() + "-" + txtName.Text.Trim() + "-" + Convert.ToDateTime(txtDate.Text.Trim()).Day.ToString() + "-" + Convert.ToDateTime(txtDate.Text.Trim()).Month.ToString() + "-" + Convert.ToDateTime(txtDate.Text.Trim()).Year.ToString() + ".pdf"
        Dim str3333 As String

        If txtDate.Text.Contains("/") Then
            If (RblSingle.Checked = True Or RblOther.Checked = True) Then

                str3333 = txtCapacity1.Text + "(LPH) -" + txtDate.Text.Replace("/", "-") + "OA"
            Else
                str3333 = txtCapacity1.Text + "(LPH) -" + txtCapacity2.Text + "(LPH) -" + txtDate.Text.Replace("/", "-") + "OA"

            End If

        Else
            If (RblSingle.Checked = True Or RblOther.Checked = True) Then
                str3333 = txtCapacity1.Text + "(LPH) -" + txtDate.Text + "OA"
            Else
                str3333 = txtCapacity1.Text + "(LPH) -" + txtCapacity2.Text + "(LPH) -" + txtDate.Text + "OA"
            End If
        End If

        fullpath12 = OrderAgreementPath + "\" + txtEnqNo.Text.Trim() + "-" + txtName.Text.Trim() + "-" + str3333 + ".pdf"



        ReDim str(5)
        Dim il As Integer
        il = 0
        wordDocument1.Close()
        wordDocument1 = Nothing
        wordApplication1.Quit()
        wordApplication1 = Nothing
        Dim _pdfforge As New PDF.PDF
        Dim _pdftext As New PDF.PDFText

        If RblPriceYes.Checked Then
            If Not IsNothing(str) Then


                Try
                    Dim files(6) As String
                    files(0) = OrderAgreementTempPath + "\Order1.pdf"
                    files(1) = OrderAgreementTempPath + "\ANNEXURE1.pdf"
                    files(2) = OrderAgreementTempPath + "\ANNEXURE2.pdf"
                    files(3) = OrderPath + "\" + Convert.ToString(Quatationid) + ".pdf"
                    files(4) = OrderPath + "\SpecialPrice\" + Convert.ToString(Quatationid) + ".pdf"
                    files(5) = OrderAgreementTempPath + "\ANNEXURE3.pdf"
                    files(6) = OrderAgreementTempPath + "\Compl1.pdf"

                    fullpath12Price = OrderAgreementPath + "\" + txtEnqNo.Text.Trim() + "-" + txtName2.Text.Trim() + "- " + str3333 + ".pdf"
                    _pdfforge.MergePDFFiles(files, fullpath12Price, False)
                    MessageBox.Show("Document Ready !")
                    Class1.killProcessOnUser()
                    System.Diagnostics.Process.Start(fullpath12Price)

                Catch ex As Exception

                    il = 1
                End Try
                If il = 1 Then
                    Dim _pdfforgeN As New PDF.PDF
                    Dim files(5) As String
                    files(0) = OrderAgreementTempPath + "\Order1.pdf"
                    files(1) = OrderAgreementTempPath + "\ANNEXURE1.pdf"
                    files(2) = OrderAgreementTempPath + "\ANNEXURE2.pdf"
                    files(3) = OrderPath + "\" + Convert.ToString(Quatationid) + ".pdf"
                    files(4) = OrderAgreementTempPath + "\ANNEXURE3.pdf"
                    files(5) = OrderAgreementTempPath + "\Compl1.pdf"


                    fullpath12 = OrderAgreementPath + "\" + txtEnqNo.Text.Trim() + "-" + txtName2.Text.Trim() + "-" + str3333 + ".pdf"
                    _pdfforgeN.MergePDFFiles(files, fullpath12, False)

                    MessageBox.Show("Document Ready !")
                    Class1.killProcessOnUser()
                    System.Diagnostics.Process.Start(fullpath12)
                End If
            Else
                MessageBox.Show("Some Problem Occured !")
            End If

        Else

            If Not IsNothing(str) Then

                Try

                    Dim files(5) As String
                    files(0) = OrderAgreementTempPath + "\Order1.pdf"
                    files(1) = OrderAgreementTempPath + "\ANNEXURE1.pdf"
                    files(2) = OrderAgreementTempPath + "\ANNEXURE2.pdf"
                    files(3) = OrderPath + "\" + Convert.ToString(Quatationid) + ".pdf"
                    files(4) = OrderPath + "\SpecialPrice\" + Convert.ToString(Quatationid) + ".pdf"
                    files(5) = OrderAgreementTempPath + "\ANNEXURE3.pdf"

                    fullpath12Price = OrderAgreementPath + "\" + txtEnqNo.Text.Trim() + "-" + txtName2.Text.Trim() + "- " + str3333 + ".pdf"
                    _pdfforge.MergePDFFiles(files, fullpath12Price, False)

                    MessageBox.Show("Document Ready !")
                    Class1.killProcessOnUser()
                    System.Diagnostics.Process.Start(fullpath12Price)

                Catch ex As Exception
                    'Dim rootFolderPath As String
                    'rootFolderPath = OrderAgreementPath
                    'Dim filesToDelete As String
                    'filesToDelete = "OrderAgreement.pdf"
                    'System.IO.File.Delete(filesToDelete)
                    il = 1
                End Try
                If il = 1 Then
                    Dim _pdfforgeN As New PDF.PDF
                    Dim files(4) As String
                    files(0) = OrderAgreementTempPath + "\Order1.pdf"
                    files(1) = OrderAgreementTempPath + "\ANNEXURE1.pdf"
                    files(2) = OrderAgreementTempPath + "\ANNEXURE2.pdf"
                    files(3) = OrderPath + "\" + Convert.ToString(Quatationid) + ".pdf"
                    files(4) = OrderAgreementTempPath + "\ANNEXURE3.pdf"

                    fullpath12 = OrderAgreementPath + "\" + txtEnqNo.Text.Trim() + "-" + txtName2.Text.Trim() + "-" + str3333 + ".pdf"
                    _pdfforgeN.MergePDFFiles(files, fullpath12, False)

                    MessageBox.Show("Document Ready !")
                    Class1.killProcessOnUser()
                    System.Diagnostics.Process.Start(fullpath12)
                End If
            Else



                MessageBox.Show("Some Problem Occured !")
            End If

        End If
        'If il = 1 Then
        '    ' Try


        '    'Dim rootFolderPath As String
        '    '    rootFolderPath = OrderAgreementPath
        '    '    C:\\SomeFolder\\AnotherFolder\\FolderCOntainingThingsToDelete";

        '    'Dim filesToDelete As String
        '    '    filesToDelete = fullpath12Price
        '    'Dim fileList As String() = System.IO.Directory.GetFiles(rootFolderPath, filesToDelete)
        '    'For Each file In fileList
        '    '    System.IO.File.Delete(file)
        '    '    Next
        '    'Catch ex As Exception

        '    'End Try
        'End If



        'objDoc.Tables(1).Columns.Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderTop)
        '    .LineStyle = objApp.Options.DefaultBorderLineStyle
        '    .LineWidth = objApp.Options.DefaultBorderLineWidth
        '    .Color = objApp.Options.DefaultBorderColor
        'End With
        'objDoc.Tables(1).Columns.Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderRight)
        '    .LineStyle = objApp.Options.DefaultBorderLineStyle
        '    .LineWidth = objApp.Options.DefaultBorderLineWidth
        '    .Color = objApp.Options.DefaultBorderColor
        'End With
        'objDoc.Tables(1).Columns.Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderLeft)
        '    .LineStyle = objApp.Options.DefaultBorderLineStyle
        '    .LineWidth = objApp.Options.DefaultBorderLineWidth
        '    .Color = objApp.Options.DefaultBorderColor
        'End With
        'objDoc.Tables(1).Columns.Select()
        'With objApp.Selection.Borders(Word.WdBorderType.wdBorderBottom)
        '    .LineStyle = objApp.Options.DefaultBorderLineStyle
        '    .LineWidth = objApp.Options.DefaultBorderLineWidth
        '    .Color = objApp.Options.DefaultBorderColor
        'End With





    End Sub



    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Call GvQuotationSearch_Bind()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub GvComapnySearch_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvComapnySearch.CellClick
        Try
            con1.Close()
        Catch ex As Exception

        End Try
        con1.Open()
        btnSave1.Text = "Update"
        CompanyId = Convert.ToInt32(Me.GvComapnySearch.SelectedCells(0).Value)

        Flag = 1
        Display(CompanyId)
        '  txtModel_Leave(Null, e)
        Gv_GetAnnexEditData()
        GetTermData()
        Gv_GetAnnex2EditData()
        GV_GetComplementry()
        Gv_EditAnne4Data()
        Try
            con1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub



    Private Sub GvComapnySearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvComapnySearch.DoubleClick
        
    End Sub
    Private Sub Gv_EditAnne4Data()

        Dim str1 As String
        Dim da1234 As New SqlDataAdapter
        Dim ds1234 As New DataSet
        Dim dt124 As New DataTable

        dt124.Columns.Add("Remove", GetType(Boolean))
        dt124.Columns.Add("Select", GetType(Boolean))
        dt124.Columns.Add("SrNo", GetType(String))
        dt124.Columns.Add("T_Name", GetType(String))
        dt124.Columns.Add("Description", GetType(String))
        str1 = "select Sr_No,T_Name,Description,IsSelected  from ANNEXURE4Data where Fk_CompanyID= " & CompanyId & ""
        da1234 = New SqlDataAdapter(str1, con1)
        ds1234 = New DataSet()
        da1234.Fill(ds1234)

        For S1 = 0 To ds1234.Tables(0).Rows.Count - 1

            Dim imagestatus As Int16
            imagestatus = 0
            If ds1234.Tables(0).Rows(S1)("IsSelected") = "Yes" Then
                imagestatus = 1
            End If
            dt124.Rows.Add(0, imagestatus, ds1234.Tables(0).Rows(S1)("Sr_No").ToString(), ds1234.Tables(0).Rows(S1)("T_Name").ToString(), ds1234.Tables(0).Rows(S1)("Description").ToString())
        Next
        gvAnne4.DataSource = dt124
        da1234.Dispose()
        dt124.Dispose()
        ds1234.Dispose()

    End Sub
    Private Sub GV_GetComplementry()

        Try

            con1.Close()
        Catch ex As Exception

        End Try
        con1.Open()
        Dim str1 As String
        Dim da1233 As New SqlDataAdapter
        Dim ds1233 As New DataSet
        Dim dt123 As New DataTable

        dt123.Columns.Add("Remove", GetType(Boolean))
        dt123.Columns.Add("Select", GetType(Boolean))
        dt123.Columns.Add("SrNo", GetType(String))
        dt123.Columns.Add("Description", GetType(String))
        dt123.Columns.Add("Qty", GetType(String))
        str1 = "select Sr_No,Description,Qty,IsSelected from ComplementryData where Fk_CompanyID= " & CompanyId & " Order By  Sr_No ASC"
        da1233 = New SqlDataAdapter(str1, con1)
        ds1233 = New DataSet()
        da1233.Fill(ds1233)

        For S1 = 0 To ds1233.Tables(0).Rows.Count - 1

            Dim imagestatus As Int16
            imagestatus = 0
            If ds1233.Tables(0).Rows(S1)("IsSelected") = "Yes" Then
                imagestatus = 1
            End If
            dt123.Rows.Add(0, imagestatus, ds1233.Tables(0).Rows(S1)("Sr_No").ToString(), ds1233.Tables(0).Rows(S1)("Description").ToString(), ds1233.Tables(0).Rows(S1)("Qty").ToString())
        Next
        dgComplem.DataSource = dt123
        da1233.Dispose()
        dt123.Dispose()
        ds1233.Dispose()
        con1.Close()
    End Sub
    Private Sub GetTermData()
        Try
            Dim str1 As String
            Dim da12311 As New SqlDataAdapter
            Dim ds12311 As New DataSet
            Dim dt1211 As New DataTable
            Dim dtr As SqlDataReader

            con1.Open()
            str1 = "select * from Chequedetail where Fk_CompanyID= " & CompanyId & ""
            cmd = New SqlCommand(str1, con1)
            dtr = cmd.ExecuteReader()
            dtr.Read()
            txtchq.Text = dtr("Cheque_No").ToString()
            txtcdate.Text = dtr("Cheque_date").ToString()
            txtcbank.Text = dtr("Bank_Name").ToString()
            txtcrace.Text = dtr("Rupees").ToString()
            txtrrscmnt.Text = dtr("Rupees_string").ToString()
            txtrobj.Text = dtr("Objective1").ToString()
            txtrObj2.Text = dtr("Objective2").ToString()
            txtrobj3.Text = dtr("Objective3").ToString()
            txtlname1.Text = dtr("MsCompany_name").ToString()
            txtrname1.Text = dtr("Auth_lname1").ToString()
            txtrname2.Text = dtr("Auth_lname2").ToString()
            txtrname4.Text = dtr("Auth_lname3").ToString()
            txtlname4.Text = dtr("Auth_rname4").ToString()
            txtlname2.Text = dtr("Auth_rname1").ToString()
            txtlname3.Text = dtr("Auth_rname2").ToString()
            chkdate.Text = dtr("RecieveDate").ToString()
            txtpalace.Text = dtr("Place").ToString()
            dtr.Dispose()
            dtr.Close()
            cmd.Dispose()

            da12311.Dispose()
            dt1211.Dispose()
            ds12311.Dispose()
            con1.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GvCategorySearch_DoubleClick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GvCategorySearch.DoubleClick
        Try
            con1.Close()
        Catch ex As Exception

        End Try
        Call btnAddClear_Click(Nothing, Nothing)

        con1.Open()
        Quatationid = Convert.ToInt32(Me.GvCategorySearch.SelectedCells(0).Value)
        btnViewPriceSheet.Visible = True

        Flag = 2
        Display(Quatationid)
        Try
            con1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Sub btnSearchCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompany.Click
        BindGvComapnySearch()
    End Sub

    Private Sub txtSearchName_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchName.DoubleClick

    End Sub

    Private Sub txtSearchName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchName.Leave
        Try
            con1.Close()
        Catch ex As Exception
        End Try
        con1.Open()
        Dim str As String
        str = "select Enq_No from Quotation_Master where Name='" + txtSearchName.Text + "'"
        da = New SqlDataAdapter(str, con1)
        ds = New DataSet()
        da.Fill(ds)
        Dim tt As Int32
        tt = GvComapnySearch.Rows.Count()

        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtSearchEnQ.AutoCompleteCustomSource.Add(dr1.Item("Enq_No").ToString())
        Next

        da.Dispose()
        ds.Dispose()

    End Sub

    Private Sub txtQuatType_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQuatType.Leave
        GetAnexData(txtQuatType.Text.Trim(), txtPlantNo.Text.Trim(), txtModel.Text.Trim())

        Try
            con1.Close()
        Catch ex As Exception
        End Try
        con1.Open()
        Dim enqtype As String = "select Distinct(Plant) from ANNEXURE1 where Qtype ='" + txtQuatType.Text + "'"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtPlantNo.AutoCompleteCustomSource.Add(dr1.Item("Plant").ToString())
        Next
        da.Dispose()
        ds.Dispose()
        con1.Close()
        txtPlantNo.Focus()
    End Sub
    Public Sub FillAutoCompleteDescription()
        Dim enqtype As String = "select Distinct(Description) from ANNEXURE1 where Qtype= '" + txtQuatType.Text.Trim() + "'"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtDescription.AutoCompleteCustomSource.Add(dr1.Item("Description").ToString())
        Next
        da.Dispose()
        ds.Dispose()


        enqtype = "select Distinct(Remarks) from ANNEXURE1 where Qtype= '" + txtQuatType.Text.Trim() + "'"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtRemarks.AutoCompleteCustomSource.Add(dr1.Item("Remarks").ToString())
        Next
        da.Dispose()
        ds.Dispose()

    End Sub
    Private Sub txtPlantNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlantNo.Leave
        GetAnexData(txtQuatType.Text.Trim(), txtPlantNo.Text.Trim(), txtModel.Text.Trim())
        FillAutoCompleteDescription()
        Try
            con1.Close()
        Catch ex As Exception

        End Try
        con1.Open()
        Dim enqtype As String = "select Distinct(Model) from ANNEXURE1 where Plant ='" + txtPlantNo.Text + "'"
        da = New SqlDataAdapter(enqtype, con1)
        ds = New DataSet()
        da.Fill(ds)
        For Each dr1 As DataRow In ds.Tables(0).Rows
            txtModel.AutoCompleteCustomSource.Add(dr1.Item("Model").ToString())
        Next
        da.Dispose()
        ds.Dispose()
        con1.Close()
        txtModel.Focus()
    End Sub

    Private Sub txtModel_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtModel.Leave
        GetAnexData(txtQuatType.Text.Trim(), txtPlantNo.Text.Trim(), txtModel.Text.Trim())
        Dim Srno As Integer
        Srno = 0
        Try
            con1.Close()
        Catch ex As Exception
        End Try
        If txtPlantNo.Text.Trim <> "" And txtQuatType.Text.Trim() <> "" And txtModel.Text.Trim() <> "" Then
            con1.Open()
            Dim enqtype As String = "select Plant,Model,Qtype,Description from ANNEXURE1 where Plant ='" + txtPlantNo.Text + "' and  Model = '" + txtModel.Text + "' and Qtype ='" + txtQuatType.Text + "'"

            enqtype = "select MAX(Sr_No) as Sr_No From ANNEXURE1  where Plant ='" + txtPlantNo.Text + "' and  Model = '" + txtModel.Text + "' and Qtype ='" + txtQuatType.Text + "'"
            da = New SqlDataAdapter(enqtype, con1)
            ds = New DataSet()
            da.Fill(ds)
            For Each dr1 As DataRow In ds.Tables(0).Rows
                If dr1.Item("Sr_No").ToString() <> "" Then
                    txtSr_NO.Text = (Convert.ToInt32(dr1.Item("Sr_No").ToString()) + 1).ToString()
                    Srno = 1
                End If

            Next

            If Srno = 0 Then
                txtSr_NO.Text = "1"
            End If
            da.Fill(ds)
            da.Dispose()
            ds.Dispose()
            con1.Close()

        End If

    End Sub

   

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirstQuat.Click
        Class1.killProcessOnUser()
        DocumentStatus = 0
        FirstPage()
        MessageBox.Show("First Letter Ready !")
        System.Diagnostics.Process.Start(OrderAgreementTempPath + "\Order1.pdf")
    End Sub

    Private Sub btnPriceSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPriceSheet.Click
        DocumentStatus = 0
        Dim ik As Integer
        ik = 0
        Try

            Dim _pdfforge As New PDF.PDF
            Dim files(1) As String
            files(0) = OrderPath + "\" + Convert.ToString(Quatationid) + ".pdf"
            files(1) = OrderPath + "\SpecialPrice\" + Convert.ToString(Quatationid) + ".pdf"
            Dim fullpath12 As String
            fullpath12 = QPath + "\PriceSheets.pdf"
            _pdfforge.MergePDFFiles(files, fullpath12, False)
            MessageBox.Show("Price Sheet Ready !")
            System.Diagnostics.Process.Start(QPath + "\PriceSheets.pdf")
        Catch ex As Exception
            ik = 1
        End Try
        If ik = 1 Then
            Dim _pdfforge As New PDF.PDF
            Dim files(0) As String
            files(0) = OrderPath + "\" + Convert.ToString(Quatationid) + ".pdf"
            Dim fullpath12 As String
            fullpath12 = QPath + "\PriceSheet.pdf"
            _pdfforge.MergePDFFiles(files, fullpath12, False)
            MessageBox.Show("Price Sheet Ready !")
            System.Diagnostics.Process.Start(QPath + "\PriceSheet.pdf")
        End If
    End Sub

    Private Sub btnTerms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTerms.Click
        Class1.killProcessOnUser()
        DocumentStatus = 0
        FinalAnn4()
        MessageBox.Show("Terms and Conditions Ready !")
        System.Diagnostics.Process.Start(OrderAgreementTempPath + "\ANNEXURE3.pdf")
    End Sub

    Private Sub BtnAddMore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddMore.Click
        Try
            con1.Close()
        Catch ex As Exception
        End Try

        Try
            Dim SrNo As Integer
            con1.Open()
            str = "insert into ANNEXURE1 (Sr_No,Description,Remarks,Qtype,Plant,Model) values(" & txtSr_NO.Text & ",'" + txtDescription.Text + "','" + txtRemarks.Text + "','" + txtQuatType.Text + "','" + txtPlantNo.Text + "','" + txtModel.Text + "')"
            cmd = New SqlCommand(str, con1)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con1.Close()
            con1.Open()
            Dim enqtype As String = "select MAX(Sr_No) as Sr_No From ANNEXURE1  where Plant ='" + txtPlantNo.Text + "' and  Model = '" + txtModel.Text + "' and Qtype ='" + txtQuatType.Text + "'"
            da = New SqlDataAdapter(enqtype, con1)
            ds = New DataSet()
            da.Fill(ds)
            For Each dr1 As DataRow In ds.Tables(0).Rows
                If dr1.Item("Sr_No").ToString() <> "" Then
                    txtSr_NO.Text = (Convert.ToInt32(dr1.Item("Sr_No").ToString()) + 1).ToString()
                    SrNo = 1
                End If

            Next
            If SrNo = 0 Then
                txtSr_NO.Text = "1"
            End If
            con1.Close()
            GetAnexData(txtQuatType.Text.Trim(), txtPlantNo.Text.Trim(), txtModel.Text.Trim())
        Catch ex As Exception
            MessageBox.Show("Please Try Again..!")
        End Try

    End Sub

    Private Sub btnAnne2Default_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnne2Default.Click
        GetANN2()
        GvGetOnlyAnn4()

    End Sub

    Private Sub BtnDefaultComplementry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDefaultComplementry.Click
        GetComplementary()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        GvGetOnlyAnn4()

    End Sub

    Private Sub txtName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.Leave
        txtlname1.Text = "M/S. " + txtName.Text
    End Sub

    Private Sub txtAnne1Default_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAnne1Default.Leave
        GetAnexData(txtQuatType.Text.Trim(), txtPlantNo.Text.Trim(), txtModel.Text.Trim())

    End Sub

    Private Sub txtName2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName2.Leave
        txtlname2.Text = txtName2.Text
    End Sub

    Private Sub txtname21_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname21.Leave
        txtlname3.Text = txtname21.Text
    End Sub

    Private Sub txtname22_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname22.Leave
        txtlname4.Text = txtname22.Text
    End Sub

    Private Sub btnAddClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddClear.Click
        SetClean()
        Quatationid = 0
        btnViewPriceSheet.Visible = False
        btnSave1.Text = "Save"
        GetAnexData(txtQuatType.Text, txtPlantNo.Text, txtModel.Text.Trim())
        GetANN2()
        GvGetOnlyAnn4()
    End Sub

    Private Sub btnViewPriceSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewPriceSheet.Click
        Static type As String
        Try
            con1.Close()
        Catch ex As Exception

        End Try

        Try
            con1.Open()
            Dim str As String
            str = "select Quatation_Type from Quotation_Master qm inner join PDFGenerate_Check p on qm.Pk_QuotationID = p.FK_QuatationID  where IsCreated = 'Yes' and qm.Pk_QuotationID =" & Quatationid & "  order by Pk_QuotationID desc"
            cmd = New SqlCommand(str, con1)
            dr = cmd.ExecuteReader()
            dr.Read()
            type = dr("Quatation_Type").ToString()
            cmd.Dispose()
            dr.Close()
            con1.Close()
        Catch ex As Exception

        End Try
        If type = "ISI" Then

            Dim f As New ISIQuotation()
            f.Text = "ISI QUATATION"
            f.FormBorderStyle = FormBorderStyle.FixedToolWindow
            f.MaximizeBox = False
            f.MinimizeBox = False
            f.StartPosition = FormStartPosition.CenterScreen
            f.Width = 750
            f.Height = 800
            f.GroupBox7.Visible = False
            f.GroupBox1.Location = New Point(3, 41)
            f.grpLangaugeBar.Visible = False

            Class1.global.QuatationId = Quatationid
            f.GvCategorySearch_DoubleClick(Nothing, Nothing)
            f.ShowDialog()

        Else
            Dim f As New QuotationMaster()
            f.Text = "NON ISI QUATATION"
            f.FormBorderStyle = FormBorderStyle.FixedToolWindow
            f.MaximizeBox = False
            f.MinimizeBox = False
            f.StartPosition = FormStartPosition.CenterScreen
            f.Width = 750
            f.Height = 800
            f.GroupBox7.Visible = False
            f.GroupBox1.Location = New Point(3, 41)
            f.grpLangaugeBar.Visible = False

            Class1.global.QuatationId = Quatationid
            f.GvCategorySearch_DoubleClick(Nothing, Nothing)
            f.ShowDialog()
        End If

    End Sub

    Private Sub GvCategorySearch_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GvCategorySearch.CellContentClick

    End Sub

    Private Sub btnISI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnISI.Click
        Dim frmIsiQuat As New ISIQuotation
        frmIsiQuat.Show()


    End Sub

    Private Sub btnNonIsi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNonIsi.Click
        Dim frmnonisiQuat As New QuotationMaster
        frmnonisiQuat.Show()
    End Sub

    Private Sub CompanyMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each control As Control In Me.Controls
            ' The textbox control.
            Dim parentControl As New Control


            If (control.GetType() Is GetType(GroupBox)) Then
                Dim grpBox As GroupBox = TryCast(control, GroupBox)
                parentControl = grpBox
            ElseIf (control.GetType() Is GetType(TabControl)) Then
                Dim TC As TabControl = TryCast(control, TabControl)
                parentControl = TC
            ElseIf (control.GetType() Is GetType(Panel)) Then
                Dim Panel As Panel = TryCast(control, Panel)
                parentControl = Panel

            Else
                Try
                    parentControl = control
                Catch ex As Exception

                End Try

            End If

            For Each subcontrol As Control In parentControl.Controls
                If (subcontrol.GetType() Is GetType(TabControl)) Then
                    For Each subcontrolTwo As Control In subcontrol.Controls
                        If (subcontrolTwo.GetType() Is GetType(TabPage)) Then
                            For Each subcontrolthree As Control In subcontrolTwo.Controls
                                If (subcontrolthree.GetType() Is GetType(Panel)) Then
                                    For Each subcontrolFour As Control In subcontrolthree.Controls
                                        If (subcontrolFour.GetType() Is GetType(TextBox)) Then
                                            Dim textBox As TextBox = TryCast(subcontrolFour, TextBox)

                                            ' If not null, set the handler.
                                            If textBox IsNot Nothing Then
                                                AddHandler textBox.Enter, AddressOf TextBox_Enter
                                                AddHandler textBox.Leave, AddressOf TextBox_Leave
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
                If (subcontrol.GetType() Is GetType(TextBox)) Then
                    Dim textBox As TextBox = TryCast(subcontrol, TextBox)

                    ' If not null, set the handler.
                    If textBox IsNot Nothing Then
                        AddHandler textBox.Enter, AddressOf TextBox_Enter
                        AddHandler textBox.Leave, AddressOf TextBox_Leave
                    End If
                End If

                ' Set the handler.
            Next

            ' Set the handler.
        Next
    End Sub
    Private Sub TextBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)

        txt.BackColor = Color.LightYellow
    End Sub
    Private Sub TextBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As New TextBox()
        txt = DirectCast(sender, TextBox)

        txt.BackColor = Color.White
    End Sub

    

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If CompanyId > 0 Then
                If MessageBox.Show("Delete Company " + txtName.Text, "Delete Data", MessageBoxButtons.OKCancel) = DialogResult.OK Then

                    oDataAccess = New DataAccess

                    Dim cmd(5) As SqlCommand
                    Dim iRowAffected As Integer = 0

                    cmd(0) = New SqlCommand

                    cmd(0).CommandText = "SP_Delete_Company_Master"
                    cmd(0).CommandType = CommandType.StoredProcedure

                    cmd(0).Parameters.Add("@Pk_CompanyID", SqlDbType.BigInt).Value = CompanyId


                    cmd(1) = New SqlCommand

                    cmd(1).CommandText = "SP_Delete_AnnexureData"
                    cmd(1).CommandType = CommandType.StoredProcedure

                    cmd(1).Parameters.Add("@Fk_CompanyID", SqlDbType.BigInt).Value = CompanyId

                    cmd(2) = New SqlCommand

                    cmd(2).CommandText = "SP_Delete_Annexure2data"
                    cmd(2).CommandType = CommandType.StoredProcedure

                    cmd(2).Parameters.Add("@Fk_CompanyID", SqlDbType.BigInt).Value = CompanyId


                    cmd(3) = New SqlCommand

                    cmd(3).CommandText = "SP_Delete_ComplementryData"
                    cmd(3).CommandType = CommandType.StoredProcedure

                    cmd(3).Parameters.Add("@Fk_CompanyID", SqlDbType.BigInt).Value = CompanyId

                    cmd(4) = New SqlCommand

                    cmd(4).CommandText = "SP_Delete_ANNEXURE4Data"
                    cmd(4).CommandType = CommandType.StoredProcedure

                    cmd(4).Parameters.Add("@Fk_CompanyID", SqlDbType.BigInt).Value = CompanyId

                    cmd(5) = New SqlCommand

                    cmd(5).CommandText = "SP_Delete_ChequeDetail"
                    cmd(5).CommandType = CommandType.StoredProcedure

                    cmd(5).Parameters.Add("@Fk_CompanyID", SqlDbType.BigInt).Value = CompanyId

                    If oDataAccess.ExecuteSP(cmd) = False Then
                        Exit Sub
                    End If

                    MessageBox.Show("Delete Data sucessful")

                    Call btnAddClear_Click(Nothing, Nothing)
                    Call BindGvComapnySearch()

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub tbanne2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbanne2.Click

    End Sub
End Class