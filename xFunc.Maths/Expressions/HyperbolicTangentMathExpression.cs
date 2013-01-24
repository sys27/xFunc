// Copyright 2012 Dmitry Kischenko
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
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class HyperbolicTangentMathExpression : UnaryMathExpression
    {

        public HyperbolicTangentMathExpression()
            : base(null)
        {

        }

        public HyperbolicTangentMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("tanh({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Tanh(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicTangentMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var cosh = new HyperbolicCosineMathExpression(firstMathExpression.Clone());
            var inv = new ExponentiationMathExpression(cosh, new NumberMathExpression(2));
            var div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), inv);

            return div;
        }

    }

}
