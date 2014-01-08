Public Enum dataProviders

    unknown = 0
    bloomberg = -1
    reuters = -2
    manual = -3

End Enum

Public Class marketData(Of T)

    Inherits baseMarketData

    Private value_ As T

    Public Sub New(ByVal value As T,
                   ByVal dataTime As Date,
                   ByVal requestTime As Date,
                   ByVal instrumentId As securityIdentifier,
                   Optional ByVal provider As dataProviders = dataProviders.unknown,
                   Optional ByVal fieldName As String = "")

        MyBase.new(dataTime,
                   requestTime,
                   instrumentId,
                   provider,
                   fieldName)

        value_ = value

    End Sub

    Public ReadOnly Property value() As T

        Get

            value = value_

        End Get

    End Property

End Class
