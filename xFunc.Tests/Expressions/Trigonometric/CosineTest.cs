// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class CosineTest : BaseExpressionTests
{
    [Theory]
    [InlineData(0.0, 1.0)] // 1
    [InlineData(30.0, 0.86602540378443864)] // sqrt(3) / 2
    [InlineData(45.0, 0.70710678118654757)] // sqrt(2) / 2
    [InlineData(60.0, 0.5)] // 1 / 2
    [InlineData(90.0, 0.0)] // 0
    [InlineData(120.0, -0.5)] // -1 / 2
    [InlineData(135.0, -0.70710678118654757)] // -sqrt(2) / 2
    [InlineData(150.0, -0.86602540378443864)] // -sqrt(3) / 2
    [InlineData(180.0, -1.0)] // -1
    [InlineData(210.0, -0.86602540378443864)] // -sqrt(3) / 2
    [InlineData(225.0, -0.70710678118654757)] // -sqrt(2) / 2
    [InlineData(240.0, -0.5)] // -1 / 2
    [InlineData(270.0, 0.0)] // 0
    [InlineData(300.0, 0.5)] // 1 / 2
    [InlineData(315.0, 0.70710678118654757)] // sqrt(2) / 2
    [InlineData(330.0, 0.86602540378443864)] // sqrt(3) / 2
    [InlineData(360.0, 1.0)] // 1
    [InlineData(1110.0, 0.86602540378443864)] // sqrt(3) / 2
    [InlineData(1770.0, 0.86602540378443864)] // sqrt(3) / 2
    [InlineData(-390.0, 0.86602540378443864)] // sqrt(3) / 2
    public void ExecuteNumberTest(double degree, double expected)
    {
        var exp = new Cos(new Number(degree));
        var result = (NumberValue)exp.Execute();

        Assert.Equal(expected, result.Number, 15);
    }

    [Fact]
    public void ExecuteRadianTest()
    {
        var exp = new Cos(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.5403023058681398);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteDegreeTest()
    {
        var exp = new Cos(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.9998476951563913);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteGradianTest()
    {
        var exp = new Cos(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.9998766324816606);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Cos(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(-3.7245455049153224, result.Real, 15);
        Assert.Equal(-0.51182256998738462, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Cos(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Cos(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}