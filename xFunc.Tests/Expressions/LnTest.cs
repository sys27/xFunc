// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class LnTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Ln(Number.Two);
        var expected = new NumberValue(Math.Log(2));

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexTest()
    {
        var complex = new Complex(2, 3);
        var exp = new Ln(new ComplexNumber(complex));

        Assert.That(exp.Execute(), Is.EqualTo(Complex.Log(complex)));
    }

    [Test]
    public void ExecuteRationalTest()
    {
        var exp = new Ln(new Rational(new Number(2), new Number(3)));
        var expected = new NumberValue(-0.4054651081081645);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Ln(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Ln(new Number(5));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}