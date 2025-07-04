﻿using System.ComponentModel.Design.Serialization;
using TreeLib;
namespace TreeExampleTests;

public class TreeExampleTests
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

    [Test]
    public void PreOderTaversal()
    {
        var root = SetUpDirectory();
        List<string> result = new List<string>();
        root.PreOderTraversing(root, result);
        List<string> expected = new List<string>
        {
            "root",
            "docs",
            "a.txt",
            "b.txt",
            "src",
            "main.cs"
        };
        Assert.That(result.SequenceEqual(expected),Is.True, "The pre-order traversal result does not match the expected output.");
    }

    [Test]
    public void PostOderTaversal()
    {
        var root = SetUpDirectory();
        List<string> result = new List<string>();
        root.PostOderTraversing(root,result);
        List<string> expected = new List<string>
        {
            "a.txt",
            "b.txt",
            "docs",
            "main.cs",
            "src",
            "root",
        };
        Assert.That(result.SequenceEqual(expected),Is.True, "The post-order traversal result does not match the expected output.");
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
