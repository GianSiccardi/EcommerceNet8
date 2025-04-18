﻿using EcommerceNet8.Api.Errors;
using EcommerceNet8.Core.Aplication.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace EcommerceNet8.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Solicitud entrante: {context.Request.Method} {context.Request.Path}");
            _logger.LogInformation($"Headers: {string.Join(", ", context.Request.Headers.Select(h => $"{h.Key}: {h.Value}"))}");

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción capturada: {Message}", ex.Message);
                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case FluentValidation.ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var errors = validationException.Errors.Select(ers => ers.ErrorMessage).ToArray();
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, errors, JsonConvert.SerializeObject(errors)));
                        break;

                    case BadRequestExcpetion badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = JsonConvert.SerializeObject(
                        new CodeErrorException(statusCode, new string[] { ex.Message }, ex.StackTrace));
                }

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result);
            }

            _logger.LogInformation($"Respuesta saliente: {context.Response.StatusCode}");
        }
    }
}
