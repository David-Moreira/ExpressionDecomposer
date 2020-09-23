using ExpressionDecomposer.Interface;
using ExpressionDecomposer.Models;
using System.Linq;
using System.Linq.Expressions;
using static ExpressionDecomposer.Models.ExpressionDecomposedTree;

namespace ExpressionDecomposer
{
    public class ExpressionDecomposer : IExpressionDecomposer
    {
        public ExpressionDecomposedTree DecomposeExpression(Expression expression)
        {
            return FillTree(expression);
        }

        private ExpressionDecomposedTree FillTree(Expression body)
        {
            ExpressionDecomposedTree tree = new ExpressionDecomposedTree(body);
            tree.Node = FillTreeNode(body);

            return tree;
        }

        private TreeNode FillTreeNode(Expression expression)
        {
            TreeNode treeNode = new NullTreeNode();
            if (expression is BinaryExpression binary)
            {
                treeNode = new BinaryTreeNode()
                {
                    Operator = expression.NodeType,
                    Left = FillTreeNode(binary.Left),
                    Right = FillTreeNode(binary.Right),
                };
            }
            else if (expression is MemberExpression)
            {
                treeNode = new AccessorTreeNode()
                {
                    Accessor = expression.ToString()
                };
            }
            else if (expression is ConstantExpression)
            {
                treeNode = new ConstantTreeNode()
                {
                    Constant = expression.ToString()
                };
            }
            else if (expression is MethodCallExpression methodCall)
            {
                treeNode = new MethodCallTreeNode()
                {
                    Method = methodCall.Method.Name,
                    Arguments = methodCall.Arguments.Select(x => x.ToString()).ToArray(),
                    Accessor = methodCall.Object.ToString()
                };
            }
            else if (expression is LambdaExpression lambdaExpr)
                treeNode = FillTreeNode(lambdaExpr.Body);

            return treeNode;
        }
    }
}