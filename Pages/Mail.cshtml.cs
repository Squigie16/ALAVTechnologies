using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LloydStephanieRealty.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using MimeKit.Text;

namespace LloydStephanieRealty.Pages
{
    public class MailModel : PageModel
    {
        private readonly LloydStephanieRealty.Models.MBS_DBContext _context;

        public MailModel(LloydStephanieRealty.Models.MBS_DBContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public ActionResult OnPost()
        {
            foreach (User u in _context.Users)
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("brodsmithjack@gmail.com"));
                email.To.Add(MailboxAddress.Parse(u.Email));
                email.Subject = "Test Email Subject";
                email.Body = new TextPart(TextFormat.Html) { Text = "<h1>WELCOME TO THE NEWSLETTER</h1>" };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("brodsmithjack@gmail.com", "test4321");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            return RedirectToPage("./Index");
        }

    }
}
