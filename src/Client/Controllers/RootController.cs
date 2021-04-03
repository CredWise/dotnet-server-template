using Microsoft.AspNetCore.Mvc;
using Plutus.Utility.Controller;

namespace Sample.Client.Controllers
{
    [ApiController]
    [Route("")]
    public class RootController : BaseController
    {
        public RootController()
        { }

        [HttpGet]
        public IActionResult Test()
        {
            return CreateResponse();
        }
    }
}