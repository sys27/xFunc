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

    public class Csc : TrigonometryMathExpression
    {

        public Csc()
            : base(null)
        {

        }

        public Csc(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public Csc(IMathExpression firstMathExpression, AngleMeasurement angleMeasurement) : base(firstMathExpression, angleMeasurement) { }

        public override string ToString()
        {
            return ToString("csc({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = firstMathExpression.Calculate(parameters, functions) * Math.PI / 180;

            return 1 / Math.Sin(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return 1 / Math.Sin(firstMathExpression.Calculate(parameters, functions));
        }

        public override double CalculateGradian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = firstMathExpression.Calculate(parameters, functions) * Math.PI / 200;

            return 1 / Math.Sin(radian);
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            UnaryMinus unary = new UnaryMinus(firstMathExpression.Clone().Differentiate(variable));
            Cot cot = new Cot(firstMathExpression.Clone(), this.angleMeasurement);
            Csc csc = new Csc(firstMathExpression.Clone(), this.angleMeasurement);
            Mul mul1 = new Mul(cot, csc);
            Mul mul2 = new Mul(unary, mul1);

            return mul2;
        }

        public override IMathExpression Clone()
        {
            return new Csc(firstMathExpression.Clone());
        }

    }

}
