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

    public class Arcoth : UnaryMathExpression
    {

        public Arcoth()
            : base(null)
        {

        }

        public Arcoth(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("arcoth({0})");
        }

        public override double Calculate()
        {
            return MathExtentions.Acoth(firstMathExpression.Calculate());
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Acoth(firstMathExpression.Calculate(parameters));
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return MathExtentions.Acoth(firstMathExpression.Calculate(parameters, functions));
        }

        public override IMathExpression Clone()
        {
            return new Arcoth(firstMathExpression.Clone());
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            var sqr = new Pow(firstMathExpression.Clone(), new Number(2));
            var sub = new Sub(new Number(1), sqr);
            var div = new Div(firstMathExpression.Clone().Differentiate(variable), sub);

            return div;
        }

    }

}
