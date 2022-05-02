// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class AbsTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestAbsNumber()
    {
        var exp = new Abs(new Number(-2));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestAbsAngleNumber()
    {
        var exp = new Abs(AngleValue.Degree(1).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestAbsPowerNumber()
    {
        var exp = new Abs(PowerValue.Watt(1).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestAbsTemperatureNumber()
    {
        var exp = new Abs(TemperatureValue.Celsius(1).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestAbsMassNumber()
    {
        var exp = new Abs(MassValue.Gram(1).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestAbsLengthNumber()
    {
        var exp = new Abs(LengthValue.Meter(1).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestAbsTimeNumber()
    {
        var exp = new Abs(TimeValue.Second(1).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestAbsAreaNumber()
    {
        var exp = new Abs(AreaValue.Meter(1).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestAbsVolumeNumber()
    {
        var exp = new Abs(VolumeValue.Meter(1).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestAbsComplexNumber()
    {
        var exp = new Abs(new ComplexNumber(2, 2));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestAbsVariable()
    {
        var exp = new Abs(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAbsVector()
    {
        var exp = new Abs(new Vector(new IExpression[] { Number.One }));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestAbsException()
    {
        var exp = new Abs(Bool.False);

        TestException(exp);
    }
}