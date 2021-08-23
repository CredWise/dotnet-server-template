using System;
using Application.Common.Interface.Services;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;
        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void Error(string data)
        {
            _logger.LogError(data);
        }

        public void Error(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        public void Info(string data)
        {
            _logger.LogInformation(data);
        }
    }
}