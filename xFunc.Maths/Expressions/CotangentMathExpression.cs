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

    public class CotangentMathExpression : TrigonometryMathExpression
    {

        public CotangentMathExpression() : base(null) { }

        public CotangentMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("cot({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 180;

            return MathExtentions.Cot(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            var x = firstMathExpression.Calculate(parameters);

            return MathExtentions.Cot(x);
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 200;

            return MathExtentions.Cot(radian);
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            SineMathExpression sine = new SineMathExpression(firstMathExpression.Clone());
            ExponentiationMathExpression involution = new ExponentiationMathExpression(sine, new NumberMathExpression(2));
            DivisionMathExpression division = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), involution);
            UnaryMinusMathExpression unMinus = new UnaryMinusMathExpression(division);

            return unMinus;
        }

        public override IMathExpression Clone()
        {
            return new CotangentMathExpression(firstMathExpression.Clone());
        }

    }

}
