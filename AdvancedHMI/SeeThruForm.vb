'*******************************************************************************

'   *******     *       *      ******     *     *         *****      *        *
'   *           **     **     *           *     *        *     *     *        *
'   *           * *   * *     *           *     *        *           *        *  
'   ****        *  * *  *     *           *     *   **   *            *      *
'   *           *   *   *     *  ****     *     *        *             *    *
'   *           *       *     *     *     *     *        *     *        *  *   
'   *******     *       *      ******      *****          *****           *

'NOTE THIS IS DIFFERENT FOR THIS VERSION ADVANCEDHMI VS PREVIOUS VERSIONS..
'YOUR PROJECT REFERENCE FOLDER IS LOCATED IN THE SOLUTION EXPLORER TO THE RIGHT -->UNDER VB SeeThru
'
'From your project, right click On "References" And Select "Manager Nuget Packages..." Option. It will open up nuget package manager. In package source, make sure that "nuget.org" Is selected.
'(If you are Using the commercial release, please check the instruction In the commercial download area For instructions To setup the commercial release nuget repository.)
'Under "Browse", enter the search text "emgu cv" And you should be able to find the Emgu.CV nuget pacakge.
'Please make sure the package Is created by "Emgu Corporation" For the official release.
'Click the "Install" button. Nuget will download Emgu CV And configure the project For you.

'**IMPORTS**
'Imports Emgu.CV
'Imports Emgu.CV.Structure
'**DECLARATIONS**
'Public capturez As VideoCapture = New VideoCapture(0) '0 is index where your built in webcam is located

Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports Emgu.CV
Imports Emgu.CV.Structure

