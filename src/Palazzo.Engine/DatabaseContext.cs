namespace Palazzo;

public interface IDatabaseContext
{
    string Name { get; }
    string Path { get; }
    DatabaseSettings Settings { get; }
}

class DatabaseContext : IDatabaseContext
{
    public string Name { get; }
    public string Path { get; }
    public DatabaseSettings Settings { get; }

    public DatabaseContext(string name, string path, DatabaseSettings settings)
    {
        Name = name;
        Path = path;
        Settings = settings;
    }
}