#if NET30

using System;
using System.Collections.Generic;
using xFunc.Logics.Resources;

namespace System.Runtime.CompilerServices
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ExtensionAttribute : Attribute { }

}

namespace xFunc.Logics
{

    public delegate V Func<in T, out V>(T arg);

    public static class EnumerableExtention
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