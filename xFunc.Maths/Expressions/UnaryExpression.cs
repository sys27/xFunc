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
using System.Collections.Immutable;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// The abstract base class that represents the unary operation.
    /// </summary>
    public abstract class UnaryExpression : IExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        protected UnaryExpression(IExpression argument)
            => Argument = argument ?? throw new ArgumentNullException(nameof(argument));

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        protected UnaryExpression(ImmutableArray<IExpression> arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            if (arguments.Length < 1)
                throw new ParseException(Resource.LessParams);

            if (arguments.Length > 1)
                throw new ParseException(Resource.MoreParams);

            Argument = arguments[0];
        }

        /// <summary>
        /// Deconstructs <see cref="UnaryExpression"/>.
        /// </summary>
        /// <param name="argument">The argument.</param>
        public void Deconstruct(out IExpression argument) => argument = Argument;

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (this == obj)
                return true;

            if (obj == null || GetType() != obj.GetType())
                return false;

            return Argument.Equals(((UnaryExpression)obj).Argument);
        }

        /// <inheritdoc />
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString() => ToString(new CommonFormatter());

        /// <inheritdoc />
        public object Execute() => Execute(null);

        /// <inheritdoc />
        public abstract object Execute(ExpressionParameters? parameters);

        /// <inheritdoc />
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            return AnalyzeInternal(analyzer);
        }

        /// <inheritdoc />
        public TResult Analyze<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            return AnalyzeInternal(analyzer, context);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        protected abstract TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer);

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="context">The context.</param>
        /// <returns>The analysis result.</returns>
        protected abstract TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context);

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <param name="argument">The argument of new expression.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public abstract IExpression Clone(IExpression? argument = null);

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public IExpression Argument { get; }
    }
}