namespace Boovey.Web.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Services.Exceptions;
    using Models.Responses;

    public class ErrorHandler
    {
        private readonly RequestDelegate next;

        public ErrorHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case ArgumentException argumentEx:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case KeyNotFoundException keyNotFoundEx:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;                    
                    case ResourceNotFoundException resourceNotFoundEx:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;                    
                    case ResourceAlreadyExistsException resourceAlreadyExistsEx:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case UnauthorizedAccessException unauthorizedAccessEx:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case NullReferenceException nullReferenceEx:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new ErrorMessageModel { Message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
