namespace TreeSymmetricLib;

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;

    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }

    public bool IsSymmetric(TreeNode? leftNode, TreeNode? rightNode)
    {
        //Idee: Zwei knoten gleichzeitig vergleichen 
        if (leftNode != null && rightNode != null)
        {
            if (leftNode.val == rightNode.val)
            {
                if (leftNode.left != null && rightNode.right != null && (leftNode.right != null && rightNode.left != null))
                {
                    return IsSymmetric(leftNode.left, rightNode.right) && IsSymmetric(leftNode.right, rightNode.left);
                }
                if (leftNode.left == null && rightNode.right == null)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
        return false;

    }
    
    public int CalculateTotalSize(TreeNode? node)
    {
        if (node == null)
        {
            return 0;
        }

        int sum = 0;
        sum += CalculateTotalSize(node.left);
        sum += CalculateTotalSize(node.right);
        
        return sum;
    }
}
