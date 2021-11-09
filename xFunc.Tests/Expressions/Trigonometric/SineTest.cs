// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class SineTest : BaseExpressionTests
{
    [Theory]
    [InlineData(0.0, 0.0)] // 0
    [InlineData(30.0, 0.5)] // 1 / 2
    [InlineData(45.0, 0.70710678118654757)] // sqrt(2) / 2
    [InlineData(60.0, 0.86602540378443864)] // sqrt(3) / 2
    [InlineData(90.0, 1.0)] // 1
    [InlineData(120.0, 0.86602540378443864)] // sqrt(3) / 2
    [InlineData(135.0, 0.70710678118654757)] // sqrt(2) / 2
    [InlineData(150.0, 0.5)] // 1 / 2
    [InlineData(180.0, 0.0)] // 0
    [InlineData(210.0, -0.5)] // -1 / 2
    [InlineData(225.0, -0.70710678118654757)] // -sqrt(2) / 2
    [InlineData(240.0, -0.86602540378443864)] // -sqrt(3) / 2
    [InlineData(270.0, -1.0)] // -1
    [InlineData(300.0, -0.86602540378443864)] // -sqrt(3) / 2
    [InlineData(315.0, -0.70710678118654757)] // -sqrt(2) / 2
    [InlineData(330.0, -0.5)] // -1 / 2
    [InlineData(360.0, 0)] // 0
    [InlineData(1110.0, 0.5)] // 1 / 2
    [InlineData(1770.0, -0.5)] // -1 / 2
    [InlineData(-390.0, -0.5)] // -1 / 2
    public void ExecuteNumberTest(double degree, double expected)
    {
        var exp = new Sin(new Number(degree));
        var result = (NumberValue)exp.Execute();

        Assert.Equal(expected, result.Number, 15);
    }

    [Fact]
    public void ExecuteRadianTest()
    {
        var exp = new Sin(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.8414709848078965);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteDegreeTest()
    {
        var exp = new Sin(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.017452406437283512);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteGradianTest()
    {
        var exp = new Sin(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.015707317311820675);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Sin(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(0.53092108624851986, result.Real, 15);
        Assert.Equal(-3.59056458998578, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Sin(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Sin(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}