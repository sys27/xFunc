// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class NAndTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var nand = new NAnd(Bool.True, Bool.True);

        Assert.That((bool) nand.Execute(), Is.False);
    }

    [Test]
    public void ExecuteTest2()
    {
        var nand = new NAnd(Bool.False, Bool.True);

        Assert.That((bool) nand.Execute(), Is.True);
    }

    [Test]
    public void ExecuteResultIsNotSupported()
        => TestNotSupported(new NAnd(Number.One, Number.Two));

    [Test]
    public void CloneTest()
    {
        var exp = new NAnd(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}