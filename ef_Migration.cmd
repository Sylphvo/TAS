@echo off
setlocal

REM ==== cấu hình ====
set "MIG=%~1"
if "%MIG%"=="" set "MIG=InitialCreate"
set "STARTUP="   REM ví dụ: TAS.Web
set "PROJECT="   REM ví dụ: TAS.Data
REM ==============

set "ARGS="
if not "%STARTUP%"=="" set "ARGS=%ARGS% -s %STARTUP%"
if not "%PROJECT%"=="" set "ARGS=%ARGS% -p %PROJECT%"

echo [check] migrations list...
dotnet ef migrations list %ARGS% > "%TEMP%\_migs.txt" 2>nul
findstr /I /C:"%MIG%" "%TEMP%\_migs.txt" >nul
if %errorlevel%==0 (
  echo [skip] "%MIG%" existed -> update only
) else (
  echo [add] dotnet ef migrations add "%MIG%"
  dotnet ef migrations add "%MIG%" %ARGS% || goto :err
)

echo [update] dotnet ef database update
dotnet ef database update %ARGS% || goto :err

echo [done]
exit /b 0

:err
echo [fail] check connection string, DbContext, -s/-p.
exit /b 1
