// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArccotTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Arccot(Number.One);
        var result = exp.Execute();
        var expected = AngleValue.Radian(0.7853981633974483);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arccot(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(0.23182380450040308, result.Real, 15);
        Assert.Equal(-0.14694666622552988, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Arccot(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Arccot(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}