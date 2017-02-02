Imports System.Data.SqlClient

Public Class FORGET_CHANGE_PASSWORD
    Dim cmd As New SqlCommand
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If TextBox1.Text = "" Then
            MsgBox("Enter Password")
            TextBox1.Focus()

        ElseIf TextBox2.Text = "" Then
            MsgBox("Enter Confirm Password")
            TextBox2.Focus()

        ElseIf TextBox1.Text = TextBox2.Text Then

            cmd.CommandText = "update LOGIN set PASSWORD='" & TextBox2.Text & "' where USER_ID='" & Label4.Text & "'"
            cmd.Connection = Login.cn
            Dim str As String
            str = cmd.ExecuteScalar

            MsgBox("Change Password Successfuly")
            Me.Close()
        Else
            MsgBox("Enter Currect Password")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Focus()

        End If
    End Sub

    Private Sub FORGET_CHANGE_PASSWORD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label4.Text = FORGET_PASSWORD.userid


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class