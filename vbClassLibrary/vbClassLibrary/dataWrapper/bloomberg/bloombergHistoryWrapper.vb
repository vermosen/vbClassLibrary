Option Explicit On

Public Class bloombergHistoryWrapper

    Inherits bloombergWrapper

    ' Elements
    Private startDate_ As Date
    Private endDate_ As Date

    Sub New(ByVal vtSecurities As securityIdentifierCollection,
            ByVal vtFields As List(Of String),
            ByVal startDate As Date,
            ByVal endDate As Date,
            Optional ByRef overrideFields As List(Of String) = Nothing,
            Optional ByRef overrideValues As Object = Nothing) ' pas encore fonctionnel

        MyBase.new(vtSecurities, vtFields, overrideFields, overrideValues)

        startDate_ = startDate
        endDate_ = endDate

        ' le mode referenceData
        blpRequest_ = refDataServices_.CreateRequest("HistoricalDataRequest")
        blpRequest_.Set("periodicityAdjustment", "ACTUAL")
        blpRequest_.Set("periodicitySelection", "DAILY")
        blpRequest_.Set("startDate", startDate_.ToString("yyyyMMdd"))
        blpRequest_.Set("endDate", endDate_.ToString("yyyyMMdd"))

    End Sub

    Protected Overrides Sub storeData(ByVal eventObj As Bloomberglp.Blpapi.[Event],
                                      ByVal status As Bloomberglp.Blpapi.[Event].EventType)

        ' on prend tous les messages de l'event
        ' chaque message contient au plus 10 securities
        For Each msg As Bloomberglp.Blpapi.Message In eventObj.GetMessages

            Dim referenceDataResponse As Bloomberglp.Blpapi.Element = msg.AsElement

            If referenceDataResponse.HasElement("responseError") Then

                Dim ex As Exception =
                    New wrapperException("error in response")

                Throw ex

            End If


            Dim securityDataArray As Bloomberglp.Blpapi.Element =
                    referenceDataResponse.GetElement("securityData")

            ' iteration sur les securities (max10)
            For i As Long = 0 To securityDataArray.NumValues - 1

                Dim securityData As Bloomberglp.Blpapi.Element

                If securityDataArray.IsArray = True Then

                    securityData = securityDataArray.GetValueAsElement(i)

                Else

                    ' 1 seul element non numéroté
                    securityData = securityDataArray

                End If

                ' mise en correspondance avec la requête initiale
                Dim securityId As securityIdentifier =
                    checkResponseId(securityData.GetElementAsString("security"))

                If (securityData.HasElement("securityError")) Then

                    Dim ex As Exception =
                        New wrapperException("error in response")

                    Throw ex

                Else

                    Dim fldDataArray As Bloomberglp.Blpapi.Element =
                        securityData.GetElement("fieldData")

                    Select Case fldDataArray.Datatype ' should be a sequence

                        Case Bloomberglp.Blpapi.Schema.Datatype.SEQUENCE
                            ' à priori tjs une séquence pour les champs historiques

                            For j As Long = 0 To fldDataArray.NumValues - 1

                                Dim tmpData As baseMarketData =
                                    New marketData(Of Double)(fldDataArray.GetValueAsElement(j).Elements(1).GetValueAsFloat64,
                                                                fldDataArray.GetValueAsElement(j).Elements(0).GetValueAsDatetime.ToSystemDateTime,
                                                                Now,
                                                                securityId,
                                                                dataProviders.bloomberg,
                                                                fldDataArray.GetValueAsElement(j).Elements(1).ElementDefinition.Name.ToString)

                                data_.Add(tmpData)

                            Next

                        Case Else

                            Dim ex As Exception =
                                New wrapperException("error in response")

                            Throw ex

                    End Select

                End If

            Next

        Next

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