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
using xFunc.Maths.Tokenization;
using static xFunc.Maths.Tokenization.TokenKind;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths
{
    /// <summary>
    /// The parser for mathematical expressions.
    /// </summary>
    public partial class Parser
    {
        private IExpression CreateFunction(in Token token, IList<IExpression> arguments)
        {
            Debug.Assert(token.IsId(), "Token should be Id.");
            Debug.Assert(!string.IsNullOrWhiteSpace(token.StringValue), "Id is empty.");

            return token.StringValue switch
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
                "tobin" => new ToBin(arguments),
                "tooct" => new ToOct(arguments),
                "tohex" => new ToHex(arguments),

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
            in Token token,
            Variable first,
            IExpression second)
        {
            if (token.Is(AddAssignOperator))
                return new AddAssign(first, second);
            if (token.Is(SubAssignOperator))
                return new SubAssign(first, second);
            if (token.Is(MulAssignOperator))
                return new MulAssign(first, second);
            if (token.Is(DivAssignOperator))
                return new DivAssign(first, second);
            if (token.Is(LeftShiftAssignOperator))
                return new LeftShiftAssign(first, second);

            Debug.Assert(token.Is(RightShiftAssignOperator), "Only '+=', '-=', '*=', '/=', '<<=', '>>=' operators are allowed here.");

            return new RightShiftAssign(first, second);
        }

        private IExpression CreateUnaryAssign(in Token token, Variable first)
        {
            if (token.Is(IncrementOperator))
                return new Inc(first);

            Debug.Assert(token.Is(DecrementOperator), "Only '++' and '--' operators are allowed here.");

            return new Dec(first);
        }

        private IExpression CreateConditionalOperator(
            in Token token,
            IExpression first,
            IExpression second)
        {
            if (token.Is(ConditionalAndOperator))
                return new ConditionalAnd(first, second);

            Debug.Assert(token.Is(ConditionalOrOperator), "Only '&&' and '||' are allowed here.");

            return new ConditionalOr(first, second);
        }

        private IExpression CreateBitwiseOperator(
            in Token token,
            IExpression first,
            IExpression second)
        {
            if (token.Is(AndOperator) || token.Is(AndKeyword))
                return new And(first, second);
            if (token.Is(OrOperator) || token.Is(OrKeyword))
                return new Or(first, second);
            if (token.Is(ImplicationOperator) || token.Is(ImplKeyword))
                return new Implication(first, second);
            if (token.Is(TokenKind.EqualityOperator) || token.Is(EqKeyword))
                return new Equality(first, second);

            if (token.Is(NAndKeyword))
                return new NAnd(first, second);
            if (token.Is(NOrKeyword))
                return new NOr(first, second);

            Debug.Assert(token.Is(XOrKeyword), "Incorrect token type.");

            return new XOr(first, second);
        }

        private IExpression CreateEqualityOperator(
            in Token token,
            IExpression first,
            IExpression second)
        {
            if (token.Is(EqualOperator))
                return new Equal(first, second);
            if (token.Is(NotEqualOperator))
                return new NotEqual(first, second);
            if (token.Is(LessThanOperator))
                return new LessThan(first, second);
            if (token.Is(LessOrEqualOperator))
                return new LessOrEqual(first, second);
            if (token.Is(GreaterThanOperator))
                return new GreaterThan(first, second);

            Debug.Assert(token.Is(GreaterOrEqualOperator), "Incorrect token type.");

            return new GreaterOrEqual(first, second);
        }

        private IExpression CreateShift(in Token token, IExpression first, IExpression second)
        {
            if (token.Is(LeftShiftOperator))
                return new LeftShift(first, second);

            Debug.Assert(token.Is(RightShiftOperator), "Only '<<', '>>' are allowed here.");

            return new RightShift(first, second);
        }

        private IExpression CreateAddSub(in Token token, IExpression first, IExpression second)
        {
            if (token.Is(PlusOperator))
                return new Add(first, second);

            Debug.Assert(token.Is(MinusOperator), "Only '+', '-' are allowed here.");

            return new Sub(first, second);
        }

        private IExpression CreateMulDivMod(in Token token, IExpression first, IExpression second)
        {
            if (token.Is(MultiplicationOperator))
                return new Mul(first, second);
            if (token.Is(DivisionOperator))
                return new Div(first, second);

            Debug.Assert(token.Is(ModuloOperator) || token.Is(ModKeyword), "Only '*', '/', '%', 'mod' are allowed here.");

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

        private IExpression CreateBoolean(in Token token)
        {
            if (token.Is(TrueKeyword))
                return Bool.True;

            Debug.Assert(token.Is(FalseKeyword), "Only True and False keywords are allowed here.");

            return Bool.False;
        }

        // TODO: positional cache
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IExpression CreateNumber(in Token numberToken)
            => new Number(numberToken.NumberValue);

        private IExpression CreateAngleNumber(in Token numberToken, AngleUnit unit)
            => new AngleValue(numberToken.NumberValue, unit).AsExpression();

        private IExpression CreateComplexNumber(
            in Token magnitudeSign,
            in Token magnitude,
            in Token phaseSign,
            in Token phase)
        {
            static int GetSign(Token token)
                => token.Is(MinusOperator) ? -1 : 1;

            var magnitudeNumber = magnitude.NumberValue * GetSign(magnitudeSign);
            var phaseNumber = phase.NumberValue * GetSign(phaseSign);
            var complex = Complex.FromPolarCoordinates(magnitudeNumber, phaseNumber);

            return new ComplexNumber(complex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Variable CreateVariable(in Token token)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(token.StringValue), "Id is null.");

            return new Variable(token.StringValue);
        }

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