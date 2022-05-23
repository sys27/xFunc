// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class LogTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new Log(Number.Two, new Number(10));
        var expected = new NumberValue(Math.Log(10, 2));

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var complex = new Complex(2, 3);
        var exp = new Log(new Number(4), new ComplexNumber(complex));

        Assert.Equal(Complex.Log(complex, 4), exp.Execute());
    }

    [Fact]
    public void ExecuteLeftResultIsNotSupported()
        => TestNotSupported(new Log(Bool.False, Bool.True));

    [Fact]
    public void ExecuteRightResultIsNotSupported()
        => TestNotSupported(new Log(new Number(10), Bool.True));

    [Fact]
    public void CloneTest()
    {
        var exp = new Log(Number.Zero, new Number(5));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}