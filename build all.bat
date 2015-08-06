@echo off

set t=%1
if "%1"=="" set t=Build
echo %t% xFunc for .Net 2.0 ...
msbuild "xFunc (Libs for .Net 2.0).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto end
if errorlevel 0 echo %t% completed!

echo %t% xFunc for .Net 3.0 ...
msbuild "xFunc (Libs for .Net 3.0).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto end
if errorlevel 0 echo %t% completed! 

echo %t% xFunc for .Net 3.5 ...
msbuild "xFunc (Libs for .Net 3.5).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto end
if errorlevel 0 echo %t% completed! 

echo %t% xFunc for .Net 4.0 ...
msbuild "xFunc (Libs for .Net 4.0).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto end
if errorlevel 0 echo %t% completed!

echo %t% xFunc for .Net 4.5 ...
msbuild "xFunc (Libs for .Net 4.5).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto end
if errorlevel 0 echo %t% completed! 

echo %t% xFunc for .Net 4.5.1 ...
msbuild "xFunc (Libs for .Net 4.5.1).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto end
if errorlevel 0 echo %t% completed! 

echo %t% xFunc for Portable ...
msbuild "xFunc (Libs for Portable).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto end
if errorlevel 0 echo %t% completed! 

:end

echo Remove temp files.
rmdir "xFunc/obj" /s /q
rmdir "xFunc.Logics/obj" /s /q
rmdir "xFunc.Maths/obj" /s /q
rmdir "xFunc.Test/obj" /s /q
rmdir "xFunc.UnitConverters/obj" /s /q

pause