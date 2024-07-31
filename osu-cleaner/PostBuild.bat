REM Move is currently only for build, as moving the files seems to prevent the program from running properly...

REM Get current directory:
echo Current directory: %cd%
set origDir=%cd%

REM SET VARIABLES
REM If SIGNTOOL environment variable is not set then try setting it to a known location
if "%SIGNTOOL%"=="" set SIGNTOOL=%ProgramFiles(x86)%\Windows Kits\10\bin\10.0.19041.0\x64\signtool.exe
REM Check to see if the signtool utility is missing
if exist "%SIGNTOOL%" goto ST
    REM Give error that SIGNTOOL environment variable needs to be set
    echo "Must set environment variable SIGNTOOL to full path for signtool.exe code signing utility"
    echo Location is of the form "C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64\bin\signtool.exe"
    exit -1
:ST

REM Set NSIS path for building the installer
if "%NSIS%"=="" set NSIS=%ProgramFiles(x86)%\NSIS\makensis.exe
if exist "%NSIS%" goto NS
    REM Give error that NSIS environment variable needs to be set
    echo "Must set environment variable NSIS to full path for makensis.exe"
    echo Location is of the form "C:\Program Files (x86)\NSIS\makensis.exe"
    exit -1
:NS


REM Set 7-Zip path for compressing built files
if "%zip%"=="" set zip=C:\Program Files\7-Zip\7z.exe
if exist "%zip%" goto ZJ
    REM Give error that NSIS environment variable needs to be set
    echo "Must set environment variable 7-Zip to full path for 7z.exe"
    echo Location is of the form "C:\Program Files\7-Zip\7z.exe"
    exit -1
:ZJ

cd %origDir%\bin\Release\
ECHO -----------------------------------
ECHO Signing files for AnyCPU Release in Visual Studio
ECHO -----------------------------------

REM "%SIGNTOOL%" sign /tr http://timestamp.sectigo.com?td=sha256 /td SHA256 /fd SHA256 /a TcNo-osu-cleaner.exe
REM Have skipped signing for now. For more details: https://github.com/TCNOco/TcNo-Acc-Switcher/commit/c283aa2bae4deaed93e7757f188813560ef3b02e

cd ../
REN "Release" "TcNo-osu-Cleaner"

REM Compress files
echo Creating .7z archive
"%zip%" a -t7z -mmt24 -mx9  "TcNo-osu-Cleaner.7z" ".\TcNo-osu-Cleaner"
echo Done!

cd %origDir%