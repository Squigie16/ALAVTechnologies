using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LloydStephanieRealty.Models;
using Microsoft.Extensions.DependencyInjection;
//using System.Net.Mail;
using System.Net;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace LloydStephanieRealty.Controllers
{
    public class HomeController : Controller
    {
        private IMailingListRepository mailingListRepository;
        private MBS_DBContext _dbContext;
        private IBlogRepository blogRepository;
        private ICommentRepository commentRepository;
        private IWebsiteContentsRepository contentsRepository;
        public HomeController(IMailingListRepository userRepository, MBS_DBContext dbContext, IBlogRepository blog, ICommentRepository comment, IWebsiteContentsRepository contents)
        {
            mailingListRepository = userRepository;
            _dbContext = dbContext;
            blogRepository = blog;
            commentRepository = comment;
            contentsRepository = contents;
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
            return View();
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        /*
        [HttpPost]
        public ViewResult Create(MailingListUser user)
        {
            _dbContext.MailingListUsers.AddRange(user);
            _dbContext.SaveChanges();
            return View("index", repository.Users);
        }
        [HttpGet]
        public ViewResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ViewResult LoginPage(MailingListUser user)
        {
            /*
            string pass = _dbContext.MailingListUsers.Where(u => u.Username == user.Username)
                   .Select(u => u.Password)
                   .FirstOrDefault();
            if (pass == user.Password)
            {
                return View("index", repository.Users);
            }
            
            return View();
        }

        public ViewResult SignUpMailingList()
        {
           return View();
        }

        */
        public IActionResult Blogs()
        {
            IQueryable<Blog> blogs = blogRepository.Blogs;
            return View(blogs);
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            WebsiteContents aboutUsContents = contentsRepository.Content;
            ViewData["AboutUsHeader"] = aboutUsContents.AboutUsHeader;
            ViewData["AboutUsPara"] = aboutUsContents.AboutUsParagraph;
            return View();
        }

        public IActionResult Blog(int id)
        {
            Blog blog = new Blog();
            IQueryable<Blog> blogs = blogRepository.Blogs;
            foreach (Blog b in blogs)
            {
                if (id == b.ID)
                {
                    blog = b;
                    break;
                }
            }

            List<Comment> commentsOnPost = new List<Comment>();

            foreach (Comment c in commentRepository.Comments)
            {
                if(c.BlogID == blog.ID)
                {
                    commentsOnPost.Add(c);
                }
            }

            ViewData["Comments"] = commentsOnPost;
            ViewData["Blog"] = blog;
            return View();
        }

        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            IQueryable<Blog> blogs = blogRepository.Blogs;
            Blog blog = new Blog();

            foreach(Blog b in blogs)
            {
                if(comment.BlogID == b.ID)
                {
                    //b.Comments.Add(comment);
                    blog = b;
                    break;
                }
            }
            comment.DateOfComment = DateTime.Now;
            commentRepository.AddComment(comment);

            return RedirectToAction("Blog", new { @id = blog.ID });
        }

        [HttpPost]
        public IActionResult AddMailingListUser(MailingListUser user)
        {
            mailingListRepository.AddUser(user);

            string emailSubject = "Test Email Subject";
            string emailContents = "<h1>WELCOME TO THE NEWSLETTER</h1>";

            EmailToCustomer email = new EmailToCustomer();

            email.SendConfirmationEmail(emailSubject, emailContents, user.Email);

            return RedirectToAction("ContactUs");
        }
    }
}
