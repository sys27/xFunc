using System;

namespace xFunc.Library.Maths
{
    
    public static class MathExtentions
    {

        public static double Cot(double d)
        {
            return Math.Cos(d) / Math.Sin(d);
        }

        public static double Acot(double d)
        {
            return Math.PI / 2 - Math.Atan(d);
        }

    }

}
