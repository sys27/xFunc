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

    public class HyperbolicArsine : UnaryMathExpression
    {

        public HyperbolicArsine()
            : base(null)
        {

        }

        public HyperbolicArsine(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("arsinh({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Asinh(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicArsine(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(Variable variable)
        {
            var sqr = new Exponentiation(firstMathExpression.Clone(), new Number(2));
            var add = new Addition(sqr, new Number(1));
            var sqrt = new Sqrt(add);
            var div = new Division(firstMathExpression.Clone().Differentiation(variable), sqrt);

            return div;
        }

    }

}
