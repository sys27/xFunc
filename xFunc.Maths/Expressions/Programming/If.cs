// Copyright 2012-2017 Dmitry Kischenko
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
using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Programming
{

    /// <summary>
    /// Represents the "if-else" statement.
    /// </summary>
    public class If : DifferentParametersExpression
    {

        [ExcludeFromCodeCoverage]
        internal If() : base(null, -1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="If"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="then">The "then" statement.</param>
        public If(IExpression condition, IExpression then) : base(new[] { condition, then }, 2) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="If"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="then">The "then" statement.</param>
        /// <param name="else">The "else" statement.</param>
        public If(IExpression condition, IExpression then, IExpression @else) : base(new[] { condition, then, @else }, 3) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="If"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        public If(IExpression[] arguments, int countOfParams)
            : base(arguments, countOfParams)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));
            if (arguments.Length != countOfParams)
                throw new ArgumentException();
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
            return new If(CloneArguments(), ParametersCount);
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinParameters => 2;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int MaxParameters => 3;

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
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        /// <remarks>
        /// Usage of this property can affect performance. Don't use this property each time if you need to check result type of current expression. Just store/cache value only once and use it everywhere.
        /// </remarks>
        public override ExpressionResultType ResultType
        {
            get
            {
                var @else = Else;
                if (@else != null)
                    return Then.ResultType | @else.ResultType;

                return Then.ResultType;
            }
        }

        /// <summary>
        /// Gets the arguments types.
        /// </summary>
        /// <value>
        /// The arguments types.
        /// </value>
        public override ExpressionResultType[] ArgumentsTypes
        {
            get
            {
                if (ParametersCount == 3)
                    return new[]
                    {
                        ExpressionResultType.Boolean, // Condition
                        ExpressionResultType.All,     // Then
                        ExpressionResultType.All      // Else
                    };

                return new[]
                {
                    ExpressionResultType.Boolean, // Condition
                    ExpressionResultType.All      // Then
                };
            }
        }

    }

}
