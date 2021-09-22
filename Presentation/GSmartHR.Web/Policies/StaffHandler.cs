using GSmartHR.Core;
using GSmartHR.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSmartHR.Web.Policies
{

    public class StaffHandler : AuthorizationHandler<StaffHandler>, IAuthorizationRequirement
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StaffHandler requirement)
        {
            var actionContextAccessor = DependencyResolver.GetService<IActionContextAccessor>();
            var workContext = DependencyResolver.GetService<IWorkContext>();

            if (workContext.IsAuthenticated)
            {

                if (workContext.IsInRole("Staff"))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
