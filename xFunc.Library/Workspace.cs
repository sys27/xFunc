using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Library.Logics.Expressions;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Library
{

    public class Workspace
    {

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
            this.maxCountOfExp = maxCountOfExp;
            mathExpressions = new List<MathExpressionItem>();
            logicExpressions = new List<LogicExpressionItem>();

            mathParameters = new MathParameterCollection();
            logicParameters = new LogicParameterCollection();
        }

        public void Add(IMathExpression exp)
        {
            if (exp == null)
                throw new NullReferenceException();

            if (mathExpressions.Count >= maxCountOfExp)
                mathExpressions.RemoveAt(0);

            MathExpressionItem item = new MathExpressionItem()
            {
                Expression = exp
            };

            if (exp is DerivativeMathExpression)
                item.Answer = exp.Derivative().ToString();
            else
                item.Answer = exp.Calculate(mathParameters).ToString();

            mathExpressions.Add(item);
        }

        public void Add(ILogicExpression exp)
        {
            if (exp == null)
                throw new NullReferenceException();

            if (logicExpressions.Count >= maxCountOfExp)
                logicExpressions.RemoveAt(0);

            LogicExpressionItem item = new LogicExpressionItem(exp, exp.Calculate(logicParameters).ToString());

            logicExpressions.Add(item);
        }

        public void Remove(MathExpressionItem item)
        {
            if (item == null)
                throw new NullReferenceException();

            mathExpressions.Remove(item);
        }

        public void Remove(LogicExpressionItem item)
        {
            if (item == null)
                throw new NullReferenceException();

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
