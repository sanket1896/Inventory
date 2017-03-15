Imports System.Data.SqlClient
Public Class PURCHASE_ORDER_DETAIL
    Dim da As New SqlDataAdapter("SELECT * FROM PURCHASE_ORDER_DETAILS", Login.cn)
    Dim ds As New DataSet
    Dim cmd As New SqlCommand
    Dim cmdbldr As New SqlCommandBuilder(da)
    Private Sub PURCHASE_ORDER_DETAIL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        showdata()
    End Sub
    Sub showdata()
        da.Fill(ds, "PURCHASE_ORDER_DETAILS")
        If PURCHASE_ORDER.DataGridView1.RowCount <= 1 Then
            cmd.CommandText = "SELECT DISTINCT P.PRODUCT_NAME FROM PRODUCT P,PURCHASE_ORDER_DETAILS P1 WHERE P.PRODUCT_ID=P1.PRODUCT_ID"
            cmd.Connection = Login.cn
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            While dr.Read
                ComboBox2.Items.Add(dr.GetValue(0).ToString)
            End While
            dr.Close()
        Else
            ComboBox2.Items.Clear()
            cmd.CommandText = "SELECT P.PRODUCT_NAME FROM PRODUCT P,PURCHASE_ORDER_DETAILS P1 WHERE P.PRODUCT_NAME NOT LIKE '" & PURCHASE_ORDER.DataGridView1.CurrentRow.Cells(0).Value & "' AND P.PRODUCT_ID=P1.PRODUCT_ID"
            cmd.Connection = Login.cn
            Dim dr1 As SqlDataReader
            dr1 = cmd.ExecuteReader
            MsgBox(PURCHASE_ORDER.DataGridView1.CurrentRow.Cells(0).Value)
            While dr1.Read
                ComboBox2.Items.Add(dr1.GetValue(0).ToString)
            End While
            dr1.Close()
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim purid As Integer
        Dim proid As Integer
        purid = PURCHASE_ORDER.TextBox1.Text  'purchase_order form


        Try
            cmd.CommandText = "SELECT PRODUCT_ID FROM PRODUCT WHERE PRODUCT_NAME='" & ComboBox2.SelectedItem.ToString & "'"
            cmd.Connection = Login.cn
            proid = cmd.ExecuteScalar
            Dim datarow As DataRow = ds.Tables(0).Rows.Add
            datarow.Item(0) = purid
            datarow.Item(1) = proid
            datarow.Item(2) = TextBox6.Text
            da.Update(ds, "PURCHASE_ORDER_DETAILS")
            MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
        Catch ex As Exception
            If proid <= 0 Then
                MsgBox("Please Select the product from the given list only")
            Else
                MsgBox(ex.Message)
            End If
        End Try
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        PURCHASE_ORDER.Close()
        Dim f As New PURCHASE_ORDER
        f.Show()
        Me.Close()
    End Sub
End Class