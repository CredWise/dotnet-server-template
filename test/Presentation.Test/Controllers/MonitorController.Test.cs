using Microsoft.AspNetCore.Mvc;
using Plutus.Utility;
using Presentation.Controllers;
using Xunit;

namespace Presentation.Test.Controllers
{
    public class MonitorControllerTest
    {
        private readonly MonitorController _controller;
        public MonitorControllerTest()
        {
            _controller = new MonitorController();
        }

        [Fact]
        public void Test_ReturnsOkResult()
        {
            var res = _controller.Server() as OkObjectResult;

            Assert.IsType<OkObjectResult>(res);
            Assert.IsType<BaseResponse>(res.Value);
        }
    }
}
