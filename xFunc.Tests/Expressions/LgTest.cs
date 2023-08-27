// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class LgTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Lg(Number.Two);
        var expected = new NumberValue(Math.Log10(2));

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexTest()
    {
        var complex = new Complex(2, 3);
        var exp = new Lg(new ComplexNumber(complex));
        var expected = Complex.Log10(complex);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRationalTest()
    {
        var exp = new Lg(new Rational(new Number(2), new Number(3)));
        var expected = new NumberValue(-0.17609125905568124);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Lg(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Lg(Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}