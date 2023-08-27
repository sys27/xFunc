// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArccosTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arccos(Number.One);
        var expected = AngleValue.Radian(0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arccos(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(0.60613782238729386).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(-1.9686379257930964).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arccos(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arccos(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}