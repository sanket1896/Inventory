Imports System.Data.SqlClient
Public Class PurchaseRep
    Dim cmd As New SqlCommand
    Private Sub PurchaseRep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        SqlDataAdapter1.Fill(ReportDataset1, "Purchase")
        Dim x As New PurchaseReport
        x.SetDataSource(ReportDataset1.Tables("Purchase"))
        CrystalReportViewer1.ReportSource = x

        Dim dr As SqlDataReader
        cmd.CommandText = "select DISTINCT(Supplier_name) from SUPPLIER"
        cmd.Connection = Login.cn
        dr = cmd.ExecuteReader
        Do While dr.Read
            ComboBox1.Items.Add(dr.GetValue(0).ToString)
        Loop
        dr.Close()

        Dim dr1 As SqlDataReader
        cmd.CommandText = "select PRODUCT_NAME from PRODUCT"
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
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_DETAILS.PURCHASE_ID, SUPPLIER.SUPPLIER_NAME, PRODUCT.PRODUCT_NAME, PURCHASE_DETAILS.QTY, PURCHASE_DETAILS.PRICE, PURCHASE_MASTER.BILL_NO, PURCHASE_MASTER.NET_AMOUNT FROM PURCHASE_MASTER INNER JOIN PURCHASE_DETAILS ON PURCHASE_MASTER.PURCHASE_ID = PURCHASE_DETAILS.PURCHASE_ID INNER JOIN PRODUCT ON PURCHASE_DETAILS.PRODUCT_ID = PRODUCT.PRODUCT_ID INNER JOIN PURCHASE_ORDER_MASTER ON PURCHASE_MASTER.PURCHASE_ORDER_ID = PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID WHERE (SUPPLIER.SUPPLIER_NAME = '" & ComboBox1.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "Purchase")
            Dim x As New PurchaseReport
            x.SetDataSource(ds3.Tables("Purchase"))
            CrystalReportViewer1.ReportSource = x
        End If
        If RadioButton2.Checked = True Then
            Dim ds31 As New DataSet
            Dim da21 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_DETAILS.PURCHASE_ID, SUPPLIER.SUPPLIER_NAME, PRODUCT.PRODUCT_NAME, PURCHASE_DETAILS.QTY, PURCHASE_DETAILS.PRICE, PURCHASE_MASTER.BILL_NO, PURCHASE_MASTER.NET_AMOUNT FROM PURCHASE_MASTER INNER JOIN PURCHASE_DETAILS ON PURCHASE_MASTER.PURCHASE_ID = PURCHASE_DETAILS.PURCHASE_ID INNER JOIN PRODUCT ON PURCHASE_DETAILS.PRODUCT_ID = PRODUCT.PRODUCT_ID INNER JOIN PURCHASE_ORDER_MASTER ON PURCHASE_MASTER.PURCHASE_ORDER_ID = PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID WHERE (PRODUCT.PRODUCT_NAME = '" & ComboBox2.Text & "')", Login.cn)
            ds31.Clear()
            da21.Fill(ds31, "Purchase")
            Dim x1 As New PurchaseReport
            x1.SetDataSource(ds31.Tables("Purchase"))
            CrystalReportViewer1.ReportSource = x1
        End If

        If RadioButton1.Checked = True And RadioButton2.Checked = True Then
            Dim ds32 As New DataSet
            Dim da22 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_DETAILS.PURCHASE_ID, SUPPLIER.SUPPLIER_NAME, PRODUCT.PRODUCT_NAME, PURCHASE_DETAILS.QTY, PURCHASE_DETAILS.PRICE, PURCHASE_MASTER.BILL_NO, PURCHASE_MASTER.NET_AMOUNT FROM PURCHASE_MASTER INNER JOIN PURCHASE_DETAILS ON PURCHASE_MASTER.PURCHASE_ID = PURCHASE_DETAILS.PURCHASE_ID INNER JOIN PRODUCT ON PURCHASE_DETAILS.PRODUCT_ID = PRODUCT.PRODUCT_ID INNER JOIN PURCHASE_ORDER_MASTER ON PURCHASE_MASTER.PURCHASE_ORDER_ID = PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID WHERE (SUPPLIER.SUPPLIER_NAME = '" & ComboBox1.Text & "') AND (PRODUCT.PRODUCT_NAME = '" & ComboBox2.Text & "')", Login.cn)
            ds32.Clear()
            da22.Fill(ds32, "Purchase")
            Dim x2 As New PurchaseReport
            x2.SetDataSource(ds32.Tables("Purchase"))
            CrystalReportViewer1.ReportSource = x2

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim from1 As String = DateTimePicker1.Text
        Dim to1 As String = DateTimePicker2.Text
        ' MsgBox(from1 & to1)

        Dim ds33 As New DataSet
        Dim da23 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_DETAILS.PURCHASE_ID, SUPPLIER.SUPPLIER_NAME, PRODUCT.PRODUCT_NAME, PURCHASE_DETAILS.QTY, PURCHASE_DETAILS.PRICE, PURCHASE_MASTER.BILL_NO, PURCHASE_MASTER.NET_AMOUNT FROM PURCHASE_MASTER INNER JOIN PURCHASE_DETAILS ON PURCHASE_MASTER.PURCHASE_ID = PURCHASE_DETAILS.PURCHASE_ID INNER JOIN PRODUCT ON PURCHASE_DETAILS.PRODUCT_ID = PRODUCT.PRODUCT_ID INNER JOIN PURCHASE_ORDER_MASTER ON PURCHASE_MASTER.PURCHASE_ORDER_ID = PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID WHERE (PURCHASE_MASTER.PURCHASE_DATE >= CONVERT(DATETIME, '" & from1 & "', 102)) AND (PURCHASE_MASTER.PURCHASE_DATE <= CONVERT(DATETIME, '" & to1 & "', 102))", Login.cn)
        ds33.Clear()
        da23.Fill(ds33, "Purchase")
        Dim x3 As New PurchaseReport
        x3.SetDataSource(ds33.Tables("Purchase"))
        CrystalReportViewer1.ReportSource = x3
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ReportDataset1.Clear()
        SqlDataAdapter1.Fill(ReportDataset1, "Purchase")
        Dim x1 As New PurchaseReport
        x1.SetDataSource(ReportDataset1.Tables("Purchase"))
        CrystalReportViewer1.ReportSource = x1
    End Sub
End Class