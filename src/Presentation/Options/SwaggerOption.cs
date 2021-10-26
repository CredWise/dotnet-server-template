using System.Collections.Generic;

namespace Presentation.Options
{
    public class SwaggerOption
    {
        public bool ShowSwagger { get; init; }
        public string? Title { get; init; }
        public string? Endpoint { get; init; }
        public string[]? Versions { get; init; }
    }
}