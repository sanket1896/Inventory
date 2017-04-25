Imports System.Data.SqlClient
Public Class PURCHASE_ORDER
    Dim da As New SqlDataAdapter("select * from PURCHASE_ORDER_MASTER", Login.cn)
    Dim da1 As New SqlDataAdapter("select * from PRODUCT", Login.cn)
    Dim da3 As New SqlDataAdapter("select * from SUPPLIER", Login.cn)
    Dim ds, ds1, ds3 As New DataSet
    Dim rpos As Integer
    Dim addmodd As Boolean = False
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand

    Private Sub PURCHASE_ORDER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        da.Fill(ds, "PURCHASE_ORDER_MASTER")
        da1.Fill(ds1, "PRODUCT")
        da3.Fill(ds3, "SUPPLIER")

        Dim dr2 As SqlDataReader
        cmd.CommandText = "select SUPPLIER_NAME from SUPPLIER"
        cmd.Connection = Login.cn
        dr2 = cmd.ExecuteReader
        Do While dr2.Read
            ComboBox1.Items.Add(dr2.GetValue(0).ToString)
        Loop
        dr2.Close()

        ComboBox1.Text = ds3.Tables(0).Rows(rpos).Item(1).ToString


        Dim dr3 As SqlDataReader
        cmd.CommandText = "select Product_name from PRODUCT"
        cmd.Connection = Login.cn
        dr3 = cmd.ExecuteReader
        Do While dr3.Read
            ComboBox2.Items.Add(dr3.GetValue(0).ToString)
        Loop
        dr3.Close()



        TextBox1.Text = ds.Tables(0).Rows(0).Item(0).ToString

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p1.Product_name as [Product name],p.QTY as [Quantity] from PURCHASE_ORDER_DETAILS p,PRODUCT p1 where p.PRODUCT_ID=p1.PRODUCT_ID and PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "PURCHASE_ORDER_DETAILS")
        DataGridView1.DataSource = ds4.Tables(0)

    End Sub

    Sub showdata()
        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        DateTimePicker1.Text = ds.Tables(0).Rows(rpos).Item(1).ToString
        Dim i As String
        cmd.CommandText = "select p.SUPPLIER_NAME from SUPPLIER p, PURCHASE_ORDER_MASTER p1 where PURCHASE_ORDER_ID='" & TextBox1.Text & "' and p.SUPPLIER_ID=p1.SUPPLIER_ID"
        cmd.Connection = Login.cn
        i = cmd.ExecuteScalar
        ComboBox1.Text = i

        Dim dr3 As SqlDataReader
        cmd.CommandText = "SELECT PRODUCT.PRODUCT_NAME, PURCHASE_ORDER_DETAILS.PURCHASE_ORDER_ID FROM PRODUCT INNER JOIN PURCHASE_ORDER_DETAILS ON PRODUCT.PRODUCT_ID = PURCHASE_ORDER_DETAILS.PRODUCT_ID WHERE (PURCHASE_ORDER_DETAILS.PURCHASE_ORDER_ID = " & TextBox1.Text & ")"
        cmd.Connection = Login.cn
        dr3 = cmd.ExecuteReader
        Do While dr3.Read
            ComboBox2.Items.Add(dr3.GetValue(0).ToString)
        Loop
        dr3.Close()




    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = Val(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(0)) + 1
        addmodd = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON
        If DateTimePicker1.Value > Now() Then
            MsgBox("Order date must be lessthan or equal to today's date...")
            Exit Sub
        End If

        If ComboBox1.Items.IndexOf(ComboBox1.Text) = -1 Then

            MsgBox("Select correct Supplier name.", MsgBoxStyle.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        End If

        If TextBox6.Text = "" Then
            MsgBox("Enter Quantity.")
            TextBox6.Focus()

        End If





        If addmodd = True Then
            Dim i As Integer
            cmd.CommandText = "select SUPPLIER_ID from SUPPLIER WHERE SUPPLIER_NAME LIKE'" & ComboBox1.SelectedValue.ToString & "'"
            cmd.Connection = Login.cn
            i = cmd.ExecuteScalar
            Try
                Dim dr As DataRow = ds.Tables(0).Rows.Add
                dr.Item(0) = TextBox1.Text
                dr.Item(1) = DateTimePicker1.Text
                dr.Item(2) = i
                da.Update(ds, "PURCHASE_ORDER_MASTER")
                addmodd = False
                MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
                ComboBox1.Enabled = True
                ComboBox2.Enabled = True
                TextBox6.Enabled = True
                Button1.Enabled = False
                Button2.Enabled = False
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Please First Click on Insert Button", MsgBoxStyle.OkOnly, "INVENTORY")
            Button1.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PURCHASE_ORDER_SELECTION.ShowDialog()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p1.Product_name as [Product name],p.QTY as [Quantity] from PURCHASE_ORDER_DETAILS p,PRODUCT p1 where p.PRODUCT_ID=p1.PRODUCT_ID and PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "PURCHASE_ORDER_DETAILS")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREVIOUS BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()

            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p1.PRODUCT_NAME as [Product name],p.QTY as [Quantity] from PURCHASE_ORDER_DETAILS p,PRODUCT p1 where p.PRODUCT_ID=p1.PRODUCT_ID and PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            DA11.Fill(ds4, "PURCHASE_ORDER_DETAILS")
            DataGridView1.DataSource = ds4.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'NEXT BUTTON CLICK
        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()

            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p1.PRODUCT_NAME as [Product name],p.QTY as [Quantity] from PURCHASE_ORDER_DETAILS p,PRODUCT p1 where p.PRODUCT_ID=p1.PRODUCT_ID and PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)

            DA11.Fill(ds4, "PURCHASE_ORDER_DETAILS")
            DataGridView1.DataSource = ds4.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p1.PRODUCT_NAME as [Product name],p.QTY as [Quantity] from PURCHASE_ORDER_DETAILS p,PRODUCT p1 where p.PRODUCT_ID=p1.PRODUCT_ID and PURCHASE_ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)

        DA11.Fill(ds4, "PURCHASE_ORDER_DETAILS")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        'Insert PURCHASE_DETAILS

        If ComboBox2.Items.IndexOf(ComboBox2.Text) = -1 Then

            MsgBox("Select correct Product", MsgBoxStyle.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        End If

        If ComboBox1.Text = "" Then
            MsgBox("Select PRODUCT name", MsgBoxStyle.OkOnly, "INVENTORY")
            Exit Sub
        End If

        If TextBox6.Text = "" Then
            MsgBox("Enter quantity", MsgBoxStyle.OkOnly, "INVENTORY")
            Exit Sub
        End If

        Dim proid As Integer    'Product ID
        cmd.CommandText = "select Product_id from PRODUCT where Product_name='" & ComboBox2.Text & "'"
        cmd.Connection = Login.cn
        proid = cmd.ExecuteScalar

        cmd.CommandText = "insert into PURCHASE_ORDER_DETAILS values(" & TextBox1.Text & "," & proid & "," & TextBox6.Text & ")"
        cmd.Connection = Login.cn
        cmd.ExecuteNonQuery()
        MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p1.Product_name as [Product name],p.QTY as [Quantity] from PURCHASE_ORDER_DETAILS p,PRODUCT p1 where p.PRODUCT_ID=p1.PRODUCT_ID and PURCHASE_ORDER_ID=" & Val(TextBox1.Text), Login.cn)
        DA11.Fill(ds4, "PURCHASE_ORDER_DETAILS")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()

        
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'UPDATE BUTTON
        Dim proid_cnt As Integer = 0
        Try

            Dim i1 As String
            cmd.CommandText = "select PRODUCT_ID from PRODUCT where PRODUCT_NAME='" & DataGridView1.CurrentRow.Cells(1).Value & "'"
            cmd.Connection = Login.cn
            i1 = cmd.ExecuteScalar
            proid_cnt = 1
            ' If DataGridView1.CurrentRow.Cells(3).Selected Then
            cmd.CommandText = "update PURCHASE_ORDER_DETAILS set QTY='" & DataGridView1.CurrentRow.Cells(2).Value & "',PRODUCT_ID='" & i1 & "' where PURCHASE_ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")

            Dim ds2 As New DataSet
            Dim adap As SqlDataAdapter
            ds2.Clear()
            adap = New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p1.Product_name as [Product name],p.QTY as [Quantity] from PURCHASE_ORDER_DETAILS p,PRODUCT p1 where p.PRODUCT_ID=p1.PRODUCT_ID and PURCHASE_ORDER_ID=" & Val(TextBox1.Text), Login.cn)
            adap.Fill(ds2, "PURCHASE_ORDER_DETAILS")
            DataGridView1.DataSource = ds2.Tables(0)

        Catch ex As Exception
            If proid_cnt = 0 Then
                MsgBox("Invalid Product Name", MsgBoxStyle.OkOnly, "INVENTORY")
            Else
                MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
            End If
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'DELETE BUTTON
        Dim i As Integer
        Try
            Dim b As MsgBoxResult
            b = MsgBox("Are you sure want to Delete Record", MsgBoxStyle.YesNo, "INVENTORY")
            If b = MsgBoxResult.Yes Then

                Dim ds2 As New DataSet
                Dim adap As SqlDataAdapter
                cmd.CommandText = "delete from PURCHASE_ORDER_DETAILS where PURCHASE_ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record Sucessfully Deleted", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p1.Product_name as [Product name],p.QTY as [Quantity] from PURCHASE_ORDER_DETAILS p,PRODUCT p1 where p.PRODUCT_ID=p1.PRODUCT_ID and PURCHASE_ORDER_ID=" & Val(TextBox1.Text), Login.cn)
                adap.Fill(ds2, "PURCHASE_ORDER_DETAILS")
                DataGridView1.DataSource = ds2.Tables(0)
                DataGridView1.Refresh()
                i += 1
            End If
        Catch ex As Exception
            If i = 0 Then
                MsgBox("Record Can Not be Deleted.. It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
            Else
                Exit Sub
            End If
        End Try
    End Sub

  
   
   
    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[0-9]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub
End Class