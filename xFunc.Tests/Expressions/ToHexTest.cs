// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class ToHexTest : BaseExpressionTests
{
    [Test]
    [TestCase(0x7, "0x07")]
    [TestCase(0x7FF, "0x07FF")]
    [TestCase(0x7FFFF, "0x07FFFF")]
    [TestCase(0x7FFFFFF, "0x07FFFFFF")]
    public void ExecuteNumberTest(double number, string result)
    {
        var exp = new ToHex(new Number(number));

        Assert.That(exp.Execute(), Is.EqualTo(result));
    }

    [Test]
    public void ExecuteNumberExceptionTest()
    {
        var exp = new ToHex(new Number(2.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteLongMaxNumberTest()
    {
        var exp = new ToHex(new Number(int.MaxValue));

        Assert.That(exp.Execute(), Is.EqualTo("0x7FFFFFFF"));
    }

    [Test]
    public void ExecuteNegativeNumberTest()
    {
        var exp = new ToHex(new Number(-2));

        Assert.That(exp.Execute(), Is.EqualTo("0xFFFFFFFE"));
    }

    [Test]
    public void ExecuteBoolTest()
        => TestNotSupported(new ToHex(Bool.False));

    [Test]
    public void CloseTest()
    {
        var exp = new ToHex(new Number(10));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}