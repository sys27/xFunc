// Copyright 2012-2016 Dmitry Kischenko
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
using xFunc.Maths;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests
{

    public class DefaultDependencyResolverTest
    {

        private ISimplifier simplifier;
        private IDifferentiator differentiator;
        private IDependencyResolver resolver;

        public DefaultDependencyResolverTest()
        {
            this.simplifier = new Simplifier();
            this.differentiator = new Differentiator();

            this.resolver = new DefaultDependencyResolver(
                new Type[] { typeof(ISimplifier), typeof(IDifferentiator) },
                new object[] { this.simplifier, this.differentiator });
        }

        [Fact]
        public void NullTypesTest()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultDependencyResolver(null, null));
        }

        [Fact]
        public void NullObjectsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultDependencyResolver(new Type[] { }, null));
        }

        [Fact]
        public void LengthTest()
        {
            Assert.Throws<ArgumentException>(() => new DefaultDependencyResolver(new Type[] { typeof(object) }, new object[] { }));
        }

        [Fact]
        public void SimplifyInjectTest()
        {
            var simp = new Simplify(new Number(2));
            resolver.Resolve(simp);

            Assert.NotNull(simp.Simplifier);
            Assert.Equal(this.simplifier, simp.Simplifier);
        }

        [Fact]
        public void DiffInjectTest()
        {
            var deriv = new Derivative(new Variable("x"), new Variable("x"));
            resolver.Resolve(deriv);

            Assert.NotNull(deriv.Differentiator);
            Assert.Equal(this.differentiator, deriv.Differentiator);
        }

    }

}
