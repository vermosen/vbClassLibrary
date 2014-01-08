Public Delegate Sub onUpdateHandler(ByVal sender As Object, ByVal data As Integer)

Public Class ClassTest

    Public Event onUpdate As onUpdateHandler

    Protected Sub onUpdateTrigger(ByVal data As Integer)

        RaiseEvent onUpdate(Me, data)

    End Sub

    Public Sub doSomething()

        onUpdateTrigger(1)

    End Sub

End Class

Public Class ClassTestCollection

    Inherits Collections.CollectionBase

End Class
