Imports System.Data.SqlClient
Imports System.Globalization

Public Class PURCHASE_ORDER_SELECTION
    Dim ds As New System.Data.DataSet
    Dim cmd As New SqlCommand
    Private Sub PURCHASE_ORDER_SELECTION_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim cmd As New SqlClient.SqlCommand
        cmd.CommandText = "select SUPPLIER_NAME from SUPPLIER"
        cmd.Connection = Login.cn

        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlClient.SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Ordre id],p.PURCHASE_ORDER_DATE as [Purchase Order date],p1.SUPPLIER_NAME as [Supplier name] from PURCHASE_ORDER_MASTER p,SUPPLIER p1 where  p.SUPPLIER_ID=p1.SUPPLIER_ID", Login.cn)
        ds1.Clear()
        da1.Fill(ds1, "PURCHASE_ORDER_MASTER")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        DataGridView1.DataBindings.Clear()
        Dim ds1 As New System.Data.DataSet
        Dim da1 As New SqlClient.SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p.PURCHASE_ORDER_DATE as [Purchase Order date],p1.SUPPLIER_NAME as [Supplier name] from PURCHASE_ORDER_MASTER p,SUPPLIER p1 where  SUPPLIER_NAME like '" & TextBox1.Text & "%' and p.SUPPLIER_ID=p1.SUPPLIER_ID", Login.cn)
        ds1.Clear()
        da1.Fill(ds1, "PURCHASE_ORDER_MASTER")
        DataGridView1.DataSource = ds1.Tables(0)
        DataGridView1.Refresh()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim dt As DateTime
        dt = DataGridView1.CurrentRow.Cells(1).Value.ToString

        Dim newdate As DateTime = dt.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)

        ''''''''''''''''''''''''''''''''date error 

        Dim SUP_ID As Integer
        Dim dte As String
        dte = DataGridView1.CurrentRow.Cells(1).Value
        Dim odte As String
        cmd.CommandText = "SELECT PURCHASE_ORDER_DATE FROM PURCHASE_ORDER_MASTER WHERE PURCHASE_ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
        cmd.Connection = Login.cn
        odte = cmd.ExecuteScalar
        MsgBox(odte)
        MsgBox(dte)
        If odte = dte Then
            Try
                cmd.CommandText = "select SUPPLIER_ID from SUPPLIER where SUPPLIER_NAME='" & DataGridView1.CurrentRow.Cells(2).Value & "'"
                cmd.Connection = Login.cn
                SUP_ID = cmd.ExecuteScalar
                MsgBox(SUP_ID)
                Dim ds2 As New DataSet
                Dim adap As SqlDataAdapter
                cmd.CommandText = "UPDATE PURCHASE_ORDER_MASTER SET SUPPLIER_ID=" & SUP_ID & " WHERE PURCHASE_ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p.PURCHASE_ORDER_DATE as [Purchase Order date],p1.SUPPLIER_NAME as [Supplier name] from PURCHASE_ORDER_MASTER p,SUPPLIER p1 where p.SUPPLIER_ID=p1.SUPPLIER_ID", Login.cn)
                adap.Fill(ds2, "PURCHASE_ORDER_MASTER")
                DataGridView1.DataSource = ds2.Tables(0)
            Catch ex As Exception
                If SUP_ID = 0 Then
                    MsgBox("ENTER APPROPRIATE NAME OF THE SUPPLIER", MsgBoxStyle.OkOnly, "INVENTORY")
                Else
                    MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
                End If

            End Try
        Else
            Try
                cmd.CommandText = "select SUPPLIER_ID from SUPPLIER where SUPPLIER_NAME='" & DataGridView1.CurrentRow.Cells(2).Value & "'"
                cmd.Connection = Login.cn
                SUP_ID = cmd.ExecuteScalar
                MsgBox(SUP_ID)
                Dim ds2 As New DataSet
                Dim adap As SqlDataAdapter
                cmd.CommandText = "UPDATE PURCHASE_ORDER_MASTER SET SUPPLIER_ID=" & SUP_ID & ",PURCHASE_ORDER_DATE='" & newdate & "' WHERE PURCHASE_ORDER_ID='" & DataGridView1.CurrentRow.Cells(0).Value & "'"
                cmd.Connection = Login.cn
                cmd.ExecuteNonQuery()
                MsgBox("Record sucessfully Updated", MsgBoxStyle.OkOnly, "INVENTORY")
                ds2.Clear()
                adap = New SqlDataAdapter("select p.PURCHASE_ORDER_ID as [Purchase Order id],p.PURCHASE_ORDER_DATE as [Purchase Order date],p1.SUPPLIER_NAME as [Supplier name] from PURCHASE_ORDER_MASTER p,SUPPLIER p1 where p.SUPPLIER_ID=p1.SUPPLIER_ID", Login.cn)
                adap.Fill(ds2, "PURCHASE_ORDER_MASTER")
                DataGridView1.DataSource = ds2.Tables(0)
            Catch ex As Exception
                If SUP_ID = 0 Then
                    MsgBox("ENTER APPROPRIATE NAME OF THE SUPPLIER", MsgBoxStyle.OkOnly, "INVENTORY")
                Else
                    MsgBox("Record Can Not be Updated... It Has Reference Records ", MsgBoxStyle.OkOnly, "INVENTORY")
                End If
            End Try
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'CLOSE BUTTON
        PURCHASE_ORDER.Show()
        Me.Close()
    End Sub
End Class