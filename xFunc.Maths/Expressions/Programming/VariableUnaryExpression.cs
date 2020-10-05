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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions.Programming
{
    /// <summary>
    /// The abstract base class that represents the unary operation with variable as argument.
    /// </summary>
    public abstract class VariableUnaryExpression : IExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableUnaryExpression"/> class.
        /// </summary>
        /// <param name="variable">The expression.</param>
        protected VariableUnaryExpression(Variable variable)
            => Variable = variable ?? throw new ArgumentNullException(nameof(variable));

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null || GetType() != obj.GetType())
                return false;

            return Variable.Equals(((VariableUnaryExpression)obj).Variable);
        }

        /// <inheritdoc />
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString() => ToString(new CommonFormatter());

        /// <inheritdoc />
        public object Execute() => Execute(null);

        /// <inheritdoc />
        public object Execute(ExpressionParameters? parameters)
        {
            if (parameters is null)
                throw new ArgumentNullException(nameof(parameters));

            var result = Variable.Execute(parameters);
            if (result is NumberValue number)
                return parameters.Variables[Variable.Name] = Execute(number);

            throw new ResultIsNotSupportedException(this, result);
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        protected abstract object Execute(NumberValue number);

        /// <inheritdoc />
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer is null)
                throw new ArgumentNullException(nameof(analyzer));

            return AnalyzeInternal(analyzer);
        }

        /// <inheritdoc />
        public TResult Analyze<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
        {
            if (analyzer is null)
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
        /// <param name="variable">The argument of new expression.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public abstract IExpression Clone(Variable? variable = null);

        /// <summary>
        /// Gets the variable.
        /// </summary>
        /// <value>The variable.</value>
        public Variable Variable { get; }
    }
}