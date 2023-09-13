// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class RootTest : BaseExpressionTests
{
    [Test]
    public void CalculateRootTest1()
    {
        var exp = new Root(new Number(8), new Number(3));
        var expected = new NumberValue(Math.Pow(8, 1.0 / 3.0));

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void CalculateRootTest2()
    {
        var exp = new Root(new Number(-8), new Number(3));

        Assert.That(exp.Execute(), Is.EqualTo(new NumberValue(-2.0)));
    }

    [Test]
    public void NegativeNumberExecuteTest()
    {
        var exp = new Root(new Number(-25), Number.Two);
        var result = (Complex)exp.Execute();
        var expected = new Complex(0, 5);

        Assert.That(result.Real, Is.EqualTo(expected.Real).Within(14));
        Assert.That(result.Imaginary, Is.EqualTo(expected.Imaginary).Within(14));
    }

    [Test]
    public void NegativeNumberExecuteTest2()
    {
        var exp = new Root(new Number(-25), new Number(-2));

        Assert.That(exp.Execute(), Is.EqualTo(new Complex(0, -0.2)));
    }

    [Test]
    public void ExecuteExceptionTest()
        => TestNotSupported(new Root(Bool.False, Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Root(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}