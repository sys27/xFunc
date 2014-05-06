// Copyright 2012-2014 Dmitry Kischenko
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
    /// Represents the "while" loop.
    /// </summary>
    public class While : BinaryExpression
    {

        internal While() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="While"/> class.
        /// </summary>
        /// <param name="body">The body of while loop.</param>
        /// <param name="condition">The condition of loop.</param>
        public While(IExpression body, IExpression condition)
            : base(body, condition) { }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("while({0}, {1})");
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Calculate(ExpressionParameters parameters)
        {
            while (right.Calculate(parameters).AsBool())
                left.Calculate(parameters);

            return double.NaN;
        }

        /// <summary>
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="While" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new While(left.Clone(), right.Clone());
        }
        
    }

}
