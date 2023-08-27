// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class RationalTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest()
    {
        var exp = new Rational(Number.One, Number.Two);
        var expected = new RationalValue(1, 2);
        var actual = exp.Execute();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteLeftExceptionTest()
        => TestNotSupported(new Rational(Bool.True, Number.Two));

    [Fact]
    public void ExecuteRightExceptionTest()
        => TestNotSupported(new Rational(Number.One, Bool.True));

    [Fact]
    public void CloneTest()
    {
        var exp = new Rational(Number.One, Number.Two);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}