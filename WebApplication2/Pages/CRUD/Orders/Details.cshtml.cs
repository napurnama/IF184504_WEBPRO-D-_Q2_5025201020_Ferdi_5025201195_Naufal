using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;
using WebApplication2.Auth;
using WebApplication2.Data;

namespace WebApplication2.Pages.CRUD.Orders
{
    public class DetailsModel : DIBasePageModel
    {
        public DetailsModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || Context.Order == null)
            {
                return NotFound();
            }

            var order = await Context.Order.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            Order = order;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Order, OrderOperations.Read);
            var isAdmin = User.IsInRole(ApplicationUserRoles.AdminRole);

            if (isAuthorized.Succeeded == false && isAdmin == false) return Forbid();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int id, OrderStatusConstants status)
        {
            var order = await Context.Order.FindAsync(id);
            if (order == null) return NotFound();

            Order = order;

            var operation = OrderOperations.Cook;
            if (status == OrderStatusConstants.Cooking) operation = OrderOperations.Cook;
            if (status == OrderStatusConstants.Ready) operation = OrderOperations.Cooked;
            if (status == OrderStatusConstants.Delivered) operation = OrderOperations.Deliver;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Order, operation);

            var isAdmin = User.IsInRole(ApplicationUserRoles.AdminRole);

            if (isAuthorized.Succeeded == false && isAdmin == false) return Forbid();

            Order.OrderStatus = status;
            Context.Order.Update(Order);

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
