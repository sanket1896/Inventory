Imports System.Data.SqlClient
Public Class COUNTRY

    Dim cmd As New SqlCommand
    Dim da As New SqlDataAdapter("select * from COUNTRY", Login.cn)
    Dim ds As New DataSet
    Dim rpos As Integer
    Dim addmodd As Boolean
    Dim cmdb As New SqlCommandBuilder(da)

    Private Sub COUNTRY_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "COUNTRY")
        showdata()
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT * FROM COUNTRY WHERE COUNTRY_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "COUNTRY")
        DataGridView1.DataSource = ds3.Tables(0)

    End Sub
    Sub showdata()
        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0)
        TextBox2.Text = ds.Tables(0).Rows(rpos).Item(1)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT * FROM COUNTRY WHERE COUNTRY_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "COUNTRY")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREVIOUS BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()

            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT * FROM COUNTRY WHERE COUNTRY_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "COUNTRY")
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
            Dim da2 As New SqlClient.SqlDataAdapter("SELECT * FROM COUNTRY WHERE COUNTRY_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "COUNTRY")
            DataGridView1.DataSource = ds3.Tables(0)
            DataGridView1.Refresh()

        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()

        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT * FROM COUNTRY WHERE COUNTRY_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "COUNTRY")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub


    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'ALL BUTTON CLICK
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'UPDATE BUTTON CLICK

        If IsNumeric(DataGridView1.CurrentRow.Cells(1).Value) Then
            MsgBox("Enetr only character", MsgBoxStyle.OkOnly, "INVENTORY")
        End If

        Try
            Dim ds2 As New DataSet
            Dim adap As SqlDataAdapter
            cmd.CommandText = "update COUNTRY set COUNTRY_NAME='" & DataGridView1.CurrentRow.Cells(1).Value & "' where COUNTRY_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
            ds2.Clear()
            adap = New SqlDataAdapter("SELECT * FROM COUNTRY", Login.cn)
            adap.Fill(ds2, "COUNTRY")
            DataGridView1.DataSource = ds2.Tables(0)
            ds.Clear()
            da.Fill(ds, "COUNTRY")
        Catch ex As Exception
            MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        'CLOSE BUTTON
        Me.Close()
    End Sub


    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        'VALIDATION OF THE COUNTRY NAME
        If e.KeyChar = vbBack Then Exit Sub
        If Char.IsWhiteSpace(e.KeyChar) Then Exit Sub
        If Not (e.KeyChar) Like "[a-z,A-Z]" Then
            e.Handled = True

        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        'SEARCHING THE COUNTRY
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT * FROM COUNTRY WHERE COUNTRY_NAME LIKE '" & TextBox3.Text & "%'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "COUNTRY")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'DELETE BUTTON
        Try
            Dim b As MsgBoxResult
            b = MsgBox("Are you sure want to Delete Record", MsgBoxStyle.YesNo, "INVENTORY")
            If b = MsgBoxResult.Yes Then
                Dim ds2 As New DataSet
                Dim adap As SqlDataAdapter
                cmd.CommandText = "DELETE FROM COUNTRY WHERE COUNTRY_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record Sucessfully Deleted", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("SELECT * FROM COUNTRY", Login.cn)
                adap.Fill(ds2, "COUNTRY")
                DataGridView1.DataSource = ds2.Tables(0)
                DataGridView1.Refresh()
                ds.Clear()
                da.Fill(ds, "COUNTRY")
            End If
        Catch ex As Exception
            MsgBox("Record Can Not be Deleted.. It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON
        'CLEAR ALL TEXTBOXES
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT * FROM COUNTRY ", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "COUNTRY")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
        TextBox2.Clear()
        TextBox1.Text = Val(ds3.Tables(0).Rows(ds3.Tables(0).Rows.Count - 1).Item(0)) + 1
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON
        'INSERT QUERRY
        If TextBox2.Text = "" Then
            MsgBox("Enter Country name", MsgBoxStyle.OkOnly, "INVENTORY")
            TextBox2.Focus()
            Exit Sub
        End If

        Try
            Dim dr As DataRow = ds.Tables(0).Rows.Add
            dr.Item(0) = TextBox1.Text
            dr.Item(1) = TextBox2.Text
            da.Update(ds, "COUNTRY")
            addmodd = False
            MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
        Catch ex As Exception
            MsgBox("You can not insert duplicate recored...", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try

        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlClient.SqlDataAdapter("SELECT * FROM COUNTRY ", Login.cn)
        ds1.Clear()
        da1.Fill(ds1, "COUNTRY")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()
    End Sub
End Class



