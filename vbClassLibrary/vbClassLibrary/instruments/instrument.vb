Public Class instrument

    ' classe conteneur pour un bond
    Protected identifiers_ As vbClassLibrary.securityIdentifierCollection

    Protected issueDate_ As Date

    Protected Sub New(ByVal identifiers As vbClassLibrary.securityIdentifierCollection,
                      ByVal issueDate As Date)


        identifiers_ = identifiers

        issueDate_ = issueDate


    End Sub

    Public ReadOnly Property identifiers As vbClassLibrary.securityIdentifierCollection

        Get

            Return identifiers_

        End Get

    End Property


    Public ReadOnly Property issueDate As Date

        Get

            Return issueDate_

        End Get

    End Property


End Class


Public Class instrumentCollection

    Inherits Collections.CollectionBase

    Default Public Property Item(ByVal index As Integer) As instrument

        Get

            Return CType(List(index), instrument)

        End Get

        Set(ByVal value As instrument)

            List(index) = value

        End Set

    End Property

    Public Function Add(ByVal value As instrument) As Integer

        Return List.Add(value)

    End Function 'Add

    ' todo remplacer le bondstruct par l'identifiant
    Public Function IndexOf(ByVal value As instrument) As Integer

        Return List.IndexOf(value)

    End Function 'IndexOf


    Public Sub Insert(ByVal index As Integer, ByVal value As instrument)

        List.Insert(index, value)

    End Sub 'Insert


    Public Sub Remove(ByVal value As instrument)

        List.Remove(value)

    End Sub 'Remove


    Public Function Contains(ByVal value As instrument) As Boolean

        ' If value is not of type bond, this will return false.
        Return List.Contains(value)

    End Function 'Contains


    Protected Overrides Sub OnValidate(ByVal value As Object)

        If Not GetType(instrument).IsAssignableFrom(value.GetType()) Then

            Throw New ArgumentException("value must be of type instrument.", "value")

        End If

    End Sub 'OnValidate 



End Class
