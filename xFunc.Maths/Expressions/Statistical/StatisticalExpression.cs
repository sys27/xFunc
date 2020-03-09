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

using System.Collections.Generic;
using System.Linq;
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Maths.Expressions.Statistical
{
    /// <summary>
    /// Represent the Avg function.
    /// </summary>
    /// <seealso cref="DifferentParametersExpression" />
    public abstract class StatisticalExpression : DifferentParametersExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticalExpression"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        protected StatisticalExpression(IList<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="numbers">The array of expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        private protected abstract double ExecuteInternal(double[] numbers);

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
            var data = Arguments;

            if (ParametersCount == 1)
            {
                var result = this[0].Execute(parameters);
                if (result is Vector vector)
                    data = vector.Arguments;
            }

            var calculated = data.Select(exp =>
            {
                var result = exp.Execute(parameters);
                if (result is double doubleResult)
                    return doubleResult;

                throw new ResultIsNotSupportedException();
            }).ToArray();

            return ExecuteInternal(calculated);
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int? MinParametersCount => 1;

        /// <summary>
        /// Gets the maximum count of parameters. <c>null</c> - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int? MaxParametersCount => null;
    }
}