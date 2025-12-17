@echo off
setlocal enabledelayedexpansion

echo ========================================
echo  ASUS FAN CONTROL - RELIABLE AUTO-STARTUP
echo ========================================
echo.
echo This will set up reliable auto-startup using Windows Startup folder.
echo The app will start when you log in and show a UAC prompt (normal behavior).
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

REM Find the app files - check multiple possible locations
set "APP_DIR="
set "FOUND_LOCATION="

REM Check if we're in the release folder
if exist "%~dp0AsusFanControlGUI.exe" (
    set "APP_DIR=%~dp0"
    set "FOUND_LOCATION=Current directory"
) else if exist "C:\AsusFanControl\AsusFanControlGUI.exe" (
    set "APP_DIR=C:\AsusFanControl\"
    set "FOUND_LOCATION=System folder"
) else if exist "%~dp0bin\x64\Release\AsusFanControlGUI.exe" (
    set "APP_DIR=%~dp0bin\x64\Release\"
    set "FOUND_LOCATION=Project Release folder"
) else (
    echo ERROR: Cannot find AsusFanControlGUI.exe in expected locations!
    echo.
    echo Searched in:
    echo - Current directory: %~dp0
    echo - System folder: C:\AsusFanControl\
    echo - Project folder: %~dp0bin\x64\Release\
    echo.
    echo Please make sure AsusFanControlGUI.exe exists in one of these locations.
    pause
    exit /b 1
)

echo ✓ Found app files in: %FOUND_LOCATION%
echo   Location: %APP_DIR%
echo.

REM Verify all required files exist
echo [1/4] Checking required files...
echo ----------------------------------------
set "FILES_OK=1"

if exist "%APP_DIR%AsusFanControlGUI.exe" (
    echo ✓ AsusFanControlGUI.exe found
) else (
    echo ✗ AsusFanControlGUI.exe missing
    set "FILES_OK=0"
)

if exist "%APP_DIR%AsusWinIO64.dll" (
    echo ✓ AsusWinIO64.dll found
) else (
    echo ✗ AsusWinIO64.dll missing
    set "FILES_OK=0"
)

if exist "%APP_DIR%PsExec.exe" (
    echo ✓ PsExec.exe found
) else (
    echo ✗ PsExec.exe missing
    set "FILES_OK=0"
)

if %FILES_OK% EQU 0 (
    echo.
    echo ERROR: Required files are missing!
    pause
    exit /b 1
)

echo.

echo [2/4] Removing old startup methods...
echo ----------------------------------------

REM Remove Task Scheduler entries
schtasks /query /tn "AsusFanControlAutoStart" >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo Removing Task Scheduler entry...
    schtasks /delete /tn "AsusFanControlAutoStart" /f >nul
    if %ERRORLEVEL% EQU 0 (
        echo ✓ Task Scheduler entry removed
    ) else (
        echo ⚠ Could not remove Task Scheduler entry
    )
) else (
    echo ✓ No Task Scheduler entry to remove
)

schtasks /query /tn "AsusFanControlTest" >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo Removing test Task Scheduler entry...
    schtasks /delete /tn "AsusFanControlTest" /f >nul
)

REM Remove any existing startup folder entries
set "STARTUP_FOLDER=%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup"
if exist "%STARTUP_FOLDER%\AsusFanControl_Startup.bat" (
    echo Removing existing startup folder entry...
    del "%STARTUP_FOLDER%\AsusFanControl_Startup.bat" >nul 2>&1
    echo ✓ Old startup folder entry removed
)

echo.

echo [3/4] Creating reliable startup entry...
echo ----------------------------------------

REM Create the startup script
set "STARTUP_SCRIPT=%STARTUP_FOLDER%\AsusFanControl_AutoStart.bat"

echo Creating startup script: %STARTUP_SCRIPT%
echo.

echo @echo off > "%STARTUP_SCRIPT%"
echo REM AsusFanControl Reliable Auto-Startup Script >> "%STARTUP_SCRIPT%"
echo REM Generated on %DATE% at %TIME% >> "%STARTUP_SCRIPT%"
echo. >> "%STARTUP_SCRIPT%"
echo REM Wait for system to fully load >> "%STARTUP_SCRIPT%"
echo timeout /t 15 /nobreak ^>nul >> "%STARTUP_SCRIPT%"
echo. >> "%STARTUP_SCRIPT%"
echo REM Check if already running to prevent duplicates >> "%STARTUP_SCRIPT%"
echo tasklist /fi "imagename eq AsusFanControlGUI.exe" 2^>nul ^| find /i "AsusFanControlGUI.exe" ^>nul >> "%STARTUP_SCRIPT%"
echo if %%ERRORLEVEL%% EQU 0 ( >> "%STARTUP_SCRIPT%"
echo     REM App is already running >> "%STARTUP_SCRIPT%"
echo     exit /b 0 >> "%STARTUP_SCRIPT%"
echo ^) >> "%STARTUP_SCRIPT%"
echo. >> "%STARTUP_SCRIPT%"
echo REM Start the app with elevated privileges >> "%STARTUP_SCRIPT%"
echo cd /d "%APP_DIR%" >> "%STARTUP_SCRIPT%"
echo powershell -WindowStyle Hidden -Command "Start-Process -FilePath '%APP_DIR%run.bat' -Verb RunAs -WindowStyle Hidden" >> "%STARTUP_SCRIPT%"

