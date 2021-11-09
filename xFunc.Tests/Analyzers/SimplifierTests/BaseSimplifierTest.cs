// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Xunit.Sdk;

namespace xFunc.Tests.Analyzers.SimplifierTests;

public abstract class BaseSimplifierTest : BaseTest
{
    protected readonly ISimplifier simplifier;

    protected BaseSimplifierTest()
    {
        simplifier = new Simplifier();
    }

    protected void SimplifyTest(IExpression exp, IExpression expected)
    {
        var simple = exp.Analyze(simplifier);

        Assert.Equal(expected, simple);
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

        throw new XunitException("The exception is expected.");
    }
}