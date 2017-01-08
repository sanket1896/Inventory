Public Class NEELKANTH_ENTERPRISE

    Private Sub LOGOFFToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LOGOFFToolStripMenuItem.Click
        Dim result As Integer = MessageBox.Show("Are you sure you want to Log off and close application.", "LOGOFF", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If

    End Sub
End Class