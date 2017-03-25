Imports System.Data.SqlClient
Public Class STATE
    Dim da As New SqlDataAdapter("select * from STATE", Login.cn)
    Dim da1 As New SqlDataAdapter("select * from COUNTRY", Login.cn)
    Dim ds As New DataSet
    Dim rpos As Integer
    Dim addmodd As Boolean
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand

    Sub showdata()
        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        TextBox2.Text = ds.Tables(0).Rows(rpos).Item(1).ToString
        '  TextBox3.Text = ds.Tables(0).Rows(rpos).Item(2).ToString
        Dim abc As String
        Dim i As Integer = ds.Tables(0).Rows(rpos).Item(2).ToString
        cmd.CommandText = "select COUNTRY_NAME from COUNTRY where COUNTRY_ID= " & i & ""
        cmd.Connection = Login.cn
        abc = cmd.ExecuteScalar
        ComboBox1.Text = abc
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON
        'CLEAR ALL TEXTBOXES
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME as [COUNTRY NAME] from STATE P, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "STATE")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()

        TextBox2.Clear()
        TextBox1.Text = Val(ds3.Tables(0).Rows(ds3.Tables(0).Rows.Count - 1).Item(0)) + 1
        TextBox2.ReadOnly = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON
        If TextBox2.Text = "" Then
            MsgBox("Enter STATE name", MsgBoxStyle.OkOnly, "INVENTORY")
            TextBox2.Focus()
            Exit Sub
        End If
        If ComboBox1.Text = "" Then
            MsgBox("Select COUNTRY name", MsgBoxStyle.OkOnly, "INVENTORY")
            Exit Sub
        End If
        cmd.CommandText = "select COUNTRY_ID from COUNTRY where COUNTRY_NAME='" & ComboBox1.SelectedItem & "'"
        cmd.Connection = Login.cn
        Dim str As Integer
        str = cmd.ExecuteScalar

        Try
            Dim dr As DataRow = ds.Tables(0).Rows.Add
            dr.Item(0) = TextBox1.Text
            dr.Item(1) = TextBox2.Text
            dr.Item(2) = str

            da.Update(ds, "STATE")
            addmodd = False
            MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME as [COUNTRY NAME] from STATE p, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID and p.STATE_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "STATE")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        'VALIDATION OF THE COUNTRY NAME
        If e.KeyChar = vbBack Then Exit Sub
        If Char.IsWhiteSpace(e.KeyChar) Then Exit Sub
        If Not (e.KeyChar) Like "[a-z,A-Z]" Then
            e.Handled = True

        End If
    End Sub


    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        'SEARCHING THE STATE
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT STATE_ID, STATE_NAME FROM STATE WHERE STATE_NAME LIKE '" & TextBox3.Text & "%'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "STATE")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'UPDATE BUTTON CLICK
        If IsNumeric(DataGridView1.CurrentRow.Cells(1).Value) Then
            MsgBox("Enetr only character", MsgBoxStyle.OkOnly, "INVENTORY")
        End If
        If IsNumeric(DataGridView1.CurrentRow.Cells(2).Value) Then
            MsgBox("Enetr only numeric", MsgBoxStyle.OkOnly, "INVENTORY")
        End If

        Try
            Dim i As Integer
            cmd.CommandText = "select COUNTRY_ID from COUNTRY where COUNTRY_NAME='" & DataGridView1.CurrentRow.Cells(2).Value & "'"
            cmd.Connection = Login.cn
            i = cmd.ExecuteScalar

            Dim ds2 As New DataSet
            Dim adap As SqlDataAdapter
            cmd.CommandText = "update STATE set STATE_NAME='" & DataGridView1.CurrentRow.Cells(1).Value & "',COUNTRY_ID='" & i & "' where STATE_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
            ds2.Clear()
            adap = New SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME as [COUNTRY NAME] from STATE P, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID", Login.cn)
            adap.Fill(ds2, "STATE")
            DataGridView1.DataSource = ds2.Tables(0)
            ds.Clear()
            da.Fill(ds, "STATE")

        Catch ex As Exception
            MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try

    End Sub

    Private Sub STATE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "STATE")
        da1.Fill(ds, "COUNTRY")
        showdata()

        Dim dr1 As SqlDataReader
        cmd.CommandText = "select COUNTRY_NAME from COUNTRY"
        cmd.Connection = LOGIN.cn
        dr1 = cmd.ExecuteReader
        Do While dr1.Read
            ComboBox1.Items.Add(dr1.GetValue(0).ToString)
        Loop

        dr1.Close()
        ComboBox1.Text = ds.Tables(1).Rows(rpos).Item(1).ToString
        '  Me.WindowState = FormWindowState.Maximized

        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME [COUNTRY NAME] from STATE P, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "STATE")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()

        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME as [COUNTRY NAME] from STATE p, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID and p.STATE_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "STATE")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREVIOUS BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()

            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME as [COUNTRY NAME] from STATE p, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID and p.STATE_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "STATE")
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
            Dim da2 As New SqlClient.SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME as [COUNTRY NAME] from STATE p, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID and p.STATE_ID='" & Val(TextBox1.Text) & "'", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "STATE")
            DataGridView1.DataSource = ds3.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()

        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME as [COUNTRY NAME] from STATE p, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID and p.STATE_ID='" & Val(TextBox1.Text) & "'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "STATE")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'ALL BUTTON CLICK

        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME as [COUNTRY NAME] from STATE P, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "STATE")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Me.Close()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'DELETE BUTTON CLICK
        Try
            Dim b As MsgBoxResult
            b = MsgBox("Are you sure want to Delete Record", MsgBoxStyle.YesNo, "INVENTORY")
            If b = MsgBoxResult.Yes Then
                Dim ds2 As New DataSet
                Dim adap As SqlDataAdapter
                cmd.CommandText = "delete from STATE where STATE_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record Sucessfully Deleted", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("select p.STATE_ID as [STATE ID],p.STATE_NAME as [STATE NAME],p1.COUNTRY_NAME as [COUNTRY NAME] from STATE P, COUNTRY p1 where p.COUNTRY_ID=p1.COUNTRY_ID", Login.cn)
                adap.Fill(ds2, "STATE")
                DataGridView1.DataSource = ds2.Tables(0)
                DataGridView1.Refresh()
                ds.Clear()
                da.Fill(ds, "STATE")
            End If


        Catch ex As Exception
            MsgBox("Record Can Not be Deleted.. It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try
    End Sub

    Private Sub TextBox2_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[a-z,A-Z]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub
End Class