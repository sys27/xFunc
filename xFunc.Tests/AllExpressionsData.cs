// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Tests
{
    public class AllExpressionsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var iExp = typeof(IExpression);
            var asm = Assembly.GetAssembly(iExp);
            if (asm is null)
                throw new InvalidOperationException();

            var exclude = new HashSet<Type>
            {
                typeof(Builder)
            };

            var types = asm
                .GetTypes()
                .Where(type => iExp.IsAssignableFrom(type) && !type.IsAbstract && !exclude.Contains(type))
                .Select(type => new[] { type });

            return types.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}