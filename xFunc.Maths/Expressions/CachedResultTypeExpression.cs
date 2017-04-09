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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// The base class for expression with cached result type. Use this class if expression contains the complicated logic of result type calculation.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Expressions.IExpression" />
    public abstract class CachedResultTypeExpression : IExpression, IChangedExpession
    {

        /// <summary>
        /// Indicates that expression is changed.
        /// </summary>
        private bool m_isChanged;
        /// <summary>
        /// The result type.
        /// </summary>
        private ResultType? m_resultType;

        /// <summary>
        /// Gets the result type.
        /// </summary>
        /// <returns>The result type of current expression.</returns>
        protected abstract ResultType GetResultType();

        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        public abstract object Execute();

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public abstract object Execute(ExpressionParameters parameters);

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public abstract string ToString(IFormatter formatter);

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public abstract TResult Analyze<TResult>(IAnalyzer<TResult> analyzer);

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public abstract IExpression Clone();

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public abstract IExpression Parent { get; set; }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public abstract int MinParameters { get; }

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public abstract int MaxParameters { get; }

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public abstract int ParametersCount { get; }

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
        public virtual ResultType ResultType
        {
            get
            {
                if (this.m_resultType == null || m_isChanged)
                {
                    m_resultType = GetResultType();
                    m_isChanged = false;
                }

                return m_resultType.Value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is changed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is changed; otherwise, <c>false</c>.
        /// </value>
        public bool IsChanged
        {
            get
            {
                return m_isChanged;
            }
            set
            {
                m_isChanged = value;

                if (Parent is IChangedExpession isChangedExpression)
                    isChangedExpression.IsChanged = value;
            }
        }

    }

}
