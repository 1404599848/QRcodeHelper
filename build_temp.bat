@echo off
cd /d D:\MyGithubProject\QRcodeHelper
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe" /p:Configuration=Debug /p:Platform=x86 QRcodeHelper.sln
exit /b %ERRORLEVEL%
