Imports System.Data.SqlClient
Public Class PAID_PAYMENT
    Dim da As New SqlDataAdapter("SELECT * FROM PAID_PAYMENT", Login.cn)
    Dim ds As New DataSet
    Dim addmodd As Boolean
    Dim rpos As Integer
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub PAID_PAYMENT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "PAID_PAYMENT")
        showdata()
        Dim dr2 As SqlDataReader
        cmd.CommandText = "select purchase_ID from PURCHASE_MASTER"
        cmd.Connection = Login.cn
        dr2 = cmd.ExecuteReader
        Do While dr2.Read
            ComboBox1.Items.Add(dr2.GetValue(0).ToString)
        Loop
        dr2.Close()
        Dim dr3 As SqlDataReader
        cmd.CommandText = "select status_NAME from STATUS"
        cmd.Connection = Login.cn
        dr3 = cmd.ExecuteReader
        Do While dr3.Read
            ComboBox2.Items.Add(dr3.GetValue(0).ToString)
        Loop
        dr3.Close()
    End Sub
    Sub showdata()

        cmd.CommandText = "SELECT S.STATUS_NAME FROM STATUS S, PAID_PAYMENT P WHERE P.STATUS_ID=S.STATUS_ID AND S.STATUS_ID=" & ds.Tables(0).Rows(rpos).Item(3)
        cmd.Connection = Login.cn
        Dim stats As String = cmd.ExecuteScalar

        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        ComboBox1.Text = ds.Tables(0).Rows(rpos).Item(1).ToString
        TextBox2.Text = ds.Tables(0).Rows(rpos).Item(2).ToString
        ComboBox2.Text = stats
        TextBox4.Text = ds.Tables(0).Rows(rpos).Item(4).ToString
        DateTimePicker1.Text = ds.Tables(0).Rows(rpos).Item(5).ToString
        TextBox5.Text = ds.Tables(0).Rows(rpos).Item(6).ToString
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'PEV BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'NEXT BUTTON CLICK
        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON
        TextBox1.Text = Val(ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(0)) + 1
        ComboBox1.Text = ""
        TextBox2.Clear()
        ComboBox2.Text = ""
        TextBox4.Clear()
        DateTimePicker1.Text = Date.Today
        TextBox5.Clear()
        addmodd = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON


        If ComboBox1.Items.IndexOf(ComboBox1.Text) = -1 Then
            MsgBox("Select correct Purchase ID.", MsgBoxStyle.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        End If

        If ComboBox1.Text = "" Then
            MsgBox("Select Purchase ID.", MsgBoxStyle.OkOnly, "INVENTORY")
            Exit Sub
        End If


        If ComboBox2.Items.IndexOf(ComboBox2.Text) = -1 Then
            MsgBox("Select correct Status.", MsgBoxStyle.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        End If

        If ComboBox2.Text = "" Then
            MsgBox("Select Status.", MsgBoxStyle.OkOnly, "INVENTORY")
            Exit Sub
        End If




        If addmodd = True Then
            Try

                cmd.CommandText = "SELECT STATUS_ID FROM STATUS WHERE STATUS_NAME='" & ComboBox2.Text & "'"
                cmd.Connection = Login.cn
                Dim stid As Integer = cmd.ExecuteScalar

                Dim dr As DataRow = ds.Tables(0).Rows.Add
                dr.Item(0) = TextBox1.Text
                dr.Item(1) = ComboBox1.Text
                dr.Item(2) = TextBox2.Text
                dr.Item(3) = stid
                dr.Item(4) = Val(TextBox4.Text)
                dr.Item(5) = DateTimePicker1.Value.Date
                dr.Item(6) = TextBox1.Text
                da.Update(ds, "PAID_PAYMENT")
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
        'UPDATE BUTTON
        Try
            cmd.CommandText = "SELECT STATUS_ID FROM STATUS WHERE STATUS_NAME='" & ComboBox2.Text & "'"
            cmd.Connection = Login.cn
            Dim stid As Integer = cmd.ExecuteScalar

            cmd.CommandText = "UPDATE PAID_PAYMENT SET PAID_AMOUNT=" & TextBox2.Text & ",STATUS_ID=" & stid & ",CHECK_NO=" & Val(TextBox4.Text) & ",CHECK_DATE=" & DateTimePicker1.Value.Date & ",BANK_NAME='" & TextBox5.Text & "' WHERE PAID_ID=" & TextBox1.Text
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("SUCCESSFULLY UPDATED", MsgBoxStyle.OkOnly, "INVENTORY")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress, TextBox4.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[0-9]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub


    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[a-z,A-Z]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub

   
End Class