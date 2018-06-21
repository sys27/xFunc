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
using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths.Analyzers
{

    /// <summary>
    /// The simplifier of expressions.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Analyzers.IAnalyzer{TResult}" />
    /// <seealso cref="xFunc.Maths.Analyzers.ISimplifier" />
    public class Simplifier : ISimplifier
    {

        private readonly Number zero = 0;
        private readonly Number one = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Simplifier"/> class.
        /// </summary>
        public Simplifier() { }

        private IExpression AnalyzeUnary(UnaryExpression exp)
        {
            exp.Argument = exp.Argument.Analyze(this);

            return exp;
        }

        private IExpression AnalyzeBinary(BinaryExpression exp)
        {
            exp.Left = exp.Left.Analyze(this);
            exp.Right = exp.Right.Analyze(this);

            return exp;
        }

        private IExpression AnalyzeTrigonometric<T>(UnaryExpression exp) where T : UnaryExpression
        {
            exp.Argument = exp.Argument.Analyze(this);
            if (exp.Argument is T trigonometric)
                return trigonometric.Argument;

            return exp;
        }

        private IExpression AnalyzeDiffParams(DifferentParametersExpression exp)
        {
            for (int i = 0; i < exp.Arguments.Length; i++)
                if (exp.Arguments[i] != null)
                    exp.Arguments[i] = exp.Arguments[i].Analyze(this);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression. This method should be only used for expessions which are not supported by xFunc (custom expression create by extendening library).
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(IExpression exp)
        {
            return null;
        }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Abs exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Add exp)
        {
            exp = AnalyzeBinary(exp) as Add;

            // plus zero
            if (exp.Left.Equals(zero))
                return exp.Right;
            if (exp.Right.Equals(zero))
                return exp.Left;

            if (exp.Left is Number && exp.Right is Number)
                return new Number((double)exp.Execute());

            // x + x
            if (exp.Left is Variable leftVar && exp.Right is Variable rightVar && leftVar.Name == rightVar.Name)
                return new Mul(new Number(2), leftVar);

            if (exp.Left is UnaryMinus)
            {
                var temp = exp.Left;
                exp.Left = exp.Right;
                exp.Right = temp;
            }

            if (exp.Right is UnaryMinus unMinus)
                return Analyze(new Sub(exp.Left, unMinus.Argument));

            // 2 + (2 + x)
            // 2 + (x + 2)
            // (2 + x) + 2
            // (x + 2) + 2
            var bracketAdd = exp.Left as Add;
            var firstNumber = exp.Right as Number;
            if (bracketAdd == null)
            {
                bracketAdd = exp.Right as Add;
                firstNumber = exp.Left as Number;
            }
            if (bracketAdd != null && firstNumber != null)
            {
                if (bracketAdd.Left is Number secondNumberLeft)
                    return Analyze(new Add(bracketAdd.Right, new Number(firstNumber.Value + secondNumberLeft.Value)));

                if (bracketAdd.Right is Number secondNumberRight)
                    return Analyze(new Add(bracketAdd.Left, new Number(firstNumber.Value + secondNumberRight.Value)));
            }

            // 2 + (2 - x)
            // 2 + (x - 2)
            // (2 - x) + 2
            // (x - 2) + 2
            var bracketSub = exp.Left as Sub;
            firstNumber = exp.Right as Number;
            if (bracketSub == null)
            {
                bracketSub = exp.Right as Sub;
                firstNumber = exp.Left as Number;
            }
            if (bracketSub != null && firstNumber != null)
            {
                if (bracketSub.Left is Number secondNumberLeft)
                    return Analyze(new Sub(new Number(firstNumber.Value + secondNumberLeft.Value), bracketSub.Right));

                if (bracketSub.Right is Number secondNumberRight)
                    return Analyze(new Add(new Number(firstNumber.Value - secondNumberRight.Value), bracketSub.Left));
            }

            // x + 2x
            // 2x + 3x
            Number leftMultiplier = null;
            Number rightMultiplier = null;
            Variable varMultiplier = null;
            if (exp.Left is Variable leftVariable && exp.Right is Mul rightMul1)
            {
                leftMultiplier = 1;
                varMultiplier = leftVariable;

                if (rightMul1.Left is Number mulLeftNumber && rightMul1.Right.Equals(varMultiplier))
                    rightMultiplier = mulLeftNumber;
                else if (rightMul1.Right is Number mulRightNumber && rightMul1.Left.Equals(varMultiplier))
                    rightMultiplier = mulRightNumber;
            }
            else if (exp.Right is Variable rightVariable && exp.Left is Mul leftMul1)
            {
                rightMultiplier = 1;
                varMultiplier = rightVariable;

                if (leftMul1.Left is Number mulLeftNumber && leftMul1.Right.Equals(varMultiplier))
                    leftMultiplier = mulLeftNumber;
                else if (leftMul1.Right is Number mulRightNumber && leftMul1.Left.Equals(varMultiplier))
                    leftMultiplier = mulRightNumber;
            }
            else if (exp.Left is Mul leftMul2 && exp.Right is Mul rightMul3)
            {
                varMultiplier = leftMul2.Left as Variable;
                if (varMultiplier == null)
                    varMultiplier = leftMul2.Right as Variable;

                if (varMultiplier != null)
                {
                    if (leftMul2.Left is Number mulLeftNumber1 && leftMul2.Right.Equals(varMultiplier))
                        leftMultiplier = mulLeftNumber1;
                    else if (leftMul2.Right is Number mulRightNumber1 && leftMul2.Left.Equals(varMultiplier))
                        leftMultiplier = mulRightNumber1;

                    if (rightMul3.Left is Number mulLeftNumber2 && rightMul3.Right.Equals(varMultiplier))
                        rightMultiplier = mulLeftNumber2;
                    else if (rightMul3.Right is Number mulRightNumber2 && rightMul3.Left.Equals(varMultiplier))
                        rightMultiplier = mulRightNumber2;
                }
            }

            if (leftMultiplier != null && rightMultiplier != null)
            {
                var multiplier = leftMultiplier.Value + rightMultiplier.Value;

                if (multiplier == 1)
                    return varMultiplier;
                if (multiplier == -1)
                    return new UnaryMinus(varMultiplier);

                return new Mul(new Number(multiplier), varMultiplier);
            }

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Ceil exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Define exp)
        {
            exp.Value = exp.Value.Analyze(this);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Del exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Derivative exp)
        {
            exp.Expression = exp.Expression.Analyze(this);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Div exp)
        {
            exp = AnalyzeBinary(exp) as Div;

            // 0 / x
            if (exp.Left.Equals(zero) && !exp.Right.Equals(zero))
                return zero;
            // x / 0
            if (exp.Right.Equals(zero) && !exp.Left.Equals(zero))
                throw new DivideByZeroException();
            // x / 1
            if (exp.Right.Equals(one))
                return exp.Left;

            if (exp.Left is Number && exp.Right is Number)
                return new Number((double)exp.Execute());

            if (exp.Left is Variable && exp.Right is Variable)
                return one;

            // (2 * x) / 2
            // (x * 2) / 2
            if (exp.Left is Mul bracketMulLeft && exp.Right is Number firstNumberRight)
            {
                if (bracketMulLeft.Left is Number secondNumberLeft)
                    return Analyze(new Div(bracketMulLeft.Right, new Number((double)firstNumberRight.Execute() / (double)secondNumberLeft.Execute())));

                if (bracketMulLeft.Right is Number secondNumberRight)
                    return Analyze(new Div(bracketMulLeft.Left, new Number((double)firstNumberRight.Execute() / (double)secondNumberRight.Execute())));
            }
            // 2 / (2 * x)
            // 2 / (x * 2)
            else if (exp.Right is Mul bracketMulRight && exp.Left is Number firstNumberLeft)
            {
                if (bracketMulRight.Left is Number secondNumberLeft)
                    return Analyze(new Div(new Number((double)firstNumberLeft.Execute() / (double)secondNumberLeft.Execute()), bracketMulRight.Right));

                if (bracketMulRight.Right is Number secondNumberRight)
                    return Analyze(new Div(new Number((double)firstNumberLeft.Execute() / (double)secondNumberRight.Execute()), bracketMulRight.Left));
            }
            // (2 / x) / 2
            // (x / 2) / 2
            else if (exp.Left is Div bracketDivLeft && exp.Right is Number firstNumberDivRight)
            {
                if (bracketDivLeft.Left is Number secondNumberLeft)
                    return Analyze(new Div(new Number((double)firstNumberDivRight.Execute() / (double)secondNumberLeft.Execute()), bracketDivLeft.Right));

                if (bracketDivLeft.Right is Number secondNumberRight)
                    return Analyze(new Div(bracketDivLeft.Left, new Number((double)firstNumberDivRight.Execute() * (double)secondNumberRight.Execute())));
            }
            // 2 / (2 / x)
            // 2 / (x / 2)
            else if (exp.Right is Div bracketDivRight && exp.Left is Number firstNumberDivLeft)
            {
                if (bracketDivRight.Left is Number secondNumberLeft)
                    return Analyze(new Mul(new Number((double)firstNumberDivLeft.Execute() / (double)secondNumberLeft.Execute()), bracketDivRight.Right));

                if (bracketDivRight.Right is Number secondNumberRight)
                    return Analyze(new Div(new Number((double)firstNumberDivLeft.Execute() * (double)secondNumberRight.Execute()), bracketDivRight.Left));
            }

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Exp exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Fact exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Floor exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(GCD exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Lb exp)
        {
            exp = AnalyzeUnary(exp) as Lb;

            if (exp.Argument.Equals(new Number(2)))
                return one;

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(LCM exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Lg exp)
        {
            exp = AnalyzeUnary(exp) as Lg;

            // lg(10)
            if (exp.Argument.Equals(new Number(10)))
                return one;

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Ln exp)
        {
            exp = AnalyzeUnary(exp) as Ln;

            // ln(e)
            if (exp.Argument.Equals(new Variable("e")))
                return one;

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Log exp)
        {
            exp = AnalyzeBinary(exp) as Log;

            // log(4x, 4x)
            if (exp.Left.Equals(exp.Right))
                return one;

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Mod exp)
        {
            return AnalyzeBinary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Mul exp)
        {
            exp = AnalyzeBinary(exp) as Mul;

            // mul by zero
            if (exp.Left.Equals(zero) || exp.Right.Equals(zero))
                return zero;

            // mul by 1
            if (exp.Left.Equals(one))
                return exp.Right;
            if (exp.Right.Equals(one))
                return exp.Left;

            if (exp.Left is Number && exp.Right is Number)
                return new Number((double)exp.Execute());

            if (exp.Left is Variable && exp.Right is Variable)
                return new Pow(exp.Left, new Number(2));

            // 2 * (2 * x)
            // 2 * (x * 2)
            // (2 * x) * 2
            // (x * 2) * 2
            var bracketMul = exp.Left as Mul;
            var firstNumber = exp.Right as Number;
            if (bracketMul == null)
            {
                bracketMul = exp.Right as Mul;
                firstNumber = exp.Left as Number;
            }
            if (bracketMul != null && firstNumber != null)
            {
                if (bracketMul.Left is Number secondNumberLeft)
                    return Analyze(new Mul(new Number(firstNumber.Value * secondNumberLeft.Value), bracketMul.Right));

                if (bracketMul.Right is Number secondNumberRight)
                    return Analyze(new Mul(new Number(firstNumber.Value * secondNumberRight.Value), bracketMul.Left));
            }

            // 2 * (2 / x)
            // 2 * (x / 2)
            // (2 / x) * 2
            // (x / 2) * 2
            var bracketDiv = exp.Left as Div;
            firstNumber = exp.Right as Number;
            if (bracketDiv == null)
            {
                bracketDiv = exp.Right as Div;
                firstNumber = exp.Left as Number;
            }
            if (bracketDiv != null && firstNumber != null)
            {
                if (bracketDiv.Left is Number secondNumberLeft)
                    return Analyze(new Div(new Number(firstNumber.Value * secondNumberLeft.Value), bracketDiv.Right));

                if (bracketDiv.Right is Number secondNumberRight)
                    return Analyze(new Mul(new Number(firstNumber.Value / secondNumberRight.Value), bracketDiv.Left));
            }

            // x * 2x
            // 2x * 3x
            Number leftMultiplier = null;
            Number rightMultiplier = null;
            Variable varMultiplier = null;
            if (exp.Left is Variable leftVariable && exp.Right is Mul rightMul1)
            {
                leftMultiplier = 1;
                varMultiplier = leftVariable;

                if (rightMul1.Left is Number mulLeftNumber && rightMul1.Right.Equals(varMultiplier))
                    rightMultiplier = mulLeftNumber;
                else if (rightMul1.Right is Number mulRightNumber && rightMul1.Left.Equals(varMultiplier))
                    rightMultiplier = mulRightNumber;
            }
            else if (exp.Right is Variable rightVariable && exp.Left is Mul leftMul1)
            {
                rightMultiplier = 1;
                varMultiplier = rightVariable;

                if (leftMul1.Left is Number mulLeftNumber && leftMul1.Right.Equals(varMultiplier))
                    leftMultiplier = mulLeftNumber;
                else if (leftMul1.Right is Number mulRightNumber && leftMul1.Left.Equals(varMultiplier))
                    leftMultiplier = mulRightNumber;
            }
            else if (exp.Left is Mul leftMul2 && exp.Right is Mul rightMul2)
            {
                varMultiplier = leftMul2.Left as Variable;
                if (varMultiplier == null)
                    varMultiplier = leftMul2.Right as Variable;

                if (varMultiplier != null)
                {
                    if (leftMul2.Left is Number mulLeftNumber1 && leftMul2.Right.Equals(varMultiplier))
                        leftMultiplier = mulLeftNumber1;
                    else if (leftMul2.Right is Number mulRightNumber1 && leftMul2.Left.Equals(varMultiplier))
                        leftMultiplier = mulRightNumber1;

                    if (rightMul2.Left is Number mulLeftNumber2 && rightMul2.Right.Equals(varMultiplier))
                        rightMultiplier = mulLeftNumber2;
                    else if (rightMul2.Right is Number mulRightNumber2 && rightMul2.Left.Equals(varMultiplier))
                        rightMultiplier = mulRightNumber2;
                }
            }

            if (leftMultiplier != null && rightMultiplier != null)
            {
                var multiplier = leftMultiplier.Value * rightMultiplier.Value;

                if (multiplier == 1)
                    return new Pow(varMultiplier, new Number(2));
                if (multiplier == -1)
                    return new UnaryMinus(new Pow(varMultiplier, new Number(2)));

                return new Mul(new Number(multiplier), new Pow(varMultiplier, new Number(2)));
            }

            if (exp.Right is UnaryMinus rightNegative)
                return new UnaryMinus(new Mul(rightNegative.Argument, exp.Left));

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Number exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Pow exp)
        {
            exp = AnalyzeBinary(exp) as Pow;

            // x^0
            if (exp.Right.Equals(zero))
                return one;
            // x^1
            if (exp.Right.Equals(one))
                return exp.Left;

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Root exp)
        {
            exp = AnalyzeBinary(exp) as Root;

            // root(x, 1)
            if (exp.Right.Equals(one))
                return exp.Left;

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Round exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Simplify exp)
        {
            return exp.Argument.Analyze(this);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Sqrt exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Sub exp)
        {
            exp = AnalyzeBinary(exp) as Sub;

            // sub zero
            if (exp.Left.Equals(zero))
                return Analyze(new UnaryMinus(exp.Right));
            if (exp.Right.Equals(zero))
                return exp.Left;

            if (exp.Left is Number && exp.Right is Number)
                return new Number((double)exp.Execute());

            if (exp.Left is Variable && exp.Right is Variable)
                return zero;

            if (exp.Right is UnaryMinus unMinus)
                return new Add(exp.Left, unMinus.Argument);

            // (2 + x) - 2
            // (x + 2) - 2
            if (exp.Left is Add bracketAddLeft1 && exp.Right is Number firstNumberRight1)
            {
                if (bracketAddLeft1.Left is Number secondNumberLeft)
                    return Analyze(new Add(bracketAddLeft1.Right, new Number((double)firstNumberRight1.Execute() - (double)secondNumberLeft.Execute())));

                if (bracketAddLeft1.Right is Number secondNumberRight)
                    return Analyze(new Add(bracketAddLeft1.Left, new Number((double)firstNumberRight1.Execute() - (double)secondNumberRight.Execute())));
            }
            // 2 - (2 + x)
            // 2 - (x + 2)
            else if (exp.Right is Add bracketAddRight1 && exp.Left is Number firstNumberLeft1)
            {
                if (bracketAddRight1.Left is Number secondNumberLeft)
                    return Analyze(new Sub(new Number((double)firstNumberLeft1.Execute() - (double)secondNumberLeft.Execute()), bracketAddRight1.Right));

                if (bracketAddRight1.Right is Number secondNumberRight)
                    return Analyze(new Sub(new Number((double)firstNumberLeft1.Execute() - (double)secondNumberRight.Execute()), bracketAddRight1.Left));
            }
            // (2 - x) - 2
            // (x - 2) - 2
            else if (exp.Left is Sub bracketSubLeft && exp.Right is Number firstNumberRight2)
            {
                if (bracketSubLeft.Left is Number secondNumberLeft)
                    return Analyze(new Sub(new Number((double)firstNumberRight2.Execute() - (double)secondNumberLeft.Execute()), bracketSubLeft.Right));

                if (bracketSubLeft.Right is Number secondNumberRight)
                    return Analyze(new Sub(bracketSubLeft.Left, new Number((double)firstNumberRight2.Execute() + (double)secondNumberRight.Execute())));
            }
            // 2 - (2 - x)
            // 2 - (x - 2)
            else if (exp.Right is Sub bracketSubRight && exp.Left is Number firstNumberLeft2)
            {
                if (bracketSubRight.Left is Number secondNumberLeft)
                    return Analyze(new Add(new Number((double)firstNumberLeft2.Execute() - (double)secondNumberLeft.Execute()), bracketSubRight.Right));

                if (bracketSubRight.Right is Number secondNumberRight)
                    return Analyze(new Sub(new Number((double)firstNumberLeft2.Execute() + (double)secondNumberRight.Execute()), bracketSubRight.Left));
            }

            // 2x - x
            Number leftMultiplier = null;
            Number rightMultiplier = null;
            Variable varMultiplier = null;
            if (exp.Left is Variable leftVariable && exp.Right is Mul rightMul1)
            {
                leftMultiplier = 1;
                varMultiplier = leftVariable;

                if (rightMul1.Left is Number mulLeft && rightMul1.Right.Equals(varMultiplier))
                    rightMultiplier = mulLeft;
                else if (rightMul1.Right is Number mulRight && rightMul1.Left.Equals(varMultiplier))
                    rightMultiplier = mulRight;
            }
            else if (exp.Right is Variable rightVariable && exp.Left is Mul leftMul1)
            {
                rightMultiplier = 1;
                varMultiplier = rightVariable;

                if (leftMul1.Left is Number mulLeft && leftMul1.Right.Equals(varMultiplier))
                    leftMultiplier = mulLeft;
                else if (leftMul1.Right is Number mulRight && leftMul1.Left.Equals(varMultiplier))
                    leftMultiplier = mulRight;
            }
            else if (exp.Left is Mul leftMul2 && exp.Right is Mul rightMul2)
            {
                varMultiplier = leftMul2.Left as Variable;
                if (varMultiplier == null)
                    varMultiplier = leftMul2.Right as Variable;

                if (varMultiplier != null)
                {
                    if (leftMul2.Left is Number mulLeft1 && leftMul2.Right.Equals(varMultiplier))
                        leftMultiplier = mulLeft1;
                    else if (leftMul2.Right is Number mulRight1 && leftMul2.Left.Equals(varMultiplier))
                        leftMultiplier = mulRight1;

                    if (rightMul2.Left is Number mulLeft2 && rightMul2.Right.Equals(varMultiplier))
                        rightMultiplier = mulLeft2;
                    else if (rightMul2.Right is Number mulRight2 && rightMul2.Left.Equals(varMultiplier))
                        rightMultiplier = mulRight2;
                }
            }
            if (leftMultiplier != null && rightMultiplier != null)
            {
                var multiplier = leftMultiplier.Value - rightMultiplier.Value;

                if (multiplier == -1)
                    return new UnaryMinus(varMultiplier);
                if (multiplier == 1)
                    return varMultiplier;

                return new Mul(new Number(multiplier), varMultiplier);
            }

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(UnaryMinus exp)
        {
            exp = AnalyzeUnary(exp) as UnaryMinus;

            // -(-x)
            if (exp.Argument is UnaryMinus unMinus)
                return unMinus.Argument;
            // -1
            if (exp.Argument is Number number)
            {
                number.Value = -number.Value;

                return number;
            }

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Undefine exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(UserFunction exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Variable exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(DelegateExpression exp)
        {
            return exp;
        }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Vector exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Matrix exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Determinant exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Inverse exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Transpose exp)
        {
            return AnalyzeUnary(exp);
        }

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(ComplexNumber exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Conjugate exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Im exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Phase exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Re exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Reciprocal exp)
        {
            return AnalyzeUnary(exp);
        }

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arccos exp)
        {
            return AnalyzeTrigonometric<Cos>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arccot exp)
        {
            return AnalyzeTrigonometric<Cot>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arccsc exp)
        {
            return AnalyzeTrigonometric<Csc>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arcsec exp)
        {
            return AnalyzeTrigonometric<Sec>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arcsin exp)
        {
            return AnalyzeTrigonometric<Sin>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arctan exp)
        {
            return AnalyzeTrigonometric<Tan>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Cos exp)
        {
            return AnalyzeTrigonometric<Arccos>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Cot exp)
        {
            return AnalyzeTrigonometric<Arccot>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Csc exp)
        {
            return AnalyzeTrigonometric<Arccsc>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Sec exp)
        {
            return AnalyzeTrigonometric<Arcsec>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Sin exp)
        {
            return AnalyzeTrigonometric<Arcsin>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Tan exp)
        {
            return AnalyzeTrigonometric<Arctan>(exp);
        }

        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arcosh exp)
        {
            return AnalyzeTrigonometric<Cosh>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arcoth exp)
        {
            return AnalyzeTrigonometric<Coth>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arcsch exp)
        {
            return AnalyzeTrigonometric<Csch>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arsech exp)
        {
            return AnalyzeTrigonometric<Sech>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Arsinh exp)
        {
            return AnalyzeTrigonometric<Sinh>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Artanh exp)
        {
            return AnalyzeTrigonometric<Tanh>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Cosh exp)
        {
            return AnalyzeTrigonometric<Arcosh>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Coth exp)
        {
            return AnalyzeTrigonometric<Arcoth>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Csch exp)
        {
            return AnalyzeTrigonometric<Arcsch>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Sech exp)
        {
            return AnalyzeTrigonometric<Arsech>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Sinh exp)
        {
            return AnalyzeTrigonometric<Arsinh>(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Tanh exp)
        {
            return AnalyzeTrigonometric<Artanh>(exp);
        }

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Avg exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Count exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Max exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Min exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Product exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Stdev exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Stdevp exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Sum exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Var exp)
        {
            return AnalyzeDiffParams(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Varp exp)
        {
            return AnalyzeDiffParams(exp);
        }

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Expressions.LogicalAndBitwise.And exp)
        {
            return AnalyzeBinary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Bool exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Equality exp)
        {
            return AnalyzeBinary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Implication exp)
        {
            return AnalyzeBinary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(NAnd exp)
        {
            return AnalyzeBinary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(NOr exp)
        {
            return AnalyzeBinary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Not exp)
        {
            return AnalyzeUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Expressions.LogicalAndBitwise.Or exp)
        {
            return AnalyzeBinary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(XOr exp)
        {
            return AnalyzeBinary(exp);
        }

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(AddAssign exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Expressions.Programming.And exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Dec exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(DivAssign exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Equal exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(For exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(GreaterOrEqual exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(GreaterThan exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(If exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Inc exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(LessOrEqual exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(LessThan exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(MulAssign exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(NotEqual exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Expressions.Programming.Or exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(SubAssign exp)
        {
            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(While exp)
        {
            return exp;
        }

        #endregion Programming

    }

}
