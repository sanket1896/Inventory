Imports System.Data.SqlClient
Public Class PRODUCT
    Dim da As New SqlDataAdapter("select * from PRODUCT", Login.cn)
    Dim da1 As New SqlDataAdapter("select * from UOM", Login.cn)
    Dim da2 As New SqlDataAdapter("select * from CATEGORY", Login.cn)
    Dim ds As New DataSet
    Dim rpos As Integer
    Dim addmodd As Boolean
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand
    Dim uomname As String
    Dim category_name As String
    Private Sub PRODUCT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "PRODUCT")
        da1.Fill(ds, "UOM")
        da2.Fill(ds, "CATEGORY")
        showdata()

        Dim dr As SqlDataReader
        cmd.CommandText = "select UOM_NAME from UOM"
        cmd.Connection = Login.cn
        dr = cmd.ExecuteReader

        Do While dr.Read
            ComboBox1.Items.Add(dr.GetValue(0).ToString)
        Loop
        dr.Close()


        Dim dr1 As SqlDataReader
        cmd.CommandText = "SELECT CATEGORY_NAME FROM CATEGORY"
        cmd.Connection = Login.cn
        dr1 = cmd.ExecuteReader

        Do While dr1.Read
            ComboBox2.Items.Add(dr1.GetValue(0).ToString)
        Loop
        dr1.Close()

        'TO SHOW THE PRODUCT IN GRIDVIEW FROM THE PRODUCT TABLE WHOSE ID IS SHOWWN IN TEXTBOX1
        Dim ds3 As New DataSet
        Dim da3 As New SqlClient.SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.VAT AS [VAT],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID AND P.PRODUCT_ID=" & Val(TextBox1.Text), Login.cn)
        ds3.Clear()
        da3.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
    End Sub


    Sub showdata()

        cmd.CommandText = "SELECT UOM_NAME FROM UOM P, PRODUCT P1 WHERE P.UOM_ID=P1.UOM_ID"
        cmd.Connection = Login.cn
        uomname = cmd.ExecuteScalar()
        cmd.CommandText = "SELECT CATEGORY_NAME FROM CATEGORY P, PRODUCT P1 WHERE P.CATEGORY_ID=P1.CATEGORY_ID"
        cmd.Connection = Login.cn
        category_name = cmd.ExecuteScalar()
        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        TextBox2.Text = ds.Tables(0).Rows(rpos).Item(1).ToString
        TextBox4.Text = ds.Tables(0).Rows(rpos).Item(3).ToString
        TextBox5.Text = ds.Tables(0).Rows(rpos).Item(4).ToString
        TextBox6.Text = ds.Tables(0).Rows(rpos).Item(5).ToString
        TextBox7.Text = ds.Tables(0).Rows(rpos).Item(6).ToString

        cmd.CommandText = "select p.UOM_NAME from UOM p, PRODUCT p1 where p1.PRODUCT_ID=" & Val(TextBox1.Text) & " and p.UOM_ID=p1.UOM_ID"
        cmd.Connection = Login.cn
        uomname = cmd.ExecuteScalar()

        cmd.CommandText = "select p.CATEGORY_NAME from CATEGORY p, PRODUCT p1 where p1.PRODUCT_ID=" & Val(TextBox1.Text) & " and p.CATEGORY_ID=p1.CATEGORY_ID"
        cmd.Connection = Login.cn
        category_name = cmd.ExecuteScalar()

        ComboBox1.Text = uomname
        ComboBox2.Text = category_name

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON CLICK
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.VAT AS [VAT],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
        TextBox2.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox1.Text = Val(ds3.Tables(0).Rows(ds3.Tables(0).Rows.Count - 1).Item(0)) + 1
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON CLICK
        'INSERT QUERRY

        If TextBox1.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox1.Focus()
            Exit Sub
        End If
        If TextBox2.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox2.Focus()
            Exit Sub
        End If


        If TextBox4.Text.Trim = "" Then
            MsgBox("Enter Valid QTY.", MsgBoxStyle.Exclamation)
            TextBox4.Focus()
            Exit Sub
        End If

        If TextBox5.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox5.Focus()
            Exit Sub
        End If

        If TextBox6.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox6.Focus()
            Exit Sub
        End If

        If TextBox7.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox7.Focus()
            Exit Sub
        End If

        If ComboBox1.Text.Trim = "" Then
            MsgBox("Select Valid value", MsgBoxStyle.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        End If
        If Not ComboBox1.Text.Trim = "" Then
            Dim i As String
            i = ComboBox1.Items.IndexOf(ComboBox1.Text)
            If i = -1 Then
                MsgBox("Select currect UOM.")
                Exit Sub
            End If
            ComboBox1.Focus()
        End If

        If ComboBox2.Text.Trim = "" Then
            MsgBox("Select Valid value", MsgBoxStyle.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        End If
        If Not ComboBox2.Text.Trim = "" Then
            Dim i As String
            i = ComboBox2.Items.IndexOf(ComboBox2.Text)
            If i = -1 Then
                MsgBox("Select currect Category.")
                Exit Sub
            End If
            ComboBox2.Focus()
        End If






        Try
            cmd.CommandText = "select UOM_ID from UOM where UOM_name='" & ComboBox1.SelectedItem.ToString & "'"
            cmd.Connection = Login.cn
            Dim uomid As Integer
            uomid = cmd.ExecuteScalar

            MsgBox(uomid)

            cmd.CommandText = "select CATEGORY_ID from CATEGORY where CATEGORY_name='" & ComboBox2.SelectedItem.ToString & "'"
            cmd.Connection = Login.cn
            Dim category_id As Integer
            category_id = cmd.ExecuteScalar

            MsgBox(category_id)

            Dim dr As DataRow = ds.Tables(0).Rows.Add
            dr.Item(0) = TextBox1.Text  'SUPPLIER ID
            dr.Item(1) = TextBox2.Text  'SUPPLIER NAME
            dr.Item(2) = uomid          'UOM NAME
            dr.Item(3) = TextBox4.Text  'QOH
            dr.Item(4) = TextBox5.Text  'PRICE
            dr.Item(5) = TextBox6.Text  'VAT
            dr.Item(6) = TextBox7.Text  'ADDITIONAL VAT
            dr.Item(7) = category_id  'CATEGORY NAME
            da.Update(ds, "PRODUCT")
            addmodd = False
            MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
        Catch ex As Exception
            MsgBox(ex.Message)

            MsgBox("you cna not insert a duplicate record...", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try


        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.VAT AS [VAT],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID AND P.PRODUCT_ID=" & Val(TextBox1.Text), Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()
        
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.VAT AS [VAT],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID AND P.PRODUCT_ID=" & Val(TextBox1.Text), Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREVIOUS BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()
            
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.VAT AS [VAT],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID AND P.PRODUCT_ID=" & Val(TextBox1.Text), Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "PRODUCT")
            DataGridView1.DataSource = ds3.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'NEXT BUTTON CLICK

        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()
            
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.VAT AS [VAT],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID AND P.PRODUCT_ID=" & Val(TextBox1.Text), Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "PRODUCT")
            DataGridView1.DataSource = ds3.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.VAT AS [VAT],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID AND P.PRODUCT_ID=" & Val(TextBox1.Text), Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        'SEARCHING THE PRODUCT
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT * FROM PRODUCT WHERE PRODUCT_NAME LIKE '" & TextBox9.Text & "%'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'ALL BUTTON CLICK
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID ", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "PRODUCT")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'UPDATE BUTTON CLICK
        Dim uomid As Integer
        Dim catid As Integer
        Try
            MsgBox("Record successfully updated.")
            cmd.CommandText = "select UOM_ID from UOM where UOM_name='" & DataGridView1.CurrentRow.Cells(6).Value & "'"
            cmd.Connection = Login.cn
            uomid = cmd.ExecuteScalar

            cmd.CommandText = "select CATEGORY_ID from CATEGORY where CATEGORY_name='" & DataGridView1.CurrentRow.Cells(7).Value & "'"
            cmd.Connection = Login.cn
            catid = cmd.ExecuteScalar


            Dim ds3 As New DataSet
            Dim adap1 As SqlDataAdapter
            MsgBox(catid)
            cmd.CommandText = "update PRODUCT set PRODUCT_name='" & DataGridView1.CurrentRow.Cells(1).Value & "',QOH='" & DataGridView1.CurrentRow.Cells(2).Value & "',PRICE='" & DataGridView1.CurrentRow.Cells(3).Value & "',VAT='" & DataGridView1.CurrentRow.Cells(4).Value & "',ADITIONAL_VAT='" & DataGridView1.CurrentRow.Cells(5).Value & "',UOM_ID='" & uomid & "',CATEGORY_ID='" & catid & "'where product_id='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
            ds3.Clear()
            adap1 = New SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.VAT AS [VAT],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID AND P.PRODUCT_ID=" & Val(TextBox1.Text), Login.cn)
            adap1.Fill(ds3, "PRODUCT")
            DataGridView1.DataSource = ds3.Tables(0)
            ds.Clear()
            da.Fill(ds, "PRODUCT")
        Catch ex As Exception
            '    MsgBox(ex.Message)
            MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'DELETE BUTTON CLICK
        Try
            Dim b As MsgBoxResult
            b = MsgBox("Are you sure want to Delete Record", MsgBoxStyle.YesNo, "INVENTORY")
            If b = MsgBoxResult.Yes Then
                Dim ds2 As New DataSet
                Dim adap As SqlDataAdapter

                cmd.CommandText = "delete from product where product_id='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record Sucessfully Deleted", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("SELECT P.PRODUCT_ID AS[PRODUCT ID],P.PRODUCT_NAME AS [PRODUCT NAME],P.QOH AS [QOH],P.PRICE AS [PRICE],P.VAT AS [VAT],P.ADITIONAL_VAT AS [ADDITIONAL VAT],P2.UOM_NAME AS [UOM NAME],P3.CATEGORY_NAME AS [CATEGORY NAME] FROM PRODUCT P,UOM P2,CATEGORY P3 WHERE P.CATEGORY_ID=P3.CATEGORY_ID AND P.UOM_ID=P2.UOM_ID AND P.PRODUCT_ID=" & Val(TextBox1.Text), Login.cn)
                adap.Fill(ds2, "PRODUCT")
                DataGridView1.DataSource = ds2.Tables(0)
                DataGridView1.Refresh()
                ds.Clear()
                da.Fill(ds, "PRODUCT")
            End If

            ' showdata()
        Catch ex As Exception
            MsgBox("Record Can Not be Deleted.. It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try

    End Sub

   
    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox4.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[0-9]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub
End Class