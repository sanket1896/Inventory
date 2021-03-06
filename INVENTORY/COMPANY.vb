﻿Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class COMPANY
    Dim da As New SqlDataAdapter("select * from COMPANY_MASTER", Login.cn)
    Dim da1 As New SqlDataAdapter("select * from CITY", Login.cn)
    Dim ds As New System.Data.DataSet
    Dim rpos As Integer
    Dim addmode As Boolean
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand
    Dim combocname As String        'THIS VARIABLE IS USED WHEN THE SELECTED ITEM OF COMBOBOX1 IS CHANGED
    Dim cityname As String
    Private Sub COMPANY_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CITYTableAdapter.Fill(Me.cityDataSet.CITY)
        da.Fill(ds, "COMPANY_MASTER")
        da1.Fill(ds, "CITY")
        showdata()
    End Sub

    Sub showdata()

        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        TextBox2.Text = ds.Tables(0).Rows(rpos).Item(1).ToString
        TextBox3.Text = ds.Tables(0).Rows(rpos).Item(2).ToString
        TextBox4.Text = ds.Tables(0).Rows(rpos).Item(3).ToString
        TextBox5.Text = ds.Tables(0).Rows(rpos).Item(5).ToString
        TextBox6.Text = ds.Tables(0).Rows(rpos).Item(6).ToString
        TextBox7.Text = ds.Tables(0).Rows(rpos).Item(7).ToString
        TextBox8.Text = ds.Tables(0).Rows(rpos).Item(8).ToString
        TextBox9.Text = ds.Tables(0).Rows(rpos).Item(9).ToString

        cmd.CommandText = "select p.CITY_NAME from CITY p, COMPANY_MASTER p1 where p1.COMPANY_ID='" & Val(TextBox1.Text) & "' and p.CITY_ID=p1.CITY_ID"
        cmd.Connection = Login.cn
        cityname = cmd.ExecuteScalar()
        ComboBox1.Text = cityname

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
        ' ADD BUTTON

        TextBox1.Text = ds.Tables(0).Rows.Count + 1         'TO GET THE TOTAL NUMBER OF ROWS IN THE DATASET TABLE
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()


        TextBox2.Focus()

        addmode = True
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'INSERT OR UPDATE BUTTON

        If TextBox1.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If TextBox2.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If TextBox3.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If TextBox4.Text.Trim = "" Then
            MsgBox("Enter Valid area name.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If TextBox5.Text.Trim = "" Then
            MsgBox("Enter Valid Pincode.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If TextBox5.Text.Length < 6 Then
            MsgBox("Enter 6 digit pincode.")
            Exit Sub
        End If

        If TextBox6.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If TextBox7.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If TextBox8.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If TextBox8.Text.Length < 9 Then
            MsgBox("Enter 10 digit Mobile number.")
            Exit Sub
        End If

        If TextBox9.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            Exit Sub
        End If





        cmd.CommandText = "select CITY_ID from CITY where CITY_NAME='" & combocname & "'"     'TO FETCH THE CITY ID FROM THE CITY TABLE WHERE THE CITY NAME WOULD BE SELECTED FROM THE COMBOBOX1
        cmd.Connection = Login.cn
        Dim cid As Integer      'TO STORE THE CITY ID
        cid = cmd.ExecuteScalar
        'MsgBox(cid)
        Try
            If addmode = True Then          'RECORD WILL BE INSERTED

                Dim dtr As DataRow = ds.Tables(0).Rows.Add
                dtr.Item(0) = TextBox1.Text
                dtr.Item(1) = TextBox2.Text
                dtr.Item(2) = TextBox3.Text
                dtr.Item(3) = TextBox4.Text
                dtr.Item(4) = cid
                dtr.Item(5) = TextBox5.Text
                dtr.Item(6) = TextBox6.Text
                dtr.Item(7) = TextBox7.Text
                dtr.Item(8) = TextBox8.Text
                dtr.Item(9) = TextBox9.Text

                da.Update(ds, "COMPANY_MASTER")
                addmode = False
                MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
            Else                            'RECORD WILL BE UPDATED
                Dim dtr As DataRow = ds.Tables(0).Rows(rpos)
                dtr.Item(0) = TextBox1.Text
                dtr.Item(1) = TextBox2.Text
                dtr.Item(2) = TextBox3.Text
                dtr.Item(3) = TextBox4.Text
                dtr.Item(4) = cid
                dtr.Item(5) = TextBox5.Text
                dtr.Item(6) = TextBox6.Text
                dtr.Item(7) = TextBox7.Text
                dtr.Item(8) = TextBox8.Text
                dtr.Item(9) = TextBox9.Text

                da.Update(ds, "COMPANY_MASTER")
                MsgBox("Record sucessfully updated", MsgBoxStyle.OkOnly, "INVENTORY")
            End If
        Catch ex As Exception
            MsgBox("Can't able to insert data because of data conflict.", MsgBoxStyle.Critical)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        combocname = ComboBox1.Text
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress, TextBox8.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress

        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[0-9]" Then  'not 0-9 then ignore
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox9_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox9.Leave
        Dim pattern As String = "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$"


        Dim match As System.Text.RegularExpressions.Match = Regex.Match(TextBox9.Text.Trim(), pattern, RegexOptions.IgnoreCase)
        If (match.Success) Then
            'MsgBox("done")
        Else
            MsgBox("Please enter valid mail id", MsgBoxStyle.Exclamation)
            ' TextBox9.Clear()
            TextBox9.Focus()

        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[a-z,A-Z]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox8.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[0-9]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub
End Class