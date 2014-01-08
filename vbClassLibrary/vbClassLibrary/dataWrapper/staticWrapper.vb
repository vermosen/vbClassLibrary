Public Class staticWrapper

    Inherits wrapper

    Protected returnStr_ As List(Of String)

    ReadOnly Property validData() As List(Of String)

        Get

            Return returnStr_

        End Get

    End Property

End Class
