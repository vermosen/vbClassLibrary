Option Strict Off
Option Explicit On

Partial Class quoteDataSet2

End Class

<Global.System.ComponentModel.DesignerCategoryAttribute("code"), _
 Global.System.ComponentModel.ToolboxItem(True), _
 Global.System.ComponentModel.DataObjectAttribute(True), _
 Global.System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" & _
    ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), _
 Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")> _
Partial Public Class MERGED_TABLESTableAdapter

    Inherits Global.System.ComponentModel.Component

    Private WithEvents _adapter As Global.System.Data.SqlServerCe.SqlCeDataAdapter

    Private _connection As Global.System.Data.SqlServerCe.SqlCeConnection

    Private _transaction As Global.System.Data.SqlServerCe.SqlCeTransaction

    Private _commandCollection() As Global.System.Data.SqlServerCe.SqlCeCommand

    Private _clearBeforeFill As Boolean

End Class

Namespace quoteDataSet2TableAdapters

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), _
     Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter"), _
     Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Fill, True)> _
    Public Overridable Overloads Function Fill3(ByVal dataTable As quoteDataSet2.TABLE_INSTRUMENTDataTable) As Integer
        Me.Adapter.SelectCommand = Me.CommandCollection(0)
        If (Me.ClearBeforeFill = True) Then
            dataTable.Clear()
        End If
        Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
        Return returnValue
    End Function

End Namespace