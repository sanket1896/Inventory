Imports System.Data.SqlClient
Public Class InvoiceRep
    Dim cmd As New SqlCommand
    Private Sub InvoiceRep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.WindowState = FormWindowState.Maximized

        Dim dr As SqlDataReader
        cmd.CommandText = "select INVOICE_ID from SALES_MASTER"
        cmd.Connection = Login.cn
        dr = cmd.ExecuteReader
        Do While dr.Read
            ComboBox1.Items.Add(dr.GetValue(0).ToString)
        Loop
        dr.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
       

        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_MASTER.INVOICE_ID, CUSTOMER.CUSTOMER_NAME, CUSTOMER.TIN_NO, SALES_MASTER.SALES_DATE, PRODUCT.PRODUCT_NAME, SALES_DETAIL.QTY, SALES_DETAIL.PRICE, CUSTOMER.ADDRESS, SALES_MASTER.VAT, SALES_MASTER.ADDITIONAL_VAT, SALES_MASTER.NET_AMOUNT FROM CUSTOMER INNER JOIN SALES_ORDER_MASTER ON CUSTOMER.CUSTOMER_ID = SALES_ORDER_MASTER.CUSTOMER_ID INNER JOIN SALES_MASTER ON SALES_ORDER_MASTER.ORDER_ID = SALES_MASTER.ORDER_ID INNER JOIN SALES_DETAIL ON SALES_MASTER.INVOICE_ID = SALES_DETAIL.INVOICE_ID INNER JOIN PRODUCT ON SALES_DETAIL.PRODUCT_ID = PRODUCT.PRODUCT_ID WHERE (SALES_MASTER.INVOICE_ID = " & ComboBox1.Text & ")", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "CUSTOMERINVOICE")


        Dim x As New InvoiceReport
        x.SetDataSource(ds3.Tables("CUSTOMERINVOICE"))
        CrystalReportViewer1.ReportSource = x
    End Sub
End Class