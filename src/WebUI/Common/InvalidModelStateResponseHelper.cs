using System.Linq;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Common
{
    public static class InvalidModelStateResponseHelper
    {
        public static IActionResult InvalidModelStateResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            
            throw new BadRequestException(errors);
        }
    }
}