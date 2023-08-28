// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class ExpTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var exp = new Exp(Number.Two);
        var expected = NumberValue.Exp(new NumberValue(2));

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest2()
    {
        var expected = new Complex(4, 2);
        var exp = new Exp(new ComplexNumber(expected));

        Assert.That(exp.Execute(), Is.EqualTo(Complex.Exp(expected)));
    }

    [Test]
    public void ExecuteExceptionTest()
        => TestNotSupported(new Exp(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Exp(Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}