using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineConnector.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];

            //do the checking
            if (token != "2a85c8bc-8112-485d-acb5-1c93db3c4d82")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access denied! Please check your Authorization token.");
                return;
            }

            //pass request further if correct
            await _next(context);
        }
    }
}
