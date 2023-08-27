// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class RoundTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestRoundVariable()
    {
        var exp = new Round(Variable.X, new Number(10));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestRoundNumber()
    {
        var exp = new Round(new Number(10), new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestRoundAngleNumber()
    {
        var exp = new Round(AngleValue.Degree(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestRoundPowerNumber()
    {
        var exp = new Round(PowerValue.Watt(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestRoundTemperatureNumber()
    {
        var exp = new Round(TemperatureValue.Celsius(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestRoundMassNumber()
    {
        var exp = new Round(MassValue.Gram(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestRoundLengthNumber()
    {
        var exp = new Round(LengthValue.Meter(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestRoundTimeNumber()
    {
        var exp = new Round(TimeValue.Second(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestRoundAreaNumber()
    {
        var exp = new Round(AreaValue.Meter(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestRoundVolumeNumber()
    {
        var exp = new Round(VolumeValue.Meter(10).AsExpression(), new Number(10));

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestRoundWithUnsupportedPrecisionException()
    {
        var exp = new Round(new Number(10), new ComplexNumber(10));

        TestDiffParamException(exp);
    }

    [Test]
    public void TestRoundException()
    {
        var exp = new Round(new ComplexNumber(10), new Number(10));

        TestDiffParamException(exp);
    }
}