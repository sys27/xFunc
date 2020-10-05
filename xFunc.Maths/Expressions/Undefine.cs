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
using System.Globalization;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the Undefine operator.
    /// </summary>
    public class Undefine : IExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Undefine"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public Undefine(IExpression key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            if (!(key is Variable || key is UserFunction))
                throw new NotSupportedException();

            Key = key;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var undef = obj as Undefine;
            if (undef is null)
                return false;

            return Key.Equals(undef.Key);
        }

        /// <inheritdoc />
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString() => ToString(new CommonFormatter());

        /// <inheritdoc />
        /// <exception cref="NotSupportedException">Always.</exception>
        public object Execute() => throw new NotSupportedException();

        /// <inheritdoc />
        public object Execute(ExpressionParameters? parameters)
        {
            if (parameters is null)
                throw new ArgumentNullException(nameof(parameters));

            if (Key is Variable variable)
            {
                parameters.Variables.Remove(variable.Name);

                return string.Format(CultureInfo.InvariantCulture, Resource.UndefineVariable, Key);
            }

            parameters.Functions.Remove((UserFunction)Key);

            return string.Format(CultureInfo.InvariantCulture, Resource.UndefineFunction, Key);
        }

        /// <inheritdoc />
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer is null)
                throw new ArgumentNullException(nameof(analyzer));

            return analyzer.Analyze(this);
        }

        /// <inheritdoc />
        public TResult Analyze<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
        {
            if (analyzer is null)
                throw new ArgumentNullException(nameof(analyzer));

            return analyzer.Analyze(this, context);
        }

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <param name="key">The argument of new expression.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public IExpression Clone(IExpression? key = null)
            => new Undefine(key ?? Key);

        /// <summary>
        /// Gets the key.
        /// </summary>
        public IExpression Key { get; }
    }
}