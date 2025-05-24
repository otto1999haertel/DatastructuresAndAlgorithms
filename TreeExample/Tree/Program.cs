// See https://aka.ms/new-console-template for more information
using TreeLib;

Console.WriteLine("Hello, World!");

// Arrange
var root = new DirectoryNode("root");
var docs = new DirectoryNode("docs");
docs.Add(new FileNode("a.txt", 3));
docs.Add(new FileNode("b.txt", 5));
root.Add(docs);

var src = new DirectoryNode("src");
src.Add(new FileNode("main.cs", 10));
var compiler = new DirectoryNode("Comp");
compiler.Add(new FileNode("Parser",100));
compiler.Add(new FileNode("Compiler", 10));
src.Add(compiler);
root.Add(src);

var ausgabe = root.PrintTree(root);
Console.WriteLine(ausgabe);

var rooNew = SetUpDirectory();
Console.WriteLine(rooNew.PrintTree(rooNew));

DirectoryNode SetUpDirectory()
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
