Public MustInherit Class bond

    Inherits instrument

    ' classe conteneur pour un bond
    Protected effectiveDate_ As Date
    Protected maturityDate_ As Date

    Protected MustOverride Sub generateCurrentCode()

    Protected Sub New(ByVal identifiers As vbClassLibrary.securityIdentifierCollection,
                      ByVal issueDate As Date,
                      ByVal effectiveDate As Date,
                      ByVal maturityDate As Date)

        MyBase.new(identifiers,
                   issueDate)

        effectiveDate_ = effectiveDate
        maturityDate_ = maturityDate

    End Sub

    Public ReadOnly Property maturityDate As Date

        Get

            Return maturityDate_

        End Get

    End Property

    Public ReadOnly Property effectiveDate As Date

        Get

            Return effectiveDate_

        End Get

    End Property

End Class