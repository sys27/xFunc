// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class EqualityTest
{
    [Test]
    public void ExecuteTest1()
    {
        var eq = new Equality(Bool.True, Bool.True);

        Assert.That((bool) eq.Execute(), Is.True);
    }

    [Test]
    public void ExecuteTest2()
    {
        var eq = new Equality(Bool.True, Bool.False);

        Assert.That((bool) eq.Execute(), Is.False);
    }

    [Test]
    public void ExecuteResultIsNotSupported()
    {
        var eq = new Equality(Number.One, Number.Two);

        Assert.Throws<ExecutionException>(() => eq.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Equality(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}