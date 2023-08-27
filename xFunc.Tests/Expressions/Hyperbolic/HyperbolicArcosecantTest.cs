// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArcosecantTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arcsch(new Number(0.5));
        var result = exp.Execute();
        var expected = AngleValue.Radian(1.2279471772995156);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arcsch(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(0.23133469857397318).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(-0.15038560432786197).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arcsch(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arcsch(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}