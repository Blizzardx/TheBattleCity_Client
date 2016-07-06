@echo off
rd /s /q gen-csharp
rd /s /q gen-java

FOR %%e IN (*.thrift) DO (
  echo gen java %%e
  call thrift-0.9.1.exe --gen java %%e

  echo gen c# %%e
  call thrift-0.9.1.exe --gen csharp %%e
)

call GBK2Utf-8.exe gen-csharp
call GBK2Utf-8.exe gen-java

set des=..\..\Assets\Script\Moudle\Project\BaseMoudle
rd /s /q %des%\MessageDefine\Message
md %des%\MessageDefine\Message
copy gen-csharp\NetWork\Auto\*.* %des%\MessageDefine\Message

pause