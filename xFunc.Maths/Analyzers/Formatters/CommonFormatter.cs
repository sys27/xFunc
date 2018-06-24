// Copyright 2012-2018 Dmitry Kischenko
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths.Analyzers.Formatters
{

    /// <summary>
    /// Converts expressions into string.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Analyzers.Formatters.IFormatter" />
    public class CommonFormatter : IFormatter
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonFormatter"/> class.
        /// </summary>
        public CommonFormatter() { }

        private string ToString(UnaryExpression exp, string format)
        {
            return string.Format(format, exp.Argument.Analyze(this));
        }

        private string ToString(BinaryExpression exp, string format)
        {
            return string.Format(format, exp.Left.Analyze(this), exp.Right.Analyze(this));
        }

        private string ToString(DifferentParametersExpression exp, string function)
        {
            var sb = new StringBuilder();

            sb.Append(function).Append('(');
            if (exp.Arguments != null)
            {
                foreach (var item in exp.Arguments)
                    sb.Append(item).Append(", ");
                sb.Remove(sb.Length - 2, 2);
            }
            sb.Append(')');

            return sb.ToString();
        }

        /// <summary>
        /// Analyzes the specified expression. This method should be only used for expessions which are not supported by xFunc (custom expression create by extendening library).
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual string Analyze(IExpression exp)
        {
            return string.Empty;
        }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Abs exp)
        {
            return ToString(exp, "abs({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Add exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is Add))
                return ToString(exp, "({0} + {1})");

            return ToString(exp, "{0} + {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Ceil exp)
        {
            return ToString(exp, "ceil({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Define exp)
        {
            return $"{exp.Key.Analyze(this)} := {exp.Value.Analyze(this)}";
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Del exp)
        {
            return ToString(exp, "del({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Derivative exp)
        {
            return ToString(exp, "deriv");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Div exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} / {1})");

            return ToString(exp, "{0} / {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Exp exp)
        {
            return ToString(exp, "exp({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Fact exp)
        {
            return ToString(exp, "{0}!");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Floor exp)
        {
            return ToString(exp, "floor({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(GCD exp)
        {
            return ToString(exp, "gcd");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Lb exp)
        {
            return ToString(exp, "lb({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(LCM exp)
        {
            return ToString(exp, "lcm");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Lg exp)
        {
            return ToString(exp, "lg({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Ln exp)
        {
            return ToString(exp, "ln({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Log exp)
        {
            return ToString(exp, "log({0}, {1})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Mod exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} % {1})");

            return ToString(exp, "{0} % {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Mul exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is Mul || exp.Parent is Add || exp.Parent is Sub))
                return ToString(exp, "({0} * {1})");

            return ToString(exp, "{0} * {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Number exp)
        {
            if (exp.Value < 0)
            {
                if (exp.Parent is Sub sub && ReferenceEquals(sub.Right, exp))
                    return $"({exp.Value.ToString(CultureInfo.InvariantCulture)})";
            }

            return exp.Value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Pow exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is Add || exp.Parent is Sub || exp.Parent is Mul))
                return ToString(exp, "({0} ^ {1})");

            return ToString(exp, "{0} ^ {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Root exp)
        {
            return ToString(exp, "root({0}, {1})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Round exp)
        {
            return ToString(exp, "round");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Simplify exp)
        {
            return ToString(exp, "simplify({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Sqrt exp)
        {
            return ToString(exp, "sqrt({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Sub exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is Sub))
                return ToString(exp, "({0} - {1})");

            return ToString(exp, "{0} - {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(UnaryMinus exp)
        {
            if (exp.Argument is BinaryExpression)
                return ToString(exp, "-({0})");
            if (exp.Parent is Sub sub && ReferenceEquals(sub.Right, exp))
                return ToString(exp, "(-{0})");

            return ToString(exp, "-{0}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Undefine exp)
        {
            return $"undef({exp.Key.Analyze(this)})";
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(UserFunction exp)
        {
            var sb = new StringBuilder();

            sb.Append(exp.Function).Append('(');
            if (exp.Arguments != null && exp.Arguments.Length > 0)
            {
                foreach (var item in exp.Arguments)
                    sb.Append(item).Append(", ");
                sb.Remove(sb.Length - 2, 2);
            }
            else if (exp.ParametersCount > 0)
            {
                for (int i = 1; i <= exp.ParametersCount; i++)
                    sb.AppendFormat("x{0}, ", i);
                sb.Remove(sb.Length - 2, 2);
            }
            sb.Append(')');

            return sb.ToString();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Variable exp)
        {
            return exp.Name;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public string Analyze(DelegateExpression exp)
        {
            return "{Delegate Expression}";
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Sign exp)
        {
            return ToString(exp, "sign({0})");
        }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Vector exp)
        {
            var sb = new StringBuilder();

            sb.Append('{');
            foreach (var item in exp.Arguments)
                sb.Append(item).Append(", ");
            sb.Remove(sb.Length - 2, 2).Append('}');

            return sb.ToString();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Matrix exp)
        {
            var sb = new StringBuilder();

            sb.Append('{');
            foreach (var item in exp.Arguments)
                sb.Append(item).Append(", ");
            sb.Remove(sb.Length - 2, 2).Append('}');

            return sb.ToString();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Determinant exp)
        {
            return ToString(exp, "det({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Inverse exp)
        {
            return ToString(exp, "inverse({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Transpose exp)
        {
            return ToString(exp, "transpose({0})");
        }

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(ComplexNumber exp)
        {
            var complex = exp.Value;
            if (complex.Real == 0)
            {
                if (complex.Imaginary == 1)
                    return "i";
                if (complex.Imaginary == -1)
                    return "-i";

                return $"{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
            }

            if (complex.Imaginary == 0)
                return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}";

            if (complex.Imaginary > 0)
                return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}+{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";

            return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Conjugate exp)
        {
            return ToString(exp, "conjugate({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Im exp)
        {
            return ToString(exp, "im({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Phase exp)
        {
            return ToString(exp, "phase({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Re exp)
        {
            return ToString(exp, "re({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Reciprocal exp)
        {
            return ToString(exp, "reciprocal({0})");
        }

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arccos exp)
        {
            return ToString(exp, "arccos({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arccot exp)
        {
            return ToString(exp, "arccot({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arccsc exp)
        {
            return ToString(exp, "arccsc({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arcsec exp)
        {
            return ToString(exp, "arcsec({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arcsin exp)
        {
            return ToString(exp, "arcsin({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arctan exp)
        {
            return ToString(exp, "arctan({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Cos exp)
        {
            return ToString(exp, "cos({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Cot exp)
        {
            return ToString(exp, "cot({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Csc exp)
        {
            return ToString(exp, "csc({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Sec exp)
        {
            return ToString(exp, "sec({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Sin exp)
        {
            return ToString(exp, "sin({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Tan exp)
        {
            return ToString(exp, "tan({0})");
        }


        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arcosh exp)
        {
            return ToString(exp, "arcosh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arcoth exp)
        {
            return ToString(exp, "arcoth({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arcsch exp)
        {
            return ToString(exp, "arcsch({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arsech exp)
        {
            return ToString(exp, "arsech({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Arsinh exp)
        {
            return ToString(exp, "arsinh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Artanh exp)
        {
            return ToString(exp, "artanh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Cosh exp)
        {
            return ToString(exp, "cosh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Coth exp)
        {
            return ToString(exp, "coth({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Csch exp)
        {
            return ToString(exp, "csch({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Sech exp)
        {
            return ToString(exp, "sech({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Sinh exp)
        {
            return ToString(exp, "sinh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Tanh exp)
        {
            return ToString(exp, "tanh({0})");
        }

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Avg exp)
        {
            return ToString(exp, "avg");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Count exp)
        {
            return ToString(exp, "count");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Max exp)
        {
            return ToString(exp, "max");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Min exp)
        {
            return ToString(exp, "min");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Product exp)
        {
            return ToString(exp, "product");
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Stdev exp)
        {
            return ToString(exp, "stdev");
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Stdevp exp)
        {
            return ToString(exp, "stdevp");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Sum exp)
        {
            return ToString(exp, "sum");
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Var exp)
        {
            return ToString(exp, "var");
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Varp exp)
        {
            return ToString(exp, "varp");
        }

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Expressions.LogicalAndBitwise.And exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} and {1})");

            return ToString(exp, "{0} and {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Bool exp)
        {
            return exp.Value.ToString();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Equality exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} <=> {1})");

            return ToString(exp, "{0} <=> {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Implication exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} => {1})");

            return ToString(exp, "{0} => {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(NAnd exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} nand {1})");

            return ToString(exp, "{0} nand {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(NOr exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} nor {1})");

            return ToString(exp, "{0} nor {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Not exp)
        {
            return ToString(exp, "not({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Expressions.LogicalAndBitwise.Or exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} or {1})");

            return ToString(exp, "{0} or {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(XOr exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} xor {1})");

            return ToString(exp, "{0} xor {1}");
        }

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(AddAssign exp)
        {
            return ToString(exp, "{0} += {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Expressions.Programming.And exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} && {1})");

            return ToString(exp, "{0} && {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Dec exp)
        {
            return ToString(exp, "{0}--");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(DivAssign exp)
        {
            return ToString(exp, "{0} /= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Equal exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is While))
                return ToString(exp, "({0} == {1})");

            return ToString(exp, "{0} == {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(For exp)
        {
            return ToString(exp, "for");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(GreaterOrEqual exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is While))
                return ToString(exp, "({0} >= {1})");

            return ToString(exp, "{0} >= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(GreaterThan exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} > {1})");

            return ToString(exp, "{0} > {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(If exp)
        {
            return ToString(exp, "if");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Inc exp)
        {
            return ToString(exp, "{0}++");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(LessOrEqual exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is While))
                return ToString(exp, "({0} <= {1})");

            return ToString(exp, "{0} <= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(LessThan exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} < {1})");

            return ToString(exp, "{0} < {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(MulAssign exp)
        {
            return ToString(exp, "{0} *= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(NotEqual exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is While))
                return ToString(exp, "({0} != {1})");

            return ToString(exp, "{0} != {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(Expressions.Programming.Or exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} || {1})");

            return ToString(exp, "{0} || {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(SubAssign exp)
        {
            return ToString(exp, "{0} -= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public string Analyze(While exp)
        {
            return ToString(exp, "while({0}, {1})");
        }

        #endregion Programming

    }

}
