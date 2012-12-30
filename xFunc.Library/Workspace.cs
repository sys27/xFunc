using System;
using System.Collections.Generic;
using xFunc.Library.Logics.Expressions;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Library
{

    public class Workspace
    {

        private int maxCountOfExp;
        private List<IMathExpression> mathExpressions;
        private List<ILogicExpression> logicExpressions;

        private MathParameterCollection mathParameters;
        private LogicParameterCollection logicParameters;

        public Workspace()
            : this(20)
        {

        }

        public Workspace(int maxCountOfExp)
        {
            this.maxCountOfExp = maxCountOfExp;
            mathExpressions = new List<IMathExpression>();
            logicExpressions = new List<ILogicExpression>();

            mathParameters = new MathParameterCollection();
            logicParameters = new LogicParameterCollection();
        }

        public void Add(IMathExpression exp)
        {
            if (exp == null)
                throw new NullReferenceException();

            if (mathExpressions.Count >= maxCountOfExp)
                mathExpressions.RemoveAt(0);

            mathExpressions.Add(exp);
        }

        public void Add(ILogicExpression exp)
        {
            if (exp == null)
                throw new NullReferenceException();

            if (logicExpressions.Count >= maxCountOfExp)
                logicExpressions.RemoveAt(0);

            logicExpressions.Add(exp);
        }

        public void Remove(IMathExpression exp)
        {
            if (exp == null)
                throw new NullReferenceException();

            mathExpressions.Remove(exp);
        }

        public void Remove(ILogicExpression exp)
        {
            if (exp == null)
                throw new NullReferenceException();

            logicExpressions.Remove(exp);
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

        public IEnumerable<IMathExpression> MathExpressions
        {
            get
            {
                return mathExpressions.AsReadOnly();
            }
        }

        public IEnumerable<ILogicExpression> LogicExpressions
        {
            get
            {
                return logicExpressions.AsReadOnly();
            }
        }

    }

}
