using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plutus.Utility;

namespace Presentation.Controllers;

[ApiController]
[Route("")]
[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
public class MonitorController : PlutusBaseController
{
    public MonitorController()
    { }

    [HttpGet]
    public IActionResult Server() => CreateResponse();
    [HttpGet("hello")]
    public async Task<IActionResult> Get()
    {
        // await this.Mediator.Send()

        return CreateResponse();
    }

}