Module Load

    Public Sub LoadParamTxt()
        'read text file containing parameters
        Using MyReader As New Microsoft.VisualBasic.
                     FileIO.TextFieldParser(
                       Application.StartupPath & "\param" & ".txt")
            'set delimiter to a comma
            'read everything between commas as seperate field
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            'loop thru all the parameters separated by commas
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    'current parameter being parsed in loop
                    Dim currentField As String
                    'put each word into the array "breakdown()"
                    Dim breakdown() As String
                    'loop thru each word in breakdown()
                    For Each currentField In currentRow
                        'split each word separating by a colon "|"
                        breakdown = Split(currentField, "|")
                        'call sub LoadParam-breakdown(0) is the var name
                        'breakdown(1) is the var value
                        LoadParam(breakdown(0), breakdown(1))
                    Next
                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try
            End While
        End Using
        'update the form after all parameters are loaded
        SeeThruForm.UpdatePref()

    End Sub
    Public Sub LoadParam(x As String, y As String)

        If x = "showName" Then
            showName = y
            savedShowName = y
        End If
        If x = "usePLC" Then
            usePLC = y
            savedUsePLC = y
        End If
        If x = "ipAddress" Then
            ipAddress = y
            savedIpAddress = y
        End If
        If x = "driver" Then
            driver = y
            savedDriver = y
        End If
        If x = "plcAddress" Then
            plcAddress = y
            savedPlcAddress = y
        End If
        If x = "debounceTmr" Then
            debounceTmr = y
            savedDebounceTmr = y
        End If
        If x = "useCam" Then
            useCam = y
            savedUseCam = y
        End If
        SeeThruForm.LoadToolStripMenuItem.Enabled = False
    End Sub

End Module
