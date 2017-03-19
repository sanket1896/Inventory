Public Class ProductRep

    Private Sub ProductRep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        SqlDataAdapter1.Fill(ReportDataset1, "PRODUCT")
        Dim x As New ProductReport
        x.SetDataSource(ReportDataset1.Tables("PRODUCT"))
        CrystalReportViewer1.ReportSource = x
    End Sub
End Class