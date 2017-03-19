Imports System.Data.SqlClient

Public Class SalesReturnRep
    Dim cmd As New SqlCommand
    Private Sub SalesReturnRep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Me.WindowState = FormWindowState.Maximized
        SqlDataAdapter1.Fill(ReportDataset1, "SALESRETURN")
        Dim x As New SalesReturnReport
        x.SetDataSource(ReportDataset1.Tables("SALESRETURN"))
        CrystalReportViewer1.ReportSource = x

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
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, PRODUCT.PRODUCT_NAME, SALES_RETURN_MASTER.SALES_RETURN_DATE, SALES_RETURN_DETAIL.QTY, PRODUCT.PRICE, SALES_RETURN_DETAIL.QTY * PRODUCT.PRICE AS [Net Amount] FROM SALES_ORDER_MASTER INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_MASTER ON SALES_ORDER_MASTER.ORDER_ID = SALES_MASTER.ORDER_ID INNER JOIN SALES_RETURN_DETAIL INNER JOIN SALES_RETURN_MASTER ON SALES_RETURN_DETAIL.SALES_RETURN_ID = SALES_RETURN_MASTER.SALES_RETURN_ID INNER JOIN PRODUCT ON SALES_RETURN_DETAIL.PRODUCT_ID = PRODUCT.PRODUCT_ID ON SALES_MASTER.INVOICE_ID = SALES_RETURN_MASTER.INVOICE_ID WHERE (CUSTOMER.CUSTOMER_NAME='" & ComboBox1.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "SALESRETURN")
            Dim x As New SalesReturnReport
            x.SetDataSource(ds3.Tables("SALESRETURN"))
            CrystalReportViewer1.ReportSource = x
        End If
        If RadioButton2.Checked = True Then
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, PRODUCT.PRODUCT_NAME, SALES_RETURN_MASTER.SALES_RETURN_DATE, SALES_RETURN_DETAIL.QTY, PRODUCT.PRICE, SALES_RETURN_DETAIL.QTY * PRODUCT.PRICE AS [Net Amount] FROM SALES_ORDER_MASTER INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_MASTER ON SALES_ORDER_MASTER.ORDER_ID = SALES_MASTER.ORDER_ID INNER JOIN SALES_RETURN_DETAIL INNER JOIN SALES_RETURN_MASTER ON SALES_RETURN_DETAIL.SALES_RETURN_ID = SALES_RETURN_MASTER.SALES_RETURN_ID INNER JOIN PRODUCT ON SALES_RETURN_DETAIL.PRODUCT_ID = PRODUCT.PRODUCT_ID ON SALES_MASTER.INVOICE_ID = SALES_RETURN_MASTER.INVOICE_ID WHERE (PRODUCT.PRODUCT_NAME='" & ComboBox2.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "SALESRETURN")
            Dim x As New SalesReturnReport
            x.SetDataSource(ds3.Tables("SALESRETURN"))
            CrystalReportViewer1.ReportSource = x
        End If

        If RadioButton1.Checked = True And RadioButton2.Checked = True Then
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, PRODUCT.PRODUCT_NAME, SALES_RETURN_MASTER.SALES_RETURN_DATE, SALES_RETURN_DETAIL.QTY, PRODUCT.PRICE, SALES_RETURN_DETAIL.QTY * PRODUCT.PRICE AS [Net Amount] FROM SALES_ORDER_MASTER INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_MASTER ON SALES_ORDER_MASTER.ORDER_ID = SALES_MASTER.ORDER_ID INNER JOIN SALES_RETURN_DETAIL INNER JOIN SALES_RETURN_MASTER ON SALES_RETURN_DETAIL.SALES_RETURN_ID = SALES_RETURN_MASTER.SALES_RETURN_ID INNER JOIN PRODUCT ON SALES_RETURN_DETAIL.PRODUCT_ID = PRODUCT.PRODUCT_ID ON SALES_MASTER.INVOICE_ID = SALES_RETURN_MASTER.INVOICE_ID WHERE (PRODUCT.PRODUCT_NAME='" & ComboBox1.Text & "') AND (CUSTOMER.CUSTOMER_NAME='" & ComboBox2.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "SALESRETURN")
            Dim x As New SalesReturnReport
            x.SetDataSource(ds3.Tables("SALESRETURN"))
            CrystalReportViewer1.ReportSource = x
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim from1 As String = DateTimePicker1.Text
        Dim to1 As String = DateTimePicker2.Text

        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, PRODUCT.PRODUCT_NAME, SALES_RETURN_MASTER.SALES_RETURN_DATE, SALES_RETURN_DETAIL.QTY, PRODUCT.PRICE, SALES_RETURN_DETAIL.QTY * PRODUCT.PRICE AS [Net Amount] FROM SALES_ORDER_MASTER INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_MASTER ON SALES_ORDER_MASTER.ORDER_ID = SALES_MASTER.ORDER_ID INNER JOIN SALES_RETURN_DETAIL INNER JOIN SALES_RETURN_MASTER ON SALES_RETURN_DETAIL.SALES_RETURN_ID = SALES_RETURN_MASTER.SALES_RETURN_ID INNER JOIN PRODUCT ON SALES_RETURN_DETAIL.PRODUCT_ID = PRODUCT.PRODUCT_ID ON SALES_MASTER.INVOICE_ID = SALES_RETURN_MASTER.INVOICE_ID WHERE (SALES_RETURN_MASTER.SALES_RETURN_DATE >= CONVERT(DATETIME, '" & from1 & "', 102)) AND (SALES_RETURN_MASTER.SALES_RETURN_DATE <= CONVERT(DATETIME, '" & to1 & "', 102))", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "SALESRETURN")
        Dim x As New SalesReturnReport
        x.SetDataSource(ds3.Tables("SALESRETURN"))
        CrystalReportViewer1.ReportSource = x
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ReportDataset1.Clear()
        SqlDataAdapter1.Fill(ReportDataset1, "SALESRETURN")
        Dim x1 As New SalesReturnReport
        x1.SetDataSource(ReportDataset1.Tables("SALESRETURN"))
        CrystalReportViewer1.ReportSource = x1
    End Sub
End Class