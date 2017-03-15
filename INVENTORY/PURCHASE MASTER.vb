Imports System.Data.SqlClient
Public Class PURCHASE_MASTER
    Dim da As New SqlDataAdapter("select * from PURCHASE_MASTER", Login.cn)
    Dim da2 As New SqlDataAdapter("select * from PRODUCT", Login.cn)
    Dim da31 As New SqlDataAdapter("select * from PURCHASE_DETAIL", Login.cn)
    Dim ds, ds12, ds2 As New DataSet
    Dim rpos, amt, vat1, addvat1, ans As Integer
    Dim addmodd As Boolean
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand
    Dim net As Double = 0
    Dim qty As Integer = 0

    Dim addmode As Boolean = False

    Sub showdata()

        cmd.CommandText = "SELECT PURCHASE_ORDER_DATE FROM PURCHASE_ORDER_MASTER AS P,PURCHASE_MASTER AS P1 WHERE P.PURCHASE_ORDER_ID=P1.PURCHASE_ORDER_ID"
        cmd.Connection = Login.cn
        DateTimePicker1.Value = cmd.ExecuteScalar()
        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        TextBox2.Text = ds.Tables(0).Rows(rpos).Item(2).ToString
        TextBox3.Text = ds.Tables(0).Rows(rpos).Item(5).ToString
        DateTimePicker2.Text = ds.Tables(0).Rows(rpos).Item(3).ToString
        TextBox4.Text = ds.Tables(0).Rows(rpos).Item(6).ToString
        TextBox5.Text = ds.Tables(0).Rows(rpos).Item(7).ToString
        TextBox8.Text = ds.Tables(0).Rows(rpos).Item(4).ToString


    End Sub
    Private Sub PURCHASE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "PURCHASE_MASTER")
        'da31.Fill(ds12, "PURCHASE_DETAIL")
        'da2.Fill(ds2, "PRODUCT")
        showdata()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'FIRST RECORD BUTTON

        rpos = 0
        showdata()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST RECORD BUTTON
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREV BUTTON

        If rpos > 0 Then
            rpos = rpos - 1
            showdata()
        End If

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' NEXT BUTTON

        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON

        TextBox1.Text = Val(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(0)) + 1
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox8.Clear()

        addmode = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' SAVE BUTTON

        ''''''''''''''''''''''''''''''''''''''''''''*****

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
End Class