// Copyright 2012-2017 Dmitry Kischenko
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
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{

    public abstract class TypeAnalyzerBaseTests
    {

        protected ITypeAnalyzer analyzer;

        public TypeAnalyzerBaseTests()
        {
            analyzer = new TypeAnalyzer();
        }

        protected void Test(IExpression exp, ResultType expected)
        {
            var simple = exp.Analyze(analyzer);

            Assert.Equal(expected, simple);
        }

        protected void TestException(IExpression exp)
        {
            Assert.Throws<ParameterTypeMismatchException>(() => exp.Analyze(analyzer));
        }

        protected void TestBinaryException(BinaryExpression exp)
        {
            Assert.Throws<BinaryParameterTypeMismatchException>(() => exp.Analyze(analyzer));
        }

        protected void TestDiffParamException(DifferentParametersExpression exp)
        {
            Assert.Throws<DifferentParameterTypeMismatchException>(() => exp.Analyze(analyzer));
        }

    }

}
