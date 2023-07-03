:: Check privileges 
net file 1>NUL 2>NUL
if not '%errorlevel%' == '0' (
    powershell Start-Process -FilePath "%0" -ArgumentList "%cd%" -verb runas >NUL 2>&1
)

:: Change directory with passed argument. Processes started with
:: "runas" start with forced C:\Windows\System32 workdir
cd /d %~dp0

@echo off

REM Check if make is installed
where make >nul 2>&1
if %errorlevel% neq 0 (
  echo "make is not installed. Installing..."

  REM Check if Chocolatey is installed
  where choco > nul
  if %errorlevel% equ 0 (
    echo "Chocolatey is not installed. No problemo! Installing Chocolatey..."

    REM Install Chocolatey using PowerShell
    powershell -Command "Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))"

    REM Refresh the environment variables to make Chocolatey available in the current session
    refreshenv
  )

  REM Install make using Chocolatey
  choco install make -y

  echo Choco installed. Pog.  
)

echo All good to go gamers.

