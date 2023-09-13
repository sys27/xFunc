// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class PowTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestPowComplexAndUndefined()
    {
        var exp = new Pow(new ComplexNumber(2, 2), Variable.X);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestPowComplexAndNumber()
    {
        var exp = new Pow(new ComplexNumber(2, 4), new Number(4));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestPowComplexAndComplex()
    {
        var exp = new Pow(new ComplexNumber(4, 2), new ComplexNumber(2, 4));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestPowUndefinedAndNumber()
    {
        var exp = new Pow(Variable.X, Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestPowNumberAndUndefined()
    {
        var exp = new Pow(Number.Two, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestPowNumber()
    {
        var exp = new Pow(new Number(4), Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestNumberAndComplex()
    {
        var exp = new Pow(Number.Two, new ComplexNumber(2, 4));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestRationalAndNumber()
    {
        var exp = new Pow(
            new Rational(new Number(2), new Number(3)),
            new Number(3));

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestPowException()
    {
        var exp = new Pow(Bool.False, Bool.False);

        TestBinaryException(exp);
    }
}