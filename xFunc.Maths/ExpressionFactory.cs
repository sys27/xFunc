// Copyright 2012-2018 Dmitry Kischenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths
{

    /// <summary>
    /// Factory of mathematic expressions.
    /// </summary>
    public class ExpressionFactory : IExpressionFactory
    {

        private readonly IDifferentiator differentiator;
        private readonly ISimplifier simplifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionFactory"/> class.
        /// </summary>
        public ExpressionFactory(IDifferentiator differentiator, ISimplifier simplifier)
        {
            this.differentiator = differentiator;
            this.simplifier = simplifier;
        }

        /// <summary>
        /// Creates a expression from specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// The expression.
        /// </returns>
        public virtual IExpression Create(IToken token)
        {
            IExpression result = null;

            if (token is OperationToken)
                result = CreateOperation((OperationToken)token);
            else if (token is NumberToken)
                result = new Number(((NumberToken)token).Number);
            else if (token is BooleanToken)
                result = new Bool(((BooleanToken)token).Value);
            else if (token is ComplexNumberToken)
                result = new ComplexNumber(((ComplexNumberToken)token).Number);
            else if (token is VariableToken)
                result = new Variable(((VariableToken)token).Variable);
            else if (token is UserFunctionToken)
                result = CreateUserFunction((UserFunctionToken)token);
            else if (token is FunctionToken)
                result = CreateFunction((FunctionToken)token);

            return result;
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
                case Operations.Modulo:
                    return new Mod();
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
                    return new Not();
                case Operations.And:
                    return new Expressions.LogicalAndBitwise.And();
                case Operations.Or:
                    return new Expressions.LogicalAndBitwise.Or();
                case Operations.XOr:
                    return new XOr();
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
            IExpression exp;

            switch (token.Function)
            {
                case Functions.Add:
                    exp = new Add(); break;
                case Functions.Sub:
                    exp = new Sub(); break;
                case Functions.Mul:
                    exp = new Mul(); break;
                case Functions.Div:
                    exp = new Div(); break;
                case Functions.Pow:
                    exp = new Pow(); break;
                case Functions.Absolute:
                    exp = new Abs(); break;
                case Functions.Sine:
                    exp = new Sin(); break;
                case Functions.Cosine:
                    exp = new Cos(); break;
                case Functions.Tangent:
                    exp = new Tan(); break;
                case Functions.Cotangent:
                    exp = new Cot(); break;
                case Functions.Secant:
                    exp = new Sec(); break;
                case Functions.Cosecant:
                    exp = new Csc(); break;
                case Functions.Arcsine:
                    exp = new Arcsin(); break;
                case Functions.Arccosine:
                    exp = new Arccos(); break;
                case Functions.Arctangent:
                    exp = new Arctan(); break;
                case Functions.Arccotangent:
                    exp = new Arccot(); break;
                case Functions.Arcsecant:
                    exp = new Arcsec(); break;
                case Functions.Arccosecant:
                    exp = new Arccsc(); break;
                case Functions.Sqrt:
                    exp = new Sqrt(); break;
                case Functions.Root:
                    exp = new Root(); break;
                case Functions.Ln:
                    exp = new Ln(); break;
                case Functions.Lg:
                    exp = new Lg(); break;
                case Functions.Lb:
                    exp = new Lb(); break;
                case Functions.Log:
                    exp = new Log(); break;
                case Functions.Sineh:
                    exp = new Sinh(); break;
                case Functions.Cosineh:
                    exp = new Cosh(); break;
                case Functions.Tangenth:
                    exp = new Tanh(); break;
                case Functions.Cotangenth:
                    exp = new Coth(); break;
                case Functions.Secanth:
                    exp = new Sech(); break;
                case Functions.Cosecanth:
                    exp = new Csch(); break;
                case Functions.Arsineh:
                    exp = new Arsinh(); break;
                case Functions.Arcosineh:
                    exp = new Arcosh(); break;
                case Functions.Artangenth:
                    exp = new Artanh(); break;
                case Functions.Arcotangenth:
                    exp = new Arcoth(); break;
                case Functions.Arsecanth:
                    exp = new Arsech(); break;
                case Functions.Arcosecanth:
                    exp = new Arcsch(); break;
                case Functions.Exp:
                    exp = new Exp(); break;
                case Functions.GCD:
                    exp = new GCD(); break;
                case Functions.LCM:
                    exp = new LCM(); break;
                case Functions.Factorial:
                    exp = new Fact(); break;
                case Functions.Sum:
                    exp = new Sum(); break;
                case Functions.Product:
                    exp = new Product(); break;
                case Functions.Round:
                    exp = new Round(); break;
                case Functions.Floor:
                    exp = new Floor(); break;
                case Functions.Ceil:
                    exp = new Ceil(); break;
                case Functions.Derivative:
                    exp = new Derivative(this.differentiator, this.simplifier); break;
                case Functions.Simplify:
                    exp = new Simplify(this.simplifier); break;
                case Functions.Del:
                    exp = new Del(this.differentiator, this.simplifier); break;
                case Functions.Define:
                    exp = new Define(); break;
                case Functions.Vector:
                    exp = new Vector(); break;
                case Functions.Matrix:
                    exp = new Matrix(); break;
                case Functions.Transpose:
                    exp = new Transpose(); break;
                case Functions.Determinant:
                    exp = new Determinant(); break;
                case Functions.Inverse:
                    exp = new Inverse(); break;
                case Functions.If:
                    exp = new If(); break;
                case Functions.For:
                    exp = new For(); break;
                case Functions.While:
                    exp = new While(); break;
                case Functions.Undefine:
                    exp = new Undefine(); break;
                case Functions.Im:
                    exp = new Im(); break;
                case Functions.Re:
                    exp = new Re(); break;
                case Functions.Phase:
                    exp = new Phase(); break;
                case Functions.Conjugate:
                    exp = new Conjugate(); break;
                case Functions.Reciprocal:
                    exp = new Reciprocal(); break;
                case Functions.Min:
                    exp = new Min(); break;
                case Functions.Max:
                    exp = new Max(); break;
                case Functions.Avg:
                    exp = new Avg(); break;
                case Functions.Count:
                    exp = new Count(); break;
                case Functions.Var:
                    exp = new Var(); break;
                case Functions.Varp:
                    exp = new Varp(); break;
                case Functions.Stdev:
                    exp = new Stdev(); break;
                case Functions.Stdevp:
                    exp = new Stdevp(); break;
                default:
                    exp = null; break;
            }

            if (exp is DifferentParametersExpression diff)
                diff.ParametersCount = token.CountOfParams;

            return exp;
        }

        /// <summary>
        /// Creates an expression object from <see cref="UserFunctionToken"/>.
        /// </summary>
        /// <param name="token">The user-function token.</param>
        /// <returns>An expression.</returns>
        protected virtual IExpression CreateUserFunction(UserFunctionToken token)
        {
            return new UserFunction(token.FunctionName, token.CountOfParams);
        }

    }

}
