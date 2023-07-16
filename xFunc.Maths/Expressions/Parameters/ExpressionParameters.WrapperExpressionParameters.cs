// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace xFunc.Maths.Expressions.Parameters;

/// <summary>
/// Strongly typed collection that contains parameters.
/// </summary>
public partial class ExpressionParameters
{
    private class WrapperExpressionParameters : IExpressionParameters
    {
        private readonly IExpressionParameters expressionParameters;

        public WrapperExpressionParameters(IExpressionParameters expressionParameters)
            => this.expressionParameters = expressionParameters;

        public virtual ParameterValue this[string key]
        {
            get => expressionParameters[key];
            set => expressionParameters[key] = value;
        }

        public virtual IEnumerator<Parameter> GetEnumerator()
            => expressionParameters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => expressionParameters.GetEnumerator();

        public virtual void Add(Parameter item)
            => expressionParameters.Add(item);

        public virtual void Clear()
            => expressionParameters.Clear();

        public virtual bool Contains(Parameter item)
            => expressionParameters.Contains(item);

        public virtual bool Remove(Parameter item)
            => expressionParameters.Remove(item);

        public virtual bool TryGetParameter(string key, [NotNullWhen(true)] out Parameter? parameter)
            => expressionParameters.TryGetParameter(key, out parameter);

        public virtual void Add(string key, ParameterValue value)
            => expressionParameters.Add(key, value);

        public virtual void Remove(string key)
            => expressionParameters.Remove(key);

        public virtual bool ContainsKey(string key)
            => expressionParameters.ContainsKey(key);
    }

    private sealed class ScopedWrapperExpressionParameters : WrapperExpressionParameters
    {
        private readonly WrapperExpressionParameters parent;

        public ScopedWrapperExpressionParameters(
            IExpressionParameters expressionParameters,
            WrapperExpressionParameters parent)
            : base(expressionParameters)
            => this.parent = parent;

        public override ParameterValue this[string key]
        {
            get
            {
                if (!TryGetParameter(key, out var parameter))
                    throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Resource.VariableNotFoundExceptionError, key));

                return parameter.Value;
            }
            set => base[key] = value;
        }

        public override IEnumerator<Parameter> GetEnumerator()
        {
            foreach (var parameter in parent)
                yield return parameter;

            using var enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public override void Clear()
        {
            base.Clear();
            parent.Clear();
        }

        public override bool Contains(Parameter item)
            => base.Contains(item) | parent.Contains(item);

        public override bool ContainsKey(string key)
            => base.ContainsKey(key) | parent.ContainsKey(key);

        public override bool TryGetParameter(string key, [NotNullWhen(true)] out Parameter? parameter)
        {
            if (base.TryGetParameter(key, out parameter))
                return true;

            return parent.TryGetParameter(key, out parameter);
        }

        public override bool Remove(Parameter item)
            => base.Remove(item) | parent.Remove(item);

        public override void Remove(string key)
        {
            base.Remove(key);
            parent.Remove(key);
        }
    }
}