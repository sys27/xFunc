// Copyright 2012-2015 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Trigonometric;
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
        public virtual IExpression Create(IToken token)
        {
            if (token is OperationToken)
                return CreateOperation(token as OperationToken);
            if (token is NumberToken)
                return new Number((token as NumberToken).Number);
            if(token is BooleanToken)
                return new Bool((token as BooleanToken).Value);
            if (token is VariableToken)
                return new Variable((token as VariableToken).Variable);
            if (token is UserFunctionToken)
                return CreateUserFunction(token as UserFunctionToken);
            if (token is FunctionToken)
                return CreateFunction(token as FunctionToken);

            return null;
        }

        /// <summary>
        /// Creates an expression object from <see cref="OperationToken"/>.
        /// </summary>
        /// <param name="token">The operation token.</param>
        /// <returns>An expression.</returns>
        protected virtual IExpression CreateOperation(OperationToken token)
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
                case Operations.ConditionalAnd:
                    return new Expressions.Programming.And();
                case Operations.ConditionalOr:
                    return new Expressions.Programming.Or();
                case Operations.Equal:
                    return new Equal();
                case Operations.NotEqual:
                    return new NotEqual();
                case Operations.LessThan:
                    return new LessThan();
                case Operations.LessOrEqual:
                    return new LessOrEqual();
                case Operations.GreaterThan:
                    return new GreaterThan();
                case Operations.GreaterOrEqual:
                    return new GreaterOrEqual();
                case Operations.AddAssign:
                    return new AddAssign();
                case Operations.SubAssign:
                    return new SubAssign();
                case Operations.MulAssign:
                    return new MulAssign();
                case Operations.DivAssign:
                    return new DivAssign();
                case Operations.Increment:
                    return new Inc();
                case Operations.Decrement:
                    return new Dec();
                case Operations.Not:
                    return new Expressions.LogicalAndBitwise.Not();
                case Operations.And:
                    return new Expressions.LogicalAndBitwise.And();
                case Operations.Or:
                    return new Expressions.LogicalAndBitwise.Or();
                case Operations.XOr:
                    return new Expressions.LogicalAndBitwise.XOr();
                case Operations.Implication:
                    return new Implication();
                case Operations.Equality:
                    return new Equality();
                case Operations.NOr:
                    return new NOr();
                case Operations.NAnd:
                    return new NAnd();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Creates an expression object from <see cref="FunctionToken"/>.
        /// </summary>
        /// <param name="token">The function token.</param>
        /// <returns>An expression.</returns>
        protected virtual IExpression CreateFunction(FunctionToken token)
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
                case Functions.Round:
                    return new Round();
                case Functions.Floor:
                    return new Floor();
                case Functions.Ceil:
                    return new Ceil();
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
                case Functions.Inverse:
                    return new Inverse();
                case Functions.If:
                    return new If();
                case Functions.For:
                    return new For();
                case Functions.While:
                    return new While();
                case Functions.Undefine:
                    return new Undefine();
                default: 
                    return null;
            }
        }

        /// <summary>
        /// Creates an expression object from <see cref="UserFunctionToken"/>.
        /// </summary>
        /// <param name="token">The user-function token.</param>
        /// <returns>An expression.</returns>
        protected virtual IExpression CreateUserFunction(UserFunctionToken token)
        {
            return new UserFunction(token.FunctionName);
        }

    }

}
