Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class SUPPLIER
    Dim da As New SqlDataAdapter("select * from SUPPLIER", Login.cn)
    Dim da1 As New SqlDataAdapter("select * from CITY", Login.cn)
    Dim da2 As New SqlDataAdapter("select * from STATE", Login.cn)
    Dim ds As New DataSet
    Dim rpos As Integer
    Dim addmodd As Boolean
    Dim cmdb As New SqlCommandBuilder(da)
    Dim cmd As New SqlCommand
    Dim cityname As String
    Dim company_name As String
    Private Sub SUPPLIER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da.Fill(ds, "SUPPLIER")
        da1.Fill(ds, "CITY")
        da2.Fill(ds, "STATE")

        showdata()

        Dim dr As SqlDataReader
        cmd.CommandText = "select City_name from CITY"
        cmd.Connection = Login.cn
        dr = cmd.ExecuteReader

        Do While dr.Read
            ComboBox1.Items.Add(dr.GetValue(0).ToString)
        Loop
        dr.Close()
        Dim ds1 As New System.Data.DataSet
        Dim da11 As New SqlClient.SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_NUMBER as [Mobile],p.Email_ID as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name] from SUPPLIER p,CITY p2 where p.City_id=p2.City_id", Login.cn)
        ds1.Clear()
        da11.Fill(ds1, "SUPPLIER")

        DataGridView1.DataSource = ds1.Tables(0)
        'Me.WindowState = FormWindowState.Maximized
        cmd.CommandText = "select p.Company_name from COMPANY_MASTER p, SUPPLIER p1 where p1.Supplier_id='" & Val(TextBox1.Text) & "' and p.Company_id=p1.Company_id"
        cmd.Connection = Login.cn

        company_name = cmd.ExecuteScalar
        TextBox3.Text = company_name
        RadioButton1.Checked = True
    End Sub

    Sub showdata()

        TextBox1.Text = ds.Tables(0).Rows(rpos).Item(0).ToString
        TextBox2.Text = ds.Tables(0).Rows(rpos).Item(1).ToString

        TextBox4.Text = ds.Tables(0).Rows(rpos).Item(3).ToString

        TextBox5.Text = ds.Tables(0).Rows(rpos).Item(4).ToString
        TextBox6.Text = ds.Tables(0).Rows(rpos).Item(10).ToString
        TextBox7.Text = ds.Tables(0).Rows(rpos).Item(9).ToString
        TextBox8.Text = ds.Tables(0).Rows(rpos).Item(8).ToString
        TextBox9.Text = ds.Tables(0).Rows(rpos).Item(7).ToString
        TextBox10.Text = ds.Tables(0).Rows(rpos).Item(6).ToString

        cmd.CommandText = "select p.CITY_NAME from CITY p, SUPPLIER p1 where p1.SUPPLIER_ID=" & Val(TextBox1.Text) & " and p.CITY_ID=p1.CITY_ID"
        cmd.Connection = Login.cn
        cityname = cmd.ExecuteScalar()
        ComboBox1.DisplayMember = cityname
        ComboBox1.SelectedValue = cityname
        ComboBox1.Text = cityname

        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'FIRST BUTTON CLICK
        rpos = 0
        showdata()

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_Number as [Mobile],p.Email_Id as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name]from SUPPLIER p,CITY p2 where p.City_id=p2.City_id and p.Supplier_id='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "SUPPLIER")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'PREVIOUS BUTTON CLICK
        If rpos > 0 Then
            rpos = rpos - 1
            showdata()

            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_Number as [Mobile],p.Email_Id as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name]from SUPPLIER p,CITY p2 where p.City_id=p2.City_id and p.Supplier_id='" & Val(TextBox1.Text) & "'", Login.cn)
            DA11.Fill(ds4, "SUPPLIER")
            DataGridView1.DataSource = ds4.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'NEXT BUTTON CLICK

        If rpos < ds.Tables(0).Rows.Count - 1 Then
            rpos += 1
            showdata()

            Dim ds4 As New DataSet
            ds4.Clear()
            Dim DA11 As New SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_Number as [Mobile],p.Email_Id as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name]from SUPPLIER p,CITY p2 where p.City_id=p2.City_id and p.Supplier_id='" & Val(TextBox1.Text) & "'", Login.cn)
            DA11.Fill(ds4, "SUPPLIER")
            DataGridView1.DataSource = ds4.Tables(0)
            DataGridView1.Refresh()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        'LAST BUTTON CLICK
        rpos = ds.Tables(0).Rows.Count - 1
        showdata()

        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_Number as [Mobile],p.Email_Id as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name]from SUPPLIER p,CITY p2 where p.City_id=p2.City_id and p.Supplier_id='" & Val(TextBox1.Text) & "'", Login.cn)
        DA11.Fill(ds4, "SUPPLIER")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'INSERT BUTTON CLICK
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_NUMBER as [Mobile],p.Email_ID as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name] from SUPPLIER p,CITY p2 where p.City_id=p2.City_id", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "SUPPLIER")
        DataGridView1.DataSource = ds3.Tables(0)
        DataGridView1.Refresh()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        TextBox1.Text = Val(ds3.Tables(0).Rows(ds3.Tables(0).Rows.Count - 1).Item(0)) + 1
        TextBox3.Text = company_name
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'SAVE BUTTON CLICK
        'INSERT QUERRY

        If TextBox1.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox1.Focus()
            Exit Sub
        End If
        If TextBox2.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox2.Focus()
            Exit Sub
        End If


        If TextBox4.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox4.Focus()
            Exit Sub
        End If

        If TextBox5.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox5.Focus()
            Exit Sub
        End If

        If TextBox6.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox6.Focus()
            Exit Sub
        End If

        If TextBox7.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox7.Focus()
            Exit Sub
        End If
        If TextBox8.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox8.Focus()
            Exit Sub
        End If

        If TextBox9.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox9.Focus()
            Exit Sub
        End If

        If TextBox10.Text.Trim = "" Then
            MsgBox("Enter Valid value", MsgBoxStyle.Exclamation)
            TextBox10.Focus()
            Exit Sub
        End If

        If ComboBox1.Text.Trim = "" Then
            MsgBox("Select Valid value", MsgBoxStyle.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        End If
        If Not ComboBox1.Text.Trim = "" Then
            Dim i As String
            i = ComboBox1.Items.IndexOf(ComboBox1.Text)
            If i = -1 Then
                MsgBox("Select currect City name.")
                Exit Sub
            End If
            ComboBox1.Focus()
        End If




        Try
            cmd.CommandText = "select City_id from CITY where City_name='" & ComboBox1.SelectedItem.ToString & "'"
            cmd.Connection = Login.cn
            Dim str As Integer
            str = cmd.ExecuteScalar

            cmd.CommandText = "select Company_id from COMPANY_MASTER where Company_name='" & TextBox3.Text.ToString & "'"
            cmd.Connection = Login.cn
            Dim CID As Integer
            CID = cmd.ExecuteScalar

            Dim dr As DataRow = ds.Tables(0).Rows.Add
            dr.Item(0) = TextBox1.Text  'SUPPLIER ID
            dr.Item(1) = TextBox2.Text  'SUPPLIER NAME
            dr.Item(2) = CID            'COMPANY ID
            dr.Item(3) = TextBox4.Text  'ADDRESS
            dr.Item(4) = TextBox5.Text  'AREA
            dr.Item(5) = str            'CITY ID
            dr.Item(6) = TextBox10.Text 'PINCODE
            dr.Item(7) = TextBox9.Text  'TIN NO
            dr.Item(8) = TextBox8.Text  'PHONE NUMBER
            dr.Item(9) = TextBox7.Text  'MOBILE NUMBER
            dr.Item(10) = TextBox6.Text 'EMAIL ID

            da.Update(ds, "SUPPLIER")
            addmodd = False
            MsgBox("Record sucessfully inserted", MsgBoxStyle.OkOnly, "INVENTORY")
        Catch ex As Exception
            MsgBox(ex.Message)

            MsgBox("you cna not insert a duplicate record...", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try

        Dim ds1 As New System.Data.DataSet
        DataGridView1.Refresh()
        Dim da11 As New SqlClient.SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_NUMBER as [Mobile],p.Email_ID as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name] from SUPPLIER p,CITY p2 where p.City_id=p2.City_id", Login.cn)
        ds1.Clear()
        da11.Fill(ds1, "SUPPLIER")
        DataGridView1.DataSource = ds1.Tables(0)
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

        If RadioButton1.Checked = True Then
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_NUMBER as [Mobile],p.Email_ID as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name] from SUPPLIER p,CITY p2 where p2.City_name like '" & TextBox11.Text & "%' and p.City_id=p2.City_id", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "SUPPLIER")
            DataGridView1.DataSource = ds3.Tables(0)
            DataGridView1.Refresh()
        End If
        If RadioButton2.Checked = True Then
            Dim ds3 As New DataSet
            Dim da2 As New SqlClient.SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_NUMBER as [Mobile],p.Email_ID as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name]  from SUPPLIER p,CITY p2 where p.Area like '" & TextBox11.Text & "%' and p.City_id=p2.City_id", Login.cn)
            ds3.Clear()
            da2.Fill(ds3, "SUPPLIER")
            DataGridView1.DataSource = ds3.Tables(0)
            DataGridView1.Refresh()
        End If

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'ALL  BUTTON CLICK
        Dim ds4 As New DataSet
        ds4.Clear()
        Dim DA11 As New SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_NUMBER as [Mobile],p.Email_ID as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name] from SUPPLIER p,CITY p2 where p.City_id=p2.City_id", Login.cn)
        DA11.Fill(ds4, "SUPPLIER")
        DataGridView1.DataSource = ds4.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        'UPDATE BUTTON CLICK
        Dim i As Integer
        Try

            cmd.CommandText = "select City_id from CITY where City_name='" & DataGridView1.CurrentRow.Cells(9).Value & "'"
            cmd.Connection = Login.cn
            i = cmd.ExecuteScalar

            Dim ds2 As New DataSet
            Dim adap As SqlDataAdapter
            'MsgBox(i)
            cmd.CommandText = "update SUPPLIER set Supplier_name='" & DataGridView1.CurrentRow.Cells(1).Value & "',Address='" & DataGridView1.CurrentRow.Cells(2).Value & "',Area='" & DataGridView1.CurrentRow.Cells(3).Value & "',Mobile_NUMBER='" & DataGridView1.CurrentRow.Cells(4).Value & "',Email_ID='" & DataGridView1.CurrentRow.Cells(5).Value & "',City_id='" & i & "',PINCODE='" & DataGridView1.CurrentRow.Cells(7).Value & "', TIN_NO='" & DataGridView1.CurrentRow.Cells(8).Value & "', PHONE_NUMBER='" & DataGridView1.CurrentRow.Cells(6).Value & "' where Supplier_id='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
            cmd.Connection = Login.cn
            cmd.ExecuteNonQuery()
            MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
            ds2.Clear()
            adap = New SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_NUMBER as [Mobile],p.Email_ID as [Email],P.PHONE_NUMBER AS [PHONE NUMBER],P.PINCODE AS [PINCODE],P.TIN_NO AS [TIN NUMBER], p2.City_name as [City name] from SUPPLIER p,CITY p2 where p.City_id=p2.City_id", Login.cn)
            adap.Fill(ds2, "SUPPLIER")
            DataGridView1.DataSource = ds2.Tables(0)
            ds.Clear()
            da.Fill(ds, "SUPPLIER")
        Catch ex As Exception
            '    MsgBox(ex.Message)
            If i = 0 Then
                MsgBox("Enter Valid City Name")
            End If
            MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        'CLOSE BUTTON CLICK
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

                cmd.CommandText = "delete from SUPPLIER where Supplier_id='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record Sucessfully Deleted", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("select p.Supplier_id as [Supplier id],p.Supplier_name as [Supplier name],p.Address as [Address],p.Area as [Area] ,p.Mobile_NUMBER as [Mobile],p.Email_ID as [Email],p2.City_name as [City name] from SUPPLIER p,CITY p2 where p.City_id=p2.City_id", Login.cn)
                adap.Fill(ds2, "SUPPLIER")
                DataGridView1.DataSource = ds2.Tables(0)
                DataGridView1.Refresh()
                ds.Clear()
                da.Fill(ds, "SUPPLIER")
            End If

            ' showdata()
        Catch ex As Exception
            MsgBox("Record Can Not be Deleted.. It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
        End Try

    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress, TextBox8.KeyPress, TextBox9.KeyPress, TextBox10.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[0-9]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox6_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.Leave
        Dim pattern As String = "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$"


        Dim match As System.Text.RegularExpressions.Match = Regex.Match(TextBox6.Text.Trim(), pattern, RegexOptions.IgnoreCase)
        If (match.Success) Then
            'MsgBox("done")
        Else
            MsgBox("Please enter valid mail id", MsgBoxStyle.Exclamation)
            ' TextBox9.Clear()
            TextBox6.Focus()

        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress
        If e.KeyChar = vbBack Then Exit Sub 'BackSpace

        If Not (e.KeyChar) Like "[a-z,A-Z]" Then  'not 0-9 then ignore
            e.Handled = True
        End If
    End Sub
End Class