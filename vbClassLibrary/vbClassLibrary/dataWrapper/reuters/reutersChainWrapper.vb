#If _REUTERS Then

Public Class reutersChainWrapper '(Of T As securityIdentifier)

    Inherits reutersWrapper

    Private WithEvents myChain_ As AdfinXRtLib.AdxRtChain
    Private modeStr_ As String
    Private dataType_ As vbClassLibrary.bloombergYellowKey

    Protected Overrides Sub Finalize()

        myChain_ = Nothing
        data_ = Nothing

    End Sub

    Sub New(ByVal IDChain As String,
            ByVal dataType As vbClassLibrary.bloombergYellowKey,
            Optional ByVal ignore As Boolean = True,
            Optional ByVal updateWhenComplete As Boolean = True,
            Optional ByVal live As Boolean = False) ' le type de données attendu

        MyBase.New(ignore, updateWhenComplete, live)

        myChain_ = New AdfinXRtLib.AdxRtChain

        data_ = New baseMarketDataCollection

        dataType_ = dataType

        dlg_ = New finalizeDataDelegate(AddressOf onUpdateTrigger)

        With myChain_

            .Source = "IDN"

            .ErrorMode = AdfinXRtLib.AdxErrorMode.EXCEPTION

            .Mode = rtDefaultMode()

            .ItemName = IDChain

        End With

        Me.sendRequest()

    End Sub

    Protected Overrides Sub sendRequest()

        myChain_.RequestChain()

    End Sub

    Private Sub statusChange(ByVal status As AdfinXRtLib.RT_SourceStatus) Handles myChain_.OnStatusChange

        If status <> AdfinXRtLib.RT_SourceStatus.RT_SOURCE_UP Then

            Dim ex As Exception =
                New wrapperException("Reuters source status is: " & status.ToString)

            Throw ex

        End If

    End Sub

    Private Sub storeData(ByVal status As AdfinXRtLib.RT_DataStatus) Handles myChain_.OnUpdate

        If status = AdfinXRtLib.RT_DataStatus.RT_DS_FULL Then

            For Each i As String In myChain_.Data

                ' retour de cusips -> catch en string
                If i <> "" Then

                    Dim tempStr As String = Mid(i, 1, Len(i) - 1)

                    If Len(tempStr) = 9 Then

                        Dim tempId As securityIdentifier = New cusip(Left(i, 9),
                                                                     dataType_)

                        Dim tempMktData As baseMarketData = New marketData(Of String)(tempId.id,
                                                                                      Now,
                                                                                      Now,
                                                                                      tempId,
                                                                                      dataProviders.reuters,
                                                                                      "Reuters Identifier")

                        data_.Add(tempMktData)

                    End If

                End If

            Next

            ' fully updated
            dlg_.Invoke(data_)

        End If

    End Sub

End Class

#End If
