Option Explicit On

Public MustInherit Class bloombergWrapper

    Inherits dataWrapper

    ' session bloomberg
    Protected blpSession_ As Bloomberglp.Blpapi.Session
    Protected blpSessionOptions_ As Bloomberglp.Blpapi.SessionOptions
    Protected refDataServices_ As Bloomberglp.Blpapi.Service
    Protected blpRequest_ As Bloomberglp.Blpapi.Request
    Protected corr_ As Bloomberglp.Blpapi.CorrelationID

    ' les champs et instruments de la requête
    Protected vtFields_ As List(Of String)
    Protected vtSecurities_ As securityIdentifierCollection

    ' pas encore fonctionnel, a tester
    Private overrideFields_ As List(Of String)
    Private overrideValues_ As List(Of Object)

    ' constantes
    Protected Const BBG_HOST As String = "localhost"
    Protected Const BBG_SERVER_PORT As Long = 8194
    Protected Const BBG_MISSING_FLD_CODE As String = "#N/A Fld"
    Protected Const BBG_MISSING_SEC_CODE As String = "#N/A Sec"
    Protected Const BBG_NA_FLD_CODE As String = "#N/A N Ap"
    Protected Const BBG_ERROR_CODE As String = "#N/A N.A."

    Protected Sub New(ByVal vtSecurities As securityIdentifierCollection,
                      ByVal vtFields As List(Of String),
                      Optional ByRef overrideFields As List(Of String) = Nothing,
                      Optional ByRef overrideValues As List(Of Object) = Nothing)

        vtFields_ = vtFields
        vtSecurities_ = vtSecurities
        overrideFields_ = overrideFields
        overrideValues_ = overrideValues

        ' session options
        blpSessionOptions_ = New Bloomberglp.Blpapi.SessionOptions()
        blpSessionOptions_.ServerHost = BBG_HOST
        blpSessionOptions_.ServerPort = BBG_SERVER_PORT

        ' connection Id
        corr_ = New Bloomberglp.Blpapi.CorrelationID
        data_ = New baseMarketDataCollection
        dlg_ = New finalizeDataDelegate(AddressOf Me.onUpdateTrigger)

        ' session setup
        blpSession_ = New Bloomberglp.Blpapi.Session(blpSessionOptions_,
                                                     New Bloomberglp.Blpapi.EventHandler(AddressOf processEvent))

        Dim startbool As Boolean = blpSession_.Start()

        If Not blpSession_.OpenService("//blp/refdata") Or Not startbool Then

            Dim ex As Exception =
                New wrapperException("bloomberg session not set")

            Throw ex

        End If

        refDataServices_ = blpSession_.GetService("//blp/refdata")

    End Sub

    Public Overrides Sub sendRequest()

        Dim tempFlds As Bloomberglp.Blpapi.Element = blpRequest_.GetElement("fields")

        ' création des objects
        For Each fld As String In vtFields_

            tempFlds.AppendValue(fld)

        Next

        Dim tempSecs As Bloomberglp.Blpapi.Element = blpRequest_.GetElement("securities")

        For Each sec As securityIdentifier In vtSecurities_

            tempSecs.AppendValue(sec.bloombergId)

        Next

        blpSession_.SendRequest(blpRequest_, corr_)

    End Sub

    Protected Overrides Sub finalize()

        blpSession_ = Nothing
        blpSessionOptions_ = Nothing
        refDataServices_ = Nothing
        blpRequest_ = Nothing

    End Sub

    Public Sub processEvent(ByVal eventObj As Bloomberglp.Blpapi.[Event], ByVal session As Bloomberglp.Blpapi.Session)

        Select Case eventObj.Type

            Case Bloomberglp.Blpapi.[Event].EventType.REQUEST_STATUS

                Dim ex As Exception =
                    New wrapperException("request status:" & Bloomberglp.Blpapi.[Event].EventType.REQUEST_STATUS)

            Case Bloomberglp.Blpapi.[Event].EventType.SUBSCRIPTION_STATUS

                MsgBox(Bloomberglp.Blpapi.[Event].EventType.SUBSCRIPTION_STATUS)

            Case Bloomberglp.Blpapi.[Event].EventType.SESSION_STATUS

                ' do nothing ? log ?

            Case Bloomberglp.Blpapi.[Event].EventType.SERVICE_STATUS

                ' do nothing ? log ?

            Case Bloomberglp.Blpapi.[Event].EventType.RESPONSE

                Call storeData(eventObj,
                               Bloomberglp.Blpapi.[Event].EventType.RESPONSE)
                feeded_ = True
                dlg_.Invoke(data_)

            Case Bloomberglp.Blpapi.[Event].EventType.PARTIAL_RESPONSE

                Call storeData(eventObj, Bloomberglp.Blpapi.[Event].EventType.PARTIAL_RESPONSE)

            Case Else

                Dim ex As Exception =
                    New wrapperException("unknown process result")

                Throw ex

        End Select

    End Sub

    Protected MustOverride Sub storeData(ByVal eventObj As Bloomberglp.Blpapi.[Event],
                                         ByVal status As Bloomberglp.Blpapi.[Event].EventType)

End Class
