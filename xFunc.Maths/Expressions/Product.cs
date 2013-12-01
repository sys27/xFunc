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
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the "product" function.
    /// </summary>
    public class Product : DifferentParametersExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        internal Product()
            : base(null, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        /// <exception cref="ArgumentException"></exception>
        public Product(IExpression[] args, int countOfParams)
            : base(args, countOfParams)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length < 2 && args.Length != countOfParams)
                throw new ArgumentException();
            if (countOfParams == 5 && !(args[4] is Variable))
                throw new ArgumentException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="to">The final value (including).</param>
        public Product(IExpression body, IExpression to)
            : base(new[] { body, to }, 2)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="from">The initial value (including).</param>
        /// <param name="to">The final value (including).</param>
        public Product(IExpression body, IExpression from, IExpression to)
            : base(new[] { body, from, to }, 3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="from">The initial value (including).</param>
        /// <param name="to">The final value (including).</param>
        /// <param name="inc">The increment.</param>
        public Product(IExpression body, IExpression from, IExpression to, IExpression inc)
            : base(new[] { body, from, to, inc }, 4)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="from">The initial value (including).</param>
        /// <param name="to">The final value (including).</param>
        /// <param name="inc">The increment.</param>
        /// <param name="variable">The increment variable.</param>
        public Product(IExpression body, IExpression from, IExpression to, IExpression inc, Variable variable)
            : base(new[] { body, from, to, inc, variable }, 5)
        {
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(1607, 6917);
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return base.ToString("product");
        }

        private static string GetVarName(ParameterCollection parameters)
        {
            string variable = "i";
            if (!parameters.ContainsKey(variable))
                return variable;

            for (int i = 1; ; i++)
            {
                var localVar = variable + i;
                if (!parameters.ContainsKey(localVar))
                    return localVar;
            }
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public override double Calculate()
        {
            var body = Body;
            var from = From != null ? From.Calculate() : 1;
            var to = To.Calculate();
            var inc = Increment != null ? Increment.Calculate() : 1;

            var localParams = new ParameterCollection();
            var variable = Variable != null ? Variable.Name : GetVarName(localParams);
            localParams.Add(variable, from);
            var param = new ExpressionParameters(localParams);

            double S = 1;
            for (; from <= to; from += inc)
            {
                localParams[variable] = from;
                S *= body.Calculate(param);
            }

            return S;
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override double Calculate(ExpressionParameters parameters)
        {
            var body = Body;
            var from = From != null ? From.Calculate(parameters) : 1;
            var to = To.Calculate(parameters);
            var inc = Increment != null ? Increment.Calculate(parameters) : 1;

            var localParams = new ParameterCollection(parameters.Parameters.Collection);
            var variable = Variable != null ? Variable.Name : GetVarName(localParams);
            localParams.Add(variable, from);
            var param = new ExpressionParameters(parameters.AngleMeasurement, localParams, parameters.Functions);

            double S = 1;
            for (; from <= to; from += inc)
            {
                localParams[variable] = from;
                S *= body.Calculate(param);
            }

            return S;
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>
        /// Throws an exception.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="NotSupportedException">Always.</exception>
        public override IExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Throws an exception.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override IExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Product(CloneArguments(), countOfParams);
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinCountOfParams
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int MaxCountOfParams
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// Gets the function that is executed on each iteration.
        /// </summary>
        /// <value>
        /// The function that is executed on each iteration.
        /// </value>
        public IExpression Body
        {
            get
            {
                return arguments[0];
            }
        }

        /// <summary>
        /// Gets ghe initial value (including).
        /// </summary>
        /// <value>
        /// The initial value (including).
        /// </value>
        public IExpression From
        {
            get
            {
                if (countOfParams >= 3)
                    return arguments[1];

                return null;
            }
        }

        /// <summary>
        /// Gets the final value (including).
        /// </summary>
        /// <value>
        /// The final value (including).
        /// </value>
        public IExpression To
        {
            get
            {
                if (countOfParams == 2)
                    return arguments[1];

                return arguments[2];
            }
        }

        /// <summary>
        /// Gets the increment.
        /// </summary>
        /// <value>
        /// The increment.
        /// </value>
        public IExpression Increment
        {
            get
            {
                if (countOfParams >= 4)
                    return arguments[3];

                return null;
            }
        }

        /// <summary>
        /// Gets the increment variable.
        /// </summary>
        /// <value>
        /// The increment variable.
        /// </value>
        public Variable Variable
        {
            get
            {
                if (countOfParams == 5)
                    return (Variable)arguments[4];

                return null;
            }
        }

    }

}
