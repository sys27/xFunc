// Copyright 2012-2014 Dmitry Kischenko
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
    /// Represents the "sum" function.
    /// </summary>
    public class Sum : DifferentParametersExpression
    {

        internal Sum()
            : base(null, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        /// <exception cref="ArgumentException"></exception>
        public Sum(IExpression[] args, int countOfParams)
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
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="to">The final value (including).</param>
        public Sum(IExpression body, IExpression to)
            : base(new[] { body, to }, 2)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="from">The initial value (including).</param>
        /// <param name="to">The final value (including).</param>
        public Sum(IExpression body, IExpression from, IExpression to)
            : base(new[] { body, from, to }, 3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="from">The initial value (including).</param>
        /// <param name="to">The final value (including).</param>
        /// <param name="inc">The increment.</param>
        public Sum(IExpression body, IExpression from, IExpression to, IExpression inc)
            : base(new[] { body, from, to, inc }, 4)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="from">The initial value (including).</param>
        /// <param name="to">The final value (including).</param>
        /// <param name="inc">The increment.</param>
        /// <param name="variable">The increment variable.</param>
        public Sum(IExpression body, IExpression from, IExpression to, IExpression inc, Variable variable)
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
            return base.GetHashCode(6089, 9949);
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return base.ToString("sum");
        }

        private static string GetVarName(ParameterCollection parameters)
        {
            const string variable = "i";
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
        public override object Calculate()
        {
            var body = Body;
            var from = From != null ? (double)From.Calculate() : 1;
            var to = (double)To.Calculate();
            var inc = Increment != null ? (double)Increment.Calculate() : 1;

            var localParams = new ParameterCollection();
            var variable = Variable != null ? Variable.Name : GetVarName(localParams);
            localParams.Add(variable, from);
            var param = new ExpressionParameters(localParams);

            double S = 0;
            for (; from <= to; from += inc)
            {
                localParams[variable] = from;
                S += (double)body.Calculate(param);
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
        public override object Calculate(ExpressionParameters parameters)
        {
            var body = Body;
            var from = From != null ? (double)From.Calculate(parameters) : 1;
            var to = (double)To.Calculate(parameters);
            var inc = Increment != null ? (double)Increment.Calculate(parameters) : 1;

            var localParams = new ParameterCollection(parameters.Parameters.Collection);
            var variable = Variable != null ? Variable.Name : GetVarName(localParams);
            localParams.Add(variable, from);
            var param = new ExpressionParameters(parameters.AngleMeasurement, localParams, parameters.Functions);

            double S = 0;
            for (; from <= to; from += inc)
            {
                localParams[variable] = from;
                S += (double)body.Calculate(param);
            }

            return S;
        }
        
        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Sum(CloneArguments(), arguments.Length);
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
                return countOfParams >= 3 ? arguments[1] : null;
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
                return countOfParams == 2 ? arguments[1] : arguments[2];
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
                return countOfParams >= 4 ? arguments[3] : null;
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
                return countOfParams == 5 ? (Variable)arguments[4] : null;
            }
        }

    }

}
