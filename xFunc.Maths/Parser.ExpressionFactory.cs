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
            return token.Operator switch
            {
                Operators.Plus => new Add(arguments),
                Operators.Minus => new Sub(arguments),
                Operators.Multiplication => new Mul(arguments),
                Operators.Division => new Div(arguments),
                Operators.Exponentiation => new Pow(arguments),
                Operators.Factorial => new Fact(arguments),
                Operators.Modulo => new Mod(arguments),
                Operators.Assign => new Define(arguments),
                Operators.ConditionalAnd => new Expressions.Programming.And(arguments),
                Operators.ConditionalOr => new Expressions.Programming.Or(arguments),
                Operators.Equal => new Equal(arguments),
                Operators.NotEqual => new NotEqual(arguments),
                Operators.LessThan => new LessThan(arguments),
                Operators.LessOrEqual => new LessOrEqual(arguments),
                Operators.GreaterThan => new GreaterThan(arguments),
                Operators.GreaterOrEqual => new GreaterOrEqual(arguments),
                Operators.AddAssign => new AddAssign(arguments),
                Operators.SubAssign => new SubAssign(arguments),
                Operators.MulAssign => new MulAssign(arguments),
                Operators.DivAssign => new DivAssign(arguments),
                Operators.Increment => new Inc(arguments),
                Operators.Decrement => new Dec(arguments),
                Operators.Not => new Not(arguments),
                Operators.And => new Expressions.LogicalAndBitwise.And(arguments),
                Operators.Or => new Expressions.LogicalAndBitwise.Or(arguments),
                Operators.XOr => new XOr(arguments),
                Operators.Implication => new Implication(arguments),
                Operators.Equality => new Equality(arguments),
                Operators.NOr => new NOr(arguments),
                Operators.NAnd => new NAnd(arguments),

                _ => null,
            };
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

                "deriv" => new Derivative(this.differentiator, this.simplifier, arguments),
                "derivative" => new Derivative(this.differentiator, this.simplifier, arguments),
                "simplify" => new Simplify(this.simplifier, arguments),

                "del" => new Del(this.differentiator, this.simplifier, arguments),
                "nabla" => new Del(this.differentiator, this.simplifier, arguments),

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
            return keywordToken.Keyword switch
            {
                Keywords.True => new Bool(true),
                Keywords.False => new Bool(false),

                Keywords.Define => new Define(arguments),
                Keywords.Undefine => new Undefine(arguments),

                Keywords.If => new If(arguments),
                Keywords.For => new For(arguments),
                Keywords.While => new While(arguments),

                Keywords.NAnd => new NAnd(arguments),
                Keywords.NOr => new NOr(arguments),
                Keywords.And => new Expressions.LogicalAndBitwise.And(arguments),
                Keywords.Or => new Expressions.LogicalAndBitwise.Or(arguments),
                Keywords.XOr => new XOr(arguments),
                Keywords.Not => new Not(arguments),
                Keywords.Eq => new Equality(arguments),
                Keywords.Impl => new Implication(arguments),

                Keywords.Mod => new Mod(arguments),

                _ => throw new ArgumentOutOfRangeException(nameof(keywordToken.Keyword)),
            };
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
    }
}