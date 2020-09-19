using ExpressionDecomposer.Models;
using System.Linq.Expressions;

namespace ExpressionDecomposer.Interface
{
    public interface IExpressionDecomposer
    {
        ExpressionDecomposed DecomposeExpression(Expression expression);
    }
}