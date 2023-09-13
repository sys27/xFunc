// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class SineTest : BaseExpressionTests
{
    [Test]
    [TestCase(0.0, 0.0)] // 0
    [TestCase(30.0, 0.5)] // 1 / 2
    [TestCase(45.0, 0.70710678118654757)] // sqrt(2) / 2
    [TestCase(60.0, 0.86602540378443864)] // sqrt(3) / 2
    [TestCase(90.0, 1.0)] // 1
    [TestCase(120.0, 0.86602540378443864)] // sqrt(3) / 2
    [TestCase(135.0, 0.70710678118654757)] // sqrt(2) / 2
    [TestCase(150.0, 0.5)] // 1 / 2
    [TestCase(180.0, 0.0)] // 0
    [TestCase(210.0, -0.5)] // -1 / 2
    [TestCase(225.0, -0.70710678118654757)] // -sqrt(2) / 2
    [TestCase(240.0, -0.86602540378443864)] // -sqrt(3) / 2
    [TestCase(270.0, -1.0)] // -1
    [TestCase(300.0, -0.86602540378443864)] // -sqrt(3) / 2
    [TestCase(315.0, -0.70710678118654757)] // -sqrt(2) / 2
    [TestCase(330.0, -0.5)] // -1 / 2
    [TestCase(360.0, 0)] // 0
    [TestCase(1110.0, 0.5)] // 1 / 2
    [TestCase(1770.0, -0.5)] // -1 / 2
    [TestCase(-390.0, -0.5)] // -1 / 2
    public void ExecuteNumberTest(double degree, double expected)
    {
        var exp = new Sin(new Number(degree));
        var result = (NumberValue)exp.Execute();

        Assert.That(result.Number, Is.EqualTo(expected).Within(15));
    }

    [Test]
    public void ExecuteRadianTest()
    {
        var exp = new Sin(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.8414709848078965);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteDegreeTest()
    {
        var exp = new Sin(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.017452406437283512);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteGradianTest()
    {
        var exp = new Sin(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.015707317311820675);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Sin(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(0.53092108624851986).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(-3.59056458998578).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Sin(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Sin(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}