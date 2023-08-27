// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArsineTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arsinh(new Number(0.5));
        var result = (AngleValue)exp.Execute();
        var expected = AngleValue.Radian(0.48121182505960347);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arsinh(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(1.983387029916535432347076).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(0.5706527843210994007).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arsinh(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arsinh(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}