Public Class NEELKANTH_ENTERPRISE

    Private Sub LOGOFFToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LOGOFFToolStripMenuItem.Click
        Dim f As New Login
        Dim result As Integer = MessageBox.Show("Are you sure you want to Log off and close application.", "LOGOFF", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Close()
            f.Show()
        End If

    End Sub

    Private Sub CHANGEPASSWORDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHANGEPASSWORDToolStripMenuItem.Click
        Dim myChild As New CHANGE_PASSWORD
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub CREATENEWUSERToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CREATENEWUSERToolStripMenuItem.Click
        Dim myChild As New CREATE_NEW_USER
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub COMPANYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COMPANYToolStripMenuItem.Click
        Dim myChild As New COMPANY
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub COUNTRYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COUNTRYToolStripMenuItem.Click
        Dim myChild As New COUNTRY
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub STATEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STATEToolStripMenuItem.Click
        Dim myChild As New STATE
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub CITYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CITYToolStripMenuItem.Click
        Dim myChild As New CITY
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub SUPPLIERToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SUPPLIERToolStripMenuItem.Click
        Dim myChild As New SUPPLIER
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub CustomerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerToolStripMenuItem.Click
        Dim myChild As New CUSTOMER
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub ProductToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductToolStripMenuItem.Click
        Dim myChild As New PRODUCT
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub CategoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CategoryToolStripMenuItem.Click
        Dim myChild As New CATEGORY
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub UOMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UOMToolStripMenuItem.Click
        Dim myChild As New UOM
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub StatusToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusToolStripMenuItem1.Click
        Dim myChild As New STATUS
        myChild.MdiParent = Me
        myChild.Show()

    End Sub

    Private Sub PurchaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseToolStripMenuItem.Click
        Dim myChild As New PURCHASE
        myChild.MdiParent = Me
        myChild.Show()
    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculatorToolStripMenuItem.Click
        Shell("Calc", AppWinStyle.NormalFocus)
    End Sub

    Private Sub NotepadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotepadToolStripMenuItem.Click
        Shell("Notepad", AppWinStyle.NormalFocus)
    End Sub

    Private Sub ExplorerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExplorerToolStripMenuItem.Click
        Shell("Explorer", AppWinStyle.NormalFocus)
    End Sub

    Private Sub PaidPaymentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaidPaymentToolStripMenuItem1.Click
        Dim myChild As New PAID_PAYMENT
        myChild.MdiParent = Me
        myChild.Show()
    End Sub

    Private Sub ReceivePaymentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceivePaymentToolStripMenuItem1.Click
        Dim myChild As New RECEIVE_PAYMENT
        myChild.MdiParent = Me
        myChild.Show()
    End Sub

    Private Sub ToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem12.Click
        Dim myChild As New PURCHASE_ORDER
        myChild.MdiParent = Me
        myChild.Show()
    End Sub

    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        Dim myChild As New SALES_ORDER
        myChild.MdiParent = Me
        myChild.Show()

    End Sub
End Class