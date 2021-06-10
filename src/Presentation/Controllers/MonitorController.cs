using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plutus.Utility;

namespace Sample.Presentation.Controllers
{
    [ApiController]
    [Route("")]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]

    public class MonitorController : PlutusBaseController
    {
        public MonitorController()
        { }

        [HttpGet]
        public IActionResult Server() => CreateResponse();
    }
}