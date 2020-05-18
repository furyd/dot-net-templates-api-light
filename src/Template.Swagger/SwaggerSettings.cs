using Microsoft.OpenApi.Models;

namespace Template.Swagger
{
    public class SwaggerSettings
    {
        public SwaggerSettings()
        {
            Name = "REST API Example";
            Info = new OpenApiInfo
            {
                Title = "REST API Example",
                Description = "REST API Versions"
            };
        }


        public string Name { get; set; }


        public OpenApiInfo Info { get; set; }


        public string RoutePrefix { get; set; }


        public string RoutePrefixWithSlash =>
            string.IsNullOrWhiteSpace(RoutePrefix)
                ? string.Empty
                : RoutePrefix + "/";
    }
}