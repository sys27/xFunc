// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests;

public abstract class BaseTest
{
    protected IExpression Create(Type type, IExpression argument)
        => (IExpression)Activator.CreateInstance(type, argument);

    protected IExpression Create(Type type, IExpression left, IExpression right)
        => (IExpression)Activator.CreateInstance(type, left, right);

    protected IExpression Create(Type type, IList<IExpression> arguments)
        => (IExpression)Activator.CreateInstance(type, arguments);

    protected T Create<T>(Type type, IList<IExpression> arguments) where T : IExpression
        => (T)Activator.CreateInstance(type, arguments);

    protected BinaryExpression CreateBinary(Type type, IExpression left, IExpression right)
        => (BinaryExpression)Activator.CreateInstance(type, left, right);

    protected DifferentParametersExpression CreateDiff(Type type, IList<IExpression> arguments)
        => (DifferentParametersExpression)Activator.CreateInstance(type, arguments);
}