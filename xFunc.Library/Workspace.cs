using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Library.Logics;
using xFunc.Library.Logics.Expressions;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Library
{

    public class Workspace
    {

        private MathParser mathParser;
        private LogicParser logicParser;

        private int maxCountOfExp;
        private List<MathExpressionItem> mathExpressions;
        private List<LogicExpressionItem> logicExpressions;

        private MathParameterCollection mathParameters;
        private LogicParameterCollection logicParameters;

        public Workspace()
            : this(20)
        {

        }

        public Workspace(int maxCountOfExp)
        {
            mathParser = new MathParser();
            logicParser = new LogicParser();

            this.maxCountOfExp = maxCountOfExp;
            mathExpressions = new List<MathExpressionItem>();
            logicExpressions = new List<LogicExpressionItem>();

            mathParameters = new MathParameterCollection();
            logicParameters = new LogicParameterCollection();
        }

        public void Add(string strExp, ExpressionType expType)
        {
            if (string.IsNullOrWhiteSpace(strExp))
                throw new ArgumentNullException();

            if (expType == ExpressionType.Math)
            {
                IMathExpression exp = mathParser.Parse(strExp);
                MathExpressionItem item = null;
                if (exp is DerivativeMathExpression)
                {
                    item = new MathExpressionItem(strExp, exp, exp.Derivative().ToString());
                }
                else if (exp is AssignMathExpression)
                {
                    exp.Calculate(mathParameters);
                    AssignMathExpression assign = (AssignMathExpression)exp;
                    item = new MathExpressionItem(strExp, exp, string.Format("The value '{1}' was assigned to the variable '{0}.", assign.Variable, assign.Value));
                }
                else
                {
                    item = new MathExpressionItem(strExp, exp, exp.Calculate(mathParameters).ToString());
                }

                mathExpressions.Add(item);
            }
            else if (expType == ExpressionType.Logic)
            {
                ILogicExpression exp = logicParser.Parse(strExp);
                LogicExpressionItem item = new LogicExpressionItem(strExp, exp, exp.Calculate(logicParameters).ToString());

                logicExpressions.Add(item);
            }
        }

        public void Remove(MathExpressionItem item)
        {
            if (item == null)
                throw new ArgumentNullException();

            mathExpressions.Remove(item);
        }

        public void Remove(LogicExpressionItem item)
        {
            if (item == null)
                throw new ArgumentNullException();

            logicExpressions.Remove(item);
        }

        public void RemoveMathAt(int index)
        {
            mathExpressions.RemoveAt(index);
        }

        public void RemoveLogicAt(int index)
        {
            logicExpressions.RemoveAt(index);
        }

        public int MaxCountOfExp
        {
            get
            {
                return maxCountOfExp;
            }
        }

        public IEnumerable<MathExpressionItem> MathExpressions
        {
            get
            {
                return mathExpressions.AsReadOnly();
            }
        }

        public IEnumerable<LogicExpressionItem> LogicExpressions
        {
            get
            {
                return logicExpressions.AsReadOnly();
            }
        }

    }

}
