using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{
    
    public class MathSimplifier : ISimplifier
    {

        public IMathExpression Simplify(IMathExpression expression)
        {
            IMathExpression exp = _Simplify(expression);
            exp.Parent = null;

            return exp;
        }

        private IMathExpression _Simplify(IMathExpression expression)
        {
            if (expression is Number)
                return expression;
            if (expression is Variable)
                return expression;

            if (expression is BinaryMathExpression)
            {
                BinaryMathExpression bin = expression as BinaryMathExpression;
                bin.FirstMathExpression = _Simplify(bin.FirstMathExpression);
                bin.SecondMathExpression = _Simplify(bin.SecondMathExpression);
            }
            else if (expression is UnaryMathExpression)
            {
                UnaryMathExpression un = expression as UnaryMathExpression;
                un.FirstMathExpression = _Simplify(un.FirstMathExpression);
            }

            Number zero = 0;
            Number one = 1;

            if (expression is UnaryMinus)
            {
                UnaryMinus unMinus = expression as UnaryMinus;
                // -(-x)
                if (unMinus.FirstMathExpression is UnaryMinus)
                    return (unMinus.FirstMathExpression as UnaryMinus).FirstMathExpression;
                // -1
                if (unMinus.FirstMathExpression is Number)
                {
                    Number number = unMinus.FirstMathExpression as Number;
                    number.Value = -number.Value;

                    return number;
                }
            }
            else if (expression is Add)
            {
                Add add = expression as Add;

                // plus zero
                if (add.FirstMathExpression.Equals(zero))
                    return add.SecondMathExpression;
                if (add.SecondMathExpression.Equals(zero))
                    return add.FirstMathExpression;

                if (add.FirstMathExpression is Number && add.SecondMathExpression is Number)
                    return new Number(add.Calculate(null));

                if (add.FirstMathExpression is UnaryMinus)
                {
                    IMathExpression temp = add.FirstMathExpression;
                    add.FirstMathExpression = add.SecondMathExpression;
                    add.SecondMathExpression = temp;
                }
                if (add.SecondMathExpression is UnaryMinus)
                {
                    UnaryMinus unMinus = add.SecondMathExpression as UnaryMinus;
                    Sub sub = new Sub(add.FirstMathExpression, unMinus.FirstMathExpression);

                    return sub;
                }

                // 2 + (2 + x)
                // 2 + (x + 2)
                // (2 + x) + 2
                // (x + 2) + 2
                Add bracketAdd = null;
                Number firstNumber = null;
                if (add.FirstMathExpression is Add && add.SecondMathExpression is Number)
                {
                    bracketAdd = add.FirstMathExpression as Add;
                    firstNumber = add.SecondMathExpression as Number;
                }
                else if (add.SecondMathExpression is Add && add.FirstMathExpression is Number)
                {
                    bracketAdd = add.SecondMathExpression as Add;
                    firstNumber = add.FirstMathExpression as Number;
                }
                if (bracketAdd != null)
                {
                    if (bracketAdd.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.FirstMathExpression as Number;
                        Add result = new Add(bracketAdd.SecondMathExpression, new Number(firstNumber.Value + secondNumber.Value));

                        return _Simplify(result);
                    }
                    if (bracketAdd.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.SecondMathExpression as Number;
                        Add result = new Add(bracketAdd.FirstMathExpression, new Number(firstNumber.Value + secondNumber.Value));

                        return _Simplify(result);
                    }
                }

                // 2 + (2 - x)
                // 2 + (x - 2)
                // (2 - x) + 2
                // (x - 2) + 2
                Sub bracketSub = null;
                if (add.FirstMathExpression is Sub && add.SecondMathExpression is Number)
                {
                    bracketSub = add.FirstMathExpression as Sub;
                    firstNumber = add.SecondMathExpression as Number;
                }
                else if (add.SecondMathExpression is Sub && add.FirstMathExpression is Number)
                {
                    bracketSub = add.SecondMathExpression as Sub;
                    firstNumber = add.FirstMathExpression as Number;
                }
                if (bracketSub != null)
                {
                    if (bracketSub.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.FirstMathExpression as Number;
                        Sub result = new Sub(new Number(firstNumber.Value + secondNumber.Value), bracketSub.SecondMathExpression);

                        return _Simplify(result);
                    }
                    if (bracketSub.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.SecondMathExpression as Number;
                        Add result = new Add(new Number(firstNumber.Value - secondNumber.Value), bracketSub.FirstMathExpression);

                        return _Simplify(result);
                    }
                }
            }
            else if (expression is Sub)
            {
                Sub sub = expression as Sub;

                // sub zero
                if (sub.FirstMathExpression.Equals(zero))
                    return _Simplify(new UnaryMinus(sub.SecondMathExpression));
                if (sub.SecondMathExpression.Equals(zero))
                    return sub.FirstMathExpression;

                if (sub.FirstMathExpression is Number && sub.SecondMathExpression is Number)
                    return new Number(sub.Calculate(null));

                if (sub.SecondMathExpression is UnaryMinus)
                {
                    UnaryMinus unMinus = sub.SecondMathExpression as UnaryMinus;
                    Add add = new Add(sub.FirstMathExpression, unMinus.FirstMathExpression);

                    return add;
                }

                // (2 + x) - 2
                // (x + 2) - 2
                if (sub.FirstMathExpression is Add && sub.SecondMathExpression is Number)
                {
                    Add bracketAdd = sub.FirstMathExpression as Add;
                    Number firstNumber = sub.SecondMathExpression as Number;

                    if (bracketAdd.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.FirstMathExpression as Number;
                        Add result = new Add(bracketAdd.SecondMathExpression, new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)));

                        return _Simplify(result);
                    }
                    if (bracketAdd.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.SecondMathExpression as Number;
                        Add result = new Add(bracketAdd.FirstMathExpression, new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)));

                        return _Simplify(result);
                    }
                }
                // 2 - (2 + x)
                // 2 - (x + 2)
                else if (sub.SecondMathExpression is Add && sub.FirstMathExpression is Number)
                {
                    Add bracketAdd = sub.SecondMathExpression as Add;
                    Number firstNumber = sub.FirstMathExpression as Number;

                    if (bracketAdd.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.FirstMathExpression as Number;
                        Sub result = new Sub(new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketAdd.SecondMathExpression);

                        return _Simplify(result);
                    }
                    if (bracketAdd.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.SecondMathExpression as Number;
                        Sub result = new Sub(new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketAdd.FirstMathExpression);

                        return _Simplify(result);
                    }
                }
                // (2 - x) - 2
                // (x - 2) - 2
                else if (sub.FirstMathExpression is Sub && sub.SecondMathExpression is Number)
                {
                    Sub bracketSub = sub.FirstMathExpression as Sub;
                    Number firstNumber = sub.SecondMathExpression as Number;

                    if (bracketSub.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.FirstMathExpression as Number;
                        Sub result = new Sub(new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketSub.SecondMathExpression);

                        return _Simplify(result);
                    }
                    if (bracketSub.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.SecondMathExpression as Number;
                        Sub result = new Sub(bracketSub.FirstMathExpression, new Number(firstNumber.Calculate(null) + secondNumber.Calculate(null)));

                        return _Simplify(result);
                    }
                }
                // 2 - (2 - x)
                // 2 - (x - 2)
                else if (sub.SecondMathExpression is Sub && sub.FirstMathExpression is Number)
                {
                    Sub bracketSub = sub.SecondMathExpression as Sub;
                    Number firstNumber = sub.FirstMathExpression as Number;

                    if (bracketSub.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.FirstMathExpression as Number;
                        Add result = new Add(new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketSub.SecondMathExpression);

                        return _Simplify(result);
                    }
                    if (bracketSub.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.SecondMathExpression as Number;
                        Sub result = new Sub(new Number(firstNumber.Calculate(null) + secondNumber.Calculate(null)), bracketSub.FirstMathExpression);

                        return _Simplify(result);
                    }
                }
            }
            else if (expression is Mul)
            {
                Mul mul = expression as Mul;

                // mul by zero
                if (mul.FirstMathExpression.Equals(zero) || mul.SecondMathExpression.Equals(zero))
                    return zero;

                // mul by 1
                if (mul.FirstMathExpression.Equals(one))
                    return mul.SecondMathExpression;
                if (mul.SecondMathExpression.Equals(one))
                    return mul.FirstMathExpression;

                if (mul.FirstMathExpression is Number && mul.SecondMathExpression is Number)
                    return new Number(mul.Calculate());

                // 2 * (2 * x)
                // 2 * (x * 2)
                // (2 * x) * 2
                // (x * 2) * 2
                Mul bracketMul = null;
                Number firstNumber = null;
                if (mul.FirstMathExpression is Mul && mul.SecondMathExpression is Number)
                {
                    bracketMul = mul.FirstMathExpression as Mul;
                    firstNumber = mul.SecondMathExpression as Number;
                }
                else if (mul.SecondMathExpression is Mul && mul.FirstMathExpression is Number)
                {
                    bracketMul = mul.SecondMathExpression as Mul;
                    firstNumber = mul.FirstMathExpression as Number;
                }
                if (bracketMul != null)
                {
                    if (bracketMul.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.FirstMathExpression as Number;
                        Mul result = new Mul(new Number(firstNumber.Value * secondNumber.Value), bracketMul.SecondMathExpression);

                        return _Simplify(result);
                    }
                    if (bracketMul.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.SecondMathExpression as Number;
                        Mul result = new Mul(new Number(firstNumber.Value * secondNumber.Value), bracketMul.FirstMathExpression);

                        return _Simplify(result);
                    }
                }

                // 2 * (2 / x)
                // 2 * (x / 2)
                // (2 / x) * 2
                // (x / 2) * 2
                Div bracketDiv = null;
                if (mul.FirstMathExpression is Div && mul.SecondMathExpression is Number)
                {
                    bracketDiv = mul.FirstMathExpression as Div;
                    firstNumber = mul.SecondMathExpression as Number;
                }
                else if (mul.SecondMathExpression is Div && mul.FirstMathExpression is Number)
                {
                    bracketDiv = mul.SecondMathExpression as Div;
                    firstNumber = mul.FirstMathExpression as Number;
                }
                if (bracketDiv != null)
                {
                    if (bracketDiv.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.FirstMathExpression as Number;
                        Div result = new Div(new Number(firstNumber.Value * secondNumber.Value), bracketDiv.SecondMathExpression);

                        return _Simplify(result);
                    }
                    if (bracketDiv.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.SecondMathExpression as Number;
                        Mul result = new Mul(new Number(firstNumber.Value / secondNumber.Value), bracketDiv.FirstMathExpression);

                        return _Simplify(result);
                    }
                }
            }
            else if (expression is Div)
            {
                Div div = expression as Div;

                // 0 / x
                if (div.FirstMathExpression.Equals(zero))
                    return zero;
                // x / 0
                if (div.SecondMathExpression.Equals(zero))
                    throw new DivideByZeroException();
                // x / 1
                if (div.SecondMathExpression.Equals(one))
                    return div.FirstMathExpression;

                if (div.FirstMathExpression is Number && div.SecondMathExpression is Number)
                    return new Number(div.Calculate());

                // (2 * x) / 2
                // (x * 2) / 2
                if (div.FirstMathExpression is Mul && div.SecondMathExpression is Number)
                {
                    Mul bracketMul = div.FirstMathExpression as Mul;
                    Number firstNumber = div.SecondMathExpression as Number;

                    if (bracketMul.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.FirstMathExpression as Number;
                        Div result = new Div(bracketMul.SecondMathExpression, new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)));

                        return _Simplify(result);
                    }
                    if (bracketMul.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.SecondMathExpression as Number;
                        Div result = new Div(bracketMul.FirstMathExpression, new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)));

                        return _Simplify(result);
                    }
                }
                // 2 / (2 * x)
                // 2 / (x * 2)
                else if (div.SecondMathExpression is Mul && div.FirstMathExpression is Number)
                {
                    Mul bracketMul = div.SecondMathExpression as Mul;
                    Number firstNumber = div.FirstMathExpression as Number;

                    if (bracketMul.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.FirstMathExpression as Number;
                        Div result = new Div(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.SecondMathExpression);

                        return _Simplify(result);
                    }
                    if (bracketMul.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.SecondMathExpression as Number;
                        Div result = new Div(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.FirstMathExpression);

                        return _Simplify(result);
                    }
                }
                // (2 / x) / 2
                // (x / 2) / 2
                else if (div.FirstMathExpression is Div && div.SecondMathExpression is Number)
                {
                    Div bracketDiv = div.FirstMathExpression as Div;
                    Number firstNumber = div.SecondMathExpression as Number;

                    if (bracketDiv.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.FirstMathExpression as Number;
                        Div result = new Div(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketDiv.SecondMathExpression);

                        return _Simplify(result);
                    }
                    if (bracketDiv.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.SecondMathExpression as Number;
                        Div result = new Div(bracketDiv.FirstMathExpression, new Number(firstNumber.Calculate(null) * secondNumber.Calculate(null)));

                        return _Simplify(result);
                    }
                }
                // 2 / (2 / x)
                // 2 / (x / 2)
                else if (div.SecondMathExpression is Div && div.FirstMathExpression is Number)
                {
                    Div bracketDiv = div.SecondMathExpression as Div;
                    Number firstNumber = div.FirstMathExpression as Number;

                    if (bracketDiv.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.FirstMathExpression as Number;
                        Mul result = new Mul(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketDiv.SecondMathExpression);

                        return _Simplify(result);
                    }
                    if (bracketDiv.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.SecondMathExpression as Number;
                        Div result = new Div(new Number(firstNumber.Calculate(null) * secondNumber.Calculate(null)), bracketDiv.FirstMathExpression);

                        return _Simplify(result);
                    }
                }
            }
            else if (expression is Pow)
            {
                Pow inv = expression as Pow;

                // x^0
                if (inv.SecondMathExpression.Equals(zero))
                    return one;
                // x^1
                if (inv.SecondMathExpression.Equals(one))
                    return inv.FirstMathExpression;
            }
            else if (expression is Root)
            {
                Root root = expression as Root;

                // root(x, 1)
                if (root.SecondMathExpression.Equals(one))
                    return root.FirstMathExpression;
            }
            else if (expression is Log)
            {
                Log log = expression as Log;

                // log(4x, 4x)
                if (log.FirstMathExpression.Equals(log.SecondMathExpression))
                    return one;
            }
            else if (expression is Ln)
            {
                Ln ln = expression as Ln;

                // ln(e)
                if (ln.FirstMathExpression.Equals(new Variable("e")))
                    return one;
            }
            else if (expression is Lg)
            {
                Lg lg = expression as Lg;

                // lg(10)
                if (lg.FirstMathExpression.Equals(new Number(10)))
                    return one;
            }

            return expression;
        }

    }

}
