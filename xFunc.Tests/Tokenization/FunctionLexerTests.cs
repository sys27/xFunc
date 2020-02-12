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

using System.Linq;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.Tokenization
{
    public class FunctionLexerTests : BaseLexerTests
    {
        [Fact]
        public void Exp()
        {
            FuncTest("exp");
        }

        [Fact]
        public void Abs()
        {
            FuncTest("abs");
        }

        [Fact]
        public void Sh()
        {
            FuncTest("sh");
        }

        [Fact]
        public void Sinh()
        {
            FuncTest("sinh");
        }

        [Fact]
        public void Ch()
        {
            FuncTest("ch");
        }

        [Fact]
        public void Cosh()
        {
            FuncTest("cosh");
        }

        [Fact]
        public void Th()
        {
            FuncTest("th");
        }

        [Fact]
        public void Tanh()
        {
            FuncTest("tanh");
        }

        [Fact]
        public void Cth()
        {
            FuncTest("cth");
        }

        [Fact]
        public void Coth()
        {
            FuncTest("coth");
        }

        [Fact]
        public void Sech()
        {
            FuncTest("sech");
        }

        [Fact]
        public void Csch()
        {
            FuncTest("csch");
        }

        [Fact]
        public void Arsinh()
        {
            FuncTest("arsinh");
        }

        [Fact]
        public void Arsh()
        {
            FuncTest("arsh");
        }

        [Fact]
        public void Arcosh()
        {
            FuncTest("arcosh");
        }

        [Fact]
        public void Arch()
        {
            FuncTest("arch");
        }

        [Fact]
        public void Artanh()
        {
            FuncTest("artanh");
        }

        [Fact]
        public void Arth()
        {
            FuncTest("arth");
        }

        [Fact]
        public void Arcoth()
        {
            FuncTest("arcoth");
        }

        [Fact]
        public void Arcth()
        {
            FuncTest("arcth");
        }

        [Fact]
        public void Arsech()
        {
            FuncTest("arsech");
        }

        [Fact]
        public void Arsch()
        {
            FuncTest("arsch");
        }

        [Fact]
        public void Arcsch()
        {
            FuncTest("arcsch");
        }

        [Fact]
        public void Sin()
        {
            FuncTest("sin");
        }

        [Fact]
        public void Cosec()
        {
            FuncTest("cosec");
        }

        [Fact]
        public void Csc()
        {
            FuncTest("csc");
        }

        [Fact]
        public void Cos()
        {
            FuncTest("cos");
        }

        [Fact]
        public void Tg()
        {
            FuncTest("tg");
        }

        [Fact]
        public void Tan()
        {
            FuncTest("tan");
        }

        [Fact]
        public void Ctg()
        {
            FuncTest("ctg");
        }

        [Fact]
        public void Cot()
        {
            FuncTest("cot");
        }

        [Fact]
        public void Sec()
        {
            FuncTest("sec");
        }

        [Fact]
        public void Arcsin()
        {
            FuncTest("arcsin");
        }

        [Fact]
        public void Arccosec()
        {
            FuncTest("arccosec");
        }

        [Fact]
        public void Arccsc()
        {
            FuncTest("arccsc");
        }

        [Fact]
        public void Arccos()
        {
            FuncTest("arccos");
        }

        [Fact]
        public void Arctg()
        {
            FuncTest("arctg");
        }

        [Fact]
        public void Arctan()
        {
            FuncTest("arctan");
        }

        [Fact]
        public void Arcctg()
        {
            FuncTest("arcctg");
        }

        [Fact]
        public void Arccot()
        {
            FuncTest("arccot");
        }

        [Fact]
        public void Arcsec()
        {
            FuncTest("arcsec");
        }

        [Fact]
        public void Sqrt()
        {
            FuncTest("sqrt");
        }

        [Fact]
        public void Round()
        {
            FuncTest("round");
        }

        [Fact]
        public void Ceil()
        {
            FuncTest("ceil");
        }

        [Fact]
        public void Floor()
        {
            FuncTest("floor");
        }

        [Fact]
        public void Root()
        {
            FuncBinaryTest("root");
        }

        [Fact]
        public void Lg()
        {
            FuncTest("lg");
        }

        [Fact]
        public void Ln()
        {
            FuncTest("ln");
        }

        [Fact]
        public void Lb()
        {
            FuncTest("lb");
        }

        [Fact]
        public void Log2()
        {
            FuncTest("log2");
        }

        [Fact]
        public void Log()
        {
            FuncBinaryTest("log");
        }

        [Fact]
        public void SignTest()
        {
            FuncTest("sign");
        }

        [Fact]
        public void DelTest()
        {
            var tokens = lexer.Tokenize("del(2x + 3y + 4z)");
            var expected = Builder()
                .Id("del")
                .OpenParenthesis()
                .Number(2)
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(3)
                .VariableY()
                .Operation(OperatorToken.Plus)
                .Number(4)
                .Id("z")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NablaTest()
        {
            var tokens = lexer.Tokenize("nabla(2x + 3y + 4z)");
            var expected = Builder()
                .Id("nabla")
                .OpenParenthesis()
                .Number(2)
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(3)
                .VariableY()
                .Operation(OperatorToken.Plus)
                .Number(4)
                .Id("z")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddTest()
        {
            FuncBinaryTest("add");
        }

        [Fact]
        public void SubTest()
        {
            FuncBinaryTest("sub");
        }

        [Fact]
        public void MulTest()
        {
            FuncBinaryTest("mul");
        }

        [Fact]
        public void DivTest()
        {
            FuncBinaryTest("div");
        }

        [Fact]
        public void PowTest()
        {
            FuncBinaryTest("pow");
        }

        [Fact]
        public void MinTest()
        {
            FuncBinaryTest("min");
        }

        [Fact]
        public void MaxTest()
        {
            FuncBinaryTest("max");
        }

        [Fact]
        public void AvgTest()
        {
            FuncBinaryTest("avg");
        }

        [Fact]
        public void CountTest()
        {
            FuncBinaryTest("count");
        }

        [Fact]
        public void VarTest()
        {
            FuncBinaryTest("var");
        }

        [Fact]
        public void VarpTest()
        {
            FuncBinaryTest("varp");
        }

        [Fact]
        public void StdevTest()
        {
            FuncBinaryTest("stdev");
        }

        [Fact]
        public void StdevpTest()
        {
            FuncBinaryTest("stdevp");
        }
    }
}