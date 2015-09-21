@echo off

set t=%1
if "%1"=="" set t=Build

echo %t% xFunc for .Net 4.0 ...
msbuild "xFunc (Libs for .Net 4.0).sln" /nologo /t:%t% /m /clp:WarningsOnly;ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto end
if errorlevel 0 echo %t% completed!

echo %t% xFunc for Portable ...
msbuild "xFunc (Libs for Portable).sln" /nologo /t:%t% /m /clp:WarningsOnly;ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto end
if errorlevel 0 echo %t% completed! 

:end

echo Remove temp files.
rmdir "xFunc/obj" /s /q
rmdir "xFunc.Maths/obj" /s /q
rmdir "xFunc.Tests/obj" /s /q
rmdir "xFunc.UnitConverters/obj" /s /q

pause