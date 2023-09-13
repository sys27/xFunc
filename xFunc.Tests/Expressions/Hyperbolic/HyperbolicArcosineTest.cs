// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArcosineTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arcosh(new Number(7));
        var result = exp.Execute();
        var expected = AngleValue.Radian(2.6339157938496336);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arcosh(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(1.9686379257930964).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(0.606137822387294).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arcosh(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arcosh(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}