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
using System.Text.RegularExpressions;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{

    /// <summary>
    /// The factory which creates operation tokens.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.FactoryBase" />
    public class OperationTokenFactory : FactoryBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationTokenFactory"/> class.
        /// </summary>
        public OperationTokenFactory() : base(new Regex(@"\G([^a-zα-ω0-9(){},°\s]+|nand|nor|and|or|xor|not|eq|impl|mod)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) { }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>
        /// The token.
        /// </returns>
        /// <exception cref="TokenizeException">Throws when <paramref name="match"/> has the not supported symbol.</exception>
        protected override FactoryResult CreateTokenInternal(Match match)
        {
            var result = new FactoryResult();
            var operation = match.Value.ToLower();

            if (operation == "+=")
            {
                result.Token = new OperationToken(Operations.AddAssign);
            }
            else if (operation == "-=" || operation == "−=")
            {
                result.Token = new OperationToken(Operations.SubAssign);
            }
            else if (operation == "*=" || operation == "×=")
            {
                result.Token = new OperationToken(Operations.MulAssign);
            }
            else if (operation == "*" || operation == "×")
            {
                result.Token = new OperationToken(Operations.Multiplication);
            }
            else if (operation == "/")
            {
                result.Token = new OperationToken(Operations.Division);
            }
            else if (operation == "^")
            {
                result.Token = new OperationToken(Operations.Exponentiation);
            }
            else if (operation == "!")
            {
                result.Token = new OperationToken(Operations.Factorial);
            }
            else if (operation == "%" || operation == "mod")
            {
                result.Token = new OperationToken(Operations.Modulo);
            }
            else if (operation == "&&")
            {
                result.Token = new OperationToken(Operations.ConditionalAnd);
            }
            else if (operation == "||")
            {
                result.Token = new OperationToken(Operations.ConditionalOr);
            }
            else if (operation == "==")
            {
                result.Token = new OperationToken(Operations.Equal);
            }
            else if (operation == "!=")
            {
                result.Token = new OperationToken(Operations.NotEqual);
            }
            else if (operation == "<=")
            {
                result.Token = new OperationToken(Operations.LessOrEqual);
            }
            else if (operation == "<")
            {
                result.Token = new OperationToken(Operations.LessThan);
            }
            else if (operation == ">=")
            {
                result.Token = new OperationToken(Operations.GreaterOrEqual);
            }
            else if (operation == ">")
            {
                result.Token = new OperationToken(Operations.GreaterThan);
            }
            else if (operation == "++")
            {
                result.Token = new OperationToken(Operations.Increment);
            }
            else if (operation == "--" || operation == "−−")
            {
                result.Token = new OperationToken(Operations.Decrement);
            }
            else if (operation == "+")
            {
                result.Token = new OperationToken(Operations.Addition);
            }
            else if (operation == "-" || operation == "−")
            {
                result.Token = new OperationToken(Operations.Subtraction);
            }
            else if (operation == "/=")
            {
                result.Token = new OperationToken(Operations.DivAssign);
            }
            else if (operation == ":=")
            {
                result.Token = new OperationToken(Operations.Assign);
            }
            else if (operation == "not" || operation == "~")
            {
                result.Token = new OperationToken(Operations.Not);
            }
            else if (operation == "and" || operation == "&")
            {
                result.Token = new OperationToken(Operations.And);
            }
            else if (operation == "or" || operation == "|")
            {
                result.Token = new OperationToken(Operations.Or);
            }
            else if (operation == "xor")
            {
                result.Token = new OperationToken(Operations.XOr);
            }
            else if (operation == "impl" || operation == "->" || operation == "−>" || operation == "=>")
            {
                result.Token = new OperationToken(Operations.Implication);
            }
            else if (operation == "eq" || operation == "<->" || operation == "<−>" || operation == "<=>")
            {
                result.Token = new OperationToken(Operations.Equality);
            }
            else if (operation == "nor")
            {
                result.Token = new OperationToken(Operations.NOr);
            }
            else if (operation == "nand")
            {
                result.Token = new OperationToken(Operations.NAnd);
            }
            else
            {
                throw new TokenizeException(string.Format(Resource.NotSupportedSymbol, operation));
            }

            result.ProcessedLength = match.Length;
            return result;
        }

    }

}