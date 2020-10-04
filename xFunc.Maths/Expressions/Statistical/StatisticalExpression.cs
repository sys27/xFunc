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
using System.Collections.Immutable;
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
        protected StatisticalExpression(IEnumerable<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticalExpression"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        protected StatisticalExpression(ImmutableArray<IExpression> arguments)
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
        protected abstract double ExecuteInternal(double[] numbers);

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var (data, size) = (Arguments, ParametersCount);

            if (ParametersCount == 1)
            {
                var result = this[0].Execute(parameters);
                if (result is Vector vector)
                    (data, size) = (vector.Arguments, vector.ParametersCount);
            }

            var calculated = new double[size];
            var i = 0;
            foreach (var expression in data)
            {
                var result = expression.Execute(parameters);
                if (!(result is double doubleResult))
                    throw new ResultIsNotSupportedException(this, result);

                calculated[i] = doubleResult;
                i++;
            }

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