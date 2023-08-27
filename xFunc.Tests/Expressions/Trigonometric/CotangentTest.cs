// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class CotangentTest : BaseExpressionTests
{
    [Test]
    [TestCase(0.0, double.PositiveInfinity)] // -
    [TestCase(30.0, 1.7320508075688772)] // sqrt(3)
    [TestCase(45.0, 1.0)] // 1
    [TestCase(60.0, 0.57735026918962573)] // sqrt(3) / 3
    [TestCase(90.0, 0.0)] // 0
    [TestCase(120.0, -0.57735026918962573)] // -sqrt(3) / 3
    [TestCase(135.0, -1)] // -1
    [TestCase(150.0, -1.7320508075688772)] // -sqrt(3)
    [TestCase(180.0, double.PositiveInfinity)] // -
    [TestCase(210.0, 1.7320508075688772)] // sqrt(3)
    [TestCase(225.0, 1.0)] // 1
    [TestCase(240.0, 0.57735026918962573)] // sqrt(3) / 3
    [TestCase(270.0, 0.0)] // 0
    [TestCase(300.0, -1.7320508075688772)] // -sqrt(3)
    [TestCase(315.0, -1.0)] // -1
    [TestCase(330.0, -0.57735026918962573)] // -sqrt(3) / 3
    [TestCase(360.0, double.PositiveInfinity)] // -
    [TestCase(1110.0, 1.7320508075688772)] // sqrt(3)
    [TestCase(1770.0, -0.57735026918962573)] // -sqrt(3) / 3
    [TestCase(-390.0, -0.57735026918962573)] // -sqrt(3) / 3
    public void ExecuteNumberTest(double degree, double expected)
    {
        var exp = new Cot(new Number(degree));
        var actual = (NumberValue)exp.Execute();

        Assert.That(actual.Number, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRadianTest()
    {
        var exp = new Cot(AngleValue.Radian(1).AsExpression());
        var actual = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.6420926159343308);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteDegreeTest()
    {
        var exp = new Cot(AngleValue.Degree(1).AsExpression());
        var actual = (NumberValue)exp.Execute();
        var expected = new NumberValue(57.28996163075943);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteGradianTest()
    {
        var exp = new Cot(AngleValue.Gradian(1).AsExpression());
        var actual = (NumberValue)exp.Execute();
        var expected = new NumberValue(63.65674116287158);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Cot(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(-0.010604783470337083).Within(14));
        Assert.That(result.Imaginary, Is.EqualTo(-1.0357466377649953).Within(14));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Cot(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Cot(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}