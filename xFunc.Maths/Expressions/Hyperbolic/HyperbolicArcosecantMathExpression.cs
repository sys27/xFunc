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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions.Hyperbolic
{

    public class HyperbolicArcosecantMathExpression : UnaryMathExpression
    {

        public HyperbolicArcosecantMathExpression()
            : base(null)
        {

        }

        public HyperbolicArcosecantMathExpression(IMathExpression firstMathExpression)
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
            return new HyperbolicArcosecantMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var inv = new ExponentiationMathExpression(firstMathExpression.Clone(), new NumberMathExpression(2));
            var add = new AdditionMathExpression(new NumberMathExpression(1), inv);
            var sqrt = new SqrtMathExpression(add);
            var abs = new AbsoluteMathExpression(firstMathExpression.Clone());
            var mul = new MultiplicationMathExpression(abs, sqrt);
            var div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), mul);
            var unMinus = new UnaryMinusMathExpression(div);

            return unMinus;
        }

    }

}
