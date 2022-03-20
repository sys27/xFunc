// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class AddSimplifierTest : BaseSimplifierTest
{
    [Fact(DisplayName = "2 + x")]
    public void Order1()
    {
        var add = new Add(Number.Two, Variable.X);
        var expected = new Add(Variable.X, Number.Two);

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "0 + 1")]
    public void AddFirstZero()
    {
        var add = new Add(Number.Zero, Number.One);
        var expected = Number.One;

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "1 + 0")]
    public void AddSecondZero()
    {
        var add = new Add(Number.One, Number.Zero);
        var expected = Number.One;

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "3 + 2")]
    public void AddTwoNumbers()
    {
        var add = new Add(new Number(3), Number.Two);
        var expected = new Number(5);

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "90 + 2 rad")]
    public void AddNumberAngle()
    {
        var add = new Add(
            new Number(90),
            AngleValue.Degree(2).AsExpression()
        );
        var expected = AngleValue.Degree(92).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "90 deg + 2")]
    public void AddAngleNumber()
    {
        var add = new Add(
            AngleValue.Degree(90).AsExpression(),
            new Number(2)
        );
        var expected = AngleValue.Degree(92).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "90 deg + 2 rad")]
    public void AddTwoAngles()
    {
        var add = new Add(
            AngleValue.Degree(90).AsExpression(),
            AngleValue.Radian(2).AsExpression()
        );
        var expected = AngleValue.Degree(90 + 114.59155902616465).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 + 10 W")]
    public void AddNumberAndPower()
    {
        var add = new Add(
            new Number(10),
            PowerValue.Watt(10).AsExpression()
        );
        var expected = PowerValue.Watt(20).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 W + 10")]
    public void AddPowerAndNumber()
    {
        var add = new Add(
            PowerValue.Watt(10).AsExpression(),
            new Number(10)
        );
        var expected = PowerValue.Watt(20).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 W + 10 kW")]
    public void AddTwoPowers()
    {
        var add = new Add(
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Kilowatt(10).AsExpression()
        );
        var expected = PowerValue.Watt(10010).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 + 10 C°")]
    public void AddNumberAndTemperature()
    {
        var add = new Add(
            new Number(10),
            TemperatureValue.Celsius(10).AsExpression()
        );
        var expected = TemperatureValue.Celsius(20).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 C° + 10")]
    public void AddTemperatureAndNumber()
    {
        var add = new Add(
            TemperatureValue.Celsius(10).AsExpression(),
            new Number(10)
        );
        var expected = TemperatureValue.Celsius(20).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 C° + 10 K")]
    public void AddTwoTemperatures()
    {
        var add = new Add(
            TemperatureValue.Celsius(10).AsExpression(),
            TemperatureValue.Kelvin(10).AsExpression()
        );
        var expected = TemperatureValue.Celsius(10 - 263.15).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 + 10 g")]
    public void AddNumberAndMass()
    {
        var add = new Add(
            new Number(10),
            MassValue.Gram(10).AsExpression()
        );
        var expected = MassValue.Gram(20).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 g + 10")]
    public void AddMassAndNumber()
    {
        var add = new Add(
            MassValue.Gram(10).AsExpression(),
            new Number(10)
        );
        var expected = MassValue.Gram(20).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 g + 10 kg")]
    public void AddTwoMasses()
    {
        var add = new Add(
            MassValue.Kilogram(10).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );
        var expected = MassValue.Gram(10010).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 + 10 m")]
    public void AddNumberAndLength()
    {
        var add = new Add(
            new Number(10),
            LengthValue.Meter(10).AsExpression()
        );
        var expected = LengthValue.Meter(20).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 m + 10")]
    public void AddLengthAndNumber()
    {
        var add = new Add(
            LengthValue.Meter(10).AsExpression(),
            new Number(10)
        );
        var expected = LengthValue.Meter(20).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "10 km + 10 m")]
    public void AddTwoLengths()
    {
        var add = new Add(
            LengthValue.Kilometer(10).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );
        var expected = LengthValue.Kilometer(10.01).AsExpression();

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "-x + 2")]
    public void AddFirstUnaryMinus()
    {
        var add = new Add(new UnaryMinus(Variable.X), Number.Two);
        var expected = new Sub(Number.Two, Variable.X);

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "2 + (-x)")]
    public void AddSecondUnaryMinus()
    {
        var add = new Add(Number.Two, new UnaryMinus(Variable.X));
        var expected = new Sub(Number.Two, Variable.X);

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "2 + (2 + x)")]
    public void AddDiffNumAdd_NumAddVar_()
    {
        var add = new Add(Number.Two, new Add(Number.Two, Variable.X));
        var expected = new Add(Variable.X, new Number(4));

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "2 + (x + 2)")]
    public void AddDiffNumAdd_VarAddNum_()
    {
        var add = new Add(Number.Two, new Add(Variable.X, Number.Two));
        var expected = new Add(Variable.X, new Number(4));

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "(2 + x) + 2")]
    public void AddDiff_NumAddVar_AddNum()
    {
        var add = new Add(new Add(Number.Two, Variable.X), Number.Two);
        var expected = new Add(Variable.X, new Number(4));

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "(x + 2) + 2")]
    public void AddDiff_VarAddNum_AddNum()
    {
        var add = new Add(new Add(Variable.X, Number.Two), Number.Two);
        var expected = new Add(Variable.X, new Number(4));

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "2 + (2 - x)")]
    public void AddDiffNum_NumSubVar_()
    {
        var add = new Add(Number.Two, new Sub(Number.Two, Variable.X));
        var expected = new Sub(new Number(4), Variable.X);

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "2 + (x - 2)")]
    public void AddDiffNum_VarSubNum_()
    {
        var add = new Add(Number.Two, new Sub(Variable.X, Number.Two));
        var expected = Variable.X;

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "(2 - x) + 2")]
    public void AddDiff_NumSubVar_AddNum()
    {
        var add = new Add(new Sub(Number.Two, Variable.X), Number.Two);
        var expected = new Sub(new Number(4), Variable.X);

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "(x - 2) + 2")]
    public void AddDiff_VarSubNum_AddNum()
    {
        var add = new Add(new Sub(Variable.X, Number.Two), Number.Two);
        var expected = Variable.X;

        SimplifyTest(add, expected);
    }

    [Fact(DisplayName = "x + x")]
    public void AddSaveVars1()
    {
        var exp = new Add(Variable.X, Variable.X);
        var expected = new Mul(Number.Two, Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "2x + x")]
    public void AddSaveVars2()
    {
        var exp = new Add(new Mul(Number.Two, Variable.X), Variable.X);
        var expected = new Mul(new Number(3), Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "2 * (x + y) + (x + y)")]
    public void AddComplexX()
    {
        var exp = new Add(
            new Mul(Number.Two, new Add(Variable.X, new Variable("Y"))),
            new Add(Variable.X, new Variable("Y")));
        var expected = new Mul(new Number(3), new Add(Variable.X, new Variable("Y")));

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "x + 2x")]
    public void AddSaveVars3()
    {
        var exp = new Add(Variable.X, new Mul(Number.Two, Variable.X));
        var expected = new Mul(new Number(3), Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "x + x * 2")]
    public void AddSaveVars4()
    {
        var exp = new Add(Variable.X, new Mul(Variable.X, Number.Two));
        var expected = new Mul(new Number(3), Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "2x + 3x")]
    public void AddSaveVars5()
    {
        var exp = new Add(
            new Mul(Number.Two, Variable.X),
            new Mul(new Number(3), Variable.X)
        );
        var expected = new Mul(new Number(5), Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "2 * (x + y) + 3 * (x + y)")]
    public void AddComplexX2()
    {
        var exp = new Add(
            new Mul(Number.Two, new Add(Variable.X, Variable.Y)),
            new Mul(new Number(3), new Add(Variable.X, Variable.Y))
        );
        var expected = new Mul(
            new Number(5),
            new Add(Variable.X, Variable.Y)
        );

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "-x + x")]
    public void AddSaveVars6()
    {
        var exp = new Add(new UnaryMinus(Variable.X), Variable.X);
        var expected = Number.Zero;

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "-x + 2x")]
    public void AddSaveVars7()
    {
        var exp = new Add(
            new UnaryMinus(Variable.X),
            new Mul(Number.Two, Variable.X)
        );
        var expected = Variable.X;

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "x * 2 + x")]
    public void AddSaveVars8()
    {
        var exp = new Add(new Mul(Variable.X, Number.Two), Variable.X);
        var expected = new Mul(new Number(3), Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "x * 2 + x * 3")]
    public void AddSaveVars9()
    {
        var exp = new Add(
            new Mul(Variable.X, Number.Two),
            new Mul(Variable.X, new Number(3))
        );
        var expected = new Mul(new Number(5), Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "3x + -2x")]
    public void AddSaveVars10()
    {
        var exp = new Add(
            new Mul(new Number(3), Variable.X),
            new Mul(new Number(-2), Variable.X)
        );
        var expected = Variable.X;

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "3x + -4x")]
    public void AddSaveVars11()
    {
        var exp = new Add(
            new Mul(new Number(3), Variable.X),
            new Mul(new Number(-4), Variable.X)
        );
        var expected = new UnaryMinus(Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "-2x + x * 3")]
    public void AddSameVars12()
    {
        var exp = new Add(
            new Mul(new Number(-2), Variable.X),
            new Mul(Variable.X, new Number(3))
        );
        var expected = Variable.X;

        SimplifyTest(exp, expected);
    }

    [Fact(DisplayName = "x * 3 + -2x")]
    public void AddSameVars13()
    {
        var exp = new Add(
            new Mul(Variable.X, new Number(3)),
            new Mul(new Number(-2), Variable.X)
        );
        var expected = Variable.X;

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void AddArgumentSimplified()
    {
        var exp = new Add(
            Variable.X,
            new Ceil(new Add(Number.One, Number.One))
        );
        var expected = new Add(Variable.X, new Ceil(Number.Two));

        SimplifyTest(exp, expected);
    }
}