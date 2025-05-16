namespace TreeLib;

public class FileNode : FileSystemNode
{
    public int Size { get; }

    public FileNode(string name, int size) : base(name)
    {
    }
}
