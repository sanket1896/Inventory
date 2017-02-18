Imports System.Data.SqlClient
Public Class PURCHASE_ORDER_SELECTION
    Dim ds As New System.Data.DataSet
    Dim cmd As New SqlCommand
    Dim da As New SqlDataAdapter("select * from PURCHASE_ORDER_MASTER", Login.cn)
    Dim cmdb As New SqlCommandBuilder(da)
    Private Sub PURCHASE_ORDER_SELECTION_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim cmd As New SqlClient.SqlCommand
        cmd.CommandText = "select Supplier_name from SUPPLIER"
        cmd.Connection = Login.cn
        
        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlDataAdapter("SELECT P.PURCHASE_ORDER_ID,P.PURCHASE_ORDER_DATE,S.SUPPLIER_NAME FROM PURCHASE_ORDER_MASTER P,SUPPLIER S WHERE S.SUPPLIER_ID=P.SUPPLIER_ID ", Login.cn)
        da1.Fill(ds1, "PURCHASE")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        DataGridView1.DataBindings.Clear()
        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlClient.SqlDataAdapter("SELECT P.PURCHASE_ORDER_ID,P.PURCHASE_ORDER_DATE,S.SUPPLIER_NAME FROM PURCHASE_ORDER_MASTER P,SUPPLIER S WHERE S.SUPPLIER_NAME LIKE '" & TextBox1.Text & "%' AND S.SUPPLIER_ID=P.SUPPLIER_ID", Login.cn)
        ds1.Clear()
        da1.Fill(ds1, "PURCHASE_ORDER_MASTER")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
        PURCHASE_ORDER.refresh1()
        PURCHASE_ORDER.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i1 As Integer
        Try

            cmd.CommandText = "select Supplier_id from SUPPLIER where Supplier_name='" & DataGridView1.CurrentRow.Cells(2).Value & "'"
            cmd.Connection = Login.cn
            i1 = cmd.ExecuteScalar
            MsgBox(i1)
            Dim ds2 As New DataSet
            Dim adap As SqlDataAdapter
            cmd.CommandText = "UPDATE PURCHASE_ORDER_MASTER SET SUPPLIER_ID='" & i1 & "' WHERE PURCHASE_ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
            ds2.Clear()

            adap = New SqlDataAdapter("SELECT P.PURCHASE_ORDER_ID,P.PURCHASE_ORDER_DATE,S.SUPPLIER_NAME FROM PURCHASE_ORDER_MASTER P,SUPPLIER S WHERE S.SUPPLIER_ID=P.SUPPLIER_ID ", Login.cn)
            adap.Fill(ds2, "PURCHASE")
            DataGridView1.DataSource = ds2.Tables(0)
            DataGridView1.Refresh()
        Catch ex As Exception
            If i1 = 0 Then
                MsgBox("ENTER NAME OF CORRECT SUPPLIER")
            Else
                MsgBox("RECORD CANNOT BE UPDATED... IT HAS SOME REFERENCE RECORDS", MsgBoxStyle.OkOnly, "INVENTORY")
            End If
        End Try
    End Sub
End Class