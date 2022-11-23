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
    public class DeleteModel : DIBasePageModel
    {
        public DeleteModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
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
            else 
            {
                Order = order;
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Order, OrderOperations.Delete);

            if (isAuthorized.Succeeded == false) return Forbid();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || Context.Order == null)
            {
                return NotFound();
            }
            var order = await Context.Order.FindAsync(id);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, order, OrderOperations.Delete);

            if (isAuthorized.Succeeded == false) return Forbid();

            if (order != null)
            {
                Order = order;
                Context.Order.Remove(Order);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
