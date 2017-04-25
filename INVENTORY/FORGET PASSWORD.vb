Imports System.Data.SqlClient
Public Class FORGET_PASSWORD
    Dim cmd As New SqlCommand
    Public Shared userid As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub FORGET_PASSWORD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim userid As String
        userid = InputBox("Enter UserID")
        Label6.Text = userid


        cmd.CommandText = "select SEQURITY_Question from LOGIN where USER_ID = '" & Label6.Text & "'"
        cmd.Connection = Login.cn
        Dim str As String
        str = cmd.ExecuteScalar

        Label5.Text = str



    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        cmd.CommandText = "select SEQURITY_ANSWER from LOGIN where USER_ID = '" & Label6.Text & "'"
        cmd.Connection = Login.cn
        Dim str As String
        str = cmd.ExecuteScalar
        'MsgBox(str)


        If TextBox2.Text = str Then
            userid = Label6.Text
            Me.Close()
            FORGET_CHANGE_PASSWORD.Show()

        Else
            MsgBox("Enter Currect Answer.")
            TextBox2.Clear()
            TextBox2.Focus()

        End If

    End Sub
End Class