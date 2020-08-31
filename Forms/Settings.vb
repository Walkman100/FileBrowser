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
    Private _showFoldersFirst As Boolean
    Private _showADSSeparate As Boolean
    Private _showHidden As Boolean
    Private _showSystem As Boolean
    Private _showDot As Boolean
    Private _showExtensions As Boolean
    Private _overlayShortcut As Boolean
    Private _overlayReparse As Boolean
    Private _overlayHardlink As Boolean
    Private _overlayCompressed As Boolean
    Private _overlayEncrypted As Boolean
    Private _overlayOffline As Boolean
    Private _specificItemIcons As Boolean
    Private _imageThumbs As Boolean
    Private _highlightCompressed As Boolean
    Private _highlightEncrypted As Boolean
    Private _disableViewAutoUpdate As Boolean
    Private _disableTreeAutoUpdate As Boolean
    Private _disableUpdateCheck As Boolean
    Private _windowsShellDefaultValue As Boolean
    Private _windowMaximised As Boolean
    Private _windowRemember As Boolean
    Private _windowDefaultWidth As Integer?
    Private _windowDefaultHeight As Integer?

    Public Property ShowFoldersFirst As Boolean
        Get
            Return _showFoldersFirst
        End Get
        Private Set(value As Boolean)
            _showFoldersFirst = value
            SaveSettings()
        End Set
    End Property
    Public Property ShowADSSeparate As Boolean
        Get
            Return _showADSSeparate
        End Get
        Private Set(value As Boolean)
            _showADSSeparate = value
            SaveSettings()
        End Set
    End Property
    Public Property ShowHidden As Boolean
        Get
            Return _showHidden
        End Get
        Private Set(value As Boolean)
            _showHidden = value
            SaveSettings()
        End Set
    End Property
    Public Property ShowSystem As Boolean
        Get
            Return _showSystem
        End Get
        Private Set(value As Boolean)
            _showSystem = value
            SaveSettings()
        End Set
    End Property
    Public Property ShowDot As Boolean
        Get
            Return _showDot
        End Get
        Private Set(value As Boolean)
            _showDot = value
            SaveSettings()
        End Set
    End Property
    Public Property ShowExtensions As Boolean
        Get
            Return _showExtensions
        End Get
        Private Set(value As Boolean)
            _showExtensions = value
            SaveSettings()
        End Set
    End Property
    Public Property OverlayShortcut As Boolean
        Get
            Return _overlayShortcut
        End Get
        Private Set(value As Boolean)
            _overlayShortcut = value
            SaveSettings()
        End Set
    End Property
    Public Property OverlayReparse As Boolean
        Get
            Return _overlayReparse
        End Get
        Private Set(value As Boolean)
            _overlayReparse = value
            SaveSettings()
        End Set
    End Property
    Public Property OverlayHardlink As Boolean
        Get
            Return _overlayHardlink
        End Get
        Private Set(value As Boolean)
            _overlayHardlink = value
            SaveSettings()
        End Set
    End Property
    Public Property OverlayCompressed As Boolean
        Get
            Return _overlayCompressed
        End Get
        Private Set(value As Boolean)
            _overlayCompressed = value
            SaveSettings()
        End Set
    End Property
    Public Property OverlayEncrypted As Boolean
        Get
            Return _overlayEncrypted
        End Get
        Private Set(value As Boolean)
            _overlayEncrypted = value
            SaveSettings()
        End Set
    End Property
    Public Property OverlayOffline As Boolean
        Get
            Return _overlayOffline
        End Get
        Private Set(value As Boolean)
            _overlayOffline = value
            SaveSettings()
        End Set
    End Property
    Public Property SpecificItemIcons As Boolean
        Get
            Return _specificItemIcons
        End Get
        Private Set(value As Boolean)
            _specificItemIcons = value
            SaveSettings()
        End Set
    End Property
    Public Property ImageThumbs As Boolean
        Get
            Return _imageThumbs
        End Get
        Private Set(value As Boolean)
            _imageThumbs = value
            SaveSettings()
        End Set
    End Property
    Public Property HighlightCompressed As Boolean
        Get
            Return _highlightCompressed
        End Get
        Private Set(value As Boolean)
            _highlightCompressed = value
            SaveSettings()
        End Set
    End Property
    Public Property HighlightEncrypted As Boolean
        Get
            Return _highlightEncrypted
        End Get
        Private Set(value As Boolean)
            _highlightEncrypted = value
            SaveSettings()
        End Set
    End Property
    Public Property DisableViewAutoUpdate As Boolean
        Get
            Return _disableViewAutoUpdate
        End Get
        Private Set(value As Boolean)
            _disableViewAutoUpdate = value
            SaveSettings()
        End Set
    End Property
    Public Property DisableTreeAutoUpdate As Boolean
        Get
            Return _disableTreeAutoUpdate
        End Get
        Private Set(value As Boolean)
            _disableTreeAutoUpdate = value
            SaveSettings()
        End Set
    End Property
    Public Property DisableUpdateCheck As Boolean
        Get
            Return _disableUpdateCheck
        End Get
        Private Set(value As Boolean)
            _disableUpdateCheck = value
            SaveSettings()
        End Set
    End Property
    Public Property WindowsShellDefaultValue As Boolean
        Get
            Return _windowsShellDefaultValue
        End Get
        Private Set(value As Boolean)
            _windowsShellDefaultValue = value
            SaveSettings()
        End Set
    End Property
    Public Property WindowMaximised As Boolean
        Get
            Return _windowMaximised
        End Get
        Private Set(value As Boolean)
            _windowMaximised = value
            SaveSettings()
        End Set
    End Property
    Public Property WindowRemember As Boolean
        Get
            Return _windowRemember
        End Get
        Private Set(value As Boolean)
            _windowRemember = value
            SaveSettings()
        End Set
    End Property
    Public Property WindowDefaultWidth As Integer?
        Get
            Return _windowDefaultWidth
        End Get
        Private Set(value As Integer?)
            _windowDefaultWidth = value
            SaveSettings()
        End Set
    End Property
    Public Property WindowDefaultHeight As Integer?
        Get
            Return _windowDefaultHeight
        End Get
        Private Set(value As Integer?)
            _windowDefaultHeight = value
            SaveSettings()
        End Set
    End Property
