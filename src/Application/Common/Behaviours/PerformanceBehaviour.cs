using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PerformanceBehaviour(ILogger<TRequest> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _timer = new Stopwatch();
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            SetIp();

            var response = await next();

            _timer.Stop();
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;


                _logger.LogWarning("Notification Service Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", requestName, elapsedMilliseconds, request);
            }

            return response;
        }


        private void SetIp()
        {
            string ip = string.Empty;

            ip = SplitCsv(GetHeaderValueAs(IpConstant.IpHeaderForwarded)).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(ip) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null) ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (string.IsNullOrWhiteSpace(ip)) ip = GetHeaderValueAs(IpConstant.IpHeaderRemote);

            _httpContextAccessor.HttpContext.Items.Add(IpConstant.Ip, ip);
        }

        private string GetHeaderValueAs(string headerName)
        {
            string value = _httpContextAccessor.HttpContext.Request.Headers[headerName];

            if (!string.IsNullOrWhiteSpace(value)) return (string)Convert.ChangeType(value.ToString(), typeof(string));

            return default(string);
        }

        private List<string> SplitCsv(string csvList)
        {
            if (string.IsNullOrWhiteSpace(csvList)) return new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable<string>()
                .Select(s => s.Trim())
                .ToList();
        }

    }
}