Master: [![Build Status](https://exit.visualstudio.com/xFunc/_apis/build/status/sys27.xFunc?branchName=master)](https://exit.visualstudio.com/xFunc/_build/latest?definitionId=4&branchName=master) [![codecov](https://codecov.io/gh/sys27/xFunc/branch/master/graph/badge.svg)](https://codecov.io/gh/sys27/xFunc)  
Dev: [![Build Status](https://exit.visualstudio.com/xFunc/_apis/build/status/sys27.xFunc?branchName=dev)](https://exit.visualstudio.com/xFunc/_build/latest?definitionId=4&branchName=dev) [![codecov](https://codecov.io/gh/sys27/xFunc/branch/dev/graph/badge.svg)](https://codecov.io/gh/sys27/xFunc)  
xFunc.Maths: [![NuGet](https://img.shields.io/nuget/v/xFunc.Maths.svg)](https://www.nuget.org/packages/xFunc.Maths) [![Downloads](https://img.shields.io/nuget/dt/xFunc.Maths.svg)](https://www.nuget.org/packages/xFunc.Maths)  
xFunc.DotnetTool: [![NuGet](https://img.shields.io/nuget/v/xFunc.DotnetTool.svg)](https://www.nuget.org/packages/xFunc.DotnetTool)

xFunc
=====

xFunc is a simple and easy to use application that allows you to build mathematical and logical expressions. It's written on C#. The library includes well-documented code that allows developers to parse strings to expression tree, to analyze (derivate, simplify) expressions by using lexer, parser and etc.

xFunc is a small-sized and portable application that you can use to create complex mathematical expressions which will be automatically computed. It can be used by teachers and students alike.

## Features:

* Calculating expressions ([supported functions and operations](https://github.com/sys27/xFunc/wiki/Supported-functions-and-operations));
* Supporting measures of angles;
* Derivative and simplifying expressions;
* Plotting graphs;
* Truth tables;
* Supported Framework: .NET Standard 2.1+;

## Usage

The main class of xFunc library is `Processor`.

### Processor 

It allows you to:

**Parse:**

```csharp
var processor = new Processor();
processor.Parse("2 + 2"); // will return the expression tree
```

_Note: The `Parse` method won't simplify expression automatically, it will return the complete representation of provided string expression._

**Solve:**

This method will parse string expression (like `Parse` method) and then calculate it (returns object which implements `IResult` interface). 
 
There is two overloads of this method (common and generic). The common returns just `IResult` (you can access result by `Result` property). The generic allows to return specific implementation of `IResult` (eg. `NumberResult`).

```csharp
var processor = new Processor();
processor.Solve("2 + 2").Result; // will return 4.0 (object)
```

```csharp
var processor = new Processor();
processor.Solve<NumberResult>("2 + 2"); // will return 4.0 (double)
```

_Note: The `Solve` method automatically simplify expression, to control this behavior you can use `simplify` argument. It's useful for differentiation, because it will eliminate unnecessary expression nodes._

**Simplify:**

```csharp
var processor = new Processor();
processor.Simplify("arcsin(sin(x))"); // will return simplified expression = "x"
```

_Detailed [simplification rules](https://github.com/sys27/xFunc/wiki/Simplification-rules)_

**Differentiate:**

```csharp
var processor = new Processor();
processor.Differentiate("2x"); // will return "2"
```

You can specified variable (default is "x") of differentiation:

```csharp
var processor = new Processor();
processor.Differentiate("2y", new Variable("y")); // will return "2"
processor.Differentiate("2x + sin(y)", new Variable("x")); // will return "2"
```

## Performance

### Processor

Version | Method |         Mean |    Allocated |
-------:|------- |-------------:|-------------:|
  3.7.1 |  Parse |     159.7 us |     62.28 KB |
  4.0.0 |  Parse | **23.51 us** |  **4.27 KB** |
  3.7.1 |  Solve |     221.1 us |     94.68 KB |
  4.0.0 |  Solve | **36.58 us** | **10.47 KB** |

[More details](https://github.com/sys27/xFunc/wiki/Performance-Comparison)

## Bug Tracker

Please, if you have a bug or a feature request, [create](https://github.com/sys27/xFunc/issues) a new issue. Before creating any issue, please search for existing issues.

## License

xFunc is released under [Apache 2.0 License](http://www.apache.org/licenses/LICENSE-2.0.html).

## Thanks

[@RonnyCSHARP](https://github.com/ronnycsharp)

[Fluent.Ribbon](https://github.com/fluentribbon/Fluent.Ribbon)  
[Azure Pipelines](https://azure.microsoft.com/en-us/services/devops/pipelines/)  
[Coverlet](https://github.com/coverlet-coverage/coverlet)  
[ReportGenerator](https://github.com/danielpalme/ReportGenerator)  
[xUnit](https://github.com/xunit/xunit)  
[Moq](https://github.com/moq/moq4)

## More:

* [NuGet](https://nuget.org/packages?q=xFunc)
