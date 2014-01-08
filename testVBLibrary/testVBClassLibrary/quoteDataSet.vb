Partial Class quoteDataSet
    Partial Class quoteDataTableDataTable

        Private Sub quoteDataTableDataTable_quoteDataTableRowChanging(ByVal sender As System.Object, ByVal e As quoteDataTableRowChangeEvent) Handles Me.quoteDataTableRowChanging

        End Sub

    End Class

End Class

Namespace quoteDataSetTableAdapters
    
    Partial Public Class quoteDataTableTableAdapter

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), _
         Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), _
         Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Insert, True)> _
        Public Overridable Overloads Function Insert(ByVal p1 As String, ByVal p2 As Date, ByVal p3 As Double, ByVal p4 As Short) As Integer

            If (p1 Is Nothing) Then

                Throw New Global.System.ArgumentNullException("p1")

            Else

                Me.Adapter.InsertCommand.Parameters(0).Value = CType(p1, String)

            End If

            Me.Adapter.InsertCommand.Parameters(1).Value = CType(p2, Date)
            Me.Adapter.InsertCommand.Parameters(2).Value = CType(p3, Double)
            Me.Adapter.InsertCommand.Parameters(3).Value = CType(p4, Short)

            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.InsertCommand.Connection.State
            If ((Me.Adapter.InsertCommand.Connection.State And Global.System.Data.ConnectionState.Open) _
                        <> Global.System.Data.ConnectionState.Open) Then
                Me.Adapter.InsertCommand.Connection.Open()
            End If
            Try
                Dim returnValue As Integer = Me.Adapter.InsertCommand.ExecuteNonQuery
                Return returnValue
            Finally
                If (previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.InsertCommand.Connection.Close()
                End If
            End Try
        End Function

    End Class
End Namespace
