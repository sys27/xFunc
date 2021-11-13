// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions;

public class UserFunctionTest
{
    [Fact]
    public void NullCtor()
    {
        ImmutableArray<IExpression> arguments = default;

        Assert.Throws<ArgumentNullException>(() => new UserFunction("f", arguments));
    }

    [Fact]
    public void ExecuteTest1()
    {
        var functions = new FunctionCollection
        {
            { new UserFunction("f", new IExpression[] { Variable.X }), new Ln(Variable.X) }
        };
        var func = new UserFunction("f", new IExpression[] { Number.One });

        var actual = func.Execute(functions);
        var expected = new NumberValue(Math.Log(1));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTest2()
    {
        var functions = new FunctionCollection();

        var func = new UserFunction("f", new IExpression[] { Number.One });

        Assert.Throws<KeyNotFoundException>(() => func.Execute(functions));
    }

    [Fact]
    public void ExecuteRecursiveTest()
    {
        var expParams = new ExpressionParameters();

        var exp = new If(new Equal(Variable.X, Number.Zero),
            Number.One,
            new Mul(Variable.X, new UserFunction("f", new[] { new Sub(Variable.X, Number.One) })));
        expParams.Functions.Add(new UserFunction("f", new[] { Variable.X }), exp);

        var func = new UserFunction("f", new[] { new Number(4) });

        Assert.Equal(new NumberValue(24.0), func.Execute(expParams));
    }

    [Fact]
    public void ExecuteBoolTest()
    {
        var expParams = new ExpressionParameters();

        var exp = new Not(Variable.X);
        expParams.Functions.Add(new UserFunction("f", new[] { Variable.X }), exp);

        var func = new UserFunction("f", new[] { Bool.False });

        Assert.Equal(true, func.Execute(expParams));
    }

    [Fact]
    public void ExecuteNullTest()
    {
        var exp = new UserFunction("f", new IExpression[0]);

        Assert.Throws<ArgumentNullException>(() => exp.Execute());
    }

    [Fact]
    public void ArgumentsAreNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new UserFunction("f", null));
    }

    [Fact]
    public void EqualDiffNameTest()
    {
        var exp1 = new UserFunction("f", new[] { new Number(5) });
        var exp2 = new UserFunction("f2", new[] { new Number(5) });

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void EqualDiffCountTest()
    {
        var exp1 = new UserFunction("f", new[] { new Number(5) });
        var exp2 = new UserFunction("f", new[] { new Number(5), Number.Two });

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void EqualDiffTypeTest()
    {
        var exp1 = new UserFunction("f", new[] { new Number(5) });

        Assert.False(exp1.Equals(Variable.X));
    }

    [Fact]
    public void ComplexNumberAnalyzeNull()
    {
        var exp = new UserFunction("f", new[] { new Number(5) });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void ComplexNumberAnalyzeContextNull()
    {
        var exp = new UserFunction("f", new[] { new Number(5) });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new UserFunction("f", new[] { new Number(5) });
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}