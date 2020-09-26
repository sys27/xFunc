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
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (!(key is Variable || key is UserFunction))
                throw new NotSupportedException();

            Key = key;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (this == obj)
                return true;

            var undef = obj as Undefine;
            if (undef == null)
                return false;

            return Key.Equals(undef.Key);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => ToString(new CommonFormatter());

        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public object Execute() => throw new NotSupportedException();

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> is null.</exception>
        public object Execute(ExpressionParameters? parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (Key is Variable variable)
            {
                parameters.Variables.Remove(variable.Name);

                return string.Format(CultureInfo.InvariantCulture, Resource.UndefineVariable, Key);
            }

            parameters.Functions.Remove((UserFunction)Key);

            return string.Format(CultureInfo.InvariantCulture, Resource.UndefineFunction, Key);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="context">The context.</param>
        /// <returns>The analysis result.</returns>
        public TResult Analyze<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
        {
            if (analyzer == null)
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