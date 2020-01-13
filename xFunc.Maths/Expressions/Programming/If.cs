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
    /// Represents the "if-else" statement.
    /// </summary>
    public class If : DifferentParametersExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="If"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="then">The "then" statement.</param>
        public If(IExpression condition, IExpression then) : base(new[] { condition, then }) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="If"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="then">The "then" statement.</param>
        /// <param name="else">The "else" statement.</param>
        public If(IExpression condition, IExpression then, IExpression @else) : base(new[] { condition, then, @else }) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="If"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public If(IExpression[] arguments) : base(arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));
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
            if ((bool)Condition.Execute(parameters))
                return Then.Execute(parameters);

            var @else = Else;

            return @else != null ? @else.Execute(parameters) : 0.0;
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
        /// Clones this instance of the <see cref="If" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="If" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new If(CloneArguments());
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public IExpression Condition => m_arguments[0];

        /// <summary>
        /// Gets the "then" statement.
        /// </summary>
        /// <value>
        /// The then.
        /// </value>
        public IExpression Then => m_arguments[1];

        /// <summary>
        /// Gets the "else" statement.
        /// </summary>
        /// <value>
        /// The else.
        /// </value>
        public IExpression Else => ParametersCount == 3 ? m_arguments[2] : null;

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int? MinParametersCount => 2;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int? MaxParametersCount => 3;

    }

}