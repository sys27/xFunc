// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArccscTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Arccsc(Number.One);
        var result = exp.Execute();
        var expected = AngleValue.Radian(1.5707963267948966);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arccsc(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(0.22996290237720782, result.Real, 15);
        Assert.Equal(-0.15735549884498545, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Arccsc(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Arccsc(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}