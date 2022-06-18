::Start-Process "cmd" "build2 2.2.8"

@echo off

"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe > buildpath.txt

set /p msbuildexe=<buildpath.txt
set apiurl="http://172.17.17.146/nuget/packages"
set apikey="API-VOJSO2ABIM8BOVH4LSHFPSYOBS"

if [%1] == [] goto ENTER_VERSION
if NOT [%1] == [] goto SET_VERSION

:ENTER_VERSION
set /p version=Enter Verion : 
goto BUILD

:SET_VERSION
set version=%1
goto BUILD  

:BUILD
if [%2] == [] goto BUILD_TEST
if [%2] == [test] goto BUILD_TEST
if [%2] == [production] goto BUILD_PRODUCTION

:BUILD_TEST
echo.
echo ************************************ build test ************************************ 
echo.
 "%msbuildexe%" BizPortal.sln /t:Rebuild /p:Configuration=DEBUG /p:RunOctoPack=true /p:OctoPackPackageVersion=%version% /p:OctoPackPublishPackageToFileShare="C:\Deploy\BizPortal" /p:OctoPackReleaseNotesFile=ReleaseNotes.txt /p:OctoPackAppendToPackageId=Test 
NuGet.exe push "C:\Deploy\BizPortal\BizPortal.Test.%version%.nupkg" -ApiKey %apikey% -Source %apiurl% -Timeout 1201
if [%2] == [] goto BUILD_PRODUCTION
goto END

:BUILD_PRODUCTION
pause;
echo.
echo ************************************ build production ******************************
echo.
 "%msbuildexe%" BizPortal.sln /t:Rebuild /p:Configuration=Release /p:RunOctoPack=true /p:OctoPackPackageVersion=%version% /p:OctoPackPublishPackageToFileShare="C:\Deploy\BizPortal" /p:OctoPackReleaseNotesFile=ReleaseNotes.txt /p:OctoPackAppendToPackageId=Production 
NuGet.exe push "C:\Deploy\BizPortal\BizPortal.Production.%version%.nupkg" -ApiKey %apikey% -Source %apiurl% -Timeout 601
goto END

:END
echo.
echo ************************************ success *************************************** 
echo.

pause;
