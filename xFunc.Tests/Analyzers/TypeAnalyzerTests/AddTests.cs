// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class AddTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestAddTwoNumberTest()
    {
        var add = new Add(Number.One, Number.Two);

        Test(add, ResultTypes.Number);
    }

    [Test]
    public void TestAddNumberVarTest()
    {
        var add = new Add(Number.One, Variable.X);

        Test(add, ResultTypes.Undefined);
    }

    [Test]
    public void TestAddComplicatedTest()
    {
        var add = new Add(new Mul(Number.One, Number.Two), Variable.X);

        Test(add, ResultTypes.Undefined);
    }

    [Test]
    public void TestAddTwoVectorTest()
    {
        var add = new Add(
            new Vector(new IExpression[] { Number.One }),
            new Vector(new IExpression[] { Number.Two })
        );

        Test(add, ResultTypes.Vector);
    }

    [Test]
    public void TestAddTwoMatrixTest()
    {
        var add = new Add(
            new Matrix(new[] { new Vector(new IExpression[] { Number.One }) }),
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        Test(add, ResultTypes.Matrix);
    }

    [Test]
    public void TestAddNumberVectorTest()
    {
        var exp = new Add(
            Number.One,
            new Vector(new IExpression[] { Number.One })
        );

        TestBinaryException(exp);
    }

    [Test]
    public void TestAddBoolVectorException()
    {
        var exp = new Add(Bool.True, new Vector(new IExpression[] { Number.One }));

        TestBinaryException(exp);
    }

    [Test]
    public void TestAddVectorNumberTest()
    {
        var exp = new Add(new Vector(new IExpression[] { Number.One }), Bool.True);

        TestBinaryException(exp);
    }

    [Test]
    public void TestAddNumberMatrixTest()
    {
        var exp = new Add(
            Number.One,
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        TestBinaryException(exp);
    }

    [Test]
    public void TestAddMatrixNumberTest()
    {
        var exp = new Add(
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
            Number.One
        );

        TestBinaryException(exp);
    }

    [Test]
    public void TestAddVectorMatrixTest()
    {
        var exp = new Add(
            new Vector(new IExpression[] { Number.One }),
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        TestBinaryException(exp);
    }

    [Test]
    public void TestAddMatrixVectorTest()
    {
        var exp = new Add(
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
            new Vector(new IExpression[] { Number.One })
        );

        TestBinaryException(exp);
    }

    [Test]
    public void TestAddNumberComplexNumberTest()
    {
        var add = new Add(Number.One, new ComplexNumber(2, 1));

        Test(add, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestAddBoolComplexNumberException()
    {
        var add = new Add(Bool.True, new ComplexNumber(2, 1));

        TestBinaryException(add);
    }

    [Test]
    public void TestAddComplexNumberNumberTest()
    {
        var add = new Add(new ComplexNumber(1, 3), Number.Two);

        Test(add, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestAddComplexNumberBoolException()
    {
        var add = new Add(new ComplexNumber(1, 3), Bool.True);

        TestBinaryException(add);
    }

    [Test]
    public void TestAddComplexNumberComplexNumberTest()
    {
        var add = new Add(new ComplexNumber(1, 3), new ComplexNumber(2, 5));

        Test(add, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestAddNumberAllTest()
    {
        var exp = new Add(
            Number.One,
            new CallExpression(
                new Variable("f"),
                new IExpression[] { Number.One }.ToImmutableArray()));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestAddComplexNumberAllTest()
    {
        var exp = new Add(
            new ComplexNumber(3, 2),
            new CallExpression(
                new Variable("f"),
                new IExpression[] { Number.One }.ToImmutableArray()));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestAddVectorAllTest()
    {
        var exp = new Add(
            new Vector(new IExpression[] { Number.One }),
            new CallExpression(
                new Variable("f"),
                new IExpression[] { Number.One }.ToImmutableArray()));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestAddMatrixAllTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });
        var exp = new Add(
            matrix,
            new CallExpression(
                new Variable("f"),
                new IExpression[] { Number.One }.ToImmutableArray()));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestAddBoolAndMatrixTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });
        var exp = new Add(Bool.True, matrix);

        TestBinaryException(exp);
    }

    [Test]
    public void TestAddMatrixAndBoolTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });
        var exp = new Add(matrix, Bool.True);

        TestBinaryException(exp);
    }

    [Test]
    public void TestAddNumberSqrtComplexTest()
    {
        var exp = new Add(Number.Two, new Sqrt(new Number(-9)));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestAddTwoVarTest()
    {
        var exp = new Add(Variable.X, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestAddThreeVarTest()
    {
        var exp = new Add(new Add(Variable.X, Variable.X), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestAddException()
    {
        var exp = new Add(Bool.False, Bool.False);

        TestException(exp);
    }

    [Test]
    public void TestAddNumberAngle()
    {
        var exp = new Add(
            new Number(10),
            AngleValue.Radian(10).AsExpression()
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestAddAngleNumber()
    {
        var exp = new Add(
            AngleValue.Degree(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestAddAngleAngle()
    {
        var exp = new Add(
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Radian(10).AsExpression()
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestAddNumberPower()
    {
        var exp = new Add(
            new Number(10),
            PowerValue.Watt(10).AsExpression()
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestAddPowerNumber()
    {
        var exp = new Add(
            PowerValue.Watt(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestAddPowerPower()
    {
        var exp = new Add(
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestAddNumberTemperature()
    {
        var exp = new Add(
            new Number(10),
            TemperatureValue.Celsius(10).AsExpression()
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestAddTemperatureNumber()
    {
        var exp = new Add(
            TemperatureValue.Celsius(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestAddTemperatureTemperature()
    {
        var exp = new Add(
            TemperatureValue.Celsius(10).AsExpression(),
            TemperatureValue.Celsius(10).AsExpression()
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestAddNumberAndMass()
    {
        var exp = new Add(
            new Number(10),
            MassValue.Gram(10).AsExpression()
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestAddMassAndNumber()
    {
        var exp = new Add(
            MassValue.Gram(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestAddMassAndMass()
    {
        var exp = new Add(
            MassValue.Gram(10).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestAddNumberAndLength()
    {
        var exp = new Add(
            new Number(10),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestAddLengthAndNumber()
    {
        var exp = new Add(
            LengthValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestAddLengthAndLength()
    {
        var exp = new Add(
            LengthValue.Meter(10).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestAddNumberAndTime()
    {
        var exp = new Add(
            new Number(10),
            TimeValue.Second(10).AsExpression()
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestAddTimeAndNumber()
    {
        var exp = new Add(
            TimeValue.Second(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestAddTimeAndTime()
    {
        var exp = new Add(
            TimeValue.Second(10).AsExpression(),
            TimeValue.Second(10).AsExpression()
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestAddNumberAndArea()
    {
        var exp = new Add(
            new Number(10),
            AreaValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestAddAreaAndNumber()
    {
        var exp = new Add(
            AreaValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestAddAreaAndArea()
    {
        var exp = new Add(
            AreaValue.Meter(10).AsExpression(),
            AreaValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestAddNumberAndVolume()
    {
        var exp = new Add(
            new Number(10),
            VolumeValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestAddVolumeAndNumber()
    {
        var exp = new Add(
            VolumeValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestAddVolumeAndVolume()
    {
        var exp = new Add(
            VolumeValue.Meter(10).AsExpression(),
            VolumeValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestAddRationalAndRational()
    {
        var exp = new Add(
            new Rational(Number.One, Number.Two),
            new Rational(Number.Two, Number.One)
        );

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestAddNumberAndRational()
    {
        var exp = new Add(
            Number.One,
            new Rational(Number.Two, Number.One)
        );

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestAddRationalAndNumber()
    {
        var exp = new Add(
            new Rational(Number.One, Number.Two),
            Number.One
        );

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestAddStringToString()
    {
        var exp = new Add(
            new StringExpression("a"),
            new StringExpression("b")
        );

        Test(exp, ResultTypes.String);
    }

    [Test]
    public void TestAddStringToNumber()
    {
        var exp = new Add(
            new StringExpression("a"),
            Number.One
        );

        Test(exp, ResultTypes.String);
    }

    [Test]
    public void TestAddNumberToString()
    {
        var exp = new Add(
            Number.One,
            new StringExpression("a")
        );

        Test(exp, ResultTypes.String);
    }

    public static IEnumerable<object[]> GetDataForTestAddAngleAndBoolTest()
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
    [TestCaseSource(nameof(GetDataForTestAddAngleAndBoolTest))]
    public void TestAddAngleAndBoolTest(IExpression left, IExpression right)
    {
        var exp = new Add(left, right);

        TestBinaryException(exp);
    }
}