#End Region

#Region "GUI Methods"
    Private Sub chkShowFoldersFirst_CheckedChanged() Handles chkShowFoldersFirst.CheckedChanged
        ShowFoldersFirst = chkShowFoldersFirst.Checked
    End Sub
    Private Sub chkShowADSSeparate_CheckedChanged() Handles chkShowADSSeparate.CheckedChanged
        ShowADSSeparate = chkShowADSSeparate.Checked
    End Sub
    Private Sub chkShowHidden_CheckedChanged() Handles chkShowHidden.CheckedChanged
        ShowHidden = chkShowHidden.Checked
    End Sub
    Private Sub chkShowSystem_CheckedChanged() Handles chkShowSystem.CheckedChanged
        ShowSystem = chkShowSystem.Checked
    End Sub
    Private Sub chkShowDot_CheckedChanged() Handles chkShowDot.CheckedChanged
        ShowDot = chkShowDot.Checked
    End Sub
    Private Sub chkShowExtensions_CheckedChanged() Handles chkShowExtensions.CheckedChanged
        ShowExtensions = chkShowExtensions.Checked
    End Sub
    Private Sub chkOverlayShortcut_CheckedChanged() Handles chkOverlayShortcut.CheckedChanged
        OverlayShortcut = chkOverlayShortcut.Checked
    End Sub
    Private Sub chkOverlayReparse_CheckedChanged() Handles chkOverlayReparse.CheckedChanged
        OverlayReparse = chkOverlayReparse.Checked
    End Sub
    Private Sub chkOverlayHardlink_CheckedChanged() Handles chkOverlayHardlink.CheckedChanged
        OverlayHardlink = chkOverlayHardlink.Checked
    End Sub
    Private Sub chkOverlayCompressed_CheckedChanged() Handles chkOverlayCompressed.CheckedChanged
        OverlayCompressed = chkOverlayCompressed.Checked
    End Sub
    Private Sub chkOverlayEncrypted_CheckedChanged() Handles chkOverlayEncrypted.CheckedChanged
        OverlayEncrypted = chkOverlayEncrypted.Checked
    End Sub
    Private Sub chkOverlayOffline_CheckedChanged() Handles chkOverlayOffline.CheckedChanged
        OverlayOffline = chkOverlayOffline.Checked
    End Sub
    Private Sub chkSpecificItemIcons_CheckedChanged() Handles chkSpecificItemIcons.CheckedChanged
        SpecificItemIcons = chkSpecificItemIcons.Checked
    End Sub
    Private Sub chkImageThumbs_CheckedChanged() Handles chkImageThumbs.CheckedChanged
        ImageThumbs = chkImageThumbs.Checked
    End Sub
    Private Sub chkHighlightCompressed_CheckedChanged() Handles chkHighlightCompressed.CheckedChanged
        HighlightCompressed = chkHighlightCompressed.Checked
    End Sub
    Private Sub chkHighlightEncrypted_CheckedChanged() Handles chkHighlightEncrypted.CheckedChanged
        HighlightEncrypted = chkHighlightEncrypted.Checked
    End Sub
    Private Sub chkDisableViewAutoUpdate_CheckedChanged() Handles chkDisableViewAutoUpdate.CheckedChanged
        DisableViewAutoUpdate = chkDisableViewAutoUpdate.Checked
    End Sub
    Private Sub chkDisableTreeAutoUpdate_CheckedChanged() Handles chkDisableTreeAutoUpdate.CheckedChanged
        DisableTreeAutoUpdate = chkDisableTreeAutoUpdate.Checked
    End Sub
    Private Sub chkDisableUpdateCheck_CheckedChanged() Handles chkDisableUpdateCheck.CheckedChanged
        DisableUpdateCheck = chkDisableUpdateCheck.Checked
    End Sub
    Private Sub chkWindowsShellDefaultValue_CheckedChanged() Handles chkWindowsShellDefaultValue.CheckedChanged
        WindowsShellDefaultValue = chkWindowsShellDefaultValue.Checked
    End Sub
    Private Sub chkWindowMaximised_CheckedChanged() Handles chkWindowMaximised.CheckedChanged
        WindowMaximised = chkWindowMaximised.Checked
    End Sub
    Private Sub chkWindowRemember_CheckedChanged() Handles chkWindowRemember.CheckedChanged
        WindowRemember = chkWindowRemember.Checked
        grpWindowDefault.Enabled = Not chkWindowRemember.Checked
    End Sub
    Private Sub txtWindowDefaultWidth_TextChanged() Handles txtWindowDefaultWidth.TextChanged
        Dim tmpInt As Integer
        If Integer.TryParse(txtWindowDefaultWidth.Text, tmpInt) Then
            txtWindowDefaultWidth.BackColor = Drawing.SystemColors.Window
            WindowDefaultWidth = tmpInt
        Else
            txtWindowDefaultWidth.BackColor = Drawing.Color.Red
            WindowDefaultWidth = Nothing
        End If
    End Sub
    Private Sub txtWindowDefaultHeight_TextChanged() Handles txtWindowDefaultHeight.TextChanged
        Dim tmpInt As Integer
        If Integer.TryParse(txtWindowDefaultHeight.Text, tmpInt) Then
            txtWindowDefaultHeight.BackColor = Drawing.SystemColors.Window
            WindowDefaultHeight = tmpInt
        Else
            txtWindowDefaultHeight.BackColor = Drawing.Color.Red
            WindowDefaultHeight = Nothing
        End If
    End Sub

    Private Sub btnClose_Click() Handles btnClose.Click
        Me.Hide()
    End Sub
    Private Sub btnShowSettingsFile_Click() Handles btnShowSettingsFile.Click
        Launch.LaunchItem(_settingsPath, "explorer.exe", "/select, ""{path}""")
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