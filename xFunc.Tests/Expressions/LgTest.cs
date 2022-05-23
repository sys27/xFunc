// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class LgTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new Lg(Number.Two);
        var expected = new NumberValue(Math.Log10(2));

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var complex = new Complex(2, 3);
        var exp = new Lg(new ComplexNumber(complex));
        var expected = Complex.Log10(complex);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Lg(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Lg(Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}