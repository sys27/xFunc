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
    /// Represents the Define operator.
    /// </summary>
    public class Define : IExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Define"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> or <paramref name="value"/> is null.</exception>
        public Define(IExpression key, IExpression value)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(value));

            if (!(key is Variable || key is UserFunction))
                throw new NotSupportedException();

            if (value is null)
                throw new ArgumentNullException(nameof(value));

            Key = key;
            Value = value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var def = obj as Define;
            if (def is null)
                return false;

            return Key.Equals(def.Key) && Value.Equals(def.Value);
        }

        /// <inheritdoc />
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString() => ToString(new CommonFormatter());

        /// <inheritdoc />
        public object Execute() => throw new NotSupportedException();

        /// <inheritdoc />
        public object Execute(ExpressionParameters? parameters)
        {
            if (parameters is null)
                throw new ArgumentNullException(nameof(parameters));

            if (Key is Variable variable)
            {
                parameters.Variables[variable.Name] = Value.Execute(parameters);

                return string.Format(CultureInfo.InvariantCulture, Resource.AssignVariable, Key, Value);
            }

            parameters.Functions[(UserFunction)Key] = Value;

            return string.Format(CultureInfo.InvariantCulture, Resource.AssignFunction, Key, Value);
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
        /// <param name="key">The left argument of new expression.</param>
        /// <param name="value">The right argument of new expression.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public IExpression Clone(IExpression? key = null, IExpression? value = null)
            => new Define(key ?? Key, value ?? Value);

        /// <summary>
        /// Gets the key.
        /// </summary>
        public IExpression Key { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public IExpression Value { get; }
    }
}