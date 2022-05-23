// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class LnTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new Ln(Number.Two);
        var expected = new NumberValue(Math.Log(2));

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var complex = new Complex(2, 3);
        var exp = new Ln(new ComplexNumber(complex));

        Assert.Equal(Complex.Log(complex), exp.Execute());
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Ln(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Ln(new Number(5));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}