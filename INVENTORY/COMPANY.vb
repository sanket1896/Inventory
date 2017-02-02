Imports System.Data.SqlClient
Public Class COMPANY
    Dim da As New SqlDataAdapter("select * from COMPANY_MASTER", Login.cn)
    Dim da1 As New SqlDataAdapter("select * from CITY", Login.cn)
    Dim ds As New System.Data.DataSet
    Dim rpos As Integer
    Dim addmode As Boolean
    Dim cmd As New SqlCommand

    Private Sub COMPANY_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "COMPANY_MASTER")
        da1.Fill(ds, "CITY")
        showdata()
    End Sub

    Sub showdata()

        Dim i As String
        'cmd.CommandText = "select p.CITY_NAME from CITY p, COMPANY_MASTER p1 where p1.COMPANY_ID='" & Val(TextBox1.Text) & "' and p.CITY_ID=p1.CITY_ID"
        cmd.CommandText = "SELECT CITY.CITY_NAME FROM CITY CROSS JOIN COMPANY_MASTER WHERE COMPANY_MASTER.COMPANY_ID ='" & TextBox1.Text & "'"
        cmd.Connection = Login.cn
        i = cmd.ExecuteScalar()
        'MsgBox(cmd.ExecuteScalar())

        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        TextBox2.Text = ds.Tables(0).Rows(rpos).Item(1).ToString
        TextBox3.Text = ds.Tables(0).Rows(rpos).Item(2).ToString
        TextBox4.Text = ds.Tables(0).Rows(rpos).Item(3).ToString
        TextBox5.Text = ds.Tables(0).Rows(rpos).Item(5).ToString
        TextBox6.Text = ds.Tables(0).Rows(rpos).Item(6).ToString
        TextBox7.Text = ds.Tables(0).Rows(rpos).Item(7).ToString
        TextBox8.Text = ds.Tables(0).Rows(rpos).Item(8).ToString
        TextBox9.Text = ds.Tables(0).Rows(rpos).Item(9).ToString
        'ComboBox1.Text = ds.Tables(0).Rows(rpos).Item(4).ToString

        ComboBox1.Text = i
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        rpos = 0
        showdata()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos = rpos + 1
            showdata()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        Dim last As Integer
        last = ds.Tables(0).Rows.Count + 1
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()

        TextBox1.Text = last
        TextBox2.Focus()
        TextBox1.ReadOnly = True
        TextBox1.BackColor = Color.White
        addmode = True
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        cmd.CommandText = "select CITY_ID from CITY where CITY_NAME='" & ComboBox1.SelectedItem.ToString & "'"
        cmd.Connection = Login.cn
        Dim str As String
        str = cmd.ExecuteScalar
        If addmode = True Then
            Dim dtrow As DataRow = ds.Tables(0).NewRow
            dtrow.Item(0) = Val(TextBox1.Text)
            dtrow.Item(1) = (TextBox2.Text)
            dtrow.Item(2) = (TextBox3.Text)
            dtrow.Item(3) = (TextBox4.Text)
            dtrow.Item(4) = Val(TextBox5.Text)
            dtrow.Item(5) = Val(TextBox6.Text)
            dtrow.Item(6) = Val(TextBox7.Text)
            dtrow.Item(7) = (TextBox8.Text)
            dtrow.Item(8) = (TextBox9.Text)
            dtrow.Item(9) = str
            ds.Tables(0).Rows.Add(dtrow)
            da.Update(ds, "COMPANY_MASTER")
            MsgBox("New Company Details Inserted")
            addmode = False
            showdata()
        Else
            Dim dtrow As DataRow = ds.Tables(0).Rows(rpos)
            dtrow.Item(0) = Val(TextBox1.Text)
            dtrow.Item(1) = (TextBox2.Text)
            dtrow.Item(2) = (TextBox3.Text)
            dtrow.Item(3) = (TextBox4.Text)
            dtrow.Item(4) = Val(TextBox5.Text)
            dtrow.Item(5) = Val(TextBox6.Text)
            dtrow.Item(6) = Val(TextBox7.Text)
            dtrow.Item(7) = (TextBox8.Text)
            dtrow.Item(8) = (TextBox9.Text)
            dtrow.Item(9) = str
            ds.Tables(0).Rows.Add(dtrow)
            da.Update(ds, "COMPANY_MASTER")
            MsgBox("New Company Details Updated")
            showdata()
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Me.Close()
    End Sub
End Class