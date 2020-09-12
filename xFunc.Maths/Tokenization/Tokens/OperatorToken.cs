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
    internal sealed class OperatorToken : IToken
    {
        private readonly string @operator;

        private OperatorToken(string @operator)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(@operator), "The operator should not be empty.");

            this.@operator = @operator;
        }

        /// <summary>
        /// Gets the '+' token.
        /// </summary>
        public static OperatorToken Plus { get; } = new OperatorToken("+");

        /// <summary>
        /// Gets the '-' token.
        /// </summary>
        public static OperatorToken Minus { get; } = new OperatorToken("-");

        /// <summary>
        /// Gets the '*' token.
        /// </summary>
        public static OperatorToken Multiplication { get; } = new OperatorToken("*");

        /// <summary>
        /// Gets the '/' token.
        /// </summary>
        public static OperatorToken Division { get; } = new OperatorToken("/");

        /// <summary>
        /// Gets the '^' token.
        /// </summary>
        public static OperatorToken Exponentiation { get; } = new OperatorToken("^");

        /// <summary>
        /// Gets the '!' token.
        /// </summary>
        public static OperatorToken Factorial { get; } = new OperatorToken("!");

        /// <summary>
        /// Gets the '%, mod' token.
        /// </summary>
        public static OperatorToken Modulo { get; } = new OperatorToken("%");

        /// <summary>
        /// Gets the '&amp;&amp;' token.
        /// </summary>
        public static OperatorToken ConditionalAnd { get; } = new OperatorToken("&&");

        /// <summary>
        /// Gets the '||' token.
        /// </summary>
        public static OperatorToken ConditionalOr { get; } = new OperatorToken("||");

        /// <summary>
        /// Gets the '==' token.
        /// </summary>
        public static OperatorToken Equal { get; } = new OperatorToken("==");

        /// <summary>
        /// Gets the '!=' token.
        /// </summary>
        public static OperatorToken NotEqual { get; } = new OperatorToken("!=");

        /// <summary>
        /// Gets the '&lt;' token.
        /// </summary>
        public static OperatorToken LessThan { get; } = new OperatorToken("<");

        /// <summary>
        /// Gets the '&lt;=' token.
        /// </summary>
        public static OperatorToken LessOrEqual { get; } = new OperatorToken("<=");

        /// <summary>
        /// Gets the '&gt;' token.
        /// </summary>
        public static OperatorToken GreaterThan { get; } = new OperatorToken(">");

        /// <summary>
        /// Gets the '&gt;=' token.
        /// </summary>
        public static OperatorToken GreaterOrEqual { get; } = new OperatorToken(">=");

        /// <summary>
        /// Gets the 'The increment (++)' token.
        /// </summary>
        public static OperatorToken Increment { get; } = new OperatorToken("++");

        /// <summary>
        /// Gets the 'The decrement (--)' token.
        /// </summary>
        public static OperatorToken Decrement { get; } = new OperatorToken("--");

        /// <summary>
        /// Gets the '+=' token.
        /// </summary>
        public static OperatorToken AddAssign { get; } = new OperatorToken("+=");

        /// <summary>
        /// Gets the '-=' token.
        /// </summary>
        public static OperatorToken SubAssign { get; } = new OperatorToken("-=");

        /// <summary>
        /// Gets the '*=' token.
        /// </summary>
        public static OperatorToken MulAssign { get; } = new OperatorToken("*=");

        /// <summary>
        /// Gets the '/=' token.
        /// </summary>
        public static OperatorToken DivAssign { get; } = new OperatorToken("/=");

        /// <summary>
        /// Gets the ':=' token.
        /// </summary>
        public static OperatorToken Assign { get; } = new OperatorToken(":=");

        /// <summary>
        /// Gets the '~, not' token.
        /// </summary>
        public static OperatorToken Not { get; } = new OperatorToken("~");

        /// <summary>
        /// Gets the '&amp;, and' token.
        /// </summary>
        public static OperatorToken And { get; } = new OperatorToken("&");

        /// <summary>
        /// Gets the '|, or' token.
        /// </summary>
        public static OperatorToken Or { get; } = new OperatorToken("|");

        /// <summary>
        /// Gets the '=>, ->, impl' token.
        /// </summary>
        public static OperatorToken Implication { get; } = new OperatorToken("->");

        /// <summary>
        /// Gets the '&lt;=>, &lt;->, eq' token.
        /// </summary>
        public static OperatorToken Equality { get; } = new OperatorToken("<->");

        /// <summary>
        /// Gets the '&lt;&lt;' token.
        /// </summary>
        public static OperatorToken LeftShift { get; } = new OperatorToken("<<");

        /// <summary>
        /// Gets the '&gt;&gt;' token.
        /// </summary>
        public static OperatorToken RightShift { get; } = new OperatorToken(">>");

        /// <summary>
        /// Gets the '&lt;&lt;=' token.
        /// </summary>
        public static OperatorToken LeftShiftAssign { get; } = new OperatorToken("<<=");

        /// <summary>
        /// Gets the '&gt;&gt;=' token.
        /// </summary>
        public static OperatorToken RightShiftAssign { get; } = new OperatorToken(">>=");
    }
}