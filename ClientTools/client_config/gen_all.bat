@echo off

rd /s /q gen-csharp
rd /s /q gen-java

FOR %%e IN (*.thrift) DO (
  echo gen c# %%e
  call thrift-0.9.1.exe --gen csharp %%e
  call thrift-0.9.1.exe --gen java %%e
)
call GBK2Utf-8.exe gen-csharp

set des=..\..\Assets\Script\\Moudle\BaseMoudle
rd /s /q %des%\Config
md %des%\Config
copy gen-csharp\Config\*.* %des%\Config

md %des%\Config\Table
copy gen-csharp\Config\Table\*.* %des%\Config\Table
pause