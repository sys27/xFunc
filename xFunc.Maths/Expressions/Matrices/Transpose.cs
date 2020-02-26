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
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Matrices
{
    /// <summary>
    /// Represets the Transpose function.
    /// </summary>
    public class Transpose : UnaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transpose"/> class.
        /// </summary>
        /// <param name="argument">The expression, which returns matrix of vector.</param>
        public Transpose(IExpression argument)
            : base(argument)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transpose"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Transpose(IExpression[] arguments)
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
        /// <exception cref="NotSupportedException">Argument is not <see cref="Matrix"/> or <see cref="Vector"/>.</exception>
        public override object Execute(ExpressionParameters parameters)
        {
            var result = Argument.Execute(parameters);

            switch (result)
            {
                case Matrix matrix:
                    if (matrix.Arguments.Any(x => x == null))
                        throw new ArgumentException(Resource.SequenceNullValuesError);
                    if (matrix.Arguments.OfType<Vector>().Any(x => x.Arguments.All(z => z == null)))
                        throw new ArgumentException(Resource.SequenceNullValuesError);

                    return matrix.Transpose();

                case Vector vector:
                    if (vector.Arguments.Any(x => x == null))
                        throw new ArgumentException(Resource.SequenceNullValuesError);

                    return vector.Transpose();

                default:
                    throw new ResultIsNotSupportedException(this, result);
            }
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
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone() =>
            new Transpose(this.Argument.Clone());
    }
}