Public Class SeeThruForm
    Public capturez As VideoCapture = New VideoCapture(1)
    '*******************************************************************************
    '* Stop polling when the form is not visible in order to reduce communications
    '* Copy this section of code to every new form created
    '*******************************************************************************
    Private NotFirstShow As Boolean

    Private Sub Form_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        '* Do not start comms on first show in case it was set to disable in design mode
        If NotFirstShow Then
            AdvancedHMIDrivers.Utilities.StopComsOnHidden(components, Me)
        Else
            NotFirstShow = True
        End If
    End Sub

    '***************************************************************
    '* .NET does not close hidden forms, so do it here
    '* to make sure forms are disposed and drivers close
    '***************************************************************
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim index As Integer
        While index < My.Application.OpenForms.Count
            If My.Application.OpenForms(index) IsNot Me Then
                My.Application.OpenForms(index).Close()
            End If
            index += 1
        End While
    End Sub


    Private Sub Run()

        Dim a As String = Now.ToShortDateString & Now.ToLongTimeString
        a = a.Replace(":", "").Replace("/", "").Replace("\", "")
        If TextBox1.Text = "" Then
            TextBox1.Text = a
        End If
        If showName = True Then
            ShowNameInFrame()
        Else
            NoNameNoFrame()
        End If
        TextBox1.Text = ""

    End Sub
    Private Sub NoNameNoFrame()

        Me.ShowIcon = False
        MenuStrip1.Visible = False
        TextBox1.Visible = False
        BtnSaveStart.Visible = False
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.ControlBox = False
        Me.Text = ""
        If useCam = True Then
            GrabCamFrame()
        Else
            GrabFrame()
        End If
        TextBox1.Visible = True
        BtnSaveStart.Visible = True
        Me.ControlBox = True
        Me.Text = "SeeThru"
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.ShowIcon = True
        MenuStrip1.Visible = True
        TextBox1.Select()

    End Sub
    Private Sub ShowNameInFrame()

        Me.Text = TextBox1.Text
        Me.ShowIcon = False
        MenuStrip1.Visible = False
        TextBox1.Visible = False
        BtnSaveStart.Visible = False
        Me.ControlBox = False
        If useCam = True Then
            GrabCamFrame()
        Else
            GrabFrame()
        End If
        TextBox1.Visible = True
        BtnSaveStart.Visible = True
        Me.ControlBox = True
        Me.Text = "SeeThru"
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.ShowIcon = True
        MenuStrip1.Visible = True
        TextBox1.Select()

    End Sub
    Private Sub GrabFrame()

        Dim area As Rectangle
        Dim capture As System.Drawing.Bitmap
        Dim graph As Graphics
        Dim fileName As String = TextBox1.Text & ".jpg"
        Dim path As String = My.Computer.FileSystem.SpecialDirectories.MyPictures & "\SeeThru\"
        Dim file As String = path & fileName
        area = Me.Bounds
        capture = New System.Drawing.Bitmap(Bounds.Width, Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        graph = Graphics.FromImage(capture)
        graph.CopyFromScreen(area.X, area.Y, 0, 0, area.Size, CopyPixelOperation.SourceCopy)
        If My.Computer.FileSystem.DirectoryExists(path) Then
            Delay(0.1)
            Try
                capture.Save(file, ImageFormat.Jpeg)
                TextBox1.Text = ""
            Catch ex As Exception
                MessageBox.Show("Error: Saving Image Failed -->" & ex.Message.ToString() & vbNewLine & "Check path exits--> " & file)
            End Try
        Else
            Dim response = MsgBox("Folder not found--> " & path & vbNewLine & vbNewLine & "Create it now?", MsgBoxStyle.OkCancel, "SeeThru")
            If response = MsgBoxResult.Ok Then
                My.Computer.FileSystem.CreateDirectory(path)
                Try
                    capture.Save(file, ImageFormat.Jpeg)
                    TextBox1.Text = ""
                Catch ex As Exception
                    MessageBox.Show("Error: Saving Image Failed -->" & ex.Message.ToString() & vbNewLine & "Check path exits--> " & file)
                End Try
            End If
        End If

    End Sub
    Private Sub GrabCamFrame()
        Dim fileName As String = TextBox1.Text & ".jpg"
        Dim path As String = My.Computer.FileSystem.SpecialDirectories.MyPictures & "\SeeThru\"

        Timer1.Enabled = False
        Try
            Dim bitmap As New Bitmap(PictureBox1.Image)
            PictureBox1.Image.Save(path & fileName)
        Catch ex As Exception
            MessageBox.Show(String.Format("Error: {0}", ex.Message) & vbNewLine & "Set to stream from cam. No cam detected." & vbNewLine & "Change settings at Edit->Preferences->Stream Image From Cam")
        End Try

        Timer1.Enabled = True



    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        start = False
        Me.FormBorderStyle = FormBorderStyle.Sizable
        LoadParamTxt()
        If usePLC = True Then
            BtnSaveStart.Text = "Start"
        Else
            BtnSaveStart.Text = "Save"
        End If

    End Sub

    Private Sub DisplayNameOnBorderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayNameOnBorderToolStripMenuItem.Click

        If DisplayNameOnBorderToolStripMenuItem.Checked = True Then
            DisplayNameOnBorderToolStripMenuItem.Checked = False
            showName = False
        Else
            DisplayNameOnBorderToolStripMenuItem.Checked = True
            showName = True
        End If

        UpdatePref()

    End Sub
    Public Sub UpdatePref()

        If showName = True Then
            DisplayNameOnBorderToolStripMenuItem.Checked = True
        Else
            DisplayNameOnBorderToolStripMenuItem.Checked = False
        End If
        If usePLC = True Then
            UsePLCToolStripMenuItem.Checked = True
            CommunicationToolStripMenuItem.Enabled = True
            PilotLight1.Visible = True
            BtnSaveStart.Text = "Start"
            If driver = "ControlLogix / CompactLogix" Then
                EthernetIPforCLXCom1.IPAddress = ipAddress
                DataSubscriber1.ComComponent = EthernetIPforCLXCom1
                PilotLight1.ComComponent = EthernetIPforCLXCom1
            Else
                EthernetIPforSLCMicroCom1.IPAddress = ipAddress
                DataSubscriber1.ComComponent = EthernetIPforSLCMicroCom1
                PilotLight1.ComComponent = EthernetIPforSLCMicroCom1
            End If
            PilotLight1.PLCAddressValue = plcAddress
            DataSubscriber1.BeginInit()
            DataSubscriber1.PLCAddressValue = New Drivers.PLCAddressItem(plcAddress)
            DataSubscriber1.EndInit()
        Else
            UsePLCToolStripMenuItem.Checked = False
            CommunicationToolStripMenuItem.Enabled = False
            BtnSaveStart.Text = "Save"
            PilotLight1.Visible = False
        End If
        If useCam = True Then
            Timer1.Enabled = True
            PictureBox1.Visible = True
            StreamImageFromCamToolStripMenuItem.Checked = True
        Else
            Timer1.Enabled = False
            PictureBox1.Visible = False
            StreamImageFromCamToolStripMenuItem.Checked = False
        End If
        ToolTip1.SetToolTip(PilotLight1, plcAddress)
        CheckChanges()
    End Sub

    Private Sub SaveToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem1.Click

        SaveStuff()
        LoadParamTxt()

    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click

        LoadParamTxt()

    End Sub

    Private Sub UsePLCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsePLCToolStripMenuItem.Click

        If UsePLCToolStripMenuItem.Checked = True Then
            UsePLCToolStripMenuItem.Checked = False
            usePLC = False
            CommunicationToolStripMenuItem.Enabled = False
        Else
            UsePLCToolStripMenuItem.Checked = True
            usePLC = True
            CommunicationToolStripMenuItem.Enabled = True
            SetIP.Show()
        End If


        UpdatePref()
    End Sub

    Private Sub CommunicationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CommunicationToolStripMenuItem.Click
        SetIP.Show()
    End Sub

    Private Sub DataSubscriber1_DataChanged(sender As Object, e As Drivers.Common.PlcComEventArgs) Handles DataSubscriber1.DataChanged

        Delay(debounceTmr)
        Dim result As Boolean
        Read(e.PlcAddress, result)
        If result = True AndAlso start = True Then
            Run()
        End If

    End Sub
    Public Function Read(ByRef y As String, ByRef x As Boolean)

        If driver = "ControlLogix / CompactLogix" Then
            x = EthernetIPforCLXCom1.Read(y)
        Else
            x = EthernetIPforSLCMicroCom1.Read(y)
        End If
        Return x

    End Function

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click

        MessageBox.Show(
    "SeeThru developed by Luke Biller" & vbNewLine & "Using AdvancedHMI" & vbNewLine &
    "Press 'Help' to learn more about" & vbNewLine & "AdvancedHMI or for licensing",
    "About",
    MessageBoxButtons.OK,
    MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1,
    0, '0 is default otherwise use MessageBoxOptions Enum
    "https://www.advancedhmi.com/")

    End Sub

    Private Sub BtnSaveStart_Click(sender As Object, e As EventArgs) Handles BtnSaveStart.Click

        If usePLC = True Then
            If start = True Then
                start = False
                BtnSaveStart.Text = "Start"
            Else
                start = True
                BtnSaveStart.Text = "Stop"
                Run()
            End If
        Else
            BtnSaveStart.Text = "Save"
            Run()
        End If

    End Sub

    Private Sub MainForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        CheckChanges()
        If askToSave = True Then
            Dim response = MsgBox("Save Changes?", MsgBoxStyle.YesNoCancel, "SeeThru")
            If response = MsgBoxResult.Yes Then
                SaveStuff()
            ElseIf response = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If

    End Sub

    Private Sub StreamImageFromCamToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StreamImageFromCamToolStripMenuItem.Click
        If StreamImageFromCamToolStripMenuItem.Checked = True Then
            StreamImageFromCamToolStripMenuItem.Checked = False
            useCam = False
        Else
            StreamImageFromCamToolStripMenuItem.Checked = True
            useCam = True
        End If
        UpdatePref()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim imagez As Image(Of Bgr, Byte) = capturez.QueryFrame().ToImage(Of Bgr, Byte)
            PictureBox1.Image = imagez.ToBitmap()
        Catch ex As Exception

        End Try
    End Sub
End Class
