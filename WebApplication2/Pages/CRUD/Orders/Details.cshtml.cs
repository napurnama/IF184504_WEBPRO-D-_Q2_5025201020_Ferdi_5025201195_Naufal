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
            else 
            {
                Order = order;
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Order, OrderOperations.Read);

            if (isAuthorized.Succeeded == false) return Forbid();

            return Page();
        }
    }
}
