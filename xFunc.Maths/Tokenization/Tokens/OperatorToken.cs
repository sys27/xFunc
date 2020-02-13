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

using System.Diagnostics;

namespace xFunc.Maths.Tokenization.Tokens
{
    /// <summary>
    /// Represents a operator token.
    /// </summary>
    [DebuggerDisplay("Operator: {@" + nameof(@operator) + "}")]
    public sealed class OperatorToken : IToken
    {
        private readonly string @operator;

        private OperatorToken(string @operator)
        {
            this.@operator = @operator;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => @operator;

        /// <summary>
        /// +
        /// </summary>
        public static OperatorToken Plus { get; } = new OperatorToken("+");

        /// <summary>
        /// -
        /// </summary>
        public static OperatorToken Minus { get; } = new OperatorToken("-");

        /// <summary>
        /// *
        /// </summary>
        public static OperatorToken Multiplication { get; } = new OperatorToken("*");

        /// <summary>
        /// /
        /// </summary>
        public static OperatorToken Division { get; } = new OperatorToken("/");

        /// <summary>
        /// ^
        /// </summary>
        public static OperatorToken Exponentiation { get; } = new OperatorToken("^");

        /// <summary>
        /// !
        /// </summary>
        public static OperatorToken Factorial { get; } = new OperatorToken("!");

        /// <summary>
        /// %, mod
        /// </summary>
        public static OperatorToken Modulo { get; } = new OperatorToken("%");

        /// <summary>
        /// &amp;&amp;
        /// </summary>
        public static OperatorToken ConditionalAnd { get; } = new OperatorToken("&&");

        /// <summary>
        /// ||
        /// </summary>
        public static OperatorToken ConditionalOr { get; } = new OperatorToken("||");

        /// <summary>
        /// ==
        /// </summary>
        public static OperatorToken Equal { get; } = new OperatorToken("==");

        /// <summary>
        /// !=
        /// </summary>
        public static OperatorToken NotEqual { get; } = new OperatorToken("!=");

        /// <summary>
        /// &lt;
        /// </summary>
        public static OperatorToken LessThan { get; } = new OperatorToken("<");

        /// <summary>
        /// &lt;=
        /// </summary>
        public static OperatorToken LessOrEqual { get; } = new OperatorToken("<=");

        /// <summary>
        /// &gt;
        /// </summary>
        public static OperatorToken GreaterThan { get; } = new OperatorToken(">");

        /// <summary>
        /// &gt;=
        /// </summary>
        public static OperatorToken GreaterOrEqual { get; } = new OperatorToken(">=");

        /// <summary>
        /// The increment (++)
        /// </summary>
        public static OperatorToken Increment { get; } = new OperatorToken("++");

        /// <summary>
        /// The decrement (--)
        /// </summary>
        public static OperatorToken Decrement { get; } = new OperatorToken("--");

        /// <summary>
        /// +=
        /// </summary>
        public static OperatorToken AddAssign { get; } = new OperatorToken("+=");

        /// <summary>
        /// -=
        /// </summary>
        public static OperatorToken SubAssign { get; } = new OperatorToken("-=");

        /// <summary>
        /// *=
        /// </summary>
        public static OperatorToken MulAssign { get; } = new OperatorToken("*=");

        /// <summary>
        /// /=
        /// </summary>
        public static OperatorToken DivAssign { get; } = new OperatorToken("/=");

        /// <summary>
        /// :=
        /// </summary>
        public static OperatorToken Assign { get; } = new OperatorToken(":=");

        /// <summary>
        /// ~, not
        /// </summary>
        public static OperatorToken Not { get; } = new OperatorToken("~");

        /// <summary>
        /// &amp;, and
        /// </summary>
        public static OperatorToken And { get; } = new OperatorToken("&");

        /// <summary>
        /// |, or
        /// </summary>
        public static OperatorToken Or { get; } = new OperatorToken("|");

        /// <summary>
        /// =>, ->, impl
        /// </summary>
        public static OperatorToken Implication { get; } = new OperatorToken("->");

        /// <summary>
        /// &lt;=>, &lt;->, eq
        /// </summary>
        public static OperatorToken Equality { get; } = new OperatorToken("<->");
    }
}