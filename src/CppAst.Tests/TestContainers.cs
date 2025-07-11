// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using static CppAst.CppTemplateArgument;

namespace CppAst.Tests
{
    public class TestContainers : InlineTestBase
    {
        [Test]
        public void TestSimple()
        {
            var options = new CppParserOptions();
            options.SystemIncludeFolders.Add(Directory.GetCurrentDirectory());
            options.ParseSystemIncludes = true;
            options.ParseMacros = true;
            ParseAssert(@"
#include <test_container.h>
struct bob bb;
#include ""test_container.h""
",
                compilation =>
                {
                    Assert.False(compilation.HasErrors);
                    Assert.AreEqual(1, compilation.System.Classes.Count);
                    var c = compilation.System.Classes[0];
                    Assert.AreEqual("bob", c.Name);
                    Assert.AreEqual(1, c.Fields.Count);
                }
                , options
            );
        }
    }
}