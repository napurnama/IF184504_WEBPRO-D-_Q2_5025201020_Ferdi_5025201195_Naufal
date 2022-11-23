using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Pizzeria.Models;

namespace WebApplication2.Auth
{
    public class AdminAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Order>
    {
        public AdminAuthorizationHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Order order)
        {
            if (context.User == null || order == null) return Task.CompletedTask;

            if (requirement.Name != OrderOperationConstants.CookOperationName
                && requirement.Name != OrderOperationConstants.CookedOperationName
                && requirement.Name != OrderOperationConstants.DeliverOperationName
                ) return Task.CompletedTask;
            
            if (context.User.IsInRole(ApplicationUserRoles.AdminRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}
