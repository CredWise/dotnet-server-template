using Microsoft.AspNetCore.Mvc;
using Plutus.Utility;

namespace Sample.Client.Controllers
{
    [ApiController]
    [Route("")]
    public class RootController : PlutusBaseController
    {
        public RootController()
        { }

        [HttpGet]
        public IActionResult Monitor()
        {
            return CreateResponse();
        }
    }
}