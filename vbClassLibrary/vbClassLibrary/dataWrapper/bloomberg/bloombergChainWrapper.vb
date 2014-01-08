Public Class bloombergChainWrapper

    Inherits bloombergWrapper

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

                End If

                Dim fieldDataArray As Bloomberglp.Blpapi.Element =
                    securityData.GetElement("fieldData")

                For Each fld As Bloomberglp.Blpapi.Element In fieldDataArray.Elements

                    ' bulk data : double count sur les valeurs et les champs de la sequence
                    If fld.Datatype = Bloomberglp.Blpapi.Schema.Datatype.SEQUENCE Then

                        For index As Long = 0 To fld.NumValues - 1

                            Dim bulkElement As Bloomberglp.Blpapi.Element = fld.GetValue(index)

                            For Each fldsElem As Bloomberglp.Blpapi.Element In bulkElement.Elements

                                ' selon le type de donnée
                                Select Case fldsElem.Datatype

                                    Case Bloomberglp.Blpapi.Schema.Datatype.STRING

                                        Dim tmpData As baseMarketData =
                                            New marketData(Of String)(fldsElem.GetValueAsString,
                                                                        Now,
                                                                        Now,
                                                                        securityId,
                                                                        dataProviders.bloomberg,
                                                                        fldsElem.ElementDefinition.Name.ToString)

                                        data_.Add(tmpData)

                                    Case Else

                                        Dim ex As Exception =
                                            New wrapperException("incorrect data type")

                                        Throw ex

                                End Select


                            Next

                        Next

                    Else

                        Dim ex As Exception =
                            New wrapperException("incorrect data type")

                        Throw ex

                    End If

                Next

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
            New wrapperException("unknown instrument return")

        Throw ex

    End Function

End Class
