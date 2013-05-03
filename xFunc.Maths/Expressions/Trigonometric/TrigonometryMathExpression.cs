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

    public abstract class TrigonometryMathExpression : UnaryMathExpression
    {

        protected AngleMeasurement angleMeasurement;

        public TrigonometryMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {
            angleMeasurement = AngleMeasurement.Degree;
        }

        public abstract double CalculateDergee(MathParameterCollection parameters, MathFunctionCollection functions);
        public abstract double CalculateRadian(MathParameterCollection parameters, MathFunctionCollection functions);
        public abstract double CalculateGradian(MathParameterCollection parameters, MathFunctionCollection functions);

        public override double Calculate()
        {
            if (angleMeasurement == AngleMeasurement.Degree)
                return CalculateDergee(null, null);
            if (angleMeasurement == AngleMeasurement.Radian)
                return CalculateRadian(null, null);
            if (angleMeasurement == AngleMeasurement.Gradian)
                return CalculateGradian(null, null);

            return double.NaN;
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            if (angleMeasurement == AngleMeasurement.Degree)
                return CalculateDergee(parameters, null);
            if (angleMeasurement == AngleMeasurement.Radian)
                return CalculateRadian(parameters, null);
            if (angleMeasurement == AngleMeasurement.Gradian)
                return CalculateGradian(parameters, null);

            return double.NaN;
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            if (angleMeasurement == AngleMeasurement.Degree)
                return CalculateDergee(parameters, functions);
            if (angleMeasurement == AngleMeasurement.Radian)
                return CalculateRadian(parameters, functions);
            if (angleMeasurement == AngleMeasurement.Gradian)
                return CalculateGradian(parameters, functions);

            return double.NaN;
        }

        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return angleMeasurement;
            }
            set
            {
                angleMeasurement = value;
            }
        }

    }

}
