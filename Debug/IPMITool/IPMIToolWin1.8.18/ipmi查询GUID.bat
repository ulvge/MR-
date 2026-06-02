@echo off
cls
echo [%date% %time%] ²éÑ¯GUID...
:loop
echo **********  %time% **********
.\ipmitool.exe -H 192.168.60.139 -I lanplus -U Administrator -P 'ttytty`12' -C 17 mc guid
timeout /t 1 /nobreak >nul
goto loop