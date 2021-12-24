namespace Palazzo;

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

    public Task<ICatalogContext> CreateCatalogAsync(string name, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ICatalogContext?> GetCatalogAsync(string name, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}