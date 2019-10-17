Module Variables

    Public savingFirstPass As Boolean
    Public savingLastPass As Boolean
    Public start As Boolean
    Public showName As Boolean
    Public usePLC As Boolean
    Public ipAddress As String
    Public driver As String
    Public plcAddress As String
    Public savedShowName As Boolean
    Public savedUsePLC As Boolean
    Public savedIpAddress As String
    Public savedDriver As String
    Public savedPlcAddress As String
    Public askToSave As Boolean
    Public debounceTmr As Double
    Public savedDebounceTmr As Double

    Public Sub CheckChanges()

        If savedDebounceTmr <> debounceTmr Or savedIpAddress <> ipAddress Or savedPlcAddress <> plcAddress Or savedDriver <> driver Or savedUsePLC <> usePLC Or savedShowName <> showName Then
            MainForm.SaveToolStripMenuItem1.Enabled = True
            MainForm.LoadToolStripMenuItem.Enabled = True
            askToSave = True
        Else
            MainForm.SaveToolStripMenuItem1.Enabled = False
            MainForm.LoadToolStripMenuItem.Enabled = False
            askToSave = False
        End If


    End Sub

End Module
