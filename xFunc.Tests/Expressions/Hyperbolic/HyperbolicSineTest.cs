// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicSineTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Sinh(Number.One);
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.01745417862959511);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteRadianTest()
    {
        var exp = new Sinh(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.1752011936438014);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteDegreeTest()
    {
        var exp = new Sinh(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.01745417862959511);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteGradianTest()
    {
        var exp = new Sinh(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.015708609240015783);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Sinh(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(-4.16890695996656435075, result.Real, 15);
        Assert.Equal(9.154499146911429573, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Sinh(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Sinh(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}