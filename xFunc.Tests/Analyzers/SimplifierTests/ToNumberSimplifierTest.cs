// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class ToNumberSimplifierTest : BaseSimplifierTest
{
    [Fact]
    public void AngleToNumberTest()
    {
        var exp = new ToNumber(AngleValue.Degree(10).AsExpression());
        var expected = new Number(10);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void PowerToNumberTest()
    {
        var exp = new ToNumber(PowerValue.Watt(10).AsExpression());
        var expected = new Number(10);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void TemperatureToNumberTest()
    {
        var exp = new ToNumber(TemperatureValue.Celsius(10).AsExpression());
        var expected = new Number(10);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void MassToNumberTest()
    {
        var exp = new ToNumber(MassValue.Gram(10).AsExpression());
        var expected = new Number(10);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void LengthToNumberTest()
    {
        var exp = new ToNumber(LengthValue.Meter(10).AsExpression());
        var expected = new Number(10);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void TimeToNumberTest()
    {
        var exp = new ToNumber(TimeValue.Second(10).AsExpression());
        var expected = new Number(10);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void AreaToNumberTest()
    {
        var exp = new ToNumber(AreaValue.Meter(10).AsExpression());
        var expected = new Number(10);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void VolumeToNumberTest()
    {
        var exp = new ToNumber(VolumeValue.Meter(10).AsExpression());
        var expected = new Number(10);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ToNumberArgumentSimplifiedTest()
    {
        var exp = new ToNumber(new Add(Number.One, Number.One));
        var expected = new ToNumber(Number.Two);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ToNumberNotSimplifiedTest()
    {
        var exp = new ToNumber(Variable.X);

        SimplifyTest(exp, exp);
    }
}