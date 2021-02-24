Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports System.Xml
Imports Ookii.Dialogs

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
        chkEnableIcons_CheckedChanged() ' Make sure grpIcons is in the correct enabled state

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
    Public ReadOnly Property OverlayReparse As Boolean
    Public ReadOnly Property OverlayHardlink As Boolean
    Public ReadOnly Property OverlayCompressed As Boolean
    Public ReadOnly Property OverlayEncrypted As Boolean
    Public ReadOnly Property OverlayOffline As Boolean
    Public ReadOnly Property EnableIcons As Boolean
    Public ReadOnly Property SpecificItemIcons As Boolean
    Public ReadOnly Property ImageThumbs As Boolean
    Public ReadOnly Property HighlightCompressed As Boolean
    Public ReadOnly Property HighlightEncrypted As Boolean
    Public ReadOnly Property DisableViewAutoUpdate As Boolean
    Public ReadOnly Property DisableTreeAutoUpdate As Boolean
    Public ReadOnly Property DisableUpdateCheck As Boolean
    Public ReadOnly Property WindowsShellDefaultValue As Boolean
    Public ReadOnly Property SizeUnits As Integer
    Public ReadOnly Property RememberDir As Boolean
    Public ReadOnly Property DefaultDir As String = ""
    Public ReadOnly Property WindowMaximised As Boolean
    Public ReadOnly Property WindowRemember As Boolean
    Public ReadOnly Property WindowDefaultWidth As Integer?
    Public ReadOnly Property WindowDefaultHeight As Integer?
    Public ReadOnly Property SplitterRemember As Boolean
    Public ReadOnly Property SplitterSize As Integer?
    Public ReadOnly Property SaveColumns As Boolean
#End Region

