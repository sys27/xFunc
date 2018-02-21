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
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    public class FunctionTokenFactory : IAbstractTokenFactory
    {
        public IList<IToken> CreateToken(string match, IToken lastToken)
        {
            var tokens = new List<IToken>();
            var lowerMatch = match.ToLower();
            if (lowerMatch == "add")
            {
                tokens.Add(new FunctionToken(Functions.Add));
            }
            else if (lowerMatch == "sub")
            {
                tokens.Add(new FunctionToken(Functions.Sub));
            }
            else if (lowerMatch == "mul")
            {
                tokens.Add(new FunctionToken(Functions.Mul));
            }
            else if (lowerMatch == "div")
            {
                tokens.Add(new FunctionToken(Functions.Div));
            }
            else if (lowerMatch == "pow")
            {
                tokens.Add(new FunctionToken(Functions.Pow));
            }
            else if (lowerMatch == "abs")
            {
                tokens.Add(new FunctionToken(Functions.Absolute));
            }
            else if (lowerMatch == "sin")
            {
                tokens.Add(new FunctionToken(Functions.Sine));
            }
            else if (lowerMatch == "cos")
            {
                tokens.Add(new FunctionToken(Functions.Cosine));
            }
            else if (lowerMatch == "tg" || lowerMatch == "tan")
            {
                tokens.Add(new FunctionToken(Functions.Tangent));
            }
            else if (lowerMatch == "ctg" || lowerMatch == "cot")
            {
                tokens.Add(new FunctionToken(Functions.Cotangent));
            }
            else if (lowerMatch == "sec")
            {
                tokens.Add(new FunctionToken(Functions.Secant));
            }
            else if (lowerMatch == "csc" || lowerMatch == "cosec")
            {
                tokens.Add(new FunctionToken(Functions.Cosecant));
            }
            else if (lowerMatch == "arcsin")
            {
                tokens.Add(new FunctionToken(Functions.Arcsine));
            }
            else if (lowerMatch == "arccos")
            {
                tokens.Add(new FunctionToken(Functions.Arccosine));
            }
            else if (lowerMatch == "arctg" || lowerMatch == "arctan")
            {
                tokens.Add(new FunctionToken(Functions.Arctangent));
            }
            else if (lowerMatch == "arcctg" || lowerMatch == "arccot")
            {
                tokens.Add(new FunctionToken(Functions.Arccotangent));
            }
            else if (lowerMatch == "arcsec")
            {
                tokens.Add(new FunctionToken(Functions.Arcsecant));
            }
            else if (lowerMatch == "arccsc" || lowerMatch == "arccosec")
            {
                tokens.Add(new FunctionToken(Functions.Arccosecant));
            }
            else if (lowerMatch == "sqrt")
            {
                tokens.Add(new FunctionToken(Functions.Sqrt));
            }
            else if (lowerMatch == "root")
            {
                tokens.Add(new FunctionToken(Functions.Root));
            }
            else if (lowerMatch == "ln")
            {
                tokens.Add(new FunctionToken(Functions.Ln));
            }
            else if (lowerMatch == "lg")
            {
                tokens.Add(new FunctionToken(Functions.Lg));
            }
            else if (lowerMatch == "lb" || lowerMatch == "log2")
            {
                tokens.Add(new FunctionToken(Functions.Lb));
            }
            else if (lowerMatch == "log")
            {
                tokens.Add(new FunctionToken(Functions.Log));
            }
            else if (lowerMatch == "sh" || lowerMatch == "sinh")
            {
                tokens.Add(new FunctionToken(Functions.Sineh));
            }
            else if (lowerMatch == "ch" || lowerMatch == "cosh")
            {
                tokens.Add(new FunctionToken(Functions.Cosineh));
            }
            else if (lowerMatch == "th" || lowerMatch == "tanh")
            {
                tokens.Add(new FunctionToken(Functions.Tangenth));
            }
            else if (lowerMatch == "cth" || lowerMatch == "coth")
            {
                tokens.Add(new FunctionToken(Functions.Cotangenth));
            }
            else if (lowerMatch == "sech")
            {
                tokens.Add(new FunctionToken(Functions.Secanth));
            }
            else if (lowerMatch == "csch")
            {
                tokens.Add(new FunctionToken(Functions.Cosecanth));
            }
            else if (lowerMatch == "arsh" || lowerMatch == "arsinh")
            {
                tokens.Add(new FunctionToken(Functions.Arsineh));
            }
            else if (lowerMatch == "arch" || lowerMatch == "arcosh")
            {
                tokens.Add(new FunctionToken(Functions.Arcosineh));
            }
            else if (lowerMatch == "arth" || lowerMatch == "artanh")
            {
                tokens.Add(new FunctionToken(Functions.Artangenth));
            }
            else if (lowerMatch == "arcth" || lowerMatch == "arcoth")
            {
                tokens.Add(new FunctionToken(Functions.Arcotangenth));
            }
            else if (lowerMatch == "arsch" || lowerMatch == "arsech")
            {
                tokens.Add(new FunctionToken(Functions.Arsecanth));
            }
            else if (lowerMatch == "arcsch")
            {
                tokens.Add(new FunctionToken(Functions.Arcosecanth));
            }
            else if (lowerMatch == "exp")
            {
                tokens.Add(new FunctionToken(Functions.Exp));
            }
            else if (lowerMatch == "gcd" || lowerMatch == "gcf" || lowerMatch == "hcf")
            {
                tokens.Add(new FunctionToken(Functions.GCD));
            }
            else if (lowerMatch == "lcm" || lowerMatch == "scm")
            {
                tokens.Add(new FunctionToken(Functions.LCM));
            }
            else if (lowerMatch == "fact")
            {
                tokens.Add(new FunctionToken(Functions.Factorial));
            }
            else if (lowerMatch == "sum")
            {
                tokens.Add(new FunctionToken(Functions.Sum));
            }
            else if (lowerMatch == "product")
            {
                tokens.Add(new FunctionToken(Functions.Product));
            }
            else if (lowerMatch == "round")
            {
                tokens.Add(new FunctionToken(Functions.Round));
            }
            else if (lowerMatch == "floor")
            {
                tokens.Add(new FunctionToken(Functions.Floor));
            }
            else if (lowerMatch == "ceil")
            {
                tokens.Add(new FunctionToken(Functions.Ceil));
            }
            else if (lowerMatch == "if")
            {
                tokens.Add(new FunctionToken(Functions.If));
            }
            else if (lowerMatch == "for")
            {
                tokens.Add(new FunctionToken(Functions.For));
            }
            else if (lowerMatch == "while")
            {
                tokens.Add(new FunctionToken(Functions.While));
            }
            else if (lowerMatch == "del" || lowerMatch == "nabla")
            {
                tokens.Add(new FunctionToken(Functions.Del));
            }
            else if (lowerMatch == "deriv")
            {
                tokens.Add(new FunctionToken(Functions.Derivative));
            }
            else if (lowerMatch == "simplify")
            {
                tokens.Add(new FunctionToken(Functions.Simplify));
            }
            else if (lowerMatch == "def")
            {
                tokens.Add(new FunctionToken(Functions.Define));
            }
            else if (lowerMatch == "undef")
            {
                tokens.Add(new FunctionToken(Functions.Undefine));
            }
            else if (lowerMatch == "transpose")
            {
                tokens.Add(new FunctionToken(Functions.Transpose));
            }
            else if (lowerMatch == "det" || lowerMatch == "determinant")
            {
                tokens.Add(new FunctionToken(Functions.Determinant));
            }
            else if (lowerMatch == "inverse")
            {
                tokens.Add(new FunctionToken(Functions.Inverse));
            }
            else if (lowerMatch == "vector")
            {
                tokens.Add(new FunctionToken(Functions.Vector));
            }
            else if (lowerMatch == "matrix")
            {
                tokens.Add(new FunctionToken(Functions.Matrix));
            }
            else if (lowerMatch == "re" || lowerMatch == "real")
            {
                tokens.Add(new FunctionToken(Functions.Re));
            }
            else if (lowerMatch == "im" || lowerMatch == "imaginary")
            {
                tokens.Add(new FunctionToken(Functions.Im));
            }
            else if (lowerMatch == "phase")
            {
                tokens.Add(new FunctionToken(Functions.Phase));
            }
            else if (lowerMatch == "conjugate")
            {
                tokens.Add(new FunctionToken(Functions.Conjugate));
            }
            else if (lowerMatch == "reciprocal")
            {
                tokens.Add(new FunctionToken(Functions.Reciprocal));
            }
            else if (lowerMatch == "min")
            {
                tokens.Add(new FunctionToken(Functions.Min));
            }
            else if (lowerMatch == "max")
            {
                tokens.Add(new FunctionToken(Functions.Max));
            }
            else if (lowerMatch == "avg")
            {
                tokens.Add(new FunctionToken(Functions.Avg));
            }
            else if (lowerMatch == "count")
            {
                tokens.Add(new FunctionToken(Functions.Count));
            }
            else if (lowerMatch == "var")
            {
                tokens.Add(new FunctionToken(Functions.Var));
            }
            else if (lowerMatch == "varp")
            {
                tokens.Add(new FunctionToken(Functions.Varp));
            }
            else if (lowerMatch == "stdev")
            {
                tokens.Add(new FunctionToken(Functions.Stdev));
            }
            else if (lowerMatch == "stdevp")
            {
                tokens.Add(new FunctionToken(Functions.Stdevp));
            }
            else
            {
                tokens.Add(new UserFunctionToken(match));
            }

            return tokens;
        }
    }
}