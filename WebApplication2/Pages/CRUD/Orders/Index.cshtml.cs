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
    public class IndexModel : DIBasePageModel
    {
        public IndexModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (Context.Order != null)
            {
                var orders = from order in Context.Order select order;
                var currentUserId = UserManager.GetUserId(User);

                if (!User.IsInRole(ApplicationUserRoles.AdminRole))
                {
                    orders = orders.Where(order => order.CstId == currentUserId);
                }

                Order = await orders.ToListAsync();
            }
        }
    }
}
