using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Template.Api.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [Route("metadata")]
    public class Metadata : Controller
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<string> Versions([FromServices] IApiVersionDescriptionProvider provider) 
            => provider.ApiVersionDescriptions.Select(
                    providerApiVersionDescription => providerApiVersionDescription.ApiVersion.ToString()
               );
    }
}