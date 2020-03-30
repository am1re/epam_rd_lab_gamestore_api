using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebUI.Common
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
    
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var fields = new Dictionary<string, string[]>();

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.UnprocessableEntity;
                    fields = validationException.Failures;
                    break;
                case ConflictDataException conflictDataException:
                    code = HttpStatusCode.Conflict;
                    fields = conflictDataException.Failures;
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    fields = badRequestException.Failures;
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            
            var result = JsonConvert.SerializeObject(new
            {
                error = new
                {
                    message = exception.Message,
                    fields,
                }
            });

            return context.Response.WriteAsync(result);
        }
    }
}