

Partial Public Class PartyDataset
    Partial Class PartyMasterDataTable

        Private Sub PartyMasterDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.AreaColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class Party_DispatchDataTable

        Private Sub Party_DispatchDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.DeliveryDistrictColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
