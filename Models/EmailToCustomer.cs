using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace LloydStephanieRealty.Models
{
    public class EmailToCustomer
    {
        private string FromEmailAddress = "alavtechnoreply@gmail.com";
        private string FromEmailAddressPassword = "AlavTech123!";
        
        public void SendEmailToGroup(string subject, string contents, IQueryable<MailingListUser> mailingListUsers)
        {
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(FromEmailAddress, FromEmailAddressPassword);
            foreach(MailingListUser mlUser in mailingListUsers)
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(FromEmailAddress));
                email.To.Add(MailboxAddress.Parse(mlUser.Email));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = contents };
                smtp.Send(email);
            }
            smtp.Disconnect(true);
        }

        public void SendConfirmationEmail(string subject, string contents, string emailAddress)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(FromEmailAddress));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = contents };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(FromEmailAddress, FromEmailAddressPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
