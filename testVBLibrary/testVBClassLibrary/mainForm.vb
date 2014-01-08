Imports vbClassLibrary

Public Class mainForm

    ' wrapper for chain upload
    Private WithEvents myChainWrapper_ As vbClassLibrary.dataWrapper

    ' wrapper for bonds upload
    Private WithEvents myDataWrapper_ As vbClassLibrary.bloombergWrapper

    ' for historical request
    Private startDate_ As Date
    Private endDate_ As Date

    ' designate current request
    Private static_ As Boolean

    ' is there ongoing process in the form ?
    Private processing_ As Boolean

    ' membres SQL
    Private conn_ As System.Data.SqlServerCe.SqlCeConnection

    ' delegates
    Private Delegate Sub printTextBoxDelegate(ByVal str As String)

    Public Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' initialisation des dates
        Me.startDateTimePicker.Value = Now
        Me.endDateTimePicker.Value = Now

        conn_ = New System.Data.SqlServerCe.SqlCeConnection(My.Settings.instrumentConnectionString)

        conn_.Open()

    End Sub

    Private Sub exitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        conn_.Close()

        Me.Close()

    End Sub

    Private Sub staticDataButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles staticDataButton.Click

        Try

            Me.textBox.Clear()


            If processing_ Then

                Dim intResult As Windows.Forms.DialogResult =
                    Windows.Forms.MessageBox.Show(
                        "a process in running currently, please wait...",
                        "An error has occured...",
                        Windows.Forms.MessageBoxButtons.OKCancel,
                        Windows.Forms.MessageBoxIcon.Information)

                Exit Sub

            End If

            ' start a process
            processing_ = True
            static_ = True

#If _REUTERS Then

            launchChainRequest("USTSY=")

#Else

            launchChainRequest("T")

#End If

        Catch ex As Exception

            Dim intResult As Windows.Forms.DialogResult =
                Windows.Forms.MessageBox.Show(
                    ex.Message, "An error has occured...",
                    Windows.Forms.MessageBoxButtons.OKCancel,
                    Windows.Forms.MessageBoxIcon.Information)

        End Try

    End Sub

    Private Sub historicalDataButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles historicalDataButton.Click

        Dim instrumentIds As vbClassLibrary.securityIdentifierCollection =
            New vbClassLibrary.securityIdentifierCollection

        If processing_ Then

            Dim intResult As Windows.Forms.DialogResult =
                Windows.Forms.MessageBox.Show(
                    "a process in running currently, please wait...",
                    "An error has occured...",
                    Windows.Forms.MessageBoxButtons.OKCancel,
                    Windows.Forms.MessageBoxIcon.Information)

            Exit Sub

        End If

        ' start a process
        processing_ = True
        static_ = False

        startDate_ = Me.startDateTimePicker.Value
        endDate_ = Me.endDateTimePicker.Value

        Try

            ' request instrument list
            Dim da As instrumentDataSetTableAdapters.TABLE_INSTRUMENTTableAdapter =
                New instrumentDataSetTableAdapters.TABLE_INSTRUMENTTableAdapter

            da.Connection = conn_

            Dim dt As instrumentDataSet.TABLE_INSTRUMENTDataTable =
                New instrumentDataSet.TABLE_INSTRUMENTDataTable

            ' recupération des données par intervalle
            da.activeBetweenDates(dt, endDate_, startDate_)

            For Each rec As instrumentDataSet.TABLE_INSTRUMENTRow In dt

                Dim isin As vbClassLibrary.securityIdentifier =
                    New vbClassLibrary.isin(rec.ISIN_ID, bloombergYellowKey.Govt)

                instrumentIds.Add(isin)

            Next

            launchHistoricalRequest(instrumentIds)

        Catch ex As Exception

            Dim intResult As Windows.Forms.DialogResult =
                Windows.Forms.MessageBox.Show(
                    ex.Message, "An error has occured...",
                    Windows.Forms.MessageBoxButtons.OKCancel,
                    Windows.Forms.MessageBoxIcon.Information)

        End Try

    End Sub

    Private Sub launchChainRequest(ByVal chain As String)

        Try

#If _REUTERS Then

            myChainWrapper_ = New vbClassLibrary.reutersChainWrapper(chain, vbClassLibrary.bloombergYellowKey.Govt)

