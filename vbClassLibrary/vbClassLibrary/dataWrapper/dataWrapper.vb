Public MustInherit Class dataWrapper

    ' base class for data wrapper
    Protected data_ As vbClassLibrary.baseMarketDataCollection

    ' onUpdate event
    Public Event onUpdate(ByVal data As vbClassLibrary.baseMarketDataCollection)

    ' finalizeData for asyncrone purpose
    Protected Delegate Sub finalizeDataDelegate(ByVal e As baseMarketDataCollection)

    ' delegate instance
    Protected dlg_ As finalizeDataDelegate

    ' feed indicator
    Protected feeded_ As Boolean

    Protected updated_ As Boolean

    Public Overridable Sub sendRequest()

    End Sub

    ReadOnly Property updated As Boolean

        Get

            Return updated_

        End Get

    End Property

    ReadOnly Property validData() As vbClassLibrary.baseMarketDataCollection

        Get

            Return data_

        End Get

    End Property

    Protected Overridable Sub onUpdateTrigger(ByVal e As vbClassLibrary.baseMarketDataCollection)

        updated_ = True

        RaiseEvent onUpdate(e)

    End Sub

End Class

Public Class wrapperException

    Inherits System.Exception

    Public Sub New()

        MyBase.New()

    End Sub

    Public Sub New(ByVal mess As String)

        MyBase.New(mess)

    End Sub

    Public Sub New(ByVal mess As String, ByVal ex As System.Exception)

        MyBase.New(mess, ex)

    End Sub

    Public Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo,
                   ByVal context As System.Runtime.Serialization.StreamingContext)

        MyBase.New(info, context)

    End Sub

End Class

Public Class dataWrapperCollection

    Inherits Collections.CollectionBase

    Private requesting_ As Boolean

    ' update and provide the data...
    Public Event onUpdate(ByVal data As vbClassLibrary.baseMarketDataCollection)

    Public Sub sendRequest()

        requesting_ = True

        For Each i As dataWrapper In Me

            i.sendRequest()

        Next

    End Sub

    Default Public Property Item(ByVal index As Integer) As dataWrapper

        Get

            Return CType(List(index), dataWrapper)

        End Get

        Set(ByVal value As dataWrapper)

            ' if blocked throw
            If requesting_ = True Then

                Dim ex As Exception =
                    New Exception("can't extend the collection while updating")

                Throw (ex)

            Else

                List(index) = value

            End If

        End Set

    End Property

    Public Function Add(ByVal value As dataWrapper) As Integer

        ' if blocked throw
        If requesting_ = True Then

            Dim ex As Exception =
                New Exception("can't extend the collection while updating")

            Throw (ex)

        Else

            Return List.Add(value)

        End If

    End Function 'Add

    Public Function IndexOf(ByVal value As dataWrapper) As Integer

        Return List.IndexOf(value)

    End Function 'IndexOf


    Public Sub Insert(ByVal index As Integer, ByVal value As dataWrapper)

        ' if blocked throw
        If requesting_ = True Then

            Dim ex As Exception =
                New Exception("can't extend the collection while updating")

            Throw (ex)

        Else

            List.Insert(index, value)

        End If

    End Sub 'Insert

    Public Sub Remove(ByVal value As dataWrapper)

        ' if blocked throw
        If requesting_ = True Then

            Dim ex As Exception =
                New Exception("can't extend the collection while updating")

            Throw (ex)

        Else

            List.Remove(value)

        End If

    End Sub 'Remove

    Public Function Contains(ByVal value As dataWrapper) As Boolean

        ' If value is not of type bond, this will return false.
        Return List.Contains(value)

    End Function 'Contains

    Protected Overrides Sub OnValidate(ByVal value As Object)

        If Not GetType(dataWrapper).IsAssignableFrom(value.GetType()) Then

            Throw New ArgumentException("value must be of type dataWrapper.", "value")

        End If

    End Sub 'OnValidate 

End Class





