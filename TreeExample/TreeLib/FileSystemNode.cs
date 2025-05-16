namespace TreeLib;

public abstract class FileSystemNode
{
    public string Name { get; }

    protected FileSystemNode(string name)
    {
        Name = name;
    }
}
