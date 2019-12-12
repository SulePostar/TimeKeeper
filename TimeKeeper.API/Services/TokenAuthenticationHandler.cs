using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Services
{
    public class TokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        protected readonly UnitOfWork _unit;
        protected readonly AccessHandler _access;

        public TokenAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            TimeContext context) : base(options, logger, encoder, clock)
        {
            _unit = new UnitOfWork(context);
            _access = new AccessHandler(_unit);
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                try
                {
                    var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                    AuthenticationTicket ticket = _access.CheckToken(authHeader.Parameter, Scheme.Name, Response.Headers);
                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
                catch
                {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid authorization header"));
                }
            }
            return Task.FromResult(AuthenticateResult.Fail("No credentials present"));
        }
    }
}
