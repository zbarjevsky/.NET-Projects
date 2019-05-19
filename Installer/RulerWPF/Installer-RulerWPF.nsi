# define name of installer
OutFile "RulerWPF-installer.exe"
 
# define installation directory
InstallDir $DESKTOP
 
# For removing Start Menu shortcut in Windows 7
RequestExecutionLevel user
 
# start default section
Section
 
    # set the installation directory as the destination for the following actions
    SetOutPath $INSTDIR
 
    # create the uninstaller
    WriteUninstaller "$INSTDIR\RulerWPF-uninstall.exe"
 
    # create a shortcut named "new shortcut" in the start menu programs directory
    # point the new shortcut at the program uninstaller
    CreateShortCut "$SMPROGRAMS\RulerWPF.lnk" "$INSTDIR\RulerWPF-uninstall.exe"
SectionEnd
 
# uninstaller section start
Section "uninstall"
 
    # first, delete the uninstaller
    Delete "$INSTDIR\RulerWPF-uninstall.exe"
 
    # second, remove the link from the start menu
    Delete "$SMPROGRAMS\RulerWPF.lnk"
 
# uninstaller section end
SectionEnd