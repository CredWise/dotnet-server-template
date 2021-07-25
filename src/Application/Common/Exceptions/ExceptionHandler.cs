using Application.Common.Enums;
using Plutus.Utility;

namespace Application.Common.Exceptions
{
    public static class ExceptionHandler
    {
        public static void HandleException(ResponseCode responseCode) => throw new BaseException(responseCode.GetDescription(), responseCode.GetStatusCode());

        public static void HandleException(ResponseCode responseCode, object payload) => throw new BaseException(responseCode.GetDescription(), payload, responseCode.GetStatusCode());

        public static void HandleException(string message, object payload) => throw new BaseException(message, payload, HttpStatusCodes.BAD_REQUEST);

        public static void HandleException(object payload) => throw new BaseException(ResponseMessageConstants.VALIDATION_ERROR, payload, HttpStatusCodes.BAD_REQUEST);
    }
}