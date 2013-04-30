using System;

namespace xFunc.Maths.Tokens
{
    
    public class OperationToken : IToken
    {

        private Operations operation;

        public OperationToken(Operations operation)
        {
            this.operation = operation;
        }

        public Operations Operation
        {
            get
            {
                return operation;
            }
        }

    }

}
