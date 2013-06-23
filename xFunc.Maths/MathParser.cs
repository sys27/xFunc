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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Bitwise;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    public class MathParser
    {

        private ILexer lexer;
        private ISimplifier simplifier;
        private IDifferentiator differentiator;

        private string lastFunc = string.Empty;
        private IMathExpression mathExpression;

        private AngleMeasurement angleMeasurement;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParser"/> class with default implementations of <see cref="ILexer"/>, <see cref="ISimplifier"/> and <see cref="IDifferentiator"/>.
        /// </summary>
        public MathParser()
        {
            this.lexer = new MathLexer();
            this.simplifier = new MathSimplifier();
            this.differentiator = new MathDifferentiator(this.simplifier);
        }

        public MathParser(ILexer lexer, ISimplifier simplifier, IDifferentiator differentiator)
        {
            this.lexer = lexer;
            this.simplifier = simplifier;
            this.differentiator = differentiator;
        }

        /// <summary>
        /// Checks the <paramref name="expression"/> parameter has <paramref name="arg"/>.
        /// </summary>
        /// <param name="expression">A expression that is checked.</param>
        /// <param name="arg">A variable that can be contained in the expression.</param>
        /// <returns>true if <paramref name="expression"/> has <paramref name="arg"/>; otherwise, false.</returns>
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

        public IMathExpression Simplify(IMathExpression expression)
        {
            return simplifier.Simplify(expression);
        }

        public IMathExpression Differentiate(IMathExpression expression)
        {
            return differentiator.Differentiate(expression);
        }

        public IMathExpression Differentiate(IMathExpression expression, Variable variable)
        {
            return differentiator.Differentiate(expression, variable);
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

                        var assign = expression as Assign;
                        assign.Value = stack.Pop();

                        var peek = stack.Peek();
                        if (!(peek is Variable || peek is UserFunction))
                            throw new MathParserException(Resource.InvalidExpression);

                        assign.Key = stack.Pop();

                        stack.Push(assign);
                    }
                    else if (expression is Undefine)
                    {
                        if (stack.Count < 1)
                            throw new MathParserException(Resource.InvalidNumberOfVariables);

                        var undef = expression as Undefine;

                        var peek = stack.Peek();
                        if (!(peek is Variable || peek is UserFunction))
                            throw new MathParserException(Resource.InvalidExpression);

                        undef.Key = stack.Pop();

                        stack.Push(undef);
                    }
                    else if (expression is UserFunction)
                    {
                        var func = expression as UserFunction;

                        IMathExpression[] arg = new IMathExpression[func.CountOfParams];
                        for (int i = func.CountOfParams, j = 0; i > 0; i--, j++)
                        {
                            arg[j] = stack.Pop();
                        }
                        func.Arguments = arg;

                        stack.Push(func);
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
                return simplifier.Simplify(mathExpression);

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
                        case Functions.GCD:
                            preOutput.Add(new GCD());
                            break;
                        case Functions.LCM:
                            preOutput.Add(new LCM());
                            break;
                        case Functions.Derivative:
                            preOutput.Add(new Derivative());
                            break;
                        case Functions.Undefine:
                            preOutput.Add(new Undefine());
                            break;
                    }
                }
                else if (token is UserFunctionToken)
                {
                    var t = token as UserFunctionToken;
                    preOutput.Add(new UserFunction(t.Function, t.CountOfParams));
                }
            }

            return preOutput;
        }

        private IEnumerable<IToken> ConvertToReversePolishNotation(IEnumerable<IToken> tokens)
        {
            var output = new List<IToken>();
            var stack = new Stack<IToken>();

            var openBracketToken = new SymbolToken(Symbols.OpenBracket);
            foreach (var token in tokens)
            {
                IToken stackToken;
                if (token is SymbolToken)
                {
                    var t = token as SymbolToken;
                    if (t.Symbol == Symbols.OpenBracket)
                    {
                        stack.Push(token);
                    }
                    else if (t.Symbol == Symbols.CloseBracket)
                    {
                        stackToken = stack.Pop();
                        while (!stackToken.Equals(openBracketToken))
                        {
                            output.Add(stackToken);
                            stackToken = stack.Pop();
                        }
                    }
                    else if (t.Symbol == Symbols.Comma)
                    {
                        stackToken = stack.Pop();

                        while (!stackToken.Equals(openBracketToken))
                        {
                            output.Add(stackToken);
                            stackToken = stack.Pop();
                        }

                        stack.Push(stackToken);
                    }
                }
                else if (token is NumberToken || token is VariableToken)
                {
                    output.Add(token);
                }
                else
                {
                    while (stack.Count != 0 && (stackToken = stack.Peek()).Priority >= token.Priority)
                    {
                        if (stackToken.Equals(openBracketToken))
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

        public ILexer Lexer
        {
            get
            {
                return lexer;
            }
            set
            {
                lexer = value;
            }
        }

        public ISimplifier Simplifier
        {
            get
            {
                return simplifier;
            }
            set
            {
                simplifier = value;
            }
        }

        /// <summary>
        /// Gets or Sets a measurement of angles.
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
