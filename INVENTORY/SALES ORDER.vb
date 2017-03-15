Imports System.Data.SqlClient
Public Class SALES_ORDER
    Dim da As New SqlDataAdapter("select * from SALES_ORDER_MASTER", Login.cn)
    Dim da1 As New SqlDataAdapter("select * from PRODUCT", Login.cn)
    Dim da3 As New SqlDataAdapter("select * from CUSTOMER", Login.cn)
    Dim da4 As New SqlDataAdapter("select * from UOM", Login.cn)
    Dim ds, ds1, ds3 As New DataSet
    Dim rpos As Integer
    Dim addmodd As Boolean = False
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand

    Private Sub SALES_ORDER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "SALES_ORDER_MASTER")
        da1.Fill(ds1, "PRODUCT")
        da3.Fill(ds3, "CUSTOMER")
        Dim dr2 As SqlDataReader
        cmd.CommandText = "select CUSTOMER_NAME from CUSTOMER"
        cmd.Connection = Login.cn
        dr2 = cmd.ExecuteReader
        Do While dr2.Read
            ComboBox1.Items.Add(dr2.GetValue(0).ToString)
        Loop
        dr2.Close()
        ComboBox1.Text = ds3.Tables(0).Rows(rpos).Item(1).ToString

        Dim dr3 As SqlDataReader
        cmd.CommandText = "select Product_name from PRODUCT"
        cmd.Connection = Login.cn
        dr3 = cmd.ExecuteReader
        Do While dr3.Read
            ComboBox2.Items.Add(dr3.GetValue(0).ToString)
        Loop
        dr3.Close()
        TextBox1.Text = ds.Tables(0).Rows(0).Item(0).ToString

        Dim dr5 As SqlDataReader
        cmd.CommandText = "select CUSTOMER_NAME from CUSTOMER"
        cmd.Connection = Login.cn
        dr5 = cmd.ExecuteReader
        Do While dr5.Read
            ComboBox3.Items.Add(dr5.GetValue(0).ToString)
        Loop
        dr5.Close()

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],p.QTY as [Quantity] from SALES_ORDER_DETAIL p,PRODUCT p1 where p.PRODUCT_ID=p1.PRODUCT_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "SALES_ORDER_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
    End Sub
End Class