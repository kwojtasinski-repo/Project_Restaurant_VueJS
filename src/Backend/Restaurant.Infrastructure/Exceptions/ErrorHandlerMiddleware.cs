﻿using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using System.Net;

namespace Restaurant.Infrastructure.Exceptions
{
    public sealed class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception);
            }
        }

        private static async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var (error, code) = Map(exception);
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsJsonAsync(error);
        }

        private static (Error error, HttpStatusCode code) Map(Exception exception)
           => exception switch
           {
               DomainException ex => (new Error(GetErrorCode(ex), ex.Message), HttpStatusCode.BadRequest),
               Application.Exceptions.ApplicationException ex => (new Error(GetErrorCode(ex), ex.Message), HttpStatusCode.BadRequest),
               _ => (new Error("error", "There was an error."), HttpStatusCode.InternalServerError)
           };

        private record Error(string Code, string Message);

        private static string GetErrorCode(Exception exception)
            => exception.GetType().Name.Replace("Exception", string.Empty).Underscore();
    }
}
