using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Handlers
{
    public class IsMemberHandler : AuthorizationHandler<IsMemberRequirement>
    {
        protected readonly UnitOfWork Unit;
        public IsMemberHandler(TimeContext ctx)
        {
            Unit = new UnitOfWork(ctx);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsMemberRequirement requirement)
        {
            if (context.Resource is AuthorizationFilterContext filterContext)
            {
                string role = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value;
                if (role == "admin" || role == "lead")
                {
                    context.Succeed(requirement);
                    return Task.FromResult(true);
                }
            }
            context.Fail();
            return Task.FromResult(false);
        }
    }
}
