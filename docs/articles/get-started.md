# Get Started 

## Installation

```bash
dotnet add package xFunc.Maths
```

## Parse your first expression

```csharp
var processor = new Processor();
var exp = processor.Parse("x ^ 2");
```

*Note: You can use `Parser` instead of `Processor`.*

The `exp` variable will contain the expression tree for `x ^ 2` and will be equal to:

```csharp
var exp = new Pow(
    Variable.X,
    new Number(2)
);
```

## Evaluate it

### Execute methods

Let's assume you have `exp` from the [previous step](#parse-your-first-expression). Now you can evaluate it but because the expression has the `x` variable you need to provide a value for it.

```csharp
var parameters = new ExpressionParameters
{
    new Parameter("x", 10.0),
};
var result = exp.Execute(parameters);
```

The `result` will be equal to 100.0. All expressions have another overload of the `Execute` method, which doesn't accept any parameters, it is useful when your expression doesn't require any parameter, for example, `2 + 2`. But be careful with this method because it could throw the `NotSupportedException` exception if the expression has any parameter or user-defined function.

### Solve methods

If you don't need the expression tree at all, you can use `Processor.Solve` method. It will parse the string expression into the tree and immediately evaluate it for you:

```csharp
var processor = new Processor();
var result = processor.Solve<NumberResult>("10 ^ 2");
```

## Analyze it

The xFunc library has the implementation of [Visitor Pattern](https://en.wikipedia.org/wiki/Visitor_pattern). It is called `IAnalyzer<TResult>`. You can implement your own analyzers but xFunc provides several built-in analyzers: `Simplifier`, `Differentiator`, `TypeAnalyzer`. I'm going to demonstrate analyzer based on `Simplifier`.

```csharp
var simplifier = new Simplifier();
var exp = new Ln(new Variable("e")); // ln(e)
var result = exp.Analyze(simplifier);
```

The `Simplifier` allows you to simplify the expression, in the code shown above, the `ln(e)` expression will be simplified just to `1`.

## CLI Tool

xFunc has a CLI tool, to install it use the following command:

```bash
dotnet tool install -g xFunc.Cli
```

after that, you will be able to use the `xfunc` command in your terminal. It supports the following commands:

### `xfunc parse`

This command parses the expression and outputs the string representation of it. Right now, its main purpose is testing (to check the expression was parsed correctly).

### `xfunc solve`

This command evaluates the expression. For example:

```
xfunc solve "sin(90 'deg')"
```

### `xfunc interactive`

This command launches the CLI tool in the interactive mode where you can enter each expression one by one, like:

```
> sin(90 'deg')
1
> cos(90 'deg')
0
```

To exit use Cmd+C or Cmd+D on Mac OS.

Also, you are able to save the current progress to a file (by using `#save <path>` command) and run it later with `xfunc run`.

### `xfunc run`

Let's assume you saved your progress to the `test.xf` file with the following content:

```
sin(90 'deg')
cos(90 'deg')
```

So, you can execute it by:

```
xfunc run test.xf
```