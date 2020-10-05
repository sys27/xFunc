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
using System.Collections.Generic;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Maths
{
    /// <summary>
    /// The helper class with additional methods.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Checks that <paramref name="expression"/> has  the <paramref name="variable"/> variable.
        /// </summary>
        /// <param name="expression">The expression that is checked.</param>
        /// <param name="variable">The variable that can be contained in the expression.</param>
        /// <returns>true if <paramref name="expression"/> has <paramref name="variable"/>; otherwise, false.</returns>
        public static bool HasVariable(IExpression expression, Variable variable)
        {
            if (expression is BinaryExpression bin)
                return HasVariable(bin.Left, variable) || HasVariable(bin.Right, variable);

            if (expression is UnaryExpression un)
                return HasVariable(un.Argument, variable);

            if (expression is DifferentParametersExpression paramExp)
            {
                foreach (var argument in paramExp.Arguments)
                    if (HasVariable(argument, variable))
                        return true;

                return false;
            }

            return expression is Variable && expression.Equals(variable);
        }

        /// <summary>
        /// Gets parameters of expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A collection of parameters.</returns>
        public static ParameterCollection GetParameters(IExpression expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var parameters = new SortedSet<Parameter>();
            var variables = GetAllVariables(expression);

            foreach (var variable in variables)
                parameters.Add(new Parameter(variable.Name, false));

            return new ParameterCollection(parameters, false);
        }

        /// <summary>
        /// Converts the logic expression to collection.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The collection of expression parts.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="expression"/> variable is null.</exception>
        public static List<IExpression> ConvertExpressionToCollection(IExpression expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var collection = new List<IExpression>();
            ConvertToCollection(expression, collection);

            return collection;
        }

        private static void ConvertToCollection(IExpression expression, List<IExpression> collection)
        {
            if (expression is UnaryExpression un)
            {
                ConvertToCollection(un.Argument, collection);
            }
            else if (expression is BinaryExpression bin)
            {
                ConvertToCollection(bin.Left, collection);
                ConvertToCollection(bin.Right, collection);
            }
            else if (expression is Variable)
            {
                return;
            }

            collection.Add(expression);
        }

        /// <summary>
        /// Gets all variables.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The list of variables.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is null.</exception>
        public static IEnumerable<Variable> GetAllVariables(IExpression expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var collection = new HashSet<Variable>();
            GetAllVariables(expression, collection);

            return collection;
        }

        private static void GetAllVariables(IExpression expression, HashSet<Variable> collection)
        {
            if (expression is UnaryExpression un)
            {
                GetAllVariables(un.Argument, collection);
            }
            else if (expression is BinaryExpression bin)
            {
                GetAllVariables(bin.Left, collection);
                GetAllVariables(bin.Right, collection);
            }
            else if (expression is DifferentParametersExpression diff)
            {
                foreach (var exp in diff.Arguments)
                    GetAllVariables(exp, collection);
            }
            else if (expression is Variable variable)
            {
                collection.Add(variable);
            }
        }
    }
}