#If _REUTERS Then

Public MustInherit Class reutersWrapper

    Inherits dataWrapper
    'This property describes how the Data property is to present its results. It is a string which should
    'contain a sequence of parameter settings. The allowed parameters are:
    'IGNE : The Ignore Empty parameter determines whether empty elements of the
    'returned chain contents are removed.
    'IGNE:YES remove empty entries from the array.
    'IGNE:NO leave empty entries in the array.
    'LAY : The Layout parameter determines how the elements will be arranged in the
    'returned array.
    'LAY:HOR or LAY:H returns items in a horizontal layout.
    'LAY:VER or LAY:V returns items in a vertical layout.
    'LIVE : The Live parameter determines whether the chain should continue to be
    'supplied with updates once the initial chain data have been fully received.
    'This allows an application to decide whether to track changes to the contents
    'of the chain coming from the data source.
    'LIVE:YES continue to receive updates once the chain has been
    'received completely.
    'LIVE:NO supply no more updates once the chain data are
    'complete (the default).
    'RET : The Return parameter allows the size of the returned array to be controlled.
    'RET:An return the first n entries of the array as an array.
    'RET:n return only the nth entry of the array (entries are
    'numbered from 1).
    'By default no return array size is defined.
    'SKIP : The Skip parameter allows various entries in the chain to be skipped before
    'returning a reduced array. Entries are numbered from 1.
    'SKIP:n remove the nth entry from the array.
    'SKIP:i-j remove all entries from the ith to the jth (inclusive) from
    'the array.
    'SKIP:i-j,k different entries or ranges to skip can be combined in
    'a comma-separated list.
    'By default no elements are skipped. Often the first few entries of a chain do
    'not contain item names.
    'UWC : The Update When Completed parameter determines whether update events
    'will be signalled to the application through the OnUpdate event callback
    'while the different constituent parts of the chain are being retrieved.
    'UWC:NO signal an update event for the retrieval of each part of
    'the chain.
    'UWC:YES wait until the chain has been retrieved completely
    'before generating an update
    Protected ignore_ As Boolean
    Protected live_ As Boolean
    Protected updateWhenComplete_ As Boolean

    ' source
    Protected Const RT_DEFAULT_SOURCE As String = "IDN"

    ' error string
    Protected Const RT_ERROR As String = "#N/A N/A"

    Protected Sub New(Optional ByVal ignore As Boolean = True,
                      Optional ByVal updateWhenComplete As Boolean = True,
                      Optional ByVal live As Boolean = False)

        MyBase.new()

        ignore_ = ignore
        updateWhenComplete_ = updateWhenComplete
        updateWhenComplete_ = False
        live_ = live

    End Sub

    Protected Function rtDefaultMode() As String

        Dim returnStr As String = "IGNE:"

        If ignore_ = True Then

            returnStr = returnStr & "YES"

        Else

            returnStr = returnStr & "NO"

        End If

        returnStr = returnStr & " UWC:"

        If updateWhenComplete_ = True Then

            returnStr = returnStr & "YES"

        Else

            returnStr = returnStr & "NO"

        End If

        returnStr = returnStr & " LIVE:"

        If live_ = True Then

            returnStr = returnStr & "YES"

        Else

            returnStr = returnStr & "NO"

        End If

        Return returnStr

    End Function

    Public Property ignore() As Boolean

        Get

            Return ignore_

        End Get

        Set(value As Boolean)

            ignore_ = value

        End Set

    End Property

    Public Property live() As Boolean

        Get

            Return live_

        End Get

        Set(value As Boolean)

            live_ = value

        End Set

    End Property

    Public Property updateWhenComplete() As Boolean

        Get

            Return updateWhenComplete_

        End Get

        Set(value As Boolean)

            updateWhenComplete_ = value

        End Set

    End Property

End Class

#end if
