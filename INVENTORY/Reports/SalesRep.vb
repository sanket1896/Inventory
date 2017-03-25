Imports System.Data.SqlClient
Public Class SalesRep
    Dim cmd As New SqlCommand
    Private Sub SalesRep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        SqlDataAdapter1.Fill(ReportDataset1, "SALES")
        Dim x As New SalesReport
        x.SetDataSource(ReportDataset1.Tables("SALES"))
        CrystalReportViewer1.ReportSource = x

        ComboBox1.Items.Add("All")
        ComboBox2.Items.Add("All")
        ComboBox1.Text = "ALL"
        ComboBox2.Text = "ALL"

        Dim dr As SqlDataReader
        cmd.CommandText = "select DISTINCT(Customer_name) from CUSTOMER"
        cmd.Connection = Login.cn
        dr = cmd.ExecuteReader
        Do While dr.Read
            ComboBox1.Items.Add(dr.GetValue(0).ToString)
        Loop
        dr.Close()

        Dim dr1 As SqlDataReader
        cmd.CommandText = "select Product_name from PRODUCT"
        cmd.Connection = Login.cn
        dr1 = cmd.ExecuteReader
        Do While dr1.Read
            ComboBox2.Items.Add(dr1.GetValue(0).ToString)
        Loop
        dr1.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_DETAIL.INVOICE_ID, SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, PRODUCT.PRODUCT_NAME, SALES_DETAIL.PRICE, SALES_DETAIL.QTY, SALES_MASTER.NET_AMOUNT FROM PRODUCT INNER JOIN SALES_DETAIL ON PRODUCT.PRODUCT_ID = SALES_DETAIL.PRODUCT_ID INNER JOIN SALES_MASTER ON SALES_DETAIL.INVOICE_ID = SALES_MASTER.INVOICE_ID INNER JOIN SALES_ORDER_MASTER ON SALES_MASTER.ORDER_ID = SALES_ORDER_MASTER.ORDER_ID INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_ORDER_DETAIL ON PRODUCT.PRODUCT_ID = SALES_ORDER_DETAIL.PRODUCT_ID AND SALES_ORDER_MASTER.ORDER_ID = SALES_ORDER_DETAIL.ORDER_ID WHERE (CUSTOMER.CUSTOMER_NAME = '" & ComboBox1.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "SALES")
            Dim x As New SalesReport
            x.SetDataSource(ds3.Tables("SALES"))
            CrystalReportViewer1.ReportSource = x
        End If
        If RadioButton2.Checked = True Then
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_DETAIL.INVOICE_ID, SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, PRODUCT.PRODUCT_NAME, SALES_DETAIL.PRICE, SALES_DETAIL.QTY, SALES_MASTER.NET_AMOUNT FROM PRODUCT INNER JOIN SALES_DETAIL ON PRODUCT.PRODUCT_ID = SALES_DETAIL.PRODUCT_ID INNER JOIN SALES_MASTER ON SALES_DETAIL.INVOICE_ID = SALES_MASTER.INVOICE_ID INNER JOIN SALES_ORDER_MASTER ON SALES_MASTER.ORDER_ID = SALES_ORDER_MASTER.ORDER_ID INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_ORDER_DETAIL ON PRODUCT.PRODUCT_ID = SALES_ORDER_DETAIL.PRODUCT_ID AND SALES_ORDER_MASTER.ORDER_ID = SALES_ORDER_DETAIL.ORDER_ID WHERE (PRODUCT.PRODUCT_NAME = '" & ComboBox2.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "SALES")
            Dim x As New SalesReport
            x.SetDataSource(ds3.Tables("SALES"))
            CrystalReportViewer1.ReportSource = x
        End If

        If RadioButton1.Checked = True And RadioButton2.Checked = True Then
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_DETAIL.INVOICE_ID, SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, PRODUCT.PRODUCT_NAME, SALES_DETAIL.PRICE, SALES_DETAIL.QTY, SALES_MASTER.NET_AMOUNT FROM PRODUCT INNER JOIN SALES_DETAIL ON PRODUCT.PRODUCT_ID = SALES_DETAIL.PRODUCT_ID INNER JOIN SALES_MASTER ON SALES_DETAIL.INVOICE_ID = SALES_MASTER.INVOICE_ID INNER JOIN SALES_ORDER_MASTER ON SALES_MASTER.ORDER_ID = SALES_ORDER_MASTER.ORDER_ID INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_ORDER_DETAIL ON PRODUCT.PRODUCT_ID = SALES_ORDER_DETAIL.PRODUCT_ID AND SALES_ORDER_MASTER.ORDER_ID = SALES_ORDER_DETAIL.ORDER_ID WHERE (PRODUCT.PRODUCT_NAME = '" & ComboBox2.Text & "') AND (CUSTOMER.CUSTOMER_NAME = '" & ComboBox1.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "SALES")
            Dim x As New SalesReport
            x.SetDataSource(ds3.Tables("SALES"))
            CrystalReportViewer1.ReportSource = x

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim from1 As String = DateTimePicker1.Text
        Dim to1 As String = DateTimePicker2.Text

        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_DETAIL.INVOICE_ID, SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, PRODUCT.PRODUCT_NAME, SALES_DETAIL.PRICE, SALES_DETAIL.QTY, SALES_MASTER.NET_AMOUNT FROM PRODUCT INNER JOIN SALES_DETAIL ON PRODUCT.PRODUCT_ID = SALES_DETAIL.PRODUCT_ID INNER JOIN SALES_MASTER ON SALES_DETAIL.INVOICE_ID = SALES_MASTER.INVOICE_ID INNER JOIN SALES_ORDER_MASTER ON SALES_MASTER.ORDER_ID = SALES_ORDER_MASTER.ORDER_ID INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_ORDER_DETAIL ON PRODUCT.PRODUCT_ID = SALES_ORDER_DETAIL.PRODUCT_ID AND SALES_ORDER_MASTER.ORDER_ID = SALES_ORDER_DETAIL.ORDER_ID WHERE (SALES_MASTER.SALES_DATE >= CONVERT(DATETIME, '" & from1 & "', 102)) AND (SALES_MASTER.SALES_DATE <= CONVERT(DATETIME, '" & to1 & "', 102))", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "SALES")
        Dim x As New SalesReport
        x.SetDataSource(ds3.Tables("SALES"))
        CrystalReportViewer1.ReportSource = x
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_DETAIL.INVOICE_ID, SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, PRODUCT.PRODUCT_NAME, SALES_DETAIL.PRICE, SALES_DETAIL.QTY, SALES_MASTER.NET_AMOUNT FROM PRODUCT INNER JOIN SALES_DETAIL ON PRODUCT.PRODUCT_ID = SALES_DETAIL.PRODUCT_ID INNER JOIN SALES_MASTER ON SALES_DETAIL.INVOICE_ID = SALES_MASTER.INVOICE_ID INNER JOIN SALES_ORDER_MASTER ON SALES_MASTER.ORDER_ID = SALES_ORDER_MASTER.ORDER_ID INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_ORDER_DETAIL ON PRODUCT.PRODUCT_ID = SALES_ORDER_DETAIL.PRODUCT_ID AND SALES_ORDER_MASTER.ORDER_ID = SALES_ORDER_DETAIL.ORDER_ID ", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "CUSTOMER")
        Dim x As New SalesReport
        x.SetDataSource(ds3.Tables(0))
        CrystalReportViewer1.ReportSource = x
    End Sub
End Class