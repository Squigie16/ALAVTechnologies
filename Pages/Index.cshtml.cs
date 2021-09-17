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
    public class IndexModel : PageModel
    {
        private readonly LloydStephanieRealty.Models.MBS_DBContext _context;

        public IndexModel(LloydStephanieRealty.Models.MBS_DBContext context)
        {
            _context = context;
        }

        public IList<MailingListUser> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.MailingListUsers.ToListAsync();
        }
    }
}
