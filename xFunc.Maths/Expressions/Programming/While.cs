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

using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Programming
{
    /// <summary>
    /// Represents the "while" loop.
    /// </summary>
    public class While : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="While"/> class.
        /// </summary>
        /// <param name="body">The body of while loop.</param>
        /// <param name="condition">The condition of loop.</param>
        public While(IExpression body, IExpression condition)
            : base(body, condition)
        {
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            while ((bool)Right.Execute(parameters))
                Left.Execute(parameters);

            return double.NaN;
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
        public override IExpression Clone(IExpression? left = null, IExpression? right = null)
            => new While(left ?? Left, right ?? Right);
    }
}