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

namespace xFunc.Maths.Expressions.Trigonometric
{

    public class ArcsinMathExpression : TrigonometryMathExpression
    {

        public ArcsinMathExpression() : base(null) { }

        public ArcsinMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("arcsin({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Asin(radian) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Asin(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Asin(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            ExponentiationMathExpression involution = new ExponentiationMathExpression(firstMathExpression.Clone(), new NumberMathExpression(2));
            SubtractionMathExpression sub = new SubtractionMathExpression(new NumberMathExpression(1), involution);
            SqrtMathExpression sqrt = new SqrtMathExpression(sub);
            DivisionMathExpression division = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), sqrt);

            return division;
        }

        public override IMathExpression Clone()
        {
            return new ArcsinMathExpression(firstMathExpression.Clone());
        }

    }

}
