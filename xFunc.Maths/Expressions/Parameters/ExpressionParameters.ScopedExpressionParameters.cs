// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace xFunc.Maths.Expressions.Parameters;

/// <summary>
/// Strongly typed collection that contains parameters.
/// </summary>
public partial class ExpressionParameters
{
    private sealed class ScopedExpressionParameters : ExpressionParameters
    {
        private readonly ExpressionParameters? parent;

        public ScopedExpressionParameters(ExpressionParameters? parent)
            : base(false)
            => this.parent = parent;

        public override IEnumerator<Parameter> GetEnumerator()
        {
            if (parent is not null)
                foreach (var parameter in parent)
                    yield return parameter;

            foreach (var (_, parameter) in collection)
                yield return parameter;
        }

        public override ParameterValue this[string key]
        {
            get
            {
                if (!TryGetParameter(key, out var parameter))
                    throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Resource.VariableNotFoundExceptionError, key));

                return parameter.Value;
            }
            set
            {
                if (collection.TryGetValue(key, out _))
                    base[key] = value;
                else if (parent?.TryGetParameter(key, out _) == true)
                    parent[key] = value;
                else
                    base[key] = value;
            }
        }

        public override bool TryGetParameter(string key, [NotNullWhen(true)] out Parameter? parameter)
            => base.TryGetParameter(key, out parameter) ||
               parent?.TryGetParameter(key, out parameter) == true;

        public override bool Contains(Parameter param)
            => base.Contains(param) || parent?.Contains(param) == true;

        public override bool ContainsKey(string key)
            => base.ContainsKey(key) || parent?.ContainsKey(key) == true;
    }
}