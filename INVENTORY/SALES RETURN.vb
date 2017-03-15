Imports System.Data.SqlClient
Public Class SALES_RETURN
    Dim da As New SqlDataAdapter("select * from SALES_RETURN_MASTER", Login.cn)
    Dim da1 As New SqlDataAdapter("select * from PRODUCT", Login.cn)
    Dim da2 As New SqlDataAdapter("select * from SALES_MASTER", Login.cn)
    Dim da3 As New SqlDataAdapter("select * from SALES_DETAIL", Login.cn)
    Dim ds, ds2, ds1 As New DataSet
    Dim rpos, amt, vat1, addvat1, ans As Integer
    Dim addmodd As Boolean
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand
    Dim net As Double = 0
    Dim qty As Integer = 0

    Private Sub SALES_RETURN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "SALES_RETURN_MASTER")
        da1.Fill(ds1, "PRODUCT")
        da2.Fill(ds2, "SALES_MASTER")
        da3.Fill(ds2, "SALES_DETAIL")
        showdata()
        Dim dr As SqlDataReader
        cmd.CommandText = "select INVOICE_ID from SALES_MASTER"
        cmd.Connection = Login.cn
        dr = cmd.ExecuteReader
        Do While dr.Read
            ComboBox1.Items.Add(dr.GetValue(0).ToString)
        Loop
        dr.Close()

    End Sub

    Sub showdata()
        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        ComboBox1.Text = ds.Tables(0).Rows(rpos).Item(1).ToString
        DateTimePicker1.Text = ds.Tables(0).Rows(rpos).Item(2).ToString

        ComboBox2.Items.Clear()
        Dim dr1 As SqlDataReader
        cmd.CommandText = "SELECT P.PRODUCT_NAME FROM PRODUCT P, SALES_DETAIL P1 WHERE P.PRODUCT_ID=P1.PRODUCT_ID AND P1.INVOICE_ID= " & ComboBox1.Text
        cmd.Connection = Login.cn
        dr1 = cmd.ExecuteReader
        Do While dr1.Read
            ComboBox2.Items.Add(dr1.GetValue(0).ToString)
        Loop
        dr1.Close()

        DataGridView1.Refresh()
        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, PRODUCT.PRODUCT_NAME, SALES_RETURN_DETAIL.QTY FROM PRODUCT INNER JOIN SALES_RETURN_DETAIL ON PRODUCT.PRODUCT_ID = SALES_RETURN_DETAIL.PRODUCT_ID WHERE SALES_RETURN_DETAIL.SALES_RETURN_ID=" & TextBox1.Text, Login.cn)
        DA11.Fill(ds4, "SALES_RETURN_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON
        TextBox1.Text = Val(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(0)) + 1
        DateTimePicker1.Text = Date.Today
        ComboBox1.Text = ""
        addmodd = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON CLICK
        cmd.CommandText = "select SALES_date from SALES_MASTER where INVOICE_id='" & ComboBox1.SelectedItem.ToString & "'"
        cmd.Connection = Login.cn
        Dim str As Date
        str = cmd.ExecuteScalar()

        If Not DateTimePicker1.Value.Date >= str.Date Then
            MsgBox("Sales return date must be Greater than '" & str & "'", MsgBoxStyle.OkOnly, "INVENTORY")
            Exit Sub
        End If

        Try
            Dim dr As DataRow = ds.Tables(0).Rows.Add
            dr.Item(0) = TextBox1.Text
            dr.Item(1) = ComboBox1.SelectedItem
            dr.Item(2) = DateTimePicker1.Text

            da.Update(ds, "SALES_RETURN_MASTER")
            MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'FIRST BUTTON
        rpos = 0
        showdata()

        DataGridView1.Refresh()
        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, PRODUCT.PRODUCT_NAME, SALES_RETURN_DETAIL.QTY FROM PRODUCT INNER JOIN SALES_RETURN_DETAIL ON PRODUCT.PRODUCT_ID = SALES_RETURN_DETAIL.PRODUCT_ID WHERE SALES_RETURN_DETAIL.SALES_RETURN_ID=" & TextBox1.Text, Login.cn)
        DA11.Fill(ds4, "SALES_RETURN_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREV BUTTON
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()

            DataGridView1.Refresh()
            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, PRODUCT.PRODUCT_NAME, SALES_RETURN_DETAIL.QTY FROM PRODUCT INNER JOIN SALES_RETURN_DETAIL ON PRODUCT.PRODUCT_ID = SALES_RETURN_DETAIL.PRODUCT_ID WHERE SALES_RETURN_DETAIL.SALES_RETURN_ID=" & TextBox1.Text, Login.cn)
            DA11.Fill(ds4, "SALES_RETURN_DETAIL")
            DataGridView1.DataSource = ds4.Tables(0)
            DataGridView1.Refresh()
        End If

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'NEXT BUTTON
        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()

            DataGridView1.Refresh()
            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, PRODUCT.PRODUCT_NAME, SALES_RETURN_DETAIL.QTY FROM PRODUCT INNER JOIN SALES_RETURN_DETAIL ON PRODUCT.PRODUCT_ID = SALES_RETURN_DETAIL.PRODUCT_ID WHERE SALES_RETURN_DETAIL.SALES_RETURN_ID=" & TextBox1.Text, Login.cn)
            DA11.Fill(ds4, "SALES_RETURN_DETAIL")
            DataGridView1.DataSource = ds4.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON
        rpos = (ds.Tables(0).Rows.Count) - 1
        showdata()

        DataGridView1.Refresh()
        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, PRODUCT.PRODUCT_NAME, SALES_RETURN_DETAIL.QTY FROM PRODUCT INNER JOIN SALES_RETURN_DETAIL ON PRODUCT.PRODUCT_ID = SALES_RETURN_DETAIL.PRODUCT_ID WHERE SALES_RETURN_DETAIL.SALES_RETURN_ID=" & TextBox1.Text, Login.cn)
        DA11.Fill(ds4, "SALES_RETURN_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()

    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        'INSERT BUTTON OF PURCHASE RETURN SECTION
        Dim proid As Integer
        cmd.CommandText = "select PRODUCT_id from PRODUCT where PRODUCT_name='" & ComboBox2.SelectedItem & "'"
        cmd.Connection = Login.cn
        proid = cmd.ExecuteScalar

        cmd.CommandText = "INSERT INTO SALES_RETURN_DETAIL VALUES(" & TextBox1.Text & "," & proid & "," & TextBox6.Text & ")"
        cmd.Connection = Login.cn
        cmd.ExecuteNonQuery()
        MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")

        DataGridView1.Refresh()
        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, PRODUCT.PRODUCT_NAME, SALES_RETURN_DETAIL.QTY FROM PRODUCT INNER JOIN SALES_RETURN_DETAIL ON PRODUCT.PRODUCT_ID = SALES_RETURN_DETAIL.PRODUCT_ID WHERE SALES_RETURN_DETAIL.SALES_RETURN_ID=" & TextBox1.Text, Login.cn)
        DA11.Fill(ds4, "SALES_RETURN_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()

        cmd.CommandText = "select PRODUCT_id from PRODUCT where PRODUCT_name='" & ComboBox2.Text & "'"
        cmd.Connection = Login.cn
        Dim t As Integer = cmd.ExecuteScalar

        cmd.CommandText = "select QOH from PRODUCT where PRODUCT_id='" & t & "'"
        cmd.Connection = Login.cn
        Dim t1 As Integer
        t1 = cmd.ExecuteScalar

        Dim upqoh As Integer = t1 + Val(TextBox6.Text)
        cmd.CommandText = "update PRODUCT set QOH=  '" & upqoh & "' where PRODUCT_id='" & t & "'"
        cmd.Connection = Login.cn
        cmd.ExecuteNonQuery()
        TextBox6.Clear()
        ComboBox2.Text = ""
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'UPDATE BUTTON
        Try
            Dim id As Integer
            cmd.CommandText = "select PRODUCT_id from PRODUCT where PRODUCT_name='" & DataGridView1.CurrentRow.Cells(1).Value & "'"
            cmd.Connection = Login.cn
            id = cmd.ExecuteScalar()


            cmd.CommandText = "select QTY from SALES_RETURN_DETAIL where SALES_RETURN_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
            cmd.Connection = Login.cn
            Dim w As Integer = cmd.ExecuteScalar()

            Dim ds2 As New DataSet
            'Dim adap As SqlDataAdapter
            cmd.CommandText = "update SALES_RETURN_DETAIL set QTY='" & DataGridView1.CurrentRow.Cells(2).Value & "' where SALES_RETURN_id='" & DataGridView1.CurrentRow.Cells(0).Value & "' and PRODUCT_id='" & id & "'"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")

            cmd.CommandText = "select QOH from PRODUCT where PRODUCT_id='" & id & "'"
            cmd.Connection = Login.cn
            Dim t2 As Integer
            t2 = cmd.ExecuteScalar

            Dim p As Integer
            If (w > DataGridView1.CurrentRow.Cells(2).Value) Then
                p = DataGridView1.CurrentRow.Cells(2).Value - w

                cmd.CommandText = "update PRODUCT set QOH='" & t2 - p & "' where PRODUCT_id='" & id & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
            ElseIf (w < DataGridView1.CurrentRow.Cells(2).Value) Then
                p = w - DataGridView1.CurrentRow.Cells(2).Value

                cmd.CommandText = "update PRODUCT set QOH='" & t2 + p & "' where PRODUCT_id='" & id & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()

                DataGridView1.Refresh()
                Dim ds4 As New DataSet
                ds4.Clear()
                Dim DA11 As New SqlDataAdapter("SELECT SALES_RETURN_DETAIL.SALES_RETURN_ID, PRODUCT.PRODUCT_NAME, SALES_RETURN_DETAIL.QTY FROM PRODUCT INNER JOIN SALES_RETURN_DETAIL ON PRODUCT.PRODUCT_ID = SALES_RETURN_DETAIL.PRODUCT_ID WHERE SALES_RETURN_DETAIL.SALES_RETURN_ID=" & TextBox1.Text, Login.cn)
                DA11.Fill(ds4, "SALES_RETURN_DETAIL")
                DataGridView1.DataSource = ds4.Tables(0)
                DataGridView1.Refresh()
            Else
                MsgBox("INVALID PRODUCT QUANTITY")
            End If
        Catch ex As Exception
            MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try
    End Sub
End Class