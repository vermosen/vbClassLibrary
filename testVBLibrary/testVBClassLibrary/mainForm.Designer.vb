<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mainForm
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainForm))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.startDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.startDateLabel = New System.Windows.Forms.Label()
        Me.endDateLabel = New System.Windows.Forms.Label()
        Me.endDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.menuTab = New System.Windows.Forms.TabControl()
        Me.downloadPage = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.staticDataButton = New System.Windows.Forms.Button()
        Me.historicalDataButton = New System.Windows.Forms.Button()
        Me.viewPage = New System.Windows.Forms.TabPage()
        Me.instrumentDataGridView = New System.Windows.Forms.DataGridView()
        Me.ISINIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ISSUEDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EFFECTIVEDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FIRSTCPNDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LASTCPNDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MATURITYDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CPNDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TABLEINSTRUMENTBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.InstrumentDataSet = New testVBClassLibrary.instrumentDataSet()
        Me.TABLE_INSTRUMENTTableAdapter = New testVBClassLibrary.instrumentDataSetTableAdapters.TABLE_INSTRUMENTTableAdapter()
        Me.textBox = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ToolStrip1.SuspendLayout()
        Me.menuTab.SuspendLayout()
        Me.downloadPage.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.viewPage.SuspendLayout()
        CType(Me.instrumentDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TABLEINSTRUMENTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InstrumentDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(543, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox1})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(29, 22)
        Me.ToolStripDropDownButton1.Text = "ToolStripDropDownButton1"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(100, 21)
        '
        'startDateTimePicker
        '
        Me.startDateTimePicker.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.startDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.startDateTimePicker.Location = New System.Drawing.Point(6, 32)
        Me.startDateTimePicker.Name = "startDateTimePicker"
        Me.startDateTimePicker.Size = New System.Drawing.Size(121, 20)
        Me.startDateTimePicker.TabIndex = 1
        '
        'startDateLabel
        '
        Me.startDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.startDateLabel.AutoSize = True
        Me.startDateLabel.Location = New System.Drawing.Point(11, 16)
        Me.startDateLabel.Name = "startDateLabel"
        Me.startDateLabel.Size = New System.Drawing.Size(78, 13)
        Me.startDateLabel.TabIndex = 5
        Me.startDateLabel.Text = "Date de départ"
        '
        'endDateLabel
        '
        Me.endDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.endDateLabel.AutoSize = True
        Me.endDateLabel.Location = New System.Drawing.Point(11, 55)
        Me.endDateLabel.Name = "endDateLabel"
        Me.endDateLabel.Size = New System.Drawing.Size(59, 13)
        Me.endDateLabel.TabIndex = 7
        Me.endDateLabel.Text = "Date de fin"
        '
        'endDateTimePicker
        '
        Me.endDateTimePicker.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.endDateTimePicker.Location = New System.Drawing.Point(6, 71)
        Me.endDateTimePicker.Name = "endDateTimePicker"
        Me.endDateTimePicker.Size = New System.Drawing.Size(121, 20)
        Me.endDateTimePicker.TabIndex = 2
        '
        'menuTab
        '
        Me.menuTab.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.menuTab.Controls.Add(Me.downloadPage)
        Me.menuTab.Controls.Add(Me.viewPage)
        Me.menuTab.Location = New System.Drawing.Point(12, 38)
        Me.menuTab.Name = "menuTab"
        Me.menuTab.SelectedIndex = 0
        Me.menuTab.Size = New System.Drawing.Size(519, 225)
        Me.menuTab.TabIndex = 8
        Me.menuTab.TabStop = False
        '
        'downloadPage
        '
        Me.downloadPage.Controls.Add(Me.GroupBox2)
        Me.downloadPage.Controls.Add(Me.GroupBox1)
        Me.downloadPage.Location = New System.Drawing.Point(4, 22)
        Me.downloadPage.Name = "downloadPage"
        Me.downloadPage.Padding = New System.Windows.Forms.Padding(3)
        Me.downloadPage.Size = New System.Drawing.Size(511, 199)
        Me.downloadPage.TabIndex = 1
        Me.downloadPage.Text = "Download"
        Me.downloadPage.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.endDateLabel)
        Me.GroupBox1.Controls.Add(Me.startDateLabel)
        Me.GroupBox1.Controls.Add(Me.historicalDataButton)
        Me.GroupBox1.Controls.Add(Me.endDateTimePicker)
        Me.GroupBox1.Controls.Add(Me.startDateTimePicker)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(133, 187)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Historical request"
        '
        'staticDataButton
        '
        Me.staticDataButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.staticDataButton.Location = New System.Drawing.Point(6, 160)
        Me.staticDataButton.Name = "staticDataButton"
        Me.staticDataButton.Size = New System.Drawing.Size(121, 21)
        Me.staticDataButton.TabIndex = 3
        Me.staticDataButton.TabStop = False
        Me.staticDataButton.Text = "static data"
        Me.staticDataButton.UseVisualStyleBackColor = True
        '
        'historicalDataButton
        '
        Me.historicalDataButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.historicalDataButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.historicalDataButton.Location = New System.Drawing.Point(6, 160)
        Me.historicalDataButton.Name = "historicalDataButton"
        Me.historicalDataButton.Size = New System.Drawing.Size(121, 21)
        Me.historicalDataButton.TabIndex = 4
        Me.historicalDataButton.TabStop = False
        Me.historicalDataButton.Text = "historical data"
        Me.historicalDataButton.UseVisualStyleBackColor = True
        '
        'viewPage
        '
        Me.viewPage.Controls.Add(Me.instrumentDataGridView)
        Me.viewPage.Location = New System.Drawing.Point(4, 22)
        Me.viewPage.Name = "viewPage"
        Me.viewPage.Size = New System.Drawing.Size(511, 199)
        Me.viewPage.TabIndex = 2
        Me.viewPage.Text = "View"
        Me.viewPage.UseVisualStyleBackColor = True
        '
        'instrumentDataGridView
        '
        Me.instrumentDataGridView.AllowUserToAddRows = False
        Me.instrumentDataGridView.AllowUserToDeleteRows = False
        Me.instrumentDataGridView.AllowUserToOrderColumns = True
        Me.instrumentDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.instrumentDataGridView.AutoGenerateColumns = False
        Me.instrumentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.instrumentDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ISINIDDataGridViewTextBoxColumn, Me.ISSUEDATEDataGridViewTextBoxColumn, Me.EFFECTIVEDATEDataGridViewTextBoxColumn, Me.FIRSTCPNDATEDataGridViewTextBoxColumn, Me.LASTCPNDATEDataGridViewTextBoxColumn, Me.MATURITYDATEDataGridViewTextBoxColumn, Me.CPNDataGridViewTextBoxColumn})
        Me.instrumentDataGridView.DataSource = Me.TABLEINSTRUMENTBindingSource
        Me.instrumentDataGridView.Location = New System.Drawing.Point(0, 3)
        Me.instrumentDataGridView.Name = "instrumentDataGridView"
        Me.instrumentDataGridView.ReadOnly = True
        Me.instrumentDataGridView.Size = New System.Drawing.Size(508, 196)
        Me.instrumentDataGridView.TabIndex = 0
        '
        'ISINIDDataGridViewTextBoxColumn
        '
        Me.ISINIDDataGridViewTextBoxColumn.DataPropertyName = "ISIN_ID"
        Me.ISINIDDataGridViewTextBoxColumn.HeaderText = "Isin"
        Me.ISINIDDataGridViewTextBoxColumn.Name = "ISINIDDataGridViewTextBoxColumn"
        Me.ISINIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ISSUEDATEDataGridViewTextBoxColumn
        '
        Me.ISSUEDATEDataGridViewTextBoxColumn.DataPropertyName = "ISSUE_DATE"
        Me.ISSUEDATEDataGridViewTextBoxColumn.HeaderText = "Issue Date"
        Me.ISSUEDATEDataGridViewTextBoxColumn.Name = "ISSUEDATEDataGridViewTextBoxColumn"
        Me.ISSUEDATEDataGridViewTextBoxColumn.ReadOnly = True
        '
        'EFFECTIVEDATEDataGridViewTextBoxColumn
        '
        Me.EFFECTIVEDATEDataGridViewTextBoxColumn.DataPropertyName = "EFFECTIVE_DATE"
        Me.EFFECTIVEDATEDataGridViewTextBoxColumn.HeaderText = "Effective Date"
        Me.EFFECTIVEDATEDataGridViewTextBoxColumn.Name = "EFFECTIVEDATEDataGridViewTextBoxColumn"
        Me.EFFECTIVEDATEDataGridViewTextBoxColumn.ReadOnly = True
        '
        'FIRSTCPNDATEDataGridViewTextBoxColumn
        '
        Me.FIRSTCPNDATEDataGridViewTextBoxColumn.DataPropertyName = "FIRST_CPN_DATE"
        Me.FIRSTCPNDATEDataGridViewTextBoxColumn.HeaderText = "First Coupon Date"
        Me.FIRSTCPNDATEDataGridViewTextBoxColumn.Name = "FIRSTCPNDATEDataGridViewTextBoxColumn"
        Me.FIRSTCPNDATEDataGridViewTextBoxColumn.ReadOnly = True
        '
        'LASTCPNDATEDataGridViewTextBoxColumn
        '
        Me.LASTCPNDATEDataGridViewTextBoxColumn.DataPropertyName = "LAST_CPN_DATE"
        Me.LASTCPNDATEDataGridViewTextBoxColumn.HeaderText = "Last Coupon Date"
        Me.LASTCPNDATEDataGridViewTextBoxColumn.Name = "LASTCPNDATEDataGridViewTextBoxColumn"
        Me.LASTCPNDATEDataGridViewTextBoxColumn.ReadOnly = True
        '
        'MATURITYDATEDataGridViewTextBoxColumn
        '
        Me.MATURITYDATEDataGridViewTextBoxColumn.DataPropertyName = "MATURITY_DATE"
        Me.MATURITYDATEDataGridViewTextBoxColumn.HeaderText = "Maturity"
        Me.MATURITYDATEDataGridViewTextBoxColumn.Name = "MATURITYDATEDataGridViewTextBoxColumn"
        Me.MATURITYDATEDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CPNDataGridViewTextBoxColumn
        '
        Me.CPNDataGridViewTextBoxColumn.DataPropertyName = "CPN"
        Me.CPNDataGridViewTextBoxColumn.HeaderText = "Coupon"
        Me.CPNDataGridViewTextBoxColumn.Name = "CPNDataGridViewTextBoxColumn"
        Me.CPNDataGridViewTextBoxColumn.ReadOnly = True
        '
        'TABLEINSTRUMENTBindingSource
        '
        Me.TABLEINSTRUMENTBindingSource.DataMember = "TABLE_INSTRUMENT"
        Me.TABLEINSTRUMENTBindingSource.DataSource = Me.InstrumentDataSet
        '
        'InstrumentDataSet
        '
        Me.InstrumentDataSet.DataSetName = "instrumentDataSet"
        Me.InstrumentDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TABLE_INSTRUMENTTableAdapter
        '
        Me.TABLE_INSTRUMENTTableAdapter.ClearBeforeFill = True
        '
        'textBox
        '
        Me.textBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBox.BackColor = System.Drawing.Color.WhiteSmoke
        Me.textBox.ForeColor = System.Drawing.SystemColors.WindowText
        Me.textBox.Location = New System.Drawing.Point(12, 269)
        Me.textBox.Multiline = True
        Me.textBox.Name = "textBox"
        Me.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.textBox.Size = New System.Drawing.Size(519, 100)
        Me.textBox.TabIndex = 9
        Me.textBox.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.staticDataButton)
        Me.GroupBox2.Location = New System.Drawing.Point(145, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(133, 187)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "static data request"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Chain Source"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Bloomberg", "Reuters"})
        Me.ComboBox1.Location = New System.Drawing.Point(6, 32)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 3
        '
        'mainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(543, 381)
        Me.Controls.Add(Me.textBox)
        Me.Controls.Add(Me.menuTab)
        Me.Controls.Add(Me.ToolStrip1)
        Me.MinimumSize = New System.Drawing.Size(550, 330)
        Me.Name = "mainForm"
        Me.Text = "test VB"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.menuTab.ResumeLayout(False)
        Me.downloadPage.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.viewPage.ResumeLayout(False)
        CType(Me.instrumentDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TABLEINSTRUMENTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InstrumentDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents startDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents startDateLabel As System.Windows.Forms.Label
    Friend WithEvents endDateLabel As System.Windows.Forms.Label
    Friend WithEvents endDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents menuTab As System.Windows.Forms.TabControl
    Friend WithEvents downloadPage As System.Windows.Forms.TabPage
    Friend WithEvents viewPage As System.Windows.Forms.TabPage
    Friend WithEvents staticDataButton As System.Windows.Forms.Button
    Friend WithEvents historicalDataButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents instrumentDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents InstrumentDataSet As testVBClassLibrary.instrumentDataSet
    Friend WithEvents TABLEINSTRUMENTBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TABLE_INSTRUMENTTableAdapter As testVBClassLibrary.instrumentDataSetTableAdapters.TABLE_INSTRUMENTTableAdapter
    Friend WithEvents ISINIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ISSUEDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EFFECTIVEDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FIRSTCPNDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LASTCPNDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MATURITYDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CPNDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents textBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
End Class
