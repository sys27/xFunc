// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class UnaryMinusTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new UnaryMinus(new Number(10));

        Assert.Equal(new NumberValue(-10.0), exp.Execute());
    }

    [Fact]
    public void ExecuteAngleNumberTest()
    {
        var exp = new UnaryMinus(AngleValue.Degree(10).AsExpression());
        var expected = AngleValue.Degree(-10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecutePowerNumberTest()
    {
        var exp = new UnaryMinus(PowerValue.Watt(10).AsExpression());
        var expected = PowerValue.Watt(-10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTemperatureNumberTest()
    {
        var exp = new UnaryMinus(TemperatureValue.Celsius(10).AsExpression());
        var expected = TemperatureValue.Celsius(-10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMassNumberTest()
    {
        var exp = new UnaryMinus(MassValue.Gram(10).AsExpression());
        var expected = MassValue.Gram(-10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteLengthNumberTest()
    {
        var exp = new UnaryMinus(LengthValue.Meter(10).AsExpression());
        var expected = LengthValue.Meter(-10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTimeNumberTest()
    {
        var exp = new UnaryMinus(TimeValue.Second(10).AsExpression());
        var expected = TimeValue.Second(-10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteAreaNumberTest()
    {
        var exp = new UnaryMinus(AreaValue.Meter(10).AsExpression());
        var expected = AreaValue.Meter(-10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteVolumeNumberTest()
    {
        var exp = new UnaryMinus(VolumeValue.Meter(10).AsExpression());
        var expected = VolumeValue.Meter(-10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteComplexTest()
    {
        var complex = new Complex(2, 3);
        var exp = new UnaryMinus(new ComplexNumber(complex));

        Assert.Equal(Complex.Negate(complex), exp.Execute());
    }

    [Fact]
    public void ExecuteRationalNumberTest()
    {
        var rational = new UnaryMinus(new Rational(Number.One, Number.Two));
        var expected = new RationalValue(-1, 2);
        var actual = rational.Execute();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NotSupportedException()
        => TestNotSupported(new UnaryMinus(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new UnaryMinus(Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new UnaryMinus(Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new UnaryMinus(Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}