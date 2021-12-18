namespace Palazzo.Server.Models;

public class GetDatabaseResponse
{
    /// <summary>
    /// The name of the database.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// The version of the database format used.
    /// </summary>
    public Version Version { get; init; } = null!;

    /// <summary>
    /// The path to the database files on disk.
    /// </summary>
    public string Path { get; init; } = null!;
}