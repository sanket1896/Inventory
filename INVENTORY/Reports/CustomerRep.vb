Imports System.Data.SqlClient

Public Class Form1
    Dim cmd As New SqlCommand
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        SqlDataAdapter1.Fill(ReportDataset1, "Customer")
        Dim cr1 As New CustomerReport
        cr1.SetDataSource(ReportDataset1.Tables(0))
        CrystalReportViewer1.ReportSource = cr1



        ComboBox1.Items.Add("All")
        ComboBox2.Items.Add("All")
        ComboBox1.Text = "All"
        ComboBox2.Text = "All"

        Dim dr As SqlDataReader
        cmd.CommandText = "select City_name from CITY"
        cmd.Connection = Login.cn
        dr = cmd.ExecuteReader
        Do While dr.Read
            ComboBox1.Items.Add(dr.GetValue(0).ToString)
        Loop
        dr.Close()

        Dim dr1 As SqlDataReader
        cmd.CommandText = "select DISTINCT(Area) from CUSTOMER"
        cmd.Connection = Login.cn
        dr1 = cmd.ExecuteReader
        Do While dr1.Read
            ComboBox2.Items.Add(dr1.GetValue(0).ToString)
        Loop
        dr1.Close()
    End Sub

   
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT CUSTOMER.Customer_name,CUSTOMER.Address, CITY.City_name,  CUSTOMER.Area,CUSTOMER.TIN_NO, CUSTOMER.Mobile_number,CUSTOMER.Email_id FROM CUSTOMER INNER JOIN  CITY ON CUSTOMER.City_id = CITY.City_id where City_name='" & ComboBox1.SelectedItem.ToString & "'", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "Customer")
        Dim x As New CustomerReport
        x.SetDataSource(ds3.Tables(0))
        CrystalReportViewer1.ReportSource = x

        If ComboBox1.SelectedItem.ToString = "All" Then
            ReportDataset1.Clear()
            SqlDataAdapter1.Fill(ReportDataset1, "Customer")
            Dim x1 As New CustomerReport
            x1.SetDataSource(ReportDataset1.Tables(0))
            CrystalReportViewer1.ReportSource = x1
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim ds4 As New DataSet
        Dim da3 As New SqlClient.SqlDataAdapter("SELECT CUSTOMER.Customer_name,CUSTOMER.Address, CITY.City_name,  CUSTOMER.Area,CUSTOMER.TIN_NO, CUSTOMER.Mobile_number,CUSTOMER.Email_id FROM CUSTOMER INNER JOIN  CITY ON CUSTOMER.City_id = CITY.City_id where Area='" & ComboBox2.SelectedItem.ToString & "'", Login.cn)
        ds4.Clear()
        da3.Fill(ds4, "Customer")
        Dim x As New CustomerReport
        x.SetDataSource(ds4.Tables(0))
        CrystalReportViewer1.ReportSource = x

        If ComboBox2.SelectedItem.ToString = "All" Then
            ReportDataset1.Clear()
            SqlDataAdapter1.Fill(ReportDataset1, "Customer")
            Dim x1 As New CustomerReport
            x1.SetDataSource(ReportDataset1.Tables(0))
            CrystalReportViewer1.ReportSource = x1
        End If
    End Sub
End Class