// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class TangentTest : BaseExpressionTests
{
    [Test]
    [TestCase(0.0, 0.0)] // 0
    [TestCase(30.0, 0.57735026918962573)] // sqrt(3) / 3
    [TestCase(45.0, 1.0)] // 1
    [TestCase(60.0, 1.7320508075688772)] // sqrt(3)
    [TestCase(90.0, double.PositiveInfinity)] // -
    [TestCase(120.0, -1.7320508075688772)] // -sqrt(3)
    [TestCase(135.0, -1)] // -1
    [TestCase(150.0, -0.57735026918962573)] // -sqrt(3) / 3
    [TestCase(180.0, 0.0)] // 0
    [TestCase(210.0, 0.57735026918962573)] // sqrt(3) / 3
    [TestCase(225.0, 1.0)] // 1
    [TestCase(240.0, 1.7320508075688772)] // sqrt(3)
    [TestCase(270.0, double.PositiveInfinity)] // -
    [TestCase(300.0, -1.7320508075688772)] // -sqrt(3)
    [TestCase(315.0, -1.0)] // -1
    [TestCase(330.0, -0.57735026918962573)] // -sqrt(3) / 3
    [TestCase(360.0, 0.0)] // 0
    [TestCase(1110.0, 0.57735026918962573)] // sqrt(3) / 3
    [TestCase(1770.0, -0.57735026918962573)] // -sqrt(3) / 3
    [TestCase(-390.0, -0.57735026918962573)] // -sqrt(3) / 3
    public void ExecuteNumberTest(double degree, double expected)
    {
        var exp = new Tan(new Number(degree));
        var result = (NumberValue)exp.Execute();

        Assert.That(result.Number, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRadianTest()
    {
        var exp = new Tan(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.5574077246549021);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteDegreeTest()
    {
        var exp = new Tan(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.017455064928217585);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteGradianTest()
    {
        var exp = new Tan(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.015709255323664916);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Tan(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(-0.0098843750383224935).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(0.96538587902213313).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Tan(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Tan(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}