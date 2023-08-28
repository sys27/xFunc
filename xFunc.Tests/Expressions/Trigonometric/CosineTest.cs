// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class CosineTest : BaseExpressionTests
{
    [Test]
    [TestCase(0.0, 1.0)] // 1
    [TestCase(30.0, 0.86602540378443864)] // sqrt(3) / 2
    [TestCase(45.0, 0.70710678118654757)] // sqrt(2) / 2
    [TestCase(60.0, 0.5)] // 1 / 2
    [TestCase(90.0, 0.0)] // 0
    [TestCase(120.0, -0.5)] // -1 / 2
    [TestCase(135.0, -0.70710678118654757)] // -sqrt(2) / 2
    [TestCase(150.0, -0.86602540378443864)] // -sqrt(3) / 2
    [TestCase(180.0, -1.0)] // -1
    [TestCase(210.0, -0.86602540378443864)] // -sqrt(3) / 2
    [TestCase(225.0, -0.70710678118654757)] // -sqrt(2) / 2
    [TestCase(240.0, -0.5)] // -1 / 2
    [TestCase(270.0, 0.0)] // 0
    [TestCase(300.0, 0.5)] // 1 / 2
    [TestCase(315.0, 0.70710678118654757)] // sqrt(2) / 2
    [TestCase(330.0, 0.86602540378443864)] // sqrt(3) / 2
    [TestCase(360.0, 1.0)] // 1
    [TestCase(1110.0, 0.86602540378443864)] // sqrt(3) / 2
    [TestCase(1770.0, 0.86602540378443864)] // sqrt(3) / 2
    [TestCase(-390.0, 0.86602540378443864)] // sqrt(3) / 2
    public void ExecuteNumberTest(double degree, double expected)
    {
        var exp = new Cos(new Number(degree));
        var result = (NumberValue)exp.Execute();

        Assert.That(result.Number, Is.EqualTo(expected).Within(15));
    }

    [Test]
    public void ExecuteRadianTest()
    {
        var exp = new Cos(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.5403023058681398);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteDegreeTest()
    {
        var exp = new Cos(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.9998476951563913);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteGradianTest()
    {
        var exp = new Cos(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.9998766324816606);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Cos(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(-3.7245455049153224).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(-0.51182256998738462).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Cos(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Cos(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}