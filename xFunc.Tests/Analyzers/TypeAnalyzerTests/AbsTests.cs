// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class AbsTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestAbsNumber()
    {
        var exp = new Abs(new Number(-2));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestAbsAngleNumber()
    {
        var exp = new Abs(AngleValue.Degree(1).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestAbsPowerNumber()
    {
        var exp = new Abs(PowerValue.Watt(1).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestAbsTemperatureNumber()
    {
        var exp = new Abs(TemperatureValue.Celsius(1).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestAbsMassNumber()
    {
        var exp = new Abs(MassValue.Gram(1).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestAbsLengthNumber()
    {
        var exp = new Abs(LengthValue.Meter(1).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestAbsTimeNumber()
    {
        var exp = new Abs(TimeValue.Second(1).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestAbsAreaNumber()
    {
        var exp = new Abs(AreaValue.Meter(1).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestAbsVolumeNumber()
    {
        var exp = new Abs(VolumeValue.Meter(1).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestAbsComplexNumber()
    {
        var exp = new Abs(new ComplexNumber(2, 2));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestAbsVariable()
    {
        var exp = new Abs(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestAbsVector()
    {
        var exp = new Abs(new Vector(new IExpression[] { Number.One }));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestAbsRational()
    {
        var exp = new Abs(new Rational(new Number(1), new Number(2)));

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestAbsException()
    {
        var exp = new Abs(Bool.False);

        TestException(exp);
    }
}