// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.ComplexNumbers;

public class ToComplexTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest()
    {
        var exp = new ToComplex(Number.Two);
        var result = (Complex)exp.Execute();
        var expected = new Complex(2, 0);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteBoolTest()
        => TestNotSupported(new ToComplex(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new ToComplex(Number.Two);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}