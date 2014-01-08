Public Class reutersChainWrapper

    Inherits staticWrapper

    Private Const RT_DEFAULT_MODE As String = "IGNE:YES UWC:YES LIVE:YES"

    Private Const RT_DEFAULT_SOURCE As String = "IDN"

    Private Const RT_ERROR As String = "#N/A N/A"


    Private WithEvents myChain_ As AdfinXRtLib.AdxRtChain


    Private modeStr_ As String

    Private feed_ As Boolean


    Public Event onUpdate(ByVal returnStr As List(Of String))


    Protected Overrides Sub Finalize()

        myChain_ = Nothing

    End Sub

    Sub New(ByVal IDChain As String)

        returnStr_ = New List(Of String)

        myChain_ = New AdfinXRtLib.AdxRtChain

        With myChain_

            .Source = "IDN"

            .ErrorMode = AdfinXRtLib.AdxErrorMode.EXCEPTION

            .Mode = RT_DEFAULT_MODE

            .ItemName = IDChain

        End With

    End Sub

    Public Sub setRequest()


        myChain_.RequestChain()


    End Sub

    Private Sub storeData() Handles myChain_.OnUpdate


        For Each i As String In myChain_.Data

            returnStr_.Add(i)

        Next


        RaiseEvent onUpdate(returnStr_)


    End Sub

End Class
