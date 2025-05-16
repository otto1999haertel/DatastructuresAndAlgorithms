using System.ComponentModel.Design.Serialization;
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

        var root = SetUpDirectory();
        // Act
        int totalSize = root.CalculateTotalSize();

        // Assert
        Assert.That(totalSize, Is.EqualTo(18));
    }

    [Test]
    public void IsFileInTree()
    {
        var root = new DirectoryNode("root");
        var docs = new DirectoryNode("docs");
        docs.Add(new FileNode("a.txt", 3));
        docs.Add(new FileNode("b.txt", 5));
        var ourDocs = new DirectoryNode("our docs");
        ourDocs.Add(new FileNode("ourPlan.txt", 3));
        docs.Add(ourDocs);
        root.Add(docs);

        var src = new DirectoryNode("src");
        src.Add(new FileNode("main.cs", 10));
        root.Add(src);

        var foundNode = root.FindFile(root, "a.txt");
        Assert.That(foundNode, Is.Not.Null);
        foundNode = root.FindFile(root, "ourPlan.txt");
        Assert.That(foundNode, Is.Not.Null);
    }

    private DirectoryNode SetUpDirectory()
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

        return root;
    }


}
