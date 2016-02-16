// Copyright 2012-2015 Dmitry Kischenko
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
using System.Linq;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    public static class Helpers
    {

        /// <summary>
        /// Checks the <paramref name="expression"/> parameter has <paramref name="arg"/>.
        /// </summary>
        /// <param name="expression">A expression that is checked.</param>
        /// <param name="arg">A variable that can be contained in the expression.</param>
        /// <returns>true if <paramref name="expression"/> has <paramref name="arg"/>; otherwise, false.</returns>
        public static bool HasVar(IExpression expression, Variable arg)
        {
            var bin = expression as BinaryExpression;
            if (bin != null)
                return HasVar(bin.Left, arg) || HasVar(bin.Right, arg);

            var un = expression as UnaryExpression;
            if (un != null)
                return HasVar(un.Argument, arg);

            var paramExp = expression as DifferentParametersExpression;
            if (paramExp != null)
                return paramExp.Arguments.Any(e => HasVar(e, arg));

            return expression is Variable && expression.Equals(arg);
        }

        /// <summary>
        /// Gets parameters of expression.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        /// <returns>A collection of parameters.</returns>
        public static ParameterCollection GetParameters(IEnumerable<IToken> tokens)
        {
#if PORTABLE
            var c = new List<Parameter>();
#else
            var c = new SortedSet<Parameter>();
#endif

            foreach (var token in tokens)
            {
                var @var = token as VariableToken;
                if (@var != null)
                    c.Add(new Parameter(@var.Variable, false));
            }
#if PORTABLE
            c.Sort();
#endif

            return new ParameterCollection(c, false);
        }

        /// <summary>
        /// Converts the logic expression to collection.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The collection of expression parts.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="expression"/> variable is null.</exception>
        public static IEnumerable<IExpression> ConvertExpressionToCollection(IExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var collection = new List<IExpression>();
            ConvertToColletion(expression, collection);

            return collection;
        }

        private static void ConvertToColletion(IExpression expression, List<IExpression> collection)
        {
            if (expression is UnaryExpression)
            {
                var un = expression as UnaryExpression;
                ConvertToColletion(un.Argument, collection);
            }
            else if (expression is BinaryExpression)
            {
                var bin = expression as BinaryExpression;
                ConvertToColletion(bin.Left, collection);
                ConvertToColletion(bin.Right, collection);
            }
            else if (expression is Variable)
            {
                return;
            }

            collection.Add(expression);
        }

    }

}
