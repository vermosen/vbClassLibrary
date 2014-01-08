Public MustInherit Class baseMarketData

    Protected instrumentId_ As securityIdentifier
    Protected dataTime_ As Date
    Protected requestTime_ As Date
    Protected dataProvider_ As dataProviders
    Protected fieldName_ As String

    Protected Sub New(ByVal dataTime As Date,
                      ByVal requestTime As Date,
                      ByVal instrumentId As securityIdentifier,
                      Optional ByVal dataProvider As dataProviders = dataProviders.unknown,
                      Optional ByVal fieldName As String = "")

        instrumentId_ = instrumentId
        dataTime_ = dataTime
        requestTime_ = requestTime
        dataProvider_ = dataProvider
        fieldName_ = fieldName

    End Sub

    Public ReadOnly Property instrumentId() As securityIdentifier

        Get

            instrumentId = instrumentId_

        End Get

    End Property

    Public ReadOnly Property dataTime() As Date

        Get

            dataTime = dataTime_

        End Get

    End Property

    Public ReadOnly Property requestTime() As Date

        Get

            requestTime = requestTime_

        End Get

    End Property

    Public ReadOnly Property dataProvider() As dataProviders

        Get

            dataProvider = dataProvider_

        End Get

    End Property

    Public ReadOnly Property fieldName() As String

        Get

            fieldName = fieldName_

        End Get

    End Property

End Class

Public Class baseMarketDataCollection

    Inherits Collections.CollectionBase

    Default Public Property Item(ByVal index As Integer) As baseMarketData

        Get

            Return CType(List(index), baseMarketData)

        End Get

        Set(ByVal value As baseMarketData)

            List(index) = value

        End Set

    End Property

    Public Function Add(ByVal value As baseMarketData) As Integer

        Return List.Add(value)

    End Function 'Add

    ' todo remplacer le bondstruct par l'identifiant
    Public Function IndexOf(ByVal value As baseMarketData) As Integer

        Return List.IndexOf(value)

    End Function 'IndexOf


    Public Sub Insert(ByVal index As Integer, ByVal value As baseMarketData)

        List.Insert(index, value)

    End Sub 'Insert


    Public Sub Remove(ByVal value As baseMarketData)

        List.Remove(value)

    End Sub 'Remove


    Public Function Contains(ByVal value As baseMarketData) As Boolean

        ' If value is not of type bond, this will return false.
        Return List.Contains(value)

    End Function 'Contains


    Protected Overrides Sub OnValidate(ByVal value As Object)

        If Not GetType(baseMarketData).IsAssignableFrom(value.GetType()) Then

            Throw New ArgumentException("value must be of type baseMarketData.", "value")

        End If

    End Sub 'OnValidate 


End Class
