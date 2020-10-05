// Copyright 2012-2020 Dmytro Kyshchenko
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