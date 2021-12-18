using System.Text.Json;

using Microsoft.Extensions.Options;

using Palazzo.Configuration;
using Palazzo.Storage;

namespace Palazzo;

class NodeContext : INodeContext
{
    readonly IPalazzoDataFormat _palazzoDataFormat;
    readonly NodeOptions _options;

    public NodeContext(IOptions<NodeOptions> options, IPalazzoDataFormat palazzoDataFormat)
    {
        _palazzoDataFormat = palazzoDataFormat;
        _options = options.Value;
    }

    public async Task<IDatabaseContext> CreateDatabaseAsync(string name, CancellationToken cancellationToken)
    {
        var dbDir = GetDbPath(name);

        // Create a database settings object to represent the database.
        var settings = new DatabaseSettings()
        {
            Version = _palazzoDataFormat.Version,
        };

        if (Directory.Exists(dbDir))
        {
            throw new InvalidOperationException("A database with that name already exists!");
        }

        Directory.CreateDirectory(dbDir);

        // Save the database settings.
        var dbFile = Path.Combine(dbDir, DatabaseSettings.FileName);
        await using var stream = new FileStream(dbFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        await _palazzoDataFormat.SerializeAsync(stream, settings, cancellationToken);

        return new DatabaseContext(name, dbDir, settings);
    }

    public async Task<IDatabaseContext?> GetDatabaseAsync(string name, CancellationToken cancellationToken)
    {
        var dbDir = GetDbPath(name);
        var settingsFile = Path.Combine(dbDir, DatabaseSettings.FileName);
        if (!File.Exists(settingsFile))
        {
            return null;
        }

        await using var stream = new FileStream(settingsFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        var settings = await _palazzoDataFormat.DeserializeAsync<DatabaseSettings>(stream, cancellationToken);

        if (settings is null)
        {
            throw new FormatException("Database settings file failed to deserialize.");
        }

        return new DatabaseContext(name, dbDir, settings);
    }

    string GetDbPath(string name)
    {
        return Path.Combine(_options.StorageRoot, "databases", name);
    }
}