﻿// Copyright 2012-2014 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Programming
{

    public static class Bool
    {

        public static int True = 1;
        public static int False = 0;

        public static bool AsBool(this object value)
        {
            if (value is double)
            {
                var num = (double)value;

                return num == False ? false : true;
            }
            if (value is int)
            {
                var num = (int)value;

                return num == False ? false : true;
            }

            return false;
        }

        public static int AsNumber(this bool value)
        {
            return value ? 1 : 0;
        }

    }

}
