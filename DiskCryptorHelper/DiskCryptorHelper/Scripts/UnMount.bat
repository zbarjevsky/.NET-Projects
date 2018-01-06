echo off
set volume=%1

rem https://diskcryptor.net/wiki/Console
"c:\Program Files\dcrypt\dccon.exe" -unmount %volume% -f -dp
"c:\Program Files\dcrypt\dccon.exe" -enum
