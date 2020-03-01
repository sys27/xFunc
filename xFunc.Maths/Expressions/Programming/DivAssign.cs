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

namespace xFunc.Maths.Expressions.Programming
{
    /// <summary>
    /// Represents the "/=" operator.
    /// </summary>
    public class DivAssign : VariableBinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivAssign" /> class.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <param name="exp">The expression.</param>
        public DivAssign(Variable variable, IExpression exp)
            : base(variable, exp)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DivAssign"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <seealso cref="IExpression"/>
        internal DivAssign(IExpression[] arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var result = Variable.Execute(parameters);
            if (result is double value)
            {
                var rightResult = Right.Execute(parameters);
                if (rightResult is double rightValue)
                {
                    var newValue = value / rightValue;
                    parameters.Variables[Variable.Name] = newValue;

                    return newValue;
                }

                throw new ResultIsNotSupportedException(this, rightResult);
            }

            throw new ResultIsNotSupportedException(this, result);
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
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="DivAssign" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone() =>
            new DivAssign((Variable)Variable.Clone(), Right.Clone());
    }
}