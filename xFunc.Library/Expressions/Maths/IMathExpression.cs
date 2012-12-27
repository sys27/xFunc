using System;

namespace xFunc.Library.Expressions.Maths
{

    public interface IMathExpression
    {

        double Calculate(MathParameterCollection parameters);
        IMathExpression Derivative();
        IMathExpression Derivative(VariableMathExpression variable);

        IMathExpression Parent { get; set; }

    }

}
