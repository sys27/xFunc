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
using System.Collections.Immutable;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the user-defined function.
    /// </summary>
    public class UserFunction : DifferentParametersExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserFunction"/> class.
        /// </summary>
        /// <param name="function">The name of function.</param>
        /// <param name="arguments">Arguments.</param>
        public UserFunction(string function, IEnumerable<IExpression> arguments)
            : this(function, arguments.ToImmutableArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFunction"/> class.
        /// </summary>
        /// <param name="function">The name of function.</param>
        /// <param name="arguments">Arguments.</param>
        public UserFunction(string function, ImmutableArray<IExpression> arguments)
            : base(arguments)
            => Function = function;

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
            => obj is UserFunction exp &&
               Function == exp.Function &&
               ParametersCount == exp.ParametersCount;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
            => HashCode.Combine(Function, ParametersCount);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => ToString(new CommonFormatter());

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters? parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var func = parameters.Functions.GetKey(this);

            var newParams = new ExpressionParameters(parameters);
            for (var i = 0; i < ParametersCount; i++)
                if (func.Arguments[i] is Variable arg)
                    newParams.Variables[arg.Name] = Arguments[i].Execute(parameters);

            return parameters.Functions[this].Execute(newParams);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="context">The context.</param>
        /// <returns>The analysis result.</returns>
        private protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
            => new UserFunction(Function, arguments ?? Arguments);

        /// <summary>
        /// Gets the name of function.
        /// </summary>
        /// <value>The name of function.</value>
        public string Function { get; }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int? MinParametersCount => null;

        /// <summary>
        /// Gets the maximum count of parameters. <c>null</c> - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int? MaxParametersCount => null;
    }
}