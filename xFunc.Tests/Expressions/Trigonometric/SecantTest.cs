// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class SecantTest : BaseExpressionTests
{
    [Theory]
    [InlineData(0.0, 1.0)] // 1
    [InlineData(30.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [InlineData(45.0, 1.4142135623730951)] // sqrt(2)
    [InlineData(60.0, 2.0)] // 2
    [InlineData(90.0, double.PositiveInfinity)] // -
    [InlineData(120.0, -2.0)] // -2
    [InlineData(135.0, -1.4142135623730951)] // -sqrt(2)
    [InlineData(150.0, -1.1547005383792515)] // -2sqrt(3) / 3
    [InlineData(180.0, -1.0)] // -1
    [InlineData(210.0, -1.1547005383792515)] // -2sqrt(3) / 3
    [InlineData(225.0, -1.4142135623730951)] // -sqrt(2)
    [InlineData(240.0, -2.0)] // -2
    [InlineData(270.0, double.PositiveInfinity)] // -
    [InlineData(300.0, -2.0)] // -2
    [InlineData(315.0, 1.4142135623730951)] // sqrt(2)
    [InlineData(330.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [InlineData(360.0, 1.0)] // 1
    [InlineData(1110.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [InlineData(1770.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [InlineData(-390.0, 1.1547005383792515)] // 2sqrt(3) / 3
    public void ExecuteNumberTest(double degree, double expected)
    {
        var exp = new Sec(new Number(degree));
        var result = (NumberValue)exp.Execute();

        Assert.Equal(expected, result.Number);
    }

    [Fact]
    public void ExecuteDegreeTest()
    {
        var exp = new Sec(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.0001523280439077);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteRadianTest()
    {
        var exp = new Sec(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.8508157176809255);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteGradianTest()
    {
        var exp = new Sec(AngleValue.Gradian(1).AsExpression());
        var actual = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.0001233827397618);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Sec(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(-0.26351297515838928, result.Real, 15);
        Assert.Equal(0.036211636558768523, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Sec(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Sec(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}