// Copyright 2012-2013 Dmitry Kischenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Bitwise;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    /// <summary>
    /// Factory of mathematic expressions.
    /// </summary>
    public class ExpressionFactory : IExpressionFactory
    {

        /// <summary>
        /// Creates a expression from specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// The expression.
        /// </returns>
        /// <exception cref="ParserException">This factory don't support specified token.</exception>
        public IExpression Create(IToken token)
        {
            if (token is OperationToken)
                return CreateOperation(token as OperationToken);
            if (token is NumberToken)
                return new Number((token as NumberToken).Number);
            if (token is VariableToken)
                return new Variable((token as VariableToken).Variable);
            if (token is UserFunctionToken)
                return CreateUserFunction(token as UserFunctionToken);
            if (token is FunctionToken)
                return CreateFunction(token as FunctionToken);

            throw new ParserException(Resource.ErrorWhileParsingTree);
        }

        private IExpression CreateOperation(OperationToken token)
        {
            switch (token.Operation)
            {
                case Operations.Addition:
                    return new Add();
                case Operations.Subtraction:
                    return new Sub();
                case Operations.Multiplication:
                    return new Mul();
                case Operations.Division:
                    return new Div();
                case Operations.Exponentiation:
                    return new Pow();
                case Operations.UnaryMinus:
                    return new UnaryMinus();
                case Operations.Factorial:
                    return new Fact();
                case Operations.Assign:
                    return new Define();
                case Operations.Not:
                    return new Not();
                case Operations.And:
                    return new And();
                case Operations.Or:
                    return new Or();
                case Operations.XOr:
                    return new XOr();
                default:
                    throw new ParserException(Resource.ErrorWhileParsingTree);
            }
        }

        private IExpression CreateFunction(FunctionToken token)
        {
            switch (token.Function)
            {
                case Functions.Absolute:
                    return new Abs();
                case Functions.Sine:
                    return new Sin();
                case Functions.Cosine:
                    return new Cos();
                case Functions.Tangent:
                    return new Tan();
                case Functions.Cotangent:
                    return new Cot();
                case Functions.Secant:
                    return new Sec();
                case Functions.Cosecant:
                    return new Csc();
                case Functions.Arcsine:
                    return new Arcsin();
                case Functions.Arccosine:
                    return new Arccos();
                case Functions.Arctangent:
                    return new Arctan();
                case Functions.Arccotangent:
                    return new Arccot();
                case Functions.Arcsecant:
                    return new Arcsec();
                case Functions.Arccosecant:
                    return new Arccsc();
                case Functions.Sqrt:
                    return new Sqrt();
                case Functions.Root:
                    return new Root();
                case Functions.Ln:
                    return new Ln();
                case Functions.Lg:
                    return new Lg();
                case Functions.Log:
                    return new Log();
                case Functions.Sineh:
                    return new Sinh();
                case Functions.Cosineh:
                    return new Cosh();
                case Functions.Tangenth:
                    return new Tanh();
                case Functions.Cotangenth:
                    return new Coth();
                case Functions.Secanth:
                    return new Sech();
                case Functions.Cosecanth:
                    return new Csch();
                case Functions.Arsineh:
                    return new Arsinh();
                case Functions.Arcosineh:
                    return new Arcosh();
                case Functions.Artangenth:
                    return new Artanh();
                case Functions.Arcotangenth:
                    return new Arcoth();
                case Functions.Arsecanth:
                    return new Arsech();
                case Functions.Arcosecanth:
                    return new Arcsch();
                case Functions.Exp:
                    return new Exp();
                case Functions.GCD:
                    return new GCD();
                case Functions.LCM:
                    return new LCM();
                case Functions.Factorial:
                    return new Fact();
                case Functions.Sum:
                    return new Sum();
                case Functions.Product:
                    return new Product();
                case Functions.Derivative:
                    return new Derivative();
                case Functions.Simplify:
                    return new Simplify();
                case Functions.Define:
                    return new Define();
                case Functions.Vector:
                    return new Vector();
                case Functions.Matrix:
                    return new Matrix();
                case Functions.Transpose:
                    return new Transpose();
                case Functions.Determinant:
                    return new Determinant();
                case Functions.Undefine:
                    return new Undefine();
                default:
                    throw new ParserException(Resource.ErrorWhileParsingTree);
            }
        }

        private IExpression CreateUserFunction(UserFunctionToken token)
        {
            return new UserFunction(token.FunctionName);
        }

    }

}
