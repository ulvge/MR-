@echo on
cls
echo ipmi一直发送 ipmi 设置 mac指令
:loop
echo **********  %time% **********
@echo on
.\ipmitool.exe -H 192.168.60.82 -I lanplus -U Administrator -P 'ttytty`12' -C 17 raw 0x30 0x90  0  0xff    1 1       0xEC 0x7C 0x2C 0x0B 0x68 0x31
@echo off
timeout /t 1 /nobreak >nul
goto loop

