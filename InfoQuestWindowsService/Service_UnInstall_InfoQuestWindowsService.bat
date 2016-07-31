@ECHO OFF

REM The following directory is for .NET 4.5
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%

echo UnInstalling InfoQuest Windows Service.....
echo --------------------------------------
C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe /u "%~dp0InfoQuestWindowsService.exe"
echo --------------------------------------
pause
echo Done.