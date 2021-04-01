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
        private IBlogRepository blogRepository;
        private ICommentRepository commentRepository;
        public HomeController(IUserRepository userRepository, MBS_DBContext dbContext, IBlogRepository blog, ICommentRepository comment)
        {
            repository = userRepository;
            _dbContext = dbContext;
            blogRepository = blog;
            commentRepository = comment;
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
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Create(User user)
        {
            _dbContext.Users.AddRange(user);
            _dbContext.SaveChanges();
            return View("index", repository.Users);
        }
        [HttpGet]
        public ViewResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ViewResult LoginPage(User user)
        {
            string pass = _dbContext.Users.Where(u => u.Username == user.Username)
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
        public IActionResult Blogs()
        {
            IQueryable<Blog> blogs = blogRepository.Blogs;
            return View(blogs);
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

            IQueryable<Comment> comments = commentRepository.Comments;
            List<Comment> commentsOnPost = new List<Comment>();

            foreach (Comment c in comments)
            {
                if(c.BlogID == blog.ID)
                {
                    commentsOnPost.Add(c);
                    Console.WriteLine("CommentBlogID: " + c.BlogID);
                }
            }

            Console.WriteLine("Number of Comments: " + commentsOnPost.Count);

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
            Console.WriteLine("Comment Added");
            Console.WriteLine("Name: " + comment.Name);
            Console.WriteLine("Desc. : " + comment.Description);
            Console.WriteLine("BlogID :" + comment.BlogID);
            commentRepository.AddComment(comment);

            return RedirectToAction("Blog", new { @id = blog.ID });
        }
    }
}
