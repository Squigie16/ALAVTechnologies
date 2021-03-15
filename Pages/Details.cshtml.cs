using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LloydStephanieRealty.Models;

namespace LloydStephanieRealty.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly LloydStephanieRealty.Models.MBS_DBContext _context;

        public DetailsModel(LloydStephanieRealty.Models.MBS_DBContext context)
        {
            _context = context;
        }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
