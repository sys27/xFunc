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

namespace xFunc.Maths.Expressions.Trigonometric
{

    public class Arccot : TrigonometryMathExpression
    {

        public Arccot() : base(null) { }

        public Arccot(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public Arccot(IMathExpression firstMathExpression, AngleMeasurement angleMeasurement) : base(firstMathExpression, angleMeasurement) { }

        public override string ToString()
        {
            return ToString("arccot({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = firstMathExpression.Calculate(parameters, functions);

            return MathExtentions.Acot(radian) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return MathExtentions.Acot(firstMathExpression.Calculate(parameters, functions));
        }

        public override double CalculateGradian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = firstMathExpression.Calculate(parameters, functions);

            return MathExtentions.Acot(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            Pow involution = new Pow(firstMathExpression.Clone(), new Number(2));
            Add add = new Add(new Number(1), involution);
            Div div = new Div(firstMathExpression.Clone().Differentiate(variable), add);
            UnaryMinus unMinus = new UnaryMinus(div);

            return unMinus;
        }

        public override IMathExpression Clone()
        {
            return new Arccot(firstMathExpression.Clone());
        }

    }

}
