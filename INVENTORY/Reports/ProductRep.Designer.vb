<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProductRep
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProductRep))
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.ReportDataset1 = New INVENTORY.ReportDataset()
        CType(Me.ReportDataset1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(284, 261)
        Me.CrystalReportViewer1.TabIndex = 0
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = resources.GetString("SqlSelectCommand1.CommandText")
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "PRODUCT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PRODUCT_ID", "PRODUCT_ID"), New System.Data.Common.DataColumnMapping("PRODUCT_NAME", "PRODUCT_NAME"), New System.Data.Common.DataColumnMapping("UOM_NAME", "UOM_NAME"), New System.Data.Common.DataColumnMapping("QOH", "QOH"), New System.Data.Common.DataColumnMapping("PRICE", "PRICE"), New System.Data.Common.DataColumnMapping("VAT", "VAT"), New System.Data.Common.DataColumnMapping("ADITIONAL_VAT", "ADITIONAL_VAT")})})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\INVANTORY.mdf;Integrate" & _
            "d Security=True;User Instance=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'ReportDataset1
        '
        Me.ReportDataset1.DataSetName = "ReportDataset"
        Me.ReportDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ProductRep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Name = "ProductRep"
        Me.Text = "ProductRep"
        CType(Me.ReportDataset1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents ReportDataset1 As INVENTORY.ReportDataset
End Class
