using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Library.Maths
{

    public class MathWorkspace
    {

        private MathParser parser;

        private int countOfExps;
        private List<MathWorkspaceItem> expressions;

        private MathParameterCollection parameters;

        public MathWorkspace()
            : this(20)
        {

        }

        public MathWorkspace(int countOfExp)
        {
            this.countOfExps = countOfExp;
            expressions = new List<MathWorkspaceItem>(countOfExp);
            parser = new MathParser();
            parameters = new MathParameterCollection();
        }

        public void Add(string strExp)
        {
            if (string.IsNullOrWhiteSpace(strExp))
                throw new ArgumentNullException("strExp");

            IMathExpression exp = parser.Parse(strExp);
            MathWorkspaceItem item = new MathWorkspaceItem(strExp, exp);
            if (exp is DerivativeMathExpression)
            {
                item.Answer = exp.Derivative().ToString();
            }
            else if (exp is AssignMathExpression)
            {
                AssignMathExpression assign = (AssignMathExpression)exp;
                assign.Calculate(parameters);
                item.Answer = string.Format("The value '{1}' was assigned to the variable '{0}.", assign.Variable, assign.Value);
            }
            else
            {
                item.Answer = exp.Calculate(parameters).ToString();
            }

            expressions.Add(item);
        }

        public void Remove(MathWorkspaceItem item)
        {
            expressions.Remove(item);
        }

        public void RemoveAt(int index)
        {
            expressions.RemoveAt(index);
        }

        public MathParser Parser
        {
            get
            {
                return parser;
            }
        }

        public int CountOfExpressions
        {
            get
            {
                return countOfExps;
            }
        }

        public IEnumerable<MathWorkspaceItem> Expressions
        {
            get
            {
                return expressions.AsReadOnly();
            }
        }

    }

}
