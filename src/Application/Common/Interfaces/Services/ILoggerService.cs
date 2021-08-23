using System;

namespace Application.Common.Interface.Services
{
    public interface ILoggerService
    {
        void Info(string data);
        void Error(string data);
        void Error(Exception ex);
    }
}