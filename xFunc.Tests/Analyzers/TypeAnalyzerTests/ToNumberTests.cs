// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class ToNumberTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestToNumberUndefined()
    {
        Test(new ToNumber(Variable.X), ResultTypes.Number);
    }

    [Fact]
    public void TestAngleToNumber()
    {
        Test(new ToNumber(AngleValue.Degree(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestPowerToNumber()
    {
        Test(new ToNumber(PowerValue.Watt(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestTemperatureToNumber()
    {
        Test(new ToNumber(TemperatureValue.Celsius(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestMassToNumber()
    {
        Test(new ToNumber(MassValue.Gram(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestLengthToNumber()
    {
        Test(new ToNumber(LengthValue.Meter(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestTimeToNumber()
    {
        Test(new ToNumber(TimeValue.Second(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestAreaToNumber()
    {
        Test(new ToNumber(AreaValue.Meter(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestVolumeToNumber()
    {
        Test(new ToNumber(VolumeValue.Meter(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestRationalToNumber()
    {
        Test(new ToNumber(new Rational(new Number(1), new Number(3))), ResultTypes.Number);
    }

    [Fact]
    public void TestToNumberException()
    {
        TestException(new ToNumber(Bool.True));
    }
}