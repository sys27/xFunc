# How to implement custom expression

## User-defined functions

xFunc supports user-defined functions. The syntax is pretty close to C# lambdas.

To define a function:

```
f := (x) => sin(x) / cos(x)
```

where:

- `f` - is a function name
- `:=` - assign operator
- `(x)` - the list of parameters of lambda, in this case, `x` is only one parameter
- `=>` - lambda operator
- `sin(x) / cos(x)` - the definition of new function (basically `tan(x)`)

After evaluation of this expression, the new function will be added to `ExpressionParameters` and you will be able to use it in your expressions as any other function:

```
2 * f(1)
```

## Fork project

I think any possible "real" use case could be handled by [User-defined functions](#user-defined-functions). But in case you need something really special to implement, you need to fork the project and implement it by yourself. Please, follow this guide: [How to build the project](how-to-build.md).

To implement a new expression:

- create a new class and implement `IExpression` (or any other base class, like `UnaryExpression`, `BinaryExpression`, etc.)
- modify the `Parser.CreateFunction` method (in `Parser.ExpressionFactory.cs` file), so the parser could use your new expression

## Old versions of xFunc (before 4.0.0)

The design of old versions was inspired by the ASP.NET MVC framework, where you could replace/extend almost anything. The same was true for xFunc, you could replace/add/extend almost anything (lexer/parser/any expression). So, to add a new expression you needed to implement it in the C# code (by implementing the `IExpression` interface or any other base class), extend expression factory and add support of new expression to `Lexer`. The old lexer/parser was based on [RPN](https://en.wikipedia.org/wiki/Reverse_Polish_notation).

But in xFunc 4.0.0, the lexer, parser and a lot of other staff were rewritten from scratch and all these extension points were removed. So, if you want to implement a custom function you need to use one of the options described above.

*Note: All versions < 4.0.0 are deprecated and no longer supported.*