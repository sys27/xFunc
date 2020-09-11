// Copyright 2012-2020 Dmytro Kyshchenko
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
    /// <summary>
    /// Contains helpers for throw exceptions.
    /// </summary>
    internal static class ThrowHelpers
    {
        /// <summary>
        /// Creates an <see name="ArgumentNullException"/>.
        /// </summary>
        /// <remarks>
        /// We can move <c>throw</c> keyword here, but it will result in false-positive errors from CA1062 analyzer.
        /// </remarks>
        /// <returns>The <see cref="ArgumentNullException" /> for 'exp' parameter.</returns>
        internal static ArgumentNullException ExpNull()
            => new ArgumentNullException("exp");

        /// <summary>
        /// Creates an <see name="ArgumentNullException"/>.
        /// </summary>
        /// <remarks>
        /// We can move <c>throw</c> keyword here, but it will result in false-positive errors from CA1062 analyzer.
        /// </remarks>
        /// <returns>The <see cref="ArgumentNullException" /> for 'context' parameter.</returns>
        internal static ArgumentNullException ContextNull()
            => new ArgumentNullException("context");
    }
}