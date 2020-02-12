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
using System.Numerics;
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
    public partial class Parser
    {
        private IExpression CreateOperator(OperatorToken token, params IExpression[] arguments)
        {
            if (token == OperatorToken.Plus)
                return new Add(arguments);
            if (token == OperatorToken.Minus)
                return new Sub(arguments);
            if (token == OperatorToken.Multiplication)
                return new Mul(arguments);
            if (token == OperatorToken.Division)
                return new Div(arguments);
            if (token == OperatorToken.Exponentiation)
                return new Pow(arguments);
            if (token == OperatorToken.Factorial)
                return new Fact(arguments);
            if (token == OperatorToken.Modulo)
                return new Mod(arguments);
            if (token == OperatorToken.ConditionalAnd)
                return new Expressions.Programming.And(arguments);
            if (token == OperatorToken.ConditionalOr)
                return new Expressions.Programming.Or(arguments);
            if (token == OperatorToken.Equal)
                return new Equal(arguments);
            if (token == OperatorToken.NotEqual)
                return new NotEqual(arguments);
            if (token == OperatorToken.LessThan)
                return new LessThan(arguments);
            if (token == OperatorToken.LessOrEqual)
                return new LessOrEqual(arguments);
            if (token == OperatorToken.GreaterThan)
                return new GreaterThan(arguments);
            if (token == OperatorToken.GreaterOrEqual)
                return new GreaterOrEqual(arguments);
            if (token == OperatorToken.Assign)
                return new Define(arguments);
            if (token == OperatorToken.AddAssign)
                return new AddAssign(arguments);
            if (token == OperatorToken.SubAssign)
                return new SubAssign(arguments);
            if (token == OperatorToken.MulAssign)
                return new MulAssign(arguments);
            if (token == OperatorToken.DivAssign)
                return new DivAssign(arguments);
            if (token == OperatorToken.Increment)
                return new Inc(arguments);
            if (token == OperatorToken.Decrement)
                return new Dec(arguments);
            if (token == OperatorToken.Not)
                return new Not(arguments);
            if (token == OperatorToken.And)
                return new Expressions.LogicalAndBitwise.And(arguments);
            if (token == OperatorToken.Or)
                return new Expressions.LogicalAndBitwise.Or(arguments);
            if (token == OperatorToken.Implication)
                return new Implication(arguments);
            if (token == OperatorToken.Equality)
                return new Equality(arguments);

            return null;
        }

        private IExpression CreateFunction(IdToken token, IExpression[] arguments)
        {
            return token.Id switch
            {
                "add" => new Add(arguments),
                "sub" => new Sub(arguments),
                "mul" => new Mul(arguments),
                "div" => new Div(arguments),
                "pow" => new Pow(arguments),
                "exp" => new Exp(arguments),
                "abs" => new Abs(arguments),
                "sqrt" => new Sqrt(arguments),
                "root" => new Root(arguments),

                "fact" => new Fact(arguments),
                "factorial" => new Fact(arguments),

                "ln" => new Ln(arguments),
                "lg" => new Lg(arguments),
                "lb" => new Lb(arguments),
                "log2" => new Lb(arguments),
                "log" => new Log(arguments),

                "sin" => new Sin(arguments),
                "cos" => new Cos(arguments),
                "tan" => new Tan(arguments),
                "tg" => new Tan(arguments),
                "cot" => new Cot(arguments),
                "ctg" => new Cot(arguments),
                "sec" => new Sec(arguments),
                "csc" => new Csc(arguments),
                "cosec" => new Csc(arguments),

                "arcsin" => new Arcsin(arguments),
                "arccos" => new Arccos(arguments),
                "arctan" => new Arctan(arguments),
                "arctg" => new Arctan(arguments),
                "arccot" => new Arccot(arguments),
                "arcctg" => new Arccot(arguments),
                "arcsec" => new Arcsec(arguments),
                "arccsc" => new Arccsc(arguments),
                "arccosec" => new Arccsc(arguments),

                "sh" => new Sinh(arguments),
                "sinh" => new Sinh(arguments),
                "ch" => new Cosh(arguments),
                "cosh" => new Cosh(arguments),
                "th" => new Tanh(arguments),
                "tanh" => new Tanh(arguments),
                "cth" => new Coth(arguments),
                "coth" => new Coth(arguments),
                "sech" => new Sech(arguments),
                "csch" => new Csch(arguments),

                "arsh" => new Arsinh(arguments),
                "arsinh" => new Arsinh(arguments),
                "arch" => new Arcosh(arguments),
                "arcosh" => new Arcosh(arguments),
                "arth" => new Artanh(arguments),
                "artanh" => new Artanh(arguments),
                "arcth" => new Arcoth(arguments),
                "arcoth" => new Arcoth(arguments),
                "arsch" => new Arsech(arguments),
                "arsech" => new Arsech(arguments),
                "arcsch" => new Arcsch(arguments),

                "gcd" => new GCD(arguments),
                "gcf" => new GCD(arguments),
                "hcf" => new GCD(arguments),
                "lcm" => new LCM(arguments),
                "scm" => new LCM(arguments),

                "round" => new Round(arguments),
                "floor" => new Floor(arguments),
                "ceil" => new Ceil(arguments),

                "deriv" => new Derivative(differentiator, simplifier, arguments),
                "derivative" => new Derivative(differentiator, simplifier, arguments),
                "simplify" => new Simplify(simplifier, arguments),

                "del" => new Del(differentiator, simplifier, arguments),
                "nabla" => new Del(differentiator, simplifier, arguments),

                "transpose" => new Transpose(arguments),
                "det" => new Determinant(arguments),
                "determinant" => new Determinant(arguments),
                "inverse" => new Inverse(arguments),
                "dotproduct" => new DotProduct(arguments),
                "crossproduct" => new CrossProduct(arguments),

                "im" => new Im(arguments),
                "imaginary" => new Im(arguments),
                "re" => new Re(arguments),
                "real" => new Re(arguments),
                "phase" => new Phase(arguments),
                "conjugate" => new Conjugate(arguments),
                "reciprocal" => new Reciprocal(arguments),

                "sum" => new Sum(arguments),
                "product" => new Product(arguments),
                "min" => new Min(arguments),
                "max" => new Max(arguments),
                "avg" => new Avg(arguments),
                "count" => new Count(arguments),
                "var" => new Var(arguments),
                "varp" => new Varp(arguments),
                "stdev" => new Stdev(arguments),
                "stdevp" => new Stdevp(arguments),

                "sign" => new Sign(arguments),

                var id => new UserFunction(id, arguments)
            };
        }

        private IExpression CreateFromKeyword(KeywordToken keywordToken, params IExpression[] arguments)
        {
            if (keywordToken == KeywordToken.True)
                return new Bool(true);
            if (keywordToken == KeywordToken.False)
                return new Bool(false);
            if (keywordToken == KeywordToken.Define)
                return new Define(arguments);
            if (keywordToken == KeywordToken.Undefine)
                return new Undefine(arguments);
            if (keywordToken == KeywordToken.If)
                return new If(arguments);
            if (keywordToken == KeywordToken.For)
                return new For(arguments);
            if (keywordToken == KeywordToken.While)
                return new While(arguments);
            if (keywordToken == KeywordToken.NAnd)
                return new NAnd(arguments);
            if (keywordToken == KeywordToken.NOr)
                return new NOr(arguments);
            if (keywordToken == KeywordToken.And)
                return new Expressions.LogicalAndBitwise.And(arguments);
            if (keywordToken == KeywordToken.Or)
                return new Expressions.LogicalAndBitwise.Or(arguments);
            if (keywordToken == KeywordToken.XOr)
                return new XOr(arguments);
            if (keywordToken == KeywordToken.Not)
                return new Not(arguments);
            if (keywordToken == KeywordToken.Eq)
                return new Equality(arguments);
            if (keywordToken == KeywordToken.Impl)
                return new Implication(arguments);
            if (keywordToken == KeywordToken.Mod)
                return new Mod(arguments);

            throw new ArgumentOutOfRangeException(nameof(keywordToken));
        }

        private IExpression CreateNumber(NumberToken numberToken) =>
            new Number(numberToken.Number);

        private IExpression CreateComplexNumber(NumberToken magnitude, NumberToken phase) =>
            new ComplexNumber(Complex.FromPolarCoordinates(magnitude.Number, phase.Number));

        private IExpression CreateVariable(IdToken variableToken) =>
            new Variable(variableToken.Id);

        private IExpression CreateVector(IExpression[] arguments) =>
            new Vector(arguments);

        private IExpression CreateMatrix(IExpression[] arguments) =>
            new Matrix(arguments);

        private IExpression CreateUnaryMinux(IExpression operand) =>
            new UnaryMinus(operand);

        private IExpression CreateMultiplication(params IExpression[] arguments) =>
            new Mul(arguments);
    }
}