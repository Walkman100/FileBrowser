Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml

Public Class Settings
    Private _settingsPath As String

    Public ReadOnly Property Loaded As Boolean = False
    Public Sub Init()
        Dim configFileName As String = "FileBrowser.xml"

        If Helpers.GetOS() = OS.Windows Then
            If Not       Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS")) Then
                Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS"))
            End If
            _settingsPath =               Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS", configFileName)
        Else
            If Not       Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS")) Then
                Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS"))
            End If
            _settingsPath =               Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS", configFileName)
        End If

        If      File.Exists(Path.Combine(Application.StartupPath, configFileName)) Then
            _settingsPath = Path.Combine(Application.StartupPath, configFileName)
        ElseIf               File.Exists(configFileName) Then
            _settingsPath = New FileInfo(configFileName).FullName
        End If

        _Loaded = True
        If File.Exists(_settingsPath) Then
            LoadSettings()
        Else
            'LoadInitialSettings()
        End If
    End Sub

    Private Sub MeVisibleChanged() Handles Me.VisibleChanged
        If Me.Visible Then Me.CenterToParent()
    End Sub

#Region "Properties"
    Public ReadOnly Property ShowFoldersFirst As Boolean
    Public ReadOnly Property ShowADSSeparate As Boolean
    Public ReadOnly Property ShowHidden As Boolean
    Public ReadOnly Property ShowSystem As Boolean
    Public ReadOnly Property ShowDot As Boolean
    Public ReadOnly Property ShowExtensions As Boolean
    Public ReadOnly Property OverlayShortcut As Boolean
    Public ReadOnly Property OverlayReparse As Boolean
    Public ReadOnly Property OverlayHardlink As Boolean
    Public ReadOnly Property OverlayCompressed As Boolean
    Public ReadOnly Property OverlayEncrypted As Boolean
    Public ReadOnly Property OverlayOffline As Boolean
    Public ReadOnly Property SpecificItemIcons As Boolean
    Public ReadOnly Property ImageThumbs As Boolean
    Public ReadOnly Property HighlightCompressed As Boolean
    Public ReadOnly Property HighlightEncrypted As Boolean
    Public ReadOnly Property DisableViewAutoUpdate As Boolean
    Public ReadOnly Property DisableTreeAutoUpdate As Boolean
    Public ReadOnly Property DisableUpdateCheck As Boolean
    Public ReadOnly Property WindowsShellDefaultValue As Boolean
    Public ReadOnly Property WindowMaximised As Boolean
    Public ReadOnly Property WindowRemember As Boolean
    Public ReadOnly Property WindowDefaultWidth As Integer?
    Public ReadOnly Property WindowDefaultHeight As Integer?
#End Region

