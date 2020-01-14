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
using System.Collections.Generic;
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
    /// Factory of expressions.
    /// </summary>
    public class ExpressionFactory : IExpressionFactory
    {

        private readonly IDifferentiator differentiator;
        private readonly ISimplifier simplifier;

        private readonly IDictionary<Operations, Func<IExpression[], IExpression>> operations;
        private readonly IDictionary<string, Func<IExpression[], IExpression>> functions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionFactory"/> class.
        /// </summary>
        public ExpressionFactory(IDifferentiator differentiator, ISimplifier simplifier)
        {
            this.differentiator = differentiator;
            this.simplifier = simplifier;

            operations = new Dictionary<Operations, Func<IExpression[], IExpression>>
            {
                { Operations.Addition, arguments => new Add(GetBinaryArguments(arguments)) },
                { Operations.Subtraction, arguments => new Sub(GetBinaryArguments(arguments)) },
                { Operations.Multiplication, arguments => new Mul(GetBinaryArguments(arguments)) },
                { Operations.Division, arguments => new Div(GetBinaryArguments(arguments)) },
                { Operations.Exponentiation, arguments => new Pow(GetBinaryArguments(arguments)) },
                { Operations.UnaryMinus, arguments => new UnaryMinus(GetSingleArgument(arguments)) },
                { Operations.Factorial, arguments => new Fact(GetSingleArgument(arguments)) },
                { Operations.Modulo, arguments => new Mod(GetBinaryArguments(arguments)) },
                { Operations.Assign, arguments => new Define(GetBinaryArguments(arguments)) },
                { Operations.ConditionalAnd, arguments => new Expressions.Programming.And(GetBinaryArguments(arguments)) },
                { Operations.ConditionalOr, arguments => new Expressions.Programming.Or(GetBinaryArguments(arguments)) },
                { Operations.Equal, arguments => new Equal(GetBinaryArguments(arguments)) },
                { Operations.NotEqual, arguments => new NotEqual(GetBinaryArguments(arguments)) },
                { Operations.LessThan, arguments => new LessThan(GetBinaryArguments(arguments)) },
                { Operations.LessOrEqual, arguments => new LessOrEqual(GetBinaryArguments(arguments)) },
                { Operations.GreaterThan, arguments => new GreaterThan(GetBinaryArguments(arguments)) },
                { Operations.GreaterOrEqual, arguments => new GreaterOrEqual(GetBinaryArguments(arguments)) },
                { Operations.AddAssign, arguments => new AddAssign(GetBinaryArguments(arguments)) },
                { Operations.SubAssign, arguments => new SubAssign(GetBinaryArguments(arguments)) },
                { Operations.MulAssign, arguments => new MulAssign(GetBinaryArguments(arguments)) },
                { Operations.DivAssign, arguments => new DivAssign(GetBinaryArguments(arguments)) },
                { Operations.Increment, arguments => new Inc(GetSingleArgument(arguments)) },
                { Operations.Decrement, arguments => new Dec(GetSingleArgument(arguments)) },
                { Operations.Not, arguments => new Not(GetSingleArgument(arguments)) },
                { Operations.And, arguments => new Expressions.LogicalAndBitwise.And(GetBinaryArguments(arguments)) },
                { Operations.Or, arguments => new Expressions.LogicalAndBitwise.Or(GetBinaryArguments(arguments)) },
                { Operations.XOr, arguments => new XOr(GetBinaryArguments(arguments)) },
                { Operations.Implication, arguments => new Implication(GetBinaryArguments(arguments)) },
                { Operations.Equality, arguments => new Equality(GetBinaryArguments(arguments)) },
                { Operations.NOr, arguments => new NOr(GetBinaryArguments(arguments)) },
                { Operations.NAnd, arguments => new NAnd(GetBinaryArguments(arguments)) },
            };

            functions = new Dictionary<string, Func<IExpression[], IExpression>>
            {
                { "add", arguments => new Add(GetBinaryArguments(arguments)) },
                { "sub", arguments => new Sub(GetBinaryArguments(arguments)) },
                { "mul", arguments => new Mul(GetBinaryArguments(arguments)) },
                { "div", arguments => new Div(GetBinaryArguments(arguments)) },
                { "pow", arguments => new Pow(GetBinaryArguments(arguments)) },
                { "abs", arguments => new Abs(GetSingleArgument(arguments)) },
                { "sin", arguments => new Sin(GetSingleArgument(arguments)) },
                { "cos", arguments => new Cos(GetSingleArgument(arguments)) },
                { "tan", arguments => new Tan(GetSingleArgument(arguments)) },
                { "tg", arguments => new Tan(GetSingleArgument(arguments)) },
                { "cot", arguments => new Cot(GetSingleArgument(arguments)) },
                { "ctg", arguments => new Cot(GetSingleArgument(arguments)) },
                { "sec", arguments => new Sec(GetSingleArgument(arguments)) },
                { "csc", arguments => new Csc(GetSingleArgument(arguments)) },
                { "cosec", arguments => new Csc(GetSingleArgument(arguments)) },
                { "arcsin", arguments => new Arcsin(GetSingleArgument(arguments)) },
                { "arccos", arguments => new Arccos(GetSingleArgument(arguments)) },
                { "arctan", arguments => new Arctan(GetSingleArgument(arguments)) },
                { "arctg", arguments => new Arctan(GetSingleArgument(arguments)) },
                { "arccot", arguments => new Arccot(GetSingleArgument(arguments)) },
                { "arcctg", arguments => new Arccot(GetSingleArgument(arguments)) },
                { "arcsec", arguments => new Arcsec(GetSingleArgument(arguments)) },
                { "arccsc", arguments => new Arccsc(GetSingleArgument(arguments)) },
                { "arccosec", arguments => new Arccsc(GetSingleArgument(arguments)) },
                { "sqrt", arguments => new Sqrt(GetSingleArgument(arguments)) },
                { "root", arguments => new Root(GetBinaryArguments(arguments)) },
                { "ln", arguments => new Ln(GetSingleArgument(arguments)) },
                { "lg", arguments => new Lg(GetSingleArgument(arguments)) },
                { "lb", arguments => new Lb(GetSingleArgument(arguments)) },
                { "log2", arguments => new Lb(GetSingleArgument(arguments)) },
                { "log", arguments => new Log(GetBinaryArguments(arguments)) },
                { "sh", arguments => new Sinh(GetSingleArgument(arguments)) },
                { "sinh", arguments => new Sinh(GetSingleArgument(arguments)) },
                { "ch", arguments => new Cosh(GetSingleArgument(arguments)) },
                { "cosh", arguments => new Cosh(GetSingleArgument(arguments)) },
                { "th", arguments => new Tanh(GetSingleArgument(arguments)) },
                { "tanh", arguments => new Tanh(GetSingleArgument(arguments)) },
                { "cth", arguments => new Coth(GetSingleArgument(arguments)) },
                { "coth", arguments => new Coth(GetSingleArgument(arguments)) },
                { "sech", arguments => new Sech(GetSingleArgument(arguments)) },
                { "csch", arguments => new Csch(GetSingleArgument(arguments)) },
                { "arsh", arguments => new Arsinh(GetSingleArgument(arguments)) },
                { "arsinh", arguments => new Arsinh(GetSingleArgument(arguments)) },
                { "arch", arguments => new Arcosh(GetSingleArgument(arguments)) },
                { "arcosh", arguments => new Arcosh(GetSingleArgument(arguments)) },
                { "arth", arguments => new Artanh(GetSingleArgument(arguments)) },
                { "artanh", arguments => new Artanh(GetSingleArgument(arguments)) },
                { "arcth", arguments => new Arcoth(GetSingleArgument(arguments)) },
                { "arcoth", arguments => new Arcoth(GetSingleArgument(arguments)) },
                { "arsch", arguments => new Arsech(GetSingleArgument(arguments)) },
                { "arsech", arguments => new Arsech(GetSingleArgument(arguments)) },
                { "arcsch", arguments => new Arcsch(GetSingleArgument(arguments)) },
                { "exp", arguments => new Exp(GetSingleArgument(arguments)) },
                { "gcd", arguments => new GCD(arguments) },
                { "gcf", arguments => new GCD(arguments) },
                { "hcf", arguments => new GCD(arguments) },
                { "lcm", arguments => new LCM(arguments) },
                { "scm", arguments => new LCM(arguments) },
                { "fact", arguments => new Fact(GetSingleArgument(arguments)) },
                { "factorial", arguments => new Fact(GetSingleArgument(arguments)) },
                { "sum", arguments => new Sum(arguments) },
                { "product", arguments => new Product(arguments) },
                { "round", arguments => new Round(arguments) },
                { "floor", arguments => new Floor(GetSingleArgument(arguments)) },
                { "ceil", arguments => new Ceil(GetSingleArgument(arguments)) },
                { "deriv", arguments => new Derivative(this.differentiator, this.simplifier, arguments) },
                { "derivative", arguments => new Derivative(this.differentiator, this.simplifier, arguments) },
                { "simplify", arguments => new Simplify(this.simplifier, GetSingleArgument(arguments)) },
                { "del", arguments => new Del(this.differentiator, this.simplifier, GetSingleArgument(arguments)) },
                { "nabla", arguments => new Del(this.differentiator, this.simplifier, GetSingleArgument(arguments)) },
                { "vector", arguments => new Vector(arguments) },
                { "matrix", arguments => new Matrix(arguments) },
                { "transpose", arguments => new Transpose(GetSingleArgument(arguments)) },
                { "det", arguments => new Determinant(GetSingleArgument(arguments)) },
                { "determinant", arguments => new Determinant(GetSingleArgument(arguments)) },
                { "inverse", arguments => new Inverse(GetSingleArgument(arguments)) },
                { "dotproduct", arguments => new DotProduct(GetBinaryArguments(arguments)) },
                { "crossproduct", arguments => new CrossProduct(GetBinaryArguments(arguments)) },
                { "if", arguments => new If(arguments) },
                { "for", arguments => new For(arguments) },
                { "while", arguments => new While(GetBinaryArguments(arguments)) },
                { "im", arguments => new Im(GetSingleArgument(arguments)) },
                { "imaginary", arguments => new Im(GetSingleArgument(arguments)) },
                { "re", arguments => new Re(GetSingleArgument(arguments)) },
                { "real", arguments => new Re(GetSingleArgument(arguments)) },
                { "phase", arguments => new Phase(GetSingleArgument(arguments)) },
                { "conjugate", arguments => new Conjugate(GetSingleArgument(arguments)) },
                { "reciprocal", arguments => new Reciprocal(GetSingleArgument(arguments)) },
                { "min", arguments => new Min(arguments) },
                { "max", arguments => new Max(arguments) },
                { "avg", arguments => new Avg(arguments) },
                { "count", arguments => new Count(arguments) },
                { "var", arguments => new Var(arguments) },
                { "varp", arguments => new Varp(arguments) },
                { "stdev", arguments => new Stdev(arguments) },
                { "stdevp", arguments => new Stdevp(arguments) },
                { "sign", arguments => new Sign(GetSingleArgument(arguments)) },
            };
        }

        /// <summary>
        /// Creates an expression object from <see cref="OperationToken"/>.
        /// </summary>
        /// <param name="token">The operation token.</param>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        public virtual IExpression CreateOperation(OperationToken token, params IExpression[] arguments)
        {
            if (!operations.TryGetValue(token.Operation, out var factory))
                return null;

            return factory(arguments);
        }

        /// <summary>
        /// Creates an expression object from <see cref="IdToken"/>.
        /// </summary>
        /// <param name="token">The function token.</param>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        public virtual IExpression CreateFunction(IdToken token, IExpression[] arguments)
        {
            if (!functions.TryGetValue(token.Id, out var factory))
                return new UserFunction(token.Id, arguments);

            return factory(arguments);
        }

        /// <summary>
        /// Creates an expression object from <see cref="NumberToken"/>.
        /// </summary>
        /// <param name="numberToken">The number token.</param>
        /// <returns>An expression.</returns>
        public virtual IExpression CreateNumber(NumberToken numberToken)
        {
            return new Number(numberToken.Number);
        }

        /// <summary>
        /// Creates an expression object from <see cref="KeywordToken"/>.
        /// </summary>
        /// <param name="keywordToken">The keyword token.</param>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        public virtual IExpression CreateFromKeyword(KeywordToken keywordToken, params IExpression[] arguments)
        {
            return keywordToken.Keyword switch
            {
                Keywords.True => new Bool(true),
                Keywords.False => new Bool(false),
                Keywords.Define => new Define(arguments[0], arguments[1]), // TODO:
                Keywords.Undefine => new Undefine(arguments[0]),
                _ => throw new Exception(),
            };
        }

        /// <summary>
        /// Creates an expression object from <see cref="ComplexNumberToken"/>.
        /// </summary>
        /// <param name="complexNumberToken">The complex number token.</param>
        /// <returns>An expression.</returns>
        public virtual IExpression CreateComplexNumber(ComplexNumberToken complexNumberToken)
        {
            return new ComplexNumber(complexNumberToken.Number);
        }

        /// <summary>
        /// Creates an expression object from <see cref="IdToken"/>.
        /// </summary>
        /// <param name="variableToken">The variable token.</param>
        /// <returns>An expression.</returns>
        public virtual IExpression CreateVariable(IdToken variableToken)
        {
            return new Variable(variableToken.Id);
        }

        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        public virtual IExpression CreateVector(IExpression[] arguments)
        {
            return new Vector(arguments);
        }

        /// <summary>
        /// Creates a matrix.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>An expression.</returns>
        public virtual IExpression CreateMatrix(IExpression[] arguments)
        {
            return new Matrix(arguments);
        }

        private IExpression GetSingleArgument(IExpression[] arguments)
        {
            if (arguments.Length < 1)
                throw new ParseException(Resource.LessParams);

            if (arguments.Length > 1) // TODO:
                throw new ParseException(Resource.MoreParams);

            return arguments[0];
        }

        private (IExpression left, IExpression right) GetBinaryArguments(IExpression[] arguments)
        {
            if (arguments.Length < 2)
                throw new ParseException(Resource.LessParams);

            if (arguments.Length > 2) // TODO:
                throw new ParseException(Resource.MoreParams);

            return (arguments[0], arguments[1]);
        }

    }

}