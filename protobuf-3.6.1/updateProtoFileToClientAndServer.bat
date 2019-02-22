echo off
protoc ./appprotobuf.proto --csharp_out=./
set curDir=%~dp0
cd..
cd Client\Assets\Scripts\Sever\MSG
copy %curDir%Appprotobuf.cs %cd%\Appprotobuf.cs
cd %~dp0
cd..
cd Server\MyPokerGameServer\MyPokerGameServer
copy %curDir%Appprotobuf.cs %cd%\Appprotobuf.cs
pause