using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace WebApplication2.Auth
{
    public class OrderOperations
    {
        public static OperationAuthorizationRequirement Create = new() { Name = OrderOperationConstants.CreateOperationName };
        public static OperationAuthorizationRequirement Read = new() { Name = OrderOperationConstants.ReadOperationName };
        public static OperationAuthorizationRequirement Update = new() { Name = OrderOperationConstants.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete = new() { Name = OrderOperationConstants.DeleteOperationName };

        public static OperationAuthorizationRequirement Cook = new() { Name = OrderOperationConstants.CookOperationName };
        public static OperationAuthorizationRequirement Cooked = new() { Name = OrderOperationConstants.CookedOperationName };
        public static OperationAuthorizationRequirement Deliver = new() { Name = OrderOperationConstants.DeliverOperationName };
    }
}
