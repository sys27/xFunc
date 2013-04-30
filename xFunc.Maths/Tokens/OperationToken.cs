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

        public override bool Equals(object obj)
        {
            OperationToken token = obj as OperationToken;
            if (token != null && this.Operation == token.Operation)
            {
                return true;
            }

            return false;
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
