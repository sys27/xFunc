// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArtangentTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Artanh(new Number(0.6));
        var result = exp.Execute();
        var expected = AngleValue.Radian(0.6931471805599453);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Artanh(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(0.2290726829685388).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(1.4099210495965755).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Artanh(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Artanh(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}