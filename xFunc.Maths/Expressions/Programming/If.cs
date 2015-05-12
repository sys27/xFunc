// Copyright 2012-2015 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Programming
{

    /// <summary>
    /// Represents the "if-else" statement.
    /// </summary>
    public class If : DifferentParametersExpression
    {

        internal If() : base(null, -1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="If"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="then">The "then" statement.</param>
        public If(IExpression condition, IExpression then)
            : base(new[] { condition, then }, 2) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="If"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="then">The "then" statement.</param>
        /// <param name="else">The "else" statement.</param>
        public If(IExpression condition, IExpression then, IExpression @else)
            : base(new[] { condition, then, @else }, 3) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="If"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        public If(IExpression[] arguments, int countOfParams)
            : base(arguments, countOfParams) { }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("if");
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">If the "else" statement is null.</exception>
        /// <seealso cref="ExpressionParameters" />
        public override object Calculate(ExpressionParameters parameters)
        {
            if (Condition.Calculate(parameters).AsBool())
                return Then.Calculate(parameters);

            var @else = Else;
            if (@else == null)
                throw new ArgumentNullException("else");

            return @else.Calculate(parameters);
        }

        /// <summary>
        /// Clones this instance of the <see cref="If" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="If" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new If(CloneArguments(), countOfParams);
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
                return 2;
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
                return 3;
            }
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public IExpression Condition
        {
            get
            {
                return m_arguments[0];
            }
        }

        /// <summary>
        /// Gets the "then" statement.
        /// </summary>
        /// <value>
        /// The then.
        /// </value>
        public IExpression Then
        {
            get
            {
                return m_arguments[1];
            }
        }

        /// <summary>
        /// Gets the "else" statement.
        /// </summary>
        /// <value>
        /// The else.
        /// </value>
        public IExpression Else
        {
            get
            {
                return countOfParams == 3 ? m_arguments[2] : null;
            }
        }

    }

}
