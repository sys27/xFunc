// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class ExpTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new Exp(Number.Two);
        var expected = NumberValue.Exp(new NumberValue(2));

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var expected = new Complex(4, 2);
        var exp = new Exp(new ComplexNumber(expected));

        Assert.Equal(Complex.Exp(expected), exp.Execute());
    }

    [Fact]
    public void ExecuteExceptionTest()
        => TestNotSupported(new Exp(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Exp(Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}