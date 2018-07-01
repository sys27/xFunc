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
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using xFunc.Maths;
using xFunc.Maths.Tokenization;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.Tokenization
{

    public class FunctionLexerTests : BaseLexerTests
    {

        [Fact]
        public void Exp()
        {
            FuncTest("exp", Functions.Exp);
        }

        [Fact]
        public void Abs()
        {
            FuncTest("abs", Functions.Absolute);
        }

        [Fact]
        public void Sh()
        {
            FuncTest("sh", Functions.Sineh);
        }

        [Fact]
        public void Sinh()
        {
            FuncTest("sinh", Functions.Sineh);
        }

        [Fact]
        public void Ch()
        {
            FuncTest("ch", Functions.Cosineh);
        }

        [Fact]
        public void Cosh()
        {
            FuncTest("cosh", Functions.Cosineh);
        }

        [Fact]
        public void Th()
        {
            FuncTest("th", Functions.Tangenth);
        }

        [Fact]
        public void Tanh()
        {
            FuncTest("tanh", Functions.Tangenth);
        }

        [Fact]
        public void Cth()
        {
            FuncTest("cth", Functions.Cotangenth);
        }

        [Fact]
        public void Coth()
        {
            FuncTest("coth", Functions.Cotangenth);
        }

        [Fact]
        public void Sech()
        {
            FuncTest("sech", Functions.Secanth);
        }

        [Fact]
        public void Csch()
        {
            FuncTest("csch", Functions.Cosecanth);
        }

        [Fact]
        public void Arsinh()
        {
            FuncTest("arsinh", Functions.Arsineh);
        }

        [Fact]
        public void Arsh()
        {
            FuncTest("arsh", Functions.Arsineh);
        }

        [Fact]
        public void Arcosh()
        {
            FuncTest("arcosh", Functions.Arcosineh);
        }

        [Fact]
        public void Arch()
        {
            FuncTest("arch", Functions.Arcosineh);
        }

        [Fact]
        public void Artanh()
        {
            FuncTest("artanh", Functions.Artangenth);
        }

        [Fact]
        public void Arth()
        {
            FuncTest("arth", Functions.Artangenth);
        }

        [Fact]
        public void Arcoth()
        {
            FuncTest("arcoth", Functions.Arcotangenth);
        }

        [Fact]
        public void Arcth()
        {
            FuncTest("arcth", Functions.Arcotangenth);
        }

        [Fact]
        public void Arsech()
        {
            FuncTest("arsech", Functions.Arsecanth);
        }

        [Fact]
        public void Arsch()
        {
            FuncTest("arsch", Functions.Arsecanth);
        }

        [Fact]
        public void Arcsch()
        {
            FuncTest("arcsch", Functions.Arcosecanth);
        }

        [Fact]
        public void Sin()
        {
            FuncTest("sin", Functions.Sine);
        }

        [Fact]
        public void Cosec()
        {
            FuncTest("cosec", Functions.Cosecant);
        }

        [Fact]
        public void Csc()
        {
            FuncTest("csc", Functions.Cosecant);
        }

        [Fact]
        public void Cos()
        {
            FuncTest("cos", Functions.Cosine);
        }

        [Fact]
        public void Tg()
        {
            FuncTest("tg", Functions.Tangent);
        }

        [Fact]
        public void Tan()
        {
            FuncTest("tan", Functions.Tangent);
        }

        [Fact]
        public void Ctg()
        {
            FuncTest("ctg", Functions.Cotangent);
        }

        [Fact]
        public void Cot()
        {
            FuncTest("cot", Functions.Cotangent);
        }

        [Fact]
        public void Sec()
        {
            FuncTest("sec", Functions.Secant);
        }

        [Fact]
        public void Arcsin()
        {
            FuncTest("arcsin", Functions.Arcsine);
        }

        [Fact]
        public void Arccosec()
        {
            FuncTest("arccosec", Functions.Arccosecant);
        }

        [Fact]
        public void Arccsc()
        {
            FuncTest("arccsc", Functions.Arccosecant);
        }

        [Fact]
        public void Arccos()
        {
            FuncTest("arccos", Functions.Arccosine);
        }

        [Fact]
        public void Arctg()
        {
            FuncTest("arctg", Functions.Arctangent);
        }

        [Fact]
        public void Arctan()
        {
            FuncTest("arctan", Functions.Arctangent);
        }

        [Fact]
        public void Arcctg()
        {
            FuncTest("arcctg", Functions.Arccotangent);
        }

        [Fact]
        public void Arccot()
        {
            FuncTest("arccot", Functions.Arccotangent);
        }

        [Fact]
        public void Arcsec()
        {
            FuncTest("arcsec", Functions.Arcsecant);
        }

        [Fact]
        public void Sqrt()
        {
            FuncTest("sqrt", Functions.Sqrt);
        }

        [Fact]
        public void Round()
        {
            FuncTest("round", Functions.Round);
        }

        [Fact]
        public void Ceil()
        {
            FuncTest("ceil", Functions.Ceil);
        }

        [Fact]
        public void Floor()
        {
            FuncTest("floor", Functions.Floor);
        }

        [Fact]
        public void Root()
        {
            var tokens = lexer.Tokenize("root(27, 3)");
            var expected = Builder()
                .Function(Functions.Root, 2)
                .OpenBracket()
                .Number(27)
                .Comma()
                .Number(3)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Lg()
        {
            FuncTest("lg", Functions.Lg);
        }

        [Fact]
        public void Ln()
        {
            FuncTest("ln", Functions.Ln);
        }

        [Fact]
        public void Lb()
        {
            FuncTest("lb", Functions.Lb);
        }

        [Fact]
        public void Log2()
        {
            FuncTest("log2", Functions.Lb);
        }

        [Fact]
        public void Log()
        {
            var tokens = lexer.Tokenize("log(2, 2)");
            var expected = Builder()
                .Function(Functions.Log, 2)
                .OpenBracket()
                .Number(2)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SignTest()
        {
            var tokens = lexer.Tokenize("sign(-10)");
            var expected = Builder()
                .Function(Functions.Sign)
                .OpenBracket()
                .Operation(Operations.UnaryMinus)
                .Number(10)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DelTest()
        {
            var tokens = lexer.Tokenize("del(2x + 3y + 4z)");
            var expected = Builder()
                .Function(Functions.Del)
                .OpenBracket()
                .Number(2)
                .Operation(Operations.Multiplication)
                .VariableX()
                .Operation(Operations.Addition)
                .Number(3)
                .Operation(Operations.Multiplication)
                .VariableY()
                .Operation(Operations.Addition)
                .Number(4)
                .Operation(Operations.Multiplication)
                .Variable("z")
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NablaTest()
        {
            var tokens = lexer.Tokenize("nabla(2x + 3y + 4z)");
            var expected = Builder()
                .Function(Functions.Del)
                .OpenBracket()
                .Number(2)
                .Operation(Operations.Multiplication)
                .VariableX()
                .Operation(Operations.Addition)
                .Number(3)
                .Operation(Operations.Multiplication)
                .VariableY()
                .Operation(Operations.Addition)
                .Number(4)
                .Operation(Operations.Multiplication)
                .Variable("z")
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddTest()
        {
            FuncBinaryTest("add", Functions.Add);
        }

        [Fact]
        public void SubTest()
        {
            FuncBinaryTest("sub", Functions.Sub);
        }

        [Fact]
        public void MulTest()
        {
            FuncBinaryTest("mul", Functions.Mul);
        }

        [Fact]
        public void DivTest()
        {
            FuncBinaryTest("div", Functions.Div);
        }

        [Fact]
        public void PowTest()
        {
            FuncBinaryTest("pow", Functions.Pow);
        }

        [Fact]
        public void MinTest()
        {
            FuncBinaryTest("min", Functions.Min);
        }

        [Fact]
        public void MaxTest()
        {
            FuncBinaryTest("max", Functions.Max);
        }

        [Fact]
        public void AvgTest()
        {
            FuncBinaryTest("avg", Functions.Avg);
        }

        [Fact]
        public void CountTest()
        {
            FuncBinaryTest("count", Functions.Count);
        }

        [Fact]
        public void VarTest()
        {
            FuncBinaryTest("var", Functions.Var);
        }

        [Fact]
        public void VarpTest()
        {
            FuncBinaryTest("varp", Functions.Varp);
        }

        [Fact]
        public void StdevTest()
        {
            FuncBinaryTest("stdev", Functions.Stdev);
        }

        [Fact]
        public void StdevpTest()
        {
            FuncBinaryTest("stdevp", Functions.Stdevp);
        }

    }

}