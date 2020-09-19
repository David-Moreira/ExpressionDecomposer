using ExpressionDecomposer.Interface;
using ExpressionDecomposer.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionDecomposer
{
    public class ExpressionDecomposer : IExpressionDecomposer
    {
        public ExpressionDecomposed DecomposeExpression(Expression expression)
        {
            IList<string> propertyNames = new List<string>();
            var memberExpressions = GetMemberExpressions(expression);
            foreach (var member in memberExpressions)
            {
                propertyNames = TraverseMemberExpression(member);
            }
            var decomposed = new ExpressionDecomposed()
            {
                OrderedPropertyNames = propertyNames
            };
            return decomposed;
        }

        private IList<string> TraverseMemberExpression(MemberExpression expression)
        {
            List<string> propertyNames = new List<string>();
            var expressionTraverse = expression;
            while (expressionTraverse is MemberExpression)
            {
                propertyNames.Insert(0, expressionTraverse.Member.Name);
                if (expressionTraverse.Expression is ParameterExpression param)
                    propertyNames.Insert(0, param.Name);

                expressionTraverse = expressionTraverse.Expression as MemberExpression;
            }
            return propertyNames;
        }

        private static IEnumerable<MemberExpression> GetMemberExpressions(Expression body)
        {
            var expressions = new Queue<Expression>(new[] { body });
            while (expressions.Count > 0)
            {
                var expr = expressions.Dequeue();
                if (expr is MemberExpression)
                {
                    yield return ((MemberExpression)expr);
                }
                else if (expr is UnaryExpression)
                {
                    expressions.Enqueue(((UnaryExpression)expr).Operand);
                }
                else if (expr is BinaryExpression)
                {
                    var binary = expr as BinaryExpression;
                    expressions.Enqueue(binary.Left);
                    expressions.Enqueue(binary.Right);
                }
                else if (expr is MethodCallExpression)
                {
                    var method = expr as MethodCallExpression;
                    foreach (var argument in method.Arguments)
                    {
                        expressions.Enqueue(argument);
                    }
                }
                else if (expr is LambdaExpression)
                {
                    expressions.Enqueue(((LambdaExpression)expr).Body);
                }
            }
        }
    }
}