namespace TreeSymmetricTests;

using TreeSymmetricLib;

public class TreeSymmetricTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SymmetricTree_ReturnsTrue()
    {
        var tree = new TreeNode(1,
            new TreeNode(2, new TreeNode(3), new TreeNode(4)),
            new TreeNode(2, new TreeNode(4), new TreeNode(3))
        );

        var result = tree.IsSymmetric(tree);
        Assert.That(result.Equals(true));
    }

    [Test]
    public void AsymmetricTree_ReturnsFalse()
    {
        var tree = new TreeNode(1,
            new TreeNode(2, null, new TreeNode(3)),
            new TreeNode(2, null, new TreeNode(3))
        );

        var result = tree.IsSymmetric(tree);
        Assert.That(result.Equals(false));
    }

    [Test]
    public void EmptyTree_ReturnsTrue()
    {
        TreeNode? tree = null;

        var result = tree.IsSymmetric(tree);
        Assert.That(result.Equals(null));
    }

    [Test]
    public void SingleNodeTree_ReturnsTrue()
    {
        var tree = new TreeNode(1);

        var result = tree.IsSymmetric(tree);
        Assert.That(result.Equals(true));
    }
}

