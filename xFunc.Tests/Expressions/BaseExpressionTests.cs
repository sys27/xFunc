// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public abstract class BaseExpressionTests : BaseTest
{
    protected void TestNotSupported(IExpression exp)
        => Assert.Throws<ResultIsNotSupportedException>(exp.Execute);
}