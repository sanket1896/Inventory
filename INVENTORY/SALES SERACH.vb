Imports System.Data.SqlClient
Public Class SALES_SERACH
    Dim ds As New System.Data.DataSet
    Dim cmd As New SqlCommand
    Dim prid As Integer
    Private Sub SALES_SERACH_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlClient.SqlDataAdapter("SELECT SALES_MASTER.INVOICE_ID, SALES_MASTER.ORDER_ID, SALES_MASTER.SALES_DATE, CUSTOMER.CUSTOMER_NAME FROM SALES_ORDER_MASTER INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_MASTER ON SALES_ORDER_MASTER.ORDER_ID = SALES_MASTER.ORDER_ID WHERE CUSTOMER_NAME LIKE'" & SEARCH.CNAME & "' AND SALES_MASTER.ORDER_ID=" & SEARCH.cid, Login.cn)
        ds1.Clear()
        da1.Fill(ds1, "SALES_MASTER")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'UPDATE BUTTON CLICK
    '    Try
    '        cmd.CommandText = "select INVOICE_ID from SALES_MASTER WHERE INVOICE_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
    '        cmd.Connection = Login.cn
    '        prid = cmd.ExecuteScalar
    '        MsgBox(prid)

    '        Dim ds2 As New DataSet
    '        Dim adap As SqlDataAdapter
    '        cmd.CommandText = "UPDATE SALES_MASTER SET SALES_DATE='" & DataGridView1.CurrentRow.Cells(2).Value & "' WHERE INVOICE_ID=" & prid
    '        cmd.Connection = Login.cn
    '        cmd.ExecuteNonQuery()

    '        adap = New SqlDataAdapter("SELECT SALES_MASTER.INVOICE_ID, SALES_MASTER.ORDER_ID, SALES_MASTER.SALES_DATE, CUSTOMER.CUSTOMER_NAME FROM SALES_ORDER_MASTER INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_MASTER ON SALES_ORDER_MASTER.ORDER_ID = SALES_MASTER.ORDER_ID", Login.cn)
    '        ds2.Clear()
    '        adap.Fill(ds2, "PURCHASE_MASTER")
    '        DataGridView1.DataSource = ds2.Tables(0)
    '        DataGridView1.Refresh()
    '    Catch ex As Exception
    '        MsgBox("Invalid Date")
    '    End Try
    'End Sub

    'Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    DataGridView1.DataBindings.Clear()
    '    Dim ds1 As New System.Data.DataSet
    '    Dim da1 As New SqlClient.SqlDataAdapter("SELECT SALES_MASTER.INVOICE_ID, SALES_MASTER.ORDER_ID, SALES_MASTER.SALES_DATE, CUSTOMER.CUSTOMER_NAME FROM SALES_ORDER_MASTER INNER JOIN CUSTOMER ON SALES_ORDER_MASTER.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN SALES_MASTER ON SALES_ORDER_MASTER.ORDER_ID = SALES_MASTER.ORDER_ID WHERE CUSTOMER.CUSTOMER_NAME LIKE '" & TextBox1.Text & "%'", Login.cn)
    '    ds1.Clear()
    '    da1.Fill(ds1, "SALES_MASTER")
    '    DataGridView1.DataSource = ds1.Tables(0)
    '    DataGridView1.Refresh()

    'End Sub
End Class