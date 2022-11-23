namespace WebApplication2.Auth
{
    public class OperationConstants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
    }

    public class OrderOperationConstants : OperationConstants
    {
        public static readonly string CookOperationName = "Cook";
        public static readonly string CookedOperationName = "Cooked";
        public static readonly string DeliverOperationName = "Deliver";
    }

    public class ApplicationUserRoles
    {
        public static readonly string AdminRole = "Admin";
    }
}
