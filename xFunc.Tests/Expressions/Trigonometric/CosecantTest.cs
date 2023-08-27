// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class CosecantTest : BaseExpressionTests
{
    [Test]
    [TestCase(0.0, double.PositiveInfinity)] // -
    [TestCase(30.0, 2.0)] // 2
    [TestCase(45.0, 1.4142135623730951)] // sqrt(2)
    [TestCase(60.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [TestCase(90.0, 1.0)] // 1
    [TestCase(120.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [TestCase(135.0, 1.4142135623730951)] // sqrt(2)
    [TestCase(150.0, 2.0)] // 2
    [TestCase(180.0, double.PositiveInfinity)] // -
    [TestCase(210.0, -2.0)] // -2
    [TestCase(225.0, -1.4142135623730951)] // -sqrt(2)
    [TestCase(240.0, -1.1547005383792515)] // -2sqrt(3) / 3
    [TestCase(270.0, -1.0)] // -1
    [TestCase(300.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [TestCase(315.0, 1.4142135623730951)] // sqrt(2)
    [TestCase(330.0, 2.0)] // 2
    [TestCase(360.0, double.PositiveInfinity)] // -
    [TestCase(1110.0, 2.0)] // 2
    [TestCase(1770.0, 2.0)] // 2
    [TestCase(-390.0, 2.0)] // 2
    public void ExecuteNumberTest(double degree, double expected)
    {
        var exp = new Csc(new Number(degree));
        var result = (NumberValue)exp.Execute();

        Assert.That(result.Number, Is.EqualTo(expected).Within(15));
    }

    [Test]
    public void ExecuteDegreeTest()
    {
        var exp = new Csc(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(57.298688498550185);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRadianTest()
    {
        var exp = new Csc(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.1883951057781212);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteGradianTest()
    {
        var exp = new Csc(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(63.664595306000564);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Csc(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(0.040300578856891527).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(0.27254866146294021).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Csc(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Csc(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}