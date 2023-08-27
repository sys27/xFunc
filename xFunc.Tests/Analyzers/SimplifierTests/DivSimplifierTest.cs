// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class DivSimplifierTest : BaseSimplifierTest
{
    [Test]
    public void DivZero()
    {
        var div = new Div(Number.Zero, Variable.X);
        var expected = Number.Zero;

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivByZero()
    {
        var div = new Div(Variable.X, Number.Zero);

        Assert.Throws<DivideByZeroException>(() => SimplifyTest(div, null));
    }

    [Test]
    public void ZeroDivByZero()
    {
        var div = new Div(Number.Zero, Number.Zero);
        var actual = (Number)div.Analyze(simplifier);

        Assert.True(actual.Value.IsNaN);
    }

    [Test]
    public void DivByOne()
    {
        var div = new Div(Variable.X, Number.One);
        var expected = Variable.X;

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivTwoNumbers()
    {
        var div = new Div(new Number(8), Number.Two);
        var expected = new Number(4);

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivAngleNumber()
    {
        var div = new Div(
            AngleValue.Degree(90).AsExpression(),
            new Number(2)
        );
        var expected = AngleValue.Degree(45).AsExpression();

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivPowerByNumber()
    {
        var div = new Div(
            PowerValue.Watt(90).AsExpression(),
            new Number(2)
        );
        var expected = PowerValue.Watt(45).AsExpression();

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivTemperatureByNumber()
    {
        var div = new Div(
            TemperatureValue.Celsius(90).AsExpression(),
            new Number(2)
        );
        var expected = TemperatureValue.Celsius(45).AsExpression();

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivMassByNumber()
    {
        var div = new Div(
            MassValue.Gram(90).AsExpression(),
            new Number(2)
        );
        var expected = MassValue.Gram(45).AsExpression();

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivLengthByNumber()
    {
        var div = new Div(
            LengthValue.Meter(90).AsExpression(),
            new Number(2)
        );
        var expected = LengthValue.Meter(45).AsExpression();

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivTimeByNumber()
    {
        var div = new Div(
            TimeValue.Second(90).AsExpression(),
            new Number(2)
        );
        var expected = TimeValue.Second(45).AsExpression();

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivAreaByNumber()
    {
        var div = new Div(
            AreaValue.Meter(90).AsExpression(),
            new Number(2)
        );
        var expected = AreaValue.Meter(45).AsExpression();

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivVolumeByNumber()
    {
        var div = new Div(
            VolumeValue.Meter(90).AsExpression(),
            new Number(2)
        );
        var expected = VolumeValue.Meter(45).AsExpression();

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivDiff_NumMulVar_DivNum()
    {
        var div = new Div(new Mul(Number.Two, Variable.X), new Number(4));
        var expected = new Div(Variable.X, Number.Two);

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivDiff_VarMulNum_DivNum()
    {
        var div = new Div(new Mul(Variable.X, Number.Two), new Number(4));
        var expected = new Div(Variable.X, Number.Two);

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivDiffNumDiv_NumMulVar_()
    {
        var div = new Div(Number.Two, new Mul(Number.Two, Variable.X));
        var expected = new Div(Number.One, Variable.X);

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivDiffNumDiv_VarMulNum_()
    {
        var div = new Div(Number.Two, new Mul(Variable.X, Number.Two));
        var expected = new Div(Number.One, Variable.X);

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivDiff_NumDivVar_DivNum()
    {
        var div = new Div(new Div(Number.Two, Variable.X), Number.Two);
        var expected = new Div(Number.One, Variable.X);

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivDiff_VarDivNum_DivNum()
    {
        var div = new Div(new Div(Variable.X, Number.Two), Number.Two);
        var expected = new Div(Variable.X, new Number(4));

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivDiffNumDiv_NumDivVar_()
    {
        var div = new Div(Number.Two, new Div(Number.Two, Variable.X));
        var expected = Variable.X;

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivDiffNumDiv_VarDivNum_()
    {
        var div = new Div(Number.Two, new Div(Variable.X, Number.Two));
        var expected = new Div(new Number(4), Variable.X);

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivSameVars()
    {
        var div = new Div(Variable.X, Variable.X);
        var expected = Number.One;

        SimplifyTest(div, expected);
    }

    [Test]
    public void DivArgumentSimplified()
    {
        var exp = new Div(
            Variable.X,
            new Ceil(new Add(Number.One, Number.One))
        );
        var expected = new Div(Variable.X, new Ceil(Number.Two));

        SimplifyTest(exp, expected);
    }
}