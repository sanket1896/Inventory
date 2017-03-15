Imports System.Data.SqlClient
Public Class PURCHASE_SERACH
    Dim ds As New System.Data.DataSet
    Dim cmd As New SqlCommand
    Dim prid As Integer
    Private Sub PURCHASE_SERACH_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_MASTER.PURCHASE_ID, PURCHASE_MASTER.PURCHASE_ORDER_ID, PURCHASE_MASTER.BILL_NO, PURCHASE_MASTER.PURCHASE_DATE, SUPPLIER.SUPPLIER_NAME FROM PURCHASE_ORDER_MASTER INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID INNER JOIN PURCHASE_MASTER ON PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID = PURCHASE_MASTER.PURCHASE_ORDER_ID", Login.cn)
        ds1.Clear()
        da1.Fill(ds1, "PURCHASE_MASTER")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        DataGridView1.DataBindings.Clear()
        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlClient.SqlDataAdapter("SELECT PURCHASE_MASTER.PURCHASE_ID, PURCHASE_MASTER.PURCHASE_ORDER_ID, PURCHASE_MASTER.BILL_NO, PURCHASE_MASTER.PURCHASE_DATE, SUPPLIER.SUPPLIER_NAME FROM PURCHASE_ORDER_MASTER INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID INNER JOIN PURCHASE_MASTER ON PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID = PURCHASE_MASTER.PURCHASE_ORDER_ID WHERE SUPPLIER.SUPPLIER_NAME LIKE '" & TextBox1.Text & "%'", Login.cn)
        ds1.Clear()
        da1.Fill(ds1, "PURCHASE_MASTER")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'UPDATE BUTTON CLICK
        cmd.CommandText = "select PURCHASE_ID from PURCHASE_MASTER WHERE PURCHASE_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
        cmd.Connection = Login.cn
        prid = cmd.ExecuteScalar
        MsgBox(prid)
        Dim ds2 As New DataSet
        Dim adap As SqlDataAdapter
        cmd.CommandText = "UPDATE PURCHASE_MASTER SET BILL_NO='" & DataGridView1.CurrentRow.Cells(2).Value & "' WHERE PURCHASE_ID=" & prid
        cmd.Connection = Login.cn
        cmd.ExecuteNonQuery()
        adap = New SqlDataAdapter("SELECT PURCHASE_MASTER.PURCHASE_ID, PURCHASE_MASTER.PURCHASE_ORDER_ID, PURCHASE_MASTER.BILL_NO, PURCHASE_MASTER.PURCHASE_DATE, SUPPLIER.SUPPLIER_NAME FROM PURCHASE_ORDER_MASTER INNER JOIN SUPPLIER ON PURCHASE_ORDER_MASTER.SUPPLIER_ID = SUPPLIER.SUPPLIER_ID INNER JOIN PURCHASE_MASTER ON PURCHASE_ORDER_MASTER.PURCHASE_ORDER_ID = PURCHASE_MASTER.PURCHASE_ORDER_ID", Login.cn)
        ds2.Clear()
        adap.Fill(ds2, "PURCHASE_MASTER")
        DataGridView1.DataSource = ds2.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class