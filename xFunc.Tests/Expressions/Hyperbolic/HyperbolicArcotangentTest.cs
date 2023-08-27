// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArcotangentTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arcoth(new Number(7));
        var result = exp.Execute();
        var expected = AngleValue.Radian(0.14384103622589042);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arcoth(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(0.2290726829685388).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(-0.16087527719832109).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arcoth(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arcoth(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}