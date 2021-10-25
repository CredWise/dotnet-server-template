using System;

namespace Application.Common.Interface.Services
{
    public interface ILoggerService
    {
        void Info(string data, params object[] args);
        void Warning(string data, params object[] args);
        void Error(string data, params object[] args);
        void Error(Exception ex, params object[] args);
    }
}