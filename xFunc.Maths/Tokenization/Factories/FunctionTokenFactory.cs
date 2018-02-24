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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    public class FunctionTokenFactory : FactoryBase
    {
        public FunctionTokenFactory() : base(new Regex(@"\G([a-zα-ω][0-9a-zα-ω]*)(\(|{)", RegexOptions.Compiled | RegexOptions.IgnoreCase)) { }

        protected override FactoryResult CreateTokenInternal(Match match, IReadOnlyList<IToken> tokens)
        {
            if (tokens.LastOrDefault() is NumberToken)
                return new FactoryResult(new OperationToken(Operations.Multiplication), 0);

            var result = new FactoryResult();
            var stringMatch = match.Groups[1].Value;
            var lowerMatch = stringMatch.ToLower();
            if (lowerMatch == "add")
            {
                result.Token = new FunctionToken(Functions.Add);
            }
            else if (lowerMatch == "sub")
            {
                result.Token = new FunctionToken(Functions.Sub);
            }
            else if (lowerMatch == "mul")
            {
                result.Token = new FunctionToken(Functions.Mul);
            }
            else if (lowerMatch == "div")
            {
                result.Token = new FunctionToken(Functions.Div);
            }
            else if (lowerMatch == "pow")
            {
                result.Token = new FunctionToken(Functions.Pow);
            }
            else if (lowerMatch == "abs")
            {
                result.Token = new FunctionToken(Functions.Absolute);
            }
            else if (lowerMatch == "sin")
            {
                result.Token = new FunctionToken(Functions.Sine);
            }
            else if (lowerMatch == "cos")
            {
                result.Token = new FunctionToken(Functions.Cosine);
            }
            else if (lowerMatch == "tg" || lowerMatch == "tan")
            {
                result.Token = new FunctionToken(Functions.Tangent);
            }
            else if (lowerMatch == "ctg" || lowerMatch == "cot")
            {
                result.Token = new FunctionToken(Functions.Cotangent);
            }
            else if (lowerMatch == "sec")
            {
                result.Token = new FunctionToken(Functions.Secant);
            }
            else if (lowerMatch == "csc" || lowerMatch == "cosec")
            {
                result.Token = new FunctionToken(Functions.Cosecant);
            }
            else if (lowerMatch == "arcsin")
            {
                result.Token = new FunctionToken(Functions.Arcsine);
            }
            else if (lowerMatch == "arccos")
            {
                result.Token = new FunctionToken(Functions.Arccosine);
            }
            else if (lowerMatch == "arctg" || lowerMatch == "arctan")
            {
                result.Token = new FunctionToken(Functions.Arctangent);
            }
            else if (lowerMatch == "arcctg" || lowerMatch == "arccot")
            {
                result.Token = new FunctionToken(Functions.Arccotangent);
            }
            else if (lowerMatch == "arcsec")
            {
                result.Token = new FunctionToken(Functions.Arcsecant);
            }
            else if (lowerMatch == "arccsc" || lowerMatch == "arccosec")
            {
                result.Token = new FunctionToken(Functions.Arccosecant);
            }
            else if (lowerMatch == "sqrt")
            {
                result.Token = new FunctionToken(Functions.Sqrt);
            }
            else if (lowerMatch == "root")
            {
                result.Token = new FunctionToken(Functions.Root);
            }
            else if (lowerMatch == "ln")
            {
                result.Token = new FunctionToken(Functions.Ln);
            }
            else if (lowerMatch == "lg")
            {
                result.Token = new FunctionToken(Functions.Lg);
            }
            else if (lowerMatch == "lb" || lowerMatch == "log2")
            {
                result.Token = new FunctionToken(Functions.Lb);
            }
            else if (lowerMatch == "log")
            {
                result.Token = new FunctionToken(Functions.Log);
            }
            else if (lowerMatch == "sh" || lowerMatch == "sinh")
            {
                result.Token = new FunctionToken(Functions.Sineh);
            }
            else if (lowerMatch == "ch" || lowerMatch == "cosh")
            {
                result.Token = new FunctionToken(Functions.Cosineh);
            }
            else if (lowerMatch == "th" || lowerMatch == "tanh")
            {
                result.Token = new FunctionToken(Functions.Tangenth);
            }
            else if (lowerMatch == "cth" || lowerMatch == "coth")
            {
                result.Token = new FunctionToken(Functions.Cotangenth);
            }
            else if (lowerMatch == "sech")
            {
                result.Token = new FunctionToken(Functions.Secanth);
            }
            else if (lowerMatch == "csch")
            {
                result.Token = new FunctionToken(Functions.Cosecanth);
            }
            else if (lowerMatch == "arsh" || lowerMatch == "arsinh")
            {
                result.Token = new FunctionToken(Functions.Arsineh);
            }
            else if (lowerMatch == "arch" || lowerMatch == "arcosh")
            {
                result.Token = new FunctionToken(Functions.Arcosineh);
            }
            else if (lowerMatch == "arth" || lowerMatch == "artanh")
            {
                result.Token = new FunctionToken(Functions.Artangenth);
            }
            else if (lowerMatch == "arcth" || lowerMatch == "arcoth")
            {
                result.Token = new FunctionToken(Functions.Arcotangenth);
            }
            else if (lowerMatch == "arsch" || lowerMatch == "arsech")
            {
                result.Token = new FunctionToken(Functions.Arsecanth);
            }
            else if (lowerMatch == "arcsch")
            {
                result.Token = new FunctionToken(Functions.Arcosecanth);
            }
            else if (lowerMatch == "exp")
            {
                result.Token = new FunctionToken(Functions.Exp);
            }
            else if (lowerMatch == "gcd" || lowerMatch == "gcf" || lowerMatch == "hcf")
            {
                result.Token = new FunctionToken(Functions.GCD);
            }
            else if (lowerMatch == "lcm" || lowerMatch == "scm")
            {
                result.Token = new FunctionToken(Functions.LCM);
            }
            else if (lowerMatch == "fact")
            {
                result.Token = new FunctionToken(Functions.Factorial);
            }
            else if (lowerMatch == "sum")
            {
                result.Token = new FunctionToken(Functions.Sum);
            }
            else if (lowerMatch == "product")
            {
                result.Token = new FunctionToken(Functions.Product);
            }
            else if (lowerMatch == "round")
            {
                result.Token = new FunctionToken(Functions.Round);
            }
            else if (lowerMatch == "floor")
            {
                result.Token = new FunctionToken(Functions.Floor);
            }
            else if (lowerMatch == "ceil")
            {
                result.Token = new FunctionToken(Functions.Ceil);
            }
            else if (lowerMatch == "if")
            {
                result.Token = new FunctionToken(Functions.If);
            }
            else if (lowerMatch == "for")
            {
                result.Token = new FunctionToken(Functions.For);
            }
            else if (lowerMatch == "while")
            {
                result.Token = new FunctionToken(Functions.While);
            }
            else if (lowerMatch == "del" || lowerMatch == "nabla")
            {
                result.Token = new FunctionToken(Functions.Del);
            }
            else if (lowerMatch == "deriv")
            {
                result.Token = new FunctionToken(Functions.Derivative);
            }
            else if (lowerMatch == "simplify")
            {
                result.Token = new FunctionToken(Functions.Simplify);
            }
            else if (lowerMatch == "def")
            {
                result.Token = new FunctionToken(Functions.Define);
            }
            else if (lowerMatch == "undef")
            {
                result.Token = new FunctionToken(Functions.Undefine);
            }
            else if (lowerMatch == "transpose")
            {
                result.Token = new FunctionToken(Functions.Transpose);
            }
            else if (lowerMatch == "det" || lowerMatch == "determinant")
            {
                result.Token = new FunctionToken(Functions.Determinant);
            }
            else if (lowerMatch == "inverse")
            {
                result.Token = new FunctionToken(Functions.Inverse);
            }
            else if (lowerMatch == "vector")
            {
                result.Token = new FunctionToken(Functions.Vector);
            }
            else if (lowerMatch == "matrix")
            {
                result.Token = new FunctionToken(Functions.Matrix);
            }
            else if (lowerMatch == "re" || lowerMatch == "real")
            {
                result.Token = new FunctionToken(Functions.Re);
            }
            else if (lowerMatch == "im" || lowerMatch == "imaginary")
            {
                result.Token = new FunctionToken(Functions.Im);
            }
            else if (lowerMatch == "phase")
            {
                result.Token = new FunctionToken(Functions.Phase);
            }
            else if (lowerMatch == "conjugate")
            {
                result.Token = new FunctionToken(Functions.Conjugate);
            }
            else if (lowerMatch == "reciprocal")
            {
                result.Token = new FunctionToken(Functions.Reciprocal);
            }
            else if (lowerMatch == "min")
            {
                result.Token = new FunctionToken(Functions.Min);
            }
            else if (lowerMatch == "max")
            {
                result.Token = new FunctionToken(Functions.Max);
            }
            else if (lowerMatch == "avg")
            {
                result.Token = new FunctionToken(Functions.Avg);
            }
            else if (lowerMatch == "count")
            {
                result.Token = new FunctionToken(Functions.Count);
            }
            else if (lowerMatch == "var")
            {
                result.Token = new FunctionToken(Functions.Var);
            }
            else if (lowerMatch == "varp")
            {
                result.Token = new FunctionToken(Functions.Varp);
            }
            else if (lowerMatch == "stdev")
            {
                result.Token = new FunctionToken(Functions.Stdev);
            }
            else if (lowerMatch == "stdevp")
            {
                result.Token = new FunctionToken(Functions.Stdevp);
            }
            else
            {
                result.Token = new UserFunctionToken(stringMatch);
            }

            result.ProcessedLength = stringMatch.Length;
            return result;
        }
    }
}