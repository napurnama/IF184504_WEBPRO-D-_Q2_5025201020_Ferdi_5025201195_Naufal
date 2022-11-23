using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Pizzeria.Models;

namespace WebApplication2.Auth
{
    public class OrderOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Order>
    {
        private UserManager<IdentityUser> _userManager;
        public OrderOwnerAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement, 
            Order order
            )
        {
            if (context.User == null || order == null) return Task.CompletedTask;

            if(requirement.Name != OrderOperationConstants.CreateOperationName 
                && requirement.Name != OrderOperationConstants.ReadOperationName
                && requirement.Name != OrderOperationConstants.UpdateOperationName
                && requirement.Name != OrderOperationConstants.DeleteOperationName) return Task.CompletedTask;

            if (_userManager.GetUserId(context.User) == order.CstId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
