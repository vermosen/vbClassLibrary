Public Class bloombergWrapper

    Inherits wrapper

    ' session bloomberg
    Private blpSession_ As Bloomberglp.Blpapi.Session
    Private blpSessionOptions_ As Bloomberglp.Blpapi.SessionOptions
    Private refDataServices_ As Bloomberglp.Blpapi.Service
    Private blpRequest_ As Bloomberglp.Blpapi.Request
    Private corr_ As Bloomberglp.Blpapi.CorrelationID

    ' variables locales
    Private feed_ As Boolean
    Private data_ As List(Of marketData)

    ' Elements
    Private vtSecurities_ As Bloomberglp.Blpapi.Element
    Private vtFields_ As Bloomberglp.Blpapi.Element

    ' constantes
    Private Const BBG_HOST As String = "localhost"
    Private Const BBG_SERVER_PORT As Long = 8194
    Private Const BBG_MISSING_FLD_CODE As String = "#N/A Fld"
    Private Const BBG_MISSING_SEC_CODE As String = "#N/A Sec"
    Private Const BBG_NA_FLD_CODE As String = "#N/A N Ap"
    Private Const BBG_ERROR_CODE As String = "#N/A N.A."

    ' events
    Public Event onUpdate(ByVal returnStr As List(Of String))

    Sub New(ByVal vtSecurities As securityIdentifierCollection,
            ByVal vtFields As List(Of String),
            Optional ByRef overRiddenFields As Object = Nothing,
            Optional ByRef overRiddenValues As Object = Nothing)

        ' setup des options de session
        blpSessionOptions_ = New Bloomberglp.Blpapi.SessionOptions()
        blpSessionOptions_.ServerHost = BBG_HOST
        blpSessionOptions_.ServerPort = BBG_SERVER_PORT

        ' setup de l'ID de la connection
        corr_ = New Bloomberglp.Blpapi.CorrelationID()
        data_ = New List(Of marketData)

        ' setup de la session
        blpSession_ = New Bloomberglp.Blpapi.Session(blpSessionOptions_,
                                                     New Bloomberglp.Blpapi.EventHandler(AddressOf processEvent))

        Dim startbool As Boolean = blpSession_.Start()

        If Not blpSession_.OpenService("//blp/refdata") Or Not startbool Then

            Dim ex As Exception =
                New Exception("bloomberg session not set")

            Throw ex

        End If


        refDataServices_ = blpSession_.GetService("//blp/refdata")
        blpRequest_ = refDataServices_.CreateRequest("ReferenceDataRequest")

        ' création des objects
        vtFields_ = blpRequest_.GetElement("fields")

        For Each fld As String In vtFields

            vtFields_.AppendValue(fld)

        Next

        vtSecurities_ = blpRequest_.GetElement("securities")

        For Each sec As securityIdentifier In vtSecurities

            vtSecurities_.AppendValue(sec.bloombergId)

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

            Case Bloomberglp.Blpapi.[Event].EventType.SUBSCRIPTION_STATUS

            Case Bloomberglp.Blpapi.[Event].EventType.RESPONSE

                Call storeData(eventObj, Bloomberglp.Blpapi.[Event].EventType.RESPONSE)

            Case Bloomberglp.Blpapi.[Event].EventType.PARTIAL_RESPONSE

                Call storeData(eventObj, Bloomberglp.Blpapi.[Event].EventType.PARTIAL_RESPONSE)

            Case Else

        End Select

    End Sub

    Private Sub storeData(ByVal eventObj As Bloomberglp.Blpapi.[Event],
                          ByVal status As Bloomberglp.Blpapi.[Event].EventType)

        ' on prend tous les messages de l'event
        For Each msg As Bloomberglp.Blpapi.Message In eventObj.GetMessages

            Dim securities As Bloomberglp.Blpapi.Element =
                msg.GetElement("securityData")

            ' toutes les securities
            For Each sec As Bloomberglp.Blpapi.Element In securities.Elements

                Dim fields As Bloomberglp.Blpapi.Element =
                    sec.GetElement("fieldData")

                ' tous les champs
                For Each fld As Bloomberglp.Blpapi.Element In fields.Elements

                    Select Case fields.

                        Case Double

                            Dim tempData As marketData = New quote(0.0, Now, Now)

                            data_.Add(tempData)

                    End Select

                Next

            Next

        Next

        Dim messages As Bloomberglp.Blpapi.Element =
            eventObj.GetMessages

    End Sub

End Class
