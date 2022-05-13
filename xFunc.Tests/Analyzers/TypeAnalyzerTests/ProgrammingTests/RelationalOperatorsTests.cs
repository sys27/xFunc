// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests;

public class GreaterOrEqualTests : TypeAnalyzerBaseTests
{
    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestUndefined(Type type)
    {
        var exp = CreateBinary(type, Variable.X, Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestNumberUndefined(Type type)
    {
        var exp = CreateBinary(type, Number.One, Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestUndefinedNumber(Type type)
    {
        var exp = CreateBinary(type, Variable.X, Number.One);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestAngleUndefined(Type type)
    {
        var exp = CreateBinary(type, AngleValue.Degree(1).AsExpression(), Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestGUndefinedAngle(Type type)
    {
        var exp = CreateBinary(type, Variable.X, AngleValue.Degree(1).AsExpression());

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestNumber(Type type)
    {
        var exp = CreateBinary(type, new Number(10), new Number(10));

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestAngle(Type type)
    {
        var exp = CreateBinary(type,
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Degree(12).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestPower(Type type)
    {
        var exp = CreateBinary(type,
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Watt(12).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestTemperature(Type type)
    {
        var exp = CreateBinary(type,
            TemperatureValue.Celsius(10).AsExpression(),
            TemperatureValue.Celsius(12).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestMass(Type type)
    {
        var exp = CreateBinary(type,
            MassValue.Gram(10).AsExpression(),
            MassValue.Gram(12).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestLength(Type type)
    {
        var exp = CreateBinary(type,
            LengthValue.Meter(10).AsExpression(),
            LengthValue.Meter(12).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestTime(Type type)
    {
        var exp = CreateBinary(type,
            TimeValue.Second(10).AsExpression(),
            TimeValue.Second(12).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestArea(Type type)
    {
        var exp = CreateBinary(type,
            AreaValue.Meter(10).AsExpression(),
            AreaValue.Meter(12).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestVolume(Type type)
    {
        var exp = CreateBinary(type,
            VolumeValue.Meter(10).AsExpression(),
            VolumeValue.Meter(12).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestBoolNumberException(Type type)
    {
        var exp = CreateBinary(type, Bool.True, new Number(10));

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestNumberBoolException(Type type)
    {
        var exp = CreateBinary(type, new Number(10), Bool.True);

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestBoolAngle(Type type)
    {
        var exp = CreateBinary(type,
            Bool.True,
            AngleValue.Degree(12).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestAngleBool(Type type)
    {
        var exp = CreateBinary(type,
            AngleValue.Degree(12).AsExpression(),
            Bool.True
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestBoolPower(Type type)
    {
        var exp = CreateBinary(type,
            Bool.True,
            PowerValue.Watt(12).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestPowerBool(Type type)
    {
        var exp = CreateBinary(type,
            PowerValue.Watt(12).AsExpression(),
            Bool.True
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestBoolTemperature(Type type)
    {
        var exp = CreateBinary(type,
            Bool.True,
            TemperatureValue.Celsius(12).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestTemperatureBool(Type type)
    {
        var exp = CreateBinary(type,
            TemperatureValue.Celsius(12).AsExpression(),
            Bool.True
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestBoolMass(Type type)
    {
        var exp = CreateBinary(type,
            Bool.True,
            MassValue.Gram(12).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestMassBool(Type type)
    {
        var exp = CreateBinary(type,
            MassValue.Gram(12).AsExpression(),
            Bool.True
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestBoolLength(Type type)
    {
        var exp = CreateBinary(type,
            Bool.True,
            LengthValue.Meter(12).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestLengthBool(Type type)
    {
        var exp = CreateBinary(type,
            LengthValue.Meter(12).AsExpression(),
            Bool.True
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestBoolTime(Type type)
    {
        var exp = CreateBinary(type,
            Bool.True,
            TimeValue.Second(12).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestTimeBool(Type type)
    {
        var exp = CreateBinary(type,
            TimeValue.Second(12).AsExpression(),
            Bool.True
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestBoolArea(Type type)
    {
        var exp = CreateBinary(type,
            Bool.True,
            AreaValue.Meter(12).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestAreaBool(Type type)
    {
        var exp = CreateBinary(type,
            AreaValue.Meter(12).AsExpression(),
            Bool.True
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestBoolVolume(Type type)
    {
        var exp = CreateBinary(type,
            Bool.True,
            VolumeValue.Meter(12).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestVolumeBool(Type type)
    {
        var exp = CreateBinary(type,
            VolumeValue.Meter(12).AsExpression(),
            Bool.True
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(GreaterThan))]
    [InlineData(typeof(GreaterOrEqual))]
    [InlineData(typeof(LessThan))]
    [InlineData(typeof(LessOrEqual))]
    public void TestRelationalOperatorException(Type type)
    {
        var exp = CreateBinary(type, Bool.True, Bool.False);

        TestException(exp);
    }
}