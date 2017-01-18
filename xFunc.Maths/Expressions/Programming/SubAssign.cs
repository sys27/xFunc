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
    /// Represents the "-=" operation.
    /// </summary>
    public class SubAssign : BinaryExpression
    {

        [ExcludeFromCodeCoverage]
        internal SubAssign() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubAssign"/> class.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <param name="exp">The expression.</param>
        public SubAssign(IExpression variable, IExpression exp) : base(variable, exp) { }

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
            var var = (Variable)m_left;
            var parameter = var.Execute(parameters);
            if (parameter is bool)
                throw new NotSupportedException();

            var newValue = Convert.ToDouble(parameter) - (double)m_right.Execute(parameters);
            parameters.Variables[var.Name] = newValue;

            return newValue;
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
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="SubAssign" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new SubAssign(m_left.Clone(), m_right.Clone());
        }

        /// <summary>
        /// The left (first) operand.
        /// </summary>
        public override IExpression Left
        {
            get
            {
                return m_left;
            }
            set
            {
                if (!(value is Variable))
                    throw new NotSupportedException();

                base.Left = value;
            }
        }

    }

}
