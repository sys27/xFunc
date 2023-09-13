// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class SubTests : TypeAnalyzerBaseTests
{
    [Test]
    public void SubTwoNumberTest()
    {
        var sub = new Sub(Number.One, Number.Two);

        Test(sub, ResultTypes.Number);
    }

    [Test]
    public void SubNumberVarTest()
    {
        var sub = new Sub(Number.One, Variable.X);

        Test(sub, ResultTypes.Undefined);
    }

    [Test]
    public void SubComplicatedTest()
    {
        var sub = new Sub(new Mul(Number.One, Number.Two), Variable.X);

        Test(sub, ResultTypes.Undefined);
    }

    [Test]
    public void SubTwoVectorTest()
    {
        var sub = new Sub(
            new Vector(new IExpression[] { Number.One }),
            new Vector(new IExpression[] { Number.Two })
        );

        Test(sub, ResultTypes.Vector);
    }

    [Test]
    public void SubTwoMatrixTest()
    {
        var sub = new Sub(
            new Matrix(new[] { new Vector(new IExpression[] { Number.One }) }),
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        Test(sub, ResultTypes.Matrix);
    }

    [Test]
    public void SubNumberVectorTest()
    {
        var exp = new Sub(
            Number.One,
            new Vector(new IExpression[] { Number.One })
        );

        TestBinaryException(exp);
    }

    [Test]
    public void SubVectorNumberTest()
    {
        var exp = new Sub(
            new Vector(new IExpression[] { Number.One }),
            Number.One
        );

        TestBinaryException(exp);
    }

    [Test]
    public void SubNumberMatrixTest()
    {
        var exp = new Sub(
            Number.One,
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        TestBinaryException(exp);
    }

    [Test]
    public void SubMatrixNumberTest()
    {
        var exp = new Sub(
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
            Number.One
        );

        TestBinaryException(exp);
    }

    [Test]
    public void SubVectorMatrixTest()
    {
        var exp = new Sub(
            new Vector(new IExpression[] { Number.One }),
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        TestBinaryException(exp);
    }

    [Test]
    public void SubMatrixVectorTest()
    {
        var exp = new Sub(
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
            new Vector(new IExpression[] { Number.One })
        );

        TestBinaryException(exp);
    }

    [Test]
    public void SubNumberComplexNumberTest()
    {
        var sub = new Sub(Number.One, new ComplexNumber(2, 1));

        Test(sub, ResultTypes.ComplexNumber);
    }

    [Test]
    public void SubComplexNumberNumberTest()
    {
        var sub = new Sub(new ComplexNumber(1, 3), Number.Two);

        Test(sub, ResultTypes.ComplexNumber);
    }

    [Test]
    public void SubComplexNumberComplexNumberTest()
    {
        var sub = new Sub(new ComplexNumber(1, 3), new ComplexNumber(3, 5));

        Test(sub, ResultTypes.ComplexNumber);
    }

    [Test]
    public void SubComplexNumberBoolException()
    {
        var sub = new Sub(new ComplexNumber(1, 3), Bool.True);

        TestBinaryException(sub);
    }

    [Test]
    public void SubBoolComplexNumberException()
    {
        var sub = new Sub(Bool.True, new ComplexNumber(1, 3));

        TestBinaryException(sub);
    }

    [Test]
    public void SubNumberAllTest()
    {
        var exp = new Sub(
            Number.One,
            new CallExpression(
                new Variable("f"),
                new IExpression[] { Number.One }.ToImmutableArray()));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void SubComplexNumberAllTest()
    {
        var exp = new Sub(
            new ComplexNumber(3, 2),
            new CallExpression(
                new Variable("f"),
                new IExpression[] { Number.One }.ToImmutableArray()));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void SubVectorVectorTest()
    {
        var left = new Vector(new IExpression[] { new Number(3) });
        var right = new Vector(new IExpression[] { Number.One });
        var exp = new Sub(left, right);

        Test(exp, ResultTypes.Vector);
    }

    [Test]
    public void SubVectorBoolTest()
    {
        var vector = new Vector(new IExpression[] { Number.One });
        var exp = new Sub(vector, Bool.True);

        TestBinaryException(exp);
    }

    [Test]
    public void SubBoolVectorTest()
    {
        var vector = new Vector(new IExpression[] { Number.One });
        var exp = new Sub(Bool.True, vector);

        TestBinaryException(exp);
    }

    [Test]
    public void SubVectorAllTest()
    {
        var exp = new Sub(
            new Vector(new IExpression[] { Number.One }),
            new CallExpression(
                new Variable("f"),
                new IExpression[] { Number.One }.ToImmutableArray()));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void SubMatrixAllTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });
        var exp = new Sub(
            matrix,
            new CallExpression(
                new Variable("f"),
                new IExpression[] { Number.One }.ToImmutableArray()));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestSubBoolAndMatrixTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });
        var exp = new Sub(Bool.True, matrix);

        TestBinaryException(exp);
    }

    [Test]
    public void TestSubMatrixAndBoolTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });
        var exp = new Sub(matrix, Bool.True);

        TestBinaryException(exp);
    }

    [Test]
    public void SubNumberComplexTest()
    {
        var exp = new Sub(Number.Two, new Sqrt(new Number(-9)));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void SubTwoVarTest()
    {
        var exp = new Sub(Variable.X, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void SubThreeVarTest()
    {
        var exp = new Sub(new Add(Variable.X, Variable.X), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestSubBoolsException()
    {
        var exp = new Sub(Bool.False, Bool.False);

        TestException(exp);
    }

    [Test]
    public void TestSubNumberAngle()
    {
        var exp = new Sub(
            new Number(10),
            AngleValue.Radian(10).AsExpression()
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestSubAngleNumber()
    {
        var exp = new Sub(
            AngleValue.Degree(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestSubAngleAngle()
    {
        var exp = new Sub(
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Radian(10).AsExpression()
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestSubNumberPower()
    {
        var exp = new Sub(
            new Number(10),
            PowerValue.Watt(10).AsExpression()
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestSubPowerNumber()
    {
        var exp = new Sub(
            PowerValue.Watt(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestSubPowerPower()
    {
        var exp = new Sub(
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestSubNumberTemperature()
    {
        var exp = new Sub(
            new Number(10),
            TemperatureValue.Celsius(10).AsExpression()
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestSubTemperatureNumber()
    {
        var exp = new Sub(
            TemperatureValue.Celsius(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestSubTemperatureTemperature()
    {
        var exp = new Sub(
            TemperatureValue.Celsius(10).AsExpression(),
            TemperatureValue.Celsius(10).AsExpression()
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestSubNumberAndMass()
    {
        var exp = new Sub(
            new Number(10),
            MassValue.Gram(10).AsExpression()
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestSubMassAndNumber()
    {
        var exp = new Sub(
            MassValue.Gram(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestSubMassAndMass()
    {
        var exp = new Sub(
            MassValue.Gram(10).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestSubNumberAndLength()
    {
        var exp = new Sub(
            new Number(10),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestSubLengthAndNumber()
    {
        var exp = new Sub(
            LengthValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestSubLengthAndLength()
    {
        var exp = new Sub(
            LengthValue.Meter(10).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestSubNumberAndTime()
    {
        var exp = new Sub(
            new Number(10),
            TimeValue.Second(10).AsExpression()
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestSubTimeAndNumber()
    {
        var exp = new Sub(
            TimeValue.Second(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestSubTimeAndTime()
    {
        var exp = new Sub(
            TimeValue.Second(10).AsExpression(),
            TimeValue.Second(10).AsExpression()
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestSubNumberAndArea()
    {
        var exp = new Sub(
            new Number(10),
            AreaValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestSubAreaAndNumber()
    {
        var exp = new Sub(
            AreaValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestSubAreaAndArea()
    {
        var exp = new Sub(
            AreaValue.Meter(10).AsExpression(),
            AreaValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestSubNumberAndVolume()
    {
        var exp = new Sub(
            new Number(10),
            VolumeValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestSubVolumeAndNumber()
    {
        var exp = new Sub(
            VolumeValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestSubVolumeAndVolume()
    {
        var exp = new Sub(
            VolumeValue.Meter(10).AsExpression(),
            VolumeValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestSubRationalAndRational()
    {
        var exp = new Sub(
            new Rational(Number.One, Number.Two),
            new Rational(Number.Two, Number.One)
        );

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestSubNumberAndRational()
    {
        var exp = new Sub(
            Number.One,
            new Rational(Number.Two, Number.One)
        );

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestSubRationalAndNumber()
    {
        var exp = new Sub(
            new Rational(Number.One, Number.Two),
            Number.One
        );

        Test(exp, ResultTypes.RationalNumber);
    }

    public static IEnumerable<object[]> GetDataForTestSubAngleAndBoolTest()
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

    [Test]
    [TestCaseSource(nameof(GetDataForTestSubAngleAndBoolTest))]
    public void TestSubAngleAndBoolTest(IExpression left, IExpression right)
    {
        var exp = new Sub(left, right);

        TestBinaryException(exp);
    }
}