Public Class SupplierRep

    Private Sub SupplierRep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        SqlDataAdapter1.Fill(ReportDataset1, "Suppler")
        Dim cr1 As New SupplierReport
        cr1.SetDataSource(ReportDataset1.Tables("Suppler"))
        CrystalReportViewer1.ReportSource = cr1
    End Sub
End Class