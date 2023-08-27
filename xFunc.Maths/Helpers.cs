// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths;

/// <summary>
/// The helper class with additional methods.
/// </summary>
internal static class Helpers
{
    /// <summary>
    /// Checks that <paramref name="expression"/> has  the <paramref name="variable"/> variable.
    /// </summary>
    /// <param name="expression">The expression that is checked.</param>
    /// <param name="variable">The variable that can be contained in the expression.</param>
    /// <returns>true if <paramref name="expression"/> has <paramref name="variable"/>; otherwise, false.</returns>
    public static bool HasVariable(IExpression expression, Variable variable)
    {
        if (expression is BinaryExpression bin)
            return HasVariable(bin.Left, variable) || HasVariable(bin.Right, variable);

        if (expression is UnaryExpression un)
            return HasVariable(un.Argument, variable);

        if (expression is DifferentParametersExpression paramExp)
        {
            foreach (var argument in paramExp.Arguments)
                if (HasVariable(argument, variable))
                    return true;

            return false;
        }

        return expression is Variable && expression.Equals(variable);
    }
}