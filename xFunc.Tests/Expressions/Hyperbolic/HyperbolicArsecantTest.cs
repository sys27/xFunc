// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArsecantTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arsech(new Number(0.5));
        var result = exp.Execute();
        var expected = AngleValue.Radian(1.3169578969248166);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arsech(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(0.15735549884498526).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(-1.3408334244176887).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arsech(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arsech(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}