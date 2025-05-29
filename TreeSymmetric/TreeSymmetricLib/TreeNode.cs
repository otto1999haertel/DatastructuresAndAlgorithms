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

    public bool IsSymmetric(TreeNode startNode)
    {
        //Idee: Tree Traversal => Compare left and right subtrees for left and rights child nodes
        //Tree lineraisieren => Beide liniearisierten bäume müssen entgegengesetzte left and right child nodes haben
        return false;
    }
}
