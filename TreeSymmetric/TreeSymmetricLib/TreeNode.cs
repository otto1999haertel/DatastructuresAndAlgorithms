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
                if (leftNode.left != null && rightNode.right != null)
                {
                    return IsSymmetric(leftNode.left, rightNode.right);
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
}
