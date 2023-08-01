// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Parameters;

/// <summary>
/// Strongly typed collection that contains parameters.
/// </summary>
public partial class ExpressionParameters
{
    private sealed class ScopedExpressionParameters : ExpressionParameters
    {
        private readonly ExpressionParameters parent;

        public ScopedExpressionParameters(ExpressionParameters parent)
            : base(false)
            => this.parent = parent;

        public override IEnumerator<Parameter> GetEnumerator()
        {
            foreach (var parameter in parent)
                yield return parameter;

            foreach (var (_, parameter) in collection)
                yield return parameter;
        }

        public override ParameterValue this[string key]
        {
            get => collection.TryGetValue(key, out var parameter)
                ? parameter.Value
                : parent[key];
            set
            {
                if (collection.TryGetValue(key, out _))
                    base[key] = value;
                else if (parent.TryGetParameter(key, out _))
                    parent[key] = value;
                else
                    base[key] = value;
            }
        }

        public override bool Contains(Parameter param)
            => base.Contains(param) || parent.Contains(param);

        public override bool ContainsKey(string key)
            => base.ContainsKey(key) || parent.ContainsKey(key);
    }
}