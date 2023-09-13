// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.ComplexNumbers;

public class PhaseTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Phase(new ComplexNumber(complex));
        var expected = AngleValue.Radian(complex.Phase);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteExceptionTest()
        => TestNotSupported(new Phase(Number.Two));

    [Test]
    public void CloneTest()
    {
        var exp = new Phase(new ComplexNumber(new Complex(2, 2)));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}