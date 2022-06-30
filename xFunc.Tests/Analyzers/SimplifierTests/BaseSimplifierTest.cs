// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using NUnit.Framework.Internal;
using xFunc.Maths.Analyzers2;
using Simplifier = xFunc.Maths.Analyzers.Simplifier;

namespace xFunc.Tests.Analyzers.SimplifierTests;

public abstract class BaseSimplifierTest : BaseTest
{
    protected readonly IAnalyzer2 simplifier;

    protected BaseSimplifierTest()
    {
        simplifier = new Simplifier2();
    }

    protected void SimplifyTest(IExpression exp, IExpression expected)
    {
        var simple = simplifier.Analyze(exp);

        Assert.That(simple, Is.EqualTo(expected));
    }

    protected void TestNullExp(Type type)
    {
        try
        {
            var method = typeof(Simplifier)
                .GetMethod(nameof(Simplifier.Analyze), new[] { type });
            method.Invoke(simplifier, new object[] { null });
        }
        catch (TargetInvocationException e)
        {
            if (e.InnerException is ArgumentNullException)
                return;

            throw;
        }

        throw new NUnitException("The exception is expected.");
    }
}