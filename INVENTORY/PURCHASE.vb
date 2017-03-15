Imports System.Data.SqlClient
Public Class PURCHASE
    Dim da As New SqlDataAdapter("select * from PURCHASE_MASTER", Login.cn)
    Dim da2 As New SqlDataAdapter("select * from PRODUCT", Login.cn)
    Dim da31 As New SqlDataAdapter("select * from PURCHASE_DETAILS", Login.cn)
    Dim ds, ds12, ds2 As New DataSet
    Dim amt, vat1, addvat1, ans As Integer
    Dim rpos As Integer
    Dim addmodd As Boolean
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand
    Dim net As Double = 0
    Dim qty As Integer = 0

    Private Sub PURCHASE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "PURCHASE_MASTER")
        da31.Fill(ds12, "PURCHASE_DETAILS")
        da2.Fill(ds2, "PRODUCT")
        ComboBox1.Visible = False
        showdata()

        Dim dr2 As SqlDataReader
        cmd.CommandText = "select PURCHASE_ORDER_ID from PURCHASE_ORDER_MASTER"
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
        TextBox2.Text = ds.Tables(0).Rows(rpos).Item(2).ToString
        DateTimePicker2.Value = ds.Tables(0).Rows(rpos).Item(3)
        TextBox8.Text = ds.Tables(0).Rows(rpos).Item(4).ToString
        TextBox3.Text = ds.Tables(0).Rows(rpos).Item(5).ToString
        TextBox4.Text = ds.Tables(0).Rows(rpos).Item(6).ToString
        TextBox5.Text = ds.Tables(0).Rows(rpos).Item(7).ToString


        'TO SHOW THE PURCHASE ORDER ID FROM PRODUCT TABLE IN TEXTBOX 9
        cmd.CommandText = "SELECT PURCHASE_ORDER_ID FROM PURCHASE_MASTER WHERE PURCHASE_ID='" & TextBox1.Text & "'"
        cmd.Connection = Login.cn
        TextBox9.Text = cmd.ExecuteScalar

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("SELECT PURCHASE_DETAILS.PURCHASE_ID, PRODUCT.PRODUCT_NAME, PURCHASE_DETAILS.QTY, PURCHASE_DETAILS.PRICE FROM PRODUCT INNER JOIN PURCHASE_DETAILS ON PRODUCT.PRODUCT_ID = PURCHASE_DETAILS.PRODUCT_ID WHERE PURCHASE_ID=" & Val(TextBox1.Text), Login.cn)
        DA11.Fill(ds4, "PURCHASE_DETAILS")
        DataGridView1.DataSource = ds4.Tables(0)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON
        ComboBox1.Visible = True
        TextBox9.Visible = False
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox8.Clear()
        TextBox1.Text = Val(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(0)) + 1
        addmodd = True
        DateTimePicker2.Text = Date.Today
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON

        If TextBox1.Text = "" Then
            MsgBox("Enter correct value", MsgBoxStyle.Exclamation)
            TextBox1.Focus()
        End If
        If TextBox2.Text = "" Then
            MsgBox("Enter correct value", MsgBoxStyle.Exclamation)
            TextBox2.Focus()
        End If
        If TextBox3.Text = "" Then
            MsgBox("Enter correct value", MsgBoxStyle.Exclamation)
            TextBox3.Focus()
        End If
        If TextBox4.Text = "" Then
            MsgBox("Enter correct value", MsgBoxStyle.Exclamation)
            TextBox4.Focus()
        End If
        If TextBox5.Text = "" Then
            MsgBox("Enter correct value", MsgBoxStyle.Exclamation)
            TextBox5.Focus()
        End If
        If TextBox8.Text = "" Then
            MsgBox("Enter correct value", MsgBoxStyle.Exclamation)
            TextBox8.Focus()
        End If
        If TextBox9.Text = "" Then
            MsgBox("Enter correct value", MsgBoxStyle.Exclamation)
            TextBox9.Focus()
        End If



        If addmodd = True Then
            Try
                Dim dr As DataRow = ds.Tables(0).Rows.Add
                dr.Item(0) = TextBox1.Text
                dr.Item(1) = ComboBox1.SelectedItem
                dr.Item(2) = TextBox2.Text
                dr.Item(3) = DateTimePicker2.Text
                dr.Item(4) = TextBox8.Text
                dr.Item(5) = TextBox3.Text
                dr.Item(6) = TextBox4.Text
                dr.Item(7) = TextBox5.Text
                da.Update(ds, "PURCHASE_MASTER")
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

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()
        ComboBox1.Visible = False
        TextBox9.Visible = True
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'NEXT BUTTON CLICK
        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()

            ComboBox1.Visible = False
            TextBox9.Visible = True
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()

        ComboBox1.Visible = False
        TextBox9.Visible = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREVIOUS BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()

            ComboBox1.Visible = False
            TextBox9.Visible = True
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        PURCHASE_SERACH.ShowDialog()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Me.Close()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        'INSERT BUTTON FROM PURCHASE DETAILS SECTION
        Try
            Dim proid As Integer    'PRODUCT_ID
            cmd.CommandText = "select Product_id from PRODUCT where Product_name='" & ComboBox2.Text & "'"
            cmd.Connection = Login.cn
            proid = cmd.ExecuteScalar

            Dim qoh As Integer = Val(TextBox6.Text)

            cmd.CommandText = "INSERT INTO PURCHASE_DETAILS VALUES (" & TextBox1.Text & "," & proid & "," & qoh & "," & Val(TextBox7.Text) & ")"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()

            'TO SHOW INSERTED RECORD IN DATAGRIDVIEW1
            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("SELECT PURCHASE_DETAILS.PURCHASE_ID, PRODUCT.PRODUCT_NAME, PURCHASE_DETAILS.QTY, PURCHASE_DETAILS.PRICE FROM PRODUCT INNER JOIN PURCHASE_DETAILS ON PRODUCT.PRODUCT_ID = PURCHASE_DETAILS.PRODUCT_ID WHERE PURCHASE_ID=" & Val(TextBox1.Text), Login.cn)
            DA11.Fill(ds4, "PURCHASE_DETAILS")
            DataGridView1.DataSource = ds4.Tables(0)

            'UPDATE THE PRODUCT QOH
            Dim qty As Integer
            cmd.CommandText = "SELECT QOH FROM PRODUCT WHERE (PRODUCT_NAME LIKE '" & ComboBox2.Text & "%')"
            cmd.Connection = Login.cn
            qty = cmd.ExecuteScalar()

            Dim newqoh As Integer
            newqoh = qty + qoh

            cmd.CommandText = "UPDATE PRODUCT SET QOH= " & newqoh & " WHERE PRODUCT_ID=" & proid
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("QOH UPDATED")
        Catch ex As Exception
            MsgBox("Data entered was invalid or insufficient")
        End Try

    End Sub

    'Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
    '    'UPDATE BUTTON CLICK FROM PURCHASE DETAILS SECTION
    '    'Try
    '    cmd.CommandText = "select QTY from PURCHASE_DETAILS where Purchase_id='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
    '    cmd.Connection = Login.cn
    '    Dim w As Integer = cmd.ExecuteScalar() 'QTY_PURCHASE_DETAILS

    '    Dim proid As Integer    'PRODUCT_ID
    '    cmd.CommandText = "select Product_id from PRODUCT where Product_name='" & DataGridView1.CurrentRow.Cells(1).Value & "'"
    '    cmd.Connection = Login.cn
    '    proid = cmd.ExecuteScalar

    '    cmd.CommandText = "UPDATE PURCHASE_DETAILS SET PRODUCT_ID=" & proid & ", QTY=" & DataGridView1.CurrentRow.Cells(2).Value & ", PRICE=" & DataGridView1.CurrentRow.Cells(3).Value & " WHERE PURCHASE_ID=" & DataGridView1.CurrentRow.Cells(0).Value & " AND PRODUCT_ID=" & proid
    '    cmd.Connection = Login.cn
    '    cmd.ExecuteNonQuery()

    '    cmd.CommandText = "select QOH from PRODUCT where PRODUCT_ID=" & proid
    '    cmd.Connection = Login.cn
    '    Dim t2 As Integer   'QOH_PRODUCT
    '    t2 = cmd.ExecuteScalar

    '    Dim p As Integer
    '    If (w < DataGridView1.CurrentRow.Cells(2).Value) Then

    '        p = DataGridView1.CurrentRow.Cells(2).Value - w

    '        'MsgBox(t2 + p)
    '        cmd.CommandText = "update PRODUCT set QOH='" & t2 + p & "' where PRODUCT_id='" & proid & "'"
    '        cmd.Connection = Login.cn
    '        cmd.ExecuteNonQuery()
    '    ElseIf (w > DataGridView1.CurrentRow.Cells(2).Value) Then

    '        p = w - DataGridView1.CurrentRow.Cells(3).Value

    '        'MsgBox(t2 - p)
    '        cmd.CommandText = "update PRODUCT set QOH='" & t2 - p & "' where PRODUCT_id='" & proid & "'"
    '        cmd.Connection = Login.cn
    '        cmd.ExecuteNonQuery()

    '    End If

    '    Dim ds4 As New DataSet
    '    ds4.Clear()
    '    Dim DA11 As New SqlDataAdapter("SELECT PURCHASE_DETAILS.PURCHASE_ID, PRODUCT.PRODUCT_NAME, PURCHASE_DETAILS.QTY, PURCHASE_DETAILS.PRICE FROM PRODUCT INNER JOIN PURCHASE_DETAILS ON PRODUCT.PRODUCT_ID = PURCHASE_DETAILS.PRODUCT_ID WHERE PURCHASE_ID=" & Val(TextBox1.Text), Login.cn)
    '    DA11.Fill(ds4, "PURCHASE_DETAILS")
    '    DataGridView1.DataSource = ds4.Tables(0)

    '    'Catch ex As Exception
    '    'MsgBox(ex.Message)
    '    'End Try
    'End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[0-9]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub
End Class