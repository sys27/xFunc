Master: [![Build Status](https://dev.azure.com/exit/xFunc/_apis/build/status/sys27.xFunc?branchName=master)](https://exit.visualstudio.com/xFunc/_build/latest?definitionId=4&branchName=master) [![codecov](https://codecov.io/gh/sys27/xFunc/branch/master/graph/badge.svg)](https://codecov.io/gh/sys27/xFunc)  
Dev: [![Build Status](https://dev.azure.com/exit/xFunc/_apis/build/status/sys27.xFunc?branchName=dev)](https://exit.visualstudio.com/xFunc/_build/latest?definitionId=4&branchName=dev) [![codecov](https://codecov.io/gh/sys27/xFunc/branch/dev/graph/badge.svg)](https://codecov.io/gh/sys27/xFunc)  
xFunc.Maths: [![NuGet](https://img.shields.io/nuget/v/xFunc.Maths.svg)](https://www.nuget.org/packages/xFunc.Maths) [![Downloads](https://img.shields.io/nuget/dt/xFunc.Maths.svg)](https://www.nuget.org/packages/xFunc.Maths)  
xFunc.Cli: [![NuGet](https://img.shields.io/nuget/v/xFunc.Cli.svg)](https://www.nuget.org/packages/xFunc.Cli)

xFunc
=====

xFunc is a user-friendly C# library for constructing and manipulating mathematical and logical expressions. This lightweight library empowers developers to effortlessly parse strings into expression trees, analyze expressions (including derivatives and simplifications), and perform various mathematical operations.

xFunc is a versatile tool suitable for both educators and students, allowing the creation of complex mathematical expressions.

Note: The WPF application (xFunc UI) was migrated to a separate repository [xFunc.UI](https://github.com/sys27/xFunc.UI).

## Features:

* Evaluate expressions (see all [supported functions and operators](https://sys27.github.io/xFunc/articles/supported-functions-and-operations.html)):
  * basic arithmetic operators: `+`, `-`, `*`, `/`, `^`, etc.
  * trigonometric functions: `sin(x)`, `cos(x)`, `tan(x)`, etc.
  * inverse trigonometric functions: `arcsin(x)`, `arccos(x)`, `arctan(x)`, etc.
  * hyperbolic functions:`sinh(x)`, `cosh(x)`, `tanh(x)`, etc.
  * inverse hyperbolic functions: `arsinh(x)`, `arcosh(x)`, `artanh(x)`, etc.
  * complex numbers: `1 + 2i`, `im(x)`, `re(x)`, etc.
  * vector/matrix: `{1, 2, 3}`, `{{1, 2}, {3, 4}}`, etc.
  * statistical: `sum(a, b, c)`, `avg(a, b, c)`, etc.
  * bitwise operators: `x or y`, `x and y`, etc.
  * units: `90 'deg'` - angles, `10 'm'` - length, `10 'min'` - time, etc.
  * lambdas: `f := (x) => x ^ 2`, `((x) => x ^ 2)(3)`, `curry(f)`, etc.
* Derivative calculation; 
* Simplify expressions ([simplification rules](https://sys27.github.io/xFunc/articles/simplification-rules.html));
* Supported Framework: .NET 6+;

## Usage

The main class of xFunc library is `Processor`. Detailed documentation is located on [GitHub Pages](https://sys27.github.io/xFunc/articles/get-started.html).

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

This method parses string expression (like the `Parse` method) and then calculates it (returns object which implements the `Result` abstract class).

```csharp
var processor = new Processor();
var result = processor.Solve("2 + 2");

Console.WriteLine(result); // 4.0
```

The `result` variable will contain `4` (as `NumberResult` which is the implementation of the `Result` class). It is a hand-made implementation of the discriminated union. The `Result` class provides the abstraction (root class) for DU, whereas implementation for each possible return type is dedicated to the appropriate nested result class. Check documentation for more examples: [Result](https://sys27.github.io/xFunc/api/xFunc.Maths.Results.Result.html), [Processor](https://sys27.github.io/xFunc/api/xFunc.Maths.Processor.html).

If your expression has any parameter, you need to assign a value to it (otherwise xFunc will throw an exception), because `Processor` has a build-in collection of parameters and user functions, you don't need to use `ExpressionParameters` directly:

```csharp
processor.Solve("x := 10");

// or explicitly through Parameters property

processor.Parameters.Variables.Add("x", 10);
```

**Simplify:**

```csharp
var processor = new Processor();

processor.Solve("simplify((x) => arcsin(sin(x)))");
// or
processor.Simplify("arcsin(sin(x))");
// will return simplified expression = "x"
```

**Differentiate:**

```csharp
var processor = new Processor();

processor.Solve("deriv((x) => 2x)");
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
|--------:|--------|--------------:|----------:|
|   3.7.3 | Parse  |   39,567.9 ns |   63736 B |
|   4.3.0 | Parse  |  10,434.04 ns |    4848 B |
|   3.7.3 | Solve  |   55,260.0 ns |   96920 B |
|   4.3.0 | Solve  |  15,683.42 ns |    9552 B |

[More details](https://sys27.github.io/xFunc/articles/performance-comparison.html)

## License

xFunc is released under MIT License.

## Thanks

[Azure Pipelines](https://azure.microsoft.com/en-us/services/devops/pipelines/)  
[Coverlet](https://github.com/coverlet-coverage/coverlet)  
[ReportGenerator](https://github.com/danielpalme/ReportGenerator)  
[NUnit](https://github.com/nunit/nunit)  
[NSubstitute](https://github.com/nsubstitute/NSubstitute)  
[docfx](https://github.com/dotnet/docfx)  
[BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)  
