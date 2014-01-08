Public Class quote

    Inherits marketData

    Private value_ As Double

    Private instrumentId_ As String

    Public Sub New(ByVal value As Double, _
                   ByVal quoteTime As Date, _
                   ByVal requestTime As Date)

        MyBase.new(quoteTime,
                   requestTime)

        value_ = value


    End Sub


End Class