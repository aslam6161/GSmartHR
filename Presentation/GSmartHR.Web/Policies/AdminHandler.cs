using Microsoft.AspNetCore.Authorization;
using GSmartHR.Core;
using GSmartHR.Core.Domain.Users;
using GSmartHR.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using GSmartHR.Services.Users;

namespace GSmartHR.Web.Policies
{
    public class AdminHandler : AuthorizationHandler<AdminHandler>, IAuthorizationRequirement
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminHandler requirement)
        {
            var actionContextAccessor = DependencyResolver.GetService<IActionContextAccessor>();
            var workContext = DependencyResolver.GetService<IWorkContext>();

            if (workContext.IsAuthenticated)
            {
        
                if (workContext.IsInRole("Administrator"))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
