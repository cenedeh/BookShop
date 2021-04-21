using BookShop.Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookShop.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exception)
            {
                case BooksValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    var errorArray = validationException.Failures.SelectMany(x => x.Value).ToArray();
                    var error = string.Join(";", errorArray);
                    result = error;
                    break;
                case UserAuthenticationException userAuthenticationException:
                    code = HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    result = JsonConvert.SerializeObject(string.IsNullOrEmpty(exception.Message) ? "An invalid entity exception occurred" : exception.Message);
                    break;
                case NotFoundExceptionException notFoundExceptionException:
                    code = HttpStatusCode.NotFound;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    result = JsonConvert.SerializeObject(string.IsNullOrEmpty(exception.Message) ? "An invalid entity exception occurred" : exception.Message);
                    break;
                case DuplicateExceptionException duplicateExceptionException:
                    code = HttpStatusCode.NotFound;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    result = JsonConvert.SerializeObject(string.IsNullOrEmpty(exception.Message) ? "An invalid entity exception occurred" : exception.Message);
                    break;
                default:
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (string.IsNullOrEmpty(result))
            {
                result = "An error occurred. Please contact bookstore support";
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
