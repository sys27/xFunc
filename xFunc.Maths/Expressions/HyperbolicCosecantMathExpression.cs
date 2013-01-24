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

    public class HyperbolicCosecantMathExpression : UnaryMathExpression
    {

        public HyperbolicCosecantMathExpression()
            : base(null)
        {

        }

        public HyperbolicCosecantMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("csch({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Csch(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicCosecantMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var coth = new HyperbolicCotangentMathExpression(firstMathExpression.Clone());
            var csch = Clone();
            var mul1 = new MultiplicationMathExpression(coth, csch);
            var mul2 = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), mul1);
            var unMinus = new UnaryMinusMathExpression(mul2);

            return unMinus;
        }

    }

}
