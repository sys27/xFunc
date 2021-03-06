// Copyright 2012-2021 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Reflection;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using Xunit;
using Xunit.Sdk;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
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
}