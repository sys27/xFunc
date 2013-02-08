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

namespace xFunc.Maths
{

    public static class MathExtentions
    {

        public static double Cot(double d)
        {
            return Math.Cos(d) / Math.Sin(d);
        }

        public static double Coth(double d)
        {
            return (Math.Exp(d) + Math.Exp(-d)) / (Math.Exp(d) - Math.Exp(-d));
        }

        public static double Sec(double d)
        {
            return 1 / Math.Cos(d);
        }

        public static double Sech(double d)
        {
            return 2 / (Math.Exp(d) + Math.Exp(-d));
        }

        public static double Csc(double d)
        {
            return 1 / Math.Sin(d);
        }

        public static double Csch(double d)
        {
            return 2 / (Math.Exp(d) - Math.Exp(-d));
        }

        public static double Asinh(double d)
        {
            return Math.Log(d + Math.Sqrt(d * d + 1));
        }

        public static double Acosh(double d)
        {
            return Math.Log(d + Math.Sqrt(d + 1) * Math.Sqrt(d - 1));
        }

        public static double Atanh(double d)
        {
            return Math.Log((1 + d) / (1 - d)) / 2;
        }

        public static double Acot(double d)
        {
            return Math.PI / 2 - Math.Atan(d);
        }

        public static double Acoth(double d)
        {
            return Math.Log((d + 1) / (d - 1)) / 2;
        }

        public static double Asec(double d)
        {
            return Math.Acos(1 / d);
        }

        public static double Asech(double d)
        {
            var z = 1 / d;
            return Math.Log(z + Math.Sqrt(z + 1) * Math.Sqrt(z - 1));
        }

        public static double Acsc(double d)
        {
            return Math.Asin(1 / d);
        }

        public static double Acsch(double d)
        {
            return Math.Log(1 / d + Math.Sqrt(1 / d * d + 1));
        }

    }

}
