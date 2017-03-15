Imports System.Data.SqlClient
Public Class SALES
    Dim da As New SqlDataAdapter("select * from SALES_MASTER", Login.cn)
    Dim da2 As New SqlDataAdapter("select * from PRODUCT", Login.cn)
    Dim da31 As New SqlDataAdapter("select * from SALES_DETAIL", Login.cn)
    Dim ds, ds12, ds2 As New DataSet
    Dim amt, vat1, addvat1, ans As Integer
    Dim rpos As Integer
    Dim addmodd As Boolean
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand
    Dim net As Double = 0
    Dim qty As Integer = 0
    Private Sub SALES_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "SALES_MASTER")
        da31.Fill(ds12, "SALES_DETAIL")
        da2.Fill(ds2, "PRODUCT")
        showdata()

        Dim dr2 As SqlDataReader
        cmd.CommandText = "select ORDER_ID from SALES_ORDER_MASTER"
        cmd.Connection = Login.cn
        dr2 = cmd.ExecuteReader
        Do While dr2.Read
            ComboBox1.Items.Add(dr2.GetValue(0).ToString)
        Loop
        dr2.Close()

        Dim dr11 As SqlDataReader
        cmd.CommandText = "select PRODUCT_NAME from PRODUCT"
        cmd.Connection = Login.cn
        dr11 = cmd.ExecuteReader
        Do While dr11.Read
            ComboBox2.Items.Add(dr11.GetValue(0).ToString)
        Loop
        dr11.Close()

    End Sub
    Sub showdata()
        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        ComboBox1.Text = ds.Tables(0).Rows(rpos).Item(1).ToString
        DateTimePicker2.Value = ds.Tables(0).Rows(rpos).Item(2).ToString
        TextBox3.Text = ds.Tables(0).Rows(rpos).Item(3).ToString
        TextBox4.Text = ds.Tables(0).Rows(rpos).Item(4).ToString
        TextBox5.Text = ds.Tables(0).Rows(rpos).Item(5).ToString
        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("SELECT SALES_DETAIL.INVOICE_ID, PRODUCT.PRODUCT_NAME, SALES_DETAIL.QTY, SALES_DETAIL.PRICE FROM PRODUCT INNER JOIN SALES_DETAIL ON PRODUCT.PRODUCT_ID = SALES_DETAIL.PRODUCT_ID WHERE SALES_DETAIL.INVOICE_ID=" & Val(TextBox1.Text), Login.cn)
        DA11.Fill(ds4, "SALES_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox1.Text = Val(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(0)) + 1
        addmodd = True
        DateTimePicker2.Text = Date.Today
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON

        If ComboBox1.Items.IndexOf(ComboBox1.Text) = -1 Then

            MsgBox("Select correct Sales Order ID.", MsgBoxStyle.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        End If


        If TextBox3.Text = "" Then
            MsgBox("Enter Correct value.", MsgBoxStyle.Exclamation)
            TextBox3.Focus()
            Exit Sub
        End If
        If TextBox4.Text = "" Then
            MsgBox("Enter Correct value.", MsgBoxStyle.Exclamation)
            TextBox4.Focus()
            Exit Sub
        End If
        If TextBox5.Text = "" Then
            MsgBox("Enter Correct value.", MsgBoxStyle.Exclamation)
            TextBox5.Focus()
            Exit Sub
        End If


        If addmodd = True Then
            Try
                Dim dr As DataRow = ds.Tables(0).Rows.Add
                dr.Item(0) = TextBox1.Text
                dr.Item(1) = ComboBox1.SelectedItem
                dr.Item(2) = DateTimePicker2.Value
                dr.Item(3) = TextBox3.Text
                dr.Item(4) = TextBox4.Text
                dr.Item(5) = TextBox5.Text
                da.Update(ds, "SALES_MASTER")
                addmodd = False
                MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Please First Click on Insert Button", MsgBoxStyle.OkOnly, "INVENTORY")
            Button1.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        SALES_SERACH.ShowDialog()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREV BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'NEXT BUTTON CLICK
        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Me.Close()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        'INSERT BUTTON FROM PURCHASE DETAILS SECTION

        If ComboBox2.Items.IndexOf(ComboBox2.Text) = -1 Then
            MsgBox("Select Product Name.", MsgBoxStyle.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        End If

        If TextBox6.Text = "" Then
            MsgBox("Enter Quantity.", MsgBoxStyle.OkOnly, "INVENTORY")
            Exit Sub
        End If
        If TextBox7.Text = "" Then
            MsgBox("Enter Price.", MsgBoxStyle.OkOnly, "INVENTORY")
            Exit Sub
        End If


        Try
            Dim proid As Integer    'PRODUCT_ID
            cmd.CommandText = "select Product_id from PRODUCT where Product_name='" & ComboBox2.Text & "'"
            cmd.Connection = Login.cn
            proid = cmd.ExecuteScalar

            Dim qoh As Integer = Val(TextBox6.Text)

            cmd.CommandText = "INSERT INTO SALES_DETAIL VALUES (" & TextBox1.Text & "," & proid & "," & qoh & "," & Val(TextBox7.Text) & ")"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()

            'TO SHOW INSERTED RECORD IN DATAGRIDVIEW1
            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("SELECT SALES_DETAIL.INVOICE_ID, PRODUCT.PRODUCT_NAME, SALES_DETAIL.QTY, SALES_DETAIL.PRICE FROM PRODUCT INNER JOIN SALES_DETAIL ON PRODUCT.PRODUCT_ID = SALES_DETAIL.PRODUCT_ID WHERE SALES_DETAIL.INVOICE_ID=" & Val(TextBox1.Text), Login.cn)
            DA11.Fill(ds4, "SALES_DETAIL")
            DataGridView1.DataSource = ds4.Tables(0)

            'UPDATE THE PRODUCT QOH
            Dim qty As Integer
            cmd.CommandText = "SELECT QOH FROM PRODUCT WHERE (PRODUCT_NAME LIKE '" & ComboBox2.Text & "%')"
            cmd.Connection = Login.cn
            qty = cmd.ExecuteScalar()

            If qty > qoh Then
                Dim newqoh As Integer
                newqoh = qty - qoh
                cmd.CommandText = "UPDATE PRODUCT SET QOH= " & newqoh & " WHERE PRODUCT_ID=" & proid
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("QOH UPDATED")
            Else
                MsgBox("You have only " & qty & " products remaining")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Data entered was invalid or insufficient")
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'UPDATE BUTTON CLICK FROM PURCHASE DETAILS SECTION
        Try
            cmd.CommandText = "select QTY from SALES_DETAIL where INVOICE_id='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
            cmd.Connection = Login.cn
            Dim w As Integer = cmd.ExecuteScalar() 'QTY_PURCHASE_DETAILS

            Dim proid As Integer    'PRODUCT_ID
            cmd.CommandText = "select Product_id from PRODUCT where Product_name='" & DataGridView1.CurrentRow.Cells(1).Value & "'"
            cmd.Connection = Login.cn
            proid = cmd.ExecuteScalar

            cmd.CommandText = "UPDATE SALES_DETAIL SET PRODUCT_ID=" & proid & ", QTY=" & DataGridView1.CurrentRow.Cells(2).Value & ", PRICE=" & DataGridView1.CurrentRow.Cells(3).Value & " WHERE INVOICE_ID=" & DataGridView1.CurrentRow.Cells(0).Value & " AND PRODUCT_ID=" & proid
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()

            cmd.CommandText = "select QOH from PRODUCT where PRODUCT_ID=" & proid
            cmd.Connection = Login.cn
            Dim t2 As Integer   'QOH_PRODUCT
            t2 = cmd.ExecuteScalar

            Dim p As Integer
            If (w > DataGridView1.CurrentRow.Cells(2).Value) Then

                p = w - DataGridView1.CurrentRow.Cells(2).Value

                cmd.CommandText = "update PRODUCT set QOH='" & t2 - p & "' where PRODUCT_id='" & proid & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Product Successfully updated", MsgBoxStyle.OkOnly, "INVENTORY")
            ElseIf (w < DataGridView1.CurrentRow.Cells(2).Value) Then

                p = DataGridView1.CurrentRow.Cells(2).Value - w

                Dim finalval As Integer
                finalval = t2 - p
                If finalval > 0 Then
                    cmd.CommandText = "update PRODUCT set QOH='" & t2 + p & "' where PRODUCT_id='" & proid & "'"
                    cmd.Connection = Login.cn
                    cmd.ExecuteNonQuery()
                Else
                    MsgBox("Quantity Entered is greater than the stock")
                End If
                MsgBox("Product Successfully updated", MsgBoxStyle.OkOnly, "INVENTORY")
            End If

            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("SELECT SALES_DETAIL.INVOICE_ID, PRODUCT.PRODUCT_NAME, SALES_DETAIL.QTY, SALES_DETAIL.PRICE FROM PRODUCT INNER JOIN SALES_DETAIL ON PRODUCT.PRODUCT_ID = SALES_DETAIL.PRODUCT_ID WHERE SALES_DETAIL.INVOICE_ID=" & Val(TextBox1.Text), Login.cn)
            DA11.Fill(ds4, "SALES_DETAIL")
            DataGridView1.DataSource = ds4.Tables(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

   
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[0-9]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub
End Class