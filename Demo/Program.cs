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
            var teste = DynamicExpressionParser.ParseLambda(new ParsingConfig() { IsCaseSensitive = true }, para, null, "Example.Nested.NestedName == \"india\"");
            var decomposed = decomposer.DecomposeExpression(teste);
            Console.Write(decomposed.GetPath());
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