#Region "GUI Methods"
    Private Sub chkShowFoldersFirst_CheckedChanged() Handles chkShowFoldersFirst.CheckedChanged
        _ShowFoldersFirst = chkShowFoldersFirst.Checked
        SaveSettings()
    End Sub
    Private Sub chkShowADSSeparate_CheckedChanged() Handles chkShowADSSeparate.CheckedChanged
        _ShowADSSeparate = chkShowADSSeparate.Checked
        SaveSettings()
    End Sub
    Private Sub chkShowHidden_CheckedChanged() Handles chkShowHidden.CheckedChanged
        _ShowHidden = chkShowHidden.Checked
        SaveSettings()
    End Sub
    Private Sub chkShowSystem_CheckedChanged() Handles chkShowSystem.CheckedChanged
        _ShowSystem = chkShowSystem.Checked
        SaveSettings()
    End Sub
    Private Sub chkShowDot_CheckedChanged() Handles chkShowDot.CheckedChanged
        _ShowDot = chkShowDot.Checked
        SaveSettings()
    End Sub
    Private Sub chkShowExtensions_CheckedChanged() Handles chkShowExtensions.CheckedChanged
        _ShowExtensions = chkShowExtensions.Checked
        SaveSettings()
    End Sub
    Private Sub chkOverlayShortcut_CheckedChanged() Handles chkOverlayShortcut.CheckedChanged
        _OverlayShortcut = chkOverlayShortcut.Checked
        SaveSettings()
    End Sub
    Private Sub chkOverlayReparse_CheckedChanged() Handles chkOverlayReparse.CheckedChanged
        _OverlayReparse = chkOverlayReparse.Checked
        SaveSettings()
    End Sub
    Private Sub chkOverlayHardlink_CheckedChanged() Handles chkOverlayHardlink.CheckedChanged
        _OverlayHardlink = chkOverlayHardlink.Checked
        SaveSettings()
    End Sub
    Private Sub chkOverlayCompressed_CheckedChanged() Handles chkOverlayCompressed.CheckedChanged
        _OverlayCompressed = chkOverlayCompressed.Checked
        SaveSettings()
    End Sub
    Private Sub chkOverlayEncrypted_CheckedChanged() Handles chkOverlayEncrypted.CheckedChanged
        _OverlayEncrypted = chkOverlayEncrypted.Checked
        SaveSettings()
    End Sub
    Private Sub chkOverlayOffline_CheckedChanged() Handles chkOverlayOffline.CheckedChanged
        _OverlayOffline = chkOverlayOffline.Checked
        SaveSettings()
    End Sub
    Private Sub chkSpecificItemIcons_CheckedChanged() Handles chkSpecificItemIcons.CheckedChanged
        _SpecificItemIcons = chkSpecificItemIcons.Checked
        SaveSettings()
    End Sub
    Private Sub chkImageThumbs_CheckedChanged() Handles chkImageThumbs.CheckedChanged
        _ImageThumbs = chkImageThumbs.Checked
        SaveSettings()
    End Sub
    Private Sub chkHighlightCompressed_CheckedChanged() Handles chkHighlightCompressed.CheckedChanged
        _HighlightCompressed = chkHighlightCompressed.Checked
        SaveSettings()
    End Sub
    Private Sub chkHighlightEncrypted_CheckedChanged() Handles chkHighlightEncrypted.CheckedChanged
        _HighlightEncrypted = chkHighlightEncrypted.Checked
        SaveSettings()
    End Sub
    Private Sub chkDisableViewAutoUpdate_CheckedChanged() Handles chkDisableViewAutoUpdate.CheckedChanged
        _DisableViewAutoUpdate = chkDisableViewAutoUpdate.Checked
        SaveSettings()
    End Sub
    Private Sub chkDisableTreeAutoUpdate_CheckedChanged() Handles chkDisableTreeAutoUpdate.CheckedChanged
        _DisableTreeAutoUpdate = chkDisableTreeAutoUpdate.Checked
        SaveSettings()
    End Sub
    Private Sub chkDisableUpdateCheck_CheckedChanged() Handles chkDisableUpdateCheck.CheckedChanged
        _DisableUpdateCheck = chkDisableUpdateCheck.Checked
        SaveSettings()
    End Sub
    Private Sub chkWindowsShellDefaultValue_CheckedChanged() Handles chkWindowsShellDefaultValue.CheckedChanged
        _WindowsShellDefaultValue = chkWindowsShellDefaultValue.Checked
        SaveSettings()
    End Sub
    Private Sub chkWindowMaximised_CheckedChanged() Handles chkWindowMaximised.CheckedChanged
        _WindowMaximised = chkWindowMaximised.Checked
        SaveSettings()
    End Sub
    Private Sub chkWindowRemember_CheckedChanged() Handles chkWindowRemember.CheckedChanged
        _WindowRemember = chkWindowRemember.Checked
        SaveSettings()
        grpWindowDefault.Enabled = Not chkWindowRemember.Checked
        FileBrowser.FileBrowser_Resize()
    End Sub
    Private Sub txtWindowDefaultWidth_TextChanged() Handles txtWindowDefaultWidth.TextChanged
        Dim tmpInt As Integer
        If Integer.TryParse(txtWindowDefaultWidth.Text, tmpInt) Then
            txtWindowDefaultWidth.BackColor = Drawing.SystemColors.Window
            _WindowDefaultWidth = tmpInt
        Else
            txtWindowDefaultWidth.BackColor = Drawing.Color.Red
            _WindowDefaultWidth = Nothing
        End If
        SaveSettings()
    End Sub
    Private Sub txtWindowDefaultHeight_TextChanged() Handles txtWindowDefaultHeight.TextChanged
        Dim tmpInt As Integer
        If Integer.TryParse(txtWindowDefaultHeight.Text, tmpInt) Then
            txtWindowDefaultHeight.BackColor = Drawing.SystemColors.Window
            _WindowDefaultHeight = tmpInt
        Else
            txtWindowDefaultHeight.BackColor = Drawing.Color.Red
            _WindowDefaultHeight = Nothing
        End If
        SaveSettings()
    End Sub

    Private Sub btnClose_Click() Handles btnClose.Click
        Me.Hide()
    End Sub
    Private Sub btnShowSettingsFile_Click() Handles btnShowSettingsFile.Click
        Helpers.ShowFileExternal(_settingsPath)
    End Sub
#End Region

