Imports System.Data.SqlClient
Public Class Login
    Public Shared cn As New SqlConnection
    Dim cmd As New SqlCommand
    Public Shared userid As String


    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.ConnectionString = ("Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\sanket\Desktop\INVENTORY\INVENTORY\INVANTORY.mdf;Integrated Security=True;User Instance=True")
        cn.Open()

        'SqlConnectionDB   ' hidden connection (not shown in code)////////////
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        FORGET_PASSWORD.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        ' Declare sql connection and command text
        cmd.CommandText = "select PASSWORD from LOGIN where USER_ID = '" & TextBox1.Text & "'"
        cmd.Connection = Login.cn
        Dim str As String
        str = cmd.ExecuteScalar

        ' Process of checking cradential
        If TextBox1.Text = "" Then
            MsgBox("Enter the Username", MsgBoxStyle.OkOnly, "INVENTORY")
            TextBox1.Focus()
            Exit Sub
        ElseIf TextBox2.Text = "" Then
            MsgBox("Enter the Password", MsgBoxStyle.OkOnly, "INVENTORY")
            TextBox2.Focus()
            Exit Sub
        ElseIf str = "" Then
            MsgBox("Invalid Username or Password", MsgBoxStyle.OkOnly, "INVENTORY")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Focus()
            Exit Sub
        ElseIf TextBox2.Text = str Then
            userid = TextBox1.Text
            NEELKANTH_ENTERPRISE.Show()
            Me.Hide()
        Else
            MsgBox("Invalid Username or Password", MsgBoxStyle.OkOnly, "INVENTORY")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Focus()

        End If
    End Sub

End Class
