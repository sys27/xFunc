// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace xFunc.DotnetTool.Options
{
    [Verb("parse", HelpText = "Parse string expression.")]
    public class ParseOptions : BaseOptions
    {
        [Usage(ApplicationAlias = "xfunc")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>
                {
                    new Example(
                        "Parse string expression",
                        new ParseOptions { StringExpression = "1 + 1" })
                };
            }
        }
    }
}