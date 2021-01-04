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
using System.Collections.Generic;
using xFunc.Maths.Expressions;

namespace xFunc.Tests
{
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
}