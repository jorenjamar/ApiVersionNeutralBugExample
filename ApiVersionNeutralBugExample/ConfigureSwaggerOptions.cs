using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace ApiVersionNeutralBugExample
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Configure each API discovered for Swagger Documentation
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        /// <summary>
        /// Configure Swagger Options. Inherited from the Interface
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        /// <summary>
        /// Create information about the version of the API
        /// </summary>
        /// <param name="description"></param>
        /// <returns>Information about the API</returns>
        private OpenApiInfo CreateVersionInfo(
                ApiVersionDescription desc)
        {
            var info = new OpenApiInfo()
            {
                Title = "ApiVersionNeutral bug example",
                Description = "Example API to showcase a bug with the ApiVersionNeutral of the Asp.Versioning.Api. " +
                "When the broken/task endpoint is called without a specified version, an error is thrown. " +
                "This while the AssumeDefaultVersionWhenUnspecified is set to true and version 1.0 is allowed for this endpoint.",
                Version = desc.ApiVersion.ToString()
            };

            if (desc.IsDeprecated)
            {
                info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
            }

            return info;
        }
    }
}
