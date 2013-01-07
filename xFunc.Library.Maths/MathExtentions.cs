using System;

namespace xFunc.Library.Maths
{

    public static class MathExtentions
    {

        public static double Cot(double d)
        {
            return Math.Cos(d) / Math.Sin(d);
        }

        public static double Sec(double d)
        {
            return 1 / Math.Cos(d);
        }

        public static double Csc(double d)
        {
            return 1 / Math.Sin(d);
        }

        public static double Acot(double d)
        {
            return Math.PI / 2 - Math.Atan(d);
        }

        public static double Asec(double d)
        {
            return Math.Acos(1 / d);
        }

        public static double Acsc(double d)
        {
            return Math.Asin(1 / d);
        }

    }

}
