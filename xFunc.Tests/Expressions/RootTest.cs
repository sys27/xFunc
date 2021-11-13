// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class RootTest : BaseExpressionTests
{
    [Fact]
    public void CalculateRootTest1()
    {
        var exp = new Root(new Number(8), new Number(3));
        var expected = new NumberValue(Math.Pow(8, 1.0 / 3.0));

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void CalculateRootTest2()
    {
        var exp = new Root(new Number(-8), new Number(3));

        Assert.Equal(new NumberValue(-2.0), exp.Execute());
    }

    [Fact]
    public void NegativeNumberExecuteTest()
    {
        var exp = new Root(new Number(-25), Number.Two);
        var result = (Complex)exp.Execute();
        var expected = new Complex(0, 5);

        Assert.Equal(expected.Real, result.Real, 14);
        Assert.Equal(expected.Imaginary, result.Imaginary, 14);
    }

    [Fact]
    public void NegativeNumberExecuteTest2()
    {
        var exp = new Root(new Number(-25), new Number(-2));

        Assert.Equal(new Complex(0, -0.2), exp.Execute());
    }

    [Fact]
    public void ExecuteExceptionTest()
        => TestNotSupported(new Root(Bool.False, Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Root(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}