Public Class zeroCouponBond

    Inherits bond

    ' classe conteneur pour un bond

    Public Sub New(ByVal identifiers As vbClassLibrary.securityIdentifierCollection,
                   ByVal issueDate As Date,
                   ByVal effectiveDate As Date,
                   ByVal maturityDate As Date)

        MyBase.New(identifiers,
                   issueDate,
                   effectiveDate,
                   maturityDate)

        generateCurrentCode()

    End Sub

    Protected Overrides Sub generateCurrentCode()

        ' ajout du code courant à la pile des identifiants
        Dim codeStr As String = "B " & " " & maturityDate_.ToString("dd/MM/yy")

        Dim code As currentCode =
            New currentCode(codeStr,
                            vbClassLibrary.bloombergYellowKey.Govt)

        identifiers_.Add(code)

    End Sub

End Class
