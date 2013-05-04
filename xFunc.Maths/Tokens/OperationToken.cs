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

namespace xFunc.Maths.Tokens
{

    public class OperationToken : IToken
    {

        private Operations operation;
        private int priority;

        public OperationToken(Operations operation)
        {
            this.operation = operation;

            SetPriority();
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

        public override string ToString()
        {
            return "Operation: " + operation;
        }

        private void SetPriority()
        {
            switch (operation)
            {
                case Operations.Addition:
                    priority = 10;
                    break;
                case Operations.Subtraction:
                    priority = 10;
                    break;
                case Operations.Multiplication:
                    priority = 11;
                    break;
                case Operations.Division:
                    priority = 11;
                    break;
                case Operations.Exponentiation:
                    priority = 12;
                    break;
                case Operations.UnaryMinus:
                    priority = 13;
                    break;
                case Operations.Assign:
                    priority = 0;
                    break;
                case Operations.Not:
                    priority = 15;
                    break;
                case Operations.And:
                    priority = 15;
                    break;
                case Operations.Or:
                    priority = 15;
                    break;
                case Operations.XOr:
                    priority = 15;
                    break;
            }
        }

        public int Priority
        {
            get
            {
                return priority;
            }
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
