RMDIR /S /Q ..\binaries
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\..\src\NServiceBusBlade.sln -t:Clean
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\..\src\NServiceBusBlade.sln -p:Configuration=Debug

xcopy /Y /S /I ..\..\src\MvcTurbine.NServiceBus\*.cs MvcTurbine.NServiceBus\src\MvcTurbine.NServiceBus
nuget pack MvcTurbine.NServiceBus/MvcTurbine.NServiceBus.nuspec -symbols
