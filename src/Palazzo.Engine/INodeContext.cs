namespace Palazzo;

/// <summary>
/// A <see cref="NodeContext"/> represents the root object for managing interactions with the engine as a Node in a Palazzo cluster.
/// </summary>
public interface INodeContext
{
    /// <summary>
    /// Creates a database with the specified name.
    /// </summary>
    /// <param name="name">The name of the database to create.</param>
    /// <returns>An <see cref="IDatabaseContext"/> that can be used to perform actions on the database.</returns>
    Task<IDatabaseContext> CreateDatabaseAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an <see cref="IDatabaseContext"/> for an existing database.
    /// </summary>
    /// <param name="name">The name of the database to access.</param>
    /// <returns>An <see cref="IDatabaseContext"/> for the database, or <c>null</c> if the database does not exist.</returns>
    Task<IDatabaseContext?> GetDatabaseAsync(string name, CancellationToken cancellationToken = default);
}