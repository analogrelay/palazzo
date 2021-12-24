namespace Palazzo;

public interface IDatabaseContext
{
    /// <summary>
    /// The name of the database.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The path to the database.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// The global settings for the database.
    /// </summary>
    DatabaseSettings Settings { get; }

    /// <summary>
    /// Creates a catalog with the specified name.
    /// </summary>
    /// <param name="name">The name of the catalog to create.</param>
    /// <returns>An <see cref="ICatalogContext"/> that can be used to perform actions on the catalog.</returns>
    Task<ICatalogContext> CreateCatalogAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an <see cref="ICatalogContext"/> for an existing catalog.
    /// </summary>
    /// <param name="name">The name of the catalog to access.</param>
    /// <returns>An <see cref="ICatalogContext"/> for the catalog, or <c>null</c> if the catalog does not exist.</returns>
    Task<ICatalogContext?> GetCatalogAsync(string name, CancellationToken cancellationToken = default);
}