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
using Microsoft.AspNetCore.Identity;

namespace LloydStephanieRealty.Controllers
{
    public class HomeController : Controller
    {
        private IMailingListRepository mailingListRepository;
        private MBS_DBContext _dbContext;
        private IBlogRepository blogRepository;
        private ITestimonyRepository testimonyRepository;
        private ICommentRepository commentRepository;
        private IWebsiteContentsRepository contentsRepository;
        private IImageModelRepository imageRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        public HomeController(IMailingListRepository userRepository, MBS_DBContext dbContext, IBlogRepository blog, ICommentRepository comment, IWebsiteContentsRepository contents, IImageModelRepository iRepository, ITestimonyRepository tRepository, SignInManager<IdentityUser> signIn)
        {
            mailingListRepository = userRepository;
            _dbContext = dbContext;
            blogRepository = blog;
            testimonyRepository = tRepository;
            commentRepository = comment;
            contentsRepository = contents;
            imageRepository = iRepository;
            signInManager = signIn;
        }
        public IActionResult Index()
        {
            ViewData["talkToUsText"] = contentsRepository.Content.HomePageTalkToUsText;
            ViewData["meetUsText"] = contentsRepository.Content.HomePageMeetUsText;
            ViewData["connectWithUsText"] = contentsRepository.Content.HomePageConnectWithUsText;
            return View();
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
    
        public IActionResult Blogs()
        {
            IQueryable<Blog> blogs = blogRepository.Blogs;
            IQueryable<ImageModel> allImages = imageRepository.Images;
            foreach(Blog b in blogs)
            {
                foreach(ImageModel i in allImages)
                {
                    if(b.ImageID == i.ImageId)
                    {
                        b.Image = i;
                    }
                }
            }
            return View(blogs);
        }
        public IActionResult ContactUs()
        {
            ViewData["emailAddress"] = contentsRepository.Content.CompanyEmailAddress;
            ViewData["phoneNumber"] = contentsRepository.Content.CompanyPhoneNumber;
            ViewData["companyOfficeAddress"] = contentsRepository.Content.CompanyHeadquarters;
            return View();
        }

        public IActionResult AboutUs()
        {
            ViewData["AboutUsHeader"] = contentsRepository.Content.AboutUsHeader;
            ViewData["AboutUsPara"] = contentsRepository.Content.AboutUsParagraph;
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

            ImageModel image = new ImageModel();

            foreach (ImageModel i in imageRepository.Images)
            {
                if(blog.ImageID == i.ImageId)
                {
                    image = i;
                    break;
                }
            }

            ViewData["Image"] = image;
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            Console.WriteLine("Username: " + model.Username);
            Console.WriteLine("Password: " + model.Password);

            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("AdminIndex", "Admin");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
               
            }

            return RedirectToPage("/Login", model);
        }
    }
}
