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
    /// Represents the "for" loop.
    /// </summary>
    public class For : DifferentParametersExpression
    {

        [ExcludeFromCodeCoverage]
        internal For() : base(null, -1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="For"/> class.
        /// </summary>
        /// <param name="body">The body of loop.</param>
        /// <param name="init">The initializer section.</param>
        /// <param name="cond">The condition section.</param>
        /// <param name="iter">The itererator section.</param>
        public For(IExpression body, IExpression init, IExpression cond, IExpression iter) : base(new[] { body, init, cond, iter }, 4) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="For" /> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="arguments"/> is null.</exception>
        /// <exception cref="System.ArgumentException">The length of <paramref name="arguments"/> is not equal to <paramref name="countOfParams"/>.</exception>
        public For(IExpression[] arguments, int countOfParams)
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
            for (Initialization.Execute(parameters); (bool)Condition.Execute(parameters); Iteration.Execute(parameters))
                Body.Execute(parameters);

            return double.NaN;
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
        /// Clones this instance of the <see cref="For" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="For" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new For(CloneArguments(), countOfParams);
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinParameters
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int MaxParameters
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Gets the body of loop.
        /// </summary>
        /// <value>
        /// The body of loop.
        /// </value>
        public IExpression Body
        {
            get
            {
                return m_arguments[0];
            }
        }

        /// <summary>
        /// Gets the initializer section.
        /// </summary>
        /// <value>
        /// The initializer section.
        /// </value>
        public IExpression Initialization
        {
            get
            {
                return m_arguments[1];
            }
        }

        /// <summary>
        /// Gets the condition section.
        /// </summary>
        /// <value>
        /// The condition section.
        /// </value>
        public IExpression Condition
        {
            get
            {
                return m_arguments[2];
            }
        }

        /// <summary>
        /// Gets the iterator section.
        /// </summary>
        /// <value>
        /// The iterator section.
        /// </value>
        public IExpression Iteration
        {
            get
            {
                return m_arguments[3];
            }
        }

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public override ExpressionResultType ResultType
        {
            get
            {
                return ExpressionResultType.Undefined;
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
                return new[]
                {
                    ExpressionResultType.All,     // Body
                    ExpressionResultType.All,     // Initialization
                    ExpressionResultType.Boolean, // Condition
                    ExpressionResultType.All      // Iteration
                };
            }
        }

    }

}
