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
using CommandLine.Text;
using System.Collections.Generic;

namespace xFunc.DotnetTool.Options
{
    [Verb("interactive", HelpText = "Run interactive mode.")]
    public class InteractiveOptions : DebugInfoOptions
    {
        public InteractiveOptions(bool debug) : base(debug) { }

        [Usage(ApplicationAlias = "xfunc")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>
                {
                    new Example("Run iteractive mode", new InteractiveOptions(false))
                };
            }
        }
    }
}