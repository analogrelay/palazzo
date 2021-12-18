namespace Palazzo.Server.Models;

/// <summary>
/// A request to create a new database.
/// </summary>
public class CreateDatabaseRequest
{
    /// <summary>
    /// The name of the database to create.
    /// </summary>
    public string? Name { get; init; }
}