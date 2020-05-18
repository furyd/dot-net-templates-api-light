using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template.Domain.V1.Interfaces;
using Template.Exceptions;

namespace Template.Domain.V1.Implementation
{
    public class Example : IExample<IActionResult>
    {   

        public async Task<IActionResult> Get(Models.Request.Example model)
        {
            return await Task.FromResult(new OkResult());
        }

        public async Task<IActionResult> Post(Models.Request.Example model)
        {
            return await Task.FromResult(MapException(new ExampleException()));
        }

        private static IActionResult MapException(Exception baseException)
        {
            switch (baseException)
            {
                case ExampleException exception:
                    return new BadRequestObjectResult(new ProblemDetails{ Type = baseException.GetType().FullName, Detail = exception.ExampleProperty});
                default:
                    return new BadRequestObjectResult(new ProblemDetails { Type = baseException.GetType().FullName, Detail = baseException.Message });
            }
        }
    }
}