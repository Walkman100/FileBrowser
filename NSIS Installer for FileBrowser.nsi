; FileBrowser Installer NSIS Script
; get NSIS at http://nsis.sourceforge.net/Download

!define ProgramName "FileBrowser"
; Icon ""

Name "${ProgramName}"
Caption "${ProgramName} Installer"
XPStyle on
ShowInstDetails show
AutoCloseWindow true

LicenseBkColor /windows
LicenseData "LICENSE.md"
LicenseForceSelection checkbox "I have read and understand this notice"
LicenseText "Please read the notice below before installing ${ProgramName}. If you understand the notice, click the checkbox below and click Next."

InstallDir $PROGRAMFILES\WalkmanOSS\${ProgramName}
InstallDirRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "InstallLocation"
OutFile "bin\Release\${ProgramName}-Installer.exe"

; Pages

Page license
Page components
Page directory
Page instfiles
Page custom postInstallShow postInstallFinish ": Install Complete"
UninstPage uninstConfirm
UninstPage instfiles

; Sections

Section "Executable, Uninstaller & Ookii.Dialogs"
  SectionIn RO
  SetOutPath $INSTDIR
  File "bin\Release\${ProgramName}.exe"
  File "bin\Release\${ProgramName}.exe.config"
  File "bin\Release\Ookii.Dialogs.dll"
  WriteUninstaller "${ProgramName}-Uninst.exe"
SectionEnd

Section "Default Icons"
  SetOutPath $INSTDIR\icons
  File "Properties\DefaultIcons\Back.png"
  File "Properties\DefaultIcons\Cancel.png"
  File "Properties\DefaultIcons\ColumnConfig.png"
  File "Properties\DefaultIcons\ContextConfig.png"
  File "Properties\DefaultIcons\CopyPath.png"
  File "Properties\DefaultIcons\Copy.png"
  File "Properties\DefaultIcons\CopyTo.png"
  File "Properties\DefaultIcons\Cut.png"
  File "Properties\DefaultIcons\Delete.png"
  File "Properties\DefaultIcons\Forward.png"
  File "Properties\DefaultIcons\Go.png"
  File "Properties\DefaultIcons\Home.png"
  File "Properties\DefaultIcons\InvertSelection.png"
  File "Properties\DefaultIcons\MoveTo.png"
  File "Properties\DefaultIcons\New.png"
  File "Properties\DefaultIcons\OpenLocation.png"
  File "Properties\DefaultIcons\OpenWith.png"
  File "Properties\DefaultIcons\Paste.png"
  File "Properties\DefaultIcons\PasteHardlink.png"
  File "Properties\DefaultIcons\PasteJunction.png"
  File "Properties\DefaultIcons\PasteShortcut.png"
  File "Properties\DefaultIcons\PasteSymlink.png"
  File "Properties\DefaultIcons\Properties.png"
  File "Properties\DefaultIcons\Quit.png"
  File "Properties\DefaultIcons\Recycle.png"
  File "Properties\DefaultIcons\Refresh.png"
  File "Properties\DefaultIcons\Rename.png"
  File "Properties\DefaultIcons\ResizeColumns.png"
  File "Properties\DefaultIcons\Root.png"
  File "Properties\DefaultIcons\RunAsAdmin.png"
  File "Properties\DefaultIcons\Run.png"
  File "Properties\DefaultIcons\SelectAll.png"
  File "Properties\DefaultIcons\SelectNone.png"
  File "Properties\DefaultIcons\Settings.png"
  File "Properties\DefaultIcons\Up.png"
  SetOutPath $INSTDIR
SectionEnd

Section "Add to Windows Programs & Features"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "DisplayName" "${ProgramName}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "Publisher" "WalkmanOSS"
  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "DisplayIcon" "$INSTDIR\${ProgramName}.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "InstallLocation" "$INSTDIR\"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "UninstallString" "$INSTDIR\${ProgramName}-Uninst.exe"
  
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "NoRepair" 1
  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "HelpLink" "https://github.com/Walkman100/${ProgramName}/issues/new"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "URLInfoAbout" "https://github.com/Walkman100/${ProgramName}" ; Support Link
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "URLUpdateInfo" "https://github.com/Walkman100/${ProgramName}/releases" ; Update Info Link
SectionEnd

Section "Start Menu Shortcuts"
  CreateDirectory "$SMPROGRAMS\WalkmanOSS"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\${ProgramName}.lnk" "$INSTDIR\${ProgramName}.exe" "" "$INSTDIR\${ProgramName}.exe" "" "" "" "${ProgramName}"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\Uninstall ${ProgramName}.lnk" "$INSTDIR\${ProgramName}-Uninst.exe" "" "" "" "" "" "Uninstall ${ProgramName}"
  ;Syntax for CreateShortCut: link.lnk target.file [parameters [icon.file [icon_index_number [start_options [keyboard_shortcut [description]]]]]]
SectionEnd

Section "Desktop Shortcut"
  CreateShortCut "$DESKTOP\${ProgramName}.lnk" "$INSTDIR\${ProgramName}.exe" "" "$INSTDIR\${ProgramName}.exe" "" "" "" "${ProgramName}"
SectionEnd

Section "Quick Launch Shortcut"
  CreateShortCut "$QUICKLAUNCH\${ProgramName}.lnk" "$INSTDIR\${ProgramName}.exe" "" "$INSTDIR\${ProgramName}.exe" "" "" "" "${ProgramName}"
SectionEnd

; Functions

