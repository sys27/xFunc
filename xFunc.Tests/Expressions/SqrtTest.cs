// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class SqrtTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new Sqrt(new Number(4));
        var expected = new NumberValue(Math.Sqrt(4));

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void NegativeNumberExecuteTest1()
    {
        var exp = new Sqrt(new Number(-25));

        Assert.Equal(new Complex(0, 5), exp.Execute());
    }

    [Fact]
    public void NegativeNumberExecuteTest2()
    {
        var exp = new Sqrt(new Number(-1));

        Assert.Equal(new Complex(0, 1), exp.Execute());
    }

    [Fact]
    public void ComplexExecuteTest()
    {
        var complex = new Complex(5, 3);
        var exp = new Sqrt(new ComplexNumber(complex));

        Assert.Equal(Complex.Sqrt(complex), exp.Execute());
    }

    [Fact]
    public void NegativeComplexNumberExecuteTest()
    {
        var complex = new Complex(-25, 13);
        var exp = new Sqrt(new ComplexNumber(complex));

        Assert.Equal(Complex.Sqrt(complex), exp.Execute());
    }

    [Fact]
    public void ExecuteBoolTest()
        => TestNotSupported(new Sqrt(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Sqrt(Number.Two);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}