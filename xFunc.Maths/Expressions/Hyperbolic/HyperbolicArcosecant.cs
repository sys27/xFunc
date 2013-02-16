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

namespace xFunc.Maths.Expressions.Hyperbolic
{

    public class HyperbolicArcosecant : UnaryMathExpression
    {

        public HyperbolicArcosecant()
            : base(null)
        {

        }

        public HyperbolicArcosecant(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("arcsch({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Acsch(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicArcosecant(firstMathExpression.Clone());
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            var inv = new Exponentiation(firstMathExpression.Clone(), new Number(2));
            var add = new Addition(new Number(1), inv);
            var sqrt = new Sqrt(add);
            var abs = new Absolute(firstMathExpression.Clone());
            var mul = new Multiplication(abs, sqrt);
            var div = new Division(firstMathExpression.Clone().Differentiation(variable), mul);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

    }

}