Function .onInit
  SetShellVarContext all
  SetAutoClose true
FunctionEnd

; Custom Install Complete page

!include nsDialogs.nsh
!include LogicLib.nsh ; For ${IF} logic
Var Dialog
Var Label
Var CheckboxReadme
Var CheckboxReadme_State
Var CheckboxRunProgram
Var CheckboxRunProgram_State

Function postInstallShow
  nsDialogs::Create 1018
  Pop $Dialog
  ${If} $Dialog == error
    Abort
  ${EndIf}
  
  ${NSD_CreateLabel} 0 0 100% 12u "Setup will launch these tasks when you click close:"
  Pop $Label
  
  ${NSD_CreateCheckbox} 10u 30u 100% 10u "&Open Readme"
  Pop $CheckboxReadme
  ${If} $CheckboxReadme_State == ${BST_CHECKED}
    ${NSD_Check} $CheckboxReadme
  ${EndIf}
  
  ${NSD_CreateCheckbox} 10u 50u 100% 10u "&Launch ${ProgramName}"
  Pop $CheckboxRunProgram
  ${If} $CheckboxRunProgram_State == ${BST_CHECKED}
    ${NSD_Check} $CheckboxRunProgram
  ${EndIf}
  
  # alternative for the above ${If}:
  #${NSD_SetState} $Checkbox_State
  nsDialogs::Show
FunctionEnd

Function postInstallFinish
  ${NSD_GetState} $CheckboxReadme $CheckboxReadme_State
  ${NSD_GetState} $CheckboxRunProgram $CheckboxRunProgram_State
  
  ${If} $CheckboxReadme_State == ${BST_CHECKED}
    ExecShell "open" "https://github.com/Walkman100/${ProgramName}/blob/master/README.md"
  ${EndIf}
  ${If} $CheckboxRunProgram_State == ${BST_CHECKED}
    ExecShell "open" "$INSTDIR\${ProgramName}.exe"
  ${EndIf}
FunctionEnd

; Uninstaller

Section "Uninstall"
  Delete "$INSTDIR\icons\Back.png"
  Delete "$INSTDIR\icons\Cancel.png"
  Delete "$INSTDIR\icons\ColumnConfig.png"
  Delete "$INSTDIR\icons\ContextConfig.png"
  Delete "$INSTDIR\icons\CopyPath.png"
  Delete "$INSTDIR\icons\Copy.png"
  Delete "$INSTDIR\icons\CopyTo.png"
  Delete "$INSTDIR\icons\Cut.png"
  Delete "$INSTDIR\icons\Delete.png"
  Delete "$INSTDIR\icons\Forward.png"
  Delete "$INSTDIR\icons\Go.png"
  Delete "$INSTDIR\icons\Home.png"
  Delete "$INSTDIR\icons\InvertSelection.png"
  Delete "$INSTDIR\icons\MoveTo.png"
  Delete "$INSTDIR\icons\New.png"
  Delete "$INSTDIR\icons\OpenLocation.png"
  Delete "$INSTDIR\icons\OpenWith.png"
  Delete "$INSTDIR\icons\Paste.png"
  Delete "$INSTDIR\icons\PasteHardlink.png"
  Delete "$INSTDIR\icons\PasteJunction.png"
  Delete "$INSTDIR\icons\PasteShortcut.png"
  Delete "$INSTDIR\icons\PasteSymlink.png"
  Delete "$INSTDIR\icons\Properties.png"
  Delete "$INSTDIR\icons\Quit.png"
  Delete "$INSTDIR\icons\Recycle.png"
  Delete "$INSTDIR\icons\Refresh.png"
  Delete "$INSTDIR\icons\Rename.png"
  Delete "$INSTDIR\icons\ResizeColumns.png"
  Delete "$INSTDIR\icons\Root.png"
  Delete "$INSTDIR\icons\RunAsAdmin.png"
  Delete "$INSTDIR\icons\Run.png"
  Delete "$INSTDIR\icons\SelectAll.png"
  Delete "$INSTDIR\icons\SelectNone.png"
  Delete "$INSTDIR\icons\Settings.png"
  Delete "$INSTDIR\icons\Up.png"
  RMDir "$INSTDIR\icons"
  
  Delete "$INSTDIR\${ProgramName}-Uninst.exe" ; Remove Application Files
  Delete "$INSTDIR\${ProgramName}.exe"
  Delete "$INSTDIR\${ProgramName}.exe.config"
  Delete "$INSTDIR\Ookii.Dialogs.dll"
  RMDir "$INSTDIR"
  
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" ; Remove Windows Programs & Features integration (uninstall info)
  
  Delete "$SMPROGRAMS\WalkmanOSS\${ProgramName}.lnk" ; Remove Start Menu Shortcuts & Folder
  Delete "$SMPROGRAMS\WalkmanOSS\Uninstall ${ProgramName}.lnk"
  RMDir "$SMPROGRAMS\WalkmanOSS"
  
  Delete "$DESKTOP\${ProgramName}.lnk"     ; Remove Desktop      Shortcut
  Delete "$QUICKLAUNCH\${ProgramName}.lnk" ; Remove Quick Launch Shortcut
SectionEnd

; Uninstaller Functions

Function un.onInit
  SetShellVarContext all
  SetAutoClose true
FunctionEnd

Function un.onUninstFailed
  MessageBox MB_OK "Uninstall Cancelled"
FunctionEnd

Function un.onUninstSuccess
  MessageBox MB_OK "Uninstall Completed"
FunctionEnd
