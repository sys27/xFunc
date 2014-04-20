@echo off

set t=%1
if "%1"=="" set t=Build
echo %t% xFunc for .Net 2.0 ...
msbuild "xFunc (Libs for .Net 2.0).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto err
if errorlevel 0 echo %t% completed!
pause

echo %t% xFunc for .Net 3.0 ...
msbuild "xFunc (Libs for .Net 3.0).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto err
if errorlevel 0 echo %t% completed! 
pause

echo %t% xFunc for .Net 3.5 ...
msbuild "xFunc (Libs for .Net 3.5).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto err
if errorlevel 0 echo %t% completed! 
pause

echo %t% xFunc for .Net 4.0 ...
msbuild "xFunc (Libs for .Net 4.0).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto err
if errorlevel 0 echo %t% completed!
pause

echo %t% xFunc for .Net 4.5 ...
msbuild "xFunc (Libs for .Net 4.5).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 0 echo %t% completed! 
if errorlevel 1 goto err
pause

echo %t% xFunc for .Net 4.5.1 ...
msbuild "xFunc (Libs for .Net 4.5.1).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto err
if errorlevel 0 echo %t% completed! 
pause

echo %t% xFunc for Portable ...
msbuild "xFunc (Libs for Portable).sln" /nologo /t:%t% /m /clp:ErrorsOnly /p:Configuration=Release
if errorlevel 1 goto err
if errorlevel 0 echo %t% completed! 
pause

goto end

:err
pause

:end