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
using System.Collections.Generic;
using System.Linq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Resources;
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
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>
        /// The expression.
        /// </returns>
        public virtual IExpression Create(IToken token, IEnumerable<IExpression> arguments)
        {
            IExpression result = null;

            if (token is OperationToken operationToken)
                result = CreateOperation(operationToken, arguments);
            else if (token is NumberToken numberToken)
                result = new Number(numberToken.Number);
            else if (token is BooleanToken booleanToken)
                result = new Bool(booleanToken.Value);
            else if (token is ComplexNumberToken complexNumberToken)
                result = new ComplexNumber(complexNumberToken.Number);
            else if (token is VariableToken variableToken)
                result = new Variable(variableToken.Variable);
            else if (token is UserFunctionToken userFunctionToken)
                result = CreateUserFunction(userFunctionToken, arguments);
            else if (token is FunctionToken functionToken)
                result = CreateFunction(functionToken, arguments);

            if (result == null)
                throw new ParseException(Resource.ErrorWhileParsingTree);

            return result;
        }

        /// <summary>
        /// Creates an expression object from <see cref="OperationToken"/>.
        /// </summary>
        /// <param name="token">The operation token.</param>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        protected virtual IExpression CreateOperation(OperationToken token, IEnumerable<IExpression> arguments)
        {
            var args = arguments.ToList();

            switch (token.Operation)
            {
                case Operations.Addition:
                    return new Add(GetSecondAgrument(args), GetArgument(args));
                case Operations.Subtraction:
                    return new Sub(GetSecondAgrument(args), GetArgument(args));
                case Operations.Multiplication:
                    return new Mul(GetSecondAgrument(args), GetArgument(args));
                case Operations.Division:
                    return new Div(GetSecondAgrument(args), GetArgument(args));
                case Operations.Exponentiation:
                    return new Pow(GetSecondAgrument(args), GetArgument(args));
                case Operations.UnaryMinus:
                    return new UnaryMinus(GetArgument(args));
                case Operations.Factorial:
                    return new Fact(GetArgument(args));
                case Operations.Modulo:
                    return new Mod(GetSecondAgrument(args), GetArgument(args));
                case Operations.Assign:
                    return new Define(GetSecondAgrument(args), GetArgument(args));
                case Operations.ConditionalAnd:
                    return new Expressions.Programming.And(GetSecondAgrument(args), GetArgument(args));
                case Operations.ConditionalOr:
                    return new Expressions.Programming.Or(GetSecondAgrument(args), GetArgument(args));
                case Operations.Equal:
                    return new Equal(GetSecondAgrument(args), GetArgument(args));
                case Operations.NotEqual:
                    return new NotEqual(GetSecondAgrument(args), GetArgument(args));
                case Operations.LessThan:
                    return new LessThan(GetSecondAgrument(args), GetArgument(args));
                case Operations.LessOrEqual:
                    return new LessOrEqual(GetSecondAgrument(args), GetArgument(args));
                case Operations.GreaterThan:
                    return new GreaterThan(GetSecondAgrument(args), GetArgument(args));
                case Operations.GreaterOrEqual:
                    return new GreaterOrEqual(GetSecondAgrument(args), GetArgument(args));
                case Operations.AddAssign:
                    return new AddAssign(GetSecondAgrument(args), GetArgument(args));
                case Operations.SubAssign:
                    return new SubAssign(GetSecondAgrument(args), GetArgument(args));
                case Operations.MulAssign:
                    return new MulAssign(GetSecondAgrument(args), GetArgument(args));
                case Operations.DivAssign:
                    return new DivAssign(GetSecondAgrument(args), GetArgument(args));
                case Operations.Increment:
                    return new Inc(GetArgument(args));
                case Operations.Decrement:
                    return new Dec(GetArgument(args));
                case Operations.Not:
                    return new Not(GetArgument(args));
                case Operations.And:
                    return new Expressions.LogicalAndBitwise.And(GetSecondAgrument(args), GetArgument(args));
                case Operations.Or:
                    return new Expressions.LogicalAndBitwise.Or(GetSecondAgrument(args), GetArgument(args));
                case Operations.XOr:
                    return new XOr(GetSecondAgrument(args), GetArgument(args));
                case Operations.Implication:
                    return new Implication(GetSecondAgrument(args), GetArgument(args));
                case Operations.Equality:
                    return new Equality(GetSecondAgrument(args), GetArgument(args));
                case Operations.NOr:
                    return new NOr(GetSecondAgrument(args), GetArgument(args));
                case Operations.NAnd:
                    return new NAnd(GetSecondAgrument(args), GetArgument(args));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Creates an expression object from <see cref="FunctionToken"/>.
        /// </summary>
        /// <param name="token">The function token.</param>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        protected virtual IExpression CreateFunction(FunctionToken token, IEnumerable<IExpression> arguments)
        {
            var args = arguments.ToList();

            switch (token.Function)
            {
                case Functions.Add:
                    return new Add(GetSecondAgrument(args), GetArgument(args));
                case Functions.Sub:
                    return new Sub(GetSecondAgrument(args), GetArgument(args));
                case Functions.Mul:
                    return new Mul(GetSecondAgrument(args), GetArgument(args));
                case Functions.Div:
                    return new Div(GetSecondAgrument(args), GetArgument(args));
                case Functions.Pow:
                    return new Pow(GetSecondAgrument(args), GetArgument(args));
                case Functions.Absolute:
                    return new Abs(GetArgument(args));
                case Functions.Sine:
                    return new Sin(GetArgument(args));
                case Functions.Cosine:
                    return new Cos(GetArgument(args));
                case Functions.Tangent:
                    return new Tan(GetArgument(args));
                case Functions.Cotangent:
                    return new Cot(GetArgument(args));
                case Functions.Secant:
                    return new Sec(GetArgument(args));
                case Functions.Cosecant:
                    return new Csc(GetArgument(args));
                case Functions.Arcsine:
                    return new Arcsin(GetArgument(args));
                case Functions.Arccosine:
                    return new Arccos(GetArgument(args));
                case Functions.Arctangent:
                    return new Arctan(GetArgument(args));
                case Functions.Arccotangent:
                    return new Arccot(GetArgument(args));
                case Functions.Arcsecant:
                    return new Arcsec(GetArgument(args));
                case Functions.Arccosecant:
                    return new Arccsc(GetArgument(args));
                case Functions.Sqrt:
                    return new Sqrt(GetArgument(args));
                case Functions.Root:
                    return new Root(GetSecondAgrument(args), GetArgument(args));
                case Functions.Ln:
                    return new Ln(GetArgument(args));
                case Functions.Lg:
                    return new Lg(GetArgument(args));
                case Functions.Lb:
                    return new Lb(GetArgument(args));
                case Functions.Log:
                    return new Log(GetArgument(args), GetSecondAgrument(args));
                case Functions.Sineh:
                    return new Sinh(GetArgument(args));
                case Functions.Cosineh:
                    return new Cosh(GetArgument(args));
                case Functions.Tangenth:
                    return new Tanh(GetArgument(args));
                case Functions.Cotangenth:
                    return new Coth(GetArgument(args));
                case Functions.Secanth:
                    return new Sech(GetArgument(args));
                case Functions.Cosecanth:
                    return new Csch(GetArgument(args));
                case Functions.Arsineh:
                    return new Arsinh(GetArgument(args));
                case Functions.Arcosineh:
                    return new Arcosh(GetArgument(args));
                case Functions.Artangenth:
                    return new Artanh(GetArgument(args));
                case Functions.Arcotangenth:
                    return new Arcoth(GetArgument(args));
                case Functions.Arsecanth:
                    return new Arsech(GetArgument(args));
                case Functions.Arcosecanth:
                    return new Arcsch(GetArgument(args));
                case Functions.Exp:
                    return new Exp(GetArgument(args));
                case Functions.GCD:
                    return new GCD(GetArguments(args, token.CountOfParams));
                case Functions.LCM:
                    return new LCM(GetArguments(args, token.CountOfParams));
                case Functions.Factorial:
                    return new Fact(GetArgument(args));
                case Functions.Sum:
                    return new Sum(GetArguments(args, token.CountOfParams));
                case Functions.Product:
                    return new Product(GetArguments(args, token.CountOfParams));
                case Functions.Round:
                    return new Round(GetArguments(args, token.CountOfParams));
                case Functions.Floor:
                    return new Floor(GetArgument(args));
                case Functions.Ceil:
                    return new Ceil(GetArgument(args));
                case Functions.Derivative:
                    return new Derivative(this.differentiator, this.simplifier, GetArgument(args));
                case Functions.Simplify:
                    return new Simplify(this.simplifier, GetArgument(args));
                case Functions.Del:
                    return new Del(this.differentiator, this.simplifier, GetArgument(args));
                case Functions.Define:
                    return new Define(GetSecondAgrument(args), GetArgument(args));
                case Functions.Vector:
                    return new Vector(GetArguments(args, token.CountOfParams));
                case Functions.Matrix:
                    return new Matrix(GetArguments(args, token.CountOfParams));
                case Functions.Transpose:
                    return new Transpose(GetArgument(args));
                case Functions.Determinant:
                    return new Determinant(GetArgument(args));
                case Functions.Inverse:
                    return new Inverse(GetArgument(args));
                case Functions.DotProduct:
                    return new DotProduct(GetSecondAgrument(args), GetArgument(args));
                case Functions.CrossProduct:
                    return new CrossProduct(GetSecondAgrument(args), GetArgument(args));
                case Functions.If:
                    return new If(GetArguments(args, token.CountOfParams));
                case Functions.For:
                    return new For(GetArguments(args, token.CountOfParams));
                case Functions.While:
                    return new While(GetSecondAgrument(args), GetArgument(args));
                case Functions.Undefine:
                    return new Undefine(GetArgument(args));
                case Functions.Im:
                    return new Im(GetArgument(args));
                case Functions.Re:
                    return new Re(GetArgument(args));
                case Functions.Phase:
                    return new Phase(GetArgument(args));
                case Functions.Conjugate:
                    return new Conjugate(GetArgument(args));
                case Functions.Reciprocal:
                    return new Reciprocal(GetArgument(args));
                case Functions.Min:
                    return new Min(GetArguments(args, token.CountOfParams));
                case Functions.Max:
                    return new Max(GetArguments(args, token.CountOfParams));
                case Functions.Avg:
                    return new Avg(GetArguments(args, token.CountOfParams));
                case Functions.Count:
                    return new Count(GetArguments(args, token.CountOfParams));
                case Functions.Var:
                    return new Var(GetArguments(args, token.CountOfParams));
                case Functions.Varp:
                    return new Varp(GetArguments(args, token.CountOfParams));
                case Functions.Stdev:
                    return new Stdev(GetArguments(args, token.CountOfParams));
                case Functions.Stdevp:
                    return new Stdevp(GetArguments(args, token.CountOfParams));
                case Functions.Sign:
                    return new Sign(GetArgument(args));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Creates an expression object from <see cref="UserFunctionToken"/>.
        /// </summary>
        /// <param name="token">The user-function token.</param>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        protected virtual IExpression CreateUserFunction(UserFunctionToken token, IEnumerable<IExpression> arguments)
        {
            var args = arguments.ToList();

            return new UserFunction(token.FunctionName, GetArguments(args, token.CountOfParams));
        }

        private IExpression GetArgument(List<IExpression> arguments)
        {
            if (arguments.Count < 1)
                throw new ParseException(Resource.LessParams);

            return arguments[arguments.Count - 1];
        }

        private IExpression GetSecondAgrument(List<IExpression> arguments)
        {
            if (arguments.Count < 2)
                throw new ParseException(Resource.LessParams);

            return arguments[arguments.Count - 2];
        }

        private IExpression[] GetArguments(List<IExpression> arguments, int count)
        {
            if (arguments.Count < count)
                throw new ParseException(Resource.LessParams);

            return arguments.GetRange(arguments.Count - count, count).ToArray();
        }

    }

}
