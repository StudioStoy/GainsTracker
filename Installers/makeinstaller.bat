@echo off

:: Ensure the script is running as administrator
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo This script needs to be run as administrator.
    powershell -Command "Start-Process -FilePath '%0' -Verb RunAs"
    exit /b
)

:: Change directory with passed argument. Processes started with
:: "runas" start with forced C:\Windows\System32 workdir
cd /d %~dp0

REM Check if make is installed
where make >nul 2>&1
if %errorlevel% neq 0 (
    echo make is not installed. Installing...

    REM Check if Chocolatey is installed
    where choco >nul 2>&1
    if %errorlevel% neq 0 (
        echo Chocolatey is not installed. No problemo! Installing Chocolatey...

        REM Install Chocolatey using PowerShell
        powershell -Command "Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))"

        REM Pause to ensure installation completes and refresh environment variables
        if exist "%ProgramData%\chocolatey\bin\choco.exe" (
            echo Chocolatey installed successfully. Refreshing environment variables...
            set "PATH=%PATH%;%ProgramData%\chocolatey\bin"
        ) else (
            echo Chocolatey installation failed. Please check for errors.
            exit /b 1
        )
    ) else (
        echo Chocolatey is already installed. Moving on...
    )

    REM Install make using Chocolatey
    choco install make -y
    if %errorlevel% neq 0 (
        echo Failed to install make. Please check for errors.
        exit /b 1
    )
    echo make has been installed successfully.
    echo Choco installed. Pog.
) else (
    echo make is already installed. If it isn't working, close and open a new terminal or restart your IDE.
)

echo All good to go gamers.
pause
