using System.Collections.Generic;

namespace Presentation.Options
{
    public class SwaggerOption
    {
        public bool ShowSwagger { get; set; }
        public string? Title { get; set; }
        public string? Endpoint { get; set; }
        public IList<string>? Versions { get; set; }
    }
}