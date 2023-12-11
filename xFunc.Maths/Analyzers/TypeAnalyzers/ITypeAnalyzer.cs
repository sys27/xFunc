// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Analyzers.TypeAnalyzers;

/// <summary>
/// Type Analyzer checks the expression tree for argument type and result type. If result type is Undefined, then Type Analyzer cannot determine the right type and bypass current expression.
/// </summary>
/// <seealso cref="IAnalyzer{ResultType}" />
public interface ITypeAnalyzer : IAnalyzer<ResultTypes>;