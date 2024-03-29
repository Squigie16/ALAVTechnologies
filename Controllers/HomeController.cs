﻿using System;
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
        private IPropertyListingRepository propertyRepository;
        public HomeController(IMailingListRepository userRepository, MBS_DBContext dbContext, IBlogRepository blog, ICommentRepository comment, IWebsiteContentsRepository contents, IImageModelRepository iRepository, ITestimonyRepository tRepository, SignInManager<IdentityUser> signIn, IPropertyListingRepository listingRepository)
        {
            mailingListRepository = userRepository;
            _dbContext = dbContext;
            blogRepository = blog;
            testimonyRepository = tRepository;
            commentRepository = comment;
            contentsRepository = contents;
            imageRepository = iRepository;
            signInManager = signIn;
            propertyRepository = listingRepository;
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
            List<Blog> blogs = blogRepository.Blogs.ToList();
            List<ImageModel> allImages = imageRepository.Images.ToList();
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
        public IActionResult Listings()
        {
            List<PropertyListing> listings = propertyRepository.PropertyListings.ToList();
            List<ImageModel> allImages = imageRepository.Images.ToList();
            foreach (PropertyListing pl in listings)
            {
                foreach (ImageModel i in allImages)
                {
                    if (pl.ImageID == i.ImageId)
                    {
                        pl.Image = i;
                    }
                }
            }
            return View(listings);
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
            ViewData["AboutUsLloyd"] = contentsRepository.Content.AboutUsLloyd;
            ViewData["AboutUsStephanie"] = contentsRepository.Content.AboutUsStephanie;
            return View();
        }

        public IActionResult Blog(int id)
        {
            Blog blog = new Blog();
            List<Blog> blogs = blogRepository.Blogs.ToList();
            foreach (Blog b in blogs)
            {
                if (id == b.ID)
                {
                    blog = b;
                    break;
                }
            }

            List<Comment> commentsOnPost = new List<Comment>();

            foreach (Comment c in commentRepository.Comments.ToList())
            {
                if(c.BlogID == blog.ID)
                {
                    commentsOnPost.Add(c);
                }
            }

            ImageModel image = new ImageModel();

            foreach (ImageModel i in imageRepository.Images.ToList())
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
            List<Blog> blogs = blogRepository.Blogs.ToList();
            Blog blog = new Blog();

            foreach(Blog b in blogs)
            {
                if(comment.BlogID == b.ID)
                {
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

                ModelState.AddModelError("error", "Invalid Login Attempt");
                
            }

            return View();
        }

        public IActionResult AddTestimony()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTestimony(Testimony testimony)
        {
            testimony.DateOfPost = DateTime.Now;
            testimonyRepository.AddTestimony(testimony);

            return RedirectToAction("ContactUs");
        }
    }
}
