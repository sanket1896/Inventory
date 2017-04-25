Imports System.Data.SqlClient

Public Class CREATE_NEW_USER
    Dim da As New SqlDataAdapter("select * from COMPANY_MASTER", Login.cn)
    Dim ds As New DataSet
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand
    Private Sub CREATE_NEW_USER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            da.Fill(ds, "COMPANY_MASTER")
            ComboBox2.DataSource = ds.Tables(0)
            ComboBox2.ValueMember = "COMPANY_ID"
            ComboBox2.DisplayMember = "COMAPNY_NAME"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox5.Clear()
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        MsgBox("ID :" & ComboBox2.SelectedValue.ToString)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox1.Text <> "" Then
            If TextBox2.Text <> "" Then
                If TextBox3.Text <> "" Then
                    If ComboBox1.Text <> "" Then
                        If TextBox5.Text <> "" Then
                            If TextBox2.Text = TextBox3.Text Then
                                Try
                                    Dim sq As String = ComboBox1.Text
                                    cmd.CommandText = "INSERT INTO LOGIN VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & sq & "','" & TextBox5.Text & "'," & ComboBox2.SelectedValue.ToString & ")"
                                    cmd.Connection = Login.cn
                                    cmd.ExecuteNonQuery()
                                    MsgBox("User created successfully", MsgBoxStyle.OkOnly, "INVENTORY")
                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                End Try
                            Else
                                MsgBox("Password and confirm password do not match", MsgBoxStyle.OkOnly, "INVENTORY")
                            End If
                        Else
                            MsgBox("Enter security answer", MsgBoxStyle.OkOnly, "INVENTORY")
                        End If
                    Else
                        MsgBox("Select security question", MsgBoxStyle.OkOnly, "INVENTORY")
                    End If
                Else
                    MsgBox("Re-enter password", MsgBoxStyle.OkOnly, "INVENTORY")
                End If
            Else
                MsgBox("Enter password", MsgBoxStyle.OkOnly, "INVENTORY")
            End If
        Else
            MsgBox("Enter user name", MsgBoxStyle.OkOnly, "INVENTORY")
        End If
    End Sub

End Class