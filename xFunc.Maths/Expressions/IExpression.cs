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

using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Defines methods to work with expressions.
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>A result of the execution.</returns>
        object Execute();

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>A result of the execution.</returns>
        /// <seealso cref="ExpressionParameters"/>
        object Execute(ExpressionParameters? parameters);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        string ToString(IFormatter formatter);

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>The analysis result.</returns>
        TResult Analyze<TResult>(IAnalyzer<TResult> analyzer);

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="context">The context.</param>
        /// <returns>The analysis result.</returns>
        TResult Analyze<TResult, TContext>(IAnalyzer<TResult, TContext> analyzer, TContext context);
    }
}