#Region "Column Saving"
    Public Structure Column
        Public SaveName As String
        Public DisplayIndex As Integer
        Public Width As Integer
    End Structure

    Public ReadOnly Property DefaultColumns As New List(Of Column)

    ''' <summary>Saves current columns from <see cref="FileBrowser.lstCurrent"/> to <see cref="DefaultColumns"/></summary>
    Public Sub SaveDefaultColumns()
        DefaultColumns.Clear()

        DefaultColumns.AddRange(FileBrowser.lstCurrent.Columns.Cast(Of ColumnHeader).
                                    Select(Function(ch) New Column With {
                                        .SaveName = DirectCast(ch.Tag, String),
                                        .DisplayIndex = ch.DisplayIndex,
                                        .Width = ch.Width
                                    }))
        SaveSettings()
    End Sub

    ''' <summary>Loads columns from <see cref="DefaultColumns"/> to columns in <see cref="FileBrowser.lstCurrent"/></summary>
    Public Sub LoadDefaultColumns(fb As FileBrowser)
        Helpers.ApplyColumns(fb, DefaultColumns)
    End Sub
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
    Private Sub chkEnableIcons_CheckedChanged() Handles chkEnableIcons.CheckedChanged
        _EnableIcons = chkEnableIcons.Checked
        SaveSettings()
        grpIcons.Enabled = chkEnableIcons.Checked
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
    Private Sub cbxSizeUnits_SelectedIndexChanged() Handles cbxSizeUnits.SelectedIndexChanged
        _SizeUnits = cbxSizeUnits.SelectedIndex
        SaveSettings()
    End Sub
    Private Sub chkRememberDir_CheckedChanged() Handles chkRememberDir.CheckedChanged
        _RememberDir = chkRememberDir.Checked
        SaveSettings()
        grpDefaultDir.Enabled = Not chkRememberDir.Checked
        If RememberDir Then txtDefaultDir.Text = FileBrowser.CurrentDir
    End Sub
    Private Sub txtDefaultDir_TextChanged() Handles txtDefaultDir.TextChanged
        _DefaultDir = txtDefaultDir.Text
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
    Private Sub chkSplitterRemember_CheckedChanged() Handles chkSplitterRemember.CheckedChanged
        _SplitterRemember = chkSplitterRemember.Checked
        SaveSettings()
        grpSplitterDefault.Enabled = Not chkSplitterRemember.Checked
        FileBrowser.scMain_SplitterDistanceChanged()
    End Sub
    Private Sub txtSplitterDefaultSize_TextChanged() Handles txtSplitterDefaultSize.TextChanged
        Dim tmpInt As Integer
        If Integer.TryParse(txtSplitterDefaultSize.Text, tmpInt) Then
            txtSplitterDefaultSize.BackColor = Drawing.SystemColors.Window
            _SplitterSize = tmpInt
        Else
            txtSplitterDefaultSize.BackColor = Drawing.Color.Red
            _SplitterSize = Nothing
        End If
        SaveSettings()
    End Sub

    Private Sub chkSaveColumns_CheckedChanged() Handles chkSaveColumns.CheckedChanged
        _SaveColumns = chkSaveColumns.Checked
        SaveSettings()
    End Sub

    Private Sub btnDefaultDirBrowse_Click() Handles btnDefaultDirBrowse.Click
        Dim sdd As New VistaFolderBrowserDialog With {
            .UseDescriptionForTitle = True,
            .Description = "Select Default Directory",
            .SelectedPath = txtDefaultDir.Text
        }

        If sdd.ShowDialog(Me) = DialogResult.OK Then
            txtDefaultDir.Text = sdd.SelectedPath
        End If
    End Sub
    Private Sub btnClearURIHistory_Click() Handles btnClearURIHistory.Click
        FileBrowser.cbxURI.Items.Clear()
    End Sub
    Private Sub btnResetColumns_Click() Handles btnResetColumns.Click
        DefaultColumns.Clear()

        Using fb As New FileBrowser
            DefaultColumns.AddRange(fb.lstCurrent.Columns.Cast(Of ColumnHeader).
                                    Select(Function(ch) New Column With {
                                        .SaveName = DirectCast(ch.Tag, String),
                                        .DisplayIndex = ch.DisplayIndex,
                                        .Width = ch.Width
                                    }))
        End Using
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
                    If reader.Read() AndAlso reader.IsStartElement AndAlso reader.Name = "Settings" AndAlso reader.Read() Then
                        While reader.IsStartElement
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
                                Case "EnableIcons"
                                    reader.Read()
                                    Boolean.TryParse(reader.Value, chkEnableIcons.Checked)
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
                                Case "SizeUnits"
                                    reader.Read()
                                    Integer.TryParse(reader.Value, cbxSizeUnits.SelectedIndex)
                                Case "RememberDir"
                                    reader.Read()
                                    Boolean.TryParse(reader.Value, chkRememberDir.Checked)
                                Case "DefaultDir"
                                    reader.Read()
                                    txtDefaultDir.Text = reader.Value
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
                                Case "SplitterRemember"
                                    reader.Read()
                                    Boolean.TryParse(reader.Value, chkSplitterRemember.Checked)
                                Case "SplitterSize"
                                    reader.Read()
                                    txtSplitterDefaultSize.Text = reader.Value
                                Case "SaveColumns"
                                    reader.Read()
                                    Boolean.TryParse(reader.Value, chkSaveColumns.Checked)
                                Case Else
                                    reader.Read() ' skip unknown values
                            End Select
                            reader.Read() : reader.Read()
                        End While
                    End If

                    If reader.Read() AndAlso reader.IsStartElement() AndAlso reader.Name = "URIHistory" AndAlso reader.Read() Then
                        FileBrowser.cbxURI.Items.Clear()
                        While reader.IsStartElement()
                            If reader.Name = "item" AndAlso reader.Read() Then
                                FileBrowser.cbxURI.Items.Add(reader.Value)
                                reader.Read() : reader.Read()
                            End If
                        End While
                    End If

                    If reader.Read() AndAlso reader.IsStartElement() AndAlso reader.Name = "ColumnSettings" Then
                        DefaultColumns.Clear()
                        While reader.IsStartElement
                            If reader.Read() AndAlso reader.IsStartElement() Then
                                Dim col As New Column With {
                                    .SaveName = reader.Name
                                }
                                Integer.TryParse(reader("index"), col.DisplayIndex)
                                Integer.TryParse(reader("width"), col.Width)
                                DefaultColumns.Add(col)
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

    Friend Sub SaveSettings() Handles btnSave.Click
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
            writer.WriteElementString("OverlayReparse", OverlayReparse.ToString())
            writer.WriteElementString("OverlayHardlink", OverlayHardlink.ToString())
            writer.WriteElementString("OverlayCompressed", OverlayCompressed.ToString())
            writer.WriteElementString("OverlayEncrypted", OverlayEncrypted.ToString())
            writer.WriteElementString("OverlayOffline", OverlayOffline.ToString())
            writer.WriteElementString("EnableIcons", EnableIcons.ToString())
            writer.WriteElementString("SpecificItemIcons", SpecificItemIcons.ToString())
            writer.WriteElementString("ImageThumbs", ImageThumbs.ToString())
            writer.WriteElementString("HighlightCompressed", HighlightCompressed.ToString())
            writer.WriteElementString("HighlightEncrypted", HighlightEncrypted.ToString())
            writer.WriteElementString("DisableViewAutoUpdate", DisableViewAutoUpdate.ToString())
            writer.WriteElementString("DisableTreeAutoUpdate", DisableTreeAutoUpdate.ToString())
            writer.WriteElementString("DisableUpdateCheck", DisableUpdateCheck.ToString())
            writer.WriteElementString("WindowsShellDefaultValue", WindowsShellDefaultValue.ToString())
            writer.WriteElementString("SizeUnits", SizeUnits.ToString())
            writer.WriteElementString("RememberDir", RememberDir.ToString())
            writer.WriteElementString("DefaultDir", DefaultDir)
            writer.WriteElementString("WindowMaximised", WindowMaximised.ToString())
            writer.WriteElementString("WindowRemember", WindowRemember.ToString())
            writer.WriteElementString("WindowDefaultWidth", WindowDefaultWidth.ToString())
            writer.WriteElementString("WindowDefaultHeight", WindowDefaultHeight.ToString())
            writer.WriteElementString("SplitterRemember", SplitterRemember.ToString())
            writer.WriteElementString("SplitterSize", SplitterSize.ToString())
            writer.WriteElementString("SaveColumns", SaveColumns.ToString())
            writer.WriteEndElement() ' Settings

            writer.WriteStartElement("URIHistory")
            For Each item As String In FileBrowser.cbxURI.Items
                writer.WriteElementString("item", item)
            Next
            writer.WriteEndElement() ' URIHistory

            writer.WriteStartElement("ColumnSettings")
            For Each column As Column In DefaultColumns
                writer.WriteStartElement(column.SaveName)
                writer.WriteAttributeString("index", column.DisplayIndex.ToString())
                writer.WriteAttributeString("width", column.Width.ToString())
                writer.WriteEndElement()
            Next
            writer.WriteEndElement() ' ColumnSettings


            writer.WriteEndElement()
            writer.WriteEndDocument()
        End Using
    End Sub
#End Region
End Class