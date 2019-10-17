Option Strict On
Option Explicit On

Imports System.IO
Imports System.Collections
'*************************************************************************************
'* Adapted from http://bytes.com/topic/net/insights/797169-reading-parsing-ini-file-c
'*************************************************************************************
Public Class IniParser
    Private keyPairs As New Dictionary(Of SectionPair, String)
    'Private FilePath As String

    Private Structure SectionPair
        Public Section As String
        Public SettingName As String
    End Structure

    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Opens the INI file at the given path and enumerates the values in the IniParser.
    ''' </summary>
    ''' <param name="iniPath">Full path to INI file.</param>
    Public Sub New(iniPath As String)
        Me.New()

        FilePath = iniPath

        If String.IsNullOrEmpty(iniPath) Then Exit Sub

        'Dim iniFile As TextReader = Nothing
        Dim strLine As String
        Dim currentRoot As String = Nothing
        Dim keyPair As String()

        Dim CurrentLineNumber As Integer

        If File.Exists(iniPath) Then
            'Try
            Using iniFile As New StreamReader(iniPath)

                strLine = iniFile.ReadLine()
                CurrentLineNumber += 1

                While strLine IsNot Nothing
                    strLine = strLine.Trim()  '.ToUpper()

                    If Not String.IsNullOrEmpty(strLine) Then
                        If strLine.StartsWith("[", StringComparison.OrdinalIgnoreCase) AndAlso
                                strLine.EndsWith("]", StringComparison.OrdinalIgnoreCase) Then
                            currentRoot = strLine.Substring(1, strLine.Length - 2).ToUpper(Globalization.CultureInfo.CurrentCulture)
                        Else
                            keyPair = strLine.Split(New Char() {"="c}, 2)

                            Dim sectionPair As New SectionPair
                            Dim value As String = Nothing

                            If currentRoot Is Nothing Then
                                currentRoot = "ROOT"
                            End If

                            sectionPair.Section = currentRoot
                            sectionPair.SettingName = keyPair(0).ToUpper(Globalization.CultureInfo.CurrentCulture)

                            If keyPair.Length > 1 Then
                                value = keyPair(1)
                            End If

                            Try
                                keyPairs.Add(sectionPair, value)
                            Catch ex As ArgumentException
                                '* Duplicate key exists
                                Throw New ArgumentException("Section " & sectionPair.Section & " and " & sectionPair.SettingName & " already exists. Duplicate at line " & CurrentLineNumber & " in file " & iniPath)
                            End Try
                        End If
                    End If

                    strLine = iniFile.ReadLine()
                    CurrentLineNumber += 1

                End While
            End Using
            'Catch ex As Exception
            '    Throw 'ex
            '    'Finally
            '    '    If iniFile IsNot Nothing Then
            '    '        iniFile.Close()
            '    '    End If
            'End Try
        Else
            Throw New FileNotFoundException("Unable to locate " & iniPath)
        End If
    End Sub


    Private m_FilePath As String
    Public Property FilePath As String
        Get
            Return m_FilePath
        End Get
        Set(value As String)
            m_FilePath = value
        End Set
    End Property



    ''' <summary>
    ''' Returns the value for the given section, key pair.
    ''' </summary>
    ''' <param name="sectionName">Section name.</param>
    Public Function GetSetting(sectionName As String, settingName As String) As String
        Dim sectionPair As SectionPair

        If sectionName Is Nothing Then sectionName = ""
        If String.IsNullOrEmpty(settingName) Then
            Throw New ArgumentException("Setting name cannot be null.")
        End If

        sectionPair.Section = sectionName.ToUpper(Globalization.CultureInfo.CurrentCulture)
        sectionPair.SettingName = settingName.ToUpper(Globalization.CultureInfo.CurrentCulture)


        'Return DirectCast(keyPairs(sectionPair), String)
        Return keyPairs(sectionPair)
    End Function

    'Public Function SectionExists(sectionName As String) As Boolean
    '    Return keyPairs.ContainsKey(sectionName.ToUpper)
    'End Function

    Public Function ListSettings(sectionName As String) As String()
        If sectionName Is Nothing Then sectionName = ""
        Dim tmpArray As New List(Of String)

        For Each pair As SectionPair In keyPairs.Keys
            If pair.Section = sectionName.ToUpper(Globalization.CultureInfo.CurrentCulture) Then
                tmpArray.Add(pair.SettingName)
            End If
        Next

        Return tmpArray.ToArray()
    End Function

    Public Function ListSections() As String()
        Dim TmpArray As New List(Of String)

        For Each pair As SectionPair In keyPairs.Keys
            If Not TmpArray.Contains(pair.Section) Then
                TmpArray.Add(pair.Section)
            End If
        Next

        Return TmpArray.ToArray()
    End Function

    ''' <summary>
    ''' Adds or replaces a setting to the table to be saved.
    ''' </summary>
    ''' <param name="sectionName">Section to add under.</param>
    ''' <param name="settingName">Key name to add.</param>
    ''' <param name="settingValue">Value of key.</param>
    Public Sub AddSetting(sectionName As String, settingName As String, settingValue As String)
        If String.IsNullOrEmpty(settingName) Then
            Throw New System.ArgumentNullException("settingName")
        End If

        If sectionName Is Nothing Then sectionName = ""

        Dim sectionPair As SectionPair
        sectionPair.Section = sectionName.ToUpper(Globalization.CultureInfo.CurrentCulture)
        sectionPair.SettingName = settingName.ToUpper(Globalization.CultureInfo.CurrentCulture)

        If keyPairs.ContainsKey(sectionPair) Then
            keyPairs.Remove(sectionPair)
        End If

        keyPairs.Add(sectionPair, settingValue)
    End Sub

    ''' <summary>
    ''' Adds or replaces a setting to the table to be saved with a null value.
    ''' </summary>
    ''' <param name="sectionName">Section to add under.</param>
    ''' <param name="settingName">Key name to add.</param>
    Public Sub AddSetting(sectionName As String, settingName As String)
        AddSetting(sectionName, settingName, Nothing)
    End Sub

    ''' <summary>
    ''' Remove a setting.
    ''' </summary>
    ''' <param name="sectionName">Section to add under.</param>
    ''' <param name="settingName">Key name to add.</param>
    Public Sub DeleteSetting(sectionName As String, settingName As String)
        If sectionName Is Nothing Then sectionName = ""
        If settingName Is Nothing Then settingName = ""
        Dim sectionPair As SectionPair
        sectionPair.Section = sectionName.ToUpper(Globalization.CultureInfo.CurrentCulture)
        sectionPair.SettingName = settingName.ToUpper(Globalization.CultureInfo.CurrentCulture)

        If keyPairs.ContainsKey(sectionPair) Then
            keyPairs.Remove(sectionPair)
        End If
    End Sub

    ''' <summary>
    ''' Save settings to new file.
    ''' </summary>
    ''' <param name="newFilePath">New file path.</param>
    Public Sub SaveSettings(newFilePath As String)
        Dim sections As New ArrayList()
        Dim tmpValue As String = ""
        Dim strToSave As String = ""

        For Each sectionPair As SectionPair In keyPairs.Keys
            If Not sections.Contains(sectionPair.Section) Then
                sections.Add(sectionPair.Section)
            End If
        Next

        For Each section As String In sections
            strToSave += ("[" & section & "]" & Convert.ToChar(13) & Convert.ToChar(10))

            For Each sectionPair As SectionPair In keyPairs.Keys
                If sectionPair.Section = section Then
                    tmpValue = DirectCast(keyPairs(sectionPair), String)

                    If tmpValue IsNot Nothing Then
                        tmpValue = sectionPair.SettingName & "=" & tmpValue
                    End If

                    strToSave += (tmpValue & Convert.ToChar(13) & Convert.ToChar(10))
                End If
            Next

            strToSave += Convert.ToChar(13) & Convert.ToChar(10)
        Next

        Try
            Using tw As TextWriter = New StreamWriter(newFilePath)
                tw.Write(strToSave)
            End Using
        Catch ex As Exception
            Throw 'ex
        End Try
    End Sub

    ''' <summary>
    ''' Save settings back to ini file.
    ''' </summary>
    Public Sub SaveSettings()
        SaveSettings(FilePath)
    End Sub

    Public Sub SetPropertiesBySection(ByVal target As Object, ByVal iniFileSection As String)
        If target Is Nothing Then
            Throw New System.ArgumentNullException("target", "SetPropertiesByIniFile null parameter of targetObject")
        End If

        Dim settings() As String = ListSettings(iniFileSection)
        '* Loop thtough all the settings in this section
        For index = 0 To settings.Length - 1
            Dim pi As System.Reflection.PropertyInfo
            pi = target.GetType().GetProperty(settings(index), Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
            '* Check if a matching property name exists in the targetObject
            If pi IsNot Nothing Then
                Dim value As Object
                If pi.PropertyType.IsEnum Then
                    Try
                        '* V3.99y - Added Enum capability
                        '* Enum type have to be converted from string to the Enum option
                        Dim Setting As String = GetSetting(iniFileSection, settings(index))
                        value = [Enum].Parse(pi.PropertyType, Setting, True)
                    Catch ex As Exception
                        Throw New System.ArgumentException("Ini File Error - Section " & iniFileSection & " and setting " & settings(index) & " with a value of """ & GetSetting(iniFileSection, settings(index)) & """. Type is an Enum. Values of " & GetSetting(iniFileSection, settings(index)) & " is not a valid option.")
                    End Try
                Else
                    Try
                        value = Convert.ChangeType(GetSetting(iniFileSection, settings(index)), target.GetType().GetProperty(pi.Name).PropertyType, Globalization.CultureInfo.InvariantCulture)
                    Catch ex As Exception
                        Throw New System.ArgumentException("Ini File Error - Section " & iniFileSection & " and setting " & settings(index) & " with a value of """ & GetSetting(iniFileSection, settings(index)) & """ cannot be converted to type " & target.GetType().GetProperty(pi.Name).PropertyType.ToString())
                    End Try
                End If
                pi.SetValue(target, value, Nothing)
            Else
                Throw New System.ArgumentException("Ini File Error - " & settings(index) & " is not a valid property.")
            End If
        Next
    End Sub

    Public Shared Sub SetPropertiesByIniFile(ByVal target As Object, ByVal iniFileName As String, ByVal iniFileSection As String)
        If target Is Nothing Then
            Throw New System.ArgumentNullException("target", "SetPropertiesByIniFile null parameter of targetObject")
        End If

        If Not String.IsNullOrEmpty(iniFileName) Then
            Dim p As New IniParser(iniFileName)
            Dim settings() As String = p.ListSettings(iniFileSection)
            '* Loop thtough all the settings in this section
            For index = 0 To settings.Length - 1
                Dim pi As System.Reflection.PropertyInfo
                pi = target.GetType().GetProperty(settings(index), Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
                '* Check if a matching property name exists in the targetObject
                If pi IsNot Nothing Then
                    Dim value As Object
                    If pi.PropertyType.IsEnum Then
                        Try
                            '* V3.99y - Added Enum capability
                            '* Enum type have to be converted from string to the Enum option
                            value = [Enum].Parse(pi.PropertyType, p.GetSetting(iniFileSection, settings(index)), True)
                        Catch ex As Exception
                            Throw New System.ArgumentException("Ini File Error - " & settings(index) & " is an an Enum. Values of " & p.GetSetting(iniFileSection, settings(index)) & " is not a valid option.")
                        End Try
                    Else
                        value = Convert.ChangeType(p.GetSetting(iniFileSection, settings(index)), target.GetType().GetProperty(pi.Name).PropertyType, Globalization.CultureInfo.InvariantCulture)
                    End If
                    pi.SetValue(target, value, Nothing)
                Else
                    Throw New System.ArgumentException("Ini File Error - " & settings(index) & " is not a valid property.")
                End If
            Next
        End If
    End Sub

End Class

