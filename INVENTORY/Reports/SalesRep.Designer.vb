<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SalesRep
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SalesRep))
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.ReportDataset1 = New INVENTORY.ReportDataset()
        CType(Me.ReportDataset1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = resources.GetString("SqlSelectCommand1.CommandText")
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "CUSTOMER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("INVOICE_ID", "INVOICE_ID"), New System.Data.Common.DataColumnMapping("ORDER_ID", "ORDER_ID"), New System.Data.Common.DataColumnMapping("CUSTOMER_NAME", "CUSTOMER_NAME"), New System.Data.Common.DataColumnMapping("PRODUCT_NAME", "PRODUCT_NAME"), New System.Data.Common.DataColumnMapping("PRICE", "PRICE"), New System.Data.Common.DataColumnMapping("QTY", "QTY"), New System.Data.Common.DataColumnMapping("NET_AMOUNT", "NET_AMOUNT")})})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\INVANTORY.mdf;Integrate" & _
            "d Security=True;User Instance=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(431, 65)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(58, 23)
        Me.Button3.TabIndex = 21
        Me.Button3.Text = "All"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(322, 65)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(103, 23)
        Me.Button2.TabIndex = 20
        Me.Button2.Text = "Date wise"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(335, 39)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(154, 20)
        Me.DateTimePicker2.TabIndex = 19
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(335, 13)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(154, 20)
        Me.DateTimePicker1.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(272, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "To"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(272, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "From"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(94, 66)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(136, 23)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Customer OR Product wise"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(109, 39)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox2.TabIndex = 14
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(109, 12)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 13
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(12, 40)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(93, 17)
        Me.RadioButton2.TabIndex = 12
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Product Name"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(12, 12)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(100, 17)
        Me.RadioButton1.TabIndex = 11
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Customer Name"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(1, 95)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(647, 291)
        Me.CrystalReportViewer1.TabIndex = 22
        '
        'ReportDataset1
        '
        Me.ReportDataset1.DataSetName = "ReportDataset"
        Me.ReportDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SalesRep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 409)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Name = "SalesRep"
        Me.Text = "SalesRep"
        CType(Me.ReportDataset1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents ReportDataset1 As INVENTORY.ReportDataset
End Class
