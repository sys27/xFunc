// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class TrigonometricTests : TypeAnalyzerBaseTests
{
    [Theory]
    [InlineData(typeof(Arccos))]
    [InlineData(typeof(Arccot))]
    [InlineData(typeof(Arccsc))]
    [InlineData(typeof(Arcsec))]
    [InlineData(typeof(Arcsin))]
    [InlineData(typeof(Arctan))]
    [InlineData(typeof(Cos))]
    [InlineData(typeof(Cot))]
    [InlineData(typeof(Csc))]
    [InlineData(typeof(Sec))]
    [InlineData(typeof(Sin))]
    [InlineData(typeof(Tan))]
    public void TestUndefined(Type type)
    {
        var exp = Create(type, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Theory]
    [InlineData(typeof(Arccos))]
    [InlineData(typeof(Arccot))]
    [InlineData(typeof(Arccsc))]
    [InlineData(typeof(Arcsec))]
    [InlineData(typeof(Arcsin))]
    [InlineData(typeof(Arctan))]
    public void TestReserveNumber(Type type)
    {
        var exp = Create(type, Number.Two);

        Test(exp, ResultTypes.AngleNumber);
    }

    [Theory]
    [InlineData(typeof(Cos))]
    [InlineData(typeof(Cot))]
    [InlineData(typeof(Csc))]
    [InlineData(typeof(Sec))]
    [InlineData(typeof(Sin))]
    [InlineData(typeof(Tan))]
    public void TestNumber(Type type)
    {
        var exp = Create(type, Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Theory]
    [InlineData(typeof(Cos))]
    [InlineData(typeof(Cot))]
    [InlineData(typeof(Csc))]
    [InlineData(typeof(Sec))]
    [InlineData(typeof(Sin))]
    [InlineData(typeof(Tan))]
    public void TestAngleNumber(Type type)
    {
        var exp = Create(type, AngleValue.Degree(10).AsExpression());

        Test(exp, ResultTypes.Number);
    }

    [Theory]
    [InlineData(typeof(Arccos))]
    [InlineData(typeof(Arccot))]
    [InlineData(typeof(Arccsc))]
    [InlineData(typeof(Arcsec))]
    [InlineData(typeof(Arcsin))]
    [InlineData(typeof(Arctan))]
    [InlineData(typeof(Cos))]
    [InlineData(typeof(Cot))]
    [InlineData(typeof(Csc))]
    [InlineData(typeof(Sec))]
    [InlineData(typeof(Sin))]
    [InlineData(typeof(Tan))]
    public void TestComplexNumber(Type type)
    {
        var exp = Create(type, new ComplexNumber(2, 2));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Theory]
    [InlineData(typeof(Arccos))]
    [InlineData(typeof(Arccot))]
    [InlineData(typeof(Arccsc))]
    [InlineData(typeof(Arcsec))]
    [InlineData(typeof(Arcsin))]
    [InlineData(typeof(Arctan))]
    [InlineData(typeof(Cos))]
    [InlineData(typeof(Cot))]
    [InlineData(typeof(Csc))]
    [InlineData(typeof(Sec))]
    [InlineData(typeof(Sin))]
    [InlineData(typeof(Tan))]
    public void TestParameterException(Type type)
    {
        var exp = Create(type, Bool.False);

        TestException(exp);
    }
}