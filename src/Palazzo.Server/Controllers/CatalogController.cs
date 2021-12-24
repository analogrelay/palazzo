using Microsoft.AspNetCore.Mvc;

namespace Palazzo.Server.Controllers;

[ApiController]
[Route("dbs/{databaseName}/catalog/{catalogName}")]
public class CatalogController: ControllerBase
{
    public CatalogController()
    {
    }

    [HttpPut]
    public Task<IActionResult> WriteObjects([FromBody] IEnumerable<string> objects)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("object/{objectId}")]
    public Task<IActionResult> GetObject(string objectId)
    {
        throw new NotImplementedException();
    }

    Task<ICatalogContext?> GetCatalogAsync()
    {
        throw new NotImplementedException();
    }
}