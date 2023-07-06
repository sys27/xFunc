Master: [![Build Status](https://dev.azure.com/exit/xFunc/_apis/build/status/sys27.xFunc?branchName=master)](https://exit.visualstudio.com/xFunc/_build/latest?definitionId=4&branchName=master) [![codecov](https://codecov.io/gh/sys27/xFunc/branch/master/graph/badge.svg)](https://codecov.io/gh/sys27/xFunc)  
Dev: [![Build Status](https://dev.azure.com/exit/xFunc/_apis/build/status/sys27.xFunc?branchName=dev)](https://exit.visualstudio.com/xFunc/_build/latest?definitionId=4&branchName=dev) [![codecov](https://codecov.io/gh/sys27/xFunc/branch/dev/graph/badge.svg)](https://codecov.io/gh/sys27/xFunc)  
xFunc.Maths: [![NuGet](https://img.shields.io/nuget/v/xFunc.Maths.svg)](https://www.nuget.org/packages/xFunc.Maths) [![Downloads](https://img.shields.io/nuget/dt/xFunc.Maths.svg)](https://www.nuget.org/packages/xFunc.Maths)  
xFunc.DotnetTool: [![NuGet](https://img.shields.io/nuget/v/xFunc.DotnetTool.svg)](https://www.nuget.org/packages/xFunc.DotnetTool)

xFunc
=====

xFunc is a simple and easy to use application that allows you to build mathematical and logical expressions. It's written on C#. The library includes well-documented code that allows developers to parse strings to expression tree, to analyze (derivate, simplify) expressions by using lexer, parser and etc.

xFunc is a small-sized and portable application that you can use to create complex mathematical expressions which will be automatically computed. It can be used by teachers and students alike.

Note: The WPF application (xFunc UI) was migrated to a separate repository [xFunc.UI](https://github.com/sys27/xFunc.UI).

## Features:

* Calculating expressions ([supported functions and operations](https://github.com/sys27/xFunc/wiki/Supported-functions-and-operations));
* Supporting measures of angles;
* Derivative and simplifying expressions;
* Plotting graphs;
* Truth tables;
* Supported Framework: .NET 6+;

## Usage

The main class of xFunc library is `Processor`.

### Processor

It allows you to:

**Parse:**

```csharp
var processor = new Processor();
var exp = processor.Parse("2 + x"); 

// 'exp' will contain the expression tree for later use
// you can calculate it or process it by analyzers (Differentiator, Simplifier, etc.)

// 'exp' has a parameter
// we should provide a value for variable 'x'
var parameters = new ExpressionParameters
{
    new Parameter("x", 10)
};
var result = exp.Execute(parameters);

// result will be equal to 12
```

_Note: The `Parse` method won't simplify the expression automatically, it will return the complete representation of provided string expression._

**Solve:**

This method parses string expression (like the `Parse` method) and then calculates it (returns object which implements the `IResult` interface).

There are two overloads of this method (common and generic). The "common" returns just `IResult` (you can access result by `Result` property). The generic allows to return specific implementation of `IResult` (eg. `NumberResult`).

```csharp
var processor = new Processor();
processor.Solve<NumberResult>("2 + 2"); // will return 4.0 (double)

// or

processor.Solve("2 + 2").Result; // will return 4.0 (object)
```

If your expression has any parameter, you need to assign a value to it (otherwise xFunc will throw an exception), because `Processor` has a build-in collection of parameters and user functions, you don't need to use `ExpressionParameters` directly:

```csharp
processor.Solve("x := 10");

// or explicitly through Parameters property

processor.Parameters.Variables.Add("x", 10);
```

_Note: The `Solve` method automatically simplifies expression, to control this behavior you can use the `simplify` argument. It's useful for differentiation because it will eliminate unnecessary expression nodes._

**Simplify:**

```csharp
var processor = new Processor();

processor.Solve<ExpressionResult>("simplify(arcsin(sin(x)))");
// or
processor.Simplify("arcsin(sin(x))");
// will return simplified expression = "x"
```

_Detailed [simplification rules](https://github.com/sys27/xFunc/wiki/Simplification-rules)_

**Differentiate:**

```csharp
var processor = new Processor();

processor.Solve<ExpressionResult>("deriv(2x)");
// or
processor.Differentiate("2x");
// will return "2"
```

You can specify variable (default is "x") of differentiation:

```csharp
var processor = new Processor();
processor.Differentiate("2y", Variable.Y); // will return "2"
processor.Differentiate("2x + sin(y)", new Variable("x")); // will return "2"
```

## Performance

### Processor

| Version | Method |          Mean | Allocated |
| ------: | ------ | ------------: | --------: |
|   3.7.3 | Parse  |   39,567.9 ns |   63736 B |
|   4.0.0 | Parse  |  9,128.180 ns |    4760 B |
|   4.2.0 | Parse  |  14,855.62 ns |    4872 B |
|   3.7.3 | Solve  |   55,260.0 ns |   96920 B |
|   4.0.0 | Solve  | 15,319.497 ns |   10672 B |
|   4.2.0 | Solve  |  22,074.89 ns |   9936 B |

[More details](https://github.com/sys27/xFunc/wiki/Performance-Comparison)

## Bug Tracker

Please, if you have a bug or a feature request, [create](https://github.com/sys27/xFunc/issues) a new issue. Before creating any issue, please search for existing issues.

## License

xFunc is released under MIT License.

## Thanks

[@RonnyCSHARP](https://github.com/ronnycsharp)

[Azure Pipelines](https://azure.microsoft.com/en-us/services/devops/pipelines/)  
[Coverlet](https://github.com/coverlet-coverage/coverlet)  
[ReportGenerator](https://github.com/danielpalme/ReportGenerator)  
[xUnit](https://github.com/xunit/xunit)  
[Moq](https://github.com/moq/moq4)
