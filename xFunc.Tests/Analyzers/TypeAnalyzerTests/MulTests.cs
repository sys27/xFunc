// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class MulTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestMulTwoNumberTest()
    {
        var mul = new Mul(Number.One, Number.Two);

        Test(mul, ResultTypes.Number);
    }

    [Fact]
    public void TestMulNumberVarTest()
    {
        var mul = new Mul(Number.One, Variable.X);

        Test(mul, ResultTypes.Undefined);
    }

    [Fact]
    public void TestMulNumberBoolTest()
    {
        var mul = new Mul(Number.One, Bool.True);

        TestBinaryException(mul);
    }

    [Fact]
    public void TestMulBoolNumberTest()
    {
        var mul = new Mul(Bool.True, Number.One);

        TestBinaryException(mul);
    }

    [Fact]
    public void TestMulVarNumberTest()
    {
        var mul = new Mul(Variable.X, Number.One);

        Test(mul, ResultTypes.Undefined);
    }

    [Fact]
    public void TestMulTwoMatrixTest()
    {
        var mul = new Mul(
            new Matrix(new[] { new Vector(new IExpression[] { Number.One }) }),
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        Test(mul, ResultTypes.Matrix);
    }

    [Fact]
    public void TestMulLeftMatrixRightException()
    {
        var mul = new Mul(
            new Matrix(new[] { new Vector(new IExpression[] { Number.One }) }),
            Bool.False
        );

        TestBinaryException(mul);
    }

    [Fact]
    public void TestMulRightMatrixLeftException()
    {
        var mul = new Mul(
            Bool.False,
            new Matrix(new[] { new Vector(new IExpression[] { Number.One }) })
        );

        TestBinaryException(mul);
    }

    [Fact]
    public void TestMulNumberVectorTest()
    {
        var mul = new Mul(
            Number.One,
            new Vector(new IExpression[] { Number.One })
        );

        Test(mul, ResultTypes.Vector);
    }

    [Fact]
    public void TestMulVectorNumber()
    {
        var mul = new Mul(
            new Vector(new IExpression[] { Number.One }),
            Number.Two
        );

        Test(mul, ResultTypes.Vector);
    }

    [Fact]
    public void TestMulVectors()
    {
        var mul = new Mul(
            new Vector(new IExpression[] { Number.One }),
            new Vector(new IExpression[] { Number.One })
        );

        Test(mul, ResultTypes.Vector);
    }

    [Fact]
    public void TestMulNumberMatrixTest()
    {
        var mul = new Mul(
            Number.One,
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        Test(mul, ResultTypes.Matrix);
    }

    [Fact]
    public void TestMulMatrixAndNumberTest()
    {
        var mul = new Mul(
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
            Number.One
        );

        Test(mul, ResultTypes.Matrix);
    }

    [Fact]
    public void TestMulVectorMatrixTest()
    {
        var mul = new Mul(
            new Vector(new IExpression[] { Number.One }),
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        Test(mul, ResultTypes.Matrix);
    }

    [Fact]
    public void TestMulMatrixAndVectorTest()
    {
        var mul = new Mul(
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
            new Vector(new IExpression[] { Number.One })
        );

        Test(mul, ResultTypes.Matrix);
    }

    [Fact]
    public void TestMulVectorBoolException()
    {
        var mul = new Mul(new Vector(new IExpression[] { Number.One }), Bool.False);

        TestBinaryException(mul);
    }

    [Fact]
    public void TestMulBoolVectorException()
    {
        var mul = new Mul(Bool.False, new Vector(new IExpression[] { Number.One }));

        TestBinaryException(mul);
    }

    [Fact]
    public void TestMulComplexNumberComplexNumberTest()
    {
        var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestMulComplexNumberNumberTest()
    {
        var exp = new Mul(new ComplexNumber(2, 5), Number.Two);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestMulComplexNumberBoolTest()
    {
        var exp = new Mul(new ComplexNumber(2, 5), Bool.True);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestMulNumberComplexNumberTest()
    {
        var exp = new Mul(Number.Two, new ComplexNumber(3, 2));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestMulBoolComplexNumberTest()
    {
        var exp = new Mul(Bool.True, new ComplexNumber(2, 5));

        TestBinaryException(exp);
    }

    [Fact]
    public void TestMulException()
    {
        var exp = new Mul(Bool.False, Bool.True);

        TestException(exp);
    }

    [Fact]
    public void TestMulNumberAngle()
    {
        var exp = new Mul(
            new Number(10),
            AngleValue.Radian(10).AsExpression()
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestMulAngleNumber()
    {
        var exp = new Mul(
            AngleValue.Degree(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestMulNumberPower()
    {
        var exp = new Mul(
            new Number(10),
            PowerValue.Watt(10).AsExpression()
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestMulPowerNumber()
    {
        var exp = new Mul(
            PowerValue.Watt(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestMulNumberTemperature()
    {
        var exp = new Mul(
            new Number(10),
            TemperatureValue.Celsius(10).AsExpression()
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestMulTemperatureNumber()
    {
        var exp = new Mul(
            TemperatureValue.Celsius(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestMulNumberAndMass()
    {
        var exp = new Mul(
            new Number(10),
            MassValue.Gram(10).AsExpression()
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestMulMassAndNumber()
    {
        var exp = new Mul(
            MassValue.Gram(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestMulNumberAndLength()
    {
        var exp = new Mul(
            new Number(10),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestMulLengthAndNumber()
    {
        var exp = new Mul(
            LengthValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestMulLengthAndLength()
    {
        var exp = new Mul(
            LengthValue.Meter(10).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestMulAreaAndLength()
    {
        var exp = new Mul(
            AreaValue.Meter(10).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestMulLengthAndArea()
    {
        var exp = new Mul(
            LengthValue.Meter(10).AsExpression(),
            AreaValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestMulNumberAndTime()
    {
        var exp = new Mul(
            new Number(10),
            TimeValue.Second(10).AsExpression()
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestMulTimeAndNumber()
    {
        var exp = new Mul(
            TimeValue.Second(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestMulNumberAndArea()
    {
        var exp = new Mul(
            new Number(10),
            AreaValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestMulAreaAndNumber()
    {
        var exp = new Mul(
            AreaValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestMulNumberAndVolume()
    {
        var exp = new Mul(
            new Number(10),
            VolumeValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestMulVolumeAndNumber()
    {
        var exp = new Mul(
            VolumeValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestMulRationalAndRational()
    {
        var exp = new Mul(
            new Rational(Number.One, Number.Two),
            new Rational(Number.Two, Number.One)
        );

        Test(exp, ResultTypes.RationalNumber);
    }

    [Fact]
    public void TestMulNumberAndRational()
    {
        var exp = new Mul(
            Number.One,
            new Rational(Number.Two, Number.One)
        );

        Test(exp, ResultTypes.RationalNumber);
    }

    [Fact]
    public void TestMulRationalAndNumber()
    {
        var exp = new Mul(
            new Rational(Number.One, Number.Two),
            Number.One
        );

        Test(exp, ResultTypes.RationalNumber);
    }

    public static IEnumerable<object[]> GetDataForTestMulAngleAndBoolTest()
    {
        yield return new object[] { AngleValue.Degree(90).AsExpression(), Bool.False };
        yield return new object[] { Bool.False, AngleValue.Degree(90).AsExpression() };

        yield return new object[] { PowerValue.Watt(90).AsExpression(), Bool.False };
        yield return new object[] { Bool.False, PowerValue.Watt(90).AsExpression() };

        yield return new object[] { TemperatureValue.Celsius(90).AsExpression(), Bool.False };
        yield return new object[] { Bool.False, TemperatureValue.Celsius(90).AsExpression() };

        yield return new object[] { MassValue.Gram(90).AsExpression(), Bool.False };
        yield return new object[] { Bool.False, MassValue.Gram(90).AsExpression() };

        yield return new object[] { LengthValue.Meter(90).AsExpression(), Bool.False };
        yield return new object[] { Bool.False, LengthValue.Meter(90).AsExpression() };

        yield return new object[] { TimeValue.Second(90).AsExpression(), Bool.False };
        yield return new object[] { Bool.False, TimeValue.Second(90).AsExpression() };

        yield return new object[] { AreaValue.Meter(90).AsExpression(), Bool.False };
        yield return new object[] { Bool.False, AreaValue.Meter(90).AsExpression() };

        yield return new object[] { VolumeValue.Meter(90).AsExpression(), Bool.False };
        yield return new object[] { Bool.False, VolumeValue.Meter(90).AsExpression() };
    }

    [Theory]
    [MemberData(nameof(GetDataForTestMulAngleAndBoolTest))]
    public void TestMulAngleAndBoolTest(IExpression left, IExpression right)
    {
        var exp = new Mul(left, right);

        TestBinaryException(exp);
    }
}