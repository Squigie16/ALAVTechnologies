using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LloydStephanieRealty.Models;

namespace LloydStephanieRealty.Pages
{
    public class SignUpMailingListModel : PageModel
    {
        private readonly LloydStephanieRealty.Models.MBS_DBContext _context;

        public SignUpMailingListModel(LloydStephanieRealty.Models.MBS_DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MailingListUser User { get; set; }
        [BindProperty]
        public string firstName { get; set; }
        [BindProperty]
        public string lastName { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //User.Username = firstName + " " + lastName;
            //User.IsMailing = true;

            _context.MailingListUsers.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
