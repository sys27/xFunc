// Copyright 2012-2021 Dmytro Kyshchenko
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

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Units;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the 'frac' function.
    /// </summary>
    public class Frac : UnaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Frac"/> class.
        /// </summary>
        /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded down.</param>
        public Frac(IExpression argument)
            : base(argument)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frac"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Frac(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var result = Argument.Execute(parameters);

            return result switch
            {
                NumberValue number => NumberValue.Frac(number),
                AngleValue angle => AngleValue.Frac(angle),
                PowerValue power => PowerValue.Frac(power),
                _ => throw new ResultIsNotSupportedException(this, result),
            };
        }

        /// <inheritdoc />
        protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <inheritdoc />
        public override IExpression Clone(IExpression? argument = null)
            => new Frac(argument ?? Argument);
    }
}