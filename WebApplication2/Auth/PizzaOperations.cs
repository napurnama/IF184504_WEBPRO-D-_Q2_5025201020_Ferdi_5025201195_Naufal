using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace WebApplication2.Auth
{
    public class PizzaOperations
    {
        public static OperationAuthorizationRequirement Create = new() { Name = OperationConstants.CreateOperationName };
        public static OperationAuthorizationRequirement Read = new() { Name = OperationConstants.ReadOperationName };
        public static OperationAuthorizationRequirement Update = new() { Name = OperationConstants.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete = new() { Name = OperationConstants.DeleteOperationName };
    }
}
