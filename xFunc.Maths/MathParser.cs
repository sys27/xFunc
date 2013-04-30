// Copyright 2012-2013 Dmitry Kischenko
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
using xFunc.Maths.Exceptions;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Resources;
using xFunc.Maths.Expressions.Bitwise;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    public class MathParser
    {

        private IMathLexer lexer;

        private string lastFunc = string.Empty;
        private IMathExpression mathExpression;

        private AngleMeasurement angleMeasurement;

        public MathParser()
            : this(new MathLexer())
        {

        }

        public MathParser(IMathLexer lexer)
        {
            this.lexer = lexer;
        }

        public static IMathExpression Simplify(IMathExpression expression)
        {
            IMathExpression exp = _Simplify(expression);
            exp.Parent = null;

            return exp;
        }

        private static IMathExpression _Simplify(IMathExpression expression)
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

        public static IMathExpression Differentiate(IMathExpression expression)
        {
            return Differentiate(expression, new Variable("x"));
        }

        public static IMathExpression Differentiate(IMathExpression expression, Variable variable)
        {
            return Simplify(expression.Differentiate(variable));
        }

        public static bool HasVar(IMathExpression expression, Variable arg)
        {
            if (expression is BinaryMathExpression)
            {
                var bin = expression as BinaryMathExpression;
                if (HasVar(bin.FirstMathExpression, arg))
                    return true;

                return HasVar(bin.SecondMathExpression, arg);
            }
            if (expression is UnaryMathExpression)
            {
                var un = expression as UnaryMathExpression;

                return HasVar(un.FirstMathExpression, arg);
            }
            if (expression.Equals(arg))
            {
                return true;
            }

            return false;
        }

        public IMathExpression Parse(string function)
        {
            return Parse(function, true);
        }

        public IMathExpression Parse(string function, bool simplify)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException("function");

            if (function != lastFunc)
            {
                IEnumerable<IToken> tokens = lexer.Tokenize(function);
                IEnumerable<IToken> rpn = ConvertToReversePolishNotation(tokens);
                IEnumerable<IMathExpression> expressions = ConvertTokensToExpressions(rpn);

                Stack<IMathExpression> stack = new Stack<IMathExpression>();
                foreach (var expression in expressions)
                {
                    if (expression is Number || expression is Variable)
                    {
                        stack.Push(expression);
                    }
                    else if (expression is BinaryMathExpression)
                    {
                        if ((expression is Log || expression is Root) && stack.Count < 2)
                            throw new MathParserException(Resource.InvalidNumberOfVariables);

                        BinaryMathExpression binExp = (BinaryMathExpression)expression;
                        binExp.SecondMathExpression = stack.Pop();
                        binExp.FirstMathExpression = stack.Pop();

                        stack.Push(binExp);
                    }
                    else if (expression is UnaryMathExpression)
                    {
                        UnaryMathExpression unaryMathExp = (UnaryMathExpression)expression;
                        unaryMathExp.FirstMathExpression = stack.Pop();

                        stack.Push(unaryMathExp);
                    }
                    else if (expression is Derivative)
                    {
                        if (stack.Count < 2)
                            throw new MathParserException(Resource.InvalidNumberOfVariables);
                        if (!(stack.Peek() is Variable))
                            throw new MathParserException(Resource.InvalidExpression);

                        Derivative binExp = (Derivative)expression;
                        binExp.Variable = (Variable)stack.Pop();
                        binExp.FirstMathExpression = stack.Pop();

                        stack.Push(binExp);
                    }
                    else if (expression is Assign)
                    {
                        if (stack.Count < 2)
                            throw new MathParserException(Resource.InvalidNumberOfVariables);

                        Assign assign = (Assign)expression;
                        assign.Value = stack.Pop();

                        if (!(stack.Peek() is Variable))
                            throw new MathParserException(Resource.InvalidExpression);

                        assign.Variable = (Variable)stack.Pop();

                        stack.Push(assign);
                    }
                    else if (expression is Undefine)
                    {
                        if (stack.Count < 1)
                            throw new MathParserException(Resource.InvalidNumberOfVariables);

                        Undefine undef = (Undefine)expression;

                        if (!(stack.Peek() is Variable))
                            throw new MathParserException(Resource.InvalidExpression);

                        undef.Variable = (Variable)stack.Pop();

                        stack.Push(undef);
                    }
                    else
                    {
                        throw new MathParserException(Resource.UnexpectedError);
                    }
                }

                if (stack.Count > 1)
                    throw new MathParserException(Resource.ErrorWhileParsingTree);

                lastFunc = function;
                mathExpression = stack.Pop();
            }

            if (simplify)
                return Simplify(mathExpression);

            return mathExpression;
        }

        private IEnumerable<IMathExpression> ConvertTokensToExpressions(IEnumerable<IToken> tokens)
        {
            List<IMathExpression> preOutput = new List<IMathExpression>();

            foreach (var token in tokens)
            {
                if (token is OperationToken)
                {
                    var t = token as OperationToken;
                    switch (t.Operation)
                    {
                        case Operations.Addition:
                            preOutput.Add(new Add());
                            break;
                        case Operations.Subtraction:
                            preOutput.Add(new Sub());
                            break;
                        case Operations.Multiplication:
                            preOutput.Add(new Mul());
                            break;
                        case Operations.Division:
                            preOutput.Add(new Div());
                            break;
                        case Operations.Exponentiation:
                            preOutput.Add(new Pow());
                            break;
                        case Operations.UnaryMinus:
                            preOutput.Add(new UnaryMinus());
                            break;
                        case Operations.Assign:
                            preOutput.Add(new Assign());
                            break;
                        case Operations.Not:
                            preOutput.Add(new Not());
                            break;
                        case Operations.And:
                            preOutput.Add(new And());
                            break;
                        case Operations.Or:
                            preOutput.Add(new Or());
                            break;
                        case Operations.XOr:
                            preOutput.Add(new XOr());
                            break;
                    }
                }
                else if (token is NumberToken)
                {
                    var t = token as NumberToken;

                    preOutput.Add(new Number(t.Number));
                }
                else if (token is VariableToken)
                {
                    var t = token as VariableToken;

                    preOutput.Add(new Variable(t.Variable));
                }
                else if (token is FunctionToken)
                {
                    var t = token as FunctionToken;

                    switch (t.Function)
                    {
                        case Functions.Absolute:
                            preOutput.Add(new Abs());
                            break;
                        case Functions.Sine:
                            preOutput.Add(new Sin() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Cosine:
                            preOutput.Add(new Cos() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Tangent:
                            preOutput.Add(new Tan() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Cotangent:
                            preOutput.Add(new Cot() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Secant:
                            preOutput.Add(new Sec() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Cosecant:
                            preOutput.Add(new Csc() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Arcsine:
                            preOutput.Add(new Arcsin() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Arccosine:
                            preOutput.Add(new Arccos() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Arctangent:
                            preOutput.Add(new Arctan() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Arccotangent:
                            preOutput.Add(new Arccot() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Arcsecant:
                            preOutput.Add(new Arcsec() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Arccosecant:
                            preOutput.Add(new Arccsc() { AngleMeasurement = this.AngleMeasurement });
                            break;
                        case Functions.Sqrt:
                            preOutput.Add(new Sqrt());
                            break;
                        case Functions.Root:
                            preOutput.Add(new Root());
                            break;
                        case Functions.Ln:
                            preOutput.Add(new Ln());
                            break;
                        case Functions.Lg:
                            preOutput.Add(new Lg());
                            break;
                        case Functions.Log:
                            preOutput.Add(new Log());
                            break;
                        case Functions.Sineh:
                            preOutput.Add(new Sinh());
                            break;
                        case Functions.Cosineh:
                            preOutput.Add(new Cosh());
                            break;
                        case Functions.Tangenth:
                            preOutput.Add(new Tanh());
                            break;
                        case Functions.Cotangenth:
                            preOutput.Add(new Coth());
                            break;
                        case Functions.Secanth:
                            preOutput.Add(new Sech());
                            break;
                        case Functions.Cosecanth:
                            preOutput.Add(new Csch());
                            break;
                        case Functions.Arsineh:
                            preOutput.Add(new Arsech());
                            break;
                        case Functions.Arcosineh:
                            preOutput.Add(new Arcosh());
                            break;
                        case Functions.Artangenth:
                            preOutput.Add(new Artanh());
                            break;
                        case Functions.Arcotangenth:
                            preOutput.Add(new Arcoth());
                            break;
                        case Functions.Arsecanth:
                            preOutput.Add(new Arsech());
                            break;
                        case Functions.Arcosecanth:
                            preOutput.Add(new Arcsch());
                            break;
                        case Functions.Exp:
                            preOutput.Add(new Exp());
                            break;
                        case Functions.Derivative:
                            preOutput.Add(new Derivative());
                            break;
                        case Functions.Undefine:
                            preOutput.Add(new Undefine());
                            break;
                    }
                }
            }

            return preOutput;
        }

        private IEnumerable<IToken> ConvertToReversePolishNotation(IEnumerable<IToken> tokens)
        {
            List<IToken> output = new List<IToken>();
            Stack<IToken> stack = new Stack<IToken>();

            // todo: implement
            //foreach (var token in tokens)
            //{
            //    MathToken stackToken;
            //    if (token.Type == MathTokenType.OpenBracket)
            //    {
            //        stack.Push(token);
            //    }
            //    else if (token.Type == MathTokenType.CloseBracket)
            //    {
            //        stackToken = stack.Pop();
            //        while (stackToken.Type != MathTokenType.OpenBracket)
            //        {
            //            output.Add(stackToken);
            //            stackToken = stack.Pop();
            //        }
            //    }
            //    else if (token.Type == MathTokenType.Comma)
            //    {
            //        stackToken = stack.Pop();
            //        while (stackToken.Type != MathTokenType.OpenBracket)
            //        {
            //            output.Add(stackToken);
            //            stackToken = stack.Pop();
            //        }

            //        stack.Push(stackToken);
            //    }
            //    else if (token.Type == MathTokenType.Number || token.Type == MathTokenType.Variable)
            //    {
            //        output.Add(token);
            //    }
            //    else
            //    {
            //        while (stack.Count != 0 && (stackToken = stack.Peek()).Type >= token.Type)
            //        {
            //            if (stackToken.Type == MathTokenType.OpenBracket)
            //                break;
            //            output.Add(stack.Pop());
            //        }

            //        stack.Push(token);
            //    }
            //}
            //if (stack.Count != 0)
            //{
            //    output.AddRange(stack);
            //}

            return output;
        }

        /// <summary>
        /// Get or Set a measurement of angles.
        /// </summary>
        /// <seealso cref="AngleMeasurement"/>
        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return angleMeasurement;
            }
            set
            {
                lastFunc = string.Empty;
                mathExpression = null;
                angleMeasurement = value;
            }
        }

    }

}
