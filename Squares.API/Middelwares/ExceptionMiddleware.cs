using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Squares.API.Domain.Constant;
using Squares.API.Domain.Dto;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Squares.API.Middelwares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new ResponseDto
            {
                Data = string.Empty,
                MetaData = new ResponseMetaData
                {
                    Code = Constants.ErrorCode,
                    Message = ex.Message
                }
            });
            await context.Response.WriteAsync(result);
        }
    }
}
