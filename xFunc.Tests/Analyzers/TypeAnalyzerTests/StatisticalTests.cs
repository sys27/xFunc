// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class StatisticalTests : TypeAnalyzerBaseTests
{
    [Test]
    [TestCase(typeof(Avg))]
    [TestCase(typeof(Count))]
    [TestCase(typeof(Max))]
    [TestCase(typeof(Min))]
    [TestCase(typeof(Product))]
    [TestCase(typeof(Stdev))]
    [TestCase(typeof(Stdevp))]
    [TestCase(typeof(Sum))]
    [TestCase(typeof(Var))]
    [TestCase(typeof(Varp))]
    public void TestUndefined(Type type)
    {
        var exp = Create(type, new IExpression[] { Variable.X, Variable.Y });

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    [TestCase(typeof(Avg))]
    [TestCase(typeof(Count))]
    [TestCase(typeof(Max))]
    [TestCase(typeof(Min))]
    [TestCase(typeof(Product))]
    [TestCase(typeof(Stdev))]
    [TestCase(typeof(Stdevp))]
    [TestCase(typeof(Sum))]
    [TestCase(typeof(Var))]
    [TestCase(typeof(Varp))]
    public void TestAvgNumber(Type type)
    {
        var exp = Create(type, new IExpression[] { new Number(3), Number.Two });

        Test(exp, ResultTypes.Number);
    }

    [Test]
    [TestCase(typeof(Avg))]
    [TestCase(typeof(Count))]
    [TestCase(typeof(Max))]
    [TestCase(typeof(Min))]
    [TestCase(typeof(Product))]
    [TestCase(typeof(Stdev))]
    [TestCase(typeof(Stdevp))]
    [TestCase(typeof(Sum))]
    [TestCase(typeof(Var))]
    [TestCase(typeof(Varp))]
    public void TestVector(Type type)
    {
        var arguments = new IExpression[]
        {
            new Vector(new IExpression[] { new Number(3), Number.Two })
        };
        var exp = Create(type, arguments);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    [TestCase(typeof(Avg))]
    [TestCase(typeof(Count))]
    [TestCase(typeof(Max))]
    [TestCase(typeof(Min))]
    [TestCase(typeof(Product))]
    [TestCase(typeof(Stdev))]
    [TestCase(typeof(Stdevp))]
    [TestCase(typeof(Sum))]
    [TestCase(typeof(Var))]
    [TestCase(typeof(Varp))]
    public void TestOneParamException(Type type)
    {
        var exp = CreateDiff(type, new IExpression[] { Bool.False });

        TestDiffParamException(exp);
    }

    [Test]
    [TestCase(typeof(Avg))]
    [TestCase(typeof(Count))]
    [TestCase(typeof(Max))]
    [TestCase(typeof(Min))]
    [TestCase(typeof(Product))]
    [TestCase(typeof(Stdev))]
    [TestCase(typeof(Stdevp))]
    [TestCase(typeof(Sum))]
    [TestCase(typeof(Var))]
    [TestCase(typeof(Varp))]
    public void TestParamException(Type type)
    {
        var exp = CreateDiff(type, new IExpression[] { Bool.False, Bool.False });

        TestDiffParamException(exp);
    }
}