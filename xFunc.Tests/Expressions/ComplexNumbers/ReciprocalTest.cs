// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.ComplexNumbers;

public class ReciprocalTest : BaseExpressionTests
{
    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Reciprocal(new ComplexNumber(complex));

        Assert.That(exp.Execute(), Is.EqualTo(Complex.Reciprocal(complex)));
    }

    [Test]
    public void ExecuteExceptionTest()
        => TestNotSupported(new Reciprocal(Number.Two));

    [Test]
    public void CloneTest()
    {
        var exp = new Reciprocal(new ComplexNumber(new Complex(2, 2)));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}