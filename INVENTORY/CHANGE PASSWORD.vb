Imports System.Data.SqlClient

Public Class CHANGE_PASSWORD
    Dim cmd As New SqlCommand

    

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("Enter old password", MsgBoxStyle.OkOnly, "INVENTORY")
            TextBox1.Focus()
            Exit Sub
        End If
        If TextBox2.Text = "" Then
            MsgBox("Enter new password", MsgBoxStyle.OkOnly, "INVENTORY")
            TextBox1.Focus()
            Exit Sub
        End If
        Dim str1 As String
        cmd.CommandText = "select Password from LOGIN where USER_ID ='" & Login.userid & "'"
        cmd.Connection = Login.cn
        str1 = cmd.ExecuteScalar
        If Not TextBox1.Text = str1.ToString Then
            MsgBox("Your old password is wrong...", MsgBoxStyle.OkOnly, "INVENTORY")
            TextBox1.Clear()
            TextBox1.Focus()
            Exit Sub
        ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Enter Currect Password", MsgBoxStyle.OkOnly, "INVENTORY")

        ElseIf TextBox2.Text = TextBox3.Text Then


            cmd.CommandText = "update LOGIN set Password ='" & TextBox2.Text & "' where USER_ID='" & Login.userid & "'"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("Your password is changed", MsgBoxStyle.OkOnly, "INVENTORY")
            Me.Close()

        Else
            MsgBox("Enter New & Confirm Currect Password", MsgBoxStyle.OkOnly, "INVENTORY")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub
End Class