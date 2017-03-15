Imports System.Data.SqlClient
Public Class PURCHASE_ORDER
    Dim da As New SqlDataAdapter("SELECT * FROM PURCHASE_ORDER_MASTER", Login.cn)
    Dim da1 As New SqlDataAdapter("SELECT * FROM SUPPLIER", Login.cn)
    Dim da2 As New SqlDataAdapter("SELECT * FROM PRODUCT", Login.cn)
    Dim ds, ds1, ds2 As New DataSet
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand
    Dim rpos As Integer = 0
    Dim addmode As Boolean = False
    Private Sub PURCHASE_ORDER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "PURCHASE_ORDER_MASTER")
        da1.Fill(ds1, "SUPPLIER")
        da2.Fill(ds2, "PRODUCT")
        showdata()

        Dim dr1 As SqlDataReader
        cmd.CommandText = "SELECT SUPPLIER_NAME FROM SUPPLIER"
        cmd.Connection = Login.cn
        dr1 = cmd.ExecuteReader
        While dr1.Read
            ComboBox1.Items.Add(dr1.GetValue(0).ToString)
        End While
        dr1.Close()

        Dim da3 As New SqlDataAdapter("SELECT P.PRODUCT_NAME,P2.QTY FROM PRODUCT P,PURCHASE_ORDER_DETAILS P2 WHERE P.PRODUCT_ID=P2.PRODUCT_ID AND P2.PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        Dim ds3 As New DataSet
        da3.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub
    Sub showdata()

        Dim sid As Integer
        sid = ds.Tables(0).Rows(rpos).Item(2).ToString
        
        Dim sname As String
        cmd.CommandText = "SELECT SUPPLIER_NAME FROM SUPPLIER WHERE SUPPLIER_ID=" & sid
        cmd.Connection = Login.cn
        sname = cmd.ExecuteScalar
        ComboBox1.Text = sname

        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        DateTimePicker1.Text = ds.Tables(0).Rows(rpos).Item(1).ToString

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON CLICK
        TextBox1.Visible = False
        Label2.Visible = False
        ComboBox1.Text = ""
        addmode = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON
        If addmode = True Then

            TextBox1.Text = Val(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(0)) + 1

            cmd.CommandText = "select Supplier_id from SUPPLIER where Supplier_name='" & ComboBox1.Text & "'"
            cmd.Connection = Login.cn
            Dim g As Integer
            g = cmd.ExecuteScalar

            Try
                Dim dr As DataRow = ds.Tables(0).Rows.Add
                dr.Item(0) = TextBox1.Text
                dr.Item(1) = DateTimePicker1.Text
                dr.Item(2) = g

                da.Update(ds, "PURCHASE_ORDER_MASTER")
                MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
                Label2.Visible = True
                TextBox1.Visible = True
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("First Click on Insert Button")
            Button1.Focus()
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()

        Dim da3 As New SqlDataAdapter("SELECT P.PRODUCT_NAME,P2.QTY FROM PRODUCT P,PURCHASE_ORDER_DETAILS P2 WHERE P.PRODUCT_ID=P2.PRODUCT_ID AND P2.PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        Dim ds3 As New DataSet
        da3.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREVIOUS BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()

            Dim da3 As New SqlDataAdapter("SELECT P.PRODUCT_NAME,P2.QTY FROM PRODUCT P,PURCHASE_ORDER_DETAILS P2 WHERE P.PRODUCT_ID=P2.PRODUCT_ID AND P2.PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            Dim ds3 As New DataSet
            da3.Fill(ds3, "PRODUCT")
            DataGridView1.DataSource = ds3.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'NEXT BUTTON CLICK
        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()

            Dim da3 As New SqlDataAdapter("SELECT P.PRODUCT_NAME,P2.QTY FROM PRODUCT P,PURCHASE_ORDER_DETAILS P2 WHERE P.PRODUCT_ID=P2.PRODUCT_ID AND P2.PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            Dim ds3 As New DataSet
            da3.Fill(ds3, "PRODUCT")
            DataGridView1.DataSource = ds3.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()

        Dim da3 As New SqlDataAdapter("SELECT P.PRODUCT_NAME,P2.QTY FROM PRODUCT P,PURCHASE_ORDER_DETAILS P2 WHERE P.PRODUCT_ID=P2.PRODUCT_ID AND P2.PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        Dim ds3 As New DataSet
        da3.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        PURCHASE_ORDER_SELECTION.ShowDialog()
        DataGridView1.Refresh()
    End Sub

    Public Shared Sub refresh1()
        Dim dn As New PURCHASE_ORDER
        dn.Show()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim f As New PURCHASE_ORDER_DETAIL
        f.ShowDialog()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Me.Close()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If IsNumeric(DataGridView1.CurrentRow.Cells(0).Value) Then
            MsgBox("Enetr only character", MsgBoxStyle.OkOnly, "INVENTORY")
        End If
        Dim proid As Integer
        Try
            cmd.CommandText = "SELECT PRODUCT_ID FROM PRODUCT WHERE PRODUCT_NAME='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
            proid = cmd.ExecuteScalar
            cmd.CommandText = "update PURCHASE_ORDER_DETAILS set PRODUCT_ID='" & proid & "', QTY='" & DataGridView1.CurrentRow.Cells(1).Value & "'where PURCHASE_ORDER_ID='" & TextBox1.Text & "'"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
            ds2.Clear()
            Dim da3 As New SqlDataAdapter("SELECT P.PRODUCT_NAME,P2.QTY FROM PRODUCT P,PURCHASE_ORDER_DETAILS P2 WHERE P.PRODUCT_ID=P2.PRODUCT_ID AND P2.PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            Dim ds3 As New DataSet
            da3.Fill(ds3, "PRODUCT")
            DataGridView1.DataSource = ds3.Tables(0)
            DataGridView1.Refresh()
        Catch ex As Exception
            If proid <= 0 Then
                MsgBox("Enter the correct Product")
            Else
                MsgBox(ex.Message)
            End If
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            Dim b As MsgBoxResult
            b = MsgBox("Are you sure want to Delete Record", MsgBoxStyle.YesNo, "INVENTORY")
            If b = MsgBoxResult.Yes Then
                Dim ds2 As New DataSet
                Dim adap As SqlDataAdapter
                cmd.CommandText = "DELETE FROM PURCHASE_ORDER_DETAILS WHERE PURCHASE_ORDER_ID='" & TextBox1.Text & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record Sucessfully Deleted", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                Dim da3 As New SqlDataAdapter("SELECT P.PRODUCT_NAME,P2.QTY FROM PRODUCT P,PURCHASE_ORDER_DETAILS P2 WHERE P.PRODUCT_ID=P2.PRODUCT_ID AND P2.PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
                Dim ds3 As New DataSet
                da3.Fill(ds3, "PRODUCT")
                DataGridView1.DataSource = ds3.Tables(0)
                DataGridView1.Refresh()
                ds.Clear()
                da.Fill(ds, "PURCHASE")
            End If
        Catch ex As Exception
            MsgBox("Record Can Not be Deleted.. It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try
    End Sub
End Class