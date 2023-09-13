// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class TrigonometricTests : TypeAnalyzerBaseTests
{
    [Test]
    [TestCase(typeof(Arccos))]
    [TestCase(typeof(Arccot))]
    [TestCase(typeof(Arccsc))]
    [TestCase(typeof(Arcsec))]
    [TestCase(typeof(Arcsin))]
    [TestCase(typeof(Arctan))]
    [TestCase(typeof(Cos))]
    [TestCase(typeof(Cot))]
    [TestCase(typeof(Csc))]
    [TestCase(typeof(Sec))]
    [TestCase(typeof(Sin))]
    [TestCase(typeof(Tan))]
    public void TestUndefined(Type type)
    {
        var exp = Create(type, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    [TestCase(typeof(Arccos))]
    [TestCase(typeof(Arccot))]
    [TestCase(typeof(Arccsc))]
    [TestCase(typeof(Arcsec))]
    [TestCase(typeof(Arcsin))]
    [TestCase(typeof(Arctan))]
    public void TestReserveNumber(Type type)
    {
        var exp = Create(type, Number.Two);

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    [TestCase(typeof(Cos))]
    [TestCase(typeof(Cot))]
    [TestCase(typeof(Csc))]
    [TestCase(typeof(Sec))]
    [TestCase(typeof(Sin))]
    [TestCase(typeof(Tan))]
    public void TestNumber(Type type)
    {
        var exp = Create(type, Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    [TestCase(typeof(Cos))]
    [TestCase(typeof(Cot))]
    [TestCase(typeof(Csc))]
    [TestCase(typeof(Sec))]
    [TestCase(typeof(Sin))]
    [TestCase(typeof(Tan))]
    public void TestAngleNumber(Type type)
    {
        var exp = Create(type, AngleValue.Degree(10).AsExpression());

        Test(exp, ResultTypes.Number);
    }

    [Test]
    [TestCase(typeof(Arccos))]
    [TestCase(typeof(Arccot))]
    [TestCase(typeof(Arccsc))]
    [TestCase(typeof(Arcsec))]
    [TestCase(typeof(Arcsin))]
    [TestCase(typeof(Arctan))]
    [TestCase(typeof(Cos))]
    [TestCase(typeof(Cot))]
    [TestCase(typeof(Csc))]
    [TestCase(typeof(Sec))]
    [TestCase(typeof(Sin))]
    [TestCase(typeof(Tan))]
    public void TestComplexNumber(Type type)
    {
        var exp = Create(type, new ComplexNumber(2, 2));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    [TestCase(typeof(Arccos))]
    [TestCase(typeof(Arccot))]
    [TestCase(typeof(Arccsc))]
    [TestCase(typeof(Arcsec))]
    [TestCase(typeof(Arcsin))]
    [TestCase(typeof(Arctan))]
    [TestCase(typeof(Cos))]
    [TestCase(typeof(Cot))]
    [TestCase(typeof(Csc))]
    [TestCase(typeof(Sec))]
    [TestCase(typeof(Sin))]
    [TestCase(typeof(Tan))]
    public void TestParameterException(Type type)
    {
        var exp = Create(type, Bool.False);

        TestException(exp);
    }
}