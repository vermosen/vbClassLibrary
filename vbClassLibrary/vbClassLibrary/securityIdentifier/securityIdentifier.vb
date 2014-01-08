Public Enum bloombergYellowKey

    ' reproduit les id de la DB
    Govt = -1
    Corp = -2
    Mtge = -3
    Comdty = -4
    Curncy = -5
    Index = -6

End Enum

Public MustInherit Class securityIdentifier

    ' classe virtuelle pure représentant
    ' un code d'identification d'un instrument
    ' et inclut un identifiant de la DB
    Protected bloombergDb_ As bloombergYellowKey

    Protected str_ As String

    Protected Sub New(ByVal str As String,
                      ByVal bloombergDb As bloombergYellowKey)

        str_ = str
        bloombergDb_ = bloombergDb

    End Sub

    Public ReadOnly Property id() As String

        Get

            Return str_

        End Get

    End Property

    Public MustOverride ReadOnly Property bloombergId() As String

    Public Shared Function convertYellowKey(ByVal str As String) As bloombergYellowKey

        str = str.ToUpper

        Select Case str

            Case "GOVT"

                Return bloombergYellowKey.Govt

            Case "CORP"

                Return bloombergYellowKey.Corp

            Case "MTGE"

                Return bloombergYellowKey.Mtge

            Case "CURNCY"

                Return bloombergYellowKey.Curncy

            Case "COMDTY"

                Return bloombergYellowKey.Comdty

            Case "INDEX"

                Return bloombergYellowKey.Comdty

            Case Else

                Dim ex As Exception = New Exception("unknown yellow key")

                Throw ex

        End Select

    End Function

    Public Shared Operator =(ByVal id1 As securityIdentifier, ByVal id2 As securityIdentifier) As Boolean

        If id1.str_ = id2.str_ And id1.bloombergDb_ = id2.bloombergDb_ Then

            Return True

        Else

            Return False

        End If

    End Operator

    Public Shared Operator <>(ByVal id1 As securityIdentifier, ByVal id2 As securityIdentifier) As Boolean

        If id1.str_ = id2.str_ And id1.bloombergDb_ = id2.bloombergDb_ Then

            Return False

        Else

            Return True

        End If

    End Operator

End Class

Public Class isin

    Inherits securityIdentifier

    Public Sub New(ByVal isinCode As String,
                   ByVal bloombergDb As bloombergYellowKey)

        MyBase.New(isinCode, bloombergDb)

        If isinCode.Length <> 12 Then

            Dim ex As Exception = New Exception("bad isin code set")

            Throw ex

        End If

    End Sub

    ' classe virtuelle pure représentant 
    ' un code d'identification d'un instrument
    Public Overrides ReadOnly Property bloombergId() As String

        Get

            Return str_ & " " & bloombergDb_.ToString

        End Get

    End Property

End Class

Public Class cusip

    Inherits securityIdentifier

    Public Sub New(ByVal cusip As String,
                   ByVal bloombergDb As bloombergYellowKey)

        MyBase.New(cusip, bloombergDb)

        If cusip.Length <> 9 Then

            Dim ex As Exception = New Exception("bad cusip code set")

            Throw ex

        End If

    End Sub

    ' classe virtuelle pure représentant 
    ' un code d'identification d'un instrument
    Public Overrides ReadOnly Property bloombergId() As String

        Get

            Return str_ & " " & bloombergDb_.ToString

        End Get

    End Property

End Class

' generic code for internal/display role
Public Class currentCode

    Inherits securityIdentifier

    Public Sub New(ByVal code As String,
                   ByVal bloombergDb As bloombergYellowKey)

        MyBase.New(code, bloombergDb)

    End Sub

    ' should not be recognized by bloomberg
    Public Overrides ReadOnly Property bloombergId() As String

        Get

            Return str_ & " " & bloombergDb_.ToString

        End Get

    End Property

End Class

Public Class securityIdentifierCollection

    Inherits Collections.CollectionBase

    Default Public Property Item(ByVal index As Integer) As securityIdentifier

        Get

            Return CType(List(index), securityIdentifier)

        End Get

        Set(ByVal value As securityIdentifier)

            List(index) = value

        End Set

    End Property

    Public Function Add(ByVal value As securityIdentifier) As Integer

        Return List.Add(value)

    End Function 'Add

    Public Function IndexOf(ByVal value As securityIdentifier) As Integer

        Return List.IndexOf(value)

    End Function 'IndexOf

    Public Sub Insert(ByVal index As Integer, ByVal value As securityIdentifier)

        List.Insert(index, value)

    End Sub 'Insert

    Public Sub Remove(ByVal value As securityIdentifier)

        List.Remove(value)

    End Sub 'Remove

    Public Function Contains(ByVal value As securityIdentifier) As Boolean

        ' If value is not of type bond, this will return false.
        Return List.Contains(value)

    End Function 'Contains

    Protected Overrides Sub OnValidate(ByVal value As Object)

        If Not GetType(securityIdentifier).IsAssignableFrom(value.GetType()) Then

            Throw New ArgumentException("value must be of type bond.", "value")

        End If

    End Sub 'OnValidate 

End Class