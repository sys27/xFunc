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

namespace xFunc.Maths.Expressions.LogicalAndBitwise
{
    /// <summary>
    /// Represents a bitwise NOT operator.
    /// </summary>
    public class Not : UnaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Not"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        public Not(IExpression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Executes this bitwise NOT expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            var arg = Argument.Execute(parameters);

            return arg switch
            {
                bool boolArg => !boolArg,
                double doubleArg => doubleArg.Not(),
                _ => throw new ResultIsNotSupportedException(this, arg),
            };
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer) =>
            analyzer.Analyze(this);

        /// <summary>
        /// Clones this instance of the <see cref="Not"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone() =>
            new Not(Argument.Clone());
    }
}