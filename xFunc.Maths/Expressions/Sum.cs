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

    /// <summary>
    /// Represents the "sum" function.
    /// </summary>
    public class Sum : DifferentParametersExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        internal Sum()
            : base(null, -1)
        {
        }

        public Sum(IMathExpression[] args, int countOfParams)
            : base(args, countOfParams)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length < 2 && args.Length != countOfParams)
                throw new ArgumentException();
            if (countOfParams == 5 && !(args[4] is Variable))
                // todo: ...
                throw new Exception();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="to">The final value (including).</param>
        public Sum(IMathExpression body, IMathExpression to)
            : base(new[] { body, to }, 2)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        /// <param name="body">The function that is executed on each iteration.</param>
        /// <param name="from">The initial value (including).</param>
        /// <param name="to">The final value (including).</param>
        public Sum(IMathExpression body, IMathExpression from, IMathExpression to)
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
        public Sum(IMathExpression body, IMathExpression from, IMathExpression to, IMathExpression inc)
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
        public Sum(IMathExpression body, IMathExpression from, IMathExpression to, IMathExpression inc, Variable variable)
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

        private string GetVarName(MathParameterCollection parameters)
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

        public override double Calculate()
        {
            var body = Body;
            var from = From != null ? From.Calculate() : 1;
            var to = To.Calculate();
            var inc = Increment != null ? Increment.Calculate() : 1;

            var localParams = new MathParameterCollection();
            var variable = Variable != null ? Variable.Name : GetVarName(localParams);
            localParams.Add(variable, from);
            var param = new ExpressionParameters(localParams);

            double S = 0;
            for (; from <= to; from += inc)
            {
                localParams[variable] = from;
                S += body.Calculate(param);
            }

            return S;
        }

        public override double Calculate(ExpressionParameters parameters)
        {
            var body = Body;
            var from = From != null ? From.Calculate(parameters) : 1;
            var to = To.Calculate(parameters);
            var inc = Increment != null ? Increment.Calculate(parameters) : 1;

            var localParams = new MathParameterCollection(parameters.Parameters.Collection);
            var variable = Variable != null ? Variable.Name : GetVarName(localParams);
            localParams.Add(variable, from);
            var param = new ExpressionParameters(parameters.AngleMeasurement, localParams, parameters.Functions);

            double S = 0;
            for (; from <= to; from += inc)
            {
                localParams[variable] = from;
                S += body.Calculate(param);
            }

            return S;
        }

        public override IMathExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance of the <see cref="IMathExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IMathExpression" /> that is a clone of this instance.
        /// </returns>
        public override IMathExpression Clone()
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

        public IMathExpression Body
        {
            get
            {
                return arguments[0];
            }
        }

        public IMathExpression From
        {
            get
            {
                if (countOfParams >= 3)
                    return arguments[1];

                return null;
            }
        }

        public IMathExpression To
        {
            get
            {
                if (countOfParams == 2)
                    return arguments[1];

                return arguments[2];
            }
        }

        public IMathExpression Increment
        {
            get
            {
                if (countOfParams >= 4)
                    return arguments[3];

                return null;
            }
        }

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
