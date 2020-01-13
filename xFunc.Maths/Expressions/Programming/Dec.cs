// Copyright 2012-2020 Dmytro Kyshchenko
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
// limitations under the Licens
using System;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Programming
{

    /// <summary>
    /// Represents the decrement operator.
    /// </summary>
    public class Dec : UnaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Dec"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        public Dec(IExpression argument) : base(argument) { }

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
            var value = m_argument.Execute(parameters);
            if (value is bool)
                throw new NotSupportedException();

            var newValue = Convert.ToDouble(value) - 1;

            if (m_argument is Variable variable)
                parameters.Variables[variable.Name] = newValue;

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
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="Inc" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Dec(m_argument.Clone());
        }

    }

}