// Copyright 2012-2019 Dmitry Kischenko
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

namespace xFunc.Maths.Analyzers.TypeAnalyzers
{
    /// <summary>
    /// Type Analyzer checks the expression tree for argument type and result type. If result type is Undefined, then Type Analyzer cannot determine the right type and bypass current expression.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Analyzers.IAnalyzer{ResultType}" />
    public interface ITypeAnalyzer : IAnalyzer<ResultType>
    {

    }

}