#Region "Settings Saving & Loading"
    Private Sub LoadSettings() Handles btnReload.Click
        If Not Loaded Then Return
        _loading = True
        Try
            Using reader As XmlReader = XmlReader.Create(_settingsPath)
                Try
                    reader.Read()
                Catch ex As XmlException
                    reader.Close()
                    Return
                End Try

                If reader.IsStartElement AndAlso reader.Name = "FileBrowser" Then
                    If reader.Read AndAlso reader.IsStartElement AndAlso reader.Name = "Settings" Then
                        While reader.Read
                            If reader.IsStartElement Then
                                Select Case reader.Name
                                    Case "ShowFoldersFirst"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkShowFoldersFirst.Checked)
                                    Case "ShowADSSeparate"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkShowADSSeparate.Checked)
                                    Case "ShowHidden"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkShowHidden.Checked)
                                    Case "ShowSystem"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkShowSystem.Checked)
                                    Case "ShowDot"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkShowDot.Checked)
                                    Case "ShowExtensions"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkShowExtensions.Checked)
                                    Case "OverlayShortcut"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkOverlayShortcut.Checked)
                                    Case "OverlayReparse"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkOverlayReparse.Checked)
                                    Case "OverlayHardlink"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkOverlayHardlink.Checked)
                                    Case "OverlayCompressed"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkOverlayCompressed.Checked)
                                    Case "OverlayEncrypted"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkOverlayEncrypted.Checked)
                                    Case "OverlayOffline"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkOverlayOffline.Checked)
                                    Case "SpecificItemIcons"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkSpecificItemIcons.Checked)
                                    Case "ImageThumbs"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkImageThumbs.Checked)
                                    Case "HighlightCompressed"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkHighlightCompressed.Checked)
                                    Case "HighlightEncrypted"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkHighlightEncrypted.Checked)
                                    Case "DisableViewAutoUpdate"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkDisableViewAutoUpdate.Checked)
                                    Case "DisableTreeAutoUpdate"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkDisableTreeAutoUpdate.Checked)
                                    Case "DisableUpdateCheck"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkDisableUpdateCheck.Checked)
                                    Case "WindowsShellDefaultValue"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkWindowsShellDefaultValue.Checked)
                                    Case "WindowMaximised"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkWindowMaximised.Checked)
                                    Case "WindowRemember"
                                        reader.Read()
                                        Boolean.TryParse(reader.Value, chkWindowRemember.Checked)
                                    Case "WindowDefaultWidth"
                                        reader.Read()
                                        txtWindowDefaultWidth.Text = reader.Value
                                    Case "WindowDefaultHeight"
                                        reader.Read()
                                        txtWindowDefaultHeight.Text = reader.Value
                                End Select
                            End If
                        End While
                    End If
                End If
            End Using
        Finally
            _loading = False
        End Try
    End Sub

    Private _loading As Boolean = False ' so that we don't save while loading settings

    Private Sub SaveSettings() Handles btnSave.Click
        If Not Loaded OrElse _loading Then Return
        Using writer As XmlWriter = XmlWriter.Create(_settingsPath, New XmlWriterSettings With {.Indent = True})
            writer.WriteStartDocument()
            writer.WriteStartElement("FileBrowser")

            writer.WriteStartElement("Settings")
            writer.WriteElementString("ShowFoldersFirst", ShowFoldersFirst.ToString())
            writer.WriteElementString("ShowADSSeparate", ShowADSSeparate.ToString())
            writer.WriteElementString("ShowHidden", ShowHidden.ToString())
            writer.WriteElementString("ShowSystem", ShowSystem.ToString())
            writer.WriteElementString("ShowDot", ShowDot.ToString())
            writer.WriteElementString("ShowExtensions", ShowExtensions.ToString())
            writer.WriteElementString("OverlayShortcut", OverlayShortcut.ToString())
            writer.WriteElementString("OverlayReparse", OverlayReparse.ToString())
            writer.WriteElementString("OverlayHardlink", OverlayHardlink.ToString())
            writer.WriteElementString("OverlayCompressed", OverlayCompressed.ToString())
            writer.WriteElementString("OverlayEncrypted", OverlayEncrypted.ToString())
            writer.WriteElementString("OverlayOffline", OverlayOffline.ToString())
            writer.WriteElementString("SpecificItemIcons", SpecificItemIcons.ToString())
            writer.WriteElementString("ImageThumbs", ImageThumbs.ToString())
            writer.WriteElementString("HighlightCompressed", HighlightCompressed.ToString())
            writer.WriteElementString("HighlightEncrypted", HighlightEncrypted.ToString())
            writer.WriteElementString("DisableViewAutoUpdate", DisableViewAutoUpdate.ToString())
            writer.WriteElementString("DisableTreeAutoUpdate", DisableTreeAutoUpdate.ToString())
            writer.WriteElementString("DisableUpdateCheck", DisableUpdateCheck.ToString())
            writer.WriteElementString("WindowsShellDefaultValue", WindowsShellDefaultValue.ToString())
            writer.WriteElementString("WindowMaximised", WindowMaximised.ToString())
            writer.WriteElementString("WindowRemember", WindowRemember.ToString())
            writer.WriteElementString("WindowDefaultWidth", WindowDefaultWidth.ToString())
            writer.WriteElementString("WindowDefaultHeight", WindowDefaultHeight.ToString())
            writer.WriteEndElement()

            writer.WriteEndElement()
            writer.WriteEndDocument()
        End Using
    End Sub
#End Region
End Class