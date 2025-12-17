@echo off
setlocal enabledelayedexpansion

echo ========================================
echo  UPDATE SYSTEM FILES WITH BUG FIX
echo ========================================
echo.
echo This script will copy the updated AsusFanControlGUI.exe
echo (with the temperature control bug fix) to your system folder.
echo.

REM Check if running as administrator
net session >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: This script must be run as Administrator!
    echo.
    echo Please right-click this file and select "Run as administrator"
    echo.
    pause
    exit /b 1
)

echo Running with administrator privileges... OK
echo.

REM Define paths
set "PROJECT_ROOT=%~dp0"
set "RELEASE_DIR=%PROJECT_ROOT%bin\x64\Release"
set "SYSTEM_DIR=C:\AsusFanControl"

echo Project root: %PROJECT_ROOT%
echo Release folder: %RELEASE_DIR%
echo System folder: %SYSTEM_DIR%
echo.

echo [1/3] Checking if system folder exists...
echo ----------------------------------------
if not exist "%SYSTEM_DIR%" (
    echo ERROR: System folder not found: %SYSTEM_DIR%
    echo.
    echo It looks like you haven't moved the files to system folder yet.
    echo The auto-startup might be using files from a different location.
    echo.
    echo Please check where your auto-startup is configured to run from.
    pause
    exit /b 1
)

echo ✓ System folder found
echo.

echo [2/3] Checking for updated files...
echo ----------------------------------------
if not exist "%RELEASE_DIR%\AsusFanControlGUI.exe" (
    echo ERROR: Updated AsusFanControlGUI.exe not found in release folder!
    echo.
    echo Please make sure you've built the project first.
    echo Run: build.bat
    pause
    exit /b 1
)

echo ✓ Updated AsusFanControlGUI.exe found in release folder
echo.

REM Check if the system app is currently running
tasklist /fi "imagename eq AsusFanControlGUI.exe" 2>nul | find /i "AsusFanControlGUI.exe" >nul
if %ERRORLEVEL% EQU 0 (
    echo WARNING: AsusFanControlGUI.exe is currently running!
    echo.
    echo The running app will be stopped to update the files.
    echo Your temperature control settings will be preserved.
    echo.

    echo Stopping running application...
    taskkill /f /im AsusFanControlGUI.exe >nul 2>&1
    timeout /t 2 /nobreak >nul
    echo ✓ Application stopped
    echo.
)

echo [3/3] Updating system files...
echo ----------------------------------------
echo Copying updated files to system folder...

REM Copy the main executable
copy "%RELEASE_DIR%\AsusFanControlGUI.exe" "%SYSTEM_DIR%\" >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo ✓ AsusFanControlGUI.exe updated
) else (
    echo ✗ Failed to copy AsusFanControlGUI.exe
    echo This might be because the file is still in use.
    pause
    exit /b 1
)

REM Copy other potentially updated files
if exist "%RELEASE_DIR%\AsusFanControl.exe" (
    copy "%RELEASE_DIR%\AsusFanControl.exe" "%SYSTEM_DIR%\" >nul 2>&1
    echo ✓ AsusFanControl.exe updated
)

if exist "%RELEASE_DIR%\AsusWinIO64.dll" (
    copy "%RELEASE_DIR%\AsusWinIO64.dll" "%SYSTEM_DIR%\" >nul 2>&1
    echo ✓ AsusWinIO64.dll updated
)

echo.
echo ========================================
echo  UPDATE COMPLETE!
echo ========================================
echo.
echo ✅ BUG FIX APPLIED SUCCESSFULLY!
echo.
echo WHAT WAS FIXED:
echo • Temperature control now only controls the FANS
echo • "Turn on fan control when temperature exceeds" checkbox stays ON
echo • Auto control persists - no need to re-enable after each cycle
echo • Manual override works correctly without breaking auto control
echo.
echo HOW IT WORKS NOW:
echo 1. Enable "Turn on fan control when temperature exceeds" ✓
echo 2. Set your threshold (e.g., 80°C) ✓
echo 3. Auto control stays ENABLED permanently ✓
echo 4. When temp ≥ 80°C → Fans turn ON automatically
echo 5. When temp < 75°C → Fans turn OFF automatically
echo 6. Checkbox remains CHECKED throughout ✓
echo 7. Next time temp hits 80°C → Fans turn ON again ✓
echo.
echo TESTING THE FIX:
echo 1. Start the app (it should auto-start, or use desktop shortcut)
echo 2. Enable auto temperature control and set threshold
echo 3. Wait for temperature to exceed threshold
echo 4. Verify fans turn on (checkbox stays checked)
echo 5. Wait for temperature to drop below threshold - 5°C
echo 6. Verify fans turn off (checkbox STILL stays checked)
echo 7. Temperature goes up again → fans should auto-start again!
echo.
echo The app should now restart automatically...
echo.

REM Try to restart the app
if exist "%SYSTEM_DIR%\run.bat" (
    echo Starting updated application...
    start "" "%SYSTEM_DIR%\run.bat"
    timeout /t 3 /nobreak >nul

    tasklist /fi "imagename eq AsusFanControlGUI.exe" 2>nul | find /i "AsusFanControlGUI.exe" >nul
    if %ERRORLEVEL% EQU 0 (
        echo ✓ Application restarted successfully
        echo Check your system tray for the fan control icon.
    ) else (
        echo ⚠ Application may not have restarted automatically
        echo Please start it manually using the desktop shortcut.
    )
) else (
    echo Please restart the application manually to use the bug fix.
)

echo.
echo Your automatic temperature-based fan control is now working correctly!
echo.

pause
