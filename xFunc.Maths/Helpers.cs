// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths;

/// <summary>
/// The helper class with additional methods.
/// </summary>
public static class Helpers
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

    /// <summary>
    /// Gets parameters of expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>A collection of parameters.</returns>
    public static ExpressionParameters GetParameters(IExpression expression)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        var parameters = new SortedSet<Parameter>();
        var variables = GetAllVariables(expression);

        foreach (var variable in variables)
            parameters.Add(new Parameter(variable.Name, false));

        return new ExpressionParameters(parameters, false);
    }

    /// <summary>
    /// Converts the logic expression to collection.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>The collection of expression parts.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="expression"/> variable is null.</exception>
    public static List<IExpression> ConvertExpressionToCollection(IExpression expression)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        var collection = new List<IExpression>();
        ConvertToCollection(expression, collection);

        return collection;
    }

    private static void ConvertToCollection(IExpression expression, List<IExpression> collection)
    {
        if (expression is UnaryExpression un)
        {
            ConvertToCollection(un.Argument, collection);
        }
        else if (expression is BinaryExpression bin)
        {
            ConvertToCollection(bin.Left, collection);
            ConvertToCollection(bin.Right, collection);
        }
        else if (expression is Variable)
        {
            return;
        }

        collection.Add(expression);
    }

    /// <summary>
    /// Gets all variables.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>The list of variables.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="expression"/> is null.</exception>
    public static IEnumerable<Variable> GetAllVariables(IExpression expression)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        var collection = new HashSet<Variable>();
        GetAllVariables(expression, collection);

        return collection;
    }

    private static void GetAllVariables(IExpression expression, HashSet<Variable> collection)
    {
        if (expression is UnaryExpression un)
        {
            GetAllVariables(un.Argument, collection);
        }
        else if (expression is BinaryExpression bin)
        {
            GetAllVariables(bin.Left, collection);
            GetAllVariables(bin.Right, collection);
        }
        else if (expression is DifferentParametersExpression diff)
        {
            foreach (var exp in diff.Arguments)
                GetAllVariables(exp, collection);
        }
        else if (expression is Variable variable)
        {
            collection.Add(variable);
        }
    }
}