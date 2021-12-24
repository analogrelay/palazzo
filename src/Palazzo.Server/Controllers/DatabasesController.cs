using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using Palazzo.Server.Models;

namespace Palazzo.Server.Controllers;

[ApiController]
[Route("dbs")]
public class DatabasesController : ControllerBase
{
    readonly INodeContext _nodeContext;

    public DatabasesController(INodeContext nodeContext)
    {
        _nodeContext = nodeContext;
    }

    /// <summary>
    /// Creates a new database.
    /// </summary>
    /// <param name="request">The settings to use to create the new database.</param>
    /// <response code="201">If the database was created.</response>
    /// <response code="400">If the request was invalid.</response>
    /// <response code="409">If there is already a database with that name.</response>
    [HttpPost]
    public async Task<IActionResult> CreateDatabaseAsync([FromBody] CreateDatabaseRequest request, CancellationToken cancellationToken)
    {
        if (request.Name is not { Length: > 0 })
        {
            return BadRequest(Problems.MissingRequiredArgument(nameof(request.Name)));
        }
        await _nodeContext.CreateDatabaseAsync(request.Name, cancellationToken);

        var location = Url.Action("Get", new { name = request.Name });
        return Created(location ?? string.Empty, null);
    }

    /// <summary>
    /// Retrieves metadata for the requested database.
    /// </summary>
    /// <param name="name">The name of the database to get metadata for.</param>
    /// <response code="200">Returns the database metadata.</response>
    /// <response code="404">If the database does not exist.</response>
    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> GetDatabaseAsync(string name, CancellationToken cancellationToken)
    {
        var db = await _nodeContext.GetDatabaseAsync(name, cancellationToken);
        if (db is null)
        {
            return NotFound(Problems.ObjectNotFound("Database", name));
        }

        return Ok(new GetDatabaseResponse() { Name = db.Name, Path = db.Path, Version = db.Settings.Version, });
    }

    /// <summary>
    /// Creates a new catalog.
    /// </summary>
    /// <param name="dbName">The database in which to create the catalog.</param>
    /// <param name="request">The settings to use to create the new database.</param>
    /// <response code="201">If the catalog was created.</response>
    /// <response code="404">If the database does not exist.</response>
    /// <response code="400">If the request was invalid.</response>
    /// <response code="409">If there is already a catalog with that name.</response>
    [HttpPost]
    [Route("{dbName}/catalogs")]
    public async Task<IActionResult> CreateCatalogAsync(string dbName, [FromBody] CreateCatalogRequest request,
        CancellationToken cancellationToken)
    {
        var db = await _nodeContext.GetDatabaseAsync(dbName, cancellationToken);
        if (db is null)
        {
            return NotFound(Problems.ObjectNotFound("Database", dbName));
        }

        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves metadata for the requested catalog.
    /// </summary>
    /// <param name="dbName">The name of the database to get metadata for.</param>
    /// <param name="name">The name of the catalog to get metadata for.</param>
    /// <response code="200">Returns the catalog metadata.</response>
    /// <response code="404">If the catalog or database does not exist.</response>
    [HttpGet]
    [Route("{dbName}/catalogs/{name}")]
    public Task<IActionResult> GetCatalogAsync(string dbName, string name, [FromBody] CreateCatalogRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}