using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Constants;
using Application.Common.Interface.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// PerformanceAndIpBehaviour checks the performance of each request. If the request take more than 500 milliseconds, it would log a warning. It also set the ip making the request to HttpContext.Item
    /// </summary>
    public class PerformanceAndIpBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly Stopwatch _timer;
        private readonly ILoggerService _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PerformanceAndIpBehaviour(ILoggerService logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _timer = new Stopwatch();
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            SetIp();

            _timer.Start();

            var response = await next();

            _timer.Stop();

            long elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500) _logger.Warning("Notification Service Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", typeof(TResponse).Name, elapsedMilliseconds, request!);

            return response;
        }


        private void SetIp()
        {
            string? ip = string.Empty;
            HttpContext httpContext = _httpContextAccessor.HttpContext!;

            ip = GetIpFromCsv(GetHeaderValueAs(IpConstant.IpHeaderForwarded));

            if (string.IsNullOrWhiteSpace(ip) && httpContext.Connection.RemoteIpAddress != null) ip = httpContext.Connection.RemoteIpAddress.ToString();

            if (string.IsNullOrWhiteSpace(ip)) ip = GetHeaderValueAs(IpConstant.IpHeaderRemote);

            httpContext.Items.Add(IpConstant.Ip, ip);
        }

        private string? GetHeaderValueAs(string headerName)
        {
            string value = _httpContextAccessor.HttpContext!.Request.Headers[headerName];

            if (!string.IsNullOrWhiteSpace(value)) return (string)Convert.ChangeType(value.ToString(), typeof(string));

            return default(string);
        }

        private string? GetIpFromCsv(string? csvList)
        {
            if (string.IsNullOrWhiteSpace(csvList)) return default(string);

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable<string>()
                .Select(s => s.Trim())
                .FirstOrDefault();
        }

    }
}