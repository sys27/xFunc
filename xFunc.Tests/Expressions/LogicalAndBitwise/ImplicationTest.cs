// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class ImplicationTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var impl = new Implication(Bool.True, Bool.False);

        Assert.That((bool) impl.Execute(), Is.False);
    }

    [Test]
    public void ExecuteTest2()
    {
        var impl = new Implication(Bool.True, Bool.True);

        Assert.That((bool) impl.Execute(), Is.True);
    }

    [Test]
    public void ExecuteResultIsNotSupported()
        => TestNotSupported(new Implication(Number.One, Number.Two));

    [Test]
    public void CloneTest()
    {
        var exp = new Implication(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}