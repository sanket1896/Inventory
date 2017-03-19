Imports System.Data.SqlClient

Public Class PurchaseReturnRep
    Dim cmd As New SqlCommand
    Private Sub PurchaseReturnRep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        SqlDataAdapter1.Fill(ReportDataset1, "PURCHASERETURN")
        Dim x As New PurchaseReturnReport
        x.SetDataSource(ReportDataset1.Tables("PURCHASERETURN"))
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
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_RETURN_DETAIL.PURCHASE_RETURN_ID, SUPPLIER.SUPPLIER_NAME, PRODUCT.PRODUCT_NAME, PURCHASE_RETURN_MASTER.PURCHASE_RETURN_DATE, PURCHASE_RETURN_DETAIL.QTY, PRODUCT.PRICE, PURCHASE_RETURN_DETAIL.QTY * PRODUCT.PRICE AS [Net amount] FROM PURCHASE_RETURN_MASTER INNER JOIN PURCHASE_MASTER ON PURCHASE_RETURN_MASTER.PURCHASE_ID = PURCHASE_MASTER.PURCHASE_ID INNER JOIN PURCHASE_RETURN_DETAIL ON PURCHASE_RETURN_MASTER.PURCHASE_RETURN_ID = PURCHASE_RETURN_DETAIL.PURCHASE_RETURN_ID INNER JOIN PRODUCT ON PURCHASE_RETURN_DETAIL.PRODUCT_ID = PRODUCT.PRODUCT_ID INNER JOIN PURCHASE_ORDER_MASTER ON PURCHASE_MASTER.PURCHASE_ORDER_ID = PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID WHERE (SUPPLIER.SUPPLIER_NAME ='" & ComboBox1.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "PURCHASERETURN")
            Dim x As New PurchaseReturnReport
            x.SetDataSource(ds3.Tables("PURCHASERETURN"))
            CrystalReportViewer1.ReportSource = x
        End If
        If RadioButton2.Checked = True Then
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_RETURN_DETAIL.PURCHASE_RETURN_ID, SUPPLIER.SUPPLIER_NAME, PRODUCT.PRODUCT_NAME, PURCHASE_RETURN_MASTER.PURCHASE_RETURN_DATE, PURCHASE_RETURN_DETAIL.QTY, PRODUCT.PRICE, PURCHASE_RETURN_DETAIL.QTY * PRODUCT.PRICE AS [Net amount] FROM PURCHASE_RETURN_MASTER INNER JOIN PURCHASE_MASTER ON PURCHASE_RETURN_MASTER.PURCHASE_ID = PURCHASE_MASTER.PURCHASE_ID INNER JOIN PURCHASE_RETURN_DETAIL ON PURCHASE_RETURN_MASTER.PURCHASE_RETURN_ID = PURCHASE_RETURN_DETAIL.PURCHASE_RETURN_ID INNER JOIN PRODUCT ON PURCHASE_RETURN_DETAIL.PRODUCT_ID = PRODUCT.PRODUCT_ID INNER JOIN PURCHASE_ORDER_MASTER ON PURCHASE_MASTER.PURCHASE_ORDER_ID = PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID WHERE (PRODUCT.PRODUCT_NAME ='" & ComboBox2.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "PURCHASERETURN")
            Dim x As New PurchaseReturnReport
            x.SetDataSource(ds3.Tables("PURCHASERETURN"))
            CrystalReportViewer1.ReportSource = x
        End If

        If RadioButton1.Checked = True And RadioButton2.Checked = True Then
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_RETURN_DETAIL.PURCHASE_RETURN_ID, SUPPLIER.SUPPLIER_NAME, PRODUCT.PRODUCT_NAME, PURCHASE_RETURN_MASTER.PURCHASE_RETURN_DATE, PURCHASE_RETURN_DETAIL.QTY, PRODUCT.PRICE, PURCHASE_RETURN_DETAIL.QTY * PRODUCT.PRICE AS [Net amount] FROM PURCHASE_RETURN_MASTER INNER JOIN PURCHASE_MASTER ON PURCHASE_RETURN_MASTER.PURCHASE_ID = PURCHASE_MASTER.PURCHASE_ID INNER JOIN PURCHASE_RETURN_DETAIL ON PURCHASE_RETURN_MASTER.PURCHASE_RETURN_ID = PURCHASE_RETURN_DETAIL.PURCHASE_RETURN_ID INNER JOIN PRODUCT ON PURCHASE_RETURN_DETAIL.PRODUCT_ID = PRODUCT.PRODUCT_ID INNER JOIN PURCHASE_ORDER_MASTER ON PURCHASE_MASTER.PURCHASE_ORDER_ID = PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID WHERE (PRODUCT.PRODUCT_NAME = '" & ComboBox2.Text & "') AND (SUPPLIER.SUPPLIER_NAME = '" & ComboBox1.Text & "')", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "PURCHASERETURN")
            Dim x As New PurchaseReturnReport
            x.SetDataSource(ds3.Tables("PURCHASERETURN"))
            CrystalReportViewer1.ReportSource = x
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim from1 As String = DateTimePicker1.Text
        Dim to1 As String = DateTimePicker2.Text

        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_RETURN_DETAIL.PURCHASE_RETURN_ID, SUPPLIER.SUPPLIER_NAME, PRODUCT.PRODUCT_NAME, PURCHASE_RETURN_MASTER.PURCHASE_RETURN_DATE, PURCHASE_RETURN_DETAIL.QTY, PRODUCT.PRICE, PURCHASE_RETURN_DETAIL.QTY * PRODUCT.PRICE AS [Net amount] FROM PURCHASE_RETURN_MASTER INNER JOIN PURCHASE_MASTER ON PURCHASE_RETURN_MASTER.PURCHASE_ID = PURCHASE_MASTER.PURCHASE_ID INNER JOIN PURCHASE_RETURN_DETAIL ON PURCHASE_RETURN_MASTER.PURCHASE_RETURN_ID = PURCHASE_RETURN_DETAIL.PURCHASE_RETURN_ID INNER JOIN PRODUCT ON PURCHASE_RETURN_DETAIL.PRODUCT_ID = PRODUCT.PRODUCT_ID INNER JOIN PURCHASE_ORDER_MASTER ON PURCHASE_MASTER.PURCHASE_ORDER_ID = PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID WHERE (PURCHASE_RETURN_MASTER.PURCHASE_RETURN_DATE >= '" & from1 & "') AND (PURCHASE_RETURN_MASTER.PURCHASE_RETURN_DATE <= '" & to1 & "')", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "PURCHASERETURN")
        Dim x As New PurchaseReturnReport
        x.SetDataSource(ds3.Tables("PURCHASERETURN"))
        CrystalReportViewer1.ReportSource = x
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ReportDataset1.Clear()
        SqlDataAdapter1.Fill(ReportDataset1, "PURCHASERETURN")
        Dim x1 As New PurchaseReturnReport
        x1.SetDataSource(ReportDataset1.Tables("PURCHASERETURN"))
        CrystalReportViewer1.ReportSource = x1
    End Sub
End Class