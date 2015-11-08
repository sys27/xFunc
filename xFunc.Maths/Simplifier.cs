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
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths
{

    /// <summary>
    /// The default implementation of <see cref="ISimplifier"/>
    /// </summary>
    public class Simplifier : ISimplifier
    {

        private readonly Number zero = 0;
        private readonly Number one = 1;

        /// <summary>
        /// Simplifies the <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">A expression to simplify.</param>
        /// <returns>A simplified expression.</returns>
        public IExpression Simplify(IExpression expression)
        {
            var exp = _Simplify(expression);
            exp.Parent = null;

            return exp;
        }

        private IExpression _Simplify(IExpression expression)
        {
            if (expression is Number)
                return expression;
            if (expression is Variable)
                return expression;

            if (expression is BinaryExpression)
            {
                var bin = expression as BinaryExpression;
                bin.Left = _Simplify(bin.Left);
                bin.Right = _Simplify(bin.Right);
            }
            else if (expression is UnaryExpression)
            {
                var un = expression as UnaryExpression;
                un.Argument = _Simplify(un.Argument);
            }
            else if (expression is Simplify)
            {
                var simp = expression as Simplify;
                simp.Expression = _Simplify(simp.Expression);

                return simp;
            }
            else if (expression is Derivative)
            {
                var deriv = expression as Derivative;
                deriv.Expression = _Simplify(deriv.Expression);

                return deriv;
            }

            if (expression is UnaryMinus)
            {
                var unMinus = expression as UnaryMinus;
                // -(-x)
                if (unMinus.Argument is UnaryMinus)
                    return (unMinus.Argument as UnaryMinus).Argument;
                // -1
                if (unMinus.Argument is Number)
                {
                    var number = unMinus.Argument as Number;
                    number.Value = -number.Value;

                    return number;
                }
            }
            else if (expression is Add)
            {
                return SimplifyAdd(expression as Add);
            }
            else if (expression is Sub)
            {
                return SimplifySub(expression as Sub);
            }
            else if (expression is Mul)
            {
                return SimplifyMul(expression as Mul);
            }
            else if (expression is Div)
            {
                return SimplifyDiv(expression as Div);
            }
            else if (expression is Pow)
            {
                var inv = expression as Pow;

                // x^0
                if (inv.Right.Equals(zero))
                    return one;
                // x^1
                if (inv.Right.Equals(one))
                    return inv.Left;
            }
            else if (expression is Root)
            {
                var root = expression as Root;

                // root(x, 1)
                if (root.Right.Equals(one))
                    return root.Left;
            }
            else if (expression is Log)
            {
                var log = expression as Log;

                // log(4x, 4x)
                if (log.Left.Equals(log.Right))
                    return one;
            }
            else if (expression is Ln)
            {
                var ln = expression as Ln;

                // ln(e)
                if (ln.Argument.Equals(new Variable("e")))
                    return one;
            }
            else if (expression is Lg)
            {
                var lg = expression as Lg;

                // lg(10)
                if (lg.Argument.Equals(new Number(10)))
                    return one;
            }
            else if (expression is TrigonometricExpression || expression is HyperbolicExpression)
            {
                return SimplifyTrig((UnaryExpression)expression);
            }

            return expression;
        }

        private IExpression SimplifyAdd(Add add)
        {
            // plus zero
            if (add.Left.Equals(zero))
                return add.Right;
            if (add.Right.Equals(zero))
                return add.Left;

            if (add.Left is Number && add.Right is Number)
                return new Number((double)add.Calculate());

            var leftVar = add.Left as Variable;
            var rightVar = add.Right as Variable;
            if (leftVar != null && rightVar != null && leftVar.Name == rightVar.Name)
                return new Mul(new Number(2), leftVar);

            if (add.Left is UnaryMinus)
            {
                IExpression temp = add.Left;
                add.Left = add.Right;
                add.Right = temp;
            }
            if (add.Right is UnaryMinus)
            {
                var unMinus = add.Right as UnaryMinus;
                var sub = new Sub(add.Left, unMinus.Argument);

                return sub;
            }

            // 2 + (2 + x)
            // 2 + (x + 2)
            // (2 + x) + 2
            // (x + 2) + 2
            Add bracketAdd = null;
            Number firstNumber = null;
            if (add.Left is Add && add.Right is Number)
            {
                bracketAdd = add.Left as Add;
                firstNumber = add.Right as Number;
            }
            else if (add.Right is Add && add.Left is Number)
            {
                bracketAdd = add.Right as Add;
                firstNumber = add.Left as Number;
            }
            if (bracketAdd != null)
            {
                if (bracketAdd.Left is Number)
                {
                    var secondNumber = bracketAdd.Left as Number;
                    var result = new Add(bracketAdd.Right, new Number(firstNumber.Value + secondNumber.Value));

                    return _Simplify(result);
                }
                if (bracketAdd.Right is Number)
                {
                    var secondNumber = bracketAdd.Right as Number;
                    var result = new Add(bracketAdd.Left, new Number(firstNumber.Value + secondNumber.Value));

                    return _Simplify(result);
                }
            }

            // 2 + (2 - x)
            // 2 + (x - 2)
            // (2 - x) + 2
            // (x - 2) + 2
            Sub bracketSub = null;
            if (add.Left is Sub && add.Right is Number)
            {
                bracketSub = add.Left as Sub;
                firstNumber = add.Right as Number;
            }
            else if (add.Right is Sub && add.Left is Number)
            {
                bracketSub = add.Right as Sub;
                firstNumber = add.Left as Number;
            }
            if (bracketSub != null)
            {
                var secondNumber = bracketSub.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Sub(new Number(firstNumber.Value + secondNumber.Value), bracketSub.Right));

                secondNumber = bracketSub.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Add(new Number(firstNumber.Value - secondNumber.Value), bracketSub.Left));
            }

            return add;
        }

        private IExpression SimplifySub(Sub sub)
        {
            // sub zero
            if (sub.Left.Equals(zero))
                return _Simplify(new UnaryMinus(sub.Right));
            if (sub.Right.Equals(zero))
                return sub.Left;

            if (sub.Left is Number && sub.Right is Number)
                return new Number((double)sub.Calculate());

            if (sub.Left is Variable && sub.Right is Variable)
                return zero;

            if (sub.Right is UnaryMinus)
            {
                var unMinus = sub.Right as UnaryMinus;
                var add = new Add(sub.Left, unMinus.Argument);

                return add;
            }

            // (2 + x) - 2
            // (x + 2) - 2
            if (sub.Left is Add && sub.Right is Number)
            {
                var bracketAdd = sub.Left as Add;
                var firstNumber = sub.Right as Number;

                var secondNumber = bracketAdd.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Add(bracketAdd.Right, new Number((double)firstNumber.Calculate() - (double)secondNumber.Calculate())));

                secondNumber = bracketAdd.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Add(bracketAdd.Left, new Number((double)firstNumber.Calculate() - (double)secondNumber.Calculate())));
            }
            // 2 - (2 + x)
            // 2 - (x + 2)
            else if (sub.Right is Add && sub.Left is Number)
            {
                var bracketAdd = sub.Right as Add;
                var firstNumber = sub.Left as Number;

                var secondNumber = bracketAdd.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Sub(new Number((double)firstNumber.Calculate() - (double)secondNumber.Calculate()), bracketAdd.Right));

                secondNumber = bracketAdd.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Sub(new Number((double)firstNumber.Calculate() - (double)secondNumber.Calculate()), bracketAdd.Left));
            }
            // (2 - x) - 2
            // (x - 2) - 2
            else if (sub.Left is Sub && sub.Right is Number)
            {
                var bracketSub = sub.Left as Sub;
                var firstNumber = sub.Right as Number;

                var secondNumber = bracketSub.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Sub(new Number((double)firstNumber.Calculate() - (double)secondNumber.Calculate()), bracketSub.Right));

                secondNumber = bracketSub.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Sub(bracketSub.Left, new Number((double)firstNumber.Calculate() + (double)secondNumber.Calculate())));
            }
            // 2 - (2 - x)
            // 2 - (x - 2)
            else if (sub.Right is Sub && sub.Left is Number)
            {
                var bracketSub = sub.Right as Sub;
                var firstNumber = sub.Left as Number;

                var secondNumber = bracketSub.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Add(new Number((double)firstNumber.Calculate() - (double)secondNumber.Calculate()), bracketSub.Right));

                secondNumber = bracketSub.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Sub(new Number((double)firstNumber.Calculate() + (double)secondNumber.Calculate()), bracketSub.Left));
            }

            return sub;
        }

        private IExpression SimplifyMul(Mul mul)
        {
            // mul by zero
            if (mul.Left.Equals(zero) || mul.Right.Equals(zero))
                return zero;

            // mul by 1
            if (mul.Left.Equals(one))
                return mul.Right;
            if (mul.Right.Equals(one))
                return mul.Left;

            if (mul.Left is Number && mul.Right is Number)
                return new Number((double)mul.Calculate());

            // 2 * (2 * x)
            // 2 * (x * 2)
            // (2 * x) * 2
            // (x * 2) * 2
            Mul bracketMul = null;
            Number firstNumber = null;
            if (mul.Left is Mul && mul.Right is Number)
            {
                bracketMul = mul.Left as Mul;
                firstNumber = mul.Right as Number;
            }
            else if (mul.Right is Mul && mul.Left is Number)
            {
                bracketMul = mul.Right as Mul;
                firstNumber = mul.Left as Number;
            }
            if (bracketMul != null)
            {
                var secondNumber = bracketMul.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Mul(new Number(firstNumber.Value * secondNumber.Value), bracketMul.Right));

                secondNumber = bracketMul.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Mul(new Number(firstNumber.Value * secondNumber.Value), bracketMul.Left));
            }

            // 2 * (2 / x)
            // 2 * (x / 2)
            // (2 / x) * 2
            // (x / 2) * 2
            Div bracketDiv = null;
            if (mul.Left is Div && mul.Right is Number)
            {
                bracketDiv = mul.Left as Div;
                firstNumber = mul.Right as Number;
            }
            else if (mul.Right is Div && mul.Left is Number)
            {
                bracketDiv = mul.Right as Div;
                firstNumber = mul.Left as Number;
            }
            if (bracketDiv != null)
            {
                var secondNumber = bracketDiv.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Div(new Number(firstNumber.Value * secondNumber.Value), bracketDiv.Right));

                secondNumber = bracketDiv.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Mul(new Number(firstNumber.Value / secondNumber.Value), bracketDiv.Left));
            }

            return mul;
        }

        private IExpression SimplifyDiv(Div div)
        {
            // 0 / x
            if (div.Left.Equals(zero) && !div.Right.Equals(zero))
                return zero;
            // x / 0
            if (div.Right.Equals(zero) && !div.Left.Equals(zero))
                throw new DivideByZeroException();
            // x / 1
            if (div.Right.Equals(one))
                return div.Left;

            if (div.Left is Number && div.Right is Number)
                return new Number((double)div.Calculate());

            if (div.Left is Variable && div.Right is Variable)
                return one;

            // (2 * x) / 2
            // (x * 2) / 2
            if (div.Left is Mul && div.Right is Number)
            {
                var bracketMul = div.Left as Mul;
                var firstNumber = div.Right as Number;

                var secondNumber = bracketMul.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Div(bracketMul.Right, new Number((double)firstNumber.Calculate() / (double)secondNumber.Calculate())));

                secondNumber = bracketMul.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Div(bracketMul.Left, new Number((double)firstNumber.Calculate() / (double)secondNumber.Calculate())));
            }
            // 2 / (2 * x)
            // 2 / (x * 2)
            else if (div.Right is Mul && div.Left is Number)
            {
                var bracketMul = div.Right as Mul;
                var firstNumber = div.Left as Number;

                var secondNumber = bracketMul.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Div(new Number((double)firstNumber.Calculate() / (double)secondNumber.Calculate()), bracketMul.Right));

                secondNumber = bracketMul.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Div(new Number((double)firstNumber.Calculate() / (double)secondNumber.Calculate()), bracketMul.Left));
            }
            // (2 / x) / 2
            // (x / 2) / 2
            else if (div.Left is Div && div.Right is Number)
            {
                var bracketDiv = div.Left as Div;
                var firstNumber = div.Right as Number;

                var secondNumber = bracketDiv.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Div(new Number((double)firstNumber.Calculate() / (double)secondNumber.Calculate()), bracketDiv.Right));

                secondNumber = bracketDiv.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Div(bracketDiv.Left, new Number((double)firstNumber.Calculate() * (double)secondNumber.Calculate())));
            }
            // 2 / (2 / x)
            // 2 / (x / 2)
            else if (div.Right is Div && div.Left is Number)
            {
                var bracketDiv = div.Right as Div;
                var firstNumber = div.Left as Number;

                var secondNumber = bracketDiv.Left as Number;
                if (secondNumber != null)
                    return _Simplify(new Mul(new Number((double)firstNumber.Calculate() / (double)secondNumber.Calculate()), bracketDiv.Right));

                secondNumber = bracketDiv.Right as Number;
                if (secondNumber != null)
                    return _Simplify(new Div(new Number((double)firstNumber.Calculate() * (double)secondNumber.Calculate()), bracketDiv.Left));
            }

            return div;
        }

        private IExpression SimplifyTrig(UnaryExpression unary)
        {
            var attrs = unary.GetType().GetCustomAttributes(typeof(ReverseFunctionAttribute), false);
            ReverseFunctionAttribute attr = null;
            if (attrs.Length > 0)
                attr = (ReverseFunctionAttribute)attrs[0];

            if (attr != null && unary.Argument.GetType() == attr.ReverseType)
                return ((UnaryExpression)unary.Argument).Argument;

            return unary;
        }

    }

}
