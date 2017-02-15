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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// The base class for expression with cached result type. Use this class if expression contains the complicated logic of result type calculation.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Expressions.UnaryExpression" />
    public abstract class CachedUnaryExpression : UnaryExpression
    {

        private bool isChanged = false;
        private ExpressionResultType? resultType;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedUnaryExpression"/> class.
        /// </summary>
        protected CachedUnaryExpression() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedUnaryExpression"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        protected CachedUnaryExpression(IExpression argument) : base(argument) { }

        /// <summary>
        /// Gets the result type.
        /// </summary>
        /// <returns>The result type of current expression.</returns>
        protected abstract ExpressionResultType GetResultType();

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public override IExpression Argument
        {
            get
            {
                return base.Argument;
            }
            set
            {
                base.Argument = value;
                isChanged = true;
            }
        }

        /// <summary>
        /// Gets the type of the result.
        /// Default: Number.
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
                if (this.resultType == null || isChanged)
                {
                    resultType = GetResultType();
                    isChanged = false;
                }

                return resultType.Value;
            }
        }

    }

}
