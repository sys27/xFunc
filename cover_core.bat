packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -output:coverage.xml -target:"dotnet.exe" -targetargs:"test xFunc.Tests\xFunc.Tests.Core.csproj" -excludebyattribute:*.ExcludeFromCodeCoverage*^ -filter:"+[xFunc.*]* -[xFunc.Tests*]* -[xFunc.*]*.Resource -[xFunc.*]*Exception"
packages\ReportGenerator.2.5.11\tools\ReportGenerator.exe coverage.xml .\coverage
pause