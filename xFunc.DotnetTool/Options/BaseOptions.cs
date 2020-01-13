// Copyright 2012-2019 Dmitry Kischenko
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
using CommandLine;

namespace xFunc.DotnetTool.Options
{
    public abstract class BaseOptions : DebugInfoOptions
    {
        protected BaseOptions(string stringExpression, bool debug) : base(debug)
        {
            StringExpression = stringExpression;
        }

        [Value(0, Required = true, MetaName = "String Expression", HelpText = "The string expression.")]
        public string StringExpression { get; }
    }
}