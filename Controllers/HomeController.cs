using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LloydStephanieRealty.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Net;

namespace LloydStephanieRealty.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository repository;
        private MBS_DBContext _dbContext;
        public HomeController(IUserRepository userRepository, MBS_DBContext dbContext)
        {
            repository = userRepository;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //_dbContext.Users.addRange(user);
            //_dbContext.SaveChanges
            //_dbContext.Teachers.Remove(repository.Users.Where(r => r.ID == user.ID).FirstOrDefault());
            /*var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("email", "password"),
                EnableSsl = true,
            };

            smtpClient.Send("email", "recipient", "subject", "body");*/
            return View(repository.Users);
        }
    }
}
