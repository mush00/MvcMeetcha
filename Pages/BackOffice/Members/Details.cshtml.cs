using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using MvcMeetcha.Data;
using MvcMeetcha.Pages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MvcMeetcha.Pages.BackOffice.Members
{
    public class DetailsModel: CustomPageModel
    {
        public DetailsModel(
            UserManager<Account> userManager,
            AppDbContext dbContext):
            base(userManager, dbContext)
        {
        }

        public Member Member { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            await Initialize();

            if (id == null)
            {
                return NotFound();
            }

            var member = await _dbContext.Members
                .Include(a => a.Account)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (member == null)
            {
                return NotFound();
            }

            Member = member;

            return Page();
        }
    }
}
