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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the "round" function.
    /// </summary>
    public class Round : DifferentParametersExpression
    {

        [ExcludeFromCodeCoverage]
        internal Round() : base(null, -1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded.</param>
        public Round(IExpression argument) : this(new[] { argument }, 1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The expression that represents the number of fractional digits in the return value.</param>
        public Round(IExpression argument, IExpression digits) : this(new[] { argument, digits }, 2) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        /// <exception cref="System.ArgumentNullException">args</exception>
        /// <exception cref="System.ArgumentException">The length of <paramref name="args"/> is not equal to <paramref name="countOfParams"/>.</exception>
        public Round(IExpression[] args, int countOfParams)
            : base(args, countOfParams)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            if (args.Length != countOfParams)
                throw new ArgumentException();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(4211, 10831);
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
            var arg = (double)Argument.Execute(parameters);
            var digits = Digits != null ? (int)(double)Digits.Execute(parameters) : 0;

            return Math.Round(arg, digits, MidpointRounding.AwayFromZero);
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
            return new Round(CloneArguments(), countOfParams);
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinParameters { get; } = 1;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int MaxParameters { get; } = 2;

        /// <summary>
        /// The expression that represents a double-precision floating-point number to be rounded.
        /// </summary>
        /// <value>
        /// The expression that represents a double-precision floating-point number to be rounded.
        /// </value>
        public IExpression Argument
        {
            get
            {
                return m_arguments[0];
            }
        }

        /// <summary>
        /// The expression that represents the number of fractional digits in the return value.
        /// </summary>
        /// <value>
        /// The expression that represents the number of fractional digits in the return value.
        /// </value>
        public IExpression Digits
        {
            get
            {
                return countOfParams == 2 ? m_arguments[1] : null;
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
                if (countOfParams == 2)
                    return new[]
                    {
                        ExpressionResultType.Number,
                        ExpressionResultType.Number
                    };

                return new[] { ExpressionResultType.Number };
            }
        }

    }

}
