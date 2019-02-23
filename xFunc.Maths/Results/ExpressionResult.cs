// Copyright 2012-2019 Dmitry Kischenko
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
using xFunc.Maths.Expressions;

namespace xFunc.Maths.Results
{

    /// <summary>
    /// Represents the result in the expression form.
    /// </summary>
    public class ExpressionResult : IResult
    {

        private readonly IExpression exp;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionResult"/> class.
        /// </summary>
        /// <param name="exp">The expression.</param>
        public ExpressionResult(IExpression exp)
        {
            this.exp = exp;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return exp.ToString();
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public IExpression Result => exp;

        object IResult.Result => exp;

    }

}
