Imports System.Data.SqlClient
Imports System.Globalization
Public Class SALES_ORDER_SEARCH
    Dim ds As New System.Data.DataSet
    Dim cmd As New SqlCommand
    Private Sub SALES_ORDER_SEARCH_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cmd As New SqlClient.SqlCommand
        cmd.CommandText = "select CUSTOMER_NAME from CUSTOMER"
        cmd.Connection = Login.cn

        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlClient.SqlDataAdapter("select p.ORDER_ID as [Purchase Ordre id],p1.CUSTOMER_NAME as [CUSTOMER name],p.ORDER_DATE as [Order date] from SALES_ORDER_MASTER p,CUSTOMER p1 where  p.CUSTOMER_ID=p1.CUSTOMER_ID", Login.cn)
        ds1.Clear()
        da1.Fill(ds1, "PURCHASE_ORDER_MASTER")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        DataGridView1.DataBindings.Clear()
        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlClient.SqlDataAdapter("select p.ORDER_ID as [Order id],p1.CUSTOMER_NAME as [Customer name] ,p.ORDER_DATE as [Order date] from SALES_ORDER_MASTER p,CUSTOMER p1 where  CUSTOMER_NAME like '" & TextBox1.Text & "%' and p.CUSTOMER_ID=p1.CUSTOMER_ID", Login.cn)
        ds1.Clear()
        da1.Fill(ds1, "SALES_ORDER_MASTER")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'UPDATE BUTTON CLICK
        Dim CUS_ID As Integer
        Dim dte As String
        dte = DataGridView1.CurrentRow.Cells(2).Value
        Dim odte As String
        cmd.CommandText = "SELECT ORDER_DATE FROM SALES_ORDER_MASTER WHERE ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
        cmd.Connection = Login.cn
        odte = cmd.ExecuteScalar
        'MsgBox(odte)
        'MsgBox(dte)
        If odte = dte Then
            Try
                cmd.CommandText = "select CUSTOMER_ID from CUSTOMER where CUSTOMER_NAME='" & DataGridView1.CurrentRow.Cells(1).Value & "'"
                cmd.Connection = Login.cn
                CUS_ID = cmd.ExecuteScalar
                'MsgBox(CUS_ID)
                Dim ds2 As New DataSet
                Dim adap As SqlDataAdapter
                cmd.CommandText = "UPDATE SALES_ORDER_MASTER SET CUSTOMER_ID=" & CUS_ID & " WHERE ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("select p.ORDER_ID as [Purchase Ordre id],p1.CUSTOMER_NAME as [CUSTOMER name],p.ORDER_DATE as [Order date] from SALES_ORDER_MASTER p,CUSTOMER p1 where  p.CUSTOMER_ID=p1.CUSTOMER_ID", Login.cn)
                adap.Fill(ds2, "SALES_ORDER_MASTER")
                DataGridView1.DataSource = ds2.Tables(0)
            Catch ex As Exception
                If CUS_ID = 0 Then
                    MsgBox("ENTER APPROPRIATE NAME OF THE CUSTOMER", MsgBoxStyle.OkOnly, "INVENTORY")
                Else
                    MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
                End If

            End Try
        Else
            Try
                cmd.CommandText = "select CUSTOMER_ID from CUSTOMER where CUSTOMER_NAME='" & DataGridView1.CurrentRow.Cells(1).Value & "'"
                cmd.Connection = Login.cn
                CUS_ID = cmd.ExecuteScalar
                'MsgBox(CUS_ID)
                Dim ds2 As New DataSet
                Dim adap As SqlDataAdapter
                cmd.CommandText = "UPDATE SALES_ORDER_MASTER SET CUSTOMER_ID=" & CUS_ID & ",ORDER_DATE='" & DataGridView1.CurrentRow.Cells(2).Value & "' WHERE ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("select p.ORDER_ID as [Purchase Ordre id],p1.CUSTOMER_NAME as [CUSTOMER name],p.ORDER_DATE as [Order date] from SALES_ORDER_MASTER p,CUSTOMER p1 where  p.CUSTOMER_ID=p1.CUSTOMER_ID", Login.cn)
                adap.Fill(ds2, "SALES_ORDER_MASTER")
                DataGridView1.DataSource = ds2.Tables(0)
            Catch ex As Exception
                If CUS_ID = 0 Then
                    MsgBox("ENTER APPROPRIATE NAME OF THE SUPPLIER", MsgBoxStyle.OkOnly, "INVENTORY")
                Else
                    MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
                End If
            End Try
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
        TextBox1.Clear()
    End Sub
End Class