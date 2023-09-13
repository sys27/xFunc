// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class LogTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestLbUndefined()
    {
        var exp = new Lb(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLbNumber()
    {
        var exp = new Lb(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLbRational()
    {
        var exp = new Lb(new Rational(new Number(2), new Number(3)));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLbException()
    {
        var exp = new Lb(Bool.False);

        TestException(exp);
    }

    [Test]
    public void TestLgUndefined()
    {
        var exp = new Lg(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestLgNumber()
    {
        var exp = new Lg(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLgComplexNumber()
    {
        var exp = new Lg(new ComplexNumber(10, 10));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestLgRational()
    {
        var exp = new Lg(new Rational(new Number(2), new Number(3)));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLgException()
    {
        var exp = new Lg(Bool.False);

        TestException(exp);
    }

    [Test]
    public void TestLnUndefined()
    {
        var exp = new Ln(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestLnNumber()
    {
        var exp = new Ln(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLnComplexNumber()
    {
        var exp = new Ln(new ComplexNumber(10, 10));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestLnRational()
    {
        var exp = new Ln(new Rational(new Number(2), new Number(3)));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLnException()
    {
        var exp = new Ln(Bool.False);

        TestException(exp);
    }

    [Test]
    public void TestLogNumberAndUndefined()
    {
        var exp = new Log(Number.Two, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestLogUndefinedAndNumber()
    {
        var exp = new Log(Variable.X, Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestLogNumber()
    {
        var exp = new Log(Number.Two, new Number(4));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLogComplexNumber()
    {
        var exp = new Log(Number.Two, new ComplexNumber(8, 3));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestLogRational()
    {
        var exp = new Log(new Number(3), new Rational(new Number(2), new Number(3)));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLogException()
    {
        var exp = new Log(Number.Two, Bool.False);

        TestBinaryException(exp);
    }

    [Test]
    public void TestLogBaseIsNotNumber()
    {
        var exp = new Log(Bool.False, Number.Two);

        TestBinaryException(exp);
    }
}