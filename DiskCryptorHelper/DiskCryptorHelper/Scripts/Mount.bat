echo off
set volume=%1
set mount_point=%2

set "psCommand=powershell -Command "$pword = read-host 'Enter Password' -AsSecureString ; ^
    $BSTR=[System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($pword); ^
        [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)""
for /f "usebackq delims=" %%p in (`%psCommand%`) do set password=%%p

rem https://diskcryptor.net/wiki/Console
"c:\Program Files\dcrypt\dccon.exe" -mount %volume% -mp %mount_point% -p %password%
"c:\Program Files\dcrypt\dccon.exe" -enum
