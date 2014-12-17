using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace RoslynCSharp
{
    class Program
    {
        public static void Main(string[] args)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(
            @"using System;
            using System.Collections.Generic;
            using System.Text;
 
            namespace HelloWorld
            {
                class Program
                {
                    static void Main(string[] args)
                    {
                        Console.WriteLine(""Hello, TDN!"");
                    }
                }
            }");

            var root = (CompilationUnitSyntax)tree.GetRoot();
            var compilation = CSharpCompilation.Create("HelloTDN")
                                               .AddReferences(references: new[] { MetadataReference.CreateFromAssembly(typeof(object).Assembly) })
                                               .AddSyntaxTrees(tree);
            var model = compilation.GetSemanticModel(tree);
            var nameInfo = model.GetSymbolInfo(root.Usings[0].Name);
            var systemSymbol = (INamespaceSymbol)nameInfo.Symbol;
            foreach (var ns in systemSymbol.GetNamespaceMembers())
            {
                Console.WriteLine(ns.Name);
            }

            Console.ReadLine();


        }
    }
}
