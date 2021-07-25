using System;
using Plutus.Utility;

namespace Application.Common.Enums
{
    public enum ResponseCode
    {
        [ResponseDetailsAttribute("Test Error", HttpStatusCodes.NOT_FOUND)]
        TEST,
    }
}
