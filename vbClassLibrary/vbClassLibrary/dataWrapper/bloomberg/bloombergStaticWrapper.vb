Public Class bloombergStaticWrapper

    Inherits bloombergWrapper

    ' pas encore fonctionnel, a tester
    Private overrideFields As Object
    Private overrideValues As Object

    Sub New(ByVal vtSecurities As securityIdentifierCollection,
            ByVal vtFields As List(Of String),
            Optional ByRef overrideFields As List(Of String) = Nothing,
            Optional ByRef overrideValues As List(Of Object) = Nothing)

        MyBase.new(vtSecurities, vtFields, overrideFields, overrideValues)

        ' le mode referenceData
        blpRequest_ = refDataServices_.CreateRequest("ReferenceDataRequest")

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

                Dim securityData As Bloomberglp.Blpapi.Element =
                    securityDataArray.GetValueAsElement(i)

                ' mise en correspondance avec la requête initiale
                Dim securityId As securityIdentifier =
                    checkResponseId(securityData.GetElementAsString("security"))

                ' ici il faut renvoyer l'Id originale
                ' une methode -> checkResponseId
                Dim sequenceNumber As Integer =
                    securityData.GetElementAsInt32("sequenceNumber")

                If (securityData.HasElement("securityError")) Then

                    Dim ex As Exception =
                        New wrapperException("error in response")

                    Throw ex

                Else

                    Dim fieldDataArray As Bloomberglp.Blpapi.Element =
                        securityData.GetElement("fieldData")

                    For Each fld As Bloomberglp.Blpapi.Element In fieldDataArray.Elements

                        Select Case fld.Datatype

                            Case Bloomberglp.Blpapi.Schema.Datatype.SEQUENCE

                                Dim ex As Exception =
                                    New wrapperException("sequence requested with a static wrapper")

                                Throw ex

                            Case Bloomberglp.Blpapi.Schema.Datatype.DATE

                                Dim tmpData As baseMarketData =
                                    New marketData(Of Date)(fld.GetValueAsDate.ToSystemDateTime,
                                                            Now,
                                                            Now,
                                                            securityId,
                                                            dataProviders.bloomberg,
                                                            fld.ElementDefinition.Name.ToString)

                                data_.Add(tmpData)

                            Case Bloomberglp.Blpapi.Schema.Datatype.STRING

                                ' todo : discriminer sur les identifiants BBG (Isin, cusip, BBG code, etc)
                                Dim tmpData As baseMarketData =
                                    New marketData(Of String)(fld.GetValueAsString,
                                                              Now,
                                                              Now,
                                                              securityId,
                                                              dataProviders.bloomberg,
                                                              fld.ElementDefinition.Name.ToString)

                                data_.Add(tmpData)

                            Case Bloomberglp.Blpapi.Schema.Datatype.FLOAT64


                                Dim tmpData As baseMarketData =
                                    New marketData(Of Double)(fld.GetValueAsFloat64,
                                                              Now,
                                                              Now,
                                                              securityId,
                                                              dataProviders.bloomberg,
                                                              fld.ElementDefinition.Name.ToString)

                                data_.Add(tmpData)


                            Case Bloomberglp.Blpapi.Schema.Datatype.FLOAT32


                                Dim tmpData As baseMarketData =
                                    New marketData(Of Double)(fld.GetValueAsFloat32,
                                                              Now,
                                                              Now,
                                                              securityId,
                                                              dataProviders.bloomberg,
                                                              fld.ElementDefinition.Name.ToString)

                                data_.Add(tmpData)

                            Case Else

                                Dim ex As Exception =
                                    New wrapperException("bloomberg session not set")

                                Throw ex

                        End Select

                    Next

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
            New wrapperException("bloomberg session not set")

        Throw ex

    End Function

End Class
