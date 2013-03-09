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

        public static IMathExpression SimplifyExpressions(IMathExpression expression)
        {
            IMathExpression exp = _SimplifyExpressions(expression);
            exp.Parent = null;

            return exp;
        }

        private static IMathExpression _SimplifyExpressions(IMathExpression expression)
        {
            if (expression is Number)
                return expression;
            if (expression is Variable)
                return expression;

            if (expression is BinaryMathExpression)
            {
                BinaryMathExpression bin = expression as BinaryMathExpression;
                bin.FirstMathExpression = _SimplifyExpressions(bin.FirstMathExpression);
                bin.SecondMathExpression = _SimplifyExpressions(bin.SecondMathExpression);
            }
            else if (expression is UnaryMathExpression)
            {
                UnaryMathExpression un = expression as UnaryMathExpression;
                un.FirstMathExpression = _SimplifyExpressions(un.FirstMathExpression);
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
            else if (expression is Addition)
            {
                Addition add = expression as Addition;

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
                    Subtraction sub = new Subtraction(add.FirstMathExpression, unMinus.FirstMathExpression);

                    return sub;
                }

                // 2 + (2 + x)
                // 2 + (x + 2)
                // (2 + x) + 2
                // (x + 2) + 2
                Addition bracketAdd = null;
                Number firstNumber = null;
                if (add.FirstMathExpression is Addition && add.SecondMathExpression is Number)
                {
                    bracketAdd = add.FirstMathExpression as Addition;
                    firstNumber = add.SecondMathExpression as Number;
                }
                else if (add.SecondMathExpression is Addition && add.FirstMathExpression is Number)
                {
                    bracketAdd = add.SecondMathExpression as Addition;
                    firstNumber = add.FirstMathExpression as Number;
                }
                if (bracketAdd != null)
                {
                    if (bracketAdd.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.FirstMathExpression as Number;
                        Addition result = new Addition(bracketAdd.SecondMathExpression, new Number(firstNumber.Value + secondNumber.Value));

                        return _SimplifyExpressions(result);
                    }
                    if (bracketAdd.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.SecondMathExpression as Number;
                        Addition result = new Addition(bracketAdd.FirstMathExpression, new Number(firstNumber.Value + secondNumber.Value));

                        return _SimplifyExpressions(result);
                    }
                }

                // 2 + (2 - x)
                // 2 + (x - 2)
                // (2 - x) + 2
                // (x - 2) + 2
                Subtraction bracketSub = null;
                if (add.FirstMathExpression is Subtraction && add.SecondMathExpression is Number)
                {
                    bracketSub = add.FirstMathExpression as Subtraction;
                    firstNumber = add.SecondMathExpression as Number;
                }
                else if (add.SecondMathExpression is Subtraction && add.FirstMathExpression is Number)
                {
                    bracketSub = add.SecondMathExpression as Subtraction;
                    firstNumber = add.FirstMathExpression as Number;
                }
                if (bracketSub != null)
                {
                    if (bracketSub.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.FirstMathExpression as Number;
                        Subtraction result = new Subtraction(new Number(firstNumber.Value + secondNumber.Value), bracketSub.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketSub.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.SecondMathExpression as Number;
                        Addition result = new Addition(new Number(firstNumber.Value - secondNumber.Value), bracketSub.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
            }
            else if (expression is Subtraction)
            {
                Subtraction sub = expression as Subtraction;

                // sub zero
                if (sub.FirstMathExpression.Equals(zero))
                    return _SimplifyExpressions(new UnaryMinus(sub.SecondMathExpression));
                if (sub.SecondMathExpression.Equals(zero))
                    return sub.FirstMathExpression;

                if (sub.FirstMathExpression is Number && sub.SecondMathExpression is Number)
                    return new Number(sub.Calculate(null));

                if (sub.SecondMathExpression is UnaryMinus)
                {
                    UnaryMinus unMinus = sub.SecondMathExpression as UnaryMinus;
                    Addition add = new Addition(sub.FirstMathExpression, unMinus.FirstMathExpression);

                    return add;
                }

                // (2 + x) - 2
                // (x + 2) - 2
                if (sub.FirstMathExpression is Addition && sub.SecondMathExpression is Number)
                {
                    Addition bracketAdd = sub.FirstMathExpression as Addition;
                    Number firstNumber = sub.SecondMathExpression as Number;

                    if (bracketAdd.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.FirstMathExpression as Number;
                        Addition result = new Addition(bracketAdd.SecondMathExpression, new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)));

                        return _SimplifyExpressions(result);
                    }
                    if (bracketAdd.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.SecondMathExpression as Number;
                        Addition result = new Addition(bracketAdd.FirstMathExpression, new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)));

                        return _SimplifyExpressions(result);
                    }
                }
                // 2 - (2 + x)
                // 2 - (x + 2)
                else if (sub.SecondMathExpression is Addition && sub.FirstMathExpression is Number)
                {
                    Addition bracketAdd = sub.SecondMathExpression as Addition;
                    Number firstNumber = sub.FirstMathExpression as Number;

                    if (bracketAdd.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.FirstMathExpression as Number;
                        Subtraction result = new Subtraction(new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketAdd.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketAdd.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketAdd.SecondMathExpression as Number;
                        Subtraction result = new Subtraction(new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketAdd.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
                // (2 - x) - 2
                // (x - 2) - 2
                else if (sub.FirstMathExpression is Subtraction && sub.SecondMathExpression is Number)
                {
                    Subtraction bracketSub = sub.FirstMathExpression as Subtraction;
                    Number firstNumber = sub.SecondMathExpression as Number;

                    if (bracketSub.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.FirstMathExpression as Number;
                        Subtraction result = new Subtraction(new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketSub.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketSub.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.SecondMathExpression as Number;
                        Subtraction result = new Subtraction(bracketSub.FirstMathExpression, new Number(firstNumber.Calculate(null) + secondNumber.Calculate(null)));

                        return _SimplifyExpressions(result);
                    }
                }
                // 2 - (2 - x)
                // 2 - (x - 2)
                else if (sub.SecondMathExpression is Subtraction && sub.FirstMathExpression is Number)
                {
                    Subtraction bracketSub = sub.SecondMathExpression as Subtraction;
                    Number firstNumber = sub.FirstMathExpression as Number;

                    if (bracketSub.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.FirstMathExpression as Number;
                        Addition result = new Addition(new Number(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketSub.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketSub.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketSub.SecondMathExpression as Number;
                        Subtraction result = new Subtraction(new Number(firstNumber.Calculate(null) + secondNumber.Calculate(null)), bracketSub.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
            }
            else if (expression is Multiplication)
            {
                Multiplication mul = expression as Multiplication;

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
                Multiplication bracketMul = null;
                Number firstNumber = null;
                if (mul.FirstMathExpression is Multiplication && mul.SecondMathExpression is Number)
                {
                    bracketMul = mul.FirstMathExpression as Multiplication;
                    firstNumber = mul.SecondMathExpression as Number;
                }
                else if (mul.SecondMathExpression is Multiplication && mul.FirstMathExpression is Number)
                {
                    bracketMul = mul.SecondMathExpression as Multiplication;
                    firstNumber = mul.FirstMathExpression as Number;
                }
                if (bracketMul != null)
                {
                    if (bracketMul.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.FirstMathExpression as Number;
                        Multiplication result = new Multiplication(new Number(firstNumber.Value * secondNumber.Value), bracketMul.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketMul.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.SecondMathExpression as Number;
                        Multiplication result = new Multiplication(new Number(firstNumber.Value * secondNumber.Value), bracketMul.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }

                // 2 * (2 / x)
                // 2 * (x / 2)
                // (2 / x) * 2
                // (x / 2) * 2
                Division bracketDiv = null;
                if (mul.FirstMathExpression is Division && mul.SecondMathExpression is Number)
                {
                    bracketDiv = mul.FirstMathExpression as Division;
                    firstNumber = mul.SecondMathExpression as Number;
                }
                else if (mul.SecondMathExpression is Division && mul.FirstMathExpression is Number)
                {
                    bracketDiv = mul.SecondMathExpression as Division;
                    firstNumber = mul.FirstMathExpression as Number;
                }
                if (bracketDiv != null)
                {
                    if (bracketDiv.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.FirstMathExpression as Number;
                        Division result = new Division(new Number(firstNumber.Value * secondNumber.Value), bracketDiv.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketDiv.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.SecondMathExpression as Number;
                        Multiplication result = new Multiplication(new Number(firstNumber.Value / secondNumber.Value), bracketDiv.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
            }
            else if (expression is Division)
            {
                Division div = expression as Division;

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
                if (div.FirstMathExpression is Multiplication && div.SecondMathExpression is Number)
                {
                    Multiplication bracketMul = div.FirstMathExpression as Multiplication;
                    Number firstNumber = div.SecondMathExpression as Number;

                    if (bracketMul.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.FirstMathExpression as Number;
                        Multiplication result = new Multiplication(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketMul.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.SecondMathExpression as Number;
                        Multiplication result = new Multiplication(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
                // 2 / (2 * x)
                // 2 / (x * 2)
                else if (div.SecondMathExpression is Multiplication && div.FirstMathExpression is Number)
                {
                    Multiplication bracketMul = div.SecondMathExpression as Multiplication;
                    Number firstNumber = div.FirstMathExpression as Number;

                    if (bracketMul.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.FirstMathExpression as Number;
                        Division result = new Division(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketMul.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketMul.SecondMathExpression as Number;
                        Division result = new Division(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
                // (2 / x) / 2
                // (x / 2) / 2
                else if (div.FirstMathExpression is Division && div.SecondMathExpression is Number)
                {
                    Division bracketDiv = div.FirstMathExpression as Division;
                    Number firstNumber = div.SecondMathExpression as Number;

                    if (bracketDiv.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.FirstMathExpression as Number;
                        Division result = new Division(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketDiv.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketDiv.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.SecondMathExpression as Number;
                        Division result = new Division(bracketDiv.FirstMathExpression, new Number(firstNumber.Calculate(null) * secondNumber.Calculate(null)));

                        return _SimplifyExpressions(result);
                    }
                }
                // 2 / (2 / x)
                // 2 / (x / 2)
                else if (div.SecondMathExpression is Division && div.FirstMathExpression is Number)
                {
                    Division bracketDiv = div.SecondMathExpression as Division;
                    Number firstNumber = div.FirstMathExpression as Number;

                    if (bracketDiv.FirstMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.FirstMathExpression as Number;
                        Multiplication result = new Multiplication(new Number(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketDiv.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketDiv.SecondMathExpression is Number)
                    {
                        Number secondNumber = bracketDiv.SecondMathExpression as Number;
                        Division result = new Division(new Number(firstNumber.Calculate(null) * secondNumber.Calculate(null)), bracketDiv.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
            }
            else if (expression is Exponentiation)
            {
                Exponentiation inv = expression as Exponentiation;

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
                if (ln.FirstMathExpression.Equals(new Variable('e')))
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

        public static IMathExpression Differentiation(IMathExpression expression)
        {
            return Differentiation(expression, new Variable('x'));
        }

        public static IMathExpression Differentiation(IMathExpression expression, Variable variable)
        {
            return SimplifyExpressions(expression.Differentiation(variable));
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
                IEnumerable<MathToken> tokens = lexer.Tokenize(function);
                IEnumerable<MathToken> rpn = ConvertToReversePolishNotation(tokens);
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
                return SimplifyExpressions(mathExpression);

            return mathExpression;
        }

        private IEnumerable<IMathExpression> ConvertTokensToExpressions(IEnumerable<MathToken> tokens)
        {
            List<IMathExpression> preOutput = new List<IMathExpression>();

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case MathTokenType.Number:
                        preOutput.Add(new Number(token.Number));
                        break;
                    case MathTokenType.Variable:
                        preOutput.Add(new Variable(token.Variable));
                        break;
                    case MathTokenType.UnaryMinus:
                        preOutput.Add(new UnaryMinus());
                        break;
                    case MathTokenType.E:
                        preOutput.Add(new Exponential());
                        break;
                    case MathTokenType.Addition:
                        preOutput.Add(new Addition());
                        break;
                    case MathTokenType.Subtraction:
                        preOutput.Add(new Subtraction());
                        break;
                    case MathTokenType.Multiplication:
                        preOutput.Add(new Multiplication());
                        break;
                    case MathTokenType.Division:
                        preOutput.Add(new Division());
                        break;
                    case MathTokenType.Exponentiation:
                        preOutput.Add(new Exponentiation());
                        break;
                    case MathTokenType.Absolute:
                        preOutput.Add(new Absolute());
                        break;
                    case MathTokenType.Sine:
                        preOutput.Add(new Sine { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Cosine:
                        preOutput.Add(new Cosine { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Tangent:
                        preOutput.Add(new Tangent { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Cotangent:
                        preOutput.Add(new Cotangent { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Secant:
                        preOutput.Add(new Secant { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Cosecant:
                        preOutput.Add(new Cosecant { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Arcsine:
                        preOutput.Add(new Arcsin { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Arccosine:
                        preOutput.Add(new Arccos { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Arctangent:
                        preOutput.Add(new Arctan { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Arcsecant:
                        preOutput.Add(new Arcsec { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Arccosecant:
                        preOutput.Add(new Arccsc { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Arccotangent:
                        preOutput.Add(new Arccot { AngleMeasurement = angleMeasurement });
                        break;
                    case MathTokenType.Sineh:
                        preOutput.Add(new HyperbolicSine());
                        break;
                    case MathTokenType.Cosineh:
                        preOutput.Add(new HyperbolicCosine());
                        break;
                    case MathTokenType.Tangenth:
                        preOutput.Add(new HyperbolicTangent());
                        break;
                    case MathTokenType.Cotangenth:
                        preOutput.Add(new HyperbolicCotangent());
                        break;
                    case MathTokenType.Secanth:
                        preOutput.Add(new HyperbolicSecant());
                        break;
                    case MathTokenType.Cosecanth:
                        preOutput.Add(new HyperbolicCosecant());
                        break;
                    case MathTokenType.Arsineh:
                        preOutput.Add(new HyperbolicArsine());
                        break;
                    case MathTokenType.Arcosineh:
                        preOutput.Add(new HyperbolicArcosine());
                        break;
                    case MathTokenType.Artangenth:
                        preOutput.Add(new HyperbolicArtangent());
                        break;
                    case MathTokenType.Arcotangenth:
                        preOutput.Add(new HyperbolicArcotangent());
                        break;
                    case MathTokenType.Arsecanth:
                        preOutput.Add(new HyperbolicArsecant());
                        break;
                    case MathTokenType.Arcosecanth:
                        preOutput.Add(new HyperbolicArcosecant());
                        break;
                    case MathTokenType.Sqrt:
                        preOutput.Add(new Sqrt());
                        break;
                    case MathTokenType.Root:
                        preOutput.Add(new Root());
                        break;
                    case MathTokenType.Lg:
                        preOutput.Add(new Lg());
                        break;
                    case MathTokenType.Ln:
                        preOutput.Add(new Ln());
                        break;
                    case MathTokenType.Log:
                        preOutput.Add(new Log());
                        break;
                    case MathTokenType.Derivative:
                        preOutput.Add(new Derivative());
                        break;
                    case MathTokenType.Assign:
                        preOutput.Add(new Assign());
                        break;
                    case MathTokenType.Not:
                        preOutput.Add(new Not());
                        break;
                    case MathTokenType.And:
                        preOutput.Add(new And());
                        break;
                    case MathTokenType.Or:
                        preOutput.Add(new Or());
                        break;
                    case MathTokenType.XOr:
                        preOutput.Add(new XOr());
                        break;
                    default:
                        throw new MathParserException(Resource.NotSupportedToken);
                }
            }

            return preOutput;
        }

        private IEnumerable<MathToken> ConvertToReversePolishNotation(IEnumerable<MathToken> tokens)
        {
            List<MathToken> output = new List<MathToken>();
            Stack<MathToken> stack = new Stack<MathToken>();

            foreach (var token in tokens)
            {
                MathToken stackToken;
                if (token.Type == MathTokenType.OpenBracket)
                {
                    stack.Push(token);
                }
                else if (token.Type == MathTokenType.CloseBracket)
                {
                    stackToken = stack.Pop();
                    while (stackToken.Type != MathTokenType.OpenBracket)
                    {
                        output.Add(stackToken);
                        stackToken = stack.Pop();
                    }
                }
                else if (token.Type == MathTokenType.Comma)
                {
                    stackToken = stack.Pop();
                    while (stackToken.Type != MathTokenType.OpenBracket)
                    {
                        output.Add(stackToken);
                        stackToken = stack.Pop();
                    }

                    if (stackToken.Type == MathTokenType.OpenBracket)
                        stack.Push(stackToken);
                }
                else if (token.Type == MathTokenType.Number || token.Type == MathTokenType.Variable)
                {
                    output.Add(token);
                }
                else
                {
                    while (stack.Count != 0 && (stackToken = stack.Peek()).Type >= token.Type)
                    {
                        if (stackToken.Type == MathTokenType.OpenBracket)
                            break;
                        output.Add(stack.Pop());
                    }

                    stack.Push(token);
                }
            }
            if (stack.Count != 0)
            {
                output.AddRange(stack);
            }

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
