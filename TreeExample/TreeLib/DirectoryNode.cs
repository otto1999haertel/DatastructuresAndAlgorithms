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
}
