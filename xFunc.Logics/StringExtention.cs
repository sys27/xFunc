#if NET20 || NET30 || NET35

using System;

namespace xFunc.Logics
{

    internal static class StringExtention
    {

        public static bool IsNullOrWhiteSpace(string value)
        {
            if (value != null)
                for (int i = 0; i < value.Length; i++)
                    if (!char.IsWhiteSpace(value[i]))
                        return false;

            return true;
        }

    }

}

#endif