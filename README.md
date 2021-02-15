# FileBrowser [![Build status](https://ci.appveyor.com/api/projects/status/bnuugyjsjrt3rjev)](https://ci.appveyor.com/project/Walkman100/FileBrowser)
.Net File Browser. Aimed at being stable, responsive, functional and customisable.

## Download
Get the latest version [here](https://github.com/Walkman100/FileBrowser/releases), and the latest build from commit
[here](https://ci.appveyor.com/project/Walkman100/FileBrowser/build/artifacts)
(note that these builds are built for the Debug config and so are not optimised)

## Compile requirements
See [CompileInstructions.md](https://github.com/Walkman100/gists/blob/master/CompileInstructions.md)

## Roadmap
See [#1](https://github.com/Walkman100/FileBrowser/issues/1)

## Info

### Context Menu File & Argument format
- Environment Variables are expanded
- If the format is empty, the Full Path is returned for `File`, and Empty for `Argument`.
- `{path}`: Full path to the item
- `{directory}`: Parent directory of the item
- `{name}`: Full name of the item
- `{namenoext}`: Name of the file excluding the extension. Empty string on directories.
- `{fileext}`: Extension of the file. Empty string on directories.
- `{openwith}`: "OpenWith" program path
- `{target}`: Target path for a symlink, or target path for a shortcut (.lnk)
- `{walkmanutils}`: Currently installed WalkmanUtils path

### Context Menu Icon format
- Environment Variables are expanded
- Resource icon paths are allowed, e.g. `%SystemRoot%\System32\imageres.dll,306`
- `{instdir}` is expanded to the path FileBrowser.exe is located

### Context Menu File Filter format
- Filter items are separated with `;`.
- Filters are applied with the VB [`Like`](https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/like-operator#pattern-options) keyword. This accepts most regex, e.g. `*.jpg;*.png;*.png`

### `Shift` alternative functions
- Create File & Create Folder: Hold <kbd>Shift</kbd> to use a SaveFileDialog to enter the new file/folder path instead of an InputBox
- Copy Path (toolbar item only): Hold <kbd>Shift</kbd> to copy path with surrounding double-quotes (`"`)
- Send to Recycle bin: Hold <kbd>Shift</kbd> to delete permanently
- Delete Permanently: Hold <kbd>Shift</kbd> to send to recycle bin
- Copy To & Move To: Hold <kbd>Shift</kbd> to use an InputBox to enter the target instead of a Select Dialog
- Cut & Copy: Hold <kbd>Shift</kbd> to add to clipboard instead of replace
