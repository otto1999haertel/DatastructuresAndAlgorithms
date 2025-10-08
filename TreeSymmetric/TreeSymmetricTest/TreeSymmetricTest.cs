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

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result.Equals(true));
    }

    [Test]
    public void AsymmetricTree_ReturnsFalse()
    {
        var tree = new TreeNode(1,
            new TreeNode(2, null, new TreeNode(3)),
            new TreeNode(2, null, new TreeNode(3))
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result.Equals(false));
    }

    [Test]
    public void EmptyTree_ReturnsTrue()
    {
        TreeNode tree = new TreeNode(0);

        var result = tree.IsSymmetric(tree, tree);
        Assert.That(result.Equals(true));
    }

    [Test]
    public void SingleNodeTree_ReturnsTrue()
    {
        var tree = new TreeNode(1);

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result.Equals(true));
    }

    [Test]
    public void SymmetricTree_ThreeLevels_ReturnsTrue()
    {
        var tree = new TreeNode(1,
            new TreeNode(2,
                new TreeNode(3),
                new TreeNode(4)
            ),
            new TreeNode(2,
                new TreeNode(4),
                new TreeNode(3)
            )
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.True);
    }

    [Test]
    public void AsymmetricTree_DifferentValues_ReturnsFalse()
    {
        var tree = new TreeNode(1,
            new TreeNode(2, new TreeNode(3), null),
            new TreeNode(2, null, new TreeNode(99)) // 99 statt 3
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.False);
    }

    [Test]
    public void AsymmetricTree_OnlyOneChild_ReturnsFalse()
    {
        var tree = new TreeNode(1,
            new TreeNode(2, new TreeNode(3), null),
            new TreeNode(2, null, null)
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.False);
    }

    [Test]
    public void AsymmetricDeepTree_WithDifferentValus()
    {
        var tree = new TreeNode(1,
            new TreeNode(2,
                new TreeNode(3),
                new TreeNode(4)
            ),
            new TreeNode(2,
                new TreeNode(4),
                new TreeNode(4)
            )
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.False); // ➜ dein Code gibt hier FALSE zurück!
    }

    [Test]
    public void Fails_With_Final_Code_OnlyOneBranchChecked()
    {
        var tree = new TreeNode(1,
            new TreeNode(2,
                new TreeNode(3),
                new TreeNode(4)
            ),
            new TreeNode(2,
                new TreeNode(4),
                new TreeNode(3)
            )
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.True); // ➜ DEIN CODE GIBT FÄLSCHLICH FALSE ZURÜCK
    }

    [Test]
    public void SymmetricTree_FourLevels_ShouldReturnTrue()
    {
        var tree = new TreeNode(1,
            new TreeNode(2,
                new TreeNode(3,
                    new TreeNode(5),
                    new TreeNode(6)
                ),
                new TreeNode(4,
                    new TreeNode(7),
                    new TreeNode(8)
                )
            ),
            new TreeNode(2,
                new TreeNode(4,
                    new TreeNode(8),
                    new TreeNode(7)
                ),
                new TreeNode(3,
                    new TreeNode(6),
                    new TreeNode(5)
                )
            )
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.True); // ✅ Muss true sein
    }

    [Test]
    public void DeepSymmetricTree_RightSideMismatch_ReturnsFalse()
    {
        var tree = new TreeNode(1,
            new TreeNode(2,
                new TreeNode(3,
                    new TreeNode(5),
                    new TreeNode(6)
                ),
                new TreeNode(4,
                    new TreeNode(7),
                    new TreeNode(8)
                )
            ),
            new TreeNode(2,
                new TreeNode(4,
                    new TreeNode(8),
                    new TreeNode(7)
                ),
                new TreeNode(3,
                    new TreeNode(999), // sollte 6 sein!
                    new TreeNode(5)
                )
            )
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.False); // ← Das MUSS false sein!
    }

    [Test]
    public void OneSidedChildrenOnly_ReturnsFalse()
    {
        var tree = new TreeNode(1,
            new TreeNode(2,
                null,
                new TreeNode(3) // nur rechtes Kind
            ),
            new TreeNode(2,
                null,
                new TreeNode(3) // auch nur rechtes Kind, aber nicht gespiegelt
            )
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.False); // ← dein Code gibt hier fälschlich TRUE zurück!
    }

    [Test]
    public void Asymmetric_MirrorSide_Missing_ReturnsFalse()
    {
        var tree = new TreeNode(1,
            new TreeNode(2, null, new TreeNode(3)),
            new TreeNode(2, null, new TreeNode(3))
        );

        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void LargeSymmetricTree_StressTest_ReturnsTrue()
    {
        var tree = BuildSymmetricTree(14); // 16.383 Knoten
        var sum = tree.CalculateTotalSize(tree);
        var result = tree.IsSymmetric(tree.left, tree.right);
        Assert.That(result, Is.True);
    }

    public TreeNode BuildSymmetricTree(int depth, int value = 1)
    {
        if (depth == 0) return null!;
        return new TreeNode(value,
            BuildSymmetricTree(depth - 1, value + 1),
            BuildSymmetricTree(depth - 1, value + 1)
        );
    }
}

