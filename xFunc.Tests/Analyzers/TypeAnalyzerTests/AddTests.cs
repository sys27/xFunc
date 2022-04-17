// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class AddTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestAddTwoNumberTest()
    {
        var add = new Add(Number.One, Number.Two);

        Test(add, ResultTypes.Number);
    }

    [Fact]
    public void TestAddNumberVarTest()
    {
        var add = new Add(Number.One, Variable.X);

        Test(add, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAddComplicatedTest()
    {
        var add = new Add(new Mul(Number.One, Number.Two), Variable.X);

        Test(add, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAddTwoVectorTest()
    {
        var add = new Add(
            new Vector(new IExpression[] { Number.One }),
            new Vector(new IExpression[] { Number.Two })
        );

        Test(add, ResultTypes.Vector);
    }

    [Fact]
    public void TestAddTwoMatrixTest()
    {
        var add = new Add(
            new Matrix(new[] { new Vector(new IExpression[] { Number.One }) }),
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        Test(add, ResultTypes.Matrix);
    }

    [Fact]
    public void TestAddNumberVectorTest()
    {
        var exp = new Add(
            Number.One,
            new Vector(new IExpression[] { Number.One })
        );

        TestBinaryException(exp);
    }

    [Fact]
    public void TestAddBoolVectorException()
    {
        var exp = new Add(Bool.True, new Vector(new IExpression[] { Number.One }));

        TestBinaryException(exp);
    }

    [Fact]
    public void TestAddVectorNumberTest()
    {
        var exp = new Add(new Vector(new IExpression[] { Number.One }), Bool.True);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestAddNumberMatrixTest()
    {
        var exp = new Add(
            Number.One,
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        TestBinaryException(exp);
    }

    [Fact]
    public void TestAddMatrixNumberTest()
    {
        var exp = new Add(
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
            Number.One
        );

        TestBinaryException(exp);
    }

    [Fact]
    public void TestAddVectorMatrixTest()
    {
        var exp = new Add(
            new Vector(new IExpression[] { Number.One }),
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
        );

        TestBinaryException(exp);
    }

    [Fact]
    public void TestAddMatrixVectorTest()
    {
        var exp = new Add(
            new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
            new Vector(new IExpression[] { Number.One })
        );

        TestBinaryException(exp);
    }

    [Fact]
    public void TestAddNumberComplexNumberTest()
    {
        var add = new Add(Number.One, new ComplexNumber(2, 1));

        Test(add, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestAddBoolComplexNumberException()
    {
        var add = new Add(Bool.True, new ComplexNumber(2, 1));

        TestBinaryException(add);
    }

    [Fact]
    public void TestAddComplexNumberNumberTest()
    {
        var add = new Add(new ComplexNumber(1, 3), Number.Two);

        Test(add, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestAddComplexNumberBoolException()
    {
        var add = new Add(new ComplexNumber(1, 3), Bool.True);

        TestBinaryException(add);
    }

    [Fact]
    public void TestAddComplexNumberComplexNumberTest()
    {
        var add = new Add(new ComplexNumber(1, 3), new ComplexNumber(2, 5));

        Test(add, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestAddNumberAllTest()
    {
        var exp = new Add(
            Number.One,
            new UserFunction("f", new IExpression[] { Number.One }));

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAddComplexNumberAllTest()
    {
        var exp = new Add(
            new ComplexNumber(3, 2),
            new UserFunction("f", new IExpression[] { Number.One }));

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAddVectorAllTest()
    {
        var vector = new Vector(new IExpression[] { Number.One });
        var exp = new Add(vector, new UserFunction("f", new IExpression[] { Number.One }));

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAddMatrixAllTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });
        var exp = new Add(matrix, new UserFunction("f", new IExpression[] { Number.One }));

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAddBoolAndMatrixTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });
        var exp = new Add(Bool.True, matrix);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestAddMatrixAndBoolTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });
        var exp = new Add(matrix, Bool.True);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestAddNumberSqrtComplexTest()
    {
        var exp = new Add(Number.Two, new Sqrt(new Number(-9)));

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAddTwoVarTest()
    {
        var exp = new Add(Variable.X, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAddThreeVarTest()
    {
        var exp = new Add(new Add(Variable.X, Variable.X), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAddException()
    {
        var exp = new Add(Bool.False, Bool.False);

        TestException(exp);
    }

    [Fact]
    public void TestAddNumberAngle()
    {
        var exp = new Add(
            new Number(10),
            AngleValue.Radian(10).AsExpression()
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestAddAngleNumber()
    {
        var exp = new Add(
            AngleValue.Degree(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestAddAngleAngle()
    {
        var exp = new Add(
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Radian(10).AsExpression()
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestAddNumberPower()
    {
        var exp = new Add(
            new Number(10),
            PowerValue.Watt(10).AsExpression()
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestAddPowerNumber()
    {
        var exp = new Add(
            PowerValue.Watt(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestAddPowerPower()
    {
        var exp = new Add(
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestAddNumberTemperature()
    {
        var exp = new Add(
            new Number(10),
            TemperatureValue.Celsius(10).AsExpression()
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestAddTemperatureNumber()
    {
        var exp = new Add(
            TemperatureValue.Celsius(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestAddTemperatureTemperature()
    {
        var exp = new Add(
            TemperatureValue.Celsius(10).AsExpression(),
            TemperatureValue.Celsius(10).AsExpression()
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestAddNumberAndMass()
    {
        var exp = new Add(
            new Number(10),
            MassValue.Gram(10).AsExpression()
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestAddMassAndNumber()
    {
        var exp = new Add(
            MassValue.Gram(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestAddMassAndMass()
    {
        var exp = new Add(
            MassValue.Gram(10).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestAddNumberAndLength()
    {
        var exp = new Add(
            new Number(10),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestAddLengthAndNumber()
    {
        var exp = new Add(
            LengthValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestAddLengthAndLength()
    {
        var exp = new Add(
            LengthValue.Meter(10).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestAddNumberAndTime()
    {
        var exp = new Add(
            new Number(10),
            TimeValue.Second(10).AsExpression()
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestAddTimeAndNumber()
    {
        var exp = new Add(
            TimeValue.Second(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestAddTimeAndTime()
    {
        var exp = new Add(
            TimeValue.Second(10).AsExpression(),
            TimeValue.Second(10).AsExpression()
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestAddNumberAndArea()
    {
        var exp = new Add(
            new Number(10),
            AreaValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestAddAreaAndNumber()
    {
        var exp = new Add(
            AreaValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestAddAreaAndArea()
    {
        var exp = new Add(
            AreaValue.Meter(10).AsExpression(),
            AreaValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestAddNumberAndVolume()
    {
        var exp = new Add(
            new Number(10),
            VolumeValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestAddVolumeAndNumber()
    {
        var exp = new Add(
            VolumeValue.Meter(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestAddVolumeAndVolume()
    {
        var exp = new Add(
            VolumeValue.Meter(10).AsExpression(),
            VolumeValue.Meter(10).AsExpression()
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestAddStringToString()
    {
        var exp = new Add(
            new StringExpression("a"),
            new StringExpression("b")
        );

        Test(exp, ResultTypes.String);
    }

    [Fact]
    public void TestAddStringToNumber()
    {
        var exp = new Add(
            new StringExpression("a"),
            Number.One
        );

        Test(exp, ResultTypes.String);
    }

    [Fact]
    public void TestAddNumberToString()
    {
        var exp = new Add(
            Number.One,
            new StringExpression("a")
        );

        Test(exp, ResultTypes.String);
    }
}