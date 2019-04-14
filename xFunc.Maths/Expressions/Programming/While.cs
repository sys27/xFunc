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
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Programming
{

    /// <summary>
    /// Represents the "while" loop.
    /// </summary>
    public class While : BinaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="While"/> class.
        /// </summary>
        /// <param name="body">The body of while loop.</param>
        /// <param name="condition">The condition of loop.</param>
        public While(IExpression body, IExpression condition) : base(body, condition) { }

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
            while ((bool)m_right.Execute(parameters))
                m_left.Execute(parameters);

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
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="While" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new While(m_left.Clone(), m_right.Clone());
        }

    }

}
