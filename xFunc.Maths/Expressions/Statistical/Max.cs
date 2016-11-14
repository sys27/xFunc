// Copyright 2012-2016 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Maths.Expressions.Statistical
{

    /// <summary>
    /// Represent the Max function.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Expressions.DifferentParametersExpression" />
    public class Max : DifferentParametersExpression
    {

        internal Max() : base(null, -1) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Max"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        public Max(IExpression[] arguments, int countOfParams) : base(arguments, countOfParams) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(8431, 12107);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return base.ToString("max");
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
            if (countOfParams == 1)
            {
                var result = this.m_arguments[0].Execute(parameters);
                var vector = result as Vector;
                if (vector != null)
                    return vector.Arguments.Max(exp => (double)exp.Execute(parameters));

                return result;
            }
            else
            {
                return this.m_arguments.Max(exp => (double)exp.Execute(parameters));
            }
        }

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Max(CloneArguments(), countOfParams);
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
                var result = new ExpressionResultType[countOfParams];
                if (countOfParams > 0)
                {
                    result[0] = ExpressionResultType.Number | ExpressionResultType.Vector;
                    for (int i = 1; i < result.Length; i++)
                        result[i] = ExpressionResultType.Number;
                }

                return result;
            }
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
                return 1;
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
                return -1;
            }
        }

    }

}
