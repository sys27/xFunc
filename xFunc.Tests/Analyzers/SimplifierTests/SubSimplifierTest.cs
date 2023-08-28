// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class SubSimplifierTest : BaseSimplifierTest
{
    [Test]
    public void SubFirstZero()
    {
        var sub = new Sub(Number.Zero, Variable.X);
        var expected = new UnaryMinus(Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSecondZero()
    {
        var sub = new Sub(Variable.X, Number.Zero);
        var expected = Variable.X;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubTwoNumbers()
    {
        var sub = new Sub(new Number(3), Number.Two);
        var expected = Number.One;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubNumberAngle()
    {
        var sub = new Sub(
            new Number(90),
            AngleValue.Degree(2).AsExpression()
        );
        var expected = AngleValue.Degree(88).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubAngleNumber()
    {
        var sub = new Sub(
            AngleValue.Degree(90).AsExpression(),
            new Number(2)
        );
        var expected = AngleValue.Degree(88).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubTwoAngles()
    {
        var sub = new Sub(
            AngleValue.Radian(2).AsExpression(),
            AngleValue.Degree(90).AsExpression()
        );
        var expected = AngleValue.Degree(114.59155902616465 - 90).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubPowerFromNumber()
    {
        var sub = new Sub(
            new Number(20),
            PowerValue.Watt(10).AsExpression()
        );
        var expected = PowerValue.Watt(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubNumberFromPower()
    {
        var sub = new Sub(
            PowerValue.Watt(20).AsExpression(),
            new Number(10)
        );
        var expected = PowerValue.Watt(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubPowerFromPower()
    {
        var sub = new Sub(
            PowerValue.Kilowatt(20).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );
        var expected = PowerValue.Watt(19990).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubTemperatureFromNumber()
    {
        var sub = new Sub(
            new Number(20),
            TemperatureValue.Celsius(10).AsExpression()
        );
        var expected = TemperatureValue.Celsius(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubNumberFromTemperature()
    {
        var sub = new Sub(
            TemperatureValue.Celsius(20).AsExpression(),
            new Number(10)
        );
        var expected = TemperatureValue.Celsius(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubTemperatureFromTemperature()
    {
        var sub = new Sub(
            TemperatureValue.Kelvin(20).AsExpression(),
            TemperatureValue.Celsius(10).AsExpression()
        );
        var expected = TemperatureValue.Kelvin(20 - 283.15).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubMassFromNumber()
    {
        var sub = new Sub(
            new Number(20),
            MassValue.Gram(10).AsExpression()
        );
        var expected = MassValue.Gram(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubNumberFromMass()
    {
        var sub = new Sub(
            MassValue.Gram(20).AsExpression(),
            new Number(10)
        );
        var expected = MassValue.Gram(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubMassFromMass()
    {
        var sub = new Sub(
            MassValue.Kilogram(20).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );
        var expected = MassValue.Kilogram(19.990).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubLengthFromNumber()
    {
        var sub = new Sub(
            new Number(20),
            LengthValue.Meter(10).AsExpression()
        );
        var expected = LengthValue.Meter(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubNumberFromLength()
    {
        var sub = new Sub(
            LengthValue.Meter(20).AsExpression(),
            new Number(10)
        );
        var expected = LengthValue.Meter(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubLengthFromLength()
    {
        var sub = new Sub(
            LengthValue.Kilometer(20).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );
        var expected = LengthValue.Kilometer(19.990).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubTimeFromNumber()
    {
        var sub = new Sub(
            new Number(20),
            TimeValue.Second(10).AsExpression()
        );
        var expected = TimeValue.Second(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubNumberFromTime()
    {
        var sub = new Sub(
            TimeValue.Second(20).AsExpression(),
            new Number(10)
        );
        var expected = TimeValue.Second(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubTimeFromTime()
    {
        var sub = new Sub(
            TimeValue.Minute(20).AsExpression(),
            TimeValue.Second(30).AsExpression()
        );
        var expected = TimeValue.Minute(19.5).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubAreaFromNumber()
    {
        var sub = new Sub(
            new Number(20),
            AreaValue.Meter(10).AsExpression()
        );
        var expected = AreaValue.Meter(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubNumberFromArea()
    {
        var sub = new Sub(
            AreaValue.Meter(20).AsExpression(),
            new Number(10)
        );
        var expected = AreaValue.Meter(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubAreaFromArea()
    {
        var sub = new Sub(
            AreaValue.Kilometer(10).AsExpression(),
            AreaValue.Meter(500000).AsExpression()
        );
        var expected = AreaValue.Kilometer(9.5).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubVolumeFromNumber()
    {
        var sub = new Sub(
            new Number(20),
            VolumeValue.Meter(10).AsExpression()
        );
        var expected = VolumeValue.Meter(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubNumberFromVolume()
    {
        var sub = new Sub(
            VolumeValue.Meter(20).AsExpression(),
            new Number(10)
        );
        var expected = VolumeValue.Meter(10).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubVolumeAndVolume()
    {
        var sub = new Sub(
            VolumeValue.Meter(10).AsExpression(),
            VolumeValue.Centimeter(500000).AsExpression()
        );
        var expected = VolumeValue.Meter(9.5).AsExpression();

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSecondUnaryMinus()
    {
        var sub = new Sub(Number.Two, new UnaryMinus(Variable.X));
        var expected = new Add(Number.Two, Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubDiff_NumAddVar_SubNum()
    {
        var sub = new Sub(new Add(Number.Two, Variable.X), Number.Two);
        var expected = Variable.X;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubDiff_VarAddNum_SubNum()
    {
        var sub = new Sub(new Add(Variable.X, Number.Two), Number.Two);
        var expected = Variable.X;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubDiffNumSub_NumAddVar_()
    {
        var sub = new Sub(Number.Two, new Add(Number.Two, Variable.X));
        var expected = new UnaryMinus(Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubDiffNumSub_VarAddNum_()
    {
        var sub = new Sub(Number.Two, new Add(Variable.X, Number.Two));
        var expected = new UnaryMinus(Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubDiff_NumSubVar_SubNum()
    {
        var sub = new Sub(new Sub(Number.Two, Variable.X), Number.Two);
        var expected = new UnaryMinus(Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubDiff_VarSubNum_SubNum()
    {
        var sub = new Sub(new Sub(Variable.X, Number.Two), Number.Two);
        var expected = new Sub(Variable.X, new Number(4));

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubDiffNumSub_NumSubVar_()
    {
        var sub = new Sub(Number.Two, new Sub(Number.Two, Variable.X));
        var expected = Variable.X;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubDiffNumSub_VarSubNum_()
    {
        var sub = new Sub(Number.Two, new Sub(Variable.X, Number.Two));
        var expected = new Sub(new Number(4), Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars1()
    {
        var sub = new Sub(Variable.X, Variable.X);
        var expected = Number.Zero;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars2()
    {
        var sub = new Sub(new Sub(Variable.X, Variable.X), Variable.X);
        var expected = new UnaryMinus(Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars3()
    {
        var sub = new Sub(new Mul(Number.Two, Variable.X), Variable.X);
        var expected = Variable.X;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars4()
    {
        var sub = new Sub(Variable.X, new Mul(Number.Two, Variable.X));
        var expected = new UnaryMinus(Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubComplexX()
    {
        var sub = new Sub(
            new Add(Variable.X, Variable.Y),
            new Mul(Number.Two, new Add(Variable.X, Variable.Y))
        );
        var expected = new UnaryMinus(new Add(Variable.X, Variable.Y));

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars5()
    {
        var sub = new Sub(Variable.X, new Mul(Variable.X, Number.Two));
        var expected = new UnaryMinus(Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubComplexX2()
    {
        var sub = new Sub(
            new Mul(Number.Two, new Add(Variable.X, Variable.Y)),
            new Add(Variable.X, Variable.Y)
        );
        var expected = new Add(Variable.X, Variable.Y);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars6()
    {
        var sub = new Sub(new Mul(Number.Two, Variable.X), Variable.X);
        var expected = Variable.X;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars7()
    {
        var sub = new Sub(new Mul(Variable.X, Number.Two), Variable.X);
        var expected = Variable.X;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars8()
    {
        var sub = new Sub(
            new Mul(new Number(3), Variable.X),
            new Mul(Number.Two, Variable.X)
        );
        var expected = Variable.X;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubComplexX3()
    {
        var sub = new Sub(
            new Mul(new Number(3), new Add(Variable.X, Variable.Y)),
            new Mul(Number.Two, new Add(Variable.X, Variable.Y))
        );
        var expected = new Add(Variable.X, Variable.Y);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars9()
    {
        var sub = new Sub(
            new Mul(Variable.X, new Number(3)),
            new Mul(Variable.X, Number.Two)
        );
        var expected = Variable.X;

        SimplifyTest(sub, expected);
    }

    [Test]
    public void SubSameVars10()
    {
        var sub = new Sub(
            new Mul(Variable.X, new Number(4)),
            new Mul(Variable.X, Number.Two)
        );
        var expected = new Mul(Number.Two, Variable.X);

        SimplifyTest(sub, expected);
    }

    [Test]
    public void AddSameVars11()
    {
        var exp = new Sub(
            new Mul(new Number(3), Variable.X),
            new Mul(Variable.X, Number.Two)
        );
        var expected = Variable.X;

        SimplifyTest(exp, expected);
    }

    [Test]
    public void AddSameVars12()
    {
        var exp = new Sub(
            new Mul(Variable.X, new Number(3)),
            new Mul(Number.Two, Variable.X)
        );
        var expected = Variable.X;

        SimplifyTest(exp, expected);
    }

    [Test]
    public void SubArgumentSimplified()
    {
        var exp = new Sub(
            Variable.X,
            new Ceil(new Add(Number.One, Number.One))
        );
        var expected = new Sub(Variable.X, new Ceil(Number.Two));

        SimplifyTest(exp, expected);
    }
}