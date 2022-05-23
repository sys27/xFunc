// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions.Statistical;

public class StatisticalTests :  BaseExpressionTests
{
    [Theory]
    [InlineData(typeof(Avg))]
    [InlineData(typeof(Count))]
    [InlineData(typeof(Max))]
    [InlineData(typeof(Min))]
    [InlineData(typeof(Product))]
    [InlineData(typeof(Stdev))]
    [InlineData(typeof(Stdevp))]
    [InlineData(typeof(Sum))]
    [InlineData(typeof(Var))]
    [InlineData(typeof(Varp))]
    public void NotSupportedException(Type type)
    {
        var exp = Create(type, new IExpression[] { Bool.False, Bool.False });

        TestNotSupported(exp);
    }

    [Theory]
    [InlineData(typeof(Avg))]
    [InlineData(typeof(Count))]
    [InlineData(typeof(Max))]
    [InlineData(typeof(Min))]
    [InlineData(typeof(Product))]
    [InlineData(typeof(Stdev))]
    [InlineData(typeof(Stdevp))]
    [InlineData(typeof(Sum))]
    [InlineData(typeof(Var))]
    [InlineData(typeof(Varp))]
    public void CloneTest(Type type)
    {
        var exp = Create<StatisticalExpression>(type, new IExpression[] { Number.One, Number.Two });
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Theory]
    [InlineData(typeof(Avg))]
    [InlineData(typeof(Count))]
    [InlineData(typeof(Max))]
    [InlineData(typeof(Min))]
    [InlineData(typeof(Product))]
    [InlineData(typeof(Stdev))]
    [InlineData(typeof(Stdevp))]
    [InlineData(typeof(Sum))]
    [InlineData(typeof(Var))]
    [InlineData(typeof(Varp))]
    public void CloneWithReplaceTest(Type type)
    {
        var exp = Create<StatisticalExpression>(type, new IExpression[] { Number.One, Number.Two });
        var arg = ImmutableArray.Create<IExpression>(Number.One);
        var clone = exp.Clone(arg);
        var expected = Create(type, arg);

        Assert.Equal(expected, clone);
    }
}