REM Verify the script was created successfully
if exist "%STARTUP_SCRIPT%" (
    echo ✓ Startup script created successfully
    echo.
    echo Contents of startup script:
    echo ================================
    type "%STARTUP_SCRIPT%"
    echo ================================
) else (
    echo ✗ Failed to create startup script
    echo Check if you have write permissions to the startup folder
    pause
    exit /b 1
)

echo.

echo [4/4] Creating desktop shortcut and testing...
echo ----------------------------------------

REM Create desktop shortcut
echo Creating desktop shortcut...
set "SHORTCUT_PATH=%USERPROFILE%\Desktop\Asus Fan Control.lnk"

powershell -Command "& {$WshShell = New-Object -comObject WScript.Shell; $Shortcut = $WshShell.CreateShortcut('%SHORTCUT_PATH%'); $Shortcut.TargetPath = '%APP_DIR%run.bat'; $Shortcut.WorkingDirectory = '%APP_DIR%'; $Shortcut.IconLocation = '%APP_DIR%AsusFanControlGUI.exe,0'; $Shortcut.Description = 'Asus Fan Control with Auto Temperature Management'; $Shortcut.Save()}" >nul 2>&1

if exist "%SHORTCUT_PATH%" (
    echo ✓ Desktop shortcut created
) else (
    echo ⚠ Desktop shortcut creation failed
)

REM Test the startup script
echo.
echo Testing the startup script...
echo This will start the app now to verify it works...

call "%STARTUP_SCRIPT%"

REM Wait and check if app started
timeout /t 5 /nobreak >nul
tasklist /fi "imagename eq AsusFanControlGUI.exe" 2>nul | find /i "AsusFanControlGUI.exe" >nul
if %ERRORLEVEL% EQU 0 (
    echo ✓ SUCCESS: App started successfully!
    echo The app should now be running in your system tray.
) else (
    echo ⚠ App may not have started - check manually
)

echo.
echo ========================================
echo  SETUP COMPLETE!
echo ========================================
echo.
echo ✅ AUTO-STARTUP CONFIGURED SUCCESSFULLY!
echo.
echo How it works:
echo • App will start automatically when you log into Windows
echo • You'll see a UAC prompt asking for admin privileges (click "Yes")
echo • App will start minimized to system tray after 15 seconds
echo • Prevents duplicate instances if already running
echo.
echo What happens at startup:
echo 1. Windows loads your user profile
echo 2. Startup folder script executes
echo 3. UAC prompt appears (click "Yes")
echo 4. App starts with admin privileges
echo 5. App minimizes to system tray
echo 6. Automatic temperature control begins
echo.
echo Files location: %APP_DIR%
echo Startup script: %STARTUP_SCRIPT%
echo.
echo ========================================
echo  CONFIGURATION STEPS
echo ========================================
echo.
echo IMPORTANT: Configure your temperature settings!
echo.
echo 1. Use the desktop shortcut "Asus Fan Control" or check system tray
echo 2. Configure these settings:
echo    ✅ "Turn on fan control when temperature exceeds"
echo    🌡️  Set your threshold (example: 80°C)
echo    ✅ "Auto refresh stats"
echo    ✅ "Minimize to tray on close"
echo.
echo 3. Close the app (it will minimize to tray)
echo 4. Restart your computer to test auto-startup
echo.
echo ========================================
echo  TROUBLESHOOTING
echo ========================================
echo.
echo If auto-startup doesn't work:
echo • Check if UAC is blocking the startup
echo • Manually run: %STARTUP_SCRIPT%
echo • Look in system tray for the fan icon
echo.
echo To disable auto-startup:
echo • Delete: %STARTUP_SCRIPT%
echo • Or disable in: Win+R → shell:startup
echo.
echo To modify startup delay:
echo • Edit the timeout value in: %STARTUP_SCRIPT%
echo • Change "timeout /t 15" to desired seconds
echo.
echo ========================================
echo  ENJOY AUTOMATIC FAN CONTROL!
echo ========================================
echo.
echo Your laptop will now automatically manage its cooling
echo based on CPU temperature. No more manual fan control needed!
echo.
echo The app runs silently in the background and only
echo appears when you need to adjust settings.
echo.

pause
