using TreeLib;
namespace TreeTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TotalSize_IsCorrect_ForNestedStructure()
    {
        // Arrange
        var root = new DirectoryNode("root");
        var docs = new DirectoryNode("docs");
        docs.Add(new FileNode("a.txt", 3));
        docs.Add(new FileNode("b.txt", 5));
        root.Add(docs);

        var src = new DirectoryNode("src");
        src.Add(new FileNode("main.cs", 10));
        root.Add(src);

        // Act
        int totalSize = CalculateTotalSize(root);

        // Assert
        Assert.That(totalSize, Is.EqualTo(18));
    }

    private int CalculateTotalSize(FileSystemNode node)
    {
        return 0;
    }
}
