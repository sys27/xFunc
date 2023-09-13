// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArcsinTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arcsin(Number.One);
        var result = exp.Execute();
        var expected = AngleValue.Radian(1.5707963267948966);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arcsin(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(0.96465850440760248).Within(14));
        Assert.That(result.Imaginary, Is.EqualTo(1.9686379257930975).Within(14));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arcsin(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arcsin(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}