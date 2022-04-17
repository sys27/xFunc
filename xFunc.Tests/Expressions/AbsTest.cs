// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Tests.Expressions;

public class AbsTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTestNumber()
    {
        var exp = new Abs(new Number(-1));
        var expected = new NumberValue(1.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestAngleNumber()
    {
        var exp = new Abs(AngleValue.Degree(-10).AsExpression());
        var expected = AngleValue.Degree(10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestPowerValue()
    {
        var exp = new Abs(PowerValue.Watt(-1).AsExpression());
        var expected = PowerValue.Watt(1);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestTemperatureValue()
    {
        var exp = new Abs(TemperatureValue.Celsius(-1).AsExpression());
        var expected = TemperatureValue.Celsius(1);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestMassValue()
    {
        var exp = new Abs(MassValue.Gram(-1).AsExpression());
        var expected = MassValue.Gram(1);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestLengthValue()
    {
        var exp = new Abs(LengthValue.Meter(-1).AsExpression());
        var expected = LengthValue.Meter(1);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestTimeValue()
    {
        var exp = new Abs(TimeValue.Second(-1).AsExpression());
        var expected = TimeValue.Second(1);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestAreaValue()
    {
        var exp = new Abs(AreaValue.Meter(-1).AsExpression());
        var expected = AreaValue.Meter(1);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestVolumeValue()
    {
        var exp = new Abs(VolumeValue.Meter(-1).AsExpression());
        var expected = VolumeValue.Meter(1);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestComplexNumber()
    {
        var exp = new Abs(new ComplexNumber(4, 2));
        var expected = Complex.Abs(new Complex(4, 2));

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestVector()
    {
        var exp = new Abs(new Vector(new IExpression[]
        {
            new Number(5), new Number(4), new Number(6), new Number(7)
        }));
        var expected = new NumberValue(11.2249721603218241567);

        Assert.Equal(expected, (NumberValue)exp.Execute());
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Abs(Bool.False));

    [Fact]
    public void EqualsTest1()
    {
        Variable x1 = "x";
        Number num1 = Number.Two;
        var mul1 = new Mul(num1, x1);
        var abs1 = new Abs(mul1);

        Variable x2 = "x";
        Number num2 = Number.Two;
        var mul2 = new Mul(num2, x2);
        var abs2 = new Abs(mul2);

        Assert.True(abs1.Equals(abs2));
        Assert.True(abs1.Equals(abs1));
    }

    [Fact]
    public void EqualsTest2()
    {
        Variable x1 = "x";
        Number num1 = Number.Two;
        var mul1 = new Mul(num1, x1);
        var abs1 = new Abs(mul1);

        Variable x2 = "x";
        Number num2 = new Number(3);
        var mul2 = new Mul(num2, x2);
        var abs2 = new Abs(mul2);

        Assert.False(abs1.Equals(abs2));
        Assert.False(abs1.Equals(mul2));
        Assert.False(abs1.Equals(null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Abs(Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}