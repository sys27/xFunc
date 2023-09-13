// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class SecantTest : BaseExpressionTests
{
    [Test]
    [TestCase(0.0, 1.0)] // 1
    [TestCase(30.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [TestCase(45.0, 1.4142135623730951)] // sqrt(2)
    [TestCase(60.0, 2.0)] // 2
    [TestCase(90.0, double.PositiveInfinity)] // -
    [TestCase(120.0, -2.0)] // -2
    [TestCase(135.0, -1.4142135623730951)] // -sqrt(2)
    [TestCase(150.0, -1.1547005383792515)] // -2sqrt(3) / 3
    [TestCase(180.0, -1.0)] // -1
    [TestCase(210.0, -1.1547005383792515)] // -2sqrt(3) / 3
    [TestCase(225.0, -1.4142135623730951)] // -sqrt(2)
    [TestCase(240.0, -2.0)] // -2
    [TestCase(270.0, double.PositiveInfinity)] // -
    [TestCase(300.0, -2.0)] // -2
    [TestCase(315.0, 1.4142135623730951)] // sqrt(2)
    [TestCase(330.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [TestCase(360.0, 1.0)] // 1
    [TestCase(1110.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [TestCase(1770.0, 1.1547005383792515)] // 2sqrt(3) / 3
    [TestCase(-390.0, 1.1547005383792515)] // 2sqrt(3) / 3
    public void ExecuteNumberTest(double degree, double expected)
    {
        var exp = new Sec(new Number(degree));
        var result = (NumberValue)exp.Execute();

        Assert.That(result.Number, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteDegreeTest()
    {
        var exp = new Sec(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.0001523280439077);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRadianTest()
    {
        var exp = new Sec(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.8508157176809255);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteGradianTest()
    {
        var exp = new Sec(AngleValue.Gradian(1).AsExpression());
        var actual = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.0001233827397618);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Sec(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(-0.26351297515838928).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(0.036211636558768523).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Sec(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Sec(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}