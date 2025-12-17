@echo off
echo Building Asus Fan Control...
echo.

REM Change to the project directory
cd /d "%~dp0"

REM Find MSBuild
set MSBUILD_PATH=""
if exist "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
) else if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
) else if exist "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
) else if exist "C:\Program Files\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
)

if %MSBUILD_PATH%=="" (
    echo ERROR: MSBuild not found. Please install Visual Studio or Visual Studio Build Tools.
    echo Download from: https://visualstudio.microsoft.com/downloads/
    pause
    exit /b 1
)

echo Using MSBuild: %MSBUILD_PATH%
echo.

REM Build the solution
echo Building Release x64...
%MSBUILD_PATH% AsusFanControl.sln -p:Configuration=Release -p:Platform=x64 -m

if %ERRORLEVEL% equ 0 (
    echo.
    echo ============================================
    echo BUILD SUCCESS!
    echo ============================================
    echo.
    echo Your enhanced Asus Fan Control is ready:
    echo GUI App: bin\x64\Release\AsusFanControlGUI.exe
    echo CLI App: bin\x64\Release\AsusFanControl.exe
    echo.
    echo New Features Added:
    echo - Automatic temperature-based fan control
    echo - Temperature threshold setting (30-100°C)
    echo - Smart hysteresis (5°C buffer)
    echo - Settings persistence
    echo.
) else (
    echo.
    echo ============================================
    echo BUILD FAILED!
    echo ============================================
    echo Please check the error messages above.
)

echo.
pause
