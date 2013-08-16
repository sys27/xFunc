// Copyright 2012-2013 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions
{

    public class Derivative : IMathExpression
    {

        private IMathExpression parent;
        private IMathExpression expression;
        private Variable variable;

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative"/> class.
        /// </summary>
        public Derivative() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        public Derivative(IMathExpression expression, Variable variable)
        {
            this.expression = expression;
            this.variable = variable;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            var exp = obj as Derivative;
            if (exp == null)
                return false;

            return expression.Equals(exp.Expression) && variable.Equals(exp.Variable);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return string.Format("deriv({0}, {1})", expression, variable);
        }

        public double Calculate()
        {
            return Differentiate().Calculate();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            return Differentiate().Calculate(parameters);
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Differentiate().Calculate(parameters, functions);
        }

        public IMathExpression Differentiate()
        {
            if (expression is Derivative)
                return expression.Differentiate(variable).Differentiate(variable);

            return expression.Differentiate(variable);
        }

        // The local "variable" is ignored.
        public IMathExpression Differentiate(Variable variable)
        {
            if (expression is Derivative)
                return expression.Differentiate(this.variable).Differentiate(this.variable);

            return expression.Differentiate(this.variable);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public IMathExpression Clone()
        {
            return new Derivative(expression.Clone(), (Variable)variable.Clone());
        }

        public IMathExpression Expression
        {
            get
            {
                return expression;
            }
            set
            {
                expression = value;
                if (expression != null)
                    expression.Parent = this;
            }
        }

        public Variable Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
                if (variable != null)
                    variable.Parent = this;
            }
        }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IMathExpression Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

    }

}
