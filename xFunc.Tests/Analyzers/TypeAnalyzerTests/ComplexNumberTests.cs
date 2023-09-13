// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class ComplexNumberTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestComplexNumber()
    {
        var exp = new ComplexNumber(2, 2);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestConjugateUndefined()
    {
        var exp = new Conjugate(Variable.X);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestConjugateComplexNumber()
    {
        var exp = new Conjugate(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestConjugateException()
    {
        var exp = new Conjugate(Number.Two);

        TestException(exp);
    }

    [Test]
    public void TestImUndefined()
    {
        var exp = new Im(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestImComplexNumber()
    {
        var exp = new Im(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestImException()
    {
        var exp = new Im(Number.Two);

        TestException(exp);
    }

    [Test]
    public void TestPhaseUndefined()
    {
        var exp = new Phase(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestPhaseComplexNumber()
    {
        var exp = new Phase(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestPhaseException()
    {
        var exp = new Phase(Number.Two);

        TestException(exp);
    }

    [Test]
    public void TestReUndefined()
    {
        var exp = new Re(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestReComplexNumber()
    {
        var exp = new Re(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestReException()
    {
        var exp = new Re(Number.Two);

        TestException(exp);
    }

    [Test]
    public void TestReciprocalUndefined()
    {
        var exp = new Reciprocal(Variable.X);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestReciprocalComplexNumber()
    {
        var exp = new Reciprocal(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestReciprocalException()
    {
        var exp = new Reciprocal(Number.Two);

        TestException(exp);
    }

    [Test]
    public void ToComplexUndefined()
        => Test(new ToComplex(Variable.X), ResultTypes.ComplexNumber);

    [Test]
    public void ToComplexNubmer()
        => Test(new ToComplex(Number.One), ResultTypes.ComplexNumber);

    [Test]
    public void ToComplexException()
        => TestException(new ToComplex(Bool.False));
}