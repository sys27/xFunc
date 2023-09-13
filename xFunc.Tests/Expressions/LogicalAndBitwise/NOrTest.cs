// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class NOrTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var nor = new NOr(Bool.False, Bool.True);

        Assert.That((bool) nor.Execute(), Is.False);
    }

    [Test]
    public void ExecuteTest2()
    {
        var nor = new NOr(Bool.False, Bool.False);

        Assert.That((bool) nor.Execute(), Is.True);
    }

    [Test]
    public void ExecuteResultIsNotSupported()
        => TestNotSupported(new NOr(Number.One, Number.Two));

    [Test]
    public void CloneTest()
    {
        var exp = new NOr(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}