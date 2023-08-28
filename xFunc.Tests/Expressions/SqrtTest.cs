// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class SqrtTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var exp = new Sqrt(new Number(4));
        var expected = new NumberValue(Math.Sqrt(4));

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void NegativeNumberExecuteTest1()
    {
        var exp = new Sqrt(new Number(-25));

        Assert.That(exp.Execute(), Is.EqualTo(new Complex(0, 5)));
    }

    [Test]
    public void NegativeNumberExecuteTest2()
    {
        var exp = new Sqrt(new Number(-1));

        Assert.That(exp.Execute(), Is.EqualTo(new Complex(0, 1)));
    }

    [Test]
    public void ComplexExecuteTest()
    {
        var complex = new Complex(5, 3);
        var exp = new Sqrt(new ComplexNumber(complex));

        Assert.That(exp.Execute(), Is.EqualTo(Complex.Sqrt(complex)));
    }

    [Test]
    public void NegativeComplexNumberExecuteTest()
    {
        var complex = new Complex(-25, 13);
        var exp = new Sqrt(new ComplexNumber(complex));

        Assert.That(exp.Execute(), Is.EqualTo(Complex.Sqrt(complex)));
    }

    [Test]
    public void ExecuteBoolTest()
        => TestNotSupported(new Sqrt(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Sqrt(Number.Two);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}