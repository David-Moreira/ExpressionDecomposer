using System.Linq.Expressions;

namespace ExpressionDecomposer.Models
{
    public class ExpressionDecomposedTree
    {
        public Expression OriginalExpression { get; set; }

        public TreeNode Node { get; set; }

        public ExpressionDecomposedTree(Expression originalExpression)
        {
            OriginalExpression = originalExpression;
        }

        public abstract class TreeNode
        {
            public ExpressionType Operator { get; set; }
        }

        public class AccessorTreeNode : TreeNode
        {
            public string Accessor { get; set; }
        }

        public class MethodCallTreeNode : TreeNode
        {
            public string Accessor { get; set; }
            public string[] Arguments { get; set; }
            public string Method { get; set; }
        }

        public class ConstantTreeNode : TreeNode
        {
            public string Constant { get; set; }
        }

        public class BinaryTreeNode : TreeNode
        {
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
        }

        public class NullTreeNode : TreeNode
        { }
    }
}