// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class ToBinTest : BaseExpressionTests
{
    [Test]
    [TestCase(0x7F, "0b01111111")]
    [TestCase(0x7FFF, "0b0111111111111111")]
    [TestCase(0x7FFFFF, "0b011111111111111111111111")]
    [TestCase(0x7FFFFFFF, "0b01111111111111111111111111111111")]
    public void ExecuteNumberTest(double number, string result)
    {
        var exp = new ToBin(new Number(number));

        Assert.That(exp.Execute(), Is.EqualTo(result));
    }

    [Test]
    public void ExecuteNumberExceptionTest()
    {
        var exp = new ToBin(new Number(2.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteLongMaxNumberTest()
    {
        var exp = new ToBin(new Number(int.MaxValue));

        Assert.That(exp.Execute(), Is.EqualTo("0b01111111111111111111111111111111"));
    }

    [Test]
    public void ExecuteNegativeNumberTest()
    {
        var exp = new ToBin(new Number(-2));

        Assert.That(exp.Execute(), Is.EqualTo("0b11111111111111111111111111111110"));
    }

    [Test]
    public void ExecuteBoolTest()
        => TestNotSupported(new ToBin(Bool.False));

    [Test]
    public void CloseTest()
    {
        var exp = new ToBin(new Number(10));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}