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
// limitations under the License.

using System;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions.LogicalAndBitwise
{
    /// <summary>
    /// Represents the boolean constant.
    /// </summary>
    public class Bool : IExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bool"/> class.
        /// </summary>
        /// <param name="value">The value of this constant.</param>
        public Bool(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        public object Execute() => Value;

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public object Execute(ExpressionParameters parameters) => Value;

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Bool"/> to <see cref="bool"/>.
        /// </summary>
        /// <param name="boolean">The boolean.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator bool(Bool boolean)
        {
            return boolean?.Value ?? throw new ArgumentNullException(nameof(boolean));
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="bool"/> to <see cref="Bool"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Bool(bool value) => new Bool(value);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var boolean = obj as Bool;

            return Value == boolean?.Value;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => ToString(new CommonFormatter());

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public IExpression Clone() => new Bool(Value);

        /// <summary>
        /// Gets or sets the parent expression.
        /// </summary>
        public IExpression Parent { get; set; }

        /// <summary>
        /// Gets the value of this expression.
        /// </summary>
        /// <value>
        /// The value of this expression.
        /// </value>
#pragma warning disable SA1623
        public bool Value { get; }
#pragma warning restore SA1623
    }
}