// Copyright 2012-2013 Dmitry Kischenko
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

namespace xFunc.Logics.Expressions
{

    /// <summary>
    /// Represents the Not operation.
    /// </summary>
    public class Not : UnaryLogicExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Not"/> class.
        /// </summary>
        internal Not()
            : base(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Not"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The expression.</param>
        public Not(ILogicExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return ToString("!");
        }

        /// <summary>
        /// Calculates this logical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        /// <seealso cref="LogicParameterCollection" />
        public override bool Calculate(LogicParameterCollection parameters)
        {
            return !argument.Calculate(parameters);
        }

    }

}
