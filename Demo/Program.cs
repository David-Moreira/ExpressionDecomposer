using ExpressionDecomposer.Interface;
using System;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IExpressionDecomposer decomposer = new ExpressionDecomposer.ExpressionDecomposer();
            ParameterExpression[] para = { Expression.Parameter(typeof(Example), "Example") };
            var teste = DynamicExpressionParser.ParseLambda(new ParsingConfig() { IsCaseSensitive = true }, para, null, "Example.Nested.NestedName.Contains(\"india\") && Example.Nested.NestedName == \"india\"");
            var decomposed = decomposer.DecomposeExpression(teste);
            Console.WriteLine(teste.ToString());
            Console.WriteLine();
            Console.WriteLine("Left: ");
            //Console.Write(decomposed.Left.GetAccessorPath());
            Console.WriteLine("---");
            Console.WriteLine("---");
            Console.WriteLine("");
            Console.WriteLine("Right: ");
            //Console.Write(decomposed.Right.GetAccessorPath());
            Console.ReadKey();
        }
    }

    public class Example
    {
        public string Name;
        public string Country;
        public Nested Nested;
    }

    public class Nested
    {
        public string NestedName;
    }
}