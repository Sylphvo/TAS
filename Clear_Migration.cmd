@echo off
setlocal enabledelayedexpansion

REM ===== Config =====
REM %1 = MIGRATION NAME, %2 = MODE(last|purge), %3 = STARTUP, %4 = PROJECT
set "MIG=%~1"
set "MODE=%~2"
set "STARTUP=%~3"
set "PROJECT=%~4"
if "%MIG%"=="" set "MIG=InitialCreate"
if "%MODE%"=="" set "MODE=last"
REM ===================

set "ARGS="
if not "%STARTUP%"=="" set ARGS=%ARGS% -s %STARTUP%
if not "%PROJECT%"=="" set ARGS=%ARGS% -p %PROJECT%

echo [list] migrations...
set "LAST=" & set "PREV="
for /f "tokens=*" %%a in ('dotnet ef migrations list %ARGS% 2^>nul') do (
  set "PREV=!LAST!"
  set "LAST=%%a"
)

if /i "%MODE%"=="last" goto do_last
if /i "%MODE%"=="purge" goto do_purge
echo Usage: %~n0 MIG_NAME [last^|purge] [StartupProject] [Project]
exit /b 2

:do_last
if defined LAST (
  if defined PREV (
    echo [down] database -> "!PREV!"
    dotnet ef database update "!PREV!" %ARGS% || goto err
  ) else (
    echo [down] database -> 0
    dotnet ef database update 0 %ARGS% || goto err
  )
  echo [remove] last migration "!LAST!"
  dotnet ef migrations remove %ARGS% || goto err
) else (
  echo [info] no migrations
)
goto add_update

:do_purge
if not defined LAST (
  echo [info] no migrations
  goto add_update
)
:purge_loop
if defined LAST (
  if defined PREV (
    echo [down] database -> "!PREV!"
    dotnet ef database update "!PREV!" %ARGS% || goto err
  ) else (
    echo [down] database -> 0
    dotnet ef database update 0 %ARGS% || goto err
  )
  echo [remove] migration "!LAST!"
  dotnet ef migrations remove %ARGS% || goto err

  set "LAST=" & set "PREV="
  for /f "tokens=*" %%a in ('dotnet ef migrations list %ARGS% 2^>nul') do (
    set "PREV=!LAST!"
    set "LAST=%%a"
  )
  goto purge_loop
)
echo [done]
exit /b 0

:err
echo [fail] check DbContext, connection string, and -s/-p.
exit /b 1
