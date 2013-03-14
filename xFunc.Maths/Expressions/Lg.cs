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

namespace xFunc.Maths.Expressions
{

    public class Lg : UnaryMathExpression
    {

        public Lg() : base(null) { }

        public Lg(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("lg({0})");
        }

        public override double Calculate()
        {
            return Math.Log10(FirstMathExpression.Calculate());
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Log10(FirstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            Ln ln = new Ln(new Number(10));
            Multiplication mul1 = new Multiplication(firstMathExpression.Clone(), ln);
            Division div = new Division(firstMathExpression.Clone().Differentiate(variable), mul1);

            return div;
        }

        public override IMathExpression Clone()
        {
            return new Lg(firstMathExpression.Clone());
        }

    }

}
