// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions;

public class GCDTest : BaseExpressionTests
{
    [Fact]
    public void CalculateTest1()
    {
        var exp = new GCD(new Number(12), new Number(16));
        var expected = new NumberValue(4.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void CalculateTest2()
    {
        var exp = new GCD(new IExpression[] { new Number(64), new Number(16), new Number(8) });
        var expected = new NumberValue(8.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void CalculateWrongArgumentTypeTest()
    {
        var exp = new GCD(new IExpression[] { Bool.True, new Number(16), new Number(8) });

        TestNotSupported(exp);
    }

    [Fact]
    public void NullArgTest()
        => Assert.Throws<ArgumentNullException>(() => new GCD(null));

    [Fact]
    public void CloneTest()
    {
        var exp = new GCD(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Fact]
    public void CloneWithArgsTest()
    {
        var exp = new GCD(Variable.X, Number.Zero);
        var args = new IExpression[] { Variable.X, Variable.Y, }.ToImmutableArray();
        var clone = exp.Clone(args);
        var expected = new GCD(args);

        Assert.Equal(expected, clone);
    }

    [Fact]
    public void EqualsSameTest()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualsNullTest()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void EqualsDiffTypesTest()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });
        var number = Number.Two;

        Assert.False(exp.Equals(number));
    }

    [Fact]
    public void EqualsDiffCountTest()
    {
        var exp1 = new GCD(new IExpression[] { new Number(16), new Number(8) });
        var exp2 = new GCD(new IExpression[] { new Number(16), new Number(8), Number.Two });

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}