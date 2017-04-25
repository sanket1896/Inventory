Imports System.Data.SqlClient
Public Class SEARCH
    Dim cmd As New SqlCommand
    Public Shared SNAME As String
    Public Shared CNAME As String
    Public Shared pid As Integer
    Public Shared cid As Integer
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub SEARCH_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        purchasepanel.Hide()
        salespanel.Hide()
        Dim dr As SqlDataReader
        cmd.CommandText = "SELECT DISTINCT S.SUPPLIER_NAME FROM SUPPLIER AS S INNER JOIN PURCHASE_ORDER_MASTER AS P ON S.SUPPLIER_ID = P.SUPPLIER_ID"
        cmd.Connection = Login.cn
        dr = cmd.ExecuteReader()
        Do While dr.Read
            ComboBox1.Items.Add(dr.GetValue(0).ToString)
        Loop
        dr.Close()

        Dim dr1 As SqlDataReader
        cmd.CommandText = "SELECT DISTINCT S.CUSTOMER_NAME FROM CUSTOMER AS S INNER JOIN SALES_ORDER_MASTER AS P ON S.CUSTOMER_ID = P.CUSTOMER_ID"
        dr1 = cmd.ExecuteReader
        Do While dr1.Read
            ComboBox2.Items.Add(dr1.GetValue(0).ToString)
        Loop
        dr1.Close()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        purchasepanel.Show()
    End Sub

    Private Sub RadioButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.Click
        salespanel.Hide()
        purchasepanel.Show()
    End Sub

    Private Sub RadioButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton2.Click
        purchasepanel.Hide()
        salespanel.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        DataGridView1.DataBindings.Clear()
        Dim ds5 As New System.Data.DataSet
        Dim da6 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID, SUPPLIER.SUPPLIER_NAME, PURCHASE_ORDER_MASTER.PURCHASE_ORDER_DATE, PRODUCT.PRODUCT_NAME, PURCHASE_ORDER_DETAILS.QTY FROM PURCHASE_ORDER_DETAILS INNER JOIN PURCHASE_ORDER_MASTER ON PURCHASE_ORDER_DETAILS.PURCHASE_ORDER_ID = PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID INNER JOIN PRODUCT ON PURCHASE_ORDER_DETAILS.PRODUCT_ID = PRODUCT.PRODUCT_ID INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID WHERE SUPPLIER.SUPPLIER_NAME LIKE'" & ComboBox1.SelectedItem & "%'", Login.cn)
        ds5.Clear()
        da6.Fill(ds5, "PURCHASE_ORDER")
        DataGridView1.DataSource = ds5.Tables(0)
        DataGridView1.Refresh()
    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        DataGridView2.DataBindings.Clear()
        Dim ds As New System.Data.DataSet
        Dim da As New SqlClient.SqlDataAdapter("SELECT SALES_ORDER_MASTER.ORDER_ID, CUSTOMER.CUSTOMER_NAME, UOM.UOM_NAME, SALES_ORDER_DETAIL.QTY, SALES_ORDER_MASTER.ORDER_DATE, SALES_ORDER_DETAIL.DUE_DATE FROM SALES_ORDER_DETAIL INNER JOIN SALES_ORDER_MASTER ON SALES_ORDER_DETAIL.ORDER_ID = SALES_ORDER_MASTER.ORDER_ID INNER JOIN UOM ON SALES_ORDER_DETAIL.UOM_ID = UOM.UOM_ID INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID WHERE CUSTOMER.CUSTOMER_NAME LIKE'" & ComboBox2.SelectedItem & "%'", Login.cn)
        ds.Clear()
        da.Fill(ds, "SALES_ORDER")
        DataGridView2.DataSource = ds.Tables(0)
        DataGridView2.Refresh()
        If DataGridView2.RowCount = 1 Then
            MsgBox("No sales order to selected customer")
            DataGridView2.DataBindings.Clear()
            Exit Sub
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'VIEW SALES BUTTON
        CNAME = ComboBox2.SelectedItem
        cid = DataGridView2.CurrentRow.Cells(0).Value
        SALES_SERACH.ShowDialog()
    End Sub


    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick

        SNAME = ComboBox1.SelectedItem
        pid = DataGridView1.CurrentRow.Cells(0).Value
        PURCHASE_SERACH.ShowDialog()

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged

    End Sub
End Class