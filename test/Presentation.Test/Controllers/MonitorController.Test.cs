using Microsoft.AspNetCore.Mvc;
using Plutus.Utility;
using Sample.Presentation.Controllers;
using Xunit;

namespace Sample.Presentation.Test.Controllers
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
            Assert.IsType<BaseResponse<object>>(res.Value);
        }
    }
}
