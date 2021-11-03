// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions;

namespace xFunc.Maths.Analyzers
{
    /// <summary>
    /// The interface for differentiator.
    /// </summary>
    public interface IDifferentiator : IAnalyzer<IExpression, DifferentiatorContext>
    {
    }
}