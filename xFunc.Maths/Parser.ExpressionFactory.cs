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

using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Tokenization.Tokens;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths
{
    /// <summary>
    /// The parser for mathematical expressions.
    /// </summary>
    public partial class Parser
    {
        private IExpression CreateFunction(IdToken token, IList<IExpression> arguments)
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

                "todeg" => new ToDegree(arguments),
                "todegree" => new ToDegree(arguments),
                "torad" => new ToRadian(arguments),
                "toradian" => new ToRadian(arguments),
                "tograd" => new ToGradian(arguments),
                "togradian" => new ToGradian(arguments),
                "tonumber" => new ToNumber(arguments),

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
                "truncate" => new Trunc(arguments),
                "trunc" => new Trunc(arguments),
                "frac" => new Frac(arguments),

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

                var id => new UserFunction(id, arguments),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateUndef(IExpression first)
            => new Undefine(first);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateAssign(IExpression first, IExpression second)
            => new Define(first, second);

        private IExpression CreateBinaryAssign(
            OperatorToken token,
            Variable first,
            IExpression second)
        {
            if (token == OperatorToken.AddAssign)
                return new AddAssign(first, second);
            if (token == OperatorToken.SubAssign)
                return new SubAssign(first, second);
            if (token == OperatorToken.MulAssign)
                return new MulAssign(first, second);

            // if (token == OperatorToken.DivAssign)
            Debug.Assert(token == OperatorToken.DivAssign, "Only '+=', '-=', '*=', '/=' operators are allowed here.");

            return new DivAssign(first, second);
        }

        private IExpression CreateUnaryAssign(OperatorToken token, Variable first)
        {
            if (token == OperatorToken.Increment)
                return new Inc(first);

            // if (token == OperatorToken.Decrement)
            Debug.Assert(token == OperatorToken.Decrement, "Only '++' and '--' operators are allowed here.");

            return new Dec(first);
        }

        private IExpression CreateConditionalOperator(
            OperatorToken token,
            IExpression first,
            IExpression second)
        {
            if (token == OperatorToken.ConditionalAnd)
                return new ConditionalAnd(first, second);

            // if (token == OperatorToken.ConditionalOr)
            Debug.Assert(token == OperatorToken.ConditionalOr, "Only '&&' and '||' are allowed here.");

            return new ConditionalOr(first, second);
        }

        private IExpression CreateBitwiseOperator(
            IToken token,
            IExpression first,
            IExpression second)
        {
            if (token == OperatorToken.And || token == KeywordToken.And)
                return new And(first, second);
            if (token == OperatorToken.Or || token == KeywordToken.Or)
                return new Or(first, second);
            if (token == OperatorToken.Implication || token == KeywordToken.Impl)
                return new Implication(first, second);
            if (token == OperatorToken.Equality || token == KeywordToken.Eq)
                return new Equality(first, second);

            if (token == KeywordToken.NAnd)
                return new NAnd(first, second);
            if (token == KeywordToken.NOr)
                return new NOr(first, second);

            // if (token == KeywordToken.XOr)
            Debug.Assert(token == KeywordToken.XOr, "Incorrect token type.");

            return new XOr(first, second);
        }

        private IExpression CreateEqualityOperator(
            OperatorToken token,
            IExpression first,
            IExpression second)
        {
            if (token == OperatorToken.Equal)
                return new Equal(first, second);
            if (token == OperatorToken.NotEqual)
                return new NotEqual(first, second);
            if (token == OperatorToken.LessThan)
                return new LessThan(first, second);
            if (token == OperatorToken.LessOrEqual)
                return new LessOrEqual(first, second);
            if (token == OperatorToken.GreaterThan)
                return new GreaterThan(first, second);

            // if (token == OperatorToken.GreaterOrEqual)
            Debug.Assert(token == OperatorToken.GreaterOrEqual, "Incorrect token type.");

            return new GreaterOrEqual(first, second);
        }

        private IExpression CreateAddSub(OperatorToken token, IExpression first, IExpression second)
        {
            if (token == OperatorToken.Plus)
                return new Add(first, second);

            // if (token == OperatorToken.Minus)
            Debug.Assert(token == OperatorToken.Minus, "Only '+', '-' are allowed here.");

            return new Sub(first, second);
        }

        private IExpression CreateMulDivMod(IToken token, IExpression first, IExpression second)
        {
            if (token == OperatorToken.Multiplication)
                return new Mul(first, second);
            if (token == OperatorToken.Division)
                return new Div(first, second);

            // if (token == OperatorToken.Modulo || token == KeywordToken.Mod)
            Debug.Assert(token == OperatorToken.Modulo || token == KeywordToken.Mod, "Only '*', '/', '%', 'mod' are allowed here.");

            return new Mod(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateNot(IExpression argument)
            => new Not(argument);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateExponentiation(IExpression first, IExpression second)
            => new Pow(first, second);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateFactorial(IExpression first)
            => new Fact(first);

        private IExpression CreateBoolean(KeywordToken keywordToken)
        {
            if (keywordToken == KeywordToken.True)
                return Bool.True;

            // if (keywordToken == KeywordToken.False)
            Debug.Assert(keywordToken == KeywordToken.False, "Only True and False keywords are allowed here.");

            return Bool.False;
        }

        // TODO: positional cache
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateNumber(NumberToken numberToken)
            => new Number(numberToken.Number);

        private IExpression CreateAngleNumber(NumberToken numberToken, AngleUnit unit)
            => new Angle(numberToken.Number, unit).AsExpression();

        private IExpression CreateComplexNumber(
            OperatorToken magnitudeSign,
            NumberToken magnitude,
            OperatorToken phaseSign,
            NumberToken phase)
        {
            static int GetSign(OperatorToken token)
                => token == OperatorToken.Minus ? -1 : 1;

            var magnitudeNumber = magnitude.Number * GetSign(magnitudeSign);
            var phaseNumber = phase.Number * GetSign(phaseSign);
            var complex = Complex.FromPolarCoordinates(magnitudeNumber, phaseNumber);

            return new ComplexNumber(complex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Variable CreateVariable(IdToken variableToken)
            => new Variable(variableToken.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector CreateVector(IList<IExpression> arguments)
            => new Vector(arguments);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Matrix CreateMatrix(IList<Vector> arguments)
            => new Matrix(arguments);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateUnaryMinus(IExpression operand)
            => new UnaryMinus(operand);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateMultiplication(IExpression left, IExpression right)
            => new Mul(left, right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateIf(IExpression condition, IExpression then)
            => new If(condition, then);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateIf(IExpression condition, IExpression then, IExpression @else)
            => new If(condition, then, @else);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateFor(
            IExpression body,
            IExpression init,
            IExpression cond,
            IExpression iter)
            => new For(body, init, cond, iter);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateWhile(IExpression body, IExpression condition)
            => new While(body, condition);
    }
}