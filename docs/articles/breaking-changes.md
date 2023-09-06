## xFunc 4.4.0 (dev)

* The `Helpers` class is removed from the public API (see [#698](https://github.com/sys27/xFunc/pull/698)).
* `LambdaExpression` doesn't capture context on `Execute` this code moved to `CallExpression` (see [#704](https://github.com/sys27/xFunc/issues/704)).
* `xFunc.DotnetTool` -> `xFunc.Cli`

## xFunc 4.3.0

* `MatrixIsInvalidException` -> `InvalidMatrixException`.
* `Vector`/`Matrix` returns `VectorValue`/`MatrixValue` instead of `Vector`/`Matrix`.
* `Del`/`Simplify`/`Derivate` functions accept/return lambdas instead of expression.
* The grammar for `CallExpression`. Now you can use any expression as the function to call, eg. `deriv((x) => x ^ 2)(3)` because `deriv` returns a lambda, you can call it immediately. Previously, only user function and inline lambda expression were supported.
* `ExpressionResult` is removed, partially replaced by `LambdaResult`.
* `ResultTypes.Expression` and `ResultTypes.Lambda` are merged into `ResultTypes.Lambda`.
* DotNet Tool has the `xfunc` name in the terminal (all letters are lowercase).
* `NumberValue.NegativeInfinity` points to `double.NegativeInfinity` instead of `double.PositiveInfinity`.
* The ability to parse the implicit mull operator (eg. 2x) is returned. 

## xFunc 4.2.0

* `xFunc.Maths` targets `.NET 6` instead of `.NET Standard 2.1`
* `ComplexNumber` doesn't perform the strict comparison of floating point numbers (Real and Imaginary parts).
* `AngleUnit` is `class` instead of `enum`.
* `Contains`/`ContainsKey` (`ParameterCollection`) doesn't check the constants collection anymore (see [#623](https://github.com/sys27/xFunc/issues/623)).
* Removed russian localization (see [#625](https://github.com/sys27/xFunc/issues/625)).
* `UserFunction` was removed, partially replaced by `CallExpression` and `Lambda` (see [#628](https://github.com/sys27/xFunc/issues/628)).
* `ParameterCollection` is renamed to `ExpressionParamaters`, and the old version of `ExpressionParameters` is removed.
* `FunctionCollection` was removed, now `ExpressionParameters` can hold functions as well (see [#628](https://github.com/sys27/xFunc/issues/628)).
* `->` and `=>` operators can be used only for lambda expressions, the implication and the equality operators can be used only by keywords: `impl` and `eq`.
* `Define`/`Undefine` can accept only `Variable` instead of `IExpression`.
* the grammar to define user function was changed from `f(x) := sin(x)` to `f := (x) => sin(x)`.
* `Define` -> `Assign`, `Undefine` -> `Unassign`.
* The `:=` operator and `assign`/`unassign` functions are parsed as expressions instead of statements (they can be used inside other expressions, for example: `1 + (x := 1)`/`(f := (x) => x)(2)`
* `Assign`/`Unassign` returns the assigned/unassigned value instead of `string`.
* The ability to parse the implicit mull operator (eg. `2x`) is removed. Now only `2 * x` is supported.

## xFunc 4.1.0

* `Analyzer<TResult, TContext>` is abstract now.
* UI moved to the separate repository [xFunc.UI](https://github.com/sys27/xFunc.UI).
* The `xFunc.UnitConverter` project was removed (unit conversion migrated to xFunc.Maths).

## xFunc 4.0.0

**The grammar is changed:**
* it is no longer "validated" by lexer/post processing, now it is a part of parsing process.
* `:=`, `def`, `undef` operators are statements now (so, they cannot be used as a parameter of any expression, execpt: 'for', 'while', 'if' or standalone usage).
* `for`, `while` functions are statements now.
* the `^` operator is right-associative (see [#244](https://github.com/sys27/xFunc/issues/244)).
* `vector`/`matrix` prefix is no longer required to create a vector/matrix, like: `{1, 2}`.
* functions should use parentheses (`(`, `)`), `{`, `}` is no longer supported.

**Lexer:**
* The lexer post processing is removed.
* The lexer factories are removed.
* `Functions`, `Symbols`, `Operators` enums are removed.
* `UserFunctionToken` is removed.
* Several operator tokens are moved to keywords.
* Lexer and Tokens are removed from public API (see [#311](https://github.com/sys27/xFunc/issues/311), [#312](https://github.com/sys27/xFunc/issues/312)).
* All Tokens are represented by single struct (see [#311](https://github.com/sys27/xFunc/issues/311)).

**Expressions:**
* Expressions are **immutable** ([#331](https://github.com/sys27/xFunc/issues/331)). 
* Precedence of operators are changed ([#343](https://github.com/sys27/xFunc/issues/343)).
* Changed validation in expression constructors (no nulls, no empty arrays, strict parameter count).
* `IFunctionExpression` is removed.
* Changer order of parameters in `Log` constructor.
* `Inc`, `Dec` do not inherit `UnaryExpression`. The `Argument` property is renamed to `Variable` (type `Variable`).
* `AddAssign`, `SubAssign`, `MulAssign`, `DivAssign` do not inherit `BinaryExpression`. The `Left` property is renamed to `Variable` (type `Variable`).
* `UnaryExpression.Argument`, `BinaryExpression.Left/Right` are no longer virtual.
* `DifferentParametersExpression.Arguments`, `Matrix.Vectors` return `IEnumerable<IExpression>` instead of array and it is only getter now.
* `Matrix`, `Vector`, `DifferentParametersExpression` do not allow `null` arguments.
* `Matrix`, `UserFunction` do not inherit `DifferentParametersExpression`.
* `Matrix.CreateIdentity`, `Matrix.this[int row, int col]` are removed.
* `Matrix.ParametersCount` -> `Rows`.
* `Matrix.SizeOfVectors` -> `Columns`.
* `And`/`Or` (from `xFunc.Maths.Expressions.Programming` namespace) -> `ConditionalAnd`/`ConditionalOr`.
* Logical/Bitwise operators do not support floating point numbers (throw `ArgumentException` exception).
* The `Parent` property is removed from `IExpression` (see [#275](https://github.com/sys27/xFunc/issues/275)).
* Inverse trigonometric/hyperbolic expression return radians, use conversion functions (`todegree`, etc) to get other units (see [#268](https://github.com/sys27/xFunc/issues/268)).

**Other:**
* The expression factory is removed (moved to parser as internal part).
* Removed support for `.NET Framework`. `xFunc.Maths`/`xFunc.UnitConverters` is [.NET Standard 2.1](https://docs.microsoft.com/en-us/dotnet/standard/net-standard).
* Removed Ukranian localization.
* xFunc UI moved to .NET Core 3.1.
* `CLSCompliant` is removed.
* `GetHashCode` implementations are removed.
* `MathExtensions`, `ComplexExtensions`, `MatrixExtensions` are removed from public API.
* `Variable` and `Parameters` properties are moved from `Differentiator` to `DifferentiatorContext` (see [#277](https://github.com/sys27/xFunc/pull/277)).
* The constructor of `Bool` is private, use static fields `True`/`False`.
* `AngleMeasurement` moved to `Expression.Angles` namespace (see [#268](https://github.com/sys27/xFunc/issues/268)).
* The `AngleMeasurement` property is removed from `ExpressionParameters`, this feature is replaced by number units, which you can specify alongside a number, like `90 degree` (see [#268](https://github.com/sys27/xFunc/issues/268)).
* `NumeralSystem.Hexidecimal` -> `NumeralSystem.Hexadecimal`.
* `NumeralSystem` is removed (see [#302](https://github.com/sys27/xFunc/issues/302)).
* It is not allowed to assign an `object` to `Parameter` (it is removed from public API).

## xFunc 3.7.0

* All internal constructors of expression was removed (see [#216](https://github.com/sys27/xFunc/issues/216)).
* `ExpressionFactory` was reworked (see [#216](https://github.com/sys27/xFunc/issues/216)).
* `LexerException` -> `TokenizeException` (see [#216](https://github.com/sys27/xFunc/issues/216)).
* `ParserException` -> `ParseException` (see [#216](https://github.com/sys27/xFunc/issues/216)).
* The `DoSimplify` property was removed (see [#220](https://github.com/sys27/xFunc/issues/220)).
* The setter for `Type` property of `Parameter` was removed.
* `Parameter` cannot contain `null` value (see [#222](https://github.com/sys27/xFunc/issues/222)).
* The value of `Parameter` (Constant or ReadOnly) cannot be changed (see [#222](https://github.com/sys27/xFunc/issues/222)).
* Max/MinParameters are removed (see [#223](https://github.com/sys27/xFunc/issues/223)).
* `FunctionToken.CountOfParams` -> `FunctionToken.CountOfParameters`

## xFunc 3.6.0

* `MinParameters`, `MaxParameters`, `ParametersCount` moved from `IExpression` to `IFunctionExpression` (see [#211](https://github.com/sys27/xFunc/issues/211)).

## xFunc 3.5.0

* Changed exceptions, which are thrown by Expressions and Type Analyzer (see [#190](https://github.com/sys27/xFunc/issues/170)).
* Removed ReverseFunctionAttribute (see [#195](https://github.com/sys27/xFunc/issues/195)).
* Variables and user functions are case sensitive (see [#197](https://github.com/sys27/xFunc/issues/197)).

## xFunc 3.2.1

* Removed `Processor.Parse(string, bool)` method (see [#170](https://github.com/sys27/xFunc/issues/170)). Add new property to control simplification of expression: `bool Processor.DoSimplify`.
