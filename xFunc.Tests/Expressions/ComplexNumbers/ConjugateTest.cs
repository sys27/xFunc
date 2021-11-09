// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.ComplexNumbers;

public class ConjugateTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Conjugate(new ComplexNumber(complex));

        Assert.Equal(Complex.Conjugate(complex), exp.Execute());
    }

    [Fact]
    public void ExecuteExceptionTest()
        => TestNotSupported(new Conjugate(Number.Two));

    [Fact]
    public void CloneTest()
    {
        var exp = new Conjugate(new ComplexNumber(new Complex(2, 2)));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}