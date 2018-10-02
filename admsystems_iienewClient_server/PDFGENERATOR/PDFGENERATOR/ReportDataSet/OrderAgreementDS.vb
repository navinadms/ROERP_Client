Partial Class OrderAgreementDS
  
    Partial Class PlantDetailsDataTable

        Private Sub PlantDetailsDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.T_Image1Column.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class ScopeSupplyDataTable

        Private Sub ScopeSupplyDataTable_ScopeSupplyRowChanging(ByVal sender As System.Object, ByVal e As ScopeSupplyRowChangeEvent) Handles Me.ScopeSupplyRowChanging

        End Sub

    End Class

    Partial Class OrderAgreementDataTable

    End Class


End Class
