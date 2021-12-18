using Microsoft.AspNetCore.Mvc;

using Palazzo.Server.Models;

namespace Palazzo.Server.Controllers;

[ApiController]
[Route("databases")]
public class DatabasesController : ControllerBase
{
    readonly INodeContext _nodeContext;

    public DatabasesController(INodeContext nodeContext)
    {
        _nodeContext = nodeContext;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDatabaseRequest request, CancellationToken cancellationToken)
    {
        if (request.Name is not { Length: > 0 })
        {
            return BadRequest(Problems.MissingRequiredArgument(nameof(request.Name)));
        }
        await _nodeContext.CreateDatabaseAsync(request.Name, cancellationToken);

        var location = Url.Action("Get", new { name = request.Name });
        return Created(location ?? string.Empty, null);
    }

    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> Get(string name, CancellationToken cancellationToken)
    {
        var db = await _nodeContext.GetDatabaseAsync(name, cancellationToken);
        if (db is null)
        {
            return NotFound(Problems.ObjectNotFound("Database", name));
        }

        return Ok(new GetDatabaseResponse() { Name = db.Name, Path = db.Path, Version = db.Settings.Version, });
    }
}