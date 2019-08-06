using Authorization.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authorization.Authorization
{
    public class AuthenticationHeaderHandler
    {
        private readonly RequestDelegate _next;
        public AuthenticationHeaderHandler(RequestDelegate d)
        {
            this._next = d;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader == null)
            {
                context.Response.StatusCode = 401; //Unauthorizied
                return;
            }
            else
            {
                var apiKeyV = authHeader.Trim();
                Guid headerKey;
                bool isValid = Guid.TryParse(apiKeyV, out headerKey);
                if (!isValid)
                {
                    InvalidSecurityKeyExceptionhandler(context);
                }
                bool keyFound = false;
                using (var _dbContext = DbContextFactory.CreateCisMainDbContext())
                {
                    
                   var myKey = _dbContext.ApiKeys.Where(k => k.AllowedApi == "SampleApiKey" && k.Id == headerKey).FirstOrDefault();
                    keyFound = (myKey != null);
                }
                var userName = (keyFound) ? "CoursePlannerAPI" : "Unknown";
                if (keyFound)
                {
                    var userNameClaim = new Claim(ClaimTypes.Name, userName);
                    var identity = new ClaimsIdentity(new[] { userNameClaim }, "Authorization");
                    identity.AddClaim(new Claim(ClaimTypes.Role, userName));
                    var principal = new ClaimsPrincipal(identity);
                    context.User = principal;
                    await this._next.Invoke(context);
                }
                else
                {
                    InvalidSecurityKeyExceptionhandler(context);
                }
            }
        }

        private void InvalidSecurityKeyExceptionhandler(HttpContext context)
        {
            string keyNotValidMsg = "Api Key is not valid!";
            context.Response.StatusCode = 401;
            throw new InvalidSecurityKeyException(keyNotValidMsg);

        }
    }
}
