using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;
using WebApplication2.Auth;
using WebApplication2.Data;

namespace WebApplication2.Pages.CRUD.Orders
{
    public class EditModel : DIBasePageModel
    {
        public EditModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
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
                User, Order, OrderOperations.Update);

            if (isAuthorized.Succeeded == false) return Forbid();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var order = await Context.Order.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
            if (order == null) return NotFound();

            Order.CstId = order.CstId;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Order, OrderOperations.Update);

            if (isAuthorized.Succeeded == false) return Forbid();

            Context.Attach(Order).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderExists(int id)
        {
          return Context.Order.Any(e => e.Id == id);
        }
    }
}
