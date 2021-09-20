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
        private ITestimonyRepository testimonyRepository;
        private ICommentRepository commentRepository;
        private IWebsiteContentsRepository contentsRepository;
        private IImageModelRepository imageRepository;
        public HomeController(IMailingListRepository userRepository, MBS_DBContext dbContext, IBlogRepository blog, ITestimonyRepository tRepository, ICommentRepository comment, IWebsiteContentsRepository contents, IImageModelRepository iRepository)
        {
            mailingListRepository = userRepository;
            _dbContext = dbContext;
            blogRepository = blog;
            testimonyRepository = tRepository;
            commentRepository = comment;
            contentsRepository = contents;
            imageRepository = iRepository;
        }
        public IActionResult Index()
        {
            WebsiteContents homePageContents = contentsRepository.Content;
            ViewData["talkToUsText"] = homePageContents.HomePageTalkToUsText;
            ViewData["meetUsText"] = homePageContents.HomePageMeetUsText;
            ViewData["connectWithUsText"] = homePageContents.HomePageConnectWithUsText;
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
            //List<ImageModel> images = new List<ImageModel>();
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
            WebsiteContents contactUsContents = contentsRepository.Content;
            ViewData["emailAddress"] = contactUsContents.CompanyEmailAddress;
            ViewData["phoneNumber"] = contactUsContents.CompanyPhoneNumber;
            ViewData["companyOfficeAddress"] = contactUsContents.CompanyHeadquarters;
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
    }
}
