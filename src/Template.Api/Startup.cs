using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Template.Domain.V1.Implementation;
using Template.Domain.V1.Interfaces;
using Template.Swagger;

namespace Template.Api
{
    public sealed class Startup
    {
        private const string ApiVersioningKey = "Api:Versioning";
        private const string ApiExplorerKey = "Api:Explorer";

        private readonly IConfiguration _configuration;

        private static IEnumerable<Assembly> Assemblies => AppDomain.CurrentDomain.GetAssemblies();

        public Startup(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IExample<IActionResult>, Example>();

            services.AddAutoMapper(Assemblies);

            services.AddApiVersioning(options => _configuration.GetSection(ApiVersioningKey).Get<ApiVersioningOptions>());

            services.AddVersionedApiExplorer(options => _configuration.GetSection(ApiExplorerKey).Get<ApiExplorerOptions>());

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

            services.AddSwaggerGen(options =>
            {
                options.ResolveConflictingActions(apiDescription => apiDescription.First());
            });

            services.AddMvc().AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblies(Assemblies));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionDescriptionProvider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var apiVersionDescription in versionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{apiVersionDescription.GroupName}/swagger.json", apiVersionDescription.GroupName.ToUpperInvariant());
                }

                options.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
