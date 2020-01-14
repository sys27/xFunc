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
using xFunc.Maths.Expressions;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths
{

    /// <summary>
    /// Factory of expressions.
    /// </summary>
    public interface IExpressionFactory
    {

        /// <summary>
        /// Creates an expression object from <see cref="OperationToken"/>.
        /// </summary>
        /// <param name="token">The operation token.</param>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        IExpression CreateOperation(OperationToken token, params IExpression[] arguments);

        /// <summary>
        /// Creates an expression object from <see cref="IdToken"/>.
        /// </summary>
        /// <param name="token">The function token.</param>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        IExpression CreateFunction(IdToken token, IExpression[] arguments);

        /// <summary>
        /// Creates an expression object from <see cref="NumberToken"/>.
        /// </summary>
        /// <param name="numberToken">The number token.</param>
        /// <returns>An expression.</returns>
        IExpression CreateNumber(NumberToken numberToken);

        /// <summary>
        /// Creates an expression object from <see cref="KeywordToken"/>.
        /// </summary>
        /// <param name="keywordToken">The keyword token.</param>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        IExpression CreateFromKeyword(KeywordToken keywordToken, params IExpression[] arguments);

        /// <summary>
        /// Creates an expression object from <see cref="ComplexNumberToken"/>.
        /// </summary>
        /// <param name="complexNumberToken">The complex number token.</param>
        /// <returns>An expression.</returns>
        IExpression CreateComplexNumber(ComplexNumberToken complexNumberToken);

        /// <summary>
        /// Creates an expression object from <see cref="IdToken"/>.
        /// </summary>
        /// <param name="variableToken">The variable token.</param>
        /// <returns>An expression.</returns>
        IExpression CreateVariable(IdToken variableToken);

        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        IExpression CreateVector(IExpression[] arguments);

        /// <summary>
        /// Creates a matrix.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        IExpression CreateMatrix(IExpression[] arguments);

    }

}