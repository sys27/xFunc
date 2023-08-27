// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class MulSimplifierTest : BaseSimplifierTest
{
    [Test]
    public void Order1()
    {
        var add = new Mul(Variable.X, Number.Two);
        var expected = new Mul(Number.Two, Variable.X);

        SimplifyTest(add, expected);
    }

    [Test]
    public void MulByFirstZero()
    {
        var mul = new Mul(Number.Zero, new Number(10));
        var expected = Number.Zero;

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulBySecondZero()
    {
        var mul = new Mul(new Number(10), Number.Zero);
        var expected = Number.Zero;

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulFirstOne()
    {
        var mul = new Mul(Number.One, new Number(10));
        var expected = new Number(10);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSecondOne()
    {
        var mul = new Mul(new Number(10), Number.One);
        var expected = new Number(10);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulFirstMinusOne()
    {
        var mul = new Mul(new Number(-1), new Number(10));
        var expected = new UnaryMinus(new Number(10));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSecondMinusOne()
    {
        var mul = new Mul(new Number(10), new Number(-1));
        var expected = new UnaryMinus(new Number(10));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulTwoNumbers()
    {
        var mul = new Mul(Number.Two, new Number(3));
        var expected = new Number(6);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulNumberAngle()
    {
        var mul = new Mul(
            new Number(90),
            AngleValue.Degree(2).AsExpression()
        );
        var expected = AngleValue.Degree(180).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulAngleNumber()
    {
        var mul = new Mul(
            AngleValue.Degree(90).AsExpression(),
            new Number(2)
        );
        var expected = AngleValue.Degree(180).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulNumberByPower()
    {
        var mul = new Mul(
            new Number(10),
            PowerValue.Watt(2).AsExpression()
        );
        var expected = PowerValue.Watt(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulPowerByNumber()
    {
        var mul = new Mul(
            PowerValue.Watt(2).AsExpression(),
            new Number(10)
        );
        var expected = PowerValue.Watt(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulNumberByTemperature()
    {
        var mul = new Mul(
            new Number(10),
            TemperatureValue.Celsius(2).AsExpression()
        );
        var expected = TemperatureValue.Celsius(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulTemperatureByNumber()
    {
        var mul = new Mul(
            TemperatureValue.Celsius(2).AsExpression(),
            new Number(10)
        );
        var expected = TemperatureValue.Celsius(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulNumberByMass()
    {
        var mul = new Mul(
            new Number(10),
            MassValue.Gram(2).AsExpression()
        );
        var expected = MassValue.Gram(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulMassByNumber()
    {
        var mul = new Mul(
            MassValue.Gram(2).AsExpression(),
            new Number(10)
        );
        var expected = MassValue.Gram(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulNumberByLength()
    {
        var mul = new Mul(
            new Number(10),
            LengthValue.Meter(2).AsExpression()
        );
        var expected = LengthValue.Meter(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulLengthByNumber()
    {
        var mul = new Mul(
            LengthValue.Meter(2).AsExpression(),
            new Number(10)
        );
        var expected = LengthValue.Meter(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulLengthByLength()
    {
        var mul = new Mul(
            LengthValue.Meter(2).AsExpression(),
            LengthValue.Meter(2).AsExpression()
        );
        var expected = AreaValue.Meter(4).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulAreaByLength()
    {
        var mul = new Mul(
            AreaValue.Meter(2).AsExpression(),
            LengthValue.Meter(2).AsExpression()
        );
        var expected = VolumeValue.Meter(4).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulLengthByArea()
    {
        var mul = new Mul(
            LengthValue.Meter(2).AsExpression(),
            AreaValue.Meter(2).AsExpression()
        );
        var expected = VolumeValue.Meter(4).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulNumberByTime()
    {
        var mul = new Mul(
            new Number(10),
            TimeValue.Second(2).AsExpression()
        );
        var expected = TimeValue.Second(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulTimeByNumber()
    {
        var mul = new Mul(
            TimeValue.Second(2).AsExpression(),
            new Number(10)
        );
        var expected = TimeValue.Second(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulNumberByArea()
    {
        var mul = new Mul(
            new Number(10),
            AreaValue.Meter(2).AsExpression()
        );
        var expected = AreaValue.Meter(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulAreaByNumber()
    {
        var mul = new Mul(
            AreaValue.Meter(2).AsExpression(),
            new Number(10)
        );
        var expected = AreaValue.Meter(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulNumberByVolume()
    {
        var mul = new Mul(
            new Number(10),
            VolumeValue.Meter(2).AsExpression()
        );
        var expected = VolumeValue.Meter(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulVolumeByNumber()
    {
        var mul = new Mul(
            VolumeValue.Meter(2).AsExpression(),
            new Number(10)
        );
        var expected = VolumeValue.Meter(20).AsExpression();

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulDiffNumMul_NumMulVar_()
    {
        var mul = new Mul(Number.Two, new Mul(Number.Two, Variable.X));
        var expected = new Mul(new Number(4), Variable.X);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulDiffNumMul_VarMulNum_()
    {
        var mul = new Mul(Number.Two, new Mul(Variable.X, Number.Two));
        var expected = new Mul(new Number(4), Variable.X);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulDiff_NumMulVar_MulNum()
    {
        var mul = new Mul(new Mul(Number.Two, Variable.X), Number.Two);
        var expected = new Mul(new Number(4), Variable.X);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulDiff_VarMulNum_MulNum()
    {
        var mul = new Mul(new Mul(Variable.X, Number.Two), Number.Two);
        var expected = new Mul(new Number(4), Variable.X);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulDiffNumMul_NumDivVar_()
    {
        var mul = new Mul(Number.Two, new Div(Number.Two, Variable.X));
        var expected = new Div(new Number(4), Variable.X);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulDiffNumMul_VarDivNum_()
    {
        var mul = new Mul(Number.Two, new Div(Variable.X, Number.Two));
        var expected = Variable.X;

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulDiff_NumDivVar_MulNum()
    {
        var mul = new Mul(new Div(Number.Two, Variable.X), Number.Two);
        var expected = new Div(new Number(4), Variable.X);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulDiff_VarDivNum_MulNum()
    {
        var mul = new Mul(new Div(Variable.X, Number.Two), Number.Two);
        var expected = Variable.X;

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar1()
    {
        var mul = new Mul(Variable.X, Variable.X);
        var expected = new Pow(Variable.X, Number.Two);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar2()
    {
        var mul = new Mul(new Mul(Number.Two, Variable.X), Variable.X);
        var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulComplexX()
    {
        var mul = new Mul(
            new Mul(Number.Two, new Add(Variable.X, Variable.Y)),
            new Add(Variable.X, Variable.Y)
        );
        var expected = new Mul(
            Number.Two,
            new Pow(new Add(Variable.X, Variable.Y), Number.Two)
        );

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar3()
    {
        var mul = new Mul(
            new Mul(Number.Two, Variable.X),
            new Mul(new Number(3), Variable.X)
        );
        var expected = new Mul(new Number(6), new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulComplexX2()
    {
        var mul = new Mul(
            new Mul(Number.Two, new Add(Variable.X, Variable.Y)),
            new Mul(new Number(3), new Add(Variable.X, Variable.Y))
        );
        var expected = new Mul(
            new Number(6),
            new Pow(new Add(Variable.X, Variable.Y), Number.Two)
        );

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar4()
    {
        var mul = new Mul(Variable.X, new Mul(Number.Two, Variable.X));
        var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar5()
    {
        var mul = new Mul(Variable.X, new Mul(Variable.X, Number.Two));
        var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar6()
    {
        var mul = new Mul(new Mul(Number.Two, Variable.X), Variable.X);
        var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar7()
    {
        var mul = new Mul(new Mul(Variable.X, Number.Two), Variable.X);
        var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar8()
    {
        var mul = new Mul(
            new Mul(Number.Two, Variable.X),
            new Mul(new Number(3), Variable.X)
        );
        var expected = new Mul(new Number(6), new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar9()
    {
        var mul = new Mul(
            new Mul(Variable.X, Number.Two),
            new Mul(Variable.X, new Number(3))
        );
        var expected = new Mul(new Number(6), new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar10()
    {
        var mul = new Mul(
            new Mul(Number.Two, Variable.X),
            new Mul(new Number(0.5), Variable.X)
        );
        var expected = new Pow(Variable.X, Number.Two);

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar11()
    {
        var mul = new Mul(
            new Mul(Number.Two, Variable.X),
            new Mul(new Number(-0.5), Variable.X)
        );
        var expected = new UnaryMinus(new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar12()
    {
        var mul = new Mul(
            new Mul(Number.Two, Variable.X),
            new Mul(Variable.X, new Number(-3))
        );
        var expected = new Mul(new Number(-6), new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulSameVar13()
    {
        var mul = new Mul(
            new Mul(Variable.X, Number.Two),
            new Mul(new Number(-3), Variable.X)
        );
        var expected = new Mul(new Number(-6), new Pow(Variable.X, Number.Two));

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulNegativeRightParamTest1()
    {
        var mul = new Mul(
            new Cos(new Cos(Variable.X)),
            new UnaryMinus(new Sin(Variable.X))
        );
        var expected = new UnaryMinus(new Mul(
            new Cos(new Cos(Variable.X)),
            new Sin(Variable.X))
        );

        SimplifyTest(mul, expected);
    }

    [Test]
    public void MulArgumentSimplified()
    {
        var exp = new Mul(
            Variable.X,
            new Ceil(new Add(Number.One, Number.One))
        );
        var expected = new Mul(Variable.X, new Ceil(Number.Two));

        SimplifyTest(exp, expected);
    }

    [Test]
    public void MulDiv1()
    {
        var exp = new Mul(
            Variable.X,
            new Div(Number.One, Variable.X)
        );
        var expected = Number.One;

        SimplifyTest(exp, expected);
    }

    [Test]
    public void MulDiv2()
    {
        var exp = new Mul(
            new Mul(Number.Two, Variable.X),
            new Div(Number.One, Variable.X)
        );
        var expected = Number.Two;

        SimplifyTest(exp, expected);
    }

    [Test]
    public void MulDiv3()
    {
        var exp = new Mul(
            new Mul(Variable.X, Number.Two),
            new Div(Number.One, Variable.X)
        );
        var expected = Number.Two;

        SimplifyTest(exp, expected);
    }
}