using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Template.Swagger
{
    public sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly SwaggerSettings _settings;

 
        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider versionDescriptionProvider,
                                          IOptions<SwaggerSettings> swaggerSettings)
        {

            this._provider = versionDescriptionProvider;
            this._settings = swaggerSettings.Value ?? new SwaggerSettings();
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            options.OperationFilter<DefaultValues>();

            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();

            AddSwaggerDocumentForEachDiscoveredApiVersion(options);
            //SetCommentsPathForSwaggerJsonAndUi(options);
        }

        private void AddSwaggerDocumentForEachDiscoveredApiVersion(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                _settings.Info.Version = description.ApiVersion.ToString();

                if (description.IsDeprecated)
                {
                    _settings.Info.Description += " - DEPRECATED";
                }

                options.SwaggerDoc(description.GroupName, _settings.Info);
            }
        }

        private static void SetCommentsPathForSwaggerJsonAndUi(SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
    }
}
