Public Class PLCAddressItemListEditForm
#Region "Constructor"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_Items = New System.Collections.ObjectModel.ObservableCollection(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItemEx)

    End Sub

    Public Sub New(items As System.Collections.ObjectModel.ObservableCollection(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItemEx))
        Me.New()

        'For Each it In items
        '    m_Items.Add(it.Clone())
        'Next
        m_Items = items

        RefreshList()
    End Sub
#End Region


#Region "Properties"
    Private m_ComComponent As MfgControl.AdvancedHMI.Drivers.IComComponent
    Public Property ComComponent As MfgControl.AdvancedHMI.Drivers.IComComponent
        Get
            Return m_ComComponent
        End Get
        Set(value As MfgControl.AdvancedHMI.Drivers.IComComponent)
            m_ComComponent = value
        End Set
    End Property

    Private m_Items As System.Collections.ObjectModel.ObservableCollection(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItemEx)
    Public ReadOnly Property Items As System.Collections.ObjectModel.ObservableCollection(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItemEx)
        Get
            Return m_Items
        End Get
        'Set(value As System.Collections.ObjectModel.ObservableCollection(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItem))
        '    m_Items = value
        'End Set
    End Property
#End Region

    Private Sub RefreshList()
        ListBox1.Items.Clear()
        For Each it In m_Items
            ListBox1.Items.Add(it)
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        Dim pi As New MfgControl.AdvancedHMI.Drivers.PLCAddressItemEx

        Dim index As Integer = 0
        Dim index2 As Integer
        Dim prefix As String = "PLCAddressItem"
        Dim Name As String = ""
        For Each it In m_Items
            If Not String.IsNullOrEmpty(it.Name) AndAlso it.Name.IndexOf(prefix, 0, it.Name.Length, StringComparison.InvariantCultureIgnoreCase) = 0 Then
                Dim v As Integer = it.Name.Substring(prefix.Length)
                If Integer.TryParse(it.Name.Substring(prefix.Length), index2) Then
                    'If (Integer.TryParse(it.Name, index2)) Then
                    index = Math.Max(index2 + 1, index) '  pi.Name = prefix & CStr(index + 1)
                    'End If
                End If
            End If
        Next
        If String.IsNullOrEmpty(pi.Name) Then
            pi.Name = prefix & CStr(index)
        End If

        pi.ComComponent = m_ComComponent

        m_Items.Add(pi)
        RefreshList()

        ListBox1.SelectedIndex = m_Items.Count - 1

        If ListBox1.SelectedIndex < 0 Then
            ListBox1.SelectedIndex = 0
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex >= 0 Then
            PropertyGrid1.SelectedObject = m_Items(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        If ListBox1.SelectedIndex >= 0 Then
            m_Items.RemoveAt(ListBox1.SelectedIndex)
            RefreshList()
            If ListBox1.Items.Count > 0 Then ListBox1.SelectedIndex = 0
        End If
    End Sub


End Class