#Else

            Dim tt As vbClassLibrary.securityIdentifierCollection = New vbClassLibrary.securityIdentifierCollection,
                t As vbClassLibrary.securityIdentifier = New vbClassLibrary.currentCode(chain, vbClassLibrary.bloombergYellowKey.Corp),
                flds As List(Of String) = New List(Of String)

            tt.Add(t)

            flds.Add("BOND CHAIN")

            myChainWrapper_ = New vbClassLibrary.bloombergChainWrapper(tt, flds)

#End If

        Catch ex As Exception

            Dim intResult As Windows.Forms.DialogResult =
                Windows.Forms.MessageBox.Show(
                    ex.Message,
                    "An error has occured...",
                    Windows.Forms.MessageBoxButtons.OKCancel,
                    Windows.Forms.MessageBoxIcon.Information)

        End Try

    End Sub

    Private Sub handleChainRequest(ByVal returnIds As baseMarketDataCollection) Handles myChainWrapper_.onUpdate

        ' reset du container

        Dim instruments_ As vbClassLibrary.securityIdentifierCollection =
            New vbClassLibrary.securityIdentifierCollection

        Try

            If returnIds.Count = 0 Then

                ' no result
                Dim intResult As Windows.Forms.DialogResult =
                    Windows.Forms.MessageBox.Show(
                        "No bond returned", "An error has occured...",
                        Windows.Forms.MessageBoxButtons.OKCancel,
                        Windows.Forms.MessageBoxIcon.Information)

            Else

                For Each id As marketData(Of String) In returnIds

                    If id.fieldName = "Bloomberg Identifier" Or id.fieldName = "Reuters Identifier" Then

                        ' convert id.value into a new identifier
                        Dim instrumentCode As vbClassLibrary.securityIdentifier =
                            New vbClassLibrary.currentCode(id.value,
                                                           vbClassLibrary.bloombergYellowKey.Govt)

                        instruments_.Add(instrumentCode)

                    End If

                Next

            End If

        Catch ex As Exception

            Dim intResult As Windows.Forms.DialogResult =
                Windows.Forms.MessageBox.Show(
                    ex.Message, "An error has occured...",
                    Windows.Forms.MessageBoxButtons.OKCancel,
                    Windows.Forms.MessageBoxIcon.Information)

        End Try

        launchBloombergStaticRequest(instruments_)

    End Sub

    Private Sub launchBloombergStaticRequest(ByVal instruments_ As vbClassLibrary.securityIdentifierCollection)

        Dim flds As List(Of String) = New List(Of String),
            results As List(Of String) = New List(Of String)

        Try

            flds.Add("ISSUE_DT")
            flds.Add("INT_ACC_DT")
            flds.Add("FIRST_CPN_DT")
            flds.Add("PENULTIMATE_CPN_DT")
            flds.Add("MATURITY")
            flds.Add("CPN")
            flds.Add("ID_ISIN")

            myDataWrapper_ =
                New vbClassLibrary.bloombergStaticWrapper(instruments_,
                                                          flds)

        Catch ex As Exception

            Dim intResult As Windows.Forms.DialogResult =
                Windows.Forms.MessageBox.Show(
                    ex.Message, "An error has occured...",
                    Windows.Forms.MessageBoxButtons.OKCancel,
                    Windows.Forms.MessageBoxIcon.Information)

        End Try

    End Sub

    Private Sub launchHistoricalRequest(ByVal returnIds As vbClassLibrary.securityIdentifierCollection)


        Dim flds As List(Of String) = New List(Of String),
            results As List(Of String) = New List(Of String)

        Try

            flds.Add("PX_BID")

            myDataWrapper_ =
                New vbClassLibrary.bloombergHistoryWrapper(returnIds,
                                                           flds,
                                                           startDate_,
                                                           endDate_)

        Catch ex As Exception

            Dim intResult As Windows.Forms.DialogResult =
                Windows.Forms.MessageBox.Show(
                    ex.Message, "An error has occured...",
                    Windows.Forms.MessageBoxButtons.OKCancel,
                    Windows.Forms.MessageBoxIcon.Information)

        End Try

    End Sub

    Private Sub handleDataWrapperResponse(
        ByVal data As vbClassLibrary.baseMarketDataCollection) Handles myDataWrapper_.onUpdate

        ' creates the instrument collection
        Dim bonds As vbClassLibrary.instrumentCollection =
            New vbClassLibrary.instrumentCollection

        Try

            ' select process
            If static_ = True Then

                ' linq request on Isins
                Dim arr = From dt As vbClassLibrary.baseMarketData In data
                          Where dt.fieldName = "ID_ISIN"
                          Select dt

                ' for each ISIN in the list
                For Each str As vbClassLibrary.marketData(Of String) In arr

                    Dim codes As vbClassLibrary.securityIdentifierCollection =
                        New vbClassLibrary.securityIdentifierCollection

                    Dim bondIsin As vbClassLibrary.securityIdentifier =
                        New vbClassLibrary.isin(str.value, vbClassLibrary.bloombergYellowKey.Govt)

                    codes.Add(bondIsin)

                    ' get all the related fields
                    Dim elements = From dt As vbClassLibrary.baseMarketData In data
                                   Where dt.instrumentId = str.instrumentId
                                   Select dt

                    Dim issueDate = From dt As vbClassLibrary.baseMarketData In elements
                                    Where dt.fieldName = "ISSUE_DT"
                                    Select dt

                    Dim maturityDate = From dt As vbClassLibrary.baseMarketData In elements
                                       Where dt.fieldName = "MATURITY"
                                       Select dt

                    Dim effectiveDate = From dt As vbClassLibrary.baseMarketData In elements
                                        Where dt.fieldName = "INT_ACC_DT"
                                        Select dt

                    Dim firstCouponDate = From dt As vbClassLibrary.baseMarketData In elements
                                          Where dt.fieldName = "FIRST_CPN_DT"
                                          Select dt

                    Dim penultimateDate = From dt As vbClassLibrary.baseMarketData In elements
                                          Where dt.fieldName = "PENULTIMATE_CPN_DT"
                                          Select dt

                    Dim cpn = From dt As vbClassLibrary.baseMarketData In elements
                              Where dt.fieldName = "CPN"
                              Select dt

                    Dim temp As vbClassLibrary.bond
                    Dim cpnValue As Double = DirectCast(cpn(0), vbClassLibrary.marketData(Of Double)).value / 100

                    ' depends on the fields that have been retrieved
                    If cpnValue = 0.0 Then

                        temp = New vbClassLibrary.zeroCouponBond(codes,
                            DirectCast(issueDate(0), vbClassLibrary.marketData(Of Date)).value,
                            DirectCast(issueDate(0), vbClassLibrary.marketData(Of Date)).value,
                            DirectCast(maturityDate(0), vbClassLibrary.marketData(Of Date)).value)

                    Else

                        temp = New vbClassLibrary.fixedRateBond(codes,
                            DirectCast(issueDate(0), vbClassLibrary.marketData(Of Date)).value,
                            DirectCast(effectiveDate(0), vbClassLibrary.marketData(Of Date)).value,
                            DirectCast(maturityDate(0), vbClassLibrary.marketData(Of Date)).value,
                            cpnValue,
                            DirectCast(firstCouponDate(0), vbClassLibrary.marketData(Of Date)).value,
                            DirectCast(penultimateDate(0), vbClassLibrary.marketData(Of Date)).value)

                    End If

                    ' add to the collection
                    bonds.Add(temp)

                Next

                ' insert bonds into db
                insertInstrument(bonds)

            Else

                insertQuote(data)

            End If

            processing_ = False
            

        Catch ex As Exception

            Dim intResult As Windows.Forms.DialogResult =
                Windows.Forms.MessageBox.Show(
                    ex.Message, "An error has occured...",
                    Windows.Forms.MessageBoxButtons.OKCancel,
                    Windows.Forms.MessageBoxIcon.Information)

        End Try

    End Sub

    Private Sub insertQuote(ByRef quotes As vbClassLibrary.baseMarketDataCollection)

        ' test ATL
        'Dim test As quantlibATLLib.fixedRateBondFactory =
        '    New quantlibATLLib.fixedRateBondFactory

        ' checks if there is an existing quote
        'Dim da As quoteDataSet2TableAdapters.TABLE_INSTRUMENTTableAdapter =
        '    New quoteDataSet2TableAdapters.TABLE_INSTRUMENTTableAdapter

        'da.Connection = conn_

        'Dim dt As quoteDataSet2.TABLE_INSTRUMENTDataTable =
        '    New quoteDataSet2.TABLE_INSTRUMENTDataTable

        '' recupération des données par intervalle
        'da.Fill(dt)

        '' test 
        ''da.Insert("XX0000000001", Now, 0.0, vbClassLibrary.dataProviders.bloomberg)

        'For Each i As vbClassLibrary.marketData(Of Double) In quotes

        '    ' linq request on Isins
        '    Dim arr = From rw As quoteDataSet.quoteDataTableRow In dt
        '              Where rw.QUOTE_DATE = i.dataTime _
        '              And rw.QUOTE_SOURCE = vbClassLibrary.dataProviders.bloomberg _
        '              And rw.ISIN_ID = i.instrumentId.id
        '              Select rw

        '    If arr.Count = 0 Then

        '        ' no previous quote -> insert
        '        'da.Insert()

        '    Else

        '        ' there is a previous quote on Isin/date/source

        '    End If

        'Next''

    End Sub

    Private Sub insertInstrument(ByVal bonds As vbClassLibrary.instrumentCollection)

        Try

            'TODO: controle sur les valeur, sinon update
            Dim da As instrumentDataSetTableAdapters.TABLE_INSTRUMENTTableAdapter =
                New instrumentDataSetTableAdapters.TABLE_INSTRUMENTTableAdapter

            da.Connection = conn_

            Dim dt As instrumentDataSet.TABLE_INSTRUMENTDataTable =
                New instrumentDataSet.TABLE_INSTRUMENTDataTable

            ' recupération des données par intervalle
            da.Fill(dt)

            ' toujours des données numériques en retour
            For Each b As vbClassLibrary.bond In bonds

                ' la donnée existe dejà ?
                Dim rows() As System.Data.DataRow =
                    dt.Select("ISIN_ID = '" &
                              b.identifiers(0).id.ToString & "'")

                If rows.Count = 0 Then

                    ' information
                    Invoke(New printTextBoxDelegate(AddressOf printTextBox), "new bond imported : " &
                           b.identifiers(0).id &
                           ", issue date : " &
                           Format(b.issueDate, "MM-dd-yyyy") &
                           ", maturity : " &
                           b.maturityDate)

                    ' insertion dans la base
                    If TypeOf b Is vbClassLibrary.fixedRateBond Then

                        Dim temp As vbClassLibrary.fixedRateBond =
                            DirectCast(b, vbClassLibrary.fixedRateBond)

                        da.InsertSingleBond(DirectCast(temp.identifiers(0), vbClassLibrary.isin).id,
                                            temp.issueDate,
                                            temp.maturityDate,
                                            temp.effectiveDate,
                                            temp.firstCouponDate,
                                            temp.lastCouponDate,
                                            temp.couponRate)

                    ElseIf TypeOf b Is vbClassLibrary.zeroCouponBond Then

                        Dim temp As vbClassLibrary.zeroCouponBond =
                            DirectCast(b, vbClassLibrary.zeroCouponBond)

                        Dim nd As Nullable(Of Date) = Nothing

                        da.InsertSingleBond(DirectCast(temp.identifiers(0), vbClassLibrary.isin).id,
                                            temp.issueDate,
                                            temp.maturityDate,
                                            temp.effectiveDate,
                                            nd,
                                            nd,
                                            0.0)

                    Else

                        Dim ex As Exception = New Exception("unsupported instrument")

                        Throw ex

                    End If

                End If

            Next b

            da.Update(dt)

        Catch ex As Exception

            Dim intResult As Windows.Forms.DialogResult =
                Windows.Forms.MessageBox.Show(
                    ex.Message, "An error has occured...",
                    Windows.Forms.MessageBoxButtons.OKCancel,
                    Windows.Forms.MessageBoxIcon.Information)

        End Try

    End Sub

    Private Sub printTextBox(ByVal line As String)

        Me.textBox.AppendText(line & vbNewLine)

    End Sub

    Private Sub mainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'TODO: cette ligne de code charge les données dans la table 'InstrumentDataSet.TABLE_INSTRUMENT'. Vous pouvez la déplacer ou la supprimer selon vos besoins.
        Me.TABLE_INSTRUMENTTableAdapter.Fill(Me.InstrumentDataSet.TABLE_INSTRUMENT)

    End Sub

    Private Sub TABLEINSTRUMENTBindingSource_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TABLEINSTRUMENTBindingSource.CurrentChanged

    End Sub

End Class