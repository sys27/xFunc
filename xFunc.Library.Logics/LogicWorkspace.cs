using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Library.Logics.Expressions;

namespace xFunc.Library.Logics
{

    public class LogicWorkspace
    {

        private LogicParser parser;

        private int countOfExps;
        private List<LogicWorkspaceItem> expressions;

        private LogicParameterCollection parameters;

        public LogicWorkspace()
            : this(20)
        {

        }

        public LogicWorkspace(int countOfExps)
        {
            this.countOfExps = countOfExps;
            this.expressions = new List<LogicWorkspaceItem>(countOfExps);
            this.parser = new LogicParser();
            this.parameters = new LogicParameterCollection();
        }

        public void Add(string strExp)
        {
            if (string.IsNullOrWhiteSpace(strExp))
                throw new ArgumentNullException("strExp");

            while (expressions.Count >= countOfExps)
                expressions.RemoveAt(0);

            ILogicExpression exp = parser.Parse(strExp);
            LogicWorkspaceItem item = new LogicWorkspaceItem(strExp, exp, exp.Calculate(parameters).ToString());

            expressions.Add(item);
        }

        public void Remove(LogicWorkspaceItem item)
        {
            expressions.Remove(item);
        }

        public void RemoveAt(int index)
        {
            expressions.RemoveAt(index);
        }

        public LogicParser Parser
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

        public IEnumerable<LogicWorkspaceItem> Expressions
        {
            get
            {
                return expressions;
            }
        }

    }

}
