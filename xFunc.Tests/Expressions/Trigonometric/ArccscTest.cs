// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArccscTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arccsc(Number.One);
        var result = exp.Execute();
        var expected = AngleValue.Radian(1.5707963267948966);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arccsc(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(0.22996290237720782).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(-0.15735549884498545).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arccsc(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arccsc(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}