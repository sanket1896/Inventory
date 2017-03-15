Imports System.Data.SqlClient
Public Class SALES_ORDER
    Dim da As New SqlDataAdapter("select * from SALES_ORDER_MASTER", Login.cn)
    Dim da1 As New SqlDataAdapter("select * from PRODUCT", Login.cn)
    Dim da3 As New SqlDataAdapter("select * from CUSTOMER", Login.cn)
    Dim da4 As New SqlDataAdapter("select * from UOM", Login.cn)
    Dim ds, ds1, ds3 As New DataSet
    Dim rpos As Integer
    Dim addmodd As Boolean = False
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand

    Private Sub SALES_ORDER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "SALES_ORDER_MASTER")
        da1.Fill(ds1, "PRODUCT")
        da3.Fill(ds3, "CUSTOMER")
        Dim dr2 As SqlDataReader
        cmd.CommandText = "select CUSTOMER_NAME from CUSTOMER"
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

        Dim dr5 As SqlDataReader
        cmd.CommandText = "select UOM_NAME from UOM"
        cmd.Connection = Login.cn
        dr5 = cmd.ExecuteReader
        Do While dr5.Read
            ComboBox3.Items.Add(dr5.GetValue(0).ToString)
        Loop
        dr5.Close()

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],P2.UOM_NAME AS [UOM], p.QTY as [Quantity],P.DUE_DATE AS [DUE DATE] from SALES_ORDER_DETAIL p,PRODUCT p1,UOM P2 where p.PRODUCT_ID=p1.PRODUCT_ID AND P.UOM_ID=P2.UOM_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "SALES_ORDER_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
    End Sub

    Sub showdata()
        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        DateTimePicker1.Text = ds.Tables(0).Rows(rpos).Item(2).ToString
        Dim i As String
        cmd.CommandText = "select p.CUSTOMER_NAME from CUSTOMER p, SALES_ORDER_MASTER p1 where ORDER_ID='" & TextBox1.Text & "' and p.CUSTOMER_ID=p1.CUSTOMER_ID"
        cmd.Connection = Login.cn
        i = cmd.ExecuteScalar
        ComboBox1.Text = i
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON
        TextBox1.Text = Val(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(0)) + 1
        addmodd = True
        ComboBox1.Text = ""
        Button1.Enabled = False
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON
        If addmodd = True Then
            Dim i As Integer
            cmd.CommandText = "select CUSTOMER_ID from CUSTOMER WHERE CUSTOMER_NAME LIKE'" & ComboBox1.SelectedItem.ToString & "'"
            cmd.Connection = Login.cn
            i = cmd.ExecuteScalar
            Try
                Dim dr As DataRow = ds.Tables(0).Rows.Add
                dr.Item(0) = TextBox1.Text
                dr.Item(2) = DateTimePicker1.Text
                dr.Item(1) = i
                da.Update(ds, "SALES_ORDER_MASTER")
                addmodd = False
                MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")

                Dim ds4 As New DataSet
                ds4.Clear()
                Dim DA11 As New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],P2.UOM_NAME AS [UOM], p.QTY as [Quantity],P.DUE_DATE AS [DUE DATE] from SALES_ORDER_DETAIL p,PRODUCT p1,UOM P2 where p.PRODUCT_ID=p1.PRODUCT_ID AND P.UOM_ID=P2.UOM_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
                DA11.Fill(ds4, "SALES_ORDER_DETAIL")
                DataGridView1.DataSource = ds4.Tables(0)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Please First Click on Insert Button", MsgBoxStyle.OkOnly, "INVENTORY")
            Button1.Focus()
            Exit Sub
        End If
        Button1.Enabled = True
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],P2.UOM_NAME AS [UOM], p.QTY as [Quantity],P.DUE_DATE AS [DUE DATE] from SALES_ORDER_DETAIL p,PRODUCT p1,UOM P2 where p.PRODUCT_ID=p1.PRODUCT_ID AND P.UOM_ID=P2.UOM_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "SALES_ORDER_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREVIOUS BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()

            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],P2.UOM_NAME AS [UOM], p.QTY as [Quantity],P.DUE_DATE AS [DUE DATE] from SALES_ORDER_DETAIL p,PRODUCT p1,UOM P2 where p.PRODUCT_ID=p1.PRODUCT_ID AND P.UOM_ID=P2.UOM_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            DA11.Fill(ds4, "SALES_ORDER_DETAIL")
            DataGridView1.DataSource = ds4.Tables(0)
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'NEXT BUTTON CLICK
        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()

            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],P2.UOM_NAME AS [UOM], p.QTY as [Quantity],P.DUE_DATE AS [DUE DATE] from SALES_ORDER_DETAIL p,PRODUCT p1,UOM P2 where p.PRODUCT_ID=p1.PRODUCT_ID AND P.UOM_ID=P2.UOM_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            DA11.Fill(ds4, "SALES_ORDER_DETAIL")
            DataGridView1.DataSource = ds4.Tables(0)
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],P2.UOM_NAME AS [UOM], p.QTY as [Quantity],P.DUE_DATE AS [DUE DATE] from SALES_ORDER_DETAIL p,PRODUCT p1,UOM P2 where p.PRODUCT_ID=p1.PRODUCT_ID AND P.UOM_ID=P2.UOM_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "SALES_ORDER_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        SALES_ORDER_SEARCH.ShowDialog()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        'INSERT BUTTON SALES ORDER DETAILS
        Dim proid As Integer    'Product ID
        cmd.CommandText = "select Product_id from PRODUCT where Product_name='" & ComboBox2.Text & "'"
        cmd.Connection = Login.cn
        proid = cmd.ExecuteScalar

        Dim uomid As Integer    'UOM ID
        cmd.CommandText = "SELECT UOM_ID FROM UOM WHERE UOM_NAME='" & ComboBox3.Text & "'"
        cmd.Connection = Login.cn
        uomid = cmd.ExecuteScalar

        cmd.CommandText = "insert into SALES_ORDER_DETAIL values(" & TextBox1.Text & "," & proid & "," & uomid & "," & TextBox6.Text & "," & DateTimePicker2.Value.Date & ")"
        cmd.Connection = Login.cn
        cmd.ExecuteNonQuery()
        MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],P2.UOM_NAME AS [UOM], p.QTY as [Quantity],P.DUE_DATE AS [DUE DATE] from SALES_ORDER_DETAIL p,PRODUCT p1,UOM P2 where p.PRODUCT_ID=p1.PRODUCT_ID AND P.UOM_ID=P2.UOM_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "SALES_ORDER_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'UPDATE BUTTON SALES ORDER DETAIL
        Dim proid_cnt As Integer = 0
        'Try

        Dim i1 As String
        cmd.CommandText = "select PRODUCT_ID from PRODUCT where PRODUCT_NAME='" & DataGridView1.CurrentRow.Cells(1).Value & "'"
        cmd.Connection = Login.cn
        i1 = cmd.ExecuteScalar
        proid_cnt = 1

        Dim uomid As Integer
        cmd.CommandText = "SELECT UOM_ID FROM UOM WHERE UOM_NAME='" & DataGridView1.CurrentRow.Cells(2).Value & "'"
        cmd.Connection = Login.cn
        uomid = cmd.ExecuteScalar

        ' If DataGridView1.CurrentRow.Cells(3).Selected Then
        cmd.CommandText = "update SALES_ORDER_DETAIL set QTY='" & DataGridView1.CurrentRow.Cells(3).Value & "',PRODUCT_ID='" & i1 & "',UOM_ID='" & uomid & "', DUE_DATE='" & DataGridView1.CurrentRow.Cells(4).Value & "' where ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
        cmd.Connection = Login.cn
        cmd.ExecuteNonQuery()
        MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],P2.UOM_NAME AS [UOM], p.QTY as [Quantity],P.DUE_DATE AS [DUE DATE] from SALES_ORDER_DETAIL p,PRODUCT p1,UOM P2 where p.PRODUCT_ID=p1.PRODUCT_ID AND P.UOM_ID=P2.UOM_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "SALES_ORDER_DETAIL")
        DataGridView1.DataSource = ds4.Tables(0)

        'Catch ex As Exception
        '    If proid_cnt = 0 Then
        '        MsgBox("Invalid Product Name", MsgBoxStyle.OkOnly, "INVENTORY")
        '    Else
        '        MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        '    End If
        'End Try
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
                cmd.CommandText = "delete from SALES_ORDER_DETAIL where ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record Sucessfully Deleted", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("select p.ORDER_ID as [Sales Order id],p1.Product_name as [Product name],P2.UOM_NAME AS [UOM], p.QTY as [Quantity],P.DUE_DATE AS [DUE DATE] from SALES_ORDER_DETAIL p,PRODUCT p1,UOM P2 where p.PRODUCT_ID=p1.PRODUCT_ID AND P.UOM_ID=P2.UOM_ID and ORDER_ID='" & Val(TextBox1.Text) & "'", Login.cn)
                adap.Fill(ds2, "SALES_ORDER_DETAIL")
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

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Me.Close()
    End Sub
End Class