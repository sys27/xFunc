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

namespace xFunc.Maths.Expressions.Statistical
{
    /// <summary>
    /// Represents the STDEV function.
    /// </summary>
    /// <seealso cref="DifferentParametersExpression" />
    public class Stdev : StatisticalExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stdev"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public Stdev(IExpression[] arguments)
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
        private protected override double ExecuteInternal(double[] numbers)
        {
            var avg = numbers.Average();
            var variance = numbers.Sum(x => Math.Pow(x - avg, 2)) / (numbers.Length - 1);

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
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer) =>
            analyzer.Analyze(this);

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone() =>
            new Stdev(CloneArguments());
    }
}