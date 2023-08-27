// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArcsecTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arcsec(Number.One);
        var result = exp.Execute();
        var expected = AngleValue.Radian(0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arcsec(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(1.3408334244176887).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(0.15735549884498545).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arcsec(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arcsec(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}