'Sub Main()
'    Dim manager As New DeviceManager
'    manager.AddDevice(New Device("First"))
'    manager.AddDevice(New Device("Second"))
'    manager.AddDevice(New Device("Third"))
'    manager.AddDevice(New Device("Fourth"))
'    Console.ReadLine()
'End Sub

'Friend Class DeviceManager
'    Private _deviceCollection As DeviceCollection

'    Public Sub New()
'        _deviceCollection = New DeviceCollection()
'AddHandler _deviceCollection.StatusChanged, AddressOf
'        StatusChangedHandler()
'    End Sub

'    Public Sub AddDevice(ByVal d As Device)
'        _deviceCollection.Add(d)
'    End Sub

'Private Sub StatusChangedHandler(ByVal sender As Object, ByVal
'e As EventArgs)
'        Console.WriteLine(CType(sender, Device).Name)
'    End Sub
'End Class

'Friend Class DeviceCollection
'    Implements ICollection(Of Device)
'    Private _devices As New List(Of Device)

'    Public Event StatusChanged As EventHandler

'Public Sub Add(ByVal item As Device) Implements
'        System.Collections.Generic.ICollection(Of Device).Add()
'AddHandler item.StatusChanged, AddressOf
'        StatusChangedHandler()
'        _devices.Add(item)
'    End Sub

'Public Sub Clear() Implements
'        System.Collections.Generic.ICollection(Of Device).Clear()
'        For Each item As Device In _devices
'RemoveHandler item.StatusChanged, AddressOf
'            StatusChangedHandler()
'        Next
'        _devices.Clear()
'    End Sub

'    Public Function Contains(ByVal item As Device) As Boolean
'Implements System.Collections.Generic.ICollection(Of Device).Contains
'        Return _devices.Contains(item)
'    End Function

'Public Sub CopyTo(ByVal array() As Device, ByVal arrayIndex As
'Integer) Implements System.Collections.Generic.ICollection(Of
'Device).CopyTo
'        _devices.CopyTo(array, arrayIndex)
'    End Sub

'Public ReadOnly Property Count() As Integer Implements
'System.Collections.Generic.ICollection(Of Device).Count
'        Get
'            Return _devices.Count
'        End Get
'    End Property

'Public ReadOnly Property IsReadOnly() As Boolean Implements
'System.Collections.Generic.ICollection(Of Device).IsReadOnly
'        Get
'            Return False
'        End Get
'    End Property

'    Public Function Remove(ByVal item As Device) As Boolean
'Implements System.Collections.Generic.ICollection(Of Device).Remove
'        If _devices.Contains(item) Then
'RemoveHandler item.StatusChanged, AddressOf
'            StatusChangedHandler()
'            _devices.Remove(item)
'        End If
'    End Function

'Public Function GetEnumerator() As
'System.Collections.Generic.IEnumerator(Of Device) Implements
'        System.Collections.Generic.IEnumerable(Of Device).GetEnumerator()
'        Return _devices.GetEnumerator()
'    End Function

'Public Function GetEnumerator1() As
'System.Collections.IEnumerator Implements
'        System.Collections.IEnumerable.GetEnumerator()
'        Return _devices.GetEnumerator()
'    End Function

'Private Sub StatusChangedHandler(ByVal sender As Object, ByVal
'e As EventArgs)
'        RaiseEvent StatusChanged(sender, e)
'    End Sub
'End Class

'Friend Class Device
'    Private _name As String
'    Public Event StatusChanged As EventHandler
'    Private _timer As System.Threading.Timer

'    Public Sub New(ByVal name As String)
'        _name = name
'        _timer = New System.Threading.Timer(AddressOf TickHandler,
'        Nothing, 1000, 1000)
'    End Sub

'    Public ReadOnly Property Name() As String
'        Get
'            Return _name
'        End Get
'    End Property

'    Private Sub TickHandler(ByVal state As Object)
'        RaiseEvent StatusChanged(Me, New System.EventArgs())
'    End Sub
'End Class
'End Module







