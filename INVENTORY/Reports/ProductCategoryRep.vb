Imports System.Data.SqlClient
Public Class ProductCategoryRep
    Dim cmd As New SqlCommand
    Private Sub ProductCategoryReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        SqlDataAdapter1.Fill(ReportDataset1, "PRODUCTCATEGORY")
        Dim x As New ProductCategoryReport
        x.SetDataSource(ReportDataset1.Tables("PRODUCTCATEGORY"))
        CrystalReportViewer1.ReportSource = x
        ComboBox1.Items.Add("All")
        ComboBox1.Text = "All"
        Dim dr As SqlDataReader
        cmd.CommandText = "select Product_name from PRODUCT"
        cmd.Connection = Login.cn
        dr = cmd.ExecuteReader
        Do While dr.Read
            ComboBox1.Items.Add(dr.GetValue(0).ToString)
        Loop
        dr.Close()
        ComboBox2.Items.Add("All")
        ComboBox2.Text = "All"

        Dim dr1 As SqlDataReader
        cmd.CommandText = "select Category_name from CATEGORY"
        cmd.Connection = Login.cn
        dr1 = cmd.ExecuteReader
        Do While dr1.Read
            ComboBox2.Items.Add(dr1.GetValue(0).ToString)
        Loop
        dr1.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'product query
        Dim productQ As String
        productQ = ComboBox1.Text
        If ComboBox1.Text = "All" Then
            productQ = "%"
        End If

        'category query
        Dim categoryQ As String
        categoryQ = ComboBox2.Text
        If ComboBox2.Text = "All" Then
            categoryQ = "%"
        End If


        Dim ds3 As New DataSet
        Dim da2 As New SqlClient.SqlDataAdapter("SELECT CATEGORY.CATEGORY_NAME, PRODUCT.PRODUCT_NAME, PRODUCT.QOH, PRODUCT.VAT, PRODUCT.ADITIONAL_VAT, PRODUCT.PRICE FROM CATEGORY INNER JOIN PRODUCT ON CATEGORY.CATEGORY_ID = PRODUCT.CATEGORY_ID WHERE (CATEGORY.CATEGORY_NAME LIKE '" & categoryQ & "') AND (PRODUCT.PRODUCT_NAME LIKE '" & productQ & "')", Login.cn)
        ds3.Clear()
        da2.Fill(ds3, "PRODUCTCATEGORY")
        Dim x As New ProductCategoryReport
        x.SetDataSource(ds3.Tables("PRODUCTCATEGORY"))
        CrystalReportViewer1.ReportSource = x


       
    End Sub
End Class