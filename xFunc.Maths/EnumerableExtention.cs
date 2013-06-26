#if NET20 || NET30

using System;
using System.Collections.Generic;
using xFunc.Maths.Resources;

namespace System.Runtime.CompilerServices
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal class ExtensionAttribute : Attribute { }

}

namespace xFunc.Maths
{

    internal delegate V Func<in T, out V>(T arg);

    internal static class EnumerableExtention
    {

        public static bool Any<T>(this IEnumerable<T> value, Func<T, bool> predicate)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            foreach (T item in value)
            {
                if (predicate(item))
                    return true;
            }

            return false;
        }
        
        public static T First<T>(this IEnumerable<T> value, Func<T, bool> predicate)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            foreach (T item in value)
            {
                if (predicate(item))
                    return item;
            }

            throw new InvalidOperationException(Resource.InvalidInFirst);
        }

    }

}

#endif