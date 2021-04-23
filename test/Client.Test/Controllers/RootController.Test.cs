using Microsoft.AspNetCore.Mvc;
using Plutus.Utility;
using Sample.Client.Controllers;
using Xunit;

namespace Sample.Client.Test.Controllers
{
    public class RootControllerTest
    {
        private readonly RootController _controller;
        public RootControllerTest()
        {
            _controller = new RootController();
        }

        [Fact]
        public void Test_ReturnsOkResult()
        {
            var res = _controller.Monitor() as OkObjectResult;

            Assert.IsType<OkObjectResult>(res);
            Assert.IsType<BaseResponse<object>>(res.Value);
        }
    }
}
