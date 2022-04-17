// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class RoundTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestRoundVariable()
    {
        var exp = new Round(Variable.X, new Number(10));

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestRoundNumber()
    {
        var exp = new Round(new Number(10), new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestRoundAngleNumber()
    {
        var exp = new Round(AngleValue.Degree(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestRoundPowerNumber()
    {
        var exp = new Round(PowerValue.Watt(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestRoundTemperatureNumber()
    {
        var exp = new Round(TemperatureValue.Celsius(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestRoundMassNumber()
    {
        var exp = new Round(MassValue.Gram(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestRoundLengthNumber()
    {
        var exp = new Round(LengthValue.Meter(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestRoundTimeNumber()
    {
        var exp = new Round(TimeValue.Second(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestRoundAreaNumber()
    {
        var exp = new Round(AreaValue.Meter(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestRoundVolumeNumber()
    {
        var exp = new Round(VolumeValue.Meter(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestRoundWithUnsupportedPrecisionException()
    {
        var exp = new Round(new Number(10), new ComplexNumber(10));

        TestDiffParamException(exp);
    }

    [Fact]
    public void TestRoundException()
    {
        var exp = new Round(new ComplexNumber(10), new Number(10));

        TestDiffParamException(exp);
    }
}