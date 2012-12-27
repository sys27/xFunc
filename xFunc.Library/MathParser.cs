using System;
using System.Collections.Generic;
using xFunc.Library.Exceptions;
using xFunc.Library.Expressions.Maths;
using xFunc.Library.Resources;

namespace xFunc.Library
{

    public class MathParser
    {

        private MathLexer lexer;

        private string lastFunc = string.Empty;
        private IMathExpression mathExpression;

        private AngleMeasurement angleMeasurement;

        public MathParser()
        {
            lexer = new MathLexer();
        }

        public static IMathExpression SimplifyExpressions(IMathExpression expression)
        {
            IMathExpression exp = _SimplifyExpressions(expression);
            exp.Parent = null;

            return exp;
        }

        private static IMathExpression _SimplifyExpressions(IMathExpression expression)
        {
            if (expression is NumberMathExpression)
                return expression;
            if (expression is VariableMathExpression)
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

            NumberMathExpression zero = new NumberMathExpression(0);
            NumberMathExpression one = new NumberMathExpression(1);

            if (expression is UnaryMinusMathExpression)
            {
                UnaryMinusMathExpression unMinus = expression as UnaryMinusMathExpression;
                // -(-x)
                if (unMinus.FirstMathExpression is UnaryMinusMathExpression)
                    return ((UnaryMinusMathExpression)unMinus.FirstMathExpression).FirstMathExpression;
                // -1
                if (unMinus.FirstMathExpression is NumberMathExpression)
                {
                    NumberMathExpression number = unMinus.FirstMathExpression as NumberMathExpression;
                    number.Number = -number.Number;

                    return number;
                }
            }
            else if (expression is AdditionMathExpression)
            {
                AdditionMathExpression add = expression as AdditionMathExpression;

                // plus zero
                if (add.FirstMathExpression.Equals(zero))
                    return add.SecondMathExpression;
                if (add.SecondMathExpression.Equals(zero))
                    return add.FirstMathExpression;

                if (add.FirstMathExpression is NumberMathExpression && add.SecondMathExpression is NumberMathExpression)
                    return new NumberMathExpression(add.Calculate(null));

                if (add.FirstMathExpression is UnaryMinusMathExpression)
                {
                    IMathExpression temp = add.FirstMathExpression;
                    add.FirstMathExpression = add.SecondMathExpression;
                    add.SecondMathExpression = temp;
                }
                if (add.SecondMathExpression is UnaryMinusMathExpression)
                {
                    UnaryMinusMathExpression unMinus = add.SecondMathExpression as UnaryMinusMathExpression;
                    SubtractionMathExpression sub = new SubtractionMathExpression(add.FirstMathExpression, unMinus.FirstMathExpression);

                    return sub;
                }

                // 2 + (2 + x)
                // 2 + (x + 2)
                // (2 + x) + 2
                // (x + 2) + 2
                AdditionMathExpression bracketAdd = null;
                NumberMathExpression firstNumber = null;
                if (add.FirstMathExpression is AdditionMathExpression && add.SecondMathExpression is NumberMathExpression)
                {
                    bracketAdd = add.FirstMathExpression as AdditionMathExpression;
                    firstNumber = add.SecondMathExpression as NumberMathExpression;
                }
                else if (add.SecondMathExpression is AdditionMathExpression && add.FirstMathExpression is NumberMathExpression)
                {
                    bracketAdd = add.SecondMathExpression as AdditionMathExpression;
                    firstNumber = add.FirstMathExpression as NumberMathExpression;
                }
                if (bracketAdd != null)
                {
                    if (bracketAdd.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketAdd.FirstMathExpression as NumberMathExpression;
                        AdditionMathExpression result = new AdditionMathExpression(bracketAdd.SecondMathExpression, new NumberMathExpression(firstNumber.Number + secondNumber.Number));

                        return _SimplifyExpressions(result);
                    }
                    if (bracketAdd.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketAdd.SecondMathExpression as NumberMathExpression;
                        AdditionMathExpression result = new AdditionMathExpression(bracketAdd.FirstMathExpression, new NumberMathExpression(firstNumber.Number + secondNumber.Number));

                        return _SimplifyExpressions(result);
                    }
                }

                // 2 + (2 - x)
                // 2 + (x - 2)
                // (2 - x) + 2
                // (x - 2) + 2
                SubtractionMathExpression bracketSub = null;
                if (add.FirstMathExpression is SubtractionMathExpression && add.SecondMathExpression is NumberMathExpression)
                {
                    bracketSub = add.FirstMathExpression as SubtractionMathExpression;
                    firstNumber = add.SecondMathExpression as NumberMathExpression;
                }
                else if (add.SecondMathExpression is SubtractionMathExpression && add.FirstMathExpression is NumberMathExpression)
                {
                    bracketSub = add.SecondMathExpression as SubtractionMathExpression;
                    firstNumber = add.FirstMathExpression as NumberMathExpression;
                }
                if (bracketSub != null)
                {
                    if (bracketSub.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketSub.FirstMathExpression as NumberMathExpression;
                        SubtractionMathExpression result = new SubtractionMathExpression(new NumberMathExpression(firstNumber.Number + secondNumber.Number), bracketSub.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketSub.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketSub.SecondMathExpression as NumberMathExpression;
                        AdditionMathExpression result = new AdditionMathExpression(new NumberMathExpression(firstNumber.Number - secondNumber.Number), bracketSub.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
            }
            else if (expression is SubtractionMathExpression)
            {
                SubtractionMathExpression sub = expression as SubtractionMathExpression;

                // sub zero
                if (sub.FirstMathExpression.Equals(zero))
                    return _SimplifyExpressions(new UnaryMinusMathExpression(sub.SecondMathExpression));
                if (sub.SecondMathExpression.Equals(zero))
                    return sub.FirstMathExpression;

                if (sub.FirstMathExpression is NumberMathExpression && sub.SecondMathExpression is NumberMathExpression)
                    return new NumberMathExpression(sub.Calculate(null));

                if (sub.SecondMathExpression is UnaryMinusMathExpression)
                {
                    UnaryMinusMathExpression unMinus = sub.SecondMathExpression as UnaryMinusMathExpression;
                    AdditionMathExpression add = new AdditionMathExpression(sub.FirstMathExpression, unMinus.FirstMathExpression);

                    return add;
                }

                // (2 + x) - 2
                // (x + 2) - 2
                if (sub.FirstMathExpression is AdditionMathExpression && sub.SecondMathExpression is NumberMathExpression)
                {
                    AdditionMathExpression bracketAdd = sub.FirstMathExpression as AdditionMathExpression;
                    NumberMathExpression firstNumber = sub.SecondMathExpression as NumberMathExpression;

                    if (bracketAdd.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketAdd.FirstMathExpression as NumberMathExpression;
                        AdditionMathExpression result = new AdditionMathExpression(bracketAdd.SecondMathExpression, new NumberMathExpression(firstNumber.Calculate(null) - secondNumber.Calculate(null)));

                        return _SimplifyExpressions(result);
                    }
                    if (bracketAdd.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketAdd.SecondMathExpression as NumberMathExpression;
                        AdditionMathExpression result = new AdditionMathExpression(bracketAdd.FirstMathExpression, new NumberMathExpression(firstNumber.Calculate(null) - secondNumber.Calculate(null)));

                        return _SimplifyExpressions(result);
                    }
                }
                // 2 - (2 + x)
                // 2 - (x + 2)
                else if (sub.SecondMathExpression is AdditionMathExpression && sub.FirstMathExpression is NumberMathExpression)
                {
                    AdditionMathExpression bracketAdd = sub.SecondMathExpression as AdditionMathExpression;
                    NumberMathExpression firstNumber = sub.FirstMathExpression as NumberMathExpression;

                    if (bracketAdd.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketAdd.FirstMathExpression as NumberMathExpression;
                        SubtractionMathExpression result = new SubtractionMathExpression(new NumberMathExpression(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketAdd.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketAdd.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketAdd.SecondMathExpression as NumberMathExpression;
                        SubtractionMathExpression result = new SubtractionMathExpression(new NumberMathExpression(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketAdd.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
                // (2 - x) - 2
                // (x - 2) - 2
                else if (sub.FirstMathExpression is SubtractionMathExpression && sub.SecondMathExpression is NumberMathExpression)
                {
                    SubtractionMathExpression bracketSub = sub.FirstMathExpression as SubtractionMathExpression;
                    NumberMathExpression firstNumber = sub.SecondMathExpression as NumberMathExpression;

                    if (bracketSub.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketSub.FirstMathExpression as NumberMathExpression;
                        SubtractionMathExpression result = new SubtractionMathExpression(new NumberMathExpression(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketSub.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketSub.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketSub.SecondMathExpression as NumberMathExpression;
                        SubtractionMathExpression result = new SubtractionMathExpression(bracketSub.FirstMathExpression, new NumberMathExpression(firstNumber.Calculate(null) + secondNumber.Calculate(null)));

                        return _SimplifyExpressions(result);
                    }
                }
                // 2 - (2 - x)
                // 2 - (x - 2)
                else if (sub.SecondMathExpression is SubtractionMathExpression && sub.FirstMathExpression is NumberMathExpression)
                {
                    SubtractionMathExpression bracketSub = sub.SecondMathExpression as SubtractionMathExpression;
                    NumberMathExpression firstNumber = sub.FirstMathExpression as NumberMathExpression;

                    if (bracketSub.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketSub.FirstMathExpression as NumberMathExpression;
                        AdditionMathExpression result = new AdditionMathExpression(new NumberMathExpression(firstNumber.Calculate(null) - secondNumber.Calculate(null)), bracketSub.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketSub.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketSub.SecondMathExpression as NumberMathExpression;
                        SubtractionMathExpression result = new SubtractionMathExpression(new NumberMathExpression(firstNumber.Calculate(null) + secondNumber.Calculate(null)), bracketSub.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
            }
            else if (expression is MultiplicationMathExpression)
            {
                MultiplicationMathExpression mul = expression as MultiplicationMathExpression;

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
                MultiplicationMathExpression bracketMul = null;
                NumberMathExpression firstNumber = null;
                if (mul.FirstMathExpression is MultiplicationMathExpression && mul.SecondMathExpression is NumberMathExpression)
                {
                    bracketMul = mul.FirstMathExpression as MultiplicationMathExpression;
                    firstNumber = mul.SecondMathExpression as NumberMathExpression;
                }
                else if (mul.SecondMathExpression is MultiplicationMathExpression && mul.FirstMathExpression is NumberMathExpression)
                {
                    bracketMul = mul.SecondMathExpression as MultiplicationMathExpression;
                    firstNumber = mul.FirstMathExpression as NumberMathExpression;
                }
                if (bracketMul != null)
                {
                    if (bracketMul.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketMul.FirstMathExpression as NumberMathExpression;
                        MultiplicationMathExpression result = new MultiplicationMathExpression(new NumberMathExpression(firstNumber.Number * secondNumber.Number), bracketMul.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketMul.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketMul.SecondMathExpression as NumberMathExpression;
                        MultiplicationMathExpression result = new MultiplicationMathExpression(new NumberMathExpression(firstNumber.Number * secondNumber.Number), bracketMul.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }

                // 2 * (2 / x)
                // 2 * (x / 2)
                // (2 / x) * 2
                // (x / 2) * 2
                DivisionMathExpression bracketDiv = null;
                if (mul.FirstMathExpression is DivisionMathExpression && mul.SecondMathExpression is NumberMathExpression)
                {
                    bracketDiv = mul.FirstMathExpression as DivisionMathExpression;
                    firstNumber = mul.SecondMathExpression as NumberMathExpression;
                }
                else if (mul.SecondMathExpression is DivisionMathExpression && mul.FirstMathExpression is NumberMathExpression)
                {
                    bracketDiv = mul.SecondMathExpression as DivisionMathExpression;
                    firstNumber = mul.FirstMathExpression as NumberMathExpression;
                }
                if (bracketDiv != null)
                {
                    if (bracketDiv.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketDiv.FirstMathExpression as NumberMathExpression;
                        DivisionMathExpression result = new DivisionMathExpression(new NumberMathExpression(firstNumber.Number * secondNumber.Number), bracketDiv.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketDiv.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketDiv.SecondMathExpression as NumberMathExpression;
                        MultiplicationMathExpression result = new MultiplicationMathExpression(new NumberMathExpression(firstNumber.Number / secondNumber.Number), bracketDiv.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
            }
            else if (expression is DivisionMathExpression)
            {
                DivisionMathExpression div = expression as DivisionMathExpression;

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
                if (div.FirstMathExpression is MultiplicationMathExpression && div.SecondMathExpression is NumberMathExpression)
                {
                    MultiplicationMathExpression bracketMul = div.FirstMathExpression as MultiplicationMathExpression;
                    NumberMathExpression firstNumber = div.SecondMathExpression as NumberMathExpression;

                    if (bracketMul.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketMul.FirstMathExpression as NumberMathExpression;
                        MultiplicationMathExpression result = new MultiplicationMathExpression(new NumberMathExpression(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketMul.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketMul.SecondMathExpression as NumberMathExpression;
                        MultiplicationMathExpression result = new MultiplicationMathExpression(new NumberMathExpression(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
                // 2 / (2 * x)
                // 2 / (x * 2)
                else if (div.SecondMathExpression is MultiplicationMathExpression && div.FirstMathExpression is NumberMathExpression)
                {
                    MultiplicationMathExpression bracketMul = div.SecondMathExpression as MultiplicationMathExpression;
                    NumberMathExpression firstNumber = div.FirstMathExpression as NumberMathExpression;

                    if (bracketMul.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketMul.FirstMathExpression as NumberMathExpression;
                        DivisionMathExpression result = new DivisionMathExpression(new NumberMathExpression(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketMul.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketMul.SecondMathExpression as NumberMathExpression;
                        DivisionMathExpression result = new DivisionMathExpression(new NumberMathExpression(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketMul.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
                // (2 / x) / 2
                // (x / 2) / 2
                else if (div.FirstMathExpression is DivisionMathExpression && div.SecondMathExpression is NumberMathExpression)
                {
                    DivisionMathExpression bracketDiv = div.FirstMathExpression as DivisionMathExpression;
                    NumberMathExpression firstNumber = div.SecondMathExpression as NumberMathExpression;

                    if (bracketDiv.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketDiv.FirstMathExpression as NumberMathExpression;
                        DivisionMathExpression result = new DivisionMathExpression(new NumberMathExpression(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketDiv.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketDiv.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketDiv.SecondMathExpression as NumberMathExpression;
                        DivisionMathExpression result = new DivisionMathExpression(bracketDiv.FirstMathExpression, new NumberMathExpression(firstNumber.Calculate(null) * secondNumber.Calculate(null)));

                        return _SimplifyExpressions(result);
                    }
                }
                // 2 / (2 / x)
                // 2 / (x / 2)
                else if (div.SecondMathExpression is DivisionMathExpression && div.FirstMathExpression is NumberMathExpression)
                {
                    DivisionMathExpression bracketDiv = div.SecondMathExpression as DivisionMathExpression;
                    NumberMathExpression firstNumber = div.FirstMathExpression as NumberMathExpression;

                    if (bracketDiv.FirstMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketDiv.FirstMathExpression as NumberMathExpression;
                        MultiplicationMathExpression result = new MultiplicationMathExpression(new NumberMathExpression(firstNumber.Calculate(null) / secondNumber.Calculate(null)), bracketDiv.SecondMathExpression);

                        return _SimplifyExpressions(result);
                    }
                    if (bracketDiv.SecondMathExpression is NumberMathExpression)
                    {
                        NumberMathExpression secondNumber = bracketDiv.SecondMathExpression as NumberMathExpression;
                        DivisionMathExpression result = new DivisionMathExpression(new NumberMathExpression(firstNumber.Calculate(null) * secondNumber.Calculate(null)), bracketDiv.FirstMathExpression);

                        return _SimplifyExpressions(result);
                    }
                }
            }
            else if (expression is ExponentiationMathExpression)
            {
                ExponentiationMathExpression inv = expression as ExponentiationMathExpression;

                // x^0
                if (inv.SecondMathExpression.Equals(zero))
                    return one;
                // x^1
                if (inv.SecondMathExpression.Equals(one))
                    return inv.FirstMathExpression;
            }

            return expression;
        }

        public static IMathExpression Derivative(IMathExpression expression)
        {
            return Derivative(expression, new VariableMathExpression('x'));
        }

        public static IMathExpression Derivative(IMathExpression expression, VariableMathExpression variable)
        {
            return SimplifyExpressions(expression.Derivative(variable));
        }

        public static bool HaveVar(IMathExpression expression, VariableMathExpression arg)
        {
            if (expression is BinaryMathExpression)
            {
                BinaryMathExpression bin = expression as BinaryMathExpression;
                if (HaveVar(bin.FirstMathExpression, arg))
                    return true;
                else
                    return HaveVar(bin.SecondMathExpression, arg);
            }
            else if (expression is UnaryMathExpression)
            {
                UnaryMathExpression un = expression as UnaryMathExpression;

                return HaveVar(un.FirstMathExpression, arg);
            }
            else if (expression is VariableMathExpression)
            {
                if (expression.Equals(arg))
                    return true;
            }

            return false;
        }

        public IMathExpression Parse(string function)
        {
            if (function != lastFunc)
            {
                IEnumerable<Token> tokens = lexer.MathTokenization(function);
                IEnumerable<Token> rpn = ConvertToReversePolishNotation(tokens);
                IEnumerable<IMathExpression> expressions = ConvertTokensToExpressions(rpn);

                Stack<IMathExpression> stack = new Stack<IMathExpression>();
                foreach (var expression in expressions)
                {
                    if (expression is NumberMathExpression || expression is VariableMathExpression)
                    {
                        stack.Push(expression);
                    }
                    else if (expression is BinaryMathExpression)
                    {
                        if ((expression is LogMathExpression || expression is RootMathExpression) && stack.Count < 2)
                            throw new ParserException(Resource.InvalidNumberOfVariables);

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
                    else if (expression is DerivativeMathExpression)
                    {
                        if (!(stack.Peek() is VariableMathExpression))
                            throw new ParserException(Resource.InvalidExpression);

                        DerivativeMathExpression binExp = (DerivativeMathExpression)expression;
                        binExp.Variable = (VariableMathExpression)stack.Pop();
                        binExp.FirstMathExpression = stack.Pop();

                        stack.Push(binExp);
                    }
                    else
                    {
                        throw new ParserException(Resource.UnexpectedError);
                    }
                }

                if (stack.Count > 1)
                    throw new ParserException(Resource.ErrorWhileParsingTree);

                lastFunc = function;
                mathExpression = stack.Pop();
            }

            return SimplifyExpressions(mathExpression);
        }

        private IEnumerable<IMathExpression> ConvertTokensToExpressions(IEnumerable<Token> tokens)
        {
            List<IMathExpression> preOutput = new List<IMathExpression>();

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                        preOutput.Add(new NumberMathExpression(token.Number));
                        break;
                    case TokenType.Variable:
                        preOutput.Add(new VariableMathExpression(token.Variable));
                        break;
                    case TokenType.UnaryMinus:
                        preOutput.Add(new UnaryMinusMathExpression());
                        break;
                    case TokenType.E:
                        preOutput.Add(new ExpMathExpression());
                        break;
                    case TokenType.Addition:
                        preOutput.Add(new AdditionMathExpression());
                        break;
                    case TokenType.Subtraction:
                        preOutput.Add(new SubtractionMathExpression());
                        break;
                    case TokenType.Multiplication:
                        preOutput.Add(new MultiplicationMathExpression());
                        break;
                    case TokenType.Division:
                        preOutput.Add(new DivisionMathExpression());
                        break;
                    case TokenType.Exponentiation:
                        preOutput.Add(new ExponentiationMathExpression());
                        break;
                    case TokenType.Absolute:
                        preOutput.Add(new AbsoluteMathExpression());
                        break;
                    case TokenType.Sine:
                        preOutput.Add(new SineMathExpression { AngleMeasurement = angleMeasurement });
                        break;
                    case TokenType.Cosine:
                        preOutput.Add(new CosineMathExpression { AngleMeasurement = angleMeasurement });
                        break;
                    case TokenType.Tangent:
                        preOutput.Add(new TangentMathExpression { AngleMeasurement = angleMeasurement });
                        break;
                    case TokenType.Cotangent:
                        preOutput.Add(new CotangentMathExpression { AngleMeasurement = angleMeasurement });
                        break;
                    case TokenType.Arcsin:
                        preOutput.Add(new ArcsinMathExpression { AngleMeasurement = angleMeasurement });
                        break;
                    case TokenType.Arccos:
                        preOutput.Add(new ArccosMathExpression { AngleMeasurement = angleMeasurement });
                        break;
                    case TokenType.Arctan:
                        preOutput.Add(new ArctanMathExpression { AngleMeasurement = angleMeasurement });
                        break;
                    case TokenType.Arccot:
                        preOutput.Add(new ArccotMathExpression { AngleMeasurement = angleMeasurement });
                        break;
                    case TokenType.Sqrt:
                        preOutput.Add(new SqrtMathExpression());
                        break;
                    case TokenType.Root:
                        preOutput.Add(new RootMathExpression());
                        break;
                    case TokenType.Lg:
                        preOutput.Add(new LgMathExpression());
                        break;
                    case TokenType.Ln:
                        preOutput.Add(new LnMathExpression());
                        break;
                    case TokenType.Log:
                        preOutput.Add(new LogMathExpression());
                        break;
                    case TokenType.Plot:
                        preOutput.Add(new PlotMathExpression());
                        break;
                    case TokenType.Derivative:
                        preOutput.Add(new DerivativeMathExpression());
                        break;
                    default:
                        throw new ParserException(Resource.NotSupportedToken);
                }
            }

            return preOutput;
        }

        private IEnumerable<Token> ConvertToReversePolishNotation(IEnumerable<Token> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException("tokens");

            List<Token> output = new List<Token>();
            Stack<Token> stack = new Stack<Token>();

            foreach (var token in tokens)
            {
                Token stackToken;
                if (token.Type == TokenType.OpenBracket)
                {
                    stack.Push(token);
                }
                else if (token.Type == TokenType.CloseBracket)
                {
                    stackToken = stack.Pop();
                    while (stackToken.Type != TokenType.OpenBracket)
                    {
                        output.Add(stackToken);
                        stackToken = stack.Pop();
                    }
                }
                else if (token.Type == TokenType.Comma)
                {
                    stackToken = stack.Pop();
                    while (stackToken.Type != TokenType.OpenBracket)
                    {
                        output.Add(stackToken);
                        stackToken = stack.Pop();
                    }

                    if (stackToken.Type == TokenType.OpenBracket)
                        stack.Push(stackToken);
                }
                else if (token.Type == TokenType.Addition ||
                         token.Type == TokenType.Subtraction ||
                         token.Type == TokenType.Multiplication ||
                         token.Type == TokenType.Division ||
                         token.Type == TokenType.Exponentiation ||
                         token.Type == TokenType.Absolute ||
                         token.Type == TokenType.Sine ||
                         token.Type == TokenType.Cosine ||
                         token.Type == TokenType.Tangent ||
                         token.Type == TokenType.Cotangent ||
                         token.Type == TokenType.Arcsin ||
                         token.Type == TokenType.Arccos ||
                         token.Type == TokenType.Arctan ||
                         token.Type == TokenType.Arccot ||
                         token.Type == TokenType.Sqrt ||
                         token.Type == TokenType.Root ||
                         token.Type == TokenType.Lg ||
                         token.Type == TokenType.Ln ||
                         token.Type == TokenType.Log ||
                         token.Type == TokenType.UnaryMinus ||
                         token.Type == TokenType.E ||
                         token.Type == TokenType.Plot ||
                         token.Type == TokenType.Derivative)
                {
                    while (stack.Count != 0 && (stackToken = stack.Peek()).Type >= token.Type)
                    {
                        if (stackToken.Type == TokenType.OpenBracket)
                            break;
                        output.Add(stack.Pop());
                    }

                    stack.Push(token);
                }
                else if (token.Type == TokenType.Number || token.Type == TokenType.Variable)
                {
                    output.Add(token);
                }
                else
                {
                    throw new ParserException(Resource.NotSupportedToken);
                }
            }
            if (stack.Count != 0)
            {
                output.AddRange(stack);
            }

            return output;
        }

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
