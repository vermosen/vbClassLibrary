Option Explicit On

'Public Enum tickFields

'    TRADE = 0
'    BID = -1
'    ASK = -2
'    BID_BEST = -3
'    ASK_BEST = -4
'    BID_YIELD = -5
'    ASK_YIELD = -6
'    MID_PRICE = -7
'    AT_TRADE = -8
'    BEST_BID = -9
'    BEST_ASK = -10

'End Enum

Public Class bloombergIntradayWrapper

    Inherits bloombergWrapper

    ' Elements
    Private startDateTime_ As Date
    Private endDateTime_ As Date

    Sub New(ByVal vtSecurities As securityIdentifierCollection,
            ByVal vtFields As List(Of String),
            ByVal startDateTime As Date,
            ByVal endDateTime As Date) ' pas encore fonctionnel

        MyBase.new(vtSecurities, vtFields, Nothing, Nothing)

        If (vtSecurities.Count <> 1) Then

            Dim ex As Exception =
                New wrapperException("only 1 security authorized")

            Throw ex

        End If

        If (vtFields.Count <> 1) Then

            Dim ex As Exception =
                New wrapperException("only 1 field authorized")

            Throw ex

        End If

        startDateTime_ = startDateTime
        endDateTime_ = endDateTime

        blpRequest_ = refDataServices_.CreateRequest("IntradayBarRequest")
        blpRequest_.Set("eventType", "TRADE") 'Trade, Bid, Offer ?
        blpRequest_.Set("interval", 1) ' 1 minute interval
        blpRequest_.Set("startDateTime", New Bloomberglp.Blpapi.Datetime(
            startDateTime_.Year,
            startDateTime_.Month,
            startDateTime_.Day,
            startDateTime_.Hour,
            startDateTime_.Minute,
            startDateTime_.Second, 0))
        blpRequest_.Set("endDateTime", New Bloomberglp.Blpapi.Datetime(
            endDateTime_.Year,
            endDateTime_.Month,
            endDateTime_.Day,
            endDateTime_.Hour,
            endDateTime_.Minute,
            endDateTime_.Second, 0))
        blpRequest_.Set("gapFillInitialBar", True)
        blpRequest_.Set("security", vtSecurities_(0).bloombergId)

        Dim tempSecs As Bloomberglp.Blpapi.Element = blpRequest_.GetElement("security")

    End Sub

    Public Overrides Sub sendRequest()

        ' insertion du titre
        blpSession_.SendRequest(blpRequest_, corr_)

    End Sub

    Protected Overrides Sub storeData(ByVal eventObj As Bloomberglp.Blpapi.[Event],
                                      ByVal status As Bloomberglp.Blpapi.[Event].EventType)

        Try

            ' should be only 1 data
            For Each msg As Bloomberglp.Blpapi.Message In eventObj.GetMessages

                Dim referenceDataResponse As Bloomberglp.Blpapi.Element = msg.AsElement

                If referenceDataResponse.HasElement("responseError") Then

                    Dim ex As Exception =
                        New wrapperException("error in response")

                    Throw ex

                End If


                Dim data As Bloomberglp.Blpapi.Element =
                        referenceDataResponse.GetElement("barData").GetElement("barTickData")

                ' iteration sur les data
                For i As Long = 0 To data.NumValues - 1

                    Dim bar As Bloomberglp.Blpapi.Element =
                        data.GetValueAsElement(i)

                    Dim tmpData As baseMarketData =
                        New marketData(Of Double)(bar.GetElementAsFloat64("open"),
                                                  bar.GetElementAsDatetime("time").ToSystemDateTime,
                                                  Now,
                                                  vtSecurities_(0),
                                                  dataProviders.bloomberg,
                                                  "open")

                    data_.Add(tmpData)

                Next

            Next

        Catch ex As Exception



        End Try

    End Sub

    Private Function checkResponseId(ByVal incomingId As String) As securityIdentifier

        ' function de rapprochement entre l'Id 
        ' initiale des instruments et celle reçue par BBG
        For Each i As securityIdentifier In vtSecurities_

            If i.bloombergId = incomingId Then

                Return i

            End If

        Next

        ' si aucune correspondance
        Dim ex As Exception =
            New wrapperException("unknown instrument returned")

        Throw ex

    End Function

End Class
