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
using System.Linq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Maths.Expressions.Statistical
{
    /// <summary>
    /// Represents the STDEVP function.
    /// </summary>
    /// <seealso cref="DifferentParametersExpression" />
    public class Stdevp : StatisticalExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stdevp"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public Stdevp(IExpression[] arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return GetHashCode(16547, 431);
        }

        private double[] ExecuteArray(IExpression[] expression, ExpressionParameters parameters)
        {
            return expression.Select(exp =>
            {
                var result = exp.Execute(parameters);
                if (result is double doubleResult)
                    return doubleResult;

                throw new ResultIsNotSupportedException();
            }).ToArray();
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
            var data = Arguments;

            if (ParametersCount == 1)
            {
                var result = Arguments[0].Execute(parameters);
                if (result is Vector vector)
                    data = vector.Arguments;
            }

            var calculatedArray = ExecuteArray(data, parameters);
            var avg = calculatedArray.Average();
            var variance = calculatedArray.Average(x => Math.Pow(x - avg, 2));

            return Math.Sqrt(variance);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public override TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Stdevp(CloneArguments());
        }
    }
}