namespace Palazzo.Server.Models;

/// <summary>
/// A request to create a new catalog
/// </summary>
public class CreateCatalogRequest
{
    /// <summary>
    /// The name of the new catalog
    /// </summary>
    public string? Name { get; init; }
}