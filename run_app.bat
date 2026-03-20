@echo off
setlocal
echo ==========================================
echo Terminating any existing HRMS processes...
echo ==========================================
:: Kill the application executable if it's running
taskkill /F /IM "HRMS.exe" /T 2>nul
:: Kill any background dotnet-watch or dotnet processes that might be holding the port
taskkill /F /IM "dotnet.exe" /T 2>nul
taskkill /F /IM "dotnet-watch.exe" /T 2>nul

echo.
echo ==========================================
echo Starting HRMS Application...
echo ==========================================
cd HRMS
dotnet watch run
pause
