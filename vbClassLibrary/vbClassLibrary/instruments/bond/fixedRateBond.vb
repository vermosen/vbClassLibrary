Public Class fixedRateBond

    Inherits bond

    ' classe conteneur pour un bond
    Private couponRate_ As Double
    Private firstCouponDate_ As System.Nullable(Of Date)
    Private lastCouponDate_ As System.Nullable(Of Date)

    Public Sub New(ByVal identifiers As vbClassLibrary.securityIdentifierCollection,
                   ByVal issueDate As Date,
                   ByVal effectiveDate As Date,
                   ByVal maturityDate As Date,
                   ByVal couponRate As Double,
                   Optional ByVal firstCouponDate As System.Nullable(Of Date) = Nothing,
                   Optional ByVal lastCouponDate As System.Nullable(Of Date) = Nothing)

        MyBase.New(identifiers,
                   issueDate,
                   effectiveDate,
                   maturityDate)

        couponRate_ = couponRate
        firstCouponDate_ = firstCouponDate
        lastCouponDate_ = lastCouponDate
        generateCurrentCode()

    End Sub

    Protected Overrides Sub generateCurrentCode()

        ' ajout du code courant à la pile des identifiants
        Dim codeStr As String = "T " & cpnStr() & " " & maturityDate_.ToString("MM/yy")

        Dim code As currentCode =
            New currentCode(codeStr, vbClassLibrary.bloombergYellowKey.Govt)

        identifiers_.Add(code)

    End Sub

    Private Function cpnStr() As String

        Dim cpnVal As Double = couponRate_ * 800 ' expected 1/8th

        If cpnVal = 0.0 Then

            Return "0"

        ElseIf cpnVal Mod 8 = 0 Then ' int

            cpnVal /= 8

            Return cpnVal.ToString("#####")

        ElseIf cpnVal Mod 4 = 0 Then ' 1/2th

            cpnVal /= 8

            Return cpnVal.ToString("#####.#", System.Globalization.CultureInfo.InvariantCulture)

        ElseIf cpnVal Mod 2 = 0 Then ' 1/4th

            cpnVal /= 8

            Return cpnVal.ToString("#####.##", System.Globalization.CultureInfo.InvariantCulture)

        ElseIf cpnVal Mod 1 = 0 Then ' 1/8th

            cpnVal /= 8

            Return cpnVal.ToString("#####.###", System.Globalization.CultureInfo.InvariantCulture)

        Else ' pas d'arrondi

            Return cpnVal.ToString("#####.###", System.Globalization.CultureInfo.InvariantCulture)

        End If

    End Function

    Public ReadOnly Property couponRate As System.Nullable(Of Double)

        Get

            Return couponRate_

        End Get

    End Property

    Public ReadOnly Property firstCouponDate As System.Nullable(Of Date)

        Get

            Return firstCouponDate_

        End Get

    End Property

    Public ReadOnly Property lastCouponDate As System.Nullable(Of Date)

        Get

            Return lastCouponDate_

        End Get

    End Property

End Class