using System.Diagnostics.Contracts;

namespace TreeLib;

public class DirectoryNode : FileSystemNode
{
    public List<FileSystemNode> Children { get; }

    public DirectoryNode(string name) : base(name)
    {
        Children = new List<FileSystemNode>();
    }

    public void Add(FileSystemNode node)
    {
        Children.Add(node);
    }

    public int CalculateTotalSize()
    {
        int sum = 0;
        foreach (var child in Children)
        {
            switch (child)
            {
                case FileNode file:
                    sum += file.Size;
                    break;

                case DirectoryNode dir:
                    sum += dir.CalculateTotalSize();
                    break;
            }
        }

        return sum;
    }

    public string PrintTree(DirectoryNode directory, string indent = "")
    {
        string tree = indent + directory.Name + "/" + Environment.NewLine;
        foreach (var child in directory.Children)
        {
            switch (child)
            {
                case FileNode fileNode:
                    tree += indent + "  " + fileNode.Name + $"({fileNode.Size})" + Environment.NewLine;
                    break;

                case DirectoryNode subDir:
                    //Mitgabe des an die Funktion
                    tree += PrintTree(subDir, indent + "  ");
                    break;
            }
        }

        return tree;
    }

    public FileNode FindFile(DirectoryNode startDirectory, string fileName)
    {
        foreach (var child in startDirectory.Children)
        {

            if (child is FileNode fileNode)
            {
                if (fileNode.Name.Equals(fileName))
                {
                    return fileNode;
                }
            }

            if (child is DirectoryNode dir)
            {
                return FindFile(dir, fileName);
            }

        }
        return null;
    }

    public List<string> PreOderTraversing(DirectoryNode directory, List<string> result)
    {
        //Idee: Wurzel -> Kinder - beginnend bei dem, welches zu erst hinzugefügt wurde
        result.Add(directory.Name);
        foreach (var child in directory.Children)
        {

            switch (child)
            {
                case FileNode fileNode:
                    result.Add(fileNode.Name);
                    break;

                case DirectoryNode subDir:
                    //Mitgabe des an die Funktion
                    //result.Add(subDir.Name);
                    PreOderTraversing(subDir, result);
                    break;
            }
        }
        return result;
    }

    public List<string> PostOderTraversing(DirectoryNode directory, List<string> result)
    {
        //Idee: Kinder -> Wurzel - beginnend bei dem, welches zu erst hinzugefügt wurde
        foreach (var child in directory.Children)
        {
            switch (child)
            {
                case FileNode fileNode:
                    result.Add(fileNode.Name);
                    break;

                case DirectoryNode subDir:
                    //Mitgabe des an die Funktion
                    //result.Add(subDir.Name);
                    PostOderTraversing(subDir, result);
                    break;
            }
        }
        result.Add(directory.Name);
        return result;
    }
}
