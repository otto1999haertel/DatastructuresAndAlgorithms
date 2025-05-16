namespace TreeLib;

public class DirectoryNode : FileSystemNode
{
    public List<FileSystemNode> Children { get; }

    public DirectoryNode(string name) : base(name)
    {
    }

    public void Add(FileSystemNode node)
    {
    }
}
