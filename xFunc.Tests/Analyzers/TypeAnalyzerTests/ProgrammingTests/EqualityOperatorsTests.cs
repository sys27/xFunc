// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests;

public class EqualityOperatorsTests : TypeAnalyzerBaseTests
{
    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefined(Type type)
    {
        var exp = Create(type, Variable.X, Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestNumberAndUndefined(Type type)
    {
        var exp = Create(type, Number.One, Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndNumber(Type type)
    {
        var exp = Create(type, Variable.X, Number.One);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestBoolAndUndefined(Type type)
    {
        var exp = Create(type, Bool.True, Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndBool(Type type)
    {
        var exp = Create(type, Variable.X, Bool.True);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestAngleAndUndefined(Type type)
    {
        var exp = Create(type, AngleValue.Degree(1).AsExpression(), Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndAngle(Type type)
    {
        var exp = Create(type, Variable.X, AngleValue.Degree(1).AsExpression());

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestPowerAndUndefined(Type type)
    {
        var exp = Create(type, PowerValue.Watt(1).AsExpression(), Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndPower(Type type)
    {
        var exp = Create(type, Variable.X, PowerValue.Watt(1).AsExpression());

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestTemperatureAndUndefined(Type type)
    {
        var exp = Create(type, TemperatureValue.Celsius(1).AsExpression(), Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndTemperature(Type type)
    {
        var exp = Create(type, Variable.X, TemperatureValue.Celsius(1).AsExpression());

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestMassAndUndefined(Type type)
    {
        var exp = Create(type, MassValue.Gram(1).AsExpression(), Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndMass(Type type)
    {
        var exp = Create(type, Variable.X, MassValue.Gram(1).AsExpression());

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestLengthAndUndefined(Type type)
    {
        var exp = Create(type, LengthValue.Centimeter(1).AsExpression(), Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndLength(Type type)
    {
        var exp = Create(type, Variable.X, LengthValue.Centimeter(1).AsExpression());

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestTimeAndUndefined(Type type)
    {
        var exp = Create(type, TimeValue.Second(1).AsExpression(), Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndTime(Type type)
    {
        var exp = Create(type, Variable.X, TimeValue.Second(1).AsExpression());

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestAreaAndUndefined(Type type)
    {
        var exp = Create(type, AreaValue.Meter(1).AsExpression(), Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndArea(Type type)
    {
        var exp = Create(type, Variable.X, AreaValue.Meter(1).AsExpression());

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestVolumeAndUndefined(Type type)
    {
        var exp = Create(type, VolumeValue.Meter(1).AsExpression(), Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestUndefinedAndVolume(Type type)
    {
        var exp = Create(type, Variable.X, VolumeValue.Meter(1).AsExpression());

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestNumber(Type type)
    {
        var exp = Create(type, new Number(20), new Number(10));

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestBoolean(Type type)
    {
        var exp = Create(type, Bool.False, Bool.True);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestAngleNumber(Type type)
    {
        var exp = Create(type,
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Degree(10).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestPowerNumber(Type type)
    {
        var exp = Create(type,
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestTemperatureNumber(Type type)
    {
        var exp = Create(type,
            TemperatureValue.Celsius(10).AsExpression(),
            TemperatureValue.Celsius(10).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestMassNumber(Type type)
    {
        var exp = Create(type,
            MassValue.Gram(10).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestLengthNumber(Type type)
    {
        var exp = Create(type,
            LengthValue.Meter(10).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestTimeNumber(Type type)
    {
        var exp = Create(type,
            TimeValue.Second(10).AsExpression(),
            TimeValue.Second(10).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestAreaNumber(Type type)
    {
        var exp = Create(type,
            AreaValue.Meter(10).AsExpression(),
            AreaValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestVolumeNumber(Type type)
    {
        var exp = Create(type,
            VolumeValue.Meter(10).AsExpression(),
            VolumeValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndNumber(Type type)
    {
        var exp = CreateBinary(type, new ComplexNumber(1, 2), new Number(20));

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestNumberAndComplex(Type type)
    {
        var exp = CreateBinary(type, new Number(20), new ComplexNumber(1, 2));

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndBool(Type type)
    {
        var exp = CreateBinary(type, new ComplexNumber(1, 2), Bool.True);

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestBoolAndComplex(Type type)
    {
        var exp = CreateBinary(type, Bool.False, new ComplexNumber(1, 2));

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndAngle(Type type)
    {
        var exp = CreateBinary(type,
            new ComplexNumber(1, 2),
            AngleValue.Degree(10).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestAngleAndComplex(Type type)
    {
        var exp = CreateBinary(type,
            AngleValue.Degree(10).AsExpression(),
            new ComplexNumber(1, 2)
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndPower(Type type)
    {
        var exp = CreateBinary(type,
            new ComplexNumber(1, 2),
            PowerValue.Watt(10).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestPowerAndComplex(Type type)
    {
        var exp = CreateBinary(type,
            PowerValue.Watt(10).AsExpression(),
            new ComplexNumber(1, 2)
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndTemperature(Type type)
    {
        var exp = CreateBinary(type,
            new ComplexNumber(1, 2),
            TemperatureValue.Celsius(10).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestTemperatureAndComplex(Type type)
    {
        var exp = CreateBinary(type,
            TemperatureValue.Celsius(10).AsExpression(),
            new ComplexNumber(1, 2)
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndMass(Type type)
    {
        var exp = CreateBinary(type,
            new ComplexNumber(1, 2),
            MassValue.Gram(10).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestMassAndComplex(Type type)
    {
        var exp = CreateBinary(type,
            MassValue.Gram(10).AsExpression(),
            new ComplexNumber(1, 2)
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndLength(Type type)
    {
        var exp = CreateBinary(type,
            new ComplexNumber(1, 2),
            LengthValue.Meter(10).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestLengthAndComplex(Type type)
    {
        var exp = CreateBinary(type,
            LengthValue.Meter(10).AsExpression(),
            new ComplexNumber(1, 2)
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndTime(Type type)
    {
        var exp = CreateBinary(type,
            new ComplexNumber(1, 2),
            TimeValue.Second(10).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestTimeAndComplex(Type type)
    {
        var exp = CreateBinary(type,
            TimeValue.Second(10).AsExpression(),
            new ComplexNumber(1, 2)
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndArea(Type type)
    {
        var exp = CreateBinary(type,
            new ComplexNumber(1, 2),
            AreaValue.Meter(10).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestAreaAndComplex(Type type)
    {
        var exp = CreateBinary(type,
            AreaValue.Meter(10).AsExpression(),
            new ComplexNumber(1, 2)
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestComplexAndVolume(Type type)
    {
        var exp = CreateBinary(type,
            new ComplexNumber(1, 2),
            VolumeValue.Meter(10).AsExpression()
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestVolumeAndComplex(Type type)
    {
        var exp = CreateBinary(type,
            VolumeValue.Meter(10).AsExpression(),
            new ComplexNumber(1, 2)
        );

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equal))]
    [InlineData(typeof(NotEqual))]
    public void TestInvalidArgsException(Type type)
    {
        var exp = Create(type, new ComplexNumber(2, 3), new ComplexNumber(2, 3));

        TestException(exp);
    }
}