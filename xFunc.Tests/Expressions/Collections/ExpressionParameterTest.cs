// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using xFunc.Maths.Expressions;

namespace xFunc.Tests.Expressions.Collections
{
    public class ExpressionParameterTest
    {
        [Fact]
        public void NullCtorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ExpressionParameters(null as ExpressionParameters));
        }
    }
}